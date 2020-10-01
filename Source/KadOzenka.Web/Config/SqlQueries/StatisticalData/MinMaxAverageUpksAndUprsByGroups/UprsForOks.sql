--если UprsObjectCost = -1, значит не был найден объект-аналог, показатели для УПРС считаться не должны

WITH propertyTypeDictionary as(
	select itemId as PropertyTypeCode,
	value as PropertyType
	from core_reference_item
	where referenceid=102
    AND
    (itemId <> 4 or itemId is null)
    AND
    (itemId <> 0 or itemId is null)
),

object_ids as (
	select u.object_id from ko_unit u where u.task_id IN ({0})
),

buildingAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {1})
),

placementAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {2})
),

initialData as (
	select * from (
		SELECT 
        	(SELECT (COALESCE(L2_R205.NUMBER, '') || '. ' || L2_R205.GROUP_NAME) AS "20500300" FROM KO_GROUP L2_R205 WHERE (L2_R205.ID = L1_R205.PARENT_ID)) ParentGroup,
			{3}
			L1_R201.ID,
			L1_R201.PROPERTY_TYPE,
			L1_R201.PROPERTY_TYPE_CODE,         
            (select get_market_object_price_for_uprs(L1_R201.cadastral_number)) / NULLIF(L1_R201.SQUARE, 0) UprsObjectValue,
            (select get_market_object_price_for_uprs(L1_R201.cadastral_number)) UprsObjectCost,
            L1_R201.SQUARE UprsObjectSquare,         
			(case when L1_R201.PROPERTY_TYPE_CODE=5 or L1_R201.PROPERTY_TYPE_CODE=6
				then true
				else false
			end) as HAS_PURPOSE,
			(case when L1_R201.PROPERTY_TYPE_CODE=5 then buildingPurpose.attributeValue
			when L1_R201.PROPERTY_TYPE_CODE=6 then placementPurpose.attributeValue
			else null end) as PURPOSE		
		FROM KO_UNIT L1_R201
        	LEFT JOIN KO_GROUP L1_R205 ON (L1_R201.GROUP_ID = L1_R205.ID)
			LEFT OUTER JOIN buildingAttrValues buildingPurpose ON L1_R201.object_id=buildingPurpose.objectId
			LEFT OUTER JOIN placementAttrValues placementPurpose ON L1_R201.object_id=placementPurpose.objectId
		WHERE L1_R201.TASK_ID IN ({0})
        AND
        (L1_R201.PROPERTY_TYPE_CODE <> 4 or L1_R201.PROPERTY_TYPE_CODE is null)
         AND
        (L1_R201.PROPERTY_TYPE_CODE <> 0 or L1_R201.PROPERTY_TYPE_CODE is null)
	) d
	where (d.HAS_PURPOSE=false or (d.HAS_PURPOSE=true and d.PURPOSE is not null)) 
),

dataGroupedByPropertyType as (
	select 
    	d.ParentGroup,
		{4}
		d.PROPERTY_TYPE,
		d.PROPERTY_TYPE_CODE,
		d.HAS_PURPOSE,
		d.PURPOSE,
		count(d.ID) as OBJECTS_COUNT,
        min(
            CASE 
              WHEN d.UprsObjectCost = -1 
              THEN NULL 
              ELSE d.UprsObjectValue
            END 
        ) as MIN,
        avg(
            CASE 
              WHEN d.UprsObjectCost = -1 
              THEN NULL 
              ELSE d.UprsObjectValue
            END
        ) as AVG,
        sum(NULLIF(d.UprsObjectCost, -1)) 
        / NULLIF(
        sum(CASE 
              WHEN d.UprsObjectCost = -1 
              THEN NULL 
              ELSE d.UprsObjectSquare
            END),
         0) AS AVGWEIGHT,
          max(
            CASE 
              WHEN d.UprsObjectCost = -1 
              THEN NULL 
              ELSE d.UprsObjectValue
            END
        ) as MAX
	from initialData d
	group by d.ParentGroup, {4} d.PROPERTY_TYPE, d.PROPERTY_TYPE_CODE, d.HAS_PURPOSE, d.PURPOSE
),

result_data as (
	select
    	dg.ParentGroup,
		{5}
        CAST(COALESCE(OBJECTS_COUNT, 0) as int) AS ObjectsCount,
        td.PropertyType as PropertyType,
		td.PropertyTypeCode as PropertyTypeCode,
		dg.HAS_PURPOSE as HasPurpose,
		dg.PURPOSE,	
		dg.MIN,
		dg.AVG,
        dg.AVGWEIGHT,
		dg.MAX
	from dataGroupedByPropertyType dg
	inner join propertyTypeDictionary td on td.PropertyTypeCode=dg.PROPERTY_TYPE_CODE
)


select * FROM result_data rd order by ParentGroup, {6} PropertyTypeCode, Purpose