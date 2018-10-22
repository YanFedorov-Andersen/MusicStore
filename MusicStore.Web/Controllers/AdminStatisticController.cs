using MusicStore.Business.Interfaces;
using MusicStore.Web.Models;
using System;
using System.Web.Mvc;

namespace MusicStore.Web.Controllers
{
    public class AdminStatisticController : Controller
    {
        private readonly IAdminStatisticService _adminStatiscticService;

        public AdminStatisticController(IAdminStatisticService adminStatiscticService)
        {
            _adminStatiscticService = adminStatiscticService;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DisplayAdminStatistic()
        {
            decimal totalMoneyEarnedForMonth = _adminStatiscticService.GetStatisticByTotalMoneyEarnedForSomeTime(new DateTime(2018, 9, 20), new DateTime(2018, 10, 20));
            decimal totalMoneyEarnedForDay = _adminStatiscticService.GetStatisticByTotalMoneyEarnedForSomeTime(new DateTime(2018, 10, 19), new DateTime(2018, 10, 20));

            int numberOfSoldSongsForMonth = _adminStatiscticService.GetStatisticByNumberOfSoldSongs(new DateTime(2018, 9, 20), new DateTime(2018, 10, 20));
            int numberOfSoldSongsForDay = _adminStatiscticService.GetStatisticByNumberOfSoldSongs(new DateTime(2018, 10, 19), new DateTime(2018, 10, 20));

            AdminStatisticViewModel adminStatisticViewModel = new AdminStatisticViewModel()
            {
                NumberOfSoldSongsForDay = numberOfSoldSongsForDay,
                NumberOfSoldSongsForMonth = numberOfSoldSongsForMonth,
                TotalMoneyEarnedForDay = totalMoneyEarnedForDay,
                TotalMoneyEarnedForMonth = totalMoneyEarnedForMonth
            };

            return View(adminStatisticViewModel);
        }
    }
}