using Moq;
using MusicStore.Business.Services.Statistics;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using System;
using Xunit;

namespace MusicStoreTests.ServicesTests.StatisticTests
{
    public class UserStatisticsServiceTests
    {
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IRepository<User>> mockUserRepository;

        private const int DEFAULT_USER_ID = 1;
        public UserStatisticsServiceTests()
        {
            mockUserRepository = new Mock<IRepository<User>>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void GetTotalNumberOfSongsTest()
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

            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUserRepository.Setup(x => x.GetItem(DEFAULT_USER_ID)).Returns(user);
            var userStatisticService = new UserStatisticService(mockUnitOfWork.Object);

            //Act
            var result = userStatisticService.GetTotalNumberOfSongs(DEFAULT_USER_ID);

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetTotalNumberOfSongsTestByLackOfSongs()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_USER_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };

            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUserRepository.Setup(x => x.GetItem(DEFAULT_USER_ID)).Returns(user);
            var userStatisticService = new UserStatisticService(mockUnitOfWork.Object);

            //Act
            var result = userStatisticService.GetTotalNumberOfSongs(DEFAULT_USER_ID);

            //Assert
            Assert.Equal(0, result);
        }
        [Fact]
        public void GetTotalNumberOfSongsTestByNegativeId()
        {
            //Arrange
            var userStatisticService = new UserStatisticService(mockUnitOfWork.Object);

            //Act
            var result = Assert.Throws< ArgumentException>(() => userStatisticService.GetTotalNumberOfSongs(-1));

            //Assert
            Assert.Equal("userId is less then 1\r\nИмя параметра: userId", result.Message);
        }

        [Fact]
        public void GetTotalSpentMoneyTest()
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

            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUserRepository.Setup(x => x.GetItem(DEFAULT_USER_ID)).Returns(user);
            var userStatisticService = new UserStatisticService(mockUnitOfWork.Object);

            //Act
            var result = userStatisticService.GetTotalSpentMoney(DEFAULT_USER_ID);

            //Assert
            Assert.Equal(9.98m, result);
        }

        [Fact]
        public void GetTotalSpentMoneyTestByLackOfBoughtSongs()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_USER_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };

            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUserRepository.Setup(x => x.GetItem(DEFAULT_USER_ID)).Returns(user);
            var userStatisticService = new UserStatisticService(mockUnitOfWork.Object);

            //Act
            var result = userStatisticService.GetTotalSpentMoney(DEFAULT_USER_ID);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetTotalSpentMoneyTestByNegativeId()
        {
            //Arrange
            var userStatisticService = new UserStatisticService(mockUnitOfWork.Object);

            //Act
            var result = Assert.Throws<ArgumentException>(() => userStatisticService.GetTotalSpentMoney(-1));

            //Assert
            Assert.Equal("userId is less then 1\r\nИмя параметра: userId", result.Message);
        }
    }
}
