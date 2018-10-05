using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.DataAccess.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using TheWitcher.Domain;
using TheWitcher.Domain.Models;

namespace MusicStore.Business.Services
{
    public class MusicStoreService: IMusicStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Song> _songRepository;
        private readonly IRepository<BoughtSong> _boughtSongRepository;

        private readonly IMapper<Song, SongDTO> _mapSong;

        public MusicStoreService(IUnitOfWork unitOfWork, IMapper<Song, SongDTO> mapSong)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserAccount;
            _songRepository = unitOfWork.Song;
            _boughtSongRepository = unitOfWork.BoughtSong;

            _mapSong = mapSong;
        }

        public List<SongDTO> DisplayAllAvailableSongs(int userId)
        {
            if (userId < 0)
            {
                return null;
            }

            var availableForUserBuySongsList = ((_songRepository as SongRepository).GetAvailableSongsForBuyByUser(userId)).Select(_mapSong.AutoMap).ToList();
            return availableForUserBuySongsList;
        }

        public bool BuySong(int songId, int userId)
        {
            if (songId < 0 || userId < 0)
            {
                return false;
            }
            User user;
            Song song;
            try
            {
                user = _userRepository.GetItem(userId);
                song = _songRepository.GetItem(songId);
            }
            catch(NullReferenceException exception)
            {
                throw new NullReferenceException("Something went wrong with DI", exception);
            }

            if (user == null || song == null || user.Money < song.Price)
            {
                return false;
            }

            BoughtSong boughtSong = new BoughtSong()
            {
                BoughtPrice = song.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
                User = user
            };
            user.Money -= song.Price;
            _boughtSongRepository.Create(boughtSong);
            _userRepository.Update(user);
            return true;
        }
    }
}
