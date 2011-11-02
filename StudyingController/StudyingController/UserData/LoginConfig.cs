using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace StudyingController.UserData
{
    public class LoginConfig : INotifyPropertyChanged
    {
        #region Fields & Properties

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private bool isMemorizeLogin;
        public bool IsMemorizeLogin
        {
            get { return isMemorizeLogin; }
            set
            {
                if (isMemorizeLogin != value)
                {
                    isMemorizeLogin = value;
                    OnPropertyChanged("IsMemorizeLogin");
                }
            }
        }

        private bool isAutologin;
        public bool IsAutologin
        {
            get { return isAutologin; }
            set
            {
                if (isAutologin != value)
                {
                    isAutologin = value;
                    OnPropertyChanged("IsAutologin");
                }
            }
        }

        private string server;
        public string Server
        {
            get { return server; }
            set
            {
                server = value;
                OnPropertyChanged("Server");
            }
        }

        private string port;
        public string Port
        {
            get { return port; }
            set
            {
                port = value;
                OnPropertyChanged("Port");
            }
        }

        #endregion

        #region Methods

        public static LoginConfig Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region INotifyPropertyChanged

        protected void OnPropertyChanged(string propertyName)
        {
            Common.Checks.CheckPropertyExists(this, propertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
