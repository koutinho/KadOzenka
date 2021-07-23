using System;
using System.Collections.Generic;
using KadOzenka.Dal.ChunkUpload;
using KadOzenka.Dal.ChunkUpload.Dtos;
using KadOzenka.Web.Models.UploadFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KadOzenka.Web.Controllers
{

	public class UploadFileController : Controller
	{
		public IActionResult ChunkSave(IEnumerable<IFormFile> files, string metaData, Guid uuid)
		{
			ChunkMetaData chunkData;

			chunkData = JsonConvert.DeserializeObject<ChunkMetaData>(metaData);

			if (files != null)
			{
				foreach (var file in files)
				{
					ChunkUploadManager.Instance().AddFileToStorage(uuid, new FileContentDto
					{
						FileStream = file.OpenReadStream(),
						FileName = chunkData?.FileName
					});
				}
				
			}

			ChunkResult fileBlob = new ChunkResult();

			if (chunkData != null)
			{
				fileBlob.uploaded = chunkData.TotalChunks - 1 <= chunkData.ChunkIndex;
				fileBlob.fileUid = chunkData.UploadUid;
			}

			return Json(fileBlob);
		}

		[HttpPost]
		public IActionResult DeleteFiles(Guid uuid)
		{ 
			bool isDeleted = ChunkUploadManager.Instance().DeleteFiles(uuid);
			return Json(isDeleted);
		}
	}
}
