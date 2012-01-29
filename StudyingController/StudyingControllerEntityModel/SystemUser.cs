using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class SystemUser : IDTOable<SystemUserDTO>, IDataBase
    {
        [DataMember]
        public UserRoles Role
        {
            get
            {
                return (UserRoles)_iUserRole;
            }
            set
            {
                _iUserRole = (int)value;
            }
        }

        #region Constructors

        public SystemUser()
        {
        }

        public SystemUser(SystemUserDTO user)
        {
            Assign(user);
        }        

        #endregion

        public SystemUserDTO ToDTO()
        {
            SystemUserDTO user = new SystemUserDTO
            {
                ID = this.ID,
                Login = this.Login,
                Password = Encoding.UTF8.GetString(this.Password),
                Role = this.Role,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO()
            };

            return user;
        }

        public void Assign(SystemUserDTO entity)
        {
            ID = entity.ID;
            Role = entity.Role;
            Login = entity.Login;
            Password = Encoding.UTF8.GetBytes(entity.Password);
            UserInformation.Assign(entity.UserInformation);
        }
    }
}
