using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class InstituteAdmin : IDTOable<InstituteAdminDTO>
    {

        #region Constructors

        public InstituteAdmin()
        {
        }

        public InstituteAdmin(SystemUserDTO user)
            : base(user)
        {
            Assign(user as InstituteAdminDTO);
        }

        #endregion

        public new InstituteAdminDTO ToDTO()
        {
            InstituteAdminDTO admin = new InstituteAdminDTO
            {
                ID = this.ID,
                Login = this.Login,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO(),
                Role = this.Role,
                InstituteID = this.InstituteID
            };

            return admin;
        }

        public void Assign(InstituteAdminDTO entity)
        {
            base.Assign(entity);

            InstituteID = entity.InstituteID;
        }
    }
}
