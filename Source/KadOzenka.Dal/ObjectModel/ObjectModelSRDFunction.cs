namespace ObjectModel.SRD
      {
          /// <summary>
          /// Словарь названий функций, поддерживаемых платформой RDP
          /// </summary>
          public class SRDCoreFunctions
          {
              // Объекты страхования (1)
              public const string INSUR_OBJ = "INSUR.OBJ";
                
              // ФСП (2)
              public const string INSUR_FSP = "INSUR.FSP";
                
              // Данные МФЦ (3)
              public const string INSUR_MFC = "INSUR.MFC";
                
              // Данные СК (4)
              public const string INSUR_SK = "INSUR.SK";
                
              // Анализ ущербов (5)
              public const string INSUR_DAMAGE = "INSUR.DAMAGE";
                
              // Общее имущество (6)
              public const string INSUR_COMMPROP = "INSUR.COMMPROP";
                
              // Многоквартирные дома (7)
              public const string INSUR_OBJ_BUILDING = "INSUR.OBJ.BUILDING";
                
              // Жилые помещения (8)
              public const string INSUR_OBJ_FLAT = "INSUR.OBJ.FLAT";
                
              // Редактирование (9)
              public const string INSUR_OBJ_BUILDING_WRITE = "INSUR.OBJ.BUILDING.WRITE";
                
              // Выгрузка в Excel (12)
              public const string INSUR_OBJ_BUILDING_EXPORT = "INSUR.OBJ.BUILDING.EXPORT";
                
              // Редактирование (15)
              public const string INSUR_OBJ_FLAT_WRITE = "INSUR.OBJ.FLAT.WRITE";
                
              // Выгрузка в Excel (16)
              public const string INSUR_OBJ_FLAT_EXPORT = "INSUR.OBJ.FLAT.EXPORT";
                
              // ФСП по ЕПД (17)
              public const string INSUR_FSP_EPD = "INSUR.FSP.EPD";
                
              // ФСП по Полисам (18)
              public const string INSUR_FSP_POLICY = "INSUR.FSP.POLICY";
                
              // ФСП по Свидетельствам (19)
              public const string INSUR_FSP_SVD = "INSUR.FSP.SVD";
                
              // Связать с помещением (20)
              public const string INSUR_FSP_EPD_ATTACHTOFLAT = "INSUR.FSP.EPD.ATTACHTOFLAT";
                
              // Выгрузка в Еxcel (21)
              public const string INSUR_FSP_EPD_EXPORT = "INSUR.FSP.EPD.EXPORT";
                
              // Связать с помещением (22)
              public const string INSUR_FSP_POLICY_ATTACHTOFLAT = "INSUR.FSP.POLICY.ATTACHTOFLAT";
                
              // Выгрузка в Excel (23)
              public const string INSUR_FSP_POLICY_EXPORT = "INSUR.FSP.POLICY.EXPORT";
                
              // Связать с помещением (24)
              public const string INSUR_FSP_SVD_ATTACHTOFLAT = "INSUR.FSP.SVD.ATTACHTOFLAT";
                
              // Выгрузка в Excel (25)
              public const string INSUR_FSP_SVD_EXPORT = "INSUR.FSP.SVD.EXPORT";
                
              // Загрузка данных (26)
              public const string INSUR_MFC_IMPORT = "INSUR.MFC.IMPORT";
                
              // Начисления страховых взносов (27)
              public const string INSUR_MFC_NACH = "INSUR.MFC.NACH";
                
              // Зачисления по страховым взносам (28)
              public const string INSUR_MFC_ZACH = "INSUR.MFC.ZACH";
                
              // Банковские строки оплат (29)
              public const string INSUR_MFC_OPL = "INSUR.MFC.OPL";
                
              // Загрузка данных от МФЦ (30)
              public const string INSUR_MFC_IMPORT_IMPORT = "INSUR.MFC.IMPORT.IMPORT";
                
              // Учет начисления на ФСП (32)
              public const string INSUR_MFC_IMPORT_ACTIONS_LINK = "INSUR.MFC.IMPORT.ACTIONS.LINK";
                
              // Выгрузка в Excel (36)
              public const string INSUR_MFC_IMPORT_EXPORT = "INSUR.MFC.IMPORT.EXPORT";
                
              // Выгрузка в Excel (38)
              public const string INSUR_MFC_NACH_EXPORT = "INSUR.MFC.NACH.EXPORT";
                
              // Операции (39)
              public const string INSUR_MFC_NACH_ACTIONS = "INSUR.MFC.NACH.ACTIONS";
                
              // Учет начисления на ФСП (40)
              public const string INSUR_MFC_NACH_ACTIONS_LINK = "INSUR.MFC.NACH.ACTIONS.LINK";
                
              // Отвязка начисления от ФСП (41)
              public const string INSUR_MFC_NACH_ACTIONS_FREE = "INSUR.MFC.NACH.ACTIONS.FREE";
                
              // Формирование контрольного начисления на ФСП (42)
              public const string INSUR_MFC_NACH_ACTIONS_CHECK = "INSUR.MFC.NACH.ACTIONS.CHECK";
                
              // Выгрузка в Excel (44)
              public const string INSUR_MFC_ZACH_EXPORT = "INSUR.MFC.ZACH.EXPORT";
                
              // Операции (45)
              public const string INSUR_MFC_ZACH_ACTIONS = "INSUR.MFC.ZACH.ACTIONS";
                
              // Учет зачисления на ФСП (46)
              public const string INSUR_MFC_ZACH_ACTIONS_LINK = "INSUR.MFC.ZACH.ACTIONS.LINK";
                
              // Отвязка зачисления от ФСП (47)
              public const string INSUR_MFC_ZACH_ACTIONS_FREE = "INSUR.MFC.ZACH.ACTIONS.FREE";
                
              // Выгрузка в Excel (49)
              public const string INSUR_MFC_OPL_EXPORT = "INSUR.MFC.OPL.EXPORT";
                
              // Пакеты данных (50)
              public const string INSUR_SK_PACKAGE = "INSUR.SK.PACKAGE";
                
              // Полисы (51)
              public const string INSUR_SK_POLICY = "INSUR.SK.POLICY";
                
              // Свидетельства (52)
              public const string INSUR_SK_SVD = "INSUR.SK.SVD";
                
              // Страховые выплаты по ЖП (53)
              public const string INSUR_SK_VIPLAT = "INSUR.SK.VIPLAT";
                
              // Отказы в выплатах по ЖП (54)
              public const string INSUR_SK_OTKAZ = "INSUR.SK.OTKAZ";
                
              // Загрузка данных (56)
              public const string INSUR_SK_PACKAGE_LOAD = "INSUR.SK.PACKAGE.LOAD";
                
              // Удаление (57)
              public const string INSUR_SK_PACKAGE_DELETE = "INSUR.SK.PACKAGE.DELETE";
                
              // Обработка (58)
              public const string INSUR_SK_PACKAGE_PROCESS = "INSUR.SK.PACKAGE.PROCESS";
                
              // Выгрузка в Excel (59)
              public const string INSUR_SK_PACKAGE_EXPORT = "INSUR.SK.PACKAGE.EXPORT";
                
              // Выгрузка в Excel (61)
              public const string INSUR_SK_POLICY_EXPORT = "INSUR.SK.POLICY.EXPORT";
                
              // Связать с ФСП по ЕПД (62)
              public const string INSUR_SK_SVD_LINKFSP2EPD = "INSUR.SK.SVD.LINKFSP2EPD";
                
              // Выгрузка в Excel (63)
              public const string INSUR_SK_SVD_EXPORT = "INSUR.SK.SVD.EXPORT";
                
              // Выгрузка в Excel (65)
              public const string INSUR_SK_VIPLAT_EXPORT = "INSUR.SK.VIPLAT.EXPORT";
                
              // Выгрузка в Excel (67)
              public const string INSUR_SK_OTKAZ_EXPORT = "INSUR.SK.OTKAZ.EXPORT";
                
              // Дела по ЖП (68)
              public const string INSUR_DAMAGE_FLAT = "INSUR.DAMAGE.FLAT";
                
              // Дела по общему имуществу (69)
              public const string INSUR_DAMAGE_COMMPROP = "INSUR.DAMAGE.COMMPROP";
                
              // Редактирование чужих дел (72)
              public const string INSUR_DAMAGE_WRITE_OTHER = "INSUR.DAMAGE.WRITE.OTHER";
                
              // Формирование акта (73)
              public const string INSUR_DAMAGE_FLAT_ACTCREATE = "INSUR.DAMAGE.FLAT.ACTCREATE";
                
              // Удаление чужих дел (74)
              public const string INSUR_DAMAGE_DELETE_OTHER = "INSUR.DAMAGE.DELETE.OTHER";
                
              // Формирование реестра оплат (75)
              public const string INSUR_DAMAGE_FLAT_ROPLCREATE = "INSUR.DAMAGE.FLAT.ROPLCREATE";
                
              // Выгрузка в Excel (76)
              public const string INSUR_DAMAGE_FLAT_EXPORT = "INSUR.DAMAGE.FLAT.EXPORT";
                
              // Удаление своих дел (79)
              public const string INSUR_DAMAGE_DELETE = "INSUR.DAMAGE.DELETE";
                
              // Редактирование своих дел (81)
              public const string INSUR_DAMAGE_WRITE = "INSUR.DAMAGE.WRITE";
                
              // Выгрузка в Excel (83)
              public const string INSUR_DAMAGE_COMMPROP_EXPORT = "INSUR.DAMAGE.COMMPROP.EXPORT";
                
              // Расчеты (84)
              public const string INSUR_COMMPROP_CALC = "INSUR.COMMPROP.CALC";
                
              // Договора (85)
              public const string INSUR_COMMPROP_RIGHT = "INSUR.COMMPROP.RIGHT";
                
              // Платежи (86)
              public const string INSUR_COMMPROP_PAY = "INSUR.COMMPROP.PAY";
                
              // Страховые выплаты (88)
              public const string INSUR_COMMPROP_VIPLAT = "INSUR.COMMPROP.VIPLAT";
                
              // Сведения об отказах в выплатах (89)
              public const string INSUR_COMMPROP_OTKAZ = "INSUR.COMMPROP.OTKAZ";
                
              // Связывать с договором (91)
              public const string INSUR_COMMPROP_CALC_LINK2CONTRACT = "INSUR.COMMPROP.CALC.LINK2CONTRACT";
                
              // Удаление (92)
              public const string INSUR_COMMPROP_CALC_DELETE = "INSUR.COMMPROP.CALC.DELETE";
                
              // Выгрузка в Excel (93)
              public const string INSUR_COMMPROP_CALC_EXPORT = "INSUR.COMMPROP.CALC.EXPORT";
                
              // Просмотр всех данных (94)
              public const string INSUR_COMMPROP_CALC_ALLDATA = "INSUR.COMMPROP.CALC.ALLDATA";
                
              // Установка статуса Проверено (95)
              public const string INSUR_DAMAGE_FLAT_CHECK = "INSUR.DAMAGE.FLAT.CHECK";
                
              // Установка статуса Согласовано (96)
              public const string INSUR_DAMAGE_FLAT_AGREED = "INSUR.DAMAGE.FLAT.AGREED";
                
              // Редактирование (99)
              public const string INSUR_COMMPROP_CALC_WRITE = "INSUR.COMMPROP.CALC.WRITE";
                
              // Связывать с договором (100)
              public const string INSUR_COMMPROP_PAY_LINK2CONTRACT = "INSUR.COMMPROP.PAY.LINK2CONTRACT";
                
              // Выгрузка в Excel (101)
              public const string INSUR_COMMPROP_PAY_EXPORT = "INSUR.COMMPROP.PAY.EXPORT";
                
              // Редактирование (104)
              public const string INSUR_COMMPROP_RIGHT_WRITE = "INSUR.COMMPROP.RIGHT.WRITE";
                
              // Выгрузка в Excel (105)
              public const string INSUR_COMMPROP_VIPLAT_EXPORT = "INSUR.COMMPROP.VIPLAT.EXPORT";
                
              // Удаление (106)
              public const string INSUR_SAVEDREPORT_DELETE = "INSUR.SAVEDREPORT.DELETE";
                
              // Выгрузка в Excel (107)
              public const string INSUR_COMMPROP_OTKAZ_EXPORT = "INSUR.COMMPROP.OTKAZ.EXPORT";
                
              // Формировать счета (108)
              public const string INSUR_COMMPROP_RIGHT_INVOICECREATE = "INSUR.COMMPROP.RIGHT.INVOICECREATE";
                
              // Выгрузка в Excel (109)
              public const string INSUR_COMMPROP_RIGHT_EXPORT = "INSUR.COMMPROP.RIGHT.EXPORT";
                
              // Реестр счетов (110)
              public const string INSUR_ROPL = "INSUR.ROPL";
                
              // Доли города по ущербу ЖП (111)
              public const string INSUR_ROPL_DAMAGE_FLAT = "INSUR.ROPL.DAMAGE.FLAT";
                
              // Доли города по ущербу ОИ (112)
              public const string INSUR_ROPL_DAMAGE_COMMPROP = "INSUR.ROPL.DAMAGE.COMMPROP";
                
              // Возвраты части премий по ОИ (113)
              public const string INSUR_ROPL_COMEBACK = "INSUR.ROPL.COMEBACK";
                
              // Учет начисления на ФСП (114)
              public const string INSUR_COMMPROP_PAY_LINK = "INSUR.COMMPROP.PAY.LINK";
                
              // Редактирование (115)
              public const string INSUR_ROPL_DAMAGE_FLAT_WRITE = "INSUR.ROPL.DAMAGE.FLAT.WRITE";
                
              // Отвязка начисления от ФСП (116)
              public const string INSUR_COMMPROP_PAY_FREE = "INSUR.COMMPROP.PAY.FREE";
                
              // Сервисные операции (117)
              public const string INSUR_SERVICEOPERATIONS = "INSUR.SERVICEOPERATIONS";
                
              // Удаление (118)
              public const string INSUR_ROPL_DAMAGE_FLAT_DELETE = "INSUR.ROPL.DAMAGE.FLAT.DELETE";
                
              // Изменение статуса (119)
              public const string INSUR_ROPL_DAMAGE_FLAT_CHSTATUS = "INSUR.ROPL.DAMAGE.FLAT.CHSTATUS";
                
              // Выгрузка в Excel (120)
              public const string INSUR_ROPL_DAMAGE_FLAT_EXPORT = "INSUR.ROPL.DAMAGE.FLAT.EXPORT";
                
              // Согласовывать счета (121)
              public const string INSUR_ROPL_DAMAGE_FLAT_AGREEDLIST = "INSUR.ROPL.DAMAGE.FLAT.AGREEDLIST";
                
              // Формировать реестр выплат (122)
              public const string INSUR_ROPL_DAMAGE_FLAT_REPORT = "INSUR.ROPL.DAMAGE.FLAT.REPORT";
                
              // Редактирование (123)
              public const string INSUR_ROPL_DAMAGE_COMMPROP_WRITE = "INSUR.ROPL.DAMAGE.COMMPROP.WRITE";
                
              // Изменение статуса (124)
              public const string INSUR_ROPL_DAMAGE_COMMPROP_CHSTATUS = "INSUR.ROPL.DAMAGE.COMMPROP.CHSTATUS";
                
              // Выгрузка в Excel (125)
              public const string INSUR_ROPL_DAMAGE_COMMPROP_EXPORT = "INSUR.ROPL.DAMAGE.COMMPROP.EXPORT";
                
              // Реестр документов (126)
              public const string INSUR_SAVEDREPORT = "INSUR.SAVEDREPORT";
                
              // Удаление (127)
              public const string INSUR_ROPL_DAMAGE_COMMPROP_DELETE = "INSUR.ROPL.DAMAGE.COMMPROP.DELETE";
                
              // Редактировать (128)
              public const string INSUR_ROPL_COMEBACK_WRITE = "INSUR.ROPL.COMEBACK.WRITE";
                
              // Изменение статуса (129)
              public const string INSUR_ROPL_COMEBACK_CHSTATUS = "INSUR.ROPL.COMEBACK.CHSTATUS";
                
              // Выгрузка в Excel (130)
              public const string INSUR_ROPL_COMEBACK_EXPORT = "INSUR.ROPL.COMEBACK.EXPORT";
                
              // Удаление (131)
              public const string INSUR_ROPL_COMEBACK_DELETE = "INSUR.ROPL.COMEBACK.DELETE";
                
              // Сформировать отчет (132)
              public const string INSUR_ROPL_COMEBACK_REPORT = "INSUR.ROPL.COMEBACK.REPORT";
                
              // Панель управления (133)
              public const string ADMIN = "ADMIN";
                
              // Пользователи (134)
              public const string CORE_SRD_USERS = "CORE.SRD.USERS";
                
              // Роли (135)
              public const string CORE_SRD_ROLES = "CORE.SRD.ROLES";
                
              // Подразделения (136)
              public const string CORE_SRD_DEPARTMENTS = "CORE.SRD.DEPARTMENTS";
                
              // Справочники (137)
              public const string ADMIN_REFERENCES = "ADMIN.REFERENCES";
                
              // СРД (138)
              public const string CORE_SRD = "CORE.SRD";
                
              // Реестры (139)
              public const string ADMIN_REGISTERS = "ADMIN.REGISTERS";
                
              // Контроль работы системы (140)
              public const string ADMIN_SYSTEM_LOGS = "ADMIN.SYSTEM.LOGS";
                
              // Фоновые процессы (141)
              public const string ADMIN_LONG_PROCESS = "ADMIN.LONG_PROCESS";
                
              // Создание / Изменение (142)
              public const string CORE_SRD_USERS_CHANGE = "CORE.SRD.USERS.CHANGE";
                
              // Создание / Изменение (143)
              public const string CORE_SRD_ROLES_CHANGE = "CORE.SRD.ROLES.CHANGE";
                
              // Создание / Изменение (144)
              public const string CORE_SRD_DEPARTMENTS_CHANGE = "CORE.SRD.DEPARTMENTS.CHANGE";
                
              // Установка связи со строкой банка (145)
              public const string INSUR_MFC_ZACH_ACTIONS_BANKLINK = "INSUR.MFC.ZACH.ACTIONS.BANKLINK";
                
              // Сервис (146)
              public const string INSUR_OBJ_BUILDING_SERVICE = "INSUR.OBJ.BUILDING.SERVICE";
                
              // Перерасчет признака Подлежит страхованию (147)
              public const string INSUR_OBJ_BUILDING_SERVICE_FLAG_INSUR_RECALCULATE = "INSUR.OBJ.BUILDING.SERVICE.FLAG_INSUR_RECALCULATE";
                
              // Копирование признака Подлежит страхованию (148)
              public const string INSUR_OBJ_BUILDING_SERVICE_FLAG_INSUR_COPY = "INSUR.OBJ.BUILDING.SERVICE.FLAG_INSUR_COPY";
                
              // Связать МКД (149)
              public const string INSUR_OBJ_BUILDING_LINK = "INSUR.OBJ.BUILDING.LINK";
                
              // Общесистемные функции (150)
              public const string GENERAL_FUNCTIONS = "GENERAL.FUNCTIONS";
                
              // Просмотр сообщений (151)
              public const string CORE_MESSAGES = "CORE.MESSAGES";
                
              // Создание сообщений (152)
              public const string CORE_MESSAGES_WRITE = "CORE.MESSAGES.WRITE";
                
              // Расширенный редактор запросов (153)
              public const string CORE_REGISTER_LAYOUTADVANCED = "CORE.REGISTER.LAYOUTADVANCED";
                
              // Менеджер выгрузок (154)
              public const string CORE_REGISTER_EXPORT = "CORE.REGISTER.EXPORT";
                
              // ФСП все типы (155)
              public const string INSUR_FSP_ALL = "INSUR.FSP.ALL";
                
              // Связать с помещением (156)
              public const string INSUR_FSP_ALL_ATTACHTOFLAT = "INSUR.FSP.ALL.ATTACHTOFLAT";
                
              // Просмотр адресатов (157)
              public const string CORE_MESSAGES_RECIPIENTS = "CORE.MESSAGES.RECIPIENTS";
                
              // Переключение сервисного режима работы (158)
              public const string ADMIN_SERVICE_MODE = "ADMIN.SERVICE_MODE";
                
              // Выгрузка в Excel (159)
              public const string INSUR_FSP_ALL_EXPORT = "INSUR.FSP.ALL.EXPORT";
                
              // Таблица изменений UNOM (160)
              public const string INSUR_FSP_VIEW_UNOM_UPDATE_HISTORY = "INSUR.FSP.VIEW_UNOM_UPDATE_HISTORY";
                
              // Перевыпуск заключения (161)
              public const string INSUR_DAMAGE_FLAT_FLAG_ZAKLUCH_REISSUE = "INSUR.DAMAGE.FLAT.FLAG_ZAKLUCH_REISSUE";
                
              // Настройка резервного копирования (162)
              public const string CORE_BACKUP_CONFIGURATION = "CORE.BACKUP.CONFIGURATION";
                
              // Основные статистические показатели (163)
              public const string AP_COMMON = "AP.COMMON";
                
              // Журналы загрузки (164)
              public const string ADMIN_IMPORT = "ADMIN.IMPORT";
                
              // Вводящие (165)
              public const string INSUR_HIERARCHICALGRIDANALYSISDAMAGE_INJECTORS = "INSUR.HIERARCHICALGRIDANALYSISDAMAGE.INJECTORS";
                
              // Проверяющие (166)
              public const string INSUR_HIERARCHICALGRIDANALYSISDAMAGE_INSPECTOR = "INSUR.HIERARCHICALGRIDANALYSISDAMAGE.INSPECTOR";
                
              // Согласующие (167)
              public const string INSUR_HIERARCHICALGRIDANALYSISDAMAGE_MATCHING = "INSUR.HIERARCHICALGRIDANALYSISDAMAGE.MATCHING";
                
              // Прочие (168)
              public const string INSUR_OTHER = "INSUR.OTHER";
                
              // Доступ к WEB - интерфейсу (1584)
              public const string CORE_WEB_INTERFACE = "CORE.WEB.INTERFACE";
                
    }
}