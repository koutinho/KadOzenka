using System.Collections.Generic;
using System.IO;
using ModelingBusiness.Factors.Entities;
using ModelingBusiness.Objects.Entities;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace ModelingBusiness.Objects
{
	public interface IModelObjectsService
	{
		List<OMModelToMarketObjects> GetModelObjects(long modelId);

		int DestroyModelMarketObjects(OMModel model);

		void ChangeObjectsStatusInCalculation(List<ModelMarketObjectRelationDto> objects);

		Stream ExportMarketObjectsToExcel(long modelId, List<ModelFactorRelation> factors);

		void ExcludeObjectFromCalculation(long objectId);
	}
}