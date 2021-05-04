using System;
using System.Collections.Generic;
using System.Linq;
using MarketPlaceBusiness.Dto.ExpressScore;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	public class MarketObjectsForExpressScoreService : MarketObjectBaseService, IMarketObjectsForExpressScoreService
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
	}
}
