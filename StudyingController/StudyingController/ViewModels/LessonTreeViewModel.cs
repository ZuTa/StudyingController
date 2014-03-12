using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Diagnostics;
using Common;

namespace StudyingController.ViewModels
{
    class LessonTreeViewModel : BaseTreeViewModel
    {
        #region Fields & Properties

        #endregion

        #region Constructors

        public LessonTreeViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {

        }
        #endregion

        #region Commands

        #endregion

        #region Methods

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetLessonTree(ControllerInterop.Session, ControllerInterop.Session.User.CopyTo(() => new SystemUserRef()));
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();
            BuildUniversityTree();
        }

        private void BuildUniversityTree()
        {
            switch (ControllerInterop.Session.User.Role)
            {
                case UserRoles.MainSecretary:
                case UserRoles.MainAdmin:
                    LoadInstitutes();
                    LoadFaculties(null, null);
                    break;
                case UserRoles.InstituteAdmin:
                case UserRoles.InstituteSecretary:
                    LoadFaculties(null, null);
                    LoadFaculties((ControllerInterop.Session.User as IInstituteable).InstituteID, null);
                    break;
                case UserRoles.FacultyAdmin:
                case UserRoles.FacultySecretary:
                    LoadCathedras((ControllerInterop.Session.User as IFacultyable).FacultyID, null);
                    break;
                case UserRoles.Student:
                case UserRoles.Teacher:
                    LoadLectures(ControllerInterop.User.ID, Tree.AppendNode(new TreeNode("Лекції", null, -1, 4)));
                    LoadPractices(ControllerInterop.User.ID, Tree.AppendNode(new TreeNode("Семінари", null, -1, 5)));
                    break;
                default:
                    throw new NotImplementedException("Unknown user's role!");
            }
        }

        private void LoadInstitutes()
        {
            List<InstituteDTO> institutes = (DataSource as List<BaseEntityDTO>)
                .Where(ds => ds is InstituteDTO)
                .Select(ds=>ds as InstituteDTO)
                .ToList();

            foreach (var institute in institutes)
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(institute.Name, institute, institute.ID, 0));

                    LoadFaculties(institute.ID, node);
                }
            }
        }

        private void LoadFaculties(int? instituteID, TreeNode parentNode)
        {
            List<FacultyDTO> faculties;
            if (instituteID.HasValue)
                faculties = (DataSource as List<BaseEntityDTO>)
                    .Where(ds => ds is FacultyDTO && (ds as FacultyDTO).InstituteID != null && (ds as FacultyDTO).InstituteID == instituteID.Value)
                    .Select(ds => ds as FacultyDTO)
                    .ToList();
            else
                faculties = (DataSource as List<BaseEntityDTO>)
                     .Where(ds => ds is FacultyDTO && (ds as FacultyDTO).InstituteID == null)
                     .Select(ds => ds as FacultyDTO)
                     .ToList();

            foreach (var faculty in faculties)
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(faculty.Name, faculty, faculty.ID, 1), parentNode);

                    LoadCathedras(faculty.ID, node);
                }
            }
        }

        private void LoadCathedras(int facultyID, TreeNode parentNode)
        {
            List<CathedraDTO> cathedras = (DataSource as List<BaseEntityDTO>)
                .Where(ds => ds is CathedraDTO && (ds as CathedraDTO).FacultyID == facultyID)
                .Select(ds => ds as CathedraDTO)
                .ToList();
            foreach (var cathedra in cathedras)
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(cathedra.Name, cathedra, cathedra.ID, 2), parentNode);
                    LoadLectureTeachers(cathedra.ID, node);
                }
            }
        }

        private void LoadLectureTeachers(int cathedraID, TreeNode parentNode)
        {
            List<TeacherDTO> teachers = (DataSource as List<BaseEntityDTO>)
                .Where(ds => ds is TeacherDTO && (ds as TeacherDTO).Cathedra.ID == cathedraID)
                .Select(ds => ds as TeacherDTO)
                .ToList();

            foreach (var teacher in teachers)   
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(teacher.Name, teacher, teacher.ID, 3), parentNode);
                    LoadLectures(teacher.ID, node);
                    LoadPractices(teacher.ID, node);
                }
            }
        }

        private void LoadLectures(int userID, TreeNode parentNode)
        {
            List<LectureDTO> lectures;
            if (ControllerInterop.User.Role == UserRoles.Student)
                lectures = ControllerInterop.Service.GetStudentLectures(ControllerInterop.Session, ControllerInterop.User.CopyTo(() => new StudentRef()));
            else
                lectures = (DataSource as List<BaseEntityDTO>)
                    .Where(ds => ds is LectureDTO && (ds as LectureDTO).TeacherID == userID)
                    .Select(ds => ds as LectureDTO)
                    .ToList();
            
            foreach (var lecture in lectures)
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(lecture.Subject.Name, lecture, lecture.ID, 4), parentNode);
                }
            }
        }

        private void LoadPractices(int userID, TreeNode parentNode)
        {
            List<PracticeTeacherDTO> practicesTeacher;
            if (ControllerInterop.User.Role == UserRoles.Student) 
                practicesTeacher = ControllerInterop.Service.GetStudentPractices(ControllerInterop.Session, ControllerInterop.User.CopyTo(() => new StudentRef()));
            else
                practicesTeacher = (DataSource as List<BaseEntityDTO>)
                    .Where(ds => ds is PracticeTeacherDTO && (ds as PracticeTeacherDTO).Teacher.ID == userID)
                    .Select(ds => ds as PracticeTeacherDTO)
                    .ToList();

            foreach (var practice in practicesTeacher)
            {
                lock (Tree)
                {
                    PracticeDTO pr = ControllerInterop.Service.GetPractice(ControllerInterop.Session, practice.Practice.ID);
                    if (pr != null)
                    {
                        TreeNode node = Tree.AppendNode(new TreeNode(pr.Subject.Name, practice, practice.Practice.ID, 5), parentNode);
                    }
                }
            }
        }

        #endregion

        #region Callbacks
        #endregion

        #region Events

       // event EventHandler 

        #endregion
    }
}
