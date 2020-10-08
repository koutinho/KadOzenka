
with initialData as (
	SELECT 
		L1_R201.ID,
		L1_R201.OBJECT_ID as ObjectId,
		L1_R201.CADASTRAL_BLOCK AS CadastralQuartal,
		L1_R201.PROPERTY_TYPE,
		L1_R201.UPKS,
		L1_R201.CADASTRAL_COST,
		L1_R201.SQUARE
	FROM KO_UNIT L1_R201
	WHERE L1_R201.TASK_ID IN ({0}) AND L1_R201.PROPERTY_TYPE_CODE<>2190
),

cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select ObjectId from initialData), {1})
),

dataGroupedByQuartalAndPropertyType as (
select 
	L1_R107.CADASTRAL_QUARTAL,
	d.PROPERTY_TYPE,
	count(d.ID) as PROPERTY_TYPE_OBJECTS_COUNT,
	min(d.UPKS) as MIN_UPKS, 
	max(d.UPKS) as MAX_UPKS, 
	avg(d.UPKS) as AVG_UPKS,
	sum(d.CADASTRAL_COST) as SUM_CADASTRAL_COST,
	sum(d.SQUARE) as SUM_SQUARE
from initialData d
	left outer join cadastralQuartalAttrValues cadastralQuartalGbu on d.ObjectId=cadastralQuartalGbu.objectId
	JOIN MARKET_REGION_DICTIONATY L1_R107 
		ON COALESCE(cadastralQuartalGbu.attributeValue, d.CadastralQuartal)=L1_R107.CADASTRAL_QUARTAL
group by L1_R107.CADASTRAL_QUARTAL, d.PROPERTY_TYPE
),

obj_count as (
	select
		dg.CADASTRAL_QUARTAL,
		sum(dg.PROPERTY_TYPE_OBJECTS_COUNT) as OBJECTS_COUNT
	from dataGroupedByQuartalAndPropertyType dg
	group by dg.CADASTRAL_QUARTAL
),

result_data as (
	select
		left(dg.CADASTRAL_QUARTAL, 5) as REGION_NUMBER,
		dg.CADASTRAL_QUARTAL,
		dg.PROPERTY_TYPE,
		c.OBJECTS_COUNT,
		dg.MIN_UPKS,
		dg.AVG_UPKS,
		dg.SUM_CADASTRAL_COST / nullif(dg.SUM_SQUARE, 0) as AVG_WEIGHT_UPKS,
		dg.MAX_UPKS
	from dataGroupedByQuartalAndPropertyType dg
	join obj_count c on dg.CADASTRAL_QUARTAL=c.CADASTRAL_QUARTAL
	order by dg.CADASTRAL_QUARTAL, dg.PROPERTY_TYPE
),

upksCalcTypes (minCalcType, avgCalcType, avgWeightCalcType, maxCalcType) as (
   values (0, 1, 2, 3)
)

select * from (
	select rd.REGION_NUMBER as CadastralRegionNumber,
		rd.CADASTRAL_QUARTAL as CadastralQuater,
		CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		calcTypes.minCalcType as UpksCalcType,
		rd.MIN_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select rd.REGION_NUMBER as CadastralRegionNumber,
		rd.CADASTRAL_QUARTAL as CadastralQuater,
		CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		calcTypes.avgCalcType as UpksCalcType,
		rd.AVG_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select rd.REGION_NUMBER as CadastralRegionNumber,
		rd.CADASTRAL_QUARTAL as CadastralQuater,
		CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		calcTypes.avgWeightCalcType as UpksCalcType,
		rd.AVG_WEIGHT_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select rd.REGION_NUMBER as CadastralRegionNumber,
		rd.CADASTRAL_QUARTAL as CadastralQuater,
		CAST(rd.OBJECTS_COUNT as int) as ObjectsCount,
		rd.PROPERTY_TYPE as PropertyType,
		calcTypes.maxCalcType as UpksCalcType,
		rd.MAX_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes ) res
order by CadastralRegionNumber, CadastralQuater, PropertyType, UpksCalcType
		
