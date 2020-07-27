CREATE OR REPLACE FUNCTION public.core_updstru_deletefkrefconstraints(character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
  declare
		sTable ALIAS FOR $1;
        qRow RECORD;
  begin
    -- Удаление внешних ссылок
    FOR qRow IN (SELECT tc.table_name as table_name_pk, tc.constraint_name as constraint_name_pk
                      , rc.constraint_name as constraint_name_fk, trc.table_name as table_name_fk
                 FROM information_schema.table_constraints as tc
                      INNER JOIN information_schema.referential_constraints as rc ON rc.unique_constraint_name = tc.constraint_name
                      INNER JOIN information_schema.table_constraints as trc ON trc.constraint_name = rc.constraint_name
                 WHERE tc.constraint_type = 'PRIMARY KEY' AND lower(tc.table_name) = lower(sTable)) LOOP
      execute immediate 'alter table ' || qRow.table_name_fk || ' drop constraint ' || qRow.constraint_name_fk;
    end loop;

  end; $function$
