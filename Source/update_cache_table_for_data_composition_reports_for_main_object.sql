CREATE OR REPLACE FUNCTION public.update_cache_table_for_data_composition_reports_for_main_object (
)
RETURNS trigger AS
$body$
BEGIN
    INSERT INTO data_composition_by_characteristics_by_tables (object_id, cadastral_number, object_type_code) 
    VALUES (NEW.id, NEW.cadastral_number, NEW.object_type_code);    
	
    RETURN NULL;
END;
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100;

ALTER FUNCTION public.update_cache_table_for_data_composition_reports_for_main_object ()
  OWNER TO cipjs_kad_ozenka;