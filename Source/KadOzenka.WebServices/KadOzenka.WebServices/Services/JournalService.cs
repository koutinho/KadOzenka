using System;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
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
		public KoResultMessage ReadLastRecord()
		{
			KoResultMessage resRecord = new KoResultMessage();
			try
			{
				var record = _appContext.ReonJournal.Where(x => x.ConfirmDate == null).OrderBy(x => x.CreateDate).FirstOrDefault();

				if (record == null) return null;

				var updatedRecord= UpdateReadDate(record);

				var task = GetTaskById((int) record.TaskId);
				resRecord.CreateDate = updatedRecord.CreateDate.Truncate(TimeSpan.FromSeconds(1));
				resRecord.Guid = updatedRecord.Guid;
				resRecord.YearTour = GetTourByTaskId((int)record.TaskId)?.Year ?? 0;
				resRecord.DateCreatedTask = task?.CreationDate.Truncate(TimeSpan.FromSeconds(1));
				resRecord.RegNumber = _appContext.TdInstances.FirstOrDefault(x => x.Id == task.DocumentId)?.RegNumber;
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
			string storageName = "DataExporterByTemplate";
			string defaultContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			var res =  new ResultLoadFileDto();
			string defaultEx = "xlsx";
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

				stream = FileStorageManager.GetFileStream(storageName, template.DateFinished.GetValueOrDefault(), template.ResultFileName);
				res.Stream = stream;
				res.ContentType = GetContentType(template.FileExtension) ?? defaultContentType;
				res.FileName = $"{template.FileResultTitle}.{(!string.IsNullOrEmpty(template.FileExtension) ? template.FileExtension : defaultEx)}";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return res;
		}

		#region support method

		private string GetContentType(string fileExtension)
		{
			if (string.IsNullOrEmpty(fileExtension))
			{
				return null;
			}
			if (fileExtension == "xlsx")
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			}
			if (fileExtension == "xml")
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

		private Task GetTaskById(int taskId)
		{
			return _appContext.Tasks.FirstOrDefault(x => x.Id == taskId);
		}

		private Tour GetTourByTaskId(int taskId)
		{
			var task = GetTaskById(taskId);

			if (task == null) return null;

			return _appContext.Tours.FirstOrDefault(x => x.Id == task.TourId);
		}

		#endregion


	}
}