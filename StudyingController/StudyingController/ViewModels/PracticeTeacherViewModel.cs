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

        private InstituteDTO institute;
        public InstituteDTO Institute
        {
            get { return institute; }
            set
            {
                if (institute != value)
                {
                    institute = value;
                    OnPropertyChanged("Institute");
                }
                UpdateProperties(institute,false);
            }
        }

        private FacultyDTO faculty;
        public FacultyDTO Faculty
        {
            get { return faculty; }
            set
            {
                if (faculty != value)
                {
                    faculty = value;
                    OnPropertyChanged("Faculty");
                }
                UpdateProperties(faculty, false);
            }
        }

        private CathedraDTO cathedra;
        public CathedraDTO Cathedra
        {
            get { return cathedra; }
            set
            {
                if (cathedra != value)
                {
                    cathedra = value;
                    OnPropertyChanged("Cathedra");
                }
                UpdateProperties(cathedra, false);
            }
        }

        private GroupDTO group;
        public GroupDTO Group
        {
            get { return group; }
            set
            {
                if (group != value)
                {
                    group = value;
                    OnPropertyChanged("Group");
                }
                UpdateProperties(group, false);
            }
        }

        private List<InstituteDTO> institutes;
        public List<InstituteDTO> Institutes
        {
            get { return institutes; }
            set
            {
                if (institutes != value)
                {
                    institutes = value;
                    OnPropertyChanged("Institutes");
                }
            }
        }

        private List<FacultyDTO> faculties;
        public List<FacultyDTO> Faculties
        {
            get { return faculties; }
            set
            {
                if (faculties != value)
                {
                    faculties = value;
                    OnPropertyChanged("Faculties");
                }
            }
        }

        private List<CathedraDTO> cathedras;
        public List<CathedraDTO> Cathedras
        {
            get { return cathedras; }
            set
            {
                if (cathedras != value)
                {
                    cathedras = value;
                    OnPropertyChanged("Cathedras");
                }
            }
        }

        private List<GroupDTO> groups;
        public List<GroupDTO> Groups
        {
            get { return groups; }
            set 
            {
                if (groups != value)
                {
                    groups = value;
                    OnPropertyChanged("Groups");
                }
            }
        }


        public bool IsEnabled
        {
            get { return !this.IsModified; }
        }

        #endregion

        #region Constructors

        public PracticeTeacherViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = new PracticeTeacherDTO();
            this.Model = new PracticeTeacherModel(originalEntity as PracticeTeacherDTO);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            UpdateProperties(null, false);
        }

        public PracticeTeacherViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, PracticeTeacherDTO practiceTeacher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = practiceTeacher;
            this.Model = new PracticeTeacherModel(practiceTeacher);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            LoadDefaultInstitutes();
            Faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, null);
            UpdateProperties(ControllerInterop.Service.GetTeacher(ControllerInterop.Session, OriginalPracticeTeacher.TeacherID).Cathedra.Faculty, false);
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

        private void UpdateProperties(BaseEntityDTO entity, bool isSubjectsInitialized)
        {
            if (entity is InstituteDTO)
            {
                if (entity == null || entity.ID == -1)
                {
                    Faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, null);
                    Cathedras = null;
                    Groups = null;
                    institute = (from i in Institutes
                                 where i.ID == -1
                                 select i).FirstOrDefault();
                }
                else
                {
                    Faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, entity.ID);
                    Cathedras = null;
                    Groups = null;
                    institute = (from i in Institutes
                                 where i.ID == entity.ID
                                 select i).FirstOrDefault();
                }
                OnPropertyChanged("Institute");
            }
            else if (entity is FacultyDTO)
            {
                if (Faculties.Find(f => f.ID == entity.ID) == null)
                    UpdateProperties((entity as FacultyDTO).Institute, true);
                Cathedras = ControllerInterop.Service.GetCathedras(ControllerInterop.Session, entity.ID);
                Groups = null;
                faculty = (from f in Faculties
                           where f.ID == entity.ID
                           select f).FirstOrDefault();
                OnPropertyChanged("Faculty");
            }
            else if (entity is CathedraDTO)
            {
                if (Cathedras.Find(c => c.ID == entity.ID) == null)
                    UpdateProperties((entity as CathedraDTO).Faculty, true);
                Groups = ControllerInterop.Service.GetGroups(ControllerInterop.Session, entity.ID);
                cathedra = (from c in Cathedras
                            where c.ID == entity.ID
                            select c).FirstOrDefault();
                OnPropertyChanged("Cathedra");
            }
            else if (entity is GroupDTO)
            {
                if (Cathedras.Find(c => c.ID == entity.ID) == null)
                    UpdateProperties((entity as GroupDTO).Cathedra, true);
                group = (from g in Groups
                         where g.ID == entity.ID
                         select g).FirstOrDefault();
                OnPropertyChanged("Group");
            }
            if(!isSubjectsInitialized)
                InitializeSubjects(entity);
        }

        private void LoadDefaultInstitutes()
        {
            Institutes = new List<InstituteDTO>();
            Institutes.Add(new InstituteDTO { ID = -1, Name = "---Без інституту---" });
            Institutes.AddRange(ControllerInterop.Service.GetInstitutes(ControllerInterop.Session));
        }

        public override void Remove()
        {
            
        }

        public override void Rollback()
        {
            PracticeTeacher.Assign(OriginalPracticeTeacher);
            SetUnModified();
            OnPropertyChanged("IsEnabled");
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
            OnPropertyChanged("IsEnabled");
        }
        #endregion

        #region Callbacks

        private void UsedStudents_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetModified();
            OnPropertyChanged("IsEnabled");
            OnPropertyChanged("UsedStudents");
            OnPropertyChanged("UnusedStudents");
        }

        #endregion
    }
}
