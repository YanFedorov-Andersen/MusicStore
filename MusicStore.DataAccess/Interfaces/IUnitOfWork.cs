namespace MusicStore.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserAccount
        {
            get;
        }
        IRepository<BoughtSong> BoughtSong
        {
            get;
        }
        IRepository<Song> Song
        {
            get;
        }
        ISongStoreRepository SongStore
        {
            get;
        }

        IUserAccountRepository UserAccountRepository { get; }
    }
}
