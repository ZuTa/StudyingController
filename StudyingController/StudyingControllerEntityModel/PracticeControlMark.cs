using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class PracticeControlMark : Mark,IDTOable<PracticeControlMarkDTO>
    {
         public PracticeControlMark()
            : base()
        {

        }

        public PracticeControlMark(PracticeControlMarkDTO mark):
            base(mark)
        {
            Assign(mark);
        }

        public PracticeControlMarkDTO ToDTO()
        {
            PracticeControlMarkDTO mark = new PracticeControlMarkDTO()
            {
                ID = this.ID,
                MarkValue = this.MarkValue,
                PracticeControlID = this.PracticeControlID,
                StudentID = this.StudentID,
                Student = this.Student.ToDTO()
            };
            return mark;
        }

        public void Assign(PracticeControlMarkDTO entity)
        {
            base.Assign(entity);

            PracticeControlID = entity.PracticeControlID;
        }
    }
}
