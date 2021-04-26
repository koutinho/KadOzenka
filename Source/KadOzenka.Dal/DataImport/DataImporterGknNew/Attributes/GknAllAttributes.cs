using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using KadOzenka.Dal.Extentions;
using KadOzenka.Dal.XmlParser;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes
{
	public class GknAllAttributes
	{
		private DataImporterGknConfig Config { get; }

		private List<ImportedAttributeGkn> General { get; }
		public List<ImportedAttributeGkn> Parcel { get; }
		public List<ImportedAttributeGkn> All { get; }


		public GknAllAttributes()
		{
			Config = ConfigurationManagers.ConfigurationManager.KoConfig.DataImporterGknConfig;

			General = new List<ImportedAttributeGkn>();
			Parcel = new List<ImportedAttributeGkn>();
			All = new List<ImportedAttributeGkn>();

			FillGeneralAttribute();
			FillParcelAttribute();

			Parcel.AddRange(General);
			All.AddRange(Parcel);
		}



		#region Support Methods

		private void FillGeneralAttribute()
		{
			var generalSection = Config.GknDataAttributes.General;

			AddAttributeToGeneral(generalSection.ObjectTypeAttributeIdValue, current => current.TypeRealty,
				(o, v) => o.TypeRealty = v?.ToString());

			AddAttributeToGeneral(generalSection.CadastralNumberAttributeIdValue, current => current.CadastralNumber,
				(o, v) => o.CadastralNumber = v?.ToString());

			AddAttributeToGeneral(generalSection.DateCreatedAttributeIdValue, current => current.DateCreate,
				(o, v) => o.DateCreate = (DateTime?)v);

			AddAttributeToGeneral(generalSection.CadastralBlockAttributeIdValue,
				current => current.CadastralNumberBlock, (o, v) => o.CadastralNumberBlock = v?.ToString());

			#region Cadastral Cost Block

			AddAttributeToGeneral(generalSection.CadastralCostValueAttributeIdValue,
				current => current.CadastralCost?.Value, (o, v) => o.CadastralCost.Value = v?.ParseToDouble());

			AddAttributeToGeneral(generalSection.CadastralCostDateValuationAttributeIdValue,
				current => current.CadastralCost?.DateValuation, (o, v) => o.CadastralCost.DateValuation = (DateTime?)v);

			AddAttributeToGeneral(generalSection.CadastralCostDateEnteringAttributeIdValue,
				current => current.CadastralCost?.DateEntering, (o, v) => o.CadastralCost.DateEntering = (DateTime?)v);

			AddAttributeToGeneral(generalSection.CadastralCostDateApprovalAttributeIdValue,
				current => current.CadastralCost?.DateApproval, (o, v) => o.CadastralCost.DateApproval = (DateTime?)v);

			AddAttributeToGeneral(generalSection.CadastralCostDocNumberAttributeIdValue,
				current => current.CadastralCost?.DocNumber, (o, v) => o.CadastralCost.DocNumber = v?.ToString());

			AddAttributeToGeneral(generalSection.CadastralCostDocDateAttributeIdValue,
				current => current.CadastralCost?.DocDate, (o, v) => o.CadastralCost.DocDate = (DateTime?)v);

			AddAttributeToGeneral(generalSection.CadastralCostApplicationDateAttributeIdValue,
				current => current.CadastralCost?.ApplicationDate, (o, v) => o.CadastralCost.ApplicationDate = (DateTime?)v);

			AddAttributeToGeneral(generalSection.CadastralCostRevisalStatementDateAttributeIdValue,
				current => current.CadastralCost?.RevisalStatementDate, (o, v) => o.CadastralCost.RevisalStatementDate = (DateTime?)v);

			AddAttributeToGeneral(generalSection.CadastralCostApplicationLastDateAttributeIdValue,
				current => current.CadastralCost?.ApplicationLastDate, (o, v) => o.CadastralCost.ApplicationLastDate = (DateTime?)v);

			AddAttributeToGeneral(generalSection.CadastralCostDocNameAttributeIdValue,
				current => current.CadastralCost?.DocName, (o, v) => o.CadastralCost.DocName = v?.ToString());

			#endregion

			#region Address

			AddAttributeToGeneral(generalSection.LocationAddressInOneStringAttributeIdValue, 
				current => xmlAdress.GetTextAdress(current.Adress), (o, v) => o.Adress.AddressStr = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationFiasAttributeIdValue,
				current => current.Adress?.FIAS, (o, v) => o.Adress.FIAS = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationOkatoAttributeIdValue, 
				current => current.Adress?.OKATO, (o, v) => o.Adress.OKATO = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationKladrAttributeIdValue, 
				current => current.Adress?.KLADR, (o, v) => o.Adress.KLADR = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationOktmoAttributeIdValue, 
				current => current.Adress?.OKTMO, (o, v) => o.Adress.OKTMO = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationPostalCodeAttributeIdValue, 
				current => current.Adress?.PostalCode, (o, v) => o.Adress.PostalCode = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationRussianFederationAttributeIdValue, 
				current => current.Adress?.RussianFederation, (o, v) => o.Adress.RussianFederation = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationRegionAttributeIdValue, 
				current => current.Adress?.Region, (o, v) => o.Adress.Region = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationDistrictAttributeIdValue, 
				current => current.Adress?.District?.Value, (o, v) => o.Adress.District.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationCityAttributeIdValue, 
				current => current.Adress?.City?.Value, (o, v) => o.Adress.City.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationUrbanDistrictAttributeIdValue, 
				current => current.Adress?.UrbanDistrict?.Value, (o, v) => o.Adress.UrbanDistrict.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationSovietVillageAttributeIdValue, 
				current => current.Adress?.SovietVillage?.Value, (o, v) => o.Adress.SovietVillage.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationLocalityAttributeIdValue, 
				current => current.Adress?.Locality?.Value, (o, v) => o.Adress.Locality.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationPlanningElementAttributeIdValue, 
				current => current.Adress?.PlanningElement?.Value, (o, v) => o.Adress.PlanningElement.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationStreetAttributeIdValue, 
				current => current.Adress?.Street?.Value, (o, v) => o.Adress.Street.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationLevel1AttributeIdValue, 
				current => current.Adress?.Level1?.Value, (o, v) => o.Adress.Level1.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationLevel2AttributeIdValue, 
				current => current.Adress?.Level2?.Value, (o, v) => o.Adress.Level2.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationLevel3AttributeIdValue, 
				current => current.Adress?.Level3?.Value, (o, v) => o.Adress.Level3.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationApartmentAttributeIdValue, 
				current => current.Adress?.Apartment?.Value, (o, v) => o.Adress.Apartment.Value = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationOtherAttributeIdValue, 
				current => current.Adress?.Other, (o, v) => o.Adress.Other = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationNoteAttributeIdValue, 
				current => current.Adress?.Note, (o, v) => o.Adress.Note = v?.ToString());

			AddAttributeToGeneral(generalSection.LocationAddressOrLocationAttributeIdValue, 
				current => current.Adress?.AddressOrLocation, (o, v) => o.Adress.AddressOrLocation = v?.ToString());

			#endregion
		}

		private void FillParcelAttribute()
		{
			var parcelSection = Config.GknDataAttributes.Parcel;

			AddAttributeToParcel(parcelSection.LocationInBoundsAttributeIdValue,
				current => current.Adress?.InBounds, (o, v) => o.Adress.InBounds = v?.ToString());

			AddAttributeToParcel(parcelSection.LocationPlacedAttributeIdValue, 
				current => current.Adress?.Placed, (o, v) => o.Adress.Placed = v?.ToString());

			AddAttributeToParcel(parcelSection.LocationElaborationReferenceMarkAttributeIdValue, 
				current => current.Adress?.Elaboration?.ReferenceMark, (o, v) => o.Adress.Elaboration.ReferenceMark = v?.ToString());
			
			AddAttributeToParcel(parcelSection.LocationElaborationDistanceAttributeIdValue, 
				current => current.Adress?.Elaboration?.Distance, (o, v) => o.Adress.Elaboration.Distance = v?.ToString());
			
			AddAttributeToParcel(parcelSection.LocationElaborationDirectionAttributeIdValue, 
				current => current.Adress?.Elaboration?.Direction, (o, v) => o.Adress.Elaboration.Direction = v?.ToString());

			AddAttributeToParcel(parcelSection.NameAttributeIdValue, 
				current => ((xmlObjectParcel)current).Name?.Name, (o, v) => o.NameParcel.Name = v?.ToString());

			AddAttributeToParcel(parcelSection.InnerCadastralNumbersAttributeIdValue,
				current => xmlCodeName.GetNames(((xmlObjectParcel) current).InnerCadastralNumbers),
				(o, v) => o.InnerCadastralNumbers = new List<string> {v?.ToString()});

			AddAttributeToParcel(parcelSection.AreaAttributeIdValue, 
				current => ((xmlObjectParcel)current).Area, (o, v) => o.Area = v?.ParseToDouble());
			
			AddAttributeToParcel(parcelSection.AreaInaccuracyAttributeIdValue, 
				current => ((xmlObjectParcel)current).AreaInaccuracy, (o, v) => o.AreaInaccuracy = v?.ParseToDouble());

			AddAttributeToParcel(parcelSection.CategoryAttributeIdValue, 
				current => ((xmlObjectParcel)current).Category?.Name, (o, v) => o.Category.Name = v?.ToString());

			AddAttributeToParcel(parcelSection.UtilizationUtilizationAttributeIdValue, 
				current => ((xmlObjectParcel)current).Utilization?.Utilization?.Name,
				(o, v) => o.Utilization.Utilization.Name = v?.ToString());
			
			AddAttributeToParcel(parcelSection.UtilizationByDocAttributeIdValue, 
				current => ((xmlObjectParcel)current).Utilization?.ByDoc,
				(o, v) => o.Utilization.ByDoc = v?.ToString());
			
			AddAttributeToParcel(parcelSection.UtilizationLandUseAttributeIdValue, 
				current => ((xmlObjectParcel)current).Utilization?.LandUse?.Name,
				(o, v) => o.Utilization.LandUse.Name = v?.ToString());
			
			AddAttributeToParcel(parcelSection.UtilizationPermittedUseTextAttributeIdValue, 
				current => ((xmlObjectParcel)current).Utilization?.PermittedUseText,
				(o, v) => o.Utilization.PermittedUseText = v?.ToString());

			var naturalObjectsLength = parcelSection.NaturalObjects.Length;
			if (naturalObjectsLength > 0)
			{
				for (var i = 0; i < naturalObjectsLength; i++)
				{
					var naturalObject = parcelSection.NaturalObjects[i];
					var iCounter = i;

					AddAttributeToParcel(naturalObject.KindAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.Kind?.Name,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.Kind.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.ForestryAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.Forestry,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.Forestry = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.ForestUseAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.ForestUse?.Name,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.ForestUse.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.QuarterNumbersAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.QuarterNumbers,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.QuarterNumbers = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.TaxationSeparationsAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.TaxationSeparations,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.TaxationSeparations = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.ProtectiveForestAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.ProtectiveForest?.Name,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.ProtectiveForest.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					for (int j = 0; j < naturalObject.ForestEncumbrances.Length; j++)
					{
						var forestEncumbrance = naturalObject.ForestEncumbrances[j];
						var jCounter = j;
						AddAttributeToParcel(forestEncumbrance.ForestEncumbranceAttributeIdValue, 
							current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.ForestEncumbrances.ElementAtOrDefault(jCounter)?.Name,
							(o, v) =>
							{
								var element = InitNaturalObject(o, iCounter);

								var forestElement = element.ForestEncumbrances.ElementAtOrDefault(iCounter);
								if (forestElement == null)
								{
									forestElement = new xmlCodeName();
									element.ForestEncumbrances.Insert(iCounter, forestElement);
								}

								forestElement.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1 && ((xmlObjectParcel)current).NaturalObjects[iCounter].ForestEncumbrances.Count >= jCounter + 1);
					}

					AddAttributeToParcel(naturalObject.WaterObjectAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.WaterObject,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.WaterObject = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.NameOtherAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.NameOther,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.NameOther = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.CharOtherAttributeIdValue, 
						current => ((xmlObjectParcel)current).NaturalObjects.ElementAtOrDefault(iCounter)?.CharOther,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.CharOther = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);
				}
			}

			var subParcelsLength = parcelSection.SubParcels.Length;
			if (subParcelsLength > 0)
			{
				for (var i = 0; i < subParcelsLength; i++)
				{
					var subParcel = parcelSection.SubParcels[i];
					var iCounter = i;
					AddAttributeToParcel(subParcel.AreaAttributeIdValue, 
						current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Area,
						(o, v) =>
						{
							var element = InitSubParcelObject(o, iCounter);
							element.Area = v?.ParseToDouble();
						},
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);

					AddAttributeToParcel(subParcel.AreaInaccuracyAttributeIdValue, 
						current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.AreaInaccuracy,
						(o, v) =>
						{
							var element = InitSubParcelObject(o, iCounter);
							element.AreaInaccuracy = v?.ParseToDouble();
						},
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);

					for (int j = 0; j < subParcel.Encumbrances.Length; j++)
					{
						var encumbrance = subParcel.Encumbrances[j];
						var jCounter = j;
						AddAttributeToParcel(encumbrance.NameAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.TypeAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Type?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Type.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.AccountNumberAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.AccountNumber,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.AccountNumber = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.CadastralNumberRestrictionAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.CadastralNumberRestriction,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.CadastralNumberRestriction = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.RegistrationNumberAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Registration?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Registration.Number = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.RegistrationDateAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Registration?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Registration.Date = v.ParseToDateTimeNullable();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.CodeAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Document?.CodeDocument?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.CodeDocument.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.NameAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Document?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.SeriesAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Document?.Series,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Series = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.NumberAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Document?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Number = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.DateAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Document?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Date = v.ParseToDateTimeNullable();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.IssueOrganAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Document?.IssueOrgan,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.IssueOrgan = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.DescAttributeIdValue,
							current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances.ElementAtOrDefault(jCounter)?.Document?.Desc,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Desc = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
					}

					AddAttributeToParcel(subParcel.NumberRecordAttributeIdValue, 
						current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.NumberRecord,
						(o, v) =>
						{
							var element = InitSubParcelObject(o, iCounter);
							element.NumberRecord = v?.ToString();
						},
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);

					AddAttributeToParcel(subParcel.DateCreatedAttributeIdValue, 
						current => ((xmlObjectParcel)current).SubParcels.ElementAtOrDefault(iCounter)?.DateCreated,
						(o, v) =>
						{
							var element = InitSubParcelObject(o, iCounter);
							element.DateCreated = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);
				}
			}

			AddAttributeToParcel(parcelSection.FacilityCadastralNumberAttributeIdValue,
				current => ((xmlObjectParcel)current).FacilityCadastralNumber,
				(o, v) => o.FacilityCadastralNumber = v?.ToString());

			AddAttributeToParcel(parcelSection.FacilityPurposeAttributeIdValue,
				current => ((xmlObjectParcel)current).FacilityPurpose,
				(o, v) => o.FacilityPurpose = v?.ToString());

			var zonesAndTerritoriesLength = parcelSection.ZonesAndTerritories.Length;
			if (zonesAndTerritoriesLength > 0)
			{
				for (var i = 0; i < zonesAndTerritoriesLength; i++)
				{
					var zoneAndTerritory = parcelSection.ZonesAndTerritories[i];
					var iCounter = i;
					AddAttributeToParcel(zoneAndTerritory.DescriptionAttributeIdValue, 
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.Description,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Description = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.CodeZoneDocAttributeIdValue, 
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.CodeZoneDoc,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.CodeZoneDoc = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.AccountNumberAttributeIdValue, 
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.AccountNumber,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.AccountNumber = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.ContentRestrictionsAttributeIdValue, 
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.ContentRestrictions,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.ContentRestrictions = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.FullPartlyAttributeIdValue, 
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.FullPartly,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.FullPartly = v.ParseToBooleanNullable();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.CodeAttributeIdValue,
							current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.Document?.CodeDocument?.Name,
							(o, v) =>
							{
								var element = InitZoneAndTerritoriesObject(o, iCounter);
								element.Document.CodeDocument.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.NameAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.Document?.Name,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.SeriesAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.Document?.Series,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Series = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.NumberAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.Document?.Number,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Number = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.DateAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.Document?.Date,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Date = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.IssueOrganAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.Document?.IssueOrgan,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.IssueOrgan = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.DescAttributeIdValue,
						current => ((xmlObjectParcel)current).ZonesAndTerritories.ElementAtOrDefault(iCounter)?.Document?.Desc,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Desc = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);
				}
			}

			var governmentLandSupervisionLength = parcelSection.GovernmentLandSupervision.Length;
			if (governmentLandSupervisionLength > 0)
			{
				for (var i = 0; i < governmentLandSupervisionLength; i++)
				{
					var supervisionEvent = parcelSection.GovernmentLandSupervision[i];
					var iCounter = i;
					AddAttributeToParcel(supervisionEvent.AgencyAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.Agency,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.Agency = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EventNameAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EventName?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EventName.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EventFormAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EventForm?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EventForm.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.InspectionEndAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.InspectionEnd,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.InspectionEnd = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.AvailabilityViolationsAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.AvailabilityViolations,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.AvailabilityViolations = v.ParseToBooleanNullable();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.IdentifiedViolationsTypeViolationsAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.IdentifiedViolations?.TypeViolations,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.IdentifiedViolations.TypeViolations = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.IdentifiedViolationsSignViolationsAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.IdentifiedViolations?.SignViolations,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.IdentifiedViolations.SignViolations = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.IdentifiedViolationsAreaAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.IdentifiedViolations?.Area,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.IdentifiedViolations.Area = v?.ParseToDouble();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.CodeAttributeIdValue,
							current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.DocRequisites?.CodeDocument?.Name,
							(o, v) =>
							{
								var element = InitGovernmentLandSupervisionObject(o, iCounter);
								element.DocRequisites.CodeDocument.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.NameAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.DocRequisites?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.SeriesAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.DocRequisites?.Series,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Series = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.NumberAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.DocRequisites?.Number,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Number = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.DateAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.DocRequisites?.Date,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Date = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.IssueOrganAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.DocRequisites?.IssueOrgan,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.IssueOrgan = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.DescAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.DocRequisites?.Desc,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Desc = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationMarkAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.Elimination?.EliminationMark,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.Elimination.EliminationMark = v.ParseToBoolean();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationAgencyAttributeIdValue, 
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.Elimination?.EliminationAgency,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.Elimination.EliminationAgency = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.CodeAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EliminationDocRequisites?.CodeDocument?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.CodeDocument.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.NameAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EliminationDocRequisites?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.SeriesAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EliminationDocRequisites?.Series,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Series = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.NumberAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EliminationDocRequisites?.Number,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Number = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.DateAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EliminationDocRequisites?.Date,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Date = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.IssueOrganAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EliminationDocRequisites?.IssueOrgan,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.IssueOrgan = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.DescAttributeIdValue,
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.ElementAtOrDefault(iCounter)?.EliminationDocRequisites?.Desc,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Desc = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);
				}
			}

			AddAttributeToParcel(parcelSection.SurveyingProjectNumAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectNum,
				(o, v) => o.SurveyingProjectNum = v?.ToString());

			AddAttributeToParcel(parcelSection.SurveyingProjectDecisionRequisites?.CodeAttributeIdValue,
				current => ((xmlObjectParcel) current).SurveyingProjectDecisionRequisites?.CodeDocument?.Name,
				(o, v) => o.SurveyingProjectDecisionRequisites.CodeDocument.Name = v?.ToString());

			AddAttributeToParcel(parcelSection.SurveyingProjectDecisionRequisites?.NameAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Name,
				(o, v) => o.SurveyingProjectDecisionRequisites.Name = v?.ToString());

			AddAttributeToParcel(parcelSection.SurveyingProjectDecisionRequisites?.SeriesAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Series,
				(o, v) => o.SurveyingProjectDecisionRequisites.Series = v?.ToString());

			AddAttributeToParcel(parcelSection.SurveyingProjectDecisionRequisites?.NumberAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Number,
				(o, v) => o.SurveyingProjectDecisionRequisites.Number = v?.ToString());

			AddAttributeToParcel(parcelSection.SurveyingProjectDecisionRequisites?.DateAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Date,
				(o, v) => o.SurveyingProjectDecisionRequisites.Date = v.ParseToDateTimeNullable());

			AddAttributeToParcel(parcelSection.SurveyingProjectDecisionRequisites?.IssueOrganAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.IssueOrgan,
				(o, v) => o.SurveyingProjectDecisionRequisites.IssueOrgan = v?.ToString());

			AddAttributeToParcel(parcelSection.SurveyingProjectDecisionRequisites?.DescAttributeIdValue,
				current => ((xmlObjectParcel)current).SurveyingProjectDecisionRequisites?.Desc,
				(o, v) => o.SurveyingProjectDecisionRequisites.Desc = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.UseHiredHouseAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.UseHiredHouse,
				(o, v) => o.HiredHouse.UseHiredHouse = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.ActBuildingAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ActBuilding,
				(o, v) => o.HiredHouse.ActBuilding = v.ParseToBooleanNullable());

			AddAttributeToParcel(parcelSection.HiredHouse?.ActDevelopmentAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ActDevelopment,
				(o, v) => o.HiredHouse.ActDevelopment = v.ParseToBooleanNullable());

			AddAttributeToParcel(parcelSection.HiredHouse?.ContractBuildingAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ContractBuilding,
				(o, v) => o.HiredHouse.ContractBuilding = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.ContractDevelopmentAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ContractDevelopment,
				(o, v) => o.HiredHouse.ContractDevelopment = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.OwnerDecisionAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.OwnerDecision,
				(o, v) => o.HiredHouse.OwnerDecision = v.ParseToBooleanNullable());

			AddAttributeToParcel(parcelSection.HiredHouse?.ContractSupportAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.ContractSupport,
				(o, v) => o.HiredHouse.ContractSupport = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.DocHiredHouse?.CodeAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.CodeDocument?.Name,
				(o, v) => o.HiredHouse.DocHiredHouse.CodeDocument.Name = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.DocHiredHouse?.NameAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Name,
				(o, v) => o.HiredHouse.DocHiredHouse.Name = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.DocHiredHouse?.SeriesAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Series,
				(o, v) => o.HiredHouse.DocHiredHouse.Series = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.DocHiredHouse?.NumberAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Number,
				(o, v) => o.HiredHouse.DocHiredHouse.Number = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.DocHiredHouse?.DateAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Date,
				(o, v) => o.HiredHouse.DocHiredHouse.Date = v.ParseToDateTimeNullable());

			AddAttributeToParcel(parcelSection.HiredHouse?.DocHiredHouse?.IssueOrganAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.IssueOrgan,
				(o, v) => o.HiredHouse.DocHiredHouse.IssueOrgan = v?.ToString());

			AddAttributeToParcel(parcelSection.HiredHouse?.DocHiredHouse?.DescAttributeIdValue,
				current => ((xmlObjectParcel)current).HiredHouse?.DocHiredHouse?.Desc,
				(o, v) => o.HiredHouse.DocHiredHouse.Desc = v?.ToString());

			AddAttributeToParcel(parcelSection.LimitedCirculationAttributeIdValue,
				current => ((xmlObjectParcel)current).LimitedCirculation,
				(o, v) => o.LimitedCirculation = v?.ToString());
		}



		private xmlNaturalObject InitNaturalObject(xmlObject o, int iCounter)
		{
			var element = o.NaturalObjects.ElementAtOrDefault(iCounter);
			if (element != null) 
				return element;

			element = new xmlNaturalObject();
			o.NaturalObjects.Insert(iCounter, element);

			return element;
		}

		private xmlSubParcel InitSubParcelObject(xmlObject o, int iCounter)
		{
			var element = o.SubParcels.ElementAtOrDefault(iCounter);
			if (element != null) 
				return element;
			
			element = new xmlSubParcel();
			o.SubParcels.Insert(iCounter, element);

			return element;
		}

		private xmlEncumbranceZu InitEncumbrancesObject(xmlObject o, int iCounter, int jCounter)
		{
			var subParcel = InitSubParcelObject(o, iCounter);
			var encumbrances = subParcel.Encumbrances;
			var element = encumbrances.ElementAtOrDefault(jCounter);
			if (element != null)
				return element;

			element = new xmlEncumbranceZu();
			encumbrances.Insert(iCounter, element);

			return element;
		}

		private xmlZoneAndTerritory InitZoneAndTerritoriesObject(xmlObject o, int iCounter)
		{
			var element = o.ZoneAndTerritorys.ElementAtOrDefault(iCounter);
			if (element != null)
				return element;

			element = new xmlZoneAndTerritory();
			o.ZoneAndTerritorys.Insert(iCounter, element);

			return element;
		}

		private xmlSupervisionEvent InitGovernmentLandSupervisionObject(xmlObject o, int iCounter)
		{
			var element = o.GovernmentLandSupervision.ElementAtOrDefault(iCounter);
			if (element != null)
				return element;

			element = new xmlSupervisionEvent();
			o.GovernmentLandSupervision.Insert(iCounter, element);

			return element;
		}

		private void AddAttributeToGeneral(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue = null)
		{
			AddGknAttribute(General, attributeId, getValue, setValue);
		}

		private void AddAttributeToParcel(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue = null)
		{
			AddGknAttribute(Parcel, attributeId, getValue, setValue);
		}

		private void AddGknAttribute(List<ImportedAttributeGkn> attributes, long? attributeId, 
			Func<xmlObjectParticular, object> getValue, Action<xmlObject, object> setValue, 
			Func<xmlObjectParticular, bool> canSetValue = null)
		{
			if (!attributeId.HasValue)
				return;

			attributes.Add(new ImportedAttributeGkn(attributeId.Value, getValue, setValue, canSetValue));
		}

		#endregion
	}
}
