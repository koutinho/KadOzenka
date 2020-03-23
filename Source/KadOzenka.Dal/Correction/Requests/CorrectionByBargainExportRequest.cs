using ObjectModel.Directory;

namespace KadOzenka.Dal.Correction.Requests
{
    public class CorrectionByBargainExportRequest: CorrectionByBargainRequest
    {
        public Hunteds? Distinct { get; set; }
        public Districts? Region { get; set; }
        public long? Zone { get; set; }
        public string CadastralQuarter { get; set; }
    }
}
