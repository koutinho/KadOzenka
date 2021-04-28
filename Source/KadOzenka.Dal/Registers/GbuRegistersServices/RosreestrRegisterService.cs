using System.Linq;
using Core.Register;
using Core.Register.RegisterEntities;

namespace KadOzenka.Dal.Registers.GbuRegistersServices
{
	public class RosreestrRegisterService : GbuRegisterService, IRosreestrRegisterService
	{
	    protected override string RegisterName => "Источник: ЕГРН";

	    public long RosreestrRegisterId => RegisterId;

	    /// <summary>
        /// Аттрибут "Наименование объекта"
        /// </summary>
        public RegisterAttribute GetObjectNameAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Наименование объекта");
        }

        /// <summary>
        /// Аттрибут "Наименование земельного участка"
        /// </summary>
        public RegisterAttribute GetParcelNameAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Наименование земельного участка");
        }

        /// <summary>
        /// Аттрибут "Назначение сооружения"
        /// </summary>
        public RegisterAttribute GetConstructionPurposeAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Назначение сооружения");
        }

        /// <summary>
        /// Аттрибут "Адрес"
        /// </summary>
        public RegisterAttribute GetAddressAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Адрес");
        }

        /// <summary>
        /// Аттрибут "Местоположение"
        /// </summary>
        public RegisterAttribute GetLocationAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Местоположение");
        }

        /// <summary>
        /// Аттрибут "Земельный участок"
        /// </summary>
        public RegisterAttribute GetParcelAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Земельный участок");
        }

        /// <summary>
        /// Аттрибут "Год постройки"
        /// </summary>
        public RegisterAttribute GetBuildYearAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Год постройки");
        }

        /// <summary>
        /// Аттрибут "Год ввода в эксплуатацию"
        /// </summary>
        public RegisterAttribute GetCommissioningYearAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Год ввода в эксплуатацию");
        }

        /// <summary>
        /// Аттрибут "Количество этажей"
        /// </summary>
        public RegisterAttribute GetFloorsNumberAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Количество этажей");
        }

        /// <summary>
        /// Аттрибут "Количество подземных этажей"
        /// </summary>
        public RegisterAttribute GetUndergroundFloorsNumberAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Количество подземных этажей");
        }

        /// <summary>
        /// Аттрибут "Этаж"
        /// </summary>
        public RegisterAttribute GetFloorAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Этаж");
        }

        /// <summary>
        /// Аттрибут "Материал стен"
        /// </summary>
        public RegisterAttribute GetWallMaterialAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Материал стен");
        }

        /// <summary>
        /// Аттрибут "Вид использования по документам"
        /// </summary>
        public RegisterAttribute GetTypeOfUseByDocumentsAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Вид использования по документам");
        }

        /// <summary>
        /// Аттрибут "Вид использования по классификатору"
        /// </summary>
        public RegisterAttribute GetTypeOfUseByClassifierAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Вид использования по классификатору");
        }

        /// <summary>
        /// Аттрибут "Категория земель"
        /// </summary>
        public RegisterAttribute GetParcelCategoryAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Категория земель");
        }

        /// <summary>
        /// Аттрибут "Площадь"
        /// </summary>
        public RegisterAttribute GetSquareAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Площадь");
        }

        /// <summary>
        /// Аттрибут "Дата образования"
        /// </summary>
        public RegisterAttribute GetFormationDateAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Дата образования");
        }

        /// <summary>
        /// Аттрибут "Назначение здания"
        /// </summary>
        public RegisterAttribute GetBuildingPurposeAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Назначение здания");
        }

        /// <summary>
        /// Аттрибут "Процент готовности"
        /// </summary>
        public RegisterAttribute GetReadinessPercentageAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Процент готовности");
        }

        /// <summary>
        /// Аттрибут "Назначение помещения"
        /// </summary>
        public RegisterAttribute GetPlacementPurposeAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Назначение помещения");
        }

        /// <summary>
        /// Аттрибут "Кадастровый номер здания или сооружения, в котором расположено помещение"
        /// </summary>
        public RegisterAttribute GetParentCadastralNumberAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Кадастровый номер здания или сооружения, в котором расположено помещение");
        }

        /// <summary>
        /// Аттрибут "Наименование объекта"
        /// </summary>
        public RegisterAttribute GetObjectNameNumberAttribute()
        {
            return GetRegisterAttributeByName(RegisterId, "Наименование объекта");
        }

        /// <summary>
        /// Аттрибут "П1. Группа" (проставляется при импорте документа для задания на оценку)
        /// </summary>
        public RegisterAttribute GetPGroupAttribute()
        {
	       return RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == 660);
        }

        /// <summary>
        /// Аттрибут "П2. ФС" (проставляется при импорте документа для задания на оценку)
        /// </summary>
        public RegisterAttribute GetPFsAttribute()
        {
	        return RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == 661);
        }

        /// <summary>
        /// Аттрибут "П3. Материал стен" (проставляется при импорте документа для задания на оценку)
        /// </summary>
        public RegisterAttribute GetPWallMaterialAttribute()
        {
	        return RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == 662);
        }

        /// <summary>
        /// Аттрибут "П4. Год постройки" (проставляется при импорте документа для задания на оценку)
        /// </summary>
        public RegisterAttribute GetPBuildYearAttribute()
        {
	        return RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == 663);
        }
    }
}
