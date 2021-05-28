using System.Configuration;

namespace GenerateObjectModel.XmlParsingSupport
{
	public class ModesCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new ModeElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ModeElement)element).RegisterFilter;
		}
	}
}