using Core.Register.RegisterEntities;

namespace KadOzenka.Dal.Registers.GbuRegistersServices
{
	public interface IRosreestrRegisterService
	{
		long RosreestrRegisterId { get; }

		/// <summary>
		/// Аттрибут "Наименование объекта"
		/// </summary>
		RegisterAttribute GetObjectNameAttribute();

		/// <summary>
		/// Аттрибут "Наименование земельного участка"
		/// </summary>
		RegisterAttribute GetParcelNameAttribute();

		/// <summary>
		/// Аттрибут "Назначение сооружения"
		/// </summary>
		RegisterAttribute GetConstructionPurposeAttribute();

		/// <summary>
		/// Аттрибут "Адрес"
		/// </summary>
		RegisterAttribute GetAddressAttribute();

		/// <summary>
		/// Аттрибут "Местоположение"
		/// </summary>
		RegisterAttribute GetLocationAttribute();

		/// <summary>
		/// Аттрибут "Земельный участок"
		/// </summary>
		RegisterAttribute GetParcelAttribute();

		/// <summary>
		/// Аттрибут "Год постройки"
		/// </summary>
		RegisterAttribute GetBuildYearAttribute();

		/// <summary>
		/// Аттрибут "Год ввода в эксплуатацию"
		/// </summary>
		RegisterAttribute GetCommissioningYearAttribute();

		/// <summary>
		/// Аттрибут "Количество этажей"
		/// </summary>
		RegisterAttribute GetFloorsNumberAttribute();

		/// <summary>
		/// Аттрибут "Количество подземных этажей"
		/// </summary>
		RegisterAttribute GetUndergroundFloorsNumberAttribute();

		/// <summary>
		/// Аттрибут "Этаж"
		/// </summary>
		RegisterAttribute GetFloorAttribute();

		/// <summary>
		/// Аттрибут "Материал стен"
		/// </summary>
		RegisterAttribute GetWallMaterialAttribute();

		/// <summary>
		/// Аттрибут "Вид использования по документам"
		/// </summary>
		RegisterAttribute GetTypeOfUseByDocumentsAttribute();

		/// <summary>
		/// Аттрибут "Вид использования по классификатору"
		/// </summary>
		RegisterAttribute GetTypeOfUseByClassifierAttribute();

		/// <summary>
		/// Аттрибут "Категория земель"
		/// </summary>
		RegisterAttribute GetParcelCategoryAttribute();

		/// <summary>
		/// Аттрибут "Площадь"
		/// </summary>
		RegisterAttribute GetSquareAttribute();

		/// <summary>
		/// Аттрибут "Дата образования"
		/// </summary>
		RegisterAttribute GetFormationDateAttribute();

		/// <summary>
		/// Аттрибут "Назначение здания"
		/// </summary>
		RegisterAttribute GetBuildingPurposeAttribute();

		/// <summary>
		/// Аттрибут "Процент готовности"
		/// </summary>
		RegisterAttribute GetReadinessPercentageAttribute();

		/// <summary>
		/// Аттрибут "Назначение помещения"
		/// </summary>
		RegisterAttribute GetPlacementPurposeAttribute();

		/// <summary>
		/// Аттрибут "Кадастровый номер здания или сооружения, в котором расположено помещение"
		/// </summary>
		RegisterAttribute GetParentCadastralNumberAttribute();

		/// <summary>
		/// Аттрибут "Наименование объекта"
		/// </summary>
		RegisterAttribute GetObjectNameNumberAttribute();

		/// <summary>
		/// Аттрибут "П1. Группа" (проставляется при импорте документа для задания на оценку)
		/// </summary>
		RegisterAttribute GetPGroupAttribute();

		/// <summary>
		/// Аттрибут "П2. ФС" (проставляется при импорте документа для задания на оценку)
		/// </summary>
		RegisterAttribute GetPFsAttribute();

		/// <summary>
		/// Аттрибут "П3. Материал стен" (проставляется при импорте документа для задания на оценку)
		/// </summary>
		RegisterAttribute GetPWallMaterialAttribute();

		/// <summary>
		/// Аттрибут "П4. Год постройки" (проставляется при импорте документа для задания на оценку)
		/// </summary>
		RegisterAttribute GetPBuildYearAttribute();
	}
}
