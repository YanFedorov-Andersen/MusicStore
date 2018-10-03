using System.Data.Entity;

namespace MusicStore.DataAccess.FluentApiConfig
{
    public static class ConfigFluentApi
    {
        public static void ConfigUserAccount(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.FirstName).HasMaxLength(20);
            modelBuilder.Entity<User>().Property(p => p.LastName).HasMaxLength(20);
            modelBuilder.Entity<User>().Property(p => p.Money).HasPrecision(8, 2);
            modelBuilder.Entity<User>().ToTable("UserAccount");
        }

        public static void ConfigGenre(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(20);
            modelBuilder.Entity<Genre>().ToTable("Genre");
        }

        public static void ConfigSong(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Song>().Property(p => p.Price).HasPrecision(5, 2);
            modelBuilder.Entity<Song>().ToTable("Song");
        }

        public static void ConfigAlbum(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Album>().Property(p => p.DiscountIfBuyAllSongs).HasPrecision(3, 1);
            modelBuilder.Entity<Album>().ToTable("Album");
        }

        public static void ConfigAddress(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().Property(p => p.Continent).HasMaxLength(20);
            modelBuilder.Entity<Address>().Property(p => p.Country).HasMaxLength(20);
            modelBuilder.Entity<Address>().Property(p => p.City).HasMaxLength(40);
            modelBuilder.Entity<Address>().Property(p => p.Street).HasMaxLength(40);
            modelBuilder.Entity<Address>().Property(p => p.House).HasMaxLength(10);
            modelBuilder.Entity<Address>().ToTable("Address");
        }

        public static void ConfigBoughtSong(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoughtSong>().Property(p => p.BoughtPrice).HasPrecision(5, 2);
            modelBuilder.Entity<BoughtSong>().ToTable("BoughtSong");
        }

        public static void ConfigArtist(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().Property(p => p.FirstName).HasMaxLength(20);
            modelBuilder.Entity<Artist>().Property(p => p.LastName).HasMaxLength(20);
            modelBuilder.Entity<Artist>().ToTable("Artist");
        }
    }
}
