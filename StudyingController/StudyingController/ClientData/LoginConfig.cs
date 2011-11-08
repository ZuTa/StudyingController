using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;

namespace StudyingController.ClientData
{
    [Serializable]
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
            LoginConfig instance = new LoginConfig();
            string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), StudyingController.Properties.Resources.LoginConfigFileName);

            if (File.Exists(configFilePath))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(LoginConfig));

                using (Stream fileStream = File.Open(configFilePath, FileMode.Open))
                {
                    if(fileStream.Length > 0) instance =  (LoginConfig)xmlSerializer.Deserialize(fileStream);
                }
            }
            return instance;
        }

        public void Save()
        {
            string appDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), StudyingController.Properties.Resources.AppDataFolderName);
            if (!Directory.Exists(appDataFolderPath))
                Directory.CreateDirectory(appDataFolderPath);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LoginConfig));

            using(Stream fileStream = new FileStream(Path.Combine(appDataFolderPath, StudyingController.Properties.Resources.LoginConfigFileName), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                if (IsMemorizeLogin) xmlSerializer.Serialize(fileStream, this);
            }
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
