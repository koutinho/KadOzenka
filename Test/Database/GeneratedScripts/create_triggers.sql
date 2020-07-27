
-- ### Скрипт создания триггеров

--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_user_role_trgd'))then
        CREATE TRIGGER core_srd_user_role_trgd AFTER DELETE ON public.core_srd_user_role FOR EACH ROW EXECUTE PROCEDURE core_srd_user_role_trgd_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_user_role_trgi'))then
        CREATE TRIGGER core_srd_user_role_trgi AFTER INSERT OR UPDATE ON public.core_srd_user_role FOR EACH ROW EXECUTE PROCEDURE core_srd_user_role_trgi_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_role_register_trgi'))then
        CREATE TRIGGER core_srd_role_register_trgi AFTER INSERT OR UPDATE ON public.core_srd_role_register FOR EACH ROW EXECUTE PROCEDURE core_srd_role_register_trgi_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_role_function_trgd'))then
        CREATE TRIGGER core_srd_role_function_trgd AFTER DELETE ON public.core_srd_role_function FOR EACH ROW EXECUTE PROCEDURE core_srd_role_function_trgd_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_role_function_trgi'))then
        CREATE TRIGGER core_srd_role_function_trgi AFTER INSERT OR UPDATE ON public.core_srd_role_function FOR EACH ROW EXECUTE PROCEDURE core_srd_role_function_trgi_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_role_trgi'))then
        CREATE TRIGGER core_srd_role_trgi AFTER INSERT OR UPDATE ON public.core_srd_role FOR EACH ROW EXECUTE PROCEDURE core_srd_role_trgi_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_role_attr_trgi'))then
        CREATE TRIGGER core_srd_role_attr_trgi AFTER INSERT OR UPDATE ON public.core_srd_role_attr FOR EACH ROW EXECUTE PROCEDURE core_srd_role_attr_trgi_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_function_trgi'))then
        CREATE TRIGGER core_srd_function_trgi AFTER INSERT OR UPDATE ON public.core_srd_function FOR EACH ROW EXECUTE PROCEDURE core_srd_function_trgi_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_department_trgi'))then
        CREATE TRIGGER core_srd_department_trgi AFTER INSERT OR UPDATE ON public.core_srd_department FOR EACH ROW EXECUTE PROCEDURE core_srd_department_trgi_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_register_cache_trg'))then
        CREATE TRIGGER core_register_cache_trg AFTER INSERT OR DELETE OR UPDATE ON public.core_register FOR EACH ROW EXECUTE PROCEDURE core_register_cache_trg();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_register_rel_cache_trg'))then
        CREATE TRIGGER core_register_rel_cache_trg AFTER INSERT OR DELETE OR UPDATE ON public.core_register_relation FOR EACH ROW EXECUTE PROCEDURE core_register_rel_cache_trg();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_register_attr_cache_trg'))then
        CREATE TRIGGER core_register_attr_cache_trg AFTER INSERT OR DELETE OR UPDATE ON public.core_register_attribute FOR EACH ROW EXECUTE PROCEDURE core_register_attr_cache_trg();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_usersettings_trgi'))then
        CREATE TRIGGER core_srd_usersettings_trgi AFTER INSERT OR UPDATE ON public.core_srd_usersettings FOR EACH ROW EXECUTE PROCEDURE core_srd_usersettings_trgi_fnc();
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexisttrigger('core_srd_user_trgi'))then
        CREATE TRIGGER core_srd_user_trgi AFTER INSERT OR UPDATE ON public.core_srd_user FOR EACH ROW EXECUTE PROCEDURE core_srd_user_trgi_fnc();
    end if;
END $$;