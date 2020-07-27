CREATE OR REPLACE FUNCTION public.core_srd_role_pkg_getfullfunctionname(p_function_id bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
declare
cur record;
vFullName varchar(4000) := '';
begin
for cur in (
	with recursive csf as (
	select f.id, f.parent_id, f.functionname , 1 lvl
	from core_srd_function f
	where f.id = p_function_id
	union all
	select f.id, f.parent_id, f.functionname , lvl+1
	from core_srd_function f
	join csf on csf.parent_id = f.id
	)
	select *
	from csf
	order by lvl
) loop
	vFullName := cur.functionname || ' - ' || vFullName;
end loop;
	return substr(vFullName,1,length(vFullName)-3);
end;
$function$
