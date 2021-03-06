using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults.Entities;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.CadastralCostDeterminationResults
{
	public class CadastralCostDeterminationResultsBaseReportLongProcess : ALinearReportsLongProcessTemplate<ReportItem, ReportLongProcessInputParameters>
	{
		private string TaskIdsStr { get; set; }
		private List<long?> GroupIds { get; set; }
		private string GroupIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private long CadastralQuarterAttributeId { get; set; }
		private ICadastralCostDeterminationResultsReport ConcreteReport { get; set; }
		protected override string ReportName => ConcreteReport?.ReportName;
		protected override string ProcessName => nameof(CadastralCostDeterminationResultsBaseReportLongProcess);
		public static string IndividuallyResultsGroupNamePhrase => "индивидуального расчета";
		protected override long ReportCode => (long)StatisticalDataType.CadastralCostDeterminationResults;



		public CadastralCostDeterminationResultsBaseReportLongProcess() : base(Log.ForContext<CadastralCostDeterminationResultsBaseReportLongProcess>())
		{
			
		}



		protected override bool AreInputParametersValid(ReportLongProcessInputParameters inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0;
		}

		protected override void PrepareVariables(ReportLongProcessInputParameters inputParameters)
		{
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);
			CadastralQuarterAttributeId = new GbuCodRegisterService().GetCadastralQuarterFinalAttribute().Id;

			var reportType = inputParameters.Type == ReportType.State ? typeof(StateResultsReport) : typeof(IndividuallyResultsReport);
			ConcreteReport = (ICadastralCostDeterminationResultsReport)Activator.CreateInstance(reportType);
			Logger.Debug("Тип отчета {ReportType}", ConcreteReport.GetType().ToString());

			GroupIds = ConcreteReport.GetAvailableGroupIds();
			GroupIdsStr = string.Join(',', GroupIds);
			Logger.Debug("Найдено {GroupsCount} Групп", GroupIds.Count);

			BaseUnitsCondition = $@" where unit.task_id IN ({TaskIdsStr}) AND 
									unit.GROUP_ID IN ({GroupIdsStr}) AND
									(unit.PROPERTY_TYPE_CODE <> 2190 or unit.PROPERTY_TYPE_CODE is null)";
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 200000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings("StateOrIndividualCadastralCostDeterminationResults", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportLongProcessInputParameters inputParameters)
		{
			if (GroupIds.Count == 0)
				return 0;

			return GetMaxUnitsCount(BaseUnitsCondition);
		}

		protected override string GetMessageForReportsWithoutUnits(ReportLongProcessInputParameters inputParameters)
		{
			var message = "У заданий на оценку нет единиц оценки, принадлежащих к группе ";

			if (ConcreteReport.GetType() == typeof(StateResultsReport))
			{
				message += $"не {IndividuallyResultsGroupNamePhrase}";
			}
			else
			{
				message += IndividuallyResultsGroupNamePhrase;
			}

			return message;
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			var unitsCondition = $@"{BaseUnitsCondition}
										order by unit.id 
										limit {packageSize} offset {packageIndex * packageSize}";

			var sql = $@"/*{packageIndex}*/ with object_ids as (
									select object_id from ko_unit unit {unitsCondition}
								),
								cadastralDistrictAttrValues as (
									select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {CadastralQuarterAttributeId})
								)
								SELECT
									SUBSTRING(COALESCE(cadastralDistrictAttr.attributeValue, unit.CADASTRAL_BLOCK), 0, 6) as CadastralDistrict,
									unit.CADASTRAL_NUMBER AS CadastralNumber,
									unit.PROPERTY_TYPE AS Type,
									unit.SQUARE AS SQUARE,
									unit.UPKS AS UPKS,
									unit.CADASTRAL_COST AS Cost
										FROM KO_UNIT unit
										LEFT JOIN cadastralDistrictAttrValues cadastralDistrictAttr ON unit.object_id=cadastralDistrictAttr.objectId
										{unitsCondition}";

			return sql;
		}

		protected override Func<IEnumerable<ReportItem>, IEnumerable<ReportItem>> FuncForDownloadedItems()
		{
			return x => x.OrderBy(y => y.CadastralDistrict);
		}

		protected override List<Column> GenerateReportHeaders()
		{
			return ConcreteReport.GenerateReportHeaders();
		}

		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			return ConcreteReport.GenerateReportReportRow(index, item);
		}
	}
}
