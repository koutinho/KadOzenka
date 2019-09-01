CREATE OR REPLACE FUNCTION public.core_updstru_checkexistcolumn(character varying, character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sTabName ALIAS FOR $1;
		sColName ALIAS FOR $2;
		nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.columns c
        --TABLE information_schema.columns
        WHERE lower(c.table_name) = lower(sTabName) AND lower(c.column_name) = lower(sColName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');

      	if nCnt = 0 then
        	return false;
	    else
    	    return true;
        end if;

    end $function$
