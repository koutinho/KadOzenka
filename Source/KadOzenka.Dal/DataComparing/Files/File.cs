namespace KadOzenka.Dal.DataComparing.Files
{
	public abstract class File
	{
		public string FullName { get; protected set; }
		public string Name { get; protected set; }
		public string FileNameWithoutExtension { get; protected set; }
		public SystemType SystemType { get; protected set; }

		protected File(string fileName)
		{
			FullName = fileName;
			Name = System.IO.Path.GetFileName(fileName);
			FileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(Name);
		}
	}
}
