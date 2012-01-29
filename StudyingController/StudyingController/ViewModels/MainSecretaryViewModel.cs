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
    public class MainSecretaryViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public SystemUserDTO OriginalMainSecretary
        {
            get { return originalEntity as SystemUserDTO; }
        }

        public SystemUserModel MainSecretary
        {
            get { return model as SystemUserModel; }
        }

        #endregion

        #region Constructors

        public MainSecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = new SystemUserDTO();
            model = new SystemUserModel(originalEntity as SystemUserDTO) { Role = UserRoles.MainSecretary };
            this.model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public MainSecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, SystemUserDTO mainSecretary)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = mainSecretary;
            model = new SystemUserModel(mainSecretary);
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
            MainSecretary.Assign(OriginalMainSecretary);
            SetUnModified();
        }

        public override void Save()
        {
            SystemUserDTO mainSecretaryDTO = MainSecretary.ToDTO();
            mainSecretaryDTO.Password = HashHelper.ComputeHash((model as SystemUserModel).Login);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, mainSecretaryDTO);
            SetUnModified();
        }

        #endregion

    }
}
