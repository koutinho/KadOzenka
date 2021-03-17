﻿using System.Collections.Generic;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.KO;
using Serilog;

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

        public HarmonizationCOD(HarmonizationCODSettings setting, long? userId, ILogger logger) : base(setting, userId, logger)
        {
            Setting = setting;

            CodDictionaryItems = new List<OMCodDictionary>();
            if (setting.IdCodJob != null)
                CodDictionaryItems = OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob).SelectAll().Execute();
        }


        protected override bool CopyLevelData(Item item, GbuObjectAttribute sourceAttribute, GbuReportService reportService)
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
                resValue, sourceAttribute.AttributeId, reportService);

            return true;
        }

        protected override void SaveFailResult(Item item, GbuReportService reportService)
        {
            var errorMessageForReport = "Установлено значение по умолчанию.";

            SaveGbuAttribute(item, Setting.IdDocument ?? -1, item.Date, item.Date, Setting.DefaultValue, null, reportService, errorMessageForReport);
        }
    }
}
