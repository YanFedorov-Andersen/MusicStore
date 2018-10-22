﻿using MusicStore.Business.Interfaces;
using MusicStore.DataAccess.Interfaces;
using MusicStore.Domain;
using System;
using MusicStore.DataAccess;

namespace MusicStore.Business.Services
{
    public class MusicStoreService: IMusicStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Song> _songRepository;
        private readonly IRepository<BoughtSong> _boughtSongRepository;
        private readonly IGenericRepositoryWithPagination<Album> _albumRepository;

        private readonly IMapper<BoughtSong, Domain.DataTransfer.BoughtSong> _mapBoughtSong;

        public MusicStoreService(IUnitOfWork unitOfWork, IMapper<BoughtSong, Domain.DataTransfer.BoughtSong> mapBoughtSong)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserAccountRepository;
            _songRepository = unitOfWork.SongRepository;
            _boughtSongRepository = unitOfWork.BoughtSongRepository;
            _albumRepository = unitOfWork.AlbumRepositoryWithPagination;

            _mapBoughtSong = mapBoughtSong;

        }

        public Domain.DataTransfer.BoughtSong BuySong(int songId, int userId, decimal discountForBuyAllAlbum = 0)
        {
            if (songId <= 0 || userId <= 0)
            {
                throw new ArgumentException("userId <= 0 or songId <= 0 in musicStoreService in BuySong", "userId or songId");
            }

            User user = _userRepository.GetItem(userId);
            Song song = _songRepository.GetItem(songId);

            if (user == null || song == null)
            {
                return null;
            }
            if (user.Money < song.Price)
            {
                throw new Exception($"User has not enough money for buy {song.Name} song");
            }

            decimal songBoughtPrice = 0;
            songBoughtPrice = discountForBuyAllAlbum > 0 ? song.Price - (song.Price * (discountForBuyAllAlbum / 100)) : song.Price;

            BoughtSong boughtSong = new BoughtSong()
            {
                BoughtPrice = songBoughtPrice,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
                User = user
            };

            user.Money -= songBoughtPrice;
            _boughtSongRepository.Create(boughtSong);
            _userRepository.Update(user);

            var result = _mapBoughtSong.AutoMap(boughtSong);
            return result;
        }
        public bool CheckDiscountAvailable(int userId, int albumId)
        {
            if (albumId <= 0 || userId <= 0)
            {
                throw new ArgumentException("userId <= 0 or albumId <= 0 in musicStoreService in BuySong", "userId or albumId");
            }

            User user = _userRepository.GetItem(userId);
            Album album = _albumRepository.GetItem(albumId);

            if (user == null || album == null)
            {
                throw new Exception("Can not find user or album in db");
            }

            foreach (var albumSong in album.Songs)
            {
                foreach (var userSong in user.BoughtSongs)
                {
                    if (albumSong.Id == userSong.Song.Id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
