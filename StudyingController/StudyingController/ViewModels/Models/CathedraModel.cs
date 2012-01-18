using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class CathedraModel : NamedModel, IDTOable<CathedraDTO>
    {
        #region Fields & Properties

        private FacultyDTO faculty;
        public FacultyDTO Faculty
        {
            get { return faculty; }
            set 
            { 
                faculty = value;
                OnPropertyChanged("Faculty");
            }
        }

        private int? facultyID;
        public int? FacultyID
        {
            get { return facultyID; }
            set { facultyID = value; }
        }

        #endregion

        public CathedraModel(CathedraDTO cathedra)
            : base(cathedra)
        {
            facultyID = cathedra.FacultyID;
        }

        public CathedraDTO ToDTO()
        {
            return new CathedraDTO
            {
                ID = this.ID,
                Name = this.Name,
            };
        }
    }
}
