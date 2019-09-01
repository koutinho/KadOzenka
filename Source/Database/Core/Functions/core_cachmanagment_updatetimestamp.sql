CREATE OR REPLACE FUNCTION public.core_cachmanagment_updatetimestamp(pcacheobject character varying, pcachekey bigint DEFAULT 0, pextradata character varying DEFAULT 'NULL'::character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
	begin
		update core_cache_updates set cache_timestamp = current_timestamp
		where cacheobject = pcacheobject 
			and cachekey = coalesce(pcachekey,0)
			and extradata = coalesce(pextradata,'NULL');
		if not found then
			INSERT INTO CORE_CACHE_UPDATES(id,cacheobject, CACHEKEY, EXTRADATA, CACHE_TIMESTAMP)
        	VALUES (nextval('reg_object_seq'), pcacheobject, pcachekey, pextradata, current_timestamp);
		end if;
	end;
$function$
