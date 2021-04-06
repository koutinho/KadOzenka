alter table ko_cod_job drop column register_id;
delete from core_register_attribute where id=21500400;


INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21500300, 'Результат', 215, 4, null, null, 'RESULT_JOB', null, null, null, null, null, 'ResultJob', 0, null, null, null, 0, null, null, 0);
ALTER TABLE KO_COD_JOB ADD COLUMN RESULT_JOB varchar(255);
