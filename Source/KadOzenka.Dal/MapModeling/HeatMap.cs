using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Register.QuerySubsystem;
using DevExpress.DataProcessing;
using ObjectModel.Core.Shared;
using ObjectModel.Market;

namespace KadOzenka.Dal.MapModeling
{

    public class HeatMap
    {

        public List<Tuple<string, decimal, int>> GroupList(List<OMReferenceItem> allData, List<IGrouping<string, OMCoreObject>> groupedData)
        {
            List<Tuple<string, decimal, int>> result = new List<Tuple<string, decimal, int>>();
            groupedData.ForEach(x => { result.Add(new Tuple<string, decimal, int>(x.Key, Math.Round((decimal)x.Average(y => y.PricePerMeter), 2), x.Count())); });
            allData.ForEach(x => { if (result.Where(y => y.Item1 == x.Value).Count() == 0) result.Add(new Tuple<string, decimal, int>(x.Value, 0, 0)); });
            Console.WriteLine($"{string.Join("\n", result.Select(x => $"{x.Item1}\t{x.Item2}\t{x.Item3}"))}\n");
            return result;
        }

        public List<(string name, string color, string counter)> SetColors(List<Tuple<string, decimal, int>> initials, string[] colorsArray)
        {
            decimal min = initials.Min(x => x.Item2), max = initials.Max(x => x.Item2), step = (max - min) / colorsArray.Length;
            int size = colorsArray.Length;
            decimal? next = null;
            List<Tuple<decimal, decimal, string>> deltas = new List<Tuple<decimal, decimal, string>>();
            List<(string name, string color, string counter)> result = new List<(string name, string color, string counter)>();
            for (int i = 0, j = 1; i < size; i++, j++)
            {
                decimal start = next != null ? (decimal)next : Math.Floor(min + step * i);
                decimal end = Math.Ceiling(min + step * j);
                deltas.Add(new Tuple<decimal, decimal, string>(start, end, colorsArray[i]));
                next = end + 1;
            }
            foreach (Tuple<string, decimal, int> pnt in initials)
            {
                foreach (Tuple<decimal, decimal, string> col in deltas)
                {
                    if (pnt.Item2 < col.Item2 && pnt.Item2 > col.Item1)
                    {
                        result.Add((pnt.Item1, col.Item3, Math.Round(pnt.Item2).ToString()));
                        break;
                    }
                }
            }
            return result;
        }

    }

}
