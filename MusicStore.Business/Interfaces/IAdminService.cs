using System.Collections.Generic;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IAdminService
    {
        IList<UserAccount> GetListOfUsers(bool isActive);
        IList<UserAccount> GetFullListOfUsers();
    }
}
