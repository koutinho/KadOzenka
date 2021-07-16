--коэффициент фактора из предыдущего тура
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21001200, 'Вес из предыдущего тура', 210, 2, null, null, 'previous_weight', null, null, 0, null, null, 'PreviousWeight', 1, null, null, null, 0, 2, '2020-11-05 10:50:22.000000', 0);
ALTER TABLE ko_model_factor ADD COLUMN previous_weight NUMERIC;


--свободные члены из предыдущего тура
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (20601800, 'Cвободный член в формуле для Линейного алгоритма в предыдущем туре', 206, 2, null, null, 'A0_linear_previous', null, null, 0, null, null, 'A0ForLinearTypeInPreviousTour', 1, null, null, null, 0, 2, '2020-11-12 14:02:34.000000', 0);
ALTER TABLE ko_model ADD COLUMN a0_linear_previous NUMERIC;

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (20601900, 'Cвободный член в формуле для Экспоненциального алгоритма в предыдущем туре', 206, 2, null, null, 'A0_exponential_previous', null, null, 0, null, null, 'A0ForExponentialTypeInPreviousTour', 1, null, null, null, 0, 2, '2020-11-12 14:05:57.000000', 0);
ALTER TABLE ko_model ADD COLUMN a0_exponential_previous NUMERIC;

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (20602000, 'Cвободный член в формуле для Мультипликативного алгоритма в предыдущем туре', 206, 2, null, null, 'A0_multiplicative_previous', null, null, 0, null, null, 'A0ForMultiplicativeTypeInPreviousTour', 1, null, null, null, 0, 2, '2020-11-12 14:07:26.000000', 0);
ALTER TABLE ko_model ADD COLUMN a0_multiplicative_previous NUMERIC;


--признаки деления, сложения
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21000700, 'Признак деления на фактор', 210, 3, null, null, 'SIGN_DIV', null, null, 0, null, null, 'SignDiv', 0, null, null, null, 0, null, null, 0);
ALTER TABLE ko_model_factor ADD COLUMN sign_div SMALLINT;

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21000800, 'Признак сложения', 210, 3, null, null, 'SIGN_ADD', null, null, null, null, null, 'SignAdd', 0, null, null, null, 0, null, null, 0);
ALTER TABLE ko_model_factor DROP COLUMN sign_add SMALLINT;

