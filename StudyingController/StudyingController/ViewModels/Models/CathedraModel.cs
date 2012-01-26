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
        [Validateable]
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
            this.Faculty = cathedra.Faculty;
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

        private bool IsFacultyValid(out string error)
        {
            error = null;
            if (faculty == null)
            {
                error = Properties.Resources.ErrorStructureNotFound;
                return false;
            }
            return true;
        }

        protected override string Validate(string property)
        {
            string error = base.Validate(property);
            if (error == null)
            {
                switch (property)
                {
                    case "Faculty":
                        IsFacultyValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }

    }
}
