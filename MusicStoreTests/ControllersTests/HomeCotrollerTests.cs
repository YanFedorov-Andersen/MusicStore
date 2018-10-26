using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Moq;
using MusicStore.Business.Interfaces;
using MusicStore.Web.Controllers;
using Xunit;

namespace MusicStoreTests.ControllersTests
{
    public class HomeCotrollerTests
    {
        private readonly Mock<IUserAccountService> mockUserAccountService;

        public HomeCotrollerTests()
        {
            mockUserAccountService = new Mock<IUserAccountService>();
        }

        [Fact]
        public void IndexTest()
        {

            var homeController = new HomeController(mockUserAccountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Returns(identity);
            userMock.Setup(p => p.IsInRole("Registered user")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                .Returns(contextMock.Object);

            homeController.ControllerContext = controllerContextMock.Object;

            //Act
            var result = (ViewResult) homeController.Index();

            //Assert
            Assert.Equal("Non-Registered user", result.ViewData["Role"]);
        }

        [Fact]
        public void IndexTestByArgumentException()
        {

            var homeController = new HomeController(mockUserAccountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Throws(new ArgumentException());
            userMock.Setup(p => p.IsInRole("Registered user")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                .Returns(contextMock.Object);

            homeController.ControllerContext = controllerContextMock.Object;

            //Act
            var result = (HttpStatusCodeResult) homeController.Index();

            //Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void IndexTestByException()
        {

            var homeController = new HomeController(mockUserAccountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Throws(new Exception());
            userMock.Setup(p => p.IsInRole("Registered user")).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                .Returns(contextMock.Object);

            homeController.ControllerContext = controllerContextMock.Object;

            //Act
            var result = (HttpStatusCodeResult)homeController.Index();

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
    }
}
