-- Замена типа поля с varchar(255) на текстовое для описаний
alter table market_core_object rename column advertisement_description to advertisement_description_old;
alter table market_core_object add column advertisement_description text;
update market_core_object set advertisement_description = advertisement_description_old;
alter table market_core_object drop column advertisement_description_old;
-- Снятие not null с референсного типа
alter table market_core_object alter column property_typets_cipjs_code drop not null;
-- Колонки под долготу и широту
alter table market_core_object add column latitude numeric;
alter table market_core_object add column longitude numeric;
-- Записи о новых атрибутах
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10003500, 'Широта', 100, 2, null, null, 'latitude', null, null, null, null, null, 'Latitude', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10004000, 'Долгота', 100, 2, null, null, 'longitude', null, null, null, null, null, 'Longitude', 1, null, null, null, 0, null, null, 0);

INSERT INTO core_long_process_type (id, process_name, class_name, schedule_type, repeat_interval, enabled, run_count, failure_count, last_start_date, last_run_duration, next_run_date, parameters, description, test_result, parameters_setter_url) VALUES (103, 'YrlFeedParserLongParser', 'KadOzenka.Dal.LongProcess.YrlParserLongProcess, KadOzenka.Dal', 0, null, 1, null, null, null, null, null, null, 'Загрузка аналогов из YRL-фида', 1, null);