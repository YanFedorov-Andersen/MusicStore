namespace MusicStore.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserAccount
        {
            get;
        }
    }
}
