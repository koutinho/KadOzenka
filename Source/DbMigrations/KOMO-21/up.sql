-- Колонка запрета на реестр со списком реестров источников
alter table ko_objects_characteristics_register
    add column if not exists disable_editing bool;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden)
VALUES (2400300, 'Запрет редактирования', 81, 3, null, null, 'DISABLE_EDITING', null, null, 0, null, null, 'DisableEditing', 1, null, null, null, 0, null, null, 0)
on conflict (id) do update set
    (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden)
        = (2400300, 'Запрет редактирования', 81, 3, null, null, 'DISABLE_EDITING', null, null, 0, null, null, 'DisableEditing', 1, null, null, null, 0, null, null, 0);

-- Колонка запрета на реестр с параметрами атрибутов источников
alter table gbu_attribute_settings
    add column if not exists disable_editing bool;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden)
VALUES (8100600, 'Запрет редактирования', 81, 3, null, null, 'DISABLE_EDITING', null, null, 0, null, null, 'DisableEditing', 1, null, null, null, 0, null, null, 0)
on conflict (id) do update set
    (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden)
        = (8100600, 'Запрет редактирования', 81, 3, null, null, 'DISABLE_EDITING', null, null, 0, null, null, 'DisableEditing', 1, null, null, null, 0, null, null, 0);


