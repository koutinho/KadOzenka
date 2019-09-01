
-- I. �������� ��������
-- ��������� �������: 920, Core.Register.List (�������������� ������ �������� ��������)-- ������ ����������� ������:
-- CORE_LIST
--<DO>--
-- �������� ������� CORE_LIST
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LIST')) then
	 execute 'create table CORE_LIST
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LIST
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LIST')) then
    execute 'alter table CORE_LIST add constraint REG_920_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 921, Core.Register.ListObject (�������������� ��������, �������� � ������)-- ������ ����������� ������:
-- CORE_LIST_OBJECT
--<DO>--
-- �������� ������� CORE_LIST_OBJECT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LIST_OBJECT')) then
	 execute 'create table CORE_LIST_OBJECT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LIST_OBJECT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LIST_OBJECT')) then
    execute 'alter table CORE_LIST_OBJECT add constraint REG_921_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 924, Core.Register.LayoutColumnType (��� ������� ���������)-- ������ ����������� ������:
-- CORE_LAYOUT_COLUMN_TYPE
--<DO>--
-- �������� ������� CORE_LAYOUT_COLUMN_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_COLUMN_TYPE')) then
	 execute 'create table CORE_LAYOUT_COLUMN_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LAYOUT_COLUMN_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LAYOUT_COLUMN_TYPE')) then
    execute 'alter table CORE_LAYOUT_COLUMN_TYPE add constraint REG_924_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 925, Core.SRD.UserSettingsRegisterView (���������������� ��������� ������������� �������)-- ������ ����������� ������:
-- CORE_SRD_USERSETTINGSREGVIEW
--<DO>--
-- �������� ������� CORE_SRD_USERSETTINGSREGVIEW
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGSREGVIEW')) then
	 execute 'create table CORE_SRD_USERSETTINGSREGVIEW
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_USERSETTINGSREGVIEW
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USERSETTINGSREGVIEW')) then
    execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add constraint REG_925_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 930, Core.Register.Register (������ ��������)-- ������ ����������� ������:
-- CORE_REGISTER
--<DO>--
-- �������� ������� CORE_REGISTER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER')) then
	 execute 'create table CORE_REGISTER
						(
						  REGISTERID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REGISTER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER')) then
    execute 'alter table CORE_REGISTER add constraint REG_930_QUANT_PK primary key (REGISTERID)';
  end if;
end $$;
-- ��������� �������: 931, Core.Register.Attribute (������ ����������� �������)-- ������ ����������� ������:
-- CORE_REGISTER_ATTRIBUTE
--<DO>--
-- �������� ������� CORE_REGISTER_ATTRIBUTE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_ATTRIBUTE')) then
	 execute 'create table CORE_REGISTER_ATTRIBUTE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REGISTER_ATTRIBUTE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER_ATTRIBUTE')) then
    execute 'alter table CORE_REGISTER_ATTRIBUTE add constraint REG_931_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 932, Core.Register.Relation (������ ������ ����� ���������)-- ������ ����������� ������:
-- CORE_REGISTER_RELATION
--<DO>--
-- �������� ������� CORE_REGISTER_RELATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_RELATION')) then
	 execute 'create table CORE_REGISTER_RELATION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REGISTER_RELATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER_RELATION')) then
    execute 'alter table CORE_REGISTER_RELATION add constraint REG_932_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 933, Core.Register.Layout (���������)-- ������ ����������� ������:
-- CORE_LAYOUT
--<DO>--
-- �������� ������� CORE_LAYOUT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT')) then
	 execute 'create table CORE_LAYOUT
						(
						  LAYOUTID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LAYOUT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LAYOUT')) then
    execute 'alter table CORE_LAYOUT add constraint REG_933_QUANT_PK primary key (LAYOUTID)';
  end if;
end $$;
-- ��������� �������: 935, Core.Register.LayoutDetail (����������� ���������)-- ������ ����������� ������:
-- CORE_LAYOUT_DETAILS
--<DO>--
-- �������� ������� CORE_LAYOUT_DETAILS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_DETAILS')) then
	 execute 'create table CORE_LAYOUT_DETAILS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LAYOUT_DETAILS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LAYOUT_DETAILS')) then
    execute 'alter table CORE_LAYOUT_DETAILS add constraint REG_935_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 936, Core.Register.Qry (������� ��������)-- ������ ����������� ������:
-- CORE_QRY
--<DO>--
-- �������� ������� CORE_QRY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY')) then
	 execute 'create table CORE_QRY
						(
						  QRYID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_QRY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_QRY')) then
    execute 'alter table CORE_QRY add constraint REG_936_QUANT_PK primary key (QRYID)';
  end if;
end $$;
-- ��������� �������: 937, Core.Register.QryFilter (������� ��������)-- ������ ����������� ������:
-- CORE_QRY_FILTER
--<DO>--
-- �������� ������� CORE_QRY_FILTER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY_FILTER')) then
	 execute 'create table CORE_QRY_FILTER
						(
						  QRYFILTERID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_QRY_FILTER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_QRY_FILTER')) then
    execute 'alter table CORE_QRY_FILTER add constraint REG_937_QUANT_PK primary key (QRYFILTERID)';
  end if;
end $$;
-- ��������� �������: 938, Core.Register.QryOperation (�������� ��������)-- ������ ����������� ������:
-- CORE_QRY_OPERATION
--<DO>--
-- �������� ������� CORE_QRY_OPERATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY_OPERATION')) then
	 execute 'create table CORE_QRY_OPERATION
						(
						  QRYOPERATIONID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_QRY_OPERATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_QRY_OPERATION')) then
    execute 'alter table CORE_QRY_OPERATION add constraint REG_938_QUANT_PK primary key (QRYOPERATIONID)';
  end if;
end $$;
-- ��������� �������: 939, Core.Register.Lock (���������� ������� �������)-- ������ ����������� ������:
-- CORE_REGISTER_LOCK
--<DO>--
-- �������� ������� CORE_REGISTER_LOCK
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_LOCK')) then
	 execute 'create table CORE_REGISTER_LOCK
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REGISTER_LOCK
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER_LOCK')) then
    execute 'alter table CORE_REGISTER_LOCK add constraint REG_939_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 940, Core.SRD.Audit (����� �������� ������������� � ��������� ������� (���������) �������)-- ������ ����������� ������:
-- CORE_SRD_AUDIT
--<DO>--
-- �������� ������� CORE_SRD_AUDIT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_AUDIT')) then
	 execute 'create table CORE_SRD_AUDIT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_AUDIT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_AUDIT')) then
    execute 'alter table CORE_SRD_AUDIT add constraint REG_940_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 941, Core.SRD.Department (������������� � ����������� ������������ �������)-- ������ ����������� ������:
-- CORE_SRD_DEPARTMENT
--<DO>--
-- �������� ������� CORE_SRD_DEPARTMENT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_DEPARTMENT')) then
	 execute 'create table CORE_SRD_DEPARTMENT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_DEPARTMENT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_DEPARTMENT')) then
    execute 'alter table CORE_SRD_DEPARTMENT add constraint REG_941_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 942, Core.SRD.Function (������� ������� (���������) �������)-- ������ ����������� ������:
-- CORE_SRD_FUNCTION
--<DO>--
-- �������� ������� CORE_SRD_FUNCTION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_FUNCTION')) then
	 execute 'create table CORE_SRD_FUNCTION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_FUNCTION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_FUNCTION')) then
    execute 'alter table CORE_SRD_FUNCTION add constraint REG_942_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 945, Core.SRD.Role (���� � �������)-- ������ ����������� ������:
-- CORE_SRD_ROLE
--<DO>--
-- �������� ������� CORE_SRD_ROLE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE')) then
	 execute 'create table CORE_SRD_ROLE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_ROLE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE')) then
    execute 'alter table CORE_SRD_ROLE add constraint REG_945_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 946, Core.SRD.RoleFunction (������� ���� (������ LOCROLE_LOCFUNCTION))-- ������ ����������� ������:
-- CORE_SRD_ROLE_FUNCTION
--<DO>--
-- �������� ������� CORE_SRD_ROLE_FUNCTION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_FUNCTION')) then
	 execute 'create table CORE_SRD_ROLE_FUNCTION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_ROLE_FUNCTION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE_FUNCTION')) then
    execute 'alter table CORE_SRD_ROLE_FUNCTION add constraint REG_946_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 947, Core.SRD.RoleRegister (����� ������� ���� � �������)-- ������ ����������� ������:
-- CORE_SRD_ROLE_REGISTER
--<DO>--
-- �������� ������� CORE_SRD_ROLE_REGISTER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_REGISTER')) then
	 execute 'create table CORE_SRD_ROLE_REGISTER
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_ROLE_REGISTER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE_REGISTER')) then
    execute 'alter table CORE_SRD_ROLE_REGISTER add constraint REG_947_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 948, Core.SRD.RoleAttr (����� ������� ���� � ��������� �������)-- ������ ����������� ������:
-- CORE_SRD_ROLE_ATTR
--<DO>--
-- �������� ������� CORE_SRD_ROLE_ATTR
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_ATTR')) then
	 execute 'create table CORE_SRD_ROLE_ATTR
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_ROLE_ATTR
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE_ATTR')) then
    execute 'alter table CORE_SRD_ROLE_ATTR add constraint REG_948_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 949, Core.SRD.Session (��������� ������ ������������)-- ������ ����������� ������:
-- CORE_SRD_SESSION
--<DO>--
-- �������� ������� CORE_SRD_SESSION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_SESSION')) then
	 execute 'create table CORE_SRD_SESSION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_SESSION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_SESSION')) then
    execute 'alter table CORE_SRD_SESSION add constraint REG_949_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 950, Core.SRD.User (������������ �������)-- ������ ����������� ������:
-- CORE_SRD_USER
--<DO>--
-- �������� ������� CORE_SRD_USER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USER')) then
	 execute 'create table CORE_SRD_USER
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_USER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USER')) then
    execute 'alter table CORE_SRD_USER add constraint REG_950_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 951, Core.SRD.UserSettings (���������������� ���������)-- ������ ����������� ������:
-- CORE_SRD_USERSETTINGS
--<DO>--
-- �������� ������� CORE_SRD_USERSETTINGS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGS')) then
	 execute 'create table CORE_SRD_USERSETTINGS
						(
						  USERID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_USERSETTINGS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USERSETTINGS')) then
    execute 'alter table CORE_SRD_USERSETTINGS add constraint REG_951_QUANT_PK primary key (USERID)';
  end if;
end $$;
-- ��������� �������: 952, Core.SRD.UserRole (���� ������������)-- ������ ����������� ������:
-- CORE_SRD_USER_ROLE
--<DO>--
-- �������� ������� CORE_SRD_USER_ROLE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USER_ROLE')) then
	 execute 'create table CORE_SRD_USER_ROLE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_USER_ROLE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USER_ROLE')) then
    execute 'alter table CORE_SRD_USER_ROLE add constraint REG_952_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 954, Core.SRD.UserSettingsLayout (���������������� ��������� ����������)-- ������ ����������� ������:
-- CORE_SRD_USERSETTINGSLAYOUT
--<DO>--
-- �������� ������� CORE_SRD_USERSETTINGSLAYOUT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGSLAYOUT')) then
	 execute 'create table CORE_SRD_USERSETTINGSLAYOUT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_USERSETTINGSLAYOUT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_USERSETTINGSLAYOUT')) then
    execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add constraint REG_954_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 955, Core.SRD.RoleFilter (������������� ���� ������� �� ������ ��������)-- ������ ����������� ������:
-- CORE_SRD_ROLE_FILTER
--<DO>--
-- �������� ������� CORE_SRD_ROLE_FILTER
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_FILTER')) then
	 execute 'create table CORE_SRD_ROLE_FILTER
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_ROLE_FILTER
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_ROLE_FILTER')) then
    execute 'alter table CORE_SRD_ROLE_FILTER add constraint REG_955_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 956, Core.Register.LayoutExport (�������� ������ �� ���������)-- ������ ����������� ������:
-- CORE_LAYOUT_EXPORT
--<DO>--
-- �������� ������� CORE_LAYOUT_EXPORT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_EXPORT')) then
	 execute 'create table CORE_LAYOUT_EXPORT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LAYOUT_EXPORT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LAYOUT_EXPORT')) then
    execute 'alter table CORE_LAYOUT_EXPORT add constraint REG_956_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 957, Core.SRD.RegisterCategory (��������� ������� � ������ ��������)-- ������ ����������� ������:
-- CORE_SRD_REGISTER_CATEGORY
--<DO>--
-- �������� ������� CORE_SRD_REGISTER_CATEGORY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_REGISTER_CATEGORY')) then
	 execute 'create table CORE_SRD_REGISTER_CATEGORY
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_REGISTER_CATEGORY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_REGISTER_CATEGORY')) then
    execute 'alter table CORE_SRD_REGISTER_CATEGORY add constraint REG_957_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 958, Core.SRD.FunctionRegisterCategory (������ ������� � ���������� ������� ��������)-- ������ ����������� ������:
-- CORE_SRD_FUNCTION_REG_CAT
--<DO>--
-- �������� ������� CORE_SRD_FUNCTION_REG_CAT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_FUNCTION_REG_CAT')) then
	 execute 'create table CORE_SRD_FUNCTION_REG_CAT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_SRD_FUNCTION_REG_CAT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_SRD_FUNCTION_REG_CAT')) then
    execute 'alter table CORE_SRD_FUNCTION_REG_CAT add constraint REG_958_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 960, Core.TD.Template (������ (���) ���������������� ���������)-- ������ ����������� ������:
-- CORE_TD_TEMPLATE
--<DO>--
-- �������� ������� CORE_TD_TEMPLATE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE')) then
	 execute 'create table CORE_TD_TEMPLATE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_TEMPLATE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TEMPLATE')) then
    execute 'alter table CORE_TD_TEMPLATE add constraint REG_960_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 961, Core.TD.TemplateVersion (������ ������� ���������������� ���������)-- ������ ����������� ������:
-- CORE_TD_TEMPLATE_VERSION
--<DO>--
-- �������� ������� CORE_TD_TEMPLATE_VERSION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE_VERSION')) then
	 execute 'create table CORE_TD_TEMPLATE_VERSION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_TEMPLATE_VERSION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TEMPLATE_VERSION')) then
    execute 'alter table CORE_TD_TEMPLATE_VERSION add constraint REG_961_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 962, Core.TD.Status (���� �������� ���������� ���������������� ���������)-- ������ ����������� ������:
-- CORE_TD_STATUS
--<DO>--
-- �������� ������� CORE_TD_STATUS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_STATUS')) then
	 execute 'create table CORE_TD_STATUS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_STATUS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_STATUS')) then
    execute 'alter table CORE_TD_STATUS add constraint REG_962_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 963, Core.TD.Instance (���������� ��������������� ����������)-- ������ ����������� ������:
-- CORE_TD_INSTANCE
--<DO>--
-- �������� ������� CORE_TD_INSTANCE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_INSTANCE')) then
	 execute 'create table CORE_TD_INSTANCE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_INSTANCE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_INSTANCE')) then
    execute 'alter table CORE_TD_INSTANCE add constraint REG_963_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 964, Core.TD.Changeset (����� ��������� � ��������)-- ������ ����������� ������:
-- CORE_TD_CHANGESET
--<DO>--
-- �������� ������� CORE_TD_CHANGESET
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_CHANGESET')) then
	 execute 'create table CORE_TD_CHANGESET
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_CHANGESET
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_CHANGESET')) then
    execute 'alter table CORE_TD_CHANGESET add constraint REG_964_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 965, Core.TD.Change (��������� � �������)-- ������ ����������� ������:
-- CORE_TD_CHANGE
--<DO>--
-- �������� ������� CORE_TD_CHANGE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_CHANGE')) then
	 execute 'create table CORE_TD_CHANGE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_CHANGE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_CHANGE')) then
    execute 'alter table CORE_TD_CHANGE add constraint REG_965_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 966, Core.TD.AuditAction (���� ���������� �������� � ����������� ���������������� ���������)-- ������ ����������� ������:
-- CORE_TD_AUDIT_ACTION
--<DO>--
-- �������� ������� CORE_TD_AUDIT_ACTION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_AUDIT_ACTION')) then
	 execute 'create table CORE_TD_AUDIT_ACTION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_AUDIT_ACTION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_AUDIT_ACTION')) then
    execute 'alter table CORE_TD_AUDIT_ACTION add constraint REG_966_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 967, Core.TD.Audit (����� �������� � ����������� ���������������� ���������)-- ������ ����������� ������:
-- CORE_TD_AUDIT
--<DO>--
-- �������� ������� CORE_TD_AUDIT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_AUDIT')) then
	 execute 'create table CORE_TD_AUDIT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_AUDIT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_AUDIT')) then
    execute 'alter table CORE_TD_AUDIT add constraint REG_967_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 968, Core.TD.Tree (������ �������� )-- ������ ����������� ������:
-- CORE_TD_TREE
--<DO>--
-- �������� ������� CORE_TD_TREE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TREE')) then
	 execute 'create table CORE_TD_TREE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_TREE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TREE')) then
    execute 'alter table CORE_TD_TREE add constraint REG_968_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 969, Core.TD.Attachment (����������� ������ ���������� ���������)-- ������ ����������� ������:
-- CORE_TD_ATTACHMENTS
--<DO>--
-- �������� ������� CORE_TD_ATTACHMENTS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_ATTACHMENTS')) then
	 execute 'create table CORE_TD_ATTACHMENTS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_ATTACHMENTS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_ATTACHMENTS')) then
    execute 'alter table CORE_TD_ATTACHMENTS add constraint REG_969_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 970, Core.TD.TP (������ �� ����������� ��������, �� �������� ������ ��������� ���������������� ���������)-- ������ ����������� ������:
-- CORE_TD_TP
--<DO>--
-- �������� ������� CORE_TD_TP
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TP')) then
	 execute 'create table CORE_TD_TP
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_TP
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TP')) then
    execute 'alter table CORE_TD_TP add constraint REG_970_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 971, Core.TD.TemplateType (��� ������� ���������������� ���������)-- ������ ����������� ������:
-- CORE_TD_TEMPLATE_TYPE
--<DO>--
-- �������� ������� CORE_TD_TEMPLATE_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE_TYPE')) then
	 execute 'create table CORE_TD_TEMPLATE_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_TD_TEMPLATE_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_TD_TEMPLATE_TYPE')) then
    execute 'alter table CORE_TD_TEMPLATE_TYPE add constraint REG_971_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 975, Core.LongProcess.Queue (������� ������ ���������)-- ������ ����������� ������:
-- CORE_LONG_PROCESS_QUEUE
--<DO>--
-- �������� ������� CORE_LONG_PROCESS_QUEUE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_QUEUE')) then
	 execute 'create table CORE_LONG_PROCESS_QUEUE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LONG_PROCESS_QUEUE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LONG_PROCESS_QUEUE')) then
    execute 'alter table CORE_LONG_PROCESS_QUEUE add constraint REG_975_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 976, Core.LongProcess.ProcessType (���� ������ ���������)-- ������ ����������� ������:
-- CORE_LONG_PROCESS_TYPE
--<DO>--
-- �������� ������� CORE_LONG_PROCESS_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_TYPE')) then
	 execute 'create table CORE_LONG_PROCESS_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LONG_PROCESS_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LONG_PROCESS_TYPE')) then
    execute 'alter table CORE_LONG_PROCESS_TYPE add constraint REG_976_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 320, Insur.Okrug (���������� �������)-- ������ ����������� ������:
-- INSUR_OKRUG
--<DO>--
-- �������� ������� INSUR_OKRUG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_OKRUG')) then
	 execute 'create table INSUR_OKRUG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_OKRUG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_OKRUG')) then
    execute 'alter table INSUR_OKRUG add constraint INSUR_OKRUG_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 977, Core.LongProcess.Log (������ ��������� ������ ���������)-- ������ ����������� ������:
-- CORE_LONG_PROCESS_LOG
--<DO>--
-- �������� ������� CORE_LONG_PROCESS_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_LOG')) then
	 execute 'create table CORE_LONG_PROCESS_LOG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_LONG_PROCESS_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_LONG_PROCESS_LOG')) then
    execute 'alter table CORE_LONG_PROCESS_LOG add constraint REG_977_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 978, Core.Shared.Configparam (����� ������������)-- ������ ����������� ������:
-- CORE_CONFIGPARAM
--<DO>--
-- �������� ������� CORE_CONFIGPARAM
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_CONFIGPARAM')) then
	 execute 'create table CORE_CONFIGPARAM
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_CONFIGPARAM
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_CONFIGPARAM')) then
    execute 'alter table CORE_CONFIGPARAM add constraint REG_978_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 982, Core.Shared.Reference (����������)-- ������ ����������� ������:
-- CORE_REFERENCE
--<DO>--
-- �������� ������� CORE_REFERENCE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE')) then
	 execute 'create table CORE_REFERENCE
						(
						  REFERENCEID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REFERENCE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REFERENCE')) then
    execute 'alter table CORE_REFERENCE add constraint REG_982_QUANT_PK primary key (REFERENCEID)';
  end if;
end $$;
-- ��������� �������: 983, Core.Shared.ReferenceItem (���������� ��������)-- ������ ����������� ������:
-- CORE_REFERENCE_ITEM
--<DO>--
-- �������� ������� CORE_REFERENCE_ITEM
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_ITEM')) then
	 execute 'create table CORE_REFERENCE_ITEM
						(
						  ITEMID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REFERENCE_ITEM
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REFERENCE_ITEM')) then
    execute 'alter table CORE_REFERENCE_ITEM add constraint REG_983_QUANT_PK primary key (ITEMID)';
  end if;
end $$;
-- ��������� �������: 984, Core.Shared.ReferenceRelation (����� ������������)-- ������ ����������� ������:
-- CORE_REFERENCE_RELATION
--<DO>--
-- �������� ������� CORE_REFERENCE_RELATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_RELATION')) then
	 execute 'create table CORE_REFERENCE_RELATION
						(
						  RELID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REFERENCE_RELATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REFERENCE_RELATION')) then
    execute 'alter table CORE_REFERENCE_RELATION add constraint REG_984_QUANT_PK primary key (RELID)';
  end if;
end $$;
-- ��������� �������: 985, Core.Shared.ReferenceTree (����� ���������� ��������)-- ������ ����������� ������:
-- CORE_REFERENCE_TREE
--<DO>--
-- �������� ������� CORE_REFERENCE_TREE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_TREE')) then
	 execute 'create table CORE_REFERENCE_TREE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REFERENCE_TREE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REFERENCE_TREE')) then
    execute 'alter table CORE_REFERENCE_TREE add constraint REG_985_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 986, Core.Shared.Attachment (�����)-- ������ ����������� ������:
-- CORE_ATTACHMENT
--<DO>--
-- �������� ������� CORE_ATTACHMENT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT')) then
	 execute 'create table CORE_ATTACHMENT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_ATTACHMENT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_ATTACHMENT')) then
    execute 'alter table CORE_ATTACHMENT add constraint REG_986_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 987, Core.Shared.AttachmentFile (����� ������)-- ������ ����������� ������:
-- CORE_ATTACHMENT_FILE
--<DO>--
-- �������� ������� CORE_ATTACHMENT_FILE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT_FILE')) then
	 execute 'create table CORE_ATTACHMENT_FILE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_ATTACHMENT_FILE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_ATTACHMENT_FILE')) then
    execute 'alter table CORE_ATTACHMENT_FILE add constraint REG_987_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 988, Core.Shared.AttachmentObject (����� ������ � ������� �������)-- ������ ����������� ������:
-- CORE_ATTACHMENT_OBJECT
--<DO>--
-- �������� ������� CORE_ATTACHMENT_OBJECT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT_OBJECT')) then
	 execute 'create table CORE_ATTACHMENT_OBJECT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_ATTACHMENT_OBJECT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_ATTACHMENT_OBJECT')) then
    execute 'alter table CORE_ATTACHMENT_OBJECT add constraint REG_988_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 989, Core.Shared.ErrorLog (������ ������)-- ������ ����������� ������:
-- CORE_ERROR_LOG
--<DO>--
-- �������� ������� CORE_ERROR_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_ERROR_LOG')) then
	 execute 'create table CORE_ERROR_LOG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_ERROR_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_ERROR_LOG')) then
    execute 'alter table CORE_ERROR_LOG add constraint REG_989_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 991, Core.Shared.RegisterState (����������� ��������� ������������� )-- ������ ����������� ������:
-- CORE_REGISTER_STATE
--<DO>--
-- �������� ������� CORE_REGISTER_STATE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_STATE')) then
	 execute 'create table CORE_REGISTER_STATE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REGISTER_STATE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGISTER_STATE')) then
    execute 'alter table CORE_REGISTER_STATE add constraint REG_991_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 992, Core.Shared.Diagnostics (������ ���������� ���������)-- ������ ����������� ������:
-- CORE_DIAGNOSTICS
--<DO>--
-- �������� ������� CORE_DIAGNOSTICS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_DIAGNOSTICS')) then
	 execute 'create table CORE_DIAGNOSTICS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_DIAGNOSTICS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_DIAGNOSTICS')) then
    execute 'alter table CORE_DIAGNOSTICS add constraint REG_992_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 994, Core.Shared.RegNomRepository (����������� ��������������� �������)-- ������ ����������� ������:
-- CORE_REGNOM_REPOSITORY
--<DO>--
-- �������� ������� CORE_REGNOM_REPOSITORY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGNOM_REPOSITORY')) then
	 execute 'create table CORE_REGNOM_REPOSITORY
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REGNOM_REPOSITORY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGNOM_REPOSITORY')) then
    execute 'alter table CORE_REGNOM_REPOSITORY add constraint  primary key (ID)';
  end if;
end $$;
-- ��������� �������: 995, Core.Shared.RegNomSequences (������������������ ��������������� �������)-- ������ ����������� ������:
-- CORE_REGNOM_SEQUENCES
--<DO>--
-- �������� ������� CORE_REGNOM_SEQUENCES
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_REGNOM_SEQUENCES')) then
	 execute 'create table CORE_REGNOM_SEQUENCES
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_REGNOM_SEQUENCES
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_REGNOM_SEQUENCES')) then
    execute 'alter table CORE_REGNOM_SEQUENCES add constraint REG_995_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 996, Core.Shared.CacheUpdates (��������� ����� ���������� ����)-- ������ ����������� ������:
-- CORE_CACHE_UPDATES
--<DO>--
-- �������� ������� CORE_CACHE_UPDATES
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_CACHE_UPDATES')) then
	 execute 'create table CORE_CACHE_UPDATES
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_CACHE_UPDATES
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_CACHE_UPDATES')) then
    execute 'alter table CORE_CACHE_UPDATES add constraint REG_996_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 997, Core.Shared.UpdateStructure (������ ���������� ��������� ��)-- ������ ����������� ������:
-- CORE_UPDSTRU_LOG
--<DO>--
-- �������� ������� CORE_UPDSTRU_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_UPDSTRU_LOG')) then
	 execute 'create table CORE_UPDSTRU_LOG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_UPDSTRU_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_UPDSTRU_LOG')) then
    execute 'alter table CORE_UPDSTRU_LOG add constraint REG_997_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 998, Core.Shared.Holiday (�������� � ����������� ���)-- ������ ����������� ������:
-- CORE_HOLIDAYS
--<DO>--
-- �������� ������� CORE_HOLIDAYS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('CORE_HOLIDAYS')) then
	 execute 'create table CORE_HOLIDAYS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� CORE_HOLIDAYS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('CORE_HOLIDAYS')) then
    execute 'alter table CORE_HOLIDAYS add constraint REG_998_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 321, Insur.District (���������� �������)-- ������ ����������� ������:
-- INSUR_DISTRICT
--<DO>--
-- �������� ������� INSUR_DISTRICT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DISTRICT')) then
	 execute 'create table INSUR_DISTRICT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_DISTRICT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DISTRICT')) then
    execute 'alter table INSUR_DISTRICT add constraint INSUR_DISTRICT_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 308, Insur.Fsp (������ ���������� ������ ������������ (���))-- ������ ����������� ������:
-- INSUR_FSP_O
-- INSUR_FSP_Q
--<DO>--
-- �������� ������� INSUR_FSP_O
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


-- �������� ����������� ������� INSUR_FSP_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FSP_O')) then
   execute 'alter table INSUR_FSP_O add constraint REG_308_OBJ_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� INSUR_FSP_Q
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

-- �������� ����������� ������� INSUR_FSP_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FSP_Q')) then
	execute 'alter table INSUR_FSP_Q add constraint REG_308_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_308_QUANT_FK_O')) then
	execute 'alter table INSUR_FSP_Q add constraint REG_308_QUANT_FK_O foreign key (EMP_ID) references INSUR_FSP_O (ID)';
  end if;


-- �������� �������� ������� INSUR_FSP_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_308_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_308_QUANT_INX_EMP_ID on INSUR_FSP_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_308_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_308_QUANT_INX_S_PO_ ON INSUR_FSP_Q (S_, PO_)';
  end if;
end $$;
-- ��������� �������: 302, Insur.LogFile (������ �������� ��������� ������ ������)-- ������ ����������� ������:
-- INSUR_LOG_FILE
--<DO>--
-- �������� ������� INSUR_LOG_FILE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LOG_FILE')) then
	 execute 'create table INSUR_LOG_FILE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_LOG_FILE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LOG_FILE')) then
    execute 'alter table INSUR_LOG_FILE add constraint REG_302_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 850, Dashboards.Dashboard (��������� ���������)-- ������ ����������� ������:
-- DASHBOARDS_DASHBOARD
--<DO>--
-- �������� ������� DASHBOARDS_DASHBOARD
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_DASHBOARD')) then
	 execute 'create table DASHBOARDS_DASHBOARD
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� DASHBOARDS_DASHBOARD
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('DASHBOARDS_DASHBOARD')) then
    execute 'alter table DASHBOARDS_DASHBOARD add constraint REG_850_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 851, Dashboards.Panel (���������� ���������)-- ������ ����������� ������:
-- DASHBOARDS_PANEL
--<DO>--
-- �������� ������� DASHBOARDS_PANEL
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_PANEL')) then
	 execute 'create table DASHBOARDS_PANEL
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� DASHBOARDS_PANEL
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('DASHBOARDS_PANEL')) then
    execute 'alter table DASHBOARDS_PANEL add constraint REG_851_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 852, Dashboards.PanelTypes (���� �������)-- ������ ����������� ������:
-- DASHBOARDS_PANEL_TYPE
--<DO>--
-- �������� ������� DASHBOARDS_PANEL_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_PANEL_TYPE')) then
	 execute 'create table DASHBOARDS_PANEL_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� DASHBOARDS_PANEL_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('DASHBOARDS_PANEL_TYPE')) then
    execute 'alter table DASHBOARDS_PANEL_TYPE add constraint REG_852_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 853, Dashboards.UserSettings (���������������� ��������� �������)-- ������ ����������� ������:
-- DASHBOARDS_USER_SETTINGS
--<DO>--
-- �������� ������� DASHBOARDS_USER_SETTINGS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_USER_SETTINGS')) then
	 execute 'create table DASHBOARDS_USER_SETTINGS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� DASHBOARDS_USER_SETTINGS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('DASHBOARDS_USER_SETTINGS')) then
    execute 'alter table DASHBOARDS_USER_SETTINGS add constraint REG_853_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 600, SPD.RequestRegistration (������ ����� �������� ���)-- ������ ����������� ������:
-- SPD_REQUEST_REGISTRATION
--<DO>--
-- �������� ������� SPD_REQUEST_REGISTRATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('SPD_REQUEST_REGISTRATION')) then
	 execute 'create table SPD_REQUEST_REGISTRATION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� SPD_REQUEST_REGISTRATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('SPD_REQUEST_REGISTRATION')) then
    execute 'alter table SPD_REQUEST_REGISTRATION add constraint REG_600_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 601, SPD.CreateFullApplicationLog (������ �������� ��� CreateFullApplication)-- ������ ����������� ������:
-- SPD_CREATE_FULL_APP_LOG
--<DO>--
-- �������� ������� SPD_CREATE_FULL_APP_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('SPD_CREATE_FULL_APP_LOG')) then
	 execute 'create table SPD_CREATE_FULL_APP_LOG
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� SPD_CREATE_FULL_APP_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('SPD_CREATE_FULL_APP_LOG')) then
    execute 'alter table SPD_CREATE_FULL_APP_LOG add constraint REG_601_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 650, SPD.DocAgreement (������������ ����������)-- ������ ����������� ������:
-- SPD_DOC_AGREEMENT
--<DO>--
-- �������� ������� SPD_DOC_AGREEMENT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('SPD_DOC_AGREEMENT')) then
	 execute 'create table SPD_DOC_AGREEMENT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� SPD_DOC_AGREEMENT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('SPD_DOC_AGREEMENT')) then
    execute 'alter table SPD_DOC_AGREEMENT add constraint REG_650_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 651, SPD.UserSRD2SPD (������������ ������������� ��� � ���)-- ������ ����������� ������:
-- SPD_USERSRD2SPD
--<DO>--
-- �������� ������� SPD_USERSRD2SPD
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('SPD_USERSRD2SPD')) then
	 execute 'create table SPD_USERSRD2SPD
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� SPD_USERSRD2SPD
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('SPD_USERSRD2SPD')) then
    execute 'alter table SPD_USERSRD2SPD add constraint REG_651_QUANT_PK primary key (ID)';
  end if;
end $$;
-- ��������� �������: 301, Insur.InputFile (������ �������� ������)-- ������ ����������� ������:
-- INSUR_INPUT_FILE
--<DO>--
-- �������� ������� INSUR_INPUT_FILE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INPUT_FILE')) then
	 execute 'create table INSUR_INPUT_FILE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_INPUT_FILE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INPUT_FILE')) then
    execute 'alter table INSUR_INPUT_FILE add constraint REG_301_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 303, Insur.BankPlat (������ ���������� ������ �����)-- ������ ����������� ������:
-- INSUR_BANK_PLAT
--<DO>--
-- �������� ������� INSUR_BANK_PLAT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BANK_PLAT')) then
	 execute 'create table INSUR_BANK_PLAT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_BANK_PLAT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BANK_PLAT')) then
    execute 'alter table INSUR_BANK_PLAT add constraint REG_303_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 304, Insur.SvodBank (������ c������ ������ �� ����� �����)-- ������ ����������� ������:
-- INSUR_SVOD_BANK
--<DO>--
-- �������� ������� INSUR_SVOD_BANK
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_SVOD_BANK')) then
	 execute 'create table INSUR_SVOD_BANK
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_SVOD_BANK
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_SVOD_BANK')) then
    execute 'alter table INSUR_SVOD_BANK add constraint REG_304_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 305, Insur.InputNach (������ ����������)-- ������ ����������� ������:
-- INSUR_INPUT_NACH
--<DO>--
-- �������� ������� INSUR_INPUT_NACH
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INPUT_NACH')) then
	 execute 'create table INSUR_INPUT_NACH
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_INPUT_NACH
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INPUT_NACH')) then
    execute 'alter table INSUR_INPUT_NACH add constraint REG_305_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 306, Insur.InputPlat (������ ���������� (��������))-- ������ ����������� ������:
-- INSUR_INPUT_PLAT
--<DO>--
-- �������� ������� INSUR_INPUT_PLAT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INPUT_PLAT')) then
	 execute 'create table INSUR_INPUT_PLAT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_INPUT_PLAT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INPUT_PLAT')) then
    execute 'alter table INSUR_INPUT_PLAT add constraint REG_306_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 307, Insur.Balance (������ ��������� ����� ��������� �������)-- ������ ����������� ������:
-- INSUR_BALANCE
--<DO>--
-- �������� ������� INSUR_BALANCE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BALANCE')) then
	 execute 'create table INSUR_BALANCE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_BALANCE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BALANCE')) then
    execute 'alter table INSUR_BALANCE add constraint REG_307_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 309, Insur.PolicySvd (������ ��������� ������� � ������������)-- ������ ����������� ������:
-- INSUR_POLICY_SVD
--<DO>--
-- �������� ������� INSUR_POLICY_SVD
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_POLICY_SVD')) then
	 execute 'create table INSUR_POLICY_SVD
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_POLICY_SVD
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_POLICY_SVD')) then
    execute 'alter table INSUR_POLICY_SVD add constraint REG_309_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 310, Insur.AllProperty (������ ��������� ����������� ������ ���������)-- ������ ����������� ������:
-- INSUR_ALL_PROPERTY
--<DO>--
-- �������� ������� INSUR_ALL_PROPERTY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_ALL_PROPERTY')) then
	 execute 'create table INSUR_ALL_PROPERTY
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_ALL_PROPERTY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_ALL_PROPERTY')) then
    execute 'alter table INSUR_ALL_PROPERTY add constraint REG_310_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 311, Insur.DopAllProperty (������ ���. ���������� �� ��������� ������ ���������)-- ������ ����������� ������:
-- INSUR_DOP_ALL_PROPERTY
--<DO>--
-- �������� ������� INSUR_DOP_ALL_PROPERTY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DOP_ALL_PROPERTY')) then
	 execute 'create table INSUR_DOP_ALL_PROPERTY
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_DOP_ALL_PROPERTY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DOP_ALL_PROPERTY')) then
    execute 'alter table INSUR_DOP_ALL_PROPERTY add constraint REG_311_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 312, Insur.ParamCalculation (������ �������� ���������� �������� ������ ���������)-- ������ ����������� ������:
-- INSUR_PARAM_CALCULATION
--<DO>--
-- �������� ������� INSUR_PARAM_CALCULATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_PARAM_CALCULATION')) then
	 execute 'create table INSUR_PARAM_CALCULATION
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_PARAM_CALCULATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_PARAM_CALCULATION')) then
    execute 'alter table INSUR_PARAM_CALCULATION add constraint REG_312_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 313, Insur.Damage (������ ��� �� �������  ����� ������)-- ������ ����������� ������:
-- INSUR_DAMAGE
--<DO>--
-- �������� ������� INSUR_DAMAGE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DAMAGE')) then
	 execute 'create table INSUR_DAMAGE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_DAMAGE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DAMAGE')) then
    execute 'alter table INSUR_DAMAGE add constraint REG_313_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 314, Insur.PayTo (������ ��������� ������)-- ������ ����������� ������:
-- INSUR_PAY_TO
--<DO>--
-- �������� ������� INSUR_PAY_TO
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_PAY_TO')) then
	 execute 'create table INSUR_PAY_TO
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_PAY_TO
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_PAY_TO')) then
    execute 'alter table INSUR_PAY_TO add constraint REG_314_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 315, Insur.NoPay (������ �������� �� ������� � ��������� ��������)-- ������ ����������� ������:
-- INSUR_NO_PAY
--<DO>--
-- �������� ������� INSUR_NO_PAY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_NO_PAY')) then
	 execute 'create table INSUR_NO_PAY
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_NO_PAY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_NO_PAY')) then
    execute 'alter table INSUR_NO_PAY add constraint REG_315_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 319, Insur.Address (������ �������)-- ������ ����������� ������:
-- INSUR_ADDRESS
--<DO>--
-- �������� ������� INSUR_ADDRESS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_ADDRESS')) then
	 execute 'create table INSUR_ADDRESS
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_ADDRESS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_ADDRESS')) then
    execute 'alter table INSUR_ADDRESS add constraint REG_319_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 322, Insur.FileStorage (��������� ������)-- ������ ����������� ������:
-- INSUR_FILE_STORAGE
--<DO>--
-- �������� ������� INSUR_FILE_STORAGE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FILE_STORAGE')) then
	 execute 'create table INSUR_FILE_STORAGE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_FILE_STORAGE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FILE_STORAGE')) then
    execute 'alter table INSUR_FILE_STORAGE add constraint CORE_FILE_STORAGE_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 323, Insur.DocBaseType (���������� ���� ����������-���������)-- ������ ����������� ������:
-- INSUR_DOC_BASE_TYPE
--<DO>--
-- �������� ������� INSUR_DOC_BASE_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DOC_BASE_TYPE')) then
	 execute 'create table INSUR_DOC_BASE_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_DOC_BASE_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DOC_BASE_TYPE')) then
    execute 'alter table INSUR_DOC_BASE_TYPE add constraint INSUR_DOC_BASE_TYPE_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 324, Insur.DamageAssessmentMethod (�������� ������ ������)-- ������ ����������� ������:
-- INSUR_DAMAGE_ASSESSMENT_METHOD
--<DO>--
-- �������� ������� INSUR_DAMAGE_ASSESSMENT_METHOD
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DAMAGE_ASSESSMENT_METHOD')) then
	 execute 'create table INSUR_DAMAGE_ASSESSMENT_METHOD
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_DAMAGE_ASSESSMENT_METHOD
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DAMAGE_ASSESSMENT_METHOD')) then
    execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add constraint INSURE_DAMAGE_ASSESSMENT_METHOD_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 325, Insur.InputFilePackage (������ ����������� �������)-- ������ ����������� ������:
-- INSUR_INPUT_FILE_PACKAGE
--<DO>--
-- �������� ������� INSUR_INPUT_FILE_PACKAGE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INPUT_FILE_PACKAGE')) then
	 execute 'create table INSUR_INPUT_FILE_PACKAGE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_INPUT_FILE_PACKAGE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INPUT_FILE_PACKAGE')) then
    execute 'alter table INSUR_INPUT_FILE_PACKAGE add constraint INSUR_INPUT_FILE_PACKAGE_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 326, Insur.LinkBuildBti   (������ ����� ������� ����������� ��� � ��������� ���)-- ������ ����������� ������:
-- INSUR_LINK_BUILD_BTI
--<DO>--
-- �������� ������� INSUR_LINK_BUILD_BTI
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LINK_BUILD_BTI')) then
	 execute 'create table INSUR_LINK_BUILD_BTI
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_LINK_BUILD_BTI
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LINK_BUILD_BTI')) then
    execute 'alter table INSUR_LINK_BUILD_BTI add constraint REG_326_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 327, Insur.LinkFlatEgrn (������ ����� ����� �������� ����������� �� � ����������� � ����������)-- ������ ����������� ������:
-- INSUR_LINK_FLAT_EGRN
--<DO>--
-- �������� ������� INSUR_LINK_FLAT_EGRN
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LINK_FLAT_EGRN')) then
	 execute 'create table INSUR_LINK_FLAT_EGRN
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_LINK_FLAT_EGRN
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LINK_FLAT_EGRN')) then
    execute 'alter table INSUR_LINK_FLAT_EGRN add constraint REG_327_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 253, Bti.Floor (������ ����� ����� �������� ����������� �� � ����������� � ����������)-- ������ ����������� ������:
-- BTI_FLOOR_O
-- BTI_FLOOR_Q
--<DO>--
-- �������� ������� BTI_FLOOR_O
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


-- �������� ����������� ������� BTI_FLOOR_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_FLOOR_O')) then
   execute 'alter table BTI_FLOOR_O add constraint REG_253_OBJECT_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� BTI_FLOOR_Q
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

-- �������� ����������� ������� BTI_FLOOR_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_FLOOR_Q')) then
	execute 'alter table BTI_FLOOR_Q add constraint REG_253_QUANT_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_253_QUANT_FK_O')) then
	execute 'alter table BTI_FLOOR_Q add constraint REG_253_QUANT_FK_O foreign key (EMP_ID) references BTI_FLOOR_O (ID)';
  end if;


-- �������� �������� ������� BTI_FLOOR_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_253_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_253_QUANT_INX_EMP_ID on BTI_FLOOR_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_253_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_253_QUANT_INX_S_PO_ ON BTI_FLOOR_Q (S_, PO_)';
  end if;
end $$;
-- ��������� �������: 257, Bti.Rooms (������ ������ ���)-- ������ ����������� ������:
-- BTI_ROOMS
--<DO>--
-- �������� ������� BTI_ROOMS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_ROOMS')) then
	 execute 'create table BTI_ROOMS
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� BTI_ROOMS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ROOMS')) then
    execute 'alter table BTI_ROOMS add constraint REG_257_QUANT_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 328, Insur.InsuranceOrganization (���������� ���������� �����������)-- ������ ����������� ������:
-- INSUR_INSURANCE_ORGANIZATION
--<DO>--
-- �������� ������� INSUR_INSURANCE_ORGANIZATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INSURANCE_ORGANIZATION')) then
	 execute 'create table INSUR_INSURANCE_ORGANIZATION
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_INSURANCE_ORGANIZATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INSURANCE_ORGANIZATION')) then
    execute 'alter table INSUR_INSURANCE_ORGANIZATION add constraint INSUR_INSURANCE_ORGANIZATION_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 254, Bti.Premase (������ ��������� ���)-- ������ ����������� ������:
-- BTI_PREMASE
--<DO>--
-- �������� ������� BTI_PREMASE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('BTI_PREMASE')) then
	 execute 'create table BTI_PREMASE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� BTI_PREMASE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_PREMASE')) then
    execute 'alter table BTI_PREMASE add constraint REG_254_QUANT_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 332, Insur.FlatStatus (���������� "������ �������� /����")-- ������ ����������� ������:
-- INSUR_FLAT_STATUS
--<DO>--
-- �������� ������� INSUR_FLAT_STATUS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FLAT_STATUS')) then
	 execute 'create table INSUR_FLAT_STATUS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_FLAT_STATUS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FLAT_STATUS')) then
    execute 'alter table INSUR_FLAT_STATUS add constraint INSUR_FLAT_STATUS_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 333, Insur.FlatType (���������� ��� ������ ���������)-- ������ ����������� ������:
-- INSUR_FLAT_TYPE
--<DO>--
-- �������� ������� INSUR_FLAT_TYPE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FLAT_TYPE')) then
	 execute 'create table INSUR_FLAT_TYPE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_FLAT_TYPE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FLAT_TYPE')) then
    execute 'alter table INSUR_FLAT_TYPE add constraint FLAT_TYPE_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 330, Insur.BaseTariff (���������� �������� ������)-- ������ ����������� ������:
-- INSUR_BASE_TARIFF
--<DO>--
-- �������� ������� INSUR_BASE_TARIFF
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BASE_TARIFF')) then
	 execute 'create table INSUR_BASE_TARIFF
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_BASE_TARIFF
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BASE_TARIFF')) then
    execute 'alter table INSUR_BASE_TARIFF add constraint REG_330_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 329, Insur.PartCompensation (���������� ����� ��������������� �ʻ)-- ������ ����������� ������:
-- INSUR_PART_COMPENSATION
--<DO>--
-- �������� ������� INSUR_PART_COMPENSATION
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_PART_COMPENSATION')) then
	 execute 'create table INSUR_PART_COMPENSATION
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_PART_COMPENSATION
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_PART_COMPENSATION')) then
    execute 'alter table INSUR_PART_COMPENSATION add constraint REG_329_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 334, Insur.AgreementProject (������ �������� ��������� �����������)-- ������ ����������� ������:
-- INSUR_AGREEMENT_PROJECT
--<DO>--
-- �������� ������� INSUR_AGREEMENT_PROJECT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_AGREEMENT_PROJECT')) then
	 execute 'create table INSUR_AGREEMENT_PROJECT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_AGREEMENT_PROJECT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_AGREEMENT_PROJECT')) then
    execute 'alter table INSUR_AGREEMENT_PROJECT add constraint REG_334_PK primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 344, Insur.Bank (���������� ������)-- ������ ����������� ������:
-- INSUR_BANK
--<DO>--
-- �������� ������� INSUR_BANK
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_BANK')) then
	 execute 'create table INSUR_BANK
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_BANK
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BANK')) then
    execute 'alter table INSUR_BANK add constraint INSUR_BANK_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 350, Insur.DamageAmount (������ �������� ������ �� ��������� �����������)-- ������ ����������� ������:
-- INSUR_DAMAGE_AMOUNT
--<DO>--
-- �������� ������� INSUR_DAMAGE_AMOUNT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DAMAGE_AMOUNT')) then
	 execute 'create table INSUR_DAMAGE_AMOUNT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_DAMAGE_AMOUNT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DAMAGE_AMOUNT')) then
    execute 'alter table INSUR_DAMAGE_AMOUNT add constraint INSUR_DAMAGE_AMOUNT_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 340, Insur.Documents (������ ����������-��������� ���)-- ������ ����������� ������:
-- INSUR_DOCUMENTS
--<DO>--
-- �������� ������� INSUR_DOCUMENTS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_DOCUMENTS')) then
	 execute 'create table INSUR_DOCUMENTS
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_DOCUMENTS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_DOCUMENTS')) then
    execute 'alter table INSUR_DOCUMENTS add constraint INSUR_DOCUNENTS_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 346, Insur.InsurRate (���������� ���������� ������)-- ������ ����������� ������:
-- INSUR_INSUR_RATE
--<DO>--
-- �������� ������� INSUR_INSUR_RATE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INSUR_RATE')) then
	 execute 'create table INSUR_INSUR_RATE
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_INSUR_RATE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INSUR_RATE')) then
    execute 'alter table INSUR_INSUR_RATE add constraint INSUR_INSUR_RATE_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 347, Insur.CommonPropertyTariff (���������� "������ �� ����������� ������ ���������")-- ������ ����������� ������:
-- INSUR_COMMON_PROPERTY_TARIFF
--<DO>--
-- �������� ������� INSUR_COMMON_PROPERTY_TARIFF
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_COMMON_PROPERTY_TARIFF')) then
	 execute 'create table INSUR_COMMON_PROPERTY_TARIFF
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_COMMON_PROPERTY_TARIFF
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_COMMON_PROPERTY_TARIFF')) then
    execute 'alter table INSUR_COMMON_PROPERTY_TARIFF add constraint INSUR_COMMON_PROPERTY_TARIFF_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 348, Insur.LivingPremiseInsurCost (���������� ���������� ��������� �ϻ)-- ������ ����������� ������:
-- INSUR_LIVING_PREMISE_INSUR_COST
--<DO>--
-- �������� ������� INSUR_LIVING_PREMISE_INSUR_COST
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LIVING_PREMISE_INSUR_COST')) then
	 execute 'create table INSUR_LIVING_PREMISE_INSUR_COST
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_LIVING_PREMISE_INSUR_COST
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LIVING_PREMISE_INSUR_COST')) then
    execute 'alter table INSUR_LIVING_PREMISE_INSUR_COST add constraint INSUR_LIVING_PREMISE_INSUR_COST_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 349, Insur.ShareResponsibilityICCity (���������� "���� ��������������� �� � ������")-- ������ ����������� ������:
-- INSUR_SHARE_RESPONSIBILITY_IC_CITY
--<DO>--
-- �������� ������� INSUR_SHARE_RESPONSIBILITY_IC_CITY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_SHARE_RESPONSIBILITY_IC_CITY')) then
	 execute 'create table INSUR_SHARE_RESPONSIBILITY_IC_CITY
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_SHARE_RESPONSIBILITY_IC_CITY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_SHARE_RESPONSIBILITY_IC_CITY')) then
    execute 'alter table INSUR_SHARE_RESPONSIBILITY_IC_CITY add constraint INSUR_SHARE_RESPONSIBILITY_IC_CITY_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 809, Fm.Reports.SavedReport (����������� ������)-- ������ ����������� ������:
-- FM_REPORTS_SAVEDREPORT
--<DO>--
-- �������� ������� FM_REPORTS_SAVEDREPORT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('FM_REPORTS_SAVEDREPORT')) then
	 execute 'create table FM_REPORTS_SAVEDREPORT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� FM_REPORTS_SAVEDREPORT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('FM_REPORTS_SAVEDREPORT')) then
    execute 'alter table FM_REPORTS_SAVEDREPORT add constraint FM_REPORTS_SAVEDREPORT_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 331, Insur.IntegrateIndicatorsReplecmentCost (���������� ����������� ����������� ��������� ���� ����������������� ��������� �������������� ��������� ������ ���������)-- ������ ����������� ������:
-- INSUR_INTEGRATED_INDICATORS_REPL_COST
--<DO>--
-- �������� ������� INSUR_INTEGRATED_INDICATORS_REPL_COST
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INTEGRATED_INDICATORS_REPL_COST')) then
	 execute 'create table INSUR_INTEGRATED_INDICATORS_REPL_COST
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_INTEGRATED_INDICATORS_REPL_COST
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INTEGRATED_INDICATORS_REPL_COST')) then
    execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add constraint INSUR_INTEGRATED_INDICATORS_REPLEMENT_COST_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 351, Insur.Tariff (���������� ���������� ������)-- ������ ����������� ������:
-- INSUR_TARIFF
--<DO>--
-- �������� ������� INSUR_TARIFF
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_TARIFF')) then
	 execute 'create table INSUR_TARIFF
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_TARIFF
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_TARIFF')) then
    execute 'alter table INSUR_TARIFF add constraint INSUR_TARIFF_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 353, Insur.ActualCostRatio (���������� "����������� ��������� �������������� ���������")-- ������ ����������� ������:
-- INSUR_ACTUAL_COST_RATIO
--<DO>--
-- �������� ������� INSUR_ACTUAL_COST_RATIO
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_ACTUAL_COST_RATIO')) then
	 execute 'create table INSUR_ACTUAL_COST_RATIO
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_ACTUAL_COST_RATIO
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_ACTUAL_COST_RATIO')) then
    execute 'alter table INSUR_ACTUAL_COST_RATIO add constraint INSUR_ACTUAL_COST_RATIO_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 354, Insur.ReestrPay (������ ����� � ������� ���)-- ������ ����������� ������:
-- INSUR_REESTR_PAY
--<DO>--
-- �������� ������� INSUR_REESTR_PAY
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_REESTR_PAY')) then
	 execute 'create table INSUR_REESTR_PAY
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_REESTR_PAY
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_REESTR_PAY')) then
    execute 'alter table INSUR_REESTR_PAY add constraint INSUR_REESTR_PAY_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 355, Insur.Invoice (������ ������)-- ������ ����������� ������:
-- INSUR_INVOICE
--<DO>--
-- �������� ������� INSUR_INVOICE
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_INVOICE')) then
	 execute 'create table INSUR_INVOICE
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_INVOICE
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_INVOICE')) then
    execute 'alter table INSUR_INVOICE add constraint INSUR_INVOICE_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 251, Bti.BtiBuilding (������ ������ ���)-- ������ ����������� ������:
-- BTI_BUILDING_O
-- BTI_BUILDING_Q
--<DO>--
-- �������� ������� BTI_BUILDING_O
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


-- �������� ����������� ������� BTI_BUILDING_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_BUILDING_O')) then
   execute 'alter table BTI_BUILDING_O add constraint REG_251_OBJ_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� BTI_BUILDING_Q
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

-- �������� ����������� ������� BTI_BUILDING_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_BUILDING_Q')) then
	execute 'alter table BTI_BUILDING_Q add constraint REG_251_QUANT_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_251_QUANT_FK_O')) then
	execute 'alter table BTI_BUILDING_Q add constraint REG_251_QUANT_FK_O foreign key (EMP_ID) references BTI_BUILDING_O (ID)';
  end if;


-- �������� �������� ������� BTI_BUILDING_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_251_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_251_QUANT_INX_EMP_ID on BTI_BUILDING_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_251_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_251_QUANT_INX_S_PO_ ON BTI_BUILDING_Q (S_, PO_)';
  end if;
end $$;
-- ��������� �������: 358, Insur.Comment (������ "�����������")-- ������ ����������� ������:
-- INSUR_COMMENT
--<DO>--
-- �������� ������� INSUR_COMMENT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_COMMENT')) then
	 execute 'create table INSUR_COMMENT
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_COMMENT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_COMMENT')) then
    execute 'alter table INSUR_COMMENT add constraint INSUR_COMMENT_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 357, Insur.GbuNoPayReason (���������� "������� ������ � ������� ������ ���")-- ������ ����������� ������:
-- INSUR_GBU_NO_PAY_REASON
--<DO>--
-- �������� ������� INSUR_GBU_NO_PAY_REASON
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_GBU_NO_PAY_REASON')) then
	 execute 'create table INSUR_GBU_NO_PAY_REASON
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_GBU_NO_PAY_REASON
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_GBU_NO_PAY_REASON')) then
    execute 'alter table INSUR_GBU_NO_PAY_REASON add constraint INSUR_GBU_NO_PAY_REASON_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 356, Insur.LinkCausesSubreasonLP (������ ����� ���������� ������ ������ � �������� ��� ��)-- ������ ����������� ������:
-- INSUR_LINK_CAUSES_SUBREASON_LP
--<DO>--
-- �������� ������� INSUR_LINK_CAUSES_SUBREASON_LP
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_LINK_CAUSES_SUBREASON_LP')) then
	 execute 'create table INSUR_LINK_CAUSES_SUBREASON_LP
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_LINK_CAUSES_SUBREASON_LP
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_LINK_CAUSES_SUBREASON_LP')) then
    execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add constraint INSUR_LINK_CAUSES_SUBREASON_LP_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 360, ImportLog.InsurBuildingLog (������ ������������ �������� ����������� ��� ������)-- ������ ����������� ������:
-- IMPORT_LOG_INSUR_BUILDING
--<DO>--
-- �������� ������� IMPORT_LOG_INSUR_BUILDING
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('IMPORT_LOG_INSUR_BUILDING')) then
	 execute 'create table IMPORT_LOG_INSUR_BUILDING
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� IMPORT_LOG_INSUR_BUILDING
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('IMPORT_LOG_INSUR_BUILDING')) then
    execute 'alter table IMPORT_LOG_INSUR_BUILDING add constraint IMPORT_LOG_INSUR_BUILDING_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 401, Ehd.Register (ehd.register)-- ������ ����������� ������:
-- EHD_REGISTER_O
-- EHD_REGISTER_Q
--<DO>--
-- �������� ������� EHD_REGISTER_O
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


-- �������� ����������� ������� EHD_REGISTER_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_REGISTER_O')) then
   execute 'alter table EHD_REGISTER_O add constraint REGISTR_FROM_EHD_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� EHD_REGISTER_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_REGISTER_Q')) then
	 execute 'create table EHD_REGISTER_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� EHD_REGISTER_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_REGISTER_Q')) then
    execute 'alter table EHD_REGISTER_Q add constraint EHD_REGISTER_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 402, Ehd.Location (ehd.location)-- ������ ����������� ������:
-- EHD_LOCATION_O
-- EHD_LOCATION_Q
--<DO>--
-- �������� ������� EHD_LOCATION_O
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


-- �������� ����������� ������� EHD_LOCATION_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_LOCATION_O')) then
   execute 'alter table EHD_LOCATION_O add constraint LOCATION_FROM_EHD_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� EHD_LOCATION_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_LOCATION_Q')) then
	 execute 'create table EHD_LOCATION_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� EHD_LOCATION_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_LOCATION_Q')) then
    execute 'alter table EHD_LOCATION_Q add constraint EHD_LOCATION_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 405, Ehd.Egrp (EHD.EGRP)-- ������ ����������� ������:
-- EHD_EGRP_O
-- EHD_EGRP_Q
--<DO>--
-- �������� ������� EHD_EGRP_O
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


-- �������� ����������� ������� EHD_EGRP_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_EGRP_O')) then
   execute 'alter table EHD_EGRP_O add constraint EHD_EGRN_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� EHD_EGRP_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_EGRP_Q')) then
	 execute 'create table EHD_EGRP_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� EHD_EGRP_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_EGRP_Q')) then
    execute 'alter table EHD_EGRP_Q add constraint EHD_EGRN_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 406, Ehd.Right (EHD.RIGHT)-- ������ ����������� ������:
-- EHD_RIGHT_O
-- EHD_RIGHT_Q
--<DO>--
-- �������� ������� EHD_RIGHT_O
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


-- �������� ����������� ������� EHD_RIGHT_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_RIGHT_O')) then
   execute 'alter table EHD_RIGHT_O add constraint EHD_RIGHT_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� EHD_RIGHT_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_RIGHT_Q')) then
	 execute 'create table EHD_RIGHT_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� EHD_RIGHT_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_RIGHT_Q')) then
    execute 'alter table EHD_RIGHT_Q add constraint EHD_RIGHT_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 407, Ehd.OldNumber (EHD.OLD_NUMBERS)-- ������ ����������� ������:
-- EHD_OLD_NUMBERS
--<DO>--
-- �������� ������� EHD_OLD_NUMBERS
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_OLD_NUMBERS')) then
	 execute 'create table EHD_OLD_NUMBERS
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� EHD_OLD_NUMBERS
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_OLD_NUMBERS')) then
    execute 'alter table EHD_OLD_NUMBERS add constraint EHD_OLD_NUMBERS_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 359, Insur.FilePlatIdentifyLog (������ ������������� ����������)-- ������ ����������� ������:
-- INSUR_FILE_PLAT_IDENTIFY_LOG
--<DO>--
-- �������� ������� INSUR_FILE_PLAT_IDENTIFY_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FILE_PLAT_IDENTIFY_LOG')) then
	 execute 'create table INSUR_FILE_PLAT_IDENTIFY_LOG
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_FILE_PLAT_IDENTIFY_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FILE_PLAT_IDENTIFY_LOG')) then
    execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add constraint INSUR_FILE_PLAT_IDENTIFY_LOG_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 345, Insur.Subject (���������� "����������� ��������")-- ������ ����������� ������:
-- INSUR_SUBJECT
--<DO>--
-- �������� ������� INSUR_SUBJECT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_SUBJECT')) then
	 execute 'create table INSUR_SUBJECT
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_SUBJECT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_SUBJECT')) then
    execute 'alter table INSUR_SUBJECT add constraint INSUR_SUBJECT_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 352, Insur.ChangesLog (������ ����������� ��������� ������)-- ������ ����������� ������:
-- INSUR_CHANGES_LOG
--<DO>--
-- �������� ������� INSUR_CHANGES_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_CHANGES_LOG')) then
	 execute 'create table INSUR_CHANGES_LOG
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_CHANGES_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_CHANGES_LOG')) then
    execute 'alter table INSUR_CHANGES_LOG add constraint INSUR_CHANGES_LOG_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 258, Bti.BtiOkrug (������ ������� ���)-- ������ ����������� ������:
-- REF_ADDR_OKRUG
--<DO>--
-- �������� ������� REF_ADDR_OKRUG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('REF_ADDR_OKRUG')) then
	 execute 'create table REF_ADDR_OKRUG
						(
						  OKRUG_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� REF_ADDR_OKRUG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('REF_ADDR_OKRUG')) then
    execute 'alter table REF_ADDR_OKRUG add constraint REF_ADDR_OKRUG_PKEY primary key (OKRUG_ID)';
  end if;
end $$;
-- ��������� �������: 259, Bti.BtiDistrict (������ ������� ���)-- ������ ����������� ������:
-- REF_ADDR_DISTRICT
--<DO>--
-- �������� ������� REF_ADDR_DISTRICT
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('REF_ADDR_DISTRICT')) then
	 execute 'create table REF_ADDR_DISTRICT
						(
						  DISTRICT_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� REF_ADDR_DISTRICT
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('REF_ADDR_DISTRICT')) then
    execute 'alter table REF_ADDR_DISTRICT add constraint REF_ADDR_DISTRICT_PKEY primary key (DISTRICT_ID)';
  end if;
end $$;
-- ��������� �������: 368, Insur.TypeBuldingFloorLink (������ ������ ����� ������ � ���������� � ����� �����������)-- ������ ����������� ������:
-- INSUR_TYPE_BUILDING_FLOOR_LINK
--<DO>--
-- �������� ������� INSUR_TYPE_BUILDING_FLOOR_LINK
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_TYPE_BUILDING_FLOOR_LINK')) then
	 execute 'create table INSUR_TYPE_BUILDING_FLOOR_LINK
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_TYPE_BUILDING_FLOOR_LINK
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_TYPE_BUILDING_FLOOR_LINK')) then
    execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add constraint INSUR_TYPE_BUILDING_FLOOR_LINK_PKEY primary key (ID)';
  end if;
end $$;
-- ��������� �������: 361, ImportLog.InsurFlatBuildingLog (������ ������������ ��������� �����������)-- ������ ����������� ������:
-- IMPORT_LOG_INSUR_FLAT_B
--<DO>--
-- �������� ������� IMPORT_LOG_INSUR_FLAT_B
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('IMPORT_LOG_INSUR_FLAT_B')) then
	 execute 'create table IMPORT_LOG_INSUR_FLAT_B
						(
						  ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� IMPORT_LOG_INSUR_FLAT_B
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('IMPORT_LOG_INSUR_FLAT_B')) then
    execute 'alter table IMPORT_LOG_INSUR_FLAT_B add constraint  primary key (ID)';
  end if;
end $$;
-- ��������� �������: 370, Insur.FileProcessLog (������ �������� ��������� ������ ���)-- ������ ����������� ������:
-- INSUR_FILE_PROCESS_LOG
--<DO>--
-- �������� ������� INSUR_FILE_PROCESS_LOG
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('INSUR_FILE_PROCESS_LOG')) then
	 execute 'create table INSUR_FILE_PROCESS_LOG
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� INSUR_FILE_PROCESS_LOG
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FILE_PROCESS_LOG')) then
    execute 'alter table INSUR_FILE_PROCESS_LOG add constraint INSUR_FILE_PROCESS_LOG_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 316, Insur.Building (������ �������� ����������� ���)-- ������ ����������� ������:
-- INSUR_BUILDING_O
-- INSUR_BUILDING_Q
--<DO>--
-- �������� ������� INSUR_BUILDING_O
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


-- �������� ����������� ������� INSUR_BUILDING_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BUILDING_O')) then
   execute 'alter table INSUR_BUILDING_O add constraint INSUR_BUILDING_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� INSUR_BUILDING_Q
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

-- �������� ����������� ������� INSUR_BUILDING_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_BUILDING_Q')) then
	execute 'alter table INSUR_BUILDING_Q add constraint INSUR_BUILDING_Q_KEY primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_316_QUANT_FK_O')) then
	execute 'alter table INSUR_BUILDING_Q add constraint REG_316_QUANT_FK_O foreign key (EMP_ID) references INSUR_BUILDING_O (ID)';
  end if;


-- �������� �������� ������� INSUR_BUILDING_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_316_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_316_QUANT_INX_EMP_ID on INSUR_BUILDING_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_316_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_316_QUANT_INX_S_PO_ ON INSUR_BUILDING_Q (S_, PO_)';
  end if;
end $$;
-- ��������� �������: 317, Insur.Flat (������ �������� ����������� ����� ���������)-- ������ ����������� ������:
-- INSUR_FLAT_O
-- INSUR_FLAT_Q
--<DO>--
-- �������� ������� INSUR_FLAT_O
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


-- �������� ����������� ������� INSUR_FLAT_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FLAT_O')) then
   execute 'alter table INSUR_FLAT_O add constraint INSUR_FLAT_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� INSUR_FLAT_Q
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

-- �������� ����������� ������� INSUR_FLAT_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('INSUR_FLAT_Q')) then
	execute 'alter table INSUR_FLAT_Q add constraint INSUR_FLAT_Q_KEY primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_317_QUANT_FK_O')) then
	execute 'alter table INSUR_FLAT_Q add constraint REG_317_QUANT_FK_O foreign key (EMP_ID) references INSUR_FLAT_O (ID)';
  end if;


-- �������� �������� ������� INSUR_FLAT_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_317_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_317_QUANT_INX_EMP_ID on INSUR_FLAT_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_317_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_317_QUANT_INX_S_PO_ ON INSUR_FLAT_Q (S_, PO_)';
  end if;
end $$;
-- ��������� �������: 400, Ehd.BuildParcel (������� ����)-- ������ ����������� ������:
-- EHD_BUILD_PARCEL_O
-- EHD_BUILD_PARCEL_Q
--<DO>--
-- �������� ������� EHD_BUILD_PARCEL_O
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


-- �������� ����������� ������� EHD_BUILD_PARCEL_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_BUILD_PARCEL_O')) then
   execute 'alter table EHD_BUILD_PARCEL_O add constraint BUILD_PARCEL_FROM_EHD_O_PKEY primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� EHD_BUILD_PARCEL_Q
DO $$
begin
  if (not CORE_UPDSTRU_CheckExistTable('EHD_BUILD_PARCEL_Q')) then
	 execute 'create table EHD_BUILD_PARCEL_Q
						(
						  EMP_ID  numeric(10) not null
						  
						)';
  end if;


-- �������� ����������� ������� EHD_BUILD_PARCEL_Q
  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('EHD_BUILD_PARCEL_Q')) then
    execute 'alter table EHD_BUILD_PARCEL_Q add constraint EHD_BUILD_PARCEL_Q_PKEY primary key (EMP_ID)';
  end if;
end $$;
-- ��������� �������: 50, Bti.ADDRESS (������ ������� ���)-- ������ ����������� ������:
-- BTI_ADDRESS_O
-- BTI_ADDRESS_Q
--<DO>--
-- �������� ������� BTI_ADDRESS_O
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


-- �������� ����������� ������� BTI_ADDRESS_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ADDRESS_O')) then
   execute 'alter table BTI_ADDRESS_O add constraint REG_50_OBJECT_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� BTI_ADDRESS_Q
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

-- �������� ����������� ������� BTI_ADDRESS_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ADDRESS_Q')) then
	execute 'alter table BTI_ADDRESS_Q add constraint REG_50_QUANT_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_50_QUANT_FK_O')) then
	execute 'alter table BTI_ADDRESS_Q add constraint REG_50_QUANT_FK_O foreign key (EMP_ID) references BTI_ADDRESS_O (ID)';
  end if;


-- �������� �������� ������� BTI_ADDRESS_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_50_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_50_QUANT_INX_EMP_ID on BTI_ADDRESS_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_50_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_50_QUANT_INX_S_PO_ ON BTI_ADDRESS_Q (S_, PO_)';
  end if;
end $$;
-- ��������� �������: 52, Bti.ADDRLINK (������ ����� ������ ��� � �������)-- ������ ����������� ������:
-- BTI_ADDRLINK_O
-- BTI_ADDRLINK_Q
--<DO>--
-- �������� ������� BTI_ADDRLINK_O
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


-- �������� ����������� ������� BTI_ADDRLINK_O

 if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ADDRLINK_O')) then
   execute 'alter table BTI_ADDRLINK_O add constraint REG_52_OBJECT_PK primary key (ID)';
 end if;
end $$;
--<DO>--
-- �������� ������� BTI_ADDRLINK_Q
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

-- �������� ����������� ������� BTI_ADDRLINK_Q

  if (not CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY('BTI_ADDRLINK_Q')) then
	execute 'alter table BTI_ADDRLINK_Q add constraint REG_52_QUANT_PK primary key (ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistConstraint('REG_52_QUANT_FK_O')) then
	execute 'alter table BTI_ADDRLINK_Q add constraint REG_52_QUANT_FK_O foreign key (EMP_ID) references BTI_ADDRLINK_O (ID)';
  end if;


-- �������� �������� ������� BTI_ADDRLINK_Q

  if (not CORE_UPDSTRU_CheckExistIndex('REG_52_QUANT_INX_EMP_ID')) then
	execute 'CREATE INDEX REG_52_QUANT_INX_EMP_ID on BTI_ADDRLINK_Q (EMP_ID)';
  end if;

  if (not CORE_UPDSTRU_CheckExistIndex('REG_52_QUANT_INX_S_PO_')) then
	execute 'CREATE INDEX REG_52_QUANT_INX_S_PO_ ON BTI_ADDRLINK_Q (S_, PO_)';
  end if;
end $$;
