using Microsoft.AspNet.Identity;
using MusicStore.Business.Interfaces;
using MusicStore.Web.Models;
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
            string identityKey = User.Identity.GetUserId();
            int userId = _userAccountService.ConvertGuidInStringIdToIntId(identityKey);

            decimal totalSpentMoney = _userStatisticService.GetTotalSpentMoney(userId);
            int totalBoughtSongAmount = _userStatisticService.GetTotalNumberOfSongs(userId);

            UserStatisticViewModel userStatisticViewModel = new UserStatisticViewModel()
            {
                TotalNumberOfSongs = totalBoughtSongAmount,
                TotalSpentMoney = totalSpentMoney
            };
            return View(userStatisticViewModel);
        }

    }
}