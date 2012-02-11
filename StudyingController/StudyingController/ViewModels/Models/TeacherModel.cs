using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using System.Collections.ObjectModel;

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

        private CathedraDTO cathedra;
        public CathedraDTO Cathedra
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
                UserInformation = this.UserInformation.ToDTO(),
                CathedraID = this.Cathedra.ID
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
