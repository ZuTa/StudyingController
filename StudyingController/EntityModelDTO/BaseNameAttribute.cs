using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntitiesDTO
{
    public class BaseNameAttribute : Attribute, ITextable
    {
        private readonly string text;
        public string Text
        {
            get { return text; }
        }

        public BaseNameAttribute(string text)
        {
            this.text = text;
        }
    }
}
