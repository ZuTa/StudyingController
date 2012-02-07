using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    class LectureModel : BaseModel, IDTOable<LectureDTO>
    {
        public LectureModel(LectureDTO lecture)
            :base(lecture)
        {
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);
        }

        public LectureDTO ToDTO()
        {
            return new LectureDTO() { ID = this.ID };
        }

        protected override string Validate(string property)
        {
            return base.Validate(property);
        }
    }
}
