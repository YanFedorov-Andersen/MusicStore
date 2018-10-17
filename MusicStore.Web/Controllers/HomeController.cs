using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;

namespace MusicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var role = claims.FirstOrDefault(c => c.Type == roleClaimType);

            ViewBag.Role = role != null ? role.Value : "Non-Registered user";

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