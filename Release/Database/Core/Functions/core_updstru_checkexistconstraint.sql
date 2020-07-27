CREATE OR REPLACE FUNCTION public.core_updstru_checkexistconstraint(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
          sName ALIAS FOR $1;
          nCnt numeric;
    begin
          SELECT COUNT(*)
          INTO nCnt
          FROM information_schema.constraint_table_usage c
          --TABLE pg_catalog.pg_constraint --LEFT OUTER JOIN pg_catalog.pg_namespace AS n ON n.oid = c.relnamespace
          WHERE lower(c.constraint_name) = lower(sName);

          if nCnt = 0 then
              return false;
          else
              return true;
          end if;

    end $function$
