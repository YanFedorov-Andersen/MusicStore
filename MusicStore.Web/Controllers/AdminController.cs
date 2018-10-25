using MusicStore.Business.Interfaces;
using System.Web.Mvc;
using MusicStore.Domain.DataTransfer;
using System;
using System.Net;

namespace MusicStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserAccountService _userAccountService;

        public AdminController(IAdminService adminService, IUserAccountService userAccountService)
        {
            _adminService = adminService;
            _userAccountService = userAccountService;
        }

        public ActionResult AdminOptionsMenu()
        {
            return View();
        }

        public ActionResult DisplayAllUsers()
        {
            return View(_adminService.GetFullListOfUsers());
        }
        public ActionResult DisplayNotActiveUsers()
        {
            return View(_adminService.GetActiveOrNotActiveUsers(false));
        }
        public ActionResult DisplayActiveUsers()
        {
            return View(_adminService.GetActiveOrNotActiveUsers(true));
        }

        [HttpPost]
        public ActionResult EditUserAccount(UserAccount userAccount)
        {
            if (userAccount == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"{nameof(userAccount)}  is null");
            }
            try
            {
                var result = _userAccountService.EditUserAccount(userAccount);
                ViewBag.ResultOfEditingUser = result;
            }
            catch(ArgumentNullException exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"userId or songId is null, exception message: {exception.Message}");
            }            
            return View();
        }

    }
}