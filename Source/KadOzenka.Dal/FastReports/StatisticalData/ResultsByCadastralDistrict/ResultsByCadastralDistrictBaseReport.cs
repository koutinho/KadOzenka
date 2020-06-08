using System.Collections.Generic;
using Core.Register.RegisterEntities;
using System.Linq;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Directory;
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
