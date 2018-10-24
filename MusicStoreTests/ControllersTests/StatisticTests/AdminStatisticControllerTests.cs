using Moq;
using MusicStore.Business.Interfaces;
using MusicStore.Web.Controllers;
using MusicStore.Web.Models;
using System;
using System.Web.Mvc;
using Xunit;

namespace MusicStoreTests.ControllersTests.StatisticTests
{
    public class AdminStatisticControllerTests
    {
        private readonly Mock<IAdminStatisticService> mockIAdminStatisticService;

        public AdminStatisticControllerTests()
        {
            mockIAdminStatisticService = new Mock<IAdminStatisticService>();
        }
        [Fact]
        public void DisplayAdminStatisticTest()
        {
            //Arrange
            var todayDate = DateTime.Today;
            var tomorrowDate = todayDate.AddDays(1);
            var monthAgoDate = todayDate.AddDays(-30);

            AdminStatisticViewModel adminStatisticViewModel = new AdminStatisticViewModel(1, 2, 2, 1);

            mockIAdminStatisticService.Setup(x => x.GetStatisticByNumberOfSoldSongs(todayDate, tomorrowDate)).Returns(1);
            mockIAdminStatisticService.Setup(x => x.GetStatisticByNumberOfSoldSongs(monthAgoDate, todayDate)).Returns(2);

            mockIAdminStatisticService.Setup(x => x.GetStatisticByTotalMoneyEarnedForSomeTime(todayDate, tomorrowDate)).Returns(1);
            mockIAdminStatisticService.Setup(x => x.GetStatisticByTotalMoneyEarnedForSomeTime(monthAgoDate, todayDate)).Returns(2);

            var adminStatisticController = new AdminStatisticController(mockIAdminStatisticService.Object);

            //Act
            var result = (ViewResult)adminStatisticController.DisplayAdminStatistic();
            var resultModel = (AdminStatisticViewModel)result.Model;

            //Assert
            Assert.Equal(adminStatisticViewModel.TotalMoneyEarnedForDay, resultModel.TotalMoneyEarnedForDay);
            Assert.Equal(adminStatisticViewModel.TotalMoneyEarnedForMonth, resultModel.TotalMoneyEarnedForMonth);

            Assert.Equal(adminStatisticViewModel.NumberOfSoldSongsForDay, resultModel.NumberOfSoldSongsForDay);
            Assert.Equal(adminStatisticViewModel.NumberOfSoldSongsForMonth, resultModel.NumberOfSoldSongsForMonth);
        }

        [Fact]
        public void DisplayAdminStatisticTestByArgumentException()
        {
            //Arrange
            var todayDate = DateTime.Today;
            var tomorrowDate = todayDate.AddDays(1);
            var monthAgoDate = todayDate.AddDays(-30);

            AdminStatisticViewModel adminStatisticViewModel = new AdminStatisticViewModel(2, 1, 1, 2);

            mockIAdminStatisticService.Setup(x => x.GetStatisticByNumberOfSoldSongs(monthAgoDate, todayDate)).Throws(new ArgumentException("exception"));

            var adminStatisticController = new AdminStatisticController(mockIAdminStatisticService.Object);

            //Act
            var result = (HttpStatusCodeResult)adminStatisticController.DisplayAdminStatistic();

            //Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void DisplayAdminStatisticTestByException()
        {
            //Arrange
            var todayDate = DateTime.Today;
            var tomorrowDate = todayDate.AddDays(1);
            var monthAgoDate = todayDate.AddDays(-30);

            AdminStatisticViewModel adminStatisticViewModel = new AdminStatisticViewModel(2, 1, 1, 2);

            mockIAdminStatisticService.Setup(x => x.GetStatisticByNumberOfSoldSongs(monthAgoDate, todayDate)).Throws(new Exception("exception"));

            var adminStatisticController = new AdminStatisticController(mockIAdminStatisticService.Object);

            //Act
            var result = (HttpStatusCodeResult)adminStatisticController.DisplayAdminStatistic();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
