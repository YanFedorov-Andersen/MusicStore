using MusicStore.Business.Interfaces;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.DataAccess.Realization;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Business.Services
{
    public class MusicStoreDisplayService: IMusicStoreDisplayService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Song> _songRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IGenericRepositoryWithPagination<Album> _albumRepository;

        private readonly IMapper<Song, Domain.DataTransfer.Song> _mapSong;
        private readonly IMapper<Album, Domain.DataTransfer.Album> _mapAlbum;

        private readonly ISongStoreRepository _songStoreRepository;


        public MusicStoreDisplayService(IUnitOfWork unitOfWork, IMapper<Song, Domain.DataTransfer.Song> mapSong, IMapper<Album, Domain.DataTransfer.Album> mapAlbum)
        {
            _unitOfWork = unitOfWork;
            _songRepository = unitOfWork.SongRepository;
            _userRepository = unitOfWork.UserAccountRepository;
            _songStoreRepository = unitOfWork.SongStoreRepository;
            _albumRepository = unitOfWork.AlbumRepositoryWithPagination;

            _mapSong = mapSong;
            _mapAlbum = mapAlbum;
        }

        public IList<Domain.DataTransfer.Song> DisplayAllAvailableSongs(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException($"{nameof(userId)} is less then 1 in musicStoreService DisplayAllAvailableSongs", nameof(userId));
            }

            var songsAvailableForBuyByUser = _songStoreRepository.GetSongsAvailableToBuyByUser(userId);

            if (songsAvailableForBuyByUser == null)
            {
                throw new Exception("not available for buying");
            }
            var availableForUserBuySongsList = songsAvailableForBuyByUser.Select(_mapSong.AutoMap).ToList();

            return availableForUserBuySongsList;
        }

        public IList<Domain.DataTransfer.Song> DisplayAllSongs()
        {
            var domainSongList = _songRepository.GetItemList().Select(_mapSong.AutoMap).ToList();
            return domainSongList;
        }

        public IndexViewItem<Domain.DataTransfer.Album> DisplayAlbumsWithPagination(int page = 1, int pageSize = 10)
        {
            var albumsList = _albumRepository.GetItemList();

            if (albumsList == null)
            {
                throw new Exception($"{nameof(albumsList)} equal null");
            }

            var resultOfPagination = _albumRepository.MakePagination(albumsList.ToList(), page, pageSize);

            if (resultOfPagination == null)
            {
                throw new Exception($"{nameof(resultOfPagination)} less then 0");
            }

            IndexViewItem<Domain.DataTransfer.Album> indexViewDomainAlbums = new IndexViewItem<Domain.DataTransfer.Album>();
            indexViewDomainAlbums.PageInfo = new Repository<Domain.DataTransfer.Album>();
            indexViewDomainAlbums.PageInfo.PageNumber = resultOfPagination.PageInfo.PageNumber;
            indexViewDomainAlbums.PageInfo.PageSize = resultOfPagination.PageInfo.PageSize;
            indexViewDomainAlbums.PageInfo.TotalItems = resultOfPagination.PageInfo.TotalItems;
            indexViewDomainAlbums.Items = resultOfPagination.Items.Select(_mapAlbum.AutoMap);
            return indexViewDomainAlbums;
        }

        public IList<Domain.DataTransfer.Song> GetSongsListFromAlbum(int albumId)
        {
            if (albumId <= 0)
            {
                throw new ArgumentException($"{nameof(albumId)} is less then 1 in musicStoreService DisplayAllAvailableSongs", nameof(albumId));
            }

            var album = _albumRepository.GetItem(albumId);

            if(album == null || album.Songs == null)
            {
                throw new Exception("album is null or album.Songs is null");
            }

            var domainSongsList = album.Songs.Select(_mapSong.AutoMap).ToList();

            return domainSongsList;
        }

        public IList<Domain.DataTransfer.Song> GetSongsListFromAlbumAvailableForBuyByUser(int albumId, int userId)
        {
            if (albumId <= 0)
            {
                throw new ArgumentException($"{nameof(albumId)} is less then 1 in musicStoreDisplayService GetSongsListAvailableForBuyByUser", nameof(albumId));
            }

            if (userId <= 0)
            {
                throw new ArgumentException($"{nameof(userId)} is less then 1 in musicStoreDisplayService GetSongsListAvailableForBuyByUser", nameof(userId));
            }

            var user = _userRepository.GetItem(userId);

            if (user == null || user.BoughtSongs == null)
            {
                throw new Exception("user is null or user.BoughtSongs is null");
            }

            var album = _albumRepository.GetItem(albumId);
           
            if (album == null || album.Songs == null)
            {
                throw new Exception("album is null or album.Songs is null");
            }

            var domainSongsList = new List<Domain.DataTransfer.Song>();

            foreach (var albumSong in album.Songs)
            {
                var coincidence = user.BoughtSongs.Any(x => x.Song.Id == albumSong.Id);

                if (!coincidence)
                {
                    domainSongsList.Add(_mapSong.AutoMap(albumSong));
                }
            }

            return domainSongsList;
        }
    }
}
