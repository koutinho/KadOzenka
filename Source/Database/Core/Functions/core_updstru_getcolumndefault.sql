CREATE OR REPLACE FUNCTION public.core_updstru_getcolumndefault(character varying, character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
  declare
		tableName ALIAS FOR $1;
        columnName  ALIAS FOR $2;
		defaultValue varchar(200);
  begin
    SELECT c.column_default
    INTO defaultValue
    FROM information_schema.columns as c
    WHERE lower(c.table_name) = lower(tableName) AND lower(c.column_name) = lower(columnName);

    return defaultValue;
  end $function$
