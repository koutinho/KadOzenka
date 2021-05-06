using System.Configuration;

namespace GenerateObjectModel.XmlParsingSupport
{
	public class ModeElement : ConfigurationElement
	{
		[ConfigurationProperty(Consts.ModeTypeTag, IsRequired = true)]
		public string Type => this[Consts.ModeTypeTag] as string;

		[ConfigurationProperty(Consts.RegisterFilterTag, IsRequired = true)]
		public string RegisterFilter => this[Consts.RegisterFilterTag] as string;

		[ConfigurationProperty(Consts.ReferenceFilterTag, IsRequired = true)]
		public string ReferenceFilter => this[Consts.ReferenceFilterTag] as string;

		[ConfigurationProperty(Consts.PathTag, IsRequired = true)]
		public string Path => this[Consts.PathTag] as string;

		public override string ToString()
		{
			return $"{Type}, {Path}, {RegisterFilter}, {ReferenceFilter}";
		}
	}
}