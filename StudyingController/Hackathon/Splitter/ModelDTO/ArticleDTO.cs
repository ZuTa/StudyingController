using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    public class ArticleDTO : BaseDTO
    {
        private string title;
        [DataMember]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string text;
        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private DateTime date;
        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public ArticleDTO()
            : base()
        {
 
        }
    }
}
