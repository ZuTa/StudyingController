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
        [FaultContract(typeof(ControllerServiceException))]
        Session Login(string login, string password);
    }
}
