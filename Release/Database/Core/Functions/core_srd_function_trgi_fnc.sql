CREATE OR REPLACE FUNCTION public.core_srd_function_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select DISTINCT r.user_id
                  from core_srd_role_function f
                  join core_srd_user_role r
                    on r.role_id = f.role_id
                 where f.function_id = NEW.ID) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       end;
$function$
