using Moq;
using MusicStore.Business.Services.Statistics;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace MusicStoreTests.ServicesTests.StatisticTests
{
    public class AdminStatisticServiceTests
    {
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IRepository<BoughtSong>> mockBoughtSongRepository;

        private const int DEFAULT_USER_ID = 1;

        public AdminStatisticServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockBoughtSongRepository = new Mock<IRepository<BoughtSong>>();
        }

        [Fact]
        public void GetStatisticByTotalMoneyEarnedForSomeTimeTest()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_USER_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
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
            user.BoughtSongs.Add(boughtSong1);
            user.BoughtSongs.Add(boughtSong2);

            var boughtSongList = new List<BoughtSong>()
            {
                boughtSong1, boughtSong2
            };

            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);
            mockBoughtSongRepository.Setup(x => x.GetItemList()).Returns(boughtSongList);
            var adminStatisticService = new AdminStatisticService(mockUnitOfWork.Object);

            //Act
            var result = adminStatisticService.GetStatisticByTotalMoneyEarnedForSomeTime(new DateTime(2018, 10, 1), new DateTime(2018, 10, 10));

            //Assert
            Assert.Equal(9.98m, result);
        }

        

        [Fact]
        public void GetStatisticByTotalMoneyEarnedForSomeTimeTestByArgumentException()
        {
            //Arrange
            var adminStatisticService = new AdminStatisticService(mockUnitOfWork.Object);

            //Act
            var result = Assert.Throws<ArgumentException>(() => adminStatisticService.GetStatisticByTotalMoneyEarnedForSomeTime(new DateTime(), new DateTime()));

            //Assert
            Assert.Equal("startDate or endDate is null\r\nИмя параметра: startDate' 'endDate", result.Message);
        }

        [Fact]
        public void GetStatisticByNumberOfSoldSongsTest()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_USER_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
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
            user.BoughtSongs.Add(boughtSong1);
            user.BoughtSongs.Add(boughtSong2);

            var boughtSongList = new List<BoughtSong>()
            {
                boughtSong1, boughtSong2
            };

            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);
            mockBoughtSongRepository.Setup(x => x.GetItemList()).Returns(boughtSongList);
            var adminStatisticService = new AdminStatisticService(mockUnitOfWork.Object);

            //Act
            var result = adminStatisticService.GetStatisticByNumberOfSoldSongs(new DateTime(2018, 10, 1), new DateTime(2018, 10, 10));

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetStatisticByNumberOfSoldSongsTestByArgumentException()
        {
            //Arrange
            var adminStatisticService = new AdminStatisticService(mockUnitOfWork.Object);

            //Act
            var result = Assert.Throws<ArgumentException>(() => adminStatisticService.GetStatisticByNumberOfSoldSongs(new DateTime(), new DateTime()));

            //Assert
            Assert.Equal("startDate or endDate is null\r\nИмя параметра: startDate' 'endDate", result.Message);
        }
    }
}
