using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Attachment : IDTOable<AttachmentDTO>, IDataBase
    {
        #region Constructors

        public Attachment()
        {
        }

        public Attachment(AttachmentDTO attachment)
        {
            Assign(attachment);
        }    

        #endregion

        #region Methods

        public AttachmentDTO ToDTO()
        {
            return new AttachmentDTO
            {
                ID = this.ID,
                Name = this.Name,
                TeacherID = this.TeacherID,
                Description = this.Description,
                DateAdded = this.DateAdded,
                Data=this.Data
            };
        }

        public void Assign(AttachmentDTO entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
            this.TeacherID = entity.TeacherID;
            this.Description = entity.Description;
            this.DateAdded = entity.DateAdded;
            this.Data = entity.Data;
        }

        #endregion
    }
}
