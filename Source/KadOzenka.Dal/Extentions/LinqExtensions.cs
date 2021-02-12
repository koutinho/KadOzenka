using System;
using System.Collections.Generic;

namespace KadOzenka.Dal.Extentions
{
    public static class LinqExtensions
    {
        public static IEnumerable<List<T>> Split<T>(this List<T> list, int chunkSize)
        {
            for (int i = 0; i < list.Count; i += chunkSize)
            {
                yield return list.GetRange(i, Math.Min(chunkSize, list.Count - i));
            }
        }
    }
}