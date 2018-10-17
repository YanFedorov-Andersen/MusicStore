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

        IAdminRepository AdminRepository { get; }
        IRepository<Album> AlbumRepository { get; }
    }
}
