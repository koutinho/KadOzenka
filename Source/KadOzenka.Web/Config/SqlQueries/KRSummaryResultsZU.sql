with unit_data as(
	select u.object_id,
	u.CADASTRAL_NUMBER,
	u.CADASTRAL_BLOCK,
	u.PROPERTY_TYPE,
	u.SQUARE,
	u.UPKS,
	u.CADASTRAL_COST
	from ko_unit u
	where u.property_type_code=4 and u.task_id IN ({0})
),

KladrAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {1})
),

PermittedUsingAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {2})
),

AddressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {3})
),

LocationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {4})
),

LandCategoryAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from unit_data), {5})
)

select
	u.CADASTRAL_NUMBER as CadastralNumber,
	u.CADASTRAL_BLOCK as CadastralQuarter,
	u.PROPERTY_TYPE as PropertyType,
	u.SQUARE as Square,
	u.UPKS as Upks,
	u.CADASTRAL_COST as CadastralCost,
	Kladr.attributeValue as Kladr,
	PermittedUsing.attributeValue as PermittedUsing,
	Address.attributeValue as Address,
	Location.attributeValue as Location,
	LandCategory.attributeValue as LandCategory
from unit_data  u
	left outer join KladrAttrValues Kladr on u.object_id=Kladr.objectId
	left outer join PermittedUsingAttrValues PermittedUsing on u.object_id=PermittedUsing.objectId
	left outer join AddressAttrValues Address on u.object_id=Address.objectId
	left outer join LocationAttrValues Location on u.object_id=Location.objectId
	left outer join LandCategoryAttrValues LandCategory on u.object_id=LandCategory.objectId