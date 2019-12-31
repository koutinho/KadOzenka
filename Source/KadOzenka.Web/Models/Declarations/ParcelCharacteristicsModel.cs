using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class ParcelCharacteristicsModel
	{
		public const string ConnectionToPowerGridsName = "Наличие/отсутствие подключения к электрическим сетям инженерно-технического обеспечения";
		public const string AvailabilityConnectionToPowerGridsName = "Возможность/отсутствие возможности подключения к сетям";
		public const string ConnectionToGasGridsName = "Наличие/отсутствие подключения к сетям газораспределения";
		public const string AvailabilityConnectionToGasGridsName = "Возможность/отсутствие возможности подключения к сетям газораспределения";
		public const string GasPowerName = "Наличие/отсутствие централизованного подключения к системе водоснабжения";
		public const string ConnectionToWaterSupplyName = "Наличие/отсутствие централизованного подключения к системе водоснабжения";
		public const string AvailabilityConnectionToWaterSupplyName = "Возможность/отсутствие возможности подключения к системе водоснабжения";
		public const string ConnectionToHeatSupplyName = "Наличие/отсутствие централизованного подключения к системе теплоснабжения";
		public const string AvailabilityConnectionToHeatSupplyName = "Возможность/отсутствие возможности подключения к системе теплоснабжения";
		public const string ConnectionToWaterDisposalName = "Наличие/отсутствие централизованного подключения к системе водоотведения";
		public const string AvailabilityConnectionToWaterDisposalName = "Возможность/отсутствие возможности подключения к системе водоотведения";

		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Идентификатор декларации (DECLARATION_ID)
		/// </summary>
		[Required(ErrorMessage = "Идентификатор декларации не указан")]
		public long DeclarationId { get; set; }

		/// <summary>
		/// Адрес земельного участка (HAR_1)
		/// </summary>
		[Display(Name = "Адрес земельного участка (описание местоположения земельного участка)")]
		public string Address { get; set; }

		/// <summary>
		/// Площадь (HAR_2)
		/// </summary>
		[Display(Name = "Площадь")]
		public decimal? Square { get; set; }

		/// <summary>
		/// Категория земель (HAR_3)
		/// </summary>
		[Display(Name = "Категория земель")]
		public string LandCategory { get; set; }

		/// <summary>
		/// Вид разрешенного использования (HAR_4)
		/// </summary>
		[Display(Name = "Вид разрешенного использования")]
		public string PermittedUseType { get; set; }

		/// <summary>
		/// Фактическое использование земельного участка (HAR_5)
		/// </summary>
		[Display(Name = "Фактическое использование земельного участка, соответствующее виду разрешенного использования")]
		public string FactUse { get; set; }

		/// <summary>
		/// Сведения о лесах, водных объектах и об иных природных объектах (HAR_6)
		/// </summary>
		[Display(Name = "Сведения о лесах, водных объектах и об иных природных объектах, расположенных в пределах земельного участка")]
		public string NaturalObjectsInformation { get; set; }

		/// <summary>
		/// Сведения о том, что земельный участок полностью или частично расположен в границах зоны с особыми условиями (HAR_7)
		/// </summary>
		[Display(Name = "Сведения о том, что земельный участок полностью или частично расположен в границах зоны с особыми условиями использования территории или территории объекта культурного наследия")]
		public string ZonesWithSpecialUsingConditionsInformation { get; set; }

		/// <summary>
		/// Сведения о том, что земельный участок расположен в границах особо охраняемой природной территории (HAR_8)
		/// </summary>
		[Display(Name = "Сведения о том, что земельный участок расположен в границах особо охраняемой природной территории, охотничьих угодий, лесничеств, лесопарков")]
		public string ProtectedNaturalZonesInformation { get; set; }

		/// <summary>
		/// Сведения о том, что земельный участок расположен в границах особой экономической зоны (HAR_9)
		/// </summary>
		[Display(Name = "Сведения о том, что земельный участок расположен в границах особой экономической зоны, территории опережающего развития, зоны территориального развития в Российской Федерации, игровой зоны")]
		public string EconomicZonesInformation { get; set; }

		/// <summary>
		/// Сведения об установленных сервитутах (HAR_10)
		/// </summary>
		[Display(Name = "Сведения об установленных сервитутах, публичных сервитутах")]
		public string EstablishedEasementsInformation { get; set; }

		/// <summary>
		/// Удаленность от автомобильных дорог с твердым покрытием (HAR_11)
		/// </summary>
		[Display(Name = "Удаленность от автомобильных дорог с твердым покрытием")]
		public string DistanceFromPavedRoads { get; set; }

		/// <summary>
		/// Сведения о наличии/отсутствии подъездных путей (HAR_12)
		/// </summary>
		[Display(Name = "Сведения о наличии/отсутствии подъездных путей")]
		public string PrecenceOfAccessRoads { get; set; }

		/// <summary>
		/// Описание коммуникаций, в том числе их удаленность  (HAR_13)
		/// </summary>
		[Display(Name = "Описание коммуникаций, в том числе их удаленность")]
		public string CommunicationsDescription { get; set; }

		/// <summary>
		/// Наличие/отсутствие подключения к электрическим сетям (HAR_13_1_1)
		/// </summary>
		[Display(Name = ConnectionToPowerGridsName)]
		public HarAvailability? ConnectionToPowerGrids { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к сетям (HAR_13_1_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToPowerGridsName)]
		public HarAvailability? AvailabilityConnectionToPowerGrids { get; set; }

		/// <summary>
		/// Мощность электрической сети (HAR_13_1_3)
		/// </summary>
		[Display(Name = "Мощность электрической сети")]
		public string ElectricPower { get; set; }

		/// <summary>
		/// Наличие/отсутствие подключения к сетям газораспределения (HAR_13_2_1)
		/// </summary>
		[Display(Name = ConnectionToGasGridsName)]
		public HarAvailability? ConnectionToGasGrids { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к сетям газораспределения (HAR_13_2_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToGasGridsName)]
		public HarAvailability? AvailabilityConnectionToGasGrids { get; set; }

		/// <summary>
		/// Мощность электрической сети (HAR_13_2_3)
		/// </summary>
		[Display(Name = "Мощность сетей газораспределения ")]
		public string GasPower { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе водоснабжения (HAR_13_3_1)
		/// </summary>
		[Display(Name = ConnectionToWaterSupplyName)]
		public HarAvailability? ConnectionToWaterSupply { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе водоснабжения (HAR_13_3_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToWaterSupplyName)]
		public HarAvailability? AvailabilityConnectionToWaterSupply { get; set; }

		/// <summary>
		/// Наличие/отсутствие централизованного подключения к системе теплоснабжения (HAR_13_4_1)
		/// </summary>
		[Display(Name = ConnectionToHeatSupplyName)]
		public HarAvailability? ConnectionToHeatSupply { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе теплоснабжения (HAR_13_4_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToHeatSupplyName)]
		public HarAvailability? AvailabilityConnectionToHeatSupply { get; set; }

		/// <summary>
		/// Наличие/отсутствие централизованного подключения к системе теплоснабжения (HAR_13_5_1)
		/// </summary>
		[Display(Name = ConnectionToWaterDisposalName)]
		public HarAvailability? ConnectionToWaterDisposal { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе теплоснабжения (HAR_13_5_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToWaterDisposalName)]
		public HarAvailability? AvailabilityConnectionToWaterDisposal { get; set; }

		/// <summary>
		/// Удаленность относительно ближайшего водного объекта (HAR_14)
		/// </summary>
		[Display(Name = "Удаленность относительно ближайшего водного объекта")]
		public string NearestWaterbodyDistance { get; set; }

		/// <summary>
		/// Удаленность относительно ближайшей рекреационной зоны (HAR_15)
		/// </summary>
		[Display(Name = "Удаленность относительно ближайшей рекреационной зоны")]
		public string NearestRecreationalZoneDistance { get; set; }

		/// <summary>
		/// Удаленность относительно железных дорог (HAR_16)
		/// </summary>
		[Display(Name = "Удаленность относительно железных дорог")]
		public string RailwaysDistance { get; set; }

		/// <summary>
		/// Удаленность относительно железнодорожных вокзалов (станций) (HAR_17)
		/// </summary>
		[Display(Name = "Удаленность относительно железнодорожных вокзалов (станций)")]
		public string RailwayStationsDistance { get; set; }

		/// <summary>
		/// Удаленность от зоны разработки полезных ископаемых (HAR_18)
		/// </summary>
		[Display(Name = "Удаленность от зоны разработки полезных ископаемых, зоны особого режима использования в границах земельных участков, промышленной зоны")]
		public string MiningZoneDistance { get; set; }

		/// <summary>
		/// Вид угодий (HAR_19)
		/// </summary>
		[Display(Name = "Вид угодий")]
		public string TypeOfLand { get; set; }

		/// <summary>
		/// Показатели состояния почв (HAR_20)
		/// </summary>
		[Display(Name = "Показатели состояния почв")]
		public string SoilConditionIndicators { get; set; }

		/// <summary>
		/// Наличие недостатков, препятствующих рациональному использованию и охране земель (HAR_21)
		/// </summary>
		[Display(Name = "Наличие недостатков, препятствующих рациональному использованию и охране земель")]
		public string DisadvantagesPresence { get; set; }

		public string GetAcceptedCharacteristics()
		{
			return GetCharacteristicsString(HarAvailability.Exists);
		}

		public string GetRejectedCharacteristics()
		{
			return GetCharacteristicsString(HarAvailability.NotExists);
		}

		private string GetCharacteristicsString(HarAvailability harAvailabilityType)
		{
			var result = new List<string>();
			if (ConnectionToPowerGrids.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(ConnectionToPowerGridsName);
			}
			if (AvailabilityConnectionToPowerGrids.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(AvailabilityConnectionToPowerGridsName);
			}
			if (ConnectionToGasGrids.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(ConnectionToGasGridsName);
			}
			if (AvailabilityConnectionToGasGrids.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(AvailabilityConnectionToGasGridsName);
			}
			if (ConnectionToWaterSupply.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(ConnectionToWaterSupplyName);
			}
			if (AvailabilityConnectionToWaterSupply.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(AvailabilityConnectionToWaterSupplyName);
			}
			if (ConnectionToHeatSupply.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(ConnectionToHeatSupplyName);
			}
			if (AvailabilityConnectionToHeatSupply.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(AvailabilityConnectionToHeatSupplyName);
			}
			if (ConnectionToWaterDisposal.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(ConnectionToWaterDisposalName);
			}
			if (AvailabilityConnectionToWaterDisposal.GetValueOrDefault() == harAvailabilityType)
			{
				result.Add(AvailabilityConnectionToWaterDisposalName);
			}

			return string.Join(",\n", result);
		}

		public static ParcelCharacteristicsModel FromEntity(OMHarParcel entity)
		{
			if (entity == null)
			{
				return new ParcelCharacteristicsModel
				{
					Id = -1,
				};
			}

			return new ParcelCharacteristicsModel
			{
				Id = entity.Id,
				Address = entity.Har_1,
				Square = entity.Har_2,
				LandCategory = entity.Har_3,
				PermittedUseType = entity.Har_4,
				FactUse = entity.Har_5,
				NaturalObjectsInformation = entity.Har_6,
				ZonesWithSpecialUsingConditionsInformation = entity.Har_7,
				ProtectedNaturalZonesInformation = entity.Har_8,
				EconomicZonesInformation = entity.Har_9,
				EstablishedEasementsInformation = entity.Har_10,
				DistanceFromPavedRoads = entity.Har_11,
				PrecenceOfAccessRoads = entity.Har_12,
				CommunicationsDescription = entity.Har_13,
				ConnectionToPowerGrids = entity.Har_13_1_1_Code,
				AvailabilityConnectionToPowerGrids = entity.Har_13_1_2_Code,
				ElectricPower = entity.Har_13_1_3,
				ConnectionToGasGrids = entity.Har_13_2_1_Code,
				AvailabilityConnectionToGasGrids = entity.Har_13_2_2_Code,
				GasPower = entity.Har_13_2_3,
				ConnectionToWaterSupply = entity.Har_13_3_1_Code,
				AvailabilityConnectionToWaterSupply = entity.Har_13_3_2_Code,
				ConnectionToHeatSupply = entity.Har_13_4_1_Code,
				AvailabilityConnectionToHeatSupply = entity.Har_13_4_2_Code,
				ConnectionToWaterDisposal = entity.Har_13_5_1_Code,
				AvailabilityConnectionToWaterDisposal = entity.Har_13_5_2_Code,
				NearestWaterbodyDistance = entity.Har_14,
				NearestRecreationalZoneDistance = entity.Har_15,
				RailwaysDistance = entity.Har_16,
				RailwayStationsDistance = entity.Har_17,
				MiningZoneDistance = entity.Har_18,
				TypeOfLand = entity.Har_19,
				SoilConditionIndicators = entity.Har_20,
				DisadvantagesPresence = entity.Har_21,
			};
		}

		public static void ToEntity(ParcelCharacteristicsModel parcelCharacteristicsViewModel, ref OMHarParcel entity)
		{
			entity.Declaration_Id = parcelCharacteristicsViewModel.DeclarationId;
			entity.Har_1 = parcelCharacteristicsViewModel.Address;
			entity.Har_2 = parcelCharacteristicsViewModel.Square;
			entity.Har_3 = parcelCharacteristicsViewModel.LandCategory;
			entity.Har_4 = parcelCharacteristicsViewModel.PermittedUseType;
			entity.Har_5 = parcelCharacteristicsViewModel.FactUse;
			entity.Har_6 = parcelCharacteristicsViewModel.NaturalObjectsInformation;
			entity.Har_7 = parcelCharacteristicsViewModel.ZonesWithSpecialUsingConditionsInformation;
			entity.Har_8 = parcelCharacteristicsViewModel.ProtectedNaturalZonesInformation;
			entity.Har_9 = parcelCharacteristicsViewModel.EconomicZonesInformation;
			entity.Har_10 = parcelCharacteristicsViewModel.EstablishedEasementsInformation;
			entity.Har_11 = parcelCharacteristicsViewModel.DistanceFromPavedRoads;
			entity.Har_12 = parcelCharacteristicsViewModel.PrecenceOfAccessRoads;
			entity.Har_13 = parcelCharacteristicsViewModel.CommunicationsDescription;
			entity.Har_13_1_1_Code = parcelCharacteristicsViewModel.ConnectionToPowerGrids.GetValueOrDefault();
			entity.Har_13_1_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToPowerGrids.GetValueOrDefault();
			entity.Har_13_1_3 = parcelCharacteristicsViewModel.ElectricPower;
			entity.Har_13_2_1_Code = parcelCharacteristicsViewModel.ConnectionToGasGrids.GetValueOrDefault();
			entity.Har_13_2_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToGasGrids.GetValueOrDefault();
			entity.Har_13_2_3 = parcelCharacteristicsViewModel.GasPower;
			entity.Har_13_3_1_Code = parcelCharacteristicsViewModel.ConnectionToWaterSupply.GetValueOrDefault();
			entity.Har_13_3_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToWaterSupply.GetValueOrDefault();
			entity.Har_13_4_1_Code = parcelCharacteristicsViewModel.ConnectionToHeatSupply.GetValueOrDefault();
			entity.Har_13_4_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToHeatSupply.GetValueOrDefault();
			entity.Har_13_5_1_Code = parcelCharacteristicsViewModel.ConnectionToWaterDisposal.GetValueOrDefault();
			entity.Har_13_5_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToWaterDisposal.GetValueOrDefault();
			entity.Har_14 = parcelCharacteristicsViewModel.NearestWaterbodyDistance;
			entity.Har_15 = parcelCharacteristicsViewModel.NearestRecreationalZoneDistance;
			entity.Har_16 = parcelCharacteristicsViewModel.RailwaysDistance;
			entity.Har_17 = parcelCharacteristicsViewModel.RailwayStationsDistance;
			entity.Har_18 = parcelCharacteristicsViewModel.MiningZoneDistance;
			entity.Har_19 = parcelCharacteristicsViewModel.TypeOfLand;
			entity.Har_20 = parcelCharacteristicsViewModel.SoilConditionIndicators;
			entity.Har_21 = parcelCharacteristicsViewModel.DisadvantagesPresence;
		}
	}
}
