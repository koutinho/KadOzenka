namespace GenerateObjectModel.JsonParsingSupport
{
	public class Mode
	{
		public string Type { get; set; }
		public string RegisterFilter { get; set; }
		public string Path { get; set; }
		public string FileNameStarting { get; set; }

		public override string ToString()
		{
			return $"{RegisterFilter} | {Path} | {FileNameStarting}";
		}
	}
}
