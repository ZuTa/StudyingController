using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class SystemUser : IDTOable<SystemUserDTO>
    {
        [DataMember]
        public UserRoles UserRole
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

        public SystemUserDTO ToDTO()
        {
            SystemUserDTO user = new SystemUserDTO
            {
                ID = this.ID,
                Login = this.Login,
                UserRole = this.UserRole,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO()
            };

            return user;
        }

    }
}
