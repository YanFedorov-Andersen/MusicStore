using MusicStore.Business.Interfaces;
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
                throw new ArgumentException("userId is less then 1 or songId is less then 1 in musicStoreService in BuySong", "userId or songId");
            }

            User user = _userRepository.GetItem(userId);

            if(user == null)
            {
                throw new Exception("User is null");
            }

            Song song = _songRepository.GetItem(songId);

            if (song == null)
            {
                throw new Exception("Song is null");
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

        public bool CheckIfMoneyEnough(int userId, decimal totalSum, decimal discount = 0)
        {
            if (userId <= 1)
            {
                throw  new ArgumentException($"{nameof(userId)} is less then 1", nameof(userId));
            }

            if (totalSum <= 0)
            {
                throw new ArgumentException($"{nameof(totalSum)} is less then 0 or equal", nameof(totalSum));
            }

            var user = _userRepository.GetItem(userId);

            if (discount == 0)
            {
                if (user.Money >= totalSum)
                {
                    return true;
                }
            }
            else if (discount > 0)
            {
                if (user.Money >= (totalSum - (totalSum * (discount / 100))))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
