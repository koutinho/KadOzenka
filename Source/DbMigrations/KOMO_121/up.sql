INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values) 
VALUES (227, '��� ������ ��� ������� �������������', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'KO.ModelDictionaryType', null, '[
	{
		"Id": 1,
		"Value": "�����",
		"Name": "Number"
	},
	{
		"Id": 2,
		"Value": "���������� ��������",
		"Name": "Boolean"
	},
	{
		"Id": 3,
		"Value": "������",
		"Name": "String"
	},
	{
		"Id": 4,
		"Value": "���� � �����",
		"Name": "Date"
	},
	{
		"Id": 5,
		"Value": "�������� �� �����������",
		"Name": "REFERENCE"
	}
]');

update core_register_attribute set referenceid=227 where id=26400300;