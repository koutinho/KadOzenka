using ObjectModel.Core.Register;
using Platform.Configurator;
using Platform.Configurator.DbConfigurator;

namespace KadOzenka.Dal.CommonFunctions
{
	public class RegisterConfiguratorWrapper : IRegisterConfiguratorWrapper
	{
		public void CreateDbTableForRegister(long registerId)
		{
			RegisterConfigurator.CreateDbTableForRegister(registerId);
		}

		public DbConfiguratorBase GetDbConfigurator()
		{
			return RegisterConfigurator.GetDbConfigurator();
		}

		public void CreateDbColumnForRegister(OMAttribute attribute, DbConfiguratorBase dbConfigurator)
		{
			RegisterConfigurator.CreateDbColumnForRegister(attribute, dbConfigurator);
		}
	}

	public interface IRegisterConfiguratorWrapper
	{
		void CreateDbTableForRegister(long registerId);
		DbConfiguratorBase GetDbConfigurator();
		void CreateDbColumnForRegister(OMAttribute attribute, DbConfiguratorBase dbConfigurator);
	}
}
