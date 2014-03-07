using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
