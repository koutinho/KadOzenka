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
		"Id": 17,
		"Value": "Квартира в новостройке",
		"Name": "NewBuildingFlat"
	},
	{
		"Id": 18,
		"Value": "Участок",
		"Name": "Land"
	}
]'
where referenceid=114;


--Справочник "Линия застройки"
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

