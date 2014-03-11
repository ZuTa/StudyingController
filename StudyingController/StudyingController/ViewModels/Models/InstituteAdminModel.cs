using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class InstituteAdminModel : SystemUserModel, IDTOable<InstituteAdminDTO>
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

        public InstituteAdminModel(InstituteAdminDTO entity)
            : base(entity)
        {
            this.Institute = entity.Institute;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            InstituteAdminDTO instituteAdmin = entity as InstituteAdminDTO;
            this.Institute = instituteAdmin.Institute;
        }

        public InstituteAdminDTO ToDTO()
        {
            return new InstituteAdminDTO
            {
                ID = this.ID,
                Login = this.Login,
                Role = this.Role,
                Password = this.Password,
                InstituteID = Institute.ID,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Picture = this.Picture,
                Birth = this.Birth,
                Email = this.Email
            };
        }

        private bool IsInstituteValid(out string error)
        {
            error = null;
            if (institute == null)
            {
                error = Properties.Resources.ErrorStructureNotFound;
                return false;
            }
            return true;
        }

        protected override string Validate(string property)
        {
            string error = base.Validate(property);
            if (error == null)
            {
                switch (property)
                {
                    case "Institute":
                        IsInstituteValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }
    }
}
