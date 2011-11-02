using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudyingControllerService
{
    [ServiceContract]
    interface IControllerService
    {
        [OperationContract]
        bool IsValidLogin(string login, string password);

        [OperationContract]
        void AddUser(string login, string password);
    }
}
