with unit_data as(
	select u.object_id,
	u.CADASTRAL_NUMBER,
	u.CADASTRAL_BLOCK,
	u.PROPERTY_TYPE,
	u.SQUARE,
	u.DEGREE_READINESS,
	u.UPKS,
	u.CADASTRAL_COST
	from ko_unit u
	where u.property_type_code<>4 and u.task_id IN ({0})
),

KladrAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {1})
),

ParentKnAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {2})
),

NameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {3})
),

PurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {4})
),

AddressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {5})
),

LocationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {6})
),

ZuCadastralNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {7})
),

BuildingYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {8})
),

CommissioningYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {9})
),

FloorCountAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {10})
),

UndergroundFloorCountAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {11})
),

FloorNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {12})
),

WallMaterialAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {13})
)

select
	u.CADASTRAL_NUMBER as CadastralNumber,
	u.CADASTRAL_BLOCK as CadastralQuarter,
	u.PROPERTY_TYPE as PropertyType,
	u.SQUARE as Square,
	u.DEGREE_READINESS as DegreeReadiness,
	u.UPKS as Upks,
	u.CADASTRAL_COST as CadastralCost,
	Kladr.attributeValue as Kladr,
	ParentKn.attributeValue as ParentKn,
	Name.attributeValue as Name,
	Purpose.attributeValue as Purpose,
	Address.attributeValue as Address,
	Location.attributeValue as Location,
	ZuCadastralNumber.attributeValue as ZuCadastralNumber,
	BuildingYear.attributeValue as BuildingYear,
	CommissioningYear.attributeValue as CommissioningYear,
	FloorCount.attributeValue as FloorCount,
	UndergroundFloorCount.attributeValue as UndergroundFloorCount,
	FloorNumber.attributeValue as FloorNumber,
	WallMaterial.attributeValue WallMaterial
from unit_data  u
	left outer join KladrAttrValues Kladr on u.object_id=Kladr.objectId
	left outer join ParentKnAttrValues ParentKn on u.object_id=ParentKn.objectId
	left outer join NameAttrValues Name on u.object_id=Name.objectId
	left outer join PurposeAttrValues Purpose on u.object_id=Purpose.objectId
	left outer join AddressAttrValues Address on u.object_id=Address.objectId
	left outer join LocationAttrValues Location on u.object_id=Location.objectId
	left outer join ZuCadastralNumberAttrValues ZuCadastralNumber on u.object_id=ZuCadastralNumber.objectId
	left outer join BuildingYearAttrValues BuildingYear on u.object_id=BuildingYear.objectId
	left outer join CommissioningYearAttrValues CommissioningYear on u.object_id=CommissioningYear.objectId
	left outer join FloorCountAttrValues FloorCount on u.object_id=FloorCount.objectId
	left outer join UndergroundFloorCountAttrValues UndergroundFloorCount on u.object_id=UndergroundFloorCount.objectId
	left outer join FloorNumberAttrValues FloorNumber on u.object_id=FloorNumber.objectId
	left outer join WallMaterialAttrValues WallMaterial on u.object_id=WallMaterial.objectId

	
	