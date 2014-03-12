using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class LectureModel : BaseWebModel<LectureRef>
    {
        public LectureDTO Lecture { get; set; }
        public IEnumerable<LectureControlDTO> LectureControls { get; set; }

        public LectureModel()
        {
            this.LectureControls = new List<LectureControlDTO>();        
        }

        public override void FillModel(LectureRef refObject)
        {
            base.FillModel(refObject);
        }
    }
}