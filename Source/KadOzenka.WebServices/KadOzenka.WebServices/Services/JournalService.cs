using System;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using KadOzenka.WebServices.Domain.Context;
using KadOzenka.WebServices.Domain.Model;
using KadOzenka.WebServices.Services.ModelDto;

namespace KadOzenka.WebServices.Services
{
	/// <summary>
	/// Journal service
	/// </summary>
	public class JournalService
	{
		private ApplicationContext _appContext;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="appContext"></param>
		public JournalService(ApplicationContext appContext)
		{
			_appContext = appContext;
		}

		/// <summary>
		/// Get last record
		/// </summary>
		/// <returns>Returns the first record without confirm date</returns>
		public RecordDto ReadLastRecord()
		{
			RecordDto resRecord = new RecordDto();
			try
			{
				var record = _appContext.ReonJournal.Where(x => x.ConfirmDate == null).OrderBy(x => x.CreateDate).FirstOrDefault();
				var updatedRecord= UpdateReadDate(record);
				resRecord.ReportId = (int)updatedRecord.ResultReportId;
				resRecord.CreateDate = updatedRecord.CreateDate;
				resRecord.Guid = updatedRecord.Guid;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return resRecord;
		}


		/// <summary>
		/// The method is setting confirm date for current record
		/// </summary>
		/// <param name="guidRecord"></param>
		/// <returns></returns>
		public bool Confirm(Guid guidRecord)
		{
			bool res = false;

			try
			{
				var record = _appContext.ReonJournal.FirstOrDefault(x => x.Guid == guidRecord.ToString());

				if (record != null)
				{
					UpdateConfirmDate(record);
					res = true;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return res;
		}

		/// <summary>
		/// Get file stream by guid
		/// </summary>
		/// <param name="guidRecord"></param>
		/// <returns></returns>
		public ResultLoadFileDto GetFileReport(Guid guidRecord)
		{

			string storageName = "KoExportResult";
			string defaultContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			var res =  new ResultLoadFileDto();
			string defaultEx = ".xlsx";
			FileStream stream;
			try
			{
				var record = _appContext.ReonJournal.FirstOrDefault(x => x.Guid == guidRecord.ToString());

				if (record == null || record.ResultReportId == 0)
				{
					return null;
				}

				var template = _appContext.ExportTemplate.FirstOrDefault(x => x.Id == record.ResultReportId);

				if (template == null)
				{
					return null;
				}

				stream = FileStorageManager.GetFileStream(storageName, template.DateCreate, template.Id.ToString());
				res.Stream = stream;
				res.ContentType = GetContentType(template.TemplateFileName) ?? defaultContentType;
				res.FileName = template.TemplateFileName;
				if (string.IsNullOrEmpty(Path.GetExtension(res.FileName)))
				{
					res.FileName += defaultEx;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return res;
		}

		#region support method

		private string GetContentType(string fileName)
		{
			string ex = Path.GetExtension(fileName);

			if (string.IsNullOrEmpty(ex))
			{
				return null;
			}
			if (ex == ".xlsx")
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			}
			if (ex == ".xml")
			{
				return "application/xml";
			}

			return null;
		}

		private ReonJournal UpdateReadDate(ReonJournal record)
		{
			record.SendDate = DateTime.Now.Date;
			_appContext.SaveChanges();
			return _appContext.ReonJournal.FirstOrDefault(x => x.Id == record.Id);
		}

		private void UpdateConfirmDate(ReonJournal record)
		{
			record.ConfirmDate = DateTime.Now.Date;
			_appContext.SaveChanges();
		}

		#endregion


	}
}