using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using KadOzenka.Dal.XmlParser;
using KadOzenka.Dal.XmlParser.GknParserXmlElements;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes
{
	public class GknAllAttributes
	{
		private DataImporterGknConfig Config { get; }

		private List<ImportedAttributeGkn> General { get; }
		public List<ImportedAttributeGkn> Parcel { get; }
		public List<ImportedAttributeGkn> Building { get; }
		public List<ImportedAttributeGkn> Construction { get; }
		public List<ImportedAttributeGkn> Flat { get; }
		public List<ImportedAttributeGkn> All { get; }


		public GknAllAttributes()
		{
			Config = ConfigurationManagers.ConfigurationManager.KoConfig.DataImporterGknConfig;

			General = new List<ImportedAttributeGkn>();
			Parcel = new List<ImportedAttributeGkn>();
			Building = new List<ImportedAttributeGkn>();
			Construction = new List<ImportedAttributeGkn>();
			Flat = new List<ImportedAttributeGkn>();
			All = new List<ImportedAttributeGkn>();

			FillGeneralAttribute();
			FillBuildingAttribute();
			FillParcelAttribute();
			FillConstructionAttribute();
			FillFlatAttribute();

			All.AddRange(General);
			All.AddRange(Building);
			All.AddRange(Parcel);
			All.AddRange(Construction);
			All.AddRange(Flat);

			Building.AddRange(General);
			Parcel.AddRange(General);
			Construction.AddRange(General);
			Flat.AddRange(General);
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

		private void AddAttributeToGeneral(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue = null)
		{
			AddGknAttribute(General, attributeId, getValue, setValue, canSetValue);
		}

		#region Parcel

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
					var iCounter = i;
					var naturalObject = parcelSection.NaturalObjects[i];
					var currentNaturalObject = new Func<object, xmlNaturalObject>(x =>
						((xmlObjectParcel) x).NaturalObjects.ElementAtOrDefault(iCounter));

					AddAttributeToParcel(naturalObject.KindAttributeIdValue, 
						current => currentNaturalObject(current)?.Kind?.Name,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.Kind.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.ForestryAttributeIdValue, 
						current => currentNaturalObject(current)?.Forestry,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.Forestry = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.ForestUseAttributeIdValue, 
						current => currentNaturalObject(current)?.ForestUse?.Name,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.ForestUse.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.QuarterNumbersAttributeIdValue, 
						current => currentNaturalObject(current)?.QuarterNumbers,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.QuarterNumbers = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.TaxationSeparationsAttributeIdValue, 
						current => currentNaturalObject(current)?.TaxationSeparations,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.TaxationSeparations = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.ProtectiveForestAttributeIdValue, 
						current => currentNaturalObject(current)?.ProtectiveForest?.Name,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.ProtectiveForest.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					for (var j = 0; j < naturalObject.ForestEncumbrances.Length; j++)
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
						current => currentNaturalObject(current)?.WaterObject,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.WaterObject = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.NameOtherAttributeIdValue, 
						current => currentNaturalObject(current)?.NameOther,
						(o, v) =>
						{
							var element = InitNaturalObject(o, iCounter);
							element.NameOther = v?.ToString();
						},
						current => ((xmlObjectParcel)current).NaturalObjects.Count >= iCounter + 1);

					AddAttributeToParcel(naturalObject.CharOtherAttributeIdValue, 
						current => currentNaturalObject(current)?.CharOther,
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
					var iCounter = i;
					var subParcel = parcelSection.SubParcels[i];
					var currentSubParcel = new Func<object, xmlSubParcel>(x =>
						((xmlObjectParcel) x).SubParcels.ElementAtOrDefault(iCounter));
					
					AddAttributeToParcel(subParcel.AreaAttributeIdValue, 
						current => currentSubParcel(current)?.Area,
						(o, v) =>
						{
							var element = InitSubParcelObject(o, iCounter);
							element.Area = v?.ParseToDouble();
						},
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);

					AddAttributeToParcel(subParcel.AreaInaccuracyAttributeIdValue, 
						current => currentSubParcel(current)?.AreaInaccuracy,
						(o, v) =>
						{
							var element = InitSubParcelObject(o, iCounter);
							element.AreaInaccuracy = v?.ParseToDouble();
						},
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);

					for (var j = 0; j < subParcel.Encumbrances.Length; j++)
					{
						var jCounter = j;
						var encumbrance = subParcel.Encumbrances[j];
						var currentEncumbrance = new Func<object, xmlEncumbranceZu>(x =>
							((xmlObjectParcel) x).SubParcels.ElementAtOrDefault(iCounter)?.Encumbrances
							.ElementAtOrDefault(jCounter));

						AddAttributeToParcel(encumbrance.NameAttributeIdValue,
							current => currentEncumbrance(current)?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.TypeAttributeIdValue,
							current => currentEncumbrance(current)?.Type?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Type.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.AccountNumberAttributeIdValue,
							current => currentEncumbrance(current)?.AccountNumber,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.AccountNumber = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.CadastralNumberRestrictionAttributeIdValue,
							current => currentEncumbrance(current)?.CadastralNumberRestriction,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.CadastralNumberRestriction = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.RegistrationNumberAttributeIdValue,
							current => currentEncumbrance(current).Registration?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Registration.Number = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.RegistrationDateAttributeIdValue,
							current => currentEncumbrance(current)?.Registration?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Registration.Date = v.ParseToDateTimeNullable();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.CodeAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.CodeDocument?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.CodeDocument.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.NameAttributeIdValue,
							current => currentEncumbrance(current).Document?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Name = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.SeriesAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Series,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Series = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.NumberAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Number = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.DateAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Date = v.ParseToDateTimeNullable();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.IssueOrganAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.IssueOrgan,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.IssueOrgan = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);

						AddAttributeToParcel(encumbrance.Document?.DescAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Desc,
							(o, v) =>
							{
								var element = InitEncumbrancesObject(o, iCounter, jCounter);
								element.Document.Desc = v?.ToString();
							},
							current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1 && ((xmlObjectParcel)current).SubParcels[iCounter].Encumbrances.Count >= jCounter + 1);
					}

					AddAttributeToParcel(subParcel.NumberRecordAttributeIdValue, 
						current => currentSubParcel(current)?.NumberRecord,
						(o, v) =>
						{
							var element = InitSubParcelObject(o, iCounter);
							element.NumberRecord = v?.ToString();
						},
						current => ((xmlObjectParcel)current).SubParcels.Count >= iCounter + 1);

					AddAttributeToParcel(subParcel.DateCreatedAttributeIdValue, 
						current => currentSubParcel(current)?.DateCreated,
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
					var iCounter = i;
					var zoneAndTerritory = parcelSection.ZonesAndTerritories[i];
					var currentZoneAndTerritory = new Func<object, xmlZoneAndTerritory>(x =>
						((xmlObjectParcel) x).ZonesAndTerritories.ElementAtOrDefault(iCounter));

					AddAttributeToParcel(zoneAndTerritory.DescriptionAttributeIdValue, 
						current => currentZoneAndTerritory(current)?.Description,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Description = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.CodeZoneDocAttributeIdValue, 
						current => currentZoneAndTerritory(current)?.CodeZoneDoc,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.CodeZoneDoc = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.AccountNumberAttributeIdValue, 
						current => currentZoneAndTerritory(current)?.AccountNumber,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.AccountNumber = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.ContentRestrictionsAttributeIdValue, 
						current => currentZoneAndTerritory(current)?.ContentRestrictions,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.ContentRestrictions = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.FullPartlyAttributeIdValue, 
						current => currentZoneAndTerritory(current)?.FullPartly,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.FullPartly = v.ParseToBooleanNullable();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.CodeAttributeIdValue,
						current => currentZoneAndTerritory(current)?.Document?.CodeDocument?.Name,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.CodeDocument.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.NameAttributeIdValue,
						current => currentZoneAndTerritory(current)?.Document?.Name,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.SeriesAttributeIdValue,
						current => currentZoneAndTerritory(current)?.Document?.Series,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Series = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.NumberAttributeIdValue,
						current => currentZoneAndTerritory(current)?.Document?.Number,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Number = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.DateAttributeIdValue,
						current => currentZoneAndTerritory(current)?.Document?.Date,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.Date = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.IssueOrganAttributeIdValue,
						current => currentZoneAndTerritory(current)?.Document?.IssueOrgan,
						(o, v) =>
						{
							var element = InitZoneAndTerritoriesObject(o, iCounter);
							element.Document.IssueOrgan = v?.ToString();
						},
						current => ((xmlObjectParcel)current).ZonesAndTerritories.Count >= iCounter + 1);

					AddAttributeToParcel(zoneAndTerritory.Document?.DescAttributeIdValue,
						current => currentZoneAndTerritory(current)?.Document?.Desc,
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
					var iCounter = i;
					var supervisionEvent = parcelSection.GovernmentLandSupervision[i];
					var currentGovernment = new Func<object, xmlSupervisionEvent>(x =>
						((xmlObjectParcel) x).GovernmentLandSupervision.ElementAtOrDefault(iCounter));

					AddAttributeToParcel(supervisionEvent.AgencyAttributeIdValue, 
						current => currentGovernment(current)?.Agency,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.Agency = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EventNameAttributeIdValue, 
						current => currentGovernment(current)?.EventName?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EventName.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EventFormAttributeIdValue, 
						current => currentGovernment(current)?.EventForm?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EventForm.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.InspectionEndAttributeIdValue, 
						current => currentGovernment(current)?.InspectionEnd,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.InspectionEnd = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.AvailabilityViolationsAttributeIdValue, 
						current => currentGovernment(current)?.AvailabilityViolations,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.AvailabilityViolations = v.ParseToBooleanNullable();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.IdentifiedViolationsTypeViolationsAttributeIdValue, 
						current => currentGovernment(current)?.IdentifiedViolations?.TypeViolations,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.IdentifiedViolations.TypeViolations = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.IdentifiedViolationsSignViolationsAttributeIdValue, 
						current => currentGovernment(current)?.IdentifiedViolations?.SignViolations,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.IdentifiedViolations.SignViolations = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.IdentifiedViolationsAreaAttributeIdValue, 
						current => currentGovernment(current)?.IdentifiedViolations?.Area,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.IdentifiedViolations.Area = v?.ParseToDouble();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.CodeAttributeIdValue,
						current => currentGovernment(current)?.DocRequisites?.CodeDocument?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.CodeDocument.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.NameAttributeIdValue,
						current => currentGovernment(current)?.DocRequisites?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.SeriesAttributeIdValue,
						current => currentGovernment(current)?.DocRequisites?.Series,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Series = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.NumberAttributeIdValue,
						current => currentGovernment(current)?.DocRequisites?.Number,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Number = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.DateAttributeIdValue,
						current => currentGovernment(current)?.DocRequisites?.Date,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Date = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.IssueOrganAttributeIdValue,
						current => currentGovernment(current)?.DocRequisites?.IssueOrgan,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.IssueOrgan = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.DocRequisites?.DescAttributeIdValue,
						current => currentGovernment(current)?.DocRequisites?.Desc,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.DocRequisites.Desc = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationMarkAttributeIdValue, 
						current => currentGovernment(current)?.Elimination?.EliminationMark,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.Elimination.EliminationMark = v.ParseToBoolean();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationAgencyAttributeIdValue, 
						current => currentGovernment(current)?.Elimination?.EliminationAgency,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.Elimination.EliminationAgency = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.CodeAttributeIdValue,
						current => currentGovernment(current)?.EliminationDocRequisites?.CodeDocument?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.CodeDocument.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.NameAttributeIdValue,
						current => currentGovernment(current)?.EliminationDocRequisites?.Name,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Name = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.SeriesAttributeIdValue,
						current => currentGovernment(current)?.EliminationDocRequisites?.Series,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Series = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.NumberAttributeIdValue,
						current => currentGovernment(current)?.EliminationDocRequisites?.Number,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Number = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.DateAttributeIdValue,
						current => currentGovernment(current)?.EliminationDocRequisites?.Date,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.Date = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.IssueOrganAttributeIdValue,
						current => currentGovernment(current)?.EliminationDocRequisites?.IssueOrgan,
						(o, v) =>
						{
							var element = InitGovernmentLandSupervisionObject(o, iCounter);
							element.EliminationDocRequisites.IssueOrgan = v?.ToString();
						},
						current => ((xmlObjectParcel)current).GovernmentLandSupervision.Count >= iCounter + 1);

					AddAttributeToParcel(supervisionEvent.EliminationDocRequisites?.DescAttributeIdValue,
						current => currentGovernment(current)?.EliminationDocRequisites?.Desc,
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

		private void AddAttributeToParcel(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue = null)
		{
			AddGknAttribute(Parcel, attributeId, getValue, setValue, canSetValue);
		}

		#endregion


		#region Building

		private void FillBuildingAttribute()
		{
			var buildingSection = Config.GknDataAttributes.Building;

			AddAttributeToBuilding(buildingSection.ParentCadastralNumbersAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectBuild)current).ParentCadastralNumbers),
				(o, v) => o.ParentCadastralNumbers.Add(v?.ToString()));

			AddAttributeToBuilding(buildingSection.NameAttributeIdValue, 
				current => ((xmlObjectBuild)current).Name,
				(o, v) => o.NameObject = v?.ToString());

			AddAttributeToBuilding(buildingSection.AssignationBuildingAttributeIdValue, 
				current => ((xmlObjectBuild)current).AssignationBuilding?.Name,
				(o, v) => o.AssignationBuilding.Name = v?.ToString());

			AddAttributeToBuilding(buildingSection.WallMaterialAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectBuild)current).Walls),
				(o, v) => o.Walls.Add(new xmlCodeName { Name = v?.ToString() }));

			AddAttributeToBuilding(buildingSection.ExploitationCharYearBuiltAttributeIdValue, 
				current => ((xmlObjectBuild)current).Years?.Year_Built,
				(o, v) => o.Years.Year_Built = v?.ToString());

			AddAttributeToBuilding(buildingSection.ExploitationCharYearUsedAttributeIdValue, 
				current => ((xmlObjectBuild)current).Years?.Year_Used,
				(o, v) => o.Years.Year_Used = v?.ToString());

			AddAttributeToBuilding(buildingSection.FloorCountAttributeIdValue, 
				current => ((xmlObjectBuild)current).Floors?.Floors,
				(o, v) => o.Floors.Floors = v?.ToString());

			AddAttributeToBuilding(buildingSection.FloorUndergroundCountAttributeIdValue, 
				current => ((xmlObjectBuild)current).Floors?.Underground_Floors,
				(o, v) => o.Floors.Underground_Floors = v?.ToString());

			AddAttributeToBuilding(buildingSection.AreaAttributeIdValue, 
				current => ((xmlObjectBuild)current).Area,
				(o, v) => o.Area = v?.ParseToDouble());

			AddAttributeToBuilding(buildingSection.ObjectPermittedUsesAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectBuild)current).ObjectPermittedUses),
				(o, v) => o.ObjectPermittedUses.Add(v?.ToString()));

			var subBuildingsLength = buildingSection.SubBuildings.Length;
			if (subBuildingsLength > 0)
			{
				for (var i = 0; i < subBuildingsLength; i++)
				{
					var subBuilding = buildingSection.SubBuildings[i];
					var iCounter = i;
					var currentSubBuilding = new Func<object, xmlSubBuildingFlat>((x) =>
						((xmlObjectBuild) x).SubBuildings.ElementAtOrDefault(iCounter));

					AddAttributeToBuilding(subBuilding.AreaAttributeIdValue,
						current => currentSubBuilding(current)?.Area,
						(o, v) =>
						{
							var element = InitFlatSubBuilding(o, iCounter);
							element.Area = v?.ParseToDouble();
						},
						current => ((xmlObjectBuild) current).SubBuildings.Count >= iCounter + 1);

					for (var j = 0; j < subBuilding.Encumbrances.Length; j++)
					{
						var encumbrance = subBuilding.Encumbrances[j];
						var jCounter = j;

						var currentEncumbrance = new Func<object, xmlEncumbranceOks>(x => ((xmlObjectBuild) x)
							.SubBuildings.ElementAtOrDefault(iCounter)
							?.EncumbrancesOks.ElementAtOrDefault(jCounter));

						AddAttributeToBuilding(encumbrance.NameAttributeIdValue,
							current => currentEncumbrance(current)?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Name = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.TypeAttributeIdValue,
							current => currentEncumbrance(current)?.Type?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Type.Name = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.RegistrationNumberAttributeIdValue,
							current => currentEncumbrance(current)?.Registration?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Registration.Number = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.RegistrationDateAttributeIdValue,
							current => currentEncumbrance(current)?.Registration?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Registration.Date = v.ParseToDateTimeNullable();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.Document?.CodeAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.CodeDocument?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Document.CodeDocument.Name = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.Document?.NameAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Document.Name = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.Document?.SeriesAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Series,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Document.Series = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.Document?.NumberAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Document.Number = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.Document?.DateAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Document.Date = v.ParseToDateTimeNullable();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.Document?.IssueOrganAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.IssueOrgan,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Document.IssueOrgan = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);

						AddAttributeToBuilding(encumbrance.Document?.DescAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Desc,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForBuilding(o, iCounter, jCounter);
								element.Document.Desc = v?.ParseToString();
							},
							current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1 && ((xmlObjectBuild)current).SubBuildings[iCounter].EncumbrancesOks.Count >= jCounter + 1);
					}

					AddAttributeToBuilding(subBuilding.NumberRecordAttributeIdValue, 
						current => currentSubBuilding(current)?.NumberRecord,
						(o, v) =>
						{
							var element = InitFlatSubBuilding(o, iCounter);
							element.NumberRecord = v?.ParseToString();
						},
						current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1);

					AddAttributeToBuilding(subBuilding.DateCreatedAttributeIdValue, 
						current => currentSubBuilding(current)?.DateCreated,
						(o, v) =>
						{
							var element = InitFlatSubBuilding(o, iCounter);
							element.DateCreated = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectBuild)current).SubBuildings.Count >= iCounter + 1);
				}
			}

			AddAttributeToBuilding(buildingSection.FlatsCadastralNumbersAttributeIdValue,
				current => xmlCodeName.GetNames(((xmlObjectBuild) current).FlatsCadastralNumbers),
				(o, v) => o.FlatsCadastralNumbers.Add(v?.ToString()));

			AddAttributeToBuilding(buildingSection.CarParkingSpacesCadastralNumbersAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectBuild)current).CarParkingSpacesCadastralNumbers),
				(o, v) => o.CarParkingSpacesCadastralNumbers.Add(v?.ToString()));
			
			AddAttributeToBuilding(buildingSection.UnitedCadastralNumberAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectBuild)current).UnitedCadastralNumbers),
				(o, v) => o.UnitedCadastralNumbers.Add(v?.ToString()));

			AddAttributeToBuilding(buildingSection.FacilityCadastralNumberAttributeIdValue, 
				current => ((xmlObjectBuild)current).FacilityCadastralNumber,
				(o, v) => o.FacilityCadastralNumber = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.FacilityPurposeAttributeIdValue, 
				current => ((xmlObjectBuild)current).FacilityPurpose,
				(o, v) => o.FacilityPurpose = v?.ToString());

			AddAttributeToBuilding(buildingSection.CulturalHeritage?.EgroknRegNumAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.EgroknRegNum,
				(o, v) => o.CulturalHeritage.EgroknRegNum = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.EgroknObjCulturalAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.EgroknObjCultural?.Name,
				(o, v) => o.CulturalHeritage.EgroknObjCultural.Name = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.EgroknNameCulturalAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.EgroknNameCultural,
				(o, v) => o.CulturalHeritage.EgroknNameCultural = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.RequirementsEnsureAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.RequirementsEnsure,
				(o, v) => o.CulturalHeritage.RequirementsEnsure = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.Document?.CodeAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.CodeDocument?.Name,
				(o, v) => o.CulturalHeritage.Document.CodeDocument.Name = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.Document?.NameAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Name,
				(o, v) => o.CulturalHeritage.Document.Name = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.Document?.SeriesAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Series,
				(o, v) => o.CulturalHeritage.Document.Series = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.Document?.NumberAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Number,
				(o, v) => o.CulturalHeritage.Document.Number = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.Document?.DateAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Date,
				(o, v) => o.CulturalHeritage.Document.Date = v.ParseToDateTimeNullable());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.Document?.IssueOrganAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.IssueOrgan,
				(o, v) => o.CulturalHeritage.Document.IssueOrgan = v?.ToString());
			
			AddAttributeToBuilding(buildingSection.CulturalHeritage?.Document?.DescAttributeIdValue, 
				current => ((xmlObjectBuild)current).CulturalHeritage?.Document?.Desc,
				(o, v) => o.CulturalHeritage.Document.Desc = v?.ToString());
		}

		private xmlSubBuildingFlat InitFlatSubBuilding(xmlObject o, int iCounter)
		{
			var element = o.SubBuildingFlats.ElementAtOrDefault(iCounter);
			if (element != null)
				return element;

			element = new xmlSubBuildingFlat();
			o.SubBuildingFlats.Insert(iCounter, element);

			return element;
		}

		private xmlEncumbranceOks InitEncumbrancesObjectForBuilding(xmlObject o, int iCounter, int jCounter)
		{
			var flatSubBuilding = InitFlatSubBuilding(o, iCounter);
			var encumbrances = flatSubBuilding.EncumbrancesOks;
			var element = encumbrances.ElementAtOrDefault(jCounter);
			if (element != null)
				return element;

			element = new xmlEncumbranceOks();
			encumbrances.Insert(iCounter, element);

			return element;
		}

		private void AddAttributeToBuilding(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue = null)
		{
			AddGknAttribute(Building, attributeId, getValue, setValue, canSetValue);
		}

		#endregion


		#region Construction

		private void FillConstructionAttribute()
		{
			var constructionSection = Config.GknDataAttributes.Construction;

			AddAttributeToConstruction(constructionSection.ParentCadastralNumbersAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectConstruction)current).ParentCadastralNumbers),
				(o, v) => o.ParentCadastralNumbers.Add(v?.ToString()));
			
			AddAttributeToConstruction(constructionSection.NameAttributeIdValue, 
				current => ((xmlObjectConstruction)current).Name,
				(o, v) => o.NameObject = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.AssignationNameAttributeIdValue, 
				current => ((xmlObjectConstruction)current).AssignationName,
				(o, v) => o.AssignationName = v?.ToString());

			AddAttributeToConstruction(constructionSection.ExploitationCharYearBuiltAttributeIdValue, 
				current => ((xmlObjectConstruction)current).Years?.Year_Built,
				(o, v) => o.Years.Year_Built = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.ExploitationCharYearUsedAttributeIdValue, 
				current => ((xmlObjectConstruction)current).Years?.Year_Used,
				(o, v) => o.Years.Year_Used = v?.ToString());

			AddAttributeToConstruction(constructionSection.FloorCountAttributeIdValue, 
				current => ((xmlObjectConstruction)current).Floors?.Floors,
				(o, v) => o.Floors.Floors = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.FloorUndergroundCountAttributeIdValue, 
				current => ((xmlObjectConstruction)current).Floors?.Underground_Floors,
				(o, v) => o.Floors.Underground_Floors = v?.ToString());

			AddAttributeToConstruction(constructionSection.KeyParametersAttributeIdValue,
				current => xmlCodeNameValue.GetNames(((xmlObjectConstruction) current).KeyParameters),
				(o, v) => o.KeyParameters.Add(new xmlCodeNameValue {Name = v?.ToString()}));

			var constructionKeyParametersLength = constructionSection.KeyParameters.Length;
			if (constructionKeyParametersLength > 0)
			{
				for (var i = 0; i < constructionKeyParametersLength; i++)
				{
					var iCounter = i;
					var keyParameter = constructionSection.KeyParameters[i];
					var currentConstructionKeyParameter =
						new Func<object, xmlCodeNameValue>(x => ((xmlObjectConstruction) x).KeyParameters.ElementAtOrDefault(iCounter));

					AddAttributeToConstruction(keyParameter.KeyParameterAttributeIdValue,
						current => currentConstructionKeyParameter(current)?.Name,
						(o, v) =>
						{
							var element = InitConstructionKeyParameters(o, iCounter);
							element.Name = v?.ToString();
						},
						current => ((xmlObjectConstruction)current).KeyParameters.Count >= iCounter + 1);

					AddAttributeToConstruction(keyParameter.KeyParameterValueAttributeIdValue, 
						current => currentConstructionKeyParameter(current)?.Value,
						(o, v) =>
						{
							var element = InitConstructionKeyParameters(o, iCounter);
							element.Value = v?.ToString();
						},
						current => ((xmlObjectConstruction)current).KeyParameters.Count >= iCounter + 1);
				}
			}

			AddAttributeToConstruction(constructionSection.ObjectPermittedUsesAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectConstruction)current).ObjectPermittedUses),
				(o, v) => o.ObjectPermittedUses.Add(v?.ToString()));

			var subConstructionsLength = constructionSection.SubConstructions.Length;
			if (subConstructionsLength > 0)
			{
				for (var i = 0; i < subConstructionsLength; i++)
				{
					var iCounter = i;
					var subConstruction = constructionSection.SubConstructions[i];
					var currentSubConstruction =
						new Func<object, xmlSubConstruction>(x => ((xmlObjectConstruction) x).SubConstructions.ElementAtOrDefault(iCounter));

					AddAttributeToConstruction(subConstruction.KeyParameter?.KeyParameterAttributeIdValue, 
						current => currentSubConstruction(current)?.KeyParameter?.Name,
						(o, v) =>
						{
							var element = InitSubConstruction(o, iCounter);
							element.KeyParameter.Name = v?.ToString();
						},
						current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1);

					AddAttributeToConstruction(subConstruction.KeyParameter?.KeyParameterValueAttributeIdValue, 
						current => currentSubConstruction(current)?.KeyParameter?.Value,
						(o, v) =>
						{
							var element = InitSubConstruction(o, iCounter);
							element.KeyParameter.Value = v?.ToString();
						},
						current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1);

					for (var j = 0; j < subConstruction.Encumbrances.Length; j++)
					{
						var jCounter = j;
						var encumbrance = subConstruction.Encumbrances[j];
						var currentEncumbrance = new Func<object, xmlEncumbranceOks>(x =>
							((xmlObjectConstruction) x).SubConstructions.ElementAtOrDefault(iCounter)?.EncumbrancesOks
							.ElementAtOrDefault(jCounter));


						AddAttributeToConstruction(encumbrance.NameAttributeIdValue,
							current => currentEncumbrance(current)?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Name = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.TypeAttributeIdValue,
							current => currentEncumbrance(current)?.Type?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Type.Name = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.RegistrationNumberAttributeIdValue,
							current => currentEncumbrance(current)?.Registration?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Registration.Number = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.RegistrationDateAttributeIdValue,
							current => currentEncumbrance(current)?.Registration?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Registration.Date = v.ParseToDateTimeNullable();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.Document?.CodeAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.CodeDocument?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Document.CodeDocument.Name = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.Document?.NameAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Document.Name = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.Document?.SeriesAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Series,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Document.Series = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.Document?.NumberAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Document.Number = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.Document?.DateAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Document.Date = v.ParseToDateTimeNullable();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.Document?.IssueOrganAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.IssueOrgan,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Document.IssueOrgan = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToConstruction(encumbrance.Document?.DescAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Desc,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForConstruction(o, iCounter, jCounter);
								element.Document.Desc = v?.ToString();
							},
							current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1 && ((xmlObjectConstruction)current).SubConstructions[iCounter].EncumbrancesOks.Count >= jCounter + 1);
					}

					AddAttributeToConstruction(subConstruction.NumberRecordAttributeIdValue, 
						current => currentSubConstruction(current)?.NumberRecord,
						(o, v) =>
						{
							var element = InitSubConstruction(o, iCounter);
							element.NumberRecord = v?.ToString();
						},
						current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1);
					
					AddAttributeToConstruction(subConstruction.DateCreatedAttributeIdValue, 
						current => currentSubConstruction(current).DateCreated,
						(o, v) =>
						{
							var element = InitSubConstruction(o, iCounter);
							element.DateCreated = v.ParseToDateTimeNullable();
						},
						current => ((xmlObjectConstruction)current).SubConstructions.Count >= iCounter + 1);
				}
			}

			AddAttributeToConstruction(constructionSection.FlatsCadastralNumbersAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectConstruction)current).FlatsCadastralNumbers),
				(o, v) => o.FlatsCadastralNumbers.Add(v?.ToString()));
			
			AddAttributeToConstruction(constructionSection.CarParkingSpacesCadastralNumbersAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectConstruction)current).CarParkingSpacesCadastralNumbers),
				(o, v) => o.CarParkingSpacesCadastralNumbers.Add(v?.ToString()));
			
			AddAttributeToConstruction(constructionSection.UnitedCadastralNumberAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectConstruction)current).UnitedCadastralNumbers),
				(o, v) => o.UnitedCadastralNumbers.Add(v?.ToString()));

			AddAttributeToConstruction(constructionSection.FacilityCadastralNumberAttributeIdValue, 
				current => ((xmlObjectConstruction)current).FacilityCadastralNumber,
				(o, v) => o.FacilityCadastralNumber = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.FacilityPurposeAttributeIdValue, 
				current => ((xmlObjectConstruction)current).FacilityPurpose,
				(o, v) => o.FacilityPurpose = v?.ToString());

			AddAttributeToConstruction(constructionSection.CulturalHeritage?.EgroknRegNumAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.EgroknRegNum,
				(o, v) => o.CulturalHeritage.EgroknRegNum = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.EgroknObjCulturalAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.EgroknObjCultural?.Name,
				(o, v) => o.CulturalHeritage.EgroknObjCultural.Name = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.EgroknNameCulturalAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.EgroknNameCultural,
				(o, v) => o.CulturalHeritage.EgroknNameCultural = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.RequirementsEnsureAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.RequirementsEnsure,
				(o, v) => o.CulturalHeritage.RequirementsEnsure = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.Document?.CodeAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.CodeDocument?.Name,
				(o, v) => o.CulturalHeritage.Document.CodeDocument.Name = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.Document?.NameAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Name,
				(o, v) => o.CulturalHeritage.Document.Name = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.Document?.SeriesAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Series,
				(o, v) => o.CulturalHeritage.Document.Series = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.Document?.NumberAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Number,
				(o, v) => o.CulturalHeritage.Document.Number = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.Document?.DateAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Date,
				(o, v) => o.CulturalHeritage.Document.Date = v.ParseToDateTimeNullable());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.Document?.IssueOrganAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.IssueOrgan,
				(o, v) => o.CulturalHeritage.Document.IssueOrgan = v?.ToString());
			
			AddAttributeToConstruction(constructionSection.CulturalHeritage?.Document?.DescAttributeIdValue, 
				current => ((xmlObjectConstruction)current).CulturalHeritage?.Document?.Desc,
				(o, v) => o.CulturalHeritage.Document.Desc = v?.ToString());
		}

		private xmlCodeNameValue InitConstructionKeyParameters(xmlObject o, int iCounter)
		{
			var element = o.KeyParameters.ElementAtOrDefault(iCounter);
			if (element != null)
				return element;

			element = new xmlCodeNameValue();
			o.KeyParameters.Insert(iCounter, element);

			return element;
		}

		private xmlSubConstruction InitSubConstruction(xmlObject o, int iCounter)
		{
			var element = o.SubConstructions.ElementAtOrDefault(iCounter);
			if (element != null)
				return element;

			element = new xmlSubConstruction();
			o.SubConstructions.Insert(iCounter, element);

			return element;
		}

		private xmlEncumbranceOks InitEncumbrancesObjectForConstruction(xmlObject o, int iCounter, int jCounter)
		{
			var subConstruction = InitSubConstruction(o, iCounter);
			var encumbrances = subConstruction.EncumbrancesOks;
			var element = encumbrances.ElementAtOrDefault(jCounter);
			if (element != null)
				return element;

			element = new xmlEncumbranceOks();
			encumbrances.Insert(iCounter, element);

			return element;
		}

		private void AddAttributeToConstruction(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue = null)
		{
			AddGknAttribute(Construction, attributeId, getValue, setValue, canSetValue);
		}

		#endregion


		#region Flat

		private void FillFlatAttribute()
		{
			var flatSection = Config.GknDataAttributes.Flat;

			AddAttributeToFlat(flatSection.CadastralNumberFlatAttributeIdValue, 
				current => ((xmlObjectFlat)current).CadastralNumberFlat,
					(o, v) => o.CadastralNumberFlat = v?.ToString());

			AddAttributeToFlat(flatSection.CadastralNumberOksAttributeIdValue, 
				current => ((xmlObjectFlat)current).CadastralNumberOKS,
				(o, v) => o.CadastralNumberOKS = v?.ToString());

			AddAttributeToFlat(flatSection.ParentOks?.CadastralNumberOksAttributeIdValue, 
				current => ((xmlObjectFlat)current).ParentOks?.CadastralNumberOKS,
				(o, v) => o.ParentOks.CadastralNumberOKS = v?.ToString());
			
			AddAttributeToFlat(flatSection.ParentOks?.ObjectTypeAttributeIdValue, 
				current => ((xmlObjectFlat)current).ParentOks?.ObjectType?.Name,
				(o, v) => o.ParentOks.ObjectType.Name = v?.ToString());
			
			AddAttributeToFlat(flatSection.ParentOks?.AssignationBuildingAttributeIdValue, 
				current => ((xmlObjectFlat)current).ParentOks?.AssignationBuilding?.Name,
				(o, v) => o.ParentOks.AssignationBuilding.Name = v?.ToString());
			
			AddAttributeToFlat(flatSection.ParentOks?.AssignationNameAttributeIdValue, 
				current => ((xmlObjectFlat)current).ParentOks?.AssignationName,
				(o, v) => o.ParentOks.AssignationName = v?.ToString());
			
			AddAttributeToFlat(flatSection.ParentOks?.WallMaterialAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectFlat)current).ParentOks?.Walls),
				(o, v) => o.ParentOks.Walls.Add(new xmlCodeName {Name = v?.ToString()}));
			
			AddAttributeToFlat(flatSection.ParentOks?.ExploitationCharYearBuiltAttributeIdValue, 
				current => ((xmlObjectFlat)current).ParentOks?.Years?.Year_Built,
				(o, v) => o.ParentOks.Years.Year_Built = v?.ToString());
			
			AddAttributeToFlat(flatSection.ParentOks?.ExploitationCharYearUsedAttributeIdValue, 
				current => ((xmlObjectFlat)current).ParentOks?.Years?.Year_Used,
				(o, v) => o.ParentOks.Years.Year_Used = v?.ToString());
			
			AddAttributeToFlat(flatSection.ParentOks?.FloorCountAttributeIdValue, 
				current => ((xmlObjectFlat)current).ParentOks?.Floors?.Floors,
				(o, v) => o.ParentOks.Floors.Floors = v?.ToString());
			
			AddAttributeToFlat(flatSection.ParentOks?.FloorUndergroundCountAttributeIdValue, 
				current => ((xmlObjectFlat)current).ParentOks?.Floors?.Underground_Floors,
				(o, v) => o.ParentOks.Floors.Underground_Floors = v?.ToString());

			AddAttributeToFlat(flatSection.NameAttributeIdValue, 
				current => ((xmlObjectFlat)current).Name,
				(o, v) => o.NameObject = v?.ToString());

			AddAttributeToFlat(flatSection.AssignationAssignationCodeAttributeIdValue, 
				current => ((xmlObjectFlat)current).AssignationFlatCode?.Name,
				(o, v) => o.AssignationFlatCode.Name = v?.ToString());
			
			AddAttributeToFlat(flatSection.AssignationAssignationTypeAttributeIdValue, 
				current => ((xmlObjectFlat)current).AssignationFlatType?.Name,
				(o, v) => o.AssignationFlatType.Name = v?.ToString());
			
			AddAttributeToFlat(flatSection.AssignationSpecialTypeAttributeIdValue, 
				current => ((xmlObjectFlat)current).AssignationSpecialType?.Name,
				(o, v) => o.AssignationSpecialType.Name = v?.ToString());
			
			AddAttributeToFlat(flatSection.AssignationTotalAssetsAttributeIdValue, 
				current => ((xmlObjectFlat)current).AssignationTotalAssets,
				(o, v) => o.AssignationTotalAssets = v.ParseToBooleanNullable());
			
			AddAttributeToFlat(flatSection.AssignationAuxiliaryFlatAttributeIdValue, 
				current => ((xmlObjectFlat)current).AssignationAuxiliaryFlat,
				(o, v) => o.AssignationAuxiliaryFlat = v.ParseToBooleanNullable());

			AddAttributeToFlat(flatSection.AreaAttributeIdValue, 
				current => ((xmlObjectFlat)current).Area,
				(o, v) => o.Area = v?.ParseToDouble());

			AddAttributeToFlat(flatSection.PositionNumberOnPlanAttributeIdValue, 
				current => ((xmlObjectFlat)current).Position?.NumberOnPlan,
				(o, v) => o.Position.NumberOnPlan = v?.ToString());
			
			AddAttributeToFlat(flatSection.PositionDescriptionAttributeIdValue, 
				current => ((xmlObjectFlat)current).Position?.Description,
				(o, v) => o.Position.Description = v?.ToString());

			var levelsLength = flatSection.Levels.Length;
 			if (levelsLength > 0)
			{
				for (var i = 0; i < levelsLength; i++)
				{
					var iCounter = i;
					var level = flatSection.Levels[i];
					var currentFlatLevel =
						new Func<object, xmlLevel>(x => ((xmlObjectFlat) x).Levels.ElementAtOrDefault(iCounter));

					AddAttributeToFlat(level.NumberAttributeIdValue, 
						current => currentFlatLevel(current)?.Number,
						(o, v) =>
						{
							var element = InitFlatLevel(o, iCounter);
							element.Number = v?.ToString();
						},
						current => ((xmlObjectFlat)current).Levels.Count >= iCounter + 1);

					AddAttributeToFlat(level.TypeAttributeIdValue,
						current => xmlCodeName.GetNames(new List<xmlCodeName> {currentFlatLevel(current)?.Type}),
						(o, v) =>
						{
							var element = InitFlatLevel(o, iCounter);
							element.Type.Name = v?.ToString();
						},
						current => ((xmlObjectFlat) current).Levels.Count >= iCounter + 1);

					AddAttributeToFlat(level.PositionNumberOnPlanAttributeIdValue, 
						current => currentFlatLevel(current)?.Position?.NumberOnPlan,
						(o, v) =>
						{
							var element = InitFlatLevel(o, iCounter);
							element.Position.NumberOnPlan = v?.ToString();
						},
						current => ((xmlObjectFlat)current).Levels.Count >= iCounter + 1);

					AddAttributeToFlat(level.PositionDescriptionAttributeIdValue, 
						current => currentFlatLevel(current)?.Position?.Description,
						(o, v) =>
						{
							var element = InitFlatLevel(o, iCounter);
							element.Position.Description = v?.ToString();
						},
						current => ((xmlObjectFlat)current).Levels.Count >= iCounter + 1);
				}
			}

			AddAttributeToFlat(flatSection.ObjectPermittedUsesAttributeIdValue, 
				current => xmlCodeName.GetNames(((xmlObjectFlat)current).ObjectPermittedUses),
				(o, v) => o.ObjectPermittedUses.Add(v?.ToString()));

			var subFlatsLength = flatSection.SubFlats.Length;
			if (subFlatsLength > 0)
			{
				for (var i = 0; i < subFlatsLength; i++)
				{
					var iCounter = i;
					var subFlat = flatSection.SubFlats[i];
					var currentSubFlat = new Func<object, xmlSubBuildingFlat>(x =>
						((xmlObjectFlat) x).SubFlats.ElementAtOrDefault(iCounter));

					AddAttributeToFlat(subFlat.AreaAttributeIdValue, 
						current => currentSubFlat(current)?.Area,
						(o, v) =>
						{
							var element = InitSubFlat(o, iCounter);
							element.Area = v?.ParseToDouble();
						},
						current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1);

					for (var j = 0; j < subFlat.Encumbrances.Length; j++)
					{
						var jCounter = j;
						var encumbrance = subFlat.Encumbrances[j];
						var currentEncumbrance = new Func<object, xmlEncumbranceOks>(x =>
							((xmlObjectFlat) x).SubFlats.ElementAtOrDefault(iCounter)?.EncumbrancesOks
							.ElementAtOrDefault(jCounter));
						
						AddAttributeToFlat(encumbrance.NameAttributeIdValue,
							current => currentEncumbrance(current)?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Name = v?.ToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.TypeAttributeIdValue,
							current => currentEncumbrance(current)?.Type?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Type.Name = v?.ToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.RegistrationNumberAttributeIdValue,
							current => currentEncumbrance(current)?.Registration?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Registration.Number = v?.ToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.RegistrationDateAttributeIdValue,
							current => currentEncumbrance(current)?.Registration?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Registration.Date = v?.ParseToDateTimeNullable();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.Document?.CodeAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.CodeDocument?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Document.CodeDocument.Name = v?.ParseToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.Document?.NameAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Name,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Document.Name = v?.ParseToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.Document?.SeriesAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Series,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Document.Series = v?.ParseToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.Document?.NumberAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Number,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Document.Number = v?.ParseToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.Document?.DateAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Date,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Document.Date = v?.ParseToDateTimeNullable();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.Document?.IssueOrganAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.IssueOrgan,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Document.IssueOrgan = v?.ToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
						
						AddAttributeToFlat(encumbrance.Document?.DescAttributeIdValue,
							current => currentEncumbrance(current)?.Document?.Desc,
							(o, v) =>
							{
								var element = InitEncumbrancesObjectForFlat(o, iCounter, jCounter);
								element.Document.Desc= v?.ToString();
							},
							current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1 && ((xmlObjectFlat)current).SubFlats[iCounter].EncumbrancesOks.Count >= jCounter + 1);
					}

					AddAttributeToFlat(subFlat.NumberRecordAttributeIdValue, 
						current => currentSubFlat(current)?.NumberRecord,
						(o, v) =>
						{
							var element = InitSubFlat(o, iCounter);
							element.NumberRecord = v?.ParseToString();
						},
						current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1);
					AddAttributeToFlat(subFlat.DateCreatedAttributeIdValue, 
						current => currentSubFlat(current)?.DateCreated,
						(o, v) =>
						{
							var element = InitSubFlat(o, iCounter);
							element.DateCreated = v?.ParseToDateTimeNullable();
						},
						current => ((xmlObjectFlat)current).SubFlats.Count >= iCounter + 1);
				}
			}

			AddAttributeToFlat(flatSection.UnitedCadastralNumberAttributeIdValue,
				current => xmlCodeName.GetNames(((xmlObjectFlat) current).UnitedCadastralNumbers),
				(o, v) => o.UnitedCadastralNumbers.Add(v?.ToString()));

			AddAttributeToFlat(flatSection.FacilityCadastralNumberAttributeIdValue, 
				current => ((xmlObjectFlat)current).FacilityCadastralNumber,
				(o, v) => o.FacilityCadastralNumber = v?.ToString());

			AddAttributeToFlat(flatSection.FacilityPurposeAttributeIdValue, 
				current => ((xmlObjectFlat)current).FacilityPurpose,
				(o, v) => o.FacilityPurpose = v?.ToString());

			AddAttributeToFlat(flatSection.CulturalHeritage?.EgroknRegNumAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.EgroknRegNum,
				(o, v) => o.CulturalHeritage.EgroknRegNum = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.EgroknObjCulturalAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.EgroknObjCultural?.Name,
				(o, v) => o.CulturalHeritage.EgroknObjCultural.Name = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.EgroknNameCulturalAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.EgroknNameCultural,
				(o, v) => o.CulturalHeritage.EgroknNameCultural = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.RequirementsEnsureAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.RequirementsEnsure,
				(o, v) => o.CulturalHeritage.RequirementsEnsure = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.Document?.CodeAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.CodeDocument?.Name,
				(o, v) => o.CulturalHeritage.Document.CodeDocument.Name = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.Document?.NameAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Name,
				(o, v) => o.CulturalHeritage.Document.Name = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.Document?.SeriesAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Series,
				(o, v) => o.CulturalHeritage.Document.Series = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.Document?.NumberAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Number,
				(o, v) => o.CulturalHeritage.Document.Number = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.Document?.DateAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Date,
				(o, v) => o.CulturalHeritage.Document.Date = v?.ParseToDateTimeNullable());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.Document?.IssueOrganAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.IssueOrgan,
				(o, v) => o.CulturalHeritage.Document.IssueOrgan = v?.ToString());
			
			AddAttributeToFlat(flatSection.CulturalHeritage?.Document?.DescAttributeIdValue, 
				current => ((xmlObjectFlat)current).CulturalHeritage?.Document?.Desc,
				(o, v) => o.CulturalHeritage.Document.Desc = v?.ToString());
		}

		private xmlLevel InitFlatLevel(xmlObject o, int iCounter)
		{
			var element = o.Levels.ElementAtOrDefault(iCounter);
			if (element != null)
				return element;

			element = new xmlLevel();
			o.Levels.Insert(iCounter, element);

			return element;
		}

		private xmlSubBuildingFlat InitSubFlat(xmlObject o, int iCounter)
		{
			var element = o.SubBuildingFlats.ElementAtOrDefault(iCounter);
			if (element != null)
				return element;

			element = new xmlSubBuildingFlat();
			o.SubBuildingFlats.Insert(iCounter, element);

			return element;
		}

		private xmlEncumbranceOks InitEncumbrancesObjectForFlat(xmlObject o, int iCounter, int jCounter)
		{
			var subFlat = InitSubFlat(o, iCounter);
			var encumbrances = subFlat.EncumbrancesOks;
			var element = encumbrances.ElementAtOrDefault(jCounter);
			if (element != null)
				return element;

			element = new xmlEncumbranceOks();
			encumbrances.Insert(iCounter, element);

			return element;
		}

		private void AddAttributeToFlat(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue, Func<xmlObjectParticular, bool> canSetValue = null)
		{
			AddGknAttribute(Flat, attributeId, getValue, setValue, canSetValue);
		}

		#endregion


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
