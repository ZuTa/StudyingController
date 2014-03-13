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
                Role = this.Role,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Picture = this.Picture,
                Birth = this.Birth.HasValue ? this.Birth.Value : DateTime.MinValue,
                Email = this.Email,
                Cathedra = new CathedraRef { ID = this.CathedraID },
                Lectures = this.Lectures.Select(l => new LectureRef { ID = l.ID, Subject = new SubjectRef { ID = l.Subject.ID, Name = l.Subject.Name } }).ToList() //ToDTOList<LectureDTO, Lecture>()
            };
            if (this.Cathedra != null)
                teacher.Cathedra = new CathedraRef { ID = Cathedra.ID, Name = Cathedra.Name };
            return teacher;
        }

        public void Assign(TeacherDTO entity)
        {
            base.Assign(entity);

            CathedraID = entity.Cathedra.ID;
        }
    }
}
