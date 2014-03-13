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
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Picture = this.Picture,
                Birth = this.Birth.HasValue ? this.Birth.Value: DateTime.MinValue,
                Email = this.Email,
            };

            return user;
        }

        public void Assign(SystemUserDTO entity)
        {
            ID = entity.ID;
            Role = entity.Role;
            Login = entity.Login;
            Password = Encoding.UTF8.GetBytes(entity.Password);
            FirstName = entity.FirstName;
            MiddleName = entity.MiddleName;
            LastName = entity.LastName;
            Picture = entity.Picture;
            Birth = entity.Birth;
            Email = entity.Email;
        }
    }
}
