//doc:BuildingService.xml

using CIPJS.DAL.Flat;
using CIPJS.DAL.ImportLogInsurFlat;
using CIPJS.DAL.InsuranceObjectLoader;
using Core.ConfigParam;
using Core.DBManagment;
using Core.RefLib;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Register.RegisterEntities;
using Core.Shared.Exceptions;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.SRD.DAL;
using DynamicExpresso;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using ObjectModel.Bti;
using ObjectModel.Core.Shared;
using ObjectModel.Directory;
using ObjectModel.Ehd;
using ObjectModel.ImportLog;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace CIPJS.DAL.Building
{
    /// <summary>Сервис работы с объектами типа ObjectModel.Insur.OMBuilding</summary>
    /// <exception>Исключений не вызывает</exception>
    public class BuildingService
    {
        private static readonly Interpreter Interpreter = new Interpreter();

        private static readonly ConfigCalculateFlagInsur ConfigFlagInsur = Configuration.GetParam<ConfigCalculateFlagInsur>("InsurObjectFlagInsur");

        private static readonly Regex regexExpressionParameter = new Regex(@"(MaxFlatsCount|BtiAddressesCount)");

        private RegisterAttribute[] btiAttributes;

        private readonly ImportLogInsurFlatService _importLogInsurFlatService;

        public BuildingService()
        {
            int regId = OMBtiBuilding.GetRegisterId();
            btiAttributes = RegisterCache
                .RegisterAttributes
                .Values
                .Where(x => x.RegisterId == regId)
                .ToArray();

            _importLogInsurFlatService = new ImportLogInsurFlatService();
        }

        /// <summary>Получить МКД по его идентификатору</summary>
        /// <param name="id">Идентификатор расчета</param>
        /// <returns>Расчет</returns>
        public OMBuilding GetById(long? id)
        {
            var entity = OMBuilding.Where(x => x.EmpId == id)
                .SelectAll()
                .Select(x => x.ParentAddress.FullAddress)
                .Execute().FirstOrDefault();

            return entity;
        }

        public OMBuilding GetBuildingNote(long? id)
        {
            var entity = OMBuilding
                .Where(x => x.EmpId == id)
                .Select(x => x.Note)
                .Execute()
                .FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// Получаем данные changes_user_id и changes_date по дому.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (string changeUser, string changeDate) GetBuildChangesData(long? id)
        {
            string query =
 $@"select changes_user_id, changes_date from insur_building_q where emp_id = {id} and actual = 1";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(query);
            var response = DBMngr.Realty.ExecuteDataSet(command).Tables[0].Rows;
            if (response.Count > 0)
            {
                var row = response[0];
                return (changeUser: row["changes_user_id"].ToString(), changeDate: row["changes_date"].ToString());
            }

            return (string.Empty, string.Empty);
        }

        /// <summary>
        /// Получаем данные по последнему изменению Примечания МКД.
        /// </summary>
        /// <param name="id">insur_building_q.emp_id</param>
        /// <returns></returns>
        public (string changeUser, string changeDate) GetBuildNoteChangesData(long? id)
        {

            string query =
            $@"with cte_last_value as (
                select pb.po_ from insur_building_q b
                inner join insur_building_q pb on pb.emp_id = b.emp_id and (pb.note != b.note or (pb.note is null and b.note is not null) or (b.note is null and pb.note is not null))
                and pb.changes_date is not null and pb.changes_user_id is not null
                where b.actual = 1 and b.emp_id = {id}
                order by pb.changes_date desc
                limit 1
                )
                select b.changes_user_id, b.changes_date from insur_building_q b
                inner join cte_last_value cte on b.s_::date = cte.po_::date and b.s_ > cte.po_
                order by b.changes_date
                limit 1
                ";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(query);
            var response = DBMngr.Realty.ExecuteDataSet(command).Tables[0].Rows;
            if (response.Count > 0)
            {
                var row = response[0];
                return (changeUser: row["changes_user_id"].ToString(), changeDate: row["changes_date"].ToString());
            }

            return (string.Empty, string.Empty);
        }

        public long? GetBuildingIdByUnom(long unom)
        {
            var building = OMBuilding
                .Where(x => x.Unom == unom)
                .ExecuteFirstOrDefault();
            return building?.EmpId;
        }

        public string GetShortAddress(long id)
        {
            var building = OMBuilding
                .Where(x => x.EmpId == id)
                .Select(x => x.ParentAddress.ShortAddress)
                .ExecuteFirstOrDefault();
            return building.ParentAddress?.ShortAddress;
        }

        public decimal? GetEpl(long? btiBuildingId)
        {
            if (btiBuildingId == null)
            {
                return null;
            }

            Core.Diagnostics.DiagnosticsManager.Trace("Test", "", "", $"building.LinkBtiFsks: {btiBuildingId}; SQL: {OMPremase.Where(x => x.ParentFloor.BuildingId == btiBuildingId).Select(x => x.Zpl).GetSql()}; ", SRDSession.GetCurrentUserId());


            return OMPremase.Where(x => x.ParentFloor.BuildingId == btiBuildingId && x.Bit0 != true).Select(x => x.Zpl).Execute().Sum(x => x.Zpl);
        }

        public OMBuilding GetByIdWithAddress(long? id, bool addFspData = true)
        {
            if (!id.HasValue) return null;

            var query = OMBuilding.Where(x => x.EmpId == id)
                .SelectAll()

                .Select(x => x.ParentBtiOkrug.Id)
                .Select(x => x.ParentBtiOkrug.Name)
                .Select(x => x.ParentBtiOkrug.ShortName)
                .Select(x => x.DistrictId)
                .Select(x => x.ParentBtiDistrict.Id)
                .Select(x => x.ParentBtiDistrict.Name)
                .Select(x => x.StatusSostBti)
                .Select(x => x.StatusSostBti_Code)
                .Select(x => x.PurposeName)
                .Select(x => x.PurposeName_Code)


                .Select(x => x.ParentAddress.EmpId)
                .Select(x => x.ParentAddress.PostalCode)
                .Select(x => x.ParentAddress.Region)
                .Select(x => x.ParentAddress.City)
                .Select(x => x.ParentAddress.TypeStreet)
                .Select(x => x.ParentAddress.Street)
                .Select(x => x.ParentAddress.House)
                .Select(x => x.ParentAddress.Corpus)
                .Select(x => x.ParentAddress.Structure)
                .Select(x => x.ParentAddress.FullAddress)
                .Select(x => x.ParentAddress.Locality)
                .Select(x => x.ParentAddress.TypeHouse);

            if (addFspData)
            {
                query.Select(x => x.AccruedSumCurrent);
                query.Select(x => x.AccruedOplCurrent);
                query.Select(x => x.AccruedSumLast);
                query.Select(x => x.AccruedOplLast);
                query.Select(x => x.CreditedSumCurrent);
                query.Select(x => x.CreditedOplCurrent);
                query.Select(x => x.CreditedSumLast);
                query.Select(x => x.CreditedOplLast);
            }

            return query.ExecuteFirstOrDefault();
        }

        public List<long> GetBuildingActualInputNachs(long? id)
        {
            List<long> inputNachs = new List<long>();
            if (id.HasValue)
            {
                string sql = $@"select nach.emp_id from insur_input_nach nach
                    inner join (
                    select fsp.emp_id from insur_fsp_q fsp
	                    inner join (
	                    select flat.emp_id from  insur_flat_q flat
		                    inner join insur_building_q build
		                    on flat.link_object_mkd = build.emp_id and flat.actual = 1
	                    where build.emp_id = {id} and build.actual = 1
	                    ) flats
                    on fsp.obj_id = flats.emp_id
                    ) fsps
                    on nach.period_reg_date = (select max(nach2.period_reg_date) from insur_input_nach nach2 where nach2.fsp_id = fsps.emp_id) 
                    and nach.fsp_id = fsps.emp_id";
                DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
                using (var reader = DBMngr.Realty.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        inputNachs.Add(reader.GetInt64(0));
                    }
                }
            }
            return inputNachs;
        }

        public static bool CalculateFlagInsur(long insurBuildingId, out string message)
        {
            OMBuilding insurBuilding = OMBuilding.Where(x => x.EmpId == insurBuildingId).Select(x => x.LinkBtiFsks).Select(x => x.LinkEgrnBild).ExecuteFirstOrDefault();

            if (insurBuilding == null)
            {
                throw new Exception("Не найден объект страхования МКД с ИД " + insurBuildingId);
            }

            if (insurBuilding.LinkBtiFsks == null)
            {
                message = "Отсутствует связь с БТИ у объекта страхования.";

                return false;
            }

            OMBtiBuilding btiBuilding = OMBtiBuilding.Where(x => x.EmpId == insurBuilding.LinkBtiFsks).SelectAll().ExecuteFirstOrDefault();

            return CalculateFlagInsur(insurBuilding.LinkEgrnBild, btiBuilding, out message);
        }

        public static bool CalculateFlagInsur(long? egrnBuildingId, OMBtiBuilding btiBuilding)
        {
            string message;
            return CalculateFlagInsur(egrnBuildingId, btiBuilding, out message);
        }

        /// <summary>
        /// Вычисление признака "Подлежит страхованию" на основе конфигурационного файла
        /// </summary>
        /// <param name="egrnBuildingId">ИД связанного здания ЕГРН</param>
        /// <param name="btiBuilding">Данные связаннного здания БТИ</param>
        /// <returns></returns>
        public static bool CalculateFlagInsur(long? egrnBuildingId, OMBtiBuilding btiBuilding, out string message)
        {
            message = "";

            if (btiBuilding == null)
            {
                message = $"Не найдено здание БТИ";
                return false;
            }

            foreach (var condition in ConfigFlagInsur.Conditions)
            {
                if (condition.Expression.IsNotEmpty())
                {
                    if (!EvalConditionExpression(condition, GetParameters(condition, egrnBuildingId, btiBuilding).ToArray()))
                    {
                        message = $"Не выполнена проверка \"{condition.Name}\"";

                        return false;
                    }
                }
                else if (condition.AllowedValuesByCode)
                {
                    long attrValue = GetAttributeValue(btiBuilding, condition.AttributeId.Value.ToString(), true).ParseToLong();

                    OMReferenceItem item = OMReferenceItem
                        .Where(x => x.ItemId == attrValue)
                        .Select(x => x.Code)
                        .Select(x => x.Value)
                        .ExecuteFirstOrDefault();

                    if (!IsAllowedValue(condition.AllowedValues, item?.Code))
                    {
                        message = $"Не выполнена проверка \"{condition.Name}\", получено значение {attrValue} ({item?.Code} - {item?.Value}), разрешенные значения {String.Join(", ", condition.AllowedValues)}";

                        return false;
                    }
                }
                else if (condition.AllowedValues.IsNotEmpty())
                {
                    object attrValue = GetAttributeValue(btiBuilding, condition.AttributeId.Value.ToString());
                    if (!IsAllowedValue(condition.AllowedValues, attrValue))
                    {
                        message = $"Не выполнена проверка \"{condition.Name}\", получено значение {attrValue}, разрешенные значения: {String.Join(", ", "\"" + condition.AllowedValues) + "\""}";

                        return false;
                    }
                }
                else
                {
                    throw new Exception("Invalid condition");
                }
            }

            return true;
        }

        private static IEnumerable<Parameter> GetParameters(FlagInsurCondition condition, long? egrnBuildingId, OMBtiBuilding btiBuilding)
        {
            object attrValue = null;
            Type attrType = null;
            List<string> parameters = regexExpressionParameter
                .Matches(condition.Expression)
                .Select(x => x.Value)
                .Distinct()
                .ToList();

            if (condition.AttributeId.HasValue)
                attrValue = GetAttributeValue(btiBuilding, condition.AttributeId.Value.ToString(), true, out attrType);
            if (attrType != null)
                yield return new Parameter("AttributeValue", attrType, attrValue);
            foreach (string parameter in parameters)
                yield return GetExpressionVariable(parameter, egrnBuildingId, btiBuilding.EmpId);
        }

        private static Parameter GetExpressionVariable(string varName, long? egrnBuildingId, long btiBuildingId)
        {
            string sql;
            DbCommand command;
            DataTable dataTable;
            switch (varName)
            {
                case "MaxFlatsCount":
                    int btiFlatsCount = OMPremase.Where(x => x.ParentFloor.BuildingId == btiBuildingId && x.ClassName == "Жилые помещения" && x.TypeName == "Квартира" && x.Bit0 == false).ExecuteCount();

                    int ehdFlatsCount = 0;
                    if (egrnBuildingId.HasValue)
                    {
                        sql = $@"select 
							  count(1) as cnt
						  from ehd_register_q ER
						  join ehd_build_parcel_q flat
							  on flat.emp_id = ER.building_parcel_id
							  and flat.type = 'Помещение'
						  where 
                              flat.actual_ehd = 0 AND
							  ER.assftp_cd = 'Жилое помещение' AND
							  ER.assftp1 = 'Квартира' AND
                              ER.state in ('Ранее учтенный', 'Учтенный') AND
							  ER.CADASTRAL_NUMBER_OKS IN (select b.object_id from ehd_build_parcel_q b where b.emp_id = {egrnBuildingId})";

                        command = DBMngr.Realty.GetSqlStringCommand(sql);
                        dataTable = DBMngr.Realty.ExecuteDataSet(command).Tables[0];
                        ehdFlatsCount = dataTable.Rows[0]["cnt"].ParseToInt();
                    }
                    int maxFlatsCount = Math.Max(btiFlatsCount, ehdFlatsCount);

                    return new Parameter(varName, maxFlatsCount.GetType(), maxFlatsCount);
                case "BtiAddressesCount":
                    sql = $"select count(1) as cnt from bti_addrlink_q t where t.actual = 1 and t.building_id = {btiBuildingId}";
                    command = DBMngr.Realty.GetSqlStringCommand(sql);
                    dataTable = DBMngr.Realty.ExecuteDataSet(command).Tables[0];
                    int btiAddressesCount = dataTable.Rows[0]["cnt"].ParseToInt();

                    return new Parameter(varName, btiAddressesCount.GetType(), btiAddressesCount);
                default:
                    throw new NotImplementedException();
            }
        }

        private static bool EvalConditionExpression(FlagInsurCondition condition, Parameter[] expressionParameters)
        {
            return Interpreter.Eval(condition.Expression, expressionParameters).ParseToBoolean();
        }

        private static bool IsAllowedReferenceValue(List<string> allowedValues, long attrValue)
        {
            OMReferenceItem item = OMReferenceItem
                .Where(x => x.ItemId == attrValue)
                .Select(x => x.Code)
                .ExecuteFirstOrDefault();
            return IsAllowedValue(allowedValues, item?.Code);
        }

        private static bool IsAllowedValue(List<string> allowedValues, object value)
        {
            return allowedValues.Select(x => x.ToLower()).Contains(value == null ? string.Empty : value.ToString().ToLower());
        }

        private bool CheckFlagInsurCondition(RegisterAttribute attribute, object value, long codeValue)
        {
            foreach (var condition in ConfigFlagInsur.Conditions)
            {
                if (condition.AttributeId != attribute.Id)
                {
                }
                else if (condition.Expression.IsNotEmpty())
                {
                    Type valueType = typeof(OMBtiBuilding).GetProperty(attribute.InternalName).PropertyType;
                    var valueParam = new Parameter("AttributeValue", valueType, value.ParseTo(valueType));
                    if (!EvalConditionExpression(condition, new[] { valueParam }))
                        return false;
                }
                else if (condition.AllowedValuesByCode)
                {
                    if (!IsAllowedReferenceValue(condition.AllowedValues, codeValue))
                        return false;
                }
                else if (condition.AllowedValues.IsNotEmpty())
                {
                    if (!IsAllowedValue(condition.AllowedValues, value))
                        return false;
                }
            }
            return true;
        }

        private RegisterAttribute GetBtiBuildingAttributeByValueField(string valueColumn)
        {
            return this.btiAttributes.FirstOrDefault(x => x.ValueField == valueColumn);
        }

        public static SourceInput GetSourceForAttribute(int sourceAttributeId)
        {
            var sourceAttributeData = RegisterCache.GetAttributeData(sourceAttributeId);
            var sourceRegisterData = RegisterCache.GetRegisterData(sourceAttributeData.RegisterId);

            SourceInput source;

            if (sourceRegisterData.Name.StartsWith("Bti."))
            {
                source = SourceInput.Bti;
            }
            else if (sourceRegisterData.Name.StartsWith("Ehd."))
            {
                source = SourceInput.Egrn;
            }
            else if (sourceRegisterData.Name.StartsWith("Insur."))
            {
                source = SourceInput.System;
            }
            else
            {
                throw new Exception($"Не распознанный источник: {sourceAttributeData.RegisterId} ({sourceRegisterData.Name})");
            }

            return source;
        }

        public bool CheckFlagInsurCondition(DataTable btiTable, string valueColumn)
        {
            if (btiTable == null || btiTable.Rows.Count == 0)
                return true;
            var row = btiTable.Rows[0];

            valueColumn = valueColumn.ToUpper();
            var attribute = GetBtiBuildingAttributeByValueField(valueColumn);
            if (attribute == null || row[valueColumn] == null)
                return true;

            long codeValue = attribute.CodeField.IsNullOrEmpty() ? 0 : row[attribute.CodeField].ParseToLong();
            return CheckFlagInsurCondition(attribute, row[valueColumn], codeValue);
        }

        public static string GetMapLogForAttribute(Dictionary<int, SourceInput> mapLog)
        {
            Dictionary<string, long> mapLogForAttribute = new Dictionary<string, long>();

            foreach (var keyVal in mapLog)
            {
                var attributeData = RegisterCache.GetAttributeData(keyVal.Key);

                mapLogForAttribute.Add(attributeData.ValueField, (int)keyVal.Value);
            }

            return JsonConvert.SerializeObject(mapLogForAttribute);
        }

        public static T Map<T>(string mapConfigName, int registerId, Dictionary<long, Object> objects, string existingObjectMap, out string mapLogAttribute)
        {
            Dictionary<int, SourceInput> mapLog = new Dictionary<int, SourceInput>();

            Dictionary<int, SourceInput> existingMap = new Dictionary<int, SourceInput>();

            if (existingObjectMap.IsNotEmpty())
            {
                Dictionary<string, long> existingMapLogForAttribute = JsonConvert.DeserializeObject<Dictionary<string, long>>(existingObjectMap);

                foreach (var keyVal in existingMapLogForAttribute)
                {
                    string valueField = keyVal.Key.ToUpper();
                    SourceInput source = (SourceInput)keyVal.Value;

                    RegisterAttribute attributeData = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == registerId && x.ValueField == valueField);

                    if (attributeData == null)
                    {
                        continue;
                    }

                    existingMap.Add(attributeData.Id, source);
                }
            }

            MapConfiguration mapConfiguration = Configuration.GetParam<MapConfiguration>(mapConfigName);
            T insurObject = (T)Activator.CreateInstance(typeof(T));

            foreach (EntityAttribute attribute in mapConfiguration.EntityAttributes)
            {
                RegisterAttribute destinationAttributeData = RegisterCache.GetAttributeData(Convert.ToInt32(attribute.OmEntityAttrId));
                PropertyInfo propertyOMEntity = typeof(T).GetProperty(destinationAttributeData.InternalName);

                if (propertyOMEntity == null)
                {
                    throw ExceptionInitializer.Create($"Ошибка маппинга поля {attribute.OmEntityAttrId}",
                        $"У типа {typeof(T)}, отсутствует поле {destinationAttributeData.InternalName}");
                }

                SourceInput existingSource;

                if (existingMap.ContainsKey(destinationAttributeData.Id))
                {
                    existingSource = existingMap[destinationAttributeData.Id];

                    // Атрибуты, исправленные вручную не изменяются
                    if (existingSource == SourceInput.ManualInput)
                    {
                        mapLog.Add(destinationAttributeData.Id, existingSource);
                        continue;
                    }

                    if (!attribute.EnableUpdateSign)
                    {
                        mapLog.Add(destinationAttributeData.Id, existingSource);
                        continue;
                    }
                }

                Object attributeValue = null;
                Object attributeValueCode = null;

                RegisterAttribute sourceAttributeData = null;

                switch (attribute.Type)
                {
                    case "A":
                        {
                            string attributeId = "";

                            if (attribute.Source1AttrId.Length > 0)
                            {
                                attributeId = attribute.Source1AttrId;

                                sourceAttributeData = RegisterCache.GetAttributeData(Convert.ToInt32(attributeId));
                                Type registerType = BigDataReaderCache.GetRegisterTypeByNumber(sourceAttributeData.RegisterId);

                                Object refObject;

                                if (objects.TryGetValue(sourceAttributeData.RegisterId, out refObject))
                                {
                                    attributeValue = GetAttributeValue(refObject, attributeId);

                                    if (sourceAttributeData.ReferenceId > 0)
                                    {
                                        attributeValueCode = GetAttributeValue(refObject, attributeId, true);
                                    }
                                }
                                else
                                {
                                    throw ExceptionInitializer.Create("Маппинг на поля данного реестра не поддерживается: " + sourceAttributeData.RegisterId,
                                        $"Description: {attribute.Description}; OmEntityAttrId: {attribute.OmEntityAttrId}; Source1AttrId: {attribute.Source1AttrId}; Source2AttrId: {attribute.Source2AttrId}");
                                }
                            }

                            if (attributeValue == null && attribute.Source2AttrId.IsNotEmpty() && attribute.UseSource2Sign)
                            {
                                attributeId = attribute.Source2AttrId;

                                sourceAttributeData = RegisterCache.GetAttributeData(Convert.ToInt32(attributeId));
                                Type registerType = BigDataReaderCache.GetRegisterTypeByNumber(sourceAttributeData.RegisterId);

                                Object refObject;

                                if (objects.TryGetValue(sourceAttributeData.RegisterId, out refObject))
                                {
                                    attributeValue = GetAttributeValue(refObject, attributeId);

                                    if (sourceAttributeData.ReferenceId > 0)
                                    {
                                        attributeValueCode = GetAttributeValue(refObject, attributeId, true);
                                    }
                                }
                                else
                                {
                                    throw new Exception("Маппинг на поля данного реестра не поддерживается: " + sourceAttributeData.RegisterId);
                                }
                            }

                            if (attributeValue == null && attribute.Source3AttrId.IsNotEmpty() && attribute.UseSource3Sign)
                            {
                                attributeId = attribute.Source3AttrId;

                                sourceAttributeData = RegisterCache.GetAttributeData(Convert.ToInt32(attributeId));
                                Type registerType = BigDataReaderCache.GetRegisterTypeByNumber(sourceAttributeData.RegisterId);

                                Object refObject;

                                if (objects.TryGetValue(sourceAttributeData.RegisterId, out refObject))
                                {
                                    attributeValue = GetAttributeValue(refObject, attributeId);

                                    if (sourceAttributeData.ReferenceId > 0)
                                    {
                                        attributeValueCode = GetAttributeValue(refObject, attributeId, true);
                                    }
                                }
                                else
                                {
                                    throw new Exception("Маппинг на поля данного реестра не поддерживается: " + sourceAttributeData.RegisterId);
                                }
                            }

                            if (attributeValue != null && attributeId.IsNotEmpty())
                            {
                                mapLog.Add(attribute.OmEntityAttrId.ParseToInt(), GetSourceForAttribute(attributeId.ParseToInt()));
                            }
                        }
                        break;

                    case "C":
                        {
                            attributeValue = attribute.Const;
                            attributeValueCode = attribute.ConstCode;
                        }
                        break;

                    case "P":
                        {
                            throw new NotImplementedException();
                        }
                }

                if (attributeValue != null && attributeValue.ToString() == "NULL")
                {
                    attributeValue = null;
                }

                if (attributeValue == null)
                {
                    continue;
                }

                // TODO: выяснить, что делать со значением NULL
                var convertedValue = attributeValue;

                //// Если значение NULL, а поле Value-типа
                //if (attributeValue == null && propertyOMEntity.PropertyType.IsValueType &&
                //	Nullable.GetUnderlyingType(propertyOMEntity.PropertyType) == null)
                //{
                //	convertedValue = Activator.CreateInstance(propertyOMEntity.PropertyType);
                //}

                //propertyOMEntity.SetValue(insurObject, convertedValue);


                if (destinationAttributeData != null && sourceAttributeData != null)
                {
                    if (destinationAttributeData.Type == RegisterAttributeType.INTEGER && sourceAttributeData.Type != RegisterAttributeType.INTEGER)
                    {
                        convertedValue = destinationAttributeData.IsNullable ? convertedValue.ParseToLongNullable() : convertedValue.ParseToLong();
                    }
                    else if (destinationAttributeData.Type == RegisterAttributeType.DECIMAL && sourceAttributeData.Type != RegisterAttributeType.DECIMAL)
                    {
                        convertedValue = destinationAttributeData.IsNullable ? convertedValue.ParseToDecimalNullable() : convertedValue.ParseToDecimal();
                    }
                    else if (destinationAttributeData.Type == RegisterAttributeType.STRING && sourceAttributeData.Type != RegisterAttributeType.STRING)
                    {
                        convertedValue = convertedValue.ToString();
                    }
                }

                try
                {
                    propertyOMEntity.SetValue(insurObject, convertedValue);
                }
                catch (Exception ex)
                {
                    throw ExceptionInitializer.Create("Ошибка маппинга поля", $"Name: {(propertyOMEntity != null ? propertyOMEntity.Name : String.Empty)}; " +
                        $"attribute.OmEntityAttrId: {attribute.OmEntityAttrId}; " +
                        $"attributeValue: {attributeValue}; " +
                        $"sourceAttributeData: {(sourceAttributeData != null ? sourceAttributeData.Id.ToString() : String.Empty)}", ex);
                }

                if (destinationAttributeData.CodeField.IsNotEmpty())
                {
                    PropertyInfo propertyOMEntityCode = typeof(T).GetProperty(destinationAttributeData.InternalName + "_Code");

                    propertyOMEntityCode.SetValue(insurObject, attributeValueCode);
                }
            }

            mapLogAttribute = GetMapLogForAttribute(mapLog);

            return insurObject;
        }

        private static object GetAttributeValue(Object ormObject, string attributeId, bool useReferenceField = false)
        {
            return GetAttributeValue(ormObject, attributeId, useReferenceField, out _);
        }

        private static object GetAttributeValue(Object ormObject, string attributeId, bool useReferenceField, out Type propertyType)
        {
            if (ormObject == null)
            {
                propertyType = typeof(String);
                return null;
            }

            RegisterAttribute registerAttribute = RegisterCache.GetAttributeData(Convert.ToInt32(attributeId));

            string propertyName = registerAttribute.InternalName;

            if (useReferenceField && registerAttribute.ReferenceId != null)
            {
                propertyName += "_Code";
            }

            PropertyInfo property = ormObject.GetType().GetProperty(propertyName);
            Object value = property.GetValue(ormObject);

            propertyType = property.PropertyType;

            if (useReferenceField && registerAttribute.ReferenceId != null && propertyType.IsEnum)
            {
                value = (long)value;
                propertyType = value.GetType();
            }

            return value;
        }

        /// <summary>
        /// Возвращает все исторические и актуальную строки для заданного идентификатора
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        public DataTable GetAllDataById(long id)
        {
            string commandText =
$@"SELECT B.*, O.NAME AS OKRUG, D.NAME AS DISTRICT, A.REGION, A.CITY, A.STREET, A.HOUSE, A.CORPUS, A.STRUCTURE, A.FULL_ADDRESS, A.SOURCE_ADDRESS, A.SOURCE_ADDRESS_CODE
FROM INSUR_BUILDING_Q B
LEFT JOIN REF_ADDR_OKRUG O ON O.OKRUG_ID = B.OKRUG_ID 
LEFT JOIN REF_ADDR_DISTRICT D ON D.DISTRICT_ID = B.DISTRICT_ID
LEFT JOIN INSUR_ADDRESS A ON A.EMP_ID = B.ADDRESS_ID
WHERE B.EMP_ID = {id} ORDER BY B.ACTUAL DESC";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// Возвращает все исторические и актуальную строки для заданного идентификатора из таблицы EHD_BUILD_PARCEL_Q
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        public DataTable GetEhdAllDataById(long id)
        {
            string commandText =
$@"SELECT B.*, R.EMP_ID as R_EMP_ID, R.*, L.EMP_ID as L_EMP_ID, L.DISTRICT, L.REGION, L.LOCALITY, L.STREET, L.LEVEL1, L.LEVEL2, L.LEVEL3, L.ADDRESS_TOTAL, L.APARTMENT
        , A1.ID as A1_ID, A1.YEAR_BUILT as EHD_YEAR_BUILT, A2.ID as A2_ID, A2.FLOORS as EHD_FLOORS, A3.ID as A3_ID, A3.WALL as EHD_WALL, A4.ID as A4_ID, A4.NUMBER as OLD_NUMBER
FROM EHD_BUILD_PARCEL_Q B 
LEFT JOIN EHD_REGISTER_Q R ON R.BUILDING_PARCEL_ID = B.BUILDING_PARCEL_ID 
LEFT JOIN EHD_LOCATION_Q L ON L.BUILDING_PARCEL_ID = B.BUILDING_PARCEL_ID 
LEFT JOIN EHD_EXPLOITATION_CHAR A1 ON A1.BUILDING_PARCEL_ID = B.BUILDING_PARCEL_ID
LEFT JOIN EHD_FLOORS A2 ON A2.BUILDING_PARCEL_ID = B.BUILDING_PARCEL_ID
LEFT JOIN EHD_ELEMENTS_CONSTRUCT A3 ON A3.BUILDING_PARCEL_ID = B.BUILDING_PARCEL_ID
LEFT JOIN EHD_OLD_NUMBERS A4 ON A4.PARCEL_ID = B.BUILDING_PARCEL_ID
WHERE B.EMP_ID = {id} ORDER BY B.ACTUAL DESC";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// Возвращает все исторические и актуальную строки для заданного идентификатора из таблицы BTI_BUILDING_Q
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        public DataTable GetBtiAllDataById(long id)
        {
            #region для теста Архивного адреса в режиме отладке локально
            /*
             SELECT B.*, A.S_ AS AS_, A.PO_ AS APO_, A.CODE_FIAS, A.SUBJECT_RF_NAME, A.TOWN_NAME, A.SETTLEMENT_NAME, A.STREET_NAME, A.HOUSE_NUMBER, A.KORPUS_NUMBER, A.STRUCTURE_NUMBER, A.FULL_NAME, O.NAME AS OKRUG_ID, D.NAME AS DISTRICT_ID, C.S_ AS CS_, C.PO_ AS CPO_
, ba.FULL_NAME AS ARCHIVE, bal.address_status_id
FROM BTI_BUILDING_Q B
     LEFT JOIN BTI_ADDRLINK_Q L ON L.BUILDING_ID=B.EMP_ID AND L.ADDRESS_STATUS_ID=685
     LEFT JOIN BTI_ADDRESS_Q A ON A.EMP_ID=L.ADDRESS_ID
     LEFT JOIN BTI_ADDRLINK_Q AL ON AL.BUILDING_ID=B.EMP_ID AND AL.ADDRESS_STATUS_ID=688 --!!!!!!
     LEFT JOIN BTI_ADDRESS_Q C ON C.EMP_ID=AL.ADDRESS_ID                                 --!!!!!!
     LEFT JOIN REF_ADDR_OKRUG O ON O.OKRUG_ID=A.OKRUG_ID
     LEFT JOIN REF_ADDR_DISTRICT D ON D.DISTRICT_ID=A.DISTRICT_ID

 	 left join insur_building_q ib ON ib.link_bti_fsks = b.emp_id and ib.actual = 1
	 --left join bti_building_Q bb on bb.emp_id = ib.link_bti_fsks
	 left join bti_addrlink_q bal on bal.building_id = b.emp_id and lower(bal.address_status_name) = 'архивный'
	 left join bti_address_q ba on bal.address_id = ba.emp_id

WHERE B.EMP_ID=6049133
ORDER BY B.S_ DESC             
             */
            #endregion

            string commandText =
$@"SELECT B.*, A.EMP_ID as A_EMP_ID, A.CODE_FIAS, A.SUBJECT_RF_NAME, A.TOWN_NAME, A.SETTLEMENT_NAME, A.STREET_NAME, A.HOUSE_NUMBER, A.KORPUS_NUMBER, A.STRUCTURE_NUMBER, A.FULL_NAME, O.OKRUG_ID AS O_OKRUG_ID, O.NAME AS OKRUG_ID, D.DISTRICT_ID AS D_DISTRICT_ID, D.NAME AS DISTRICT_ID, BA.EMP_ID as BA_EMP_ID, BA.FULL_NAME AS ARCHIVE
FROM BTI_BUILDING_Q B
     LEFT JOIN BTI_ADDRLINK_Q L ON L.BUILDING_ID = B.EMP_ID AND L.ADDRESS_STATUS_ID = 685
     LEFT JOIN BTI_ADDRESS_Q A ON A.EMP_ID = L.ADDRESS_ID
     LEFT JOIN BTI_ADDRLINK_Q AL ON AL.BUILDING_ID = B.EMP_ID AND AL.ADDRESS_STATUS_ID = 688 --!!!!!!
     LEFT JOIN BTI_ADDRESS_Q C ON C.EMP_ID = AL.ADDRESS_ID                                 --!!!!!!
     LEFT JOIN REF_ADDR_OKRUG O ON O.OKRUG_ID = A.OKRUG_ID
     LEFT JOIN REF_ADDR_DISTRICT D ON D.DISTRICT_ID = A.DISTRICT_ID

     /* Для отображения Архивного адреса в карточке МКД */
 	 LEFT JOIN INSUR_BUILDING_Q IB ON IB.LINK_BTI_FSKS = B.EMP_ID AND IB.ACTUAL = 1
	 LEFT JOIN BTI_ADDRLINK_Q BAL ON BAL.BUILDING_ID = B.EMP_ID AND BAL.ADDRESS_STATUS_ID = 773
	 LEFT JOIN BTI_ADDRESS_Q BA ON BAL.ADDRESS_ID = BA.EMP_ID

WHERE B.EMP_ID = {id} ORDER BY B.ACTUAL DESC";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// CIPJS-52 Площадь помещений, не входящих в общую площадь нужно определять на основании данных БТИ ,
        /// находим объект БТИ связанный с INSUR_BUILDING, далее находим помещения связанные с объектом БТИ,
        /// отбираем только помещения "не входящие в общую площадь", для них определяем total_area.
        /// Через  реестр связи , если найдено объектов несколько , то унжно выбирать тот для которого INSUR_LINK_BUILD_BTI . FLAG_DUBL_UNOM =0
        /// class_name = Нежилые помещения
        /// CIPJS-339 Отобрать все помещения по зданию БТИ со следующими параметрами: класс помещений 3 (Вне квартир (за итогом) , суммировать по атрибуту bti_ premase. ZPL;
        /// </summary>
        /// <param name="buildingId">Идентификатор здания страхования</param>
        /// <returns></returns>
        public decimal? GetBtiBuildingOverallArea(long buildingId)
        {
            OMLinkBuildBti linkBuildBti = OMLinkBuildBti
                .Where(x => x.IdInsurBuild == buildingId && (x.FlagDublUnom == null || x.FlagDublUnom == true))
                .Select(x => x.IdBtiFsks)
                .Execute()
                .FirstOrDefault();

            if (linkBuildBti == null || !linkBuildBti.IdBtiFsks.HasValue)
            {
                return null;
            }

            ObjectModel.Bti.OMBtiBuilding btiBuilding = ObjectModel.Bti.OMBtiBuilding
                .Where(x => x.EmpId == linkBuildBti.IdBtiFsks.Value).Execute().FirstOrDefault();

            if (btiBuilding == null)
            {
                return null;
            }

            List<ObjectModel.Bti.OMFloor> buildingFloors = ObjectModel.Bti.OMFloor
                .Where(x => x.BuildingId == btiBuilding.EmpId)
                .Select(x => x.Premase[0].TotalArea)
                .Select(x => x.Premase[0].ClassName)
                .Select(x => x.Premase[0].ClassName_Code)
                .Select(x => x.Premase[0].Zpl)
                .Execute();

            List<OMReferenceItem> premiseClassReferenceItems = ReferencesCommon.GetItems(30);

            if (premiseClassReferenceItems.Count == 0)
            {
                return null;
            }

            decimal? overallArea = null;

            //код 4 Вне квартир (за итогом) по БТИ KLS
            long? outsidePremiseItemId = premiseClassReferenceItems.FirstOrDefault(x => x.Code == "4")?.ItemId;

            foreach (ObjectModel.Bti.OMFloor floor in buildingFloors)
            {
                if (floor.Premase == null || floor.Premase.Count == 0)
                {
                    continue;
                }

                foreach (ObjectModel.Bti.OMPremase premise in floor.Premase)
                {
                    if (outsidePremiseItemId.HasValue
                        && premise.ClassName_Code == outsidePremiseItemId.Value
                        && premise.Zpl.HasValue)
                    {
                        overallArea = (overallArea ?? 0) + premise.TotalArea.Value;
                    }
                }
            }

            return overallArea;
        }

        /// <summary>
        /// Получаем диапазон квартир дома.
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public string GetBuildingDiapKv(long? empId)
        {
            var response = string.Empty;
            if(empId == null)
            {
                return response;
            }
            OMBuilding build = OMBuilding.Where(x => x.EmpId == empId).Select(x => x.LinkBtiFsks).ExecuteFirstOrDefault();
            if (build == null)
                return response;

            var diapKv = OMDiapKv.Where(x => x.EmpId == build.LinkBtiFsks).Select(x => x.I1).Select(x => x.I2).ExecuteFirstOrDefault();
            if (diapKv == null)
                return response;

            response = $"{diapKv.I1} - {diapKv.I2}";

            return response;
        }

        /// <summary>
        /// Получение главного адреса здания.
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public string GetBuildingBtiMainAddress(long? empId)
        {
            var response = string.Empty;
            if (empId == null)
            {
                return response;
            }

            string query = $@"
select a.full_name
from insur_building_q ib
join bti_building_q bb on bb.emp_id = ib.link_bti_fsks and bb.actual = 1
join bti_addrlink_q al on al.building_id = bb.emp_id and al.actual = 1
join bti_address_q a on al.address_id = a.emp_id and a.actual = 1
where ib.actual = 1 and al.address_status_id in (770, 685) and ib.emp_id = {empId} and a.full_name is not null";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(query);
            var rows = DBMngr.Realty.ExecuteDataSet(command).Tables[0].Rows;
            if (rows.Count > 0)
            {
                var row = rows[0];
                response = row["full_name"].ToString();
            }
            return response;
        }

        /// <summary>
        /// Возвращает UNOM-ы всех связанных БТИ объектов
        /// </summary>
        /// <param name="id">Идентификатор объекта МКД</param>
        public List<long?> GetAllBtiUnoms(long id)
        {
            return OMLinkBuildBti.Where(x => x.IdInsurBuild == id)
                .Select(x => x.ParentBuilding.Unom).Execute()
                .Where(x => x.ParentBuilding?.Unom != null)
                .Select(x => x.ParentBuilding?.Unom).ToList();
        }

        /// <summary>
        /// Общая площадь по зданию ( включая холодные помещения), кв.м =
        /// Общая площадь по зданию + Площадь балконов + Площадь лоджий
        /// </summary>
        /// <returns></returns>
        public decimal? GetBuildingOplc(OMBuilding building)
        {
            if (building == null)
            {
                return null;
            }

            if (!building.Opl.HasValue
                && !building.Bpl.HasValue
                && !building.Lpl.HasValue)
            {
                return null;
            }

            decimal oplValue = building.Opl ?? 0m;
            decimal bplValue = building.Bpl ?? 0m;
            decimal lplValue = building.Lpl ?? 0m;

            return oplValue + bplValue + lplValue;
        }

        /// <summary>
        /// Обновляет существующий объект
        /// </summary>
        /// <param name="omBuilding">Объект с новыми данными</param>
        public void Update(OMBuilding omBuilding)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_OBJ_BUILDING_WRITE, true, false, true);

            OMBuilding currentOMBuilding = OMBuilding.Where(x => x.EmpId == omBuilding.EmpId).SelectAll().Execute().FirstOrDefault();

            if (currentOMBuilding == null) throw new Exception($"Объект не найден (ИД={omBuilding.EmpId})");

            OMAddress currentOMAddress = null;

            if (currentOMBuilding.AddressId.HasValue)
            {
                currentOMAddress = OMAddress.Where(x => x.EmpId == currentOMBuilding.AddressId.Value).SelectAll().Execute().FirstOrDefault();
            }

            Dictionary<string, SourceInput> sources = null;

            try
            {
                sources = JsonConvert.DeserializeObject<Dictionary<string, SourceInput>>(currentOMBuilding.AttributeSource) ?? new Dictionary<string, SourceInput>();
            }
            catch { sources = new Dictionary<string, SourceInput>(); }

            if ((currentOMBuilding.LoadDate.HasValue && !omBuilding.LoadDate.HasValue)
                || (!currentOMBuilding.LoadDate.HasValue && omBuilding.LoadDate.HasValue)
                || (currentOMBuilding.LoadDate.HasValue && omBuilding.LoadDate.HasValue
                && currentOMBuilding.LoadDate.Value.Date != omBuilding.LoadDate.Value.Date))
            {
                currentOMBuilding.LoadDate = omBuilding.LoadDate.HasValue ? (DateTime?)omBuilding.LoadDate.Value.Date : null;
                sources[OMBuilding.GetAttributeData(x => x.LoadDate).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.FlagInsur != omBuilding.FlagInsur)
            {
                currentOMBuilding.FlagInsur = omBuilding.FlagInsur;
                sources[OMBuilding.GetAttributeData(x => x.FlagInsur).ValueField] = SourceInput.ManualInput;

                CopyFlatFlagInsurFromBuilding(currentOMBuilding.EmpId, omBuilding.FlagInsur);

            }
            if (currentOMBuilding.Unom != omBuilding.Unom)
            {
                currentOMBuilding.Unom = omBuilding.Unom;
                sources[OMBuilding.GetAttributeData(x => x.Unom).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.TypeMkd_Code != omBuilding.TypeMkd_Code)
            {
                currentOMBuilding.TypeMkd_Code = omBuilding.TypeMkd_Code;
                OMReferenceItem refItem = OMReferenceItem.Where(x => x.ItemId == (long)currentOMBuilding.TypeMkd_Code).Select(x => x.Value).ExecuteFirstOrDefault();
                if (refItem != null)
                {
                    currentOMBuilding.TypeMkd = refItem.Value;
                }

                sources[OMBuilding.GetAttributeData(x => x.TypeMkd).ValueField] = SourceInput.ManualInput;
                sources[OMBuilding.GetAttributeData(x => x.TypeMkd_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.CadasrNum != omBuilding.CadasrNum)
            {
                currentOMBuilding.CadasrNum = omBuilding.CadasrNum;
                sources[OMBuilding.GetAttributeData(x => x.CadasrNum).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.CadastrDate != omBuilding.CadastrDate)
            {
                currentOMBuilding.CadastrDate = omBuilding.CadastrDate;
                sources[OMBuilding.GetAttributeData(x => x.CadastrDate).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.StatusEgrn_Code != omBuilding.StatusEgrn_Code)
            {
                currentOMBuilding.StatusEgrn_Code = omBuilding.StatusEgrn_Code;
                sources[OMBuilding.GetAttributeData(x => x.StatusEgrn).ValueField] = SourceInput.ManualInput;
                sources[OMBuilding.GetAttributeData(x => x.StatusEgrn_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.PurposeName_Code != omBuilding.PurposeName_Code)
            {
                currentOMBuilding.PurposeName_Code = omBuilding.PurposeName_Code;
                currentOMBuilding.PurposeName = OMReferenceItem.Where(x => x.ItemId == (long)omBuilding.PurposeName_Code).Select(x => x.Value)?.ExecuteFirstOrDefault()?.Value;
                sources[OMBuilding.GetAttributeData(x => x.PurposeName).ValueField] = SourceInput.ManualInput;
                sources[OMBuilding.GetAttributeData(x => x.PurposeName_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.StatusSostBti_Code != omBuilding.StatusSostBti_Code)
            {
                currentOMBuilding.StatusSostBti_Code = omBuilding.StatusSostBti_Code;
                currentOMBuilding.StatusSostBti = OMReferenceItem.Where(x => x.ItemId == (long)omBuilding.StatusSostBti_Code).Select(x => x.Value)?.ExecuteFirstOrDefault()?.Value;
                sources[OMBuilding.GetAttributeData(x => x.StatusSostBti).ValueField] = SourceInput.ManualInput;
                sources[OMBuilding.GetAttributeData(x => x.StatusSostBti_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.OkrugId != omBuilding.OkrugId)
            {
                currentOMBuilding.OkrugId = omBuilding.OkrugId;
                sources["OKRUG"] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.DistrictId != omBuilding.DistrictId)
            {
                currentOMBuilding.DistrictId = omBuilding.DistrictId;
                sources["DISTRICT"] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.YearStroi != omBuilding.YearStroi)
            {
                currentOMBuilding.YearStroi = omBuilding.YearStroi;
                sources[OMBuilding.GetAttributeData(x => x.YearStroi).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.KolGp != omBuilding.KolGp)
            {
                currentOMBuilding.KolGp = omBuilding.KolGp;
                sources[OMBuilding.GetAttributeData(x => x.KolGp).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.CountFloor != omBuilding.CountFloor)
            {
                currentOMBuilding.CountFloor = omBuilding.CountFloor;
                sources[OMBuilding.GetAttributeData(x => x.CountFloor).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Lfpq != omBuilding.Lfpq)
            {
                currentOMBuilding.Lfpq = omBuilding.Lfpq;
                sources[OMBuilding.GetAttributeData(x => x.Lfpq).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Lfgpq != omBuilding.Lfgpq)
            {
                currentOMBuilding.Lfgpq = omBuilding.Lfgpq;
                sources[OMBuilding.GetAttributeData(x => x.Lfgpq).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Opl != omBuilding.Opl)
            {
                currentOMBuilding.Opl = omBuilding.Opl;
                sources[OMBuilding.GetAttributeData(x => x.Opl).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.OplN != omBuilding.OplN)
            {
                currentOMBuilding.OplN = omBuilding.OplN;
                sources[OMBuilding.GetAttributeData(x => x.OplN).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.OplG != omBuilding.OplG)
            {
                currentOMBuilding.OplG = omBuilding.OplG;
                sources[OMBuilding.GetAttributeData(x => x.OplG).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Bpl != omBuilding.Bpl)
            {
                currentOMBuilding.Bpl = omBuilding.Bpl;
                sources[OMBuilding.GetAttributeData(x => x.Bpl).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Lpl != omBuilding.Lpl)
            {
                currentOMBuilding.Lpl = omBuilding.Lpl;
                sources[OMBuilding.GetAttributeData(x => x.Lpl).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Hpl != omBuilding.Hpl)
            {
                currentOMBuilding.Hpl = omBuilding.Hpl;
                sources[OMBuilding.GetAttributeData(x => x.Hpl).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Krovpl != omBuilding.Krovpl)
            {
                currentOMBuilding.Krovpl = omBuilding.Krovpl;
                sources[OMBuilding.GetAttributeData(x => x.Krovpl).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.GuidFiasMkd != omBuilding.GuidFiasMkd)
            {
                currentOMBuilding.GuidFiasMkd = omBuilding.GuidFiasMkd;
                sources[OMBuilding.GetAttributeData(x => x.GuidFiasMkd).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Note != omBuilding.Note)
            {
                currentOMBuilding.Note = omBuilding.Note;
                sources[OMBuilding.GetAttributeData(x => x.Note).ValueField] = SourceInput.ManualInput;
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                UpdateAddress(sources, omBuilding.ParentAddress, currentOMAddress);

                if (currentOMAddress == null)
                {
                    currentOMBuilding.AddressId = omBuilding.ParentAddress.EmpId;
                }

                string attributeSource = JsonConvert.SerializeObject(sources);
                if (currentOMBuilding.AttributeSource != attributeSource)
                {
                    currentOMBuilding.AttributeSource = attributeSource;
                }

                if (currentOMBuilding.PropertyChangedList.Count > 0)
                {
                    currentOMBuilding.Save();
                }

                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_OBJ_BUILDING_WRITE, true, "МКД обновлен",
                             OMBuilding.GetRegisterId(), currentOMBuilding.EmpId);

                ts.Complete();
            }
        }

        /// <summary>
        /// Копирование признака "Подлежит страхованию" из карточки МКД
        /// </summary>
        /// <param name="building"></param>
        public static void CopyFlatFlagInsurFromBuilding(long? buildingId, bool? flagInsur)
        {
            if (buildingId != null && flagInsur != null)
            {
                List<OMFlat> flats = OMFlat.Where(x => x.LinkObjectMkd == buildingId && x.SpecialActual == 1 && x.FlagInsur != flagInsur).Execute();
                foreach (var flat in flats)
                {
                    flat.FlagInsur = flagInsur;
                    flat.Save();
                }
            }
        }

        /// <summary>
        /// Обновляет существующий объект
        /// </summary>
        /// <param name="omBuilding">Объект с новыми данными</param>
        public void UpdateNote(OMBuilding omBuilding)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_OBJ_BUILDING_WRITE, true, false, true);

            OMBuilding currentOMBuilding = OMBuilding.Where(x => x.EmpId == omBuilding.EmpId).SelectAll().Execute().FirstOrDefault();

            if (currentOMBuilding == null) throw new Exception($"Объект не найден (ИД={omBuilding.EmpId})");

            Dictionary<string, SourceInput> sources = null;

            try
            {
                sources = JsonConvert.DeserializeObject<Dictionary<string, SourceInput>>(currentOMBuilding.AttributeSource) ?? new Dictionary<string, SourceInput>();
            }
            catch { sources = new Dictionary<string, SourceInput>(); }

            if (currentOMBuilding.Note != omBuilding.Note)
            {
                currentOMBuilding.Note = omBuilding.Note;
                sources[OMBuilding.GetAttributeData(x => x.Note).ValueField] = SourceInput.ManualInput;
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                if (currentOMBuilding.PropertyChangedList.Count > 0)
                {
                    currentOMBuilding.Save();
                }

                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_OBJ_BUILDING_WRITE, true, "МКД обновлен",
                             OMBuilding.GetRegisterId(), currentOMBuilding.EmpId);

                ts.Complete();
            }
        }

        /// <summary>
        /// Создает объект МКД
        /// </summary>
        /// <param name="omBuilding">Объект МКД</param>
        public long Create(OMBuilding omBuilding)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_OBJ_BUILDING, true, false, true);

            Dictionary<string, SourceInput> sources = new Dictionary<string, SourceInput>();

            if (omBuilding.LoadDate.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.LoadDate).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.FlagInsur.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.FlagInsur).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Unom.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.Unom).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.TypeMkd_Code != 0)
            {
                sources[OMBuilding.GetAttributeData(x => x.TypeMkd).ValueField] = SourceInput.ManualInput;
                sources[OMBuilding.GetAttributeData(x => x.TypeMkd_Code).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.CadasrNum.IsNotEmpty())
            {
                sources[OMBuilding.GetAttributeData(x => x.CadasrNum).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.CadastrDate.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.CadastrDate).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.StatusEgrn_Code != State.None)
            {
                sources[OMBuilding.GetAttributeData(x => x.StatusEgrn).ValueField] = SourceInput.ManualInput;
                sources[OMBuilding.GetAttributeData(x => x.StatusEgrn_Code).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.PurposeName_Code != Purpose.None)
            {
                sources[OMBuilding.GetAttributeData(x => x.PurposeName).ValueField] = SourceInput.ManualInput;
                sources[OMBuilding.GetAttributeData(x => x.PurposeName_Code).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.StatusSostBti_Code != StructureStatus.None)
            {
                sources[OMBuilding.GetAttributeData(x => x.StatusSostBti).ValueField] = SourceInput.ManualInput;
                sources[OMBuilding.GetAttributeData(x => x.StatusSostBti_Code).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.OkrugId.HasValue)
            {
                sources["OKRUG"] = SourceInput.ManualInput;
            }
            if (omBuilding.DistrictId.HasValue)
            {

                sources["DISTRICT"] = SourceInput.ManualInput;
            }
            if (omBuilding.YearStroi.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.YearStroi).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.KolGp.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.KolGp).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.CountFloor.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.CountFloor).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Lfpq.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.Lfpq).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Lfgpq.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.Lfgpq).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Opl.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.Opl).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.OplN.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.OplN).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.OplG.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.OplG).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Bpl.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.Bpl).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Lpl.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.Lpl).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Hpl.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.Hpl).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Krovpl.HasValue)
            {
                sources[OMBuilding.GetAttributeData(x => x.Krovpl).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.GuidFiasMkd.IsNotEmpty())
            {
                sources[OMBuilding.GetAttributeData(x => x.GuidFiasMkd).ValueField] = SourceInput.ManualInput;
            }
            if (omBuilding.Note.IsNotEmpty())
            {
                sources[OMBuilding.GetAttributeData(x => x.Note).ValueField] = SourceInput.ManualInput;
            }

            long result;

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                UpdateAddress(sources, omBuilding.ParentAddress);
                omBuilding.EmpId = -1;
                omBuilding.AttributeSource = JsonConvert.SerializeObject(sources);
                result = omBuilding.Save();

                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_OBJ_BUILDING, true, "МКД добавлен",
                             OMBuilding.GetRegisterId(), omBuilding.EmpId);

                ts.Complete();
            }

            return result;
        }

        /// <summary>
        /// Получение списка МКД, у которых есть привязка к ЕГРН, но нет к БТИ
        /// </summary>
        /// <returns></returns>
        public List<OMBuilding> GetBuildingWithoutBti(List<long> selectedList)
        {
            List<OMBuilding> buildings = null;
            if (selectedList.Count > 0)
            {
                buildings = OMBuilding.Where(x => x.LinkBtiFsks == null && x.LinkEgrnBild != null && selectedList.Contains(x.EmpId))
                .SelectAll()
                .Select(x => x.ParentBtiOkrug.Id)
                .Select(x => x.ParentBtiOkrug.ShortName)
                .Select(x => x.ParentBtiDistrict.Id)
                .Select(x => x.ParentBtiDistrict.ShortName)
                .Select(x => x.ParentAddress.EmpId)
                .Select(x => x.ParentAddress.PostalCode)
                .Select(x => x.ParentAddress.Region)
                .Select(x => x.ParentAddress.City)
                .Select(x => x.ParentAddress.TypeStreet)
                .Select(x => x.ParentAddress.Street)
                .Select(x => x.ParentAddress.House)
                .Select(x => x.ParentAddress.Corpus)
                .Select(x => x.ParentAddress.Structure)
                .Select(x => x.ParentAddress.FullAddress)
                .Execute()
                .ToList();

                return buildings;
            }
            buildings = OMBuilding.Where(x => x.LinkBtiFsks == null && x.LinkEgrnBild != null)
                 .SelectAll()
                 .Select(x => x.ParentBtiOkrug.Id)
                 .Select(x => x.ParentBtiOkrug.ShortName)
                 .Select(x => x.ParentBtiDistrict.Id)
                 .Select(x => x.ParentBtiDistrict.ShortName)
                 .Select(x => x.ParentAddress.EmpId)
                 .Select(x => x.ParentAddress.PostalCode)
                 .Select(x => x.ParentAddress.Region)
                 .Select(x => x.ParentAddress.City)
                 .Select(x => x.ParentAddress.TypeStreet)
                 .Select(x => x.ParentAddress.Street)
                 .Select(x => x.ParentAddress.House)
                 .Select(x => x.ParentAddress.Corpus)
                 .Select(x => x.ParentAddress.Structure)
                 .Select(x => x.ParentAddress.FullAddress)
                 .Execute()
                 .ToList();

            return buildings;
        }

        /// <summary>
        /// Получение списка МКД, у которых есть привязка к БТИ, но нет к ЕГРН
        /// </summary>
        /// <param name="building">отбираем только записи, у которых есть совпадение с полями из building</param>
        /// <returns></returns>
        public List<OMBuilding> GetBuildingWithoutFsks(OMBuilding building)
        {
            if (building == null)
            {
                return new List<OMBuilding>();
            }
            List<OMBuilding> buildings = OMBuilding.Where(x => x.LinkBtiFsks != null && x.LinkEgrnBild == null &&
                ((x.CadasrNum == building.CadasrNum && building.CadasrNum != "") ||
                (x.GuidFiasMkd == building.GuidFiasMkd && building.GuidFiasMkd != "") ||
                (x.AddressId == building.AddressId && building.AddressId != null) ||
                (x.YearStroi == building.YearStroi && building.YearStroi != null) ||
                (x.Opl == building.Opl && building.Opl != null) ||
                (x.Unom == building.Unom && building.Unom != null) ||
                (x.KolGp == building.KolGp && building.KolGp != null)))
                .SelectAll()
                .Select(x => x.ParentBtiOkrug.Id)
                .Select(x => x.ParentBtiOkrug.ShortName)
                .Select(x => x.ParentBtiDistrict.Id)
                .Select(x => x.ParentBtiDistrict.ShortName)
                .Select(x => x.ParentAddress.EmpId)
                .Select(x => x.ParentAddress.PostalCode)
                .Select(x => x.ParentAddress.Region)
                .Select(x => x.ParentAddress.City)
                .Select(x => x.ParentAddress.TypeStreet)
                .Select(x => x.ParentAddress.Street)
                .Select(x => x.ParentAddress.House)
                .Select(x => x.ParentAddress.Corpus)
                .Select(x => x.ParentAddress.Structure)
                .Select(x => x.ParentAddress.FullAddress)
                .Execute()
                .ToList();

            return buildings;
        }

        /// <summary>
        /// Получение списка МКД, у которых есть привязка к ЕГРН, но нет к БТИ и найдена аналогичная запись с привязкой к БТИ, но без ЕГРН
        /// </summary>
        /// <returns></returns>
        public List<OMBuilding> GetBuildingWithoutBtiWithSingleLink(List<long> selectedList)
        {
            List<OMBuilding> totalBuidlings = new List<OMBuilding>();
            List<OMBuilding> buildings;

            if (selectedList.Count > 0)
            {
                buildings = OMBuilding.Where(x => x.LinkBtiFsks == null && x.LinkEgrnBild != null && selectedList.Contains(x.EmpId))
                .SelectAll()
                .Select(x => x.ParentBtiOkrug.Id)
                .Select(x => x.ParentBtiOkrug.ShortName)
                .Select(x => x.ParentBtiDistrict.Id)
                .Select(x => x.ParentBtiDistrict.ShortName)
                .Select(x => x.ParentAddress.EmpId)
                .Select(x => x.ParentAddress.PostalCode)
                .Select(x => x.ParentAddress.Region)
                .Select(x => x.ParentAddress.City)
                .Select(x => x.ParentAddress.TypeStreet)
                .Select(x => x.ParentAddress.Street)
                .Select(x => x.ParentAddress.House)
                .Select(x => x.ParentAddress.Corpus)
                .Select(x => x.ParentAddress.Structure)
                .Select(x => x.ParentAddress.FullAddress)
                .Execute()
                .ToList();
            }
            else
            {
                buildings = OMBuilding.Where(x => x.LinkBtiFsks == null && x.LinkEgrnBild != null)
                .SelectAll()
                .Select(x => x.ParentBtiOkrug.Id)
                .Select(x => x.ParentBtiOkrug.ShortName)
                .Select(x => x.ParentBtiDistrict.Id)
                .Select(x => x.ParentBtiDistrict.ShortName)
                .Select(x => x.ParentAddress.EmpId)
                .Select(x => x.ParentAddress.PostalCode)
                .Select(x => x.ParentAddress.Region)
                .Select(x => x.ParentAddress.City)
                .Select(x => x.ParentAddress.TypeStreet)
                .Select(x => x.ParentAddress.Street)
                .Select(x => x.ParentAddress.House)
                .Select(x => x.ParentAddress.Corpus)
                .Select(x => x.ParentAddress.Structure)
                .Select(x => x.ParentAddress.FullAddress)
                .Execute()
                .ToList();
            }

            List<OMBuilding> btiBuildings = OMBuilding.Where(x => x.LinkBtiFsks != null && x.LinkEgrnBild == null).SelectAll().Execute().ToList();
            //нужно убедиться, что есть однозначное совпадение с записью, у которой есть привязка только БТИ, и она одна
            for (int i = 0; i < buildings.Count; ++i)
            {
                List<OMBuilding> btiBuildingSelect = btiBuildings.Where(x =>
                        (((x.CadasrNum == buildings[i].CadasrNum && buildings[i].CadasrNum != "") || (buildings[i].CadasrNum == "" && x.CadasrNum == "")) &&
                        (((x.GuidFiasMkd == buildings[i].GuidFiasMkd && buildings[i].GuidFiasMkd != "") || (buildings[i].GuidFiasMkd == "" && x.GuidFiasMkd == "")) ||
                        ((x.AddressId == buildings[i].AddressId && buildings[i].AddressId != null) || (buildings[i].AddressId == null && x.AddressId == null))) &&
                        ((x.YearStroi == buildings[i].YearStroi && buildings[i].YearStroi != null) || (buildings[i].YearStroi == null && x.YearStroi == null)) &&
                        ((x.Opl == buildings[i].Opl && buildings[i].Opl != null) || (buildings[i].Opl == null && x.Opl == null)) &&
                        ((x.Unom == buildings[i].Unom && buildings[i].Unom != null) || (buildings[i].Unom == null && x.Unom == null)) &&
                        ((x.KolGp == buildings[i].KolGp && buildings[i].KolGp != null) || (buildings[i].KolGp == null && x.KolGp == null))))
                        .ToList();
                if (btiBuildingSelect.Count == 1)
                {
                    // проверка, что ЖП у этих МКд нормально связываются
                    FlatService service = new FlatService();
                    string message = service.CheckLinkFlats(buildings[i].EmpId, btiBuildingSelect.First().EmpId);
                    // если вернулся текст, то есть ошибки
                    if (!message.IsNotEmpty())
                    {
                        totalBuidlings.AddRange(btiBuildingSelect);
                    }
                    else
                    {
                        buildings.RemoveAt(i--);
                    }
                }
                else
                {
                    buildings.RemoveAt(i--);
                }
            }
            if (buildings.Count > 0)
            {
                totalBuidlings.AddRange(buildings);
            }

            return totalBuidlings;
        }

        /// <summary>
        /// Обновляет существующий объект
        /// </summary>
        /// <param name="omBuilding">Объект с новыми данными</param>
        public OMBuilding UpdateBuildingByLink(OMBuilding omBuilding, string reason)
        {
            SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.INSUR_OBJ_BUILDING_WRITE, true, false, true);

            OMBuilding currentOMBuilding = OMBuilding.Where(x => x.EmpId == omBuilding.EmpId).SelectAll()
                .Select(x => x.ParentAddress.EmpId)
                .Select(x => x.ParentAddress.PostalCode)
                .Select(x => x.ParentAddress.Region)
                .Select(x => x.ParentAddress.City)
                .Select(x => x.ParentAddress.TypeStreet)
                .Select(x => x.ParentAddress.Street)
                .Select(x => x.ParentAddress.House)
                .Select(x => x.ParentAddress.Corpus)
                .Select(x => x.ParentAddress.Structure)
                .Select(x => x.ParentAddress.FullAddress).Execute().FirstOrDefault();
            OMAddress currentOMAddress = OMAddress.Where(x => x.EmpId == omBuilding.AddressId).SelectAll().Execute().FirstOrDefault();

            if (currentOMBuilding == null) throw new Exception($"Объект не найден (ИД={omBuilding.EmpId})");

            Dictionary<string, SourceInput> sources = null;

            try
            {
                sources = JsonConvert.DeserializeObject<Dictionary<string, SourceInput>>(currentOMBuilding.AttributeSource) ?? new Dictionary<string, SourceInput>();
            }
            catch { sources = new Dictionary<string, SourceInput>(); }

            if (currentOMBuilding.Unom != omBuilding.Unom)
            {
                new OMChangesLog
                {
                    ObjectId = omBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.Unom,
                    Reason = reason,
                    OldValue = currentOMBuilding.Unom.ToString(),
                    NewValue = omBuilding.Unom.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMBuilding.Unom = omBuilding.Unom;
                sources[OMBuilding.GetAttributeData(x => x.Unom).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.CadasrNum != omBuilding.CadasrNum)
            {
                new OMChangesLog
                {
                    ObjectId = omBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.CadastrNum,
                    Reason = reason,
                    OldValue = currentOMBuilding.CadasrNum.ToString(),
                    NewValue = omBuilding.CadasrNum.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMBuilding.CadasrNum = omBuilding.CadasrNum;
                sources[OMBuilding.GetAttributeData(x => x.CadasrNum).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.OkrugId != omBuilding.OkrugId)
            {
                new OMChangesLog
                {
                    ObjectId = omBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.OkrugId,
                    Reason = reason,
                    OldValue = currentOMBuilding.OkrugId.ToString(),
                    NewValue = omBuilding.OkrugId.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMBuilding.OkrugId = omBuilding.OkrugId;
                sources["OKRUG"] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.DistrictId != omBuilding.DistrictId)
            {
                new OMChangesLog
                {
                    ObjectId = omBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.DistrictId,
                    Reason = reason,
                    OldValue = currentOMBuilding.DistrictId.ToString(),
                    NewValue = omBuilding.DistrictId.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMBuilding.DistrictId = omBuilding.DistrictId;
                sources["DISTRICT"] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.YearStroi != omBuilding.YearStroi)
            {
                new OMChangesLog
                {
                    ObjectId = omBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.YearStroi,
                    Reason = reason,
                    OldValue = currentOMBuilding.YearStroi.ToString(),
                    NewValue = omBuilding.YearStroi.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMBuilding.YearStroi = omBuilding.YearStroi;
                sources[OMBuilding.GetAttributeData(x => x.YearStroi).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.KolGp != omBuilding.KolGp)
            {
                new OMChangesLog
                {
                    ObjectId = omBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.KolGp,
                    Reason = reason,
                    OldValue = currentOMBuilding.KolGp.ToString(),
                    NewValue = omBuilding.KolGp.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMBuilding.KolGp = omBuilding.KolGp;
                sources[OMBuilding.GetAttributeData(x => x.KolGp).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.Opl != omBuilding.Opl)
            {
                new OMChangesLog
                {
                    ObjectId = omBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.Opl,
                    Reason = reason,
                    OldValue = currentOMBuilding.Opl.ToString(),
                    NewValue = omBuilding.Opl.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMBuilding.Opl = omBuilding.Opl;
                sources[OMBuilding.GetAttributeData(x => x.Opl).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMBuilding.AddressId != omBuilding.AddressId)
            {
                new OMChangesLog
                {
                    ObjectId = omBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.AddressId,
                    Reason = reason,
                    OldValue = currentOMBuilding.AddressId.ToString(),
                    NewValue = omBuilding.AddressId.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMBuilding.AddressId = omBuilding.AddressId;
                sources[OMBuilding.GetAttributeData(x => x.AddressId).ValueField] = SourceInput.ManualInput;
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                if (omBuilding.ParentAddress != null)
                {
                    UpdateAddress(sources, omBuilding.ParentAddress, currentOMAddress);
                }
                currentOMBuilding.AttributeSource = JsonConvert.SerializeObject(sources);
                currentOMBuilding.Save();

                SRDAudit.Add(ObjectModel.SRD.SRDCoreFunctions.INSUR_OBJ_BUILDING_WRITE, true, "МКД обновлен",
                             OMBuilding.GetRegisterId(), currentOMBuilding.EmpId);

                ts.Complete();
            }

            return currentOMBuilding;
        }

        /// <summary>
        /// Перевод МКД в статус "не актуален"
        /// </summary>
        /// <param name="id"></param>
        public void BuildingNotActual(long? id)
        {
            if (id.HasValue)
            {
                OMBuilding buildong = OMBuilding.Where(x => x.EmpId == id).SelectAll(false).Execute().FirstOrDefault();
                if (buildong != null)
                {
                    buildong.DestroyLogically();
                }
            }
        }

        /// <summary>
        /// CIPJS-800:Ручная связка объектов
        /// </summary>
        /// <param name="buildingOneId"></param>
        /// <param name="buildingTwoId"></param>
        public List<OMBuilding> GetTwoBuildingForLink(long buildingOneId, long buildingTwoId, ref string mes)
        {
            OMBuilding firstBuilding = OMBuilding.Where(x => x.EmpId == buildingOneId).Select(x => x.LinkEgrnBild).Select(x => x.LinkBtiFsks).Select(x => x.Unom).Select(x => x.CadasrNum).Select(x => x.ParentAddress.FullAddress).Execute().FirstOrDefault();
            OMBuilding secondBuilding = OMBuilding.Where(x => x.EmpId == buildingTwoId).Select(x => x.LinkEgrnBild).Select(x => x.LinkBtiFsks).Select(x => x.Unom).Select(x => x.CadasrNum).Select(x => x.ParentAddress.FullAddress).Execute().FirstOrDefault();
            if (firstBuilding.LinkEgrnBild != null && firstBuilding.LinkBtiFsks == null && secondBuilding.LinkBtiFsks != null && secondBuilding.LinkEgrnBild == null)
            {
                return new List<OMBuilding> { firstBuilding, secondBuilding };
            }
            else if (firstBuilding.LinkEgrnBild == null && firstBuilding.LinkBtiFsks != null && secondBuilding.LinkBtiFsks == null && secondBuilding.LinkEgrnBild != null)
            {
                return new List<OMBuilding> { secondBuilding, firstBuilding };
            }
            else
            {
                mes = "Не удалось выделить МКД, связанного с ЕГРН и МКД, связанного с БТИ";
                return new List<OMBuilding>();
            }
        }

        /// <summary>
        /// CIPJS-800:Ручная связка объектов
        /// </summary>
        /// <param name="buildingOneId"></param>
        /// <param name="buildingTwoId"></param>
        public void LinkTwoBuilding(long buildingOneId, long buildingTwoId)
        {
            OMBuilding firstBuilding = OMBuilding.Where(x => x.EmpId == buildingOneId).SelectAll().Execute().FirstOrDefault();
            OMBuilding secondBuilding = OMBuilding.Where(x => x.EmpId == buildingTwoId).SelectAll().Execute().FirstOrDefault();
            if (firstBuilding.LinkEgrnBild != null && firstBuilding.LinkBtiFsks == null && secondBuilding.LinkBtiFsks != null && secondBuilding.LinkEgrnBild == null)
            {
                LinkBuilding(firstBuilding, secondBuilding, false);
            }
            else if (firstBuilding.LinkEgrnBild == null && firstBuilding.LinkBtiFsks != null && secondBuilding.LinkBtiFsks == null && secondBuilding.LinkEgrnBild != null)
            {
                LinkBuilding(secondBuilding, firstBuilding, false);
            }
            else
            {
                throw new Exception("Не удалось выделить МКД, связанного с ЕГРН и МКД, связанного с БТИ");
            }
        }

        /// <summary>
        /// Связывание МКД, у котрого нет связки с БТИ и МКД, у которой нет связки с ЕГРН
        /// </summary>
        /// <param name="egrnBuildingId"></param>
        /// <param name="btiBuildingId"></param>
        public void LinkBuilding(long? egrnBuildingId, long? btiBuildingId, bool checkFlat)
        {
            OMBuilding fiasBuilding = OMBuilding.Where(x => x.EmpId == egrnBuildingId).SelectAll().Execute().FirstOrDefault();
            if (fiasBuilding == null)
            {
                throw new Exception("Не найдена запись по МКД, связанного с ЕГРН");
            }

            OMBuilding btiBuilding = OMBuilding.Where(x => x.EmpId == btiBuildingId).SelectAll().Execute().FirstOrDefault();
            if (btiBuilding == null)
            {
                throw new Exception("Не найдена запись по МКД, связанного с БТИ");
            }
            LinkBuilding(fiasBuilding, btiBuilding, checkFlat);
        }

        private void LinkBuilding(OMBuilding fiasBuilding, OMBuilding btiBuilding, bool checkFlat)
        {
            List<OMFlat> flatsFias = OMFlat.Where(x => x.LinkObjectMkd == fiasBuilding.EmpId && x.LinkBtiFlat == null).SelectAll().Execute().ToList();
            List<OMFlat> flatsBti = OMFlat.Where(x => x.LinkObjectMkd == btiBuilding.EmpId).SelectAll().Execute().ToList();

            #region Config
            OMBtiBuilding building = OMBtiBuilding.Where(x => x.EmpId == btiBuilding.LinkBtiFsks).SelectAll().Execute().FirstOrDefault();
            if (building == null)
            {
                throw new Exception("Не найдена информация по МКД в БТИ");
            }
            OMADDRESS address = OMADDRESS.Where(x => x.ADDRLINK[0].BuildingId == btiBuilding.LinkBtiFsks && x.ADDRLINK[0].AddressStatusName_Code == AddressStatus.Main).SelectAll().Execute().FirstOrDefault();
            if (address == null)
            {
                address = OMADDRESS.Where(x => x.ADDRLINK[0].BuildingId == btiBuilding.LinkBtiFsks).SelectAll().Execute().FirstOrDefault();
                if (address == null)
                {
                    throw new Exception("Не найден адрес по МКД в БТИ");
                }
            }
            OMBuildParcel buildParcel = OMBuildParcel.Where(x => x.EmpId == fiasBuilding.LinkEgrnBild).SelectAll().Execute().FirstOrDefault();
            if (buildParcel == null)
            {
                throw new Exception("Не найдена информация по МКД в ЕГРН");
            }
            OMLocation location = OMLocation.Where(x => x.BuildingParcelId == buildParcel.BuildingParcelId).SelectAll().Execute().FirstOrDefault();
            if (location == null)
            {
                throw new Exception("Не найден ehd.location по МКД в ЕГРН");
            }
            OMRegister register = OMRegister.Where(x => x.BuildingParcelId == buildParcel.BuildingParcelId).SelectAll().Execute().FirstOrDefault();
            OMOldNumber oldNumber = null;
            if (register != null)
            {
                oldNumber = register != null ?
                    OMOldNumber.Where(x => x.RegisterId == register.EmpId && x.Type == "Условный номер" && x.Number.RegexpLike("^[0-9]+$")).SelectAll().ExecuteFirstOrDefault() : null;
            }

            Dictionary<long, object> objects = new Dictionary<long, object>
            {
                { OMADDRESS.GetRegisterId(), address },
                { OMBtiBuilding.GetRegisterId(), building },

                { OMBuildParcel.GetRegisterId(), buildParcel },
                { OMLocation.GetRegisterId(), location },
                { OMRegister.GetRegisterId(), register },
                { OMOldNumber.GetRegisterId(), oldNumber }
            };

            var insurBuilding = BuildingService.Map<OMBuilding>("InsurObjectMapBuilding", OMBuilding.GetRegisterId(), objects, fiasBuilding.AttributeSource, out string mapLogAttribute);

            insurBuilding.LoadDate = DateTime.Now;

            insurBuilding.EmpId = fiasBuilding.EmpId;
            insurBuilding.LinkEgrnBild = fiasBuilding.LinkEgrnBild;
            insurBuilding.LinkBtiFsks = btiBuilding.LinkBtiFsks;

            insurBuilding.AttributeSource = mapLogAttribute;

            insurBuilding.FlagInsurCalculated = CalculateFlagInsur(fiasBuilding.LinkEgrnBild, building);
            insurBuilding.FlagInsur = insurBuilding.FlagInsurCalculated;



            List<OMLinkBuildBti> linkBuildBti = OMLinkBuildBti
                .Where(x => x.IdInsurBuild == btiBuilding.EmpId)
                .Select(x => x.IdBtiFsks)
                .Execute()
                .ToList();
            #endregion

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Suppress))
            {
                // связываем помещения
                LinkFlats(flatsFias, flatsBti, checkFlat, insurBuilding, btiBuilding);
                // потом сами МКД
                insurBuilding.Save();

                // Создание адреса через ХП, если источник адреса БТИ или ЕГРН.
                if (insurBuilding?.AddressId != null)
                {
                    var adress = OMAddress.Where(x => x.EmpId == insurBuilding.AddressId).Select(x => x.SourceAddress_Code).ExecuteFirstOrDefault();
                    if (adress?.SourceAddress_Code != null && (adress?.SourceAddress_Code == AddressSource.Bti || adress?.SourceAddress_Code == AddressSource.Egrn))
                    {
                        DbCommand dbCommand = DBMngr.Realty.GetSqlStringCommand($"select fias_fill_insur_address({insurBuilding.EmpId}, 0)");
                        DBMngr.Realty.ExecuteNonQuery(dbCommand);
                    }
                }
                // Редактируем записи в import_log_insur_building
                var fiaslog = OMInsurBuildingLog.Where(x => x.InsurBuildingId == insurBuilding.EmpId).SelectAll().ExecuteFirstOrDefault();
                if (fiaslog != null)
                {
                    FillInsurBuildingLog(fiaslog, insurBuilding);
                    var btilog = OMInsurBuildingLog.Where(x => x.InsurBuildingId == btiBuilding.EmpId).SelectAll().ExecuteFirstOrDefault();
                    if (btilog != null)
                    {
                        btilog.Destroy();
                    }
                }

                CopyFlatFlagInsurFromBuilding(insurBuilding.EmpId, insurBuilding.FlagInsurCalculated);

                Console.WriteLine($"Сохранен объект: {insurBuilding.EmpId}, связи с БТИ: {linkBuildBti.Count}");

                List<OMLinkBuildBti> existsLinksBti = OMLinkBuildBti.Where(x => x.IdInsurBuild == insurBuilding.EmpId).Select(x => x.IdBtiFsks).Execute();

                // Сохраняем новые, обновляем старые
                foreach (OMLinkBuildBti linkBti in linkBuildBti)
                {
                    OMLinkBuildBti existsLinkBti = existsLinksBti.FirstOrDefault(x => x.IdBtiFsks == linkBti.IdBtiFsks);

                    if (existsLinkBti != null)
                    {
                        linkBti.EmpId = existsLinkBti.EmpId;
                    }

                    linkBti.IdInsurBuild = insurBuilding.EmpId;
                    linkBti.Save();
                }

                btiBuilding.FlagInsur = false;

                List<OMAllProperty> allPropertyies = OMAllProperty.Where(x => x.ObjId == btiBuilding.EmpId).SelectAll(false).Execute().ToList();
                if (allPropertyies != null)
                {
                    foreach (OMAllProperty allProperty in allPropertyies)
                    {
                        allProperty.ObjId = fiasBuilding.EmpId;
                        allProperty.Save();
                    }
                }

                List<OMDamage> damages = OMDamage.Where(x => x.ObjId == btiBuilding.EmpId && x.ObjReestrId == OMBuilding.GetRegisterId()).SelectAll(false).Execute().ToList();
                if (damages != null)
                {
                    foreach (OMDamage damage in damages)
                    {
                        damage.ObjId = fiasBuilding.EmpId;
                        damage.Save();
                    }
                }

                new OMChangesLog
                {
                    ObjectId = fiasBuilding.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMBuilding.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.LinkBuildings,
                    Reason = "Связка объектов МКД привязанных к ЕГРН и БТИ",
                    OldValue = null,
                    NewValue = btiBuilding.LinkBtiFsks.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();

                btiBuilding.DestroyLogically();

                ts.Complete();
            }
        }

        private void LinkFlats(List<OMFlat> flatsFias, List<OMFlat> flatsBti, bool checkFlat, OMBuilding insurBuilding, OMBuilding btiBuilding)
        {
            FlatService service = new FlatService();
            for (int i = 0; i < flatsFias.Count; ++i)
            {
                List<OMFlat> btiFlatSelect = flatsBti.Where(x => (x.CadastrNum == flatsFias[i].CadastrNum && flatsFias[i].CadastrNum != "") || (flatsFias[i].CadastrNum == "" && x.CadastrNum == ""))
                        .ToList();
                if (btiFlatSelect.Count == 1)
                {
                    service.LinkFlat(flatsFias[i].EmpId, btiFlatSelect.First().EmpId, insurBuilding);
                    flatsFias.RemoveAt(i--);
                    flatsBti.Remove(btiFlatSelect.First());
                }
                else if (btiFlatSelect.Count > 1)
                {
                    btiFlatSelect = btiFlatSelect.Where(x => (x.Kvnom == flatsFias[i].Kvnom && flatsFias[i].Kvnom != "") || (flatsFias[i].Kvnom == "" && x.Kvnom == "")).ToList();
                    if (btiFlatSelect.Count == 1)
                    {
                        service.LinkFlat(flatsFias[i].EmpId, btiFlatSelect.First().EmpId, insurBuilding);
                        flatsFias.RemoveAt(i--);
                        flatsBti.Remove(btiFlatSelect.First());
                    }
                }
                else
                {
                    btiFlatSelect = flatsBti.Where(x => ((x.Kvnom == flatsFias[i].Kvnom && flatsFias[i].Kvnom != "") || (flatsFias[i].Kvnom == "" && x.Kvnom == ""))).ToList();
                    if (btiFlatSelect.Count == 1)
                    {
                        service.LinkFlat(flatsFias[i].EmpId, btiFlatSelect.First().EmpId, insurBuilding);
                        flatsFias.RemoveAt(i--);
                        flatsBti.Remove(btiFlatSelect.First());
                    }
                }
            }
            if (checkFlat)
            {
                if (flatsFias.Count > 0)
                {
                    throw new Exception("У МКД, связанного с ЕГРН имеются не связанные ЖП");
                }
                if (flatsBti.Count > 0)
                {
                    throw new Exception("У МКД, связанного с БТИ имеются не связанные ЖП");
                }
            }
            else
            {
                // если остались не связанные ЖП по БТИ и пользователь знает, что для них нет аналога ЕГРН, то просто меняем их привязку МКд на МКд, связанный с ЕГРН
                if (flatsBti.Count > 0)
                {
                    foreach (var flatBti in flatsBti)
                    {
                        flatBti.LinkObjectMkd = insurBuilding.EmpId;
                        flatBti.FlagInsur = insurBuilding.FlagInsur;
                        flatBti.Save();
                        // В import_log_insur_flat делать запись о изменении ссылки в insur_building_id на новый МКД
                        _importLogInsurFlatService.FillInsurFlatBuildingLog(flatBti);
                    }
                }
                if (flatsFias.Count > 0)
                {
                    foreach (var flatFias in flatsFias)
                    {
                        flatFias.Unom = btiBuilding.Unom;
                        flatFias.FlagInsur = insurBuilding.FlagInsur;
                        flatFias.Save();
                        // В import_log_insur_flat делать запись о изменении ссылки в insur_building_id на новый МКД
                        _importLogInsurFlatService.FillInsurFlatBuildingLog(flatFias);
                    }
                }
            }
        }

        private void UpdateAddress(Dictionary<string, SourceInput> sources, OMAddress omAddress, OMAddress currentOMAddress = null)
        {
            if (currentOMAddress == null)
            {
                if (omAddress.FullAddress.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.FullAddress).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.Region.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.Region).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.City.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.City).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.TypeHouse.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.TypeHouse).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.Locality.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.Locality).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.Street.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.Street).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.House.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.House).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.Corpus.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.Corpus).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.Structure.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.Structure).ValueField] = SourceInput.ManualInput;
                }
                if (omAddress.ShortAddress.IsNotEmpty())
                {
                    sources[OMAddress.GetAttributeData(x => x.ShortAddress).ValueField] = SourceInput.ManualInput;
                }

                omAddress.EmpId = -1;
                omAddress.SourceAddress_Code = AddressSource.Manual;
                omAddress.Save();
            }
            else
            {
                if ((currentOMAddress.Street != omAddress.Street && !(currentOMAddress.Street == null && omAddress.Street == "")) || currentOMAddress.House != omAddress.House
                    || currentOMAddress.Corpus != omAddress.Corpus || currentOMAddress.Structure != omAddress.Structure)
                {
                    currentOMAddress.ShortAddress = omAddress.ShortAddress;
                    sources[OMAddress.GetAttributeData(x => x.ShortAddress).ValueField] = SourceInput.ManualInput;
                }

                if (currentOMAddress.FullAddress != omAddress.FullAddress)
                {
                    currentOMAddress.FullAddress = omAddress.FullAddress;
                    sources[OMAddress.GetAttributeData(x => x.FullAddress).ValueField] = SourceInput.ManualInput;
                }
                if (currentOMAddress.Region != null && currentOMAddress.Region.TrimStart() != omAddress.Region)
                {
                    currentOMAddress.TypeRegion = omAddress.TypeRegion;
                    currentOMAddress.Region = omAddress.Region;
                    sources[OMAddress.GetAttributeData(x => x.Region).ValueField] = SourceInput.ManualInput;
                }
                if (currentOMAddress.City != omAddress.City)
                {
                    currentOMAddress.City = omAddress.City;
                    sources[OMAddress.GetAttributeData(x => x.City).ValueField] = SourceInput.ManualInput;
                }
                if (currentOMAddress.Locality != omAddress.Locality)
                {
                    currentOMAddress.Locality = omAddress.Locality;
                    sources[OMAddress.GetAttributeData(x => x.Locality).ValueField] = SourceInput.ManualInput;
                }
                if (currentOMAddress.Street != omAddress.Street && !(currentOMAddress.Street == null && omAddress.Street == "" ))
                {
                    currentOMAddress.TypeStreet = omAddress.TypeStreet;
                    currentOMAddress.Street = omAddress.Street;
                    sources[OMAddress.GetAttributeData(x => x.Street).ValueField] = SourceInput.ManualInput;
                }
                if (currentOMAddress.TypeHouse != omAddress.TypeHouse)
                {
                    currentOMAddress.TypeHouse = omAddress.TypeHouse;
                    sources[OMAddress.GetAttributeData(x => x.TypeHouse).ValueField] = SourceInput.ManualInput;
                }
                if (currentOMAddress.House != omAddress.House)
                {
                    currentOMAddress.House = omAddress.House;
                    sources[OMAddress.GetAttributeData(x => x.House).ValueField] = SourceInput.ManualInput;
                }
                if (currentOMAddress.Corpus != omAddress.Corpus)
                {
                    currentOMAddress.Corpus = omAddress.Corpus;
                    sources[OMAddress.GetAttributeData(x => x.Corpus).ValueField] = SourceInput.ManualInput;
                }
                if (currentOMAddress.Structure != omAddress.Structure)
                {
                    currentOMAddress.Structure = omAddress.Structure;
                    sources[OMAddress.GetAttributeData(x => x.Structure).ValueField] = SourceInput.ManualInput;
                }

                if (currentOMAddress.PropertyChangedList.Count > 0)
                {
                    currentOMAddress.SourceAddress_Code = AddressSource.Manual;
                    currentOMAddress.Save();
                }
            }
        }

        #region UnLinkMKD

        /// <summary>
        /// Отсоединение МКД и ЖП.
        /// CIPJS-983: Реализовать процедуру отсоединения МКД и ЖП.
        /// </summary>
        /// <param name="empId"></param>
        public void UnLinkMkd(long? empId)
        {
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Required))
            {
                // Логика МКД.
                OMBuilding actualMkd = OMBuilding.Where(x => x.EmpId == empId && x.SpecialActual == 1 && x.LinkBtiFsks != null && x.LinkEgrnBild != null).SelectAll().ExecuteFirstOrDefault();
                if (actualMkd == null)
                    throw new Exception($"Не найден актуальынй МКД {empId}, связанный с БТИ и ЕГРН");

                // БТИ
                var newBuildBti = CreateBtiBuildWithUnLinkMkd(actualMkd);

                // ЕГРН
                // 4.Для  строки актуального МКД, связанного с БТИ и ЕГРН, проставить insur_building.actual = 0;
                var newBuildEgrn = CreateEgrnBuildWithUnLinkMkd(actualMkd);

                // п.11 
                RemoveLinkBuildBti(actualMkd);

                // Логика реестра ЖП.
                // 1. Для  строки актуального ЖП, связанного с БТИ и ЕГРН (либо только с БТИ, либо только с ЕГРН), проставить insur_flat.actual = 0;
                var actualFlats = OMFlat.Where(x => x.LinkObjectMkd == actualMkd.EmpId && x.SpecialActual == 1).SelectAll().Execute();


                //Настройка параллельной обработки;
                ParallelOptions options = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 10
                };
                // Параллельная обработка квартир.
                Parallel.ForEach(actualFlats, options, actualFlat =>
                {
                    // 8. Удалить связь помещения с ФСП.
                    RemoveLinkFlatWithFsp(actualFlat);

                    if (actualFlat.LinkBtiFlat != null && actualFlat.LinkInsurEgrn != null)
                    {
                        CreateBtiFlat(actualFlat, actualMkd, newBuildBti);
                        CreateEgrnFlat(actualFlat, actualMkd, newBuildEgrn);

                    }
                    else if (actualFlat.LinkBtiFlat != null && actualFlat.LinkInsurEgrn == null)
                    {
                        CreateBtiFlat(actualFlat, actualMkd, newBuildBti);
                        actualFlat.DestroyLogically();
                    }
                    else if (actualFlat.LinkBtiFlat == null && actualFlat.LinkInsurEgrn != null)
                    {
                        CreateEgrnFlat(actualFlat, actualMkd, newBuildEgrn);
                    }
                });

                ts.Complete();
            }


        }

        /// <summary>
        /// Создаем ЖП, связанное с БТИ.
        /// </summary>
        private void CreateBtiFlat(OMFlat actualFlat, OMBuilding actualBuild, OMBuilding btiBuild)
        {
            OMPremase premise = OMPremase.Where(x => x.EmpId == actualFlat.LinkBtiFlat).SelectAll().Execute().FirstOrDefault();
            if (premise == null)
            {
                throw new Exception("Не найдена информация по ЖП в БТИ");
            }
            OMFloor floor = OMFloor.Where(x => x.EmpId == premise.FloorId).SelectAll().Execute().FirstOrDefault();
            if (floor == null)
            {
                throw new Exception("Не найдена информация по связи по ЖП в БТИ");
            }

            Dictionary<long, Object> objects = new Dictionary<long, object>
            {
                { OMBuilding.GetRegisterId(), btiBuild },
                { OMPremase.GetRegisterId(), premise },
                { OMFloor.GetRegisterId(), floor },
                { OMBuildParcel.GetRegisterId(), null },
                { OMRegister.GetRegisterId(), null },
                { OMEgrp.GetRegisterId(), null },
                { OMLocation.GetRegisterId(), null },
            };

            OMFlat btiFlat = BuildingService.Map<OMFlat>("InsurObjectMapFlat", OMFlat.GetRegisterId(), objects, null, out string mapLogAttribute);
            btiFlat.LinkBtiFlat = actualFlat.LinkBtiFlat;
            btiFlat.GuidFiasMkd = btiBuild.GuidFiasMkd;
            btiFlat.FlagInsur = btiBuild.FlagInsur;
            btiFlat.LinkObjectMkd = btiBuild.EmpId;
            btiFlat.LoadDate = DateTime.Now;
            btiFlat.AttributeSource = mapLogAttribute;
            // Дополнительная обработка
            if (btiFlat.PropertyChangedList.Contains("Kvnom"))
            {
                btiFlat.Kvnom = actualFlat.Kvnom.Replace("Квартира", "").Trim();
            }

            btiFlat.Save();

            // 6. Для помещения из БТИ создать строку в import_log_insur_flat;
            _importLogInsurFlatService.FillInsurFlatBuildingLog(btiFlat);

            // 9. Если с помещением было связано  дело об ущербе, то в insur_damage.obj_id  заменить  на insur_flat.emp_id нового объекта, связанного с БТИ; 
            var flatDamages = OMDamage.Where(x => x.ObjId == actualFlat.EmpId && x.SpecialActual == 1).Select(x => x.ObjId).Execute();
            foreach (var flatDamage in flatDamages)
            {
                flatDamage.ObjId = btiFlat.EmpId;
                flatDamage.Save();
            }

        }

        /// <summary>
        /// Создаем ЖП, связанное с ЕГРН.
        /// </summary>
        private void CreateEgrnFlat(OMFlat actualFlat, OMBuilding actualBuild, OMBuilding egrnBuild)
        {
            OMBuildParcel buildParcel = OMBuildParcel.Where(x => x.EmpId == actualFlat.LinkInsurEgrn).SelectAll().Execute().FirstOrDefault();
            OMRegister register = OMRegister.Where(x => x.BuildingParcelId == buildParcel.EmpId).SelectAll().Execute().FirstOrDefault();
            OMEgrp egrp = OMEgrp.Where(x => x.NumCadnum == buildParcel.ObjectId).SelectAll().Execute().FirstOrDefault();
            OMLocation locatuon = OMLocation.Where(x => x.BuildingParcelId == actualFlat.LinkInsurEgrn).SelectAll().Execute().FirstOrDefault();
            Dictionary<long, Object> objects = new Dictionary<long, object>
            {
                { OMBuilding.GetRegisterId(), null },
                { OMPremase.GetRegisterId(), null },
                { OMFloor.GetRegisterId(), null },
                { OMBuildParcel.GetRegisterId(), buildParcel },
                { OMRegister.GetRegisterId(), register },
                { OMEgrp.GetRegisterId(), egrp },
                { OMLocation.GetRegisterId(), locatuon },
            };

            var egrnFlat = BuildingService.Map<OMFlat>("InsurObjectMapFlat", OMFlat.GetRegisterId(), objects, actualFlat.AttributeSource, out string mapLogAttribute);
            egrnFlat.EmpId = actualFlat.EmpId;
            egrnFlat.LinkInsurEgrn = actualFlat.LinkInsurEgrn;
            egrnFlat.GuidFiasMkd = egrnBuild.GuidFiasMkd;
            egrnFlat.FlagInsur = egrnBuild.FlagInsur;
            egrnFlat.LinkObjectMkd = egrnBuild.EmpId;
            egrnFlat.LoadDate = DateTime.Now;
            egrnFlat.AttributeSource = mapLogAttribute;
            // Дополнительная обработка
            if (egrnFlat.PropertyChangedList.Contains("Kvnom"))
            {
                egrnFlat.Kvnom = egrnFlat.Kvnom.Replace("Квартира", "").Trim();
            }
            // Инициализация
            egrnFlat.LinkBtiFlat = egrnFlat.LinkBtiFlat ?? null;
            egrnFlat.CadastrNum = egrnFlat.CadastrNum ?? null;
            egrnFlat.Unom = egrnFlat.Unom ?? null;
            egrnFlat.KolGp = egrnFlat.KolGp ?? null;
            egrnFlat.Fopl = egrnFlat.Fopl ?? null;
            egrnFlat.Ppl = egrnFlat.Ppl ?? null;
            egrnFlat.Gpl = egrnFlat.Gpl ?? null;
            egrnFlat.KlassFlat = egrnFlat.KlassFlat ?? null;
            egrnFlat.KlassFlat_Code = egrnFlat.KlassFlat_Code;
            egrnFlat.TypeFlat = egrnFlat.TypeFlat ?? null;
            egrnFlat.TypeFlat_Code = egrnFlat.TypeFlat_Code;

            egrnFlat.Save();

            // 7. Для помещения из ЕГРН обновить строку в import_log_insur_flat;
            _importLogInsurFlatService.FillInsurFlatBuildingLog(egrnFlat);
        }

        /// <summary>
        /// Создаем здание связанное с ЕГРН.
        /// </summary>
        /// <param name="actualMkd"></param>
        private OMBuilding CreateEgrnBuildWithUnLinkMkd(OMBuilding actualMkd)
        {
            // Поиск данных для ЕГРН.
            OMBuildParcel buildParcel = OMBuildParcel.Where(x => x.EmpId == actualMkd.LinkEgrnBild).SelectAll().Execute().FirstOrDefault();
            if (buildParcel == null)
            {
                throw new Exception("Не найдена информация по МКД в ЕГРН");
            }
            OMLocation location = OMLocation.Where(x => x.BuildingParcelId == buildParcel.BuildingParcelId).SelectAll().Execute().FirstOrDefault();
            if (location == null)
            {
                throw new Exception("Не найден ehd.location по МКД в ЕГРН");
            }
            OMRegister register = OMRegister.Where(x => x.BuildingParcelId == buildParcel.BuildingParcelId).SelectAll().Execute().FirstOrDefault();
            OMOldNumber oldNumber = null;
            if (register != null)
            {
                oldNumber = register != null ?
                    OMOldNumber.Where(x => x.RegisterId == register.EmpId && x.Type == "Условный номер" && x.Number.RegexpLike("^[0-9]+$")).SelectAll().ExecuteFirstOrDefault() : null;
            }

            Dictionary<long, object> objects = new Dictionary<long, object>
            {
                { OMADDRESS.GetRegisterId(), null },
                { OMBtiBuilding.GetRegisterId(), null },
                { OMBuildParcel.GetRegisterId(), buildParcel },
                { OMLocation.GetRegisterId(), location },
                { OMRegister.GetRegisterId(), register },
                { OMOldNumber.GetRegisterId(), oldNumber }
            };

           

            // 5-6-7. Инициализация данных
            OMBuilding build = BuildingService.Map<OMBuilding>("InsurObjectMapBuilding", OMBuilding.GetRegisterId(), objects, null, out string mapLogAttribute);
            build.EmpId = actualMkd.EmpId;
            build.LinkEgrnBild = actualMkd.LinkEgrnBild;
            build.LoadDate = DateTime.Now;
            build.FlagInsurCalculated = false;
            build.FlagInsur = false;
            build.AttributeSource = mapLogAttribute;
            build.AddressId = actualMkd.AddressId;
            build.CodeKladr = actualMkd.CodeKladr;

            // Инициализация для обновления.
            build.LinkBtiFsks = null;
            build.OkrugId = build.OkrugId ?? null;
            build.DistrictId = build.DistrictId ?? null;
            build.Unom = null; //build.Unom ?? null;
            build.YearStroi = build.YearStroi ?? null;
            build.CountFloor = build.CountFloor ?? null;
            build.KolGp = build.KolGp ?? null;
            build.OplG = build.OplG ?? null;
            build.Bpl = build.Bpl ?? null;
            build.Hpl = build.Hpl ?? null;
            build.Lpl = build.Lpl ?? null;
            build.Lfpq = build.Lfpq ?? null;
            build.Lfgpq = build.Lfgpq ?? null;
            build.Lfgq = build.Lfgq ?? null;
            build.GuidFiasMkd = build.GuidFiasMkd ?? null;
            build.Krovpl = build.Krovpl ?? null;
            build.PurposeName = build.PurposeName ?? null;
            build.PurposeName_Code = build.PurposeName_Code;
            build.StatusSostBti = build.StatusSostBti ?? null;
            build.StatusSostBti_Code = build.StatusSostBti_Code;

            build.Save();

            // Создание адреса через ХП, если источник адреса БТИ.
            if (build?.AddressId != null)
            {
                var adress = OMAddress.Where(x => x.EmpId == build.AddressId).Select(x => x.SourceAddress_Code).ExecuteFirstOrDefault();
                if (adress?.SourceAddress_Code != null && adress?.SourceAddress_Code == AddressSource.Bti)
                {
                    DbCommand dbCommand = DBMngr.Realty.GetSqlStringCommand($"select fias_fill_insur_address({build.EmpId}, 0)");
                    DBMngr.Realty.ExecuteNonQuery(dbCommand);
                }
            }

            // 9. Для здания из ЕГРН обновитьть строку в import_log_insur_building.
            OMInsurBuildingLog log = OMInsurBuildingLog.Where(x => x.InsurBuildingId == build.EmpId && x.SpecialActual == 1).SelectAll().ExecuteFirstOrDefault() ?? new OMInsurBuildingLog();
            FillInsurBuildingLog(log, build);

            return build;

        }

        /// <summary>
        /// Создаем здание связанное с БТИ.
        /// </summary>
        /// <param name="actualMkd"></param>
        private OMBuilding CreateBtiBuildWithUnLinkMkd(OMBuilding actualMkd)
        {
            // Поиск данных для БТИ.
            OMBtiBuilding btiBuild = OMBtiBuilding.Where(x => x.EmpId == actualMkd.LinkBtiFsks).SelectAll().Execute().FirstOrDefault();
            if (btiBuild == null)
            {
                throw new Exception("Не найдена информация по МКД в БТИ");
            }
            OMADDRESS address = OMADDRESS.Where(x => x.ADDRLINK[0].BuildingId == actualMkd.LinkBtiFsks && x.ADDRLINK[0].AddressStatusName_Code == AddressStatus.Main).SelectAll().Execute().FirstOrDefault();
            if (address == null)
            {
                address = OMADDRESS.Where(x => x.ADDRLINK[0].BuildingId == actualMkd.LinkBtiFsks).SelectAll().Execute().FirstOrDefault();
                if (address == null)
                {
                    throw new Exception("Не найден адрес по МКД в БТИ");
                }
            }

            Dictionary<long, object> objects = new Dictionary<long, object>
            {
                { OMADDRESS.GetRegisterId(), address },
                { OMBtiBuilding.GetRegisterId(), btiBuild },
                { OMBuildParcel.GetRegisterId(), null },
                { OMLocation.GetRegisterId(), null },
                { OMRegister.GetRegisterId(), null },
                { OMOldNumber.GetRegisterId(), null }
            };

            // 6-7 Инициализация данных.
            OMBuilding build = BuildingService.Map<OMBuilding>("InsurObjectMapBuilding", OMBuilding.GetRegisterId(), objects, null, out string mapLogAttribute);
            build.LinkBtiFsks = actualMkd.LinkBtiFsks;
            build.LoadDate = DateTime.Now;
            // 10.	Запустить процедуру «Пересчитать признак «Подлежит страхованию» для выделенных записей» и процедуру «Копирование признака «Подлежит страхованию» (для зданий и квартир);
            string message = "";
            build.FlagInsurCalculated = CalculateFlagInsur(build.LinkEgrnBild, btiBuild, out message);
            build.FlagInsur = build.FlagInsurCalculated;
            /* 1042 - 1. в insur_building_q для МКД, связанного с БТИ, заполнять атрибут source_atrib; */
            build.AttributeSource = mapLogAttribute;

            DateTime fromDate = DateTime.Now;
            build.Save(fromDate: fromDate);

            // Создание адреса через ХП
            DbCommand dbCommand = DBMngr.Realty.GetSqlStringCommand($"select fias_fill_insur_address({build.EmpId}, 1)");
            var v_addressId = DBMngr.Realty.ExecuteScalar(dbCommand)?.ParseToLongNullable();
            if (v_addressId != null)
            {
                build.AddressId = v_addressId;
                build.Save(fromDate: fromDate);
            }


            // 8. Для здания из БТИ создать строку в import_log_insur_building.
            OMInsurBuildingLog log = new OMInsurBuildingLog();
            FillInsurBuildingLog(log, build);

            /* 1042 - 3.  в insur_link_build_bti для вновь созданного МКД, связанного с БТИ, создавать строку связи с  объектом БТИ */
            OMLinkBuildBti oLinkBuildBti = new OMLinkBuildBti();
            {
                oLinkBuildBti.IdBtiFsks = build.LinkBtiFsks;
                oLinkBuildBti.IdInsurBuild = build.EmpId;
                oLinkBuildBti.FlagDublUnom = false;
            }
            oLinkBuildBti.Save();

            // 12. Если для здания существует договор, то в insur_all_property.obj_id  заменить  на insur_building.emp_id нового объекта, связанного с БТИ;
            var allProperties = OMAllProperty.Where(x => x.ObjId == actualMkd.EmpId && x.SpecialActual == 1).Select(x => x.ObjId).Execute();
            foreach (var allProperty in allProperties)
            {
                allProperty.ObjId = build.EmpId;
                allProperty.Save();
            }
            // 13.	Если со зданием был связан расчет параметров объектов общего имущества,  то в insur_param_calculation.obj_id  заменить  на insur_building.emp_id нового объекта, связанного с БТИ;
            var oMParamCalculations = OMParamCalculation.Where(x => x.ObjId == actualMkd.EmpId && x.SpecialActual == 1).Select(x => x.ObjId).Execute();
            foreach (var oMParamCalculation in oMParamCalculations)
            {
                oMParamCalculation.ObjId = build.EmpId;
                oMParamCalculation.Save();
            }
            // 14.	Если со зданием было связано  дело об ущербе, то в insur_damage.obj_id  заменить  на insur_building.emp_id нового объекта, связанного с БТИ; 
            var omDamages = OMDamage.Where(x => x.ObjId == actualMkd.EmpId && x.SpecialActual == 1).Select(x => x.ObjId).Execute();
            foreach (var omDamage in omDamages)
            {
                omDamage.ObjId = build.EmpId;
                omDamage.Save();
            }

            // 15. Если с одним зданием ЕГРН было связано более одного здания БТИ, то в Insur_svod_data_calculated (реестр сводных показателей) найти строки, 
            // в которых link_mkd = emp_id «старого» связанного здания и link_bti и заменить в них ссылки со старого МКД на новые МКД, связанные с БТИ 
            var exist = OMLinkBuildBti.Where(x => x.IdInsurBuild == actualMkd.EmpId && x.IdBtiFsks != actualMkd.LinkBtiFsks && x.SpecialActual == 1).ExecuteExists();
            if (exist)
            {
                var svodDatas = OMBuildingSvodDataCalculated.Where(x => x.LinkMkd == actualMkd.EmpId && x.LinkBti == actualMkd.LinkBtiFsks && x.SpecialActual == 1).Select(x => x.LinkMkd).Execute();
                foreach (var svodData in svodDatas)
                {
                    svodData.LinkMkd = build.EmpId;
                    svodData.Save();
                }
            }
            return build;

        }

        /// <summary>
        /// Заполнение полей import_log_insur_building при отвязке МКД.
        /// </summary>
        /// <param name="log"></param>
        /// <param name="build"></param>
        private void FillInsurBuildingLog(OMInsurBuildingLog log, OMBuilding build)
        {
            if (log == null || build == null)
            {
                return;
            }

            if (log.EhdParcelId != build.LinkEgrnBild)
            {
                log.UpdateDateEhd = DateTime.Now;
            }
            log.EhdParcelId = build.LinkEgrnBild;

            if (log.BtiBuildingId != build.LinkBtiFsks)
            {
                log.UpdateDateBti = DateTime.Now;
            }
            log.BtiBuildingId = build.LinkBtiFsks;
            log.InsurBuildingId = build.EmpId;
            log.DateLoaded = DateTime.Now;
            log.Unom = build.Unom;
            log.CadNum = build.CadasrNum;
            log.Save();
        }


        /// <summary>
        /// 11.	Если с одним зданием ЕГРН было связано более одного здания БТИ, то в Insur_link_build_bti (реестр связей) удалить строки, в которых id_insur_build = emp_id «старого» связанного здания;
        /// </summary>
        private void RemoveLinkBuildBti(OMBuilding actualMkd)
        {
            var exist = OMLinkBuildBti.Where(x => x.IdInsurBuild == actualMkd.EmpId && x.IdBtiFsks != actualMkd.LinkBtiFsks && x.SpecialActual == 1).ExecuteExists();
            if (exist)
            {
                var links = OMLinkBuildBti.Where(x => x.IdInsurBuild == actualMkd.EmpId && x.SpecialActual == 1).SelectAll().Execute();
                foreach (var link in links)
                {
                    /* 1042 - 4.   в insur_link_build_bti для  МКД, связанного с ЕГРН, удалять строку  строку связи с  объектом БТИ */
                    link.Destroy();
                }
            }
        }

        /// <summary>
        /// Удаляем связь помещения с ФСП.
        /// </summary>
        /// <param name="flat"></param>
        private void RemoveLinkFlatWithFsp(OMFlat flat)
        {
            var fsps = OMFsp.Where(x => x.ObjId == flat.EmpId && x.SpecialActual == 1)
                .Select(x => x.ObjId)
                .Select(x => x.ObjReestrId)
                .Execute();
            // Обрабатываем fsp квартиры.
            foreach (var fsp in fsps)
            {
                // присвоить insur_fsp.obj_id is null and insur_fsp.obj_reestr_id is null, если obj_reestr_id = 317.
                if (fsp.ObjReestrId == 317)
                {
                    fsp.ObjId = null;
                    fsp.ObjReestrId = null;
                    fsp.Save();
                }
                // присвоить insur_fsp.obj_id is null and insur_fsp.obj_reestr_id is null и в insur_link_fsp_flat удалить строчку, 
                // где insur_fsp.emp_id =  insur_link_fsp_flat.fsp_id, если obj_reestr_id = 383.
                else if (fsp.ObjReestrId == 383)
                {
                    var linksFspFlat = OMLinkFspFlat.Where(x => x.FspId == fsp.EmpId && x.SpecialActual == 1).Select(x => x.FspId).Execute();
                    foreach (var linkFspFlat in linksFspFlat)
                    {
                        linkFspFlat.Destroy();
                    }
                    fsp.ObjId = null;
                    fsp.ObjReestrId = null;
                    fsp.Save();
                }
            }
        }

        #endregion

    }
}