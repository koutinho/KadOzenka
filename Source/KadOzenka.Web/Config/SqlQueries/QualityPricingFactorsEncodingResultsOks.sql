with unit_data as(
	select u.object_id,
	u.CADASTRAL_NUMBER,
	u.CADASTRAL_BLOCK,
	u.PROPERTY_TYPE,
	u.SQUARE
	from ko_unit u
	where u.property_type_code<>4 and u.PROPERTY_TYPE_CODE<>2190 and u.task_id IN ({0})
),

ParentKnAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {1})
),

TypeOfUsingNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {2})
),

TypeOfUsingCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {3})
),

TypeOfUsingCodeSourceAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {4})
),

TypeOfUsingGroupCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {5})
),

SegmentAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {6})
),

FunctionalGroupNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {7})
),

NameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {8})
),

PurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {9})
),

AddressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {10})
),

LocationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {11})
),

ZuCadastralNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {12})
),

BuildingYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {13})
),

CommissioningYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {14})
),

FloorCountAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {15})
),

UndergroundFloorCountAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {16})
),

FloorNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {17})
),

WallMaterialAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {18})
),

cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {14})
)

select
	u.PROPERTY_TYPE as PropertyType,
	u.CADASTRAL_NUMBER as CadastralNumber,
	COALESCE(cadastralQuartalGbu.attributeValue, u.CADASTRAL_BLOCK) as CadastralQuarter,
	u.SQUARE as Square,
	ParentKn.attributeValue as ParentKn,
	TypeOfUsingName.attributeValue as TypeOfUsingName,
	TypeOfUsingCode.attributeValue as TypeOfUsingCode,
	TypeOfUsingCodeSource.attributeValue as TypeOfUsingCodeSource,
	TypeOfUsingGroupCode.attributeValue as TypeOfUsingGroupCode,
	Segment.attributeValue as Segment,
	FunctionalGroupName.attributeValue as FunctionalGroupName,
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
	WallMaterial.attributeValue as WallMaterial
from unit_data  u
	left outer join ParentKnAttrValues ParentKn on u.object_id=ParentKn.objectId
	left outer join TypeOfUsingNameAttrValues TypeOfUsingName on u.object_id=TypeOfUsingName.objectId
	left outer join TypeOfUsingCodeAttrValues TypeOfUsingCode on u.object_id=TypeOfUsingCode.objectId
	left outer join TypeOfUsingCodeSourceAttrValues TypeOfUsingCodeSource on u.object_id=TypeOfUsingCodeSource.objectId
	left outer join TypeOfUsingGroupCodeAttrValues TypeOfUsingGroupCode on u.object_id=TypeOfUsingGroupCode.objectId
	left outer join SegmentAttrValues Segment on u.object_id=Segment.objectId	
	left outer join FunctionalGroupNameAttrValues FunctionalGroupName on u.object_id=FunctionalGroupName.objectId	
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
	left outer join cadastralQuartalAttrValues cadastralQuartalGbu on u.object_id=cadastralQuartalGbu.objectId
