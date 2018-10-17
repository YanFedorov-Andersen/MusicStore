using System.Collections.Generic;
using System.Linq;
using MusicStore.DataAccess.Interfaces;

namespace MusicStore.DataAccess.Realization
{
    public class AdminRepository: IAdminRepository
    {
        private readonly MusicStoreContext _dataBase;

        public AdminRepository(MusicStoreContext dataBase)
        {
            _dataBase = dataBase;
        }

        public IList<User> GetActiveOrNotActiveUsers(bool isActive)
        {
            return _dataBase.Users.Where(x => x.IsActive == isActive).ToList();
        }
    }
}
