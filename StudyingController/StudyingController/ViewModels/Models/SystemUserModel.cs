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

        public string Name
        {
            get { return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName); }
        }

        public string ShortName
        {
            get
            {
                return LastName
                + (!string.IsNullOrEmpty(FirstName) ? FirstName[0] + "." : "")
                + (!string.IsNullOrEmpty(MiddleName) ? MiddleName[0] + "." : "");
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                OnPropertyChanged("MiddleName");
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private byte[] picture;
        public byte[] Picture
        {
            get { return picture; }
            set
            {
                picture = value;
                OnPropertyChanged("Picture");
            }
        }

        private DateTime birth;
        public DateTime Birth
        {
            get { return birth; }
            set 
            { 
                birth = value;
                OnPropertyChanged("Birth");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public SystemUserModel(SystemUserDTO user)
            : base(user)
        {
            this.login = user.Login;
            this.role = user.Role;
            this.password = user.Password;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            SystemUserDTO user = entity as SystemUserDTO;
            this.Login = user.Login;
            this.Role = user.Role;
            this.Password = user.Password;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Picture = Picture;
            this.Birth = Birth;
            this.Email = Email;
        }

        public SystemUserDTO ToDTO()
        {
            return new SystemUserDTO
            {
                ID = this.ID,
                Login = this.Login,
                Password = this.Password,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Picture = this.Picture,
                Birth = this.Birth,
                Email = this.Email
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
