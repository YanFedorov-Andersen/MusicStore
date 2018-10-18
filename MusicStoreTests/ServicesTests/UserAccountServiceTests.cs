using Moq;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.Domain;
using MusicStore.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using Xunit;

namespace MusicStoreTests.ServicesTests
{
    public class UserAccountServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly Mock<IMapper<User, MusicStore.Domain.DataTransfer.UserAccount>> _mockMapUser;
        private const int DEFAULT_USER_ID = 4;
        private static readonly Guid DEFAULT_USER_ID_STRING = new Guid();

        public UserAccountServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapUser = new Mock<IMapper<User, UserAccount>>();
            _mockUserRepository = new Mock<IRepository<User>>();
        }

        [Theory]
        [InlineData(false)]
        public void RegisterUserAccountTest(bool boolVariable)
        {
            //Arrange
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            List<User> usersList = new List<User>
            {
                user1
            };
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);

            _mockUserRepository.Setup(x => x.Create(It.IsAny<MusicStore.DataAccess.User>())).Returns(-1);
            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);
            _mockUserRepository.Setup(x => x.GetItemList()).Returns(usersList);

            //Act
            var result = Assert.Throws<Exception>(() => userAccountService.RegisterUserAccount(DEFAULT_USER_ID_STRING.ToString()));

            //Assert
            Assert.Equal("Internal server error: can not create user", result.Message);
        }
        [Fact]
        public void RegisterUserAccountTestArgExcept()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);

            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);

            //Act
            var result = Assert.Throws<ArgumentException>(() => userAccountService.RegisterUserAccount(null));

            //Assert
            Assert.Equal("User id is not valid", result.Message);
        }
        [Fact]
        public void RegisterUserAccountTestArgExceptParse()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);

            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);

            //Act
            var result = Assert.Throws<ArgumentException>(() => userAccountService.RegisterUserAccount("1"));

            //Assert
            Assert.Equal("Can not parse string to guid", result.Message);
        }
        [Theory]
        [InlineData(true)]
        public void RegisterUserAccountTestTrue(bool boolVariable)
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);

            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);

            //Act
            var result = userAccountService.RegisterUserAccount(DEFAULT_USER_ID_STRING.ToString());

            //Assert
            Assert.True(result);
        }
        [Fact]
        public void EditUserAccountTest()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);
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
        public void EditUserAccountTestArgExcept()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);
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
            var result = Assert.Throws<ArgumentException>(() => userAccountService.EditUserAccount(null));

            //Assert
            Assert.Equal("Can not update user, because it is null", result.Message);
        }

        [Fact]
        public void GetUserDataTest()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);
            var user1 = new User()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
                IdentityKey = DEFAULT_USER_ID_STRING
            };
            var domainUser = new UserAccount()
            {
                Id = 0,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Money = 12.56m,
            };
            List<User> usersList = new List<User>();
            usersList.Add(user1);
            _mockUserRepository.Setup(x => x.GetItemList()).Returns(usersList);
            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser
            );

            //Act
            var result = userAccountService.GetUserData(DEFAULT_USER_ID_STRING.ToString());

            //Assert
            Assert.Equal(domainUser, result);
        }
        [Fact]
        public void GetUserDataTestArgExceptStr()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);
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
            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser
            );

            //Act
            var result = Assert.Throws<ArgumentException>(() => userAccountService.GetUserData(null));

            //Assert
            Assert.Equal("User id is not valid", result.Message);
        }
        [Fact]
        public void GetUserDataTestArgExceptResultOfParse()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);
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
            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser
            );

            //Act
            var result = Assert.Throws<ArgumentException>(() => userAccountService.GetUserData("re"));

            //Assert
            Assert.Equal("Can not parse string to guid", result.Message);
        }
        [Fact]
        public void GetUserDataTestGetItemWithGuidIdReturnsNull()
        {
            //Arrange
            _mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(_mockUserRepository.Object);
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
            var userAccountService = new UserAccountService(_mockUnitOfWork.Object, _mockMapUser.Object);
            _mockMapUser.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.User>())).Returns(domainUser
            );

            //Act
            var result = Assert.Throws<Exception>(() => userAccountService.GetUserData(DEFAULT_USER_ID_STRING.ToString()));

            //Assert
            Assert.Equal("User not found", result.Message);
        }
    }
}
