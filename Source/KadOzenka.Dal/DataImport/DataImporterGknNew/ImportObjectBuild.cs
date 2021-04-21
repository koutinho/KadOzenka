using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectBuild : ImportObjectBase<xmlObjectBuild>
	{
		private long? _ftWallInheritanceFactorId;
		private long? _ftYearInheritanceFactorId;

        public override PropertyTypes PropertyType => PropertyTypes.Building;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Зданий";
        public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Зданий";
        public override string SuccessMessage => "Импорт Зданий завершен";

        public ImportObjectBuild(DateTime unitDate, OMTask task, 
	        Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction) 
	        : base(unitDate, task, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
        {
	        InitFactorInheritanceSettings();
        }

        private void InitFactorInheritanceSettings()
        {
	        var factorSettings = OMFactorSettings
		        .Where(x => x.Inheritance_Code == FactorInheritance.ftWall || x.Inheritance_Code == FactorInheritance.ftYear)
		        .Select(x => new { x.Inheritance_Code, x.FactorId})
		        .Execute();

	        if (factorSettings.Any(x => x.Inheritance_Code == FactorInheritance.ftWall))
		        _ftWallInheritanceFactorId = factorSettings.First(x => x.Inheritance_Code == FactorInheritance.ftWall).FactorId;

	        if (factorSettings.Any(x => x.Inheritance_Code == FactorInheritance.ftYear))
		        _ftYearInheritanceFactorId = factorSettings.First(x => x.Inheritance_Code == FactorInheritance.ftYear).FactorId;
        }

        protected override void  InitAttributeChangeStatusList()
        {
            base.InitAttributeChangeStatusList();
            AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.SquareAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.Assignment, new ImportedAttribute(Consts.BuildingPurposeAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(Consts.YearOfBuildAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(Consts.YearOfUseAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(Consts.FloorCountAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(Consts.FloorUndergroundCountAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ObjectNameAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(Consts.WallMaterialAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.NumberParcel, new ImportedAttribute(Consts.ParcelAttributeId));
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
            if (prTypeObjectCheck && unitChangesDictionary[KoChangeStatus.Name]
                                  && unitChangesDictionary[KoChangeStatus.Assignment])
            {
                #region Наследование группы и подгруппы предыдущего объекта

                koUnit.GroupId = lastUnit.GroupId;
                koUnit.Save();

                #endregion
            }

            //Если материал стен не поменялся
            if (_ftWallInheritanceFactorId.HasValue)
            {
	            //Если в предыдущем объекте есть фактор Материал стен итоговый
                //его надо скопировать в новый объект, если нет, добавить надо.
                koUnit.AddKOFactor(_ftWallInheritanceFactorId.Value,
                    (unitChangesDictionary[KoChangeStatus.Walls])
                        ? lastUnit
                        : null, string.Empty);
            }

            //Если год ввода в эксплуатацию и год завершения строительства  не поменялся
            if (_ftYearInheritanceFactorId.HasValue)
            {
                //Если в предыдущем объекте есть фактор Год постройки итоговый
                //его надо скопировать в новый объект, если нет, добавить надо.
                koUnit.AddKOFactor(_ftYearInheritanceFactorId.Value,
                    (unitChangesDictionary[KoChangeStatus.YearUse] && unitChangesDictionary[KoChangeStatus.YearBuild])
                        ? lastUnit
                        : null, string.Empty);
            }
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
	        TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.ParentCadastralNumbersAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectBuild)current).ParentCadastralNumbers));
	        TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.NameAttributeIdValue, current => ((xmlObjectBuild)current).Name);
	        TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.AssignationBuildingAttributeIdValue, current => ((xmlObjectBuild)current).AssignationBuilding?.Name);
	        TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.WallMaterialAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectBuild)current).Walls));

	        TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.ExploitationCharYearBuiltAttributeIdValue, current => ((xmlObjectBuild)current).Years?.Year_Built);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.ExploitationCharYearUsedAttributeIdValue, current => ((xmlObjectBuild)current).Years?.Year_Used);

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.FloorCountAttributeIdValue, current => ((xmlObjectBuild)current).Floors?.Floors);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.FloorUndergroundCountAttributeIdValue, current => ((xmlObjectBuild)current).Floors?.Underground_Floors);

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.AreaAttributeIdValue, current => ((xmlObjectBuild)current).Area);

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.ObjectPermittedUsesAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectBuild)current).ObjectPermittedUses));
            
            if(DataImporterGknConfig.GknDataAttributes.Building.SubBuildings.Length > 0)
            {
	            for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Building.SubBuildings.Length; i++)
	            {
		            var subBuilding = DataImporterGknConfig.GknDataAttributes.Building.SubBuildings[i];
		            var iCounter = i;
		            TryAddGknDataAttribute(subBuilding.AreaAttributeIdValue, current => ((xmlObjectBuild)current).SubBuildings[iCounter].Area,
	                    current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1);

		            for (int j = 0; j < subBuilding.Encumbrances.Length; j++)
		            {
			            var encumbrance = subBuilding.Encumbrances[j];
			            var jCounter = j;
			            TryAddGknDataAttribute(encumbrance.NameAttributeIdValue,
				            current => ((xmlObjectBuild) current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Name,
				            current => ((xmlObjectBuild) current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.TypeAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Type?.Name,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.RegistrationNumberAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Registration?.Number,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.RegistrationDateAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Registration?.Date,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.Document?.CodeAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Document?.CodeDocument?.Name,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.Document?.NameAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Document?.Name,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.Document?.SeriesAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Document?.Series,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.Document?.NumberAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Document?.Number,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.Document?.DateAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Document?.Date,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.Document?.IssueOrganAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Document?.IssueOrgan,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
			            TryAddGknDataAttribute(encumbrance.Document?.DescAttributeIdValue,
				            current => ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks[jCounter].Document?.Desc,
				            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
		            }

		            TryAddGknDataAttribute(subBuilding.NumberRecordAttributeIdValue, current => ((xmlObjectBuild)current).SubBuildings[iCounter].NumberRecord,
			            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1);
		            TryAddGknDataAttribute(subBuilding.DateCreatedAttributeIdValue, current => ((xmlObjectBuild)current).SubBuildings[iCounter].DateCreated,
			            current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1);
                }
            }

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.FlatsCadastralNumbersAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectBuild)current).FlatsCadastralNumbers));
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CarParkingSpacesCadastralNumbersAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectBuild)current).CarParkingSpacesCadastralNumbers));
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.UnitedCadastralNumberAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectBuild)current).UnitedCadastralNumbers));
            
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.FacilityCadastralNumberAttributeIdValue, current => ((xmlObjectBuild)current).FacilityCadastralNumber);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.FacilityPurposeAttributeIdValue, current => ((xmlObjectBuild)current).FacilityPurpose);

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.EgroknRegNumAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.EgroknRegNum);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.EgroknObjCulturalAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.EgroknObjCultural?.Name);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.EgroknNameCulturalAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.EgroknNameCultural);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.RequirementsEnsureAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.RequirementsEnsure);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.Document?.CodeAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.CodeDocument?.Name);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.Document?.NameAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Name);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.Document?.SeriesAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Series);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.Document?.NumberAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Number);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.Document?.DateAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Date);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.Document?.IssueOrganAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.IssueOrgan);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Building.CulturalHeritage?.Document?.DescAttributeIdValue, current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Desc);
        }

        protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectBuild current)
        {
	        koUnit.Square = current.Area.ParseToDecimal();
        }
	}
}
