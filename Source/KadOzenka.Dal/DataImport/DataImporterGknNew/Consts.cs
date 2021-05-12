using System.Collections.Generic;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public static class Consts
	{
		public static readonly int RosreestrRegisterId = 2;

		public static readonly int ParcelNameAttributeId = 1;
		public static readonly int SquareAttributeId = 2;
		public static readonly int LandCategoryAttributeId = 3;
		public static readonly int TypeOfUseByDocumentsAttributeId = 4;
		public static readonly int TypeOfUseByClassifierAttributeId = 5;
		public static readonly int CadastralCostAttributeId = 6;
		public static readonly int LocationAttributeId = 8;
		public static readonly int CreationDateAttributeId = 13;
		public static readonly int BuildingPurposeAttributeId = 14;
		public static readonly int YearOfBuildAttributeId = 15;
		public static readonly int YearOfUseAttributeId = 16;
		public static readonly int FloorCountAttributeId = 17;
		public static readonly int FloorUndergroundCountAttributeId = 18;
		public static readonly int ObjectNameAttributeId = 19;
		public static readonly int EvaluationGroupAttributeId = 20;
		public static readonly int WallMaterialAttributeId = 21;
		public static readonly int ConstructionPurposeAttributeId = 22;
		public static readonly int PlacementPurposeAttributeId = 23;
		public static readonly int FloorNumberAttributeId = 24;
		public static readonly int FloorTypeAttributeId = 25;
		public static readonly int ObjectTypeAttributeId = 26;
		public static readonly int PlacementCharacteristicAttributeId = 44;
		public static readonly int ReadinessPercentageAttributeId = 46;
		public static readonly int AddressAttributeId = 600;
		public static readonly int CadastralQuarterAttributeId = 601;
		public static readonly int ParcelAttributeId = 602;
		public static readonly int PlacementTypeAttributeId = 603;
		public static readonly int CadastralNumberOKSAttributeId = 604;
		public static readonly int CadastralNumberFlatAttributeId = 605;
		public static readonly int FlatNumbersAttributeId = 606;

		public static readonly int P1GroupAttributeId = 660;
		public static readonly int P2FsAttributeId = 661;
		public static readonly int P3WallMaterialAttributeId = 662;
		public static readonly int P4YearOfBuildAttributeId = 663;
	}

	public static class RequiredFieldsForExcelMapping
	{
		public static readonly long ObjectTypeAttributeId = Consts.ObjectTypeAttributeId;
		public static readonly long CadastralNumberAttributeId = 1416;
		public static readonly long SquareAttributeId = Consts.SquareAttributeId;
		public static readonly long AssessmentDateAttributeId;

		public static readonly List<long> RequiredAttributeIds;

		static RequiredFieldsForExcelMapping()
		{
			AssessmentDateAttributeId = OMUnit.GetColumnAttributeId(x => x.AssessmentDate);

			RequiredAttributeIds = new List<long>
			{
				ObjectTypeAttributeId,
				CadastralNumberAttributeId,
				SquareAttributeId,
				AssessmentDateAttributeId
			};
		}
	}
}
