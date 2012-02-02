using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using StudyingController.Common;
using System.Reflection;

namespace StudyingController.ClientData
{
    [Serializable]
    public class LoginConfig : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields & Properties

        private static readonly string DEFAULT_FOLDER_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), StudyingController.Properties.Resources.AppDataFolderName);

        public bool IsValid
        {
            get 
            {
                return ValidateProperties();
            }
        }

        private string login;
        [Validateable]
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
        [Validateable]
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
        [Validateable]
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
        [Validateable]
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
            if (login == null || login.Length == 0)
                errors.Add(Properties.Resources.ErrorPortEmpty);
            else
            {
                if (!Regex.IsMatch(login, "^\\w+$"))
                    errors.Add(Properties.Resources.ErrorLoginBadChars);
                if (login.Length < 3)
                    errors.Add(Properties.Resources.ErrorLoginLength);
            }
            return string.Join(Environment.NewLine, errors);
        }

        private string IsPasswordValid()
        {
            List<string> errors = new List<string>();
            if (password == null || password.Length < 4)
                errors.Add("");
            return string.Join(Environment.NewLine, errors);
        }

        private string IsServerValid()
        {
            List<string> errors = new List<string>();
            if (server == null || server.Length == 0)
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
            if (port == null || port.Length == 0)
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

        private bool ValidateProperties()
        {
            bool result = true;

            Type type = this.GetType();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                string error = Validate(propertyInfo.Name);
                if (propertyInfo.GetCustomAttributes(typeof(ValidateableAttribute), false).Length > 0 && error != null && error != string.Empty)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private string Validate(string property)
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
                default:
                    return null;
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
            get { return Validate(property); }
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
