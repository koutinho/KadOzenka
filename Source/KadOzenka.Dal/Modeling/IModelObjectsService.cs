using System.Collections.Generic;
using System.IO;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Entities;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
	public interface IModelObjectsService
	{
		int MaxRowsCountInFileForUpdating { get; set; }
		int CurrentRowIndexInFileForUpdating { get; set; }
		string PrefixForFactor { get; }
		string PrefixForValueInNormalizedColumn { get; }
		string PrefixForCoefficientInNormalizedColumn { get; }


		List<OMModelToMarketObjects> GetModelObjects(long modelId);

		List<OMModelToMarketObjects> GetIncludedModelObjects(long modelId, bool isForTraining);

		int DestroyModelMarketObjects(OMModel model);

		void ChangeObjectsStatusInCalculation(List<ModelMarketObjectRelationDto> objects);

		Stream ExportMarketObjectsToExcel(long modelId, List<OMModelFactor> factors);

		Stream UpdateModelObjects(ExcelFile file, List<ColumnToAttributeMapping> columnsMapping);

		ModelObjectsService.ModelObjectsCalculationParameters GetModelCalculationParameters(decimal? a0, decimal? objectPrice,
			List<OMModelFactor> factors, List<CoefficientForObject> objectCoefficients, string cadastralNumber);
	}
}