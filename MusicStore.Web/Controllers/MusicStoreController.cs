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
        private readonly IUserAccountService _userAccountService;
        public MusicStoreController(IMusicStoreService musicStoreService, IUserAccountService userAccountService)
        {
            _musicStoreService = musicStoreService;
            _userAccountService = userAccountService;
        }

        [Authorize(Roles = "Registered user")]
        public ActionResult DisplayAvailableMusicForLoggedUser()
        {
            var identityKey = User.Identity.GetUserId();
            int userId = _userAccountService.ConvertGuidInStringIdToIntId(identityKey);
            ViewBag.AvailableMusicList = _musicStoreService.DisplayAllAvailableSongs(userId);
            ViewBag.userId = userId;
            return View();
        }
        public ActionResult DisplayMusic()
        {
            ViewBag.MusicList = _musicStoreService.DisplayAllSongs();
            return View();
        }

        [Authorize(Roles = "Registered user")]
        [HttpPost]
        public ActionResult BuyMusic(int userId, int songId)
        {
            if (userId < 0 || songId < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "userId or songId is null");
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
            return View();
        }
    }
}
