using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public class UserProfileViewModel : LoadableViewModel
    {
        #region Fields & Properties

        private SystemUserDTO user;
        public SystemUserDTO User
        {
            get { return user; }
            set { user = value; }
        }


        #endregion

        #region Constructors

        public UserProfileViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, SystemUserDTO user)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.user = user;
        }

        #endregion

        #region Methods

        protected override void LoadData()
        {

        }

        protected override void ClearData()
        {
        }

        #endregion
    }
}
