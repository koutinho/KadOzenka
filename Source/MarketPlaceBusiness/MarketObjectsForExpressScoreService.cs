using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Dto.ExpressScore;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForExpressScoreService : AMarketObjectBaseService, IMarketObjectsForExpressScoreService
	{
		public List<AnalogDto> GetAnalogsByIds(List<int> ids)
		{
			return OMCoreObject.Where(x => ids.Contains((int)x.Id))
				.Select(x => new
				{
					x.Id,
					x.CadastralNumber,
					x.Price,
					x.Area,
					x.ParserTime,
					x.LastDateUpdate,
					x.FloorNumber,
					x.BuildingYear,
					x.Address,
					x.DealType_Code,
					x.Vat_Code,
					x.IsOperatingCostsIncluded
				}).Execute()
				.Select(x => new AnalogDto
				{
					Id = x.Id,
					Kn = x.CadastralNumber,
					Price = x.Price.GetValueOrDefault(),
					Square = x.Area.GetValueOrDefault(),
					Date = x.LastDateUpdate ?? x.ParserTime ?? DateTime.MinValue,
					Floor = x.FloorNumber.GetValueOrDefault(),
					YearBuild = x.BuildingYear.GetValueOrDefault(),
					Address = x.Address,
					DealType = x.DealType_Code,
					Vat = x.Vat_Code,
					IsOperatingCostsIncluded = x.IsOperatingCostsIncluded.GetValueOrDefault()
				}).ToList();
		}

		public long? GetAnalogId(string cadastralNumber)
		{
			return OMCoreObject.Where(x => x.CadastralNumber == cadastralNumber).ExecuteFirstOrDefault()?.Id;
		}

		public List<OMCoreObject> GetObjectsInfoForCard(List<long?> resultObjectIds)
		{
			return OMCoreObject
				.Where(x => resultObjectIds.Contains(x.Id))
				.Select(x => new
				{
					x.Id, x.Images, x.Price, x.PricePerMeter, x.Area, x.Address, x.CadastralNumber,
					x.PropertyMarketSegment, x.DealType, x.Market_Code, x.PropertyTypesCIPJS_Code
				})
				.Execute();
		}

		public List<OMCoreObject> GetNearestObjects(DateTime actualDate, QSCondition conditionAnalog)
		{
			var actualDateCondition = GetActualDateCondition(actualDate);

			return OMCoreObject.Where(conditionAnalog.And(actualDateCondition))
				.SetJoins(JoinPriceHistory())
				.Select(x => new
				{
					x.Id, 
					x.Lat, 
					x.Lng, 
					x.CadastralNumber
				}).Execute().ToList();

		}

		#region Support Methods

		private List<QSJoin> JoinPriceHistory()
		{
			return new List<QSJoin>()
			{
				new QSJoin
				{
					JoinCondition = new QSConditionSimple(OMPriceHistory.GetColumn(x => x.InitialId), QSConditionType.Equal,
						Consts.PrimaryKeyColumn),
					RegisterId = OMPriceHistory.GetRegisterId(),
					JoinType = QSJoinType.Left
				}
			};
		}

		private QSCondition GetActualDateCondition(DateTime actualDate)
		{

			actualDate = actualDate + new TimeSpan(23, 59, 59);

			DateTime minSearchDate = new DateTime(actualDate.Year - 1, actualDate.Month, actualDate.Day);


			var actualDateCondition = new QSConditionSimple
			{
				ConditionType = QSConditionType.LessOrEqual,
				LeftOperand = OMPriceHistory.GetColumn(x => x.ChangingDate),
				RightOperand = new QSColumnConstant(actualDate)
			}.And(new QSConditionSimple
			{
				ConditionType = QSConditionType.GreaterOrEqual,
				LeftOperand = OMPriceHistory.GetColumn(x => x.ChangingDate),
				RightOperand = new QSColumnConstant(minSearchDate)
			}).Or(new QSConditionSimple
			{
				ConditionType = QSConditionType.LessOrEqual,
				LeftOperand = Consts.ParserTimeColumn,
				RightOperand = new QSColumnConstant(actualDate)
			}.And(new QSConditionSimple
			{
				ConditionType = QSConditionType.GreaterOrEqual,
				LeftOperand = Consts.ParserTimeColumn,
				RightOperand = new QSColumnConstant(minSearchDate)
			}).And(new QSConditionSimple
			{
				ConditionType = QSConditionType.IsNull,
				LeftOperand = Consts.LastDateUpdateColumn,
			})).Or(new QSConditionSimple
			{
				ConditionType = QSConditionType.LessOrEqual,
				LeftOperand = Consts.LastDateUpdateColumn,
				RightOperand = new QSColumnConstant(actualDate)
			}.And(new QSConditionSimple
			{
				ConditionType = QSConditionType.GreaterOrEqual,
				LeftOperand = Consts.LastDateUpdateColumn,
				RightOperand = new QSColumnConstant(minSearchDate)
			}));

			return actualDateCondition;
		}

		#endregion
	}
}
