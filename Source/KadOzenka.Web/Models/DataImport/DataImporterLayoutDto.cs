using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using CommonSdks;
using Core.Main.FileStorages;
using Core.Register;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using Newtonsoft.Json;
using ObjectModel.Common;
using ObjectModel.Directory.Common;

namespace KadOzenka.Web.Models.DataImport
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
        public string DataFileName { get; set; }

        [Display(Name = "Имя результирующего файла")]
        public string ResultFileName { get; set; }
		public long? ErrorId { get; set; }
		public long? MainRegisterId { get; set; }
		public string RegisterViewId { get; set; }
		public string ResultMessage { get; set; }
		public string DataFileSizeKb { get; set; }
		public string DataFileSizeMb { get; set; }
		public string ResultFileSizeKb { get; set; }
		public string ResultFileSizeMb { get; set; }
		public string ColumnsMappingDtoListJson { get; set; }
        public long? NumberOfImportedObjects { get; set; }
        public long? TotalNumberOfObjects { get; set; }


        public static DataImporterLayoutDto OMMap(OMImportDataLog entity, string userName, ImportStatus? status)
		{
			if (entity == null)
			{
				return new DataImporterLayoutDto();
			}

			long? dataFileSize = null;
			string dataFileLocation = FileStorageManager.GetPathForFileFolder(DataImporterCommon.FileStorageName, entity.DateCreated);
			dataFileLocation = Path.Combine(dataFileLocation, DataImporterCommon.GetStorageDataFileName(entity.Id));
			if (!string.IsNullOrEmpty(dataFileLocation) && System.IO.File.Exists(dataFileLocation))
			{
				dataFileSize = new FileInfo(dataFileLocation).Length;
			}

			long? resultFileSize = null;
			string resultFileLocation = FileStorageManager.GetPathForFileFolder(DataImporterCommon.FileStorageName, entity.DateFinished.GetValueOrDefault());
			resultFileLocation = Path.Combine(resultFileLocation, DataImporterCommon.GetStorageResultFileName(entity.Id));
			if (!string.IsNullOrEmpty(resultFileLocation) && System.IO.File.Exists(resultFileLocation))
			{
				resultFileSize = new FileInfo(resultFileLocation).Length;
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
				DataFileName = entity.DataFileTitle,
				ResultFileName = entity.ResultFileTitle,
				ErrorId = entity.ErrorId,
				MainRegisterId = entity.MainRegisterId,
				RegisterViewId = entity.RegisterViewId,
				ResultMessage = entity.ResultMessage,
				DataFileSizeKb = dataFileSize.HasValue ? Convert.ToString(dataFileSize / 1024) : string.Empty,
				DataFileSizeMb = dataFileSize.HasValue ? Convert.ToString(dataFileSize / (1024 * 1024)) : string.Empty,
				ResultFileSizeKb = resultFileSize.HasValue ? Convert.ToString(resultFileSize / 1024) : string.Empty,
				ResultFileSizeMb = resultFileSize.HasValue ? Convert.ToString(resultFileSize / (1024 * 1024)) : string.Empty,
				ColumnsMappingDtoListJson = JsonConvert.SerializeObject(columnsMappingDtoList),
			    NumberOfImportedObjects = entity.NumberOfImportedObjects,
                TotalNumberOfObjects = entity.TotalNumberOfObjects
			};
		}
	}
}
