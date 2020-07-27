CREATE OR REPLACE FUNCTION public.core_updstru_checkexistfunction(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sFuncName ALIAS FOR $1;
        nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_proc as p
        --TABLE pg_catalog.pg_proc
        WHERE p.proname = lower(sFuncName);

        if nCnt = 0 then
        	return false;
        else
        	return true;
        end if;

    end $function$
