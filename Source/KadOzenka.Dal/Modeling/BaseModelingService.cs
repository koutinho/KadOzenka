using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.KO;
using ObjectModel.Ko;
using GroupDto = KadOzenka.Dal.Modeling.Dto.GroupDto;

namespace KadOzenka.Dal.Modeling
{
	public abstract class BaseModelingService
	{
		public DictionaryService DictionaryService { get; set; }
		public ModelFactorsService ModelFactorsService { get; set; }

		public BaseModelingService()
		{
			DictionaryService = new DictionaryService();
			ModelFactorsService = new ModelFactorsService();
		}



        public OMModel GetModelEntityByGroupId(long? groupId)
        {
	        if (groupId == null)
		        throw new Exception("Не передан идентификатор Группы для поиска модели");

	        var model = OMModel.Where(x => x.GroupId == groupId).SelectAll().ExecuteFirstOrDefault();
	        if (model == null)
		        throw new Exception($"Не найдена модель для Группы с id='{groupId}'");

	        return model;
        }

        public OMModel GetModelEntityById(long? modelId)
        {
	        if (modelId == null)
		        throw new Exception("Не передан идентификатор Модели для поиска");

	        var model = OMModel.Where(x => x.Id == modelId).SelectAll().ExecuteFirstOrDefault();
	        if (model == null)
		        throw new Exception($"Не найдена Модель с id='{modelId}'");

	        return model;
        }

        public ModelingModelDto GetModelById(long modelId)
        {
	        var model = OMModel.Where(x => x.Id == modelId)
		        .Select(x => new
		        {
			        x.Description,
			        x.Name,
			        x.LinearTrainingResult,
			        x.ExponentialTrainingResult,
			        x.MultiplicativeTrainingResult,
                    x.GroupId,
			        x.ParentGroup.Id,
			        x.ParentGroup.GroupName,
			        x.IsOksObjectType,
                    x.Type_Code,
                    x.AlgoritmType_Code
		        }).ExecuteFirstOrDefault();

	        if (model == null)
				throw new Exception($"Не найдена модель с Id='{modelId}'");

	        var tour = GetModelTour(model.GroupId);

            return new ModelingModelDto
	        {
		        ModelId = model.Id,
		        Name = model.Name,
		        Description = model.Description,
		        LinearTrainingResult = model.LinearTrainingResult,
		        ExponentialTrainingResult = model.ExponentialTrainingResult,
		        MultiplicativeTrainingResult = model.MultiplicativeTrainingResult,
				TourId = tour.Id,
				TourYear = tour.Year.GetValueOrDefault(),
				GroupId = model.ParentGroup.Id,
		        GroupName = model.ParentGroup.GroupName,
		        IsOksObjectType = model.IsOksObjectType.GetValueOrDefault(),
                Type = model.Type_Code,
                AlgorithmType = model.AlgoritmType_Code
	        };
        }

        public List<GroupDto> GetGroups(long tourId)
        {
            var groupsInTour = OMTourGroup.Where(x => x.TourId == tourId).Select(x => x.GroupId).Execute()
                .Select(x => x.GroupId).ToList();
            if (groupsInTour.Count == 0)
                return new List<GroupDto>();

            var groupsToMarketSegmentInTour = OMGroupToMarketSegmentRelation.Where(x => groupsInTour.Contains(x.GroupId))
                .Select(x => x.GroupId)
                .Select(x => x.ParentGroup.GroupName)
                .Execute().Select(
                    x => new GroupDto
                    {
                        GroupId = x.GroupId,
                        Name = x.ParentGroup?.GroupName
                    }).OrderBy(x => x.Name).ToList();

            return groupsToMarketSegmentInTour;
        }

        public OMTour GetModelTour(long? groupId)
        {
	        var tourToGroupRelation = OMTourGroup.Where(x => x.GroupId == groupId).Select(x => new
	        {
		        x.ParentTour.Id,
		        x.ParentTour.Year
	        }).ExecuteFirstOrDefault();
	        if (tourToGroupRelation?.ParentTour == null)
		        throw new Exception($"Для группы {groupId} не найдено Тура");

	        return tourToGroupRelation.ParentTour;
        }

        public abstract void AddModel(ModelingModelDto modelDto);

        public abstract bool UpdateModel(ModelingModelDto modelDto);


        #region Support Methods

		protected virtual void ValidateModel(ModelingModelDto modelDto)
		{
			var message = new StringBuilder();

			if (string.IsNullOrWhiteSpace(modelDto.Name))
				message.AppendLine("У модели не заполнено Имя");
			if (string.IsNullOrWhiteSpace(modelDto.Description))
				message.AppendLine("У модели не заполнено Описание");

			var isTourExists = OMTour.Where(x => x.Id == modelDto.TourId).ExecuteExists();
			if(!isTourExists)
				message.AppendLine($"Не найден Тур с Id='{modelDto.TourId}'");

			if(modelDto.GroupId == 0)
				message.AppendLine("Для модели не выбрана группа");

            var isGroupBelongToTour = OMTourGroup.Where(x => x.TourId == modelDto.TourId && x.GroupId == modelDto.GroupId).ExecuteExists();
            if (!isGroupBelongToTour)
                message.AppendLine($"Группа c Id='{modelDto.GroupId}'не принадлежит туру с Id='{modelDto.TourId}'");

            var isModelExists = OMModel.Where(x => x.Id != modelDto.ModelId && x.GroupId == modelDto.GroupId).ExecuteExists();
            if (isModelExists)
	            message.AppendLine("Модель для данной группы уже существует");

            if (message.Length != 0)
				throw new Exception(message.ToString());
		}

        #endregion
	}
}
