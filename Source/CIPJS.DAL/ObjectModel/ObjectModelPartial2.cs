using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;
using System.Xml.Serialization;


namespace ObjectModel.Bti
{
    /// <summary>
    /// 52 БТИ: Адрес здания (связь)
    /// </summary>
    public partial class OMADDRLINK
    {
        /// <summary>
        /// Ссылка на (50 БТИ: Адрес)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMADDRESS ParentADDRESS { get; set; }

        /// <summary>
        /// Ссылка на (251 БТИ: Здание)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiBuilding ParentBtiBuilding { get; set; }

    }
}


namespace ObjectModel.Bti
{
    /// <summary>
    /// 253 БТИ: Этаж
    /// </summary>
    public partial class OMFloor
    {
        /// <summary>
        /// Ссылка на (251 БТИ: Здание)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiBuilding ParentBtiBuilding { get; set; }

    }
}


namespace ObjectModel.Bti
{
    /// <summary>
    /// 254 БТИ: Помещение
    /// </summary>
    public partial class OMPremase
    {
        /// <summary>
        /// Ссылка на (253 БТИ: Этаж)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMFloor ParentFloor { get; set; }

    }
}


namespace ObjectModel.Bti
{
    /// <summary>
    /// 257 БТИ: Комната
    /// </summary>
    public partial class OMRooms
    {
        /// <summary>
        /// Ссылка на (253 БТИ: Этаж)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMFloor ParentFloor { get; set; }

        /// <summary>
        /// Ссылка на (254 БТИ: Помещение)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMPremase ParentPremase { get; set; }

    }
}


namespace ObjectModel.Bti
{
    /// <summary>
    /// 258 БТИ: Округ
    /// </summary>
    public partial class OMBtiOkrug
    {
        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 301 Реестр загрузки файлов
    /// </summary>
    public partial class OMInputFile
    {
        /// <summary>
        /// Ссылка на (302 Реестр журналов обработки пакета файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMLogFile ParentLogFile { get; set; }

        /// <summary>
        /// Ссылка на (321 Справочник районов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDistrict ParentDistrict { get; set; }

        /// <summary>
        /// Ссылка на (322 Хранилище файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFileStorage ParentFileStorage { get; set; }

        /// <summary>
        /// Ссылка на (325 Реестр загружаемых пакетов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFilePackage ParentInputFilePackage { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 302 Реестр журналов обработки пакета файлов
    /// </summary>
    public partial class OMLogFile
    {
        /// <summary>
        /// Ссылка на (322 Хранилище файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFileStorage ParentFileStorage { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 303 Реестр банковских файлов оплат
    /// </summary>
    public partial class OMBankPlat
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (304 Реестр cводные данные по файлу оплат)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMSvodBank ParentSvodBank { get; set; }

        /// <summary>
        /// Ссылка на (321 Справочник районов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDistrict ParentDistrict { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 304 Реестр cводные данные по файлу оплат
    /// </summary>
    public partial class OMSvodBank
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (321 Справочник районов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDistrict ParentDistrict { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 305 Реестр начислений
    /// </summary>
    public partial class OMInputNach
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFsp ParentFsp { get; set; }

        /// <summary>
        /// Ссылка на (321 Справочник районов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDistrict ParentDistrict { get; set; }

        /// <summary>
        /// Ссылка на (332 Справочник "Статус квартиры /доли")
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFlatStatus ParentFlatStatus { get; set; }

        /// <summary>
        /// Ссылка на (333 Справочник Тип жилого помещения)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFlatType ParentFlatType { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 306 Реестр зачислений (платежей)
    /// </summary>
    public partial class OMInputPlat
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (303 Реестр банковских файлов оплат)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBankPlat ParentBankPlat { get; set; }

        /// <summary>
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFsp ParentFsp { get; set; }

        /// <summary>
        /// Ссылка на (309 Реестр страховых полисов и свидетельств)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMPolicySvd ParentPolicySvd { get; set; }

        /// <summary>
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMAllProperty ParentAllProperty { get; set; }

        /// <summary>
        /// Ссылка на (321 Справочник районов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDistrict ParentDistrict { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

        /// <summary>
        /// Ссылка на (333 Справочник Тип жилого помещения)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFlatType ParentFlatType { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 307 Реестр ведомости учета страховых взносов
    /// </summary>
    public partial class OMBalance
    {
        /// <summary>
        /// Ссылка на (305 Реестр начислений)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputNach ParentInputNach { get; set; }

        /// <summary>
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFsp ParentFsp { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 308 Реестр Финансовых счетов плательщиков (ФСП)
    /// </summary>
    public partial class OMFsp
    {
        /// <summary>
        /// Ссылка на (309 Реестр страховых полисов и свидетельств)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMPolicySvd ParentPolicySvd { get; set; }

        /// <summary>
        /// Ссылка на (317 Реестр объектов страхования жилых помещений)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFlat ParentFlat { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 309 Реестр страховых полисов и свидетельств
    /// </summary>
    public partial class OMPolicySvd
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (320 Справочник округов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMOkrug ParentOkrug { get; set; }

        /// <summary>
        /// Ссылка на (321 Справочник районов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDistrict ParentDistrict { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 310 Реестр договоров страхования общего имущества
    /// </summary>
    public partial class OMAllProperty
    {
        /// <summary>
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFsp ParentFsp { get; set; }

        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBuilding ParentBuilding { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

        /// <summary>
        /// Ссылка на (345 Справочник "Управляющие компании")
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMSubject ParentSubject { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 311 Реестр доп. соглашений по договорам общего имущества
    /// </summary>
    public partial class OMDopAllProperty
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMAllProperty ParentAllProperty { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 312 Реестр расчетов параметров объектов общего имущества
    /// </summary>
    public partial class OMParamCalculation
    {
        /// <summary>
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMAllProperty ParentAllProperty { get; set; }

        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBuilding ParentBuilding { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

        /// <summary>
        /// Ссылка на (345 Справочник "Управляющие компании")
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMSubject ParentSubject { get; set; }

        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 313 Реестр дел по расчету  суммы ущерба
    /// </summary>
    public partial class OMDamage
    {
        /// <summary>
        /// Ссылка на (307 Реестр ведомости учета страховых взносов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBalance ParentBalance { get; set; }

        /// <summary>
        /// Ссылка на (317 Реестр объектов страхования жилых помещений)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFlat ParentFlat { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 314 Реестр страховых выплат
    /// </summary>
    public partial class OMPayTo
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFsp ParentFsp { get; set; }

        /// <summary>
        /// Ссылка на (313 Реестр дел по расчету  суммы ущерба)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDamage ParentDamage { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

        /// <summary>
        /// Ссылка на (345 Справочник "Управляющие компании")
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMSubject ParentSubject { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 315 Реестр сведений об отказах в страховых выплатах
    /// </summary>
    public partial class OMNoPay
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFsp ParentFsp { get; set; }

        /// <summary>
        /// Ссылка на (320 Справочник округов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMOkrug ParentOkrug { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

        /// <summary>
        /// Ссылка на (345 Справочник "Управляющие компании")
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMSubject ParentSubject { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 316 Реестр объектов страхования МКД
    /// </summary>
    public partial class OMBuilding
    {
        /// <summary>
        /// Ссылка на (251 БТИ: Здание)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiBuilding ParentBtiBuilding { get; set; }

        /// <summary>
        /// Ссылка на (258 БТИ: Округ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiOkrug ParentBtiOkrug { get; set; }

        /// <summary>
        /// Ссылка на (259 БТИ: Район)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiDistrict ParentBtiDistrict { get; set; }

        /// <summary>
        /// Ссылка на (319 Реестр адресов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMAddress ParentAddress { get; set; }

        /// <summary>
        /// Ссылка на (400 Объекты ЕГРН)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Ehd.OMBuildParcel ParentBuildParcel { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 317 Реестр объектов страхования жилых помещений
    /// </summary>
    public partial class OMFlat
    {
        /// <summary>
        /// Ссылка на (254 БТИ: Помещение)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMPremase ParentPremase { get; set; }

        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBuilding ParentBuilding { get; set; }

        /// <summary>
        /// Ссылка на (332 Справочник "Статус квартиры /доли")
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFlatStatus ParentFlatStatus { get; set; }

        /// <summary>
        /// Ссылка на (333 Справочник Тип жилого помещения)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFlatType ParentFlatType { get; set; }

        /// <summary>
        /// Ссылка на (400 Объекты ЕГРН)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Ehd.OMBuildParcel ParentBuildParcel { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 320 Справочник округов МФЦ
    /// </summary>
    public partial class OMOkrug
    {
        /// <summary>
        /// Ссылка на (258 БТИ: Округ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiOkrug ParentBtiOkrug { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 321 Справочник районов МФЦ
    /// </summary>
    public partial class OMDistrict
    {
        /// <summary>
        /// Ссылка на (320 Справочник округов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMOkrug ParentOkrug { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 325 Реестр загружаемых пакетов
    /// </summary>
    public partial class OMInputFilePackage
    {
        /// <summary>
        /// Ссылка на (320 Справочник округов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMOkrug ParentOkrug { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 326 Реестр связи объекта страхования МКД с объектами БТИ
    /// </summary>
    public partial class OMLinkBuildBti  
    {
        /// <summary>
        /// Ссылка на (251 БТИ: Здание)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiBuilding ParentBtiBuilding { get; set; }

        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBuilding ParentBuilding { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 334 Реестр проектов договоров страхования
    /// </summary>
    public partial class OMAgreementProject
    {
        /// <summary>
        /// Ссылка на (312 Реестр расчетов параметров объектов общего имущества)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMParamCalculation ParentParamCalculation { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 340 Реестр документов-оснований дел
    /// </summary>
    public partial class OMDocuments
    {
        /// <summary>
        /// Ссылка на (313 Реестр дел по расчету  суммы ущерба)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDamage ParentDamage { get; set; }

        /// <summary>
        /// Ссылка на (323 Справочник Виды документов-оснований)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDocBaseType ParentDocBaseType { get; set; }

        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 345 Справочник "Управляющие компании"
    /// </summary>
    public partial class OMSubject
    {
        /// <summary>
        /// Ссылка на (320 Справочник округов МФЦ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMOkrug ParentOkrug { get; set; }

        /// <summary>
        /// Ссылка на (344 Справочник «Банки»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBank ParentBank { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 350 Реестр расчетов ущерба по элементам конструкций
    /// </summary>
    public partial class OMDamageAmount
    {
        /// <summary>
        /// Ссылка на (324 Методики оценки ущерба)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDamageAssessmentMethod ParentDamageAssessmentMethod { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 352 Реестр регистрации изменения данных
    /// </summary>
    public partial class OMChangesLog
    {
        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 355 Реестр счетов
    /// </summary>
    public partial class OMInvoice
    {
        /// <summary>
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMFsp ParentFsp { get; set; }

        /// <summary>
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMAllProperty ParentAllProperty { get; set; }

        /// <summary>
        /// Ссылка на (313 Реестр дел по расчету  суммы ущерба)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDamage ParentDamage { get; set; }

        /// <summary>
        /// Ссылка на (344 Справочник «Банки»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBank ParentBank { get; set; }

        /// <summary>
        /// Ссылка на (345 Справочник "Управляющие компании")
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMSubject ParentSubject { get; set; }

        /// <summary>
        /// Ссылка на (354 Реестр оплат в системе ОПС)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMReestrPay ParentReestrPay { get; set; }

        /// <summary>
        /// Ссылка на (357 Справочник "Причины отказа в выплате ущерба ГБУ")
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMGbuNoPayReason ParentGbuNoPayReason { get; set; }

        /// <summary>
        /// Ссылка на (381 Сводный реестр счетов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInvoiceSvod ParentInvoiceSvod { get; set; }

        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 358 Комментарии
    /// </summary>
    public partial class OMComment
    {
        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 359 Журнал идентификации зачислений
    /// </summary>
    public partial class OMFilePlatIdentifyLog
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 370 Журнал процесса обработки файлов МФЦ
    /// </summary>
    public partial class OMFileProcessLog
    {
        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInputFile ParentInputFile { get; set; }

        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 371 Реестр расчетных сводных показателей объектов страхования МКД
    /// </summary>
    public partial class OMBuildingSvodDataCalculated
    {
        /// <summary>
        /// Ссылка на (251 БТИ: Здание)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiBuilding ParentBtiBuilding { get; set; }

        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBuilding ParentBuilding { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 373 Реестр связи СК с кодом поставщика
    /// </summary>
    public partial class OMLinkStrahPost
    {
        /// <summary>
        /// Ссылка на (328 Справочник «Страховые организации»)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMInsuranceOrganization ParentInsuranceOrganization { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 374 История изменения UNOM в ФСП
    /// </summary>
    public partial class OMUnomUpdate
    {
        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBuilding ParentBuilding { get; set; }

        /// <summary>
        /// Ссылка на (950 Пользователи системы)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Core.SRD.OMUser ParentUser { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 382 Реестр управляющих компаний
    /// </summary>
    public partial class OMOrgUnom
    {
        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMBuilding ParentBuilding { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 384 Статистика по ущербу по жилым помещениям
    /// </summary>
    public partial class OMvDjpDamageStat
    {
        /// <summary>
        /// Ссылка на (313 Реестр дел по расчету  суммы ущерба)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMDamage ParentDamage { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 385 Сптсок округов для выгрузок из реестра расчетов
    /// </summary>
    public partial class OMvOkrug2IO
    {
        /// <summary>
        /// Ссылка на (312 Реестр расчетов параметров объектов общего имущества)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMParamCalculation ParentParamCalculation { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 386 Месяцы для выгрузок по договорам
    /// </summary>
    public partial class OMvMonth4AP
    {
        /// <summary>
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMAllProperty ParentAllProperty { get; set; }

    }
}


namespace ObjectModel.Insur
{
    /// <summary>
    /// 387 Реестр Управляющих компаний
    /// </summary>
    public partial class OMUpravCompany
    {
        /// <summary>
        /// Ссылка на (258 БТИ: Округ)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Bti.OMBtiOkrug ParentBtiOkrug { get; set; }

        /// <summary>
        /// Ссылка на (382 Реестр управляющих компаний)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Insur.OMOrgUnom ParentOrgUnom { get; set; }

    }
}


namespace ObjectModel.Ehd
{
    /// <summary>
    /// 401 ehd.register
    /// </summary>
    public partial class OMRegister
    {
        /// <summary>
        /// Ссылка на (400 Объекты ЕГРН)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Ehd.OMBuildParcel ParentBuildParcel { get; set; }

    }
}


namespace ObjectModel.Ehd
{
    /// <summary>
    /// 408 Этажность
    /// </summary>
    public partial class OMFloors
    {
        /// <summary>
        /// Ссылка на (400 Объекты ЕГРН)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Ehd.OMBuildParcel ParentBuildParcel { get; set; }

    }
}


namespace ObjectModel.Ehd.Elements
{
    /// <summary>
    /// 409 Материал стен
    /// </summary>
    public partial class OMConstruct
    {
        /// <summary>
        /// Ссылка на (400 Объекты ЕГРН)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Ehd.OMBuildParcel ParentBuildParcel { get; set; }

        /// <summary>
        /// Ссылка на (410 Постройка/ввод в эксплуатацию)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Ehd.Exploitation.OMChar ParentChar { get; set; }

    }
}


namespace ObjectModel.Ehd.Exploitation
{
    /// <summary>
    /// 410 Постройка/ввод в эксплуатацию
    /// </summary>
    public partial class OMChar
    {
        /// <summary>
        /// Ссылка на (400 Объекты ЕГРН)
        /// </summary>
        [ParentRegister]
        [XmlIgnore]
        public ObjectModel.Ehd.OMBuildParcel ParentBuildParcel { get; set; }

    }
}
