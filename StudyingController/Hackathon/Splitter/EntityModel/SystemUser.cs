using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelDTO;

namespace EntityModel
{
    public partial class SystemUser : IDTOable<SystemUserDTO>
    {
        #region Constructors

        public SystemUser()
        {
        }

        public SystemUser(SystemUserDTO user)
        {
            Assign(user);
        }

        #endregion

        #region Methods

        public SystemUserDTO ToDTO()
        {
            return new SystemUserDTO
                {
                    ID = this.ID,
                    Login = this.Login,
                    Password = Encoding.UTF8.GetString(this.Password)
                };
        }

        public void Assign(SystemUserDTO user)
        {
            this.ID = user.ID;
            this.Login = user.Login;
            this.Password = Encoding.UTF8.GetBytes(user.Password);
        }

        #endregion
    }
}
