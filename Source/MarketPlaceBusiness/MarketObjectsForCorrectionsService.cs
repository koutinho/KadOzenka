using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Register.QuerySubsystem;
using MarketPlaceBusiness.Dto.Corrections;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForCorrectionsService : IMarketObjectsForCorrectionsService
	{
		public List<OMCoreObject> GetObjectsForCorrectionByDate()
		{
			return OMCoreObject
				.Where(x => x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal &&
					(x.ParserTime != null || x.LastDateUpdate != null) &&
					x.PropertyMarketSegment != null)
				.SelectAll().Execute();
		}

		public List<OMCoreObject> GetObjectsForCorrectionByRoom()
		{ 
			return OMCoreObject.Where(x => x.RoomsCount == 1 || x.RoomsCount == 3).SelectAll().Execute();
		}

		public QSQuery<OMCoreObject> GetBaseQueryForCorrectionByBargain()
		{
			return OMCoreObject
				.Where(x => (x.DealType_Code == DealType.SaleSuggestion || x.DealType_Code == DealType.SaleDeal) &&
				            (x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed));
		}

		public List<OMCoreObject> GetObjectsForCorrectionByBargain(QSQuery<OMCoreObject> marketObjectsQuery)
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

		public List<GeneralInfoForCorrectionByStage> GetObjectsForCorrectionByStage(bool isForStage, List<MarketSegment> segments)
		{
			Expression<Func<OMCoreObject, bool>> whereExpression;
			if (isForStage)
			{
				whereExpression = x =>
					x.DealType_Code == DealType.SaleSuggestion
					&& x.CadastralNumber != null
					&& x.FloorNumber >= 0
					&& segments.Contains(x.PropertyMarketSegment_Code);
			}
			else
			{
				whereExpression = x =>
					x.DealType_Code == DealType.SaleSuggestion
					&& x.CadastralNumber != null
					&& x.FloorNumber < 0
					&& segments.Contains(x.PropertyMarketSegment_Code);
			}

			return OMCoreObject.Where(whereExpression)
				.GroupBy(x => new { x.CadastralNumber, x.PropertyMarketSegment_Code })
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
	}
}
