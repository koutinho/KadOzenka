using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using DevExpress.DataAccess.Native.Sql.ConnectionProviders;
using KadOzenka.Dal.Enum;
using ObjectModel.Sud;

namespace KadOzenka.Web.Models.Sud
{
    public class EditApprovalReportLinkModel
    {
        public long Id { get; set; }

        public string IdReport { get; set; }
        public string Rs { get; set; }
        public EditApprovalReportModel Report { get; set; }

        public static EditApprovalReportLinkModel FromEntity(List<OMParam> param)
        {
            return new EditApprovalReportLinkModel
            {
                IdReport = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.IdReport.GetEnumDescription())?.Pid.ToString(),
                Rs = param.FirstOrDefault(x => x.ParamName == ParamNameEnum.Rs.GetEnumDescription())?.Pid.ToString(),
                Report = EditApprovalReportModel.FromEntity(param)
            };
        }
    }
}