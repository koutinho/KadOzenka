using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.ObjectBuilder2;
using Serilog;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports.ResultComposition
{
	public class UniformReportLongProcess : BaseReportLongProcess<UniformReportLongProcess.ReportItem>
	{
		protected override string ReportName => "Итоговый состав данных по характеристикам объектов недвижимости";
		protected override string ProcessName => nameof(UniformReportLongProcess);
		protected override long ReportCode => (long)StatisticalDataType.PricingFactorsCompositionFinalUniform;


		public UniformReportLongProcess() : base(Log.ForContext<UniformReportLongProcess>())
		{
			
		}


		protected override List<string> GenerateReportReportRow(int index, ReportItem item)
		{
			var attributesInfo = new List<string>();
			item.FullAttributes.ForEach(x =>
			{
				attributesInfo.Add(x.Name);
				attributesInfo.Add(x.RegisterName);
			});
			var values = new List<string> { (index + 1).ToString(), item.CadastralNumber };
			values.AddRange(attributesInfo);

			return values;
		}

		protected override List<GbuReportService.Column> GenerateReportHeaders(List<ReportItem> info)
		{
			var sequentialNumberColumn = new GbuReportService.Column
			{
				Index = 0,
				Header = "№п/п",
				Width = 2
			};

			var cadastralNumberColumn = new GbuReportService.Column
			{
				Index = 1,
				Header = "Кадастровый номер",
				Width = 4
			};

			var maxNumberOfAttributes = info.Max(x => x.FullAttributes?.Count) ?? 0;
			var columns = new List<GbuReportService.Column>(maxNumberOfAttributes + 2) { sequentialNumberColumn, cadastralNumberColumn };

			//2 - чтобы учесть колонки с номером по порядку и КН
			var columnWidth = 8;
			var columnIndex = 2;
			for (var i = 0; i < maxNumberOfAttributes; i++)
			{
				var characteristicColumn = new GbuReportService.Column
				{
					Header = $"Характеристика объекта {i + 1}",
					Index = columnIndex,
					Width = columnWidth
				};
				var sourceColumn = new GbuReportService.Column
				{
					Header = $"Итоговый источник информации {i + 1}",
					Index = characteristicColumn.Index + 1,
					Width = columnWidth
				};

				columns.Add(characteristicColumn);
				columns.Add(sourceColumn);

				columnIndex += 2;
			}

			return columns;
		}

		#region Entities

		public class ReportItem
		{
			private List<Attribute> _fullAttributes;

			public string CadastralNumber { get; set; }
			public long[] Attributes { get; set; }
			public List<Attribute> FullAttributes => _fullAttributes ?? (_fullAttributes = GetUniqueAttributes());


			private List<Attribute> GetUniqueAttributes()
			{
				var objectAttributes = new List<Attribute>();
				Attributes?.ForEach(attributeId =>
				{
					var attribute = DataCompositionByCharacteristicsService.CachedAttributes.FirstOrDefault(x => x.Id == attributeId);
					var register = DataCompositionByCharacteristicsService.CachedRegisters.FirstOrDefault(x => x.Id == attribute?.RegisterId);
					if (attribute == null || register == null)
						return;

					objectAttributes.Add(new Attribute
					{
						Name = attribute.Name,
						RegisterId = register.Id,
						RegisterName = register.Description
					});
				});

				if (objectAttributes.Count == 0)
					return new List<Attribute>();

				var gbuAttributesExceptRosreestr = objectAttributes
					.Where(x => x.RegisterId != DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();
				var rosreestrAttributes = objectAttributes
					.Where(x => x.RegisterId == DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();

				//симметрическая разность множеств
				var uniqueAttributes = new List<Attribute>();
				//отбираем уникальные аттрибуты из РР
				rosreestrAttributes.ForEach(rr =>
				{
					var isSameAttributesExist = gbuAttributesExceptRosreestr.Any(gbu =>
						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

					if (!isSameAttributesExist)
						uniqueAttributes.Add(rr);
				});
				//отбираем уникальные аттрибуты из всех источников кроме РР
				gbuAttributesExceptRosreestr.ForEach(gbu =>
				{
					var isSameAttributesExist = rosreestrAttributes.Any(rr =>
						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase));

					if (!isSameAttributesExist)
						uniqueAttributes.Add(gbu);
				});

				return uniqueAttributes;
			}
		}

		#endregion
	}
}
