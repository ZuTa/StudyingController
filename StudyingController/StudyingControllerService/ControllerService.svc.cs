using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StudyingControllerEntityModel;

namespace StudyingControllerService
{
    public class ControllerService : IControllerService
    {
        public bool IsValidLogin(string login, string password)
        {
            bool result = false;

            try
            {
                using (UniversityEntities context = new UniversityEntities())
                {
                    var user = context.SystemUsers.Where(u => u.Login == login.ToLower()).FirstOrDefault();
                    result = user != null && Encoding.UTF8.GetString(user.Password) == password;
                }
            }
                //TODO: Handle exceptions
            catch (Exception ex)
            {
            }

            return result;
        }

        public void AddUser(string login, string password)
        {
            try
            {
                using (UniversityEntities context = new UniversityEntities())
                {
                    //var role = (from r in context. where r.ID == 1 select r).FirstOrDefault();
                    //context.SystemUsers.AddObject(new SystemUser() { Login = login.ToLower(), Password = Encoding.UTF8.GetBytes(password), UserRole = role });
                    //context.SaveChanges();
                }
            }
            catch
            {
            }
        }
    }
}
