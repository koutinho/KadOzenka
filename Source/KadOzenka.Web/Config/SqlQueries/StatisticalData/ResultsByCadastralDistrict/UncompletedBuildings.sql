with object_ids as (
	select u.object_id from ko_unit u where u.task_id IN ({0})
),
--ROSREESTR ATTRIBUTES
buildYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {1})
),
formationDateAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {2})
),
undergroundFloorsNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {3})
),
floorsNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {4})
),
wallMaterialAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {5})
),
locationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {6})
),
addressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {7})
),
buildingPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {8})
),
objectNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {9})
),
readinessPercentageAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {10})
),

--INPUT PARAMETERS
segmentAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {11})
),
usageTypeNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {12})
),
usageTypeCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {13})
),
usageTypeCodeSourceAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {14})
),
subGroupUsageTypeCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {15})
),
functionalSubGroupNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {16})
),

--TOUR ATTRIBUTES
objectTypeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {17})
),
cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {18})
),
subGroupNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {19})
),


initial_data as (
SELECT distinct
    unit.CADASTRAL_NUMBER as CadastralNumber,
    unit.SQUARE as Square,
	unit.UPKS as Upks,
	unit.CADASTRAL_COST as CadastralCost,
    buildYearAttr.attributeValue as BuildYear,
    formationDateAttr.attributeValue as FormationDate,
    undergroundFloorsNumberAttr.attributeValue as UndergroundFloorsNumber,
    floorsNumberAttr.attributeValue as FloorsNumber,
    wallMaterialAttr.attributeValue as WallMaterial,
    locationAttr.attributeValue as Location,
    addressAttr.attributeValue as Address,
    buildingPurposeAttr.attributeValue as BuildingPurpose,
    objectNameAttr.attributeValue as ObjectName,
    readinessPercentageAttr.attributeValue as ReadinessPercentage,
    segmentAttr.attributeValue as Segment,
    usageTypeNameAttr.attributeValue as UsageTypeName,
    usageTypeCodeAttr.attributeValue as UsageTypeCode,
    usageTypeCodeSourceAttr.attributeValue as UsageTypeCodeSource,
    subGroupUsageTypeCodeAttr.attributeValue as SubGroupUsageTypeCode,
    functionalSubGroupNameAttr.attributeValue as FunctionalSubGroupName,
    objectTypeAttr.attributeValue as ObjectType,
    cadastralQuartalAttr.attributeValue as CadastralQuartal,
    subGroupNumberAttr.attributeValue as SubGroupNumber
		FROM KO_UNIT unit
            LEFT JOIN buildYearAttrValues buildYearAttr ON unit.object_id=buildYearAttr.objectId
            LEFT JOIN formationDateAttrValues formationDateAttr ON unit.object_id=formationDateAttr.objectId
            LEFT JOIN undergroundFloorsNumberAttrValues undergroundFloorsNumberAttr ON unit.object_id=undergroundFloorsNumberAttr.objectId
            LEFT JOIN floorsNumberAttrValues floorsNumberAttr ON unit.object_id=floorsNumberAttr.objectId
            LEFT JOIN wallMaterialAttrValues wallMaterialAttr ON unit.object_id=wallMaterialAttr.objectId
            LEFT JOIN locationAttrValues locationAttr ON unit.object_id=locationAttr.objectId
            LEFT JOIN addressAttrValues addressAttr ON unit.object_id=addressAttr.objectId
            LEFT JOIN buildingPurposeAttrValues buildingPurposeAttr ON unit.object_id=buildingPurposeAttr.objectId
            LEFT JOIN objectNameAttrValues objectNameAttr ON unit.object_id=objectNameAttr.objectId
            LEFT JOIN readinessPercentageAttrValues readinessPercentageAttr ON unit.object_id=readinessPercentageAttr.objectId
            LEFT JOIN segmentAttrValues segmentAttr ON unit.object_id=segmentAttr.objectId
            LEFT JOIN usageTypeNameAttrValues usageTypeNameAttr ON unit.object_id=usageTypeNameAttr.objectId
            LEFT JOIN usageTypeCodeAttrValues usageTypeCodeAttr ON unit.object_id=usageTypeCodeAttr.objectId
            LEFT JOIN usageTypeCodeSourceAttrValues usageTypeCodeSourceAttr ON unit.object_id=usageTypeCodeSourceAttr.objectId
            LEFT JOIN subGroupUsageTypeCodeAttrValues subGroupUsageTypeCodeAttr ON unit.object_id=subGroupUsageTypeCodeAttr.objectId
            LEFT JOIN functionalSubGroupNameAttrValues functionalSubGroupNameAttr ON unit.object_id=functionalSubGroupNameAttr.objectId
            LEFT JOIN objectTypeAttrValues objectTypeAttr ON unit.object_id=objectTypeAttr.objectId
            LEFT JOIN cadastralQuartalAttrValues cadastralQuartalAttr ON unit.object_id=cadastralQuartalAttr.objectId
            LEFT JOIN subGroupNumberAttrValues subGroupNumberAttr ON unit.object_id=subGroupNumberAttr.objectId
		WHERE unit.TASK_ID IN ({0})
        AND
        (unit.PROPERTY_TYPE_CODE = 8 and unit.OBJECT_ID is not null)
		ORDER BY unit.CADASTRAL_NUMBER
)
        
select DISTINCT ON (CadastralNumber) 
  CadastralNumber, 
  Square, 
  Upks, 
  CadastralCost, 
  BuildYear,
  FormationDate,
  UndergroundFloorsNumber,
  FloorsNumber,
  WallMaterial,
  Location,
  Address,
  BuildingPurpose,
  ObjectName,
  ReadinessPercentage,
  Segment,
  UsageTypeName,
  UsageTypeCode,
  UsageTypeCodeSource,
  SubGroupUsageTypeCode,
  FunctionalSubGroupName,
  ObjectType,
  CadastralQuartal,
  SubGroupNumber
from initial_data
