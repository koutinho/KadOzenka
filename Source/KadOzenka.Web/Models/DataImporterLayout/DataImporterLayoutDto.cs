using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Directory.Common;

namespace KadOzenka.Web.Models.DataImporterLayout
{
	public class DataImporterLayoutDto
	{
		public long? Id { get; set; }

        [Display(Name = "Автор")]
        public string UserName { get; set; }

		public ImportStatus? Status { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime? DateCreated { get; set; }
		public DateTime? DateStarted { get; set; }
		public DateTime? DateFinished { get; set; }

        [Display(Name = "Имя файла")]
        public string TemplateFileName { get; set; }
		public long? ErrorId { get; set; }
		public long? MainRegisterId { get; set; }
		public string RegisterViewId { get; set; }
		public string ResultMessage { get; set; }
		public string FileSizeKb { get; set; }
		public string FileSizeMb { get; set; }
		public string ColumnsMappingDtoListJson { get; set; }


		public static DataImporterLayoutDto OMMap(OMImportDataLog entity, string userName, ImportStatus? status)
		{
			if (entity == null)
			{
				return new DataImporterLayoutDto();
			}

			var fileLocation =
				Path.Combine(
					FileStorageManager.GetPathForFileFolder(DataImporterCommon.FileStorageName, entity.DateCreated),
					DataImporterCommon.GetTemplateName(entity.Id));
			long? fileSize = null;
			if (!string.IsNullOrEmpty(fileLocation) && System.IO.File.Exists(fileLocation))
			{
				fileSize = new FileInfo(fileLocation).Length;
			}

			List<ColumnsMappingDto> columnsMappingDtoList = null;

			if(entity.ColumnsMapping.IsNotEmpty())
			{
				var dbColumns = JsonConvert.DeserializeObject<List<DataExportColumn>>(entity.ColumnsMapping);

				columnsMappingDtoList = dbColumns.Select(x => new ColumnsMappingDto
				{
					ColumnName = x.ColumnName,
					AttributeName = RegisterCache.GetAttributeData((int)x.AttributrId).Name,
					IsKey = x.IsKey
				}).ToList();
			}
			
			return new DataImporterLayoutDto
			{
				Id = entity.Id,
				UserName = userName,
				Status = status,
				DateCreated = entity.DateCreated,
				DateStarted = entity.DateStarted,
				DateFinished = entity.DateFinished,
				TemplateFileName = entity.DataFileName,
				ErrorId = entity.ErrorId,
				MainRegisterId = entity.MainRegisterId,
				RegisterViewId = entity.RegisterViewId,
				ResultMessage = entity.ResultMessage,
				FileSizeKb = fileSize.HasValue ? Convert.ToString(fileSize / 1024) : string.Empty,
				FileSizeMb = fileSize.HasValue ? Convert.ToString(fileSize / (1024 * 1024)) : string.Empty,
				ColumnsMappingDtoListJson = JsonConvert.SerializeObject(columnsMappingDtoList)
			};
		}
	}
}
