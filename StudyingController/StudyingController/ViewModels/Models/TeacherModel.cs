using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using System.Collections.ObjectModel;
using StudyingController.Common;

namespace StudyingController.ViewModels.Models
{
    public class TeacherModel : SystemUserModel, IDTOable<TeacherDTO>
    {
        #region Fields & Properties

        private List<LectureDTO> lectures;
        public List<LectureDTO> Lectures
        {
            get { return lectures; }
            set { lectures = value; }
        }

        private CathedraRef cathedra;
        [Validateable]
        public CathedraRef Cathedra
        {
            get { return cathedra; }
            set
            {
                cathedra = value;
                OnPropertyChanged("Cathedra");
            }
        }

        #endregion

        #region Constructors

        public TeacherModel(TeacherDTO teacher)
            : base(teacher)
        {
            Assign(teacher);
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            TeacherDTO teacher = entity as TeacherDTO;
            this.Cathedra = teacher.Cathedra;
        }

        public TeacherDTO ToDTO()
        {
            return new TeacherDTO
            {
                ID = this.ID,
                Login = this.Login,
                Password = this.Password,
                Role = this.Role,
                Cathedra = this.Cathedra,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Picture = this.Picture,
                Birth = this.Birth,
                Email = this.Email
            };
        }

        private bool IsCathedraValid(out string error)
        {
            error = null;
            if (cathedra == null)
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
                    case "Cathedra":
                        IsCathedraValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }

        #endregion
    }
}
