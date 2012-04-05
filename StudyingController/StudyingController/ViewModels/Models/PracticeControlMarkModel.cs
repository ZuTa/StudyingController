using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class PracticeControlMarkModel : MarkModel, IDTOable<PracticeControlMarkDTO>
    {
        #region Fields & Properties

        private int practiceControlID;
        public int PracticeControlID
        {
            get { return practiceControlID; }
            set { practiceControlID = value; }
        }

        #endregion

        #region Constructors

        public PracticeControlMarkModel(PracticeControlMarkDTO practiceControlMark)
            : base(practiceControlMark)
        {
            this.Assign(practiceControlMark); 
        }

        public PracticeControlMarkModel()
            : base()
        {
        }

        #endregion

        public PracticeControlMarkDTO ToDTO()
        {
            PracticeControlMarkDTO mark = new PracticeControlMarkDTO()
            {
                ID = this.ID,
                MarkValue = this.MarkValue,
                PracticeControlID = this.PracticeControlID,
                StudentID = this.StudentID
            };
            return mark;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            PracticeControlMarkDTO mark = (entity as PracticeControlMarkDTO);

            practiceControlID = mark.PracticeControlID;
        }
    }
}
