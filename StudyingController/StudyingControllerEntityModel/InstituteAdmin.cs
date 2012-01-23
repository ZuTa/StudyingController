﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class InstituteAdmin : IDTOable<InstituteAdminDTO>
    {
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
            throw new NotImplementedException();
        }
    }
}
