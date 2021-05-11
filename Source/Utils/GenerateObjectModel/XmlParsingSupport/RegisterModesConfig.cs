using System.Configuration;

namespace GenerateObjectModel.XmlParsingSupport
{
	public class RegisterModesConfig : ConfigurationSection
	{
		public static RegisterModesConfig GetConfig()
		{
			return (RegisterModesConfig)ConfigurationManager.GetSection("RegisteredModes") ?? new RegisterModesConfig();
		}

		[ConfigurationProperty(Consts.ModesCollectionTag)]
		[ConfigurationCollection(typeof(ModesCollection), AddItemName = "Mode")]
		public ModesCollection ModesCollection
		{
			get
			{
				var o = this[Consts.ModesCollectionTag];
				return o as ModesCollection;
			}
		}
	}
}