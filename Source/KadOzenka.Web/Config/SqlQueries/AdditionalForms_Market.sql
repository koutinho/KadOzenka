with market_data as (
select * from (
	SELECT
		COALESCE(L1_R100.MARKET_ID, L1_R100.ID) AS UniqueNumber,
		L1_R100.CADASTRAL_NUMBER AS Kn,
		L1_R100.KO_GROUP AS SegmentGroup,
		substring(L1_R100.CADASTRAL_QUARTAL from 1 for 2) AS SubjectCode,
		'45000000' as OKTMO,
		L1_R100.ADDRESS AS AddressReferencePoint,
		L1_R100.METRO AS Metro,
		L1_R100.MARKET AS Market,
		L1_R100.URL AS Link,
		L1_R100.PHONE_NUMBER AS Phone,
		COALESCE(L1_R100.LAST_DATE_UPDATE, L1_R100.PARSER_TIME) as Date,
		--L1_R100.DESCRIPTION as AdText,
		L1_R100.PROPERTY_TYPETS_CIPJS as TypeOfProperty,
		L1_R100.ROOMS_COUNT as RoomCount,
		L1_R100.DEAL_TYPE as DealSuggestion,
		case when L1_R100.PROPERTY_TYPETS_CIPJS='Земельные участки'
			then L1_R100.AREA_LAND*100
			else L1_R100.AREA
		end as Square,
		L1_R100.PRICE as Price,
		case when L1_R100.DEAL_TYPE='Сделка купли-продажи' or L1_R100.DEAL_TYPE='Предложение-продажа'
			then case when L1_R100.PROPERTY_TYPETS_CIPJS='Земельные участки'
					then L1_R100.PRICE / nullif(L1_R100.AREA_LAND*100, 0)
					else L1_R100.PRICE / nullif(L1_R100.AREA, 0) 
				end
			else null::numeric
		end as Upks,
		case when L1_R100.DEAL_TYPE='Сделка-аренда' or L1_R100.DEAL_TYPE='Предложение-аренда'
			then case when L1_R100.PROPERTY_TYPETS_CIPJS='Земельные участки'
					then 12 * L1_R100.PRICE / nullif(L1_R100.AREA_LAND*100, 0)
					else 12 * L1_R100.PRICE / nullif(L1_R100.AREA, 0) 
				end
			else null::numeric
		end as AnnualRateOfRent,
		L1_R100.OWNERSHIP_TYPE as TypeOfRight,
		L1_R200.ID AS GbuObjectId,
		row_number() over (PARTITION BY L1_R100.ID) as num
	FROM MARKET_CORE_OBJECT L1_R100
	 left JOIN GBU_MAIN_OBJECT L1_R200 ON L1_R100.CADASTRAL_NUMBER = L1_R200.CADASTRAL_NUMBER
	WHERE (
		L1_R100.PROCESS_TYPE_CODE = 742
		AND
		(L1_R200.IS_ACTIVE <> 0 or L1_R200.IS_ACTIVE is null)
		AND (case 
				when {0} is not null 
			 		then ((L1_R100.LAST_DATE_UPDATE IS NOT NULL AND L1_R100.LAST_DATE_UPDATE >= {0}) 
											or (L1_R100.LAST_DATE_UPDATE IS NULL AND L1_R100.PARSER_TIME >= {0}))
				else true 
			 end)
		AND (case 
				when {1} is not null 
			 		then ((L1_R100.LAST_DATE_UPDATE IS NOT NULL AND L1_R100.LAST_DATE_UPDATE <= {1}) 
											or (L1_R100.LAST_DATE_UPDATE IS NULL AND L1_R100.PARSER_TIME <= {1}))
				else true 
			 end)
	)
)d where d.num=1
),

TypeOfUseCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from market_data), {2})
),

OksGroupAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from market_data), {3})
),

TypeOfUseAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select GbuObjectId from market_data), {4})
)

select distinct d.*,
	TypeOfUseCode.attributeValue as TypeOfUseCode,
	OksGroup.attributeValue as OksGroup,
	TypeOfUse.attributeValue as TypeOfUse
from market_data d
	left outer join TypeOfUseCodeAttrValues TypeOfUseCode on d.GbuObjectId=TypeOfUseCode.objectId
	left outer join OksGroupAttrValues OksGroup on d.GbuObjectId=OksGroup.objectId
	left outer join TypeOfUseAttrValues TypeOfUse on d.GbuObjectId=TypeOfUse.objectId
	limit {5} offset {6} * {5}
