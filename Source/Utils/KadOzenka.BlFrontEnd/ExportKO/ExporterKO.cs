using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using ObjectModel.KO;
using ObjectModel.Core.TD;

namespace KadOzenka.BlFrontEnd.ExportKO
{
    public static class ExporterKO
    {
        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMUnit. По 5000 записей. 
        /// </summary>
        public static void ExportXmlUnit()
        {
            string filename = "";
            string dir_name = "C:\\Temp\\KO_Cost";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            List<OMUnit> units_all = OMUnit.Where(x => x.TaskId == 17971700 /*x => x.GroupId == 100003*/).SelectAll().Execute();
            int count_curr = 0;
            int count_all = units_all.Count();

            List<ObjectModel.KO.OMUnit> units_curr = new List<ObjectModel.KO.OMUnit>();
            int count_write = 5000;
            int count_file = 1;
            foreach (OMUnit unit in units_all)
            {
                units_curr.Add(unit);
                count_curr++;
                if (count_curr == count_write)
                {
                    filename = dir_name + "\\COST_" + ConfigurationManager.AppSettings["ucSender"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + count_file.ToString().PadLeft(4, '0') + ".xml";
                    Stream resultFile = Dal.DataExport.DEKOUnit.ExportToXml(units_curr);
                    using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
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
            Stream resultFile1 = Dal.DataExport.DEKOUnit.ExportToXml(units_curr);
            using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
            {
                resultFile1.CopyTo(output);
            }
        }

        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMGroup
        /// </summary>
        public static void ExportXmlGroup()
        {
            string dir_name = "C:\\Temp\\KO_Groups";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            // Выбираем все подгруппы
            List<ObjectModel.KO.OMGroup> koGroups = ObjectModel.KO.OMGroup.Where(x => x.ParentId != -1).SelectAll().Execute();
            int countCurr = 0;
            int countAll = koGroups.Count();
            foreach (OMGroup subgroup in koGroups)
            {
                countCurr++;
                string str_message = "Выгружается группа" + countCurr.ToString() + " из " + countAll.ToString() + " -- Id= " + subgroup.Id.ToString();
                Console.WriteLine(str_message);

                subgroup.Unit = OMUnit.Where(x => x.GroupId == subgroup.Id).SelectAll().Execute();

                if (subgroup.Unit.Count > 0)
                {
                    Stream resultFile = Dal.DataExport.DEKOGroup.ExportToXml(subgroup, str_message);

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
        /// </summary>
        public static void ExportXmlRD()
        {
            string dir_name = "C:\\Temp\\KO_ResponseDoc";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            OMInstance response_doc = OMInstance.Where(x => x.Id == 100000008).SelectAll().ExecuteFirstOrDefault();
            if (response_doc != null)
                Dal.DataExport.DEKOResponseDoc.ExportToXml(response_doc, dir_name);
        }

        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMUnit для ВУОН
        /// </summary>
        public static void ExportXmlVUON()
        {
            string dir_name = "C:\\Temp\\KO_Vuon";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            OMInstance response_doc = OMInstance.Where(x => x.Id == 100000008).SelectAll().ExecuteFirstOrDefault();
            if (response_doc != null)
                Dal.DataExport.DEKOVuon.ExportToXml(response_doc, dir_name);
        }
    }
}
