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
                Role = this.Role,
                UserInformation = this.UserInformation.ToDTO(),
                FacultyID = Faculty.ID
            };
        }
    }
}
