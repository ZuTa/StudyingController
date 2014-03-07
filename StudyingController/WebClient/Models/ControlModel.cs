using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public enum ControlType
    {
        Lecture = 0,
        Practice = 1
    }

    public class ControlModel
    {
        public ControlDTO Control { get; set; }

        public IEnumerable<MarkModel> Marks { get; set; }

        public ControlModel()
        {
            this.Marks = new List<MarkModel>();
        }
    }
}