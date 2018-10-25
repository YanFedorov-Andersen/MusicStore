using Moq;
using MusicStore.Business.Interfaces;
using MusicStore.Web.Controllers;
using MusicStore.Web.Models;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace MusicStoreTests.ControllersTests.StatisticTests
{
    public class UserStatisticControllerTest
    {
        private readonly Mock<IUserStatisticService> mockUserStatisticService;
        private readonly Mock<IUserAccountService> mockUserAccountService;
        private const int DEFAULT_ENTITY_ID = 4;
        public UserStatisticControllerTest()
        {
            mockUserAccountService = new Mock<IUserAccountService>();
            mockUserStatisticService = new Mock<IUserStatisticService>();
        }
        [Fact]
        public void DisplayUserStatisticTest()
        {
            //Arrange
            UserStatisticViewModel userStatisticViewModel = new UserStatisticViewModel(1, 2);

            mockUserAccountService.Setup(x => x.ConvertGuidInStringIdToIntId(null)).Returns(DEFAULT_ENTITY_ID);
            mockUserStatisticService.Setup(x => x.GetTotalNumberOfSongs(DEFAULT_ENTITY_ID)).Returns(1);
            mockUserStatisticService.Setup(x => x.GetTotalSpentMoney(DEFAULT_ENTITY_ID)).Returns(2);

            var userStatisticController = new UserStatisticController(mockUserStatisticService.Object, mockUserAccountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Returns(identity);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            userStatisticController.ControllerContext = controllerContextMock.Object;

            //Act
            var result = (ViewResult) userStatisticController.DisplayUserStatistic();
            var resultModel = (UserStatisticViewModel) result.Model;

            //Assert
            Assert.Equal(userStatisticViewModel.TotalNumberOfSongs, resultModel.TotalNumberOfSongs);
            Assert.Equal(userStatisticViewModel.TotalSpentMoney, resultModel.TotalSpentMoney);
        }

        [Fact]
        public void DisplayUserStatisticTestByArgumentException()
        {
            //Arrange
            UserStatisticViewModel userStatisticViewModel = new UserStatisticViewModel(1, 2);

            mockUserAccountService.Setup(x => x.ConvertGuidInStringIdToIntId(null)).Throws(new ArgumentException("error"));

            mockUserStatisticService.Setup(x => x.GetTotalNumberOfSongs(DEFAULT_ENTITY_ID)).Returns(1);
            mockUserStatisticService.Setup(x => x.GetTotalSpentMoney(DEFAULT_ENTITY_ID)).Returns(2);

            var userStatisticController = new UserStatisticController(mockUserStatisticService.Object, mockUserAccountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Returns(identity);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            userStatisticController.ControllerContext = controllerContextMock.Object;

            //Act
            var result = (HttpStatusCodeResult) userStatisticController.DisplayUserStatistic();

            //Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void DisplayUserStatisticTestByException()
        {
            //Arrange
            UserStatisticViewModel userStatisticViewModel = new UserStatisticViewModel(1, 2);

            mockUserAccountService.Setup(x => x.ConvertGuidInStringIdToIntId(null)).Throws(new Exception("error"));

            mockUserStatisticService.Setup(x => x.GetTotalNumberOfSongs(DEFAULT_ENTITY_ID)).Returns(1);
            mockUserStatisticService.Setup(x => x.GetTotalSpentMoney(DEFAULT_ENTITY_ID)).Returns(2);

            var userStatisticController = new UserStatisticController(mockUserStatisticService.Object, mockUserAccountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Returns(identity);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            userStatisticController.ControllerContext = controllerContextMock.Object;

            //Act
            var result = (HttpStatusCodeResult)userStatisticController.DisplayUserStatistic();

            //Assert
            Assert.Equal(500, result.StatusCode);

        }
    }
}
