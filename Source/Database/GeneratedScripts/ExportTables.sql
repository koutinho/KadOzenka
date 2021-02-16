
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
	if (not CORE_UPDSTRU_CheckExistTable('COMISSION_COST_A')) then
		execute 'create table COMISSION_COST_A ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'id')) then
        execute 'alter table COMISSION_COST_A add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'object_id')) then
        execute 'alter table COMISSION_COST_A add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'attribute_id')) then
        execute 'alter table COMISSION_COST_A add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 's')) then
        execute 'alter table COMISSION_COST_A add "s" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'po')) then
        execute 'alter table COMISSION_COST_A add "po" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'ref_item_id')) then
        execute 'alter table COMISSION_COST_A add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'text_value')) then
        execute 'alter table COMISSION_COST_A add "text_value" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'number_value')) then
        execute 'alter table COMISSION_COST_A add "number_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'date_value')) then
        execute 'alter table COMISSION_COST_A add "date_value" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMISSION_COST_A', 'change_user_id')) then
        execute 'alter table COMISSION_COST_A add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg400_a_pkey')) then
    execute 'alter table COMISSION_COST_A add constraint reg400_a_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('COMMON_DATA_FORM_STORAGE')) then
		execute 'create table COMMON_DATA_FORM_STORAGE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_DATA_FORM_STORAGE', 'id')) then
        execute 'alter table COMMON_DATA_FORM_STORAGE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_DATA_FORM_STORAGE', 'id_user')) then
        execute 'alter table COMMON_DATA_FORM_STORAGE add "id_user" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_DATA_FORM_STORAGE', 'formtype')) then
        execute 'alter table COMMON_DATA_FORM_STORAGE add "formtype" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_DATA_FORM_STORAGE', 'data_form')) then
        execute 'alter table COMMON_DATA_FORM_STORAGE add "data_form" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_DATA_FORM_STORAGE', 'template_name')) then
        execute 'alter table COMMON_DATA_FORM_STORAGE add "template_name" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_DATA_FORM_STORAGE', 'is_common')) then
        execute 'alter table COMMON_DATA_FORM_STORAGE add "is_common" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('common_data_form_storage_pkey')) then
    execute 'alter table COMMON_DATA_FORM_STORAGE add constraint common_data_form_storage_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('common_data_form_storage_unique_indx')) then
	execute 'CREATE UNIQUE INDEX common_data_form_storage_unique_indx on COMMON_DATA_FORM_STORAGE (formtype)';
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
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "template_file_name" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'columns_mapping')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "columns_mapping" VARCHAR';
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
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'result_file_name')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "result_file_name" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'file_extension')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "file_extension" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'file_template_title')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "file_template_title" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_EXPORT_BY_TEMPLATES', 'file_result_title')) then
        execute 'alter table COMMON_EXPORT_BY_TEMPLATES add "file_result_title" VARCHAR(512)';
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
	if (not CORE_UPDSTRU_CheckExistTable('COMMON_GBU_OPERATIONS_REPORTS')) then
		execute 'create table COMMON_GBU_OPERATIONS_REPORTS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_GBU_OPERATIONS_REPORTS', 'id')) then
        execute 'alter table COMMON_GBU_OPERATIONS_REPORTS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_GBU_OPERATIONS_REPORTS', 'user_id')) then
        execute 'alter table COMMON_GBU_OPERATIONS_REPORTS add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_GBU_OPERATIONS_REPORTS', 'creation_date')) then
        execute 'alter table COMMON_GBU_OPERATIONS_REPORTS add "creation_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_GBU_OPERATIONS_REPORTS', 'finish_date')) then
        execute 'alter table COMMON_GBU_OPERATIONS_REPORTS add "finish_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_GBU_OPERATIONS_REPORTS', 'status')) then
        execute 'alter table COMMON_GBU_OPERATIONS_REPORTS add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_GBU_OPERATIONS_REPORTS', 'file_name')) then
        execute 'alter table COMMON_GBU_OPERATIONS_REPORTS add "file_name" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_814_q_pk')) then
    execute 'alter table COMMON_GBU_OPERATIONS_REPORTS add constraint reg_814_q_pk primary key (id)';
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
        execute 'alter table COMMON_IMPORT_DATA_LOG add "data_file_name" VARCHAR(512)';
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
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'register_id')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "register_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'object_id')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'number_of_imported_objects')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "number_of_imported_objects" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'total_number_of_objects')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "total_number_of_objects" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'document_id')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "document_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'result_file_name')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "result_file_name" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'file_extension')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "file_extension" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'data_file_title')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "data_file_title" VARCHAR(512)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_IMPORT_DATA_LOG', 'result_file_title')) then
        execute 'alter table COMMON_IMPORT_DATA_LOG add "result_file_title" VARCHAR(512)';
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
	if (not CORE_UPDSTRU_CheckExistTable('COMMON_RECYCLE_BIN')) then
		execute 'create table COMMON_RECYCLE_BIN ("event_id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_RECYCLE_BIN', 'event_id')) then
        execute 'alter table COMMON_RECYCLE_BIN add "event_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_RECYCLE_BIN', 'deleted_time')) then
        execute 'alter table COMMON_RECYCLE_BIN add "deleted_time" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_RECYCLE_BIN', 'user_id')) then
        execute 'alter table COMMON_RECYCLE_BIN add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_RECYCLE_BIN', 'object_register_id')) then
        execute 'alter table COMMON_RECYCLE_BIN add "object_register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_RECYCLE_BIN', 'description')) then
        execute 'alter table COMMON_RECYCLE_BIN add "description" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_812_q_pk')) then
    execute 'alter table COMMON_RECYCLE_BIN add constraint reg_812_q_pk primary key (event_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('COMMON_REGISTERS_WITH_SOFT_DELETION')) then
		execute 'create table COMMON_REGISTERS_WITH_SOFT_DELETION ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REGISTERS_WITH_SOFT_DELETION', 'id')) then
        execute 'alter table COMMON_REGISTERS_WITH_SOFT_DELETION add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REGISTERS_WITH_SOFT_DELETION', 'register_id')) then
        execute 'alter table COMMON_REGISTERS_WITH_SOFT_DELETION add "register_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REGISTERS_WITH_SOFT_DELETION', 'main_table_name')) then
        execute 'alter table COMMON_REGISTERS_WITH_SOFT_DELETION add "main_table_name" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_813_main_table_name_unique')) then
    execute 'alter table COMMON_REGISTERS_WITH_SOFT_DELETION add constraint reg_813_main_table_name_unique unique (main_table_name)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_813_q_pk')) then
    execute 'alter table COMMON_REGISTERS_WITH_SOFT_DELETION add constraint reg_813_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_813_main_table_name_unique')) then
	execute 'CREATE UNIQUE INDEX reg_813_main_table_name_unique on COMMON_REGISTERS_WITH_SOFT_DELETION (main_table_name)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('COMMON_REPORT_FILES')) then
		execute 'create table COMMON_REPORT_FILES ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REPORT_FILES', 'id')) then
        execute 'alter table COMMON_REPORT_FILES add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REPORT_FILES', 'user_id')) then
        execute 'alter table COMMON_REPORT_FILES add "user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REPORT_FILES', 'creation_date')) then
        execute 'alter table COMMON_REPORT_FILES add "creation_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REPORT_FILES', 'status')) then
        execute 'alter table COMMON_REPORT_FILES add "status" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REPORT_FILES', 'file_name')) then
        execute 'alter table COMMON_REPORT_FILES add "file_name" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REPORT_FILES', 'finish_date')) then
        execute 'alter table COMMON_REPORT_FILES add "finish_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('COMMON_REPORT_FILES', 'file_extension')) then
        execute 'alter table COMMON_REPORT_FILES add "file_extension" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_810_q_pk')) then
    execute 'alter table COMMON_REPORT_FILES add constraint reg_810_q_pk primary key (id)';
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
	if (not CORE_UPDSTRU_CheckExistTable('CORE_BACKGROUND_EXPORTS')) then
		execute 'create table CORE_BACKGROUND_EXPORTS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'id')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'name')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "name" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'scheduler_type')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "scheduler_type" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'next_run_date')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "next_run_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'input_parameters')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "input_parameters" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'is_for_report')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "is_for_report" SMALLINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'first_formation_date')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "first_formation_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'error_id')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "error_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_BACKGROUND_EXPORTS', 'last_notification_date')) then
        execute 'alter table CORE_BACKGROUND_EXPORTS add "last_notification_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_979_q_pk')) then
    execute 'alter table CORE_BACKGROUND_EXPORTS add constraint reg_979_q_pk primary key (id)';
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
  if (not CORE_UPDSTRU_CheckExistIndex('core_diagnostics_action_idx')) then
	execute 'CREATE  INDEX core_diagnostics_action_idx on CORE_DIAGNOSTICS (action_date)';
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
  if (not CORE_UPDSTRU_CheckExistIndex('core_error_log_composite_idx')) then
	execute 'CREATE  INDEX core_error_log_composite_idx on CORE_ERROR_LOG (id, errordate)';
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
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'is_zip')) then
        execute 'alter table CORE_LAYOUT_EXPORT add "is_zip" SMALLINT';
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
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'parameters')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add "parameters" VARCHAR';
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
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'parameters_setter_url')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add "parameters_setter_url" VARCHAR';
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
	if (not CORE_UPDSTRU_CheckExistTable('CORE_REFERENCE_ITEM_ORG')) then
		execute 'create table CORE_REFERENCE_ITEM_ORG ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM_ORG', 'id')) then
        execute 'alter table CORE_REFERENCE_ITEM_ORG add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM_ORG', 'ref_item_id')) then
        execute 'alter table CORE_REFERENCE_ITEM_ORG add "ref_item_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM_ORG', 'org_id')) then
        execute 'alter table CORE_REFERENCE_ITEM_ORG add "org_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM_ORG', 'up_id')) then
        execute 'alter table CORE_REFERENCE_ITEM_ORG add "up_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('core_reference_item_org_pkey')) then
    execute 'alter table CORE_REFERENCE_ITEM_ORG add constraint core_reference_item_org_pkey primary key (id)';
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
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'is_deleted')) then
        execute 'alter table CORE_REGISTER add "is_deleted" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'allpri_partitioning')) then
        execute 'alter table CORE_REGISTER add "allpri_partitioning" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'main_register')) then
        execute 'alter table CORE_REGISTER add "main_register" BIGINT';
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
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'is_deleted')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "is_deleted" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'change_user_id')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'change_date')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'hidden')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add "hidden" BIGINT DEFAULT 0';
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
  if (not CORE_UPDSTRU_CheckExistIndex('core_register_attribute_unique_name_constraint')) then
	execute 'CREATE UNIQUE INDEX core_register_attribute_unique_name_constraint on CORE_REGISTER_ATTRIBUTE (registerid)';
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
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'qscondition')) then
        execute 'alter table CORE_REGISTER_RELATION add "qscondition" VARCHAR';
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
        execute 'alter table CORE_SRD_AUDIT add "result_desc" VARCHAR';
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
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'auth_token')) then
        execute 'alter table CORE_SRD_SESSION add "auth_token" VARCHAR(1024)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'created_token')) then
        execute 'alter table CORE_SRD_SESSION add "created_token" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'refresh_token')) then
        execute 'alter table CORE_SRD_SESSION add "refresh_token" VARCHAR(1024)';
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
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'position_for_doc')) then
        execute 'alter table CORE_SRD_USER add "position_for_doc" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'short_name')) then
        execute 'alter table CORE_SRD_USER add "short_name" VARCHAR(500)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'short_name_for_doc')) then
        execute 'alter table CORE_SRD_USER add "short_name_for_doc" VARCHAR(500)';
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
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'search_type')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "search_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'selected_list')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "selected_list" VARCHAR(1000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'condition_fixed_search')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add "condition_fixed_search" VARCHAR';
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
	if (not CORE_UPDSTRU_CheckExistTable('DASHBOARDS_PANELINDEXCARDCACHE')) then
		execute 'create table DASHBOARDS_PANELINDEXCARDCACHE ("id" NUMERIC NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'id')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "id" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'key')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "key" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'lastrequested')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "lastrequested" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'expired')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "expired" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'updatingstart')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "updatingstart" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'updated')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "updated" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'status')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "status" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'enabled')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "enabled" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'updatecounter')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "updatecounter" NUMERIC NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'lasttimeout')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "lasttimeout" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANELINDEXCARDCACHE', 'datacacheitem')) then
        execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add "datacacheitem" VARCHAR NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('dashboards_panelindexcardcache_key_key')) then
    execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add constraint dashboards_panelindexcardcache_key_key unique (key)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('dashboards_panelindexcardcache_pkey')) then
    execute 'alter table DASHBOARDS_PANELINDEXCARDCACHE add constraint dashboards_panelindexcardcache_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('dashboards_panelindexcardcache_key_key')) then
	execute 'CREATE UNIQUE INDEX dashboards_panelindexcardcache_key_key on DASHBOARDS_PANELINDEXCARDCACHE (key)';
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
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK', 'change_user_id')) then
        execute 'alter table DECLARATIONS_BOOK add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK', 'change_date')) then
        execute 'alter table DECLARATIONS_BOOK add "change_date" TIMESTAMP';
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
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_BOOK_A')) then
		execute 'create table DECLARATIONS_BOOK_A ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'id')) then
        execute 'alter table DECLARATIONS_BOOK_A add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'object_id')) then
        execute 'alter table DECLARATIONS_BOOK_A add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'attribute_id')) then
        execute 'alter table DECLARATIONS_BOOK_A add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 's')) then
        execute 'alter table DECLARATIONS_BOOK_A add "s" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'po')) then
        execute 'alter table DECLARATIONS_BOOK_A add "po" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'ref_item_id')) then
        execute 'alter table DECLARATIONS_BOOK_A add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'text_value')) then
        execute 'alter table DECLARATIONS_BOOK_A add "text_value" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'number_value')) then
        execute 'alter table DECLARATIONS_BOOK_A add "number_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'date_value')) then
        execute 'alter table DECLARATIONS_BOOK_A add "date_value" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_BOOK_A', 'change_user_id')) then
        execute 'alter table DECLARATIONS_BOOK_A add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('declarations_book_a_pkey')) then
    execute 'alter table DECLARATIONS_BOOK_A add constraint declarations_book_a_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_book_a_obj_attr_idx')) then
	execute 'CREATE  INDEX declarations_book_a_obj_attr_idx on DECLARATIONS_BOOK_A (object_id, attribute_id)';
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
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'change_user_id')) then
        execute 'alter table DECLARATIONS_DECLARATION add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'change_date')) then
        execute 'alter table DECLARATIONS_DECLARATION add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'check_time')) then
        execute 'alter table DECLARATIONS_DECLARATION add "check_time" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'spd_app_id')) then
        execute 'alter table DECLARATIONS_DECLARATION add "spd_app_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'spd_app_name')) then
        execute 'alter table DECLARATIONS_DECLARATION add "spd_app_name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION', 'spd_app_date')) then
        execute 'alter table DECLARATIONS_DECLARATION add "spd_app_date" TIMESTAMP';
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
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_DECLARATION_A')) then
		execute 'create table DECLARATIONS_DECLARATION_A ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'id')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'object_id')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'attribute_id')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 's')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "s" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'po')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "po" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'ref_item_id')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'text_value')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "text_value" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'number_value')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "number_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'date_value')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "date_value" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_DECLARATION_A', 'change_user_id')) then
        execute 'alter table DECLARATIONS_DECLARATION_A add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('declarations_declaration_a_pkey')) then
    execute 'alter table DECLARATIONS_DECLARATION_A add constraint declarations_declaration_a_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_declaration_a_obj_attr_idx')) then
	execute 'CREATE  INDEX declarations_declaration_a_obj_attr_idx on DECLARATIONS_DECLARATION_A (object_id, attribute_id)';
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
        execute 'alter table DECLARATIONS_HAR_OKS add "har_1" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_2')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_2" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_OKS add "har_4" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_5')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_5" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_6')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_6" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_7')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_7" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_8')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_8" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_9')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_9" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_10')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_10" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_11')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_11" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_12')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_12" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_OKS add "har_17" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_18')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_18" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_19')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_19" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_20')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_20" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'har_21')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_1_3" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_OKS add "har_21_2_3" VARCHAR(4096)';
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
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'change_user_id')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS', 'change_date')) then
        execute 'alter table DECLARATIONS_HAR_OKS add "change_date" TIMESTAMP';
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
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_HAR_OKS_A')) then
		execute 'create table DECLARATIONS_HAR_OKS_A ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'id')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'object_id')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'attribute_id')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 's')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "s" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'po')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "po" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'ref_item_id')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'text_value')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "text_value" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'number_value')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "number_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'date_value')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "date_value" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_A', 'change_user_id')) then
        execute 'alter table DECLARATIONS_HAR_OKS_A add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('declarations_har_oks_a_pkey')) then
    execute 'alter table DECLARATIONS_HAR_OKS_A add constraint declarations_har_oks_a_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_har_oks_a_obj_attr_idx')) then
	execute 'CREATE  INDEX declarations_har_oks_a_obj_attr_idx on DECLARATIONS_HAR_OKS_A (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_HAR_OKS_ADDITIONAL_INFO')) then
		execute 'create table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_ADDITIONAL_INFO', 'id')) then
        execute 'alter table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_ADDITIONAL_INFO', 'har_oks_id')) then
        execute 'alter table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO add "har_oks_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_ADDITIONAL_INFO', 'har_oks_name')) then
        execute 'alter table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO add "har_oks_name" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_ADDITIONAL_INFO', 'har_status')) then
        execute 'alter table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO add "har_status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_OKS_ADDITIONAL_INFO', 'is_used_in_declaration')) then
        execute 'alter table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO add "is_used_in_declaration" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_509_q_pk')) then
    execute 'alter table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO add constraint reg_509_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_509_q_unique')) then
    execute 'alter table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO add constraint reg_509_q_unique unique (har_oks_id, har_oks_name)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_har_oks_additional_info_har_id_idx')) then
	execute 'CREATE  INDEX declarations_har_oks_additional_info_har_id_idx on DECLARATIONS_HAR_OKS_ADDITIONAL_INFO (har_oks_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_509_q_unique')) then
	execute 'CREATE UNIQUE INDEX reg_509_q_unique on DECLARATIONS_HAR_OKS_ADDITIONAL_INFO (har_oks_id, har_oks_name)';
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
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_6" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_1" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_3" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_4')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_4" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_5')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_5" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_7')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_7" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_8')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_8" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_9')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_9" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_10')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_10" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_12')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_12" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_13')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_1_3" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_13_2_3" VARCHAR(4096)';
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
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_14" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_15')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_15" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_16')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_16" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_17')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_17" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_18')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_18" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_19')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_19" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_20')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_20" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_21')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_21" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'har_11')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "har_11" VARCHAR(4096)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'change_user_id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL', 'change_date')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL add "change_date" TIMESTAMP';
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
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_HAR_PARCEL_A')) then
		execute 'create table DECLARATIONS_HAR_PARCEL_A ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'object_id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'attribute_id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 's')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "s" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'po')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "po" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'ref_item_id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'text_value')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "text_value" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'number_value')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "number_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'date_value')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "date_value" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_A', 'change_user_id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_A add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('declarations_har_parcel_a_pkey')) then
    execute 'alter table DECLARATIONS_HAR_PARCEL_A add constraint declarations_har_parcel_a_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_har_parcel_a_obj_attr_idx')) then
	execute 'CREATE  INDEX declarations_har_parcel_a_obj_attr_idx on DECLARATIONS_HAR_PARCEL_A (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO')) then
		execute 'create table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO', 'id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO', 'har_parcel_id')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO add "har_parcel_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO', 'har_parcel_name')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO add "har_parcel_name" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO', 'har_status')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO add "har_status" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO', 'is_used_in_declaration')) then
        execute 'alter table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO add "is_used_in_declaration" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_510_q_pk')) then
    execute 'alter table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO add constraint reg_510_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_510_q_unique')) then
    execute 'alter table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO add constraint reg_510_q_unique unique (har_parcel_id, har_parcel_name)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_har_parcel_additional_info_har_id_idx')) then
	execute 'CREATE  INDEX declarations_har_parcel_additional_info_har_id_idx on DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO (har_parcel_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_510_q_unique')) then
	execute 'CREATE UNIQUE INDEX reg_510_q_unique on DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO (har_parcel_id, har_parcel_name)';
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
        execute 'alter table DECLARATIONS_RESULT add "text_yes" VARCHAR(1024)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_RESULT', 'text_no')) then
        execute 'alter table DECLARATIONS_RESULT add "text_no" VARCHAR(1024)';
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
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_SIGNATORY')) then
		execute 'create table DECLARATIONS_SIGNATORY ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SIGNATORY', 'id')) then
        execute 'alter table DECLARATIONS_SIGNATORY add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SIGNATORY', 'full_name')) then
        execute 'alter table DECLARATIONS_SIGNATORY add "full_name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SIGNATORY', 'position')) then
        execute 'alter table DECLARATIONS_SIGNATORY add "position" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_508_q_pk')) then
    execute 'alter table DECLARATIONS_SIGNATORY add constraint reg_508_q_pk primary key (id)';
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
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'zip')) then
        execute 'alter table DECLARATIONS_SUBJECT add "zip" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'city')) then
        execute 'alter table DECLARATIONS_SUBJECT add "city" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'street')) then
        execute 'alter table DECLARATIONS_SUBJECT add "street" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'house')) then
        execute 'alter table DECLARATIONS_SUBJECT add "house" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'building')) then
        execute 'alter table DECLARATIONS_SUBJECT add "building" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'flat')) then
        execute 'alter table DECLARATIONS_SUBJECT add "flat" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'change_user_id')) then
        execute 'alter table DECLARATIONS_SUBJECT add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'change_date')) then
        execute 'alter table DECLARATIONS_SUBJECT add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT', 'office_number')) then
        execute 'alter table DECLARATIONS_SUBJECT add "office_number" VARCHAR(255)';
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
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_SUBJECT_A')) then
		execute 'create table DECLARATIONS_SUBJECT_A ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'id')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'object_id')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'attribute_id')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 's')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "s" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'po')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "po" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'ref_item_id')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'text_value')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "text_value" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'number_value')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "number_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'date_value')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "date_value" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_SUBJECT_A', 'change_user_id')) then
        execute 'alter table DECLARATIONS_SUBJECT_A add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('declarations_subject_a_pkey')) then
    execute 'alter table DECLARATIONS_SUBJECT_A add constraint declarations_subject_a_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_subject_a_obj_attr_idx')) then
	execute 'CREATE  INDEX declarations_subject_a_obj_attr_idx on DECLARATIONS_SUBJECT_A (object_id, attribute_id)';
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
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'rejection_reason')) then
        execute 'alter table DECLARATIONS_UVED add "rejection_reason" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'rejection_reason_type')) then
        execute 'alter table DECLARATIONS_UVED add "rejection_reason_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'change_user_id')) then
        execute 'alter table DECLARATIONS_UVED add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'change_date')) then
        execute 'alter table DECLARATIONS_UVED add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED', 'annex')) then
        execute 'alter table DECLARATIONS_UVED add "annex" VARCHAR';
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
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_UVED_A')) then
		execute 'create table DECLARATIONS_UVED_A ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'id')) then
        execute 'alter table DECLARATIONS_UVED_A add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'object_id')) then
        execute 'alter table DECLARATIONS_UVED_A add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'attribute_id')) then
        execute 'alter table DECLARATIONS_UVED_A add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 's')) then
        execute 'alter table DECLARATIONS_UVED_A add "s" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'po')) then
        execute 'alter table DECLARATIONS_UVED_A add "po" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'ref_item_id')) then
        execute 'alter table DECLARATIONS_UVED_A add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'text_value')) then
        execute 'alter table DECLARATIONS_UVED_A add "text_value" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'number_value')) then
        execute 'alter table DECLARATIONS_UVED_A add "number_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'date_value')) then
        execute 'alter table DECLARATIONS_UVED_A add "date_value" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_A', 'change_user_id')) then
        execute 'alter table DECLARATIONS_UVED_A add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('declarations_uved_a_pkey')) then
    execute 'alter table DECLARATIONS_UVED_A add constraint declarations_uved_a_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_uved_a_obj_attr_idx')) then
	execute 'CREATE  INDEX declarations_uved_a_obj_attr_idx on DECLARATIONS_UVED_A (object_id, attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('DECLARATIONS_UVED_REJECTION_REASON_TYPE')) then
		execute 'create table DECLARATIONS_UVED_REJECTION_REASON_TYPE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_REJECTION_REASON_TYPE', 'id')) then
        execute 'alter table DECLARATIONS_UVED_REJECTION_REASON_TYPE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_REJECTION_REASON_TYPE', 'uved_id')) then
        execute 'alter table DECLARATIONS_UVED_REJECTION_REASON_TYPE add "uved_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('DECLARATIONS_UVED_REJECTION_REASON_TYPE', 'rejection_reason_type')) then
        execute 'alter table DECLARATIONS_UVED_REJECTION_REASON_TYPE add "rejection_reason_type" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_507_q_pk')) then
    execute 'alter table DECLARATIONS_UVED_REJECTION_REASON_TYPE add constraint reg_507_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_507_q_unique')) then
    execute 'alter table DECLARATIONS_UVED_REJECTION_REASON_TYPE add constraint reg_507_q_unique unique (uved_id, rejection_reason_type)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('declarations_uved_rej_reason_type_uved_id_idx')) then
	execute 'CREATE  INDEX declarations_uved_rej_reason_type_uved_id_idx on DECLARATIONS_UVED_REJECTION_REASON_TYPE (uved_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_507_q_unique')) then
	execute 'CREATE UNIQUE INDEX reg_507_q_unique on DECLARATIONS_UVED_REJECTION_REASON_TYPE (uved_id, rejection_reason_type)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('ES_EXPRESS_SCORE')) then
		execute 'create table ES_EXPRESS_SCORE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'id')) then
        execute 'alter table ES_EXPRESS_SCORE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'kn')) then
        execute 'alter table ES_EXPRESS_SCORE add "kn" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'date_cost')) then
        execute 'alter table ES_EXPRESS_SCORE add "date_cost" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'summary_cost')) then
        execute 'alter table ES_EXPRESS_SCORE add "summary_cost" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'cost_square_meter')) then
        execute 'alter table ES_EXPRESS_SCORE add "cost_square_meter" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'square')) then
        execute 'alter table ES_EXPRESS_SCORE add "square" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'floor')) then
        execute 'alter table ES_EXPRESS_SCORE add "floor" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'object_id')) then
        execute 'alter table ES_EXPRESS_SCORE add "object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'scenario_type')) then
        execute 'alter table ES_EXPRESS_SCORE add "scenario_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'deal_type')) then
        execute 'alter table ES_EXPRESS_SCORE add "deal_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'segment_type')) then
        execute 'alter table ES_EXPRESS_SCORE add "segment_type" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'address')) then
        execute 'alter table ES_EXPRESS_SCORE add "address" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'target_market_object_id')) then
        execute 'alter table ES_EXPRESS_SCORE add "target_market_object_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'cost_calculate_factors')) then
        execute 'alter table ES_EXPRESS_SCORE add "cost_calculate_factors" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'change_user_id')) then
        execute 'alter table ES_EXPRESS_SCORE add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_EXPRESS_SCORE', 'change_date')) then
        execute 'alter table ES_EXPRESS_SCORE add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('es_exspress_score_pkey')) then
    execute 'alter table ES_EXPRESS_SCORE add constraint es_exspress_score_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('ES_REFERENCE')) then
		execute 'create table ES_REFERENCE ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE', 'id')) then
        execute 'alter table ES_REFERENCE add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE', 'name')) then
        execute 'alter table ES_REFERENCE add "name" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE', 'value_type')) then
        execute 'alter table ES_REFERENCE add "value_type" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE', 'value_type_code')) then
        execute 'alter table ES_REFERENCE add "value_type_code" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE', 'use_interval')) then
        execute 'alter table ES_REFERENCE add "use_interval" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE', 'change_user_id')) then
        execute 'alter table ES_REFERENCE add "change_user_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE', 'change_date')) then
        execute 'alter table ES_REFERENCE add "change_date" TIMESTAMP';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_609_q_pk')) then
    execute 'alter table ES_REFERENCE add constraint reg_609_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('ES_REFERENCE_ITEM')) then
		execute 'create table ES_REFERENCE_ITEM ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE_ITEM', 'id')) then
        execute 'alter table ES_REFERENCE_ITEM add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE_ITEM', 'es_reference_id')) then
        execute 'alter table ES_REFERENCE_ITEM add "es_reference_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE_ITEM', 'value')) then
        execute 'alter table ES_REFERENCE_ITEM add "value" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE_ITEM', 'calculation_value')) then
        execute 'alter table ES_REFERENCE_ITEM add "calculation_value" NUMERIC';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE_ITEM', 'common_value')) then
        execute 'alter table ES_REFERENCE_ITEM add "common_value" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE_ITEM', 'value_from')) then
        execute 'alter table ES_REFERENCE_ITEM add "value_from" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_REFERENCE_ITEM', 'value_to')) then
        execute 'alter table ES_REFERENCE_ITEM add "value_to" VARCHAR(255)';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_610_q_pk')) then
    execute 'alter table ES_REFERENCE_ITEM add constraint reg_610_q_pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('ES_SETTINGS_PARAMS')) then
		execute 'create table ES_SETTINGS_PARAMS ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_SETTINGS_PARAMS', 'id')) then
        execute 'alter table ES_SETTINGS_PARAMS add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_SETTINGS_PARAMS', 'id_tour')) then
        execute 'alter table ES_SETTINGS_PARAMS add "id_tour" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_SETTINGS_PARAMS', 'id_register')) then
        execute 'alter table ES_SETTINGS_PARAMS add "id_register" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_SETTINGS_PARAMS', 'cost_factors')) then
        execute 'alter table ES_SETTINGS_PARAMS add "cost_factors" VARCHAR';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_SETTINGS_PARAMS', 'segment_type')) then
        execute 'alter table ES_SETTINGS_PARAMS add "segment_type" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_SETTINGS_PARAMS', 'build_cad_number')) then
        execute 'alter table ES_SETTINGS_PARAMS add "build_cad_number" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('es_settings_params_pkey')) then
    execute 'alter table ES_SETTINGS_PARAMS add constraint es_settings_params_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('ES_TO_MARKET_CORE_OBJECT')) then
		execute 'create table ES_TO_MARKET_CORE_OBJECT ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_TO_MARKET_CORE_OBJECT', 'id')) then
        execute 'alter table ES_TO_MARKET_CORE_OBJECT add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_TO_MARKET_CORE_OBJECT', 'es_id')) then
        execute 'alter table ES_TO_MARKET_CORE_OBJECT add "es_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('ES_TO_MARKET_CORE_OBJECT', 'market_object_id')) then
        execute 'alter table ES_TO_MARKET_CORE_OBJECT add "market_object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('es_to_market_core_object_pkey')) then
    execute 'alter table ES_TO_MARKET_CORE_OBJECT add constraint es_to_market_core_object_pkey primary key (id)';
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
	if (not CORE_UPDSTRU_CheckExistTable('GBU_ATTRIBUTE_SETTINGS')) then
		execute 'create table GBU_ATTRIBUTE_SETTINGS ("attribute_id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_ATTRIBUTE_SETTINGS', 'attribute_id')) then
        execute 'alter table GBU_ATTRIBUTE_SETTINGS add "attribute_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_ATTRIBUTE_SETTINGS', 'use_parent_attribute_for_living_placements')) then
        execute 'alter table GBU_ATTRIBUTE_SETTINGS add "use_parent_attribute_for_living_placements" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_ATTRIBUTE_SETTINGS', 'use_parent_attribute_for_not_living_placements')) then
        execute 'alter table GBU_ATTRIBUTE_SETTINGS add "use_parent_attribute_for_not_living_placements" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_ATTRIBUTE_SETTINGS', 'use_parent_attribute_for_car_place')) then
        execute 'alter table GBU_ATTRIBUTE_SETTINGS add "use_parent_attribute_for_car_place" SMALLINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_81_q_pk')) then
    execute 'alter table GBU_ATTRIBUTE_SETTINGS add constraint reg_81_q_pk primary key (attribute_id)';
  end if;
end $$;
--<DO>--

DO $$
begin
	if (not CORE_UPDSTRU_CheckExistTable('GBU_KADASTR_KVARTAL')) then
		execute 'create table GBU_KADASTR_KVARTAL ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_KADASTR_KVARTAL', 'id')) then
        execute 'alter table GBU_KADASTR_KVARTAL add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_KADASTR_KVARTAL', 'kadastr_kvartal')) then
        execute 'alter table GBU_KADASTR_KVARTAL add "kadastr_kvartal" VARCHAR(255) NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_KADASTR_KVARTAL', 'parent_id')) then
        execute 'alter table GBU_KADASTR_KVARTAL add "parent_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_KADASTR_KVARTAL', 'type_territory_2020')) then
        execute 'alter table GBU_KADASTR_KVARTAL add "type_territory_2020" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('GBU_KADASTR_KVARTAL', 'type_territory_2017')) then
        execute 'alter table GBU_KADASTR_KVARTAL add "type_territory_2017" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('gbu_kadastr_kvartal_kadastr_kvartal_key')) then
    execute 'alter table GBU_KADASTR_KVARTAL add constraint gbu_kadastr_kvartal_kadastr_kvartal_key unique (kadastr_kvartal)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('gbu_kadastr_kvartal_pkey')) then
    execute 'alter table GBU_KADASTR_KVARTAL add constraint gbu_kadastr_kvartal_pkey primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('gbu_kadastr_kvartal_kadastr_kvartal_key')) then
	execute 'CREATE UNIQUE INDEX gbu_kadastr_kvartal_kadastr_kvartal_key on GBU_KADASTR_KVARTAL (kadastr_kvartal)';
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
    if (not core_updstru_CheckExistColumn('GBU_MAIN_OBJECT', 'kadastr_kvartal')) then
        execute 'alter table GBU_MAIN_OBJECT add "kadastr_kvartal" VARCHAR(255)';
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
  if (not CORE_UPDSTRU_CheckExistIndex('gbu_main_object_kad_num_text_inx')) then
	execute 'CREATE  INDEX gbu_main_object_kad_num_text_inx on GBU_MAIN_OBJECT (cadastral_number)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('gbu_main_object_type_idx')) then
	execute 'CREATE  INDEX gbu_main_object_type_idx on GBU_MAIN_OBJECT (object_type_code)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('gbu_main_object_kvart_idx')) then
	execute 'CREATE  INDEX gbu_main_object_kvart_idx on GBU_MAIN_OBJECT (kadastr_kvartal)';
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
        execute 'alter table gbu_source1_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source1_a_dt', 'value')) then
        execute 'alter table gbu_source1_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source1_a_num add "actual" BIGINT';
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
        execute 'alter table gbu_source1_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source1_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source10_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_dt', 'value')) then
        execute 'alter table gbu_source10_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source10_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source10_a_num', 'value')) then
        execute 'alter table gbu_source10_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source10_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source10_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source11_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_dt', 'value')) then
        execute 'alter table gbu_source11_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source11_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source11_a_num', 'value')) then
        execute 'alter table gbu_source11_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source11_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source11_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source12_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_dt', 'value')) then
        execute 'alter table gbu_source12_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source12_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source12_a_num', 'value')) then
        execute 'alter table gbu_source12_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source12_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source12_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source13_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_dt', 'value')) then
        execute 'alter table gbu_source13_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source13_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source13_a_num', 'value')) then
        execute 'alter table gbu_source13_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source13_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source13_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source14_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_dt', 'value')) then
        execute 'alter table gbu_source14_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source14_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source14_a_num', 'value')) then
        execute 'alter table gbu_source14_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source14_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source14_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source15_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_dt', 'value')) then
        execute 'alter table gbu_source15_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source15_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source15_a_num', 'value')) then
        execute 'alter table gbu_source15_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source15_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source15_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source16_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_dt', 'value')) then
        execute 'alter table gbu_source16_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source16_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source16_a_num', 'value')) then
        execute 'alter table gbu_source16_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source16_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source16_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source17_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_dt', 'value')) then
        execute 'alter table gbu_source17_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source17_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source17_a_num', 'value')) then
        execute 'alter table gbu_source17_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source17_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source17_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source18_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_dt', 'value')) then
        execute 'alter table gbu_source18_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source18_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source18_a_num', 'value')) then
        execute 'alter table gbu_source18_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source18_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source18_a_txt add "value" VARCHAR(5000)';
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
        execute 'alter table gbu_source19_a_dt add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_dt', 'value')) then
        execute 'alter table gbu_source19_a_dt add "value" TIMESTAMP';
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
        execute 'alter table gbu_source19_a_num add "actual" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source19_a_num', 'value')) then
        execute 'alter table gbu_source19_a_num add "value" NUMERIC';
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
        execute 'alter table gbu_source19_a_txt add "actual" BIGINT';
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
        execute 'alter table gbu_source19_a_txt add "value" VARCHAR(5000)';
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
	if (not CORE_UPDSTRU_CheckExistTable('gbu_source2_a_1')) then
		execute 'create table gbu_source2_a_1 ("id" BIGINT NOT NULL)';
	end if;
end $$;
--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 'id')) then
        execute 'alter table gbu_source2_a_1 add "id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 'object_id')) then
        execute 'alter table gbu_source2_a_1 add "object_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 'ot')) then
        execute 'alter table gbu_source2_a_1 add "ot" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 's')) then
        execute 'alter table gbu_source2_a_1 add "s" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 'ref_item_id')) then
        execute 'alter table gbu_source2_a_1 add "ref_item_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 'value')) then
        execute 'alter table gbu_source2_a_1 add "value" VARCHAR(4000)';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 'change_date')) then
        execute 'alter table gbu_source2_a_1 add "change_date" TIMESTAMP NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 'change_user_id')) then
        execute 'alter table gbu_source2_a_1 add "change_user_id" BIGINT NOT NULL';
    end if;
end $$;

--<DO>--

DO $$
begin
    if (not core_updstru_CheckExistColumn('gbu_source2_a_1', 'change_doc_id')) then
        execute 'alter table gbu_source2_a_1 add "change_doc_id" BIGINT';
    end if;
end $$;

--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_2_a_1__pk')) then
    execute 'alter table gbu_source2_a_1 add constraint reg_2_a_1__pk primary key (id)';
  end if;
end $$;
--<DO>--

DO $$
begin
  if (not core_updstru_checkexistconstraint('reg_2_a_1__fk_o')) then
	execute 'alter table gbu_source2_a_1 add constraint reg_2_a_1__fk_o foreign key (object_id) references gbu_main_object (id)';
  end if;
end $$;

--<DO>--

DO $$
begin
  if (not CORE_UPDSTRU_CheckExistIndex('reg_2_a_1_inx_obj_attr_id')) then
	execute 'CREATE UNIQUE INDEX reg_2_a_1_inx_obj_attr_id on gbu_source2_a_1 (object_id, ot)';
  end if;
end $$;
--<DO>--
