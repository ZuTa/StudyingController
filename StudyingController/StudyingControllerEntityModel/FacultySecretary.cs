using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    partial class FacultySecretary : IDTOable<FacultySecretaryDTO>
    {
        #region Constructors

        public FacultySecretary()
        {
        }

        public FacultySecretary(SystemUserDTO user)
            : base(user)
        {
            Assign(user as FacultySecretaryDTO);
        }

        #endregion

        public new FacultySecretaryDTO ToDTO()
        {
            return new FacultySecretaryDTO
            {
                ID = this.ID,
                Login = this.Login,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO(),
                Role = this.Role,
                FacultyID = this.FacultyID
            };
        }

        public void Assign(FacultySecretaryDTO entity)
        {
            base.Assign(entity);

            FacultyID = entity.FacultyID;
        }
    }
}
