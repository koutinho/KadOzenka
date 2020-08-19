WITH cte2 AS (
	WITH cte1 AS (
		SELECT
			L1_R201.ID AS id, 
			substring(L1_R107.CADASTRAL_QUARTAL from 1 for 5) AS  cadastralQuartal, 
			L1_R107.REGION_CODE AS regionCode, 
			L1_R107.DISTRICT_CODE AS districtCode, 
			L1_R201.PROPERTY_TYPE_CODE AS propertyTypeCode, 
			L1_R201.UPKS AS objectUpks, 
			L1_R201.CADASTRAL_COST AS objectCost, 
			L1_R201.SQUARE AS objectSquare 
		FROM KO_UNIT L1_R201 JOIN MARKET_REGION_DICTIONATY L1_R107 ON (L1_R201.CADASTRAL_BLOCK = L1_R107.CADASTRAL_QUARTAL) 
		WHERE (L1_R201.TASK_ID IN ({0}))
	) 
	SELECT 
		cadastralQuartal AS cadastralDistrict, 
		propertyTypeCode as propertyTypeCode,  
		count(id) as objectsCount, 
		MIN(objectUpks) as min, 
		round(avg(objectUpks), 2) as average, 
		CASE
			WHEN sum(objectSquare) != 0 THEN round(sum(objectCost)/sum(objectSquare), 2)
			ELSE 0
		END as averageWeight, 
		MAX(objectUpks) as max
	FROM cte1 
	GROUP BY (cadastralDistrict, propertyTypeCode) 
	ORDER BY cadastralDistrict, propertyTypeCode
)
SELECT 
	MIN(cadastralDistrict) AS cadastralDistrict, 
	MIN(propertyTypeCode) AS propertyTypeCode, 
	(SELECT SUM(objectsCount) FROM cte2 AS amount WHERE cte2.cadastralDistrict = cadastralDistrict GROUP BY cadastralDistrict) AS objectsCount, 
	MIN(min) AS min, 
	MIN(average) AS average, 
	MIN(averageWeight) AS averageWeight, 
	MIN(max) AS max 
FROM cte2 
GROUP BY (cadastralDistrict, propertyTypeCode) 
ORDER BY cadastralDistrict, propertyTypeCode;