using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Адрес земельного участка (описание местоположения земельного участка)' составляет 4096 символа")]
		public CharacteristicModel Address { get; set; }

		/// <summary>
		/// Площадь (HAR_2)
		/// </summary>
		[Display(Name = "Площадь")]
		public CharacteristicModel Square { get; set; }

		/// <summary>
		/// Категория земель (HAR_3)
		/// </summary>
		[Display(Name = "Категория земель")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Категория земель' составляет 4096 символа")]
		public CharacteristicModel LandCategory { get; set; }

		/// <summary>
		/// Вид разрешенного использования (HAR_4)
		/// </summary>
		[Display(Name = "Вид разрешенного использования")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Вид разрешенного использования' составляет 4096 символа")]
		public CharacteristicModel PermittedUseType { get; set; }

		/// <summary>
		/// Фактическое использование земельного участка (HAR_5)
		/// </summary>
		[Display(Name = "Фактическое использование земельного участка, соответствующее виду разрешенного использования")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Фактическое использование земельного участка, соответствующее виду разрешенного использования' составляет 4096 символа")]
		public CharacteristicModel FactUse { get; set; }

		/// <summary>
		/// Сведения о лесах, водных объектах и об иных природных объектах (HAR_6)
		/// </summary>
		[Display(Name = "Сведения о лесах, водных объектах и об иных природных объектах, расположенных в пределах земельного участка")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Сведения о лесах, водных объектах и об иных природных объектах, расположенных в пределах земельного участка' составляет 4096 символа")]
		public CharacteristicModel NaturalObjectsInformation { get; set; }

		/// <summary>
		/// Сведения о том, что земельный участок полностью или частично расположен в границах зоны с особыми условиями (HAR_7)
		/// </summary>
		[Display(Name = "Сведения о том, что земельный участок полностью или частично расположен в границах зоны с особыми условиями использования территории или территории объекта культурного наследия")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Сведения о том, что земельный участок полностью или частично расположен в границах зоны с особыми условиями использования территории или территории объекта культурного наследия' составляет 4096 символа")]
		public CharacteristicModel ZonesWithSpecialUsingConditionsInformation { get; set; }

		/// <summary>
		/// Сведения о том, что земельный участок расположен в границах особо охраняемой природной территории (HAR_8)
		/// </summary>
		[Display(Name = "Сведения о том, что земельный участок расположен в границах особо охраняемой природной территории, охотничьих угодий, лесничеств, лесопарков")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Сведения о том, что земельный участок расположен в границах особо охраняемой природной территории, охотничьих угодий, лесничеств, лесопарков' составляет 4096 символа")]
		public CharacteristicModel ProtectedNaturalZonesInformation { get; set; }

		/// <summary>
		/// Сведения о том, что земельный участок расположен в границах особой экономической зоны (HAR_9)
		/// </summary>
		[Display(Name = "Сведения о том, что земельный участок расположен в границах особой экономической зоны, территории опережающего развития, зоны территориального развития в Российской Федерации, игровой зоны")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Сведения о том, что земельный участок расположен в границах особой экономической зоны, территории опережающего развития, зоны территориального развития в Российской Федерации, игровой зоны' составляет 4096 символа")]
		public CharacteristicModel EconomicZonesInformation { get; set; }

		/// <summary>
		/// Сведения об установленных сервитутах (HAR_10)
		/// </summary>
		[Display(Name = "Сведения об установленных сервитутах, публичных сервитутах")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Сведения об установленных сервитутах, публичных сервитутах' составляет 4096 символа")]
		public CharacteristicModel EstablishedEasementsInformation { get; set; }

		/// <summary>
		/// Удаленность от автомобильных дорог с твердым покрытием (HAR_11)
		/// </summary>
		[Display(Name = "Удаленность от автомобильных дорог с твердым покрытием")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Удаленность от автомобильных дорог с твердым покрытием' составляет 4096 символа")]
		public CharacteristicModel DistanceFromPavedRoads { get; set; }

		/// <summary>
		/// Сведения о наличии/отсутствии подъездных путей (HAR_12)
		/// </summary>
		[Display(Name = "Сведения о наличии/отсутствии подъездных путей")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Сведения о наличии/отсутствии подъездных путей' составляет 4096 символа")]
		public CharacteristicModel PrecenceOfAccessRoads { get; set; }

		/// <summary>
		/// Описание коммуникаций, в том числе их удаленность  (HAR_13)
		/// </summary>
		[Display(Name = "Описание коммуникаций, в том числе их удаленность")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Описание коммуникаций, в том числе их удаленность' составляет 4096 символа")]
		public CharacteristicModel CommunicationsDescription { get; set; }

		/// <summary>
		/// Наличие/отсутствие подключения к электрическим сетям (HAR_13_1_1)
		/// </summary>
		[Display(Name = ConnectionToPowerGridsName)]
		public CharacteristicModel ConnectionToPowerGrids { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к сетям (HAR_13_1_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToPowerGridsName)]
		public CharacteristicModel AvailabilityConnectionToPowerGrids { get; set; }

		/// <summary>
		/// Мощность электрической сети (HAR_13_1_3)
		/// </summary>
		[Display(Name = "Мощность электрической сети")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Мощность электрической сети' составляет 4096 символа")]
		public CharacteristicModel ElectricPower { get; set; }

		/// <summary>
		/// Наличие/отсутствие подключения к сетям газораспределения (HAR_13_2_1)
		/// </summary>
		[Display(Name = ConnectionToGasGridsName)]
		public CharacteristicModel ConnectionToGasGrids { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к сетям газораспределения (HAR_13_2_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToGasGridsName)]
		public CharacteristicModel AvailabilityConnectionToGasGrids { get; set; }

		/// <summary>
		/// Мощность электрической сети (HAR_13_2_3)
		/// </summary>
		[Display(Name = "Мощность сетей газораспределения ")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Мощность сетей газораспределения' составляет 4096 символа")]
		public CharacteristicModel GasPower { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе водоснабжения (HAR_13_3_1)
		/// </summary>
		[Display(Name = ConnectionToWaterSupplyName)]
		public CharacteristicModel ConnectionToWaterSupply { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе водоснабжения (HAR_13_3_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToWaterSupplyName)]
		public CharacteristicModel AvailabilityConnectionToWaterSupply { get; set; }

		/// <summary>
		/// Наличие/отсутствие централизованного подключения к системе теплоснабжения (HAR_13_4_1)
		/// </summary>
		[Display(Name = ConnectionToHeatSupplyName)]
		public CharacteristicModel ConnectionToHeatSupply { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе теплоснабжения (HAR_13_4_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToHeatSupplyName)]
		public CharacteristicModel AvailabilityConnectionToHeatSupply { get; set; }

		/// <summary>
		/// Наличие/отсутствие централизованного подключения к системе теплоснабжения (HAR_13_5_1)
		/// </summary>
		[Display(Name = ConnectionToWaterDisposalName)]
		public CharacteristicModel ConnectionToWaterDisposal { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе теплоснабжения (HAR_13_5_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToWaterDisposalName)]
		public CharacteristicModel AvailabilityConnectionToWaterDisposal { get; set; }

		/// <summary>
		/// Удаленность относительно ближайшего водного объекта (HAR_14)
		/// </summary>
		[Display(Name = "Удаленность относительно ближайшего водного объекта")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Удаленность относительно ближайшего водного объекта' составляет 4096 символа")]
		public CharacteristicModel NearestWaterbodyDistance { get; set; }

		/// <summary>
		/// Удаленность относительно ближайшей рекреационной зоны (HAR_15)
		/// </summary>
		[Display(Name = "Удаленность относительно ближайшей рекреационной зоны")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Удаленность относительно ближайшей рекреационной зоны' составляет 4096 символа")]
		public CharacteristicModel NearestRecreationalZoneDistance { get; set; }

		/// <summary>
		/// Удаленность относительно железных дорог (HAR_16)
		/// </summary>
		[Display(Name = "Удаленность относительно железных дорог")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Удаленность относительно железных дорог' составляет 4096 символа")]
		public CharacteristicModel RailwaysDistance { get; set; }

		/// <summary>
		/// Удаленность относительно железнодорожных вокзалов (станций) (HAR_17)
		/// </summary>
		[Display(Name = "Удаленность относительно железнодорожных вокзалов (станций)")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Удаленность относительно железнодорожных вокзалов (станций)' составляет 4096 символа")]
		public CharacteristicModel RailwayStationsDistance { get; set; }

		/// <summary>
		/// Удаленность от зоны разработки полезных ископаемых (HAR_18)
		/// </summary>
		[Display(Name = "Удаленность от зоны разработки полезных ископаемых, зоны особого режима использования в границах земельных участков, промышленной зоны")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Удаленность от зоны разработки полезных ископаемых, зоны особого режима использования в границах земельных участков, промышленной зоны' составляет 4096 символа")]
		public CharacteristicModel MiningZoneDistance { get; set; }

		/// <summary>
		/// Вид угодий (HAR_19)
		/// </summary>
		[Display(Name = "Вид угодий")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Вид угодий' составляет 4096 символа")]
		public CharacteristicModel TypeOfLand { get; set; }

		/// <summary>
		/// Показатели состояния почв (HAR_20)
		/// </summary>
		[Display(Name = "Показатели состояния почв")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Показатели состояния почв' составляет 4096 символа")]
		public CharacteristicModel SoilConditionIndicators { get; set; }

		/// <summary>
		/// Наличие недостатков, препятствующих рациональному использованию и охране земель (HAR_21)
		/// </summary>
		[Display(Name = "Наличие недостатков, препятствующих рациональному использованию и охране земель")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Наличие недостатков, препятствующих рациональному использованию и охране земель' составляет 4096 символа")]
		public CharacteristicModel DisadvantagesPresence { get; set; }

		public bool IsEditDeclarationCharacteristics { get; set; }
		public bool CanIncludeInFormalChecking { get; set; }

		public string GetAcceptedCharacteristics()
		{
			var result = new List<string>();
			IList<PropertyInfo> props = new List<PropertyInfo>(this.GetType().GetProperties());
			foreach (PropertyInfo prop in props)
			{
				if (prop.PropertyType == typeof(CharacteristicModel))
				{
					var propValue = prop.GetValue(this, null) as CharacteristicModel;
					if (propValue != null && propValue.AdditionalInfo.IsShownInDeclaration.GetValueOrDefault(false)
					                      && propValue.AdditionalInfo.HarStatus.GetValueOrDefault(HarStatus.None) == HarStatus.Accepted)
					{
						object[] attrs = prop.GetCustomAttributes(true);
						foreach (object attr in attrs)
						{
							DisplayAttribute displayAttribute = attr as DisplayAttribute;
							if (displayAttribute != null)
							{
								result.Add(displayAttribute.Name);
							}
						}
					}
				}
			}
			var resultString = string.Join(",\n", result);

			return string.IsNullOrEmpty(resultString) ? null : resultString;
		}

		public string GetRejectedCharacteristics()
		{
			var result = new List<string>();
			IList<PropertyInfo> props = new List<PropertyInfo>(this.GetType().GetProperties());
			foreach (PropertyInfo prop in props)
			{
				if (prop.PropertyType == typeof(CharacteristicModel))
				{
					var propValue = prop.GetValue(this, null) as CharacteristicModel;
					if (propValue != null && propValue.AdditionalInfo.IsShownInDeclaration.GetValueOrDefault(false)
					                      && propValue.AdditionalInfo.HarStatus.GetValueOrDefault(HarStatus.None) == HarStatus.Rejected)
					{
						object[] attrs = prop.GetCustomAttributes(true);
						foreach (object attr in attrs)
						{
							DisplayAttribute displayAttribute = attr as DisplayAttribute;
							if (displayAttribute != null)
							{
								result.Add(displayAttribute.Name);
							}
						}
					}
				}
			}
			var resultString = string.Join(",\n", result);

			return string.IsNullOrEmpty(resultString) ? null : resultString;
		}

		public static ParcelCharacteristicsModel FromEntity(OMHarParcel entity, List<OMHarParcelAdditionalInfo> additionalInfos)
		{
			return new ParcelCharacteristicsModel
			{
				Id = entity == null ? -1 : entity.Id,
				Address = new CharacteristicModel { Value = entity?.Har_1, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_1))) },
				Square = new CharacteristicModel { Value = entity?.Har_2, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_2))) },
				LandCategory = new CharacteristicModel { Value = entity?.Har_3, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_3))) },
				PermittedUseType = new CharacteristicModel { Value = entity?.Har_4, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_4))) },
				FactUse = new CharacteristicModel { Value = entity?.Har_5, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_5))) },
				NaturalObjectsInformation = new CharacteristicModel { Value = entity?.Har_6, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_6))) },
				ZonesWithSpecialUsingConditionsInformation = new CharacteristicModel { Value = entity?.Har_7, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_7))) },
				ProtectedNaturalZonesInformation = new CharacteristicModel { Value = entity?.Har_8, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_8))) },
				EconomicZonesInformation = new CharacteristicModel { Value = entity?.Har_9, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_9))) },
				EstablishedEasementsInformation = new CharacteristicModel { Value = entity?.Har_10, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_10))) },
				DistanceFromPavedRoads = new CharacteristicModel { Value = entity?.Har_11, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_11))) },
				PrecenceOfAccessRoads = new CharacteristicModel { Value = entity?.Har_12, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_12))) },
				CommunicationsDescription = new CharacteristicModel { Value = entity?.Har_13, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13))) },
				ConnectionToPowerGrids = new CharacteristicModel { Value = entity?.Har_13_1_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_1_1))) },
				AvailabilityConnectionToPowerGrids = new CharacteristicModel { Value = entity?.Har_13_1_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_2_1))) },
				ElectricPower = new CharacteristicModel { Value = entity?.Har_13_1_3, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_1_3))) },
				ConnectionToGasGrids = new CharacteristicModel { Value = entity?.Har_13_2_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_2_1))) },
				AvailabilityConnectionToGasGrids = new CharacteristicModel { Value = entity?.Har_13_2_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_2_2))) },
				GasPower = new CharacteristicModel { Value = entity?.Har_13_2_3, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_2_3))) },
				ConnectionToWaterSupply = new CharacteristicModel { Value = entity?.Har_13_3_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_3_1))) },
				AvailabilityConnectionToWaterSupply = new CharacteristicModel { Value = entity?.Har_13_3_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_3_2))) },
				ConnectionToHeatSupply = new CharacteristicModel { Value = entity?.Har_13_4_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_4_1))) },
				AvailabilityConnectionToHeatSupply = new CharacteristicModel { Value = entity?.Har_13_4_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_4_2))) },
				ConnectionToWaterDisposal = new CharacteristicModel { Value = entity?.Har_13_5_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_5_1))) },
				AvailabilityConnectionToWaterDisposal = new CharacteristicModel { Value = entity?.Har_13_5_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_13_5_2))) },
				NearestWaterbodyDistance = new CharacteristicModel { Value = entity?.Har_14, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_14))) },
				NearestRecreationalZoneDistance = new CharacteristicModel { Value = entity?.Har_15, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_15))) },
				RailwaysDistance = new CharacteristicModel { Value = entity?.Har_16, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_16))) },
				RailwayStationsDistance = new CharacteristicModel { Value = entity?.Har_17, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_17))) },
				MiningZoneDistance = new CharacteristicModel { Value = entity?.Har_18, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_18))) },
				TypeOfLand = new CharacteristicModel { Value = entity?.Har_19, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_19))) },
				SoilConditionIndicators = new CharacteristicModel { Value = entity?.Har_20, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_20))) },
				DisadvantagesPresence = new CharacteristicModel { Value = entity?.Har_21, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarParcelName == nameof(entity.Har_21))) },
			};
		}

		public static void ToEntity(ParcelCharacteristicsModel parcelCharacteristicsViewModel, ref OMHarParcel entity, 
			ref List<OMHarParcelAdditionalInfo> characteristicAdditionalInfo)
		{
			entity.Declaration_Id = parcelCharacteristicsViewModel.DeclarationId;

			entity.Har_1 = parcelCharacteristicsViewModel.Address.StringValue;
			var harName = nameof(entity.Har_1);
			var harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.Address.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.Address.AdditionalInfo, ref harInfo);
			}

			entity.Har_2 = parcelCharacteristicsViewModel.Square.DecimalValue;
			harName = nameof(entity.Har_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.Square.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.Square.AdditionalInfo, ref harInfo);
			}

			entity.Har_3 = parcelCharacteristicsViewModel.LandCategory.StringValue;
			harName = nameof(entity.Har_3);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.LandCategory.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.LandCategory.AdditionalInfo, ref harInfo);
			}

			entity.Har_4 = parcelCharacteristicsViewModel.PermittedUseType.StringValue;
			harName = nameof(entity.Har_4);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.PermittedUseType.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.PermittedUseType.AdditionalInfo, ref harInfo);
			}

			entity.Har_5 = parcelCharacteristicsViewModel.FactUse.StringValue;
			harName = nameof(entity.Har_5);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.FactUse.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.FactUse.AdditionalInfo, ref harInfo);
			}

			entity.Har_6 = parcelCharacteristicsViewModel.NaturalObjectsInformation.StringValue;
			harName = nameof(entity.Har_6);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.NaturalObjectsInformation.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.NaturalObjectsInformation.AdditionalInfo, ref harInfo);
			}

			entity.Har_7 = parcelCharacteristicsViewModel.ZonesWithSpecialUsingConditionsInformation.StringValue;
			harName = nameof(entity.Har_7);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ZonesWithSpecialUsingConditionsInformation.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ZonesWithSpecialUsingConditionsInformation.AdditionalInfo, ref harInfo);
			}

			entity.Har_8 = parcelCharacteristicsViewModel.ProtectedNaturalZonesInformation.StringValue;
			harName = nameof(entity.Har_8);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ProtectedNaturalZonesInformation.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ProtectedNaturalZonesInformation.AdditionalInfo, ref harInfo);
			}

			entity.Har_9 = parcelCharacteristicsViewModel.EconomicZonesInformation.StringValue;
			harName = nameof(entity.Har_9);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.EconomicZonesInformation.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.EconomicZonesInformation.AdditionalInfo, ref harInfo);
			}

			entity.Har_10 = parcelCharacteristicsViewModel.EstablishedEasementsInformation.StringValue;
			harName = nameof(entity.Har_10);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.EstablishedEasementsInformation.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.EstablishedEasementsInformation.AdditionalInfo, ref harInfo);
			}

			entity.Har_11 = parcelCharacteristicsViewModel.DistanceFromPavedRoads.StringValue;
			harName = nameof(entity.Har_11);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DistanceFromPavedRoads.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DistanceFromPavedRoads.AdditionalInfo, ref harInfo);
			}

			entity.Har_12 = parcelCharacteristicsViewModel.PrecenceOfAccessRoads.StringValue;
			harName = nameof(entity.Har_12);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.PrecenceOfAccessRoads.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.PrecenceOfAccessRoads.AdditionalInfo, ref harInfo);
			}

			entity.Har_13 = parcelCharacteristicsViewModel.CommunicationsDescription.StringValue;
			harName = nameof(entity.Har_13);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.CommunicationsDescription.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.CommunicationsDescription.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_1_1_Code = parcelCharacteristicsViewModel.ConnectionToPowerGrids.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_1_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToPowerGrids.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToPowerGrids.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_1_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToPowerGrids.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_1_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToPowerGrids.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToPowerGrids.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_1_3 = parcelCharacteristicsViewModel.ElectricPower.StringValue;
			harName = nameof(entity.Har_13_1_3);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ElectricPower.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ElectricPower.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_2_1_Code = parcelCharacteristicsViewModel.ConnectionToGasGrids.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_2_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToGasGrids.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToGasGrids.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_2_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToGasGrids.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_2_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToGasGrids.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToGasGrids.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_2_3 = parcelCharacteristicsViewModel.GasPower.StringValue;
			harName = nameof(entity.Har_13_2_3);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.GasPower.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.GasPower.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_3_1_Code = parcelCharacteristicsViewModel.ConnectionToWaterSupply.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_3_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToWaterSupply.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToWaterSupply.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_3_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToWaterSupply.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_3_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToWaterSupply.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToWaterSupply.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_4_1_Code = parcelCharacteristicsViewModel.ConnectionToHeatSupply.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_4_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToHeatSupply.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToHeatSupply.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_4_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToHeatSupply.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_4_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToHeatSupply.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToHeatSupply.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_5_1_Code = parcelCharacteristicsViewModel.ConnectionToWaterDisposal.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_5_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToWaterDisposal.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToWaterDisposal.AdditionalInfo, ref harInfo);
			}

			entity.Har_13_5_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToWaterDisposal.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_13_5_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToWaterDisposal.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToWaterDisposal.AdditionalInfo, ref harInfo);
			}

			entity.Har_14 = parcelCharacteristicsViewModel.NearestWaterbodyDistance.StringValue;
			harName = nameof(entity.Har_14);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.NearestWaterbodyDistance.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.NearestWaterbodyDistance.AdditionalInfo, ref harInfo);
			}

			entity.Har_15 = parcelCharacteristicsViewModel.NearestRecreationalZoneDistance.StringValue;
			harName = nameof(entity.Har_15);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.NearestRecreationalZoneDistance.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.NearestRecreationalZoneDistance.AdditionalInfo, ref harInfo);
			}

			entity.Har_16 = parcelCharacteristicsViewModel.RailwaysDistance.StringValue;
			harName = nameof(entity.Har_16);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.RailwaysDistance.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.RailwaysDistance.AdditionalInfo, ref harInfo);
			}

			entity.Har_17 = parcelCharacteristicsViewModel.RailwayStationsDistance.StringValue;
			harName = nameof(entity.Har_17);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.RailwayStationsDistance.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.RailwayStationsDistance.AdditionalInfo, ref harInfo);
			}

			entity.Har_18 = parcelCharacteristicsViewModel.MiningZoneDistance.StringValue;
			harName = nameof(entity.Har_18);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.MiningZoneDistance.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.MiningZoneDistance.AdditionalInfo, ref harInfo);
			}

			entity.Har_19 = parcelCharacteristicsViewModel.TypeOfLand.StringValue;
			harName = nameof(entity.Har_19);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.TypeOfLand.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.TypeOfLand.AdditionalInfo, ref harInfo);
			}

			entity.Har_20 = parcelCharacteristicsViewModel.SoilConditionIndicators.StringValue;
			harName = nameof(entity.Har_20);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.SoilConditionIndicators.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.SoilConditionIndicators.AdditionalInfo, ref harInfo);
			}

			entity.Har_21 = parcelCharacteristicsViewModel.DisadvantagesPresence.StringValue;
			harName = nameof(entity.Har_21);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarParcelName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarParcelAdditionalInfo() { HarParcelName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DisadvantagesPresence.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DisadvantagesPresence.AdditionalInfo, ref harInfo);
			}
		}
	}
}
