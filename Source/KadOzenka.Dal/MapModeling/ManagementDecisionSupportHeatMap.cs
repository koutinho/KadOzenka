using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using KadOzenka.Dal.ConfigurationManagers;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.MapBuilding;

namespace KadOzenka.Dal.MapModeling
{
	public class ManagementDecisionSupportHeatMap : HeatMap
	{
		public List<ColoredDataDto> SetColors(List<UnitAverageCadastralCostDto> initials, string[] colorsArray)
		{
			decimal min = initials.Min(x => x.AverageCadastralCost.Value), max = initials.Max(x => x.AverageCadastralCost.Value), step = (max - min) / colorsArray.Length;
			int size = colorsArray.Length;
			decimal? next = null;
			List<Tuple<decimal, decimal, string>> deltas = new List<Tuple<decimal, decimal, string>>();
			List<ColoredDataDto> result = new List<ColoredDataDto>();
			for (int i = 0, j = 1; i < size; i++, j++)
			{
				decimal start = next != null ? (decimal)next : Math.Floor(min + step * i);
				decimal end = Math.Ceiling(min + step * j);
				deltas.Add(new Tuple<decimal, decimal, string>(start, end, colorsArray[i]));
				next = end + 1;
			}
			foreach (UnitAverageCadastralCostDto pnt in initials)
			{
				foreach (Tuple<decimal, decimal, string> col in deltas)
				{
					if (pnt.AverageCadastralCost < col.Item2 && pnt.AverageCadastralCost > col.Item1)
					{
						result.Add(new ColoredDataDto { name = pnt.DivisionField, color = col.Item3 });
						break;
					}
				}
			}

			return result;
		}

		protected override void ClearHeatMapInitialImages()
		{
			ManagementDecisionSupportInitialImagesCache.ClearManagementDecisionSupportHeatMapInitialImages();
		}

		protected override string GetHeatMapInitialImageFileName(int zoom)
		{
			return ConfigurationManager.KoConfig.MapTilesConfig.GetManagementDecisionSupportHeatMapInitialImageFileName(zoom);
		}

		protected override Image GetHeatMapInitialImage(int zoom)
		{
			return ManagementDecisionSupportInitialImagesCache.GetManagementDecisionSupportHeatMapInitialImage(zoom);
		}
	}
}
