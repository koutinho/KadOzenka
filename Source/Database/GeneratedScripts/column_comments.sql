
-- ###. Скрипт записи комментария(COMMENT) к столбцу

--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_insur_rate', 'id'))then
        COMMENT ON COLUMN public.insur_insur_rate.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_insur_rate', 'date_begin'))then
        COMMENT ON COLUMN public.insur_insur_rate.date_begin IS 'Дата начала действия тарифа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_insur_rate', 'tariff'))then
        COMMENT ON COLUMN public.insur_insur_rate.tariff IS 'Размер тарифа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'referenceid'))then
        COMMENT ON COLUMN public.core_reference.referenceid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'description'))then
        COMMENT ON COLUMN public.core_reference.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'viddoc'))then
        COMMENT ON COLUMN public.core_reference.viddoc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'readonly'))then
        COMMENT ON COLUMN public.core_reference.readonly IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'progid'))then
        COMMENT ON COLUMN public.core_reference.progid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'istree'))then
        COMMENT ON COLUMN public.core_reference.istree IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'controlheight'))then
        COMMENT ON COLUMN public.core_reference.controlheight IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'controlwidth'))then
        COMMENT ON COLUMN public.core_reference.controlwidth IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'controltype'))then
        COMMENT ON COLUMN public.core_reference.controltype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'hidereference'))then
        COMMENT ON COLUMN public.core_reference.hidereference IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'skiptreelevel'))then
        COMMENT ON COLUMN public.core_reference.skiptreelevel IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'customeditor'))then
        COMMENT ON COLUMN public.core_reference.customeditor IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'defaultvalue'))then
        COMMENT ON COLUMN public.core_reference.defaultvalue IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'displaytree'))then
        COMMENT ON COLUMN public.core_reference.displaytree IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'usetreehelper'))then
        COMMENT ON COLUMN public.core_reference.usetreehelper IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'istable'))then
        COMMENT ON COLUMN public.core_reference.istable IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference', 'name'))then
        COMMENT ON COLUMN public.core_reference.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_repository', 'regnomvalue'))then
        COMMENT ON COLUMN public.core_regnom_repository.regnomvalue IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_repository', 'idsequence'))then
        COMMENT ON COLUMN public.core_regnom_repository.idsequence IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_repository', 'regnomincrement'))then
        COMMENT ON COLUMN public.core_regnom_repository.regnomincrement IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'id'))then
        COMMENT ON COLUMN public.core_regnom_sequences.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'numeratorid'))then
        COMMENT ON COLUMN public.core_regnom_sequences.numeratorid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'regnomtype'))then
        COMMENT ON COLUMN public.core_regnom_sequences.regnomtype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par1'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par1 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par0'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par0 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par2'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par2 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par3'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par3 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par4'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par4 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par5'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par5 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par6'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par6 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par7'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par7 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par8'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par8 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'par9'))then
        COMMENT ON COLUMN public.core_regnom_sequences.par9 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_regnom_sequences', 'currentincrement'))then
        COMMENT ON COLUMN public.core_regnom_sequences.currentincrement IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_common_property_tariff', 'id'))then
        COMMENT ON COLUMN public.insur_common_property_tariff.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_common_property_tariff', 'date_begin'))then
        COMMENT ON COLUMN public.insur_common_property_tariff.date_begin IS 'Дата начала действия';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_common_property_tariff', 'category'))then
        COMMENT ON COLUMN public.insur_common_property_tariff.category IS 'Категория';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_common_property_tariff', 'value'))then
        COMMENT ON COLUMN public.insur_common_property_tariff.value IS 'Значение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'itemid'))then
        COMMENT ON COLUMN public.core_reference_item.itemid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'referenceid'))then
        COMMENT ON COLUMN public.core_reference_item.referenceid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'code'))then
        COMMENT ON COLUMN public.core_reference_item.code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'value'))then
        COMMENT ON COLUMN public.core_reference_item.value IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'short_title'))then
        COMMENT ON COLUMN public.core_reference_item.short_title IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'is_archives'))then
        COMMENT ON COLUMN public.core_reference_item.is_archives IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'user_name'))then
        COMMENT ON COLUMN public.core_reference_item.user_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'date_end_change'))then
        COMMENT ON COLUMN public.core_reference_item.date_end_change IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'date_s'))then
        COMMENT ON COLUMN public.core_reference_item.date_s IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'flag'))then
        COMMENT ON COLUMN public.core_reference_item.flag IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_item', 'name'))then
        COMMENT ON COLUMN public.core_reference_item.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_relation', 'relid'))then
        COMMENT ON COLUMN public.core_reference_relation.relid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_relation', 'parentkey'))then
        COMMENT ON COLUMN public.core_reference_relation.parentkey IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_relation', 'childkey'))then
        COMMENT ON COLUMN public.core_reference_relation.childkey IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_relation', 'parentref'))then
        COMMENT ON COLUMN public.core_reference_relation.parentref IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_relation', 'childref'))then
        COMMENT ON COLUMN public.core_reference_relation.childref IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_relation', 'parentreq'))then
        COMMENT ON COLUMN public.core_reference_relation.parentreq IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_relation', 'treelevel'))then
        COMMENT ON COLUMN public.core_reference_relation.treelevel IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_tree', 'id'))then
        COMMENT ON COLUMN public.core_reference_tree.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_tree', 'code'))then
        COMMENT ON COLUMN public.core_reference_tree.code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_tree', 'parentid'))then
        COMMENT ON COLUMN public.core_reference_tree.parentid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_tree', 'childid'))then
        COMMENT ON COLUMN public.core_reference_tree.childid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_tree', 'referenceid'))then
        COMMENT ON COLUMN public.core_reference_tree.referenceid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_tree', 'level'))then
        COMMENT ON COLUMN public.core_reference_tree.level IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_tree', 'cldreferenceid'))then
        COMMENT ON COLUMN public.core_reference_tree.cldreferenceid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_reference_tree', 'adress_type'))then
        COMMENT ON COLUMN public.core_reference_tree.adress_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit_action', 'id'))then
        COMMENT ON COLUMN public.core_td_audit_action.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit_action', 'name'))then
        COMMENT ON COLUMN public.core_td_audit_action.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_change', 'id'))then
        COMMENT ON COLUMN public.core_td_change.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_change', 'changeset_id'))then
        COMMENT ON COLUMN public.core_td_change.changeset_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_change', 'register_id'))then
        COMMENT ON COLUMN public.core_td_change.register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_change', 'object_id'))then
        COMMENT ON COLUMN public.core_td_change.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_change', 'quant_id'))then
        COMMENT ON COLUMN public.core_td_change.quant_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_change', 'action'))then
        COMMENT ON COLUMN public.core_td_change.action IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_tree', 'id'))then
        COMMENT ON COLUMN public.core_td_tree.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_tree', 'parent_id'))then
        COMMENT ON COLUMN public.core_td_tree.parent_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_tree', 'folder_name'))then
        COMMENT ON COLUMN public.core_td_tree.folder_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_tree', 'template_id'))then
        COMMENT ON COLUMN public.core_td_tree.template_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_tree', 'tree_order'))then
        COMMENT ON COLUMN public.core_td_tree.tree_order IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_type', 'id'))then
        COMMENT ON COLUMN public.core_td_template_type.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_type', 'name'))then
        COMMENT ON COLUMN public.core_td_template_type.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_tp', 'id'))then
        COMMENT ON COLUMN public.core_td_tp.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_status', 'id'))then
        COMMENT ON COLUMN public.core_td_status.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_status', 'name'))then
        COMMENT ON COLUMN public.core_td_status.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_filter', 'id'))then
        COMMENT ON COLUMN public.core_srd_role_filter.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_filter', 'role_id'))then
        COMMENT ON COLUMN public.core_srd_role_filter.role_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_filter', 'register_id'))then
        COMMENT ON COLUMN public.core_srd_role_filter.register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_filter', 'register_view_id'))then
        COMMENT ON COLUMN public.core_srd_role_filter.register_view_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_filter', 'qscondition'))then
        COMMENT ON COLUMN public.core_srd_role_filter.qscondition IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_filter', 'description'))then
        COMMENT ON COLUMN public.core_srd_role_filter.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template', 'id'))then
        COMMENT ON COLUMN public.core_td_template.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template', 'current_version_id'))then
        COMMENT ON COLUMN public.core_td_template.current_version_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template', 'template_name'))then
        COMMENT ON COLUMN public.core_td_template.template_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template', 'scheme_name'))then
        COMMENT ON COLUMN public.core_td_template.scheme_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_function_reg_cat', 'id'))then
        COMMENT ON COLUMN public.core_srd_function_reg_cat.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_function_reg_cat', 'function_id'))then
        COMMENT ON COLUMN public.core_srd_function_reg_cat.function_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_function_reg_cat', 'register_category_id'))then
        COMMENT ON COLUMN public.core_srd_function_reg_cat.register_category_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_register_category', 'id'))then
        COMMENT ON COLUMN public.core_srd_register_category.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_register_category', 'register_id'))then
        COMMENT ON COLUMN public.core_srd_register_category.register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_register_category', 'parent_id'))then
        COMMENT ON COLUMN public.core_srd_register_category.parent_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_register_category', 'name'))then
        COMMENT ON COLUMN public.core_srd_register_category.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_register_category', 'qs_condition'))then
        COMMENT ON COLUMN public.core_srd_register_category.qs_condition IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_dashboard', 'id'))then
        COMMENT ON COLUMN public.dashboards_dashboard.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_dashboard', 'user_id'))then
        COMMENT ON COLUMN public.dashboards_dashboard.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_dashboard', 'layout_type'))then
        COMMENT ON COLUMN public.dashboards_dashboard.layout_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_dashboard', 'name'))then
        COMMENT ON COLUMN public.dashboards_dashboard.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_dashboard', 'description'))then
        COMMENT ON COLUMN public.dashboards_dashboard.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_dashboard', 'iscommon'))then
        COMMENT ON COLUMN public.dashboards_dashboard.iscommon IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'id'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'stove_type'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.stove_type IS 'Тип плиты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'stove_type_code'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.stove_type_code IS 'Тип плиты (code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'elements_constructions'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.elements_constructions IS 'Элемент конструкции';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'elements_constructions_code'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.elements_constructions_code IS 'Элемент конструкции (code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'floor_material'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.floor_material IS 'Материал пола';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'floor_material_code'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.floor_material_code IS 'Материал пола (code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'building_type'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.building_type IS 'Тип здания';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'building_type_code'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.building_type_code IS 'Тип здания (code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'cost_value'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.cost_value IS 'Удельный вес стоимости';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'parent_id'))then
        COMMENT ON COLUMN public.insur_integrated_indicators_repl_cost.parent_id IS 'Идентификтаор родителя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_usersrd2spd', 'id'))then
        COMMENT ON COLUMN public.spd_usersrd2spd.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_usersrd2spd', 'srd_user_id'))then
        COMMENT ON COLUMN public.spd_usersrd2spd.srd_user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_usersrd2spd', 'spd_user_id'))then
        COMMENT ON COLUMN public.spd_usersrd2spd.spd_user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_usersrd2spd', 'usercategory'))then
        COMMENT ON COLUMN public.spd_usersrd2spd.usercategory IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_usersrd2spd', 'usercategory_code'))then
        COMMENT ON COLUMN public.spd_usersrd2spd.usercategory_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingsregview', 'id'))then
        COMMENT ON COLUMN public.core_srd_usersettingsregview.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingsregview', 'user_id'))then
        COMMENT ON COLUMN public.core_srd_usersettingsregview.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingsregview', 'register_view_id'))then
        COMMENT ON COLUMN public.core_srd_usersettingsregview.register_view_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingsregview', 'fast_filter'))then
        COMMENT ON COLUMN public.core_srd_usersettingsregview.fast_filter IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_user_settings', 'id'))then
        COMMENT ON COLUMN public.dashboards_user_settings.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_user_settings', 'user_id'))then
        COMMENT ON COLUMN public.dashboards_user_settings.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_user_settings', 'default_panel_id'))then
        COMMENT ON COLUMN public.dashboards_user_settings.default_panel_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel', 'id'))then
        COMMENT ON COLUMN public.dashboards_panel.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel', 'dashboard_id'))then
        COMMENT ON COLUMN public.dashboards_panel.dashboard_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel', 'title'))then
        COMMENT ON COLUMN public.dashboards_panel.title IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel', 'column_index'))then
        COMMENT ON COLUMN public.dashboards_panel.column_index IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel', 'order_in_column'))then
        COMMENT ON COLUMN public.dashboards_panel.order_in_column IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel', 'panel_type_id'))then
        COMMENT ON COLUMN public.dashboards_panel.panel_type_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel', 'settings'))then
        COMMENT ON COLUMN public.dashboards_panel.settings IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel_type', 'id'))then
        COMMENT ON COLUMN public.dashboards_panel_type.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel_type', 'name'))then
        COMMENT ON COLUMN public.dashboards_panel_type.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel_type', 'description'))then
        COMMENT ON COLUMN public.dashboards_panel_type.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel_type', 'url'))then
        COMMENT ON COLUMN public.dashboards_panel_type.url IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('dashboards_panel_type', 'dto_class_full_name'))then
        COMMENT ON COLUMN public.dashboards_panel_type.dto_class_full_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list_object', 'id'))then
        COMMENT ON COLUMN public.core_list_object.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list_object', 'list_id'))then
        COMMENT ON COLUMN public.core_list_object.list_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list_object', 'object_id'))then
        COMMENT ON COLUMN public.core_list_object.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_column_type', 'id'))then
        COMMENT ON COLUMN public.core_layout_column_type.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_column_type', 'code'))then
        COMMENT ON COLUMN public.core_layout_column_type.code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_column_type', 'name'))then
        COMMENT ON COLUMN public.core_layout_column_type.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'qryfilterid'))then
        COMMENT ON COLUMN public.core_qry_filter.qryfilterid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'qryid'))then
        COMMENT ON COLUMN public.core_qry_filter.qryid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'qryoperationid'))then
        COMMENT ON COLUMN public.core_qry_filter.qryoperationid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'kindelementid'))then
        COMMENT ON COLUMN public.core_qry_filter.kindelementid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'condition'))then
        COMMENT ON COLUMN public.core_qry_filter.condition IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'andtrueorfalse'))then
        COMMENT ON COLUMN public.core_qry_filter.andtrueorfalse IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'qryposition'))then
        COMMENT ON COLUMN public.core_qry_filter.qryposition IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'value'))then
        COMMENT ON COLUMN public.core_qry_filter.value IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'byref'))then
        COMMENT ON COLUMN public.core_qry_filter.byref IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'bracketsfirst'))then
        COMMENT ON COLUMN public.core_qry_filter.bracketsfirst IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'bracketsclose'))then
        COMMENT ON COLUMN public.core_qry_filter.bracketsclose IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'referenceid'))then
        COMMENT ON COLUMN public.core_qry_filter.referenceid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'specialregisterid'))then
        COMMENT ON COLUMN public.core_qry_filter.specialregisterid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_filter', 'specialattributetype'))then
        COMMENT ON COLUMN public.core_qry_filter.specialattributetype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'id'))then
        COMMENT ON COLUMN public.core_layout_details.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'layoutid'))then
        COMMENT ON COLUMN public.core_layout_details.layoutid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'detailtype'))then
        COMMENT ON COLUMN public.core_layout_details.detailtype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'ordinal'))then
        COMMENT ON COLUMN public.core_layout_details.ordinal IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'attributeid'))then
        COMMENT ON COLUMN public.core_layout_details.attributeid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'sortbyattribute'))then
        COMMENT ON COLUMN public.core_layout_details.sortbyattribute IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'referenceid'))then
        COMMENT ON COLUMN public.core_layout_details.referenceid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'headertext'))then
        COMMENT ON COLUMN public.core_layout_details.headertext IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'headerwidth'))then
        COMMENT ON COLUMN public.core_layout_details.headerwidth IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'visible'))then
        COMMENT ON COLUMN public.core_layout_details.visible IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'format'))then
        COMMENT ON COLUMN public.core_layout_details.format IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'datatype'))then
        COMMENT ON COLUMN public.core_layout_details.datatype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'expression'))then
        COMMENT ON COLUMN public.core_layout_details.expression IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'sqlexpression'))then
        COMMENT ON COLUMN public.core_layout_details.sqlexpression IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'totaltext'))then
        COMMENT ON COLUMN public.core_layout_details.totaltext IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'totaltype'))then
        COMMENT ON COLUMN public.core_layout_details.totaltype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'style'))then
        COMMENT ON COLUMN public.core_layout_details.style IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'enablestyle'))then
        COMMENT ON COLUMN public.core_layout_details.enablestyle IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'textalign'))then
        COMMENT ON COLUMN public.core_layout_details.textalign IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_details', 'qscolumn'))then
        COMMENT ON COLUMN public.core_layout_details.qscolumn IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_operation', 'qryoperationid'))then
        COMMENT ON COLUMN public.core_qry_operation.qryoperationid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_operation', 'description'))then
        COMMENT ON COLUMN public.core_qry_operation.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry_operation', 'sqlstatement'))then
        COMMENT ON COLUMN public.core_qry_operation.sqlstatement IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_status', 'id'))then
        COMMENT ON COLUMN public.insur_flat_status.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_status', 'name'))then
        COMMENT ON COLUMN public.insur_flat_status.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_status', 'short_name'))then
        COMMENT ON COLUMN public.insur_flat_status.short_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_status', 'code'))then
        COMMENT ON COLUMN public.insur_flat_status.code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_file', 'id'))then
        COMMENT ON COLUMN public.core_attachment_file.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_file', 'attachment_id'))then
        COMMENT ON COLUMN public.core_attachment_file.attachment_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_file', 'filename'))then
        COMMENT ON COLUMN public.core_attachment_file.filename IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_file', 'mimetype'))then
        COMMENT ON COLUMN public.core_attachment_file.mimetype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_file', 'page'))then
        COMMENT ON COLUMN public.core_attachment_file.page IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_file', 'is_main'))then
        COMMENT ON COLUMN public.core_attachment_file.is_main IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_o', 'id'))then
        COMMENT ON COLUMN public.insur_fsp_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_o', 'info'))then
        COMMENT ON COLUMN public.insur_fsp_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_o', 'deleted'))then
        COMMENT ON COLUMN public.insur_fsp_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_o', 'uid'))then
        COMMENT ON COLUMN public.insur_fsp_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_o', 'enddatechange'))then
        COMMENT ON COLUMN public.insur_fsp_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_type', 'id'))then
        COMMENT ON COLUMN public.insur_flat_type.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_type', 'code'))then
        COMMENT ON COLUMN public.insur_flat_type.code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_type', 'name'))then
        COMMENT ON COLUMN public.insur_flat_type.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_type', 'short_name'))then
        COMMENT ON COLUMN public.insur_flat_type.short_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_build_bti', 'emp_id'))then
        COMMENT ON COLUMN public.insur_link_build_bti.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_build_bti', 'id_bti_fsks'))then
        COMMENT ON COLUMN public.insur_link_build_bti.id_bti_fsks IS 'Ссылка на запись в Реестре зданий БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_build_bti', 'id_insur_build'))then
        COMMENT ON COLUMN public.insur_link_build_bti.id_insur_build IS 'Ссылка на запись в Реестре объектов страхования МКД INSUR_BUILDING';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_build_bti', 'flag_dubl_unom'))then
        COMMENT ON COLUMN public.insur_link_build_bti.flag_dubl_unom IS '1/0 ( Дублируется UNOM = да, нет)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_flat_egrn', 'emp_id'))then
        COMMENT ON COLUMN public.insur_link_flat_egrn.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_flat_egrn', 'id_insur_flat'))then
        COMMENT ON COLUMN public.insur_link_flat_egrn.id_insur_flat IS 'Ссылка на запись в Реестре зданий БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_flat_egrn', 'id_egrn_flat'))then
        COMMENT ON COLUMN public.insur_link_flat_egrn.id_egrn_flat IS 'Ссылка на запись в Реестре объектов страхования МКД INSUR_BUILDING';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_o', 'id'))then
        COMMENT ON COLUMN public.bti_floor_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_o', 'info'))then
        COMMENT ON COLUMN public.bti_floor_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_o', 'deleted'))then
        COMMENT ON COLUMN public.bti_floor_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_o', 'uid'))then
        COMMENT ON COLUMN public.bti_floor_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_o', 'enddatechange'))then
        COMMENT ON COLUMN public.bti_floor_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_o', 'id'))then
        COMMENT ON COLUMN public.bti_addrlink_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_o', 'info'))then
        COMMENT ON COLUMN public.bti_addrlink_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_o', 'deleted'))then
        COMMENT ON COLUMN public.bti_addrlink_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_o', 'uid'))then
        COMMENT ON COLUMN public.bti_addrlink_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_o', 'enddatechange'))then
        COMMENT ON COLUMN public.bti_addrlink_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'emp_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.emp_id IS 'Инд.номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'actual'))then
        COMMENT ON COLUMN public.bti_addrlink_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'status'))then
        COMMENT ON COLUMN public.bti_addrlink_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'dept_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 's_'))then
        COMMENT ON COLUMN public.bti_addrlink_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'po_'))then
        COMMENT ON COLUMN public.bti_addrlink_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'address_state'))then
        COMMENT ON COLUMN public.bti_addrlink_q.address_state IS 'Состояние адреса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'address_state_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.address_state_id IS 'Состояние адреса - С.А.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'address_state_name'))then
        COMMENT ON COLUMN public.bti_addrlink_q.address_state_name IS 'Состояние адреса - С.А.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'address_state_date'))then
        COMMENT ON COLUMN public.bti_addrlink_q.address_state_date IS 'Дата состояния - С.А.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'address_status_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.address_status_id IS 'Статус адреса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'address_status_name'))then
        COMMENT ON COLUMN public.bti_addrlink_q.address_status_name IS 'Статус адреса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'individual_number'))then
        COMMENT ON COLUMN public.bti_addrlink_q.individual_number IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document'))then
        COMMENT ON COLUMN public.bti_addrlink_q.ground_document IS 'Документ основание (Д.О.)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_type_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.ground_document_type_id IS 'Тип документа - Д.О.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_type_name'))then
        COMMENT ON COLUMN public.bti_addrlink_q.ground_document_type_name IS 'Тип документа - Д.О.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_number'))then
        COMMENT ON COLUMN public.bti_addrlink_q.ground_document_number IS 'Номер документа - Д.О.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_date_issue'))then
        COMMENT ON COLUMN public.bti_addrlink_q.ground_document_date_issue IS 'Дата выдачи документа - Д.О.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_content_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.ground_document_content_id IS 'Содержание документа - Д.О.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_content_name'))then
        COMMENT ON COLUMN public.bti_addrlink_q.ground_document_content_name IS 'Содержание документа - Д.О.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'unad'))then
        COMMENT ON COLUMN public.bti_addrlink_q.unad IS 'UNAD';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'address_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.address_id IS 'Адрес';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'address_name'))then
        COMMENT ON COLUMN public.bti_addrlink_q.address_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'building_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.building_id IS 'ID объекта';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'building_name'))then
        COMMENT ON COLUMN public.bti_addrlink_q.building_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_number'))then
        COMMENT ON COLUMN public.bti_addrlink_q.reg_number IS 'Регистрационный номер в адресном реестре';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_date'))then
        COMMENT ON COLUMN public.bti_addrlink_q.reg_date IS 'Дата регистрации адреса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_type_name'))then
        COMMENT ON COLUMN public.bti_addrlink_q.reg_doc_type_name IS 'Тип документа-основания (рег)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_type_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.reg_doc_type_id IS 'Тип документа-основания (рег)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_number'))then
        COMMENT ON COLUMN public.bti_addrlink_q.reg_doc_number IS 'Номер документа-основания (рег)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_date'))then
        COMMENT ON COLUMN public.bti_addrlink_q.reg_doc_date IS 'Дата документа-основания (рег)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_content_name'))then
        COMMENT ON COLUMN public.bti_addrlink_q.reg_doc_content_name IS 'Содержание документа-основания (рег)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_content_id'))then
        COMMENT ON COLUMN public.bti_addrlink_q.reg_doc_content_id IS 'Содержание документа-основания (рег)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'register_object_number'))then
        COMMENT ON COLUMN public.bti_addrlink_q.register_object_number IS 'Номер реестра объекта';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'comments'))then
        COMMENT ON COLUMN public.bti_addrlink_q.comments IS 'Комментарий ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'id_source'))then
        COMMENT ON COLUMN public.bti_addrlink_q.id_source IS 'Источник данных';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_addrlink_q', 'text_source'))then
        COMMENT ON COLUMN public.bti_addrlink_q.text_source IS 'Источник данных';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_o', 'id'))then
        COMMENT ON COLUMN public.bti_address_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_o', 'info'))then
        COMMENT ON COLUMN public.bti_address_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_o', 'deleted'))then
        COMMENT ON COLUMN public.bti_address_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_o', 'uid'))then
        COMMENT ON COLUMN public.bti_address_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_o', 'enddatechange'))then
        COMMENT ON COLUMN public.bti_address_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'id'))then
        COMMENT ON COLUMN public.bti_address_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'emp_id'))then
        COMMENT ON COLUMN public.bti_address_q.emp_id IS 'Инд.номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'actual'))then
        COMMENT ON COLUMN public.bti_address_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'status'))then
        COMMENT ON COLUMN public.bti_address_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'dept_id'))then
        COMMENT ON COLUMN public.bti_address_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 's_'))then
        COMMENT ON COLUMN public.bti_address_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'po_'))then
        COMMENT ON COLUMN public.bti_address_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'individual_number'))then
        COMMENT ON COLUMN public.bti_address_q.individual_number IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'full_name'))then
        COMMENT ON COLUMN public.bti_address_q.full_name IS 'Полное наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'short_name'))then
        COMMENT ON COLUMN public.bti_address_q.short_name IS 'Краткое наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'name_for_sort'))then
        COMMENT ON COLUMN public.bti_address_q.name_for_sort IS 'Наименование для сортировки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'main_name'))then
        COMMENT ON COLUMN public.bti_address_q.main_name IS 'Основное наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'main_name_print'))then
        COMMENT ON COLUMN public.bti_address_q.main_name_print IS 'Основное наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'full_name_print'))then
        COMMENT ON COLUMN public.bti_address_q.full_name_print IS 'Полное наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'id_in_ds'))then
        COMMENT ON COLUMN public.bti_address_q.id_in_ds IS 'ID в источнике данных';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'data_source_id'))then
        COMMENT ON COLUMN public.bti_address_q.data_source_id IS 'Источник данных';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'data_source_name'))then
        COMMENT ON COLUMN public.bti_address_q.data_source_name IS 'Источник данных';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'subject_rf_id'))then
        COMMENT ON COLUMN public.bti_address_q.subject_rf_id IS 'Субъект РФ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'subject_rf_name'))then
        COMMENT ON COLUMN public.bti_address_q.subject_rf_name IS 'Субъект РФ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'okrug_id'))then
        COMMENT ON COLUMN public.bti_address_q.okrug_id IS 'Округ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'district_id'))then
        COMMENT ON COLUMN public.bti_address_q.district_id IS 'Район';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'settlement_id'))then
        COMMENT ON COLUMN public.bti_address_q.settlement_id IS 'Городское/сельское поселение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'settlement_name'))then
        COMMENT ON COLUMN public.bti_address_q.settlement_name IS 'Городское/сельское поселение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'town_id'))then
        COMMENT ON COLUMN public.bti_address_q.town_id IS 'Город/НП';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'town_name'))then
        COMMENT ON COLUMN public.bti_address_q.town_name IS 'Город/НП';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'pse_id'))then
        COMMENT ON COLUMN public.bti_address_q.pse_id IS 'Элемент планир. структуры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'pse_name'))then
        COMMENT ON COLUMN public.bti_address_q.pse_name IS 'Элемент планир. структуры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'street_id'))then
        COMMENT ON COLUMN public.bti_address_q.street_id IS 'Улица';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'street_name'))then
        COMMENT ON COLUMN public.bti_address_q.street_name IS 'Улица';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'property_type_id'))then
        COMMENT ON COLUMN public.bti_address_q.property_type_id IS 'Тип владения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'property_type_name'))then
        COMMENT ON COLUMN public.bti_address_q.property_type_name IS 'Тип владения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'plot_number'))then
        COMMENT ON COLUMN public.bti_address_q.plot_number IS 'Номер участка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'house_number'))then
        COMMENT ON COLUMN public.bti_address_q.house_number IS 'Дом';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'korpus_number'))then
        COMMENT ON COLUMN public.bti_address_q.korpus_number IS 'Корпус';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'structure_type_id'))then
        COMMENT ON COLUMN public.bti_address_q.structure_type_id IS 'Тип сооружения (адр)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'structure_type_name'))then
        COMMENT ON COLUMN public.bti_address_q.structure_type_name IS 'Тип сооружения (адр)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'structure_number'))then
        COMMENT ON COLUMN public.bti_address_q.structure_number IS 'Строение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'letter_number'))then
        COMMENT ON COLUMN public.bti_address_q.letter_number IS 'Литера';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'location_desc'))then
        COMMENT ON COLUMN public.bti_address_q.location_desc IS 'Описание местоположения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'okato_code'))then
        COMMENT ON COLUMN public.bti_address_q.okato_code IS 'Код ОКАТО';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'kladr_code'))then
        COMMENT ON COLUMN public.bti_address_q.kladr_code IS 'Код КЛАДР';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'index_postal'))then
        COMMENT ON COLUMN public.bti_address_q.index_postal IS 'Почтовый индекс';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'type_corpus'))then
        COMMENT ON COLUMN public.bti_address_q.type_corpus IS 'Тип корпуса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'type_corpus_id'))then
        COMMENT ON COLUMN public.bti_address_q.type_corpus_id IS 'Тип корпуса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'code_nsi'))then
        COMMENT ON COLUMN public.bti_address_q.code_nsi IS 'ИД адреса в НСИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'other'))then
        COMMENT ON COLUMN public.bti_address_q.other IS 'Иное';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'oktmo'))then
        COMMENT ON COLUMN public.bti_address_q.oktmo IS 'Код ОКТМО';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'oktmo_id'))then
        COMMENT ON COLUMN public.bti_address_q.oktmo_id IS 'Код ОКТМО';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'full_mix_address'))then
        COMMENT ON COLUMN public.bti_address_q.full_mix_address IS 'Адрес для полнотекстового поиска';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'address_or_location'))then
        COMMENT ON COLUMN public.bti_address_q.address_or_location IS 'Признак адреса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_address_q', 'code_fias'))then
        COMMENT ON COLUMN public.bti_address_q.code_fias IS 'Код ФИАС';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'emp_id'))then
        COMMENT ON COLUMN public.bti_rooms.emp_id IS 'Инд.номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'dept_id'))then
        COMMENT ON COLUMN public.bti_rooms.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'premise_id'))then
        COMMENT ON COLUMN public.bti_rooms.premise_id IS 'Помещение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'purpose_id'))then
        COMMENT ON COLUMN public.bti_rooms.purpose_id IS 'Назначение комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'purpose_name'))then
        COMMENT ON COLUMN public.bti_rooms.purpose_name IS 'Назначение комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'special_purpose_id'))then
        COMMENT ON COLUMN public.bti_rooms.special_purpose_id IS 'Специальное назначение комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'special_purpose_name'))then
        COMMENT ON COLUMN public.bti_rooms.special_purpose_name IS 'Специальное назначение комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'area_kind_id'))then
        COMMENT ON COLUMN public.bti_rooms.area_kind_id IS 'Вид площади комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'area_kind_name'))then
        COMMENT ON COLUMN public.bti_rooms.area_kind_name IS 'Вид площади комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'area_type_id'))then
        COMMENT ON COLUMN public.bti_rooms.area_type_id IS 'Тип площади комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'area_type_name'))then
        COMMENT ON COLUMN public.bti_rooms.area_type_name IS 'Тип площади комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'height'))then
        COMMENT ON COLUMN public.bti_rooms.height IS 'Высота';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'survey_date'))then
        COMMENT ON COLUMN public.bti_rooms.survey_date IS 'Дата обследования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'area_calculation_formula'))then
        COMMENT ON COLUMN public.bti_rooms.area_calculation_formula IS 'Формула подсчета площади';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'area'))then
        COMMENT ON COLUMN public.bti_rooms.area IS 'Площадь комнаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'number_pp'))then
        COMMENT ON COLUMN public.bti_rooms.number_pp IS 'Номер пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'floor_id'))then
        COMMENT ON COLUMN public.bti_rooms.floor_id IS 'Этаж';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'plan_number'))then
        COMMENT ON COLUMN public.bti_rooms.plan_number IS 'Номер на плане';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'document_number'))then
        COMMENT ON COLUMN public.bti_rooms.document_number IS 'Номер для документов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'reduction_ratio_id'))then
        COMMENT ON COLUMN public.bti_rooms.reduction_ratio_id IS 'Понижающий коэффициент';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'reduction_ratio_name'))then
        COMMENT ON COLUMN public.bti_rooms.reduction_ratio_name IS 'Понижающий коэффициент';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'guid_pp'))then
        COMMENT ON COLUMN public.bti_rooms.guid_pp IS 'GUID_пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'area_pp'))then
        COMMENT ON COLUMN public.bti_rooms.area_pp IS 'Площадь_пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'number_room_pp'))then
        COMMENT ON COLUMN public.bti_rooms.number_room_pp IS 'Номер_комнаты_пп ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'is_refitted_wo_permission'))then
        COMMENT ON COLUMN public.bti_rooms.is_refitted_wo_permission IS 'Переоборудовано без разрешения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'is_common_prorerty_appartment'))then
        COMMENT ON COLUMN public.bti_rooms.is_common_prorerty_appartment IS 'Общее имущество многоквартирного дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'is_common_prorerty_condominium'))then
        COMMENT ON COLUMN public.bti_rooms.is_common_prorerty_condominium IS 'Общее имущество кондоминиума';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'kadastr_number'))then
        COMMENT ON COLUMN public.bti_rooms.kadastr_number IS 'Кадастровый номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'id_in_source'))then
        COMMENT ON COLUMN public.bti_rooms.id_in_source IS 'ID объекта в системе источнике';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_rooms', 'update_date'))then
        COMMENT ON COLUMN public.bti_rooms.update_date IS 'Дата обновления';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_base_tariff', 'emp_id'))then
        COMMENT ON COLUMN public.insur_base_tariff.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_base_tariff', 'name'))then
        COMMENT ON COLUMN public.insur_base_tariff.name IS 'Наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_base_tariff', 'value'))then
        COMMENT ON COLUMN public.insur_base_tariff.value IS 'Значение, в процентах';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_scan_doc', 'emp_id'))then
        COMMENT ON COLUMN public.insur_scan_doc.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_scan_doc', 'doc_date'))then
        COMMENT ON COLUMN public.insur_scan_doc.doc_date IS 'Дата';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_scan_doc', 'fio_scan'))then
        COMMENT ON COLUMN public.insur_scan_doc.fio_scan IS 'ФИО сотрудника загрузившего документ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_scan_doc', 'doc_number_id'))then
        COMMENT ON COLUMN public.insur_scan_doc.doc_number_id IS 'Номер дела (Ссылка на реестр INSUR_DAMAGE)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'id'))then
        COMMENT ON COLUMN public.insur_damage_assessment_radio_tv_phone.id IS 'Уникальный номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'building_type'))then
        COMMENT ON COLUMN public.insur_damage_assessment_radio_tv_phone.building_type IS 'Тип здания';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'wires'))then
        COMMENT ON COLUMN public.insur_damage_assessment_radio_tv_phone.wires IS 'Провода';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'input_device'))then
        COMMENT ON COLUMN public.insur_damage_assessment_radio_tv_phone.input_device IS 'Ввод. устройтсва';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'construction_type'))then
        COMMENT ON COLUMN public.insur_damage_assessment_radio_tv_phone.construction_type IS 'Тип конструкции';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'district_id'))then
        COMMENT ON COLUMN public.ref_addr_district.district_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'okrug_id'))then
        COMMENT ON COLUMN public.ref_addr_district.okrug_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'subject_rf_id'))then
        COMMENT ON COLUMN public.ref_addr_district.subject_rf_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'full_name'))then
        COMMENT ON COLUMN public.ref_addr_district.full_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'short_name'))then
        COMMENT ON COLUMN public.ref_addr_district.short_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'name_for_sort'))then
        COMMENT ON COLUMN public.ref_addr_district.name_for_sort IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'steks_code'))then
        COMMENT ON COLUMN public.ref_addr_district.steks_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'omk_code'))then
        COMMENT ON COLUMN public.ref_addr_district.omk_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'type_ref'))then
        COMMENT ON COLUMN public.ref_addr_district.type_ref IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'name'))then
        COMMENT ON COLUMN public.ref_addr_district.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_district', 'code_givc'))then
        COMMENT ON COLUMN public.ref_addr_district.code_givc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'street_id'))then
        COMMENT ON COLUMN public.ref_addr_street.street_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'pse_id'))then
        COMMENT ON COLUMN public.ref_addr_street.pse_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'town_id'))then
        COMMENT ON COLUMN public.ref_addr_street.town_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'subject_rf_id'))then
        COMMENT ON COLUMN public.ref_addr_street.subject_rf_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'full_name'))then
        COMMENT ON COLUMN public.ref_addr_street.full_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'short_name'))then
        COMMENT ON COLUMN public.ref_addr_street.short_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'name_for_sort'))then
        COMMENT ON COLUMN public.ref_addr_street.name_for_sort IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'steks_code'))then
        COMMENT ON COLUMN public.ref_addr_street.steks_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'omk_code'))then
        COMMENT ON COLUMN public.ref_addr_street.omk_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'type_ref'))then
        COMMENT ON COLUMN public.ref_addr_street.type_ref IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'kladr_code'))then
        COMMENT ON COLUMN public.ref_addr_street.kladr_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'name'))then
        COMMENT ON COLUMN public.ref_addr_street.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_street', 'code_givc'))then
        COMMENT ON COLUMN public.ref_addr_street.code_givc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'bti_id'))then
        COMMENT ON COLUMN public.bti_import_log.bti_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'num_cadnum'))then
        COMMENT ON COLUMN public.bti_import_log.num_cadnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'unom'))then
        COMMENT ON COLUMN public.bti_import_log.unom IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'is_new'))then
        COMMENT ON COLUMN public.bti_import_log.is_new IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'alt_building_id'))then
        COMMENT ON COLUMN public.bti_import_log.alt_building_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'dateedit'))then
        COMMENT ON COLUMN public.bti_import_log.dateedit IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'is_error'))then
        COMMENT ON COLUMN public.bti_import_log.is_error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'message'))then
        COMMENT ON COLUMN public.bti_import_log.message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'error_id'))then
        COMMENT ON COLUMN public.bti_import_log.error_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'task_id'))then
        COMMENT ON COLUMN public.bti_import_log.task_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_import_log', 'insert_date'))then
        COMMENT ON COLUMN public.bti_import_log.insert_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file_package', 'id'))then
        COMMENT ON COLUMN public.insur_input_file_package.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file_package', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_input_file_package.period_reg_date IS 'Период';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file_package', 'okrug_id'))then
        COMMENT ON COLUMN public.insur_input_file_package.okrug_id IS 'Идентификатор округа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file_package', 'count_district'))then
        COMMENT ON COLUMN public.insur_input_file_package.count_district IS 'Общее количество районов в округе';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_part_compensation', 'emp_id'))then
        COMMENT ON COLUMN public.insur_part_compensation.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_part_compensation', 'date_begin'))then
        COMMENT ON COLUMN public.insur_part_compensation.date_begin IS 'Дата начала действия';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_part_compensation', 'type'))then
        COMMENT ON COLUMN public.insur_part_compensation.type IS 'Тип отвественности СК и города';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_part_compensation', 'ic_value'))then
        COMMENT ON COLUMN public.insur_part_compensation.ic_value IS 'Доля ответственности СК, %';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_part_compensation', 'city_value'))then
        COMMENT ON COLUMN public.insur_part_compensation.city_value IS 'Доля ответственности города, %';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'id'))then
        COMMENT ON COLUMN public.insur_share_responsibility_ic_city.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'date_begin'))then
        COMMENT ON COLUMN public.insur_share_responsibility_ic_city.date_begin IS 'Дата начала  действия';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'type'))then
        COMMENT ON COLUMN public.insur_share_responsibility_ic_city.type IS 'Тип';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'ic_share'))then
        COMMENT ON COLUMN public.insur_share_responsibility_ic_city.ic_share IS 'Доля СК,%';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'city_share'))then
        COMMENT ON COLUMN public.insur_share_responsibility_ic_city.city_share IS 'Доля города,%';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'note'))then
        COMMENT ON COLUMN public.insur_share_responsibility_ic_city.note IS 'Примечание';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'emp_id'))then
        COMMENT ON COLUMN public.insur_dop_all_property.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'contract_id'))then
        COMMENT ON COLUMN public.insur_dop_all_property.contract_id IS 'Ссылка на INSUR_ALL_PROPERTY';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'kod'))then
        COMMENT ON COLUMN public.insur_dop_all_property.kod IS 'Код страховой организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'unom'))then
        COMMENT ON COLUMN public.insur_dop_all_property.unom IS 'Уникальный номер строения ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'ndog'))then
        COMMENT ON COLUMN public.insur_dop_all_property.ndog IS 'Уникальный номер договора страхования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'ndogdat'))then
        COMMENT ON COLUMN public.insur_dop_all_property.ndogdat IS 'Дата начала действия договора страхования ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'ndops'))then
        COMMENT ON COLUMN public.insur_dop_all_property.ndops IS 'Номер дополнительного соглашения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'st1_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.st1_new IS 'Страховая стоимость конструктивных элементов и помещений общего пользования (новое значение по доп соглашению)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'st2_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.st2_new IS 'Страховая стоимость внеквартирного инженерного оборудования (новое значение по доп соглашению)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'st3_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.st3_new IS 'Страховая стоимость лифтового оборудования(новое значение по доп соглашению)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'ss1_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.ss1_new IS 'Страховая сумма конструктивных элементов и помещений общего пользования(новое значение по доп соглашению)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'ss2_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.ss2_new IS 'Страховая сумма внеквартирного инженерного оборудования(новое значение по доп соглашению)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'ss3_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.ss3_new IS 'Страховая сумма лифтового оборудования(новое значение по доп соглашению)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'part_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.part_new IS 'Доля ответственности страховой организации в возмещении ущерба(новое значение по доп соглашению)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'part_city_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.part_city_new IS 'Доля города Москвы в праве на общее имущество(новое значение по доп соглашению)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'paysign_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.paysign_new IS 'Признак рассрочки платежа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'ras_pripay_new'))then
        COMMENT ON COLUMN public.insur_dop_all_property.ras_pripay_new IS '	Рассчитанный размер страховой премии';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_dop_all_property', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_dop_all_property.link_id_file IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_insurance_organization', 'id'))then
        COMMENT ON COLUMN public.insur_insurance_organization.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_insurance_organization', 'full_name'))then
        COMMENT ON COLUMN public.insur_insurance_organization.full_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_insurance_organization', 'short_name'))then
        COMMENT ON COLUMN public.insur_insurance_organization.short_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_insurance_organization', 'code'))then
        COMMENT ON COLUMN public.insur_insurance_organization.code IS 'Код страховой компании';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'emp_id'))then
        COMMENT ON COLUMN public.insur_policy_svd.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'type_doc_code'))then
        COMMENT ON COLUMN public.insur_policy_svd.type_doc_code IS 'Тип документа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'fsp_id'))then
        COMMENT ON COLUMN public.insur_policy_svd.fsp_id IS 'Ссылка на ФСП INSUR_FSP_Q.EMP_ID';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_policy_svd.link_id_file IS 'Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'link_id_file_end'))then
        COMMENT ON COLUMN public.insur_policy_svd.link_id_file_end IS 'Ссылка на файл загрузки, используется при загрузке данных из файла POLYC_D.DBF';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'insurance_organization_id'))then
        COMMENT ON COLUMN public.insur_policy_svd.insurance_organization_id IS 'Код страховой организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'okrug_id'))then
        COMMENT ON COLUMN public.insur_policy_svd.okrug_id IS 'Код округа ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'district_id'))then
        COMMENT ON COLUMN public.insur_policy_svd.district_id IS 'Код района ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'org_type'))then
        COMMENT ON COLUMN public.insur_policy_svd.org_type IS 'Код формы объединения собственников (6 – ЖСК, ЖК,  7 – ТСЖ, 8 – БО (без объединения собственников), 0 – нет данных)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'org_id'))then
        COMMENT ON COLUMN public.insur_policy_svd.org_id IS 'Код управляющей организации (УО)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'plat_id'))then
        COMMENT ON COLUMN public.insur_policy_svd.plat_id IS 'Код вида организации, начисляющей страховые взносы – по справочнику «Виды организаций, начисляющих страховые взносы»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'unom'))then
        COMMENT ON COLUMN public.insur_policy_svd.unom IS 'Уникальный номер строения ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'unkva'))then
        COMMENT ON COLUMN public.insur_policy_svd.unkva IS 'Уникальный номер квартиры  в доме';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'nom'))then
        COMMENT ON COLUMN public.insur_policy_svd.nom IS 'Номер квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'nomi'))then
        COMMENT ON COLUMN public.insur_policy_svd.nomi IS 'Индекс квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'kvnom'))then
        COMMENT ON COLUMN public.insur_policy_svd.kvnom IS 'Номер квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'kolgp'))then
        COMMENT ON COLUMN public.insur_policy_svd.kolgp IS 'Количество жилых помещений в квартире';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'flatstatus'))then
        COMMENT ON COLUMN public.insur_policy_svd.flatstatus IS 'Код статуса жилого помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'fopl'))then
        COMMENT ON COLUMN public.insur_policy_svd.fopl IS 'Общая площадь квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'opl'))then
        COMMENT ON COLUMN public.insur_policy_svd.opl IS 'Подлежащая страхованию площадь жилого помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'kodpl'))then
        COMMENT ON COLUMN public.insur_policy_svd.kodpl IS 'Код плательщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'ls'))then
        COMMENT ON COLUMN public.insur_policy_svd.ls IS 'Лицевой счет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'npol'))then
        COMMENT ON COLUMN public.insur_policy_svd.npol IS 'Уникальный номер страхового полиса/уникальный номер страхового свидетельства';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'dat'))then
        COMMENT ON COLUMN public.insur_policy_svd.dat IS 'Дата начала действия договора ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'vznos'))then
        COMMENT ON COLUMN public.insur_policy_svd.vznos IS 'Страховой взнос за 1 кв. м. в месяц';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'pralt_code'))then
        COMMENT ON COLUMN public.insur_policy_svd.pralt_code IS 'Признак условия страхования (0-основные условия, 1-алттернативные условия)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'pr'))then
        COMMENT ON COLUMN public.insur_policy_svd.pr IS 'Признак договора (1– страховщик несет ответственность, 2 – страховщик не несет ответственности)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'ss'))then
        COMMENT ON COLUMN public.insur_policy_svd.ss IS 'Страховая сумма';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'kolds'))then
        COMMENT ON COLUMN public.insur_policy_svd.kolds IS 'Количество свидетельств в квартире';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'soplvz'))then
        COMMENT ON COLUMN public.insur_policy_svd.soplvz IS 'Сумма страхового взноса, уплаченного в отчетном месяце';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'dat_end'))then
        COMMENT ON COLUMN public.insur_policy_svd.dat_end IS 'Дата досрочного погашения договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'type_doc'))then
        COMMENT ON COLUMN public.insur_policy_svd.type_doc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'type_prop'))then
        COMMENT ON COLUMN public.insur_policy_svd.type_prop IS 'Тип собственности';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'type_prop_code'))then
        COMMENT ON COLUMN public.insur_policy_svd.type_prop_code IS 'Тип собственности (code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_policy_svd', 'pralt'))then
        COMMENT ON COLUMN public.insur_policy_svd.pralt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'emp_id'))then
        COMMENT ON COLUMN public.insur_input_nach.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_input_nach.link_id_file IS 'Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'fsp_id'))then
        COMMENT ON COLUMN public.insur_input_nach.fsp_id IS 'Ссылка на реестр ФСП INSUR_FSP';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'type_source'))then
        COMMENT ON COLUMN public.insur_input_nach.type_source IS 'Источник  (1-МФЦ, 2-СК, 3-Банк, 4-ГБУ)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'status_identif'))then
        COMMENT ON COLUMN public.insur_input_nach.status_identif IS 'Статус идентификации записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_input_nach.period_reg_date IS 'Период учета данных в Системе';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'period'))then
        COMMENT ON COLUMN public.insur_input_nach.period IS 'Период, за который произведена оплата';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'district_id'))then
        COMMENT ON COLUMN public.insur_input_nach.district_id IS 'Идентификатор района';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'kod'))then
        COMMENT ON COLUMN public.insur_input_nach.kod IS 'Код страховой организации ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'unom'))then
        COMMENT ON COLUMN public.insur_input_nach.unom IS 'Уникальный номер дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'adres_t'))then
        COMMENT ON COLUMN public.insur_input_nach.adres_t IS 'Адрес дома ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'unkva'))then
        COMMENT ON COLUMN public.insur_input_nach.unkva IS 'Уникальный номер квартиры в доме';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'nomi'))then
        COMMENT ON COLUMN public.insur_input_nach.nomi IS 'Индекс квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'nom'))then
        COMMENT ON COLUMN public.insur_input_nach.nom IS 'Номер квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'kvnom'))then
        COMMENT ON COLUMN public.insur_input_nach.kvnom IS 'Номер квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'flat_status_id'))then
        COMMENT ON COLUMN public.insur_input_nach.flat_status_id IS 'Код статуса жилого помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'flat_type_id'))then
        COMMENT ON COLUMN public.insur_input_nach.flat_type_id IS 'Код типа жилого помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'kolgp'))then
        COMMENT ON COLUMN public.insur_input_nach.kolgp IS 'Количество жилых помещений в квартире';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'fopl'))then
        COMMENT ON COLUMN public.insur_input_nach.fopl IS 'Общая площадь квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'opl'))then
        COMMENT ON COLUMN public.insur_input_nach.opl IS 'Подлежащая страхованию площадь жилого помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'kodpl'))then
        COMMENT ON COLUMN public.insur_input_nach.kodpl IS 'Код плательщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'ls'))then
        COMMENT ON COLUMN public.insur_input_nach.ls IS 'Лицевой счет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'sum_nach'))then
        COMMENT ON COLUMN public.insur_input_nach.sum_nach IS 'Величина начисленного страхового взноса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'flag_unom_no'))then
        COMMENT ON COLUMN public.insur_input_nach.flag_unom_no IS '1/0 (UNOM найден в Адресном списке/ UNOM не найден в Адресном списке)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'fio'))then
        COMMENT ON COLUMN public.insur_input_nach.fio IS 'ФИО плательщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'type_source_code'))then
        COMMENT ON COLUMN public.insur_input_nach.type_source_code IS 'Код источника';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'status_identif_code'))then
        COMMENT ON COLUMN public.insur_input_nach.status_identif_code IS 'Код статуса идентификации записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'load_status'))then
        COMMENT ON COLUMN public.insur_input_nach.load_status IS 'Статус загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'load_status_code'))then
        COMMENT ON COLUMN public.insur_input_nach.load_status_code IS 'Код статуса загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_nach', 'criteria_json'))then
        COMMENT ON COLUMN public.insur_input_nach.criteria_json IS 'Список вхождения по критериям';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_o', 'id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_o', 'info'))then
        COMMENT ON COLUMN public.ehd_build_parcel_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_o', 'deleted'))then
        COMMENT ON COLUMN public.ehd_build_parcel_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_o', 'uid'))then
        COMMENT ON COLUMN public.ehd_build_parcel_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_o', 'enddatechange'))then
        COMMENT ON COLUMN public.ehd_build_parcel_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'id'))then
        COMMENT ON COLUMN public.ehd_location_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'emp_id'))then
        COMMENT ON COLUMN public.ehd_location_q.emp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'actual'))then
        COMMENT ON COLUMN public.ehd_location_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'status'))then
        COMMENT ON COLUMN public.ehd_location_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'dept_id'))then
        COMMENT ON COLUMN public.ehd_location_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 's_'))then
        COMMENT ON COLUMN public.ehd_location_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'po_'))then
        COMMENT ON COLUMN public.ehd_location_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'load_date'))then
        COMMENT ON COLUMN public.ehd_location_q.load_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'id_location_ehd'))then
        COMMENT ON COLUMN public.ehd_location_q.id_location_ehd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'parcel_id'))then
        COMMENT ON COLUMN public.ehd_location_q.parcel_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'person_id'))then
        COMMENT ON COLUMN public.ehd_location_q.person_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'organization_id'))then
        COMMENT ON COLUMN public.ehd_location_q.organization_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'building_parcel_id'))then
        COMMENT ON COLUMN public.ehd_location_q.building_parcel_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'global_id'))then
        COMMENT ON COLUMN public.ehd_location_q.global_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'placed'))then
        COMMENT ON COLUMN public.ehd_location_q.placed IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'in_bounds'))then
        COMMENT ON COLUMN public.ehd_location_q.in_bounds IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'code_okato'))then
        COMMENT ON COLUMN public.ehd_location_q.code_okato IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'code_kladr'))then
        COMMENT ON COLUMN public.ehd_location_q.code_kladr IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'postal_code'))then
        COMMENT ON COLUMN public.ehd_location_q.postal_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'region'))then
        COMMENT ON COLUMN public.ehd_location_q.region IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'district'))then
        COMMENT ON COLUMN public.ehd_location_q.district IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'city'))then
        COMMENT ON COLUMN public.ehd_location_q.city IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'urban_district'))then
        COMMENT ON COLUMN public.ehd_location_q.urban_district IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'soviet_village'))then
        COMMENT ON COLUMN public.ehd_location_q.soviet_village IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'locality'))then
        COMMENT ON COLUMN public.ehd_location_q.locality IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'street'))then
        COMMENT ON COLUMN public.ehd_location_q.street IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'level1'))then
        COMMENT ON COLUMN public.ehd_location_q.level1 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'level2'))then
        COMMENT ON COLUMN public.ehd_location_q.level2 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'level3'))then
        COMMENT ON COLUMN public.ehd_location_q.level3 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'apartment'))then
        COMMENT ON COLUMN public.ehd_location_q.apartment IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'full_address'))then
        COMMENT ON COLUMN public.ehd_location_q.full_address IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'address_total'))then
        COMMENT ON COLUMN public.ehd_location_q.address_total IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_q', 'other'))then
        COMMENT ON COLUMN public.ehd_location_q.other IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_o', 'id'))then
        COMMENT ON COLUMN public.ehd_location_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_o', 'info'))then
        COMMENT ON COLUMN public.ehd_location_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_o', 'deleted'))then
        COMMENT ON COLUMN public.ehd_location_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_o', 'uid'))then
        COMMENT ON COLUMN public.ehd_location_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_location_o', 'enddatechange'))then
        COMMENT ON COLUMN public.ehd_location_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'id'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.id IS 'ИД (80900100)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'code'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.code IS 'Код типа отчета (80900200)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'internal_name'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.internal_name IS 'Внутреннее наименование типа отчета (80900300)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'title'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.title IS 'Наименование отчета (80900400)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'user_id'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.user_id IS 'ИД пользователя, создавшего отчет (80900500)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'object_register_id'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.object_register_id IS 'ИД реестра объекта, для которого создан отчет (80900600)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'object_id'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.object_id IS 'ИД объекта, для которого создан отчет (80900700)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'create_date'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.create_date IS 'Дата создания отчета (80900800)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'report_number'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.report_number IS 'Номер отчета (809000900)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'comments'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.comments IS 'Комментарии к отчету (80901000)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'parameters'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.parameters IS 'Параметры отчета (80901100)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'status'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.status IS 'Статус (80901200)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'end_date'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.end_date IS 'Дата окончания асинхронной выгрузки (80901300)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'result_message'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.result_message IS 'Результат сохранения отчета (80901400)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'is_deleted'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.is_deleted IS 'Признак удаления (80901500)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'file_type'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.file_type IS 'Тип сохраненного файла (80901600)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_reports_savedreport', 'section'))then
        COMMENT ON COLUMN public.fm_reports_savedreport.section IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_podpisant', 'id'))then
        COMMENT ON COLUMN public.fm_podpisant.id IS 'ИД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_podpisant', 'code'))then
        COMMENT ON COLUMN public.fm_podpisant.code IS 'Код';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_podpisant', 'name'))then
        COMMENT ON COLUMN public.fm_podpisant.name IS 'Наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_podpisant', 'post'))then
        COMMENT ON COLUMN public.fm_podpisant.post IS 'Должность';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_podpisant', 'text'))then
        COMMENT ON COLUMN public.fm_podpisant.text IS 'Формулировка "в лице"';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fm_podpisant', 'is_deleted'))then
        COMMENT ON COLUMN public.fm_podpisant.is_deleted IS 'Признак удаления';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_tariff', 'id'))then
        COMMENT ON COLUMN public.insur_tariff.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_tariff', 'date_begin'))then
        COMMENT ON COLUMN public.insur_tariff.date_begin IS 'Дата начала действия тарифа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_tariff', 'value'))then
        COMMENT ON COLUMN public.insur_tariff.value IS 'Размер тарифа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_o', 'id'))then
        COMMENT ON COLUMN public.insur_building_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_o', 'info'))then
        COMMENT ON COLUMN public.insur_building_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_o', 'deleted'))then
        COMMENT ON COLUMN public.insur_building_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_o', 'uid'))then
        COMMENT ON COLUMN public.insur_building_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_o', 'enddatechange'))then
        COMMENT ON COLUMN public.insur_building_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_o', 'id'))then
        COMMENT ON COLUMN public.insur_flat_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_o', 'info'))then
        COMMENT ON COLUMN public.insur_flat_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_o', 'deleted'))then
        COMMENT ON COLUMN public.insur_flat_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_o', 'uid'))then
        COMMENT ON COLUMN public.insur_flat_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_o', 'enddatechange'))then
        COMMENT ON COLUMN public.insur_flat_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_actual_cost_ratio', 'id'))then
        COMMENT ON COLUMN public.insur_actual_cost_ratio.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_actual_cost_ratio', 'date_begin'))then
        COMMENT ON COLUMN public.insur_actual_cost_ratio.date_begin IS 'Дата начала действия';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_actual_cost_ratio', 'value'))then
        COMMENT ON COLUMN public.insur_actual_cost_ratio.value IS 'Значение коэффициента';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'emp_id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.emp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'actual'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'status'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'dept_id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 's_'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'po_'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'load_date'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.load_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'building_parcel_id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.building_parcel_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'global_id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.global_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'name'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'assignation_code'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.assignation_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'area'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.area IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'notes'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.notes IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'assignation_name'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.assignation_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'assignation_name_id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.assignation_name_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'degree_readiness'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.degree_readiness IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'actual_ehd'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.actual_ehd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'update_date_ehd'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.update_date_ehd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'type'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'type_id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.type_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'subbuildings'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.subbuildings IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'object_id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'package_id'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.package_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'actual_on_date'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.actual_on_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'floors'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.floors IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'elements_construct'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.elements_construct IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_build_parcel_q', 'old_number'))then
        COMMENT ON COLUMN public.ehd_build_parcel_q.old_number IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_o', 'id'))then
        COMMENT ON COLUMN public.ehd_right_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_o', 'info'))then
        COMMENT ON COLUMN public.ehd_right_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_o', 'deleted'))then
        COMMENT ON COLUMN public.ehd_right_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_o', 'uid'))then
        COMMENT ON COLUMN public.ehd_right_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_o', 'enddatechange'))then
        COMMENT ON COLUMN public.ehd_right_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_o', 'id'))then
        COMMENT ON COLUMN public.ehd_egrp_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_o', 'info'))then
        COMMENT ON COLUMN public.ehd_egrp_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_o', 'deleted'))then
        COMMENT ON COLUMN public.ehd_egrp_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_o', 'uid'))then
        COMMENT ON COLUMN public.ehd_egrp_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_o', 'enddatechange'))then
        COMMENT ON COLUMN public.ehd_egrp_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'id'))then
        COMMENT ON COLUMN public.ehd_egrp_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'emp_id'))then
        COMMENT ON COLUMN public.ehd_egrp_q.emp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'actual'))then
        COMMENT ON COLUMN public.ehd_egrp_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'status'))then
        COMMENT ON COLUMN public.ehd_egrp_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'dept_id'))then
        COMMENT ON COLUMN public.ehd_egrp_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 's_'))then
        COMMENT ON COLUMN public.ehd_egrp_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'po_'))then
        COMMENT ON COLUMN public.ehd_egrp_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'id_in_ehd_egrp'))then
        COMMENT ON COLUMN public.ehd_egrp_q.id_in_ehd_egrp IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'area'))then
        COMMENT ON COLUMN public.ehd_egrp_q.area IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'global_id'))then
        COMMENT ON COLUMN public.ehd_egrp_q.global_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'objt_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.objt_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'objecttp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.objecttp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'regtp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.regtp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'disttp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.disttp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'citytp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.citytp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'loctp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.loctp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'strtp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.strtp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'level1tp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.level1tp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'level2tp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.level2tp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'level3tp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.level3tp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'aparttp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.aparttp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'purposetp_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.purposetp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'objectst_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.objectst_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'actst_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.actst_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'fakt_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.fakt_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'bydoc_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.bydoc_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'groundcat_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.groundcat_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'purpose'))then
        COMMENT ON COLUMN public.ehd_egrp_q.purpose IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'invnum'))then
        COMMENT ON COLUMN public.ehd_egrp_q.invnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'literbti'))then
        COMMENT ON COLUMN public.ehd_egrp_q.literbti IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_refmark'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_refmark IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_id'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_cdcountry'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_cdcountry IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_cdokato'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_cdokato IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_postcd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_postcd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_dist_name'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_dist_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_dist_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_dist_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_city_name'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_city_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_city_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_city_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_loc_name'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_loc_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_loc_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_loc_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_str_name'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_str_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_str_cd'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_str_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_level1_num'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_level1_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_level2_num'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_level2_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_level3_num'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_level3_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_apart'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_apart IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_other'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_other IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_note'))then
        COMMENT ON COLUMN public.ehd_egrp_q.addr_note IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'num_cadnum'))then
        COMMENT ON COLUMN public.ehd_egrp_q.num_cadnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'num_condnum'))then
        COMMENT ON COLUMN public.ehd_egrp_q.num_condnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'name'))then
        COMMENT ON COLUMN public.ehd_egrp_q.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'floor_gr'))then
        COMMENT ON COLUMN public.ehd_egrp_q.floor_gr IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'floor_und'))then
        COMMENT ON COLUMN public.ehd_egrp_q.floor_und IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'techar_height'))then
        COMMENT ON COLUMN public.ehd_egrp_q.techar_height IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'techar_lenght'))then
        COMMENT ON COLUMN public.ehd_egrp_q.techar_lenght IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'techar_vol'))then
        COMMENT ON COLUMN public.ehd_egrp_q.techar_vol IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'num_floor'))then
        COMMENT ON COLUMN public.ehd_egrp_q.num_floor IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'num_flat'))then
        COMMENT ON COLUMN public.ehd_egrp_q.num_flat IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'regdt'))then
        COMMENT ON COLUMN public.ehd_egrp_q.regdt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'brkdt'))then
        COMMENT ON COLUMN public.ehd_egrp_q.brkdt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'mdfdt'))then
        COMMENT ON COLUMN public.ehd_egrp_q.mdfdt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'updt'))then
        COMMENT ON COLUMN public.ehd_egrp_q.updt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'act_dt'))then
        COMMENT ON COLUMN public.ehd_egrp_q.act_dt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'object_id'))then
        COMMENT ON COLUMN public.ehd_egrp_q.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'update_date'))then
        COMMENT ON COLUMN public.ehd_egrp_q.update_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'actual_id'))then
        COMMENT ON COLUMN public.ehd_egrp_q.actual_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'actual_on_date'))then
        COMMENT ON COLUMN public.ehd_egrp_q.actual_on_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'address_total'))then
        COMMENT ON COLUMN public.ehd_egrp_q.address_total IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'json'))then
        COMMENT ON COLUMN public.ehd_egrp_q.json IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_egrp_q', 'load_date'))then
        COMMENT ON COLUMN public.ehd_egrp_q.load_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'id'))then
        COMMENT ON COLUMN public.ehd_right_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'emp_id'))then
        COMMENT ON COLUMN public.ehd_right_q.emp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'actual'))then
        COMMENT ON COLUMN public.ehd_right_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'status'))then
        COMMENT ON COLUMN public.ehd_right_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'dept_id'))then
        COMMENT ON COLUMN public.ehd_right_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 's_'))then
        COMMENT ON COLUMN public.ehd_right_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'po_'))then
        COMMENT ON COLUMN public.ehd_right_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'egrp_id'))then
        COMMENT ON COLUMN public.ehd_right_q.egrp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'global_id'))then
        COMMENT ON COLUMN public.ehd_right_q.global_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'id_ehd_right'))then
        COMMENT ON COLUMN public.ehd_right_q.id_ehd_right IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'mdfdt'))then
        COMMENT ON COLUMN public.ehd_right_q.mdfdt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'object_id'))then
        COMMENT ON COLUMN public.ehd_right_q.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'reg_close_regdt'))then
        COMMENT ON COLUMN public.ehd_right_q.reg_close_regdt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'reg_close_regnum'))then
        COMMENT ON COLUMN public.ehd_right_q.reg_close_regnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'reg_open_regdt'))then
        COMMENT ON COLUMN public.ehd_right_q.reg_open_regdt IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'reg_open_regnum'))then
        COMMENT ON COLUMN public.ehd_right_q.reg_open_regnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'rightst_cd'))then
        COMMENT ON COLUMN public.ehd_right_q.rightst_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'righttp_cd'))then
        COMMENT ON COLUMN public.ehd_right_q.righttp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'right_key'))then
        COMMENT ON COLUMN public.ehd_right_q.right_key IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'sharecomflat_text'))then
        COMMENT ON COLUMN public.ehd_right_q.sharecomflat_text IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'sharecom_text'))then
        COMMENT ON COLUMN public.ehd_right_q.sharecom_text IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'share_den'))then
        COMMENT ON COLUMN public.ehd_right_q.share_den IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'share_num'))then
        COMMENT ON COLUMN public.ehd_right_q.share_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'share_text'))then
        COMMENT ON COLUMN public.ehd_right_q.share_text IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'tp_name'))then
        COMMENT ON COLUMN public.ehd_right_q.tp_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'sharecomflat_den'))then
        COMMENT ON COLUMN public.ehd_right_q.sharecomflat_den IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'sharecomflat_num'))then
        COMMENT ON COLUMN public.ehd_right_q.sharecomflat_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'sharecom_den'))then
        COMMENT ON COLUMN public.ehd_right_q.sharecom_den IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'sharecom_num'))then
        COMMENT ON COLUMN public.ehd_right_q.sharecom_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'load_date'))then
        COMMENT ON COLUMN public.ehd_right_q.load_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_right_q', 'righttp_cd_code'))then
        COMMENT ON COLUMN public.ehd_right_q.righttp_cd_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'id'))then
        COMMENT ON COLUMN public.ehd_register_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'emp_id'))then
        COMMENT ON COLUMN public.ehd_register_q.emp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'actual'))then
        COMMENT ON COLUMN public.ehd_register_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'status'))then
        COMMENT ON COLUMN public.ehd_register_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'dept_id'))then
        COMMENT ON COLUMN public.ehd_register_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 's_'))then
        COMMENT ON COLUMN public.ehd_register_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'po_'))then
        COMMENT ON COLUMN public.ehd_register_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'load_date'))then
        COMMENT ON COLUMN public.ehd_register_q.load_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'building_parcel_id'))then
        COMMENT ON COLUMN public.ehd_register_q.building_parcel_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'global_id'))then
        COMMENT ON COLUMN public.ehd_register_q.global_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number_parent'))then
        COMMENT ON COLUMN public.ehd_register_q.cadastral_number_parent IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number'))then
        COMMENT ON COLUMN public.ehd_register_q.cadastral_number IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'date_created'))then
        COMMENT ON COLUMN public.ehd_register_q.date_created IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'date_removed'))then
        COMMENT ON COLUMN public.ehd_register_q.date_removed IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'state'))then
        COMMENT ON COLUMN public.ehd_register_q.state IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'state_id'))then
        COMMENT ON COLUMN public.ehd_register_q.state_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'method'))then
        COMMENT ON COLUMN public.ehd_register_q.method IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number_oks'))then
        COMMENT ON COLUMN public.ehd_register_q.cadastral_number_oks IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number_kk'))then
        COMMENT ON COLUMN public.ehd_register_q.cadastral_number_kk IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number_flat'))then
        COMMENT ON COLUMN public.ehd_register_q.cadastral_number_flat IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'totalass'))then
        COMMENT ON COLUMN public.ehd_register_q.totalass IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'assftp1'))then
        COMMENT ON COLUMN public.ehd_register_q.assftp1 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'assftp_cd'))then
        COMMENT ON COLUMN public.ehd_register_q.assftp_cd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'assftp1_code'))then
        COMMENT ON COLUMN public.ehd_register_q.assftp1_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_q', 'assftp_cd_code'))then
        COMMENT ON COLUMN public.ehd_register_q.assftp_cd_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_o', 'id'))then
        COMMENT ON COLUMN public.ehd_register_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_o', 'info'))then
        COMMENT ON COLUMN public.ehd_register_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_o', 'deleted'))then
        COMMENT ON COLUMN public.ehd_register_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_o', 'uid'))then
        COMMENT ON COLUMN public.ehd_register_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_register_o', 'enddatechange'))then
        COMMENT ON COLUMN public.ehd_register_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_estatestatus', 'estatestatusid'))then
        COMMENT ON COLUMN public.fias_estatestatus.estatestatusid IS 'Признак владения. Принимает значение:0 – Не определено,1 – Владение,2 – Дом,3 – Домовладение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_estatestatus', 'estatestatusname'))then
        COMMENT ON COLUMN public.fias_estatestatus.estatestatusname IS 'Наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_estatestatus', 'estatestatusshortname'))then
        COMMENT ON COLUMN public.fias_estatestatus.estatestatusshortname IS 'Краткое наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_structurestatus', 'structurestatusid'))then
        COMMENT ON COLUMN public.fias_structurestatus.structurestatusid IS 'Признак строения. Принимает значение:0 – Не определено,1 – Строение,2 – Сооружение,3 – Литер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_structurestatus', 'structurestatusname'))then
        COMMENT ON COLUMN public.fias_structurestatus.structurestatusname IS 'Наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_structurestatus', 'structurestatusshortname'))then
        COMMENT ON COLUMN public.fias_structurestatus.structurestatusshortname IS 'Краткое наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'id'))then
        COMMENT ON COLUMN public.spd_request_registration.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'app_date'))then
        COMMENT ON COLUMN public.spd_request_registration.app_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'app_name'))then
        COMMENT ON COLUMN public.spd_request_registration.app_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'app_id'))then
        COMMENT ON COLUMN public.spd_request_registration.app_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'app_status'))then
        COMMENT ON COLUMN public.spd_request_registration.app_status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'doc_date'))then
        COMMENT ON COLUMN public.spd_request_registration.doc_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'doc_name'))then
        COMMENT ON COLUMN public.spd_request_registration.doc_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'doc_link'))then
        COMMENT ON COLUMN public.spd_request_registration.doc_link IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'sig_link'))then
        COMMENT ON COLUMN public.spd_request_registration.sig_link IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'app_doc_id'))then
        COMMENT ON COLUMN public.spd_request_registration.app_doc_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'doc_type'))then
        COMMENT ON COLUMN public.spd_request_registration.doc_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'custom_xml'))then
        COMMENT ON COLUMN public.spd_request_registration.custom_xml IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'fls'))then
        COMMENT ON COLUMN public.spd_request_registration.fls IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'status'))then
        COMMENT ON COLUMN public.spd_request_registration.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'status_code'))then
        COMMENT ON COLUMN public.spd_request_registration.status_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'date_create'))then
        COMMENT ON COLUMN public.spd_request_registration.date_create IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'date_confirm'))then
        COMMENT ON COLUMN public.spd_request_registration.date_confirm IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'date_perform'))then
        COMMENT ON COLUMN public.spd_request_registration.date_perform IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'doc_number'))then
        COMMENT ON COLUMN public.spd_request_registration.doc_number IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'name'))then
        COMMENT ON COLUMN public.spd_request_registration.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'error'))then
        COMMENT ON COLUMN public.spd_request_registration.error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'error_message'))then
        COMMENT ON COLUMN public.spd_request_registration.error_message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'regid'))then
        COMMENT ON COLUMN public.spd_request_registration.regid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'app_status_id'))then
        COMMENT ON COLUMN public.spd_request_registration.app_status_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'inn'))then
        COMMENT ON COLUMN public.spd_request_registration.inn IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_request_registration', 'typerus'))then
        COMMENT ON COLUMN public.spd_request_registration.typerus IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'id'))then
        COMMENT ON COLUMN public.spd_doc_agreement.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'planid'))then
        COMMENT ON COLUMN public.spd_doc_agreement.planid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'status'))then
        COMMENT ON COLUMN public.spd_doc_agreement.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'status_code'))then
        COMMENT ON COLUMN public.spd_doc_agreement.status_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'createdate'))then
        COMMENT ON COLUMN public.spd_doc_agreement.createdate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'changedate'))then
        COMMENT ON COLUMN public.spd_doc_agreement.changedate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_appid'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_appid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_appdocid'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_appdocid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_definition'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_definition IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_ishand'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_ishand IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_issogl'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_issogl IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_num'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_podp'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_podp IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_soglcode'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_soglcode IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_sogldate'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_sogldate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_userid'))then
        COMMENT ON COLUMN public.spd_doc_agreement.spd_userid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'docattached'))then
        COMMENT ON COLUMN public.spd_doc_agreement.docattached IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_doc_agreement', 'userid'))then
        COMMENT ON COLUMN public.spd_doc_agreement.userid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_profile_id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.spd_profile_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'create_date'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.create_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'comments'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.comments IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_send_date'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.spd_send_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'message'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'error_id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.error_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_app_date'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.spd_app_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_app_id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.spd_app_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_app_name'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.spd_app_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'object_register_id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.object_register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'object_id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'object_ids'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.object_ids IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'main_object_register_id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.main_object_register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'main_object_id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.main_object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('spd_create_full_app_log', 'user_id'))then
        COMMENT ON COLUMN public.spd_create_full_app_log.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_o', 'id'))then
        COMMENT ON COLUMN public.bti_building_o.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_o', 'info'))then
        COMMENT ON COLUMN public.bti_building_o.info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_o', 'deleted'))then
        COMMENT ON COLUMN public.bti_building_o.deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_o', 'uid'))then
        COMMENT ON COLUMN public.bti_building_o.uid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_o', 'enddatechange'))then
        COMMENT ON COLUMN public.bti_building_o.enddatechange IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'id'))then
        COMMENT ON COLUMN public.bti_building_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'emp_id'))then
        COMMENT ON COLUMN public.bti_building_q.emp_id IS 'Инд.номер (25200100)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'actual'))then
        COMMENT ON COLUMN public.bti_building_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'status'))then
        COMMENT ON COLUMN public.bti_building_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'dept_id'))then
        COMMENT ON COLUMN public.bti_building_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 's_'))then
        COMMENT ON COLUMN public.bti_building_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'po_'))then
        COMMENT ON COLUMN public.bti_building_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'unom'))then
        COMMENT ON COLUMN public.bti_building_q.unom IS 'UNOM (25200200)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'kl_code'))then
        COMMENT ON COLUMN public.bti_building_q.kl_code IS 'Класс строения (25200300, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'kl'))then
        COMMENT ON COLUMN public.bti_building_q.kl IS 'Класс строения (25200300)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'naz_code'))then
        COMMENT ON COLUMN public.bti_building_q.naz_code IS 'Назначение (25200400, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'naz'))then
        COMMENT ON COLUMN public.bti_building_q.naz IS 'Назначение (25200400)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'mst_code'))then
        COMMENT ON COLUMN public.bti_building_q.mst_code IS 'Материал стен (25200500, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'mst'))then
        COMMENT ON COLUMN public.bti_building_q.mst IS 'Материал стен (25200500)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'et'))then
        COMMENT ON COLUMN public.bti_building_q.et IS 'Этажность максимальная (25200600)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'gdpostr'))then
        COMMENT ON COLUMN public.bti_building_q.gdpostr IS 'Год постройки (25200700)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'kad_n'))then
        COMMENT ON COLUMN public.bti_building_q.kad_n IS 'Кадастровый номер (К.Н.) (25200800)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'et_min'))then
        COMMENT ON COLUMN public.bti_building_q.et_min IS 'Этажность минимальная (25200900)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'et_pdz'))then
        COMMENT ON COLUMN public.bti_building_q.et_pdz IS 'Этажность подземная (25201000)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'opl'))then
        COMMENT ON COLUMN public.bti_building_q.opl IS 'Площадь общая (25201200)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'sost'))then
        COMMENT ON COLUMN public.bti_building_q.sost IS 'Состояние строения (25201400)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'sost_code'))then
        COMMENT ON COLUMN public.bti_building_q.sost_code IS 'Состояние строения (25201400, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'dtsost'))then
        COMMENT ON COLUMN public.bti_building_q.dtsost IS 'Дата состояния (25201500)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'gddo1917'))then
        COMMENT ON COLUMN public.bti_building_q.gddo1917 IS 'Признак постройки до 1917 г. (25201900)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'avarzd'))then
        COMMENT ON COLUMN public.bti_building_q.avarzd IS 'Признак Аварийное (25202500)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'dtavarzd'))then
        COMMENT ON COLUMN public.bti_building_q.dtavarzd IS 'Дата решения об аварийности здания (25202600)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'samovol'))then
        COMMENT ON COLUMN public.bti_building_q.samovol IS 'Признак Самовольное возведение (25202800)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'opl_g'))then
        COMMENT ON COLUMN public.bti_building_q.opl_g IS 'Площадь общая жилых помещений (25203000)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'opl_n'))then
        COMMENT ON COLUMN public.bti_building_q.opl_n IS 'Площадь общая нежилых помещений (25203200)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'narpl'))then
        COMMENT ON COLUMN public.bti_building_q.narpl IS 'Площадь застройки (25203400)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'gdpereob'))then
        COMMENT ON COLUMN public.bti_building_q.gdpereob IS 'Год переоборудования (25204000)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'gdkaprem'))then
        COMMENT ON COLUMN public.bti_building_q.gdkaprem IS 'Год капитального ремонта (25204100)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'ppl'))then
        COMMENT ON COLUMN public.bti_building_q.ppl IS 'Площадь общая с летними жилых помещений (25204200)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'ser'))then
        COMMENT ON COLUMN public.bti_building_q.ser IS 'Серия проекта (25203600)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'ser_code'))then
        COMMENT ON COLUMN public.bti_building_q.ser_code IS 'Серия проекта (25203600, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'kap'))then
        COMMENT ON COLUMN public.bti_building_q.kap IS 'Капитальность (25204300, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'komm'))then
        COMMENT ON COLUMN public.bti_building_q.komm IS 'Комментарий  (25204500)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'source'))then
        COMMENT ON COLUMN public.bti_building_q.source IS 'Источник данных (25206600)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'source_id'))then
        COMMENT ON COLUMN public.bti_building_q.source_id IS 'Источник данных (25206600, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'kat_code'))then
        COMMENT ON COLUMN public.bti_building_q.kat_code IS 'Тип строения (25207900, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'kat'))then
        COMMENT ON COLUMN public.bti_building_q.kat IS 'Тип строения (25207900)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'obj_type_code'))then
        COMMENT ON COLUMN public.bti_building_q.obj_type_code IS 'Тип здания (25208700, справочный код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'obj_type'))then
        COMMENT ON COLUMN public.bti_building_q.obj_type IS 'Тип здания (25208700)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'download_date'))then
        COMMENT ON COLUMN public.bti_building_q.download_date IS 'Дата загрузки сведений (25208900)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'pdvpl_n'))then
        COMMENT ON COLUMN public.bti_building_q.pdvpl_n IS 'Площадь нежилых подвалов (25209100)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'actproc'))then
        COMMENT ON COLUMN public.bti_building_q.actproc IS 'Процент износа (25110000)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'gdproc'))then
        COMMENT ON COLUMN public.bti_building_q.gdproc IS 'Год установки процента износа (25110100)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'pamarc'))then
        COMMENT ON COLUMN public.bti_building_q.pamarc IS 'признак памятник архитектуры (25202400)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'krovpl'))then
        COMMENT ON COLUMN public.bti_building_q.krovpl IS 'Площадь кровли';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'lfpq'))then
        COMMENT ON COLUMN public.bti_building_q.lfpq IS 'Количество пассажирских лифтов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'lfgpq'))then
        COMMENT ON COLUMN public.bti_building_q.lfgpq IS 'Количество грузопассажирских лифтов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'lfgq'))then
        COMMENT ON COLUMN public.bti_building_q.lfgq IS 'Количество грузовых лифтов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'pmq_g'))then
        COMMENT ON COLUMN public.bti_building_q.pmq_g IS 'Количество жилых помещений';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'kmq_g'))then
        COMMENT ON COLUMN public.bti_building_q.kmq_g IS 'Количество комнат в жилых помещениях';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'kwq'))then
        COMMENT ON COLUMN public.bti_building_q.kwq IS 'Количество квартир';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'prkor'))then
        COMMENT ON COLUMN public.bti_building_q.prkor IS 'Отметка. 1 - считать как корпус, 0 - нет. Отметка "Считать как корпус" - говорит, что это уникальный объект недвижимости. Иначе это часть другого объекта недвижимости, выделенная для целей статистического учёта.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'hpl'))then
        COMMENT ON COLUMN public.bti_building_q.hpl IS 'Площадь холодных помещений';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'eleq'))then
        COMMENT ON COLUMN public.bti_building_q.eleq IS 'Количество электрических плит';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'gazq'))then
        COMMENT ON COLUMN public.bti_building_q.gazq IS 'Количество газовых плит';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'bpl'))then
        COMMENT ON COLUMN public.bti_building_q.bpl IS 'Площадь балконов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'lpl'))then
        COMMENT ON COLUMN public.bti_building_q.lpl IS 'Площадь лоджий';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'perekr_code'))then
        COMMENT ON COLUMN public.bti_building_q.perekr_code IS 'Код справочника материал перекрытий';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'perekr'))then
        COMMENT ON COLUMN public.bti_building_q.perekr IS 'Материал перекрытий';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'krov_code'))then
        COMMENT ON COLUMN public.bti_building_q.krov_code IS 'Код справочника материал кровли';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'krov'))then
        COMMENT ON COLUMN public.bti_building_q.krov IS 'Материал кровли';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'otskorp'))then
        COMMENT ON COLUMN public.bti_building_q.otskorp IS 'Состояние отселения корпуса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_building_q', 'otskorp_code'))then
        COMMENT ON COLUMN public.bti_building_q.otskorp_code IS 'Код состояния отселения корпуса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'id'))then
        COMMENT ON COLUMN public.bti_floor_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'emp_id'))then
        COMMENT ON COLUMN public.bti_floor_q.emp_id IS 'Инд.номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'actual'))then
        COMMENT ON COLUMN public.bti_floor_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'status'))then
        COMMENT ON COLUMN public.bti_floor_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'dept_id'))then
        COMMENT ON COLUMN public.bti_floor_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 's_'))then
        COMMENT ON COLUMN public.bti_floor_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'po_'))then
        COMMENT ON COLUMN public.bti_floor_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'building_id'))then
        COMMENT ON COLUMN public.bti_floor_q.building_id IS 'ID объекта';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'building_name'))then
        COMMENT ON COLUMN public.bti_floor_q.building_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'type_id'))then
        COMMENT ON COLUMN public.bti_floor_q.type_id IS 'Тип этажа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'type_name'))then
        COMMENT ON COLUMN public.bti_floor_q.type_name IS 'Тип этажа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'floor_number'))then
        COMMENT ON COLUMN public.bti_floor_q.floor_number IS 'Номер этажа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'floor_number_pp'))then
        COMMENT ON COLUMN public.bti_floor_q.floor_number_pp IS 'Номер этажа п/п';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'area_pp'))then
        COMMENT ON COLUMN public.bti_floor_q.area_pp IS 'Площадь_пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'guid_pp'))then
        COMMENT ON COLUMN public.bti_floor_q.guid_pp IS 'GUID_пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'number_pp'))then
        COMMENT ON COLUMN public.bti_floor_q.number_pp IS 'Номер_этажа_пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'type_pp'))then
        COMMENT ON COLUMN public.bti_floor_q.type_pp IS 'Тип_этажа_пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'is_undeground'))then
        COMMENT ON COLUMN public.bti_floor_q.is_undeground IS 'Признак Подземный';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'register_object_number'))then
        COMMENT ON COLUMN public.bti_floor_q.register_object_number IS 'Номер реестра';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_floor_q', 'floor_plan_presence'))then
        COMMENT ON COLUMN public.bti_floor_q.floor_plan_presence IS 'Наличие поэтажного плана';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'id'))then
        COMMENT ON COLUMN public.core_attachment.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'doc_number'))then
        COMMENT ON COLUMN public.core_attachment.doc_number IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'description'))then
        COMMENT ON COLUMN public.core_attachment.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'barcode'))then
        COMMENT ON COLUMN public.core_attachment.barcode IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'doc_type'))then
        COMMENT ON COLUMN public.core_attachment.doc_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'doc_type_id'))then
        COMMENT ON COLUMN public.core_attachment.doc_type_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'photo_type'))then
        COMMENT ON COLUMN public.core_attachment.photo_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'photo_type_id'))then
        COMMENT ON COLUMN public.core_attachment.photo_type_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'created_by_id'))then
        COMMENT ON COLUMN public.core_attachment.created_by_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'created_date'))then
        COMMENT ON COLUMN public.core_attachment.created_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'is_deleted'))then
        COMMENT ON COLUMN public.core_attachment.is_deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'deleted_by_id'))then
        COMMENT ON COLUMN public.core_attachment.deleted_by_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment', 'deleted_date'))then
        COMMENT ON COLUMN public.core_attachment.deleted_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'id'))then
        COMMENT ON COLUMN public.core_attachment_object.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'attachment_id'))then
        COMMENT ON COLUMN public.core_attachment_object.attachment_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'register_id'))then
        COMMENT ON COLUMN public.core_attachment_object.register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'object_id'))then
        COMMENT ON COLUMN public.core_attachment_object.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'is_deleted'))then
        COMMENT ON COLUMN public.core_attachment_object.is_deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'deleted_by_id'))then
        COMMENT ON COLUMN public.core_attachment_object.deleted_by_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'deleted_date'))then
        COMMENT ON COLUMN public.core_attachment_object.deleted_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'is_main'))then
        COMMENT ON COLUMN public.core_attachment_object.is_main IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'created_by_id'))then
        COMMENT ON COLUMN public.core_attachment_object.created_by_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_attachment_object', 'created_date'))then
        COMMENT ON COLUMN public.core_attachment_object.created_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_cache_updates', 'id'))then
        COMMENT ON COLUMN public.core_cache_updates.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_cache_updates', 'cacheobject'))then
        COMMENT ON COLUMN public.core_cache_updates.cacheobject IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_cache_updates', 'cachekey'))then
        COMMENT ON COLUMN public.core_cache_updates.cachekey IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_cache_updates', 'extradata'))then
        COMMENT ON COLUMN public.core_cache_updates.extradata IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_cache_updates', 'cache_timestamp'))then
        COMMENT ON COLUMN public.core_cache_updates.cache_timestamp IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_configparam', 'id'))then
        COMMENT ON COLUMN public.core_configparam.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_configparam', 'parentkey'))then
        COMMENT ON COLUMN public.core_configparam.parentkey IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_configparam', 'childkey'))then
        COMMENT ON COLUMN public.core_configparam.childkey IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_configparam', 'xmldata'))then
        COMMENT ON COLUMN public.core_configparam.xmldata IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_configparam', 'description'))then
        COMMENT ON COLUMN public.core_configparam.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_configparam', 'chdate'))then
        COMMENT ON COLUMN public.core_configparam.chdate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_configparam', 'userid'))then
        COMMENT ON COLUMN public.core_configparam.userid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'id'))then
        COMMENT ON COLUMN public.core_diagnostics.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'user_id'))then
        COMMENT ON COLUMN public.core_diagnostics.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'module'))then
        COMMENT ON COLUMN public.core_diagnostics.module IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'method'))then
        COMMENT ON COLUMN public.core_diagnostics.method IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'extra_key'))then
        COMMENT ON COLUMN public.core_diagnostics.extra_key IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'action_date'))then
        COMMENT ON COLUMN public.core_diagnostics.action_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'execution_duration'))then
        COMMENT ON COLUMN public.core_diagnostics.execution_duration IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'action_descr'))then
        COMMENT ON COLUMN public.core_diagnostics.action_descr IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'callstack'))then
        COMMENT ON COLUMN public.core_diagnostics.callstack IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_diagnostics', 'callstack_clob'))then
        COMMENT ON COLUMN public.core_diagnostics.callstack_clob IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'id'))then
        COMMENT ON COLUMN public.core_error_log.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'userid'))then
        COMMENT ON COLUMN public.core_error_log.userid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'message'))then
        COMMENT ON COLUMN public.core_error_log.message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'errordate'))then
        COMMENT ON COLUMN public.core_error_log.errordate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'errorpage_shown'))then
        COMMENT ON COLUMN public.core_error_log.errorpage_shown IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'msgtype'))then
        COMMENT ON COLUMN public.core_error_log.msgtype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'params_short'))then
        COMMENT ON COLUMN public.core_error_log.params_short IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'params_clob'))then
        COMMENT ON COLUMN public.core_error_log.params_clob IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'callstack'))then
        COMMENT ON COLUMN public.core_error_log.callstack IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_error_log', 'callstack_clob'))then
        COMMENT ON COLUMN public.core_error_log.callstack_clob IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_holidays', 'id'))then
        COMMENT ON COLUMN public.core_holidays.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_holidays', 'value'))then
        COMMENT ON COLUMN public.core_holidays.value IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'layoutid'))then
        COMMENT ON COLUMN public.core_layout.layoutid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'layoutname'))then
        COMMENT ON COLUMN public.core_layout.layoutname IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'layoutcomment'))then
        COMMENT ON COLUMN public.core_layout.layoutcomment IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'registerid'))then
        COMMENT ON COLUMN public.core_layout.registerid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'defaultsort'))then
        COMMENT ON COLUMN public.core_layout.defaultsort IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'preffered'))then
        COMMENT ON COLUMN public.core_layout.preffered IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'username'))then
        COMMENT ON COLUMN public.core_layout.username IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'createdate'))then
        COMMENT ON COLUMN public.core_layout.createdate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'qsquery'))then
        COMMENT ON COLUMN public.core_layout.qsquery IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'isdistinct'))then
        COMMENT ON COLUMN public.core_layout.isdistinct IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'ordertype'))then
        COMMENT ON COLUMN public.core_layout.ordertype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'user_id'))then
        COMMENT ON COLUMN public.core_layout.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'iscommon'))then
        COMMENT ON COLUMN public.core_layout.iscommon IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'internal_name'))then
        COMMENT ON COLUMN public.core_layout.internal_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'enable_minicards_mode'))then
        COMMENT ON COLUMN public.core_layout.enable_minicards_mode IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'register_view_id'))then
        COMMENT ON COLUMN public.core_layout.register_view_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout', 'as_domain_id'))then
        COMMENT ON COLUMN public.core_layout.as_domain_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'id'))then
        COMMENT ON COLUMN public.core_layout_export.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'layout_id'))then
        COMMENT ON COLUMN public.core_layout_export.layout_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'user_id'))then
        COMMENT ON COLUMN public.core_layout_export.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'start_date'))then
        COMMENT ON COLUMN public.core_layout_export.start_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'end_date'))then
        COMMENT ON COLUMN public.core_layout_export.end_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'status'))then
        COMMENT ON COLUMN public.core_layout_export.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'result_message'))then
        COMMENT ON COLUMN public.core_layout_export.result_message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'file_location'))then
        COMMENT ON COLUMN public.core_layout_export.file_location IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'rows_count'))then
        COMMENT ON COLUMN public.core_layout_export.rows_count IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'qs_query'))then
        COMMENT ON COLUMN public.core_layout_export.qs_query IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'type'))then
        COMMENT ON COLUMN public.core_layout_export.type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_layout_export', 'register_view_id'))then
        COMMENT ON COLUMN public.core_layout_export.register_view_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list', 'id'))then
        COMMENT ON COLUMN public.core_list.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list', 'name'))then
        COMMENT ON COLUMN public.core_list.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list', 'register_view_id'))then
        COMMENT ON COLUMN public.core_list.register_view_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list', 'register_id'))then
        COMMENT ON COLUMN public.core_list.register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list', 'author'))then
        COMMENT ON COLUMN public.core_list.author IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list', 'iscommon'))then
        COMMENT ON COLUMN public.core_list.iscommon IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list', 'list_comment'))then
        COMMENT ON COLUMN public.core_list.list_comment IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_list', 'change_date'))then
        COMMENT ON COLUMN public.core_list.change_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_log', 'id'))then
        COMMENT ON COLUMN public.core_long_process_log.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_log', 'exe_info'))then
        COMMENT ON COLUMN public.core_long_process_log.exe_info IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_log', 'start_date'))then
        COMMENT ON COLUMN public.core_long_process_log.start_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_log', 'last_check_date'))then
        COMMENT ON COLUMN public.core_long_process_log.last_check_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_log', 'status'))then
        COMMENT ON COLUMN public.core_long_process_log.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_log', 'user_id'))then
        COMMENT ON COLUMN public.core_long_process_log.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'id'))then
        COMMENT ON COLUMN public.core_long_process_type.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'process_name'))then
        COMMENT ON COLUMN public.core_long_process_type.process_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'class_name'))then
        COMMENT ON COLUMN public.core_long_process_type.class_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'schedule_type'))then
        COMMENT ON COLUMN public.core_long_process_type.schedule_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'repeat_interval'))then
        COMMENT ON COLUMN public.core_long_process_type.repeat_interval IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'enabled'))then
        COMMENT ON COLUMN public.core_long_process_type.enabled IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'run_count'))then
        COMMENT ON COLUMN public.core_long_process_type.run_count IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'failure_count'))then
        COMMENT ON COLUMN public.core_long_process_type.failure_count IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'last_start_date'))then
        COMMENT ON COLUMN public.core_long_process_type.last_start_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'last_run_duration'))then
        COMMENT ON COLUMN public.core_long_process_type.last_run_duration IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'next_run_date'))then
        COMMENT ON COLUMN public.core_long_process_type.next_run_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'parameters'))then
        COMMENT ON COLUMN public.core_long_process_type.parameters IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'description'))then
        COMMENT ON COLUMN public.core_long_process_type.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_type', 'test_result'))then
        COMMENT ON COLUMN public.core_long_process_type.test_result IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_lock', 'id'))then
        COMMENT ON COLUMN public.core_register_lock.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_lock', 'userid'))then
        COMMENT ON COLUMN public.core_register_lock.userid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_lock', 'registerid'))then
        COMMENT ON COLUMN public.core_register_lock.registerid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_lock', 'objectid'))then
        COMMENT ON COLUMN public.core_register_lock.objectid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_lock', 'lockdate'))then
        COMMENT ON COLUMN public.core_register_lock.lockdate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_state', 'id'))then
        COMMENT ON COLUMN public.core_register_state.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_state', 'register_view_id'))then
        COMMENT ON COLUMN public.core_register_state.register_view_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_state', 'user_id'))then
        COMMENT ON COLUMN public.core_register_state.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_state', 'state_save_date'))then
        COMMENT ON COLUMN public.core_register_state.state_save_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'id'))then
        COMMENT ON COLUMN public.core_srd_audit.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'function_id'))then
        COMMENT ON COLUMN public.core_srd_audit.function_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'user_id'))then
        COMMENT ON COLUMN public.core_srd_audit.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'actiontime'))then
        COMMENT ON COLUMN public.core_srd_audit.actiontime IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'result'))then
        COMMENT ON COLUMN public.core_srd_audit.result IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'result_desc'))then
        COMMENT ON COLUMN public.core_srd_audit.result_desc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'session_id'))then
        COMMENT ON COLUMN public.core_srd_audit.session_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'object_register_id'))then
        COMMENT ON COLUMN public.core_srd_audit.object_register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'object_id'))then
        COMMENT ON COLUMN public.core_srd_audit.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'object_status_id'))then
        COMMENT ON COLUMN public.core_srd_audit.object_status_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'external_id'))then
        COMMENT ON COLUMN public.core_srd_audit.external_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'main_object_register_id'))then
        COMMENT ON COLUMN public.core_srd_audit.main_object_register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_audit', 'main_object_id'))then
        COMMENT ON COLUMN public.core_srd_audit.main_object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'id'))then
        COMMENT ON COLUMN public.core_srd_session.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'user_id'))then
        COMMENT ON COLUMN public.core_srd_session.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'logintime'))then
        COMMENT ON COLUMN public.core_srd_session.logintime IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'logouttime'))then
        COMMENT ON COLUMN public.core_srd_session.logouttime IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'ip'))then
        COMMENT ON COLUMN public.core_srd_session.ip IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'asp_session_id'))then
        COMMENT ON COLUMN public.core_srd_session.asp_session_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'browser_name'))then
        COMMENT ON COLUMN public.core_srd_session.browser_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'browser_version'))then
        COMMENT ON COLUMN public.core_srd_session.browser_version IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'browser_platform'))then
        COMMENT ON COLUMN public.core_srd_session.browser_platform IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'browser_js_version'))then
        COMMENT ON COLUMN public.core_srd_session.browser_js_version IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'login_status'))then
        COMMENT ON COLUMN public.core_srd_session.login_status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'commentary'))then
        COMMENT ON COLUMN public.core_srd_session.commentary IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_session', 'last_activity'))then
        COMMENT ON COLUMN public.core_srd_session.last_activity IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_attachments', 'id'))then
        COMMENT ON COLUMN public.core_td_attachments.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_attachments', 'td_id'))then
        COMMENT ON COLUMN public.core_td_attachments.td_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_attachments', 'attachment_id'))then
        COMMENT ON COLUMN public.core_td_attachments.attachment_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_attachments', 'is_deleted'))then
        COMMENT ON COLUMN public.core_td_attachments.is_deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_attachments', 'deleted_by'))then
        COMMENT ON COLUMN public.core_td_attachments.deleted_by IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_attachments', 'deleted_date'))then
        COMMENT ON COLUMN public.core_td_attachments.deleted_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'id'))then
        COMMENT ON COLUMN public.core_td_audit.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'td_id'))then
        COMMENT ON COLUMN public.core_td_audit.td_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'action_id'))then
        COMMENT ON COLUMN public.core_td_audit.action_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'date_time'))then
        COMMENT ON COLUMN public.core_td_audit.date_time IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'actionresult'))then
        COMMENT ON COLUMN public.core_td_audit.actionresult IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'statusafter'))then
        COMMENT ON COLUMN public.core_td_audit.statusafter IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'newauthor'))then
        COMMENT ON COLUMN public.core_td_audit.newauthor IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'newnumber'))then
        COMMENT ON COLUMN public.core_td_audit.newnumber IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'description'))then
        COMMENT ON COLUMN public.core_td_audit.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_audit', 'user_id'))then
        COMMENT ON COLUMN public.core_td_audit.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_changeset', 'id'))then
        COMMENT ON COLUMN public.core_td_changeset.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_changeset', 'td_id'))then
        COMMENT ON COLUMN public.core_td_changeset.td_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_changeset', 'changeset_date'))then
        COMMENT ON COLUMN public.core_td_changeset.changeset_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_changeset', 'status'))then
        COMMENT ON COLUMN public.core_td_changeset.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_changeset', 'user_id'))then
        COMMENT ON COLUMN public.core_td_changeset.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'id'))then
        COMMENT ON COLUMN public.core_td_instance.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'template_version_id'))then
        COMMENT ON COLUMN public.core_td_instance.template_version_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'description'))then
        COMMENT ON COLUMN public.core_td_instance.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'author_id'))then
        COMMENT ON COLUMN public.core_td_instance.author_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'regnumber'))then
        COMMENT ON COLUMN public.core_td_instance.regnumber IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'create_date'))then
        COMMENT ON COLUMN public.core_td_instance.create_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'change_date'))then
        COMMENT ON COLUMN public.core_td_instance.change_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'status'))then
        COMMENT ON COLUMN public.core_td_instance.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'priority'))then
        COMMENT ON COLUMN public.core_td_instance.priority IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'object_id'))then
        COMMENT ON COLUMN public.core_td_instance.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'approve_date'))then
        COMMENT ON COLUMN public.core_td_instance.approve_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'approve_user'))then
        COMMENT ON COLUMN public.core_td_instance.approve_user IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_instance', 'register_id'))then
        COMMENT ON COLUMN public.core_td_instance.register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'id'))then
        COMMENT ON COLUMN public.core_td_template_version.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'template_id'))then
        COMMENT ON COLUMN public.core_td_template_version.template_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'version'))then
        COMMENT ON COLUMN public.core_td_template_version.version IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'xsd'))then
        COMMENT ON COLUMN public.core_td_template_version.xsd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'xsl_print'))then
        COMMENT ON COLUMN public.core_td_template_version.xsl_print IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'publish_path'))then
        COMMENT ON COLUMN public.core_td_template_version.publish_path IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'create_date'))then
        COMMENT ON COLUMN public.core_td_template_version.create_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'author'))then
        COMMENT ON COLUMN public.core_td_template_version.author IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'xsd_class_name'))then
        COMMENT ON COLUMN public.core_td_template_version.xsd_class_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'template_type'))then
        COMMENT ON COLUMN public.core_td_template_version.template_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_td_template_version', 'print_view_specified'))then
        COMMENT ON COLUMN public.core_td_template_version.print_view_specified IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_updstru_log', 'id'))then
        COMMENT ON COLUMN public.core_updstru_log.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_updstru_log', 'date_start'))then
        COMMENT ON COLUMN public.core_updstru_log.date_start IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_updstru_log', 'date_finish'))then
        COMMENT ON COLUMN public.core_updstru_log.date_finish IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_updstru_log', 'script_name'))then
        COMMENT ON COLUMN public.core_updstru_log.script_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_updstru_log', 'script_version'))then
        COMMENT ON COLUMN public.core_updstru_log.script_version IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_updstru_log', 'has_err'))then
        COMMENT ON COLUMN public.core_updstru_log.has_err IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_updstru_log', 'result_message'))then
        COMMENT ON COLUMN public.core_updstru_log.result_message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'aoguid'))then
        COMMENT ON COLUMN public.fias_house.aoguid IS 'Глобальный уникальный идентификатор записи родительского объекта (улицы, города, населенного пункта и т.п.)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'buildnum'))then
        COMMENT ON COLUMN public.fias_house.buildnum IS 'Номер корпуса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'enddate'))then
        COMMENT ON COLUMN public.fias_house.enddate IS 'Окончание действия записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'eststatus'))then
        COMMENT ON COLUMN public.fias_house.eststatus IS 'Признак владения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'houseguid'))then
        COMMENT ON COLUMN public.fias_house.houseguid IS 'Глобальный уникальный идентификатор дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'houseid'))then
        COMMENT ON COLUMN public.fias_house.houseid IS 'Уникальный идентификатор записи дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'housenum'))then
        COMMENT ON COLUMN public.fias_house.housenum IS 'Номер дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'statstatus'))then
        COMMENT ON COLUMN public.fias_house.statstatus IS 'Состояние дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'ifnsfl'))then
        COMMENT ON COLUMN public.fias_house.ifnsfl IS 'Код ИФНС ФЛ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'ifnsul'))then
        COMMENT ON COLUMN public.fias_house.ifnsul IS 'Код ИФНС ЮЛ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'okato'))then
        COMMENT ON COLUMN public.fias_house.okato IS 'ОКАТО';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'oktmo'))then
        COMMENT ON COLUMN public.fias_house.oktmo IS 'ОКТМО';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'postalcode'))then
        COMMENT ON COLUMN public.fias_house.postalcode IS 'Почтовый индекс';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'startdate'))then
        COMMENT ON COLUMN public.fias_house.startdate IS 'Начало действия записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'strucnum'))then
        COMMENT ON COLUMN public.fias_house.strucnum IS 'Номер строения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'strstatus'))then
        COMMENT ON COLUMN public.fias_house.strstatus IS 'Признак строения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'terrifnsfl'))then
        COMMENT ON COLUMN public.fias_house.terrifnsfl IS 'Код территориального участка ИФНС ФЛ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'terrifnsul'))then
        COMMENT ON COLUMN public.fias_house.terrifnsul IS 'Код территориального участка ИФНС ЮЛ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'updatedate'))then
        COMMENT ON COLUMN public.fias_house.updatedate IS 'Дата  внесения (обновления) записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'normdoc'))then
        COMMENT ON COLUMN public.fias_house.normdoc IS 'Внешний ключ на нормативный документ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'counter'))then
        COMMENT ON COLUMN public.fias_house.counter IS 'Счетчик записей домов для КЛАДР 4';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'cadnum'))then
        COMMENT ON COLUMN public.fias_house.cadnum IS 'Кадастровый номер здания';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_house', 'divtype'))then
        COMMENT ON COLUMN public.fias_house.divtype IS 'Тип деления: 0 – не определено 1 – муниципальное 2 – административное';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'roomid'))then
        COMMENT ON COLUMN public.fias_room.roomid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'roomguid'))then
        COMMENT ON COLUMN public.fias_room.roomguid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'houseguid'))then
        COMMENT ON COLUMN public.fias_room.houseguid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'regioncode'))then
        COMMENT ON COLUMN public.fias_room.regioncode IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'flatnumber'))then
        COMMENT ON COLUMN public.fias_room.flatnumber IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'flattype'))then
        COMMENT ON COLUMN public.fias_room.flattype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'roomnumber'))then
        COMMENT ON COLUMN public.fias_room.roomnumber IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'roomtype'))then
        COMMENT ON COLUMN public.fias_room.roomtype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'cadnum'))then
        COMMENT ON COLUMN public.fias_room.cadnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'roomcadnum'))then
        COMMENT ON COLUMN public.fias_room.roomcadnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'postalcode'))then
        COMMENT ON COLUMN public.fias_room.postalcode IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'updatedate'))then
        COMMENT ON COLUMN public.fias_room.updatedate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'previd'))then
        COMMENT ON COLUMN public.fias_room.previd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'nextid'))then
        COMMENT ON COLUMN public.fias_room.nextid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'operstatus'))then
        COMMENT ON COLUMN public.fias_room.operstatus IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'startdate'))then
        COMMENT ON COLUMN public.fias_room.startdate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'enddate'))then
        COMMENT ON COLUMN public.fias_room.enddate IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'livestatus'))then
        COMMENT ON COLUMN public.fias_room.livestatus IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_room', 'normdoc'))then
        COMMENT ON COLUMN public.fias_room.normdoc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'emp_id'))then
        COMMENT ON COLUMN public.insur_bank_plat.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_bank_plat.link_id_file IS 'Ссылка на INSUR_INPUT_FILE';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'link_svod_bank'))then
        COMMENT ON COLUMN public.insur_bank_plat.link_svod_bank IS 'Ссылка на INSUR_SVOD_BANK';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'district_id'))then
        COMMENT ON COLUMN public.insur_bank_plat.district_id IS 'Идентификатор района';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'bank_day'))then
        COMMENT ON COLUMN public.insur_bank_plat.bank_day IS 'Банковский день';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kodpl'))then
        COMMENT ON COLUMN public.insur_bank_plat.kodpl IS 'Код плательщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_bank_plat.period_reg_date IS 'Первое число месяца, в котором данные должны быть учтены на ФСП';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'period'))then
        COMMENT ON COLUMN public.insur_bank_plat.period IS 'Оплачиваемый период';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'nom_doc'))then
        COMMENT ON COLUMN public.insur_bank_plat.nom_doc IS 'Номер документа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'sum_all'))then
        COMMENT ON COLUMN public.insur_bank_plat.sum_all IS 'Cумма платежа (всего по ЕПД)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kom_bank_all'))then
        COMMENT ON COLUMN public.insur_bank_plat.kom_bank_all IS 'Комиссия Банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'bic_bank'))then
        COMMENT ON COLUMN public.insur_bank_plat.bic_bank IS 'БИК Банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'data_pp'))then
        COMMENT ON COLUMN public.insur_bank_plat.data_pp IS 'Дата платежа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'cod_doc'))then
        COMMENT ON COLUMN public.insur_bank_plat.cod_doc IS 'Уникальный код документа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kod_ysl'))then
        COMMENT ON COLUMN public.insur_bank_plat.kod_ysl IS 'Код услуги';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kod_post'))then
        COMMENT ON COLUMN public.insur_bank_plat.kod_post IS 'Код поставщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'sum_by_code'))then
        COMMENT ON COLUMN public.insur_bank_plat.sum_by_code IS 'Сумма платежа по коду';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kom_bank'))then
        COMMENT ON COLUMN public.insur_bank_plat.kom_bank IS 'Комиссия Банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kom_bank_obr'))then
        COMMENT ON COLUMN public.insur_bank_plat.kom_bank_obr IS 'Комиссия за обработку';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kom_eirc'))then
        COMMENT ON COLUMN public.insur_bank_plat.kom_eirc IS 'Комиссия ЕИРЦ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kom_plat'))then
        COMMENT ON COLUMN public.insur_bank_plat.kom_plat IS 'Комиссия за работу с плательщиками';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'flag_vozvr'))then
        COMMENT ON COLUMN public.insur_bank_plat.flag_vozvr IS 'Признак распределения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'type_opl'))then
        COMMENT ON COLUMN public.insur_bank_plat.type_opl IS 'Тип оплаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'kod_ypravl'))then
        COMMENT ON COLUMN public.insur_bank_plat.kod_ypravl IS 'Код управляющей компании';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'flag_nach'))then
        COMMENT ON COLUMN public.insur_bank_plat.flag_nach IS 'Признак начисления';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'sum_vsego'))then
        COMMENT ON COLUMN public.insur_bank_plat.sum_vsego IS 'Сумма платежей в файле';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'strok_vsego'))then
        COMMENT ON COLUMN public.insur_bank_plat.strok_vsego IS 'Кол-во строк в файле';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank_plat', 'doc_period'))then
        COMMENT ON COLUMN public.insur_bank_plat.doc_period IS 'Период документа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_svod_bank', 'emp_id'))then
        COMMENT ON COLUMN public.insur_svod_bank.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_svod_bank', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_svod_bank.link_id_file IS 'Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_svod_bank', 'file_name'))then
        COMMENT ON COLUMN public.insur_svod_bank.file_name IS 'Название файла';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_svod_bank', 'bank_day'))then
        COMMENT ON COLUMN public.insur_svod_bank.bank_day IS 'Банковский день';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_svod_bank', 'district_id'))then
        COMMENT ON COLUMN public.insur_svod_bank.district_id IS 'Идентификатор района';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_svod_bank', 'str'))then
        COMMENT ON COLUMN public.insur_svod_bank.str IS 'Кол-во строк в файле';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_svod_bank', 'pay_sum'))then
        COMMENT ON COLUMN public.insur_svod_bank.pay_sum IS 'Общая сумма платежей';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_svod_bank', 'cod_post'))then
        COMMENT ON COLUMN public.insur_svod_bank.cod_post IS 'Код поставщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'qryid'))then
        COMMENT ON COLUMN public.core_qry.qryid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'name'))then
        COMMENT ON COLUMN public.core_qry.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'description'))then
        COMMENT ON COLUMN public.core_qry.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'datefrom'))then
        COMMENT ON COLUMN public.core_qry.datefrom IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'inlist'))then
        COMMENT ON COLUMN public.core_qry.inlist IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'qry_user'))then
        COMMENT ON COLUMN public.core_qry.qry_user IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'registerid'))then
        COMMENT ON COLUMN public.core_qry.registerid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'qscondition'))then
        COMMENT ON COLUMN public.core_qry.qscondition IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'user_id'))then
        COMMENT ON COLUMN public.core_qry.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'iscommon'))then
        COMMENT ON COLUMN public.core_qry.iscommon IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'internal_name'))then
        COMMENT ON COLUMN public.core_qry.internal_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'register_view_id'))then
        COMMENT ON COLUMN public.core_qry.register_view_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_qry', 'author'))then
        COMMENT ON COLUMN public.core_qry.author IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'id'))then
        COMMENT ON COLUMN public.insur_link_causes_subreason_lp.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'causes_of_damage'))then
        COMMENT ON COLUMN public.insur_link_causes_subreason_lp.causes_of_damage IS 'Причина ущерба по ЖП (справочник, 12125)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'causes_of_damage_code'))then
        COMMENT ON COLUMN public.insur_link_causes_subreason_lp.causes_of_damage_code IS 'Причина ущерба по ЖП (справочник, 12125, code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'subreason_causes_of_damage'))then
        COMMENT ON COLUMN public.insur_link_causes_subreason_lp.subreason_causes_of_damage IS 'Подпричины ущерба по ЖП (справочник, 12134)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'subreason_causes_of_damage_code'))then
        COMMENT ON COLUMN public.insur_link_causes_subreason_lp.subreason_causes_of_damage_code IS 'Подпричины ущерба по ЖП (справочник, 12134, code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'refinement_subreason'))then
        COMMENT ON COLUMN public.insur_link_causes_subreason_lp.refinement_subreason IS 'Уточнение Подпричины ущерба (справочник, 12135)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'refinement_subreason_code'))then
        COMMENT ON COLUMN public.insur_link_causes_subreason_lp.refinement_subreason_code IS 'Уточнение Подпричины ущерба (справочник, 12135, code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_gbu_no_pay_reason', 'id'))then
        COMMENT ON COLUMN public.insur_gbu_no_pay_reason.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_gbu_no_pay_reason', 'reason'))then
        COMMENT ON COLUMN public.insur_gbu_no_pay_reason.reason IS 'Причина';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_gbu_no_pay_reason', 'type_insur'))then
        COMMENT ON COLUMN public.insur_gbu_no_pay_reason.type_insur IS 'Вид страхования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_gbu_no_pay_reason', 'short_explanation'))then
        COMMENT ON COLUMN public.insur_gbu_no_pay_reason.short_explanation IS 'Краткое пояснение (должно быть напечатано на заключении)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'emp_id'))then
        COMMENT ON COLUMN public.insur_pay_to.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'fsp_id'))then
        COMMENT ON COLUMN public.insur_pay_to.fsp_id IS 'Ссылка на ФСП INSUR_FSP_Q.EMP_ID';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'contract_id'))then
        COMMENT ON COLUMN public.insur_pay_to.contract_id IS '-Ссылка на  запись в INSUR_POLICY_SVD или  INSUR_ALL_PROPERTY';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'id_reestr_contr'))then
        COMMENT ON COLUMN public.insur_pay_to.id_reestr_contr IS 'Номер реестра:';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'type_doc'))then
        COMMENT ON COLUMN public.insur_pay_to.type_doc IS 'Тип договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'type_doc_code'))then
        COMMENT ON COLUMN public.insur_pay_to.type_doc_code IS 'Тип договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'period'))then
        COMMENT ON COLUMN public.insur_pay_to.period IS 'Первый день месяца, за который предоставляются данные';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'insurance_organization_id'))then
        COMMENT ON COLUMN public.insur_pay_to.insurance_organization_id IS 'Код страховой организации – по справочнику «Страховые организации»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'aok'))then
        COMMENT ON COLUMN public.insur_pay_to.aok IS 'Код административного округа*';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'unom'))then
        COMMENT ON COLUMN public.insur_pay_to.unom IS 'Уникальный номер дома*';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'nom'))then
        COMMENT ON COLUMN public.insur_pay_to.nom IS 'Номер квартиры*';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'nomi'))then
        COMMENT ON COLUMN public.insur_pay_to.nomi IS 'Индекс квартиры*';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'npol'))then
        COMMENT ON COLUMN public.insur_pay_to.npol IS 'Номер полиса или страхового свидетельства';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'npoldate'))then
        COMMENT ON COLUMN public.insur_pay_to.npoldate IS 'Дата начала действия договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'comnumber'))then
        COMMENT ON COLUMN public.insur_pay_to.comnumber IS 'Номер акта осмотра жилого помещения/объекта общего имущества';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'comdate'))then
        COMMENT ON COLUMN public.insur_pay_to.comdate IS 'Дата акта осмотра жилого помещения/объекта общего имущества';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'sl'))then
        COMMENT ON COLUMN public.insur_pay_to.sl IS 'Размер ущерба';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'sp_fact'))then
        COMMENT ON COLUMN public.insur_pay_to.sp_fact IS 'Сумма фактически выплаченного страхового                                              возмещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'sp_no'))then
        COMMENT ON COLUMN public.insur_pay_to.sp_no IS 'Размер удержанной части страхового возмещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'factnumber'))then
        COMMENT ON COLUMN public.insur_pay_to.factnumber IS 'Номер платежного поручения страховой организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'factdate'))then
        COMMENT ON COLUMN public.insur_pay_to.factdate IS 'Дата платежного поручения страховой организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'ndoc'))then
        COMMENT ON COLUMN public.insur_pay_to.ndoc IS 'Номер договора страхования общего имущества';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'ndogdat'))then
        COMMENT ON COLUMN public.insur_pay_to.ndogdat IS 'Дата начала действия договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_pay_to.link_id_file IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'obj_id'))then
        COMMENT ON COLUMN public.insur_pay_to.obj_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'obj_reestr_id'))then
        COMMENT ON COLUMN public.insur_pay_to.obj_reestr_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'link_damage_id'))then
        COMMENT ON COLUMN public.insur_pay_to.link_damage_id IS 'Ссылка на реестр дел';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_pay_to', 'subject_id'))then
        COMMENT ON COLUMN public.insur_pay_to.subject_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'emp_id'))then
        COMMENT ON COLUMN public.insur_no_pay.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'fsp_id'))then
        COMMENT ON COLUMN public.insur_no_pay.fsp_id IS 'Ссылка на ФСП INSUR_FSP_Q.EMP_ID';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'contract_id'))then
        COMMENT ON COLUMN public.insur_no_pay.contract_id IS 'Ссылка на  запись в INSUR_POLICY_SVD или  INSUR_ALL_PROPERTY';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'id_reestr_contr'))then
        COMMENT ON COLUMN public.insur_no_pay.id_reestr_contr IS 'Номер реестра:';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'type_doc'))then
        COMMENT ON COLUMN public.insur_no_pay.type_doc IS 'Тип договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'type_doc_code'))then
        COMMENT ON COLUMN public.insur_no_pay.type_doc_code IS 'Тип договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_no_pay.period_reg_date IS 'Первый день месяца, за который предоставляются данные';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'insurance_organization_id'))then
        COMMENT ON COLUMN public.insur_no_pay.insurance_organization_id IS 'Код страховой организации – по справочнику «Страховые организации»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'okrug_id'))then
        COMMENT ON COLUMN public.insur_no_pay.okrug_id IS 'Код административного округа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'npol'))then
        COMMENT ON COLUMN public.insur_no_pay.npol IS 'Номер полиса или страхового свидетельства';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'npoldate'))then
        COMMENT ON COLUMN public.insur_no_pay.npoldate IS 'Дата начала действия договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'eventdat'))then
        COMMENT ON COLUMN public.insur_no_pay.eventdat IS 'Дата страхового события';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'appdat'))then
        COMMENT ON COLUMN public.insur_no_pay.appdat IS 'Дата заявления о страховом событии';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'reject_code'))then
        COMMENT ON COLUMN public.insur_no_pay.reject_code IS 'Код причины отказа в страховой выплате – по справочнику «Причины отказа в выплате по договору страхования жилого помещения» /«Причины отказа в выплате по договору страхования общего помещения»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'renumber'))then
        COMMENT ON COLUMN public.insur_no_pay.renumber IS 'Номер письма страховой организации об отказе в страховой выплате';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'redat'))then
        COMMENT ON COLUMN public.insur_no_pay.redat IS 'Дата письма страховой организации об отказе в страховой выплате';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'unom'))then
        COMMENT ON COLUMN public.insur_no_pay.unom IS 'Уникальный номер строения ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'subject_id'))then
        COMMENT ON COLUMN public.insur_no_pay.subject_id IS 'Код управляющей организации (по справочнику «Управляющие организации»)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'name'))then
        COMMENT ON COLUMN public.insur_no_pay.name IS 'Наименование страхователя (ЖСК, ЖК, ТСЖ, управляющей организации)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'ndog'))then
        COMMENT ON COLUMN public.insur_no_pay.ndog IS 'Уникальный номер договора страхования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'ndogdat'))then
        COMMENT ON COLUMN public.insur_no_pay.ndogdat IS 'Дата начала действия договора страхования ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'phonedat'))then
        COMMENT ON COLUMN public.insur_no_pay.phonedat IS 'Дата сообщения о страховом событии';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'inspnumber'))then
        COMMENT ON COLUMN public.insur_no_pay.inspnumber IS 'Номер акта осмотра объекта по заявленному событию';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'inspdat'))then
        COMMENT ON COLUMN public.insur_no_pay.inspdat IS 'Дата акта осмотра объекта по заявленному событию';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'reason'))then
        COMMENT ON COLUMN public.insur_no_pay.reason IS 'Причина страховых событий по договору страхования общего имущества';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'reason_code'))then
        COMMENT ON COLUMN public.insur_no_pay.reason_code IS 'Код предполагаемой причины страхового события – по справочнику  «Причины страховых событий по договору страхования общего имущества»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'reasabs'))then
        COMMENT ON COLUMN public.insur_no_pay.reasabs IS 'Причина отсутствия решения о страховой выплате по договору страхования общего имущества';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'reasabs_code'))then
        COMMENT ON COLUMN public.insur_no_pay.reasabs_code IS 'Код причины отсутствия решения по страховому событию – по справочнику “Причины отсутствия решения о страховой выплате по договору страхования общего имущества»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'reject'))then
        COMMENT ON COLUMN public.insur_no_pay.reject IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_no_pay.link_id_file IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'obj_id'))then
        COMMENT ON COLUMN public.insur_no_pay.obj_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'obj_reestr_id'))then
        COMMENT ON COLUMN public.insur_no_pay.obj_reestr_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'org_type'))then
        COMMENT ON COLUMN public.insur_no_pay.org_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_no_pay', 'org_type_code'))then
        COMMENT ON COLUMN public.insur_no_pay.org_type_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'emp_id'))then
        COMMENT ON COLUMN public.insur_input_plat.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_input_plat.link_id_file IS 'Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'fsp_id'))then
        COMMENT ON COLUMN public.insur_input_plat.fsp_id IS 'Ссылка на реестр ФСП INSUR_FSP';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'link_bank_id'))then
        COMMENT ON COLUMN public.insur_input_plat.link_bank_id IS 'Ссылка на реестр c банковскими днями INSUR_BANK_PLAT';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'unom'))then
        COMMENT ON COLUMN public.insur_input_plat.unom IS 'Уникальный номер дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'adres'))then
        COMMENT ON COLUMN public.insur_input_plat.adres IS 'Адрес дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'nom'))then
        COMMENT ON COLUMN public.insur_input_plat.nom IS 'Номер квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'kodpl'))then
        COMMENT ON COLUMN public.insur_input_plat.kodpl IS 'Код плательщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'ls'))then
        COMMENT ON COLUMN public.insur_input_plat.ls IS 'Лицевой счет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'tx_id'))then
        COMMENT ON COLUMN public.insur_input_plat.tx_id IS 'Банковская кодировка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'pmt_date'))then
        COMMENT ON COLUMN public.insur_input_plat.pmt_date IS 'Дата оплаты';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'date_in_tofk'))then
        COMMENT ON COLUMN public.insur_input_plat.date_in_tofk IS 'Дата поступления оплаты в банк';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'period'))then
        COMMENT ON COLUMN public.insur_input_plat.period IS 'Период, за который произведена оплата';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_input_plat.period_reg_date IS 'Период учета данных в Системе';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'sum_nach'))then
        COMMENT ON COLUMN public.insur_input_plat.sum_nach IS 'Начисленная сумма';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'sum_opl'))then
        COMMENT ON COLUMN public.insur_input_plat.sum_opl IS 'Оплаченная сумма (может быть отрицательным числом)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'fee'))then
        COMMENT ON COLUMN public.insur_input_plat.fee IS 'Комиссия банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'opl'))then
        COMMENT ON COLUMN public.insur_input_plat.opl IS 'Площадь, подлежащая страхованию';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'flat_type_id'))then
        COMMENT ON COLUMN public.insur_input_plat.flat_type_id IS 'Код типа жилого помещения, на основании справочника «Тип жилого помещения»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'fio'))then
        COMMENT ON COLUMN public.insur_input_plat.fio IS 'ФИО';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'comment'))then
        COMMENT ON COLUMN public.insur_input_plat.comment IS 'Комментарий';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'status_identif'))then
        COMMENT ON COLUMN public.insur_input_plat.status_identif IS 'Статус идентификации записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'flag_unom_no'))then
        COMMENT ON COLUMN public.insur_input_plat.flag_unom_no IS '1/0 (UNOM найден в Адресном списке/ UNOM не найден в Адресном списке)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'kod'))then
        COMMENT ON COLUMN public.insur_input_plat.kod IS 'Код страховой организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'ndog'))then
        COMMENT ON COLUMN public.insur_input_plat.ndog IS 'Уникальный номер договора страхования (для платежей по договорам  общего имущества)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'ndogdat'))then
        COMMENT ON COLUMN public.insur_input_plat.ndogdat IS 'Дата начала действия договора(для платежей по договорам  общего имущества)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'ndops'))then
        COMMENT ON COLUMN public.insur_input_plat.ndops IS 'Номер дополнительного соглашения((для платежей по договорам  общего имущества)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'paynumber'))then
        COMMENT ON COLUMN public.insur_input_plat.paynumber IS 'Номер платежного поручения по договору (для платежей по договорам  общего имущества)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'status_identif_code'))then
        COMMENT ON COLUMN public.insur_input_plat.status_identif_code IS 'Код статуса идентификации записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'district_id'))then
        COMMENT ON COLUMN public.insur_input_plat.district_id IS 'Идентификатор района';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'type_source_code'))then
        COMMENT ON COLUMN public.insur_input_plat.type_source_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'type_doc_code'))then
        COMMENT ON COLUMN public.insur_input_plat.type_doc_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'type_source'))then
        COMMENT ON COLUMN public.insur_input_plat.type_source IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'type_doc'))then
        COMMENT ON COLUMN public.insur_input_plat.type_doc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'insur_policy_svd_id'))then
        COMMENT ON COLUMN public.insur_input_plat.insur_policy_svd_id IS 'Ссылка на реестр INSUR_POLICY_SVD';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'load_status'))then
        COMMENT ON COLUMN public.insur_input_plat.load_status IS 'Статус загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'load_status_code'))then
        COMMENT ON COLUMN public.insur_input_plat.load_status_code IS 'Код статуса загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'criteria_json'))then
        COMMENT ON COLUMN public.insur_input_plat.criteria_json IS 'Список вхождений по критериям';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_plat', 'link_all_property_id'))then
        COMMENT ON COLUMN public.insur_input_plat.link_all_property_id IS 'Ссылка на реестр AllProrepty';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user_role', 'id'))then
        COMMENT ON COLUMN public.core_srd_user_role.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user_role', 'user_id'))then
        COMMENT ON COLUMN public.core_srd_user_role.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user_role', 'role_id'))then
        COMMENT ON COLUMN public.core_srd_user_role.role_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_register', 'id'))then
        COMMENT ON COLUMN public.core_srd_role_register.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_register', 'role_id'))then
        COMMENT ON COLUMN public.core_srd_role_register.role_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_register', 'register_id'))then
        COMMENT ON COLUMN public.core_srd_role_register.register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_register', 'can_read'))then
        COMMENT ON COLUMN public.core_srd_role_register.can_read IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_register', 'can_write'))then
        COMMENT ON COLUMN public.core_srd_role_register.can_write IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_register', 'all_attributes'))then
        COMMENT ON COLUMN public.core_srd_role_register.all_attributes IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_function', 'id'))then
        COMMENT ON COLUMN public.core_srd_role_function.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_function', 'function_id'))then
        COMMENT ON COLUMN public.core_srd_role_function.function_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_function', 'role_id'))then
        COMMENT ON COLUMN public.core_srd_role_function.role_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role', 'id'))then
        COMMENT ON COLUMN public.core_srd_role.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role', 'rolename'))then
        COMMENT ON COLUMN public.core_srd_role.rolename IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role', 'roletag'))then
        COMMENT ON COLUMN public.core_srd_role.roletag IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role', 'isadmin'))then
        COMMENT ON COLUMN public.core_srd_role.isadmin IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role', 'all_registers_read'))then
        COMMENT ON COLUMN public.core_srd_role.all_registers_read IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role', 'all_registers_write'))then
        COMMENT ON COLUMN public.core_srd_role.all_registers_write IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role', 'subsystem'))then
        COMMENT ON COLUMN public.core_srd_role.subsystem IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_attr', 'id'))then
        COMMENT ON COLUMN public.core_srd_role_attr.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_attr', 'rule_id'))then
        COMMENT ON COLUMN public.core_srd_role_attr.rule_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_attr', 'attribute_id'))then
        COMMENT ON COLUMN public.core_srd_role_attr.attribute_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_attr', 'can_read'))then
        COMMENT ON COLUMN public.core_srd_role_attr.can_read IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_role_attr', 'can_write'))then
        COMMENT ON COLUMN public.core_srd_role_attr.can_write IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_function', 'id'))then
        COMMENT ON COLUMN public.core_srd_function.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_function', 'functionname'))then
        COMMENT ON COLUMN public.core_srd_function.functionname IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_function', 'functiontag'))then
        COMMENT ON COLUMN public.core_srd_function.functiontag IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_function', 'parent_id'))then
        COMMENT ON COLUMN public.core_srd_function.parent_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_function', 'description'))then
        COMMENT ON COLUMN public.core_srd_function.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_department', 'id'))then
        COMMENT ON COLUMN public.core_srd_department.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_department', 'code'))then
        COMMENT ON COLUMN public.core_srd_department.code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_department', 'name'))then
        COMMENT ON COLUMN public.core_srd_department.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_department', 'manager'))then
        COMMENT ON COLUMN public.core_srd_department.manager IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_department', 'name_genitive_case'))then
        COMMENT ON COLUMN public.core_srd_department.name_genitive_case IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_department', 'is_deleted'))then
        COMMENT ON COLUMN public.core_srd_department.is_deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_comment', 'id'))then
        COMMENT ON COLUMN public.insur_comment.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_comment', 'comment'))then
        COMMENT ON COLUMN public.insur_comment.comment IS 'Комментарий';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_comment', 'user_id'))then
        COMMENT ON COLUMN public.insur_comment.user_id IS 'Пользователь';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_comment', 'date_create'))then
        COMMENT ON COLUMN public.insur_comment.date_create IS 'Дата создания';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_comment', 'link_object_id'))then
        COMMENT ON COLUMN public.insur_comment.link_object_id IS 'Ссылка на объект';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_comment', 'link_reestr_id'))then
        COMMENT ON COLUMN public.insur_comment.link_reestr_id IS 'Номер реестра';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'emp_id'))then
        COMMENT ON COLUMN public.insur_log_file.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'file_storage_id'))then
        COMMENT ON COLUMN public.insur_log_file.file_storage_id IS 'Ссылка на пакет загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'loaddate'))then
        COMMENT ON COLUMN public.insur_log_file.loaddate IS 'Дата загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'tracedata'))then
        COMMENT ON COLUMN public.insur_log_file.tracedata IS 'Результаты загрузки пакет данных (LOG-файл)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'okrug_id'))then
        COMMENT ON COLUMN public.insur_log_file.okrug_id IS 'Идентификатор округа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_log_file.period_reg_date IS 'Период';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'status'))then
        COMMENT ON COLUMN public.insur_log_file.status IS 'Прогресс загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'status_code'))then
        COMMENT ON COLUMN public.insur_log_file.status_code IS 'Код прогресса загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'general_status'))then
        COMMENT ON COLUMN public.insur_log_file.general_status IS 'Общий статус загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'general_status_code'))then
        COMMENT ON COLUMN public.insur_log_file.general_status_code IS 'Код общего статуса загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'start_date'))then
        COMMENT ON COLUMN public.insur_log_file.start_date IS 'Дата начала загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_log_file', 'end_date'))then
        COMMENT ON COLUMN public.insur_log_file.end_date IS 'Дата окончания загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_storage', 'id'))then
        COMMENT ON COLUMN public.insur_file_storage.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_storage', 'filename'))then
        COMMENT ON COLUMN public.insur_file_storage.filename IS 'Наименование файла';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_storage', 'is_virtual_directory'))then
        COMMENT ON COLUMN public.insur_file_storage.is_virtual_directory IS 'Признак виртуальной папки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_storage', 'virtual_directory_id'))then
        COMMENT ON COLUMN public.insur_file_storage.virtual_directory_id IS 'Идентификатор виртуальной папки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_storage', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_file_storage.period_reg_date IS 'Период учета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_storage', 'hash'))then
        COMMENT ON COLUMN public.insur_file_storage.hash IS 'MD5';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'emp_id'))then
        COMMENT ON COLUMN public.insur_subject.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'okrug_id'))then
        COMMENT ON COLUMN public.insur_subject.okrug_id IS 'Ссылка на реестр Округ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'kod_org'))then
        COMMENT ON COLUMN public.insur_subject.kod_org IS 'Код организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'kod_upk'))then
        COMMENT ON COLUMN public.insur_subject.kod_upk IS 'Код управляющей компании';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'subject_name'))then
        COMMENT ON COLUMN public.insur_subject.subject_name IS 'Название организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'org_id_t'))then
        COMMENT ON COLUMN public.insur_subject.org_id_t IS 'Идентификатор организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'empl_role'))then
        COMMENT ON COLUMN public.insur_subject.empl_role IS 'Должность руководителя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'fio_adm'))then
        COMMENT ON COLUMN public.insur_subject.fio_adm IS 'ФИО руководителя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'org_adr_u'))then
        COMMENT ON COLUMN public.insur_subject.org_adr_u IS 'Юридический адрес организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'org_adr_f'))then
        COMMENT ON COLUMN public.insur_subject.org_adr_f IS 'Фактический адрес организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'org_phone'))then
        COMMENT ON COLUMN public.insur_subject.org_phone IS 'Телефон организации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'date_input'))then
        COMMENT ON COLUMN public.insur_subject.date_input IS 'Дата создания';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'birthday'))then
        COMMENT ON COLUMN public.insur_subject.birthday IS 'Дата рождения (для физ лиц)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'inn'))then
        COMMENT ON COLUMN public.insur_subject.inn IS 'ИНН';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'kpp'))then
        COMMENT ON COLUMN public.insur_subject.kpp IS 'КПП';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'rach_acc'))then
        COMMENT ON COLUMN public.insur_subject.rach_acc IS 'Расчетный счет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'num_card'))then
        COMMENT ON COLUMN public.insur_subject.num_card IS 'Номер банковской карточки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'nom_doc'))then
        COMMENT ON COLUMN public.insur_subject.nom_doc IS 'Номер документа, удостоверяющего личность';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'date_doc'))then
        COMMENT ON COLUMN public.insur_subject.date_doc IS 'Дата паспорта, удостоверяющего личность';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'date_in_doc'))then
        COMMENT ON COLUMN public.insur_subject.date_in_doc IS 'Дата выдачи документа, удостоверяющего личность';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'org_doc'))then
        COMMENT ON COLUMN public.insur_subject.org_doc IS 'Организация выдавшая документ, удостоверяющего личность';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'type_subject'))then
        COMMENT ON COLUMN public.insur_subject.type_subject IS 'Тип субъекта (выбор из справочника «Тип субъекта» ­ 12142)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'type_subject_code'))then
        COMMENT ON COLUMN public.insur_subject.type_subject_code IS 'Тип субъекта (выбор из справочника «Тип субъекта», code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_subject', 'bank_id'))then
        COMMENT ON COLUMN public.insur_subject.bank_id IS 'Ссылка на EMP_ID  в INSUR_BANK';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'actstatus'))then
        COMMENT ON COLUMN public.fias_addrobj.actstatus IS 'Статус актуальности адресообразующего элемента ФИАС. Актуальный адрес на текущую дату. Обычно последняя запись об адресообразующем элементе. 0 – Не актуальный, 1 - Актуальный';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'aoguid'))then
        COMMENT ON COLUMN public.fias_addrobj.aoguid IS 'Глобальный уникальный идентификатор адресообразующего элемента';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'aoid'))then
        COMMENT ON COLUMN public.fias_addrobj.aoid IS 'Уникальный идентификатор записи. Ключевое поле';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'aolevel'))then
        COMMENT ON COLUMN public.fias_addrobj.aolevel IS 'Уровень адресообразующего элемента ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'areacode'))then
        COMMENT ON COLUMN public.fias_addrobj.areacode IS 'Код района';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'autocode'))then
        COMMENT ON COLUMN public.fias_addrobj.autocode IS 'Код автономии';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'centstatus'))then
        COMMENT ON COLUMN public.fias_addrobj.centstatus IS 'Статус центра';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'citycode'))then
        COMMENT ON COLUMN public.fias_addrobj.citycode IS 'Код города';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'code'))then
        COMMENT ON COLUMN public.fias_addrobj.code IS 'Код адресообразующего элемента одной строкой с признаком актуальности из КЛАДР 4.0.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'currstatus'))then
        COMMENT ON COLUMN public.fias_addrobj.currstatus IS 'Статус актуальности КЛАДР 4 (последние две цифры в коде)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'enddate'))then
        COMMENT ON COLUMN public.fias_addrobj.enddate IS 'Окончание действия записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'formalname'))then
        COMMENT ON COLUMN public.fias_addrobj.formalname IS 'Формализованное наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'ifnsfl'))then
        COMMENT ON COLUMN public.fias_addrobj.ifnsfl IS 'Код ИФНС ФЛ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'ifnsul'))then
        COMMENT ON COLUMN public.fias_addrobj.ifnsul IS 'Код ИФНС ЮЛ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'nextid'))then
        COMMENT ON COLUMN public.fias_addrobj.nextid IS 'Идентификатор записи  связывания с последующей исторической записью';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'offname'))then
        COMMENT ON COLUMN public.fias_addrobj.offname IS 'Официальное наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'okato'))then
        COMMENT ON COLUMN public.fias_addrobj.okato IS 'ОКАТО';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'oktmo'))then
        COMMENT ON COLUMN public.fias_addrobj.oktmo IS 'ОКТМО';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'operstatus'))then
        COMMENT ON COLUMN public.fias_addrobj.operstatus IS 'Статус действия над записью – причина появления записи (см. описание таблицы OperationStatus)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'parentguid'))then
        COMMENT ON COLUMN public.fias_addrobj.parentguid IS 'Идентификатор элемента родительского элемента';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'placecode'))then
        COMMENT ON COLUMN public.fias_addrobj.placecode IS 'Код населенного пункта';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'plaincode'))then
        COMMENT ON COLUMN public.fias_addrobj.plaincode IS 'Код адресообразующего элемента из КЛАДР 4.0 одной строкой без признака актуальности (последних двух цифр)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'postalcode'))then
        COMMENT ON COLUMN public.fias_addrobj.postalcode IS 'Почтовый индекс';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'previd'))then
        COMMENT ON COLUMN public.fias_addrobj.previd IS 'Идентификатор записи связывания с предыдушей исторической записью';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'regioncode'))then
        COMMENT ON COLUMN public.fias_addrobj.regioncode IS 'Код региона';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'shortname'))then
        COMMENT ON COLUMN public.fias_addrobj.shortname IS 'Краткое наименование типа элемента';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'startdate'))then
        COMMENT ON COLUMN public.fias_addrobj.startdate IS 'Начало действия записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'streetcode'))then
        COMMENT ON COLUMN public.fias_addrobj.streetcode IS 'Код улицы';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'terrifnsfl'))then
        COMMENT ON COLUMN public.fias_addrobj.terrifnsfl IS 'Код территориального участка ИФНС ФЛ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'terrifnsul'))then
        COMMENT ON COLUMN public.fias_addrobj.terrifnsul IS 'Код территориального участка ИФНС ЮЛ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'updatedate'))then
        COMMENT ON COLUMN public.fias_addrobj.updatedate IS 'Дата  внесения (обновления) записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'ctarcode'))then
        COMMENT ON COLUMN public.fias_addrobj.ctarcode IS 'Код внутригородского района';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'extrcode'))then
        COMMENT ON COLUMN public.fias_addrobj.extrcode IS 'Код дополнительного адресообразующего элемента';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'sextcode'))then
        COMMENT ON COLUMN public.fias_addrobj.sextcode IS 'Код подчиненного дополнительного адресообразующего элемента';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'livestatus'))then
        COMMENT ON COLUMN public.fias_addrobj.livestatus IS 'Признак действующего адресообразующего элемента: 0 – недействующий адресный элемент, 1 - действующий';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'normdoc'))then
        COMMENT ON COLUMN public.fias_addrobj.normdoc IS 'Внешний ключ на нормативный документ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'plancode'))then
        COMMENT ON COLUMN public.fias_addrobj.plancode IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'cadnum'))then
        COMMENT ON COLUMN public.fias_addrobj.cadnum IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'divtype'))then
        COMMENT ON COLUMN public.fias_addrobj.divtype IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('fias_addrobj', 'emp_id'))then
        COMMENT ON COLUMN public.fias_addrobj.emp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'id'))then
        COMMENT ON COLUMN public.ehd_old_numbers.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'global_id'))then
        COMMENT ON COLUMN public.ehd_old_numbers.global_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'type'))then
        COMMENT ON COLUMN public.ehd_old_numbers.type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'number'))then
        COMMENT ON COLUMN public.ehd_old_numbers.number IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'date'))then
        COMMENT ON COLUMN public.ehd_old_numbers.date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'organ'))then
        COMMENT ON COLUMN public.ehd_old_numbers.organ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'register_id'))then
        COMMENT ON COLUMN public.ehd_old_numbers.register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'parcel_id'))then
        COMMENT ON COLUMN public.ehd_old_numbers.parcel_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ehd_old_numbers', 'load_date'))then
        COMMENT ON COLUMN public.ehd_old_numbers.load_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank', 'emp_id'))then
        COMMENT ON COLUMN public.insur_bank.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank', 'bank_name'))then
        COMMENT ON COLUMN public.insur_bank.bank_name IS 'Наименование банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank', 'date_input'))then
        COMMENT ON COLUMN public.insur_bank.date_input IS 'Дата создания';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank', 'inn'))then
        COMMENT ON COLUMN public.insur_bank.inn IS 'ИНН банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank', 'kpp'))then
        COMMENT ON COLUMN public.insur_bank.kpp IS 'КПП банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank', 'bic'))then
        COMMENT ON COLUMN public.insur_bank.bic IS 'БИК банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_bank', 'kor_acc'))then
        COMMENT ON COLUMN public.insur_bank.kor_acc IS 'Корреспондентский счет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'stat_date'))then
        COMMENT ON COLUMN public.system_daily_stat_db_obj.stat_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'segment_type'))then
        COMMENT ON COLUMN public.system_daily_stat_db_obj.segment_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'segment_name'))then
        COMMENT ON COLUMN public.system_daily_stat_db_obj.segment_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'table_name'))then
        COMMENT ON COLUMN public.system_daily_stat_db_obj.table_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'size_meg'))then
        COMMENT ON COLUMN public.system_daily_stat_db_obj.size_meg IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'registerid'))then
        COMMENT ON COLUMN public.core_register.registerid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'registername'))then
        COMMENT ON COLUMN public.core_register.registername IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'registerdescription'))then
        COMMENT ON COLUMN public.core_register.registerdescription IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'allpri_table'))then
        COMMENT ON COLUMN public.core_register.allpri_table IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'object_table'))then
        COMMENT ON COLUMN public.core_register.object_table IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'quant_table'))then
        COMMENT ON COLUMN public.core_register.quant_table IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'track_changes_column'))then
        COMMENT ON COLUMN public.core_register.track_changes_column IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'storage_type'))then
        COMMENT ON COLUMN public.core_register.storage_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'object_sequence'))then
        COMMENT ON COLUMN public.core_register.object_sequence IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'is_virtual'))then
        COMMENT ON COLUMN public.core_register.is_virtual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'contains_quant_in_future'))then
        COMMENT ON COLUMN public.core_register.contains_quant_in_future IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register', 'db_connection_name'))then
        COMMENT ON COLUMN public.core_register.db_connection_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_relation', 'id'))then
        COMMENT ON COLUMN public.core_register_relation.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_relation', 'name'))then
        COMMENT ON COLUMN public.core_register_relation.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_relation', 'parentregister'))then
        COMMENT ON COLUMN public.core_register_relation.parentregister IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_relation', 'chieldregister'))then
        COMMENT ON COLUMN public.core_register_relation.chieldregister IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_relation', 'cardinality'))then
        COMMENT ON COLUMN public.core_register_relation.cardinality IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_relation', 'kindid'))then
        COMMENT ON COLUMN public.core_register_relation.kindid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'id'))then
        COMMENT ON COLUMN public.core_register_attribute.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'name'))then
        COMMENT ON COLUMN public.core_register_attribute.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'registerid'))then
        COMMENT ON COLUMN public.core_register_attribute.registerid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'type'))then
        COMMENT ON COLUMN public.core_register_attribute.type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'parentid'))then
        COMMENT ON COLUMN public.core_register_attribute.parentid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'referenceid'))then
        COMMENT ON COLUMN public.core_register_attribute.referenceid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'value_field'))then
        COMMENT ON COLUMN public.core_register_attribute.value_field IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'code_field'))then
        COMMENT ON COLUMN public.core_register_attribute.code_field IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'value_template'))then
        COMMENT ON COLUMN public.core_register_attribute.value_template IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'primary_key'))then
        COMMENT ON COLUMN public.core_register_attribute.primary_key IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'user_key'))then
        COMMENT ON COLUMN public.core_register_attribute.user_key IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'qscolumn'))then
        COMMENT ON COLUMN public.core_register_attribute.qscolumn IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'internal_name'))then
        COMMENT ON COLUMN public.core_register_attribute.internal_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'is_nullable'))then
        COMMENT ON COLUMN public.core_register_attribute.is_nullable IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'description'))then
        COMMENT ON COLUMN public.core_register_attribute.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_register_attribute', 'layout'))then
        COMMENT ON COLUMN public.core_register_attribute.layout IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'emp_id'))then
        COMMENT ON COLUMN public.insur_changes_log.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'object_id'))then
        COMMENT ON COLUMN public.insur_changes_log.object_id IS 'ID-записи, в которую вносятся изменения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'reestr_id'))then
        COMMENT ON COLUMN public.insur_changes_log.reestr_id IS 'Номер реестра  для записи, в которую вносятся изменения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'load_date'))then
        COMMENT ON COLUMN public.insur_changes_log.load_date IS 'Дата внесения изменения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'operation_type'))then
        COMMENT ON COLUMN public.insur_changes_log.operation_type IS 'Тип операции';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'operation_type_code'))then
        COMMENT ON COLUMN public.insur_changes_log.operation_type_code IS 'Код типа операции';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'reason'))then
        COMMENT ON COLUMN public.insur_changes_log.reason IS 'Комментарий: причина изменения данных';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'user_id'))then
        COMMENT ON COLUMN public.insur_changes_log.user_id IS 'Идентификатор пользователя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'old_value'))then
        COMMENT ON COLUMN public.insur_changes_log.old_value IS 'Старое значение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_changes_log', 'new_value'))then
        COMMENT ON COLUMN public.insur_changes_log.new_value IS 'Новое значение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingslayout', 'id'))then
        COMMENT ON COLUMN public.core_srd_usersettingslayout.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingslayout', 'user_id'))then
        COMMENT ON COLUMN public.core_srd_usersettingslayout.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingslayout', 'layout_id'))then
        COMMENT ON COLUMN public.core_srd_usersettingslayout.layout_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingslayout', 'settings'))then
        COMMENT ON COLUMN public.core_srd_usersettingslayout.settings IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'id'))then
        COMMENT ON COLUMN public.import_log_insur_building.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'ehd_parcel_id'))then
        COMMENT ON COLUMN public.import_log_insur_building.ehd_parcel_id IS 'Идентификатор здания ЕГРН';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'bti_building_id'))then
        COMMENT ON COLUMN public.import_log_insur_building.bti_building_id IS 'Идентификатор здания БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'insur_building_id'))then
        COMMENT ON COLUMN public.import_log_insur_building.insur_building_id IS 'Идентификатор объекта страхования МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'date_loaded'))then
        COMMENT ON COLUMN public.import_log_insur_building.date_loaded IS 'Дата загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'error_message'))then
        COMMENT ON COLUMN public.import_log_insur_building.error_message IS 'Сообщение об ошибке';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'error_id'))then
        COMMENT ON COLUMN public.import_log_insur_building.error_id IS 'Идентификатор ошибки в журнале';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'is_error'))then
        COMMENT ON COLUMN public.import_log_insur_building.is_error IS 'Признак ошибки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'update_date_ehd'))then
        COMMENT ON COLUMN public.import_log_insur_building.update_date_ehd IS 'Дата обновления объекта в источнике данных ЕГРН';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'cad_num'))then
        COMMENT ON COLUMN public.import_log_insur_building.cad_num IS 'Кадастровый номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'unom'))then
        COMMENT ON COLUMN public.import_log_insur_building.unom IS 'УНОМ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'update_date_bti'))then
        COMMENT ON COLUMN public.import_log_insur_building.update_date_bti IS 'Дата обновления объекта в источнике данных БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_building', 'error_attempts_count'))then
        COMMENT ON COLUMN public.import_log_insur_building.error_attempts_count IS 'Количество неудачных попыток обновления объекта';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_type_building_floor_link', 'id'))then
        COMMENT ON COLUMN public.insur_type_building_floor_link.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_building'))then
        COMMENT ON COLUMN public.insur_type_building_floor_link.type_building IS 'Тип здания (справочник "Типы зданий", 12132)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_building_code'))then
        COMMENT ON COLUMN public.insur_type_building_floor_link.type_building_code IS 'Тип здания (справочник "Типы зданий", code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_building_structure'))then
        COMMENT ON COLUMN public.insur_type_building_floor_link.type_building_structure IS 'Тип конструкции строения (справочник "Тип конструкции строения", 12143)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_building_structure_code'))then
        COMMENT ON COLUMN public.insur_type_building_floor_link.type_building_structure_code IS 'Тип конструкции строения (справочник "Тип конструкции строения", code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_floors'))then
        COMMENT ON COLUMN public.insur_type_building_floor_link.type_floors IS 'Этажность строения (справочник "Этажность строения", 12144)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_floors_code'))then
        COMMENT ON COLUMN public.insur_type_building_floor_link.type_floors_code IS 'Этажность строения (справочник "Этажность строения", code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'id'))then
        COMMENT ON COLUMN public.insur_fsp_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'emp_id'))then
        COMMENT ON COLUMN public.insur_fsp_q.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'actual'))then
        COMMENT ON COLUMN public.insur_fsp_q.actual IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'status'))then
        COMMENT ON COLUMN public.insur_fsp_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 's_'))then
        COMMENT ON COLUMN public.insur_fsp_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'po_'))then
        COMMENT ON COLUMN public.insur_fsp_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'fsp_type'))then
        COMMENT ON COLUMN public.insur_fsp_q.fsp_type IS 'Тип ФСП';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'fsp_number'))then
        COMMENT ON COLUMN public.insur_fsp_q.fsp_number IS 'Номер  ФСП ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'ls'))then
        COMMENT ON COLUMN public.insur_fsp_q.ls IS 'Номер лицевого счета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'obj_id'))then
        COMMENT ON COLUMN public.insur_fsp_q.obj_id IS 'Идентификатор объекта (ссылка на INSUR_FLAT)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'obj_reestr_id'))then
        COMMENT ON COLUMN public.insur_fsp_q.obj_reestr_id IS 'Номер реестра объекта ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'contract_id'))then
        COMMENT ON COLUMN public.insur_fsp_q.contract_id IS 'Идентификатор договора ( полиса/свидетельства/договора страхования общего имущества)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'id_reestr_contr'))then
        COMMENT ON COLUMN public.insur_fsp_q.id_reestr_contr IS 'Номер реестра договоров';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'fsp_type_code'))then
        COMMENT ON COLUMN public.insur_fsp_q.fsp_type_code IS 'Тип фсп (код)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'date_open'))then
        COMMENT ON COLUMN public.insur_fsp_q.date_open IS 'Дата создания ФСП';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'kodpl'))then
        COMMENT ON COLUMN public.insur_fsp_q.kodpl IS 'Код плательщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_fsp_q', 'opl_kodpl'))then
        COMMENT ON COLUMN public.insur_fsp_q.opl_kodpl IS 'Площадь, подлежащая страхованию';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'id'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'ehd_parcel_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.ehd_parcel_id IS 'Идентификатор здания ЕГРН';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'bti_building_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.bti_building_id IS 'Идентификатор здания БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'insur_building_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.insur_building_id IS 'Идентификатор объекта страхования МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'date_loaded'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.date_loaded IS 'Дата загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'error_message'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.error_message IS 'Сообщение об ошибке';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'error_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.error_id IS 'Идентификатор ошибки в журнале';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'is_error'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.is_error IS 'Признак ошибки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'update_date_ehd'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.update_date_ehd IS 'Дата обновления объекта в источнике данных БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'update_date_bti'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.update_date_bti IS 'Дата обновления объекта в источнике данных ЕГРН';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'cad_num'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.cad_num IS 'Кадастровый номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'unom'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.unom IS 'УНОМ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat_b', 'error_attempts_count'))then
        COMMENT ON COLUMN public.import_log_insur_flat_b.error_attempts_count IS 'Количество неудачных попыток обновления объекта';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'bti_id'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.bti_id IS 'Идентификатор здания в источнике БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'num_cadnum'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.num_cadnum IS 'Кадастровый номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'unom'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.unom IS 'УНОМ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'is_new'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.is_new IS 'Признак нового';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'alt_building_id'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.alt_building_id IS 'Идентификатор импортированного объекта БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'dateedit'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.dateedit IS 'Дата редактирования объекта в БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'is_error'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.is_error IS 'Признак ошибки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'message'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.message IS 'Сообщение об ошибке';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'error_id'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.error_id IS 'Идентификатор ошибки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'task_id'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.task_id IS 'Идентификатор потока';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'insert_date'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.insert_date IS 'Дата вставки записи в журнал';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'import_date'))then
        COMMENT ON COLUMN public.cipjs_import_bti_daily_log.import_date IS 'Дата обработки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'emp_id'))then
        COMMENT ON COLUMN public.insur_file_process_log.emp_id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'input_file_id'))then
        COMMENT ON COLUMN public.insur_file_process_log.input_file_id IS 'Идентификатор файла для обработки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'total_count'))then
        COMMENT ON COLUMN public.insur_file_process_log.total_count IS 'Количество записей для обработки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'status'))then
        COMMENT ON COLUMN public.insur_file_process_log.status IS 'Статус обработки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'status_code'))then
        COMMENT ON COLUMN public.insur_file_process_log.status_code IS 'Код статуса обработки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'start_date'))then
        COMMENT ON COLUMN public.insur_file_process_log.start_date IS 'Дата начала процесса обработки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'end_date'))then
        COMMENT ON COLUMN public.insur_file_process_log.end_date IS 'Дата окончания процесса обработки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'processed_count'))then
        COMMENT ON COLUMN public.insur_file_process_log.processed_count IS 'Обработано записей';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'total_fsp_count'))then
        COMMENT ON COLUMN public.insur_file_process_log.total_fsp_count IS 'Количество фсп для перерасчета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_process_log', 'processed_fsp_count'))then
        COMMENT ON COLUMN public.insur_file_process_log.processed_fsp_count IS 'Количество рассчитанных ФСП';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'emp_id'))then
        COMMENT ON COLUMN public.insur_documents.emp_id IS 'Уникальный номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'doc_type_id'))then
        COMMENT ON COLUMN public.insur_documents.doc_type_id IS 'Вид документа-основания (выбор из справочника, справочник «Виды документов-оснований»)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'doc_is_have'))then
        COMMENT ON COLUMN public.insur_documents.doc_is_have IS 'Признак наличия документа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'obj_id'))then
        COMMENT ON COLUMN public.insur_documents.obj_id IS 'Ссылка на уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'reestr_id'))then
        COMMENT ON COLUMN public.insur_documents.reestr_id IS 'Номер реестра записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'doc_type_m'))then
        COMMENT ON COLUMN public.insur_documents.doc_type_m IS 'Тип (бумажная/электронная)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'doc_type_m_code'))then
        COMMENT ON COLUMN public.insur_documents.doc_type_m_code IS 'Тип (бумажная/электронная)(code, 12167)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'doc_number'))then
        COMMENT ON COLUMN public.insur_documents.doc_number IS 'Номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'doc_date'))then
        COMMENT ON COLUMN public.insur_documents.doc_date IS 'Дата';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'doc_org_id'))then
        COMMENT ON COLUMN public.insur_documents.doc_org_id IS 'Организация  (выбор из справочника, справочник  «Страховые организации)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'file_storage_id'))then
        COMMENT ON COLUMN public.insur_documents.file_storage_id IS 'Ссылка на хранилище документов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'user_id'))then
        COMMENT ON COLUMN public.insur_documents.user_id IS 'Пользователь, создавший запись';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'date_create'))then
        COMMENT ON COLUMN public.insur_documents.date_create IS 'Дата ввода';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_documents', 'fio_scan'))then
        COMMENT ON COLUMN public.insur_documents.fio_scan IS 'ФИО сотрудника загрузившего документ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'emp_id'))then
        COMMENT ON COLUMN public.insur_agreement_project.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'got_user_id'))then
        COMMENT ON COLUMN public.insur_agreement_project.got_user_id IS 'Пользователь, который получил проект договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'got_date'))then
        COMMENT ON COLUMN public.insur_agreement_project.got_date IS 'Дата получения проекта договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'approval_user_id'))then
        COMMENT ON COLUMN public.insur_agreement_project.approval_user_id IS 'Пользователь, который согласовал проект договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'approval_date'))then
        COMMENT ON COLUMN public.insur_agreement_project.approval_date IS 'Дата согласования проекта договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'note'))then
        COMMENT ON COLUMN public.insur_agreement_project.note IS 'Примечание';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'calculation_id'))then
        COMMENT ON COLUMN public.insur_agreement_project.calculation_id IS 'Идентификатор расчета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'comment_spravka'))then
        COMMENT ON COLUMN public.insur_agreement_project.comment_spravka IS 'Для отчета Справка, пункт Замечания по пакету документов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'resume_spravka'))then
        COMMENT ON COLUMN public.insur_agreement_project.resume_spravka IS 'Для отчета Справка, пункт Принятое решение с учетом замечаний';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'part_moscow'))then
        COMMENT ON COLUMN public.insur_agreement_project.part_moscow IS 'Доля города Москвы, %';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'kat_1'))then
        COMMENT ON COLUMN public.insur_agreement_project.kat_1 IS 'Использовать расчет для первой категории';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'kat_2'))then
        COMMENT ON COLUMN public.insur_agreement_project.kat_2 IS 'Использовать расчет для второй категории';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'kat_3'))then
        COMMENT ON COLUMN public.insur_agreement_project.kat_3 IS 'Использовать расчет для третьей категории';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'progect_num'))then
        COMMENT ON COLUMN public.insur_agreement_project.progect_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_agreement_project', 'size_bonus_mkd'))then
        COMMENT ON COLUMN public.insur_agreement_project.size_bonus_mkd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_doc_base_type', 'id'))then
        COMMENT ON COLUMN public.insur_doc_base_type.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_doc_base_type', 'document_base'))then
        COMMENT ON COLUMN public.insur_doc_base_type.document_base IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_doc_base_type', 'type'))then
        COMMENT ON COLUMN public.insur_doc_base_type.type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_doc_base_type', 'ordinal'))then
        COMMENT ON COLUMN public.insur_doc_base_type.ordinal IS 'Порядок для сортировки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_doc_base_type', 'need_set_date'))then
        COMMENT ON COLUMN public.insur_doc_base_type.need_set_date IS 'Признак, что дата обязательная для заполнения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'okrug_id'))then
        COMMENT ON COLUMN public.ref_addr_okrug.okrug_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'subject_rf_id'))then
        COMMENT ON COLUMN public.ref_addr_okrug.subject_rf_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'full_name'))then
        COMMENT ON COLUMN public.ref_addr_okrug.full_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'short_name'))then
        COMMENT ON COLUMN public.ref_addr_okrug.short_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'name_for_sort'))then
        COMMENT ON COLUMN public.ref_addr_okrug.name_for_sort IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'steks_code'))then
        COMMENT ON COLUMN public.ref_addr_okrug.steks_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'omk_code'))then
        COMMENT ON COLUMN public.ref_addr_okrug.omk_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'name'))then
        COMMENT ON COLUMN public.ref_addr_okrug.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'type_ref'))then
        COMMENT ON COLUMN public.ref_addr_okrug.type_ref IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'code_givc'))then
        COMMENT ON COLUMN public.ref_addr_okrug.code_givc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('ref_addr_okrug', 'insurance_company_id'))then
        COMMENT ON COLUMN public.ref_addr_okrug.insurance_company_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'id'))then
        COMMENT ON COLUMN public.insur_flat_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'emp_id'))then
        COMMENT ON COLUMN public.insur_flat_q.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'actual'))then
        COMMENT ON COLUMN public.insur_flat_q.actual IS '1/0';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'status'))then
        COMMENT ON COLUMN public.insur_flat_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 's_'))then
        COMMENT ON COLUMN public.insur_flat_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'po_'))then
        COMMENT ON COLUMN public.insur_flat_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'load_date'))then
        COMMENT ON COLUMN public.insur_flat_q.load_date IS 'Дата загрузки данных';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'cadastr_num'))then
        COMMENT ON COLUMN public.insur_flat_q.cadastr_num IS 'Кадастровый номер МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'unom'))then
        COMMENT ON COLUMN public.insur_flat_q.unom IS 'УНОМ МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'kvnom'))then
        COMMENT ON COLUMN public.insur_flat_q.kvnom IS 'Номер квартиры';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'flatstatus'))then
        COMMENT ON COLUMN public.insur_flat_q.flatstatus IS 'Статус жилого помещения на основании справочника «Статус жилого помещения »';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'prkom'))then
        COMMENT ON COLUMN public.insur_flat_q.prkom IS 'Признак коммунальности квартиры ( 0-отдельная квартира,1 коммунальная квартира, 2- квартира в долевой  собственности)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'kol_gp'))then
        COMMENT ON COLUMN public.insur_flat_q.kol_gp IS 'Кол-во комнат в квартире';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'fopl'))then
        COMMENT ON COLUMN public.insur_flat_q.fopl IS 'Площадь жилых помещений';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'ppl'))then
        COMMENT ON COLUMN public.insur_flat_q.ppl IS 'Площадь с летними';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'gpl'))then
        COMMENT ON COLUMN public.insur_flat_q.gpl IS 'Площадь жилая';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'guid_fias_flat'))then
        COMMENT ON COLUMN public.insur_flat_q.guid_fias_flat IS 'GUID-ссылка в справочнике ФИАС на адрес квартиру';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'guid_fias_mkd'))then
        COMMENT ON COLUMN public.insur_flat_q.guid_fias_mkd IS 'GUID-ссылка в справочнике ФИАС на адрес МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'link_bti_flat'))then
        COMMENT ON COLUMN public.insur_flat_q.link_bti_flat IS 'Ссылка на Реестр связей с объектами БТИ  ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'link_insur_egrn'))then
        COMMENT ON COLUMN public.insur_flat_q.link_insur_egrn IS 'Ссылка на Реестр зданий  в Росреестре';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'link_object_mkd'))then
        COMMENT ON COLUMN public.insur_flat_q.link_object_mkd IS 'Ссылка на INSUR_BUILDING ( объекты страхования – МКД)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'source_atrib'))then
        COMMENT ON COLUMN public.insur_flat_q.source_atrib IS 'Источник заполнения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'flag_insur'))then
        COMMENT ON COLUMN public.insur_flat_q.flag_insur IS '1/0 признак участия в программе страхования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'address_fias_mkd'))then
        COMMENT ON COLUMN public.insur_flat_q.address_fias_mkd IS 'Адрес МКД в ФИАС';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'type_flat'))then
        COMMENT ON COLUMN public.insur_flat_q.type_flat IS 'Тип помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'type_flat_code'))then
        COMMENT ON COLUMN public.insur_flat_q.type_flat_code IS 'Тип помещения (Code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'type_flat_2'))then
        COMMENT ON COLUMN public.insur_flat_q.type_flat_2 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'type_flat_2_code'))then
        COMMENT ON COLUMN public.insur_flat_q.type_flat_2_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'cadastr_date'))then
        COMMENT ON COLUMN public.insur_flat_q.cadastr_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'status_egrn'))then
        COMMENT ON COLUMN public.insur_flat_q.status_egrn IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'status_egrn_code'))then
        COMMENT ON COLUMN public.insur_flat_q.status_egrn_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'opl'))then
        COMMENT ON COLUMN public.insur_flat_q.opl IS 'Общая площадь помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'cadastr_remove'))then
        COMMENT ON COLUMN public.insur_flat_q.cadastr_remove IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'dept_id'))then
        COMMENT ON COLUMN public.insur_flat_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'klass_flat_code'))then
        COMMENT ON COLUMN public.insur_flat_q.klass_flat_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'klass_flat'))then
        COMMENT ON COLUMN public.insur_flat_q.klass_flat IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'code_kladr'))then
        COMMENT ON COLUMN public.insur_flat_q.code_kladr IS 'Код КЛАДР';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'source_input'))then
        COMMENT ON COLUMN public.insur_flat_q.source_input IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'source_input_code'))then
        COMMENT ON COLUMN public.insur_flat_q.source_input_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_flat_q', 'note'))then
        COMMENT ON COLUMN public.insur_flat_q.note IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'id'))then
        COMMENT ON COLUMN public.system_daily_statistics.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'stat_date'))then
        COMMENT ON COLUMN public.system_daily_statistics.stat_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'db_size'))then
        COMMENT ON COLUMN public.system_daily_statistics.db_size IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'errors'))then
        COMMENT ON COLUMN public.system_daily_statistics.errors IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'warnings'))then
        COMMENT ON COLUMN public.system_daily_statistics.warnings IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'actions'))then
        COMMENT ON COLUMN public.system_daily_statistics.actions IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'sessions'))then
        COMMENT ON COLUMN public.system_daily_statistics.sessions IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'changes'))then
        COMMENT ON COLUMN public.system_daily_statistics.changes IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'diagnostics_slow'))then
        COMMENT ON COLUMN public.system_daily_statistics.diagnostics_slow IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'bti_objects_loaded'))then
        COMMENT ON COLUMN public.system_daily_statistics.bti_objects_loaded IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'bti_objects_loaded_error'))then
        COMMENT ON COLUMN public.system_daily_statistics.bti_objects_loaded_error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'ehd_objects_loaded'))then
        COMMENT ON COLUMN public.system_daily_statistics.ehd_objects_loaded IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'ehd_objects_loaded_error'))then
        COMMENT ON COLUMN public.system_daily_statistics.ehd_objects_loaded_error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'insur_building_loaded'))then
        COMMENT ON COLUMN public.system_daily_statistics.insur_building_loaded IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'insur_building_loaded_error'))then
        COMMENT ON COLUMN public.system_daily_statistics.insur_building_loaded_error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'insur_flat_loaded'))then
        COMMENT ON COLUMN public.system_daily_statistics.insur_flat_loaded IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'insur_flat_loaded_error'))then
        COMMENT ON COLUMN public.system_daily_statistics.insur_flat_loaded_error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'total_count_insur_building'))then
        COMMENT ON COLUMN public.system_daily_statistics.total_count_insur_building IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'total_count_insur_flat'))then
        COMMENT ON COLUMN public.system_daily_statistics.total_count_insur_flat IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'long_proc_run'))then
        COMMENT ON COLUMN public.system_daily_statistics.long_proc_run IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'long_proc_run_error'))then
        COMMENT ON COLUMN public.system_daily_statistics.long_proc_run_error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'long_proc_insur_load_run'))then
        COMMENT ON COLUMN public.system_daily_statistics.long_proc_insur_load_run IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_statistics', 'long_proc_insur_load_run_error'))then
        COMMENT ON COLUMN public.system_daily_statistics.long_proc_insur_load_run_error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_district', 'id'))then
        COMMENT ON COLUMN public.insur_district.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_district', 'code'))then
        COMMENT ON COLUMN public.insur_district.code IS 'Код';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_district', 'name'))then
        COMMENT ON COLUMN public.insur_district.name IS 'Наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_district', 'okrug_id'))then
        COMMENT ON COLUMN public.insur_district.okrug_id IS 'Идентификатор округа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_district', 'ref_addr_district_id'))then
        COMMENT ON COLUMN public.insur_district.ref_addr_district_id IS 'Ссылка на район в БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_district', 'ref_addr_district_code_givc'))then
        COMMENT ON COLUMN public.insur_district.ref_addr_district_code_givc IS 'Код района в БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_okrug', 'id'))then
        COMMENT ON COLUMN public.insur_okrug.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_okrug', 'code'))then
        COMMENT ON COLUMN public.insur_okrug.code IS 'Код';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_okrug', 'name'))then
        COMMENT ON COLUMN public.insur_okrug.name IS 'Наименование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_okrug', 'short_name'))then
        COMMENT ON COLUMN public.insur_okrug.short_name IS 'Сокращенное название';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_okrug', 'insurance_company_id'))then
        COMMENT ON COLUMN public.insur_okrug.insurance_company_id IS 'Идентификатор страховой компании';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_okrug', 'ref_addr_okrug_id'))then
        COMMENT ON COLUMN public.insur_okrug.ref_addr_okrug_id IS 'Ссылка на округ в БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_okrug', 'ref_addr_okrug_code_givc'))then
        COMMENT ON COLUMN public.insur_okrug.ref_addr_okrug_code_givc IS 'Код округа в БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'emp_id'))then
        COMMENT ON COLUMN public.insur_building_q.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 's_'))then
        COMMENT ON COLUMN public.insur_building_q.s_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'po_'))then
        COMMENT ON COLUMN public.insur_building_q.po_ IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'actual'))then
        COMMENT ON COLUMN public.insur_building_q.actual IS '1/0';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'load_date'))then
        COMMENT ON COLUMN public.insur_building_q.load_date IS 'Дата загрузки данных';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'cadastr_num'))then
        COMMENT ON COLUMN public.insur_building_q.cadastr_num IS 'Кадастровый номер МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'status_egrn'))then
        COMMENT ON COLUMN public.insur_building_q.status_egrn IS 'Ссылка на номер из справочника «Статус объекта ЕГРН»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'status_sost_bti'))then
        COMMENT ON COLUMN public.insur_building_q.status_sost_bti IS 'Ссылка на номер из справочника «Статус состояния БТИ»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'cadastr_date'))then
        COMMENT ON COLUMN public.insur_building_q.cadastr_date IS 'Дата постановки на кадастровый учет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'okrug_id'))then
        COMMENT ON COLUMN public.insur_building_q.okrug_id IS 'Идентификатор округа из справочника округов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'district_id'))then
        COMMENT ON COLUMN public.insur_building_q.district_id IS 'Идентификатор района из справочника районов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'unom'))then
        COMMENT ON COLUMN public.insur_building_q.unom IS 'УНОМ МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'type_mkd_code'))then
        COMMENT ON COLUMN public.insur_building_q.type_mkd_code IS 'Код типа дома из справочника «Статус дома ГБУ» ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'year_stroi'))then
        COMMENT ON COLUMN public.insur_building_q.year_stroi IS 'Год постройки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'count_floor'))then
        COMMENT ON COLUMN public.insur_building_q.count_floor IS 'Кол-во этажей';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'kol_gp'))then
        COMMENT ON COLUMN public.insur_building_q.kol_gp IS 'Кол-во квартир в доме';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'opl'))then
        COMMENT ON COLUMN public.insur_building_q.opl IS 'Общая площадь (  OPL  в БТИ= AREA в Росреестре)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'opl_g'))then
        COMMENT ON COLUMN public.insur_building_q.opl_g IS 'Площадь жилых помещений';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'opl_n'))then
        COMMENT ON COLUMN public.insur_building_q.opl_n IS 'Площадь нежилых помещений';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'bpl'))then
        COMMENT ON COLUMN public.insur_building_q.bpl IS 'Площадь балконов';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'hpl'))then
        COMMENT ON COLUMN public.insur_building_q.hpl IS 'Площадь холодных помещений';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'lpl'))then
        COMMENT ON COLUMN public.insur_building_q.lpl IS 'Площадь лоджий';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'lfpq'))then
        COMMENT ON COLUMN public.insur_building_q.lfpq IS 'Кол-во лифтов пассажирских';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'lfgpq'))then
        COMMENT ON COLUMN public.insur_building_q.lfgpq IS 'Кол-во лифтов грузопассажирских';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'lfgq'))then
        COMMENT ON COLUMN public.insur_building_q.lfgq IS 'Кол-во лифтов грузовых';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'guid_fias_mkd'))then
        COMMENT ON COLUMN public.insur_building_q.guid_fias_mkd IS 'GUID-ссылка в справочнике ФИАС на адрес МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'link_bti_fsks'))then
        COMMENT ON COLUMN public.insur_building_q.link_bti_fsks IS 'Ссылка на Реестр связей с объектами БТИ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'link_egrn_bild'))then
        COMMENT ON COLUMN public.insur_building_q.link_egrn_bild IS 'Ссылка на Реестр зданий  в Росреестре';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'source_atrib'))then
        COMMENT ON COLUMN public.insur_building_q.source_atrib IS 'Источник заполнения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'flag_insur'))then
        COMMENT ON COLUMN public.insur_building_q.flag_insur IS '1/0 признак участия в программе страхования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'type_mkd'))then
        COMMENT ON COLUMN public.insur_building_q.type_mkd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'purpose_name'))then
        COMMENT ON COLUMN public.insur_building_q.purpose_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'purpose_name_code'))then
        COMMENT ON COLUMN public.insur_building_q.purpose_name_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'status_egrn_code'))then
        COMMENT ON COLUMN public.insur_building_q.status_egrn_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'krovpl'))then
        COMMENT ON COLUMN public.insur_building_q.krovpl IS 'Площадь кровли';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'stroi_price'))then
        COMMENT ON COLUMN public.insur_building_q.stroi_price IS 'Строительная стоимость';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'status_sost_bti_code'))then
        COMMENT ON COLUMN public.insur_building_q.status_sost_bti_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'address_id'))then
        COMMENT ON COLUMN public.insur_building_q.address_id IS 'Ссылка на адрес МКД (INSUR_ADDRESS.EMP_ID)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'cadastr_remove'))then
        COMMENT ON COLUMN public.insur_building_q.cadastr_remove IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'id'))then
        COMMENT ON COLUMN public.insur_building_q.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'status'))then
        COMMENT ON COLUMN public.insur_building_q.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'dept_id'))then
        COMMENT ON COLUMN public.insur_building_q.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'code_kladr'))then
        COMMENT ON COLUMN public.insur_building_q.code_kladr IS 'Код КЛАДР';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'epl'))then
        COMMENT ON COLUMN public.insur_building_q.epl IS 'Площадь помещений, не входящих в общую зону, кв.м.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'pizn'))then
        COMMENT ON COLUMN public.insur_building_q.pizn IS 'Процент износа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'source_input'))then
        COMMENT ON COLUMN public.insur_building_q.source_input IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'source_input_code'))then
        COMMENT ON COLUMN public.insur_building_q.source_input_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'note'))then
        COMMENT ON COLUMN public.insur_building_q.note IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_building_q', 'flag_insur_calculated'))then
        COMMENT ON COLUMN public.insur_building_q.flag_insur_calculated IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'emp_id'))then
        COMMENT ON COLUMN public.insur_balance.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'fsp_id'))then
        COMMENT ON COLUMN public.insur_balance.fsp_id IS 'Ссылка на ФСП INSUR_FSP_Q.EMP_ID';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'flag_opl'))then
        COMMENT ON COLUMN public.insur_balance.flag_opl IS '1/0 (Оплачено/не оплачено начисление)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'link_input_nach'))then
        COMMENT ON COLUMN public.insur_balance.link_input_nach IS 'Ссылка на запись по начислению в INSUR_INPUT_NACH';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'flag_insur'))then
        COMMENT ON COLUMN public.insur_balance.flag_insur IS '1/0 (Застрахован период/Не застрахован)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'ostatok_sum'))then
        COMMENT ON COLUMN public.insur_balance.ostatok_sum IS 'Нераспределенный остаток на начало периода';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_balance.period_reg_date IS 'Период учета данных в Системе';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'sum_opl'))then
        COMMENT ON COLUMN public.insur_balance.sum_opl IS 'Сумма зачислений, нарастающим итогом';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'sum_nach_mfc'))then
        COMMENT ON COLUMN public.insur_balance.sum_nach_mfc IS 'Сумма начислений  МФЦ в периоде, нарастающим итогом';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'sum_nach_gby'))then
        COMMENT ON COLUMN public.insur_balance.sum_nach_gby IS 'Сумма начислений  ГБУ в периоде, нарастающим итогом';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'sum_nach_opl'))then
        COMMENT ON COLUMN public.insur_balance.sum_nach_opl IS 'Сумма оплаченных начислений  в периоде, нарастающим итогом';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_balance', 'strah_end'))then
        COMMENT ON COLUMN public.insur_balance.strah_end IS 'Последний застрахованный период';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'emp_id'))then
        COMMENT ON COLUMN public.insur_reestr_pay.emp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'num'))then
        COMMENT ON COLUMN public.insur_reestr_pay.num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'date'))then
        COMMENT ON COLUMN public.insur_reestr_pay.date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'data_creation'))then
        COMMENT ON COLUMN public.insur_reestr_pay.data_creation IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'data_payment'))then
        COMMENT ON COLUMN public.insur_reestr_pay.data_payment IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'user_creation'))then
        COMMENT ON COLUMN public.insur_reestr_pay.user_creation IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'user_payment'))then
        COMMENT ON COLUMN public.insur_reestr_pay.user_payment IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'status'))then
        COMMENT ON COLUMN public.insur_reestr_pay.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'status_code'))then
        COMMENT ON COLUMN public.insur_reestr_pay.status_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'type'))then
        COMMENT ON COLUMN public.insur_reestr_pay.type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'type_code'))then
        COMMENT ON COLUMN public.insur_reestr_pay.type_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'note'))then
        COMMENT ON COLUMN public.insur_reestr_pay.note IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'file_storage_id_dgi'))then
        COMMENT ON COLUMN public.insur_reestr_pay.file_storage_id_dgi IS 'Ссылка на файл для статуса "Утвержден в ДГИ"';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'file_storage_id_pay'))then
        COMMENT ON COLUMN public.insur_reestr_pay.file_storage_id_pay IS 'Ссылка на файл для статуса "Передано в оплату"';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'date_cancel'))then
        COMMENT ON COLUMN public.insur_reestr_pay.date_cancel IS 'Дата расформирования реестра';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_reestr_pay', 'cancel_user_id'))then
        COMMENT ON COLUMN public.insur_reestr_pay.cancel_user_id IS 'Пользователь, расформировавший реестр';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'emp_id'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.emp_id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'input_file_id'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.input_file_id IS 'Идентификатор загруженного файла';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'plat_count'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.plat_count IS 'Количество обрабатываемых записей';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'status'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.status IS 'Статус идентификаци';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'status_code'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.status_code IS 'Код статуса идентифкации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'start_date'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.start_date IS 'Дата начала процесса идентификации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'end_date'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.end_date IS 'Дата завершения процесса идентификации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'identified_count'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.identified_count IS 'Количество идентифицированных записей';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'not_identified_count'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.not_identified_count IS 'Количество неидентифицированных записей';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'need_process'))then
        COMMENT ON COLUMN public.insur_file_plat_identify_log.need_process IS 'Запускать процесс обработки после идентифкации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'emp_id'))then
        COMMENT ON COLUMN public.insur_all_property.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'fsp_id'))then
        COMMENT ON COLUMN public.insur_all_property.fsp_id IS 'Ссылка на ФСП INSUR_FSP_Q.EMP_ID';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'insurance_id'))then
        COMMENT ON COLUMN public.insur_all_property.insurance_id IS 'Код страховой организации – по справочнику «Страховые организации»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'okrug_id'))then
        COMMENT ON COLUMN public.insur_all_property.okrug_id IS 'Код административного округа ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'unom'))then
        COMMENT ON COLUMN public.insur_all_property.unom IS 'Уникальный номер строения ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'subject_id'))then
        COMMENT ON COLUMN public.insur_all_property.subject_id IS 'Код управляющей компании (по справочнику «Управляющие компании»)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'name'))then
        COMMENT ON COLUMN public.insur_all_property.name IS 'Наименование страхователя (ЖСК, ЖК, ТСЖ, управляющей компании)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'ndog'))then
        COMMENT ON COLUMN public.insur_all_property.ndog IS 'Уникальный номер договора страхования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'ndogdat'))then
        COMMENT ON COLUMN public.insur_all_property.ndogdat IS 'Дата начала действия договора страхования ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'st1'))then
        COMMENT ON COLUMN public.insur_all_property.st1 IS 'Страховая стоимость конструктивных элементов и помещений общего пользования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'st2'))then
        COMMENT ON COLUMN public.insur_all_property.st2 IS 'Страховая стоимость внеквартирного инженерного оборудования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'st3'))then
        COMMENT ON COLUMN public.insur_all_property.st3 IS 'Страховая стоимость лифтового оборудования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'ss1'))then
        COMMENT ON COLUMN public.insur_all_property.ss1 IS 'Страховая сумма конструктивных элементов и помещений общего пользования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'ss2'))then
        COMMENT ON COLUMN public.insur_all_property.ss2 IS 'Страховая сумма внеквартирного инженерного оборудования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'ss3'))then
        COMMENT ON COLUMN public.insur_all_property.ss3 IS 'Страховая сумма лифтового оборудования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'part'))then
        COMMENT ON COLUMN public.insur_all_property.part IS 'Доля ответственности страховой организации в возмещении ущерба';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'part_city'))then
        COMMENT ON COLUMN public.insur_all_property.part_city IS 'Доля города Москвы в праве на общее имущество';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'ras_pripay'))then
        COMMENT ON COLUMN public.insur_all_property.ras_pripay IS '	Рассчитанный размер страховой премии';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'link_id_file'))then
        COMMENT ON COLUMN public.insur_all_property.link_id_file IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'obj_id'))then
        COMMENT ON COLUMN public.insur_all_property.obj_id IS 'Идентификатор объекта (ссылка на INSUR_BUILDING)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'org_type'))then
        COMMENT ON COLUMN public.insur_all_property.org_type IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'org_type_code'))then
        COMMENT ON COLUMN public.insur_all_property.org_type_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'paysign'))then
        COMMENT ON COLUMN public.insur_all_property.paysign IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'paysign_code'))then
        COMMENT ON COLUMN public.insur_all_property.paysign_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'status'))then
        COMMENT ON COLUMN public.insur_all_property.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'status_code'))then
        COMMENT ON COLUMN public.insur_all_property.status_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_all_property', 'org_id_file'))then
        COMMENT ON COLUMN public.insur_all_property.org_id_file IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'emp_id'))then
        COMMENT ON COLUMN public.insur_damage_amount.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'damage_id'))then
        COMMENT ON COLUMN public.insur_damage_amount.damage_id IS 'Ссылка на дело (INSUR_DAMAGE)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'damage_assessment_method_id'))then
        COMMENT ON COLUMN public.insur_damage_amount.damage_assessment_method_id IS 'Ссылка на справочник "Методики расчета ущерба"';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'material_damage'))then
        COMMENT ON COLUMN public.insur_damage_amount.material_damage IS 'Материальный ущерб';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'proportion_replacement_cost'))then
        COMMENT ON COLUMN public.insur_damage_amount.proportion_replacement_cost IS 'Удельный вес восстановительной стоимости';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'proportion_damaged_area'))then
        COMMENT ON COLUMN public.insur_damage_amount.proportion_damaged_area IS 'Удельный вес поврежденного участка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'damage_amount'))then
        COMMENT ON COLUMN public.insur_damage_amount.damage_amount IS 'Сумма ущерба';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'element_construction'))then
        COMMENT ON COLUMN public.insur_damage_amount.element_construction IS 'Элемент конструкции';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'element_construction_code'))then
        COMMENT ON COLUMN public.insur_damage_amount.element_construction_code IS 'Элемент конструкции (code, 12126)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_amount', 'correction'))then
        COMMENT ON COLUMN public.insur_damage_amount.correction IS 'Поправочный коэффициент';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'id'))then
        COMMENT ON COLUMN public.system_daily_stat_file_stor.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'stat_date'))then
        COMMENT ON COLUMN public.system_daily_stat_file_stor.stat_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'file_key'))then
        COMMENT ON COLUMN public.system_daily_stat_file_stor.file_key IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'description'))then
        COMMENT ON COLUMN public.system_daily_stat_file_stor.description IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'size_mb'))then
        COMMENT ON COLUMN public.system_daily_stat_file_stor.size_mb IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingsreport', 'id'))then
        COMMENT ON COLUMN public.core_srd_usersettingsreport.id IS 'Идентификатор';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingsreport', 'user_id'))then
        COMMENT ON COLUMN public.core_srd_usersettingsreport.user_id IS 'Идентифиатор пользователя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingsreport', 'report_id'))then
        COMMENT ON COLUMN public.core_srd_usersettingsreport.report_id IS 'Идентификатор отчета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettingsreport', 'settings'))then
        COMMENT ON COLUMN public.core_srd_usersettingsreport.settings IS 'Параметры отчета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'id'))then
        COMMENT ON COLUMN public.core_long_process_queue.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'user_id'))then
        COMMENT ON COLUMN public.core_long_process_queue.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'process_type_id'))then
        COMMENT ON COLUMN public.core_long_process_queue.process_type_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'object_register_id'))then
        COMMENT ON COLUMN public.core_long_process_queue.object_register_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'object_id'))then
        COMMENT ON COLUMN public.core_long_process_queue.object_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'create_date'))then
        COMMENT ON COLUMN public.core_long_process_queue.create_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'start_date'))then
        COMMENT ON COLUMN public.core_long_process_queue.start_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'end_date'))then
        COMMENT ON COLUMN public.core_long_process_queue.end_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'status'))then
        COMMENT ON COLUMN public.core_long_process_queue.status IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'last_check_date'))then
        COMMENT ON COLUMN public.core_long_process_queue.last_check_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'error_id'))then
        COMMENT ON COLUMN public.core_long_process_queue.error_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'message'))then
        COMMENT ON COLUMN public.core_long_process_queue.message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'service_log_id'))then
        COMMENT ON COLUMN public.core_long_process_queue.service_log_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'log'))then
        COMMENT ON COLUMN public.core_long_process_queue.log IS 'Журнал (состяние процесса)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_long_process_queue', 'progress'))then
        COMMENT ON COLUMN public.core_long_process_queue.progress IS 'Прогресс выполнение 0..100';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'emp_id'))then
        COMMENT ON COLUMN public.insur_invoice.emp_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'subject_id'))then
        COMMENT ON COLUMN public.insur_invoice.subject_id IS 'Ссылка на субъект-получатель';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'subject_name'))then
        COMMENT ON COLUMN public.insur_invoice.subject_name IS 'Имя получателя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'phone'))then
        COMMENT ON COLUMN public.insur_invoice.phone IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'data_input'))then
        COMMENT ON COLUMN public.insur_invoice.data_input IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'inn'))then
        COMMENT ON COLUMN public.insur_invoice.inn IS 'ИНН получателя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'kpp'))then
        COMMENT ON COLUMN public.insur_invoice.kpp IS 'КПП получателя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'rach_acc'))then
        COMMENT ON COLUMN public.insur_invoice.rach_acc IS 'Расчетный счет получателя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'num_card'))then
        COMMENT ON COLUMN public.insur_invoice.num_card IS 'Номер бакновской карточки получателя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'link_damage'))then
        COMMENT ON COLUMN public.insur_invoice.link_damage IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'link_all_property'))then
        COMMENT ON COLUMN public.insur_invoice.link_all_property IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'link_fsp'))then
        COMMENT ON COLUMN public.insur_invoice.link_fsp IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'link_reestr_pay'))then
        COMMENT ON COLUMN public.insur_invoice.link_reestr_pay IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'sum_opl'))then
        COMMENT ON COLUMN public.insur_invoice.sum_opl IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'status_name'))then
        COMMENT ON COLUMN public.insur_invoice.status_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'status_code'))then
        COMMENT ON COLUMN public.insur_invoice.status_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'user_id'))then
        COMMENT ON COLUMN public.insur_invoice.user_id IS 'Пользователь, создавший счет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'comment'))then
        COMMENT ON COLUMN public.insur_invoice.comment IS 'Комментарий';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'note_no_pay_id'))then
        COMMENT ON COLUMN public.insur_invoice.note_no_pay_id IS 'Причина отказа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'bank_id'))then
        COMMENT ON COLUMN public.insur_invoice.bank_id IS 'Ссылка на банк';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'bank_name'))then
        COMMENT ON COLUMN public.insur_invoice.bank_name IS 'Название банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'inn_bank'))then
        COMMENT ON COLUMN public.insur_invoice.inn_bank IS 'ИНН банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'kpp_bank'))then
        COMMENT ON COLUMN public.insur_invoice.kpp_bank IS 'КПП банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'bic_bank'))then
        COMMENT ON COLUMN public.insur_invoice.bic_bank IS 'БИК банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'kor_acc'))then
        COMMENT ON COLUMN public.insur_invoice.kor_acc IS 'Корреспондентский счет банка';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'date_agree'))then
        COMMENT ON COLUMN public.insur_invoice.date_agree IS 'Дата согласования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'user_agree_id'))then
        COMMENT ON COLUMN public.insur_invoice.user_agree_id IS 'Пользователь, согласовавший счет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'num_invoice'))then
        COMMENT ON COLUMN public.insur_invoice.num_invoice IS 'Номер счета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'date_invoice'))then
        COMMENT ON COLUMN public.insur_invoice.date_invoice IS 'Дата счета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'contract_id'))then
        COMMENT ON COLUMN public.insur_invoice.contract_id IS 'Ссылка на номер договора или в INSUR_POLICY_SVD или INSUR_ALL_PROPERTY';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'reestr_contract_id'))then
        COMMENT ON COLUMN public.insur_invoice.reestr_contract_id IS 'Номер реестра или INSUR_POLICY_SVD=309, INSUR_ALL_PROPERTY= ?-';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'part_dog'))then
        COMMENT ON COLUMN public.insur_invoice.part_dog IS 'Доля в праве';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'svid_polyc_num'))then
        COMMENT ON COLUMN public.insur_invoice.svid_polyc_num IS 'Свидетельство/Полис';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'svd_polyce_date'))then
        COMMENT ON COLUMN public.insur_invoice.svd_polyce_date IS 'Свидетельство/Полис дата';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_invoice', 'sort'))then
        COMMENT ON COLUMN public.insur_invoice.sort IS 'Сортировка для Заключения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'id'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'damage_symptom'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.damage_symptom IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'material_damage'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.material_damage IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'work_composition'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.work_composition IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'element_construction_description'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.element_construction_description IS 'Элемент конструкции';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'quantification'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.quantification IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'element_construction'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.element_construction IS 'Элемент конструкции (справочник)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'element_construction_code'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.element_construction_code IS 'Элемент конструкции (code, 12126)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'material_damage_min'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.material_damage_min IS 'Минимальный процент урона';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'material_damage_max'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.material_damage_max IS 'Максимальный процент урона';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'ref_id'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.ref_id IS 'Ссылка на реестр справочников';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage_assessment_method', 'ref_item_id'))then
        COMMENT ON COLUMN public.insur_damage_assessment_method.ref_item_id IS 'Ссылка на запись реестра справочников для фильтрации';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'emp_id'))then
        COMMENT ON COLUMN public.bti_premase.emp_id IS 'Инд.номер';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'dept_id'))then
        COMMENT ON COLUMN public.bti_premase.dept_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'kadastr'))then
        COMMENT ON COLUMN public.bti_premase.kadastr IS 'Кадастровый номер (К.Н.)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'floor_id'))then
        COMMENT ON COLUMN public.bti_premase.floor_id IS 'Этаж';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'inspection_date'))then
        COMMENT ON COLUMN public.bti_premase.inspection_date IS 'Дата обследования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'unkv'))then
        COMMENT ON COLUMN public.bti_premase.unkv IS 'Уникальный номер в здании';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'class_id'))then
        COMMENT ON COLUMN public.bti_premase.class_id IS 'Класс помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'class_name'))then
        COMMENT ON COLUMN public.bti_premase.class_name IS 'Класс помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'type_id'))then
        COMMENT ON COLUMN public.bti_premase.type_id IS 'Тип помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'type_name'))then
        COMMENT ON COLUMN public.bti_premase.type_name IS 'Тип помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'total_area'))then
        COMMENT ON COLUMN public.bti_premase.total_area IS 'Общая площадь помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'height'))then
        COMMENT ON COLUMN public.bti_premase.height IS 'Высота помещения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'area_pp'))then
        COMMENT ON COLUMN public.bti_premase.area_pp IS 'Площадь_пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'kvnom'))then
        COMMENT ON COLUMN public.bti_premase.kvnom IS 'Номер_помещения_пп';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'section_number'))then
        COMMENT ON COLUMN public.bti_premase.section_number IS 'Номер секции';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'living_area'))then
        COMMENT ON COLUMN public.bti_premase.living_area IS 'Жилая площадь';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'total_area_with_summer'))then
        COMMENT ON COLUMN public.bti_premase.total_area_with_summer IS 'Общая площадь (с летними)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'id_in_source'))then
        COMMENT ON COLUMN public.bti_premase.id_in_source IS 'ID объекта в системе источнике';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'rooms_count'))then
        COMMENT ON COLUMN public.bti_premase.rooms_count IS 'Количество жилых комнат';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'update_date'))then
        COMMENT ON COLUMN public.bti_premase.update_date IS 'Дата обновления';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'tet_code'))then
        COMMENT ON COLUMN public.bti_premase.tet_code IS 'Код справочника тип этажа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'tet'))then
        COMMENT ON COLUMN public.bti_premase.tet IS 'Тип этажа';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'unom'))then
        COMMENT ON COLUMN public.bti_premase.unom IS 'UNOM';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'obj_type_code'))then
        COMMENT ON COLUMN public.bti_premase.obj_type_code IS 'Код типа объекта недвижимости';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'obj_type'))then
        COMMENT ON COLUMN public.bti_premase.obj_type IS 'Тип объекта недвижимости';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'zpl'))then
        COMMENT ON COLUMN public.bti_premase.zpl IS 'Не входящие в общую площадь';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('bti_premase', 'bit0'))then
        COMMENT ON COLUMN public.bti_premase.bit0 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages_to', 'id'))then
        COMMENT ON COLUMN public.core_messages_to.id IS 'Суррогатный ключ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages_to', 'message_id'))then
        COMMENT ON COLUMN public.core_messages_to.message_id IS 'ИД сообщения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages_to', 'user_id'))then
        COMMENT ON COLUMN public.core_messages_to.user_id IS 'ИД пользователя';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages_to', 'was_readed'))then
        COMMENT ON COLUMN public.core_messages_to.was_readed IS 'Отметка когда сообщение было прочитано';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages_to', 'was_deleted'))then
        COMMENT ON COLUMN public.core_messages_to.was_deleted IS 'Отметка когда сообщение было удалено';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('mv_refresh_list', 'id'))then
        COMMENT ON COLUMN public.mv_refresh_list.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('mv_refresh_list', 'mv_name'))then
        COMMENT ON COLUMN public.mv_refresh_list.mv_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('mv_refresh_log', 'refresh_date'))then
        COMMENT ON COLUMN public.mv_refresh_log.refresh_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('mv_refresh_log', 'mv_name'))then
        COMMENT ON COLUMN public.mv_refresh_log.mv_name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('mv_refresh_log', 'msg_event'))then
        COMMENT ON COLUMN public.mv_refresh_log.msg_event IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('mv_refresh_log', 'err_message'))then
        COMMENT ON COLUMN public.mv_refresh_log.err_message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettings', 'userid'))then
        COMMENT ON COLUMN public.core_srd_usersettings.userid IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettings', 'settings'))then
        COMMENT ON COLUMN public.core_srd_usersettings.settings IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_usersettings', 'default_layout_settings'))then
        COMMENT ON COLUMN public.core_srd_usersettings.default_layout_settings IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'emp_id'))then
        COMMENT ON COLUMN public.insur_address.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'full_address'))then
        COMMENT ON COLUMN public.insur_address.full_address IS 'Полный адрес МКД по справочнику ФИАС';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'guid_fias_house'))then
        COMMENT ON COLUMN public.insur_address.guid_fias_house IS 'GUID-ссылка в справочнике ФИАС на адрес МКД';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'guid_fias_street'))then
        COMMENT ON COLUMN public.insur_address.guid_fias_street IS 'GUID-ссылка в справочнике ФИАС на улицу';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_city'))then
        COMMENT ON COLUMN public.insur_address.type_city IS 'Тип города';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'city'))then
        COMMENT ON COLUMN public.insur_address.city IS 'Город';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_urban_territory'))then
        COMMENT ON COLUMN public.insur_address.type_urban_territory IS 'Тип внутригородской территории';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'urban_territory'))then
        COMMENT ON COLUMN public.insur_address.urban_territory IS 'Внутригородская территория';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_district'))then
        COMMENT ON COLUMN public.insur_address.type_district IS 'Тип района';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'district'))then
        COMMENT ON COLUMN public.insur_address.district IS 'Район';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_street'))then
        COMMENT ON COLUMN public.insur_address.type_street IS 'Тип улицы';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'street'))then
        COMMENT ON COLUMN public.insur_address.street IS 'Улица';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_house'))then
        COMMENT ON COLUMN public.insur_address.type_house IS 'Тип дома';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'house'))then
        COMMENT ON COLUMN public.insur_address.house IS 'Дом';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_corpus'))then
        COMMENT ON COLUMN public.insur_address.type_corpus IS 'Тип корпуса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'corpus'))then
        COMMENT ON COLUMN public.insur_address.corpus IS 'Корпус';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_structure'))then
        COMMENT ON COLUMN public.insur_address.type_structure IS 'Тип строения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'structure'))then
        COMMENT ON COLUMN public.insur_address.structure IS 'Строение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'guid_region'))then
        COMMENT ON COLUMN public.insur_address.guid_region IS 'Guid-ссылка региона на ФИАС ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_region'))then
        COMMENT ON COLUMN public.insur_address.type_region IS 'Тип региона';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'region'))then
        COMMENT ON COLUMN public.insur_address.region IS 'Регион';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_avtonomnyy_okrug'))then
        COMMENT ON COLUMN public.insur_address.type_avtonomnyy_okrug IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'avtonomnyy_okrug'))then
        COMMENT ON COLUMN public.insur_address.avtonomnyy_okrug IS 'Автономный округ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'type_locality'))then
        COMMENT ON COLUMN public.insur_address.type_locality IS 'Тип населенного пункта';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'locality'))then
        COMMENT ON COLUMN public.insur_address.locality IS 'Населенный пункт';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'postal_code'))then
        COMMENT ON COLUMN public.insur_address.postal_code IS 'Почтовый индекс';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'source_address'))then
        COMMENT ON COLUMN public.insur_address.source_address IS 'Источник поступления адреса';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'source_address_code'))then
        COMMENT ON COLUMN public.insur_address.source_address_code IS 'Источник поступления адреса (справочное значение)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_address', 'short_address'))then
        COMMENT ON COLUMN public.insur_address.short_address IS 'Короткий адрес (без региона, индекса и города)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('tmp_all_prop_psign_code', 'ndog'))then
        COMMENT ON COLUMN public.tmp_all_prop_psign_code.ndog IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('tmp_all_prop_psign_code', 'ndogdat'))then
        COMMENT ON COLUMN public.tmp_all_prop_psign_code.ndogdat IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('tmp_all_prop_psign_code', 'paysign_code'))then
        COMMENT ON COLUMN public.tmp_all_prop_psign_code.paysign_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'emp_id'))then
        COMMENT ON COLUMN public.insur_param_calculation.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'obj_id'))then
        COMMENT ON COLUMN public.insur_param_calculation.obj_id IS 'Идентификатор объекта (Ссылка на реестр зданий)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'contract_id'))then
        COMMENT ON COLUMN public.insur_param_calculation.contract_id IS 'Идентификатор договора , ссылка на INSUR_ALL_ PROPERTY';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'insurance_id'))then
        COMMENT ON COLUMN public.insur_param_calculation.insurance_id IS 'Идентификатор страховой организации – по справочнику «Страховые организации»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'part_СОmpensation'))then
        COMMENT ON COLUMN public.insur_param_calculation.part_СОmpensation IS 'Доля ответственности страховой организации по возмещению вреда';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'actual_cost'))then
        COMMENT ON COLUMN public.insur_param_calculation.actual_cost IS 'Действительная стоимость дома, руб';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'coef_actual_cost'))then
        COMMENT ON COLUMN public.insur_param_calculation.coef_actual_cost IS 'Коэффициент пересчета действительной стоимости';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'actual_cost_current'))then
        COMMENT ON COLUMN public.insur_param_calculation.actual_cost_current IS 'Действительная стоимость дома (в пересчете на текущую цену), руб';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'indicator_r'))then
        COMMENT ON COLUMN public.insur_param_calculation.indicator_r IS 'Показатель рациональности объемно-планировочного решения, R';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'calculated_area'))then
        COMMENT ON COLUMN public.insur_param_calculation.calculated_area IS 'Расчетная площадь для определения стоимости общего имущества в МКД, кв.м.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_1'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_1 IS 'Земляные работы, фундамент';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_2'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_2 IS 'Стены и перегородки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_3'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_3 IS 'Перекрытия';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_4'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_4 IS 'Полы';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_5'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_5 IS 'Проемы';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_6'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_6 IS 'Отделочные работы';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_7'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_7 IS 'Прочие';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_8'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_8 IS 'Крыша';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_9'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_9 IS 'Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_10'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_10 IS 'Лифты и лифтовое оборудование';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'ui_11'))then
        COMMENT ON COLUMN public.insur_param_calculation.ui_11 IS 'Всего';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'total_cost_1'))then
        COMMENT ON COLUMN public.insur_param_calculation.total_cost_1 IS 'Общая стоимость конструкций без санитарно-технических работ и внутридомового инженерного оборудования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'design_cost_1'))then
        COMMENT ON COLUMN public.insur_param_calculation.design_cost_1 IS 'Сумма конструкций без санитарно-технических работ и внутридомового инженерного оборудования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'basic_rate_1'))then
        COMMENT ON COLUMN public.insur_param_calculation.basic_rate_1 IS 'Базовый тариф ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'annual_bonus_1'))then
        COMMENT ON COLUMN public.insur_param_calculation.annual_bonus_1 IS 'Годовая премия ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'total_cost_2'))then
        COMMENT ON COLUMN public.insur_param_calculation.total_cost_2 IS 'Общая стоимость конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'design_cost_2'))then
        COMMENT ON COLUMN public.insur_param_calculation.design_cost_2 IS 'Сумма конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'basic_rate_2'))then
        COMMENT ON COLUMN public.insur_param_calculation.basic_rate_2 IS 'Базовый тариф';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'annual_bonus_2'))then
        COMMENT ON COLUMN public.insur_param_calculation.annual_bonus_2 IS 'Годовая премия';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'total_cost_3'))then
        COMMENT ON COLUMN public.insur_param_calculation.total_cost_3 IS 'Общая стоимость конструкций лифтов и лифтового оборудования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'design_cost_3'))then
        COMMENT ON COLUMN public.insur_param_calculation.design_cost_3 IS 'Сумма конструкций лифтов и лифтового оборудования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'basic_rate_3'))then
        COMMENT ON COLUMN public.insur_param_calculation.basic_rate_3 IS 'Базовый тариф';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'annual_bonus_3'))then
        COMMENT ON COLUMN public.insur_param_calculation.annual_bonus_3 IS 'Годовая премия';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'size_annual_bonus'))then
        COMMENT ON COLUMN public.insur_param_calculation.size_annual_bonus IS 'Размер годовой премии по дому';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'request_number'))then
        COMMENT ON COLUMN public.insur_param_calculation.request_number IS 'Номер заявки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'request_date'))then
        COMMENT ON COLUMN public.insur_param_calculation.request_date IS 'Дата заявки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'note'))then
        COMMENT ON COLUMN public.insur_param_calculation.note IS 'Примечание';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'created_date'))then
        COMMENT ON COLUMN public.insur_param_calculation.created_date IS 'Дата создания';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'approval_date'))then
        COMMENT ON COLUMN public.insur_param_calculation.approval_date IS 'Дата согласования';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'created_user_id'))then
        COMMENT ON COLUMN public.insur_param_calculation.created_user_id IS 'Пользователь, который создал расчет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'approval_user_id'))then
        COMMENT ON COLUMN public.insur_param_calculation.approval_user_id IS 'Пользователь, который согласовал расчет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'status'))then
        COMMENT ON COLUMN public.insur_param_calculation.status IS 'Статус расчета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'deleted'))then
        COMMENT ON COLUMN public.insur_param_calculation.deleted IS 'Удалено, да/нет (1/0)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'status_code'))then
        COMMENT ON COLUMN public.insur_param_calculation.status_code IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'subject_id'))then
        COMMENT ON COLUMN public.insur_param_calculation.subject_id IS 'Страхователь';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'date_fill_1'))then
        COMMENT ON COLUMN public.insur_param_calculation.date_fill_1 IS 'Дата согласования 1';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'date_fill_2'))then
        COMMENT ON COLUMN public.insur_param_calculation.date_fill_2 IS 'Дата согласования 2';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'agreement_id_1'))then
        COMMENT ON COLUMN public.insur_param_calculation.agreement_id_1 IS 'Согласовавший пользователь 1';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'agreement_id_2'))then
        COMMENT ON COLUMN public.insur_param_calculation.agreement_id_2 IS 'Согласовавший пользователь 2';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'package_num'))then
        COMMENT ON COLUMN public.insur_param_calculation.package_num IS 'Номер пакета';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'flag_okrugl'))then
        COMMENT ON COLUMN public.insur_param_calculation.flag_okrugl IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_param_calculation', 'all_area'))then
        COMMENT ON COLUMN public.insur_param_calculation.all_area IS 'Общая площадь по зданию';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'id'))then
        COMMENT ON COLUMN public.core_srd_user.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'department_id'))then
        COMMENT ON COLUMN public.core_srd_user.department_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'username'))then
        COMMENT ON COLUMN public.core_srd_user.username IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'fullname'))then
        COMMENT ON COLUMN public.core_srd_user.fullname IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'name'))then
        COMMENT ON COLUMN public.core_srd_user.name IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'surname'))then
        COMMENT ON COLUMN public.core_srd_user.surname IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'patronymic'))then
        COMMENT ON COLUMN public.core_srd_user.patronymic IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'fullname_for_doc'))then
        COMMENT ON COLUMN public.core_srd_user.fullname_for_doc IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'position'))then
        COMMENT ON COLUMN public.core_srd_user.position IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'is_deleted'))then
        COMMENT ON COLUMN public.core_srd_user.is_deleted IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'change_date'))then
        COMMENT ON COLUMN public.core_srd_user.change_date IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'password_md5'))then
        COMMENT ON COLUMN public.core_srd_user.password_md5 IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'email'))then
        COMMENT ON COLUMN public.core_srd_user.email IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'phone'))then
        COMMENT ON COLUMN public.core_srd_user.phone IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'external_id'))then
        COMMENT ON COLUMN public.core_srd_user.external_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_srd_user', 'isdomainuser'))then
        COMMENT ON COLUMN public.core_srd_user.isdomainuser IS 'Признак доменной учетной записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'id'))then
        COMMENT ON COLUMN public.insur_living_premise_insur_cost.id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'date_begin'))then
        COMMENT ON COLUMN public.insur_living_premise_insur_cost.date_begin IS 'Дата начала действия';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'condition'))then
        COMMENT ON COLUMN public.insur_living_premise_insur_cost.condition IS 'Условие';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'value'))then
        COMMENT ON COLUMN public.insur_living_premise_insur_cost.value IS 'Значение, руб.';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'strah_tarif'))then
        COMMENT ON COLUMN public.insur_living_premise_insur_cost.strah_tarif IS 'Ставка ежемесячного страхового взноса за 1 кв м';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'strah_bonus'))then
        COMMENT ON COLUMN public.insur_living_premise_insur_cost.strah_bonus IS 'Страховая премия за 1 кв м';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'emp_id'))then
        COMMENT ON COLUMN public.insur_input_file.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'file_name'))then
        COMMENT ON COLUMN public.insur_input_file.file_name IS 'Название файла';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'type_file_code'))then
        COMMENT ON COLUMN public.insur_input_file.type_file_code IS 'Код типа файла  на основании справочника «Код типа файла»';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'period_reg_date'))then
        COMMENT ON COLUMN public.insur_input_file.period_reg_date IS 'Период учета данных в Системе';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'district_id'))then
        COMMENT ON COLUMN public.insur_input_file.district_id IS 'Идентификатор района';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'type_source'))then
        COMMENT ON COLUMN public.insur_input_file.type_source IS 'Источник  (1-МФЦ, 2-СК)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'date_input'))then
        COMMENT ON COLUMN public.insur_input_file.date_input IS 'Дата загрузки в Систему';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'count_str'))then
        COMMENT ON COLUMN public.insur_input_file.count_str IS 'Количество строк в файле';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'status'))then
        COMMENT ON COLUMN public.insur_input_file.status IS 'Статус загрузки/обработки файла';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'type_file'))then
        COMMENT ON COLUMN public.insur_input_file.type_file IS 'Тип файла';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'type_source_code'))then
        COMMENT ON COLUMN public.insur_input_file.type_source_code IS 'Код источника';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'status_code'))then
        COMMENT ON COLUMN public.insur_input_file.status_code IS 'Код статуса загрузки/обработки файла';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'link_package'))then
        COMMENT ON COLUMN public.insur_input_file.link_package IS 'Идентификатор пакета загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'user_id'))then
        COMMENT ON COLUMN public.insur_input_file.user_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'sum_all'))then
        COMMENT ON COLUMN public.insur_input_file.sum_all IS 'Общая сумма по данным в файле';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'file_storage_id'))then
        COMMENT ON COLUMN public.insur_input_file.file_storage_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'criteria_set'))then
        COMMENT ON COLUMN public.insur_input_file.criteria_set IS 'Пройдена процедура установки критериев';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'kod_post'))then
        COMMENT ON COLUMN public.insur_input_file.kod_post IS 'Код поставщика';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'parent_id'))then
        COMMENT ON COLUMN public.insur_input_file.parent_id IS 'Ранее загруженный файл';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'count_str_load'))then
        COMMENT ON COLUMN public.insur_input_file.count_str_load IS 'Загружено строк из файла';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'log_file_id'))then
        COMMENT ON COLUMN public.insur_input_file.log_file_id IS 'Идентификатор журнала загрузки';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_input_file', 'insurance_organization_id'))then
        COMMENT ON COLUMN public.insur_input_file.insurance_organization_id IS 'Ссылка на страховую организацию';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages', 'id'))then
        COMMENT ON COLUMN public.core_messages.id IS 'ИД сообщения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages', 'user_id'))then
        COMMENT ON COLUMN public.core_messages.user_id IS 'ИД пользователя создавшего сообщение';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages', 'subject'))then
        COMMENT ON COLUMN public.core_messages.subject IS 'Тема сообщения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages', 'message'))then
        COMMENT ON COLUMN public.core_messages.message IS 'Текст сообщения';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages', 'was_sended'))then
        COMMENT ON COLUMN public.core_messages.was_sended IS 'Дата и время когда сообщение было отправлено';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('core_messages', 'field_to'))then
        COMMENT ON COLUMN public.core_messages.field_to IS 'Строковое представление Кому';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'emp_id'))then
        COMMENT ON COLUMN public.insur_damage.emp_id IS 'Уникальный номер записи';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'obj_id'))then
        COMMENT ON COLUMN public.insur_damage.obj_id IS 'Идентификатор объекта (Ссылка на реестр зданий)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'type_doc'))then
        COMMENT ON COLUMN public.insur_damage.type_doc IS 'Тип договора';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'type_doc_code'))then
        COMMENT ON COLUMN public.insur_damage.type_doc_code IS 'Тип договора (1 - Общего имущества или по 2 -  Жилищным помещениям)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'insur_org_id'))then
        COMMENT ON COLUMN public.insur_damage.insur_org_id IS 'Название страховой компании';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'insur_date'))then
        COMMENT ON COLUMN public.insur_damage.insur_date IS 'Исходящая дата дела от СК';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'insur_nom'))then
        COMMENT ON COLUMN public.insur_damage.insur_nom IS 'Исходящий номер дела от СК';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'damage_data'))then
        COMMENT ON COLUMN public.insur_damage.damage_data IS 'Дата ущерба';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'damage_reason_gp'))then
        COMMENT ON COLUMN public.insur_damage.damage_reason_gp IS 'Причина ущерба по ЖП (на основании справочника «Причина ущерба для ЖП», 12125)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'damage_reason_gp_code'))then
        COMMENT ON COLUMN public.insur_damage.damage_reason_gp_code IS 'Причина ущерба по ЖП (на основании справочника «Причина ущерба для ЖП») (Code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'damage_reason_oi'))then
        COMMENT ON COLUMN public.insur_damage.damage_reason_oi IS 'Причина ущерба по ОИ (на основании справочника «Причина ущерба для ОИ», 12136)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'damage_reason_oi_code'))then
        COMMENT ON COLUMN public.insur_damage.damage_reason_oi_code IS 'Причина ущерба по ОИ (на основании справочника «Причина ущерба для ОИ», code)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'estimated_value'))then
        COMMENT ON COLUMN public.insur_damage.estimated_value IS 'Расчетная стоимость';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'insur_sum'))then
        COMMENT ON COLUMN public.insur_damage.insur_sum IS 'Страховая сумма';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'part_insur'))then
        COMMENT ON COLUMN public.insur_damage.part_insur IS 'Доля ответственности СК';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'type_build'))then
        COMMENT ON COLUMN public.insur_damage.type_build IS 'Тип здания (выбор из справочника)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'type_build_code'))then
        COMMENT ON COLUMN public.insur_damage.type_build_code IS 'Тип здания (выбор из справочника «Типы зданий»)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'type_cooker'))then
        COMMENT ON COLUMN public.insur_damage.type_cooker IS 'Плита (выбор из справочника)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'type_cooker_code'))then
        COMMENT ON COLUMN public.insur_damage.type_cooker_code IS 'Плита (выбор из справочника)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'type_floor_code'))then
        COMMENT ON COLUMN public.insur_damage.type_floor_code IS 'Материал пола (выбор из справочника)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'type_floor'))then
        COMMENT ON COLUMN public.insur_damage.type_floor IS 'Материал пола (выбор из справочника)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'sum_damage'))then
        COMMENT ON COLUMN public.insur_damage.sum_damage IS 'Сумма ущерба';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'subsidy'))then
        COMMENT ON COLUMN public.insur_damage.subsidy IS 'Размер субсидии';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'agreement_id_1'))then
        COMMENT ON COLUMN public.insur_damage.agreement_id_1 IS 'Согласующий_1, кто производит расчет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'agreement_id_2'))then
        COMMENT ON COLUMN public.insur_damage.agreement_id_2 IS 'Согласующий_2, кто проверяет';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_input'))then
        COMMENT ON COLUMN public.insur_damage.date_input IS 'Дата';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_fill_1'))then
        COMMENT ON COLUMN public.insur_damage.date_fill_1 IS 'Дата заполнения_1';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_fill_2'))then
        COMMENT ON COLUMN public.insur_damage.date_fill_2 IS 'Дата заполнения_2';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'main_agreement_id'))then
        COMMENT ON COLUMN public.insur_damage.main_agreement_id IS 'Основной согласующий (Выбор из справочника «Основной согласующий»)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_fill_main'))then
        COMMENT ON COLUMN public.insur_damage.date_fill_main IS 'Дата заполнения_3';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'signature_id'))then
        COMMENT ON COLUMN public.insur_damage.signature_id IS 'Подписант акта (Выбор из справочника «Подписанты»)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_fill_signature'))then
        COMMENT ON COLUMN public.insur_damage.date_fill_signature IS 'Дата заполнения_4';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'damage_amount_status'))then
        COMMENT ON COLUMN public.insur_damage.damage_amount_status IS 'Статус дела расчета ущерба';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'damage_amount_status_code'))then
        COMMENT ON COLUMN public.insur_damage.damage_amount_status_code IS 'Статус дела расчета ущерба (code, 12165)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'obj_reestr_id'))then
        COMMENT ON COLUMN public.insur_damage.obj_reestr_id IS 'Номер реестра объекта';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'nom_doc'))then
        COMMENT ON COLUMN public.insur_damage.nom_doc IS 'Номер дела в ГБУ';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'nom_date'))then
        COMMENT ON COLUMN public.insur_damage.nom_date IS 'Дата дела в ОПС';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'part_town'))then
        COMMENT ON COLUMN public.insur_damage.part_town IS 'Доля ответсвенности города';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'calcul_damage'))then
        COMMENT ON COLUMN public.insur_damage.calcul_damage IS 'Сумма ущерба, расчетная в Системе';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'subreason_damage_reason'))then
        COMMENT ON COLUMN public.insur_damage.subreason_damage_reason IS 'Подпричины ущерба по ЖП (справочник, 12134)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'subreason_damage_reason_code'))then
        COMMENT ON COLUMN public.insur_damage.subreason_damage_reason_code IS 'Подпричины ущерба по ЖП (справочник, code';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'refinement_subreason'))then
        COMMENT ON COLUMN public.insur_damage.refinement_subreason IS 'Уточнение Подпричины ущерба (справочник, 12135)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'refinement_subreason_code'))then
        COMMENT ON COLUMN public.insur_damage.refinement_subreason_code IS 'Уточнение Подпричины ущерба (справочник, code';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'strah_plat'))then
        COMMENT ON COLUMN public.insur_damage.strah_plat IS 'Выплата СК';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_input_gby'))then
        COMMENT ON COLUMN public.insur_damage.date_input_gby IS 'Дата поступления дела в ЦИПиЖС';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_doc_last_gby'))then
        COMMENT ON COLUMN public.insur_damage.date_doc_last_gby IS 'Дата поступления последнего документа в ЦИПиЖС';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_doc_add_gby'))then
        COMMENT ON COLUMN public.insur_damage.date_doc_add_gby IS 'Дата досылки документов в ЦИПиЖС';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'note'))then
        COMMENT ON COLUMN public.insur_damage.note IS 'Примечание';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'date_control'))then
        COMMENT ON COLUMN public.insur_damage.date_control IS 'Дата передано на проверку';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'control_user_id'))then
        COMMENT ON COLUMN public.insur_damage.control_user_id IS 'Пользователь, передавший на проверку';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'franchise'))then
        COMMENT ON COLUMN public.insur_damage.franchise IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'flag_slygebka'))then
        COMMENT ON COLUMN public.insur_damage.flag_slygebka IS 'Выплата по служебной записке';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'additional_data'))then
        COMMENT ON COLUMN public.insur_damage.additional_data IS 'Дополнительная информация';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'sum_damage_base'))then
        COMMENT ON COLUMN public.insur_damage.sum_damage_base IS 'Сумма ущерба (базовая)';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'calc_note'))then
        COMMENT ON COLUMN public.insur_damage.calc_note IS 'Примечание расчета ущерба';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('insur_damage', 'estimated_value_different'))then
        COMMENT ON COLUMN public.insur_damage.estimated_value_different IS 'Признак расхождения расчетной стоимости';
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'id'))then
        COMMENT ON COLUMN public.import_log_insur_flat.id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'ehd_parcel_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat.ehd_parcel_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'bti_flat_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat.bti_flat_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'insur_flat_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat.insur_flat_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'date_loaded'))then
        COMMENT ON COLUMN public.import_log_insur_flat.date_loaded IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'error_message'))then
        COMMENT ON COLUMN public.import_log_insur_flat.error_message IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'error_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat.error_id IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'is_error'))then
        COMMENT ON COLUMN public.import_log_insur_flat.is_error IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'update_date_ehd'))then
        COMMENT ON COLUMN public.import_log_insur_flat.update_date_ehd IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'update_date_bti'))then
        COMMENT ON COLUMN public.import_log_insur_flat.update_date_bti IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'cad_num'))then
        COMMENT ON COLUMN public.import_log_insur_flat.cad_num IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'kvnom'))then
        COMMENT ON COLUMN public.import_log_insur_flat.kvnom IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'error_attempts_count'))then
        COMMENT ON COLUMN public.import_log_insur_flat.error_attempts_count IS NULL;
    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if(core_updstru_checkexistcolumn('import_log_insur_flat', 'insur_building_id'))then
        COMMENT ON COLUMN public.import_log_insur_flat.insur_building_id IS NULL;
    end if;
END $$;