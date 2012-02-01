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
            Load();

            originalEntity = new InstituteSecretaryDTO();
            Model = new InstituteSecretaryModel(originalEntity as InstituteSecretaryDTO) { Role = UserRoles.InstituteSecretary };
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public InstituteSecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, InstituteSecretaryDTO instituteSecretary)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();
            Load();

            originalEntity = instituteSecretary;
            instituteSecretary.Institute = (from institute in institutes
                                        where institute.ID == OriginalInstituteSecretary.InstituteID
                                        select institute).FirstOrDefault();

            Model = new InstituteSecretaryModel(instituteSecretary);
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public override void Remove()
        {
            throw new NotImplementedException();
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

        public void Load()
        {
            Institutes = ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
        }

        #endregion
    }
}
