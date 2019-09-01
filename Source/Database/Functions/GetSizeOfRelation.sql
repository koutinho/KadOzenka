create or replace function GetSizeOfRelation(varchar) returns bigint AS $$
declare
	relName ALIAS FOR $1;
    result bigint;
begin
	SELECT pg_total_relation_size(relName) INTO result;
    return result;
exception
	WHEN others THEN
		return 0;
end; $$ LANGUAGE plpgsql

--SELECT GetSizeOfRelation('fias_house_aoguid_houseguid_idx')