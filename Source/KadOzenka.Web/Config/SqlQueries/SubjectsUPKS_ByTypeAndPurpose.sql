with propertyTypeDictionary as(
	select itemId as PropertyTypeCode,
	value as PropertyType
	from core_reference_item
	where referenceid=102 AND itemId<>2190
),

initialData as (
		SELECT 
			L1_R201.ID,
			L1_R201.object_id,
			L1_R201.PROPERTY_TYPE,
			L1_R201.PROPERTY_TYPE_CODE,
			L1_R201.UPKS,
			L1_R201.CADASTRAL_COST,
			L1_R201.SQUARE,
			(case when L1_R201.PROPERTY_TYPE='Здание' or L1_R201.PROPERTY_TYPE='Помещение'
				then true
				else false
			end) as HAS_PURPOSE
		FROM KO_UNIT L1_R201
		WHERE L1_R201.TASK_ID IN ({0}) AND L1_R201.PROPERTY_TYPE_CODE<>2190
),

buildingAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from initialData), {1})
),

placementAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from initialData), {2})
),

dataGroupedByPropertyType as (
select * from (
	select 
		d.PROPERTY_TYPE,
		d.PROPERTY_TYPE_CODE,
		d.HAS_PURPOSE,
		(case when d.PROPERTY_TYPE='Здание' then buildingPurpose.attributeValue
			when d.PROPERTY_TYPE='Помещение' then placementPurpose.attributeValue
			else null end) as PURPOSE,	
		count(d.ID) as OBJECTS_COUNT,
		min(d.UPKS) as MIN_UPKS, 
		max(d.UPKS) as MAX_UPKS, 
		avg(d.UPKS) as AVG_UPKS,
		sum(d.CADASTRAL_COST) as SUM_CADASTRAL_COST,
		sum(d.SQUARE) as SUM_SQUARE
	from initialData d
		LEFT OUTER JOIN buildingAttrValues buildingPurpose ON d.object_id=buildingPurpose.objectId
		LEFT OUTER JOIN placementAttrValues placementPurpose ON d.object_id=placementPurpose.objectId
	group by d.PROPERTY_TYPE, d.PROPERTY_TYPE_CODE, d.HAS_PURPOSE, PURPOSE) d1
where (d1.HAS_PURPOSE=false or d1.HAS_PURPOSE=true and d1.PURPOSE is not null)
),

result_data as (
	select
		td.PropertyTypeCode as PROPERTY_TYPE_CODE,
		td.PropertyType as PROPERTY_TYPE,
		dg.HAS_PURPOSE,
		dg.PURPOSE,
		COALESCE(OBJECTS_COUNT, 0) as OBJECTS_COUNT,
		dg.MIN_UPKS,
		dg.AVG_UPKS,
		dg.SUM_CADASTRAL_COST / nullif(dg.SUM_SQUARE, 0) as AVG_WEIGHT_UPKS,
		dg.MAX_UPKS
	from dataGroupedByPropertyType dg
	right join propertyTypeDictionary td on td.PropertyTypeCode=dg.PROPERTY_TYPE_CODE
),

upksCalcTypes (minCalcType, avgCalcType, avgWeightCalcType, maxCalcType) as (
   values (0, 1, 2, 3)
)

select * from (
	select CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		rd.PROPERTY_TYPE_CODE as PropertyTypeCode,
		rd.HAS_PURPOSE as HasPurpose,
		rd.PURPOSE as Purpose,
		calcTypes.minCalcType as UpksCalcType,
		rd.MIN_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		rd.PROPERTY_TYPE_CODE as PropertyTypeCode,
		rd.HAS_PURPOSE as HasPurpose,
		rd.PURPOSE as Purpose,
		calcTypes.avgCalcType as UpksCalcType,
		rd.AVG_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		rd.PROPERTY_TYPE_CODE as PropertyTypeCode,
		rd.HAS_PURPOSE as HasPurpose,
		rd.PURPOSE as Purpose,
		calcTypes.avgWeightCalcType as UpksCalcType,
		rd.AVG_WEIGHT_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		rd.PROPERTY_TYPE_CODE as PropertyTypeCode,
		rd.HAS_PURPOSE as HasPurpose,
		rd.PURPOSE as Purpose,
		calcTypes.maxCalcType as UpksCalcType,
		rd.MAX_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes ) res
order by PropertyTypeCode, Purpose, UpksCalcType

