using CIPJS.DAL.Invoice;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.DAL.DamageAnalysis
{
    public class DamageAnalysisCardDto
    {
        public long? Id { get; set; }

        public bool IsReadOnly { get; set; }

        public long UserId { get; set; }

        #region Info
        [Display(Name = "Тип договора")]
        public ContractType TypeCode { get; set; }

        [Display(Name = "Номер дела в ОПС")]
        public string NomDoc { get; set; }

        [Display(Name = "Дата дела в ОПС")]
        public DateTime? NomDate { get; set; }

        [Display(Name = "Статус")]
        public StatusDamageAmount StatusDoc { get; set; }

        [Display(Name = "Расчетная стоимость")]
        public decimal? EstimatedValue { get; set; }
        #endregion

        #region Данные от Страховой компании
        public long? InsurCompanyId { get; set; }

        [Display(Name = "Страховая компания")]
        public string InsurCompanyName { get; set; }

        [Display(Name = "Доля СК, %")]
        public decimal? PartInsur { get; set; }

        [Display(Name = "Доля ответственности города, %")]
        public decimal? PartTown { get; set; }

        [Display(Name = "Выплата СК")]
        public decimal? StrahPlat { get; set; }

        [Display(Name = "Исходящий номер письма от СК")]
        public string InsurNom { get; set; }

        [Display(Name = "Дата исходящего номера письма от СК")]
        public DateTime? InsurDate { get; set; }

        [Display(Name = "Дата страхового события")]
        public DateTime? DamageData { get; set; }

        [Display(Name = "Дата поступления дела в ЦИПиЖС")]
        public DateTime? DateInputGBY { get; set; }

        /// <summary>
        /// Дата поступления последнего документа в СК
        /// </summary>
        [Display(Name = "Дата поступления последнего документа в СК")]
        public DateTime? DateDocLastGBY { get; set; }

        /// <summary>
        /// Дата досылки документов в ЦИПиЖС
        /// </summary>
        [Display(Name = "Дата досылки документов в ЦИПиЖС")]
        public DateTime? DateDocAddGBY { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        [Display(Name = "Примечание")]
        public string Note { get; set; }

        [Display(Name = "Размер ущерба")]
        public decimal? SumDamage { get; set; }

        [Display(Name = "Размер ущерба для расчета выплаты по данным ОПС")]
        public decimal? SumDamageBase { get; set; }

        [Display(Name = "Размер ущерба, СК")]
        public decimal? CalculDamage { get; set; }

        [Display(Name = "Доля города, сумма")]
        public decimal? TownPartSum { get; set; }

        [Display(Name = "Выплата по служебной записке")]
        public bool? FlagSlygebka { get; set; }

        [Display(Name = "Примечание")]
        public string CalcNote { get; set; }

        [Display(Name = "Причина ущерба")]
        public CausesOfDamageGP CausesOfDamageGP { get; set; }

        [Display(Name = "Подпричина")]
        public SubReasonCausesOfDamage SubReasonCausesOfDamage { get; set; }

        [Display(Name = "Уточнение")]
        public RefinementSubReasonCOD RefinementSubReasonCOD { get; set; }

        [Display(Name = "Причина ущерба")]
        public CausesOfDamageOI CausesOfDamageOI { get; set; }

        public List<DamageAnalysisPayToDto> InsurOrgPayToDto { get; set; }

        [Display(Name = "Повреждено")]
        public string Fault { get; set; }
        #endregion

        #region Object
        public long? ObjId { get; set; }

        [Display(Name = "UNOM")]
        public string UNOM { get; set; }

        [Display(Name = "Номер квартиры")]
        public string FlatNumber { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Площадь квартиры")]
        public decimal? Area { get; set; }

        [Display(Name = "Кадастровый номер")]
        public string CadastrNumber { get; set; }

        [Display(Name = "Округ")]
        public string Okrug { get; set; }

        [Display(Name = "Район")]
        public string District { get; set; }

        [Display(Name = "Тип помещения")]
        public string FlatTypeName { get; set; }

        [Display(Name = "Статус помещения")]
        public string FlatStatus { get; set; }
        #endregion

        #region Contract
        public List<DamageAnalysisContractDto> Contracts { get; set; }
        #endregion

        #region Calc
        [Display(Name = "Тип конструкции строения")]
        public TypeBuildingStructure BuildingStructure { get; set; }

        [Display(Name = "Этажность строения")]
        public TypeFloors Floors { get; set; }

        [Display(Name = "Плита")]
        public StoveType StoveType { get; set; }

        [Display(Name = "Материал пола")]
        public FloorMaterial FloorType { get; set; }
        #endregion

        #region Agreement
        public bool CanSendToCheck { get; set; }

        public bool CanChecked { get; set; }

        public bool CanAgreed { get; set; }

        public bool CanEdit { get; set; }

        [Display(Name = "Дата")]
        public DateTime? CalculatePersonDate { get; set; }

        [Display(Name = "Первый проверяющий")]
        public long? CalculatePersonId { get; set; }

        [Display(Name = "Сотрудник")]
        public string CalculatePersonFIO { get; set; }

        [Display(Name = "Должность")]
        public string CalculatePersonPost { get; set; }

        [Display(Name = "Дата")]
        public DateTime? InspectorPersonCheckDate { get; set; }

        [Display(Name = "Второй проверяющий")]
        public long? InspectorPersonCheckId { get; set; }

        [Display(Name = "Сотрудник")]
        public string InspectorPersonCheckFIO { get; set; }

        [Display(Name = "Должность")]
        public string InspectorPersonCheckPost { get; set; }

        [Display(Name = "Дата")]
        public DateTime? InspectorPersonDate { get; set; }

        [Display(Name = "Второй проверяющий")]
        public long? InspectorPersonId { get; set; }

        [Display(Name = "Сотрудник")]
        public string InspectorPersonFIO { get; set; }

        [Display(Name = "Должность")]
        public string InspectorPersonPost { get; set; }

        [Display(Name = "Дата")]
        public DateTime? PrimaryMatchingDate { get; set; }

        [Display(Name = "Основной согласующий")]
        public long? PrimaryMatchingPersonId { get; set; }

        [Display(Name = "Сотрудник")]
        public string PrimaryMatchingFIO { get; set; }

        [Display(Name = "Должность")]
        public string PrimaryMatchingPost { get; set; }
        #endregion

        [Display(Name = "Условная франшиза")]
        public decimal? Franchise { get; set; }

        [Display(Name = "Идентификатор платежного документа")]
        public long? PaidDocTypeId { get; set; }

        [Display(Name = "Идентификатор акта осмотра")]
        public long? InspectionActDocTypeId { get; set; }

        [Display(Name = "Признак расхождения расчетной стоимости")]
        public bool? EstimatedValueDifferent { get; set; }

        [Display(Name = "Признак дополнительной выплаты")]
        public bool HasOtherDamages { get; set; }

        [Display(Name = "Ссылка на основное дело")]
        public long? BaseDamageId { get; set; }

        [Display(Name = "Доп. выплата")]
        public bool? BaseDamageExists { get; set; }

        [Display(Name = "Основное дело")]
        public string BaseDamageNomDoc { get; set; }

        public bool RecalcEstimatedValue { get; set; }

        [Display(Name = "Перевыпуск заключения")]
        public bool FlagZakluchReissue { get; set; }
    }
}
