using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class FacultyModel : NamedModel, IDTOable<FacultyDTO>
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
