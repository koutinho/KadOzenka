using System.Collections.Generic;
using Core.Register.QuerySubsystem;
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

		public List<OMCoreObject> GetMarketObjectsForCorrectionByBargain(QSQuery<OMCoreObject> marketObjectsQuery)
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
    }
}
