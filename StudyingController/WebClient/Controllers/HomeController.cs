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
        public ActionResult LogIn(LoginModel loginConfig)
        {
            try
            {
                var serviceClient = new SCS.ControllerServiceClient();
                var session = serviceClient.Login(loginConfig.Login, loginConfig.Password);

                if (session != null)
                {
                    this.Session["Session"] = session;
                    return RedirectToAction("UserPage");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        public ActionResult LogOut()
        {
            this.Session["Session"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult UserPage()
        {
            if (this.Session["Session"] != null)
            {
                var session = (SCS.Session)this.Session["Session"];

                var model = new MainModel();
                model.Name = session.User.UserInformation.FirstName + ' ' + session.User.UserInformation.LastName;
                model.AdditionalInfo = session.User.UserInformation.Email;

                var serviceClient = new SCS.ControllerServiceClient();
                model.Notifications = serviceClient.GetNotifications(session, session.User.ID).ToList();

                return View(model);
            }

            ViewBag.Error = "Спочатку увійдіть в систему!";
            return View("Index");
        }
    }
}
