using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class StudentViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public StudentDTO OriginalStudent
        {
            get { return originalEntity as StudentDTO; }
        }

        public StudentModel Student
        {
            get { return Model as StudentModel; }
        }

        private FacultyDTO faculty;
        public FacultyDTO Faculty
        {
            get { return faculty; }
            set 
            { 
                faculty = value;
                UpdateCathedras();
                OnPropertyChanged("Faculty");
            }
        }

        private List<FacultyDTO> faculties;
        public List<FacultyDTO> Faculties
        {
            get { return faculties; }
            set 
            { 
                faculties = value;
                UpdateCathedras();
                OnPropertyChanged("Faculties");
            }
        }


        private CathedraDTO cathedra;
        public CathedraDTO Cathedra
        {
            get { return cathedra; }
            set 
            { 
                cathedra = value;
                UpdateGroups();
                OnPropertyChanged("Cathedra");
            }
        }

        private List<CathedraDTO> cathedras;
        public List<CathedraDTO> Cathedras
        {
            get { return cathedras; }
            set
            {
                cathedras = value;
                OnPropertyChanged("Cathedras");
            }
        }

        private List<GroupDTO> groups;
        public List<GroupDTO> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }

        #endregion

        #region Constructors

        public StudentViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            groups = new List<GroupDTO>();
            Load();

            originalEntity = new StudentDTO();
            Model = new StudentModel(originalEntity as StudentDTO) { Role = UserRoles.Student };
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public StudentViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, StudentDTO student)
            : base(userInterop, controllerInterop, dispatcher)
        {
            groups = new List<GroupDTO>();
            Load();

            originalEntity = student;
            
            student.Group = ControllerInterop.Service.GetGroupByID(ControllerInterop.Session, student.GroupID);

            Model = new StudentModel(student);

            Cathedra = (from cathedra in ControllerInterop.Service.GetAllCathedras(ControllerInterop.Session)
                        where cathedra.ID == Student.Group.CathedraID
                        select cathedra).FirstOrDefault();

            Faculty = (from faculty in Faculties
                       where faculty.ID == Cathedra.FacultyID
                       select faculty).FirstOrDefault();

            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            ControllerInterop.Service.DeleteUser(ControllerInterop.Session, Student.ID);
        }

        public override void Rollback()
        {
            Student.Assign(OriginalStudent);
            SetUnModified();
        }

        public override void Save()
        {
            StudentDTO studentDTO = Student.ToDTO();
            if(studentDTO.Password == null)
                studentDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Login);
            else
                studentDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Password);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, studentDTO);
            SetUnModified();
        }

        private void UpdateCathedras()
        {
            if (Faculty != null)
            {
                Cathedras = ControllerInterop.Service.GetCathedras(ControllerInterop.Session, Faculty.ID);
                Groups = new List<GroupDTO>();
                
                if (Student.Group != null) Cathedra = (from cathedra in Cathedras
                            where cathedra.ID == Student.Group.CathedraID
                            select cathedra).FirstOrDefault();
            }
        }

        private void UpdateGroups()
        {
            if (Cathedra != null)
            {
                Groups = ControllerInterop.Service.GetGroups(ControllerInterop.Session, Cathedra.ID);
                
                if (Student.Group != null) Student.Group = (from gr in Groups
                                                            where gr.ID == Student.Group.ID
                                                            select gr).FirstOrDefault();
            }
        }

        private void Load()
        {
            Faculties = ControllerInterop.Service.GetAllFaculties(ControllerInterop.Session);
        }

        #endregion
    }
}
