INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values) 
VALUES (227, 'Тип данных для словаря моделирования', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'KO.ModelDictionaryType', null, '[
	{
		"Id": 1,
		"Value": "Число",
		"Name": "Number"
	},
	{
		"Id": 2,
		"Value": "Логическое значение",
		"Name": "Boolean"
	},
	{
		"Id": 3,
		"Value": "Строка",
		"Name": "String"
	},
	{
		"Id": 4,
		"Value": "Дата и время",
		"Name": "Date"
	},
	{
		"Id": 5,
		"Value": "Значение из справочника",
		"Name": "REFERENCE"
	}
]');

update core_register_attribute set referenceid=227 where id=26400300;