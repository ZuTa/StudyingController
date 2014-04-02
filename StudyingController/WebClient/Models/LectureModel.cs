using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class LectureModel : BaseWebModel<LectureRef>
    {
        public LectureRef Lecture { get; set; }
        public IEnumerable<LectureControlDTO> LectureControls { get; set; }

        public LectureModel()
            : base()
        {
        }

        public LectureModel(LectureRef lecture)
            : base(lecture)
        {
            this.FillModel(lecture);
        }

        public override void FillModel(LectureRef refObject)
        {
            base.FillModel(refObject);

            this.LectureControls = new List<LectureControlDTO>();
        }
    }
}