using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingControllerEntityModel
{
    public partial class Visiting : IDTOable<VisitingDTO>, IDataBase
    {
         #region Constructors

        public Visiting()
        {
        }

        public Visiting(VisitingDTO visiting)
        {
            Assign(visiting);
        }

        #endregion

        public VisitingDTO ToDTO()
        {
            return new VisitingDTO
            {
                ID = this.ID,
                Date = this.Date,
                Value = (VisitingValue)this.Value,
                Description = this.Description,
                Lecture = this.Lecture == null ? null : this.Lecture.ToDTO(),
                Practice = this.Practice == null ? null : this.Practice.ToDTO()
            };
        }

        public void Assign(VisitingDTO entity)
        {
            ID = entity.ID;
            Date = entity.Date;
            Value = (int)entity.Value;
            Description = entity.Description;
            Lecture = entity.Lecture == null ? null : new Lecture(entity.Lecture);
            Practice = entity.Practice == null ? null : new Practice(entity.Practice);
        }
    }
}
