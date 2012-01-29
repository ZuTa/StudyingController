using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.ViewModels.Models;
using EntitiesDTO;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class MainAdminViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public SystemUserDTO OriginalMainAdmin
        {
            get { return originalEntity as SystemUserDTO; }
        }

        public SystemUserModel MainAdmin
        {
            get { return model as SystemUserModel; }
        }

        #endregion

        #region Constructors

        public MainAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = new SystemUserDTO();
            model = new SystemUserModel(originalEntity as SystemUserDTO) { Role = UserRoles.MainAdmin };
            this.model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public MainAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, SystemUserDTO mainAdmin)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = mainAdmin;
            model = new SystemUserModel(mainAdmin);
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
            MainAdmin.Assign(OriginalMainAdmin);
            SetUnModified();
        }

        public override void Save()
        {
            SystemUserDTO mainAdminDTO = MainAdmin.ToDTO();
            mainAdminDTO.Password = HashHelper.ComputeHash((model as SystemUserModel).Login);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, mainAdminDTO);
            SetUnModified();
        }

        #endregion
    }
}
