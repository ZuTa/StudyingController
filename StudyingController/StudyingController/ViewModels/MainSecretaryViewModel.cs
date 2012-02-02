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
            get { return Model as SystemUserModel; }
        }

        #endregion

        #region Constructors

        public MainSecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = new SystemUserDTO();
            Model = new SystemUserModel(originalEntity as SystemUserDTO) { Role = UserRoles.MainSecretary };
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public MainSecretaryViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, SystemUserDTO mainSecretary)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = mainSecretary;
            Model = new SystemUserModel(mainSecretary);
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public override void Remove()
        {
            ControllerInterop.Service.DeleteUser(ControllerInterop.Session, MainSecretary.ID);
        }

        public override void Rollback()
        {
            MainSecretary.Assign(OriginalMainSecretary);
            SetUnModified();
        }

        public override void Save()
        {
            SystemUserDTO mainSecretaryDTO = MainSecretary.ToDTO();
            if(mainSecretaryDTO.Password == null)
                mainSecretaryDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Login);
            else
                mainSecretaryDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Password);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, mainSecretaryDTO);
            SetUnModified();
        }

        #endregion

    }
}
