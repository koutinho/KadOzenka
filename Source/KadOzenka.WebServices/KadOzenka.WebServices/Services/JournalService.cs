﻿using System;
using System.Linq;
using KadOzenka.WebServices.Domain.Context;
using KadOzenka.WebServices.Domain.Model;

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
		public ReonJournal ReadLastRecord()
		{
			ReonJournal resRecord;
			try
			{
				var record = _appContext.ReonJournal.Where(x => x.ConfirmDate == null).OrderBy(x => x.CreateDate).FirstOrDefault();
				resRecord = UpdateReadDate(record);
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

	}
}