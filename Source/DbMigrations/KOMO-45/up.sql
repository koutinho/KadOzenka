alter table market_core_object add "download_date" TIMESTAMP;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10007800, 'Дата загрузки', 100, 5, null, null, 'download_date', null, null, 0, null, null, 'DownloadDate', 1, null, null, null, 0, 1, '2021-05-17 15:17:28.870038', 0);

alter table market_core_object add "external_advertisement_id" VARCHAR(255);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10007900, 'Внешний Id объявления', 100, 4, null, null, 'external_advertisement_id', null, null, 0, null, null, 'ExternalAdvertisementId', 1, null, null, null, 0, 1, '2021-05-17 15:48:15.542255', 0);

alter table market_core_object add "advertisement_description" VARCHAR(255);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008000, 'Текст объявления', 100, 4, null, null, 'advertisement_description', null, null, 0, null, null, 'AdvertisementDescription', 1, null, null, null, 0, 1, '2021-05-17 16:05:19.752146', 0);

alter table market_core_object add "area_from" NUMERIC;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008100, 'Площадь от', 100, 2, null, null, 'area_from', null, null, 0, null, null, 'AreaFrom', 1, null, null, null, 0, 1, '2021-05-18 12:18:06.852573', 0);

alter table market_core_object add "name" VARCHAR(255);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008200, 'Название', 100, 4, null, null, 'name', null, null, 0, null, null, 'Name', 1, null, null, null, 0, 1, '2021-05-18 12:58:17.385019', 0);

alter table market_core_object add "flat_number" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008300, 'Номер квартиры на площадке', 100, 1, null, null, 'flat_number', null, null, 0, null, null, 'FlatNumber', 1, null, null, null, 0, 1, '2021-05-18 13:00:02.085994', 0);

alter table market_core_object add "section_number" VARCHAR(255);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008400, 'Номер секции', 100, 4, null, null, 'section_number', null, null, 0, null, null, 'SectionNumber', 1, null, null, null, 0, 1, '2021-05-18 13:01:05.726251', 0);

alter table market_core_object add "flat_type" VARCHAR(255);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008500, 'Тип квартиры', 100, 4, null, null, 'flat_type', null, null, 0, null, null, 'FlatType', 1, null, null, null, 0, 1, '2021-05-18 13:01:50.880357', 0);
