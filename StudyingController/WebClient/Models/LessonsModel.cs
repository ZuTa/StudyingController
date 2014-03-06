using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class LessonsModel
    {
        public List<LectureDTO> Lectures { get; set; }
        public List<PracticeDTO> Practices { get; set; }

        public LessonsModel()
        {
            this.Lectures = new List<LectureDTO>();
            this.Practices = new List<PracticeDTO>();
        }
    }
}