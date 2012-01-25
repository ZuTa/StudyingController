using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    partial class Teacher : IDTOable<TeacherDTO>
    {
        public new TeacherDTO ToDTO()
        {
            return new TeacherDTO
            {
                ID = this.ID,
                Login = this.Login,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO(),
                Role = this.Role,
                CathedraID = this.CathedraID
            };
        }

        public void UpdateData(TeacherDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
