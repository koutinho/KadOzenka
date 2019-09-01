
-- VI. Загрузка раскладок
DO $$
begin
delete from core_layout_details t where (t.layoutid between 1000000 and 20000000 or t.layoutid between 30000000 and 40000000);
delete from core_layout t where (t.layoutid between 1000000 and 20000000 or t.layoutid between 30000000 and 40000000);
end $$;


--<DO>--
-- 1000006
/* 
'Список пользователей'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000006, 'Список пользователей', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 950, NULL, 1, 'tm', '2018-11-15 23:50:29', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000007
/* 
'Список ролей'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000007, 'Список ролей', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 945, NULL, 1, 'tm', '2018-11-15 16:29:50', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000008
/* 
'Список подразделений'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000008, 'Список подразделений', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 941, NULL, 1, 'tm', '2018-03-19 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000809
/* 
'Основная раскладка для документов (список документов)'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000809, 'Основная раскладка для документов (список документов)', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 809, 1001021, 0, NULL, '2018-10-23 00:00:00', NULL, 0, 'DESC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000920
/* 
'Списки'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000920, 'Списки', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 920, NULL, 1, 'inecas\filinov', '2018-03-13 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>92000300</Alias>
      <AttributeID>92000300</AttributeID>
      <Type>Value</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>ИД представления реестра</Alias>
      <Value xsi:type="xsd:string">{Get:RegisterViewId}</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType>Inner</JoinType>
</QSQuery>', 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000930
/* 
'Список реестров'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000930, 'Список реестров', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 930, NULL, 1, 'tm', '2018-12-10 11:31:10', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000931
/* 
'Список показателей реестров'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000931, 'Список показателей реестров', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 931, NULL, 1, 'tm', NULL, NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000932
/* 
'Список связей реестров'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000932, 'Список связей реестров', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 932, NULL, 1, 'tm', '2016-06-16 00:00:00', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000933
/* 
'Раскладки'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000933, 'Раскладки', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 933, NULL, 1, 'tm', '2018-11-26 18:14:10', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionGroup">
        <Type>Or</Type>
        <Conditions>
          <QSCondition xsi:type="QSConditionGroup">
            <Type>And</Type>
            <Conditions>
              <QSCondition xsi:type="QSConditionSimple">
                <ConditionType>IsNotNull</ConditionType>
                <LeftOperand xsi:type="QSColumnSimple">
                  <Alias>93301600</Alias>
                  <AttributeID>93301600</AttributeID>
                  <Type>Value</Type>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperandLevel>0</RightOperandLevel>
              </QSCondition>
              <QSCondition xsi:type="QSConditionSimple">
                <ConditionType>BeginFrom</ConditionType>
                <LeftOperand xsi:type="QSColumnConstant">
                  <Alias>ИД представления реестра</Alias>
                  <Value xsi:type="xsd:string">{Get:RegisterViewId}</Value>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperand xsi:type="QSColumnSimple">
                  <Alias>93301600</Alias>
                  <AttributeID>93301600</AttributeID>
                  <Type>Value</Type>
                </RightOperand>
                <RightOperandLevel>0</RightOperandLevel>
              </QSCondition>
            </Conditions>
          </QSCondition>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>IsNull</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>93301600</Alias>
              <AttributeID>93301600</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
        </Conditions>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>93300400</Alias>
          <AttributeID>93300400</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>Идентификатор реестра</Alias>
          <Value xsi:type="xsd:string">{Get:LayoutRegisterId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000936
/* 
'Фильтры'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000936, 'Фильтры', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 936, NULL, 1, 'inecas\filinov', '2018-03-16 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionGroup">
        <Type>Or</Type>
        <Conditions>
          <QSCondition xsi:type="QSConditionGroup">
            <Type>And</Type>
            <Conditions>
              <QSCondition xsi:type="QSConditionSimple">
                <ConditionType>IsNotNull</ConditionType>
                <LeftOperand xsi:type="QSColumnSimple">
                  <Alias>93601300</Alias>
                  <AttributeID>93601300</AttributeID>
                  <Type>Value</Type>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperandLevel>0</RightOperandLevel>
              </QSCondition>
              <QSCondition xsi:type="QSConditionSimple">
                <ConditionType>RegexpLike</ConditionType>
                <LeftOperand xsi:type="QSColumnConstant">
                  <Alias>ИД представления реестра</Alias>
                  <Value xsi:type="xsd:string">{Get:RegisterViewId}</Value>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperand xsi:type="QSColumnSimple">
                  <Alias>93601300</Alias>
                  <AttributeID>93601300</AttributeID>
                  <Type>Value</Type>
                </RightOperand>
                <RightOperandLevel>0</RightOperandLevel>
              </QSCondition>
            </Conditions>
          </QSCondition>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>IsNull</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>93601300</Alias>
              <AttributeID>93601300</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
        </Conditions>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>93600800</Alias>
          <AttributeID>93600800</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>Идентификатор реестра</Alias>
          <Value xsi:type="xsd:string">{Get:FilterRegisterId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000940
/* 
'Действия'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000940, 'Действия', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 940, NULL, 1, 'tm', '2018-11-15 23:36:31', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000949
/* 
'Сессии'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000949, 'Сессии', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 949, NULL, 1, 'tm', '2018-11-16 09:32:42', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000955
/* 
'Список фильтров у роли'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000955, 'Список фильтров у роли', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 955, NULL, 1, NULL, '2014-12-15 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
</QSQuery>', 0, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000956
/* 
'Список выгрузок - Для одной раскладки'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000956, 'Список выгрузок - Для одной раскладки', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 956, 1000882, 1, 'TM', '2018-11-28 15:26:51', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>95600200</Alias>
      <AttributeID>95600200</AttributeID>
      <Type>Value</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>{Get:ExportLayoutId}</Alias>
      <Value xsi:type="xsd:string">{Get:ExportLayoutId}</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 0, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000960
/* 
'Список шаблонов'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000960, 'Список шаблонов', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 960, NULL, 1, 'tm', NULL, NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000965
/* 
'Журнал изменений в реестрах'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000965, 'Журнал изменений в реестрах', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 965, NULL, 1, NULL, '2014-12-30 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <JoinType>Inner</JoinType>
  <Joins />
</QSQuery>', 0, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000975
/* 
'Основная раскладка для списка текущих процессов'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000975, 'Основная раскладка для списка текущих процессов', NULL, 975, 1002038, NULL, NULL, '2018-12-03 23:01:50', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000976
/* 
'Основная раскладка для типов долгих процессов'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000976, 'Основная раскладка для типов долгих процессов', NULL, 976, 1002054, NULL, NULL, '2018-12-03 20:31:47', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000977
/* 
'Основная раскладка для списка сервисов'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000977, 'Основная раскладка для списка сервисов', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 977, 1002034, NULL, NULL, '2018-11-28 17:10:40', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 0, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000982
/* 
'Список справочников'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000982, 'Список справочников', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 982, NULL, 1, 'tm', '2018-12-05 20:17:41', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000986
/* 
'Основная для реестра образов'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000986, 'Основная для реестра образов', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 986, NULL, 1, NULL, '2014-11-17 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>98600900</Alias>
          <AttributeID>98600900</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>0</Alias>
          <Value xsi:type="xsd:double">0</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Inner</JoinType>
  <Joins>
    <QSJoin>
      <RegisterId>988</RegisterId>
      <JoinType>Left</JoinType>
      <JoinCondition xsi:type="QSConditionGroup">
        <Type>And</Type>
        <Conditions>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>98800200</Alias>
              <AttributeID>98800200</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnSimple">
              <Alias>98600100</Alias>
              <AttributeID>98600100</AttributeID>
              <Type>Value</Type>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>98800500</Alias>
              <AttributeID>98800500</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnConstant">
              <Alias>0</Alias>
              <Value xsi:type="xsd:double">0</Value>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
        </Conditions>
      </JoinCondition>
      <ActualDate xsi:type="QSColumnFunction">
        <Alias>Актуальные дата и время</Alias>
        <FunctionType>ActualDateTime</FunctionType>
        <Operands />
      </ActualDate>
    </QSJoin>
    <QSJoin>
      <RegisterId>963</RegisterId>
      <JoinType>Left</JoinType>
      <JoinCondition xsi:type="QSConditionGroup">
        <Type>And</Type>
        <Conditions>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>96300100</Alias>
              <AttributeID>96300100</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnSimple">
              <Alias>98800400</Alias>
              <AttributeID>98800400</AttributeID>
              <Type>Value</Type>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>98800300</Alias>
              <AttributeID>98800300</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnConstant">
              <Alias>963</Alias>
              <Value xsi:type="xsd:double">963</Value>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
        </Conditions>
      </JoinCondition>
      <ActualDate xsi:type="QSColumnFunction">
        <Alias>Актуальные дата и время</Alias>
        <FunctionType>ActualDateTime</FunctionType>
        <Operands />
      </ActualDate>
    </QSJoin>
  </Joins>
</QSQuery>', 1, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000989
/* 
'Журнал ошибок'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000989, 'Журнал ошибок', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 989, NULL, 1, 'tm', '2015-09-08 00:00:00', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1000992
/* 
'Журнал отладочных сообщений'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1000992, 'Журнал отладочных сообщений', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 992, NULL, 1, 'tm', '2015-08-14 00:00:00', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1002511
/* 
'Основная раскладка для данных БТИ'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1002511, 'Основная раскладка для данных БТИ', NULL, 251, NULL, NULL, NULL, '2018-12-05 10:38:03', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>251</MainRegisterID>
  <TDInstanceID>0</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>5200200</Alias>
          <AttributeID>5200200</AttributeID>
          <Type>Code</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Value xsi:type="xsd:string">685</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 0, NULL, 0, 'BtiData');
end $$;

--<DO>--
-- 1003011
/* 
'Пакеты данных СК'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003011, 'Пакеты данных СК', NULL, 301, 93, NULL, NULL, '2018-11-23 10:47:37', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>301</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>301000600_CODE</Alias>
      <AttributeID>301000600</AttributeID>
      <Type>Code</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>12121002</Alias>
      <Value xsi:type="xsd:string">12121002</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003021
/* 
'Логирование загрузок МФЦ'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003021, 'Логирование загрузок МФЦ', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 302, 100302103, 0, NULL, '2018-11-26 20:04:53', NULL, 0, NULL, 1, NULL, 0, 'InsurLogFile');
end $$;

--<DO>--
-- 1003031
/* 
'Реестр строк  '
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003031, 'Реестр строк  ', NULL, 303, NULL, NULL, NULL, '2018-12-09 15:38:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 0, NULL, 0, 'BankPaymentFiles');
end $$;

--<DO>--
-- 1003051
/* 
'Начисления страховых взносов от МФЦ'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003051, 'Начисления страховых взносов от МФЦ', NULL, 305, NULL, NULL, NULL, '2018-12-10 21:30:18', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 0, NULL, 0, 'MfcInputNach');
end $$;

--<DO>--
-- 1003061
/* 
'Зачисления по страховым взносам от МФЦ'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003061, 'Зачисления по страховым взносам от МФЦ', NULL, 306, NULL, NULL, NULL, '2018-12-09 16:06:39', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 0, NULL, 0, 'MfcInputPlat');
end $$;

--<DO>--
-- 1003062
/* 
'Общее имущество/Платежи'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003062, 'Общее имущество/Платежи', NULL, 306, NULL, NULL, NULL, '2018-10-26 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>306</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>306002400_Code</Alias>
      <AttributeID>306002400</AttributeID>
      <Type>Code</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Value xsi:type="xsd:double">12121002</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'InputPlatOI');
end $$;

--<DO>--
-- 1003081
/* 
'Основная раскладка для ФСП'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003081, 'Основная раскладка для ФСП', NULL, 308, 0, 1, 'tm', NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, 'FspEpd');
end $$;

--<DO>--
-- 1003082
/* 
'ФСП по Полисам'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003082, 'ФСП по Полисам', NULL, 308, 0, 1, 'tm', NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType>Left</JoinType>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, 'FspPolicy');
end $$;

--<DO>--
-- 1003083
/* 
'ФСП по Свидетельствам'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003083, 'ФСП по Свидетельствам', NULL, 308, NULL, 1, 'tm', '2018-12-05 14:19:49', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType>Left</JoinType>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'FspSvd');
end $$;

--<DO>--
-- 1003084
/* 
'Раскладка ФСП для выбора'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003084, 'Раскладка ФСП для выбора', NULL, 308, 100308401, 0, 'tm', NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	<MainRegisterID>933</MainRegisterID>
	<TDInstanceID>-1</TDInstanceID>
	<Columns />
	<Condition xsi:type="QSConditionGroup">
		<Type>And</Type>
		<Conditions>
			<QSCondition xsi:type="QSConditionSimple">
				<ConditionType>IsNotNull</ConditionType>
				<LeftOperand xsi:type="QSColumnSimple">
					<Alias>308001100</Alias>
					<AttributeID>308001100</AttributeID>
					<Type>Value</Type>
				</LeftOperand>
				<LeftOperandLevel>0</LeftOperandLevel>
				<RightOperandLevel>0</RightOperandLevel>
			</QSCondition>
		</Conditions>
	</Condition>
	<ActualDate>0001-01-01T00:00:00</ActualDate>
	<IsActual>false</IsActual>
	<Distinct>false</Distinct>
	<ManualJoin>false</ManualJoin>
	<PackageSize>0</PackageSize>
	<PackageIndex>0</PackageIndex>
	<OrderBy />
	<GroupBy />
	<JoinType>Inner</JoinType>
	<LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, 'FspSelect');
end $$;

--<DO>--
-- 1003091
/* 
'Реестр полисов'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003091, 'Реестр полисов', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 309, 22, NULL, 'tm', '2018-11-13 18:52:32', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>309</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>309000200</Alias>
          <AttributeID>309000200</AttributeID>
          <Type>Code</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Value xsi:type="xsd:string">12123001</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, 'SkPolicySvdPolis');
end $$;

--<DO>--
-- 1003092
/* 
'Реестр свидетельств'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003092, 'Реестр свидетельств', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 309, 34, NULL, NULL, '2018-10-18 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>309</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>309000200_CODE</Alias>
      <AttributeID>309000200</AttributeID>
      <Type>Code</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>12123002</Alias>
      <Value xsi:type="xsd:double">12123002</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, 'SkPolicySvdCertificate');
end $$;

--<DO>--
-- 1003101
/* 
'Реестр договоров'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003101, 'Реестр договоров', NULL, 310, NULL, NULL, NULL, '2018-11-15 15:40:33', NULL, 0, NULL, 1, NULL, 0, 'Contracts');
end $$;

--<DO>--
-- 1003121
/* 
'Реестр расчетов'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003121, 'Реестр расчетов', NULL, 312, NULL, NULL, NULL, '2018-12-03 08:26:59', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType>Left</JoinType>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'InsurCalculations');
end $$;

--<DO>--
-- 1003122
/* 
'Список расчетов страховой стоимости общего имущества'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003122, 'Список расчетов страховой стоимости общего имущества', NULL, 312, 0, 0, 'tm', NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType>Left</JoinType>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 1, 'ASC', 1, NULL, 0, 'InsurCalculations');
end $$;

--<DO>--
-- 1003131
/* 
'Реестр дел по расчету ущерба'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003131, 'Реестр дел по расчету ущерба', NULL, 313, NULL, NULL, 'tm', '2018-10-18 00:00:00', NULL, 0, NULL, 1, NULL, 0, 'Damages');
end $$;

--<DO>--
-- 1003132
/* 
'Реестр дел по ЖП'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003132, 'Реестр дел по ЖП', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 313, NULL, NULL, 'tm', '2018-12-01 17:09:18', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>313</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>313006000</Alias>
      <AttributeID>313006000</AttributeID>
      <Type>Value</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>317</Alias>
      <Value xsi:type="xsd:double">317</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'DamageAnalysisGP');
end $$;

--<DO>--
-- 1003133
/* 
'Реестр дел по ОИ'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003133, 'Реестр дел по ОИ', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 313, NULL, NULL, 'tm', '2018-11-07 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>313</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>313006000</Alias>
      <AttributeID>313006000</AttributeID>
      <Type>Value</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>310</Alias>
      <Value xsi:type="xsd:double">316</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'DamageAnalysisOI');
end $$;

--<DO>--
-- 1003141
/* 
'Реестр страховых выплат по Общему имуществу'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003141, 'Реестр страховых выплат по Общему имуществу', NULL, 314, NULL, NULL, NULL, '2018-11-15 11:51:03', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>314000500_CODE</Alias>
      <AttributeID>314000500</AttributeID>
      <Type>Code</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>12122002</Alias>
      <Value xsi:type="xsd:double">12122002</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'PaymentsOI');
end $$;

--<DO>--
-- 1003142
/* 
'Реестр страховых выплат по ЖП'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003142, 'Реестр страховых выплат по ЖП', NULL, 314, 140, NULL, NULL, '2018-10-18 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>314</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>314000500_CODE</Alias>
      <AttributeID>314000500</AttributeID>
      <Type>Code</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>12122001</Alias>
      <Value xsi:type="xsd:double">12122001</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Inner</JoinType>
  <Joins />
</QSQuery>', 0, 'ASC', 1, NULL, 0, 'PaymentsOI');
end $$;

--<DO>--
-- 1003151
/* 
'Реестр сведений об отказах в страховых выплатах'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003151, 'Реестр сведений об отказах в страховых выплатах', NULL, 315, NULL, NULL, NULL, '2018-11-15 16:21:35', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>315</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>315000800_CODE</Alias>
      <AttributeID>315000800</AttributeID>
      <Type>Code</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>12122002</Alias>
      <Value xsi:type="xsd:double">12122002</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'Refusals');
end $$;

--<DO>--
-- 1003152
/* 
'Реестр отказов в страховых выплат по ЖП'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003152, 'Реестр отказов в страховых выплат по ЖП', NULL, 315, 147, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>315</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>315000800_CODE</Alias>
      <AttributeID>315000800</AttributeID>
      <Type>Code</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>12122001</Alias>
      <Value xsi:type="xsd:double">12122001</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, 'InsuranceNoPayments');
end $$;

--<DO>--
-- 1003161
/* 
'Реестр объектов общего имущества'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003161, 'Реестр объектов общего имущества', NULL, 316, NULL, NULL, NULL, '2018-11-29 10:48:32', NULL, 0, NULL, 1, NULL, 0, 'InsurBuildings');
end $$;

--<DO>--
-- 1003162
/* 
'Реестр Многоквартирные дома'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003162, 'Реестр Многоквартирные дома', NULL, 316, NULL, NULL, NULL, '2018-12-05 16:50:56', NULL, 0, NULL, 1, NULL, 0, 'Tenements');
end $$;

--<DO>--
-- 1003171
/* 
'Реестр объектов страхования жилых помещений'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003171, 'Реестр объектов страхования жилых помещений', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 317, NULL, NULL, 'tm', '2018-12-05 16:52:56', NULL, 0, NULL, 1, NULL, 0, 'LivingSpaces');
end $$;

--<DO>--
-- 1003231
/* 
'Справочник "Вид документа-основания"'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003231, 'Справочник "Вид документа-основания"', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 323, NULL, NULL, 'tm', '2018-10-29 00:00:00', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003241
/* 
'Методик оценки ущерба'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003241, 'Методик оценки ущерба', NULL, 324, 348, NULL, 'tm', '2018-11-21 20:23:06', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType>Inner</JoinType>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003251
/* 
'Реестр пакеты данных МФЦ'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003251, 'Реестр пакеты данных МФЦ', NULL, 325, 0, NULL, '', NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <JoinType>Inner</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 0, NULL, 0, 'InputPackageFile');
end $$;

--<DO>--
-- 1003281
/* 
'Справочник «Страховые организации»'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003281, 'Справочник «Страховые организации»', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 328, NULL, NULL, 'tm', '2018-10-29 00:00:00', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003282
/* 
'Справочник «Страховые организации» для расчета'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003282, 'Справочник «Страховые организации» для расчета', NULL, 328, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>328</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>328000100</Alias>
      <AttributeID>328000100</AttributeID>
      <Type>Value</Type>
    </LeftOperand>
    <LeftOperandLevel>1</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnQuery">
      <SubQuery>
        <MainRegisterID>258</MainRegisterID>
        <TDInstanceID>-1</TDInstanceID>
        <Columns>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>258001000</Alias>
            <AttributeID>258001000</AttributeID>
            <Type>Value</Type>
          </QSColumn>
        </Columns>
        <Condition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>258000100</Alias>
            <AttributeID>258000100</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>2</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnConstant">
            <Value xsi:type="xsd:string">{Get:OkrugId}</Value>
          </RightOperand>
          <RightOperandLevel>2</RightOperandLevel>
        </Condition>
        <ActualDate>0001-01-01T00:00:00</ActualDate>
        <ActualDateColumn xsi:type="QSColumnFunction">
          <FunctionType>ActualDateTime</FunctionType>
          <Operands />
        </ActualDateColumn>
        <IsActual>false</IsActual>
        <Distinct>false</Distinct>
        <ManualJoin>false</ManualJoin>
        <PackageSize>0</PackageSize>
        <PackageIndex>0</PackageIndex>
        <OrderBy />
        <GroupBy />
        <RegisterLinks />
        <JoinType xsi:nil="true" />
        <LoadRelations>false</LoadRelations>
      </SubQuery>
    </RightOperand>
    <RightOperandLevel>1</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'InsuranceOrganizationSelectCalc');
end $$;

--<DO>--
-- 1003441
/* 
'Банки'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003441, 'Банки', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 344, NULL, NULL, 'tm', '2018-11-28 20:16:11', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003451
/* 
'Управляющие организации'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003451, 'Управляющие организации', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 345, NULL, NULL, 'tm', '2018-11-28 19:51:12', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003481
/* 
'Справочник «Страховая стоимость ЖП»'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003481, 'Справочник «Страховая стоимость ЖП»', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 348, NULL, NULL, 'tm', '2018-10-16 00:00:00', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003491
/* 
'Справочник "Доли ответственности СК и города"'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003491, 'Справочник "Доли ответственности СК и города"', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 349, NULL, NULL, 'tm', '2018-11-08 13:56:30', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003521
/* 
'Реестр регистрации изменения данных'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003521, 'Реестр регистрации изменения данных', NULL, 352, NULL, NULL, NULL, '2018-12-04 18:08:41', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>352</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'InsurChangesLog');
end $$;

--<DO>--
-- 1003541
/* 
'Реестр оплат'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003541, 'Реестр оплат', NULL, 354, 360, NULL, NULL, '2018-11-29 09:36:04', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>354</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>354000900_CODE</Alias>
      <AttributeID>354000900</AttributeID>
      <Type>Code</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Alias>12122001</Alias>
      <Value xsi:type="xsd:string">{Get:Type}</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, 'Pays');
end $$;

--<DO>--
-- 1003551
/* 
'Реестр счетов'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003551, 'Реестр счетов', NULL, 355, 365, NULL, NULL, '2018-11-28 14:01:41', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>355</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>355001500</Alias>
      <AttributeID>355001500</AttributeID>
      <Type>Value</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Value xsi:type="xsd:string">{Get:PayId}</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, 'Invoices');
end $$;

--<DO>--
-- 1003552
/* 
'Реестр счетов ОИ'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003552, 'Реестр счетов ОИ', NULL, 355, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>355</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionSimple">
    <ConditionType>Equal</ConditionType>
    <LeftOperand xsi:type="QSColumnSimple">
      <Alias>355001500</Alias>
      <AttributeID>355001500</AttributeID>
      <Type>Value</Type>
    </LeftOperand>
    <LeftOperandLevel>0</LeftOperandLevel>
    <RightOperand xsi:type="QSColumnConstant">
      <Value xsi:type="xsd:string">{Get:PayId}</Value>
    </RightOperand>
    <RightOperandLevel>0</RightOperandLevel>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'InvoicesOI');
end $$;

--<DO>--
-- 1003571
/* 
'Справочник "Причины отказа в выплате ущерба ГБУ"'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003571, 'Справочник "Причины отказа в выплате ущерба ГБУ"', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 357, NULL, NULL, 'tm', '2018-11-14 12:08:55', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003591
/* 
'Логирование процесса идентифкации зачислений'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003591, 'Логирование процесса идентифкации зачислений', NULL, 359, 100359101, 0, NULL, '2018-12-04 16:34:05', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <JoinType>Inner</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, 'InsurFilePlatIdentifyLog');
end $$;

--<DO>--
-- 1003601
/* 
'Основная раскладка для журнала формирования зданий страхования'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003601, 'Основная раскладка для журнала формирования зданий страхования', NULL, 360, NULL, NULL, NULL, '2018-12-05 13:16:06', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>360</MainRegisterID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'AdminImportInsurBuilding');
end $$;

--<DO>--
-- 1003611
/* 
'Основная раскладка для журнала загрузки помещений страхования'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003611, 'Основная раскладка для журнала загрузки помещений страхования', NULL, 361, NULL, NULL, NULL, '2018-12-05 13:40:55', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>361</MainRegisterID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 0, NULL, 0, 'AdminImportInsurFlat');
end $$;

--<DO>--
-- 1003621
/* 
'Журнал загрузки таблицы ehd.building_parcel'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003621, 'Журнал загрузки таблицы ehd.building_parcel', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 362, NULL, NULL, 'tm', '2018-11-26 12:20:20', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003641
/* 
'Журнал загрузки таблицы ehd.location'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003641, 'Журнал загрузки таблицы ehd.location', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 364, NULL, NULL, 'tm', '2018-11-27 17:32:38', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003651
/* 
'Журнал загрузки таблицы ehd.egrp'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003651, 'Журнал загрузки таблицы ehd.egrp', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 365, NULL, NULL, 'tm', '2018-11-27 17:33:11', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003661
/* 
'Журнал загрузки таблицы ehd.right'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003661, 'Журнал загрузки таблицы ehd.right', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 366, NULL, NULL, 'tm', '2018-11-27 17:33:57', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003671
/* 
'Журнал загрузки таблицы ehd.old_numbers'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003671, 'Журнал загрузки таблицы ehd.old_numbers', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 367, NULL, NULL, 'tm', '2018-11-27 17:34:39', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1003691
/* 
'Основная раскладка для журнала загрузки данных БТИ'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003691, 'Основная раскладка для журнала загрузки данных БТИ', NULL, 369, NULL, NULL, NULL, '2018-12-05 15:10:18', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>369</MainRegisterID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 0, NULL, 0, 'AdminImportBti');
end $$;

--<DO>--
-- 1003701
/* 
'Логирование процесса обработки начислений/зачилсений'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003701, 'Логирование процесса обработки начислений/зачилсений', NULL, 370, 100370105, 0, NULL, '2018-12-05 17:07:12', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <JoinType>Inner</JoinType>
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, 'InsurFileProcessLog');
end $$;

--<DO>--
-- 1003801
/* 
'Основные статистические показатели'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1003801, 'Основные статистические показатели', NULL, 380, NULL, NULL, NULL, '2018-12-05 22:52:48', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>380</MainRegisterID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 0, NULL, 0, 'ApCommon');
end $$;

--<DO>--
-- 1004001
/* 
'Основная раскладка для данных ЕГРН'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1004001, 'Основная раскладка для данных ЕГРН', NULL, 400, NULL, NULL, NULL, '2018-12-05 10:57:05', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>400</MainRegisterID>
  <TDInstanceID>0</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>Or</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>400000800</Alias>
          <AttributeID>400000800</AttributeID>
          <Type>Code</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Value xsi:type="xsd:string">1281447</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>400000800</Alias>
          <AttributeID>400000800</AttributeID>
          <Type>Code</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Value xsi:type="xsd:string">1281446</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 0, NULL, 0, 'EhdData');
end $$;

--<DO>--
-- 1009331
/* 
'Нерегламентированная отчетность: запросы'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009331, 'Нерегламентированная отчетность: запросы', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 933, NULL, 0, 'tm', '2018-02-26 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1009401
/* 
'Аудит действий с актами сверки'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009401, 'Аудит действий с актами сверки', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 940, 1100485, NULL, NULL, '2016-08-11 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94000800</Alias>
          <AttributeID>94000800</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>{Get:ObjectRegisterId}</Alias>
          <Value xsi:type="xsd:string">{Get:ObjectRegisterId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94000900</Alias>
          <AttributeID>94000900</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>{Get:ObjectId}</Alias>
          <Value xsi:type="xsd:string">{Get:ObjectId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1009402
/* 
'Аудит действий с расчетами'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009402, 'Аудит действий с расчетами', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 940, 1100493, NULL, NULL, '2015-07-21 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94000800</Alias>
          <AttributeID>94000800</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>{Get:ObjectRegisterId}</Alias>
          <Value xsi:type="xsd:string">{Get:ObjectRegisterId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94000900</Alias>
          <AttributeID>94000900</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>{Get:ObjectId}</Alias>
          <Value xsi:type="xsd:string">{Get:ObjectId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1009403
/* 
'Аудит действий с объектом'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009403, 'Аудит действий с объектом', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 940, 1000840, 0, NULL, '2016-01-11 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94000800</Alias>
          <AttributeID>94000800</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>{Get:ObjectRegisterId}</Alias>
          <Value xsi:type="xsd:string">{Get:ObjectRegisterId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94000900</Alias>
          <AttributeID>94000900</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>{Get:ObjectId}</Alias>
          <Value xsi:type="xsd:string">{Get:ObjectId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1009404
/* 
'Аудит действий с ФЛС'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009404, 'Аудит действий с ФЛС', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 940, NULL, 0, NULL, '2016-08-11 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>Or</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionGroup">
        <Type>And</Type>
        <Conditions>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>94000800</Alias>
              <AttributeID>94000800</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnConstant">
              <Alias>{Get:ObjectRegisterId}</Alias>
              <Value xsi:type="xsd:string">{Get:ObjectRegisterId}</Value>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>94000900</Alias>
              <AttributeID>94000900</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnConstant">
              <Alias>{Get:ObjectId}</Alias>
              <Value xsi:type="xsd:string">{Get:ObjectId}</Value>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
        </Conditions>
      </QSCondition>
      <QSCondition xsi:type="QSConditionGroup">
        <Type>And</Type>
        <Conditions>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>94001200</Alias>
              <AttributeID>94001200</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnConstant">
              <Alias>{Get:ObjectRegisterId}</Alias>
              <Value xsi:type="xsd:string">{Get:ObjectRegisterId}</Value>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>94001300</Alias>
              <AttributeID>94001300</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnConstant">
              <Alias>{Get:ObjectId}</Alias>
              <Value xsi:type="xsd:string">{Get:ObjectId}</Value>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
        </Conditions>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1009405
/* 
'Аудит действий со значением расчетных данных (Оценка эффективности)'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009405, 'Аудит действий со значением расчетных данных (Оценка эффективности)', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 940, 1001422, 0, NULL, '2016-12-07 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94000800</Alias>
          <AttributeID>94000800</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>{Get:ObjectRegisterId}</Alias>
          <Value xsi:type="xsd:string">{Get:ObjectRegisterId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94000900</Alias>
          <AttributeID>94000900</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>{Get:ObjectId}</Alias>
          <Value xsi:type="xsd:string">{Get:ObjectId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1009561
/* 
'Список выгрузок - Все'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009561, 'Список выгрузок - Все', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 956, 1002087, 0, 'TM', '2018-03-02 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'DESC', 0, NULL, 0, NULL);
end $$;

--<DO>--
-- 1009751
/* 
'Журнал загрузки СК'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009751, 'Журнал загрузки СК', NULL, 975, 0, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>975</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>Or</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>97600200</Alias>
          <AttributeID>97600200</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>234</Alias>
          <Value xsi:type="xsd:string">SkImportLoadProcess</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>97600200</Alias>
          <AttributeID>97600200</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>123</Alias>
          <Value xsi:type="xsd:string">SkProcessLoadProcess</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Inner</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, 'SkLoadFile', 0, NULL);
end $$;

--<DO>--
-- 1009831
/* 
'Основная раскладка для справочных значений'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1009831, 'Основная раскладка для справочных значений', NULL, 983, NULL, NULL, NULL, '2018-12-04 23:18:36', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>983</MainRegisterID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, NULL, 1, NULL, 0, 'CoreSharedReferenceItem');
end $$;

--<DO>--
-- 1094501
/* 
'Список ролей (для выбора)'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1094501, 'Список ролей (для выбора)', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 945, 109450102, 1, 'tm', '2018-04-06 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>NotExists</ConditionType>
        <LeftOperand xsi:type="QSColumnQuery">
          <Alias>НоваяКолонка_1</Alias>
          <SubQuery>
            <MainRegisterID>952</MainRegisterID>
            <TDInstanceID>0</TDInstanceID>
            <Columns>
              <QSColumn xsi:type="QSColumnConstant">
                <Alias>1</Alias>
                <Value xsi:type="xsd:double">1</Value>
              </QSColumn>
            </Columns>
            <Condition xsi:type="QSConditionGroup">
              <Type>And</Type>
              <Conditions>
                <QSCondition xsi:type="QSConditionSimple">
                  <ConditionType>Equal</ConditionType>
                  <LeftOperand xsi:type="QSColumnSimple">
                    <Alias>95200200</Alias>
                    <AttributeID>95200200</AttributeID>
                    <Type>Value</Type>
                  </LeftOperand>
                  <LeftOperandLevel>0</LeftOperandLevel>
                  <RightOperand xsi:type="QSColumnSimple">
                    <Alias>94500100</Alias>
                    <AttributeID>94500100</AttributeID>
                    <Type>Value</Type>
                  </RightOperand>
                  <RightOperandLevel>1</RightOperandLevel>
                </QSCondition>
                <QSCondition xsi:type="QSConditionSimple">
                  <ConditionType>Equal</ConditionType>
                  <LeftOperand xsi:type="QSColumnSimple">
                    <Alias>95200100</Alias>
                    <AttributeID>95200100</AttributeID>
                    <Type>Value</Type>
                  </LeftOperand>
                  <LeftOperandLevel>0</LeftOperandLevel>
                  <RightOperand xsi:type="QSColumnConstant">
                    <Alias>{Get:UserId}</Alias>
                    <Value xsi:type="xsd:string">{Get:UserId}</Value>
                  </RightOperand>
                  <RightOperandLevel>0</RightOperandLevel>
                </QSCondition>
              </Conditions>
            </Condition>
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
            <LoadRelations>false</LoadRelations>
          </SubQuery>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Inner</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1094502
/* 
'Список ролей (для функции)'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1094502, 'Список ролей (для функции)', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 945, 1002129, 1, 'tm', '2018-03-24 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
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
  <LoadRelations>false</LoadRelations>
</QSQuery>', 1, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1094601
/* 
'Доступные роли для функции'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1094601, 'Доступные роли для функции', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 946, NULL, 0, 'tm', '2018-03-23 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>946</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>true</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Inner</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 1, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 1095001
/* 
'Список пользователей (для функции)'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (1095001, 'Список пользователей (для функции)', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 950, 1002131, 0, 'tm', '2018-03-24 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>946</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Exists</ConditionType>
        <LeftOperand xsi:type="QSColumnQuery">
          <Alias>НоваяКолонка_1</Alias>
          <SubQuery>
            <MainRegisterID>952</MainRegisterID>
            <TDInstanceID>0</TDInstanceID>
            <Columns>
              <QSColumn xsi:type="QSColumnConstant">
                <Alias>1</Alias>
                <Value xsi:type="xsd:double">1</Value>
              </QSColumn>
            </Columns>
            <Condition xsi:type="QSConditionGroup">
              <Type>And</Type>
              <Conditions>
                <QSCondition xsi:type="QSConditionSimple">
                  <ConditionType>Equal</ConditionType>
                  <LeftOperand xsi:type="QSColumnSimple">
                    <Alias>94600100</Alias>
                    <AttributeID>94600100</AttributeID>
                    <Type>Value</Type>
                  </LeftOperand>
                  <LeftOperandLevel>0</LeftOperandLevel>
                  <RightOperand xsi:type="QSColumnConstant">
                    <Alias>{Get:FunctionId}</Alias>
                    <Value xsi:type="xsd:string">{Get:FunctionId}</Value>
                  </RightOperand>
                  <RightOperandLevel>0</RightOperandLevel>
                </QSCondition>
                <QSCondition xsi:type="QSConditionSimple">
                  <ConditionType>IsNotNull</ConditionType>
                  <LeftOperand xsi:type="QSColumnSimple">
                    <Alias>95200000</Alias>
                    <AttributeID>95200000</AttributeID>
                    <Type>Value</Type>
                  </LeftOperand>
                  <LeftOperandLevel>0</LeftOperandLevel>
                  <RightOperandLevel>0</RightOperandLevel>
                </QSCondition>
                <QSCondition xsi:type="QSConditionSimple">
                  <ConditionType>Equal</ConditionType>
                  <LeftOperand xsi:type="QSColumnSimple">
                    <Alias>95200100</Alias>
                    <AttributeID>95200100</AttributeID>
                    <Type>Value</Type>
                  </LeftOperand>
                  <LeftOperandLevel>0</LeftOperandLevel>
                  <RightOperand xsi:type="QSColumnSimple">
                    <Alias>95000100</Alias>
                    <AttributeID>95000100</AttributeID>
                    <Type>Value</Type>
                  </RightOperand>
                  <RightOperandLevel>1</RightOperandLevel>
                </QSCondition>
              </Conditions>
            </Condition>
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
            <LoadRelations>false</LoadRelations>
          </SubQuery>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnFunction">
          <Alias>NVL</Alias>
          <FunctionType>Coalesce</FunctionType>
          <Operands>
            <QSColumn xsi:type="QSColumnSimple">
              <Alias>95001000</Alias>
              <AttributeID>95001000</AttributeID>
              <Type>Value</Type>
            </QSColumn>
            <QSColumn xsi:type="QSColumnConstant">
              <Alias>0</Alias>
              <Value xsi:type="xsd:double">0</Value>
            </QSColumn>
          </Operands>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>0</Alias>
          <Value xsi:type="xsd:double">0</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>true</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Inner</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 1, 'ASC', 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 10003631
/* 
'Журнал загрузки таблицы ehd.register'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (10003631, 'Журнал загрузки таблицы ehd.register', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 363, NULL, NULL, 'tm', '2018-11-27 17:31:04', NULL, 0, NULL, 1, NULL, 0, NULL);
end $$;

--<DO>--
-- 10009361
/* 
'Фильтры (выбор)'
*/
DO $$
begin
INSERT INTO CORE_LAYOUT (LAYOUTID, LAYOUTNAME, LAYOUTCOMMENT, REGISTERID, DEFAULTSORT, PREFFERED, USERNAME, CREATEDATE, QSQUERY, ISDISTINCT, ORDERTYPE, ISCOMMON,INTERNAL_NAME,ENABLE_MINICARDS_MODE,REGISTER_VIEW_ID)
VALUES (10009361, 'Фильтры (выбор)', 'Данная раскладка находится на поддержке и обновляется одновременно с установкой новой версии системы', 936, 1000936102, 0, 'inecas\filinov', '2018-03-16 00:00:00', '<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MainRegisterID>933</MainRegisterID>
  <TDInstanceID>-1</TDInstanceID>
  <Columns />
  <Condition xsi:type="QSConditionGroup">
    <Type>And</Type>
    <Conditions>
      <QSCondition xsi:type="QSConditionGroup">
        <Type>Or</Type>
        <Conditions>
          <QSCondition xsi:type="QSConditionGroup">
            <Type>And</Type>
            <Conditions>
              <QSCondition xsi:type="QSConditionSimple">
                <ConditionType>IsNotNull</ConditionType>
                <LeftOperand xsi:type="QSColumnSimple">
                  <Alias>93601300</Alias>
                  <AttributeID>93601300</AttributeID>
                  <Type>Value</Type>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperandLevel>0</RightOperandLevel>
              </QSCondition>
              <QSCondition xsi:type="QSConditionSimple">
                <ConditionType>RegexpLike</ConditionType>
                <LeftOperand xsi:type="QSColumnConstant">
                  <Alias>ИД представления реестра</Alias>
                  <Value xsi:type="xsd:string">{Get:RegisterViewId}</Value>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperand xsi:type="QSColumnSimple">
                  <Alias>93601300</Alias>
                  <AttributeID>93601300</AttributeID>
                  <Type>Value</Type>
                </RightOperand>
                <RightOperandLevel>0</RightOperandLevel>
              </QSCondition>
            </Conditions>
          </QSCondition>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>IsNull</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>93601300</Alias>
              <AttributeID>93601300</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
        </Conditions>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>93600800</Alias>
          <AttributeID>93600800</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>Идентификатор реестра</Alias>
          <Value xsi:type="xsd:string">{Get:FilterRegisterId}</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
      <QSCondition xsi:type="QSConditionGroup">
        <Type>Or</Type>
        <Conditions>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>93601100</Alias>
              <AttributeID>93601100</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnConstant">
              <Alias>1</Alias>
              <Value xsi:type="xsd:double">1</Value>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
          <QSCondition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <Alias>93601000</Alias>
              <AttributeID>93601000</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>0</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnConstant">
              <Alias>{SRDSession:UserID}</Alias>
              <Value xsi:type="xsd:string">{SRDSession:UserID}</Value>
            </RightOperand>
            <RightOperandLevel>0</RightOperandLevel>
          </QSCondition>
        </Conditions>
      </QSCondition>
      <QSCondition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>93600600</Alias>
          <AttributeID>93600600</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>1</Alias>
          <Value xsi:type="xsd:double">1</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </QSCondition>
    </Conditions>
  </Condition>
  <ActualDate>0001-01-01T00:00:00</ActualDate>
  <IsActual>false</IsActual>
  <Distinct>false</Distinct>
  <ManualJoin>false</ManualJoin>
  <PackageSize>0</PackageSize>
  <PackageIndex>0</PackageIndex>
  <OrderBy />
  <GroupBy />
  <RegisterLinks />
  <JoinType>Left</JoinType>
  <Joins />
  <Parameters />
  <SubMapRegisters />
  <LoadRelations>false</LoadRelations>
</QSQuery>', 0, 'ASC', 1, NULL, 0, NULL);
end $$;
-- VII. Загрузка деталей раскладок



--<DO>--
-- 1000205
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000205, 1000006, 0, 3, 94100300, NULL, 0, 'Департамент', 25, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000204
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000204, 1000006, 0, 2, 95000300, NULL, 0, 'Логин', 20, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000203
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000203, 1000006, 0, 1, 95000400, NULL, 0, 'ФИО', 35, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000206
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000206, 1000006, 3, 4, NULL, NULL, NULL, 'Активный', 10, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnIf">
  <Alias>Активный</Alias>
  <Blocks>
    <QSColumnIfBlock>
      <Condition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>95001000</Alias>
          <AttributeID>95001000</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>1</Alias>
          <Value xsi:type="xsd:double">1</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </Condition>
      <Result xsi:type="QSColumnConstant">
        <Alias>Заблокирован</Alias>
        <Value xsi:type="xsd:string">Заблокирован</Value>
      </Result>
    </QSColumnIfBlock>
    <QSColumnIfBlock>
      <Result xsi:type="QSColumnConstant">
        <Alias>Активный</Alias>
        <Value xsi:type="xsd:string">Активный</Value>
      </Result>
    </QSColumnIfBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1002162
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002162, 1000006, 3, 5, NULL, NULL, NULL, 'Ролей у пользователя', 10, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnQuery">
  <Alias>Ролей у пользователя</Alias>
  <SubQuery>
    <MainRegisterID>952</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>Количество</Alias>
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Alias>1</Alias>
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>95200100</Alias>
        <AttributeID>95200100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>95000100</Alias>
        <AttributeID>95000100</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <ManualJoin>false</ManualJoin>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy />
    <GroupBy />
    <JoinType xsi:nil="true" />
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000207
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000207, 1000007, 0, 1, 94500200, NULL, 0, 'Роль', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000208
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000208, 1000007, 0, 3, 94500400, NULL, 0, 'Признак администратора', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002119
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002119, 1000007, 0, 2, 94500700, NULL, NULL, 'Подсистема', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002130
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002130, 1000007, 3, 4, NULL, NULL, NULL, 'Пользователей в роли', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnQuery">
  <Alias>Пользователей в роли</Alias>
  <SubQuery>
    <MainRegisterID>952</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>НоваяКолонка_2</Alias>
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Alias>1</Alias>
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>95200200</Alias>
            <AttributeID>95200200</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <Alias>94500100</Alias>
            <AttributeID>94500100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnFunction">
            <Alias>NVL</Alias>
            <FunctionType>Coalesce</FunctionType>
            <Operands>
              <QSColumn xsi:type="QSColumnSimple">
                <Alias>95001000</Alias>
                <AttributeID>95001000</AttributeID>
                <Type>Value</Type>
              </QSColumn>
              <QSColumn xsi:type="QSColumnConstant">
                <Alias>0</Alias>
                <Value xsi:type="xsd:double">0</Value>
              </QSColumn>
            </Operands>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnConstant">
            <Alias>0</Alias>
            <Value xsi:type="xsd:double">0</Value>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <ManualJoin>false</ManualJoin>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy />
    <GroupBy />
    <JoinType xsi:nil="true" />
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000210
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000210, 1000008, 0, 1, 94100200, NULL, 0, 'Код подразделения', 10, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000211
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000211, 1000008, 0, 2, 94100300, NULL, 0, 'Наименование', 80, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000212
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000212, 1000008, 0, 3, 94100600, NULL, 0, 'Удален', 10, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?><StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><Conditions><StyleConditionItem><Id>0</Id><Condition>равно</Condition><Value>1</Value><ValueId>0</ValueId><ForeColor Web="" Alpha="0" /><BackColor Web="" Alpha="0" /><Bold>false</Bold><Underline>false</Underline><Strikethru>false</Strikethru><ImagePath>~/CoreUI/Content/RegisterImages/16x16_Cancel.png</ImagePath></StyleConditionItem><StyleConditionItem><Id>0</Id><Condition>равно</Condition><Value>0</Value><ValueId>0</ValueId><ForeColor Web="" Alpha="0" /><BackColor Web="" Alpha="0" /><Bold>false</Bold><Underline>false</Underline><Strikethru>false</Strikethru><ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath></StyleConditionItem></Conditions><RowStyle>false</RowStyle></StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1001022
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001022, 1000809, 0, 3, 80900400, NULL, NULL, 'Вид документа', 12, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001021
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001021, 1000809, 0, 2, 80900800, NULL, NULL, 'Дата', 8, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001020
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001020, 1000809, 0, 1, 80900900, NULL, NULL, 'Номер', 8, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001026
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001026, 1000809, 0, 7, 80901000, NULL, NULL, 'Комментарий', 20, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001023
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001023, 1000809, 0, 4, 95000400, NULL, NULL, 'Пользователь', 15, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000507
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000507, 1000920, 0, 2, 92000200, NULL, 0, 'Наименование', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000508
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000508, 1000920, 0, 5, 92000600, NULL, 0, 'Общий', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Null.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', 0, NULL, NULL);
end $$;


--<DO>--
-- 1000509
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000509, 1000920, 0, 4, 92000800, NULL, 0, 'Дата последнего изменения', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000510
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000510, 1000920, 0, 3, 95000400, NULL, 0, 'Имя пользователя, создавшего список', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002103
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002103, 1000920, 2, 1, NULL, NULL, NULL, 'Столбец выбора', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000232
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000232, 1000930, 0, 1, 93000100, NULL, 0, 'Номер', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000233
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000233, 1000930, 0, 3, 93000200, NULL, 0, 'Код', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000234
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000234, 1000930, 0, 2, 93000300, NULL, 0, 'Наименование', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000237
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000237, 1000930, 0, 6, 93000400, NULL, 0, 'Таблица ALLPRI', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000238
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000238, 1000930, 0, 4, 93000500, NULL, 0, 'Таблица OBJECT', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000231
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000231, 1000930, 0, 5, 93000600, NULL, 0, 'Таблица QUANT', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100909
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100909, 1000930, 0, 7, 93000800, NULL, NULL, 'Тип хранения данных в реестрах', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000242
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000242, 1000931, 0, 1, 93100100, NULL, 0, 'Номер', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000243
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000243, 1000931, 0, 2, 93100200, NULL, 0, 'Наименование', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000241
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000241, 1000931, 0, 3, 93100300, NULL, 0, 'Номер реестра', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000247
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000247, 1000931, 0, 12, 93100500, NULL, 0, 'Родительский показатель - Номер', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000245
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000245, 1000931, 0, 10, 93100600, NULL, 0, 'Справочник - Номер', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000380
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000380, 1000931, 0, 5, 93100700, NULL, 0, 'Поле значения', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000381
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000381, 1000931, 0, 6, 93100800, NULL, 0, 'Поле справочного кода', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000244
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000244, 1000931, 0, 7, 93101000, NULL, 0, 'Главный ключ', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000379
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000379, 1000931, 0, 9, 93101300, NULL, 0, 'Код', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000382
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000382, 1000931, 3, 4, NULL, NULL, 0, 'Тип данных', NULL, 1, NULL, 0, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <Alias>Тип данных</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>93100400</Alias>
    <AttributeID>93100400</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>1</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>1 - Целое</Alias>
        <Value xsi:type="xsd:string">1 - Целое</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>2</Alias>
        <Value xsi:type="xsd:double">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>2 - Вещественное</Alias>
        <Value xsi:type="xsd:string">2 - Вещественное</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>3</Alias>
        <Value xsi:type="xsd:double">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>3 - Логическое</Alias>
        <Value xsi:type="xsd:string">3 - Логическое</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>4</Alias>
        <Value xsi:type="xsd:double">4</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>4 - Строка</Alias>
        <Value xsi:type="xsd:string">4 - Строка</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>5</Alias>
        <Value xsi:type="xsd:double">5</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>5 - Дата</Alias>
        <Value xsi:type="xsd:string">5 - Дата</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>6</Alias>
        <Value xsi:type="xsd:double">6</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>6 - Массив байт</Alias>
        <Value xsi:type="xsd:string">6 - Массив байт</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1000377
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000377, 1000931, 3, 8, NULL, NULL, 0, 'Виртуальный', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Да</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnIf">
  <Alias>Виртуальный</Alias>
  <Blocks>
    <QSColumnIfBlock>
      <Condition xsi:type="QSConditionSimple">
        <ConditionType>IsNotNull</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>93101200</Alias>
          <AttributeID>93101200</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperandLevel>0</RightOperandLevel>
      </Condition>
      <Result xsi:type="QSColumnConstant">
        <Alias>Да</Alias>
        <Value xsi:type="xsd:string">Да</Value>
      </Result>
    </QSColumnIfBlock>
    <QSColumnIfBlock>
      <Result xsi:type="QSColumnConstant">
        <Alias>Пусто</Alias>
      </Result>
    </QSColumnIfBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1000378
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000378, 1000931, 3, 11, NULL, NULL, 0, 'Справочник - Наименование', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Справочник - Наименование</Alias>
  <SubQuery>
    <MainRegisterID>982</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>98200200</Alias>
        <AttributeID>98200200</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>98200100</Alias>
        <AttributeID>98200100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>93100600</Alias>
        <AttributeID>93100600</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy />
    <GroupBy />
    <JoinType>Inner</JoinType>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000248
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000248, 1000931, 3, 13, NULL, NULL, 0, 'Родительский показатель - Наименование', NULL, 1, NULL, 0, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Родительский показатель - Наименование</Alias>
  <SubQuery>
    <MainRegisterID>931</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>93100200</Alias>
        <AttributeID>93100200</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>93100100</Alias>
        <AttributeID>93100100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>93100500</Alias>
        <AttributeID>93100500</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy />
    <RegisterLinks />
    <JoinType>Inner</JoinType>
    <Joins />
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000384
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000384, 1000932, 0, 5, 93000100, NULL, 0, 'Дочерний реестр - Номер', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000385
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000385, 1000932, 0, 6, 93000300, NULL, 0, 'Дочерний реестр - Наименование', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000389
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000389, 1000932, 0, 8, 93100200, NULL, 0, 'Наименование показателя дочернего реестра', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000390
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000390, 1000932, 0, 1, 93200100, NULL, 0, 'Номер связи', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000383
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000383, 1000932, 0, 2, 93200200, NULL, 0, 'Наименование связи', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000391
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000391, 1000932, 0, 7, 93200600, NULL, 0, 'Номер показателя дочернего реестра', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000386
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000386, 1000932, 0, 3, 99300100, NULL, 0, 'Родительский реестр - Номер', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000387
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000387, 1000932, 0, 4, 99300300, NULL, 0, 'Родительский реестр - Наименование', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000544
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000544, 1000933, 0, 7, 93300100, NULL, NULL, 'Идентификатор раскладки', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>true</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>{Get:RequestObjectId}</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>true</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000392
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000392, 1000933, 0, 1, 93300200, NULL, 0, 'Название', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000393
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000393, 1000933, 0, 2, 93300300, NULL, 0, 'Комментарий', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000394
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000394, 1000933, 0, 5, 93300600, NULL, 0, 'По умолчанию', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000395
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000395, 1000933, 0, 4, 93300800, NULL, 0, 'Дата последнего изменения', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000396
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000396, 1000933, 0, 6, 93301300, NULL, 0, 'Общая', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000397
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000397, 1000933, 0, 3, 95000400, NULL, 0, 'Имя пользователя, создавшего раскладку', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1001983
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001983, 1000933, 3, 8, NULL, NULL, NULL, 'Дата последней выгрузки', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Дата последней выгрузки</Alias>
  <SubQuery>
    <MainRegisterID>956</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>НоваяКолонка_2</Alias>
        <FunctionType>Max</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>95600500</Alias>
            <AttributeID>95600500</AttributeID>
            <Type>Value</Type>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>95600200</Alias>
            <AttributeID>95600200</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <Alias>93300100</Alias>
            <AttributeID>93300100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <ManualJoin>false</ManualJoin>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy />
    <GroupBy />
    <JoinType xsi:nil="true" />
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000398
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000398, 1000936, 0, 1, 93600200, NULL, 0, 'Наименование фильтра', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000399
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000399, 1000936, 0, 2, 93600300, NULL, 0, 'Описание фильтра', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000400
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000400, 1000936, 0, 4, 93600500, NULL, 0, 'Дата последнего изменения', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000401
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000401, 1000936, 0, 5, 93600600, NULL, 0, 'В списке', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Null.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000402
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000402, 1000936, 0, 6, 93601100, NULL, 0, 'Общий', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Null.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000403
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000403, 1000936, 0, 3, 95000400, NULL, 0, 'Имя пользователя, создавшего фильтр', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000221
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000221, 1000940, 0, 1, 94000400, NULL, 0, 'Время выполнения функции', NULL, 1, NULL, 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000222
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000222, 1000940, 0, 4, 94000500, NULL, 0, 'Признак успешного выполнения', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000223
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000223, 1000940, 0, 5, 94000600, NULL, 0, 'Комментарий по выполнению функции', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000220
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000220, 1000940, 0, 2, 95000400, NULL, 0, 'Пользователь', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000224
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000224, 1000940, 3, 3, NULL, NULL, 0, 'Наименование функции', NULL, 1, NULL, 2, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <SubQuery>
    <Columns>
      <QSColumn xsi:type="QSColumnIf">
        <Blocks>
          <QSColumnIfBlock>
            <Condition xsi:type="QSConditionSimple">
              <ConditionType>IsNull</ConditionType>
              <LeftOperand xsi:type="QSColumnSimple">
                <AttributeID>94200400</AttributeID>
                <Type>Value</Type>
              </LeftOperand>
              <LeftOperandLevel>2</LeftOperandLevel>
              <RightOperandLevel>2</RightOperandLevel>
            </Condition>
            <Result xsi:type="QSColumnSimple">
              <AttributeID>94200200</AttributeID>
              <Type>Value</Type>
            </Result>
          </QSColumnIfBlock>
          <QSColumnIfBlock>
            <Result xsi:type="QSColumnFunction">
              <FunctionType>Concatenation</FunctionType>
              <Operands>
                <QSColumn xsi:type="QSColumnQuery">
                  <SubQuery>
                    <Columns>
                      <QSColumn xsi:type="QSColumnSimple">
                        <AttributeID>94200200</AttributeID>
                        <Type>Value</Type>
                      </QSColumn>
                    </Columns>
                    <Condition xsi:type="QSConditionSimple">
                      <ConditionType>Equal</ConditionType>
                      <LeftOperand xsi:type="QSColumnSimple">
                        <AttributeID>94200100</AttributeID>
                        <Type>Value</Type>
                      </LeftOperand>
                      <LeftOperandLevel>3</LeftOperandLevel>
                      <RightOperand xsi:type="QSColumnSimple">
                        <AttributeID>94200400</AttributeID>
                        <Type>Value</Type>
                      </RightOperand>
                      <RightOperandLevel>2</RightOperandLevel>
                    </Condition>
                    <MainRegisterID>942</MainRegisterID>
                  </SubQuery>
                </QSColumn>
                <QSColumn xsi:type="QSColumnConstant">
                  <Value xsi:type="xsd:string"> - </Value>
                </QSColumn>
                <QSColumn xsi:type="QSColumnSimple">
                  <AttributeID>94200200</AttributeID>
                  <Type>Value</Type>
                </QSColumn>
              </Operands>
            </Result>
          </QSColumnIfBlock>
        </Blocks>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <AttributeID>94200100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>2</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <AttributeID>94000200</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
    <MainRegisterID>942</MainRegisterID>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000215
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000215, 1000949, 0, 1, 94900300, NULL, 0, 'Время входа в систему', NULL, 1, NULL, 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000216
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000216, 1000949, 0, 3, 94900400, NULL, 0, 'Время выхода из системы', NULL, 1, NULL, 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000217
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000217, 1000949, 0, 4, 94900500, NULL, 0, 'IP адрес клиентской машины', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000218
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000218, 1000949, 0, 5, 94900700, NULL, 0, 'Имя браузера', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000219
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000219, 1000949, 0, 6, 94900800, NULL, 0, 'Версия браузера', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000706
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000706, 1000949, 0, 8, 94901100, NULL, NULL, 'Проблема с входом', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000708
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000708, 1000949, 0, 9, 94901200, NULL, NULL, 'Примечание', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000214
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000214, 1000949, 0, 2, 95000400, NULL, 0, 'Пользователь', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000213
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000213, 1000949, 3, 7, NULL, NULL, 0, 'Количество действий', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <SubQuery>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>count</Alias>
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Value xsi:type="xsd:int">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <AttributeID>94900100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>1</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <AttributeID>94000700</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>2</RightOperandLevel>
    </Condition>
    <MainRegisterID>940</MainRegisterID>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000565
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000565, 1000955, 0, 6, 95500300, NULL, 0, 'ИД реестра', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000566
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000566, 1000955, 0, 4, 95500400, NULL, 0, 'ИД представления реестра', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000567
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000567, 1000955, 0, 2, 95500600, NULL, 0, 'Описание', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000882
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000882, 1000956, 0, 2, 95600400, NULL, NULL, 'Дата начала', 20, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000883
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000883, 1000956, 0, 3, 95600500, NULL, NULL, 'Дата окончания', 20, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000885
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000885, 1000956, 0, 5, 95600900, NULL, NULL, 'Количество строк в таблице', 10, 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000884
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000884, 1000956, 0, 4, 95601100, NULL, NULL, 'Тип выгрузки', 10, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000881
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000881, 1000956, 3, 1, NULL, NULL, NULL, 'Статус', 5, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Создана</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-edit</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Запущена</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-play</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Завершена</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Ошибка</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-close</ImagePath>
      <Text />
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <Alias>Статус</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>95600600</Alias>
    <AttributeID>95600600</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">0</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Создана</Alias>
        <Value xsi:type="xsd:string">Создана</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>1</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Создана</Alias>
        <Value xsi:type="xsd:string">Запущена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>2</Alias>
        <Value xsi:type="xsd:double">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Завершена</Alias>
        <Value xsi:type="xsd:string">Завершена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>3</Alias>
        <Value xsi:type="xsd:double">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Ошибка</Alias>
        <Value xsi:type="xsd:string">Ошибка</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1000886
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000886, 1000956, 3, 6, NULL, NULL, NULL, 'Автор', 35, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Автор</Alias>
  <SubQuery>
    <MainRegisterID>950</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>95000400</Alias>
        <AttributeID>95000400</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>95000100</Alias>
        <AttributeID>95000100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>95600300</Alias>
        <AttributeID>95600300</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
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
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000596
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000596, 1000960, 0, 1, 96000100, NULL, 0, 'Идентификатор', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000597
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000597, 1000960, 0, 2, 96000300, NULL, 0, 'Наименование', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000598
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000598, 1000960, 0, 3, 96000400, NULL, 0, 'Внутреннее имя', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000568
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000568, 1000965, 0, 7, 96300300, NULL, 0, 'Описание', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;


--<DO>--
-- 1000569
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000569, 1000965, 0, 1, 96400300, NULL, 0, 'Дата изменения', NULL, 1, NULL, 5, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;


--<DO>--
-- 1000570
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000570, 1000965, 0, 3, 96500400, NULL, 0, 'Идентификатор объекта в который внесены изменения', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;


--<DO>--
-- 1000571
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000571, 1000965, 3, 2, NULL, NULL, 0, 'Реестр', NULL, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Реестр</Alias>
  <SubQuery>
    <MainRegisterID>930</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>93000300</Alias>
        <AttributeID>93000300</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>93000100</Alias>
        <AttributeID>93000100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>96500300</Alias>
        <AttributeID>96500300</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
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
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000572
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000572, 1000965, 3, 4, NULL, NULL, 0, 'Автор', NULL, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Автор</Alias>
  <SubQuery>
    <MainRegisterID>950</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>95000400</Alias>
        <AttributeID>95000400</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>95000100</Alias>
        <AttributeID>95000100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>96400500</Alias>
        <AttributeID>96400500</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
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
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000573
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000573, 1000965, 3, 5, NULL, NULL, 0, 'Тип действия с объектом', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Изменен</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Edit.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Добавлен</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Add.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Удален</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Cancel.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Удален логически</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_ArrowLeft.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <Alias>Тип действия с объектом</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>96500600</Alias>
    <AttributeID>96500600</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>1</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Изменен</Alias>
        <Value xsi:type="xsd:string">Изменен</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>2</Alias>
        <Value xsi:type="xsd:double">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Добавлен</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>3</Alias>
        <Value xsi:type="xsd:double">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Удален</Alias>
        <Value xsi:type="xsd:string">Удален</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>4</Alias>
        <Value xsi:type="xsd:double">4</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Удален логически</Alias>
        <Value xsi:type="xsd:string">Удален логически</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1000574
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000574, 1000965, 3, 6, NULL, NULL, 0, 'Статус набора изменений', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Черновик</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Edit.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Подтверждено</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Отменено</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Cancel.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <Alias>Статус набора изменений</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>96400400</Alias>
    <AttributeID>96400400</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">0</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Черновик</Alias>
        <Value xsi:type="xsd:string">Черновик</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>1</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Подтверждено</Alias>
        <Value xsi:type="xsd:string">Подтверждено</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>2</Alias>
        <Value xsi:type="xsd:double">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Отменено</Alias>
        <Value xsi:type="xsd:string">Отменено</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1002118
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002118, 1000975, 0, 2, 97500600, NULL, NULL, 'Дата создания', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Left', NULL);
end $$;


--<DO>--
-- 1002039
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002039, 1000975, 0, 4, 97500700, NULL, NULL, 'Дата запуска', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Left', NULL);
end $$;


--<DO>--
-- 1002040
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002040, 1000975, 0, 5, 97500800, NULL, NULL, 'Дата завершения', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Left', NULL);
end $$;


--<DO>--
-- 1002038
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002038, 1000975, 0, 3, 97501000, NULL, NULL, 'Проверка статуса', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Left', NULL);
end $$;


--<DO>--
-- 1002043
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002043, 1000975, 0, 8, 97501100, NULL, NULL, 'ИД ошибки', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002055
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002055, 1000975, 0, 9, 97501200, NULL, NULL, 'Сообщение', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002041
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002041, 1000975, 0, 6, 97501300, NULL, NULL, 'ИД службы', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002042
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002042, 1000975, 0, 7, 97600200, NULL, NULL, 'Тип процесса', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002112
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002112, 1000975, 3, 1, NULL, NULL, NULL, 'Статус', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Выполняется</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-play</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Завершен с ошибкой</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-close</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Завершен успешно</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Остановлен</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-arrow-down</ImagePath>
      <Text />
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnSwitch">
  <Alias>ИД службы</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>97500900</Alias>
    <AttributeID>97500900</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">0</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Добавлен</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Подготовлен к запуску</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Выполняется</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Завершен успешно</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">4</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Завершен с ошибкой</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">5</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Отправлен запрос на остановку</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">6</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Остановлен</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1002056
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002056, 1000975, 3, 10, NULL, NULL, NULL, 'Пользователь', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnQuery">
  <Alias>Пользователь</Alias>
  <SubQuery>
    <MainRegisterID>950</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>95000400</Alias>
        <AttributeID>95000400</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>95000100</Alias>
        <AttributeID>95000100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>97500200</Alias>
        <AttributeID>97500200</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <ManualJoin>false</ManualJoin>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy />
    <GroupBy />
    <JoinType xsi:nil="true" />
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1002065
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002065, 1000975, 3, 11, NULL, NULL, NULL, 'Объект', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnFunctionExternal">
  <Alias>Объект</Alias>
  <FunctionName>Core_Register_PKG_GetUserKeyString</FunctionName>
  <Operands>
    <QSColumn xsi:type="QSColumnSimple">
      <Alias>97500400</Alias>
      <AttributeID>97500400</AttributeID>
      <Type>Value</Type>
    </QSColumn>
    <QSColumn xsi:type="QSColumnSimple">
      <Alias>97500500</Alias>
      <AttributeID>97500500</AttributeID>
      <Type>Value</Type>
    </QSColumn>
    <QSColumn xsi:type="QSColumnFunction">
      <Alias>Актуальные дата и время</Alias>
      <FunctionType>ActualDateTime</FunctionType>
      <Operands />
    </QSColumn>
  </Operands>
</QSColumn>');
end $$;


--<DO>--
-- 1002054
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002054, 1000976, 0, 1, 97600100, NULL, NULL, 'ИД', 5, 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002046
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002046, 1000976, 0, 3, 97600200, NULL, NULL, 'Системное имя процесса', 12, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002053
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002053, 1000976, 0, 12, 97600500, NULL, NULL, 'Интервал повторения', 15, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002047
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002047, 1000976, 0, 4, 97600600, NULL, NULL, 'Активен', 5, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Да</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Нет</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-close</ImagePath>
      <Text />
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002050
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002050, 1000976, 0, 10, 97600700, NULL, NULL, 'Количество запусков', 7, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002051
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002051, 1000976, 0, 11, 97600800, NULL, NULL, 'Количество ошибок', 7, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002048
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002048, 1000976, 0, 7, 97600900, NULL, NULL, 'Дата последнего запуска', 7, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Left', NULL);
end $$;


--<DO>--
-- 1002049
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002049, 1000976, 0, 9, 97601000, NULL, NULL, 'Продолжительность последнего выполнения (в тиках)', 7, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002067
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002067, 1000976, 0, 8, 97601100, NULL, NULL, 'Дата следующего запуска', 7, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Left', NULL);
end $$;


--<DO>--
-- 100097601
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100097601, 1000976, 0, 2, 97601300, NULL, NULL, 'Описание', 21, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002195
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002195, 1000976, 0, 6, 97601400, NULL, NULL, 'Тест', 5, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>0</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-close</ImagePath>
      <Text />
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002109
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002109, 1000976, 3, 5, NULL, NULL, NULL, 'По расписанию', 7, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnSwitch">
  <Alias>По расписанию</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>97600400</Alias>
    <AttributeID>97600400</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">0</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>По запросу</Alias>
        <Value xsi:type="xsd:string">По запросу</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>1</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>По расписанию</Alias>
        <Value xsi:type="xsd:string">По расписанию</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1002035
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002035, 1000977, 0, 4, 97700200, NULL, NULL, 'Параметры исполняемого файла', 50, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002032
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002032, 1000977, 0, 2, 97700300, NULL, NULL, 'Дата запуска', 10, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002034
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002034, 1000977, 0, 1, 97700400, NULL, NULL, 'Проверка статуса', 10, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002045
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002045, 1000977, 3, 3, NULL, NULL, NULL, 'Статус', 10, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Работает</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-play</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Остановлена</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Запрос на экстренную остановку</Value>
      <ValueId>0</ValueId>
      <ForeColor />
      <BackColor />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-close</ImagePath>
      <Text />
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnSwitch">
  <Alias>Статус</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>97700500</Alias>
    <AttributeID>97700500</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>1</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Работает</Alias>
        <Value xsi:type="xsd:string">Работает</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>2</Alias>
        <Value xsi:type="xsd:double">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Остановлена</Alias>
        <Value xsi:type="xsd:string">Остановлена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>3</Alias>
        <Value xsi:type="xsd:double">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Запрос на экстренную остановку</Alias>
        <Value xsi:type="xsd:string">Запрос на экстренную остановку</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1002036
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002036, 1000977, 3, 5, NULL, NULL, NULL, 'Активных процессов', 10, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnQuery">
  <Alias>Активных процессов</Alias>
  <SubQuery>
    <MainRegisterID>975</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>Количество</Alias>
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Alias>1</Alias>
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>97501300</Alias>
            <AttributeID>97501300</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <Alias>97700100</Alias>
            <AttributeID>97700100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>97500900</Alias>
            <AttributeID>97500900</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnConstant">
            <Alias>НоваяКолонка_4</Alias>
            <Value xsi:type="xsd:double">2</Value>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
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
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1002037
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002037, 1000977, 3, 6, NULL, NULL, NULL, 'Всего процессов', 10, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnQuery">
  <Alias>Всего процессов</Alias>
  <SubQuery>
    <MainRegisterID>975</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>Количество</Alias>
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Alias>1</Alias>
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>97501300</Alias>
        <AttributeID>97501300</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>97700100</Alias>
        <AttributeID>97700100</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
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
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000226
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000226, 1000982, 0, 1, 98200100, NULL, NULL, 'Главный ключ', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000227
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000227, 1000982, 0, 2, 98200200, NULL, NULL, 'Наименование справочника', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000225
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000225, 1000982, 3, 3, NULL, NULL, NULL, 'Родительский справочник', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <SubQuery>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <AttributeID>98200200</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <AttributeID>98200100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>2</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnQuery">
        <SubQuery>
          <Columns>
            <QSColumn xsi:type="QSColumnSimple">
              <AttributeID>98400400</AttributeID>
              <Type>Value</Type>
            </QSColumn>
          </Columns>
          <Condition xsi:type="QSConditionSimple">
            <ConditionType>Equal</ConditionType>
            <LeftOperand xsi:type="QSColumnSimple">
              <AttributeID>98400500</AttributeID>
              <Type>Value</Type>
            </LeftOperand>
            <LeftOperandLevel>3</LeftOperandLevel>
            <RightOperand xsi:type="QSColumnSimple">
              <AttributeID>98200100</AttributeID>
              <Type>Value</Type>
            </RightOperand>
            <RightOperandLevel>1</RightOperandLevel>
          </Condition>
          <MainRegisterID>984</MainRegisterID>
        </SubQuery>
      </RightOperand>
      <RightOperandLevel>0</RightOperandLevel>
    </Condition>
    <MainRegisterID>982</MainRegisterID>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000228
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000228, 1000982, 3, 4, NULL, NULL, NULL, 'Иерархический', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <ValueToCompare xsi:type="QSColumnSimple">
    <AttributeID>98200800</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:string">Да</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <Result xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:string" />
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1000230
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000230, 1000982, 3, 5, NULL, NULL, NULL, 'Табличный', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <ValueToCompare xsi:type="QSColumnSimple">
    <AttributeID>98200900</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:string">Да</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <Result xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:string" />
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1000229
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000229, 1000982, 3, 6, NULL, NULL, NULL, 'Только для чтения', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', 0, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <ValueToCompare xsi:type="QSColumnSimple">
    <AttributeID>98200400</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:string">Да</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <Result xsi:type="QSColumnConstant">
        <Value xsi:type="xsd:string" />
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1100901
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100901, 1000982, 3, 7, NULL, NULL, NULL, 'Связанные атрибуты', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnQuery">
  <Alias>Связанные атрибуты</Alias>
  <SubQuery>
    <MainRegisterID>931</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>Количество</Alias>
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Alias>1</Alias>
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>98200100</Alias>
        <AttributeID>98200100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>1</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>93100600</Alias>
        <AttributeID>93100600</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>0</RightOperandLevel>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <ManualJoin>false</ManualJoin>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy/>
    <GroupBy/>
    <JoinType xsi:nil="true"/>
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1000550
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000550, 1000986, 0, 6, 95300400, NULL, 0, 'Автор', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;


--<DO>--
-- 1000551
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000551, 1000986, 0, 1, 98600200, NULL, 0, 'Номер', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;


--<DO>--
-- 1000552
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000552, 1000986, 0, 4, 98600300, NULL, 0, 'Описание', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;


--<DO>--
-- 1000553
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000553, 1000986, 0, 5, 98600400, NULL, 0, 'Штрих-код', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;


--<DO>--
-- 1000554
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000554, 1000986, 0, 3, 98600500, NULL, 0, 'Тип', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;


--<DO>--
-- 1000555
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000555, 1000986, 0, 2, 98600800, NULL, 0, 'Дата', NULL, 1, 'ddMMyyyy', 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000250
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000250, 1000989, 0, 3, 95000400, NULL, 0, 'Пользователь', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000249
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000249, 1000989, 0, 1, 98900100, NULL, 0, 'ID ошибки', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000252
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000252, 1000989, 0, 4, 98900400, NULL, 0, 'Описание ошибки', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000254
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000254, 1000989, 0, 2, 98900600, NULL, 0, 'Дата ошибки', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000255
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000255, 1000989, 0, 5, 98900700, NULL, 0, 'Страница с ошибкой показана', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000256
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000256, 1000989, 0, 6, 98900800, NULL, 0, 'Тип сообщения', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000263
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000263, 1000992, 0, 3, 95000400, NULL, 0, 'Полное имя пользователя', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000257
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000257, 1000992, 0, 1, 99200100, NULL, 0, 'Номер сообщения', NULL, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000260
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000260, 1000992, 0, 4, 99200300, NULL, 0, 'Наименование подсистемы', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000261
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000261, 1000992, 0, 5, 99200400, NULL, 0, 'Наименование метода', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000262
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000262, 1000992, 0, 6, 99200500, NULL, 0, 'Дополнительный ключ', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000258
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000258, 1000992, 0, 2, 99200600, NULL, 0, 'Дата действия', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000259
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000259, 1000992, 3, 7, NULL, NULL, 0, 'Длительность (сек)', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, 0, NULL, 0, 'Right', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnFunction">
  <Alias>Длительность (сек)</Alias>
  <FunctionType>Divide</FunctionType>
  <Operands>
    <QSColumn xsi:type="QSColumnSimple">
      <Alias>99200700</Alias>
      <AttributeID>99200700</AttributeID>
      <Type>Value</Type>
    </QSColumn>
    <QSColumn xsi:type="QSColumnConstant">
      <Alias>10000*1000</Alias>
      <Value xsi:type="xsd:string">10000000</Value>
    </QSColumn>
  </Operands>
</QSColumn>');
end $$;


--<DO>--
-- 1000264
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000264, 1000992, 3, 8, NULL, NULL, 0, 'Длительность (мсек)', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, 0, NULL, 0, 'Right', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnFunction">
  <Alias>Длительность (мсек)</Alias>
  <FunctionType>Divide</FunctionType>
  <Operands>
    <QSColumn xsi:type="QSColumnSimple">
      <Alias>99200700</Alias>
      <AttributeID>99200700</AttributeID>
      <Type>Value</Type>
    </QSColumn>
    <QSColumn xsi:type="QSColumnConstant">
      <Alias>10000</Alias>
      <Value xsi:type="xsd:string">10000</Value>
    </QSColumn>
  </Operands>
</QSColumn>');
end $$;


--<DO>--
-- 1100864
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100864, 1002511, 0, 1, 25100200, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100870
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100870, 1002511, 0, 7, 25100300, NULL, NULL, 'Класс строения', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100871
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100871, 1002511, 0, 8, 25100400, NULL, NULL, 'Назначение', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100865
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100865, 1002511, 0, 3, 25100700, NULL, NULL, 'Год постройки', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100866
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100866, 1002511, 0, 2, 25100800, NULL, NULL, 'Кадастровый номер (К.Н.)', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100868
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100868, 1002511, 0, 5, 25101200, NULL, NULL, 'Площадь общая', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100867
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100867, 1002511, 0, 4, 25110800, NULL, NULL, 'Количество квартир', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100869
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100869, 1002511, 0, 6, 50000200, NULL, NULL, 'Полное наименование (5000200)', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100586
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100586, 1003011, 0, 7, 95000400, NULL, NULL, 'ФИО пользователя', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100541
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100541, 1003011, 0, 2, 301000200, NULL, NULL, 'Название файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../SkPackages/Transition?id=[ObjectId]" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100622
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100622, 1003011, 0, 5, 301000300, NULL, NULL, 'Тип файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100750
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100750, 1003011, 0, 3, 301000400, NULL, NULL, 'Период учета данных в Системе', NULL, 1, 'MMMMyyyy', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100540
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100540, 1003011, 0, 4, 301000700, NULL, NULL, 'Дата загрузки', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100542
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100542, 1003011, 0, 6, 301000800, NULL, NULL, 'Количество строк', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100623
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100623, 1003011, 0, 8, 301000900, NULL, NULL, 'Статус файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100624
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100624, 1003011, 2, 1, NULL, NULL, NULL, NULL, 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100302103
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100302103, 1003021, 0, 1, 302000100, NULL, NULL, 'Уникальный номер записи', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100756
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100756, 1003021, 0, 3, 302000800, NULL, NULL, 'Статус загрузки', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100754
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100754, 1003021, 0, 4, 302000900, NULL, NULL, 'Дата начала', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100755
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100755, 1003021, 0, 5, 302001000, NULL, NULL, 'Дата окончания', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100302101
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100302101, 1003021, 0, 2, 322000200, NULL, NULL, 'Наименование файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100526
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100526, 1003031, 0, 3, 301000700, NULL, NULL, 'Дата загрузки', NULL, 1, 'ddMMyyyy', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100847
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100847, 1003031, 0, 13, 301000900, NULL, NULL, 'Статус загрузки/обработки файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100528
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100528, 1003031, 0, 7, 303000600, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100525
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100525, 1003031, 0, 2, 303000700, NULL, NULL, 'Период учета', NULL, 1, 'MMMMyyyy', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100530
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100530, 1003031, 0, 9, 303000900, NULL, NULL, 'Номер платежа', NULL, 0, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100531
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100531, 1003031, 0, 10, 303001000, NULL, NULL, 'Всего по ЕПД', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100529
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100529, 1003031, 0, 8, 303001600, NULL, NULL, 'Код поставщика', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100532
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100532, 1003031, 0, 11, 303001700, NULL, NULL, 'Сумма взноса (банк)', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100848
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100848, 1003031, 0, 14, 303002300, NULL, NULL, 'Признак распределения', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100832
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100832, 1003031, 0, 6, 304000400, NULL, NULL, 'Банковский день', NULL, 1, 'ddMMyy', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100908
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100908, 1003031, 0, 16, 306000600, NULL, NULL, 'Адрес дома', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100533
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100533, 1003031, 0, 12, 306001600, NULL, NULL, 'Сумма взноса (МФЦ)', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100902
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100902, 1003031, 0, 15, 306002200, NULL, NULL, 'Статус идентификации записи', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100752
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100752, 1003031, 0, 4, 320000400, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100527
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100527, 1003031, 0, 5, 321000200, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100685
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100685, 1003031, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100538
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100538, 1003051, 0, 19, 301000700, NULL, NULL, 'Дата загрузки', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100910
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100910, 1003051, 0, 17, 301000900, NULL, NULL, 'Статус загрузки/обработки файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100823
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100823, 1003051, 0, 12, 305000300, NULL, NULL, 'Ссылка на реестр ФСП INSUR_FSP', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100500
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100500, 1003051, 0, 18, 305000500, NULL, NULL, 'Статус идентификации', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100537
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100537, 1003051, 0, 2, 305000700, NULL, NULL, 'Период учета', NULL, 1, 'MMMMyyyy', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100690
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100690, 1003051, 0, 5, 305001200, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100495
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100495, 1003051, 0, 6, 305001300, NULL, NULL, 'Адрес дома ', NULL, 0, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100496
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100496, 1003051, 0, 7, 305001600, NULL, NULL, 'Номер квартиры', NULL, 0, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100536
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100536, 1003051, 0, 10, 305002000, NULL, NULL, 'Кол-во комнат', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100497
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100497, 1003051, 0, 14, 305002100, NULL, NULL, 'Общая площадь', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100498
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100498, 1003051, 0, 15, 305002200, NULL, NULL, 'Площадь страхования', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100691
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100691, 1003051, 0, 11, 305002300, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100499
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100499, 1003051, 0, 16, 305002500, NULL, NULL, 'Сумма', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100770
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100770, 1003051, 0, 13, 308000400, NULL, NULL, 'Номер  ФСП ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[305000300]&amp;RegisterViewId=FspEpd&amp;isVertical=true&amp;useMasterPage=true" Target="Self" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100751
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100751, 1003051, 0, 3, 320000400, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100539
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100539, 1003051, 0, 4, 321000200, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100534
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100534, 1003051, 0, 8, 332000400, NULL, NULL, 'Статус ЖП', NULL, 0, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100535
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100535, 1003051, 0, 9, 333000400, NULL, NULL, 'Тип ЖП', NULL, 0, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100683
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100683, 1003051, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100763
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100763, 1003061, 0, 13, 301000700, NULL, NULL, 'Дата загрузки', NULL, 0, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100858
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100858, 1003061, 0, 20, 301000900, NULL, NULL, 'Статус загрузки/обработки файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100851
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100851, 1003061, 0, 18, 303001300, NULL, NULL, 'Дата платежа (банк)', NULL, 0, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100849
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100849, 1003061, 0, 16, 303001700, NULL, NULL, 'Сумма платежа по коду (банк)', NULL, 0, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100850
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100850, 1003061, 0, 17, 303002300, NULL, NULL, 'Признак распределения (банк)', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100825
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100825, 1003061, 0, 14, 306000300, NULL, NULL, 'Ссылка на реестр ФСП INSUR_FSP', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100759
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100759, 1003061, 0, 6, 306000500, NULL, NULL, 'UNOM', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100758
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100758, 1003061, 0, 5, 306000600, NULL, NULL, 'Адрес дома', NULL, 0, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100760
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100760, 1003061, 0, 7, 306000700, NULL, NULL, 'Номер квартиры', NULL, 0, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100761
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100761, 1003061, 0, 9, 306000800, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100824
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100824, 1003061, 0, 12, 306001100, NULL, NULL, 'Дата оплаты', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100768
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100768, 1003061, 0, 2, 306001400, NULL, NULL, 'Период учета', NULL, 1, 'MMMMyyyy', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100767
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100767, 1003061, 0, 11, 306001600, NULL, NULL, 'Сумма МФЦ', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100762
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100762, 1003061, 0, 10, 306001800, NULL, NULL, 'Площадь страхования', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100766
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100766, 1003061, 0, 19, 306002200, NULL, NULL, 'Статус идентификации записи', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100769
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100769, 1003061, 0, 15, 308000400, NULL, NULL, 'Номер  ФСП ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[306000300]&amp;RegisterViewId=FspEpd&amp;isVertical=true&amp;useMasterPage=true" Target="Self" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100764
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100764, 1003061, 0, 3, 320000400, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100757
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100757, 1003061, 0, 4, 321000200, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100765
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100765, 1003061, 0, 8, 333000400, NULL, NULL, 'Тип ЖП', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100692
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100692, 1003061, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100744
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100744, 1003062, 0, 13, 301000700, NULL, NULL, 'Дата загрузки', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100747
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100747, 1003062, 0, 6, 306000500, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100737
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100737, 1003062, 0, 5, 306000600, NULL, NULL, 'Адрес дома', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100738
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100738, 1003062, 0, 7, 306000700, NULL, NULL, 'Номер квартиры', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100748
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100748, 1003062, 0, 9, 306000800, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100743
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100743, 1003062, 0, 2, 306001400, NULL, NULL, 'Период учета', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100740
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100740, 1003062, 0, 11, 306001500, NULL, NULL, 'Сумма платежа', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100739
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100739, 1003062, 0, 10, 306001800, NULL, NULL, 'Площадь страхования', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100741
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100741, 1003062, 0, 12, 306002200, NULL, NULL, 'Статус идентификации', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100745
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100745, 1003062, 0, 3, 320000300, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100746
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100746, 1003062, 0, 4, 321000200, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100742
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100742, 1003062, 0, 8, 333000400, NULL, NULL, 'Тип ЖП', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100749
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100749, 1003062, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100905
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100905, 1003081, 0, 6, 308000100, NULL, NULL, 'Уникальный номер записи', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100524
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100524, 1003081, 0, 1, 308000400, NULL, NULL, 'Номер  ФСП ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=FspEpd&amp;isVertical=true&amp;useMasterPage=true"  />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100662
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100662, 1003081, 0, 3, 308000500, NULL, NULL, 'Номер лицевого счета', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100668
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100668, 1003081, 0, 4, 308001000, NULL, NULL, 'Дата создания ФСП', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100681
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100681, 1003081, 0, 2, 308001100, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100906
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100906, 1003081, 0, 7, 316000900, NULL, NULL, 'UNOM МКД', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100907
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100907, 1003081, 0, 8, 317000600, NULL, NULL, 'UNOM ЖП', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100881
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100881, 1003081, 0, 5, 319000300, NULL, NULL, 'Адрес объекта', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100677
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100677, 1003082, 0, 2, 308000400, NULL, NULL, 'Номер  ФСП ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=FspPolicy&amp;isVertical=true&amp;useMasterPage=true" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100679
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100679, 1003082, 0, 4, 308000500, NULL, NULL, 'Номер лицевого счета', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100680
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100680, 1003082, 0, 5, 308001000, NULL, NULL, 'Дата создания ФСП', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100678
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100678, 1003082, 0, 3, 308001100, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100882
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100882, 1003082, 0, 6, 319000300, NULL, NULL, 'Адрес объекта', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100753
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100753, 1003082, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100675
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100675, 1003083, 0, 1, 308000400, NULL, NULL, 'Номер  ФСП ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=FspSvd&amp;isVertical=true&amp;useMasterPage=true" />
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100676
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100676, 1003083, 0, 3, 308000500, NULL, NULL, 'Номер лицевого счета', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100673
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100673, 1003083, 0, 4, 308001000, NULL, NULL, 'Дата создания ФСП', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100674
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100674, 1003083, 0, 2, 308001100, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100883
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100883, 1003083, 0, 5, 319000300, NULL, NULL, 'Адрес объекта', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100308403
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100308403, 1003084, 0, 3, 308000200, NULL, 333, 'Тип ФСП', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100308401
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100308401, 1003084, 0, 1, 308000400, NULL, NULL, 'Номер ФСП', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100308402
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100308402, 1003084, 0, 2, 308001100, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100682
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100682, 1003091, 0, 1, 301000400, NULL, NULL, 'Период учета', NULL, 1, 'MMMMyyyy', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100628
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100628, 1003091, 0, 15, 309001000, NULL, NULL, 'Управляющая организация ', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100625
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100625, 1003091, 0, 10, 309001200, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100503
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100503, 1003091, 0, 11, 309001400, NULL, NULL, 'Номер квартиры', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100504
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100504, 1003091, 0, 12, 309001800, NULL, NULL, 'Статус квартиры', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100627
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100627, 1003091, 0, 13, 309001900, NULL, NULL, 'Общая площадь', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100505
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100505, 1003091, 0, 14, 309002000, NULL, NULL, 'Площадь страхования', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100501
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100501, 1003091, 0, 5, 309002300, NULL, NULL, 'Номер полиса', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100502
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100502, 1003091, 0, 6, 309002400, NULL, NULL, 'Дата', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100626
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100626, 1003091, 0, 7, 309002600, NULL, NULL, 'Условия', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100507
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100507, 1003091, 0, 8, 309002800, NULL, NULL, 'Страховая сумма', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100506
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100506, 1003091, 0, 9, 309003000, NULL, NULL, 'Сумма уплаченной премии', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100570
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100570, 1003091, 0, 2, 320000400, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100522
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100522, 1003091, 0, 3, 321000200, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100566
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100566, 1003091, 0, 4, 328000300, NULL, NULL, 'Страховая компания', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100629
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100629, 1003092, 0, 12, 309001000, NULL, NULL, 'Управляющая организация', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100631
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100631, 1003092, 0, 8, 309001200, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100510
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100510, 1003092, 0, 9, 309001400, NULL, NULL, 'Номер квартиры', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100630
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100630, 1003092, 0, 10, 309001900, NULL, NULL, 'Общая площадь квартиры', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100511
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100511, 1003092, 0, 11, 309002000, NULL, NULL, 'Площадь страхования', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100632
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100632, 1003092, 0, 2, 309002100, NULL, NULL, 'Код плательщика', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100508
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100508, 1003092, 0, 3, 309002300, NULL, NULL, 'Номер свидетельства', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100509
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100509, 1003092, 0, 5, 309002400, NULL, NULL, 'Дата', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100633
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100633, 1003092, 0, 4, 309003000, NULL, NULL, 'Сумма взноса', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100569
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100569, 1003092, 0, 6, 320000400, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100568
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100568, 1003092, 0, 7, 321000200, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100567
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100567, 1003092, 0, 1, 328000300, NULL, NULL, 'Страховая компания', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100519
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100519, 1003101, 0, 7, 310000600, NULL, NULL, 'Форма объединения собственников', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100621
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100621, 1003101, 0, 5, 310000800, NULL, NULL, 'Наименование страхователя', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100517
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100517, 1003101, 0, 2, 310000900, NULL, NULL, 'Номер договора', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=Contracts&amp;isVertical=true&amp;useMasterPage=true" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100518
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100518, 1003101, 0, 3, 310001000, NULL, NULL, 'Дата договора', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100520
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100520, 1003101, 0, 9, 310001800, NULL, NULL, 'Доля г. Москвы в праве на имущество', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100620
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100620, 1003101, 0, 8, 310001900, NULL, NULL, 'Рассрочка платежа', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100521
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100521, 1003101, 0, 6, 310002000, NULL, NULL, 'Страховая премия', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100732
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100732, 1003101, 0, 10, 316000900, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100728
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100728, 1003101, 0, 4, 328000300, NULL, NULL, 'Страховая компания', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100701
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100701, 1003101, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100523
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100523, 1003121, 0, 10, 310000900, NULL, NULL, 'Номер договора', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100516
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100516, 1003121, 0, 11, 312000500, NULL, NULL, 'Доля ответственности СК', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100515
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100515, 1003121, 0, 9, 312000600, NULL, NULL, 'Стоимость дома', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100514
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100514, 1003121, 0, 8, 312001000, NULL, NULL, 'Площадь здания', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100512
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100512, 1003121, 0, 2, 312003500, NULL, NULL, 'Номер заявки', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=InsurCalculations&amp;isVertical=true&amp;useMasterPage=true" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100513
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100513, 1003121, 0, 4, 312003600, NULL, NULL, 'Дата заявки', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100648
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100648, 1003121, 0, 3, 312003800, NULL, NULL, 'Дата создания', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100663
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100663, 1003121, 0, 5, 312004200, NULL, NULL, 'Статус расчета', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100578
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100578, 1003121, 0, 7, 316000900, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100846
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100846, 1003121, 0, 6, 328000300, NULL, NULL, 'Страховая компания', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100723
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100723, 1003121, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100544
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100544, 1003122, 0, 2, 312003500, NULL, NULL, 'Номер заявки', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100545
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100545, 1003122, 0, 3, 312003800, NULL, NULL, 'Дата создания', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100543
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100543, 1003122, 0, 1, 312004200, NULL, NULL, 'Статус расчета', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100653
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100653, 1003131, 0, 5, 313001100, NULL, NULL, 'Дата ущерба', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100654
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100654, 1003131, 0, 6, 313001400, NULL, NULL, 'Подлежит выплате СК', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100655
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100655, 1003131, 0, 7, 313001500, NULL, NULL, 'Размер доплаты', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100657
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100657, 1003131, 0, 9, 313001600, NULL, NULL, 'Расчетная стоимость', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100660
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100660, 1003131, 0, 12, 313001700, NULL, NULL, 'Страховая сумма', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100661
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100661, 1003131, 0, 13, 313002400, NULL, NULL, 'Сумма ущерба', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100656
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100656, 1003131, 0, 8, 313002500, NULL, NULL, 'Размер субсидии', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100652
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100652, 1003131, 0, 4, 313003000, NULL, NULL, 'Дата', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100659
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100659, 1003131, 0, 11, 313003200, NULL, NULL, 'Статус дела', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100649
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100649, 1003131, 0, 1, 313003400, NULL, NULL, 'Застрахован период/Не застрахован', NULL, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100658
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100658, 1003131, 0, 10, 313004100, NULL, NULL, 'Расчетный счет', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100651
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100651, 1003131, 0, 3, 313004300, NULL, NULL, 'БИК', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100650
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100650, 1003131, 0, 2, 313004600, NULL, NULL, 'Банк', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100716
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100716, 1003132, 0, 5, 313001100, NULL, NULL, 'Дата ущерба', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100546
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100546, 1003132, 0, 6, 313002400, NULL, NULL, 'Сумма ущерба, СК', NULL, 1, 'TwoDecimalDigitsWithSeparator', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100717
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100717, 1003132, 0, 7, 313003200, NULL, NULL, 'Сумма ущерба,Система', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100713
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100713, 1003132, 0, 8, 313005900, NULL, NULL, 'Статус дела', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100711
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100711, 1003132, 0, 2, 313006100, NULL, NULL, 'Номер дела', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=DamageAnalysisGP&amp;isVertical=true&amp;useMasterPage=true" />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100712
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100712, 1003132, 0, 3, 313006200, NULL, NULL, 'Дата', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100724
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100724, 1003132, 0, 9, 317000600, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100718
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100718, 1003132, 0, 10, 317000700, NULL, NULL, 'Номер квартиры', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100714
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100714, 1003132, 0, 4, 328000300, NULL, NULL, 'Название страховой компании', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100700
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100700, 1003132, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100313304
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313304, 1003133, 0, 5, 313001100, NULL, NULL, 'Дата ущерба', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313305
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313305, 1003133, 0, 6, 313002400, NULL, NULL, 'Сумма ущерба, СК', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313311
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313311, 1003133, 0, 7, 313003200, NULL, NULL, 'Сумма ущерба,Система', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313306
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313306, 1003133, 0, 8, 313005900, NULL, NULL, 'Статус дела', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313310
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313310, 1003133, 0, 2, 313006100, NULL, NULL, 'Номер дела', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=DamageAnalysisOI&amp;isVertical=true&amp;useMasterPage=true" />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313302
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313302, 1003133, 0, 3, 313006200, NULL, NULL, 'Дата', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313308
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313308, 1003133, 0, 10, 317000600, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313307
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313307, 1003133, 0, 9, 317000700, NULL, NULL, 'Номер квартиры', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313309
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313309, 1003133, 0, 11, 317001400, NULL, NULL, 'Площадь квартиры', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313303
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313303, 1003133, 0, 4, 328000300, NULL, NULL, 'Название страховой компании', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100313301
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100313301, 1003133, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100642
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100642, 1003141, 0, 9, 314000800, NULL, NULL, 'Округ', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100646
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100646, 1003141, 0, 10, 314000900, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100643
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100643, 1003141, 0, 11, 314001000, NULL, NULL, 'Номер квартиры', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100644
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100644, 1003141, 0, 4, 314001200, NULL, NULL, 'Номер договора', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100638
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100638, 1003141, 0, 5, 314001300, NULL, NULL, 'Дата начала действия договора', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100647
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100647, 1003141, 0, 12, 314001400, NULL, NULL, 'Номер акта', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100637
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100637, 1003141, 0, 13, 314001500, NULL, NULL, 'Дата акта', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100640
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100640, 1003141, 0, 6, 314001600, NULL, NULL, 'Размер ущерба', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100645
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100645, 1003141, 0, 8, 314001700, NULL, NULL, 'Сумма выплаты', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100641
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100641, 1003141, 0, 7, 314001800, NULL, NULL, 'Удержанная часть страхового возмещения', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100639
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100639, 1003141, 0, 14, 314002000, NULL, NULL, 'Дата пп', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100733
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100733, 1003141, 0, 2, 328000300, NULL, NULL, 'Страховая компания', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100729
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100729, 1003141, 0, 3, 345000500, NULL, NULL, 'Наименование страхователя', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100731
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100731, 1003141, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100634
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100634, 1003142, 0, 4, 314000900, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100636
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100636, 1003142, 0, 5, 314001000, NULL, NULL, 'Номер квартиры*', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100554
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100554, 1003142, 0, 2, 314001200, NULL, NULL, 'Номер договора', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100555
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100555, 1003142, 0, 3, 314001300, NULL, NULL, 'Дата договора', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100556
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100556, 1003142, 0, 9, 314001400, NULL, NULL, 'Номер акта', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100557
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100557, 1003142, 0, 10, 314001500, NULL, NULL, 'Дата акта', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100553
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100553, 1003142, 0, 6, 314001600, NULL, NULL, 'Размер ущерба', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100551
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100551, 1003142, 0, 7, 314001700, NULL, NULL, 'Сумма возмещения', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100552
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100552, 1003142, 0, 8, 314001800, NULL, NULL, 'Удержанная часть ', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100635
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100635, 1003142, 0, 11, 314002000, NULL, NULL, 'Дата пп', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100550
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100550, 1003142, 0, 1, 328000300, NULL, NULL, 'Страховая компания', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100610
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100610, 1003151, 0, 9, 315001500, NULL, NULL, 'Дата начала действия договора', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100612
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100612, 1003151, 0, 10, 315001600, NULL, NULL, 'Дата страхового события', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100609
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100609, 1003151, 0, 12, 315001700, NULL, NULL, 'Дата заявления о страховом событии', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100617
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100617, 1003151, 0, 15, 315001800, NULL, NULL, 'Причина отказа в страховой выплате ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100616
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100616, 1003151, 0, 3, 315002100, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100619
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100619, 1003151, 0, 4, 315002200, NULL, NULL, 'Форма объединения собственников', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100618
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100618, 1003151, 0, 7, 315002400, NULL, NULL, 'Наименование страхователя', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100615
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100615, 1003151, 0, 8, 315002500, NULL, NULL, 'Номер договора', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100611
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100611, 1003151, 0, 11, 315002700, NULL, NULL, 'Дата сообщения о страховом событии', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100613
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100613, 1003151, 0, 13, 315003000, NULL, NULL, 'Причина страхового события', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100614
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100614, 1003151, 0, 14, 315003100, NULL, NULL, 'Причина отсутствия решения', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100734
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100734, 1003151, 0, 2, 320000400, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100735
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100735, 1003151, 0, 5, 328000300, NULL, NULL, 'Страховая организация', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100736
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100736, 1003151, 0, 6, 345000500, NULL, NULL, 'Управляющая компания', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100730
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100730, 1003151, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100561
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100561, 1003152, 0, 2, 315001400, NULL, NULL, 'Номер договора', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100562
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100562, 1003152, 0, 3, 315001500, NULL, NULL, 'Дата договора', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100559
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100559, 1003152, 0, 4, 315001600, NULL, NULL, 'Дата страхового события', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100560
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100560, 1003152, 0, 5, 315001700, NULL, NULL, 'Дата заявления', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100563
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100563, 1003152, 0, 6, 315001800, NULL, NULL, 'Причина отказа', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100564
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100564, 1003152, 0, 7, 315001900, NULL, NULL, 'Номер письма об отказе', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100565
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100565, 1003152, 0, 8, 315002000, NULL, NULL, 'Дата письма об отказе', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100558
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100558, 1003152, 0, 1, 328000300, NULL, NULL, 'Страховая компания', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100779
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100779, 1003161, 0, 3, 258000300, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100585
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100585, 1003161, 0, 4, 259000300, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100580
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100580, 1003161, 0, 2, 316000300, NULL, NULL, 'Кадастровый номер МКД', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100584
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100584, 1003161, 0, 1, 316000900, NULL, NULL, 'UNOM МКД', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100579
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100579, 1003161, 0, 5, 316001100, NULL, NULL, 'Год постройки', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100582
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100582, 1003161, 0, 7, 316001200, NULL, NULL, 'Кол-во этажей', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100581
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100581, 1003161, 0, 6, 316001300, NULL, NULL, 'Кол-во квартир в доме', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100583
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100583, 1003161, 0, 8, 316001400, NULL, NULL, 'Общая площадь', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100833
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100833, 1003162, 0, 3, 258000300, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100897
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100897, 1003162, 0, 11, 259000300, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100593
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100593, 1003162, 0, 2, 316000300, NULL, NULL, 'Кадастровый номер', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=Tenements&amp;isVertical=true&amp;useMasterPage=true" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100592
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100592, 1003162, 0, 1, 316000900, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=Tenements&amp;isVertical=true&amp;useMasterPage=true" />
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>пусто</Condition>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text>Н/А</Text>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100594
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100594, 1003162, 0, 5, 316001100, NULL, NULL, 'Год постройки', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100596
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100596, 1003162, 0, 7, 316001300, NULL, NULL, 'Кол-во квартир', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100595
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100595, 1003162, 0, 6, 316001400, NULL, NULL, 'Общая площадь', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100597
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100597, 1003162, 0, 8, 316002700, NULL, NULL, 'Программа', NULL, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text>-</Text>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'Center', NULL);
end $$;


--<DO>--
-- 1100664
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100664, 1003162, 0, 9, 316003200, NULL, NULL, 'БТИ', NULL, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text>-</Text>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'Center', NULL);
end $$;


--<DO>--
-- 1100665
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100665, 1003162, 0, 10, 316004300, NULL, NULL, 'ЕГРН', NULL, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text>-</Text>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'Center', NULL);
end $$;


--<DO>--
-- 1100722
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100722, 1003162, 0, 4, 319000300, NULL, NULL, 'Адрес', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100898
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100898, 1003171, 0, 4, 258000300, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100899
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100899, 1003171, 0, 5, 259000300, NULL, NULL, 'Район', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100671
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100671, 1003171, 0, 2, 317000300, NULL, NULL, 'Кадастровый номер', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=LivingSpaces&amp;isVertical=true&amp;useMasterPage=true" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100599
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100599, 1003171, 0, 3, 317000600, NULL, NULL, 'UNOM', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100598
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100598, 1003171, 0, 1, 317000700, NULL, NULL, 'Номер квартиры', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=LivingSpaces&amp;isVertical=true&amp;useMasterPage=true" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100880
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100880, 1003171, 0, 8, 317001200, NULL, NULL, 'Кол-во комнат в квартире', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100900
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100900, 1003171, 0, 7, 317001300, NULL, NULL, 'Общая площадь', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100669
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100669, 1003171, 0, 9, 317002200, NULL, NULL, 'Программа', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text>-</Text>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'Center', NULL);
end $$;


--<DO>--
-- 1100666
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100666, 1003171, 0, 10, 317002800, NULL, NULL, 'БТИ', NULL, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text>-</Text>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'Center', NULL);
end $$;


--<DO>--
-- 1100667
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100667, 1003171, 0, 11, 317002900, NULL, NULL, 'ЕГРН', NULL, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text>-</Text>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'Center', NULL);
end $$;


--<DO>--
-- 1100787
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100787, 1003171, 0, 6, 319000300, NULL, NULL, 'Адрес МКД ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100696
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100696, 1003231, 0, 2, 32300300, NULL, NULL, 'Документ-основание', 90, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100695
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100695, 1003231, 0, 1, 32300400, NULL, NULL, 'Вид', 10, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100684
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100684, 1003241, 0, 1, 324000200, NULL, NULL, 'Признаки  ущерба', 60, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100693
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100693, 1003241, 0, 2, 324000700, NULL, NULL, 'Минимальный процент урона', 20, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100694
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100694, 1003241, 0, 3, 324000800, NULL, NULL, 'Максимальный процент урона', 20, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100589
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100589, 1003251, 0, 1, 32500200, NULL, NULL, 'Период', NULL, 1, 'MMMMyyyy', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100591
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100591, 1003251, 0, 3, 32500400, NULL, NULL, 'Районов всего', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100686
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100686, 1003251, 0, 4, 32500500, NULL, NULL, 'Начисления.Загружено файлов ', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100687
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100687, 1003251, 0, 5, 32500600, NULL, NULL, 'Начисления.Обработано файлов', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100688
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100688, 1003251, 0, 6, 32500700, NULL, NULL, 'Зачисления.Загружено файлов', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100689
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100689, 1003251, 0, 7, 32500800, NULL, NULL, 'Зачисления. Обработано файлов', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100826
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100826, 1003251, 0, 8, 32500900, NULL, NULL, 'Банковские строки. Загружено файлов', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100590
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100590, 1003251, 0, 2, 320000400, NULL, NULL, 'Округ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[ObjectId]&amp;RegisterViewId=InputPackageFile&amp;isVertical=true&amp;useMasterPage=true&amp;hideSidePanel=false" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100697
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100697, 1003281, 0, 3, 328000200, NULL, NULL, 'Наименование страховой организации', 70, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100698
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100698, 1003281, 0, 2, 328000300, NULL, NULL, 'Сокращенное наименование страховой компании', 20, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100699
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100699, 1003281, 0, 1, 328000400, NULL, NULL, 'Код', 10, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100571
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100571, 1003441, 0, 1, 344000200, NULL, NULL, 'Наименование банка', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100828
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100828, 1003441, 0, 3, 344000400, NULL, NULL, 'ИНН банка', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100829
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100829, 1003441, 0, 4, 344000500, NULL, NULL, 'КПП банка', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100827
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100827, 1003441, 0, 2, 344000600, NULL, NULL, 'БИК банка', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100830
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100830, 1003441, 0, 5, 344000700, NULL, NULL, 'Корреспондентский счет', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100784
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100784, 1003451, 0, 5, 344000200, NULL, NULL, 'Наименование банка', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100785
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100785, 1003451, 0, 6, 344000600, NULL, NULL, 'БИК банка', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100786
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100786, 1003451, 0, 7, 344000700, NULL, NULL, 'Корреспондентский счет', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100781
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100781, 1003451, 0, 2, 345000500, NULL, NULL, 'Наименование субъекта', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100782
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100782, 1003451, 0, 3, 345001300, NULL, NULL, 'Дата рождения (для физ лиц)', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100783
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100783, 1003451, 0, 4, 345001400, NULL, NULL, 'ИНН', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100780
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100780, 1003451, 0, 1, 345002300, NULL, NULL, 'Тип субъекта', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100602
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100602, 1003481, 0, 1, 348000200, NULL, NULL, 'Дата начала действия', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100604
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100604, 1003481, 0, 3, 348000300, NULL, NULL, 'Условие', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100603
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100603, 1003481, 0, 2, 348000400, NULL, NULL, 'Значение, руб.', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100605
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100605, 1003491, 0, 1, 349000200, NULL, NULL, 'Дата начала  действия', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100607
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100607, 1003491, 0, 3, 349000400, NULL, NULL, 'Доля СК,%', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100606
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100606, 1003491, 0, 2, 349000500, NULL, NULL, 'Доля города,%', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100608
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100608, 1003491, 0, 4, 349000600, NULL, NULL, 'Примечание', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100841
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100841, 1003521, 0, 3, 95000300, NULL, NULL, 'Пользователь', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100839
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100839, 1003521, 0, 1, 352000100, NULL, NULL, 'Уникальный номер записи', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100840
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100840, 1003521, 0, 2, 352000400, NULL, NULL, 'Дата изменения ', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100842
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100842, 1003521, 0, 4, 352000500, NULL, NULL, 'Тип операции', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100845
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100845, 1003521, 0, 7, 352000600, NULL, NULL, 'Основание', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100844
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100844, 1003521, 0, 6, 352000800, NULL, NULL, 'Старое значение', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100843
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100843, 1003521, 0, 5, 352000900, NULL, NULL, 'Новое значение', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100703
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100703, 1003541, 0, 3, 354000200, NULL, NULL, 'Номер', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100702
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100702, 1003541, 0, 2, 354000300, NULL, NULL, 'Дата', NULL, 1, 'ddMMyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100721
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100721, 1003541, 0, 7, 354000400, NULL, NULL, 'Дата создания', NULL, 0, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100704
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100704, 1003541, 0, 4, 354000800, NULL, NULL, 'Статус', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100719
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100719, 1003541, 0, 5, 354001100, NULL, NULL, 'Реестр', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ReestrPay/DownloadFileDGI?Id=[ObjectId]" Target="Blank" IsNotNull="True" />
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>Существует</Condition>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-attachment</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>пусто</Condition>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text />
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'Center', NULL);
end $$;


--<DO>--
-- 1100831
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100831, 1003541, 0, 6, 354001200, NULL, NULL, 'Оплата', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ReestrPay/DownloadFilePay?Id=[ObjectId]" />
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>Существует</Condition>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-attachment</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>пусто</Condition>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath />
      <Text />
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100720
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100720, 1003541, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100710
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100710, 1003551, 0, 6, 313003200, NULL, NULL, 'Размер ущерба', NULL, 1, 'TwoDecimalDigits', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[355001300]&amp;RegisterViewId=DamageAnalysisGP&amp;isVertical=true&amp;useMasterPage=true" Target="Blank" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100706
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100706, 1003551, 0, 2, 355000200, NULL, NULL, 'Получатель', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100707
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100707, 1003551, 0, 3, 355000500, NULL, NULL, 'ИНН', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100715
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100715, 1003551, 0, 7, 355001300, NULL, NULL, 'Ссылка на INSUR_DAMAGE ( Реестр дел по расчету ущербов)', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100708
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100708, 1003551, 0, 4, 355001600, NULL, NULL, 'Сумма счета', NULL, 1, 'TwoDecimalDigits', 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Right', NULL);
end $$;


--<DO>--
-- 1100709
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100709, 1003551, 0, 5, 355001800, NULL, NULL, 'Статус счета', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100705
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100705, 1003551, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100355207
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100355207, 1003552, 0, 6, 310000900, NULL, NULL, 'Номер договора', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Link Url="../ObjectCard?ObjId=[355001400]&amp;RegisterViewId=Contracts&amp;isVertical=true&amp;useMasterPage=true" Target="Blank" />
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100355202
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100355202, 1003552, 0, 2, 355000200, NULL, NULL, 'Получатель', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100355203
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100355203, 1003552, 0, 3, 355000500, NULL, NULL, 'ИНН', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100355205
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100355205, 1003552, 0, 7, 355001400, NULL, NULL, 'Ссылка на договор', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100355206
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100355206, 1003552, 0, 4, 355001600, NULL, NULL, 'Сумма счета', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100355204
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100355204, 1003552, 0, 5, 355001800, NULL, NULL, 'Статус счета', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 100355201
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100355201, 1003552, 2, 1, NULL, NULL, NULL, 'Столбец выбора', 40, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100725
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100725, 1003571, 0, 1, 357000200, NULL, NULL, 'Причина', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100726
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100726, 1003571, 0, 2, 357000300, NULL, NULL, 'Вид страхования', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100727
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100727, 1003571, 0, 3, 357000400, NULL, NULL, 'Краткое пояснение (должно быть напечатано на заключении)', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100359102
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100359102, 1003591, 0, 2, 301000200, NULL, NULL, 'Название файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100359103
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100359103, 1003591, 0, 3, 301000400, NULL, NULL, 'Период', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100359101
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100359101, 1003591, 0, 1, 359000100, NULL, NULL, 'Идентификатор', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100359104
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100359104, 1003591, 0, 4, 359000600, NULL, NULL, 'Дата начала процесса идентификации', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 100359105
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100359105, 1003591, 0, 5, 359000700, NULL, NULL, 'Дата окончания процесса идентификации', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100856
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100856, 1003601, 0, 5, 36000500, NULL, NULL, 'Дата загрузки', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100853
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100853, 1003601, 0, 4, 36000600, NULL, NULL, 'Сообщение об ошибке', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100852
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100852, 1003601, 0, 3, 36000800, NULL, NULL, 'Статус', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>0</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-close</ImagePath>
      <Text />
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>k-i-check</ImagePath>
      <Text />
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100857
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100857, 1003601, 0, 6, 36000900, NULL, NULL, 'Дата обновления ЕГРН', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100854
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100854, 1003601, 0, 1, 36001000, NULL, NULL, 'Кадастровый номер', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100855
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100855, 1003601, 0, 2, 36001100, NULL, NULL, 'УНОМ', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100879
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100879, 1003601, 0, 7, 36001200, NULL, NULL, 'Дата обновления БТИ', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100888
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100888, 1003611, 0, 5, 36100500, NULL, NULL, 'Дата загрузки', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100887
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100887, 1003611, 0, 4, 36100600, NULL, NULL, 'Сообщение об ошибке', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100886
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100886, 1003611, 0, 3, 36100800, NULL, NULL, 'Признак ошибки', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100890
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100890, 1003611, 0, 7, 36100900, NULL, NULL, 'Дата обновления БТИ', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100889
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100889, 1003611, 0, 6, 36101000, NULL, NULL, 'Дата обновления ЕГРН', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100884
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100884, 1003611, 0, 1, 36101100, NULL, NULL, 'Кадастровый номер', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100885
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100885, 1003611, 0, 2, 36101200, NULL, NULL, 'УНОМ', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100771
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100771, 1003621, 0, 1, 362000200, NULL, NULL, 'GLOBAL_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100772
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100772, 1003621, 0, 2, 362000300, NULL, NULL, 'UPDATE_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100773
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100773, 1003621, 0, 3, 362000400, NULL, NULL, 'IS_ERROR', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100774
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100774, 1003621, 0, 4, 362000500, NULL, NULL, 'MESSAGE', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100775
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100775, 1003621, 0, 5, 362000600, NULL, NULL, 'ERROR_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100776
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100776, 1003621, 0, 6, 362000700, NULL, NULL, 'TASK_ID', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100777
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100777, 1003621, 0, 7, 362000800, NULL, NULL, 'INSERT_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100778
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100778, 1003621, 0, 8, 362000900, NULL, NULL, 'IMPORT_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100795
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100795, 1003641, 0, 1, 364000100, NULL, NULL, 'ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100796
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100796, 1003641, 0, 2, 364000200, NULL, NULL, 'GLOBAL_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100797
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100797, 1003641, 0, 3, 364000300, NULL, NULL, 'UPDATE_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100800
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100800, 1003641, 0, 6, 364000400, NULL, NULL, 'IS_ERROR', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100799
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100799, 1003641, 0, 5, 364000500, NULL, NULL, 'MESSAGE', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100801
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100801, 1003641, 0, 7, 364000600, NULL, NULL, 'ERROR_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100798
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100798, 1003641, 0, 4, 364000900, NULL, NULL, 'IMPORT_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100802
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100802, 1003651, 0, 1, 365000100, NULL, NULL, 'ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100803
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100803, 1003651, 0, 2, 365000200, NULL, NULL, 'GLOBAL_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100804
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100804, 1003651, 0, 3, 365000300, NULL, NULL, 'UPDATE_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100806
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100806, 1003651, 0, 5, 365000400, NULL, NULL, 'IS_ERROR', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100807
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100807, 1003651, 0, 6, 365000500, NULL, NULL, 'MESSAGE', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100808
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100808, 1003651, 0, 7, 365000600, NULL, NULL, 'ERROR_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100805
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100805, 1003651, 0, 4, 365000900, NULL, NULL, 'IMPORT_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100809
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100809, 1003661, 0, 1, 366000100, NULL, NULL, 'ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100810
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100810, 1003661, 0, 2, 366000200, NULL, NULL, 'GLOBAL_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100811
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100811, 1003661, 0, 3, 366000300, NULL, NULL, 'UPDATE_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100813
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100813, 1003661, 0, 5, 366000400, NULL, NULL, 'IS_ERROR', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100814
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100814, 1003661, 0, 6, 366000500, NULL, NULL, 'MESSAGE', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100815
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100815, 1003661, 0, 7, 366000600, NULL, NULL, 'ERROR_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100812
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100812, 1003661, 0, 4, 366000900, NULL, NULL, 'IMPORT_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100816
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100816, 1003671, 0, 1, 367000100, NULL, NULL, 'ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100817
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100817, 1003671, 0, 2, 367000200, NULL, NULL, 'GLOBAL_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100818
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100818, 1003671, 0, 3, 367000300, NULL, NULL, 'UPDATE_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100820
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100820, 1003671, 0, 5, 367000400, NULL, NULL, 'IS_ERROR', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100821
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100821, 1003671, 0, 6, 367000500, NULL, NULL, 'MESSAGE', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100822
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100822, 1003671, 0, 7, 367000600, NULL, NULL, 'ERROR_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100819
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100819, 1003671, 0, 4, 367000900, NULL, NULL, 'IMPORT_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100896
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100896, 1003691, 0, 2, 36900200, NULL, NULL, 'Кадастровый номер', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100891
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100891, 1003691, 0, 1, 36900300, NULL, NULL, 'УНОМ', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100892
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100892, 1003691, 0, 3, 36900600, NULL, NULL, 'Дата редактирования объекта в БТИ', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100894
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100894, 1003691, 0, 5, 36900700, NULL, NULL, 'Признак ошибки', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100895
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100895, 1003691, 0, 6, 36900800, NULL, NULL, 'Сообщение об ошибке', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100893
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100893, 1003691, 0, 4, 36901200, NULL, NULL, 'Дата обработки', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100370101
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100370101, 1003701, 0, 2, 301000200, NULL, NULL, 'Название файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100370102
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100370102, 1003701, 0, 3, 301000400, NULL, NULL, 'Период учета', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100370105
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100370105, 1003701, 0, 1, 370000100, NULL, NULL, 'Идентификатор', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100370103
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100370103, 1003701, 0, 4, 370000500, NULL, NULL, 'Дата начала', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 100370104
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (100370104, 1003701, 0, 5, 370000600, NULL, NULL, 'Дата окончания', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100903
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100903, 1003801, 0, 1, 38000200, NULL, NULL, 'Наименование аналитического показателя', 70, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100904
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100904, 1003801, 0, 2, 38000300, NULL, NULL, 'Значение', 30, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100873
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100873, 1004001, 0, 2, 400000400, NULL, NULL, 'Наименование', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100878
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100878, 1004001, 0, 7, 400000600, NULL, NULL, 'Площадь', NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100874
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100874, 1004001, 0, 3, 400000800, NULL, NULL, 'Назначение', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100872
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100872, 1004001, 0, 1, 400001400, NULL, NULL, 'Кадастровый номер', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100875
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100875, 1004001, 0, 6, 400001700, NULL, NULL, 'Дата загрузки', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100876
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100876, 1004001, 0, 4, 400001800, NULL, NULL, 'Кол-во этажей', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100877
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100877, 1004001, 0, 5, 400001900, NULL, NULL, 'Материал стен', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002084
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002084, 1009331, 0, 7, 93300100, NULL, NULL, 'Идентификатор раскладки', NULL, 0, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>true</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>{Get:RequestObjectId}</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>true</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002078
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002078, 1009331, 0, 1, 93300200, NULL, 0, 'Название', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002079
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002079, 1009331, 0, 2, 93300300, NULL, 0, 'Комментарий', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002082
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002082, 1009331, 0, 5, 93300600, NULL, 0, 'По умолчанию', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Null.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002081
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002081, 1009331, 0, 4, 93300800, NULL, 0, 'Дата последнего изменения', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002083
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002083, 1009331, 0, 6, 93301300, NULL, 0, 'Общая', NULL, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Null.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002080
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002080, 1009331, 0, 3, 95000400, NULL, 0, 'Имя пользователя, создавшего раскладку', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002085
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002085, 1009331, 3, 8, NULL, NULL, NULL, 'Дата последней выгрузки', NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Дата последней выгрузки</Alias>
  <SubQuery>
    <MainRegisterID>956</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>НоваяКолонка_2</Alias>
        <FunctionType>Max</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>95600500</Alias>
            <AttributeID>95600500</AttributeID>
            <Type>Value</Type>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>95600200</Alias>
            <AttributeID>95600200</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <Alias>93300100</Alias>
            <AttributeID>93300100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <ManualJoin>false</ManualJoin>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy />
    <GroupBy />
    <JoinType xsi:nil="true" />
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1100479
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100479, 1009401, 0, 3, 94000400, NULL, NULL, 'Дата выполнения', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100480
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100480, 1009401, 0, 6, 94000500, NULL, NULL, 'Признак успешного выполнения', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100481
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100481, 1009401, 0, 7, 94000600, NULL, NULL, 'Примечание', 30, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100482
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100482, 1009401, 0, 5, 94100300, NULL, NULL, 'Подразделение', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100483
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100483, 1009401, 0, 2, 94200200, NULL, NULL, 'Действие', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100484
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100484, 1009401, 0, 4, 95000400, NULL, NULL, 'Пользователь', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100485
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100485, 1009401, 3, 1, NULL, NULL, NULL, '№', 5, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <SubQuery>
    <MainRegisterID>940</MainRegisterID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000900</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000900</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000800</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000800</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>LessOrEqual</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000100</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1100486
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100486, 1009401, 3, 8, NULL, NULL, NULL, 'Статус', 10, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Действующая</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Отменен</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Cancel.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Проект</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Edit.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <SubQuery>
    <MainRegisterID>983</MainRegisterID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <AttributeID>98300400</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <AttributeID>98300100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <RightOperand xsi:type="QSColumnSimple">
        <AttributeID>94001000</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1100487
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100487, 1009402, 0, 3, 94000400, NULL, NULL, 'Дата выполнения', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100488
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100488, 1009402, 0, 6, 94000500, NULL, NULL, 'Признак успешного выполнения', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100489
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100489, 1009402, 0, 7, 94000600, NULL, NULL, 'Примечание', 30, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100490
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100490, 1009402, 0, 5, 94100300, NULL, NULL, 'Подразделение', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100491
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100491, 1009402, 0, 2, 94200200, NULL, NULL, 'Действие', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100492
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100492, 1009402, 0, 4, 95000400, NULL, NULL, 'Пользователь', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100493
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100493, 1009402, 3, 1, NULL, NULL, NULL, '№', 5, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <SubQuery>
    <MainRegisterID>940</MainRegisterID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000900</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000900</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000800</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000800</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>LessOrEqual</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000100</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1100494
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100494, 1009402, 3, 8, NULL, NULL, NULL, 'Статус', 10, 1, NULL, 4, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Действует</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Не действует</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Edit.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Удален</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Remove.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnIf">
  <Alias>Статус</Alias>
  <Blocks>
    <QSColumnIfBlock>
      <Condition xsi:type="QSConditionSimple">
        <ConditionType>Equal</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>94001000</Alias>
          <AttributeID>94001000</AttributeID>
          <Type>Value</Type>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperand xsi:type="QSColumnConstant">
          <Alias>1</Alias>
          <Value xsi:type="xsd:double">1</Value>
        </RightOperand>
        <RightOperandLevel>0</RightOperandLevel>
      </Condition>
      <Result xsi:type="QSColumnConstant">
        <Alias>Действует</Alias>
        <Value xsi:type="xsd:string">Действует</Value>
      </Result>
    </QSColumnIfBlock>
    <QSColumnIfBlock>
      <Result xsi:type="QSColumnConstant">
        <Alias>Не действует</Alias>
        <Value xsi:type="xsd:string">Не действует</Value>
      </Result>
    </QSColumnIfBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1000842
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000842, 1009403, 0, 3, 94000400, NULL, NULL, 'Дата выполнения', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000845
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000845, 1009403, 0, 6, 94000500, NULL, NULL, 'Признак успешного выполнения', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000846
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000846, 1009403, 0, 7, 94000600, NULL, NULL, 'Примечание', 35, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000844
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000844, 1009403, 0, 5, 94100300, NULL, NULL, 'Подразделение', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000841
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000841, 1009403, 0, 2, 94200200, NULL, NULL, 'Действие', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000843
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000843, 1009403, 0, 4, 95000400, NULL, NULL, 'Пользователь', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000840
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000840, 1009403, 3, 1, NULL, NULL, NULL, '№', 5, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <SubQuery>
    <MainRegisterID>940</MainRegisterID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000900</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000900</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000800</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000800</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>LessOrEqual</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000100</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1001069
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001069, 1009404, 0, 1, 94000400, NULL, NULL, 'Дата выполнения', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001072
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001072, 1009404, 0, 6, 94000500, NULL, NULL, 'Успешно', 5, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001073
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001073, 1009404, 0, 7, 94000600, NULL, NULL, 'Примечание', 20, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001074
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001074, 1009404, 0, 8, 94090100, NULL, NULL, 'Объект', 20, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001071
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001071, 1009404, 0, 5, 94100300, NULL, NULL, 'Подразделение', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001075
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001075, 1009404, 0, 3, 94290100, NULL, NULL, 'Действие', 15, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001070
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001070, 1009404, 0, 4, 95000400, NULL, NULL, 'Пользователь', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001424
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001424, 1009405, 0, 3, 94000400, NULL, NULL, 'Дата выполнения', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001427
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001427, 1009405, 0, 6, 94000500, NULL, NULL, 'Успешно', 5, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001428
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001428, 1009405, 0, 7, 94000600, NULL, NULL, 'Примечание', 30, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001426
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001426, 1009405, 0, 5, 94100300, NULL, NULL, 'Подразделение', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001423
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001423, 1009405, 0, 2, 94200200, NULL, NULL, 'Действие', 10, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001425
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001425, 1009405, 0, 4, 95000400, NULL, NULL, 'Пользователь', 15, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1001422
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001422, 1009405, 3, 1, NULL, NULL, NULL, '№', 5, 1, NULL, 1, NULL, NULL, NULL, 0, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <SubQuery>
    <MainRegisterID>940</MainRegisterID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000900</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000900</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000800</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000800</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>LessOrEqual</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <AttributeID>94000100</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <AttributeID>94000100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1001429
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1001429, 1009405, 3, 8, NULL, NULL, NULL, 'Результирующий статус', 10, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Результирующий статус</Alias>
  <SubQuery>
    <MainRegisterID>983</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>98300400</Alias>
        <AttributeID>98300400</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>98300100</Alias>
        <AttributeID>98300100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>94001000</Alias>
        <AttributeID>94001000</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
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
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1002087
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002087, 1009561, 0, 2, 95600400, NULL, NULL, 'Дата начала', 20, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002088
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002088, 1009561, 0, 3, 95600500, NULL, NULL, 'Дата окончания', 20, 1, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002090
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002090, 1009561, 0, 5, 95600900, NULL, NULL, 'Количество строк в таблице', 10, 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002089
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002089, 1009561, 0, 4, 95601100, NULL, NULL, 'Тип выгрузки', 10, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002086
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002086, 1009561, 3, 1, NULL, NULL, NULL, 'Статус', 5, 1, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper>
  <RowStyle>false</RowStyle>
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Создана</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Edit.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Запущена</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Play.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Завершена</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>Ошибка</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Cancel.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
</StyleConditionItemWrapper>', NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnSwitch">
  <Alias>Статус</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>95600600</Alias>
    <AttributeID>95600600</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">0</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Создана</Alias>
        <Value xsi:type="xsd:string">Создана</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>1</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Создана</Alias>
        <Value xsi:type="xsd:string">Запущена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>2</Alias>
        <Value xsi:type="xsd:double">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Завершена</Alias>
        <Value xsi:type="xsd:string">Завершена</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>3</Alias>
        <Value xsi:type="xsd:double">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Ошибка</Alias>
        <Value xsi:type="xsd:string">Ошибка</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1002091
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002091, 1009561, 3, 6, NULL, NULL, NULL, 'Автор', 35, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Автор</Alias>
  <SubQuery>
    <MainRegisterID>950</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>95000400</Alias>
        <AttributeID>95000400</AttributeID>
        <Type>Value</Type>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>95000100</Alias>
        <AttributeID>95000100</AttributeID>
        <Type>Value</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnSimple">
        <Alias>95600300</Alias>
        <AttributeID>95600300</AttributeID>
        <Type>Value</Type>
      </RightOperand>
      <RightOperandLevel>1</RightOperandLevel>
    </Condition>
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
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 1100836
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100836, 1009751, 0, 3, 97500700, NULL, NULL, 'Дата начала', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Left', NULL);
end $$;


--<DO>--
-- 1100837
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100837, 1009751, 0, 4, 97500800, NULL, NULL, 'Дата окончания', NULL, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'Left', NULL);
end $$;


--<DO>--
-- 1100838
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100838, 1009751, 3, 2, 97500900, NULL, NULL, 'Статус загрузки', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnSwitch">
  <Alias>ИД службы</Alias>
  <ValueToCompare xsi:type="QSColumnSimple">
    <Alias>97500900</Alias>
    <AttributeID>97500900</AttributeID>
    <Type>Value</Type>
  </ValueToCompare>
  <Blocks>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">0</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Добавлен</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Подготовлен к запуску</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">2</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Выполняется</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">3</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Завершен успешно</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">4</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Завершен с ошибкой</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">5</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Отправлен запрос на остановку</Value>
      </Result>
    </QSColumnSwitchBlock>
    <QSColumnSwitchBlock>
      <ValueToCompare xsi:type="QSColumnConstant">
        <Alias>0</Alias>
        <Value xsi:type="xsd:double">6</Value>
      </ValueToCompare>
      <Result xsi:type="QSColumnConstant">
        <Alias>Добавлен</Alias>
        <Value xsi:type="xsd:string">Остановлен</Value>
      </Result>
    </QSColumnSwitchBlock>
  </Blocks>
</QSColumn>');
end $$;


--<DO>--
-- 1100835
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100835, 1009751, 0, 1, 322000200, NULL, NULL, 'Наименование файла', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100859
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100859, 1009831, 0, 1, 98300100, NULL, NULL, 'ИД', 15, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100860
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100860, 1009831, 0, 2, 98300300, NULL, NULL, 'Код', 15, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100862
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100862, 1009831, 0, 3, 98300400, NULL, NULL, 'Наименоание', 30, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100861
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100861, 1009831, 0, 4, 98300500, NULL, NULL, 'Краткое наименование', 25, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
  <Conditions />
</StyleConditionItemWrapper>', NULL, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100863
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100863, 1009831, 0, 5, 98300600, NULL, NULL, 'Признак архива (удаления)', 15, 1, NULL, 3, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 109450102
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (109450102, 1094501, 0, 2, 94500200, NULL, 0, 'Роль', 40, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002124
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002124, 1094501, 0, 4, 94500400, NULL, NULL, 'Администратор', 15, 1, NULL, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 109450103
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (109450103, 1094501, 0, 3, 94500700, NULL, NULL, 'Подсистема', 40, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 109450101
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (109450101, 1094501, 2, 1, NULL, NULL, 0, 'Столбец выбора', 5, 1, NULL, 0, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?><StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><Conditions /><RowStyle>false</RowStyle></StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002129
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002129, 1094502, 0, 1, 94500200, NULL, NULL, 'Наименование роли', 70, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1002149
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002149, 1094502, 3, 2, NULL, NULL, NULL, 'Пользователей в роли', 30, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSColumnQuery">
  <Alias>Пользователей в роли</Alias>
  <SubQuery>
    <MainRegisterID>952</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnFunction">
        <Alias>НоваяКолонка_2</Alias>
        <FunctionType>Count</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnConstant">
            <Alias>1</Alias>
            <Value xsi:type="xsd:double">1</Value>
          </QSColumn>
        </Operands>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>95200200</Alias>
            <AttributeID>95200200</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <Alias>94500100</Alias>
            <AttributeID>94500100</AttributeID>
            <Type>Value</Type>
          </RightOperand>
          <RightOperandLevel>1</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnFunction">
            <Alias>NVL</Alias>
            <FunctionType>Coalesce</FunctionType>
            <Operands>
              <QSColumn xsi:type="QSColumnSimple">
                <Alias>95001000</Alias>
                <AttributeID>95001000</AttributeID>
                <Type>Value</Type>
              </QSColumn>
              <QSColumn xsi:type="QSColumnConstant">
                <Alias>0</Alias>
                <Value xsi:type="xsd:double">0</Value>
              </QSColumn>
            </Operands>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnConstant">
            <Alias>0</Alias>
            <Value xsi:type="xsd:double">0</Value>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </Condition>
    <ActualDate>0001-01-01T00:00:00</ActualDate>
    <IsActual>false</IsActual>
    <Distinct>false</Distinct>
    <ManualJoin>false</ManualJoin>
    <PackageSize>0</PackageSize>
    <PackageIndex>0</PackageIndex>
    <OrderBy />
    <GroupBy />
    <JoinType xsi:nil="true" />
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>');
end $$;


--<DO>--
-- 109460101
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (109460101, 1094601, 0, 2, 94500200, NULL, 0, 'Роль', NULL, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002133
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002133, 1095001, 0, 4, 94100300, NULL, 0, 'Департамент', 30, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002132
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002132, 1095001, 0, 3, 95000300, NULL, 0, 'Логин', 25, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002131
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002131, 1095001, 0, 2, 95000400, NULL, 0, 'ФИО', 45, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1100788
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100788, 10003631, 0, 1, 363000100, NULL, NULL, 'ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100789
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100789, 10003631, 0, 2, 363000200, NULL, NULL, 'GLOBAL_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100790
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100790, 10003631, 0, 3, 363000300, NULL, NULL, 'UPDATE_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100792
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100792, 10003631, 0, 5, 363000400, NULL, NULL, 'IS_ERROR', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100793
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100793, 10003631, 0, 6, 363000500, NULL, NULL, 'MESSAGE', NULL, 1, NULL, 4, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100794
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100794, 10003631, 0, 7, 363000600, NULL, NULL, 'ERROR_ID', NULL, 1, NULL, 1, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1100791
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1100791, 10003631, 0, 4, 363000900, NULL, NULL, 'IMPORT_DATE', NULL, 1, NULL, 5, NULL, NULL, NULL, NULL, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000936102
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000936102, 10009361, 0, 2, 93600200, NULL, 0, 'Наименование фильтра', 25, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002116
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002116, 10009361, 0, 3, 93600300, NULL, NULL, 'Описание', 35, 1, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
end $$;


--<DO>--
-- 1000936103
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000936103, 10009361, 0, 4, 93600500, NULL, 0, 'Дата последнего изменения', 10, 1, 'ddMMyyyyhhmmss', 5, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002113
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002113, 10009361, 0, 6, 93601100, NULL, 0, 'Общий', 10, 1, NULL, 3, NULL, NULL, NULL, 0, '<?xml version="1.0" encoding="utf-16"?>
<StyleConditionItemWrapper xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Conditions>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Ok.png</ImagePath>
    </StyleConditionItem>
    <StyleConditionItem>
      <Id>0</Id>
      <Condition>не равно</Condition>
      <Value>1</Value>
      <ValueId>0</ValueId>
      <ForeColor Web="" Alpha="0" />
      <BackColor Web="" Alpha="0" />
      <Bold>false</Bold>
      <Underline>false</Underline>
      <Strikethru>false</Strikethru>
      <Italic>false</Italic>
      <ImagePath>~/CoreUI/Content/RegisterImages/16x16_Null.png</ImagePath>
    </StyleConditionItem>
  </Conditions>
  <RowStyle>false</RowStyle>
</StyleConditionItemWrapper>', 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1002114
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1002114, 10009361, 0, 5, 95000400, NULL, 0, 'Имя пользователя, создавшего фильтр', 15, 1, NULL, 4, NULL, NULL, NULL, 0, NULL, 0, 'NotSet', NULL);
end $$;


--<DO>--
-- 1000936101
DO $$
begin
    INSERT INTO CORE_LAYOUT_DETAILS (ID,LAYOUTID,DETAILTYPE,ORDINAL,ATTRIBUTEID,SORTBYATTRIBUTE,REFERENCEID,HEADERTEXT,HEADERWIDTH,VISIBLE,FORMAT,DATATYPE,EXPRESSION,SQLEXPRESSION,TOTALTEXT,TOTALTYPE,STYLE,ENABLESTYLE,TEXTALIGN,QSCOLUMN)
    VALUES (1000936101, 10009361, 2, 1, NULL, NULL, 0, 'Столбец выбора', 5, 1, NULL, NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL);
end $$;
