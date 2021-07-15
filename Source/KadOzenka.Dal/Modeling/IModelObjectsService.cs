using System.Collections.Generic;
using System.IO;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
	public interface IModelObjectsService
	{
		List<OMModelToMarketObjects> GetModelObjects(long modelId);

		int DestroyModelMarketObjects(OMModel model);

		void ChangeObjectsStatusInCalculation(List<ModelMarketObjectRelationDto> objects);

		Stream ExportMarketObjectsToExcel(long modelId, List<OMModelFactor> factors);

		ModelObjectsService.ModelObjectsCalculationParameters GetModelCalculationParameters(decimal? a0, decimal? objectPrice,
			List<OMModelFactor> factors, List<CoefficientForObject> objectCoefficients, string cadastralNumber);

		void ExcludeObjectFromCalculation(long objectId);
	}
}