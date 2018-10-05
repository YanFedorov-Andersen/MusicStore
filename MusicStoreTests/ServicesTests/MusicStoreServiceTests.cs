using Moq;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using TheWitcher.Domain;
using TheWitcher.Domain.Models;
using Xunit;

namespace MusicStoreTests.ServicesTests
{
    public class MusicStoreServiceTests
    {
        Mock<IUnitOfWork> mockUnitOfWork;
        Mock<IMapper<User, UserAccountDTO>> mockMapUser;
        Mock<IMapper<Song, SongDTO>> mockMapSong;
        Mock<IRepository<User>> mockUserRepository;
        Mock<IRepository<Song>> mockSongRepository;
        Mock<IRepository<BoughtSong>> mockBoughtSongRepository;

        public MusicStoreServiceTests()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockMapUser = new Mock<IMapper<User, UserAccountDTO>>();
            mockMapSong = new Mock<IMapper<Song, SongDTO>>();
            mockUserRepository = new Mock<IRepository<User>>();
            mockSongRepository = new Mock<IRepository<Song>>();
            mockBoughtSongRepository = new Mock<IRepository<BoughtSong>>();
        }

        [Fact]
        public void DisplayAllAvailableSongsTestByNegativeId()
        {
            //arrange
            mockUnitOfWork.Setup(x => x.Song).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccount).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSong).Returns(mockBoughtSongRepository.Object);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapSong.Object);

            //act
            var result  = musicStoreService.DisplayAllAvailableSongs(-1);

            //result
            Assert.Null(result);
        }

        [Fact]
        public void BuySongTestByNegativeId()
        {
            //arrange
            mockUnitOfWork.Setup(x => x.Song).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccount).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSong).Returns(mockBoughtSongRepository.Object);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapSong.Object);

            //act
            var result = musicStoreService.BuySong(-1, -1);

            //result
            Assert.False(result);
        }

        [Fact]
        public void BuySongTests()
        {
            //arrange
            User user = new User()
            {
                Id = 4,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            Song song = new Song()
            {
                Id = 4,
                Name = "Name",
                Price = 3
            };
            mockUnitOfWork.Setup(x => x.Song).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccount).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSong).Returns(mockBoughtSongRepository.Object);

            mockUserRepository.Setup(x => x.GetItem(4)).Returns(user);
            mockSongRepository.Setup(x => x.GetItem(4)).Returns(song);

            var musicStoreService = new MusicStoreService(mockUnitOfWork.Object, mockMapSong.Object);

            //act
            var result = musicStoreService.BuySong(4, 4);

            //result
            Assert.True(result);
        }
    }
}
