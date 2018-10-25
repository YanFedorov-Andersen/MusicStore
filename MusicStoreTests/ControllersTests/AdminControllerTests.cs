using System;
using Moq;
using MusicStore.Business.Interfaces;
using MusicStore.Web.Controllers;
using System.Web.Mvc;
using MusicStore.Domain.DataTransfer;
using Xunit;

namespace MusicStoreTests.ControllersTests
{
    public class AdminControllerTests
    {
        private readonly Mock<IAdminService> _mockAdminService;
        private readonly Mock<IUserAccountService> _mockUserAccountService;

        public AdminControllerTests()
        {
            _mockAdminService = new Mock<IAdminService>();
            _mockUserAccountService = new Mock<IUserAccountService>();
        }

        [Fact]
        public void EditUserAccountTestByNullParameter()
        {
            var adminController = new AdminController(_mockAdminService.Object, _mockUserAccountService.Object);

            //Act
            var result = (HttpStatusCodeResult) adminController.EditUserAccount(null);

            //Assert
            Equals(400, result.StatusCode);
        }

        [Fact]
        public void EditUserAccountTestByArgumentNullException()
        {
            _mockUserAccountService.Setup(x => x.EditUserAccount(It.IsAny<UserAccount>())).Throws(new ArgumentNullException());
            var adminController = new AdminController(_mockAdminService.Object, _mockUserAccountService.Object);

            //Act
            var result = (HttpStatusCodeResult)adminController.EditUserAccount(new UserAccount());

            //Assert
            Equals(400, result.StatusCode);
        }

        [Fact]
        public void EditUserAccountTest()
        {
            _mockUserAccountService.Setup(x => x.EditUserAccount(null)).Returns(true);
            var adminController = new AdminController(_mockAdminService.Object, _mockUserAccountService.Object);

            //Act
            var result = (ViewResult) adminController.EditUserAccount(new UserAccount());

            //Assert
            Equals(true, result.TempData["ResultOfEditingUser"]);
        }
    }
}
