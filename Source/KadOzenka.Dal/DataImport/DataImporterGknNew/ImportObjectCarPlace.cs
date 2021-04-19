using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectCarPlace : ImportObjectBase<xmlObjectCarPlace>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.Parking;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Машино-мест";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Машино-мест";
		public override string SuccessMessage => "Импорт Машино-мест завершен";


		public ImportObjectCarPlace(DateTime unitDate, long idTour, OMTask task, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument, Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction)
			: base(unitDate, idTour, task, koNoteType, sDate, otDate, idDocument, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
		{
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.SquareAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ObjectNameAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Use, new ImportedAttribute(Consts.BuildingPurposeAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(Consts.YearOfBuildAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(Consts.YearOfUseAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(Consts.FloorCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(Consts.FloorUndergroundCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(Consts.WallMaterialAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.CadastralBuilding, new ImportedAttribute(Consts.CadastralNumberOKSAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.NumberFloor, new ImportedAttribute(24));
		}

		protected override void InitTaskFormingAttributes()
		{
			base.InitTaskFormingAttributes();
			TaskFormingAttributes.Add(new ImportedAttribute(Consts.P3WallMaterialAttributeId));
			TaskFormingAttributes.Add(new ImportedAttribute(Consts.P4YearOfBuildAttributeId));
		}

        protected override void DoInheritanceFromPrevUnit(OMUnit lastUnit, OMUnit koUnit, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
        {
	        //Признак не поменялся ли тип объекта?
	        bool prTypeObjectCheck = lastUnit.PropertyType_Code == koUnit.PropertyType_Code;

	        //Если не было изменений типа, наименования и назначения и не было обращения
	        if (prTypeObjectCheck && unitChangesDictionary[KoChangeStatus.Use]
	                              && unitChangesDictionary[KoChangeStatus.Name])
	        {
		        #region Наследование группы и подгруппы предыдущего объекта
		        koUnit.GroupId = lastUnit.GroupId;
		        koUnit.Save();
		        #endregion
	        }
        }

        protected override void SetCODTasksFormingAttributesWithChecking(long gbuObjectId, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			if (!unitChangesDictionary[KoChangeStatus.Name] || !unitChangesDictionary[KoChangeStatus.Use])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P1GroupAttributeId), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.CadastralBlock] || !unitChangesDictionary[KoChangeStatus.Place])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P2FsAttributeId), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.Walls])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P3WallMaterialAttributeId), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.YearBuild] || !unitChangesDictionary[KoChangeStatus.YearUse])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P4YearOfBuildAttributeId), true, gbuObjectId);
			}
        }

		protected override void InitGknDataAttributes()
		{
			base.InitGknDataAttributes();

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.CadastralNumberOksAttributeIdValue, current => ((xmlObjectCarPlace)current).CadastralNumberOKS);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.CadastralNumberOksAttributeIdValue, current => ((xmlObjectCarPlace)current).ParentOks?.CadastralNumberOKS);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.ObjectTypeAttributeIdValue, current => ((xmlObjectCarPlace)current).ParentOks?.ObjectType?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.AssignationBuildingAttributeIdValue, current => ((xmlObjectCarPlace)current).ParentOks?.AssignationBuilding?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.AssignationNameAttributeIdValue, current => ((xmlObjectCarPlace)current).ParentOks?.AssignationName);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.WallMaterialAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectCarPlace)current).ParentOks?.Walls));
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.ExploitationCharYearBuiltAttributeIdValue, current => ((xmlObjectCarPlace)current).ParentOks?.Years?.Year_Built);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.ExploitationCharYearUsedAttributeIdValue, current => ((xmlObjectCarPlace)current).ParentOks?.Years?.Year_Used);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.FloorCountAttributeIdValue, current => ((xmlObjectCarPlace)current).ParentOks?.Floors?.Floors);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.ParentOks?.FloorUndergroundCountAttributeIdValue, current => ((xmlObjectCarPlace)current).ParentOks?.Floors?.Underground_Floors);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.AreaAttributeIdValue, current => ((xmlObjectCarPlace)current).Area);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.PositionInObject?.NumberAttributeIdValue, current => ((xmlObjectCarPlace)current).PositionInObject?.Number);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.PositionInObject?.TypeAttributeIdValue, current => ((xmlObjectCarPlace)current).PositionInObject?.Type?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.PositionInObject?.PositionNumberOnPlanAttributeIdValue, current => ((xmlObjectCarPlace)current).PositionInObject?.Position?.NumberOnPlan);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.PositionInObject?.PositionDescriptionAttributeIdValue, current => ((xmlObjectCarPlace)current).PositionInObject?.Position?.Description);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.UnitedCadastralNumberAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectCarPlace)current).UnitedCadastralNumbers));

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.FacilityCadastralNumberAttributeIdValue, current => ((xmlObjectCarPlace)current).FacilityCadastralNumber);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.CarPlace.FacilityPurposeAttributeIdValue, current => ((xmlObjectCarPlace)current).FacilityPurpose);
		}

		protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectCarPlace current)
		{
			koUnit.Square = current.Area.ParseToDecimal();
			koUnit.BuildingCadastralNumber = current.CadastralNumberOKS;
		}
    }
}
