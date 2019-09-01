
-- VI. Загрузка фильтров
DO $$
begin
delete from core_qry_filter t where (t.qryid between 1000000 and 20000000 or t.qryid between 30000000 and 40000000);
delete from core_qry t where (t.qryid between 1000000 and 20000000 or t.qryid between 30000000 and 40000000);
end $$;
--<DO>--
-- 1000809
/* 
'Не удаленные отчеты'
*/
DO $$
begin
    INSERT INTO CORE_QRY(QRYID, NAME, DESCRIPTION, AUTHOR, DATEFROM, INLIST, QRY_USER, REGISTERID, QSCONDITION, ISCOMMON, REGISTER_VIEW_ID)
    VALUES (1000809, 'Не удаленные отчеты', 'Данный фильтр находится на поддержке и обновляется одновременно с установкой новой версии системы', NULL, '2018-10-23 00:00:00', 0, NULL, 809, '<?xml version="1.0" encoding="utf-16"?>
<QSCondition xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSConditionGroup">
  <Type>And</Type>
  <Conditions>
    <QSCondition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnFunction">
        <Alias>НоваяКолонка_1</Alias>
        <FunctionType>Coalesce</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>80901500</Alias>
            <AttributeID>80901500</AttributeID>
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
</QSCondition>', 0, 'FinanceSavedReport');
end $$;

--<DO>--
-- 1003081
/* 
'Фильтр ФСП ЕПД'
*/
DO $$
begin
    INSERT INTO CORE_QRY(QRYID, NAME, DESCRIPTION, AUTHOR, DATEFROM, INLIST, QRY_USER, REGISTERID, QSCONDITION, ISCOMMON, REGISTER_VIEW_ID)
    VALUES (1003081, 'Фильтр ФСП ЕПД', 'Данный фильтр находится на поддержке и обновляется одновременно с установкой новой версии системы', NULL, NULL, 0, NULL, 308, NULL, 1, 'FspEpd');
end $$;

--<DO>--
-- 1003082
/* 
'Фильтр ФСП по полисам'
*/
DO $$
begin
    INSERT INTO CORE_QRY(QRYID, NAME, DESCRIPTION, AUTHOR, DATEFROM, INLIST, QRY_USER, REGISTERID, QSCONDITION, ISCOMMON, REGISTER_VIEW_ID)
    VALUES (1003082, 'Фильтр ФСП по полисам', 'Данный фильтр находится на поддержке и обновляется одновременно с установкой новой версии системы', NULL, NULL, 0, NULL, 308, NULL, 1, 'FspPolicy');
end $$;

--<DO>--
-- 1003083
/* 
'Фильтр ФСП по свидетельствам'
*/
DO $$
begin
    INSERT INTO CORE_QRY(QRYID, NAME, DESCRIPTION, AUTHOR, DATEFROM, INLIST, QRY_USER, REGISTERID, QSCONDITION, ISCOMMON, REGISTER_VIEW_ID)
    VALUES (1003083, 'Фильтр ФСП по свидетельствам', 'Данный фильтр находится на поддержке и обновляется одновременно с установкой новой версии системы', NULL, NULL, 0, NULL, 308, NULL, 1, 'FspSvd');
end $$;

--<DO>--
-- 1003084
/* 
'Фильтр ФСП по общему имуществу'
*/
DO $$
begin
    INSERT INTO CORE_QRY(QRYID, NAME, DESCRIPTION, AUTHOR, DATEFROM, INLIST, QRY_USER, REGISTERID, QSCONDITION, ISCOMMON, REGISTER_VIEW_ID)
    VALUES (1003084, 'Фильтр ФСП по общему имуществу', 'Данный фильтр находится на поддержке и обновляется одновременно с установкой новой версии системы', NULL, NULL, 0, NULL, 308, NULL, 1, 'FspAllProperty');
end $$;

--<DO>--
-- 1003121
/* 
'Не удаленные расчеты'
*/
DO $$
begin
    INSERT INTO CORE_QRY(QRYID, NAME, DESCRIPTION, AUTHOR, DATEFROM, INLIST, QRY_USER, REGISTERID, QSCONDITION, ISCOMMON, REGISTER_VIEW_ID)
    VALUES (1003121, 'Не удаленные расчеты', 'Данный фильтр находится на поддержке и обновляется одновременно с установкой новой версии системы', NULL, NULL, 0, NULL, 312, '<?xml version="1.0" encoding="utf-16"?>
<QSCondition xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSConditionGroup">
  <Type>And</Type>
  <Conditions>
    <QSCondition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnFunction">
        <Alias>НоваяКолонка_1</Alias>
        <FunctionType>Coalesce</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>312004300</Alias>
            <AttributeID>312004300</AttributeID>
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
</QSCondition>', 0, 'InsurCalculations');
end $$;

--<DO>--
-- 1004001
/* 
'Только ПФХД'
*/
DO $$
begin
    INSERT INTO CORE_QRY(QRYID, NAME, DESCRIPTION, AUTHOR, DATEFROM, INLIST, QRY_USER, REGISTERID, QSCONDITION, ISCOMMON, REGISTER_VIEW_ID)
    VALUES (1004001, 'Только ПФХД', 'Данный фильтр находится на поддержке и обновляется одновременно с установкой новой версии системы', NULL, '2018-04-16 00:00:00', 0, NULL, 400, '<?xml version="1.0" encoding="utf-16"?>
<QSCondition xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSConditionGroup">
  <Type>And</Type>
  <Conditions>
    <QSCondition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnFunction">
        <Alias>NVL</Alias>
        <FunctionType>Coalesce</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>40000400</Alias>
            <AttributeID>40000400</AttributeID>
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
    <QSCondition xsi:type="QSConditionGroup">
      <Type>Or</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnQuery">
            <Alias>НоваяКолонка_1</Alias>
            <SubQuery>
              <MainRegisterID>456</MainRegisterID>
              <TDInstanceID>0</TDInstanceID>
              <Columns>
                <QSColumn xsi:type="QSColumnSimple">
                  <Alias>45600200</Alias>
                  <AttributeID>45600200</AttributeID>
                  <Type>Value</Type>
                </QSColumn>
              </Columns>
              <Condition xsi:type="QSConditionSimple">
                <ConditionType>Equal</ConditionType>
                <LeftOperand xsi:type="QSColumnSimple">
                  <Alias>45600100</Alias>
                  <AttributeID>45600100</AttributeID>
                  <Type>Value</Type>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperand xsi:type="QSColumnQuery">
                  <Alias>НоваяКолонка_2</Alias>
                  <SubQuery>
                    <MainRegisterID>950</MainRegisterID>
                    <TDInstanceID>0</TDInstanceID>
                    <Columns>
                      <QSColumn xsi:type="QSColumnSimple">
                        <Alias>95001500</Alias>
                        <AttributeID>95001500</AttributeID>
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
                      <RightOperand xsi:type="QSColumnConstant">
                        <Alias>{Session:UserId}</Alias>
                        <Value xsi:type="xsd:string">{SRDSession:UserId}</Value>
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
                  </SubQuery>
                </RightOperand>
                <RightOperandLevel>0</RightOperandLevel>
              </Condition>
              <ActualDate>0001-01-01T00:00:00</ActualDate>
              <IsActual>false</IsActual>
              <Distinct>false</Distinct>
              <ManualJoin>false</ManualJoin>
              <PackageSize>1</PackageSize>
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
          <RightOperand xsi:type="QSColumnConstant">
            <Alias>1</Alias>
            <Value xsi:type="xsd:double">1</Value>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>In</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>40002200</Alias>
            <AttributeID>40002200</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnQuery">
            <Alias>НоваяКолонка_1</Alias>
            <SubQuery>
              <MainRegisterID>456</MainRegisterID>
              <TDInstanceID>0</TDInstanceID>
              <Columns>
                <QSColumn xsi:type="QSColumnSimple">
                  <Alias>45600400</Alias>
                  <AttributeID>45600400</AttributeID>
                  <Type>Value</Type>
                </QSColumn>
              </Columns>
              <Condition xsi:type="QSConditionSimple">
                <ConditionType>Equal</ConditionType>
                <LeftOperand xsi:type="QSColumnSimple">
                  <Alias>45600100</Alias>
                  <AttributeID>45600100</AttributeID>
                  <Type>Value</Type>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperand xsi:type="QSColumnQuery">
                  <Alias>НоваяКолонка_2</Alias>
                  <SubQuery>
                    <MainRegisterID>950</MainRegisterID>
                    <TDInstanceID>0</TDInstanceID>
                    <Columns>
                      <QSColumn xsi:type="QSColumnSimple">
                        <Alias>95001500</Alias>
                        <AttributeID>95001500</AttributeID>
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
                      <RightOperand xsi:type="QSColumnConstant">
                        <Alias>{Session:UserId}</Alias>
                        <Value xsi:type="xsd:string">{SRDSession:UserId}</Value>
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
                  </SubQuery>
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
            </SubQuery>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </QSCondition>
    <QSCondition xsi:type="QSConditionSimple">
      <ConditionType>NotEqual</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>40001000_CODE</Alias>
        <AttributeID>40001000</AttributeID>
        <Type>Code</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnConstant">
        <Alias>Удален</Alias>
        <Value xsi:type="xsd:long">126</Value>
      </RightOperand>
      <RightOperandLevel>0</RightOperandLevel>
    </QSCondition>
  </Conditions>
</QSCondition>', 1, 'Pfhd');
end $$;

--<DO>--
-- 1004002
/* 
'Только отчет к ПФХД'
*/
DO $$
begin
    INSERT INTO CORE_QRY(QRYID, NAME, DESCRIPTION, AUTHOR, DATEFROM, INLIST, QRY_USER, REGISTERID, QSCONDITION, ISCOMMON, REGISTER_VIEW_ID)
    VALUES (1004002, 'Только отчет к ПФХД', 'Данный фильтр находится на поддержке и обновляется одновременно с установкой новой версии системы', NULL, '2018-04-16 00:00:00', 0, NULL, 400, '<?xml version="1.0" encoding="utf-16"?>
<QSCondition xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="QSConditionGroup">
  <Type>And</Type>
  <Conditions>
    <QSCondition xsi:type="QSConditionSimple">
      <ConditionType>Equal</ConditionType>
      <LeftOperand xsi:type="QSColumnFunction">
        <Alias>NVL</Alias>
        <FunctionType>Coalesce</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>40000400</Alias>
            <AttributeID>40000400</AttributeID>
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
        <Alias>1</Alias>
        <Value xsi:type="xsd:double">1</Value>
      </RightOperand>
      <RightOperandLevel>0</RightOperandLevel>
    </QSCondition>
    <QSCondition xsi:type="QSConditionGroup">
      <Type>Or</Type>
      <Conditions>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>Equal</ConditionType>
          <LeftOperand xsi:type="QSColumnQuery">
            <Alias>НоваяКолонка_1</Alias>
            <SubQuery>
              <MainRegisterID>456</MainRegisterID>
              <TDInstanceID>0</TDInstanceID>
              <Columns>
                <QSColumn xsi:type="QSColumnSimple">
                  <Alias>45600200</Alias>
                  <AttributeID>45600200</AttributeID>
                  <Type>Value</Type>
                </QSColumn>
              </Columns>
              <Condition xsi:type="QSConditionSimple">
                <ConditionType>Equal</ConditionType>
                <LeftOperand xsi:type="QSColumnSimple">
                  <Alias>45600100</Alias>
                  <AttributeID>45600100</AttributeID>
                  <Type>Value</Type>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperand xsi:type="QSColumnQuery">
                  <Alias>НоваяКолонка_2</Alias>
                  <SubQuery>
                    <MainRegisterID>950</MainRegisterID>
                    <TDInstanceID>0</TDInstanceID>
                    <Columns>
                      <QSColumn xsi:type="QSColumnSimple">
                        <Alias>95001500</Alias>
                        <AttributeID>95001500</AttributeID>
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
                      <RightOperand xsi:type="QSColumnConstant">
                        <Alias>{Session:UserId}</Alias>
                        <Value xsi:type="xsd:string">{SRDSession:UserId}</Value>
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
                  </SubQuery>
                </RightOperand>
                <RightOperandLevel>0</RightOperandLevel>
              </Condition>
              <ActualDate>0001-01-01T00:00:00</ActualDate>
              <IsActual>false</IsActual>
              <Distinct>false</Distinct>
              <ManualJoin>false</ManualJoin>
              <PackageSize>1</PackageSize>
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
          <RightOperand xsi:type="QSColumnConstant">
            <Alias>1</Alias>
            <Value xsi:type="xsd:double">1</Value>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
        <QSCondition xsi:type="QSConditionSimple">
          <ConditionType>In</ConditionType>
          <LeftOperand xsi:type="QSColumnSimple">
            <Alias>40002200</Alias>
            <AttributeID>40002200</AttributeID>
            <Type>Value</Type>
          </LeftOperand>
          <LeftOperandLevel>0</LeftOperandLevel>
          <RightOperand xsi:type="QSColumnQuery">
            <Alias>НоваяКолонка_1</Alias>
            <SubQuery>
              <MainRegisterID>456</MainRegisterID>
              <TDInstanceID>0</TDInstanceID>
              <Columns>
                <QSColumn xsi:type="QSColumnSimple">
                  <Alias>45600400</Alias>
                  <AttributeID>45600400</AttributeID>
                  <Type>Value</Type>
                </QSColumn>
              </Columns>
              <Condition xsi:type="QSConditionSimple">
                <ConditionType>Equal</ConditionType>
                <LeftOperand xsi:type="QSColumnSimple">
                  <Alias>45600100</Alias>
                  <AttributeID>45600100</AttributeID>
                  <Type>Value</Type>
                </LeftOperand>
                <LeftOperandLevel>0</LeftOperandLevel>
                <RightOperand xsi:type="QSColumnQuery">
                  <Alias>НоваяКолонка_2</Alias>
                  <SubQuery>
                    <MainRegisterID>950</MainRegisterID>
                    <TDInstanceID>0</TDInstanceID>
                    <Columns>
                      <QSColumn xsi:type="QSColumnSimple">
                        <Alias>95001500</Alias>
                        <AttributeID>95001500</AttributeID>
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
                      <RightOperand xsi:type="QSColumnConstant">
                        <Alias>{Session:UserId}</Alias>
                        <Value xsi:type="xsd:string">{SRDSession:UserId}</Value>
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
                  </SubQuery>
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
            </SubQuery>
          </RightOperand>
          <RightOperandLevel>0</RightOperandLevel>
        </QSCondition>
      </Conditions>
    </QSCondition>
    <QSCondition xsi:type="QSConditionSimple">
      <ConditionType>NotEqual</ConditionType>
      <LeftOperand xsi:type="QSColumnSimple">
        <Alias>40001000_CODE</Alias>
        <AttributeID>40001000</AttributeID>
        <Type>Code</Type>
      </LeftOperand>
      <LeftOperandLevel>0</LeftOperandLevel>
      <RightOperand xsi:type="QSColumnConstant">
        <Alias>Удален</Alias>
        <Value xsi:type="xsd:long">126</Value>
      </RightOperand>
      <RightOperandLevel>0</RightOperandLevel>
    </QSCondition>
  </Conditions>
</QSCondition>', 1, 'Pfhd');
end $$;


-- VI. Загрузка фильтров
--<DO>--
-- 1003081
/* 
1003081
*/
DO $$
BEGIN
    INSERT INTO CORE_QRY_FILTER
    VALUES (1003081, 1003081, 1, 308000200, 'ЕПД', 1, 1, '333001', 1, NULL, NULL, 333, NULL, NULL);
END $$;

--<DO>--
-- 1003082
/* 
1003082
*/
DO $$
BEGIN
    INSERT INTO CORE_QRY_FILTER
    VALUES (1003082, 1003082, 1, 308000200, 'Полис', 1, 1, '333002', 1, NULL, NULL, 333, NULL, NULL);
END $$;

--<DO>--
-- 1003083
/* 
1003083
*/
DO $$
BEGIN
    INSERT INTO CORE_QRY_FILTER
    VALUES (1003083, 1003083, 1, 308000200, 'Свидетельство', 1, 1, '333003', 1, NULL, NULL, 333, NULL, NULL);
END $$;

--<DO>--
-- 1003084
/* 
1003084
*/
DO $$
BEGIN
    INSERT INTO CORE_QRY_FILTER
    VALUES (1003084, 1003084, 1, 308000200, 'Общее имущество', 1, 1, '333004', 1, NULL, NULL, 333, NULL, NULL);
END $$;

