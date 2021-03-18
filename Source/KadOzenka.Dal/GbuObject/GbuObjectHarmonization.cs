using KadOzenka.Dal.GbuObject.Dto;
using Serilog;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Простая гармонизация
    /// </summary>
    public class Harmonization : GbuObjectHarmonizationBase
    {
        protected override string ReportName => "Отчет гармонизации";

        public Harmonization(HarmonizationSettings setting, long? userId, ILogger logger) : base(setting, userId, logger)
        {
        }


        protected override bool CopyLevelData(Item item, GbuObjectAttribute sourceAttribute, GbuReportService reportService)
        {
            var sourceAttributeValueInString = sourceAttribute.GetValueInString();
            if (string.IsNullOrWhiteSpace(sourceAttributeValueInString))
                return false;

            SaveGbuAttribute(item, sourceAttribute.ChangeDocId, sourceAttribute.S, sourceAttribute.Ot,
                sourceAttributeValueInString, sourceAttribute.AttributeId, reportService);

            return true;
        }

        protected override void SaveFailResult(Item item, GbuReportService reportService)
        {
            var errorMessageForReport = "Не найдено значение.";

            SaveGbuAttribute(item, -1, item.Date, item.Date, null, null, reportService, errorMessageForReport);
        }
    }
}
