CREATE OR REPLACE FUNCTION public.core_updstru_checkexistview(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
	    sName ALIAS FOR $1;
        nCnt numeric;
    begin
	    SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.views as v
        --TABLE information_schema.views
        WHERE lower(v.table_name) = lower(sName) AND v.table_schema NOT IN ('pg_catalog', 'information_schema');

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

    end $function$
