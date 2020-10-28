with districtsDictionary as(
	select itemId as DistrictCode,
	short_title as ShortTitle
	from core_reference_item
	where referenceid=119
),

initialData as(
	SELECT 
		u.OBJECT_ID as ObjectId,
		u.CADASTRAL_BLOCK AS CadastralQuartal,
		(SELECT case when parentGroup.NUMBER is null then parentGroup.GROUP_NAME 
                else CONCAT(parentGroup.NUMBER, '. ', parentGroup.GROUP_NAME) end
			FROM KO_GROUP parentGroup
			WHERE parentGroup.ID = subgroup.PARENT_ID
		) AS ParentGroup,
		(SELECT parentGroup.NUMBER
			FROM KO_GROUP parentGroup
			WHERE parentGroup.ID = subgroup.PARENT_ID
		) AS ParentGroupNumber,
		u.CADASTRAL_COST AS ObjectCost,
		u.SQUARE AS ObjectSquare
	FROM ko_unit u
		LEFT JOIN KO_GROUP subgroup ON (u.GROUP_ID = subgroup.ID)
	WHERE u.TASK_ID IN ({1})
	AND case when '{2}'= 'True' then u.PROPERTY_TYPE_CODE<>4 AND u.PROPERTY_TYPE_CODE<>2190
			else u.PROPERTY_TYPE_CODE=4
		end
),

cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select ObjectId from initialData), {3})
),

unit_data as (
select u.ParentGroup, u.ParentGroupNumber, u.ObjectCost, u.ObjectSquare,
	case 
		when '{0}'='Districts' then districtsDict.ShortTitle
		when '{0}'='RegionNumbers' then left(marketDict.CADASTRAL_QUARTAL, 5)
		when '{0}'='Quarters' then marketDict.CADASTRAL_QUARTAL
	end as Name
	from initialData u
		left outer join cadastralQuartalAttrValues cadastralQuartalGbu on u.ObjectId=cadastralQuartalGbu.objectId
		JOIN MARKET_REGION_DICTIONATY marketDict 
			ON COALESCE(cadastralQuartalGbu.attributeValue, u.CadastralQuartal)=marketDict.CADASTRAL_QUARTAL
		JOIN districtsDictionary districtsDict on districtsDict.DistrictCode=marketDict.DISTRICT_CODE
)

select * from (
	(select d.Name as Name, 
	 0 as IsTotal,
	COALESCE(d.ParentGroup, 'Без группы') as GroupName,
	min(d.ParentGroupNumber::int) as GroupNumber,
	case when d.ParentGroup is null then 1 else 0 end as HasGroup,
	sum(d.ObjectCost)/ nullif(sum(d.ObjectSquare), 0) as UpksAverageWeight
	from unit_data d
	group by Name, GroupName, HasGroup)
union all
	(select 'Итого' as Name, 
	 1 as IsTotal,
	 dg.GroupName,
	 dg.GroupNumber,
	 dg.HasGroup,
	 dg.UpksAverageWeight
	 from ( select
		COALESCE(d.ParentGroup, 'Без группы') as GroupName,
		min(d.ParentGroupNumber::int) as GroupNumber,
		case when d.ParentGroup is null then 1 else 0 end as HasGroup,
		sum(d.ObjectCost)/ nullif(sum(d.ObjectSquare), 0) as UpksAverageWeight
		from unit_data d
		group by GroupName, HasGroup
	 ) dg)
) res
	order by IsTotal, HasGroup desc, GroupNumber, Name
	