using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class AttachmentModel : NamedModel
    {
        #region Fields & Properties

        private string size;
        public string Size
        {
            get { return size; }
            set 
            {
                if (size != value)
                {
                    size = value;
                    OnPropertyChanged("Size");
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set 
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                    OnModelChanged();
                }
            }
        }

        private int teacherID;
        public int TeacherID
        {
            get { return teacherID; }
            set { teacherID = value; }
        }

        private byte[] data;
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        private DateTime dateAdded;
        public DateTime DateAdded
        {
            get { return dateAdded; }
            set { dateAdded = value; }
        }

        #endregion

        #region Constructors

        public AttachmentModel(AttachmentDTO attachment)
            :base(attachment)
        {
            Assign(attachment);
        }

        #endregion

        #region Methods 

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            AttachmentDTO attachment = entity as AttachmentDTO;
            this.description = attachment.Description;
            this.teacherID = attachment.TeacherID;
            this.data = attachment.Data;
            this.dateAdded = attachment.DateAdded;
            this.size = attachment.Size;
        }

        public AttachmentDTO ToDTO()
        {
            return new AttachmentDTO
            {
                ID = this.ID,
                TeacherID = this.TeacherID,
                Data = this.Data,
                DateAdded = this.DateAdded,
                Description = this.Description,
                Name = this.Name
            };
        }

        #endregion

        #region Callbacks
        #endregion

        #region Event

        

        #endregion
    }
}
