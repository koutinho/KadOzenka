
-- I. Загрузка реестров
-- Обработка реестра: 920, Core.Register.List (Поименнованные списки объектов реестров)-- Список создаваемых таблиц:
-- CORE_LIST
--<DO>--
-- Создание таблицы CORE_LIST
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LIST')) then
	 execute 'create table CORE_LIST
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LIST
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LIST')) then
    execute 'alter table CORE_LIST add constraint REG_920_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 921, Core.Register.ListObject (Идентификаторы объектов, входящих в список)-- Список создаваемых таблиц:
-- CORE_LIST_OBJECT
--<DO>--
-- Создание таблицы CORE_LIST_OBJECT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LIST_OBJECT')) then
	 execute 'create table CORE_LIST_OBJECT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LIST_OBJECT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LIST_OBJECT')) then
    execute 'alter table CORE_LIST_OBJECT add constraint REG_921_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 924, Core.Register.LayoutColumnType (Тип колонки раскладки)-- Список создаваемых таблиц:
-- CORE_LAYOUT_COLUMN_TYPE
--<DO>--
-- Создание таблицы CORE_LAYOUT_COLUMN_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_COLUMN_TYPE')) then
	 execute 'create table CORE_LAYOUT_COLUMN_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LAYOUT_COLUMN_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LAYOUT_COLUMN_TYPE')) then
    execute 'alter table CORE_LAYOUT_COLUMN_TYPE add constraint REG_924_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 925, Core.SRD.UserSettingsRegisterView (Пользовательские настройки представления реестра)-- Список создаваемых таблиц:
-- CORE_SRD_USERSETTINGSREGVIEW
--<DO>--
-- Создание таблицы CORE_SRD_USERSETTINGSREGVIEW
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGSREGVIEW')) then
	 execute 'create table CORE_SRD_USERSETTINGSREGVIEW
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_USERSETTINGSREGVIEW
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USERSETTINGSREGVIEW')) then
    execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add constraint REG_925_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 930, Core.Register.Register (Список реестров)-- Список создаваемых таблиц:
-- CORE_REGISTER
--<DO>--
-- Создание таблицы CORE_REGISTER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER')) then
	 execute 'create table CORE_REGISTER
						(
						  REGISTERID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REGISTER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER')) then
    execute 'alter table CORE_REGISTER add constraint REG_930_QUANT_PK primary key (REGISTERID)';
  end if;
end $$;
-- Обработка реестра: 931, Core.Register.Attribute (Список показателей реестра)-- Список создаваемых таблиц:
-- CORE_REGISTER_ATTRIBUTE
--<DO>--
-- Создание таблицы CORE_REGISTER_ATTRIBUTE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_ATTRIBUTE')) then
	 execute 'create table CORE_REGISTER_ATTRIBUTE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REGISTER_ATTRIBUTE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER_ATTRIBUTE')) then
    execute 'alter table CORE_REGISTER_ATTRIBUTE add constraint REG_931_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 932, Core.Register.Relation (Список связей между реестрами)-- Список создаваемых таблиц:
-- CORE_REGISTER_RELATION
--<DO>--
-- Создание таблицы CORE_REGISTER_RELATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_RELATION')) then
	 execute 'create table CORE_REGISTER_RELATION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REGISTER_RELATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER_RELATION')) then
    execute 'alter table CORE_REGISTER_RELATION add constraint REG_932_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 933, Core.Register.Layout (Раскладки)-- Список создаваемых таблиц:
-- CORE_LAYOUT
--<DO>--
-- Создание таблицы CORE_LAYOUT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT')) then
	 execute 'create table CORE_LAYOUT
						(
						  LAYOUTID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LAYOUT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LAYOUT')) then
    execute 'alter table CORE_LAYOUT add constraint REG_933_QUANT_PK primary key (LAYOUTID)';
  end if;
end $$;
-- Обработка реестра: 935, Core.Register.LayoutDetail (Детализация раскладок)-- Список создаваемых таблиц:
-- CORE_LAYOUT_DETAILS
--<DO>--
-- Создание таблицы CORE_LAYOUT_DETAILS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_DETAILS')) then
	 execute 'create table CORE_LAYOUT_DETAILS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LAYOUT_DETAILS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LAYOUT_DETAILS')) then
    execute 'alter table CORE_LAYOUT_DETAILS add constraint REG_935_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 936, Core.Register.Qry (Фильтры реестров)-- Список создаваемых таблиц:
-- CORE_QRY
--<DO>--
-- Создание таблицы CORE_QRY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY')) then
	 execute 'create table CORE_QRY
						(
						  QRYID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_QRY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_QRY')) then
    execute 'alter table CORE_QRY add constraint REG_936_QUANT_PK primary key (QRYID)';
  end if;
end $$;
-- Обработка реестра: 937, Core.Register.QryFilter (Условия фильтров)-- Список создаваемых таблиц:
-- CORE_QRY_FILTER
--<DO>--
-- Создание таблицы CORE_QRY_FILTER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY_FILTER')) then
	 execute 'create table CORE_QRY_FILTER
						(
						  QRYFILTERID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_QRY_FILTER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_QRY_FILTER')) then
    execute 'alter table CORE_QRY_FILTER add constraint REG_937_QUANT_PK primary key (QRYFILTERID)';
  end if;
end $$;
-- Обработка реестра: 938, Core.Register.QryOperation (Операции фильтров)-- Список создаваемых таблиц:
-- CORE_QRY_OPERATION
--<DO>--
-- Создание таблицы CORE_QRY_OPERATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY_OPERATION')) then
	 execute 'create table CORE_QRY_OPERATION
						(
						  QRYOPERATIONID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_QRY_OPERATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_QRY_OPERATION')) then
    execute 'alter table CORE_QRY_OPERATION add constraint REG_938_QUANT_PK primary key (QRYOPERATIONID)';
  end if;
end $$;
-- Обработка реестра: 939, Core.Register.Lock (Блокировка объекта реестра)-- Список создаваемых таблиц:
-- CORE_REGISTER_LOCK
--<DO>--
-- Создание таблицы CORE_REGISTER_LOCK
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_LOCK')) then
	 execute 'create table CORE_REGISTER_LOCK
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REGISTER_LOCK
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER_LOCK')) then
    execute 'alter table CORE_REGISTER_LOCK add constraint REG_939_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 940, Core.SRD.Audit (Аудит действий пользователей с функциями модулей (подсистем) системы)-- Список создаваемых таблиц:
-- CORE_SRD_AUDIT
--<DO>--
-- Создание таблицы CORE_SRD_AUDIT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_AUDIT')) then
	 execute 'create table CORE_SRD_AUDIT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_AUDIT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_AUDIT')) then
    execute 'alter table CORE_SRD_AUDIT add constraint REG_940_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 941, Core.SRD.Department (Подразделение в организации пользователя системы)-- Список создаваемых таблиц:
-- CORE_SRD_DEPARTMENT
--<DO>--
-- Создание таблицы CORE_SRD_DEPARTMENT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_DEPARTMENT')) then
	 execute 'create table CORE_SRD_DEPARTMENT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_DEPARTMENT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_DEPARTMENT')) then
    execute 'alter table CORE_SRD_DEPARTMENT add constraint REG_941_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 942, Core.SRD.Function (Функции модулей (подсистем) системы)-- Список создаваемых таблиц:
-- CORE_SRD_FUNCTION
--<DO>--
-- Создание таблицы CORE_SRD_FUNCTION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_FUNCTION')) then
	 execute 'create table CORE_SRD_FUNCTION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_FUNCTION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_FUNCTION')) then
    execute 'alter table CORE_SRD_FUNCTION add constraint REG_942_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 945, Core.SRD.Role (Роли в системе)-- Список создаваемых таблиц:
-- CORE_SRD_ROLE
--<DO>--
-- Создание таблицы CORE_SRD_ROLE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE')) then
	 execute 'create table CORE_SRD_ROLE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_ROLE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE')) then
    execute 'alter table CORE_SRD_ROLE add constraint REG_945_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 946, Core.SRD.RoleFunction (Функции роли (бывшая LOCROLE_LOCFUNCTION))-- Список создаваемых таблиц:
-- CORE_SRD_ROLE_FUNCTION
--<DO>--
-- Создание таблицы CORE_SRD_ROLE_FUNCTION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_FUNCTION')) then
	 execute 'create table CORE_SRD_ROLE_FUNCTION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_ROLE_FUNCTION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE_FUNCTION')) then
    execute 'alter table CORE_SRD_ROLE_FUNCTION add constraint REG_946_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 947, Core.SRD.RoleRegister (Права доступа роли к реестру)-- Список создаваемых таблиц:
-- CORE_SRD_ROLE_REGISTER
--<DO>--
-- Создание таблицы CORE_SRD_ROLE_REGISTER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_REGISTER')) then
	 execute 'create table CORE_SRD_ROLE_REGISTER
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_ROLE_REGISTER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE_REGISTER')) then
    execute 'alter table CORE_SRD_ROLE_REGISTER add constraint REG_947_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 948, Core.SRD.RoleAttr (Права доступа роли к атрибутам реестра)-- Список создаваемых таблиц:
-- CORE_SRD_ROLE_ATTR
--<DO>--
-- Создание таблицы CORE_SRD_ROLE_ATTR
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_ATTR')) then
	 execute 'create table CORE_SRD_ROLE_ATTR
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_ROLE_ATTR
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE_ATTR')) then
    execute 'alter table CORE_SRD_ROLE_ATTR add constraint REG_948_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 949, Core.SRD.Session (Параметры сессии пользователя)-- Список создаваемых таблиц:
-- CORE_SRD_SESSION
--<DO>--
-- Создание таблицы CORE_SRD_SESSION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_SESSION')) then
	 execute 'create table CORE_SRD_SESSION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_SESSION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_SESSION')) then
    execute 'alter table CORE_SRD_SESSION add constraint REG_949_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 950, Core.SRD.User (Пользователи системы)-- Список создаваемых таблиц:
-- CORE_SRD_USER
--<DO>--
-- Создание таблицы CORE_SRD_USER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USER')) then
	 execute 'create table CORE_SRD_USER
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_USER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USER')) then
    execute 'alter table CORE_SRD_USER add constraint REG_950_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 951, Core.SRD.UserSettings (Пользовательские настройки)-- Список создаваемых таблиц:
-- CORE_SRD_USERSETTINGS
--<DO>--
-- Создание таблицы CORE_SRD_USERSETTINGS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGS')) then
	 execute 'create table CORE_SRD_USERSETTINGS
						(
						  USERID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_USERSETTINGS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USERSETTINGS')) then
    execute 'alter table CORE_SRD_USERSETTINGS add constraint REG_951_QUANT_PK primary key (USERID)';
  end if;
end $$;
-- Обработка реестра: 952, Core.SRD.UserRole (Роли пользователя)-- Список создаваемых таблиц:
-- CORE_SRD_USER_ROLE
--<DO>--
-- Создание таблицы CORE_SRD_USER_ROLE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USER_ROLE')) then
	 execute 'create table CORE_SRD_USER_ROLE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_USER_ROLE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USER_ROLE')) then
    execute 'alter table CORE_SRD_USER_ROLE add constraint REG_952_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 954, Core.SRD.UserSettingsLayout (Пользовательские настройки раскладкок)-- Список создаваемых таблиц:
-- CORE_SRD_USERSETTINGSLAYOUT
--<DO>--
-- Создание таблицы CORE_SRD_USERSETTINGSLAYOUT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGSLAYOUT')) then
	 execute 'create table CORE_SRD_USERSETTINGSLAYOUT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_USERSETTINGSLAYOUT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USERSETTINGSLAYOUT')) then
    execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add constraint REG_954_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 955, Core.SRD.RoleFilter (Разграничение прав доступа по данным реестров)-- Список создаваемых таблиц:
-- CORE_SRD_ROLE_FILTER
--<DO>--
-- Создание таблицы CORE_SRD_ROLE_FILTER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_FILTER')) then
	 execute 'create table CORE_SRD_ROLE_FILTER
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_ROLE_FILTER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE_FILTER')) then
    execute 'alter table CORE_SRD_ROLE_FILTER add constraint REG_955_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 956, Core.Register.LayoutExport (Выгрузка данных по раскладке)-- Список создаваемых таблиц:
-- CORE_LAYOUT_EXPORT
--<DO>--
-- Создание таблицы CORE_LAYOUT_EXPORT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_EXPORT')) then
	 execute 'create table CORE_LAYOUT_EXPORT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LAYOUT_EXPORT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LAYOUT_EXPORT')) then
    execute 'alter table CORE_LAYOUT_EXPORT add constraint REG_956_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 957, Core.SRD.RegisterCategory (Категории доступа к данным реестров)-- Список создаваемых таблиц:
-- CORE_SRD_REGISTER_CATEGORY
--<DO>--
-- Создание таблицы CORE_SRD_REGISTER_CATEGORY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_REGISTER_CATEGORY')) then
	 execute 'create table CORE_SRD_REGISTER_CATEGORY
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_REGISTER_CATEGORY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_REGISTER_CATEGORY')) then
    execute 'alter table CORE_SRD_REGISTER_CATEGORY add constraint REG_957_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 958, Core.SRD.FunctionRegisterCategory (Доступ функции к категориям доступа реестров)-- Список создаваемых таблиц:
-- CORE_SRD_FUNCTION_REG_CAT
--<DO>--
-- Создание таблицы CORE_SRD_FUNCTION_REG_CAT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_FUNCTION_REG_CAT')) then
	 execute 'create table CORE_SRD_FUNCTION_REG_CAT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_SRD_FUNCTION_REG_CAT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_FUNCTION_REG_CAT')) then
    execute 'alter table CORE_SRD_FUNCTION_REG_CAT add constraint REG_958_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 960, Core.TD.Template (Шаблон (тип) технологического документа)-- Список создаваемых таблиц:
-- CORE_TD_TEMPLATE
--<DO>--
-- Создание таблицы CORE_TD_TEMPLATE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE')) then
	 execute 'create table CORE_TD_TEMPLATE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_TEMPLATE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TEMPLATE')) then
    execute 'alter table CORE_TD_TEMPLATE add constraint REG_960_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 961, Core.TD.TemplateVersion (Версия шаблона технологического документа)-- Список создаваемых таблиц:
-- CORE_TD_TEMPLATE_VERSION
--<DO>--
-- Создание таблицы CORE_TD_TEMPLATE_VERSION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE_VERSION')) then
	 execute 'create table CORE_TD_TEMPLATE_VERSION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_TEMPLATE_VERSION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TEMPLATE_VERSION')) then
    execute 'alter table CORE_TD_TEMPLATE_VERSION add constraint REG_961_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 962, Core.TD.Status (Типы статусов экземпляра технологического документа)-- Список создаваемых таблиц:
-- CORE_TD_STATUS
--<DO>--
-- Создание таблицы CORE_TD_STATUS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_STATUS')) then
	 execute 'create table CORE_TD_STATUS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_STATUS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_STATUS')) then
    execute 'alter table CORE_TD_STATUS add constraint REG_962_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 963, Core.TD.Instance (Экземпляры технологическох документов)-- Список создаваемых таблиц:
-- CORE_TD_INSTANCE
--<DO>--
-- Создание таблицы CORE_TD_INSTANCE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_INSTANCE')) then
	 execute 'create table CORE_TD_INSTANCE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_INSTANCE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_INSTANCE')) then
    execute 'alter table CORE_TD_INSTANCE add constraint REG_963_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 964, Core.TD.Changeset (Набор изменений в реестрах)-- Список создаваемых таблиц:
-- CORE_TD_CHANGESET
--<DO>--
-- Создание таблицы CORE_TD_CHANGESET
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_CHANGESET')) then
	 execute 'create table CORE_TD_CHANGESET
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_CHANGESET
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_CHANGESET')) then
    execute 'alter table CORE_TD_CHANGESET add constraint REG_964_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 965, Core.TD.Change (Изменение в реестре)-- Список создаваемых таблиц:
-- CORE_TD_CHANGE
--<DO>--
-- Создание таблицы CORE_TD_CHANGE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_CHANGE')) then
	 execute 'create table CORE_TD_CHANGE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_CHANGE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_CHANGE')) then
    execute 'alter table CORE_TD_CHANGE add constraint REG_965_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 966, Core.TD.AuditAction (Типы аудируемых действий с экземпляром технологического документа)-- Список создаваемых таблиц:
-- CORE_TD_AUDIT_ACTION
--<DO>--
-- Создание таблицы CORE_TD_AUDIT_ACTION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_AUDIT_ACTION')) then
	 execute 'create table CORE_TD_AUDIT_ACTION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_AUDIT_ACTION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_AUDIT_ACTION')) then
    execute 'alter table CORE_TD_AUDIT_ACTION add constraint REG_966_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 967, Core.TD.Audit (Аудит действий с экземпляром технологического документа)-- Список создаваемых таблиц:
-- CORE_TD_AUDIT
--<DO>--
-- Создание таблицы CORE_TD_AUDIT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_AUDIT')) then
	 execute 'create table CORE_TD_AUDIT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_AUDIT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_AUDIT')) then
    execute 'alter table CORE_TD_AUDIT add constraint REG_967_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 968, Core.TD.Tree (Дерево шаблонов )-- Список создаваемых таблиц:
-- CORE_TD_TREE
--<DO>--
-- Создание таблицы CORE_TD_TREE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TREE')) then
	 execute 'create table CORE_TD_TREE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_TREE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TREE')) then
    execute 'alter table CORE_TD_TREE add constraint REG_968_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 969, Core.TD.Attachment (Электронные образы экземпляра документа)-- Список создаваемых таблиц:
-- CORE_TD_ATTACHMENTS
--<DO>--
-- Создание таблицы CORE_TD_ATTACHMENTS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_ATTACHMENTS')) then
	 execute 'create table CORE_TD_ATTACHMENTS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_ATTACHMENTS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_ATTACHMENTS')) then
    execute 'alter table CORE_TD_ATTACHMENTS add constraint REG_969_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 970, Core.TD.TP (Данные об уведомлении процесса, из которого создан экземпляр технологического документа)-- Список создаваемых таблиц:
-- CORE_TD_TP
--<DO>--
-- Создание таблицы CORE_TD_TP
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TP')) then
	 execute 'create table CORE_TD_TP
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_TP
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TP')) then
    execute 'alter table CORE_TD_TP add constraint REG_970_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 971, Core.TD.TemplateType (Тип шаблона технологического документа)-- Список создаваемых таблиц:
-- CORE_TD_TEMPLATE_TYPE
--<DO>--
-- Создание таблицы CORE_TD_TEMPLATE_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE_TYPE')) then
	 execute 'create table CORE_TD_TEMPLATE_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_TD_TEMPLATE_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TEMPLATE_TYPE')) then
    execute 'alter table CORE_TD_TEMPLATE_TYPE add constraint REG_971_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 975, Core.LongProcess.Queue (Очередь долгих процессов)-- Список создаваемых таблиц:
-- CORE_LONG_PROCESS_QUEUE
--<DO>--
-- Создание таблицы CORE_LONG_PROCESS_QUEUE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_QUEUE')) then
	 execute 'create table CORE_LONG_PROCESS_QUEUE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LONG_PROCESS_QUEUE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LONG_PROCESS_QUEUE')) then
    execute 'alter table CORE_LONG_PROCESS_QUEUE add constraint REG_975_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 976, Core.LongProcess.ProcessType (Типы долгих процессов)-- Список создаваемых таблиц:
-- CORE_LONG_PROCESS_TYPE
--<DO>--
-- Создание таблицы CORE_LONG_PROCESS_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_TYPE')) then
	 execute 'create table CORE_LONG_PROCESS_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LONG_PROCESS_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LONG_PROCESS_TYPE')) then
    execute 'alter table CORE_LONG_PROCESS_TYPE add constraint REG_976_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 320, Insur.Okrug (Справочник округов)-- Список создаваемых таблиц:
-- INSUR_OKRUG
--<DO>--
-- Создание таблицы INSUR_OKRUG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_OKRUG')) then
	 execute 'create table INSUR_OKRUG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_OKRUG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_OKRUG')) then
    execute 'alter table INSUR_OKRUG add constraint INSUR_OKRUG_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 977, Core.LongProcess.Log (Журнал менеджера долгих процессов)-- Список создаваемых таблиц:
-- CORE_LONG_PROCESS_LOG
--<DO>--
-- Создание таблицы CORE_LONG_PROCESS_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_LOG')) then
	 execute 'create table CORE_LONG_PROCESS_LOG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_LONG_PROCESS_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LONG_PROCESS_LOG')) then
    execute 'alter table CORE_LONG_PROCESS_LOG add constraint REG_977_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 978, Core.Shared.Configparam (Файлы конфигурации)-- Список создаваемых таблиц:
-- CORE_CONFIGPARAM
--<DO>--
-- Создание таблицы CORE_CONFIGPARAM
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_CONFIGPARAM')) then
	 execute 'create table CORE_CONFIGPARAM
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_CONFIGPARAM
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_CONFIGPARAM')) then
    execute 'alter table CORE_CONFIGPARAM add constraint REG_978_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 982, Core.Shared.Reference (Справочник)-- Список создаваемых таблиц:
-- CORE_REFERENCE
--<DO>--
-- Создание таблицы CORE_REFERENCE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE')) then
	 execute 'create table CORE_REFERENCE
						(
						  REFERENCEID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REFERENCE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REFERENCE')) then
    execute 'alter table CORE_REFERENCE add constraint REG_982_QUANT_PK primary key (REFERENCEID)';
  end if;
end $$;
-- Обработка реестра: 983, Core.Shared.ReferenceItem (Справочное значение)-- Список создаваемых таблиц:
-- CORE_REFERENCE_ITEM
--<DO>--
-- Создание таблицы CORE_REFERENCE_ITEM
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_ITEM')) then
	 execute 'create table CORE_REFERENCE_ITEM
						(
						  ITEMID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REFERENCE_ITEM
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REFERENCE_ITEM')) then
    execute 'alter table CORE_REFERENCE_ITEM add constraint REG_983_QUANT_PK primary key (ITEMID)';
  end if;
end $$;
-- Обработка реестра: 984, Core.Shared.ReferenceRelation (Связи справочников)-- Список создаваемых таблиц:
-- CORE_REFERENCE_RELATION
--<DO>--
-- Создание таблицы CORE_REFERENCE_RELATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_RELATION')) then
	 execute 'create table CORE_REFERENCE_RELATION
						(
						  RELID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REFERENCE_RELATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REFERENCE_RELATION')) then
    execute 'alter table CORE_REFERENCE_RELATION add constraint REG_984_QUANT_PK primary key (RELID)';
  end if;
end $$;
-- Обработка реестра: 985, Core.Shared.ReferenceTree (Связи справочных значений)-- Список создаваемых таблиц:
-- CORE_REFERENCE_TREE
--<DO>--
-- Создание таблицы CORE_REFERENCE_TREE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_TREE')) then
	 execute 'create table CORE_REFERENCE_TREE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REFERENCE_TREE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REFERENCE_TREE')) then
    execute 'alter table CORE_REFERENCE_TREE add constraint REG_985_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 986, Core.Shared.Attachment (Образ)-- Список создаваемых таблиц:
-- CORE_ATTACHMENT
--<DO>--
-- Создание таблицы CORE_ATTACHMENT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT')) then
	 execute 'create table CORE_ATTACHMENT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_ATTACHMENT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_ATTACHMENT')) then
    execute 'alter table CORE_ATTACHMENT add constraint REG_986_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 987, Core.Shared.AttachmentFile (Файлы образа)-- Список создаваемых таблиц:
-- CORE_ATTACHMENT_FILE
--<DO>--
-- Создание таблицы CORE_ATTACHMENT_FILE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT_FILE')) then
	 execute 'create table CORE_ATTACHMENT_FILE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_ATTACHMENT_FILE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_ATTACHMENT_FILE')) then
    execute 'alter table CORE_ATTACHMENT_FILE add constraint REG_987_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 988, Core.Shared.AttachmentObject (Связь образа и объекта реестра)-- Список создаваемых таблиц:
-- CORE_ATTACHMENT_OBJECT
--<DO>--
-- Создание таблицы CORE_ATTACHMENT_OBJECT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT_OBJECT')) then
	 execute 'create table CORE_ATTACHMENT_OBJECT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_ATTACHMENT_OBJECT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_ATTACHMENT_OBJECT')) then
    execute 'alter table CORE_ATTACHMENT_OBJECT add constraint REG_988_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 989, Core.Shared.ErrorLog (Журнал ошибок)-- Список создаваемых таблиц:
-- CORE_ERROR_LOG
--<DO>--
-- Создание таблицы CORE_ERROR_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_ERROR_LOG')) then
	 execute 'create table CORE_ERROR_LOG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_ERROR_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_ERROR_LOG')) then
    execute 'alter table CORE_ERROR_LOG add constraint REG_989_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 991, Core.Shared.RegisterState (Сохраненные состояния представлений )-- Список создаваемых таблиц:
-- CORE_REGISTER_STATE
--<DO>--
-- Создание таблицы CORE_REGISTER_STATE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_STATE')) then
	 execute 'create table CORE_REGISTER_STATE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REGISTER_STATE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER_STATE')) then
    execute 'alter table CORE_REGISTER_STATE add constraint REG_991_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 992, Core.Shared.Diagnostics (Журнал отладочных сообщений)-- Список создаваемых таблиц:
-- CORE_DIAGNOSTICS
--<DO>--
-- Создание таблицы CORE_DIAGNOSTICS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_DIAGNOSTICS')) then
	 execute 'create table CORE_DIAGNOSTICS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_DIAGNOSTICS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_DIAGNOSTICS')) then
    execute 'alter table CORE_DIAGNOSTICS add constraint REG_992_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 994, Core.Shared.RegNomRepository (Репозиторий регистрационных номеров)-- Список создаваемых таблиц:
-- CORE_REGNOM_REPOSITORY
--<DO>--
-- Создание таблицы CORE_REGNOM_REPOSITORY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGNOM_REPOSITORY')) then
	 execute 'create table CORE_REGNOM_REPOSITORY
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REGNOM_REPOSITORY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGNOM_REPOSITORY')) then
    execute 'alter table CORE_REGNOM_REPOSITORY add constraint  primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 995, Core.Shared.RegNomSequences (Последовательности регистрационных номеров)-- Список создаваемых таблиц:
-- CORE_REGNOM_SEQUENCES
--<DO>--
-- Создание таблицы CORE_REGNOM_SEQUENCES
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGNOM_SEQUENCES')) then
	 execute 'create table CORE_REGNOM_SEQUENCES
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_REGNOM_SEQUENCES
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGNOM_SEQUENCES')) then
    execute 'alter table CORE_REGNOM_SEQUENCES add constraint REG_995_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 996, Core.Shared.CacheUpdates (Временные метки обновления кэша)-- Список создаваемых таблиц:
-- CORE_CACHE_UPDATES
--<DO>--
-- Создание таблицы CORE_CACHE_UPDATES
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_CACHE_UPDATES')) then
	 execute 'create table CORE_CACHE_UPDATES
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_CACHE_UPDATES
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_CACHE_UPDATES')) then
    execute 'alter table CORE_CACHE_UPDATES add constraint REG_996_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 997, Core.Shared.UpdateStructure (Журнал обновления структуры БД)-- Список создаваемых таблиц:
-- CORE_UPDSTRU_LOG
--<DO>--
-- Создание таблицы CORE_UPDSTRU_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_UPDSTRU_LOG')) then
	 execute 'create table CORE_UPDSTRU_LOG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_UPDSTRU_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_UPDSTRU_LOG')) then
    execute 'alter table CORE_UPDSTRU_LOG add constraint REG_997_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 998, Core.Shared.Holiday (Выходные и праздничные дни)-- Список создаваемых таблиц:
-- CORE_HOLIDAYS
--<DO>--
-- Создание таблицы CORE_HOLIDAYS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_HOLIDAYS')) then
	 execute 'create table CORE_HOLIDAYS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы CORE_HOLIDAYS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_HOLIDAYS')) then
    execute 'alter table CORE_HOLIDAYS add constraint REG_998_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 321, Insur.District (Справочник районов)-- Список создаваемых таблиц:
-- INSUR_DISTRICT
--<DO>--
-- Создание таблицы INSUR_DISTRICT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DISTRICT')) then
	 execute 'create table INSUR_DISTRICT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_DISTRICT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DISTRICT')) then
    execute 'alter table INSUR_DISTRICT add constraint INSUR_DISTRICT_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 308, Insur.Fsp (Реестр Финансовых счетов плательщиков (ФСП))-- Список создаваемых таблиц:
-- INSUR_FSP_O
-- INSUR_FSP_Q
--<DO>--
-- Создание таблицы INSUR_FSP_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FSP_O')) then
	execute 'create table INSUR_FSP_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы INSUR_FSP_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FSP_O')) then
   execute 'alter table INSUR_FSP_O add constraint REG_308_OBJ_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы INSUR_FSP_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FSP_Q')) then
	 execute 'create table INSUR_FSP_Q
						(
						  ID      numeric(10) not null,
						  EMP_ID     numeric(10) not null,
						  ACTUAL  numeric(10) not null,
						  STATUS  numeric(10) not null,
						  
						  S_      DATE not null,
						  PO_     DATE not null
						)';
  end if;

-- Создание ограничений таблицы INSUR_FSP_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FSP_Q')) then
	execute 'alter table INSUR_FSP_Q add constraint REG_308_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_308_QUANT_FK_O')) then
	execute 'alter table INSUR_FSP_Q add constraint REG_308_QUANT_FK_O foreign key (EMP_ID) references INSUR_FSP_O (ID)';
  end if;


-- Создание индексов таблицы INSUR_FSP_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_308_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_308_QUANT_INX_EMP_ID on INSUR_FSP_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_308_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_308_QUANT_INX_S_PO_ ON INSUR_FSP_Q (S_, PO_)';
  end if;
end $$;
-- Обработка реестра: 302, Insur.LogFile (Реестр журналов обработки пакета файлов)-- Список создаваемых таблиц:
-- INSUR_LOG_FILE
--<DO>--
-- Создание таблицы INSUR_LOG_FILE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LOG_FILE')) then
	 execute 'create table INSUR_LOG_FILE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_LOG_FILE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LOG_FILE')) then
    execute 'alter table INSUR_LOG_FILE add constraint REG_302_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 850, Dashboards.Dashboard (Настройка дашбоарда)-- Список создаваемых таблиц:
-- DASHBOARDS_DASHBOARD
--<DO>--
-- Создание таблицы DASHBOARDS_DASHBOARD
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_DASHBOARD')) then
	 execute 'create table DASHBOARDS_DASHBOARD
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы DASHBOARDS_DASHBOARD
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('DASHBOARDS_DASHBOARD')) then
    execute 'alter table DASHBOARDS_DASHBOARD add constraint REG_850_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 851, Dashboards.Panel (Содержание дашбоарда)-- Список создаваемых таблиц:
-- DASHBOARDS_PANEL
--<DO>--
-- Создание таблицы DASHBOARDS_PANEL
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_PANEL')) then
	 execute 'create table DASHBOARDS_PANEL
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы DASHBOARDS_PANEL
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('DASHBOARDS_PANEL')) then
    execute 'alter table DASHBOARDS_PANEL add constraint REG_851_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 852, Dashboards.PanelTypes (Типы панелей)-- Список создаваемых таблиц:
-- DASHBOARDS_PANEL_TYPE
--<DO>--
-- Создание таблицы DASHBOARDS_PANEL_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_PANEL_TYPE')) then
	 execute 'create table DASHBOARDS_PANEL_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы DASHBOARDS_PANEL_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('DASHBOARDS_PANEL_TYPE')) then
    execute 'alter table DASHBOARDS_PANEL_TYPE add constraint REG_852_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 853, Dashboards.UserSettings (Пользовательские настройки панелей)-- Список создаваемых таблиц:
-- DASHBOARDS_USER_SETTINGS
--<DO>--
-- Создание таблицы DASHBOARDS_USER_SETTINGS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_USER_SETTINGS')) then
	 execute 'create table DASHBOARDS_USER_SETTINGS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы DASHBOARDS_USER_SETTINGS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('DASHBOARDS_USER_SETTINGS')) then
    execute 'alter table DASHBOARDS_USER_SETTINGS add constraint REG_853_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 600, SPD.RequestRegistration (Журнал учёта запросов СПД)-- Список создаваемых таблиц:
-- SPD_REQUEST_REGISTRATION
--<DO>--
-- Создание таблицы SPD_REQUEST_REGISTRATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('SPD_REQUEST_REGISTRATION')) then
	 execute 'create table SPD_REQUEST_REGISTRATION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы SPD_REQUEST_REGISTRATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('SPD_REQUEST_REGISTRATION')) then
    execute 'alter table SPD_REQUEST_REGISTRATION add constraint REG_600_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 601, SPD.CreateFullApplicationLog (Журнал запросов СПД CreateFullApplication)-- Список создаваемых таблиц:
-- SPD_CREATE_FULL_APP_LOG
--<DO>--
-- Создание таблицы SPD_CREATE_FULL_APP_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('SPD_CREATE_FULL_APP_LOG')) then
	 execute 'create table SPD_CREATE_FULL_APP_LOG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы SPD_CREATE_FULL_APP_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('SPD_CREATE_FULL_APP_LOG')) then
    execute 'alter table SPD_CREATE_FULL_APP_LOG add constraint REG_601_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 650, SPD.DocAgreement (Согласование документов)-- Список создаваемых таблиц:
-- SPD_DOC_AGREEMENT
--<DO>--
-- Создание таблицы SPD_DOC_AGREEMENT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('SPD_DOC_AGREEMENT')) then
	 execute 'create table SPD_DOC_AGREEMENT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы SPD_DOC_AGREEMENT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('SPD_DOC_AGREEMENT')) then
    execute 'alter table SPD_DOC_AGREEMENT add constraint REG_650_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 651, SPD.UserSRD2SPD (Соответствие пользователей СРД и СПД)-- Список создаваемых таблиц:
-- SPD_USERSRD2SPD
--<DO>--
-- Создание таблицы SPD_USERSRD2SPD
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('SPD_USERSRD2SPD')) then
	 execute 'create table SPD_USERSRD2SPD
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы SPD_USERSRD2SPD
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('SPD_USERSRD2SPD')) then
    execute 'alter table SPD_USERSRD2SPD add constraint REG_651_QUANT_PK primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 301, Insur.InputFile (Реестр загрузки файлов)-- Список создаваемых таблиц:
-- INSUR_INPUT_FILE
--<DO>--
-- Создание таблицы INSUR_INPUT_FILE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INPUT_FILE')) then
	 execute 'create table INSUR_INPUT_FILE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_INPUT_FILE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INPUT_FILE')) then
    execute 'alter table INSUR_INPUT_FILE add constraint REG_301_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 303, Insur.BankPlat (Реестр банковских файлов оплат)-- Список создаваемых таблиц:
-- INSUR_BANK_PLAT
--<DO>--
-- Создание таблицы INSUR_BANK_PLAT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BANK_PLAT')) then
	 execute 'create table INSUR_BANK_PLAT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_BANK_PLAT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BANK_PLAT')) then
    execute 'alter table INSUR_BANK_PLAT add constraint REG_303_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 304, Insur.SvodBank (Реестр cводные данные по файлу оплат)-- Список создаваемых таблиц:
-- INSUR_SVOD_BANK
--<DO>--
-- Создание таблицы INSUR_SVOD_BANK
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_SVOD_BANK')) then
	 execute 'create table INSUR_SVOD_BANK
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_SVOD_BANK
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_SVOD_BANK')) then
    execute 'alter table INSUR_SVOD_BANK add constraint REG_304_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 305, Insur.InputNach (Реестр начислений)-- Список создаваемых таблиц:
-- INSUR_INPUT_NACH
--<DO>--
-- Создание таблицы INSUR_INPUT_NACH
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INPUT_NACH')) then
	 execute 'create table INSUR_INPUT_NACH
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_INPUT_NACH
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INPUT_NACH')) then
    execute 'alter table INSUR_INPUT_NACH add constraint REG_305_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 306, Insur.InputPlat (Реестр зачислений (платежей))-- Список создаваемых таблиц:
-- INSUR_INPUT_PLAT
--<DO>--
-- Создание таблицы INSUR_INPUT_PLAT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INPUT_PLAT')) then
	 execute 'create table INSUR_INPUT_PLAT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_INPUT_PLAT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INPUT_PLAT')) then
    execute 'alter table INSUR_INPUT_PLAT add constraint REG_306_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 307, Insur.Balance (Реестр ведомости учета страховых взносов)-- Список создаваемых таблиц:
-- INSUR_BALANCE
--<DO>--
-- Создание таблицы INSUR_BALANCE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BALANCE')) then
	 execute 'create table INSUR_BALANCE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_BALANCE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BALANCE')) then
    execute 'alter table INSUR_BALANCE add constraint REG_307_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 309, Insur.PolicySvd (Реестр страховых полисов и свидетельств)-- Список создаваемых таблиц:
-- INSUR_POLICY_SVD
--<DO>--
-- Создание таблицы INSUR_POLICY_SVD
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_POLICY_SVD')) then
	 execute 'create table INSUR_POLICY_SVD
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_POLICY_SVD
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_POLICY_SVD')) then
    execute 'alter table INSUR_POLICY_SVD add constraint REG_309_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 310, Insur.AllProperty (Реестр договоров страхования общего имущества)-- Список создаваемых таблиц:
-- INSUR_ALL_PROPERTY
--<DO>--
-- Создание таблицы INSUR_ALL_PROPERTY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_ALL_PROPERTY')) then
	 execute 'create table INSUR_ALL_PROPERTY
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_ALL_PROPERTY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_ALL_PROPERTY')) then
    execute 'alter table INSUR_ALL_PROPERTY add constraint REG_310_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 311, Insur.DopAllProperty (Реестр доп. соглашений по договорам общего имущества)-- Список создаваемых таблиц:
-- INSUR_DOP_ALL_PROPERTY
--<DO>--
-- Создание таблицы INSUR_DOP_ALL_PROPERTY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DOP_ALL_PROPERTY')) then
	 execute 'create table INSUR_DOP_ALL_PROPERTY
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_DOP_ALL_PROPERTY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DOP_ALL_PROPERTY')) then
    execute 'alter table INSUR_DOP_ALL_PROPERTY add constraint REG_311_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 312, Insur.ParamCalculation (Реестр расчетов параметров объектов общего имущества)-- Список создаваемых таблиц:
-- INSUR_PARAM_CALCULATION
--<DO>--
-- Создание таблицы INSUR_PARAM_CALCULATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_PARAM_CALCULATION')) then
	 execute 'create table INSUR_PARAM_CALCULATION
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_PARAM_CALCULATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_PARAM_CALCULATION')) then
    execute 'alter table INSUR_PARAM_CALCULATION add constraint REG_312_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 313, Insur.Damage (Реестр дел по расчету  суммы ущерба)-- Список создаваемых таблиц:
-- INSUR_DAMAGE
--<DO>--
-- Создание таблицы INSUR_DAMAGE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DAMAGE')) then
	 execute 'create table INSUR_DAMAGE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_DAMAGE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DAMAGE')) then
    execute 'alter table INSUR_DAMAGE add constraint REG_313_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 314, Insur.PayTo (Реестр страховых выплат)-- Список создаваемых таблиц:
-- INSUR_PAY_TO
--<DO>--
-- Создание таблицы INSUR_PAY_TO
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_PAY_TO')) then
	 execute 'create table INSUR_PAY_TO
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_PAY_TO
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_PAY_TO')) then
    execute 'alter table INSUR_PAY_TO add constraint REG_314_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 315, Insur.NoPay (Реестр сведений об отказах в страховых выплатах)-- Список создаваемых таблиц:
-- INSUR_NO_PAY
--<DO>--
-- Создание таблицы INSUR_NO_PAY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_NO_PAY')) then
	 execute 'create table INSUR_NO_PAY
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_NO_PAY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_NO_PAY')) then
    execute 'alter table INSUR_NO_PAY add constraint REG_315_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 319, Insur.Address (Реестр адресов)-- Список создаваемых таблиц:
-- INSUR_ADDRESS
--<DO>--
-- Создание таблицы INSUR_ADDRESS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_ADDRESS')) then
	 execute 'create table INSUR_ADDRESS
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_ADDRESS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_ADDRESS')) then
    execute 'alter table INSUR_ADDRESS add constraint REG_319_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 322, Insur.FileStorage (Хранилище файлов)-- Список создаваемых таблиц:
-- INSUR_FILE_STORAGE
--<DO>--
-- Создание таблицы INSUR_FILE_STORAGE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FILE_STORAGE')) then
	 execute 'create table INSUR_FILE_STORAGE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_FILE_STORAGE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FILE_STORAGE')) then
    execute 'alter table INSUR_FILE_STORAGE add constraint CORE_FILE_STORAGE_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 323, Insur.DocBaseType (Справочник Виды документов-оснований)-- Список создаваемых таблиц:
-- INSUR_DOC_BASE_TYPE
--<DO>--
-- Создание таблицы INSUR_DOC_BASE_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DOC_BASE_TYPE')) then
	 execute 'create table INSUR_DOC_BASE_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_DOC_BASE_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DOC_BASE_TYPE')) then
    execute 'alter table INSUR_DOC_BASE_TYPE add constraint INSUR_DOC_BASE_TYPE_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 324, Insur.DamageAssessmentMethod (Методики оценки ущерба)-- Список создаваемых таблиц:
-- INSUR_DAMAGE_ASSESSMENT_METHOD
--<DO>--
-- Создание таблицы INSUR_DAMAGE_ASSESSMENT_METHOD
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DAMAGE_ASSESSMENT_METHOD')) then
	 execute 'create table INSUR_DAMAGE_ASSESSMENT_METHOD
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_DAMAGE_ASSESSMENT_METHOD
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DAMAGE_ASSESSMENT_METHOD')) then
    execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add constraint INSURE_DAMAGE_ASSESSMENT_METHOD_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 325, Insur.InputFilePackage (Реестр загружаемых пакетов)-- Список создаваемых таблиц:
-- INSUR_INPUT_FILE_PACKAGE
--<DO>--
-- Создание таблицы INSUR_INPUT_FILE_PACKAGE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INPUT_FILE_PACKAGE')) then
	 execute 'create table INSUR_INPUT_FILE_PACKAGE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_INPUT_FILE_PACKAGE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INPUT_FILE_PACKAGE')) then
    execute 'alter table INSUR_INPUT_FILE_PACKAGE add constraint INSUR_INPUT_FILE_PACKAGE_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 326, Insur.LinkBuildBti   (Реестр связи объекта страхования МКД с объектами БТИ)-- Список создаваемых таблиц:
-- INSUR_LINK_BUILD_BTI
--<DO>--
-- Создание таблицы INSUR_LINK_BUILD_BTI
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LINK_BUILD_BTI')) then
	 execute 'create table INSUR_LINK_BUILD_BTI
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_LINK_BUILD_BTI
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LINK_BUILD_BTI')) then
    execute 'alter table INSUR_LINK_BUILD_BTI add constraint REG_326_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 327, Insur.LinkFlatEgrn (Реестр связи между объектом страхования ЖП с помещениями в Росреестре)-- Список создаваемых таблиц:
-- INSUR_LINK_FLAT_EGRN
--<DO>--
-- Создание таблицы INSUR_LINK_FLAT_EGRN
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LINK_FLAT_EGRN')) then
	 execute 'create table INSUR_LINK_FLAT_EGRN
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_LINK_FLAT_EGRN
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LINK_FLAT_EGRN')) then
    execute 'alter table INSUR_LINK_FLAT_EGRN add constraint REG_327_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 253, Bti.Floor (Реестр связи между объектом страхования ЖП с помещениями в Росреестре)-- Список создаваемых таблиц:
-- BTI_FLOOR_O
-- BTI_FLOOR_Q
--<DO>--
-- Создание таблицы BTI_FLOOR_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_FLOOR_O')) then
	execute 'create table BTI_FLOOR_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы BTI_FLOOR_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_FLOOR_O')) then
   execute 'alter table BTI_FLOOR_O add constraint REG_253_OBJECT_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы BTI_FLOOR_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_FLOOR_Q')) then
	 execute 'create table BTI_FLOOR_Q
						(
						  ID      numeric(10) not null,
						  EMP_ID     numeric(10) not null,
						  ACTUAL  numeric(10) not null,
						  STATUS  numeric(10) not null,
						  
						  S_      DATE not null,
						  PO_     DATE not null
						)';
  end if;

-- Создание ограничений таблицы BTI_FLOOR_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_FLOOR_Q')) then
	execute 'alter table BTI_FLOOR_Q add constraint REG_253_QUANT_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_253_QUANT_FK_O')) then
	execute 'alter table BTI_FLOOR_Q add constraint REG_253_QUANT_FK_O foreign key (EMP_ID) references BTI_FLOOR_O (ID)';
  end if;


-- Создание индексов таблицы BTI_FLOOR_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_253_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_253_QUANT_INX_EMP_ID on BTI_FLOOR_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_253_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_253_QUANT_INX_S_PO_ ON BTI_FLOOR_Q (S_, PO_)';
  end if;
end $$;
-- Обработка реестра: 257, Bti.Rooms (Реестр комнат БТИ)-- Список создаваемых таблиц:
-- BTI_ROOMS
--<DO>--
-- Создание таблицы BTI_ROOMS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_ROOMS')) then
	 execute 'create table BTI_ROOMS
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы BTI_ROOMS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ROOMS')) then
    execute 'alter table BTI_ROOMS add constraint REG_257_QUANT_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 328, Insur.InsuranceOrganization (Справочник «Страховые организации»)-- Список создаваемых таблиц:
-- INSUR_INSURANCE_ORGANIZATION
--<DO>--
-- Создание таблицы INSUR_INSURANCE_ORGANIZATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INSURANCE_ORGANIZATION')) then
	 execute 'create table INSUR_INSURANCE_ORGANIZATION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_INSURANCE_ORGANIZATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INSURANCE_ORGANIZATION')) then
    execute 'alter table INSUR_INSURANCE_ORGANIZATION add constraint INSUR_INSURANCE_ORGANIZATION_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 254, Bti.Premase (Реестр помещений БТИ)-- Список создаваемых таблиц:
-- BTI_PREMASE
--<DO>--
-- Создание таблицы BTI_PREMASE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_PREMASE')) then
	 execute 'create table BTI_PREMASE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы BTI_PREMASE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_PREMASE')) then
    execute 'alter table BTI_PREMASE add constraint REG_254_QUANT_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 332, Insur.FlatStatus (Справочник "Статус квартиры /доли")-- Список создаваемых таблиц:
-- INSUR_FLAT_STATUS
--<DO>--
-- Создание таблицы INSUR_FLAT_STATUS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FLAT_STATUS')) then
	 execute 'create table INSUR_FLAT_STATUS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_FLAT_STATUS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FLAT_STATUS')) then
    execute 'alter table INSUR_FLAT_STATUS add constraint INSUR_FLAT_STATUS_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 333, Insur.FlatType (Справочник Тип жилого помещения)-- Список создаваемых таблиц:
-- INSUR_FLAT_TYPE
--<DO>--
-- Создание таблицы INSUR_FLAT_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FLAT_TYPE')) then
	 execute 'create table INSUR_FLAT_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_FLAT_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FLAT_TYPE')) then
    execute 'alter table INSUR_FLAT_TYPE add constraint FLAT_TYPE_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 330, Insur.BaseTariff (Справочник «Базовый тариф»)-- Список создаваемых таблиц:
-- INSUR_BASE_TARIFF
--<DO>--
-- Создание таблицы INSUR_BASE_TARIFF
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BASE_TARIFF')) then
	 execute 'create table INSUR_BASE_TARIFF
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_BASE_TARIFF
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BASE_TARIFF')) then
    execute 'alter table INSUR_BASE_TARIFF add constraint REG_330_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 329, Insur.PartCompensation (Справочник «Доля ответственности СК»)-- Список создаваемых таблиц:
-- INSUR_PART_COMPENSATION
--<DO>--
-- Создание таблицы INSUR_PART_COMPENSATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_PART_COMPENSATION')) then
	 execute 'create table INSUR_PART_COMPENSATION
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_PART_COMPENSATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_PART_COMPENSATION')) then
    execute 'alter table INSUR_PART_COMPENSATION add constraint REG_329_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 334, Insur.AgreementProject (Реестр проектов договоров страхования)-- Список создаваемых таблиц:
-- INSUR_AGREEMENT_PROJECT
--<DO>--
-- Создание таблицы INSUR_AGREEMENT_PROJECT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_AGREEMENT_PROJECT')) then
	 execute 'create table INSUR_AGREEMENT_PROJECT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_AGREEMENT_PROJECT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_AGREEMENT_PROJECT')) then
    execute 'alter table INSUR_AGREEMENT_PROJECT add constraint REG_334_PK primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 344, Insur.Bank (Справочник «Банки»)-- Список создаваемых таблиц:
-- INSUR_BANK
--<DO>--
-- Создание таблицы INSUR_BANK
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BANK')) then
	 execute 'create table INSUR_BANK
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_BANK
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BANK')) then
    execute 'alter table INSUR_BANK add constraint INSUR_BANK_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 350, Insur.DamageAmount (Реестр расчетов ущерба по элементам конструкций)-- Список создаваемых таблиц:
-- INSUR_DAMAGE_AMOUNT
--<DO>--
-- Создание таблицы INSUR_DAMAGE_AMOUNT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DAMAGE_AMOUNT')) then
	 execute 'create table INSUR_DAMAGE_AMOUNT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_DAMAGE_AMOUNT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DAMAGE_AMOUNT')) then
    execute 'alter table INSUR_DAMAGE_AMOUNT add constraint INSUR_DAMAGE_AMOUNT_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 340, Insur.Documents (Реестр документов-оснований дел)-- Список создаваемых таблиц:
-- INSUR_DOCUMENTS
--<DO>--
-- Создание таблицы INSUR_DOCUMENTS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DOCUMENTS')) then
	 execute 'create table INSUR_DOCUMENTS
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_DOCUMENTS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DOCUMENTS')) then
    execute 'alter table INSUR_DOCUMENTS add constraint INSUR_DOCUNENTS_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 346, Insur.InsurRate (Справочник «Страховой тариф»)-- Список создаваемых таблиц:
-- INSUR_INSUR_RATE
--<DO>--
-- Создание таблицы INSUR_INSUR_RATE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INSUR_RATE')) then
	 execute 'create table INSUR_INSUR_RATE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_INSUR_RATE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INSUR_RATE')) then
    execute 'alter table INSUR_INSUR_RATE add constraint INSUR_INSUR_RATE_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 347, Insur.CommonPropertyTariff (Справочник "Тарифы по страхованию общего имущества")-- Список создаваемых таблиц:
-- INSUR_COMMON_PROPERTY_TARIFF
--<DO>--
-- Создание таблицы INSUR_COMMON_PROPERTY_TARIFF
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_COMMON_PROPERTY_TARIFF')) then
	 execute 'create table INSUR_COMMON_PROPERTY_TARIFF
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_COMMON_PROPERTY_TARIFF
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_COMMON_PROPERTY_TARIFF')) then
    execute 'alter table INSUR_COMMON_PROPERTY_TARIFF add constraint INSUR_COMMON_PROPERTY_TARIFF_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 348, Insur.LivingPremiseInsurCost (Справочник «Страховая стоимость ЖП»)-- Список создаваемых таблиц:
-- INSUR_LIVING_PREMISE_INSUR_COST
--<DO>--
-- Создание таблицы INSUR_LIVING_PREMISE_INSUR_COST
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LIVING_PREMISE_INSUR_COST')) then
	 execute 'create table INSUR_LIVING_PREMISE_INSUR_COST
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_LIVING_PREMISE_INSUR_COST
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LIVING_PREMISE_INSUR_COST')) then
    execute 'alter table INSUR_LIVING_PREMISE_INSUR_COST add constraint INSUR_LIVING_PREMISE_INSUR_COST_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 349, Insur.ShareResponsibilityICCity (Справочник "Доля ответственности СК и города")-- Список создаваемых таблиц:
-- INSUR_SHARE_RESPONSIBILITY_IC_CITY
--<DO>--
-- Создание таблицы INSUR_SHARE_RESPONSIBILITY_IC_CITY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_SHARE_RESPONSIBILITY_IC_CITY')) then
	 execute 'create table INSUR_SHARE_RESPONSIBILITY_IC_CITY
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_SHARE_RESPONSIBILITY_IC_CITY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_SHARE_RESPONSIBILITY_IC_CITY')) then
    execute 'alter table INSUR_SHARE_RESPONSIBILITY_IC_CITY add constraint INSUR_SHARE_RESPONSIBILITY_IC_CITY_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 809, Fm.Reports.SavedReport (Сохраненные отчеты)-- Список создаваемых таблиц:
-- FM_REPORTS_SAVEDREPORT
--<DO>--
-- Создание таблицы FM_REPORTS_SAVEDREPORT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('FM_REPORTS_SAVEDREPORT')) then
	 execute 'create table FM_REPORTS_SAVEDREPORT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы FM_REPORTS_SAVEDREPORT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('FM_REPORTS_SAVEDREPORT')) then
    execute 'alter table FM_REPORTS_SAVEDREPORT add constraint FM_REPORTS_SAVEDREPORT_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 331, Insur.IntegrateIndicatorsReplecmentCost (Справочник укрупненных показателей удельного веса восстановительной стоимости конструктивных элементов жилого помещения)-- Список создаваемых таблиц:
-- INSUR_INTEGRATED_INDICATORS_REPL_COST
--<DO>--
-- Создание таблицы INSUR_INTEGRATED_INDICATORS_REPL_COST
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INTEGRATED_INDICATORS_REPL_COST')) then
	 execute 'create table INSUR_INTEGRATED_INDICATORS_REPL_COST
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_INTEGRATED_INDICATORS_REPL_COST
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INTEGRATED_INDICATORS_REPL_COST')) then
    execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add constraint INSUR_INTEGRATED_INDICATORS_REPLEMENT_COST_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 351, Insur.Tariff (Справочник «Страховой тариф»)-- Список создаваемых таблиц:
-- INSUR_TARIFF
--<DO>--
-- Создание таблицы INSUR_TARIFF
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_TARIFF')) then
	 execute 'create table INSUR_TARIFF
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_TARIFF
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_TARIFF')) then
    execute 'alter table INSUR_TARIFF add constraint INSUR_TARIFF_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 353, Insur.ActualCostRatio (Справочник "Коэффициент пересчета действительной стоимости")-- Список создаваемых таблиц:
-- INSUR_ACTUAL_COST_RATIO
--<DO>--
-- Создание таблицы INSUR_ACTUAL_COST_RATIO
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_ACTUAL_COST_RATIO')) then
	 execute 'create table INSUR_ACTUAL_COST_RATIO
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_ACTUAL_COST_RATIO
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_ACTUAL_COST_RATIO')) then
    execute 'alter table INSUR_ACTUAL_COST_RATIO add constraint INSUR_ACTUAL_COST_RATIO_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 354, Insur.ReestrPay (Реестр оплат в системе ОПС)-- Список создаваемых таблиц:
-- INSUR_REESTR_PAY
--<DO>--
-- Создание таблицы INSUR_REESTR_PAY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_REESTR_PAY')) then
	 execute 'create table INSUR_REESTR_PAY
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_REESTR_PAY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_REESTR_PAY')) then
    execute 'alter table INSUR_REESTR_PAY add constraint INSUR_REESTR_PAY_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 355, Insur.Invoice (Реестр счетов)-- Список создаваемых таблиц:
-- INSUR_INVOICE
--<DO>--
-- Создание таблицы INSUR_INVOICE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INVOICE')) then
	 execute 'create table INSUR_INVOICE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_INVOICE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INVOICE')) then
    execute 'alter table INSUR_INVOICE add constraint INSUR_INVOICE_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 251, Bti.BtiBuilding (Реестр зданий БТИ)-- Список создаваемых таблиц:
-- BTI_BUILDING_O
-- BTI_BUILDING_Q
--<DO>--
-- Создание таблицы BTI_BUILDING_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_BUILDING_O')) then
	execute 'create table BTI_BUILDING_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы BTI_BUILDING_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_BUILDING_O')) then
   execute 'alter table BTI_BUILDING_O add constraint REG_251_OBJ_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы BTI_BUILDING_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_BUILDING_Q')) then
	 execute 'create table BTI_BUILDING_Q
						(
						  ID      numeric(10) not null,
						  EMP_ID     numeric(10) not null,
						  ACTUAL  numeric(10) not null,
						  STATUS  numeric(10) not null,
						  
						  S_      DATE not null,
						  PO_     DATE not null
						)';
  end if;

-- Создание ограничений таблицы BTI_BUILDING_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_BUILDING_Q')) then
	execute 'alter table BTI_BUILDING_Q add constraint REG_251_QUANT_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_251_QUANT_FK_O')) then
	execute 'alter table BTI_BUILDING_Q add constraint REG_251_QUANT_FK_O foreign key (EMP_ID) references BTI_BUILDING_O (ID)';
  end if;


-- Создание индексов таблицы BTI_BUILDING_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_251_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_251_QUANT_INX_EMP_ID on BTI_BUILDING_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_251_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_251_QUANT_INX_S_PO_ ON BTI_BUILDING_Q (S_, PO_)';
  end if;
end $$;
-- Обработка реестра: 358, Insur.Comment (Реестр "Комментарии")-- Список создаваемых таблиц:
-- INSUR_COMMENT
--<DO>--
-- Создание таблицы INSUR_COMMENT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_COMMENT')) then
	 execute 'create table INSUR_COMMENT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_COMMENT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_COMMENT')) then
    execute 'alter table INSUR_COMMENT add constraint INSUR_COMMENT_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 357, Insur.GbuNoPayReason (Справочник "Причины отказа в выплате ущерба ГБУ")-- Список создаваемых таблиц:
-- INSUR_GBU_NO_PAY_REASON
--<DO>--
-- Создание таблицы INSUR_GBU_NO_PAY_REASON
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_GBU_NO_PAY_REASON')) then
	 execute 'create table INSUR_GBU_NO_PAY_REASON
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_GBU_NO_PAY_REASON
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_GBU_NO_PAY_REASON')) then
    execute 'alter table INSUR_GBU_NO_PAY_REASON add constraint INSUR_GBU_NO_PAY_REASON_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 356, Insur.LinkCausesSubreasonLP (Реестр связи справочник причин ущерба и подпричн для ЖП)-- Список создаваемых таблиц:
-- INSUR_LINK_CAUSES_SUBREASON_LP
--<DO>--
-- Создание таблицы INSUR_LINK_CAUSES_SUBREASON_LP
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LINK_CAUSES_SUBREASON_LP')) then
	 execute 'create table INSUR_LINK_CAUSES_SUBREASON_LP
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_LINK_CAUSES_SUBREASON_LP
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LINK_CAUSES_SUBREASON_LP')) then
    execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add constraint INSUR_LINK_CAUSES_SUBREASON_LP_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 360, ImportLog.InsurBuildingLog (Журнал формирования объектов страхования для Зданий)-- Список создаваемых таблиц:
-- IMPORT_LOG_INSUR_BUILDING
--<DO>--
-- Создание таблицы IMPORT_LOG_INSUR_BUILDING
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('IMPORT_LOG_INSUR_BUILDING')) then
	 execute 'create table IMPORT_LOG_INSUR_BUILDING
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы IMPORT_LOG_INSUR_BUILDING
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('IMPORT_LOG_INSUR_BUILDING')) then
    execute 'alter table IMPORT_LOG_INSUR_BUILDING add constraint IMPORT_LOG_INSUR_BUILDING_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 401, Ehd.Register (ehd.register)-- Список создаваемых таблиц:
-- EHD_REGISTER_O
-- EHD_REGISTER_Q
--<DO>--
-- Создание таблицы EHD_REGISTER_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_REGISTER_O')) then
	execute 'create table EHD_REGISTER_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы EHD_REGISTER_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_REGISTER_O')) then
   execute 'alter table EHD_REGISTER_O add constraint REGISTR_FROM_EHD_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы EHD_REGISTER_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_REGISTER_Q')) then
	 execute 'create table EHD_REGISTER_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы EHD_REGISTER_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_REGISTER_Q')) then
    execute 'alter table EHD_REGISTER_Q add constraint EHD_REGISTER_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 402, Ehd.Location (ehd.location)-- Список создаваемых таблиц:
-- EHD_LOCATION_O
-- EHD_LOCATION_Q
--<DO>--
-- Создание таблицы EHD_LOCATION_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_LOCATION_O')) then
	execute 'create table EHD_LOCATION_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы EHD_LOCATION_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_LOCATION_O')) then
   execute 'alter table EHD_LOCATION_O add constraint LOCATION_FROM_EHD_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы EHD_LOCATION_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_LOCATION_Q')) then
	 execute 'create table EHD_LOCATION_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы EHD_LOCATION_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_LOCATION_Q')) then
    execute 'alter table EHD_LOCATION_Q add constraint EHD_LOCATION_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 405, Ehd.Egrp (EHD.EGRP)-- Список создаваемых таблиц:
-- EHD_EGRP_O
-- EHD_EGRP_Q
--<DO>--
-- Создание таблицы EHD_EGRP_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_EGRP_O')) then
	execute 'create table EHD_EGRP_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы EHD_EGRP_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_EGRP_O')) then
   execute 'alter table EHD_EGRP_O add constraint EHD_EGRN_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы EHD_EGRP_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_EGRP_Q')) then
	 execute 'create table EHD_EGRP_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы EHD_EGRP_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_EGRP_Q')) then
    execute 'alter table EHD_EGRP_Q add constraint EHD_EGRN_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 406, Ehd.Right (EHD.RIGHT)-- Список создаваемых таблиц:
-- EHD_RIGHT_O
-- EHD_RIGHT_Q
--<DO>--
-- Создание таблицы EHD_RIGHT_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_RIGHT_O')) then
	execute 'create table EHD_RIGHT_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы EHD_RIGHT_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_RIGHT_O')) then
   execute 'alter table EHD_RIGHT_O add constraint EHD_RIGHT_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы EHD_RIGHT_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_RIGHT_Q')) then
	 execute 'create table EHD_RIGHT_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы EHD_RIGHT_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_RIGHT_Q')) then
    execute 'alter table EHD_RIGHT_Q add constraint EHD_RIGHT_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 407, Ehd.OldNumber (EHD.OLD_NUMBERS)-- Список создаваемых таблиц:
-- EHD_OLD_NUMBERS
--<DO>--
-- Создание таблицы EHD_OLD_NUMBERS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_OLD_NUMBERS')) then
	 execute 'create table EHD_OLD_NUMBERS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы EHD_OLD_NUMBERS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_OLD_NUMBERS')) then
    execute 'alter table EHD_OLD_NUMBERS add constraint EHD_OLD_NUMBERS_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 359, Insur.FilePlatIdentifyLog (Журнал идентификации зачислений)-- Список создаваемых таблиц:
-- INSUR_FILE_PLAT_IDENTIFY_LOG
--<DO>--
-- Создание таблицы INSUR_FILE_PLAT_IDENTIFY_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FILE_PLAT_IDENTIFY_LOG')) then
	 execute 'create table INSUR_FILE_PLAT_IDENTIFY_LOG
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_FILE_PLAT_IDENTIFY_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FILE_PLAT_IDENTIFY_LOG')) then
    execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add constraint INSUR_FILE_PLAT_IDENTIFY_LOG_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 345, Insur.Subject (Справочник "Управляющие компании")-- Список создаваемых таблиц:
-- INSUR_SUBJECT
--<DO>--
-- Создание таблицы INSUR_SUBJECT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_SUBJECT')) then
	 execute 'create table INSUR_SUBJECT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_SUBJECT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_SUBJECT')) then
    execute 'alter table INSUR_SUBJECT add constraint INSUR_SUBJECT_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 352, Insur.ChangesLog (Реестр регистрации изменения данных)-- Список создаваемых таблиц:
-- INSUR_CHANGES_LOG
--<DO>--
-- Создание таблицы INSUR_CHANGES_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_CHANGES_LOG')) then
	 execute 'create table INSUR_CHANGES_LOG
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_CHANGES_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_CHANGES_LOG')) then
    execute 'alter table INSUR_CHANGES_LOG add constraint INSUR_CHANGES_LOG_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 258, Bti.BtiOkrug (Реестр округов БТИ)-- Список создаваемых таблиц:
-- REF_ADDR_OKRUG
--<DO>--
-- Создание таблицы REF_ADDR_OKRUG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('REF_ADDR_OKRUG')) then
	 execute 'create table REF_ADDR_OKRUG
						(
						  OKRUG_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы REF_ADDR_OKRUG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('REF_ADDR_OKRUG')) then
    execute 'alter table REF_ADDR_OKRUG add constraint REF_ADDR_OKRUG_PKEY primary key (OKRUG_ID)';
  end if;
end $$;
-- Обработка реестра: 259, Bti.BtiDistrict (Реестр районов БТИ)-- Список создаваемых таблиц:
-- REF_ADDR_DISTRICT
--<DO>--
-- Создание таблицы REF_ADDR_DISTRICT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('REF_ADDR_DISTRICT')) then
	 execute 'create table REF_ADDR_DISTRICT
						(
						  DISTRICT_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы REF_ADDR_DISTRICT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('REF_ADDR_DISTRICT')) then
    execute 'alter table REF_ADDR_DISTRICT add constraint REF_ADDR_DISTRICT_PKEY primary key (DISTRICT_ID)';
  end if;
end $$;
-- Обработка реестра: 368, Insur.TypeBuldingFloorLink (Реестр связей типов здания с этажностью и типом констуркции)-- Список создаваемых таблиц:
-- INSUR_TYPE_BUILDING_FLOOR_LINK
--<DO>--
-- Создание таблицы INSUR_TYPE_BUILDING_FLOOR_LINK
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_TYPE_BUILDING_FLOOR_LINK')) then
	 execute 'create table INSUR_TYPE_BUILDING_FLOOR_LINK
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_TYPE_BUILDING_FLOOR_LINK
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_TYPE_BUILDING_FLOOR_LINK')) then
    execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add constraint INSUR_TYPE_BUILDING_FLOOR_LINK_PKEY primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 361, ImportLog.InsurFlatBuildingLog (Журнал формирования помещений страхования)-- Список создаваемых таблиц:
-- IMPORT_LOG_INSUR_FLAT_B
--<DO>--
-- Создание таблицы IMPORT_LOG_INSUR_FLAT_B
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('IMPORT_LOG_INSUR_FLAT_B')) then
	 execute 'create table IMPORT_LOG_INSUR_FLAT_B
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы IMPORT_LOG_INSUR_FLAT_B
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('IMPORT_LOG_INSUR_FLAT_B')) then
    execute 'alter table IMPORT_LOG_INSUR_FLAT_B add constraint  primary key (ID)';
  end if;
end $$;
-- Обработка реестра: 370, Insur.FileProcessLog (Журнал процесса обработки файлов МФЦ)-- Список создаваемых таблиц:
-- INSUR_FILE_PROCESS_LOG
--<DO>--
-- Создание таблицы INSUR_FILE_PROCESS_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FILE_PROCESS_LOG')) then
	 execute 'create table INSUR_FILE_PROCESS_LOG
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы INSUR_FILE_PROCESS_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FILE_PROCESS_LOG')) then
    execute 'alter table INSUR_FILE_PROCESS_LOG add constraint INSUR_FILE_PROCESS_LOG_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 316, Insur.Building (Реестр объектов страхования МКД)-- Список создаваемых таблиц:
-- INSUR_BUILDING_O
-- INSUR_BUILDING_Q
--<DO>--
-- Создание таблицы INSUR_BUILDING_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BUILDING_O')) then
	execute 'create table INSUR_BUILDING_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы INSUR_BUILDING_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BUILDING_O')) then
   execute 'alter table INSUR_BUILDING_O add constraint INSUR_BUILDING_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы INSUR_BUILDING_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BUILDING_Q')) then
	 execute 'create table INSUR_BUILDING_Q
						(
						  ID      numeric(10) not null,
						  EMP_ID     numeric(10) not null,
						  ACTUAL  numeric(10) not null,
						  STATUS  numeric(10) not null,
						  
						  S_      DATE not null,
						  PO_     DATE not null
						)';
  end if;

-- Создание ограничений таблицы INSUR_BUILDING_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BUILDING_Q')) then
	execute 'alter table INSUR_BUILDING_Q add constraint INSUR_BUILDING_Q_KEY primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_316_QUANT_FK_O')) then
	execute 'alter table INSUR_BUILDING_Q add constraint REG_316_QUANT_FK_O foreign key (EMP_ID) references INSUR_BUILDING_O (ID)';
  end if;


-- Создание индексов таблицы INSUR_BUILDING_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_316_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_316_QUANT_INX_EMP_ID on INSUR_BUILDING_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_316_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_316_QUANT_INX_S_PO_ ON INSUR_BUILDING_Q (S_, PO_)';
  end if;
end $$;
-- Обработка реестра: 317, Insur.Flat (Реестр объектов страхования жилых помещений)-- Список создаваемых таблиц:
-- INSUR_FLAT_O
-- INSUR_FLAT_Q
--<DO>--
-- Создание таблицы INSUR_FLAT_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FLAT_O')) then
	execute 'create table INSUR_FLAT_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы INSUR_FLAT_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FLAT_O')) then
   execute 'alter table INSUR_FLAT_O add constraint INSUR_FLAT_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы INSUR_FLAT_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FLAT_Q')) then
	 execute 'create table INSUR_FLAT_Q
						(
						  ID      numeric(10) not null,
						  EMP_ID     numeric(10) not null,
						  ACTUAL  numeric(10) not null,
						  STATUS  numeric(10) not null,
						  
						  S_      DATE not null,
						  PO_     DATE not null
						)';
  end if;

-- Создание ограничений таблицы INSUR_FLAT_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FLAT_Q')) then
	execute 'alter table INSUR_FLAT_Q add constraint INSUR_FLAT_Q_KEY primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_317_QUANT_FK_O')) then
	execute 'alter table INSUR_FLAT_Q add constraint REG_317_QUANT_FK_O foreign key (EMP_ID) references INSUR_FLAT_O (ID)';
  end if;


-- Создание индексов таблицы INSUR_FLAT_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_317_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_317_QUANT_INX_EMP_ID on INSUR_FLAT_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_317_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_317_QUANT_INX_S_PO_ ON INSUR_FLAT_Q (S_, PO_)';
  end if;
end $$;
-- Обработка реестра: 400, Ehd.BuildParcel (Объекты ЕГРН)-- Список создаваемых таблиц:
-- EHD_BUILD_PARCEL_O
-- EHD_BUILD_PARCEL_Q
--<DO>--
-- Создание таблицы EHD_BUILD_PARCEL_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_BUILD_PARCEL_O')) then
	execute 'create table EHD_BUILD_PARCEL_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы EHD_BUILD_PARCEL_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_BUILD_PARCEL_O')) then
   execute 'alter table EHD_BUILD_PARCEL_O add constraint BUILD_PARCEL_FROM_EHD_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы EHD_BUILD_PARCEL_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_BUILD_PARCEL_Q')) then
	 execute 'create table EHD_BUILD_PARCEL_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- Создание ограничений таблицы EHD_BUILD_PARCEL_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_BUILD_PARCEL_Q')) then
    execute 'alter table EHD_BUILD_PARCEL_Q add constraint EHD_BUILD_PARCEL_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- Обработка реестра: 50, Bti.ADDRESS (Реестр адресов БТИ)-- Список создаваемых таблиц:
-- BTI_ADDRESS_O
-- BTI_ADDRESS_Q
--<DO>--
-- Создание таблицы BTI_ADDRESS_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_ADDRESS_O')) then
	execute 'create table BTI_ADDRESS_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы BTI_ADDRESS_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ADDRESS_O')) then
   execute 'alter table BTI_ADDRESS_O add constraint REG_50_OBJECT_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы BTI_ADDRESS_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_ADDRESS_Q')) then
	 execute 'create table BTI_ADDRESS_Q
						(
						  ID      numeric(10) not null,
						  EMP_ID     numeric(10) not null,
						  ACTUAL  numeric(10) not null,
						  STATUS  numeric(10) not null,
						  
						  S_      DATE not null,
						  PO_     DATE not null
						)';
  end if;

-- Создание ограничений таблицы BTI_ADDRESS_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ADDRESS_Q')) then
	execute 'alter table BTI_ADDRESS_Q add constraint REG_50_QUANT_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_50_QUANT_FK_O')) then
	execute 'alter table BTI_ADDRESS_Q add constraint REG_50_QUANT_FK_O foreign key (EMP_ID) references BTI_ADDRESS_O (ID)';
  end if;


-- Создание индексов таблицы BTI_ADDRESS_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_50_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_50_QUANT_INX_EMP_ID on BTI_ADDRESS_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_50_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_50_QUANT_INX_S_PO_ ON BTI_ADDRESS_Q (S_, PO_)';
  end if;
end $$;
-- Обработка реестра: 52, Bti.ADDRLINK (Реестр связи здания БТИ с адресом)-- Список создаваемых таблиц:
-- BTI_ADDRLINK_O
-- BTI_ADDRLINK_Q
--<DO>--
-- Создание таблицы BTI_ADDRLINK_O
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_ADDRLINK_O')) then
	execute 'create table BTI_ADDRLINK_O
						(
						  ID            numeric(10) not null,
						  INFO          VARCHAR(100),
						  DELETED       numeric(10) default 0 not null,
						  "UID"       VARCHAR(50),
						  ENDDATECHANGE DATE
						)';
  end if;


-- Создание ограничений таблицы BTI_ADDRLINK_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ADDRLINK_O')) then
   execute 'alter table BTI_ADDRLINK_O add constraint REG_52_OBJECT_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- Создание таблицы BTI_ADDRLINK_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_ADDRLINK_Q')) then
	 execute 'create table BTI_ADDRLINK_Q
						(
						  ID      numeric(10) not null,
						  EMP_ID     numeric(10) not null,
						  ACTUAL  numeric(10) not null,
						  STATUS  numeric(10) not null,
						  
						  S_      DATE not null,
						  PO_     DATE not null
						)';
  end if;

-- Создание ограничений таблицы BTI_ADDRLINK_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ADDRLINK_Q')) then
	execute 'alter table BTI_ADDRLINK_Q add constraint REG_52_QUANT_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_52_QUANT_FK_O')) then
	execute 'alter table BTI_ADDRLINK_Q add constraint REG_52_QUANT_FK_O foreign key (EMP_ID) references BTI_ADDRLINK_O (ID)';
  end if;


-- Создание индексов таблицы BTI_ADDRLINK_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_52_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_52_QUANT_INX_EMP_ID on BTI_ADDRLINK_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_52_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_52_QUANT_INX_S_PO_ ON BTI_ADDRLINK_Q (S_, PO_)';
  end if;
end $$;
