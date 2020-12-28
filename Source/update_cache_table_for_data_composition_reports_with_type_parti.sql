CREATE OR REPLACE FUNCTION public.update_cache_table_for_data_composition_reports_with_type_parti (
)
RETURNS trigger AS
$body$
DECLARE
	row RECORD;
    is_attribute_already_added bigint;

BEGIN
    SELECT * INTO row FROM data_composition_by_characteristics_by_tables WHERE object_id = NEW.object_id;
    
    IF NOT FOUND THEN
        INSERT INTO data_composition_by_characteristics_by_tables (object_id, attributes) VALUES (NEW.object_id, array[ NEW.attribute_id ]);
    ELSE
    	/*≈сли атрибут не был добавлен ранее, то добавл€ем его*/
        IF (array_position(row.attributes, NEW.attribute_id) is NULL) THEN
    		update data_composition_by_characteristics_by_tables cache_table set attributes = array_append(attributes, NEW.attribute_id) 
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

ALTER FUNCTION public.update_cache_table_for_data_composition_reports_with_type_parti ()
  OWNER TO cipjs_kad_ozenka;