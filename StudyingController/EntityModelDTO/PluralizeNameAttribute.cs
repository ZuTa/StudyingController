using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntitiesDTO
{
    public class PluralizeNameAttribute : BaseNameAttribute
    {

        public PluralizeNameAttribute(string text)
            : base(text)
        { 
        }            

    }
}
