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
    public class FacultyViewModel : BaseApplicationViewModel, ISaveable
    {

        #region Fields & Properties
        
        private FacultyDTO originalFaculty;

        private FacultyModel faculty;
        public FacultyModel Faculty
        {
            get { return faculty; }
            set { faculty = value; }
        }

        private InstituteDTO selectedInstitute;
        public InstituteDTO SelectedInstitute
        {
            get { return selectedInstitute; }
            set 
            {
                if (selectedInstitute != value)
                {
                    selectedInstitute = value;
                    OnPropertyChanged("SelectedInstitute");
                }
            }
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
            originalFaculty = new FacultyDTO();
            faculty = new FacultyModel(originalFaculty);
            Load();
        }

        public FacultyViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, FacultyDTO faculty)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();
            this.originalFaculty = faculty;
            this.faculty = new FacultyModel(originalFaculty);
            Load();
        }

        #endregion

        #region Methods

        public void Load()
        {
            Institutes = ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
            SelectedInstitute = (from institute in institutes 
                                 where institute.ID==originalFaculty.InstituteID 
                                 select institute).FirstOrDefault();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public FacultyDTO ModelToDTO(FacultyModel model)
        {
            return new FacultyDTO() { ID = model.ID, Name = model.Name, InstituteID = SelectedInstitute.ID };
        }

        #endregion

        #region Callbacks

        #endregion
    }
}
