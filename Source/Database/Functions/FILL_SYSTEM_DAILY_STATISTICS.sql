create or replace function FILL_SYSTEM_DAILY_STATISTICS() returns void AS $$
declare
  ID_VAL                       numeric;
  STAT_DATE_VAL                date;
  DB_SIZE_VAL                  numeric;
  ERRORS_VAL                   numeric;
  WARNINGS_VAL                 numeric;
  ACTIONS_VAL                  numeric;
  SESSIONS_VAL                 numeric;
  CHANGES_VAL                  numeric;
  DIAGNOSTICS_SLOW_VAL         numeric;
  
  LONG_PROC_RUN_VAL                numeric;
  LONG_PROC_RUN_ERROR_VAL          numeric;
  LONG_PROC_INSUR_LOAD_RUN_VAL                numeric;
  LONG_PROC_INSUR_LOAD_RUN_ERROR_VAL          numeric;
  
  BTI_OBJECTS_LOADED_VAL       numeric;
  BTI_OBJECTS_LOADED_ERROR_VAL numeric;
  
  EHD_OBJECTS_LOADED_VAL       numeric;
  EHD_OBJECTS_LOADED_ERROR_VAL numeric;
  
  INSUR_BUILDING_LOADED_VAL       numeric;
  INSUR_BUILDING_LOADED_ERROR_VAL numeric;
  
  INSUR_FLAT_LOADED_VAL       numeric;
  INSUR_FLAT_LOADED_ERROR_VAL numeric;
  
  TOTAL_COUNT_INSUR_BUILDING_VAL NUMERIC;
  TOTAL_COUNT_INSUR_FLAT_VAL     NUMERIC;
  
  
begin
  -- DATE
  --STAT_DATE_VAL := trunc(sysdate) - 1;
  STAT_DATE_VAL := CURRENT_DATE - 1;
  
  INSERT INTO system_daily_stat_db_obj
  (SELECT STAT_DATE_VAL as STAT_DATE
       , 'INDEX' as SEGMENT_TYPE
       , i.indexname as SEGMENT_NAME
       , i.tablename as TABLE_NAME
       , GetSizeOfRelation(i.indexname::varchar)::numeric/(1024*1024) as SIZE_MEG
  FROM pg_indexes as i
  WHERE i.schemaname = 'public'

  UNION ALL

  SELECT STAT_DATE_VAL as STAT_DATE
       , 'TABLE' as SEGMENT_TYPE
       , t.table_name as SEGMENT_NAME
       , t.table_name as TABLE_NAME
       , GetSizeOfRelation(t.table_name::varchar)::numeric/(1024*1024) as SIZE_MEG
  FROM information_schema.tables as t);

  --DB_SIZE
  --select sum(t.bytes) / (1024 * 1024) into DB_SIZE_VAL from user_extents t;
  --SELECT pg_catalog.pg_database_size('cipjs_main') / (1024 * 1024) INTO DB_SIZE_VAL;
  SELECT sum(t.size_meg) INTO DB_SIZE_VAL from system_daily_stat_db_obj t where t.stat_date = STAT_DATE_VAL;
  

  -- ID
  select count(1) + 1 into ID_VAL from SYSTEM_DAILY_STATISTICS;

  --ERRORS
  select count(1)
    into ERRORS_VAL
    from core_error_log t
   where t.errordate >= STAT_DATE_VAL
     and t.errordate < STAT_DATE_VAL + 1
     and t.msgtype = 'ERROR';

  --WARNINGS
  select count(1)
    into WARNINGS_VAL
    from core_error_log t
   where t.errordate >= STAT_DATE_VAL
     and t.errordate < STAT_DATE_VAL + 1
     and t.msgtype = 'WARNING';

  --ACTIONS
  select count(1)
    into ACTIONS_VAL
    from core_srd_audit t
   where t.actiontime >= STAT_DATE_VAL
     and t.actiontime < STAT_DATE_VAL + 1;

  --SESSIONS
  select count(1)
    into SESSIONS_VAL
    from core_srd_session t
   where t.logintime >= STAT_DATE_VAL
     and t.logintime < STAT_DATE_VAL + 1;

  --CHANGES
  select count(1)
    into CHANGES_VAL
    from core_td_change ch
    join core_td_changeset chs
      on chs.id = ch.changeset_id
   where chs.changeset_date >= STAT_DATE_VAL
     and chs.changeset_date < STAT_DATE_VAL + 1;

  --DIAGNOSTICS_SLOW
  select count(1)
    into DIAGNOSTICS_SLOW_VAL
    from core_diagnostics t
   where t.action_date >= STAT_DATE_VAL
     and t.action_date < STAT_DATE_VAL + 1
     and t.execution_duration > 10000000;
  
  -- LONG PROCESS
  select count(1)
    into LONG_PROC_RUN_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 3;
  
  -- LONG PROCESS ERROR
  select count(1)
    into LONG_PROC_RUN_ERROR_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 4;
     
    
  -- LONG PROCESS INSUR LOAD
  select count(1)
    into LONG_PROC_INSUR_LOAD_RUN_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 3
    and t.process_type_id in (3,5,7,8);
  
  -- LONG PROCESS INSUR LOAD ERROR
  select count(1)
    into LONG_PROC_INSUR_LOAD_RUN_ERROR_VAL
    from core_long_process_queue t
  where t.end_date >= STAT_DATE_VAL
    and t.end_date < STAT_DATE_VAL + 1
    and t.status = 4
    and t.process_type_id in (3,5,7,8);
    
  -- BTI_OBJECTS_LOADED_VAL
  -- BTI_OBJECTS_LOADED_ERROR_VAL
  
  -- EHD_OBJECTS_LOADED_VAL
  -- EHD_OBJECTS_LOADED_ERROR_VAL
  
  -- INSUR_BUILDING_LOADED_VAL
  select count(1)
    into INSUR_BUILDING_LOADED_VAL
    from import_log_insur_building t
   where t.date_loaded >= STAT_DATE_VAL
     and t.date_loaded < STAT_DATE_VAL + 1
     and t.is_error = 0;
     
  -- INSUR_BUILDING_LOADED_ERROR_VAL
  select count(1)
    into INSUR_BUILDING_LOADED_ERROR_VAL
    from import_log_insur_building t
   where t.date_loaded >= STAT_DATE_VAL
     and t.date_loaded < STAT_DATE_VAL + 1
     and t.is_error = 1;
  
  -- INSUR_FLAT_LOADED_VAL
  select count(1)
    into INSUR_FLAT_LOADED_VAL
    from import_log_insur_flat_b t
   where t.date_loaded >= STAT_DATE_VAL
     and t.date_loaded < STAT_DATE_VAL + 1
     and t.is_error = 0;
     
  -- INSUR_FLAT_LOADED_ERROR_VAL
  select count(1)
    into INSUR_FLAT_LOADED_ERROR_VAL
    from import_log_insur_flat_b t
   where t.date_loaded >= STAT_DATE_VAL
     and t.date_loaded < STAT_DATE_VAL + 1
     and t.is_error = 0;
  
  -- TOTAL_COUNT_INSUR_BUILDING_VAL
   select count(1)
    into TOTAL_COUNT_INSUR_BUILDING_VAL
    from insur_building_q t
   where t.actual = 1;
  
  -- TOTAL_COUNT_INSUR_FLAT_VAL
   select count(1)
    into TOTAL_COUNT_INSUR_FLAT_VAL
    from insur_flat_q t
   where t.actual = 1;

  insert into SYSTEM_DAILY_STATISTICS
    (ID,
     STAT_DATE,
     DB_SIZE,
     ERRORS,
     WARNINGS,
     ACTIONS,
     SESSIONS,
     CHANGES,
     DIAGNOSTICS_SLOW,
     
     LONG_PROC_RUN,
     LONG_PROC_RUN_ERROR,
     LONG_PROC_INSUR_LOAD_RUN,
     LONG_PROC_INSUR_LOAD_RUN_ERROR,
  
     INSUR_BUILDING_LOADED,
     INSUR_BUILDING_LOADED_ERROR,
  
     INSUR_FLAT_LOADED,
     INSUR_FLAT_LOADED_ERROR,
  
     TOTAL_COUNT_INSUR_BUILDING,
     TOTAL_COUNT_INSUR_FLAT
     
     )
  VALUES
    (ID_VAL,
     STAT_DATE_VAL,
     DB_SIZE_VAL,
     ERRORS_VAL,
     WARNINGS_VAL,
     ACTIONS_VAL,
     SESSIONS_VAL,
     CHANGES_VAL,
     DIAGNOSTICS_SLOW_VAL,
     
     LONG_PROC_RUN_VAL,
     LONG_PROC_RUN_ERROR_VAL,
     LONG_PROC_INSUR_LOAD_RUN_VAL,
     LONG_PROC_INSUR_LOAD_RUN_ERROR_VAL,
  
     INSUR_BUILDING_LOADED_VAL,
     INSUR_BUILDING_LOADED_ERROR_VAL,
  
     INSUR_FLAT_LOADED_VAL,
     INSUR_FLAT_LOADED_ERROR_VAL,
  
     TOTAL_COUNT_INSUR_BUILDING_VAL,
     TOTAL_COUNT_INSUR_FLAT_VAL
     
     );

end; $$ LANGUAGE plpgsql;