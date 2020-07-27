CREATE OR REPLACE FUNCTION public.core_updstru_checkexisttrigger(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
	declare
    	sTriggerName ALIAS FOR $1;
        nCnt numeric;
    begin
		SELECT COUNT(*)
        INTO nCnt
        FROM information_schema.triggers as t
        --TABLE information_schema.triggers
        WHERE lower(t.trigger_name) = lower(sTriggerName) AND t.trigger_schema NOT IN ('pg_catalog', 'information_schema');

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

    end $function$
