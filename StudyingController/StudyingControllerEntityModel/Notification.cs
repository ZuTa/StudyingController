using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingControllerEntityModel
{
    partial class Notification : IDTOable<NotificationDTO>, IDataBase
    {
        #region Constructors

        public Notification()
        {
        }

        public Notification(NotificationDTO notification)
        {
            Assign(notification);
        }    

        #endregion

        #region Methods

        public NotificationDTO ToDTO()
        {
            return new NotificationDTO
            {
                ID = this.ID,
                Message = this.Message,
                UserID = this.UserID,
                Date = this.Date
            };
        }

        public void Assign(NotificationDTO entity)
        {
            this.ID = entity.ID;
            this.Message = entity.Message;
            this.UserID = entity.UserID;
            this.Date = entity.Date;
        }

        #endregion
    }
}
