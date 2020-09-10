with object_ids as (
	select u.object_id from ko_unit u where u.task_id IN (38676792)
),
--ROSREESTR ATTRIBUTES
ñommissioningYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
buildYearAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
undergroundFloorsNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
floorsNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
wallMaterialAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
locationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
addressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
parentCadastralNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
placementPurposeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
objectNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
floorAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),

--INPUT PARAMETERS
segmentAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
usageTypeNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
usageTypeCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
usageTypeCodeSourceAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
subGroupUsageTypeCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),
functionalSubGroupNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 1)
),

--TOUR ATTRIBUTES
objectTypeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 603)
),
cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 548)
),
subGroupNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), 589)
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
    undergroundFloorsNumberAttr.attributeValue as UndergroundFloorsNumber,
    floorsNumberAttr.attributeValue as FloorsNumber,
    wallMaterialAttr.attributeValue as WallMaterial,
    locationAttr.attributeValue as Location,
    addressAttr.attributeValue as Address,
    parentCadastralNumberAttr.attributeValue as ParentCadastralNumber,
    placementPurposeAttr.attributeValue as PlacementPurpose,
    objectNameAttr.attributeValue as ObjectName,
    floorAttr.attributeValue as Floor,
    segmentAttr.attributeValue as Segment,
    usageTypeNameAttr.attributeValue as UsageTypeName,
    usageTypeCodeAttr.attributeValue as UsageTypeCode,
    usageTypeCodeSourceAttr.attributeValue as UsageTypeCodeSource,
    subGroupUsageTypeCodeAttr.attributeValue as SubGroupUsageTypeCode,
    functionalSubGroupNameAttr.attributeValue as FunctionalSubGroupName,
    objectTypeAttr.attributeValue as ObjectType,
    cadastralQuartalAttr.attributeValue as CadastralQuartal,
    subGroupNumberAttr.attributeValue as SubGroupNumber,
    (select * from  gbu_get_allpri_attribute_value((select id from gbu_main_object where cadastral_number='77:22:0020229:2534' limit 1), 14))
    	 as ParentPurpose
		FROM KO_UNIT L1_R201
			LEFT JOIN ñommissioningYearAttrValues ñommissioningYearAttr ON L1_R201.object_id=ñommissioningYearAttr.objectId
            LEFT JOIN buildYearAttrValues buildYearAttr ON L1_R201.object_id=buildYearAttr.objectId
            LEFT JOIN undergroundFloorsNumberAttrValues undergroundFloorsNumberAttr ON L1_R201.object_id=undergroundFloorsNumberAttr.objectId
            LEFT JOIN floorsNumberAttrValues floorsNumberAttr ON L1_R201.object_id=floorsNumberAttr.objectId
            LEFT JOIN wallMaterialAttrValues wallMaterialAttr ON L1_R201.object_id=wallMaterialAttr.objectId
            LEFT JOIN locationAttrValues locationAttr ON L1_R201.object_id=locationAttr.objectId
            LEFT JOIN addressAttrValues addressAttr ON L1_R201.object_id=addressAttr.objectId
            LEFT JOIN parentCadastralNumberAttrValues parentCadastralNumberAttr ON L1_R201.object_id=parentCadastralNumberAttr.objectId
            LEFT JOIN placementPurposeAttrValues placementPurposeAttr ON L1_R201.object_id=placementPurposeAttr.objectId
            LEFT JOIN objectNameAttrValues objectNameAttr ON L1_R201.object_id=objectNameAttr.objectId
            LEFT JOIN floorAttrValues floorAttr ON L1_R201.object_id=floorAttr.objectId
            LEFT JOIN segmentAttrValues segmentAttr ON L1_R201.object_id=segmentAttr.objectId
            LEFT JOIN usageTypeNameAttrValues usageTypeNameAttr ON L1_R201.object_id=usageTypeNameAttr.objectId
            LEFT JOIN usageTypeCodeAttrValues usageTypeCodeAttr ON L1_R201.object_id=usageTypeCodeAttr.objectId
            LEFT JOIN usageTypeCodeSourceAttrValues usageTypeCodeSourceAttr ON L1_R201.object_id=usageTypeCodeSourceAttr.objectId
            LEFT JOIN subGroupUsageTypeCodeAttrValues subGroupUsageTypeCodeAttr ON L1_R201.object_id=subGroupUsageTypeCodeAttr.objectId
            LEFT JOIN functionalSubGroupNameAttrValues functionalSubGroupNameAttr ON L1_R201.object_id=functionalSubGroupNameAttr.objectId
            LEFT JOIN objectTypeAttrValues objectTypeAttr ON L1_R201.object_id=objectTypeAttr.objectId
            LEFT JOIN cadastralQuartalAttrValues cadastralQuartalAttr ON L1_R201.object_id=cadastralQuartalAttr.objectId
            LEFT JOIN subGroupNumberAttrValues subGroupNumberAttr ON L1_R201.object_id=subGroupNumberAttr.objectId
		WHERE L1_R201.TASK_ID IN (38676792)
        AND
        (L1_R201.PROPERTY_TYPE_CODE = 11 and L1_R201.OBJECT_ID is not null)
        and L1_R201=12435691
		ORDER BY L1_R201.CADASTRAL_NUMBER
)
        
select DISTINCT ON (CadastralNumber) 
  CadastralNumber, 
  Square, 
  Upks, 
  CadastralCost, 
  ÑommissioningYear,
  BuildYear,
  UndergroundFloorsNumber,
  FloorsNumber,
  WallMaterial,
  Location,
  Address,
  ParentCadastralNumber,
  PlacementPurpose,
  ObjectName,
  Floor,
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
