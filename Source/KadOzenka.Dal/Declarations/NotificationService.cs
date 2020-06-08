﻿using System;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using Core.Register.Enums;
using Core.Shared.Extensions;
using KadOzenka.Dal.Spd;
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
		private const string _reportStorage = "SaveReportPath";

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
				string downloadFileName = ReportStorage.GetFileName(savedReport, fileType);

				var spdRequest = new SpdApplicationRequest();
				SpdRequest service = new SpdRequest();

				//Отправка заявки
				var responceAppl = service.CreateFullApplication(
					spdRequest.ProfileName,
					OMUved.GetRegisterId(),
					savedReport.Id.ToString(),
					spdRequest.Params
				);

				if (responceAppl.error)
				{
					throw new Exception($"Ошибка создания заявки в СПД: {responceAppl.message}");
				}


				ApplicationDocument spdDoc = new ApplicationDocument()
				{
					FILENAME = savedReport.Title,
					BYTES = data,
					DOCNAME = downloadFileName,
					DEFINITION = savedReport.Comments
				};

				BaseResponse response = SpdRequest.AddApplicationDocument(responceAppl.APPID.ParseToLong(), spdDoc);

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