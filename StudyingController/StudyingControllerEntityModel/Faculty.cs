using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Faculty : IDTOable<FacultyDTO>
    {
        public FacultyDTO ToDTO()
        {
            FacultyDTO faculty = new FacultyDTO
            {
                ID = this.ID,
                Name = this.Name,
                InstituteID = this.InstituteID
            };

            return faculty;
        }
    }
}
