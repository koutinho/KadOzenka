using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.Gbu.Harmonization;

namespace KadOzenka.Web.Models.GbuObject
{
	public class HarmonizationCODViewModel : HarmonizationViewModel
	{
		/// <summary>
		/// Идентификатор задания ЦОД
		/// </summary>
		[Display(Name = "Задание ЦОД")]
		[Required(ErrorMessage = "Выберете Задание ЦОД")]
		public long? IdCodJob { get; set; }

		/// <summary>
		/// Значение по умолчанию 
		/// </summary>
		[Display(Name = "Значение по умолчанию")]
		public string DefaultValue { get; set; }

		/// <summary>
		/// Документ для значения по умолчанию 
		/// </summary>
		[Display(Name = "Документ")]
		public PartialDocumentViewModel Document { get; set; } = new PartialDocumentViewModel();


        public HarmonizationCODSettings ToHarmonizationCODSettings()
		{
			var settings = new HarmonizationCODSettings
			{
				IdCodJob = IdCodJob,
				IdAttributeResult = IdAttributeResult,
				PropertyType = (PropertyTypes) PropertyType.GetValueOrDefault(),
				SelectAllObject = SelectAllObject,
				IdAttributeFilter = IdAttributeFilter,
				ValuesFilter = ValuesFilter,
				Level1Attribute = Level1Attribute,
				Level2Attribute = Level2Attribute,
				Level3Attribute = Level3Attribute,
				Level4Attribute = Level4Attribute,
				Level5Attribute = Level5Attribute,
				Level6Attribute = Level6Attribute,
				Level7Attribute = Level7Attribute,
				Level8Attribute = Level8Attribute,
				Level9Attribute = Level9Attribute,
				Level10Attribute = Level10Attribute,
                AdditionalLevels = MapAdditionalCustomLevels(),
				DefaultValue = DefaultValue,
				IdDocument = Document.IdDocument,
				TaskFilter = IsTaskFilterUsed ? TaskFilter : null,
				DateActual = IsDataActualUsed ? DataActual : null
			};

			return settings;
		}
	}
}
