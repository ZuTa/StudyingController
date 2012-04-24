using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class LectureControlMarkModel : MarkModel, IDTOable<LectureControlMarkDTO>
    {
        #region Fields & Properties

        private int lectureControlID;
        public int LectureControlID
        {
            get { return lectureControlID; }
            set { lectureControlID = value; }
        }

        #endregion

        #region Contructor

        public LectureControlMarkModel(LectureControlMarkDTO lectureControlMark)
            : base(lectureControlMark)
        {
            this.Assign(lectureControlMark); 
        }

        public LectureControlMarkModel()
            : base()
        {
        }

        #endregion

        public LectureControlMarkDTO ToDTO()
        {
            LectureControlMarkDTO mark = new LectureControlMarkDTO()
            {
                ID = this.ID,
                LectureControlID = this.LectureControlID,
                MarkValue = this.MarkValue,
                StudentID = this.StudentID,
                Description = this.Description
            };
            return mark;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            LectureControlMarkDTO mark = (entity as LectureControlMarkDTO);

            lectureControlID = mark.LectureControlID;
        }

    }
}
