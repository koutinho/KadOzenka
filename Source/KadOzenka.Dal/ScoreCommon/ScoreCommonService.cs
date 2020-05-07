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
		public List<long> GetUnitsIdsByCadastralNumber(string cadastralNumber, int tourId)
		{
			return OMUnit.Where(x => x.CadastralNumber == cadastralNumber && x.TourId == tourId)
				.Select(x => x.Id).Execute().Select(x => x.Id).ToList();
		}

		public ParameterDataDto GetParameters(List<long> unitsIds, int attributeId, int registerId, QSConditionGroup qsGroup = null)
		{
			var idAttribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == registerId && x.IsPrimaryKey)?.Id;

			var query = GetQsQuery(registerId, (int)idAttribute.GetValueOrDefault(), unitsIds, qsGroup);
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
			var referenceItems = GetReferenceItems(referenceId);
			var type = GetReferenceValueType(referenceId);

			if (type == ReferenceItemCodeType.String)
			{
				return referenceItems.Select(ReferenceToString).FirstOrDefault(x => x.Key == parameterData.StringValue)?.Value ?? 1;
			}
			return 0;
		}

		public decimal GetCoefficientFromNumberFactor(ParameterDataDto parameterData, int referenceId)
		{
			if (referenceId == 0)
			{
				return parameterData.NumberValue;
			}

			var referenceItems = GetReferenceItems(referenceId);
			var type = GetReferenceValueType(referenceId);

			if (type == ReferenceItemCodeType.Number)
			{
				return referenceItems.Select(ReferenceToNumber).FirstOrDefault(x => x.Key == parameterData.NumberValue)?.Value ?? 1;
			}
			return 0;
		}

		public decimal GetCoefficientFromDateFactor(ParameterDataDto parameterData, int referenceId)
		{
			var referenceItems = GetReferenceItems(referenceId);
			var type = GetReferenceValueType(referenceId);

			if (type == ReferenceItemCodeType.Date)
			{
				return referenceItems.Select(ReferenceToDate).FirstOrDefault(x => x.Key == parameterData.DateValue)?.Value ?? 1;
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


		#region Support Methods

		private List<OMEsReferenceItem> GetReferenceItems(int referenceId)
		{
			return OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll().Execute().ToList();
		}

		private ReferenceItemCodeType GetReferenceValueType(int referenceId)
		{
			return OMEsReference.Where(x => x.Id == referenceId).SelectAll().ExecuteFirstOrDefault().ValueType_Code;
		}

		#endregion
	}
}
