using MusicStore.DataAccess.FluentApiConfig;
using System.Data.Entity;

namespace MusicStore.DataAccess
{
    public class MusicStoreContext: DbContext
    {
        public MusicStoreContext(): base("DefaultConnection")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<BoughtSong> BoughtSongs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigFluentApi.ConfigUserAccount(modelBuilder);
            ConfigFluentApi.ConfigSong(modelBuilder);
            ConfigFluentApi.ConfigGenre(modelBuilder);
            ConfigFluentApi.ConfigBoughtSong(modelBuilder);
            ConfigFluentApi.ConfigArtist(modelBuilder);
            ConfigFluentApi.ConfigAlbum(modelBuilder);
            ConfigFluentApi.ConfigAddress(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
