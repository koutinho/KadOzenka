using System.ComponentModel;

namespace KadOzenka.Dal.ManagementDecisionSupport.Enums
{
	public enum StatisticalDataType
	{
		[Description("Статистика по количеству объектов по административным округам")]
		[StatisticalDataFmReportCode(1010)]
		OnTheNumberOfObjectsByAdministrativeDistricts = 1,

		[Description("Статистика по количеству объектов в разрезе групп")]
		[StatisticalDataFmReportCode(1011)]
		OnTheNumberOfObjectsByGroups = 2,

		[Description("Статистика по зонам и подгруппам")]
		[StatisticalDataFmReportCode(1012)]
		OnTheNumberOfObjectsByZoneAndSubgroups = 3,

		[Description("Статистика по минимальным, максимальным и средним (арифметическое и средневзвешенное) УПКС по административным округам")]
		[StatisticalDataFmReportCode(1013)]
		OnTheMinMaxAverageUPKSByAdministrativeDistricts = 4,

		[Description("Статистика по минимальным, максимальным и средним (арифметическое и средневзвешенное) УПКС/УПРС в разрезе групп")]
		[StatisticalDataFmReportCode(1014)]
		OnTheMinMaxAverageUPKSByGroups = 5,

		[Description("Статистика по минимальным, максимальным и средним (арифметическое и средневзвешенное) УПКС/УПРС в разрезе кадастровых кварталов")]
		[StatisticalDataFmReportCode(1015)]
		OnTheMinMaxAverageUPKSByCadastralQuarters = 6,

		[Description("УПКС по субъекту")]
		[StatisticalDataFmReportCode(1016)]
		SubjectsUPKS = 7,

		[Description("Результаты в разрезе КР")]
		[StatisticalDataFmReportCode(1017)]
		ResultsByKR = 8,

		[Description("Результаты на утверждение")]
		[StatisticalDataFmReportCode(1018)]
		ResultsForApproval = 9,

		[Description("Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (ОКС)")]
		[StatisticalDataFmReportCode(10191)]
		KRSummaryResultsOks = 101,

		[Description("Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (ЗУ)")]
		[StatisticalDataFmReportCode(10192)]
		KRSummaryResultsZu = 102,

		[Description("Среднее значения УПКС по КК, кадастровым районам и субъекту РФ")]
		[StatisticalDataFmReportCode(1020)]
		UPKSAverageValuesByKKCadastralRegionsAndRussianFederationSubject = 11,

		[Description("Обобщённые показатели")]
		[StatisticalDataFmReportCode(1021)]
		GeneralizedIndicators = 12,

		[Description("Результаты определения кадастровой стоимости")]
		[StatisticalDataFmReportCode(1022)]
		CadastralCostDeterminationResults = 13,

		[Description("Сведения о способе определения кадастровой стоимости (массово или индивидуально) с указанием моделей, подходов, методов, использованных при определения кадастровой стоимости (для каждого объекта недвижимости)")]
		[StatisticalDataFmReportCode(1023)]
		InfoAboutCadastralCostDeterminingMethod = 14,

		[Description("Параметры расчета по подгруппам")]
		[StatisticalDataFmReportCode(10240)]
		CalculationParams = 150,
        [Description("Результаты моделирования")]
        [StatisticalDataFmReportCode(10241)]
        ModelingResults = 151,

        [Description("Состав ценообразующих факторов")]
		[StatisticalDataFmReportCode(1025)]
		PricingFactorsComposition = 16,

		[Description("Результаты кодировки качественных ценообразующих факторов")]
		[StatisticalDataFmReportCode(1026)]
		QualityPricingFactorsEncodingResults = 17,

		[Description("Статистика по количеству/средних значениях/перцентилях/суммарных значениях в разрезе произвольных параметров")]
		[StatisticalDataFmReportCode(1027)]
		StatisticsInContextOfArbitraryParams = 18,
	}
}
