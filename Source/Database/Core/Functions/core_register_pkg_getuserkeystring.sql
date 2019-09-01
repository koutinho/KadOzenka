CREATE OR REPLACE FUNCTION public.core_register_pkg_getuserkeystring(p_register_id bigint, p_object_id bigint, p_date timestamp without time zone)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
  return to_char(p_object_id, '99999999999');
END;
$function$
