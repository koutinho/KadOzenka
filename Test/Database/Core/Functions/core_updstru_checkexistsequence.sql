CREATE OR REPLACE FUNCTION public.core_updstru_checkexistsequence(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
          sName ALIAS FOR $1;
          nCnt numeric;
    begin
          SELECT COUNT(*)
          INTO nCnt
          FROM pg_catalog.pg_sequences as s
          --TABLE pg_catalog.pg_sequences
          WHERE s.sequencename = lower(sName) AND s.schemaname NOT IN ('pg_catalog', 'information_schema');

          if (nCnt = 0) then
              return false;
          else
              return true;
          end if;

    end $function$
