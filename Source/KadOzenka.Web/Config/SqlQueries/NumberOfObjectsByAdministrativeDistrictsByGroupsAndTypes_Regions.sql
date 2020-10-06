WITH cte1 AS (
SELECT
        L1_R201.ID AS id,
		L1_R107.DISTRICT_CODE AS DistrictCode,
        L1_R107.REGION_CODE as RegionCode,
        L1_R201.PROPERTY_TYPE_CODE AS PropertyTypeCode,
		(SELECT case when L2_R205.NUMBER is null then L2_R205.GROUP_NAME 
                else CONCAT(L2_R205.NUMBER, '. ', L2_R205.GROUP_NAME) end
        FROM KO_GROUP L2_R205 WHERE(L2_R205.ID = L1_R205.PARENT_ID)) AS ParentGroup
FROM KO_UNIT L1_R201
JOIN MARKET_REGION_DICTIONATY L1_R107 ON (L1_R201.CADASTRAL_BLOCK = L1_R107.CADASTRAL_QUARTAL)
LEFT JOIN KO_GROUP L1_R205 ON (L1_R201.GROUP_ID = L1_R205.ID) 
WHERE (L1_R201.TASK_ID IN ({0}) AND L1_R201.OBJECT_ID IS NOT NULL
    and case when '{1}'= 'True' then L1_R201.PROPERTY_TYPE_CODE<>4
			else L1_R201.PROPERTY_TYPE_CODE=4
		end)
)
SELECT DistrictCode, RegionCode, PropertyTypeCode, count(id) AS objectsCount, ParentGroup FROM cte1
GROUP BY (DistrictCode, RegionCode, PropertyTypeCode, ParentGroup);