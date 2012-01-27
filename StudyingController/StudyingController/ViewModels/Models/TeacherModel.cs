using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class TeacherModel : SystemUserModel, IDTOable<TeacherDTO>
    {
        #region Fields & Properties

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
            this.Cathedra = teacher.Cathedra;
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
                Role = this.Role,
                UserInformation = this.UserInformation.ToDTO(),
                CathedraID = this.Cathedra.ID
            };
        }

        #endregion
    }
}
