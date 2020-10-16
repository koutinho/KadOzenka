using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.MapBuilding;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.MapModeling;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Directory;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class MapBuildingService
	{
		private ManagementDecisionSupportHeatMap _heatMap;
		public MapBuildingService()
		{
			_heatMap = new ManagementDecisionSupportHeatMap();
		}

		public int GetMapMinZoom()
		{
			return _heatMap.MapMinZoom;
		}

		public int GetMapMaxZoom()
		{
			return _heatMap.MapMaxZoom;
		}

		public IEnumerable<ColoredDataDto> GetHeatMapData(long tourId, PropertyTypes objectType, MapDivisionType divisionType, string[] colors)
		{
			var unitAverageDtos = GetUnitCadastralCostData(tourId, objectType, divisionType)
				.Where(x => x.AverageCadastralCost.HasValue).ToList();

			List<ColoredDataDto> result = null;
			if (!unitAverageDtos.IsEmpty())
			{
				result = _heatMap.SetColors(unitAverageDtos, colors).Where(x => !x.name.IsEmpty()).ToList();
			}

			if (divisionType == MapDivisionType.Quarters)
			{
				_heatMap.GenerateHeatMapQuartalInitialImages(result?.ToDictionary(x => x.name, x => x.color));
			}

			return result;
		}

		private List<UnitAverageCadastralCostDto> GetUnitCadastralCostData(long tourId, PropertyTypes objectType, MapDivisionType divisionType)
		{
			var sql = @$"select MapDivisionName as DivisionField,
AverageCadastralCost as AverageCadastralCost
from average_cadastral_cost_data_for_heatmap
where TourId={tourId} and PropertyType={(int)objectType} and MapDivisionType={(int)divisionType}";
			var data = QSQuery.ExecuteSql<UnitAverageCadastralCostDto>(sql);

			return data;
		}

		public Stream GetHeatMapTile(int x, int y, int z)
		{
			return _heatMap.GetHeatMapTile(x, y, z);
		}

		public void UpdateHeatMapData()
		{
			var sql = "REFRESH MATERIALIZED VIEW CONCURRENTLY average_cadastral_cost_data_for_heatmap;";
			var command = DBMngr.Main.GetSqlStringCommand(sql);
			DBMngr.Main.ExecuteNonQuery(command);
		}
	}
}
