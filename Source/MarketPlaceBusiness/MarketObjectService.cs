using System.Collections.Generic;
using System.Linq;
using MarketPlaceBusiness.Common;
using MarketPlaceBusiness.Interfaces;
using ObjectModel.Directory;
using ObjectModel.Market;

namespace MarketPlaceBusiness
{
	/// <summary>
	/// Сервис с методами, для которых нет смысла создавать отдельные сервисы
	/// </summary>
	public class MarketObjectService : AMarketObjectBaseService, IMarketObjectService
	{
		//TODO inject via IoC
		public MarketObjectService(IMarketObjectsRepository marketObjectsRepository = null)
			: base(marketObjectsRepository)
		{
		}


		#region Для удаления дубликатов

		public List<OMCoreObject> GetObjectsForDuplicatesChecking()
		{
			return OMCoreObject
				.Where(x => x.ProcessType_Code == ProcessStep.CadastralNumberStep ||
				            x.ProcessType_Code == ProcessStep.InProcess ||
				            x.ExclusionStatus_Code == ExclusionStatus.Duplicate)
				.Select(x => new { x.CadastralNumber, x.DealType_Code, x.PropertyTypesCIPJS_Code, x.PropertyMarketSegment_Code, x.Market_Code, x.Market, x.ExclusionStatus_Code, x.Price, x.Area, x.ParserTime, x.DealType })
				.Execute()
				.ToList();
		}

		#endregion
	}
}
