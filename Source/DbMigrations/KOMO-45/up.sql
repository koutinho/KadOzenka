update core_register_attribute set name='Класс строения' where id=10007200;

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


alter table market_core_object add deal_type VARCHAR(50);
alter table market_core_object add deal_type_code BIGINT;
update market_core_object set deal_type='Значение отсутствует', deal_type_code=0;
ALTER TABLE market_core_object ALTER COLUMN deal_type SET NOT NULL;
ALTER TABLE market_core_object ALTER COLUMN deal_type_code SET NOT NULL;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden)
VALUES (10003600, 'Тип сделки', 100, 4, null, 110, 'DEAL_TYPE', 'DEAL_TYPE_CODE', null, null, null, null, 'DealType', 0, null, null, null, 0, null, null, 0);

alter table market_core_object add "house_line" VARCHAR(255);
alter table market_core_object add "house_line_code" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008600, 'Линия застройки домов', 100, 4, null, 12083, 'house_line', 'house_line_code', null, 0, null, null, 'HouseLine', 1, null, null, null, 0, 1, '2021-05-18 16:45:07.247678', 0);

alter table market_core_object add "developer" VARCHAR(255);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008700, 'Застройщик', 100, 4, null, null, 'developer', null, null, 0, null, null, 'Developer', 1, null, null, null, 0, 1, '2021-05-18 17:01:11.716191', 0);

alter table market_core_object add "finishing_condition" VARCHAR(255);
alter table market_core_object add "finishing_condition_code" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008800, 'Состояние отделки', 100, 4, null, 12084, 'finishing_condition', 'finishing_condition_code', null, 0, null, null, 'FinishingCondition', 1, null, null, null, 0, 1, '2021-05-19 09:41:33.626469', 0);

alter table market_core_object add "house_type" VARCHAR(255);
alter table market_core_object add "house_type_code" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008900, 'Тип дома', 100, 4, null, 12085, 'house_type', 'house_type_code', null, 0, null, null, 'HouseType', 1, null, null, null, 0, 1, '2021-05-19 10:03:55.384762', 0);

alter table market_core_object add "layout" VARCHAR(255);
alter table market_core_object add "layout_code" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10009000, 'Планировка', 100, 4, null, 12086, 'layout', 'layout_code', null, 0, null, null, 'Layout', 1, null, null, null, 0, 1, '2021-05-19 10:27:26.006963', 0);

alter table market_core_object add "permitted_use_type" VARCHAR(255);
alter table market_core_object add "permitted_use_type_code" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10009100, 'Вид разрешённого использования', 100, 4, null, 12087, 'permitted_use_type', 'permitted_use_type_code', null, 0, null, null, 'PermittedUseType', 1, null, null, null, 0, 1, '2021-05-19 10:46:53.959408', 0);

alter table market_core_object add "driveway_type" VARCHAR(255);
alter table market_core_object add "driveway_type_code" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10009200, 'Подъездные пути', 100, 4, null, 12088, 'driveway_type', 'driveway_type_code', null, 0, null, null, 'DrivewayType', 1, null, null, null, 0, 1, '2021-05-19 11:03:29.477667', 0);


alter table market_core_object add "parcel_area_unit_type" VARCHAR(255);
alter table market_core_object add "parcel_area_unit_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") 
values (10009300, 'Единица измерения площади участка', 100, 4, NULL, 12089, 'parcel_area_unit_type', 'parcel_area_unit_type_code', NULL, 0, NULL, NULL, 'ParcelAreaUnitType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.19 14:52:51', 'YYYY.MM.DD HH24:MI:SS'), 0)

alter table market_core_object add "parcel_status" VARCHAR(255);
alter table market_core_object add "parcel_status_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") 
values (10009400, 'Статус земли', 100, 4, NULL, 12091, 'parcel_status', 'parcel_status_code', NULL, 0, NULL, NULL, 'ParcelStatus', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.19 14:55:29', 'YYYY.MM.DD HH24:MI:SS'), 0)

alter table market_core_object add "parcel_type" VARCHAR(255);
alter table market_core_object add "parcel_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") 
values (10009500, 'Тип участка', 100, 4, NULL, 12090, 'parcel_type', 'parcel_type_code', NULL, 0, NULL, NULL, 'ParcelType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.19 14:57:07', 'YYYY.MM.DD HH24:MI:SS'), 0)


alter table market_core_object add "electricity_location_type" VARCHAR(255);
alter table market_core_object add "electricity_location_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10009600, 'Локация электроснабжения', 100, 4, NULL, 12092, 'electricity_location_type', 'electricity_location_type_code', NULL, 0, NULL, NULL, 'ElectricityLocationType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.19 16:07:23', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "possibility_to_connect_electricity" SMALLINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10009700, 'Возможность подключения электичества', 100, 3, NULL, NULL, 'possibility_to_connect_electricity', NULL, NULL, 0, NULL, NULL, 'PossibilityToConnectElectricity', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.19 16:09:01', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "electricity_power" BIGINT;
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10009800, 'Мощность электричества, кВТ', 100, 1, null, null, 'electricity_power', null, null, 0, null, null, 'ElectricityPower', 1, null, null, null, 0, 1, '2021-05-19 16:37:55.556421', 0);

alter table market_core_object add "gas_location_type" VARCHAR(255);
alter table market_core_object add "gas_location_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10009900, 'Локация газоснабжения', 100, 4, NULL, 12093, 'gas_location_type', 'gas_location_type_code', NULL, 0, NULL, NULL, 'GasLocationType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 09:15:14', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "possibility_to_connect_gas" SMALLINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010000, 'Возможность подключения газа', 100, 3, NULL, NULL, 'possibility_to_connect_gas', NULL, NULL, 0, NULL, NULL, 'PossibilityToConnectGas', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 09:16:30', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "gas_capacity" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010100, 'Емкость газа', 100, 1, NULL, NULL, 'gas_capacity', NULL, NULL, 0, NULL, NULL, 'GasCapacity', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 09:17:15', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "gas_pressure_type" VARCHAR(255);
alter table market_core_object add "gas_pressure_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010200, 'Давление газа', 100, 4, NULL, 12094, 'gas_pressure_type', 'gas_pressure_type_code', NULL, 0, NULL, NULL, 'GasPressureType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 09:18:43', 'YYYY.MM.DD HH24:MI:SS'), 0);


alter table market_core_object add "drainage_location_type" VARCHAR(255);
alter table market_core_object add "drainage_location_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010300, 'Локация канализации', 100, 4, NULL, 12095, 'drainage_location_type', 'drainage_location_type_code', NULL, 0, NULL, NULL, 'DrainageLocationType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 10:02:49', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "possibility_to_connect_drainage" SMALLINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010400, 'Возможность подключения канализации', 100, 3, NULL, NULL, 'possibility_to_connect_drainage', NULL, NULL, 0, NULL, NULL, 'PossibilityToConnectDrainage', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 10:04:04', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "drainage_capacity" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010500, 'Объем канализации', 100, 1, NULL, NULL, 'drainage_capacity', NULL, NULL, 0, NULL, NULL, 'DrainageCapacity', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 10:04:49', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "drainage_type" VARCHAR(255);
alter table market_core_object add "drainage_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010600, 'Тип канализации', 100, 4, NULL, 12096, 'drainage_type', 'drainage_type_code', NULL, 0, NULL, NULL, 'DrainageType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 10:05:33', 'YYYY.MM.DD HH24:MI:SS'), 0);
