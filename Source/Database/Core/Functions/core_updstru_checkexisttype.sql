CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttype(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
    	sName ALIAS FOR $1;
        nCnt numeric;
    begin
        SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_type t
        --TABLE pg_catalog.pg_type
        WHERE t.typname = lower(sName);

        if (nCnt = 0) then
   	        return false;
		else
   	        return true;
        end if;
    end $function$
