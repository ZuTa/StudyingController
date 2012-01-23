using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public class UserInformationViewModel : BaseApplicationViewModel
    {
        #region Fields & Properies

        private UserInformationDTO userInformation;
        public UserInformationDTO UserInformation
        {
          get { return userInformation; }
          set
          {
              if (userInformation != value)
              {
                  userInformation = value;
                  OnPropertyChanged("UserInformation");
              }
          }
        }

        #endregion

        #region Constructors

        public UserInformationViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, UserInformationDTO userInformation)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.UserInformation = userInformation;
        }

        #endregion

        #region Commands

        #endregion

        #region Methods

        #endregion

        #region Callbacks

        #endregion

        #region Events

        #endregion
    }
}
