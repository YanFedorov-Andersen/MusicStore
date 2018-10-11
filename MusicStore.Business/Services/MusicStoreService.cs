using MusicStore.Business.Interfaces;
using MusicStore.DataAccess.Interfaces;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using MusicStore.DataAccess;

namespace MusicStore.Business.Services
{
    public class MusicStoreService: IMusicStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Song> _songRepository;
        private readonly IRepository<BoughtSong> _boughtSongRepository;

        private readonly IMapper<Song, Domain.DataTransfer.Song> _mapSong;
        private readonly IMapper<BoughtSong, Domain.DataTransfer.BoughtSong> _mapBoughtSong;

        private readonly ISongStoreRepository _songStoreRepository;

        public MusicStoreService(IUnitOfWork unitOfWork, IMapper<Song, Domain.DataTransfer.Song> mapSong, IMapper<BoughtSong, Domain.DataTransfer.BoughtSong> mapBoughtSong)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserAccount;
            _songRepository = unitOfWork.Song;
            _boughtSongRepository = unitOfWork.BoughtSong;
            _songStoreRepository = unitOfWork.SongStore;

            _mapSong = mapSong;
            _mapBoughtSong = mapBoughtSong;
        }

        public List<Domain.DataTransfer.Song> DisplayAllAvailableSongs(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentException("userId < 0 in musicStoreService DisplayAllAvailableSongs");
            }

            var availableForUserBuySongsList = _songStoreRepository.GetAvailableSongsForBuyByUser(userId).Select(_mapSong.AutoMap).ToList();
            return availableForUserBuySongsList;
        }

        public Domain.DataTransfer.BoughtSong BuySong(int songId, int userId)
        {
            if (songId < 0 || userId < 0)
            {
                throw new ArgumentException("userId < 0 or songId < 0 in musicStoreService in BuySong");
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
                return null;
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
            var result = _mapBoughtSong.AutoMap(boughtSong);
            return result;
        }

        public List<Domain.DataTransfer.Song> DisplayAllSongs()
        {
            List<Domain.DataTransfer.Song> domainSongList= new List<Domain.DataTransfer.Song>();
            domainSongList = _songRepository.GetItemList().Select(_mapSong.AutoMap).ToList();
            return domainSongList;
        }
    }
}
