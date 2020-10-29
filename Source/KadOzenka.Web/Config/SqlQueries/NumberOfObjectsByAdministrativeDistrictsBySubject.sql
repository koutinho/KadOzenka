WITH cte1 AS (
SELECT
        L1_R201.ID AS id,
        L1_R201.PROPERTY_TYPE_CODE AS PropertyTypeCode,
		(SELECT case when L2_R205.NUMBER is null then L2_R205.GROUP_NAME 
                else CONCAT(L2_R205.NUMBER, '. ', L2_R205.GROUP_NAME) end
        FROM KO_GROUP L2_R205 WHERE(L2_R205.ID = L1_R205.PARENT_ID)) AS ParentGroup,
        (SELECT L2_R205.NUMBER
            FROM KO_GROUP L2_R205 WHERE(L2_R205.ID = L1_R205.PARENT_ID)) AS ParentGroupNumber
FROM KO_UNIT L1_R201
LEFT JOIN KO_GROUP L1_R205 ON (L1_R201.GROUP_ID = L1_R205.ID) 
WHERE (L1_R201.TASK_ID IN ({0}) AND L1_R201.OBJECT_ID IS NOT NULL
    and case when '{1}'= 'True' then L1_R201.PROPERTY_TYPE_CODE<>4 and L1_R201.PROPERTY_TYPE_CODE<>2190
			else L1_R201.PROPERTY_TYPE_CODE=4
		end)
)
SELECT PropertyTypeCode, count(id) AS objectsCount, ParentGroup FROM cte1
GROUP BY (PropertyTypeCode, ParentGroup)
order by min(ParentGroupNumber::int);