using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class SelectorViewModel:BaseApplicationViewModel
    {
        #region Fields & Properties

        private InstituteDTO institute;
        public InstituteDTO Institute
        {
            get { return institute; }
            set
            {
                if (institute != value)
                {
                    institute = value;
                    //OnPropertyChanged("Institute");
                    UpdateProperties(institute, false);
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
                    //OnPropertyChanged("Faculty");
                    UpdateProperties(faculty, false);
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
                   // OnPropertyChanged("Cathedra");
                    UpdateProperties(cathedra, false);
                }
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
                   // OnPropertyChanged("Group");
                    UpdateProperties(group, false);
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
                    //OnPropertyChanged("Institutes");
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
                    //OnPropertyChanged("Faculties");
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
                   // OnPropertyChanged("Cathedras");
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
                    //OnPropertyChanged("Groups");
                }
            }
        }

        private bool isContainsGroups;
        public bool IsContainsGroups
        {
            get { return isContainsGroups; }
            set
            {
                isContainsGroups = value;
                //OnPropertyChanged("IsContainsGroups");
            }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set 
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        public bool IsEnableCathedras
        {
            get { return Cathedras != null; }
        }

        public bool IsEnableGroups
        {
            get { return Groups != null; }
        }

        public SelectorHelper Helper
        {
            get { return SelectorHelper.Instance; }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                    Helper.IsExpanded = value;
                }
            }
        }

        private BaseEntityDTO currentEntity;
        public BaseEntityDTO CurrentEntity
        {
            get { return currentEntity; }
            set
            {
                currentEntity = value;
                Helper.Entity = value;
            }
        }

        #endregion

        #region Constructors

        public SelectorViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher,bool isContainsGroups)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        public SelectorViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, BaseEntityDTO entity,EventHandler handler,bool isContainsGroups)
            : base(userInterop, controllerInterop, dispatcher)
        {
            LoadDefaultInstitutes();
            
            this.SelectorItemChanged += handler;
            IsContainsGroups = isContainsGroups;
            if (Helper.Entity == null || (isContainsGroups==Helper.IsLecture))
                UpdateProperties(entity, false);
            else
                UpdateProperties(Helper.Entity, false);
            Helper.IsLecture = !isContainsGroups;
            IsEnabled = true;
            IsExpanded = Helper.IsExpanded;
        }

        #endregion

        #region Methods

        public void UpdateProperties(BaseEntityDTO entity, bool isInitialized)
        {
            if (entity == null || entity is InstituteDTO)
            {
                if (entity == null || entity.ID == -1)
                {
                    institute = (from i in Institutes
                                 where i.ID == -1
                                 select i).FirstOrDefault();
                    faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, null);
                    cathedras = null;
                    groups = null;
                }
                else
                {
                    institute = (from i in Institutes
                                 where i.ID == entity.ID
                                 select i).FirstOrDefault();
                    faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, new InstituteRef { ID = entity.ID });
                    cathedras = null;
                    groups = null;
                }
                faculty = null;
                cathedra = null;
                group = null;
            }
            else if (entity is FacultyDTO)
            {
                if (faculties == null || faculties.Find(f => f.ID == entity.ID) == null)
                    UpdateProperties(ControllerInterop.Service.GetInstituteByID(ControllerInterop.Session,(entity as FacultyDTO).InstituteID),true);//(entity as FacultyDTO).Institute, true);
                groups = null;
                cathedra = null;
                group = null;
                cathedras = ControllerInterop.Service.GetCathedras(ControllerInterop.Session, new FacultyRef { ID = entity.ID });
                
                faculty = (from f in Faculties
                           where f.ID == entity.ID
                           select f).FirstOrDefault();
                
            }
            else if (entity is CathedraDTO)
            {
                if (cathedras==null || cathedras.Find(c => c.ID == entity.ID) == null)
                    UpdateProperties(ControllerInterop.Service.GetFacultyByID(ControllerInterop.Session, (entity as CathedraDTO).FacultyID), true);
                group = null;
                groups = ControllerInterop.Service.GetGroups(ControllerInterop.Session, new CathedraRef { ID = entity.ID });
                cathedra = (from c in Cathedras
                            where c.ID == entity.ID
                            select c).FirstOrDefault();
                
            }
            else if (entity is GroupDTO && isContainsGroups)
            {
                if (groups==null || groups.Find(c => c.ID == entity.ID) == null)
                    UpdateProperties(ControllerInterop.Service.GetCathedraByID(ControllerInterop.Session, (entity as GroupDTO).CathedraID), true);
                group = (from g in Groups
                         where g.ID == entity.ID
                         select g).FirstOrDefault();
            }
            if (!isInitialized)
            {
                if (SelectorItemChanged != null)
                    SelectorItemChanged(this, new SelectorItemChangedEventArgs(entity));

                OnPropertyChanged("Institutes");
                OnPropertyChanged("Faculties");
                OnPropertyChanged("Cathedras");
                OnPropertyChanged("Groups");
                OnPropertyChanged("Institute");
                OnPropertyChanged("Faculty");
                OnPropertyChanged("Cathedra");
                OnPropertyChanged("Group");
                OnPropertyChanged("IsEnableGroups");
                OnPropertyChanged("IsEnableCathedras");
            }
            CurrentEntity = entity;
        }

        private void LoadDefaultInstitutes()
        {
            Institutes = new List<InstituteDTO>();
            Institutes.Add(new InstituteDTO { ID = -1, Name = "---Без інституту---" });
            Institutes.AddRange(ControllerInterop.Service.GetInstitutes(ControllerInterop.Session));
        }

        #endregion

        #region Event

        public event EventHandler SelectorItemChanged;

        #endregion

        public class SelectorItemChangedEventArgs : EventArgs
        {
            private BaseEntityDTO entity;
            public BaseEntityDTO Entity
            {
                get { return entity; }
                set { entity = value; }
            }

            public SelectorItemChangedEventArgs(BaseEntityDTO entity)
            {
                this.entity = entity;
            }
        }
    }

    public class SelectorHelper
    {
        private static SelectorHelper instance;
        public static SelectorHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new SelectorHelper();
                return instance;
            }
        }

        private BaseEntityDTO entity;
        public BaseEntityDTO Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        private bool isLecture;
        public bool IsLecture
        {
            get { return isLecture; }
            set { isLecture = value; }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; }
        }

        private SelectorHelper() { }

        public void Clear()
        {
            this.Entity = null;
            //this.IsExpanded = true;
        }
    }
}
