using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.MapBuilding;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class MapBuildingService
	{
		public IEnumerable<ColoredDataDto> GetHeatMapData(long tourId, PropertyTypes objectType, MapDivisionType divisionType, string[] colors)
		{
			var data = GetUnitCadastralCostData(tourId, objectType, divisionType);

			var unitAverageDtos = data.Where(x => x.CadastralCost.HasValue).GroupBy(x => new { x.DivisionField })
				.Select(
					group => new UnitAverageCadastralCostDto
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

		private List<UnitCadastralCostDto> GetUnitCadastralCostData(long tourId, PropertyTypes objectType, MapDivisionType divisionType)
		{
			var tourCondition =
				new QSConditionSimple(OMTour.GetColumn(x => x.Id), QSConditionType.Equal, tourId);
			var propertyCondition =
				new QSConditionSimple(OMUnit.GetColumn(x => x.PropertyType_Code), QSConditionType.Equal, (int) objectType);
			var query = new QSQuery
			{
				MainRegisterID = OMUnit.GetRegisterId(),
				Condition = new QSConditionGroup
				{
					Type = QSConditionGroupType.And,
					Conditions = new List<QSCondition> {tourCondition, propertyCondition}
				},
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMTour.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.TourId),
							RightOperand = OMTour.GetColumn(x => x.Id)
						},
						JoinType = QSJoinType.Inner
					},
					new QSJoin
					{
						RegisterId = OMQuartalDictionary.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = OMUnit.GetColumn(x => x.CadastralBlock),
							RightOperand = OMQuartalDictionary.GetColumn(x => x.CadastralQuartal)
						},
						JoinType = QSJoinType.Inner
					}
				}
			};

			query.AddColumn(OMUnit.GetColumn(x => x.Id, "Id"));
			query.AddColumn(OMUnit.GetColumn(x => x.CadastralCost, "CadastralCost"));
			switch (divisionType)
			{
				case MapDivisionType.Districts:
					query.AddColumn(OMQuartalDictionary.GetColumn(x => x.District, "DivisionField"));
					break;
				case MapDivisionType.Regions:
					query.AddColumn(OMQuartalDictionary.GetColumn(x => x.Region, "DivisionField"));
					break;
				case MapDivisionType.Zones:
					query.AddColumn(OMQuartalDictionary.GetColumn(x => x.ZoneRegion, "DivisionField"));
					break;
				case MapDivisionType.Quarters:
					query.AddColumn(OMQuartalDictionary.GetColumn(x => x.CadastralQuartal, "DivisionField"));
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(divisionType), divisionType, null);
			}

			var data = query.ExecuteQuery<UnitCadastralCostDto>();

			return data;
		}

		private List<ColoredDataDto> SetColors(List<UnitAverageCadastralCostDto> initials, string[] colorsArray)
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
	}
}
