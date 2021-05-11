using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Dto.Corrections;
using MarketPlaceBusiness.Interfaces.Corrections;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForCorrectionsService : IMarketObjectsForCorrectionByDate,
		IMarketObjectsForCorrectionByRoom, IMarketObjectsForCorrectionByBargain, IMarketObjectsForCorrectionByStage,
		IMarketObjectsForCorrectionByFirstFloor
	{
		#region Дата

		List<OMCoreObject> IMarketObjectsForCorrectionByDate.GetObjects()
		{
			return OMCoreObject
				.Where(x => x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal &&
					(x.ParserTime != null || x.LastDateUpdate != null) &&
					x.PropertyMarketSegment != null)
				.SelectAll().Execute();
		}

		List<IGrouping<MarketSegment, OMCoreObject>> IMarketObjectsForCorrectionByDate.GetObjectsGroupedBySegment(
			DateTime startDate, DateTime endDate)
		{
			return OMCoreObject.Where(x =>
					(x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal) &&
					x.BuildingCadastralNumber != null && x.BuildingCadastralNumber != "" &&
					x.PropertyMarketSegment != null &&
					((x.ParserTime != null && x.ParserTime >= startDate && x.ParserTime <= endDate) ||
					 (x.LastDateUpdate != null && x.LastDateUpdate >= startDate && x.LastDateUpdate <= endDate)))
				.SelectAll(false)
				.Execute()
				.GroupBy(x => x.PropertyMarketSegment_Code)
				.ToList();
		}

		#endregion


		#region Комнатность

		List<OMCoreObject> IMarketObjectsForCorrectionByRoom.GetObjects()
		{
			return OMCoreObject.Where(x => x.RoomsCount == 1 || x.RoomsCount == 3).SelectAll().Execute();
		}

		List<IGrouping<ObjectsGroupedBySegmentForCorrectionByRoom, OMCoreObject>> IMarketObjectsForCorrectionByRoom.GetObjectsGroupedBySegment(List<MarketSegment> calculatedMarketSegments,
				long?[] numberOfRooms)
		{
			return OMCoreObject.Where(x =>
					calculatedMarketSegments.Contains(x.PropertyMarketSegment_Code) &&
					x.BuildingCadastralNumber != null &&
					x.RoomsCount != null && numberOfRooms.Contains(x.RoomsCount) &&
					x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal)
				.SelectAll(false)
				.Execute()
				.GroupBy(x => new
					ObjectsGroupedBySegmentForCorrectionByRoom
					{
						Segment = x.PropertyMarketSegment_Code
					}).ToList();
		}

		public bool IsBuildingContainAllRoomsTypes(List<OMCoreObject> objectsInBuilding)
		{
			bool haveOneRoomApartment = false, haveTwoRoomsApartment = false, haveThreeRoomsApartment = false;

			objectsInBuilding.ForEach(obj =>
			{
				switch (obj.RoomsCount)
				{
					case 1:
						haveOneRoomApartment = true;
						break;
					case 2:
						haveTwoRoomsApartment = true;
						break;
					case 3:
						haveThreeRoomsApartment = true;
						break;
				}
			});

			return haveOneRoomApartment && haveTwoRoomsApartment && haveThreeRoomsApartment;
		}

		public decimal GetAveragePricePerMeter(IEnumerable<OMCoreObject> objects, int numberOfRooms)
		{
			return objects.Where(x => x.RoomsCount == numberOfRooms).Average(x => x.PricePerMeter.GetValueOrDefault());
		}

		#endregion


		#region Торг

		QSQuery<OMCoreObject> IMarketObjectsForCorrectionByBargain.GetBaseQuery()
		{
			return OMCoreObject
				.Where(x => (x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal) &&
				            (x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed));
		}

		List<OMCoreObject> IMarketObjectsForCorrectionByBargain.GetObjects(QSQuery<OMCoreObject> marketObjectsQuery)
		{
			var objects = marketObjectsQuery
				.Select(x => x.Id)
				.Select(x => x.ProcessType_Code)
				.Select(x => x.DealType_Code)
				.Select(x => x.CadastralNumber)
				.Select(x => x.Address)
				.Select(x => x.BuildingCadastralNumber)
				.Select(x => x.PropertyMarketSegment_Code)
				.Select(x => x.District_Code)
				.Select(x => x.Neighborhood_Code)
				.Select(x => x.Zone)
				.Select(x => x.CadastralQuartal)
				.Select(x => x.Price)
				.Select(x => x.Area)
				.Select(x => x.PricePerMeter)
				.Select(x => x.PriceAfterCorrectionByBargain)
				.Select(x => x.LastDateUpdate)
				.Select(x => x.ParserTime)
				.Execute();

			return objects;
		}

		#endregion


		#region Подваль/Цоколь

		List<GeneralInfoForCorrectionByStage> IMarketObjectsForCorrectionByStage.GetObjects(bool isForStage,
			List<MarketSegment> segments)
		{
			var baseQuery = GetBaseQueryForCorrectionByStage(isForStage, segments);

			return baseQuery
				.GroupBy(x => new {x.CadastralNumber, x.PropertyMarketSegment_Code})
				//платформа не дает привести к типу сразу
				.ExecuteSelect(x => new
				{
					CadastralNumber = x.CadastralNumber,
					Segment = x.PropertyMarketSegment_Code,
					Price = x.Avg(y => y.PriceAfterCorrectionByRooms ?? y.Price).Round(4)
				}).Select(x => new GeneralInfoForCorrectionByStage
				{
					CadastralNumber = x.CadastralNumber,
					Segment = x.Segment,
					Price = x.Price
				}).ToList();
		}

		List<OMCoreObject> IMarketObjectsForCorrectionByStage.GetBasementObjects(List<MarketSegment> segments)
		{
			var baseQuery = GetBaseQueryForCorrectionByStage(false, segments);

			return baseQuery
				.Select(x => new
				{
					x.CadastralNumber,
					x.PropertyMarketSegment_Code,
					x.Price,
					x.PriceAfterCorrectionByRooms
				})
				.Execute();
		}

		private QSQuery<OMCoreObject> GetBaseQueryForCorrectionByStage(bool isForStage, List<MarketSegment> segments)
		{
			var dealType = DealType.SaleSuggestion;
			Expression<Func<OMCoreObject, bool>> whereExpression;
			if (isForStage)
			{
				whereExpression = x =>
					x.DealType_Code == dealType
					&& x.CadastralNumber != null
					&& x.FloorNumber >= 0
					&& segments.Contains(x.PropertyMarketSegment_Code);
			}
			else
			{
				whereExpression = x =>
					x.DealType_Code == dealType
					&& x.CadastralNumber != null
					&& x.FloorNumber < 0
					&& segments.Contains(x.PropertyMarketSegment_Code);
			}

			return OMCoreObject.Where(whereExpression);
		}

		#endregion


		#region Первый этаж

		List<OMCoreObject> IMarketObjectsForCorrectionByFirstFloor.GetFirstFloors(List<MarketSegment> segments,
			MarketSegment? segment = null)
		{
			var query = OMCoreObject.Where(x =>
				x.FloorNumber == 1 && x.Price > 1 && x.DealType_Code == DealType.SaleSuggestion);

			query = segment != null
				? query.And(o => o.PropertyMarketSegment_Code == segment.GetValueOrDefault())
				: query.And(o => segments.Contains(o.PropertyMarketSegment_Code));

			return query.SelectAll().Execute();
		}

		List<FloorStatsForCorrectionByFirstFloor> IMarketObjectsForCorrectionByFirstFloor.GetFloorStats(
			bool includeCorrectionByRooms, bool firstFloor = false)
		{
			var query = OMCoreObject
				.Where(o =>
					o.DealType_Code == DealType.SaleSuggestion
					&& o.PropertyMarketSegment_Code != MarketSegment.NoSegment
					&& o.PropertyMarketSegment_Code != MarketSegment.None
					&& o.Price > 1 // Отсекаем пустые цены и цены в 1 (частое явление)
					&& o.CadastralNumber != null
					&& o.CadastralNumber != "");
			var result =
				(firstFloor ? query.And(o => o.FloorNumber == 1) : query.And(o => o.FloorNumber > 1))
				.GroupBy(o => new
				{
					o.CadastralNumber,
					o.PropertyMarketSegment_Code
				})
				.ExecuteSelect(obj => new
				{
					obj.CadastralNumber,
					Segment = obj.PropertyMarketSegment_Code,
					UnitCost = obj.Sum(ff =>
#pragma warning disable 162
						// ReSharper disable once UnreachableCode
						(includeCorrectionByRooms ? ff.PriceAfterCorrectionByRooms : null)
#pragma warning restore 162
						?? ff.Price) / obj.Sum(ff => ff.Area)
				})
				// ExecuteSelect не позволяет сразу привести к нужному типу
				.Select(obj => new FloorStatsForCorrectionByFirstFloor
				{
					CadastralNumber = obj.CadastralNumber,
					Segment = obj.Segment,
					UnitCost = obj.UnitCost
				})
				.ToList();

			return result;
		}

		#endregion
	}
}
