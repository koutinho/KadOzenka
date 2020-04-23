using Core.Shared.Extensions;
using KadOzenka.Dal.Correction.Dto.CorrectionSettings;
using ObjectModel.Directory.MarketObjects;
using ObjectModel.Market;

namespace KadOzenka.Dal.Correction
{
    public class CorrectionSettingsService
    {
        public CorrectionSettings GetCorrectionSettings(CorrectionTypes correctionType)
        {
            var omCorrectionSettings = OMCorrectionSettings.Where(x => x.CorrectionType_Code == correctionType)
                .SelectAll().ExecuteFirstOrDefault();

            return omCorrectionSettings == null
                ? new CorrectionSettings()
                : omCorrectionSettings.Settings.DeserializeFromXml<CorrectionSettings>();
        }

        public void SaveCorrectionSettings(CorrectionSettings settings, CorrectionTypes correctionType)
        {
            var omCorrectionSettings = OMCorrectionSettings.Where(x => x.CorrectionType_Code == correctionType)
                .SelectAll().ExecuteFirstOrDefault();

            if (omCorrectionSettings == null)
            {
                omCorrectionSettings = new OMCorrectionSettings
                {
                    CorrectionType_Code = correctionType,
                };
            }

            omCorrectionSettings.Settings = settings.SerializeToXml<CorrectionSettings>();
            omCorrectionSettings.Save();
        }
    }
}
