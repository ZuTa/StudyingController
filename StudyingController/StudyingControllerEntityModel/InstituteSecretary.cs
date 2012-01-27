using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    partial class InstituteSecretary : IDTOable<InstituteSecretaryDTO>
    {
        #region Constructors

        public InstituteSecretary()
        {
        }

        public InstituteSecretary(SystemUserDTO user)
            : base(user)
        {
            InstituteSecretaryDTO secretary = user as InstituteSecretaryDTO;

            this.InstituteID = secretary.InstituteID;
        }

        #endregion

        public new InstituteSecretaryDTO ToDTO()
        {
            return new InstituteSecretaryDTO
            {
                ID = this.ID,
                Login = this.Login,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO(),
                Role = this.Role,
                InstituteID = this.InstituteID
            };
        }

        public void UpdateData(InstituteSecretaryDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
