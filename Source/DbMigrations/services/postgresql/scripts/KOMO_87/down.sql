INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) VALUES (1002638, 1002541, 0, 5, 25400800, null, null, 'Тип территории', null, 1, null, 4, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', null, null, null, null);

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (25400800, 'Тип территории', 254, 4, null, null, 'TERRITORY_TYPE', null, null, 0, null, null, 'TerritoryType', 1, null, null, null, 0, null, null, 0);

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
	},
	{
		"Id": 3,
		"Value": "Атрибут типа территории",
		"Name": "TerritoryTypeAttribute"
	}
]'
where referenceid = 215;
