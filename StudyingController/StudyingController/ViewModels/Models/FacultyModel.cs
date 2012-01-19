using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class FacultyModel : NamedModel
    {
        #region Fields & Properties

        private InstituteDTO institute;
        public InstituteDTO Institute
        {
            get { return institute; }
            set 
            { 
                institute = value;
                OnPropertyChanged("Institute");
            }
        }

        private int? instituteID;
        public int? InstituteID
        {
            get { return instituteID; }
            set { instituteID = value; }
        }

        #endregion

        public FacultyModel(FacultyDTO faculty)
            : base(faculty)
        {
            instituteID = faculty.InstituteID;            
        }

        public virtual void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            FacultyDTO faculty = entity as FacultyDTO;
        }

        public FacultyDTO ToDTO()
        {
            return new FacultyDTO
            {
                ID = this.ID,
                Name = this.Name,
                InstituteID = this.InstituteID
            };
        }
    }
}
