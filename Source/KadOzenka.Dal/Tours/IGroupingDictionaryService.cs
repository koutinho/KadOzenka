using System;
using System.Collections.Generic;
using System.IO;
using KadOzenka.Dal.Tours.Dto;
using ObjectModel.Common;
using ObjectModel.Directory.ES;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tours
{
	public interface IGroupingDictionaryService
	{
		int RowsCount { get; set; }
		int CurrentRow { get; set; }

		bool MustUseLongProcess(Stream fileStream);
		List<OMGroupingDictionary> GetDictionaries();
		List<OMGroupingDictionary> GetDictionaries(List<long> dictionaryIds, bool withItems = true);
		OMGroupingDictionary GetDictionaryById(long id);
		long CreateDictionary(string name, ReferenceItemCodeType valueType);
		void UpdateDictionary(long id, string newName, ReferenceItemCodeType newValueType);
		void DeleteDictionary(long id);
		List<OMGroupingDictionariesValues> GetDictionaryValues(long dictionaryId);
		OMGroupingDictionariesValues GetDictionaryValueById(long id);
		long CreateDictionaryValue(GroupingDictionaryValueDto dto);
		void UpdateDictionaryValue(GroupingDictionaryValueDto dto);
		void DeleteDictionaryValue(long id);
		void ValidateDictionaryValue(OMGroupingDictionary dictionary, string value, long dictionaryValueId);

		long CreateDictionaryFromExcel(Stream fileStream, GroupingDictionaryImportFileInfoDto fileImportInfo,
			string newDictionaryName, OMImportDataLog import);

		void UpdateDictionaryFromExcel(Stream fileStream, GroupingDictionaryImportFileInfoDto fileImportInfo,
			long dictionaryId, bool deleteOldValues, OMImportDataLog import);
	}
}
