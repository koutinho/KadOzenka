using System.Collections.Generic;
using Core.SRD;
using ObjectModel.Common;
using ObjectModel.Directory.Common;

namespace KadOzenka.Dal.CommonFunctions
{
    public class TemplateService
    {
        public List<OMDataFormStorage> GetTemplates(DataFormStorege formType)
        {
            return OMDataFormStorage
                .Where(x => x.UserId == SRDSession.GetCurrentUserId().Value && x.FormType_Code == formType).SelectAll()
                .Execute();
        }
    }
}
