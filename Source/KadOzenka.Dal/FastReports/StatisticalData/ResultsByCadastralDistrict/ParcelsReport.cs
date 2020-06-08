using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.UI.Registers.Reports.Model;
using ObjectModel.Directory;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ParcelsReport : ResultsByCadastralDistrictBaseReport
    {
        private readonly string _typeOfUseByClassifierFilter = "TypeOfUseByClassifier";
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
            
            var typeOfUseByClassifierFilter = filterValues.FirstOrDefault(f => f.ParamName == _typeOfUseByClassifierFilter);
            var infoAboutExistenceOfOtherObjectsFilter = filterValues.FirstOrDefault(f => f.ParamName == _infoAboutExistenceOfOtherObjects);
            var infoSourceFilter = filterValues.FirstOrDefault(f => f.ParamName == _infoSource);
            var segmentFilter = filterValues.FirstOrDefault(f => f.ParamName == _segment);
            var usageTypeCodeFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeCode);
            var usageTypeNameFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeName);
            var usageTypeCodeSourceFilter = filterValues.FirstOrDefault(f => f.ParamName == _usageTypeCodeSource);

            InitialiseGbuAttributesFilterValue(typeOfUseByClassifierFilter, infoAboutExistenceOfOtherObjectsFilter,
                infoSourceFilter, segmentFilter, usageTypeCodeFilter, usageTypeNameFilter, usageTypeCodeSourceFilter);
        }

        #region Support Methods

        private InputParameters GetInputParameters(NameValueCollection query)
        {
            var typeOfUseByClassifierAttributeId = GetFilterParameterValue(query, _typeOfUseByClassifierFilter, "Вид использования по классификатору");
            var infoAboutExistenceOfOtherObjectsAttributeId = GetFilterParameterValue(query, _infoAboutExistenceOfOtherObjects, "Сведения о нахождении на земельном участке других связанных с ним объектов недвижимости");
            var infoSourceAttributeId = GetFilterParameterValue(query, _infoSource, "Источник информации");
            var segmentAttributeId = GetFilterParameterValue(query, _segment, "Сегмент");
            var usageTypeCodeAttributeId = GetFilterParameterValue(query, _usageTypeCode, "Код вида использования");
            var usageTypeNameAttributeId = GetFilterParameterValue(query, _usageTypeName, "Наименование вида использования");
            var usageTypeCodeSourceAttributeId = GetFilterParameterValue(query, _usageTypeCodeSource, "Источник информации кода вида использования");

            return new InputParameters
            {
                TypeOfUseByClassifierAttributeId = typeOfUseByClassifierAttributeId,
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
            var attributesDictionary = GetAttributesForReport(tourId, inputParameters);

            var units = GetUnits(taskIds, PropertyTypes.Stead);
            //var objectIdForTesting = 11188991;
            //units[0].ObjectId = objectIdForTesting; //объект у которого есть значения в РР (для тестирования)

            var gbuAttributes = GbuObjectService.GetAllAttributes(
                units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList(),
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay());

            var result = new List<ReportItem>();
            units.ToList().ForEach(unit =>
            //units.Where(x => x.ObjectId == objectIdForTesting).ToList().ForEach(unit =>  // для тестирования
            {
                var item = new ReportItem
                {
                    CadastralNumber = unit.CadastralNumber,
                    Square = unit.Square,
                    Upks = unit.Upks,
                    CadastralCost = unit.CadastralCost
                };

                SetAttributes(unit.ObjectId, gbuAttributes, attributesDictionary, item);
                item.CadastralDistrict = GetCadastralDistrict(item.CadastralQuartal);
                
                result.Add(item);
            });

            return result;
        }

        private Dictionary<string, RegisterAttribute> GetAttributesForReport(long tourId, InputParameters inputParameters)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
            attributesDictionary.Add(nameof(ReportItem.ParcelName), StatisticalDataService.GetRosreestrParcelNameAttribute());
            attributesDictionary.Add(nameof(ReportItem.Location), StatisticalDataService.GetRosreestrLocationAttribute());
            attributesDictionary.Add(nameof(ReportItem.Address), StatisticalDataService.GetRosreestrAddressAttribute());
            attributesDictionary.Add(nameof(ReportItem.FormationDate), StatisticalDataService.GetRosreestrFormationDateAttribute());
            attributesDictionary.Add(nameof(ReportItem.ParcelCategory), StatisticalDataService.GetRosreestrParcelCategoryAttribute());
            attributesDictionary.Add(nameof(ReportItem.TypeOfUseByDocuments), StatisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute());

            attributesDictionary.Add(nameof(ReportItem.TypeOfUseByClassifier), RegisterCache.GetAttributeData(inputParameters.TypeOfUseByClassifierAttributeId));
            attributesDictionary.Add(nameof(ReportItem.InfoAboutExistenceOfOtherObjects), RegisterCache.GetAttributeData(inputParameters.InfoAboutExistenceOfOtherObjectsAttributeId));
            attributesDictionary.Add(nameof(ReportItem.InfoSource), RegisterCache.GetAttributeData(inputParameters.InfoSourceAttributeId));
            attributesDictionary.Add(nameof(ReportItem.Segment), RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId));
            attributesDictionary.Add(nameof(ReportItem.UsageTypeCode), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId));
            attributesDictionary.Add(nameof(ReportItem.UsageTypeName), RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId));
            attributesDictionary.Add(nameof(ReportItem.UsageTypeCodeSource), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId));

            var generalAttributes = GetGeneralAttributesForReport(tourId);
            var result = attributesDictionary.Concat(generalAttributes).ToDictionary(x => x.Key, x => x.Value);

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
            public long TypeOfUseByClassifierAttributeId { get; set; }
            public long InfoAboutExistenceOfOtherObjectsAttributeId { get; set; }
            public long InfoSourceAttributeId { get; set; }
            public long SegmentAttributeId { get; set; }
            public long UsageTypeCodeAttributeId { get; set; }
            public long UsageTypeNameAttributeId { get; set; }
            public long UsageTypeCodeSourceAttributeId { get; set; }
        }

        private class ReportItem : InfoFromSettings
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
