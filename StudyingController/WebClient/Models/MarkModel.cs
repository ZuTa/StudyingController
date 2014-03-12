using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class MarkModel : BaseWebModel<MarkRef>
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

        public MarkModel(MarkRef mark)
            : base(mark)
        {
        }

        public override void FillModel(MarkRef refObject)
        {
            base.FillModel(refObject);

            if (refObject != null && refObject.Student != null)
            {
                this.StudentName = string.Format(
                    "{0} {1}",
                    refObject.Student.LastName,
                    refObject.Student.FirstName);
            }

            this.MarkValue = refObject.Value.ToString();
        }

        public override BaseRef GetRefObject()
        {
            return base.GetRefObject();
        }
    }
}