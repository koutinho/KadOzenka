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