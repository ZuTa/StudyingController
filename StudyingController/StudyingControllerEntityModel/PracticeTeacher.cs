using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using Common;

namespace StudyingControllerEntityModel
{
    public partial class PracticeTeacher : IDTOable<PracticeTeacherDTO>
    {
         #region Constructors

        public PracticeTeacher()
        {
        }

        public PracticeTeacher(PracticeTeacherDTO prTeacher)
        {
            Assign(prTeacher);
        }    

        #endregion

        #region Methods

        public PracticeTeacherDTO ToDTO()
        {
            return new PracticeTeacherDTO
            {
                ID = this.ID,
                Practice = this.Practice.CopyTo(()=>new PracticeRef()),
                Teacher = this.Teacher.CopyTo(()=>new TeacherRef()),
                Students = this.Students.ToDTOList<StudentDTO, Student>()
            };
        }

        public void Assign(PracticeTeacherDTO entity)
        {
            this.ID = entity.ID;
            this.PracticeID = entity.Practice.ID;
            this.TeacherID = entity.Teacher.ID;
            this.Students.Update<Student, StudentDTO>(entity.Students);
        }

        #endregion
    }
}
