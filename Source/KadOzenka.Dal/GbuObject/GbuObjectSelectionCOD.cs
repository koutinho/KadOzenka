using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using ObjectModel.Gbu.CodSelection;
using ObjectModel.Core.TD;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Выборка из справочника ЦОД
    /// </summary>
    public class SelectionCOD
    {
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

        private static GbuReportService _reportService;

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
            _reportService = new GbuReportService();

            _reportService.AddHeaders(0, new List<string> { "КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения", "Ошибка" });

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
            List<ObjectModel.Gbu.OMMainObject> Objs = new List<ObjectModel.Gbu.OMMainObject>();
            foreach (ObjectModel.KO.OMCodDictionary item in DictionaryItem)
            {
                ObjectModel.Gbu.OMMainObject obj = ObjectModel.Gbu.OMMainObject.Where(x => x.ObjectType_Code == setting.PropertyType && x.IsActive == true && x.CadastralNumber == item.Code).SelectAll().ExecuteFirstOrDefault();
                if (obj != null) Objs.Add(obj);
            }

            long idDoc = (setting.IdDocument == null) ? -1 : setting.IdDocument.Value;
            OMInstance doc = OMInstance.Where(x => x.Id == idDoc).SelectAll().ExecuteFirstOrDefault();

            DateTime dt = (doc == null) ? DateTime.Now : doc.CreateDate;

            MaxCount = Objs.Count;
            CurrentCount = 0;
            Parallel.ForEach(Objs, options, item => { RunOneGbu(item, setting, DictionaryItem, dt); });
            CurrentCount = 0;
            MaxCount = 0;

            _reportService.SetStyle();
            _reportService.SetIndividualWidth(inputFieldColumn, 6);
            _reportService.SetIndividualWidth(knColumn, 4);
            _reportService.SetIndividualWidth(valueColumn, 3);
            _reportService.SetIndividualWidth(outputFieldColumn, 6);
            _reportService.SetIndividualWidth(errorColumn, 5);
            long reportId = _reportService.SaveReport("Отчет выборки из справочника ЦОД по кадастровому номеру");
            _reportService = null;

            return reportId;

        }

        public static void RunOneGbu(ObjectModel.Gbu.OMMainObject obj, CodSelectionSettings setting, List<ObjectModel.KO.OMCodDictionary> dictionaryItem, DateTime dt)
        {
            lock (locked)
            {
                CurrentCount++;
            }

            ObjectModel.KO.OMCodDictionary item = ObjectModel.KO.OMCodDictionary.Where(x => x.Code == obj.CadastralNumber).SelectAll().ExecuteFirstOrDefault();
            string value = string.Empty;
            if (item != null) value = item.Value;


            int rowReport;

            lock (locked)
            {
                rowReport = _reportService.GetCurrentRow();
            }

            AddRowToReport(rowReport, obj.CadastralNumber, value, setting.IdAttributeResult.Value);

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
            attributeValue.Save();

        }

        public static void AddRowToReport(int rowNumber, string kn, string value, long resultAttribute, string errorMessage = "", string sourceName = "Справочник ЦОД")
        {
	        string resultName = GbuObjectService.GetAttributeNameById(resultAttribute);
	        _reportService.AddValue(kn, knColumn, rowNumber);
	        _reportService.AddValue(sourceName, inputFieldColumn, rowNumber);
	        _reportService.AddValue(value, valueColumn, rowNumber);
	        _reportService.AddValue(resultName, outputFieldColumn, rowNumber);
	        _reportService.AddValue(errorMessage, errorColumn, rowNumber);
        }

	}

}
