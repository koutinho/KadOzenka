WITH cte1 AS (
SELECT
        L1_R201.ID AS id,
        L1_R201.PROPERTY_TYPE AS PropertyType,
        (case when L1_R205.NUMBER is null then L1_R205.GROUP_NAME 
                else CONCAT(L1_R205.NUMBER, '. ', L1_R205.GROUP_NAME) end) AS Group,
        L1_R205.NUMBER AS GroupNumber,
        (SELECT case when L2_R205.NUMBER is null then L2_R205.GROUP_NAME 
                else CONCAT(L2_R205.NUMBER, '. ', L2_R205.GROUP_NAME) end AS "20500300" 
        FROM KO_GROUP L2_R205 WHERE (L2_R205.ID = L1_R205.PARENT_ID)) AS ParentGroup,
        (SELECT L2_R205.NUMBER 
        FROM KO_GROUP L2_R205 WHERE (L2_R205.ID = L1_R205.PARENT_ID)) AS ParentGroupNumber
FROM KO_UNIT L1_R201 LEFT JOIN KO_GROUP L1_R205 ON (L1_R201.GROUP_ID = L1_R205.ID)
WHERE (L1_R201.TASK_ID IN ({0}) AND L1_R201.PROPERTY_TYPE_CODE != 4 AND L1_R201.PROPERTY_TYPE_CODE<>2190))
SELECT PropertyType, count(id) AS objectsCount, cte1.Group, cte1.ParentGroup FROM cte1
GROUP BY (cte1.PropertyType, cte1.Group, cte1.ParentGroup)
order by min(cte1.ParentGroupNumber::int), min(nullif(split_part(cte1.GroupNumber, '.', 2), '')::numeric);