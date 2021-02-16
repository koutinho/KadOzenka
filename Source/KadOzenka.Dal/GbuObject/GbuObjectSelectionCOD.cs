using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Register.QuerySubsystem;
using Core.SRD;
using KadOzenka.Dal.DataImport;
using Newtonsoft.Json;
using ObjectModel.Gbu.CodSelection;
using ObjectModel.Core.TD;
using ObjectModel.Gbu;
using ObjectModel.KO;
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

		private static readonly int resultAttributeColumn = 1;

		private static readonly int valueColumn = 2;

		#endregion


		/// <summary>
		/// Выборка из справочника ЦОД по кадастровому номеру
		/// </summary>
		public static string Run(CodSelectionSettings setting)
        {
	        _log.ForContext("InputParameters", JsonConvert.SerializeObject(setting)).Debug("Входные данные для Выборки из справочника ЦОД");

            using var reportService = new GbuReportService("Отчет выборки из справочника ЦОД по кадастровому номеру");
            reportService.AddHeaders(new List<string> { "КН", "Поле в которое производилась запись", "Внесенное значение" });
            reportService.SetIndividualWidth(resultAttributeColumn, 6);
            reportService.SetIndividualWidth(knColumn, 4);
            reportService.SetIndividualWidth(valueColumn, 3);

            locked = new object();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };
            var dictionaryItems = OMCodDictionary.Where(x => x.IdCodjob == setting.IdCodJob)
	            .Select(x => new{x.Code, x.Value})
	            .Execute();
            _log.Debug("Найдено {CodItemsCount} значений из справочника ЦОС с ИД {CodId}", dictionaryItems.Count, setting.IdCodJob);

            var gbuObjects = OMMainObject
	            .Where(x => x.ObjectType_Code == setting.PropertyType && x.IsActive == true)
	            .Join(OMCodDictionary.GetRegisterId(), new QSConditionGroup
	            {
		            Type = QSConditionGroupType.And,
		            Conditions = new List<QSCondition>
		            {
			            new QSConditionSimple(OMCodDictionary.GetColumn(x => x.Value), QSConditionType.Equal,
				            OMMainObject.GetColumn(x => x.CadastralNumber)),
			            new QSConditionSimple(OMCodDictionary.GetColumn(x => x.IdCodjob), QSConditionType.Equal,
				            setting.IdCodJob.GetValueOrDefault()),
		            }
	            })
	            .Select(x => x.CadastralNumber)
	            .Execute();
            _log.Debug("Найдено {GbuObjectsCount} Гбу объектов", gbuObjects.Count);

            long idDoc = setting.IdDocument ?? -1;
            OMInstance doc = OMInstance.Where(x => x.Id == idDoc).SelectAll().ExecuteFirstOrDefault();

            DateTime dt = (doc == null) ? DateTime.Now : doc.CreateDate;
            _log.ForContext("DateForNewAttributes", dt).Debug("Дата, с которой будут сохранены новые атрибуты");

            MaxCount = gbuObjects.Count;
            CurrentCount = 0;
            Parallel.ForEach(gbuObjects, options, gbuObject => { RunOneGbu(reportService, gbuObject, setting, dictionaryItems, dt); });
            CurrentCount = 0;
            MaxCount = 0;


            var reportId = reportService.SaveReport();

            _log.Debug("Закончена операция Выборки из справочника ЦОД");

            return reportService.GetUrlToDownloadFile(reportId);
        }

        public static void RunOneGbu(GbuReportService reportService, OMMainObject obj, CodSelectionSettings setting, List<OMCodDictionary> dictionaryItems, DateTime dt)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            OMCodDictionary item = dictionaryItems.FirstOrDefault(x => x.Value == obj.CadastralNumber);
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
            GbuObjectService.SaveAttributeValueWithCheck(attributeValue);
        }

        public static void AddRowToReport(GbuReportService reportService, GbuReportService.Row rowNumber, string kn, string value, long resultAttribute)
        {
	        string resultAttributeName = GbuObjectService.GetAttributeNameById(resultAttribute);
	        reportService.AddValue(kn, knColumn, rowNumber);
	        reportService.AddValue(resultAttributeName, resultAttributeColumn, rowNumber);
	        reportService.AddValue(value, valueColumn, rowNumber);
        }

	}

}
