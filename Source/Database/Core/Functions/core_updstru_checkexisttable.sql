CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttable(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
	declare
        sName ALIAS FOR $1;
        nCnt numeric;
    begin
        SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_tables t
        --TABLE pg_catalog.pg_tables
        WHERE t.tablename = lower(sName); -- AND t.tablespace NOT IN ('pg_catalog', 'information_schema');

        if (nCnt = 0) then
   	        return false;
		else
   	        return true;
        end if;
    end $function$
