using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace StudyingController.ClientData
{
    [Serializable]
    public class LoginConfig : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields & Properties

        private static readonly string DEFAULT_FOLDER_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), StudyingController.Properties.Resources.AppDataFolderName); 

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
            string configFilePath = Path.Combine(DEFAULT_FOLDER_PATH, StudyingController.Properties.Resources.LoginConfigFileName);

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

        private string IsLoginValid()
        {
            List<string> errors = new List<string>();
            if (login.Length == 0)
                errors.Add(Properties.Resources.ErrorPortEmpty);
            else
            {
                if (!Regex.IsMatch(login, "^\\w+$"))
                    errors.Add(Properties.Resources.ErrorLoginBadChars);
                if (login.Length < 4)
                    errors.Add(Properties.Resources.ErrorLoginLength);
            }
            return string.Join(Environment.NewLine, errors);
        }

        private string IsPasswordValid()
        {
            List<string> errors = new List<string>();
            if (password.Length < 4)
                errors.Add("");
            return string.Join(Environment.NewLine, errors);
        }

        private string IsServerValid()
        {
            List<string> errors = new List<string>();
            if (server.Length == 0)
                errors.Add(Properties.Resources.ErrorServerEmpty);
            else
            {
                if (!Regex.IsMatch(server, "^[:a-zA-Z0-9/.-]+$"))
                    errors.Add(Properties.Resources.ErrorServerBadChars);
            }
            return string.Join(Environment.NewLine, errors);
        }

        private string IsPortValid()
        {
            List<string> errors = new List<string>();
            int currPort;
            if (port.Length == 0)
                errors.Add(Properties.Resources.ErrorPortEmpty);
            else
            {
                if (int.TryParse(port, out currPort))
                {
                    if (currPort > 65535 || currPort < 0)
                        errors.Add(Properties.Resources.ErrorPortBorder);
                }
                else
                    errors.Add(Properties.Resources.ErrorPortNotInt);
            }
            return string.Join(Environment.NewLine, errors);
        }

        public void Save()
        {
            string appDataFolderPath = DEFAULT_FOLDER_PATH;
            if (!Directory.Exists(appDataFolderPath))
                Directory.CreateDirectory(appDataFolderPath);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LoginConfig));

            using(Stream fileStream = new FileStream(Path.Combine(appDataFolderPath, StudyingController.Properties.Resources.LoginConfigFileName), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlSerializer.Serialize(fileStream, this);
            }
        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get { return null; }
        }

        public string this[string property]
        {
            get
            {
                switch (property)
                {
                    case "Login":
                        return IsLoginValid();
                    case "Password":
                        return IsPasswordValid();
                    case "Server":
                        return IsServerValid();
                    case "Port":
                        return IsPortValid();
                }
                return null;
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
