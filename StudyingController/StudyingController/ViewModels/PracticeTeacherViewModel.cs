using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using System.Collections.ObjectModel;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    class PracticeTeacherViewModel:SaveableViewModel
    {
        #region Fields & Properties

        public PracticeTeacherDTO OriginalPracticeTeacher
        {
            get { return originalEntity as PracticeTeacherDTO; }
        }
        public PracticeTeacherModel PracticeTeacher
        {
            get { return Model as PracticeTeacherModel; }
        }

        private ObservableCollection<StudentDTO> unusedStudents;
        public ObservableCollection<StudentDTO> UnusedStudents
        {
            get { return unusedStudents; }
            set 
            {
                if (unusedStudents != value)
                {
                    unusedStudents = value;
                    OnPropertyChanged("UnusedStudents");
                }
            }
        }

        private ObservableCollection<StudentDTO> usedStudents;
        public ObservableCollection<StudentDTO> UsedStudents
        {
            get { return usedStudents; }
            set 
            {
                if (usedStudents != value)
                {
                    usedStudents = value;
                    OnPropertyChanged("UsedStudents");
                }
            }
        }

        private UniversityTree universityTree;
        public UniversityTree UniversityTree
        {
            get { return universityTree; }
            set 
            {
                if (universityTree != value)
                {
                    universityTree = value;
                    OnPropertyChanged("UniversityTree");
                }
            }
        }

        #endregion

        #region Constructors

        public PracticeTeacherViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = new PracticeTeacherDTO();
            this.Model = new PracticeTeacherModel(originalEntity as PracticeTeacherDTO);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            this.UniversityTree = UniversityTree.GetInstance(UserInterop, ControllerInterop, dispatcher, true); //new UniversityTree(UserInterop, ControllerInterop, dispatcher, true);
            UniversityTree.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(UniversityTree_SelectedEntityChangedEvent);
            InitializeSubjects(null);
        }

        public PracticeTeacherViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, PracticeTeacherDTO practiceTeacher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = practiceTeacher;
            this.Model = new PracticeTeacherModel(practiceTeacher);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            this.UniversityTree = UniversityTree.GetInstance(UserInterop, ControllerInterop, dispatcher, true);
            UniversityTree.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(UniversityTree_SelectedEntityChangedEvent);
            InitializeSubjects(null);
        }

        #endregion

        #region Methods

        private void InitializeSubjects(BaseEntityDTO entity)
        {
            unusedStudents = new ObservableCollection<StudentDTO>();
            List<StudentDTO> students = ControllerInterop.Service.GetAllStudents(ControllerInterop.Session);
            UsedStudents = new ObservableCollection<StudentDTO>(PracticeTeacher.Students);
            foreach (StudentDTO student in students)
                if (PracticeTeacher.Students.Find(s => s.ID == student.ID) == null)
                    UnusedStudents.Add(student);

            if (entity is InstituteDTO)
            {
                List<StudentDTO> instituteStudents = ControllerInterop.Service.GetInstituteStudents(ControllerInterop.Session, entity.ID); 
                   UsedStudents = new ObservableCollection<StudentDTO>((from s in usedStudents
                                                                         where instituteStudents.Find(st=> st.ID == s.ID) != null
                                                                         select s).ToList());
                    UnusedStudents = new ObservableCollection<StudentDTO>((from s in unusedStudents
                                                                           where instituteStudents.Find(st => st.ID == s.ID) != null
                                                                           select s).ToList());
            }
            else if (entity is FacultyDTO)
            {
                List<StudentDTO> facultyStudents = ControllerInterop.Service.GetFacultyStudents(ControllerInterop.Session, entity.ID);
                UsedStudents = new ObservableCollection<StudentDTO>((from s in usedStudents
                                                                     where facultyStudents.Find(st => st.ID == s.ID) != null
                                                                     select s).ToList());
                UnusedStudents = new ObservableCollection<StudentDTO>((from s in unusedStudents
                                                                       where facultyStudents.Find(st => st.ID == s.ID) != null
                                                                       select s).ToList());
            }
            else if (entity is CathedraDTO)
            {
                List<StudentDTO> cathedraStudents = ControllerInterop.Service.GetCathedraStudents(ControllerInterop.Session, entity.ID);
                UsedStudents = new ObservableCollection<StudentDTO>((from s in usedStudents
                                                                     where cathedraStudents.Find(st => st.ID == s.ID) != null
                                                                     select s).ToList());
                UnusedStudents = new ObservableCollection<StudentDTO>((from s in unusedStudents
                                                                       where cathedraStudents.Find(st => st.ID == s.ID) != null
                                                                       select s).ToList());
            }
            else if (entity is GroupDTO)
            {
                List<StudentDTO> groupStudents = ControllerInterop.Service.GetGroupStudents(ControllerInterop.Session, entity.ID);
                UsedStudents = new ObservableCollection<StudentDTO>((from s in usedStudents
                                                                     where groupStudents.Find(st => st.ID == s.ID) != null
                                                                     select s).ToList());
                UnusedStudents = new ObservableCollection<StudentDTO>((from s in unusedStudents
                                                                       where groupStudents.Find(st => st.ID == s.ID) != null
                                                                       select s).ToList());
            }
            UsedStudents.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(UsedStudents_CollectionChanged);
            UnusedStudents.CollectionChanged+=new System.Collections.Specialized.NotifyCollectionChangedEventHandler(UsedStudents_CollectionChanged);
        }

        public override void Remove()
        {
            
        }

        public override void Rollback()
        {
            PracticeTeacher.Assign(OriginalPracticeTeacher);
            SetUnModified();
            UniversityTree.IsUseful = true;
            UniversityTree.IsNotChanged = true;
        }

        public override void Save()
        {
            foreach (var student in UsedStudents)
            {
                if (PracticeTeacher.Students.Find(s => s.ID == student.ID) == null)
                    PracticeTeacher.Students.Add(student);
            }
            foreach (var student in UnusedStudents)
            {
                if (PracticeTeacher.Students.Find(s => s.ID == student.ID) != null)
                    PracticeTeacher.Students.Remove(student);
            }
            ControllerInterop.Service.SavePracticeTeacher(ControllerInterop.Session, PracticeTeacher.ToDTO());
            SetUnModified();
            UniversityTree.IsUseful = true;
            UniversityTree.IsNotChanged = true;
        }
        #endregion

        #region Callbacks

        private void UniversityTree_SelectedEntityChangedEvent(object sender, SelectedEntityChangedArgs e)
        {
            UsedStudents.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(UsedStudents_CollectionChanged);
            UnusedStudents.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(UsedStudents_CollectionChanged);
            InitializeSubjects(e.Value);
        }

        private void UsedStudents_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetModified();
            UniversityTree.IsNotChanged = false;
            OnPropertyChanged("UsedStudents");
            OnPropertyChanged("UnusedStudents");
        }

        #endregion
    }
}
