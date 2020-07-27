CREATE OR REPLACE FUNCTION public.core_updstru_getcolumntype(character varying, character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare
	    tableName ALIAS FOR $1;
        columnName ALIAS FOR $2;
	    columnType varchar(100);
	begin
      	SELECT c.data_type
        INTO columnType
        FROM information_schema.columns c
        --TABLE information_schema.columns
        WHERE c.table_name = lower(tableName) AND c.column_name = lower(columnName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');

        return columnType;
	end $function$
