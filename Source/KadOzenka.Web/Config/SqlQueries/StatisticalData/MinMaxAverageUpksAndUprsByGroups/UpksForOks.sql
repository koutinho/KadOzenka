with propertyTypeDictionary as(
	select itemId as PropertyTypeCode,
	value as PropertyType
	from core_reference_item
	where referenceid=102
    AND
    (itemId <> 4 or itemId is null)
    AND
    (itemId <> 0 or itemId is null)
	AND
    (itemId <> 2190 or itemId is null)
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
        	(SELECT case when L2_R205.NUMBER is null then L2_R205.GROUP_NAME 
                else CONCAT(L2_R205.NUMBER, '. ', L2_R205.GROUP_NAME) end AS "20500300" FROM KO_GROUP L2_R205 WHERE (L2_R205.ID = L1_R205.PARENT_ID)) ParentGroup,
			(SELECT L2_R205.NUMBER 
			FROM KO_GROUP L2_R205 WHERE (L2_R205.ID = L1_R205.PARENT_ID)) AS ParentGroupNumber,
            {3}
			L1_R201.ID,
			L1_R201.PROPERTY_TYPE,
			L1_R201.PROPERTY_TYPE_CODE,
			L1_R201.UPKS,
			L1_R201.CADASTRAL_COST,
			L1_R201.SQUARE,
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
		 AND
        (L1_R201.PROPERTY_TYPE_CODE <> 2190 or L1_R201.PROPERTY_TYPE_CODE is null)
	) d
	where (d.HAS_PURPOSE=false or (d.HAS_PURPOSE=true and d.PURPOSE is not null)) 
),

dataGroupedByPropertyType as (
	select 
    	d.ParentGroup,
		min(d.ParentGroupNumber::int) as ParentGroupNumber,
        {4}
		{5}
		d.PROPERTY_TYPE,
		d.PROPERTY_TYPE_CODE,
		d.HAS_PURPOSE,
		d.PURPOSE,
		count(d.ID) as OBJECTS_COUNT,
		min(d.UPKS) as MIN_UPKS, 
		max(d.UPKS) as MAX_UPKS, 
		avg(d.UPKS) as AVG_UPKS,
		sum(d.CADASTRAL_COST) as SUM_CADASTRAL_COST,
		sum(d.SQUARE) as SUM_SQUARE
	from initialData d
	group by d.ParentGroup, {4} d.PROPERTY_TYPE, d.PROPERTY_TYPE_CODE, d.HAS_PURPOSE, d.PURPOSE
),

result_data as (
	select
		td.PropertyTypeCode as PROPERTY_TYPE_CODE,
		td.PropertyType as PROPERTY_TYPE,
        dg.ParentGroup,
		dg.ParentGroupNumber,
        {6}
		dg.HAS_PURPOSE,
		dg.PURPOSE,
		COALESCE(OBJECTS_COUNT, 0) AS OBJECTS_COUNT,
		dg.MIN_UPKS,
		dg.AVG_UPKS,
		dg.SUM_CADASTRAL_COST / NULLIF(dg.SUM_SQUARE, 0) as AVG_WEIGHT_UPKS,
		dg.MAX_UPKS
	from dataGroupedByPropertyType dg
	inner join propertyTypeDictionary td on td.PropertyTypeCode=dg.PROPERTY_TYPE_CODE
)

select
    	rd.ParentGroup, 
        {7}
    	CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		rd.PROPERTY_TYPE_CODE as PropertyTypeCode,
		rd.HAS_PURPOSE as HasPurpose,
		rd.PURPOSE as Purpose,
		rd.MIN_UPKS as Min,
        rd.AVG_UPKS as Avg,
        rd.AVG_WEIGHT_UPKS as AvgWeight,
        rd.MAX_UPKS as Max
	from result_data rd

order by ParentGroupNumber, {8} ParentGroup, {9} PropertyTypeCode, Purpose