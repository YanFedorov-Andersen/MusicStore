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
            AdminStatisticViewModel adminStatisticViewModel = new AdminStatisticViewModel()
            {
                NumberOfSoldSongsForDay = 2,
                NumberOfSoldSongsForMonth = 1,
                TotalMoneyEarnedForDay = 2m,
                TotalMoneyEarnedForMonth = 1m
            };

            mockIAdminStatisticService.Setup(x => x.GetStatisticByNumberOfSoldSongs(new DateTime(2018, 9, 20), new DateTime(2018, 10, 20))).Returns(1);
            mockIAdminStatisticService.Setup(x => x.GetStatisticByNumberOfSoldSongs(new DateTime(2018, 10, 19), new DateTime(2018, 10, 20))).Returns(2);

            mockIAdminStatisticService.Setup(x => x.GetStatisticByTotalMoneyEarnedForSomeTime(new DateTime(2018, 9, 20), new DateTime(2018, 10, 20))).Returns(1);
            mockIAdminStatisticService.Setup(x => x.GetStatisticByTotalMoneyEarnedForSomeTime(new DateTime(2018, 10, 19), new DateTime(2018, 10, 20))).Returns(2);

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
    }
}
