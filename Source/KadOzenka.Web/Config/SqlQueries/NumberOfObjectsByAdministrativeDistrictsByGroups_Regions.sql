WITH unit_data AS (
SELECT
        L1_R201.ID AS id,
        L1_R201.OBJECT_ID as ObjectId,
        L1_R201.CADASTRAL_BLOCK AS CadastralQuartal,
        (SELECT case when L2_R205.NUMBER is null then L2_R205.GROUP_NAME 
                else CONCAT(L2_R205.NUMBER, '. ', L2_R205.GROUP_NAME) end AS "20500300" 
         FROM KO_GROUP L2_R205 WHERE(L2_R205.ID = L1_R205.PARENT_ID)) AS ParentGroup,
         (SELECT L2_R205.NUMBER
            FROM KO_GROUP L2_R205 WHERE(L2_R205.ID = L1_R205.PARENT_ID)) AS ParentGroupNumber
FROM KO_UNIT L1_R201
JOIN MARKET_REGION_DICTIONATY L1_R107 ON (L1_R201.CADASTRAL_BLOCK = L1_R107.CADASTRAL_QUARTAL)
LEFT JOIN KO_GROUP L1_R205 ON (L1_R201.GROUP_ID = L1_R205.ID)
WHERE (L1_R201.TASK_ID IN ({0})
    and case when '{1}'= 'True' then L1_R201.PROPERTY_TYPE_CODE<>4 and L1_R201.PROPERTY_TYPE_CODE<>2190
			else L1_R201.PROPERTY_TYPE_CODE=4
		end)
),

cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select ObjectId from unit_data), {2})
),

data as(
	select u.id,
	L1_R107.DISTRICT_CODE AS DistrictCode,
    L1_R107.REGION_CODE as RegionCode,
    u.ParentGroup, u.ParentGroupNumber
	from unit_data u
    left outer join cadastralQuartalAttrValues cadastralQuartalGbu on u.ObjectId=cadastralQuartalGbu.objectId
	JOIN MARKET_REGION_DICTIONATY L1_R107 
		ON COALESCE(cadastralQuartalGbu.attributeValue, u.CadastralQuartal)=L1_R107.CADASTRAL_QUARTAL
)
SELECT DistrictCode, RegionCode, ParentGroup, count(id) AS objectsCount FROM data 
GROUP BY (data.DistrictCode, data.RegionCode, data.ParentGroup)
order by min(ParentGroupNumber::int);