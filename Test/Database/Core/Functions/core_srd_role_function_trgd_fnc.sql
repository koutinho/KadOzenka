CREATE OR REPLACE FUNCTION public.core_srd_role_function_trgd_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = OLD.Role_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       end;
$function$
