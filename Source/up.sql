--удаление view all_reports_in_system
DROP VIEW IF EXISTS all_reports_in_system;
delete from core_register_attribute where registerid = 1000811;
delete from core_register where registerid = 1000811;

--удаление таблицы COMMON_REPORT_FILES
DROP TABLE IF EXISTS COMMON_REPORT_FILES;
delete from core_register_attribute where registerid = 810;
delete from core_register where registerid = 810;
delete from core_register_relation where id=222;

--добавление новой раскладки
--удаление старой раскладки
delete from core_layout where layoutid=1000811;
delete from core_layout_details  where layoutid=1000811;


--добавление новой раскладки
--<DO>--
insert into core_layout ("layoutid", "layoutname", "layoutcomment", "registerid", "defaultsort", "preffered", "username", "createdate", "qsquery", "isdistinct", "ordertype", "iscommon", "internal_name", "enable_minicards_mode", "register_view_id", "as_domain_id", "column_width_type", "is_using_extended_editor") values
(10009365, 'Раскладка по умолчанию', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 809, 1002700, NULL, NULL, TO_TIMESTAMP('2021.03.16 17:45:10', 'YYYY.MM.DD HH24:MI:SS'), '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>809</MainRegisterID>
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
  <RegisterLinks />
  <JoinType xsi:nil="true" />
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <ExcludeLinks />
  <DefaultAlias>false</DefaultAlias>
  <AddPKColumn>true</AddPKColumn>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, 'CreatedReports', NULL, NULL, 0)
on conflict (layoutid) do update set
"layoutname"='Раскладка по умолчанию', "layoutcomment"='Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', "registerid"=809, "defaultsort"=1002700, "preffered"=NULL, "username"=NULL, "createdate"=TO_TIMESTAMP('2021.03.16 17:45:10', 'YYYY.MM.DD HH24:MI:SS'), "qsquery"='<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>809</MainRegisterID>
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
  <RegisterLinks />
  <JoinType xsi:nil="true" />
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <ExcludeLinks />
  <DefaultAlias>false</DefaultAlias>
  <AddPKColumn>true</AddPKColumn>
  <LoadRelations>false</LoadRelations>
</QSQuery>', "isdistinct"=0, "ordertype"='DESC', "iscommon"=1, "internal_name"=NULL, "enable_minicards_mode"=0, "register_view_id"='CreatedReports', "as_domain_id"=NULL, "column_width_type"=NULL, "is_using_extended_editor"=0;
--<DO>--
insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
(1002699, 10009365, 0, 5, 95000400, NULL, NULL, 'Полное имя пользователя', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL, NULL)
on conflict (id) do update set
"layoutid"=10009365, "detailtype"=0, "ordinal"=5, "attributeid"=95000400, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Полное имя пользователя', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=4, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"=NULL, "export_column_name"=NULL;

--<DO>--
insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
(1002700, 10009365, 0, 2, 80900800, NULL, NULL, 'Дата создания отчета', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL, NULL)
on conflict (id) do update set
"layoutid"=10009365, "detailtype"=0, "ordinal"=2, "attributeid"=80900800, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Дата создания отчета', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=5, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"=NULL, "export_column_name"=NULL;

--<DO>--
insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
(1002701, 10009365, 0, 3, 80901300, NULL, NULL, 'Дата окончания', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL, NULL)
on conflict (id) do update set
"layoutid"=10009365, "detailtype"=0, "ordinal"=3, "attributeid"=80901300, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Дата окончания', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=5, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"=NULL, "export_column_name"=NULL;

--<DO>--
insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
(1002702, 10009365, 0, 4, 80900400, NULL, NULL, 'Наименование отчета', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL, NULL)
on conflict (id) do update set
"layoutid"=10009365, "detailtype"=0, "ordinal"=4, "attributeid"=80900400, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Наименование отчета', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=4, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"=NULL, "export_column_name"=NULL;

--<DO>--
insert into core_layout_details ("id", "layoutid", "detailtype", "ordinal", "attributeid", "sortbyattribute", "referenceid", "headertext", "headerwidth", "visible", "format", "datatype", "expression", "sqlexpression", "totaltext", "totaltype", "style", "enablestyle", "textalign", "qscolumn", "export_column_name") values
(1002703, 10009365, 3, 1, NULL, NULL, NULL, 'Статус', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <Alias>Колонка_1</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>Колонка_1</Alias>
    <AttributeID>80901200</AttributeID>
    <Type>Value</Type>
    <Level>0</Level>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>Колонка_3</Alias>
        <Value xsi:type="xsd:long">0</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_4</Alias>
        <Value xsi:type="xsd:string">Создана</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>Колонка_5</Alias>
        <Value xsi:type="xsd:long">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_6</Alias>
        <Value xsi:type="xsd:string">Запущена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>Колонка_7</Alias>
        <Value xsi:type="xsd:long">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_8</Alias>
        <Value xsi:type="xsd:string">Завершена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>Колонка_9</Alias>
        <Value xsi:type="xsd:long">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_10</Alias>
        <Value xsi:type="xsd:string">Ошибка</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>', NULL)
on conflict (id) do update set
"layoutid"=10009365, "detailtype"=3, "ordinal"=1, "attributeid"=NULL, "sortbyattribute"=NULL, "referenceid"=NULL, "headertext"='Статус', "headerwidth"=NULL, "visible"=1, "format"=NULL, "datatype"=NULL, "expression"=NULL, "sqlexpression"=NULL, "totaltext"=NULL, "totaltype"=NULL, "style"='<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', "enablestyle"=NULL, "textalign"=NULL, "qscolumn"='<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <Alias>Колонка_1</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>Колонка_1</Alias>
    <AttributeID>80901200</AttributeID>
    <Type>Value</Type>
    <Level>0</Level>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>Колонка_3</Alias>
        <Value xsi:type="xsd:long">0</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_4</Alias>
        <Value xsi:type="xsd:string">Создана</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>Колонка_5</Alias>
        <Value xsi:type="xsd:long">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_6</Alias>
        <Value xsi:type="xsd:string">Запущена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>Колонка_7</Alias>
        <Value xsi:type="xsd:long">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_8</Alias>
        <Value xsi:type="xsd:string">Завершена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>Колонка_9</Alias>
        <Value xsi:type="xsd:long">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_10</Alias>
        <Value xsi:type="xsd:string">Ошибка</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>', "export_column_name"=NULL;