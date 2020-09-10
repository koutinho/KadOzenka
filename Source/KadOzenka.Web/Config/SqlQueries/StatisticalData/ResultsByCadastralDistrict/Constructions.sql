with object_ids as (
	select u.object_id from ko_unit u where u.task_id IN ({0})
),
--ROSREESTR ATTRIBUTES
ñommissioningYearAttrValues as (
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
constructionPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {9})
),
objectNameAttrValues as (
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
	--L1_R201.ID,
	--L1_R201.OBJECT_ID,
    L1_R201.CADASTRAL_NUMBER as CadastralNumber,
    L1_R201.SQUARE as Square,
	L1_R201.UPKS as Upks,
	L1_R201.CADASTRAL_COST as CadastralCost,
	ñommissioningYearAttr.attributeValue as ÑommissioningYear,
    buildYearAttr.attributeValue as BuildYear,
    formationDateAttr.attributeValue as FormationDate,
    undergroundFloorsNumberAttr.attributeValue as UndergroundFloorsNumber,
    floorsNumberAttr.attributeValue as FloorsNumber,
    wallMaterialAttr.attributeValue as WallMaterial,
    locationAttr.attributeValue as Location,
    addressAttr.attributeValue as Address,
    constructionPurposeAttr.attributeValue as ConstructionPurpose,
    objectNameAttr.attributeValue as ObjectName,
    segmentAttr.attributeValue as Segment,
    usageTypeNameAttr.attributeValue as UsageTypeName,
    usageTypeCodeAttr.attributeValue as UsageTypeCode,
    usageTypeCodeSourceAttr.attributeValue as UsageTypeCodeSource,
    subGroupUsageTypeCodeAttr.attributeValue as SubGroupUsageTypeCode,
    functionalSubGroupNameAttr.attributeValue as FunctionalSubGroupName,
    objectTypeAttr.attributeValue as ObjectType,
    cadastralQuartalAttr.attributeValue as CadastralQuartal,
    subGroupNumberAttr.attributeValue as SubGroupNumber
		FROM KO_UNIT L1_R201
			LEFT JOIN ñommissioningYearAttrValues ñommissioningYearAttr ON L1_R201.object_id=ñommissioningYearAttr.objectId
            LEFT JOIN buildYearAttrValues buildYearAttr ON L1_R201.object_id=buildYearAttr.objectId
            LEFT JOIN formationDateAttrValues formationDateAttr ON L1_R201.object_id=formationDateAttr.objectId
            LEFT JOIN undergroundFloorsNumberAttrValues undergroundFloorsNumberAttr ON L1_R201.object_id=undergroundFloorsNumberAttr.objectId
            LEFT JOIN floorsNumberAttrValues floorsNumberAttr ON L1_R201.object_id=floorsNumberAttr.objectId
            LEFT JOIN wallMaterialAttrValues wallMaterialAttr ON L1_R201.object_id=wallMaterialAttr.objectId
            LEFT JOIN locationAttrValues locationAttr ON L1_R201.object_id=locationAttr.objectId
            LEFT JOIN addressAttrValues addressAttr ON L1_R201.object_id=addressAttr.objectId
            LEFT JOIN constructionPurposeAttrValues constructionPurposeAttr ON L1_R201.object_id=constructionPurposeAttr.objectId
            LEFT JOIN objectNameAttrValues objectNameAttr ON L1_R201.object_id=objectNameAttr.objectId
            LEFT JOIN segmentAttrValues segmentAttr ON L1_R201.object_id=segmentAttr.objectId
            LEFT JOIN usageTypeNameAttrValues usageTypeNameAttr ON L1_R201.object_id=usageTypeNameAttr.objectId
            LEFT JOIN usageTypeCodeAttrValues usageTypeCodeAttr ON L1_R201.object_id=usageTypeCodeAttr.objectId
            LEFT JOIN usageTypeCodeSourceAttrValues usageTypeCodeSourceAttr ON L1_R201.object_id=usageTypeCodeSourceAttr.objectId
            LEFT JOIN subGroupUsageTypeCodeAttrValues subGroupUsageTypeCodeAttr ON L1_R201.object_id=subGroupUsageTypeCodeAttr.objectId
            LEFT JOIN functionalSubGroupNameAttrValues functionalSubGroupNameAttr ON L1_R201.object_id=functionalSubGroupNameAttr.objectId
            LEFT JOIN objectTypeAttrValues objectTypeAttr ON L1_R201.object_id=objectTypeAttr.objectId
            LEFT JOIN cadastralQuartalAttrValues cadastralQuartalAttr ON L1_R201.object_id=cadastralQuartalAttr.objectId
            LEFT JOIN subGroupNumberAttrValues subGroupNumberAttr ON L1_R201.object_id=subGroupNumberAttr.objectId
		WHERE L1_R201.TASK_ID IN ({0})
        AND
        (L1_R201.PROPERTY_TYPE_CODE = 7 and L1_R201.OBJECT_ID is not null)
		ORDER BY L1_R201.CADASTRAL_NUMBER
)
        
select DISTINCT ON (CadastralNumber) 
  CadastralNumber, 
  Square, 
  Upks, 
  CadastralCost, 
  ÑommissioningYear,
  BuildYear,
  FormationDate,
  UndergroundFloorsNumber,
  FloorsNumber,
  WallMaterial,
  Location,
  Address,
  ConstructionPurpose,
  ObjectName,
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
