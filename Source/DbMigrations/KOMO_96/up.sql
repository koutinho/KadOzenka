INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (226, 'Тип метки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'Ko.MarkType', null, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По умолчанию",
		"Name": "Default"
	},
	{
		"Id": 2,
		"Value": "Прямая",
		"Name": "Straight"
	},
	{
		"Id": 3,
		"Value": "Обратная",
		"Name": "Reverse"
	}
]');


ALTER TABLE ko_model_factor ADD COLUMN sign_exponentiation smallint;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21001400, 'Признак возведения в степень', 210, 3, null, null, 'sign_exponentiation', null, null, 0, null, null, 'SignExponentiation', 1, null, null, null, 0, 1, '2021-06-17 09:33:48.718394', 0);


alter table KO_MODEL_FACTOR add "mark_type" VARCHAR(255);
alter table KO_MODEL_FACTOR add "mark_type_code" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21001500, 'Тип метки', 210, 4, null, 226, 'mark_type', 'mark_type_code', null, 0, null, null, 'MarkType', 1, null, null, null, 0, 1, '2021-06-17 10:44:07.352847', 0);


update core_register_attribute set name='Поправка' where id = 21000500;