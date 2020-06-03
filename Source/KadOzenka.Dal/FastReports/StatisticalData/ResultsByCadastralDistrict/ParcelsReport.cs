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
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    //TODO
    public class ParcelsReport : StatisticalDataReport
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "ResultsByCadastralDistrictForParcelsReport";
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            var taskIds = GetTaskIdList(query)?.ToList();
            var operations = GetOperations(taskIds, 1, 1, 1, 1, 1, 1, 1);

            return new DataSet();
        }

        public override void InitializeFilterValues(long objId, string senderName, bool initialisation, List<FilterValue> filterValues)
        {
            if (!initialisation)
                return;

            //InitialiseGbuAttributesFilterValue(
            //    filterValues.FirstOrDefault(f => f.ParamName == "KlardAttribute"));
        }

        #region Support Methods

        private List<ReportItem> GetOperations(List<long> taskIds, long typeOfUseByClassifierAttributeId, long infoAboutExistenceOfOtherObjectsAttributeId, long infoSourceAttributeId, long segmentAttributeId, long usageTypeCodeAttributeId, long usageTypeNameAttributeId, long usageTypeCodeSourceAttributeId)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
            attributesDictionary.Add(nameof(RosreestrAttributes.ParcelName), StatisticalDataService.GetRosreestrParcelNameAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.Location), StatisticalDataService.GetRosreestrLocationAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.Address), StatisticalDataService.GetRosreestrAddressAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.FormationDate), StatisticalDataService.GetRosreestrFormationDateAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.ParcelCategory), StatisticalDataService.GetRosreestrParcelCategoryAttribute());
            attributesDictionary.Add(nameof(RosreestrAttributes.TypeOfUseByDocuments), StatisticalDataService.GetRosreestrTypeOfUseByDocumentsAttribute());

            attributesDictionary.Add(nameof(CustomAttributes.TypeOfUseByClassifier), RegisterCache.GetAttributeData(typeOfUseByClassifierAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.InfoAboutExistenceOfOtherObjects), RegisterCache.GetAttributeData(infoAboutExistenceOfOtherObjectsAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.InfoSource), RegisterCache.GetAttributeData(infoSourceAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.Segment), RegisterCache.GetAttributeData(segmentAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeCode), RegisterCache.GetAttributeData(usageTypeCodeAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeName), RegisterCache.GetAttributeData(usageTypeNameAttributeId));
            attributesDictionary.Add(nameof(CustomAttributes.UsageTypeCodeSource), RegisterCache.GetAttributeData(usageTypeCodeSourceAttributeId));

            var units = GetUnits(taskIds);

            var gbuAttributes = GbuObjectService.GetAllAttributes(
                units.Select(x => x.ObjectId.GetValueOrDefault()).Distinct().ToList(),
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay());

            var result = new List<ReportItem>();
            //TODO
            //units.ForEach(unit =>
            //{
            //    var unitInfo = new UnitInfo
            //    {
            //        ObjectId = unit.ObjectId,
            //        CadastralNumber = unit.CadastralNumber,
            //        CadastralQuartal = unit.CadastralBlock,
            //        CadastralDistrict = GetCadastralDistrict(unit.CadastralBlock),
            //        ObjectType = unit.PropertyType_Code,
            //        Square = unit.Square,
            //        Group = unit.ParentGroup?.GroupName,
            //        Upks = unit.Upks,
            //        CadastralCost = unit.CadastralCost
            //    };

            //    var item = new ReportItem
            //    {
            //        UnitInfo = unitInfo
            //    };

            //    var objectAttributes = gbuAttributes.Where(x => x.ObjectId == unit.ObjectId).ToList();
            //    foreach (var attribute in objectAttributes)
            //    {
            //        var attributeKeys = attributesDictionary.Where(x => x.Value.Id == attribute.AttributeId).Select(x => x.Key);
            //        foreach (var key in attributeKeys)
            //        {
            //            var a = typeof(ReportItem).GetProperty(key);
            //            var b = attribute.GetValueInString();
            //            typeof(ReportItem).GetProperty(key)?.SetValue(item, attribute.GetValueInString());
            //        }
            //    }

            //    result.Add(item);
            //});

            return result;
        }

        private List<OMUnit> GetUnits(List<long> taskIds)
        {
            return OMUnit.Where(x =>
                    taskIds.Contains((long) x.TaskId) && 
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

        #endregion

        #region Entities

        private class UnitInfo
        {
            public long? ObjectId { get; set; }
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
            public DateTime? FormationDate { get; set; }
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
        }

        #endregion
    }
}
