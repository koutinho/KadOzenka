using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.ObjectModel.CustomAttribute;

namespace ObjectModel.Bti
{
    /// <summary>
    /// 50 БТИ: Адрес
    /// </summary>
    public partial class OMADDRESS
    {


        /// <summary>
        /// Ссылка на (52 БТИ: Адрес здания (связь))
        /// </summary>
        [Reference]
        public List<ObjectModel.Bti.OMADDRLINK> ADDRLINK { get; set; }
        public OMADDRESS()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            ADDRLINK = new List<ObjectModel.Bti.OMADDRLINK>();

        }
        public OMADDRESS(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 52 БТИ: Адрес здания (связь)
    /// </summary>
    public partial class OMADDRLINK
    {

        public OMADDRLINK()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMADDRLINK(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 250 БТИ: Адрес
    /// </summary>
    public partial class OMFads
    {

        public OMFads()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMFads(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 251 БТИ: Здание
    /// </summary>
    public partial class OMBtiBuilding
    {


        /// <summary>
        /// Ссылка на (52 БТИ: Адрес здания (связь))
        /// </summary>
        [Reference]
        public List<ObjectModel.Bti.OMADDRLINK> ADDRLINK { get; set; }

        /// <summary>
        /// Ссылка на (253 БТИ: Этаж)
        /// </summary>
        [Reference]
        public List<ObjectModel.Bti.OMFloor> Floor { get; set; }

        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBuilding> Building { get; set; }

        /// <summary>
        /// Ссылка на (326 Реестр связи объекта страхования МКД с объектами БТИ)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMLinkBuildBti  > LinkBuildBti   { get; set; }

        /// <summary>
        /// Ссылка на (371 Реестр расчетных сводных показателей объектов страхования МКД)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBuildingSvodDataCalculated> BuildingSvodDataCalculated { get; set; }
        public OMBtiBuilding()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            ADDRLINK = new List<ObjectModel.Bti.OMADDRLINK>();

            Floor = new List<ObjectModel.Bti.OMFloor>();

            Building = new List<ObjectModel.Insur.OMBuilding>();

            LinkBuildBti   = new List<ObjectModel.Insur.OMLinkBuildBti  >();

            BuildingSvodDataCalculated = new List<ObjectModel.Insur.OMBuildingSvodDataCalculated>();

        }
        public OMBtiBuilding(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (254 БТИ: Помещение)
        /// </summary>
        [Reference]
        public List<ObjectModel.Bti.OMPremase> Premase { get; set; }

        /// <summary>
        /// Ссылка на (257 БТИ: Комната)
        /// </summary>
        [Reference]
        public List<ObjectModel.Bti.OMRooms> Rooms { get; set; }
        public OMFloor()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Premase = new List<ObjectModel.Bti.OMPremase>();

            Rooms = new List<ObjectModel.Bti.OMRooms>();

        }
        public OMFloor(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (257 БТИ: Комната)
        /// </summary>
        [Reference]
        public List<ObjectModel.Bti.OMRooms> Rooms { get; set; }

        /// <summary>
        /// Ссылка на (317 Реестр объектов страхования жилых помещений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFlat> Flat { get; set; }
        public OMPremase()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Rooms = new List<ObjectModel.Bti.OMRooms>();

            Flat = new List<ObjectModel.Insur.OMFlat>();

        }
        public OMPremase(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 257 БТИ: Комната
    /// </summary>
    public partial class OMRooms
    {

        public OMRooms()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMRooms(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBuilding> Building { get; set; }

        /// <summary>
        /// Ссылка на (320 Справочник округов МФЦ)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMOkrug> Okrug { get; set; }

        /// <summary>
        /// Ссылка на (387 Реестр Управляющих компаний)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMUpravCompany> UpravCompany { get; set; }
        public OMBtiOkrug()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Building = new List<ObjectModel.Insur.OMBuilding>();

            Okrug = new List<ObjectModel.Insur.OMOkrug>();

            UpravCompany = new List<ObjectModel.Insur.OMUpravCompany>();

        }
        public OMBtiOkrug(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 259 БТИ: Район
    /// </summary>
    public partial class OMBtiDistrict
    {


        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBuilding> Building { get; set; }
        public OMBtiDistrict()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Building = new List<ObjectModel.Insur.OMBuilding>();

        }
        public OMBtiDistrict(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Bti
{
    /// <summary>
    /// 260 БТИ: Диапазоны квартир
    /// </summary>
    public partial class OMDiapKv
    {

        public OMDiapKv()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMDiapKv(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (303 Реестр банковских файлов оплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBankPlat> BankPlat { get; set; }

        /// <summary>
        /// Ссылка на (304 Реестр cводные данные по файлу оплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMSvodBank> SvodBank { get; set; }

        /// <summary>
        /// Ссылка на (305 Реестр начислений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputNach> InputNach { get; set; }

        /// <summary>
        /// Ссылка на (306 Реестр зачислений (платежей))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputPlat> InputPlat { get; set; }

        /// <summary>
        /// Ссылка на (309 Реестр страховых полисов и свидетельств)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPolicySvd> PolicySvd { get; set; }

        /// <summary>
        /// Ссылка на (311 Реестр доп. соглашений по договорам общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDopAllProperty> DopAllProperty { get; set; }

        /// <summary>
        /// Ссылка на (314 Реестр страховых выплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPayTo> PayTo { get; set; }

        /// <summary>
        /// Ссылка на (315 Реестр сведений об отказах в страховых выплатах)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMNoPay> NoPay { get; set; }

        /// <summary>
        /// Ссылка на (359 Журнал идентификации зачислений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFilePlatIdentifyLog> FilePlatIdentifyLog { get; set; }

        /// <summary>
        /// Ссылка на (370 Журнал процесса обработки файлов МФЦ)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFileProcessLog> FileProcessLog { get; set; }
        public OMInputFile()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            BankPlat = new List<ObjectModel.Insur.OMBankPlat>();

            SvodBank = new List<ObjectModel.Insur.OMSvodBank>();

            InputNach = new List<ObjectModel.Insur.OMInputNach>();

            InputPlat = new List<ObjectModel.Insur.OMInputPlat>();

            PolicySvd = new List<ObjectModel.Insur.OMPolicySvd>();

            DopAllProperty = new List<ObjectModel.Insur.OMDopAllProperty>();

            PayTo = new List<ObjectModel.Insur.OMPayTo>();

            NoPay = new List<ObjectModel.Insur.OMNoPay>();

            FilePlatIdentifyLog = new List<ObjectModel.Insur.OMFilePlatIdentifyLog>();

            FileProcessLog = new List<ObjectModel.Insur.OMFileProcessLog>();

        }
        public OMInputFile(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputFile> InputFile { get; set; }
        public OMLogFile()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputFile = new List<ObjectModel.Insur.OMInputFile>();

        }
        public OMLogFile(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (306 Реестр зачислений (платежей))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputPlat> InputPlat { get; set; }
        public OMBankPlat()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputPlat = new List<ObjectModel.Insur.OMInputPlat>();

        }
        public OMBankPlat(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (303 Реестр банковских файлов оплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBankPlat> BankPlat { get; set; }
        public OMSvodBank()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            BankPlat = new List<ObjectModel.Insur.OMBankPlat>();

        }
        public OMSvodBank(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (307 Реестр ведомости учета страховых взносов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBalance> Balance { get; set; }
        public OMInputNach()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Balance = new List<ObjectModel.Insur.OMBalance>();

        }
        public OMInputNach(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 306 Реестр зачислений (платежей)
    /// </summary>
    public partial class OMInputPlat
    {

        public OMInputPlat()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMInputPlat(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (313 Реестр дел по расчету  суммы ущерба)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDamage> Damage { get; set; }
        public OMBalance()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Damage = new List<ObjectModel.Insur.OMDamage>();

        }
        public OMBalance(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (305 Реестр начислений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputNach> InputNach { get; set; }

        /// <summary>
        /// Ссылка на (306 Реестр зачислений (платежей))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputPlat> InputPlat { get; set; }

        /// <summary>
        /// Ссылка на (307 Реестр ведомости учета страховых взносов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBalance> Balance { get; set; }

        /// <summary>
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMAllProperty> AllProperty { get; set; }

        /// <summary>
        /// Ссылка на (314 Реестр страховых выплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPayTo> PayTo { get; set; }

        /// <summary>
        /// Ссылка на (315 Реестр сведений об отказах в страховых выплатах)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMNoPay> NoPay { get; set; }

        /// <summary>
        /// Ссылка на (355 Реестр счетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInvoice> Invoice { get; set; }
        public OMFsp()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputNach = new List<ObjectModel.Insur.OMInputNach>();

            InputPlat = new List<ObjectModel.Insur.OMInputPlat>();

            Balance = new List<ObjectModel.Insur.OMBalance>();

            AllProperty = new List<ObjectModel.Insur.OMAllProperty>();

            PayTo = new List<ObjectModel.Insur.OMPayTo>();

            NoPay = new List<ObjectModel.Insur.OMNoPay>();

            Invoice = new List<ObjectModel.Insur.OMInvoice>();

        }
        public OMFsp(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (306 Реестр зачислений (платежей))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputPlat> InputPlat { get; set; }

        /// <summary>
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFsp> Fsp { get; set; }
        public OMPolicySvd()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputPlat = new List<ObjectModel.Insur.OMInputPlat>();

            Fsp = new List<ObjectModel.Insur.OMFsp>();

        }
        public OMPolicySvd(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (306 Реестр зачислений (платежей))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputPlat> InputPlat { get; set; }

        /// <summary>
        /// Ссылка на (311 Реестр доп. соглашений по договорам общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDopAllProperty> DopAllProperty { get; set; }

        /// <summary>
        /// Ссылка на (312 Реестр расчетов параметров объектов общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMParamCalculation> ParamCalculation { get; set; }

        /// <summary>
        /// Ссылка на (355 Реестр счетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInvoice> Invoice { get; set; }

        /// <summary>
        /// Ссылка на (386 Месяцы для выгрузок по договорам)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMvMonth4AP> vMonth4AP { get; set; }

        /// <summary>
        /// Ссылка на (809 Сохраненные отчеты)
        /// </summary>
        [Reference]
        public List<ObjectModel.Core.Reports.OMSavedReport> SavedReport { get; set; }
        public OMAllProperty()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputPlat = new List<ObjectModel.Insur.OMInputPlat>();

            DopAllProperty = new List<ObjectModel.Insur.OMDopAllProperty>();

            ParamCalculation = new List<ObjectModel.Insur.OMParamCalculation>();

            Invoice = new List<ObjectModel.Insur.OMInvoice>();

            vMonth4AP = new List<ObjectModel.Insur.OMvMonth4AP>();

            SavedReport = new List<ObjectModel.Core.Reports.OMSavedReport>();

        }
        public OMAllProperty(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 311 Реестр доп. соглашений по договорам общего имущества
    /// </summary>
    public partial class OMDopAllProperty
    {

        public OMDopAllProperty()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMDopAllProperty(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (334 Реестр проектов договоров страхования)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMAgreementProject> AgreementProject { get; set; }

        /// <summary>
        /// Ссылка на (385 Сптсок округов для выгрузок из реестра расчетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMvOkrug2IO> vOkrug2IO { get; set; }
        public OMParamCalculation()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            AgreementProject = new List<ObjectModel.Insur.OMAgreementProject>();

            vOkrug2IO = new List<ObjectModel.Insur.OMvOkrug2IO>();

        }
        public OMParamCalculation(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (314 Реестр страховых выплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPayTo> PayTo { get; set; }

        /// <summary>
        /// Ссылка на (340 Реестр документов-оснований дел)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDocuments> Documents { get; set; }

        /// <summary>
        /// Ссылка на (355 Реестр счетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInvoice> Invoice { get; set; }

        /// <summary>
        /// Ссылка на (384 Статистика по ущербу по жилым помещениям)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMvDjpDamageStat> vDjpDamageStat { get; set; }
        public OMDamage()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            PayTo = new List<ObjectModel.Insur.OMPayTo>();

            Documents = new List<ObjectModel.Insur.OMDocuments>();

            Invoice = new List<ObjectModel.Insur.OMInvoice>();

            vDjpDamageStat = new List<ObjectModel.Insur.OMvDjpDamageStat>();

        }
        public OMDamage(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 314 Реестр страховых выплат
    /// </summary>
    public partial class OMPayTo
    {

        public OMPayTo()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMPayTo(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 315 Реестр сведений об отказах в страховых выплатах
    /// </summary>
    public partial class OMNoPay
    {

        public OMNoPay()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMNoPay(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMAllProperty> AllProperty { get; set; }

        /// <summary>
        /// Ссылка на (312 Реестр расчетов параметров объектов общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMParamCalculation> ParamCalculation { get; set; }

        /// <summary>
        /// Ссылка на (317 Реестр объектов страхования жилых помещений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFlat> Flat { get; set; }

        /// <summary>
        /// Ссылка на (326 Реестр связи объекта страхования МКД с объектами БТИ)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMLinkBuildBti  > LinkBuildBti   { get; set; }

        /// <summary>
        /// Ссылка на (371 Реестр расчетных сводных показателей объектов страхования МКД)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBuildingSvodDataCalculated> BuildingSvodDataCalculated { get; set; }

        /// <summary>
        /// Ссылка на (374 История изменения UNOM в ФСП)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMUnomUpdate> UnomUpdate { get; set; }

        /// <summary>
        /// Ссылка на (382 Реестр управляющих компаний)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMOrgUnom> OrgUnom { get; set; }
        public OMBuilding()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            AllProperty = new List<ObjectModel.Insur.OMAllProperty>();

            ParamCalculation = new List<ObjectModel.Insur.OMParamCalculation>();

            Flat = new List<ObjectModel.Insur.OMFlat>();

            LinkBuildBti   = new List<ObjectModel.Insur.OMLinkBuildBti  >();

            BuildingSvodDataCalculated = new List<ObjectModel.Insur.OMBuildingSvodDataCalculated>();

            UnomUpdate = new List<ObjectModel.Insur.OMUnomUpdate>();

            OrgUnom = new List<ObjectModel.Insur.OMOrgUnom>();

        }
        public OMBuilding(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (308 Реестр Финансовых счетов плательщиков (ФСП))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFsp> Fsp { get; set; }

        /// <summary>
        /// Ссылка на (313 Реестр дел по расчету  суммы ущерба)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDamage> Damage { get; set; }
        public OMFlat()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Fsp = new List<ObjectModel.Insur.OMFsp>();

            Damage = new List<ObjectModel.Insur.OMDamage>();

        }
        public OMFlat(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 319 Реестр адресов
    /// </summary>
    public partial class OMAddress
    {


        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBuilding> Building { get; set; }
        public OMAddress()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Building = new List<ObjectModel.Insur.OMBuilding>();

        }
        public OMAddress(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (309 Реестр страховых полисов и свидетельств)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPolicySvd> PolicySvd { get; set; }

        /// <summary>
        /// Ссылка на (315 Реестр сведений об отказах в страховых выплатах)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMNoPay> NoPay { get; set; }

        /// <summary>
        /// Ссылка на (321 Справочник районов МФЦ)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDistrict> District { get; set; }

        /// <summary>
        /// Ссылка на (325 Реестр загружаемых пакетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputFilePackage> InputFilePackage { get; set; }

        /// <summary>
        /// Ссылка на (345 Справочник "Управляющие компании")
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMSubject> Subject { get; set; }
        public OMOkrug()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            PolicySvd = new List<ObjectModel.Insur.OMPolicySvd>();

            NoPay = new List<ObjectModel.Insur.OMNoPay>();

            District = new List<ObjectModel.Insur.OMDistrict>();

            InputFilePackage = new List<ObjectModel.Insur.OMInputFilePackage>();

            Subject = new List<ObjectModel.Insur.OMSubject>();

        }
        public OMOkrug(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputFile> InputFile { get; set; }

        /// <summary>
        /// Ссылка на (303 Реестр банковских файлов оплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBankPlat> BankPlat { get; set; }

        /// <summary>
        /// Ссылка на (304 Реестр cводные данные по файлу оплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMSvodBank> SvodBank { get; set; }

        /// <summary>
        /// Ссылка на (305 Реестр начислений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputNach> InputNach { get; set; }

        /// <summary>
        /// Ссылка на (306 Реестр зачислений (платежей))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputPlat> InputPlat { get; set; }

        /// <summary>
        /// Ссылка на (309 Реестр страховых полисов и свидетельств)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPolicySvd> PolicySvd { get; set; }
        public OMDistrict()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputFile = new List<ObjectModel.Insur.OMInputFile>();

            BankPlat = new List<ObjectModel.Insur.OMBankPlat>();

            SvodBank = new List<ObjectModel.Insur.OMSvodBank>();

            InputNach = new List<ObjectModel.Insur.OMInputNach>();

            InputPlat = new List<ObjectModel.Insur.OMInputPlat>();

            PolicySvd = new List<ObjectModel.Insur.OMPolicySvd>();

        }
        public OMDistrict(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 322 Хранилище файлов
    /// </summary>
    public partial class OMFileStorage
    {


        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputFile> InputFile { get; set; }

        /// <summary>
        /// Ссылка на (302 Реестр журналов обработки пакета файлов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMLogFile> LogFile { get; set; }

        /// <summary>
        /// Ссылка на (975 Очередь долгих процессов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Core.LongProcess.OMQueue> Queue { get; set; }
        public OMFileStorage()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputFile = new List<ObjectModel.Insur.OMInputFile>();

            LogFile = new List<ObjectModel.Insur.OMLogFile>();

            Queue = new List<ObjectModel.Core.LongProcess.OMQueue>();

        }
        public OMFileStorage(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 323 Справочник Виды документов-оснований
    /// </summary>
    public partial class OMDocBaseType
    {


        /// <summary>
        /// Ссылка на (340 Реестр документов-оснований дел)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDocuments> Documents { get; set; }
        public OMDocBaseType()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Documents = new List<ObjectModel.Insur.OMDocuments>();

        }
        public OMDocBaseType(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 324 Методики оценки ущерба
    /// </summary>
    public partial class OMDamageAssessmentMethod
    {


        /// <summary>
        /// Ссылка на (350 Реестр расчетов ущерба по элементам конструкций)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDamageAmount> DamageAmount { get; set; }
        public OMDamageAssessmentMethod()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            DamageAmount = new List<ObjectModel.Insur.OMDamageAmount>();

        }
        public OMDamageAssessmentMethod(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputFile> InputFile { get; set; }
        public OMInputFilePackage()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputFile = new List<ObjectModel.Insur.OMInputFile>();

        }
        public OMInputFilePackage(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 326 Реестр связи объекта страхования МКД с объектами БТИ
    /// </summary>
    public partial class OMLinkBuildBti  
    {

        public OMLinkBuildBti  ()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMLinkBuildBti  (bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 327 Реестр связи между объектом страхования ЖП с помещениями в Росреестре
    /// </summary>
    public partial class OMLinkFlatEgrn
    {

        public OMLinkFlatEgrn()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMLinkFlatEgrn(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 328 Справочник «Страховые организации»
    /// </summary>
    public partial class OMInsuranceOrganization
    {


        /// <summary>
        /// Ссылка на (258 БТИ: Округ)
        /// </summary>
        [Reference]
        public List<ObjectModel.Bti.OMBtiOkrug> BtiOkrug { get; set; }

        /// <summary>
        /// Ссылка на (301 Реестр загрузки файлов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputFile> InputFile { get; set; }

        /// <summary>
        /// Ссылка на (306 Реестр зачислений (платежей))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputPlat> InputPlat { get; set; }

        /// <summary>
        /// Ссылка на (309 Реестр страховых полисов и свидетельств)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPolicySvd> PolicySvd { get; set; }

        /// <summary>
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMAllProperty> AllProperty { get; set; }

        /// <summary>
        /// Ссылка на (312 Реестр расчетов параметров объектов общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMParamCalculation> ParamCalculation { get; set; }

        /// <summary>
        /// Ссылка на (313 Реестр дел по расчету  суммы ущерба)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDamage> Damage { get; set; }

        /// <summary>
        /// Ссылка на (314 Реестр страховых выплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPayTo> PayTo { get; set; }

        /// <summary>
        /// Ссылка на (315 Реестр сведений об отказах в страховых выплатах)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMNoPay> NoPay { get; set; }

        /// <summary>
        /// Ссылка на (340 Реестр документов-оснований дел)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMDocuments> Documents { get; set; }

        /// <summary>
        /// Ссылка на (373 Реестр связи СК с кодом поставщика)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMLinkStrahPost> LinkStrahPost { get; set; }
        public OMInsuranceOrganization()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            BtiOkrug = new List<ObjectModel.Bti.OMBtiOkrug>();

            InputFile = new List<ObjectModel.Insur.OMInputFile>();

            InputPlat = new List<ObjectModel.Insur.OMInputPlat>();

            PolicySvd = new List<ObjectModel.Insur.OMPolicySvd>();

            AllProperty = new List<ObjectModel.Insur.OMAllProperty>();

            ParamCalculation = new List<ObjectModel.Insur.OMParamCalculation>();

            Damage = new List<ObjectModel.Insur.OMDamage>();

            PayTo = new List<ObjectModel.Insur.OMPayTo>();

            NoPay = new List<ObjectModel.Insur.OMNoPay>();

            Documents = new List<ObjectModel.Insur.OMDocuments>();

            LinkStrahPost = new List<ObjectModel.Insur.OMLinkStrahPost>();

        }
        public OMInsuranceOrganization(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 329 Справочник «Доля ответственности СК»
    /// </summary>
    public partial class OMPartCompensation
    {

        public OMPartCompensation()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMPartCompensation(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 330 Справочник «Базовый тариф»
    /// </summary>
    public partial class OMBaseTariff
    {

        public OMBaseTariff()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMBaseTariff(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 331 Справочник укрупненных показателей удельного веса восстановительной стоимости конструктивных элементов жилого помещения
    /// </summary>
    public partial class OMIntegrateIndicatorsReplecmentCost
    {

        public OMIntegrateIndicatorsReplecmentCost()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMIntegrateIndicatorsReplecmentCost(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 332 Справочник "Статус квартиры /доли"
    /// </summary>
    public partial class OMFlatStatus
    {


        /// <summary>
        /// Ссылка на (305 Реестр начислений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputNach> InputNach { get; set; }

        /// <summary>
        /// Ссылка на (317 Реестр объектов страхования жилых помещений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFlat> Flat { get; set; }
        public OMFlatStatus()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputNach = new List<ObjectModel.Insur.OMInputNach>();

            Flat = new List<ObjectModel.Insur.OMFlat>();

        }
        public OMFlatStatus(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 333 Справочник Тип жилого помещения
    /// </summary>
    public partial class OMFlatType
    {


        /// <summary>
        /// Ссылка на (305 Реестр начислений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputNach> InputNach { get; set; }

        /// <summary>
        /// Ссылка на (306 Реестр зачислений (платежей))
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInputPlat> InputPlat { get; set; }

        /// <summary>
        /// Ссылка на (317 Реестр объектов страхования жилых помещений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFlat> Flat { get; set; }
        public OMFlatType()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            InputNach = new List<ObjectModel.Insur.OMInputNach>();

            InputPlat = new List<ObjectModel.Insur.OMInputPlat>();

            Flat = new List<ObjectModel.Insur.OMFlat>();

        }
        public OMFlatType(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 334 Реестр проектов договоров страхования
    /// </summary>
    public partial class OMAgreementProject
    {

        public OMAgreementProject()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMAgreementProject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 340 Реестр документов-оснований дел
    /// </summary>
    public partial class OMDocuments
    {

        public OMDocuments()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMDocuments(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 344 Справочник «Банки»
    /// </summary>
    public partial class OMBank
    {


        /// <summary>
        /// Ссылка на (345 Справочник "Управляющие компании")
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMSubject> Subject { get; set; }

        /// <summary>
        /// Ссылка на (355 Реестр счетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInvoice> Invoice { get; set; }
        public OMBank()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Subject = new List<ObjectModel.Insur.OMSubject>();

            Invoice = new List<ObjectModel.Insur.OMInvoice>();

        }
        public OMBank(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (310 Реестр договоров страхования общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMAllProperty> AllProperty { get; set; }

        /// <summary>
        /// Ссылка на (312 Реестр расчетов параметров объектов общего имущества)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMParamCalculation> ParamCalculation { get; set; }

        /// <summary>
        /// Ссылка на (314 Реестр страховых выплат)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMPayTo> PayTo { get; set; }

        /// <summary>
        /// Ссылка на (315 Реестр сведений об отказах в страховых выплатах)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMNoPay> NoPay { get; set; }

        /// <summary>
        /// Ссылка на (355 Реестр счетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInvoice> Invoice { get; set; }
        public OMSubject()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            AllProperty = new List<ObjectModel.Insur.OMAllProperty>();

            ParamCalculation = new List<ObjectModel.Insur.OMParamCalculation>();

            PayTo = new List<ObjectModel.Insur.OMPayTo>();

            NoPay = new List<ObjectModel.Insur.OMNoPay>();

            Invoice = new List<ObjectModel.Insur.OMInvoice>();

        }
        public OMSubject(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 347 Справочник "Тарифы по страхованию общего имущества"
    /// </summary>
    public partial class OMCommonPropertyTariff
    {

        public OMCommonPropertyTariff()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMCommonPropertyTariff(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 348 Справочник «Страховая стоимость ЖП»
    /// </summary>
    public partial class OMLivingPremiseInsurCost
    {

        public OMLivingPremiseInsurCost()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMLivingPremiseInsurCost(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 349 Справочник "Доля ответственности СК и города"
    /// </summary>
    public partial class OMShareResponsibilityICCity
    {

        public OMShareResponsibilityICCity()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMShareResponsibilityICCity(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 350 Реестр расчетов ущерба по элементам конструкций
    /// </summary>
    public partial class OMDamageAmount
    {

        public OMDamageAmount()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMDamageAmount(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 351 Справочник «Страховой тариф»
    /// </summary>
    public partial class OMTariff
    {

        public OMTariff()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMTariff(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 352 Реестр регистрации изменения данных
    /// </summary>
    public partial class OMChangesLog
    {

        public OMChangesLog()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMChangesLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 353 Справочник "Коэффициент пересчета действительной стоимости"
    /// </summary>
    public partial class OMActualCostRatio
    {

        public OMActualCostRatio()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMActualCostRatio(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 354 Реестр оплат в системе ОПС
    /// </summary>
    public partial class OMReestrPay
    {


        /// <summary>
        /// Ссылка на (355 Реестр счетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInvoice> Invoice { get; set; }
        public OMReestrPay()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Invoice = new List<ObjectModel.Insur.OMInvoice>();

        }
        public OMReestrPay(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 355 Реестр счетов
    /// </summary>
    public partial class OMInvoice
    {

        public OMInvoice()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMInvoice(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 356 Реестр связи справочник причин ущерба и подпричн для ЖП
    /// </summary>
    public partial class OMLinkCausesSubreasonLP
    {

        public OMLinkCausesSubreasonLP()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMLinkCausesSubreasonLP(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 357 Справочник "Причины отказа в выплате ущерба ГБУ"
    /// </summary>
    public partial class OMGbuNoPayReason
    {


        /// <summary>
        /// Ссылка на (355 Реестр счетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInvoice> Invoice { get; set; }
        public OMGbuNoPayReason()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Invoice = new List<ObjectModel.Insur.OMInvoice>();

        }
        public OMGbuNoPayReason(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 358 Комментарии
    /// </summary>
    public partial class OMComment
    {

        public OMComment()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMComment(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 359 Журнал идентификации зачислений
    /// </summary>
    public partial class OMFilePlatIdentifyLog
    {

        public OMFilePlatIdentifyLog()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMFilePlatIdentifyLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 360 Журнал формирования объектов страхования для Зданий
    /// </summary>
    public partial class OMInsurBuildingLog
    {

        public OMInsurBuildingLog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMInsurBuildingLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 361 Журнал формирования помещений страхования
    /// </summary>
    public partial class OMInsurFlatBuildingLog
    {

        public OMInsurFlatBuildingLog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMInsurFlatBuildingLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 362 Журнал загрузки таблицы ehd.building_parcel
    /// </summary>
    public partial class OMEhdBuildingParcelLog
    {

        public OMEhdBuildingParcelLog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMEhdBuildingParcelLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 363 Журнал загрузки таблицы ehd.register
    /// </summary>
    public partial class OMEhdRegisterLog
    {

        public OMEhdRegisterLog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMEhdRegisterLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 364 Журнал загрузки таблицы ehd.location
    /// </summary>
    public partial class OMEhdLocationLog
    {

        public OMEhdLocationLog()
        {

            id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMEhdLocationLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 365 Журнал загрузки таблицы ehd.egrp
    /// </summary>
    public partial class OMEhdEGRPLog
    {

        public OMEhdEGRPLog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMEhdEGRPLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 366 Журнал загрузки таблицы ehd.right
    /// </summary>
    public partial class OMEhdRightLog
    {

        public OMEhdRightLog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMEhdRightLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 367 Журнал загрузки таблицы ehd.old_numbers
    /// </summary>
    public partial class OMEhdOldNumbersLog
    {

        public OMEhdOldNumbersLog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMEhdOldNumbersLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 368 Реестр связей типов здания с этажностью и типом констуркции
    /// </summary>
    public partial class OMTypeBuldingFloorLink
    {

        public OMTypeBuldingFloorLink()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMTypeBuldingFloorLink(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.ImportLog
{
    /// <summary>
    /// 369 Журнал загрузки данных БТИ
    /// </summary>
    public partial class OMBtiImportLog
    {

        public OMBtiImportLog()
        {

            BtiId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMBtiImportLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 370 Журнал процесса обработки файлов МФЦ
    /// </summary>
    public partial class OMFileProcessLog
    {

        public OMFileProcessLog()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMFileProcessLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 371 Реестр расчетных сводных показателей объектов страхования МКД
    /// </summary>
    public partial class OMBuildingSvodDataCalculated
    {

        public OMBuildingSvodDataCalculated()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMBuildingSvodDataCalculated(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 372 Подписанты
    /// </summary>
    public partial class OMPodpisant
    {

        public OMPodpisant()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMPodpisant(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 373 Реестр связи СК с кодом поставщика
    /// </summary>
    public partial class OMLinkStrahPost
    {

        public OMLinkStrahPost()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMLinkStrahPost(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 374 История изменения UNOM в ФСП
    /// </summary>
    public partial class OMUnomUpdate
    {

        public OMUnomUpdate()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMUnomUpdate(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 380 АП - Общая статистика
    /// </summary>
    public partial class OMApCommon
    {

        public OMApCommon()
        {

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMApCommon(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 381 Сводный реестр счетов
    /// </summary>
    public partial class OMInvoiceSvod
    {


        /// <summary>
        /// Ссылка на (355 Реестр счетов)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMInvoice> Invoice { get; set; }
        public OMInvoiceSvod()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Invoice = new List<ObjectModel.Insur.OMInvoice>();

        }
        public OMInvoiceSvod(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (387 Реестр Управляющих компаний)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMUpravCompany> UpravCompany { get; set; }
        public OMOrgUnom()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            UpravCompany = new List<ObjectModel.Insur.OMUpravCompany>();

        }
        public OMOrgUnom(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 383 Реестр связи ФСП и объектами ЖП
    /// </summary>
    public partial class OMLinkFspFlat
    {

        public OMLinkFspFlat()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMLinkFspFlat(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 385 Сптсок округов для выгрузок из реестра расчетов
    /// </summary>
    public partial class OMvOkrug2IO
    {

        public OMvOkrug2IO()
        {

            Column385000100 = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMvOkrug2IO(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 386 Месяцы для выгрузок по договорам
    /// </summary>
    public partial class OMvMonth4AP
    {

        public OMvMonth4AP()
        {

            Column386000100 = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMvMonth4AP(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Insur
{
    /// <summary>
    /// 387 Реестр Управляющих компаний
    /// </summary>
    public partial class OMUpravCompany
    {

        public OMUpravCompany()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMUpravCompany(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 400 Объекты ЕГРН
    /// </summary>
    public partial class OMBuildParcel
    {


        /// <summary>
        /// Ссылка на (316 Реестр объектов страхования МКД)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMBuilding> Building { get; set; }

        /// <summary>
        /// Ссылка на (317 Реестр объектов страхования жилых помещений)
        /// </summary>
        [Reference]
        public List<ObjectModel.Insur.OMFlat> Flat { get; set; }

        /// <summary>
        /// Ссылка на (401 ehd.register)
        /// </summary>
        [Reference]
        public List<ObjectModel.Ehd.OMRegister> Register { get; set; }

        /// <summary>
        /// Ссылка на (408 Этажность)
        /// </summary>
        [Reference]
        public List<ObjectModel.Ehd.OMFloors> Floors { get; set; }

        /// <summary>
        /// Ссылка на (409 Материал стен)
        /// </summary>
        [Reference]
        public List<ObjectModel.Ehd.Elements.OMConstruct> Construct { get; set; }

        /// <summary>
        /// Ссылка на (410 Постройка/ввод в эксплуатацию)
        /// </summary>
        [Reference]
        public List<ObjectModel.Ehd.Exploitation.OMChar> Char { get; set; }
        public OMBuildParcel()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Building = new List<ObjectModel.Insur.OMBuilding>();

            Flat = new List<ObjectModel.Insur.OMFlat>();

            Register = new List<ObjectModel.Ehd.OMRegister>();

            Floors = new List<ObjectModel.Ehd.OMFloors>();

            Construct = new List<ObjectModel.Ehd.Elements.OMConstruct>();

            Char = new List<ObjectModel.Ehd.Exploitation.OMChar>();

        }
        public OMBuildParcel(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 401 ehd.register
    /// </summary>
    public partial class OMRegister
    {

        public OMRegister()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMRegister(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 402 ehd.location
    /// </summary>
    public partial class OMLocation
    {

        public OMLocation()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMLocation(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 405 EHD.EGRP
    /// </summary>
    public partial class OMEgrp
    {

        public OMEgrp()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMEgrp(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 406 EHD.RIGHT
    /// </summary>
    public partial class OMRight
    {

        public OMRight()
        {

            EmpId = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMRight(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 407 EHD.OLD_NUMBERS
    /// </summary>
    public partial class OMOldNumber
    {

        public OMOldNumber()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMOldNumber(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Ehd
{
    /// <summary>
    /// 408 Этажность
    /// </summary>
    public partial class OMFloors
    {

        public OMFloors()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMFloors(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.Ehd.Elements
{
    /// <summary>
    /// 409 Материал стен
    /// </summary>
    public partial class OMConstruct
    {

        public OMConstruct()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMConstruct(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
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
        /// Ссылка на (409 Материал стен)
        /// </summary>
        [Reference]
        public List<ObjectModel.Ehd.Elements.OMConstruct> Construct { get; set; }
        public OMChar()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

            Construct = new List<ObjectModel.Ehd.Elements.OMConstruct>();

        }
        public OMChar(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.MV
{
    /// <summary>
    /// 500 Список REFRESH MATERIALIZED VIEW
    /// </summary>
    public partial class OMRefreshList
    {

        public OMRefreshList()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMRefreshList(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}

namespace ObjectModel.MV
{
    /// <summary>
    /// 501 Список логов RefreshList
    /// </summary>
    public partial class OMRefreshLog
    {

        public OMRefreshLog()
        {

            Id = -1;

            CollectPropertyChanged = true;
            PropertyChangedList = new HashSet<String>();

        }
        public OMRefreshLog(bool trackPropertyChanging) : this()
        {
            CollectPropertyChanged = trackPropertyChanging;
        }
    }
}