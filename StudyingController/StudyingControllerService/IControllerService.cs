﻿using System;
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
    public interface IControllerService : IRefService
    {
        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        Session Login(string login, string password);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<InstituteDTO> GetInstitutes(Session session);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<FacultyDTO> GetFaculties(Session session, InstituteRef institute);

        [OperationContract(Name = "GetAllFaculties")]
        [FaultContract(typeof(ControllerServiceException))]
        List<FacultyDTO> GetFaculties(Session session);

        [OperationContract(Name = "GetAllCathedras")]
        [FaultContract(typeof(ControllerServiceException))]
        List<CathedraDTO> GetCathedras(Session session);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<CathedraDTO> GetCathedras(Session session, FacultyRef faculty);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<GroupDTO> GetGroups(Session session, CathedraRef cathedra);

        [OperationContract(Name = "GetAllGroups")]
        [FaultContract(typeof(ControllerServiceException))]
        List<GroupDTO> GetGroups(Session session);

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

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<LectureDTO> GetLectures(Session session, TeacherRef teacher);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<GroupDTO> GetLectureGroups(Session session, int lectureID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<TeacherDTO> GetTeachers(Session session, CathedraRef cathedra);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<SubjectDTO> GetSubjects(Session session, int cathedraID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveLecture(Session session, LectureDTO lecture);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveTeacherSubjects(Session session, int teacherID, List<SubjectDTO> subjects);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<PracticeTeacherDTO> GetPracticesTeacher(Session session, TeacherRef teacher);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<GroupDTO> GetGroupsPractice(Session session, int practiceTeacherID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<SubjectDTO> GetTeacherPracticeSubjects(Session session, int teacherID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<StudentDTO> GetAllStudents(Session session);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<StudentDTO> GetGroupStudents(Session session, int groupID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<StudentDTO> GetCathedraStudents(Session session, int cathedraID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<StudentDTO> GetFacultyStudents(Session session, int facultyID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<StudentDTO> GetInstituteStudents(Session session, int instituteID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SavePracticeTeacher(Session session, PracticeTeacherDTO practiceTeacher);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<GroupDTO> GetFacultyGroups(Session session, int facultyID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<GroupDTO> GetInstituteGroups(Session session, int instituteID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SavePracticeTeacherSubjects(Session session, int teacherID, List<SubjectDTO> subjects);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        TeacherDTO GetTeacher(Session session, int teacherID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        InstituteDTO GetInstituteByID(Session session, int? instituteID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        FacultyDTO GetFacultyByID(Session session, int facultyID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        CathedraDTO GetCathedraByID(Session session, int cathedraID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<LectureControlDTO> GetLectureControls(Session session, int lectureID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveLectureControls(Session session, int lectureID, List<LectureControlDTO> controls);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<PracticeControlDTO> GetPracticeControls(Session session, int practiceID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SavePracticeControls(Session session, int practiceID, List<PracticeControlDTO> controls);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<ControlMessageDTO> GetControlMessages(Session session, int controlID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveControlMessage(Session session, ControlMessageDTO message);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveLectureControl(Session session, LectureControlDTO control);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<LectureDTO> GetStudentLectures(Session session, StudentRef student);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<AttachmentDTO> GetAttachments(Session session, int teacherID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveAttachments(Session session, int userID, List<AttachmentDTO> attachments);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void DeleteAttachment(Session session, int attachmentID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        decimal GetLectureMark(Session session, int studentID, int controlID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        decimal GetPracticeMark(Session session, int studentID, int controlID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SavePracticeControl(Session session, PracticeControlDTO control);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<PracticeTeacherDTO> GetStudentPractices(Session session, StudentRef student);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<MarkDTO> GetMarks(Session session, ControlDTO control);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveMarks(Session session, ControlDTO control, List<MarkDTO> marks);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<UserRateItemDTO> GetStudentRateList(Session session, BaseEntityDTO universityStructureItem);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<NotificationDTO> GetNotifications(Session session, int userID);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        GroupDTO GetGroup(Session session, int id);
        
        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        PracticeDTO GetPractice(Session session, int id);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        SpecializationDTO GetSpecialization(Session session, int id);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        CathedraDTO GetCathedra(Session session, int id);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        FacultyDTO GetFaculty(Session session, int id);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        InstituteDTO GetInstitute(Session session, int id);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        StudentDTO GetStudent(Session session, int id);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<VisitingsDTO> GetVisitingsForLecture(Session session, LectureRef lecture);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void SaveVisitingsForLecture(Session session, LectureRef lecture, List<VisitingDTO> visitings);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        ControlDTO GetControlById(Session session, int id);

        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        void UpdateMarkValue(Session session, MarkRef mark);

        [ServiceKnownType(typeof(InstituteDTO))]
        [ServiceKnownType(typeof(FacultyDTO))]
        [ServiceKnownType(typeof(CathedraDTO))]
        [ServiceKnownType(typeof(TeacherDTO))]
        [ServiceKnownType(typeof(LectureDTO))]
        [ServiceKnownType(typeof(PracticeTeacherDTO))]
        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<BaseEntityDTO> GetLessonTree(Session session, SystemUserRef user);

        [ServiceKnownType(typeof(InstituteDTO))]
        [ServiceKnownType(typeof(FacultyDTO))]
        [ServiceKnownType(typeof(CathedraDTO))]
        [ServiceKnownType(typeof(GroupDTO))]
        [OperationContract]
        [FaultContract(typeof(ControllerServiceException))]
        List<BaseEntityDTO> GetUniversityTree(Session session, SystemUserRef user);
    }
}
