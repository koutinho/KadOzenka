using System;
using System.Collections.Generic;
using KadOzenka.Dal.Modeling.Dto.Factors;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
{
	public class ModelFactorsService
	{
		#region Факторы

		public OMModelFactor GetFactorById(long? id)
		{
			var factor = OMModelFactor.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			if (factor == null)
				throw new Exception($"Не найден фактор модели с ИД '{id}'");

			return factor;
		}

		public int AddFactor(ModelFactorDto dto)
		{
			ValidateFactor(dto);

			new OMModelAttribute
			{
				GeneralModelId = dto.GeneralModelId.Value,
				AttributeId = dto.FactorId.Value
			}.Save();

			var id = new OMModelFactor
			{
				ModelId = dto.GeneralModelId,
				FactorId = dto.FactorId,
				MarkerId = -1,
				Weight = dto.Weight,
				B0 = dto.Weight,
				SignDiv = dto.SignDiv,
				SignAdd = dto.SignAdd,
				SignMarket = dto.SignMarket
			}.Save();

			RecalculateFormula(dto.GeneralModelId);

			return id;
		}

		public void UpdateFactor(ModelFactorDto dto)
		{
			var factor = GetFactorById(dto.Id);

			ValidateFactor(dto);

			factor.Weight = dto.Weight;
			factor.B0 = dto.B0;
			factor.SignDiv = dto.SignDiv;
			factor.SignAdd = dto.SignAdd;
			factor.SignMarket = dto.SignMarket;

			factor.Save();

			RecalculateFormula(dto.GeneralModelId);
		}


		#region Support Methods

		private void ValidateFactor(ModelFactorDto factorDto)
		{
			if (factorDto.GeneralModelId == null)
				throw new Exception("Не передан ИД основной модели");

			if (factorDto.FactorId == null)
				throw new Exception("Не передан ИД фактора");

			var model = OMModel.Where(x => x.Id == factorDto.GeneralModelId).Select(x => x.Type_Code).ExecuteFirstOrDefault();
			if (model?.Type_Code == KoModelType.Automatic)
				throw new Exception($"Нельзя вручную работать с факторами для модели типа '{KoModelType.Automatic}'");
		}

		private void RecalculateFormula(long? generalModelId)
		{
			var model = OMModel.Where(x => x.Id == generalModelId).SelectAll().ExecuteFirstOrDefault();
			if(model == null)
				throw new Exception($"Не найдена модель с ИД '{generalModelId}'");

			model.Formula = model.GetFormulaFull(true);
			model.Save();
		}

		#endregion

		#endregion

		#region Метки

		public List<OMMarkCatalog> GetMarks(long generalModelId, long factorId)
		{
			return OMMarkCatalog.Where(x => x.FactorId == factorId && x.GeneralModelId == generalModelId).SelectAll().Execute();
		}

		public OMMarkCatalog GetMarkById(long id)
		{
			var mark = OMMarkCatalog.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
			if (mark == null)
				throw new Exception($"Не найдена метка с ИД {id}");

			return mark;
		}

		public int CreateMark(string value, decimal? metka, long? factorId, long? generalModelId)
		{
			if (generalModelId == null)
				throw new Exception("Не переден ИД основной модели");

			var id = new OMMarkCatalog
			{
				GroupId = -1,
				FactorId = factorId,
				MetkaFactor = metka,
				ValueFactor = value,
				GeneralModelId = generalModelId,
			}.Save();

			return id;
		}

		public void UpdateMark(long id, string value, decimal? metka)
		{
			var mark = GetMarkById(id);

			mark.ValueFactor = value;
			mark.MetkaFactor = metka;

			mark.Save();
		}

		public void DeleteMark(long id)
		{
			var mark = GetMarkById(id);

			mark.Destroy();
		}

		#endregion
	}
}
