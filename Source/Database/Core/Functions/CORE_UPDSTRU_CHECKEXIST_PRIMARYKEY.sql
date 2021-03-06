CREATE OR REPLACE FUNCTION CORE_UPDSTRU_CHECKEXIST_PRIMARYKEY(varchar) RETURNS boolean AS $$
DECLARE
	tablename ALIAS FOR $1;
    result int = 0;
BEGIN
	SELECT COUNT(*)
    INTO result
	FROM information_schema.table_constraints as tc
	WHERE lower(tc.table_name) = lower(tablename) AND tc.constraint_type = 'PRIMARY KEY' AND tc.table_schema = 'public';

    if(result > 0)then
    	return true;
    else
    	return false;
    end if;
END $$ LANGUAGE plpgsql;