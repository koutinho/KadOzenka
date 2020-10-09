with object_ids as (
	select u.object_id from ko_unit u where u.task_id IN ({0})
),
--Rosreestr
commissioningYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {1})
),
buildYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {2})
),
formationDateAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {3})
),
undergroundFloorsNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {4})
),
floorsNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {5})
),
wallMaterialAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {6})
),
locationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {7})
),
addressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {8})
),
buildingPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {9})
),
placementPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {10})
),
constructionPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {11})
),
objectNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {12})
),


--Tour
objectTypeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {13})
),
cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {14})
),
subGroupNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {15})
)


SELECT DISTINCT ON (unit.CADASTRAL_NUMBER)
	unit.CADASTRAL_NUMBER AS CadastralNumber,
	unit.SQUARE AS Square,
	costRosreesrt.COSTVALUE AS CostValue,
	costRosreesrt.DATEVALUATION AS DateValuation,
	costRosreesrt.DATEENTERING AS DateEntering,
	costRosreesrt.DATEAPPROVAL AS DateApproval,
	costRosreesrt.DOCNUMBER AS DocNumber,
	costRosreesrt.DOCDATE AS DocDate,
	costRosreesrt.DOCNAME AS DocName,
	costRosreesrt.APPLICATIONDATE AS ApplicationDate,
	costRosreesrt.REVISALSTATEMENTDATE AS RevisalStatementDate,
    --Rosreestr
    commissioningYearAttr.attributeValue as CommissioningYear,
    buildYearAttr.attributeValue as BuildYear,
    formationDateAttr.attributeValue as FormationDate,
    undergroundFloorsNumberAttr.attributeValue as UndergroundFloorsNumber,
    floorsNumberAttr.attributeValue as FloorsNumber,
    wallMaterialAttr.attributeValue as WallMaterial,
    locationAttr.attributeValue as Location,
    addressAttr.attributeValue as Address,
    (case when unit.PROPERTY_TYPE_CODE = 5 then buildingPurposeAttr.attributeValue
			when unit.PROPERTY_TYPE_CODE = 6 then placementPurposeAttr.attributeValue
            when unit.PROPERTY_TYPE_CODE = 7 then constructionPurposeAttr.attributeValue
			else null end) as Purpose,	 
    objectNameAttr.attributeValue as ObjectName,
    --Tour
    objectTypeAttr.attributeValue as ObjectType,
    cadastralQuartalAttr.attributeValue as CadastralQuartal,
    subGroupNumberAttr.attributeValue as SubGroupNumber
FROM KO_UNIT unit
 	LEFT JOIN KO_COST_ROSREESTR costRosreesrt ON (unit.ID = costRosreesrt.ID_OBJECT)
     --Rosreestr
    LEFT OUTER JOIN commissioningYearAttrValues commissioningYearAttr ON unit.object_id=commissioningYearAttr.objectId
    LEFT OUTER JOIN buildYearAttrValues buildYearAttr ON unit.object_id=buildYearAttr.objectId
    LEFT OUTER JOIN formationDateAttrValues formationDateAttr ON unit.object_id=formationDateAttr.objectId
    LEFT OUTER JOIN undergroundFloorsNumberAttrValues undergroundFloorsNumberAttr ON unit.object_id=undergroundFloorsNumberAttr.objectId
    LEFT OUTER JOIN floorsNumberAttrValues floorsNumberAttr ON unit.object_id=floorsNumberAttr.objectId
    LEFT OUTER JOIN wallMaterialAttrValues wallMaterialAttr ON unit.object_id=wallMaterialAttr.objectId
    LEFT OUTER JOIN locationAttrValues locationAttr ON unit.object_id=locationAttr.objectId
    LEFT OUTER JOIN addressAttrValues addressAttr ON unit.object_id=addressAttr.objectId
    LEFT OUTER JOIN buildingPurposeAttrValues buildingPurposeAttr ON unit.object_id=buildingPurposeAttr.objectId
    LEFT OUTER JOIN placementPurposeAttrValues placementPurposeAttr ON unit.object_id=placementPurposeAttr.objectId
    LEFT OUTER JOIN constructionPurposeAttrValues constructionPurposeAttr ON unit.object_id=constructionPurposeAttr.objectId
    LEFT OUTER JOIN objectNameAttrValues objectNameAttr ON unit.object_id=objectNameAttr.objectId
    --Tour
    LEFT OUTER JOIN objectTypeAttrValues objectTypeAttr ON unit.object_id=objectTypeAttr.objectId
    LEFT OUTER JOIN cadastralQuartalAttrValues cadastralQuartalAttr ON unit.object_id=cadastralQuartalAttr.objectId
    LEFT OUTER JOIN subGroupNumberAttrValues subGroupNumberAttr ON unit.object_id=subGroupNumberAttr.objectId
WHERE 
	(unit.TASK_ID IN ({0}) AND unit.PROPERTY_TYPE_CODE <> 4 AND unit.PROPERTY_TYPE_CODE<>2190 AND unit.OBJECT_ID IS NOT NULL)
    order by unit.CADASTRAL_NUMBER