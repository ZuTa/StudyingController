using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public class CathedraViewModel : BaseApplicationViewModel, ISaveable
    {
        #region Fields & Properties

        private CathedraDTO originalCathedra;

        private CathedraModel cathedra;
        public CathedraModel Cathedra
        {
            get { return cathedra; }
            set { cathedra = value; }
        }

        private FacultyDTO selectedFaculty;
        public FacultyDTO SelectedFaculty
        {
            get { return selectedFaculty; }
            set
            {
                if (selectedFaculty != value)
                {
                    selectedFaculty = value;
                    OnPropertyChanged("SelectedFaculty");
                }
            }
        }

        private List<FacultyDTO> faculties;
        public List<FacultyDTO> Faculties
        {
            get { return faculties; }
            set
            {
                faculties = value;
                OnPropertyChanged("Faculties");
            }
        }


        public bool IsModified
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CanModify
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSave
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Constructors

        public CathedraViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            originalCathedra = new CathedraDTO();
            cathedra = new CathedraModel(originalCathedra);
            Load();
        }

        public CathedraViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, CathedraDTO cathedra)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            this.originalCathedra = cathedra;
            this.cathedra = new CathedraModel(originalCathedra);
            Load();
        }

        #endregion

        #region Methods

        public void Save()
        {
            throw new NotImplementedException();
        }

        private void Load()
        {
            List<InstituteDTO> list = ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
            foreach(InstituteDTO institute in ControllerInterop.Service.GetInstitutes(ControllerInterop.Session))
                Faculties.AddRange(ControllerInterop.Service.GetFaculties(ControllerInterop.Session, institute.ID));
            SelectedFaculty = (from faculty in faculties
                               where faculty.ID == originalCathedra.FacultyID
                               select faculty).FirstOrDefault();
        }

        public CathedraDTO ModelToDTO(CathedraModel model)
        {
            return new CathedraDTO() { ID = model.ID, Name = model.Name, FacultyID = SelectedFaculty.ID };
        }

        #endregion

        #region Callbacks

        #endregion
    }
}
