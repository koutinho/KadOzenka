using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalReportModel
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public static EditApprovalReportModel FromEntity(List<OMParam> param)
        {
            return new EditApprovalReportModel
            {
                Number = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Number.GetEnumDescription())?.Pid.ToString(),
            };
        }
    }
}