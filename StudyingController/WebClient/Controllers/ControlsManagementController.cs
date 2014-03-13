using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinClient.Common;
using ThinClient.Models;
using ThinClient.SCS;
using Common;

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
                var teacherRef = new TeacherRef() { ID = session.User.ID };
                model.Lectures = this.serviceClient.GetLectures(session, teacherRef).ToList();
                model.Practices = this.serviceClient.GetPracticesTeacher(session, teacherRef).ToList();
            }

            return View(model);
        }

        public ActionResult LectureView(int lectureId)
        {
            var model = new LectureModel();
            var session = this.GetSession();
            if (session != null)
            {
                var teacherRef = new TeacherRef() { ID = session.User.ID };
                model.Lecture = this.serviceClient.GetLectures(session, teacherRef).FirstOrDefault(l => l.ID == lectureId);
                model.LectureControls = this.serviceClient.GetLectureControls(session, lectureId);
            }

            return View(model);
        }

        public ActionResult PracticeView(int practiceId)
        {
            var model = new PracticeModel();
            var session = this.GetSession();
            if (session != null)
            {
                var teacherRef = new TeacherRef() { ID = session.User.ID };
                model.Practice = this.serviceClient.GetPracticesTeacher(session, teacherRef).FirstOrDefault(l => l.ID == practiceId);
                model.PracticeControls = this.serviceClient.GetPracticeControls(session, practiceId);
            }

            return View(model);
        }

        public ActionResult ControlView(int controlId)
        {
            var model = new ControlModel();
            //var session = this.GetSession();
            //if (session != null)
            //{
            //    model.Control = this.serviceClient.GetControlById(session, controlId);
            //    model.Marks = this.serviceClient.GetMarks(session, model.Control)
            //        .Select(m => new MarkModel(m)).OrderBy(m => m.StudentName);
            //}

            return View(model);
        }

        public JsonResult UpdateMark(MarkModel mark)
        {
            var session = this.GetSession();
            if (session != null)
            {
                var markRef = new MarkRef() 
                {
                    ID = Convert.ToInt32(Encryptor.Decrypt(mark.EncryptedId)),
                    Value = Convert.ToDecimal(mark.MarkValue)
                };

                this.serviceClient.UpdateMarkValue(session, markRef);
            }

            string message = "Success";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
