--Реестр под файлы выгрузок результатов оценки
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register)
VALUES (267, 'KO.UnloadResultFiles', 'Реестр с файлами выгрузок результатов оценки', null, null, 'KO_UNLOAD_RESULT_FILES', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
drop table if exists ko_unload_result_files;
create table if not exists ko_unload_result_files (
                                                      id                  bigint       not null
                                                          constraint reg_267_q_pk
                                                              primary key,
                                                      unload_id           bigint       not null,
                                                      unload_type         bigint       not null,
                                                      date_created        timestamp    not null,
                                                      file_name           varchar(512),
                                                      file_extension      varchar(512)
);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (26700100, 'Идентификатор', 267, 1, null, null, 'ID', null, null, 1, null, null, 'Id', null, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (26700200, 'Идентификатор выгрузки', 267, 1, null, null, 'UNLOAD_ID', null, null, null, null, null, 'UnloadId', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (26700300, 'Тип выгрузки', 267, 4, null, 220, null, 'UNLOAD_TYPE', null, null, null, null, 'UnloadType', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (26700400, 'Дата создания', 267, 5, null, null, 'DATE_CREATED', null, null, null, null, null, 'DateCreated', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (26700600, 'Имя файла', 267, 4, null, null, 'FILE_NAME', null, null, null, null, null, 'FileName', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (26700700, 'Расширение файла', 267, 4, null, null, 'FILE_EXTENSION', null, null, null, null, null, 'FileExtension', 0, null, null, null, 0, null, null, 0);

INSERT INTO core_layout (layoutid, layoutname, layoutcomment, registerid, defaultsort, preffered, username, createdate, qsquery, isdistinct, ordertype, user_id, iscommon, internal_name, enable_minicards_mode, register_view_id, as_domain_id, column_width_type, is_using_extended_editor) VALUES (1000262, 'История выгрузки результатов оценки', null, 262, null, null, null, '2021-03-15 17:53:43.066655', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>262</MainRegisterID>
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
  <AddPKColumn>true</AddPKColumn>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, null, 2, 1, null, 0, 'UnloadResultsQueue', null, null, 0);
INSERT INTO core_layout (layoutid, layoutname, layoutcomment, registerid, defaultsort, preffered, username, createdate, qsquery, isdistinct, ordertype, user_id, iscommon, internal_name, enable_minicards_mode, register_view_id, as_domain_id, column_width_type, is_using_extended_editor) VALUES (1000267, 'Раскладка для файлов в выгрузке результатов оценки', null, 267, null, null, null, '2021-03-15 17:54:59.263892', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>267</MainRegisterID>
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
  <RegisterLinks />
  <JoinType xsi:nil="true" />
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <ExcludeLinks />
  <DefaultAlias>false</DefaultAlias>
  <AddPKColumn>true</AddPKColumn>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, null, 2, 1, null, 0, 'UnloadResultsCard', null, null, 0);

INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1262003, 1000262, 0, 3, 26200600, null, null, 'Дата завершения', null, 1, null, 5, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);
INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1262004, 1000262, 0, 4, 26200300, null, null, 'Статус', null, 1, null, 4, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);
INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1262002, 1000262, 0, 2, 26200400, null, null, 'Дата создания', null, 1, null, 5, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);
INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1262001, 1000262, 0, 1, 26200100, null, null, 'Идентификатор', null, 1, null, 1, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);
INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1262005, 1000262, 0, 5, 26200700, null, null, 'Сообщение об ошибке', null, 1, null, 4, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);

INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1267002, 1000267, 0, 2, 26700600, null, null, 'Имя файла', null, 1, null, 4, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);
INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1267003, 1000267, 0, 3, 26700700, null, null, 'Расширение файла', null, 1, null, 4, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);
INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1267001, 1000267, 0, 1, 26700100, null, null, 'Идентификатор', null, 1, null, 1, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);
INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1267004, 1000267, 0, 4, 26700300, null, null, 'Тип выгрузки', null, 1, null, 4, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);
INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1267005, 1000267, 0, 5, 26700400, null, null, 'Дата создания', null, 1, null, 5, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);