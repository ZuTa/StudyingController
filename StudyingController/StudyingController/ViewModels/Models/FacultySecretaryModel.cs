using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class FacultySecretaryModel : SystemUserModel, IDTOable<FacultySecretaryDTO>
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

        public FacultySecretaryModel(FacultySecretaryDTO entity)
            : base(entity)
        {
            this.Faculty = entity.Faculty;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            FacultySecretaryDTO facultySecretary = entity as FacultySecretaryDTO;
            this.Faculty = facultySecretary.Faculty;
        }

        public FacultySecretaryDTO ToDTO()
        {
            return new FacultySecretaryDTO
            {
                ID = this.ID,
                Login = this.Login,
                Password = this.Password,
                Role = this.Role,
                FacultyID = Faculty.ID,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Picture = this.Picture,
                Birth = this.Birth,
                Email = this.Email
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
