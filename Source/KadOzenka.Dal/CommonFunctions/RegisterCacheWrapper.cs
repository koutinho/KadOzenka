using System.Collections.Generic;
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

        public Dictionary<int, RegisterData> GetRegistersCache()
        {
            return RegisterCache.Registers;
        }

        public void UpdateCache()
        {
            RegisterCache.UpdateCache(0, null);
        }

        public Dictionary<long, RegisterAttribute> GetRegisterAttributesCache()
        {
            return RegisterCache.RegisterAttributes;
        }
    }

	public interface IRegisterCacheWrapper
	{
		RegisterData GetRegisterData(int registerId);

        Dictionary<int, RegisterData> GetRegistersCache();

        Dictionary<long, RegisterAttribute> GetRegisterAttributesCache();

        void UpdateCache();
    }
}
