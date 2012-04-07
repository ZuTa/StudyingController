using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class StudyRange : IDTOable<StudyRangeDTO>
    {

        public StudyRange()
        {
        }

        public StudyRange(StudyRangeDTO studyRange)
        {
            Assign(studyRange);
        }

        public StudyRangeDTO ToDTO()
        {
            return new StudyRangeDTO
                {
                    ID = this.ID,
                    Year = this.Year,
                    YearPart = this.Part
                };
        }

        public void Assign(StudyRangeDTO studyRange)
        {
            this.ID = studyRange.ID;
            this.Year = studyRange.Year;
            this.Part = studyRange.YearPart;
        }
    }
}
