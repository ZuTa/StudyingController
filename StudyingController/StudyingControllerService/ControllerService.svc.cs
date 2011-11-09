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
                    var query = from u in context.SystemUsers select u;
                    foreach (SystemUser su in query)
                    {
                        SystemUser ss = su;
                    }
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
                    Faculty faculty = new Faculty { Name = "Факультет Кібернетики" };
                    UserInformation userInformation = new UserInformation { FirstName = "Ivan", LastName = "Ivanov", Email = "email@email.com" };
                    context.SystemUsers.AddObject(new FacultyAdmin() { Login = login.ToLower(), Password = Encoding.UTF8.GetBytes(password), UserRole = UserRoles.FacultyAdmin, UserInformation = userInformation, Faculty = faculty });
                    context.SaveChanges();
                }
            }
            catch
            {
            }
        }
    }
}
