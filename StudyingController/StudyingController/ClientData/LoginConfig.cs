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

        private const string LOGIN_ERROR_LENGTH = "Логін повинен мати більше 4 символів";
        private const string LOGIN_ERROR_SYMBOLS = "Логін повинен складатись лише з символів a-z(A-Z)0-9_";
        private const string LOGIN_ERROR_EMPTY = "Поле логін не може бути порожнім";
        private const string PASSWORD_ERROR_LENGTH = "";
        private const string PORT_ERROR_INT = "Порт повинен бути цілим числом";
        private const string PORT_ERROR_BORDER = "Порт за межами (0-65535)";
        private const string PORT_ERROR_EMPTY = "Поле порт не може бути порожнім";
        private const string SERVER_ERROR_NAME = "Використано неприпустимі символи для сервера";
        private const string SERVER_ERROR_EMPTY = "Поле сервер не може бути порожнім";

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
                errors.Add(LOGIN_ERROR_EMPTY);
            else
            {
                if (!Regex.IsMatch(login, "^\\w+$"))
                    errors.Add(LOGIN_ERROR_SYMBOLS);
                if (login.Length < 4)
                    errors.Add(LOGIN_ERROR_LENGTH);
            }
            return string.Join(Environment.NewLine, errors);
        }

        private string IsPasswordValid()
        {
            List<string> errors = new List<string>();
            if (password.Length < 4)
                errors.Add(PASSWORD_ERROR_LENGTH);
            return string.Join(Environment.NewLine, errors);
        }

        private string IsServerValid()
        {
            List<string> errors = new List<string>();
            if (server.Length == 0)
                errors.Add(SERVER_ERROR_EMPTY);
            else
            {
                if (!Regex.IsMatch(server, "^[:a-zA-Z0-9/.-]+$"))
                    errors.Add(SERVER_ERROR_NAME);
            }
            return string.Join(Environment.NewLine, errors);
        }

        private string IsPortValid()
        {
            List<string> errors = new List<string>();
            int currPort;
            if (port.Length == 0)
                errors.Add(PORT_ERROR_EMPTY);
            else
            {
                if (int.TryParse(port, out currPort))
                {
                    if (currPort > 65535 || currPort < 0)
                        errors.Add(PORT_ERROR_BORDER);
                }
                else
                    errors.Add(PORT_ERROR_INT);
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
