namespace MusicStore.DataAccess.Interfaces
{
    public interface IUserAccountRepository
    {
        bool CreateWithGuidId(string Id);
    }
}
