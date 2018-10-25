using Microsoft.AspNet.Identity;
using MusicStore.Business.Interfaces;
using MusicStore.Web.Models;
using System;
using System.Net;
using System.Web.Mvc;

namespace MusicStore.Web.Controllers
{
    public class UserStatisticController : Controller
    {
        private readonly IUserStatisticService _userStatisticService;
        private readonly IUserAccountService _userAccountService;

        public UserStatisticController(IUserStatisticService userStatisticService, IUserAccountService userAccountService)
        {
            _userStatisticService = userStatisticService;
            _userAccountService = userAccountService;
        }

        [Authorize(Roles = "Registered user")]
        public ActionResult DisplayUserStatistic()
        {
            try
            {
                string identityKey = User.Identity.GetUserId();
                int userId = _userAccountService.ConvertGuidInStringIdToIntId(identityKey);

                decimal totalSpentMoney = _userStatisticService.GetTotalSpentMoney(userId);
                int totalBoughtSongAmount = _userStatisticService.GetTotalNumberOfSongs(userId);

                var userStatisticViewModel = new UserStatisticViewModel(totalBoughtSongAmount, totalSpentMoney);

                return View(userStatisticViewModel);
            }
            catch (ArgumentException exception)
            {
                var innerExcept = exception.InnerException != null ? exception.InnerException.Message : " ";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"{innerExcept} and {exception.Message}");
            }
            catch (Exception exception)
            {
                var innerExcept = exception.InnerException != null ? exception.InnerException.Message : " ";
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, $"{innerExcept} and {exception.Message}");
            }
        }
    }
}