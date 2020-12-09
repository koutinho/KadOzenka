using Core.Messages;

namespace KadOzenka.Dal.DataComparing.DataComparers
{
	public interface IDataComparer
	{
		void PerformProc(MessageAddressersDto messageAddresses);
	}
}
