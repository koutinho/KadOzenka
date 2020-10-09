with unit_data as(
	select u.object_id as ObjectId,
	u.PROPERTY_TYPE as PropertyType,
	u.CADASTRAL_NUMBER as CadastralNumber,
	(SELECT refItem.VALUE
	FROM CORE_REFERENCE_ITEM refItem
	WHERE refItem.ITEMID = model.CALCULATION_METHOD
	) AS ModelCalculationMethod
	from ko_unit u
	left join ko_model model on u.GROUP_ID = model.GROUP_ID
	where u.task_id IN ({0}) and u.PROPERTY_TYPE_CODE<>2190
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