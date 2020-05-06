using System;
using System.Collections.Generic;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ScoreCommon.Dto;
using ObjectModel.Directory.ES;
using ObjectModel.ES;
using ObjectModel.KO;

namespace KadOzenka.Dal.ScoreCommon
{
	public class ScoreCommonService
	{
		public List<long> GetUnitsIdsByKn(string kn, int tourId)
		{
			return OMUnit.Where(x => x.CadastralNumber == kn && x.TourId == tourId)
				.Select(x => x.Id).Execute().Select(x => x.Id).ToList();
		}

		public ParameterDto GetEstimateParameters(List<long> unitsIds, int attributeId, int registerId, QSConditionGroup qsGroup = null)
		{
			var idAttribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == registerId && x.IsPrimaryKey)?.Id;

			var query = GetQsQuery(registerId, (int)idAttribute.GetValueOrDefault(), unitsIds, qsGroup);
			query.AddColumn(new QSColumnSimple(attributeId, nameof(PureParameterDto.Value)));

			return query.ExecuteQuery<PureParameterDto>().Select(x => new ParameterDto(x)).OrderByDescending(x => x.Id).FirstOrDefault();
		}

		public QSQuery GetQsQuery(int registerId, int filterId, List<long> filterValues, QSConditionGroup qsGroup = null)
		{
			var requiredCondition = new QSConditionSimple
			{
				ConditionType = QSConditionType.In,
				LeftOperand = new QSColumnSimple(filterId),
				RightOperand = new QSColumnConstant(filterValues)
			};

			var resQsCGroup = new QSConditionGroup(QSConditionGroupType.And);
			resQsCGroup.Add(qsGroup);
			resQsCGroup.Add(requiredCondition);

			var query = new QSQuery
			{
				MainRegisterID = registerId,
				Condition = resQsCGroup
			};

			return query;
		}

		public decimal GetCoefficientFromStringFactor(ParameterDto parameter, int referenceId)
		{
			var dict = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll().Execute()
				.ToList();
			var type = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault().ValueType_Code;

			if (type == ReferenceItemCodeType.String)
			{
				return dict.Select(ReferenceToString).FirstOrDefault(x => x.Key == parameter.StringValue)?.Value ?? 1;
			}
			return 0;
		}

		public decimal GetCoefficientFromNumberFactor(ParameterDto parameter, int referenceId)
		{
			if (referenceId == 0)
			{
				return parameter.NumberValue;
			}

			var dict = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll().Execute()
				.ToList();
			var type = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault().ValueType_Code;

			if (type == ReferenceItemCodeType.Number)
			{
				return dict.Select(ReferenceToNumber).FirstOrDefault(x => x.Key == parameter.NumberValue)?.Value ?? 1;
			}
			return 0;
		}

		public decimal GetCoefficientFromDateFactor(ParameterDto parameter, int referenceId)
		{
			var dict = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll().Execute()
				.ToList();
			var type = OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault().ValueType_Code;

			if (type == ReferenceItemCodeType.Date)
			{
				return dict.Select(ReferenceToDate).FirstOrDefault(x => x.Key == parameter.DateValue)?.Value ?? 1;
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

		public StringReference ReferenceToString(OMEsReferenceItem item)
		{
			return new StringReference
			{
				Key = item.Value,
				Value = item.CalculationValue.GetValueOrDefault()
			};
		}
	}
}
