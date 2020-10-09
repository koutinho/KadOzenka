with unit_init_data as(
SELECT 
	L1_R200.ID AS GbuObjectId,
	L1_R200.CADASTRAL_NUMBER AS CadastralNumber,
	L1_R200.OBJECT_TYPE_CODE AS PropertyType,
	L1_R201.CADASTRAL_BLOCK AS CadastralQuartal,
	L1_R203.NOTE_TYPE_CODE AS TaskType,
	(case when L1_R205.NUMBER is null then L1_R205.GROUP_NAME 
          else CONCAT(L1_R205.NUMBER, '. ', L1_R205.GROUP_NAME) end) AS GroupName,
	(SELECT (case when L2_R205.NUMBER is null then L2_R205.GROUP_NAME 
            else CONCAT(L2_R205.NUMBER, '. ', L2_R205.GROUP_NAME) end) AS "20500300"
		FROM KO_GROUP L2_R205
		WHERE L2_R205.ID = L1_R205.PARENT_ID
	) AS ParentGroup,
	L1_R201.UPKS AS ObjectUpks,
	L1_R201.CADASTRAL_COST AS ObjectCost,
	L1_R201.CREATION_DATE AS UnitCreationDate,
	L1_R201.STATUS_CODE AS Status,
	(SELECT L2.ChangedFactors
				 FROM
					(SELECT L3.id_unit, string_agg(L3.status_change, ', ') AS ChangedFactors 
					FROM
						(SELECT DISTINCT id_unit, status_change
						FROM ko_unit_change) L3
					GROUP  BY 1
					) L2
				 WHERE L2.id_unit = L1_R201.ID
	)  AS ChangedFactors
FROM KO_UNIT L1_R201
	 JOIN GBU_MAIN_OBJECT L1_R200
	 	ON (L1_R201.OBJECT_ID = L1_R200.ID)
	 JOIN KO_TASK L1_R203
	 	ON (L1_R201.TASK_ID = L1_R203.ID)
	 LEFT JOIN KO_GROUP L1_R205
	 	ON (L1_R201.GROUP_ID = L1_R205.ID)
WHERE L1_R201.TASK_ID in ({0}) and L1_R201.property_type_code<>2190
),

cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_init_data), {1})
),

unit_data as(
	select u.GbuObjectId, u.CadastralNumber, u.PropertyType, u.TaskType, u.GroupName, u.ParentGroup, 
		u.ObjectUpks, u.ObjectCost, u.UnitCreationDate, u.Status, u.ChangedFactors,
		COALESCE(cadastralQuartalGbu.attributeValue, u.CadastralQuartal) as CadastralQuartal,
		marketDict.ZONE AS Zone
	from unit_init_data u
		left outer join cadastralQuartalAttrValues cadastralQuartalGbu on u.GbuObjectId=cadastralQuartalGbu.objectId
		LEFT JOIN MARKET_REGION_DICTIONATY marketDict 
			ON COALESCE(cadastralQuartalGbu.attributeValue, u.CadastralQuartal)=marketDict.CADASTRAL_QUARTAL
),

RosreestrSquareAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_data), {2})
),

ObjectNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_data), {3})
),

TypeOfUseAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_data), {4})
),

BuildingPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_data), {5})
),

PlacementPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_data), {6})
),

ConstructionPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_data), {7})
),

AddressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_data), {8})
),

LocationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from unit_data), {9})
),

gko2018InitData as (
	select d.CadastralNumber,
	d.GbuObjectId,
	d.CadastralQuartal,
	d.Upks,
	d.CadastralCost,
	d.GroupName,
	d.ParentGroup
	from (
		select u.CADASTRAL_NUMBER as CadastralNumber, u.OBJECT_ID as GbuObjectId,
		row_number() over (PARTITION BY u.CADASTRAL_NUMBER) as num,
		u.CADASTRAL_BLOCK as CadastralQuartal,
		u.UPKS as Upks,
		u.CADASTRAL_COST as CadastralCost,
		(case when g.NUMBER is null then g.GROUP_NAME 
          else CONCAT(g.NUMBER, '. ', g.GROUP_NAME) end) as GroupName,
		(SELECT (case when parent_gr.NUMBER is null then parent_gr.GROUP_NAME 
					else CONCAT(parent_gr.NUMBER, '. ', parent_gr.GROUP_NAME) end)
			FROM KO_GROUP parent_gr
			WHERE parent_gr.ID = g.PARENT_ID
		) AS ParentGroup
		from ko_unit u			
			join unit_data main_data on main_data.CadastralNumber=u.CADASTRAL_NUMBER
			join ko_task task on task.id=u.task_id
			join ko_tour tour on tour.id=task.tour_id
			left join ko_group g on u.group_id=g.id
		where tour.year=2018 and task.note_type_code=4 and u.property_type_code<>2190) d
	where d.num=1
),

cadastralQuartal2018AttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from gko2018InitData), {1})
),

gko2018Data as (
	select d.CadastralNumber,
		COALESCE(cadastralQuartalGbu.attributeValue, d.CadastralQuartal) as CadastralQuartal,
		d.Upks,
		d.CadastralCost,
		d.GroupName,
		d.ParentGroup
	from gko2018InitData d
		left outer join cadastralQuartal2018AttrValues cadastralQuartalGbu on d.GbuObjectId=cadastralQuartalGbu.objectId
),

zones_info as (
	select main_data.Zone,
		min(main_data.ObjectUpks) as MinUpks,
		max(main_data.ObjectUpks) as MaxUpks,
		avg(main_data.ObjectUpks) as AverageUpks
	from unit_data main_data
	where main_data.Zone is not null
	group by main_data.Zone
),

quartals_info as (
	select main_data.CadastralQuartal,
		min(main_data.ObjectUpks) as MinUpks,
		max(main_data.ObjectUpks) as MaxUpks,
		avg(main_data.ObjectUpks) as AverageUpks
	from unit_data main_data
	where main_data.CadastralQuartal is not null
	group by main_data.CadastralQuartal
),

participating_count as (
	select u.CADASTRAL_NUMBER as CadastralNumber, count(*) as Count
	from ko_unit u 
	where exists (select 1 from unit_data main_data where main_data.CadastralNumber=u.CADASTRAL_NUMBER) 
	group by u.CADASTRAL_NUMBER	
),

participating_date_count as (
	select u.CADASTRAL_NUMBER as CadastralNumber, count(*) as Count
	from ko_unit u 
		join ko_task t on t.id=u.task_id
	where exists (select 1 from unit_data main_data where main_data.CadastralNumber=u.CADASTRAL_NUMBER) 
		and  t.note_type_code=1 
	group by u.CADASTRAL_NUMBER
),

participating_year_count as (
	select u.CADASTRAL_NUMBER as CadastralNumber, count(*) as Count
	from ko_unit u 
		join ko_task t on t.id=u.task_id
	where exists (select 1 from unit_data main_data where main_data.CadastralNumber=u.CADASTRAL_NUMBER) 
		and t.note_type_code=3
	group by u.CADASTRAL_NUMBER	
)


select main_data.CadastralNumber as CadastralNumber,
	main_data.PropertyType as Type,
	RosreestrSquare.attributeValue as RosreestrSquareValue,
	case when main_data.PropertyType=4 then TypeOfUse.attributeValue
		else ObjectName.attributeValue 
	end as ObjectNameTypeOfUse,
	case when main_data.PropertyType=5 then BuildingPurpose.attributeValue 
		 when main_data.PropertyType=6 then PlacementPurpose.attributeValue 
		 when main_data.PropertyType=7 then ConstructionPurpose.attributeValue 
		 else null
	end as Purpose,
	Address.attributeValue as Address,
	Location.attributeValue as Location,
	CONCAT(gko2018.ParentGroup, ' ', gko2018.GroupName) as EvaluationSubgroup2018,
	round(gko2018.Upks, 2) as Upks2018,
	gko2018.CadastralCost as CadastralCost2018,
	gko2018.CadastralQuartal as CadastralQuartal2018,
	main_data.TaskType as TaskType,
	CONCAT(main_data.ParentGroup, ' ', main_data.GroupName) as EvaluationSubgroup,
	main_data.ObjectUpks as Upks,
	main_data.ObjectCost as CadastralCost,
	main_data.CadastralQuartal as CadastralQuartal,
	main_data.UnitCreationDate as EGRNChangeDate,
	main_data.Status as Status,
	main_data.ChangedFactors as Changes,
	quartals_info.MinUpks as MinUpksByCadastralQuartal,
	quartals_info.AverageUpks as AverageUpksByCadastralQuartal,
	quartals_info.MaxUpks as MaxUpksByCadastralQuartal,
	zones_info.MinUpks as MinUpksByZone,
	zones_info.AverageUpks as AverageUpksByZone,
	zones_info.MaxUpks as MaxUpksByZone,
	COALESCE(participating.Count, 0) as ParticipatingCount,
	COALESCE(participating_year.Count, 0) as CountInYear,
	COALESCE(participating_date.Count, 0) as CountInDays
from unit_data main_data
	left outer join gko2018Data gko2018 on main_data.CadastralNumber=gko2018.CadastralNumber
	left outer join zones_info zones_info on main_data.Zone=zones_info.Zone
	left outer join quartals_info quartals_info on main_data.CadastralQuartal=quartals_info.CadastralQuartal
	left outer join participating_count participating on main_data.CadastralNumber=participating.CadastralNumber
	left outer join participating_date_count participating_date on main_data.CadastralNumber=participating_date.CadastralNumber
	left outer join participating_year_count participating_year on main_data.CadastralNumber=participating_year.CadastralNumber
	left outer join RosreestrSquareAttrValues RosreestrSquare on main_data.GbuObjectId=RosreestrSquare.objectId
	left outer join ObjectNameAttrValues ObjectName on main_data.GbuObjectId=ObjectName.objectId
	left outer join TypeOfUseAttrValues TypeOfUse on main_data.GbuObjectId=TypeOfUse.objectId
	left outer join BuildingPurposeAttrValues BuildingPurpose on main_data.GbuObjectId=BuildingPurpose.objectId
	left outer join PlacementPurposeAttrValues PlacementPurpose on main_data.GbuObjectId=PlacementPurpose.objectId
	left outer join ConstructionPurposeAttrValues ConstructionPurpose on main_data.GbuObjectId=ConstructionPurpose.objectId
	left outer join AddressAttrValues Address on main_data.GbuObjectId=Address.objectId
	left outer join LocationAttrValues Location on main_data.GbuObjectId=Location.objectId
