using ObjectModel.Gbu.Harmonization;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Простая гармонизация
    /// </summary>
    public class Harmonization : GbuObjectHarmonizationBase
    {
        protected override string ReportName => "Отчет гармонизации";
        private HarmonizationSettings Setting { get; }

        public Harmonization(HarmonizationSettings setting) : base(setting)
        {
            Setting = setting;
        }


        protected override bool CopyLevelData(Item item, GbuObjectAttribute sourceAttribute)
        {
            var sourceAttributeValueInString = sourceAttribute.GetValueInString();
            if (string.IsNullOrWhiteSpace(sourceAttributeValueInString))
                return false;

            SaveGbuAttribute(item, sourceAttribute.ChangeDocId, sourceAttribute.S, sourceAttribute.Ot,
                sourceAttributeValueInString, sourceAttribute.AttributeId);

            return true;
        }

        protected override void SaveFailResult(Item item)
        {
            var errorMessageForReport = "Не найдено значение.";

            SaveGbuAttribute(item, -1, item.Date, item.Date, null, null, errorMessageForReport);
        }
    }
}
