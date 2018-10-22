using Moq;
using MusicStore.Business.Services;
using MusicStore.DataAccess;
using MusicStore.DataAccess.Interfaces;
using MusicStore.DataAccess.Realization;
using MusicStore.Domain;
using MusicStore.Domain.DataTransfer;
using System;
using System.Collections.Generic;
using Xunit;

namespace MusicStoreTests.ServicesTests
{
    public class MusicStoreDisplayServiceTests
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
        private readonly Mock<IGenericRepositoryWithPagination<MusicStore.DataAccess.Album>> mockAlbumRepository;

        private const int DEFAULT_NEGATIVE_ENTITIES_ID = -1;
        private const int DEFAULT_ENTITIES_ID = 4;
        public MusicStoreDisplayServiceTests()
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
            mockAlbumRepository = new Mock<IGenericRepositoryWithPagination<MusicStore.DataAccess.Album>>();
        }
        [Fact]
        public void DisplayAllAvailableSongsTestByNegativeId()
        {
            //Arrange
            mockUnitOfWork.Setup(x => x.SongRepository).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);

            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);

            //Act
            var ex = Assert.Throws<ArgumentException>(() => musicStoreDisplayService.DisplayAllAvailableSongs(DEFAULT_NEGATIVE_ENTITIES_ID));

            //Assert
            Assert.Equal("userId <= 0 in musicStoreService DisplayAllAvailableSongs\r\nИмя параметра: userId", ex.Message);
        }
        [Fact]
        public void DisplayAllAvailableSongsTest()
        {
            //Arrange
            mockUnitOfWork.Setup(x => x.SongRepository).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);
            mockUnitOfWork.Setup(x => x.SongStoreRepository).Returns(mockSongStoreRepository.Object);

            var song = new MusicStore.DataAccess.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            List<MusicStore.DataAccess.Song> songs = new List<MusicStore.DataAccess.Song>
            {
                song
            };
            List<MusicStore.Domain.DataTransfer.Song> songsDomain = new List<MusicStore.Domain.DataTransfer.Song>
            {
                songDomain
            };

            mockMapSong.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Song>())).Returns(songDomain);
            mockSongStoreRepository.Setup(x => x.GetSongsAvailableToBuyByUser(DEFAULT_ENTITIES_ID)).Returns(songs);

            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);


            //Act
            var result = musicStoreDisplayService.DisplayAllAvailableSongs(DEFAULT_ENTITIES_ID);

            //Assert
            Assert.Equal(songsDomain, result);
        }
        [Fact]
        public void DisplayAllAvailableSongsTestNullExcept()
        {
            //Arrange
            mockUnitOfWork.Setup(x => x.SongRepository).Returns(mockSongRepository.Object);
            mockUnitOfWork.Setup(x => x.UserAccountRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(x => x.BoughtSongRepository).Returns(mockBoughtSongRepository.Object);
            mockUnitOfWork.Setup(x => x.SongStoreRepository).Returns(mockSongStoreRepository.Object);

            var song = new MusicStore.DataAccess.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            List<MusicStore.DataAccess.Song> songs = new List<MusicStore.DataAccess.Song>
            {
                song
            };
            List<MusicStore.Domain.DataTransfer.Song> songsDomain = new List<MusicStore.Domain.DataTransfer.Song>
            {
                songDomain
            };

            mockMapSong.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Song>())).Returns(songDomain);
            mockSongStoreRepository.Setup(x => x.GetSongsAvailableToBuyByUser(DEFAULT_ENTITIES_ID));

            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);


            //Act
            var result = Assert.Throws<Exception>(() => musicStoreDisplayService.DisplayAllAvailableSongs(DEFAULT_ENTITIES_ID));

            //Assert
            Assert.Equal("not available for buying", result.Message);
        }
        [Fact]
        public void DisplayAlbumsWithPaginationTest()
        {
            //Arrange
            var album = new MusicStore.DataAccess.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            var albumDomain = new MusicStore.Domain.DataTransfer.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            var albumList = new List<MusicStore.DataAccess.Album>()
            {
                album
            };
            var albumDomainList = new List<MusicStore.Domain.DataTransfer.Album>()
            {
                albumDomain
            };
            mockUnitOfWork.Setup(x => x.AlbumRepositoryWithPagination).Returns(mockAlbumRepository.Object);
            mockAlbumRepository.Setup(x => x.GetItemList()).Returns(albumList);
            var indexViewItem = new IndexViewItem<MusicStore.DataAccess.Album>()
            {
                Items = albumList,
                PageInfo = new Repository<MusicStore.DataAccess.Album>()
                {
                    PageNumber = 1,
                    PageSize = 1,
                    TotalItems = 1
                }
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
            mockAlbumRepository.Setup(x => x.MakePagination(It.IsAny<List<MusicStore.DataAccess.Album>>(), 1, 3)).Returns(indexViewItem);
            mockMapAlbum.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Album>())).Returns(albumDomain);

            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);

            //Act
            var result = musicStoreDisplayService.DisplayAlbumsWithPagination();

            //Assert
            Assert.Equal(indexViewDomainItem.Items, result.Items);
        }

        [Fact]
        public void GetSongsListFromAlbumTest()
        {
            //Arrange
            var song1 = new MusicStore.DataAccess.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var album = new MusicStore.DataAccess.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            album.Songs.Add(song1);
            var albumDomain = new MusicStore.Domain.DataTransfer.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            mockUnitOfWork.Setup(x => x.AlbumRepositoryWithPagination).Returns(mockAlbumRepository.Object);
            var albumList = new List<MusicStore.DataAccess.Album>()
            {
                album
            };
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songDomainList = new List<MusicStore.Domain.DataTransfer.Song>()
            {
                songDomain
            };
            albumDomain.Songs = new List<MusicStore.DataAccess.Song>();
            albumDomain.Songs.Add(song1);
            mockAlbumRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(album);
            mockMapAlbum.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Album>())).Returns(albumDomain);
            mockMapSong.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Song>())).Returns(songDomain);
            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);

            //Act
            var result = musicStoreDisplayService.GetSongsListFromAlbum(DEFAULT_ENTITIES_ID);

            //Assert
            Assert.Equal(songDomainList, result);
        }
        [Fact]
        public void GetSongsListFromAlbumTestByNegativeId()
        {
            //Arrange            
            mockUnitOfWork.Setup(x => x.AlbumRepositoryWithPagination).Returns(mockAlbumRepository.Object);

            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);

            //Act
            var result = Assert.Throws<ArgumentException>(() => musicStoreDisplayService.GetSongsListFromAlbum(DEFAULT_NEGATIVE_ENTITIES_ID));

            //Assert
            Assert.Equal("userId <= 0 in musicStoreService DisplayAllAvailableSongs\r\nИмя параметра: albumId", result.Message);
        }
        [Fact]
        public void GetSongsListFromAlbumAvailableForBuyByUserTestByOverLappingValues()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_ENTITIES_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            var song1 = new MusicStore.DataAccess.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            MusicStore.DataAccess.BoughtSong boughtSong = new MusicStore.DataAccess.BoughtSong()
            {
                Id = DEFAULT_ENTITIES_ID,
                BoughtPrice = song1.Price,
                IsVisible = true,
                BoughtDate = DateTime.Now,
                Song = song1,
                User = user
            };
            user.BoughtSongs.Add(boughtSong);
            var album = new MusicStore.DataAccess.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            album.Songs.Add(song1);
            var albumDomain = new MusicStore.Domain.DataTransfer.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };

            var albumList = new List<MusicStore.DataAccess.Album>()
            {
                album
            };
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songDomainList = new List<MusicStore.Domain.DataTransfer.Song>()
            {
                songDomain
            };
            var songDomainEmptyList = new List<MusicStore.Domain.DataTransfer.Song>();
            albumDomain.Songs = new List<MusicStore.DataAccess.Song>();
            albumDomain.Songs.Add(song1);


            mockUnitOfWork.Setup(x => x.AlbumRepositoryWithPagination).Returns(mockAlbumRepository.Object);
            mockUnitOfWork.Setup(X =>
            X.UserAccountRepository).Returns(mockUserRepository.Object);

            
            mockAlbumRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(album);
            mockUserRepository.Setup(X => X.GetItem(DEFAULT_ENTITIES_ID)).Returns(user);

            mockMapAlbum.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Album>())).Returns(albumDomain);
            mockMapSong.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Song>())).Returns(songDomain);
            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);

            //Act
            var result = musicStoreDisplayService.GetSongsListFromAlbumAvailableForBuyByUser(DEFAULT_ENTITIES_ID, DEFAULT_ENTITIES_ID);

            //Assert
            Assert.Equal(songDomainEmptyList, result);
        }
        [Fact]
        public void GetSongsListFromAlbumAvailableForBuyByUserTestByNotOverLappingValues()
        {
            //Arrange
            User user = new User()
            {
                Id = DEFAULT_ENTITIES_ID,
                FirstName = "1",
                LastName = "2",
                Money = 12
            };
            var album = new MusicStore.DataAccess.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };
            var song1 = new MusicStore.DataAccess.Song()
            {
                Id = 1,
                Name = "All world for you",
                Price = 1.99m,
                Album = album,
            };
            var song2 = new MusicStore.DataAccess.Song()
            {
                Id = 2,
                Name = "All world again you",
                Price = 2.99m,
                Album = album
            };
            var song3 = new MusicStore.DataAccess.Song()
            {
                Id = 3,
                Name = "All world do not see you",
                Price = 3.99m,
                Album = album
            };
            var song4 = new MusicStore.DataAccess.Song()
            {
                Id = 4,
                Name = "4",
                Price = 4.99m,
            };
            var song5 = new MusicStore.DataAccess.Song()
            {
                Id = 5,
                Name = "5",
                Price = 4.99m,
            };
            var boughtSong1 = new MusicStore.DataAccess.BoughtSong()
            {
                Id = 0,
                User = user,
                IsVisible = true,
                Song = song4,
                BoughtPrice = song4.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };
            var boughtSong2 = new MusicStore.DataAccess.BoughtSong()
            {
                Id = 1,
                User = user,
                IsVisible = true,
                Song = song5,
                BoughtPrice = song5.Price,
                BoughtDate = new DateTime(2018, 10, 3)
            };
            album.Songs.Add(song1);
            album.Songs.Add(song2);
            album.Songs.Add(song3);
            user.BoughtSongs.Add(boughtSong1);
            user.BoughtSongs.Add(boughtSong2);

            var albumDomain = new MusicStore.Domain.DataTransfer.Album()
            {
                Id = 1,
                Name = "Album 1",
                DiscountIfBuyAllSongs = 15.0m,
            };

            var albumList = new List<MusicStore.DataAccess.Album>()
            {
                album
            };
            var songDomain = new MusicStore.Domain.DataTransfer.Song()
            {
                Id = 0,
                Name = "All world for you",
                Price = 1.99m,
            };
            var songDomainList = new List<MusicStore.Domain.DataTransfer.Song>();
            songDomainList.Add(songDomain);
            songDomainList.Add(songDomain);
            songDomainList.Add(songDomain);
            albumDomain.Songs = new List<MusicStore.DataAccess.Song>();
            albumDomain.Songs.Add(song1);


            mockUnitOfWork.Setup(x => x.AlbumRepositoryWithPagination).Returns(mockAlbumRepository.Object);
            mockUnitOfWork.Setup(X =>
            X.UserAccountRepository).Returns(mockUserRepository.Object);


            mockAlbumRepository.Setup(x => x.GetItem(DEFAULT_ENTITIES_ID)).Returns(album);
            mockUserRepository.Setup(X => X.GetItem(DEFAULT_ENTITIES_ID)).Returns(user);

            mockMapAlbum.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Album>())).Returns(albumDomain);
            mockMapSong.Setup(x => x.AutoMap(It.IsAny<MusicStore.DataAccess.Song>())).Returns(songDomain);
            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);

            //Act
            var result = musicStoreDisplayService.GetSongsListFromAlbumAvailableForBuyByUser(DEFAULT_ENTITIES_ID, DEFAULT_ENTITIES_ID);

            //Assert
            Assert.Equal(songDomainList, result);
        }
        [Theory]
        [InlineData(-1, -1, "albumId <= 0 in musicStoreDisplayService GetSongsListAvailableForBuyByUser\r\nИмя параметра: albumId")]
        [InlineData(1, -1, "userId <= 0 in musicStoreDisplayService GetSongsListAvailableForBuyByUser\r\nИмя параметра: userId")]
        [InlineData(-1, 1, "albumId <= 0 in musicStoreDisplayService GetSongsListAvailableForBuyByUser\r\nИмя параметра: albumId")]
        public void GetSongsListFromAlbumAvailableForBuyByUserTestByNegativeId(int albumId, int userId, string exception)
        {
            //Arrange
            mockUnitOfWork.Setup(x => x.AlbumRepositoryWithPagination).Returns(mockAlbumRepository.Object);
            mockUnitOfWork.Setup(X =>
            X.UserAccountRepository).Returns(mockUserRepository.Object);

            var musicStoreDisplayService = new MusicStoreDisplayService(mockUnitOfWork.Object, mockMapSong.Object, mockMapAlbum.Object);

            //Act
            var result = Assert.Throws<ArgumentException>(() => musicStoreDisplayService.GetSongsListFromAlbumAvailableForBuyByUser(albumId, userId));

            //Assert
            Assert.Equal(exception, result.Message);
        }
    }
}
