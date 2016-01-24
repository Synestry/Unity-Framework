using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Framework.Utils.Extensions
{
    public static class EnumerableExtension
    {
        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            return source.GetRandom(1).Single();
        }

        public static IEnumerable<T> GetRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}
