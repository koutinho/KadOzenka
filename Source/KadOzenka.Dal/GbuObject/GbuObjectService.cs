using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using KadOzenka.Dal.Models.GbuObject;
using KadOzenka.Web.Models.GbuObject;
using System;
using System.Collections.Generic;
using System.Linq;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.GbuObject
{
    public class GbuObjectService
    {
		public static List<string> Postfixes = new List<string> { "TXT", "NUM", "DT" };

		public List<GbuObjectAttribute> GetAllAttributes(long objectId, List<long> sources = null, List<long> attributes = null, DateTime? dateS = null, DateTime? dateOt = null)
		{
			var getSources = sources;

			if(getSources == null)
			{
				var mainRegister = RegisterCache.GetRegisterData(ObjectModel.Gbu.OMMainObject.GetRegisterId());

				getSources = RegisterCache.Registers.Values.Where(x => x.QuantTable == mainRegister.QuantTable && x.Id != mainRegister.Id).Select(x => (long)x.Id).ToList();
			}

			var result = new List<GbuObjectAttribute>();

			foreach (var registerId in getSources)
			{
				var registerData = RegisterCache.GetRegisterData((int)registerId);

				var postfixes = new List<string> { "TXT", "NUM", "DT" };

				foreach(var postfix in postfixes)
				{
					var propName = "StringValue";

					if(postfix == "NUM")
					{
						propName = "NumValue";
					}
					else if(postfix == "DT")
					{
						propName = "DtValue";
					}

					var sql = $@"
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
where a.object_id = {objectId}";

					if(attributes != null && attributes.Count > 0)
					{
						sql = $"{sql} and a.attribute_id in ({String.Join(",", attributes)})";
					}

					if(dateS != null || dateOt != null)
					{
						string dateSFilter = dateS == null ? String.Empty : $"AND [A].s <= {CrossDBSQL.ToDate(dateS.Value)}";
						string dateOtFilter = dateOt == null ? String.Empty : $"AND A3.Ot <= {CrossDBSQL.ToDate(dateOt.Value)}";

						sql = $"{sql} and A.ID = (SELECT MAX(a2.id) FROM {registerData.AllpriTable}_{postfix} a2 WHERE a2.object_id = a.object_id AND a2.attribute_id = a.attribute_id {dateSFilter.Replace("[A]", "a2")} AND a2.ot = (SELECT MAX(a3.ot) FROM {registerData.AllpriTable}_{postfix} a3 WHERE a3.object_id = a.object_id AND a3.attribute_id = a.attribute_id {dateSFilter.Replace("[A]", "a3")} {dateOtFilter}))";
					}

					result.AddRange(QSQuery.ExecuteSql<GbuObjectAttribute>(sql));
				}
			}
			
			return result;
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

					foreach (var postfix in Postfixes)
					{
						string sql = $"select count(1) as ValuesCount from {register.AllpriTable}_{postfix} a where a.object_id = {objectId}";

						valuesCount += QSQuery.ExecuteSql(sql, () => new {
							ValuesCount = default(long)
						}).FirstOrDefault()?.ValuesCount ?? 0;
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

				foreach (var postfix in Postfixes)
				{
					string sql = $"select a.attribute_id as AttributeId, count(1) as ValuesCount from {parentRegisterData.AllpriTable}_{postfix} a where a.object_id = {objectId} group by a.attribute_id";

					attributesValuesCount.AddRange(QSQuery.ExecuteSql<AttributeValuesCount>(sql));
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


        public List<OMAttribute> GetGbuAttributes()
        {
            var registersIds = ObjectModel.KO.OMObjectsCharacteristicsRegister.Where(x => true)
                .Select(x => x.RegisterId).Execute().Select(x => x.RegisterId).ToList();

            return OMAttribute.Where(x => registersIds.Contains(x.RegisterId)).SelectAll().Execute();
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
	}
}
