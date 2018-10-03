using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            modelBuilder.Entity<User>().Property(p => p.FirstName).HasMaxLength(20);
            modelBuilder.Entity<User>().Property(p => p.LastName).HasMaxLength(20);
            modelBuilder.Entity<Genre>().Property(p => p.Name).HasMaxLength(20);
            modelBuilder.Entity<Song>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Album>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Address>().Property(p => p.Continent).HasMaxLength(20);
            modelBuilder.Entity<Address>().Property(p => p.Country).HasMaxLength(20);
            modelBuilder.Entity<Address>().Property(p => p.City).HasMaxLength(40);
            modelBuilder.Entity<Address>().Property(p => p.Street).HasMaxLength(40);
            modelBuilder.Entity<Address>().Property(p => p.House).HasMaxLength(10);

            modelBuilder.Entity<User>().Property(p => p.Money).HasPrecision(6, 2);
            modelBuilder.Entity<Song>().Property(p => p.Price).HasPrecision(3, 2);
            modelBuilder.Entity<Album>().Property(p => p.DiscountIfBuyAllSongs).HasPrecision(2, 1);
            modelBuilder.Entity<BoughtSong>().Property(p => p.BoughtPrice).HasPrecision(3, 2);

            modelBuilder.Entity<User>().ToTable("UserAccount");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<BoughtSong>().ToTable("BoughtSong");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Artist>().ToTable("Artist");
            base.OnModelCreating(modelBuilder);
        }

    }
}
