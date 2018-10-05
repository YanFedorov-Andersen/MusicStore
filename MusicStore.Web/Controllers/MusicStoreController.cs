using MusicStore.Business.Interfaces;
using System;
using System.Net;
using System.Web.Mvc;

namespace MusicStore.Web.Controllers
{
    public class MusicStoreController: Controller
    {
        private readonly IMusicStoreService _musicStoreService;

        private const int DEFAULT_USER_ID = 1;
        public MusicStoreController(IMusicStoreService musicStoreService)
        {
            _musicStoreService = musicStoreService;
        }

        public ActionResult DisplayAvailableMusicForLoggedUser()
        {
            ViewBag.AvailableMusicList = _musicStoreService.DisplayAllAvailableSongs(DEFAULT_USER_ID);
            ViewBag.userId = DEFAULT_USER_ID;
            return View();
        }

        [HttpPost]
        public ActionResult BuyMusic(int userId, int songId)
        {
            if(userId < 0 || songId < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            try
            {
                bool resultOfBuy = _musicStoreService.BuySong(songId, userId);
                ViewBag.OperationResult = resultOfBuy ? "Покупка совершена успешно" : "Покупка не совершена успешно";
            }
            catch (NullReferenceException exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, exception.Message);
            }
            return View();
        }
    }
}
