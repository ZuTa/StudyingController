using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ModelDTO;

namespace SplitterService
{
    [ServiceContract]
    public interface ISplitterService
    {
        [OperationContract]
        [FaultContract(typeof(SplitterServiceException))]
        Session Login(string login, string password);

        [OperationContract]
        [FaultContract(typeof(SplitterServiceException))]
        bool CanRegister(SystemUserDTO user);

        [OperationContract]
        [FaultContract(typeof(SplitterServiceException))]
        void Register(SystemUserDTO user);

    }
}
