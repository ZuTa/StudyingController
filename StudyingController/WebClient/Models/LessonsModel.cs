using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class LessonsModel
    {
        public List<LectureRef> Lectures { get; set; }
        public List<PracticeTeacherRef> Practices { get; set; }

        public LessonsModel()
        {
            this.Lectures = new List<LectureRef>();
            this.Practices = new List<PracticeTeacherRef>();
        }
    }
}