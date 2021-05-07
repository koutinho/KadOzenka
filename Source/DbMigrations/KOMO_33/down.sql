INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008300, 'Показатель тест', 100, 1, null, 303, null, null, null, 0, null, null, null, 1, null, null, null, null, 2, '2020-03-24 13:00:51.000000', 0);

INSERT INTO public.core_register_relation (id, name, parentregister, chieldregister, cardinality, kindid, parentregister_attribute_id, qscondition) 
VALUES (103, 'От объекта аналога к объекту недвижимости', 200, 100, null, 10005400, 20000200, null);

INSERT INTO core_register_relation (id, name, parentregister, chieldregister, cardinality, kindid, parentregister_attribute_id, qscondition) 
VALUES (218, 'От единицы кадастровой оценки к справочнику кадастровых кварталов', 107, 201, null, 20101700, 10700200, null);