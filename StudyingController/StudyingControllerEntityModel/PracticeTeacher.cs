using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

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
                PracticeID = this.PracticeID,
                TeacherID = this.TeacherID,
                Practice = this.Practice.ToDTO(),
                Students = this.Students.ToDTOList<StudentDTO,Student>()
            };
        }

        public void Assign(PracticeTeacherDTO entity)
        {
            this.ID = entity.ID;
            this.PracticeID = entity.PracticeID;
            this.TeacherID = entity.TeacherID;
        }

        #endregion
    }
}
