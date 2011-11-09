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
        public Session Login(string login, string password)
        {
            Session session = null;

            try
            {
                using (UniversityEntities context = new UniversityEntities())
                {
                    var user = context.SystemUsers.Where(u => u.Login == login.ToLower()).FirstOrDefault();
                    if (!(user != null && Encoding.UTF8.GetString(user.Password) == password))
                        throw new Exception("У доступі відмовлено!");

                    session = new Session();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }

            return session;
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
