using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;
using System.Linq;

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

        public bool IsDiscountAvailable(int userId, int albumId)
        {
            if (albumId <= 0 || userId <= 0)
            {
                throw new ArgumentException("userId is less then 1 or albumId is less then 1 in musicStoreService in BuySong", "userId or albumId");
            }

            User user = _userRepository.GetItem(userId);

            if (user == null)
            {
                throw new Exception("Can not find user in db");
            }

            Album album = _albumRepository.GetItem(albumId);

            if (album == null)
            {
                throw new Exception("Can not find album in db");
            }

            foreach (var albumSong in album.Songs)
            {
                if (user.BoughtSongs.Any(x => x.Song.Id == albumSong.Id))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
