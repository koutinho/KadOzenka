delete from core_layout_details where id=1002638;

alter table KO_COMPLIANCE_GUIDE drop column territory_type;
delete from core_register_attribute where id=25400800;

update core_reference set simple_values =
'[
	{
		"Id": 1,
		"Value": "Атрибут кода группы",
		"Name": "CodeGroupAttribute"
	},
	{
		"Id": 2,
		"Value": "Атрибут кадастрового квартала",
		"Name": "CodeQuarterAttribute"
	}
]'
where referenceid = 215;