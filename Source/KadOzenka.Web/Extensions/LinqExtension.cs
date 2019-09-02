using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIPJS.Extensions
{
    /// <summary>
    /// Класс custom расширения Linq.
    /// </summary>
    public static class LinqExtension
    {

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
