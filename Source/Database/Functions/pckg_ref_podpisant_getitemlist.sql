CREATE OR REPLACE FUNCTION public.pckg_ref_podpisant_getitemlist(_s_filter character varying, OUT o_lst refcursor)
 RETURNS refcursor
 LANGUAGE plpgsql
 STRICT
AS $function$
DECLARE
   _sqlParam varchar;
   ref_cursor refcursor; 
   l_sql varchar;
BEGIN
if _s_filter is not null then 
_sqlParam = ' and ' || _s_filter ;
end if;
  l_sql := 'SELECT l."ID", l."POST", l."CODE", l."NAME", l."IS_DELETED", l."TEXT", l."NAME" ||   '' - '' || l."POST", '''' as short_title, 0 is_archives FROM fm_podpisant l WHERE l."IS_DELETED" = 0' || _sqlParam;  
  OPEN o_lst FOR EXECUTE l_sql || ' ORDER BY l."NAME"';
END;
$function$
