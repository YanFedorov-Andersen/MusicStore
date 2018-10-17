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
        private IRepository<Album> _albumRepository;
        public IRepository<User> UserAccount
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
        public IRepository<BoughtSong> BoughtSong
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
        public IRepository<Song> Song
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
        public ISongStoreRepository SongStore
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
        public IRepository<Album> AlbumRepository
        {
            get
            {
                if (_albumRepository == null)

                {
                    _albumRepository = new Repository<Album>(_musicStoreContext);
                }
                return _albumRepository;
            }

        }
    }
}
