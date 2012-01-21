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

        #endregion

        public CathedraModel(CathedraDTO cathedra)
            : base(cathedra)
        {
            this.faculty = cathedra.Faculty;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            CathedraDTO cathedra = entity as CathedraDTO;
            this.faculty = cathedra.Faculty;
        }

        public CathedraDTO ToDTO()
        {
            if (faculty == null)
                throw new Exception("Need UI validation!");

            return new CathedraDTO
            {
                ID = this.ID,
                Name = this.Name,
                FacultyID = Faculty.ID
            };
        }
    }
}
