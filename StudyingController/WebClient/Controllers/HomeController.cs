using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinClient.Models;

namespace ThinClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginConfig loginConfig)
        {
            var serviceClient = new SCS.ControllerServiceClient();
            var session = serviceClient.Login(loginConfig.Login, loginConfig.Password);

            if (session != null)
            {
                this.Session["Session"] = session;
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Unsuccessful login.";
            return View();
        }
    }
}
