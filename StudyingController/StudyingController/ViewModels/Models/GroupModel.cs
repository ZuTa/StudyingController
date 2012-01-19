using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class GroupModel : NamedModel
    {
        #region Fields & Properties

        private CathedraDTO cathedra;
        public CathedraDTO Cathedra
        {
            get { return cathedra; }
            set { cathedra = value; }
        }

        #endregion

        #region Constructors

        public GroupModel(GroupDTO group)
            : base(group)
        {
            cathedra = group.Cathedra;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            GroupDTO group = entity as GroupDTO;
            this.Cathedra = group.Cathedra;
        }

        public GroupDTO ToDTO()
        {
            return new GroupDTO
            {
                ID = this.ID,
                Name = this.Name,
                CathedraID = cathedra.ID
            };
        }
        #endregion
    }
}
