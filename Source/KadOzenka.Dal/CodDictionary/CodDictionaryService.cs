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
using KadOzenka.Dal.CodDictionary.Resources;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Registers;
using ObjectModel.Common;
using ObjectModel.Core.Register;
using ObjectModel.Gbu.GroupingAlgoritm;
using ObjectModel.KO;

namespace KadOzenka.Dal.CodDictionary
{
    public class CodDictionaryService : ICodDictionaryService
    {
        private IRegisterService RegisterService { get; }
        public IRegisterAttributeService RegisterAttributeService { get; }
        public IRegisterAttributeRepository RegisterAttributeRepository { get; }
        private IRegisterConfiguratorWrapper RegisterConfiguratorWrapper { get; }
        public IRecycleBinService RecycleBinService { get; }
        public IRegisterObjectWrapper RegisterObjectWrapper { get; }
        public IRegisterCacheWrapper RegisterCacheWrapper { get; }
        public ICodDictionaryRepository CodDictionaryRepository { get; }


        public CodDictionaryService(IRegisterService registerService,
            IRegisterAttributeService registerAttributeService,
            IRegisterConfiguratorWrapper registerConfiguratorWrapper,
            IRecycleBinService recycleBinService,
            IRegisterObjectWrapper registerObjectWrapper,
            IRegisterCacheWrapper registerCacheWrapper,
            ICodDictionaryRepository codDictionaryRepository,
            IRegisterAttributeRepository registerAttributeRepository)
        {
            RegisterService = registerService;
            RegisterAttributeService = registerAttributeService;
            RegisterConfiguratorWrapper = registerConfiguratorWrapper;
            RecycleBinService = recycleBinService;
            RegisterObjectWrapper = registerObjectWrapper;
            RegisterCacheWrapper = registerCacheWrapper;
            CodDictionaryRepository = codDictionaryRepository;
            RegisterAttributeRepository = registerAttributeRepository;
        }

        public CodDictionaryService()
        {
            RegisterService = new RegisterService();
            RegisterAttributeService = new RegisterAttributeService();
            RegisterConfiguratorWrapper = new RegisterConfiguratorWrapper();
            RecycleBinService = new RecycleBinService();
            RegisterObjectWrapper = new RegisterObjectWrapper();
            RegisterCacheWrapper = new RegisterCacheWrapper();
            CodDictionaryRepository = new CodDictionaryRepository();
            RegisterAttributeRepository = new RegisterAttributeRepository();
        }


        #region Словарь

        public OMCodJob GetDictionary(long id)
        {
            var dictionary = CodDictionaryRepository.GetById(id, null);
            if (dictionary == null)
                throw new Exception($"Не найден словарь с ИД {id}");

            return dictionary;
        }

        public static IEnumerable<ValidationResult> ValidateCodDictionary(CodDictionaryDto codDictionary)
        {
            if (string.IsNullOrWhiteSpace(codDictionary.Name))
            {
                yield return new ValidationResult(CodMessages.EmptyDictionaryName);
            }

            if (codDictionary.Values == null)
            {
                yield return new ValidationResult(CodMessages.NoDictionaryValues);
                yield break;
            }

            var valuesCount = codDictionary.Values?.Count ?? 0;
            if (valuesCount > CodDictionaryConsts.MaxValuesCount)
            {
                yield return new ValidationResult(
                    $"{CodMessages.MaxValuesCountExceed} {CodDictionaryConsts.MaxValuesCount}");
            }

            if (valuesCount < CodDictionaryConsts.MinValuesCount)
            {
                yield return new ValidationResult(
                    $"{CodMessages.MinValuesCountExceed} {CodDictionaryConsts.MinValuesCount}");
            }

            for (var i = 0; i < valuesCount; i++)
            {
                if (string.IsNullOrWhiteSpace(codDictionary.Values?.ElementAtOrDefault(i)?.Name))
                {
                    yield return new ValidationResult(string.Format(CodMessages.EmptyValueName, i + 1));
                }
            }

            if (codDictionary.Values != null &&
                codDictionary.Values.Any(x => x.Name == CodDictionaryConsts.CodeColumnName))
            {
                yield return new ValidationResult(
                    $"{CodMessages.ForbiddenValueName} '{CodDictionaryConsts.CodeColumnName}'");
            }
        }

        public long AddCodDictionary(CodDictionaryDto dictionaryDto)
        {
            ValidateCodDictionaryInternal(dictionaryDto);

            long codDictionaryId;
            using (var ts = new TransactionScope())
            {
                var omRegister = CreateRegister(dictionaryDto.Name);

                CreateColumns(dictionaryDto.Values.Select(x => x.Name).ToList(), omRegister.RegisterId);

                var dictionary = new OMCodJob
                {
                    NameJob = dictionaryDto.Name,
                    RegisterId = omRegister.RegisterId
                };
                codDictionaryId = CodDictionaryRepository.Save(dictionary);

                RecycleBinService.CreateDeletedTable(omRegister.RegisterId, omRegister.QuantTable);

                ts.Complete();
            }

            RegisterCacheWrapper.UpdateCache();

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
                RegisterCacheWrapper.UpdateCache();
            }

            if (dictionary.NameJob != codDictionary.Name)
            {
                dictionary.NameJob = codDictionary.Name;
                CodDictionaryRepository.Save(dictionary);
            }
        }

        public void DeleteDictionary(long id)
        {
            var dictionary = GetDictionary(id);

            var recycleBin = new OMRecycleBin
            {
                DeletedTime = DateTime.Now,
                UserId = SRDSession.GetCurrentUserId().GetValueOrDefault(),
                ObjectRegisterId = OMCodJob.GetRegisterId(),
                Description = $"Справочник ЦОД '{dictionary.NameJob}'"
            };
            var eventId = RecycleBinService.Save(recycleBin);

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
            var existedCodDictionariesCount = RegisterCacheWrapper.GetRegistersCache().Count(x =>
                x.Value.QuantTable.StartsWith(tableNameTemplate, StringComparison.InvariantCultureIgnoreCase)) + 1;

            var registerName = $"Gbu.CodDictionary{existedCodDictionariesCount}";
            var tableName = $"{tableNameTemplate}{existedCodDictionariesCount}";

            var omRegister = RegisterService.CreateRegister(registerName, description, tableName);

            RegisterService.CreateIdColumnForRegister(omRegister.RegisterId);
            RegisterConfiguratorWrapper.CreateDbTableForRegister(omRegister.RegisterId);

            return omRegister;
        }

        private void CreateColumns(List<string> columns, long registerId)
        {
            var dbConfigurator = RegisterConfiguratorWrapper.GetDbConfigurator();
            dbConfigurator.DefaultColumnSizeForStringColumn = CodDictionaryConsts.DefaultColumnSizeForStringColumn;

            columns.ForEach(x =>
            {
                var attribute =
                    RegisterAttributeService.CreateRegisterAttribute(x, registerId, RegisterAttributeType.STRING, true);
                RegisterConfiguratorWrapper.CreateDbColumnForRegister(attribute, dbConfigurator);
            });

            var codeAttribute = RegisterAttributeService.CreateRegisterAttribute(CodDictionaryConsts.CodeColumnName,
                registerId, RegisterAttributeType.STRING, true, isNullable: false);
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
            RegisterService.SaveRegister(register);
        }

        private void UpdateAttributes(CodDictionaryDto codDictionary, ref bool isCacheUpdatingNeeded)
        {
            var attributeIds = codDictionary.Values.Select(x => x.Id).ToList();
            var attributes =
                RegisterAttributeRepository.GetEntitiesByCondition(x => attributeIds.Contains(x.Id), x => new {x.Name});
            for (var i = 0; i < codDictionary.Values.Count; i++)
            {
                var value = codDictionary.Values[i];

                var attribute = attributes.FirstOrDefault(x => x.Id == value.Id);
                if (attribute == null || attribute.Name == value.Name)
                    continue;

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
                RegisterObjectWrapper.SetAttributeValue(registerObject, (int) attribute.Id, currentValue);
            });

            RegisterObjectWrapper.SetAttributeValue(registerObject, (int) codeAttributeInfo.Id, value.Code);

            RegisterObjectWrapper.Save(registerObject);
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
                RegisterCache.RegisterAttributes.First(x => x.Value.RegisterId == registerId && x.Value.IsPrimaryKey)
                    .Key;
            var registerPrimaryKeyColumn = new QSColumnSimple((int) primaryKeyAttributeId);

            var query = new QSQuery
            {
                MainRegisterID = (int) registerId,
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
                MainRegisterID = (int) registerId
            };

            return GetDictionaryValuesInternal(attributes, query);
        }

        public List<CodDictionaryValue> GetDictionaryValuesByDictId(long codJobId)
        {
            var dict = CodDictionaryRepository.GetById(codJobId, null);
            return GetDictionaryValues(dict.RegisterId);
        }

        public List<RegisterAttribute> GetDictionaryRegisterAttributes(long registerId, bool withCodeAttribute = true)
        {
            return RegisterCacheWrapper.GetRegisterAttributesCache()
                .Where(x =>
                {
                    var codeCondition = true;
                    if (!withCodeAttribute)
                    {
                        codeCondition = x.Value.Name != CodDictionaryConsts.CodeColumnName;
                    }

                    return x.Value.RegisterId == registerId && !x.Value.IsPrimaryKey && codeCondition;
                }).Select(x => x.Value).ToList();
        }

        public static IEnumerable<ValidationResult> ValidateDictionaryValue(CodDictionaryValue dictionaryValue)
        {
            if (string.IsNullOrWhiteSpace(dictionaryValue.Code))
            {
                yield return new ValidationResult(CodMessages.NoValueCode);
            }

            if (dictionaryValue.Values.All(x => string.IsNullOrWhiteSpace(x.Value)))
            {
                yield return new ValidationResult(CodMessages.AtLeastOneDictionaryValueNeeded);
            }
        }

        #endregion

        public List<CodJobInfo> GetAllCodJobInfo()
        {
            var codJobs = OMCodJob.Where(x => x).SelectAll().Execute();
            var codRegisters = codJobs.Select(x => x.RegisterId).ToList();
            var regIdColumn = OMAttribute.GetColumn(x => x.RegisterId);
            var regColId = OMAttribute.GetColumnAttributeId(x => x.RegisterId);
            var codJobAttributes = new QSQuery
            {
                MainRegisterID = OMAttribute.GetRegisterId(),
                Columns = new List<QSColumn>
                {
                    regIdColumn,
                    new QSColumnFunction(QSColumnFunctionType.Count, regIdColumn, "count")
                },
                GroupBy = new List<QSGroup>
                {
                    new QSGroup(regIdColumn)
                },
                Condition = new QSConditionSimple(regColId, QSColumnSimpleType.Value, QSConditionType.In, codRegisters)
            }.ExecuteQuery<CodJobParamCount>();
            OMAttribute.Where(x => codRegisters.Contains(x.RegisterId) && !x.IsDeleted)
                .GroupBy(x => x.RegisterId).Select(x => x.RegisterId).Execute();

            var res = codJobs.Join(codJobAttributes, x => x.RegisterId, y => y.CodJobRegId,
                (x, y) => new CodJobInfo
                    {Value = x.Id, Text = x.NameJob, RegisterId = x.RegisterId, ParamCount = y.ParamCount}).ToList();
            return res;
        }

        public List<LevelItem> GetSelectCodJobInfo(long codJobId)
        {
            var codJob = CodDictionaryRepository.GetById(codJobId, null);
            var codJobParams =
                OMAttribute.Where(x => x.RegisterId == codJob.RegisterId && x.ValueField != "ID" && x.Name != "Код")
                    .SelectAll().Execute().Select(x=> new LevelItem{ CodValueId = x.Id, CodValueName = x.Name}).ToList();
            return codJobParams;
        }

        public class CodJobInfo
        {
            public long Value { get; set; }
            public string Text { get; set; }
            public long RegisterId { get; set; }
            public long ParamCount { get; set; }
        }

        public class CodJobParamCount
        {
            public long CodJobRegId { get; set; }
            public long ParamCount { get; set; }
        }

        #region Support Methods

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

        private void ValidateCodDictionaryValueInternal(CodDictionaryValue value)
        {
            var errors = ValidateDictionaryValue(value).ToList();

            ConvertErrors(errors);
        }

        #endregion
    }
}