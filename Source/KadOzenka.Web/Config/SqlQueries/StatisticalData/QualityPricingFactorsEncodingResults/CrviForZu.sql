with unit_data as(
	select unit.object_id,
	unit.CADASTRAL_NUMBER,
	unit.CADASTRAL_BLOCK,
	unit.PROPERTY_TYPE,
	unit.SQUARE
	from ko_unit unit {0}
),

LinkedObjectsInfoAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {1})
),

LinkedObjectsInfoSourceAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {2})
),

SegmentAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {3})
),

TypeOfUsingNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {4})
),

TypeOfUsingCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {5})
),

TypeOfUsingCodeSourceAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {6})
),

NameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {7})
),

PermittedUsingAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {8})
),

AddressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {9})
),

LocationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {10})
),

cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {11})
)

select
	unit.PROPERTY_TYPE as PropertyType,
	unit.CADASTRAL_NUMBER as CadastralNumber,
	COALESCE(cadastralQuartalGbu.attributeValue, unit.CADASTRAL_BLOCK) as CadastralQuarter,
	unit.SQUARE as Square,
	LinkedObjectsInfo.attributeValue as LinkedObjectsInfo,
	LinkedObjectsInfoSource.attributeValue as LinkedObjectsInfoSource,
	Segment.attributeValue as Segment,
	TypeOfUsingName.attributeValue as TypeOfUsingName,
	TypeOfUsingCode.attributeValue as TypeOfUsingCode,
	TypeOfUsingCodeSource.attributeValue as TypeOfUsingCodeSource,
	Name.attributeValue as Name,
	PermittedUsing.attributeValue as PermittedUsing,
	Address.attributeValue as Address,
	Location.attributeValue as Location
from unit_data unit
	left outer join LinkedObjectsInfoAttrValues LinkedObjectsInfo on unit.object_id=LinkedObjectsInfo.objectId
	left outer join LinkedObjectsInfoSourceAttrValues LinkedObjectsInfoSource on unit.object_id=LinkedObjectsInfoSource.objectId
	left outer join SegmentAttrValues Segment on unit.object_id=Segment.objectId
	left outer join TypeOfUsingNameAttrValues TypeOfUsingName on unit.object_id=TypeOfUsingName.objectId
	left outer join TypeOfUsingCodeAttrValues TypeOfUsingCode on unit.object_id=TypeOfUsingCode.objectId
	left outer join TypeOfUsingCodeSourceAttrValues TypeOfUsingCodeSource on unit.object_id=TypeOfUsingCodeSource.objectId
	left outer join NameAttrValues Name on unit.object_id=Name.objectId	
	left outer join PermittedUsingAttrValues PermittedUsing on unit.object_id=PermittedUsing.objectId	
	left outer join AddressAttrValues Address on unit.object_id=Address.objectId	
	left outer join LocationAttrValues Location on unit.object_id=Location.objectId
	left outer join cadastralQuartalAttrValues cadastralQuartalGbu on unit.object_id=cadastralQuartalGbu.objectId
