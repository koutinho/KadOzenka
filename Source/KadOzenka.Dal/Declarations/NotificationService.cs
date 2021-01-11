using System;
using System.IO;
using System.Linq;
using System.Web;
using Core.Register.Enums;
using Core.Shared.Extensions;
using ObjectModel.Core.Reports;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;
using Platform.Reports;
using SpdIntegration.Request;
using SpdIntegration.SpdServiceWebReference;

namespace KadOzenka.Dal.Declarations
{
	public class NotificationService
	{
		public OMUved GetNotification(long notificationId)
		{
			var omUved = OMUved
				.Where(x => x.Id == notificationId)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (omUved == null)
			{
				throw new Exception($"Уведомление с ИД {notificationId} не найдено");
			}

			return omUved;
		}

		public void SendNotificationToSpd(long notificationId)
		{
			
			var uved = GetNotification(notificationId);
			var registerId = OMUved.GetRegisterId();
			var reportType = GetNotificationReportType(uved);
			var savedReport = OMSavedReport.Where(x =>
					x.ObjectId == notificationId &&
					x.ObjectRegisterId == registerId &&
					x.Code == reportType &&
					(x.IsDeleted == null || x.IsDeleted == false)).SelectAll()
				.Execute()
				.FirstOrDefault();

			if (savedReport == null)
			{
				throw new Exception("Сохраненный отчет не найден.");
			}


			string fileLocation = ReportStorage.GetFileLocation(savedReport.Id, savedReport.FileType);

			if (File.Exists(fileLocation))
			{
				RegistersExportType fileType = savedReport.FileType.ParseToEnum<RegistersExportType>();
				var data = File.ReadAllBytes(fileLocation);
				string downloadFileName = HttpUtility.UrlDecode(ReportStorage.GetFileName(savedReport, fileType));

				var appId = OMDeclaration.Where(x => x.Id == uved.Declaration_Id).SelectAll().ExecuteFirstOrDefault()?.SpdAppId;

				if (appId == null)
				{
					throw new Exception("У деклорации отсутствует SpdAppId");
				}

				ApplicationDocument spdDoc = new ApplicationDocument
				{
					FILENAME = downloadFileName,
					BYTES = data,
					DOCNAME = savedReport.Title,
					DEFINITION = savedReport.Title,
					DOCDATE = savedReport.CreateDate,
				};

				BaseResponse response = SpdRequest.AddApplicationDocument(appId.GetValueOrDefault(), spdDoc);

			}
			else
			{
				throw new Exception("Не был найден файл");
			}
			
		}

		public int GetNotificationReportType(OMUved omUved)
		{
			int reportType;
			switch (omUved?.Type_Code)
			{
				case UvedType.Item5:
					reportType = 1001;
					break;
				case UvedType.Item3:
					reportType = 1002;
					break;
				case UvedType.Item4:
					reportType = 1003;
					break;
				case UvedType.Item1:
					reportType = 1004;
					break;
				default:
					throw new Exception(
						$"Тип уведомления '{omUved?.Type_Code.GetEnumDescription()}' не поддерживает формирование по шаблону");
			}

			return reportType;
		}
	}
}