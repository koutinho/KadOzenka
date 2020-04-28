using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Modeling;

namespace KadOzenka.Dal.Modeling
{
	public class ModelingService
	{
		public int AddModel(ModelingModelDto modelDto)
		{
			return new OMModelingModel
			{
				Name = modelDto.Name
			}.Save();
		}
	}
}
