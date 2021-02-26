using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.GbuObject;
using ObjectModel.KO;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using KadOzenka.Dal.Modeling;
using KadOzenka.Dal.Modeling.Repositories;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
    public class PreviousToursService
    {
	    public readonly QueryManager QueryManager;
        protected readonly GbuObjectService GbuObjectService;
        protected readonly StatisticalDataService StatisticalDataService;
        protected readonly RosreestrRegisterService RosreestrRegisterService;
        protected readonly FactorsService FactorsService;
        protected readonly ModelingService ModelingService;

        private const string ReportTitle = "Таблица. Состав данных о результатах кадастровой оценки предыдущих туров";
        private const string TypeTitle = "Тип";
        private const string SquareTitle = "Площадь";
        private const string NameTitle = "Наименование";
        private const string PurposeTitle = "Назначение (для ОКС)";
        private const string PermittedUseTitle = "Разрешенное использование (для ЗУ)";
        private const string AddressTitle = "Адрес";
        private const string LocationTitle = "Местоположение";
        private const string CadastralQuartalTitle = "Кадастровый квартал";
        private const string ParentCadastralNumberTitle = "Кадастровый номер земельного участка в пределах которого расположен объект недвижимости";
        private const string BuildYearTitle = "Год постройки (для ОКС)";
        private const string CommissioningYearTitle = "Год ввода в эксплуатацию (для ОКС)";
        private const string FloorsNumberTitle = "Кол-во этажей (для ОКС)";
        private const string UnderGroundFloorsNumberTitle = "Подземных этажей (для ОКС)";
        private const string WallMaterialTitle = "Материал стен (для ОКС)";
        private const string GroupOrSubgroupTitle = "Группа/ Подгруппа";
        private const string CadastralCostTitle = "Кадастровая стоимость";

        public static readonly List<string> ColumnTitles = new List<string>
        {
            TypeTitle, SquareTitle, NameTitle, PurposeTitle, PermittedUseTitle,
            AddressTitle, LocationTitle, CadastralQuartalTitle, ParentCadastralNumberTitle,
            BuildYearTitle, CommissioningYearTitle, FloorsNumberTitle, UnderGroundFloorsNumberTitle,
            WallMaterialTitle, GroupOrSubgroupTitle, CadastralCostTitle
        };

        public PreviousToursService()
        {
            QueryManager = new QueryManager();
            GbuObjectService = new GbuObjectService();
            StatisticalDataService = new StatisticalDataService();
            RosreestrRegisterService = new RosreestrRegisterService();
            FactorsService = new FactorsService();
            ModelingService = new ModelingService();
        }

        public PreviousToursReportInfo GetReportInfo(List<long> taskIds, long groupId)
        {
            var model = ModelingService.GetActiveModelEntityByGroupId(groupId);
            var factorsByRegisters = model == null
                ? new List<FactorsService.PricingFactors>()
                : FactorsService.GetGroupedModelFactors(model.Id, QueryManager);
            var generalAttributes = factorsByRegisters.SelectMany(x => x.Attributes).ToList();

            if (QueryManager.IsRequestCancellationToken())
            {
                return new PreviousToursReportInfo();
            }
            var sqlFileContent = StatisticalDataService.GetSqlFileContent("PricingFactorsComposition", "PreviousTours");

            var oksName = RosreestrRegisterService.GetObjectNameAttribute();
            var zuName = RosreestrRegisterService.GetParcelNameAttribute();
            var buildingPurpose = RosreestrRegisterService.GetBuildingPurposeAttribute();
            var placementPurpose = RosreestrRegisterService.GetPlacementPurposeAttribute();
            var constructionPurpose = RosreestrRegisterService.GetConstructionPurposeAttribute();
            var permittedUse = RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute();
            var address = RosreestrRegisterService.GetAddressAttribute();
            var location = RosreestrRegisterService.GetLocationAttribute();
            var parentCadastralNumberForOks = RosreestrRegisterService.GetParcelAttribute();
            var buildYear = RosreestrRegisterService.GetBuildYearAttribute();
            var commissioningYear = RosreestrRegisterService.GetCommissioningYearAttribute();
            var floorsNumber = RosreestrRegisterService.GetFloorsNumberAttribute();
            var undergroundFloorsNumber = RosreestrRegisterService.GetUndergroundFloorsNumberAttribute();
            var wallMaterial = RosreestrRegisterService.GetWallMaterialAttribute();

            var sqlForModelFactors = FactorsService.GetSqlForModelFactors(model?.Id, factorsByRegisters);
            if (!string.IsNullOrWhiteSpace(sqlForModelFactors.Columns))
	            sqlForModelFactors.Columns = $", {sqlForModelFactors.Columns}";

            var sqlWithParameters = string.Format(sqlFileContent, string.Join(", ", taskIds), groupId, oksName.Id,
	            zuName.Id, buildingPurpose.Id, placementPurpose.Id, constructionPurpose.Id, permittedUse.Id, address.Id,
	            location.Id, parentCadastralNumberForOks.Id, buildYear.Id, commissioningYear.Id, floorsNumber.Id, 
	            undergroundFloorsNumber.Id, wallMaterial.Id, sqlForModelFactors.Columns, sqlForModelFactors.Tables);

            var dataTable = QueryManager.ExecuteSqlStringToDataSet(sqlWithParameters).Tables[0];

            var items = new List<PreviousTourReportItem>();
            foreach (DataRow row in dataTable.Rows)
            {
	            var item = new PreviousTourReportItem
                {
	                CadastralNumber = row[nameof(PreviousTourReportItem.CadastralNumber)].ParseToStringNullable(),
	                Square = row[nameof(PreviousTourReportItem.Square)].ParseToDecimalNullable(),
	                CadastralCost = row[nameof(PreviousTourReportItem.CadastralCost)].ParseToDecimalNullable(),
                    TourYear = row[nameof(PreviousTourReportItem.TourYear)].ParseToLongNullable(),
                    ResultName = row[nameof(PreviousTourReportItem.ResultName)].ParseToStringNullable(),
                    ResultPurpose = row[nameof(PreviousTourReportItem.ResultPurpose)].ParseToStringNullable(),
                    PermittedUse = row[nameof(PreviousTourReportItem.PermittedUse)].ParseToStringNullable(),
                    Address = row[nameof(PreviousTourReportItem.Address)].ParseToStringNullable(),
                    Location = row[nameof(PreviousTourReportItem.Location)].ParseToStringNullable(),
                    ParentCadastralNumberForOks = row[nameof(PreviousTourReportItem.ParentCadastralNumberForOks)].ParseToStringNullable(),
                    BuildYear = row[nameof(PreviousTourReportItem.BuildYear)].ParseToStringNullable(),
                    CommissioningYear = row[nameof(PreviousTourReportItem.CommissioningYear)].ParseToStringNullable(),
                    FloorsNumber = row[nameof(PreviousTourReportItem.FloorsNumber)].ParseToStringNullable(),
                    UndergroundFloorsNumber = row[nameof(PreviousTourReportItem.UndergroundFloorsNumber)].ParseToStringNullable(),
                    WallMaterial = row[nameof(PreviousTourReportItem.WallMaterial)].ParseToStringNullable(),
                    ObjectType = row[nameof(PreviousTourReportItem.ObjectType)].ParseToStringNullable(),
                    CadastralQuartal = row[nameof(PreviousTourReportItem.CadastralQuartal)].ParseToStringNullable(),
                    SubGroupNumber = row[nameof(PreviousTourReportItem.SubGroupNumber)].ParseToStringNullable(),
                    Factors = FactorsService.ProcessModelFactors(row, generalAttributes)
	            };

	            items.Add(item);
            }

            return new PreviousToursReportInfo
            {
                Title = ReportTitle,
                ColumnTitles = ColumnTitles,
                Items = items.ToList(),
                Tours = GetToursByTasks(taskIds).OrderBy(x => x.Year).ToList(),
                PricingFactors = generalAttributes.OrderBy(x => x.Name).ToList()
            };
        }

        public object GetValueForReportItem(string title, PreviousTourReportItem item)
        {
            if (item == null)
                return null;

            switch (title)
            {
                case TypeTitle:
                    return item.ObjectType;
                case SquareTitle:
                    return item.Square;
                case NameTitle:
                    return item.ResultName;
                case PurposeTitle:
                    return item.ResultPurpose;
                case PermittedUseTitle:
                    return item.PermittedUse;
                case AddressTitle:
                    return item.Address;
                case LocationTitle:
                    return item.Location;
                case CadastralQuartalTitle:
                    return item.CadastralQuartal;
                case ParentCadastralNumberTitle:
                    return item.ParentCadastralNumberForOks;
                case BuildYearTitle:
                    return item.BuildYear;
                case CommissioningYearTitle:
                    return item.CommissioningYear;
                case FloorsNumberTitle:
                    return item.FloorsNumber;
                case UnderGroundFloorsNumberTitle:
                    return item.UndergroundFloorsNumber;
                case WallMaterialTitle:
                    return item.WallMaterial;
                case GroupOrSubgroupTitle:
                    return item.SubGroupNumber;
                case CadastralCostTitle:
                    return item.CadastralCost;
            }

            return null;
        }


        #region Support Methods

        private List<OMTour> GetToursByTasks(List<long> taskIds)
        {
	        return OMTask.Where(x => taskIds.Contains(x.Id))
		        .Select(x => new
		        {
			        x.ParentTour.Id,
			        x.ParentTour.Year
                })
		        .OrderBy(x => x.ParentTour.Year)
		        .Execute()
                .Select(x => x.ParentTour)
                .DistinctBy(x => x.Id)
                .ToList();
        }

        #endregion
    }
}
