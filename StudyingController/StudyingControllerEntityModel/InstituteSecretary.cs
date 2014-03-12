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
            Assign(user as InstituteSecretaryDTO);
        }

        #endregion

        public new InstituteSecretaryDTO ToDTO()
        {
            return new InstituteSecretaryDTO
            {
                ID = this.ID,
                Login = this.Login,
                Role = this.Role,
                InstituteID = this.InstituteID,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Picture = this.Picture,
                Birth = this.Birth.HasValue ? this.Birth.Value : DateTime.MinValue,
                Email = this.Email
            };
        }

        public void Assign(InstituteSecretaryDTO entity)
        {
            base.Assign(entity);

            InstituteID = entity.InstituteID;
        }
    }
}
