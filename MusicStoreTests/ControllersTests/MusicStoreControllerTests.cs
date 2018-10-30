using Moq;
using MusicStore.Business.Interfaces;
using MusicStore.DataAccess.Realization;
using MusicStore.Domain;
using MusicStore.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
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
        private const string EXPECTED_NO_BUY_MESSAGE = "Покупка не совершена успешно";
        private const string EXPECTED_BUY_MESSAGE = "Покупка не совершена успешно";

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
        public void BuyMusicTestByArgumentException()
        {
            const string EXPECTED_MESSAGE = "Покупка совершена успешно";
            //Arrange
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong();
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong();

            mockMusicStoreService
                .Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES, DEFAULT_DISCOUNT))
                .Throws(new ArgumentException("exception"));
            mockMapBoughtSong.Setup(x => x.AutoMap(boughtSong)).Returns(boughtSongDTO);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);

            //Act
            var result = (HttpStatusCodeResult)(musicStoreController.BuyMusic(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES));

            //Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void BuyMusicTestNoBuy()
        {
            
            //Arrange
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong();
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong();

            mockMusicStoreService.Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES, DEFAULT_DISCOUNT));
            mockMapBoughtSong.Setup(x => x.AutoMap(boughtSong)).Returns(boughtSongDTO);
            MusicStoreController musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            //Act
            ViewResult result = (ViewResult)musicStoreController.BuyMusic(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES);

            //Assert
            Assert.Equal(EXPECTED_NO_BUY_MESSAGE, result.ViewData["OperationResult"]);
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

        [Fact]
        public void DisplayPaginatedAlbums()
        {
            //Arrange
            var albumDomain = new MusicStore.Domain.DataTransfer.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            var albumDomainList = new List<MusicStore.Domain.DataTransfer.Album>()
            {
                albumDomain
            };
            var indexViewDomainItem = new IndexViewItem<MusicStore.Domain.DataTransfer.Album>()
            {
                Items = albumDomainList,
                PageInfo = new Repository<MusicStore.Domain.DataTransfer.Album>()
                {
                    PageNumber = 1,
                    PageSize = 1,
                    TotalItems = 1
                }
            };
            mockMusicStoreDisplayService.Setup(x => x.DisplayAlbumsWithPagination(1, 1)).Returns(indexViewDomainItem);
            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            //Act
            var result = (ViewResult)musicStoreController.DisplayPaginatedAlbums(1, 1);

            //Assert
            Assert.Equal(indexViewDomainItem, result.Model);
        }
        [Fact]
        public void DisplayPaginatedAlbumsTestByNegativeId()
        {
            //Arrange            
            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            //Act
            var result = Assert.Throws<ArgumentException>(() => musicStoreController.DisplayPaginatedAlbums(-1, 1));

            //Assert
            Assert.Equal("page is less then 0\r\nИмя параметра: page", result.Message);
        }

        [Fact]
        public void DisplayPaginatedAlbumsTestByInternalServerError()
        {
            //Arrange
            mockMusicStoreDisplayService.Setup(x => x.DisplayAlbumsWithPagination(1, 1));
            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            //Act
            var result = (HttpStatusCodeResult)musicStoreController.DisplayPaginatedAlbums(1, 1);

            //Assert
            Assert.Equal(500, result.StatusCode);
        }
        [Fact]
        public void DisplayPaginatedAlbumsTestByArgumentException()
        {
            //Arrange
            mockMusicStoreDisplayService.Setup(x => x.DisplayAlbumsWithPagination(1, 1)).Throws(new ArgumentException("exception"));
            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            //Act
            var result = (HttpStatusCodeResult)musicStoreController.DisplayPaginatedAlbums(1, 1);

            //Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void DisplayPaginatedAlbumsTestByException()
        {
            //Arrange
            mockMusicStoreDisplayService.Setup(x => x.DisplayAlbumsWithPagination(1, 1)).Throws(new Exception("exception"));
            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            //Act
            var result = (HttpStatusCodeResult)musicStoreController.DisplayPaginatedAlbums(1, 1);

            //Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [InlineData(true, EXPECTED_BUY_MESSAGE)]
        [InlineData(false, EXPECTED_NO_BUY_MESSAGE)]
        public void BuyWholeAlbumTest(bool hasDiscount, string message)
        {
            //Arrange
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songsDomain = new List<MusicStore.Domain.DataTransfer.Song>
            {
                songDomain
            };
            MusicStore.DataAccess.Song song = new MusicStore.DataAccess.Song()
            {
                Id = DEFAULT_ID_FOR_ENTITIES,
                Name = "Name",
                Price = 3
            };
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong()
            {
                Id = DEFAULT_ID_FOR_ENTITIES,
                BoughtPrice = song.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
            };
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong()
            {
                BoughtPrice = boughtSong.BoughtPrice,
                Id = boughtSong.Id,
                BoughtDate = boughtSong.BoughtDate,
                Song = song,
            };

            mockUserAccountService.Setup(x => x.ConvertGuidInStringIdToIntId(null)).Returns(DEFAULT_ID_FOR_ENTITIES);
            mockMusicStoreDisplayService.Setup(x => x.GetSongsListFromAlbum(DEFAULT_ID_FOR_ENTITIES)).Returns(songsDomain);
            mockDiscountService.Setup(x => x.IsDiscountAvailable(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES)).Returns(hasDiscount);
            mockMusicStoreService.Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES, 0)).Returns(boughtSongDTO);

            if (hasDiscount)
            {
                mockMusicStoreService.Setup(x => x.CheckIfMoneyEnough(DEFAULT_ID_FOR_ENTITIES, 1.99m, 4)).Returns(true);
            }
            else
            {
                mockMusicStoreService.Setup(x => x.CheckIfMoneyEnough(DEFAULT_ID_FOR_ENTITIES, 1.99m, 0)).Returns(true);
            }
           

            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Returns(identity);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            musicStoreController.ControllerContext = controllerContextMock.Object;

            //Act
            ViewResult result = (ViewResult)musicStoreController.BuyWholeAlbum(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES);
            var resultModel =  (List<string>)result.Model;
            //Assert
            Assert.Equal(message, resultModel[0]);
        }
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BuyWholeAlbumTestByUnableToBuy(bool hasDiscount)
        {
            //Arrange
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songsDomain = new List<MusicStore.Domain.DataTransfer.Song>
            {
                songDomain
            };
            MusicStore.DataAccess.Song song = new MusicStore.DataAccess.Song()
            {
                Id = DEFAULT_ID_FOR_ENTITIES,
                Name = "Name",
                Price = 3
            };
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong()
            {
                Id = DEFAULT_ID_FOR_ENTITIES,
                BoughtPrice = song.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
            };
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong()
            {
                BoughtPrice = boughtSong.BoughtPrice,
                Id = boughtSong.Id,
                BoughtDate = boughtSong.BoughtDate,
                Song = song,
            };

            mockUserAccountService.Setup(x => x.ConvertGuidInStringIdToIntId(null)).Returns(DEFAULT_ID_FOR_ENTITIES);
            mockMusicStoreDisplayService.Setup(x => x.GetSongsListFromAlbum(DEFAULT_ID_FOR_ENTITIES)).Returns(songsDomain);
            mockDiscountService.Setup(x => x.IsDiscountAvailable(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES)).Returns(hasDiscount);
            mockMusicStoreService.Setup(x => x.BuySong(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES, 0));
            mockMusicStoreService.Setup(x => x.CheckIfMoneyEnough(DEFAULT_ID_FOR_ENTITIES, 1.99m, 0)).Returns(false);

            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Returns(identity);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            musicStoreController.ControllerContext = controllerContextMock.Object;

            //Act
            ViewResult result = (ViewResult)musicStoreController.BuyWholeAlbum(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES);
            var resultModel = (List<string>)result.Model;

            //Assert
            Assert.Equal("Недостаточно денег для покупки всего альбома", resultModel[0]);
        }
        [Theory]
        [InlineData(false)]
        public void BuyWholeAlbumTestByArgumentException(bool hasDiscount)
        {
            //Arrange
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songsDomain = new List<MusicStore.Domain.DataTransfer.Song>
            {
                songDomain
            };
            MusicStore.DataAccess.Song song = new MusicStore.DataAccess.Song()
            {
                Id = DEFAULT_ID_FOR_ENTITIES,
                Name = "Name",
                Price = 3
            };
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong()
            {
                Id = DEFAULT_ID_FOR_ENTITIES,
                BoughtPrice = song.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song,
            };
            MusicStore.Domain.DataTransfer.BoughtSong boughtSongDTO = new MusicStore.Domain.DataTransfer.BoughtSong()
            {
                BoughtPrice = boughtSong.BoughtPrice,
                Id = boughtSong.Id,
                BoughtDate = boughtSong.BoughtDate,
                Song = song,
            };

            mockUserAccountService.Setup(x => x.ConvertGuidInStringIdToIntId(null)).Returns(DEFAULT_ID_FOR_ENTITIES);
            mockMusicStoreDisplayService.Setup(x => x.GetSongsListFromAlbum(DEFAULT_ID_FOR_ENTITIES)).Returns(songsDomain);
            mockDiscountService.Setup(x => x.IsDiscountAvailable(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES)).Returns(hasDiscount);
            mockMusicStoreService.Setup(x => x.BuySong(0, DEFAULT_ID_FOR_ENTITIES, 0)).Throws(new ArgumentException("exception"));
            mockMusicStoreService.Setup(x => x.CheckIfMoneyEnough(DEFAULT_ID_FOR_ENTITIES, 1.99m, 0)).Returns(true);


            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Returns(identity);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            musicStoreController.ControllerContext = controllerContextMock.Object;

            //Act
            var result = (HttpStatusCodeResult)musicStoreController.BuyWholeAlbum(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES);

            //Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void DisplaySongsOfAlbum()
        {
            //Arrange

            mockUserAccountService.Setup(x => x.ConvertGuidInStringIdToIntId(null)).Returns(DEFAULT_ID_FOR_ENTITIES);

            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);
            var userMock = new Mock<IPrincipal>();
            GenericIdentity identity = new GenericIdentity("a");
            userMock.Setup(p => p.Identity).Returns(identity);
            userMock.Setup(p => p.Identity.IsAuthenticated).Returns(true);

            var contextMock = new Mock<HttpContextBase>();
            contextMock.SetupGet(ctx => ctx.User)
                       .Returns(userMock.Object);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.SetupGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            musicStoreController.ControllerContext = controllerContextMock.Object;

            //Act
            var result = musicStoreController.DisplaySongsOfAlbum(DEFAULT_ID_FOR_ENTITIES);

            //Assert
            Assert.IsType(typeof(RedirectToRouteResult), result);
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        public void GetAvailableSongsListForBuyByUserTestByNegativeId(int userId, int albumId)
        {
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songsDomain = new List<MusicStore.Domain.DataTransfer.Song>
            {
                songDomain
            };

            mockMusicStoreDisplayService.Setup(x => x.GetSongsListFromAlbumAvailableForBuyByUser(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES)).Returns(songsDomain);

            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);

            //Act
            var result = (HttpStatusCodeResult)musicStoreController.GetAvailableSongsListForBuyByUser(userId, albumId);

            //Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void GetAvailableSongsListForBuyByUserTest()
        {
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songsDomain = new List<MusicStore.Domain.DataTransfer.Song>
            {
                songDomain
            };

            mockMusicStoreDisplayService.Setup(x => x.GetSongsListFromAlbumAvailableForBuyByUser(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES)).Returns(songsDomain);

            var musicStoreController = new MusicStoreController(mockMusicStoreService.Object, mockUserAccountService.Object, mockMusicStoreDisplayService.Object, mockDiscountService.Object);

            //Act
            var result = (ViewResult)musicStoreController.GetAvailableSongsListForBuyByUser(DEFAULT_ID_FOR_ENTITIES, DEFAULT_ID_FOR_ENTITIES);

            //Assert
            Assert.Equal(songsDomain, result.Model);
        }
    }
}
