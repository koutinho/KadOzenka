using System.Collections.Generic;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.KO;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Гармонизация с использованием справочника ЦОД
    /// </summary>
    public class HarmonizationCOD : GbuObjectHarmonizationBase
    {
        protected override string ReportName => "Отчет гармонизации ЦОД";
        private HarmonizationCODSettings Setting { get; }
        private List<OMCodDictionary> CodDictionaryItems { get; }

        public HarmonizationCOD(HarmonizationCODSettings setting) : base(setting)
        {
            Setting = setting;

            CodDictionaryItems = new List<OMCodDictionary>();
            if (setting.IdCodJob != null)
                CodDictionaryItems = OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob).SelectAll().Execute();
        }


        protected override bool CopyLevelData(Item item, GbuObjectAttribute sourceAttribute)
        {
            var sourceAttributeValueInString = sourceAttribute.GetValueInString();
            if (string.IsNullOrWhiteSpace(sourceAttributeValueInString))
                return false;

            var resValue = string.Empty;
            var dictionaryRecord = CodDictionaryItems.Find(x => x.Value == sourceAttributeValueInString.ToUpper());
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

            SaveGbuAttribute(item, sourceAttribute.ChangeDocId, sourceAttribute.S, sourceAttribute.Ot,
                resValue, sourceAttribute.AttributeId);

            return true;
        }

        protected override void SaveFailResult(Item item)
        {
            var errorMessageForReport = "Установлено значение по умолчанию.";

            SaveGbuAttribute(item, Setting.IdDocument ?? -1, item.Date, item.Date, Setting.DefaultValue, null, errorMessageForReport);
        }
    }
}
