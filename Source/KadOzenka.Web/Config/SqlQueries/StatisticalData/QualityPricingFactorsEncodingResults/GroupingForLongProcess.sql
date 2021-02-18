with unit_data as(
	select unit.object_id as ObjectId,
	unit.PROPERTY_TYPE as PropertyType,
	unit.CADASTRAL_NUMBER as CadastralNumber,
	(SELECT refItem.VALUE
	FROM CORE_REFERENCE_ITEM refItem
	WHERE refItem.ITEMID = model.CALCULATION_METHOD
	) AS ModelCalculationMethod
	from ko_unit unit
	left join ko_model model on unit.GROUP_ID = model.GROUP_ID and model.is_active = 1
	{0}
),

codeGroupAttrAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select ObjectId from unit_data), {1})
)

select
	u.PropertyType,
	u.CadastralNumber,
	codeGroup.attributeValue as GroupNumber,
	u.ModelCalculationMethod
from unit_data u
	left outer join codeGroupAttrAttrValues codeGroup on u.ObjectId=codeGroup.objectId