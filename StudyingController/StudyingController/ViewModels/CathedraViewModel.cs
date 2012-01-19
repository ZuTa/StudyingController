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

        public CathedraDTO OriginalCathedra
        {
            get
            {
                return originalEntity as CathedraDTO;
            }
        }

        public CathedraModel Cathedra
        {
            get { return model as CathedraModel; }         
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

        #endregion

        #region Constructors

        public CathedraViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            Load();

            originalEntity = new CathedraDTO();
            model = new CathedraModel(originalEntity as CathedraDTO);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public CathedraViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, CathedraDTO cathedra)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            Load();
            
            originalEntity = cathedra;
            cathedra.Faculty = (from faculty in faculties
                                where faculty.ID == OriginalCathedra.FacultyID
                                select faculty).FirstOrDefault();

            model = new CathedraModel(cathedra);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public override void Rollback()
        {
            Cathedra.Assign(OriginalCathedra);
            SetUnModified();
        }

        public override void Save()
        {
            CathedraDTO cathedraDTO = Cathedra.ToDTO();
            ControllerInterop.Service.SaveCathedra(ControllerInterop.Session, cathedraDTO);
            SetUnModified();
        }

        private void Load()
        {
            Faculties.AddRange(ControllerInterop.Service.GetAllFaculties(ControllerInterop.Session));
        }

        #endregion

        #region Callbacks

        #endregion
    }
}
