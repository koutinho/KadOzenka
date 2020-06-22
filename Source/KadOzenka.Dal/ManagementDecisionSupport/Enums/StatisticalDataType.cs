using System.ComponentModel;

namespace KadOzenka.Dal.ManagementDecisionSupport.Enums
{
	public enum StatisticalDataType
	{
		[Description("Количество объектов  по КР/МР/АО в разрезе групп и видов объектов недвижимости")]
		[StatisticalDataFmReportCode(10101)]
		OnTheNumberOfObjectsByAdministrativeDistrictsByGroupsAndTypes = 1001,

		[Description("Количество объектов  по субъекту в разрезе групп и видов объектов недвижимости")]
		[StatisticalDataFmReportCode(10102)]
		OnTheNumberOfObjectsByAdministrativeDistrictsBySubject = 1002,

		[Description("Количество объектов по группам")]
		[StatisticalDataFmReportCode(10103)]
		OnTheNumberOfObjectsByAdministrativeDistrictsByGroups = 1003,

		[Description("Статистика по количеству объектов в разрезе групп")]
		[StatisticalDataFmReportCode(1011)]
		OnTheNumberOfObjectsByGroups = 2,

		[Description("Статистика по зонам ОКС")]
		[StatisticalDataFmReportCode(10121)]
		OnTheNumberOfObjectsByZoneAndSubgroupsOks = 31,

		[Description("Статистика по зонам ЗУ")]
		[StatisticalDataFmReportCode(10122)]
		OnTheNumberOfObjectsByZoneAndSubgroupsZu = 32,

		[Description("Статистика по минимальным, максимальным и средним (арифметическое и средневзвешенное) УПКС по административным округам")]
		[StatisticalDataFmReportCode(1013)]
		OnTheMinMaxAverageUPKSByAdministrativeDistricts = 4,

		[Description("Статистика УПКС в разрезе групп")]
		[StatisticalDataFmReportCode(10141)]
		OnTheMinMaxAverageUPKSByGroups = 51,

		[Description("Статистика УПРС в разрезе групп")]
		[StatisticalDataFmReportCode(10142)]
		OnTheMinMaxAverageUPRSByGroups = 52,

		[Description("Статистика УПКС-УПРС в разрезе групп")]
		[StatisticalDataFmReportCode(10143)]
		OnTheMinMaxAverageUPKSUPRSByGroups = 53,

		[Description("Статистика по минимальным, максимальным и средним (арифметическое и средневзвешенное) УПКС/УПРС в разрезе кадастровых кварталов")]
		[StatisticalDataFmReportCode(1015)]
		OnTheMinMaxAverageUPKSByCadastralQuarters = 6,

		[Description("УПКС по субъекту")]
		[StatisticalDataFmReportCode(1016)]
		SubjectsUPKS = 7,

		[Description("Земельные участки")]
		[StatisticalDataFmReportCode(10170)]
		ResultsByKRForParcels = 80,
        [Description("Здания")]
        [StatisticalDataFmReportCode(10171)]
        ResultsByKRForBuildings = 81,
        [Description("ОНС")]
        [StatisticalDataFmReportCode(10172)]
        ResultsByKRForUncompletedBuildings = 82,
        [Description("Сооружения")]
        [StatisticalDataFmReportCode(10173)]
        ResultsByKRForConstructions = 83,
        [Description("Помещения")]
        [StatisticalDataFmReportCode(10174)]
        ResultsByKRForPlacements = 84,
        [Description("Машино-места")]
        [StatisticalDataFmReportCode(10175)]
        ResultsByKRForParking = 85,

        [Description("Результаты на утверждение")]
		[StatisticalDataFmReportCode(10181)]
		ResultsForApproval = 91,

		[Description("Среднее значение УПКС объектов недвижимости по административным округам")]
		[StatisticalDataFmReportCode(10182)]
		ResultsForApprovalUpksAverageByAdministrativeDistricts = 92,

		[Description("Среднее значение УПКС объектов недвижимости по кадастровым районам")]
		[StatisticalDataFmReportCode(10183)]
		ResultsForApprovalUpksAverageByCadastralRegions = 93,

		[Description("Среднее значение УПКС объектов недвижимости по кадастровым кварталам")]
		[StatisticalDataFmReportCode(10184)]
		ResultsForApprovalUpksAverageByCadastralQuarters = 94,

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

        [Description("Состав данных по перечню объектов недвижимости (ЗУ)")]
		[StatisticalDataFmReportCode(10250)]
        PricingFactorsCompositionForZu = 160,
        [Description("Состав данных по перечню объектов недвижимости (ОКС)")]
        [StatisticalDataFmReportCode(10251)]
        PricingFactorsCompositionForOks = 161,
        [Description("Итоговый состав данных по характеристикам объектов недвижимости")]
        [StatisticalDataFmReportCode(10252)]
        PricingFactorsCompositionFinalUniform = 162,
        [Description("Состав данных о результатах кадастровой оценки предыдущих туров")]
        [StatisticalDataFmReportCode(10253)]
        PricingFactorsCompositionForPreviousTours = 163,
        [Description("Состав данных по характеристикам он взаимно увязанных разнородными сведениями по различным источникам")]
        [StatisticalDataFmReportCode(10254)]
        PricingFactorsCompositionFinalNonuniform = 164,

        [Description("Состав данных объектов недвижимости с присвоенными крви (ОКС)")]
		[StatisticalDataFmReportCode(10261)]
		QualityPricingFactorsEncodingResultsOks = 171,

		[Description("Состав данных объектов недвижимости с присвоенными крви (ЗУ)")]
		[StatisticalDataFmReportCode(10262)]
		QualityPricingFactorsEncodingResultsZu = 172,

		[Description("Группировка объектов недвижимости")]
		[StatisticalDataFmReportCode(10263)]
		QualityPricingFactorsEncodingResultsGrouping = 173,

		[Description("Статистика по количеству/средних значениях/перцентилях/суммарных значениях в разрезе произвольных параметров")]
		[StatisticalDataFmReportCode(1027)]
		StatisticsInContextOfArbitraryParams = 18,

		[Description("Состав данных о рыночной информации")]
		[StatisticalDataFmReportCode(10281)]
		AdditionalFormsMarketDataInfo = 191,

		[Description("Анализ расчетов")]
		[StatisticalDataFmReportCode(10282)]
		AdditionalFormsCalculationsAnalysis = 192,

		[Description("Анализ результатов")]
		[StatisticalDataFmReportCode(10283)]
		AdditionalFormsResultsAnalysis = 193,

		[Description("Выгрузка изменений")]
		[StatisticalDataFmReportCode(10284)]
		AdditionalFormsChangesUploading = 194,

		[Description("Статистика по расчету")]
		[StatisticalDataFmReportCode(10285)]
		AdditionalFormsCalculationStatistics = 195,

	}
}
