INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (227, 'Тип данных для словаря моделирования', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'KO.ModelDictionaryType', null, '[
	{
		"Id": 1,
		"Value": "Целое число",
		"Name": "Integer"
	},	
	{
		"Id": 2,
		"Value": "Десятичное число",
		"Name": "Decimal"
	},
	{
		"Id": 3,
		"Value": "Логическое значение",
		"Name": "Boolean"
	},
	{
		"Id": 4,
		"Value": "Строка",
		"Name": "String"
	},
	{
		"Id": 5,
		"Value": "Дата и время",
		"Name": "Date"
	},
	{
		"Id": 6,
		"Value": "Значение из справочника",
		"Name": "Reference"
	}
]');

update core_register_attribute set referenceid=227 where id=26400300;

-- Удаление раскадки для справочников моделирования
delete from core_layout_details where layoutid=1000264;
delete from core_layout where layoutid=1000264;

--удаление таблицы с факторами группы
delete from core_register_attribute where registerid=208;
delete from core_register where registerid=208;
delete from core_register_relation where id in (214, 273);
DROP TABLE KO_GROUP_FACTOR;

--удаление таблицы с метками
delete from core_register_attribute where registerid=211;
delete from core_register where registerid=211;
delete from core_register_relation where id=210;
DROP TABLE KO_MARK_CATALOG;

