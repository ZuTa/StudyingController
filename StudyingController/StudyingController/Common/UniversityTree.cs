using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using StudyingController.ViewModels;
using EntitiesDTO;

namespace StudyingController.Common
{
    class UniversityTree:BaseTreeViewModel
    {
        #region Fields and Properties
        private bool isIncludeGroups;

        private bool isNotChanged;
        public bool IsNotChanged
        {
            get { return isNotChanged; }
            set 
            {
                if (isNotChanged != value)
                {
                    isNotChanged = value;
                    OnPropertyChanged("IsNotChanged");
                }
            }
        }

        private bool isUseful;
        public bool IsUseful
        {
            get { return isUseful; }
            set { isUseful = value; }
        }

        private static UniversityTree instance;

        #endregion

        #region Constructor

        private UniversityTree(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, bool isIncludeGroups)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.isIncludeGroups = isIncludeGroups;
            IsNotChanged = true;
            if(isIncludeGroups)
                Load();
        }

        #endregion

        #region Methods

        public static UniversityTree GetInstance(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, bool isIncludeGroups)
        {
            if (instance == null || instance.isUseful == false)
                instance = new UniversityTree(userInterop, controllerInterop, dispatcher, isIncludeGroups);
            instance.isUseful = false;
            return instance;
        }


        protected override void LoadData()
        {
            base.LoadData();

            BuildUniversityTree();
        }

        private void BuildUniversityTree()
        {
            LoadInstitutes();
            LoadFaculties(null, null);
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
                    if(isIncludeGroups)
                        LoadGroups(cathedra.ID, node);
                }
            }
        }

        private void LoadGroups(int cathedraID, TreeNode parentNode)
        {
            List<GroupDTO> groups = ControllerInterop.Service.GetGroups(ControllerInterop.Session, cathedraID);
            foreach (var group in groups)
            {
                TreeNode node = Tree.AppendNode(new TreeNode(group.Name, group, group.ID, 3), parentNode);
            }
        }
        #endregion
    }
}
