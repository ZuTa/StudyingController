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
    public class FacultyAdminViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public FacultyAdminDTO OriginalFacultyAdmin
        {
            get { return originalEntity as FacultyAdminDTO; }
        }

        public FacultyAdminModel FacultyAdmin
        {
            get { return model as FacultyAdminModel; }
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

        public FacultyAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            Load();

            originalEntity = new FacultyAdminDTO();
            model = new FacultyAdminModel(originalEntity as FacultyAdminDTO) { Role = UserRoles.FacultyAdmin };
            this.model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public FacultyAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, FacultyAdminDTO facultyAdmin)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            Load();

            originalEntity = facultyAdmin;
            facultyAdmin.Faculty = (from faculty in faculties
                                        where faculty.ID == OriginalFacultyAdmin.FacultyID
                                        select faculty).FirstOrDefault();

            model = new FacultyAdminModel(facultyAdmin);
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
            FacultyAdmin.Assign(OriginalFacultyAdmin);
            SetUnModified();
        }

        public override void Save()
        {
            FacultyAdminDTO facultyAdminDTO = FacultyAdmin.ToDTO();
            facultyAdminDTO.Password = HashHelper.ComputeHash((model as SystemUserModel).Login);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, facultyAdminDTO);
            SetUnModified();
        }

        public void Load()
        {
            Faculties = ControllerInterop.Service.GetAllFaculties(ControllerInterop.Session);
        }

        #endregion
    }
}
