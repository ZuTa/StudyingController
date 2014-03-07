using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class PracticeModel
    {
        public PracticeTeacherDTO Practice { get; set; }
        public IEnumerable<PracticeControlDTO> PracticeControls { get; set; }

        public PracticeModel()
        {
            this.PracticeControls = new List<PracticeControlDTO>();        
        }
    }
}