using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Group : IDTOable<GroupDTO>
    {
        #region Constructors

        public Group()
        {
        }

        public Group(GroupDTO group)
        {
            this.ID = group.ID;
            UpdateData(group);
        }

        #endregion

        #region Methods

        public GroupDTO ToDTO()
        {
            return new GroupDTO
            {
                ID = this.ID,
                CathedraID = this.CathedraID,
                Name = this.Name
            };
        }

        public void UpdateData(GroupDTO entity)
        {
            this.Name = entity.Name;
            this.CathedraID = entity.CathedraID;
        }

        #endregion
    }
}
