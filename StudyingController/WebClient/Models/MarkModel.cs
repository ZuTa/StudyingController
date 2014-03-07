using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class MarkModel : BaseEntityModel<MarkDTO>
    {
        public string StudentName
        {
            get;
            set;
        }

        public string MarkValue
        {
            get;
            set;
        }

        public MarkModel()
            : base()
        {

        }

        public MarkModel(MarkDTO mark)
        {
            this.FillModel(mark);   
        }

        public override void FillModel(MarkDTO entityObject)
        {
            base.FillModel(entityObject);

            if (entityObject != null && entityObject.Student != null && entityObject.Student.UserInformation != null)
            {
                this.StudentName = string.Format(
                    "{0} {1}",
                    entityObject.Student.UserInformation.LastName,
                    entityObject.Student.UserInformation.FirstName);
            }

            this.MarkValue = entityObject.MarkValue.ToString();
        }
    }
}