with object_ids as (
	select u.object_id from ko_unit u where u.task_id IN ({0})
),
--ROSREESTR ATTRIBUTES
parcelNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {1})
),
locationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {2})
),
addressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {3})
),
formationDateAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {4})
),
parcelCategoryAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {5})
),
typeOfUseByDocumentsAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {6})
),
typeOfUseByClassifierAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {7})
),
--INPUT PARAMETERS
infoAboutExistenceOfOtherObjectsAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {8})
),
infoSourceAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {9})
),
segmentAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {10})
),
usageTypeCodeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {11})
),
usageTypeNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {12})
),
usageTypeCodeSourceAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {13})
),
--TOUR ATTRIBUTES
objectTypeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {14})
),
cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {15})
),
subGroupNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {16})
),

initial_data as (
SELECT distinct
	--unit.ID,
	--unit.OBJECT_ID,
    unit.CADASTRAL_NUMBER as CadastralNumber,
    unit.SQUARE as Square,
	unit.UPKS as Upks,
	unit.CADASTRAL_COST as CadastralCost,
	parcelNameAttr.attributeValue as ParcelName,
    locationAttr.attributeValue as Location,
    addressAttr.attributeValue as Address,
    formationDateAttr.attributeValue as FormationDate,
    parcelCategoryAttr.attributeValue as ParcelCategory,
    typeOfUseByDocumentsAttr.attributeValue as TypeOfUseByDocuments,
    typeOfUseByClassifierAttr.attributeValue as TypeOfUseByClassifier,
    infoAboutExistenceOfOtherObjectsAttr.attributeValue as InfoAboutExistenceOfOtherObjects,
    infoSourceAttr.attributeValue as InfoSource,
    segmentAttr.attributeValue as Segment,
    usageTypeCodeAttr.attributeValue as UsageTypeCode,
    usageTypeNameAttr.attributeValue as UsageTypeName,
    usageTypeCodeSourceAttr.attributeValue as UsageTypeCodeSource,
    objectTypeAttr.attributeValue as ObjectType,
    cadastralQuartalAttr.attributeValue as CadastralQuartal,
    subGroupNumberAttr.attributeValue as SubGroupNumber,
    SUBSTRING(cadastralQuartalAttr.attributeValue, 0, 6) as CadastralDistrict			
		FROM KO_UNIT unit
			LEFT JOIN parcelNameAttrValues parcelNameAttr ON unit.object_id=parcelNameAttr.objectId
            LEFT JOIN locationAttrValues locationAttr ON unit.object_id=locationAttr.objectId
            LEFT JOIN addressAttrValues addressAttr ON unit.object_id=addressAttr.objectId
            LEFT JOIN formationDateAttrValues formationDateAttr ON unit.object_id=formationDateAttr.objectId
            LEFT JOIN parcelCategoryAttrValues parcelCategoryAttr ON unit.object_id=parcelCategoryAttr.objectId
            LEFT JOIN typeOfUseByDocumentsAttrValues typeOfUseByDocumentsAttr ON unit.object_id=typeOfUseByDocumentsAttr.objectId
            LEFT JOIN typeOfUseByClassifierAttrValues typeOfUseByClassifierAttr ON unit.object_id=typeOfUseByClassifierAttr.objectId
            LEFT JOIN infoAboutExistenceOfOtherObjectsAttrValues infoAboutExistenceOfOtherObjectsAttr ON unit.object_id=infoAboutExistenceOfOtherObjectsAttr.objectId
            LEFT JOIN infoSourceAttrValues infoSourceAttr ON unit.object_id=infoSourceAttr.objectId
            LEFT JOIN segmentAttrValues segmentAttr ON unit.object_id=segmentAttr.objectId
            LEFT JOIN usageTypeCodeAttrValues usageTypeCodeAttr ON unit.object_id=usageTypeCodeAttr.objectId
            LEFT JOIN usageTypeNameAttrValues usageTypeNameAttr ON unit.object_id=usageTypeNameAttr.objectId
            LEFT JOIN usageTypeCodeSourceAttrValues usageTypeCodeSourceAttr ON unit.object_id=usageTypeCodeSourceAttr.objectId
            LEFT JOIN objectTypeAttrValues objectTypeAttr ON unit.object_id=objectTypeAttr.objectId
            LEFT JOIN cadastralQuartalAttrValues cadastralQuartalAttr ON unit.object_id=cadastralQuartalAttr.objectId
            LEFT JOIN subGroupNumberAttrValues subGroupNumberAttr ON unit.object_id=subGroupNumberAttr.objectId
		WHERE unit.TASK_ID IN ({0})
        AND
        (unit.PROPERTY_TYPE_CODE = 4 and unit.OBJECT_ID is not null)
		ORDER BY unit.CADASTRAL_NUMBER
)
        
select DISTINCT ON (CadastralNumber) 
  CadastralNumber, 
  Square, 
  Upks, 
  CadastralCost, 
  ParcelName,
  Location,
  Address,
  FormationDate,
  ParcelCategory,
  TypeOfUseByDocuments,
  TypeOfUseByClassifier,
  InfoAboutExistenceOfOtherObjects,
  InfoSource,
  Segment,
  UsageTypeCode,
  UsageTypeName,
  UsageTypeCodeSource,
  ObjectType,
  CadastralQuartal,
  SubGroupNumber,
  CadastralDistrict
from initial_data
