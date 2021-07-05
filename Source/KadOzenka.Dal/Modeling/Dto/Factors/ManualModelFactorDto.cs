﻿using ObjectModel.Directory;
using ObjectModel.Directory.Ko;

namespace KadOzenka.Dal.Modeling.Dto.Factors
{
	public class ManualModelFactorDto
	{
		public long Id { get; set; }
		public long? GeneralModelId { get; set; }
		public KoAlgoritmType Type { get; set; }
		public long? FactorId { get; set; }
		public long? DictionaryId { get; set; }
		public string Factor { get; set; }
		public long? MarkerId { get; set; }
		public decimal Weight { get; set; }
		public decimal B0 { get; set; }
		public bool SignDiv { get; set; }
		public bool SignAdd { get; set; }
		public MarkType MarkType { get; set; }
		public decimal? CorrectItem { get; set; }
		public decimal? K { get; set; }
	}
}
