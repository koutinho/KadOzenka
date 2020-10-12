using System;
using System.Linq;
using Core.Register;
using Core.Register.RegisterEntities;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;

namespace KadOzenka.Dal.Registers.GbuRegistersServices
{
	public abstract class GbuRegisterService
	{
		protected abstract string RegisterName { get; }

		private long? _registerId;
		public long RegisterId
		{
			get
			{
				if (_registerId != null)
					return _registerId.Value;

				var omMainObjectRegisterId = OMMainObject.GetRegisterId();
				_registerId = OMRegister
					.Where(x => x.MainRegister == omMainObjectRegisterId &&
					            x.RegisterDescription == RegisterName).ExecuteFirstOrDefault().RegisterId;

				return _registerId.Value;
			}
		}

		protected RegisterAttribute GetRegisterAttributeByName(long registerId, string name)
		{
			var attribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == registerId && x.Name == name);
			if (attribute == null)
				throw new Exception($"Не найден атрибут источника '{RegisterName}' с именем '{name}'");

			return attribute;
		}
	}
}
