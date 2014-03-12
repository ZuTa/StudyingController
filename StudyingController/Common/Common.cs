using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
    public static class Common
    {
        public static TResult WithClass<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            return o == null ? null : evaluator(o);
        }

        public static TResult WithStruct<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : struct
            where TInput : class
        {
            return o == null ? new TResult() : evaluator(o);
        }

        public static TOutput CopyTo<TOutput,TInput>(this TInput o, Func<TOutput> evaluator)
            where TOutput : new()
            where TInput : class
        {
            TOutput obj = evaluator();
            Copy(o, obj);
            return obj;
        }

        public static void Copy(Object from, Object to)
        {
            Type typeFrom = from.GetType();
            Type typeTo = to.GetType();
            var propsTo = typeTo.GetProperties();//PropertyInfoContainer.GetPropertiesInfo(typeTo);
            foreach (var propTo in propsTo)
            {
                PropertyInfo propFrom = typeFrom.GetProperties().FirstOrDefault(p => p.Name == propTo.Name && p.PropertyType == propTo.PropertyType);
                if (propFrom == null || propTo.PropertyType != propFrom.PropertyType || !propTo.CanWrite)
                    continue;
                var propFromValue = propFrom.GetValue(from, null);
                if (propFromValue is IEnumerable && propFromValue.GetType().IsGenericType)
                    continue;

                propTo.SetValue(to, propFromValue, null);
            }
        }


    }
}
