
/*reg_987_quant_pk 1*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_attachment_file') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_attachment_file') THEN
			ALTER TABLE core_attachment_file DROP CONSTRAINT IF EXISTS reg_987_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_987_quant_pk;
		alter table CORE_ATTACHMENT_FILE add constraint reg_987_quant_pk primary key (id);
	END IF;
END $$;

/*reg_40116955_q_pk 2*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_27_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_27_q') THEN
			ALTER TABLE source_27_q DROP CONSTRAINT IF EXISTS reg_40116955_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_40116955_q_pk;
		CREATE UNIQUE INDEX reg_40116955_q_pk ON public.source_27_q USING btree (id);
	END IF;
END $$;

/*reg_989_quant_pk 3*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_error_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_error_log') THEN
			ALTER TABLE core_error_log DROP CONSTRAINT IF EXISTS reg_989_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_989_quant_pk;
		alter table CORE_ERROR_LOG add constraint reg_989_quant_pk primary key (id);
	END IF;
END $$;

/*core_error_log_idx 4*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_error_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_error_log') THEN
			ALTER TABLE core_error_log DROP CONSTRAINT IF EXISTS core_error_log_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_error_log_idx;
		CREATE INDEX core_error_log_idx ON public.core_error_log USING btree (errordate);
	END IF;
END $$;

/*core_error_log_composite_idx 5*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_error_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_error_log') THEN
			ALTER TABLE core_error_log DROP CONSTRAINT IF EXISTS core_error_log_composite_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_error_log_composite_idx;
		CREATE INDEX core_error_log_composite_idx ON public.core_error_log USING btree (id, errordate);
	END IF;
END $$;

/*reg_986_quant_pk 6*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_attachment') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_attachment') THEN
			ALTER TABLE core_attachment DROP CONSTRAINT IF EXISTS reg_986_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_986_quant_pk;
		alter table CORE_ATTACHMENT add constraint reg_986_quant_pk primary key (id);
	END IF;
END $$;

/*reg_22_a_dt_pk 7*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source22_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source22_a_dt') THEN
			ALTER TABLE gbu_source22_a_dt DROP CONSTRAINT IF EXISTS reg_22_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_22_a_dt_pk;
		CREATE UNIQUE INDEX reg_22_a_dt_pk ON public.gbu_source22_a_dt USING btree (id);
	END IF;
END $$;

/*reg_22_a_dt_inx_obj_attr_id 8*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source22_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source22_a_dt') THEN
			ALTER TABLE gbu_source22_a_dt DROP CONSTRAINT IF EXISTS reg_22_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_22_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_22_a_dt_inx_obj_attr_id ON public.gbu_source22_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_930_idx 9*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_register_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_register_deleted') THEN
			ALTER TABLE core_register_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_930_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_930_idx;
		CREATE INDEX deleted_event_id_930_idx ON public.core_register_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_22_a_num_pk 10*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source22_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source22_a_num') THEN
			ALTER TABLE gbu_source22_a_num DROP CONSTRAINT IF EXISTS reg_22_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_22_a_num_pk;
		CREATE UNIQUE INDEX reg_22_a_num_pk ON public.gbu_source22_a_num USING btree (id);
	END IF;
END $$;

/*reg_22_a_num_inx_obj_attr_id 11*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source22_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source22_a_num') THEN
			ALTER TABLE gbu_source22_a_num DROP CONSTRAINT IF EXISTS reg_22_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_22_a_num_inx_obj_attr_id;
		CREATE INDEX reg_22_a_num_inx_obj_attr_id ON public.gbu_source22_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_931_idx 12*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_register_attribute_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_register_attribute_deleted') THEN
			ALTER TABLE core_register_attribute_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_931_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_931_idx;
		CREATE INDEX deleted_event_id_931_idx ON public.core_register_attribute_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_986_idx 13*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_attachment_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_attachment_deleted') THEN
			ALTER TABLE core_attachment_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_986_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_986_idx;
		CREATE INDEX deleted_event_id_986_idx ON public.core_attachment_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_988_idx 14*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_attachment_object_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_attachment_object_deleted') THEN
			ALTER TABLE core_attachment_object_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_988_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_988_idx;
		CREATE INDEX deleted_event_id_988_idx ON public.core_attachment_object_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_22986693_q_pk 15*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors12506545') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors12506545') THEN
			ALTER TABLE ko_tourzufactors12506545 DROP CONSTRAINT IF EXISTS reg_22986693_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_22986693_q_pk;
		CREATE UNIQUE INDEX reg_22986693_q_pk ON public.ko_tourzufactors12506545 USING btree (id);
	END IF;
END $$;

/*reg_28_a_txt_pk 16*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source28_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source28_a_txt') THEN
			ALTER TABLE gbu_source28_a_txt DROP CONSTRAINT IF EXISTS reg_28_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_28_a_txt_pk;
		CREATE UNIQUE INDEX reg_28_a_txt_pk ON public.gbu_source28_a_txt USING btree (id);
	END IF;
END $$;

/*reg_28_a_txt_inx_obj_attr_id 17*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source28_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source28_a_txt') THEN
			ALTER TABLE gbu_source28_a_txt DROP CONSTRAINT IF EXISTS reg_28_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_28_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_28_a_txt_inx_obj_attr_id ON public.gbu_source28_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_23_a_dt_pk 18*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source23_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source23_a_dt') THEN
			ALTER TABLE gbu_source23_a_dt DROP CONSTRAINT IF EXISTS reg_23_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_23_a_dt_pk;
		CREATE UNIQUE INDEX reg_23_a_dt_pk ON public.gbu_source23_a_dt USING btree (id);
	END IF;
END $$;

/*reg_23_a_dt_inx_obj_attr_id 19*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source23_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source23_a_dt') THEN
			ALTER TABLE gbu_source23_a_dt DROP CONSTRAINT IF EXISTS reg_23_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_23_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_23_a_dt_inx_obj_attr_id ON public.gbu_source23_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_18_a_num_pk 20*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source18_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source18_a_num') THEN
			ALTER TABLE gbu_source18_a_num DROP CONSTRAINT IF EXISTS reg_18_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_18_a_num_pk;
		CREATE UNIQUE INDEX reg_18_a_num_pk ON public.gbu_source18_a_num USING btree (id);
	END IF;
END $$;

/*reg_18_a_num_inx_obj_attr_id 21*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source18_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source18_a_num') THEN
			ALTER TABLE gbu_source18_a_num DROP CONSTRAINT IF EXISTS reg_18_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_18_a_num_inx_obj_attr_id;
		CREATE INDEX reg_18_a_num_inx_obj_attr_id ON public.gbu_source18_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578488_a_txt_pk 22*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_43_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_43_txt') THEN
			ALTER TABLE gbu_custom_source_43_txt DROP CONSTRAINT IF EXISTS reg_54578488_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578488_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578488_a_txt_pk ON public.gbu_custom_source_43_txt USING btree (id);
	END IF;
END $$;

/*reg_54578488_a_txt_inx_obj_attr_id 23*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_43_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_43_txt') THEN
			ALTER TABLE gbu_custom_source_43_txt DROP CONSTRAINT IF EXISTS reg_54578488_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578488_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578488_a_txt_inx_obj_attr_id ON public.gbu_custom_source_43_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_958_quant_pk 24*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_function_reg_cat') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_function_reg_cat') THEN
			ALTER TABLE core_srd_function_reg_cat DROP CONSTRAINT IF EXISTS reg_958_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_958_quant_pk;
		alter table CORE_SRD_FUNCTION_REG_CAT add constraint reg_958_quant_pk primary key (id);
	END IF;
END $$;

/*reg_4_a_num_pk 25*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source4_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source4_a_num') THEN
			ALTER TABLE gbu_source4_a_num DROP CONSTRAINT IF EXISTS reg_4_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_4_a_num_pk;
		CREATE UNIQUE INDEX reg_4_a_num_pk ON public.gbu_source4_a_num USING btree (id);
	END IF;
END $$;

/*reg_4_a_num_inx_obj_attr_id 26*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source4_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source4_a_num') THEN
			ALTER TABLE gbu_source4_a_num DROP CONSTRAINT IF EXISTS reg_4_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_4_a_num_inx_obj_attr_id;
		CREATE INDEX reg_4_a_num_inx_obj_attr_id ON public.gbu_source4_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_952_quant_pk 27*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_user_role') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_user_role') THEN
			ALTER TABLE core_srd_user_role DROP CONSTRAINT IF EXISTS reg_952_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_952_quant_pk;
		alter table CORE_SRD_USER_ROLE add constraint reg_952_quant_pk primary key (id);
	END IF;
END $$;

/*reg_2_a_660__pk 28*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_660') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_660') THEN
			ALTER TABLE gbu_source2_a_660 DROP CONSTRAINT IF EXISTS reg_2_a_660__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_660__pk;
		CREATE UNIQUE INDEX reg_2_a_660__pk ON public.gbu_source2_a_660 USING btree (id);
	END IF;
END $$;

/*reg_2_a_660_inx_obj_attr_id 29*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_660') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_660') THEN
			ALTER TABLE gbu_source2_a_660 DROP CONSTRAINT IF EXISTS reg_2_a_660_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_660_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_660_inx_obj_attr_id ON public.gbu_source2_a_660 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_41707116_q_pk 30*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors41707115') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors41707115') THEN
			ALTER TABLE ko_tourzufactors41707115 DROP CONSTRAINT IF EXISTS reg_41707116_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41707116_q_pk;
		CREATE UNIQUE INDEX reg_41707116_q_pk ON public.ko_tourzufactors41707115 USING btree (id);
	END IF;
END $$;

/*reg_258_q_pk 31*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tour_attribute_settings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tour_attribute_settings') THEN
			ALTER TABLE ko_tour_attribute_settings DROP CONSTRAINT IF EXISTS reg_258_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_258_q_pk;
		alter table KO_TOUR_ATTRIBUTE_SETTINGS add constraint reg_258_q_pk primary key (id);
	END IF;
END $$;

/*reg_42726724_a_num_pk 32*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_26_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_26_num') THEN
			ALTER TABLE gbu_custom_source_26_num DROP CONSTRAINT IF EXISTS reg_42726724_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42726724_a_num_pk;
		CREATE UNIQUE INDEX reg_42726724_a_num_pk ON public.gbu_custom_source_26_num USING btree (id);
	END IF;
END $$;

/*reg_42726724_a_num_inx_obj_attr_id 33*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_26_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_26_num') THEN
			ALTER TABLE gbu_custom_source_26_num DROP CONSTRAINT IF EXISTS reg_42726724_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42726724_a_num_inx_obj_attr_id;
		CREATE INDEX reg_42726724_a_num_inx_obj_attr_id ON public.gbu_custom_source_26_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_27388644_q_pk 34*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors12506547') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors12506547') THEN
			ALTER TABLE ko_tourzufactors12506547 DROP CONSTRAINT IF EXISTS reg_27388644_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_27388644_q_pk;
		CREATE UNIQUE INDEX reg_27388644_q_pk ON public.ko_tourzufactors12506547 USING btree (id);
	END IF;
END $$;

/*reg_44355285_a_txt_pk 35*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_27_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_27_txt') THEN
			ALTER TABLE gbu_custom_source_27_txt DROP CONSTRAINT IF EXISTS reg_44355285_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355285_a_txt_pk;
		CREATE UNIQUE INDEX reg_44355285_a_txt_pk ON public.gbu_custom_source_27_txt USING btree (id);
	END IF;
END $$;

/*reg_44355285_a_txt_inx_obj_attr_id 36*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_27_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_27_txt') THEN
			ALTER TABLE gbu_custom_source_27_txt DROP CONSTRAINT IF EXISTS reg_44355285_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355285_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_44355285_a_txt_inx_obj_attr_id ON public.gbu_custom_source_27_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_41937379_a_num_pk 37*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_22_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_22_num') THEN
			ALTER TABLE gbu_custom_source_22_num DROP CONSTRAINT IF EXISTS reg_41937379_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41937379_a_num_pk;
		CREATE UNIQUE INDEX reg_41937379_a_num_pk ON public.gbu_custom_source_22_num USING btree (id);
	END IF;
END $$;

/*reg_41937379_a_num_inx_obj_attr_id 38*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_22_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_22_num') THEN
			ALTER TABLE gbu_custom_source_22_num DROP CONSTRAINT IF EXISTS reg_41937379_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41937379_a_num_inx_obj_attr_id;
		CREATE INDEX reg_41937379_a_num_inx_obj_attr_id ON public.gbu_custom_source_22_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_29180374_q_pk 39*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors29151974') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors29151974') THEN
			ALTER TABLE ko_touroksfactors29151974 DROP CONSTRAINT IF EXISTS reg_29180374_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29180374_q_pk;
		CREATE UNIQUE INDEX reg_29180374_q_pk ON public.ko_touroksfactors29151974 USING btree (id);
	END IF;
END $$;

/*reg_29871819_q_pk 40*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors29839891') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors29839891') THEN
			ALTER TABLE ko_touroksfactors29839891 DROP CONSTRAINT IF EXISTS reg_29871819_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29871819_q_pk;
		CREATE UNIQUE INDEX reg_29871819_q_pk ON public.ko_touroksfactors29839891 USING btree (id);
	END IF;
END $$;

/*reg_39959475_q_pk 41*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_23_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_23_q') THEN
			ALTER TABLE source_23_q DROP CONSTRAINT IF EXISTS reg_39959475_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_39959475_q_pk;
		CREATE UNIQUE INDEX reg_39959475_q_pk ON public.source_23_q USING btree (id);
	END IF;
END $$;

/*reg_42726724_a_dt_pk 42*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_26_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_26_dt') THEN
			ALTER TABLE gbu_custom_source_26_dt DROP CONSTRAINT IF EXISTS reg_42726724_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42726724_a_dt_pk;
		CREATE UNIQUE INDEX reg_42726724_a_dt_pk ON public.gbu_custom_source_26_dt USING btree (id);
	END IF;
END $$;

/*reg_42726724_a_dt_inx_obj_attr_id 43*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_26_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_26_dt') THEN
			ALTER TABLE gbu_custom_source_26_dt DROP CONSTRAINT IF EXISTS reg_42726724_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42726724_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_42726724_a_dt_inx_obj_attr_id ON public.gbu_custom_source_26_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_44355285_a_num_pk 44*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_27_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_27_num') THEN
			ALTER TABLE gbu_custom_source_27_num DROP CONSTRAINT IF EXISTS reg_44355285_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355285_a_num_pk;
		CREATE UNIQUE INDEX reg_44355285_a_num_pk ON public.gbu_custom_source_27_num USING btree (id);
	END IF;
END $$;

/*reg_44355285_a_num_inx_obj_attr_id 45*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_27_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_27_num') THEN
			ALTER TABLE gbu_custom_source_27_num DROP CONSTRAINT IF EXISTS reg_44355285_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355285_a_num_inx_obj_attr_id;
		CREATE INDEX reg_44355285_a_num_inx_obj_attr_id ON public.gbu_custom_source_27_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_44355285_a_dt_pk 46*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_27_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_27_dt') THEN
			ALTER TABLE gbu_custom_source_27_dt DROP CONSTRAINT IF EXISTS reg_44355285_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355285_a_dt_pk;
		CREATE UNIQUE INDEX reg_44355285_a_dt_pk ON public.gbu_custom_source_27_dt USING btree (id);
	END IF;
END $$;

/*reg_44355285_a_dt_inx_obj_attr_id 47*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_27_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_27_dt') THEN
			ALTER TABLE gbu_custom_source_27_dt DROP CONSTRAINT IF EXISTS reg_44355285_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355285_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_44355285_a_dt_inx_obj_attr_id ON public.gbu_custom_source_27_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_3_a_dt_pk 48*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source3_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source3_a_dt') THEN
			ALTER TABLE gbu_source3_a_dt DROP CONSTRAINT IF EXISTS reg_3_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_3_a_dt_pk;
		CREATE UNIQUE INDEX reg_3_a_dt_pk ON public.gbu_source3_a_dt USING btree (id);
	END IF;
END $$;

/*reg_3_a_dt_inx_obj_attr_id 49*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source3_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source3_a_dt') THEN
			ALTER TABLE gbu_source3_a_dt DROP CONSTRAINT IF EXISTS reg_3_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_3_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_3_a_dt_inx_obj_attr_id ON public.gbu_source3_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_37302129_q_pk 50*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors37294645') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors37294645') THEN
			ALTER TABLE ko_tourzufactors37294645 DROP CONSTRAINT IF EXISTS reg_37302129_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_37302129_q_pk;
		CREATE UNIQUE INDEX reg_37302129_q_pk ON public.ko_tourzufactors37294645 USING btree (id);
	END IF;
END $$;

/*reg_3_a_num_pk 51*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source3_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source3_a_num') THEN
			ALTER TABLE gbu_source3_a_num DROP CONSTRAINT IF EXISTS reg_3_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_3_a_num_pk;
		CREATE UNIQUE INDEX reg_3_a_num_pk ON public.gbu_source3_a_num USING btree (id);
	END IF;
END $$;

/*reg_3_a_num_inx_obj_attr_id 52*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source3_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source3_a_num') THEN
			ALTER TABLE gbu_source3_a_num DROP CONSTRAINT IF EXISTS reg_3_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_3_a_num_inx_obj_attr_id;
		CREATE INDEX reg_3_a_num_inx_obj_attr_id ON public.gbu_source3_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_256_q_pk 53*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_change') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_change') THEN
			ALTER TABLE ko_unit_change DROP CONSTRAINT IF EXISTS reg_256_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_256_q_pk;
		alter table KO_UNIT_CHANGE add constraint reg_256_q_pk primary key (id);
	END IF;
END $$;

/*reg_7_a_dt_pk 54*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source7_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source7_a_dt') THEN
			ALTER TABLE gbu_source7_a_dt DROP CONSTRAINT IF EXISTS reg_7_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_7_a_dt_pk;
		CREATE UNIQUE INDEX reg_7_a_dt_pk ON public.gbu_source7_a_dt USING btree (id);
	END IF;
END $$;

/*reg_7_a_dt_inx_obj_attr_id 55*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source7_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source7_a_dt') THEN
			ALTER TABLE gbu_source7_a_dt DROP CONSTRAINT IF EXISTS reg_7_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_7_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_7_a_dt_inx_obj_attr_id ON public.gbu_source7_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_23_a_num_pk 56*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source23_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source23_a_num') THEN
			ALTER TABLE gbu_source23_a_num DROP CONSTRAINT IF EXISTS reg_23_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_23_a_num_pk;
		CREATE UNIQUE INDEX reg_23_a_num_pk ON public.gbu_source23_a_num USING btree (id);
	END IF;
END $$;

/*reg_23_a_num_inx_obj_attr_id 57*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source23_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source23_a_num') THEN
			ALTER TABLE gbu_source23_a_num DROP CONSTRAINT IF EXISTS reg_23_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_23_a_num_inx_obj_attr_id;
		CREATE INDEX reg_23_a_num_inx_obj_attr_id ON public.gbu_source23_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_9_a_num_pk 58*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source9_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source9_a_num') THEN
			ALTER TABLE gbu_source9_a_num DROP CONSTRAINT IF EXISTS reg_9_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_9_a_num_pk;
		CREATE UNIQUE INDEX reg_9_a_num_pk ON public.gbu_source9_a_num USING btree (id);
	END IF;
END $$;

/*reg_9_a_num_inx_obj_attr_id 59*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source9_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source9_a_num') THEN
			ALTER TABLE gbu_source9_a_num DROP CONSTRAINT IF EXISTS reg_9_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_9_a_num_inx_obj_attr_id;
		CREATE INDEX reg_9_a_num_inx_obj_attr_id ON public.gbu_source9_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_9_a_txt_pk 60*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source9_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source9_a_txt') THEN
			ALTER TABLE gbu_source9_a_txt DROP CONSTRAINT IF EXISTS reg_9_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_9_a_txt_pk;
		CREATE UNIQUE INDEX reg_9_a_txt_pk ON public.gbu_source9_a_txt USING btree (id);
	END IF;
END $$;

/*reg_9_a_txt_inx_obj_attr_id 61*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source9_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source9_a_txt') THEN
			ALTER TABLE gbu_source9_a_txt DROP CONSTRAINT IF EXISTS reg_9_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_9_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_9_a_txt_inx_obj_attr_id ON public.gbu_source9_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_208_q_pk 62*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_group_factor') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_group_factor') THEN
			ALTER TABLE ko_group_factor DROP CONSTRAINT IF EXISTS reg_208_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_208_q_pk;
		alter table KO_GROUP_FACTOR add constraint reg_208_q_pk primary key (id);
	END IF;
END $$;

/*reg_47578317_a_txt_pk 63*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_35_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_35_txt') THEN
			ALTER TABLE gbu_custom_source_35_txt DROP CONSTRAINT IF EXISTS reg_47578317_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47578317_a_txt_pk;
		CREATE UNIQUE INDEX reg_47578317_a_txt_pk ON public.gbu_custom_source_35_txt USING btree (id);
	END IF;
END $$;

/*reg_47578317_a_txt_inx_obj_attr_id 64*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_35_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_35_txt') THEN
			ALTER TABLE gbu_custom_source_35_txt DROP CONSTRAINT IF EXISTS reg_47578317_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47578317_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_47578317_a_txt_inx_obj_attr_id ON public.gbu_custom_source_35_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*pk_outer_core_object_uid 65*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_core_object_old') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_core_object_old') THEN
			ALTER TABLE market_core_object_old DROP CONSTRAINT IF EXISTS pk_outer_core_object_uid RESTRICT;
		END IF;
		DROP INDEX IF EXISTS pk_outer_core_object_uid;
		CREATE UNIQUE INDEX pk_outer_core_object_uid ON public.market_core_object_old USING btree (id);
	END IF;
END $$;

/*reg_28_a_num_pk 66*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source28_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source28_a_num') THEN
			ALTER TABLE gbu_source28_a_num DROP CONSTRAINT IF EXISTS reg_28_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_28_a_num_pk;
		CREATE UNIQUE INDEX reg_28_a_num_pk ON public.gbu_source28_a_num USING btree (id);
	END IF;
END $$;

/*reg_28_a_num_inx_obj_attr_id 67*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source28_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source28_a_num') THEN
			ALTER TABLE gbu_source28_a_num DROP CONSTRAINT IF EXISTS reg_28_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_28_a_num_inx_obj_attr_id;
		CREATE INDEX reg_28_a_num_inx_obj_attr_id ON public.gbu_source28_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_7_a_num_pk 68*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source7_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source7_a_num') THEN
			ALTER TABLE gbu_source7_a_num DROP CONSTRAINT IF EXISTS reg_7_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_7_a_num_pk;
		CREATE UNIQUE INDEX reg_7_a_num_pk ON public.gbu_source7_a_num USING btree (id);
	END IF;
END $$;

/*reg_7_a_num_inx_obj_attr_id 69*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source7_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source7_a_num') THEN
			ALTER TABLE gbu_source7_a_num DROP CONSTRAINT IF EXISTS reg_7_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_7_a_num_inx_obj_attr_id;
		CREATE INDEX reg_7_a_num_inx_obj_attr_id ON public.gbu_source7_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_302_q_pk 70*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_zaklink') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_zaklink') THEN
			ALTER TABLE sud_zaklink DROP CONSTRAINT IF EXISTS reg_302_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_302_q_pk;
		alter table SUD_ZAKLINK add constraint reg_302_q_pk primary key (id);
	END IF;
END $$;

/*sud_zaklink_obj_zak_idx 71*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_zaklink') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_zaklink') THEN
			ALTER TABLE sud_zaklink DROP CONSTRAINT IF EXISTS sud_zaklink_obj_zak_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_zaklink_obj_zak_idx;
		CREATE INDEX sud_zaklink_obj_zak_idx ON public.sud_zaklink USING btree (id_object, id_zak);
	END IF;
END $$;

/*reg_800_q_pk 72*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_export_by_templates') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_export_by_templates') THEN
			ALTER TABLE common_export_by_templates DROP CONSTRAINT IF EXISTS reg_800_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_800_q_pk;
		alter table COMMON_EXPORT_BY_TEMPLATES add constraint reg_800_q_pk primary key (id);
	END IF;
END $$;

/*reg_203_q_pk 73*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_task') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_task') THEN
			ALTER TABLE ko_task DROP CONSTRAINT IF EXISTS reg_203_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_203_q_pk;
		alter table KO_TASK add constraint reg_203_q_pk primary key (id);
	END IF;
END $$;

/*ko_task_creation_date_idx 74*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_task') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_task') THEN
			ALTER TABLE ko_task DROP CONSTRAINT IF EXISTS ko_task_creation_date_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_task_creation_date_idx;
		CREATE INDEX ko_task_creation_date_idx ON public.ko_task USING btree (creation_date);
	END IF;
END $$;

/*system_daily_statistics_pk 75*/
DO $$
BEGIN
	IF (SELECT to_regclass('system_daily_statistics') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='system_daily_statistics') THEN
			ALTER TABLE system_daily_statistics DROP CONSTRAINT IF EXISTS system_daily_statistics_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS system_daily_statistics_pk;
		CREATE UNIQUE INDEX system_daily_statistics_pk ON public.system_daily_statistics USING btree (id);
	END IF;
END $$;

/*system_daily_stat_date_uk 76*/
DO $$
BEGIN
	IF (SELECT to_regclass('system_daily_statistics') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='system_daily_statistics') THEN
			ALTER TABLE system_daily_statistics DROP CONSTRAINT IF EXISTS system_daily_stat_date_uk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS system_daily_stat_date_uk;
		CREATE UNIQUE INDEX system_daily_stat_date_uk ON public.system_daily_statistics USING btree (stat_date);
	END IF;
END $$;

/*reg_44355304_a_txt_pk 77*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_29_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_29_txt') THEN
			ALTER TABLE gbu_custom_source_29_txt DROP CONSTRAINT IF EXISTS reg_44355304_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355304_a_txt_pk;
		CREATE UNIQUE INDEX reg_44355304_a_txt_pk ON public.gbu_custom_source_29_txt USING btree (id);
	END IF;
END $$;

/*reg_44355304_a_txt_inx_obj_attr_id 78*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_29_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_29_txt') THEN
			ALTER TABLE gbu_custom_source_29_txt DROP CONSTRAINT IF EXISTS reg_44355304_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355304_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_44355304_a_txt_inx_obj_attr_id ON public.gbu_custom_source_29_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*pk_outer_properties_uid 79*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_properties') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_properties') THEN
			ALTER TABLE market_properties DROP CONSTRAINT IF EXISTS pk_outer_properties_uid RESTRICT;
		END IF;
		DROP INDEX IF EXISTS pk_outer_properties_uid;
		alter table market_properties add constraint pk_outer_properties_uid primary key (uid);
	END IF;
END $$;

/*reg_54578488_a_num_pk 80*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_43_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_43_num') THEN
			ALTER TABLE gbu_custom_source_43_num DROP CONSTRAINT IF EXISTS reg_54578488_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578488_a_num_pk;
		CREATE UNIQUE INDEX reg_54578488_a_num_pk ON public.gbu_custom_source_43_num USING btree (id);
	END IF;
END $$;

/*reg_54578488_a_num_inx_obj_attr_id 81*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_43_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_43_num') THEN
			ALTER TABLE gbu_custom_source_43_num DROP CONSTRAINT IF EXISTS reg_54578488_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578488_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578488_a_num_inx_obj_attr_id ON public.gbu_custom_source_43_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*data_composition_by_characteristics_tmp_idx 82*/
DO $$
BEGIN
	IF (SELECT to_regclass('data_composition_by_characteristics_tmp') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='data_composition_by_characteristics_tmp') THEN
			ALTER TABLE data_composition_by_characteristics_tmp DROP CONSTRAINT IF EXISTS data_composition_by_characteristics_tmp_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS data_composition_by_characteristics_tmp_idx;
		CREATE INDEX data_composition_by_characteristics_tmp_idx ON public.data_composition_by_characteristics_tmp USING btree (object_id);
	END IF;
END $$;

/*reg_47578317_a_num_pk 83*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_35_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_35_num') THEN
			ALTER TABLE gbu_custom_source_35_num DROP CONSTRAINT IF EXISTS reg_47578317_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47578317_a_num_pk;
		CREATE UNIQUE INDEX reg_47578317_a_num_pk ON public.gbu_custom_source_35_num USING btree (id);
	END IF;
END $$;

/*reg_47578317_a_num_inx_obj_attr_id 84*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_35_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_35_num') THEN
			ALTER TABLE gbu_custom_source_35_num DROP CONSTRAINT IF EXISTS reg_47578317_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47578317_a_num_inx_obj_attr_id;
		CREATE INDEX reg_47578317_a_num_inx_obj_attr_id ON public.gbu_custom_source_35_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_214_q_pk 85*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_cod_dictionary') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_cod_dictionary') THEN
			ALTER TABLE ko_cod_dictionary DROP CONSTRAINT IF EXISTS reg_214_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_214_q_pk;
		alter table KO_COD_DICTIONARY add constraint reg_214_q_pk primary key (id);
	END IF;
END $$;

/*market_core_object_pkey 86*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_core_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_core_object') THEN
			ALTER TABLE market_core_object DROP CONSTRAINT IF EXISTS market_core_object_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS market_core_object_pkey;
		alter table market_core_object add constraint market_core_object_pkey primary key (id);
	END IF;
END $$;

/*cadastral_number_index 87*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_core_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_core_object') THEN
			ALTER TABLE market_core_object DROP CONSTRAINT IF EXISTS cadastral_number_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS cadastral_number_index;
		CREATE INDEX cadastral_number_index ON public.market_core_object USING btree (cadastral_number);
	END IF;
END $$;

/*deal_type_code_index 88*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_core_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_core_object') THEN
			ALTER TABLE market_core_object DROP CONSTRAINT IF EXISTS deal_type_code_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deal_type_code_index;
		CREATE INDEX deal_type_code_index ON public.market_core_object USING btree (deal_type_code);
	END IF;
END $$;

/*url_index 89*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_core_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_core_object') THEN
			ALTER TABLE market_core_object DROP CONSTRAINT IF EXISTS url_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS url_index;
		CREATE INDEX url_index ON public.market_core_object USING btree (url);
	END IF;
END $$;

/*parser_time_index 90*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_core_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_core_object') THEN
			ALTER TABLE market_core_object DROP CONSTRAINT IF EXISTS parser_time_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS parser_time_index;
		CREATE INDEX parser_time_index ON public.market_core_object USING brin (parser_time);
	END IF;
END $$;

/*reg_253_q_pk 91*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_params_zu_2016') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_params_zu_2016') THEN
			ALTER TABLE ko_unit_params_zu_2016 DROP CONSTRAINT IF EXISTS reg_253_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_253_q_pk;
		alter table KO_UNIT_PARAMS_ZU_2016 add constraint reg_253_q_pk primary key (id);
	END IF;
END $$;

/*reg_119_q_pk 92*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_outliers_checking_history') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_outliers_checking_history') THEN
			ALTER TABLE market_outliers_checking_history DROP CONSTRAINT IF EXISTS reg_119_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_119_q_pk;
		alter table market_outliers_checking_history add constraint reg_119_q_pk primary key (id);
	END IF;
END $$;

/*reg_44355304_a_num_pk 93*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_29_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_29_num') THEN
			ALTER TABLE gbu_custom_source_29_num DROP CONSTRAINT IF EXISTS reg_44355304_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355304_a_num_pk;
		CREATE UNIQUE INDEX reg_44355304_a_num_pk ON public.gbu_custom_source_29_num USING btree (id);
	END IF;
END $$;

/*reg_44355304_a_num_inx_obj_attr_id 94*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_29_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_29_num') THEN
			ALTER TABLE gbu_custom_source_29_num DROP CONSTRAINT IF EXISTS reg_44355304_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355304_a_num_inx_obj_attr_id;
		CREATE INDEX reg_44355304_a_num_inx_obj_attr_id ON public.gbu_custom_source_29_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*pk_market_settings_id 95*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_settings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_settings') THEN
			ALTER TABLE market_settings DROP CONSTRAINT IF EXISTS pk_market_settings_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS pk_market_settings_id;
		alter table market_settings add constraint pk_market_settings_id primary key (id);
	END IF;
END $$;

/*pk_outer_avito_object_id 96*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_avito_object_old') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_avito_object_old') THEN
			ALTER TABLE market_avito_object_old DROP CONSTRAINT IF EXISTS pk_outer_avito_object_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS pk_outer_avito_object_id;
		alter table market_avito_object_old add constraint pk_outer_avito_object_id primary key (id);
	END IF;
END $$;

/*reg_10_a_txt_pk 97*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source10_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source10_a_txt') THEN
			ALTER TABLE gbu_source10_a_txt DROP CONSTRAINT IF EXISTS reg_10_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_10_a_txt_pk;
		CREATE UNIQUE INDEX reg_10_a_txt_pk ON public.gbu_source10_a_txt USING btree (id);
	END IF;
END $$;

/*reg_10_a_txt_inx_obj_attr_id 98*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source10_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source10_a_txt') THEN
			ALTER TABLE gbu_source10_a_txt DROP CONSTRAINT IF EXISTS reg_10_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_10_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_10_a_txt_inx_obj_attr_id ON public.gbu_source10_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_303_q_pk 99*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_drs') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_drs') THEN
			ALTER TABLE sud_drs DROP CONSTRAINT IF EXISTS reg_303_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_303_q_pk;
		alter table SUD_DRS add constraint reg_303_q_pk primary key (id_object);
	END IF;
END $$;

/*pk_system_daily_stat_file_stor 100*/
DO $$
BEGIN
	IF (SELECT to_regclass('system_daily_stat_file_stor') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='system_daily_stat_file_stor') THEN
			ALTER TABLE system_daily_stat_file_stor DROP CONSTRAINT IF EXISTS pk_system_daily_stat_file_stor RESTRICT;
		END IF;
		DROP INDEX IF EXISTS pk_system_daily_stat_file_stor;
		CREATE UNIQUE INDEX pk_system_daily_stat_file_stor ON public.system_daily_stat_file_stor USING btree (id);
	END IF;
END $$;

/*reg_307_q_pk 101*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_otchetstatus') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_otchetstatus') THEN
			ALTER TABLE sud_otchetstatus DROP CONSTRAINT IF EXISTS reg_307_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_307_q_pk;
		alter table SUD_OTCHETSTATUS add constraint reg_307_q_pk primary key (id);
	END IF;
END $$;

/*reg_301_q_pk 102*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_log') THEN
			ALTER TABLE sud_log DROP CONSTRAINT IF EXISTS reg_301_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_301_q_pk;
		alter table SUD_LOG add constraint reg_301_q_pk primary key (id);
	END IF;
END $$;

/*reg_306_q_pk 103*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_otchetlinkstatus') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_otchetlinkstatus') THEN
			ALTER TABLE sud_otchetlinkstatus DROP CONSTRAINT IF EXISTS reg_306_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_306_q_pk;
		alter table SUD_OTCHETLINKSTATUS add constraint reg_306_q_pk primary key (id);
	END IF;
END $$;

/*sud_otchetlinkstatus_obj_idx 104*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_otchetlinkstatus') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_otchetlinkstatus') THEN
			ALTER TABLE sud_otchetlinkstatus DROP CONSTRAINT IF EXISTS sud_otchetlinkstatus_obj_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_otchetlinkstatus_obj_idx;
		CREATE INDEX sud_otchetlinkstatus_obj_idx ON public.sud_otchetlinkstatus USING btree (id_object);
	END IF;
END $$;

/*reg_40116957_q_pk 105*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_28_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_28_q') THEN
			ALTER TABLE source_28_q DROP CONSTRAINT IF EXISTS reg_40116957_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_40116957_q_pk;
		CREATE UNIQUE INDEX reg_40116957_q_pk ON public.source_28_q USING btree (id);
	END IF;
END $$;

/*reg_23_a_txt_pk 106*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source23_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source23_a_txt') THEN
			ALTER TABLE gbu_source23_a_txt DROP CONSTRAINT IF EXISTS reg_23_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_23_a_txt_pk;
		CREATE UNIQUE INDEX reg_23_a_txt_pk ON public.gbu_source23_a_txt USING btree (id);
	END IF;
END $$;

/*reg_23_a_txt_inx_obj_attr_id 107*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source23_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source23_a_txt') THEN
			ALTER TABLE gbu_source23_a_txt DROP CONSTRAINT IF EXISTS reg_23_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_23_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_23_a_txt_inx_obj_attr_id ON public.gbu_source23_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_27407381_q_pk 108*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors12506547') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors12506547') THEN
			ALTER TABLE ko_touroksfactors12506547 DROP CONSTRAINT IF EXISTS reg_27407381_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_27407381_q_pk;
		CREATE UNIQUE INDEX reg_27407381_q_pk ON public.ko_touroksfactors12506547 USING btree (id);
	END IF;
END $$;

/*reg_309_q_pk 109*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_sudlinkstatus') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_sudlinkstatus') THEN
			ALTER TABLE sud_sudlinkstatus DROP CONSTRAINT IF EXISTS reg_309_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_309_q_pk;
		alter table SUD_SUDLINKSTATUS add constraint reg_309_q_pk primary key (id);
	END IF;
END $$;

/*reg_28_a_dt_pk 110*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source28_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source28_a_dt') THEN
			ALTER TABLE gbu_source28_a_dt DROP CONSTRAINT IF EXISTS reg_28_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_28_a_dt_pk;
		CREATE UNIQUE INDEX reg_28_a_dt_pk ON public.gbu_source28_a_dt USING btree (id);
	END IF;
END $$;

/*reg_28_a_dt_inx_obj_attr_id 111*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source28_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source28_a_dt') THEN
			ALTER TABLE gbu_source28_a_dt DROP CONSTRAINT IF EXISTS reg_28_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_28_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_28_a_dt_inx_obj_attr_id ON public.gbu_source28_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*id 112*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_factor_settings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_factor_settings') THEN
			ALTER TABLE ko_factor_settings DROP CONSTRAINT IF EXISTS id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS id;
		alter table KO_FACTOR_SETTINGS add constraint id primary key (id);
	END IF;
END $$;

/*reg_44355304_a_dt_pk 113*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_29_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_29_dt') THEN
			ALTER TABLE gbu_custom_source_29_dt DROP CONSTRAINT IF EXISTS reg_44355304_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355304_a_dt_pk;
		CREATE UNIQUE INDEX reg_44355304_a_dt_pk ON public.gbu_custom_source_29_dt USING btree (id);
	END IF;
END $$;

/*reg_44355304_a_dt_inx_obj_attr_id 114*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_29_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_29_dt') THEN
			ALTER TABLE gbu_custom_source_29_dt DROP CONSTRAINT IF EXISTS reg_44355304_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44355304_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_44355304_a_dt_inx_obj_attr_id ON public.gbu_custom_source_29_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_39964619_q_pk 115*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_24_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_24_q') THEN
			ALTER TABLE source_24_q DROP CONSTRAINT IF EXISTS reg_39964619_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_39964619_q_pk;
		CREATE UNIQUE INDEX reg_39964619_q_pk ON public.source_24_q USING btree (id);
	END IF;
END $$;

/*"DASHBOARDINDEXCARDCACHE_KEY_PK" 116*/
DO $$
BEGIN
	IF (SELECT to_regclass('dashboards_indexcardcache') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='dashboards_indexcardcache') THEN
			ALTER TABLE dashboards_indexcardcache DROP CONSTRAINT IF EXISTS "DASHBOARDINDEXCARDCACHE_KEY_PK" RESTRICT;
		END IF;
		DROP INDEX IF EXISTS "DASHBOARDINDEXCARDCACHE_KEY_PK";
		CREATE UNIQUE INDEX "DASHBOARDINDEXCARDCACHE_KEY_PK" ON public.dashboards_indexcardcache USING btree (key);
	END IF;
END $$;

/*reg_310_q_pk 117*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_sudstatus') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_sudstatus') THEN
			ALTER TABLE sud_sudstatus DROP CONSTRAINT IF EXISTS reg_310_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_310_q_pk;
		alter table SUD_SUDSTATUS add constraint reg_310_q_pk primary key (id);
	END IF;
END $$;

/*reg_991_quant_pk 118*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_register_state') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_register_state') THEN
			ALTER TABLE core_register_state DROP CONSTRAINT IF EXISTS reg_991_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_991_quant_pk;
		alter table CORE_REGISTER_STATE add constraint reg_991_quant_pk primary key (id);
	END IF;
END $$;

/*reg_41619157_q_pk 119*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_30_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_30_q') THEN
			ALTER TABLE source_30_q DROP CONSTRAINT IF EXISTS reg_41619157_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41619157_q_pk;
		CREATE UNIQUE INDEX reg_41619157_q_pk ON public.source_30_q USING btree (id);
	END IF;
END $$;

/*reg_28928795_q_pk 120*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors28859124') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors28859124') THEN
			ALTER TABLE ko_tourzufactors28859124 DROP CONSTRAINT IF EXISTS reg_28928795_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_28928795_q_pk;
		CREATE UNIQUE INDEX reg_28928795_q_pk ON public.ko_tourzufactors28859124 USING btree (id);
	END IF;
END $$;

/*reg_961_quant_pk 121*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_template_version') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_template_version') THEN
			ALTER TABLE core_td_template_version DROP CONSTRAINT IF EXISTS reg_961_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_961_quant_pk;
		alter table CORE_TD_TEMPLATE_VERSION add constraint reg_961_quant_pk primary key (id);
	END IF;
END $$;

/*reg_260_q_pk 122*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_auto_calculation_settings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_auto_calculation_settings') THEN
			ALTER TABLE ko_auto_calculation_settings DROP CONSTRAINT IF EXISTS reg_260_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_260_q_pk;
		alter table KO_AUTO_CALCULATION_SETTINGS add constraint reg_260_q_pk primary key (id);
	END IF;
END $$;

/*reg_47578317_a_dt_pk 123*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_35_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_35_dt') THEN
			ALTER TABLE gbu_custom_source_35_dt DROP CONSTRAINT IF EXISTS reg_47578317_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47578317_a_dt_pk;
		CREATE UNIQUE INDEX reg_47578317_a_dt_pk ON public.gbu_custom_source_35_dt USING btree (id);
	END IF;
END $$;

/*reg_47578317_a_dt_inx_obj_attr_id 124*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_35_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_35_dt') THEN
			ALTER TABLE gbu_custom_source_35_dt DROP CONSTRAINT IF EXISTS reg_47578317_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47578317_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_47578317_a_dt_inx_obj_attr_id ON public.gbu_custom_source_35_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_41983898_a_txt_pk 125*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_23_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_23_txt') THEN
			ALTER TABLE gbu_custom_source_23_txt DROP CONSTRAINT IF EXISTS reg_41983898_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41983898_a_txt_pk;
		CREATE UNIQUE INDEX reg_41983898_a_txt_pk ON public.gbu_custom_source_23_txt USING btree (id);
	END IF;
END $$;

/*reg_41983898_a_txt_inx_obj_attr_id 126*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_23_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_23_txt') THEN
			ALTER TABLE gbu_custom_source_23_txt DROP CONSTRAINT IF EXISTS reg_41983898_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41983898_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_41983898_a_txt_inx_obj_attr_id ON public.gbu_custom_source_23_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_46717425_a_txt_pk 127*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_30_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_30_txt') THEN
			ALTER TABLE gbu_custom_source_30_txt DROP CONSTRAINT IF EXISTS reg_46717425_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46717425_a_txt_pk;
		CREATE UNIQUE INDEX reg_46717425_a_txt_pk ON public.gbu_custom_source_30_txt USING btree (id);
	END IF;
END $$;

/*reg_46717425_a_txt_inx_obj_attr_id 128*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_30_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_30_txt') THEN
			ALTER TABLE gbu_custom_source_30_txt DROP CONSTRAINT IF EXISTS reg_46717425_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46717425_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_46717425_a_txt_inx_obj_attr_id ON public.gbu_custom_source_30_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_600_q_pk 129*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_objects_characteristics_register') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_objects_characteristics_register') THEN
			ALTER TABLE ko_objects_characteristics_register DROP CONSTRAINT IF EXISTS reg_600_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_600_q_pk;
		alter table ko_objects_characteristics_register add constraint reg_600_q_pk primary key (id);
	END IF;
END $$;

/*reg_41935245_q_pk 130*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_34_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_34_q') THEN
			ALTER TABLE source_34_q DROP CONSTRAINT IF EXISTS reg_41935245_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41935245_q_pk;
		CREATE UNIQUE INDEX reg_41935245_q_pk ON public.source_34_q USING btree (id);
	END IF;
END $$;

/*reg_22907629_q_pk 131*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors22907439') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors22907439') THEN
			ALTER TABLE ko_tourzufactors22907439 DROP CONSTRAINT IF EXISTS reg_22907629_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_22907629_q_pk;
		CREATE UNIQUE INDEX reg_22907629_q_pk ON public.ko_tourzufactors22907439 USING btree (id);
	END IF;
END $$;

/*reg_931_quant_pk 132*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_register_attribute') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_register_attribute') THEN
			ALTER TABLE core_register_attribute DROP CONSTRAINT IF EXISTS reg_931_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_931_quant_pk;
		alter table CORE_REGISTER_ATTRIBUTE add constraint reg_931_quant_pk primary key (id);
	END IF;
END $$;

/*core_register_attribute_unique_name_constraint 133*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_register_attribute') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_register_attribute') THEN
			ALTER TABLE core_register_attribute DROP CONSTRAINT IF EXISTS core_register_attribute_unique_name_constraint RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_register_attribute_unique_name_constraint;
		CREATE UNIQUE INDEX core_register_attribute_unique_name_constraint ON public.core_register_attribute USING btree (btrim(lower((name)::text)), registerid) WHERE ((is_deleted <> 1) OR (is_deleted IS NULL));
	END IF;
END $$;

/*reg_47496917_q_pk 134*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors12506549') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors12506549') THEN
			ALTER TABLE ko_touroksfactors12506549 DROP CONSTRAINT IF EXISTS reg_47496917_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47496917_q_pk;
		CREATE UNIQUE INDEX reg_47496917_q_pk ON public.ko_touroksfactors12506549 USING btree (id);
	END IF;
END $$;

/*reg_222_q_pk 135*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_group_to_market_segment_relation') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_group_to_market_segment_relation') THEN
			ALTER TABLE ko_group_to_market_segment_relation DROP CONSTRAINT IF EXISTS reg_222_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_222_q_pk;
		alter table KO_GROUP_TO_MARKET_SEGMENT_RELATION add constraint reg_222_q_pk primary key (id);
	END IF;
END $$;

/*ko_group_to_market_segment_relation_idx 136*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_group_to_market_segment_relation') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_group_to_market_segment_relation') THEN
			ALTER TABLE ko_group_to_market_segment_relation DROP CONSTRAINT IF EXISTS ko_group_to_market_segment_relation_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_group_to_market_segment_relation_idx;
		CREATE UNIQUE INDEX ko_group_to_market_segment_relation_idx ON public.ko_group_to_market_segment_relation USING btree (group_id);
	END IF;
END $$;

/*reg_46717425_a_num_pk 137*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_30_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_30_num') THEN
			ALTER TABLE gbu_custom_source_30_num DROP CONSTRAINT IF EXISTS reg_46717425_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46717425_a_num_pk;
		CREATE UNIQUE INDEX reg_46717425_a_num_pk ON public.gbu_custom_source_30_num USING btree (id);
	END IF;
END $$;

/*reg_46717425_a_num_inx_obj_attr_id 138*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_30_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_30_num') THEN
			ALTER TABLE gbu_custom_source_30_num DROP CONSTRAINT IF EXISTS reg_46717425_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46717425_a_num_inx_obj_attr_id;
		CREATE INDEX reg_46717425_a_num_inx_obj_attr_id ON public.gbu_custom_source_30_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_998_quant_pk 139*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_holidays') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_holidays') THEN
			ALTER TABLE core_holidays DROP CONSTRAINT IF EXISTS reg_998_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_998_quant_pk;
		alter table CORE_HOLIDAYS add constraint reg_998_quant_pk primary key (id);
	END IF;
END $$;

/*reg_46717425_a_dt_pk 140*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_30_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_30_dt') THEN
			ALTER TABLE gbu_custom_source_30_dt DROP CONSTRAINT IF EXISTS reg_46717425_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46717425_a_dt_pk;
		CREATE UNIQUE INDEX reg_46717425_a_dt_pk ON public.gbu_custom_source_30_dt USING btree (id);
	END IF;
END $$;

/*reg_46717425_a_dt_inx_obj_attr_id 141*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_30_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_30_dt') THEN
			ALTER TABLE gbu_custom_source_30_dt DROP CONSTRAINT IF EXISTS reg_46717425_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46717425_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_46717425_a_dt_inx_obj_attr_id ON public.gbu_custom_source_30_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_933_quant_pk 142*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_layout') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_layout') THEN
			ALTER TABLE core_layout DROP CONSTRAINT IF EXISTS reg_933_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_933_quant_pk;
		alter table CORE_LAYOUT add constraint reg_933_quant_pk primary key (layoutid);
	END IF;
END $$;

/*reg_924_quant_pk 143*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_layout_column_type') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_layout_column_type') THEN
			ALTER TABLE core_layout_column_type DROP CONSTRAINT IF EXISTS reg_924_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_924_quant_pk;
		alter table CORE_LAYOUT_COLUMN_TYPE add constraint reg_924_quant_pk primary key (id);
	END IF;
END $$;

/*reg_3_a_txt_pk 144*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source3_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source3_a_txt') THEN
			ALTER TABLE gbu_source3_a_txt DROP CONSTRAINT IF EXISTS reg_3_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_3_a_txt_pk;
		CREATE UNIQUE INDEX reg_3_a_txt_pk ON public.gbu_source3_a_txt USING btree (id);
	END IF;
END $$;

/*reg_3_a_txt_inx_obj_attr_id 145*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source3_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source3_a_txt') THEN
			ALTER TABLE gbu_source3_a_txt DROP CONSTRAINT IF EXISTS reg_3_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_3_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_3_a_txt_inx_obj_attr_id ON public.gbu_source3_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_4_a_dt_pk 146*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source4_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source4_a_dt') THEN
			ALTER TABLE gbu_source4_a_dt DROP CONSTRAINT IF EXISTS reg_4_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_4_a_dt_pk;
		CREATE UNIQUE INDEX reg_4_a_dt_pk ON public.gbu_source4_a_dt USING btree (id);
	END IF;
END $$;

/*reg_4_a_dt_inx_obj_attr_id 147*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source4_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source4_a_dt') THEN
			ALTER TABLE gbu_source4_a_dt DROP CONSTRAINT IF EXISTS reg_4_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_4_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_4_a_dt_inx_obj_attr_id ON public.gbu_source4_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_4_a_txt_pk 148*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source4_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source4_a_txt') THEN
			ALTER TABLE gbu_source4_a_txt DROP CONSTRAINT IF EXISTS reg_4_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_4_a_txt_pk;
		CREATE UNIQUE INDEX reg_4_a_txt_pk ON public.gbu_source4_a_txt USING btree (id);
	END IF;
END $$;

/*reg_4_a_txt_inx_obj_attr_id 149*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source4_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source4_a_txt') THEN
			ALTER TABLE gbu_source4_a_txt DROP CONSTRAINT IF EXISTS reg_4_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_4_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_4_a_txt_inx_obj_attr_id ON public.gbu_source4_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_5_a_dt_pk 150*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source5_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source5_a_dt') THEN
			ALTER TABLE gbu_source5_a_dt DROP CONSTRAINT IF EXISTS reg_5_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_5_a_dt_pk;
		CREATE UNIQUE INDEX reg_5_a_dt_pk ON public.gbu_source5_a_dt USING btree (id);
	END IF;
END $$;

/*reg_5_a_dt_inx_obj_attr_id 151*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source5_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source5_a_dt') THEN
			ALTER TABLE gbu_source5_a_dt DROP CONSTRAINT IF EXISTS reg_5_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_5_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_5_a_dt_inx_obj_attr_id ON public.gbu_source5_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_5_a_num_pk 152*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source5_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source5_a_num') THEN
			ALTER TABLE gbu_source5_a_num DROP CONSTRAINT IF EXISTS reg_5_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_5_a_num_pk;
		CREATE UNIQUE INDEX reg_5_a_num_pk ON public.gbu_source5_a_num USING btree (id);
	END IF;
END $$;

/*reg_5_a_num_inx_obj_attr_id 153*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source5_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source5_a_num') THEN
			ALTER TABLE gbu_source5_a_num DROP CONSTRAINT IF EXISTS reg_5_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_5_a_num_inx_obj_attr_id;
		CREATE INDEX reg_5_a_num_inx_obj_attr_id ON public.gbu_source5_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_5_a_txt_pk 154*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source5_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source5_a_txt') THEN
			ALTER TABLE gbu_source5_a_txt DROP CONSTRAINT IF EXISTS reg_5_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_5_a_txt_pk;
		CREATE UNIQUE INDEX reg_5_a_txt_pk ON public.gbu_source5_a_txt USING btree (id);
	END IF;
END $$;

/*reg_5_a_txt_inx_obj_attr_id 155*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source5_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source5_a_txt') THEN
			ALTER TABLE gbu_source5_a_txt DROP CONSTRAINT IF EXISTS reg_5_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_5_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_5_a_txt_inx_obj_attr_id ON public.gbu_source5_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_970_quant_pk 156*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_tp') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_tp') THEN
			ALTER TABLE core_td_tp DROP CONSTRAINT IF EXISTS reg_970_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_970_quant_pk;
		alter table CORE_TD_TP add constraint reg_970_quant_pk primary key (id);
	END IF;
END $$;

/*reg_968_quant_pk 157*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_tree') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_tree') THEN
			ALTER TABLE core_td_tree DROP CONSTRAINT IF EXISTS reg_968_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_968_quant_pk;
		alter table CORE_TD_TREE add constraint reg_968_quant_pk primary key (id);
	END IF;
END $$;

/*reg_28954487_q_pk 158*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors28859124') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors28859124') THEN
			ALTER TABLE ko_touroksfactors28859124 DROP CONSTRAINT IF EXISTS reg_28954487_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_28954487_q_pk;
		CREATE UNIQUE INDEX reg_28954487_q_pk ON public.ko_touroksfactors28859124 USING btree (id);
	END IF;
END $$;

/*reg_29503139_q_pk 159*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors29425150') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors29425150') THEN
			ALTER TABLE ko_tourzufactors29425150 DROP CONSTRAINT IF EXISTS reg_29503139_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29503139_q_pk;
		CREATE UNIQUE INDEX reg_29503139_q_pk ON public.ko_tourzufactors29425150 USING btree (id);
	END IF;
END $$;

/*reg_29431915_q_pk 160*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors17618026') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors17618026') THEN
			ALTER TABLE ko_tourzufactors17618026 DROP CONSTRAINT IF EXISTS reg_29431915_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29431915_q_pk;
		CREATE UNIQUE INDEX reg_29431915_q_pk ON public.ko_tourzufactors17618026 USING btree (id);
	END IF;
END $$;

/*reg_46811155_a_num_pk 161*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_31_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_31_num') THEN
			ALTER TABLE gbu_custom_source_31_num DROP CONSTRAINT IF EXISTS reg_46811155_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811155_a_num_pk;
		CREATE UNIQUE INDEX reg_46811155_a_num_pk ON public.gbu_custom_source_31_num USING btree (id);
	END IF;
END $$;

/*reg_46811155_a_num_inx_obj_attr_id 162*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_31_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_31_num') THEN
			ALTER TABLE gbu_custom_source_31_num DROP CONSTRAINT IF EXISTS reg_46811155_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811155_a_num_inx_obj_attr_id;
		CREATE INDEX reg_46811155_a_num_inx_obj_attr_id ON public.gbu_custom_source_31_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_46811155_a_txt_pk 163*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_31_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_31_txt') THEN
			ALTER TABLE gbu_custom_source_31_txt DROP CONSTRAINT IF EXISTS reg_46811155_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811155_a_txt_pk;
		CREATE UNIQUE INDEX reg_46811155_a_txt_pk ON public.gbu_custom_source_31_txt USING btree (id);
	END IF;
END $$;

/*reg_46811155_a_txt_inx_obj_attr_id 164*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_31_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_31_txt') THEN
			ALTER TABLE gbu_custom_source_31_txt DROP CONSTRAINT IF EXISTS reg_46811155_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811155_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_46811155_a_txt_inx_obj_attr_id ON public.gbu_custom_source_31_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_46811155_a_dt_pk 165*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_31_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_31_dt') THEN
			ALTER TABLE gbu_custom_source_31_dt DROP CONSTRAINT IF EXISTS reg_46811155_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811155_a_dt_pk;
		CREATE UNIQUE INDEX reg_46811155_a_dt_pk ON public.gbu_custom_source_31_dt USING btree (id);
	END IF;
END $$;

/*reg_46811155_a_dt_inx_obj_attr_id 166*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_31_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_31_dt') THEN
			ALTER TABLE gbu_custom_source_31_dt DROP CONSTRAINT IF EXISTS reg_46811155_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811155_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_46811155_a_dt_inx_obj_attr_id ON public.gbu_custom_source_31_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_41707120_q_pk 167*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors41707115') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors41707115') THEN
			ALTER TABLE ko_touroksfactors41707115 DROP CONSTRAINT IF EXISTS reg_41707120_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41707120_q_pk;
		CREATE UNIQUE INDEX reg_41707120_q_pk ON public.ko_touroksfactors41707115 USING btree (id);
	END IF;
END $$;

/*reg_41707140_q_pk 168*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors41707139') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors41707139') THEN
			ALTER TABLE ko_tourzufactors41707139 DROP CONSTRAINT IF EXISTS reg_41707140_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41707140_q_pk;
		CREATE UNIQUE INDEX reg_41707140_q_pk ON public.ko_tourzufactors41707139 USING btree (id);
	END IF;
END $$;

/*reg_29169156_q_pk 169*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors29151974') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors29151974') THEN
			ALTER TABLE ko_tourzufactors29151974 DROP CONSTRAINT IF EXISTS reg_29169156_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29169156_q_pk;
		CREATE UNIQUE INDEX reg_29169156_q_pk ON public.ko_tourzufactors29151974 USING btree (id);
	END IF;
END $$;

/*reg_29434779_q_pk 170*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors17618026') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors17618026') THEN
			ALTER TABLE ko_touroksfactors17618026 DROP CONSTRAINT IF EXISTS reg_29434779_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29434779_q_pk;
		CREATE UNIQUE INDEX reg_29434779_q_pk ON public.ko_touroksfactors17618026 USING btree (id);
	END IF;
END $$;

/*reg_8_a_txt_pk 171*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source8_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source8_a_txt') THEN
			ALTER TABLE gbu_source8_a_txt DROP CONSTRAINT IF EXISTS reg_8_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_8_a_txt_pk;
		CREATE UNIQUE INDEX reg_8_a_txt_pk ON public.gbu_source8_a_txt USING btree (id);
	END IF;
END $$;

/*reg_8_a_txt_inx_obj_attr_id 172*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source8_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source8_a_txt') THEN
			ALTER TABLE gbu_source8_a_txt DROP CONSTRAINT IF EXISTS reg_8_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_8_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_8_a_txt_inx_obj_attr_id ON public.gbu_source8_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_9_a_dt_pk 173*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source9_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source9_a_dt') THEN
			ALTER TABLE gbu_source9_a_dt DROP CONSTRAINT IF EXISTS reg_9_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_9_a_dt_pk;
		CREATE UNIQUE INDEX reg_9_a_dt_pk ON public.gbu_source9_a_dt USING btree (id);
	END IF;
END $$;

/*reg_9_a_dt_inx_obj_attr_id 174*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source9_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source9_a_dt') THEN
			ALTER TABLE gbu_source9_a_dt DROP CONSTRAINT IF EXISTS reg_9_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_9_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_9_a_dt_inx_obj_attr_id ON public.gbu_source9_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_48446061_q_pk 175*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors48446047') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors48446047') THEN
			ALTER TABLE ko_touroksfactors48446047 DROP CONSTRAINT IF EXISTS reg_48446061_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_48446061_q_pk;
		CREATE UNIQUE INDEX reg_48446061_q_pk ON public.ko_touroksfactors48446047 USING btree (id);
	END IF;
END $$;

/*reg_41707678_q_pk 176*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors41707674') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors41707674') THEN
			ALTER TABLE ko_touroksfactors41707674 DROP CONSTRAINT IF EXISTS reg_41707678_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41707678_q_pk;
		CREATE UNIQUE INDEX reg_41707678_q_pk ON public.ko_touroksfactors41707674 USING btree (id);
	END IF;
END $$;

/*reg_49413437_q_pk 177*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors49413423') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors49413423') THEN
			ALTER TABLE ko_tourzufactors49413423 DROP CONSTRAINT IF EXISTS reg_49413437_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49413437_q_pk;
		CREATE UNIQUE INDEX reg_49413437_q_pk ON public.ko_tourzufactors49413423 USING btree (id);
	END IF;
END $$;

/*reg_105_q_pk 178*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_price_history') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_price_history') THEN
			ALTER TABLE market_price_history DROP CONSTRAINT IF EXISTS reg_105_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_105_q_pk;
		alter table market_price_history add constraint reg_105_q_pk primary key (id);
	END IF;
END $$;

/*market_price_history_obj_id_idx 179*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_price_history') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_price_history') THEN
			ALTER TABLE market_price_history DROP CONSTRAINT IF EXISTS market_price_history_obj_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS market_price_history_obj_id_idx;
		CREATE INDEX market_price_history_obj_id_idx ON public.market_price_history USING btree (initial_id);
	END IF;
END $$;

/*reg_101_q_pk 180*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_address_yandex') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_address_yandex') THEN
			ALTER TABLE market_address_yandex DROP CONSTRAINT IF EXISTS reg_101_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_101_q_pk;
		alter table market_address_yandex add constraint reg_101_q_pk primary key (id);
	END IF;
END $$;

/*formalized_address_index 181*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_address_yandex') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_address_yandex') THEN
			ALTER TABLE market_address_yandex DROP CONSTRAINT IF EXISTS formalized_address_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS formalized_address_index;
		CREATE INDEX formalized_address_index ON public.market_address_yandex USING btree (formalized_address);
	END IF;
END $$;

/*pk_outer_cian_object_id 182*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_cian_object_old') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_cian_object_old') THEN
			ALTER TABLE market_cian_object_old DROP CONSTRAINT IF EXISTS pk_outer_cian_object_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS pk_outer_cian_object_id;
		alter table market_cian_object_old add constraint pk_outer_cian_object_id primary key (id);
	END IF;
END $$;

/*reg_104_q_pk 183*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_screenshots') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_screenshots') THEN
			ALTER TABLE market_screenshots DROP CONSTRAINT IF EXISTS reg_104_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_104_q_pk;
		alter table market_screenshots add constraint reg_104_q_pk primary key (id);
	END IF;
END $$;

/*market_screenshots_initial_id_idx 184*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_screenshots') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_screenshots') THEN
			ALTER TABLE market_screenshots DROP CONSTRAINT IF EXISTS market_screenshots_initial_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS market_screenshots_initial_id_idx;
		CREATE INDEX market_screenshots_initial_id_idx ON public.market_screenshots USING btree (initial_id);
	END IF;
END $$;

/*reg_41619187_q_pk 185*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_31_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_31_q') THEN
			ALTER TABLE source_31_q DROP CONSTRAINT IF EXISTS reg_41619187_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41619187_q_pk;
		CREATE UNIQUE INDEX reg_41619187_q_pk ON public.source_31_q USING btree (id);
	END IF;
END $$;

/*reg315_a_pkey 186*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_object_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_object_a') THEN
			ALTER TABLE sud_object_a DROP CONSTRAINT IF EXISTS reg315_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg315_a_pkey;
		alter table SUD_OBJECT_A add constraint reg315_a_pkey primary key (id);
	END IF;
END $$;

/*reg_110_q_pk 187*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_core_object_test') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_core_object_test') THEN
			ALTER TABLE market_core_object_test DROP CONSTRAINT IF EXISTS reg_110_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_110_q_pk;
		alter table market_core_object_test add constraint reg_110_q_pk primary key (id);
	END IF;
END $$;

/*reg_216_q_pk 188*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_cost_rosreestr') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_cost_rosreestr') THEN
			ALTER TABLE ko_cost_rosreestr DROP CONSTRAINT IF EXISTS reg_216_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_216_q_pk;
		alter table KO_COST_ROSREESTR add constraint reg_216_q_pk primary key (id);
	END IF;
END $$;

/*ko_cost_rosreestr_id_obj_idx 189*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_cost_rosreestr') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_cost_rosreestr') THEN
			ALTER TABLE ko_cost_rosreestr DROP CONSTRAINT IF EXISTS ko_cost_rosreestr_id_obj_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_cost_rosreestr_id_obj_idx;
		CREATE INDEX ko_cost_rosreestr_id_obj_idx ON public.ko_cost_rosreestr USING btree (id_object);
	END IF;
END $$;

/*reg400_a_pkey 190*/
DO $$
BEGIN
	IF (SELECT to_regclass('comission_cost_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='comission_cost_a') THEN
			ALTER TABLE comission_cost_a DROP CONSTRAINT IF EXISTS reg400_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg400_a_pkey;
		alter table COMISSION_COST_A add constraint reg400_a_pkey primary key (id);
	END IF;
END $$;

/*reg_218_q_pk 191*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_etalon') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_etalon') THEN
			ALTER TABLE ko_etalon DROP CONSTRAINT IF EXISTS reg_218_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_218_q_pk;
		alter table KO_ETALON add constraint reg_218_q_pk primary key (id);
	END IF;
END $$;

/*reg_217_q_pk 192*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_explication') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_explication') THEN
			ALTER TABLE ko_explication DROP CONSTRAINT IF EXISTS reg_217_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_217_q_pk;
		alter table KO_EXPLICATION add constraint reg_217_q_pk primary key (id);
	END IF;
END $$;

/*ko_tour_factor_register_pkey 193*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tour_factor_register') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tour_factor_register') THEN
			ALTER TABLE ko_tour_factor_register DROP CONSTRAINT IF EXISTS ko_tour_factor_register_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_tour_factor_register_pkey;
		alter table KO_TOUR_FACTOR_REGISTER add constraint ko_tour_factor_register_pkey primary key (id);
	END IF;
END $$;

/*reg_49413440_q_pk 194*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors49413423') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors49413423') THEN
			ALTER TABLE ko_touroksfactors49413423 DROP CONSTRAINT IF EXISTS reg_49413440_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49413440_q_pk;
		CREATE UNIQUE INDEX reg_49413440_q_pk ON public.ko_touroksfactors49413423 USING btree (id);
	END IF;
END $$;

/*reg_16_a_num_pk 195*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source16_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source16_a_num') THEN
			ALTER TABLE gbu_source16_a_num DROP CONSTRAINT IF EXISTS reg_16_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_16_a_num_pk;
		CREATE UNIQUE INDEX reg_16_a_num_pk ON public.gbu_source16_a_num USING btree (id);
	END IF;
END $$;

/*reg_16_a_num_inx_obj_attr_id 196*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source16_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source16_a_num') THEN
			ALTER TABLE gbu_source16_a_num DROP CONSTRAINT IF EXISTS reg_16_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_16_a_num_inx_obj_attr_id;
		CREATE INDEX reg_16_a_num_inx_obj_attr_id ON public.gbu_source16_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_16_a_txt_pk 197*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source16_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source16_a_txt') THEN
			ALTER TABLE gbu_source16_a_txt DROP CONSTRAINT IF EXISTS reg_16_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_16_a_txt_pk;
		CREATE UNIQUE INDEX reg_16_a_txt_pk ON public.gbu_source16_a_txt USING btree (id);
	END IF;
END $$;

/*reg_16_a_txt_inx_obj_attr_id 198*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source16_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source16_a_txt') THEN
			ALTER TABLE gbu_source16_a_txt DROP CONSTRAINT IF EXISTS reg_16_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_16_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_16_a_txt_inx_obj_attr_id ON public.gbu_source16_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_17_a_dt_pk 199*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source17_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source17_a_dt') THEN
			ALTER TABLE gbu_source17_a_dt DROP CONSTRAINT IF EXISTS reg_17_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_17_a_dt_pk;
		CREATE UNIQUE INDEX reg_17_a_dt_pk ON public.gbu_source17_a_dt USING btree (id);
	END IF;
END $$;

/*reg_17_a_dt_inx_obj_attr_id 200*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source17_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source17_a_dt') THEN
			ALTER TABLE gbu_source17_a_dt DROP CONSTRAINT IF EXISTS reg_17_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_17_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_17_a_dt_inx_obj_attr_id ON public.gbu_source17_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_6_a_dt_pk 201*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source6_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source6_a_dt') THEN
			ALTER TABLE gbu_source6_a_dt DROP CONSTRAINT IF EXISTS reg_6_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_6_a_dt_pk;
		CREATE UNIQUE INDEX reg_6_a_dt_pk ON public.gbu_source6_a_dt USING btree (id);
	END IF;
END $$;

/*reg_6_a_dt_inx_obj_attr_id 202*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source6_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source6_a_dt') THEN
			ALTER TABLE gbu_source6_a_dt DROP CONSTRAINT IF EXISTS reg_6_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_6_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_6_a_dt_inx_obj_attr_id ON public.gbu_source6_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578488_a_dt_pk 203*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_43_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_43_dt') THEN
			ALTER TABLE gbu_custom_source_43_dt DROP CONSTRAINT IF EXISTS reg_54578488_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578488_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578488_a_dt_pk ON public.gbu_custom_source_43_dt USING btree (id);
	END IF;
END $$;

/*reg_54578488_a_dt_inx_obj_attr_id 204*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_43_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_43_dt') THEN
			ALTER TABLE gbu_custom_source_43_dt DROP CONSTRAINT IF EXISTS reg_54578488_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578488_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578488_a_dt_inx_obj_attr_id ON public.gbu_custom_source_43_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_6_a_num_pk 205*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source6_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source6_a_num') THEN
			ALTER TABLE gbu_source6_a_num DROP CONSTRAINT IF EXISTS reg_6_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_6_a_num_pk;
		CREATE UNIQUE INDEX reg_6_a_num_pk ON public.gbu_source6_a_num USING btree (id);
	END IF;
END $$;

/*reg_6_a_num_inx_obj_attr_id 206*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source6_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source6_a_num') THEN
			ALTER TABLE gbu_source6_a_num DROP CONSTRAINT IF EXISTS reg_6_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_6_a_num_inx_obj_attr_id;
		CREATE INDEX reg_6_a_num_inx_obj_attr_id ON public.gbu_source6_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_6_a_txt_pk 207*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source6_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source6_a_txt') THEN
			ALTER TABLE gbu_source6_a_txt DROP CONSTRAINT IF EXISTS reg_6_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_6_a_txt_pk;
		CREATE UNIQUE INDEX reg_6_a_txt_pk ON public.gbu_source6_a_txt USING btree (id);
	END IF;
END $$;

/*reg_6_a_txt_inx_obj_attr_id 208*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source6_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source6_a_txt') THEN
			ALTER TABLE gbu_source6_a_txt DROP CONSTRAINT IF EXISTS reg_6_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_6_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_6_a_txt_inx_obj_attr_id ON public.gbu_source6_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_202_q_pk 209*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tour') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tour') THEN
			ALTER TABLE ko_tour DROP CONSTRAINT IF EXISTS reg_202_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_202_q_pk;
		alter table KO_TOUR add constraint reg_202_q_pk primary key (id);
	END IF;
END $$;

/*ko_tour_year_key 210*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tour') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tour') THEN
			ALTER TABLE ko_tour DROP CONSTRAINT IF EXISTS ko_tour_year_key RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_tour_year_key;
		CREATE UNIQUE INDEX ko_tour_year_key ON public.ko_tour USING btree (year);
	END IF;
END $$;

/*reg_48446048_q_pk 211*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors48446047') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors48446047') THEN
			ALTER TABLE ko_tourzufactors48446047 DROP CONSTRAINT IF EXISTS reg_48446048_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_48446048_q_pk;
		CREATE UNIQUE INDEX reg_48446048_q_pk ON public.ko_tourzufactors48446047 USING btree (id);
	END IF;
END $$;

/*reg_812_q_pk 212*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_recycle_bin') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_recycle_bin') THEN
			ALTER TABLE common_recycle_bin DROP CONSTRAINT IF EXISTS reg_812_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_812_q_pk;
		alter table COMMON_RECYCLE_BIN add constraint reg_812_q_pk primary key (event_id);
	END IF;
END $$;

/*reg_813_q_pk 213*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_registers_with_soft_deletion') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_registers_with_soft_deletion') THEN
			ALTER TABLE common_registers_with_soft_deletion DROP CONSTRAINT IF EXISTS reg_813_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_813_q_pk;
		alter table COMMON_REGISTERS_WITH_SOFT_DELETION add constraint reg_813_q_pk primary key (id);
	END IF;
END $$;

/*reg_813_main_table_name_unique 214*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_registers_with_soft_deletion') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_registers_with_soft_deletion') THEN
			ALTER TABLE common_registers_with_soft_deletion DROP CONSTRAINT IF EXISTS reg_813_main_table_name_unique RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_813_main_table_name_unique;
		CREATE UNIQUE INDEX reg_813_main_table_name_unique ON public.common_registers_with_soft_deletion USING btree (main_table_name);
	END IF;
END $$;

/*reg_48400455_q_pk 215*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors38670860') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors38670860') THEN
			ALTER TABLE ko_tourzufactors38670860 DROP CONSTRAINT IF EXISTS reg_48400455_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_48400455_q_pk;
		CREATE UNIQUE INDEX reg_48400455_q_pk ON public.ko_tourzufactors38670860 USING btree (id);
	END IF;
END $$;

/*avg_ks_heatmap_chosen_category_type_index 216*/
DO $$
BEGIN
	IF (SELECT to_regclass('average_cadastral_cost_data_for_heatmap') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='average_cadastral_cost_data_for_heatmap') THEN
			ALTER TABLE average_cadastral_cost_data_for_heatmap DROP CONSTRAINT IF EXISTS avg_ks_heatmap_chosen_category_type_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS avg_ks_heatmap_chosen_category_type_index;
		CREATE INDEX avg_ks_heatmap_chosen_category_type_index ON public.average_cadastral_cost_data_for_heatmap USING btree (tourid, propertytype, mapdivisiontype);
	END IF;
END $$;

/*avg_ks_heatmap_unique_index 217*/
DO $$
BEGIN
	IF (SELECT to_regclass('average_cadastral_cost_data_for_heatmap') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='average_cadastral_cost_data_for_heatmap') THEN
			ALTER TABLE average_cadastral_cost_data_for_heatmap DROP CONSTRAINT IF EXISTS avg_ks_heatmap_unique_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS avg_ks_heatmap_unique_index;
		CREATE UNIQUE INDEX avg_ks_heatmap_unique_index ON public.average_cadastral_cost_data_for_heatmap USING btree (tourid, propertytype, mapdivisiontype, mapdivisionname);
	END IF;
END $$;

/*reg_2_a_661__pk 218*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_661') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_661') THEN
			ALTER TABLE gbu_source2_a_661 DROP CONSTRAINT IF EXISTS reg_2_a_661__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_661__pk;
		CREATE UNIQUE INDEX reg_2_a_661__pk ON public.gbu_source2_a_661 USING btree (id);
	END IF;
END $$;

/*reg_2_a_661_inx_obj_attr_id 219*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_661') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_661') THEN
			ALTER TABLE gbu_source2_a_661 DROP CONSTRAINT IF EXISTS reg_2_a_661_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_661_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_661_inx_obj_attr_id ON public.gbu_source2_a_661 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_662__pk 220*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_662') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_662') THEN
			ALTER TABLE gbu_source2_a_662 DROP CONSTRAINT IF EXISTS reg_2_a_662__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_662__pk;
		CREATE UNIQUE INDEX reg_2_a_662__pk ON public.gbu_source2_a_662 USING btree (id);
	END IF;
END $$;

/*reg_2_a_662_inx_obj_attr_id 221*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_662') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_662') THEN
			ALTER TABLE gbu_source2_a_662 DROP CONSTRAINT IF EXISTS reg_2_a_662_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_662_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_662_inx_obj_attr_id ON public.gbu_source2_a_662 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_47020715_a_txt_pk 222*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_33_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_33_txt') THEN
			ALTER TABLE gbu_custom_source_33_txt DROP CONSTRAINT IF EXISTS reg_47020715_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020715_a_txt_pk;
		CREATE UNIQUE INDEX reg_47020715_a_txt_pk ON public.gbu_custom_source_33_txt USING btree (id);
	END IF;
END $$;

/*reg_47020715_a_txt_inx_obj_attr_id 223*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_33_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_33_txt') THEN
			ALTER TABLE gbu_custom_source_33_txt DROP CONSTRAINT IF EXISTS reg_47020715_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020715_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_47020715_a_txt_inx_obj_attr_id ON public.gbu_custom_source_33_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_2_a_663__pk 224*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_663') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_663') THEN
			ALTER TABLE gbu_source2_a_663 DROP CONSTRAINT IF EXISTS reg_2_a_663__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_663__pk;
		CREATE UNIQUE INDEX reg_2_a_663__pk ON public.gbu_source2_a_663 USING btree (id);
	END IF;
END $$;

/*reg_2_a_663_inx_obj_attr_id 225*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_663') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_663') THEN
			ALTER TABLE gbu_source2_a_663 DROP CONSTRAINT IF EXISTS reg_2_a_663_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_663_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_663_inx_obj_attr_id ON public.gbu_source2_a_663 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_29860805_q_pk 226*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors29839891') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors29839891') THEN
			ALTER TABLE ko_tourzufactors29839891 DROP CONSTRAINT IF EXISTS reg_29860805_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29860805_q_pk;
		CREATE UNIQUE INDEX reg_29860805_q_pk ON public.ko_tourzufactors29839891 USING btree (id);
	END IF;
END $$;

/*reg_47020715_a_dt_pk 227*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_33_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_33_dt') THEN
			ALTER TABLE gbu_custom_source_33_dt DROP CONSTRAINT IF EXISTS reg_47020715_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020715_a_dt_pk;
		CREATE UNIQUE INDEX reg_47020715_a_dt_pk ON public.gbu_custom_source_33_dt USING btree (id);
	END IF;
END $$;

/*reg_47020715_a_dt_inx_obj_attr_id 228*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_33_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_33_dt') THEN
			ALTER TABLE gbu_custom_source_33_dt DROP CONSTRAINT IF EXISTS reg_47020715_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020715_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_47020715_a_dt_inx_obj_attr_id ON public.gbu_custom_source_33_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_2_a_2__pk 229*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_2') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_2') THEN
			ALTER TABLE gbu_source2_a_2 DROP CONSTRAINT IF EXISTS reg_2_a_2__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_2__pk;
		CREATE UNIQUE INDEX reg_2_a_2__pk ON public.gbu_source2_a_2 USING btree (id);
	END IF;
END $$;

/*reg_2_a_2_inx_obj_attr_id 230*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_2') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_2') THEN
			ALTER TABLE gbu_source2_a_2 DROP CONSTRAINT IF EXISTS reg_2_a_2_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_2_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_2_inx_obj_attr_id ON public.gbu_source2_a_2 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_54578491_a_txt_pk 231*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_44_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_44_txt') THEN
			ALTER TABLE gbu_custom_source_44_txt DROP CONSTRAINT IF EXISTS reg_54578491_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578491_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578491_a_txt_pk ON public.gbu_custom_source_44_txt USING btree (id);
	END IF;
END $$;

/*reg_54578491_a_txt_inx_obj_attr_id 232*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_44_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_44_txt') THEN
			ALTER TABLE gbu_custom_source_44_txt DROP CONSTRAINT IF EXISTS reg_54578491_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578491_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578491_a_txt_inx_obj_attr_id ON public.gbu_custom_source_44_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_220_q_pk 233*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_document_link') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_document_link') THEN
			ALTER TABLE ko_document_link DROP CONSTRAINT IF EXISTS reg_220_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_220_q_pk;
		alter table KO_DOCUMENT_LINK add constraint reg_220_q_pk primary key (id);
	END IF;
END $$;

/*reg_300_q_pk 234*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_zak') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_zak') THEN
			ALTER TABLE sud_zak DROP CONSTRAINT IF EXISTS reg_300_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_300_q_pk;
		alter table SUD_ZAK add constraint reg_300_q_pk primary key (id);
	END IF;
END $$;

/*reg_41935242_q_pk 235*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_33_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_33_q') THEN
			ALTER TABLE source_33_q DROP CONSTRAINT IF EXISTS reg_41935242_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41935242_q_pk;
		CREATE UNIQUE INDEX reg_41935242_q_pk ON public.source_33_q USING btree (id);
	END IF;
END $$;

/*declarations_declaration_a_pkey 236*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_declaration_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_declaration_a') THEN
			ALTER TABLE declarations_declaration_a DROP CONSTRAINT IF EXISTS declarations_declaration_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_declaration_a_pkey;
		alter table DECLARATIONS_DECLARATION_A add constraint declarations_declaration_a_pkey primary key (id);
	END IF;
END $$;

/*declarations_declaration_a_obj_attr_idx 237*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_declaration_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_declaration_a') THEN
			ALTER TABLE declarations_declaration_a DROP CONSTRAINT IF EXISTS declarations_declaration_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_declaration_a_obj_attr_idx;
		CREATE INDEX declarations_declaration_a_obj_attr_idx ON public.declarations_declaration_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_47686787_q_pk 238*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors47504845') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors47504845') THEN
			ALTER TABLE ko_tourzufactors47504845 DROP CONSTRAINT IF EXISTS reg_47686787_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47686787_q_pk;
		CREATE UNIQUE INDEX reg_47686787_q_pk ON public.ko_tourzufactors47504845 USING btree (id);
	END IF;
END $$;

/*reg_502_q_pk 239*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_oks') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_oks') THEN
			ALTER TABLE declarations_har_oks DROP CONSTRAINT IF EXISTS reg_502_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_502_q_pk;
		alter table DECLARATIONS_HAR_OKS add constraint reg_502_q_pk primary key (id);
	END IF;
END $$;

/*declarations_har_oks_a_pkey 240*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_oks_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_oks_a') THEN
			ALTER TABLE declarations_har_oks_a DROP CONSTRAINT IF EXISTS declarations_har_oks_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_har_oks_a_pkey;
		alter table DECLARATIONS_HAR_OKS_A add constraint declarations_har_oks_a_pkey primary key (id);
	END IF;
END $$;

/*declarations_har_oks_a_obj_attr_idx 241*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_oks_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_oks_a') THEN
			ALTER TABLE declarations_har_oks_a DROP CONSTRAINT IF EXISTS declarations_har_oks_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_har_oks_a_obj_attr_idx;
		CREATE INDEX declarations_har_oks_a_obj_attr_idx ON public.declarations_har_oks_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_39964641_q_pk 242*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_25_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_25_q') THEN
			ALTER TABLE source_25_q DROP CONSTRAINT IF EXISTS reg_39964641_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_39964641_q_pk;
		CREATE UNIQUE INDEX reg_39964641_q_pk ON public.source_25_q USING btree (id);
	END IF;
END $$;

/*reg_39964643_q_pk 243*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_26_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_26_q') THEN
			ALTER TABLE source_26_q DROP CONSTRAINT IF EXISTS reg_39964643_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_39964643_q_pk;
		CREATE UNIQUE INDEX reg_39964643_q_pk ON public.source_26_q USING btree (id);
	END IF;
END $$;

/*reg_949_quant_pk 244*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_session') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_session') THEN
			ALTER TABLE core_srd_session DROP CONSTRAINT IF EXISTS reg_949_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_949_quant_pk;
		alter table CORE_SRD_SESSION add constraint reg_949_quant_pk primary key (id);
	END IF;
END $$;

/*reg_305_q_pk 245*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_objectstatus') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_objectstatus') THEN
			ALTER TABLE sud_objectstatus DROP CONSTRAINT IF EXISTS reg_305_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_305_q_pk;
		alter table SUD_OBJECTSTATUS add constraint reg_305_q_pk primary key (id);
	END IF;
END $$;

/*ko_compliance_guide_pkey 246*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_compliance_guide') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_compliance_guide') THEN
			ALTER TABLE ko_compliance_guide DROP CONSTRAINT IF EXISTS ko_compliance_guide_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_compliance_guide_pkey;
		alter table ko_compliance_guide add constraint ko_compliance_guide_pkey primary key (id);
	END IF;
END $$;

/*reg_54578491_a_num_pk 247*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_44_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_44_num') THEN
			ALTER TABLE gbu_custom_source_44_num DROP CONSTRAINT IF EXISTS reg_54578491_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578491_a_num_pk;
		CREATE UNIQUE INDEX reg_54578491_a_num_pk ON public.gbu_custom_source_44_num USING btree (id);
	END IF;
END $$;

/*reg_54578491_a_num_inx_obj_attr_id 248*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_44_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_44_num') THEN
			ALTER TABLE gbu_custom_source_44_num DROP CONSTRAINT IF EXISTS reg_54578491_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578491_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578491_a_num_inx_obj_attr_id ON public.gbu_custom_source_44_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_47020718_a_txt_pk 249*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_34_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_34_txt') THEN
			ALTER TABLE gbu_custom_source_34_txt DROP CONSTRAINT IF EXISTS reg_47020718_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020718_a_txt_pk;
		CREATE UNIQUE INDEX reg_47020718_a_txt_pk ON public.gbu_custom_source_34_txt USING btree (id);
	END IF;
END $$;

/*reg_47020718_a_txt_inx_obj_attr_id 250*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_34_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_34_txt') THEN
			ALTER TABLE gbu_custom_source_34_txt DROP CONSTRAINT IF EXISTS reg_47020718_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020718_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_47020718_a_txt_inx_obj_attr_id ON public.gbu_custom_source_34_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_47020718_a_num_pk 251*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_34_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_34_num') THEN
			ALTER TABLE gbu_custom_source_34_num DROP CONSTRAINT IF EXISTS reg_47020718_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020718_a_num_pk;
		CREATE UNIQUE INDEX reg_47020718_a_num_pk ON public.gbu_custom_source_34_num USING btree (id);
	END IF;
END $$;

/*reg_47020718_a_num_inx_obj_attr_id 252*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_34_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_34_num') THEN
			ALTER TABLE gbu_custom_source_34_num DROP CONSTRAINT IF EXISTS reg_47020718_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020718_a_num_inx_obj_attr_id;
		CREATE INDEX reg_47020718_a_num_inx_obj_attr_id ON public.gbu_custom_source_34_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_49412726_a_num_pk 253*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_36_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_36_num') THEN
			ALTER TABLE gbu_custom_source_36_num DROP CONSTRAINT IF EXISTS reg_49412726_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49412726_a_num_pk;
		CREATE UNIQUE INDEX reg_49412726_a_num_pk ON public.gbu_custom_source_36_num USING btree (id);
	END IF;
END $$;

/*reg_49412726_a_num_inx_obj_attr_id 254*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_36_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_36_num') THEN
			ALTER TABLE gbu_custom_source_36_num DROP CONSTRAINT IF EXISTS reg_49412726_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49412726_a_num_inx_obj_attr_id;
		CREATE INDEX reg_49412726_a_num_inx_obj_attr_id ON public.gbu_custom_source_36_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_47020718_a_dt_pk 255*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_34_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_34_dt') THEN
			ALTER TABLE gbu_custom_source_34_dt DROP CONSTRAINT IF EXISTS reg_47020718_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020718_a_dt_pk;
		CREATE UNIQUE INDEX reg_47020718_a_dt_pk ON public.gbu_custom_source_34_dt USING btree (id);
	END IF;
END $$;

/*reg_47020718_a_dt_inx_obj_attr_id 256*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_34_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_34_dt') THEN
			ALTER TABLE gbu_custom_source_34_dt DROP CONSTRAINT IF EXISTS reg_47020718_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020718_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_47020718_a_dt_inx_obj_attr_id ON public.gbu_custom_source_34_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_251_q_pk 257*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_params_zu_2018') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_params_zu_2018') THEN
			ALTER TABLE ko_unit_params_zu_2018 DROP CONSTRAINT IF EXISTS reg_251_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_251_q_pk;
		alter table KO_UNIT_PARAMS_ZU_2018 add constraint reg_251_q_pk primary key (id);
	END IF;
END $$;

/*reg_50437121_q_pk 258*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors50437120') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors50437120') THEN
			ALTER TABLE ko_tourzufactors50437120 DROP CONSTRAINT IF EXISTS reg_50437121_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_50437121_q_pk;
		CREATE UNIQUE INDEX reg_50437121_q_pk ON public.ko_tourzufactors50437120 USING btree (id);
	END IF;
END $$;

/*reg_49412726_a_dt_pk 259*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_36_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_36_dt') THEN
			ALTER TABLE gbu_custom_source_36_dt DROP CONSTRAINT IF EXISTS reg_49412726_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49412726_a_dt_pk;
		CREATE UNIQUE INDEX reg_49412726_a_dt_pk ON public.gbu_custom_source_36_dt USING btree (id);
	END IF;
END $$;

/*reg_49412726_a_dt_inx_obj_attr_id 260*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_36_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_36_dt') THEN
			ALTER TABLE gbu_custom_source_36_dt DROP CONSTRAINT IF EXISTS reg_49412726_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49412726_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_49412726_a_dt_inx_obj_attr_id ON public.gbu_custom_source_36_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578491_a_dt_pk 261*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_44_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_44_dt') THEN
			ALTER TABLE gbu_custom_source_44_dt DROP CONSTRAINT IF EXISTS reg_54578491_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578491_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578491_a_dt_pk ON public.gbu_custom_source_44_dt USING btree (id);
	END IF;
END $$;

/*reg_54578491_a_dt_inx_obj_attr_id 262*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_44_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_44_dt') THEN
			ALTER TABLE gbu_custom_source_44_dt DROP CONSTRAINT IF EXISTS reg_54578491_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578491_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578491_a_dt_inx_obj_attr_id ON public.gbu_custom_source_44_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*sud_objectadditionalanalysislog_pkey 263*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_dopanaliz_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_dopanaliz_log') THEN
			ALTER TABLE sud_dopanaliz_log DROP CONSTRAINT IF EXISTS sud_objectadditionalanalysislog_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_objectadditionalanalysislog_pkey;
		alter table SUD_DOPANALIZ_LOG add constraint sud_objectadditionalanalysislog_pkey primary key (id);
	END IF;
END $$;

/*reg_510_q_pk 264*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_parcel_additional_info') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_parcel_additional_info') THEN
			ALTER TABLE declarations_har_parcel_additional_info DROP CONSTRAINT IF EXISTS reg_510_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_510_q_pk;
		alter table DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO add constraint reg_510_q_pk primary key (id);
	END IF;
END $$;

/*declarations_har_parcel_additional_info_har_id_idx 265*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_parcel_additional_info') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_parcel_additional_info') THEN
			ALTER TABLE declarations_har_parcel_additional_info DROP CONSTRAINT IF EXISTS declarations_har_parcel_additional_info_har_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_har_parcel_additional_info_har_id_idx;
		CREATE INDEX declarations_har_parcel_additional_info_har_id_idx ON public.declarations_har_parcel_additional_info USING btree (har_parcel_id);
	END IF;
END $$;

/*reg_510_q_unique 266*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_parcel_additional_info') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_parcel_additional_info') THEN
			ALTER TABLE declarations_har_parcel_additional_info DROP CONSTRAINT IF EXISTS reg_510_q_unique RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_510_q_unique;
		CREATE UNIQUE INDEX reg_510_q_unique ON public.declarations_har_parcel_additional_info USING btree (har_parcel_id, har_parcel_name);
	END IF;
END $$;

-- /*reg_609_q_pk 267*/
-- DO $$
-- BEGIN
-- 	IF (SELECT to_regclass('es_reference') IS NOT null) THEN
-- 		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='es_reference') THEN
-- 			ALTER TABLE es_reference DROP CONSTRAINT IF EXISTS reg_609_q_pk RESTRICT;
-- 		END IF;
-- 		DROP INDEX IF EXISTS reg_609_q_pk;
-- 		alter table ES_REFERENCE add constraint reg_609_q_pk primary key (id);
-- 	END IF;
-- END $$;

/*reg_200_q_pk 268*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_main_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_main_object') THEN
			ALTER TABLE gbu_main_object DROP CONSTRAINT IF EXISTS reg_200_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_200_q_pk;
		alter table GBU_MAIN_OBJECT add constraint reg_200_q_pk primary key (id);
	END IF;
END $$;

/*"CadastralNumberMainIndex" 269*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_main_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_main_object') THEN
			ALTER TABLE gbu_main_object DROP CONSTRAINT IF EXISTS "CadastralNumberMainIndex" RESTRICT;
		END IF;
		DROP INDEX IF EXISTS "CadastralNumberMainIndex";
		CREATE INDEX "CadastralNumberMainIndex" ON public.gbu_main_object USING btree (cadastral_number);
	END IF;
END $$;

/*gbu_main_object_kad_num_text_inx 270*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_main_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_main_object') THEN
			ALTER TABLE gbu_main_object DROP CONSTRAINT IF EXISTS gbu_main_object_kad_num_text_inx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS gbu_main_object_kad_num_text_inx;
		CREATE INDEX gbu_main_object_kad_num_text_inx ON public.gbu_main_object USING gin (cadastral_number gin_trgm_ops);
	END IF;
END $$;

/*gbu_main_object_type_idx 271*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_main_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_main_object') THEN
			ALTER TABLE gbu_main_object DROP CONSTRAINT IF EXISTS gbu_main_object_type_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS gbu_main_object_type_idx;
		CREATE INDEX gbu_main_object_type_idx ON public.gbu_main_object USING btree (object_type_code);
	END IF;
END $$;

/*gbu_main_object_kvart_idx 272*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_main_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_main_object') THEN
			ALTER TABLE gbu_main_object DROP CONSTRAINT IF EXISTS gbu_main_object_kvart_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS gbu_main_object_kvart_idx;
		CREATE INDEX gbu_main_object_kvart_idx ON public.gbu_main_object USING btree (kadastr_kvartal);
	END IF;
END $$;

/*reg_509_q_pk 273*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_oks_additional_info') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_oks_additional_info') THEN
			ALTER TABLE declarations_har_oks_additional_info DROP CONSTRAINT IF EXISTS reg_509_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_509_q_pk;
		alter table DECLARATIONS_HAR_OKS_ADDITIONAL_INFO add constraint reg_509_q_pk primary key (id);
	END IF;
END $$;

/*declarations_har_oks_additional_info_har_id_idx 274*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_oks_additional_info') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_oks_additional_info') THEN
			ALTER TABLE declarations_har_oks_additional_info DROP CONSTRAINT IF EXISTS declarations_har_oks_additional_info_har_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_har_oks_additional_info_har_id_idx;
		CREATE INDEX declarations_har_oks_additional_info_har_id_idx ON public.declarations_har_oks_additional_info USING btree (har_oks_id);
	END IF;
END $$;

/*reg_509_q_unique 275*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_oks_additional_info') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_oks_additional_info') THEN
			ALTER TABLE declarations_har_oks_additional_info DROP CONSTRAINT IF EXISTS reg_509_q_unique RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_509_q_unique;
		CREATE UNIQUE INDEX reg_509_q_unique ON public.declarations_har_oks_additional_info USING btree (har_oks_id, har_oks_name);
	END IF;
END $$;

/*tmp_attr_8_2_obj_id_idx 276*/
DO $$
BEGIN
	IF (SELECT to_regclass('tmp_attr_8_2') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='tmp_attr_8_2') THEN
			ALTER TABLE tmp_attr_8_2 DROP CONSTRAINT IF EXISTS tmp_attr_8_2_obj_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS tmp_attr_8_2_obj_id_idx;
		CREATE INDEX tmp_attr_8_2_obj_id_idx ON public.tmp_attr_8_2 USING btree (object_id);
	END IF;
END $$;

/*ko_report_history_pkey 277*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_report_history') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_report_history') THEN
			ALTER TABLE ko_report_history DROP CONSTRAINT IF EXISTS ko_report_history_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_report_history_pkey;
		alter table KO_REPORT_HISTORY add constraint ko_report_history_pkey primary key (id);
	END IF;
END $$;

/*reg_113_q_pk 278*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_price_after_correction_by_rooms_h') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_price_after_correction_by_rooms_h') THEN
			ALTER TABLE market_price_after_correction_by_rooms_h DROP CONSTRAINT IF EXISTS reg_113_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_113_q_pk;
		alter table market_price_after_correction_by_rooms_h add constraint reg_113_q_pk primary key (id);
	END IF;
END $$;

/*reg_988_quant_pk 279*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_attachment_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_attachment_object') THEN
			ALTER TABLE core_attachment_object DROP CONSTRAINT IF EXISTS reg_988_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_988_quant_pk;
		alter table CORE_ATTACHMENT_OBJECT add constraint reg_988_quant_pk primary key (id);
	END IF;
END $$;

/*reg_996_quant_pk 280*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_cache_updates') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_cache_updates') THEN
			ALTER TABLE core_cache_updates DROP CONSTRAINT IF EXISTS reg_996_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_996_quant_pk;
		alter table CORE_CACHE_UPDATES add constraint reg_996_quant_pk primary key (id);
	END IF;
END $$;

/*reg_41983898_a_num_pk 281*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_23_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_23_num') THEN
			ALTER TABLE gbu_custom_source_23_num DROP CONSTRAINT IF EXISTS reg_41983898_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41983898_a_num_pk;
		CREATE UNIQUE INDEX reg_41983898_a_num_pk ON public.gbu_custom_source_23_num USING btree (id);
	END IF;
END $$;

/*reg_41983898_a_num_inx_obj_attr_id 282*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_23_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_23_num') THEN
			ALTER TABLE gbu_custom_source_23_num DROP CONSTRAINT IF EXISTS reg_41983898_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41983898_a_num_inx_obj_attr_id;
		CREATE INDEX reg_41983898_a_num_inx_obj_attr_id ON public.gbu_custom_source_23_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_41983898_a_dt_pk 283*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_23_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_23_dt') THEN
			ALTER TABLE gbu_custom_source_23_dt DROP CONSTRAINT IF EXISTS reg_41983898_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41983898_a_dt_pk;
		CREATE UNIQUE INDEX reg_41983898_a_dt_pk ON public.gbu_custom_source_23_dt USING btree (id);
	END IF;
END $$;

/*reg_41983898_a_dt_inx_obj_attr_id 284*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_23_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_23_dt') THEN
			ALTER TABLE gbu_custom_source_23_dt DROP CONSTRAINT IF EXISTS reg_41983898_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41983898_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_41983898_a_dt_inx_obj_attr_id ON public.gbu_custom_source_23_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_114_q_pk 285*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_coefficients_for_first_floor_corr') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_coefficients_for_first_floor_corr') THEN
			ALTER TABLE market_coefficients_for_first_floor_corr DROP CONSTRAINT IF EXISTS reg_114_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_114_q_pk;
		alter table market_coefficients_for_first_floor_corr add constraint reg_114_q_pk primary key (id);
	END IF;
END $$;

/*coefficient_for_first_floor_correction_index 286*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_coefficients_for_first_floor_corr') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_coefficients_for_first_floor_corr') THEN
			ALTER TABLE market_coefficients_for_first_floor_corr DROP CONSTRAINT IF EXISTS coefficient_for_first_floor_correction_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS coefficient_for_first_floor_correction_index;
		CREATE UNIQUE INDEX coefficient_for_first_floor_correction_index ON public.market_coefficients_for_first_floor_corr USING btree (building_cadastral_number, stats_date, market_segment_code);
	END IF;
END $$;

/*es_to_market_core_object_pkey 287*/
DO $$
BEGIN
	IF (SELECT to_regclass('es_to_market_core_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='es_to_market_core_object') THEN
			ALTER TABLE es_to_market_core_object DROP CONSTRAINT IF EXISTS es_to_market_core_object_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS es_to_market_core_object_pkey;
		alter table ES_TO_MARKET_CORE_OBJECT add constraint es_to_market_core_object_pkey primary key (id);
	END IF;
END $$;

/*reg_42430534_a_txt_pk 288*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_24_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_24_txt') THEN
			ALTER TABLE gbu_custom_source_24_txt DROP CONSTRAINT IF EXISTS reg_42430534_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42430534_a_txt_pk;
		CREATE UNIQUE INDEX reg_42430534_a_txt_pk ON public.gbu_custom_source_24_txt USING btree (id);
	END IF;
END $$;

/*reg_42430534_a_txt_inx_obj_attr_id 289*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_24_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_24_txt') THEN
			ALTER TABLE gbu_custom_source_24_txt DROP CONSTRAINT IF EXISTS reg_42430534_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42430534_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_42430534_a_txt_inx_obj_attr_id ON public.gbu_custom_source_24_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_46811238_a_txt_pk 290*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_32_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_32_txt') THEN
			ALTER TABLE gbu_custom_source_32_txt DROP CONSTRAINT IF EXISTS reg_46811238_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811238_a_txt_pk;
		CREATE UNIQUE INDEX reg_46811238_a_txt_pk ON public.gbu_custom_source_32_txt USING btree (id);
	END IF;
END $$;

/*reg_46811238_a_txt_inx_obj_attr_id 291*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_32_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_32_txt') THEN
			ALTER TABLE gbu_custom_source_32_txt DROP CONSTRAINT IF EXISTS reg_46811238_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811238_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_46811238_a_txt_inx_obj_attr_id ON public.gbu_custom_source_32_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_42430534_a_num_pk 292*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_24_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_24_num') THEN
			ALTER TABLE gbu_custom_source_24_num DROP CONSTRAINT IF EXISTS reg_42430534_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42430534_a_num_pk;
		CREATE UNIQUE INDEX reg_42430534_a_num_pk ON public.gbu_custom_source_24_num USING btree (id);
	END IF;
END $$;

/*reg_42430534_a_num_inx_obj_attr_id 293*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_24_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_24_num') THEN
			ALTER TABLE gbu_custom_source_24_num DROP CONSTRAINT IF EXISTS reg_42430534_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42430534_a_num_inx_obj_attr_id;
		CREATE INDEX reg_42430534_a_num_inx_obj_attr_id ON public.gbu_custom_source_24_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_42436643_a_txt_pk 294*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_25_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_25_txt') THEN
			ALTER TABLE gbu_custom_source_25_txt DROP CONSTRAINT IF EXISTS reg_42436643_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42436643_a_txt_pk;
		CREATE UNIQUE INDEX reg_42436643_a_txt_pk ON public.gbu_custom_source_25_txt USING btree (id);
	END IF;
END $$;

/*reg_42436643_a_txt_inx_obj_attr_id 295*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_25_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_25_txt') THEN
			ALTER TABLE gbu_custom_source_25_txt DROP CONSTRAINT IF EXISTS reg_42436643_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42436643_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_42436643_a_txt_inx_obj_attr_id ON public.gbu_custom_source_25_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_42430534_a_dt_pk 296*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_24_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_24_dt') THEN
			ALTER TABLE gbu_custom_source_24_dt DROP CONSTRAINT IF EXISTS reg_42430534_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42430534_a_dt_pk;
		CREATE UNIQUE INDEX reg_42430534_a_dt_pk ON public.gbu_custom_source_24_dt USING btree (id);
	END IF;
END $$;

/*reg_42430534_a_dt_inx_obj_attr_id 297*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_24_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_24_dt') THEN
			ALTER TABLE gbu_custom_source_24_dt DROP CONSTRAINT IF EXISTS reg_42430534_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42430534_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_42430534_a_dt_inx_obj_attr_id ON public.gbu_custom_source_24_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_610_q_pk 298*/
DO $$
BEGIN
	IF (SELECT to_regclass('es_reference_item') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='es_reference_item') THEN
			ALTER TABLE es_reference_item DROP CONSTRAINT IF EXISTS reg_610_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_610_q_pk;
		alter table ES_REFERENCE_ITEM add constraint reg_610_q_pk primary key (id);
	END IF;
END $$;

/*reg_42436643_a_num_pk 299*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_25_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_25_num') THEN
			ALTER TABLE gbu_custom_source_25_num DROP CONSTRAINT IF EXISTS reg_42436643_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42436643_a_num_pk;
		CREATE UNIQUE INDEX reg_42436643_a_num_pk ON public.gbu_custom_source_25_num USING btree (id);
	END IF;
END $$;

/*reg_42436643_a_num_inx_obj_attr_id 300*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_25_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_25_num') THEN
			ALTER TABLE gbu_custom_source_25_num DROP CONSTRAINT IF EXISTS reg_42436643_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42436643_a_num_inx_obj_attr_id;
		CREATE INDEX reg_42436643_a_num_inx_obj_attr_id ON public.gbu_custom_source_25_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_2_a_49420743__pk 301*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420743') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420743') THEN
			ALTER TABLE gbu_source2_a_49420743 DROP CONSTRAINT IF EXISTS reg_2_a_49420743__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420743__pk;
		CREATE UNIQUE INDEX reg_2_a_49420743__pk ON public.gbu_source2_a_49420743 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420743_inx_obj_attr_id 302*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420743') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420743') THEN
			ALTER TABLE gbu_source2_a_49420743 DROP CONSTRAINT IF EXISTS reg_2_a_49420743_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420743_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420743_inx_obj_attr_id ON public.gbu_source2_a_49420743 USING btree (object_id, ot);
	END IF;
END $$;

/*core_messages_pk 303*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_messages') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_messages') THEN
			ALTER TABLE core_messages DROP CONSTRAINT IF EXISTS core_messages_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_messages_pk;
		alter table CORE_MESSAGES add constraint core_messages_pk primary key (id);
	END IF;
END $$;

/*core_messages_to_pk 304*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_messages_to') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_messages_to') THEN
			ALTER TABLE core_messages_to DROP CONSTRAINT IF EXISTS core_messages_to_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_messages_to_pk;
		alter table CORE_MESSAGES_TO add constraint core_messages_to_pk primary key (id);
	END IF;
END $$;

/*reg_978_quant_pk 305*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_configparam') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_configparam') THEN
			ALTER TABLE core_configparam DROP CONSTRAINT IF EXISTS reg_978_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_978_quant_pk;
		alter table CORE_CONFIGPARAM add constraint reg_978_quant_pk primary key (id);
	END IF;
END $$;

/*reg_992_quant_pk 306*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_diagnostics') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_diagnostics') THEN
			ALTER TABLE core_diagnostics DROP CONSTRAINT IF EXISTS reg_992_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_992_quant_pk;
		alter table CORE_DIAGNOSTICS add constraint reg_992_quant_pk primary key (id);
	END IF;
END $$;

/*core_diagnostics_action_idx 307*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_diagnostics') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_diagnostics') THEN
			ALTER TABLE core_diagnostics DROP CONSTRAINT IF EXISTS core_diagnostics_action_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_diagnostics_action_idx;
		CREATE INDEX core_diagnostics_action_idx ON public.core_diagnostics USING btree (action_date);
	END IF;
END $$;

/*reg_935_quant_pk 308*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_layout_details') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_layout_details') THEN
			ALTER TABLE core_layout_details DROP CONSTRAINT IF EXISTS reg_935_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_935_quant_pk;
		alter table CORE_LAYOUT_DETAILS add constraint reg_935_quant_pk primary key (id);
	END IF;
END $$;

/*reg_112_q_pk 309*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_price_correction_by_stage_history') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_price_correction_by_stage_history') THEN
			ALTER TABLE market_price_correction_by_stage_history DROP CONSTRAINT IF EXISTS reg_112_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_112_q_pk;
		alter table market_price_correction_by_stage_history add constraint reg_112_q_pk primary key (id);
	END IF;
END $$;

/*reg_921_quant_pk 310*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_list_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_list_object') THEN
			ALTER TABLE core_list_object DROP CONSTRAINT IF EXISTS reg_921_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_921_quant_pk;
		alter table CORE_LIST_OBJECT add constraint reg_921_quant_pk primary key (id);
	END IF;
END $$;

/*reg_920_quant_pk 311*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_list') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_list') THEN
			ALTER TABLE core_list DROP CONSTRAINT IF EXISTS reg_920_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_920_quant_pk;
		alter table CORE_LIST add constraint reg_920_quant_pk primary key (id);
	END IF;
END $$;

/*reg_977_quant_pk 312*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_long_process_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_long_process_log') THEN
			ALTER TABLE core_long_process_log DROP CONSTRAINT IF EXISTS reg_977_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_977_quant_pk;
		alter table CORE_LONG_PROCESS_LOG add constraint reg_977_quant_pk primary key (id);
	END IF;
END $$;

/*reg_975_quant_pk 313*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_long_process_queue') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_long_process_queue') THEN
			ALTER TABLE core_long_process_queue DROP CONSTRAINT IF EXISTS reg_975_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_975_quant_pk;
		alter table CORE_LONG_PROCESS_QUEUE add constraint reg_975_quant_pk primary key (id);
	END IF;
END $$;

/*reg_936_quant_pk 314*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_qry') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_qry') THEN
			ALTER TABLE core_qry DROP CONSTRAINT IF EXISTS reg_936_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_936_quant_pk;
		alter table CORE_QRY add constraint reg_936_quant_pk primary key (qryid);
	END IF;
END $$;

/*reg_938_quant_pk 315*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_qry_operation') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_qry_operation') THEN
			ALTER TABLE core_qry_operation DROP CONSTRAINT IF EXISTS reg_938_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_938_quant_pk;
		alter table CORE_QRY_OPERATION add constraint reg_938_quant_pk primary key (qryoperationid);
	END IF;
END $$;

/*reg_984_quant_pk 316*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_reference_relation') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_reference_relation') THEN
			ALTER TABLE core_reference_relation DROP CONSTRAINT IF EXISTS reg_984_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_984_quant_pk;
		alter table CORE_REFERENCE_RELATION add constraint reg_984_quant_pk primary key (relid);
	END IF;
END $$;

/*reg_976_quant_pk 317*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_long_process_type') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_long_process_type') THEN
			ALTER TABLE core_long_process_type DROP CONSTRAINT IF EXISTS reg_976_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_976_quant_pk;
		alter table CORE_LONG_PROCESS_TYPE add constraint reg_976_quant_pk primary key (id);
	END IF;
END $$;

/*reg_950_quant_pk 318*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_user') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_user') THEN
			ALTER TABLE core_srd_user DROP CONSTRAINT IF EXISTS reg_950_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_950_quant_pk;
		alter table CORE_SRD_USER add constraint reg_950_quant_pk primary key (id);
	END IF;
END $$;

/*core_srd_user_username_key 319*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_user') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_user') THEN
			ALTER TABLE core_srd_user DROP CONSTRAINT IF EXISTS core_srd_user_username_key RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_srd_user_username_key;
		CREATE UNIQUE INDEX core_srd_user_username_key ON public.core_srd_user USING btree (username);
	END IF;
END $$;

/*reg_985_quant_pk 320*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_reference_tree') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_reference_tree') THEN
			ALTER TABLE core_reference_tree DROP CONSTRAINT IF EXISTS reg_985_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_985_quant_pk;
		alter table CORE_REFERENCE_TREE add constraint reg_985_quant_pk primary key (id);
	END IF;
END $$;

/*reg_982_quant_pk 321*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_reference') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_reference') THEN
			ALTER TABLE core_reference DROP CONSTRAINT IF EXISTS reg_982_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_982_quant_pk;
		alter table CORE_REFERENCE add constraint reg_982_quant_pk primary key (referenceid);
	END IF;
END $$;

/*reg_940_quant_pk 322*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_audit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_audit') THEN
			ALTER TABLE core_srd_audit DROP CONSTRAINT IF EXISTS reg_940_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_940_quant_pk;
		alter table CORE_SRD_AUDIT add constraint reg_940_quant_pk primary key (id);
	END IF;
END $$;

/*core_regnom_repository_pkey 323*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_regnom_repository') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_regnom_repository') THEN
			ALTER TABLE core_regnom_repository DROP CONSTRAINT IF EXISTS core_regnom_repository_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_regnom_repository_pkey;
		alter table CORE_REGNOM_REPOSITORY add constraint core_regnom_repository_pkey primary key (id);
	END IF;
END $$;

/*reg_932_quant_pk 324*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_register_relation') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_register_relation') THEN
			ALTER TABLE core_register_relation DROP CONSTRAINT IF EXISTS reg_932_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_932_quant_pk;
		alter table CORE_REGISTER_RELATION add constraint reg_932_quant_pk primary key (id);
	END IF;
END $$;

/*reg_941_quant_pk 325*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_department') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_department') THEN
			ALTER TABLE core_srd_department DROP CONSTRAINT IF EXISTS reg_941_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_941_quant_pk;
		alter table CORE_SRD_DEPARTMENT add constraint reg_941_quant_pk primary key (id);
	END IF;
END $$;

/*core_srd_function_idx 326*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_function') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_function') THEN
			ALTER TABLE core_srd_function DROP CONSTRAINT IF EXISTS core_srd_function_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_srd_function_idx;
		CREATE UNIQUE INDEX core_srd_function_idx ON public.core_srd_function USING btree (functiontag);
	END IF;
END $$;

/*reg_942_quant_pk 327*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_function') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_function') THEN
			ALTER TABLE core_srd_function DROP CONSTRAINT IF EXISTS reg_942_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_942_quant_pk;
		alter table CORE_SRD_FUNCTION add constraint reg_942_quant_pk primary key (id);
	END IF;
END $$;

/*reg_948_quant_pk 328*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_role_attr') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_role_attr') THEN
			ALTER TABLE core_srd_role_attr DROP CONSTRAINT IF EXISTS reg_948_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_948_quant_pk;
		alter table CORE_SRD_ROLE_ATTR add constraint reg_948_quant_pk primary key (id);
	END IF;
END $$;

/*reg_946_quant_pk 329*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_role_function') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_role_function') THEN
			ALTER TABLE core_srd_role_function DROP CONSTRAINT IF EXISTS reg_946_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_946_quant_pk;
		alter table CORE_SRD_ROLE_FUNCTION add constraint reg_946_quant_pk primary key (id);
	END IF;
END $$;

/*reg_947_quant_pk 330*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_role_register') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_role_register') THEN
			ALTER TABLE core_srd_role_register DROP CONSTRAINT IF EXISTS reg_947_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_947_quant_pk;
		alter table CORE_SRD_ROLE_REGISTER add constraint reg_947_quant_pk primary key (id);
	END IF;
END $$;

/*reg_945_quant_pk 331*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_role') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_role') THEN
			ALTER TABLE core_srd_role DROP CONSTRAINT IF EXISTS reg_945_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_945_quant_pk;
		alter table CORE_SRD_ROLE add constraint reg_945_quant_pk primary key (id);
	END IF;
END $$;

/*reg_956_quant_pk 332*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_layout_export') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_layout_export') THEN
			ALTER TABLE core_layout_export DROP CONSTRAINT IF EXISTS reg_956_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_956_quant_pk;
		alter table CORE_LAYOUT_EXPORT add constraint reg_956_quant_pk primary key (id);
	END IF;
END $$;

/*reg_954_quant_pk 333*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_usersettingslayout') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_usersettingslayout') THEN
			ALTER TABLE core_srd_usersettingslayout DROP CONSTRAINT IF EXISTS reg_954_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_954_quant_pk;
		alter table CORE_SRD_USERSETTINGSLAYOUT add constraint reg_954_quant_pk primary key (id);
	END IF;
END $$;

/*reg_969_quant_pk 334*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_attachments') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_attachments') THEN
			ALTER TABLE core_td_attachments DROP CONSTRAINT IF EXISTS reg_969_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_969_quant_pk;
		alter table CORE_TD_ATTACHMENTS add constraint reg_969_quant_pk primary key (id);
	END IF;
END $$;

/*common_data_form_storage_pkey 335*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_data_form_storage') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_data_form_storage') THEN
			ALTER TABLE common_data_form_storage DROP CONSTRAINT IF EXISTS common_data_form_storage_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS common_data_form_storage_pkey;
		alter table COMMON_DATA_FORM_STORAGE add constraint common_data_form_storage_pkey primary key (id);
	END IF;
END $$;

/*common_data_form_storage_unique_indx 336*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_data_form_storage') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_data_form_storage') THEN
			ALTER TABLE common_data_form_storage DROP CONSTRAINT IF EXISTS common_data_form_storage_unique_indx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS common_data_form_storage_unique_indx;
		CREATE UNIQUE INDEX common_data_form_storage_unique_indx ON public.common_data_form_storage USING btree (COALESCE(id_user, ('-1'::integer)::bigint), formtype, COALESCE(template_name, ''::character varying), COALESCE((is_common)::integer, 0));
	END IF;
END $$;

/*reg_964_quant_pk 337*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_changeset') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_changeset') THEN
			ALTER TABLE core_td_changeset DROP CONSTRAINT IF EXISTS reg_964_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_964_quant_pk;
		alter table CORE_TD_CHANGESET add constraint reg_964_quant_pk primary key (id);
	END IF;
END $$;

/*reg_951_quant_pk 338*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_usersettings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_usersettings') THEN
			ALTER TABLE core_srd_usersettings DROP CONSTRAINT IF EXISTS reg_951_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_951_quant_pk;
		alter table CORE_SRD_USERSETTINGS add constraint reg_951_quant_pk primary key (userid);
	END IF;
END $$;

/*reg_801_q_pk 339*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_import_data_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_import_data_log') THEN
			ALTER TABLE common_import_data_log DROP CONSTRAINT IF EXISTS reg_801_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_801_q_pk;
		alter table COMMON_IMPORT_DATA_LOG add constraint reg_801_q_pk primary key (id);
	END IF;
END $$;

/*common_import_data_log_object_id_index 340*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_import_data_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_import_data_log') THEN
			ALTER TABLE common_import_data_log DROP CONSTRAINT IF EXISTS common_import_data_log_object_id_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS common_import_data_log_object_id_index;
		CREATE INDEX common_import_data_log_object_id_index ON public.common_import_data_log USING btree (object_id);
	END IF;
END $$;

/*reg_967_quant_pk 341*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_audit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_audit') THEN
			ALTER TABLE core_td_audit DROP CONSTRAINT IF EXISTS reg_967_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_967_quant_pk;
		alter table CORE_TD_AUDIT add constraint reg_967_quant_pk primary key (id);
	END IF;
END $$;

/*reg_925_quant_pk 342*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_usersettingsregview') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_usersettingsregview') THEN
			ALTER TABLE core_srd_usersettingsregview DROP CONSTRAINT IF EXISTS reg_925_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_925_quant_pk;
		alter table CORE_SRD_USERSETTINGSREGVIEW add constraint reg_925_quant_pk primary key (id);
	END IF;
END $$;

/*reg_54578493_a_txt_pk 343*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_45_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_45_txt') THEN
			ALTER TABLE gbu_custom_source_45_txt DROP CONSTRAINT IF EXISTS reg_54578493_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578493_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578493_a_txt_pk ON public.gbu_custom_source_45_txt USING btree (id);
	END IF;
END $$;

/*reg_54578493_a_txt_inx_obj_attr_id 344*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_45_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_45_txt') THEN
			ALTER TABLE gbu_custom_source_45_txt DROP CONSTRAINT IF EXISTS reg_54578493_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578493_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578493_a_txt_inx_obj_attr_id ON public.gbu_custom_source_45_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_966_quant_pk 345*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_audit_action') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_audit_action') THEN
			ALTER TABLE core_td_audit_action DROP CONSTRAINT IF EXISTS reg_966_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_966_quant_pk;
		alter table CORE_TD_AUDIT_ACTION add constraint reg_966_quant_pk primary key (id);
	END IF;
END $$;

/*reg_965_quant_pk 346*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_change') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_change') THEN
			ALTER TABLE core_td_change DROP CONSTRAINT IF EXISTS reg_965_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_965_quant_pk;
		alter table CORE_TD_CHANGE add constraint reg_965_quant_pk primary key (id);
	END IF;
END $$;

/*reg_963_quant_pk 347*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_instance') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_instance') THEN
			ALTER TABLE core_td_instance DROP CONSTRAINT IF EXISTS reg_963_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_963_quant_pk;
		alter table CORE_TD_INSTANCE add constraint reg_963_quant_pk primary key (id);
	END IF;
END $$;

/*reg_997_quant_pk 348*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_updstru_log') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_updstru_log') THEN
			ALTER TABLE core_updstru_log DROP CONSTRAINT IF EXISTS reg_997_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_997_quant_pk;
		alter table CORE_UPDSTRU_LOG add constraint reg_997_quant_pk primary key (id);
	END IF;
END $$;

/*reg_850_quant_pk 349*/
DO $$
BEGIN
	IF (SELECT to_regclass('dashboards_dashboard') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='dashboards_dashboard') THEN
			ALTER TABLE dashboards_dashboard DROP CONSTRAINT IF EXISTS reg_850_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_850_quant_pk;
		alter table DASHBOARDS_DASHBOARD add constraint reg_850_quant_pk primary key (id);
	END IF;
END $$;

/*reg_851_quant_pk 350*/
DO $$
BEGIN
	IF (SELECT to_regclass('dashboards_panel') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='dashboards_panel') THEN
			ALTER TABLE dashboards_panel DROP CONSTRAINT IF EXISTS reg_851_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_851_quant_pk;
		alter table DASHBOARDS_PANEL add constraint reg_851_quant_pk primary key (id);
	END IF;
END $$;

/*reg_852_quant_pk 351*/
DO $$
BEGIN
	IF (SELECT to_regclass('dashboards_panel_type') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='dashboards_panel_type') THEN
			ALTER TABLE dashboards_panel_type DROP CONSTRAINT IF EXISTS reg_852_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_852_quant_pk;
		alter table DASHBOARDS_PANEL_TYPE add constraint reg_852_quant_pk primary key (id);
	END IF;
END $$;

/*reg_11_a_num_pk 352*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source11_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source11_a_num') THEN
			ALTER TABLE gbu_source11_a_num DROP CONSTRAINT IF EXISTS reg_11_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_11_a_num_pk;
		CREATE UNIQUE INDEX reg_11_a_num_pk ON public.gbu_source11_a_num USING btree (id);
	END IF;
END $$;

/*reg_11_a_num_inx_obj_attr_id 353*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source11_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source11_a_num') THEN
			ALTER TABLE gbu_source11_a_num DROP CONSTRAINT IF EXISTS reg_11_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_11_a_num_inx_obj_attr_id;
		CREATE INDEX reg_11_a_num_inx_obj_attr_id ON public.gbu_source11_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*fm_reports_savedreport_pkey 354*/
DO $$
BEGIN
	IF (SELECT to_regclass('fm_reports_savedreport') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='fm_reports_savedreport') THEN
			ALTER TABLE fm_reports_savedreport DROP CONSTRAINT IF EXISTS fm_reports_savedreport_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS fm_reports_savedreport_pkey;
		alter table FM_REPORTS_SAVEDREPORT add constraint fm_reports_savedreport_pkey primary key (id);
	END IF;
END $$;

/*reg_11_a_dt_pk 355*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source11_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source11_a_dt') THEN
			ALTER TABLE gbu_source11_a_dt DROP CONSTRAINT IF EXISTS reg_11_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_11_a_dt_pk;
		CREATE UNIQUE INDEX reg_11_a_dt_pk ON public.gbu_source11_a_dt USING btree (id);
	END IF;
END $$;

/*reg_11_a_dt_inx_obj_attr_id 356*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source11_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source11_a_dt') THEN
			ALTER TABLE gbu_source11_a_dt DROP CONSTRAINT IF EXISTS reg_11_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_11_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_11_a_dt_inx_obj_attr_id ON public.gbu_source11_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_11_a_txt_pk 357*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source11_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source11_a_txt') THEN
			ALTER TABLE gbu_source11_a_txt DROP CONSTRAINT IF EXISTS reg_11_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_11_a_txt_pk;
		CREATE UNIQUE INDEX reg_11_a_txt_pk ON public.gbu_source11_a_txt USING btree (id);
	END IF;
END $$;

/*reg_11_a_txt_inx_obj_attr_id 358*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source11_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source11_a_txt') THEN
			ALTER TABLE gbu_source11_a_txt DROP CONSTRAINT IF EXISTS reg_11_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_11_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_11_a_txt_inx_obj_attr_id ON public.gbu_source11_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_12_a_dt_pk 359*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source12_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source12_a_dt') THEN
			ALTER TABLE gbu_source12_a_dt DROP CONSTRAINT IF EXISTS reg_12_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_12_a_dt_pk;
		CREATE UNIQUE INDEX reg_12_a_dt_pk ON public.gbu_source12_a_dt USING btree (id);
	END IF;
END $$;

/*reg_12_a_dt_inx_obj_attr_id 360*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source12_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source12_a_dt') THEN
			ALTER TABLE gbu_source12_a_dt DROP CONSTRAINT IF EXISTS reg_12_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_12_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_12_a_dt_inx_obj_attr_id ON public.gbu_source12_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_12_a_num_pk 361*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source12_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source12_a_num') THEN
			ALTER TABLE gbu_source12_a_num DROP CONSTRAINT IF EXISTS reg_12_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_12_a_num_pk;
		CREATE UNIQUE INDEX reg_12_a_num_pk ON public.gbu_source12_a_num USING btree (id);
	END IF;
END $$;

/*reg_12_a_num_inx_obj_attr_id 362*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source12_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source12_a_num') THEN
			ALTER TABLE gbu_source12_a_num DROP CONSTRAINT IF EXISTS reg_12_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_12_a_num_inx_obj_attr_id;
		CREATE INDEX reg_12_a_num_inx_obj_attr_id ON public.gbu_source12_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_853_quant_pk 363*/
DO $$
BEGIN
	IF (SELECT to_regclass('dashboards_user_settings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='dashboards_user_settings') THEN
			ALTER TABLE dashboards_user_settings DROP CONSTRAINT IF EXISTS reg_853_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_853_quant_pk;
		alter table DASHBOARDS_USER_SETTINGS add constraint reg_853_quant_pk primary key (id);
	END IF;
END $$;

/*reg_10_a_dt_pk 364*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source10_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source10_a_dt') THEN
			ALTER TABLE gbu_source10_a_dt DROP CONSTRAINT IF EXISTS reg_10_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_10_a_dt_pk;
		CREATE UNIQUE INDEX reg_10_a_dt_pk ON public.gbu_source10_a_dt USING btree (id);
	END IF;
END $$;

/*reg_10_a_dt_inx_obj_attr_id 365*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source10_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source10_a_dt') THEN
			ALTER TABLE gbu_source10_a_dt DROP CONSTRAINT IF EXISTS reg_10_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_10_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_10_a_dt_inx_obj_attr_id ON public.gbu_source10_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_10_a_num_pk 366*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source10_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source10_a_num') THEN
			ALTER TABLE gbu_source10_a_num DROP CONSTRAINT IF EXISTS reg_10_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_10_a_num_pk;
		CREATE UNIQUE INDEX reg_10_a_num_pk ON public.gbu_source10_a_num USING btree (id);
	END IF;
END $$;

/*reg_10_a_num_inx_obj_attr_id 367*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source10_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source10_a_num') THEN
			ALTER TABLE gbu_source10_a_num DROP CONSTRAINT IF EXISTS reg_10_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_10_a_num_inx_obj_attr_id;
		CREATE INDEX reg_10_a_num_inx_obj_attr_id ON public.gbu_source10_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_13_a_dt_pk 368*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source13_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source13_a_dt') THEN
			ALTER TABLE gbu_source13_a_dt DROP CONSTRAINT IF EXISTS reg_13_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_13_a_dt_pk;
		CREATE UNIQUE INDEX reg_13_a_dt_pk ON public.gbu_source13_a_dt USING btree (id);
	END IF;
END $$;

/*reg_13_a_dt_inx_obj_attr_id 369*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source13_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source13_a_dt') THEN
			ALTER TABLE gbu_source13_a_dt DROP CONSTRAINT IF EXISTS reg_13_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_13_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_13_a_dt_inx_obj_attr_id ON public.gbu_source13_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_13_a_num_pk 370*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source13_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source13_a_num') THEN
			ALTER TABLE gbu_source13_a_num DROP CONSTRAINT IF EXISTS reg_13_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_13_a_num_pk;
		CREATE UNIQUE INDEX reg_13_a_num_pk ON public.gbu_source13_a_num USING btree (id);
	END IF;
END $$;

/*reg_13_a_num_inx_obj_attr_id 371*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source13_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source13_a_num') THEN
			ALTER TABLE gbu_source13_a_num DROP CONSTRAINT IF EXISTS reg_13_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_13_a_num_inx_obj_attr_id;
		CREATE INDEX reg_13_a_num_inx_obj_attr_id ON public.gbu_source13_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_15_a_num_pk 372*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source15_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source15_a_num') THEN
			ALTER TABLE gbu_source15_a_num DROP CONSTRAINT IF EXISTS reg_15_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_15_a_num_pk;
		CREATE UNIQUE INDEX reg_15_a_num_pk ON public.gbu_source15_a_num USING btree (id);
	END IF;
END $$;

/*reg_15_a_num_inx_obj_attr_id 373*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source15_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source15_a_num') THEN
			ALTER TABLE gbu_source15_a_num DROP CONSTRAINT IF EXISTS reg_15_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_15_a_num_inx_obj_attr_id;
		CREATE INDEX reg_15_a_num_inx_obj_attr_id ON public.gbu_source15_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_41935287_q_pk 374*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_36_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_36_q') THEN
			ALTER TABLE source_36_q DROP CONSTRAINT IF EXISTS reg_41935287_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41935287_q_pk;
		CREATE UNIQUE INDEX reg_41935287_q_pk ON public.source_36_q USING btree (id);
	END IF;
END $$;

/*reg_937_quant_pk 375*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_qry_filter') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_qry_filter') THEN
			ALTER TABLE core_qry_filter DROP CONSTRAINT IF EXISTS reg_937_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_937_quant_pk;
		alter table CORE_QRY_FILTER add constraint reg_937_quant_pk primary key (qryfilterid);
	END IF;
END $$;

/*reg_37312256_q_pk 376*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors37294645') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors37294645') THEN
			ALTER TABLE ko_touroksfactors37294645 DROP CONSTRAINT IF EXISTS reg_37312256_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_37312256_q_pk;
		CREATE UNIQUE INDEX reg_37312256_q_pk ON public.ko_touroksfactors37294645 USING btree (id);
	END IF;
END $$;

/*reg_41619124_q_pk 377*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_29_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_29_q') THEN
			ALTER TABLE source_29_q DROP CONSTRAINT IF EXISTS reg_41619124_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41619124_q_pk;
		CREATE UNIQUE INDEX reg_41619124_q_pk ON public.source_29_q USING btree (id);
	END IF;
END $$;

/*reg_41937379_a_txt_pk 378*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_22_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_22_txt') THEN
			ALTER TABLE gbu_custom_source_22_txt DROP CONSTRAINT IF EXISTS reg_41937379_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41937379_a_txt_pk;
		CREATE UNIQUE INDEX reg_41937379_a_txt_pk ON public.gbu_custom_source_22_txt USING btree (id);
	END IF;
END $$;

/*reg_41937379_a_txt_inx_obj_attr_id 379*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_22_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_22_txt') THEN
			ALTER TABLE gbu_custom_source_22_txt DROP CONSTRAINT IF EXISTS reg_41937379_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41937379_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_41937379_a_txt_inx_obj_attr_id ON public.gbu_custom_source_22_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_41707106_q_pk 380*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors41707102') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors41707102') THEN
			ALTER TABLE ko_touroksfactors41707102 DROP CONSTRAINT IF EXISTS reg_41707106_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41707106_q_pk;
		CREATE UNIQUE INDEX reg_41707106_q_pk ON public.ko_touroksfactors41707102 USING btree (id);
	END IF;
END $$;

/*reg_42726724_a_txt_pk 381*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_26_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_26_txt') THEN
			ALTER TABLE gbu_custom_source_26_txt DROP CONSTRAINT IF EXISTS reg_42726724_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42726724_a_txt_pk;
		CREATE UNIQUE INDEX reg_42726724_a_txt_pk ON public.gbu_custom_source_26_txt USING btree (id);
	END IF;
END $$;

/*reg_42726724_a_txt_inx_obj_attr_id 382*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_26_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_26_txt') THEN
			ALTER TABLE gbu_custom_source_26_txt DROP CONSTRAINT IF EXISTS reg_42726724_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42726724_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_42726724_a_txt_inx_obj_attr_id ON public.gbu_custom_source_26_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_205_q_pk 383*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_group') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_group') THEN
			ALTER TABLE ko_group DROP CONSTRAINT IF EXISTS reg_205_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_205_q_pk;
		alter table KO_GROUP add constraint reg_205_q_pk primary key (id);
	END IF;
END $$;

/*reg_111_q_pk 384*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_coefficient_for_rooms_correction') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_coefficient_for_rooms_correction') THEN
			ALTER TABLE market_coefficient_for_rooms_correction DROP CONSTRAINT IF EXISTS reg_111_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_111_q_pk;
		alter table market_coefficient_for_rooms_correction add constraint reg_111_q_pk primary key (id);
	END IF;
END $$;

/*correction_by_rooms_history_index 385*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_coefficient_for_rooms_correction') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_coefficient_for_rooms_correction') THEN
			ALTER TABLE market_coefficient_for_rooms_correction DROP CONSTRAINT IF EXISTS correction_by_rooms_history_index RESTRICT;
		END IF;
		DROP INDEX IF EXISTS correction_by_rooms_history_index;
		CREATE UNIQUE INDEX correction_by_rooms_history_index ON public.market_coefficient_for_rooms_correction USING btree (building_cadastral_number, changing_date, market_segment_code);
	END IF;
END $$;

/*reg_939_quant_pk 386*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_register_lock') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_register_lock') THEN
			ALTER TABLE core_register_lock DROP CONSTRAINT IF EXISTS reg_939_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_939_quant_pk;
		alter table CORE_REGISTER_LOCK add constraint reg_939_quant_pk primary key (id);
	END IF;
END $$;

/*gbu_source2_a_txt_bkp_pkey 387*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_txt_bkp') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_txt_bkp') THEN
			ALTER TABLE gbu_source2_a_txt_bkp DROP CONSTRAINT IF EXISTS gbu_source2_a_txt_bkp_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS gbu_source2_a_txt_bkp_pkey;
		CREATE UNIQUE INDEX gbu_source2_a_txt_bkp_pkey ON public.gbu_source2_a_txt_bkp USING btree (id);
	END IF;
END $$;

/*gbu_source2_a_txt_bkp_object_id_attribute_id_idx 388*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_txt_bkp') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_txt_bkp') THEN
			ALTER TABLE gbu_source2_a_txt_bkp DROP CONSTRAINT IF EXISTS gbu_source2_a_txt_bkp_object_id_attribute_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS gbu_source2_a_txt_bkp_object_id_attribute_id_idx;
		CREATE INDEX gbu_source2_a_txt_bkp_object_id_attribute_id_idx ON public.gbu_source2_a_txt_bkp USING btree (object_id, attribute_id);
	END IF;
END $$;

/*gbu_source2_a_txt_bkp_attribute_id_idx 389*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_txt_bkp') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_txt_bkp') THEN
			ALTER TABLE gbu_source2_a_txt_bkp DROP CONSTRAINT IF EXISTS gbu_source2_a_txt_bkp_attribute_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS gbu_source2_a_txt_bkp_attribute_id_idx;
		CREATE INDEX gbu_source2_a_txt_bkp_attribute_id_idx ON public.gbu_source2_a_txt_bkp USING btree (attribute_id);
	END IF;
END $$;

/*ko_transfer_attributes_pkey 390*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_transfer_attributes') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_transfer_attributes') THEN
			ALTER TABLE ko_transfer_attributes DROP CONSTRAINT IF EXISTS ko_transfer_attributes_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_transfer_attributes_pkey;
		alter table KO_TRANSFER_ATTRIBUTES add constraint ko_transfer_attributes_pkey primary key (id);
	END IF;
END $$;

/*reg_930_quant_pk 391*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_register') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_register') THEN
			ALTER TABLE core_register DROP CONSTRAINT IF EXISTS reg_930_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_930_quant_pk;
		alter table CORE_REGISTER add constraint reg_930_quant_pk primary key (registerid);
	END IF;
END $$;

/*reg_2_a_1__pk 392*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_1') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_1') THEN
			ALTER TABLE gbu_source2_a_1 DROP CONSTRAINT IF EXISTS reg_2_a_1__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_1__pk;
		CREATE UNIQUE INDEX reg_2_a_1__pk ON public.gbu_source2_a_1 USING btree (id);
	END IF;
END $$;

/*reg_2_a_1_inx_obj_attr_id 393*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_1') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_1') THEN
			ALTER TABLE gbu_source2_a_1 DROP CONSTRAINT IF EXISTS reg_2_a_1_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_1_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_1_inx_obj_attr_id ON public.gbu_source2_a_1 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_41707143_q_pk 394*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors41707139') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors41707139') THEN
			ALTER TABLE ko_touroksfactors41707139 DROP CONSTRAINT IF EXISTS reg_41707143_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41707143_q_pk;
		CREATE UNIQUE INDEX reg_41707143_q_pk ON public.ko_touroksfactors41707139 USING btree (id);
	END IF;
END $$;

/*reg_41891427_q_pk 395*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_32_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_32_q') THEN
			ALTER TABLE source_32_q DROP CONSTRAINT IF EXISTS reg_41891427_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41891427_q_pk;
		CREATE UNIQUE INDEX reg_41891427_q_pk ON public.source_32_q USING btree (id);
	END IF;
END $$;

/*reg_29504581_q_pk 396*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors29425150') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors29425150') THEN
			ALTER TABLE ko_touroksfactors29425150 DROP CONSTRAINT IF EXISTS reg_29504581_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29504581_q_pk;
		CREATE UNIQUE INDEX reg_29504581_q_pk ON public.ko_touroksfactors29425150 USING btree (id);
	END IF;
END $$;

/*reg_41937379_a_dt_pk 397*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_22_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_22_dt') THEN
			ALTER TABLE gbu_custom_source_22_dt DROP CONSTRAINT IF EXISTS reg_41937379_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41937379_a_dt_pk;
		CREATE UNIQUE INDEX reg_41937379_a_dt_pk ON public.gbu_custom_source_22_dt USING btree (id);
	END IF;
END $$;

/*reg_41937379_a_dt_inx_obj_attr_id 398*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_22_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_22_dt') THEN
			ALTER TABLE gbu_custom_source_22_dt DROP CONSTRAINT IF EXISTS reg_41937379_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41937379_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_41937379_a_dt_inx_obj_attr_id ON public.gbu_custom_source_22_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_41707675_q_pk 399*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors41707674') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors41707674') THEN
			ALTER TABLE ko_tourzufactors41707674 DROP CONSTRAINT IF EXISTS reg_41707675_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41707675_q_pk;
		CREATE UNIQUE INDEX reg_41707675_q_pk ON public.ko_tourzufactors41707674 USING btree (id);
	END IF;
END $$;

/*reg_7_a_txt_pk 400*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source7_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source7_a_txt') THEN
			ALTER TABLE gbu_source7_a_txt DROP CONSTRAINT IF EXISTS reg_7_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_7_a_txt_pk;
		CREATE UNIQUE INDEX reg_7_a_txt_pk ON public.gbu_source7_a_txt USING btree (id);
	END IF;
END $$;

/*reg_7_a_txt_inx_obj_attr_id 401*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source7_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source7_a_txt') THEN
			ALTER TABLE gbu_source7_a_txt DROP CONSTRAINT IF EXISTS reg_7_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_7_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_7_a_txt_inx_obj_attr_id ON public.gbu_source7_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_206_q_pk 402*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_model') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_model') THEN
			ALTER TABLE ko_model DROP CONSTRAINT IF EXISTS reg_206_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_206_q_pk;
		alter table KO_MODEL add constraint reg_206_q_pk primary key (id);
	END IF;
END $$;

/*ko_model_unique_constraint 403*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_model') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_model') THEN
			ALTER TABLE ko_model DROP CONSTRAINT IF EXISTS ko_model_unique_constraint RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_model_unique_constraint;
		CREATE UNIQUE INDEX ko_model_unique_constraint ON public.ko_model USING btree (btrim(lower((name)::text)));
	END IF;
END $$;

/*reg_250_q_pk 404*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_params_oks_2018') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_params_oks_2018') THEN
			ALTER TABLE ko_unit_params_oks_2018 DROP CONSTRAINT IF EXISTS reg_250_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_250_q_pk;
		alter table KO_UNIT_PARAMS_OKS_2018 add constraint reg_250_q_pk primary key (id);
	END IF;
END $$;

/*reg_107_q_pk 405*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_region_dictionaty') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_region_dictionaty') THEN
			ALTER TABLE market_region_dictionaty DROP CONSTRAINT IF EXISTS reg_107_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_107_q_pk;
		alter table market_region_dictionaty add constraint reg_107_q_pk primary key (id);
	END IF;
END $$;

/*reg_8_a_num_pk 406*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source8_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source8_a_num') THEN
			ALTER TABLE gbu_source8_a_num DROP CONSTRAINT IF EXISTS reg_8_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_8_a_num_pk;
		CREATE UNIQUE INDEX reg_8_a_num_pk ON public.gbu_source8_a_num USING btree (id);
	END IF;
END $$;

/*reg_8_a_num_inx_obj_attr_id 407*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source8_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source8_a_num') THEN
			ALTER TABLE gbu_source8_a_num DROP CONSTRAINT IF EXISTS reg_8_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_8_a_num_inx_obj_attr_id;
		CREATE INDEX reg_8_a_num_inx_obj_attr_id ON public.gbu_source8_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_308_q_pk 408*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_otchet') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_otchet') THEN
			ALTER TABLE sud_otchet DROP CONSTRAINT IF EXISTS reg_308_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_308_q_pk;
		alter table SUD_OTCHET add constraint reg_308_q_pk primary key (id);
	END IF;
END $$;

/*sud_otchet_a_pkey 409*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_otchet_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_otchet_a') THEN
			ALTER TABLE sud_otchet_a DROP CONSTRAINT IF EXISTS sud_otchet_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_otchet_a_pkey;
		alter table SUD_OTCHET_A add constraint sud_otchet_a_pkey primary key (id);
	END IF;
END $$;

/*sud_otchet_a_obj_attr_idx 410*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_otchet_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_otchet_a') THEN
			ALTER TABLE sud_otchet_a DROP CONSTRAINT IF EXISTS sud_otchet_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_otchet_a_obj_attr_idx;
		CREATE INDEX sud_otchet_a_obj_attr_idx ON public.sud_otchet_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_501_q_pk 411*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_declaration') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_declaration') THEN
			ALTER TABLE declarations_declaration DROP CONSTRAINT IF EXISTS reg_501_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_501_q_pk;
		alter table DECLARATIONS_DECLARATION add constraint reg_501_q_pk primary key (id);
	END IF;
END $$;

/*reg_106_q_pk 412*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_duplicates_history') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_duplicates_history') THEN
			ALTER TABLE market_duplicates_history DROP CONSTRAINT IF EXISTS reg_106_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_106_q_pk;
		alter table market_duplicates_history add constraint reg_106_q_pk primary key (id);
	END IF;
END $$;

/*sud_sud_a_pkey 413*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_sud_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_sud_a') THEN
			ALTER TABLE sud_sud_a DROP CONSTRAINT IF EXISTS sud_sud_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_sud_a_pkey;
		alter table SUD_SUD_A add constraint sud_sud_a_pkey primary key (id);
	END IF;
END $$;

/*sud_sud_a_obj_attr_idx 414*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_sud_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_sud_a') THEN
			ALTER TABLE sud_sud_a DROP CONSTRAINT IF EXISTS sud_sud_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_sud_a_obj_attr_idx;
		CREATE INDEX sud_sud_a_obj_attr_idx ON public.sud_sud_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_316_q_pk 415*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_sud') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_sud') THEN
			ALTER TABLE sud_sud DROP CONSTRAINT IF EXISTS reg_316_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_316_q_pk;
		alter table SUD_SUD add constraint reg_316_q_pk primary key (id);
	END IF;
END $$;

/*tmp_attr_8_partition_idx 416*/
DO $$
BEGIN
	IF (SELECT to_regclass('tmp_attr_8_partition') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='tmp_attr_8_partition') THEN
			ALTER TABLE tmp_attr_8_partition DROP CONSTRAINT IF EXISTS tmp_attr_8_partition_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS tmp_attr_8_partition_idx;
		CREATE INDEX tmp_attr_8_partition_idx ON public.tmp_attr_8_partition USING btree (object_id, ot);
	END IF;
END $$;

/*reg_995_quant_pk 417*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_regnom_sequences') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_regnom_sequences') THEN
			ALTER TABLE core_regnom_sequences DROP CONSTRAINT IF EXISTS reg_995_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_995_quant_pk;
		alter table CORE_REGNOM_SEQUENCES add constraint reg_995_quant_pk primary key (id);
	END IF;
END $$;

/*reg_955_quant_pk 418*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_role_filter') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_role_filter') THEN
			ALTER TABLE core_srd_role_filter DROP CONSTRAINT IF EXISTS reg_955_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_955_quant_pk;
		alter table CORE_SRD_ROLE_FILTER add constraint reg_955_quant_pk primary key (id);
	END IF;
END $$;

/*reg_304_q_pk 419*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_otchetlink') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_otchetlink') THEN
			ALTER TABLE sud_otchetlink DROP CONSTRAINT IF EXISTS reg_304_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_304_q_pk;
		alter table SUD_OTCHETLINK add constraint reg_304_q_pk primary key (id);
	END IF;
END $$;

/*reg_311_q_pk 420*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_zaklinkstatus') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_zaklinkstatus') THEN
			ALTER TABLE sud_zaklinkstatus DROP CONSTRAINT IF EXISTS reg_311_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_311_q_pk;
		alter table SUD_ZAKLINKSTATUS add constraint reg_311_q_pk primary key (id);
	END IF;
END $$;

/*reg_317_q_pk 421*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_param') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_param') THEN
			ALTER TABLE sud_param DROP CONSTRAINT IF EXISTS reg_317_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_317_q_pk;
		alter table SUD_PARAM add constraint reg_317_q_pk primary key (pid);
	END IF;
END $$;

/*reg317_q_id_idx 422*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_param') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_param') THEN
			ALTER TABLE sud_param DROP CONSTRAINT IF EXISTS reg317_q_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg317_q_id_idx;
		CREATE INDEX reg317_q_id_idx ON public.sud_param USING btree (id);
	END IF;
END $$;

/*reg_312_q_pk 423*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_zakstatus') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_zakstatus') THEN
			ALTER TABLE sud_zakstatus DROP CONSTRAINT IF EXISTS reg_312_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_312_q_pk;
		alter table SUD_ZAKSTATUS add constraint reg_312_q_pk primary key (id);
	END IF;
END $$;

/*reg_313_q_pk 424*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_dict') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_dict') THEN
			ALTER TABLE sud_dict DROP CONSTRAINT IF EXISTS reg_313_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_313_q_pk;
		alter table SUD_DICT add constraint reg_313_q_pk primary key (id);
	END IF;
END $$;

/*reg_314_q_pk 425*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_sudlink') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_sudlink') THEN
			ALTER TABLE sud_sudlink DROP CONSTRAINT IF EXISTS reg_314_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_314_q_pk;
		alter table SUD_SUDLINK add constraint reg_314_q_pk primary key (id);
	END IF;
END $$;

/*sud_sudlink_obj_sud_idx 426*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_sudlink') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_sudlink') THEN
			ALTER TABLE sud_sudlink DROP CONSTRAINT IF EXISTS sud_sudlink_obj_sud_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_sudlink_obj_sud_idx;
		CREATE INDEX sud_sudlink_obj_sud_idx ON public.sud_sudlink USING btree (id_object, id_sud);
	END IF;
END $$;

/*reg_400_q_pk 427*/
DO $$
BEGIN
	IF (SELECT to_regclass('comission_cost') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='comission_cost') THEN
			ALTER TABLE comission_cost DROP CONSTRAINT IF EXISTS reg_400_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_400_q_pk;
		alter table COMISSION_COST add constraint reg_400_q_pk primary key (id);
	END IF;
END $$;

/*reg_505_q_pk 428*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_subject') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_subject') THEN
			ALTER TABLE declarations_subject DROP CONSTRAINT IF EXISTS reg_505_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_505_q_pk;
		alter table DECLARATIONS_SUBJECT add constraint reg_505_q_pk primary key (id);
	END IF;
END $$;

/*reg_212_q_pk 429*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tour_groups') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tour_groups') THEN
			ALTER TABLE ko_tour_groups DROP CONSTRAINT IF EXISTS reg_212_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_212_q_pk;
		alter table KO_TOUR_GROUPS add constraint reg_212_q_pk primary key (id);
	END IF;
END $$;

/*reg_213_q_pk 430*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_attribute_map') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_attribute_map') THEN
			ALTER TABLE ko_attribute_map DROP CONSTRAINT IF EXISTS reg_213_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_213_q_pk;
		alter table KO_ATTRIBUTE_MAP add constraint reg_213_q_pk primary key (id);
	END IF;
END $$;

/*reg_47948760_q_pk 431*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors47945336') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors47945336') THEN
			ALTER TABLE ko_tourzufactors47945336 DROP CONSTRAINT IF EXISTS reg_47948760_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47948760_q_pk;
		CREATE UNIQUE INDEX reg_47948760_q_pk ON public.ko_tourzufactors47945336 USING btree (id);
	END IF;
END $$;

/*reg_252_q_pk 432*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_params_oks_2016') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_params_oks_2016') THEN
			ALTER TABLE ko_unit_params_oks_2016 DROP CONSTRAINT IF EXISTS reg_252_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_252_q_pk;
		alter table KO_UNIT_PARAMS_OKS_2016 add constraint reg_252_q_pk primary key (id);
	END IF;
END $$;

/*gbu_kadastr_kvartal_pkey 433*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_kadastr_kvartal') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_kadastr_kvartal') THEN
			ALTER TABLE gbu_kadastr_kvartal DROP CONSTRAINT IF EXISTS gbu_kadastr_kvartal_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS gbu_kadastr_kvartal_pkey;
		alter table GBU_KADASTR_KVARTAL add constraint gbu_kadastr_kvartal_pkey primary key (id);
	END IF;
END $$;

/*gbu_kadastr_kvartal_kadastr_kvartal_key 434*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_kadastr_kvartal') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_kadastr_kvartal') THEN
			ALTER TABLE gbu_kadastr_kvartal DROP CONSTRAINT IF EXISTS gbu_kadastr_kvartal_kadastr_kvartal_key RESTRICT;
		END IF;
		DROP INDEX IF EXISTS gbu_kadastr_kvartal_kadastr_kvartal_key;
		CREATE UNIQUE INDEX gbu_kadastr_kvartal_kadastr_kvartal_key ON public.gbu_kadastr_kvartal USING btree (kadastr_kvartal);
	END IF;
END $$;

/*reg_504_q_pk 435*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_result') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_result') THEN
			ALTER TABLE declarations_result DROP CONSTRAINT IF EXISTS reg_504_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_504_q_pk;
		alter table DECLARATIONS_RESULT add constraint reg_504_q_pk primary key (declaration_id);
	END IF;
END $$;

/*reg_215_q_pk 436*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_cod_job') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_cod_job') THEN
			ALTER TABLE ko_cod_job DROP CONSTRAINT IF EXISTS reg_215_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_215_q_pk;
		alter table KO_COD_JOB add constraint reg_215_q_pk primary key (id);
	END IF;
END $$;

/*sud_zak_a_pkey 437*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_zak_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_zak_a') THEN
			ALTER TABLE sud_zak_a DROP CONSTRAINT IF EXISTS sud_zak_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_zak_a_pkey;
		alter table SUD_ZAK_A add constraint sud_zak_a_pkey primary key (id);
	END IF;
END $$;

/*sud_zak_a_obj_attr_idx 438*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_zak_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_zak_a') THEN
			ALTER TABLE sud_zak_a DROP CONSTRAINT IF EXISTS sud_zak_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS sud_zak_a_obj_attr_idx;
		CREATE INDEX sud_zak_a_obj_attr_idx ON public.sud_zak_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_500_q_pk 439*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_book') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_book') THEN
			ALTER TABLE declarations_book DROP CONSTRAINT IF EXISTS reg_500_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_500_q_pk;
		alter table DECLARATIONS_BOOK add constraint reg_500_q_pk primary key (id);
	END IF;
END $$;

/*declarations_book_a_pkey 440*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_book_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_book_a') THEN
			ALTER TABLE declarations_book_a DROP CONSTRAINT IF EXISTS declarations_book_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_book_a_pkey;
		alter table DECLARATIONS_BOOK_A add constraint declarations_book_a_pkey primary key (id);
	END IF;
END $$;

/*declarations_book_a_obj_attr_idx 441*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_book_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_book_a') THEN
			ALTER TABLE declarations_book_a DROP CONSTRAINT IF EXISTS declarations_book_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_book_a_obj_attr_idx;
		CREATE INDEX declarations_book_a_obj_attr_idx ON public.declarations_book_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*declarations_har_parcel_a_pkey 442*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_parcel_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_parcel_a') THEN
			ALTER TABLE declarations_har_parcel_a DROP CONSTRAINT IF EXISTS declarations_har_parcel_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_har_parcel_a_pkey;
		alter table DECLARATIONS_HAR_PARCEL_A add constraint declarations_har_parcel_a_pkey primary key (id);
	END IF;
END $$;

/*declarations_har_parcel_a_obj_attr_idx 443*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_parcel_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_parcel_a') THEN
			ALTER TABLE declarations_har_parcel_a DROP CONSTRAINT IF EXISTS declarations_har_parcel_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_har_parcel_a_obj_attr_idx;
		CREATE INDEX declarations_har_parcel_a_obj_attr_idx ON public.declarations_har_parcel_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*declarations_subject_a_pkey 444*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_subject_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_subject_a') THEN
			ALTER TABLE declarations_subject_a DROP CONSTRAINT IF EXISTS declarations_subject_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_subject_a_pkey;
		alter table DECLARATIONS_SUBJECT_A add constraint declarations_subject_a_pkey primary key (id);
	END IF;
END $$;

/*declarations_subject_a_obj_attr_idx 445*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_subject_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_subject_a') THEN
			ALTER TABLE declarations_subject_a DROP CONSTRAINT IF EXISTS declarations_subject_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_subject_a_obj_attr_idx;
		CREATE INDEX declarations_subject_a_obj_attr_idx ON public.declarations_subject_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_507_q_pk 446*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_uved_rejection_reason_type') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_uved_rejection_reason_type') THEN
			ALTER TABLE declarations_uved_rejection_reason_type DROP CONSTRAINT IF EXISTS reg_507_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_507_q_pk;
		alter table DECLARATIONS_UVED_REJECTION_REASON_TYPE add constraint reg_507_q_pk primary key (id);
	END IF;
END $$;

/*declarations_uved_rej_reason_type_uved_id_idx 447*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_uved_rejection_reason_type') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_uved_rejection_reason_type') THEN
			ALTER TABLE declarations_uved_rejection_reason_type DROP CONSTRAINT IF EXISTS declarations_uved_rej_reason_type_uved_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_uved_rej_reason_type_uved_id_idx;
		CREATE INDEX declarations_uved_rej_reason_type_uved_id_idx ON public.declarations_uved_rejection_reason_type USING btree (uved_id);
	END IF;
END $$;

/*reg_507_q_unique 448*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_uved_rejection_reason_type') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_uved_rejection_reason_type') THEN
			ALTER TABLE declarations_uved_rejection_reason_type DROP CONSTRAINT IF EXISTS reg_507_q_unique RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_507_q_unique;
		CREATE UNIQUE INDEX reg_507_q_unique ON public.declarations_uved_rejection_reason_type USING btree (uved_id, rejection_reason_type);
	END IF;
END $$;

/*declarations_uved_a_pkey 449*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_uved_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_uved_a') THEN
			ALTER TABLE declarations_uved_a DROP CONSTRAINT IF EXISTS declarations_uved_a_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_uved_a_pkey;
		alter table DECLARATIONS_UVED_A add constraint declarations_uved_a_pkey primary key (id);
	END IF;
END $$;

/*declarations_uved_a_obj_attr_idx 450*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_uved_a') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_uved_a') THEN
			ALTER TABLE declarations_uved_a DROP CONSTRAINT IF EXISTS declarations_uved_a_obj_attr_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS declarations_uved_a_obj_attr_idx;
		CREATE INDEX declarations_uved_a_obj_attr_idx ON public.declarations_uved_a USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_506_q_pk 451*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_uved') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_uved') THEN
			ALTER TABLE declarations_uved DROP CONSTRAINT IF EXISTS reg_506_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_506_q_pk;
		alter table DECLARATIONS_UVED add constraint reg_506_q_pk primary key (id);
	END IF;
END $$;

/*reg_508_q_pk 452*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_signatory') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_signatory') THEN
			ALTER TABLE declarations_signatory DROP CONSTRAINT IF EXISTS reg_508_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_508_q_pk;
		alter table DECLARATIONS_SIGNATORY add constraint reg_508_q_pk primary key (id);
	END IF;
END $$;

/*reg_983_quant_pk 453*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_reference_item') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_reference_item') THEN
			ALTER TABLE core_reference_item DROP CONSTRAINT IF EXISTS reg_983_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_983_quant_pk;
		alter table CORE_REFERENCE_ITEM add constraint reg_983_quant_pk primary key (itemid);
	END IF;
END $$;

/*core_reference_item_org_pkey 454*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_reference_item_org') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_reference_item_org') THEN
			ALTER TABLE core_reference_item_org DROP CONSTRAINT IF EXISTS core_reference_item_org_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS core_reference_item_org_pkey;
		alter table CORE_REFERENCE_ITEM_ORG add constraint core_reference_item_org_pkey primary key (id);
	END IF;
END $$;

/*reg_108_q_pk 455*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_indexes_for_date_correction') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_indexes_for_date_correction') THEN
			ALTER TABLE market_indexes_for_date_correction DROP CONSTRAINT IF EXISTS reg_108_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_108_q_pk;
		alter table market_indexes_for_date_correction add constraint reg_108_q_pk primary key (id);
	END IF;
END $$;

/*market_indexes_for_date_correction_date_key 456*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_indexes_for_date_correction') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_indexes_for_date_correction') THEN
			ALTER TABLE market_indexes_for_date_correction DROP CONSTRAINT IF EXISTS market_indexes_for_date_correction_date_key RESTRICT;
		END IF;
		DROP INDEX IF EXISTS market_indexes_for_date_correction_date_key;
		CREATE UNIQUE INDEX market_indexes_for_date_correction_date_key ON public.market_indexes_for_date_correction USING btree (date, building_cadastral_number, market_segment_code);
	END IF;
END $$;

/*reg_13_a_txt_pk 457*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source13_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source13_a_txt') THEN
			ALTER TABLE gbu_source13_a_txt DROP CONSTRAINT IF EXISTS reg_13_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_13_a_txt_pk;
		CREATE UNIQUE INDEX reg_13_a_txt_pk ON public.gbu_source13_a_txt USING btree (id);
	END IF;
END $$;

/*reg_13_a_txt_inx_obj_attr_id 458*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source13_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source13_a_txt') THEN
			ALTER TABLE gbu_source13_a_txt DROP CONSTRAINT IF EXISTS reg_13_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_13_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_13_a_txt_inx_obj_attr_id ON public.gbu_source13_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_14_a_dt_pk 459*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source14_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source14_a_dt') THEN
			ALTER TABLE gbu_source14_a_dt DROP CONSTRAINT IF EXISTS reg_14_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_14_a_dt_pk;
		CREATE UNIQUE INDEX reg_14_a_dt_pk ON public.gbu_source14_a_dt USING btree (id);
	END IF;
END $$;

/*reg_14_a_dt_inx_obj_attr_id 460*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source14_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source14_a_dt') THEN
			ALTER TABLE gbu_source14_a_dt DROP CONSTRAINT IF EXISTS reg_14_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_14_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_14_a_dt_inx_obj_attr_id ON public.gbu_source14_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_14_a_num_pk 461*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source14_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source14_a_num') THEN
			ALTER TABLE gbu_source14_a_num DROP CONSTRAINT IF EXISTS reg_14_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_14_a_num_pk;
		CREATE UNIQUE INDEX reg_14_a_num_pk ON public.gbu_source14_a_num USING btree (id);
	END IF;
END $$;

/*reg_14_a_num_inx_obj_attr_id 462*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source14_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source14_a_num') THEN
			ALTER TABLE gbu_source14_a_num DROP CONSTRAINT IF EXISTS reg_14_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_14_a_num_inx_obj_attr_id;
		CREATE INDEX reg_14_a_num_inx_obj_attr_id ON public.gbu_source14_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_41707103_q_pk 463*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors41707102') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors41707102') THEN
			ALTER TABLE ko_tourzufactors41707102 DROP CONSTRAINT IF EXISTS reg_41707103_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41707103_q_pk;
		CREATE UNIQUE INDEX reg_41707103_q_pk ON public.ko_tourzufactors41707102 USING btree (id);
	END IF;
END $$;

/*reg_41935625_q_pk 464*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_37_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_37_q') THEN
			ALTER TABLE source_37_q DROP CONSTRAINT IF EXISTS reg_41935625_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41935625_q_pk;
		CREATE UNIQUE INDEX reg_41935625_q_pk ON public.source_37_q USING btree (id);
	END IF;
END $$;

/*reg_116_q_pk 465*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_price_after_correction_by_date_h') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_price_after_correction_by_date_h') THEN
			ALTER TABLE market_price_after_correction_by_date_h DROP CONSTRAINT IF EXISTS reg_116_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_116_q_pk;
		alter table market_price_after_correction_by_date_h add constraint reg_116_q_pk primary key (id);
	END IF;
END $$;

/*reg_115_q_pk 466*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_price_for_first_floor_history') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_price_for_first_floor_history') THEN
			ALTER TABLE market_price_for_first_floor_history DROP CONSTRAINT IF EXISTS reg_115_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_115_q_pk;
		alter table market_price_for_first_floor_history add constraint reg_115_q_pk primary key (id);
	END IF;
END $$;

/*reg_117_q_pk 467*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_correction_settings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_correction_settings') THEN
			ALTER TABLE market_correction_settings DROP CONSTRAINT IF EXISTS reg_117_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_117_q_pk;
		alter table market_correction_settings add constraint reg_117_q_pk primary key (id);
	END IF;
END $$;

/*reg_2_a_3__pk 468*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_3') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_3') THEN
			ALTER TABLE gbu_source2_a_3 DROP CONSTRAINT IF EXISTS reg_2_a_3__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_3__pk;
		CREATE UNIQUE INDEX reg_2_a_3__pk ON public.gbu_source2_a_3 USING btree (id);
	END IF;
END $$;

/*reg_2_a_3_inx_obj_attr_id 469*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_3') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_3') THEN
			ALTER TABLE gbu_source2_a_3 DROP CONSTRAINT IF EXISTS reg_2_a_3_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_3_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_3_inx_obj_attr_id ON public.gbu_source2_a_3 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_4__pk 470*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_4') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_4') THEN
			ALTER TABLE gbu_source2_a_4 DROP CONSTRAINT IF EXISTS reg_2_a_4__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_4__pk;
		CREATE UNIQUE INDEX reg_2_a_4__pk ON public.gbu_source2_a_4 USING btree (id);
	END IF;
END $$;

/*reg_2_a_4_inx_obj_attr_id 471*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_4') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_4') THEN
			ALTER TABLE gbu_source2_a_4 DROP CONSTRAINT IF EXISTS reg_2_a_4_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_4_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_4_inx_obj_attr_id ON public.gbu_source2_a_4 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_5__pk 472*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_5') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_5') THEN
			ALTER TABLE gbu_source2_a_5 DROP CONSTRAINT IF EXISTS reg_2_a_5__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_5__pk;
		CREATE UNIQUE INDEX reg_2_a_5__pk ON public.gbu_source2_a_5 USING btree (id);
	END IF;
END $$;

/*reg_2_a_5_inx_obj_attr_id 473*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_5') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_5') THEN
			ALTER TABLE gbu_source2_a_5 DROP CONSTRAINT IF EXISTS reg_2_a_5_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_5_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_5_inx_obj_attr_id ON public.gbu_source2_a_5 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_6__pk 474*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_6') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_6') THEN
			ALTER TABLE gbu_source2_a_6 DROP CONSTRAINT IF EXISTS reg_2_a_6__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_6__pk;
		CREATE UNIQUE INDEX reg_2_a_6__pk ON public.gbu_source2_a_6 USING btree (id);
	END IF;
END $$;

/*reg_2_a_6_inx_obj_attr_id 475*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_6') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_6') THEN
			ALTER TABLE gbu_source2_a_6 DROP CONSTRAINT IF EXISTS reg_2_a_6_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_6_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_6_inx_obj_attr_id ON public.gbu_source2_a_6 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_54578493_a_num_pk 476*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_45_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_45_num') THEN
			ALTER TABLE gbu_custom_source_45_num DROP CONSTRAINT IF EXISTS reg_54578493_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578493_a_num_pk;
		CREATE UNIQUE INDEX reg_54578493_a_num_pk ON public.gbu_custom_source_45_num USING btree (id);
	END IF;
END $$;

/*reg_54578493_a_num_inx_obj_attr_id 477*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_45_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_45_num') THEN
			ALTER TABLE gbu_custom_source_45_num DROP CONSTRAINT IF EXISTS reg_54578493_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578493_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578493_a_num_inx_obj_attr_id ON public.gbu_custom_source_45_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_14_a_txt_pk 478*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source14_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source14_a_txt') THEN
			ALTER TABLE gbu_source14_a_txt DROP CONSTRAINT IF EXISTS reg_14_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_14_a_txt_pk;
		CREATE UNIQUE INDEX reg_14_a_txt_pk ON public.gbu_source14_a_txt USING btree (id);
	END IF;
END $$;

/*reg_14_a_txt_inx_obj_attr_id 479*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source14_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source14_a_txt') THEN
			ALTER TABLE gbu_source14_a_txt DROP CONSTRAINT IF EXISTS reg_14_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_14_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_14_a_txt_inx_obj_attr_id ON public.gbu_source14_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_15_a_dt_pk 480*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source15_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source15_a_dt') THEN
			ALTER TABLE gbu_source15_a_dt DROP CONSTRAINT IF EXISTS reg_15_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_15_a_dt_pk;
		CREATE UNIQUE INDEX reg_15_a_dt_pk ON public.gbu_source15_a_dt USING btree (id);
	END IF;
END $$;

/*reg_15_a_dt_inx_obj_attr_id 481*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source15_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source15_a_dt') THEN
			ALTER TABLE gbu_source15_a_dt DROP CONSTRAINT IF EXISTS reg_15_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_15_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_15_a_dt_inx_obj_attr_id ON public.gbu_source15_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_15_a_txt_pk 482*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source15_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source15_a_txt') THEN
			ALTER TABLE gbu_source15_a_txt DROP CONSTRAINT IF EXISTS reg_15_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_15_a_txt_pk;
		CREATE UNIQUE INDEX reg_15_a_txt_pk ON public.gbu_source15_a_txt USING btree (id);
	END IF;
END $$;

/*reg_15_a_txt_inx_obj_attr_id 483*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source15_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source15_a_txt') THEN
			ALTER TABLE gbu_source15_a_txt DROP CONSTRAINT IF EXISTS reg_15_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_15_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_15_a_txt_inx_obj_attr_id ON public.gbu_source15_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_2_a_8__pk 484*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_8') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_8') THEN
			ALTER TABLE gbu_source2_a_8 DROP CONSTRAINT IF EXISTS reg_2_a_8__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_8__pk;
		CREATE UNIQUE INDEX reg_2_a_8__pk ON public.gbu_source2_a_8 USING btree (id);
	END IF;
END $$;

/*reg_2_a_8_inx_obj_attr_id 485*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_8') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_8') THEN
			ALTER TABLE gbu_source2_a_8 DROP CONSTRAINT IF EXISTS reg_2_a_8_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_8_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_8_inx_obj_attr_id ON public.gbu_source2_a_8 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_14__pk 486*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_14') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_14') THEN
			ALTER TABLE gbu_source2_a_14 DROP CONSTRAINT IF EXISTS reg_2_a_14__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_14__pk;
		CREATE UNIQUE INDEX reg_2_a_14__pk ON public.gbu_source2_a_14 USING btree (id);
	END IF;
END $$;

/*reg_2_a_14_inx_obj_attr_id 487*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_14') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_14') THEN
			ALTER TABLE gbu_source2_a_14 DROP CONSTRAINT IF EXISTS reg_2_a_14_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_14_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_14_inx_obj_attr_id ON public.gbu_source2_a_14 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_15__pk 488*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_15') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_15') THEN
			ALTER TABLE gbu_source2_a_15 DROP CONSTRAINT IF EXISTS reg_2_a_15__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_15__pk;
		CREATE UNIQUE INDEX reg_2_a_15__pk ON public.gbu_source2_a_15 USING btree (id);
	END IF;
END $$;

/*reg_2_a_15_inx_obj_attr_id 489*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_15') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_15') THEN
			ALTER TABLE gbu_source2_a_15 DROP CONSTRAINT IF EXISTS reg_2_a_15_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_15_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_15_inx_obj_attr_id ON public.gbu_source2_a_15 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_16__pk 490*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_16') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_16') THEN
			ALTER TABLE gbu_source2_a_16 DROP CONSTRAINT IF EXISTS reg_2_a_16__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_16__pk;
		CREATE UNIQUE INDEX reg_2_a_16__pk ON public.gbu_source2_a_16 USING btree (id);
	END IF;
END $$;

/*reg_2_a_16_inx_obj_attr_id 491*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_16') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_16') THEN
			ALTER TABLE gbu_source2_a_16 DROP CONSTRAINT IF EXISTS reg_2_a_16_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_16_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_16_inx_obj_attr_id ON public.gbu_source2_a_16 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_17__pk 492*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_17') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_17') THEN
			ALTER TABLE gbu_source2_a_17 DROP CONSTRAINT IF EXISTS reg_2_a_17__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_17__pk;
		CREATE UNIQUE INDEX reg_2_a_17__pk ON public.gbu_source2_a_17 USING btree (id);
	END IF;
END $$;

/*reg_2_a_17_inx_obj_attr_id 493*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_17') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_17') THEN
			ALTER TABLE gbu_source2_a_17 DROP CONSTRAINT IF EXISTS reg_2_a_17_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_17_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_17_inx_obj_attr_id ON public.gbu_source2_a_17 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_18__pk 494*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_18') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_18') THEN
			ALTER TABLE gbu_source2_a_18 DROP CONSTRAINT IF EXISTS reg_2_a_18__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_18__pk;
		CREATE UNIQUE INDEX reg_2_a_18__pk ON public.gbu_source2_a_18 USING btree (id);
	END IF;
END $$;

/*reg_2_a_18_inx_obj_attr_id 495*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_18') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_18') THEN
			ALTER TABLE gbu_source2_a_18 DROP CONSTRAINT IF EXISTS reg_2_a_18_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_18_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_18_inx_obj_attr_id ON public.gbu_source2_a_18 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_19__pk 496*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_19') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_19') THEN
			ALTER TABLE gbu_source2_a_19 DROP CONSTRAINT IF EXISTS reg_2_a_19__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_19__pk;
		CREATE UNIQUE INDEX reg_2_a_19__pk ON public.gbu_source2_a_19 USING btree (id);
	END IF;
END $$;

/*reg_2_a_19_inx_obj_attr_id 497*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_19') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_19') THEN
			ALTER TABLE gbu_source2_a_19 DROP CONSTRAINT IF EXISTS reg_2_a_19_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_19_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_19_inx_obj_attr_id ON public.gbu_source2_a_19 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_20__pk 498*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_20') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_20') THEN
			ALTER TABLE gbu_source2_a_20 DROP CONSTRAINT IF EXISTS reg_2_a_20__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_20__pk;
		CREATE UNIQUE INDEX reg_2_a_20__pk ON public.gbu_source2_a_20 USING btree (id);
	END IF;
END $$;

/*reg_2_a_20_inx_obj_attr_id 499*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_20') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_20') THEN
			ALTER TABLE gbu_source2_a_20 DROP CONSTRAINT IF EXISTS reg_2_a_20_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_20_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_20_inx_obj_attr_id ON public.gbu_source2_a_20 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_21__pk 500*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_21') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_21') THEN
			ALTER TABLE gbu_source2_a_21 DROP CONSTRAINT IF EXISTS reg_2_a_21__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_21__pk;
		CREATE UNIQUE INDEX reg_2_a_21__pk ON public.gbu_source2_a_21 USING btree (id);
	END IF;
END $$;

/*reg_2_a_21_inx_obj_attr_id 501*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_21') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_21') THEN
			ALTER TABLE gbu_source2_a_21 DROP CONSTRAINT IF EXISTS reg_2_a_21_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_21_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_21_inx_obj_attr_id ON public.gbu_source2_a_21 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_22__pk 502*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_22') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_22') THEN
			ALTER TABLE gbu_source2_a_22 DROP CONSTRAINT IF EXISTS reg_2_a_22__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_22__pk;
		CREATE UNIQUE INDEX reg_2_a_22__pk ON public.gbu_source2_a_22 USING btree (id);
	END IF;
END $$;

/*reg_2_a_22_inx_obj_attr_id 503*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_22') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_22') THEN
			ALTER TABLE gbu_source2_a_22 DROP CONSTRAINT IF EXISTS reg_2_a_22_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_22_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_22_inx_obj_attr_id ON public.gbu_source2_a_22 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_23__pk 504*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_23') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_23') THEN
			ALTER TABLE gbu_source2_a_23 DROP CONSTRAINT IF EXISTS reg_2_a_23__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_23__pk;
		CREATE UNIQUE INDEX reg_2_a_23__pk ON public.gbu_source2_a_23 USING btree (id);
	END IF;
END $$;

/*reg_2_a_23_inx_obj_attr_id 505*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_23') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_23') THEN
			ALTER TABLE gbu_source2_a_23 DROP CONSTRAINT IF EXISTS reg_2_a_23_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_23_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_23_inx_obj_attr_id ON public.gbu_source2_a_23 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_24__pk 506*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_24') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_24') THEN
			ALTER TABLE gbu_source2_a_24 DROP CONSTRAINT IF EXISTS reg_2_a_24__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_24__pk;
		CREATE UNIQUE INDEX reg_2_a_24__pk ON public.gbu_source2_a_24 USING btree (id);
	END IF;
END $$;

/*reg_2_a_24_inx_obj_attr_id 507*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_24') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_24') THEN
			ALTER TABLE gbu_source2_a_24 DROP CONSTRAINT IF EXISTS reg_2_a_24_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_24_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_24_inx_obj_attr_id ON public.gbu_source2_a_24 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_25__pk 508*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_25') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_25') THEN
			ALTER TABLE gbu_source2_a_25 DROP CONSTRAINT IF EXISTS reg_2_a_25__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_25__pk;
		CREATE UNIQUE INDEX reg_2_a_25__pk ON public.gbu_source2_a_25 USING btree (id);
	END IF;
END $$;

/*reg_2_a_25_inx_obj_attr_id 509*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_25') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_25') THEN
			ALTER TABLE gbu_source2_a_25 DROP CONSTRAINT IF EXISTS reg_2_a_25_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_25_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_25_inx_obj_attr_id ON public.gbu_source2_a_25 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_26__pk 510*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_26') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_26') THEN
			ALTER TABLE gbu_source2_a_26 DROP CONSTRAINT IF EXISTS reg_2_a_26__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_26__pk;
		CREATE UNIQUE INDEX reg_2_a_26__pk ON public.gbu_source2_a_26 USING btree (id);
	END IF;
END $$;

/*reg_2_a_26_inx_obj_attr_id 511*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_26') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_26') THEN
			ALTER TABLE gbu_source2_a_26 DROP CONSTRAINT IF EXISTS reg_2_a_26_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_26_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_26_inx_obj_attr_id ON public.gbu_source2_a_26 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_606__pk 512*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_606') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_606') THEN
			ALTER TABLE gbu_source2_a_606 DROP CONSTRAINT IF EXISTS reg_2_a_606__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_606__pk;
		CREATE UNIQUE INDEX reg_2_a_606__pk ON public.gbu_source2_a_606 USING btree (id);
	END IF;
END $$;

/*reg_2_a_606_inx_obj_attr_id 513*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_606') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_606') THEN
			ALTER TABLE gbu_source2_a_606 DROP CONSTRAINT IF EXISTS reg_2_a_606_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_606_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_606_inx_obj_attr_id ON public.gbu_source2_a_606 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_27__pk 514*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_27') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_27') THEN
			ALTER TABLE gbu_source2_a_27 DROP CONSTRAINT IF EXISTS reg_2_a_27__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_27__pk;
		CREATE UNIQUE INDEX reg_2_a_27__pk ON public.gbu_source2_a_27 USING btree (id);
	END IF;
END $$;

/*reg_2_a_27_inx_obj_attr_id 515*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_27') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_27') THEN
			ALTER TABLE gbu_source2_a_27 DROP CONSTRAINT IF EXISTS reg_2_a_27_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_27_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_27_inx_obj_attr_id ON public.gbu_source2_a_27 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_43__pk 516*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_43') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_43') THEN
			ALTER TABLE gbu_source2_a_43 DROP CONSTRAINT IF EXISTS reg_2_a_43__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_43__pk;
		CREATE UNIQUE INDEX reg_2_a_43__pk ON public.gbu_source2_a_43 USING btree (id);
	END IF;
END $$;

/*reg_2_a_43_inx_obj_attr_id 517*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_43') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_43') THEN
			ALTER TABLE gbu_source2_a_43 DROP CONSTRAINT IF EXISTS reg_2_a_43_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_43_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_43_inx_obj_attr_id ON public.gbu_source2_a_43 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_44__pk 518*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_44') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_44') THEN
			ALTER TABLE gbu_source2_a_44 DROP CONSTRAINT IF EXISTS reg_2_a_44__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_44__pk;
		CREATE UNIQUE INDEX reg_2_a_44__pk ON public.gbu_source2_a_44 USING btree (id);
	END IF;
END $$;

/*reg_2_a_44_inx_obj_attr_id 519*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_44') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_44') THEN
			ALTER TABLE gbu_source2_a_44 DROP CONSTRAINT IF EXISTS reg_2_a_44_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_44_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_44_inx_obj_attr_id ON public.gbu_source2_a_44 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_30_a_dt_pk 520*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source30_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source30_a_dt') THEN
			ALTER TABLE gbu_source30_a_dt DROP CONSTRAINT IF EXISTS reg_30_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_30_a_dt_pk;
		CREATE UNIQUE INDEX reg_30_a_dt_pk ON public.gbu_source30_a_dt USING btree (id);
	END IF;
END $$;

/*reg_30_a_dt_inx_obj_attr_id 521*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source30_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source30_a_dt') THEN
			ALTER TABLE gbu_source30_a_dt DROP CONSTRAINT IF EXISTS reg_30_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_30_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_30_a_dt_inx_obj_attr_id ON public.gbu_source30_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_16_a_dt_pk 522*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source16_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source16_a_dt') THEN
			ALTER TABLE gbu_source16_a_dt DROP CONSTRAINT IF EXISTS reg_16_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_16_a_dt_pk;
		CREATE UNIQUE INDEX reg_16_a_dt_pk ON public.gbu_source16_a_dt USING btree (id);
	END IF;
END $$;

/*reg_16_a_dt_inx_obj_attr_id 523*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source16_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source16_a_dt') THEN
			ALTER TABLE gbu_source16_a_dt DROP CONSTRAINT IF EXISTS reg_16_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_16_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_16_a_dt_inx_obj_attr_id ON public.gbu_source16_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_17_a_num_pk 524*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source17_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source17_a_num') THEN
			ALTER TABLE gbu_source17_a_num DROP CONSTRAINT IF EXISTS reg_17_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_17_a_num_pk;
		CREATE UNIQUE INDEX reg_17_a_num_pk ON public.gbu_source17_a_num USING btree (id);
	END IF;
END $$;

/*reg_17_a_num_inx_obj_attr_id 525*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source17_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source17_a_num') THEN
			ALTER TABLE gbu_source17_a_num DROP CONSTRAINT IF EXISTS reg_17_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_17_a_num_inx_obj_attr_id;
		CREATE INDEX reg_17_a_num_inx_obj_attr_id ON public.gbu_source17_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_17_a_txt_pk 526*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source17_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source17_a_txt') THEN
			ALTER TABLE gbu_source17_a_txt DROP CONSTRAINT IF EXISTS reg_17_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_17_a_txt_pk;
		CREATE UNIQUE INDEX reg_17_a_txt_pk ON public.gbu_source17_a_txt USING btree (id);
	END IF;
END $$;

/*reg_17_a_txt_inx_obj_attr_id 527*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source17_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source17_a_txt') THEN
			ALTER TABLE gbu_source17_a_txt DROP CONSTRAINT IF EXISTS reg_17_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_17_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_17_a_txt_inx_obj_attr_id ON public.gbu_source17_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*ko_modeling_dictionaries_name_key 528*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_modeling_dictionaries') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_modeling_dictionaries') THEN
			ALTER TABLE ko_modeling_dictionaries DROP CONSTRAINT IF EXISTS ko_modeling_dictionaries_name_key RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_modeling_dictionaries_name_key;
		CREATE UNIQUE INDEX ko_modeling_dictionaries_name_key ON public.ko_modeling_dictionaries USING btree (name);
	END IF;
END $$;

/*reg_264_q_pk 529*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_modeling_dictionaries') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_modeling_dictionaries') THEN
			ALTER TABLE ko_modeling_dictionaries DROP CONSTRAINT IF EXISTS reg_264_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_264_q_pk;
		alter table KO_MODELING_DICTIONARIES add constraint reg_264_q_pk primary key (id);
	END IF;
END $$;

/*reg_54578493_a_dt_pk 530*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_45_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_45_dt') THEN
			ALTER TABLE gbu_custom_source_45_dt DROP CONSTRAINT IF EXISTS reg_54578493_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578493_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578493_a_dt_pk ON public.gbu_custom_source_45_dt USING btree (id);
	END IF;
END $$;

/*reg_54578493_a_dt_inx_obj_attr_id 531*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_45_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_45_dt') THEN
			ALTER TABLE gbu_custom_source_45_dt DROP CONSTRAINT IF EXISTS reg_54578493_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578493_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578493_a_dt_inx_obj_attr_id ON public.gbu_custom_source_45_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_44_a_txt_pk 532*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source44_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source44_a_txt') THEN
			ALTER TABLE gbu_source44_a_txt DROP CONSTRAINT IF EXISTS reg_44_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44_a_txt_pk;
		CREATE UNIQUE INDEX reg_44_a_txt_pk ON public.gbu_source44_a_txt USING btree (id);
	END IF;
END $$;

/*reg_44_a_txt_inx_obj_attr_id 533*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source44_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source44_a_txt') THEN
			ALTER TABLE gbu_source44_a_txt DROP CONSTRAINT IF EXISTS reg_44_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_44_a_txt_inx_obj_attr_id ON public.gbu_source44_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_18_a_dt_pk 534*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source18_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source18_a_dt') THEN
			ALTER TABLE gbu_source18_a_dt DROP CONSTRAINT IF EXISTS reg_18_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_18_a_dt_pk;
		CREATE UNIQUE INDEX reg_18_a_dt_pk ON public.gbu_source18_a_dt USING btree (id);
	END IF;
END $$;

/*reg_18_a_dt_inx_obj_attr_id 535*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source18_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source18_a_dt') THEN
			ALTER TABLE gbu_source18_a_dt DROP CONSTRAINT IF EXISTS reg_18_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_18_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_18_a_dt_inx_obj_attr_id ON public.gbu_source18_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_12_a_txt_pk 536*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source12_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source12_a_txt') THEN
			ALTER TABLE gbu_source12_a_txt DROP CONSTRAINT IF EXISTS reg_12_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_12_a_txt_pk;
		CREATE UNIQUE INDEX reg_12_a_txt_pk ON public.gbu_source12_a_txt USING btree (id);
	END IF;
END $$;

/*reg_12_a_txt_inx_obj_attr_id 537*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source12_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source12_a_txt') THEN
			ALTER TABLE gbu_source12_a_txt DROP CONSTRAINT IF EXISTS reg_12_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_12_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_12_a_txt_inx_obj_attr_id ON public.gbu_source12_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_18_a_txt_pk 538*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source18_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source18_a_txt') THEN
			ALTER TABLE gbu_source18_a_txt DROP CONSTRAINT IF EXISTS reg_18_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_18_a_txt_pk;
		CREATE UNIQUE INDEX reg_18_a_txt_pk ON public.gbu_source18_a_txt USING btree (id);
	END IF;
END $$;

/*reg_18_a_txt_inx_obj_attr_id 539*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source18_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source18_a_txt') THEN
			ALTER TABLE gbu_source18_a_txt DROP CONSTRAINT IF EXISTS reg_18_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_18_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_18_a_txt_inx_obj_attr_id ON public.gbu_source18_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_44_a_dt_pk 540*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source44_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source44_a_dt') THEN
			ALTER TABLE gbu_source44_a_dt DROP CONSTRAINT IF EXISTS reg_44_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44_a_dt_pk;
		CREATE UNIQUE INDEX reg_44_a_dt_pk ON public.gbu_source44_a_dt USING btree (id);
	END IF;
END $$;

/*reg_44_a_dt_inx_obj_attr_id 541*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source44_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source44_a_dt') THEN
			ALTER TABLE gbu_source44_a_dt DROP CONSTRAINT IF EXISTS reg_44_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_44_a_dt_inx_obj_attr_id ON public.gbu_source44_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_44_a_num_pk 542*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source44_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source44_a_num') THEN
			ALTER TABLE gbu_source44_a_num DROP CONSTRAINT IF EXISTS reg_44_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44_a_num_pk;
		CREATE UNIQUE INDEX reg_44_a_num_pk ON public.gbu_source44_a_num USING btree (id);
	END IF;
END $$;

/*reg_44_a_num_inx_obj_attr_id 543*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source44_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source44_a_num') THEN
			ALTER TABLE gbu_source44_a_num DROP CONSTRAINT IF EXISTS reg_44_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_44_a_num_inx_obj_attr_id;
		CREATE INDEX reg_44_a_num_inx_obj_attr_id ON public.gbu_source44_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_19_a_dt_pk 544*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source19_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source19_a_dt') THEN
			ALTER TABLE gbu_source19_a_dt DROP CONSTRAINT IF EXISTS reg_19_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_19_a_dt_pk;
		CREATE UNIQUE INDEX reg_19_a_dt_pk ON public.gbu_source19_a_dt USING btree (id);
	END IF;
END $$;

/*reg_19_a_dt_inx_obj_attr_id 545*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source19_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source19_a_dt') THEN
			ALTER TABLE gbu_source19_a_dt DROP CONSTRAINT IF EXISTS reg_19_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_19_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_19_a_dt_inx_obj_attr_id ON public.gbu_source19_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_19_a_num_pk 546*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source19_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source19_a_num') THEN
			ALTER TABLE gbu_source19_a_num DROP CONSTRAINT IF EXISTS reg_19_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_19_a_num_pk;
		CREATE UNIQUE INDEX reg_19_a_num_pk ON public.gbu_source19_a_num USING btree (id);
	END IF;
END $$;

/*reg_19_a_num_inx_obj_attr_id 547*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source19_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source19_a_num') THEN
			ALTER TABLE gbu_source19_a_num DROP CONSTRAINT IF EXISTS reg_19_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_19_a_num_inx_obj_attr_id;
		CREATE INDEX reg_19_a_num_inx_obj_attr_id ON public.gbu_source19_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_19_a_txt_pk 548*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source19_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source19_a_txt') THEN
			ALTER TABLE gbu_source19_a_txt DROP CONSTRAINT IF EXISTS reg_19_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_19_a_txt_pk;
		CREATE UNIQUE INDEX reg_19_a_txt_pk ON public.gbu_source19_a_txt USING btree (id);
	END IF;
END $$;

/*reg_19_a_txt_inx_obj_attr_id 549*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source19_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source19_a_txt') THEN
			ALTER TABLE gbu_source19_a_txt DROP CONSTRAINT IF EXISTS reg_19_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_19_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_19_a_txt_inx_obj_attr_id ON public.gbu_source19_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_25_a_dt_pk 550*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source25_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source25_a_dt') THEN
			ALTER TABLE gbu_source25_a_dt DROP CONSTRAINT IF EXISTS reg_25_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_25_a_dt_pk;
		CREATE UNIQUE INDEX reg_25_a_dt_pk ON public.gbu_source25_a_dt USING btree (id);
	END IF;
END $$;

/*reg_25_a_dt_inx_obj_attr_id 551*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source25_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source25_a_dt') THEN
			ALTER TABLE gbu_source25_a_dt DROP CONSTRAINT IF EXISTS reg_25_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_25_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_25_a_dt_inx_obj_attr_id ON public.gbu_source25_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_25_a_num_pk 552*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source25_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source25_a_num') THEN
			ALTER TABLE gbu_source25_a_num DROP CONSTRAINT IF EXISTS reg_25_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_25_a_num_pk;
		CREATE UNIQUE INDEX reg_25_a_num_pk ON public.gbu_source25_a_num USING btree (id);
	END IF;
END $$;

/*reg_25_a_num_inx_obj_attr_id 553*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source25_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source25_a_num') THEN
			ALTER TABLE gbu_source25_a_num DROP CONSTRAINT IF EXISTS reg_25_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_25_a_num_inx_obj_attr_id;
		CREATE INDEX reg_25_a_num_inx_obj_attr_id ON public.gbu_source25_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_25_a_txt_pk 554*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source25_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source25_a_txt') THEN
			ALTER TABLE gbu_source25_a_txt DROP CONSTRAINT IF EXISTS reg_25_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_25_a_txt_pk;
		CREATE UNIQUE INDEX reg_25_a_txt_pk ON public.gbu_source25_a_txt USING btree (id);
	END IF;
END $$;

/*reg_25_a_txt_inx_obj_attr_id 555*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source25_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source25_a_txt') THEN
			ALTER TABLE gbu_source25_a_txt DROP CONSTRAINT IF EXISTS reg_25_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_25_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_25_a_txt_inx_obj_attr_id ON public.gbu_source25_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_979_q_pk 556*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_background_exports') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_background_exports') THEN
			ALTER TABLE core_background_exports DROP CONSTRAINT IF EXISTS reg_979_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_979_q_pk;
		alter table CORE_BACKGROUND_EXPORTS add constraint reg_979_q_pk primary key (id);
	END IF;
END $$;

/*reg_265_q_pk 557*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_modeling_dictionaries_values') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_modeling_dictionaries_values') THEN
			ALTER TABLE ko_modeling_dictionaries_values DROP CONSTRAINT IF EXISTS reg_265_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_265_q_pk;
		alter table KO_MODELING_DICTIONARIES_VALUES add constraint reg_265_q_pk primary key (id);
	END IF;
END $$;

/*reg_209_q_pk 558*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_model_attributes') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_model_attributes') THEN
			ALTER TABLE ko_model_attributes DROP CONSTRAINT IF EXISTS reg_209_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_209_q_pk;
		CREATE UNIQUE INDEX reg_209_q_pk ON public.ko_model_attributes USING btree (id);
	END IF;
END $$;

/*reg_20_a_num_pk 559*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source20_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source20_a_num') THEN
			ALTER TABLE gbu_source20_a_num DROP CONSTRAINT IF EXISTS reg_20_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_20_a_num_pk;
		CREATE UNIQUE INDEX reg_20_a_num_pk ON public.gbu_source20_a_num USING btree (id);
	END IF;
END $$;

/*reg_20_a_num_inx_obj_attr_id 560*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source20_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source20_a_num') THEN
			ALTER TABLE gbu_source20_a_num DROP CONSTRAINT IF EXISTS reg_20_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_20_a_num_inx_obj_attr_id;
		CREATE INDEX reg_20_a_num_inx_obj_attr_id ON public.gbu_source20_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_1_a_dt_pk 561*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source1_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source1_a_dt') THEN
			ALTER TABLE gbu_source1_a_dt DROP CONSTRAINT IF EXISTS reg_1_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_1_a_dt_pk;
		CREATE UNIQUE INDEX reg_1_a_dt_pk ON public.gbu_source1_a_dt USING btree (id);
	END IF;
END $$;

/*reg_1_a_dt_inx_obj_attr_id 562*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source1_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source1_a_dt') THEN
			ALTER TABLE gbu_source1_a_dt DROP CONSTRAINT IF EXISTS reg_1_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_1_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_1_a_dt_inx_obj_attr_id ON public.gbu_source1_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_1_a_num_pk 563*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source1_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source1_a_num') THEN
			ALTER TABLE gbu_source1_a_num DROP CONSTRAINT IF EXISTS reg_1_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_1_a_num_pk;
		CREATE UNIQUE INDEX reg_1_a_num_pk ON public.gbu_source1_a_num USING btree (id);
	END IF;
END $$;

/*reg_1_a_num_inx_obj_attr_id 564*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source1_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source1_a_num') THEN
			ALTER TABLE gbu_source1_a_num DROP CONSTRAINT IF EXISTS reg_1_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_1_a_num_inx_obj_attr_id;
		CREATE INDEX reg_1_a_num_inx_obj_attr_id ON public.gbu_source1_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_1_a_txt_pk 565*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source1_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source1_a_txt') THEN
			ALTER TABLE gbu_source1_a_txt DROP CONSTRAINT IF EXISTS reg_1_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_1_a_txt_pk;
		CREATE UNIQUE INDEX reg_1_a_txt_pk ON public.gbu_source1_a_txt USING btree (id);
	END IF;
END $$;

/*reg_1_a_txt_inx_obj_attr_id 566*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source1_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source1_a_txt') THEN
			ALTER TABLE gbu_source1_a_txt DROP CONSTRAINT IF EXISTS reg_1_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_1_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_1_a_txt_inx_obj_attr_id ON public.gbu_source1_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_20_a_dt_pk 567*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source20_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source20_a_dt') THEN
			ALTER TABLE gbu_source20_a_dt DROP CONSTRAINT IF EXISTS reg_20_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_20_a_dt_pk;
		CREATE UNIQUE INDEX reg_20_a_dt_pk ON public.gbu_source20_a_dt USING btree (id);
	END IF;
END $$;

/*reg_20_a_dt_inx_obj_attr_id 568*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source20_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source20_a_dt') THEN
			ALTER TABLE gbu_source20_a_dt DROP CONSTRAINT IF EXISTS reg_20_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_20_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_20_a_dt_inx_obj_attr_id ON public.gbu_source20_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_20_a_txt_pk 569*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source20_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source20_a_txt') THEN
			ALTER TABLE gbu_source20_a_txt DROP CONSTRAINT IF EXISTS reg_20_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_20_a_txt_pk;
		CREATE UNIQUE INDEX reg_20_a_txt_pk ON public.gbu_source20_a_txt USING btree (id);
	END IF;
END $$;

/*reg_20_a_txt_inx_obj_attr_id 570*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source20_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source20_a_txt') THEN
			ALTER TABLE gbu_source20_a_txt DROP CONSTRAINT IF EXISTS reg_20_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_20_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_20_a_txt_inx_obj_attr_id ON public.gbu_source20_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_21_a_dt_pk 571*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source21_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source21_a_dt') THEN
			ALTER TABLE gbu_source21_a_dt DROP CONSTRAINT IF EXISTS reg_21_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_21_a_dt_pk;
		CREATE UNIQUE INDEX reg_21_a_dt_pk ON public.gbu_source21_a_dt USING btree (id);
	END IF;
END $$;

/*reg_21_a_dt_inx_obj_attr_id 572*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source21_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source21_a_dt') THEN
			ALTER TABLE gbu_source21_a_dt DROP CONSTRAINT IF EXISTS reg_21_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_21_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_21_a_dt_inx_obj_attr_id ON public.gbu_source21_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_21_a_num_pk 573*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source21_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source21_a_num') THEN
			ALTER TABLE gbu_source21_a_num DROP CONSTRAINT IF EXISTS reg_21_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_21_a_num_pk;
		CREATE UNIQUE INDEX reg_21_a_num_pk ON public.gbu_source21_a_num USING btree (id);
	END IF;
END $$;

/*reg_21_a_num_inx_obj_attr_id 574*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source21_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source21_a_num') THEN
			ALTER TABLE gbu_source21_a_num DROP CONSTRAINT IF EXISTS reg_21_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_21_a_num_inx_obj_attr_id;
		CREATE INDEX reg_21_a_num_inx_obj_attr_id ON public.gbu_source21_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_21_a_txt_pk 575*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source21_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source21_a_txt') THEN
			ALTER TABLE gbu_source21_a_txt DROP CONSTRAINT IF EXISTS reg_21_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_21_a_txt_pk;
		CREATE UNIQUE INDEX reg_21_a_txt_pk ON public.gbu_source21_a_txt USING btree (id);
	END IF;
END $$;

/*reg_21_a_txt_inx_obj_attr_id 576*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source21_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source21_a_txt') THEN
			ALTER TABLE gbu_source21_a_txt DROP CONSTRAINT IF EXISTS reg_21_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_21_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_21_a_txt_inx_obj_attr_id ON public.gbu_source21_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_81_q_pk 577*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_attribute_settings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_attribute_settings') THEN
			ALTER TABLE gbu_attribute_settings DROP CONSTRAINT IF EXISTS reg_81_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_81_q_pk;
		alter table GBU_ATTRIBUTE_SETTINGS add constraint reg_81_q_pk primary key (attribute_id);
	END IF;
END $$;

/*reg_2_a_49420746__pk 578*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420746') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420746') THEN
			ALTER TABLE gbu_source2_a_49420746 DROP CONSTRAINT IF EXISTS reg_2_a_49420746__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420746__pk;
		CREATE UNIQUE INDEX reg_2_a_49420746__pk ON public.gbu_source2_a_49420746 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420746_inx_obj_attr_id 579*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420746') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420746') THEN
			ALTER TABLE gbu_source2_a_49420746 DROP CONSTRAINT IF EXISTS reg_2_a_49420746_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420746_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420746_inx_obj_attr_id ON public.gbu_source2_a_49420746 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_29_a_txt_pk 580*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source29_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source29_a_txt') THEN
			ALTER TABLE gbu_source29_a_txt DROP CONSTRAINT IF EXISTS reg_29_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29_a_txt_pk;
		CREATE UNIQUE INDEX reg_29_a_txt_pk ON public.gbu_source29_a_txt USING btree (id);
	END IF;
END $$;

/*reg_29_a_txt_inx_obj_attr_id 581*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source29_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source29_a_txt') THEN
			ALTER TABLE gbu_source29_a_txt DROP CONSTRAINT IF EXISTS reg_29_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_29_a_txt_inx_obj_attr_id ON public.gbu_source29_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_2_a_49420747__pk 582*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420747') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420747') THEN
			ALTER TABLE gbu_source2_a_49420747 DROP CONSTRAINT IF EXISTS reg_2_a_49420747__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420747__pk;
		CREATE UNIQUE INDEX reg_2_a_49420747__pk ON public.gbu_source2_a_49420747 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420747_inx_obj_attr_id 583*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420747') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420747') THEN
			ALTER TABLE gbu_source2_a_49420747 DROP CONSTRAINT IF EXISTS reg_2_a_49420747_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420747_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420747_inx_obj_attr_id ON public.gbu_source2_a_49420747 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_49420748__pk 584*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420748') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420748') THEN
			ALTER TABLE gbu_source2_a_49420748 DROP CONSTRAINT IF EXISTS reg_2_a_49420748__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420748__pk;
		CREATE UNIQUE INDEX reg_2_a_49420748__pk ON public.gbu_source2_a_49420748 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420748_inx_obj_attr_id 585*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420748') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420748') THEN
			ALTER TABLE gbu_source2_a_49420748 DROP CONSTRAINT IF EXISTS reg_2_a_49420748_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420748_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420748_inx_obj_attr_id ON public.gbu_source2_a_49420748 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_54578514_a_txt_pk 586*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_46_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_46_txt') THEN
			ALTER TABLE gbu_custom_source_46_txt DROP CONSTRAINT IF EXISTS reg_54578514_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578514_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578514_a_txt_pk ON public.gbu_custom_source_46_txt USING btree (id);
	END IF;
END $$;

/*reg_54578514_a_txt_inx_obj_attr_id 587*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_46_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_46_txt') THEN
			ALTER TABLE gbu_custom_source_46_txt DROP CONSTRAINT IF EXISTS reg_54578514_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578514_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578514_a_txt_inx_obj_attr_id ON public.gbu_custom_source_46_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_47506942_q_pk 588*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors47504845') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors47504845') THEN
			ALTER TABLE ko_touroksfactors47504845 DROP CONSTRAINT IF EXISTS reg_47506942_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47506942_q_pk;
		CREATE UNIQUE INDEX reg_47506942_q_pk ON public.ko_touroksfactors47504845 USING btree (id);
	END IF;
END $$;

/*deleted_event_id_47498663_idx 589*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors18099454_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors18099454_deleted') THEN
			ALTER TABLE ko_touroksfactors18099454_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_47498663_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_47498663_idx;
		CREATE INDEX deleted_event_id_47498663_idx ON public.ko_touroksfactors18099454_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_47506942_idx 590*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors47504845_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors47504845_deleted') THEN
			ALTER TABLE ko_touroksfactors47504845_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_47506942_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_47506942_idx;
		CREATE INDEX deleted_event_id_47506942_idx ON public.ko_touroksfactors47504845_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_47686787_idx 591*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors47504845_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors47504845_deleted') THEN
			ALTER TABLE ko_tourzufactors47504845_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_47686787_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_47686787_idx;
		CREATE INDEX deleted_event_id_47686787_idx ON public.ko_tourzufactors47504845_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_48443829_idx 592*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors1_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors1_deleted') THEN
			ALTER TABLE ko_tourzufactors1_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_48443829_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_48443829_idx;
		CREATE INDEX deleted_event_id_48443829_idx ON public.ko_tourzufactors1_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_47801421_idx 593*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors36698522_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors36698522_deleted') THEN
			ALTER TABLE ko_tourzufactors36698522_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_47801421_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_47801421_idx;
		CREATE INDEX deleted_event_id_47801421_idx ON public.ko_tourzufactors36698522_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_47801426_idx 594*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors36698522_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors36698522_deleted') THEN
			ALTER TABLE ko_touroksfactors36698522_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_47801426_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_47801426_idx;
		CREATE INDEX deleted_event_id_47801426_idx ON public.ko_touroksfactors36698522_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_266_q_pk 595*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_system_attribute_settings') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_system_attribute_settings') THEN
			ALTER TABLE ko_system_attribute_settings DROP CONSTRAINT IF EXISTS reg_266_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_266_q_pk;
		alter table KO_SYSTEM_ATTRIBUTE_SETTINGS add constraint reg_266_q_pk primary key (id);
	END IF;
END $$;

/*deleted_event_id_48446048_idx 596*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors48446047_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors48446047_deleted') THEN
			ALTER TABLE ko_tourzufactors48446047_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_48446048_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_48446048_idx;
		CREATE INDEX deleted_event_id_48446048_idx ON public.ko_tourzufactors48446047_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_2_a_49421360__pk 597*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49421360') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49421360') THEN
			ALTER TABLE gbu_source2_a_49421360 DROP CONSTRAINT IF EXISTS reg_2_a_49421360__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49421360__pk;
		CREATE UNIQUE INDEX reg_2_a_49421360__pk ON public.gbu_source2_a_49421360 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49421360_inx_obj_attr_id 598*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49421360') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49421360') THEN
			ALTER TABLE gbu_source2_a_49421360 DROP CONSTRAINT IF EXISTS reg_2_a_49421360_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49421360_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49421360_inx_obj_attr_id ON public.gbu_source2_a_49421360 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_49420749__pk 599*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420749') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420749') THEN
			ALTER TABLE gbu_source2_a_49420749 DROP CONSTRAINT IF EXISTS reg_2_a_49420749__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420749__pk;
		CREATE UNIQUE INDEX reg_2_a_49420749__pk ON public.gbu_source2_a_49420749 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420749_inx_obj_attr_id 600*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420749') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420749') THEN
			ALTER TABLE gbu_source2_a_49420749 DROP CONSTRAINT IF EXISTS reg_2_a_49420749_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420749_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420749_inx_obj_attr_id ON public.gbu_source2_a_49420749 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_47496789_q_pk 601*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors12506549') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors12506549') THEN
			ALTER TABLE ko_tourzufactors12506549 DROP CONSTRAINT IF EXISTS reg_47496789_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47496789_q_pk;
		CREATE UNIQUE INDEX reg_47496789_q_pk ON public.ko_tourzufactors12506549 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420750__pk 602*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420750') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420750') THEN
			ALTER TABLE gbu_source2_a_49420750 DROP CONSTRAINT IF EXISTS reg_2_a_49420750__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420750__pk;
		CREATE UNIQUE INDEX reg_2_a_49420750__pk ON public.gbu_source2_a_49420750 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420750_inx_obj_attr_id 603*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420750') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420750') THEN
			ALTER TABLE gbu_source2_a_49420750 DROP CONSTRAINT IF EXISTS reg_2_a_49420750_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420750_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420750_inx_obj_attr_id ON public.gbu_source2_a_49420750 USING btree (object_id, ot);
	END IF;
END $$;

/*z_query_test_data_ind_pkey 604*/
DO $$
BEGIN
	IF (SELECT to_regclass('z_query_test_data_ind') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='z_query_test_data_ind') THEN
			ALTER TABLE z_query_test_data_ind DROP CONSTRAINT IF EXISTS z_query_test_data_ind_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS z_query_test_data_ind_pkey;
		CREATE UNIQUE INDEX z_query_test_data_ind_pkey ON public.z_query_test_data_ind USING btree (id);
	END IF;
END $$;

/*z_query_test_data_ind_cadastral_number_idx 605*/
DO $$
BEGIN
	IF (SELECT to_regclass('z_query_test_data_ind') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='z_query_test_data_ind') THEN
			ALTER TABLE z_query_test_data_ind DROP CONSTRAINT IF EXISTS z_query_test_data_ind_cadastral_number_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS z_query_test_data_ind_cadastral_number_idx;
		CREATE INDEX z_query_test_data_ind_cadastral_number_idx ON public.z_query_test_data_ind USING btree (cadastral_number);
	END IF;
END $$;

/*z_query_test_data_ind_object_type_code_idx 606*/
DO $$
BEGIN
	IF (SELECT to_regclass('z_query_test_data_ind') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='z_query_test_data_ind') THEN
			ALTER TABLE z_query_test_data_ind DROP CONSTRAINT IF EXISTS z_query_test_data_ind_object_type_code_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS z_query_test_data_ind_object_type_code_idx;
		CREATE INDEX z_query_test_data_ind_object_type_code_idx ON public.z_query_test_data_ind USING btree (object_type_code);
	END IF;
END $$;

/*z_query_test_data_ind_kadastr_kvartal_idx 607*/
DO $$
BEGIN
	IF (SELECT to_regclass('z_query_test_data_ind') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='z_query_test_data_ind') THEN
			ALTER TABLE z_query_test_data_ind DROP CONSTRAINT IF EXISTS z_query_test_data_ind_kadastr_kvartal_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS z_query_test_data_ind_kadastr_kvartal_idx;
		CREATE INDEX z_query_test_data_ind_kadastr_kvartal_idx ON public.z_query_test_data_ind USING btree (kadastr_kvartal);
	END IF;
END $$;

/*z_query_test_data_no_ind_pkey 608*/
DO $$
BEGIN
	IF (SELECT to_regclass('z_query_test_data_no_ind') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='z_query_test_data_no_ind') THEN
			ALTER TABLE z_query_test_data_no_ind DROP CONSTRAINT IF EXISTS z_query_test_data_no_ind_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS z_query_test_data_no_ind_pkey;
		CREATE UNIQUE INDEX z_query_test_data_no_ind_pkey ON public.z_query_test_data_no_ind USING btree (id);
	END IF;
END $$;

/*reg_2_a_45__pk 609*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_45') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_45') THEN
			ALTER TABLE gbu_source2_a_45 DROP CONSTRAINT IF EXISTS reg_2_a_45__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_45__pk;
		CREATE UNIQUE INDEX reg_2_a_45__pk ON public.gbu_source2_a_45 USING btree (id);
	END IF;
END $$;

/*reg_2_a_45_inx_obj_attr_id 610*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_45') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_45') THEN
			ALTER TABLE gbu_source2_a_45 DROP CONSTRAINT IF EXISTS reg_2_a_45_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_45_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_45_inx_obj_attr_id ON public.gbu_source2_a_45 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_46__pk 611*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_46') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_46') THEN
			ALTER TABLE gbu_source2_a_46 DROP CONSTRAINT IF EXISTS reg_2_a_46__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_46__pk;
		CREATE UNIQUE INDEX reg_2_a_46__pk ON public.gbu_source2_a_46 USING btree (id);
	END IF;
END $$;

/*reg_2_a_46_inx_obj_attr_id 612*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_46') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_46') THEN
			ALTER TABLE gbu_source2_a_46 DROP CONSTRAINT IF EXISTS reg_2_a_46_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_46_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_46_inx_obj_attr_id ON public.gbu_source2_a_46 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_600__pk 613*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_600') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_600') THEN
			ALTER TABLE gbu_source2_a_600 DROP CONSTRAINT IF EXISTS reg_2_a_600__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_600__pk;
		CREATE UNIQUE INDEX reg_2_a_600__pk ON public.gbu_source2_a_600 USING btree (id);
	END IF;
END $$;

/*reg_2_a_600_inx_obj_attr_id 614*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_600') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_600') THEN
			ALTER TABLE gbu_source2_a_600 DROP CONSTRAINT IF EXISTS reg_2_a_600_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_600_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_600_inx_obj_attr_id ON public.gbu_source2_a_600 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_601__pk 615*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_601') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_601') THEN
			ALTER TABLE gbu_source2_a_601 DROP CONSTRAINT IF EXISTS reg_2_a_601__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_601__pk;
		CREATE UNIQUE INDEX reg_2_a_601__pk ON public.gbu_source2_a_601 USING btree (id);
	END IF;
END $$;

/*reg_2_a_601_inx_obj_attr_id 616*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_601') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_601') THEN
			ALTER TABLE gbu_source2_a_601 DROP CONSTRAINT IF EXISTS reg_2_a_601_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_601_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_601_inx_obj_attr_id ON public.gbu_source2_a_601 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_602__pk 617*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_602') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_602') THEN
			ALTER TABLE gbu_source2_a_602 DROP CONSTRAINT IF EXISTS reg_2_a_602__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_602__pk;
		CREATE UNIQUE INDEX reg_2_a_602__pk ON public.gbu_source2_a_602 USING btree (id);
	END IF;
END $$;

/*reg_2_a_602_inx_obj_attr_id 618*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_602') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_602') THEN
			ALTER TABLE gbu_source2_a_602 DROP CONSTRAINT IF EXISTS reg_2_a_602_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_602_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_602_inx_obj_attr_id ON public.gbu_source2_a_602 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_603__pk 619*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_603') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_603') THEN
			ALTER TABLE gbu_source2_a_603 DROP CONSTRAINT IF EXISTS reg_2_a_603__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_603__pk;
		CREATE UNIQUE INDEX reg_2_a_603__pk ON public.gbu_source2_a_603 USING btree (id);
	END IF;
END $$;

/*reg_2_a_603_inx_obj_attr_id 620*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_603') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_603') THEN
			ALTER TABLE gbu_source2_a_603 DROP CONSTRAINT IF EXISTS reg_2_a_603_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_603_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_603_inx_obj_attr_id ON public.gbu_source2_a_603 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_604__pk 621*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_604') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_604') THEN
			ALTER TABLE gbu_source2_a_604 DROP CONSTRAINT IF EXISTS reg_2_a_604__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_604__pk;
		CREATE UNIQUE INDEX reg_2_a_604__pk ON public.gbu_source2_a_604 USING btree (id);
	END IF;
END $$;

/*reg_2_a_604_inx_obj_attr_id 622*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_604') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_604') THEN
			ALTER TABLE gbu_source2_a_604 DROP CONSTRAINT IF EXISTS reg_2_a_604_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_604_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_604_inx_obj_attr_id ON public.gbu_source2_a_604 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_605__pk 623*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_605') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_605') THEN
			ALTER TABLE gbu_source2_a_605 DROP CONSTRAINT IF EXISTS reg_2_a_605__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_605__pk;
		CREATE UNIQUE INDEX reg_2_a_605__pk ON public.gbu_source2_a_605 USING btree (id);
	END IF;
END $$;

/*reg_2_a_605_inx_obj_attr_id 624*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_605') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_605') THEN
			ALTER TABLE gbu_source2_a_605 DROP CONSTRAINT IF EXISTS reg_2_a_605_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_605_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_605_inx_obj_attr_id ON public.gbu_source2_a_605 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_29_a_num_pk 625*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source29_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source29_a_num') THEN
			ALTER TABLE gbu_source29_a_num DROP CONSTRAINT IF EXISTS reg_29_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29_a_num_pk;
		CREATE UNIQUE INDEX reg_29_a_num_pk ON public.gbu_source29_a_num USING btree (id);
	END IF;
END $$;

/*reg_29_a_num_inx_obj_attr_id 626*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source29_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source29_a_num') THEN
			ALTER TABLE gbu_source29_a_num DROP CONSTRAINT IF EXISTS reg_29_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29_a_num_inx_obj_attr_id;
		CREATE INDEX reg_29_a_num_inx_obj_attr_id ON public.gbu_source29_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_29_a_dt_pk 627*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source29_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source29_a_dt') THEN
			ALTER TABLE gbu_source29_a_dt DROP CONSTRAINT IF EXISTS reg_29_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29_a_dt_pk;
		CREATE UNIQUE INDEX reg_29_a_dt_pk ON public.gbu_source29_a_dt USING btree (id);
	END IF;
END $$;

/*reg_29_a_dt_inx_obj_attr_id 628*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source29_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source29_a_dt') THEN
			ALTER TABLE gbu_source29_a_dt DROP CONSTRAINT IF EXISTS reg_29_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_29_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_29_a_dt_inx_obj_attr_id ON public.gbu_source29_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578514_a_num_pk 629*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_46_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_46_num') THEN
			ALTER TABLE gbu_custom_source_46_num DROP CONSTRAINT IF EXISTS reg_54578514_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578514_a_num_pk;
		CREATE UNIQUE INDEX reg_54578514_a_num_pk ON public.gbu_custom_source_46_num USING btree (id);
	END IF;
END $$;

/*reg_54578514_a_num_inx_obj_attr_id 630*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_46_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_46_num') THEN
			ALTER TABLE gbu_custom_source_46_num DROP CONSTRAINT IF EXISTS reg_54578514_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578514_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578514_a_num_inx_obj_attr_id ON public.gbu_custom_source_46_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_46811238_a_num_pk 631*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_32_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_32_num') THEN
			ALTER TABLE gbu_custom_source_32_num DROP CONSTRAINT IF EXISTS reg_46811238_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811238_a_num_pk;
		CREATE UNIQUE INDEX reg_46811238_a_num_pk ON public.gbu_custom_source_32_num USING btree (id);
	END IF;
END $$;

/*reg_46811238_a_num_inx_obj_attr_id 632*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_32_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_32_num') THEN
			ALTER TABLE gbu_custom_source_32_num DROP CONSTRAINT IF EXISTS reg_46811238_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811238_a_num_inx_obj_attr_id;
		CREATE INDEX reg_46811238_a_num_inx_obj_attr_id ON public.gbu_custom_source_32_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_900_q_pk 633*/
DO $$
BEGIN
	IF (SELECT to_regclass('spd_request_registration') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='spd_request_registration') THEN
			ALTER TABLE spd_request_registration DROP CONSTRAINT IF EXISTS reg_900_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_900_q_pk;
		alter table SPD_REQUEST_REGISTRATION add constraint reg_900_q_pk primary key (id);
	END IF;
END $$;

/*reg_46811238_a_dt_pk 634*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_32_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_32_dt') THEN
			ALTER TABLE gbu_custom_source_32_dt DROP CONSTRAINT IF EXISTS reg_46811238_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811238_a_dt_pk;
		CREATE UNIQUE INDEX reg_46811238_a_dt_pk ON public.gbu_custom_source_32_dt USING btree (id);
	END IF;
END $$;

/*reg_46811238_a_dt_inx_obj_attr_id 635*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_32_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_32_dt') THEN
			ALTER TABLE gbu_custom_source_32_dt DROP CONSTRAINT IF EXISTS reg_46811238_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_46811238_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_46811238_a_dt_inx_obj_attr_id ON public.gbu_custom_source_32_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578514_a_dt_pk 636*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_46_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_46_dt') THEN
			ALTER TABLE gbu_custom_source_46_dt DROP CONSTRAINT IF EXISTS reg_54578514_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578514_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578514_a_dt_pk ON public.gbu_custom_source_46_dt USING btree (id);
	END IF;
END $$;

/*reg_54578514_a_dt_inx_obj_attr_id 637*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_46_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_46_dt') THEN
			ALTER TABLE gbu_custom_source_46_dt DROP CONSTRAINT IF EXISTS reg_54578514_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578514_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578514_a_dt_inx_obj_attr_id ON public.gbu_custom_source_46_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_30_a_txt_pk 638*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source30_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source30_a_txt') THEN
			ALTER TABLE gbu_source30_a_txt DROP CONSTRAINT IF EXISTS reg_30_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_30_a_txt_pk;
		CREATE UNIQUE INDEX reg_30_a_txt_pk ON public.gbu_source30_a_txt USING btree (id);
	END IF;
END $$;

/*reg_30_a_txt_inx_obj_attr_id 639*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source30_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source30_a_txt') THEN
			ALTER TABLE gbu_source30_a_txt DROP CONSTRAINT IF EXISTS reg_30_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_30_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_30_a_txt_inx_obj_attr_id ON public.gbu_source30_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_211_q_pk 640*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_mark_catalog') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_mark_catalog') THEN
			ALTER TABLE ko_mark_catalog DROP CONSTRAINT IF EXISTS reg_211_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_211_q_pk;
		alter table KO_MARK_CATALOG add constraint reg_211_q_pk primary key (id);
	END IF;
END $$;

/*group_id_idx 641*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_mark_catalog') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_mark_catalog') THEN
			ALTER TABLE ko_mark_catalog DROP CONSTRAINT IF EXISTS group_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS group_id_idx;
		CREATE INDEX group_id_idx ON public.ko_mark_catalog USING btree (group_id);
	END IF;
END $$;

/*factor_id_idx 642*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_mark_catalog') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_mark_catalog') THEN
			ALTER TABLE ko_mark_catalog DROP CONSTRAINT IF EXISTS factor_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS factor_id_idx;
		CREATE INDEX factor_id_idx ON public.ko_mark_catalog USING btree (factor_id);
	END IF;
END $$;

/*value_factor_idx 643*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_mark_catalog') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_mark_catalog') THEN
			ALTER TABLE ko_mark_catalog DROP CONSTRAINT IF EXISTS value_factor_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS value_factor_idx;
		CREATE INDEX value_factor_idx ON public.ko_mark_catalog USING btree (value_factor);
	END IF;
END $$;

/*reg_30_a_num_pk 644*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source30_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source30_a_num') THEN
			ALTER TABLE gbu_source30_a_num DROP CONSTRAINT IF EXISTS reg_30_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_30_a_num_pk;
		CREATE UNIQUE INDEX reg_30_a_num_pk ON public.gbu_source30_a_num USING btree (id);
	END IF;
END $$;

/*reg_30_a_num_inx_obj_attr_id 645*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source30_a_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source30_a_num') THEN
			ALTER TABLE gbu_source30_a_num DROP CONSTRAINT IF EXISTS reg_30_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_30_a_num_inx_obj_attr_id;
		CREATE INDEX reg_30_a_num_inx_obj_attr_id ON public.gbu_source30_a_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54579037_a_txt_pk 646*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_53_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_53_txt') THEN
			ALTER TABLE gbu_custom_source_53_txt DROP CONSTRAINT IF EXISTS reg_54579037_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54579037_a_txt_pk;
		CREATE UNIQUE INDEX reg_54579037_a_txt_pk ON public.gbu_custom_source_53_txt USING btree (id);
	END IF;
END $$;

/*reg_54579037_a_txt_inx_obj_attr_id 647*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_53_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_53_txt') THEN
			ALTER TABLE gbu_custom_source_53_txt DROP CONSTRAINT IF EXISTS reg_54579037_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54579037_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54579037_a_txt_inx_obj_attr_id ON public.gbu_custom_source_53_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_48443829_q_pk 648*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors1') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors1') THEN
			ALTER TABLE ko_tourzufactors1 DROP CONSTRAINT IF EXISTS reg_48443829_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_48443829_q_pk;
		CREATE UNIQUE INDEX reg_48443829_q_pk ON public.ko_tourzufactors1 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420754__pk 649*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420754') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420754') THEN
			ALTER TABLE gbu_source2_a_49420754 DROP CONSTRAINT IF EXISTS reg_2_a_49420754__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420754__pk;
		CREATE UNIQUE INDEX reg_2_a_49420754__pk ON public.gbu_source2_a_49420754 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420754_inx_obj_attr_id 650*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420754') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420754') THEN
			ALTER TABLE gbu_source2_a_49420754 DROP CONSTRAINT IF EXISTS reg_2_a_49420754_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420754_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420754_inx_obj_attr_id ON public.gbu_source2_a_49420754 USING btree (object_id, ot);
	END IF;
END $$;

/*dashboards_panelindexcardcache_pkey 651*/
DO $$
BEGIN
	IF (SELECT to_regclass('dashboards_panelindexcardcache') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='dashboards_panelindexcardcache') THEN
			ALTER TABLE dashboards_panelindexcardcache DROP CONSTRAINT IF EXISTS dashboards_panelindexcardcache_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS dashboards_panelindexcardcache_pkey;
		alter table DASHBOARDS_PANELINDEXCARDCACHE add constraint dashboards_panelindexcardcache_pkey primary key (id);
	END IF;
END $$;

/*dashboards_panelindexcardcache_key_key 652*/
DO $$
BEGIN
	IF (SELECT to_regclass('dashboards_panelindexcardcache') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='dashboards_panelindexcardcache') THEN
			ALTER TABLE dashboards_panelindexcardcache DROP CONSTRAINT IF EXISTS dashboards_panelindexcardcache_key_key RESTRICT;
		END IF;
		DROP INDEX IF EXISTS dashboards_panelindexcardcache_key_key;
		CREATE UNIQUE INDEX dashboards_panelindexcardcache_key_key ON public.dashboards_panelindexcardcache USING btree (key);
	END IF;
END $$;

/*reg_50437125_q_pk 653*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors50437120') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors50437120') THEN
			ALTER TABLE ko_touroksfactors50437120 DROP CONSTRAINT IF EXISTS reg_50437125_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_50437125_q_pk;
		CREATE UNIQUE INDEX reg_50437125_q_pk ON public.ko_touroksfactors50437120 USING btree (id);
	END IF;
END $$;

/*reg_48404844_q_pk 654*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors38670860') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors38670860') THEN
			ALTER TABLE ko_touroksfactors38670860 DROP CONSTRAINT IF EXISTS reg_48404844_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_48404844_q_pk;
		CREATE UNIQUE INDEX reg_48404844_q_pk ON public.ko_touroksfactors38670860 USING btree (id);
	END IF;
END $$;

/*es_exspress_score_pkey 655*/
DO $$
BEGIN
	IF (SELECT to_regclass('es_express_score') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='es_express_score') THEN
			ALTER TABLE es_express_score DROP CONSTRAINT IF EXISTS es_exspress_score_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS es_exspress_score_pkey;
		alter table ES_EXPRESS_SCORE add constraint es_exspress_score_pkey primary key (id);
	END IF;
END $$;

/*ko_unit_property_type_code_idx 656*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS ko_unit_property_type_code_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_unit_property_type_code_idx;
		CREATE INDEX ko_unit_property_type_code_idx ON public.ko_unit USING btree (property_type_code);
	END IF;
END $$;

/*reg_201_q_pk 657*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS reg_201_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_201_q_pk;
		alter table KO_UNIT add constraint reg_201_q_pk primary key (id);
	END IF;
END $$;

/*cadastralnumberindex 658*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS cadastralnumberindex RESTRICT;
		END IF;
		DROP INDEX IF EXISTS cadastralnumberindex;
		CREATE INDEX cadastralnumberindex ON public.ko_unit USING btree (cadastral_number);
	END IF;
END $$;

/*groupidindex 659*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS groupidindex RESTRICT;
		END IF;
		DROP INDEX IF EXISTS groupidindex;
		CREATE INDEX groupidindex ON public.ko_unit USING btree (group_id);
	END IF;
END $$;

/*"CadastralNumberIndex" 660*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS "CadastralNumberIndex" RESTRICT;
		END IF;
		DROP INDEX IF EXISTS "CadastralNumberIndex";
		CREATE INDEX "CadastralNumberIndex" ON public.ko_unit USING btree (cadastral_number);
	END IF;
END $$;

/*"GroupIdIndex" 661*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS "GroupIdIndex" RESTRICT;
		END IF;
		DROP INDEX IF EXISTS "GroupIdIndex";
		CREATE INDEX "GroupIdIndex" ON public.ko_unit USING btree (group_id);
	END IF;
END $$;

/*ko_unit_task_id_idx 662*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS ko_unit_task_id_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_unit_task_id_idx;
		CREATE INDEX ko_unit_task_id_idx ON public.ko_unit USING btree (task_id);
	END IF;
END $$;

/*ko_unit_kad_num_text_inx 663*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS ko_unit_kad_num_text_inx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_unit_kad_num_text_inx;
		CREATE INDEX ko_unit_kad_num_text_inx ON public.ko_unit USING gin (cadastral_number gin_trgm_ops);
	END IF;
END $$;

/*ko_unit_obj_idx 664*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit') THEN
			ALTER TABLE ko_unit DROP CONSTRAINT IF EXISTS ko_unit_obj_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_unit_obj_idx;
		CREATE INDEX ko_unit_obj_idx ON public.ko_unit USING btree (object_id);
	END IF;
END $$;

/*ko_result_send_journal_pkey 665*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_result_send_journal') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_result_send_journal') THEN
			ALTER TABLE ko_result_send_journal DROP CONSTRAINT IF EXISTS ko_result_send_journal_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_result_send_journal_pkey;
		alter table KO_RESULT_SEND_JOURNAL add constraint ko_result_send_journal_pkey primary key (id);
	END IF;
END $$;

/*reg_48443837_q_pk 666*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors1') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors1') THEN
			ALTER TABLE ko_touroksfactors1 DROP CONSTRAINT IF EXISTS reg_48443837_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_48443837_q_pk;
		CREATE UNIQUE INDEX reg_48443837_q_pk ON public.ko_touroksfactors1 USING btree (id);
	END IF;
END $$;

/*reg_47801421_q_pk 667*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors36698522') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors36698522') THEN
			ALTER TABLE ko_tourzufactors36698522 DROP CONSTRAINT IF EXISTS reg_47801421_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47801421_q_pk;
		CREATE UNIQUE INDEX reg_47801421_q_pk ON public.ko_tourzufactors36698522 USING btree (id);
	END IF;
END $$;

/*reg_210_q_pk 668*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_model_factor') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_model_factor') THEN
			ALTER TABLE ko_model_factor DROP CONSTRAINT IF EXISTS reg_210_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_210_q_pk;
		alter table KO_MODEL_FACTOR add constraint reg_210_q_pk primary key (id);
	END IF;
END $$;

/*reg_54578518_a_txt_pk 669*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_47_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_47_txt') THEN
			ALTER TABLE gbu_custom_source_47_txt DROP CONSTRAINT IF EXISTS reg_54578518_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578518_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578518_a_txt_pk ON public.gbu_custom_source_47_txt USING btree (id);
	END IF;
END $$;

/*reg_54578518_a_txt_inx_obj_attr_id 670*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_47_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_47_txt') THEN
			ALTER TABLE gbu_custom_source_47_txt DROP CONSTRAINT IF EXISTS reg_54578518_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578518_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578518_a_txt_inx_obj_attr_id ON public.gbu_custom_source_47_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_49413060_a_txt_pk 671*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_37_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_37_txt') THEN
			ALTER TABLE gbu_custom_source_37_txt DROP CONSTRAINT IF EXISTS reg_49413060_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49413060_a_txt_pk;
		CREATE UNIQUE INDEX reg_49413060_a_txt_pk ON public.gbu_custom_source_37_txt USING btree (id);
	END IF;
END $$;

/*reg_49413060_a_txt_inx_obj_attr_id 672*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_37_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_37_txt') THEN
			ALTER TABLE gbu_custom_source_37_txt DROP CONSTRAINT IF EXISTS reg_49413060_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49413060_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_49413060_a_txt_inx_obj_attr_id ON public.gbu_custom_source_37_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_118_q_pk 673*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_coeff_for_outliers_checking') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_coeff_for_outliers_checking') THEN
			ALTER TABLE market_coeff_for_outliers_checking DROP CONSTRAINT IF EXISTS reg_118_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_118_q_pk;
		alter table market_coeff_for_outliers_checking add constraint reg_118_q_pk primary key (id);
	END IF;
END $$;

/*zone_district_region_unique_indx 674*/
DO $$
BEGIN
	IF (SELECT to_regclass('market_coeff_for_outliers_checking') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='market_coeff_for_outliers_checking') THEN
			ALTER TABLE market_coeff_for_outliers_checking DROP CONSTRAINT IF EXISTS zone_district_region_unique_indx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS zone_district_region_unique_indx;
		CREATE UNIQUE INDEX zone_district_region_unique_indx ON public.market_coeff_for_outliers_checking USING btree (zone, district_code, region_code);
	END IF;
END $$;

/*reg_54578518_a_num_pk 675*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_47_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_47_num') THEN
			ALTER TABLE gbu_custom_source_47_num DROP CONSTRAINT IF EXISTS reg_54578518_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578518_a_num_pk;
		CREATE UNIQUE INDEX reg_54578518_a_num_pk ON public.gbu_custom_source_47_num USING btree (id);
	END IF;
END $$;

/*reg_54578518_a_num_inx_obj_attr_id 676*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_47_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_47_num') THEN
			ALTER TABLE gbu_custom_source_47_num DROP CONSTRAINT IF EXISTS reg_54578518_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578518_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578518_a_num_inx_obj_attr_id ON public.gbu_custom_source_47_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_49413060_a_num_pk 677*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_37_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_37_num') THEN
			ALTER TABLE gbu_custom_source_37_num DROP CONSTRAINT IF EXISTS reg_49413060_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49413060_a_num_pk;
		CREATE UNIQUE INDEX reg_49413060_a_num_pk ON public.gbu_custom_source_37_num USING btree (id);
	END IF;
END $$;

/*reg_49413060_a_num_inx_obj_attr_id 678*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_37_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_37_num') THEN
			ALTER TABLE gbu_custom_source_37_num DROP CONSTRAINT IF EXISTS reg_49413060_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49413060_a_num_inx_obj_attr_id;
		CREATE INDEX reg_49413060_a_num_inx_obj_attr_id ON public.gbu_custom_source_37_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_206_idx 679*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_model_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_model_deleted') THEN
			ALTER TABLE ko_model_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_206_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_206_idx;
		CREATE INDEX deleted_event_id_206_idx ON public.ko_model_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_49413060_a_dt_pk 680*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_37_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_37_dt') THEN
			ALTER TABLE gbu_custom_source_37_dt DROP CONSTRAINT IF EXISTS reg_49413060_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49413060_a_dt_pk;
		CREATE UNIQUE INDEX reg_49413060_a_dt_pk ON public.gbu_custom_source_37_dt USING btree (id);
	END IF;
END $$;

/*reg_49413060_a_dt_inx_obj_attr_id 681*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_37_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_37_dt') THEN
			ALTER TABLE gbu_custom_source_37_dt DROP CONSTRAINT IF EXISTS reg_49413060_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49413060_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_49413060_a_dt_inx_obj_attr_id ON public.gbu_custom_source_37_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578518_a_dt_pk 682*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_47_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_47_dt') THEN
			ALTER TABLE gbu_custom_source_47_dt DROP CONSTRAINT IF EXISTS reg_54578518_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578518_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578518_a_dt_pk ON public.gbu_custom_source_47_dt USING btree (id);
	END IF;
END $$;

/*reg_54578518_a_dt_inx_obj_attr_id 683*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_47_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_47_dt') THEN
			ALTER TABLE gbu_custom_source_47_dt DROP CONSTRAINT IF EXISTS reg_54578518_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578518_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578518_a_dt_inx_obj_attr_id ON public.gbu_custom_source_47_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_47801426_q_pk 684*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors36698522') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors36698522') THEN
			ALTER TABLE ko_touroksfactors36698522 DROP CONSTRAINT IF EXISTS reg_47801426_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47801426_q_pk;
		CREATE UNIQUE INDEX reg_47801426_q_pk ON public.ko_touroksfactors36698522 USING btree (id);
	END IF;
END $$;

/*deleted_event_id_208_idx 685*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_group_factor_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_group_factor_deleted') THEN
			ALTER TABLE ko_group_factor_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_208_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_208_idx;
		CREATE INDEX deleted_event_id_208_idx ON public.ko_group_factor_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_2_a_49420761__pk 686*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420761') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420761') THEN
			ALTER TABLE gbu_source2_a_49420761 DROP CONSTRAINT IF EXISTS reg_2_a_49420761__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420761__pk;
		CREATE UNIQUE INDEX reg_2_a_49420761__pk ON public.gbu_source2_a_49420761 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420761_inx_obj_attr_id 687*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420761') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420761') THEN
			ALTER TABLE gbu_source2_a_49420761 DROP CONSTRAINT IF EXISTS reg_2_a_49420761_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420761_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420761_inx_obj_attr_id ON public.gbu_source2_a_49420761 USING btree (object_id, ot);
	END IF;
END $$;

/*deleted_event_id_260_idx 688*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_auto_calculation_settings_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_auto_calculation_settings_deleted') THEN
			ALTER TABLE ko_auto_calculation_settings_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_260_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_260_idx;
		CREATE INDEX deleted_event_id_260_idx ON public.ko_auto_calculation_settings_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_210_idx 689*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_model_factor_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_model_factor_deleted') THEN
			ALTER TABLE ko_model_factor_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_210_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_210_idx;
		CREATE INDEX deleted_event_id_210_idx ON public.ko_model_factor_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_702_idx 690*/
DO $$
BEGIN
	IF (SELECT to_regclass('modeling_model_to_market_objects_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='modeling_model_to_market_objects_deleted') THEN
			ALTER TABLE modeling_model_to_market_objects_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_702_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_702_idx;
		CREATE INDEX deleted_event_id_702_idx ON public.modeling_model_to_market_objects_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_255_idx 691*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_calc_group_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_calc_group_deleted') THEN
			ALTER TABLE ko_calc_group_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_255_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_255_idx;
		CREATE INDEX deleted_event_id_255_idx ON public.ko_calc_group_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_222_idx 692*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_group_to_market_segment_relation_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_group_to_market_segment_relation_deleted') THEN
			ALTER TABLE ko_group_to_market_segment_relation_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_222_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_222_idx;
		CREATE INDEX deleted_event_id_222_idx ON public.ko_group_to_market_segment_relation_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_212_idx 693*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tour_groups_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tour_groups_deleted') THEN
			ALTER TABLE ko_tour_groups_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_212_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_212_idx;
		CREATE INDEX deleted_event_id_212_idx ON public.ko_tour_groups_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_205_idx 694*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_group_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_group_deleted') THEN
			ALTER TABLE ko_group_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_205_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_205_idx;
		CREATE INDEX deleted_event_id_205_idx ON public.ko_group_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_702_q_pk 695*/
DO $$
BEGIN
	IF (SELECT to_regclass('modeling_model_to_market_objects') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='modeling_model_to_market_objects') THEN
			ALTER TABLE modeling_model_to_market_objects DROP CONSTRAINT IF EXISTS reg_702_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_702_q_pk;
		alter table MODELING_MODEL_TO_MARKET_OBJECTS add constraint reg_702_q_pk primary key (id);
	END IF;
END $$;

/*reg_2_a_49413074__pk 696*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49413074') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49413074') THEN
			ALTER TABLE gbu_source2_a_49413074 DROP CONSTRAINT IF EXISTS reg_2_a_49413074__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49413074__pk;
		CREATE UNIQUE INDEX reg_2_a_49413074__pk ON public.gbu_source2_a_49413074 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49413074_inx_obj_attr_id 697*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49413074') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49413074') THEN
			ALTER TABLE gbu_source2_a_49413074 DROP CONSTRAINT IF EXISTS reg_2_a_49413074_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49413074_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49413074_inx_obj_attr_id ON public.gbu_source2_a_49413074 USING btree (object_id, ot);
	END IF;
END $$;

/*deleted_event_id_219_idx 698*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tour_factor_register_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tour_factor_register_deleted') THEN
			ALTER TABLE ko_tour_factor_register_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_219_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_219_idx;
		CREATE INDEX deleted_event_id_219_idx ON public.ko_tour_factor_register_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_202_idx 699*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tour_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tour_deleted') THEN
			ALTER TABLE ko_tour_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_202_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_202_idx;
		CREATE INDEX deleted_event_id_202_idx ON public.ko_tour_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_2_a_49420762__pk 700*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420762') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420762') THEN
			ALTER TABLE gbu_source2_a_49420762 DROP CONSTRAINT IF EXISTS reg_2_a_49420762__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420762__pk;
		CREATE UNIQUE INDEX reg_2_a_49420762__pk ON public.gbu_source2_a_49420762 USING btree (id);
	END IF;
END $$;

/*reg_2_a_49420762_inx_obj_attr_id 701*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49420762') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49420762') THEN
			ALTER TABLE gbu_source2_a_49420762 DROP CONSTRAINT IF EXISTS reg_2_a_49420762_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_49420762_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_49420762_inx_obj_attr_id ON public.gbu_source2_a_49420762 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_54578520_a_txt_pk 702*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_48_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_48_txt') THEN
			ALTER TABLE gbu_custom_source_48_txt DROP CONSTRAINT IF EXISTS reg_54578520_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578520_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578520_a_txt_pk ON public.gbu_custom_source_48_txt USING btree (id);
	END IF;
END $$;

/*reg_54578520_a_txt_inx_obj_attr_id 703*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_48_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_48_txt') THEN
			ALTER TABLE gbu_custom_source_48_txt DROP CONSTRAINT IF EXISTS reg_54578520_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578520_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578520_a_txt_inx_obj_attr_id ON public.gbu_custom_source_48_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_254_idx 704*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_compliance_guide_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_compliance_guide_deleted') THEN
			ALTER TABLE ko_compliance_guide_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_254_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_254_idx;
		CREATE INDEX deleted_event_id_254_idx ON public.ko_compliance_guide_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_223_q_pk 705*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_model_training_result_images') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_model_training_result_images') THEN
			ALTER TABLE ko_model_training_result_images DROP CONSTRAINT IF EXISTS reg_223_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_223_q_pk;
		alter table ko_model_training_result_images add constraint reg_223_q_pk primary key (id);
	END IF;
END $$;

/*reg_54578520_a_num_pk 706*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_48_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_48_num') THEN
			ALTER TABLE gbu_custom_source_48_num DROP CONSTRAINT IF EXISTS reg_54578520_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578520_a_num_pk;
		CREATE UNIQUE INDEX reg_54578520_a_num_pk ON public.gbu_custom_source_48_num USING btree (id);
	END IF;
END $$;

/*reg_54578520_a_num_inx_obj_attr_id 707*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_48_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_48_num') THEN
			ALTER TABLE gbu_custom_source_48_num DROP CONSTRAINT IF EXISTS reg_54578520_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578520_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578520_a_num_inx_obj_attr_id ON public.gbu_custom_source_48_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54579037_a_dt_pk 708*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_53_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_53_dt') THEN
			ALTER TABLE gbu_custom_source_53_dt DROP CONSTRAINT IF EXISTS reg_54579037_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54579037_a_dt_pk;
		CREATE UNIQUE INDEX reg_54579037_a_dt_pk ON public.gbu_custom_source_53_dt USING btree (id);
	END IF;
END $$;

/*reg_54579037_a_dt_inx_obj_attr_id 709*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_53_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_53_dt') THEN
			ALTER TABLE gbu_custom_source_53_dt DROP CONSTRAINT IF EXISTS reg_54579037_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54579037_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54579037_a_dt_inx_obj_attr_id ON public.gbu_custom_source_53_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578520_a_dt_pk 710*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_48_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_48_dt') THEN
			ALTER TABLE gbu_custom_source_48_dt DROP CONSTRAINT IF EXISTS reg_54578520_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578520_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578520_a_dt_pk ON public.gbu_custom_source_48_dt USING btree (id);
	END IF;
END $$;

/*reg_54578520_a_dt_inx_obj_attr_id 711*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_48_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_48_dt') THEN
			ALTER TABLE gbu_custom_source_48_dt DROP CONSTRAINT IF EXISTS reg_54578520_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578520_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578520_a_dt_inx_obj_attr_id ON public.gbu_custom_source_48_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54579037_a_num_pk 712*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_53_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_53_num') THEN
			ALTER TABLE gbu_custom_source_53_num DROP CONSTRAINT IF EXISTS reg_54579037_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54579037_a_num_pk;
		CREATE UNIQUE INDEX reg_54579037_a_num_pk ON public.gbu_custom_source_53_num USING btree (id);
	END IF;
END $$;

/*reg_54579037_a_num_inx_obj_attr_id 713*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_53_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_53_num') THEN
			ALTER TABLE gbu_custom_source_53_num DROP CONSTRAINT IF EXISTS reg_54579037_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54579037_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54579037_a_num_inx_obj_attr_id ON public.gbu_custom_source_53_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_252_idx 714*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_params_oks_2016_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_params_oks_2016_deleted') THEN
			ALTER TABLE ko_unit_params_oks_2016_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_252_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_252_idx;
		CREATE INDEX deleted_event_id_252_idx ON public.ko_unit_params_oks_2016_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_253_idx 715*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_params_zu_2016_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_params_zu_2016_deleted') THEN
			ALTER TABLE ko_unit_params_zu_2016_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_253_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_253_idx;
		CREATE INDEX deleted_event_id_253_idx ON public.ko_unit_params_zu_2016_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_39959492_idx 716*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors18099454_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors18099454_deleted') THEN
			ALTER TABLE ko_tourzufactors18099454_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_39959492_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_39959492_idx;
		CREATE INDEX deleted_event_id_39959492_idx ON public.ko_tourzufactors18099454_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_29504581_idx 717*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors29425150_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors29425150_deleted') THEN
			ALTER TABLE ko_touroksfactors29425150_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_29504581_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_29504581_idx;
		CREATE INDEX deleted_event_id_29504581_idx ON public.ko_touroksfactors29425150_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_41707103_idx 718*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors41707102_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors41707102_deleted') THEN
			ALTER TABLE ko_tourzufactors41707102_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_41707103_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_41707103_idx;
		CREATE INDEX deleted_event_id_41707103_idx ON public.ko_tourzufactors41707102_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_22986693_idx 719*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors12506545_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors12506545_deleted') THEN
			ALTER TABLE ko_tourzufactors12506545_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_22986693_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_22986693_idx;
		CREATE INDEX deleted_event_id_22986693_idx ON public.ko_tourzufactors12506545_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_54578526_a_txt_pk 720*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_49_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_49_txt') THEN
			ALTER TABLE gbu_custom_source_49_txt DROP CONSTRAINT IF EXISTS reg_54578526_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578526_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578526_a_txt_pk ON public.gbu_custom_source_49_txt USING btree (id);
	END IF;
END $$;

/*reg_54578526_a_txt_inx_obj_attr_id 721*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_49_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_49_txt') THEN
			ALTER TABLE gbu_custom_source_49_txt DROP CONSTRAINT IF EXISTS reg_54578526_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578526_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578526_a_txt_inx_obj_attr_id ON public.gbu_custom_source_49_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_251_idx 722*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_params_zu_2018_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_params_zu_2018_deleted') THEN
			ALTER TABLE ko_unit_params_zu_2018_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_251_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_251_idx;
		CREATE INDEX deleted_event_id_251_idx ON public.ko_unit_params_zu_2018_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_250_idx 723*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_params_oks_2018_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_params_oks_2018_deleted') THEN
			ALTER TABLE ko_unit_params_oks_2018_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_250_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_250_idx;
		CREATE INDEX deleted_event_id_250_idx ON public.ko_unit_params_oks_2018_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_27388644_idx 724*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors12506547_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors12506547_deleted') THEN
			ALTER TABLE ko_tourzufactors12506547_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_27388644_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_27388644_idx;
		CREATE INDEX deleted_event_id_27388644_idx ON public.ko_tourzufactors12506547_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_27407381_idx 725*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors12506547_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors12506547_deleted') THEN
			ALTER TABLE ko_touroksfactors12506547_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_27407381_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_27407381_idx;
		CREATE INDEX deleted_event_id_27407381_idx ON public.ko_touroksfactors12506547_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_29503139_idx 726*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors29425150_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors29425150_deleted') THEN
			ALTER TABLE ko_tourzufactors29425150_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_29503139_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_29503139_idx;
		CREATE INDEX deleted_event_id_29503139_idx ON public.ko_tourzufactors29425150_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_29860805_idx 727*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors29839891_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors29839891_deleted') THEN
			ALTER TABLE ko_tourzufactors29839891_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_29860805_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_29860805_idx;
		CREATE INDEX deleted_event_id_29860805_idx ON public.ko_tourzufactors29839891_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_29871819_idx 728*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors29839891_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors29839891_deleted') THEN
			ALTER TABLE ko_touroksfactors29839891_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_29871819_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_29871819_idx;
		CREATE INDEX deleted_event_id_29871819_idx ON public.ko_touroksfactors29839891_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_41707106_idx 729*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors41707102_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors41707102_deleted') THEN
			ALTER TABLE ko_touroksfactors41707102_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_41707106_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_41707106_idx;
		CREATE INDEX deleted_event_id_41707106_idx ON public.ko_touroksfactors41707102_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_48404844_idx 730*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors38670860_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors38670860_deleted') THEN
			ALTER TABLE ko_touroksfactors38670860_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_48404844_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_48404844_idx;
		CREATE INDEX deleted_event_id_48404844_idx ON public.ko_touroksfactors38670860_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_48443837_idx 731*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors1_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors1_deleted') THEN
			ALTER TABLE ko_touroksfactors1_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_48443837_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_48443837_idx;
		CREATE INDEX deleted_event_id_48443837_idx ON public.ko_touroksfactors1_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_50437125_idx 732*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors50437120_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors50437120_deleted') THEN
			ALTER TABLE ko_touroksfactors50437120_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_50437125_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_50437125_idx;
		CREATE INDEX deleted_event_id_50437125_idx ON public.ko_touroksfactors50437120_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_48400455_idx 733*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors38670860_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors38670860_deleted') THEN
			ALTER TABLE ko_tourzufactors38670860_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_48400455_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_48400455_idx;
		CREATE INDEX deleted_event_id_48400455_idx ON public.ko_tourzufactors38670860_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_47496789_idx 734*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors12506549_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors12506549_deleted') THEN
			ALTER TABLE ko_tourzufactors12506549_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_47496789_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_47496789_idx;
		CREATE INDEX deleted_event_id_47496789_idx ON public.ko_tourzufactors12506549_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_47496917_idx 735*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors12506549_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors12506549_deleted') THEN
			ALTER TABLE ko_touroksfactors12506549_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_47496917_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_47496917_idx;
		CREATE INDEX deleted_event_id_47496917_idx ON public.ko_touroksfactors12506549_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_48446061_idx 736*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors48446047_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors48446047_deleted') THEN
			ALTER TABLE ko_touroksfactors48446047_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_48446061_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_48446061_idx;
		CREATE INDEX deleted_event_id_48446061_idx ON public.ko_touroksfactors48446047_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_50437121_idx 737*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors50437120_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors50437120_deleted') THEN
			ALTER TABLE ko_tourzufactors50437120_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_50437121_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_50437121_idx;
		CREATE INDEX deleted_event_id_50437121_idx ON public.ko_tourzufactors50437120_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_256_idx 738*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_change_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_change_deleted') THEN
			ALTER TABLE ko_unit_change_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_256_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_256_idx;
		CREATE INDEX deleted_event_id_256_idx ON public.ko_unit_change_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_216_idx 739*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_cost_rosreestr_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_cost_rosreestr_deleted') THEN
			ALTER TABLE ko_cost_rosreestr_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_216_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_216_idx;
		CREATE INDEX deleted_event_id_216_idx ON public.ko_cost_rosreestr_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_801_idx 740*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_import_data_log_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_import_data_log_deleted') THEN
			ALTER TABLE common_import_data_log_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_801_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_801_idx;
		CREATE INDEX deleted_event_id_801_idx ON public.common_import_data_log_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_975_idx 741*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_long_process_queue_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_long_process_queue_deleted') THEN
			ALTER TABLE core_long_process_queue_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_975_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_975_idx;
		CREATE INDEX deleted_event_id_975_idx ON public.core_long_process_queue_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_201_idx 742*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unit_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unit_deleted') THEN
			ALTER TABLE ko_unit_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_201_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_201_idx;
		CREATE INDEX deleted_event_id_201_idx ON public.ko_unit_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_203_idx 743*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_task_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_task_deleted') THEN
			ALTER TABLE ko_task_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_203_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_203_idx;
		CREATE INDEX deleted_event_id_203_idx ON public.ko_task_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_1_idx 744*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_1_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_1_deleted') THEN
			ALTER TABLE gbu_source2_a_1_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_1_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_1_idx;
		CREATE INDEX deleted_event_id_2_1_idx ON public.gbu_source2_a_1_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_2_idx 745*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_2_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_2_deleted') THEN
			ALTER TABLE gbu_source2_a_2_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_2_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_2_idx;
		CREATE INDEX deleted_event_id_2_2_idx ON public.gbu_source2_a_2_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_4_idx 746*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_4_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_4_deleted') THEN
			ALTER TABLE gbu_source2_a_4_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_4_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_4_idx;
		CREATE INDEX deleted_event_id_2_4_idx ON public.gbu_source2_a_4_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_5_idx 747*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_5_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_5_deleted') THEN
			ALTER TABLE gbu_source2_a_5_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_5_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_5_idx;
		CREATE INDEX deleted_event_id_2_5_idx ON public.gbu_source2_a_5_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_6_idx 748*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_6_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_6_deleted') THEN
			ALTER TABLE gbu_source2_a_6_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_6_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_6_idx;
		CREATE INDEX deleted_event_id_2_6_idx ON public.gbu_source2_a_6_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_8_idx 749*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_8_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_8_deleted') THEN
			ALTER TABLE gbu_source2_a_8_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_8_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_8_idx;
		CREATE INDEX deleted_event_id_2_8_idx ON public.gbu_source2_a_8_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_13_idx 750*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_13_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_13_deleted') THEN
			ALTER TABLE gbu_source2_a_13_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_13_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_13_idx;
		CREATE INDEX deleted_event_id_2_13_idx ON public.gbu_source2_a_13_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_14_idx 751*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_14_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_14_deleted') THEN
			ALTER TABLE gbu_source2_a_14_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_14_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_14_idx;
		CREATE INDEX deleted_event_id_2_14_idx ON public.gbu_source2_a_14_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_17_idx 752*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_17_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_17_deleted') THEN
			ALTER TABLE gbu_source2_a_17_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_17_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_17_idx;
		CREATE INDEX deleted_event_id_2_17_idx ON public.gbu_source2_a_17_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_18_idx 753*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_18_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_18_deleted') THEN
			ALTER TABLE gbu_source2_a_18_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_18_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_18_idx;
		CREATE INDEX deleted_event_id_2_18_idx ON public.gbu_source2_a_18_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_19_idx 754*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_19_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_19_deleted') THEN
			ALTER TABLE gbu_source2_a_19_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_19_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_19_idx;
		CREATE INDEX deleted_event_id_2_19_idx ON public.gbu_source2_a_19_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_20_idx 755*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_20_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_20_deleted') THEN
			ALTER TABLE gbu_source2_a_20_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_20_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_20_idx;
		CREATE INDEX deleted_event_id_2_20_idx ON public.gbu_source2_a_20_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_54578526_a_num_pk 756*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_49_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_49_num') THEN
			ALTER TABLE gbu_custom_source_49_num DROP CONSTRAINT IF EXISTS reg_54578526_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578526_a_num_pk;
		CREATE UNIQUE INDEX reg_54578526_a_num_pk ON public.gbu_custom_source_49_num USING btree (id);
	END IF;
END $$;

/*reg_54578526_a_num_inx_obj_attr_id 757*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_49_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_49_num') THEN
			ALTER TABLE gbu_custom_source_49_num DROP CONSTRAINT IF EXISTS reg_54578526_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578526_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578526_a_num_inx_obj_attr_id ON public.gbu_custom_source_49_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_2_22_idx 758*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_22_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_22_deleted') THEN
			ALTER TABLE gbu_source2_a_22_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_22_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_22_idx;
		CREATE INDEX deleted_event_id_2_22_idx ON public.gbu_source2_a_22_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_23_idx 759*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_23_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_23_deleted') THEN
			ALTER TABLE gbu_source2_a_23_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_23_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_23_idx;
		CREATE INDEX deleted_event_id_2_23_idx ON public.gbu_source2_a_23_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_24_idx 760*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_24_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_24_deleted') THEN
			ALTER TABLE gbu_source2_a_24_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_24_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_24_idx;
		CREATE INDEX deleted_event_id_2_24_idx ON public.gbu_source2_a_24_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_25_idx 761*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_25_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_25_deleted') THEN
			ALTER TABLE gbu_source2_a_25_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_25_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_25_idx;
		CREATE INDEX deleted_event_id_2_25_idx ON public.gbu_source2_a_25_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_26_idx 762*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_26_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_26_deleted') THEN
			ALTER TABLE gbu_source2_a_26_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_26_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_26_idx;
		CREATE INDEX deleted_event_id_2_26_idx ON public.gbu_source2_a_26_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_27_idx 763*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_27_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_27_deleted') THEN
			ALTER TABLE gbu_source2_a_27_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_27_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_27_idx;
		CREATE INDEX deleted_event_id_2_27_idx ON public.gbu_source2_a_27_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_43_idx 764*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_43_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_43_deleted') THEN
			ALTER TABLE gbu_source2_a_43_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_43_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_43_idx;
		CREATE INDEX deleted_event_id_2_43_idx ON public.gbu_source2_a_43_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_44_idx 765*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_44_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_44_deleted') THEN
			ALTER TABLE gbu_source2_a_44_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_44_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_44_idx;
		CREATE INDEX deleted_event_id_2_44_idx ON public.gbu_source2_a_44_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_54578526_a_dt_pk 766*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_49_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_49_dt') THEN
			ALTER TABLE gbu_custom_source_49_dt DROP CONSTRAINT IF EXISTS reg_54578526_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578526_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578526_a_dt_pk ON public.gbu_custom_source_49_dt USING btree (id);
	END IF;
END $$;

/*reg_54578526_a_dt_inx_obj_attr_id 767*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_49_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_49_dt') THEN
			ALTER TABLE gbu_custom_source_49_dt DROP CONSTRAINT IF EXISTS reg_54578526_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578526_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578526_a_dt_inx_obj_attr_id ON public.gbu_custom_source_49_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_2_45_idx 768*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_45_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_45_deleted') THEN
			ALTER TABLE gbu_source2_a_45_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_45_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_45_idx;
		CREATE INDEX deleted_event_id_2_45_idx ON public.gbu_source2_a_45_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_46_idx 769*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_46_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_46_deleted') THEN
			ALTER TABLE gbu_source2_a_46_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_46_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_46_idx;
		CREATE INDEX deleted_event_id_2_46_idx ON public.gbu_source2_a_46_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_600_idx 770*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_600_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_600_deleted') THEN
			ALTER TABLE gbu_source2_a_600_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_600_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_600_idx;
		CREATE INDEX deleted_event_id_2_600_idx ON public.gbu_source2_a_600_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_601_idx 771*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_601_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_601_deleted') THEN
			ALTER TABLE gbu_source2_a_601_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_601_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_601_idx;
		CREATE INDEX deleted_event_id_2_601_idx ON public.gbu_source2_a_601_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_602_idx 772*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_602_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_602_deleted') THEN
			ALTER TABLE gbu_source2_a_602_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_602_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_602_idx;
		CREATE INDEX deleted_event_id_2_602_idx ON public.gbu_source2_a_602_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_603_idx 773*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_603_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_603_deleted') THEN
			ALTER TABLE gbu_source2_a_603_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_603_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_603_idx;
		CREATE INDEX deleted_event_id_2_603_idx ON public.gbu_source2_a_603_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_604_idx 774*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_604_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_604_deleted') THEN
			ALTER TABLE gbu_source2_a_604_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_604_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_604_idx;
		CREATE INDEX deleted_event_id_2_604_idx ON public.gbu_source2_a_604_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_605_idx 775*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_605_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_605_deleted') THEN
			ALTER TABLE gbu_source2_a_605_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_605_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_605_idx;
		CREATE INDEX deleted_event_id_2_605_idx ON public.gbu_source2_a_605_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_606_idx 776*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_606_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_606_deleted') THEN
			ALTER TABLE gbu_source2_a_606_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_606_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_606_idx;
		CREATE INDEX deleted_event_id_2_606_idx ON public.gbu_source2_a_606_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_660_idx 777*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_660_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_660_deleted') THEN
			ALTER TABLE gbu_source2_a_660_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_660_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_660_idx;
		CREATE INDEX deleted_event_id_2_660_idx ON public.gbu_source2_a_660_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_661_idx 778*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_661_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_661_deleted') THEN
			ALTER TABLE gbu_source2_a_661_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_661_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_661_idx;
		CREATE INDEX deleted_event_id_2_661_idx ON public.gbu_source2_a_661_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_662_idx 779*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_662_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_662_deleted') THEN
			ALTER TABLE gbu_source2_a_662_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_662_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_662_idx;
		CREATE INDEX deleted_event_id_2_662_idx ON public.gbu_source2_a_662_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_663_idx 780*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_663_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_663_deleted') THEN
			ALTER TABLE gbu_source2_a_663_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_663_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_663_idx;
		CREATE INDEX deleted_event_id_2_663_idx ON public.gbu_source2_a_663_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_49413074_idx 781*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_49413074_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_49413074_deleted') THEN
			ALTER TABLE gbu_source2_a_49413074_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_49413074_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_49413074_idx;
		CREATE INDEX deleted_event_id_2_49413074_idx ON public.gbu_source2_a_49413074_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_54578534_a_txt_pk 782*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_50_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_50_txt') THEN
			ALTER TABLE gbu_custom_source_50_txt DROP CONSTRAINT IF EXISTS reg_54578534_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578534_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578534_a_txt_pk ON public.gbu_custom_source_50_txt USING btree (id);
	END IF;
END $$;

/*reg_54578534_a_txt_inx_obj_attr_id 783*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_50_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_50_txt') THEN
			ALTER TABLE gbu_custom_source_50_txt DROP CONSTRAINT IF EXISTS reg_54578534_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578534_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578534_a_txt_inx_obj_attr_id ON public.gbu_custom_source_50_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_51666775_a_txt_pk 784*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_38_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_38_txt') THEN
			ALTER TABLE gbu_custom_source_38_txt DROP CONSTRAINT IF EXISTS reg_51666775_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666775_a_txt_pk;
		CREATE UNIQUE INDEX reg_51666775_a_txt_pk ON public.gbu_custom_source_38_txt USING btree (id);
	END IF;
END $$;

/*reg_51666775_a_txt_inx_obj_attr_id 785*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_38_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_38_txt') THEN
			ALTER TABLE gbu_custom_source_38_txt DROP CONSTRAINT IF EXISTS reg_51666775_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666775_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_51666775_a_txt_inx_obj_attr_id ON public.gbu_custom_source_38_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_315_q_pk 786*/
DO $$
BEGIN
	IF (SELECT to_regclass('sud_object') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='sud_object') THEN
			ALTER TABLE sud_object DROP CONSTRAINT IF EXISTS reg_315_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_315_q_pk;
		alter table SUD_OBJECT add constraint reg_315_q_pk primary key (id);
	END IF;
END $$;

/*reg_22_a_txt_pk 787*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source22_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source22_a_txt') THEN
			ALTER TABLE gbu_source22_a_txt DROP CONSTRAINT IF EXISTS reg_22_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_22_a_txt_pk;
		CREATE UNIQUE INDEX reg_22_a_txt_pk ON public.gbu_source22_a_txt USING btree (id);
	END IF;
END $$;

/*reg_22_a_txt_inx_obj_attr_id 788*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source22_a_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source22_a_txt') THEN
			ALTER TABLE gbu_source22_a_txt DROP CONSTRAINT IF EXISTS reg_22_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_22_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_22_a_txt_inx_obj_attr_id ON public.gbu_source22_a_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_51666775_a_num_pk 789*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_38_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_38_num') THEN
			ALTER TABLE gbu_custom_source_38_num DROP CONSTRAINT IF EXISTS reg_51666775_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666775_a_num_pk;
		CREATE UNIQUE INDEX reg_51666775_a_num_pk ON public.gbu_custom_source_38_num USING btree (id);
	END IF;
END $$;

/*reg_51666775_a_num_inx_obj_attr_id 790*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_38_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_38_num') THEN
			ALTER TABLE gbu_custom_source_38_num DROP CONSTRAINT IF EXISTS reg_51666775_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666775_a_num_inx_obj_attr_id;
		CREATE INDEX reg_51666775_a_num_inx_obj_attr_id ON public.gbu_custom_source_38_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_51666775_a_dt_pk 791*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_38_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_38_dt') THEN
			ALTER TABLE gbu_custom_source_38_dt DROP CONSTRAINT IF EXISTS reg_51666775_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666775_a_dt_pk;
		CREATE UNIQUE INDEX reg_51666775_a_dt_pk ON public.gbu_custom_source_38_dt USING btree (id);
	END IF;
END $$;

/*reg_51666775_a_dt_inx_obj_attr_id 792*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_38_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_38_dt') THEN
			ALTER TABLE gbu_custom_source_38_dt DROP CONSTRAINT IF EXISTS reg_51666775_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666775_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_51666775_a_dt_inx_obj_attr_id ON public.gbu_custom_source_38_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578534_a_num_pk 793*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_50_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_50_num') THEN
			ALTER TABLE gbu_custom_source_50_num DROP CONSTRAINT IF EXISTS reg_54578534_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578534_a_num_pk;
		CREATE UNIQUE INDEX reg_54578534_a_num_pk ON public.gbu_custom_source_50_num USING btree (id);
	END IF;
END $$;

/*reg_54578534_a_num_inx_obj_attr_id 794*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_50_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_50_num') THEN
			ALTER TABLE gbu_custom_source_50_num DROP CONSTRAINT IF EXISTS reg_54578534_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578534_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578534_a_num_inx_obj_attr_id ON public.gbu_custom_source_50_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_51666777_a_txt_pk 795*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_39_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_39_txt') THEN
			ALTER TABLE gbu_custom_source_39_txt DROP CONSTRAINT IF EXISTS reg_51666777_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666777_a_txt_pk;
		CREATE UNIQUE INDEX reg_51666777_a_txt_pk ON public.gbu_custom_source_39_txt USING btree (id);
	END IF;
END $$;

/*reg_51666777_a_txt_inx_obj_attr_id 796*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_39_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_39_txt') THEN
			ALTER TABLE gbu_custom_source_39_txt DROP CONSTRAINT IF EXISTS reg_51666777_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666777_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_51666777_a_txt_inx_obj_attr_id ON public.gbu_custom_source_39_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578534_a_dt_pk 797*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_50_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_50_dt') THEN
			ALTER TABLE gbu_custom_source_50_dt DROP CONSTRAINT IF EXISTS reg_54578534_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578534_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578534_a_dt_pk ON public.gbu_custom_source_50_dt USING btree (id);
	END IF;
END $$;

/*reg_54578534_a_dt_inx_obj_attr_id 798*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_50_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_50_dt') THEN
			ALTER TABLE gbu_custom_source_50_dt DROP CONSTRAINT IF EXISTS reg_54578534_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578534_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578534_a_dt_inx_obj_attr_id ON public.gbu_custom_source_50_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_51666777_a_num_pk 799*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_39_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_39_num') THEN
			ALTER TABLE gbu_custom_source_39_num DROP CONSTRAINT IF EXISTS reg_51666777_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666777_a_num_pk;
		CREATE UNIQUE INDEX reg_51666777_a_num_pk ON public.gbu_custom_source_39_num USING btree (id);
	END IF;
END $$;

/*reg_51666777_a_num_inx_obj_attr_id 800*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_39_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_39_num') THEN
			ALTER TABLE gbu_custom_source_39_num DROP CONSTRAINT IF EXISTS reg_51666777_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666777_a_num_inx_obj_attr_id;
		CREATE INDEX reg_51666777_a_num_inx_obj_attr_id ON public.gbu_custom_source_39_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_51666777_a_dt_pk 801*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_39_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_39_dt') THEN
			ALTER TABLE gbu_custom_source_39_dt DROP CONSTRAINT IF EXISTS reg_51666777_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666777_a_dt_pk;
		CREATE UNIQUE INDEX reg_51666777_a_dt_pk ON public.gbu_custom_source_39_dt USING btree (id);
	END IF;
END $$;

/*reg_51666777_a_dt_inx_obj_attr_id 802*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_39_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_39_dt') THEN
			ALTER TABLE gbu_custom_source_39_dt DROP CONSTRAINT IF EXISTS reg_51666777_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51666777_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_51666777_a_dt_inx_obj_attr_id ON public.gbu_custom_source_39_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_51742779_q_pk 803*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors49413492') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors49413492') THEN
			ALTER TABLE ko_touroksfactors49413492 DROP CONSTRAINT IF EXISTS reg_51742779_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51742779_q_pk;
		CREATE UNIQUE INDEX reg_51742779_q_pk ON public.ko_touroksfactors49413492 USING btree (id);
	END IF;
END $$;

/*deleted_event_id_51742765_idx 804*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors49413492_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors49413492_deleted') THEN
			ALTER TABLE ko_tourzufactors49413492_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_51742765_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_51742765_idx;
		CREATE INDEX deleted_event_id_51742765_idx ON public.ko_tourzufactors49413492_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_51742769_idx 805*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors51487641_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors51487641_deleted') THEN
			ALTER TABLE ko_tourzufactors51487641_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_51742769_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_51742769_idx;
		CREATE INDEX deleted_event_id_51742769_idx ON public.ko_tourzufactors51487641_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_51742769_q_pk 806*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors51487641') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors51487641') THEN
			ALTER TABLE ko_tourzufactors51487641 DROP CONSTRAINT IF EXISTS reg_51742769_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51742769_q_pk;
		CREATE UNIQUE INDEX reg_51742769_q_pk ON public.ko_tourzufactors51487641 USING btree (id);
	END IF;
END $$;

/*reg_51742765_q_pk 807*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors49413492') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors49413492') THEN
			ALTER TABLE ko_tourzufactors49413492 DROP CONSTRAINT IF EXISTS reg_51742765_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_51742765_q_pk;
		CREATE UNIQUE INDEX reg_51742765_q_pk ON public.ko_tourzufactors49413492 USING btree (id);
	END IF;
END $$;

/*reg_814_q_pk 808*/
DO $$
BEGIN
	IF (SELECT to_regclass('common_gbu_operations_reports') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='common_gbu_operations_reports') THEN
			ALTER TABLE common_gbu_operations_reports DROP CONSTRAINT IF EXISTS reg_814_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_814_q_pk;
		alter table COMMON_GBU_OPERATIONS_REPORTS add constraint reg_814_q_pk primary key (id);
	END IF;
END $$;

/*deleted_event_id_2_53596331_idx 809*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596331_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596331_deleted') THEN
			ALTER TABLE gbu_source2_a_53596331_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_53596331_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_53596331_idx;
		CREATE INDEX deleted_event_id_2_53596331_idx ON public.gbu_source2_a_53596331_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_53596332_idx 810*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596332_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596332_deleted') THEN
			ALTER TABLE gbu_source2_a_53596332_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_53596332_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_53596332_idx;
		CREATE INDEX deleted_event_id_2_53596332_idx ON public.gbu_source2_a_53596332_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_51742779_idx 811*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors49413492_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors49413492_deleted') THEN
			ALTER TABLE ko_touroksfactors49413492_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_51742779_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_51742779_idx;
		CREATE INDEX deleted_event_id_51742779_idx ON public.ko_touroksfactors49413492_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_2_a_53596331__pk 812*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596331') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596331') THEN
			ALTER TABLE gbu_source2_a_53596331 DROP CONSTRAINT IF EXISTS reg_2_a_53596331__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596331__pk;
		CREATE UNIQUE INDEX reg_2_a_53596331__pk ON public.gbu_source2_a_53596331 USING btree (id);
	END IF;
END $$;

/*reg_2_a_53596331_inx_obj_attr_id 813*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596331') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596331') THEN
			ALTER TABLE gbu_source2_a_53596331 DROP CONSTRAINT IF EXISTS reg_2_a_53596331_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596331_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_53596331_inx_obj_attr_id ON public.gbu_source2_a_53596331 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_2_a_53596332__pk 814*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596332') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596332') THEN
			ALTER TABLE gbu_source2_a_53596332 DROP CONSTRAINT IF EXISTS reg_2_a_53596332__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596332__pk;
		CREATE UNIQUE INDEX reg_2_a_53596332__pk ON public.gbu_source2_a_53596332 USING btree (id);
	END IF;
END $$;

/*reg_2_a_53596332_inx_obj_attr_id 815*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596332') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596332') THEN
			ALTER TABLE gbu_source2_a_53596332 DROP CONSTRAINT IF EXISTS reg_2_a_53596332_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596332_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_53596332_inx_obj_attr_id ON public.gbu_source2_a_53596332 USING btree (object_id, ot);
	END IF;
END $$;

/*deleted_event_id_2_53596333_idx 816*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596333_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596333_deleted') THEN
			ALTER TABLE gbu_source2_a_53596333_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_53596333_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_53596333_idx;
		CREATE INDEX deleted_event_id_2_53596333_idx ON public.gbu_source2_a_53596333_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_53596334_idx 817*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596334_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596334_deleted') THEN
			ALTER TABLE gbu_source2_a_53596334_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_53596334_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_53596334_idx;
		CREATE INDEX deleted_event_id_2_53596334_idx ON public.gbu_source2_a_53596334_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_2_a_53596336__pk 818*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596336') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596336') THEN
			ALTER TABLE gbu_source2_a_53596336 DROP CONSTRAINT IF EXISTS reg_2_a_53596336__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596336__pk;
		CREATE UNIQUE INDEX reg_2_a_53596336__pk ON public.gbu_source2_a_53596336 USING btree (id);
	END IF;
END $$;

/*reg_2_a_53596336_inx_obj_attr_id 819*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596336') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596336') THEN
			ALTER TABLE gbu_source2_a_53596336 DROP CONSTRAINT IF EXISTS reg_2_a_53596336_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596336_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_53596336_inx_obj_attr_id ON public.gbu_source2_a_53596336 USING btree (object_id, ot);
	END IF;
END $$;

/*reg_54578536_a_txt_pk 820*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_51_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_51_txt') THEN
			ALTER TABLE gbu_custom_source_51_txt DROP CONSTRAINT IF EXISTS reg_54578536_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578536_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578536_a_txt_pk ON public.gbu_custom_source_51_txt USING btree (id);
	END IF;
END $$;

/*reg_54578536_a_txt_inx_obj_attr_id 821*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_51_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_51_txt') THEN
			ALTER TABLE gbu_custom_source_51_txt DROP CONSTRAINT IF EXISTS reg_54578536_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578536_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578536_a_txt_inx_obj_attr_id ON public.gbu_custom_source_51_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_2_a_53596334__pk 822*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596334') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596334') THEN
			ALTER TABLE gbu_source2_a_53596334 DROP CONSTRAINT IF EXISTS reg_2_a_53596334__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596334__pk;
		CREATE UNIQUE INDEX reg_2_a_53596334__pk ON public.gbu_source2_a_53596334 USING btree (id);
	END IF;
END $$;

/*reg_2_a_53596334_inx_obj_attr_id 823*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596334') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596334') THEN
			ALTER TABLE gbu_source2_a_53596334 DROP CONSTRAINT IF EXISTS reg_2_a_53596334_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596334_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_53596334_inx_obj_attr_id ON public.gbu_source2_a_53596334 USING btree (object_id, ot);
	END IF;
END $$;

/*deleted_event_id_2_53596336_idx 824*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596336_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596336_deleted') THEN
			ALTER TABLE gbu_source2_a_53596336_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_53596336_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_53596336_idx;
		CREATE INDEX deleted_event_id_2_53596336_idx ON public.gbu_source2_a_53596336_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_53596337_a_txt_pk 825*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_40_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_40_txt') THEN
			ALTER TABLE gbu_custom_source_40_txt DROP CONSTRAINT IF EXISTS reg_53596337_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53596337_a_txt_pk;
		CREATE UNIQUE INDEX reg_53596337_a_txt_pk ON public.gbu_custom_source_40_txt USING btree (id);
	END IF;
END $$;

/*reg_53596337_a_txt_inx_obj_attr_id 826*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_40_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_40_txt') THEN
			ALTER TABLE gbu_custom_source_40_txt DROP CONSTRAINT IF EXISTS reg_53596337_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53596337_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_53596337_a_txt_inx_obj_attr_id ON public.gbu_custom_source_40_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_53596337_a_num_pk 827*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_40_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_40_num') THEN
			ALTER TABLE gbu_custom_source_40_num DROP CONSTRAINT IF EXISTS reg_53596337_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53596337_a_num_pk;
		CREATE UNIQUE INDEX reg_53596337_a_num_pk ON public.gbu_custom_source_40_num USING btree (id);
	END IF;
END $$;

/*reg_53596337_a_num_inx_obj_attr_id 828*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_40_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_40_num') THEN
			ALTER TABLE gbu_custom_source_40_num DROP CONSTRAINT IF EXISTS reg_53596337_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53596337_a_num_inx_obj_attr_id;
		CREATE INDEX reg_53596337_a_num_inx_obj_attr_id ON public.gbu_custom_source_40_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578536_a_num_pk 829*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_51_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_51_num') THEN
			ALTER TABLE gbu_custom_source_51_num DROP CONSTRAINT IF EXISTS reg_54578536_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578536_a_num_pk;
		CREATE UNIQUE INDEX reg_54578536_a_num_pk ON public.gbu_custom_source_51_num USING btree (id);
	END IF;
END $$;

/*reg_54578536_a_num_inx_obj_attr_id 830*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_51_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_51_num') THEN
			ALTER TABLE gbu_custom_source_51_num DROP CONSTRAINT IF EXISTS reg_54578536_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578536_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578536_a_num_inx_obj_attr_id ON public.gbu_custom_source_51_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_53642500_a_txt_pk 831*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_41_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_41_txt') THEN
			ALTER TABLE gbu_custom_source_41_txt DROP CONSTRAINT IF EXISTS reg_53642500_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53642500_a_txt_pk;
		CREATE UNIQUE INDEX reg_53642500_a_txt_pk ON public.gbu_custom_source_41_txt USING btree (id);
	END IF;
END $$;

/*reg_53642500_a_txt_inx_obj_attr_id 832*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_41_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_41_txt') THEN
			ALTER TABLE gbu_custom_source_41_txt DROP CONSTRAINT IF EXISTS reg_53642500_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53642500_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_53642500_a_txt_inx_obj_attr_id ON public.gbu_custom_source_41_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_53596337_a_dt_pk 833*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_40_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_40_dt') THEN
			ALTER TABLE gbu_custom_source_40_dt DROP CONSTRAINT IF EXISTS reg_53596337_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53596337_a_dt_pk;
		CREATE UNIQUE INDEX reg_53596337_a_dt_pk ON public.gbu_custom_source_40_dt USING btree (id);
	END IF;
END $$;

/*reg_53596337_a_dt_inx_obj_attr_id 834*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_40_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_40_dt') THEN
			ALTER TABLE gbu_custom_source_40_dt DROP CONSTRAINT IF EXISTS reg_53596337_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53596337_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_53596337_a_dt_inx_obj_attr_id ON public.gbu_custom_source_40_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_262_q_pk 835*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unload_result_queue') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unload_result_queue') THEN
			ALTER TABLE ko_unload_result_queue DROP CONSTRAINT IF EXISTS reg_262_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_262_q_pk;
		alter table KO_UNLOAD_RESULT_QUEUE add constraint reg_262_q_pk primary key (id);
	END IF;
END $$;

/*reg_54578536_a_dt_pk 836*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_51_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_51_dt') THEN
			ALTER TABLE gbu_custom_source_51_dt DROP CONSTRAINT IF EXISTS reg_54578536_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578536_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578536_a_dt_pk ON public.gbu_custom_source_51_dt USING btree (id);
	END IF;
END $$;

/*reg_54578536_a_dt_inx_obj_attr_id 837*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_51_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_51_dt') THEN
			ALTER TABLE gbu_custom_source_51_dt DROP CONSTRAINT IF EXISTS reg_54578536_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578536_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578536_a_dt_inx_obj_attr_id ON public.gbu_custom_source_51_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_53642500_a_num_pk 838*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_41_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_41_num') THEN
			ALTER TABLE gbu_custom_source_41_num DROP CONSTRAINT IF EXISTS reg_53642500_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53642500_a_num_pk;
		CREATE UNIQUE INDEX reg_53642500_a_num_pk ON public.gbu_custom_source_41_num USING btree (id);
	END IF;
END $$;

/*reg_53642500_a_num_inx_obj_attr_id 839*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_41_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_41_num') THEN
			ALTER TABLE gbu_custom_source_41_num DROP CONSTRAINT IF EXISTS reg_53642500_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53642500_a_num_inx_obj_attr_id;
		CREATE INDEX reg_53642500_a_num_inx_obj_attr_id ON public.gbu_custom_source_41_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*deleted_event_id_963_idx 840*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_instance_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_instance_deleted') THEN
			ALTER TABLE core_td_instance_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_963_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_963_idx;
		CREATE INDEX deleted_event_id_963_idx ON public.core_td_instance_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_267_q_pk 841*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_unload_result_files') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_unload_result_files') THEN
			ALTER TABLE ko_unload_result_files DROP CONSTRAINT IF EXISTS reg_267_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_267_q_pk;
		CREATE UNIQUE INDEX reg_267_q_pk ON public.ko_unload_result_files USING btree (id);
	END IF;
END $$;

/*reg_53642500_a_dt_pk 842*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_41_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_41_dt') THEN
			ALTER TABLE gbu_custom_source_41_dt DROP CONSTRAINT IF EXISTS reg_53642500_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53642500_a_dt_pk;
		CREATE UNIQUE INDEX reg_53642500_a_dt_pk ON public.gbu_custom_source_41_dt USING btree (id);
	END IF;
END $$;

/*reg_53642500_a_dt_inx_obj_attr_id 843*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_41_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_41_dt') THEN
			ALTER TABLE gbu_custom_source_41_dt DROP CONSTRAINT IF EXISTS reg_53642500_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_53642500_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_53642500_a_dt_inx_obj_attr_id ON public.gbu_custom_source_41_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578540_a_txt_pk 844*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_52_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_52_txt') THEN
			ALTER TABLE gbu_custom_source_52_txt DROP CONSTRAINT IF EXISTS reg_54578540_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578540_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578540_a_txt_pk ON public.gbu_custom_source_52_txt USING btree (id);
	END IF;
END $$;

/*reg_54578540_a_txt_inx_obj_attr_id 845*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_52_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_52_txt') THEN
			ALTER TABLE gbu_custom_source_52_txt DROP CONSTRAINT IF EXISTS reg_54578540_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578540_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578540_a_txt_inx_obj_attr_id ON public.gbu_custom_source_52_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578466_a_txt_pk 846*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_42_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_42_txt') THEN
			ALTER TABLE gbu_custom_source_42_txt DROP CONSTRAINT IF EXISTS reg_54578466_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578466_a_txt_pk;
		CREATE UNIQUE INDEX reg_54578466_a_txt_pk ON public.gbu_custom_source_42_txt USING btree (id);
	END IF;
END $$;

/*reg_54578466_a_txt_inx_obj_attr_id 847*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_42_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_42_txt') THEN
			ALTER TABLE gbu_custom_source_42_txt DROP CONSTRAINT IF EXISTS reg_54578466_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578466_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_54578466_a_txt_inx_obj_attr_id ON public.gbu_custom_source_42_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578466_a_num_pk 848*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_42_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_42_num') THEN
			ALTER TABLE gbu_custom_source_42_num DROP CONSTRAINT IF EXISTS reg_54578466_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578466_a_num_pk;
		CREATE UNIQUE INDEX reg_54578466_a_num_pk ON public.gbu_custom_source_42_num USING btree (id);
	END IF;
END $$;

/*reg_54578466_a_num_inx_obj_attr_id 849*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_42_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_42_num') THEN
			ALTER TABLE gbu_custom_source_42_num DROP CONSTRAINT IF EXISTS reg_54578466_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578466_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578466_a_num_inx_obj_attr_id ON public.gbu_custom_source_42_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578540_a_num_pk 850*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_52_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_52_num') THEN
			ALTER TABLE gbu_custom_source_52_num DROP CONSTRAINT IF EXISTS reg_54578540_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578540_a_num_pk;
		CREATE UNIQUE INDEX reg_54578540_a_num_pk ON public.gbu_custom_source_52_num USING btree (id);
	END IF;
END $$;

/*reg_54578540_a_num_inx_obj_attr_id 851*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_52_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_52_num') THEN
			ALTER TABLE gbu_custom_source_52_num DROP CONSTRAINT IF EXISTS reg_54578540_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578540_a_num_inx_obj_attr_id;
		CREATE INDEX reg_54578540_a_num_inx_obj_attr_id ON public.gbu_custom_source_52_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578466_a_dt_pk 852*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_42_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_42_dt') THEN
			ALTER TABLE gbu_custom_source_42_dt DROP CONSTRAINT IF EXISTS reg_54578466_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578466_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578466_a_dt_pk ON public.gbu_custom_source_42_dt USING btree (id);
	END IF;
END $$;

/*reg_54578466_a_dt_inx_obj_attr_id 853*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_42_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_42_dt') THEN
			ALTER TABLE gbu_custom_source_42_dt DROP CONSTRAINT IF EXISTS reg_54578466_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578466_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578466_a_dt_inx_obj_attr_id ON public.gbu_custom_source_42_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_54578540_a_dt_pk 854*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_52_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_52_dt') THEN
			ALTER TABLE gbu_custom_source_52_dt DROP CONSTRAINT IF EXISTS reg_54578540_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578540_a_dt_pk;
		CREATE UNIQUE INDEX reg_54578540_a_dt_pk ON public.gbu_custom_source_52_dt USING btree (id);
	END IF;
END $$;

/*reg_54578540_a_dt_inx_obj_attr_id 855*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_52_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_52_dt') THEN
			ALTER TABLE gbu_custom_source_52_dt DROP CONSTRAINT IF EXISTS reg_54578540_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_54578540_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_54578540_a_dt_inx_obj_attr_id ON public.gbu_custom_source_52_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_47498663_q_pk 856*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_touroksfactors18099454') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_touroksfactors18099454') THEN
			ALTER TABLE ko_touroksfactors18099454 DROP CONSTRAINT IF EXISTS reg_47498663_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47498663_q_pk;
		CREATE UNIQUE INDEX reg_47498663_q_pk ON public.ko_touroksfactors18099454 USING btree (id);
	END IF;
END $$;

/*reg_39959492_q_pk 857*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_tourzufactors18099454') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_tourzufactors18099454') THEN
			ALTER TABLE ko_tourzufactors18099454 DROP CONSTRAINT IF EXISTS reg_39959492_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_39959492_q_pk;
		CREATE UNIQUE INDEX reg_39959492_q_pk ON public.ko_tourzufactors18099454 USING btree (id);
	END IF;
END $$;

/*ko_file_storage_pk 858*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_file_storage') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_file_storage') THEN
			ALTER TABLE ko_file_storage DROP CONSTRAINT IF EXISTS ko_file_storage_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_file_storage_pk;
		CREATE UNIQUE INDEX ko_file_storage_pk ON public.ko_file_storage USING btree (id);
	END IF;
END $$;

/*ko_calc_group_pkey 859*/
DO $$
BEGIN
	IF (SELECT to_regclass('ko_calc_group') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='ko_calc_group') THEN
			ALTER TABLE ko_calc_group DROP CONSTRAINT IF EXISTS ko_calc_group_pkey RESTRICT;
		END IF;
		DROP INDEX IF EXISTS ko_calc_group_pkey;
		alter table KO_CALC_GROUP add constraint ko_calc_group_pkey primary key (id);
	END IF;
END $$;

/*reg_47020715_a_num_pk 860*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_33_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_33_num') THEN
			ALTER TABLE gbu_custom_source_33_num DROP CONSTRAINT IF EXISTS reg_47020715_a_num_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020715_a_num_pk;
		CREATE UNIQUE INDEX reg_47020715_a_num_pk ON public.gbu_custom_source_33_num USING btree (id);
	END IF;
END $$;

/*reg_47020715_a_num_inx_obj_attr_id 861*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_33_num') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_33_num') THEN
			ALTER TABLE gbu_custom_source_33_num DROP CONSTRAINT IF EXISTS reg_47020715_a_num_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_47020715_a_num_inx_obj_attr_id;
		CREATE INDEX reg_47020715_a_num_inx_obj_attr_id ON public.gbu_custom_source_33_num USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_503_q_pk 862*/
DO $$
BEGIN
	IF (SELECT to_regclass('declarations_har_parcel') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='declarations_har_parcel') THEN
			ALTER TABLE declarations_har_parcel DROP CONSTRAINT IF EXISTS reg_503_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_503_q_pk;
		alter table DECLARATIONS_HAR_PARCEL add constraint reg_503_q_pk primary key (id);
	END IF;
END $$;

/*reg_49412726_a_txt_pk 863*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_36_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_36_txt') THEN
			ALTER TABLE gbu_custom_source_36_txt DROP CONSTRAINT IF EXISTS reg_49412726_a_txt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49412726_a_txt_pk;
		CREATE UNIQUE INDEX reg_49412726_a_txt_pk ON public.gbu_custom_source_36_txt USING btree (id);
	END IF;
END $$;

/*reg_49412726_a_txt_inx_obj_attr_id 864*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_36_txt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_36_txt') THEN
			ALTER TABLE gbu_custom_source_36_txt DROP CONSTRAINT IF EXISTS reg_49412726_a_txt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_49412726_a_txt_inx_obj_attr_id;
		CREATE INDEX reg_49412726_a_txt_inx_obj_attr_id ON public.gbu_custom_source_36_txt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_926_quant_pk 865*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_usersettingsreport') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_usersettingsreport') THEN
			ALTER TABLE core_srd_usersettingsreport DROP CONSTRAINT IF EXISTS reg_926_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_926_quant_pk;
		alter table CORE_SRD_USERSETTINGSREPORT add constraint reg_926_quant_pk primary key (id);
	END IF;
END $$;

/*reg_8_a_dt_pk 866*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source8_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source8_a_dt') THEN
			ALTER TABLE gbu_source8_a_dt DROP CONSTRAINT IF EXISTS reg_8_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_8_a_dt_pk;
		CREATE UNIQUE INDEX reg_8_a_dt_pk ON public.gbu_source8_a_dt USING btree (id);
	END IF;
END $$;

/*reg_8_a_dt_inx_obj_attr_id 867*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source8_a_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source8_a_dt') THEN
			ALTER TABLE gbu_source8_a_dt DROP CONSTRAINT IF EXISTS reg_8_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_8_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_8_a_dt_inx_obj_attr_id ON public.gbu_source8_a_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

-- /*es_settings_params_pkey 868*/
-- DO $$
-- BEGIN
-- 	IF (SELECT to_regclass('es_settings_params') IS NOT null) THEN
-- 		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='es_settings_params') THEN
-- 			ALTER TABLE es_settings_params DROP CONSTRAINT IF EXISTS es_settings_params_pkey RESTRICT;
-- 		END IF;
-- 		DROP INDEX IF EXISTS es_settings_params_pkey;
-- 		alter table ES_SETTINGS_PARAMS add constraint es_settings_params_pkey primary key (id);
-- 	END IF;
-- END $$;

/*reg_957_quant_pk 869*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_srd_register_category') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_srd_register_category') THEN
			ALTER TABLE core_srd_register_category DROP CONSTRAINT IF EXISTS reg_957_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_957_quant_pk;
		alter table CORE_SRD_REGISTER_CATEGORY add constraint reg_957_quant_pk primary key (id);
	END IF;
END $$;

/*reg_962_quant_pk 870*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_status') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_status') THEN
			ALTER TABLE core_td_status DROP CONSTRAINT IF EXISTS reg_962_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_962_quant_pk;
		alter table CORE_TD_STATUS add constraint reg_962_quant_pk primary key (id);
	END IF;
END $$;

/*reg_960_quant_pk 871*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_template') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_template') THEN
			ALTER TABLE core_td_template DROP CONSTRAINT IF EXISTS reg_960_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_960_quant_pk;
		alter table CORE_TD_TEMPLATE add constraint reg_960_quant_pk primary key (id);
	END IF;
END $$;

/*reg_971_quant_pk 872*/
DO $$
BEGIN
	IF (SELECT to_regclass('core_td_template_type') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='core_td_template_type') THEN
			ALTER TABLE core_td_template_type DROP CONSTRAINT IF EXISTS reg_971_quant_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_971_quant_pk;
		alter table CORE_TD_TEMPLATE_TYPE add constraint reg_971_quant_pk primary key (id);
	END IF;
END $$;

/*reg_41935285_q_pk 873*/
DO $$
BEGIN
	IF (SELECT to_regclass('source_35_q') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='source_35_q') THEN
			ALTER TABLE source_35_q DROP CONSTRAINT IF EXISTS reg_41935285_q_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_41935285_q_pk;
		CREATE UNIQUE INDEX reg_41935285_q_pk ON public.source_35_q USING btree (id);
	END IF;
END $$;

/*reg_42436643_a_dt_pk 874*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_25_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_25_dt') THEN
			ALTER TABLE gbu_custom_source_25_dt DROP CONSTRAINT IF EXISTS reg_42436643_a_dt_pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42436643_a_dt_pk;
		CREATE UNIQUE INDEX reg_42436643_a_dt_pk ON public.gbu_custom_source_25_dt USING btree (id);
	END IF;
END $$;

/*reg_42436643_a_dt_inx_obj_attr_id 875*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_custom_source_25_dt') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_custom_source_25_dt') THEN
			ALTER TABLE gbu_custom_source_25_dt DROP CONSTRAINT IF EXISTS reg_42436643_a_dt_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_42436643_a_dt_inx_obj_attr_id;
		CREATE INDEX reg_42436643_a_dt_inx_obj_attr_id ON public.gbu_custom_source_25_dt USING btree (object_id, attribute_id);
	END IF;
END $$;

/*reg_2_a_13__pk 876*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_13') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_13') THEN
			ALTER TABLE gbu_source2_a_13 DROP CONSTRAINT IF EXISTS reg_2_a_13__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_13__pk;
		CREATE UNIQUE INDEX reg_2_a_13__pk ON public.gbu_source2_a_13 USING btree (id);
	END IF;
END $$;

/*reg_2_a_13_inx_obj_attr_id 877*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_13') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_13') THEN
			ALTER TABLE gbu_source2_a_13 DROP CONSTRAINT IF EXISTS reg_2_a_13_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_13_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_13_inx_obj_attr_id ON public.gbu_source2_a_13 USING btree (object_id, ot);
	END IF;
END $$;

/*deleted_event_id_2_3_idx 878*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_3_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_3_deleted') THEN
			ALTER TABLE gbu_source2_a_3_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_3_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_3_idx;
		CREATE INDEX deleted_event_id_2_3_idx ON public.gbu_source2_a_3_deleted USING btree (event_id);
	END IF;
END $$;

/*deleted_event_id_2_21_idx 879*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_21_deleted') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_21_deleted') THEN
			ALTER TABLE gbu_source2_a_21_deleted DROP CONSTRAINT IF EXISTS deleted_event_id_2_21_idx RESTRICT;
		END IF;
		DROP INDEX IF EXISTS deleted_event_id_2_21_idx;
		CREATE INDEX deleted_event_id_2_21_idx ON public.gbu_source2_a_21_deleted USING btree (event_id);
	END IF;
END $$;

/*reg_2_a_53596333__pk 880*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596333') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596333') THEN
			ALTER TABLE gbu_source2_a_53596333 DROP CONSTRAINT IF EXISTS reg_2_a_53596333__pk RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596333__pk;
		CREATE UNIQUE INDEX reg_2_a_53596333__pk ON public.gbu_source2_a_53596333 USING btree (id);
	END IF;
END $$;

/*reg_2_a_53596333_inx_obj_attr_id 881*/
DO $$
BEGIN
	IF (SELECT to_regclass('gbu_source2_a_53596333') IS NOT null) THEN
		IF(SELECT count(*) FROM pg_tables WHERE schemaname='public' AND tablename='gbu_source2_a_53596333') THEN
			ALTER TABLE gbu_source2_a_53596333 DROP CONSTRAINT IF EXISTS reg_2_a_53596333_inx_obj_attr_id RESTRICT;
		END IF;
		DROP INDEX IF EXISTS reg_2_a_53596333_inx_obj_attr_id;
		CREATE UNIQUE INDEX reg_2_a_53596333_inx_obj_attr_id ON public.gbu_source2_a_53596333 USING btree (object_id, ot);
	END IF;
END $$;
