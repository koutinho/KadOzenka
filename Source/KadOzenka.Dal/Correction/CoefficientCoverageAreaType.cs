using System.ComponentModel;
using DocumentFormat.OpenXml.Wordprocessing;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    /// <summary>
    /// Охват коэффициента
    /// </summary>
    public enum CoefficientCoverageAreaType
    {
        [Description("По всему городу")]
        City,

        [Description("По административному округу")]
        [CoveragePropertyName(nameof(OMCoreObject.District_Code))]
        District,

        [Description("По району")]
        [CoveragePropertyName(nameof(OMCoreObject.Neighborhood_Code))]
        Region,

        [Description("По зоне")]
        [CoveragePropertyName(nameof(OMCoreObject.Zone))]
        Zone,

        /// <summary>
        /// По кварталу
        /// </summary>
        [Description("По кадастровому кварталу")]
        [CoveragePropertyName(nameof(OMCoreObject.CadastralQuartal))]
        CadastralQuarter
    }
}
