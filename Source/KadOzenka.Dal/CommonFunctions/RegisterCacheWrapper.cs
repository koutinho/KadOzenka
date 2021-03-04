using Core.Register;
using Core.Register.RegisterEntities;

namespace KadOzenka.Dal.CommonFunctions
{
	public class RegisterCacheWrapper : IRegisterCacheWrapper
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
