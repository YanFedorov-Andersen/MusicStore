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
        //TODO: SingleResponsibility? So many methods...
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Song> _songRepository;
        private readonly IRepository<BoughtSong> _boughtSongRepository;
        private readonly IRepository<Album> _albumRepository;

        private readonly IMapper<Song, Domain.DataTransfer.Song> _mapSong;
        private readonly IMapper<BoughtSong, Domain.DataTransfer.BoughtSong> _mapBoughtSong;
        private readonly IMapper<Album, Domain.DataTransfer.Album> _mapAlbum;

        private readonly ISongStoreRepository _songStoreRepository;

        private readonly IPagination<Domain.DataTransfer.Album> _pagination;

        public MusicStoreService(IUnitOfWork unitOfWork, IMapper<Song, Domain.DataTransfer.Song> mapSong, IMapper<BoughtSong, Domain.DataTransfer.BoughtSong> mapBoughtSong, IPagination<Domain.DataTransfer.Album> pagination, IMapper<Album, Domain.DataTransfer.Album> mapAlbum)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.UserAccount;
            _songRepository = unitOfWork.Song;
            _boughtSongRepository = unitOfWork.BoughtSong;
            _songStoreRepository = unitOfWork.SongStore;
            _albumRepository = unitOfWork.AlbumRepository;

            _mapSong = mapSong;
            _mapBoughtSong = mapBoughtSong;
            _mapAlbum = mapAlbum;

            _pagination = pagination;
        }

        public IList<Domain.DataTransfer.Song> DisplayAllAvailableSongs(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentException("userId < 0 in musicStoreService DisplayAllAvailableSongs", nameof(userId));
            }

            var songsAvailableForByByUser = _songStoreRepository.GetSongsAvailableToBuyByUser(userId);

            if (songsAvailableForByByUser == null)
            {
                throw new Exception("no available for buy songs");
            }
            var availableForUserBuySongsList = songsAvailableForByByUser.Select(_mapSong.AutoMap).ToList();

            return availableForUserBuySongsList;
        }

        public Domain.DataTransfer.BoughtSong BuySong(int songId, int userId)
        {
            if (songId < 0 || userId < 0)
            {
                throw new ArgumentException("userId < 0 or songId < 0 in musicStoreService in BuySong", "userId or songId");
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

        public IList<Domain.DataTransfer.Song> DisplayAllSongs()
        {
            List<Domain.DataTransfer.Song> domainSongList= new List<Domain.DataTransfer.Song>();
            domainSongList = _songRepository.GetItemList().Select(_mapSong.AutoMap).ToList();
            return domainSongList;
        }

        public IndexViewItem<Domain.DataTransfer.Album> DisplayAlbumsWithPagination(int page = 1)
        {
            var albumsList = _albumRepository.GetItemList();

            if(albumsList == null)
            {
                throw new ArgumentNullException(nameof(albumsList), $"{nameof(albumsList)} less then 0");
            }
            
            var resultOfPagination = _pagination.MakePagination(albumsList.Select(_mapAlbum.AutoMap).ToList(), page);
            
            if(resultOfPagination == null)
            {
                throw new ArgumentNullException(nameof(resultOfPagination), $"{nameof(resultOfPagination)} less then 0");
            }

            return resultOfPagination;
        }
    }
}
