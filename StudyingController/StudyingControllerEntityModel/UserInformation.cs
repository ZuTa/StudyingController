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
        public UserInformationDTO ToDTO()
        {
            UserInformationDTO info = new UserInformationDTO
            {
                ID = this.SystemUserID,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
            };

            return info;
        }
    }
}
