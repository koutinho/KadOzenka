using System;
using System.Collections.Generic;
using Core.Register.RegisterEntities;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ResultsByCadastralDistrictBaseReport : StatisticalDataReport
    {
        protected class InfoFromSettings
        {
            public string ObjectType { get; set; }
            public string CadastralQuartal { get; set; }
            public string SubGroupNumber { get; set; }
        }

        protected interface ParentInfo
        {
             string ParentPurpose { get; set; }
             string ParentGroup { get; set; }
        }

        protected Dictionary<string, RegisterAttribute> GetGeneralAttributesForReport(long tourId)
        {
            var attributesDictionary = new Dictionary<string, RegisterAttribute>();
            attributesDictionary.Add(nameof(InfoFromSettings.ObjectType), StatisticalDataService.GetObjectTypeAttributeFromTourSettings(tourId));
            attributesDictionary.Add(nameof(InfoFromSettings.CadastralQuartal), StatisticalDataService.GetCadastralQuartalAttributeFromTourSettings(tourId));
            attributesDictionary.Add(nameof(InfoFromSettings.SubGroupNumber), StatisticalDataService.GetGroupAttributeFromTourSettings(tourId));
           
            return attributesDictionary;
        }

        protected static void SetAttributes(long? objectId, List<GbuObjectAttribute> gbuAttributes,
            Dictionary<string, RegisterAttribute> attributesDictionary, object item)
        {
            var objectAttributes = gbuAttributes.Where(x => x.ObjectId == objectId).ToList();
            foreach (var objectAttribute in objectAttributes)
            {
                var attributeKeys = attributesDictionary.Where(x => x.Value.Id == objectAttribute.AttributeId).Select(x => x.Key);
                foreach (var key in attributeKeys)
                {
                    item.GetType().GetProperty(key)?.SetValue(item, objectAttribute.GetValueInString());
                }
            }
        }

        protected void SetParentObjectAttributes(long tourId, string parentCadastralNumber, ParentInfo item)
        {
            if (string.IsNullOrWhiteSpace(parentCadastralNumber))
                return;

            var obj = OMMainObject.Where(x => x.CadastralNumber == parentCadastralNumber)
                .Select(x => x.ObjectType_Code)
                .ExecuteFirstOrDefault();
            if (obj == null)
                return;

            var groupAttribute = StatisticalDataService.GetGroupAttributeFromTourSettings(tourId);
            var attributesDictionary = new Dictionary<string, RegisterAttribute>
            {
                {nameof(ParentInfo.ParentGroup), groupAttribute}
            };

            RegisterAttribute purposeAttribute = null;
            switch (obj.ObjectType_Code)
            {
                case PropertyTypes.Building:
                    purposeAttribute = StatisticalDataService.GetRosreestrBuildingPurposeAttribute();
                    break;
                case PropertyTypes.Construction:
                    purposeAttribute = StatisticalDataService.GetRosreestrConstructionPurposeAttribute();
                    break;
            }
            if (purposeAttribute != null)
            {
                attributesDictionary.Add(nameof(ParentInfo.ParentPurpose), purposeAttribute);
            }

            var parentAttributes = GbuObjectService.GetAllAttributes(obj.Id,
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay());

            SetAttributes(obj.Id, parentAttributes, attributesDictionary, item);
        }

        protected List<OMUnit> GetUnits(List<long> taskIds, PropertyTypes type)
        {
            return OMUnit.Where(x => taskIds.Contains((long)x.TaskId) &&
                                     x.PropertyType_Code == type &&
                                     x.ObjectId != null)
                .SelectAll()
                .Execute();
        }
    }
}
