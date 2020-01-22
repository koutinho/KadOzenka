using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		/// Вид объекта недвижимости (HAR_1)
		/// </summary>
		[Display(Name = "Вид объекта недвижимости")]
		public string ObjectType { get; set; }

		/// <summary>
		/// Адрес (описание местоположения) (HAR_2)
		/// </summary>
		[Display(Name = "Адрес (описание местоположения)")]
		public string Address { get; set; }

		/// <summary>
		/// Площадь (HAR_3)
		/// </summary>
		[Display(Name = "Площадь")]
		public decimal? Square { get; set; }

		/// <summary>
		/// Тип и значение основной характеристики сооружения (HAR_4)
		/// </summary>
		[Display(Name = "Тип и значение основной характеристики сооружения")]
		public string TypeAndValueMainCharacteristic { get; set; }

		/// <summary>
		/// Степень готовности объекта незавершенного строительства (HAR_5)
		/// </summary>
		[Display(Name = "Степень готовности объекта незавершенного строительства")]
		public string ReadinessDegreeOns { get; set; }

		/// <summary>
		/// Проектируемое назначение здания, сооружения, строительство которых не завершено (для объектов незавершенного строительства) (HAR_6)
		/// </summary>
		[Display(Name = "Проектируемый тип и значение основной характеристики объекта незавершенного строительства")]
		public string DesignedTypeAndValueOfMainCharacteristicsOns { get; set; }

		/// <summary>
		/// Проектируемое назначение здания, сооружения, строительство которых не завершено (для объектов незавершенного строительства) (HAR_7)
		/// </summary>
		[Display(Name = "Проектируемое назначение здания, сооружения, строительство которых не завершено (для объектов незавершенного строительства)")]
		public string DesignedPurposeOns { get; set; }

		/// <summary>
		/// Количество этажей (HAR_8)
		/// </summary>
		[Display(Name = "Количество этажей")]
		public long? FloorCount { get; set; }

		/// <summary>
		/// Номер этажа здания или сооружения, на котором расположено помещение или машино-место (HAR_9)
		/// </summary>
		[Display(Name = "Номер этажа здания или сооружения, на котором расположено помещение или машино-место")]
		public long? FloorNumber { get; set; }

		/// <summary>
		/// Материал наружных стен, если объектом недвижимости является здание (HAR_10)
		/// </summary>
		[Display(Name = "Материал наружных стен, если объектом недвижимости является здание")]
		public string BuildingWallMaterial { get; set; }

		/// <summary>
		/// Материал основных несущих конструкций, перекрытий (HAR_11)
		/// </summary>
		[Display(Name = "Материал основных несущих конструкций, перекрытий")]
		public string MainSupportingStructuresMaterial { get; set; }

		/// <summary>
		/// Материал кровли (HAR_12)
		/// </summary>
		[Display(Name = "Материал кровли")]
		public string RoofMaterial { get; set; }

		/// <summary>
		/// Год ввода в эксплуатацию объекта недвижимости  (HAR_13)
		/// </summary>
		[Display(Name = "Год ввода в эксплуатацию объекта недвижимости")]
		public DateTime? CommissioningYear { get; set; }

		/// <summary>
		/// Год завершения строительства объекта недвижимости (HAR_14)
		/// </summary>
		[Display(Name = "Год завершения строительства объекта недвижимости")]
		public DateTime? CompletionYear { get; set; }

		/// <summary>
		/// Удаленность относительно ближайшей рекреационной зоны (HAR_15)
		/// </summary>
		[Display(Name = "Дата окончания проведения капитального ремонта")]
		public DateTime? OverhaulCompletionYear { get; set; }

		/// <summary>
		/// Дата окончания проведения реконструкции (HAR_16)
		/// </summary>
		[Display(Name = "Дата окончания проведения реконструкции")]
		public DateTime? ReconstructionDate { get; set; }

		/// <summary>
		/// Вид жилого помещения (HAR_17)
		/// </summary>
		[Display(Name = "Вид жилого помещения")]
		public string DwellingType { get; set; }

		/// <summary>
		/// Вид или виды разрешенного использования (HAR_18)
		/// </summary>
		[Display(Name = "Вид или виды разрешенного использования объектов капитального строительства")]
		public string PermittedUseType { get; set; }

		/// <summary>
		/// Сведения о включении объекта недвижимости в единый государственный реестр объектов культурного наследия (HAR_19)
		/// </summary>
		[Display(Name = "Сведения о включении объекта недвижимости в единый государственный реестр объектов культурного наследия (памятников истории и культуры) народов Российской Федерации ")]
		public string ObjectInclusionIntoCulturalHeritageInfo { get; set; }

		/// <summary>
		/// Физический износ (HAR_20)
		/// </summary>
		[Display(Name = "Физический износ")]
		public string PhysicalDeterioration { get; set; }

		/// <summary>
		/// Описание коммуникаций, в том числе их удаленность (HAR_21)
		/// </summary>
		[Display(Name = "Описание коммуникаций, в том числе их удаленность")]
		public string CommunicationsDescription { get; set; }

		/// <summary>
		/// Наличие/отсутствие подключения к электрическим сетям (HAR_21_1_1)
		/// </summary>
		[Display(Name = ConnectionToPowerGridsName)]
		public HarAvailability? ConnectionToPowerGrids { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к сетям (HAR_21_1_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToPowerGridsName)]
		public HarAvailability? AvailabilityConnectionToPowerGrids { get; set; }

		/// <summary>
		/// Мощность электрической сети (HAR_21_1_3)
		/// </summary>
		[Display(Name = "Мощность электрической сети")]
		public string ElectricPower { get; set; }

		/// <summary>
		/// Наличие/отсутствие подключения к сетям газораспределения (HAR_21_2_1)
		/// </summary>
		[Display(Name = ConnectionToGasGridsName)]
		public HarAvailability? ConnectionToGasGrids { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к сетям газораспределения (HAR_21_2_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToGasGridsName)]
		public HarAvailability? AvailabilityConnectionToGasGrids { get; set; }

		/// <summary>
		/// Мощность электрической сети (HAR_21_2_3)
		/// </summary>
		[Display(Name = GasPowerName)]
		public string GasPower { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе водоснабжения (HAR_21_3_1)
		/// </summary>
		[Display(Name = ConnectionToWaterSupplyName)]
		public HarAvailability? ConnectionToWaterSupply { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе водоснабжения (HAR_21_3_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToWaterSupplyName)]
		public HarAvailability? AvailabilityConnectionToWaterSupply { get; set; }

		/// <summary>
		/// Наличие/отсутствие централизованного подключения к системе теплоснабжения (HAR_21_4_1)
		/// </summary>
		[Display(Name = ConnectionToHeatSupplyName)]
		public HarAvailability? ConnectionToHeatSupply { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе теплоснабжения (HAR_21_4_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToHeatSupplyName)]
		public HarAvailability? AvailabilityConnectionToHeatSupply { get; set; }

		/// <summary>
		/// Наличие/отсутствие централизованного подключения к системе теплоснабжения (HAR_21_5_1)
		/// </summary>
		[Display(Name = ConnectionToWaterDisposalName)]
		public HarAvailability? ConnectionToWaterDisposal { get; set; }

		/// <summary>
		/// Возможность/отсутствие возможности подключения к системе теплоснабжения (HAR_21_5_2)
		/// </summary>
		[Display(Name = AvailabilityConnectionToWaterDisposalName)]
		public HarAvailability? AvailabilityConnectionToWaterDisposal { get; set; }

		public bool IsEditDeclarationCharacteristics { get; set; }

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

		public static OksCharacteristicsModel FromEntity(OMHarOKS entity)
		{
			if (entity == null)
			{
				return new OksCharacteristicsModel
				{
					Id = -1,
				};
			}

			return new OksCharacteristicsModel
			{
				Id = entity.Id,
				ObjectType = entity.Har_1,
				Address = entity.Har_2,
				Square = entity.Har_3,
				TypeAndValueMainCharacteristic = entity.Har_4,
				ReadinessDegreeOns = entity.Har_5,
				DesignedTypeAndValueOfMainCharacteristicsOns = entity.Har_6,
				DesignedPurposeOns = entity.Har_7,
				FloorCount = entity.Har_8,
				FloorNumber = entity.Har_9,
				BuildingWallMaterial = entity.Har_10,
				MainSupportingStructuresMaterial = entity.Har_11,
				RoofMaterial = entity.Har_12,
				CommissioningYear = entity.Har_13,
				CompletionYear = entity.Har_14,
				OverhaulCompletionYear = entity.Har_15,
				ReconstructionDate = entity.Har_16,
				DwellingType = entity.Har_17,
				PermittedUseType = entity.Har_18,
				ObjectInclusionIntoCulturalHeritageInfo = entity.Har_19,
				PhysicalDeterioration = entity.Har_20,
				CommunicationsDescription = entity.Har_21,
				ConnectionToPowerGrids = entity.Har_21_1_1_Code,
				AvailabilityConnectionToPowerGrids = entity.Har_21_1_2_Code,
				ElectricPower = entity.Har_21_1_3,
				ConnectionToGasGrids = entity.Har_21_2_1_Code,
				AvailabilityConnectionToGasGrids = entity.Har_21_2_2_Code,
				GasPower = entity.Har_21_2_3,
				ConnectionToWaterSupply = entity.Har_21_3_1_Code,
				AvailabilityConnectionToWaterSupply = entity.Har_21_3_2_Code,
				ConnectionToHeatSupply = entity.Har_21_4_1_Code,
				AvailabilityConnectionToHeatSupply = entity.Har_21_4_2_Code,
				ConnectionToWaterDisposal = entity.Har_21_5_1_Code,
				AvailabilityConnectionToWaterDisposal = entity.Har_21_5_2_Code,
			};
		}

		public static void ToEntity(OksCharacteristicsModel parcelCharacteristicsViewModel, ref OMHarOKS entity)
		{
			entity.Declaration_Id = parcelCharacteristicsViewModel.DeclarationId;
			entity.Har_1 = parcelCharacteristicsViewModel.ObjectType;
			entity.Har_2 = parcelCharacteristicsViewModel.Address;
			entity.Har_3 = parcelCharacteristicsViewModel.Square;
			entity.Har_4 = parcelCharacteristicsViewModel.TypeAndValueMainCharacteristic;
			entity.Har_5 = parcelCharacteristicsViewModel.ReadinessDegreeOns;
			entity.Har_6 = parcelCharacteristicsViewModel.DesignedTypeAndValueOfMainCharacteristicsOns;
			entity.Har_7 = parcelCharacteristicsViewModel.DesignedPurposeOns;
			entity.Har_8 = parcelCharacteristicsViewModel.FloorCount;
			entity.Har_9 = parcelCharacteristicsViewModel.FloorNumber;
			entity.Har_10 = parcelCharacteristicsViewModel.BuildingWallMaterial;
			entity.Har_11 = parcelCharacteristicsViewModel.MainSupportingStructuresMaterial;
			entity.Har_12 = parcelCharacteristicsViewModel.RoofMaterial;
			entity.Har_13 = parcelCharacteristicsViewModel.CommissioningYear;
			entity.Har_14 = parcelCharacteristicsViewModel.CompletionYear;
			entity.Har_15 = parcelCharacteristicsViewModel.OverhaulCompletionYear;
			entity.Har_16 = parcelCharacteristicsViewModel.ReconstructionDate;
			entity.Har_17 = parcelCharacteristicsViewModel.DwellingType;
			entity.Har_18 = parcelCharacteristicsViewModel.PermittedUseType;
			entity.Har_19 = parcelCharacteristicsViewModel.ObjectInclusionIntoCulturalHeritageInfo;
			entity.Har_20 = parcelCharacteristicsViewModel.PhysicalDeterioration;
			entity.Har_21 = parcelCharacteristicsViewModel.CommunicationsDescription;
			entity.Har_21_1_1_Code = parcelCharacteristicsViewModel.ConnectionToPowerGrids.GetValueOrDefault();
			entity.Har_21_1_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToPowerGrids.GetValueOrDefault();
			entity.Har_21_1_3 = parcelCharacteristicsViewModel.ElectricPower;
			entity.Har_21_2_1_Code = parcelCharacteristicsViewModel.ConnectionToGasGrids.GetValueOrDefault();
			entity.Har_21_2_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToGasGrids.GetValueOrDefault();
			entity.Har_21_2_3 = parcelCharacteristicsViewModel.GasPower;
			entity.Har_21_3_1_Code = parcelCharacteristicsViewModel.ConnectionToWaterSupply.GetValueOrDefault();
			entity.Har_21_3_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToWaterSupply.GetValueOrDefault();
			entity.Har_21_4_1_Code = parcelCharacteristicsViewModel.ConnectionToHeatSupply.GetValueOrDefault();
			entity.Har_21_4_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToHeatSupply.GetValueOrDefault();
			entity.Har_21_5_1_Code = parcelCharacteristicsViewModel.ConnectionToWaterDisposal.GetValueOrDefault();
			entity.Har_21_5_2_Code = parcelCharacteristicsViewModel.AvailabilityConnectionToWaterDisposal.GetValueOrDefault();
		}
	}
}
