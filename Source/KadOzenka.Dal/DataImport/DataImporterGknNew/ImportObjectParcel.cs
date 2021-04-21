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
	public class ImportObjectParcel : ImportObjectBase<xmlObjectParcel>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.Stead;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Участков";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Участков";
		public override string SuccessMessage => "Импорт Участков завершен";

		public ImportObjectParcel(DateTime unitDate, OMTask task,
			Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction) 
			: base(unitDate, task, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
		{
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ParcelNameAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.SquareAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Category, new ImportedAttribute(Consts.LandCategoryAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Use, new ImportedAttribute(Consts.TypeOfUseByDocumentsAttributeId));
		}

        protected override void DoInheritanceFromPrevUnit(OMUnit lastUnit, OMUnit koUnit, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			//Признак не поменялся ли тип объекта?
			bool prTypeObjectCheck = lastUnit.PropertyType_Code == koUnit.PropertyType_Code;

			//Если не было изменений типа, наименования и назначения и не было обращения
			if (prTypeObjectCheck && unitChangesDictionary[KoChangeStatus.Name]
                && unitChangesDictionary[KoChangeStatus.Use])
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
		}

        protected override void InitGknDataAttributes()
        {
            base.InitGknDataAttributes();

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.LocationInBoundsAttributeIdValue, current => current.Adress?.InBounds);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.LocationPlacedAttributeIdValue, current => current.Adress?.Placed);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.LocationElaborationReferenceMarkAttributeIdValue, current => current.Adress?.Elaboration?.ReferenceMark);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.LocationElaborationDistanceAttributeIdValue, current => current.Adress?.Elaboration?.Distance);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.LocationElaborationDirectionAttributeIdValue, current => current.Adress?.Elaboration?.Direction);

            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.NameAttributeIdValue, current => ((xmlObjectParcel)current).Name?.Name);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.InnerCadastralNumbersAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectParcel)current).InnerCadastralNumbers));
            
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.AreaAttributeIdValue, current => ((xmlObjectParcel)current).Area);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.AreaInaccuracyAttributeIdValue, current => ((xmlObjectParcel)current).AreaInaccuracy);
            
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.CategoryAttributeIdValue, current => ((xmlObjectParcel)current).Category?.Name);
            
            
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.UtilizationUtilizationAttributeIdValue, current => ((xmlObjectParcel)current).Utilization?.Utilization?.Name);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.UtilizationByDocAttributeIdValue, current => ((xmlObjectParcel)current).Utilization?.ByDoc);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.UtilizationLandUseAttributeIdValue, current => ((xmlObjectParcel)current).Utilization?.LandUse?.Name);
            TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.UtilizationPermittedUseTextAttributeIdValue, current => ((xmlObjectParcel)current).Utilization?.PermittedUseText);

            if (DataImporterGknConfig.GknDataAttributes.Parcel.NaturalObjects.Length > 0)
            {
	            for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Parcel.NaturalObjects.Length; i++)
	            {
		            var naturalObject = DataImporterGknConfig.GknDataAttributes.Parcel.NaturalObjects[i];
		            var iCounter = i;

		            TryAddGknDataAttribute(naturalObject.KindAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].Kind?.Name,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
		            TryAddGknDataAttribute(naturalObject.ForestryAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].Forestry,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
		            TryAddGknDataAttribute(naturalObject.ForestUseAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].ForestUse?.Name,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
		            TryAddGknDataAttribute(naturalObject.QuarterNumbersAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].QuarterNumbers,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
		            TryAddGknDataAttribute(naturalObject.TaxationSeparationsAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].TaxationSeparations,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
		            TryAddGknDataAttribute(naturalObject.ProtectiveForestAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].ProtectiveForest?.Name,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

		            for (int j = 0; j < naturalObject.ForestEncumbrances.Length; j++)
		            {
			            var forestEncumbrance = naturalObject.ForestEncumbrances[j];
			            var jCounter = j;
			            TryAddGknDataAttribute(forestEncumbrance.ForestEncumbranceAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].ForestEncumbrances[jCounter]?.Name,
				            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1 && ((xmlObjectParcel)current).NaturalObjects[iCounter].ForestEncumbrances.Count >= jCounter + 1);
					}

		            TryAddGknDataAttribute(naturalObject.WaterObjectAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].WaterObject,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
		            TryAddGknDataAttribute(naturalObject.NameOtherAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].NameOther,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
		            TryAddGknDataAttribute(naturalObject.CharOtherAttributeIdValue, current => ((xmlObjectParcel)current).NaturalObjects[iCounter].CharOther,
			            current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
				}
            }

			if (DataImporterGknConfig.GknDataAttributes.Parcel.SubParcels.Length > 0)
			{
				for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Parcel.SubParcels.Length; i++)
				{
					var subParcel = DataImporterGknConfig.GknDataAttributes.Parcel.SubParcels[i];
					var iCounter = i;
					TryAddGknDataAttribute(subParcel.AreaAttributeIdValue, current => ((xmlObjectParcel)current).SubParcels[iCounter].Area,
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);
					TryAddGknDataAttribute(subParcel.AreaInaccuracyAttributeIdValue, current => ((xmlObjectParcel)current).SubParcels[iCounter].AreaInaccuracy,
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);

					for (int j = 0; j < subParcel.Encumbrances.Length; j++)
					{
						var encumbrance = subParcel.Encumbrances[j];
						var jCounter = j;
						TryAddGknDataAttribute(encumbrance.NameAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Name,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.TypeAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Type?.Name,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.AccountNumberAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].AccountNumber,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.CadastralNumberRestrictionAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].CadastralNumberRestriction,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.RegistrationNumberAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Registration?.Number,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.RegistrationDateAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Registration?.Date,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.CodeAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Document?.CodeDocument?.Name,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.NameAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Document?.Name,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.SeriesAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Document?.Series,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.NumberAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Document?.Number,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.DateAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Document?.Date,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.IssueOrganAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Document?.IssueOrgan,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
						TryAddGknDataAttribute(encumbrance.Document?.DescAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances[jCounter].Document?.Desc,
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
					}

					TryAddGknDataAttribute(subParcel.NumberRecordAttributeIdValue, current => ((xmlObjectParcel)current).SubParcels[iCounter].NumberRecord,
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);
					TryAddGknDataAttribute(subParcel.DateCreatedAttributeIdValue, current => ((xmlObjectParcel)current).SubParcels[iCounter].DateCreated,
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);
				}
			}

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.FacilityCadastralNumberAttributeIdValue, current => ((xmlObjectParcel)current).FacilityCadastralNumber);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.FacilityPurposeAttributeIdValue, current => ((xmlObjectParcel)current).FacilityPurpose);

			if (DataImporterGknConfig.GknDataAttributes.Parcel.ZonesAndTerritories.Length > 0)
			{
				for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Parcel.ZonesAndTerritories.Length; i++)
				{
					var zoneAndTerritory = DataImporterGknConfig.GknDataAttributes.Parcel.ZonesAndTerritories[i];
					var iCounter = i;
					TryAddGknDataAttribute(zoneAndTerritory.DescriptionAttributeIdValue, current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].Description,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.CodeZoneDocAttributeIdValue, current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].CodeZoneDoc,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.AccountNumberAttributeIdValue, current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].AccountNumber,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.ContentRestrictionsAttributeIdValue, current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].ContentRestrictions,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.FullPartlyAttributeIdValue, current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].FullPartly,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.Document?.CodeAttributeIdValue,
							current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].Document?.CodeDocument?.Name,
							current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.Document?.NameAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].Document?.Name,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.Document?.SeriesAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].Document?.Series,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.Document?.NumberAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].Document?.Number,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.Document?.DateAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].Document?.Date,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.Document?.IssueOrganAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].Document?.IssueOrgan,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
					TryAddGknDataAttribute(zoneAndTerritory.Document?.DescAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories[iCounter].Document?.Desc,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
				}
			}

			if (DataImporterGknConfig.GknDataAttributes.Parcel.GovernmentLandSupervision.Length > 0)
			{
				for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Parcel.GovernmentLandSupervision.Length; i++)
				{
					var supervisionEvent = DataImporterGknConfig.GknDataAttributes.Parcel.GovernmentLandSupervision[i];
					var iCounter = i;
					TryAddGknDataAttribute(supervisionEvent.AgencyAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].Agency,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EventNameAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EventName?.Name,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EventFormAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EventForm?.Name,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.InspectionEndAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].InspectionEnd,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.AvailabilityViolationsAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].AvailabilityViolations,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.IdentifiedViolationsTypeViolationsAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].IdentifiedViolations?.TypeViolations,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.IdentifiedViolationsSignViolationsAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].IdentifiedViolations?.SignViolations,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.IdentifiedViolationsAreaAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].IdentifiedViolations?.Area,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					TryAddGknDataAttribute(supervisionEvent.DocRequisites?.CodeAttributeIdValue,
							current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].DocRequisites?.CodeDocument?.Name,
							current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.DocRequisites?.NameAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].DocRequisites?.Name,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.DocRequisites?.SeriesAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].DocRequisites?.Series,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.DocRequisites?.NumberAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].DocRequisites?.Number,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.DocRequisites?.DateAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].DocRequisites?.Date,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.DocRequisites?.IssueOrganAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].DocRequisites?.IssueOrgan,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.DocRequisites?.DescAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].DocRequisites?.Desc,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					TryAddGknDataAttribute(supervisionEvent.EliminationMarkAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].Elimination?.EliminationMark,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EliminationAgencyAttributeIdValue, current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].Elimination?.EliminationAgency,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					TryAddGknDataAttribute(supervisionEvent.EliminationDocRequisites?.CodeAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EliminationDocRequisites?.CodeDocument?.Name,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EliminationDocRequisites?.NameAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EliminationDocRequisites?.Name,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EliminationDocRequisites?.SeriesAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EliminationDocRequisites?.Series,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EliminationDocRequisites?.NumberAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EliminationDocRequisites?.Number,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EliminationDocRequisites?.DateAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EliminationDocRequisites?.Date,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EliminationDocRequisites?.IssueOrganAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EliminationDocRequisites?.IssueOrgan,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
					TryAddGknDataAttribute(supervisionEvent.EliminationDocRequisites?.DescAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision[iCounter].EliminationDocRequisites?.Desc,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
				}
			}

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.SurveyingProjectNumAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectNum);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.SurveyingProjectDecisionRequisites?.CodeAttributeIdValue,
						current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.CodeDocument?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.SurveyingProjectDecisionRequisites?.NameAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.SurveyingProjectDecisionRequisites?.SeriesAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Series);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.SurveyingProjectDecisionRequisites?.NumberAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Number);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.SurveyingProjectDecisionRequisites?.DateAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Date);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.SurveyingProjectDecisionRequisites?.IssueOrganAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.IssueOrgan);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.SurveyingProjectDecisionRequisites?.DescAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Desc);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.UseHiredHouseAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.UseHiredHouse);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.ActBuildingAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ActBuilding);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.ActDevelopmentAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ActDevelopment);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.ContractBuildingAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ContractBuilding);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.ContractDevelopmentAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ContractDevelopment);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.OwnerDecisionAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.OwnerDecision);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.ContractSupportAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ContractSupport);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.DocHiredHouse?.CodeAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.CodeDocument?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.DocHiredHouse?.NameAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Name);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.DocHiredHouse?.SeriesAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Series);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.DocHiredHouse?.NumberAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Number);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.DocHiredHouse?.DateAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Date);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.DocHiredHouse?.IssueOrganAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.IssueOrgan);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.HiredHouse?.DocHiredHouse?.DescAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Desc);

			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Parcel.LimitedCirculationAttributeIdValue,
				current => ((xmlObjectParcel)current).LimitedCirculation);
        }

        protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectParcel current)
        {
	        koUnit.Square = current.Area.ParseToDecimal();
        }
    }
}
