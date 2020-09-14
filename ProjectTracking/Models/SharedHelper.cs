using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking
{
    public static class SharedHelper
    {
        public static List<string> GetErrorListFromModelState
                                              (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }

        //public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        //{
        //    foreach (var parent in source)
        //    {
        //        yield return parent;

        //        var children = selector(parent);
        //        foreach (var child in SelectRecursive(children, selector))
        //            yield return child;
        //    }
        //}
    }
}
