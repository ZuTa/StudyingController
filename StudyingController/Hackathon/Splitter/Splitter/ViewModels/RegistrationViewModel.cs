using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using ModelDTO;
using System.Security.Cryptography;

namespace Splitter.ViewModels
{
    public class RegistrationViewModel : LoadableViewModel, IPasswordConsumer
    {
        #region Fields & Properties

        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set 
            {
                isLoggedIn = value;
                if (LoggedIn != null) LoggedIn(this, null);
                OnPropertyChanged("IsLoggedIn");
            }
        }

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
                        PasswordSource.SetPassword(user.Password);
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
                    registerCommand = new RelayCommand((param) => Register());
                return registerCommand; 
            }
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new RelayCommand((param) => Login());
                return loginCommand;
            }
        }


        #endregion

        #region Constructors

        public RegistrationViewModel(IUserInterop userInterop,ISplitterInterop splitterInterop, Dispatcher dispatcher)
            : base(userInterop, splitterInterop, dispatcher)
        {
            user = new SystemUserDTO();
        }

        #endregion

        #region Methods

        private void Register()
        {
            if (passwordSource != null)
                User.Password = HashHelper.ComputeHash(passwordSource.GetPassword());

            if (SplitterInterop.Service.CanRegister(User))
            {
                SplitterInterop.Service.Register(User);
                IsLoggedIn = true;
            }
            else UserInterop.ShowMessage("Login already exists");
        }

        private void Login()
        {
            try
            {
                if (passwordSource != null)
                    User.Password = passwordSource.GetPassword();

                SplitterInterop.Service.Login(user.Login, HashHelper.ComputeHash(User.Password));
                IsLoggedIn = true;
            }
            catch (Exception ex)
            {
                UserInterop.ShowMessage(ex.Message);
            }
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

        #region Events

        public event EventHandler LoggedIn;

        #endregion
    }
}
