using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using ObjectModel.KO;
using ObjectModel.Core.TD;
using KadOzenka.Dal.DataExport;

namespace KadOzenka.BlFrontEnd.ExportKO
{
    public static class ExporterKO
    {
        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMUnit. По 5000 записей. 
        /// Обертка для вызова DEKOUnit.ExportToXml
        /// </summary>
        public static void ExportXmlUnit()
        {
            string filename = "";
            string dir_name = "C:\\Temp\\KO_Cost";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == 17971700).SelectAll().Execute();
            int count_curr = 0;
            int count_all = units_all.Count();

            List<OMUnit> units_curr = new List<OMUnit>();
            int count_write = 5000;
            int count_file = 1;
            foreach (OMUnit unit in units_all)
            {
                units_curr.Add(unit);
                count_curr++;
                if (count_curr == count_write)
                {
                    filename = dir_name + "\\COST_" + ConfigurationManager.AppSettings["ucSender"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + count_file.ToString().PadLeft(4, '0') + ".xml";
                    Stream resultFile = DEKOUnit.ExportToXml(units_curr);
                    using (FileStream output = new FileStream(filename, FileMode.Create))
                    {
                        resultFile.CopyTo(output);
                    }
                    units_curr.Clear();
                    count_curr = 0;
                    count_file++;
                }
                count_curr++;
                Console.WriteLine("Выгружено " + count_curr.ToString() + " из " + count_all.ToString());
            }

            filename = dir_name + "\\COST_" + ConfigurationManager.AppSettings["ucSender"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + count_file.ToString().PadLeft(4, '0') + ".xml";
            Stream resultFile1 = DEKOUnit.ExportToXml(units_curr);
            using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
            {
                resultFile1.CopyTo(output);
            }
        }

        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMGroup
        /// Обертка для вызова DEKOGroup.ExportToXml
        /// </summary>
        public static void ExportXmlGroup()
        {
            string dir_name = "C:\\Temp\\KO_Groups";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            // Выбираем все подгруппы
            List<OMGroup> koGroups = OMGroup.Where(x => x.ParentId != -1).SelectAll().Execute();
            int countCurr = 0;
            int countAll = koGroups.Count();
            foreach (OMGroup subgroup in koGroups)
            {
                countCurr++;
                string str_message = "Выгружается группа " + countCurr.ToString() + " (Id=" + subgroup.Id.ToString() + ") из " + countAll.ToString();
                Console.WriteLine(str_message);

                subgroup.Unit = OMUnit.Where(x => x.GroupId == subgroup.Id).SelectAll().Execute();

                if (subgroup.Unit.Count > 0)
                {
                    Stream resultFile = DEKOGroup.ExportToXml(subgroup, str_message);

                    OMGroup parent_group = OMGroup.Where(x => x.Id == subgroup.ParentId).SelectAll().ExecuteFirstOrDefault();
                    string full_group_num = ((parent_group.Number == null ? parent_group.Id.ToString() : parent_group.Number)) + "." +
                                            ((subgroup.Number == null ? subgroup.Id.ToString() : subgroup.Number));
                    full_group_num = full_group_num.Replace("\n", "");

                    if (!Directory.Exists(dir_name + "\\" + full_group_num))
                        Directory.CreateDirectory(dir_name + "\\" + full_group_num);

                    string file_name = dir_name + "\\" + full_group_num + "\\FD_State_Cadastral_Valuation_" + subgroup.Id.ToString().PadLeft(5, '0') + countCurr.ToString().PadLeft(5, '0') + ".xml";
                    using (FileStream output = new FileStream(file_name, FileMode.Create))
                    {
                        resultFile.CopyTo(output);
                    }
                }
            }
        }

        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMUnit по Response Document
        /// Обертка для вызова DEKOResponseDoc.ExportToXml
        /// </summary>
        public static void ExportXmlRD()
        {
            string dir_name = "C:\\Temp\\KO_ResponseDoc";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            OMInstance response_doc = OMInstance.Where(x => x.Id == 100000008).SelectAll().ExecuteFirstOrDefault();
            if (response_doc != null)
                DEKOResponseDoc.ExportToXml(response_doc, dir_name);
        }

        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMUnit для ВУОН
        /// Обертка для вызова DEKOVuon.ExportToXml
        /// </summary>
        public static void ExportXmlVUON()
        {
            string dir_name = "C:\\Temp\\KO_Vuon";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            OMInstance response_doc = OMInstance.Where(x => x.Id == 100000008).SelectAll().ExecuteFirstOrDefault();
            if (response_doc != null)
                DEKOVuon.ExportToXml(response_doc, dir_name);
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 4. Группировка объектов недвижимости".
        /// Обертка для вызова DEKODifferent.ExportToXls4
        /// </summary>
        public static void ExportXlsTable4()
        {
            string dir_name = "D:\\Temp\\KO_Excel\\Table4";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            long? task_id = 1;
            DEKODifferent.ExportToXls4(task_id, dir_name);
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 5. Результаты моделирования".
        /// Обертка для вызова DEKODifferent.ExportToXls5
        /// </summary>
        public static void ExportXlsTable5()
        {
            string dir_name = "D:\\Temp\\KO_Excel\\Table5";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            long? task_id = 1;
            DEKODifferent.ExportToXls5(task_id, dir_name);
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 7. Обобщенные показатели по кадастровым районам".
        /// </summary>
        public static void ExportXlsTable7()
        {
            string dir_name = "D:\\Temp\\KO_Excel\\Table7";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            long? task_id = 1;
            DEKODifferent.ExportToXls7(task_id, ObjectModel.Directory.PropertyTypes.Building, dir_name);
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам".
        /// </summary>
        public static void ExportXlsTable8()
        {
            string dir_name = "D:\\Temp\\KO_Excel\\Table8";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            long? task_id = 1;
            DEKODifferent.ExportToXls8(task_id, ObjectModel.Directory.PropertyTypes.Building, dir_name);
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 9. Результаты определения кадастровой стоимости".
        /// Обертка для вызова DEKODifferent.ExportToXls9
        /// </summary>
        public static void ExportXlsTable9()
        {
            string dir_name = "D:\\Temp\\KO_Excel\\Table9";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            long? task_id = 1;
            DEKODifferent.ExportToXls9(task_id, dir_name);
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 10. Результаты государственной кадастровой оценки".
        /// Обертка для вызова DEKODifferent.ExportToXls10
        /// </summary>
        public static void ExportXlsTable10()
        {
            string dir_name = "D:\\Temp\\KO_Excel\\Table10";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            long? task_id = 1;
            DEKODifferent.ExportToXls10(task_id, dir_name);
        }

        /// <summary>
        /// Выгрузка в Excel "Таблица 11. Сводные результаты по кадастровому району".
        /// Обертка для вызова DEKODifferent.ExportToXls11
        /// </summary>
        public static void ExportXlsTable11()
        {
            string dir_name = "D:\\Temp\\KO_Excel\\Table11";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            long? task_id = 1;
            DEKODifferent.ExportToXls11(task_id, dir_name);
        }
    }
}
