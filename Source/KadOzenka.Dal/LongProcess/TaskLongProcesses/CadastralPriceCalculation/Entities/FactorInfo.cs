using Core.Register;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Entities
{
	public class FactorInfo
	{
		public long FactorId { get; set; }
		public MarkType MarkType { get; set; }

		public string AttributeName { get; set; }
		public RegisterAttributeType AttributeType { get; set; }
	}
}