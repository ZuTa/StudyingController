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
                Name = this.Name,
                SpecializationID = this.SpecializationID,
                StudyRangeID = this.StudyRangeID
            };
        }

        public void Assign(GroupDTO entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
            this.CathedraID = entity.CathedraID;
            this.SpecializationID = entity.SpecializationID;
            this.StudyRangeID = entity.StudyRangeID;
        }

        #endregion
    }
}
