﻿using MusicStore.DataAccess.Interfaces;

namespace MusicStore.DataAccess.Realization
{
    public class UnitOfWork: IUnitOfWork
    {
        private MusicStoreContext _musicStoreContext = new MusicStoreContext();
        private IRepository<User> _userRepository;
        private IRepository<BoughtSong> _boughtSongRepository;
        private IRepository<Song> _songRepository;
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
    }
}