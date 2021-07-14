using Core.Register;
using Core.Register.DAL;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using ObjectModel.Declarations;
using ObjectModel.Directory;
using ObjectModel.Directory.Declarations;
using ObjectModel.Directory.Sud;
using ObjectModel.KO;
using ObjectModel.Sud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JetBrains.Annotations;

namespace KadOzenka.Dal.Gadgets
{
    public class GadgetService
    {
        private static string _moveToPageSymbol = string.Empty;

        private static QSQuery GetQuery(string registerViewId)
        {
            var registerView = RegisterCommonConfiguration.GetRegisterViewConfiguration(registerViewId);

            var layout = LayoutEditorDAL.GetLayoutWithDetails(registerView.DefaultLayoutId);

            QSQuery query = RegisterCommonConfiguration
                .GetLayoutQuery(HttpContextHelper.HttpContext, layout, registerView).GetCountQuery();

            query.Columns = new List<QSColumn>
            {
                new QSColumnFunction
                {
                    FunctionType = QSColumnFunctionType.CountDistinct,
                    Operands = new List<QSColumn>
                    {
                        new QSColumnSimple(RegisterCache.GetPKAttributeID(query.MainRegisterID))
                    }
                }
            };

            return query;
        }

        /// <summary>
        /// 1. Объекты
        /// </summary>
        [UsedImplicitly]
        public static DataTable ObjectsWidget()
        {
            string linkParam = "Transition=1&31501000={Type}";

            var objects = OMObject.Where(GetQuery("SudObjects"))
                .GroupBy(x => x.Typeobj_Code)
                .ExecuteSelect(x => new
                {
                    x.Typeobj_Code,
                    Count = QSExtensions.Count<OMObject>(y => 1)
                });

            var data = new DataTable();

            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS))
            {
                data.Rows.Add(
                    linkParam.Replace("{Type}", SudObjectType.Site.GetEnumCode()),
                    "Участки",
                    objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Site)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", SudObjectType.Building.GetEnumCode()),
                    "Здания",
                    objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Building)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", SudObjectType.Room.GetEnumCode()),
                    "Помещения",
                    objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Room)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", SudObjectType.Construction.GetEnumCode()),
                    "Сооружения",
                    objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Construction)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", SudObjectType.Ons.GetEnumCode()),
                    "Онс",
                    objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Ons)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", SudObjectType.ParkingPlace.GetEnumCode()),
                    "Машиноместа",
                    objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.ParkingPlace)?.Count ?? 0);
            }

            return data;
        }

        /// <summary>
        /// 2. Суды
        /// </summary>
        [UsedImplicitly]
        public static DataTable СourtsWidget()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET))
            {
                var otchetCount = OMOtchet.Where(GetQuery("SudOtchet")).ExecuteCount();
                data.Rows.Add("SudOtchet", "Отчеты", otchetCount);
            }

            if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK))
            {
                var zakCount = OMOtchet.Where(GetQuery("SudZak")).ExecuteCount();
                data.Rows.Add("SudZak", "Заключения", zakCount);
            }

            if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH))
            {
                var reshCount = OMOtchet.Where(GetQuery("SudResh")).ExecuteCount();
                data.Rows.Add("SudResh", "Решения", reshCount);
            }

            return data;
        }

        /// <summary>
        /// Декларации по типам объектов
        /// </summary>
        [UsedImplicitly]
        public static DataTable DeclarationsObjectTypesWidget()
        {
            string linkParam = "Transition=1&50101200={Type}";

            var objects = OMDeclaration.Where(GetQuery("DeclarationsDeclaration"))
                .GroupBy(x => x.TypeObj_Code)
                .ExecuteSelect(x => new
                {
                    x.TypeObj_Code,
                    Count = QSExtensions.Count<OMDeclaration>(y => 1)
                });

            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS))
            {
                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.Site.GetEnumCode()),
                    "Земельные участки",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Site)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.Building.GetEnumCode()),
                    "Здания",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Building)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.Room.GetEnumCode()),
                    "Помещения",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Room)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.Construction.GetEnumCode()),
                    "Сооружения",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Construction)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.ParkingPlace.GetEnumCode()),
                    "Машино-места",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.ParkingPlace)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.Ons.GetEnumCode()),
                    "ОНС",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Ons)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.Ens.GetEnumCode()),
                    "Единые недвижимые комплексы",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Ens)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.Pik.GetEnumCode()),
                    "Производственно имущественные комплексы",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Pik)?.Count ?? 0);

                data.Rows.Add(
                    linkParam.Replace("{Type}", ObjectType.Other.GetEnumCode()),
                    "Иное",
                    objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Other)?.Count ?? 0);
            }

            return data;
        }

        /// <summary>
        /// Объекты недвижимости (карточка основного рабочего стола)
        /// </summary>
        [UsedImplicitly]
        public static DataTable GbuObjects()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add("/RegistersView/GbuObjects", "Реестр объектов недвижимости", _moveToPageSymbol);
            data.Rows.Add("/GbuObject/Harmonization?useMasterPage=true", "Выполнить гармонизацию", _moveToPageSymbol);
            data.Rows.Add("~/GbuObject/GroupingObject?useMasterPage=true", "Выполнить нормализацию", _moveToPageSymbol);
            data.Rows.Add("~/GbuObject/GroupingObjectFinalize?useMasterPage=true", "Финализировать нормализацию",
                _moveToPageSymbol);
            data.Rows.Add("~/GbuObject/Inheritance?useMasterPage=true", "Выполнить наследование", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/GbuCodJob", "Справочники ЦОД", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/Documents", "Реестр документов", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/ObjectsCharacteristics", "Характеристики объектов", _moveToPageSymbol);

            return data;
        }

        /// <summary>
        /// Объекты аналоги (карточка основного рабочего стола)
        /// </summary>
        [UsedImplicitly]
        public static DataTable MarketObjects()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add("/RegistersView/MarketObjects", "Реестр объектов аналогов", _moveToPageSymbol);
            data.Rows.Add("/Map", "На карту", _moveToPageSymbol);
            data.Rows.Add("Map/VisMio", "Карта ВИСМИО-МО", _moveToPageSymbol);

            return data;
        }

        /// <summary>
        /// Судебные решения (карточка основного рабочего стола)
        /// </summary>
        [UsedImplicitly]
        public static DataTable Sud()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add("/RegistersView/SudObjects", "Перейти к объектам", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/SudOtchet", "Перейти к отчетам", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/SudZak", "Перейти к заключениям", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/SudResh", "Перейти к решениям", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/SudSpdRegister", "Перейти к заявкам СПД", _moveToPageSymbol);

            return data;
        }

        /// <summary>
        /// Расчетная подсистема (карточка основного рабочего стола)
        /// </summary>
        [UsedImplicitly]
        public static DataTable Ko()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add("/RegistersView/KoTasks", "Задания на оценку", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/KoObjects", "Единицы оценки", _moveToPageSymbol);
            data.Rows.Add("/GknDataImport/ImportGkn?useMasterPage=true", "Создать задание на оценку",
                _moveToPageSymbol);
            data.Rows.Add("/Task/CalculateCadastralPrice?useMasterPage=true", "Произвести расчет кадастровой стоимости",
                _moveToPageSymbol);
            data.Rows.Add("ObjectCard?RegisterViewId=KoTours&amp;isVertical=true&amp;useMasterPage=true",
                "Перейти к справочнику туров", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/KoModels", "Перейти к справочнику моделей", string.Empty);

            if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.KO_DICT_MODELS_APPROVED))
            {
                data.Rows.Add("/RegistersView/KoApprovedModels", "Утвержденные модели", string.Empty);
            }

            data.Rows.Add("/RegistersView/GroupingDictionaries", "Перейти к справочникам для группировки",
                string.Empty);
            data.Rows.Add("/Task/TransferAttributes?useMasterPage=true", "Перенос атрибутов", _moveToPageSymbol);
            data.Rows.Add("/Task/CreateAndTransferAttributes?useMasterPage=true", "Перенос и создание атрибутов",
                _moveToPageSymbol);
            data.Rows.Add("/RegistersView/KoToursFactors", "Ценообразующие факторы", _moveToPageSymbol);

            return data;
        }

        /// <summary>
        /// Декларации (карточка основного рабочего стола)
        /// </summary>
        [UsedImplicitly]
        public static DataTable Declarations()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add("/RegistersView/DeclarationsDeclaration", "Перейти к декларациям", _moveToPageSymbol);
            data.Rows.Add("/Declarations/EditDeclaration?useMasterPage=true", "Перейти к добавлению декларации",
                _moveToPageSymbol);
            data.Rows.Add("/RegistersView/DeclarationsBook", "Перейти к книгам", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/DeclarationsSubject", "Перейти в субъекты", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/DeclarationsSignatory", "Перейти в подписанты", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/DeclarationsSpdRegister", "Перейти к заявкам СПД", _moveToPageSymbol);

            return data;
        }

        /// <summary>
        /// Комиссии по рассмотрению споров о результатах определения кадастровой стоимости (карточка основного рабочего стола)
        /// </summary>
        [UsedImplicitly]
        public static DataTable Commissions()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add("/RegistersView/CommissionCost", "Перейти к комиссиям", _moveToPageSymbol);
            data.Rows.Add("/Commission/EditCommission?useMasterPage=true", "Перейти добавлению комиссии",
                _moveToPageSymbol);

            return data;
        }

        /// <summary>
        /// Поддержка принятия управленческих решений (карточка основного рабочего стола)
        /// </summary>
        [UsedImplicitly]
        public static DataTable ManagementDecisionSupport()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add("/ManagementDecisionSupport/Map", "Построение тематических карт", _moveToPageSymbol);
            data.Rows.Add("/ManagementDecisionSupport/StatisticalData?useMasterPage=true", "Статистическая информация",
                _moveToPageSymbol);
            // Пункт меню есть, но URL не указан на основной странице?
            data.Rows.Add("#", "Экономический анализ", _moveToPageSymbol);
            data.Rows.Add("/Report/Viewer?ReportTypeId=1007", "Количество вновь учтенных объектов недвижимости",
                _moveToPageSymbol);
            data.Rows.Add("/Report/Viewer?ReportTypeId=1008", "Количество ранее учтенных объектов недвижимости",
                _moveToPageSymbol);
            data.Rows.Add("/Report/Viewer?ReportTypeId=1009", "Количество измененных объектов недвижимости",
                _moveToPageSymbol);

            return data;
        }

        /// <summary>
        /// Единицы оценки по типам
        /// </summary>
        [UsedImplicitly]
        public static DataTable KoUnitTypesWidget()
        {
            string linkParam = "Transition=1&20101800={PropertyType}";

            var objects = OMUnit.Where(GetQuery("KoObjectsWithoutDefaultSearchControls"))
                .GroupBy(x => x.PropertyType_Code)
                .ExecuteSelect(x => new
                {
                    x.PropertyType_Code,
                    Count = QSExtensions.Count<OMObject>(y => 1)
                });

            var data = new DataTable();

            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.Stead).ToString()),
                "Земельные участки",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.Stead)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.Building).ToString()),
                "Здания",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.Building)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.Pllacement).ToString()),
                "Помещения",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.Pllacement)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.Construction).ToString()),
                "Сооружения",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.Construction)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.UncompletedBuilding).ToString()),
                "Объекты незавершенного строительства",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.UncompletedBuilding)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.Company).ToString()),
                "Предприятия как имущественные комплексы",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.Company)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.UnitedPropertyComplex).ToString()),
                "Единые недвижимые комплексы",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.UnitedPropertyComplex)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.Parking).ToString()),
                "Машино-места",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.Parking)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", ((long) PropertyTypes.Other).ToString()),
                "Иные объекты недвижимости",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.Other)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{PropertyType}", PropertyTypes.OtherMore.GetEnumCode()),
                "Сооружения, ОНС, ЕНК, и иные ОН",
                objects.FirstOrDefault(x => x.PropertyType_Code == PropertyTypes.OtherMore)?.Count ?? 0);

            return data;
        }

        /// <summary>
        /// "Статистика по работе с объектами"
        /// Единицы оценки по статусу для заданий на оценку на заданную дату актуализации
        /// </summary>
        [UsedImplicitly]
        public static DataTable KoUnitTaskStatusWidgetForActualDate()
        {
            //TODO: дата актуализации должна будет передаваться в виджет
            //DateTime? actualDate = new DateTime(2018, 1, 1);
            DateTime? actualDate = DateTime.Now.Date;
            var dateFrom = actualDate.HasValue
                ? actualDate.Value.Date
                : DateTime.Now.Date;
            var dateTo = actualDate.HasValue
                ? actualDate.Value.GetEndOfTheDay()
                : DateTime.Now.GetEndOfTheDay();

            string linkParam = "Transition=1&20100700={Status}&20300200={actualDate}"
                .Replace("{actualDate}", dateFrom.ToString("dd.MM.yyyy"));

            var objects = OMUnit
                .Where(x => x.ParentTask.CreationDate >= dateFrom && x.ParentTask.CreationDate <= dateTo)
                .GroupBy(x => x.Status_Code)
                .ExecuteSelect(x => new
                {
                    x.Status_Code,
                    Count = QSExtensions.Count<OMObject>(y => 1)
                });

            var data = new DataTable();

            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});

            data.Rows.Add(
                linkParam.Replace("{Status}", ((long) KoUnitStatus.Initial).ToString()),
                "Исходный",
                objects.FirstOrDefault(x => x.Status_Code == KoUnitStatus.Initial)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{Status}", ((long) KoUnitStatus.New).ToString()),
                "Новый",
                objects.FirstOrDefault(x => x.Status_Code == KoUnitStatus.New)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{Status}", ((long) KoUnitStatus.Recalculated).ToString()),
                "Пересчитанный",
                objects.FirstOrDefault(x => x.Status_Code == KoUnitStatus.Recalculated)?.Count ?? 0);

            data.Rows.Add(
                linkParam.Replace("{Status}", ((long) KoUnitStatus.Annual).ToString()),
                "Годовой",
                objects.FirstOrDefault(x => x.Status_Code == KoUnitStatus.Annual)?.Count ?? 0);

            return data;
        }

        /// <summary>
        /// "Подсистема мониторинга обращений"
        /// </summary>
        [UsedImplicitly]
        public static DataTable CitizensAppeals()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] {new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value")});
            return data;
        }
    }
}