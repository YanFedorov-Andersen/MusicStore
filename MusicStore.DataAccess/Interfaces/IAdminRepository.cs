using System.Collections.Generic;

namespace MusicStore.DataAccess.Interfaces
{
    public interface IAdminRepository
    {
        IList<User> ActiveOrNotActiveUsers(bool isActive);
    }
}
