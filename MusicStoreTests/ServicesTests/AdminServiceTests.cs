using System;
using Moq;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.Domain;
using MusicStore.Domain.DataTransfer;
using System.Collections.Generic;
using Xunit;

namespace MusicStoreTests.ServicesTests
{
    public class AdminServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly Mock<IAdminRepository> _mockAdminRepository;
        private readonly Mock<IMapper<User, UserAccount>> _mockMapUser;

        public AdminServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockAdminRepository = new Mock<IAdminRepository>();
            _mockMapUser = new Mock<IMapper<User, UserAccount>>();
            _mockUserRepository = new Mock<IRepository<User>>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetListOfUsersTests(bool isActive)
        {
            // Arrange
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            var userList = new List<User>();
            userList.Add(user1);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);
            _mockUnitOfWork.Setup(x => x.AdminRepository).Returns(_mockAdminRepository.Object);
            _mockAdminRepository.Setup(x => x.ActiveOrNotActiveUsers(isActive)).Returns(userList);

            var domainUser = new UserAccount()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser);
            var adminService = new AdminService(_mockUnitOfWork.Object, _mockMapUser.Object);
            List<UserAccount> userAccountList = new List<UserAccount>();
            userAccountList.Add(domainUser);

            //Act
            var result = adminService.GetListOfUsers(isActive);

            //Assert
            Assert.Equal(userAccountList, result);
        }
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetListOfUsersTestsByNullException(bool isActive)
        {
            // Arrange
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            var userList = new List<User>();
            userList.Add(user1);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);
            _mockUnitOfWork.Setup(x => x.AdminRepository).Returns(_mockAdminRepository.Object);
            _mockAdminRepository.Setup(x => x.ActiveOrNotActiveUsers(isActive));

            var domainUser = new UserAccount()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser);
            var adminService = new AdminService(_mockUnitOfWork.Object, _mockMapUser.Object);
            List<UserAccount> userAccountList = new List<UserAccount>();
            userAccountList.Add(domainUser);

            //Act
            var result = Assert.Throws<Exception>(() => adminService.GetListOfUsers(isActive));

            //Assert
            Assert.Equal("Such users not exists", result.Message);
        }

        [Fact]
        public void GetFullListOfUsersTest()
        {
            // Arrange
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            var userList = new List<User>();
            userList.Add(user1);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);
            _mockUnitOfWork.Setup(x => x.AdminRepository).Returns(_mockAdminRepository.Object);


            var domainUser = new UserAccount()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };

            _mockUserRepository.Setup(x => x.GetItemList()).Returns(userList);
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser);
            var adminService = new AdminService(_mockUnitOfWork.Object, _mockMapUser.Object);
            List<UserAccount> userAccountList = new List<UserAccount>();
            userAccountList.Add(domainUser);

            //Act
            var result = adminService.GetFullListOfUsers();

            //Assert
            Assert.Equal(userAccountList, result);
        }
        [Fact]
        public void GetFullListOfUsersTestByNullRes()
        {
            // Arrange
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            var userList = new List<User>();
            userList.Add(user1);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);
            _mockUnitOfWork.Setup(x => x.AdminRepository).Returns(_mockAdminRepository.Object);


            var domainUser = new UserAccount()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };

            _mockUserRepository.Setup(x => x.GetItemList());
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser);
            var adminService = new AdminService(_mockUnitOfWork.Object, _mockMapUser.Object);
            List<UserAccount> userAccountList = new List<UserAccount>();
            userAccountList.Add(domainUser);

            //Act
            var result = adminService.GetFullListOfUsers();

            //Assert
            Assert.Null(result);
        }
    }
}
