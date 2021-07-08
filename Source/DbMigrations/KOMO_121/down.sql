delete from core_reference where referenceid=227;
update core_register_attribute set referenceid=600 where id=26400300;


INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) 
VALUES (1002652, 1000264, 0, 1, 26400200, null, null, '���', null, 1, null, 4, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);


INSERT INTO core_layout (layoutid, layoutname, layoutcomment, registerid, defaultsort, preffered, username, createdate, qsquery, isdistinct, ordertype, user_id, iscommon, internal_name, enable_minicards_mode, register_view_id, as_domain_id, column_width_type, is_using_extended_editor) 
VALUES (1000264, '�������� ��������� ��� ������������� ������������ �������������', '������ ��������� ��������� �� ��������� � ����������� ������������ � ���������� ����� ������ �������', 264, null, null, null, '2020-10-08 12:59:12.000000', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>264</MainRegisterID>
  <TDInstanceID>0</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType xsi:nil="true" />
  <DefaultAlias>false</DefaultAlias>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, null, null, 1, null, 0, 'ModelingDictionaries', null, null, 0);





INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) 
VALUES (208, 'KO.GroupFactor', '������� ������', null, null, 'KO_GROUP_FACTOR', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, 0, null, null);

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (20800100, '�������������', 208, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, 0, null, null, 0);

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (20800200, '������������� ������', 208, 1, null, null, 'GROUP_ID', null, null, null, null, null, 'GroupId', 1, null, null, null, 0, null, null, 0);

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (20800300, '������������� �������', 208, 1, null, null, 'FACTOR_ID', null, null, null, null, null, 'FactorId', 1, null, null, null, 0, null, null, 0);

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (20800400, '������� ������������� �����', 208, 3, null, null, 'SIGN_NARKET', null, null, null, null, null, 'SignMarket', 1, null, null, null, 0, null, null, 0);

create table ko_group_factor
(
    id          bigint not null
        constraint reg_208_q_pk
            primary key,
    group_id    bigint not null,
    factor_id   bigint not null,
    sign_narket smallint
);

alter table ko_group_factor
    owner to postgres;

