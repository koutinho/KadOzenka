
DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('COMISSION_COST')) then
		execute 'create table COMISSION_COST ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'id')) then
        execute 'alter table COMISSION_COST add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'type_commission')) then
        execute 'alter table COMISSION_COST add "type_commission" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'kn_object')) then
        execute 'alter table COMISSION_COST add "kn_object" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'kc_object')) then
        execute 'alter table COMISSION_COST add "kc_object" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'date_kc')) then
        execute 'alter table COMISSION_COST add "date_kc" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'num_statement')) then
        execute 'alter table COMISSION_COST add "num_statement" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'date_statement')) then
        execute 'alter table COMISSION_COST add "date_statement" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'status_applicant')) then
        execute 'alter table COMISSION_COST add "status_applicant" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'num_decision')) then
        execute 'alter table COMISSION_COST add "num_decision" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'date_decision')) then
        execute 'alter table COMISSION_COST add "date_decision" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'result_decision')) then
        execute 'alter table COMISSION_COST add "result_decision" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'market_value')) then
        execute 'alter table COMISSION_COST add "market_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'kc_commission')) then
        execute 'alter table COMISSION_COST add "kc_commission" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'group_commission')) then
        execute 'alter table COMISSION_COST add "group_commission" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'change_commission')) then
        execute 'alter table COMISSION_COST add "change_commission" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'change_user_id')) then
        execute 'alter table COMISSION_COST add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST', 'change_date')) then
        execute 'alter table COMISSION_COST add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_400_q_pk')) then
    execute 'alter table COMISSION_COST add constraint reg_400_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('COMMON_EXPORT_BY_TEMPLATES')) then
		execute 'create table COMMON_EXPORT_BY_TEMPLATES ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'id')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'user_id')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'status')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'date_created')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "date_created" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'template_file_name')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "template_file_name" VARCHAR(512) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'columns_mapping')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "columns_mapping" VARCHAR NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'error_id')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "error_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'date_started')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "date_started" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'date_finished')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "date_finished" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'result_message')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "result_message" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'main_register_id')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "main_register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'register_view_id')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "register_view_id" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_800_q_pk')) then
    execute 'alter table COMMON_EXPORT_BY_TEMPLATES add constraint reg_800_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('COMMON_IMPORT_DATA_LOG')) then
		execute 'create table COMMON_IMPORT_DATA_LOG ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'id')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'user_id')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'status')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'date_created')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "date_created" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'data_file_name')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "data_file_name" VARCHAR(512) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'columns_mapping')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "columns_mapping" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'error_id')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "error_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'date_started')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "date_started" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'date_finished')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "date_finished" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'result_message')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "result_message" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'main_register_id')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "main_register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'register_view_id')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "register_view_id" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_801_q_pk')) then
    execute 'alter table COMMON_IMPORT_DATA_LOG add constraint reg_801_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT')) then
		execute 'create table CORE_ATTACHMENT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'id')) then
        execute 'alter table CORE_ATTACHMENT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'doc_number')) then
        execute 'alter table CORE_ATTACHMENT add "doc_number" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'description')) then
        execute 'alter table CORE_ATTACHMENT add "description" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'barcode')) then
        execute 'alter table CORE_ATTACHMENT add "barcode" VARCHAR(32)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'doc_type')) then
        execute 'alter table CORE_ATTACHMENT add "doc_type" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'doc_type_id')) then
        execute 'alter table CORE_ATTACHMENT add "doc_type_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'photo_type')) then
        execute 'alter table CORE_ATTACHMENT add "photo_type" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'photo_type_id')) then
        execute 'alter table CORE_ATTACHMENT add "photo_type_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'created_by_id')) then
        execute 'alter table CORE_ATTACHMENT add "created_by_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'created_date')) then
        execute 'alter table CORE_ATTACHMENT add "created_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'is_deleted')) then
        execute 'alter table CORE_ATTACHMENT add "is_deleted" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'deleted_by_id')) then
        execute 'alter table CORE_ATTACHMENT add "deleted_by_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'deleted_date')) then
        execute 'alter table CORE_ATTACHMENT add "deleted_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_986_quant_pk')) then
    execute 'alter table CORE_ATTACHMENT add constraint reg_986_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT_FILE')) then
		execute 'create table CORE_ATTACHMENT_FILE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'id')) then
        execute 'alter table CORE_ATTACHMENT_FILE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'attachment_id')) then
        execute 'alter table CORE_ATTACHMENT_FILE add "attachment_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'filename')) then
        execute 'alter table CORE_ATTACHMENT_FILE add "filename" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'mimetype')) then
        execute 'alter table CORE_ATTACHMENT_FILE add "mimetype" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'page')) then
        execute 'alter table CORE_ATTACHMENT_FILE add "page" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'is_main')) then
        execute 'alter table CORE_ATTACHMENT_FILE add "is_main" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'file_data')) then
        execute 'alter table CORE_ATTACHMENT_FILE add "file_data" BYTEA';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'file_data_small')) then
        execute 'alter table CORE_ATTACHMENT_FILE add "file_data_small" BYTEA';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_987_quant_pk')) then
    execute 'alter table CORE_ATTACHMENT_FILE add constraint reg_987_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_ATTACHMENT_OBJECT')) then
		execute 'create table CORE_ATTACHMENT_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'id')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'attachment_id')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "attachment_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'register_id')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'object_id')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'is_deleted')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "is_deleted" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'deleted_by_id')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "deleted_by_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'deleted_date')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "deleted_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'is_main')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "is_main" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'created_by_id')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "created_by_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'created_date')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add "created_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_988_quant_pk')) then
    execute 'alter table CORE_ATTACHMENT_OBJECT add constraint reg_988_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_CACHE_UPDATES')) then
		execute 'create table CORE_CACHE_UPDATES ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'id')) then
        execute 'alter table CORE_CACHE_UPDATES add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'cacheobject')) then
        execute 'alter table CORE_CACHE_UPDATES add "cacheobject" VARCHAR(50) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'cachekey')) then
        execute 'alter table CORE_CACHE_UPDATES add "cachekey" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'extradata')) then
        execute 'alter table CORE_CACHE_UPDATES add "extradata" VARCHAR(200) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'cache_timestamp')) then
        execute 'alter table CORE_CACHE_UPDATES add "cache_timestamp" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_996_quant_pk')) then
    execute 'alter table CORE_CACHE_UPDATES add constraint reg_996_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_CONFIGPARAM')) then
		execute 'create table CORE_CONFIGPARAM ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'id')) then
        execute 'alter table CORE_CONFIGPARAM add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'parentkey')) then
        execute 'alter table CORE_CONFIGPARAM add "parentkey" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'childkey')) then
        execute 'alter table CORE_CONFIGPARAM add "childkey" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'xmldata')) then
        execute 'alter table CORE_CONFIGPARAM add "xmldata" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'description')) then
        execute 'alter table CORE_CONFIGPARAM add "description" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'chdate')) then
        execute 'alter table CORE_CONFIGPARAM add "chdate" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'userid')) then
        execute 'alter table CORE_CONFIGPARAM add "userid" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_978_quant_pk')) then
    execute 'alter table CORE_CONFIGPARAM add constraint reg_978_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_DIAGNOSTICS')) then
		execute 'create table CORE_DIAGNOSTICS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'id')) then
        execute 'alter table CORE_DIAGNOSTICS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'user_id')) then
        execute 'alter table CORE_DIAGNOSTICS add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'module')) then
        execute 'alter table CORE_DIAGNOSTICS add "module" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'method')) then
        execute 'alter table CORE_DIAGNOSTICS add "method" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'extra_key')) then
        execute 'alter table CORE_DIAGNOSTICS add "extra_key" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'action_date')) then
        execute 'alter table CORE_DIAGNOSTICS add "action_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'execution_duration')) then
        execute 'alter table CORE_DIAGNOSTICS add "execution_duration" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'action_descr')) then
        execute 'alter table CORE_DIAGNOSTICS add "action_descr" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'callstack')) then
        execute 'alter table CORE_DIAGNOSTICS add "callstack" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'callstack_clob')) then
        execute 'alter table CORE_DIAGNOSTICS add "callstack_clob" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_992_quant_pk')) then
    execute 'alter table CORE_DIAGNOSTICS add constraint reg_992_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_ERROR_LOG')) then
		execute 'create table CORE_ERROR_LOG ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'id')) then
        execute 'alter table CORE_ERROR_LOG add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'userid')) then
        execute 'alter table CORE_ERROR_LOG add "userid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'message')) then
        execute 'alter table CORE_ERROR_LOG add "message" VARCHAR(2000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'errordate')) then
        execute 'alter table CORE_ERROR_LOG add "errordate" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'errorpage_shown')) then
        execute 'alter table CORE_ERROR_LOG add "errorpage_shown" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'msgtype')) then
        execute 'alter table CORE_ERROR_LOG add "msgtype" VARCHAR(10)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'params_short')) then
        execute 'alter table CORE_ERROR_LOG add "params_short" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'params_clob')) then
        execute 'alter table CORE_ERROR_LOG add "params_clob" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'callstack')) then
        execute 'alter table CORE_ERROR_LOG add "callstack" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'callstack_clob')) then
        execute 'alter table CORE_ERROR_LOG add "callstack_clob" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'exception_type')) then
        execute 'alter table CORE_ERROR_LOG add "exception_type" VARCHAR(1024)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_989_quant_pk')) then
    execute 'alter table CORE_ERROR_LOG add constraint reg_989_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('core_error_log_idx')) then
	execute 'CREATE  INDEX core_error_log_idx on CORE_ERROR_LOG (errordate)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_HOLIDAYS')) then
		execute 'create table CORE_HOLIDAYS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_HOLIDAYS', 'id')) then
        execute 'alter table CORE_HOLIDAYS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_HOLIDAYS', 'value')) then
        execute 'alter table CORE_HOLIDAYS add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_998_quant_pk')) then
    execute 'alter table CORE_HOLIDAYS add constraint reg_998_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT')) then
		execute 'create table CORE_LAYOUT ("layoutid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'layoutid')) then
        execute 'alter table CORE_LAYOUT add "layoutid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'layoutname')) then
        execute 'alter table CORE_LAYOUT add "layoutname" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'layoutcomment')) then
        execute 'alter table CORE_LAYOUT add "layoutcomment" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'registerid')) then
        execute 'alter table CORE_LAYOUT add "registerid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'defaultsort')) then
        execute 'alter table CORE_LAYOUT add "defaultsort" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'preffered')) then
        execute 'alter table CORE_LAYOUT add "preffered" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'username')) then
        execute 'alter table CORE_LAYOUT add "username" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'createdate')) then
        execute 'alter table CORE_LAYOUT add "createdate" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'qsquery')) then
        execute 'alter table CORE_LAYOUT add "qsquery" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'isdistinct')) then
        execute 'alter table CORE_LAYOUT add "isdistinct" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'ordertype')) then
        execute 'alter table CORE_LAYOUT add "ordertype" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'user_id')) then
        execute 'alter table CORE_LAYOUT add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'iscommon')) then
        execute 'alter table CORE_LAYOUT add "iscommon" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'internal_name')) then
        execute 'alter table CORE_LAYOUT add "internal_name" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'enable_minicards_mode')) then
        execute 'alter table CORE_LAYOUT add "enable_minicards_mode" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'register_view_id')) then
        execute 'alter table CORE_LAYOUT add "register_view_id" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'as_domain_id')) then
        execute 'alter table CORE_LAYOUT add "as_domain_id" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'column_width_type')) then
        execute 'alter table CORE_LAYOUT add "column_width_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'is_using_extended_editor')) then
        execute 'alter table CORE_LAYOUT add "is_using_extended_editor" SMALLINT DEFAULT 0';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_933_quant_pk')) then
    execute 'alter table CORE_LAYOUT add constraint reg_933_quant_pk primary key (layoutid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_COLUMN_TYPE')) then
		execute 'create table CORE_LAYOUT_COLUMN_TYPE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_COLUMN_TYPE', 'id')) then
        execute 'alter table CORE_LAYOUT_COLUMN_TYPE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_COLUMN_TYPE', 'code')) then
        execute 'alter table CORE_LAYOUT_COLUMN_TYPE add "code" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_COLUMN_TYPE', 'name')) then
        execute 'alter table CORE_LAYOUT_COLUMN_TYPE add "name" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_924_quant_pk')) then
    execute 'alter table CORE_LAYOUT_COLUMN_TYPE add constraint reg_924_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_DETAILS')) then
		execute 'create table CORE_LAYOUT_DETAILS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'id')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'layoutid')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "layoutid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'detailtype')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "detailtype" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'ordinal')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "ordinal" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'attributeid')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "attributeid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'sortbyattribute')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "sortbyattribute" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'referenceid')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "referenceid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'headertext')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "headertext" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'headerwidth')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "headerwidth" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'visible')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "visible" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'format')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "format" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'datatype')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "datatype" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'expression')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "expression" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'sqlexpression')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "sqlexpression" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'totaltext')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "totaltext" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'totaltype')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "totaltype" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'style')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "style" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'enablestyle')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "enablestyle" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'textalign')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "textalign" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'qscolumn')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "qscolumn" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'export_column_name')) then
        execute 'alter table CORE_LAYOUT_DETAILS add "export_column_name" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_935_quant_pk')) then
    execute 'alter table CORE_LAYOUT_DETAILS add constraint reg_935_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LAYOUT_EXPORT')) then
		execute 'create table CORE_LAYOUT_EXPORT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'id')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'layout_id')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "layout_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'user_id')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'start_date')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "start_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'end_date')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "end_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'status')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'result_message')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "result_message" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'file_location')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "file_location" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'rows_count')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "rows_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'qs_query')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "qs_query" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'type')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "type" VARCHAR(10)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'register_view_id')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "register_view_id" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'parameters')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "parameters" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_956_quant_pk')) then
    execute 'alter table CORE_LAYOUT_EXPORT add constraint reg_956_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LIST')) then
		execute 'create table CORE_LIST ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'id')) then
        execute 'alter table CORE_LIST add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'name')) then
        execute 'alter table CORE_LIST add "name" VARCHAR(500) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'register_view_id')) then
        execute 'alter table CORE_LIST add "register_view_id" VARCHAR(200) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'register_id')) then
        execute 'alter table CORE_LIST add "register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'author')) then
        execute 'alter table CORE_LIST add "author" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'iscommon')) then
        execute 'alter table CORE_LIST add "iscommon" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'list_comment')) then
        execute 'alter table CORE_LIST add "list_comment" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'change_date')) then
        execute 'alter table CORE_LIST add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_920_quant_pk')) then
    execute 'alter table CORE_LIST add constraint reg_920_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LIST_OBJECT')) then
		execute 'create table CORE_LIST_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST_OBJECT', 'id')) then
        execute 'alter table CORE_LIST_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST_OBJECT', 'list_id')) then
        execute 'alter table CORE_LIST_OBJECT add "list_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LIST_OBJECT', 'object_id')) then
        execute 'alter table CORE_LIST_OBJECT add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_921_quant_pk')) then
    execute 'alter table CORE_LIST_OBJECT add constraint reg_921_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_LOG')) then
		execute 'create table CORE_LONG_PROCESS_LOG ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'id')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'exe_info')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add "exe_info" VARCHAR(1024)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'start_date')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add "start_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'last_check_date')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add "last_check_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'status')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'user_id')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_977_quant_pk')) then
    execute 'alter table CORE_LONG_PROCESS_LOG add constraint reg_977_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_QUEUE')) then
		execute 'create table CORE_LONG_PROCESS_QUEUE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'id')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'user_id')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'process_type_id')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "process_type_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'object_register_id')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "object_register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'object_id')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'create_date')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "create_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'start_date')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "start_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'end_date')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "end_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'status')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'last_check_date')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "last_check_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'error_id')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "error_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'message')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "message" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'service_log_id')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "service_log_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'log')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "log" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'progress')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "progress" BIGINT DEFAULT 0';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_975_quant_pk')) then
    execute 'alter table CORE_LONG_PROCESS_QUEUE add constraint reg_975_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_LONG_PROCESS_TYPE')) then
		execute 'create table CORE_LONG_PROCESS_TYPE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'id')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'process_name')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "process_name" VARCHAR(256) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'class_name')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "class_name" VARCHAR(512) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'schedule_type')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "schedule_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'repeat_interval')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "repeat_interval" VARCHAR(256)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'enabled')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "enabled" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'run_count')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "run_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'failure_count')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "failure_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'last_start_date')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "last_start_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'last_run_duration')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "last_run_duration" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'next_run_date')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "next_run_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'parameters')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "parameters" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'description')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "description" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'test_result')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "test_result" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_976_quant_pk')) then
    execute 'alter table CORE_LONG_PROCESS_TYPE add constraint reg_976_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_MESSAGES')) then
		execute 'create table CORE_MESSAGES ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'id')) then
        execute 'alter table CORE_MESSAGES add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'user_id')) then
        execute 'alter table CORE_MESSAGES add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'subject')) then
        execute 'alter table CORE_MESSAGES add "subject" VARCHAR(250) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'message')) then
        execute 'alter table CORE_MESSAGES add "message" VARCHAR(4000) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'was_sended')) then
        execute 'alter table CORE_MESSAGES add "was_sended" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'field_to')) then
        execute 'alter table CORE_MESSAGES add "field_to" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'is_email_message')) then
        execute 'alter table CORE_MESSAGES add "is_email_message" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'is_urgent')) then
        execute 'alter table CORE_MESSAGES add "is_urgent" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES', 'urgent_expire_date')) then
        execute 'alter table CORE_MESSAGES add "urgent_expire_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('core_messages_pk')) then
    execute 'alter table CORE_MESSAGES add constraint core_messages_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_MESSAGES_TO')) then
		execute 'create table CORE_MESSAGES_TO ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES_TO', 'id')) then
        execute 'alter table CORE_MESSAGES_TO add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES_TO', 'message_id')) then
        execute 'alter table CORE_MESSAGES_TO add "message_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES_TO', 'user_id')) then
        execute 'alter table CORE_MESSAGES_TO add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES_TO', 'was_readed')) then
        execute 'alter table CORE_MESSAGES_TO add "was_readed" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES_TO', 'was_deleted')) then
        execute 'alter table CORE_MESSAGES_TO add "was_deleted" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES_TO', 'send_result')) then
        execute 'alter table CORE_MESSAGES_TO add "send_result" VARCHAR(1024)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_MESSAGES_TO', 'is_delivered')) then
        execute 'alter table CORE_MESSAGES_TO add "is_delivered" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('core_messages_to_pk')) then
    execute 'alter table CORE_MESSAGES_TO add constraint core_messages_to_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY')) then
		execute 'create table CORE_QRY ("qryid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'qryid')) then
        execute 'alter table CORE_QRY add "qryid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'name')) then
        execute 'alter table CORE_QRY add "name" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'description')) then
        execute 'alter table CORE_QRY add "description" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'datefrom')) then
        execute 'alter table CORE_QRY add "datefrom" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'inlist')) then
        execute 'alter table CORE_QRY add "inlist" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'qry_user')) then
        execute 'alter table CORE_QRY add "qry_user" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'registerid')) then
        execute 'alter table CORE_QRY add "registerid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'qscondition')) then
        execute 'alter table CORE_QRY add "qscondition" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'user_id')) then
        execute 'alter table CORE_QRY add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'iscommon')) then
        execute 'alter table CORE_QRY add "iscommon" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'internal_name')) then
        execute 'alter table CORE_QRY add "internal_name" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'register_view_id')) then
        execute 'alter table CORE_QRY add "register_view_id" VARCHAR(200) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'author')) then
        execute 'alter table CORE_QRY add "author" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'is_using_extended_editor')) then
        execute 'alter table CORE_QRY add "is_using_extended_editor" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_936_quant_pk')) then
    execute 'alter table CORE_QRY add constraint reg_936_quant_pk primary key (qryid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY_FILTER')) then
		execute 'create table CORE_QRY_FILTER ("qryfilterid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'qryfilterid')) then
        execute 'alter table CORE_QRY_FILTER add "qryfilterid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'qryid')) then
        execute 'alter table CORE_QRY_FILTER add "qryid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'qryoperationid')) then
        execute 'alter table CORE_QRY_FILTER add "qryoperationid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'kindelementid')) then
        execute 'alter table CORE_QRY_FILTER add "kindelementid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'condition')) then
        execute 'alter table CORE_QRY_FILTER add "condition" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'andtrueorfalse')) then
        execute 'alter table CORE_QRY_FILTER add "andtrueorfalse" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'qryposition')) then
        execute 'alter table CORE_QRY_FILTER add "qryposition" BIGINT DEFAULT 0';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'value')) then
        execute 'alter table CORE_QRY_FILTER add "value" VARCHAR(120) DEFAULT ''''''''''''::character varying';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'byref')) then
        execute 'alter table CORE_QRY_FILTER add "byref" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'bracketsfirst')) then
        execute 'alter table CORE_QRY_FILTER add "bracketsfirst" VARCHAR(10) DEFAULT ''''''''''''::character varying';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'bracketsclose')) then
        execute 'alter table CORE_QRY_FILTER add "bracketsclose" VARCHAR(10) DEFAULT ''''''''''''::character varying';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'referenceid')) then
        execute 'alter table CORE_QRY_FILTER add "referenceid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'specialregisterid')) then
        execute 'alter table CORE_QRY_FILTER add "specialregisterid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'specialattributetype')) then
        execute 'alter table CORE_QRY_FILTER add "specialattributetype" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_937_quant_pk')) then
    execute 'alter table CORE_QRY_FILTER add constraint reg_937_quant_pk primary key (qryfilterid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_QRY_OPERATION')) then
		execute 'create table CORE_QRY_OPERATION ("qryoperationid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_OPERATION', 'qryoperationid')) then
        execute 'alter table CORE_QRY_OPERATION add "qryoperationid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_OPERATION', 'description')) then
        execute 'alter table CORE_QRY_OPERATION add "description" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_QRY_OPERATION', 'sqlstatement')) then
        execute 'alter table CORE_QRY_OPERATION add "sqlstatement" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_938_quant_pk')) then
    execute 'alter table CORE_QRY_OPERATION add constraint reg_938_quant_pk primary key (qryoperationid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE')) then
		execute 'create table CORE_REFERENCE ("referenceid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'referenceid')) then
        execute 'alter table CORE_REFERENCE add "referenceid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'description')) then
        execute 'alter table CORE_REFERENCE add "description" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'readonly')) then
        execute 'alter table CORE_REFERENCE add "readonly" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'progid')) then
        execute 'alter table CORE_REFERENCE add "progid" VARCHAR(60)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'istree')) then
        execute 'alter table CORE_REFERENCE add "istree" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'defaultvalue')) then
        execute 'alter table CORE_REFERENCE add "defaultvalue" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'name')) then
        execute 'alter table CORE_REFERENCE add "name" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'register_id')) then
        execute 'alter table CORE_REFERENCE add "register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'simple_values')) then
        execute 'alter table CORE_REFERENCE add "simple_values" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_982_quant_pk')) then
    execute 'alter table CORE_REFERENCE add constraint reg_982_quant_pk primary key (referenceid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_ITEM')) then
		execute 'create table CORE_REFERENCE_ITEM ("itemid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'itemid')) then
        execute 'alter table CORE_REFERENCE_ITEM add "itemid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'referenceid')) then
        execute 'alter table CORE_REFERENCE_ITEM add "referenceid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'code')) then
        execute 'alter table CORE_REFERENCE_ITEM add "code" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'value')) then
        execute 'alter table CORE_REFERENCE_ITEM add "value" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'short_title')) then
        execute 'alter table CORE_REFERENCE_ITEM add "short_title" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'is_archives')) then
        execute 'alter table CORE_REFERENCE_ITEM add "is_archives" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'user_name')) then
        execute 'alter table CORE_REFERENCE_ITEM add "user_name" VARCHAR(150)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'date_end_change')) then
        execute 'alter table CORE_REFERENCE_ITEM add "date_end_change" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'date_s')) then
        execute 'alter table CORE_REFERENCE_ITEM add "date_s" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'flag')) then
        execute 'alter table CORE_REFERENCE_ITEM add "flag" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'name')) then
        execute 'alter table CORE_REFERENCE_ITEM add "name" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_983_quant_pk')) then
    execute 'alter table CORE_REFERENCE_ITEM add constraint reg_983_quant_pk primary key (itemid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_RELATION')) then
		execute 'create table CORE_REFERENCE_RELATION ("relid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'relid')) then
        execute 'alter table CORE_REFERENCE_RELATION add "relid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'parentkey')) then
        execute 'alter table CORE_REFERENCE_RELATION add "parentkey" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'childkey')) then
        execute 'alter table CORE_REFERENCE_RELATION add "childkey" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'parentref')) then
        execute 'alter table CORE_REFERENCE_RELATION add "parentref" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'childref')) then
        execute 'alter table CORE_REFERENCE_RELATION add "childref" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'parentreq')) then
        execute 'alter table CORE_REFERENCE_RELATION add "parentreq" BIGINT DEFAULT 0';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'treelevel')) then
        execute 'alter table CORE_REFERENCE_RELATION add "treelevel" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_984_quant_pk')) then
    execute 'alter table CORE_REFERENCE_RELATION add constraint reg_984_quant_pk primary key (relid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_TREE')) then
		execute 'create table CORE_REFERENCE_TREE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'id')) then
        execute 'alter table CORE_REFERENCE_TREE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'code')) then
        execute 'alter table CORE_REFERENCE_TREE add "code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'parentid')) then
        execute 'alter table CORE_REFERENCE_TREE add "parentid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'childid')) then
        execute 'alter table CORE_REFERENCE_TREE add "childid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'referenceid')) then
        execute 'alter table CORE_REFERENCE_TREE add "referenceid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'level')) then
        execute 'alter table CORE_REFERENCE_TREE add "level" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'cldreferenceid')) then
        execute 'alter table CORE_REFERENCE_TREE add "cldreferenceid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'adress_type')) then
        execute 'alter table CORE_REFERENCE_TREE add "adress_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_985_quant_pk')) then
    execute 'alter table CORE_REFERENCE_TREE add constraint reg_985_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER')) then
		execute 'create table CORE_REGISTER ("registerid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'registerid')) then
        execute 'alter table CORE_REGISTER add "registerid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'registername')) then
        execute 'alter table CORE_REGISTER add "registername" VARCHAR(80) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'registerdescription')) then
        execute 'alter table CORE_REGISTER add "registerdescription" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'allpri_table')) then
        execute 'alter table CORE_REGISTER add "allpri_table" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'object_table')) then
        execute 'alter table CORE_REGISTER add "object_table" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'quant_table')) then
        execute 'alter table CORE_REGISTER add "quant_table" VARCHAR(40)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'track_changes_column')) then
        execute 'alter table CORE_REGISTER add "track_changes_column" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'storage_type')) then
        execute 'alter table CORE_REGISTER add "storage_type" BIGINT DEFAULT 1';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'object_sequence')) then
        execute 'alter table CORE_REGISTER add "object_sequence" VARCHAR(30) DEFAULT ''REG_OBJECT_SEQ''::character varying';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'is_virtual')) then
        execute 'alter table CORE_REGISTER add "is_virtual" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'contains_quant_in_future')) then
        execute 'alter table CORE_REGISTER add "contains_quant_in_future" SMALLINT DEFAULT 1 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'db_connection_name')) then
        execute 'alter table CORE_REGISTER add "db_connection_name" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'track_changes_userid')) then
        execute 'alter table CORE_REGISTER add "track_changes_userid" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'track_changes_date')) then
        execute 'alter table CORE_REGISTER add "track_changes_date" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_930_quant_pk')) then
    execute 'alter table CORE_REGISTER add constraint reg_930_quant_pk primary key (registerid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_ATTRIBUTE')) then
		execute 'create table CORE_REGISTER_ATTRIBUTE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'id')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'name')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "name" VARCHAR(300) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'registerid')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "registerid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'type')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "type" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'parentid')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "parentid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'referenceid')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "referenceid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'value_field')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "value_field" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'code_field')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "code_field" VARCHAR(32)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'value_template')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "value_template" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'primary_key')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "primary_key" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'user_key')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "user_key" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'qscolumn')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "qscolumn" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'internal_name')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "internal_name" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'is_nullable')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "is_nullable" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'description')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "description" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'layout')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "layout" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'export_column_name')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "export_column_name" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_931_quant_pk')) then
    execute 'alter table CORE_REGISTER_ATTRIBUTE add constraint reg_931_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_LOCK')) then
		execute 'create table CORE_REGISTER_LOCK ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'id')) then
        execute 'alter table CORE_REGISTER_LOCK add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'userid')) then
        execute 'alter table CORE_REGISTER_LOCK add "userid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'registerid')) then
        execute 'alter table CORE_REGISTER_LOCK add "registerid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'objectid')) then
        execute 'alter table CORE_REGISTER_LOCK add "objectid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'lockdate')) then
        execute 'alter table CORE_REGISTER_LOCK add "lockdate" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_939_quant_pk')) then
    execute 'alter table CORE_REGISTER_LOCK add constraint reg_939_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_RELATION')) then
		execute 'create table CORE_REGISTER_RELATION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'id')) then
        execute 'alter table CORE_REGISTER_RELATION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'name')) then
        execute 'alter table CORE_REGISTER_RELATION add "name" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'parentregister')) then
        execute 'alter table CORE_REGISTER_RELATION add "parentregister" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'chieldregister')) then
        execute 'alter table CORE_REGISTER_RELATION add "chieldregister" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'cardinality')) then
        execute 'alter table CORE_REGISTER_RELATION add "cardinality" VARCHAR(4)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'kindid')) then
        execute 'alter table CORE_REGISTER_RELATION add "kindid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'parentregister_attribute_id')) then
        execute 'alter table CORE_REGISTER_RELATION add "parentregister_attribute_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_932_quant_pk')) then
    execute 'alter table CORE_REGISTER_RELATION add constraint reg_932_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REGISTER_STATE')) then
		execute 'create table CORE_REGISTER_STATE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_STATE', 'id')) then
        execute 'alter table CORE_REGISTER_STATE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_STATE', 'register_view_id')) then
        execute 'alter table CORE_REGISTER_STATE add "register_view_id" VARCHAR(100) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_STATE', 'user_id')) then
        execute 'alter table CORE_REGISTER_STATE add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_STATE', 'state_save_date')) then
        execute 'alter table CORE_REGISTER_STATE add "state_save_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_991_quant_pk')) then
    execute 'alter table CORE_REGISTER_STATE add constraint reg_991_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REGNOM_REPOSITORY')) then
		execute 'create table CORE_REGNOM_REPOSITORY ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_REPOSITORY', 'regnomvalue')) then
        execute 'alter table CORE_REGNOM_REPOSITORY add "regnomvalue" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_REPOSITORY', 'idsequence')) then
        execute 'alter table CORE_REGNOM_REPOSITORY add "idsequence" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_REPOSITORY', 'regnomincrement')) then
        execute 'alter table CORE_REGNOM_REPOSITORY add "regnomincrement" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_REPOSITORY', 'id')) then
        execute 'alter table CORE_REGNOM_REPOSITORY add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('core_regnom_repository_pkey')) then
    execute 'alter table CORE_REGNOM_REPOSITORY add constraint core_regnom_repository_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REGNOM_SEQUENCES')) then
		execute 'create table CORE_REGNOM_SEQUENCES ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'id')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'numeratorid')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "numeratorid" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'regnomtype')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "regnomtype" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par1')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par1" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par0')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par0" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par2')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par2" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par3')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par3" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par4')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par4" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par5')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par5" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par6')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par6" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par7')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par7" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par8')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par8" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'par9')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "par9" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'currentincrement')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add "currentincrement" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_995_quant_pk')) then
    execute 'alter table CORE_REGNOM_SEQUENCES add constraint reg_995_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_AUDIT')) then
		execute 'create table CORE_SRD_AUDIT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'id')) then
        execute 'alter table CORE_SRD_AUDIT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'function_id')) then
        execute 'alter table CORE_SRD_AUDIT add "function_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'user_id')) then
        execute 'alter table CORE_SRD_AUDIT add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'actiontime')) then
        execute 'alter table CORE_SRD_AUDIT add "actiontime" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'result')) then
        execute 'alter table CORE_SRD_AUDIT add "result" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'result_desc')) then
        execute 'alter table CORE_SRD_AUDIT add "result_desc" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'session_id')) then
        execute 'alter table CORE_SRD_AUDIT add "session_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'object_register_id')) then
        execute 'alter table CORE_SRD_AUDIT add "object_register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'object_id')) then
        execute 'alter table CORE_SRD_AUDIT add "object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'object_status_id')) then
        execute 'alter table CORE_SRD_AUDIT add "object_status_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'external_id')) then
        execute 'alter table CORE_SRD_AUDIT add "external_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'main_object_register_id')) then
        execute 'alter table CORE_SRD_AUDIT add "main_object_register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'main_object_id')) then
        execute 'alter table CORE_SRD_AUDIT add "main_object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_940_quant_pk')) then
    execute 'alter table CORE_SRD_AUDIT add constraint reg_940_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_DEPARTMENT')) then
		execute 'create table CORE_SRD_DEPARTMENT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'id')) then
        execute 'alter table CORE_SRD_DEPARTMENT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'code')) then
        execute 'alter table CORE_SRD_DEPARTMENT add "code" VARCHAR(10)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'name')) then
        execute 'alter table CORE_SRD_DEPARTMENT add "name" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'manager')) then
        execute 'alter table CORE_SRD_DEPARTMENT add "manager" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'name_genitive_case')) then
        execute 'alter table CORE_SRD_DEPARTMENT add "name_genitive_case" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'is_deleted')) then
        execute 'alter table CORE_SRD_DEPARTMENT add "is_deleted" SMALLINT DEFAULT 0';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_941_quant_pk')) then
    execute 'alter table CORE_SRD_DEPARTMENT add constraint reg_941_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('core_srd_function')) then
		execute 'create table core_srd_function ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('core_srd_function', 'id')) then
        execute 'alter table core_srd_function add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('core_srd_function', 'functionname')) then
        execute 'alter table core_srd_function add "functionname" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('core_srd_function', 'functiontag')) then
        execute 'alter table core_srd_function add "functiontag" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('core_srd_function', 'parent_id')) then
        execute 'alter table core_srd_function add "parent_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('core_srd_function', 'description')) then
        execute 'alter table core_srd_function add "description" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('core_srd_function_idx')) then
    execute 'alter table core_srd_function add constraint core_srd_function_idx unique (functiontag)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_942_quant_pk')) then
    execute 'alter table core_srd_function add constraint reg_942_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('core_srd_function_idx')) then
	execute 'CREATE UNIQUE INDEX core_srd_function_idx on core_srd_function (functiontag)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_FUNCTION')) then
		execute 'create table CORE_SRD_FUNCTION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'id')) then
        execute 'alter table CORE_SRD_FUNCTION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'functionname')) then
        execute 'alter table CORE_SRD_FUNCTION add "functionname" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'functiontag')) then
        execute 'alter table CORE_SRD_FUNCTION add "functiontag" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'parent_id')) then
        execute 'alter table CORE_SRD_FUNCTION add "parent_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'description')) then
        execute 'alter table CORE_SRD_FUNCTION add "description" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('core_srd_function_idx')) then
    execute 'alter table CORE_SRD_FUNCTION add constraint core_srd_function_idx unique (functiontag)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_942_quant_pk')) then
    execute 'alter table CORE_SRD_FUNCTION add constraint reg_942_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('core_srd_function_idx')) then
	execute 'CREATE UNIQUE INDEX core_srd_function_idx on CORE_SRD_FUNCTION (functiontag)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_FUNCTION_REG_CAT')) then
		execute 'create table CORE_SRD_FUNCTION_REG_CAT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION_REG_CAT', 'id')) then
        execute 'alter table CORE_SRD_FUNCTION_REG_CAT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION_REG_CAT', 'function_id')) then
        execute 'alter table CORE_SRD_FUNCTION_REG_CAT add "function_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION_REG_CAT', 'register_category_id')) then
        execute 'alter table CORE_SRD_FUNCTION_REG_CAT add "register_category_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_958_quant_pk')) then
    execute 'alter table CORE_SRD_FUNCTION_REG_CAT add constraint reg_958_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_REGISTER_CATEGORY')) then
		execute 'create table CORE_SRD_REGISTER_CATEGORY ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'id')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'register_id')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add "register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'parent_id')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add "parent_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'name')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add "name" VARCHAR(256)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'qs_condition')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add "qs_condition" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_957_quant_pk')) then
    execute 'alter table CORE_SRD_REGISTER_CATEGORY add constraint reg_957_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE')) then
		execute 'create table CORE_SRD_ROLE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'id')) then
        execute 'alter table CORE_SRD_ROLE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'rolename')) then
        execute 'alter table CORE_SRD_ROLE add "rolename" VARCHAR(320)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'roletag')) then
        execute 'alter table CORE_SRD_ROLE add "roletag" VARCHAR(320)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'isadmin')) then
        execute 'alter table CORE_SRD_ROLE add "isadmin" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'all_registers_read')) then
        execute 'alter table CORE_SRD_ROLE add "all_registers_read" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'all_registers_write')) then
        execute 'alter table CORE_SRD_ROLE add "all_registers_write" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'subsystem')) then
        execute 'alter table CORE_SRD_ROLE add "subsystem" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_945_quant_pk')) then
    execute 'alter table CORE_SRD_ROLE add constraint reg_945_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_ATTR')) then
		execute 'create table CORE_SRD_ROLE_ATTR ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'id')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'rule_id')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add "rule_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'attribute_id')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'can_read')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add "can_read" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'can_write')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add "can_write" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_948_quant_pk')) then
    execute 'alter table CORE_SRD_ROLE_ATTR add constraint reg_948_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_FILTER')) then
		execute 'create table CORE_SRD_ROLE_FILTER ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'id')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'role_id')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add "role_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'register_id')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add "register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'register_view_id')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add "register_view_id" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'qscondition')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add "qscondition" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'description')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add "description" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_955_quant_pk')) then
    execute 'alter table CORE_SRD_ROLE_FILTER add constraint reg_955_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_FUNCTION')) then
		execute 'create table CORE_SRD_ROLE_FUNCTION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FUNCTION', 'id')) then
        execute 'alter table CORE_SRD_ROLE_FUNCTION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FUNCTION', 'function_id')) then
        execute 'alter table CORE_SRD_ROLE_FUNCTION add "function_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FUNCTION', 'role_id')) then
        execute 'alter table CORE_SRD_ROLE_FUNCTION add "role_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_946_quant_pk')) then
    execute 'alter table CORE_SRD_ROLE_FUNCTION add constraint reg_946_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_ROLE_REGISTER')) then
		execute 'create table CORE_SRD_ROLE_REGISTER ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'id')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'role_id')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add "role_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'register_id')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add "register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'can_read')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add "can_read" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'can_write')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add "can_write" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'all_attributes')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add "all_attributes" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_947_quant_pk')) then
    execute 'alter table CORE_SRD_ROLE_REGISTER add constraint reg_947_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_SESSION')) then
		execute 'create table CORE_SRD_SESSION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'id')) then
        execute 'alter table CORE_SRD_SESSION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'user_id')) then
        execute 'alter table CORE_SRD_SESSION add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'logintime')) then
        execute 'alter table CORE_SRD_SESSION add "logintime" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'logouttime')) then
        execute 'alter table CORE_SRD_SESSION add "logouttime" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'ip')) then
        execute 'alter table CORE_SRD_SESSION add "ip" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'asp_session_id')) then
        execute 'alter table CORE_SRD_SESSION add "asp_session_id" VARCHAR(40)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'browser_name')) then
        execute 'alter table CORE_SRD_SESSION add "browser_name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'browser_version')) then
        execute 'alter table CORE_SRD_SESSION add "browser_version" VARCHAR(30)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'browser_platform')) then
        execute 'alter table CORE_SRD_SESSION add "browser_platform" VARCHAR(10)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'browser_js_version')) then
        execute 'alter table CORE_SRD_SESSION add "browser_js_version" VARCHAR(5)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'login_status')) then
        execute 'alter table CORE_SRD_SESSION add "login_status" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'commentary')) then
        execute 'alter table CORE_SRD_SESSION add "commentary" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'last_activity')) then
        execute 'alter table CORE_SRD_SESSION add "last_activity" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_949_quant_pk')) then
    execute 'alter table CORE_SRD_SESSION add constraint reg_949_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USER')) then
		execute 'create table CORE_SRD_USER ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'id')) then
        execute 'alter table CORE_SRD_USER add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'department_id')) then
        execute 'alter table CORE_SRD_USER add "department_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'username')) then
        execute 'alter table CORE_SRD_USER add "username" VARCHAR(68) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'fullname')) then
        execute 'alter table CORE_SRD_USER add "fullname" VARCHAR(100) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'name')) then
        execute 'alter table CORE_SRD_USER add "name" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'surname')) then
        execute 'alter table CORE_SRD_USER add "surname" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'patronymic')) then
        execute 'alter table CORE_SRD_USER add "patronymic" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'fullname_for_doc')) then
        execute 'alter table CORE_SRD_USER add "fullname_for_doc" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'position')) then
        execute 'alter table CORE_SRD_USER add "position" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'is_deleted')) then
        execute 'alter table CORE_SRD_USER add "is_deleted" SMALLINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'change_date')) then
        execute 'alter table CORE_SRD_USER add "change_date" TIMESTAMP DEFAULT CURRENT_TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'password_md5')) then
        execute 'alter table CORE_SRD_USER add "password_md5" VARCHAR(32)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'email')) then
        execute 'alter table CORE_SRD_USER add "email" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'phone')) then
        execute 'alter table CORE_SRD_USER add "phone" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'external_id')) then
        execute 'alter table CORE_SRD_USER add "external_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'isdomainuser')) then
        execute 'alter table CORE_SRD_USER add "isdomainuser" SMALLINT DEFAULT 0';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'external_system')) then
        execute 'alter table CORE_SRD_USER add "external_system" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'blocked_untill')) then
        execute 'alter table CORE_SRD_USER add "blocked_untill" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'password_change_date')) then
        execute 'alter table CORE_SRD_USER add "password_change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'prev_passwords')) then
        execute 'alter table CORE_SRD_USER add "prev_passwords" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'blocked_from')) then
        execute 'alter table CORE_SRD_USER add "blocked_from" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'max_entrance_count')) then
        execute 'alter table CORE_SRD_USER add "max_entrance_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'current_entrance_count')) then
        execute 'alter table CORE_SRD_USER add "current_entrance_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_950_quant_pk')) then
    execute 'alter table CORE_SRD_USER add constraint reg_950_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USER_ROLE')) then
		execute 'create table CORE_SRD_USER_ROLE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER_ROLE', 'id')) then
        execute 'alter table CORE_SRD_USER_ROLE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER_ROLE', 'user_id')) then
        execute 'alter table CORE_SRD_USER_ROLE add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER_ROLE', 'role_id')) then
        execute 'alter table CORE_SRD_USER_ROLE add "role_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_952_quant_pk')) then
    execute 'alter table CORE_SRD_USER_ROLE add constraint reg_952_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGS')) then
		execute 'create table CORE_SRD_USERSETTINGS ("userid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGS', 'userid')) then
        execute 'alter table CORE_SRD_USERSETTINGS add "userid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGS', 'settings')) then
        execute 'alter table CORE_SRD_USERSETTINGS add "settings" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGS', 'default_layout_settings')) then
        execute 'alter table CORE_SRD_USERSETTINGS add "default_layout_settings" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_951_quant_pk')) then
    execute 'alter table CORE_SRD_USERSETTINGS add constraint reg_951_quant_pk primary key (userid)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGSLAYOUT')) then
		execute 'create table CORE_SRD_USERSETTINGSLAYOUT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSLAYOUT', 'id')) then
        execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSLAYOUT', 'user_id')) then
        execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSLAYOUT', 'layout_id')) then
        execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add "layout_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSLAYOUT', 'settings')) then
        execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add "settings" VARCHAR NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_954_quant_pk')) then
    execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add constraint reg_954_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGSREGVIEW')) then
		execute 'create table CORE_SRD_USERSETTINGSREGVIEW ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'id')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'user_id')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'register_view_id')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "register_view_id" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'fast_filter')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "fast_filter" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'splitter_orientation')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "splitter_orientation" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'splitter_size')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "splitter_size" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'page_size')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "page_size" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'condition')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "condition" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_925_quant_pk')) then
    execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add constraint reg_925_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_SRD_USERSETTINGSREPORT')) then
		execute 'create table CORE_SRD_USERSETTINGSREPORT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREPORT', 'id')) then
        execute 'alter table CORE_SRD_USERSETTINGSREPORT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREPORT', 'user_id')) then
        execute 'alter table CORE_SRD_USERSETTINGSREPORT add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREPORT', 'report_id')) then
        execute 'alter table CORE_SRD_USERSETTINGSREPORT add "report_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREPORT', 'settings')) then
        execute 'alter table CORE_SRD_USERSETTINGSREPORT add "settings" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_926_quant_pk')) then
    execute 'alter table CORE_SRD_USERSETTINGSREPORT add constraint reg_926_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_ATTACHMENTS')) then
		execute 'create table CORE_TD_ATTACHMENTS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'id')) then
        execute 'alter table CORE_TD_ATTACHMENTS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'td_id')) then
        execute 'alter table CORE_TD_ATTACHMENTS add "td_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'attachment_id')) then
        execute 'alter table CORE_TD_ATTACHMENTS add "attachment_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'is_deleted')) then
        execute 'alter table CORE_TD_ATTACHMENTS add "is_deleted" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'deleted_by')) then
        execute 'alter table CORE_TD_ATTACHMENTS add "deleted_by" VARCHAR(120)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'deleted_date')) then
        execute 'alter table CORE_TD_ATTACHMENTS add "deleted_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_969_quant_pk')) then
    execute 'alter table CORE_TD_ATTACHMENTS add constraint reg_969_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_AUDIT')) then
		execute 'create table CORE_TD_AUDIT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'id')) then
        execute 'alter table CORE_TD_AUDIT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'td_id')) then
        execute 'alter table CORE_TD_AUDIT add "td_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'action_id')) then
        execute 'alter table CORE_TD_AUDIT add "action_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'date_time')) then
        execute 'alter table CORE_TD_AUDIT add "date_time" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'actionresult')) then
        execute 'alter table CORE_TD_AUDIT add "actionresult" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'statusafter')) then
        execute 'alter table CORE_TD_AUDIT add "statusafter" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'newauthor')) then
        execute 'alter table CORE_TD_AUDIT add "newauthor" VARCHAR(68)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'newnumber')) then
        execute 'alter table CORE_TD_AUDIT add "newnumber" VARCHAR(40)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'description')) then
        execute 'alter table CORE_TD_AUDIT add "description" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'user_id')) then
        execute 'alter table CORE_TD_AUDIT add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_967_quant_pk')) then
    execute 'alter table CORE_TD_AUDIT add constraint reg_967_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_AUDIT_ACTION')) then
		execute 'create table CORE_TD_AUDIT_ACTION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT_ACTION', 'id')) then
        execute 'alter table CORE_TD_AUDIT_ACTION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT_ACTION', 'name')) then
        execute 'alter table CORE_TD_AUDIT_ACTION add "name" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_966_quant_pk')) then
    execute 'alter table CORE_TD_AUDIT_ACTION add constraint reg_966_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_CHANGE')) then
		execute 'create table CORE_TD_CHANGE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'id')) then
        execute 'alter table CORE_TD_CHANGE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'changeset_id')) then
        execute 'alter table CORE_TD_CHANGE add "changeset_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'register_id')) then
        execute 'alter table CORE_TD_CHANGE add "register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'object_id')) then
        execute 'alter table CORE_TD_CHANGE add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'quant_id')) then
        execute 'alter table CORE_TD_CHANGE add "quant_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'action')) then
        execute 'alter table CORE_TD_CHANGE add "action" BIGINT DEFAULT 1 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'change_user_id')) then
        execute 'alter table CORE_TD_CHANGE add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'change_date')) then
        execute 'alter table CORE_TD_CHANGE add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_965_quant_pk')) then
    execute 'alter table CORE_TD_CHANGE add constraint reg_965_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_CHANGESET')) then
		execute 'create table CORE_TD_CHANGESET ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'id')) then
        execute 'alter table CORE_TD_CHANGESET add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'td_id')) then
        execute 'alter table CORE_TD_CHANGESET add "td_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'changeset_date')) then
        execute 'alter table CORE_TD_CHANGESET add "changeset_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'status')) then
        execute 'alter table CORE_TD_CHANGESET add "status" BIGINT DEFAULT 1 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'user_id')) then
        execute 'alter table CORE_TD_CHANGESET add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_964_quant_pk')) then
    execute 'alter table CORE_TD_CHANGESET add constraint reg_964_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_INSTANCE')) then
		execute 'create table CORE_TD_INSTANCE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'id')) then
        execute 'alter table CORE_TD_INSTANCE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'template_version_id')) then
        execute 'alter table CORE_TD_INSTANCE add "template_version_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'description')) then
        execute 'alter table CORE_TD_INSTANCE add "description" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'author_id')) then
        execute 'alter table CORE_TD_INSTANCE add "author_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'regnumber')) then
        execute 'alter table CORE_TD_INSTANCE add "regnumber" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'create_date')) then
        execute 'alter table CORE_TD_INSTANCE add "create_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'change_date')) then
        execute 'alter table CORE_TD_INSTANCE add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'status')) then
        execute 'alter table CORE_TD_INSTANCE add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'priority')) then
        execute 'alter table CORE_TD_INSTANCE add "priority" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'object_id')) then
        execute 'alter table CORE_TD_INSTANCE add "object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'approve_date')) then
        execute 'alter table CORE_TD_INSTANCE add "approve_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'approve_user')) then
        execute 'alter table CORE_TD_INSTANCE add "approve_user" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'register_id')) then
        execute 'alter table CORE_TD_INSTANCE add "register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'xml_data')) then
        execute 'alter table CORE_TD_INSTANCE add "xml_data" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_963_quant_pk')) then
    execute 'alter table CORE_TD_INSTANCE add constraint reg_963_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_STATUS')) then
		execute 'create table CORE_TD_STATUS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_STATUS', 'id')) then
        execute 'alter table CORE_TD_STATUS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_STATUS', 'name')) then
        execute 'alter table CORE_TD_STATUS add "name" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_962_quant_pk')) then
    execute 'alter table CORE_TD_STATUS add constraint reg_962_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE')) then
		execute 'create table CORE_TD_TEMPLATE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE', 'id')) then
        execute 'alter table CORE_TD_TEMPLATE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE', 'current_version_id')) then
        execute 'alter table CORE_TD_TEMPLATE add "current_version_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE', 'template_name')) then
        execute 'alter table CORE_TD_TEMPLATE add "template_name" VARCHAR(150) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE', 'scheme_name')) then
        execute 'alter table CORE_TD_TEMPLATE add "scheme_name" VARCHAR(50) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_960_quant_pk')) then
    execute 'alter table CORE_TD_TEMPLATE add constraint reg_960_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE_TYPE')) then
		execute 'create table CORE_TD_TEMPLATE_TYPE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_TYPE', 'id')) then
        execute 'alter table CORE_TD_TEMPLATE_TYPE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_TYPE', 'name')) then
        execute 'alter table CORE_TD_TEMPLATE_TYPE add "name" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_971_quant_pk')) then
    execute 'alter table CORE_TD_TEMPLATE_TYPE add constraint reg_971_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TEMPLATE_VERSION')) then
		execute 'create table CORE_TD_TEMPLATE_VERSION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'id')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'template_id')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "template_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'version')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "version" BIGINT DEFAULT 1 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'xsd')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "xsd" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'xsl_print')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "xsl_print" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'publish_path')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "publish_path" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'create_date')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "create_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'author')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "author" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'xsd_class_name')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "xsd_class_name" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'template_type')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "template_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'print_view_specified')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add "print_view_specified" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_961_quant_pk')) then
    execute 'alter table CORE_TD_TEMPLATE_VERSION add constraint reg_961_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TP')) then
		execute 'create table CORE_TD_TP ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TP', 'id')) then
        execute 'alter table CORE_TD_TP add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_970_quant_pk')) then
    execute 'alter table CORE_TD_TP add constraint reg_970_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_TD_TREE')) then
		execute 'create table CORE_TD_TREE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'id')) then
        execute 'alter table CORE_TD_TREE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'parent_id')) then
        execute 'alter table CORE_TD_TREE add "parent_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'folder_name')) then
        execute 'alter table CORE_TD_TREE add "folder_name" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'template_id')) then
        execute 'alter table CORE_TD_TREE add "template_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'tree_order')) then
        execute 'alter table CORE_TD_TREE add "tree_order" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_968_quant_pk')) then
    execute 'alter table CORE_TD_TREE add constraint reg_968_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('CORE_UPDSTRU_LOG')) then
		execute 'create table CORE_UPDSTRU_LOG ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'id')) then
        execute 'alter table CORE_UPDSTRU_LOG add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'date_start')) then
        execute 'alter table CORE_UPDSTRU_LOG add "date_start" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'date_finish')) then
        execute 'alter table CORE_UPDSTRU_LOG add "date_finish" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'script_name')) then
        execute 'alter table CORE_UPDSTRU_LOG add "script_name" VARCHAR(100) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'script_version')) then
        execute 'alter table CORE_UPDSTRU_LOG add "script_version" VARCHAR(20)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'has_err')) then
        execute 'alter table CORE_UPDSTRU_LOG add "has_err" BIGINT DEFAULT 0 NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'result_message')) then
        execute 'alter table CORE_UPDSTRU_LOG add "result_message" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_997_quant_pk')) then
    execute 'alter table CORE_UPDSTRU_LOG add constraint reg_997_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_DASHBOARD')) then
		execute 'create table DASHBOARDS_DASHBOARD ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'id')) then
        execute 'alter table DASHBOARDS_DASHBOARD add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'user_id')) then
        execute 'alter table DASHBOARDS_DASHBOARD add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'layout_type')) then
        execute 'alter table DASHBOARDS_DASHBOARD add "layout_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'name')) then
        execute 'alter table DASHBOARDS_DASHBOARD add "name" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'description')) then
        execute 'alter table DASHBOARDS_DASHBOARD add "description" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'iscommon')) then
        execute 'alter table DASHBOARDS_DASHBOARD add "iscommon" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'srd_dashboard')) then
        execute 'alter table DASHBOARDS_DASHBOARD add "srd_dashboard" VARCHAR(200)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'subsystem')) then
        execute 'alter table DASHBOARDS_DASHBOARD add "subsystem" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_850_quant_pk')) then
    execute 'alter table DASHBOARDS_DASHBOARD add constraint reg_850_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_PANEL')) then
		execute 'create table DASHBOARDS_PANEL ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'id')) then
        execute 'alter table DASHBOARDS_PANEL add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'dashboard_id')) then
        execute 'alter table DASHBOARDS_PANEL add "dashboard_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'title')) then
        execute 'alter table DASHBOARDS_PANEL add "title" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'column_index')) then
        execute 'alter table DASHBOARDS_PANEL add "column_index" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'order_in_column')) then
        execute 'alter table DASHBOARDS_PANEL add "order_in_column" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'panel_type_id')) then
        execute 'alter table DASHBOARDS_PANEL add "panel_type_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'settings')) then
        execute 'alter table DASHBOARDS_PANEL add "settings" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_851_quant_pk')) then
    execute 'alter table DASHBOARDS_PANEL add constraint reg_851_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_PANEL_TYPE')) then
		execute 'create table DASHBOARDS_PANEL_TYPE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'id')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'name')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add "name" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'description')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add "description" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'url')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add "url" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'dto_class_full_name')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add "dto_class_full_name" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_852_quant_pk')) then
    execute 'alter table DASHBOARDS_PANEL_TYPE add constraint reg_852_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_USER_SETTINGS')) then
		execute 'create table DASHBOARDS_USER_SETTINGS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_USER_SETTINGS', 'id')) then
        execute 'alter table DASHBOARDS_USER_SETTINGS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_USER_SETTINGS', 'user_id')) then
        execute 'alter table DASHBOARDS_USER_SETTINGS add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_USER_SETTINGS', 'default_panel_id')) then
        execute 'alter table DASHBOARDS_USER_SETTINGS add "default_panel_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_USER_SETTINGS', 'subsystem')) then
        execute 'alter table DASHBOARDS_USER_SETTINGS add "subsystem" VARCHAR(250)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_853_quant_pk')) then
    execute 'alter table DASHBOARDS_USER_SETTINGS add constraint reg_853_quant_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_BOOK')) then
		execute 'create table DECLARATIONS_BOOK ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK', 'id')) then
        execute 'alter table DECLARATIONS_BOOK add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK', 'prefics')) then
        execute 'alter table DECLARATIONS_BOOK add "prefics" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK', 'date_end')) then
        execute 'alter table DECLARATIONS_BOOK add "date_end" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK', 'status')) then
        execute 'alter table DECLARATIONS_BOOK add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK', 'type')) then
        execute 'alter table DECLARATIONS_BOOK add "type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK', 'date_begin')) then
        execute 'alter table DECLARATIONS_BOOK add "date_begin" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_500_q_pk')) then
    execute 'alter table DECLARATIONS_BOOK add constraint reg_500_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_DECLARATION')) then
		execute 'create table DECLARATIONS_DECLARATION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'id')) then
        execute 'alter table DECLARATIONS_DECLARATION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'book_id')) then
        execute 'alter table DECLARATIONS_DECLARATION add "book_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'agent_id')) then
        execute 'alter table DECLARATIONS_DECLARATION add "agent_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'user_isp_id')) then
        execute 'alter table DECLARATIONS_DECLARATION add "user_isp_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'user_reg_id')) then
        execute 'alter table DECLARATIONS_DECLARATION add "user_reg_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'num_in')) then
        execute 'alter table DECLARATIONS_DECLARATION add "num_in" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_in')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_in" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'duration_in')) then
        execute 'alter table DECLARATIONS_DECLARATION add "duration_in" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'status')) then
        execute 'alter table DECLARATIONS_DECLARATION add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'cadastral_num_obj')) then
        execute 'alter table DECLARATIONS_DECLARATION add "cadastral_num_obj" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'type_obj')) then
        execute 'alter table DECLARATIONS_DECLARATION add "type_obj" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_in_isp')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_in_isp" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_in_plan')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_in_plan" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'check_point1')) then
        execute 'alter table DECLARATIONS_DECLARATION add "check_point1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'check_point3')) then
        execute 'alter table DECLARATIONS_DECLARATION add "check_point3" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'check_point4')) then
        execute 'alter table DECLARATIONS_DECLARATION add "check_point4" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_check_plan')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_check_plan" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_check_fact')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_check_fact" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'check_uved')) then
        execute 'alter table DECLARATIONS_DECLARATION add "check_uved" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_uved_plan')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_uved_plan" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_uved_fact')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_uved_fact" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_end')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_end" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'owner_type')) then
        execute 'alter table DECLARATIONS_DECLARATION add "owner_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'uved_type_owner')) then
        execute 'alter table DECLARATIONS_DECLARATION add "uved_type_owner" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'uved_type_agent')) then
        execute 'alter table DECLARATIONS_DECLARATION add "uved_type_agent" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'certificate_num')) then
        execute 'alter table DECLARATIONS_DECLARATION add "certificate_num" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'certificate_name')) then
        execute 'alter table DECLARATIONS_DECLARATION add "certificate_name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'owner_id')) then
        execute 'alter table DECLARATIONS_DECLARATION add "owner_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'date_in_fact')) then
        execute 'alter table DECLARATIONS_DECLARATION add "date_in_fact" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'check_point2')) then
        execute 'alter table DECLARATIONS_DECLARATION add "check_point2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'certificate_date')) then
        execute 'alter table DECLARATIONS_DECLARATION add "certificate_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'purpose_dec')) then
        execute 'alter table DECLARATIONS_DECLARATION add "purpose_dec" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_501_q_pk')) then
    execute 'alter table DECLARATIONS_DECLARATION add constraint reg_501_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_HAR_OKS')) then
		execute 'create table DECLARATIONS_HAR_OKS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'id')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'declaration_id')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "declaration_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_1')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_1" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_2')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_2" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_3')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_3" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_4')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_4" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_5')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_5" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_6')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_6" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_7')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_7" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_8')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_8" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_9')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_9" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_10')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_10" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_11')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_11" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_12')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_12" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_13')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_13" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_14')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_14" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_15')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_15" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_16')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_16" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_17')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_17" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_18')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_18" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_19')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_19" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_20')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_20" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_1_1')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_1_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_1_2')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_1_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_1_3')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_1_3" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_2_1')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_2_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_2_2')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_2_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_2_3')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_2_3" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_3_1')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_3_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_3_2')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_3_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_4_1')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_4_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_5_1')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_5_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_4_2')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_4_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21_5_2')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_5_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_502_q_pk')) then
    execute 'alter table DECLARATIONS_HAR_OKS add constraint reg_502_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_HAR_PARCEL')) then
		execute 'create table DECLARATIONS_HAR_PARCEL ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_6')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_6" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'declaration_id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "declaration_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_1')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_1" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_2')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_2" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_3')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_3" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_4')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_4" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_5')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_5" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_7')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_7" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_8')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_8" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_9')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_9" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_10')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_10" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_12')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_12" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_1_1')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_1_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_1_2')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_1_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_1_3')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_1_3" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_2_1')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_2_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_2_2')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_2_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_2_3')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_2_3" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_3_1')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_3_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_3_2')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_3_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_4_1')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_4_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_4_2')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_4_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_5_1')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_5_1" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13_5_2')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_5_2" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_14')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_14" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_15')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_15" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_16')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_16" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_17')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_17" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_18')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_18" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_19')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_19" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_20')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_20" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_21')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_21" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_11')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_11" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_503_q_pk')) then
    execute 'alter table DECLARATIONS_HAR_PARCEL add constraint reg_503_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_RESULT')) then
		execute 'create table DECLARATIONS_RESULT ("declaration_id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_RESULT', 'declaration_id')) then
        execute 'alter table DECLARATIONS_RESULT add "declaration_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_RESULT', 'text_yes')) then
        execute 'alter table DECLARATIONS_RESULT add "text_yes" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_RESULT', 'text_no')) then
        execute 'alter table DECLARATIONS_RESULT add "text_no" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_504_q_pk')) then
    execute 'alter table DECLARATIONS_RESULT add constraint reg_504_q_pk primary key (declaration_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_SCAN_DATA')) then
		execute 'create table DECLARATIONS_SCAN_DATA ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SCAN_DATA', 'id')) then
        execute 'alter table DECLARATIONS_SCAN_DATA add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SCAN_DATA', 'declaration_id')) then
        execute 'alter table DECLARATIONS_SCAN_DATA add "declaration_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SCAN_DATA', 'creation_date')) then
        execute 'alter table DECLARATIONS_SCAN_DATA add "creation_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SCAN_DATA', 'file_name')) then
        execute 'alter table DECLARATIONS_SCAN_DATA add "file_name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SCAN_DATA', 'file_type')) then
        execute 'alter table DECLARATIONS_SCAN_DATA add "file_type" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_507_q_pk')) then
    execute 'alter table DECLARATIONS_SCAN_DATA add constraint reg_507_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_scan_data_declaration_id_idx')) then
	execute 'CREATE  INDEX declarations_scan_data_declaration_id_idx on DECLARATIONS_SCAN_DATA (declaration_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_SUBJECT')) then
		execute 'create table DECLARATIONS_SUBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'id')) then
        execute 'alter table DECLARATIONS_SUBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'name')) then
        execute 'alter table DECLARATIONS_SUBJECT add "name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'f_name')) then
        execute 'alter table DECLARATIONS_SUBJECT add "f_name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'i_name')) then
        execute 'alter table DECLARATIONS_SUBJECT add "i_name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'o_name')) then
        execute 'alter table DECLARATIONS_SUBJECT add "o_name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'address')) then
        execute 'alter table DECLARATIONS_SUBJECT add "address" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'mail')) then
        execute 'alter table DECLARATIONS_SUBJECT add "mail" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'telefon')) then
        execute 'alter table DECLARATIONS_SUBJECT add "telefon" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'type')) then
        execute 'alter table DECLARATIONS_SUBJECT add "type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_505_q_pk')) then
    execute 'alter table DECLARATIONS_SUBJECT add constraint reg_505_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_UVED')) then
		execute 'create table DECLARATIONS_UVED ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'id')) then
        execute 'alter table DECLARATIONS_UVED add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'declaration_id')) then
        execute 'alter table DECLARATIONS_UVED add "declaration_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'book_id')) then
        execute 'alter table DECLARATIONS_UVED add "book_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'num')) then
        execute 'alter table DECLARATIONS_UVED add "num" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'date')) then
        execute 'alter table DECLARATIONS_UVED add "date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'type')) then
        execute 'alter table DECLARATIONS_UVED add "type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'mail_num')) then
        execute 'alter table DECLARATIONS_UVED add "mail_num" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'mail_date')) then
        execute 'alter table DECLARATIONS_UVED add "mail_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_506_q_pk')) then
    execute 'alter table DECLARATIONS_UVED add constraint reg_506_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('FM_REPORTS_SAVEDREPORT')) then
		execute 'create table FM_REPORTS_SAVEDREPORT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'id')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'code')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'internal_name')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "internal_name" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'title')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "title" VARCHAR(128)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'user_id')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'object_register_id')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "object_register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'object_id')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'create_date')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "create_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'report_number')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "report_number" VARCHAR(100)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'comments')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "comments" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'parameters')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "parameters" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'status')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'end_date')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "end_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'result_message')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "result_message" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'is_deleted')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "is_deleted" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'file_type')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "file_type" VARCHAR(10)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'section')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add "section" VARCHAR(10)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('fm_reports_savedreport_pkey')) then
    execute 'alter table FM_REPORTS_SAVEDREPORT add constraint fm_reports_savedreport_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_MAIN_OBJECT')) then
		execute 'create table GBU_MAIN_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'id')) then
        execute 'alter table GBU_MAIN_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'cadastral_number')) then
        execute 'alter table GBU_MAIN_OBJECT add "cadastral_number" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'object_type_code')) then
        execute 'alter table GBU_MAIN_OBJECT add "object_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'is_active')) then
        execute 'alter table GBU_MAIN_OBJECT add "is_active" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_200_q_pk')) then
    execute 'alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberMainIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberMainIndex" on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source1_a_dt')) then
		execute 'create table gbu_source1_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'id')) then
        execute 'alter table gbu_source1_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'object_id')) then
        execute 'alter table gbu_source1_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source1_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'ot')) then
        execute 'alter table gbu_source1_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 's')) then
        execute 'alter table gbu_source1_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'actual')) then
        execute 'alter table gbu_source1_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'value')) then
        execute 'alter table gbu_source1_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'change_id')) then
        execute 'alter table gbu_source1_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'change_date')) then
        execute 'alter table gbu_source1_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source1_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source1_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_1_a_dt_pk')) then
    execute 'alter table gbu_source1_a_dt add constraint reg_1_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_1_a_dt_fk_o')) then
	execute 'alter table gbu_source1_a_dt add constraint reg_1_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_1_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_1_a_dt_inx_obj_attr_id on gbu_source1_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source1_a_num')) then
		execute 'create table gbu_source1_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'id')) then
        execute 'alter table gbu_source1_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'object_id')) then
        execute 'alter table gbu_source1_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'attribute_id')) then
        execute 'alter table gbu_source1_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'ot')) then
        execute 'alter table gbu_source1_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 's')) then
        execute 'alter table gbu_source1_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'actual')) then
        execute 'alter table gbu_source1_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'value')) then
        execute 'alter table gbu_source1_a_num add "value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'change_id')) then
        execute 'alter table gbu_source1_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'change_date')) then
        execute 'alter table gbu_source1_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'change_user_id')) then
        execute 'alter table gbu_source1_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source1_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_1_a_num_pk')) then
    execute 'alter table gbu_source1_a_num add constraint reg_1_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_1_a_num_fk_o')) then
	execute 'alter table gbu_source1_a_num add constraint reg_1_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_1_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_1_a_num_inx_obj_attr_id on gbu_source1_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source1_a_txt')) then
		execute 'create table gbu_source1_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'id')) then
        execute 'alter table gbu_source1_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'object_id')) then
        execute 'alter table gbu_source1_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source1_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'ot')) then
        execute 'alter table gbu_source1_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 's')) then
        execute 'alter table gbu_source1_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'actual')) then
        execute 'alter table gbu_source1_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source1_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'value')) then
        execute 'alter table gbu_source1_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'change_id')) then
        execute 'alter table gbu_source1_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'change_date')) then
        execute 'alter table gbu_source1_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source1_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source1_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_1_a_txt_pk')) then
    execute 'alter table gbu_source1_a_txt add constraint reg_1_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_1_a_txt_fk_o')) then
	execute 'alter table gbu_source1_a_txt add constraint reg_1_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_1_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_1_a_txt_inx_obj_attr_id on gbu_source1_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source10_a_dt')) then
		execute 'create table gbu_source10_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'id')) then
        execute 'alter table gbu_source10_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'object_id')) then
        execute 'alter table gbu_source10_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source10_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'ot')) then
        execute 'alter table gbu_source10_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 's')) then
        execute 'alter table gbu_source10_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'actual')) then
        execute 'alter table gbu_source10_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'value')) then
        execute 'alter table gbu_source10_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'change_id')) then
        execute 'alter table gbu_source10_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'change_date')) then
        execute 'alter table gbu_source10_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source10_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source10_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_10_a_dt_pk')) then
    execute 'alter table gbu_source10_a_dt add constraint reg_10_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_10_a_dt_fk_o')) then
	execute 'alter table gbu_source10_a_dt add constraint reg_10_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_10_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_10_a_dt_inx_obj_attr_id on gbu_source10_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source10_a_num')) then
		execute 'create table gbu_source10_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'id')) then
        execute 'alter table gbu_source10_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'object_id')) then
        execute 'alter table gbu_source10_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'attribute_id')) then
        execute 'alter table gbu_source10_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'ot')) then
        execute 'alter table gbu_source10_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 's')) then
        execute 'alter table gbu_source10_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'actual')) then
        execute 'alter table gbu_source10_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'value')) then
        execute 'alter table gbu_source10_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'change_id')) then
        execute 'alter table gbu_source10_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'change_date')) then
        execute 'alter table gbu_source10_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'change_user_id')) then
        execute 'alter table gbu_source10_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source10_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_10_a_num_pk')) then
    execute 'alter table gbu_source10_a_num add constraint reg_10_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_10_a_num_fk_o')) then
	execute 'alter table gbu_source10_a_num add constraint reg_10_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_10_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_10_a_num_inx_obj_attr_id on gbu_source10_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source10_a_txt')) then
		execute 'create table gbu_source10_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'id')) then
        execute 'alter table gbu_source10_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'object_id')) then
        execute 'alter table gbu_source10_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source10_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'ot')) then
        execute 'alter table gbu_source10_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 's')) then
        execute 'alter table gbu_source10_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'actual')) then
        execute 'alter table gbu_source10_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source10_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'value')) then
        execute 'alter table gbu_source10_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'change_id')) then
        execute 'alter table gbu_source10_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'change_date')) then
        execute 'alter table gbu_source10_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source10_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source10_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_10_a_txt_pk')) then
    execute 'alter table gbu_source10_a_txt add constraint reg_10_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_10_a_txt_fk_o')) then
	execute 'alter table gbu_source10_a_txt add constraint reg_10_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_10_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_10_a_txt_inx_obj_attr_id on gbu_source10_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source11_a_dt')) then
		execute 'create table gbu_source11_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'id')) then
        execute 'alter table gbu_source11_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'object_id')) then
        execute 'alter table gbu_source11_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source11_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'ot')) then
        execute 'alter table gbu_source11_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 's')) then
        execute 'alter table gbu_source11_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'actual')) then
        execute 'alter table gbu_source11_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'value')) then
        execute 'alter table gbu_source11_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'change_id')) then
        execute 'alter table gbu_source11_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'change_date')) then
        execute 'alter table gbu_source11_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source11_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source11_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_11_a_dt_pk')) then
    execute 'alter table gbu_source11_a_dt add constraint reg_11_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_11_a_dt_fk_o')) then
	execute 'alter table gbu_source11_a_dt add constraint reg_11_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_11_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_11_a_dt_inx_obj_attr_id on gbu_source11_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source11_a_num')) then
		execute 'create table gbu_source11_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'id')) then
        execute 'alter table gbu_source11_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'object_id')) then
        execute 'alter table gbu_source11_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'attribute_id')) then
        execute 'alter table gbu_source11_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'ot')) then
        execute 'alter table gbu_source11_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 's')) then
        execute 'alter table gbu_source11_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'actual')) then
        execute 'alter table gbu_source11_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'value')) then
        execute 'alter table gbu_source11_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'change_id')) then
        execute 'alter table gbu_source11_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'change_date')) then
        execute 'alter table gbu_source11_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'change_user_id')) then
        execute 'alter table gbu_source11_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source11_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_11_a_num_pk')) then
    execute 'alter table gbu_source11_a_num add constraint reg_11_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_11_a_num_fk_o')) then
	execute 'alter table gbu_source11_a_num add constraint reg_11_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_11_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_11_a_num_inx_obj_attr_id on gbu_source11_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source11_a_txt')) then
		execute 'create table gbu_source11_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'id')) then
        execute 'alter table gbu_source11_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'object_id')) then
        execute 'alter table gbu_source11_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source11_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'ot')) then
        execute 'alter table gbu_source11_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 's')) then
        execute 'alter table gbu_source11_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'actual')) then
        execute 'alter table gbu_source11_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source11_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'value')) then
        execute 'alter table gbu_source11_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'change_id')) then
        execute 'alter table gbu_source11_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'change_date')) then
        execute 'alter table gbu_source11_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source11_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source11_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_11_a_txt_pk')) then
    execute 'alter table gbu_source11_a_txt add constraint reg_11_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_11_a_txt_fk_o')) then
	execute 'alter table gbu_source11_a_txt add constraint reg_11_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_11_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_11_a_txt_inx_obj_attr_id on gbu_source11_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source12_a_dt')) then
		execute 'create table gbu_source12_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'id')) then
        execute 'alter table gbu_source12_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'object_id')) then
        execute 'alter table gbu_source12_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source12_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'ot')) then
        execute 'alter table gbu_source12_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 's')) then
        execute 'alter table gbu_source12_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'actual')) then
        execute 'alter table gbu_source12_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'value')) then
        execute 'alter table gbu_source12_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'change_id')) then
        execute 'alter table gbu_source12_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'change_date')) then
        execute 'alter table gbu_source12_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source12_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source12_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_12_a_dt_pk')) then
    execute 'alter table gbu_source12_a_dt add constraint reg_12_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_12_a_dt_fk_o')) then
	execute 'alter table gbu_source12_a_dt add constraint reg_12_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_12_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_12_a_dt_inx_obj_attr_id on gbu_source12_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source12_a_num')) then
		execute 'create table gbu_source12_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'id')) then
        execute 'alter table gbu_source12_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'object_id')) then
        execute 'alter table gbu_source12_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'attribute_id')) then
        execute 'alter table gbu_source12_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'ot')) then
        execute 'alter table gbu_source12_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 's')) then
        execute 'alter table gbu_source12_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'actual')) then
        execute 'alter table gbu_source12_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'value')) then
        execute 'alter table gbu_source12_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'change_id')) then
        execute 'alter table gbu_source12_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'change_date')) then
        execute 'alter table gbu_source12_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'change_user_id')) then
        execute 'alter table gbu_source12_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source12_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_12_a_num_pk')) then
    execute 'alter table gbu_source12_a_num add constraint reg_12_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_12_a_num_fk_o')) then
	execute 'alter table gbu_source12_a_num add constraint reg_12_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_12_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_12_a_num_inx_obj_attr_id on gbu_source12_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source12_a_txt')) then
		execute 'create table gbu_source12_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'id')) then
        execute 'alter table gbu_source12_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'object_id')) then
        execute 'alter table gbu_source12_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source12_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'ot')) then
        execute 'alter table gbu_source12_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 's')) then
        execute 'alter table gbu_source12_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'actual')) then
        execute 'alter table gbu_source12_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source12_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'value')) then
        execute 'alter table gbu_source12_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'change_id')) then
        execute 'alter table gbu_source12_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'change_date')) then
        execute 'alter table gbu_source12_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source12_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source12_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_12_a_txt_pk')) then
    execute 'alter table gbu_source12_a_txt add constraint reg_12_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_12_a_txt_fk_o')) then
	execute 'alter table gbu_source12_a_txt add constraint reg_12_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_12_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_12_a_txt_inx_obj_attr_id on gbu_source12_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source13_a_dt')) then
		execute 'create table gbu_source13_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'id')) then
        execute 'alter table gbu_source13_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'object_id')) then
        execute 'alter table gbu_source13_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source13_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'ot')) then
        execute 'alter table gbu_source13_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 's')) then
        execute 'alter table gbu_source13_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'actual')) then
        execute 'alter table gbu_source13_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'value')) then
        execute 'alter table gbu_source13_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'change_id')) then
        execute 'alter table gbu_source13_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'change_date')) then
        execute 'alter table gbu_source13_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source13_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source13_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_13_a_dt_pk')) then
    execute 'alter table gbu_source13_a_dt add constraint reg_13_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_13_a_dt_fk_o')) then
	execute 'alter table gbu_source13_a_dt add constraint reg_13_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_13_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_13_a_dt_inx_obj_attr_id on gbu_source13_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source13_a_num')) then
		execute 'create table gbu_source13_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'id')) then
        execute 'alter table gbu_source13_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'object_id')) then
        execute 'alter table gbu_source13_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'attribute_id')) then
        execute 'alter table gbu_source13_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'ot')) then
        execute 'alter table gbu_source13_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 's')) then
        execute 'alter table gbu_source13_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'actual')) then
        execute 'alter table gbu_source13_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'value')) then
        execute 'alter table gbu_source13_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'change_id')) then
        execute 'alter table gbu_source13_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'change_date')) then
        execute 'alter table gbu_source13_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'change_user_id')) then
        execute 'alter table gbu_source13_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source13_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_13_a_num_pk')) then
    execute 'alter table gbu_source13_a_num add constraint reg_13_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_13_a_num_fk_o')) then
	execute 'alter table gbu_source13_a_num add constraint reg_13_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_13_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_13_a_num_inx_obj_attr_id on gbu_source13_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source13_a_txt')) then
		execute 'create table gbu_source13_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'id')) then
        execute 'alter table gbu_source13_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'object_id')) then
        execute 'alter table gbu_source13_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source13_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'ot')) then
        execute 'alter table gbu_source13_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 's')) then
        execute 'alter table gbu_source13_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'actual')) then
        execute 'alter table gbu_source13_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source13_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'value')) then
        execute 'alter table gbu_source13_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'change_id')) then
        execute 'alter table gbu_source13_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'change_date')) then
        execute 'alter table gbu_source13_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source13_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source13_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_13_a_txt_pk')) then
    execute 'alter table gbu_source13_a_txt add constraint reg_13_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_13_a_txt_fk_o')) then
	execute 'alter table gbu_source13_a_txt add constraint reg_13_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_13_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_13_a_txt_inx_obj_attr_id on gbu_source13_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source14_a_dt')) then
		execute 'create table gbu_source14_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'id')) then
        execute 'alter table gbu_source14_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'object_id')) then
        execute 'alter table gbu_source14_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source14_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'ot')) then
        execute 'alter table gbu_source14_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 's')) then
        execute 'alter table gbu_source14_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'actual')) then
        execute 'alter table gbu_source14_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'value')) then
        execute 'alter table gbu_source14_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'change_id')) then
        execute 'alter table gbu_source14_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'change_date')) then
        execute 'alter table gbu_source14_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source14_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source14_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_14_a_dt_pk')) then
    execute 'alter table gbu_source14_a_dt add constraint reg_14_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_14_a_dt_fk_o')) then
	execute 'alter table gbu_source14_a_dt add constraint reg_14_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_14_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_14_a_dt_inx_obj_attr_id on gbu_source14_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source14_a_num')) then
		execute 'create table gbu_source14_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'id')) then
        execute 'alter table gbu_source14_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'object_id')) then
        execute 'alter table gbu_source14_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'attribute_id')) then
        execute 'alter table gbu_source14_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'ot')) then
        execute 'alter table gbu_source14_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 's')) then
        execute 'alter table gbu_source14_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'actual')) then
        execute 'alter table gbu_source14_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'value')) then
        execute 'alter table gbu_source14_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'change_id')) then
        execute 'alter table gbu_source14_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'change_date')) then
        execute 'alter table gbu_source14_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'change_user_id')) then
        execute 'alter table gbu_source14_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source14_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_14_a_num_pk')) then
    execute 'alter table gbu_source14_a_num add constraint reg_14_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_14_a_num_fk_o')) then
	execute 'alter table gbu_source14_a_num add constraint reg_14_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_14_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_14_a_num_inx_obj_attr_id on gbu_source14_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source14_a_txt')) then
		execute 'create table gbu_source14_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'id')) then
        execute 'alter table gbu_source14_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'object_id')) then
        execute 'alter table gbu_source14_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source14_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'ot')) then
        execute 'alter table gbu_source14_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 's')) then
        execute 'alter table gbu_source14_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'actual')) then
        execute 'alter table gbu_source14_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source14_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'value')) then
        execute 'alter table gbu_source14_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'change_id')) then
        execute 'alter table gbu_source14_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'change_date')) then
        execute 'alter table gbu_source14_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source14_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source14_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_14_a_txt_pk')) then
    execute 'alter table gbu_source14_a_txt add constraint reg_14_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_14_a_txt_fk_o')) then
	execute 'alter table gbu_source14_a_txt add constraint reg_14_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_14_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_14_a_txt_inx_obj_attr_id on gbu_source14_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source15_a_dt')) then
		execute 'create table gbu_source15_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'id')) then
        execute 'alter table gbu_source15_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'object_id')) then
        execute 'alter table gbu_source15_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source15_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'ot')) then
        execute 'alter table gbu_source15_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 's')) then
        execute 'alter table gbu_source15_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'actual')) then
        execute 'alter table gbu_source15_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'value')) then
        execute 'alter table gbu_source15_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'change_id')) then
        execute 'alter table gbu_source15_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'change_date')) then
        execute 'alter table gbu_source15_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source15_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source15_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_15_a_dt_pk')) then
    execute 'alter table gbu_source15_a_dt add constraint reg_15_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_15_a_dt_fk_o')) then
	execute 'alter table gbu_source15_a_dt add constraint reg_15_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_15_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_15_a_dt_inx_obj_attr_id on gbu_source15_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source15_a_num')) then
		execute 'create table gbu_source15_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'id')) then
        execute 'alter table gbu_source15_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'object_id')) then
        execute 'alter table gbu_source15_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'attribute_id')) then
        execute 'alter table gbu_source15_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'ot')) then
        execute 'alter table gbu_source15_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 's')) then
        execute 'alter table gbu_source15_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'actual')) then
        execute 'alter table gbu_source15_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'value')) then
        execute 'alter table gbu_source15_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'change_id')) then
        execute 'alter table gbu_source15_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'change_date')) then
        execute 'alter table gbu_source15_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'change_user_id')) then
        execute 'alter table gbu_source15_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source15_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_15_a_num_pk')) then
    execute 'alter table gbu_source15_a_num add constraint reg_15_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_15_a_num_fk_o')) then
	execute 'alter table gbu_source15_a_num add constraint reg_15_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_15_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_15_a_num_inx_obj_attr_id on gbu_source15_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source15_a_txt')) then
		execute 'create table gbu_source15_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'id')) then
        execute 'alter table gbu_source15_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'object_id')) then
        execute 'alter table gbu_source15_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source15_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'ot')) then
        execute 'alter table gbu_source15_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 's')) then
        execute 'alter table gbu_source15_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'actual')) then
        execute 'alter table gbu_source15_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source15_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'value')) then
        execute 'alter table gbu_source15_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'change_id')) then
        execute 'alter table gbu_source15_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'change_date')) then
        execute 'alter table gbu_source15_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source15_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source15_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_15_a_txt_pk')) then
    execute 'alter table gbu_source15_a_txt add constraint reg_15_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_15_a_txt_fk_o')) then
	execute 'alter table gbu_source15_a_txt add constraint reg_15_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_15_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_15_a_txt_inx_obj_attr_id on gbu_source15_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source16_a_dt')) then
		execute 'create table gbu_source16_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'id')) then
        execute 'alter table gbu_source16_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'object_id')) then
        execute 'alter table gbu_source16_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source16_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'ot')) then
        execute 'alter table gbu_source16_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 's')) then
        execute 'alter table gbu_source16_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'actual')) then
        execute 'alter table gbu_source16_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'value')) then
        execute 'alter table gbu_source16_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'change_id')) then
        execute 'alter table gbu_source16_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'change_date')) then
        execute 'alter table gbu_source16_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source16_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source16_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_16_a_dt_pk')) then
    execute 'alter table gbu_source16_a_dt add constraint reg_16_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_16_a_dt_fk_o')) then
	execute 'alter table gbu_source16_a_dt add constraint reg_16_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_16_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_16_a_dt_inx_obj_attr_id on gbu_source16_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source16_a_num')) then
		execute 'create table gbu_source16_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'id')) then
        execute 'alter table gbu_source16_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'object_id')) then
        execute 'alter table gbu_source16_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'attribute_id')) then
        execute 'alter table gbu_source16_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'ot')) then
        execute 'alter table gbu_source16_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 's')) then
        execute 'alter table gbu_source16_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'actual')) then
        execute 'alter table gbu_source16_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'value')) then
        execute 'alter table gbu_source16_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'change_id')) then
        execute 'alter table gbu_source16_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'change_date')) then
        execute 'alter table gbu_source16_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'change_user_id')) then
        execute 'alter table gbu_source16_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source16_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_16_a_num_pk')) then
    execute 'alter table gbu_source16_a_num add constraint reg_16_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_16_a_num_fk_o')) then
	execute 'alter table gbu_source16_a_num add constraint reg_16_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_16_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_16_a_num_inx_obj_attr_id on gbu_source16_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source16_a_txt')) then
		execute 'create table gbu_source16_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'id')) then
        execute 'alter table gbu_source16_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'object_id')) then
        execute 'alter table gbu_source16_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source16_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'ot')) then
        execute 'alter table gbu_source16_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 's')) then
        execute 'alter table gbu_source16_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'actual')) then
        execute 'alter table gbu_source16_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source16_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'value')) then
        execute 'alter table gbu_source16_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'change_id')) then
        execute 'alter table gbu_source16_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'change_date')) then
        execute 'alter table gbu_source16_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source16_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source16_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_16_a_txt_pk')) then
    execute 'alter table gbu_source16_a_txt add constraint reg_16_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_16_a_txt_fk_o')) then
	execute 'alter table gbu_source16_a_txt add constraint reg_16_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_16_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_16_a_txt_inx_obj_attr_id on gbu_source16_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source17_a_dt')) then
		execute 'create table gbu_source17_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'id')) then
        execute 'alter table gbu_source17_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'object_id')) then
        execute 'alter table gbu_source17_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source17_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'ot')) then
        execute 'alter table gbu_source17_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 's')) then
        execute 'alter table gbu_source17_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'actual')) then
        execute 'alter table gbu_source17_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'value')) then
        execute 'alter table gbu_source17_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'change_id')) then
        execute 'alter table gbu_source17_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'change_date')) then
        execute 'alter table gbu_source17_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source17_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source17_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_17_a_dt_pk')) then
    execute 'alter table gbu_source17_a_dt add constraint reg_17_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_17_a_dt_fk_o')) then
	execute 'alter table gbu_source17_a_dt add constraint reg_17_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_17_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_17_a_dt_inx_obj_attr_id on gbu_source17_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source17_a_num')) then
		execute 'create table gbu_source17_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'id')) then
        execute 'alter table gbu_source17_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'object_id')) then
        execute 'alter table gbu_source17_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'attribute_id')) then
        execute 'alter table gbu_source17_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'ot')) then
        execute 'alter table gbu_source17_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 's')) then
        execute 'alter table gbu_source17_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'actual')) then
        execute 'alter table gbu_source17_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'value')) then
        execute 'alter table gbu_source17_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'change_id')) then
        execute 'alter table gbu_source17_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'change_date')) then
        execute 'alter table gbu_source17_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'change_user_id')) then
        execute 'alter table gbu_source17_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source17_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_17_a_num_pk')) then
    execute 'alter table gbu_source17_a_num add constraint reg_17_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_17_a_num_fk_o')) then
	execute 'alter table gbu_source17_a_num add constraint reg_17_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_17_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_17_a_num_inx_obj_attr_id on gbu_source17_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source17_a_txt')) then
		execute 'create table gbu_source17_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'id')) then
        execute 'alter table gbu_source17_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'object_id')) then
        execute 'alter table gbu_source17_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source17_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'ot')) then
        execute 'alter table gbu_source17_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 's')) then
        execute 'alter table gbu_source17_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'actual')) then
        execute 'alter table gbu_source17_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source17_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'value')) then
        execute 'alter table gbu_source17_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'change_id')) then
        execute 'alter table gbu_source17_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'change_date')) then
        execute 'alter table gbu_source17_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source17_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source17_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_17_a_txt_pk')) then
    execute 'alter table gbu_source17_a_txt add constraint reg_17_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_17_a_txt_fk_o')) then
	execute 'alter table gbu_source17_a_txt add constraint reg_17_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_17_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_17_a_txt_inx_obj_attr_id on gbu_source17_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source18_a_dt')) then
		execute 'create table gbu_source18_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'id')) then
        execute 'alter table gbu_source18_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'object_id')) then
        execute 'alter table gbu_source18_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source18_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'ot')) then
        execute 'alter table gbu_source18_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 's')) then
        execute 'alter table gbu_source18_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'actual')) then
        execute 'alter table gbu_source18_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'value')) then
        execute 'alter table gbu_source18_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'change_id')) then
        execute 'alter table gbu_source18_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'change_date')) then
        execute 'alter table gbu_source18_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source18_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source18_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_18_a_dt_pk')) then
    execute 'alter table gbu_source18_a_dt add constraint reg_18_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_18_a_dt_fk_o')) then
	execute 'alter table gbu_source18_a_dt add constraint reg_18_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_18_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_18_a_dt_inx_obj_attr_id on gbu_source18_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source18_a_num')) then
		execute 'create table gbu_source18_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'id')) then
        execute 'alter table gbu_source18_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'object_id')) then
        execute 'alter table gbu_source18_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'attribute_id')) then
        execute 'alter table gbu_source18_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'ot')) then
        execute 'alter table gbu_source18_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 's')) then
        execute 'alter table gbu_source18_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'actual')) then
        execute 'alter table gbu_source18_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'value')) then
        execute 'alter table gbu_source18_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'change_id')) then
        execute 'alter table gbu_source18_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'change_date')) then
        execute 'alter table gbu_source18_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'change_user_id')) then
        execute 'alter table gbu_source18_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source18_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_18_a_num_pk')) then
    execute 'alter table gbu_source18_a_num add constraint reg_18_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_18_a_num_fk_o')) then
	execute 'alter table gbu_source18_a_num add constraint reg_18_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_18_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_18_a_num_inx_obj_attr_id on gbu_source18_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source18_a_txt')) then
		execute 'create table gbu_source18_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'id')) then
        execute 'alter table gbu_source18_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'object_id')) then
        execute 'alter table gbu_source18_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source18_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'ot')) then
        execute 'alter table gbu_source18_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 's')) then
        execute 'alter table gbu_source18_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'actual')) then
        execute 'alter table gbu_source18_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source18_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'value')) then
        execute 'alter table gbu_source18_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'change_id')) then
        execute 'alter table gbu_source18_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'change_date')) then
        execute 'alter table gbu_source18_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source18_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source18_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_18_a_txt_pk')) then
    execute 'alter table gbu_source18_a_txt add constraint reg_18_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_18_a_txt_fk_o')) then
	execute 'alter table gbu_source18_a_txt add constraint reg_18_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_18_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_18_a_txt_inx_obj_attr_id on gbu_source18_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source19_a_dt')) then
		execute 'create table gbu_source19_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'id')) then
        execute 'alter table gbu_source19_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'object_id')) then
        execute 'alter table gbu_source19_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source19_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'ot')) then
        execute 'alter table gbu_source19_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 's')) then
        execute 'alter table gbu_source19_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'actual')) then
        execute 'alter table gbu_source19_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'value')) then
        execute 'alter table gbu_source19_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'change_id')) then
        execute 'alter table gbu_source19_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'change_date')) then
        execute 'alter table gbu_source19_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source19_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source19_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_19_a_dt_pk')) then
    execute 'alter table gbu_source19_a_dt add constraint reg_19_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_19_a_dt_fk_o')) then
	execute 'alter table gbu_source19_a_dt add constraint reg_19_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_19_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_19_a_dt_inx_obj_attr_id on gbu_source19_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source19_a_num')) then
		execute 'create table gbu_source19_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'id')) then
        execute 'alter table gbu_source19_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'object_id')) then
        execute 'alter table gbu_source19_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'attribute_id')) then
        execute 'alter table gbu_source19_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'ot')) then
        execute 'alter table gbu_source19_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 's')) then
        execute 'alter table gbu_source19_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'actual')) then
        execute 'alter table gbu_source19_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'value')) then
        execute 'alter table gbu_source19_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'change_id')) then
        execute 'alter table gbu_source19_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'change_date')) then
        execute 'alter table gbu_source19_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'change_user_id')) then
        execute 'alter table gbu_source19_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source19_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_19_a_num_pk')) then
    execute 'alter table gbu_source19_a_num add constraint reg_19_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_19_a_num_fk_o')) then
	execute 'alter table gbu_source19_a_num add constraint reg_19_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_19_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_19_a_num_inx_obj_attr_id on gbu_source19_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source19_a_txt')) then
		execute 'create table gbu_source19_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'id')) then
        execute 'alter table gbu_source19_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'object_id')) then
        execute 'alter table gbu_source19_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source19_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'ot')) then
        execute 'alter table gbu_source19_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 's')) then
        execute 'alter table gbu_source19_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'actual')) then
        execute 'alter table gbu_source19_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source19_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'value')) then
        execute 'alter table gbu_source19_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'change_id')) then
        execute 'alter table gbu_source19_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'change_date')) then
        execute 'alter table gbu_source19_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source19_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source19_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_19_a_txt_pk')) then
    execute 'alter table gbu_source19_a_txt add constraint reg_19_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_19_a_txt_fk_o')) then
	execute 'alter table gbu_source19_a_txt add constraint reg_19_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_19_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_19_a_txt_inx_obj_attr_id on gbu_source19_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source2_a_dt')) then
		execute 'create table gbu_source2_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'id')) then
        execute 'alter table gbu_source2_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'object_id')) then
        execute 'alter table gbu_source2_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source2_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'ot')) then
        execute 'alter table gbu_source2_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 's')) then
        execute 'alter table gbu_source2_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'actual')) then
        execute 'alter table gbu_source2_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'value')) then
        execute 'alter table gbu_source2_a_dt add "value" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'change_id')) then
        execute 'alter table gbu_source2_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'change_date')) then
        execute 'alter table gbu_source2_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source2_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source2_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_2_a_dt_pk')) then
    execute 'alter table gbu_source2_a_dt add constraint reg_2_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_2_a_dt_fk_o')) then
	execute 'alter table gbu_source2_a_dt add constraint reg_2_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_2_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_2_a_dt_inx_obj_attr_id on gbu_source2_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source2_a_num')) then
		execute 'create table gbu_source2_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'id')) then
        execute 'alter table gbu_source2_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'object_id')) then
        execute 'alter table gbu_source2_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'attribute_id')) then
        execute 'alter table gbu_source2_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'ot')) then
        execute 'alter table gbu_source2_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 's')) then
        execute 'alter table gbu_source2_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'actual')) then
        execute 'alter table gbu_source2_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'value')) then
        execute 'alter table gbu_source2_a_num add "value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'change_id')) then
        execute 'alter table gbu_source2_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'change_date')) then
        execute 'alter table gbu_source2_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'change_user_id')) then
        execute 'alter table gbu_source2_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source2_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_2_a_num_pk')) then
    execute 'alter table gbu_source2_a_num add constraint reg_2_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_2_a_num_fk_o')) then
	execute 'alter table gbu_source2_a_num add constraint reg_2_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_2_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_2_a_num_inx_obj_attr_id on gbu_source2_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source2_a_txt')) then
		execute 'create table gbu_source2_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'id')) then
        execute 'alter table gbu_source2_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'object_id')) then
        execute 'alter table gbu_source2_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source2_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'ot')) then
        execute 'alter table gbu_source2_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 's')) then
        execute 'alter table gbu_source2_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'actual')) then
        execute 'alter table gbu_source2_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source2_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'value')) then
        execute 'alter table gbu_source2_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'change_id')) then
        execute 'alter table gbu_source2_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'change_date')) then
        execute 'alter table gbu_source2_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source2_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source2_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_2_a_txt_pk')) then
    execute 'alter table gbu_source2_a_txt add constraint reg_2_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_2_a_txt_fk_o')) then
	execute 'alter table gbu_source2_a_txt add constraint reg_2_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_2_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_2_a_txt_inx_obj_attr_id on gbu_source2_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source20_a_dt')) then
		execute 'create table gbu_source20_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'id')) then
        execute 'alter table gbu_source20_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'object_id')) then
        execute 'alter table gbu_source20_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source20_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'ot')) then
        execute 'alter table gbu_source20_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 's')) then
        execute 'alter table gbu_source20_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'actual')) then
        execute 'alter table gbu_source20_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'value')) then
        execute 'alter table gbu_source20_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'change_id')) then
        execute 'alter table gbu_source20_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'change_date')) then
        execute 'alter table gbu_source20_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source20_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source20_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_20_a_dt_pk')) then
    execute 'alter table gbu_source20_a_dt add constraint reg_20_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_20_a_dt_fk_o')) then
	execute 'alter table gbu_source20_a_dt add constraint reg_20_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_20_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_20_a_dt_inx_obj_attr_id on gbu_source20_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source20_a_num')) then
		execute 'create table gbu_source20_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'id')) then
        execute 'alter table gbu_source20_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'object_id')) then
        execute 'alter table gbu_source20_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'attribute_id')) then
        execute 'alter table gbu_source20_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'ot')) then
        execute 'alter table gbu_source20_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 's')) then
        execute 'alter table gbu_source20_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'actual')) then
        execute 'alter table gbu_source20_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'value')) then
        execute 'alter table gbu_source20_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'change_id')) then
        execute 'alter table gbu_source20_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'change_date')) then
        execute 'alter table gbu_source20_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'change_user_id')) then
        execute 'alter table gbu_source20_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source20_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_20_a_num_pk')) then
    execute 'alter table gbu_source20_a_num add constraint reg_20_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_20_a_num_fk_o')) then
	execute 'alter table gbu_source20_a_num add constraint reg_20_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_20_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_20_a_num_inx_obj_attr_id on gbu_source20_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source20_a_txt')) then
		execute 'create table gbu_source20_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'id')) then
        execute 'alter table gbu_source20_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'object_id')) then
        execute 'alter table gbu_source20_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source20_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'ot')) then
        execute 'alter table gbu_source20_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 's')) then
        execute 'alter table gbu_source20_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'actual')) then
        execute 'alter table gbu_source20_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source20_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'value')) then
        execute 'alter table gbu_source20_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'change_id')) then
        execute 'alter table gbu_source20_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'change_date')) then
        execute 'alter table gbu_source20_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source20_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source20_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source20_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_20_a_txt_pk')) then
    execute 'alter table gbu_source20_a_txt add constraint reg_20_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_20_a_txt_fk_o')) then
	execute 'alter table gbu_source20_a_txt add constraint reg_20_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_20_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_20_a_txt_inx_obj_attr_id on gbu_source20_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source21_a_dt')) then
		execute 'create table gbu_source21_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'id')) then
        execute 'alter table gbu_source21_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'object_id')) then
        execute 'alter table gbu_source21_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source21_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'ot')) then
        execute 'alter table gbu_source21_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 's')) then
        execute 'alter table gbu_source21_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'actual')) then
        execute 'alter table gbu_source21_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'value')) then
        execute 'alter table gbu_source21_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'change_id')) then
        execute 'alter table gbu_source21_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'change_date')) then
        execute 'alter table gbu_source21_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source21_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source21_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_21_a_dt_pk')) then
    execute 'alter table gbu_source21_a_dt add constraint reg_21_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_21_a_dt_fk_o')) then
	execute 'alter table gbu_source21_a_dt add constraint reg_21_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_21_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_21_a_dt_inx_obj_attr_id on gbu_source21_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source21_a_num')) then
		execute 'create table gbu_source21_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'id')) then
        execute 'alter table gbu_source21_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'object_id')) then
        execute 'alter table gbu_source21_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'attribute_id')) then
        execute 'alter table gbu_source21_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'ot')) then
        execute 'alter table gbu_source21_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 's')) then
        execute 'alter table gbu_source21_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'actual')) then
        execute 'alter table gbu_source21_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'value')) then
        execute 'alter table gbu_source21_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'change_id')) then
        execute 'alter table gbu_source21_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'change_date')) then
        execute 'alter table gbu_source21_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'change_user_id')) then
        execute 'alter table gbu_source21_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source21_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_21_a_num_pk')) then
    execute 'alter table gbu_source21_a_num add constraint reg_21_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_21_a_num_fk_o')) then
	execute 'alter table gbu_source21_a_num add constraint reg_21_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_21_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_21_a_num_inx_obj_attr_id on gbu_source21_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source21_a_txt')) then
		execute 'create table gbu_source21_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'id')) then
        execute 'alter table gbu_source21_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'object_id')) then
        execute 'alter table gbu_source21_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source21_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'ot')) then
        execute 'alter table gbu_source21_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 's')) then
        execute 'alter table gbu_source21_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'actual')) then
        execute 'alter table gbu_source21_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source21_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'value')) then
        execute 'alter table gbu_source21_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'change_id')) then
        execute 'alter table gbu_source21_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'change_date')) then
        execute 'alter table gbu_source21_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source21_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source21_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source21_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_21_a_txt_pk')) then
    execute 'alter table gbu_source21_a_txt add constraint reg_21_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_21_a_txt_fk_o')) then
	execute 'alter table gbu_source21_a_txt add constraint reg_21_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_21_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_21_a_txt_inx_obj_attr_id on gbu_source21_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source22_a_dt')) then
		execute 'create table gbu_source22_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'id')) then
        execute 'alter table gbu_source22_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'object_id')) then
        execute 'alter table gbu_source22_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source22_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'ot')) then
        execute 'alter table gbu_source22_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 's')) then
        execute 'alter table gbu_source22_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'actual')) then
        execute 'alter table gbu_source22_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'value')) then
        execute 'alter table gbu_source22_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'change_id')) then
        execute 'alter table gbu_source22_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'change_date')) then
        execute 'alter table gbu_source22_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source22_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source22_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_22_a_dt_pk')) then
    execute 'alter table gbu_source22_a_dt add constraint reg_22_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_22_a_dt_fk_o')) then
	execute 'alter table gbu_source22_a_dt add constraint reg_22_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_22_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_22_a_dt_inx_obj_attr_id on gbu_source22_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source22_a_num')) then
		execute 'create table gbu_source22_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'id')) then
        execute 'alter table gbu_source22_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'object_id')) then
        execute 'alter table gbu_source22_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'attribute_id')) then
        execute 'alter table gbu_source22_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'ot')) then
        execute 'alter table gbu_source22_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 's')) then
        execute 'alter table gbu_source22_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'actual')) then
        execute 'alter table gbu_source22_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'value')) then
        execute 'alter table gbu_source22_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'change_id')) then
        execute 'alter table gbu_source22_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'change_date')) then
        execute 'alter table gbu_source22_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'change_user_id')) then
        execute 'alter table gbu_source22_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source22_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_22_a_num_pk')) then
    execute 'alter table gbu_source22_a_num add constraint reg_22_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_22_a_num_fk_o')) then
	execute 'alter table gbu_source22_a_num add constraint reg_22_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_22_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_22_a_num_inx_obj_attr_id on gbu_source22_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source22_a_txt')) then
		execute 'create table gbu_source22_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'id')) then
        execute 'alter table gbu_source22_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'object_id')) then
        execute 'alter table gbu_source22_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source22_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'ot')) then
        execute 'alter table gbu_source22_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 's')) then
        execute 'alter table gbu_source22_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'actual')) then
        execute 'alter table gbu_source22_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source22_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'value')) then
        execute 'alter table gbu_source22_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'change_id')) then
        execute 'alter table gbu_source22_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'change_date')) then
        execute 'alter table gbu_source22_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source22_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source22_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source22_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_22_a_txt_pk')) then
    execute 'alter table gbu_source22_a_txt add constraint reg_22_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_22_a_txt_fk_o')) then
	execute 'alter table gbu_source22_a_txt add constraint reg_22_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_22_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_22_a_txt_inx_obj_attr_id on gbu_source22_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source23_a_dt')) then
		execute 'create table gbu_source23_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'id')) then
        execute 'alter table gbu_source23_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'object_id')) then
        execute 'alter table gbu_source23_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source23_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'ot')) then
        execute 'alter table gbu_source23_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 's')) then
        execute 'alter table gbu_source23_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'actual')) then
        execute 'alter table gbu_source23_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'value')) then
        execute 'alter table gbu_source23_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'change_id')) then
        execute 'alter table gbu_source23_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'change_date')) then
        execute 'alter table gbu_source23_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source23_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source23_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_23_a_dt_pk')) then
    execute 'alter table gbu_source23_a_dt add constraint reg_23_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_23_a_dt_fk_o')) then
	execute 'alter table gbu_source23_a_dt add constraint reg_23_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_23_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_23_a_dt_inx_obj_attr_id on gbu_source23_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source23_a_num')) then
		execute 'create table gbu_source23_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'id')) then
        execute 'alter table gbu_source23_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'object_id')) then
        execute 'alter table gbu_source23_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'attribute_id')) then
        execute 'alter table gbu_source23_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'ot')) then
        execute 'alter table gbu_source23_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 's')) then
        execute 'alter table gbu_source23_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'actual')) then
        execute 'alter table gbu_source23_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'value')) then
        execute 'alter table gbu_source23_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'change_id')) then
        execute 'alter table gbu_source23_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'change_date')) then
        execute 'alter table gbu_source23_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'change_user_id')) then
        execute 'alter table gbu_source23_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source23_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_23_a_num_pk')) then
    execute 'alter table gbu_source23_a_num add constraint reg_23_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_23_a_num_fk_o')) then
	execute 'alter table gbu_source23_a_num add constraint reg_23_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_23_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_23_a_num_inx_obj_attr_id on gbu_source23_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source23_a_txt')) then
		execute 'create table gbu_source23_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'id')) then
        execute 'alter table gbu_source23_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'object_id')) then
        execute 'alter table gbu_source23_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source23_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'ot')) then
        execute 'alter table gbu_source23_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 's')) then
        execute 'alter table gbu_source23_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'actual')) then
        execute 'alter table gbu_source23_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source23_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'value')) then
        execute 'alter table gbu_source23_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'change_id')) then
        execute 'alter table gbu_source23_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'change_date')) then
        execute 'alter table gbu_source23_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source23_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source23_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source23_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_23_a_txt_pk')) then
    execute 'alter table gbu_source23_a_txt add constraint reg_23_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_23_a_txt_fk_o')) then
	execute 'alter table gbu_source23_a_txt add constraint reg_23_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_23_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_23_a_txt_inx_obj_attr_id on gbu_source23_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source3_a_dt')) then
		execute 'create table gbu_source3_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'id')) then
        execute 'alter table gbu_source3_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'object_id')) then
        execute 'alter table gbu_source3_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source3_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'ot')) then
        execute 'alter table gbu_source3_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 's')) then
        execute 'alter table gbu_source3_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'actual')) then
        execute 'alter table gbu_source3_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'value')) then
        execute 'alter table gbu_source3_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'change_id')) then
        execute 'alter table gbu_source3_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'change_date')) then
        execute 'alter table gbu_source3_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source3_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source3_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_3_a_dt_pk')) then
    execute 'alter table gbu_source3_a_dt add constraint reg_3_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_3_a_dt_fk_o')) then
	execute 'alter table gbu_source3_a_dt add constraint reg_3_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_3_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_3_a_dt_inx_obj_attr_id on gbu_source3_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source3_a_num')) then
		execute 'create table gbu_source3_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'id')) then
        execute 'alter table gbu_source3_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'object_id')) then
        execute 'alter table gbu_source3_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'attribute_id')) then
        execute 'alter table gbu_source3_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'ot')) then
        execute 'alter table gbu_source3_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 's')) then
        execute 'alter table gbu_source3_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'actual')) then
        execute 'alter table gbu_source3_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'value')) then
        execute 'alter table gbu_source3_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'change_id')) then
        execute 'alter table gbu_source3_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'change_date')) then
        execute 'alter table gbu_source3_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'change_user_id')) then
        execute 'alter table gbu_source3_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source3_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_3_a_num_pk')) then
    execute 'alter table gbu_source3_a_num add constraint reg_3_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_3_a_num_fk_o')) then
	execute 'alter table gbu_source3_a_num add constraint reg_3_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_3_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_3_a_num_inx_obj_attr_id on gbu_source3_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source3_a_txt')) then
		execute 'create table gbu_source3_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'id')) then
        execute 'alter table gbu_source3_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'object_id')) then
        execute 'alter table gbu_source3_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source3_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'ot')) then
        execute 'alter table gbu_source3_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 's')) then
        execute 'alter table gbu_source3_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'actual')) then
        execute 'alter table gbu_source3_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source3_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'value')) then
        execute 'alter table gbu_source3_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'change_id')) then
        execute 'alter table gbu_source3_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'change_date')) then
        execute 'alter table gbu_source3_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source3_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source3_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source3_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_3_a_txt_pk')) then
    execute 'alter table gbu_source3_a_txt add constraint reg_3_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_3_a_txt_fk_o')) then
	execute 'alter table gbu_source3_a_txt add constraint reg_3_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_3_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_3_a_txt_inx_obj_attr_id on gbu_source3_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source4_a_dt')) then
		execute 'create table gbu_source4_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'id')) then
        execute 'alter table gbu_source4_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'object_id')) then
        execute 'alter table gbu_source4_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source4_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'ot')) then
        execute 'alter table gbu_source4_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 's')) then
        execute 'alter table gbu_source4_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'actual')) then
        execute 'alter table gbu_source4_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'value')) then
        execute 'alter table gbu_source4_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'change_id')) then
        execute 'alter table gbu_source4_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'change_date')) then
        execute 'alter table gbu_source4_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source4_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source4_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_4_a_dt_pk')) then
    execute 'alter table gbu_source4_a_dt add constraint reg_4_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_4_a_dt_fk_o')) then
	execute 'alter table gbu_source4_a_dt add constraint reg_4_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_4_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_4_a_dt_inx_obj_attr_id on gbu_source4_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source4_a_num')) then
		execute 'create table gbu_source4_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'id')) then
        execute 'alter table gbu_source4_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'object_id')) then
        execute 'alter table gbu_source4_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'attribute_id')) then
        execute 'alter table gbu_source4_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'ot')) then
        execute 'alter table gbu_source4_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 's')) then
        execute 'alter table gbu_source4_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'actual')) then
        execute 'alter table gbu_source4_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'value')) then
        execute 'alter table gbu_source4_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'change_id')) then
        execute 'alter table gbu_source4_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'change_date')) then
        execute 'alter table gbu_source4_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'change_user_id')) then
        execute 'alter table gbu_source4_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source4_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_4_a_num_pk')) then
    execute 'alter table gbu_source4_a_num add constraint reg_4_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_4_a_num_fk_o')) then
	execute 'alter table gbu_source4_a_num add constraint reg_4_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_4_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_4_a_num_inx_obj_attr_id on gbu_source4_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source4_a_txt')) then
		execute 'create table gbu_source4_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'id')) then
        execute 'alter table gbu_source4_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'object_id')) then
        execute 'alter table gbu_source4_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source4_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'ot')) then
        execute 'alter table gbu_source4_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 's')) then
        execute 'alter table gbu_source4_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'actual')) then
        execute 'alter table gbu_source4_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source4_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'value')) then
        execute 'alter table gbu_source4_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'change_id')) then
        execute 'alter table gbu_source4_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'change_date')) then
        execute 'alter table gbu_source4_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source4_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source4_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source4_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_4_a_txt_pk')) then
    execute 'alter table gbu_source4_a_txt add constraint reg_4_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_4_a_txt_fk_o')) then
	execute 'alter table gbu_source4_a_txt add constraint reg_4_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_4_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_4_a_txt_inx_obj_attr_id on gbu_source4_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source5_a_dt')) then
		execute 'create table gbu_source5_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'id')) then
        execute 'alter table gbu_source5_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'object_id')) then
        execute 'alter table gbu_source5_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source5_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'ot')) then
        execute 'alter table gbu_source5_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 's')) then
        execute 'alter table gbu_source5_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'actual')) then
        execute 'alter table gbu_source5_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'value')) then
        execute 'alter table gbu_source5_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'change_id')) then
        execute 'alter table gbu_source5_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'change_date')) then
        execute 'alter table gbu_source5_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source5_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source5_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_5_a_dt_pk')) then
    execute 'alter table gbu_source5_a_dt add constraint reg_5_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_5_a_dt_fk_o')) then
	execute 'alter table gbu_source5_a_dt add constraint reg_5_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_5_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_5_a_dt_inx_obj_attr_id on gbu_source5_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source5_a_num')) then
		execute 'create table gbu_source5_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'id')) then
        execute 'alter table gbu_source5_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'object_id')) then
        execute 'alter table gbu_source5_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'attribute_id')) then
        execute 'alter table gbu_source5_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'ot')) then
        execute 'alter table gbu_source5_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 's')) then
        execute 'alter table gbu_source5_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'actual')) then
        execute 'alter table gbu_source5_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'value')) then
        execute 'alter table gbu_source5_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'change_id')) then
        execute 'alter table gbu_source5_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'change_date')) then
        execute 'alter table gbu_source5_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'change_user_id')) then
        execute 'alter table gbu_source5_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source5_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_5_a_num_pk')) then
    execute 'alter table gbu_source5_a_num add constraint reg_5_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_5_a_num_fk_o')) then
	execute 'alter table gbu_source5_a_num add constraint reg_5_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_5_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_5_a_num_inx_obj_attr_id on gbu_source5_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source5_a_txt')) then
		execute 'create table gbu_source5_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'id')) then
        execute 'alter table gbu_source5_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'object_id')) then
        execute 'alter table gbu_source5_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source5_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'ot')) then
        execute 'alter table gbu_source5_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 's')) then
        execute 'alter table gbu_source5_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'actual')) then
        execute 'alter table gbu_source5_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source5_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'value')) then
        execute 'alter table gbu_source5_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'change_id')) then
        execute 'alter table gbu_source5_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'change_date')) then
        execute 'alter table gbu_source5_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source5_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source5_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source5_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_5_a_txt_pk')) then
    execute 'alter table gbu_source5_a_txt add constraint reg_5_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_5_a_txt_fk_o')) then
	execute 'alter table gbu_source5_a_txt add constraint reg_5_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_5_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_5_a_txt_inx_obj_attr_id on gbu_source5_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source6_a_dt')) then
		execute 'create table gbu_source6_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'id')) then
        execute 'alter table gbu_source6_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'object_id')) then
        execute 'alter table gbu_source6_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source6_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'ot')) then
        execute 'alter table gbu_source6_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 's')) then
        execute 'alter table gbu_source6_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'actual')) then
        execute 'alter table gbu_source6_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'value')) then
        execute 'alter table gbu_source6_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'change_id')) then
        execute 'alter table gbu_source6_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'change_date')) then
        execute 'alter table gbu_source6_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source6_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source6_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_6_a_dt_pk')) then
    execute 'alter table gbu_source6_a_dt add constraint reg_6_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_6_a_dt_fk_o')) then
	execute 'alter table gbu_source6_a_dt add constraint reg_6_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_6_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_6_a_dt_inx_obj_attr_id on gbu_source6_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source6_a_num')) then
		execute 'create table gbu_source6_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'id')) then
        execute 'alter table gbu_source6_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'object_id')) then
        execute 'alter table gbu_source6_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'attribute_id')) then
        execute 'alter table gbu_source6_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'ot')) then
        execute 'alter table gbu_source6_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 's')) then
        execute 'alter table gbu_source6_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'actual')) then
        execute 'alter table gbu_source6_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'value')) then
        execute 'alter table gbu_source6_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'change_id')) then
        execute 'alter table gbu_source6_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'change_date')) then
        execute 'alter table gbu_source6_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'change_user_id')) then
        execute 'alter table gbu_source6_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source6_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_6_a_num_pk')) then
    execute 'alter table gbu_source6_a_num add constraint reg_6_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_6_a_num_fk_o')) then
	execute 'alter table gbu_source6_a_num add constraint reg_6_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_6_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_6_a_num_inx_obj_attr_id on gbu_source6_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source6_a_txt')) then
		execute 'create table gbu_source6_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'id')) then
        execute 'alter table gbu_source6_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'object_id')) then
        execute 'alter table gbu_source6_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source6_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'ot')) then
        execute 'alter table gbu_source6_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 's')) then
        execute 'alter table gbu_source6_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'actual')) then
        execute 'alter table gbu_source6_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source6_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'value')) then
        execute 'alter table gbu_source6_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'change_id')) then
        execute 'alter table gbu_source6_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'change_date')) then
        execute 'alter table gbu_source6_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source6_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source6_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source6_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_6_a_txt_pk')) then
    execute 'alter table gbu_source6_a_txt add constraint reg_6_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_6_a_txt_fk_o')) then
	execute 'alter table gbu_source6_a_txt add constraint reg_6_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_6_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_6_a_txt_inx_obj_attr_id on gbu_source6_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source7_a_dt')) then
		execute 'create table gbu_source7_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'id')) then
        execute 'alter table gbu_source7_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'object_id')) then
        execute 'alter table gbu_source7_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source7_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'ot')) then
        execute 'alter table gbu_source7_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 's')) then
        execute 'alter table gbu_source7_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'actual')) then
        execute 'alter table gbu_source7_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'value')) then
        execute 'alter table gbu_source7_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'change_id')) then
        execute 'alter table gbu_source7_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'change_date')) then
        execute 'alter table gbu_source7_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source7_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source7_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_7_a_dt_pk')) then
    execute 'alter table gbu_source7_a_dt add constraint reg_7_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_7_a_dt_fk_o')) then
	execute 'alter table gbu_source7_a_dt add constraint reg_7_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_7_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_7_a_dt_inx_obj_attr_id on gbu_source7_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source7_a_num')) then
		execute 'create table gbu_source7_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'id')) then
        execute 'alter table gbu_source7_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'object_id')) then
        execute 'alter table gbu_source7_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'attribute_id')) then
        execute 'alter table gbu_source7_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'ot')) then
        execute 'alter table gbu_source7_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 's')) then
        execute 'alter table gbu_source7_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'actual')) then
        execute 'alter table gbu_source7_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'value')) then
        execute 'alter table gbu_source7_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'change_id')) then
        execute 'alter table gbu_source7_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'change_date')) then
        execute 'alter table gbu_source7_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'change_user_id')) then
        execute 'alter table gbu_source7_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source7_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_7_a_num_pk')) then
    execute 'alter table gbu_source7_a_num add constraint reg_7_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_7_a_num_fk_o')) then
	execute 'alter table gbu_source7_a_num add constraint reg_7_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_7_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_7_a_num_inx_obj_attr_id on gbu_source7_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source7_a_txt')) then
		execute 'create table gbu_source7_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'id')) then
        execute 'alter table gbu_source7_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'object_id')) then
        execute 'alter table gbu_source7_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source7_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'ot')) then
        execute 'alter table gbu_source7_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 's')) then
        execute 'alter table gbu_source7_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'actual')) then
        execute 'alter table gbu_source7_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source7_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'value')) then
        execute 'alter table gbu_source7_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'change_id')) then
        execute 'alter table gbu_source7_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'change_date')) then
        execute 'alter table gbu_source7_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source7_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source7_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source7_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_7_a_txt_pk')) then
    execute 'alter table gbu_source7_a_txt add constraint reg_7_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_7_a_txt_fk_o')) then
	execute 'alter table gbu_source7_a_txt add constraint reg_7_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_7_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_7_a_txt_inx_obj_attr_id on gbu_source7_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source8_a_dt')) then
		execute 'create table gbu_source8_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'id')) then
        execute 'alter table gbu_source8_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'object_id')) then
        execute 'alter table gbu_source8_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source8_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'ot')) then
        execute 'alter table gbu_source8_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 's')) then
        execute 'alter table gbu_source8_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'actual')) then
        execute 'alter table gbu_source8_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'value')) then
        execute 'alter table gbu_source8_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'change_id')) then
        execute 'alter table gbu_source8_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'change_date')) then
        execute 'alter table gbu_source8_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source8_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source8_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_8_a_dt_pk')) then
    execute 'alter table gbu_source8_a_dt add constraint reg_8_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_8_a_dt_fk_o')) then
	execute 'alter table gbu_source8_a_dt add constraint reg_8_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_8_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_8_a_dt_inx_obj_attr_id on gbu_source8_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source8_a_num')) then
		execute 'create table gbu_source8_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'id')) then
        execute 'alter table gbu_source8_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'object_id')) then
        execute 'alter table gbu_source8_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'attribute_id')) then
        execute 'alter table gbu_source8_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'ot')) then
        execute 'alter table gbu_source8_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 's')) then
        execute 'alter table gbu_source8_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'actual')) then
        execute 'alter table gbu_source8_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'value')) then
        execute 'alter table gbu_source8_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'change_id')) then
        execute 'alter table gbu_source8_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'change_date')) then
        execute 'alter table gbu_source8_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'change_user_id')) then
        execute 'alter table gbu_source8_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source8_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_8_a_num_pk')) then
    execute 'alter table gbu_source8_a_num add constraint reg_8_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_8_a_num_fk_o')) then
	execute 'alter table gbu_source8_a_num add constraint reg_8_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_8_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_8_a_num_inx_obj_attr_id on gbu_source8_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source8_a_txt')) then
		execute 'create table gbu_source8_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'id')) then
        execute 'alter table gbu_source8_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'object_id')) then
        execute 'alter table gbu_source8_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source8_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'ot')) then
        execute 'alter table gbu_source8_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 's')) then
        execute 'alter table gbu_source8_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'actual')) then
        execute 'alter table gbu_source8_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source8_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'value')) then
        execute 'alter table gbu_source8_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'change_id')) then
        execute 'alter table gbu_source8_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'change_date')) then
        execute 'alter table gbu_source8_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source8_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source8_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source8_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_8_a_txt_pk')) then
    execute 'alter table gbu_source8_a_txt add constraint reg_8_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_8_a_txt_fk_o')) then
	execute 'alter table gbu_source8_a_txt add constraint reg_8_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_8_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_8_a_txt_inx_obj_attr_id on gbu_source8_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source9_a_dt')) then
		execute 'create table gbu_source9_a_dt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'id')) then
        execute 'alter table gbu_source9_a_dt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'object_id')) then
        execute 'alter table gbu_source9_a_dt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'attribute_id')) then
        execute 'alter table gbu_source9_a_dt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'ot')) then
        execute 'alter table gbu_source9_a_dt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 's')) then
        execute 'alter table gbu_source9_a_dt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'actual')) then
        execute 'alter table gbu_source9_a_dt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'value')) then
        execute 'alter table gbu_source9_a_dt add "value" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'change_id')) then
        execute 'alter table gbu_source9_a_dt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'change_date')) then
        execute 'alter table gbu_source9_a_dt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'change_user_id')) then
        execute 'alter table gbu_source9_a_dt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_dt', 'change_doc_id')) then
        execute 'alter table gbu_source9_a_dt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_9_a_dt_pk')) then
    execute 'alter table gbu_source9_a_dt add constraint reg_9_a_dt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_9_a_dt_fk_o')) then
	execute 'alter table gbu_source9_a_dt add constraint reg_9_a_dt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_9_a_dt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_9_a_dt_inx_obj_attr_id on gbu_source9_a_dt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source9_a_num')) then
		execute 'create table gbu_source9_a_num ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'id')) then
        execute 'alter table gbu_source9_a_num add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'object_id')) then
        execute 'alter table gbu_source9_a_num add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'attribute_id')) then
        execute 'alter table gbu_source9_a_num add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'ot')) then
        execute 'alter table gbu_source9_a_num add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 's')) then
        execute 'alter table gbu_source9_a_num add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'actual')) then
        execute 'alter table gbu_source9_a_num add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'value')) then
        execute 'alter table gbu_source9_a_num add "value" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'change_id')) then
        execute 'alter table gbu_source9_a_num add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'change_date')) then
        execute 'alter table gbu_source9_a_num add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'change_user_id')) then
        execute 'alter table gbu_source9_a_num add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_num', 'change_doc_id')) then
        execute 'alter table gbu_source9_a_num add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_9_a_num_pk')) then
    execute 'alter table gbu_source9_a_num add constraint reg_9_a_num_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_9_a_num_fk_o')) then
	execute 'alter table gbu_source9_a_num add constraint reg_9_a_num_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_9_a_num_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_9_a_num_inx_obj_attr_id on gbu_source9_a_num (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source9_a_txt')) then
		execute 'create table gbu_source9_a_txt ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'id')) then
        execute 'alter table gbu_source9_a_txt add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'object_id')) then
        execute 'alter table gbu_source9_a_txt add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'attribute_id')) then
        execute 'alter table gbu_source9_a_txt add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'ot')) then
        execute 'alter table gbu_source9_a_txt add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 's')) then
        execute 'alter table gbu_source9_a_txt add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'actual')) then
        execute 'alter table gbu_source9_a_txt add "actual" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'ref_item_id')) then
        execute 'alter table gbu_source9_a_txt add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'value')) then
        execute 'alter table gbu_source9_a_txt add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'change_id')) then
        execute 'alter table gbu_source9_a_txt add "change_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'change_date')) then
        execute 'alter table gbu_source9_a_txt add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'change_user_id')) then
        execute 'alter table gbu_source9_a_txt add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source9_a_txt', 'change_doc_id')) then
        execute 'alter table gbu_source9_a_txt add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_9_a_txt_pk')) then
    execute 'alter table gbu_source9_a_txt add constraint reg_9_a_txt_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_9_a_txt_fk_o')) then
	execute 'alter table gbu_source9_a_txt add constraint reg_9_a_txt_fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_9_a_txt_inx_obj_attr_id')) then
	execute 'CREATE  INDEX reg_9_a_txt_inx_obj_attr_id on gbu_source9_a_txt (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_ATTRIBUTE_MAP')) then
		execute 'create table KO_ATTRIBUTE_MAP ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_ATTRIBUTE_MAP', 'id')) then
        execute 'alter table KO_ATTRIBUTE_MAP add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_ATTRIBUTE_MAP', 'gbu_attribute_id')) then
        execute 'alter table KO_ATTRIBUTE_MAP add "gbu_attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_ATTRIBUTE_MAP', 'ko_attribute_id')) then
        execute 'alter table KO_ATTRIBUTE_MAP add "ko_attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_213_q_pk')) then
    execute 'alter table KO_ATTRIBUTE_MAP add constraint reg_213_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_COD_DICTIONARY')) then
		execute 'create table KO_COD_DICTIONARY ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_DICTIONARY', 'id')) then
        execute 'alter table KO_COD_DICTIONARY add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_DICTIONARY', 'id_codjob')) then
        execute 'alter table KO_COD_DICTIONARY add "id_codjob" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_DICTIONARY', 'value')) then
        execute 'alter table KO_COD_DICTIONARY add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_DICTIONARY', 'code')) then
        execute 'alter table KO_COD_DICTIONARY add "code" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_DICTIONARY', 'source')) then
        execute 'alter table KO_COD_DICTIONARY add "source" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_DICTIONARY', 'expert')) then
        execute 'alter table KO_COD_DICTIONARY add "expert" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_214_q_pk')) then
    execute 'alter table KO_COD_DICTIONARY add constraint reg_214_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_COD_JOB')) then
		execute 'create table KO_COD_JOB ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_JOB', 'id')) then
        execute 'alter table KO_COD_JOB add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_JOB', 'name_job')) then
        execute 'alter table KO_COD_JOB add "name_job" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COD_JOB', 'result_job')) then
        execute 'alter table KO_COD_JOB add "result_job" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_215_q_pk')) then
    execute 'alter table KO_COD_JOB add constraint reg_215_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_COST_ROSREESTR')) then
		execute 'create table KO_COST_ROSREESTR ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'id')) then
        execute 'alter table KO_COST_ROSREESTR add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'id_object')) then
        execute 'alter table KO_COST_ROSREESTR add "id_object" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'datevaluation')) then
        execute 'alter table KO_COST_ROSREESTR add "datevaluation" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'dateentering')) then
        execute 'alter table KO_COST_ROSREESTR add "dateentering" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'docnumber')) then
        execute 'alter table KO_COST_ROSREESTR add "docnumber" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'docdate')) then
        execute 'alter table KO_COST_ROSREESTR add "docdate" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'applicationdate')) then
        execute 'alter table KO_COST_ROSREESTR add "applicationdate" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'docname')) then
        execute 'alter table KO_COST_ROSREESTR add "docname" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'costvalue')) then
        execute 'alter table KO_COST_ROSREESTR add "costvalue" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'dateapproval')) then
        execute 'alter table KO_COST_ROSREESTR add "dateapproval" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_COST_ROSREESTR', 'revisalstatementdate')) then
        execute 'alter table KO_COST_ROSREESTR add "revisalstatementdate" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_216_q_pk')) then
    execute 'alter table KO_COST_ROSREESTR add constraint reg_216_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_ETALON')) then
		execute 'create table KO_ETALON ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_ETALON', 'id')) then
        execute 'alter table KO_ETALON add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_ETALON', 'cadastraldistrict')) then
        execute 'alter table KO_ETALON add "cadastraldistrict" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_ETALON', 'cadastralnumber')) then
        execute 'alter table KO_ETALON add "cadastralnumber" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_ETALON', 'group_id')) then
        execute 'alter table KO_ETALON add "group_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_218_q_pk')) then
    execute 'alter table KO_ETALON add constraint reg_218_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_EXPLICATION')) then
		execute 'create table KO_EXPLICATION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_EXPLICATION', 'id')) then
        execute 'alter table KO_EXPLICATION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_EXPLICATION', 'object_id')) then
        execute 'alter table KO_EXPLICATION add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_EXPLICATION', 'group_id')) then
        execute 'alter table KO_EXPLICATION add "group_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_EXPLICATION', 'square')) then
        execute 'alter table KO_EXPLICATION add "square" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_EXPLICATION', 'upks')) then
        execute 'alter table KO_EXPLICATION add "upks" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_EXPLICATION', 'kc')) then
        execute 'alter table KO_EXPLICATION add "kc" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_EXPLICATION', 'upks_analog')) then
        execute 'alter table KO_EXPLICATION add "upks_analog" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_EXPLICATION', 'name_analog')) then
        execute 'alter table KO_EXPLICATION add "name_analog" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_217_q_pk')) then
    execute 'alter table KO_EXPLICATION add constraint reg_217_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_GROUP')) then
		execute 'create table KO_GROUP ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_GROUP', 'id')) then
        execute 'alter table KO_GROUP add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_GROUP', 'group_name')) then
        execute 'alter table KO_GROUP add "group_name" VARCHAR(4000) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_GROUP', 'group_algoritm')) then
        execute 'alter table KO_GROUP add "group_algoritm" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_GROUP', 'parent_id')) then
        execute 'alter table KO_GROUP add "parent_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_205_q_pk')) then
    execute 'alter table KO_GROUP add constraint reg_205_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_GROUP_FACTOR')) then
		execute 'create table KO_GROUP_FACTOR ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_GROUP_FACTOR', 'id')) then
        execute 'alter table KO_GROUP_FACTOR add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_GROUP_FACTOR', 'group_id')) then
        execute 'alter table KO_GROUP_FACTOR add "group_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_GROUP_FACTOR', 'factor_id')) then
        execute 'alter table KO_GROUP_FACTOR add "factor_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_GROUP_FACTOR', 'sign_narket')) then
        execute 'alter table KO_GROUP_FACTOR add "sign_narket" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_208_q_pk')) then
    execute 'alter table KO_GROUP_FACTOR add constraint reg_208_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_MARK_CATALOG')) then
		execute 'create table KO_MARK_CATALOG ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MARK_CATALOG', 'id')) then
        execute 'alter table KO_MARK_CATALOG add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MARK_CATALOG', 'group_id')) then
        execute 'alter table KO_MARK_CATALOG add "group_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MARK_CATALOG', 'factor_id')) then
        execute 'alter table KO_MARK_CATALOG add "factor_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MARK_CATALOG', 'value_factor')) then
        execute 'alter table KO_MARK_CATALOG add "value_factor" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MARK_CATALOG', 'metka_factor')) then
        execute 'alter table KO_MARK_CATALOG add "metka_factor" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_211_q_pk')) then
    execute 'alter table KO_MARK_CATALOG add constraint reg_211_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_MODEL')) then
		execute 'create table KO_MODEL ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL', 'id')) then
        execute 'alter table KO_MODEL add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL', 'algoritm_type')) then
        execute 'alter table KO_MODEL add "algoritm_type" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL', 'formula')) then
        execute 'alter table KO_MODEL add "formula" VARCHAR(4000) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL', 'description')) then
        execute 'alter table KO_MODEL add "description" VARCHAR(4000) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL', 'name')) then
        execute 'alter table KO_MODEL add "name" VARCHAR(4000) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL', 'group_id')) then
        execute 'alter table KO_MODEL add "group_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL', 'a0')) then
        execute 'alter table KO_MODEL add "a0" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_206_q_pk')) then
    execute 'alter table KO_MODEL add constraint reg_206_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_MODEL_FACTOR')) then
		execute 'create table KO_MODEL_FACTOR ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'id')) then
        execute 'alter table KO_MODEL_FACTOR add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'model_id')) then
        execute 'alter table KO_MODEL_FACTOR add "model_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'factor_id')) then
        execute 'alter table KO_MODEL_FACTOR add "factor_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'marker_id')) then
        execute 'alter table KO_MODEL_FACTOR add "marker_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'weight')) then
        execute 'alter table KO_MODEL_FACTOR add "weight" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'b0')) then
        execute 'alter table KO_MODEL_FACTOR add "b0" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'sign_div')) then
        execute 'alter table KO_MODEL_FACTOR add "sign_div" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'sign_add')) then
        execute 'alter table KO_MODEL_FACTOR add "sign_add" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_MODEL_FACTOR', 'sign_market')) then
        execute 'alter table KO_MODEL_FACTOR add "sign_market" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_210_q_pk')) then
    execute 'alter table KO_MODEL_FACTOR add constraint reg_210_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_TASK')) then
		execute 'create table KO_TASK ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'id')) then
        execute 'alter table KO_TASK add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'creation_date')) then
        execute 'alter table KO_TASK add "creation_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'tour_id')) then
        execute 'alter table KO_TASK add "tour_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'note_type')) then
        execute 'alter table KO_TASK add "note_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'note_type_code')) then
        execute 'alter table KO_TASK add "note_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'status')) then
        execute 'alter table KO_TASK add "status" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'status_code')) then
        execute 'alter table KO_TASK add "status_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'response_document_id')) then
        execute 'alter table KO_TASK add "response_document_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TASK', 'document_id')) then
        execute 'alter table KO_TASK add "document_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_203_q_pk')) then
    execute 'alter table KO_TASK add constraint reg_203_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_TOUR')) then
		execute 'create table KO_TOUR ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TOUR', 'id')) then
        execute 'alter table KO_TOUR add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TOUR', 'year')) then
        execute 'alter table KO_TOUR add "year" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_202_q_pk')) then
    execute 'alter table KO_TOUR add constraint reg_202_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('ko_tour_factor_register')) then
		execute 'create table ko_tour_factor_register ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ko_tour_factor_register', 'id')) then
        execute 'alter table ko_tour_factor_register add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ko_tour_factor_register', 'tour_id')) then
        execute 'alter table ko_tour_factor_register add "tour_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ko_tour_factor_register', 'object_type')) then
        execute 'alter table ko_tour_factor_register add "object_type" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ko_tour_factor_register', 'register_id')) then
        execute 'alter table ko_tour_factor_register add "register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('ko_tour_factor_register_pkey')) then
    execute 'alter table ko_tour_factor_register add constraint ko_tour_factor_register_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_TOUR_GROUPS')) then
		execute 'create table KO_TOUR_GROUPS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TOUR_GROUPS', 'id')) then
        execute 'alter table KO_TOUR_GROUPS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TOUR_GROUPS', 'tour_id')) then
        execute 'alter table KO_TOUR_GROUPS add "tour_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_TOUR_GROUPS', 'group_id')) then
        execute 'alter table KO_TOUR_GROUPS add "group_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_212_q_pk')) then
    execute 'alter table KO_TOUR_GROUPS add constraint reg_212_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_UNIT')) then
		execute 'create table KO_UNIT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'id')) then
        execute 'alter table KO_UNIT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'object_id')) then
        execute 'alter table KO_UNIT add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'tour_id')) then
        execute 'alter table KO_UNIT add "tour_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'task_id')) then
        execute 'alter table KO_UNIT add "task_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'model_id')) then
        execute 'alter table KO_UNIT add "model_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'group_id')) then
        execute 'alter table KO_UNIT add "group_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'status')) then
        execute 'alter table KO_UNIT add "status" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'status_code')) then
        execute 'alter table KO_UNIT add "status_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'creation_date')) then
        execute 'alter table KO_UNIT add "creation_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'cadastral_cost')) then
        execute 'alter table KO_UNIT add "cadastral_cost" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'upks')) then
        execute 'alter table KO_UNIT add "upks" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'cadastral_cost_pre')) then
        execute 'alter table KO_UNIT add "cadastral_cost_pre" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'upks_pre')) then
        execute 'alter table KO_UNIT add "upks_pre" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'status_result_calc')) then
        execute 'alter table KO_UNIT add "status_result_calc" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'status_result_calc_code')) then
        execute 'alter table KO_UNIT add "status_result_calc_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'status_repeat_calc')) then
        execute 'alter table KO_UNIT add "status_repeat_calc" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'status_repeat_calc_code')) then
        execute 'alter table KO_UNIT add "status_repeat_calc_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'parent_calc_type_code')) then
        execute 'alter table KO_UNIT add "parent_calc_type_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'square')) then
        execute 'alter table KO_UNIT add "square" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'cadastral_number')) then
        execute 'alter table KO_UNIT add "cadastral_number" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'cadastral_block')) then
        execute 'alter table KO_UNIT add "cadastral_block" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'property_type')) then
        execute 'alter table KO_UNIT add "property_type" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'property_type_code')) then
        execute 'alter table KO_UNIT add "property_type_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'degree_readiness')) then
        execute 'alter table KO_UNIT add "degree_readiness" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'parent_calc_number')) then
        execute 'alter table KO_UNIT add "parent_calc_number" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'use_as_prototype')) then
        execute 'alter table KO_UNIT add "use_as_prototype" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT', 'response_document_id')) then
        execute 'alter table KO_UNIT add "response_document_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_201_q_pk')) then
    execute 'alter table KO_UNIT add constraint reg_201_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"CadastralNumberIndex"')) then
	execute 'CREATE  INDEX "CadastralNumberIndex" on KO_UNIT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('"GroupIdIndex"')) then
	execute 'CREATE  INDEX "GroupIdIndex" on KO_UNIT (group_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_UNIT_PARAMS_OKS_2016')) then
		execute 'create table KO_UNIT_PARAMS_OKS_2016 ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'id')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_98')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_98" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_104')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_104" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_130')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_130" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_140')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_140" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_148')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_148" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_152')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_152" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_154')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_154" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_155')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_155" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_157')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_157" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_160')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_160" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_173')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_173" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_179')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_179" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_99')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_99" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_100')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_100" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_101')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_101" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_102')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_102" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_103')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_103" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_105')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_105" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_106')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_106" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_107')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_107" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_108')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_108" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_109')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_109" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_110')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_110" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_111')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_111" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_112')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_112" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_113')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_113" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_114')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_114" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_115')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_115" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_116')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_116" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_117')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_117" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_118')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_118" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_119')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_119" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_120')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_120" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_121')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_121" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_122')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_122" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_123')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_123" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_124')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_124" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_125')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_125" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_126')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_126" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_127')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_127" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_128')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_128" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_129')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_129" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_131')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_131" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_132')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_132" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_133')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_133" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_134')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_134" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_135')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_135" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_136')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_136" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_137')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_137" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_138')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_138" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_139')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_139" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_141')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_141" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_142')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_142" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_143')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_143" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_144')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_144" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_145')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_145" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_146')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_146" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_147')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_147" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_149')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_149" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_150')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_150" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_151')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_151" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_153')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_153" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_156')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_156" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_158')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_158" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_159')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_159" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_161')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_161" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_162')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_162" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_163')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_163" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_164')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_164" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_165')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_165" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_166')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_166" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_167')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_167" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_169')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_169" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_170')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_170" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_171')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_171" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_172')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_172" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_174')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_174" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_175')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_175" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_176')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_176" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_177')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_177" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_178')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_178" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_180')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_180" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_181')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_181" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2016', 'field_182')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2016 add "field_182" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_252_q_pk')) then
    execute 'alter table KO_UNIT_PARAMS_OKS_2016 add constraint reg_252_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_UNIT_PARAMS_OKS_2018')) then
		execute 'create table KO_UNIT_PARAMS_OKS_2018 ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'id')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_165')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_165" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_183')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_183" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_184')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_184" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_185')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_185" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_186')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_186" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_187')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_187" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_188')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_188" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_189')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_189" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_190')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_190" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_191')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_191" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_192')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_192" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_193')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_193" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_194')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_194" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_195')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_195" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_196')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_196" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_197')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_197" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_198')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_198" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_199')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_199" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_200')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_200" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_201')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_201" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_202')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_202" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_203')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_203" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_204')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_204" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_205')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_205" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_206')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_206" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_207')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_207" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_208')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_208" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_209')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_209" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_210')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_210" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_211')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_211" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_212')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_212" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_213')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_213" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_214')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_214" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_215')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_215" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_216')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_216" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_217')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_217" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_218')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_218" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_219')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_219" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_220')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_220" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_221')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_221" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_222')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_222" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_223')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_223" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_224')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_224" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_225')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_225" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_226')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_226" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_227')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_227" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_228')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_228" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_229')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_229" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_230')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_230" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_231')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_231" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_232')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_232" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_233')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_233" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_234')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_234" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_235')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_235" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_236')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_236" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_237')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_237" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_238')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_238" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_239')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_239" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_241')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_241" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_OKS_2018', 'field_244')) then
        execute 'alter table KO_UNIT_PARAMS_OKS_2018 add "field_244" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_250_q_pk')) then
    execute 'alter table KO_UNIT_PARAMS_OKS_2018 add constraint reg_250_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_UNIT_PARAMS_ZU_2016')) then
		execute 'create table KO_UNIT_PARAMS_ZU_2016 ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'id')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_176')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_176" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_177')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_177" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_179')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_179" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_180')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_180" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_181')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_181" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_182')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_182" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_183')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_183" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_184')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_184" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_185')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_185" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_186')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_186" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_187')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_187" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_188')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_188" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_190')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_190" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_191')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_191" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_192')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_192" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_193')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_193" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_194')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_194" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_195')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_195" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_196')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_196" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_197')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_197" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_198')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_198" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_199')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_199" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_200')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_200" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_201')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_201" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_178')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_178" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_189')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_189" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_202')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_202" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_203')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_203" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_204')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_204" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_205')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_205" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_206')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_206" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_207')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_207" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_208')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_208" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_209')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_209" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_210')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_210" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_211')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_211" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_212')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_212" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_213')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_213" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_214')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_214" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2016', 'field_215')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2016 add "field_215" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_253_q_pk')) then
    execute 'alter table KO_UNIT_PARAMS_ZU_2016 add constraint reg_253_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('KO_UNIT_PARAMS_ZU_2018')) then
		execute 'create table KO_UNIT_PARAMS_ZU_2018 ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'id')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_183')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_183" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_184')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_184" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_185')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_185" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_186')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_186" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_187')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_187" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_188')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_188" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_189')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_189" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_190')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_190" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_191')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_191" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_192')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_192" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_193')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_193" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_194')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_194" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_195')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_195" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_196')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_196" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_197')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_197" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_198')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_198" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_199')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_199" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_200')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_200" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_201')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_201" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_203')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_203" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_204')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_204" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_205')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_205" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_206')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_206" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_207')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_207" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_208')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_208" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_209')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_209" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_210')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_210" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_211')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_211" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_212')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_212" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_213')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_213" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_214')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_214" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_215')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_215" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_216')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_216" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_217')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_217" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_218')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_218" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_219')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_219" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_220')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_220" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_221')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_221" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_222')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_222" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_223')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_223" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_224')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_224" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_225')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_225" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_226')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_226" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_227')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_227" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_228')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_228" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_229')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_229" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_230')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_230" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_231')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_231" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_232')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_232" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_233')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_233" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_234')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_234" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_235')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_235" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_236')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_236" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_237')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_237" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_238')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_238" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_239')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_239" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_240')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_240" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_241')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_241" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_242')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_242" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_243')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_243" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_244')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_244" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_245')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_245" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_246')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_246" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_247')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_247" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_248')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_248" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_249')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_249" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_250')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_250" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_251')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_251" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('KO_UNIT_PARAMS_ZU_2018', 'field_252')) then
        execute 'alter table KO_UNIT_PARAMS_ZU_2018 add "field_252" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_251_q_pk')) then
    execute 'alter table KO_UNIT_PARAMS_ZU_2018 add constraint reg_251_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('MARKET_ADDRESS_YANDEX')) then
		execute 'create table MARKET_ADDRESS_YANDEX ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'id')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'formalized_address')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "formalized_address" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'cadastral_number')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "cadastral_number" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'lat')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "lat" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'lng')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "lng" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'country')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "country" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'province')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "province" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'province_2')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "province_2" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'area')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "area" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'locality')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "locality" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'area_2')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "area_2" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'locality_2')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "locality_2" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'district')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "district" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'district_3')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "district_3" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'district_2')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "district_2" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'airport')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "airport" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'vegetation')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "vegetation" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'route')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "route" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'railway_station')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "railway_station" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'street')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "street" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'house')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "house" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'other')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "other" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_ADDRESS_YANDEX', 'initial_id')) then
        execute 'alter table MARKET_ADDRESS_YANDEX add "initial_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_101_q_pk')) then
    execute 'alter table MARKET_ADDRESS_YANDEX add constraint reg_101_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('formalized_address_index')) then
	execute 'CREATE  INDEX formalized_address_index on MARKET_ADDRESS_YANDEX (formalized_address)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('MARKET_CORE_OBJECT')) then
		execute 'create table MARKET_CORE_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'id')) then
        execute 'alter table MARKET_CORE_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'url')) then
        execute 'alter table MARKET_CORE_OBJECT add "url" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'market_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "market_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'market')) then
        execute 'alter table MARKET_CORE_OBJECT add "market" VARCHAR(50) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'property_type_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "property_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'property_type')) then
        execute 'alter table MARKET_CORE_OBJECT add "property_type" VARCHAR(50) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'market_id')) then
        execute 'alter table MARKET_CORE_OBJECT add "market_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'price')) then
        execute 'alter table MARKET_CORE_OBJECT add "price" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'parser_time')) then
        execute 'alter table MARKET_CORE_OBJECT add "parser_time" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'region')) then
        execute 'alter table MARKET_CORE_OBJECT add "region" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'city')) then
        execute 'alter table MARKET_CORE_OBJECT add "city" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'address')) then
        execute 'alter table MARKET_CORE_OBJECT add "address" VARCHAR(2000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'metro')) then
        execute 'alter table MARKET_CORE_OBJECT add "metro" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'images')) then
        execute 'alter table MARKET_CORE_OBJECT add "images" VARCHAR(10000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'description')) then
        execute 'alter table MARKET_CORE_OBJECT add "description" VARCHAR(10000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'lat')) then
        execute 'alter table MARKET_CORE_OBJECT add "lat" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'lng')) then
        execute 'alter table MARKET_CORE_OBJECT add "lng" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'deal_type')) then
        execute 'alter table MARKET_CORE_OBJECT add "deal_type" VARCHAR(50) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'deal_type_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "deal_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'rooms_count')) then
        execute 'alter table MARKET_CORE_OBJECT add "rooms_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'floor_number')) then
        execute 'alter table MARKET_CORE_OBJECT add "floor_number" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'floors_count')) then
        execute 'alter table MARKET_CORE_OBJECT add "floors_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'area')) then
        execute 'alter table MARKET_CORE_OBJECT add "area" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'area_kitchen')) then
        execute 'alter table MARKET_CORE_OBJECT add "area_kitchen" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'area_living')) then
        execute 'alter table MARKET_CORE_OBJECT add "area_living" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'area_land')) then
        execute 'alter table MARKET_CORE_OBJECT add "area_land" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'building_year')) then
        execute 'alter table MARKET_CORE_OBJECT add "building_year" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'category')) then
        execute 'alter table MARKET_CORE_OBJECT add "category" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'subcategory')) then
        execute 'alter table MARKET_CORE_OBJECT add "subcategory" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'category_id')) then
        execute 'alter table MARKET_CORE_OBJECT add "category_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'region_id')) then
        execute 'alter table MARKET_CORE_OBJECT add "region_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'city_id')) then
        execute 'alter table MARKET_CORE_OBJECT add "city_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'cadastral_number')) then
        execute 'alter table MARKET_CORE_OBJECT add "cadastral_number" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'building_cadastral_number')) then
        execute 'alter table MARKET_CORE_OBJECT add "building_cadastral_number" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'cadastral_quartal')) then
        execute 'alter table MARKET_CORE_OBJECT add "cadastral_quartal" VARCHAR(50)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'ko_group')) then
        execute 'alter table MARKET_CORE_OBJECT add "ko_group" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'ko_group_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "ko_group_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'zone')) then
        execute 'alter table MARKET_CORE_OBJECT add "zone" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'district')) then
        execute 'alter table MARKET_CORE_OBJECT add "district" VARCHAR(256)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'district_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "district_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'ko_subgroup')) then
        execute 'alter table MARKET_CORE_OBJECT add "ko_subgroup" VARCHAR(256)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'ko_subgroup_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "ko_subgroup_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'price_per_meter')) then
        execute 'alter table MARKET_CORE_OBJECT add "price_per_meter" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'process_type')) then
        execute 'alter table MARKET_CORE_OBJECT add "process_type" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'process_type_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "process_type_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'property_market_segment')) then
        execute 'alter table MARKET_CORE_OBJECT add "property_market_segment" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'property_market_segment_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "property_market_segment_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'wall_material')) then
        execute 'alter table MARKET_CORE_OBJECT add "wall_material" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'wall_material_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "wall_material_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'quality_class')) then
        execute 'alter table MARKET_CORE_OBJECT add "quality_class" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'quality_class_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "quality_class_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'subway_space')) then
        execute 'alter table MARKET_CORE_OBJECT add "subway_space" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'exclusion_status')) then
        execute 'alter table MARKET_CORE_OBJECT add "exclusion_status" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'exclusion_status_code')) then
        execute 'alter table MARKET_CORE_OBJECT add "exclusion_status_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'formalized_address_id')) then
        execute 'alter table MARKET_CORE_OBJECT add "formalized_address_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT', 'last_date_update')) then
        execute 'alter table MARKET_CORE_OBJECT add "last_date_update" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('market_core_object_pkey')) then
    execute 'alter table MARKET_CORE_OBJECT add constraint market_core_object_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('cadastral_number_index')) then
	execute 'CREATE  INDEX cadastral_number_index on MARKET_CORE_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('deal_type_code_index')) then
	execute 'CREATE  INDEX deal_type_code_index on MARKET_CORE_OBJECT (deal_type_code)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('url_index')) then
	execute 'CREATE  INDEX url_index on MARKET_CORE_OBJECT (url)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('MARKET_CORE_OBJECT_TEST')) then
		execute 'create table MARKET_CORE_OBJECT_TEST ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'id')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'cadastral_number')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "cadastral_number" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'address')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "address" VARCHAR(20000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'parser_time')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "parser_time" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'description')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "description" VARCHAR(10000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'area')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "area" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'price')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "price" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'price_per_meter')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "price_per_meter" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'process_type')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "process_type" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'process_type_code')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "process_type_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'exclusion_status')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "exclusion_status" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'exclusion_status_code')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "exclusion_status_code" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'deal_type')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "deal_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'deal_type_code')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "deal_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'property_type')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "property_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'property_type_code')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "property_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'subcategory')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "subcategory" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_CORE_OBJECT_TEST', 'result_from_source_file')) then
        execute 'alter table MARKET_CORE_OBJECT_TEST add "result_from_source_file" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_110_q_pk')) then
    execute 'alter table MARKET_CORE_OBJECT_TEST add constraint reg_110_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('MARKET_DUPLICATES_HISTORY')) then
		execute 'create table MARKET_DUPLICATES_HISTORY ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_DUPLICATES_HISTORY', 'id')) then
        execute 'alter table MARKET_DUPLICATES_HISTORY add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_DUPLICATES_HISTORY', 'check_date')) then
        execute 'alter table MARKET_DUPLICATES_HISTORY add "check_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_DUPLICATES_HISTORY', 'market_segment')) then
        execute 'alter table MARKET_DUPLICATES_HISTORY add "market_segment" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_DUPLICATES_HISTORY', 'area_delta')) then
        execute 'alter table MARKET_DUPLICATES_HISTORY add "area_delta" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_DUPLICATES_HISTORY', 'price_delta')) then
        execute 'alter table MARKET_DUPLICATES_HISTORY add "price_delta" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_DUPLICATES_HISTORY', 'common_count')) then
        execute 'alter table MARKET_DUPLICATES_HISTORY add "common_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_DUPLICATES_HISTORY', 'in_progress_count')) then
        execute 'alter table MARKET_DUPLICATES_HISTORY add "in_progress_count" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_DUPLICATES_HISTORY', 'duplicate_objects')) then
        execute 'alter table MARKET_DUPLICATES_HISTORY add "duplicate_objects" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_106_q_pk')) then
    execute 'alter table MARKET_DUPLICATES_HISTORY add constraint reg_106_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('MARKET_PRICE_HISTORY')) then
		execute 'create table MARKET_PRICE_HISTORY ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_PRICE_HISTORY', 'id')) then
        execute 'alter table MARKET_PRICE_HISTORY add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_PRICE_HISTORY', 'initial_id')) then
        execute 'alter table MARKET_PRICE_HISTORY add "initial_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_PRICE_HISTORY', 'changing_date')) then
        execute 'alter table MARKET_PRICE_HISTORY add "changing_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_PRICE_HISTORY', 'price_value_to')) then
        execute 'alter table MARKET_PRICE_HISTORY add "price_value_to" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_PRICE_HISTORY', 'price_value_from')) then
        execute 'alter table MARKET_PRICE_HISTORY add "price_value_from" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_105_q_pk')) then
    execute 'alter table MARKET_PRICE_HISTORY add constraint reg_105_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('MARKET_SCREENSHOTS')) then
		execute 'create table MARKET_SCREENSHOTS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_SCREENSHOTS', 'id')) then
        execute 'alter table MARKET_SCREENSHOTS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_SCREENSHOTS', 'initial_id')) then
        execute 'alter table MARKET_SCREENSHOTS add "initial_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_SCREENSHOTS', 'creation_date')) then
        execute 'alter table MARKET_SCREENSHOTS add "creation_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_SCREENSHOTS', 'type')) then
        execute 'alter table MARKET_SCREENSHOTS add "type" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_104_q_pk')) then
    execute 'alter table MARKET_SCREENSHOTS add constraint reg_104_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('market_screenshots_initial_id_idx')) then
	execute 'CREATE  INDEX market_screenshots_initial_id_idx on MARKET_SCREENSHOTS (initial_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('MARKET_SETTINGS')) then
		execute 'create table MARKET_SETTINGS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_SETTINGS', 'id')) then
        execute 'alter table MARKET_SETTINGS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('MARKET_SETTINGS', 'setting_value')) then
        execute 'alter table MARKET_SETTINGS add "setting_value" VARCHAR(1000) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('pk_market_settings_id')) then
    execute 'alter table MARKET_SETTINGS add constraint pk_market_settings_id primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_DICT')) then
		execute 'create table SUD_DICT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DICT', 'id')) then
        execute 'alter table SUD_DICT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DICT', 'type')) then
        execute 'alter table SUD_DICT add "type" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DICT', 'name')) then
        execute 'alter table SUD_DICT add "name" VARCHAR(4000) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DICT', 'id_parent')) then
        execute 'alter table SUD_DICT add "id_parent" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_313_q_pk')) then
    execute 'alter table SUD_DICT add constraint reg_313_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_DRS')) then
		execute 'create table SUD_DRS ("id_object" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'id_object')) then
        execute 'alter table SUD_DRS add "id_object" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq7')) then
        execute 'alter table SUD_DRS add "drs_sq7" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_group')) then
        execute 'alter table SUD_DRS add "drs_group" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq1')) then
        execute 'alter table SUD_DRS add "drs_sq1" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq2')) then
        execute 'alter table SUD_DRS add "drs_sq2" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq3')) then
        execute 'alter table SUD_DRS add "drs_sq3" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq4')) then
        execute 'alter table SUD_DRS add "drs_sq4" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_drs')) then
        execute 'alter table SUD_DRS add "drs_drs" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq5')) then
        execute 'alter table SUD_DRS add "drs_sq5" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq6')) then
        execute 'alter table SUD_DRS add "drs_sq6" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq8')) then
        execute 'alter table SUD_DRS add "drs_sq8" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sq9')) then
        execute 'alter table SUD_DRS add "drs_sq9" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_sost')) then
        execute 'alter table SUD_DRS add "drs_sost" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_prichin')) then
        execute 'alter table SUD_DRS add "drs_prichin" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_updrs')) then
        execute 'alter table SUD_DRS add "drs_updrs" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_DRS', 'drs_owner')) then
        execute 'alter table SUD_DRS add "drs_owner" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_303_q_pk')) then
    execute 'alter table SUD_DRS add constraint reg_303_q_pk primary key (id_object)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_LOG')) then
		execute 'create table SUD_LOG ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_LOG', 'id')) then
        execute 'alter table SUD_LOG add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_LOG', 'id_user')) then
        execute 'alter table SUD_LOG add "id_user" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_LOG', 'id_table')) then
        execute 'alter table SUD_LOG add "id_table" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_LOG', 'type_oper')) then
        execute 'alter table SUD_LOG add "type_oper" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_LOG', 'name_table')) then
        execute 'alter table SUD_LOG add "name_table" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_LOG', 'id_record')) then
        execute 'alter table SUD_LOG add "id_record" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_LOG', 'xml_data')) then
        execute 'alter table SUD_LOG add "xml_data" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_LOG', 'date_oper')) then
        execute 'alter table SUD_LOG add "date_oper" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_301_q_pk')) then
    execute 'alter table SUD_LOG add constraint reg_301_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_OBJECT')) then
		execute 'create table SUD_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'id')) then
        execute 'alter table SUD_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'kn')) then
        execute 'alter table SUD_OBJECT add "kn" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'date')) then
        execute 'alter table SUD_OBJECT add "date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'square')) then
        execute 'alter table SUD_OBJECT add "square" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'kc')) then
        execute 'alter table SUD_OBJECT add "kc" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'stat_dgi')) then
        execute 'alter table SUD_OBJECT add "stat_dgi" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'owner')) then
        execute 'alter table SUD_OBJECT add "owner" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'adres')) then
        execute 'alter table SUD_OBJECT add "adres" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'typeobj')) then
        execute 'alter table SUD_OBJECT add "typeobj" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'workstat')) then
        execute 'alter table SUD_OBJECT add "workstat" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'name_center')) then
        execute 'alter table SUD_OBJECT add "name_center" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'change_user_id')) then
        execute 'alter table SUD_OBJECT add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECT', 'change_date')) then
        execute 'alter table SUD_OBJECT add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_315_q_pk')) then
    execute 'alter table SUD_OBJECT add constraint reg_315_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_OBJECTSTATUS')) then
		execute 'create table SUD_OBJECTSTATUS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'id')) then
        execute 'alter table SUD_OBJECTSTATUS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'kn')) then
        execute 'alter table SUD_OBJECTSTATUS add "kn" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'date')) then
        execute 'alter table SUD_OBJECTSTATUS add "date" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'square')) then
        execute 'alter table SUD_OBJECTSTATUS add "square" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'kc')) then
        execute 'alter table SUD_OBJECTSTATUS add "kc" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'name_center')) then
        execute 'alter table SUD_OBJECTSTATUS add "name_center" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'stat_dgi')) then
        execute 'alter table SUD_OBJECTSTATUS add "stat_dgi" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'owner')) then
        execute 'alter table SUD_OBJECTSTATUS add "owner" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'adres')) then
        execute 'alter table SUD_OBJECTSTATUS add "adres" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'typeobj')) then
        execute 'alter table SUD_OBJECTSTATUS add "typeobj" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OBJECTSTATUS', 'status')) then
        execute 'alter table SUD_OBJECTSTATUS add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_305_q_pk')) then
    execute 'alter table SUD_OBJECTSTATUS add constraint reg_305_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_OTCHET')) then
		execute 'create table SUD_OTCHET ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'id')) then
        execute 'alter table SUD_OTCHET add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'number')) then
        execute 'alter table SUD_OTCHET add "number" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'date')) then
        execute 'alter table SUD_OTCHET add "date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'org')) then
        execute 'alter table SUD_OTCHET add "org" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'fio')) then
        execute 'alter table SUD_OTCHET add "fio" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'sro')) then
        execute 'alter table SUD_OTCHET add "sro" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'date_in')) then
        execute 'alter table SUD_OTCHET add "date_in" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'jalob')) then
        execute 'alter table SUD_OTCHET add "jalob" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'id_org')) then
        execute 'alter table SUD_OTCHET add "id_org" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'id_fio')) then
        execute 'alter table SUD_OTCHET add "id_fio" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHET', 'id_sro')) then
        execute 'alter table SUD_OTCHET add "id_sro" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_308_q_pk')) then
    execute 'alter table SUD_OTCHET add constraint reg_308_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_OTCHETLINK')) then
		execute 'create table SUD_OTCHETLINK ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINK', 'id')) then
        execute 'alter table SUD_OTCHETLINK add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINK', 'id_otchet')) then
        execute 'alter table SUD_OTCHETLINK add "id_otchet" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINK', 'use')) then
        execute 'alter table SUD_OTCHETLINK add "use" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINK', 'rs')) then
        execute 'alter table SUD_OTCHETLINK add "rs" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINK', 'uprs')) then
        execute 'alter table SUD_OTCHETLINK add "uprs" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINK', 'descr')) then
        execute 'alter table SUD_OTCHETLINK add "descr" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINK', 'id_object')) then
        execute 'alter table SUD_OTCHETLINK add "id_object" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_304_q_pk')) then
    execute 'alter table SUD_OTCHETLINK add constraint reg_304_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_OTCHETLINKSTATUS')) then
		execute 'create table SUD_OTCHETLINKSTATUS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINKSTATUS', 'id')) then
        execute 'alter table SUD_OTCHETLINKSTATUS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINKSTATUS', 'id_object')) then
        execute 'alter table SUD_OTCHETLINKSTATUS add "id_object" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINKSTATUS', 'id_otchet')) then
        execute 'alter table SUD_OTCHETLINKSTATUS add "id_otchet" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINKSTATUS', 'use')) then
        execute 'alter table SUD_OTCHETLINKSTATUS add "use" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINKSTATUS', 'rs')) then
        execute 'alter table SUD_OTCHETLINKSTATUS add "rs" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINKSTATUS', 'uprs')) then
        execute 'alter table SUD_OTCHETLINKSTATUS add "uprs" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINKSTATUS', 'descr')) then
        execute 'alter table SUD_OTCHETLINKSTATUS add "descr" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETLINKSTATUS', 'status')) then
        execute 'alter table SUD_OTCHETLINKSTATUS add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_306_q_pk')) then
    execute 'alter table SUD_OTCHETLINKSTATUS add constraint reg_306_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('sud_otchetlinkstatus_obj_idx')) then
	execute 'CREATE  INDEX sud_otchetlinkstatus_obj_idx on SUD_OTCHETLINKSTATUS (id_object)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_OTCHETSTATUS')) then
		execute 'create table SUD_OTCHETSTATUS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'id')) then
        execute 'alter table SUD_OTCHETSTATUS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'number')) then
        execute 'alter table SUD_OTCHETSTATUS add "number" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'date')) then
        execute 'alter table SUD_OTCHETSTATUS add "date" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'date_in')) then
        execute 'alter table SUD_OTCHETSTATUS add "date_in" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'jalob')) then
        execute 'alter table SUD_OTCHETSTATUS add "jalob" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'id_org')) then
        execute 'alter table SUD_OTCHETSTATUS add "id_org" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'id_fio')) then
        execute 'alter table SUD_OTCHETSTATUS add "id_fio" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'id_sro')) then
        execute 'alter table SUD_OTCHETSTATUS add "id_sro" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_OTCHETSTATUS', 'status')) then
        execute 'alter table SUD_OTCHETSTATUS add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_307_q_pk')) then
    execute 'alter table SUD_OTCHETSTATUS add constraint reg_307_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_PARAM')) then
		execute 'create table SUD_PARAM ("pid" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'pid')) then
        execute 'alter table SUD_PARAM add "pid" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'id_table')) then
        execute 'alter table SUD_PARAM add "id_table" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'id')) then
        execute 'alter table SUD_PARAM add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'param_name')) then
        execute 'alter table SUD_PARAM add "param_name" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'param_double')) then
        execute 'alter table SUD_PARAM add "param_double" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'param_char')) then
        execute 'alter table SUD_PARAM add "param_char" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'param_int')) then
        execute 'alter table SUD_PARAM add "param_int" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'id_user')) then
        execute 'alter table SUD_PARAM add "id_user" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'date_user')) then
        execute 'alter table SUD_PARAM add "date_user" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'param_status')) then
        execute 'alter table SUD_PARAM add "param_status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_PARAM', 'param_date')) then
        execute 'alter table SUD_PARAM add "param_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_317_q_pk')) then
    execute 'alter table SUD_PARAM add constraint reg_317_q_pk primary key (pid)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg317_q_id_idx')) then
	execute 'CREATE  INDEX reg317_q_id_idx on SUD_PARAM (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_SUD')) then
		execute 'create table SUD_SUD ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUD', 'id')) then
        execute 'alter table SUD_SUD add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUD', 'name')) then
        execute 'alter table SUD_SUD add "name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUD', 'number')) then
        execute 'alter table SUD_SUD add "number" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUD', 'date')) then
        execute 'alter table SUD_SUD add "date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUD', 'sud_date')) then
        execute 'alter table SUD_SUD add "sud_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUD', 'status')) then
        execute 'alter table SUD_SUD add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_316_q_pk')) then
    execute 'alter table SUD_SUD add constraint reg_316_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_SUDLINK')) then
		execute 'create table SUD_SUDLINK ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINK', 'id')) then
        execute 'alter table SUD_SUDLINK add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINK', 'id_object')) then
        execute 'alter table SUD_SUDLINK add "id_object" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINK', 'id_sud')) then
        execute 'alter table SUD_SUDLINK add "id_sud" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINK', 'use')) then
        execute 'alter table SUD_SUDLINK add "use" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINK', 'rs')) then
        execute 'alter table SUD_SUDLINK add "rs" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINK', 'uprs')) then
        execute 'alter table SUD_SUDLINK add "uprs" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINK', 'descr')) then
        execute 'alter table SUD_SUDLINK add "descr" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_314_q_pk')) then
    execute 'alter table SUD_SUDLINK add constraint reg_314_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('sud_sudlink_obj_sud_idx')) then
	execute 'CREATE  INDEX sud_sudlink_obj_sud_idx on SUD_SUDLINK (id_object, id_sud)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_SUDLINKSTATUS')) then
		execute 'create table SUD_SUDLINKSTATUS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINKSTATUS', 'id')) then
        execute 'alter table SUD_SUDLINKSTATUS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINKSTATUS', 'id_object')) then
        execute 'alter table SUD_SUDLINKSTATUS add "id_object" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINKSTATUS', 'id_sud')) then
        execute 'alter table SUD_SUDLINKSTATUS add "id_sud" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINKSTATUS', 'use')) then
        execute 'alter table SUD_SUDLINKSTATUS add "use" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINKSTATUS', 'rs')) then
        execute 'alter table SUD_SUDLINKSTATUS add "rs" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINKSTATUS', 'uprs')) then
        execute 'alter table SUD_SUDLINKSTATUS add "uprs" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINKSTATUS', 'descr')) then
        execute 'alter table SUD_SUDLINKSTATUS add "descr" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDLINKSTATUS', 'status')) then
        execute 'alter table SUD_SUDLINKSTATUS add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_309_q_pk')) then
    execute 'alter table SUD_SUDLINKSTATUS add constraint reg_309_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_SUDSTATUS')) then
		execute 'create table SUD_SUDSTATUS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDSTATUS', 'id')) then
        execute 'alter table SUD_SUDSTATUS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDSTATUS', 'name')) then
        execute 'alter table SUD_SUDSTATUS add "name" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDSTATUS', 'number')) then
        execute 'alter table SUD_SUDSTATUS add "number" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDSTATUS', 'date')) then
        execute 'alter table SUD_SUDSTATUS add "date" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDSTATUS', 'sud_date')) then
        execute 'alter table SUD_SUDSTATUS add "sud_date" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDSTATUS', 'status')) then
        execute 'alter table SUD_SUDSTATUS add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_SUDSTATUS', 'astatus')) then
        execute 'alter table SUD_SUDSTATUS add "astatus" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_310_q_pk')) then
    execute 'alter table SUD_SUDSTATUS add constraint reg_310_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_ZAK')) then
		execute 'create table SUD_ZAK ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'id')) then
        execute 'alter table SUD_ZAK add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'number')) then
        execute 'alter table SUD_ZAK add "number" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'date')) then
        execute 'alter table SUD_ZAK add "date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'org')) then
        execute 'alter table SUD_ZAK add "org" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'fio')) then
        execute 'alter table SUD_ZAK add "fio" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'sro')) then
        execute 'alter table SUD_ZAK add "sro" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'rec_date')) then
        execute 'alter table SUD_ZAK add "rec_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'rec_user')) then
        execute 'alter table SUD_ZAK add "rec_user" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'rec_letter')) then
        execute 'alter table SUD_ZAK add "rec_letter" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'rec_before')) then
        execute 'alter table SUD_ZAK add "rec_before" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'rec_after')) then
        execute 'alter table SUD_ZAK add "rec_after" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'rec_soglas')) then
        execute 'alter table SUD_ZAK add "rec_soglas" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'id_org')) then
        execute 'alter table SUD_ZAK add "id_org" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'id_fio')) then
        execute 'alter table SUD_ZAK add "id_fio" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAK', 'id_sro')) then
        execute 'alter table SUD_ZAK add "id_sro" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_300_q_pk')) then
    execute 'alter table SUD_ZAK add constraint reg_300_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_ZAKLINK')) then
		execute 'create table SUD_ZAKLINK ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINK', 'id')) then
        execute 'alter table SUD_ZAKLINK add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINK', 'id_object')) then
        execute 'alter table SUD_ZAKLINK add "id_object" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINK', 'id_zak')) then
        execute 'alter table SUD_ZAKLINK add "id_zak" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINK', 'use')) then
        execute 'alter table SUD_ZAKLINK add "use" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINK', 'rs')) then
        execute 'alter table SUD_ZAKLINK add "rs" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINK', 'uprs')) then
        execute 'alter table SUD_ZAKLINK add "uprs" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINK', 'descr')) then
        execute 'alter table SUD_ZAKLINK add "descr" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_302_q_pk')) then
    execute 'alter table SUD_ZAKLINK add constraint reg_302_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('sud_zaklink_obj_zak_idx')) then
	execute 'CREATE  INDEX sud_zaklink_obj_zak_idx on SUD_ZAKLINK (id_object, id_zak)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_ZAKLINKSTATUS')) then
		execute 'create table SUD_ZAKLINKSTATUS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINKSTATUS', 'id')) then
        execute 'alter table SUD_ZAKLINKSTATUS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINKSTATUS', 'id_object')) then
        execute 'alter table SUD_ZAKLINKSTATUS add "id_object" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINKSTATUS', 'id_zak')) then
        execute 'alter table SUD_ZAKLINKSTATUS add "id_zak" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINKSTATUS', 'use')) then
        execute 'alter table SUD_ZAKLINKSTATUS add "use" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINKSTATUS', 'rs')) then
        execute 'alter table SUD_ZAKLINKSTATUS add "rs" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINKSTATUS', 'uprs')) then
        execute 'alter table SUD_ZAKLINKSTATUS add "uprs" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINKSTATUS', 'descr')) then
        execute 'alter table SUD_ZAKLINKSTATUS add "descr" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKLINKSTATUS', 'status')) then
        execute 'alter table SUD_ZAKLINKSTATUS add "status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_311_q_pk')) then
    execute 'alter table SUD_ZAKLINKSTATUS add constraint reg_311_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('SUD_ZAKSTATUS')) then
		execute 'create table SUD_ZAKSTATUS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'id')) then
        execute 'alter table SUD_ZAKSTATUS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'number')) then
        execute 'alter table SUD_ZAKSTATUS add "number" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'date')) then
        execute 'alter table SUD_ZAKSTATUS add "date" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'rec_date')) then
        execute 'alter table SUD_ZAKSTATUS add "rec_date" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'rec_user')) then
        execute 'alter table SUD_ZAKSTATUS add "rec_user" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'rec_letter')) then
        execute 'alter table SUD_ZAKSTATUS add "rec_letter" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'rec_before')) then
        execute 'alter table SUD_ZAKSTATUS add "rec_before" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'rec_after')) then
        execute 'alter table SUD_ZAKSTATUS add "rec_after" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'rec_soglas')) then
        execute 'alter table SUD_ZAKSTATUS add "rec_soglas" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'id_org')) then
        execute 'alter table SUD_ZAKSTATUS add "id_org" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'id_fio')) then
        execute 'alter table SUD_ZAKSTATUS add "id_fio" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'id_sro')) then
        execute 'alter table SUD_ZAKSTATUS add "id_sro" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('SUD_ZAKSTATUS', 'status')) then
        execute 'alter table SUD_ZAKSTATUS add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_312_q_pk')) then
    execute 'alter table SUD_ZAKSTATUS add constraint reg_312_q_pk primary key (id)';
  end if;
end $$;
--<DO>--
