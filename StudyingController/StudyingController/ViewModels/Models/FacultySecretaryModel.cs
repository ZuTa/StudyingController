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
                Role = this.Role,
                UserInformation = this.UserInformation.ToDTO(),
                FacultyID = Faculty.ID
            };
        }
    }
}
