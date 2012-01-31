using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class FacultyAdmin : IDTOable<FacultyAdminDTO>
    {

        #region Constructors

        public FacultyAdmin()
        {
        }

        public FacultyAdmin(SystemUserDTO user)
            : base(user)
        {
            Assign(user as FacultyAdminDTO);
        }

        #endregion

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

        public void Assign(FacultyAdminDTO entity)
        {
            base.Assign(entity);

            FacultyID = entity.FacultyID;
        }
    }
}
