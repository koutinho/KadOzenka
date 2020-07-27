CREATE OR REPLACE FUNCTION public.core_updstru_isnullablecolumn(character varying, character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sTabName ALIAS FOR $1;
        sColName ALIAS FOR $2;
        sNullable varchar(3);
    begin
        begin
			SELECT c.is_nullable
			INTO sNullable
            FROM information_schema.columns c
            --TABLE information_schema.columns
        	WHERE lower(c.table_name) = lower(sTabName) AND lower(c.column_name) = lower(sColName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');
        exception
          when NO_DATA_FOUND then
            return false;
        end;

        if sNullable = 'Yes' then
			return true;
        else
          	return false;
        end if;

    end $function$
