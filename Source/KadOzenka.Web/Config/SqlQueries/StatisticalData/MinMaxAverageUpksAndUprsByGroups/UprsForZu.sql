--если UprsObjectCost = -1, значит не был найден объект-аналог, показатели для УПРС считаться не должны
select
	temp."ParentGroup",
	{0}
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
  (SELECT case when L2_R205.NUMBER is null then L2_R205.GROUP_NAME 
          else CONCAT(L2_R205.NUMBER, '. ', L2_R205.GROUP_NAME) end AS "GROUP_NAME" FROM KO_GROUP L2_R205 WHERE L2_R205.ID = L1_R205.PARENT_ID) as "ParentGroup",
   (SELECT L2_R205.NUMBER
        FROM KO_GROUP L2_R205 WHERE L2_R205.ID = L1_R205.PARENT_ID) AS ParentGroupNumber,
  {1}
  (select get_market_object_price_for_uprs(L1_R201.cadastral_number)) / NULLIF(L1_R201.SQUARE, 0) as "UprsObjectValue",
  (select get_market_object_price_for_uprs(L1_R201.cadastral_number)) as "UprsObjectCost",
  L1_R201.SQUARE as "UprsObjectSquare"
  FROM KO_UNIT L1_R201 LEFT JOIN KO_GROUP L1_R205 ON (L1_R201.GROUP_ID = L1_R205.ID)
  WHERE 
  (L1_R201.TASK_ID IN ({2}) AND L1_R201.PROPERTY_TYPE_CODE = 4)
) 
as temp
group by temp."ParentGroup" {3}
order by  min(temp.ParentGroupNumber::int) {4}, temp."ParentGroup" {3}