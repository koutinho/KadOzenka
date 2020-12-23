using System.ComponentModel;

namespace KadOzenka.Dal.DataComparing
{
	public enum DataComparingFileType
	{
		[Description("Протокол изменений")]
		TaskChangesPkkoFile,
		[Description("COST файлы")]
		CostPkkoFiles,
		[Description("FD файлы")]
		FdPkkoFiles
	}
}
