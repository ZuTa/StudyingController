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
    public class InstituteSecretaryViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public InstituteSecretaryDTO OriginalInstituteSecretary
        {
            get { return originalEntity as InstituteSecretaryDTO; }
        }

        public InstituteSecretaryModel InstituteSecretary
        {
            get { return Model as InstituteSecretaryModel; }
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

        public InstituteSecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();

            originalEntity = new InstituteSecretaryDTO() { Role = UserRoles.InstituteSecretary };
        }

        public InstituteSecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, InstituteSecretaryDTO instituteSecretary)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();

            originalEntity = instituteSecretary;
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            ControllerInterop.Service.DeleteUser(ControllerInterop.Session, InstituteSecretary.ID);
        }

        public override void Rollback()
        {
            InstituteSecretary.Assign(OriginalInstituteSecretary);
            SetUnModified();
        }

        public override void Save()
        {
            InstituteSecretaryDTO instituteSecretaryDTO = InstituteSecretary.ToDTO();
            if(instituteSecretaryDTO.Password == null)
                instituteSecretaryDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Login);
            else
                instituteSecretaryDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Password);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, instituteSecretaryDTO);
            SetUnModified();
        }

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();
            Institutes = (DataSource as List<InstituteDTO>);

            InstituteSecretaryDTO instituteSecretary = originalEntity as InstituteSecretaryDTO;
            if (originalEntity.Exists())
            {
                instituteSecretary.Institute = (from institute in institutes
                                                where institute.ID == OriginalInstituteSecretary.InstituteID
                                                select institute).FirstOrDefault();
            }

            Model = new InstituteSecretaryModel(instituteSecretary);
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        protected override void ClearData()
        {
            if (Institutes != null)
                Institutes.Clear();
        }

        #endregion
    }
}
