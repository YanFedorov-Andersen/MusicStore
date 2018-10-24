using MusicStore.Business.Interfaces;
using MusicStore.Web.Models;
using System;
using System.Net;
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
            var todayDate = DateTime.Today;
            var tomorrowDate = todayDate.AddDays(1);
            var monthAgoDate = todayDate.AddDays(-30);

            try
            {
                decimal totalMoneyEarnedForMonth = _adminStatiscticService.GetStatisticByTotalMoneyEarnedForSomeTime(monthAgoDate, todayDate);
                decimal totalMoneyEarnedForDay = _adminStatiscticService.GetStatisticByTotalMoneyEarnedForSomeTime(todayDate, tomorrowDate);

                int numberOfSoldSongsForMonth = _adminStatiscticService.GetStatisticByNumberOfSoldSongs(monthAgoDate, todayDate);
                int numberOfSoldSongsForDay = _adminStatiscticService.GetStatisticByNumberOfSoldSongs(todayDate, tomorrowDate);

                var adminStatisticViewModel = new AdminStatisticViewModel(totalMoneyEarnedForDay, totalMoneyEarnedForMonth, numberOfSoldSongsForMonth, numberOfSoldSongsForDay);

                return View(adminStatisticViewModel);
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