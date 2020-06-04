using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using Core.UI.Registers.Reports.Model;
using ObjectModel.Directory;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ParcelsReport : StatisticalDataReport
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
            var inputParameters = GetInputParameters(query);

            var operations = GetOperations(taskIds, inputParameters);

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

        private List<ReportItem> GetOperations(List<long> taskIds, InputParameters inputParameters)
        {
            var attributesDictionary = GetAttributesForReport(inputParameters);

            var units = GetUnits(taskIds);
            //units[0].ObjectId = 14427146; //объект у которого есть значения в РР (для тестирования)

            var gbuAttributes = GbuObjectService.GetAllAttributes(
                units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList(),
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay());

            var properties = typeof(ReportItem).GetProperties();
            var rosreestrAttributesProperty = properties.FirstOrDefault(x => x.Name == nameof(ReportItem.RosreestrAttributes));
            var customAttributesProperty = properties.FirstOrDefault(x => x.Name == nameof(ReportItem.CustomAttributes));

            var result = new List<ReportItem>();
            units.ToList().ForEach(unit =>
            //units.Where(x => x.ObjectId == 14427146).ToList().ForEach(unit =>  // для тестирования
            {
                var item = new ReportItem
                {
                    UnitInfo = new UnitInfo
                    {
                        CadastralNumber = unit.CadastralNumber,
                        CadastralQuartal = unit.CadastralBlock,
                        CadastralDistrict = GetCadastralDistrict(unit.CadastralBlock),
                        ObjectType = unit.PropertyType_Code,
                        Square = unit.Square,
                        Group = unit.ParentGroup?.GroupName,
                        Upks = unit.Upks,
                        CadastralCost = unit.CadastralCost
                    }
                };

                SetAttributes(unit.ObjectId, gbuAttributes, attributesDictionary, rosreestrAttributesProperty,
                    customAttributesProperty, item);

                result.Add(item);
            });

            return result;
        }

        private static void SetAttributes(long? objectId, List<GbuObjectAttribute> gbuAttributes, Dictionary<string, RegisterAttribute> attributesDictionary,
            PropertyInfo rosreestrAttributesProperty, PropertyInfo customAttributesProperty, ReportItem item)
        {
            var objectAttributes = gbuAttributes.Where(x => x.ObjectId == objectId).ToList();
            foreach (var objectAttribute in objectAttributes)
            {
                var attributeKeys = attributesDictionary.Where(x => x.Value.Id == objectAttribute.AttributeId).Select(x => x.Key);
                foreach (var key in attributeKeys)
                {
                    var property = rosreestrAttributesProperty?.PropertyType.GetProperty(key);
                    if (property == null)
                    {
                        property = customAttributesProperty?.PropertyType.GetProperty(key);
                        property?.SetValue(item.CustomAttributes, objectAttribute.GetValueInString());
                    }
                    else
                    {
                        property.SetValue(item.RosreestrAttributes, objectAttribute.GetValueInString());
                    }
                }
            }
        }

        private Dictionary<string, RegisterAttribute> GetAttributesForReport(InputParameters inputParameters)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
            attributesDictionary.Add(nameof(RosreestrAttributes.ParcelName), StatisticalDataService.GetRosreestrParcelNameAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.Location), StatisticalDataService.GetRosreestrLocationAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.Address), StatisticalDataService.GetRosreestrAddressAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.FormationDate), StatisticalDataService.GetRosreestrFormationDateAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.ParcelCategory), StatisticalDataService.GetRosreestrParcelCategoryAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.TypeOfUseByDocuments), StatisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute());

            attributesDictionary.Add(nameof(CustomAttributes.TypeOfUseByClassifier), RegisterCache.GetAttributeData(inputParameters.TypeOfUseByClassifierAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.InfoAboutExistenceOfOtherObjects), RegisterCache.GetAttributeData(inputParameters.InfoAboutExistenceOfOtherObjectsAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.InfoSource), RegisterCache.GetAttributeData(inputParameters.InfoSourceAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.Segment), RegisterCache.GetAttributeData(inputParameters.SegmentAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeCode), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeName), RegisterCache.GetAttributeData(inputParameters.UsageTypeNameAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeCodeSource), RegisterCache.GetAttributeData(inputParameters.UsageTypeCodeSourceAttributeId));

            return attributesDictionary;
        }

        private List<OMUnit> GetUnits(List<long> taskIds)
        {
            return OMUnit.Where(x => taskIds.Contains((long) x.TaskId) && 
                    x.PropertyType_Code == PropertyTypes.Stead &&
                    x.ObjectId != null)
                .Select(x => x.ObjectId)
                .Select(x => x.CadastralNumber)
                .Select(x => x.CadastralBlock)
                .Select(x => x.PropertyType_Code)
                .Select(x => x.Square)
                .Select(x => x.ParentGroup.GroupName)
                .Select(x => x.Upks)
                .Select(x => x.CadastralCost)
                .Execute();
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
                    operations[i].UnitInfo.CadastralNumber,
                    operations[i].UnitInfo.CadastralQuartal,
                    operations[i].UnitInfo.CadastralDistrict,
                    operations[i].UnitInfo.ObjectType == PropertyTypes.None
                        ? null
                        : operations[i].UnitInfo.ObjectType.GetEnumDescription(),
                    operations[i].UnitInfo.Square,
                    operations[i].RosreestrAttributes.ParcelName,
                    operations[i].RosreestrAttributes.Location,
                    operations[i].RosreestrAttributes.Address,
                    operations[i].RosreestrAttributes.FormationDate,
                    operations[i].RosreestrAttributes.ParcelCategory,
                    operations[i].RosreestrAttributes.TypeOfUseByDocuments,
                    operations[i].CustomAttributes.TypeOfUseByClassifier,
                    operations[i].CustomAttributes.InfoAboutExistenceOfOtherObjects,
                    operations[i].CustomAttributes.InfoSource,
                    operations[i].CustomAttributes.Segment,
                    operations[i].CustomAttributes.UsageTypeCode,
                    operations[i].CustomAttributes.UsageTypeName,
                    operations[i].CustomAttributes.UsageTypeCodeSource,
                    operations[i].UnitInfo.Group,
                    operations[i].UnitInfo.Upks,
                    operations[i].UnitInfo.CadastralCost);
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

        private class UnitInfo
        {
            public string CadastralNumber { get; set; }
            public string CadastralQuartal { get; set; }
            public string CadastralDistrict { get; set; }
            public PropertyTypes ObjectType { get; set; }
            public decimal? Square { get; set; }
            public string Group { get; set; }
            public decimal? Upks { get; set; }
            public decimal? CadastralCost { get; set; }
        }

        private class RosreestrAttributes
        {
            public string ParcelName { get; set; }
            public string Location { get; set; }
            public string Address { get; set; }
            public string FormationDate { get; set; }
            public string ParcelCategory { get; set; }
            public string TypeOfUseByDocuments { get; set; }
        }

        private class CustomAttributes
        {
            public string TypeOfUseByClassifier { get; set; }
            public string InfoAboutExistenceOfOtherObjects { get; set; }
            public string InfoSource { get; set; }
            public string Segment { get; set; }
            public string UsageTypeCode { get; set; }
            public string UsageTypeName { get; set; }
            public string UsageTypeCodeSource { get; set; }
        }

        private class ReportItem
        {
            public UnitInfo UnitInfo { get; set; }
            public RosreestrAttributes RosreestrAttributes { get; set; }
            public CustomAttributes CustomAttributes { get; set; }

            public ReportItem()
            {
                UnitInfo = new UnitInfo();
                RosreestrAttributes = new RosreestrAttributes();
                CustomAttributes = new CustomAttributes();
            }
        }

        #endregion
    }
}
