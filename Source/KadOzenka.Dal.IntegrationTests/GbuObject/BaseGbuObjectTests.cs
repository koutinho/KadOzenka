using System.Collections.Generic;
using Core.Register.QuerySubsystem;
using KadOzenka.Common.Tests.Consts;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.IntegrationTests;
using NUnit.Framework;

namespace KadOzenka.Dal.Integration.GbuObject
{
	public class BaseGbuObjectTests : BaseTests
	{
		protected IGbuObjectService GbuObjectService { get; set; }


		[OneTimeSetUp]
		protected void OneTimeSetUpForGbuObject()
		{
			GbuObjectService = new GbuObjectService();
		}


		//делаем через свой метод, чтобы не зависить от GbuObjectService
		protected List<EgrnAttributeRow> GetAttributeValue(EgrnAttributeForTest attribute, long objectId, long documentId)
		{
			var sql = $@"select value as {nameof(EgrnAttributeRow.Value)} 
							from {attribute.TableName} 
							where object_id = {objectId} and change_doc_id = {documentId}";

			return QSQuery.ExecuteSql<EgrnAttributeRow>(sql);
		}

		public class EgrnAttributeRow
		{
			//public long Id { get; set; }
			//public long ObjectId { get; set; }
			//public DateTime Ot { get; set; }
			//public DateTime S { get; set; }
			//public long? ReferenceId { get; set; }
			public object Value { get; set; }
			//public DateTime ChangeDate { get; set; }
			//public long ChangeUserId { get; set; }
			//public long? ChangeDocumentId { get; set; }
		}
	}
}
