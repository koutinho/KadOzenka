
ALTER TABLE KO_UNIT ADD COLUMN assessment_date timestamp not null;


INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (20102500, 'Дата оценки', 201, 5, null, null, 'assessment_date', null, null, 0, null, null, 'AssessmentDate', 0, null, null, null, 0, 1, '2021-04-16 17:51:23.044025', 0);

