namespace MusicStore.DataAccess.Interfaces
{
    public interface IUserAccountRepository
    {
        bool CreateWithGuidId(string Id);
        User GetItemWithGuidId(string id);
        int GetUserId(string identityId);
    }
}
