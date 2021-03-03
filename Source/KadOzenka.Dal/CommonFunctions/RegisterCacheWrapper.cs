using Core.Register;
using Core.Register.RegisterEntities;

namespace KadOzenka.Dal.CommonFunctions
{
	public class RegisterCacheWrapper
	{
		public RegisterData GetRegisterData(int registerId)
		{
			return RegisterCache.GetRegisterData(registerId);
		}
	}

	public interface IRegisterCacheWrapper
	{
		RegisterData GetRegisterData(int registerId);
	}
}
