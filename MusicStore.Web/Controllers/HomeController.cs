using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicStore.Business.Interfaces;

namespace MusicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAccountService _userAccountService;

        public HomeController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        public ActionResult Index()
        {
            try
            {
                var userIdentity = (ClaimsIdentity) User.Identity;
                var claims = userIdentity.Claims;
                var roleClaimType = userIdentity.RoleClaimType;
                var role = claims.FirstOrDefault(c => c.Type == roleClaimType);

                ViewBag.Role = role != null ? role.Value : "Non-Registered user";

                if (role != null && role.Value != "Admin")
                {
                    string identityKey = User.Identity.GetUserId();
                    if (!_userAccountService.CheckIfActive(identityKey))
                    {
                        return Redirect("/Home/AccountNotActive");
                    }
                }
            }
            catch (ArgumentException exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
            catch (Exception exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, exception.Message);
            }

            return View();
        }

        public ActionResult AccountNotActive()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}