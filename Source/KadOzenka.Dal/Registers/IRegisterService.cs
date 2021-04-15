using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Registers
{
	public interface IRegisterService
	{
		OMRegister GetRegister(long? registerId);

		OMRegister CreateRegister(string registerName, string registerDescription, string quantTable,
			string allPriTable = null, long? storageType = 4);

		int CreateIdColumnForRegister(long registerId);
		void RemoveRegister(long registerId, long eventId);
        int SaveRegister(OMRegister register);
    }
}
