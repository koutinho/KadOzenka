WITH unit_data AS (
		SELECT
			L1_R201.ID AS id, 
			L1_R201.OBJECT_ID as ObjectId,
			L1_R201.CADASTRAL_BLOCK AS CadastralQuartal,
			L1_R201.PROPERTY_TYPE_CODE AS propertyTypeCode, 
			L1_R201.UPKS AS objectUpks, 
			L1_R201.CADASTRAL_COST AS objectCost, 
			L1_R201.SQUARE AS objectSquare 
		FROM KO_UNIT L1_R201
		WHERE (L1_R201.TASK_ID IN ({0}) AND L1_R201.PROPERTY_TYPE_CODE<>2190)
),

cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select ObjectId from unit_data), {1})
),

cte2 AS (
	SELECT 
		L1_R107.DISTRICT_CODE as districtCode, 
		u.propertyTypeCode as propertyTypeCode,  
		count(u.id) as objectsCount, 
		MIN(u.objectUpks) as min, 
		round(avg(u.objectUpks), 2) as average, 
		CASE
			WHEN sum(u.objectSquare) != 0 THEN round(sum(u.objectCost)/sum(u.objectSquare), 2)
			ELSE 0
		END as averageWeight, 
		MAX(u.objectUpks) as max
	FROM unit_data u 
	left outer join cadastralQuartalAttrValues cadastralQuartalGbu on u.ObjectId=cadastralQuartalGbu.objectId
	JOIN MARKET_REGION_DICTIONATY L1_R107 
		ON COALESCE(cadastralQuartalGbu.attributeValue, u.CadastralQuartal)=L1_R107.CADASTRAL_QUARTAL
	GROUP BY (districtCode, propertyTypeCode) 
	ORDER BY districtCode, propertyTypeCode
)
SELECT 
	MIN(districtCode) AS districtCode, 
	MIN(propertyTypeCode) AS propertyTypeCode, 
	(SELECT SUM(objectsCount) FROM cte2 AS amount WHERE cte2.districtCode = districtCode GROUP BY districtCode) AS objectsCount, 
	MIN(min) AS min, 
	MIN(average) AS average, 
	MIN(averageWeight) AS averageWeight, 
	MIN(max) AS max 
FROM cte2 
GROUP BY (districtCode, propertyTypeCode) 
ORDER BY districtCode, propertyTypeCode;