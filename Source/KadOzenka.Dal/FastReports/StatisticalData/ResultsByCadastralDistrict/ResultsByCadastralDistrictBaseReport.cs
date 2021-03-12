using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using Core.Register.RegisterEntities;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.FastReports.StatisticalData.Common;
using KadOzenka.Dal.GbuObject.Entities;
using ObjectModel.Directory;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.FastReports.StatisticalData.ResultsByCadastralDistrict
{
    public class ResultsByCadastralDistrictBaseReport : StatisticalDataReport
    {
        protected readonly string BaseFolderWithSql = "ResultsByCadastralDistrict";

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
                    purposeAttribute = RosreestrRegisterService.GetBuildingPurposeAttribute();
                    break;
                case PropertyTypes.Construction:
                    purposeAttribute = RosreestrRegisterService.GetConstructionPurposeAttribute();
                    break;
            }
            if (purposeAttribute != null)
            {
                attributesDictionary.Add(nameof(IParentInfo.ParentPurpose), purposeAttribute);
            }

            var parentAttributes = GbuObjectService.GetAllAttributes(obj.Id,
                attributesDictionary.Values.Select(x => (long)x.RegisterId).Distinct().ToList(),
                attributesDictionary.Values.Select(x => x.Id).Distinct().ToList(),
                DateTime.Now.GetEndOfTheDay(),
                attributesToDownload: new List<GbuColumnsToDownload> { GbuColumnsToDownload.Value });

            SetAttributes(obj.Id, parentAttributes, attributesDictionary, item);
        }

        protected string GetSqlFileContent(string fileName)
        {
            return StatisticalDataService.GetSqlFileContent(BaseFolderWithSql, fileName);
        }

        protected override DataSet GetReportData(NameValueCollection query, HashSet<long> objectList = null)
        {
	        return new DataSet();
        }
    }
}
