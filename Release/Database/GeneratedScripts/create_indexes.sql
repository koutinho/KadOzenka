
-- XIV. Скрипт создания индексов

--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_insur_rate_pkey'))then
		CREATE UNIQUE INDEX insur_insur_rate_pkey ON public.insur_insur_rate USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_995_quant_pk'))then
		CREATE UNIQUE INDEX reg_995_quant_pk ON public.core_regnom_sequences USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_integrated_indicators_replement_cost_pkey'))then
		CREATE UNIQUE INDEX insur_integrated_indicators_replement_cost_pkey ON public.insur_integrated_indicators_repl_cost USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_983_quant_pk'))then
		CREATE UNIQUE INDEX reg_983_quant_pk ON public.core_reference_item USING btree (itemid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_984_quant_pk'))then
		CREATE UNIQUE INDEX reg_984_quant_pk ON public.core_reference_relation USING btree (relid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_985_quant_pk'))then
		CREATE UNIQUE INDEX reg_985_quant_pk ON public.core_reference_tree USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_living_premise_insur_cost_pkey'))then
		CREATE UNIQUE INDEX insur_living_premise_insur_cost_pkey ON public.insur_living_premise_insur_cost USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_970_quant_pk'))then
		CREATE UNIQUE INDEX reg_970_quant_pk ON public.core_td_tp USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_962_quant_pk'))then
		CREATE UNIQUE INDEX reg_962_quant_pk ON public.core_td_status USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_966_quant_pk'))then
		CREATE UNIQUE INDEX reg_966_quant_pk ON public.core_td_audit_action USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_965_quant_pk'))then
		CREATE UNIQUE INDEX reg_965_quant_pk ON public.core_td_change USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_968_quant_pk'))then
		CREATE UNIQUE INDEX reg_968_quant_pk ON public.core_td_tree USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_971_quant_pk'))then
		CREATE UNIQUE INDEX reg_971_quant_pk ON public.core_td_template_type USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_946_quant_pk'))then
		CREATE UNIQUE INDEX reg_946_quant_pk ON public.core_srd_role_function USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_948_quant_pk'))then
		CREATE UNIQUE INDEX reg_948_quant_pk ON public.core_srd_role_attr USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_951_quant_pk'))then
		CREATE UNIQUE INDEX reg_951_quant_pk ON public.core_srd_usersettings USING btree (userid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_301_pk'))then
		CREATE UNIQUE INDEX reg_301_pk ON public.insur_input_file USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_952_quant_pk'))then
		CREATE UNIQUE INDEX reg_952_quant_pk ON public.core_srd_user_role USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_955_quant_pk'))then
		CREATE UNIQUE INDEX reg_955_quant_pk ON public.core_srd_role_filter USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_960_quant_pk'))then
		CREATE UNIQUE INDEX reg_960_quant_pk ON public.core_td_template USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_958_quant_pk'))then
		CREATE UNIQUE INDEX reg_958_quant_pk ON public.core_srd_function_reg_cat USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_957_quant_pk'))then
		CREATE UNIQUE INDEX reg_957_quant_pk ON public.core_srd_register_category USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_302_pk'))then
		CREATE UNIQUE INDEX reg_302_pk ON public.insur_log_file USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_942_quant_pk'))then
		CREATE UNIQUE INDEX reg_942_quant_pk ON public.core_srd_function USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_850_quant_pk'))then
		CREATE UNIQUE INDEX reg_850_quant_pk ON public.dashboards_dashboard USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_651_quant_pk'))then
		CREATE UNIQUE INDEX reg_651_quant_pk ON public.spd_usersrd2spd USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_925_quant_pk'))then
		CREATE UNIQUE INDEX reg_925_quant_pk ON public.core_srd_usersettingsregview USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_853_quant_pk'))then
		CREATE UNIQUE INDEX reg_853_quant_pk ON public.dashboards_user_settings USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_851_quant_pk'))then
		CREATE UNIQUE INDEX reg_851_quant_pk ON public.dashboards_panel USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_852_quant_pk'))then
		CREATE UNIQUE INDEX reg_852_quant_pk ON public.dashboards_panel_type USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_921_quant_pk'))then
		CREATE UNIQUE INDEX reg_921_quant_pk ON public.core_list_object USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_924_quant_pk'))then
		CREATE UNIQUE INDEX reg_924_quant_pk ON public.core_layout_column_type USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_931_quant_pk'))then
		CREATE UNIQUE INDEX reg_931_quant_pk ON public.core_register_attribute USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_932_quant_pk'))then
		CREATE UNIQUE INDEX reg_932_quant_pk ON public.core_register_relation USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_935_quant_pk'))then
		CREATE UNIQUE INDEX reg_935_quant_pk ON public.core_layout_details USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_938_quant_pk'))then
		CREATE UNIQUE INDEX reg_938_quant_pk ON public.core_qry_operation USING btree (qryoperationid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_941_quant_pk'))then
		CREATE UNIQUE INDEX reg_941_quant_pk ON public.core_srd_department USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_930_quant_pk'))then
		CREATE UNIQUE INDEX reg_930_quant_pk ON public.core_register USING btree (registerid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_937_quant_pk'))then
		CREATE UNIQUE INDEX reg_937_quant_pk ON public.core_qry_filter USING btree (qryfilterid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_947_quant_pk'))then
		CREATE UNIQUE INDEX reg_947_quant_pk ON public.core_srd_role_register USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_310_pk'))then
		CREATE UNIQUE INDEX reg_310_pk ON public.insur_all_property USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('build_parcel_from_ehd_o_pkey'))then
		CREATE UNIQUE INDEX build_parcel_from_ehd_o_pkey ON public.ehd_build_parcel_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('registr_from_ehd_o_pkey'))then
		CREATE UNIQUE INDEX registr_from_ehd_o_pkey ON public.ehd_register_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_982_quant_pk'))then
		CREATE UNIQUE INDEX reg_982_quant_pk ON public.core_reference USING btree (referenceid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_309000400'))then
		CREATE INDEX ind_309000400 ON public.insur_policy_svd USING btree (link_id_file);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_309000300'))then
		CREATE INDEX ind_309000300 ON public.insur_policy_svd USING btree (fsp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_311000200'))then
		CREATE INDEX ind_311000200 ON public.insur_dop_all_property USING btree (contract_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_310000200'))then
		CREATE INDEX ind_310000200 ON public.insur_all_property USING btree (fsp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_308_obj_pk'))then
		CREATE UNIQUE INDEX reg_308_obj_pk ON public.insur_fsp_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_district_pkey'))then
		CREATE UNIQUE INDEX insur_district_pkey ON public.insur_district USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('flat_type_pkey'))then
		CREATE UNIQUE INDEX flat_type_pkey ON public.insur_flat_type USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_302000200'))then
		CREATE INDEX ind_302000200 ON public.insur_log_file USING btree (file_storage_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_306_pk'))then
		CREATE UNIQUE INDEX reg_306_pk ON public.insur_input_plat USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_309_pk'))then
		CREATE UNIQUE INDEX reg_309_pk ON public.insur_policy_svd USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_311_pk'))then
		CREATE UNIQUE INDEX reg_311_pk ON public.insur_dop_all_property USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_doc_base_type_pkey'))then
		CREATE UNIQUE INDEX insur_doc_base_type_pkey ON public.insur_doc_base_type USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_insurance_organization_pkey'))then
		CREATE UNIQUE INDEX insur_insurance_organization_pkey ON public.insur_insurance_organization USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insure_damage_assessment_method_pkey'))then
		CREATE UNIQUE INDEX insure_damage_assessment_method_pkey ON public.insur_damage_assessment_method USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_945_quant_pk'))then
		CREATE UNIQUE INDEX reg_945_quant_pk ON public.core_srd_role USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_input_file_package_pkey'))then
		CREATE UNIQUE INDEX insur_input_file_package_pkey ON public.insur_input_file_package USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_987_quant_pk'))then
		CREATE UNIQUE INDEX reg_987_quant_pk ON public.core_attachment_file USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_okrug_pkey'))then
		CREATE UNIQUE INDEX insur_okrug_pkey ON public.insur_okrug USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_314_pk'))then
		CREATE UNIQUE INDEX reg_314_pk ON public.insur_pay_to USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_326_pk'))then
		CREATE UNIQUE INDEX reg_326_pk ON public.insur_link_build_bti USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_327_pk'))then
		CREATE UNIQUE INDEX reg_327_pk ON public.insur_link_flat_egrn USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_315_pk'))then
		CREATE UNIQUE INDEX reg_315_pk ON public.insur_no_pay USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_315000200'))then
		CREATE INDEX ind_315000200 ON public.insur_no_pay USING btree (fsp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_253_object_pk'))then
		CREATE UNIQUE INDEX reg_253_object_pk ON public.bti_floor_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_257_quant_pk'))then
		CREATE UNIQUE INDEX reg_257_quant_pk ON public.bti_rooms USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('idx_alt_roomprem_fl_doc_inx'))then
		CREATE INDEX idx_alt_roomprem_fl_doc_inx ON public.bti_rooms USING btree (premise_id, floor_id, document_number);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('idx_bti_room_num_fl_ar_inx'))then
		CREATE INDEX idx_bti_room_num_fl_ar_inx ON public.bti_rooms USING btree (number_pp, floor_id, area);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('bti_room_floor_id_inx'))then
		CREATE INDEX bti_room_floor_id_inx ON public.bti_rooms USING btree (floor_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('bti_room_prem_fl_num_inx'))then
		CREATE INDEX bti_room_prem_fl_num_inx ON public.bti_rooms USING btree (premise_id, floor_id, number_pp);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_52_object_pk'))then
		CREATE UNIQUE INDEX reg_52_object_pk ON public.bti_addrlink_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('address_q_id_ds_actual'))then
		CREATE INDEX address_q_id_ds_actual ON public.bti_address_q USING btree (actual, id_in_ds, emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_52_quant_pk'))then
		CREATE UNIQUE INDEX reg_52_quant_pk ON public.bti_addrlink_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('addlink_q_buil_addr_s_po_emp'))then
		CREATE INDEX addlink_q_buil_addr_s_po_emp ON public.bti_addrlink_q USING btree (actual, address_id, building_id, emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('addrlink_q_addr_id_inx'))then
		CREATE INDEX addrlink_q_addr_id_inx ON public.bti_addrlink_q USING btree (address_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('addrlink_q_asi_addid_s_po'))then
		CREATE INDEX addrlink_q_asi_addid_s_po ON public.bti_addrlink_q USING btree (address_status_id, address_id, s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('addrlink_q_asi_bi_ai_s_po'))then
		CREATE INDEX addrlink_q_asi_bi_ai_s_po ON public.bti_addrlink_q USING btree (address_status_id, building_id, address_id, s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('addrlink_q_asi_empid_s_po'))then
		CREATE INDEX addrlink_q_asi_empid_s_po ON public.bti_addrlink_q USING btree (address_status_id, emp_id, s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('addrlink_q_asi_s_po'))then
		CREATE INDEX addrlink_q_asi_s_po ON public.bti_addrlink_q USING btree (address_status_id, s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('addrlink_q_build_id_inx'))then
		CREATE INDEX addrlink_q_build_id_inx ON public.bti_addrlink_q USING btree (building_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('idx_addrlink_q'))then
		CREATE INDEX idx_addrlink_q ON public.bti_addrlink_q USING btree (building_id, s_, po_, address_status_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('idx_addrlink_q_add_st_id'))then
		CREATE INDEX idx_addrlink_q_add_st_id ON public.bti_addrlink_q USING btree (address_status_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_52_actual_inx'))then
		CREATE INDEX reg_52_actual_inx ON public.bti_addrlink_q USING btree (actual);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_52_quant_inx_emp_id'))then
		CREATE INDEX reg_52_quant_inx_emp_id ON public.bti_addrlink_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_52_quant_inx_s_po_'))then
		CREATE INDEX reg_52_quant_inx_s_po_ ON public.bti_addrlink_q USING btree (s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_50_object_pk'))then
		CREATE UNIQUE INDEX reg_50_object_pk ON public.bti_address_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('address_q_streetid_spo'))then
		CREATE INDEX address_q_streetid_spo ON public.bti_address_q USING btree (street_id, s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('address_q_s_po'))then
		CREATE INDEX address_q_s_po ON public.bti_address_q USING btree (s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('address_q_s_po_id_in_ds'))then
		CREATE INDEX address_q_s_po_id_in_ds ON public.bti_address_q USING btree (id_in_ds, s_, po_, status, emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('address_q_up_id_in_ds'))then
		CREATE INDEX address_q_up_id_in_ds ON public.bti_address_q USING btree (upper((id_in_ds)::text));
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('idx_address_q'))then
		CREATE INDEX idx_address_q ON public.bti_address_q USING btree (s_, po_, full_name);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_314000200'))then
		CREATE INDEX ind_314000200 ON public.insur_pay_to USING btree (fsp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_50_quant_pk'))then
		CREATE UNIQUE INDEX reg_50_quant_pk ON public.bti_address_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_50_actual_inx'))then
		CREATE INDEX reg_50_actual_inx ON public.bti_address_q USING btree (actual);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_50_quant_inx_emp_id'))then
		CREATE INDEX reg_50_quant_inx_emp_id ON public.bti_address_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_330_pk'))then
		CREATE UNIQUE INDEX reg_330_pk ON public.insur_base_tariff USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_334_pk'))then
		CREATE UNIQUE INDEX reg_334_pk ON public.insur_agreement_project USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_scan_doc_pkey'))then
		CREATE UNIQUE INDEX insur_scan_doc_pkey ON public.insur_scan_doc USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_bank_pkey'))then
		CREATE UNIQUE INDEX insur_bank_pkey ON public.insur_bank USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_306000200'))then
		CREATE INDEX ind_306000200 ON public.insur_input_plat USING btree (link_id_file);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_306000400'))then
		CREATE INDEX ind_306000400 ON public.insur_input_plat USING btree (link_bank_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_306000300'))then
		CREATE INDEX ind_306000300 ON public.insur_input_plat USING btree (fsp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_damage_assessment_radio_tv_phone_pkey'))then
		CREATE UNIQUE INDEX insur_damage_assessment_radio_tv_phone_pkey ON public.insur_damage_assessment_radio_tv_phone USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ref_addr_okrug_pkey'))then
		CREATE UNIQUE INDEX ref_addr_okrug_pkey ON public.ref_addr_okrug USING btree (okrug_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('location_from_ehd_o_pkey'))then
		CREATE UNIQUE INDEX location_from_ehd_o_pkey ON public.ehd_location_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ref_addr_district_pkey'))then
		CREATE UNIQUE INDEX ref_addr_district_pkey ON public.ref_addr_district USING btree (district_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ref_addr_street_pkey'))then
		CREATE UNIQUE INDEX ref_addr_street_pkey ON public.ref_addr_street USING btree (street_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_305_pk'))then
		CREATE UNIQUE INDEX reg_305_pk ON public.insur_input_nach USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_305000300'))then
		CREATE INDEX ind_305000300 ON public.insur_input_nach USING btree (fsp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_305000200'))then
		CREATE INDEX ind_305000200 ON public.insur_input_nach USING btree (link_id_file);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_329_pk'))then
		CREATE UNIQUE INDEX reg_329_pk ON public.insur_part_compensation USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_305000400'))then
		CREATE INDEX ind_305000400 ON public.insur_input_nach USING btree (district_id, period_reg_date DESC);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_305000500'))then
		CREATE INDEX ind_305000500 ON public.insur_input_nach USING btree (criteria_json, district_id, period_reg_date DESC);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('fm_reports_savedreport_pkey'))then
		CREATE UNIQUE INDEX fm_reports_savedreport_pkey ON public.fm_reports_savedreport USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('fm_podpisant_pkey'))then
		CREATE UNIQUE INDEX fm_podpisant_pkey ON public.fm_podpisant USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_307_pk'))then
		CREATE UNIQUE INDEX reg_307_pk ON public.insur_balance USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_307000500'))then
		CREATE INDEX ind_307000500 ON public.insur_balance USING btree (link_input_nach);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_307000200'))then
		CREATE INDEX ind_307000200 ON public.insur_balance USING btree (fsp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_tariff_pkey'))then
		CREATE UNIQUE INDEX insur_tariff_pkey ON public.insur_tariff USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_building_o_pkey'))then
		CREATE UNIQUE INDEX insur_building_o_pkey ON public.insur_building_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_flat_o_pkey'))then
		CREATE UNIQUE INDEX insur_flat_o_pkey ON public.insur_flat_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_damage_amount_pkey'))then
		CREATE UNIQUE INDEX insur_damage_amount_pkey ON public.insur_damage_amount USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_actual_cost_ratio_pkey'))then
		CREATE UNIQUE INDEX insur_actual_cost_ratio_pkey ON public.insur_actual_cost_ratio USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_egrn_q_pkey'))then
		CREATE UNIQUE INDEX ehd_egrn_q_pkey ON public.ehd_egrp_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_egrn_o_pkey'))then
		CREATE UNIQUE INDEX ehd_egrn_o_pkey ON public.ehd_egrp_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_right_o_pkey'))then
		CREATE UNIQUE INDEX ehd_right_o_pkey ON public.ehd_right_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_right_q_pkey'))then
		CREATE UNIQUE INDEX ehd_right_q_pkey ON public.ehd_right_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('xpkfias_estatestatus'))then
		CREATE UNIQUE INDEX xpkfias_estatestatus ON public.fias_estatestatus USING btree (estatestatusid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('xpkfias_structurestatus'))then
		CREATE UNIQUE INDEX xpkfias_structurestatus ON public.fias_structurestatus USING btree (structurestatusid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_600_quant_pk'))then
		CREATE UNIQUE INDEX reg_600_quant_pk ON public.spd_request_registration USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_650_quant_pk'))then
		CREATE UNIQUE INDEX reg_650_quant_pk ON public.spd_doc_agreement USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_601_quant_pk'))then
		CREATE UNIQUE INDEX reg_601_quant_pk ON public.spd_create_full_app_log USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_251_obj_pk'))then
		CREATE UNIQUE INDEX reg_251_obj_pk ON public.bti_building_o USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_251_quant_pk'))then
		CREATE UNIQUE INDEX reg_251_quant_pk ON public.bti_building_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('idx_building_s_id'))then
		CREATE INDEX idx_building_s_id ON public.bti_building_q USING btree (source_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg251_quant_inx_empid_s_po'))then
		CREATE INDEX reg251_quant_inx_empid_s_po ON public.bti_building_q USING btree (emp_id, s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg251_q_inx_unomspoempid'))then
		CREATE INDEX reg251_q_inx_unomspoempid ON public.bti_building_q USING btree (unom, s_, po_, emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_251_quant_inx_emp_id'))then
		CREATE INDEX reg_251_quant_inx_emp_id ON public.bti_building_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_251_quant_inx_s_po_'))then
		CREATE INDEX reg_251_quant_inx_s_po_ ON public.bti_building_q USING btree (s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_252_actual_inx'))then
		CREATE INDEX reg_252_actual_inx ON public.bti_building_q USING btree (actual);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_253_quant_pk'))then
		CREATE UNIQUE INDEX reg_253_quant_pk ON public.bti_floor_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_253_actual_inx'))then
		CREATE INDEX reg_253_actual_inx ON public.bti_floor_q USING btree (actual);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_253_quant_inx_emp_id'))then
		CREATE INDEX reg_253_quant_inx_emp_id ON public.bti_floor_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('bti_floor_q_bid_et_tet'))then
		CREATE INDEX bti_floor_q_bid_et_tet ON public.bti_floor_q USING btree (actual, building_id, floor_number, type_id, emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('idx_bti_floor1'))then
		CREATE INDEX idx_bti_floor1 ON public.bti_floor_q USING btree (s_, po_, emp_id, building_id, type_id, floor_number);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('idx_bti_floor_q'))then
		CREATE INDEX idx_bti_floor_q ON public.bti_floor_q USING btree (building_id, number_pp, po_, s_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_253_quant_inx_s_po_'))then
		CREATE INDEX reg_253_quant_inx_s_po_ ON public.bti_floor_q USING btree (s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('uk_bti_floor_q'))then
		CREATE INDEX uk_bti_floor_q ON public.bti_floor_q USING btree (number_pp, s_, po_, building_id, emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_986_quant_pk'))then
		CREATE UNIQUE INDEX reg_986_quant_pk ON public.core_attachment USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_988_quant_pk'))then
		CREATE UNIQUE INDEX reg_988_quant_pk ON public.core_attachment_object USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_996_quant_pk'))then
		CREATE UNIQUE INDEX reg_996_quant_pk ON public.core_cache_updates USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_978_quant_pk'))then
		CREATE UNIQUE INDEX reg_978_quant_pk ON public.core_configparam USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_992_quant_pk'))then
		CREATE UNIQUE INDEX reg_992_quant_pk ON public.core_diagnostics USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_989_quant_pk'))then
		CREATE UNIQUE INDEX reg_989_quant_pk ON public.core_error_log USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('core_error_log_idx'))then
		CREATE INDEX core_error_log_idx ON public.core_error_log USING btree (errordate);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_998_quant_pk'))then
		CREATE UNIQUE INDEX reg_998_quant_pk ON public.core_holidays USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_933_quant_pk'))then
		CREATE UNIQUE INDEX reg_933_quant_pk ON public.core_layout USING btree (layoutid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_956_quant_pk'))then
		CREATE UNIQUE INDEX reg_956_quant_pk ON public.core_layout_export USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_920_quant_pk'))then
		CREATE UNIQUE INDEX reg_920_quant_pk ON public.core_list USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_977_quant_pk'))then
		CREATE UNIQUE INDEX reg_977_quant_pk ON public.core_long_process_log USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_976_quant_pk'))then
		CREATE UNIQUE INDEX reg_976_quant_pk ON public.core_long_process_type USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_936_quant_pk'))then
		CREATE UNIQUE INDEX reg_936_quant_pk ON public.core_qry USING btree (qryid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_939_quant_pk'))then
		CREATE UNIQUE INDEX reg_939_quant_pk ON public.core_register_lock USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_991_quant_pk'))then
		CREATE UNIQUE INDEX reg_991_quant_pk ON public.core_register_state USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_940_quant_pk'))then
		CREATE UNIQUE INDEX reg_940_quant_pk ON public.core_srd_audit USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_949_quant_pk'))then
		CREATE UNIQUE INDEX reg_949_quant_pk ON public.core_srd_session USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_969_quant_pk'))then
		CREATE UNIQUE INDEX reg_969_quant_pk ON public.core_td_attachments USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_967_quant_pk'))then
		CREATE UNIQUE INDEX reg_967_quant_pk ON public.core_td_audit USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_964_quant_pk'))then
		CREATE UNIQUE INDEX reg_964_quant_pk ON public.core_td_changeset USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_963_quant_pk'))then
		CREATE UNIQUE INDEX reg_963_quant_pk ON public.core_td_instance USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_961_quant_pk'))then
		CREATE UNIQUE INDEX reg_961_quant_pk ON public.core_td_template_version USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_997_quant_pk'))then
		CREATE UNIQUE INDEX reg_997_quant_pk ON public.core_updstru_log USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_303_pk'))then
		CREATE UNIQUE INDEX reg_303_pk ON public.insur_bank_plat USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_303000300'))then
		CREATE INDEX ind_303000300 ON public.insur_bank_plat USING btree (link_svod_bank);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_303000200'))then
		CREATE INDEX ind_303000200 ON public.insur_bank_plat USING btree (link_id_file);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_304_pk'))then
		CREATE UNIQUE INDEX reg_304_pk ON public.insur_svod_bank USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_304000200'))then
		CREATE INDEX ind_304000200 ON public.insur_svod_bank USING btree (link_id_file);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('fias_addrobj_parentguid_idx'))then
		CREATE INDEX fias_addrobj_parentguid_idx ON public.fias_addrobj USING btree (parentguid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('fias_addrobj_aoguid_idx'))then
		CREATE INDEX fias_addrobj_aoguid_idx ON public.fias_addrobj USING btree (aoguid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('fias_house_houseguid_idx'))then
		CREATE INDEX fias_house_houseguid_idx ON public.fias_house USING btree (houseguid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('fias_house_aoguid_houseguid_idx'))then
		CREATE INDEX fias_house_aoguid_houseguid_idx ON public.fias_house USING btree (aoguid, houseguid);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_50_quant_inx_s_po_'))then
		CREATE INDEX reg_50_quant_inx_s_po_ ON public.bti_address_q USING btree (s_, po_);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_link_causes_subreason_lp_pkey'))then
		CREATE UNIQUE INDEX insur_link_causes_subreason_lp_pkey ON public.insur_link_causes_subreason_lp USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_gbu_no_pay_reason_pkey'))then
		CREATE UNIQUE INDEX insur_gbu_no_pay_reason_pkey ON public.insur_gbu_no_pay_reason USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_312_pk'))then
		CREATE UNIQUE INDEX reg_312_pk ON public.insur_param_calculation USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_312000200'))then
		CREATE INDEX ind_312000200 ON public.insur_param_calculation USING btree (obj_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_comment_pkey'))then
		CREATE UNIQUE INDEX insur_comment_pkey ON public.insur_comment USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('core_srd_function_idx'))then
		CREATE UNIQUE INDEX core_srd_function_idx ON public.core_srd_function USING btree (functiontag);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_313_pk'))then
		CREATE UNIQUE INDEX reg_313_pk ON public.insur_damage USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ind_313000200'))then
		CREATE INDEX ind_313000200 ON public.insur_damage USING btree (obj_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_file_plat_identify_log_pkey'))then
		CREATE UNIQUE INDEX insur_file_plat_identify_log_pkey ON public.insur_file_plat_identify_log USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_build_parcel_q_pkey'))then
		CREATE UNIQUE INDEX ehd_build_parcel_q_pkey ON public.ehd_build_parcel_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_register_q_pkey'))then
		CREATE UNIQUE INDEX ehd_register_q_pkey ON public.ehd_register_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_location_q_pkey'))then
		CREATE UNIQUE INDEX ehd_location_q_pkey ON public.ehd_location_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('core_file_storage_pkey'))then
		CREATE UNIQUE INDEX core_file_storage_pkey ON public.insur_file_storage USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_subject_pkey'))then
		CREATE UNIQUE INDEX insur_subject_pkey ON public.insur_subject USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_old_numbers_pkey'))then
		CREATE UNIQUE INDEX ehd_old_numbers_pkey ON public.ehd_old_numbers USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_changes_log_pkey'))then
		CREATE UNIQUE INDEX insur_changes_log_pkey ON public.insur_changes_log USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('ehd_register_q_kn_oks_idx'))then
		CREATE INDEX ehd_register_q_kn_oks_idx ON public.ehd_register_q USING btree (cadastral_number_oks);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_flat_q_key'))then
		CREATE UNIQUE INDEX insur_flat_q_key ON public.insur_flat_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_flat_q_emp_id_idx'))then
		CREATE INDEX insur_flat_q_emp_id_idx ON public.insur_flat_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_flat_q_unom_kvnom_idx'))then
		CREATE INDEX insur_flat_q_unom_kvnom_idx ON public.insur_flat_q USING btree (unom, kvnom);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_flat_q_flaginsur_cadastrnum_idx'))then
		CREATE INDEX insur_flat_q_flaginsur_cadastrnum_idx ON public.insur_flat_q USING btree (cadastr_num, flag_insur);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_954_quant_pk'))then
		CREATE UNIQUE INDEX reg_954_quant_pk ON public.core_srd_usersettingslayout USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('import_log_insur_building_pkey'))then
		CREATE UNIQUE INDEX import_log_insur_building_pkey ON public.import_log_insur_building USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_type_building_floor_link_pkey'))then
		CREATE UNIQUE INDEX insur_type_building_floor_link_pkey ON public.insur_type_building_floor_link USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_308_pk'))then
		CREATE UNIQUE INDEX reg_308_pk ON public.insur_fsp_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_fsp_q_empidx'))then
		CREATE INDEX insur_fsp_q_empidx ON public.insur_fsp_q USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_file_process_log_pkey'))then
		CREATE UNIQUE INDEX insur_file_process_log_pkey ON public.insur_file_process_log USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_docunents_pkey'))then
		CREATE UNIQUE INDEX insur_docunents_pkey ON public.insur_documents USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_254_quant_pk'))then
		CREATE UNIQUE INDEX reg_254_quant_pk ON public.bti_premase USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_254_floor_id_inx'))then
		CREATE INDEX reg_254_floor_id_inx ON public.bti_premase USING btree (floor_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_319_pk'))then
		CREATE UNIQUE INDEX reg_319_pk ON public.insur_address USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('system_daily_statistics_pk'))then
		CREATE UNIQUE INDEX system_daily_statistics_pk ON public.system_daily_statistics USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('system_daily_stat_date_uk'))then
		CREATE UNIQUE INDEX system_daily_stat_date_uk ON public.system_daily_statistics USING btree (stat_date);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_building_q_key'))then
		CREATE UNIQUE INDEX insur_building_q_key ON public.insur_building_q USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_building_q_cadastr_num_idx'))then
		CREATE INDEX insur_building_q_cadastr_num_idx ON public.insur_building_q USING btree (cadastr_num);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_building_q_link_bti_fsks_idx'))then
		CREATE INDEX insur_building_q_link_bti_fsks_idx ON public.insur_building_q USING btree (link_bti_fsks);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_reestr_pay_pkey'))then
		CREATE UNIQUE INDEX insur_reestr_pay_pkey ON public.insur_reestr_pay USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_policy_svd_kodpl_idx'))then
		CREATE INDEX insur_policy_svd_kodpl_idx ON public.insur_policy_svd USING btree (kodpl);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('pk_system_daily_stat_file_stor'))then
		CREATE UNIQUE INDEX pk_system_daily_stat_file_stor ON public.system_daily_stat_file_stor USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_fsp_q_contract_id_idx'))then
		CREATE INDEX insur_fsp_q_contract_id_idx ON public.insur_fsp_q USING btree (contract_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_fsp_q_fsp_number_idx'))then
		CREATE INDEX insur_fsp_q_fsp_number_idx ON public.insur_fsp_q USING btree (fsp_number);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_926_quant_pk'))then
		CREATE UNIQUE INDEX reg_926_quant_pk ON public.core_srd_usersettingsreport USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_975_quant_pk'))then
		CREATE UNIQUE INDEX reg_975_quant_pk ON public.core_long_process_queue USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('insur_invoice_q_pkey'))then
		CREATE UNIQUE INDEX insur_invoice_q_pkey ON public.insur_invoice USING btree (emp_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('core_messages_pk'))then
		CREATE UNIQUE INDEX core_messages_pk ON public.core_messages USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('core_messages_to_pk'))then
		CREATE UNIQUE INDEX core_messages_to_pk ON public.core_messages_to USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('reg_950_quant_pk'))then
		CREATE UNIQUE INDEX reg_950_quant_pk ON public.core_srd_user USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('import_log_insur_flat_pkey'))then
		CREATE UNIQUE INDEX import_log_insur_flat_pkey ON public.import_log_insur_flat USING btree (id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('import_log_insur_flat_bti_id_idx'))then
		CREATE INDEX import_log_insur_flat_bti_id_idx ON public.import_log_insur_flat USING btree (bti_flat_id);
    end if;
END $$;
--<DO>--
DO $$
BEGIN
	if(not core_updstru_checkexistindex('import_log_insur_flat_ehd_id_idx'))then
		CREATE INDEX import_log_insur_flat_ehd_id_idx ON public.import_log_insur_flat USING btree (ehd_parcel_id);
    end if;
END $$;