using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class LectureControlMark : Mark, IDTOable<LectureControlMarkDTO>
    {
        public LectureControlMark()
            : base()
        {

        }

        public LectureControlMark(LectureControlMarkDTO mark):
            base(mark)
        {
            Assign(mark);
        }

        public LectureControlMarkDTO ToDTO()
        {
            LectureControlMarkDTO mark = new LectureControlMarkDTO()
            {
                ID = this.ID,
                LectureControlID = this.LectureControlID,
                MarkValue = this.MarkValue,
                StudentID = this.StudentID,
                Student = this.Student.ToDTO(),
                Description = this.Description
            };
            return mark;
        }

        public void Assign(LectureControlMarkDTO entity)
        {
            base.Assign(entity);

            this.LectureControlID = entity.LectureControlID;
        }
    }
}
