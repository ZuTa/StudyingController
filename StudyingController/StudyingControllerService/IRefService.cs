using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace StudyingControllerService
{
    [ServiceContract]
    public interface IRefService
    {
        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        IEnumerable<LectureRef> GetLectureRefsByTeacherId(Session session, int teacherId);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        IEnumerable<PracticeTeacherRef> GetPracticeTeacherRefsByTeacherId(Session session, int teacherId);
    }
}