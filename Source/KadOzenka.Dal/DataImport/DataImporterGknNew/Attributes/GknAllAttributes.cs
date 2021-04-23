using System;
using System.Collections.Generic;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using KadOzenka.Dal.XmlParser;

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
				current => current.CadastralCost?.Value, (o, v) => o.CadastralCost.Value = (double?)v);

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
				current => xmlAdress.GetTextAdress(current.Adress), (o, v) => o.Adress = new xmlAdress
				{
					AddressStr= v?.ToString()
				});

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
		}

		private void AddAttributeToGeneral(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue)
		{
			AddGknAttribute(General, attributeId, getValue, setValue);
		}

		private void AddAttributeToParcel(long? attributeId, Func<xmlObjectParticular, object> getValue,
			Action<xmlObject, object> setValue)
		{
			AddGknAttribute(Parcel, attributeId, getValue, setValue);
		}

		private void AddGknAttribute(List<ImportedAttributeGkn> attributes, long? attributeId, 
			Func<xmlObjectParticular, object> getValue, Action<xmlObject, object> setValue)
		{
			if (!attributeId.HasValue)
				return;

			attributes.Add(new ImportedAttributeGkn(attributeId.Value, getValue, setValue));
		}

		#endregion
	}
}
