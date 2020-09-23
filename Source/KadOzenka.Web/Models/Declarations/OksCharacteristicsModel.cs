using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class OksCharacteristicsModel
	{
		public const string ConnectionToPowerGridsName = "Наличие/отсутствие подключения к электрическим сетям";
		public const string AvailabilityConnectionToPowerGridsName = "Возможность/отсутствие возможности подключения к сетям инженерно-технического обеспечения";
		public const string ConnectionToGasGridsName = "Наличие/отсутствие подключения к сетям газораспределения";
		public const string AvailabilityConnectionToGasGridsName = "Возможность/отсутствие возможности подключения к сетям газораспределения";
		public const string GasPowerName = "Мощность сетей газораспределения";
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
		/// Вид объекта недвижимости (HAR_1)
		/// </summary>
		[Display(Name = "Вид объекта недвижимости")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Вид объекта недвижимости' составляет 4096 символа")]
		public CharacteristicModel ObjectType { get; set; }

		/// <summary>
		/// Адрес (описание местоположения) (HAR_2)
		/// </summary>
		[Display(Name = "Адрес (описание местоположения)")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Адрес (описание местоположения)' составляет 4096 символа")]
		public CharacteristicModel Address { get; set; }

		/// <summary>
		/// Площадь (HAR_3)
		/// </summary>
		[Display(Name = "Площадь")]
		public CharacteristicModel Square { get; set; }

		/// <summary>
		/// Тип и значение основной характеристики сооружения (HAR_4)
		/// </summary>
		[Display(Name = "Тип и значение основной характеристики сооружения")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Тип и значение основной характеристики сооружения' составляет 4096 символа")]
		public CharacteristicModel TypeAndValueMainCharacteristic { get; set; }

		/// <summary>
		/// Степень готовности объекта незавершенного строительства (HAR_5)
		/// </summary>
		[Display(Name = "Степень готовности объекта незавершенного строительства")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Степень готовности объекта незавершенного строительства' составляет 4096 символа")]
		public CharacteristicModel ReadinessDegreeOns { get; set; }

		/// <summary>
		/// Проектируемое назначение здания, сооружения, строительство которых не завершено (для объектов незавершенного строительства) (HAR_6)
		/// </summary>
		[Display(Name = "Проектируемый тип и значение основной характеристики объекта незавершенного строительства")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Проектируемый тип и значение основной характеристики объекта незавершенного строительства' составляет 4096 символа")]
		public CharacteristicModel DesignedTypeAndValueOfMainCharacteristicsOns { get; set; }

		/// <summary>
		/// Проектируемое назначение здания, сооружения, строительство которых не завершено (для объектов незавершенного строительства) (HAR_7)
		/// </summary>
		[Display(Name = "Проектируемое назначение здания, сооружения, строительство которых не завершено (для объектов незавершенного строительства)")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Проектируемое назначение здания, сооружения, строительство которых не завершено (для объектов незавершенного строительства)' составляет 4096 символа")]
		public CharacteristicModel DesignedPurposeOns { get; set; }

		/// <summary>
		/// Количество этажей (HAR_8)
		/// </summary>
		[Display(Name = "Количество этажей")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage = "Максимальная длина значения для поля 'Количество этажей' составляет 4096 символа")]
		public CharacteristicModel FloorCount { get; set; }

		/// <summary>
		/// Номер этажа здания или сооружения, на котором расположено помещение или машино-место (HAR_9)
		/// </summary>
		[Display(Name = "Номер этажа здания или сооружения, на котором расположено помещение или машино-место")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage = "Максимальная длина значения для поля 'Номер этажа здания или сооружения, на котором расположено помещение или машино-место' составляет 4096 символа")]
		public CharacteristicModel FloorNumber { get; set; }

		/// <summary>
		/// Материал наружных стен, если объектом недвижимости является здание (HAR_10)
		/// </summary>
		[Display(Name = "Материал наружных стен, если объектом недвижимости является здание")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Материал наружных стен, если объектом недвижимости является здание' составляет 4096 символа")]
		public CharacteristicModel BuildingWallMaterial { get; set; }

		/// <summary>
		/// Материал основных несущих конструкций, перекрытий (HAR_11)
		/// </summary>
		[Display(Name = "Материал основных несущих конструкций, перекрытий")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Материал основных несущих конструкций, перекрытий' составляет 4096 символа")]
		public CharacteristicModel MainSupportingStructuresMaterial { get; set; }

		/// <summary>
		/// Материал кровли (HAR_12)
		/// </summary>
		[Display(Name = "Материал кровли")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Материал кровли' составляет 4096 символа")]
		public CharacteristicModel RoofMaterial { get; set; }

		/// <summary>
		/// Год ввода в эксплуатацию объекта недвижимости  (HAR_13)
		/// </summary>
		[Display(Name = "Год ввода в эксплуатацию объекта недвижимости")]
		public CharacteristicModel CommissioningYear { get; set; }

		/// <summary>
		/// Год завершения строительства объекта недвижимости (HAR_14)
		/// </summary>
		[Display(Name = "Год завершения строительства объекта недвижимости")]
		public CharacteristicModel CompletionYear { get; set; }

		/// <summary>
		/// Удаленность относительно ближайшей рекреационной зоны (HAR_15)
		/// </summary>
		[Display(Name = "Дата окончания проведения капитального ремонта")]
		public CharacteristicModel OverhaulCompletionYear { get; set; }

		/// <summary>
		/// Дата окончания проведения реконструкции (HAR_16)
		/// </summary>
		[Display(Name = "Дата окончания проведения реконструкции")]
		public CharacteristicModel ReconstructionDate { get; set; }

		/// <summary>
		/// Вид жилого помещения (HAR_17)
		/// </summary>
		[Display(Name = "Вид жилого помещения")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Вид жилого помещения' составляет 4096 символа")]
		public CharacteristicModel DwellingType { get; set; }

		/// <summary>
		/// Вид или виды разрешенного использования (HAR_18)
		/// </summary>
		[Display(Name = "Вид или виды разрешенного использования объектов капитального строительства")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Вид или виды разрешенного использования объектов капитального строительства' составляет 4096 символа")]
		public CharacteristicModel PermittedUseType { get; set; }

		/// <summary>
		/// Сведения о включении объекта недвижимости в единый государственный реестр объектов культурного наследия (HAR_19)
		/// </summary>
		[Display(Name = "Сведения о включении объекта недвижимости в единый государственный реестр объектов культурного наследия (памятников истории и культуры) народов Российской Федерации ")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Сведения о включении объекта недвижимости в единый государственный реестр объектов культурного наследия (памятников истории и культуры) народов Российской Федерации' составляет 4096 символа")]
		public CharacteristicModel ObjectInclusionIntoCulturalHeritageInfo { get; set; }

		/// <summary>
		/// Физический износ (HAR_20)
		/// </summary>
		[Display(Name = "Физический износ")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Физический износ' составляет 4096 символа")]
		public CharacteristicModel PhysicalDeterioration { get; set; }

		/// <summary>
		/// Описание коммуникаций, в том числе их удаленность (HAR_21)
		/// </summary>
		[Display(Name = "Описание коммуникаций, в том числе их удаленность")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Описание коммуникаций, в том числе их удаленность' составляет 4096 символа")]
		public CharacteristicModel CommunicationsDescription { get; set; }

		/// <summary>
		/// Наличие/отсутствие подключения к электрическим сетям (HAR_21_1_1)
		/// </summary>
		[Display(Name = ConnectionToPowerGridsName)]
		public CharacteristicModel ConnectionToPowerGrids { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к сетям (HAR_21_1_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToPowerGridsName)]
		public CharacteristicModel AvailabilityConnectionToPowerGrids { get; set; }

		/// <summary>
		/// Мощность электрической сети (HAR_21_1_3)
		/// </summary>
		[Display(Name = "Мощность электрической сети")]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Мощность электрической сети' составляет 4096 символа")]
		public CharacteristicModel ElectricPower { get; set; }

		/// <summary>
		/// Наличие/отсутствие подключения к сетям газораспределения (HAR_21_2_1)
		/// </summary>
		[Display(Name = ConnectionToGasGridsName)]
		public CharacteristicModel ConnectionToGasGrids { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к сетям газораспределения (HAR_21_2_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToGasGridsName)]
		public CharacteristicModel AvailabilityConnectionToGasGrids { get; set; }

		/// <summary>
		/// Мощность сетей газораспределения (HAR_21_2_3)
		/// </summary>
		[Display(Name = GasPowerName)]
		[CharacteristicsModelMaxLength(4096, ErrorMessage ="Максимальная длина значения для поля 'Мощность сетей газораспределения' составляет 4096 символа")]
		public CharacteristicModel GasPower { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе водоснабжения (HAR_21_3_1)
		/// </summary>
		[Display(Name = ConnectionToWaterSupplyName)]
		public CharacteristicModel ConnectionToWaterSupply { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе водоснабжения (HAR_21_3_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToWaterSupplyName)]
		public CharacteristicModel AvailabilityConnectionToWaterSupply { get; set; }

		/// <summary>
		/// Наличие/отсутствие централизованного подключения к системе теплоснабжения (HAR_21_4_1)
		/// </summary>
		[Display(Name = ConnectionToHeatSupplyName)]
		public CharacteristicModel ConnectionToHeatSupply { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе теплоснабжения (HAR_21_4_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToHeatSupplyName)]
		public CharacteristicModel AvailabilityConnectionToHeatSupply { get; set; }

		/// <summary>
		/// Наличие/отсутствие централизованного подключения к системе теплоснабжения (HAR_21_5_1)
		/// </summary>
		[Display(Name = ConnectionToWaterDisposalName)]
		public CharacteristicModel ConnectionToWaterDisposal { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе теплоснабжения (HAR_21_5_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToWaterDisposalName)]
		public CharacteristicModel AvailabilityConnectionToWaterDisposal { get; set; }

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

		public static OksCharacteristicsModel FromEntity(OMHarOKS entity, List<OMHarOKSAdditionalInfo> additionalInfos)
		{
			return new OksCharacteristicsModel
			{
				Id = entity == null ? -1 : entity.Id,
				ObjectType = new CharacteristicModel{Value = entity?.Har_1, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_1))) },
				Address = new CharacteristicModel { Value = entity?.Har_2, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_2))) },
				Square = new CharacteristicModel { Value = entity?.Har_3, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_3))) },
				TypeAndValueMainCharacteristic = new CharacteristicModel { Value = entity?.Har_4, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_4))) },
				ReadinessDegreeOns = new CharacteristicModel { Value = entity?.Har_5, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_5))) },
				DesignedTypeAndValueOfMainCharacteristicsOns = new CharacteristicModel { Value = entity?.Har_6, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_6))) },
				DesignedPurposeOns = new CharacteristicModel { Value = entity?.Har_7, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_7))) },
				FloorCount = new CharacteristicModel { Value = entity?.Har_8, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_8))) },
				FloorNumber = new CharacteristicModel { Value = entity?.Har_9, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_9))) },
				BuildingWallMaterial = new CharacteristicModel { Value = entity?.Har_10, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_10))) },
				MainSupportingStructuresMaterial = new CharacteristicModel { Value = entity?.Har_11, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_11))) },
				RoofMaterial = new CharacteristicModel { Value = entity?.Har_12, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_12))) },
				CommissioningYear = new CharacteristicModel { Value = entity?.Har_13, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_13))) },
				CompletionYear = new CharacteristicModel { Value = entity?.Har_14, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_14))) },
				OverhaulCompletionYear = new CharacteristicModel { Value = entity?.Har_15, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_15))) },
				ReconstructionDate = new CharacteristicModel { Value = entity?.Har_16, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_16))) },
				DwellingType = new CharacteristicModel { Value = entity?.Har_17, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_17))) },
				PermittedUseType = new CharacteristicModel { Value = entity?.Har_18, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_18))) },
				ObjectInclusionIntoCulturalHeritageInfo = new CharacteristicModel { Value = entity?.Har_19, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_19))) },
				PhysicalDeterioration = new CharacteristicModel { Value = entity?.Har_20, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_20))) },
				CommunicationsDescription = new CharacteristicModel { Value = entity?.Har_21, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21))) },
				ConnectionToPowerGrids = new CharacteristicModel { Value = entity?.Har_21_1_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_1_1))) },
				AvailabilityConnectionToPowerGrids = new CharacteristicModel { Value = entity?.Har_21_1_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_1_2))) },
				ElectricPower = new CharacteristicModel { Value = entity?.Har_21_1_3, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_1_3))) },
				ConnectionToGasGrids = new CharacteristicModel { Value = entity?.Har_21_2_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_2_1))) },
				AvailabilityConnectionToGasGrids = new CharacteristicModel { Value = entity?.Har_21_2_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_2_2))) },
				GasPower = new CharacteristicModel { Value = entity?.Har_21_2_3, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_2_3))) },
				ConnectionToWaterSupply = new CharacteristicModel { Value = entity?.Har_21_3_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_3_1))) },
				AvailabilityConnectionToWaterSupply = new CharacteristicModel { Value = entity?.Har_21_3_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_3_2))) },
				ConnectionToHeatSupply = new CharacteristicModel { Value = entity?.Har_21_4_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_4_1))) },
				AvailabilityConnectionToHeatSupply = new CharacteristicModel { Value = entity?.Har_21_4_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_4_2))) },
				ConnectionToWaterDisposal = new CharacteristicModel { Value = entity?.Har_21_5_1_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_5_1))) },
				AvailabilityConnectionToWaterDisposal = new CharacteristicModel { Value = entity?.Har_21_5_2_Code, AdditionalInfo = CharacteristicAdditionalInfoModel.FromEntity(additionalInfos?.FirstOrDefault(x => entity != null && x.HarOKSName == nameof(entity.Har_21_5_2))) },
			};
		}

		public static void ToEntity(OksCharacteristicsModel parcelCharacteristicsViewModel, ref OMHarOKS entity,
			ref List<OMHarOKSAdditionalInfo> characteristicAdditionalInfo)
		{
			entity.Declaration_Id = parcelCharacteristicsViewModel.DeclarationId;

			entity.Har_1 = parcelCharacteristicsViewModel.ObjectType.StringValue;
			var harName = nameof(entity.Har_1);
			var harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo { HarOKSName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ObjectType.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ObjectType.AdditionalInfo, ref harInfo);
			}

			entity.Har_2 = parcelCharacteristicsViewModel.Address.StringValue;
			harName = nameof(entity.Har_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo { HarOKSName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.Address.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.Address.AdditionalInfo,
					ref harInfo);
			}

			entity.Har_3 = parcelCharacteristicsViewModel.Square.DecimalValue;
			harName = nameof(entity.Har_3);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.Square.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.Square.AdditionalInfo, ref harInfo);
			}

			entity.Har_4 = parcelCharacteristicsViewModel.TypeAndValueMainCharacteristic.StringValue;
			harName = nameof(entity.Har_4);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.TypeAndValueMainCharacteristic.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.TypeAndValueMainCharacteristic.AdditionalInfo, ref harInfo);
			}

			entity.Har_5 = parcelCharacteristicsViewModel.ReadinessDegreeOns.StringValue;
			harName = nameof(entity.Har_5);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo { HarOKSName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ReadinessDegreeOns.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ReadinessDegreeOns.AdditionalInfo, ref harInfo);
			}

			entity.Har_6 = parcelCharacteristicsViewModel.DesignedTypeAndValueOfMainCharacteristicsOns.StringValue;
			harName = nameof(entity.Har_6);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DesignedTypeAndValueOfMainCharacteristicsOns.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DesignedTypeAndValueOfMainCharacteristicsOns.AdditionalInfo, ref harInfo);
			}

			entity.Har_7 = parcelCharacteristicsViewModel.DesignedPurposeOns.StringValue;
			harName = nameof(entity.Har_7);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DesignedPurposeOns.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DesignedPurposeOns.AdditionalInfo, ref harInfo);
			}

			entity.Har_8 = parcelCharacteristicsViewModel.FloorCount.StringValue;
			harName = nameof(entity.Har_8);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.FloorCount.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.FloorCount.AdditionalInfo, ref harInfo);
			}

			entity.Har_9 = parcelCharacteristicsViewModel.FloorNumber.StringValue;
			harName = nameof(entity.Har_9);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo { HarOKSName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.FloorNumber.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.FloorNumber.AdditionalInfo, ref harInfo);
			}

			entity.Har_10 = parcelCharacteristicsViewModel.BuildingWallMaterial.StringValue;
			harName = nameof(entity.Har_10);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.BuildingWallMaterial.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.BuildingWallMaterial.AdditionalInfo, ref harInfo);
			}

			entity.Har_11 = parcelCharacteristicsViewModel.MainSupportingStructuresMaterial.StringValue;
			harName = nameof(entity.Har_11);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo { HarOKSName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.MainSupportingStructuresMaterial.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.MainSupportingStructuresMaterial.AdditionalInfo, ref harInfo);
			}

			entity.Har_12 = parcelCharacteristicsViewModel.RoofMaterial.StringValue;
			harName = nameof(entity.Har_12);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo { HarOKSName = harName };
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.RoofMaterial.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.RoofMaterial.AdditionalInfo, ref harInfo);
			}

			entity.Har_13 = parcelCharacteristicsViewModel.CommissioningYear.DateTimeValue;
			harName = nameof(entity.Har_13);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.CommissioningYear.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.CommissioningYear.AdditionalInfo, ref harInfo);
			}

			entity.Har_14 = parcelCharacteristicsViewModel.CompletionYear.DateTimeValue;
			harName = nameof(entity.Har_14);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.CompletionYear.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.CompletionYear.AdditionalInfo, ref harInfo);
			}

			entity.Har_15 = parcelCharacteristicsViewModel.OverhaulCompletionYear.DateTimeValue;
			harName = nameof(entity.Har_15);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.OverhaulCompletionYear.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.OverhaulCompletionYear.AdditionalInfo, ref harInfo);
			}

			entity.Har_16 = parcelCharacteristicsViewModel.ReconstructionDate.DateTimeValue;
			harName = nameof(entity.Har_16);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ReconstructionDate.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ReconstructionDate.AdditionalInfo, ref harInfo);
			}
			

			entity.Har_17 = parcelCharacteristicsViewModel.DwellingType.StringValue;
			harName = nameof(entity.Har_17);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DwellingType.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.DwellingType.AdditionalInfo, ref harInfo);
			}

			entity.Har_18 = parcelCharacteristicsViewModel.PermittedUseType.StringValue;
			harName = nameof(entity.Har_18);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.PermittedUseType.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.PermittedUseType.AdditionalInfo, ref harInfo);
			}

			entity.Har_19 = parcelCharacteristicsViewModel.ObjectInclusionIntoCulturalHeritageInfo.StringValue;
			harName = nameof(entity.Har_19);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ObjectInclusionIntoCulturalHeritageInfo.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ObjectInclusionIntoCulturalHeritageInfo.AdditionalInfo, ref harInfo);
			}

			entity.Har_20 = parcelCharacteristicsViewModel.PhysicalDeterioration.StringValue;
			harName = nameof(entity.Har_20);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.PhysicalDeterioration.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.PhysicalDeterioration.AdditionalInfo, ref harInfo);
			}

			entity.Har_21 = parcelCharacteristicsViewModel.CommunicationsDescription.StringValue;
			harName = nameof(entity.Har_21);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.CommunicationsDescription.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.CommunicationsDescription.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_1_1_Code = parcelCharacteristicsViewModel.ConnectionToPowerGrids.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_1_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToPowerGrids.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToPowerGrids.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_1_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToPowerGrids.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_1_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToPowerGrids.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToPowerGrids.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_1_3 = parcelCharacteristicsViewModel.ElectricPower.StringValue;
			harName = nameof(entity.Har_21_1_3);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ElectricPower.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ElectricPower.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_2_1_Code = parcelCharacteristicsViewModel.ConnectionToGasGrids.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_2_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToGasGrids.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToGasGrids.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_2_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToGasGrids.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_2_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToGasGrids.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToGasGrids.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_2_3 = parcelCharacteristicsViewModel.GasPower.StringValue;
			harName = nameof(entity.Har_21_2_3);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.GasPower.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.GasPower.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_3_1_Code = parcelCharacteristicsViewModel.ConnectionToWaterSupply.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_3_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToWaterSupply.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToWaterSupply.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_3_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToWaterSupply.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_3_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToWaterSupply.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToWaterSupply.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_4_1_Code = parcelCharacteristicsViewModel.ConnectionToHeatSupply.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_4_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToHeatSupply.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToHeatSupply.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_4_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToHeatSupply.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_4_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToHeatSupply.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToHeatSupply.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_5_1_Code = parcelCharacteristicsViewModel.ConnectionToWaterDisposal.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_5_1);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToWaterDisposal.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.ConnectionToWaterDisposal.AdditionalInfo, ref harInfo);
			}

			entity.Har_21_5_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToWaterDisposal.HarAvailabilityValue.GetValueOrDefault();
			harName = nameof(entity.Har_21_5_2);
			harInfo = characteristicAdditionalInfo.FirstOrDefault(x => x.HarOKSName == harName);
			if (harInfo == null)
			{
				harInfo = new OMHarOKSAdditionalInfo {HarOKSName = harName};
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToWaterDisposal.AdditionalInfo, ref harInfo);
				characteristicAdditionalInfo.Add(harInfo);
			}
			else
			{
				CharacteristicAdditionalInfoModel.ToEntity(parcelCharacteristicsViewModel.AvailabilityConnectionToWaterDisposal.AdditionalInfo, ref harInfo);
			}
			
		}
	}
}
