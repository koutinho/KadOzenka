update core_register_attribute set name='Коэффициент для линейного алгоритма', value_field='COEFFICIENT_FOR_LINEAR',  internal_name='CoefficientForLinear', is_nullable = 1 where id=21000600;
alter table ko_model_factor rename coefficient to COEFFICIENT_FOR_LINEAR;

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden)
VALUES (21001800, 'Коэффициент для экспоненциального алгоритма', 210, 2, null, null, 'COEFFICIENT_FOR_EXPONENTIAL', null, null, null, null, null, 'CoefficientForExponential', 1, null, null, null, 0, null, null, 0);
ALTER TABLE ko_model_factor ADD COLUMN IF NOT EXISTS COEFFICIENT_FOR_EXPONENTIAL numeric;

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden)
VALUES (21001900, 'Коэффициент для мультипликативного алгоритма', 210, 2, null, null, 'COEFFICIENT_FOR_MULTIPLICATIVE', null, null, null, null, null, 'CoefficientForMultiplicative', 1, null, null, null, 0, null, null, 0);
ALTER TABLE ko_model_factor ADD COLUMN IF NOT EXISTS COEFFICIENT_FOR_MULTIPLICATIVE numeric;

delete from core_register_attribute where id in(21000400, 21001100);
ALTER TABLE ko_model_factor DROP COLUMN IF EXISTS MARKER_ID;
ALTER TABLE ko_model_factor DROP COLUMN IF EXISTS algorithm_type;