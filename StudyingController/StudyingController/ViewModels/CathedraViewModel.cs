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
    public class CathedraViewModel : SaveableViewModel
    {
        #region Fields & Properties

        private CathedraDTO originalCathedra;

        private CathedraModel cathedra;
        public CathedraModel Cathedra
        {
            get { return cathedra; }
            set { cathedra = value; }
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
            foreach(InstituteDTO institute in ControllerInterop.Service.GetInstitutes(ControllerInterop.Session))
                Faculties.AddRange(ControllerInterop.Service.GetFaculties(ControllerInterop.Session, institute.ID));

            Faculties.AddRange(ControllerInterop.Service.GetFaculties(ControllerInterop.Session, null));

            Cathedra.Faculty = (from faculty in faculties
                               where faculty.ID == originalCathedra.FacultyID
                               select faculty).FirstOrDefault();
        }

        #endregion

        #region Callbacks

        #endregion
    }
}
