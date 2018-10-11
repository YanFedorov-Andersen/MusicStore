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

        private const int DEFAULT_NEGATIVE_ENTITIES_ID = -1;
        private const int DEFAULT_ENTITIES_ID = 4;

        public MusicStoreServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockMapUser = new Mock<IMapper<User, UserAccount>>();
            mockMapSong = new Mock<IMapper<MusicStore.DataAccess.Song, MusicStore.Domain.DataTransfer.Song>>();
            mockUserRepository = new Mock<IRepository<User>>();
            mockSongRepository = new Mock<IRepository<MusicStore.DataAccess.Song>>();
            mockBoughtSongRepository = new Mock<IRepository<MusicStore.DataAccess.BoughtSong>>();
            mockMapBoughtSong = new Mock<IMapper<MusicStore.DataAccess.BoughtSong, MusicStore.Domain.DataTransfer.BoughtSong>>();
        }

        [Fact]
        public void DisplayAllAvailableSongsTestByNegativeId()
        {
            //Arrange
            mockUnitOfWork.Setup(x => x.Song).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccount).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSong).Returns(mockBoughtSongRepository.Object);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapSong.Object, mockMapBoughtSong.Object);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => musicStoreService.DisplayAllAvailableSongs(DEFAULT_NEGATIVE_ENTITIES_ID));

            //Assert
            Assert.Equal("userId < 0 in musicStoreService DisplayAllAvailableSongs", ex.Message);
        }

        [Fact]
        public void BuySongTestByNegativeId()
        {
            //Arrange
            mockUnitOfWork.Setup(x => x.Song).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccount).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSong).Returns(mockBoughtSongRepository.Object);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapSong.Object, mockMapBoughtSong.Object);

            //Act
            ArgumentException ex = Assert.Throws<ArgumentException>(() => musicStoreService.BuySong(DEFAULT_NEGATIVE_ENTITIES_ID, DEFAULT_NEGATIVE_ENTITIES_ID));

            //Assert
            ArgumentException exception = new ArgumentException();
            Assert.Equal("userId < 0 or songId < 0 in musicStoreService in BuySong", ex.Message);
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
            mockUnitOfWork.Setup(x => x.Song).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccount).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSong).Returns(mockBoughtSongRepository.Object);

            mockUserRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(user);
            mockSongRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(song);
            mockMapBoughtSong.Setup(x => x.AutoMap (It.IsAny<MusicStore.DataAccess.BoughtSong>())).Returns(boughtSongDTO);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapSong.Object, mockMapBoughtSong.Object);

            //Act
            var result = musicStoreService.BuySong(DEFAULT_ENTITIES_ID, DEFAULT_ENTITIES_ID);

            //Assert
            Assert.Equal(boughtSongDTO, result);
        }
    }
}
