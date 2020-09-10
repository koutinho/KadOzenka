using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using Core.Register;
using KadOzenka.Dal.ManagementDecisionSupport.StatisticalData.Entities;
using Core.Register.QuerySubsystem;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ParcelsReport : ResultsByCadastralDistrictBaseReport
    {
        private readonly string _infoAboutExistenceOfOtherObjects = "InfoAboutExistenceOfOtherObjects";
        private readonly string _infoSource = "InfoSource";
        private readonly string _segment = "Segment";
        private readonly string _usageTypeCode = "UsageTypeCode";
        private readonly string _usageTypeName = "UsageTypeName";
        private readonly string _usageTypeCodeSource = "UsageTypeCodeSource";

        protected override string TemplateName(NameValueCollection query)
        {
            return "ResultsByCadastralDistrictForParcelsReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query)?.ToList();
            var tourId = GetTourId(query);
            var inputParameters = GetInputParameters(query);

            var operations = GetOperations(tourId, taskIds, inputParameters);

            var dataSet = new DataSet();
            var itemTable = GetItemDataTable(operations);
            dataSet.Tables.Add(itemTable);

            return dataSet;
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
        {
            if (!initialisation)
                return;
            
            var infoAboutExistenceOfOtherObjectsFilter = filterValues.FirstOrDefault(f => f.ParamName == _infoAboutExistenceOfOtherObjects);
            var infoSourceFilter = filterValues.FirstOrDefault(f => f.ParamName == _infoSource);
            var segmentFilter = filterValues.FirstOrDefault(f => f.ParamName == _segment);
            var usageTypeCodeFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeCode);
            var usageTypeNameFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeName);
            var usageTypeCodeSourceFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeCodeSource);

            InitialiseGbuAttributesFilterValue(infoAboutExistenceOfOtherObjectsFilter,
                infoSourceFilter, segmentFilter, usageTypeCodeFilter, usageTypeNameFilter, usageTypeCodeSourceFilter);
        }


        #region Support Methods

        private InputParameters GetInputParameters(NameValueCollection query)
        {
            var infoAboutExistenceOfOtherObjectsAttributeId = GetFilterParameterValue(query, _infoAboutExistenceOfOtherObjects, "Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости");
            var infoSourceAttributeId = GetFilterParameterValue(query, _infoSource, "Источник информации");
            var segmentAttributeId = GetFilterParameterValue(query, _segment, "Сегмент");
            var usageTypeCodeAttributeId = GetFilterParameterValue(query, _usageTypeCode, "Код вида использования");
            var usageTypeNameAttributeId = GetFilterParameterValue(query, _usageTypeName, "Наименование вида использования");
            var usageTypeCodeSourceAttributeId = GetFilterParameterValue(query, _usageTypeCodeSource, "Источник информации кода вида использования");

            return new InputParameters
            {
                InfoAboutExistenceOfOtherObjectsAttributeId = infoAboutExistenceOfOtherObjectsAttributeId,
                InfoSourceAttributeId = infoSourceAttributeId,
                SegmentAttributeId = segmentAttributeId,
                UsageTypeCodeAttributeId = usageTypeCodeAttributeId,
                UsageTypeNameAttributeId = usageTypeNameAttributeId,
                UsageTypeCodeSourceAttributeId = usageTypeCodeSourceAttributeId
            };
        }

        private List<ReportItem> GetOperations(long tourId, List<long> taskIds, InputParameters inputParameters)
        {
            var sql = GetSqlFileContent("Parcels");

            var parcelName = RosreestrRegisterService.GetParcelNameAttribute();
            var location = RosreestrRegisterService.GetLocationAttribute();
            var address = RosreestrRegisterService.GetAddressAttribute();
            var formationDate = RosreestrRegisterService.GetFormationDateAttribute();
            var parcelCategory = RosreestrRegisterService.GetParcelCategoryAttribute();
            var typeOfUseByDocuments = RosreestrRegisterService.GetTypeOfUseByDocumentsAttribute();
            var typeOfUseByClassifier = RosreestrRegisterService.GetTypeOfUseByClassifierAttribute();

            var infoAboutExistenceOfOtherObjects = RegisterCache.GetAttributeData(inputParameters.InfoAboutExistenceOfOtherObjectsAttributeId);
            var infoSource = RegisterCache.GetAttributeData(inputParameters.InfoSourceAttributeId);
            var segment = RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId);
            var usageTypeCode = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId);
            var usageTypeName = RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId);
            var usageTypeCodeSource = RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId);

            var objectType = StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId);
            var cadastralQuartal = StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId);
            var subGroupNumber = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);

            var sqlWithParameters = string.Format(sql, string.Join(", ", taskIds), parcelName.Id, location.Id,
                address.Id, formationDate.Id, parcelCategory.Id, typeOfUseByDocuments.Id, typeOfUseByClassifier.Id,
                infoAboutExistenceOfOtherObjects.Id, infoSource.Id, segment.Id, usageTypeCode.Id, usageTypeName.Id,
                usageTypeCodeSource.Id, objectType.Id, cadastralQuartal.Id, subGroupNumber.Id);

            var result = QSQuery.ExecuteSql<ReportItem>(sqlWithParameters);

            return result;
        }

        private DataTable GetItemDataTable(List<ReportItem> operations)
        {
            var dataTable = new DataTable("Item");

            dataTable.Columns.Add("Number");

            dataTable.Columns.Add("CadastralNumber");
            dataTable.Columns.Add("CadastralQuartal");
            dataTable.Columns.Add("CadastralDistrict");
            dataTable.Columns.Add("ObjectType");
            dataTable.Columns.Add("Square");

            dataTable.Columns.Add("ParcelName");
            dataTable.Columns.Add("Location");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("FormationDate");
            dataTable.Columns.Add("ParcelCategory");
            dataTable.Columns.Add("TypeOfUseByDocuments");

            dataTable.Columns.Add("TypeOfUseByClassifier");
            dataTable.Columns.Add("InfoAboutExistenceOfOtherObjects");
            dataTable.Columns.Add("InfoSource");
            dataTable.Columns.Add("Segment");
            dataTable.Columns.Add("UsageTypeCode");
            dataTable.Columns.Add("UsageTypeName");
            dataTable.Columns.Add("UsageTypeCodeSource");

            dataTable.Columns.Add("Group");
            dataTable.Columns.Add("Upks");
            dataTable.Columns.Add("CadastralCost");

            for (var i = 0; i < operations.Count; i++)
            {
                dataTable.Rows.Add(i + 1,
                    operations[i].CadastralNumber,
                    operations[i].CadastralQuartal,
                    operations[i].CadastralDistrict,
                    operations[i].ObjectType,
                    operations[i].Square,
                    operations[i].ParcelName,
                    operations[i].Location,
                    operations[i].Address,
                    operations[i].FormationDate,
                    operations[i].ParcelCategory,
                    operations[i].TypeOfUseByDocuments,
                    operations[i].TypeOfUseByClassifier,
                    operations[i].InfoAboutExistenceOfOtherObjects,
                    operations[i].InfoSource,
                    operations[i].Segment,
                    operations[i].UsageTypeCode,
                    operations[i].UsageTypeName,
                    operations[i].UsageTypeCodeSource,
                    operations[i].SubGroupNumber,
                    operations[i].Upks,
                    operations[i].CadastralCost);
            }

            return dataTable;
        }

        #endregion

        #region Entities

        private class InputParameters
        {
            public long InfoAboutExistenceOfOtherObjectsAttributeId { get; set; }
            public long InfoSourceAttributeId { get; set; }
            public long SegmentAttributeId { get; set; }
            public long UsageTypeCodeAttributeId { get; set; }
            public long UsageTypeNameAttributeId { get; set; }
            public long UsageTypeCodeSourceAttributeId { get; set; }
        }

        private class ReportItem : InfoFromTourSettings
        {
            //From Unit
            public string CadastralNumber { get; set; }
            public string CadastralDistrict { get; set; }
            public decimal? Square { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }

            //From Rosreestr
            public string ParcelName { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string FormationDate { get; set; }
            public string ParcelCategory { get; set; }
            public string TypeOfUseByDocuments { get; set; }

            //From Tour Settings


            //From UI
            public string TypeOfUseByClassifier { get; set; }
            public string InfoAboutExistenceOfOtherObjects { get; set; }
            public string InfoSource { get; set; }
            public string Segment { get; set; }
            public string UsageTypeCode { get; set; }
            public string UsageTypeName { get; set; }
            public string UsageTypeCodeSource { get; set; }
        }

        #endregion
    }
}
