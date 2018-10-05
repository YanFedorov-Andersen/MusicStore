using MusicStore.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefaultApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DefaultInterface _defaultInterface;
        public HomeController(DefaultInterface defaultInterface)
        {
            _defaultInterface = defaultInterface;
        }
        public ActionResult Index()
        {
            var x = _defaultInterface.Return2();
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