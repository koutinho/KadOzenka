with initialData as (
	SELECT 
		L1_R201.ID,
		L1_R201.PROPERTY_TYPE,
		L1_R201.PROPERTY_TYPE_CODE,
		L1_R201.UPKS,
		L1_R201.CADASTRAL_COST,
		L1_R201.SQUARE
	FROM KO_UNIT L1_R201
	WHERE L1_R201.TASK_ID IN ({0})
),

dataGroupedByPropertyType as (
select 
	d.PROPERTY_TYPE,
	d.PROPERTY_TYPE_CODE,
	count(d.ID) as OBJECTS_COUNT,
	min(d.UPKS) as MIN_UPKS, 
	max(d.UPKS) as MAX_UPKS, 
	avg(d.UPKS) as AVG_UPKS,
	sum(d.CADASTRAL_COST) as SUM_CADASTRAL_COST,
	sum(d.SQUARE) as SUM_SQUARE
from initialData d
group by d.PROPERTY_TYPE, d.PROPERTY_TYPE_CODE
),

propertyTypeDictionary as(
	select itemId as PropertyTypeCode,
	value as PropertyType
	from core_reference_item
	where referenceid=102
),

result_data as (
	select
		td.PropertyTypeCode as PROPERTY_TYPE_CODE,
		td.PropertyType as PROPERTY_TYPE,
		case when dg.OBJECTS_COUNT is not null then dg.OBJECTS_COUNT else 0 end as OBJECTS_COUNT,
		dg.MIN_UPKS,
		dg.AVG_UPKS,
		case 
			when dg.SUM_SQUARE <> 0 then dg.SUM_CADASTRAL_COST / dg.SUM_SQUARE
			else null
		end as AVG_WEIGHT_UPKS,
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
		calcTypes.minCalcType as UpksCalcType,
		rd.MIN_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		rd.PROPERTY_TYPE_CODE as PropertyTypeCode,
		calcTypes.avgCalcType as UpksCalcType,
		rd.AVG_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		rd.PROPERTY_TYPE_CODE as PropertyTypeCode,
		calcTypes.avgWeightCalcType as UpksCalcType,
		rd.AVG_WEIGHT_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		rd.PROPERTY_TYPE_CODE as PropertyTypeCode,
		calcTypes.maxCalcType as UpksCalcType,
		rd.MAX_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes ) res
order by PropertyTypeCode, UpksCalcType
