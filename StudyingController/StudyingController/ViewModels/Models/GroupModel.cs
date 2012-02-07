using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;

namespace StudyingController.ViewModels.Models
{
    public class GroupModel : NamedModel
    {
        #region Fields & Properties

        private SpecializationDTO specialization;
        [Validateable]
        public SpecializationDTO Specialization
        {
            get { return specialization; }
            set 
            { 
                specialization = value;
                OnPropertyChanged("Specialization");
            }
        }

        private CathedraDTO cathedra;
        [Validateable]
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

        public GroupModel(GroupDTO group)
            : base(group)
        {
            this.cathedra = group.Cathedra;
            this.specialization = group.Specialization;
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            GroupDTO group = entity as GroupDTO;
            this.Cathedra = group.Cathedra;
            this.Specialization = group.Specialization;
        }

        public GroupDTO ToDTO()
        {
            return new GroupDTO
            {
                ID = this.ID,
                Name = this.Name,
                CathedraID = this.cathedra.ID,
                SpecializationID = this.specialization.ID
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

        private bool IsSpecializationValid(out string error)
        {
            error = null;
            if (specialization == null)
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
                    case "Specialization":
                        IsSpecializationValid(out error);
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
