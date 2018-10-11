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
        private readonly Mock<IUserAccountRepository> _mockUserAccountRepository;
        private readonly Mock<IAdminRepository> _mockAdminRepository;
        private readonly Mock<IMapper<User, UserAccount>> _mockMapUser;

        public AdminServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserAccountRepository = new Mock<IUserAccountRepository>();
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
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserAccountRepository.Object);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);
            _mockUnitOfWork.Setup(x => x.AdminRepository).Returns(_mockAdminRepository.Object);
            _mockAdminRepository.Setup(x => x.ActiveUsers()).Returns(userList);
            _mockAdminRepository.Setup(x => x.NotActiveUsers()).Returns(userList);

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
    }
}
