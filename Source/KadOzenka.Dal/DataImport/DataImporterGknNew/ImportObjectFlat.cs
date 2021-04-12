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
	public class ImportObjectFlat : ImportObjectBase<xmlObjectFlat>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.Pllacement;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Помещений";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Помещений";
		public override string SuccessMessage => "Импорт Помещений завершен";

		public ImportObjectFlat(DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument, Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction)
			: base(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
		{
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.SquareAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Assignment, new ImportedAttribute(Consts.PlacementPurposeAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ObjectNameAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Use, new ImportedAttribute(Consts.BuildingPurposeAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(Consts.YearOfBuildAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(Consts.YearOfUseAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(Consts.FloorCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(Consts.FloorUndergroundCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(Consts.WallMaterialAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.CadastralBuilding, new ImportedAttribute(Consts.CadastralNumberOKSAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.NumberFloor, new ImportedAttribute(Consts.FloorNumberAttributeId));
		}

        protected override void DoInheritanceFromPrevUnit(OMUnit lastUnit, OMUnit koUnit, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			//Признак не поменялся ли тип объекта?
			bool prTypeObjectCheck = lastUnit.PropertyType_Code == koUnit.PropertyType_Code;

			//Если не было изменений типа, наименования и назначения и не было обращения
			if (prTypeObjectCheck && unitChangesDictionary[KoChangeStatus.Assignment]
                                  && unitChangesDictionary[KoChangeStatus.Name])
			{
				#region Наследование группы и подгруппы предыдущего объекта
				koUnit.GroupId = lastUnit.GroupId;
				koUnit.Save();
				#endregion
			}
		}

		protected override void InitTaskFormingAttributes()
		{
			base.InitTaskFormingAttributes();
			TaskFormingAttributes.Add(new ImportedAttribute(Consts.P3WallMaterialAttributeId));
			TaskFormingAttributes.Add(new ImportedAttribute(Consts.P4YearOfBuildAttributeId));
		}

        protected override void SetCODTasksFormingAttributesWithChecking(long gbuObjectId, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			if (!unitChangesDictionary[KoChangeStatus.Name] || !unitChangesDictionary[KoChangeStatus.Assignment])
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
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CadastralNumberFlatAttributeIdValue, current => ((xmlObjectFlat)current).CadastralNumberFlat);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CadastralNumberOksAttributeIdValue, current => ((xmlObjectFlat)current).CadastralNumberOKS);
			
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.CadastralNumberOksAttributeIdValue, current => ((xmlObjectFlat)current).ParentOks?.CadastralNumberOKS);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.ObjectTypeAttributeIdValue, current => ((xmlObjectFlat)current).ParentOks?.ObjectType?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.AssignationBuildingAttributeIdValue, current => ((xmlObjectFlat)current).ParentOks?.AssignationBuilding?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.AssignationNameAttributeIdValue, current => ((xmlObjectFlat)current).ParentOks?.AssignationName);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.WallMaterialAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectFlat)current).ParentOks?.Walls));
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.ExploitationCharYearBuiltAttributeIdValue, current => ((xmlObjectFlat)current).ParentOks?.Years?.Year_Built);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.ExploitationCharYearUsedAttributeIdValue, current => ((xmlObjectFlat)current).ParentOks?.Years?.Year_Used);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.FloorCountAttributeIdValue, current => ((xmlObjectFlat)current).ParentOks?.Floors?.Floors);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ParentOks?.FloorUndergroundCountAttributeIdValue, current => ((xmlObjectFlat)current).ParentOks?.Floors?.Underground_Floors);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.NameAttributeIdValue, current => ((xmlObjectFlat)current).Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.AssignationAssignationCodeAttributeIdValue, current => ((xmlObjectFlat)current).AssignationFlatCode?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.AssignationAssignationTypeAttributeIdValue, current => ((xmlObjectFlat)current).AssignationFlatType?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.AssignationSpecialTypeAttributeIdValue, current => ((xmlObjectFlat)current).AssignationSpecialType?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.AssignationTotalAssetsAttributeIdValue, current => ((xmlObjectFlat)current).AssignationTotalAssets);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.AssignationAuxiliaryFlatAttributeIdValue, current => ((xmlObjectFlat)current).AssignationAuxiliaryFlat);
			
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.AreaAttributeIdValue, current => ((xmlObjectFlat)current).Area);
			
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.PositionNumberOnPlanAttributeIdValue, current => ((xmlObjectFlat)current).Position?.NumberOnPlan);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.PositionDescriptionAttributeIdValue, current => ((xmlObjectFlat)current).Position?.Description);

			if (DataImporterGknConfig.GknDataAttributes.Flat.Levels.Length > 0)
			{
				for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Flat.Levels.Length; i++)
				{
					var level = DataImporterGknConfig.GknDataAttributes.Flat.Levels[i];
					var iCounter = i;

					TryAddGknDataAttribute(level.NumberAttributeIdValue, current => ((xmlObjectFlat)current).Levels[iCounter].Number,
						current => ((xmlObjectFlat)current).Levels.Count >= iCounter + 1);
					TryAddGknDataAttribute(level.TypeAttributeIdValue, current => ((xmlObjectFlat)current).Levels[iCounter].Type,
						current => ((xmlObjectFlat)current).Levels.Count >= iCounter + 1);
					TryAddGknDataAttribute(level.PositionNumberOnPlanAttributeIdValue, current => ((xmlObjectFlat)current).Levels[iCounter].Position?.NumberOnPlan,
						current => ((xmlObjectFlat)current).Levels.Count >= iCounter + 1);
					TryAddGknDataAttribute(level.PositionDescriptionAttributeIdValue, current => ((xmlObjectFlat)current).Levels[iCounter].Position?.Description,
						current => ((xmlObjectFlat)current).Levels.Count >= iCounter + 1);
				}
			}



			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.ObjectPermittedUsesAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectFlat)current).ObjectPermittedUses));

			if (DataImporterGknConfig.GknDataAttributes.Flat.SubFlats.Length > 0)
			{
				for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Flat.SubFlats.Length; i++)
				{
					var subFlat = DataImporterGknConfig.GknDataAttributes.Flat.SubFlats[i];
					var iCounter = i;
					TryAddGknDataAttribute(subFlat.AreaAttributeIdValue, current => ((xmlObjectFlat)current).SubFlats[iCounter].Area,
						current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1);

					for (int j = 0; j < subFlat.Encumbrances.Length; j++)
					{
						var encumbrance = subFlat.Encumbrances[j];
						var jCounter = j;
						TryAddGknDataAttribute(encumbrance.NameAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Name,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.TypeAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Type?.Name,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.RegistrationNumberAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Registration?.Number,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.RegistrationDateAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Registration?.Date,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.CodeAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Document?.CodeDocument?.Name,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.NameAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Document?.Name,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.SeriesAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Document?.Series,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.NumberAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Document?.Number,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.DateAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Document?.Date,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.IssueOrganAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Document?.IssueOrgan,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.DescAttributeIdValue,
							current => ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks[jCounter].Document?.Desc,
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
					}

					TryAddGknDataAttribute(subFlat.NumberRecordAttributeIdValue, current => ((xmlObjectFlat)current).SubFlats[iCounter].NumberRecord,
						current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1);
					TryAddGknDataAttribute(subFlat.DateCreatedAttributeIdValue, current => ((xmlObjectFlat)current).SubFlats[iCounter].DateCreated,
						current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1);
				}
			}

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.UnitedCadastralNumberAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectFlat)current).UnitedCadastralNumbers));

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.FacilityCadastralNumberAttributeIdValue, current => ((xmlObjectFlat)current).FacilityCadastralNumber);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.FacilityPurposeAttributeIdValue, current => ((xmlObjectFlat)current).FacilityPurpose);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.EgroknRegNumAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.EgroknRegNum);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.EgroknObjCulturalAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.EgroknObjCultural?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.EgroknNameCulturalAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.EgroknNameCultural);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.RequirementsEnsureAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.RequirementsEnsure);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.Document?.CodeAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.CodeDocument?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.Document?.NameAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.Document?.SeriesAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Series);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.Document?.NumberAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Number);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.Document?.DateAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Date);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.Document?.IssueOrganAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.IssueOrgan);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Flat.CulturalHeritage?.Document?.DescAttributeIdValue, current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Desc);
		}

		protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectFlat current)
		{
			koUnit.BuildingCadastralNumber = current.CadastralNumberOKS;
		}
    }
}
