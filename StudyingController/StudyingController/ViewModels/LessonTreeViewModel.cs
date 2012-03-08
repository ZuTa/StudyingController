using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;

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

        protected override void LoadData()
        {
            base.LoadData();
            BuildUniversityTree();
        }

        private void BuildUniversityTree()
        {
            StartLoading();

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

            StopLoading();
        }

        private void LoadInstitutes()
        {
            List<InstituteDTO> institutes = ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
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
                faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, instituteID.Value);
            else
                faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, null);

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
            List<CathedraDTO> cathedras = ControllerInterop.Service.GetCathedras(ControllerInterop.Session, facultyID);
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
            List<TeacherDTO> teachers = ControllerInterop.Service.GetTeachers(ControllerInterop.Session, cathedraID);

            foreach (var teacher in teachers)   
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(teacher.UserInformation.LastName, teacher, teacher.ID, 3), parentNode);
                    LoadLectures(teacher.ID, node);
                    LoadPractices(teacher.ID, node);
                }
            }
        }

        private void LoadLectures(int userID, TreeNode parentNode)
        {
            List<LectureDTO> lectures;
            if (ControllerInterop.User.Role == UserRoles.Student) lectures = ControllerInterop.Service.GetStudentLectures(ControllerInterop.Session, userID);
            else lectures = ControllerInterop.Service.GetLectures(ControllerInterop.Session, userID);
            
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
            List<PracticeTeacherDTO> practicesTeacher = ControllerInterop.Service.GetPracticesTeacher(ControllerInterop.Session, userID);
            foreach (var practice in practicesTeacher)
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(practice.Practice.Subject.Name, practice, practice.PracticeID, 5), parentNode);
                }
            }
        }

        #endregion

        #region Callbacks
        #endregion

        #region Events

        #endregion
    }
}
