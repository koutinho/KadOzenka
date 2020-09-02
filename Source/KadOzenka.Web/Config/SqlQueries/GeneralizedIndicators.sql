﻿with districtsDictionary as(
	select itemId as DistrictCode,
	short_title as ShortTitle
	from core_reference_item
	where referenceid=119
),

initialData as(
	SELECT 
		case 
			when '{0}'='Districts' then districtsDict.ShortTitle
			when '{0}'='Regions' then marketDict.REGION
			when '{0}'='RegionNumbers' then left(marketDict.CADASTRAL_QUARTAL, 5)
			when '{0}'='Quarters' then marketDict.CADASTRAL_QUARTAL
		end as Name,
		case 
			when '{0}'='Quarters' then left(marketDict.CADASTRAL_QUARTAL, 5) else null
		end as AdditionalName,
		(SELECT parentGroup.GROUP_NAME
			FROM KO_GROUP parentGroup
			WHERE parentGroup.ID = subgroup.PARENT_ID
		) AS ParentGroup,
		u.CADASTRAL_COST AS ObjectCost,
		u.SQUARE AS ObjectSquare,
		u.UPKS AS ObjectUpks
	FROM ko_unit u
		JOIN MARKET_REGION_DICTIONATY marketDict on u.CADASTRAL_BLOCK = marketDict.CADASTRAL_QUARTAL
		JOIN districtsDictionary districtsDict on districtsDict.DistrictCode=marketDict.DISTRICT_CODE
		LEFT JOIN KO_GROUP subgroup ON (u.GROUP_ID = subgroup.ID)
	WHERE u.TASK_ID IN ({1})
		and case 
			when '{2}'='Oks' then u.property_type_code<>4
			when '{2}'='Zu' then u.property_type_code=4
			when '{2}'='OksAndZu' then true end
),

obj_count as (
	select
		d.Name,
		d.AdditionalName,
		count(*) as OBJECTS_COUNT
	from initialData d
	group by d.Name, d.AdditionalName
	
	union all
	
	select
		'Итого' as Name,
		'Итого' as AdditionalName,
		count(*) as OBJECTS_COUNT
	from initialData d
),


result_data as (
	(select
		d.Name,
		d.AdditionalName,
		0 as IsTotal,
		COALESCE(d.ParentGroup, 'Без группы') as GroupName,
		case when d.ParentGroup is null then 1 else 0 end as HasGroup,
		c.OBJECTS_COUNT ObjectsCount,
		min(d.ObjectUpks) as MIN_UPKS,
		sum(d.ObjectCost) / nullif(sum(d.ObjectSquare), 0) as AVG_WEIGHT_UPKS,
		max(d.ObjectUpks) as MAX_UPKS
	from initialData d
	join obj_count c on d.Name=c.Name and (d.AdditionalName=c.AdditionalName or d.AdditionalName is null)
	group by d.Name, d.AdditionalName, c.OBJECTS_COUNT, GroupName, HasGroup
	order by HasGroup desc)
	
	union all
	
	(select 'Итого' as Name, 
	'Итого' as AdditionalName, 
	 1 as IsTotal,
	 dg.GroupName,
	 dg.HasGroup,
	 c.OBJECTS_COUNT as ObjectsCount,
	 dg.MIN_UPKS,
	 dg.AVG_WEIGHT_UPKS,
	 dg.MAX_UPKS
	 from ( select
		COALESCE(d.ParentGroup, 'Без группы') as GroupName,
		case when d.ParentGroup is null then 1 else 0 end as HasGroup,
		min(d.ObjectUpks) as MIN_UPKS,
		sum(d.ObjectCost) / nullif(sum(d.ObjectSquare), 0) as AVG_WEIGHT_UPKS,
		max(d.ObjectUpks) as MAX_UPKS
		from initialData d
		group by GroupName, HasGroup
	 ) dg, obj_count c
	where c.Name='Итого' and c.AdditionalName='Итого'
	order by HasGroup desc)
), 
	
upksCalcTypes (minCalcType, avgCalcType, avgWeightCalcType, maxCalcType) as (
   values (0, 1, 2, 3)
)

select * from (
	select rd.Name as Name,
		rd.AdditionalName as AdditionalName,
		rd.IsTotal,
		CAST(rd.ObjectsCount as int) as ObjectsCount,
		rd.GroupName as GroupName,
		rd.HasGroup,
		calcTypes.minCalcType as UpksCalcType,
		rd.MIN_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select rd.Name as Name,
		rd.AdditionalName as AdditionalName,
		rd.IsTotal,
		CAST(rd.ObjectsCount as int) as ObjectsCount,
		rd.GroupName as GroupName,
		rd.HasGroup,
		calcTypes.avgWeightCalcType as UpksCalcType,
		rd.AVG_WEIGHT_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
	union all
	select rd.Name as Name,
		rd.AdditionalName as AdditionalName,
		rd.IsTotal,
		CAST(rd.ObjectsCount as int) as ObjectsCount,
		rd.GroupName as GroupName,
		rd.HasGroup,
		calcTypes.maxCalcType as UpksCalcType,
		rd.MAX_UPKS as UpksCalcValue
	from result_data rd, upksCalcTypes calcTypes
) res
order by IsTotal, HasGroup desc, UpksCalcType
	