using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.LongProcess.Reports.CalculationParams.Entities;
using KadOzenka.Dal.LongProcess.Reports.Entities;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.CalculationParams
{
	public class ModelingResultsLongProcess : ALinearReportsLongProcessTemplate<ModelingResultsLongProcess.ReportItem, ReportInputParameters>
	{
		protected override string ReportName => "Результаты моделирования";
		protected override string ProcessName => nameof(ModelingResultsLongProcess);
		private FactorsService FactorsService { get; }
		private RosreestrRegisterService RosreestrRegisterService { get; }
		private GbuCodRegisterService GbuCodRegisterService { get; }
		private ModelingService ModelingService { get; }
		private string TaskIdsStr { get; set; }
		private string BaseUnitsCondition { get; set; }
		private string BaseSql { get; set; }
		private List<FactorsService.PricingFactors> GroupedFactors { get; set; }
		private List<FactorsService.Attribute> AllAttributes { get; set; }
		private long? ModelId { get; set; }
		private long GroupId { get; set; }


		public ModelingResultsLongProcess() : base(Log.ForContext<ModelingResultsLongProcess>())
		{
			FactorsService = new FactorsService();
			RosreestrRegisterService = new RosreestrRegisterService();
			GbuCodRegisterService = new GbuCodRegisterService();
			ModelingService = new ModelingService();
		}


		protected override bool AreInputParametersValid(ReportInputParameters inputParameters)
		{
			return inputParameters?.TaskIds != null && inputParameters.TaskIds.Count != 0 && inputParameters.GroupId != 0;
		}

		protected override void PrepareVariables(ReportInputParameters inputParameters)
		{
			TaskIdsStr = string.Join(',', inputParameters.TaskIds);

			GroupId = inputParameters.GroupId;
			ModelId = ModelingService.GetActiveModelEntityByGroupId(GroupId)?.Id;

			GroupedFactors = ModelId == null
				? new List<FactorsService.PricingFactors>()
				: FactorsService.GetGroupedModelFactors(ModelId.Value, _queryManager);
			AllAttributes = GroupedFactors.SelectMany(x => x.Attributes).OrderBy(x => x.Name).ToList();

			BaseUnitsCondition = $@" WHERE unit.TASK_ID in ({TaskIdsStr}) and 
										unit.GROUP_ID= {GroupId} and 
										unit.PROPERTY_TYPE_CODE <> 2190 ";

			BaseSql = GetBaseSql();

			Logger.Debug("ИД модели '{ModelId}' для группы '{GroupId}'", ModelId, GroupId);
		}

		protected override ReportsConfig GetProcessConfig()
		{
			var defaultPackageSize = 300000;
			var defaultThreadsCount = 4;

			return GetProcessConfigFromSettings("ModelingResults", defaultPackageSize, defaultThreadsCount);
		}

		protected override int GetMaxItemsCount(ReportInputParameters inputParameters,
			QueryManager queryManager)
		{
			return GetMaxUnitsCount(BaseUnitsCondition, queryManager);
		}

		protected override string GetSql(int packageIndex, int packageSize)
		{
			return $@"{BaseSql}
					{BaseUnitsCondition}
					order by unit.id 
					limit {packageSize} offset {packageIndex * packageSize}";
		}

		protected override Func<ReportItem, string> GetSortingCondition()
		{
			return x => x.CadastralDistrict;
		}

		protected override List<ReportItem> GetReportItems(string sql)
		{
			var dataTable = _queryManager.ExecuteSqlStringToDataSet(sql).Tables[0];

			var items = new List<ReportItem>();
			foreach (DataRow row in dataTable.Rows)
			{
				var item = new ReportItem
				{
					ObjectType = row[nameof(ReportItem.ObjectType)].ParseToStringNullable(),
					CadastralDistrict = row[nameof(ReportItem.CadastralDistrict)].ParseToStringNullable(),
					CadastralNumber = row[nameof(ReportItem.CadastralNumber)].ParseToStringNullable(),
					Address = row[nameof(ReportItem.Address)].ParseToStringNullable(),
					Square = row[nameof(ReportItem.Square)].ParseToDecimalNullable(),
					Upks = row[nameof(ReportItem.Upks)].ParseToDecimalNullable(),
					CadastralCost = row[nameof(ReportItem.CadastralCost)].ParseToDecimalNullable(),
					Factors = FactorsService.ProcessModelFactors(row, AllAttributes)
				};

				items.Add(item);
			}

			return items;
		}

		protected override string GenerateReportTitle()
		{
			var group = OMGroup.Where(x => x.Id == GroupId).Select(x => new
			{
				x.GroupName,
				x.Number
			}).ExecuteFirstOrDefault();

			return $"Результаты моделирования {group?.FullGroupName}";
		}

		protected override List<GbuReportService.Column> GenerateReportHeaders()
		{
			var firstPart = new List<GbuReportService.Column>
			{
				new GbuReportService.Column {Header = "№ п/п", Width = 3},
				new GbuReportService.Column {Header = "Номер кадастрового района", Width = 3},
				new GbuReportService.Column {Header = "Вид объекта недвижимости", Width = 3},
				new GbuReportService.Column {Header = "Кадастровый номер объекта недвижимости", Width = ColumnWidthForCadastralNumber},
				new GbuReportService.Column {Header = "Адрес (местоположение) объекта недвижимости", Width = ColumnWidthForAddresses}
			};

			var factors = new List<GbuReportService.Column>();
			AllAttributes.ForEach(x =>
			{
				factors.Add(new GbuReportService.Column
				{
					Header = x.Name,
					Width = 4
				});
			});

			var secondPart = new List<GbuReportService.Column>
			{
				new GbuReportService.Column
				{
					Header = "Площадь",
					Width = ColumnWidthForDecimals
				},
				new GbuReportService.Column
				{
					Header = "Удельный показатель кадастровой стоимости",
					Width = ColumnWidthForDecimals
				},
				new GbuReportService.Column
				{
					Header = "Кадастровая стоимость",
					Width = ColumnWidthForDecimals
				}
			};

			firstPart.AddRange(factors);
			firstPart.AddRange(secondPart);

			var counter = 0;
			firstPart.ForEach(x => x.Index = counter++);

			return firstPart;
		}

		protected override List<MergedColumns> GenerateReportMergedHeaders()
		{
			if (GroupedFactors.Count == 0)
				return new List<MergedColumns>();

			var numberOfColumnsBeforeFactors = 5;
			return new List<MergedColumns>
			{
				new MergedColumns
				{
					OrderNumber = 1,
					Text = string.Empty,
					StartColumnIndex = 0,
					EndColumnIndex =  numberOfColumnsBeforeFactors - 1
				},
				new MergedColumns
				{
					OrderNumber = 1,
					Text = "Значения ценообразующих факторов",
					StartColumnIndex = numberOfColumnsBeforeFactors,
					EndColumnIndex =  numberOfColumnsBeforeFactors + AllAttributes.Count - 1
				}
			};
		}

		protected override List<object> GenerateReportReportRow(int index, ReportItem item)
		{
			var firstPart = new List<object>
			{
				(index + 1).ToString(),
				item.CadastralDistrict,
				item.ObjectType,
				item.CadastralNumber,
				item.Address
			};

			var factors = new List<object>();
			item.Factors.OrderBy(x => x.Name).ToList().ForEach(x =>
			{
				factors.Add(x.Value);
			});

			var secondPart = new List<object>
			{
				item.Square,
				item.Upks,
				item.CadastralCost
			};

			firstPart.AddRange(factors);
			firstPart.AddRange(secondPart);

			return firstPart;
		}



		#region Support Methods

		private string GetBaseSql()
		{
			var sqlForModelFactors = FactorsService.GetSqlForModelFactors(ModelId, GroupedFactors);

			var addressAttribute = RosreestrRegisterService.GetAddressAttribute();

			var baseSelect = $@"SELECT
                unit.PROPERTY_TYPE as {nameof(ReportItem.ObjectType)}, 
                SUBSTRING(COALESCE(
					(select * from  gbu_get_allpri_attribute_value(unit.object_id, {GbuCodRegisterService.GetCadastralQuarterFinalAttribute().Id})), 
					unit.CADASTRAL_BLOCK), 0, 6) as {nameof(ReportItem.CadastralDistrict)}, 
                unit.CADASTRAL_NUMBER as {nameof(ReportItem.CadastralNumber)}, 
                (select * from  gbu_get_allpri_attribute_value(unit.object_id, {addressAttribute.Id})) as {nameof(ReportItem.Address)},
                unit.SQUARE as {nameof(ReportItem.Square)}, 
                unit.UPKS as {nameof(ReportItem.Upks)}, 
                unit.CADASTRAL_COST as {nameof(ReportItem.CadastralCost)} ";

			var sql = string.IsNullOrWhiteSpace(sqlForModelFactors.Columns)
				? baseSelect
				: $"{baseSelect}, {sqlForModelFactors.Columns} ";

			sql = $@"{sql} FROM ko_unit unit 
                        {sqlForModelFactors.Tables} ";

			return sql;
		}

		#endregion


		#region Entities

		public class ReportItem
		{
			public string ObjectType { get; set; }
			public string CadastralDistrict { get; set; }
			public string CadastralNumber { get; set; }
			public List<FactorsService.Attribute> Factors { get; set; }
			public string Address { get; set; }
			public decimal? Square { get; set; }
			public decimal? Upks { get; set; }
			public decimal? CadastralCost { get; set; }
		}

		#endregion
	}
}
