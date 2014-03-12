using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public class InstituteAdminViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public InstituteAdminDTO OriginalInstituteAdmin
        {
            get { return originalEntity as InstituteAdminDTO; }
        }

        public InstituteAdminModel InstituteAdmin
        {
            get { return Model as InstituteAdminModel; }
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

        public InstituteAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();

            originalEntity = new InstituteAdminDTO() { Role = UserRoles.InstituteAdmin };
        }

        public InstituteAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, InstituteAdminDTO instituteAdmin)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();

            originalEntity = instituteAdmin;
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            ControllerInterop.Service.DeleteUser(ControllerInterop.Session, InstituteAdmin.ID);
        }

        public override void Rollback()
        {
            InstituteAdmin.Assign(OriginalInstituteAdmin);
            SetUnModified();
        }

        public override void Save()
        {
            InstituteAdminDTO instituteAdminDTO = InstituteAdmin.ToDTO();
            if(instituteAdminDTO.Password == null)
                instituteAdminDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Login);
            else
                instituteAdminDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Password);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, instituteAdminDTO);
            SetUnModified();
        }

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
        }

        protected override void AfterDataLoaded()
        {
            Institutes = DataSource as List<InstituteDTO>;

            InstituteAdminDTO instituteAdmin = originalEntity as InstituteAdminDTO;

            if (originalEntity.Exists())
            {
                instituteAdmin.Institute = (from institute in institutes
                                            where institute.ID == OriginalInstituteAdmin.InstituteID
                                            select institute).FirstOrDefault();
            }

            Model = new InstituteAdminModel(instituteAdmin);
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        protected override void ClearData()
        {
            if (institutes != null)
                Institutes.Clear();
        }

        #endregion
    }
}
