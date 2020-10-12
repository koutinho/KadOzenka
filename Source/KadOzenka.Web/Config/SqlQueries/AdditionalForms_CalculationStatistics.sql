with group_data as (
	select distinct g.id,
		nullif(split_part(g.number, '.', 2), '')::DECIMAL as subgroupNumber,
		nullif(pg.number, '')::DECIMAL as parentGroupNumber,
		REPLACE(g.number,'.','_') as subgroupName,
		g.group_algoritm
	from ko_group g
		join ko_unit u on u.group_id=g.id
		left join ko_group pg on g.parent_id=pg.id
	where u.task_id in ({0})
	and case when '{1}'= 'True' then u.PROPERTY_TYPE_CODE<>4 and u.PROPERTY_TYPE_CODE<>2190
			else u.PROPERTY_TYPE_CODE=4
		end
)

select * from (
	select 
		g.id as SubgroupId,
		g.subgroupName as SubgroupName,
		g.subgroupNumber as SubgroupNumber,
		g.parentGroupNumber as ParentGroupNumber,
		g.group_algoritm as CalculationMethod,
		m.formula as Formula,
		a.name as FactorsSubgroups,
		mf.weight as Coef,
		case 
			when mf.sign_market=1 then 'Да' else null 
		end as SighMarket
	from group_data g
		left join ko_model m on g.id=m.group_id
		left join ko_model_factor mf on m.id=mf.model_id
		join core_register_attribute a on mf.factor_id=a.id
	where g.group_algoritm=1

	union all

	select 
		g.id as SubgroupId,
		g.subgroupName as SubgroupName,
		g.subgroupNumber as SubgroupNumber,
		g.parentGroupNumber as ParentGroupNumber,
		g.group_algoritm as CalculationMethod,
		null as Formula,
		REPLACE(cbg.number,'.','_') as FactorsSubgroups,
		null as Coef,
		null as SighMarket
	from group_data g
		left join ko_calc_group cg on g.id=cg.group_id
		left join ko_group cbg on cg.parent_calc_group_id=cbg.id
	where g.group_algoritm<>1
) d
order by ParentGroupNumber, SubgroupNumber
