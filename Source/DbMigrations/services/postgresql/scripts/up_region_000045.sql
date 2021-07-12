--справочник "Источник объявления"
delete from core_reference_item where referenceid=101 and itemid <> 1;

--справочник "Класс строения"
INSERT INTO core_reference_item (itemid, referenceid, code, value, short_title, is_archives, user_name, date_end_change, date_s, flag, name) 
VALUES (1000902, 116, '6', 'B-', null, null, null, null, null, null, 'Bminus');

--справочник "Сегмент рынка недвижимости"
delete from core_reference_item where referenceid=114;
update core_reference set progid='Core.RefLib.Executors.ReferenceExecutorSimple',
                          simple_values='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Квартира",
		"Name": "Flat"
	},
	{
		"Id": 2,
		"Value": "Койко-место",
		"Name": "Bed"
	},
	{
		"Id": 3,
		"Value": "Комната",
		"Name": "Room"
	},
	{
		"Id": 4,
		"Value": "Дом/дача",
		"Name": "House"
	},
	{
		"Id": 5,
		"Value": "Коттедж",
		"Name": "Cottage"
	},
	{
		"Id": 6,
		"Value": "Таунхаус",
		"Name": "Townhouse"
	},
	{
		"Id": 7,
		"Value": "Часть дома",
		"Name": "HouseShare"
	},
	{
		"Id": 8,
		"Value": "Гараж",
		"Name": "Garage"
	},
	{
		"Id": 9,
		"Value": "Готовый бизнес",
		"Name": "Business"
	},
	{
		"Id": 10,
		"Value": "Здание",
		"Name": "Building"
	},
	{
		"Id": 11,
		"Value": "Коммерческая земля",
		"Name": "CommercialLand"
	},
	{
		"Id": 12,
		"Value": "Офис",
		"Name": "Office"
	},
	{
		"Id": 13,
		"Value": "Помещение свободного назначения",
		"Name": "FreeAppointmentObject"
	},
	{
		"Id": 14,
		"Value": "Производство",
		"Name": "Industry"
	},
	{
		"Id": 15,
		"Value": "Склад",
		"Name": "Warehouse"
	},
	{
		"Id": 16,
		"Value": "Торговая площадь",
		"Name": "ShoppingArea"
	},
	{
		"Id": 17,
		"Value": "Доля в квартире",
		"Name": "FlatShare"
	},
	{
		"Id": 18,
		"Value": "Квартира в новостройке",
		"Name": "NewBuildingFlat"
	},
	{
		"Id": 19,
		"Value": "Участок",
		"Name": "Land"
	}
]'
where referenceid=114;

INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values) 
VALUES (12083, 'Линия застройки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'HouseLineType', null, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Первая",
		"Name": "First"
	},
	{
		"Id": 2,
		"Value": "Вторая",
		"Name": "Second "
	},
	{
		"Id": 3,
		"Value": "Иная",
		"Name": "Other"
	}
]');


INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12084, 'Состояние отделки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'FinishingCondition', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Требуется косметический ремонт",
		"Name": "CosmeticRepairsRequired"
	},
	{
		"Id": 2,
		"Value": "Дизайнерский ремонт",
		"Name": "Design"
	},
	{
		"Id": 3,
		"Value": "Под чистовую отделку",
		"Name": "Finishing"
	},
	{
		"Id": 4,
		"Value": "Требуется капитальный ремонт",
		"Name": "MajorRepairsRequired"
	},
	{
		"Id": 5,
		"Value": "Офисная отделка",
		"Name": "Office"
	},
	{
		"Id": 6,
		"Value": "Типовой ремонт",
		"Name": "Typical"
	}
]');


INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12085, 'Тип дома', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'HouseType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Газобетонный блок",
		"Name": "AerocreteBlock"
	},
	{
		"Id": 2,
		"Value": "Блочный",
		"Name": "Block"
	},
	{
		"Id": 3,
		"Value": "Щитовой",
		"Name": "Boards"
	},
	{
		"Id": 4,
		"Value": "Кирпичный",
		"Name": "Brick"
	},
	{
		"Id": 5,
		"Value": "Пенобетонный блок",
		"Name": "FoamConcreteBlock"
	},
	{
		"Id": 6,
		"Value": "Газосиликатный блок",
		"Name": "GasSilicateBlock"
	},
	{
		"Id": 7,
		"Value": "Монолитный",
		"Name": "Monolith"
	},
	{
		"Id": 8,
		"Value": "Монолитно-кирпичный",
		"Name": "MonolithBrick"
	},
	{
		"Id": 9,
		"Value": "Старый фонд",
		"Name": "Old"
	},
	{
		"Id": 10,
		"Value": "Панельный",
		"Name": "Panel "
	},
	{
		"Id": 11,
		"Value": "Сталинский",
		"Name": "Stalin"
	},
	{
		"Id": 12,
		"Value": "Каркасный",
		"Name": "Wireframe"
	},
	{
		"Id": 13,
		"Value": "Деревянный",
		"Name": "Wood"
	}
]');

INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12086, 'Планировка', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'Layout', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Кабинетная",
		"Name": "Cabinet"
	},
	{
		"Id": 2,
		"Value": "Коридорная",
		"Name": "Corridorplan"
	},
	{
		"Id": 3,
		"Value": "Смешанная",
		"Name": "Mixed"
	},
	{
		"Id": 4,
		"Value": "Открытая",
		"Name": "OpenSpace"
	}
]');


INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12087, 'Вид разрешённого использования', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'PermittedUseType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Cельскохозяйственное использование",
		"Name": "Agricultural"
	},
	{
		"Id": 2,
		"Value": "Деловое управление",
		"Name": "BusinessManagement"
	},
	{
		"Id": 3,
		"Value": "Общее пользование территории",
		"Name": "CommonUseArea"
	},
	{
		"Id": 4,
		"Value": "Высотная застройка",
		"Name": "HighriseBuildings"
	},
	{
		"Id": 5,
		"Value": "Гостиничное обслуживание",
		"Name": "HotelAmenities"
	},
	{
		"Id": 6,
		"Value": "Индивидуальное жилищное строительство (ИЖС)",
		"Name": "IndividualHousingConstruction"
	},
	{
		"Id": 7,
		"Value": "Промышленность",
		"Name": "Industry"
	},
	{
		"Id": 8,
		"Value": "Отдых (рекреация)",
		"Name": "Leisure"
	},
	{
		"Id": 9,
		"Value": "Малоэтажное жилищное строительство (МЖС)",
		"Name": "LowriseHousing"
	},
	{
		"Id": 10,
		"Value": "Общественное использование объектов капитального строительства",
		"Name": "PublicUseOfCapitalConstruction"
	},
	{
		"Id": 11,
		"Value": "Обслуживание автотранспорта",
		"Name": "ServiceVehicles"
	},
	{
		"Id": 12,
		"Value": "Торговые центры",
		"Name": "ShoppingCenters"
	},
	{
		"Id": 13,
		"Value": "Склады",
		"Name": "Warehouses"
	}
]');


INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12088, 'Подъездные пути', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'DrivewayType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Асфальтированная дорога",
		"Name": "Asphalt"
	},
	{
		"Id": 2,
		"Value": "Грунтовая дорога",
		"Name": "Ground"
	},
	{
		"Id": 3,
		"Value": "Нет",
		"Name": "No"
	}
]');


INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12089, 'Единица измерения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'ParcelAreaUnitType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Гектар",
		"Name": "Hectare"
	},
	{
		"Id": 2,
		"Value": "Сотка",
		"Name": "Sotka"
	}
]');
INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12090, 'Тип участка', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'ParcelType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "В собственности",
		"Name": "Owned"
	},
	{
		"Id": 2,
		"Value": "В аренде",
		"Name": "Rent"
	}
]');
INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12091, 'Статус земли', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'ParcelStatus', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Фермерское хозяйство",
		"Name": "Farm"
	},
	{
		"Id": 2,
		"Value": "Садоводство",
		"Name": "Gardening"
	},
	{
		"Id": 3,
		"Value": "Индивидуальное жилищное строительство",
		"Name": "IndividualHousingConstruction"
	},
	{
		"Id": 4,
		"Value": "Земля промышленного назначения",
		"Name": "IndustrialLand"
	},
	{
		"Id": 5,
		"Value": "Инвестпроект",
		"Name": "InvestmentProject"
	},
	{
		"Id": 6,
		"Value": "Личное подсобное хозяйство",
		"Name": "PrivateFarm"
	},
	{
		"Id": 7,
		"Value": "Дачное некоммерческое партнерство",
		"Name": "SuburbanNonProfitPartnership"
	},
	{
		"Id": 8,
		"Value": "Участок сельскохозяйственного назначения",
		"Name": "ForAgriculturalPurposes"
	},
	{
		"Id": 9,
		"Value": "Участок промышленности, транспорта, связи и иного не сельхоз. назначения",
		"Name": "IndustryTransportCommunications"
	},
	{
		"Id": 10,
		"Value": "Поселений",
		"Name": "Settlements"
	}
]');

INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12092, 'Локация электроснабжения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'ElectricityLocationType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]');

INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12093, 'Локация газоснабжения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'GasLocationType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]');

INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12094, 'Давление газа', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'GasPressureType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Высокое",
		"Name": "High"
	},
	{
		"Id": 2,
		"Value": "Среднее",
		"Name": "Middle"
	},
	{
		"Id": 3,
		"Value": "Низкое",
		"Name": "Low"
	}
]');

INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12095, 'Локация канализации', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'DrainageLocationType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]');

INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12096, 'Тип канализации', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'DrainageType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Автономная",
		"Name": "Autonomous"
	},
	{
		"Id": 2,
		"Value": "Центральная",
		"Name": "Central"
	}
]');


INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12097, 'Локация водоснабжения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'WaterLocationType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]');

INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (12098, 'Тип водоснабжения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'WaterType', null, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Автономная",
		"Name": "Autonomous"
	},
	{
		"Id": 2,
		"Value": "Центральная",
		"Name": "Central"
	},
	{
		"Id": 3,
		"Value": "Водонапорная станция",
		"Name": "PumpingStation"
	},
	{
		"Id": 4,
		"Value": "Водозаборный узел",
		"Name": "WaterIntakeFacility"
	},
	{
		"Id": 5,
		"Value": "Водонапорная башня",
		"Name": "WaterTower"
	}
]');



--маппим все Аналоги из Москвы на Область
update market_core_object set market_code=1, market='ЦИАН';
delete from market_core_object  where property_market_segment in ('Апартаменты', 'ИЖС', 'МЖС', 'Машиноместа', 'Гостиницы');
update market_core_object set property_market_segment='Офис', property_market_segment_code=12 where property_market_segment='Офисы';
update market_core_object set property_market_segment='Торговая площадь', property_market_segment_code=16 where property_market_segment='Торговля';
update market_core_object set property_market_segment='Дом/дача', property_market_segment_code=4 where property_market_segment='Садоводческое, огородническое и дачное использование';
update market_core_object set property_market_segment='Производство', property_market_segment_code=14 where property_market_segment='Производство и склады';
update market_core_object set property_market_segment='Значение отсутствует', property_market_segment_code=0 where property_market_segment='Без сегмента';
update market_core_object set property_market_segment='Гараж', property_market_segment_code=8 where property_market_segment='Гаражи';



--Колонки
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
values (10009300, 'Единица измерения площади участка', 100, 4, NULL, 12089, 'parcel_area_unit_type', 'parcel_area_unit_type_code', NULL, 0, NULL, NULL, 'ParcelAreaUnitType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.19 14:52:51', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "parcel_status" VARCHAR(255);
alter table market_core_object add "parcel_status_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") 
values (10009400, 'Статус земли', 100, 4, NULL, 12091, 'parcel_status', 'parcel_status_code', NULL, 0, NULL, NULL, 'ParcelStatus', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.19 14:55:29', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "parcel_type" VARCHAR(255);
alter table market_core_object add "parcel_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") 
values (10009500, 'Тип участка', 100, 4, NULL, 12090, 'parcel_type', 'parcel_type_code', NULL, 0, NULL, NULL, 'ParcelType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.19 14:57:07', 'YYYY.MM.DD HH24:MI:SS'), 0);

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

alter table market_core_object add "water_location_type" VARCHAR(255);
alter table market_core_object add "water_location_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010700, 'Тип локации водоснабжения', 100, 4, NULL, 12097, 'water_location_type', 'water_location_type_code', NULL, 0, NULL, NULL, 'WaterLocationType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 11:15:20', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "possibility_to_connect_water" SMALLINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010800, 'Возможность подключения воды', 100, 3, NULL, NULL, 'possibility_to_connect_water', NULL, NULL, 0, NULL, NULL, 'PossibilityToConnectWater', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 11:16:12', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "water_capacity" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10010900, 'Объем водоснабжения', 100, 1, NULL, NULL, 'water_capacity', NULL, NULL, 0, NULL, NULL, 'WaterCapacity', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 11:16:48', 'YYYY.MM.DD HH24:MI:SS'), 0);

alter table market_core_object add "water_type" VARCHAR(255);
alter table market_core_object add "water_type_code" BIGINT;
insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(10011000, 'Тип водоснабжения', 100, 4, NULL, 12098, 'water_type', 'water_type_code', NULL, 0, NULL, NULL, 'WaterType', 1, NULL, NULL, NULL, 0, 1, TO_TIMESTAMP('2021.05.20 11:17:54', 'YYYY.MM.DD HH24:MI:SS'), 0);

--Устанавливаем ограничения на обязательные поля
update core_register_attribute set is_nullable=0 where id in (10007700, 10004300, 10002700, 10003100);
update market_core_object set PROPERTY_TYPETS_CIPJS='Значение отсутствует', PROPERTY_TYPETS_CIPJS_CODE='0', PRICE=-1, AREA=-1, ADDRESS='Значение отсутствует';
ALTER TABLE market_core_object ALTER COLUMN PROPERTY_TYPETS_CIPJS SET NOT NULL;
ALTER TABLE market_core_object ALTER COLUMN PROPERTY_TYPETS_CIPJS_CODE SET NOT NULL;
ALTER TABLE market_core_object ALTER COLUMN AREA SET NOT NULL;
ALTER TABLE market_core_object ALTER COLUMN PRICE SET NOT NULL;
ALTER TABLE market_core_object ALTER COLUMN ADDRESS SET NOT NULL;
