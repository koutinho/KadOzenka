delete from core_reference where referenceid=227;
update core_register_attribute set referenceid=600 where id=26400300;


INSERT INTO core_layout_details (id, layoutid, detailtype, ordinal, attributeid, sortbyattribute, referenceid, headertext, headerwidth, visible, format, datatype, expression, sqlexpression, totaltext, totaltype, style, enablestyle, textalign, qscolumn, export_column_name) 
VALUES (1002652, 1000264, 0, 1, 26400200, null, null, 'Имя', null, 1, null, 4, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', null, null, null, null);


INSERT INTO core_layout (layoutid, layoutname, layoutcomment, registerid, defaultsort, preffered, username, createdate, qsquery, isdistinct, ordertype, user_id, iscommon, internal_name, enable_minicards_mode, register_view_id, as_domain_id, column_width_type, is_using_extended_editor) 
VALUES (1000264, 'Основная раскладка для представления Справочников Моделирования', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 264, null, null, null, '2020-10-08 12:59:12.000000', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>264</MainRegisterID>
  <TDInstanceID>0</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType xsi:nil="true" />
  <DefaultAlias>false</DefaultAlias>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, null, null, 1, null, 0, 'ModelingDictionaries', null, null, 0);

