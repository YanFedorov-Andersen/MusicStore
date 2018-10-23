using MusicStore.Business.Interfaces;
using System;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MusicStore.Web.Controllers
{
    public class MusicStoreController: Controller
    {
        private readonly IMusicStoreService _musicStoreService;
        private readonly IMusicStoreDisplayService _musicStoreDisplayService;
        private readonly IUserAccountService _userAccountService;
        private readonly IDiscountService _discountService;
        public MusicStoreController(IMusicStoreService musicStoreService, IUserAccountService userAccountService, IMusicStoreDisplayService musicStoreDisplayService, IDiscountService discountService)
        {
            _musicStoreService = musicStoreService;
            _userAccountService = userAccountService;
            _musicStoreDisplayService = musicStoreDisplayService;
            _discountService = discountService;
        }

        [Authorize(Roles = "Registered user")]
        public ActionResult DisplayAvailableMusicForLoggedUser()
        {
            var identityKey = User.Identity.GetUserId();
            int userId = _userAccountService.ConvertGuidInStringIdToIntId(identityKey);
            ViewBag.AvailableMusicList = _musicStoreDisplayService.DisplayAllAvailableSongs(userId);
            ViewBag.userId = userId;
            return View();
        }
        public ActionResult DisplayMusic()
        {
            ViewBag.MusicList = _musicStoreDisplayService.DisplayAllSongs();
            return View();
        }

        public ActionResult DisplayPaginatedAlbums(int page = 1, int pageSize = 10)
        {
            if(page < 0)
            {
                throw new ArgumentException("page is less then 0", nameof(page));
            }
            try
            {
                var resultOfAlbumPagination = _musicStoreDisplayService.DisplayAlbumsWithPagination(page, pageSize);
                if (resultOfAlbumPagination == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Something wrong with pagination or albums in db");
                }
                return View(resultOfAlbumPagination);
            }
            catch (ArgumentException exception)
            {
                var innerExcept = exception.InnerException != null ? exception.InnerException.Message : " ";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"{innerExcept} and {exception.Message}");
            }
            catch (Exception exception)
            {
                var innerExcept = exception.InnerException != null ? exception.InnerException.Message : " ";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"{innerExcept} and {exception.Message}");
            }
        }

        [Authorize(Roles = "Registered user")]
        [HttpPost]
        public ActionResult BuyMusic(int songId, int userId )
        {
            if (userId < 1 || songId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "userId or songId is less 1");
            }
            
            try
            {
                var resultOfBuy = _musicStoreService.BuySong(songId, userId);
                ViewBag.OperationResult = (resultOfBuy != null) ? "Покупка совершена успешно" : "Покупка не совершена успешно";
            }
            catch (ArgumentException exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
            catch (Exception exception) when (exception.Message.Contains("User has not enough money for buy"))
            {
                return View("~/Views/MusicStore/BuyMusicNotEnoughMoney.cshtml");
            }
            return View();
        }
        [Authorize(Roles = "Registered user")]
        [HttpPost]
        public ActionResult BuyWholeAlbum(int albumId, decimal discount)
        {
            if (albumId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"{nameof(albumId)} is less 1");
            }
            var identityKey = GetUserIdentityId();
            int userId = _userAccountService.ConvertGuidInStringIdToIntId(identityKey);

            var albumSongsList = _musicStoreDisplayService.GetSongsListFromAlbum(albumId);
            var checkDiscountAvailable = _discountService.CheckDiscountAvailable(userId, albumId);
            Domain.DataTransfer.BoughtSong resultOfBuySong;
            try
            {

                if (!checkDiscountAvailable)
                {
                    foreach (var song in albumSongsList)
                    {
                        resultOfBuySong = _musicStoreService.BuySong(song.Id, userId);
                        ViewBag.OperationResult += (resultOfBuySong != null) ? "Покупка совершена успешно" : "Покупка не совершена успешно";
                    }
                }
                else
                {
                    foreach (var song in albumSongsList)
                    {
                        resultOfBuySong = _musicStoreService.BuySong(song.Id, userId, discount);
                        ViewBag.OperationResult += (resultOfBuySong != null) ? "Покупка совершена успешно" : "Покупка не совершена успешно";
                    }
                }

            }
            catch (ArgumentException exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
            catch (Exception exception) when (exception.Message.Contains("User has not enough money for buy"))
            {
                return View("~/Views/MusicStore/BuyMusicNotEnoughMoney.cshtml");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DisplaySongsOfAlbum(int albumId)
        {
            if(albumId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "albumId is less 1");
            }

            if (User.Identity.IsAuthenticated)
            {
                var identityKey = User.Identity.GetUserId();
                int userId = _userAccountService.ConvertGuidInStringIdToIntId(identityKey);
                return RedirectToAction("GetAvailableSongsListForBuyByUser", new {userId, albumId });
            }
            return View(_musicStoreDisplayService.GetSongsListFromAlbum(albumId));
        }
        [Authorize(Roles = "Registered user")]
        public ActionResult GetAvailableSongsListForBuyByUser(int userId, int albumId)
        {
            if(userId < 1 || albumId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "albumId is less 1 or userId is less 1");
            }

            var availableSongsFormAlbumToBuyForUser = _musicStoreDisplayService.GetSongsListFromAlbumAvailableForBuyByUser(albumId, userId);
            ViewBag.userId = userId;
            return View(availableSongsFormAlbumToBuyForUser);
        }
        public string GetUserIdentityId()
        {
            return User.Identity.GetUserId();
        }
    }
}
