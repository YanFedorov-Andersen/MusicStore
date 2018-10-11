using System.Collections.Generic;

namespace MusicStore.DataAccess.Interfaces
{
    public interface IAdminRepository
    {
        List<User> NotActiveUsers();
        List<User> ActiveUsers();
    }
}
