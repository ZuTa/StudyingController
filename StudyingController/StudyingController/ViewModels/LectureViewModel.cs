using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    class LectureViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public LectureDTO OriginalLecture
        {
            get { return originalEntity as LectureDTO; }
        }

        public LectureModel Lecture
        {
            get { return Model as LectureModel; }
        }

        private ObservableCollection<GroupDTO> unusedGroups;
        public ObservableCollection<GroupDTO> UnusedGroups
        {
            get { return unusedGroups; }
            set
            {
                if (unusedGroups != value)
                {
                    unusedGroups = value;
                    OnPropertyChanged("UnusedGroups");
                }
            }
        }

        private ObservableCollection<GroupDTO> usedGroups;
        public ObservableCollection<GroupDTO> UsedGroups
        {
            get { return usedGroups; }
            set
            {
                if (usedGroups != value)
                {
                    usedGroups = value;
                    OnPropertyChanged("UsedGroups");
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
                    UpdateProperties(institute,false);
                    OnPropertyChanged("Institute");
                }
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
                    UpdateProperties(faculty, false);
                    OnPropertyChanged("Faculty");
                }
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
                    UpdateProperties(cathedra, false);
                    OnPropertyChanged("Cathedra");
                }
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

        public bool IsEnabled
        {
            get { return !IsModified; }
        }

        #endregion

        #region Constructors

         public LectureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = new LectureDTO();
            this.Model = new LectureModel(originalEntity as LectureDTO);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            UpdateProperties(null, false);
        }

        public LectureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, LectureDTO lecture)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = lecture;
            this.Model = new LectureModel(lecture);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            LoadDefaultInstitutes();
            Faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, null);
            UpdateProperties(ControllerInterop.Service.GetTeacher(ControllerInterop.Session, OriginalLecture.TeacherID).Cathedra.Faculty, false);
        }

        #endregion

        #region Methods

        private void InitializeGroups(BaseEntityDTO entity)
        {
            unusedGroups = new ObservableCollection<GroupDTO>();
            List<GroupDTO> groups = ControllerInterop.Service.GetAllGroups(ControllerInterop.Session);
            UsedGroups = new ObservableCollection<GroupDTO>(Lecture.Groups);
            foreach (GroupDTO group in groups)
                if (Lecture.Groups.Find(g => g.ID == group.ID) == null)
                    UnusedGroups.Add(group);

            if (entity is InstituteDTO)
            {
                List<GroupDTO> instituteGroups = ControllerInterop.Service.GetInstituteGroups(ControllerInterop.Session, entity.ID);
                UsedGroups = new ObservableCollection<GroupDTO>((from g in usedGroups
                                                                     where instituteGroups.Find(st => st.ID == g.ID) != null
                                                                     select g).ToList());
                UnusedGroups = new ObservableCollection<GroupDTO>((from g in unusedGroups
                                                                       where instituteGroups.Find(st => st.ID == g.ID) != null
                                                                       select g).ToList());
            }
            else if (entity is FacultyDTO)
            {
                List<GroupDTO> facultyGroups = ControllerInterop.Service.GetFacultyGroups(ControllerInterop.Session, entity.ID);
                UsedGroups = new ObservableCollection<GroupDTO>((from g in usedGroups
                                                                 where facultyGroups.Find(st => st.ID == g.ID) != null
                                                                 select g).ToList());
                UnusedGroups = new ObservableCollection<GroupDTO>((from g in unusedGroups
                                                                   where facultyGroups.Find(st => st.ID == g.ID) != null
                                                                   select g).ToList());
            }
            else if (entity is CathedraDTO)
            {
                List<GroupDTO> cathedraGroups = ControllerInterop.Service.GetGroups(ControllerInterop.Session, entity.ID);
                UsedGroups = new ObservableCollection<GroupDTO>((from g in usedGroups
                                                                 where cathedraGroups.Find(st => st.ID == g.ID) != null
                                                                 select g).ToList());
                UnusedGroups = new ObservableCollection<GroupDTO>((from g in unusedGroups
                                                                   where cathedraGroups.Find(st => st.ID == g.ID) != null
                                                                   select g).ToList());
            }
            UsedGroups.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(UsedGroups_CollectionChanged);
            UnusedGroups.CollectionChanged +=new System.Collections.Specialized.NotifyCollectionChangedEventHandler(UsedGroups_CollectionChanged);
        }

        private void UpdateProperties(BaseEntityDTO entity,bool isGroupsInitialized)
        {
            if (entity is InstituteDTO)
            {
                if (entity == null || entity.ID == -1)
                {
                    Faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, null);
                    Cathedras = null;
                    institute = (from i in Institutes
                                 where i.ID == -1
                                 select i).FirstOrDefault();
                }
                else
                {
                    Faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, entity.ID);
                    Cathedras = null;
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
                faculty = (from f in Faculties
                           where f.ID == entity.ID
                           select f).FirstOrDefault();
                OnPropertyChanged("Faculty");
            }
            else if (entity is CathedraDTO)
            {
                if (Cathedras.Find(c => c.ID == entity.ID) == null)
                    UpdateProperties((entity as CathedraDTO).Faculty, true);
                cathedra = (from c in Cathedras
                            where c.ID == entity.ID
                            select c).FirstOrDefault();
                OnPropertyChanged("Cathedra");
            }
            if (!isGroupsInitialized)
                InitializeGroups(entity);
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
            Lecture.Assign(OriginalLecture);
            SetUnModified();
            OnPropertyChanged("IsEnabled");
        }

        public override void Save()
        {
            foreach (var group in UsedGroups)
            {
                if (Lecture.Groups.Find(g => g.ID == group.ID) == null)
                    Lecture.Groups.Add(group);
            }
            foreach (var group in UnusedGroups)
            {
                if (Lecture.Groups.Find(g => g.ID == group.ID) != null)
                    Lecture.Groups.Remove(group);
            }
            ControllerInterop.Service.SaveLecture(ControllerInterop.Session, Lecture.ToDTO());
            SetUnModified();
            OnPropertyChanged("IsEnabled");
        }
        #endregion

        #region Callbacks

        private void UsedGroups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetModified();
            OnPropertyChanged("IsEnabled");
            OnPropertyChanged("UsedGroups");
            OnPropertyChanged("UnusedGroups");
        }


        #endregion
    }
}
