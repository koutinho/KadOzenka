with object_ids as (
	select unit.object_id from ko_unit unit {0}
),
--Rosreestr
typeOfUseByDocumentsAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {1})
),
typeOfUseByClassifierAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {2})
),
formationDateAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {3})
),
parcelCategoryAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {4})
),
locationAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {5})
),
addressAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {6})
),
parcelNameAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {7})
),
--Tour
objectTypeAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {8})
),
cadastralQuartalAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {9})
),
subGroupNumberAttrValues as (
	select * from  gbu_get_allpri_attribute_values( ARRAY(select object_id from object_ids), {10})
)


SELECT
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
    typeOfUseByDocumentsAttr.attributeValue as TypeOfUseByDocuments,
    typeOfUseByClassifierAttr.attributeValue as TypeOfUseByClassifier,
    formationDateAttr.attributeValue as FormationDate,
    parcelCategoryAttr.attributeValue as ParcelCategory,
    locationAttr.attributeValue as Location,
    addressAttr.attributeValue as Address,
    parcelNameAttr.attributeValue as ParcelName,
    --Tour
    objectTypeAttr.attributeValue as ObjectType,
    cadastralQuartalAttr.attributeValue as CadastralQuartal,
    subGroupNumberAttr.attributeValue as SubGroupNumber
FROM KO_UNIT unit
 	LEFT JOIN KO_COST_ROSREESTR costRosreesrt ON (unit.ID = costRosreesrt.ID_OBJECT)
     --Rosreestr
    LEFT JOIN typeOfUseByDocumentsAttrValues typeOfUseByDocumentsAttr ON unit.object_id=typeOfUseByDocumentsAttr.objectId
    LEFT JOIN typeOfUseByClassifierAttrValues typeOfUseByClassifierAttr ON unit.object_id=typeOfUseByClassifierAttr.objectId
    LEFT JOIN formationDateAttrValues formationDateAttr ON unit.object_id=formationDateAttr.objectId
    LEFT JOIN parcelCategoryAttrValues parcelCategoryAttr ON unit.object_id=parcelCategoryAttr.objectId
    LEFT JOIN locationAttrValues locationAttr ON unit.object_id=locationAttr.objectId
    LEFT JOIN addressAttrValues addressAttr ON unit.object_id=addressAttr.objectId
    LEFT JOIN parcelNameAttrValues parcelNameAttr ON unit.object_id=parcelNameAttr.objectId
    --Tour
    LEFT JOIN objectTypeAttrValues objectTypeAttr ON unit.object_id=objectTypeAttr.objectId
    LEFT JOIN cadastralQuartalAttrValues cadastralQuartalAttr ON unit.object_id=cadastralQuartalAttr.objectId
    LEFT JOIN subGroupNumberAttrValues subGroupNumberAttr ON unit.object_id=subGroupNumberAttr.objectId
{0}

