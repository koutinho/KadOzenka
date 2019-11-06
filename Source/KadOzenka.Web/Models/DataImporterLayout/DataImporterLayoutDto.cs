﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using Core.Register;
using Core.Register.Enums;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using Newtonsoft.Json;
using ObjectModel.Common;

namespace KadOzenka.Web.Models.DataImporterLayout
{
	public class DataImporterLayoutDto
	{
		public class ColumnsMappingDto
		{
			public string ColumnName { get; set; }
			public string AttributeName { get; set; }
			public bool IsKey { get; set; }
		}

		public long? Id { get; set; }
		public string UserName { get; set; }
		public RegistersExportStatus? Status { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateStarted { get; set; }
		public DateTime? DateFinished { get; set; }
		public string TemplateFileName { get; set; }
		public long? ErrorId { get; set; }
		public long? MainRegisterId { get; set; }
		public string RegisterViewId { get; set; }
		public string ResultMessage { get; set; }
		public string FileSizeKb { get; set; }
		public string FileSizeMb { get; set; }
		public List<ColumnsMappingDto> ColumnsMappingDtoList { get; set; }


		public static DataImporterLayoutDto OMMap(OMImportFromTemplates entity, string userName, RegistersExportStatus? status)
		{
			if (entity == null)
			{
				return new DataImporterLayoutDto();
			}

			var fileLocation =
				Path.Combine(
					FileStorageManager.GetPathForFileFolder(DataImporter.FileStorageName, entity.DateCreated),
					DataImporter.GetTemplateName(entity.Id));
			long? fileSize = null;
			if (!string.IsNullOrEmpty(fileLocation) && System.IO.File.Exists(fileLocation))
			{
				fileSize = new FileInfo(fileLocation).Length;
			}

			var dbColumns = JsonConvert.DeserializeObject<List<DataExportColumn>>(entity.ColumnsMapping);
			var columnsMappingDtoList = dbColumns.Select(x => new ColumnsMappingDto
			{
				ColumnName = x.ColumnName,
				AttributeName = RegisterCache.GetAttributeData((int)x.AttributrId).Name,
				IsKey = x.IsKey
			}).ToList();

			return new DataImporterLayoutDto
			{
				Id = entity.Id,
				UserName = userName,
				Status = status.GetValueOrDefault(),
				DateCreated = entity.DateCreated,
				DateStarted = entity.DateStarted,
				DateFinished = entity.DateFinished,
				TemplateFileName = entity.TemplateFileName,
				ErrorId = entity.ErrorId,
				MainRegisterId = entity.MainRegisterId,
				RegisterViewId = entity.RegisterViewId,
				ResultMessage = entity.ResultMessage,
				FileSizeKb = fileSize.HasValue ? Convert.ToString(fileSize / 1024) : string.Empty,
				FileSizeMb = fileSize.HasValue ? Convert.ToString(fileSize / (1024 * 1024)) : string.Empty,
				ColumnsMappingDtoList = columnsMappingDtoList
			};
		}
	}
}
