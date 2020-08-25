--если UprsObjectCost = -1, значит не был найден объект-аналог, показатели для УПРС считаться не должны
select
	temp."ParentGroup",
    temp."SubGroup",
	count(*) as "ObjectsCount",
    
    min(
        CASE 
          WHEN temp."UprsObjectCost" = -1 
          THEN NULL 
          ELSE temp."UprsObjectValue"
     	END 
    ) as "Min",
    
    avg(
    	CASE 
          WHEN temp."UprsObjectCost" = -1 
          THEN NULL 
          ELSE temp."UprsObjectValue"
     	END
    ) as "Avg",
    
    sum(NULLIF(temp."UprsObjectCost", -1)) 
    / NULLIF(
    sum(CASE 
          WHEN temp."UprsObjectCost" = -1 
          THEN NULL 
          ELSE temp."UprsObjectSquare"
     	END),
     0)  
    AS "AvgWeight",
    
    max(
    	CASE 
          WHEN temp."UprsObjectCost" = -1 
          THEN NULL 
          ELSE temp."UprsObjectValue"
     	END
    ) as "Max"
    
from
(
  select
  (SELECT L2_R205.GROUP_NAME AS "GROUP_NAME" FROM KO_GROUP L2_R205 WHERE L2_R205.ID = L1_R205.PARENT_ID) as "ParentGroup",
  L1_R205.GROUP_NAME as "SubGroup",
  (select get_market_object_price_for_uprs(L1_R201.cadastral_number)) / NULLIF(L1_R201.SQUARE, 0) as "UprsObjectValue",
  (select get_market_object_price_for_uprs(L1_R201.cadastral_number)) as "UprsObjectCost",
  L1_R201.SQUARE as "UprsObjectSquare"
  FROM KO_UNIT L1_R201 LEFT JOIN KO_GROUP L1_R205 ON (L1_R201.GROUP_ID = L1_R205.ID)
  WHERE 
  (L1_R201.TASK_ID IN ({0}) AND L1_R201.PROPERTY_TYPE_CODE = 4)
) 
as temp
group by temp."ParentGroup", temp."SubGroup"
order by temp."ParentGroup", temp."SubGroup"