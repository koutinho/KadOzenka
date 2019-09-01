CREATE OR REPLACE FUNCTION public.core_srd_role_attr_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
                join core_srd_role_register rr on rr.role_id = ur.role_id
               where rr.id = NEW.rule_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       end;
$function$
