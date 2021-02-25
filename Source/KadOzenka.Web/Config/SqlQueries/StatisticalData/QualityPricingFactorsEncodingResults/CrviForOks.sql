with unit_data as(
	select unit.object_id,
	unit.CADASTRAL_NUMBER,
	unit.CADASTRAL_BLOCK,
	unit.PROPERTY_TYPE,
	unit.SQUARE
	from ko_unit unit
	{0}
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
	unit.PROPERTY_TYPE as PropertyType,
	unit.CADASTRAL_NUMBER as CadastralNumber,
	COALESCE(cadastralQuartalGbu.attributeValue, unit.CADASTRAL_BLOCK) as CadastralQuarter,
	unit.SQUARE as Square,
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
from unit_data  unit
	left outer join ParentKnAttrValues ParentKn on unit.object_id=ParentKn.objectId
	left outer join TypeOfUsingNameAttrValues TypeOfUsingName on unit.object_id=TypeOfUsingName.objectId
	left outer join TypeOfUsingCodeAttrValues TypeOfUsingCode on unit.object_id=TypeOfUsingCode.objectId
	left outer join TypeOfUsingCodeSourceAttrValues TypeOfUsingCodeSource on unit.object_id=TypeOfUsingCodeSource.objectId
	left outer join TypeOfUsingGroupCodeAttrValues TypeOfUsingGroupCode on unit.object_id=TypeOfUsingGroupCode.objectId
	left outer join SegmentAttrValues Segment on unit.object_id=Segment.objectId	
	left outer join FunctionalGroupNameAttrValues FunctionalGroupName on unit.object_id=FunctionalGroupName.objectId	
	left outer join NameAttrValues Name on unit.object_id=Name.objectId	
	left outer join PurposeAttrValues Purpose on unit.object_id=Purpose.objectId	
	left outer join AddressAttrValues Address on unit.object_id=Address.objectId	
	left outer join LocationAttrValues Location on unit.object_id=Location.objectId	
	left outer join ZuCadastralNumberAttrValues ZuCadastralNumber on unit.object_id=ZuCadastralNumber.objectId	
	left outer join BuildingYearAttrValues BuildingYear on unit.object_id=BuildingYear.objectId	
	left outer join CommissioningYearAttrValues CommissioningYear on unit.object_id=CommissioningYear.objectId	
	left outer join FloorCountAttrValues FloorCount on unit.object_id=FloorCount.objectId	
	left outer join UndergroundFloorCountAttrValues UndergroundFloorCount on unit.object_id=UndergroundFloorCount.objectId	
	left outer join FloorNumberAttrValues FloorNumber on unit.object_id=FloorNumber.objectId	
	left outer join WallMaterialAttrValues WallMaterial on unit.object_id=WallMaterial.objectId	
	left outer join cadastralQuartalAttrValues cadastralQuartalGbu on unit.object_id=cadastralQuartalGbu.objectId
