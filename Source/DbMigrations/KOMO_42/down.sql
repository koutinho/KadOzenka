INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10007500, 'ID города', 100, 1, null, null, 'CITY_ID', null, null, null, null, null, 'CityId', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10003300, 'URL-адреса изображений', 100, 4, null, null, 'IMAGES', null, null, null, null, null, 'Images', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10002100, 'URL-адрес объявления', 100, 4, null, null, 'URL', null, null, null, null, null, 'Url', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10005200, 'Административный округ', 100, 4, null, 119, 'DISTRICT', 'DISTRICT_CODE', null, null, null, null, 'District', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10009001, 'Актуальная цена с учетом корректировки на дату', 100, 2, null, null, null, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Колонка_1</Alias>
  <SubQuery>
    <MainRegisterID>116</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>Колонка_1</Alias>
        <AttributeID>11600200</AttributeID>
        <Type>Value</Type>
        <Level>0</Level>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>Колонка_2</Alias>
            <AttributeID>11600300</AttributeID>
            <Type>Value</Type>
            <Level>0</Level>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <Alias>Колонка_3</Alias>
            <AttributeID>10002000</AttributeID>
            <Type>Value</Type>
            <Level>1</Level>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>Колонка_4</Alias>
            <AttributeID>11600400</AttributeID>
            <Type>Value</Type>
            <Level>0</Level>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnQuery">
            <Alias>Колонка_5</Alias>
            <SubQuery>
              <MainRegisterID>116</MainRegisterID>
              <TDInstanceID>0</TDInstanceID>
              <Columns>
                <QSColumn xsi:type="QSColumnFunction">
                  <Alias>Колонка_5</Alias>
                  <FunctionType>Max</FunctionType>
                  <Operands>
                    <QSColumn xsi:type="QSColumnSimple">
                      <Alias>Колонка_5</Alias>
                      <AttributeID>11600400</AttributeID>
                      <Type>Value</Type>
                      <Level>0</Level>
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
                      <Alias>Колонка_7</Alias>
                      <AttributeID>11600300</AttributeID>
                      <Type>Value</Type>
                      <Level>0</Level>
                    </LeftOperand>
                    <LeftOperandLevel>0</LeftOperandLevel>
                    <RightOperand xsi:type="QSColumnSimple">
                      <Alias>Колонка_8</Alias>
                      <AttributeID>10002000</AttributeID>
                      <Type>Value</Type>
                      <Level>1</Level>
                    </RightOperand>
                    <RightOperandLevel>0</RightOperandLevel>
                  </QSCondition>
                  <QSCondition xsi:type="QSConditionSimple">
                    <ConditionType>LessOrEqual</ConditionType>
                    <LeftOperand xsi:type="QSColumnSimple">
                      <Alias>Колонка_9</Alias>
                      <AttributeID>11600400</AttributeID>
                      <Type>Value</Type>
                      <Level>0</Level>
                    </LeftOperand>
                    <LeftOperandLevel>0</LeftOperandLevel>
                    <RightOperand xsi:type="QSColumnFunction">
                      <Alias>Колонка_10</Alias>
                      <FunctionType>ActualDateTime</FunctionType>
                      <Operands />
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
              <JoinType>Inner</JoinType>
              <Joins />
              <Parameters />
              <LoadRelations>false</LoadRelations>
            </SubQuery>
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
    <JoinType>Inner</JoinType>
    <Joins />
    <Parameters />
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>', null, 1, null, null, null, null, 2, '2020-04-17 16:43:34.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10008700, 'Актуальная цена с учетом корректировки на комнатность', 100, 2, null, null, null, null, null, null, null, '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnQuery">
  <Alias>Колонка_1</Alias>
  <SubQuery>
    <MainRegisterID>113</MainRegisterID>
    <TDInstanceID>0</TDInstanceID>
    <Columns>
      <QSColumn xsi:type="QSColumnSimple">
        <Alias>Колонка_1</Alias>
        <AttributeID>11300500</AttributeID>
        <Type>Value</Type>
        <Level>0</Level>
      </QSColumn>
    </Columns>
    <Condition xsi:type="QSConditionGroup">
      <Type>And</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>Колонка_2</Alias>
            <AttributeID>11300200</AttributeID>
            <Type>Value</Type>
            <Level>0</Level>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnSimple">
            <Alias>Колонка_3</Alias>
            <AttributeID>10002000</AttributeID>
            <Type>Value</Type>
            <Level>1</Level>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>Колонка_4</Alias>
            <AttributeID>11300300</AttributeID>
            <Type>Value</Type>
            <Level>0</Level>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnQuery">
            <Alias>Колонка_5</Alias>
            <SubQuery>
              <MainRegisterID>113</MainRegisterID>
              <TDInstanceID>0</TDInstanceID>
              <Columns>
                <QSColumn xsi:type="QSColumnFunction">
                  <Alias>Колонка_5</Alias>
                  <FunctionType>Max</FunctionType>
                  <Operands>
                    <QSColumn xsi:type="QSColumnSimple">
                      <Alias>Колонка_5</Alias>
                      <AttributeID>11300300</AttributeID>
                      <Type>Value</Type>
                      <Level>0</Level>
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
                      <Alias>Колонка_7</Alias>
                      <AttributeID>11300200</AttributeID>
                      <Type>Value</Type>
                      <Level>0</Level>
                    </LeftOperand>
                    <LeftOperandLevel>0</LeftOperandLevel>
                    <RightOperand xsi:type="QSColumnSimple">
                      <Alias>Колонка_8</Alias>
                      <AttributeID>10002000</AttributeID>
                      <Type>Value</Type>
                      <Level>1</Level>
                    </RightOperand>
                    <RightOperandLevel>0</RightOperandLevel>
                  </QSCondition>
                  <QSCondition xsi:type="QSConditionSimple">
                    <ConditionType>LessOrEqual</ConditionType>
                    <LeftOperand xsi:type="QSColumnSimple">
                      <Alias>Колонка_9</Alias>
                      <AttributeID>11300300</AttributeID>
                      <Type>Value</Type>
                      <Level>0</Level>
                    </LeftOperand>
                    <LeftOperandLevel>0</LeftOperandLevel>
                    <RightOperand xsi:type="QSColumnFunction">
                      <Alias>Колонка_10</Alias>
                      <FunctionType>ActualDateTime</FunctionType>
                      <Operands />
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
              <JoinType>Inner</JoinType>
              <Joins />
              <Parameters />
              <LoadRelations>false</LoadRelations>
            </SubQuery>
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
    <JoinType>Inner</JoinType>
    <Joins />
    <Parameters />
    <LoadRelations>false</LoadRelations>
  </SubQuery>
</QSColumn>', null, 1, null, null, null, null, 2, '2020-04-01 16:21:34.000000', 0);

INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10003000, 'Город', 100, 4, null, null, 'CITY', null, null, null, null, null, 'City', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10005700, 'Группа сегмента рынка', 100, 4, null, null, 'KO_GROUP', 'KO_GROUP_CODE', null, null, null, null, 'Group', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10007600, 'Дата последнего обновления цены', 100, 5, null, null, 'LAST_DATE_UPDATE', null, null, null, null, null, 'LastDateUpdate', 1, null, null, null, 0, null, null, 0);