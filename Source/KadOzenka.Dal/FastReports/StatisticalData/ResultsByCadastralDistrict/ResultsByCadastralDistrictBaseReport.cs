using System;
using System.Collections.Generic;
using Core.Register.RegisterEntities;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using ObjectModel.Directory;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ResultsByCadastralDistrictBaseReport : StatisticalDataReport
    {
        protected interface IParentInfo
        {
            string ParentPurpose { get; set; }
            string ParentGroup { get; set; }
        }

        protected void SetParentObjectAttributes(long tourId, string parentCadastralNumber, IParentInfo item)
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
                {nameof(IParentInfo.ParentGroup), groupAttribute}
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
                attributesDictionary.Add(nameof(IParentInfo.ParentPurpose), purposeAttribute);
            }

            var parentAttributes = GbuObjectService.GetAllAttributes(obj.Id,
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay());

            SetAttributes(obj.Id, parentAttributes, attributesDictionary, item);
        }
    }
}
