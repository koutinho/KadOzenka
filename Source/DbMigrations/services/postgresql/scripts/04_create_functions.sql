
/*getsizeofrelation 1*/
CREATE OR REPLACE FUNCTION public.getsizeofrelation(character varying)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
declare
	relName ALIAS FOR $1;
    result bigint;
begin
	SELECT pg_total_relation_size(relName) INTO result;
    return result;
exception
	WHEN others THEN
		return 0;
END
$function$;

/*core_srd_role_pkg_getfullfunctionname 2*/
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
$function$;

/*core_long_process_type_cache_trg 3*/
CREATE OR REPLACE FUNCTION public.core_long_process_type_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.long.process');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END
$function$;

/*core_register_attr_cache_trg 4*/
CREATE OR REPLACE FUNCTION public.core_register_attr_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;

/*core_register_cache_trg 5*/
CREATE OR REPLACE FUNCTION public.core_register_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;

/*core_register_rel_cache_trg 6*/
CREATE OR REPLACE FUNCTION public.core_register_rel_cache_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.registers');
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END $function$;

/*core_srd_department_trgi_fnc 7*/
CREATE OR REPLACE FUNCTION public.core_srd_department_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	   	 execute core_cachmanagment_updateTimeStamp('core.srd.common');
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

/*last_post 8*/
CREATE OR REPLACE FUNCTION public.last_post(text, character)
 RETURNS integer
 LANGUAGE sql
 IMMUTABLE
AS $function$ 
     select length($1)- length(regexp_replace($1, '.*' || $2,''));
$function$;

/*core_updstru_checkexistsequence 9*/
CREATE OR REPLACE FUNCTION public.core_updstru_checkexistsequence(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
          sName ALIAS FOR $1;
          nCnt numeric;
    begin
          SELECT COUNT(*)
          INTO nCnt
          FROM pg_catalog.pg_sequences as s
          --TABLE pg_catalog.pg_sequences
          WHERE s.sequencename = lower(sName) AND s.schemaname NOT IN ('pg_catalog', 'information_schema');

          if (nCnt = 0) then
              return false;
          else
              return true;
          end if;

    END $function$;

/*core_srd_function_trgi_fnc 10*/
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
       END $function$;

/*core_updstru_checkexistindex 11*/
CREATE OR REPLACE FUNCTION public.core_updstru_checkexistindex(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
  declare
		sIndexName ALIAS FOR $1;
        nCnt numeric;
  begin
		SELECT COUNT(*)
        INTO nCnt
        FROM pg_catalog.pg_indexes as i
        --TABLE pg_catalog.pg_indexes
        WHERE lower(i.indexname) = lower(sIndexName) AND i.schemaname = 'public';

        if(nCnt = 0)then
        	return false;
        else
        	return true;
        end if;

  END
$function$;

/*core_srd_role_function_trgd_fnc 12*/
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
       END $function$;

/*core_srd_role_function_trgi_fnc 13*/
CREATE OR REPLACE FUNCTION public.core_srd_role_function_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.Role_id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

/*core_srd_role_register_trgi_fnc 14*/
CREATE OR REPLACE FUNCTION public.core_srd_role_register_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.Role_id) loop

    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
       
	   if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

/*core_srd_role_trgi_fnc 15*/
CREATE OR REPLACE FUNCTION public.core_srd_role_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	   declare
	   	cur record;
       begin
  		for cur in (select ur.*
                from core_srd_user_role ur
               where ur.role_id = NEW.id) loop
    		execute core_cachmanagment_updateTimeStamp('core.srd.user', cur.USER_ID);
		end loop;
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

/*fill_system_daily_statistics 16*/
CREATE OR REPLACE FUNCTION public.fill_system_daily_statistics()
 RETURNS void
 LANGUAGE plpgsql
AS $function$
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
     LONG_PROC_RUN_ERROR
	 
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
     LONG_PROC_RUN_ERROR_VAL
     
     );

END
$function$;

/*core_srd_role_attr_trgi_fnc 17*/
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
       END $function$;

/*core_srd_user_role_trgd_fnc 18*/
CREATE OR REPLACE FUNCTION public.core_srd_user_role_trgd_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user',OLD.user_id);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

/*core_srd_user_role_trgi_fnc 19*/
CREATE OR REPLACE FUNCTION public.core_srd_user_role_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.user_id);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

/*core_srd_user_trgi_fnc 20*/
CREATE OR REPLACE FUNCTION public.core_srd_user_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.ID);
	     execute core_cachmanagment_updateTimeStamp('core.srd.common');
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

/*core_srd_usersettings_trgi_fnc 21*/
CREATE OR REPLACE FUNCTION public.core_srd_usersettings_trgi_fnc()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
       begin
	     execute core_cachmanagment_updateTimeStamp('core.srd.user', NEW.USERID);
         if TG_OP = 'DELETE' then return OLD;
         else return NEW;
         end if;
       END $function$;

/*core_cachmanagment_updatetimestamp 22*/
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
	END
$function$;

/*core_numerator_sregnomdecrement 23*/
CREATE OR REPLACE FUNCTION public.core_numerator_sregnomdecrement(p_numeratorid bigint, p_regnomtype bigint, p_par0 character varying, p_par1 character varying, p_par2 character varying, p_par3 character varying, p_par4 character varying, p_par5 character varying, p_par6 character varying, p_par7 character varying, p_par8 character varying, p_par9 character varying, p_decrementstep bigint, OUT p_sequenceid bigint, OUT p_numdecrement bigint)
 RETURNS record
 LANGUAGE plpgsql
AS $function$
DECLARE
  v_sequenceid      bigint default -1;
  v_newsequenceid   bigint default -1;
  v_currentsequence bigint default 0;
BEGIN
  lock table core_regnom_sequences in access exclusive mode;
  p_numdecrement := 0;
    begin
      select rsq.id
        into strict v_sequenceid
        from core_regnom_sequences rsq
       where (rsq.numeratorid = p_numeratorid and
             rsq.regnomtype = p_regnomtype)
         and (case coalesce(rsq.par0, '') when coalesce(p_par0, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par1, '') when coalesce(p_par1, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par2, '') when coalesce(p_par2, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par3, '') when coalesce(p_par3, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par4, '') when coalesce(p_par4, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par5, '') when coalesce(p_par5, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par6, '') when coalesce(p_par6, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par7, '') when coalesce(p_par7, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par8, '') when coalesce(p_par8, '') then 1 else 0 end) = 1
         and (case coalesce(rsq.par9, '') when coalesce(p_par9, '') then 1 else 0 end) = 1;
    exception
      when no_data_found then
        v_sequenceid := -1;
    end;

    if coalesce(v_sequenceid, -1) <> -1 then
      select currentincrement
        into v_currentsequence
        from core_regnom_sequences
       where id = v_sequenceid;
       
      update core_regnom_sequences
        set currentincrement = core_regnom_sequences.currentincrement - p_decrementstep
      where id = v_sequenceid
      returning currentincrement into p_numdecrement;
      p_sequenceid := v_sequenceid;          
    end if;
END
$function$;

/*core_numerator_sregnomincrement 24*/
CREATE OR REPLACE FUNCTION public.core_numerator_sregnomincrement(p_numeratorid bigint, p_regnomtype bigint, p_par0 character varying, p_par1 character varying, p_par2 character varying, p_par3 character varying, p_par4 character varying, p_par5 character varying, p_par6 character varying, p_par7 character varying, p_par8 character varying, p_par9 character varying, p_minval bigint, p_maxval bigint, p_incrementstep bigint, OUT p_sequenceid bigint, OUT p_numincrement bigint)
 RETURNS record
 LANGUAGE plpgsql
AS $function$
DECLARE
  v_sequenceid      bigint default - 1;
  v_newsequenceid   bigint default - 1;
  v_currentsequence bigint default 0;
BEGIN
  lock table core_regnom_sequences in access exclusive mode;
  p_numincrement := 0;
    begin
      select rsq.id
        into strict v_sequenceid
        from core_regnom_sequences rsq
       where (rsq.numeratorid = p_numeratorid and
             rsq.regnomtype = p_regnomtype)
         and (case coalesce(rsq.par0,'') when coalesce(p_par0,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par1,'') when coalesce(p_par1,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par2,'') when coalesce(p_par2,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par3,'') when coalesce(p_par3,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par4,'') when coalesce(p_par4,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par5,'') when coalesce(p_par5,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par6,'') when coalesce(p_par6,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par7,'') when coalesce(p_par7,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par8,'') when coalesce(p_par8,'') then 1 else 0 end) = 1
         and (case coalesce(rsq.par9,'') when coalesce(p_par9,'') then 1 else 0 end) = 1;
    exception
      when no_data_found then
        v_sequenceid := -1;
    end;

    if coalesce(v_sequenceid, -1) <> -1 then
      select currentincrement
        into v_currentsequence
        from core_regnom_sequences
       where id = v_sequenceid;
      if v_currentsequence = p_maxval then
      	begin
          update core_regnom_sequences
             set currentincrement = p_minval
           where id = v_sequenceid
          returning v_currentsequence into p_numincrement;
        end;
      else
      	begin
          update core_regnom_sequences
             set currentincrement = core_regnom_sequences.currentincrement +
                                                    p_incrementstep
           where id = v_sequenceid
          returning currentincrement into p_numincrement;
          p_sequenceid := v_sequenceid;
        end;
      end if;
    else
      select coalesce(max(id) + 1, 1) into v_newsequenceid from core_regnom_sequences;
      insert into core_regnom_sequences
        (id,
         numeratorid,
         regnomtype,
         par0,
         par1,
         par2,
         par3,
         par4,
         par5,
         par6,
         par7,
         par8,
         par9,
         currentincrement)
      values
        (v_newsequenceid,
         p_numeratorid,
         p_regnomtype,
         p_par0,
         p_par1,
         p_par2,
         p_par3,
         p_par4,
         p_par5,
         p_par6,
         p_par7,
         p_par8,
         p_par9,
         p_minval)
      returning p_minval into p_numincrement;
      p_sequenceid := v_newsequenceid;
    end if;
END
$function$;

/*core_register_pkg_getorcreatechangeset 25*/
CREATE OR REPLACE FUNCTION public.core_register_pkg_getorcreatechangeset(itdinstanceid bigint, iuserid bigint, istatus bigint, changesetdate timestamp without time zone, OUT ichangesetid bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
BEGIN
  begin
      -- проверка есть ли сохраненный ранее набор изменений для данного документа и пользователя со статусом 1
      select chs.id
        into strict ichangesetid
        from core_td_changeset chs
       where chs.status = istatus
         and chs.td_id = itdinstanceid
         and chs.user_id = iuserid;
    exception
      when no_data_found then
        --создание нового набора изменений
        insert into core_td_changeset
          (id, td_id, changeset_date, status, user_id)
        values
          (nextval('seq_core_td'), itdinstanceid, changesetdate, istatus, iuserid)
        returning id into ichangesetid;
    end;
END
$function$;

/*core_register_pkg_getuserkeystring 26*/
CREATE OR REPLACE FUNCTION public.core_register_pkg_getuserkeystring(p_register_id bigint, p_object_id bigint, p_date timestamp without time zone)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
objexists bool;
quant_table varchar(40);
primary_key varchar(50);
storage_type int8;
attr RECORD;
query varchar;
val varchar;
str varchar := '';
begin
    if p_register_id is null or p_object_id is null then
        return null;
    end if;

    select cr.quant_table, cr.storage_type, cra.value_field
    into quant_table, storage_type, primary_key
    from core_register cr, core_register_attribute cra
    where cr.registerid = p_register_id
        and cra.registerid = p_register_id
        and cra.primary_key = 1;
    IF NOT FOUND THEN
        return 'Не найдены метаданные реестра ' || p_register_id;
    END IF;

    EXECUTE format('select exists(select 1 from %s where %s=$1)', quant_table, primary_key) using p_object_id into objexists;
    IF NOT objexists THEN
          return 'Объект не найден: реестр = ' || p_register_id || '; ид объекта = ' || p_object_id;
    END IF;

    FOR attr IN (
        select ra.name, ra.value_field, ra.type
        from core_register_attribute ra
        where ra.registerid = p_register_id
            and ra.user_key is not null
        order by ra.user_key
    ) loop
        query := 'select coalesce('
            || case attr.type
                when 4 then '%s'
                when 5 then 'to_char(%s, ''DD.MM.YYYY HH24:MI:SS'')'
                else        '%s::varchar' end
            || ', ''NULL'') from %s where %s=$1'
            || case when (storage_type = 1 or storage_type = 2) then ' and $2 between s_ and po_' else '' end;
        EXECUTE format(query, attr.value_field, quant_table, primary_key) using p_object_id, p_date into val;
        str := str || val || '; ';
    END LOOP;
    IF NOT FOUND THEN
        return 'ИД: ' || p_object_id;
    END IF;

    return case when char_length(str)>2 then substring(str, 1, char_length(str)-2) else 'ИД: ' || p_object_id end;
END
$function$;

/*core_updstru_checkexist_primarykey 27*/
CREATE OR REPLACE FUNCTION public.core_updstru_checkexist_primarykey(character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
DECLARE
	tablename ALIAS FOR $1;
    result int = 0;
BEGIN
	SELECT COUNT(*)
    INTO result
	FROM information_schema.table_constraints as tc
	WHERE lower(tc.table_name) = lower(tablename) AND tc.constraint_type = 'PRIMARY KEY' AND tc.table_schema = 'public';

    if(result > 0)then
    	return true;
    else
    	return false;
    end if;
END $function$;

/*additional_analysis_checker 35*/
CREATE OR REPLACE FUNCTION public.additional_analysis_checker(a_param integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
declare count_object int := 0;
BEGIN
/*task CIPJSKO-81*/
/*Создаем таблицу в которой будем хранить данные всех объектов которым был проставлен параметр дополнительного анализа*/
create temporary table return_gid (
      	id_object int,
		type_obj int,
		kn varchar,
		address varchar,
		date_definition TIMESTAMP,
		kc numeric,
		sud_number character varying,
		parameter_case int
);
/*1*/
/*джойним только последние заключения и решения*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 1
from sud_object as ob
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY zakl.id_object ORDER BY zak.date DESC) as rn
  FROM sud_zaklink as zakl
	left join sud_zak as zak on zakl.id_zak = zak.id
) x WHERE x.rn = 1) as zaklink on zaklink.id_object = ob.id
left join sud_zak as zak on zak.id = zaklink.id_zak
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where sud.sud_date < zak.date and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null);


/*2*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 2
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where (SELECT date_part('year', CAST(sud.sud_date AS DATE))) <> (SELECT date_part('year', CAST(ob.date AS DATE)))
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;


 
 /*3*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 3
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where ob.date > '2013-01-01 00:00:00' and ob.date < '2015-12-15 00:00:00'
and sud.sud_date > '2019-01-01 00:00:00' and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*4*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 4
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY zakl.id_object ORDER BY zak.date DESC) as rn
  FROM sud_zaklink as zakl
	left join sud_zak as zak on zakl.id_zak = zak.id
) x WHERE x.rn = 1) as zaklink on zaklink.id_object = ob.id
left join sud_zak as zak on zak.id = zaklink.id_zak
where zak.date +  interval '2 month' < current_date and (select count(*) from sud_sudlink where id_object = ob.id) = 0
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*5*/
/*Джойним таблицу с изменениями значений кадастровой стоимости,
в джойне сразу сортируем по дате изменнеия и сначала берем последню запись для кадастровой стоимости
а в следующем джойне пред последнюю запись изменения и в условии ищем дельту
Переписать надо*/
/*insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 5 from sud_object as ob
left join sud_sudlink as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
join
(select param_double, id from (SELECT *,
							   row_number () over (PARTITION BY id order by date_user desc) as r_n
	FROM public.sud_param where id_table = 1 and param_name = 'kc')
 x where r_n = 1) as var1/*последнее изменение*/ on var1.id = ob.id
 join
(select param_double, id from (SELECT *,
							   row_number () over (PARTITION BY id order by date_user desc) as r_n
	FROM public.sud_param where id_table = 1 and param_name = 'kc')
 x where r_n = 2) as var2 /*пред последнее изменение*/ on var2.id = ob.id
 where var2.param_double is not null and var1.param_double is not null
 and @(var1.param_double - var2.param_double) > @(var2.param_double/2)
 and (ob.exception = 0 or ob.exception is null)
 and (ob.additional_analysis = 0 or ob.additional_analysis is null)*/
 
 
/*5*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние решение*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, resh.number, 5
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
where @(ob.kc/2) > resh.rs
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null);



 /*6*/
 insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 6
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where resh.rs > ob.kc  and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*8*/
 insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 8
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where resh.rs is not null and (sud.status = 1 or sud.status = 3)
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*9*/
 insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
/*джойним только последние заключения и решения*/
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 9
from sud_object as ob
left join  (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
where (resh.rs is null or resh.rs = 0) and sud.status = 2
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*10*/
/*групперуем по суд номерам и истцам (соответственно если разные будут истцы и номера то будет несколько записей),
сортируем по количеству, тех что больше будут выше
так же простовляем номера строк и берем соответственно не первую строчку чтобы проставить флаг тем которых меньше
далее джойним  полученную таблицу к объектам и получаем объекты которым надо проставить условие*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 10 from sud_object as ob
left join sud_sudlink as slink on slink.id_object = ob.id
left join sud_sud as sud on sud.id = slink.id_sud
left join (select * from(select sud.number, ob.owner, count(*),
			  row_number () over (PARTITION BY sud.number ORDER BY sud.count DESC ) as r_n
from sud_sud as sud
left join sud_sudlink as slink on slink.id_sud = sud.id
left join sud_object as ob on slink.id_object = ob.id
group by sud.number, ob.owner)
		   x where x.r_n > 1) as temp_table
on temp_table.owner = ob.owner and temp_table.number = sud.number
where (select count(*) from sud_sudlink where id_object = ob.id) > 0
and temp_table.number is not null
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*11*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sud.number, 11 from sud_object as ob
left join sud_sudlink as resh on resh.id_object = ob.id
left join sud_sud as sud on sud.id = resh.id_sud
join
(select kn, kc, count(*)
from sud_object
where kn is not null
group by kn, kc) as tmp_grup on tmp_grup.kn = ob.kn and tmp_grup.kc = ob.kc
where tmp_grup.count > 1
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*12*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sudlink.number, 12
from sud_object as ob
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as sudlink on sudlink.id_object = ob.id
left join sud_zaklink as zaklink on zaklink.id_object = ob.id
where sudlink.rs <> zaklink.rs and (select count(*)
from sud_zaklink as z group by z.id_object having z.id_object = ob.id) = 1
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*13*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select ob.id, ob.typeobj, ob.kn, ob.adres, ob.date, ob.kc, sudlink.number, 13
from sud_object as ob
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY sudl.id_object ORDER BY sud.sud_date DESC) as rn
  FROM sud_sudlink as sudl
	left join sud_sud as sud on sudl.id_sud = sud.id
) x WHERE x.rn = 1) as sudlink on sudlink.id_object = ob.id
left join (SELECT * FROM (
  SELECT *, ROW_NUMBER () OVER (PARTITION BY zakl.id_object ORDER BY zak.date DESC) as rn
  FROM sud_zaklink as zakl
	left join sud_zak as zak on zakl.id_zak = zak.id
) x WHERE x.rn = 1) as zaklink on zaklink.id_object = ob.id
where sudlink.rs <> zaklink.rs and (select count(*)
from sud_zaklink as z group by z.id_object having z.id_object = ob.id) > 1
and (ob.exception = 0 or ob.exception is null)
and (ob.additional_analysis = 0 or ob.additional_analysis is null)
/*and ob.id not in (select id_object from return_gid)*/;

/*14*/
insert into return_gid (id_object, type_obj, kn, address, date_definition, kc, sud_number, parameter_case)
select
obj.id, obj.typeobj, obj.kn, obj.adres, obj.date, obj.kc, sud.number, 14
from sud_object as obj
left join sud_sudlink as resh on resh.id_object = obj.id
left join sud_sud as sud on sud.id = resh.id_sud
left join (select * from (select otchet.id, otchetl.id_object, otchetl.id_otchet, otchetl.rs, ROW_NUMBER () OVER (PARTITION BY otchetl.id_object order by otchet.date DESC) as rn
							from sud_otchetlink as otchetl
							left join sud_otchet as otchet on otchetl.id_otchet = otchet.id) x
			where x.rn = 1) as ol on  ol.id_object = obj.id
left join (select * from (select zakl.id, zakl.id_object, zakl.id_zak, zakl.rs, ROW_NUMBER () OVER (PARTITION BY zakl.id_object order by zak.date DESC) as rn
							from sud_zaklink as zakl
							left join sud_zak as zak on zakl.id_zak = zak.id) x
			where x.rn = 1) as zl on zl.id_object = obj.id
where ol.rs=zl.rs
and (obj.exception = 0 or obj.exception is null)
and (obj.additional_analysis = 0 or obj.additional_analysis is null)
/*and obj.id not in (select id_object from return_gid)*/;

update sud_object
set additional_analysis = 1
where id in (select id_object from return_gid);

insert into sud_dopanaliz_log (id_object, kn, address, date_definition, kc, sud_number, parameter_case, id_process, typeobj)
select r_grid.id_object, r_grid.kn, r_grid.address, r_grid.date_definition, r_grid.kc, r_grid.sud_number,
r_grid.parameter_case, a_param, r_grid.type_obj
from return_gid as r_grid;

count_object := (select count(*) from (select id_object from return_gid group by id_object) x_1);
drop table return_gid;
RETURN count_object;

END;
$function$;

/*notify_gbu_long_proc_updating 36*/
CREATE OR REPLACE FUNCTION public.notify_gbu_long_proc_updating()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	begin
		if (NEW.process_type_id = 12  or NEW.process_type_id = 13 or NEW.process_type_id = 14) then
			PERFORM pg_notify('notify_gbu_long_proc_updating'::text, 'notify_gbu_long_proc_updating'::text);
		end if;
		return null;
	end;
	$function$;

/*core_updstru_deletefkrefconstraints 37*/
CREATE OR REPLACE FUNCTION public.core_updstru_deletefkrefconstraints(character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
  declare
		sTable ALIAS FOR $1;
        qRow RECORD;
  begin
    -- Удаление внешних ссылок
    FOR qRow IN (SELECT tc.table_name as table_name_pk, tc.constraint_name as constraint_name_pk
                      , rc.constraint_name as constraint_name_fk, trc.table_name as table_name_fk
                 FROM information_schema.table_constraints as tc
                      INNER JOIN information_schema.referential_constraints as rc ON rc.unique_constraint_name = tc.constraint_name
                      INNER JOIN information_schema.table_constraints as trc ON trc.constraint_name = rc.constraint_name
                 WHERE tc.constraint_type = 'PRIMARY KEY' AND lower(tc.table_name) = lower(sTable)) LOOP
      execute immediate 'alter table ' || qRow.table_name_fk || ' drop constraint ' || qRow.constraint_name_fk;
    end loop;

  END $function$;

/*core_updstru_getcolumndefault 38*/
CREATE OR REPLACE FUNCTION public.core_updstru_getcolumndefault(character varying, character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
  declare
		tableName ALIAS FOR $1;
        columnName  ALIAS FOR $2;
		defaultValue varchar(200);
  begin
    SELECT c.column_default
    INTO defaultValue
    FROM information_schema.columns as c
    WHERE lower(c.table_name) = lower(tableName) AND lower(c.column_name) = lower(columnName);

    return defaultValue;
  END $function$;

/*core_updstru_getcolumntype 39*/
CREATE OR REPLACE FUNCTION public.core_updstru_getcolumntype(character varying, character varying)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare
	    tableName ALIAS FOR $1;
        columnName ALIAS FOR $2;
	    columnType varchar(100);
	begin
      	SELECT c.data_type
        INTO columnType
        FROM information_schema.columns c
        --TABLE information_schema.columns
        WHERE c.table_name = lower(tableName) AND c.column_name = lower(columnName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');

        return columnType;
	END $function$;

/*core_updstru_isnullablecolumn 40*/
CREATE OR REPLACE FUNCTION public.core_updstru_isnullablecolumn(character varying, character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    declare
		sTabName ALIAS FOR $1;
        sColName ALIAS FOR $2;
        sNullable varchar(3);
    begin
        begin
			SELECT c.is_nullable
			INTO sNullable
            FROM information_schema.columns c
            --TABLE information_schema.columns
        	WHERE lower(c.table_name) = lower(sTabName) AND lower(c.column_name) = lower(sColName) AND c.table_schema NOT IN ('pg_catalog', 'information_schema');
        exception
          when NO_DATA_FOUND then
            return false;
        end;

        if sNullable = 'Yes' then
			return true;
        else
          	return false;
        end if;

    END $function$;

/*notify_core_long_queue_for_widget 41*/
CREATE OR REPLACE FUNCTION public.notify_core_long_queue_for_widget()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE
notification json;
BEGIN
if(NEW.process_type_id is not null) then
notification = json_build_object(
                          'id', new.id,
                          'status', new.status,
                          'errorId', new.error_id,
                          'progress', new.progress,
                          'processTypeId', new.process_type_id,
                          'message', new.message,
                          'userId', NEW.user_id
                          );
			PERFORM pg_notify('notify_core_long_queue_for_widget'::text, notification::text);
            end if;
            return null;
END;
$function$;

/*proc_cancel_test_proc 42*/
CREATE OR REPLACE FUNCTION public.proc_cancel_test_proc(a_param integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
declare count_object int := 0;
BEGIN
	
	FOR i IN 0..a_param LOOP
		insert into proc_cancel_test_table
		values('1', '1', '1');	
	END LOOP;
	
	PERFORM  pg_sleep(15);
	--RAISE EXCEPTION 'Cancel exception!'  USING ERRCODE = '57014';
	
	count_object := (select count(*) from proc_cancel_test_table);
RETURN count_object;

END;
$function$;

/*notify_ko_unload_result_proc_updating 43*/
CREATE OR REPLACE FUNCTION public.notify_ko_unload_result_proc_updating()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	begin
		PERFORM pg_notify('notify_ko_unload_result_proc_updating'::text, 'notify_ko_unload_result_proc_updating'::text);
		return null;
	end;
	$function$;

/*ko_get_full_group_name 44*/
CREATE OR REPLACE FUNCTION public.ko_get_full_group_name(bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare
		_groupId ALIAS FOR $1;
		_resultGroupName character varying;
		_groupName character varying;
		_groupAlgoritm bigint;
		_parentId bigint;
		_tourYear bigint;
    begin
		if _groupId is null then
			return null;
		end if;
		
		select g.group_name, g.group_algoritm, g.parent_id, t.year
		into _groupName, _groupAlgoritm, _parentId, _tourYear
		from ko_tour_groups k
		JOIN ko_group g on k.group_id=g.id
		JOIN ko_tour t on k.tour_id=t.id
		where g.id=_groupId;
		IF NOT FOUND THEN
				select g.group_name, g.group_algoritm, g.parent_id
				into _groupName, _groupAlgoritm, _parentId
				from ko_group g 
				where g.id=_groupId;
				IF NOT FOUND THEN
					return null;
				END IF;
		END IF;

		_resultGroupName := _groupName;
		
		WHILE _parentId <> -1
        LOOP
			_groupId := _parentId;
			
			select g.group_name, g.group_algoritm, g.parent_id
			into _groupName, _groupAlgoritm, _parentId
			from ko_tour_groups k
			join ko_group g on k.group_id=g.id
			join ko_tour t on k.tour_id=t.id
			where g.id=_groupId;
			IF NOT FOUND THEN
				select g.group_name, g.group_algoritm, g.parent_id
				into _groupName, _groupAlgoritm, _parentId
				from ko_group g 
				where g.id=_groupId;
				IF NOT FOUND THEN
					EXIT;
				END IF;
			END IF;
			
			_resultGroupName := CONCAT(_resultGroupName, ' (', _groupName, ')');
        END LOOP;

		if _groupAlgoritm = 98 then
			_resultGroupName := CONCAT(_resultGroupName, ' (ОКС)');
		end if;
			
		if _groupAlgoritm = 99 then
			_resultGroupName := CONCAT(_resultGroupName,' (ЗУ)');
		end if;
			
		if _tourYear is not null then
			_resultGroupName := CONCAT(_resultGroupName, '(Тур ', _tourYear, ')');
		end if;
		
		return _resultGroupName;
		
	END $function$;

/*get_market_object_price_for_uprs 45*/
CREATE OR REPLACE FUNCTION public.get_market_object_price_for_uprs(cadastral_number character varying)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
 declare
	price numeric;
        
BEGIN


select
-- проверяем, есть ли объект-аналог с подходящим КН
case when not exists
(
	SELECT 1
          FROM MARKET_CORE_OBJECT L1_R100
          WHERE 
          (
            (L1_R100.CADASTRAL_NUMBER = get_market_object_price_for_uprs.cadastral_number AND L1_R100.PROCESS_TYPE_CODE = 742)
            AND (L1_R100.DEAL_TYPE_CODE = 734 OR L1_R100.DEAL_TYPE_CODE = 733)
          )
)
then
(
	-- если объекта нет
  	-1
 )
 
ELSE
(
    -- если объекты есть, смотрим - есть ли объект, поступивший из Росреестра (market_code = 737)
    select
    case when exists 
          (SELECT 1
            FROM MARKET_CORE_OBJECT L1_R100
            WHERE 
            (
              (L1_R100.CADASTRAL_NUMBER = get_market_object_price_for_uprs.cadastral_number AND L1_R100.PROCESS_TYPE_CODE = 742)
              AND (L1_R100.DEAL_TYPE_CODE = 734 OR L1_R100.DEAL_TYPE_CODE = 733)
              and L1_R100.market_code = 737
            )
          )
      then 
      (
          -- есть объекты из Росрееста: берем первый с самой последней датой
          SELECT 
            L1_R100.PRICE as "MarketObjectPrice"
            FROM MARKET_CORE_OBJECT L1_R100
            WHERE 
            (
              (L1_R100.CADASTRAL_NUMBER = get_market_object_price_for_uprs.cadastral_number AND L1_R100.PROCESS_TYPE_CODE = 742)
              AND (L1_R100.DEAL_TYPE_CODE = 734 OR L1_R100.DEAL_TYPE_CODE = 733)
              and L1_R100.market_code = 737
            )
            ORDER BY COALESCE(L1_R100.last_date_update, L1_R100.parser_time) desc
            limit 1
      )
      else 
      (
      		-- нет объекта из Росреестра: берем первый с самой последней датой
            SELECT 
              L1_R100.PRICE as "MarketObjectPrice"
              FROM MARKET_CORE_OBJECT L1_R100
              WHERE 
              (
                (L1_R100.CADASTRAL_NUMBER = get_market_object_price_for_uprs.cadastral_number AND L1_R100.PROCESS_TYPE_CODE = 742)
                AND (L1_R100.DEAL_TYPE_CODE = 734 OR L1_R100.DEAL_TYPE_CODE = 733)
              )
              ORDER BY COALESCE(L1_R100.last_date_update, L1_R100.parser_time) desc
              limit 1
      )
    end
)
  
END
into price;

RETURN price;
  
END;

/* Для тестирования
--нет объекта, должно быть "-1"
select get_market_object_price_for_uprs('50:27:0020315:11');
--есть объект, должна быть цена
select get_market_object_price_for_uprs('50:26:0151306:108');
--есть объект, должно быть NULL
select get_market_object_price_for_uprs('77:08:0002007:2409');
*/
$function$;

/*gbu_get_allpri_attribute_values 46*/
CREATE OR REPLACE FUNCTION public.gbu_get_allpri_attribute_values(objectids bigint[], attributeid bigint)
 RETURNS TABLE(objectid bigint, attributevalue character varying)
 LANGUAGE plpgsql
AS $function$
    declare
                _query character varying;
                _allpriTableName character varying;
                _allpriPartitioning bigint;
                _allpriTablePostfix character varying;
                 _fullAllpriTableName character varying;
       		 	_additionalConditionForTablesWithPartitionByData character varying;
                _attributeType bigint;
                _currentEndDate timestamp without time zone;

    begin

                if array_length(objectids, 1) IS NULL or array_length(objectids, 1)=0 then
                        return;
                end if;
                --raise notice '_array: %', array_length(ARRAY(1,2), 1);
                select CAST((CURRENT_DATE + INTERVAL '1 day - 1 second') AS TIMESTAMP) into _currentEndDate;

                select r.allpri_table, r.allpri_partitioning, a.type
                into _allpriTableName, _allpriPartitioning, _attributeType
                from core_register r
                join core_register_attribute a on a.registerid=r.registerid
                where a.id=attributeId;
                IF NOT FOUND THEN
                        return;
                END IF;

                if(_allpriPartitioning=2)then
                _allpriTablePostfix := CAST(attributeId as character varying);
        else
                case
                                when _attributeType=1 or _attributeType=2 or _attributeType=3
                                        then _allpriTablePostfix := 'NUM';
                                when _attributeType=4 then _allpriTablePostfix := 'TXT';
                                when _attributeType=5 then _allpriTablePostfix := 'DT';
                        end case;
        end if;

				_fullAllpriTableName :=  _allpriTableName || '_' || _allpriTablePostfix;
                _query := concat('select a.object_id as objectid, CAST(a.value as character varying) as attributeValue from ', _fullAllpriTableName, ' a where a.object_id in (', array_to_string(objectids, ','), ')');

                if _allpriPartitioning <> 2 then
                	_query = concat(_query, '  and a.attribute_id=', attributeId, ' and 
                      A.ID = (SELECT MAX(a2.id) FROM ', _fullAllpriTableName, ' a2 
                      WHERE a2.object_id = a.object_id AND a2.attribute_id = a.attribute_id AND 
                      a2.s <= ''', _currentEndDate, '''::timestamp without time zone AND 
                      a2.ot = (SELECT MAX(a3.ot) FROM ', _fullAllpriTableName, ' a3 WHERE a3.object_id = a.object_id AND 
                      a3.attribute_id = a.attribute_id AND a3.s <= ''', _currentEndDate, '''::timestamp without time zone ))');
                else
                	_query = concat(_query, ' AND a.s <= ''', _currentEndDate, '''::timestamp without time zone ',
                        ' and a.OT = (SELECT MAX(A2.OT) FROM ', _fullAllpriTableName, ' A2
                                                WHERE A2.object_id = a.object_id', ' AND A2.s <= ''', _currentEndDate, '''::timestamp without time zone )');
                end if;

 
                --raise notice '_query: %', _query;
                
                RETURN QUERY EXECUTE _query;

        END
$function$;

/*gbu_get_allpri_attribute_value 47*/
CREATE OR REPLACE FUNCTION public.gbu_get_allpri_attribute_value(objectid bigint, attributeid bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare
         _attributeValueString character varying;

	begin
                
		select attributevalue from gbu_get_allpri_attribute_values(ARRAY[objectid], attributeid) into _attributeValueString;
    	return _attributeValueString;

	END
$function$;

/*get_parent_info 48*/
CREATE OR REPLACE FUNCTION public.get_parent_info("parentCadastralNumbers" character varying[], "buildingPurposeAttributeId" bigint, "constructionPurposeAttributeId" bigint, "groupAttributeId" bigint)
 RETURNS TABLE(purpose character varying, "group" character varying, "cadastralNumberOutPut" character varying)
 LANGUAGE plpgsql
AS $function$
    declare	
    	_objectId BIGINT;
        _objectTypeCode BIGINT;
		_resultPurposeAttributeId BIGINT;
		_purpose character varying;
        _group character varying;
        _parentCadastralNumber character varying;
        
    begin
      FOREACH _parentCadastralNumber IN ARRAY "parentCadastralNumbers"
      LOOP
          _objectId := 0;
          --ищем парент-объект 
          select 
              obj.id, obj.object_type_code into _objectId, _objectTypeCode
          from gbu_main_object obj where cadastral_number = _parentCadastralNumber limit 1;
		  
		  IF FOUND THEN
			--если тип объекта - "Здание"
            if(_objectTypeCode = 5) then
                _resultPurposeAttributeId := "buildingPurposeAttributeId";
            end if;
            --если тип объекта - "Сооружение"
            if(_objectTypeCode = 7) then
                _resultPurposeAttributeId := "constructionPurposeAttributeId";
            end if;
            
            select * from gbu_get_allpri_attribute_value(_objectId, _resultPurposeAttributeId) into _purpose;
            select * from gbu_get_allpri_attribute_value(_objectId, "groupAttributeId") into _group;
            
            RETURN QUERY SELECT _purpose, _group, _parentCadastralNumber;	
		  END IF;
          
      END LOOP;
      
    	/* для тестирования
      		select * from get_parent_info(ARRAY['77:22:0020229:2534', '77:22:0030404:31', '77:22:0020229:2534213'], 14, 22, 589)
      	*/
	END
$function$;

/*notify_market_outliers_checking_updating 49*/
CREATE OR REPLACE FUNCTION public.notify_market_outliers_checking_updating()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
	begin
		PERFORM pg_notify('notify_market_outliers_checking_updating'::text, 'notify_market_outliers_checking_updating'::text);
		return null;
	end;
	$function$;

/*get_first_zach_date 50*/
CREATE OR REPLACE FUNCTION public.get_first_zach_date(p_flat_id bigint)
 RETURNS timestamp without time zone
 LANGUAGE plpgsql
AS $function$
declare
 rval timestamp;
begin
with s as(
select f.emp_id
from insur_fsp_q f
where f.obj_id = p_flat_id and f.actual = 1
union 
select ff.fsp_id
from insur_link_fsp_flat ff
where ff.obj_id = p_flat_id
)
select min(iiz.period_reg_date)
into rval
from s 
join insur_input_plat iiz on iiz.fsp_id = s.emp_id
;
return rval;
END
$function$;

/*notify_core_message_to_insert_and_update 51*/
CREATE OR REPLACE FUNCTION public.notify_core_message_to_insert_and_update()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE 
  notification json;
BEGIN
if (NEW.was_readed is null or (NEW.was_readed is not null and OLD.was_readed is null)) then
   notification = json_build_object(
                          'id', new.id,
                          'userId', new.user_id
                          );
			PERFORM pg_notify('notify_core_message_to_insert_and_update'::text, notification::text);
		end if;
		return null;
END;
$function$;

/*notify_core_message_update 52*/
CREATE OR REPLACE FUNCTION public.notify_core_message_update()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE 
  notification json;
BEGIN
if (NEW.is_urgent = 1 and (NEW.urgent_expire_date is null or NEW.urgent_expire_date > current_timestamp)) then
   notification = json_build_object(
                          'id', new.id,
                          'userId', new.user_id
                          );
			PERFORM pg_notify('notify_core_message_update'::text, notification::text);
		end if;
		return null;
END;
$function$;

/*notify_core_message_to_update 53*/
CREATE OR REPLACE FUNCTION public.notify_core_message_to_update()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE 
  notification json;
  is_send int;
BEGIN
is_send := (select 1 from core_messages where id = NEW.message_id and is_urgent = 1 and 
  (urgent_expire_date > current_timestamp or urgent_expire_date is null));
if (is_send = 1) then
   notification = json_build_object(
                          'id', new.message_id,
                          'userId', new.user_id
                          );
			PERFORM pg_notify('notify_core_message_update'::text, notification::text);
		end if;
		return null;
END;
$function$;

/*update_cache_table_for_data_composition_reports_for_registers 54*/
CREATE OR REPLACE FUNCTION public.update_cache_table_for_data_composition_reports_for_registers()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE
	row RECORD;
    is_attribute_already_added bigint;
    attribute_id bigint;

BEGIN
	/* Для Росреестра передаем ИД атрибута в параметры, для остальных реестров - берем из вновь вставленной строки */
	IF (TG_ARGV[0] IS NULL) THEN
    	attribute_id := NEW.attribute_id;
    ELSE
    	attribute_id := TG_ARGV[0];
    END IF;

    SELECT * INTO row FROM data_composition_by_characteristics_by_tables WHERE object_id = NEW.object_id;
    
    IF NOT FOUND THEN
        INSERT INTO data_composition_by_characteristics_by_tables (object_id, attributes) VALUES (NEW.object_id, array[ attribute_id ]);
    ELSE
    	/*Если атрибут не был добавлен ранее, то добавляем его*/
        IF (array_position(row.attributes, attribute_id) is NULL) THEN
    		update data_composition_by_characteristics_by_tables cache_table set attributes = array_append(attributes, attribute_id) 
            	where cache_table.object_id = NEW.object_id;
        END IF;
    END IF;

    
	RETURN NULL;
END;
$function$;

/*update_cache_table_for_data_composition_reports_for_main_object 55*/
CREATE OR REPLACE FUNCTION public.update_cache_table_for_data_composition_reports_for_main_object()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    INSERT INTO data_composition_by_characteristics_by_tables (object_id, cadastral_number, object_type_code) 
    VALUES (NEW.id, NEW.cadastral_number, NEW.object_type_code);    
	
    RETURN NULL;
END;
$function$;

/*ko_delete_tasks 56*/
CREATE OR REPLACE FUNCTION public.ko_delete_tasks(VARIADIC tasks bigint[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
DECLARE	
	taskId bigint;
	_query character varying;
	gbuAttributeInfoRow record;
	registerData record;
	attachmentsIds bigint[];
	
	taskTourId bigint;
	taskDocumentId bigint;
	gbuObjectCount bigint;
BEGIN
	FOREACH taskId IN ARRAY tasks
    LOOP
    	RAISE NOTICE 'task_ID is %', taskId;
		
		select t.tour_id, t.document_id
		into taskTourId, taskDocumentId
		from ko_task t
		where t.id=taskId;
		IF NOT FOUND THEN
			RAISE NOTICE 'task was not found';
			return;
		END IF;
		
		RAISE NOTICE 'taskTourId is %', taskTourId;
		RAISE NOTICE 'taskDocumentId is %', taskDocumentId;
		
		select count(*) into gbuObjectCount from (
			select distinct u.object_id 
			from ko_unit u
			where u.task_id=taskId 
		) d;			
		RAISE NOTICE 'gbuObjects count=%', gbuObjectCount;

		/*удаление из ГБУ части значений связанных атрибутов*/
		FOR gbuAttributeInfoRow IN
			select a.id, r.allpri_table from core_register_attribute a
			join core_register r on a.registerid=r.registerid 
			where r.registerdescription='Источник: Росреестр' 
				and (a.primary_key=0 or a.primary_key is null) 
				and (a.is_deleted=0 or a.is_deleted is null)
			order by a.id
		LOOP
			_query := concat('delete from ', gbuAttributeInfoRow.allpri_table, '_', gbuAttributeInfoRow.id, ' attr ', 
							 ' where attr.change_doc_id=', taskDocumentId,
							 ' and exists (select 1 from ko_unit u where u.object_id=attr.object_id and u.task_id=', taskId, ')'
							);
			raise notice '%', _query;
			EXECUTE _query;
		END LOOP;

		
		/*удаление из КО части значений из таблиц срезов факторов*/	
		FOR registerData IN
			select r.quant_table
			from core_register r
			where r.registerid in (select fr.register_id
								  from ko_tour_factor_register fr
								  where fr.tour_id=taskTourId)
		LOOP
			_query := concat('delete from ', registerData.quant_table, ' t ',
							 ' where exists (select 1 from ko_unit u where u.task_id=', taskId, ' and u.id=t.id)'
							);
			raise notice '%', _query;
			EXECUTE _query;
		END LOOP;
		
		/*удаление из таблицы изменений юнитов*/
		RAISE NOTICE 'delete from ko_unit_change';
		delete from ko_unit_change where id_unit in (
			select id from ko_unit
			where task_id= taskId
		);
		
		/*удаление из таблицы данных о кадастровой стоимости из Росреестра*/
		RAISE NOTICE 'delete from KO_COST_ROSREESTR';
		delete from KO_COST_ROSREESTR where id_object in (
			select id from ko_unit
			where task_id=taskId
		);
		
		/*логическое удаление связанных с таской образов*/
		RAISE NOTICE 'update core_attachment_object';
		attachmentsIds := ARRAY(
			select distinct at1.id 
			from core_attachment at1
				left join core_attachment_object at_obj on at1.id=at_obj.attachment_id
			where at_obj.register_id=203 and at_obj.object_id=taskId 
				and (at1.is_deleted=0 or at1.is_deleted is null)
				and (at_obj.is_deleted=0 or at_obj.is_deleted is null)
				and not exists (select * 
								from core_attachment at2 
									join core_attachment_object at_obj2 on at2.id=at_obj2.attachment_id
								where at1.id=at2.id and (at_obj2.register_id<>203 or at_obj2.object_id<>taskId) 
									and (at2.is_deleted=0 or at2.is_deleted is null)
							   		and (at_obj2.is_deleted=0 or at_obj2.is_deleted is null))
		);	
		update core_attachment_object set is_deleted=1, deleted_date=CURRENT_DATE
		where attachment_id =any(attachmentsIds);		
		update core_attachment set is_deleted=1, deleted_date=CURRENT_DATE
		where id =any(attachmentsIds);
		
		/* удаление записей из журналов при загрузки юнитов*/
		RAISE NOTICE 'delete from core_long_process_queue';
		delete from core_long_process_queue
			where process_type_id in (select id from core_long_process_type where process_name ='DataImporterGkn')
			and object_id in (select id from COMMON_IMPORT_DATA_LOG where REGISTER_ID=203 and OBJECT_ID=taskId);
		delete from COMMON_IMPORT_DATA_LOG where REGISTER_ID=203 and OBJECT_ID=taskId;
		
		/*удаление связанных с таской юнитов*/
		RAISE NOTICE 'delete from ko_unit';
		delete from ko_unit where task_id=taskId;

		/*удаление таски*/
		RAISE NOTICE 'delete from ko_task';
		delete from ko_task where id=taskId;
		
    END LOOP;
END

$function$;

/*synchronize_tables_with_deleted_data 57*/
CREATE OR REPLACE FUNCTION public.synchronize_tables_with_deleted_data()
 RETURNS event_trigger
 LANGUAGE plpgsql
AS $function$
	DECLARE r RECORD;
	BEGIN
		FOR r IN SELECT * FROM pg_event_trigger_ddl_commands() LOOP
		  IF ( r.objid::regclass::text = 'test_logical_delete_table' )
		  THEN
				RAISE EXCEPTION 'You are not allowed to change %', r.object_identity;
		  END IF;
		END LOOP;
	END;
	$function$;

/*get_modeling_results 58*/
CREATE OR REPLACE FUNCTION public.get_modeling_results("taskIds" bigint[], "modelId" bigint, "groupId" bigint, "addressAttributeId" bigint)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
    declare	
        _columns varchar;
        _tables character varying;
        _counter BIGINT;
        _query varchar;
        _baseSelect varchar;
        registerInfoRow record;
        attributesInfowRow record;
    BEGIN
   
    _tables := '';
    _columns := '';
    _counter := 1;
    FOR registerInfoRow IN
            with attributesInfo as(
            select attribute.id, attribute.name, attribute.value_field, attribute.registerId, register.quant_table
                from KO_MODEL_FACTOR factor 
                    left join core_register_attribute attribute on factor.FACTOR_ID = attribute.Id
                    left join core_register register on attribute.RegisterId = register.registerid
                where factor.model_id = "modelId"
            order by attribute.Name
            )
            select registerId, max(quant_table) as quant_table from attributesInfo group by registerId
        LOOP 
            FOR attributesInfowRow IN
                --SELECT value_field, id from attributesInfo where register_Id = registerInfoRow.registerId
               with attributesInfo as(
                select attribute.id, attribute.name, attribute.value_field, attribute.registerId, register.quant_table
                    from KO_MODEL_FACTOR factor 
                        left join core_register_attribute attribute on factor.FACTOR_ID = attribute.Id
                        left join core_register register on attribute.RegisterId = register.registerid
                    where factor.model_id = "modelId"
                order by attribute.Name
                )
                select * from attributesInfo where registerId = registerInfoRow.registerId
                LOOP
                    _columns := concat(_columns, 'factorsTable', _counter, '.', attributesInfowRow.value_field, 
                    ' as "', attributesInfowRow.id, '", ');				
                END LOOP; 
    		
            _tables := concat(_tables, ' left join ', registerInfoRow.quant_table, ' factorsTable', _counter,
            ' on unit.id = factorsTable', _counter, '.Id ');
            _counter := _counter + 1;
        END LOOP;  
    	
        _baseSelect := concat('SELECT unit.Id, unit.PROPERTY_TYPE, unit.CADASTRAL_BLOCK,', 
        ' unit.CADASTRAL_NUMBER, unit.SQUARE, unit.UPKS, unit.CADASTRAL_COST, ',
        '(select * from  gbu_get_allpri_attribute_value( unit.id,', "addressAttributeId", ')) as Address ');
        if(_columns = '') then
            _query := concat(_query, _baseSelect);
        else
            _query := concat(_query, _baseSelect, ', ', LEFT(_columns,-2), ' ');
        end if;
        
        _query := concat(_query, 'FROM ko_unit unit ', _tables, ' WHERE unit.TASK_ID in (', array_to_string("taskIds", ','),
         ') and unit.GROUP_ID=', "groupId", ' order by unit.CADASTRAL_BLOCK');
        
        --RETURN QUERY EXECUTE _query;
        RETURN _query;
            /* для тестирования
                select * from get_modeling_results(ARRAY[2 , 3], 7977718, 10)
            */
    END
$function$;

/*ko_delete_task 59*/
CREATE OR REPLACE FUNCTION public.ko_delete_task(taskid bigint)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    declare	
		_query character varying;
		gbuObject record;
		gbuAttributeInfoRow record;
		unitObject record; 
		attachmentsIds bigint[];
    begin
	
		--удаление из ГБУ части значений связанных атрибутов
		FOR gbuObject IN
			select distinct u.object_id, t.document_id, t.estimation_date
			from ko_unit u
				join ko_task t on t.id=u.task_id
			where u.task_id=taskid
		LOOP
			FOR gbuAttributeInfoRow IN
				select a.id, r.allpri_table from core_register_attribute a
				join core_register r on a.registerid=r.registerid 
				where r.registerdescription='Источник: Росреестр' 
					and (a.primary_key=0 or a.primary_key is null) 
					and (a.is_deleted=0 or a.is_deleted is null)
				order by a.id
			LOOP
				_query := concat('delete from ', gbuAttributeInfoRow.allpri_table, '_', gbuAttributeInfoRow.id, 
								 ' where object_id=', gbuObject.object_id,
								 ' and change_doc_id=', gbuObject.document_id,
								' and s= ''', gbuObject.estimation_date, '''::timestamp without time zone '
								' and ot= ''', gbuObject.estimation_date, '''::timestamp without time zone ');
				--raise notice '%', _query;
				EXECUTE _query;
				
			END LOOP;
		END LOOP;
		
		--удаление из КО части значений из таблиц срезов факторов
		FOR unitObject IN
			with unit_data as (
				select u.id,
				(select fr.register_id
					from ko_tour_factor_register fr
					where fr.tour_id=t.tour_id
					and case when u.property_type_code=4
							then fr.object_type_code=4
							else fr.object_type_code<>4
						end
				limit 1) as register_id
				from ko_unit u
					join ko_task t on t.id=u.task_id
				where u.task_id=taskid)
			select 	d.id, r.quant_table
			from unit_data d
				join core_register r on d.register_id=r.registerid
		LOOP
			_query := concat('delete from ', unitObject.quant_table,
							' where id=', unitObject.id);
			--raise notice '%', _query;
			EXECUTE _query;
		END LOOP;
		
		--удаление из таблицы изменений юнитов
		delete from ko_unit_change where id_unit in (
			select id from ko_unit
			where task_id= taskid
		);
		
		--удаление из таблицы данных о кадастровой стоимости из Росреестра
		delete from KO_COST_ROSREESTR where id_object in (
			select id from ko_unit
			where task_id=taskId
		);
		
		--удаление из таблицы данных о кадастровой стоимости из Росреестра
		delete from KO_COST_ROSREESTR where id_object in (
			select id from ko_unit
			where task_id=taskId
		);
		
		--логическое удаление связанных с таской образов
		attachmentsIds := ARRAY(
			select distinct at1.id 
			from core_attachment at1
				left join core_attachment_object at_obj on at1.id=at_obj.attachment_id
			where at_obj.register_id=203 and at_obj.object_id=taskId 
				and (at1.is_deleted=0 or at1.is_deleted is null)
				and (at_obj.is_deleted=0 or at_obj.is_deleted is null)
				and not exists (select * 
								from core_attachment at2 
									join core_attachment_object at_obj2 on at2.id=at_obj2.attachment_id
								where at1.id=at2.id and (at_obj2.register_id<>203 or at_obj2.object_id<>taskId) 
									and (at2.is_deleted=0 or at2.is_deleted is null)
							   		and (at_obj2.is_deleted=0 or at_obj2.is_deleted is null))
		);	
		update core_attachment_object set is_deleted=1, deleted_date=CURRENT_DATE
		where attachment_id =any(attachmentsIds);		
		update core_attachment set is_deleted=1, deleted_date=CURRENT_DATE
		where id =any(attachmentsIds);
		
		-- удаление записей из журналов при загрузки юнитов
		delete from core_long_process_queue
			where process_type_id in (select id from core_long_process_type where process_name ='DataImporterGkn')
			and object_id in (select id from COMMON_IMPORT_DATA_LOG where REGISTER_ID=203 and OBJECT_ID=taskId);
		delete from COMMON_IMPORT_DATA_LOG where REGISTER_ID=203 and OBJECT_ID=taskId;
		
		--удаление связанных с таской юнитов
		delete from ko_unit where task_id=taskId;

		--удаление таски
		delete from ko_task where id=taskId;
	END
$function$;

/*deconfigure_logging_for_register 60*/
CREATE OR REPLACE FUNCTION public.deconfigure_logging_for_register(register_code bigint, drop_history_table boolean DEFAULT false, detach_history_table boolean DEFAULT false, remove_user_tracking boolean DEFAULT false, remove_date_tracking boolean DEFAULT false)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
declare
    reg core_register%rowtype;
begin
    select *
    from core_register
    where registerid = register_code
    into reg;

    if not found then
        raise exception 'Реестр с номером % не найден', register_code;
    end if;

    if remove_user_tracking then
        execute format('alter table %s drop column if exists CHANGE_USER_ID;', reg.quant_table);
        update core_register set track_changes_userid = NULL where registerid = reg.registerid;
    end if;

    if remove_date_tracking then
        execute format('alter table %s drop column if exists CHANGE_DATE;', reg.quant_table);
        update core_register set track_changes_date = NULL where registerid = reg.registerid;
    end if;

    if detach_history_table then
        update core_register set allpri_table = NULL where registerid = reg.registerid;
    end if;

    if drop_history_table then
        update core_register set allpri_table = NULL where registerid = reg.registerid;
        execute format('drop table %s', reg.allpri_table);
    end if;

    return reg.registerid;
end
$function$;

/*configure_logging_for_register 61*/
CREATE OR REPLACE FUNCTION public.configure_logging_for_register(register_code bigint, create_history_table boolean DEFAULT false, track_user boolean DEFAULT true, track_change_date boolean DEFAULT true)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
declare
    reg core_register%rowtype;
begin
    select *
    from core_register
    where registerid = register_code
    into reg;

    if not found then
        raise exception 'Реестр с номером % не найден', register_code;
    end if;

    if track_user then
        execute format('alter table %s add column if not exists CHANGE_USER_ID bigint;', reg.quant_table);
        update core_register set track_changes_userid = 'CHANGE_USER_ID' where registerid = reg.registerid;
    end if;

    if track_change_date then
        execute format('alter table %s add column if not exists CHANGE_DATE timestamp;', reg.quant_table);
        update core_register set track_changes_date = 'CHANGE_DATE' where registerid = reg.registerid;
    end if;

    if create_history_table then
        execute format('create table %s_a
        (
            id             bigint not null
                constraint %1$s_a_pkey
                    primary key,
            object_id      bigint not null,
            attribute_id   bigint not null,
            s              timestamp,
            po             timestamp,
            ref_item_id    bigint,
            text_value     varchar,
            number_value   numeric,
            date_value     timestamp,
            change_user_id bigint
        );

        alter table %1$s_a
            owner to cipjs_kad_ozenka;

        grant select on %1$s_a to test_role;

        create index %1$s_a_obj_attr_idx
        on %1$s_a (object_id, attribute_id);', reg.quant_table);
        update core_register set allpri_table = concat(reg.quant_table,'_A') where registerid = reg.registerid;
    end if;

    return reg.registerid;
end
$function$;

/*create_trigger_for_new_gbu_source_table_with_partition_by_data 62*/
CREATE OR REPLACE FUNCTION public.create_trigger_for_new_gbu_source_table_with_partition_by_data()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
DECLARE
	prefix text;
    table_name text;
    trigger_name text;
	prefixes text[3];

BEGIN
	prefixes := ARRAY ['txt', 'num', 'dt'];
    
	FOREACH prefix IN ARRAY prefixes
      LOOP
      	  table_name := NEW.allpri_table || '_' || prefix;
          trigger_name := 'trigger_for_' || table_name;
          EXECUTE 'DROP TRIGGER IF EXISTS ' || trigger_name || ' ON ' || table_name || ';' ||
                              'CREATE TRIGGER ' ||  trigger_name ||
                              ' AFTER INSERT 
                              ON ' || table_name || '
                              FOR EACH ROW
                              EXECUTE FUNCTION update_cache_table_for_data_composition_reports_for_registers();';

      END LOOP;
	
	RETURN NULL;
END;
$function$;

/*common_registers_with_soft_deletion_update_trg 63*/
CREATE OR REPLACE FUNCTION public.common_registers_with_soft_deletion_update_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
declare
	register_code bigint;
    reg core_register%rowtype;
	gbuAttributeInfoRow record;
	allpri_table_name text;
begin
		if TG_OP = 'DELETE' then register_code := OLD.register_id;
           else register_code := NEW.register_id;
         end if;
		 
		select *
		from core_register
		where registerid = register_code
		into reg;
		if not found then
			raise exception 'Реестр с номером % не найден', register_code;
		end if;
		
		if reg.storage_type=4 then 
			CASE TG_OP
			WHEN 'INSERT' THEN	
				execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_idx ON %1$s_deleted (EVENT_ID)', NEW.main_table_name, NEW.register_id);
			WHEN 'DELETE' THEN
			   execute format('DROP TABLE IF EXISTS %s_deleted', OLD.main_table_name);
			WHEN 'UPDATE' THEN
			   raise exception 'Операция обновления не поддерживается для таблицы common_registers_with_soft_deletion. Удалите и создайте запись заново';
			END CASE;
		elsif reg.storage_type=5 THEN
			CASE TG_OP
			WHEN 'INSERT' THEN	
				if reg.ALLPRI_PARTITIONING=1 then	
					allpri_table_name := concat(NEW.main_table_name, '_NUM');
					execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_num_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.register_id);
							   
				    allpri_table_name := concat(NEW.main_table_name, '_DT');
					execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_dt_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.register_id);
							   
				    allpri_table_name := concat(NEW.main_table_name, '_TXT');
					execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_txt_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.register_id);
				elsif reg.ALLPRI_PARTITIONING=2 THEN
					FOR gbuAttributeInfoRow IN
						select a.id from core_register_attribute a
						where a.registerid=register_code
							and (a.primary_key=0 or a.primary_key is null) 
							and (a.is_deleted=0 or a.is_deleted is null)
						order by a.id
					LOOP
						allpri_table_name := concat(NEW.main_table_name, '_', gbuAttributeInfoRow.id);
			   			execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (like %1$s including defaults, 
							   EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_%3$s_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.register_id, gbuAttributeInfoRow.id);
					END LOOP;
				else 
					raise exception 'Неподдерживаемый тип разбиения allpri: %', reg.ALLPRI_PARTITIONING; 
				end if;
				
			WHEN 'DELETE' THEN
				if reg.ALLPRI_PARTITIONING=1 then	
					allpri_table_name := concat(OLD.main_table_name, '_NUM');
			   		execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
					
					allpri_table_name := concat(OLD.main_table_name, '_DT');
			   		execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
					
					allpri_table_name := concat(OLD.main_table_name, '_TXT');
			   		execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
				elsif reg.ALLPRI_PARTITIONING=2 THEN
					FOR gbuAttributeInfoRow IN
						select a.id from core_register_attribute a
						where a.registerid=register_code
							and (a.primary_key=0 or a.primary_key is null) 
							and (a.is_deleted=0 or a.is_deleted is null)
						order by a.id
					LOOP
						allpri_table_name := concat(OLD.main_table_name, '_', gbuAttributeInfoRow.id);
			   			execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
					END LOOP;
				else 
					raise exception 'Неподдерживаемый тип разбиения allpri: %', reg.ALLPRI_PARTITIONING;
				end if;
			   
			WHEN 'UPDATE' THEN
			   raise exception 'Операция обновления не поддерживается для таблицы common_registers_with_soft_deletion. Удалите и создайте запись заново';
			END CASE;
			
		else 
			raise exception 'Реестр с типом хранения данных % не поддерживается', reg.storage_type;
        end if;
		
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END
$function$;

/*core_register_attr_sync_delete_table_trg 64*/
CREATE OR REPLACE FUNCTION public.core_register_attr_sync_delete_table_trg()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
declare
	register_code bigint;
    reg core_register%rowtype;
	reg_with_soft_deletion common_registers_with_soft_deletion%rowtype;
	command text;
	allpri_table_name text;
	value_type text;
	ref_item_col text;
begin
		if TG_OP = 'DELETE' then register_code := OLD.registerid;
           else register_code := NEW.registerid;
         end if;
		 
		select *
		from core_register
		where registerid = register_code
		into reg;
		if not found then
			raise exception 'Реестр с номером % не найден', register_code;
		end if;
		
		
			if TG_OP='INSERT' THEN
				select *
				from common_registers_with_soft_deletion
				where register_id = register_code
				into reg_with_soft_deletion;

				if not found then
					return NEW;
				end if;
				
				if reg.storage_type=4 then
					if NEW.value_field is not null and NEW.value_field <> '' then
						command := format('ALTER TABLE %1$s_deleted ADD COLUMN IF NOT EXISTS %2$s ', reg_with_soft_deletion.main_table_name, NEW.value_field);

						CASE NEW.type
							WHEN 1 THEN
								command := concat(command,'  BIGINT');
							WHEN 2 THEN
								command := concat(command,'  NUMERIC');
							WHEN 3 THEN
								command := concat(command,'  SMALLINT');
							WHEN 4 THEN
								command := concat(command,'  VARCHAR(250)');
							WHEN 5 THEN
								command := concat(command,'  TIMESTAMP');
							WHEN 6 THEN
								command := concat(command,'  BYTEA');
							ELSE raise exception 'Неподдерживаемый тип: % (ИД атрибута %)', NEW.type, NEW.id;
						END CASE;

						if NEW.is_nullable is null or NEW.is_nullable<>1 then
							command := concat(command,'  NOT NULL');
						end if;
						execute command;
					end if;

					if NEW.code_field is not null and NEW.code_field <> '' then
						command := format('ALTER TABLE %s_deleted ADD COLUMN IF NOT EXISTS %2$s BIGINT', reg_with_soft_deletion.main_table_name, NEW.code_field);
						if NEW.is_nullable is null or NEW.is_nullable<>1 then
							command := concat(command,'  NOT NULL');
						end if;

						execute command;
					end if;
				elsif reg.storage_type=5 THEN
					if reg.ALLPRI_PARTITIONING=1 then
						allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_NUM');
						execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
								   (id bigint NOT NULL,
									object_id bigint NOT NULL,
									attribute_id bigint NOT NULL,
									ot timestamp without time zone NOT NULL,
									s timestamp without time zone NOT NULL,
									value numeric, 
									change_id bigint,
									change_date timestamp without time zone NOT NULL,
									change_user_id bigint NOT NULL,
									change_doc_id bigint,
								   EVENT_ID bigint not null);
								   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_num_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.registerid);
						
						allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_DT');
						execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
								   (id bigint NOT NULL,
									object_id bigint NOT NULL,
									attribute_id bigint NOT NULL,
									ot timestamp without time zone NOT NULL,
									s timestamp without time zone NOT NULL,
									value timestamp without time zone, 
									change_id bigint,
									change_date timestamp without time zone NOT NULL,
									change_user_id bigint NOT NULL,
									change_doc_id bigint,
								   EVENT_ID bigint not null);
								   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_dt_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.registerid);
								   
						allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_TXT');
						execute format('CREATE TABLE IF NOT EXISTS %s_deleted 
								   (id bigint NOT NULL,
									object_id bigint NOT NULL,
									attribute_id bigint NOT NULL,
									ot timestamp without time zone NOT NULL,
									s timestamp without time zone NOT NULL,
									ref_item_id bigint,
    								value character varying(4000),
									change_id bigint,
									change_date timestamp without time zone NOT NULL,
									change_user_id bigint NOT NULL,
									change_doc_id bigint,
								   EVENT_ID bigint not null);
								   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_txt_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.registerid);

					elsif reg.ALLPRI_PARTITIONING=2 THEN
						allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_', NEW.id);
						ref_item_col := '';
						CASE NEW.type
							WHEN 1 THEN
								value_type := 'BIGINT';
							WHEN 2 THEN
								value_type := 'NUMERIC';
							WHEN 3 THEN
								value_type := 'SMALLINT';
							WHEN 4 THEN
								value_type := 'VARCHAR(4000)';
								ref_item_col := 'ref_item_id bigint,';
							WHEN 5 THEN
								value_type := 'timestamp without time zone';
							WHEN 6 THEN
								value_type := 'BYTEA';
						END CASE;
						
						command := format('CREATE TABLE IF NOT EXISTS %s_deleted 
							   (id bigint NOT NULL,
								object_id bigint NOT NULL,
								ot timestamp without time zone NOT NULL,
								s timestamp without time zone NOT NULL,
								value %3$s,
								%4$s	
								change_date timestamp without time zone NOT NULL,
								change_user_id bigint NOT NULL,
								change_doc_id bigint, 
							    EVENT_ID bigint not null);
							   CREATE INDEX IF NOT EXISTS deleted_event_id_%2$s_%5$s_idx ON %1$s_deleted (EVENT_ID)', allpri_table_name, NEW.registerid, value_type, ref_item_col, NEW.id);
						execute	command;
					end if;
				end if;

			elsif TG_OP='UPDATE' THEN
				select *
				from common_registers_with_soft_deletion
				where register_id = register_code
				into reg_with_soft_deletion;
				if  not found then
					return NEW;
				end if;
				
				if reg.storage_type=4 then 
					if (NEW.value_field is null and OLD.value_field is not null) or (NEW.value_field is not null and OLD.value_field is null) or  NEW.value_field <> OLD.value_field then
						if(OLD.value_field is not null and OLD.value_field<>'') then
							command := format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS %2$s ', reg_with_soft_deletion.main_table_name, OLD.value_field);
							execute command;
						end if;
						if NEW.value_field is not null and NEW.value_field <> '' then
							command := format('ALTER TABLE %s_deleted ADD COLUMN IF NOT EXISTS %2$s ', reg_with_soft_deletion.main_table_name, NEW.value_field);

							CASE NEW.type
								WHEN 1 THEN
									command := concat(command,'  BIGINT');
								WHEN 2 THEN
									command := concat(command,'  NUMERIC');
								WHEN 3 THEN
									command := concat(command,'  SMALLINT');
								WHEN 4 THEN
									command := concat(command,'  VARCHAR(255)');
								WHEN 5 THEN
									command := concat(command,'  TIMESTAMP');
								WHEN 6 THEN
									command := concat(command,'  BYTEA');
								ELSE raise exception 'Неподдерживаемый тип: % (ИД атрибута %)', NEW.type, NEW.id;
							END CASE;

							if NEW.is_nullable is null or NEW.is_nullable<>1 then
								command := concat(command,'  NOT NULL');
							end if;

							execute command;
						end if;
					else
						if NEW.value_field is not null and NEW.value_field <> '' then
							if NEW.type<>OLD.type then
								command := format('ALTER TABLE %s_deleted ALTER COLUMN %2$s ', reg_with_soft_deletion.main_table_name, NEW.value_field);
								CASE NEW.type
										WHEN 1 THEN
											command := concat(command,'TYPE  BIGINT USING ', NEW.value_field,'::bigint');
										WHEN 2 THEN
											command := concat(command,'TYPE  NUMERIC USING ', NEW.value_field,'::NUMERIC');
										WHEN 3 THEN
											command := concat(command,'TYPE  SMALLINT USING ', NEW.value_field,'::SMALLINT');
										WHEN 4 THEN
											command := concat(command,'TYPE  VARCHAR(255) USING ', NEW.value_field,'::VARCHAR(255)');
										WHEN 5 THEN
											command := concat(command,'TYPE  TIMESTAMP USING ', NEW.value_field,'::timestamp without time zone');
										WHEN 6 THEN
											command := concat(command,'TYPE  BYTEA USING ', NEW.value_field,'::BYTEA');
										ELSE raise exception 'Неподдерживаемый тип: % (ИД атрибута %)', NEW.type, NEW.id;
								END CASE;
								execute command;
							end if;

							if (NEW.is_nullable is null and OLD.is_nullable is not null) or (NEW.is_nullable is not null and OLD.is_nullable is null) or NEW.is_nullable<>OLD.is_nullable then
								command := format('ALTER TABLE %s_deleted ALTER COLUMN %2$s ', reg_with_soft_deletion.main_table_name, NEW.value_field);
								if (NEW.is_nullable is null or NEW.is_nullable<>1) and OLD.is_nullable=1 then
									command := concat(command,' SET NOT NULL');
								end if;
								if NEW.is_nullable=1 and (OLD.is_nullable is null or OLD.is_nullable<>1) then
									command := concat(command,' DROP NOT NULL');
								end if;

								execute command;
							end if;
						end if;
					end if;

					if (NEW.code_field is null and OLD.code_field is not null) or (NEW.code_field is not null and OLD.code_field is null) or NEW.code_field <> OLD.code_field then
						if(OLD.code_field is not null and OLD.code_field<>'') then
							command := format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS %2$s ', reg_with_soft_deletion.main_table_name, OLD.code_field);
							execute command;
						end if;

						if NEW.code_field is not null and NEW.code_field <> '' then
							command := format('ALTER TABLE %s_deleted ADD COLUMN IF NOT EXISTS %2$s BIGINT', reg_with_soft_deletion.main_table_name, NEW.code_field);
							if NEW.is_nullable is null or NEW.is_nullable<>1 then
								command := concat(command,'  NOT NULL');
							end if;

							execute command;
						end if;
					else
						if NEW.code_field is not null and NEW.code_field <> '' then
							if (NEW.is_nullable is null and OLD.is_nullable is not null) or (NEW.is_nullable is not null and OLD.is_nullable is null) or NEW.is_nullable<>OLD.is_nullable then
								command := format('ALTER TABLE %s_deleted ALTER COLUMN %2$s ', reg_with_soft_deletion.main_table_name, NEW.code_field);
								if NEW.is_nullable is null or NEW.is_nullable<>1 then
									command := concat(command,' SET NOT NULL');
								end if;
								if NEW.is_nullable=1 and (OLD.is_nullable is null or OLD.is_nullable<>1) then
									command := concat(command,' DROP NOT NULL');
								end if;

								execute command;
							end if;
						end if;
					end if;
				elsif reg.storage_type=5 and reg.ALLPRI_PARTITIONING=2 and NEW.type<>OLD.type THEN
					allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_', NEW.id);
					command := format('ALTER TABLE %s_deleted ALTER COLUMN value ', allpri_table_name);
					CASE NEW.type
							WHEN 1 THEN
								command := concat(command,'TYPE  BIGINT USING value::bigint');
							WHEN 2 THEN
								command := concat(command,'TYPE  NUMERIC USING value::NUMERIC');
							WHEN 3 THEN
								command := concat(command,'TYPE  SMALLINT USING value::SMALLINT');
							WHEN 4 THEN
								command := concat(command,'TYPE  VARCHAR(4000) USING value::VARCHAR(255)');
							WHEN 5 THEN
								command := concat(command,'TYPE  TIMESTAMP USING value::timestamp without time zone');
							WHEN 6 THEN
								command := concat(command,'TYPE  BYTEA USING value::BYTEA');
							ELSE raise exception 'Неподдерживаемый тип: % (ИД атрибута %)', NEW.type, NEW.id;
					END CASE;
					execute command;
					if NEW.type=4 then
						execute format('ALTER TABLE %s_deleted ADD COLUMN ref_item_id bigint', allpri_table_name);
					end if;
					if OLD.type=4 then
						execute format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS ref_item_id', allpri_table_name);
					end if;
								
				end if;
				
			else	
				select *
				from common_registers_with_soft_deletion
				where register_id = register_code
				into reg_with_soft_deletion;
				if not found then
					return OLD;
				end if;
				
				if reg.storage_type=4 then 
					if OLD.value_field is not null and OLD.value_field <> '' then
						command := format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS %2$s', reg_with_soft_deletion.main_table_name, OLD.value_field);					
						execute command;
					end if;

					if OLD.code_field is not null and OLD.code_field <> '' then
						command := format('ALTER TABLE %s_deleted DROP COLUMN IF EXISTS %2$s', reg_with_soft_deletion.main_table_name, OLD.code_field);					
						execute command;
					end if;	
				elsif reg.storage_type=5 and reg.ALLPRI_PARTITIONING=2 then
					allpri_table_name := concat(reg_with_soft_deletion.main_table_name, '_', OLD.id);
					execute format('DROP TABLE IF EXISTS %s_deleted', allpri_table_name);
				end if;
		END IF;
		 
       
         if TG_OP = 'DELETE' then return OLD;
           else return NEW;
         end if;
       
       END
$function$;
