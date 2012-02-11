using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Practice:IDTOable<PracticeDTO>
    {
         #region Constructors

        public Practice()
        {
        }

        public Practice(PracticeDTO practice)
        {
            Assign(practice);
        }    

        #endregion

        #region Methods

        public PracticeDTO ToDTO()
        {
            return new PracticeDTO
            {
                ID = this.ID,
                SubjectID = this.Subject.ID,
                Subject = this.Subject.ToDTO()
            };
        }

        public void Assign(PracticeDTO entity)
        {
            this.ID = entity.ID;
            this.SubjectID = entity.SubjectID;
        }

        #endregion
    }
}
