using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Group : IDTOable<GroupDTO>, IDataBase
    {
        #region Constructors

        public Group()
        {
        }

        public Group(GroupDTO group)
        {
            Assign(group);
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

        public void Assign(GroupDTO entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
            this.CathedraID = entity.CathedraID;
        }

        #endregion
    }
}
