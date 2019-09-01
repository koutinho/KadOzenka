

namespace CIPJS.DAL.Egas
{
	public class ConfigEhdObjectsImport
    {
		public string BuildingParcel { get; set; }
		public string BuildingParcelLog { get; set; }
		public bool BuildingParcelSkip { get; set; }

		public string Register { get; set; }
		public string RegisterLog { get; set; }
		public bool RegisterSkip { get; set; }

		public string Location { get; set; }
		public string LocationLog { get; set; }
		public bool LocationSkip { get; set; }

		public string Egrp { get; set; }
		public string EgrpLog { get; set; }
		public bool EgrpSkip { get; set; }

		public string Right { get; set; }
		public string RightLog { get; set; }
		public bool RightSkip { get; set; }
		public string Space { get; set; }

		public string OldNumbers { get; set; }
		public string OldNumbersLog { get; set; }
		public bool OldNumbersSkip { get; set; }

        public string Floors { get; set; }
        public string FloorsLog { get; set; }
        public bool FloorsSkip { get; set; }

        public string ElementsConstruct { get; set; }
        public string ElementsConstructLog { get; set; }
        public bool ElementsConstructSkip { get; set; }

        public string ExploitationChar { get; set; }
        public string ExploitationCharLog { get; set; }
        public bool ExploitationCharSkip { get; set; }

		public int? PackageSize { get; set; }

		public int? ThreadsCount { get; set; }
	}
}
