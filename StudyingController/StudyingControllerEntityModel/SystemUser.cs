﻿using System;
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

        public SystemUserDTO ToDTO()
        {
            SystemUserDTO user = new SystemUserDTO
            {
                ID = this.ID,
                Login = this.Login,
                Role = this.Role,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO()
            };

            return user;
        }



        public void UpdateData(SystemUserDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
