using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectConstruction : ImportObjectBase<xmlObjectConstruction>
    {
	    private long? _ftYearInheritanceFactorId;

        public override PropertyTypes PropertyType => PropertyTypes.Construction;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Сооружений";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Сооружений";
		public override string SuccessMessage => "Импорт Сооружений завершен";

		public ImportObjectConstruction(DateTime unitDate, OMTask task,
			Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction) 
            : base(unitDate, task, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
		{
			InitFactorInheritanceSettings();
        }

		private void InitFactorInheritanceSettings()
		{
			var factorSettings = OMFactorSettings
				.Where(x => x.Inheritance_Code == FactorInheritance.ftYear)
				.Select(x => x.FactorId)
				.ExecuteFirstOrDefault();

			if (factorSettings!= null)
				_ftYearInheritanceFactorId = factorSettings.FactorId;
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.PlacementCharacteristicAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Assignment, new ImportedAttribute(Consts.ConstructionPurposeAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(Consts.YearOfBuildAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(Consts.YearOfUseAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(Consts.FloorCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(Consts.FloorUndergroundCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ObjectNameAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.NumberParcel, new ImportedAttribute(Consts.ParcelAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(Consts.WallMaterialAttributeId));
		}

        protected override void InitTaskFormingAttributes()
		{
			base.InitTaskFormingAttributes();
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

			//Если год ввода в эксплуатацию и год завершения строительства  не поменялся
			if (_ftYearInheritanceFactorId.HasValue)
			{
				//Если в предыдущем объекте есть фактор Год постройки итоговый
				//его надо скопировать в новый объект, если нет, добавить надо.
				koUnit.AddKOFactor(_ftYearInheritanceFactorId.Value, (unitChangesDictionary[KoChangeStatus.YearUse]
				                                                && unitChangesDictionary[KoChangeStatus.YearBuild]) ? lastUnit : null, string.Empty);
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
			if (!unitChangesDictionary[KoChangeStatus.YearBuild] || !unitChangesDictionary[KoChangeStatus.YearUse])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P4YearOfBuildAttributeId), true, gbuObjectId);
			}
		}


        protected override void InitGknDataAttributes()
        {
            base.InitGknDataAttributes();
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.ParentCadastralNumbersAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectConstruction)current).ParentCadastralNumbers));
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.NameAttributeIdValue, current => ((xmlObjectConstruction)current).Name);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.AssignationNameAttributeIdValue, current => ((xmlObjectConstruction)current).AssignationName);

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.ExploitationCharYearBuiltAttributeIdValue, current => ((xmlObjectConstruction)current).Years?.Year_Built);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.ExploitationCharYearUsedAttributeIdValue, current => ((xmlObjectConstruction)current).Years?.Year_Used);

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.FloorCountAttributeIdValue, current => ((xmlObjectConstruction)current).Floors?.Floors);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.FloorUndergroundCountAttributeIdValue, current => ((xmlObjectConstruction)current).Floors?.Underground_Floors);
            
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.KeyParametersAttributeIdValue, current => xmlCodeNameValue.GetNames(((xmlObjectConstruction)current).KeyParameters));
			if (DataImporterGknConfig.GknDataAttributes.Construction.KeyParameters.Length > 0)
			{
				for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Construction.KeyParameters.Length; i++)
				{
					var keyParameter = DataImporterGknConfig.GknDataAttributes.Construction.KeyParameters[i];
					var iCounter = i;
					TryAddGknDataAttribute(keyParameter.KeyParameterAttributeIdValue, current => ((xmlObjectConstruction)current).KeyParameters[iCounter]?.Name,
						current => ((xmlObjectConstruction)current).KeyParameters.Count >= iCounter + 1);
					TryAddGknDataAttribute(keyParameter.KeyParameterValueAttributeIdValue, current => ((xmlObjectConstruction)current).KeyParameters[iCounter]?.Value,
						current => ((xmlObjectConstruction)current).KeyParameters.Count >= iCounter + 1);
				}
			}

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.ObjectPermittedUsesAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectConstruction)current).ObjectPermittedUses));

			if (DataImporterGknConfig.GknDataAttributes.Construction.SubConstructions.Length > 0)
			{
				for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Construction.SubConstructions.Length; i++)
				{
					var subConstruction = DataImporterGknConfig.GknDataAttributes.Construction.SubConstructions[i];
					var iCounter = i;
					TryAddGknDataAttribute(subConstruction.KeyParameter?.KeyParameterAttributeIdValue, current => ((xmlObjectConstruction)current).SubConstructions[iCounter].KeyParameter?.Name,
						current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1);
					TryAddGknDataAttribute(subConstruction.KeyParameter?.KeyParameterValueAttributeIdValue, current => ((xmlObjectConstruction)current).SubConstructions[iCounter].KeyParameter?.Value,
						current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1);

					for (int j = 0; j < subConstruction.Encumbrances.Length; j++)
					{
						var encumbrance = subConstruction.Encumbrances[j];
						var jCounter = j;
						TryAddGknDataAttribute(encumbrance.NameAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Name,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.TypeAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Type?.Name,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.RegistrationNumberAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Registration?.Number,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.RegistrationDateAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Registration?.Date,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.CodeAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Document?.CodeDocument?.Name,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.NameAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Document?.Name,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.SeriesAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Document?.Series,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.NumberAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Document?.Number,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.DateAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Document?.Date,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.IssueOrganAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Document?.IssueOrgan,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.DescAttributeIdValue,
							current => ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks[jCounter].Document?.Desc,
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
					}

					TryAddGknDataAttribute(subConstruction.NumberRecordAttributeIdValue, current => ((xmlObjectConstruction)current).SubConstructions[iCounter].NumberRecord,
						current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1);
					TryAddGknDataAttribute(subConstruction.DateCreatedAttributeIdValue, current => ((xmlObjectConstruction)current).SubConstructions[iCounter].DateCreated,
						current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1);
				}
			}


			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.FlatsCadastralNumbersAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectConstruction)current).FlatsCadastralNumbers));
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CarParkingSpacesCadastralNumbersAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectConstruction)current).CarParkingSpacesCadastralNumbers));
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.UnitedCadastralNumberAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectConstruction)current).UnitedCadastralNumbers));

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.FacilityCadastralNumberAttributeIdValue, current => ((xmlObjectConstruction)current).FacilityCadastralNumber);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.FacilityPurposeAttributeIdValue, current => ((xmlObjectConstruction)current).FacilityPurpose);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.EgroknRegNumAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.EgroknRegNum);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.EgroknObjCulturalAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.EgroknObjCultural?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.EgroknNameCulturalAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.EgroknNameCultural);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.RequirementsEnsureAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.RequirementsEnsure);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.Document?.CodeAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.CodeDocument?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.Document?.NameAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.Document?.SeriesAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Series);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.Document?.NumberAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Number);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.Document?.DateAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Date);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.Document?.IssueOrganAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.IssueOrgan);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Construction.CulturalHeritage?.Document?.DescAttributeIdValue, current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Desc);
        }
	}
}
