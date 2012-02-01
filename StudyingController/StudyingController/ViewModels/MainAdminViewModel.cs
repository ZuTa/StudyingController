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
            get { return Model as SystemUserModel; }
        }

        #endregion

        #region Constructors

        public MainAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = new SystemUserDTO();
            Model = new SystemUserModel(originalEntity as SystemUserDTO) { Role = UserRoles.MainAdmin };
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public MainAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, SystemUserDTO mainAdmin)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = mainAdmin;
            Model = new SystemUserModel(mainAdmin);
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
            MainAdmin.Assign(OriginalMainAdmin);
            SetUnModified();
        }

        public override void Save()
        {
            SystemUserDTO mainAdminDTO = MainAdmin.ToDTO();
            if(mainAdminDTO.Password == null)
                mainAdminDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Login);
            else
                mainAdminDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Password);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, mainAdminDTO);
            SetUnModified();
        }

        #endregion
    }
}
