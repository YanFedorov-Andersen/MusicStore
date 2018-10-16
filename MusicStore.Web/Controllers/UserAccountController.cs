using Microsoft.AspNet.Identity;
using MusicStore.Business.Interfaces;
using MusicStore.Domain.DataTransfer;
using System.Web.Mvc;

namespace MusicStore.Web.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Registered user")]
        public ActionResult GetUserData()
        {
            User.Identity.GetUserId();
            var userId = User.Identity.GetUserId();
            var user = _userAccountService.GetUserData(userId);
            ViewBag.User = user;
            return View(user);
        }

        
        [Authorize(Roles = "Admin, Registered user")]
        [HttpPost]
        public ActionResult EditUserData(UserAccount userData)
        {
            _userAccountService.EditUserAccount(userData);
            return View();
        }
    }
}