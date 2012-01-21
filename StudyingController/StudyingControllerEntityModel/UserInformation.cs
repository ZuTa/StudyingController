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
            throw new NotImplementedException();
        }
    }
}
