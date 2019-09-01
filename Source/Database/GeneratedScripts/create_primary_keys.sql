
-- ### Скрипт создания первичнх ключей

--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_insur_rate')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_insur_rate_pkey'))then
    	    ALTER TABLE insur_insur_rate
        	    ADD CONSTRAINT insur_insur_rate_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_reference')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_982_quant_pk'))then
    	    ALTER TABLE core_reference
        	    ADD CONSTRAINT reg_982_quant_pk PRIMARY KEY (referenceid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_regnom_sequences')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_995_quant_pk'))then
    	    ALTER TABLE core_regnom_sequences
        	    ADD CONSTRAINT reg_995_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_reference_item')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_983_quant_pk'))then
    	    ALTER TABLE core_reference_item
        	    ADD CONSTRAINT reg_983_quant_pk PRIMARY KEY (itemid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_reference_relation')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_984_quant_pk'))then
    	    ALTER TABLE core_reference_relation
        	    ADD CONSTRAINT reg_984_quant_pk PRIMARY KEY (relid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_reference_tree')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_985_quant_pk'))then
    	    ALTER TABLE core_reference_tree
        	    ADD CONSTRAINT reg_985_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_audit_action')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_966_quant_pk'))then
    	    ALTER TABLE core_td_audit_action
        	    ADD CONSTRAINT reg_966_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_change')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_965_quant_pk'))then
    	    ALTER TABLE core_td_change
        	    ADD CONSTRAINT reg_965_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_tree')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_968_quant_pk'))then
    	    ALTER TABLE core_td_tree
        	    ADD CONSTRAINT reg_968_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_template_type')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_971_quant_pk'))then
    	    ALTER TABLE core_td_template_type
        	    ADD CONSTRAINT reg_971_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_tp')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_970_quant_pk'))then
    	    ALTER TABLE core_td_tp
        	    ADD CONSTRAINT reg_970_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_status')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_962_quant_pk'))then
    	    ALTER TABLE core_td_status
        	    ADD CONSTRAINT reg_962_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role_filter')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_955_quant_pk'))then
    	    ALTER TABLE core_srd_role_filter
        	    ADD CONSTRAINT reg_955_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_template')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_960_quant_pk'))then
    	    ALTER TABLE core_td_template
        	    ADD CONSTRAINT reg_960_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_function_reg_cat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_958_quant_pk'))then
    	    ALTER TABLE core_srd_function_reg_cat
        	    ADD CONSTRAINT reg_958_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_register_category')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_957_quant_pk'))then
    	    ALTER TABLE core_srd_register_category
        	    ADD CONSTRAINT reg_957_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('dashboards_dashboard')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_850_quant_pk'))then
    	    ALTER TABLE dashboards_dashboard
        	    ADD CONSTRAINT reg_850_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_integrated_indicators_repl_cost')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_integrated_indicators_replement_cost_pkey'))then
    	    ALTER TABLE insur_integrated_indicators_repl_cost
        	    ADD CONSTRAINT insur_integrated_indicators_replement_cost_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('spd_usersrd2spd')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_651_quant_pk'))then
    	    ALTER TABLE spd_usersrd2spd
        	    ADD CONSTRAINT reg_651_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_usersettingsregview')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_925_quant_pk'))then
    	    ALTER TABLE core_srd_usersettingsregview
        	    ADD CONSTRAINT reg_925_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('dashboards_user_settings')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_853_quant_pk'))then
    	    ALTER TABLE dashboards_user_settings
        	    ADD CONSTRAINT reg_853_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('dashboards_panel')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_851_quant_pk'))then
    	    ALTER TABLE dashboards_panel
        	    ADD CONSTRAINT reg_851_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('dashboards_panel_type')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_852_quant_pk'))then
    	    ALTER TABLE dashboards_panel_type
        	    ADD CONSTRAINT reg_852_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_list_object')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_921_quant_pk'))then
    	    ALTER TABLE core_list_object
        	    ADD CONSTRAINT reg_921_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_layout_column_type')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_924_quant_pk'))then
    	    ALTER TABLE core_layout_column_type
        	    ADD CONSTRAINT reg_924_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_qry_filter')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_937_quant_pk'))then
    	    ALTER TABLE core_qry_filter
        	    ADD CONSTRAINT reg_937_quant_pk PRIMARY KEY (qryfilterid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_layout_details')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_935_quant_pk'))then
    	    ALTER TABLE core_layout_details
        	    ADD CONSTRAINT reg_935_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_qry_operation')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_938_quant_pk'))then
    	    ALTER TABLE core_qry_operation
        	    ADD CONSTRAINT reg_938_quant_pk PRIMARY KEY (qryoperationid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_flat_status')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('INSUR_FLAT_STATUS_pkey'))then
    	    ALTER TABLE insur_flat_status
        	    ADD CONSTRAINT INSUR_FLAT_STATUS_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_attachment_file')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_987_quant_pk'))then
    	    ALTER TABLE core_attachment_file
        	    ADD CONSTRAINT reg_987_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_fsp_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_308_obj_pk'))then
    	    ALTER TABLE insur_fsp_o
        	    ADD CONSTRAINT reg_308_obj_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_flat_type')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('flat_type_pkey'))then
    	    ALTER TABLE insur_flat_type
        	    ADD CONSTRAINT flat_type_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_link_build_bti')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_326_pk'))then
    	    ALTER TABLE insur_link_build_bti
        	    ADD CONSTRAINT reg_326_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_link_flat_egrn')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_327_pk'))then
    	    ALTER TABLE insur_link_flat_egrn
        	    ADD CONSTRAINT reg_327_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_floor_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_253_object_pk'))then
    	    ALTER TABLE bti_floor_o
        	    ADD CONSTRAINT reg_253_object_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_addrlink_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_52_object_pk'))then
    	    ALTER TABLE bti_addrlink_o
        	    ADD CONSTRAINT reg_52_object_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_addrlink_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_52_quant_pk'))then
    	    ALTER TABLE bti_addrlink_q
        	    ADD CONSTRAINT reg_52_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_address_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_50_object_pk'))then
    	    ALTER TABLE bti_address_o
        	    ADD CONSTRAINT reg_50_object_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_address_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_50_quant_pk'))then
    	    ALTER TABLE bti_address_q
        	    ADD CONSTRAINT reg_50_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_rooms')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_257_quant_pk'))then
    	    ALTER TABLE bti_rooms
        	    ADD CONSTRAINT reg_257_quant_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_base_tariff')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_330_pk'))then
    	    ALTER TABLE insur_base_tariff
        	    ADD CONSTRAINT reg_330_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_scan_doc')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_scan_doc_pkey'))then
    	    ALTER TABLE insur_scan_doc
        	    ADD CONSTRAINT insur_scan_doc_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_damage_assessment_radio_tv_phone')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_damage_assessment_radio_tv_phone_pkey'))then
    	    ALTER TABLE insur_damage_assessment_radio_tv_phone
        	    ADD CONSTRAINT insur_damage_assessment_radio_tv_phone_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ref_addr_district')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ref_addr_district_pkey'))then
    	    ALTER TABLE ref_addr_district
        	    ADD CONSTRAINT ref_addr_district_pkey PRIMARY KEY (district_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ref_addr_street')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ref_addr_street_pkey'))then
    	    ALTER TABLE ref_addr_street
        	    ADD CONSTRAINT ref_addr_street_pkey PRIMARY KEY (street_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_file_package')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_input_file_package_pkey'))then
    	    ALTER TABLE insur_input_file_package
        	    ADD CONSTRAINT insur_input_file_package_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_part_compensation')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_329_pk'))then
    	    ALTER TABLE insur_part_compensation
        	    ADD CONSTRAINT reg_329_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_share_responsibility_ic_city')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_share_responsibility_IC_City_pkey'))then
    	    ALTER TABLE insur_share_responsibility_ic_city
        	    ADD CONSTRAINT insur_share_responsibility_IC_City_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_dop_all_property')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_311_pk'))then
    	    ALTER TABLE insur_dop_all_property
        	    ADD CONSTRAINT reg_311_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_insurance_organization')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_insurance_organization_pkey'))then
    	    ALTER TABLE insur_insurance_organization
        	    ADD CONSTRAINT insur_insurance_organization_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_policy_svd')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_309_pk'))then
    	    ALTER TABLE insur_policy_svd
        	    ADD CONSTRAINT reg_309_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_nach')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_305_pk'))then
    	    ALTER TABLE insur_input_nach
        	    ADD CONSTRAINT reg_305_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_build_parcel_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('build_parcel_from_ehd_o_pkey'))then
    	    ALTER TABLE ehd_build_parcel_o
        	    ADD CONSTRAINT build_parcel_from_ehd_o_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_location_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ehd_location_q_pkey'))then
    	    ALTER TABLE ehd_location_q
        	    ADD CONSTRAINT ehd_location_q_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_location_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('location_from_ehd_o_pkey'))then
    	    ALTER TABLE ehd_location_o
        	    ADD CONSTRAINT location_from_ehd_o_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fm_reports_savedreport')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('fm_reports_savedreport_pkey'))then
    	    ALTER TABLE fm_reports_savedreport
        	    ADD CONSTRAINT fm_reports_savedreport_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fm_podpisant')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('fm_podpisant_pkey'))then
    	    ALTER TABLE fm_podpisant
        	    ADD CONSTRAINT fm_podpisant_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_tariff')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_tariff_pkey'))then
    	    ALTER TABLE insur_tariff
        	    ADD CONSTRAINT insur_tariff_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_building_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_building_o_pkey'))then
    	    ALTER TABLE insur_building_o
        	    ADD CONSTRAINT insur_building_o_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_flat_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_flat_o_pkey'))then
    	    ALTER TABLE insur_flat_o
        	    ADD CONSTRAINT insur_flat_o_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_actual_cost_ratio')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_actual_cost_ratio_pkey'))then
    	    ALTER TABLE insur_actual_cost_ratio
        	    ADD CONSTRAINT insur_actual_cost_ratio_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_build_parcel_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ehd_build_parcel_q_pkey'))then
    	    ALTER TABLE ehd_build_parcel_q
        	    ADD CONSTRAINT ehd_build_parcel_q_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_right_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ehd_right_o_pkey'))then
    	    ALTER TABLE ehd_right_o
        	    ADD CONSTRAINT ehd_right_o_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_egrp_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ehd_egrn_o_pkey'))then
    	    ALTER TABLE ehd_egrp_o
        	    ADD CONSTRAINT ehd_egrn_o_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_egrp_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ehd_egrn_q_pkey'))then
    	    ALTER TABLE ehd_egrp_q
        	    ADD CONSTRAINT ehd_egrn_q_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_right_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ehd_right_q_pkey'))then
    	    ALTER TABLE ehd_right_q
        	    ADD CONSTRAINT ehd_right_q_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_register_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ehd_register_q_pkey'))then
    	    ALTER TABLE ehd_register_q
        	    ADD CONSTRAINT ehd_register_q_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_register_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('registr_from_ehd_o_pkey'))then
    	    ALTER TABLE ehd_register_o
        	    ADD CONSTRAINT registr_from_ehd_o_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fias_estatestatus')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('xpkfias_estatestatus'))then
    	    ALTER TABLE fias_estatestatus
        	    ADD CONSTRAINT xpkfias_estatestatus PRIMARY KEY (estatestatusid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fias_structurestatus')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('xpkfias_structurestatus'))then
    	    ALTER TABLE fias_structurestatus
        	    ADD CONSTRAINT xpkfias_structurestatus PRIMARY KEY (structurestatusid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('spd_request_registration')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_600_quant_pk'))then
    	    ALTER TABLE spd_request_registration
        	    ADD CONSTRAINT reg_600_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('spd_doc_agreement')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_650_quant_pk'))then
    	    ALTER TABLE spd_doc_agreement
        	    ADD CONSTRAINT reg_650_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('spd_create_full_app_log')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_601_quant_pk'))then
    	    ALTER TABLE spd_create_full_app_log
        	    ADD CONSTRAINT reg_601_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_building_o')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_251_obj_pk'))then
    	    ALTER TABLE bti_building_o
        	    ADD CONSTRAINT reg_251_obj_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_building_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_251_quant_pk'))then
    	    ALTER TABLE bti_building_q
        	    ADD CONSTRAINT reg_251_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_floor_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_253_quant_pk'))then
    	    ALTER TABLE bti_floor_q
        	    ADD CONSTRAINT reg_253_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_attachment')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_986_quant_pk'))then
    	    ALTER TABLE core_attachment
        	    ADD CONSTRAINT reg_986_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_attachment_object')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_988_quant_pk'))then
    	    ALTER TABLE core_attachment_object
        	    ADD CONSTRAINT reg_988_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_cache_updates')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_996_quant_pk'))then
    	    ALTER TABLE core_cache_updates
        	    ADD CONSTRAINT reg_996_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_configparam')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_978_quant_pk'))then
    	    ALTER TABLE core_configparam
        	    ADD CONSTRAINT reg_978_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_diagnostics')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_992_quant_pk'))then
    	    ALTER TABLE core_diagnostics
        	    ADD CONSTRAINT reg_992_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_error_log')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_989_quant_pk'))then
    	    ALTER TABLE core_error_log
        	    ADD CONSTRAINT reg_989_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_holidays')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_998_quant_pk'))then
    	    ALTER TABLE core_holidays
        	    ADD CONSTRAINT reg_998_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_layout')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_933_quant_pk'))then
    	    ALTER TABLE core_layout
        	    ADD CONSTRAINT reg_933_quant_pk PRIMARY KEY (layoutid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_layout_export')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_956_quant_pk'))then
    	    ALTER TABLE core_layout_export
        	    ADD CONSTRAINT reg_956_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_list')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_920_quant_pk'))then
    	    ALTER TABLE core_list
        	    ADD CONSTRAINT reg_920_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_long_process_log')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_977_quant_pk'))then
    	    ALTER TABLE core_long_process_log
        	    ADD CONSTRAINT reg_977_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_long_process_type')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_976_quant_pk'))then
    	    ALTER TABLE core_long_process_type
        	    ADD CONSTRAINT reg_976_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register_lock')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_939_quant_pk'))then
    	    ALTER TABLE core_register_lock
        	    ADD CONSTRAINT reg_939_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register_state')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_991_quant_pk'))then
    	    ALTER TABLE core_register_state
        	    ADD CONSTRAINT reg_991_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_audit')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_940_quant_pk'))then
    	    ALTER TABLE core_srd_audit
        	    ADD CONSTRAINT reg_940_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_session')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_949_quant_pk'))then
    	    ALTER TABLE core_srd_session
        	    ADD CONSTRAINT reg_949_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_attachments')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_969_quant_pk'))then
    	    ALTER TABLE core_td_attachments
        	    ADD CONSTRAINT reg_969_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_audit')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_967_quant_pk'))then
    	    ALTER TABLE core_td_audit
        	    ADD CONSTRAINT reg_967_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_changeset')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_964_quant_pk'))then
    	    ALTER TABLE core_td_changeset
        	    ADD CONSTRAINT reg_964_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_instance')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_963_quant_pk'))then
    	    ALTER TABLE core_td_instance
        	    ADD CONSTRAINT reg_963_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_template_version')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_961_quant_pk'))then
    	    ALTER TABLE core_td_template_version
        	    ADD CONSTRAINT reg_961_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_updstru_log')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_997_quant_pk'))then
    	    ALTER TABLE core_updstru_log
        	    ADD CONSTRAINT reg_997_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_bank_plat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_303_pk'))then
    	    ALTER TABLE insur_bank_plat
        	    ADD CONSTRAINT reg_303_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_svod_bank')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_304_pk'))then
    	    ALTER TABLE insur_svod_bank
        	    ADD CONSTRAINT reg_304_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_qry')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_936_quant_pk'))then
    	    ALTER TABLE core_qry
        	    ADD CONSTRAINT reg_936_quant_pk PRIMARY KEY (qryid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_link_causes_subreason_lp')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_link_causes_subreason_lp_pkey'))then
    	    ALTER TABLE insur_link_causes_subreason_lp
        	    ADD CONSTRAINT insur_link_causes_subreason_lp_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_gbu_no_pay_reason')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_gbu_no_pay_reason_pkey'))then
    	    ALTER TABLE insur_gbu_no_pay_reason
        	    ADD CONSTRAINT insur_gbu_no_pay_reason_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_pay_to')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_314_pk'))then
    	    ALTER TABLE insur_pay_to
        	    ADD CONSTRAINT reg_314_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_no_pay')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_315_pk'))then
    	    ALTER TABLE insur_no_pay
        	    ADD CONSTRAINT reg_315_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_plat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_306_pk'))then
    	    ALTER TABLE insur_input_plat
        	    ADD CONSTRAINT reg_306_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_user_role')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_952_quant_pk'))then
    	    ALTER TABLE core_srd_user_role
        	    ADD CONSTRAINT reg_952_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role_register')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_947_quant_pk'))then
    	    ALTER TABLE core_srd_role_register
        	    ADD CONSTRAINT reg_947_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role_function')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_946_quant_pk'))then
    	    ALTER TABLE core_srd_role_function
        	    ADD CONSTRAINT reg_946_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_945_quant_pk'))then
    	    ALTER TABLE core_srd_role
        	    ADD CONSTRAINT reg_945_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role_attr')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_948_quant_pk'))then
    	    ALTER TABLE core_srd_role_attr
        	    ADD CONSTRAINT reg_948_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_function')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_942_quant_pk'))then
    	    ALTER TABLE core_srd_function
        	    ADD CONSTRAINT reg_942_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_department')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_941_quant_pk'))then
    	    ALTER TABLE core_srd_department
        	    ADD CONSTRAINT reg_941_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_comment')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_comment_pkey'))then
    	    ALTER TABLE insur_comment
        	    ADD CONSTRAINT insur_comment_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_log_file')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_302_pk'))then
    	    ALTER TABLE insur_log_file
        	    ADD CONSTRAINT reg_302_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_file_storage')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('core_file_storage_pkey'))then
    	    ALTER TABLE insur_file_storage
        	    ADD CONSTRAINT core_file_storage_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_subject')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_subject_pkey'))then
    	    ALTER TABLE insur_subject
        	    ADD CONSTRAINT insur_subject_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_old_numbers')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ehd_old_numbers_pkey'))then
    	    ALTER TABLE ehd_old_numbers
        	    ADD CONSTRAINT ehd_old_numbers_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_bank')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_bank_pkey'))then
    	    ALTER TABLE insur_bank
        	    ADD CONSTRAINT insur_bank_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_930_quant_pk'))then
    	    ALTER TABLE core_register
        	    ADD CONSTRAINT reg_930_quant_pk PRIMARY KEY (registerid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register_relation')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_932_quant_pk'))then
    	    ALTER TABLE core_register_relation
        	    ADD CONSTRAINT reg_932_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register_attribute')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_931_quant_pk'))then
    	    ALTER TABLE core_register_attribute
        	    ADD CONSTRAINT reg_931_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_changes_log')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_changes_log_pkey'))then
    	    ALTER TABLE insur_changes_log
        	    ADD CONSTRAINT insur_changes_log_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_usersettingslayout')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_954_quant_pk'))then
    	    ALTER TABLE core_srd_usersettingslayout
        	    ADD CONSTRAINT reg_954_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('import_log_insur_building')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('import_log_insur_building_pkey'))then
    	    ALTER TABLE import_log_insur_building
        	    ADD CONSTRAINT import_log_insur_building_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_type_building_floor_link')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_type_building_floor_link_pkey'))then
    	    ALTER TABLE insur_type_building_floor_link
        	    ADD CONSTRAINT insur_type_building_floor_link_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_fsp_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_308_pk'))then
    	    ALTER TABLE insur_fsp_q
        	    ADD CONSTRAINT reg_308_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_file_process_log')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_file_process_log_pkey'))then
    	    ALTER TABLE insur_file_process_log
        	    ADD CONSTRAINT insur_file_process_log_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_documents')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_docunents_pkey'))then
    	    ALTER TABLE insur_documents
        	    ADD CONSTRAINT insur_docunents_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_agreement_project')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_334_pk'))then
    	    ALTER TABLE insur_agreement_project
        	    ADD CONSTRAINT reg_334_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_doc_base_type')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_doc_base_type_pkey'))then
    	    ALTER TABLE insur_doc_base_type
        	    ADD CONSTRAINT insur_doc_base_type_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ref_addr_okrug')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('ref_addr_okrug_pkey'))then
    	    ALTER TABLE ref_addr_okrug
        	    ADD CONSTRAINT ref_addr_okrug_pkey PRIMARY KEY (okrug_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_flat_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_flat_q_key'))then
    	    ALTER TABLE insur_flat_q
        	    ADD CONSTRAINT insur_flat_q_key PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('system_daily_statistics')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('system_daily_statistics_pk'))then
    	    ALTER TABLE system_daily_statistics
        	    ADD CONSTRAINT system_daily_statistics_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_district')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_district_pkey'))then
    	    ALTER TABLE insur_district
        	    ADD CONSTRAINT insur_district_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_okrug')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_okrug_pkey'))then
    	    ALTER TABLE insur_okrug
        	    ADD CONSTRAINT insur_okrug_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_building_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_building_q_key'))then
    	    ALTER TABLE insur_building_q
        	    ADD CONSTRAINT insur_building_q_key PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_balance')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_307_pk'))then
    	    ALTER TABLE insur_balance
        	    ADD CONSTRAINT reg_307_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_reestr_pay')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_reestr_pay_pkey'))then
    	    ALTER TABLE insur_reestr_pay
        	    ADD CONSTRAINT insur_reestr_pay_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_file_plat_identify_log')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_file_plat_identify_log_pkey'))then
    	    ALTER TABLE insur_file_plat_identify_log
        	    ADD CONSTRAINT insur_file_plat_identify_log_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_all_property')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_310_pk'))then
    	    ALTER TABLE insur_all_property
        	    ADD CONSTRAINT reg_310_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_damage_amount')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_damage_amount_pkey'))then
    	    ALTER TABLE insur_damage_amount
        	    ADD CONSTRAINT insur_damage_amount_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('system_daily_stat_file_stor')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('pk_system_daily_stat_file_stor'))then
    	    ALTER TABLE system_daily_stat_file_stor
        	    ADD CONSTRAINT pk_system_daily_stat_file_stor PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_usersettingsreport')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_926_quant_pk'))then
    	    ALTER TABLE core_srd_usersettingsreport
        	    ADD CONSTRAINT reg_926_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_long_process_queue')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_975_quant_pk'))then
    	    ALTER TABLE core_long_process_queue
        	    ADD CONSTRAINT reg_975_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_invoice')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_invoice_q_pkey'))then
    	    ALTER TABLE insur_invoice
        	    ADD CONSTRAINT insur_invoice_q_pkey PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_damage_assessment_method')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insure_damage_assessment_method_pkey'))then
    	    ALTER TABLE insur_damage_assessment_method
        	    ADD CONSTRAINT insure_damage_assessment_method_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_premase')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_254_quant_pk'))then
    	    ALTER TABLE bti_premase
        	    ADD CONSTRAINT reg_254_quant_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_messages_to')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('core_messages_to_pk'))then
    	    ALTER TABLE core_messages_to
        	    ADD CONSTRAINT core_messages_to_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_usersettings')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_951_quant_pk'))then
    	    ALTER TABLE core_srd_usersettings
        	    ADD CONSTRAINT reg_951_quant_pk PRIMARY KEY (userid);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_address')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_319_pk'))then
    	    ALTER TABLE insur_address
        	    ADD CONSTRAINT reg_319_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_param_calculation')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_312_pk'))then
    	    ALTER TABLE insur_param_calculation
        	    ADD CONSTRAINT reg_312_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_user')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_950_quant_pk'))then
    	    ALTER TABLE core_srd_user
        	    ADD CONSTRAINT reg_950_quant_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_living_premise_insur_cost')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_living_premise_insur_cost_pkey'))then
    	    ALTER TABLE insur_living_premise_insur_cost
        	    ADD CONSTRAINT insur_living_premise_insur_cost_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_file')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_301_pk'))then
    	    ALTER TABLE insur_input_file
        	    ADD CONSTRAINT reg_301_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_messages')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('core_messages_pk'))then
    	    ALTER TABLE core_messages
        	    ADD CONSTRAINT core_messages_pk PRIMARY KEY (id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_damage')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_313_pk'))then
    	    ALTER TABLE insur_damage
        	    ADD CONSTRAINT reg_313_pk PRIMARY KEY (emp_id);
	    end if;        
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('import_log_insur_flat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('import_log_insur_flat_pkey'))then
    	    ALTER TABLE import_log_insur_flat
        	    ADD CONSTRAINT import_log_insur_flat_pkey PRIMARY KEY (id);
	    end if;        
    end if;
END $$;