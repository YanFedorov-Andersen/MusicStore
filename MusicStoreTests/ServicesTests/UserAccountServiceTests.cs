using Moq;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.Domain;
using MusicStore.Domain.DataTransfer;
using System;
using Xunit;

namespace MusicStoreTests.ServicesTests
{
    public class UserAccountServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserAccountRepository> _mockUserAccountRepository;
        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly Mock<IMapper<User, MusicStore.Domain.DataTransfer.UserAccount>> _mockMapUser;
        private const int DEFAULT_USER_ID = 4;
        private const string DEFAULT_USER_ID_STRING = "4";

        public UserAccountServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapUser = new Mock<IMapper<User, UserAccount>>();
            _mockUserAccountRepository = new Mock<IUserAccountRepository>();
            _mockUserRepository = new Mock<IRepository<User>>();
        }

        [Theory]
        [InlineData(false)]
        public void RegisterUserAccountTest(bool boolVariable)
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserAccountRepository.Object);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);

            _mockUserAccountRepository.Setup(x => x.CreateWithGuidId(DEFAULT_USER_ID_STRING)).Returns(boolVariable);
            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);

            //Act
            var result = Assert.Throws<Exception>(() => userAccountService.RegisterUserAccount(DEFAULT_USER_ID_STRING));

            //Assert
            Assert.Equal("Internal server error: can not create user", result.Message);
        }
        [Theory]
        [InlineData(true)]
        public void RegisterUserAccountTestTrue(bool boolVariable)
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserAccountRepository.Object);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);

            _mockUserAccountRepository.Setup(x => x.CreateWithGuidId(DEFAULT_USER_ID_STRING)).Returns(boolVariable);
            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);

            //Act
            var result = userAccountService.RegisterUserAccount(DEFAULT_USER_ID_STRING);

            //Assert
            Assert.True(result);
        }
        [Fact]
        public void EditUserAccountTest()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserAccountRepository.Object);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            var domainUser = new UserAccount()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            _mockUserRepository.Setup(x => x.GetItem(DEFAULT_USER_ID)).Returns(user1);
            _mockUserRepository.Setup(x => x.Update(It.IsAny<MusicStore.DataAccess.User>())).Returns(DEFAULT_USER_ID);
            _mockMapUser.Setup(x => x.ReAutoMap(It.IsAny<MusicStore.Domain.DataTransfer.UserAccount>(), It.IsAny<MusicStore.DataAccess.User>())).Returns(user1);

            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);

            //Act
            var result = userAccountService.EditUserAccount(domainUser);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GetUserDataTest()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserAccountRepository.Object);
            _mockUnitOfWork.Setup(x => x.UserAccount).Returns(_mockUserRepository.Object);
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            var domainUser = new UserAccount()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            _mockUserAccountRepository.Setup(x => x.GetItemWithGuidId(DEFAULT_USER_ID_STRING)).Returns(user1);
            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser
            );

            //Act
            var result = userAccountService.GetUserData(DEFAULT_USER_ID_STRING);

            //Assert
            Assert.Equal(domainUser, result);
        }

    }
}
