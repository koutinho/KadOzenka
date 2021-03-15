using System.ComponentModel;

namespace KadOzenka.Dal.GbuObject.Entities
{
	public enum GbuColumnsToDownload
	{
		[Description("a.Ot")]
		Ot,
		[Description("a.value")]
		Value,
		[Description("a.change_doc_id as ChangeDocId")]
		DocumentId
		//TODO продумать если будут нужны данные из присоединенных (join) таблиц
	}
}
