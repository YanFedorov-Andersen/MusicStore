using MusicStore.DataAccess.Interfaces;

namespace MusicStore.DataAccess.Realization
{
    public class UnitOfWork: IUnitOfWork
    {
        private MusicStoreContext _musicStoreContext = new MusicStoreContext();
        private IRepository<User> _userRepository;
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
    }
}
