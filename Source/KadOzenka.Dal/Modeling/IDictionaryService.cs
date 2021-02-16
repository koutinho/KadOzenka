using System;
using System.Collections.Generic;
using System.IO;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Common;
using ObjectModel.Directory.ES;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
{
	public interface IDictionaryService
	{
		int RowsCount { get; set; }
		int CurrentRow { get; set; }

		bool MustUseLongProcess(Stream fileStream);
		List<OMModelingDictionary> GetDictionaries();
		List<OMModelingDictionary> GetDictionaries(List<long> dictionaryIds, bool withItems = true);
		OMModelingDictionary GetDictionaryById(long id);
		long CreateDictionary(string name, ReferenceItemCodeType valueType);
		void UpdateDictionary(long id, string newName, ReferenceItemCodeType newValueType);
		void DeleteDictionary(long id);
		decimal GetCoefficientFromStringFactor(string stringValue, OMModelingDictionary dictionary);
		decimal GetCoefficientFromDateFactor(DateTime? date, OMModelingDictionary dictionary);
		decimal GetCoefficientFromNumberFactor(decimal? number, OMModelingDictionary dictionary);
		List<OMModelingDictionariesValues> GetDictionaryValues(long dictionaryId);
		OMModelingDictionariesValues GetDictionaryValueById(long id);
		long CreateDictionaryValue(DictionaryValueDto dto);
		void UpdateDictionaryValue(DictionaryValueDto dto);
		void DeleteDictionaryValue(long id);
		void ValidateDictionaryValue(OMModelingDictionary dictionary, string value, long dictionaryValueId);

		long CreateDictionaryFromExcel(Stream fileStream, DictionaryImportFileInfoDto fileImportInfo,
			string newDictionaryName, OMImportDataLog import);

		void UpdateDictionaryFromExcel(Stream fileStream, DictionaryImportFileInfoDto fileImportInfo,
			long dictionaryId, bool deleteOldValues, OMImportDataLog import);
	}
}
