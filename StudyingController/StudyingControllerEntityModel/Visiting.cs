using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

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
                Student = new StudentRef{ ID = StudentID },
                Lecture = this.Lecture.WithClass(l => l.ToRef()),
                Practice = this.Practice.WithClass(p => p.ToRef())
            };
        }

        public void Assign(VisitingDTO entity)
        {
            ID = entity.ID;
            Date = entity.Date;
            Value = (int)entity.Value;
            Description = entity.Description;
            StudentID = entity.Student.WithStruct(s=>s.ID);
            Lecture = entity.Lecture.WithClass(l => new Lecture
                {
                    ID = l.ID
                });
            Practice = entity.Practice.WithClass(p=> new Practice
            {
                ID = p.ID
            });
        }
    }
}
