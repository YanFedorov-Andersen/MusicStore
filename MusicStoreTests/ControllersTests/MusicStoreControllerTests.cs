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
        private Mock<IMusicStoreDisplayService> mockMusicStoreDisplayService;
        private Mock<IUserAccountService> mockUserAccountService;
        private Mock<IDiscountService> mockDiscountService;

        private const int DEFAULT_ID_FOR_ENTITIES = 4;
        private const int DEFAULT_NEGATIVE_ID_FOR_ENTITIES = -1;
        private const int DEFAULT_DISCOUNT = 0;
        
        private readonly Mock<IMapper<MusicStore.DataAccess.BoughtSong, MusicStore.Domain.DataTransfer.BoughtSong>> mockMapBoughtSong;

        public MusicStoreControllerTests()
        {
            mockUserAccountService = new Mock<IUserAccountService>();
            mockMusicStoreService = new Mock<IMusicStoreService>();
            mockMapBoughtSong = new Mock<IMapper<MusicStore.DataAccess.BoughtSong, MusicStore.Domain.DataTransfer.BoughtSong>>();
            mockMusicStoreDisplayService = new Mock<IMusicStoreDisplayService>();
            mockDiscountService = new Mock<IDiscountService>();
        }

        [Fact]
        public void BuyMusicTest()
        {
            const string EXPECTED_MESSAGE = "Покупка совершена успешно";
            //Arrange
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong();
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong();

            mockMusicStoreService.Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES, DEFAULT_DISCOUNT)).Returns(boughtSongDTO);
            mockMapBoughtSong.Setup(x => x.AutoMap(boughtSong)).Returns(boughtSongDTO);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);

            //Act
            ViewResult result = (ViewResult)musicStoreController.BuyMusic(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES);

            //Assert
            Assert.Equal(EXPECTED_MESSAGE, result.ViewData["OperationResult"]);            
        }
      
        [Fact]
        public void BuyMusicTestNoBuy()
        {
            const string EXPECTED_MESSAGE = "Покупка не совершена успешно";
            //Arrange
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong();
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong();

            mockMusicStoreService.Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES, DEFAULT_DISCOUNT));
            mockMapBoughtSong.Setup(x => x.AutoMap(boughtSong)).Returns(boughtSongDTO);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            //Act
            ViewResult result = (ViewResult)musicStoreController.BuyMusic(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES);

            //Assert
            Assert.Equal(EXPECTED_MESSAGE, result.ViewData["OperationResult"]);
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        public void BuyMusicTestByNegativeId(int userId, int songId)
        {
            //Arrange
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong();
            mockMusicStoreService.Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES, DEFAULT_DISCOUNT)).Returns(boughtSongDTO);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);

            //Act
            HttpStatusCodeResult result = (HttpStatusCodeResult)musicStoreController.BuyMusic(userId, songId);

            //Assert
            Assert.Equal(400, result.StatusCode);
        }
    }
}
