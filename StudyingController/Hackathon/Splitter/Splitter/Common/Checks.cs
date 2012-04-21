using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace Splitter.Common
{
    public static class Checks
    {
        [Conditional("DEBUG")]
        public static void CheckPropertyExists(object component, string propertyName)
        {
            if (TypeDescriptor.GetProperties(component)[propertyName] == null)
                throw new InvalidOperationException(string.Format("property '{0}' does not exists", propertyName));
        }

        [Conditional("DEBUG")]
        public static void AssertNotNull(object obj, string ArgName)
        {
            if (obj == null)
                throw new ArgumentNullException(ArgName);
        }
    }
}
