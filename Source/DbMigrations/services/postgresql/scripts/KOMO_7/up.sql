
--Добавление колонки с информацией о созданной таблице для словаря
ALTER TABLE KO_COD_JOB ADD COLUMN register_id bigint;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21500400, 'ИД реестра', 215, 1, null, null, 'register_id', null, null, 0, null, null, 'RegisterId', 0, null, null, null, 0, 1, '2021-04-06 11:37:40.969821', 0);

--Удаление колонки RESULT_JOB по согласованию с Димой Л
delete from core_register_attribute where id=21500300;
alter table ko_cod_job drop column RESULT_JOB;

--Для мягкого удаления словаря
INSERT INTO common_registers_with_soft_deletion (id, register_id, main_table_name)
VALUES ((select nextval('REG_OBJECT_SEQ')), 215, 'KO_COD_JOB');

--TODO удалить старую таблицу со значениями словаря (KO_COD_DICTIONARY) - пока нельзя, т.к. падает гармонизация
--delete from core_register_attribute where registerid=214;
--delete from core_register where registerid=214;
--DROP TABLE KO_COD_DICTIONARY;
