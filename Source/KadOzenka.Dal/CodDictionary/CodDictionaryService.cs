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
        public IRegisterAttributeService RegisterAttributeService { get; }
        private IRegisterConfiguratorWrapper RegisterConfiguratorWrapper { get; }

        public CodDictionaryService(IRegisterService registerService,
            IRegisterAttributeService registerAttributeService,
            IRegisterConfiguratorWrapper registerConfiguratorWrapper)
        {
            RegisterService = registerService;
            RegisterAttributeService = registerAttributeService;
            RegisterConfiguratorWrapper = registerConfiguratorWrapper;
        }


        public OMCodJob GetDictionary(long id)
        {
            var dictionary = OMCodJob.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (dictionary == null)
                throw new Exception($"Не найден словарь с ИД {id}");

            return dictionary;
        }


        public long AddCodDictionary(CodDictionaryDto codDictionary)
        {
            ValidateCodDictionaryInternal(codDictionary, true);

            long codDictionaryId;
            using (var ts = new TransactionScope())
            {
                var omRegister = CreateRegister(codDictionary.Name);

                CreateColumns(codDictionary.Values, omRegister.RegisterId);

                codDictionaryId = new OMCodJob
                {
                    NameJob = codDictionary.Name,
                    ResultJob = codDictionary.Result,
                    RegisterId = omRegister.RegisterId
                }.Save();

                ts.Complete();
            }

            RegisterCache.UpdateCache(0, null);

            return codDictionaryId;
        }

        public void UpdateCodDictionary(CodDictionaryDto codDictionary)
        {
            ValidateCodDictionaryInternal(codDictionary, false);

             var dictionary = GetDictionary(codDictionary.Id);

            dictionary.NameJob = codDictionary.Name;
            dictionary.ResultJob = codDictionary.Result;
            dictionary.Save();
        }

        public static IEnumerable<ValidationResult> ValidateCodDictionaryForUpdating(CodDictionaryDto codDictionary)
        {
            if (string.IsNullOrWhiteSpace(codDictionary.Name))
            {
                yield return new ValidationResult("Не указано Имя справочника");
            }

            if (string.IsNullOrWhiteSpace(codDictionary.Result))
            {
                yield return new ValidationResult("Не указан Результат");
            }
        }

        public static IEnumerable<ValidationResult> ValidateCodDictionaryForAddition(CodDictionaryDto codDictionary)
        {
            return ValidateCodDictionaryForUpdating(codDictionary).Concat(ValidateCodDictionaryValues(codDictionary));
        }


        #region Support Methods

        private static IEnumerable<ValidationResult> ValidateCodDictionaryValues(CodDictionaryDto codDictionary)
        {
            if (codDictionary.Values == null)
            {
                yield return new ValidationResult("Не указаны Значения");
            }

            var valuesCount = codDictionary.Values?.Count ?? 0;
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
                if (string.IsNullOrWhiteSpace(codDictionary.Values?.ElementAtOrDefault(i)))
                {
                    yield return new ValidationResult($"Значение {i + 1} не может быть пустым");
                }
            }
        }

        private void ValidateCodDictionaryInternal(CodDictionaryDto codDictionary, bool isNewDictionary)
        {
            var errors = isNewDictionary
                ? ValidateCodDictionaryForAddition(codDictionary).ToList()
                : ValidateCodDictionaryForUpdating(codDictionary).ToList();

            if (errors.Count == 0) 
                return;

            var message = string.Join(';', errors.Select(x => x.ErrorMessage));
            throw new Exception(message);
        }

        private OMRegister CreateRegister(string name)
        {
            var existedCodDictionariesCount = OMCodJob.Where(x => true).ExecuteCount() + 1;
            var registerName = $"Gbu.CodDictionary{existedCodDictionariesCount}";
            var tableName = $"gbu_cod_dictionary_{existedCodDictionariesCount}";

            var omRegister = RegisterService.CreateRegister(registerName, name, tableName, null, (long)StorageType.Type4);

            RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);
            RegisterConfiguratorWrapper.CreateDbTableForRegister(omRegister.RegisterId);

            return omRegister;
        }

        private void CreateColumns(List<string> columns, long registerId)
        {
            columns.ForEach(x =>
            {
                var omAttribute = RegisterAttributeService.CreateRegisterAttribute(x, registerId, RegisterAttributeType.STRING, true);

                var dbConfigurator = RegisterConfiguratorWrapper.GetDbConfigurator();
                RegisterConfiguratorWrapper.CreateDbColumnForRegister(omAttribute, dbConfigurator);
            });
        }

        #endregion
    }
}
