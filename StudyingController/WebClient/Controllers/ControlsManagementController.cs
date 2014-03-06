using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinClient.Models;
using ThinClient.SCS;

namespace ThinClient.Controllers
{
    public class ControlsManagementController : BaseController
    {
        public ActionResult LessonsView()
        {
            var model = new LessonsModel();
            var session = this.GetSession();
            if (session != null)
            {
                model.Lectures = this.serviceClient.GetLectures(session, session.User.ID).ToList();
            }

            return View(model);
        }

        public ActionResult LectureView(int lectureId)
        {
            var model = new LectureModel();
            var session = this.GetSession();
            if (session != null)
            {
                model.Lecture = this.serviceClient.GetLectures(session, session.User.ID).FirstOrDefault(l => l.ID == lectureId);
                model.LectureControls = this.serviceClient.GetLectureControls(session, lectureId);
            }

            return View(model);
        }

        public ActionResult ControlView(int controlId, ControlType controlType)
        {
            var model = new ControlModel();
            var session = this.GetSession();
            if (session != null)
            {
                if (controlType == ControlType.Lecture)
                {
                    model.Control = this.serviceClient.GetControlById(session, controlId);
                    model.Marks = this.serviceClient.GetMarks(session, model.Control)
                        .Select(m => new SecureMarkModel(m));
                }
            }

            return View(model);
        }

        //public JsonResult UpdateUser(UserModel model)
        //{
        //    // Update model to your db
        //    string message = "Success";
        //    return Json(message, JsonRequestBehavior.AllowGet);
        //}
    }
}
