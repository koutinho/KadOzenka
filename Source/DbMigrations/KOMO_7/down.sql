
--Удаление колонки с информацией о созданной таблице для словаря
alter table ko_cod_job drop column register_id;
delete from core_register_attribute where id=21500400;

--Добавление удаленной колонки RESULT_JOB
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21500300, 'Результат', 215, 4, null, null, 'RESULT_JOB', null, null, null, null, null, 'ResultJob', 0, null, null, null, 0, null, null, 0);
ALTER TABLE KO_COD_JOB ADD COLUMN RESULT_JOB varchar(255);

--Отменяем мягкое удаления словаря
delete from common_registers_with_soft_deletion where register_id = 215;

--Возвращаем старую таблицу для хранения значений словаря
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) 
VALUES (214, 'KO.CodDictionary', 'Справочник ЦОД', null, null, 'KO_COD_DICTIONARY', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, 0, null, null);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (21400100, 'Идентификатор', 214, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (21400200, 'Идентификатор задания ЦОД', 214, 1, null, null, 'ID_CODJOB', null, null, null, null, null, 'IdCodjob', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (21400300, 'Значение', 214, 4, null, null, 'VALUE', null, null, null, null, null, 'Value', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (21400400, 'Код', 214, 4, null, null, 'CODE', null, null, null, null, null, 'Code', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (21400500, ' Источник', 214, 4, null, null, 'SOURCE', null, null, null, null, null, 'Source', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (21400600, 'ФИО эксперта', 214, 4, null, null, 'EXPERT', null, null, null, null, null, 'Expert', 1, null, null, null, 0, null, null, 0);


CREATE TABLE public.ko_cod_dictionary (
  id BIGINT NOT NULL,
  id_codjob BIGINT NOT NULL,
  value VARCHAR(4000),
  code VARCHAR(255),
  source VARCHAR(255),
  expert VARCHAR(255),
  CONSTRAINT reg_214_q_pk PRIMARY KEY(id)
) 
WITH (oids = false);

COMMENT ON TABLE public.ko_cod_dictionary
IS 'Справочник ЦОД';

COMMENT ON COLUMN public.ko_cod_dictionary.id
IS 'Идентификатор';

COMMENT ON COLUMN public.ko_cod_dictionary.id_codjob
IS 'Идентификатор задания ЦОД';

COMMENT ON COLUMN public.ko_cod_dictionary.value
IS 'Значение';

COMMENT ON COLUMN public.ko_cod_dictionary.code
IS 'Код';

COMMENT ON COLUMN public.ko_cod_dictionary.source
IS ' Источник';

COMMENT ON COLUMN public.ko_cod_dictionary.expert
IS 'ФИО эксперта';

ALTER TABLE public.ko_cod_dictionary
  OWNER TO cipjs_kad_ozenka;