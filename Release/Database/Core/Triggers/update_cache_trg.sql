CREATE OR REPLACE FUNCTION public.CORE_REGISTER_ATTR_CACHE_TRG ()
RETURNS trigger AS
$body$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       end;
$body$
LANGUAGE 'plpgsql';

drop trigger CORE_REGISTER_ATTR_CACHE_TRG on core_register_attribute;

CREATE TRIGGER CORE_REGISTER_ATTR_CACHE_TRG
  AFTER INSERT OR UPDATE OR DELETE
  ON public.core_register_attribute
  
FOR EACH ROW 
  EXECUTE PROCEDURE public.CORE_REGISTER_ATTR_CACHE_TRG();
  
  

CREATE OR REPLACE FUNCTION public.CORE_REGISTER_CACHE_TRG ()
RETURNS trigger AS
$body$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       end;
$body$
LANGUAGE 'plpgsql';

drop trigger CORE_REGISTER_CACHE_TRG on core_register;

CREATE TRIGGER CORE_REGISTER_CACHE_TRG
  AFTER INSERT OR UPDATE OR DELETE
  ON public.core_register
  
FOR EACH ROW 
  EXECUTE PROCEDURE public.CORE_REGISTER_CACHE_TRG();
  
  
  
CREATE OR REPLACE FUNCTION public.CORE_REGISTER_REL_CACHE_TRG ()
RETURNS trigger AS
$body$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       end;
$body$
LANGUAGE 'plpgsql';

drop trigger CORE_REGISTER_REL_CACHE_TRG on core_register_relation;

CREATE TRIGGER CORE_REGISTER_REL_CACHE_TRG
  AFTER INSERT OR UPDATE OR DELETE
  ON public.core_register_relation
  
FOR EACH ROW 
  EXECUTE PROCEDURE public.CORE_REGISTER_REL_CACHE_TRG();