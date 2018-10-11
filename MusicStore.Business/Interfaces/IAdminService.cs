using System.Collections.Generic;
using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IAdminService
    {
        List<UserAccount> GetListOfUsers(bool isActive);
        List<UserAccount> GetFullListOfUsers();
    }
}
