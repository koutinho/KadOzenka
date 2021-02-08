CREATE OR REPLACE FUNCTION public.gbu_get_allpri_attribute_value (
  objectid bigint,
  attributeid bigint
)
RETURNS varchar AS
$body$
    declare
         _attributeValueString character varying;

	begin
                
		select attributevalue from gbu_get_allpri_attribute_values(ARRAY[objectid], attributeid) into _attributeValueString;
    	return _attributeValueString;

	END
$body$
LANGUAGE 'plpgsql'
VOLATILE
CALLED ON NULL INPUT
SECURITY INVOKER
PARALLEL UNSAFE
COST 100;

ALTER FUNCTION public.gbu_get_allpri_attribute_value (objectid bigint, attributeid bigint)
  OWNER TO cipjs_gko_ppr;