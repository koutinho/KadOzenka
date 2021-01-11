﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using KadOzenka.Dal.DataImport;
using Newtonsoft.Json;
using ObjectModel.Gbu.CodSelection;
using ObjectModel.Core.TD;
using Serilog;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Выборка из справочника ЦОД
    /// </summary>
    public class SelectionCOD
    {
	    private static readonly ILogger _log = Log.ForContext<SelectionCOD>();

        /// <summary>
        /// Объект для блокировки счетчика в многопоточке
        /// </summary>
        private static object locked;
        /// <summary>
        /// Общее число объектов
        /// </summary>
        public static int MaxCount = 0;
        /// <summary>
        /// Индекс текущего объекта
        /// </summary>
        public static int CurrentCount = 0;

        #region columns for report

		/// <summary>
		/// Номер столбца кад номера для записи в отчет
		/// </summary>
		private static readonly int knColumn = 0;

		private static readonly int inputFieldColumn = 1;

		private static readonly int valueColumn = 2;

		private static readonly int outputFieldColumn = 3;

		private static readonly int errorColumn = 4;

		#endregion


		/// <summary>
		/// Выборка из справочника ЦОД по кадастровому номеру
		/// </summary>
		public static long Run(CodSelectionSettings setting)
        {
	        _log.ForContext("InputParameters", JsonConvert.SerializeObject(setting)).Debug("Входные данные для Выборки из справочника ЦОД");

            using var reportService = new GbuReportService("Отчет выборки из справочника ЦОД по кадастровому номеру");
            reportService.AddHeaders(new List<string> { "КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения", "Ошибка" });
            reportService.SetIndividualWidth(inputFieldColumn, 6);
            reportService.SetIndividualWidth(knColumn, 4);
            reportService.SetIndividualWidth(valueColumn, 3);
            reportService.SetIndividualWidth(outputFieldColumn, 6);
            reportService.SetIndividualWidth(errorColumn, 5);

            locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };

            List<ObjectModel.KO.OMCodDictionary> DictionaryItem = new List<ObjectModel.KO.OMCodDictionary>();
            if (setting.IdCodJob != null)
                DictionaryItem = ObjectModel.KO.OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob).SelectAll().Execute();
            _log.Debug("Найдено {CodItemsCount} значений из справочника ЦОС с ИД {CodId}", DictionaryItem.Count, setting.IdCodJob);

            List<ObjectModel.Gbu.OMMainObject> Objs = new List<ObjectModel.Gbu.OMMainObject>();
            foreach (ObjectModel.KO.OMCodDictionary item in DictionaryItem)
            {
                ObjectModel.Gbu.OMMainObject obj = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == setting.PropertyType && x.IsActive == true && x.CadastralNumber == item.Value).SelectAll().ExecuteFirstOrDefault();
                if (obj != null) Objs.Add(obj);
            }

            long idDoc = (setting.IdDocument == null) ? -1 : setting.IdDocument.Value;
            OMInstance doc = OMInstance.Where(x => x.Id == idDoc).SelectAll().ExecuteFirstOrDefault();

            DateTime dt = (doc == null) ? DateTime.Now : doc.CreateDate;
            _log.ForContext("DateForNewAttributes", dt).Debug("Дата, с которой будут сохранены новые атрибуты");

            MaxCount = Objs.Count;
            _log.Debug("Найдено {Count} Единиц оценки", MaxCount);
            CurrentCount = 0;
            Parallel.ForEach(Objs, options, item => { RunOneGbu(reportService, item, setting, DictionaryItem, dt); });
            CurrentCount = 0;
            MaxCount = 0;


            long reportId = reportService.SaveReport();

            _log.Debug("Закончена операция Выборки из справочника ЦОД");

            return reportId;
        }

        public static void RunOneGbu(GbuReportService reportService, ObjectModel.Gbu.OMMainObject obj, CodSelectionSettings setting, List<ObjectModel.KO.OMCodDictionary> dictionaryItem, DateTime dt)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            ObjectModel.KO.OMCodDictionary item = ObjectModel.KO.OMCodDictionary.Where(x => x.Value == obj.CadastralNumber).SelectAll().ExecuteFirstOrDefault();
            string value = string.Empty;
            if (item != null) value = item.Code;

            lock (locked)
            {
                var rowReport = reportService.GetCurrentRow();
                AddRowToReport(reportService, rowReport, obj.CadastralNumber, value, setting.IdAttributeResult.Value);
            }

            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = setting.IdAttributeResult.Value,
                ObjectId = obj.Id,
                ChangeDocId = (setting.IdDocument == null) ? -1 : setting.IdDocument.Value,
                S = dt,
                ChangeUserId = SRDSession.Current.UserID,
                ChangeDate = DateTime.Now,
                Ot = dt,
                StringValue = value
            };
            DataImporterGkn.SaveAttributeValueWithCheck(attributeValue);
        }

        public static void AddRowToReport(GbuReportService reportService, GbuReportService.Row rowNumber, string kn, string value, long resultAttribute, string errorMessage = "", string sourceName = "Справочник ЦОД")
        {
	        string resultName = GbuObjectService.GetAttributeNameById(resultAttribute);
	        reportService.AddValue(kn, knColumn, rowNumber);
	        reportService.AddValue(sourceName, inputFieldColumn, rowNumber);
	        reportService.AddValue(value, valueColumn, rowNumber);
	        reportService.AddValue(resultName, outputFieldColumn, rowNumber);
	        reportService.AddValue(errorMessage, errorColumn, rowNumber);
        }

	}

}
