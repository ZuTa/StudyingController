using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    partial class FacultySecretary : IDTOable<FacultySecretaryDTO>
    {
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

        public void UpdateData(FacultySecretaryDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
