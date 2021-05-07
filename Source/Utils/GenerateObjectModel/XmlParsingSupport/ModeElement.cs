using System.Configuration;

namespace GenerateObjectModel.XmlParsingSupport
{
	public class ModeElement : ConfigurationElement
	{
		[ConfigurationProperty(Consts.ModeTypeTag, IsRequired = true)]
		public string Type => this[Consts.ModeTypeTag] as string;

		private string _registerFilter;
		[ConfigurationProperty(Consts.RegisterFilterTag, IsRequired = true)]
		public string RegisterFilter
		{
			get
			{
				if(string.IsNullOrWhiteSpace(_registerFilter))
					_registerFilter = this[Consts.RegisterFilterTag] as string;

				return _registerFilter;
			}
			set => _registerFilter = value;
		}

		[ConfigurationProperty(Consts.PathTag, IsRequired = true)]
		public string Path => this[Consts.PathTag] as string;

		[ConfigurationProperty(Consts.FileNameStartingTag, IsRequired = true)]
		public string FileNameStarting => this[Consts.FileNameStartingTag] as string;

		public override string ToString()
		{
			return $"{Type}, {Path}, {RegisterFilter}, {FileNameStarting}";
		}
	}
}