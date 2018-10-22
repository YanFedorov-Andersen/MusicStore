using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;

namespace MusicStore.Business.Services
{
    public class DiscountService: IDiscountService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IGenericRepositoryWithPagination<Album> _albumRepository;
        public DiscountService(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.UserAccountRepository;
            _albumRepository = unitOfWork.AlbumRepositoryWithPagination;        
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
