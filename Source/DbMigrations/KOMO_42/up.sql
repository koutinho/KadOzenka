--раскладка

update  core_layout set qsquery='<?xml version="1.0" encoding="utf-16"?>
<QSQuery xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <MainRegisterID>100</MainRegisterID>
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
</QSQuery>' where layoutid=1001001;

delete from core_layout_details where id in (1002310, 1002267, 1002235, 1002348, 1002203, 1002302, 1002301, 1002205, 1002275, 1002276, 1002277, 1002278,
                                            1002254, 1002269, 1002273, 1002288, 1002394, 1002395, 1002676, 1002677, 1002673, 1002674, 1002675);


--обновление виртуальных колонок
update core_register_attribute set qscolumn = '<?xml version="1.0" encoding="utf-16"?>
<QSColumn xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="QSColumnIf">
  <Alias>Колонка_1</Alias>
  <Blocks>
    <QSColumnIfBlock>
      <Condition xsi:type="QSConditionSimple">
        <ConditionType>IsNotNull</ConditionType>
        <LeftOperand xsi:type="QSColumnSimple">
          <Alias>Колонка_1</Alias>
          <AttributeID>10004300</AttributeID>
          <Type>Value</Type>
          <Level>0</Level>
        </LeftOperand>
        <LeftOperandLevel>0</LeftOperandLevel>
        <RightOperandLevel>0</RightOperandLevel>
      </Condition>
      <Result xsi:type="QSColumnFunction">
        <Alias>Колонка_2</Alias>
        <FunctionType>Divide</FunctionType>
        <Operands>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>Колонка_2</Alias>
            <AttributeID>10002700</AttributeID>
            <Type>Value</Type>
            <Level>0</Level>
          </QSColumn>
          <QSColumn xsi:type="QSColumnSimple">
            <Alias>Колонка_4</Alias>
            <AttributeID>10004300</AttributeID>
            <Type>Value</Type>
            <Level>0</Level>
          </QSColumn>
        </Operands>
      </Result>
    </QSColumnIfBlock>
    <QSColumnIfBlock>
      <Result xsi:type="QSColumnConstant">
        <Alias>Колонка_6</Alias>
      </Result>
    </QSColumnIfBlock>
  </Blocks>
</QSColumn>'
where id = 10002701;


--колонки
delete from core_register_attribute where id = 10007500;
ALTER TABLE market_core_object DROP COLUMN CITY_ID;

delete from core_register_attribute where id = 10003300;
ALTER TABLE market_core_object DROP COLUMN IMAGES;

delete from core_register_attribute where id = 10002100;
ALTER TABLE market_core_object DROP COLUMN URL;

delete from core_register_attribute where id = 10005200;
ALTER TABLE market_core_object DROP COLUMN DISTRICT;

delete from core_register_attribute where id = 10009001;
delete from core_register_attribute where id = 10008700;

delete from core_register_attribute where id = 10007800;
ALTER TABLE market_core_object DROP COLUMN PROPERTY_LAW_TYPE;

delete from core_register_attribute where id = 10009002;
ALTER TABLE market_core_object DROP COLUMN OWNERSHIP_TYPE;

delete from core_register_attribute where id = 10004700;
ALTER TABLE market_core_object DROP COLUMN BUILDING_YEAR;

delete from core_register_attribute where id = 10003000;
ALTER TABLE market_core_object DROP COLUMN CITY;

delete from core_register_attribute where id = 10005700;
ALTER TABLE market_core_object DROP COLUMN KO_GROUP;
ALTER TABLE market_core_object DROP COLUMN KO_GROUP_CODE;

delete from core_register_attribute where id = 10007600;
ALTER TABLE market_core_object DROP COLUMN LAST_DATE_UPDATE;

delete from core_register_attribute where id = 10004500;
ALTER TABLE market_core_object DROP COLUMN AREA_LIVING;

delete from core_register_attribute where id = 10005900;
ALTER TABLE market_core_object DROP COLUMN ZONE;

delete from core_register_attribute where id = 10005100;
ALTER TABLE market_core_object DROP COLUMN ZONE_REGION;

delete from core_register_attribute where id = 10002600;
ALTER TABLE market_core_object DROP COLUMN MARKET_ID;

delete from core_register_attribute where id = 10008600;
ALTER TABLE market_core_object DROP COLUMN FORMALIZED_ADDRESS_ID;

delete from core_register_attribute where id = 10005600;
ALTER TABLE market_core_object DROP COLUMN CADASTRAL_QUARTAL;

delete from core_register_attribute where id = 10005500;
ALTER TABLE market_core_object DROP COLUMN BUILDING_CADASTRAL_NUMBER;

delete from core_register_attribute where id = 10007400;
ALTER TABLE market_core_object DROP COLUMN REGION_ID;

delete from core_register_attribute where id = 10003900;
ALTER TABLE market_core_object DROP COLUMN ROOMS_COUNT;

delete from core_register_attribute where id = 10009006;
ALTER TABLE market_core_object DROP COLUMN IS_UTILITIES_INCLUDED;

delete from core_register_attribute where id = 10009011;
ALTER TABLE market_core_object DROP COLUMN CCT;

delete from core_register_attribute where id = 10009010;
ALTER TABLE market_core_object DROP COLUMN BUILDING_LINE;

delete from core_register_attribute where id = 10003200;
ALTER TABLE market_core_object DROP COLUMN METRO;

delete from core_register_attribute where id = 10005101;
ALTER TABLE market_core_object DROP COLUMN CUSTOM_ZONE;

delete from core_register_attribute where id = 10009007;
ALTER TABLE market_core_object DROP COLUMN VAT;
ALTER TABLE market_core_object DROP COLUMN VAT_CODE;

delete from core_register_attribute where id = 10004600;
ALTER TABLE market_core_object DROP COLUMN AREA_LAND;

delete from core_register_attribute where id = 10004400;
ALTER TABLE market_core_object DROP COLUMN AREA_KITCHEN;

delete from core_register_attribute where id = 10005800;
ALTER TABLE market_core_object DROP COLUMN KO_SUBGROUP;
ALTER TABLE market_core_object DROP COLUMN KO_SUBGROUP_CODE;

delete from core_register_attribute where id = 10006001;
ALTER TABLE market_core_object DROP COLUMN EXCLUSION_STATUS;
ALTER TABLE market_core_object DROP COLUMN EXCLUSION_STATUS_CODE;

delete from core_register_attribute where id = 10007900;
ALTER TABLE market_core_object DROP COLUMN PROPERTY_PART_SIZE;

delete from core_register_attribute where id = 10005300;
ALTER TABLE market_core_object DROP COLUMN NEIGHBORHOOD;

delete from core_register_attribute where id = 10007300;
ALTER TABLE market_core_object DROP COLUMN SUBWAY_SPACE;

delete from core_register_attribute where id = 10002900;
ALTER TABLE market_core_object DROP COLUMN REGION;

delete from core_register_attribute where id = 10009004;
ALTER TABLE market_core_object DROP COLUMN QUALITY;

delete from core_register_attribute where id = 10009009;
ALTER TABLE market_core_object DROP COLUMN RENOVATION;

delete from core_register_attribute where id = 10006000;
ALTER TABLE market_core_object DROP COLUMN PROCESS_TYPE;
ALTER TABLE market_core_object DROP COLUMN PROCESS_TYPE_CODE;

delete from core_register_attribute where id = 10003400;
ALTER TABLE market_core_object DROP COLUMN DESCRIPTION;

delete from core_register_attribute where id = 10006500;
ALTER TABLE market_core_object DROP COLUMN PHONE_NUMBER;

delete from core_register_attribute where id = 10009008;
ALTER TABLE market_core_object DROP COLUMN ENTRANCE_TYPE;

delete from core_register_attribute where id = 10009003;
ALTER TABLE market_core_object DROP COLUMN PLACEMENT_TYPE;

delete from core_register_attribute where id = 10008100;
ALTER TABLE market_core_object DROP COLUMN PRICE_AFTER_CORRECTION_BY_DATE;

delete from core_register_attribute where id = 10008400;
ALTER TABLE market_core_object DROP COLUMN PRICE_AFTER_CORRECTION_BY_ROOMS;

delete from core_register_attribute where id = 10008800;
ALTER TABLE market_core_object DROP COLUMN PRICE_AFTER_CORRECTION_FOR_FIRST_FLOOR;

delete from core_register_attribute where id = 10008200;
ALTER TABLE market_core_object DROP COLUMN PRICE_AFTER_CORRECTION_BY_BARGAIN;

delete from core_register_attribute where id = 10008900;
ALTER TABLE market_core_object DROP COLUMN PRICE_AFTER_CORRECTION_BY_STAGE;

