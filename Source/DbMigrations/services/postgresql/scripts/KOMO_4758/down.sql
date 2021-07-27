update core_register_attribute set name='Коэффициент', value_field='COEFFICIENT', internal_name='Coefficient', is_nullable = 0 where id=21000600;
alter table ko_model_factor rename COEFFICIENT_FOR_LINEAR to coefficient;

delete from core_register_attribute where id in(21001800, 21001900);
ALTER TABLE ko_model_factor DROP COLUMN IF EXISTS COEFFICIENT_FOR_EXPONENTIAL;
ALTER TABLE ko_model_factor DROP COLUMN IF EXISTS COEFFICIENT_FOR_MULTIPLICATIVE;

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21001100, ' Алгоритм рассчёта', 210, 4, null, 205, null, 'algorithm_type', null, 0, null, null, 'AlgorithmType', 1, null, null, null, 0, 2, '2020-10-22 08:24:09.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21000400, 'Идентификатор метки', 210, 1, null, null, 'MARKER_ID', null, null, null, null, null, 'MarkerId', 1, null, null, null, 0, null, null, 0);
ALTER TABLE ko_model_factor ADD COLUMN IF NOT EXISTS MARKER_ID bigint;
ALTER TABLE ko_model_factor ADD COLUMN IF NOT EXISTS algorithm_type bigint;
