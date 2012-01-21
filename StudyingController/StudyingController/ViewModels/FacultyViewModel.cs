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

        #endregion

        #region Constructors

        public FacultyViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();
            Load();

            originalEntity = new FacultyDTO();
            model = new FacultyModel(originalEntity as FacultyDTO);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public FacultyViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, FacultyDTO faculty)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();
            Load();
            originalEntity = faculty;
            faculty.Institute = (from institute in institutes
                                 where institute.ID == OriginalFaculty.InstituteID
                                 select institute).FirstOrDefault();

            model = new FacultyModel(faculty);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods
        
        public override void Rollback()
        {
            Faculty.Assign(OriginalFaculty);
            SetUnModified();
        }

        public void Load()
        {
            Institutes = ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
        }

        public override void Save()
        {
            FacultyDTO facultyDTO = Faculty.ToDTO();
            ControllerInterop.Service.SaveFaculty(ControllerInterop.Session, facultyDTO);
            SetUnModified();
        }

        #endregion

        #region Callbacks

        #endregion
    }
}
