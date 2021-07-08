INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (226, 'Тип метки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'Ko.MarkType', null, '[
	{
		"Id": 0,
		"Value": "Не использовать метку",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По умолчанию",
		"Name": "Default"
	},
	{
		"Id": 2,
		"Value": "Прямая метка",
		"Name": "Straight"
	},
	{
		"Id": 3,
		"Value": "Обратная метка",
		"Name": "Reverse"
	}
]');



alter table KO_MODEL_FACTOR add "mark_type" VARCHAR(255);
alter table KO_MODEL_FACTOR add "mark_type_code" BIGINT;
update ko_model_factor set mark_type='Не использовать метку', mark_type_code=0;
ALTER TABLE KO_MODEL_FACTOR ALTER COLUMN "mark_type" SET NOT NULL;
ALTER TABLE KO_MODEL_FACTOR ALTER COLUMN "mark_type" SET NOT NULL;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21001500, 'Тип метки', 210, 4, null, 226, 'mark_type', 'mark_type_code', null, 0, null, null, 'MarkType', 0, null, null, null, 0, 1, '2021-06-17 10:44:07.352847', 0);


alter table KO_MODEL_FACTOR add "correcting_term" NUMERIC;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21001600, 'Корректирующее слагаемое', 210, 2, null, null, 'correcting_term', null, null, 0, null, null, 'CorrectingTerm', 1, null, null, null, 0, 1, '2021-06-18 09:34:47.668412', 0);


alter table KO_MODEL_FACTOR add "k" NUMERIC;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21001700, 'K', 210, 2, null, null, 'k', null, null, 0, null, null, 'K', 1, null, null, null, 0, 1, '2021-06-18 09:35:58.557549', 0);


update core_register_attribute set name = 'Поправка' where id = 21000500;

