namespace ObjectModel.SRD
      {
          /// <summary>
          /// Словарь названий функций, поддерживаемых платформой RDP
          /// </summary>
          public class SRDCoreFunctions
          {
              // Общесистемные функции (1)
              public const string GENERAL = "GENERAL";
                
              // Подразделения (28)
              public const string CORE_SRD_DEPARTMENTS = "CORE.SRD.DEPARTMENTS";
                
              // Диагностика (29)
              public const string CORE_CONF_CORE_DIAGNOSTICS = "CORE.CONF.CORE_DIAGNOSTICS";
                
              // Фоновые процессы. Журнал запуска службы (30)
              public const string ADMIN_LONG_PROCESS = "ADMIN.LONG_PROCESS";
                
              // Администрирование (31)
              public const string ADMIN = "ADMIN";
                
              // Пользователи (32)
              public const string CORE_SRD_USERS = "CORE.SRD.USERS";
                
              // Просмотр сообщений (33)
              public const string CORE_MESSAGES = "CORE.MESSAGES";
                
              // Смена своего пароля пользователем (35)
              public const string CORE_SRD_USERS_CHANGE_PASSWORD_OWN = "CORE.SRD.USERS.CHANGE_PASSWORD_OWN";
                
              // Создание / изменение (36)
              public const string CORE_SRD_USERS_CHANGE = "CORE.SRD.USERS.CHANGE";
                
              // Блокировка / Разблокировка (37)
              public const string CORE_SRD_USERS_BLOCKING = "CORE.SRD.USERS.BLOCKING";
                
              // Настройка подсистемы безопасности (39)
              public const string CORE_SRD_SETTINGS_EDIT = "CORE.SRD.SETTINGS.EDIT";
                
              // Администрирование импорт (40)
              public const string ADMIN_IMPORT = "ADMIN.IMPORT";
                
              // Рабочие столы (50)
              public const string DASHBOARD = "DASHBOARD";
                
              // Панель управления (51)
              public const string DASHBOARD_PANEL = "DASHBOARD.PANEL";
                
              // Режим редактирования (52)
              public const string DASHBOARD_EDIT = "DASHBOARD.EDIT";
                
              // Настройка доступа к панели (55)
              public const string DASHBOARD_EDIT_SET_SRD = "DASHBOARD.EDIT.SET_SRD";
                
              // СРД (350)
              public const string CORE_SRD = "CORE.SRD";
                
              // Аудит (351)
              public const string CORE_SRD_AUDIT = "CORE.SRD.AUDIT";
                
              // Действия (352)
              public const string CORE_SRD_AUDIT_ACTIONS = "CORE.SRD.AUDIT.ACTIONS";
                
              // Изменения в реестрах (353)
              public const string CORE_SRD_AUDIT_REGISTER_CHANGES = "CORE.SRD.AUDIT.REGISTER_CHANGES";
                
              // Сессии (354)
              public const string CORE_SRD_AUDIT_SESSIONS = "CORE.SRD.AUDIT.SESSIONS";
                
              // Смена пароля (355)
              public const string CORE_SRD_USERS_CHANGE_PASSWORD = "CORE.SRD.USERS.CHANGE_PASSWORD";
                
              // Создание / изменение (357)
              public const string CORE_SRD_DEPARTMENTS_CHANGE = "CORE.SRD.DEPARTMENTS.CHANGE";
                
              // Удаление (358)
              public const string CORE_SRD_DEPARTMENTS_DELETE = "CORE.SRD.DEPARTMENTS.DELETE";
                
              // Удаление (359)
              public const string CORE_SRD_USERS_DELETE = "CORE.SRD.USERS.DELETE";
                
              // Роли (360)
              public const string CORE_SRD_ROLES = "CORE.SRD.ROLES";
                
              // Создание / Изменение (361)
              public const string CORE_SRD_ROLES_CHANGE = "CORE.SRD.ROLES.CHANGE";
                
              // Удаление (362)
              public const string CORE_SRD_ROLES_DELETE = "CORE.SRD.ROLES.DELETE";
                
              // Копирование (363)
              public const string CORE_SRD_ROLES_COPY = "CORE.SRD.ROLES.COPY";
                
              // Копирование (364)
              public const string CORE_SRD_USERS_COPY = "CORE.SRD.USERS.COPY";
                
              // Объекты аналоги (500)
              public const string MARKET = "MARKET";
                
              // Объекты недвижимости (501)
              public const string GBU = "GBU";
                
              // Рассчетная подсистема (502)
              public const string KO = "KO";
                
              // Судебные решения (503)
              public const string SUD = "SUD";
                
              // Комиссии (504)
              public const string COMMISSION = "COMMISSION";
                
              // Декларации (505)
              public const string DECLARATIONS = "DECLARATIONS";
                
              // Отчеты (506)
              public const string SUD_OTCHET = "SUD.OTCHET";
                
              // Создание/Редактирование (507)
              public const string SUD_OTCHET_EDIT = "SUD.OTCHET.EDIT";
                
              // Заключения (508)
              public const string SUD_ZAK = "SUD.ZAK";
                
              // Создание/Редактирование (509)
              public const string SUD_ZAK_EDIT = "SUD.ZAK.EDIT";
                
              // Решения (510)
              public const string SUD_RESH = "SUD.RESH";
                
              // Создание/Редактирование (511)
              public const string SUD_RESH_EDIT = "SUD.RESH.EDIT";
                
              // Объекты (512)
              public const string SUD_OBJECTS = "SUD.OBJECTS";
                
              // Создание/Редактирование (513)
              public const string SUD_OBJECTS_EDIT = "SUD.OBJECTS.EDIT";
                
              // Утверждение (514)
              public const string SUD_OBJECTS_APPROVE = "SUD.OBJECTS.APPROVE";
                
              // Утверждение отчета из карточки объекта (515)
              public const string SUD_OBJECTS_OTCHET_APPROVE = "SUD.OBJECTS.OTCHET.APPROVE";
                
              // Утверждение заключения из карточки объекта (516)
              public const string SUD_OBJECTS_ZAK_APPROVE = "SUD.OBJECTS.ZAK.APPROVE";
                
              // Утверждение решения из карточки объекта (517)
              public const string SUD_OBJECTS_RESH_APPROVE = "SUD.OBJECTS.RESH.APPROVE";
                
              // Утверждение отчетов (518)
              public const string SUD_OTCHET_APPROVE = "SUD.OTCHET.APPROVE";
                
              // Утверждение заключений (519)
              public const string SUD_ZAK_APPROVE = "SUD.ZAK.APPROVE";
                
              // Утверждение решений (520)
              public const string SUD_RESH_APPROVE = "SUD.RESH.APPROVE";
                
              // Загрузка данных (521)
              public const string SUD_IMPORT = "SUD.IMPORT";
                
              // Декларации (523)
              public const string DECLARATIONS_DECLARATION = "DECLARATIONS.DECLARATION";
                
              // Создание (524)
              public const string DECLARATIONS_DECLARATION_CREATE = "DECLARATIONS.DECLARATION.CREATE";
                
              // Редактирование (525)
              public const string DECLARATIONS_DECLARATION_EDIT = "DECLARATIONS.DECLARATION.EDIT";
                
              // Декларации Редактирование: блок «Подача декларации» (526)
              public const string DECLARATIONS_DECLARATION_EDIT_SUPPLY_BLOCK = "DECLARATIONS.DECLARATION.EDIT.SUPPLY_BLOCK";
                
              // Декларации Редактирование: блок «Обработка декларации» (527)
              public const string DECLARATIONS_DECLARATION_EDIT_PROCESSING_BLOCK = "DECLARATIONS.DECLARATION.EDIT.PROCESSING_BLOCK";
                
              // Декларации Редактирование: изменение статуса декларации (528)
              public const string DECLARATIONS_DECLARATION_EDIT_STATUS = "DECLARATIONS.DECLARATION.EDIT.STATUS";
                
              // Декларации Редактирование: вкладка «Формальная проверка/уведомление» (529)
              public const string DECLARATIONS_DECLARATION_EDIT_FORMAL_CHECKING = "DECLARATIONS.DECLARATION.EDIT.FORMAL_CHECKING";
                
              // Декларации Редактирование: внесение информации о характеристиках (530)
              public const string DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS = "DECLARATIONS.DECLARATION.EDIT.CHARACTERISTICS";
                
              // Декларации Редактирование: формирование уведомлений (531)
              public const string DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS = "DECLARATIONS.DECLARATION.EDIT.NOTIFICATIONS";
                
              // Декларации Редактирование: формирование уведомления о принятии (532)
              public const string DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_APPROVE_NOTIFICATION = "DECLARATIONS.DECLARATION.EDIT.NOTIFICATIONS.APPROVE_NOTIFICATION";
                
              // Декларации Редактирование: формирование остальных уведомлений (533)
              public const string DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_OTHER_NOTIFICATIONS = "DECLARATIONS.DECLARATION.EDIT.NOTIFICATIONS.OTHER_NOTIFICATIONS";
                
              // Декларации Редактирование: прикрепление образов (534)
              public const string DECLARATIONS_DECLARATION_EDIT_ATTACHMENTS = "DECLARATIONS.DECLARATION.EDIT.ATTACHMENTS";
                
              // Книги (535)
              public const string DECLARATIONS_BOOK = "DECLARATIONS.BOOK";
                
              // Книги Создание (536)
              public const string DECLARATIONS_BOOK_CREATE = "DECLARATIONS.BOOK.CREATE";
                
              // Книги Редактирование (537)
              public const string DECLARATIONS_BOOK_EDIT = "DECLARATIONS.BOOK.EDIT";
                
              // Субъекты (538)
              public const string DECLARATIONS_SUBJECT = "DECLARATIONS.SUBJECT";
                
              // Субъекты Создание (539)
              public const string DECLARATIONS_SUBJECT_CREATE = "DECLARATIONS.SUBJECT.CREATE";
                
              // Субъекты Редактирование (540)
              public const string DECLARATIONS_SUBJECT_EDIT = "DECLARATIONS.SUBJECT.EDIT";
                
              // Подписанты (541)
              public const string DECLARATIONS_SIGNATORY = "DECLARATIONS.SIGNATORY";
                
              // Подписанты Создание (542)
              public const string DECLARATIONS_SIGNATORY_CREATE = "DECLARATIONS.SIGNATORY.CREATE";
                
              // Подписанты Редактирование (543)
              public const string DECLARATIONS_SIGNATORY_EDIT = "DECLARATIONS.SIGNATORY.EDIT";
                
              // Просмотр истории изменения всех полей (544)
              public const string SUD_OBJECTS_HISTORY = "SUD.OBJECTS.HISTORY";
                
              // Просмотр статистики (545)
              public const string SUD_OBJECTS_STATISTICS = "SUD.OBJECTS.STATISTICS";
                
              // Просмотр истории изменения всех полей (546)
              public const string SUD_OTCHET_HISTORY = "SUD.OTCHET.HISTORY";
                
              // Просмотр истории изменения всех полей (547)
              public const string SUD_ZAK_HISTORY = "SUD.ZAK.HISTORY";
                
              // Просмотр истории изменения всех полей (548)
              public const string SUD_RESH_HISTORY = "SUD.RESH.HISTORY";
                
              // Выгрузка судебных решений для ГБУ (549)
              public const string SUD_EXPORT_GBU = "SUD.EXPORT.GBU";
                
              // Полная выгрузка в Excel (550)
              public const string SUD_EXPORT_ALL = "SUD.EXPORT.ALL";
                
              // Положительные судебные решения (551)
              public const string SUD_OBJECTS_STATISTICS_TRUE = "SUD.OBJECTS.STATISTICS.TRUE";
                
              // Сводная статистика (552)
              public const string SUD_OBJECTS_STATISTICS_SUMMARY = "SUD.OBJECTS.STATISTICS.SUMMARY";
                
              // Статистика по объектам недвижимости (553)
              public const string SUD_OBJECTS_STATISTICS_OBJECT = "SUD.OBJECTS.STATISTICS.OBJECT";
                
              // Выгрузка в Excel (554)
              public const string SUD_EXPORT_EXCEL = "SUD.EXPORT.EXCEL";
                
              // Выгрузка данных (555)
              public const string SUD_EXPORT = "SUD.EXPORT";
                
              // Выгрузка отчета по доп. параметрам (556)
              public const string SUD_EXPORT_DOP_PARAM = "SUD.EXPORT.DOP_PARAM";
                
              // Выгрузка в XML (557)
              public const string SUD_EXPORT_XML = "SUD.EXPORT.XML";
                
              // Удаление объектов (558)
              public const string SUD_OBJECTS_REMOVE = "SUD.OBJECTS.REMOVE";
                
              // Просмотр удаленных объектов (559)
              public const string SUD_OBJECTS_REMOVED_VIEW = "SUD.OBJECTS.REMOVED.VIEW";
                
              // Экспресс оценка (560)
              public const string EXPRESSSCORE = "EXPRESSSCORE";
                
              // Выполнение расчетов (561)
              public const string EXPRESSSCORE_CALCULATE = "EXPRESSSCORE.CALCULATE";
                
              // История расчетов (562)
              public const string EXPRESSSCORE_HISTORY = "EXPRESSSCORE.HISTORY";
                
              // Конструктор расчетов (563)
              public const string EXPRESSSCORE_CONSTRUCTOR = "EXPRESSSCORE.CONSTRUCTOR";
                
              // Конструктор расчетов. Редактирование (564)
              public const string EXPRESSSCORE_CONSTRUCTOR_EDIT = "EXPRESSSCORE.CONSTRUCTOR.EDIT";
                
              // Карта (570)
              public const string MARKET_MAP = "MARKET.MAP";
                
              // Все данные (571)
              public const string MARKET_ALL_DATA = "MARKET.ALL_DATA";
                
              // Загрузка данных по списку (572)
              public const string MARKET_DATA_IMPORT = "MARKET.DATA_IMPORT";
                
              // Выгрузка данных по списку (573)
              public const string MARKET_DATA_EXPORT = "MARKET.DATA_EXPORT";
                
              // Выгрузка скриншотов (574)
              public const string MARKET_UNLOAD_SCREENSHOTS = "MARKET.UNLOAD_SCREENSHOTS";
                
              // Выгрузка в Excel (575)
              public const string MARKET_EXPORT_TO_EXCEL = "MARKET.EXPORT_TO_EXCEL";
                
              // Присвоение округов, районов, зон (576)
              public const string MARKET_ACTIVATE_PROCESS = "MARKET.ACTIVATE_PROCESS";
                
              // Присвоение координат (577)
              public const string MARKET_ACTIVATE_COORDINATES = "MARKET.ACTIVATE_COORDINATES";
                
              // Моделирование корреляции (578)
              public const string MARKET_CORRELATION = "MARKET.CORRELATION";
                
              // Корректировки (580)
              public const string MARKET_CORRECTION = "MARKET.CORRECTION";
                
              // Редактирование (581)
              public const string MARKET_CORRECTION_EDIT = "MARKET.CORRECTION.EDIT";
                
              // Отчет (582)
              public const string MARKET_CORRECTION_REPORT = "MARKET.CORRECTION.REPORT";
                
              // Реестр (583)
              public const string GBU_OBJECTS = "GBU.OBJECTS";
                
              // Справочники ЦОД (584)
              public const string GBU_COD = "GBU.COD";
                
              // Характеристики объектов (585)
              public const string GBU_OBJ_PARAM = "GBU.OBJ_PARAM";
                
              // Нормализация данных (586)
              public const string GBU_OBJECTS_GROUPING_OBJECT = "GBU.OBJECTS.GROUPING_OBJECT";
                
              // Выборка из справочника ЦОД (587)
              public const string GBU_OBJECTS_UNLOADING_FROM_DICT = "GBU.OBJECTS.UNLOADING_FROM_DICT";
                
              // Наследование (588)
              public const string GBU_OBJECTS_INHERITANCE = "GBU.OBJECTS.INHERITANCE";
                
              // Присвоить оценочную группу (589)
              public const string GBU_OBJECTS_SET_ESTIMATED_GROUP = "GBU.OBJECTS.SET_ESTIMATED_GROUP";
                
              // Загрузка данных по списку (590)
              public const string GBU_OBJECTS_DATA_IMPORT = "GBU.OBJECTS.DATA_IMPORT";
                
              // Выгрузка данных по списку (591)
              public const string GBU_OBJECTS_DATA_EXPORT = "GBU.OBJECTS.DATA_EXPORT";
                
              // Выгрузка в Excel (592)
              public const string GBU_OBJECTS_EXPORT_TO_EXCEL = "GBU.OBJECTS.EXPORT_TO_EXCEL";
                
              // Гармонизация (593)
              public const string GBU_OBJECTS_HARMONIZATION = "GBU.OBJECTS.HARMONIZATION";
                
              // Гармонизация по классификатору ЦОД (594)
              public const string GBU_OBJECTS_HARMONIZATION_COD = "GBU.OBJECTS.HARMONIZATION_COD";
                
              // Добавить (595)
              public const string GBU_COD_JOB_ADD = "GBU.COD.JOB_ADD";
                
              // Удалить (596)
              public const string GBU_COD_JOB_DELETE = "GBU.COD.JOB_DELETE";
                
              // Импорт классификатора ЦОД (597)
              public const string GBU_COD_IMPORT = "GBU.COD.IMPORT";
                
              // Все данные (598)
              public const string GBU_OBJ_PARAM_ALL_DATA = "GBU.OBJ_PARAM.ALL_DATA";
                
              // Добавить источник (599)
              public const string GBU_OBJ_PARAM_ADD_SOURCE = "GBU.OBJ_PARAM.ADD_SOURCE";
                
              // Редактировать источник (600)
              public const string GBU_OBJ_PARAM_EDIT_SOURCE = "GBU.OBJ_PARAM.EDIT_SOURCE";
                
              // Все данные (601)
              public const string GBU_OBJECTS_ALL_DATA = "GBU.OBJECTS.ALL_DATA";
                
              // Справочники (602)
              public const string EXPRESSSCORE_REFERENCES = "EXPRESSSCORE.REFERENCES";
                
              // Все данные (603)
              public const string EXPRESSSCORE_REFERENCES_ALL_DATA = "EXPRESSSCORE.REFERENCES.ALL_DATA";
                
              // Добавить/Редактировать (605)
              public const string EXPRESSSCORE_REFERENCES_EDIT = "EXPRESSSCORE.REFERENCES.EDIT";
                
              // Удалить (606)
              public const string EXPRESSSCORE_REFERENCES_DELETE = "EXPRESSSCORE.REFERENCES.DELETE";
                
              // Импорт справочника (607)
              public const string EXPRESSSCORE_REFERENCES_IMPORT = "EXPRESSSCORE.REFERENCES.IMPORT";
                
              // Добавить/Редактировать (608)
              public const string COMMISSION_EDIT_COMMISSION = "COMMISSION.EDIT_COMMISSION";
                
              // Выгрузка в Excel (609)
              public const string COMMISSION_EXPORT_TO_EXCEL = "COMMISSION.EXPORT_TO_EXCEL";
                
              // Все данные (610)
              public const string COMMISSION_ALL_DATA = "COMMISSION.ALL_DATA";
                
              // Загрузка данных (611)
              public const string COMMISSION_LOAD_DOCUMENT = "COMMISSION.LOAD_DOCUMENT";
                
              // Поддержка принятия управленческих решений (612)
              public const string DECISION_SUPPORT = "DECISION_SUPPORT";
                
              // Декларации Редактирование: добавление характеристик в раздел Формальная проверка / Уведомление (613)
              public const string DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS_INCLUDE_IN_FORMAL_CHECKING = "DECLARATIONS.DECLARATION.EDIT.CHARACTERISTICS.INCLUDE_IN_FORMAL_CHECKING";
                
              // Построение тематических карт (614)
              public const string DECISION_SUPPORT_THEME_MAPS = "DECISION_SUPPORT.THEME_MAPS";
                
              // Статистическая информация (615)
              public const string DECISION_SUPPORT_STATISTICS = "DECISION_SUPPORT.STATISTICS";
                
              // Экономический анализ (616)
              public const string DECISION_SUPPORT_ANALYSIS = "DECISION_SUPPORT.ANALYSIS";
                
              // Формирование отчетных форм (617)
              public const string DECISION_SUPPORT_REPORT_FORMS = "DECISION_SUPPORT.REPORT_FORMS";
                
              // Задания на оценку (618)
              public const string KO_TASKS = "KO.TASKS";
                
              // Единицы оценки (619)
              public const string KO_OBJECTS = "KO.OBJECTS";
                
              // Создать задание на оценку (620)
              public const string KO_IMPORT_GKN = "KO.IMPORT_GKN";
                
              // Произвести расчет кадастровой стоимости (621)
              public const string KO_CALCULATE_CADASTRAL_PRICE = "KO.CALCULATE_CADASTRAL_PRICE";
                
              // Выгрузка результатов оценки (622)
              public const string KO_UNLOAD_RESULTS = "KO.UNLOAD_RESULTS";
                
              // Справочники (623)
              public const string KO_DICT = "KO.DICT";
                
              // Журнал отправки итогов расчета КО в ИС РЕОН (624)
              public const string KO_JOURNAL = "KO.JOURNAL";
                
              // Все данные (625)
              public const string KO_TASKS_ALL_DATA = "KO.TASKS.ALL_DATA";
                
              // Перенос атрибутов (626)
              public const string KO_TASKS_TRANSFER_ATTRIBUTES = "KO.TASKS.TRANSFER_ATTRIBUTES";
                
              // Импорт данных ГКН (628)
              public const string KO_TASKS_IMPORT_GKN = "KO.TASKS.IMPORT_GKN";
                
              // Выгрузка в Excel (629)
              public const string KO_TASKS_EXPORT_TO_EXCEL = "KO.TASKS.EXPORT_TO_EXCEL";
                
              // Загрузка графических факторов из ИС РЕОН (630)
              public const string KO_TASKS_DOWNLOAD_GRAPHIC_FACTORS_FROM_REON = "KO.TASKS.DOWNLOAD_GRAPHIC_FACTORS_FROM_REON";
                
              // Все данные (631)
              public const string KO_OBJECTS_ALL_DATA = "KO.OBJECTS.ALL_DATA";
                
              // Все действия (632)
              public const string KO_OBJECTS_CORE_AUDIT_COMMON = "KO.OBJECTS.CORE_AUDIT_COMMON";
                
              // Выгрузка в Excel (633)
              public const string KO_OBJECTS_EXPORT_TO_EXCEL = "KO.OBJECTS.EXPORT_TO_EXCEL";
                
              // Туры (634)
              public const string KO_DICT_TOURS = "KO.DICT.TOURS";
                
              // Модели (635)
              public const string KO_DICT_MODELS = "KO.DICT.MODELS";
                
              // Ценообразующие факторы (636)
              public const string KO_DICT_FACTORS = "KO.DICT.FACTORS";
                
              // Туры оценки (637)
              public const string KO_DICT_TOURS_ESTIMATES = "KO.DICT.TOURS.ESTIMATES";
                
              // Группы (638)
              public const string KO_DICT_TOURS_GROUPS = "KO.DICT.TOURS.GROUPS";
                
              // Значения меток (639)
              public const string KO_DICT_TOURS_MARK_CATALOG = "KO.DICT.TOURS.MARK_CATALOG";
                
              // Импорт группы из Excel (640)
              public const string KO_DICT_TOURS_IMPORT_GROUP_DATA_FROM_EXCEL = "KO.DICT.TOURS.IMPORT_GROUP_DATA_FROM_EXCEL";
                
              // Настройки атрибутов туров (641)
              public const string KO_DICT_TOURS_ATTRIBUTE_SETTINGS = "KO.DICT.TOURS.ATTRIBUTE_SETTINGS";
                
              // Модель (642)
              public const string KO_DICT_MODELS_ADD_MODEL = "KO.DICT.MODELS.ADD_MODEL";
                
              // Просмотр объектов (643)
              public const string KO_DICT_MODELS_MODEL_OBJECTS = "KO.DICT.MODELS.MODEL_OBJECTS";
                
              // Все данные (644)
              public const string KO_DICT_FACTORS_ALL_DATA = "KO.DICT.FACTORS.ALL_DATA";
                
              // Туры оценки (645)
              public const string KO_DICT_FACTORS_TOUR_ESTIMATES = "KO.DICT.FACTORS.TOUR_ESTIMATES";
                
              // Загрузка данных по списку (646)
              public const string GBU_COD_DATA_IMPORT = "GBU.COD.DATA_IMPORT";
                
              // Документы (647)
              public const string DOCUMENTS = "DOCUMENTS";
                
              // Редактирование документа (648)
              public const string DOCUMENTS_EDIT = "DOCUMENTS.EDIT";
                
              // Удаление документа (649)
              public const string DOCUMENTS_DELETE = "DOCUMENTS.DELETE";
                
              // Просмотр справочников моделирования (651)
              public const string KO_DICT_MODELS_DICTIONARIES = "KO.DICT.MODELS.DICTIONARIES";
                
              // Работа со справочниками моделирования (652)
              public const string KO_DICT_MODELS_DICTIONARIES_MODIFICATION = "KO.DICT.MODELS.DICTIONARIES.MODIFICATION";
                
              // Просмотр значений справочников моделирования (653)
              public const string KO_DICT_MODELS_DICTIONARIES_VALUES = "KO.DICT.MODELS.DICTIONARIES.VALUES";
                
              // Работа со значениями справочников моделирования (654)
              public const string KO_DICT_MODELS_DICTIONARIES_VALUES_MODIFICATION = "KO.DICT.MODELS.DICTIONARIES.VALUES.MODIFICATION";
                
              // Редактирование статуса Решение вступило в законную силу (655)
              public const string SUD_OBJECTS_EDIT_DECISION_ENTERED_INTO_FORCE = "SUD.OBJECTS.EDIT.DECISION_ENTERED_INTO_FORCE";
                
              // Доступ к выгрузкам из раскладок (660)
              public const string CORE_REGISTER_EXPORT = "CORE.REGISTER.EXPORT";
                
              // Настройка справочников (661)
              public const string ADMIN_REFERENCES = "ADMIN.REFERENCES";
                
              // Настройка реестров (662)
              public const string ADMIN_REGISTERS = "ADMIN.REGISTERS";
                
              // Переключение сервисного режима работы (663)
              public const string ADMIN_SERVICE_MODE = "ADMIN.SERVICE_MODE";
                
              // Контроль работы системы (664)
              public const string ADMIN_SYSTEM_LOGS = "ADMIN.SYSTEM.LOGS";
                
              // Календарь рабочих дней (665)
              public const string CORE_HOLIDAYS_VIEW = "CORE.HOLIDAYS.VIEW";
                
              // Доступ к веб-интерфейсу (12344)
              public const string CORE_WEB_INTERFACE = "CORE.WEB.INTERFACE";
                
              // Смена дизайна (12345)
              public const string CORE_WEB_INTERFACE_CHANGE = "CORE.WEB.INTERFACE.CHANGE";
                
              // Доступ к расширенному редактору раскладок (1300126)
              public const string CORE_REGISTER_LAYOUTADVANCED = "CORE.REGISTER.LAYOUTADVANCED";
                
              // Выгрузка в Excel (1300131)
              public const string DASHBOARD_EXPORTTOEXCEL = "DASHBOARD.EXPORTTOEXCEL";
                
              // Просмотр адресатов (1300138)
              public const string CORE_MESSAGES_RECIPIENTS = "CORE.MESSAGES.RECIPIENTS";
                
              // Создание сообщений (1300139)
              public const string CORE_MESSAGES_WRITE = "CORE.MESSAGES.WRITE";
                
              // Просмотр всех сообщений (1300140)
              public const string CORE_MESSAGES_ALL = "CORE.MESSAGES.ALL";
                
              // Отчетность (1300141)
              public const string REPORTS = "REPORTS";
                
              // Cохранение отчетов (1300142)
              public const string CORE_SAVED_REPORTS_SAVE = "CORE.SAVED.REPORTS.SAVE";
                
              // Реестр утвержденных моделей (1300143)
              public const string KO_DICT_MODELS_APPROVED = "KO.DICT.MODELS.APPROVED";
                
    }
}