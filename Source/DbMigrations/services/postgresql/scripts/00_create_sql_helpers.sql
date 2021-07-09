/*core_updstru_checkexisttable 31*/
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
    END $function$;
	
	
	
/*core_updstru_checkexistcolumn 28*/
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
        WHERE lower(c.table_name) = lower(sTabName) AND lower(c.column_name) = lower(sColName) AND c.table_schema = 'public';

      	if nCnt = 0 then
        	return false;
	    else
    	    return true;
        end if;

    END
$function$;

/*core_updstru_checkexistconstraint 29*/
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

    END $function$;

/*core_updstru_checkexistfunction 30*/
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

    END $function$;
    

/*core_updstru_checkexisttrigger 32*/
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

    END $function$;

/*core_updstru_checkexisttype 33*/
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
    END $function$;

/*core_updstru_checkexistview 34*/
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
        WHERE lower(v.table_name) = lower(sName) AND v.table_schema = 'public';

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

    END
$function$;


/*core_updstru_checkexistindex 11*/
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
        WHERE lower(i.indexname) = lower(sIndexName) AND i.schemaname = 'public';

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

  END
$function$;


CREATE EXTENSION pg_trgm;
