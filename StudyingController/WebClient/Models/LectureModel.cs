using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class LectureModel
    {
        public LectureDTO Lecture { get; set; }
        public IEnumerable<LectureControlDTO> LectureControls { get; set; }

        public LectureModel()
        {
            this.LectureControls = new List<LectureControlDTO>();        
        }
    }
}