--?????????????? ??????? COMMON_REPORT_FILES
			
			create table common_report_files
			(
			    id             bigint       not null
			        constraint reg_810_q_pk
			            primary key,
			    user_id        bigint       not null,
			    creation_date  timestamp    not null,
			    status         bigint       not null,
			    file_name      varchar(255) not null,
			    finish_date    timestamp,
			    file_extension varchar(255) not null
			);
			comment on table common_report_files is '??????? ??? ????????? ???????';
			alter table common_report_files owner to cipjs_kad_ozenka;

			INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (810, 'Common.ReportFiles', '??????? ??? ????????? ???????', null, null, 'COMMON_REPORT_FILES', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
			
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000100, '?????????????', 810, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, '2020-12-24 10:43:17.735173', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000200, '?? ????????????', 810, 1, null, null, 'user_id', null, null, 0, null, null, 'UserId', 0, null, null, null, 0, 2, '2020-12-24 10:44:55.410758', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000800, '?????????? ?????', 810, 4, null, null, 'file_extension', null, null, 0, null, null, 'FileExtension', 0, null, null, null, 0, 2, '2020-12-24 15:10:47.348561', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000300, '???? ????????', 810, 5, null, null, 'creation_date', null, null, 0, null, null, 'CreationDate', 0, null, null, null, 0, 2, '2020-12-24 10:48:16.873168', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000500, '??????', 810, 4, null, 802, null, 'status', null, 0, null, null, 'Status', 0, null, null, null, 0, 2, '2020-12-24 11:00:24.453443', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000600, '??? ?????', 810, 4, null, null, 'file_name', null, null, 0, null, null, 'FileName', 0, null, null, null, 0, 2, '2020-12-24 12:13:23.174627', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81000700, '???? ??????????', 810, 5, null, null, 'finish_date', null, null, 0, null, null, 'FinishDate', 1, null, null, null, 0, 2, '2020-12-24 12:26:04.790233', 0);

			INSERT INTO core_register_relation (id, name, parentregister, chieldregister, cardinality, kindid, parentregister_attribute_id, qscondition) VALUES (222, '?? ????????? ??????? ? ????????????', 950, 1000811, null, 81100200, null, null);


			--?????????????? view all_reports_in_system

			CREATE VIEW public.all_reports_in_system (
			    id,
			    file_name,
			    creation_date,
			    finish_date,
			    user_id,
			    status,
			    is_platform)
			AS
			SELECT common_report_files.id,
			    common_report_files.file_name,
			    common_report_files.creation_date,
			    common_report_files.finish_date,
			    common_report_files.user_id,
			        CASE
			            WHEN common_report_files.status = 0 THEN '???????'::text
			            WHEN common_report_files.status = 1 THEN '????????'::text
			            WHEN common_report_files.status = 2 THEN '?????????'::text
			            WHEN common_report_files.status = 3 THEN '??????'::text
			            ELSE NULL::text
			        END AS status,
			    (
			    SELECT false AS bool
			    ) AS is_platform
			FROM common_report_files
			UNION ALL
			SELECT fm_reports_savedreport.id,
			    fm_reports_savedreport.title AS file_name,
			    fm_reports_savedreport.create_date AS creation_date,
			    fm_reports_savedreport.end_date AS finish_date,
			    fm_reports_savedreport.user_id,
			        CASE
			            WHEN fm_reports_savedreport.status = 0 THEN '???????'::text
			            WHEN fm_reports_savedreport.status = 1 THEN '????????'::text
			            WHEN fm_reports_savedreport.status = 2 THEN '?????????'::text
			            WHEN fm_reports_savedreport.status = 3 THEN '??????'::text
			            ELSE NULL::text
			        END AS status,
			    (
			    SELECT true AS bool
			    ) AS is_platform
			FROM fm_reports_savedreport;
			ALTER VIEW all_reports_in_system OWNER TO cipjs_kad_ozenka;

			INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (1000811, 'Common.AllReportsInSystemView', 'View ?? ????? ???????? ? ??????? (????????????? + ??????????????? ??????? ????? ?????????? ???????)', null, null, 'all_reports_in_system', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
			
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100300, '??? ?????', 1000811, 4, null, null, 'file_name', null, null, 0, null, null, 'FileName', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100400, '???? ????????', 1000811, 5, null, null, 'creation_date', null, null, 0, null, null, 'CreationDate', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100500, '???? ??????????', 1000811, 5, null, null, 'finish_date', null, null, 0, null, null, 'FinishDate', 1, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100700, '??????', 1000811, 4, null, null, 'status', null, null, 0, null, null, 'Status', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100600, '????????????? ?????', 1000811, 3, null, null, 'is_platform', null, null, 0, null, null, 'IsPlatformReport', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100100, '?????????????', 1000811, 1, null, null, 'id', null, null, 1, null, null, 'Id', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);
			INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (81100200, '?? ????????????', 1000811, 1, null, null, 'user_id', null, null, 0, null, null, 'UserId', 0, null, null, null, 0, 2, '2020-12-25 00:00:00.000000', 0);


			--???????? ????? ?????????
			delete from core_layout where layoutid=10009365;
			delete from core_layout_details  where layoutid=10009365;