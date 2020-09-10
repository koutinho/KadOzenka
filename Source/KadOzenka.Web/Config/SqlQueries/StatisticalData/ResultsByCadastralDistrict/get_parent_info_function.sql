CREATE OR REPLACE FUNCTION public.get_purpose (
  "parentCadastralNumber" varchar,
  "buildingPurposeAttributeId" bigint,
  "constructionPurposeAttributeId" bigint
)
RETURNS varchar AS
$body$
    declare	
    	_objectId BIGINT;
        _objectTypeCode BIGINT;
		_resultAttributeId BIGINT;
		_attributeValueString character varying;
        
        
    begin
	
    	--ищем парент-объект 
    	select 
        	obj.id, obj.object_type_code into _objectId, _objectTypeCode
        from gbu_main_object obj where cadastral_number="parentCadastralNumber" limit 1;
        
        --если тип объекта - "Здание"
        if(_objectTypeCode = 5) then
        	_resultAttributeId := "buildingPurposeAttributeId";
        end if;
        
        --если тип объекта - "Сооружение"
        if(_objectTypeCode = 7) then
        	_resultAttributeId := "constructionPurposeAttributeId";
        end if;
        
        select * from gbu_get_allpri_attribute_value(_objectId, _resultAttributeId) into _attributeValueString;
        
		return _attributeValueString;
        
        /*
        	select object_id from ko_unit where task_id=15534573 and property_type_code=7 and object_id is not null limit 1
select count(*) from ko_unit where task_id=1 and property_type_code=11 and object_id is not null
select task_id, count(*) from ko_unit where property_type_code=11 and object_id is not null group by task_id
select * from ko_task where id=38676792
select * from ko_tour where id=38670860
select * from KO_TOUR_ATTRIBUTE_SETTINGS where tour_id=2018

select * from ko_unit where object_id=11188991


select * from core_register_attribute where name like 'Кадастровый номер здания или сооружения, в котором расположено помещение'
select * from gbu_source2_a_22
--10430593

select * from core_register_attribute where id=589
select * from gbu_source23_a_txt limit 1
--17581251


select * from  gbu_get_allpri_attribute_value((select id from gbu_main_object where cadastral_number='77:22:0020229:2534' limit 1), 589)
select * from  gbu_get_allpri_attribute_value((select id from gbu_main_object where cadastral_number='77:22:0020229:2534' limit 1), 14)
        */
		
	END
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100;

ALTER FUNCTION public.get_purpose ("parentCadastralNumber" varchar, "buildingPurposeAttributeId" bigint, "constructionPurposeAttributeId" bigint)
  OWNER TO cipjs_kad_ozenka;