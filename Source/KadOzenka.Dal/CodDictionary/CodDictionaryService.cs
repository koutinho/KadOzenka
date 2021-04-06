using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using Core.Register;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.Registers;
using ObjectModel.Core.Register;
using ObjectModel.KO;

namespace KadOzenka.Dal.CodDictionary
{
    public class CodDictionaryService : ICodDictionaryService
    {
        private IRegisterService RegisterService { get; }
        private IRegisterConfiguratorWrapper RegisterConfiguratorWrapper { get; }
        private static readonly StorageType StorageType = StorageType.Type4;
        private static readonly string _quantTable = "GBU_MAIN_OBJECT";

        public CodDictionaryService(IRegisterService registerService, IRegisterConfiguratorWrapper registerConfiguratorWrapper)
        {
            RegisterService = registerService;
            RegisterConfiguratorWrapper = registerConfiguratorWrapper;
        }


        public long AddCodDictionary(CodDictionaryDto codDictionary)
        {
            ValidateCodDictionaryInternal(codDictionary);

            OMRegister omRegister;
            using (var ts = new TransactionScope())
            {
                var existedCodDictionariesCount = GetNumberOfExistedCodDictionaries() + 1;
                var registerName = $"Gbu.CodDictionary{existedCodDictionariesCount}";
                var tableName = $"gbu_cod_dictionary_{existedCodDictionariesCount}";
                omRegister = RegisterService.CreateRegister(registerName, codDictionary.Name, tableName, null, (long)StorageType);

                RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);

                RegisterConfiguratorWrapper.CreateDbTableForRegister(omRegister.RegisterId);

                //TODO добавить register_id и цикл по колонкам
                var dictionaryId = new OMCodJob
                {
                    NameJob = codDictionary.Name,
                    ResultJob = codDictionary.Result
                }.Save();

                ts.Complete();
            }

            return omRegister.RegisterId;
        }

        public static IEnumerable<ValidationResult> ValidateCodDictionary(CodDictionaryDto codDictionary)
        {
            if (string.IsNullOrWhiteSpace(codDictionary.Name))
            {
                yield return new ValidationResult("Не указано Имя справочника");
            }

            if (string.IsNullOrWhiteSpace(codDictionary.Result))
            {
                yield return new ValidationResult("Не указан Результат");
            }

            var valuesCount = codDictionary.Values.Count;
            if (valuesCount > CodDictionaryConsts.MaxValuesCount)
            {
                yield return new ValidationResult($"Максимальное количество значений - {CodDictionaryConsts.MaxValuesCount}");
            }

            if (valuesCount < CodDictionaryConsts.MinValuesCount)
            {
                yield return new ValidationResult($"Минимальное количество значений - {CodDictionaryConsts.MinValuesCount}");
            }

            for (var i = 0; i < valuesCount; i++)
            {
                if (string.IsNullOrWhiteSpace(codDictionary.Values.ElementAtOrDefault(i)))
                {
                    yield return new ValidationResult($"Значение {i + 1} не может быть пустым");
                }
            }
        }

        public int GetNumberOfExistedCodDictionaries()
        {
            return OMCodJob.Where(x => true).ExecuteCount();
        }

        #region Support Methods

        private void ValidateCodDictionaryInternal(CodDictionaryDto codDictionary)
        {
            var errors = ValidateCodDictionary(codDictionary).ToList();
            if (errors.Count == 0) 
                return;

            var message = string.Join(';', errors.Select(x => x.ErrorMessage));
            throw new Exception(message);
        }

        #endregion
    }
}
