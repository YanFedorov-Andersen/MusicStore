using MusicStore.Business.Interfaces;
using System.Web.Mvc;
using MusicStore.Domain.DataTransfer;

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
            return View(_adminService.GetListOfUsers(false));
        }
        public ActionResult DisplayActiveUsers()
        {
            return View(_adminService.GetListOfUsers(true));
        }

        [HttpPost]
        public ActionResult EditUserAccount(UserAccount userAccount)
        {
            var result = _userAccountService.EditUserAccount(userAccount);
            ViewBag.ResultOfEditingUser = result;
            return View();
        }

    }
}