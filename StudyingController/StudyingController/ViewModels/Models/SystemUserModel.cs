using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using System.Text.RegularExpressions;
using StudyingController.Common;

namespace StudyingController.ViewModels.Models
{
    public class SystemUserModel : BaseModel, IDTOable<SystemUserDTO>
    {
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
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private UserRoles role;
        public UserRoles Role
        {
            get { return role; }
            set { role = value; }
        }

        private UserInformationModel userInformation;
        [Validateable]
        public UserInformationModel UserInformation
        {
            get { return userInformation; }
            set 
            { 
                userInformation = value;
            }
        }

        public SystemUserModel(SystemUserDTO user)
            : base(user)
        {
            this.login = user.Login;
            this.role = user.Role;
            this.password = user.Password;
            this.userInformation = new UserInformationModel(user.UserInformation);

            userInformation.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(userInformation_PropertyChanged);
        }

        void userInformation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("UserInformation");
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            SystemUserDTO user = entity as SystemUserDTO;
            this.Login = user.Login;
            this.Role = user.Role;
            this.Password = user.Password;
            this.UserInformation.Assign(user.UserInformation);
        }

        public SystemUserDTO ToDTO()
        {
            return new SystemUserDTO
            {
                ID = this.ID,
                Login = this.Login,
                Password = this.Password,
                Role = this.Role,
                UserInformation = this.UserInformation.ToDTO()
            };
        }

        private bool IsLoginValid(out string error)
        {
            error = null;
            if (login == null || login.Length < 3)
            {
                error = Properties.Resources.ErrorFieldEmpty;
                return false;
            }
            if (!Regex.IsMatch(login, "^[a-zA-Z0-9_\\-]+$"))
            {
                error = Properties.Resources.ErrorBadCharsUsed;
                return false;
            }
            return true;
        }

        protected override string Validate(string property)
        {
            string error = base.Validate(property);
            if (error == null)
            {
                switch (property)
                {
                    case "Login":
                        IsLoginValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }
    }
}
