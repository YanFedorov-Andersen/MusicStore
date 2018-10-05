using Moq;
using MusicStore.Business.Interfaces;
using MusicStore.Web.Controllers;
using System.Net;
using System.Web.Mvc;
using Xunit;

namespace MusicStoreTests.ControllersTests
{
    public class MusicStoreControllerTests
    {
        private Mock<IMusicStoreService> mockMusicStoreService;
        public MusicStoreControllerTests()
        {
            mockMusicStoreService = new Mock<IMusicStoreService>();
        }

        [Fact]
        public void BuyMusicTest()
        {
            //arrange
            mockMusicStoreService.Setup(x => x.BuySong(4, 4)).Returns(true);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object);

            //act
            ViewResult result = (ViewResult)musicStoreController.BuyMusic(4, 4);

            //assert
            Assert.Equal("Покупка совершена успешно", result.ViewData["OperationResult"]);
            
        }

        [Fact]
        public void BuyMusicTestByNegativeId()
        {
            //arrange
            mockMusicStoreService.Setup(x => x.BuySong(4, 4)).Returns(true);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object);

            //act
            HttpStatusCodeResult result = (HttpStatusCodeResult)musicStoreController.BuyMusic(-1, -1);

            //assert
            Assert.Equal(new HttpStatusCodeResult(HttpStatusCode.Unauthorized).StatusCode, result.StatusCode);
        }
    }
}
