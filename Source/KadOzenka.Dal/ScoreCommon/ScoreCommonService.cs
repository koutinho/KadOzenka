using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ScoreCommon.Dto;
using ObjectModel.Directory.ES;
using ObjectModel.ES;
using ObjectModel.KO;

namespace KadOzenka.Dal.ScoreCommon
{
	public class ScoreCommonService
	{
		public List<long> GetUnitsIdsByCadastralNumber(string cadastralNumber, int tourId)
		{
			return OMUnit.Where(x => x.CadastralNumber == cadastralNumber && x.TourId == tourId)
				.Select(x => x.Id).Execute().Select(x => x.Id).ToList();
		}

        public List<OMUnit> GetUnitsByCadastralNumbers(List<string> cadastralNumbers, int tourId)
        {
            if (cadastralNumbers == null || cadastralNumbers.Count == 0)
                return new List<OMUnit>();

            return OMUnit.Where(x => cadastralNumbers.Contains(x.CadastralNumber) && x.TourId == tourId)
                .Select(x => x.Id)
                .Select(x => x.CadastralNumber)
                .Execute();
        }

        public ParameterDataDto GetParameters(List<long> objectIds, int attributeId, int registerId, QSConditionGroup qsGroup = null)
		{
			var idAttribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == registerId && x.IsPrimaryKey)?.Id;

			var query = GetQsQuery(registerId, (int)idAttribute.GetValueOrDefault(), objectIds, qsGroup);
			query.AddColumn(new QSColumnSimple(attributeId, nameof(PureParameterDataDto.Value)));

			return query.ExecuteQuery<PureParameterDataDto>().Select(x => new ParameterDataDto(x)).OrderByDescending(x => x.Id).FirstOrDefault();
		}

		public QSQuery GetQsQuery(int registerId, int filterId, List<long> filterValues, QSConditionGroup qsGroup = null)
		{
			var qsConditionGroup = new QSConditionGroup(QSConditionGroupType.And);
			qsConditionGroup.Add(new QSConditionSimple
			{
				ConditionType = QSConditionType.In,
				LeftOperand = new QSColumnSimple(filterId),
				RightOperand = new QSColumnConstant(filterValues)
			});
			if(qsGroup != null)
				qsConditionGroup.Add(qsGroup);

			var query = new QSQuery
			{
				MainRegisterID = registerId,
				Condition = qsConditionGroup
			};

			return query;
		}

        public List<OMEsReference> GetDictionaries(List<long> dictionaryIds, bool withItems = true)
        {
            if (dictionaryIds == null || dictionaryIds.Count == 0)
                return new List<OMEsReference>();

            var dictionaries = OMEsReference.Where(x => dictionaryIds.Contains(x.Id)).SelectAll().Execute();

            if (!withItems)
                return dictionaries;

            var dictionariesItems = OMEsReferenceItem.Where(x => dictionaryIds.Contains(x.ReferenceId)).SelectAll()
                .Execute().ToList();

            dictionaries.ForEach(dictionary =>
            {
                dictionary.EsReferenceItem = dictionariesItems.Where(item => item.ReferenceId == dictionary.Id).ToList();
            });

            return dictionaries;
        }

        public decimal GetCoefficientFromStringFactor(ParameterDataDto parameterData, int referenceId)
		{
            var type = GetReferenceValueType(referenceId);

			if (type == ReferenceItemCodeType.String)
			{
                var referenceItems = GetReferenceItems(referenceId);
                return referenceItems.FirstOrDefault(x => x.Value.ToLower() == parameterData.StringValue?.ToLower())?.CalculationValue ?? 1;
			}
			return 0;
		}

        /// <summary>
        /// Поиск в словаре коэффициента для строкового атрибута
        /// </summary>
        /// <param name="stringValue">Атрибут</param>
        /// <param name="dictionary">Словарь (желательно с заполненными OMEsReferenceItem)</param>
        /// <returns></returns>
        public decimal GetCoefficientFromStringFactor(string stringValue, OMEsReference dictionary)
        {
            if (dictionary == null)
                return 0;

            if (dictionary.ValueType_Code == ReferenceItemCodeType.String)
            {
                var referenceItems = dictionary.EsReferenceItem ?? GetReferenceItems(dictionary.Id);
                return referenceItems?.FirstOrDefault(x => x.Value == stringValue)?.CalculationValue ?? 1;
            }

            return 0;
        }

        public decimal GetCoefficientFromNumberFactor(ParameterDataDto parameterData, int referenceId)
		{
			if (referenceId == 0)
			{
				return parameterData.NumberValue;
			}

			var type = GetReferenceValueType(referenceId);

			if (type == ReferenceItemCodeType.Number)
			{
                var referenceItems = GetReferenceItems(referenceId, parameterData.NumberValue.ToString(), true);
                return referenceItems.Select(ReferenceToNumber).Count() != 0 ? referenceItems.Select(ReferenceToNumber).FirstOrDefault(x => x.Key == parameterData.NumberValue)?.Value ?? 1 : 1;
			}
			return 0;
		}

        /// <summary>
        /// Поиск в словаре коэффициента для числового атрибута
        /// </summary>
        /// <param name="number">Атрибут</param>
        /// <param name="dictionary">Словарь (желательно с заполненными OMEsReferenceItem)</param>
        public decimal GetCoefficientFromNumberFactor(decimal? number, OMEsReference dictionary)
        {
            if (dictionary == null || number == null)
                return number ?? 0;

            if (dictionary.ValueType_Code == ReferenceItemCodeType.Number)
            {
                var referenceItems = dictionary.EsReferenceItem ?? GetReferenceItems(dictionary.Id);
                return referenceItems?.Select(ReferenceToNumber).FirstOrDefault(x => x.Key == number)?.Value ?? 1;
            }
            return 0;
        }

        public decimal GetCoefficientFromDateFactor(ParameterDataDto parameterData, int referenceId)
		{
            var type = GetReferenceValueType(referenceId);

			if (type == ReferenceItemCodeType.Date)
			{
                var referenceItems = GetReferenceItems(referenceId);
                return referenceItems.Select(ReferenceToDate).FirstOrDefault(x => x.Key == parameterData.DateValue)?.Value ?? 1;
			}
			return 0;
		}

        /// <summary>
        /// Поиск в словаре коэффициента для атрибута с типом 'датa'
        /// </summary>
        /// <param name="date">Атрибут</param>
        /// <param name="dictionary">Словарь (желательно с заполненными OMEsReferenceItem)</param>
        public decimal GetCoefficientFromDateFactor(DateTime? date, OMEsReference dictionary)
        {
            if (dictionary == null || date == null)
                return 0;

            if (dictionary.ValueType_Code == ReferenceItemCodeType.Date)
            {
                var referenceItems = dictionary.EsReferenceItem ?? GetReferenceItems(dictionary.Id);
                return referenceItems?.Select(ReferenceToDate).FirstOrDefault(x => x.Key == date)?.Value ?? 1;
            }
            return 0;
        }

        public DateReference ReferenceToDate(OMEsReferenceItem item)
		{
			return new DateReference
			{
				Key = DateTime.Parse(item.Value),
				Value = item.CalculationValue.GetValueOrDefault()
			};
		}

		public NumberReference ReferenceToNumber(OMEsReferenceItem item)
		{
			return new NumberReference
			{
				Key = decimal.TryParse(item.Value, out var res) ? res : decimal.Zero,
				Value = item.CalculationValue.GetValueOrDefault()
			};
		}


        #region Support Methods

		private List<OMEsReferenceItem> GetReferenceItems(long referenceId, string addVar = "", bool specialData = false)
		{
            QSQuery<OMEsReferenceItem> query = addVar.IsEmpty() ?
                OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll() :
                OMEsReferenceItem.Where(x => x.ReferenceId == referenceId && x.Value == addVar).SelectAll();
            List<OMEsReferenceItem> result = new List<OMEsReferenceItem>();
            return query.Execute().ToList();
            /*
            result = new List<OMEsReferenceItem>();
            if(specialData) result.Add(query.ExecuteFirstOrDefault());
            else result = query.Execute().ToList();
            return result;
            */
        }

		private ReferenceItemCodeType GetReferenceValueType(long referenceId)
		{
			return OMEsReference.Where(x => x.Id == referenceId).Select(x => x.ValueType_Code).ExecuteFirstOrDefault().ValueType_Code;
		}

		#endregion
	}
}
