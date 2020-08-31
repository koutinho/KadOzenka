using System;
using System.IO;
using ObjectModel.KO;
using ObjectModel.Core.TD;
using KadOzenka.Dal.DataExport;
using Core.Main.FileStorages;
using Core.SRD;
using Newtonsoft.Json;

namespace KadOzenka.BlFrontEnd.ExportKO
{
    public static class ExporterKO
    {
        public static void ReportProgress(int prog, bool endOfProcess, string progressMessage)
        {
            // TO DO
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
            {
	            var setting = new KOUnloadSettings
		            { UnloadDEKOResponseDocExportToXml = true, IdResponseDocument = response_doc.Id };
                var koUnloadResults = KOUnloadResult.GetKoUnloadResultTypes(setting);
                
                var unloadResultQueue =
	                new OMUnloadResultQueue
	                {
		                UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
		                DateCreated = DateTime.Now,
                        DateStarted = DateTime.Now,
		                Status_Code = ObjectModel.Directory.Common.ImportStatus.Running,
		                UnloadTypesMapping = JsonConvert.SerializeObject(koUnloadResults),
		                UnloadCurrentCount = 0,
		                UnloadTotalCount = koUnloadResults.Count
	                };
                unloadResultQueue.Save();

                try
                {
                    DEKOResponseDoc.ExportToXml(unloadResultQueue, setting, ReportProgress);

	                unloadResultQueue.DateFinished = DateTime.Now;
	                unloadResultQueue.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
	                unloadResultQueue.Save();
                }
                catch (Exception e)
                {
	                unloadResultQueue.DateFinished = DateTime.Now;
	                unloadResultQueue.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
	                unloadResultQueue.ErrorMessage = e.Message;
	                unloadResultQueue.Save();
                    throw;
                }
                
            }
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
            {
	            var setting = new KOUnloadSettings
		            { UnloadDEKOVuonExportToXml = true, IdResponseDocument = response_doc.Id };
	            var koUnloadResults = KOUnloadResult.GetKoUnloadResultTypes(setting);

	            var unloadResultQueue =
		            new OMUnloadResultQueue
		            {
			            UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
			            DateCreated = DateTime.Now,
			            DateStarted = DateTime.Now,
                        Status_Code = ObjectModel.Directory.Common.ImportStatus.Running,
			            UnloadTypesMapping = JsonConvert.SerializeObject(koUnloadResults),
			            UnloadCurrentCount = 0,
			            UnloadTotalCount = koUnloadResults.Count
		            };
	            unloadResultQueue.Save();

	            try
	            {
                    DEKOVuon.ExportToXml(unloadResultQueue, setting, ReportProgress);

		            unloadResultQueue.DateFinished = DateTime.Now;
		            unloadResultQueue.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
		            unloadResultQueue.Save();
	            }
	            catch (Exception e)
	            {
		            unloadResultQueue.DateFinished = DateTime.Now;
		            unloadResultQueue.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
		            unloadResultQueue.ErrorMessage = e.Message;
		            unloadResultQueue.Save();
                    throw;
	            }
            }
        }

        /// <summary>
        /// Выгрузка в Word документа о предоставлении разъяснений
        /// Обертка для вызова DEKODocOtvet.ExportDocOtvet
        /// </summary>
        public static void ExportDocOtvet()
        {
            OMUnit unit = OMUnit.Where(x => x.CadastralNumber == "77:02:0025011:1236").SelectAll().ExecuteFirstOrDefault();
            Stream resultFile = DEKODocOtvet.ExportToDoc(unit);
            if (resultFile != null)
            {
                string StorageName = "KoExportResult";
                FileStorageManager.Save(resultFile, StorageName, DateTime.Now, "C:\\Temp\\KO_Otvet\\Otvet.docx");
            }
            else
                throw new Exception($"Документ предоставления разъяснений не создан.");
        }
    }
}
