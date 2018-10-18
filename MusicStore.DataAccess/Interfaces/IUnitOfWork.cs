namespace MusicStore.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserAccountRepository
        {
            get;
        }
        IRepository<BoughtSong> BoughtSongRepository
        {
            get;
        }
        IRepository<Song> SongRepository
        {
            get;
        }
        ISongStoreRepository SongStoreRepository
        {
            get;
        }

        IAdminRepository AdminRepository { get; }
        IGenericRepositoryWithPagination<Album> AlbumRepositoryWithPagination { get; }
        IGenericRepositoryWithPagination<Song> SongRepositoryWithPagination { get; }
    }
}
