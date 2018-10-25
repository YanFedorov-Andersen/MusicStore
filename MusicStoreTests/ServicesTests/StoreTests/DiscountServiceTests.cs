using Moq;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;
using Xunit;

namespace MusicStoreTests.ServicesTests
{
    public class DiscountServiceTests
    {
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IRepository<User>> mockUserRepository;
        private readonly Mock<IGenericRepositoryWithPagination<Album>> mockAlbumRepository;

        private const int DEFAULT_USER_ID = 1;
        private const int DEFAULT_ALBUM_ID = 1;
        public DiscountServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUserRepository = new Mock<IRepository<User>>();
            mockAlbumRepository = new Mock<IGenericRepositoryWithPagination<Album>>();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(1, -1)]
        [InlineData(-1, 1)]
        public void CheckDiscountAvailableTestsByWrongParameters(int userId, int albumId)
        {
            //Arrange
            var discountService = new DiscountService(mockUnitOfWork.Object);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => discountService.IsDiscountAvailable(userId, albumId));

            //Assert
            Assert.Equal("userId is less then 1 or albumId is less then 1 in musicStoreService in BuySong\r\nИмя параметра: userId or albumId", ex.Message);
        }
        [Fact]
        public void CheckDiscountAvailableTest()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_USER_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            var album = new Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            var song1 = new Song()
            {
                Id = 1,
                Name = "All world for you",
                Price = 1.99m,
                Album = album,
            };
            var song2 = new Song()
            {
                Id = 2,
                Name = "All world again you",
                Price = 2.99m,
                Album = album
            };
            var song3 = new Song()
            {
                Id = 3,
                Name = "All world do not see you",
                Price = 3.99m,
                Album = album
            };
            var song4 = new Song()
            {
                Id = 4,
                Name = "4",
                Price = 4.99m,
            };
            var song5 = new Song()
            {
                Id = 5,
                Name = "5",
                Price = 4.99m,
            };
            var boughtSong1 = new BoughtSong()
            {
                Id = 0,
                User = user,
                IsVisible = true,
                Song = song4,
                BoughtPrice = song4.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };
            var boughtSong2 = new BoughtSong()
            {
                Id = 1,
                User = user,
                IsVisible = true,
                Song = song5,
                BoughtPrice = song5.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };
            album.Songs.Add(song1);
            album.Songs.Add(song2);
            album.Songs.Add(song3);
            user.BoughtSongs.Add(boughtSong1);
            user.BoughtSongs.Add(boughtSong2);

            mockUnitOfWork.Setup(x => x.AlbumRepositoryWithPagination).Returns(mockAlbumRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);

            mockUserRepository.Setup(x => x.GetItem(DEFAULT_USER_ID)).Returns(user);
            mockAlbumRepository.Setup(x => x.GetItem(DEFAULT_ALBUM_ID)).Returns(album);
            var discountService = new DiscountService(mockUnitOfWork.Object);
            //Act
            var result = discountService.IsDiscountAvailable(DEFAULT_USER_ID, DEFAULT_ALBUM_ID);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckDiscountAvailableTestDataCollision()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_USER_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            var album = new Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            var song1 = new Song()
            {
                Id = 1,
                Name = "All world for you",
                Price = 1.99m,
                Album = album,
            };
            var song2 = new Song()
            {
                Id = 2,
                Name = "All world again you",
                Price = 2.99m,
                Album = album
            };
            var song3 = new Song()
            {
                Id = 3,
                Name = "All world do not see you",
                Price = 3.99m,
                Album = album
            };
            var song4 = new Song()
            {
                Id = 4,
                Name = "4",
                Price = 4.99m,
            };
            var song5 = new Song()
            {
                Id = 5,
                Name = "5",
                Price = 4.99m,
            };
            var boughtSong1 = new BoughtSong()
            {
                Id = 0,
                User = user,
                IsVisible = true,
                Song = song4,
                BoughtPrice = song4.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };
            var boughtSong2 = new BoughtSong()
            {
                Id = 1,
                User = user,
                IsVisible = true,
                Song = song5,
                BoughtPrice = song5.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };
            var boughtSong3 = new BoughtSong()
            {
                Id = 1,
                User = user,
                IsVisible = true,
                Song = song3,
                BoughtPrice = song3.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };
            album.Songs.Add(song1);
            album.Songs.Add(song2);
            album.Songs.Add(song3);
            user.BoughtSongs.Add(boughtSong1);
            user.BoughtSongs.Add(boughtSong2);
            user.BoughtSongs.Add(boughtSong3);

            mockUnitOfWork.Setup(x => x.AlbumRepositoryWithPagination).Returns(mockAlbumRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);

            mockUserRepository.Setup(x => x.GetItem(DEFAULT_USER_ID)).Returns(user);
            mockAlbumRepository.Setup(x => x.GetItem(DEFAULT_ALBUM_ID)).Returns(album);
            var discountService = new DiscountService(mockUnitOfWork.Object);

            //Act
            var result = discountService.IsDiscountAvailable(DEFAULT_USER_ID, DEFAULT_ALBUM_ID);

            //Assert
            Assert.False(result);
        }
    }
}
