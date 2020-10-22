using System;
using System.Collections.Generic;
using System.Transactions;
using Core.Shared.Extensions;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Directory;
using ObjectModel.KO;
using KadOzenka.Dal.Modeling.Dto.Factors;

namespace KadOzenka.Dal.Modeling
{
	public class ManualModelingService : BaseModelingService
    {
	    public override void AddModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			var model = new OMModel
			{
				Name = modelDto.Name,
				Description = modelDto.Description,
				GroupId = modelDto.GroupId,
				AlgoritmType_Code = modelDto.AlgorithmType,
                Type_Code = KoModelType.Manual
			};

			model.Formula = model.GetFormulaFull(true);

            model.Save();
        }

		public override bool UpdateModel(ModelingModelDto modelDto)
		{
			ValidateModel(modelDto);

			var existedModel = GetModelEntityById(modelDto.ModelId);

			var newAttributes = modelDto.Attributes ?? new List<ModelAttributeRelationDto>();

			using (var ts = new TransactionScope())
            {
	            existedModel.Name = modelDto.Name;
                existedModel.Description = modelDto.Description;
                existedModel.GroupId = modelDto.GroupId;
                existedModel.IsOksObjectType = modelDto.IsOksObjectType;
				existedModel.AlgoritmType_Code = modelDto.AlgorithmType;
				existedModel.Formula = existedModel.GetFormulaFull(true);
                existedModel.Save();

                newAttributes.ForEach(x =>
                {
	                ModelFactorsService.AddFactor(new ModelFactorDto
	                {
		                GeneralModelId = existedModel.Id,
		                FactorId = x.AttributeId,
		                DictionaryId = x.DictionaryId,
		                Type = existedModel.AlgoritmType_Code
					});
				});

                ts.Complete();
            }

            return false;
        }


		#region Support

		protected override void ValidateModel(ModelingModelDto modelDto)
		{
			base.ValidateModel(modelDto);

			if (modelDto.Type == KoModelType.Manual && modelDto.AlgorithmType == KoAlgoritmType.None)
				throw new Exception($"Для модели типа '{KoModelType.Manual.GetEnumDescription()}' нужно указать Тип алгоритма");
		}

		#endregion
    }
}
