using System;
using System.IO;
using Core.Main.FileStorages;

namespace KadOzenka.Dal.CommonFunctions
{
	public interface IFileStorageManagerWrapper
	{
		FileStream GetFileStream(string storageKey, DateTime fileDate, string realFileName);
	}

	public class FileStorageManagerWrapper : IFileStorageManagerWrapper
	{
		public FileStream GetFileStream(string storageKey, DateTime fileDate, string realFileName)
		{
			return FileStorageManager.GetFileStream(storageKey, fileDate, realFileName);
		}
	}
}
