using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KadOzenka.BlFrontEnd.ExportKO
{
    class ExporterKO
    {
        /// <summary>
        /// Выгрузка в XML из ObjectModel.KO.OMUnit. По 5000 записей. 
        /// </summary>
        public static void ExportXml1()
        {
            string filename = @"D:\Temp\ko_xml_";
            List<ObjectModel.KO.OMUnit> UnitsAll = ObjectModel.KO.OMUnit.Where().SelectAll().Execute();
            int countCurr = 0;
            int countAll = UnitsAll.Count();

            List<ObjectModel.KO.OMUnit> UnitsCurr = new List<ObjectModel.KO.OMUnit>();
            int count_write = 5000;
            int count_curr = 0;
            int count_file = 1;
            foreach (ObjectModel.KO.OMUnit unit in UnitsAll)
            {
                UnitsCurr.Add(unit);
                count_curr++;
                if (count_curr == count_write)
                {
                    string filename_new = filename + count_file.ToString() + ".xml";
                    Stream resultFile = KadOzenka.Dal.DataExport.DataExporterKO.ExportCostToXml(UnitsCurr);
                    using (System.IO.FileStream output = new System.IO.FileStream(filename_new, FileMode.Create))
                    {
                        resultFile.CopyTo(output);
                    }
                    UnitsCurr.Clear();
                    count_curr = 0;
                    count_file++;
                }
                countCurr++;
                Console.WriteLine("Выгружено " + countCurr.ToString() + " из " + countAll.ToString());
            }

            filename = filename + count_file.ToString() + ".xml";
            Stream resultFile1 = KadOzenka.Dal.DataExport.DataExporterKO.ExportCostToXml(UnitsCurr);
            using (System.IO.FileStream output = new System.IO.FileStream(filename, FileMode.Create))
            {
                resultFile1.CopyTo(output);
            }
        }
    }
}
