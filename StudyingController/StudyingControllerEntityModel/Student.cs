using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    partial class Student : IDTOable<StudentDTO>
    {
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

        public void UpdateData(StudentDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
