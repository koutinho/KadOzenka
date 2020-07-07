using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Gbu.Harmonization;
using Core.SRD;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Простая гармонизация
    /// </summary>
    public class Harmonization : GbuObjectHarmonizationBase
    {
        public HarmonizationSettings Setting { get; set; }

        public Harmonization(HarmonizationSettings setting) : base(setting)
        {
            Setting = setting;
        }


        protected override bool CopyLevelData(Item item, long sourceAttributeId, long resultAttributeId, List<GbuObjectAttribute> allAttributes)
        {
            var sourceAttribute = allAttributes.FirstOrDefault(x => x.AttributeId == sourceAttributeId);
            if (sourceAttribute == null)
                return false;

            var sourceAttributeValueInString = sourceAttribute.GetValueInString();
            if (string.IsNullOrEmpty(sourceAttributeValueInString))
                return false;

            lock (locked)
            {
                var rowReport = ReportService.GetCurrentRow();
                AddRowToReport(rowReport, item.CadastralNumber, sourceAttributeId, sourceAttributeValueInString, resultAttributeId, string.Empty);
            }

            new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = resultAttributeId,
                ObjectId = item.ObjectId,
                ChangeDocId = sourceAttribute.ChangeDocId,
                S = sourceAttribute.S,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = sourceAttribute.Ot,
                StringValue = sourceAttributeValueInString
            }.Save();

            return true;
        }

        protected override void SaveFailResult(Item item)
        {
            lock (locked)
            {
                var rowReport = ReportService.GetCurrentRow();
                var message = "Для текущего объекта не было записанно значение, т.к не было найдено.";
                AddRowToReport(rowReport, item.CadastralNumber, 0, string.Empty, Setting.IdAttributeResult.Value, message);
            }

            new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = Setting.IdAttributeResult.Value,
                ObjectId = item.ObjectId,
                ChangeDocId = -1,
                S = item.Date,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = item.Date
            }.Save();
        }
    }
}
