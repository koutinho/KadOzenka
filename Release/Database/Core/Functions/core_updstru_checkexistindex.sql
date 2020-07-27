CREATE OR REPLACE FUNCTION public.core_updstru_checkexistindex(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
  declare
		sIndexName ALIAS FOR $1;
        nCnt numeric;
  begin
		SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_indexes as i
        --TABLE pg_catalog.pg_indexes
        WHERE lower(i.indexname) = lower(sIndexName) AND i.schemaname NOT IN('pg_catalog', 'information_schema');

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

  end $function$
