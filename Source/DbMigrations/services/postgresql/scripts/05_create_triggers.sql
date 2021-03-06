
/*common_registers_with_soft_deletion_update_trg 1*/
DROP TRIGGER IF EXISTS common_registers_with_soft_deletion_update_trg ON common_registers_with_soft_deletion;
CREATE TRIGGER common_registers_with_soft_deletion_update_trg AFTER INSERT OR DELETE OR UPDATE ON public.common_registers_with_soft_deletion FOR EACH ROW EXECUTE PROCEDURE common_registers_with_soft_deletion_update_trg();

/*core_register_attr_sync_delete_table_trg 2*/
DROP TRIGGER IF EXISTS core_register_attr_sync_delete_table_trg ON core_register_attribute;
CREATE TRIGGER core_register_attr_sync_delete_table_trg AFTER INSERT OR DELETE OR UPDATE ON public.core_register_attribute FOR EACH ROW EXECUTE PROCEDURE core_register_attr_sync_delete_table_trg();

/*core_long_process_queue_changed 3*/
DROP TRIGGER IF EXISTS core_long_process_queue_changed ON core_long_process_queue;
CREATE TRIGGER core_long_process_queue_changed AFTER INSERT OR UPDATE ON public.core_long_process_queue FOR EACH ROW EXECUTE PROCEDURE notify_gbu_long_proc_updating();

/*core_long_process_type_cache_trg 4*/
DROP TRIGGER IF EXISTS core_long_process_type_cache_trg ON core_long_process_type;
CREATE TRIGGER core_long_process_type_cache_trg AFTER INSERT OR DELETE OR UPDATE ON public.core_long_process_type FOR EACH ROW EXECUTE PROCEDURE core_long_process_type_cache_trg();

/*core_register_cache_trg 5*/
DROP TRIGGER IF EXISTS core_register_cache_trg ON core_register;
CREATE TRIGGER core_register_cache_trg AFTER INSERT OR DELETE OR UPDATE ON public.core_register FOR EACH ROW EXECUTE PROCEDURE core_register_cache_trg();

/*core_register_rel_cache_trg 6*/
DROP TRIGGER IF EXISTS core_register_rel_cache_trg ON core_register_relation;
CREATE TRIGGER core_register_rel_cache_trg AFTER INSERT OR DELETE OR UPDATE ON public.core_register_relation FOR EACH ROW EXECUTE PROCEDURE core_register_rel_cache_trg();

/*core_srd_department_trgi 7*/
DROP TRIGGER IF EXISTS core_srd_department_trgi ON core_srd_department;
CREATE TRIGGER core_srd_department_trgi AFTER INSERT OR UPDATE ON public.core_srd_department FOR EACH ROW EXECUTE PROCEDURE core_srd_department_trgi_fnc();

/*core_srd_function_trgi 8*/
DROP TRIGGER IF EXISTS core_srd_function_trgi ON core_srd_function;
CREATE TRIGGER core_srd_function_trgi AFTER INSERT OR UPDATE ON public.core_srd_function FOR EACH ROW EXECUTE PROCEDURE core_srd_function_trgi_fnc();

/*core_srd_role_attr_trgi 9*/
DROP TRIGGER IF EXISTS core_srd_role_attr_trgi ON core_srd_role_attr;
CREATE TRIGGER core_srd_role_attr_trgi AFTER INSERT OR UPDATE ON public.core_srd_role_attr FOR EACH ROW EXECUTE PROCEDURE core_srd_role_attr_trgi_fnc();

/*core_srd_role_function_trgd 10*/
DROP TRIGGER IF EXISTS core_srd_role_function_trgd ON core_srd_role_function;
CREATE TRIGGER core_srd_role_function_trgd AFTER DELETE ON public.core_srd_role_function FOR EACH ROW EXECUTE PROCEDURE core_srd_role_function_trgd_fnc();

/*core_srd_role_function_trgi 11*/
DROP TRIGGER IF EXISTS core_srd_role_function_trgi ON core_srd_role_function;
CREATE TRIGGER core_srd_role_function_trgi AFTER INSERT OR UPDATE ON public.core_srd_role_function FOR EACH ROW EXECUTE PROCEDURE core_srd_role_function_trgi_fnc();

/*core_srd_role_register_trgi 12*/
DROP TRIGGER IF EXISTS core_srd_role_register_trgi ON core_srd_role_register;
CREATE TRIGGER core_srd_role_register_trgi AFTER INSERT OR UPDATE ON public.core_srd_role_register FOR EACH ROW EXECUTE PROCEDURE core_srd_role_register_trgi_fnc();

/*core_srd_role_trgi 13*/
DROP TRIGGER IF EXISTS core_srd_role_trgi ON core_srd_role;
CREATE TRIGGER core_srd_role_trgi AFTER INSERT OR UPDATE ON public.core_srd_role FOR EACH ROW EXECUTE PROCEDURE core_srd_role_trgi_fnc();

/*core_srd_user_role_trgd 14*/
DROP TRIGGER IF EXISTS core_srd_user_role_trgd ON core_srd_user_role;
CREATE TRIGGER core_srd_user_role_trgd AFTER DELETE ON public.core_srd_user_role FOR EACH ROW EXECUTE PROCEDURE core_srd_user_role_trgd_fnc();

/*core_srd_user_role_trgi 15*/
DROP TRIGGER IF EXISTS core_srd_user_role_trgi ON core_srd_user_role;
CREATE TRIGGER core_srd_user_role_trgi AFTER INSERT OR UPDATE ON public.core_srd_user_role FOR EACH ROW EXECUTE PROCEDURE core_srd_user_role_trgi_fnc();

/*core_srd_user_trgi 16*/
DROP TRIGGER IF EXISTS core_srd_user_trgi ON core_srd_user;
CREATE TRIGGER core_srd_user_trgi AFTER INSERT OR UPDATE ON public.core_srd_user FOR EACH ROW EXECUTE PROCEDURE core_srd_user_trgi_fnc();

/*core_srd_usersettings_trgi 17*/
DROP TRIGGER IF EXISTS core_srd_usersettings_trgi ON core_srd_usersettings;
CREATE TRIGGER core_srd_usersettings_trgi AFTER INSERT OR UPDATE ON public.core_srd_usersettings FOR EACH ROW EXECUTE PROCEDURE core_srd_usersettings_trgi_fnc();

/*core_register_attr_cache_trg 18*/
DROP TRIGGER IF EXISTS core_register_attr_cache_trg ON core_register_attribute;
CREATE TRIGGER core_register_attr_cache_trg AFTER INSERT OR DELETE OR UPDATE ON public.core_register_attribute FOR EACH ROW EXECUTE PROCEDURE core_register_attr_cache_trg();

/*ko_unload_result_queue_changed 19*/
DROP TRIGGER IF EXISTS ko_unload_result_queue_changed ON ko_unload_result_queue;
CREATE TRIGGER ko_unload_result_queue_changed AFTER INSERT OR UPDATE ON public.ko_unload_result_queue FOR EACH ROW EXECUTE PROCEDURE notify_ko_unload_result_proc_updating();

/*market_outliers_checking_history_changed 20*/
DROP TRIGGER IF EXISTS market_outliers_checking_history_changed ON market_outliers_checking_history;
CREATE TRIGGER market_outliers_checking_history_changed AFTER INSERT OR UPDATE ON public.market_outliers_checking_history FOR EACH ROW EXECUTE PROCEDURE notify_market_outliers_checking_updating();

/*widget_background_process 21*/
DROP TRIGGER IF EXISTS widget_background_process ON core_long_process_queue;
CREATE TRIGGER widget_background_process AFTER INSERT OR UPDATE OF status, error_id, message, progress ON public.core_long_process_queue FOR EACH ROW EXECUTE PROCEDURE notify_core_long_queue_for_widget();

/*core_message_to_insert 22*/
DROP TRIGGER IF EXISTS core_message_to_insert ON core_messages_to;
CREATE TRIGGER core_message_to_insert AFTER INSERT ON public.core_messages_to FOR EACH ROW EXECUTE PROCEDURE notify_core_message_to_update();

/*core_message_to_insert_and_update 23*/
DROP TRIGGER IF EXISTS core_message_to_insert_and_update ON core_messages_to;
CREATE TRIGGER core_message_to_insert_and_update AFTER INSERT OR UPDATE OF was_readed ON public.core_messages_to FOR EACH ROW EXECUTE PROCEDURE notify_core_message_to_insert_and_update();
