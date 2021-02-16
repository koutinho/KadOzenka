using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.Models.GbuObject;
using KadOzenka.Web.Models.GbuObject;
using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.GbuObject.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Core.Register;
using ObjectModel.KO;
using Core.Register.RegisterEntities;
using KadOzenka.Dal.CancellationQueryManager;
using ObjectModel.Directory;
using Platform.Register;
using Serilog;
using SerilogTimings.Extensions;

namespace KadOzenka.Dal.GbuObject
{
	public class GbuObjectService : IGbuObjectService
	{
        private static readonly ILogger _log = Log.ForContext<GbuObjectService>();
		public static List<string> Postfixes = new List<string> { "TXT", "NUM", "DT" };

		private QueryManager _queryManager;

		public GbuObjectService(){}

		public GbuObjectService(QueryManager queryManager)
		{
			_queryManager = queryManager;
		}

		public static GbuObjectAttribute CheckExistsValueFromAttributeIdPartition(long objectId, long attributeId, DateTime otDate)
		{
			var attributeData = RegisterCache.GetAttributeData(attributeId);

			var registerData = RegisterCache.GetRegisterData(attributeData.RegisterId);

			if (registerData.AllpriPartitioning != Platform.Register.AllpriPartitioningType.AttributeId) return null;

			var sql = $@"select A.change_date as ChangeDate, A.change_doc_id as ChangeDocId from {registerData.AllpriTable}_{attributeData.Id} A where A.object_Id = {objectId} and A.Ot = {CrossDBSQL.ToDate(otDate)}";
            
            _log.ForContext("SQL", sql)
                .ForContext("OtDate", CrossDBSQL.ToDate(otDate))
                .Verbose("Проверка наличия значения по объекту {ObjectId} в {AllpriTable}_{AttributeDataId}", objectId, registerData.AllpriTable, attributeData.Id);
			
            return QSQuery.ExecuteSql<GbuObjectAttribute>(sql).FirstOrDefault();
		}

		public static DateTime GetNextOtFromAttributeIdPartition(long objectId, long attributeId, DateTime otDate)
		{
			while(true)
			{
				otDate = otDate.AddSeconds(1);

				if (CheckExistsValueFromAttributeIdPartition(objectId, attributeId, otDate) == null) return otDate; 
			}
		}

        public List<GbuObjectAttribute> GetAllAttributes(long objectId, List<long> sources = null,
            List<long> attributes = null, DateTime? dateS = null, DateTime? dateOt = null, bool isLight = false)
        {
            return GetAllAttributes(new List<long> {objectId}, sources, attributes, dateS, dateOt, isLight);
        }

        public List<GbuObjectAttribute> GetAllAttributes(List<long> objectIds, List<long> sources = null, List<long> inputAttributes = null, DateTime? dateS = null, DateTime? dateOt = null, bool isLight = false)
		{
			using (_log.TimeOperation("Получение ГБУ атрибутов"))
			{
				if (objectIds == null || objectIds.Count == 0) return new List<GbuObjectAttribute>();

				var uniqueObjectIds = objectIds.Distinct().ToList();
				var attributes = inputAttributes?.Distinct().ToList();
				if (sources == null)
				{
					List<int> registerIds = attributes != null && attributes.Count > 0
						? RegisterCache.RegisterAttributes.Values
							.Where(x => attributes.Contains(x.Id))
							.Select(x => x.RegisterId)
							.ToList()
						: null;

					var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());

					sources = RegisterCache.Registers.Values
						.Where(x => x.QuantTable == mainRegister.QuantTable && x.Id != mainRegister.Id &&
						            (registerIds == null || registerIds.Contains(x.Id)))
						.Select(x => (long) x.Id)
						.ToList();
				}

				var uniqueSources = sources.Distinct().ToList();
				var result = new List<GbuObjectAttribute>();
				foreach (var registerId in uniqueSources)
				{
					var registerData = RegisterCache.GetRegisterData((int) registerId);

					if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.DataType)
					{
						var postfixes = new List<string> {"TXT", "NUM", "DT"};

						foreach (var postfix in postfixes)
						{
							var propName = "StringValue";

							if (postfix == "NUM") propName = "NumValue";
							else if (postfix == "DT") propName = "DtValue";

							var sql = GetSqlForRegisterWithDataPartitioning(uniqueObjectIds, postfix, propName,
								registerData, isLight);

							if (attributes != null && attributes.Count > 0)
								sql = $"{sql} and a.attribute_id in ({String.Join(",", attributes)})";

							if (dateS != null || dateOt != null)
							{
								string dateSFilter = dateS == null
									? String.Empty
									: $"AND [A].s <= {CrossDBSQL.ToDate(dateS.Value)}";
								string dateOtFilter = dateOt == null
									? String.Empty
									: $"AND A3.Ot <= {CrossDBSQL.ToDate(dateOt.Value)}";

								sql =
									$"{sql} and A.ID = (SELECT MAX(a2.id) FROM {registerData.AllpriTable}_{postfix} a2 WHERE a2.object_id = a.object_id AND a2.attribute_id = a.attribute_id {dateSFilter.Replace("[A]", "a2")} AND a2.ot = (SELECT MAX(a3.ot) FROM {registerData.AllpriTable}_{postfix} a3 WHERE a3.object_id = a.object_id AND a3.attribute_id = a.attribute_id {dateSFilter.Replace("[A]", "a3")} {dateOtFilter}))";
							}

							result.AddRange(_queryManager != null ? _queryManager.ExecuteSql<GbuObjectAttribute>(sql) : QSQuery.ExecuteSql<GbuObjectAttribute>(sql));
						}
					}
					else if (registerData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
					{
						List<RegisterAttribute> attributesData;

						if (attributes == null)
							attributesData = RegisterCache.RegisterAttributes.Values.ToList()
								.Where(x => x.RegisterId == registerId).ToList();
						else
							attributesData = RegisterCache.RegisterAttributes.Values.ToList()
								.Where(x => attributes.Contains(x.Id) && x.RegisterId == registerId).ToList();

						foreach (var attributeData in attributesData)
						{
							if (attributeData.IsPrimaryKey) continue;

							var propName = "StringValue";
							switch (attributeData.Type)
							{
								case RegisterAttributeType.INTEGER:
								case RegisterAttributeType.DECIMAL:
								case RegisterAttributeType.BOOLEAN:
									propName = "NumValue";
									break;
								case RegisterAttributeType.DATE:
									propName = "DtValue";
									break;
								default:
									propName = "StringValue";
									break;
							}

							var sql = GetSqlForRegisterWithAttributePartitioning(uniqueObjectIds, propName,
								attributeData, registerData, isLight);

							if (dateS != null || dateOt != null)
							{
								string dateSFilter = dateS == null
									? String.Empty
									: $"AND [A].s <= {CrossDBSQL.ToDate(dateS.Value)}";
								string dateOtFilter = dateOt == null
									? String.Empty
									: $"AND [A].Ot <= {CrossDBSQL.ToDate(dateOt.Value)}";

								sql =
									$"{sql} {dateSFilter.Replace("[A]", "A")} and A.OT = (SELECT MAX(A2.OT) FROM {registerData.AllpriTable}_{attributeData.Id} A2 WHERE A2.object_id = A.object_id  {dateSFilter.Replace("[A]", "A2")} {dateOtFilter.Replace("[A]", "A2")})";
							}

							result.AddRange(_queryManager != null ? _queryManager.ExecuteSql<GbuObjectAttribute>(sql) : QSQuery.ExecuteSql<GbuObjectAttribute>(sql));
						}
					}
				}

				return result;
			}
		}

        private static string GetSqlForRegisterWithDataPartitioning(List<long> objectIds, string postfix, string propName, RegisterData registerData, bool isLight)
        {
            if (isLight)
				return $@"
                    select 
	                    --a.id,
	                    a.object_id as ObjectId,
	                    a.attribute_id as AttributeId,
	                    --a.Ot,
	                    --a.S,
	                    --{(postfix == "TXT" ? "a.ref_item_id as RefItemId," : String.Empty)}
	                    a.value as {propName}--,

	                    --a.change_user_id as ChangeUserId,
	                    --a.change_doc_id as ChangeDocId,
	                    --a.change_date as ChangeDate,

	                    --a.change_id as ChangeId,

	                    --u.fullname as UserFullname,

	                    --td.regnumber as DocNumber,
	                    --td.description as DocType,
	                    --td.create_date as DocDate

                    from {registerData.AllpriTable}_{postfix} a
                    --left join core_srd_user u on u.id = a.change_user_id
                    --left join core_td_instance td on td.id = a.change_doc_id
                    where a.object_id in ({string.Join(",", objectIds)})";

			return $@"
                select 
	                a.id,
	                a.object_id as ObjectId,
	                a.attribute_id as AttributeId,
	                a.Ot,
	                a.S,
	                {(postfix == "TXT" ? "a.ref_item_id as RefItemId," : String.Empty)}
	                a.value as {propName},

	                a.change_user_id as ChangeUserId,
	                a.change_doc_id as ChangeDocId,
	                a.change_date as ChangeDate,

	                a.change_id as ChangeId,

	                u.fullname as UserFullname,

	                td.regnumber as DocNumber,
	                td.description as DocType,
	                td.create_date as DocDate

                from {registerData.AllpriTable}_{postfix} a
                left join core_srd_user u on u.id = a.change_user_id
                left join core_td_instance td on td.id = a.change_doc_id
                where a.object_id in ({string.Join(",", objectIds)})";
        }

        private static string GetSqlForRegisterWithAttributePartitioning(List<long> objectIds, string propName,
            RegisterAttribute attributeData, RegisterData registerData, bool isLight = false)
        {
            if (isLight)
                return $@"
                    select 
	                --a.id,
	                a.object_id as ObjectId,
	                {attributeData.Id} as AttributeId,
	                --a.Ot,
	                --a.S,
	                --{(attributeData.Type == RegisterAttributeType.STRING ? "a.ref_item_id as RefItemId," : string.Empty)}
	                a.value as {propName}--,

	                --a.change_user_id as ChangeUserId,
	                --a.change_doc_id as ChangeDocId,
	                --a.change_date as ChangeDate,

	                --null as ChangeId,

	                --u.fullname as UserFullname,

	                --td.regnumber as DocNumber,
	                --td.description as DocType,
	                --td.create_date as DocDate

                from {registerData.AllpriTable}_{attributeData.Id} a
                --left join core_srd_user u on u.id = a.change_user_id
                --left join core_td_instance td on td.id = a.change_doc_id
                where a.object_id in ({string.Join(",", objectIds)})";

            return $@"
                select 
	                a.id,
	                a.object_id as ObjectId,
	                {attributeData.Id} as AttributeId,
	                a.Ot,
	                a.S,
	                {(attributeData.Type == RegisterAttributeType.STRING ? "a.ref_item_id as RefItemId," : String.Empty)}
	                a.value as {propName},

	                a.change_user_id as ChangeUserId,
	                a.change_doc_id as ChangeDocId,
	                a.change_date as ChangeDate,

	                null as ChangeId,

	                u.fullname as UserFullname,

	                td.regnumber as DocNumber,
	                td.description as DocType,
	                td.create_date as DocDate

                from {registerData.AllpriTable}_{attributeData.Id} a
                left join core_srd_user u on u.id = a.change_user_id
                left join core_td_instance td on td.id = a.change_doc_id
                where a.object_id in ({string.Join(",", objectIds)})";
        }

        public List<AllDataTreeDto> GetAllDataTree(long objectId, string parentNodeId, long nodeLevel)
		{
			List<AllDataTreeDto> result = new List<AllDataTreeDto>();

			if(parentNodeId.IsNullOrEmpty())
			{
				var mainRegisterData = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());

				var objectName = RegisterHelpServices.GetUserKeyString(mainRegisterData.Id, objectId);

				result.Add(new AllDataTreeDto
				{
					NodeId = "0",
					ParentNodeId = String.Empty,
					AttributeId = null,
					RegisterId = mainRegisterData.Id,
					NodeText = $"{objectName}",
					Level = 0,
					ContentUrl = $"/GbuObject/AllDetails?objectId={objectId}".ResolveClientUrl(),
					HasChilds = false
				});

				foreach (var register in RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegisterData.QuantTable && x.Id != mainRegisterData.Id).OrderBy(x => x.Description))
				{
					long valuesCount = 0;

					if(register.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
					{
						foreach (var attributeData in RegisterCache.RegisterAttributes.Values.Where(x => x.RegisterId == register.Id && x.IsPrimaryKey != true))
						{
							string sql = $"select count(1) as ValuesCount from {register.AllpriTable}_{attributeData.Id} a where a.object_id = {objectId}";

							valuesCount += QSQuery.ExecuteSql(sql, () => new {
								ValuesCount = default(long)
							}).FirstOrDefault()?.ValuesCount ?? 0;
						}
					}
					else
					{
						foreach (var postfix in Postfixes)
						{
							string sql = $"select count(1) as ValuesCount from {register.AllpriTable}_{postfix} a where a.object_id = {objectId}";

							valuesCount += QSQuery.ExecuteSql(sql, () => new {
								ValuesCount = default(long)
							}).FirstOrDefault()?.ValuesCount ?? 0;
						}
					}


					if (valuesCount == 0) continue;

					result.Add(new AllDataTreeDto
					{
						NodeId = register.Id.ToString(),
						ParentNodeId = String.Empty,
						AttributeId = null,
						RegisterId = register.Id,
						NodeText = $"{register.Description} ({valuesCount})",
						Level = 0,
						ContentUrl = $"/GbuObject/AllDetails?objectId={objectId}&registerId={register.Id}".ResolveClientUrl(),
						HasChilds = true
					});
				}
			}
			else
			{
				var parentRegisterData = RegisterCache.GetRegisterData(parentNodeId.ParseToInt());

				var attributesValuesCount = new List<AttributeValuesCount>();

				if (parentRegisterData.AllpriPartitioning == Platform.Register.AllpriPartitioningType.AttributeId)
				{
					foreach (var attributeData in RegisterCache.RegisterAttributes.Values.Where(x => x.RegisterId == parentRegisterData.Id && x.IsPrimaryKey != true))
					{
						string sql = $"select {attributeData.Id} as AttributeId, count(1) as ValuesCount from {parentRegisterData.AllpriTable}_{attributeData.Id} a where a.object_id = {objectId}";

						attributesValuesCount.AddRange(QSQuery.ExecuteSql<AttributeValuesCount>(sql));
					}
				}
				else
				{
					foreach (var postfix in Postfixes)
					{
						string sql = $"select a.attribute_id as AttributeId, count(1) as ValuesCount from {parentRegisterData.AllpriTable}_{postfix} a where a.object_id = {objectId} group by a.attribute_id";

						attributesValuesCount.AddRange(QSQuery.ExecuteSql<AttributeValuesCount>(sql));
					}
				}


					
				
				foreach (var attributeValueCount in attributesValuesCount)
				{
					result.Add(new AllDataTreeDto
					{
						NodeId = $"{parentRegisterData.Id}_{attributeValueCount.AttributeId}",
						ParentNodeId = parentRegisterData.Id.ToString(),
						AttributeId = attributeValueCount.AttributeId,
						RegisterId = parentRegisterData.Id,
						NodeText = $"{RegisterCache.GetAttributeData((int)attributeValueCount.AttributeId).Name} ({attributeValueCount.ValuesCount})",
						Level = 1,
						ContentUrl = $"/GbuObject/AllDetails?objectId={objectId}&attributeId={attributeValueCount.AttributeId}".ResolveClientUrl(),
						HasChilds = false
					});

					result = result.OrderBy(x => RegisterCache.GetAttributeData((int)x.AttributeId).Name).ToList();
				}
			}			

			return result;
		}

		public List<GbuAttributeValueObjectsCountDto> GetAttributeValueKoObjectsCount(long attributeId, KoUnitStatus koUnitStatus, DateTime? taskCreationDateFrom, DateTime? taskCreationDateTo,
			QueryManager queryManager)
		{
			var attributeData = RegisterCache.GetAttributeData(attributeId);
			var registerData = RegisterCache.GetRegisterData(attributeData.RegisterId);

			string postfix;
			if (registerData.AllpriPartitioning == AllpriPartitioningType.DataType)
			{
				switch (attributeData.Type)
				{
					case RegisterAttributeType.INTEGER:
					case RegisterAttributeType.DECIMAL:
					case RegisterAttributeType.BOOLEAN:
						postfix  = "NUM";
						break;
					case RegisterAttributeType.DATE:
						postfix = "DT";
						break;
					default:
						postfix = "TXT";
						break;
				}
			}
			else
			{
				postfix = attributeData.Id.ToString();
			}

			string sql =
$@"select 
	data.Value as AttributeValue, 
	count(data.UnitId) as ObjectsCount
from (select 
			a.value as Value, 
			u.id as UnitId
		from {registerData.AllpriTable}_{postfix} a 
		join ko_unit u on u.object_id=a.object_id
		join ko_task t on t.id=u.task_id
		where a.S <= {CrossDBSQL.ToDate(DateTime.Now.GetEndOfTheDay())} 
			and a.OT = (SELECT MAX(A2.OT) FROM
						{registerData.AllpriTable}_{postfix} A2 
						WHERE A2.object_id = A.object_id
							AND A2.s <= {CrossDBSQL.ToDate(DateTime.Now.GetEndOfTheDay())})
			and u.STATUS_CODE = {(long)koUnitStatus}";

			if (registerData.AllpriPartitioning == AllpriPartitioningType.DataType)
			{
				sql += $" and a.attribute_id={attributeData.Id}";
			}

			if (taskCreationDateFrom.HasValue)
			{
				sql += $" and t.CREATION_DATE >= {CrossDBSQL.ToDate(taskCreationDateFrom.Value)}";
			}

			if (taskCreationDateTo.HasValue)
			{
				sql += $" and t.CREATION_DATE <= {CrossDBSQL.ToDate(taskCreationDateTo.Value)}";
			}

			sql += ") data group by data.Value";

			return queryManager.ExecuteSql<GbuAttributeValueObjectsCountDto>(sql);
		}

		public List<long> GetGbuRegistersIds()
		{
			return OMObjectsCharacteristicsRegister.Where(x => true)
				.Select(x => x.RegisterId).Execute().Select(x => x.RegisterId.GetValueOrDefault()).ToList();
		}

		public List<GbuAttributesTreeDto> GetGbuAttributesTree()
		{
			var gbuRegisterIds = GetGbuRegistersIds();
			return RegisterCache.Registers.Values.Where(x => gbuRegisterIds.Contains(x.Id)).Select(x => new GbuAttributesTreeDto
			{
				Text = x.Description,
				Value = x.Id.ToString(),
				Items = RegisterCache.RegisterAttributes.Values.Where(y => y.RegisterId == x.Id && y.IsDeleted == false)
					.Select(y => new SelectListItem
					{
						Text = y.Name,
						Value = y.Id.ToString()
					}).ToList()
			}).ToList();
		}

        public List<OMAttribute> GetGbuAttributes()
        {
	        return OMAttribute.Where(x => GetGbuRegistersIds().Contains(x.RegisterId) && x.IsDeleted.Coalesce(false) == false).SelectAll().Select(x => x.ParentRegister.RegisterDescription).Execute();
        }

        public int AddNewVirtualAttribute(string attributeName, long registerId, RegisterAttributeType type)
        {
			if (string.IsNullOrEmpty(attributeName) || registerId == 0) return 0;
			int id;
			try
			{
				id = new OMAttribute
				{
					Name = attributeName,
					RegisterId = registerId,
					Type = (long) type,
				}.Save();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return id;
        }

        public static string GetAttributeNameById(long idAttribute)
        {
	        return RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == idAttribute)?.Name;
        }

        public static string GetRegisterNameByAttributeId(long idAttribute)
        {
	       var attrId= RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == idAttribute)?.RegisterId;

	       return RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == attrId)?.Description;
        }

        public static void SaveAttributeValueWithCheck(GbuObjectAttribute attributeValue)
        {
	        if (CheckExistsValueFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot) != null)
	        {
		        // Проблема в наличии значения на ту же дату ОТ
			        attributeValue.Ot = GetNextOtFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot);
		    }

	        attributeValue.Save();
        }
	}
}
