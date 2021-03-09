using Core.SRD;

namespace KadOzenka.Dal.CommonFunctions
{
	public class SRDSessionWrapper : ISRDSessionWrapper
	{
		public void CheckAccessToFunction(string functionTag, bool exceptionOnAccessDenied = false, bool logOnSuccess = false, bool logOnSuccessDenied = false)
		{
			SRDSession.Current.CheckAccessToFunction(functionTag, exceptionOnAccessDenied, logOnSuccess, logOnSuccessDenied);
		}
	}

	public interface ISRDSessionWrapper
	{
		void CheckAccessToFunction(string functionTag, bool exceptionOnAccessDenied = false, bool logOnSuccess = false,
			bool logOnSuccessDenied = false);
	}
}
