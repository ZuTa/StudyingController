using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class SystemUserModel : BaseModel, IDTOable<SystemUserDTO>
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

        private UserInformationModel userInformation;
        public UserInformationModel UserInformation
        {
            get { return userInformation; }
            set { userInformation = value; }
        }

        public SystemUserModel(SystemUserDTO user)
            : base(user)
        {
            this.login = user.Login;
            this.role = user.Role;
            this.userInformation = new UserInformationModel(user.UserInformation);
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            SystemUserDTO user = entity as SystemUserDTO;
            this.Login = user.Login;
            this.UserInformation.Assign(user.UserInformation);
        }

        public SystemUserDTO ToDTO()
        {
            return new SystemUserDTO
            {
                ID = this.ID,
                Login = this.Login,
                Role = this.Role,
                UserInformation = this.UserInformation.ToDTO()
            };
        }
    }
}
