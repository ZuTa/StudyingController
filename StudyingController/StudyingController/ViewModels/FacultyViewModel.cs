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
    public class FacultyViewModel : SaveableViewModel
    {

        #region Fields & Properties

        public FacultyDTO OriginalFaculty
        {
            get { return originalEntity as FacultyDTO; }
        }
      
        public FacultyModel Faculty
        {
            get { return model as FacultyModel; }
        }

        private List<InstituteDTO> institutes;
        public List<InstituteDTO> Institutes
        {
            get { return institutes; }
            set 
            { 
                institutes = value;
                OnPropertyChanged("Institutes");
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

        public FacultyViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();
            originalEntity = new FacultyDTO();

            if (!(originalEntity is FacultyDTO))
                throw new InvalidCastException("Unknown entity's type");

            model = new FacultyModel(originalEntity as FacultyDTO);
            Load();
        }

        public FacultyViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, FacultyDTO faculty)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();
            this.originalEntity = faculty;
            this.model = new FacultyModel(originalEntity as FacultyDTO);
            Load();
        }

        #endregion

        #region Methods

        public void Load()
        {
            Institutes = ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
            Faculty.Institute = (from institute in institutes 
                                 where institute.ID==OriginalFaculty.InstituteID 
                                 select institute).FirstOrDefault();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Callbacks

        #endregion
    }
}
