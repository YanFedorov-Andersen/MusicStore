using System.Collections.Generic;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IAdminService
    {
        IList<UserAccount> GetActiveOrNotActiveUsers(bool isActive);
        IList<UserAccount> GetFullListOfUsers();
        bool EditUserAccount(UserAccount userDomain);
    }
}
