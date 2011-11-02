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
                    var user = from u in context.SystemUsers
                                      where u.Login == login.ToLower()
                                      select u;
                    foreach (SystemUser su in user)
                    {
                        string s = Encoding.UTF8.GetString(su.Password);
                        result = Encoding.UTF8.GetString(su.Password) == password;
                    }
                }
            }
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
                    var role = (from r in context.UserRoles where r.ID == 1 select r).FirstOrDefault();
                    context.SystemUsers.AddObject(new SystemUser() { Login = login.ToLower(), Password = Encoding.UTF8.GetBytes(password), UserRole = role });
                    context.SaveChanges();
                }
            }
            catch
            {
            }
        }
    }
}
