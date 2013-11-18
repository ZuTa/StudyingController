using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels.Models
{
    public class NotificationModel : BaseModel
    {
        #region Fields & Properties

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        #endregion

        #region Constructors

        public NotificationModel(NotificationDTO notification)
            :base(notification)
        {
            this.Assign(notification);
        }

        #endregion

        #region Methods 

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            NotificationDTO notification = entity as NotificationDTO;
            this.message = notification.Message;
            this.date = notification.Date;
        }

        #endregion
    }
}
