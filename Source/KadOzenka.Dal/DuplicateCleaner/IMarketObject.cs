using System;
using ObjectModel.Directory;

namespace KadOzenka.Dal.DuplicateCleaner
{
	public interface IMarketObject
	{
		string CadastralNumber { get; set; }
		DealType DealType_Code { get; set; }
		PropertyTypes PropertyType_Code { get; set; }
		string Subcategory { get; set; }
		decimal? Area { get; set; }
		long? Price { get; set; }
		DateTime? ParserTime { get; set; }
		ProcessStep ProcessType_Code { get; set; }
		ExclusionStatus ExclusionStatus_Code { get; set; }

		int Save();
	}
}
