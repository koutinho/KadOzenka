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
              public const string CORE_SRD_USERS_CHANGEPASSWORD = "CORE.SRD.USERS.CHANGEPASSWORD";
                
              // Создание / изменение (36)
              public const string CORE_SRD_USERS_CHANGE = "CORE.SRD.USERS.CHANGE";
                
              // Блокировка / Разблокировка (37)
              public const string CORE_SRD_USERS_BLOCKING = "CORE.SRD.USERS.BLOCKING";
                
              // Настройка подсистемы безопасности (39)
              public const string CORE_SRD_SETTINGS_EDIT = "CORE.SRD.SETTINGS.EDIT";
                
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
                
              // Объекты аналоги (500)
              public const string MARKET = "MARKET";
                
              // Объекты оценки (501)
              public const string GBU = "GBU";
                
              // Задания на оценку (502)
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
                
              // Выгрузка данных (522)
              public const string SUD_EXPORT = "SUD.EXPORT";
                
              // Добавление декларации (523)
              public const string DECLARATIONS_CREATE = "DECLARATIONS.CREATE";
                
              // Декларации Редактирование (524)
              public const string DECLARATIONS_EDIT = "DECLARATIONS.EDIT";
                
              // Доступ к веб-интерфейсу (12344)
              public const string CORE_WEB_INTERFACE = "CORE.WEB.INTERFACE";
                
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
                
    }
}