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

