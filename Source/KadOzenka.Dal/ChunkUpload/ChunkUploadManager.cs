using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Main.FileStorages;
using DocumentFormat.OpenXml.Drawing.Charts;
using KadOzenka.Dal.ChunkUpload.Dtos;
using KadOzenka.Dal.ChunkUpload.Model;
using Serilog;

namespace KadOzenka.Dal.ChunkUpload
{
	public class ChunkUploadManager: IDisposable
	{
		private ILogger _log = Log.ForContext<ChunkUploadManager>();
		private readonly string storageKey = "TempFileStorage";
		private readonly Dictionary<Guid, List<FileDataModel>> _packageStorage = new ();
		private static ChunkUploadManager _instance;

		private ChunkUploadManager()
		{
		}

		public static ChunkUploadManager Instance()
		{
			return _instance ??= new ChunkUploadManager();
		}

		public void AddFileToStorage(Guid uuid, FileContentDto content)
		{
			if (_packageStorage.ContainsKey(uuid))
			{
				var fileModel = _packageStorage[uuid].FirstOrDefault(x => x.FileName == content.FileName);
				if (fileModel != null)
				{
					
					AppendToFile(content, fileModel);
				}
				else
				{
					AddFile(content, uuid);
				}
			}
			else
			{
				_packageStorage.Add(uuid, new());
				AddFile(content, uuid);
			}
		}

		public List<FileContentDto> GetFilesByUuid(Guid uuid)
		{
			List<FileContentDto> res = new();

			if (_packageStorage.ContainsKey(uuid))
			{
				var dataFiles = _packageStorage[uuid];

				foreach (var dataFile in dataFiles)
				{
					var pathFolder = FileStorageManager.GetPathForFileFolder(storageKey, dataFile.DateCreated);
					var path = Path.Combine(pathFolder, dataFile.FileNameToSave);
					if (File.Exists(path))
					{
						var fi = new FileInfo(path);
						using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, (int)fi.Length, FileOptions.DeleteOnClose);

						var fileContent = new FileContentDto
						{
							FileName = dataFile.FileName,
							FileStream = new MemoryStream()
						};
						stream.CopyTo(fileContent.FileStream);
						res.Add(fileContent);
					}
				}
				_log.ForContext("files", _packageStorage[uuid], true).Debug("Отдали файлы по {uuid}", uuid);
			}
			else
			{
				_log.Warning("Файлы для uuid = {uuid} не надены", uuid);

			}
		
			CheckFileAndDeleteRecord(uuid);
			return res;
		}

		public bool DeleteFiles(Guid uuid)
		{
			try
			{
				if (_packageStorage.ContainsKey(uuid))
				{
					var dataFiles = _packageStorage[uuid];
					var dataFilesToRemove = new List<FileDataModel>();
					foreach (var dataFile in dataFiles)
					{
						var pathFolder = FileStorageManager.GetPathForFileFolder(storageKey, dataFile.DateCreated);
						var path = Path.Combine(pathFolder, dataFile.FileNameToSave);
						if (File.Exists(path))
						{
							var fi = new FileInfo(path);
							using FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, (int)fi.Length, FileOptions.DeleteOnClose);
							dataFilesToRemove.Add(dataFile);
						}
					}
					dataFilesToRemove.ForEach(x => _packageStorage[uuid].Remove(x));
					if (_packageStorage[uuid].Count == 0)
					{
						_packageStorage.Remove(uuid);
					}
				}

				return true;
			}
			catch (Exception e)
			{
				_log.Error(e, e.Message);
				return false;
			}

		}

		#region inside methods

		private void CheckFileAndDeleteRecord(Guid uuid)
		{
			if (_packageStorage.ContainsKey(uuid))
			{
				var dataFiles = _packageStorage[uuid];
				var dataFilesToRemove = new List<FileDataModel>();
				foreach (var dataFile in dataFiles)
				{
					var pathFolder = FileStorageManager.GetPathForFileFolder(storageKey, dataFile.DateCreated);
					var path = Path.Combine(pathFolder, dataFile.FileNameToSave);
					if (!File.Exists(path))
					{
						dataFilesToRemove.Add(dataFile);
					}
				}
				dataFilesToRemove.ForEach(x => _packageStorage[uuid].Remove(x));
				_log.Debug("Удалили файлы для uuid = {uuid}, количество файлов = {count}", uuid, _packageStorage[uuid].Count);
				if (_packageStorage[uuid].Count == 0)
				{
					_packageStorage.Remove(uuid);
				}

			}
		}

		private void AppendToFile(FileContentDto content, FileDataModel data)
		{
			try
			{
				var fullPath = FileStorageManager.GetFullFileName(storageKey, data.DateCreated, data.FileNameToSave);
				using FileStream stream = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				content.FileStream.CopyTo(stream);
			}
			catch (Exception e)
			{
				_log.Error(e, e.Message);
				throw;
			}

		}

		private void AddFile(FileContentDto content,  Guid uuid)
		{
			try
			{
				var fileData = new FileDataModel
				{
					FileName = content.FileName,
					DateCreated = DateTime.Now,
					FileNameToSave = Guid.NewGuid().ToString()

				};
				FileStorageManager.Save(content.FileStream, storageKey, fileData.DateCreated, fileData.FileNameToSave);
				_packageStorage[uuid].Add(fileData);
			}
			catch (Exception e)
			{
				_log.Error(e, e.Message);
				throw;
			}
		}


		#endregion

		public void Dispose()
		{
			var path = FileStorageManager.GetPathForStorage(storageKey);
			if (Directory.Exists(path))
			{
				Directory.Delete(path);
			}
		}
	}

}