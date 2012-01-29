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
            get { return model as InstituteSecretaryModel; }
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
            model = new InstituteSecretaryModel(originalEntity as InstituteSecretaryDTO) { Role = UserRoles.InstituteSecretary };
            this.model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
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

            model = new InstituteSecretaryModel(instituteSecretary);
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
            InstituteSecretary.Assign(OriginalInstituteSecretary);
            SetUnModified();
        }

        public override void Save()
        {
            InstituteSecretaryDTO instituteSecretaryDTO = InstituteSecretary.ToDTO();
            instituteSecretaryDTO.Password = HashHelper.ComputeHash((model as SystemUserModel).Login);
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
