CREATE OR REPLACE FUNCTION public.update_cache_table_for_data_composition_reports_for_registers (
)
RETURNS trigger AS
$body$
DECLARE
	row RECORD;
    is_attribute_already_added bigint;
    attribute_id bigint;

BEGIN
	/* Для Росреестра передаем ИД атрибута в параметры, для остальных реестров - берем из вновь вставленной строки */
	IF (TG_ARGV[0] IS NULL) THEN
    	attribute_id := NEW.attribute_id;
    ELSE
    	attribute_id := TG_ARGV[0];
    END IF;

    SELECT * INTO row FROM data_composition_by_characteristics_by_tables WHERE object_id = NEW.object_id;
    
    IF NOT FOUND THEN
        INSERT INTO data_composition_by_characteristics_by_tables (object_id, attributes) VALUES (NEW.object_id, array[ attribute_id ]);
    ELSE
    	/*Если атрибут не был добавлен ранее, то добавляем его*/
        IF (array_position(row.attributes, attribute_id) is NULL) THEN
    		update data_composition_by_characteristics_by_tables cache_table set attributes = array_append(attributes, attribute_id) 
            	where cache_table.object_id = NEW.object_id;
        END IF;
    END IF;

    
	RETURN NULL;
END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100;

ALTER FUNCTION public.update_cache_table_for_data_composition_reports_for_registers ()
  OWNER TO cipjs_kad_ozenka;