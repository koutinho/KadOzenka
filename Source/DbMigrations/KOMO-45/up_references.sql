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