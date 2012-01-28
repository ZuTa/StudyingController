using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class UserInformation : IDTOable<UserInformationDTO>
    {
        #region Constructors

        public UserInformation()
        {
        }

        public UserInformation(UserInformationDTO info)
        {
            this.SystemUserID = info.ID;
            this.FirstName = info.FirstName;
            this.LastName = info.LastName;
            this.Email = info.Email;
        }

        #endregion

        public UserInformationDTO ToDTO()
        {
            return new UserInformationDTO
            {
                ID = this.SystemUserID,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
            };
        }


        public void UpdateData(UserInformationDTO entity)
        {
            SystemUserID = entity.ID;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
        }
    }
}
