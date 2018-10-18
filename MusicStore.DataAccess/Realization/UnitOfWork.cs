using MusicStore.DataAccess.Interfaces;

namespace MusicStore.DataAccess.Realization
{
    public class UnitOfWork: IUnitOfWork
    {
        private MusicStoreContext _musicStoreContext = new MusicStoreContext();
        private IRepository<User> _userRepository;
        private IRepository<BoughtSong> _boughtSongRepository;
        private IRepository<Song> _songRepository;
        private IAdminRepository _adminRepository;
        private IGenericRepositoryWithPagination<Album> _albumRepositoryWithPagination;
        private IGenericRepositoryWithPagination<Song>  _songRepositoryWithPagination;
        public IRepository<User> UserAccountRepository
        {
            get
            {
                if(_userRepository == null)
                {
                    _userRepository = new UserAccountRepository(_musicStoreContext);
                }
                return _userRepository;
            }
        }
        public IRepository<BoughtSong> BoughtSongRepository
        {
            get
            {
                if (_boughtSongRepository == null)
                {
                    _boughtSongRepository = new BoughtSongRepository(_musicStoreContext);
                }
                return _boughtSongRepository;
            }
        }
        public IRepository<Song> SongRepository
        {
            get
            {
                if (_songRepository == null)
                {
                    _songRepository = new SongRepository(_musicStoreContext);
                }
                return _songRepository;
            }
        }
        public ISongStoreRepository SongStoreRepository
        {
            get
            {
                if (_songRepository == null)
                {
                    _songRepository = new SongRepository(_musicStoreContext);
                }
                return _songRepository as ISongStoreRepository;
            }
        }

        public IAdminRepository AdminRepository
        {
            get
            {
                if (_adminRepository == null)
                {
                    _adminRepository = new AdminRepository(_musicStoreContext);
                }
                return _adminRepository;
            }
        }
        public IGenericRepositoryWithPagination<Album> AlbumRepositoryWithPagination
        {
            get
            {
                if (_albumRepositoryWithPagination == null)

                {
                    _albumRepositoryWithPagination = new Repository<Album>(_musicStoreContext);
                }
                return _albumRepositoryWithPagination;
            }

        }
        public IGenericRepositoryWithPagination<Song> SongRepositoryWithPagination
        {
            get
            {
                if (_songRepositoryWithPagination == null)

                {
                    _songRepositoryWithPagination = new Repository<Song>(_musicStoreContext);
                }
                return _songRepositoryWithPagination;
            }

        }
    }
}
