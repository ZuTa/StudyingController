using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerService
{
    [ServiceContract]
    interface IControllerService
    {
        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        Session Login(string login, string password);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<InstituteDTO> GetInstitutes(Session session);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<FacultyDTO> GetFaculties(Session session, int? instituteID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<CathedraDTO> GetCathedras(Session session, int facultyID);

    }
}
