﻿using System;
using KadOzenka.Web.Models.KoBase;

namespace KadOzenka.Web.Models.GeneralReports
{
	public class ReportFileGeneralInfoModel
	{
		public string User { get; set; }
		public string Status { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime? FinishDate { get; set; }
		public string FileName { get; set; }
		public FileSize FileSize { get; set; }


		public ReportFileGeneralInfoModel()
		{
			FileSize = new FileSize();
		}
	}
}
