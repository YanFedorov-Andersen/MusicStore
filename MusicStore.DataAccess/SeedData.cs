using System;
using System.Linq;

namespace MusicStore.DataAccess
{
    public static class SeedData
    {
        public static void Initialize(MusicStoreContext _modelContext)
        {
            _modelContext.Database.Exists();
            var address1 = new Address()
            {
                Id = 0,
                Continent = "Europe",
                Country = "France",
                City = "Paris",
                Street = "Napoleon",
                House = "3B",
                Flat = 3
            };
            var address2 = new Address()
            {
                Id = 1,
                Continent = "Asia",
                Country = "China",
                City = "Hong Kong",
                Street = "Street",
                House = "567K",
                Flat = 2527
            };

            if (!_modelContext.Addresses.Any())
            {
                _modelContext.Addresses.Add(address1);
                _modelContext.Addresses.Add(address1);
            }


            var artist1 = new Artist()
            {
                Id = 0,
                FirstName = "Artist",
                LastName = "1",
            };
            var artist2 = new Artist()
            {
                Id = 1,
                FirstName = "Artist",
                LastName = "2",
            };

            if (!_modelContext.Addresses.Any())
            {
                _modelContext.Artists.Add(artist1);
                _modelContext.Artists.Add(artist1);
            }


            var album1 = new Album()
            {
                Id = 0,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            var album2 = new Album()
            {
                Id = 1,
                Name = "Album 2",
                DiscountIfBuyAllSongs = 7.5m,
            };
            var album3 = new Album()
            {
                Id = 2,
                Name = "Album 3",
                DiscountIfBuyAllSongs = 7.5m,
            };
            var album4 = new Album()
            {
                Id = 3,
                Name = "Album 4",
                DiscountIfBuyAllSongs = 7.5m,
            };
            var album5 = new Album()
            {
                Id = 4,
                Name = "Album 5",
                DiscountIfBuyAllSongs = 7.5m,
            };
            var album6 = new Album()
            {
                Id = 5,
                Name = "Album 6",
                DiscountIfBuyAllSongs = 7.5m,
            };
            var album7 = new Album()
            {
                Id = 6,
                Name = "Album 7",
                DiscountIfBuyAllSongs = 7.5m,
            };

            if (!_modelContext.Addresses.Any())
            {
                _modelContext.Albums.Add(album1);
                _modelContext.Albums.Add(album2);
            }
            _modelContext.Albums.Add(album3);
            _modelContext.Albums.Add(album4);
            _modelContext.Albums.Add(album5); 
            _modelContext.Albums.Add(album6);
            _modelContext.Albums.Add(album7);


            var genre1 = new Genre()
            {
                Id = 0,
                Name = "Clasic"
            };
            var genre2 = new Genre()
            {
                Id = 1,
                Name = "Rock"
            };

            if (!_modelContext.Genres.Any())
            {
                _modelContext.Genres.Add(genre1);
                _modelContext.Genres.Add(genre2);
            }


            var song1 = new Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
                Album = album1,
                Artist = artist1,
                Genre = genre1
            };
            var song2 = new Song()
            {
                Id = 1,
                Name = "All world again you",
                Price = 2.99m,
                Album = album2,
                Artist = artist2,
                Genre = genre2
            };
            var song3 = new Song()
            {
                Id = 2,
                Name = "All world do not see you",
                Price = 3.99m,
                Album = album2,
                Artist = artist2,
                Genre = genre2
            };
            var song4 = new Song()
            {
                Id = 3,
                Name = "4",
                Price = 4.99m,
                Album = album5,
                Artist = artist2,
                Genre = genre2
            };
            var song5 = new Song()
            {
                Name = "5",
                Price = 4.99m,
                Album = album2,
                Artist = artist2,
                Genre = genre2
            };
            var song6 = new Song()
            {
                Name = "6",
                Price = 4.99m,
                Album = album7,
                Artist = artist2,
                Genre = genre2
            };
            var song7 = new Song()
            {
                Name = "7",
                Price = 4.99m,
                Album = album7,
                Artist = artist2,
                Genre = genre2
            };
            if (!_modelContext.Songs.Any())
            {
                _modelContext.Songs.Add(song1);
                _modelContext.Songs.Add(song2);
                _modelContext.Songs.Add(song3);

            }
            _modelContext.Songs.Add(song4);
            _modelContext.Songs.Add(song5);
            _modelContext.Songs.Add(song6);
            _modelContext.Songs.Add(song7);


            var user1 = new User()
            {
                Id = 0,
                Address = address1,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            var user2 = new User()
            {
                Id = 1,
                Address = address2,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 3.56m,
            };

            if (!_modelContext.Users.Any())
            {
                _modelContext.Users.Add(user1);
                _modelContext.Users.Add(user2);
            }


            var boughtSong1 = new BoughtSong()
            {
                Id = 0,
                User = user1,
                IsVisible = true,
                Song = song1,
                BoughtPrice = song1.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };
            var boughtSong2 = new BoughtSong()
            {
                Id = 1,
                User = user1,
                IsVisible = true,
                Song = song2,
                BoughtPrice = song2.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };

            if (!_modelContext.BoughtSongs.Any())
            {
                _modelContext.BoughtSongs.Add(boughtSong1);
                _modelContext.BoughtSongs.Add(boughtSong2);
            }
            artist1.Songs.Add(song1);
            artist2.Songs.Add(song2);

            _modelContext.SaveChanges();
        }
    }
}