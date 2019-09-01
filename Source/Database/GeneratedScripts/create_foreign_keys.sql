
-- ### Скрипт создания внешних ключей

--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_addrlink_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_52_quant_fk_o'))then
    	    ALTER TABLE bti_addrlink_q
        	    ADD CONSTRAINT reg_52_quant_fk_o FOREIGN KEY (emp_id) REFERENCES bti_addrlink_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_addrlink_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('fk_bti_addrlink_q_address_o'))then
    	    ALTER TABLE bti_addrlink_q
        	    ADD CONSTRAINT fk_bti_addrlink_q_address_o FOREIGN KEY (address_id) REFERENCES bti_address_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_address_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_50_quant_fk_o'))then
    	    ALTER TABLE bti_address_q
        	    ADD CONSTRAINT reg_50_quant_fk_o FOREIGN KEY (emp_id) REFERENCES bti_address_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_dop_all_property')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_dop_all_property_contract_id_fkey'))then
    	    ALTER TABLE insur_dop_all_property
        	    ADD CONSTRAINT insur_dop_all_property_contract_id_fkey FOREIGN KEY (contract_id) REFERENCES insur_all_property(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_policy_svd')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_policy_svd_fsp_id_fkey'))then
    	    ALTER TABLE insur_policy_svd
        	    ADD CONSTRAINT insur_policy_svd_fsp_id_fkey FOREIGN KEY (fsp_id) REFERENCES insur_fsp_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_policy_svd')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_policy_svd_link_id_file_fkey'))then
    	    ALTER TABLE insur_policy_svd
        	    ADD CONSTRAINT insur_policy_svd_link_id_file_fkey FOREIGN KEY (link_id_file) REFERENCES insur_input_file(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_nach')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_input_nach_district_id_fkey'))then
    	    ALTER TABLE insur_input_nach
        	    ADD CONSTRAINT insur_input_nach_district_id_fkey FOREIGN KEY (district_id) REFERENCES insur_district(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_nach')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_input_nach_link_id_file_fkey'))then
    	    ALTER TABLE insur_input_nach
        	    ADD CONSTRAINT insur_input_nach_link_id_file_fkey FOREIGN KEY (link_id_file) REFERENCES insur_input_file(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_nach')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_input_nach_fsp_id_fkey'))then
    	    ALTER TABLE insur_input_nach
        	    ADD CONSTRAINT insur_input_nach_fsp_id_fkey FOREIGN KEY (fsp_id) REFERENCES insur_fsp_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_building_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_251_quant_fk_o'))then
    	    ALTER TABLE bti_building_q
        	    ADD CONSTRAINT reg_251_quant_fk_o FOREIGN KEY (emp_id) REFERENCES bti_building_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_floor_q')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('reg_253_quant_fk_o'))then
    	    ALTER TABLE bti_floor_q
        	    ADD CONSTRAINT reg_253_quant_fk_o FOREIGN KEY (emp_id) REFERENCES bti_floor_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_bank_plat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_bank_plat_link_id_file_fkey'))then
    	    ALTER TABLE insur_bank_plat
        	    ADD CONSTRAINT insur_bank_plat_link_id_file_fkey FOREIGN KEY (link_id_file) REFERENCES insur_input_file(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_bank_plat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_bank_plat_link_svod_bank_fkey'))then
    	    ALTER TABLE insur_bank_plat
        	    ADD CONSTRAINT insur_bank_plat_link_svod_bank_fkey FOREIGN KEY (link_svod_bank) REFERENCES insur_svod_bank(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_svod_bank')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_svod_bank_link_id_file_fkey'))then
    	    ALTER TABLE insur_svod_bank
        	    ADD CONSTRAINT insur_svod_bank_link_id_file_fkey FOREIGN KEY (link_id_file) REFERENCES insur_input_file(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_pay_to')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_pay_to_fsp_id_fkey'))then
    	    ALTER TABLE insur_pay_to
        	    ADD CONSTRAINT insur_pay_to_fsp_id_fkey FOREIGN KEY (fsp_id) REFERENCES insur_fsp_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_no_pay')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_no_pay_fsp_id_fkey'))then
    	    ALTER TABLE insur_no_pay
        	    ADD CONSTRAINT insur_no_pay_fsp_id_fkey FOREIGN KEY (fsp_id) REFERENCES insur_fsp_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_plat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_input_plat_link_id_file_fkey'))then
    	    ALTER TABLE insur_input_plat
        	    ADD CONSTRAINT insur_input_plat_link_id_file_fkey FOREIGN KEY (link_id_file) REFERENCES insur_input_file(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_plat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_input_plat_fsp_id_fkey'))then
    	    ALTER TABLE insur_input_plat
        	    ADD CONSTRAINT insur_input_plat_fsp_id_fkey FOREIGN KEY (fsp_id) REFERENCES insur_fsp_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_plat')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_input_plat_link_bank_id_fkey'))then
    	    ALTER TABLE insur_input_plat
        	    ADD CONSTRAINT insur_input_plat_link_bank_id_fkey FOREIGN KEY (link_bank_id) REFERENCES insur_bank_plat(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_log_file')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_log_file_link_id_file_fkey'))then
    	    ALTER TABLE insur_log_file
        	    ADD CONSTRAINT insur_log_file_link_id_file_fkey FOREIGN KEY (file_storage_id) REFERENCES insur_file_storage(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_balance')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_balance_fsp_id_fkey'))then
    	    ALTER TABLE insur_balance
        	    ADD CONSTRAINT insur_balance_fsp_id_fkey FOREIGN KEY (fsp_id) REFERENCES insur_fsp_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_balance')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_balance_link_input_nach_fkey'))then
    	    ALTER TABLE insur_balance
        	    ADD CONSTRAINT insur_balance_link_input_nach_fkey FOREIGN KEY (link_input_nach) REFERENCES insur_input_nach(emp_id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_all_property')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_all_property_fsp_id_fkey'))then
    	    ALTER TABLE insur_all_property
        	    ADD CONSTRAINT insur_all_property_fsp_id_fkey FOREIGN KEY (fsp_id) REFERENCES insur_fsp_o(id);
	    end if;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_param_calculation')) IS NOT NULL)then
	    if(not core_updstru_checkexistconstraint('insur_param_calculation_obj_id_fkey'))then
    	    ALTER TABLE insur_param_calculation
        	    ADD CONSTRAINT insur_param_calculation_obj_id_fkey FOREIGN KEY (contract_id) REFERENCES insur_all_property(emp_id);
	    end if;
    end if;
END $$;