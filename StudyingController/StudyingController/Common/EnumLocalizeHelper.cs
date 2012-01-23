using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using EntitiesDTO;

namespace StudyingController.Common
{
    public static class EnumLocalizeHelper
    {
        public static string GetText<T>(this Enum value) where T : BaseNameAttribute
        {
            Type type = value.GetType();
            FieldInfo info = type.GetField(value.ToString());

            T[] attr = (T[])info.GetCustomAttributes(typeof(T), false);
            object o = Properties.Resources.ResourceManager.GetString(attr[0].Text);
            return attr.Length > 0 ? Properties.Resources.ResourceManager.GetString(attr[0].Text) : string.Empty;
        }
    }
}
