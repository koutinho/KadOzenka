ALTER TABLE KO_COD_JOB ADD COLUMN register_id bigint;

INSERT INTO public.core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21500400, 'ИД реестра', 215, 1, null, null, 'register_id', null, null, 0, null, null, 'RegisterId', 0, null, null, null, 0, 1, '2021-04-06 11:37:40.969821', 0);