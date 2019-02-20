using System;
using System.Collections.Generic;
using System.Linq;

namespace Sinema
{
    public static class KoltukGrupla
    {
        public static IEnumerable<int> Bölenler(int x)
        {
            var List = new List<int>();
            for (var i = 1; i <= Math.Floor(Math.Sqrt(x)); i++)
            {
                if (x % i != 0) continue;
                List.Add(i);
                List.Add(x / i);
            }

            if (List[List.Count / 2] == List[List.Count / 2 - 1])
                List.Remove(List[List.Count / 2]);

            return List;
        }

        public static IEnumerable<IGrouping<int, TSource>> GroupBy<TSource>
            (this IEnumerable<TSource> source, int itemsPerGroup) => source.Zip(Enumerable.Range(0, source.Count()), (s, r) => new
            {
                Group = r / itemsPerGroup,
                Item = s
            }).GroupBy(i => i.Group, g => g.Item).ToList();
    }
}