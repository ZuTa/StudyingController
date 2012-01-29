using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class FacultySecretaryViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public FacultySecretaryDTO OriginalFacultySecretary
        {
            get { return originalEntity as FacultySecretaryDTO; }
        }

        public FacultySecretaryModel FacultySecretary
        {
            get { return model as FacultySecretaryModel; }
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

        public FacultySecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            Load();

            originalEntity = new FacultySecretaryDTO();
            model = new FacultySecretaryModel(originalEntity as FacultySecretaryDTO) { Role = UserRoles.FacultySecretary };
            this.model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public FacultySecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, FacultySecretaryDTO facultySecretary)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            Load();

            originalEntity = facultySecretary;
            facultySecretary.Faculty = (from faculty in faculties
                                    where faculty.ID == OriginalFacultySecretary.FacultyID
                                    select faculty).FirstOrDefault();

            model = new FacultySecretaryModel(facultySecretary);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Rollback()
        {
            FacultySecretary.Assign(OriginalFacultySecretary);
            SetUnModified();
        }

        public override void Save()
        {
            FacultySecretaryDTO facultySecretaryDTO = FacultySecretary.ToDTO();
            facultySecretaryDTO.Password = HashHelper.ComputeHash((model as SystemUserModel).Login);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, facultySecretaryDTO);
            SetUnModified();
        }

        public void Load()
        {
            Faculties = ControllerInterop.Service.GetAllFaculties(ControllerInterop.Session);
        }

        #endregion
    }
}
