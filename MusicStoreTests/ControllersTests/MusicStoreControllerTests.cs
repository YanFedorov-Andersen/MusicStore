using Moq;
using MusicStore.Business.Interfaces;
using MusicStore.Domain;
using MusicStore.Web.Controllers;
using System.Web.Mvc;
using Xunit;

namespace MusicStoreTests.ControllersTests
{
    public class MusicStoreControllerTests
    {
        private Mock<IMusicStoreService> mockMusicStoreService;

        private const int DEFAULT_ID_FOR_ENTITIES = 4;
        private const int DEFAULT_NEGATIVE_ID_FOR_ENTITIES = -1;
        private const string EXPECTED_MESSAGE = "Покупка совершена успешно";
        private readonly Mock<IMapper<MusicStore.DataAccess.BoughtSong, MusicStore.Domain.DataTransfer.BoughtSong>> mockMapBoughtSong;


        public MusicStoreControllerTests()
        {
            mockMusicStoreService = new Mock<IMusicStoreService>();
            mockMapBoughtSong = new Mock<IMapper<MusicStore.DataAccess.BoughtSong, MusicStore.Domain.DataTransfer.BoughtSong>>();
        }

        [Fact]
        public void BuyMusicTest()
        {
            //Arrange
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong();
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong();

            mockMusicStoreService.Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES)).Returns(boughtSongDTO);
            mockMapBoughtSong.Setup(x => x.AutoMap(boughtSong)).Returns(boughtSongDTO);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object);

            //Act
            ViewResult result = (ViewResult)musicStoreController.BuyMusic(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES);

            //Assert
            Assert.Equal(EXPECTED_MESSAGE, result.ViewData["OperationResult"]);
            
        }

        [Fact]
        public void BuyMusicTestByNegativeId()
        {
            //Arrange
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong();
            mockMusicStoreService.Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES)).Returns(boughtSongDTO);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object);

            //Act
            HttpStatusCodeResult result = (HttpStatusCodeResult)musicStoreController.BuyMusic(DEFAULT_NEGATIVE_ID_FOR_ENTITIES, DEFAULT_NEGATIVE_ID_FOR_ENTITIES);

            //Assert
            Assert.Equal(400, result.StatusCode);

        }
    }
}
