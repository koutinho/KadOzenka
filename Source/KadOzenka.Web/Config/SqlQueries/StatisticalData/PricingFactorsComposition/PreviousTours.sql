with object_ids as (
	select u.object_id from ko_unit u where u.task_id IN ({0}) and u.group_id = {1} --and u.id=15280959
),

--ROSREESTR ATTRIBUTES
oksNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {2})
),
zuNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {3})
),
buildingPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {4})
),
placementPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {5})
),
constructionPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {6})
),
permittedUseAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {7})
),
addressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {8})
),
locationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {9})
),
parentCadastralNumberForOksAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {10})
),
buildYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {11})
),
commissioningYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {12})
),
floorsNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {13})
),
undergroundFloorsNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {14})
),
wallMaterialAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {15})
),


--TOURS ATTRIBUTES
tourAttrValues as (
	select tour_id, attribute_using_type_code, attribute_id from KO_TOUR_ATTRIBUTE_SETTINGS
),

initial_data as (
SELECT
    tour.year as TourYear,
    unit.cadastral_number as CadastralNumber,
    unit.square,
    unit.cadastral_cost as CadastralCost,
    --ROSREESTR ATTRIBUTES
    (case when (unit.PROPERTY_TYPE_CODE=5 or unit.PROPERTY_TYPE_CODE=6 or unit.PROPERTY_TYPE_CODE=7) then oksNameAttr.attributeValue
			when unit.PROPERTY_TYPE_CODE=4 then zuNameAttr.attributeValue
			else null end) as ResultName,	
    (case when unit.PROPERTY_TYPE_CODE=5 then buildingPurposeAttr.attributeValue
			when unit.PROPERTY_TYPE_CODE=6 then placementPurposeAttr.attributeValue
            when unit.PROPERTY_TYPE_CODE=7 then constructionPurposeAttr.attributeValue
			else null end) as ResultPurpose,		
    permittedUseAttr.attributeValue as PermittedUse,
    addressAttr.attributeValue as Address,
    locationAttr.attributeValue as Location,
    (case when (unit.PROPERTY_TYPE_CODE=5 or unit.PROPERTY_TYPE_CODE=6 or unit.PROPERTY_TYPE_CODE=7) then parentCadastralNumberForOksAttr.attributeValue
			when unit.PROPERTY_TYPE_CODE=4 then NULL
			else null end) as ParentCadastralNumberForOks,
    buildYearAttr.attributeValue as BuildYear,
    commissioningYearAttr.attributeValue as CommissioningYear,
    floorsNumberAttr.attributeValue as FloorsNumber,
    undergroundFloorsNumberAttr.attributeValue as UndergroundFloorsNumber,
    wallMaterialAttr.attributeValue as WallMaterial,
	--TOURS ATTRIBUTES
	(select * from gbu_get_allpri_attribute_value(unit.object_id, 
    	(select attribute_id from tourAttrValues tourAttr where unit.tour_id=tourAttr.tour_id and tourAttr.attribute_using_type_code = 3 limit 1))) as ObjectType,
    (select * from gbu_get_allpri_attribute_value(unit.object_id, 
    	(select attribute_id from tourAttrValues tourAttr where unit.tour_id=tourAttr.tour_id and tourAttr.attribute_using_type_code = 2 limit 1))) as CadastralQuartal,
    (select * from gbu_get_allpri_attribute_value(unit.object_id, 
    	(select attribute_id from tourAttrValues tourAttr where unit.tour_id=tourAttr.tour_id and tourAttr.attribute_using_type_code = 1 limit 1))) as SubGroupNumber
    -- MODEL FACTORS
    {16}
    
    FROM ko_unit unit 
    	LEFT JOIN ko_tour tour on unit.tour_id = tour.id
        LEFT JOIN oksNameAttrValues oksNameAttr ON unit.object_id=oksNameAttr.objectId
        LEFT JOIN zuNameAttrValues zuNameAttr ON unit.object_id=zuNameAttr.objectId
        LEFT JOIN buildingPurposeAttrValues buildingPurposeAttr ON unit.object_id=buildingPurposeAttr.objectId
        LEFT JOIN placementPurposeAttrValues placementPurposeAttr ON unit.object_id=placementPurposeAttr.objectId
        LEFT JOIN constructionPurposeAttrValues constructionPurposeAttr ON unit.object_id=constructionPurposeAttr.objectId
        LEFT JOIN permittedUseAttrValues permittedUseAttr ON unit.object_id=permittedUseAttr.objectId
        LEFT JOIN addressAttrValues addressAttr ON unit.object_id=addressAttr.objectId
        LEFT JOIN locationAttrValues locationAttr ON unit.object_id=locationAttr.objectId
        LEFT JOIN parentCadastralNumberForOksAttrValues parentCadastralNumberForOksAttr ON unit.object_id=parentCadastralNumberForOksAttr.objectId
        LEFT JOIN buildYearAttrValues buildYearAttr ON unit.object_id=buildYearAttr.objectId
        LEFT JOIN commissioningYearAttrValues commissioningYearAttr ON unit.object_id=commissioningYearAttr.objectId
        LEFT JOIN floorsNumberAttrValues floorsNumberAttr ON unit.object_id=floorsNumberAttr.objectId
        LEFT JOIN undergroundFloorsNumberAttrValues undergroundFloorsNumberAttr ON unit.object_id=undergroundFloorsNumberAttr.objectId
        LEFT JOIN wallMaterialAttrValues wallMaterialAttr ON unit.object_id=wallMaterialAttr.objectId
    	{17}
       
    WHERE unit.TASK_ID in ({0}) and unit.GROUP_ID = {1} and unit.object_id is not null
    --and unit.id=15280959
    order by unit.cadastral_number, tour.year
)
        
select * from initial_data   
                    
