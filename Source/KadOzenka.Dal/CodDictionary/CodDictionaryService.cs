using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Registers;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.KO;
using Platform.Configurator.DbConfigurator;

namespace KadOzenka.Dal.CodDictionary
{
    public class CodDictionaryService : ICodDictionaryService
    {
        private IRegisterService RegisterService { get; }
        public IRegisterAttributeService RegisterAttributeService { get; }
        private IRegisterConfiguratorWrapper RegisterConfiguratorWrapper { get; }
        public IRecycleBinService RecycleBinService { get; }


        public CodDictionaryService(IRegisterService registerService,
            IRegisterAttributeService registerAttributeService,
            IRegisterConfiguratorWrapper registerConfiguratorWrapper,
            IRecycleBinService recycleBinService)
        {
            RegisterService = registerService;
            RegisterAttributeService = registerAttributeService;
            RegisterConfiguratorWrapper = registerConfiguratorWrapper;
            RecycleBinService = recycleBinService;
        }


        #region Словарь

        public OMCodJob GetDictionary(long id)
        {
            var dictionary = OMCodJob.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
            if (dictionary == null)
                throw new Exception($"Не найден словарь с ИД {id}");

            return dictionary;
        }

        public static IEnumerable<ValidationResult> ValidateCodDictionary(CodDictionaryDto codDictionary)
        {
            if (string.IsNullOrWhiteSpace(codDictionary.Name))
            {
                yield return new ValidationResult("Не указано Имя справочника");
            }

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
                if (string.IsNullOrWhiteSpace(codDictionary.Values?.ElementAtOrDefault(i)?.Name))
                {
                    yield return new ValidationResult($"Значение {i + 1} не может быть пустым");
                }
            }

            if (codDictionary.Values != null && codDictionary.Values.Any(x => x.Name == CodDictionaryConsts.CodeColumnName))
            {
                yield return new ValidationResult($"Нельзя создать значение с зарезервированным именем '{CodDictionaryConsts.CodeColumnName}'");
            }
        }

        public long AddCodDictionary(CodDictionaryDto codDictionary)
        {
            ValidateCodDictionaryInternal(codDictionary);

            long codDictionaryId;
            using (var ts = new TransactionScope())
            {
                var omRegister = CreateRegister(codDictionary.Name);

                CreateColumns(codDictionary.Values.Select(x => x.Name).ToList(), omRegister.RegisterId);

                codDictionaryId = new OMCodJob
                {
                    NameJob = codDictionary.Name,
                    RegisterId = omRegister.RegisterId
                }.Save();

                RecycleBinService.CreateDeletedTable(omRegister.RegisterId, omRegister.QuantTable);

                ts.Complete();
            }

            RegisterCache.UpdateCache(0, null);

            return codDictionaryId;
        }

        public void UpdateCodDictionary(CodDictionaryDto codDictionary)
        {
            ValidateCodDictionaryInternal(codDictionary);

            var dictionary = GetDictionary(codDictionary.Id);

            var isCacheUpdatingNeeded = false;

            UpdateRegister(codDictionary, dictionary, ref isCacheUpdatingNeeded);

            UpdateAttributes(codDictionary, ref isCacheUpdatingNeeded);

            if (isCacheUpdatingNeeded)
            {
                RegisterCache.UpdateCache(0, null);
            }

            dictionary.NameJob = codDictionary.Name;
            dictionary.Save();
        }

        public void DeleteDictionary(long id)
        {
            var dictionary = GetDictionary(id);

            var eventId = new OMRecycleBin
            {
                DeletedTime = DateTime.Now,
                UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
                ObjectRegisterId = OMCodJob.GetRegisterId(),
                Description = $"Справочник ЦОД '{dictionary.NameJob}'"
            }.Save();

            RegisterService.RemoveRegister(dictionary.RegisterId, eventId);

            RecycleBinService.MoveObjectToRecycleBin(dictionary.Id, OMCodJob.GetRegisterId(), eventId);
        }


        #region Support Methods
        
        private void ValidateCodDictionaryInternal(CodDictionaryDto codDictionary)
        {
            var errors = ValidateCodDictionary(codDictionary).ToList();

            ConvertErrors(errors);
        }

        private static void ConvertErrors(List<ValidationResult> errors)
        {
            if (errors.Count == 0)
                return;

            var message = string.Join(';', errors.Select(x => x.ErrorMessage));
            throw new Exception(message);
        }

        private OMRegister CreateRegister(string description)
        {
            var tableNameTemplate = "gbu_cod_dictionary_";
            var existedCodDictionariesCount = RegisterCache.Registers.Count(x =>
                x.Value.QuantTable.StartsWith(tableNameTemplate, StringComparison.InvariantCultureIgnoreCase)) + 1;

            var registerName = $"Gbu.CodDictionary{existedCodDictionariesCount}";
            var tableName = $"{tableNameTemplate}{existedCodDictionariesCount}";

            var omRegister = RegisterService.CreateRegister(registerName, description, tableName, null, (long)StorageType.Type4);

            RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);
            RegisterConfiguratorWrapper.CreateDbTableForRegister(omRegister.RegisterId);

            return omRegister;
        }

        private void CreateColumns(List<string> columns, long registerId)
        {
            var dbConfigurator = RegisterConfiguratorWrapper.GetDbConfigurator();

            columns.ForEach(x =>
            {
                CreateAttribute(x, registerId, dbConfigurator);
            });

            CreateAttribute(CodDictionaryConsts.CodeColumnName, registerId, dbConfigurator);
        }

        private void CreateAttribute(string name, long registerId, DbConfiguratorBase dbConfigurator)
        {
            var codeAttribute = RegisterAttributeService.CreateRegisterAttribute(name, registerId, RegisterAttributeType.STRING, true);
            RegisterConfiguratorWrapper.CreateDbColumnForRegister(codeAttribute, dbConfigurator);
        }

        private void UpdateRegister(CodDictionaryDto codDictionary, OMCodJob dictionaryFromDb,
            ref bool isCacheUpdatingNeeded)
        {
            if (dictionaryFromDb.NameJob == codDictionary.Name)
                return;

            isCacheUpdatingNeeded = true;
            var register = RegisterService.GetRegister(dictionaryFromDb.RegisterId);
            if (register == null)
                throw new Exception($"Не найден реестр с ИД '{dictionaryFromDb.RegisterId}'");

            register.RegisterDescription = codDictionary.Name;
            register.Save();
        }

        private void UpdateAttributes(CodDictionaryDto codDictionary, ref bool isCacheUpdatingNeeded)
        {
            var attributeIds = codDictionary.Values.Select(x => x.Id).ToList();
            var attributes = OMAttribute.Where(x => attributeIds.Contains(x.Id)).Select(x => x.Name).Execute();
            for (var i = 0; i < codDictionary.Values.Count; i++)
            {
                var value = codDictionary.Values[i];

                var attribute = attributes.FirstOrDefault(x => x.Id == value.Id);
                if (attribute == null || attribute.Name == value.Name)
                    return;

                isCacheUpdatingNeeded = true;
                attribute.Name = value.Name;
                attribute.Save();
            }
        }

        #endregion

        #endregion


        #region Значения словаря

        public void EditDictionaryValue(long dictionaryId, CodDictionaryValue value)
        {
            ValidateCodDictionaryValueInternal(value);

            var dictionary = GetDictionary(dictionaryId);
            var attributes = GetDictionaryRegisterAttributes(dictionary.RegisterId);
            var codeAttributeInfo = attributes.First(x => x.Name == CodDictionaryConsts.CodeColumnName);
            attributes.Remove(codeAttributeInfo);

            var registerObject = new RegisterObject((int) dictionary.RegisterId, (int) value.Id);
            attributes.ForEach(attribute =>
            {
                var currentValue = value.Values.FirstOrDefault(x => x.AttributeId == attribute.Id)?.Value;
                registerObject.SetAttributeValue((int)attribute.Id, currentValue);
            });

            registerObject.SetAttributeValue((int)codeAttributeInfo.Id, value.Code);

            RegisterStorage.Save(registerObject);
        }

        public CodDictionaryValue GetDictionaryValue(OMCodJob dictionary, long dictionaryValueId)
        {
            var value = GetDictionaryValue(dictionary.RegisterId, dictionaryValueId);
            if (value == null)
                throw new Exception($"Не найдено значение словаря '{dictionary.NameJob}' с ИД {dictionaryValueId}");

            return value;
        }

        public CodDictionaryValue GetDictionaryValue(long registerId, long dictionaryValueId)
        {
            var attributes = GetDictionaryRegisterAttributes(registerId);
            var primaryKeyAttributeId =
                RegisterCache.RegisterAttributes.First(x => x.Value.RegisterId == registerId && x.Value.IsPrimaryKey).Key;
            var registerPrimaryKeyColumn = new QSColumnSimple((int)primaryKeyAttributeId);

            var query = new QSQuery
            {
                MainRegisterID = (int)registerId,
                Condition = new QSConditionSimple
                {
                    ConditionType = QSConditionType.In,
                    LeftOperand = registerPrimaryKeyColumn,
                    RightOperand = new QSColumnConstant(dictionaryValueId)
                }
            };

            return GetDictionaryValuesInternal(attributes, query).FirstOrDefault();
        }

        public List<CodDictionaryValue> GetDictionaryValues(long registerId)
        {
            var attributes = GetDictionaryRegisterAttributes(registerId);
            
            var query = new QSQuery
            {
                MainRegisterID = (int)registerId
            };

            return GetDictionaryValuesInternal(attributes, query);
        }

        public static List<RegisterAttribute> GetDictionaryRegisterAttributes(long registerId)
        {
            return RegisterCache.RegisterAttributes
                .Where(x => x.Value.RegisterId == registerId && !x.Value.IsPrimaryKey).Select(x => x.Value).ToList();
        }

        public static IEnumerable<ValidationResult> ValidateDictionaryValue(CodDictionaryValue dictionaryValue)
        {
            if (string.IsNullOrWhiteSpace(dictionaryValue.Code))
            {
                yield return new ValidationResult("Не указан Код");
            }
            if (dictionaryValue.Values.All(x => string.IsNullOrWhiteSpace(x.Value)))
            {
                yield return new ValidationResult("Дожно быть заполнено хотя бы одно значение");
            }
        }

        private List<CodDictionaryValue> GetDictionaryValuesInternal(List<RegisterAttribute> attributes, QSQuery query)
        {
            var codeAttribute = attributes.First(x => x.Name == CodDictionaryConsts.CodeColumnName);

            attributes.ForEach(attribute =>
            {
                var attributeId = attribute.Id;
                query.AddColumn(attributeId, attributeId.ToString());
            });

            //var sql = query.GetSql();

            var rows = new List<CodDictionaryValue>();
            var table = query.ExecuteQuery();
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var id = row["id"].ParseToLong();
                var code = string.Empty;
                var rowValues = new List<CodDictionaryValuePure>();
                attributes.ForEach(attribute =>
                {
                    var attributeId = attribute.Id;
                    var value = row[attributeId.ToString()].ParseToStringNullable();

                    if (attribute.Id == codeAttribute.Id)
                    {
                        code = value;
                    }
                    else
                    {
                        rowValues.Add(new CodDictionaryValuePure(attributeId, value));
                    }
                });

                rows.Add(new CodDictionaryValue(id, code, rowValues));
            }

            return rows;
        }

        #endregion


        #region Support Methods

        private void ValidateCodDictionaryValueInternal(CodDictionaryValue value)
        {
            var errors = ValidateDictionaryValue(value).ToList();

            ConvertErrors(errors);
        }

        #endregion
    }
}
