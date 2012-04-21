using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using ModelDTO;

namespace Splitter.ViewModels
{
    public class RegistrationViewModel : LoadableViewModel, IPasswordConsumer
    {
        #region Fields & Properties

        private SystemUserDTO user;
        public SystemUserDTO User
        {
            get { return user; }
            set { user = value; }
        }

        private IPasswordSource passwordSource;
        public IPasswordSource PasswordSource
        {
            get
            {
                return passwordSource;
            }
            set
            {
                if (passwordSource != value)
                {
                    passwordSource = value;
                    if (passwordSource != null)
                        PasswordSource.SetPassword(User.Password);
                    OnPropertyChanged("PasswordSource");
                }
            }
        }

        private RelayCommand registerCommand;
        public RelayCommand RegisterCommand
        {
            get 
            {
                if (registerCommand == null)
                    registerCommand = new RelayCommand((param) => RegisterUser());
                return registerCommand; 
            }
        }

        #endregion

        #region Constructors

        public RegistrationViewModel(IUserInterop userInterop,ISplitterInterop splitterInterop, Dispatcher dispatcher)
            : base(userInterop, splitterInterop, dispatcher)
        {
            
        }

        #endregion

        #region Methods

        private void RegisterUser()
        {
 
        }

        protected override void LoadData()
        {
            throw new NotImplementedException();
        }

        protected override void ClearData()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
