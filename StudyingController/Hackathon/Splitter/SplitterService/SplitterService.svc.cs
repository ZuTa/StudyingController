using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EntityModel;
using ModelDTO;

namespace SplitterService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class SplitterService : ISplitterService
    {

        private T GetDTO<T>(System.Data.Objects.DataClasses.EntityObject o) where T : BaseDTO
        {
            return (o as IDTOable<T>).ToDTO();
        }

        public Session Login(string login, string password)
        {
            return null;
        }

        public bool CanRegister(ModelDTO.SystemUserDTO user)
        {
            try
            {
                bool result = false;

                using (SplitterDBEntities context = new SplitterDBEntities())
                {
                    var systemUser = context.SystemUsers.FirstOrDefault(u => u.Login == user.Login);

                    result = systemUser == null;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<SplitterServiceException>(new SplitterServiceException(ex.Message), ex.Message);
            }
        }

        public void Register(ModelDTO.SystemUserDTO user)
        {
            try
            {
                using (SplitterDBEntities context = new SplitterDBEntities())
                {
                    context.SystemUsers.AddObject(new SystemUser(user));
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<SplitterServiceException>(new SplitterServiceException(ex.Message), ex.Message);
            }
        }
    }
}
