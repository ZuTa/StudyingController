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

        [OperationContract(Name = "GetAllFaculties")]
        [FaultContract(typeof(ControllerServiceException))]
        List<FacultyDTO> GetFaculties(Session session);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<FacultyDTO> GetFaculties(Session session, int? instituteID);

        [OperationContract(Name = "GetAllCathedras")]
        [FaultContract(typeof(ControllerServiceException))]
        List<CathedraDTO> GetCathedras(Session session);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<CathedraDTO> GetCathedras(Session session, int facultyID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<GroupDTO> GetGroups(Session session, int cathedraID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveInstitute(Session session, InstituteDTO institute);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveFaculty(Session session, FacultyDTO faculty);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveCathedra(Session session, CathedraDTO cathedra);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveGroup(Session session, GroupDTO group);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<SystemUserDTO> GetUsers(Session session, UserRoles roles);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveUser(Session session, SystemUserDTO user);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<SpecializationDTO> GetSpecializations(Session session, int facultyID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void DeleteInstitute(Session session, int instituteID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void DeleteFaculty(Session session, int facultyID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void DeleteCathedra(Session session, int cathedraID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void DeleteGroup(Session session, int groupID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void DeleteUser(Session session, int userID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        GroupDTO GetGroupByID(Session session, int? groupID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        SpecializationDTO GetSpecializationByID(Session session, int? specializationID);
    }
}
