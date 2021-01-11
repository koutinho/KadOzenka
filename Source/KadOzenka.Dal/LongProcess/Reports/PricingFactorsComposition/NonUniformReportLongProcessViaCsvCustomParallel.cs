using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.PricingFactorsComposition;
using Microsoft.Practices.ObjectBuilder2;

namespace KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition
{
	public class NonUniformReportLongProcess : BaseReportLongProcess<NonUniformReportLongProcess.ReportItem>
	{
		protected override string ReportName => "Состав данных по характеристикам объектов недвижимости взаимно увязанных разнородными сведениями по различным источникам";
		protected override string ProcessName => nameof(NonUniformReportLongProcess);

		public NonUniformReportLongProcess() : base(Log.ForContext<NonUniformReportLongProcess>())
		{
		}


		protected override List<string> GenerateReportReportRow(int index, ReportItem item)
		{
			var attributesInfo = new List<string>();
			item.FullAttributes?.ForEach(attribute =>
			{
				attributesInfo.Add(attribute.Name);
				attribute.RegisterNames.ForEach(registerName => attributesInfo.Add(registerName));
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
			var columns = new List<GbuReportService.Column> { sequentialNumberColumn, cadastralNumberColumn };

			var maxNumberOfAttributes = info.Max(x => x.FullAttributes?.Count) ?? 0;
			if (maxNumberOfAttributes == 0)
				return columns;
			var maxNumberOfRegisters = info.SelectMany(x => x.FullAttributes.Select(r => r.RegisterNames?.Count ?? 0)).Max();

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
				columns.Add(characteristicColumn);

				var registerColumnsIndex = characteristicColumn.Index + 1;
				for (var j = 0; j < maxNumberOfRegisters; j++)
				{
					var sourceColumn = new GbuReportService.Column
					{
						Header = $"Итоговый источник информации {j + 1}",
						Index = registerColumnsIndex,
						Width = columnWidth
					};
					columns.Add(sourceColumn);
					registerColumnsIndex++;
				}

				columnIndex += registerColumnsIndex;
			}

			return columns;
		}


		#region Entities

		public class SameAttributes
		{
			public string Name { get; set; }
			public List<string> RegisterNames { get; set; }
		}

		public class ReportItem
		{
			private List<SameAttributes> _fullAttributes;

			public string CadastralNumber { get; set; }
			public long[] Attributes { get; set; }
			public List<SameAttributes> FullAttributes => _fullAttributes ?? (_fullAttributes = GetNotUniqueAttributes());

			private List<SameAttributes> GetNotUniqueAttributes()
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
					return new List<SameAttributes>();

				var gbuAttributesExceptRosreestr = objectAttributes
					.Where(x => x.RegisterId != DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();
				var rosreestrAttributes = objectAttributes
					.Where(x => x.RegisterId == DataCompositionByCharacteristicsService.RosreestrRegisterId).ToList();

				var notUniqueAttribute = new List<SameAttributes>();
				rosreestrAttributes.ForEach(rr =>
				{
					var sameAttributes = gbuAttributesExceptRosreestr.Where(gbu =>
						gbu.Name.StartsWith(rr.Name, StringComparison.InvariantCultureIgnoreCase)).ToList();
					if (sameAttributes.Count == 0)
						return;

					var registerNames = new List<string> { rr.RegisterName };
					registerNames.AddRange(sameAttributes.Select(x => x.RegisterName));
					var attribute = new SameAttributes
					{
						Name = rr.Name,
						RegisterNames = registerNames
					};

					notUniqueAttribute.Add(attribute);
				});

				return notUniqueAttribute;
			}
		}

		#endregion
	}
}
