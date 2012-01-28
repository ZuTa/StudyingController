using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class FacultyAdminModel : SystemUserModel, IDTOable<FacultyAdminDTO>
    {
        private FacultyDTO faculty;
        public FacultyDTO Faculty
        {
            get { return faculty; }
            set
            {
                faculty = value;
                OnPropertyChanged("Faculty");
            }
        }

        public FacultyAdminModel(FacultyAdminDTO entity)
            : base(entity)
        {
            this.Faculty = entity.Faculty;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            FacultyAdminDTO facultyAdmin = entity as FacultyAdminDTO;
            this.Faculty = facultyAdmin.Faculty;
        }

        public FacultyAdminDTO ToDTO()
        {
            return new FacultyAdminDTO
            {
                ID = this.ID,
                Login = this.Login,
                Password = this.Password,
                Role = this.Role,
                UserInformation = this.UserInformation.ToDTO(),
                FacultyID = Faculty.ID
            };
        }

        private bool IsFacultyValid(out string error)
        {
            error = null;
            if (faculty == null)
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
                    case "Faculty":
                        IsFacultyValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }
    }
}
