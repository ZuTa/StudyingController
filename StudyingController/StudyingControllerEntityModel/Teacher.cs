using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Teacher : IDTOable<TeacherDTO>
    {
        #region Constructors

        public Teacher()
        {
        }

        public Teacher(SystemUserDTO user)
            : base(user)
        {
            Assign(user as TeacherDTO);
        }

        #endregion

        public new TeacherDTO ToDTO()
        {
            TeacherDTO teacher = new TeacherDTO
            {
                ID = this.ID,
                Login = this.Login,
                UserInformation = (this.UserInformation as IDTOable<UserInformationDTO>).ToDTO(),
                Role = this.Role,
                CathedraID = this.CathedraID,
                Lectures = this.Lectures.ToDTOList<LectureDTO, Lecture>()
            };
            if (this.Cathedra != null)
                teacher.Cathedra = this.Cathedra.ToDTO();
            return teacher;
        }

        public void Assign(TeacherDTO entity)
        {
            base.Assign(entity);

            CathedraID = entity.CathedraID;
        }
    }
}
