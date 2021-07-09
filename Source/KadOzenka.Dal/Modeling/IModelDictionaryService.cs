﻿using System;
using System.Collections.Generic;
using System.IO;
using Core.Register;
using KadOzenka.Dal.Modeling.Dto;
using ObjectModel.Common;
using ObjectModel.KO;

namespace KadOzenka.Dal.Modeling
{
	public interface IModelDictionaryService
	{
		int RowsCount { get; set; }
		int CurrentRow { get; set; }

		bool MustUseLongProcess(Stream fileStream);
		List<OMModelingDictionary> GetDictionaries();
		List<OMModelingDictionary> GetDictionaries(List<long> dictionaryIds, bool withItems = true);
		OMModelingDictionary GetDictionaryById(long id);
		long CreateDictionary(string name, RegisterAttributeType factorType);
		//void UpdateDictionary(long id, string newName, ModelDictionaryType newValueType);
		void UpdateDictionaryFromExcel(Stream fileStream, DictionaryImportFileInfoDto fileImportInfo,
			long dictionaryId, bool isDeleteExistedMarks, OMImportDataLog import);
		int DeleteDictionary(long? id);
		decimal GetCoefficientFromStringFactor(string stringValue, OMModelingDictionary dictionary);
		decimal GetCoefficientFromDateFactor(DateTime? date, OMModelingDictionary dictionary);
		decimal GetCoefficientFromNumberFactor(decimal? number, OMModelingDictionary dictionary);

		List<OMModelingDictionariesValues> GetMarks(long dictionaryId);
		List<OMModelingDictionariesValues> GetMarks(List<long?> dictionaryIds);
		OMModelingDictionariesValues GetMark(long id);
		long CreateMark(DictionaryMarkDto dto);
		void UpdateMark(DictionaryMarkDto dto);
		void DeleteMark(long markId);
		int DeleteMarks(long? dictionaryId);
		OMImportDataLog CreateDataFileImport(Stream fileStream, string inputFileName);
	}
}
