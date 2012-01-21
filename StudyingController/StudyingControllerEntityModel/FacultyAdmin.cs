using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class FacultyAdmin : IDTOable<FacultyAdminDTO>
    {
        public new FacultyAdminDTO ToDTO()
        {
            return new FacultyAdminDTO
            {
                ID = this.ID,
                Login = this.Login,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO(),
                Role = this.Role,
                FacultyID = this.FacultyID
            };
        }

        public void UpdateData(FacultyAdminDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
