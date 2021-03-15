using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataExport
{
    /// <summary>
    /// Класс разнообразной выгрузки в Excel результатов Кадастровой оценки.
    /// </summary>
    public class DEKODifferent : IKoUnloadResult
    {
        /// <summary>
        /// Выгрузка Таблица 4. Группировка объектов недвижимости
        /// </summary>
        /// <param name="setting"></param>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable04)]
        public static List<ResultKoUnloadSettings> ExportToXls4(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Группировка объектов недвижимости";
            var taskCounter = 0;
            var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var res = new List<ResultKoUnloadSettings>();
            foreach (long taskId in setting.TaskFilter)
            {
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId && x.CadastralCost > 0).OrderBy(x => x.CadastralNumber).SelectAll().Execute();
                if (units_all.Count == 0) return res;

                List<OMUnit> units_curr = new List<OMUnit>();
                int num_pp = 0;
                int count_curr = 0;
                int count_file = 0;
                string message = "";
                int count_all = units_all.Count();
                string cad_num_curr = "";
                string cad_num = units_all[0].CadastralNumber.Substring(0, 5); // Номер кадастрового района первого объекта

                foreach (OMUnit unit in units_all)
                {
                    cad_num_curr = unit.CadastralNumber.Substring(0, 5);
                    if (cad_num_curr != cad_num)
                    {  // Если начались объекты из другого кадастрового района, то предыдущие сохраняем в файл 

                        count_file++;
                        var fileResult = SaveExcel4(unloadResultQueue.Id, units_curr, ref num_pp, count_file, cad_num, setting.DirectoryName,
                            taskId, message);
                        res.Add(fileResult);
                        units_curr.Clear();
                        cad_num = cad_num_curr;
                    }
                    units_curr.Add(unit);

                    progress = (count_curr * 100 / count_all + taskCounter * 100) / setting.TaskFilter.Count;
                    setProgress(progress, progressMessage: progressMessage);
                    KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }

                if (units_curr.Count > 0)
                {
                    count_file++;
                    var fileResult = SaveExcel4(unloadResultQueue.Id, units_curr, ref num_pp, count_file, cad_num_curr, setting.DirectoryName, taskId, message);
                    res.Add(fileResult);
                }

                taskCounter++;
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel4(long unloadId, List<OMUnit> _units_curr, ref int _num_pp, int _count_file,
            string _cad_num, string _dir_name, long _taskid, string _mess)
        {
            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 3;
            int count_cells = 0;
            HeaderExcel4(sheet_edit, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            for (int i = 0; i < _units_curr.Count; i++)
            {
                if (_units_curr[i].GroupId != -1)
                {
                    OMGroup group = OMGroup.Where(x => x.Id == _units_curr[i].GroupId).SelectAll().ExecuteFirstOrDefault();

                    _num_pp++;
                    List<object> objarrs = new List<object>();
                    objarrs.Add(_num_pp);
                    objarrs.Add(_units_curr[i].PropertyType);
                    objarrs.Add(_units_curr[i].CadastralNumber);
                    objarrs.Add(group.Number);
                    objarrs.Add(group.GroupAlgoritm);

                    for (int f = 0; f < count_cells; f++)
                    {
                        objvals[curindval, f] = objarrs[f];
                    }

                    if (curindval >= 99)
                    {
                        DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                        curindval = -1;
                        objvals = new object[100, count_cells];
                    }
                    curindval++;
                    start_rows++;
                }
            }
            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            string file_name = "Task_" + _taskid + "_Таблица 4.Группировка объектов недвижимости"
                               + " " + _cad_num.Replace(":", "_")
                               + "." + _count_file.ToString().PadLeft(5, '0');

            long id =  SaveUnloadResult.SaveResult(file_name, excel_edit, unloadId, KoUnloadResultType.UnloadTable04);
            
            //excel_edit.Save(_dir_name + "\\" + file_name + ".xlsx"); //temp
            
            return new ResultKoUnloadSettings
            {
                FileId = id,
                FileName = file_name,
                TaskId = _taskid
            };
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 4
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel4(ExcelWorksheet _sheet, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№ п/п");
            objcaps1.Add("Тип");
            objcaps1.Add("Кадастровый номер");
            objcaps1.Add("Номер подгруппы");
            objcaps1.Add("Метод расчета");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");
            objcaps2.Add("4");
            objcaps2.Add("5");

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(50 * 256);
            widths_cells.Add(50 * 256);

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Таблица 4. Группировка объектов недвижимости");
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[1].Height = 4 * 256;
        }

        /// <summary>
        /// Выгрузка  Таблица 5. Модельная стоимость и Таблица 5. Метод УПКС
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable05)]
        public static List<ResultKoUnloadSettings> ExportToXls5(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Модельная стоимость и Метод УПКС";
            var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var res = new List<ResultKoUnloadSettings>();
            // Выбираем все группы
            List<OMGroup> groups = OMGroup.Where(x => x.ParentId == -1).SelectAll().Execute();
            int    num_group    = 0;
            int    num_subgroup = 0;
            int countAll = groups.Count;
            string message      = "";
            foreach (OMGroup group in groups)
            {
                num_group++;
                num_subgroup = 0;
                // Выбираем все подгруппы группы
                List<OMGroup> subgroups = OMGroup.Where(x => x.ParentId == group.Id).SelectAll().Execute();
                foreach (OMGroup subgroup in subgroups)
                {
                    num_subgroup++;
                    message = "Группа (" + num_group.ToString() + "-" + groups.Count().ToString() + ")"
                              + " - подгруппа (" + num_subgroup.ToString() + "-" + subgroups.Count().ToString() + ")";

                    var taskCounter = 0;
                    foreach (long taskId in setting.TaskFilter)
                    {
                        //Выбираем объекты данной подгруппы и ID задачи на оценку
                        List<OMUnit> units = OMUnit.Where(x => x.GroupId == subgroup.Id && x.TaskId == taskId && x.CadastralCost > 0).SelectAll().Execute();
                        if (units.Count == 0)
                        {
                            setProgress(100, true, progressMessage);
                            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
                            return res;
                        }

                        if ((subgroup.GroupAlgoritm_Code == KoGroupAlgoritm.Model) || (subgroup.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon))
                        {
                            var fileResult = SaveExcel5Model(units, subgroup, unloadResultQueue.Id, setting.DirectoryName, taskId, message);
                            res.Add(fileResult);
                        }
                        else
                        {
                            var fileResult = SaveExcel5Upksz(units, subgroup, unloadResultQueue.Id, setting.DirectoryName, taskId, message);
                            res.Add(fileResult);
                        }
                        taskCounter++;
                        progress = ((taskCounter*100/setting.TaskFilter.Count + num_subgroup * 100 )
                                    / subgroups.Count + num_group * 100) / groups.Count;
                        setProgress(progress, progressMessage: progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                }
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel5Model(List<OMUnit> _units, OMGroup _subgroup, long unloadId, string _dir_name, long _taskid, string _message)
        {
            OMModel model = OMModel.Where(x => x.GroupId == _subgroup.Id && x.IsActive.Coalesce(false) == true).SelectAll().ExecuteFirstOrDefault();
            if (model == null)
            {
                return new ResultKoUnloadSettings(true);
            }

            if (model.ModelFactor.Count == 0)
                model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id && x.AlgorithmType_Code == model.AlgoritmType_Code).SelectAll().Execute();

            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 4;
            int count_cells = 0;
            int num_pp = 0;
            HeaderExcel5Model(sheet_edit, _subgroup, model, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            foreach (OMUnit unit in _units)
            {
                List<object> objarrs = new List<object>();
                objarrs.Add(++num_pp);
                objarrs.Add(unit.CadastralBlock.Substring(0, 5));
                objarrs.Add(unit.PropertyType);
                objarrs.Add(unit.CadastralNumber);
                string value_attr = "";
                DataExportCommon.GetObjectAttribute(unit, 600, out value_attr);
                objarrs.Add(value_attr);

                #region Получили реестр Id группы, реестр, где лежат ее факторы
                int? factorReestrId = OMGroup.GetFactorReestrId(_subgroup);
                //Получаем список факторов группы и их значения
                List<CalcItem> FactorValuesGroup = new List<CalcItem>();
                DataTable data = RegisterStorage.GetAttributes((int)unit.Id, factorReestrId.Value);
                if (data != null)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        FactorValuesGroup.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(), row.ItemArray[7].ParseToString()));
                    }
                }
                #endregion

                foreach (OMModelFactor factor in model.ModelFactor)
                {
                    bool findf = false;
                    string value_item = string.Empty;//TODO: значение фактора для данного объекта
                    CalcItem factor_item = FactorValuesGroup.Find(x => x.FactorId == factor.FactorId);
                    if (factor_item != null)
                    {
                        findf = true;
                        value_item = factor_item.Value;
                    }
                    objarrs.Add((findf) ? value_item : "");
                }

                objarrs.Add(unit.Square.ToString());
                objarrs.Add(unit.Upks.ToString());
                objarrs.Add(unit.CadastralCost.ToString());

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }

                curindval++;
                start_rows++;
            }

            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            string file_name = "Task_" + _taskid + "_Таблица 5. Модельная стоимость"
                               + " " + DataExportCommon.GetFullNumberGroup(_subgroup);
            long id = SaveUnloadResult.SaveResult(file_name, excel_edit, unloadId, KoUnloadResultType.UnloadTable05);

            return new ResultKoUnloadSettings
            {
                FileId = id,
                FileName = file_name,
                TaskId = _taskid
            };

        }

        /// <summary>
        /// Сохранение в Excel "Таблица 5. Метод УПКС".
        /// </summary>
        /// <param name="_subgroup"></param>
        private static ResultKoUnloadSettings SaveExcel5Upksz(List<OMUnit> _units, OMGroup _subgroup, long unloadId, string _dir_name, long _taskid, string _message)
        {
            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 3;
            int count_cells = 0;
            int num_pp = 0;
            HeaderExcel5Upksz(sheet_edit, _subgroup, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            foreach (OMUnit unit in _units)
            {
                List<object> objarrs = new List<object>();
                objarrs.Add(++num_pp);
                objarrs.Add(unit.PropertyType);
                objarrs.Add(unit.CadastralNumber);
                objarrs.Add(unit.CadastralBlock);
                objarrs.Add(unit.CadastralBlock.Substring(0, 5));
                objarrs.Add(unit.Square.ToString());
                objarrs.Add(unit.Upks.ToString());
                objarrs.Add(unit.CadastralCost.ToString());

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }

                curindval++;
                start_rows++;
            }

            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            string file_name =  "Task_" + _taskid + "\\Таблица 5. Метод УПКС"
                                + " " + DataExportCommon.GetFullNumberGroup(_subgroup);
            long id = SaveUnloadResult.SaveResult(file_name, excel_edit, unloadId, KoUnloadResultType.UnloadTable05);

            return new ResultKoUnloadSettings
            {
                FileName = file_name,
                FileId = id,
                TaskId = _taskid
            };
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 5. Модельная стоимость
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel5Model(ExcelWorksheet _sheet, OMGroup _sub_group, OMModel _model, ref int _count_cells)
        {
            List<object> objcaps2 = new List<object>();
            objcaps2.Add("№п/п");
            objcaps2.Add("Номер кадастрового района");
            objcaps2.Add("Вид объекта недвижимости");
            objcaps2.Add("Кадастровый номер объекта недвижимости");
            objcaps2.Add("Адрес (местоположение) объекта недвижимости");
            foreach (OMModelFactor factor in _model.ModelFactor)
            {
                RegisterAttribute attribute_factor = RegisterCache.GetAttributeData((int)(factor.FactorId));
                objcaps2.Add(attribute_factor.Name);
            }
            objcaps2.Add("Площадь, кв.м");
            objcaps2.Add("УПКС, руб./кв.м");
            objcaps2.Add("Кадастровая стоимость, руб.");

            List<object> objcaps3 = new List<object>();
            for (int i = 0; i < objcaps2.Count; i++)
                objcaps3.Add(i + 1);

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(40 * 256);
            for (int i = 0; i < _model.ModelFactor.Count; i++)
                widths_cells.Add(30 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);

            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[2].Height = 5 * 256;

            _count_cells = objcaps2.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Таблица 5. Результаты моделирования " + DataExportCommon.GetFullNameGroup(_sub_group), true);
            DataExportCommon.MergeCell(_sheet, 1, 1, 5, 4+_model.ModelFactor.Count(), "Значения ценообразующих факторов");
            _sheet.Cells.GetSubrangeAbsolute(1, 0, 2, 0).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 1, 2, 1).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 2, 2, 2).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 3, 2, 3).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 4, 2, 4).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 5 + _model.ModelFactor.Count(), 2, 5 + _model.ModelFactor.Count()).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 6 + _model.ModelFactor.Count(), 2, 6 + _model.ModelFactor.Count()).Merged = true;
            _sheet.Cells.GetSubrangeAbsolute(1, 7 + _model.ModelFactor.Count(), 2, 7 + _model.ModelFactor.Count()).Merged = true;
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 3, objcaps3.ToArray(), widths_cells.ToArray(), true, true, false);
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 5. Метод УПКС
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel5Upksz(ExcelWorksheet _sheet, OMGroup _sub_group, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№п/п");
            objcaps1.Add("Вид объекта недвижимости");
            objcaps1.Add("Кадастровый номер объекта недвижимости");
            objcaps1.Add("Кадастровый квартал");
            objcaps1.Add("Кадастровый район");
            objcaps1.Add("Площадь, кв.м");
            objcaps1.Add("УПКС, руб./кв.м");
            objcaps1.Add("Кадастровая стоимость, руб.");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");
            objcaps2.Add("4");
            objcaps2.Add("5");
            objcaps2.Add("6");
            objcaps2.Add("7");
            objcaps2.Add("8");

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Результаты расчета кадастровой стоимости объектов недвижимости (метод УПКС) подгруппы " + DataExportCommon.GetFullNameGroup(_sub_group), true);
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 5 * 256;
            _sheet.Rows[1].Height = 5 * 256;
        }

        /// <summary>
        /// Выгрузка файлов Excel "Таблица 7. Обобщенные показатели по кадастровым районам"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable07)]
        public static List<ResultKoUnloadSettings> ExportToXls7(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Обобщенные показатели по кадастровым районам";
            var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var result = new List<ResultKoUnloadSettings>();
            int num_save = 0;
            int count_group = (setting.UnloadParcel) ?13:16;
            List<GeneralizedValuesUPKSZ> list_statistics = new List<GeneralizedValuesUPKSZ>(count_group);

            #region Собираем статистику
            if (setting.UnloadParcel)
            {   //Выгружаем Земельные участки 
                var taskCounter = 0;
                foreach (long taskId in setting.TaskFilter)
                {
                    var unitCounter = 0;
                    List<OMUnit> units_zu = OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == PropertyTypes.Stead && x.CadastralCost > 0).OrderBy(x => x.CadastralBlock).SelectAll().Execute();
                    foreach (OMUnit unit in units_zu)
                    {
                        OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                        if (!group.Number.IsNullOrEmpty())
                        {
                            int num_group = Convert.ToInt32(group.Number.Substring(0, 1));
                            CalculationStat7(ref list_statistics, unit, num_group, count_group);
                            num_save++;
                        }

                        unitCounter++;
                        progress = (unitCounter * 70 / units_zu.Count + taskCounter * 70) / setting.TaskFilter.Count;
                        setProgress(progress, progressMessage: progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                    taskCounter++;
                }
            }
            else
            {   //Выгружаем объекты ОКС
                List<PropertyTypes> prop_types = new List<PropertyTypes>()
                {
                    PropertyTypes.Building,
                    PropertyTypes.Pllacement,
                    PropertyTypes.Construction,
                    PropertyTypes.OtherMore,
                    PropertyTypes.Parking,
                };

                int num_prop = 0;
                foreach (PropertyTypes prop_type in prop_types)
                {
                    num_prop++;
                    var taskCounter = 0;
                    foreach (long taskId in setting.TaskFilter)
                    {
                        var unitCounter = 0;
                        List<OMUnit> units_oks = OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == prop_type && x.CadastralCost > 0).OrderBy(x => x.CadastralBlock).SelectAll().Execute();
                        foreach (OMUnit unit in units_oks)
                        {
                            OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                            if (group != null)
                            {
                                if (!group.Number.IsNullOrEmpty())
                                {
                                    int num_group = Convert.ToInt32(group.Number.Substring(0, 1));
                                    CalculationStat7(ref list_statistics, unit, num_group, count_group);
                                    num_save++;
                                }
                            }

                            unitCounter++;
                            progress = (unitCounter * 70 / units_oks.Count + taskCounter * 70 / setting.TaskFilter.Count + (num_prop - 1) * 70) / prop_types.Count;
                            setProgress(progress, progressMessage: progressMessage);
                            KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                        }
                        taskCounter++;
                    }
                }
            }
            #endregion

            setProgress(70, progressMessage: "Обобщенные показатели по кадастровым районам - средние и итого");
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 70);
            #region Пересчитываем средние и всего
            Console.WriteLine("Пересчитываем средние и итого ...");
            GeneralizedValuesUPKSZ stat_total = new GeneralizedValuesUPKSZ(count_group);
            stat_total.CadastralArea = "Итого по Москве";
            var statsCount = 0;
            foreach (GeneralizedValuesUPKSZ stat in list_statistics)
            {
                stat_total.CountObj += stat.CountObj;
                for(int i = 0; i < count_group; i++)
                {
                    if (stat.MinAvgMax[3, i] != 0)
                    {
                        stat_total.MinAvgMax[0, i]  = (stat_total.MinAvgMax[0, i] == -1)
                            ? stat.MinAvgMax[0, i]
                            : Math.Min(stat_total.MinAvgMax[0, i], stat.MinAvgMax[0, i]);
                        stat.MinAvgMax[1, i]  = stat.MinAvgMax[1, i] / stat.MinAvgMax[3, i];
                        stat_total.MinAvgMax[1, i] += stat.MinAvgMax[1, i];
                        stat_total.MinAvgMax[2, i]  = Math.Max(stat_total.MinAvgMax[2, i], stat.MinAvgMax[2, i]);
                    }
                }

                statsCount++;
                progress = 70+(statsCount*20/list_statistics.Count);
                setProgress(progress, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
            }
            // Расчет Средние ИТОГО
            for (int i = 0; i < count_group; i++)
            {
                stat_total.MinAvgMax[1, i] = stat_total.MinAvgMax[1, i] / list_statistics.Count;
                progress = 90 + (i * 10 / count_group);
                setProgress(progress, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
            }

            list_statistics.Add(stat_total);
            #endregion

            #region Формирование отчета
            Console.WriteLine("Формирование отчета ...");
            var fileResult = SaveExcel7(list_statistics, unloadResultQueue.Id, setting.UnloadParcel, count_group, setting.DirectoryName,
                setting.TaskFilter.FirstOrDefault());
            result.Add(fileResult);
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, "Обобщенные показатели по кадастровым районам - Формирование отчета");

            return result;

            #endregion
        }

        private static void CalculationStat7(ref List<GeneralizedValuesUPKSZ> _list_statistics, OMUnit _unit, int _num, int _count_group)
        {
            bool is_find = false;
            string cad_area = _unit.CadastralBlock.Substring(0, 5);

            foreach (var statistic in _list_statistics)
            {
                if (statistic.CadastralArea == cad_area)
                {
                    statistic.CountObj++;
                    statistic.MinAvgMax[0, _num - 1] = (statistic.MinAvgMax[0, _num - 1] == -1)
                        ? (double)_unit.Upks
                        : Math.Min(statistic.MinAvgMax[0, _num - 1], (double)_unit.Upks);
                    statistic.MinAvgMax[1, _num - 1] += (double)_unit.Upks;
                    statistic.MinAvgMax[2, _num - 1] = Math.Max(statistic.MinAvgMax[2, _num - 1], (double)_unit.Upks);
                    statistic.MinAvgMax[3, _num - 1]++;
                    is_find = true;
                    break;
                }
            }
            if (!is_find)
            {   //Добавить новый кадастровый район
                GeneralizedValuesUPKSZ statistic_new = new GeneralizedValuesUPKSZ(_count_group);
                statistic_new.CadastralArea = cad_area;
                statistic_new.CountObj = 1;
                statistic_new.MinAvgMax[0, _num - 1] = (double)_unit.Upks; //Минимальное
                statistic_new.MinAvgMax[1, _num - 1] = (double)_unit.Upks; //Сумма УПКСЗ, потом запишется среднее
                statistic_new.MinAvgMax[2, _num - 1] = (double)_unit.Upks; //Максимальное
                statistic_new.MinAvgMax[3, _num - 1] = 1;                  //Количество объектов этой группы. Для расчета среднего УПКСЗ
                _list_statistics.Add(statistic_new);
            }
        }

        private static ResultKoUnloadSettings SaveExcel7(List<GeneralizedValuesUPKSZ> _statistics, long unloadId, bool _is_parsel, int _count_group, string _dir_name, long firrsTaskId)
        {
            FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream((_is_parsel) ?"Table7_zu": "Table7_oks", ".xlsx", "ExcelTemplates");
            ExcelFile excel_edit = ExcelFile.Load(fileStream, GemBox.Spreadsheet.LoadOptions.XlsxDefault);
            var sheet_edit = excel_edit.Worksheets[0];

            int num_pp = 0;
            int start_rows = 7;
            foreach (GeneralizedValuesUPKSZ stat in _statistics)
            {
                object[,] objvals = new object[3, _count_group+1];
                objvals[0, 0] = "Минимальное";
                objvals[1, 0] = "Среднее";
                objvals[2, 0] = "Максимальное";
                for (int i = 1; i <= _count_group; i++)
                {
                    objvals[0, i] = (stat.MinAvgMax[0, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[0, i - 1].ToString();
                    objvals[1, i] = (stat.MinAvgMax[1, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[1, i - 1].ToString();
                    objvals[2, i] = (stat.MinAvgMax[2, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[2, i - 1].ToString();
                }
                num_pp++;
                DataExportCommon.AddRow(sheet_edit, start_rows, 3, objvals);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows+2, 0, 0, num_pp.ToString()       , false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows+2, 1, 1, stat.CadastralArea      , false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows+2, 2, 2, stat.CountObj.ToString(), false);
                start_rows += 3;
            }

            //string path_name = _dir_name + "\\Table7";
            //if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name =  "Таблица 7. Обобщенные показатели результатов расчета кадастровой стоимости по кадастровым районам города Москвы"
                                + "." + ((_is_parsel) ? "ЗУ" : "ОКС");

            long id = SaveUnloadResult.SaveResult(file_name, excel_edit, unloadId, KoUnloadResultType.UnloadTable07);

            return new ResultKoUnloadSettings
            {
                FileId = id,
                FileName = file_name,
                TaskId = firrsTaskId
            };
            //excel_edit.Save(file_name);
        }

        /// <summary>
        /// Выгрузка файлов Excel "Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable08)]
        public static List<ResultKoUnloadSettings> ExportToXls8(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Минимальные, максимальные, средние УПКС по кадастровым кварталам";
            var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            var result = new List<ResultKoUnloadSettings>();
            int num_save = 0;
            int count_group = (setting.UnloadParcel) ? 13 : 16;
            List<GeneralizedValuesUPKSZ> list_statistics = new List<GeneralizedValuesUPKSZ>(count_group);

            #region Собираем статистику
            if (setting.UnloadParcel)
            {  
                //Выгружаем Земельные участки 
                var taskCounter = 0;
                foreach (long taskId in setting.TaskFilter)
                {
                    var unitCounter = 0;
                    List<OMUnit> units_zu = OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == PropertyTypes.Stead && x.CadastralCost > 0).OrderBy(x => x.CadastralBlock).SelectAll().Execute();
                    foreach (OMUnit unit in units_zu)
                    {
                        OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                        if (!group.Number.IsNullOrEmpty())
                        {
                            int num_group = Convert.ToInt32(group.Number.Substring(0, 1));
                            CalculationStat8(ref list_statistics, unit, num_group, count_group);
                            num_save++;
                        }

                        unitCounter++;
                        progress = (unitCounter * 70 / units_zu.Count + taskCounter * 70) / setting.TaskFilter.Count;
                        setProgress(progress, progressMessage: progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                    taskCounter++;
                }
            }
            else
            {   //Выгружаем объекты ОКС
                List<PropertyTypes> prop_types = new List<PropertyTypes>()
                {
                    PropertyTypes.Building,
                    PropertyTypes.Pllacement,
                    PropertyTypes.Construction,
                    PropertyTypes.OtherMore,
                    PropertyTypes.Parking,
                };

                int num_prop = 0;
                foreach (PropertyTypes prop_type in prop_types)
                {
                    num_prop++;
                    var taskCounter = 0;
                    foreach (long taskId in setting.TaskFilter)
                    {
                        var unitCounter = 0;
                        List<OMUnit> units_oks = OMUnit.Where(x => x.TaskId == taskId && x.PropertyType_Code == prop_type && x.CadastralCost > 0).OrderBy(x => x.CadastralBlock).SelectAll().Execute();
                        foreach (OMUnit unit in units_oks)
                        {
                            OMGroup group = OMGroup.Where(x => x.Id == unit.GroupId).SelectAll().ExecuteFirstOrDefault();
                            if (group != null)
                            {
                                if (!group.Number.IsNullOrEmpty())
                                {
                                    int num_group = Convert.ToInt32(group.Number.Substring(0, 1));
                                    CalculationStat8(ref list_statistics, unit, num_group, count_group);
                                    num_save++;
                                }
                            }

                            unitCounter++;
                            progress = (unitCounter * 70 / units_oks.Count + taskCounter * 70 / setting.TaskFilter.Count + (num_prop - 1) * 70) / prop_types.Count;
                            setProgress(progress, progressMessage: progressMessage);
                            KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                        }
                        taskCounter++;
                        
                    }
                }
            }
            #endregion

            #region Пересчитываем средние
            Console.WriteLine("Пересчитываем средние ...");
            setProgress(70, progressMessage: "Минимальные, максимальные, средние УПКС по кадастровым кварталам - средние");
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
            var statsCount = 0;
            foreach (GeneralizedValuesUPKSZ stat in list_statistics)
            {
                for (int i = 0; i < count_group; i++)
                {
                    if (stat.MinAvgMax[3, i] != 0)
                    {
                        stat.MinAvgMax[1, i] = stat.MinAvgMax[1, i] / stat.MinAvgMax[3, i];
                    }
                }
                statsCount++;
                progress = 70 + (statsCount * 30 / list_statistics.Count);
                setProgress(progress, progressMessage: progressMessage);
                KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
            }
            #endregion

            #region Формирование отчета
            Console.WriteLine("Формирование отчета ...");
            var fileResult = SaveExcel8(list_statistics, unloadResultQueue.Id, setting.UnloadParcel, count_group, setting.DirectoryName,
                setting.TaskFilter.FirstOrDefault());
            result.Add(fileResult);
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, "Минимальные, максимальные, средние УПКС по кадастровым кварталам - Формирование отчета");


            return result;

            #endregion
        }

        private static void CalculationStat8(ref List<GeneralizedValuesUPKSZ> _list_statistics, OMUnit _unit, int _num, int _count_group)
        {
            bool is_find = false;
            string cad_area = _unit.CadastralBlock.Substring(0, 5);

            foreach (var statistic in _list_statistics)
            {
                if (statistic.CadastralBlok == _unit.CadastralBlock)
                {
                    statistic.CountObj++;
                    statistic.MinAvgMax[0, _num - 1] = (statistic.MinAvgMax[0, _num - 1] == -1)
                        ? (double)_unit.Upks
                        : Math.Min(statistic.MinAvgMax[0, _num - 1], (double)_unit.Upks);
                    statistic.MinAvgMax[1, _num - 1] += (double)_unit.Upks;
                    statistic.MinAvgMax[2, _num - 1] = Math.Max(statistic.MinAvgMax[2, _num - 1], (double)_unit.Upks);
                    statistic.MinAvgMax[3, _num - 1]++;
                    is_find = true;
                    break;
                }
            }
            if (!is_find)
            {   //Добавить новый кадастровый квартал
                GeneralizedValuesUPKSZ statistic_new = new GeneralizedValuesUPKSZ(_count_group);
                statistic_new.CadastralArea = cad_area;
                statistic_new.CadastralBlok = _unit.CadastralBlock;
                statistic_new.CountObj = 1;
                statistic_new.MinAvgMax[0, _num - 1] = (double)_unit.Upks; //Минимальное
                statistic_new.MinAvgMax[1, _num - 1] = (double)_unit.Upks; //Сумма УПКСЗ, потом запишется среднее
                statistic_new.MinAvgMax[2, _num - 1] = (double)_unit.Upks; //Максимальное
                statistic_new.MinAvgMax[3, _num - 1] = 1;                  //Количество объектов этой группы. Для расчета среднего УПКСЗ
                _list_statistics.Add(statistic_new);
            }
        }

        private static ResultKoUnloadSettings SaveExcel8(List<GeneralizedValuesUPKSZ> _statistics, long unloadId, bool _is_parsel, int _count_group, string _dir_name, long firstTaskId)
        {
            FileStream fileStream = Core.ConfigParam.Configuration.GetFileStream((_is_parsel) ? "Table8_zu" : "Table8_oks", ".xlsx", "ExcelTemplates");
            ExcelFile excel_edit = ExcelFile.Load(fileStream, GemBox.Spreadsheet.LoadOptions.XlsxDefault);
            var sheet_edit = excel_edit.Worksheets[0];

            int num_pp = 0;
            int start_rows = 7;
            foreach (GeneralizedValuesUPKSZ stat in _statistics)
            {
                object[,] objvals = new object[3, _count_group + 1];
                objvals[0, 0] = "Минимальное";
                objvals[1, 0] = "Среднее";
                objvals[2, 0] = "Максимальное";
                for (int i = 1; i <= _count_group; i++)
                {
                    objvals[0, i] = (stat.MinAvgMax[0, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[0, i - 1].ToString();
                    objvals[1, i] = (stat.MinAvgMax[1, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[1, i - 1].ToString();
                    objvals[2, i] = (stat.MinAvgMax[2, i - 1] == 0 || stat.MinAvgMax[0, i - 1] == -1) ? "-" : stat.MinAvgMax[2, i - 1].ToString();
                }
                num_pp++;
                DataExportCommon.AddRow(sheet_edit, start_rows, 4, objvals);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows + 2, 0, 0, num_pp.ToString(), false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows + 2, 1, 1, stat.CadastralArea, false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows + 2, 2, 2, stat.CadastralBlok, false);
                DataExportCommon.MergeCell(sheet_edit, start_rows, start_rows + 2, 3, 3, stat.CountObj.ToString(), false);
                start_rows += 3;
            }

            // string path_name = _dir_name + "\\Table8";
            // if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name ="Таблица 8. Обобщенные показатели результатов расчета кадастровой стоимости по кадастровым кварталам города Москвы"
                              + "." + ((_is_parsel) ? "ЗУ" : "ОКС");

            long id = SaveUnloadResult.SaveResult(file_name, excel_edit, unloadId, KoUnloadResultType.UnloadTable08);
            // excel_edit.Save(file_name);

            return new ResultKoUnloadSettings
            {
                FileName = file_name,
                FileId = id,
                TaskId = firstTaskId
            };
        }

        /// <summary>
        /// Выгрузка файлов Excel "Таблица 9. Результаты определения КС"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable09)]
        public static List<ResultKoUnloadSettings> ExportToXls9(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Результаты определения КС";
            var taskCounter = 0;
            var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            List < ResultKoUnloadSettings > res = new List<ResultKoUnloadSettings>();

            foreach (long taskId in setting.TaskFilter)
            {
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId && x.CadastralCost > 0).OrderBy(x => x.CadastralNumber).SelectAll().Execute();
                if (units_all.Count == 0) return new List<ResultKoUnloadSettings>();

                List<OMUnit> units_curr = new List<OMUnit>();
                int num_pp = 0;
                int count_curr = 0;
                int count_file = 0;
                string message = "";
                int count_all = units_all.Count();
                string cad_num_curr = "";
                string cad_num = units_all[0].CadastralNumber.Substring(0, 5); // Номер кадастрового района первого объекта

                foreach (OMUnit unit in units_all)
                {
                    cad_num_curr = unit.CadastralNumber.Substring(0, 5);
                    if (cad_num_curr != cad_num)
                    {  // Если начались объекты из другого кадастрового района, то предыдущие сохраняем в файл 
                        count_file++;
                        var fileResult = SaveExcel9(units_curr, unloadResultQueue.Id, ref num_pp, count_file, cad_num, setting.DirectoryName,
                            taskId, message);
                        res.Add(fileResult);
                        units_curr.Clear();
                        cad_num = cad_num_curr;
                    }
                    units_curr.Add(unit);

                    count_curr++;
                    progress = (count_curr * 100 / count_all + taskCounter * 100) / setting.TaskFilter.Count;
                    setProgress(progress, progressMessage: progressMessage);
                    KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }
                if (units_curr.Count > 0)
                {
                    count_file++;
                    var fileResult = SaveExcel9(units_curr, unloadResultQueue.Id, ref num_pp, count_file, cad_num_curr, setting.DirectoryName,
                        taskId, message);
                    res.Add(fileResult);
                }

                taskCounter++;
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true,progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel9(List<OMUnit> _units_curr, long unloadId, ref int _num_pp,
            int _count_file, string _cad_num, string _dir_name, long _taskid, string _mess)
        {
            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 3;
            int count_cells = 0;
            HeaderExcel9(sheet_edit, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            for (int i = 0; i < _units_curr.Count; i++)
            {
                string message = _mess + " -- сохраняется " + (i + 1).ToString() + " из " + _units_curr.Count.ToString();
                Console.WriteLine(message);

                _num_pp++;
                List<object> objarrs = new List<object>();
                objarrs.Add(_num_pp);
                objarrs.Add(_units_curr[i].CadastralNumber);
                objarrs.Add(_units_curr[i].CadastralCost);

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }
                curindval++;
                start_rows++;
            }
            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            //string path_name = _dir_name + "\\Table9\\Task_" + _taskid.ToString();
            //if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name = "Task_" + _taskid + "Таблица 9. Результаты определения КС"
                               + " " + _cad_num.Replace(":", "_") + "." + _count_file.ToString().PadLeft(5, '0');
            //excel_edit.Save(file_name);

            long id = SaveUnloadResult.SaveResult(file_name, excel_edit, unloadId, KoUnloadResultType.UnloadTable09);

            return new ResultKoUnloadSettings
            {
                FileName = file_name,
                FileId = id,
                TaskId = _taskid
            };
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 9
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel9(ExcelWorksheet _sheet, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№ п/п");
            objcaps1.Add("Кадастровый номер объекта недвижимости");
            objcaps1.Add("Кадастровая стоимость объекта недвижимости, руб.");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(40 * 256);
            widths_cells.Add(40 * 256);

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Результаты определения кадастровой стоимости объектов недвижимости на территории субъекта Российской Федерации город Москва", true);
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[1].Height = 4 * 256;
        }

        /// <summary>
        /// Выгрузка файлов Excel "Таблица 10. Результаты ГКО"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable10)]
        public static List<ResultKoUnloadSettings> ExportToXls10(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Результаты ГКО";
            var taskCounter = 0;
            var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            List<ResultKoUnloadSettings> res = new List<ResultKoUnloadSettings>();

            foreach (long taskId in setting.TaskFilter)
            {
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId && x.CadastralCost > 0).OrderBy(x => x.CadastralNumber).SelectAll().Execute();
                if (units_all.Count == 0) return new List<ResultKoUnloadSettings>();

                List<OMUnit> units_curr = new List<OMUnit>();
                int num_pp = 0;
                int count_curr = 0;
                int count_file = 0;
                string message = "";
                int count_all = units_all.Count();
                string cad_num_curr = "";
                string cad_num = units_all[0].CadastralNumber.Substring(0, 5); // Номер кадастрового района первого объекта

                foreach (OMUnit unit in units_all)
                {
                    cad_num_curr = unit.CadastralNumber.Substring(0, 5);
                    if (cad_num_curr != cad_num)
                    {  // Если начались объекты из другого кадастрового района, то предыдущие сохраняем в файл 

                        count_file++;
                        var fileResult = SaveExcel10(units_curr, unloadResultQueue.Id, ref num_pp, count_file, cad_num, setting.DirectoryName,
                            taskId, message);
                        res.Add(fileResult);
                        units_curr.Clear();
                        cad_num = cad_num_curr;
                    }
                    units_curr.Add(unit);

                    count_curr++;
                    progress = (count_curr * 100 / count_all + taskCounter * 100) / setting.TaskFilter.Count;
                    setProgress(progress, progressMessage: progressMessage);
                    KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                }
                if (units_curr.Count > 0)
                {
                    count_file++;
                    var fileResult = SaveExcel10(units_curr, unloadResultQueue.Id, ref num_pp, count_file, cad_num_curr,
                        setting.DirectoryName, taskId, message);
                    res.Add(fileResult);
                }
                taskCounter++;
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel10(List<OMUnit> _units_curr, long unloadId, ref int _num_pp,
            int _count_file, string _cad_num,
            string _dir_name, long _taskid, string _mess)
        {
            ExcelFile excel_edit = new ExcelFile();
            ExcelWorksheet sheet_edit = excel_edit.Worksheets.Add("КО");
            int start_rows = 3;
            int count_cells = 0;
            HeaderExcel10(sheet_edit, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            for (int i = 0; i < _units_curr.Count; i++)
            {
                string message = _mess + " -- сохраняется " + (i + 1).ToString() + " из " + _units_curr.Count.ToString();
                Console.WriteLine(message);

                _num_pp++;
                List<object> objarrs = new List<object>();
                objarrs.Add(_num_pp);
                objarrs.Add(_cad_num);
                objarrs.Add(_units_curr[i].CadastralNumber);
                objarrs.Add(_units_curr[i].PropertyType);
                objarrs.Add(_units_curr[i].Square);
                objarrs.Add(_units_curr[i].Upks);
                objarrs.Add(_units_curr[i].CadastralCost);

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(sheet_edit, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }

                curindval++;
                start_rows++;
            }
            if (curindval != 0)
            {
                DataExportCommon.AddRow(sheet_edit, start_rows - curindval, objvals, curindval);
            }

            //string path_name = _dir_name + "\\Table10\\Task_" + _taskid.ToString();
            //if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name = "Task_" + _taskid + "Таблица 10. Результаты ГКО"
                               + " " + _cad_num.Replace(":", "_") + "." + _count_file.ToString().PadLeft(5, '0');
            //excel_edit.Save(file_name);

            long id = SaveUnloadResult.SaveResult(file_name, excel_edit, unloadId, KoUnloadResultType.UnloadTable10);

            return new ResultKoUnloadSettings
            {
                FileName = file_name,
                FileId = id,
                TaskId = _taskid
            };
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 10
        /// </summary>
        /// <param name="_sheet">Рабочий лист</param>
        /// <param name="_count_cells">Количество столбцов (ячеек в строке)</param>
        private static void HeaderExcel10(ExcelWorksheet _sheet, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№ п/п");
            objcaps1.Add("Кадастровый район");
            objcaps1.Add("Кадастровый номер объекта недвижимости");
            objcaps1.Add("Вид объекта недвижимости");
            objcaps1.Add("Общая площадь объекта недвижимости, кв.м.");
            objcaps1.Add("УПКС объекта недвижимости, руб./кв.м.");
            objcaps1.Add("Кадастровая стоимость объекта недвижимости, руб.");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");
            objcaps2.Add("4");
            objcaps2.Add("5");
            objcaps2.Add("6");
            objcaps2.Add("7");

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(17 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(50 * 256);
            widths_cells.Add(50 * 256);

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Результаты государственной кадастровой оценки объектов недвижимости");
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[1].Height = 4 * 256;
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 11. Сводные результаты по КР"
        /// </summary>
        [KoUnloadResultAction(KoUnloadResultType.UnloadTable11)]
        public static List<ResultKoUnloadSettings> ExportToXls11(OMUnloadResultQueue unloadResultQueue, KOUnloadSettings setting, SetProgress setProgress)
        {
            var progressMessage = "Сводные результаты по КР";
            var taskCounter = 0;
            var progress = 0;
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 0);
            List <ResultKoUnloadSettings> res = new List<ResultKoUnloadSettings>();

            foreach (long taskId in setting.TaskFilter)
            {
                List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == taskId).OrderBy(x => x.CadastralNumber).SelectAll().Execute();
                if (units_all.Count == 0) return new List<ResultKoUnloadSettings>();

                List<PropertyTypes> prop_types = new List<PropertyTypes>()
                {
                    PropertyTypes.Building,
                    PropertyTypes.Company,
                    PropertyTypes.Construction,
                    PropertyTypes.Other,
                    PropertyTypes.OtherMore,
                    PropertyTypes.Parking,
                    PropertyTypes.Pllacement,
                    PropertyTypes.Stead,
                    PropertyTypes.UncompletedBuilding,
                    PropertyTypes.UnitedPropertyComplex
                };

                string message = "";
                int ind_type = 0;
                foreach (PropertyTypes prop_type in prop_types)
                {
                    ind_type++;
                    message = prop_type.GetEnumDescription() + " (" + ind_type.ToString() + "-" + prop_types.Count().ToString() + ")";
                    List<OMUnit> units_types = units_all.Where(x => x.PropertyType_Code == prop_type && x.CadastralCost > 0).OrderBy(x => x.CadastralNumber).ToList();
                    if (units_types.Count == 0) continue;

                    List<OMUnit> units_curr = new List<OMUnit>();
                    int num_pp = 0;
                    int count_curr = 0;
                    int count_file = 0;
                    int count_all = units_types.Count();
                    string message1 = "";
                    string cad_num_curr = "";
                    string cad_num = units_types[0].CadastralNumber.Substring(0, 5); // Номер кадастрового района первого объекта

                    foreach (OMUnit unit in units_types)
                    {
                        cad_num_curr = unit.CadastralNumber.Substring(0, 5);
                        if (cad_num_curr != cad_num)
                        {  // Если начались объекты из другого кадастрового района, то предыдущие сохраняем в файл 

                            count_file++;
                            var fileResult = SaveExcel11(unloadResultQueue.Id, prop_type, units_curr, ref num_pp, count_file, cad_num,
                                setting.DirectoryName, taskId, message1);
                            res.Add(fileResult);
                            units_curr.Clear();
                            cad_num = cad_num_curr;
                        }
                        units_curr.Add(unit);

                        count_curr++;
                        progress = ((count_curr * 100 / count_all + (ind_type - 1) * 100) / prop_types.Count + taskCounter * 100) / setting.TaskFilter.Count;
                        setProgress(progress, progressMessage: progressMessage);
                        KOUnloadResult.SetCurrentProgress(unloadResultQueue, progress);
                    }
                    if (units_curr.Count > 0)
                    {
                        count_file++;
                        var fileResult = SaveExcel11(unloadResultQueue.Id, prop_type, units_curr, ref num_pp, count_file, cad_num_curr,
                            setting.DirectoryName, taskId, message1);
                        res.Add(fileResult);
                    }
                }
                taskCounter++;
            }
            KOUnloadResult.SetCurrentProgress(unloadResultQueue, 100);
            setProgress(100, true, progressMessage);

            return res;
        }

        private static ResultKoUnloadSettings SaveExcel11(long unloadId, PropertyTypes _prop_type,
            List<OMUnit> _units_curr, ref int _num_pp,
            int _count_file, string _cad_num, string _dir_name, long _taskid, string _mess)
        {
            int start_rows = 3;
            int count_cells = 0;

            ExcelFile excelTemplate = new ExcelFile();
            ExcelWorksheet mainWorkSheet = excelTemplate.Worksheets.Add("КО");
            HeaderExcel11(mainWorkSheet, _cad_num, _prop_type, ref count_cells);

            object[,] objvals = new object[100, count_cells];
            int curindval = 0;
            for (int i = 0; i < _units_curr.Count; i++)
            {
                string message = _mess + " -- сохраняется " + (i + 1).ToString() + " из " + _units_curr.Count.ToString();
                Console.WriteLine(message);

                _num_pp++;
                List<object> objarrs = new List<object>();
                objarrs.Add(_num_pp);
                objarrs.Add(_units_curr[i].CadastralNumber);
                objarrs.Add(_units_curr[i].PropertyType );      //TYPE_OBJECT_STR
                objarrs.Add(_units_curr[i].Square );            //SQUARE_OBJECT

                string value_attr = "";
                long number_attr = -1;

                if (_units_curr[i].PropertyType_Code == PropertyTypes.Stead) number_attr = 1;
                else number_attr = 19;
                if (number_attr > 0)
                    DataExportCommon.GetObjectAttribute(_units_curr[i], number_attr, out value_attr);   //Код - Наименование объекта
                objarrs.Add(value_attr);                        //NAME_OBJECT
                value_attr = "";
                number_attr = -1;
                if (_units_curr[i].PropertyType_Code == PropertyTypes.Building) number_attr = 14;
                else if (_units_curr[i].PropertyType_Code == PropertyTypes.Construction) number_attr = 22;
                else if (_units_curr[i].PropertyType_Code == PropertyTypes.Pllacement) number_attr = 23;
                if (number_attr > 0)
                    DataExportCommon.GetObjectAttribute(_units_curr[i], number_attr, out value_attr);   //Код  - Назначение
                objarrs.Add(value_attr);                        //USE_OBJECT);
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 600, out value_attr);               //Код 600 - Адрес 
                objarrs.Add(value_attr);                        //ADRESS_OBJECT
                objarrs.Add("770000000000");                    //KLADR_OBJECT                          //TODO надо определять КЛАДР
                value_attr = "";
                if (_units_curr[i].PropertyType_Code == PropertyTypes.Pllacement)
                    DataExportCommon.GetObjectAttribute(_units_curr[i], 604, out value_attr);           //Код 604 - Кадастровый номер здания или сооружения, в котором расположено помещение
                objarrs.Add(value_attr);                        //KN_PARENT
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 8, out value_attr);                 //Код 8 - Местоположение
                objarrs.Add(value_attr);                        //PLACE_OBJECT
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 601, out value_attr);               //Код 601 - Кадастровый квартал 
                objarrs.Add(value_attr);                        //KN_KK
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 602, out value_attr);               //Код 602 - Земельный участок 
                objarrs.Add(value_attr);                        //KN_ZU
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 15, out value_attr);                //Код 15 - Год постройки
                objarrs.Add(value_attr);                        //YEAR_BUILT
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 16, out value_attr);                //Код 16 - Год ввода в эксплуатацию
                objarrs.Add(value_attr);                        //YEAR_USED
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 17, out value_attr);                //Код 17 - Количество этажей
                objarrs.Add(value_attr);                        //FLOORS
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 18, out value_attr);                //Код 18 - Количество подземных этажей
                objarrs.Add(value_attr);                        //UNDER_FLOORS
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 24, out value_attr);                //Код 24 - Этаж (для помещения)
                objarrs.Add(value_attr);                        //LEVEL_FLAT
                value_attr = "";
                DataExportCommon.GetObjectAttribute(_units_curr[i], 21, out value_attr);                //Код 21 - Материал стен
                objarrs.Add(value_attr);                        //WALL
                if (_prop_type == PropertyTypes.Pllacement)
                {
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_units_curr[i], 14, out value_attr);            //Код 14 - Назначение здания
                    objarrs.Add(value_attr);
                    objarrs.Add("");   //TODO разобраться что сюда записывать, должно "Наименование здания"
                }
                if (_prop_type == PropertyTypes.Construction)
                {
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_units_curr[i], 44, out value_attr);            //Код 44 - Характеристика (протяженность)
                    objarrs.Add(value_attr);                    //KEY_PARAMETR
                }
                if (_prop_type == PropertyTypes.UncompletedBuilding)
                {
                    value_attr = "";
                    DataExportCommon.GetObjectAttribute(_units_curr[i], 46, out value_attr);            //Код 46 - Процент готовности
                    objarrs.Add(value_attr);                    //ProcentOrName
                }
                objarrs.Add(_units_curr[i].Upks);
                objarrs.Add(_units_curr[i].CadastralCost);

                for (int f = 0; f < count_cells; f++)
                {
                    objvals[curindval, f] = objarrs[f];
                }

                if (curindval >= 99)
                {
                    DataExportCommon.AddRow(mainWorkSheet, start_rows - 99, objvals);
                    curindval = -1;
                    objvals = new object[100, count_cells];
                }

                curindval++;
                start_rows++;
            }
            if (curindval != 0)
            {
                DataExportCommon.AddRow(mainWorkSheet, start_rows - curindval, objvals, curindval);
            }

            string path_name = _dir_name + "\\Table11\\Task_" + _taskid.ToString();
            if (!Directory.Exists(path_name)) Directory.CreateDirectory(path_name);
            string file_name = "Task_" + _taskid + "Таблица 11. Сводные результаты по КР"
                               + " " + _cad_num.Replace(":", "_")
                               + "." + _prop_type.GetEnumDescription()
                               + "." + _count_file.ToString().PadLeft(5, '0');

            long id = SaveUnloadResult.SaveResult(file_name, excelTemplate, unloadId, KoUnloadResultType.UnloadTable11);

            //excelTemplate.Save(file_name);
            return new ResultKoUnloadSettings
            {
                FileName = file_name,
                FileId = id,
                TaskId = _taskid
            };
        }

        /// <summary>
        /// Создание шапки на рабочем листе Excel Таблицы 11
        /// </summary>
        /// <param name="_sheet"></param>
        private static void HeaderExcel11(ExcelWorksheet _sheet, string _cad_num, PropertyTypes _prop_type, ref int _count_cells)
        {
            List<object> objcaps1 = new List<object>();
            objcaps1.Add("№ п/п");
            objcaps1.Add("КН");
            objcaps1.Add("Тип");
            objcaps1.Add("Площадь");
            objcaps1.Add("Наименование");
            objcaps1.Add("Назначение");
            objcaps1.Add("Адрес");
            objcaps1.Add("КЛАДР");
            objcaps1.Add("КН родителя");
            objcaps1.Add("Местоположение");
            objcaps1.Add("Кадастровый квартал");
            objcaps1.Add("Кадастровый номер земельного участка");
            objcaps1.Add("Год постройки");
            objcaps1.Add("Год ввода в эксплуатацию");
            objcaps1.Add("Кол-во этажей");
            objcaps1.Add("Подземных этажей");
            objcaps1.Add("Этаж (для помещения)");
            objcaps1.Add("Материал стен");
            if (_prop_type == PropertyTypes.Pllacement)
            {
                objcaps1.Add("Назначение здания");
                objcaps1.Add("Наименование здания");
            }
            if (_prop_type == PropertyTypes.Construction)
            {
                objcaps1.Add("Характеристика");
            }
            if (_prop_type == PropertyTypes.UncompletedBuilding)
            {
                objcaps1.Add("Процент готовности");
            }
            objcaps1.Add("УПКС объекта недвижимости, руб./кв.м.");
            objcaps1.Add("Кадастровая стоимость объекта недвижимости, руб.");

            List<object> objcaps2 = new List<object>();
            objcaps2.Add("1");
            objcaps2.Add("2");
            objcaps2.Add("3");
            objcaps2.Add("4");
            objcaps2.Add("5");
            objcaps2.Add("6");
            objcaps2.Add("7");
            objcaps2.Add("8");
            objcaps2.Add("9");
            objcaps2.Add("10");
            objcaps2.Add("11");
            objcaps2.Add("12");
            objcaps2.Add("13");
            objcaps2.Add("14");
            objcaps2.Add("15");
            objcaps2.Add("16");
            objcaps2.Add("17");
            objcaps2.Add("18");
            if (_prop_type == PropertyTypes.Pllacement) {
                objcaps2.Add("19");
                objcaps2.Add("20");
                objcaps2.Add("21");
                objcaps2.Add("22");
            } else if (_prop_type == PropertyTypes.Construction) {
                objcaps2.Add("19");
                objcaps2.Add("20");
                objcaps2.Add("21");
            } else if (_prop_type == PropertyTypes.UncompletedBuilding) {
                objcaps2.Add("19");
                objcaps2.Add("20");
                objcaps2.Add("21");
            } else {
                objcaps2.Add("19");
                objcaps2.Add("20");
            }

            List<int> widths_cells = new List<int>();
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(17 * 256);
            widths_cells.Add(23 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(30 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(20 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);
            widths_cells.Add(15 * 256);

            if (_prop_type == PropertyTypes.Pllacement) {
                widths_cells.Add(15 * 256);
                widths_cells.Add(15 * 256);
                widths_cells.Add(15 * 256);
                widths_cells.Add(20 * 256);
            } else if (_prop_type == PropertyTypes.Construction) {
                widths_cells.Add(15 * 256);
                widths_cells.Add(15 * 256);
                widths_cells.Add(20 * 256);
            } else if (_prop_type == PropertyTypes.UncompletedBuilding) {
                widths_cells.Add(15 * 256);
                widths_cells.Add(15 * 256);
                widths_cells.Add(20 * 256);
            } else {
                widths_cells.Add(15 * 256);
                widths_cells.Add(20 * 256);
            }

            _count_cells = objcaps1.Count();
            DataExportCommon.MergeCell(_sheet, 0, 0, 0, _count_cells - 1, "Сводные результаты государственной кадастровой оценки объектов недвижимости по кадастровому району (" + _cad_num + ")");
            DataExportCommon.AddRow(_sheet, 1, objcaps1.ToArray(), widths_cells.ToArray(), true, true, false);
            DataExportCommon.AddRow(_sheet, 2, objcaps2.ToArray(), widths_cells.ToArray(), true, true, false);
            _sheet.Rows[0].Height = 4 * 256;
            _sheet.Rows[1].Height = 4 * 256;
        }
    }
}