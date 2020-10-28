using System;
using System.Collections.Generic;
using System.Globalization;
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

		public decimal GetCoefficientFromStringFactor(ParameterDataDto parameterData, int referenceId)
		{
            var type = GetReferenceValueType(referenceId, out bool isIntervalRef);

            if (isIntervalRef)
            {
	            return 0;
            }

			if (type == ReferenceItemCodeType.String)
			{
                var referenceItems = GetReferenceItems(referenceId);
                return referenceItems.FirstOrDefault(x => x.Value.ToLower() == parameterData.StringValue?.ToLower())?.CalculationValue ?? 1;
			}
			return 0;
		}


        public decimal GetCoefficientFromNumberFactor(ParameterDataDto parameterData, int referenceId)
		{
			if (referenceId == 0)
			{
				return parameterData.NumberValue;
			}

			var type = GetReferenceValueType(referenceId, out bool isIntervalReference);

			if (type == ReferenceItemCodeType.Number)
			{
				List<OMEsReferenceItem> referenceItems;
                if (isIntervalReference){
	                 referenceItems = GetReferenceItems(referenceId);
	                 return referenceItems.Select(IntervalReferenceToNumber)
		                 .FirstOrDefault(x => x.KeyFrom <= parameterData.NumberValue && x.KeyTo >= parameterData.NumberValue)?.Value ?? 1;
                }
				referenceItems = GetReferenceItems(referenceId, parameterData.NumberValue.ToString(CultureInfo.InvariantCulture));
                return referenceItems.Select(ReferenceToNumber).Count() != 0 ? referenceItems.Select(ReferenceToNumber).FirstOrDefault(x => x.Key == parameterData.NumberValue)?.Value ?? 1 : 1;
			}
			return 0;
		}


        public decimal GetCoefficientFromDateFactor(ParameterDataDto parameterData, int referenceId)
		{
            var type = GetReferenceValueType(referenceId, out bool isIntervalRef);

			if (type == ReferenceItemCodeType.Date)
			{
                var referenceItems = GetReferenceItems(referenceId);
                if (isIntervalRef)
                { 
	                return referenceItems.Select(IntervalReferenceToDate)
		                .FirstOrDefault(x => x.KeyFrom <= parameterData.DateValue && x.KeyTo >= parameterData.DateValue)?.Value ?? 1;
                }
                return referenceItems.Select(ReferenceToDate).FirstOrDefault(x => x.Key == parameterData.DateValue)?.Value ?? 1;
			}
			return 0;
		}


        public DateReference ReferenceToDate(OMEsReferenceItem item)
		{
			return new DateReference
			{
				CommonValue = item.CommonValue,
				Key = DateTime.Parse(item.Value),
				Value = item.CalculationValue.GetValueOrDefault()
			};
		}

        public DateReferenceInterval IntervalReferenceToDate(OMEsReferenceItem item)
        {
	        return new DateReferenceInterval
            {
				CommonValue = item.CommonValue,
		        KeyFrom = DateTime.Parse(item.ValueFrom),
		        KeyTo = DateTime.Parse(item.ValueTo),
		        Value = item.CalculationValue.GetValueOrDefault()
	        };
        }

        public NumberReference ReferenceToNumber(OMEsReferenceItem item)
		{
			return new NumberReference
			{
				CommonValue = item.CommonValue,
				Key = decimal.TryParse(item.Value, out var res) ? res : decimal.Zero,
				Value = item.CalculationValue.GetValueOrDefault()
			};
		}

        public NumberReferenceInterval IntervalReferenceToNumber(OMEsReferenceItem item)
        {
	        return new NumberReferenceInterval
	        {
		        CommonValue = item.CommonValue,
				KeyFrom = decimal.TryParse(item.ValueFrom, out var resFrom) ? resFrom : decimal.Zero,
		        KeyTo = decimal.TryParse(item.ValueTo, out var resTo) ? resTo : decimal.Zero,
		        Value = item.CalculationValue.GetValueOrDefault()
	        };
        }



        #region Support Methods
        /// <summary>
        /// Получить значения справочника
        /// </summary>
        /// <param name="referenceId">Ид справочника</param>
        /// <param name="value"> Значение справочника если известно</param>
        /// <returns>Значения справочника</returns>
        private List<OMEsReferenceItem> GetReferenceItems(long referenceId, string value = "")
		{
            QSQuery<OMEsReferenceItem> query = value.IsEmpty() ?
                OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll() :
                OMEsReferenceItem.Where(x => x.ReferenceId == referenceId && x.Value == value).SelectAll();
            List<OMEsReferenceItem> result = new List<OMEsReferenceItem>();
            return query.Execute().ToList();
            /*
            result = new List<OMEsReferenceItem>();
            if(specialData) result.Add(query.ExecuteFirstOrDefault());
            else result = query.Execute().ToList();
            return result;
            */
        }

		private ReferenceItemCodeType GetReferenceValueType(long referenceId, out bool isIntervalReference)
		{
			isIntervalReference = false;
            var res = OMEsReference.Where(x => x.Id == referenceId).Select(x => new {x.UseInterval, x.ValueType_Code})
				.ExecuteFirstOrDefault();
            isIntervalReference = res.UseInterval.GetValueOrDefault();
            return res.ValueType_Code;
		}

		#endregion
	}
}
