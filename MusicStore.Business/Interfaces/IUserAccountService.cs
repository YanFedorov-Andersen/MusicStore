using MusicStore.Domain.DataTransfer;

namespace MusicStore.Business.Interfaces
{
    public interface IUserAccountService
    {
        bool RegisterUserAccount(string Id);
        bool EditUserAccount(UserAccount user);
        Domain.DataTransfer.UserAccount GetUserData(string userGuidId);
        int ConvertGuidInStringIdToIntId(string id);
        bool CheckIfActive(string identityId);
    }
}
