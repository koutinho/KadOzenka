using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ObjectModel.Core.Shared;
using ObjectModel.Market;

namespace KadOzenka.Dal.MapModeling
{

	public class HeatMap
	{
		private static readonly object LockObject = new object();

		public List<Tuple<string, decimal, int>> GroupList(List<OMReferenceItem> allData, List<IGrouping<string, OMCoreObject>> groupedData)
		{
			List<Tuple<string, decimal, int>> result = new List<Tuple<string, decimal, int>>();
			groupedData.ForEach(x => { result.Add(new Tuple<string, decimal, int>(x.Key, Math.Round((decimal)x.Average(y => y.PricePerMeter), 2), x.Count())); });
			allData.ForEach(x => { if (result.Where(y => y.Item1 == x.Value).Count() == 0) result.Add(new Tuple<string, decimal, int>(x.Value, 0, 0)); });
			Console.WriteLine($"{string.Join("\n", result.Select(x => $"{x.Item1}\t{x.Item2}\t{x.Item3}"))}\n");
			return result;
		}

		public List<Tuple<string, decimal, int>> GroupList(List<string> allData, List<IGrouping<string, OMCoreObject>> groupedData)
		{
			List<Tuple<string, decimal, int>> result = new List<Tuple<string, decimal, int>>();
			groupedData.ForEach(x => { result.Add(new Tuple<string, decimal, int>(x.Key, Math.Round((decimal)x.Average(y => y.PricePerMeter), 2), x.Count())); });
			allData.ForEach(x => { if (result.Where(y => y.Item1 == x).Count() == 0) result.Add(new Tuple<string, decimal, int>(x, 0, 0)); });
			Console.WriteLine($"{string.Join("\n", result.Select(x => $"{x.Item1}\t{x.Item2}\t{x.Item3}"))}\n");
			return result;
		}

		public (List<(string name, string color, string counter)> ColoredData, List<(string min, string max)> MinMaxData) SetColors(List<Tuple<string, decimal, int>> initials, string[] colorsArray)
		{
			decimal min = initials.Min(x => x.Item2), max = initials.Max(x => x.Item2), step = (max - min) / colorsArray.Length;
			int size = colorsArray.Length;
			decimal? next = null;
			List<Tuple<decimal, decimal, string>> deltas = new List<Tuple<decimal, decimal, string>>();
			List<(string name, string color, string counter)> ColoredResult = new List<(string name, string color, string counter)>();
			List<(string min, string max)> MinMaxResult = new List<(string min, string max)>();
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
						ColoredResult.Add((pnt.Item1, col.Item3, Math.Round(pnt.Item2).ToString()));
						break;
					}
				}
			}
			foreach (Tuple<decimal, decimal, string> col in deltas) MinMaxResult.Add((Math.Round(col.Item1).ToString(), Math.Round(col.Item2).ToString()));
			return (ColoredResult, MinMaxResult);
		}

		public void GenerateHeatMapQuartalInitialImages(List<(string name, string color, string counter)> coloredData)
		{
			var colorDictionary = coloredData?.ToDictionary(x => x.name, x => x.color);
			MapTilesConfig.ClearMarketHeatMapInitialImages();
			for (int i = MapTilesConfig.Current.MCMinZoom, mult = 1; i <= MapTilesConfig.Current.MCMaxZoom; i++, mult *= 2)
			{
				List<dynamic> coords = MapTilesConfig.GetPixelCoordinatesFromJsonConfigFile(i);
				var image = new ImageProccessor.MapDrawer().DrawMap(MapTilesConfig.Current.MCImgWidth * mult,
					MapTilesConfig.Current.MCImgHeight * mult,
					coords,
					MapTilesConfig.GetMarketHeatMapInitialImageFileName(i),
					i > 12,
					colorDictionary);

				MapTilesConfig.AddMarketHeatMapInitialImages(i, image);
			}
		}

		public Stream GetHeatMapTile(int x, int y, int z)
		{
			Stream result = null;
			var initialImage = MapTilesConfig.GetMarketHeatMapInitialImage(z);

			if (initialImage == null)
				return null;

			lock (LockObject)
			{
				result = new ImageProccessor.MapDrawer().ChopTileFromImage(
					initialImage,
					x, 
					y,
					MapTilesConfig.Current.MCHorizontalStartTile,
					MapTilesConfig.Current.MCVerticalStartTile, 
					MapTilesConfig.Current.MCMinZoom, 
					z,
					MapTilesConfig.Current.MCTileSize);
			}

			return result;
		}
	}

}
