using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class SystemUserModel : BaseModel
    {
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

        private UserRoles role;
        public UserRoles Role
        {
            get { return role; }
            set { role = value; }
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

            if (user.UserInformation != null)
            {
                this.firstName = user.UserInformation.FirstName;
                this.lastName = user.UserInformation.LastName;
                this.email = user.UserInformation.Email;
            }
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            SystemUserDTO userEntity = entity as SystemUserDTO;
            this.Login = userEntity.Login;
            
            if (userEntity.UserInformation == null) userEntity.UserInformation = new UserInformationDTO();
            this.FirstName = userEntity.UserInformation.FirstName;
            this.LastName = userEntity.UserInformation.LastName;
            this.Email = userEntity.UserInformation.Email;
        }
    }
}
