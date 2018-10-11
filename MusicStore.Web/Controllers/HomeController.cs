using System.Linq;
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
            var roles = claims.Where(c => c.Type == roleClaimType).ToList();
            if (roles.Count == 1)
            {
                ViewBag.Role = roles.First().Value;
            }
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