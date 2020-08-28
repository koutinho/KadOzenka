with unit_data as(
	select u.object_id,
	u.CADASTRAL_NUMBER,
	u.CADASTRAL_BLOCK,
	u.PROPERTY_TYPE,
	u.SQUARE
	from ko_unit u
	where u.property_type_code=4 and u.task_id IN ({0})
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
)

select
	u.PROPERTY_TYPE as PropertyType,
	u.CADASTRAL_NUMBER as CadastralNumber,
	u.CADASTRAL_BLOCK as CadastralQuarter,
	u.SQUARE as Square,
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
from unit_data  u
	left outer join LinkedObjectsInfoAttrValues LinkedObjectsInfo on u.object_id=LinkedObjectsInfo.objectId
	left outer join LinkedObjectsInfoSourceAttrValues LinkedObjectsInfoSource on u.object_id=LinkedObjectsInfoSource.objectId
	left outer join SegmentAttrValues Segment on u.object_id=Segment.objectId
	left outer join TypeOfUsingNameAttrValues TypeOfUsingName on u.object_id=TypeOfUsingName.objectId
	left outer join TypeOfUsingCodeAttrValues TypeOfUsingCode on u.object_id=TypeOfUsingCode.objectId
	left outer join TypeOfUsingCodeSourceAttrValues TypeOfUsingCodeSource on u.object_id=TypeOfUsingCodeSource.objectId
	left outer join NameAttrValues Name on u.object_id=Name.objectId	
	left outer join PermittedUsingAttrValues PermittedUsing on u.object_id=PermittedUsing.objectId	
	left outer join AddressAttrValues Address on u.object_id=Address.objectId	
	left outer join LocationAttrValues Location on u.object_id=Location.objectId
