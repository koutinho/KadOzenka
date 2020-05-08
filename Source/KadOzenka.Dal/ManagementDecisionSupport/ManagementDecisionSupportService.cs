using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class ManagementDecisionSupportService
	{
		public IEnumerable<ColoredDataDto> GetHeatMapData(long toutId, PropertyTypes objectType, DivisionType divisionType, string[] colors)
		{
			string divisionField = null;
			switch (divisionType)
			{
				case DivisionType.Districts:
					divisionField = "district";
					break;
				case DivisionType.Regions:
					divisionField = "region";
					break;
				case DivisionType.Zones:
					divisionField = "zone_region";
					break;
				case DivisionType.Quarters:
					divisionField = "cadastral_quartal";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(divisionType), divisionType, null);
			}

			var sql = $@"
				select u.id as Id, 
					u.cadastral_cost as CadastralCost, 
					d.{divisionField} as DivisionField 
				from ko_unit u
					join ko_tour tour on tour.id=u.tour_id
					join market_region_dictionaty d on d.cadastral_quartal=u.cadastral_block
				where tour.id={toutId} and property_type_code={(int)objectType}";
			var data = QSQuery.ExecuteSql<UnitNewDto>(sql);
			var unitAverageDtos = data.Where(x => x.CadastralCost.HasValue).GroupBy(x => new { x.DivisionField })
				.Select(
					group => new UnitAverageDto
					{
						DivisionField = group.Key.DivisionField,
						AverageCadastralCost = group.ToList().DefaultIfEmpty().Average(x => x.CadastralCost),
					}).Where(x => x.AverageCadastralCost.HasValue).ToList();

			IEnumerable<ColoredDataDto> result = null;
			if (!unitAverageDtos.IsEmpty())
			{
				result = SetColors(unitAverageDtos, colors).Where(x => !x.name.IsEmpty());
			}

			return result;
		}

		private List<ColoredDataDto> SetColors(List<UnitAverageDto> initials, string[] colorsArray)
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
			foreach (UnitAverageDto pnt in initials)
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
	}
}
