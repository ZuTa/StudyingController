using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    class TeacherLecturesModel : BaseModel, IDTOable<TeacherDTO>
    {
        public TeacherLecturesModel(TeacherDTO teacher)
            :base(teacher)
        {
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);
        }

        public TeacherDTO ToDTO()
        {
            return new TeacherDTO() { ID = this.ID };
        }

        protected override string Validate(string property)
        {
            return base.Validate(property);
        }
    }
}
