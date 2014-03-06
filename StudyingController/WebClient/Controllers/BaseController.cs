using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThinClient.Controllers
{
    public class BaseController : Controller
    {
        protected SCS.ControllerServiceClient serviceClient = new SCS.ControllerServiceClient();

        public SCS.Session GetSession()
        {
            if (this.Session["Session"] != null)
            {
                return (SCS.Session)this.Session["Session"];
            }
            
            return null;
        }
    }
}