--коэффициент фактора из предыдущего тура
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21001200, 'Вес из предыдущего тура', 210, 2, null, null, 'previous_weight', null, null, 0, null, null, 'PreviousWeight', 1, null, null, null, 0, 2, '2020-11-05 10:50:22.000000', 0);

ALTER TABLE ko_model_factor ADD COLUMN previous_weight NUMERIC;