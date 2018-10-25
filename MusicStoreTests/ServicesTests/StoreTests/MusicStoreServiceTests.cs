using Moq;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.Domain;
using System;
using MusicStore.Domain.DataTransfer;
using Xunit;

namespace MusicStoreTests.ServicesTests
{
    public class MusicStoreServiceTests
    {
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IMapper<User, UserAccount>> mockMapUser;
        private readonly Mock<IMapper<MusicStore.DataAccess.Song, MusicStore.Domain.DataTransfer.Song>> mockMapSong;
        private readonly Mock<IRepository<User>> mockUserRepository;
        private readonly Mock<IRepository<MusicStore.DataAccess.Song>> mockSongRepository;
        private readonly Mock<IRepository<MusicStore.DataAccess.BoughtSong>> mockBoughtSongRepository;
        private readonly Mock<IMapper<MusicStore.DataAccess.BoughtSong, MusicStore.Domain.DataTransfer.BoughtSong>> mockMapBoughtSong;
        private readonly Mock<ISongStoreRepository> mockSongStoreRepository;
        private readonly Mock<IMapper<MusicStore.DataAccess.Album, MusicStore.Domain.DataTransfer.Album>> mockMapAlbum;

        private const int DEFAULT_NEGATIVE_ENTITIES_ID = -1;
        private const int DEFAULT_ENTITIES_ID = 4;
        private const decimal DEFAULT_DISCOUNT = 3;

        public MusicStoreServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockMapUser = new Mock<IMapper<User, UserAccount>>();
            mockMapSong = new Mock<IMapper<MusicStore.DataAccess.Song, MusicStore.Domain.DataTransfer.Song>>();
            mockUserRepository = new Mock<IRepository<User>>();
            mockSongRepository = new Mock<IRepository<MusicStore.DataAccess.Song>>();
            mockBoughtSongRepository = new Mock<IRepository<MusicStore.DataAccess.BoughtSong>>();
            mockMapBoughtSong = new Mock<IMapper<MusicStore.DataAccess.BoughtSong, MusicStore.Domain.DataTransfer.BoughtSong>>();
            mockSongStoreRepository = new Mock<ISongStoreRepository>();
            mockMapAlbum = new Mock<IMapper<MusicStore.DataAccess.Album, MusicStore.Domain.DataTransfer.Album>>();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(1, -1)]
        [InlineData(-1, 1)]
        public void BuySongTestByNegativeId(int userId, int songId)
        {
            //Arrange
            mockUnitOfWork.Setup(x => x.SongRepository).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapBoughtSong.Object);

            //Act
            ArgumentException ex = Assert.Throws<ArgumentException>(() => musicStoreService.BuySong(userId, songId));

            //Assert
            Assert.Equal("userId is less then 1 or songId is less then 1 in musicStoreService in BuySong\r\nИмя параметра: userId or songId", ex.Message);
        }

        [Fact]
        public void BuySongTests()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_ENTITIES_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            MusicStore.DataAccess.Song song = new MusicStore.DataAccess.Song()
            {
                Id = DEFAULT_ENTITIES_ID,
                Name = "Name",
                Price = 3
            };
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong()
            {
                Id = DEFAULT_ENTITIES_ID,
                BoughtPrice = song.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
                User = user
            };
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong()
            {
                BoughtPrice = boughtSong.BoughtPrice,
                Id = boughtSong.Id,
                BoughtDate = boughtSong.BoughtDate,
                Song = song,
            };
            mockUnitOfWork.Setup(x => x.SongRepository).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);

            mockUserRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(user);
            mockSongRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(song);
            mockMapBoughtSong.Setup(x => x.AutoMap (It.IsAny<MusicStore.DataAccess.BoughtSong>())).Returns(boughtSongDTO);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapBoughtSong.Object);

            //Act
            var result = musicStoreService.BuySong(DEFAULT_ENTITIES_ID, DEFAULT_ENTITIES_ID);

            //Assert
            Assert.Equal(boughtSongDTO, result);
        }
        [Fact]
        public void BuySongTestsWithDiscount()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_ENTITIES_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            MusicStore.DataAccess.Song song = new MusicStore.DataAccess.Song()
            {
                Id = DEFAULT_ENTITIES_ID,
                Name = "Name",
                Price = 3
            };
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong()
            {
                Id = DEFAULT_ENTITIES_ID,
                BoughtPrice = song.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
                User = user
            };
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong()
            {
                BoughtPrice = boughtSong.BoughtPrice,
                Id = boughtSong.Id,
                BoughtDate = boughtSong.BoughtDate,
                Song = song,
            };
            mockUnitOfWork.Setup(x => x.SongRepository).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);

            mockUserRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(user);
            mockSongRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(song);
            mockMapBoughtSong.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.BoughtSong>())).Returns(boughtSongDTO);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapBoughtSong.Object);

            //Act
            var result = musicStoreService.BuySong(DEFAULT_ENTITIES_ID, DEFAULT_ENTITIES_ID, DEFAULT_DISCOUNT);

            //Assert
            Assert.Equal(boughtSongDTO, result);
        }
        [Fact]
        public void BuySongTestsByNullUserEntity()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_ENTITIES_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            MusicStore.DataAccess.Song song = new MusicStore.DataAccess.Song()
            {
                Id = DEFAULT_ENTITIES_ID,
                Name = "Name",
                Price = 3
            };
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong()
            {
                Id = DEFAULT_ENTITIES_ID,
                BoughtPrice = song.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
                User = user
            };
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong()
            {
                BoughtPrice = boughtSong.BoughtPrice,
                Id = boughtSong.Id,
                BoughtDate = boughtSong.BoughtDate,
                Song = song,
            };
            mockUnitOfWork.Setup(x => x.SongRepository).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);

            mockUserRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID));
            mockSongRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(song);
            mockMapBoughtSong.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.BoughtSong>())).Returns(boughtSongDTO);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapBoughtSong.Object);

            //Act
            var result = Assert.Throws<Exception>(() => musicStoreService.BuySong(DEFAULT_ENTITIES_ID, DEFAULT_ENTITIES_ID));

            //Assert
            Assert.Equal("User is null", result.Message);
        }
        [Fact]
        public void BuySongTestsByNullSongEntity()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_ENTITIES_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            MusicStore.DataAccess.Song song = new MusicStore.DataAccess.Song()
            {
                Id = DEFAULT_ENTITIES_ID,
                Name = "Name",
                Price = 3
            };
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong()
            {
                Id = DEFAULT_ENTITIES_ID,
                BoughtPrice = song.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
                User = user
            };
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong()
            {
                BoughtPrice = boughtSong.BoughtPrice,
                Id = boughtSong.Id,
                BoughtDate = boughtSong.BoughtDate,
                Song = song,
            };
            mockUnitOfWork.Setup(x => x.SongRepository).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);

            mockUserRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(user);
            mockSongRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID));
            mockMapBoughtSong.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.BoughtSong>())).Returns(boughtSongDTO);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapBoughtSong.Object);

            //Act
            var result = Assert.Throws<Exception>(() => musicStoreService.BuySong(DEFAULT_ENTITIES_ID, DEFAULT_ENTITIES_ID));

            //Assert
            Assert.Equal("Song is null", result.Message);
        }
    }
}
