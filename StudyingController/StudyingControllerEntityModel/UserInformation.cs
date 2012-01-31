using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class UserInformation : IDTOable<UserInformationDTO>, IDataBase
    {
        #region Constructors

        public UserInformation()
        {
        }

        public UserInformation(UserInformationDTO info)
        {
            Assign(info);
        }

        #endregion

        public UserInformationDTO ToDTO()
        {
            return new UserInformationDTO
            {
                ID = this.ID,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
            };
        }


        public void Assign(UserInformationDTO entity)
        {
            ID = entity.ID;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
        }
    }
}
