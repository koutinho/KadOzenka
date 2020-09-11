CREATE OR REPLACE FUNCTION public.get_parent_info (
  "parentCadastralNumbers" varchar [],
  "buildingPurposeAttributeId" bigint,
  "constructionPurposeAttributeId" bigint,
  "groupAttributeId" bigint
)
RETURNS TABLE (
  purpose varchar,
  "group" varchar,
  "cadastralNumberOutPut" varchar
) AS
$body$
    declare	
    	_objectId BIGINT;
        _objectTypeCode BIGINT;
		_resultPurposeAttributeId BIGINT;
		_purpose character varying;
        _group character varying;
        _parentCadastralNumber character varying;
        
    begin
      FOREACH _parentCadastralNumber IN ARRAY "parentCadastralNumbers"
      LOOP
          --ищем парент-объект 
          select 
              obj.id, obj.object_type_code into _objectId, _objectTypeCode
          from gbu_main_object obj where cadastral_number = _parentCadastralNumber limit 1;
          
          --если тип объекта - "Здание"
          if(_objectTypeCode = 5) then
              _resultPurposeAttributeId := "buildingPurposeAttributeId";
          end if;
          --если тип объекта - "Сооружение"
          if(_objectTypeCode = 7) then
              _resultPurposeAttributeId := "constructionPurposeAttributeId";
          end if;
          
          select * from gbu_get_allpri_attribute_value(_objectId, _resultPurposeAttributeId) into _purpose;
          select * from gbu_get_allpri_attribute_value(_objectId, "groupAttributeId") into _group;
          
          RETURN QUERY SELECT _purpose, _group, _parentCadastralNumber;	
          
      END LOOP;
    	
	END
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100 ROWS 1000;

ALTER FUNCTION public.get_parent_info ("parentCadastralNumbers" varchar [], "buildingPurposeAttributeId" bigint, "constructionPurposeAttributeId" bigint, "groupAttributeId" bigint)
  OWNER TO cipjs_kad_ozenka;