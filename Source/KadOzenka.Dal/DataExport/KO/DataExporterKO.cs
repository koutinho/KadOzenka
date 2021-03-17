using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using KadOzenka.Dal.Modeling;

namespace KadOzenka.Dal.DataExport
{
    public static class DataExporterKO
    {
        /// <summary>
        /// Выгрузка значений меток по фактору и группе в формате Excel
        /// </summary>
        public static Stream ExportMarkerToExcel(long groupId, long factorId)
        {
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Метки");

            DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Значение фактора", "Метка" });

            List<ObjectModel.KO.OMMarkCatalog> objs = ObjectModel.KO.OMMarkCatalog.Where(x => x.GroupId == groupId && x.FactorId == factorId).SelectAll().Execute();
            int curIndex = 0;
            if (objs.Count > 0)
            {
                CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                ParallelOptions options = new ParallelOptions
                {
                    CancellationToken = cancelTokenSource.Token,
                    MaxDegreeOfParallelism = 20
                };

                object locked = new object();
                List<List<object>> values = new List<List<object>>();

                Parallel.ForEach(objs, options, obj =>
                {
                    curIndex++;
                    if (curIndex % 40 == 0) Console.WriteLine(curIndex);

                    #region Заголовок объекта
                    List<object> value = new List<object>();
                    value.Add(obj.ValueFactor);
                    value.Add(obj.MetkaFactor);
                    #endregion

                    lock (locked)
                    {
                        values.Add(value);
                    }
                });

                int row = 1;
                foreach (List<object> value in values)
                {
                    DataExportCommon.AddRow(mainWorkSheet, row, value.ToArray());
                    row++;
                }
                Console.WriteLine(values.Count);
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        /// <summary>
        /// Выгрузка списка значений по фактору и группе в формате Excel
        /// </summary>
        public static Stream ExportMarkerListToExcel(long groupId, long factorId)
        {
            ExcelFile excelTemplate = new ExcelFile();

            var mainWorkSheet = excelTemplate.Worksheets.Add("Метки");

            DataExportCommon.AddRow(mainWorkSheet, 0, new object[] { "Значение фактора", "Метка" });

            //List<ObjectModel.KO.OMUnit> units = ObjectModel.KO.OMUnit.Where(x => x.GroupId == groupId).SelectAll().Execute();
            //if (units.Count > 0)
            //{
            //CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            //ParallelOptions options = new ParallelOptions
            //{
            //    CancellationToken = cancelTokenSource.Token,
            //    MaxDegreeOfParallelism = 20
            //};

            //object locked = new object();
            //List<List<object>> values = new List<List<object>>();
            //List<string> fvalues = new List<string>();
            //int curIndex = 0;
            //Parallel.ForEach(units, options, unit =>
            //{
            //    curIndex++;
            //    if (curIndex % 40 == 0) Console.WriteLine(curIndex);

            //    DataTable data = RegisterStorage.GetAttribute((int)unit.Id, OMGroup.GetFactorReestrId(unit), (int)factorId);
            //    string curValue = string.Empty;
            //    if (data != null)
            //    {
            //        if (data.Rows.Count > 0)
            //        {
            //            curValue = data.Rows[0].ItemArray[6].ParseToString();
            //        }
            //    }


            //    if (curValue != string.Empty)
            //    {
            //        lock (locked)
            //        {
            //            if (!fvalues.Contains(curValue))
            //                fvalues.Add(curValue);
            //        }
            //    }

            //});

            //int row = 1;
            //List<ObjectModel.KO.OMMarkCatalog> markers = ObjectModel.KO.OMMarkCatalog.Where(x => x.GroupId == groupId && x.FactorId == factorId).SelectAll().Execute();

            //foreach (string value in fvalues)
            //{
            //    string mvalue = string.Empty;
            //    ObjectModel.KO.OMMarkCatalog marker = markers.Find(x => x.ValueFactor.ToUpper() == value.ToUpper());
            //    if (marker != null)
            //    {
            //        mvalue = (marker.MetkaFactor == null) ? string.Empty : marker.MetkaFactor.ParseToString();
            //    }
            //    DataExportCommon.AddRow(mainWorkSheet, row, new object[] { value, mvalue });
            //    row++;
            //}
            //Console.WriteLine(fvalues.Count);
            //}

            var row = 1;
            var markers = new ModelFactorsService().GetMarks(groupId, factorId);

            foreach (var marker in markers)
            {
                var factor = marker.ValueFactor == null ? string.Empty : marker.ValueFactor;
                var metka = marker.MetkaFactor == null ? string.Empty : marker.MetkaFactor.ParseToString();

                DataExportCommon.AddRow(mainWorkSheet, row, new object[] { factor, metka });
                row++;
            }

            MemoryStream stream = new MemoryStream();
            excelTemplate.Save(stream, GemBox.Spreadsheet.SaveOptions.XlsxDefault);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}