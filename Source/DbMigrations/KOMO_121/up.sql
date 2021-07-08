INSERT INTO core_reference (referenceid, description, readonly, progid, istree, defaultvalue, name, register_id, simple_values)
VALUES (227, '��� ������ ��� ������� �������������', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', null, null, 'KO.ModelDictionaryType', null, '[
	{
		"Id": 1,
		"Value": "����� �����",
		"Name": "Integer"
	},	
	{
		"Id": 2,
		"Value": "���������� �����",
		"Name": "Decimal"
	},
	{
		"Id": 3,
		"Value": "���������� ��������",
		"Name": "Boolean"
	},
	{
		"Id": 4,
		"Value": "������",
		"Name": "String"
	},
	{
		"Id": 5,
		"Value": "���� � �����",
		"Name": "Date"
	},
	{
		"Id": 6,
		"Value": "�������� �� �����������",
		"Name": "Reference"
	}
]');

update core_register_attribute set referenceid=227 where id=26400300;

-- �������� �������� ��� ������������ �������������
delete from core_layout_details where layoutid=1000264;
delete from core_layout where layoutid=1000264;

--�������� ������� � ��������� ������
delete from core_register_attribute where registerid=208;
delete from core_register where registerid=208;
delete from core_register_relation where id in (214, 273);
DROP TABLE KO_GROUP_FACTOR;

--�������� ������� � �������
delete from core_register_attribute where registerid=211;
delete from core_register where registerid=211;
delete from core_register_relation where id=210;
DROP TABLE KO_MARK_CATALOG;

