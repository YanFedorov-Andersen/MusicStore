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
        private readonly IGenericRepositoryWithPagination<Album> _albumRepository;

        private readonly IMapper<Song, Domain.DataTransfer.Song> _mapSong;
        private readonly IMapper<Album, Domain.DataTransfer.Album> _mapAlbum;

        private readonly ISongStoreRepository _songStoreRepository;


        public MusicStoreDisplayService(IUnitOfWork unitOfWork, IMapper<Song, Domain.DataTransfer.Song> mapSong, IMapper<Album, Domain.DataTransfer.Album> mapAlbum)
        {
            _unitOfWork = unitOfWork;
            _songRepository = unitOfWork.SongRepository;
            _songStoreRepository = unitOfWork.SongStoreRepository;
            _albumRepository = unitOfWork.AlbumRepositoryWithPagination;

            _mapSong = mapSong;
            _mapAlbum = mapAlbum;
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

        public IList<Domain.DataTransfer.Song> DisplayAllSongs()
        {
            List<Domain.DataTransfer.Song> domainSongList = new List<Domain.DataTransfer.Song>();
            domainSongList = _songRepository.GetItemList().Select(_mapSong.AutoMap).ToList();
            return domainSongList;
        }

        public IndexViewItem<Domain.DataTransfer.Album> DisplayAlbumsWithPagination(int page = 1)
        {
            var albumsList = _albumRepository.GetItemList();

            if (albumsList == null)
            {
                throw new ArgumentNullException(nameof(albumsList), $"{nameof(albumsList)} less then 0");
            }

            var resultOfPagination = _albumRepository.MakePagination(albumsList.ToList(), page);

            if (resultOfPagination == null)
            {
                throw new ArgumentNullException(nameof(resultOfPagination), $"{nameof(resultOfPagination)} less then 0");
            }

            IndexViewItem<Domain.DataTransfer.Album> indexViewDomainAlbums = new IndexViewItem<Domain.DataTransfer.Album>();
            indexViewDomainAlbums.PageInfo = new Repository<Domain.DataTransfer.Album>();
            indexViewDomainAlbums.PageInfo.PageNumber = resultOfPagination.PageInfo.PageNumber;
            indexViewDomainAlbums.PageInfo.PageSize = resultOfPagination.PageInfo.PageSize;
            indexViewDomainAlbums.PageInfo.TotalItems = resultOfPagination.PageInfo.TotalItems;
            indexViewDomainAlbums.Items = resultOfPagination.Items.Select(_mapAlbum.AutoMap);
            return indexViewDomainAlbums;
        }
    }
}
