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

        public static void ExportDocOtvet()
        {
            string dir_name = "C:\\Temp\\KO_Otvet";
            if (!Directory.Exists(dir_name))
                Directory.CreateDirectory(dir_name);

            OMUnit unit = OMUnit.Where(x => x.CadastralNumber == "77:02:0025011:1236").SelectAll().ExecuteFirstOrDefault();
            if (unit != null)
                DEKODocOtvet.ExportToDoc(unit, dir_name);
        }
    }
}
