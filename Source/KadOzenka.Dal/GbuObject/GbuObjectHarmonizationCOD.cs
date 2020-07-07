using System;
using System.Collections.Generic;
using ObjectModel.Gbu.Harmonization;
using Core.SRD;
using ObjectModel.KO;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Гармонизация с использованием справочника ЦОД
    /// </summary>
    public class HarmonizationCOD : GbuObjectHarmonizationBase
    {
        public HarmonizationCODSettings Setting { get; set; }
        public List<OMCodDictionary> CodDictionaryItems { get; set; }

        public HarmonizationCOD(HarmonizationCODSettings setting) : base(setting)
        {
            Setting = setting;

            CodDictionaryItems = new List<OMCodDictionary>();
            if (setting.IdCodJob != null)
                CodDictionaryItems = OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob).SelectAll().Execute();
        }


        protected override bool CopyLevelData(Item item, long sourceAttributeId, long resultAttributeId, List<GbuObjectAttribute> allAttributes)
        {
            var sourceAttribute = allAttributes.Find(x => x.AttributeId == sourceAttributeId);
            if (sourceAttribute == null)
                return false; 

            var sourceAttributeValueInString = sourceAttribute.GetValueInString();
            if (string.IsNullOrEmpty(sourceAttributeValueInString))
                return false;

            var resValue = string.Empty;
            var dictionaryRecord = CodDictionaryItems.Find(x => x.Value == sourceAttributeValueInString);
            if (dictionaryRecord != null)
            {
                var code = dictionaryRecord.Code.Replace(" ", "");
                if (code != "0")
                {
                    resValue = code;
                }
            }
            if (resValue == string.Empty)
                return false;

            lock (locked)
            {
                var rowReport = ReportService.GetCurrentRow();
                AddRowToReport(rowReport, item.CadastralNumber, sourceAttributeId, resValue, resultAttributeId, string.Empty);
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
                StringValue = resValue
            }.Save();

            return true;
        }

        protected override void SaveFailResult(Item item)
        {
            lock (locked)
            {
                var rowReport = ReportService.GetCurrentRow();
                var message = "Для текущего объекта было установлено значение по умолчанию.";
                AddRowToReport(rowReport, item.CadastralNumber, 0, Setting.DefaultValue, Setting.IdAttributeResult.Value, message);
            }

            new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = Setting.IdAttributeResult.Value,
                ObjectId = item.ObjectId,
                ChangeDocId = Setting.IdDocument ?? -1,
                S = item.Date,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = item.Date,
                StringValue = Setting.DefaultValue
            }.Save();
        }
    }
}
