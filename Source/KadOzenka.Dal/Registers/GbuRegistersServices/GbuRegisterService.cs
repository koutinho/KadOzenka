using System;
using System.Linq;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.UI.Registers.Models.Registers;
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
				var register = OMRegister
					.Where(x => x.MainRegister == omMainObjectRegisterId &&
					            x.RegisterDescription == RegisterName).ExecuteFirstOrDefault();
				if (register == null)
					throw new Exception($"Не найден реестр с именем '{RegisterName}' и основным реестром 'Объекты недвижимости'");

				_registerId = register.RegisterId;

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
