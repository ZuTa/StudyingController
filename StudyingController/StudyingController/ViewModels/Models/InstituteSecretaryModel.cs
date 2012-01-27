using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class InstituteSecretaryModel : SystemUserModel, IDTOable<InstituteSecretaryDTO>
    {
        private InstituteDTO institute;
        public InstituteDTO Institute
        {
            get { return institute; }
            set 
            { 
                institute = value;
                OnPropertyChanged("Institute");
            }
        }

        public InstituteSecretaryModel(InstituteSecretaryDTO entity)
            : base(entity)
        {
            this.Institute = entity.Institute;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            InstituteSecretaryDTO InstituteSecretary = entity as InstituteSecretaryDTO;
            this.Institute = InstituteSecretary.Institute;
        }

        public InstituteSecretaryDTO ToDTO()
        {
            return new InstituteSecretaryDTO
            {
                ID = this.ID,
                Login = this.Login,
                Role = this.Role,
                UserInformation = this.UserInformation.ToDTO(),
                InstituteID = Institute.ID
            };
        }
    }
}
