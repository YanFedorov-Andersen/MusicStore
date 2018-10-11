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

        public List<User> NotActiveUsers()
        {
            return _dataBase.Users.Where(x => x.IsActive == false).ToList();
        }
        public List<User> ActiveUsers()
        {
            return _dataBase.Users.Where(x => x.IsActive == true).ToList();
        }
    }
}
