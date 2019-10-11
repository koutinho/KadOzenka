using Core.Register;
using Core.Register.QuerySubsystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KadOzenka.Dal.GbuObject
{
    public class GbuObjectService
    {
		public List<GbuObjectAttribute> GetAllAttributes(long objectId, List<long> sources = null)
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
select a.id,
a.object_id as ObjectId,
a.attribute_id as AttributeId,
a.Ot,
a.S,
{(postfix == "TXT" ? "a.ref_item_id as RefItemId," : String.Empty)}
a.value as {propName},

a.change_user_id as ChangeUserId,
a.change_doc_id as ChangeDocId,
a.change_date as ChangeDate,

u.fullname as UserFullname,
td.regnumber as DocNumber,
td.description as DocType,
td.create_date as DocDate,
a.change_id as ChangeId
from {registerData.AllpriTable}_{postfix} a
left join core_srd_user u on u.id = a.change_user_id
left join core_td_instance td on td.id = a.change_doc_id
where a.object_id = {objectId}";

					result.AddRange(QSQuery.ExecuteSql<GbuObjectAttribute>(sql));
				}
			}
			
			return result;
		}

	}
}
