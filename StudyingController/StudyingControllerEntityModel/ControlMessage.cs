using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class ControlMessage : IDTOable<ControlMessageDTO>
    {
        #region Constructors

        public ControlMessage()
        {
        }

        public ControlMessage(ControlMessageDTO entity)
        {
            Assign(entity);
        }

        #endregion

        #region Methods

        public ControlMessageDTO ToDTO()
        {
            return new ControlMessageDTO
            {
                ID = this.ID,
                Control = Control.ToDTO(),
                ControlID = this.ControlID,
                Date = this.Date,
                Owner = SystemUser.ToDTO(),
                Text = this.Text
            };
        }

        public void Assign(ControlMessageDTO entity)
        {
            this.ID = entity.ID;
            this.ControlID = entity.ControlID;
            this.Text = entity.Text;
            this.SystemUserID = entity.Owner.ID;
            this.Date = entity.Date;
        }

        #endregion
    }
}
