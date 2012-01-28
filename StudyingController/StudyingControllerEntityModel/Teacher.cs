using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    partial class Teacher : IDTOable<TeacherDTO>
    {
        #region Constructors

        public Teacher()
        {
        }

        public Teacher(SystemUserDTO user)
            : base(user)
        {
            TeacherDTO teacher = user as TeacherDTO;

            this.CathedraID = teacher.CathedraID;
        }

        #endregion

        public TeacherDTO ToDTO()
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
            base.UpdateData(entity);

            CathedraID = entity.CathedraID;
        }
    }
}
