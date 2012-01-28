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
            InstituteAdminDTO admin = user as InstituteAdminDTO;

            this.InstituteID = admin.InstituteID;
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

        public void UpdateData(InstituteAdminDTO entity)
        {
            base.UpdateData(entity);

            InstituteID = entity.InstituteID;
        }
    }
}
