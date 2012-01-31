using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Student : IDTOable<StudentDTO>
    {
        #region Constructors

        public Student()
        {
 
        }

        public Student(SystemUserDTO user)
            :base(user)
        {
            Assign(user as StudentDTO);
        }

        #endregion
        
        public new StudentDTO ToDTO()
        {
            return new StudentDTO
            {
                ID = this.ID,
                Login = this.Login,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO(),
                Role = this.Role,
                GroupID = this.GroupID
            };
        }

        public void Assign(StudentDTO entity)
        {
            base.Assign(entity);

            GroupID = entity.GroupID;
        }
    }
}
