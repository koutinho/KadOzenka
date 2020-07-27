CREATE OR REPLACE FUNCTION public.core_srd_user_role_trgd_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user',OLD.user_id);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       end;
$function$
