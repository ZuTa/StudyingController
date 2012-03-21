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
            get { return Model as FacultySecretaryModel; }
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

            originalEntity = new FacultySecretaryDTO() { Role = UserRoles.FacultySecretary };
        }

        public FacultySecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, FacultySecretaryDTO facultySecretary)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();

            originalEntity = facultySecretary;
        }

        #endregion

        #region Methods
        
        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            ControllerInterop.Service.DeleteUser(ControllerInterop.Session, FacultySecretary.ID);
        }

        public override void Rollback()
        {
            FacultySecretary.Assign(OriginalFacultySecretary);
            SetUnModified();
        }

        public override void Save()
        {
            FacultySecretaryDTO facultySecretaryDTO = FacultySecretary.ToDTO();
            if(facultySecretaryDTO.Password == null)
                facultySecretaryDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Login);
            else
                facultySecretaryDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Password);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, facultySecretaryDTO);
            SetUnModified();
        }

        protected override void LoadData()
        {
            Faculties = ControllerInterop.Service.GetAllFaculties(ControllerInterop.Session);

            FacultySecretaryDTO secretary = originalEntity as FacultySecretaryDTO;

            if (secretary.Exists())
            {
                secretary.Faculty = (from faculty in faculties
                                            where faculty.ID == OriginalFacultySecretary.FacultyID
                                            select faculty).FirstOrDefault();
            }

            Model = new FacultySecretaryModel(secretary);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        protected override void ClearData()
        {
            if (faculties != null)
                faculties.Clear();
        }

        #endregion
    }
}
