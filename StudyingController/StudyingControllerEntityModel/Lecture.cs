using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Lecture : IDTOable<LectureDTO>
    {
        #region Constructors

        public Lecture()
        {
        }

        public Lecture(LectureDTO lecture)
        {
            Assign(lecture);
        }    

        #endregion

        #region Methods

        public LectureDTO ToDTO()
        {
            return new LectureDTO
            {
                ID = this.ID,
                Subject = this.Subject.ToDTO(),
                TeacherID = this.TeacherID,
                Groups = this.Groups.ToDTOList<GroupDTO, Group>()
            };
        }

        public void Assign(LectureDTO entity)
        {
            this.ID = entity.ID;
            this.SubjectID = entity.Subject.ID;
            this.TeacherID = entity.TeacherID;
        }

        #endregion
    }
}
