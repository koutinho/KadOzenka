
-- XV. Скрипт создания таблиц

--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_insur_rate')) IS NULL)then
        CREATE TABLE insur_insur_rate
        (
            id bigint NOT NULL,
		date_begin timestamp without time zone,
		tariff numeric(10, 4)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_insur_rate', 'id'))then
    	ALTER TABLE insur_insur_rate
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_insur_rate', 'date_begin'))then
    	ALTER TABLE insur_insur_rate
        	ADD date_begin timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_insur_rate', 'tariff'))then
    	ALTER TABLE insur_insur_rate
        	ADD tariff numeric(10, 4);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_reference')) IS NULL)then
        CREATE TABLE core_reference
        (
            referenceid bigint NOT NULL,
		description varchar(128),
		viddoc bigint,
		readonly smallint NOT NULL,
		progid varchar(60),
		istree smallint NOT NULL,
		controlheight bigint DEFAULT 0,
		controlwidth bigint DEFAULT 0,
		controltype varchar(60),
		hidereference bigint,
		skiptreelevel bigint,
		customeditor varchar(1000),
		defaultvalue bigint,
		displaytree smallint,
		usetreehelper smallint,
		istable smallint,
		name varchar(128)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_reference', 'referenceid'))then
    	ALTER TABLE core_reference
        	ADD referenceid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'description'))then
    	ALTER TABLE core_reference
        	ADD description varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'viddoc'))then
    	ALTER TABLE core_reference
        	ADD viddoc bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'readonly'))then
    	ALTER TABLE core_reference
        	ADD readonly smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'progid'))then
    	ALTER TABLE core_reference
        	ADD progid varchar(60);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'istree'))then
    	ALTER TABLE core_reference
        	ADD istree smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'controlheight'))then
    	ALTER TABLE core_reference
        	ADD controlheight bigint DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'controlwidth'))then
    	ALTER TABLE core_reference
        	ADD controlwidth bigint DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'controltype'))then
    	ALTER TABLE core_reference
        	ADD controltype varchar(60);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'hidereference'))then
    	ALTER TABLE core_reference
        	ADD hidereference bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'skiptreelevel'))then
    	ALTER TABLE core_reference
        	ADD skiptreelevel bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'customeditor'))then
    	ALTER TABLE core_reference
        	ADD customeditor varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'defaultvalue'))then
    	ALTER TABLE core_reference
        	ADD defaultvalue bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'displaytree'))then
    	ALTER TABLE core_reference
        	ADD displaytree smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'usetreehelper'))then
    	ALTER TABLE core_reference
        	ADD usetreehelper smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'istable'))then
    	ALTER TABLE core_reference
        	ADD istable smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference', 'name'))then
    	ALTER TABLE core_reference
        	ADD name varchar(128);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_regnom_repository')) IS NULL)then
        CREATE TABLE core_regnom_repository
        (
            regnomvalue varchar(120),
		idsequence bigint NOT NULL,
		regnomincrement bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_regnom_repository', 'regnomvalue'))then
    	ALTER TABLE core_regnom_repository
        	ADD regnomvalue varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_repository', 'idsequence'))then
    	ALTER TABLE core_regnom_repository
        	ADD idsequence bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_repository', 'regnomincrement'))then
    	ALTER TABLE core_regnom_repository
        	ADD regnomincrement bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_regnom_sequences')) IS NULL)then
        CREATE TABLE core_regnom_sequences
        (
            id bigint NOT NULL,
		numeratorid bigint,
		regnomtype bigint,
		par1 varchar(20),
		par0 varchar(20),
		par2 varchar(20),
		par3 varchar(20),
		par4 varchar(20),
		par5 varchar(20),
		par6 varchar(20),
		par7 varchar(20),
		par8 varchar(20),
		par9 varchar(20),
		currentincrement bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'id'))then
    	ALTER TABLE core_regnom_sequences
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'numeratorid'))then
    	ALTER TABLE core_regnom_sequences
        	ADD numeratorid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'regnomtype'))then
    	ALTER TABLE core_regnom_sequences
        	ADD regnomtype bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par1'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par1 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par0'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par0 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par2'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par2 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par3'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par3 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par4'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par4 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par5'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par5 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par6'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par6 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par7'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par7 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par8'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par8 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'par9'))then
    	ALTER TABLE core_regnom_sequences
        	ADD par9 varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_regnom_sequences', 'currentincrement'))then
    	ALTER TABLE core_regnom_sequences
        	ADD currentincrement bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_common_property_tariff')) IS NULL)then
        CREATE TABLE insur_common_property_tariff
        (
            id bigint NOT NULL,
		date_begin timestamp without time zone,
		category bigint,
		value numeric(10, 4)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_common_property_tariff', 'id'))then
    	ALTER TABLE insur_common_property_tariff
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_common_property_tariff', 'date_begin'))then
    	ALTER TABLE insur_common_property_tariff
        	ADD date_begin timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_common_property_tariff', 'category'))then
    	ALTER TABLE insur_common_property_tariff
        	ADD category bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_common_property_tariff', 'value'))then
    	ALTER TABLE insur_common_property_tariff
        	ADD value numeric(10, 4);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_reference_item')) IS NULL)then
        CREATE TABLE core_reference_item
        (
            itemid bigint NOT NULL,
		referenceid bigint,
		code varchar(50),
		value varchar(1000),
		short_title varchar(1000),
		is_archives smallint,
		user_name varchar(150),
		date_end_change timestamp without time zone,
		date_s timestamp without time zone,
		flag varchar(100),
		name varchar(128)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_reference_item', 'itemid'))then
    	ALTER TABLE core_reference_item
        	ADD itemid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'referenceid'))then
    	ALTER TABLE core_reference_item
        	ADD referenceid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'code'))then
    	ALTER TABLE core_reference_item
        	ADD code varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'value'))then
    	ALTER TABLE core_reference_item
        	ADD value varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'short_title'))then
    	ALTER TABLE core_reference_item
        	ADD short_title varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'is_archives'))then
    	ALTER TABLE core_reference_item
        	ADD is_archives smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'user_name'))then
    	ALTER TABLE core_reference_item
        	ADD user_name varchar(150);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'date_end_change'))then
    	ALTER TABLE core_reference_item
        	ADD date_end_change timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'date_s'))then
    	ALTER TABLE core_reference_item
        	ADD date_s timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'flag'))then
    	ALTER TABLE core_reference_item
        	ADD flag varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_item', 'name'))then
    	ALTER TABLE core_reference_item
        	ADD name varchar(128);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_reference_relation')) IS NULL)then
        CREATE TABLE core_reference_relation
        (
            relid bigint NOT NULL,
		parentkey varchar(120),
		childkey varchar(120),
		parentref bigint,
		childref bigint NOT NULL,
		parentreq bigint DEFAULT 0,
		treelevel bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_reference_relation', 'relid'))then
    	ALTER TABLE core_reference_relation
        	ADD relid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_relation', 'parentkey'))then
    	ALTER TABLE core_reference_relation
        	ADD parentkey varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_relation', 'childkey'))then
    	ALTER TABLE core_reference_relation
        	ADD childkey varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_relation', 'parentref'))then
    	ALTER TABLE core_reference_relation
        	ADD parentref bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_relation', 'childref'))then
    	ALTER TABLE core_reference_relation
        	ADD childref bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_relation', 'parentreq'))then
    	ALTER TABLE core_reference_relation
        	ADD parentreq bigint DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_relation', 'treelevel'))then
    	ALTER TABLE core_reference_relation
        	ADD treelevel bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_reference_tree')) IS NULL)then
        CREATE TABLE core_reference_tree
        (
            id bigint NOT NULL,
		code bigint,
		parentid bigint NOT NULL,
		childid bigint,
		referenceid bigint,
		level bigint,
		cldreferenceid bigint,
		adress_type bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_reference_tree', 'id'))then
    	ALTER TABLE core_reference_tree
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_tree', 'code'))then
    	ALTER TABLE core_reference_tree
        	ADD code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_tree', 'parentid'))then
    	ALTER TABLE core_reference_tree
        	ADD parentid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_tree', 'childid'))then
    	ALTER TABLE core_reference_tree
        	ADD childid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_tree', 'referenceid'))then
    	ALTER TABLE core_reference_tree
        	ADD referenceid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_tree', 'level'))then
    	ALTER TABLE core_reference_tree
        	ADD level bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_tree', 'cldreferenceid'))then
    	ALTER TABLE core_reference_tree
        	ADD cldreferenceid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_reference_tree', 'adress_type'))then
    	ALTER TABLE core_reference_tree
        	ADD adress_type bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_audit_action')) IS NULL)then
        CREATE TABLE core_td_audit_action
        (
            id bigint NOT NULL,
		name varchar(100)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_audit_action', 'id'))then
    	ALTER TABLE core_td_audit_action
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit_action', 'name'))then
    	ALTER TABLE core_td_audit_action
        	ADD name varchar(100);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_change')) IS NULL)then
        CREATE TABLE core_td_change
        (
            id bigint NOT NULL,
		changeset_id bigint NOT NULL,
		register_id bigint NOT NULL,
		object_id bigint NOT NULL,
		quant_id bigint,
		action bigint NOT NULL DEFAULT 1
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_change', 'id'))then
    	ALTER TABLE core_td_change
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_change', 'changeset_id'))then
    	ALTER TABLE core_td_change
        	ADD changeset_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_change', 'register_id'))then
    	ALTER TABLE core_td_change
        	ADD register_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_change', 'object_id'))then
    	ALTER TABLE core_td_change
        	ADD object_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_change', 'quant_id'))then
    	ALTER TABLE core_td_change
        	ADD quant_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_change', 'action'))then
    	ALTER TABLE core_td_change
        	ADD action bigint NOT NULL DEFAULT 1;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_tree')) IS NULL)then
        CREATE TABLE core_td_tree
        (
            id bigint NOT NULL,
		parent_id bigint,
		folder_name varchar(250),
		template_id bigint,
		tree_order bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_tree', 'id'))then
    	ALTER TABLE core_td_tree
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_tree', 'parent_id'))then
    	ALTER TABLE core_td_tree
        	ADD parent_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_tree', 'folder_name'))then
    	ALTER TABLE core_td_tree
        	ADD folder_name varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_tree', 'template_id'))then
    	ALTER TABLE core_td_tree
        	ADD template_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_tree', 'tree_order'))then
    	ALTER TABLE core_td_tree
        	ADD tree_order bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_template_type')) IS NULL)then
        CREATE TABLE core_td_template_type
        (
            id bigint NOT NULL,
		name varchar(50)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_template_type', 'id'))then
    	ALTER TABLE core_td_template_type
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_type', 'name'))then
    	ALTER TABLE core_td_template_type
        	ADD name varchar(50);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_tp')) IS NULL)then
        CREATE TABLE core_td_tp
        (
            id bigint NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_tp', 'id'))then
    	ALTER TABLE core_td_tp
        	ADD id bigint NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_status')) IS NULL)then
        CREATE TABLE core_td_status
        (
            id bigint NOT NULL,
		name varchar(50)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_status', 'id'))then
    	ALTER TABLE core_td_status
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_status', 'name'))then
    	ALTER TABLE core_td_status
        	ADD name varchar(50);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role_filter')) IS NULL)then
        CREATE TABLE core_srd_role_filter
        (
            id bigint NOT NULL,
		role_id bigint NOT NULL,
		register_id bigint,
		register_view_id varchar(500),
		qscondition text,
		description varchar(500)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_role_filter', 'id'))then
    	ALTER TABLE core_srd_role_filter
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_filter', 'role_id'))then
    	ALTER TABLE core_srd_role_filter
        	ADD role_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_filter', 'register_id'))then
    	ALTER TABLE core_srd_role_filter
        	ADD register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_filter', 'register_view_id'))then
    	ALTER TABLE core_srd_role_filter
        	ADD register_view_id varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_filter', 'qscondition'))then
    	ALTER TABLE core_srd_role_filter
        	ADD qscondition text;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_filter', 'description'))then
    	ALTER TABLE core_srd_role_filter
        	ADD description varchar(500);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_template')) IS NULL)then
        CREATE TABLE core_td_template
        (
            id bigint NOT NULL,
		current_version_id bigint,
		template_name varchar(150) NOT NULL,
		scheme_name varchar(50) NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_template', 'id'))then
    	ALTER TABLE core_td_template
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template', 'current_version_id'))then
    	ALTER TABLE core_td_template
        	ADD current_version_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template', 'template_name'))then
    	ALTER TABLE core_td_template
        	ADD template_name varchar(150) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template', 'scheme_name'))then
    	ALTER TABLE core_td_template
        	ADD scheme_name varchar(50) NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_function_reg_cat')) IS NULL)then
        CREATE TABLE core_srd_function_reg_cat
        (
            id bigint NOT NULL,
		function_id bigint,
		register_category_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_function_reg_cat', 'id'))then
    	ALTER TABLE core_srd_function_reg_cat
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_function_reg_cat', 'function_id'))then
    	ALTER TABLE core_srd_function_reg_cat
        	ADD function_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_function_reg_cat', 'register_category_id'))then
    	ALTER TABLE core_srd_function_reg_cat
        	ADD register_category_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_register_category')) IS NULL)then
        CREATE TABLE core_srd_register_category
        (
            id bigint NOT NULL,
		register_id bigint NOT NULL,
		parent_id bigint,
		name varchar(256),
		qs_condition text
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_register_category', 'id'))then
    	ALTER TABLE core_srd_register_category
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_register_category', 'register_id'))then
    	ALTER TABLE core_srd_register_category
        	ADD register_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_register_category', 'parent_id'))then
    	ALTER TABLE core_srd_register_category
        	ADD parent_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_register_category', 'name'))then
    	ALTER TABLE core_srd_register_category
        	ADD name varchar(256);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_register_category', 'qs_condition'))then
    	ALTER TABLE core_srd_register_category
        	ADD qs_condition text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('dashboards_dashboard')) IS NULL)then
        CREATE TABLE dashboards_dashboard
        (
            id bigint NOT NULL,
		user_id bigint,
		layout_type bigint,
		name varchar(128),
		description varchar(128),
		iscommon smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('dashboards_dashboard', 'id'))then
    	ALTER TABLE dashboards_dashboard
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_dashboard', 'user_id'))then
    	ALTER TABLE dashboards_dashboard
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_dashboard', 'layout_type'))then
    	ALTER TABLE dashboards_dashboard
        	ADD layout_type bigint;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_dashboard', 'name'))then
    	ALTER TABLE dashboards_dashboard
        	ADD name varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_dashboard', 'description'))then
    	ALTER TABLE dashboards_dashboard
        	ADD description varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_dashboard', 'iscommon'))then
    	ALTER TABLE dashboards_dashboard
        	ADD iscommon smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_integrated_indicators_repl_cost')) IS NULL)then
        CREATE TABLE insur_integrated_indicators_repl_cost
        (
            id bigint NOT NULL,
		stove_type varchar(255),
		stove_type_code bigint,
		elements_constructions varchar(255),
		elements_constructions_code bigint,
		floor_material varchar(255),
		floor_material_code bigint,
		building_type varchar(255),
		building_type_code bigint,
		cost_value numeric(10, 4),
		parent_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'id'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'stove_type'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD stove_type varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'stove_type_code'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD stove_type_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'elements_constructions'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD elements_constructions varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'elements_constructions_code'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD elements_constructions_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'floor_material'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD floor_material varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'floor_material_code'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD floor_material_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'building_type'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD building_type varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'building_type_code'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD building_type_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'cost_value'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD cost_value numeric(10, 4);
	end if;


	if(not core_updstru_checkexistcolumn('insur_integrated_indicators_repl_cost', 'parent_id'))then
    	ALTER TABLE insur_integrated_indicators_repl_cost
        	ADD parent_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('spd_usersrd2spd')) IS NULL)then
        CREATE TABLE spd_usersrd2spd
        (
            id bigint NOT NULL,
		srd_user_id bigint,
		spd_user_id bigint NOT NULL,
		usercategory varchar(128) NOT NULL,
		usercategory_code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('spd_usersrd2spd', 'id'))then
    	ALTER TABLE spd_usersrd2spd
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('spd_usersrd2spd', 'srd_user_id'))then
    	ALTER TABLE spd_usersrd2spd
        	ADD srd_user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_usersrd2spd', 'spd_user_id'))then
    	ALTER TABLE spd_usersrd2spd
        	ADD spd_user_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('spd_usersrd2spd', 'usercategory'))then
    	ALTER TABLE spd_usersrd2spd
        	ADD usercategory varchar(128) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('spd_usersrd2spd', 'usercategory_code'))then
    	ALTER TABLE spd_usersrd2spd
        	ADD usercategory_code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_usersettingsregview')) IS NULL)then
        CREATE TABLE core_srd_usersettingsregview
        (
            id bigint NOT NULL,
		user_id bigint,
		register_view_id varchar(120),
		fast_filter varchar(120)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_usersettingsregview', 'id'))then
    	ALTER TABLE core_srd_usersettingsregview
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingsregview', 'user_id'))then
    	ALTER TABLE core_srd_usersettingsregview
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingsregview', 'register_view_id'))then
    	ALTER TABLE core_srd_usersettingsregview
        	ADD register_view_id varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingsregview', 'fast_filter'))then
    	ALTER TABLE core_srd_usersettingsregview
        	ADD fast_filter varchar(120);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('dashboards_user_settings')) IS NULL)then
        CREATE TABLE dashboards_user_settings
        (
            id bigint NOT NULL,
		user_id bigint,
		default_panel_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('dashboards_user_settings', 'id'))then
    	ALTER TABLE dashboards_user_settings
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_user_settings', 'user_id'))then
    	ALTER TABLE dashboards_user_settings
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_user_settings', 'default_panel_id'))then
    	ALTER TABLE dashboards_user_settings
        	ADD default_panel_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('dashboards_panel')) IS NULL)then
        CREATE TABLE dashboards_panel
        (
            id bigint NOT NULL,
		dashboard_id bigint,
		title varchar(128),
		column_index bigint,
		order_in_column bigint,
		panel_type_id bigint,
		settings text
        );
    else
            
	if(not core_updstru_checkexistcolumn('dashboards_panel', 'id'))then
    	ALTER TABLE dashboards_panel
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel', 'dashboard_id'))then
    	ALTER TABLE dashboards_panel
        	ADD dashboard_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel', 'title'))then
    	ALTER TABLE dashboards_panel
        	ADD title varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel', 'column_index'))then
    	ALTER TABLE dashboards_panel
        	ADD column_index bigint;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel', 'order_in_column'))then
    	ALTER TABLE dashboards_panel
        	ADD order_in_column bigint;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel', 'panel_type_id'))then
    	ALTER TABLE dashboards_panel
        	ADD panel_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel', 'settings'))then
    	ALTER TABLE dashboards_panel
        	ADD settings text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('dashboards_panel_type')) IS NULL)then
        CREATE TABLE dashboards_panel_type
        (
            id bigint NOT NULL,
		name varchar(128),
		description varchar(128),
		url varchar(128),
		dto_class_full_name varchar(128)
        );
    else
            
	if(not core_updstru_checkexistcolumn('dashboards_panel_type', 'id'))then
    	ALTER TABLE dashboards_panel_type
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel_type', 'name'))then
    	ALTER TABLE dashboards_panel_type
        	ADD name varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel_type', 'description'))then
    	ALTER TABLE dashboards_panel_type
        	ADD description varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel_type', 'url'))then
    	ALTER TABLE dashboards_panel_type
        	ADD url varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('dashboards_panel_type', 'dto_class_full_name'))then
    	ALTER TABLE dashboards_panel_type
        	ADD dto_class_full_name varchar(128);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_list_object')) IS NULL)then
        CREATE TABLE core_list_object
        (
            id bigint NOT NULL,
		list_id bigint NOT NULL,
		object_id bigint NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_list_object', 'id'))then
    	ALTER TABLE core_list_object
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_list_object', 'list_id'))then
    	ALTER TABLE core_list_object
        	ADD list_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_list_object', 'object_id'))then
    	ALTER TABLE core_list_object
        	ADD object_id bigint NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_layout_column_type')) IS NULL)then
        CREATE TABLE core_layout_column_type
        (
            id bigint NOT NULL,
		code varchar(30),
		name varchar(100)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_layout_column_type', 'id'))then
    	ALTER TABLE core_layout_column_type
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_column_type', 'code'))then
    	ALTER TABLE core_layout_column_type
        	ADD code varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_column_type', 'name'))then
    	ALTER TABLE core_layout_column_type
        	ADD name varchar(100);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_qry_filter')) IS NULL)then
        CREATE TABLE core_qry_filter
        (
            qryfilterid bigint NOT NULL,
		qryid bigint,
		qryoperationid bigint NOT NULL,
		kindelementid bigint,
		condition varchar(1000),
		andtrueorfalse smallint NOT NULL,
		qryposition bigint DEFAULT 0,
		value varchar(120) DEFAULT ''''''::character varying,
		byref smallint NOT NULL DEFAULT 0,
		bracketsfirst varchar(10) DEFAULT ''''''::character varying,
		bracketsclose varchar(10) DEFAULT ''''''::character varying,
		referenceid bigint,
		specialregisterid bigint,
		specialattributetype bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_qry_filter', 'qryfilterid'))then
    	ALTER TABLE core_qry_filter
        	ADD qryfilterid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'qryid'))then
    	ALTER TABLE core_qry_filter
        	ADD qryid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'qryoperationid'))then
    	ALTER TABLE core_qry_filter
        	ADD qryoperationid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'kindelementid'))then
    	ALTER TABLE core_qry_filter
        	ADD kindelementid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'condition'))then
    	ALTER TABLE core_qry_filter
        	ADD condition varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'andtrueorfalse'))then
    	ALTER TABLE core_qry_filter
        	ADD andtrueorfalse smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'qryposition'))then
    	ALTER TABLE core_qry_filter
        	ADD qryposition bigint DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'value'))then
    	ALTER TABLE core_qry_filter
        	ADD value varchar(120) DEFAULT ''''''::character varying;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'byref'))then
    	ALTER TABLE core_qry_filter
        	ADD byref smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'bracketsfirst'))then
    	ALTER TABLE core_qry_filter
        	ADD bracketsfirst varchar(10) DEFAULT ''''''::character varying;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'bracketsclose'))then
    	ALTER TABLE core_qry_filter
        	ADD bracketsclose varchar(10) DEFAULT ''''''::character varying;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'referenceid'))then
    	ALTER TABLE core_qry_filter
        	ADD referenceid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'specialregisterid'))then
    	ALTER TABLE core_qry_filter
        	ADD specialregisterid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_filter', 'specialattributetype'))then
    	ALTER TABLE core_qry_filter
        	ADD specialattributetype bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_layout_details')) IS NULL)then
        CREATE TABLE core_layout_details
        (
            id bigint NOT NULL,
		layoutid bigint NOT NULL,
		detailtype bigint NOT NULL,
		ordinal bigint NOT NULL,
		attributeid bigint,
		sortbyattribute bigint,
		referenceid bigint,
		headertext varchar(500),
		headerwidth bigint,
		visible smallint NOT NULL,
		format varchar(50),
		datatype bigint,
		expression varchar(200),
		sqlexpression varchar(200),
		totaltext varchar(100),
		totaltype bigint,
		style varchar(4000),
		enablestyle smallint,
		textalign varchar(20),
		qscolumn text
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_layout_details', 'id'))then
    	ALTER TABLE core_layout_details
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'layoutid'))then
    	ALTER TABLE core_layout_details
        	ADD layoutid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'detailtype'))then
    	ALTER TABLE core_layout_details
        	ADD detailtype bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'ordinal'))then
    	ALTER TABLE core_layout_details
        	ADD ordinal bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'attributeid'))then
    	ALTER TABLE core_layout_details
        	ADD attributeid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'sortbyattribute'))then
    	ALTER TABLE core_layout_details
        	ADD sortbyattribute bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'referenceid'))then
    	ALTER TABLE core_layout_details
        	ADD referenceid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'headertext'))then
    	ALTER TABLE core_layout_details
        	ADD headertext varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'headerwidth'))then
    	ALTER TABLE core_layout_details
        	ADD headerwidth bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'visible'))then
    	ALTER TABLE core_layout_details
        	ADD visible smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'format'))then
    	ALTER TABLE core_layout_details
        	ADD format varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'datatype'))then
    	ALTER TABLE core_layout_details
        	ADD datatype bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'expression'))then
    	ALTER TABLE core_layout_details
        	ADD expression varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'sqlexpression'))then
    	ALTER TABLE core_layout_details
        	ADD sqlexpression varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'totaltext'))then
    	ALTER TABLE core_layout_details
        	ADD totaltext varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'totaltype'))then
    	ALTER TABLE core_layout_details
        	ADD totaltype bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'style'))then
    	ALTER TABLE core_layout_details
        	ADD style varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'enablestyle'))then
    	ALTER TABLE core_layout_details
        	ADD enablestyle smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'textalign'))then
    	ALTER TABLE core_layout_details
        	ADD textalign varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_details', 'qscolumn'))then
    	ALTER TABLE core_layout_details
        	ADD qscolumn text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_qry_operation')) IS NULL)then
        CREATE TABLE core_qry_operation
        (
            qryoperationid bigint NOT NULL,
		description varchar(50),
		sqlstatement varchar(50)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_qry_operation', 'qryoperationid'))then
    	ALTER TABLE core_qry_operation
        	ADD qryoperationid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_operation', 'description'))then
    	ALTER TABLE core_qry_operation
        	ADD description varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('core_qry_operation', 'sqlstatement'))then
    	ALTER TABLE core_qry_operation
        	ADD sqlstatement varchar(50);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_flat_status')) IS NULL)then
        CREATE TABLE insur_flat_status
        (
            id bigint NOT NULL,
		name varchar(255),
		short_name varchar(255),
		code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_flat_status', 'id'))then
    	ALTER TABLE insur_flat_status
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_status', 'name'))then
    	ALTER TABLE insur_flat_status
        	ADD name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_status', 'short_name'))then
    	ALTER TABLE insur_flat_status
        	ADD short_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_status', 'code'))then
    	ALTER TABLE insur_flat_status
        	ADD code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_attachment_file')) IS NULL)then
        CREATE TABLE core_attachment_file
        (
            id bigint NOT NULL,
		attachment_id bigint NOT NULL,
		filename varchar(500),
		mimetype varchar(100),
		page bigint NOT NULL,
		is_main smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_attachment_file', 'id'))then
    	ALTER TABLE core_attachment_file
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_file', 'attachment_id'))then
    	ALTER TABLE core_attachment_file
        	ADD attachment_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_file', 'filename'))then
    	ALTER TABLE core_attachment_file
        	ADD filename varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_file', 'mimetype'))then
    	ALTER TABLE core_attachment_file
        	ADD mimetype varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_file', 'page'))then
    	ALTER TABLE core_attachment_file
        	ADD page bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_file', 'is_main'))then
    	ALTER TABLE core_attachment_file
        	ADD is_main smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_fsp_o')) IS NULL)then
        CREATE TABLE insur_fsp_o
        (
            id bigint NOT NULL,
		info varchar(255),
		deleted smallint,
		uid varchar(255),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_fsp_o', 'id'))then
    	ALTER TABLE insur_fsp_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_o', 'info'))then
    	ALTER TABLE insur_fsp_o
        	ADD info varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_o', 'deleted'))then
    	ALTER TABLE insur_fsp_o
        	ADD deleted smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_o', 'uid'))then
    	ALTER TABLE insur_fsp_o
        	ADD uid varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_o', 'enddatechange'))then
    	ALTER TABLE insur_fsp_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_flat_type')) IS NULL)then
        CREATE TABLE insur_flat_type
        (
            id bigint NOT NULL,
		code bigint,
		name varchar(255),
		short_name varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_flat_type', 'id'))then
    	ALTER TABLE insur_flat_type
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_type', 'code'))then
    	ALTER TABLE insur_flat_type
        	ADD code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_type', 'name'))then
    	ALTER TABLE insur_flat_type
        	ADD name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_type', 'short_name'))then
    	ALTER TABLE insur_flat_type
        	ADD short_name varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_link_build_bti')) IS NULL)then
        CREATE TABLE insur_link_build_bti
        (
            emp_id bigint NOT NULL,
		id_bti_fsks bigint,
		id_insur_build bigint,
		flag_dubl_unom smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_link_build_bti', 'emp_id'))then
    	ALTER TABLE insur_link_build_bti
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_build_bti', 'id_bti_fsks'))then
    	ALTER TABLE insur_link_build_bti
        	ADD id_bti_fsks bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_build_bti', 'id_insur_build'))then
    	ALTER TABLE insur_link_build_bti
        	ADD id_insur_build bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_build_bti', 'flag_dubl_unom'))then
    	ALTER TABLE insur_link_build_bti
        	ADD flag_dubl_unom smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_link_flat_egrn')) IS NULL)then
        CREATE TABLE insur_link_flat_egrn
        (
            emp_id bigint NOT NULL,
		id_insur_flat bigint,
		id_egrn_flat bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_link_flat_egrn', 'emp_id'))then
    	ALTER TABLE insur_link_flat_egrn
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_flat_egrn', 'id_insur_flat'))then
    	ALTER TABLE insur_link_flat_egrn
        	ADD id_insur_flat bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_flat_egrn', 'id_egrn_flat'))then
    	ALTER TABLE insur_link_flat_egrn
        	ADD id_egrn_flat bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_floor_o')) IS NULL)then
        CREATE TABLE bti_floor_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_floor_o', 'id'))then
    	ALTER TABLE bti_floor_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_o', 'info'))then
    	ALTER TABLE bti_floor_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_o', 'deleted'))then
    	ALTER TABLE bti_floor_o
        	ADD deleted bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_o', 'uid'))then
    	ALTER TABLE bti_floor_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_o', 'enddatechange'))then
    	ALTER TABLE bti_floor_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_addrlink_o')) IS NULL)then
        CREATE TABLE bti_addrlink_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL DEFAULT 0,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_addrlink_o', 'id'))then
    	ALTER TABLE bti_addrlink_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_o', 'info'))then
    	ALTER TABLE bti_addrlink_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_o', 'deleted'))then
    	ALTER TABLE bti_addrlink_o
        	ADD deleted bigint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_o', 'uid'))then
    	ALTER TABLE bti_addrlink_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_o', 'enddatechange'))then
    	ALTER TABLE bti_addrlink_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_addrlink_q')) IS NULL)then
        CREATE TABLE bti_addrlink_q
        (
            id bigint NOT NULL,
		emp_id bigint NOT NULL,
		actual bigint NOT NULL,
		status bigint NOT NULL,
		dept_id bigint,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		address_state varchar(4000),
		address_state_id bigint,
		address_state_name varchar(4000),
		address_state_date timestamp without time zone,
		address_status_id bigint,
		address_status_name varchar(4000),
		individual_number bigint,
		ground_document varchar(4000),
		ground_document_type_id bigint,
		ground_document_type_name varchar(4000),
		ground_document_number varchar(4000),
		ground_document_date_issue timestamp without time zone,
		ground_document_content_id bigint,
		ground_document_content_name varchar(4000),
		unad bigint,
		address_id bigint,
		address_name varchar(4000),
		building_id bigint,
		building_name varchar(4000),
		reg_number bigint,
		reg_date timestamp without time zone,
		reg_doc_type_name varchar(4000),
		reg_doc_type_id bigint,
		reg_doc_number varchar(4000),
		reg_doc_date timestamp without time zone,
		reg_doc_content_name varchar(4000),
		reg_doc_content_id bigint,
		register_object_number bigint,
		comments varchar(4000),
		id_source bigint,
		text_source varchar(250)
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'emp_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'actual'))then
    	ALTER TABLE bti_addrlink_q
        	ADD actual bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'status'))then
    	ALTER TABLE bti_addrlink_q
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'dept_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 's_'))then
    	ALTER TABLE bti_addrlink_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'po_'))then
    	ALTER TABLE bti_addrlink_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'address_state'))then
    	ALTER TABLE bti_addrlink_q
        	ADD address_state varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'address_state_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD address_state_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'address_state_name'))then
    	ALTER TABLE bti_addrlink_q
        	ADD address_state_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'address_state_date'))then
    	ALTER TABLE bti_addrlink_q
        	ADD address_state_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'address_status_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD address_status_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'address_status_name'))then
    	ALTER TABLE bti_addrlink_q
        	ADD address_status_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'individual_number'))then
    	ALTER TABLE bti_addrlink_q
        	ADD individual_number bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document'))then
    	ALTER TABLE bti_addrlink_q
        	ADD ground_document varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_type_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD ground_document_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_type_name'))then
    	ALTER TABLE bti_addrlink_q
        	ADD ground_document_type_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_number'))then
    	ALTER TABLE bti_addrlink_q
        	ADD ground_document_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_date_issue'))then
    	ALTER TABLE bti_addrlink_q
        	ADD ground_document_date_issue timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_content_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD ground_document_content_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'ground_document_content_name'))then
    	ALTER TABLE bti_addrlink_q
        	ADD ground_document_content_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'unad'))then
    	ALTER TABLE bti_addrlink_q
        	ADD unad bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'address_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD address_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'address_name'))then
    	ALTER TABLE bti_addrlink_q
        	ADD address_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'building_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD building_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'building_name'))then
    	ALTER TABLE bti_addrlink_q
        	ADD building_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_number'))then
    	ALTER TABLE bti_addrlink_q
        	ADD reg_number bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_date'))then
    	ALTER TABLE bti_addrlink_q
        	ADD reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_type_name'))then
    	ALTER TABLE bti_addrlink_q
        	ADD reg_doc_type_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_type_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD reg_doc_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_number'))then
    	ALTER TABLE bti_addrlink_q
        	ADD reg_doc_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_date'))then
    	ALTER TABLE bti_addrlink_q
        	ADD reg_doc_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_content_name'))then
    	ALTER TABLE bti_addrlink_q
        	ADD reg_doc_content_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'reg_doc_content_id'))then
    	ALTER TABLE bti_addrlink_q
        	ADD reg_doc_content_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'register_object_number'))then
    	ALTER TABLE bti_addrlink_q
        	ADD register_object_number bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'comments'))then
    	ALTER TABLE bti_addrlink_q
        	ADD comments varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'id_source'))then
    	ALTER TABLE bti_addrlink_q
        	ADD id_source bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_addrlink_q', 'text_source'))then
    	ALTER TABLE bti_addrlink_q
        	ADD text_source varchar(250);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_address_o')) IS NULL)then
        CREATE TABLE bti_address_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL DEFAULT 0,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_address_o', 'id'))then
    	ALTER TABLE bti_address_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_o', 'info'))then
    	ALTER TABLE bti_address_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_o', 'deleted'))then
    	ALTER TABLE bti_address_o
        	ADD deleted bigint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_o', 'uid'))then
    	ALTER TABLE bti_address_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_o', 'enddatechange'))then
    	ALTER TABLE bti_address_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_address_q')) IS NULL)then
        CREATE TABLE bti_address_q
        (
            id bigint NOT NULL,
		emp_id bigint NOT NULL,
		actual bigint NOT NULL,
		status bigint NOT NULL,
		dept_id bigint,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		individual_number bigint,
		full_name varchar(4000),
		short_name varchar(4000),
		name_for_sort varchar(4000),
		main_name varchar(4000),
		main_name_print varchar(4000),
		full_name_print varchar(4000),
		id_in_ds varchar(4000),
		data_source_id bigint,
		data_source_name varchar(4000),
		subject_rf_id bigint,
		subject_rf_name varchar(4000),
		okrug_id bigint,
		district_id bigint,
		settlement_id bigint,
		settlement_name varchar(4000),
		town_id bigint,
		town_name varchar(4000),
		pse_id bigint,
		pse_name varchar(4000),
		street_id bigint,
		street_name varchar(4000),
		property_type_id bigint,
		property_type_name varchar(4000),
		plot_number varchar(4000),
		house_number varchar(4000),
		korpus_number varchar(4000),
		structure_type_id bigint,
		structure_type_name varchar(4000),
		structure_number varchar(4000),
		letter_number varchar(4000),
		location_desc varchar(4000),
		okato_code varchar(11),
		kladr_code varchar(20),
		index_postal varchar(6),
		type_corpus varchar(255),
		type_corpus_id bigint,
		code_nsi varchar(20),
		other varchar(255),
		oktmo varchar(64),
		oktmo_id bigint,
		full_mix_address varchar(4000),
		address_or_location smallint,
		code_fias varchar(128)
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_address_q', 'id'))then
    	ALTER TABLE bti_address_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'emp_id'))then
    	ALTER TABLE bti_address_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'actual'))then
    	ALTER TABLE bti_address_q
        	ADD actual bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'status'))then
    	ALTER TABLE bti_address_q
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'dept_id'))then
    	ALTER TABLE bti_address_q
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 's_'))then
    	ALTER TABLE bti_address_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'po_'))then
    	ALTER TABLE bti_address_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'individual_number'))then
    	ALTER TABLE bti_address_q
        	ADD individual_number bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'full_name'))then
    	ALTER TABLE bti_address_q
        	ADD full_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'short_name'))then
    	ALTER TABLE bti_address_q
        	ADD short_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'name_for_sort'))then
    	ALTER TABLE bti_address_q
        	ADD name_for_sort varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'main_name'))then
    	ALTER TABLE bti_address_q
        	ADD main_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'main_name_print'))then
    	ALTER TABLE bti_address_q
        	ADD main_name_print varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'full_name_print'))then
    	ALTER TABLE bti_address_q
        	ADD full_name_print varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'id_in_ds'))then
    	ALTER TABLE bti_address_q
        	ADD id_in_ds varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'data_source_id'))then
    	ALTER TABLE bti_address_q
        	ADD data_source_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'data_source_name'))then
    	ALTER TABLE bti_address_q
        	ADD data_source_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'subject_rf_id'))then
    	ALTER TABLE bti_address_q
        	ADD subject_rf_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'subject_rf_name'))then
    	ALTER TABLE bti_address_q
        	ADD subject_rf_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'okrug_id'))then
    	ALTER TABLE bti_address_q
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'district_id'))then
    	ALTER TABLE bti_address_q
        	ADD district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'settlement_id'))then
    	ALTER TABLE bti_address_q
        	ADD settlement_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'settlement_name'))then
    	ALTER TABLE bti_address_q
        	ADD settlement_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'town_id'))then
    	ALTER TABLE bti_address_q
        	ADD town_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'town_name'))then
    	ALTER TABLE bti_address_q
        	ADD town_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'pse_id'))then
    	ALTER TABLE bti_address_q
        	ADD pse_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'pse_name'))then
    	ALTER TABLE bti_address_q
        	ADD pse_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'street_id'))then
    	ALTER TABLE bti_address_q
        	ADD street_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'street_name'))then
    	ALTER TABLE bti_address_q
        	ADD street_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'property_type_id'))then
    	ALTER TABLE bti_address_q
        	ADD property_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'property_type_name'))then
    	ALTER TABLE bti_address_q
        	ADD property_type_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'plot_number'))then
    	ALTER TABLE bti_address_q
        	ADD plot_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'house_number'))then
    	ALTER TABLE bti_address_q
        	ADD house_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'korpus_number'))then
    	ALTER TABLE bti_address_q
        	ADD korpus_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'structure_type_id'))then
    	ALTER TABLE bti_address_q
        	ADD structure_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'structure_type_name'))then
    	ALTER TABLE bti_address_q
        	ADD structure_type_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'structure_number'))then
    	ALTER TABLE bti_address_q
        	ADD structure_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'letter_number'))then
    	ALTER TABLE bti_address_q
        	ADD letter_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'location_desc'))then
    	ALTER TABLE bti_address_q
        	ADD location_desc varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'okato_code'))then
    	ALTER TABLE bti_address_q
        	ADD okato_code varchar(11);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'kladr_code'))then
    	ALTER TABLE bti_address_q
        	ADD kladr_code varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'index_postal'))then
    	ALTER TABLE bti_address_q
        	ADD index_postal varchar(6);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'type_corpus'))then
    	ALTER TABLE bti_address_q
        	ADD type_corpus varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'type_corpus_id'))then
    	ALTER TABLE bti_address_q
        	ADD type_corpus_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'code_nsi'))then
    	ALTER TABLE bti_address_q
        	ADD code_nsi varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'other'))then
    	ALTER TABLE bti_address_q
        	ADD other varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'oktmo'))then
    	ALTER TABLE bti_address_q
        	ADD oktmo varchar(64);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'oktmo_id'))then
    	ALTER TABLE bti_address_q
        	ADD oktmo_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'full_mix_address'))then
    	ALTER TABLE bti_address_q
        	ADD full_mix_address varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'address_or_location'))then
    	ALTER TABLE bti_address_q
        	ADD address_or_location smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_address_q', 'code_fias'))then
    	ALTER TABLE bti_address_q
        	ADD code_fias varchar(128);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_rooms')) IS NULL)then
        CREATE TABLE bti_rooms
        (
            emp_id bigint NOT NULL,
		dept_id bigint,
		premise_id bigint,
		purpose_id bigint,
		purpose_name varchar(4000),
		special_purpose_id bigint,
		special_purpose_name varchar(4000),
		area_kind_id bigint,
		area_kind_name varchar(4000),
		area_type_id bigint,
		area_type_name varchar(4000),
		height bigint,
		survey_date timestamp without time zone,
		area_calculation_formula varchar(4000),
		area bigint,
		number_pp bigint,
		floor_id bigint,
		plan_number varchar(4000),
		document_number varchar(4000),
		reduction_ratio_id bigint,
		reduction_ratio_name varchar(4000),
		guid_pp varchar(38),
		area_pp bigint,
		number_room_pp varchar(1000),
		is_refitted_wo_permission smallint,
		is_common_prorerty_appartment smallint,
		is_common_prorerty_condominium smallint,
		kadastr_number varchar(128),
		id_in_source bigint,
		update_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_rooms', 'emp_id'))then
    	ALTER TABLE bti_rooms
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'dept_id'))then
    	ALTER TABLE bti_rooms
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'premise_id'))then
    	ALTER TABLE bti_rooms
        	ADD premise_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'purpose_id'))then
    	ALTER TABLE bti_rooms
        	ADD purpose_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'purpose_name'))then
    	ALTER TABLE bti_rooms
        	ADD purpose_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'special_purpose_id'))then
    	ALTER TABLE bti_rooms
        	ADD special_purpose_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'special_purpose_name'))then
    	ALTER TABLE bti_rooms
        	ADD special_purpose_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'area_kind_id'))then
    	ALTER TABLE bti_rooms
        	ADD area_kind_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'area_kind_name'))then
    	ALTER TABLE bti_rooms
        	ADD area_kind_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'area_type_id'))then
    	ALTER TABLE bti_rooms
        	ADD area_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'area_type_name'))then
    	ALTER TABLE bti_rooms
        	ADD area_type_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'height'))then
    	ALTER TABLE bti_rooms
        	ADD height bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'survey_date'))then
    	ALTER TABLE bti_rooms
        	ADD survey_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'area_calculation_formula'))then
    	ALTER TABLE bti_rooms
        	ADD area_calculation_formula varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'area'))then
    	ALTER TABLE bti_rooms
        	ADD area bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'number_pp'))then
    	ALTER TABLE bti_rooms
        	ADD number_pp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'floor_id'))then
    	ALTER TABLE bti_rooms
        	ADD floor_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'plan_number'))then
    	ALTER TABLE bti_rooms
        	ADD plan_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'document_number'))then
    	ALTER TABLE bti_rooms
        	ADD document_number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'reduction_ratio_id'))then
    	ALTER TABLE bti_rooms
        	ADD reduction_ratio_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'reduction_ratio_name'))then
    	ALTER TABLE bti_rooms
        	ADD reduction_ratio_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'guid_pp'))then
    	ALTER TABLE bti_rooms
        	ADD guid_pp varchar(38);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'area_pp'))then
    	ALTER TABLE bti_rooms
        	ADD area_pp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'number_room_pp'))then
    	ALTER TABLE bti_rooms
        	ADD number_room_pp varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'is_refitted_wo_permission'))then
    	ALTER TABLE bti_rooms
        	ADD is_refitted_wo_permission smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'is_common_prorerty_appartment'))then
    	ALTER TABLE bti_rooms
        	ADD is_common_prorerty_appartment smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'is_common_prorerty_condominium'))then
    	ALTER TABLE bti_rooms
        	ADD is_common_prorerty_condominium smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'kadastr_number'))then
    	ALTER TABLE bti_rooms
        	ADD kadastr_number varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'id_in_source'))then
    	ALTER TABLE bti_rooms
        	ADD id_in_source bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_rooms', 'update_date'))then
    	ALTER TABLE bti_rooms
        	ADD update_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_base_tariff')) IS NULL)then
        CREATE TABLE insur_base_tariff
        (
            emp_id bigint NOT NULL,
		name varchar(30),
		value numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_base_tariff', 'emp_id'))then
    	ALTER TABLE insur_base_tariff
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_base_tariff', 'name'))then
    	ALTER TABLE insur_base_tariff
        	ADD name varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('insur_base_tariff', 'value'))then
    	ALTER TABLE insur_base_tariff
        	ADD value numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_scan_doc')) IS NULL)then
        CREATE TABLE insur_scan_doc
        (
            emp_id bigint NOT NULL,
		doc_date timestamp without time zone,
		fio_scan varchar(255),
		doc_number_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_scan_doc', 'emp_id'))then
    	ALTER TABLE insur_scan_doc
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_scan_doc', 'doc_date'))then
    	ALTER TABLE insur_scan_doc
        	ADD doc_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_scan_doc', 'fio_scan'))then
    	ALTER TABLE insur_scan_doc
        	ADD fio_scan varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_scan_doc', 'doc_number_id'))then
    	ALTER TABLE insur_scan_doc
        	ADD doc_number_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_damage_assessment_radio_tv_phone')) IS NULL)then
        CREATE TABLE insur_damage_assessment_radio_tv_phone
        (
            id bigint NOT NULL,
		building_type varchar(255),
		wires numeric(10, 4),
		input_device numeric(10, 4),
		construction_type varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'id'))then
    	ALTER TABLE insur_damage_assessment_radio_tv_phone
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'building_type'))then
    	ALTER TABLE insur_damage_assessment_radio_tv_phone
        	ADD building_type varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'wires'))then
    	ALTER TABLE insur_damage_assessment_radio_tv_phone
        	ADD wires numeric(10, 4);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'input_device'))then
    	ALTER TABLE insur_damage_assessment_radio_tv_phone
        	ADD input_device numeric(10, 4);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_radio_tv_phone', 'construction_type'))then
    	ALTER TABLE insur_damage_assessment_radio_tv_phone
        	ADD construction_type varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ref_addr_district')) IS NULL)then
        CREATE TABLE ref_addr_district
        (
            district_id bigint NOT NULL,
		okrug_id bigint,
		subject_rf_id bigint,
		full_name varchar(1000),
		short_name varchar(1000),
		name_for_sort varchar(1000),
		steks_code bigint,
		omk_code varchar(100),
		type_ref bigint,
		name varchar(1000),
		code_givc varchar(500)
        );
    else
            
	if(not core_updstru_checkexistcolumn('ref_addr_district', 'district_id'))then
    	ALTER TABLE ref_addr_district
        	ADD district_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'okrug_id'))then
    	ALTER TABLE ref_addr_district
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'subject_rf_id'))then
    	ALTER TABLE ref_addr_district
        	ADD subject_rf_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'full_name'))then
    	ALTER TABLE ref_addr_district
        	ADD full_name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'short_name'))then
    	ALTER TABLE ref_addr_district
        	ADD short_name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'name_for_sort'))then
    	ALTER TABLE ref_addr_district
        	ADD name_for_sort varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'steks_code'))then
    	ALTER TABLE ref_addr_district
        	ADD steks_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'omk_code'))then
    	ALTER TABLE ref_addr_district
        	ADD omk_code varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'type_ref'))then
    	ALTER TABLE ref_addr_district
        	ADD type_ref bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'name'))then
    	ALTER TABLE ref_addr_district
        	ADD name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_district', 'code_givc'))then
    	ALTER TABLE ref_addr_district
        	ADD code_givc varchar(500);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ref_addr_street')) IS NULL)then
        CREATE TABLE ref_addr_street
        (
            street_id bigint NOT NULL,
		pse_id bigint,
		town_id bigint,
		subject_rf_id bigint,
		full_name varchar(1000),
		short_name varchar(1000),
		name_for_sort varchar(1000),
		steks_code bigint,
		omk_code varchar(100),
		type_ref bigint,
		kladr_code varchar(17),
		name varchar(1000),
		code_givc varchar(500)
        );
    else
            
	if(not core_updstru_checkexistcolumn('ref_addr_street', 'street_id'))then
    	ALTER TABLE ref_addr_street
        	ADD street_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'pse_id'))then
    	ALTER TABLE ref_addr_street
        	ADD pse_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'town_id'))then
    	ALTER TABLE ref_addr_street
        	ADD town_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'subject_rf_id'))then
    	ALTER TABLE ref_addr_street
        	ADD subject_rf_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'full_name'))then
    	ALTER TABLE ref_addr_street
        	ADD full_name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'short_name'))then
    	ALTER TABLE ref_addr_street
        	ADD short_name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'name_for_sort'))then
    	ALTER TABLE ref_addr_street
        	ADD name_for_sort varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'steks_code'))then
    	ALTER TABLE ref_addr_street
        	ADD steks_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'omk_code'))then
    	ALTER TABLE ref_addr_street
        	ADD omk_code varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'type_ref'))then
    	ALTER TABLE ref_addr_street
        	ADD type_ref bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'kladr_code'))then
    	ALTER TABLE ref_addr_street
        	ADD kladr_code varchar(17);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'name'))then
    	ALTER TABLE ref_addr_street
        	ADD name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_street', 'code_givc'))then
    	ALTER TABLE ref_addr_street
        	ADD code_givc varchar(500);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_import_log')) IS NULL)then
        CREATE TABLE bti_import_log
        (
            bti_id bigint NOT NULL,
		num_cadnum varchar(255),
		unom varchar(255),
		is_new smallint,
		alt_building_id bigint,
		dateedit timestamp without time zone,
		is_error smallint,
		message varchar(255),
		error_id bigint,
		task_id bigint,
		insert_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_import_log', 'bti_id'))then
    	ALTER TABLE bti_import_log
        	ADD bti_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'num_cadnum'))then
    	ALTER TABLE bti_import_log
        	ADD num_cadnum varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'unom'))then
    	ALTER TABLE bti_import_log
        	ADD unom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'is_new'))then
    	ALTER TABLE bti_import_log
        	ADD is_new smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'alt_building_id'))then
    	ALTER TABLE bti_import_log
        	ADD alt_building_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'dateedit'))then
    	ALTER TABLE bti_import_log
        	ADD dateedit timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'is_error'))then
    	ALTER TABLE bti_import_log
        	ADD is_error smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'message'))then
    	ALTER TABLE bti_import_log
        	ADD message varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'error_id'))then
    	ALTER TABLE bti_import_log
        	ADD error_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'task_id'))then
    	ALTER TABLE bti_import_log
        	ADD task_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_import_log', 'insert_date'))then
    	ALTER TABLE bti_import_log
        	ADD insert_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_file_package')) IS NULL)then
        CREATE TABLE insur_input_file_package
        (
            id bigint NOT NULL,
		period_reg_date timestamp without time zone,
		okrug_id bigint,
		count_district bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_input_file_package', 'id'))then
    	ALTER TABLE insur_input_file_package
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file_package', 'period_reg_date'))then
    	ALTER TABLE insur_input_file_package
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file_package', 'okrug_id'))then
    	ALTER TABLE insur_input_file_package
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file_package', 'count_district'))then
    	ALTER TABLE insur_input_file_package
        	ADD count_district bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_part_compensation')) IS NULL)then
        CREATE TABLE insur_part_compensation
        (
            emp_id bigint NOT NULL,
		date_begin timestamp without time zone,
		"type" integer,
		ic_value numeric,
		city_value numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_part_compensation', 'emp_id'))then
    	ALTER TABLE insur_part_compensation
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_part_compensation', 'date_begin'))then
    	ALTER TABLE insur_part_compensation
        	ADD date_begin timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_part_compensation', 'type'))then
    	ALTER TABLE insur_part_compensation
        	ADD "type" integer;
	end if;


	if(not core_updstru_checkexistcolumn('insur_part_compensation', 'ic_value'))then
    	ALTER TABLE insur_part_compensation
        	ADD ic_value numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_part_compensation', 'city_value'))then
    	ALTER TABLE insur_part_compensation
        	ADD city_value numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_share_responsibility_ic_city')) IS NULL)then
        CREATE TABLE insur_share_responsibility_ic_city
        (
            id bigint NOT NULL,
		date_begin timestamp without time zone,
		"type" varchar(255),
		ic_share bigint,
		city_share bigint,
		note varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'id'))then
    	ALTER TABLE insur_share_responsibility_ic_city
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'date_begin'))then
    	ALTER TABLE insur_share_responsibility_ic_city
        	ADD date_begin timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'type'))then
    	ALTER TABLE insur_share_responsibility_ic_city
        	ADD "type" varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'ic_share'))then
    	ALTER TABLE insur_share_responsibility_ic_city
        	ADD ic_share bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'city_share'))then
    	ALTER TABLE insur_share_responsibility_ic_city
        	ADD city_share bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_share_responsibility_ic_city', 'note'))then
    	ALTER TABLE insur_share_responsibility_ic_city
        	ADD note varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_dop_all_property')) IS NULL)then
        CREATE TABLE insur_dop_all_property
        (
            emp_id bigint NOT NULL,
		contract_id bigint,
		kod bigint,
		unom bigint,
		ndog varchar(255),
		ndogdat timestamp without time zone,
		ndops numeric,
		st1_new numeric,
		st2_new numeric,
		st3_new numeric,
		ss1_new numeric,
		ss2_new numeric,
		ss3_new numeric,
		part_new numeric,
		part_city_new numeric,
		paysign_new numeric,
		ras_pripay_new numeric,
		link_id_file bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'emp_id'))then
    	ALTER TABLE insur_dop_all_property
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'contract_id'))then
    	ALTER TABLE insur_dop_all_property
        	ADD contract_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'kod'))then
    	ALTER TABLE insur_dop_all_property
        	ADD kod bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'unom'))then
    	ALTER TABLE insur_dop_all_property
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'ndog'))then
    	ALTER TABLE insur_dop_all_property
        	ADD ndog varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'ndogdat'))then
    	ALTER TABLE insur_dop_all_property
        	ADD ndogdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'ndops'))then
    	ALTER TABLE insur_dop_all_property
        	ADD ndops numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'st1_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD st1_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'st2_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD st2_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'st3_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD st3_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'ss1_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD ss1_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'ss2_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD ss2_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'ss3_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD ss3_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'part_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD part_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'part_city_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD part_city_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'paysign_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD paysign_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'ras_pripay_new'))then
    	ALTER TABLE insur_dop_all_property
        	ADD ras_pripay_new numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_dop_all_property', 'link_id_file'))then
    	ALTER TABLE insur_dop_all_property
        	ADD link_id_file bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_insurance_organization')) IS NULL)then
        CREATE TABLE insur_insurance_organization
        (
            id bigint NOT NULL,
		full_name varchar(255) NOT NULL,
		short_name varchar(50) NOT NULL,
		code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_insurance_organization', 'id'))then
    	ALTER TABLE insur_insurance_organization
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_insurance_organization', 'full_name'))then
    	ALTER TABLE insur_insurance_organization
        	ADD full_name varchar(255) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_insurance_organization', 'short_name'))then
    	ALTER TABLE insur_insurance_organization
        	ADD short_name varchar(50) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_insurance_organization', 'code'))then
    	ALTER TABLE insur_insurance_organization
        	ADD code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_policy_svd')) IS NULL)then
        CREATE TABLE insur_policy_svd
        (
            emp_id bigint NOT NULL,
		type_doc_code bigint,
		fsp_id bigint,
		link_id_file bigint,
		link_id_file_end bigint,
		insurance_organization_id bigint,
		okrug_id bigint,
		district_id bigint,
		org_type bigint,
		org_id bigint,
		plat_id bigint,
		unom bigint,
		unkva varchar(255),
		nom varchar(255),
		nomi varchar(255),
		kvnom varchar(255),
		kolgp bigint,
		flatstatus bigint,
		fopl numeric,
		opl numeric,
		kodpl bigint,
		ls bigint,
		npol varchar(255),
		dat timestamp without time zone,
		vznos numeric,
		pralt_code bigint,
		pr bigint,
		ss numeric,
		kolds bigint,
		soplvz numeric,
		dat_end timestamp without time zone,
		type_doc varchar(255),
		type_prop varchar(255),
		type_prop_code bigint,
		pralt varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'emp_id'))then
    	ALTER TABLE insur_policy_svd
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'type_doc_code'))then
    	ALTER TABLE insur_policy_svd
        	ADD type_doc_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'fsp_id'))then
    	ALTER TABLE insur_policy_svd
        	ADD fsp_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'link_id_file'))then
    	ALTER TABLE insur_policy_svd
        	ADD link_id_file bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'link_id_file_end'))then
    	ALTER TABLE insur_policy_svd
        	ADD link_id_file_end bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'insurance_organization_id'))then
    	ALTER TABLE insur_policy_svd
        	ADD insurance_organization_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'okrug_id'))then
    	ALTER TABLE insur_policy_svd
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'district_id'))then
    	ALTER TABLE insur_policy_svd
        	ADD district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'org_type'))then
    	ALTER TABLE insur_policy_svd
        	ADD org_type bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'org_id'))then
    	ALTER TABLE insur_policy_svd
        	ADD org_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'plat_id'))then
    	ALTER TABLE insur_policy_svd
        	ADD plat_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'unom'))then
    	ALTER TABLE insur_policy_svd
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'unkva'))then
    	ALTER TABLE insur_policy_svd
        	ADD unkva varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'nom'))then
    	ALTER TABLE insur_policy_svd
        	ADD nom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'nomi'))then
    	ALTER TABLE insur_policy_svd
        	ADD nomi varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'kvnom'))then
    	ALTER TABLE insur_policy_svd
        	ADD kvnom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'kolgp'))then
    	ALTER TABLE insur_policy_svd
        	ADD kolgp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'flatstatus'))then
    	ALTER TABLE insur_policy_svd
        	ADD flatstatus bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'fopl'))then
    	ALTER TABLE insur_policy_svd
        	ADD fopl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'opl'))then
    	ALTER TABLE insur_policy_svd
        	ADD opl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'kodpl'))then
    	ALTER TABLE insur_policy_svd
        	ADD kodpl bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'ls'))then
    	ALTER TABLE insur_policy_svd
        	ADD ls bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'npol'))then
    	ALTER TABLE insur_policy_svd
        	ADD npol varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'dat'))then
    	ALTER TABLE insur_policy_svd
        	ADD dat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'vznos'))then
    	ALTER TABLE insur_policy_svd
        	ADD vznos numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'pralt_code'))then
    	ALTER TABLE insur_policy_svd
        	ADD pralt_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'pr'))then
    	ALTER TABLE insur_policy_svd
        	ADD pr bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'ss'))then
    	ALTER TABLE insur_policy_svd
        	ADD ss numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'kolds'))then
    	ALTER TABLE insur_policy_svd
        	ADD kolds bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'soplvz'))then
    	ALTER TABLE insur_policy_svd
        	ADD soplvz numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'dat_end'))then
    	ALTER TABLE insur_policy_svd
        	ADD dat_end timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'type_doc'))then
    	ALTER TABLE insur_policy_svd
        	ADD type_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'type_prop'))then
    	ALTER TABLE insur_policy_svd
        	ADD type_prop varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'type_prop_code'))then
    	ALTER TABLE insur_policy_svd
        	ADD type_prop_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_policy_svd', 'pralt'))then
    	ALTER TABLE insur_policy_svd
        	ADD pralt varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_nach')) IS NULL)then
        CREATE TABLE insur_input_nach
        (
            emp_id bigint NOT NULL,
		link_id_file bigint,
		fsp_id bigint,
		type_source varchar(255),
		status_identif varchar(255),
		period_reg_date timestamp without time zone,
		period timestamp without time zone,
		district_id bigint,
		kod bigint,
		unom bigint,
		adres_t varchar(2000),
		unkva varchar(255),
		nomi varchar(255),
		nom varchar(255),
		kvnom varchar(255),
		flat_status_id bigint,
		flat_type_id bigint,
		kolgp bigint,
		fopl numeric,
		opl numeric,
		kodpl varchar(255),
		ls bigint,
		sum_nach numeric,
		flag_unom_no bigint,
		fio varchar(255),
		type_source_code bigint,
		status_identif_code bigint,
		load_status varchar(255),
		load_status_code bigint,
		criteria_json text
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_input_nach', 'emp_id'))then
    	ALTER TABLE insur_input_nach
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'link_id_file'))then
    	ALTER TABLE insur_input_nach
        	ADD link_id_file bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'fsp_id'))then
    	ALTER TABLE insur_input_nach
        	ADD fsp_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'type_source'))then
    	ALTER TABLE insur_input_nach
        	ADD type_source varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'status_identif'))then
    	ALTER TABLE insur_input_nach
        	ADD status_identif varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'period_reg_date'))then
    	ALTER TABLE insur_input_nach
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'period'))then
    	ALTER TABLE insur_input_nach
        	ADD period timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'district_id'))then
    	ALTER TABLE insur_input_nach
        	ADD district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'kod'))then
    	ALTER TABLE insur_input_nach
        	ADD kod bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'unom'))then
    	ALTER TABLE insur_input_nach
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'adres_t'))then
    	ALTER TABLE insur_input_nach
        	ADD adres_t varchar(2000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'unkva'))then
    	ALTER TABLE insur_input_nach
        	ADD unkva varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'nomi'))then
    	ALTER TABLE insur_input_nach
        	ADD nomi varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'nom'))then
    	ALTER TABLE insur_input_nach
        	ADD nom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'kvnom'))then
    	ALTER TABLE insur_input_nach
        	ADD kvnom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'flat_status_id'))then
    	ALTER TABLE insur_input_nach
        	ADD flat_status_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'flat_type_id'))then
    	ALTER TABLE insur_input_nach
        	ADD flat_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'kolgp'))then
    	ALTER TABLE insur_input_nach
        	ADD kolgp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'fopl'))then
    	ALTER TABLE insur_input_nach
        	ADD fopl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'opl'))then
    	ALTER TABLE insur_input_nach
        	ADD opl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'kodpl'))then
    	ALTER TABLE insur_input_nach
        	ADD kodpl varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'ls'))then
    	ALTER TABLE insur_input_nach
        	ADD ls bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'sum_nach'))then
    	ALTER TABLE insur_input_nach
        	ADD sum_nach numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'flag_unom_no'))then
    	ALTER TABLE insur_input_nach
        	ADD flag_unom_no bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'fio'))then
    	ALTER TABLE insur_input_nach
        	ADD fio varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'type_source_code'))then
    	ALTER TABLE insur_input_nach
        	ADD type_source_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'status_identif_code'))then
    	ALTER TABLE insur_input_nach
        	ADD status_identif_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'load_status'))then
    	ALTER TABLE insur_input_nach
        	ADD load_status varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'load_status_code'))then
    	ALTER TABLE insur_input_nach
        	ADD load_status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_nach', 'criteria_json'))then
    	ALTER TABLE insur_input_nach
        	ADD criteria_json text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_build_parcel_o')) IS NULL)then
        CREATE TABLE ehd_build_parcel_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_build_parcel_o', 'id'))then
    	ALTER TABLE ehd_build_parcel_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_o', 'info'))then
    	ALTER TABLE ehd_build_parcel_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_o', 'deleted'))then
    	ALTER TABLE ehd_build_parcel_o
        	ADD deleted bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_o', 'uid'))then
    	ALTER TABLE ehd_build_parcel_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_o', 'enddatechange'))then
    	ALTER TABLE ehd_build_parcel_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_location_q')) IS NULL)then
        CREATE TABLE ehd_location_q
        (
            id bigint,
		emp_id bigint NOT NULL,
		actual bigint,
		status bigint,
		dept_id bigint,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		load_date timestamp without time zone,
		id_location_ehd bigint,
		parcel_id bigint,
		person_id bigint,
		organization_id bigint,
		building_parcel_id bigint,
		global_id bigint,
		placed varchar(5),
		in_bounds varchar(50),
		code_okato varchar(11),
		code_kladr varchar(20),
		postal_code varchar(6),
		region varchar(50),
		district varchar(4000),
		city varchar(4000),
		urban_district varchar(306),
		soviet_village varchar(266),
		locality varchar(4000),
		street varchar(4000),
		level1 varchar(306),
		level2 varchar(306),
		level3 varchar(306),
		apartment varchar(306),
		full_address varchar(4000),
		address_total varchar(4000),
		other varchar(2500)
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_location_q', 'id'))then
    	ALTER TABLE ehd_location_q
        	ADD id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'emp_id'))then
    	ALTER TABLE ehd_location_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'actual'))then
    	ALTER TABLE ehd_location_q
        	ADD actual bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'status'))then
    	ALTER TABLE ehd_location_q
        	ADD status bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'dept_id'))then
    	ALTER TABLE ehd_location_q
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 's_'))then
    	ALTER TABLE ehd_location_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'po_'))then
    	ALTER TABLE ehd_location_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'load_date'))then
    	ALTER TABLE ehd_location_q
        	ADD load_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'id_location_ehd'))then
    	ALTER TABLE ehd_location_q
        	ADD id_location_ehd bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'parcel_id'))then
    	ALTER TABLE ehd_location_q
        	ADD parcel_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'person_id'))then
    	ALTER TABLE ehd_location_q
        	ADD person_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'organization_id'))then
    	ALTER TABLE ehd_location_q
        	ADD organization_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'building_parcel_id'))then
    	ALTER TABLE ehd_location_q
        	ADD building_parcel_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'global_id'))then
    	ALTER TABLE ehd_location_q
        	ADD global_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'placed'))then
    	ALTER TABLE ehd_location_q
        	ADD placed varchar(5);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'in_bounds'))then
    	ALTER TABLE ehd_location_q
        	ADD in_bounds varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'code_okato'))then
    	ALTER TABLE ehd_location_q
        	ADD code_okato varchar(11);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'code_kladr'))then
    	ALTER TABLE ehd_location_q
        	ADD code_kladr varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'postal_code'))then
    	ALTER TABLE ehd_location_q
        	ADD postal_code varchar(6);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'region'))then
    	ALTER TABLE ehd_location_q
        	ADD region varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'district'))then
    	ALTER TABLE ehd_location_q
        	ADD district varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'city'))then
    	ALTER TABLE ehd_location_q
        	ADD city varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'urban_district'))then
    	ALTER TABLE ehd_location_q
        	ADD urban_district varchar(306);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'soviet_village'))then
    	ALTER TABLE ehd_location_q
        	ADD soviet_village varchar(266);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'locality'))then
    	ALTER TABLE ehd_location_q
        	ADD locality varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'street'))then
    	ALTER TABLE ehd_location_q
        	ADD street varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'level1'))then
    	ALTER TABLE ehd_location_q
        	ADD level1 varchar(306);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'level2'))then
    	ALTER TABLE ehd_location_q
        	ADD level2 varchar(306);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'level3'))then
    	ALTER TABLE ehd_location_q
        	ADD level3 varchar(306);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'apartment'))then
    	ALTER TABLE ehd_location_q
        	ADD apartment varchar(306);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'full_address'))then
    	ALTER TABLE ehd_location_q
        	ADD full_address varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'address_total'))then
    	ALTER TABLE ehd_location_q
        	ADD address_total varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_q', 'other'))then
    	ALTER TABLE ehd_location_q
        	ADD other varchar(2500);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_location_o')) IS NULL)then
        CREATE TABLE ehd_location_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_location_o', 'id'))then
    	ALTER TABLE ehd_location_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_o', 'info'))then
    	ALTER TABLE ehd_location_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_o', 'deleted'))then
    	ALTER TABLE ehd_location_o
        	ADD deleted bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_o', 'uid'))then
    	ALTER TABLE ehd_location_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_location_o', 'enddatechange'))then
    	ALTER TABLE ehd_location_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fm_reports_savedreport')) IS NULL)then
        CREATE TABLE fm_reports_savedreport
        (
            id bigint NOT NULL,
		code bigint,
		internal_name varchar(128),
		title varchar(128),
		user_id bigint,
		object_register_id bigint,
		object_id bigint,
		create_date timestamp without time zone,
		report_number varchar(100),
		comments varchar(1000),
		parameters text,
		status bigint,
		end_date timestamp without time zone,
		result_message varchar,
		is_deleted smallint,
		file_type varchar(10),
		section varchar(10)
        );
    else
            
	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'id'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'code'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'internal_name'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD internal_name varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'title'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD title varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'user_id'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'object_register_id'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD object_register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'object_id'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'create_date'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD create_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'report_number'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD report_number varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'comments'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD comments varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'parameters'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD parameters text;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'status'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD status bigint;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'end_date'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD end_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'result_message'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD result_message varchar;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'is_deleted'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD is_deleted smallint;
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'file_type'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD file_type varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('fm_reports_savedreport', 'section'))then
    	ALTER TABLE fm_reports_savedreport
        	ADD section varchar(10);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fm_podpisant')) IS NULL)then
        CREATE TABLE fm_podpisant
        (
            id integer NOT NULL,
		code varchar(1000),
		name varchar(1000),
		post varchar(1000),
		text varchar(1000),
		is_deleted numeric(1, 0) NOT NULL DEFAULT 0
        );
    else
            
	if(not core_updstru_checkexistcolumn('fm_podpisant', 'id'))then
    	ALTER TABLE fm_podpisant
        	ADD id integer NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('fm_podpisant', 'code'))then
    	ALTER TABLE fm_podpisant
        	ADD code varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('fm_podpisant', 'name'))then
    	ALTER TABLE fm_podpisant
        	ADD name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('fm_podpisant', 'post'))then
    	ALTER TABLE fm_podpisant
        	ADD post varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('fm_podpisant', 'text'))then
    	ALTER TABLE fm_podpisant
        	ADD text varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('fm_podpisant', 'is_deleted'))then
    	ALTER TABLE fm_podpisant
        	ADD is_deleted numeric(1, 0) NOT NULL DEFAULT 0;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_tariff')) IS NULL)then
        CREATE TABLE insur_tariff
        (
            id bigint NOT NULL,
		date_begin timestamp without time zone,
		value numeric(18, 2)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_tariff', 'id'))then
    	ALTER TABLE insur_tariff
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_tariff', 'date_begin'))then
    	ALTER TABLE insur_tariff
        	ADD date_begin timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_tariff', 'value'))then
    	ALTER TABLE insur_tariff
        	ADD value numeric(18, 2);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_building_o')) IS NULL)then
        CREATE TABLE insur_building_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_building_o', 'id'))then
    	ALTER TABLE insur_building_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_o', 'info'))then
    	ALTER TABLE insur_building_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_o', 'deleted'))then
    	ALTER TABLE insur_building_o
        	ADD deleted bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_o', 'uid'))then
    	ALTER TABLE insur_building_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_o', 'enddatechange'))then
    	ALTER TABLE insur_building_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_flat_o')) IS NULL)then
        CREATE TABLE insur_flat_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_flat_o', 'id'))then
    	ALTER TABLE insur_flat_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_o', 'info'))then
    	ALTER TABLE insur_flat_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_o', 'deleted'))then
    	ALTER TABLE insur_flat_o
        	ADD deleted bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_o', 'uid'))then
    	ALTER TABLE insur_flat_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_o', 'enddatechange'))then
    	ALTER TABLE insur_flat_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_actual_cost_ratio')) IS NULL)then
        CREATE TABLE insur_actual_cost_ratio
        (
            id bigint NOT NULL,
		date_begin timestamp without time zone,
		value numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_actual_cost_ratio', 'id'))then
    	ALTER TABLE insur_actual_cost_ratio
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_actual_cost_ratio', 'date_begin'))then
    	ALTER TABLE insur_actual_cost_ratio
        	ADD date_begin timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_actual_cost_ratio', 'value'))then
    	ALTER TABLE insur_actual_cost_ratio
        	ADD value numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_build_parcel_q')) IS NULL)then
        CREATE TABLE ehd_build_parcel_q
        (
            id bigint,
		emp_id bigint NOT NULL,
		actual bigint,
		status bigint,
		dept_id bigint,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		load_date timestamp without time zone,
		building_parcel_id bigint,
		global_id bigint,
		name varchar(4000),
		assignation_code varchar(4000),
		area numeric,
		notes varchar(4000),
		assignation_name varchar(255),
		assignation_name_id bigint,
		degree_readiness numeric,
		actual_ehd bigint,
		update_date_ehd timestamp without time zone,
		"type" varchar(255),
		type_id bigint,
		subbuildings varchar(4000),
		object_id varchar(50),
		package_id bigint,
		actual_on_date timestamp without time zone,
		floors smallint,
		elements_construct varchar(255),
		old_number varchar(200)
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'emp_id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'actual'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD actual bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'status'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD status bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'dept_id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 's_'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'po_'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'load_date'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD load_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'building_parcel_id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD building_parcel_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'global_id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD global_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'name'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'assignation_code'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD assignation_code varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'area'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD area numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'notes'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD notes varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'assignation_name'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD assignation_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'assignation_name_id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD assignation_name_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'degree_readiness'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD degree_readiness numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'actual_ehd'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD actual_ehd bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'update_date_ehd'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD update_date_ehd timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'type'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD "type" varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'type_id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'subbuildings'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD subbuildings varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'object_id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD object_id varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'package_id'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD package_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'actual_on_date'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD actual_on_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'floors'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD floors smallint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'elements_construct'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD elements_construct varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_build_parcel_q', 'old_number'))then
    	ALTER TABLE ehd_build_parcel_q
        	ADD old_number varchar(200);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_right_o')) IS NULL)then
        CREATE TABLE ehd_right_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_right_o', 'id'))then
    	ALTER TABLE ehd_right_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_o', 'info'))then
    	ALTER TABLE ehd_right_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_o', 'deleted'))then
    	ALTER TABLE ehd_right_o
        	ADD deleted bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_o', 'uid'))then
    	ALTER TABLE ehd_right_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_o', 'enddatechange'))then
    	ALTER TABLE ehd_right_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_egrp_o')) IS NULL)then
        CREATE TABLE ehd_egrp_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_egrp_o', 'id'))then
    	ALTER TABLE ehd_egrp_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_o', 'info'))then
    	ALTER TABLE ehd_egrp_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_o', 'deleted'))then
    	ALTER TABLE ehd_egrp_o
        	ADD deleted bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_o', 'uid'))then
    	ALTER TABLE ehd_egrp_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_o', 'enddatechange'))then
    	ALTER TABLE ehd_egrp_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_egrp_q')) IS NULL)then
        CREATE TABLE ehd_egrp_q
        (
            id bigint NOT NULL,
		emp_id bigint NOT NULL,
		actual bigint NOT NULL,
		status bigint NOT NULL,
		dept_id bigint NOT NULL,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		id_in_ehd_egrp bigint,
		area numeric,
		global_id bigint,
		objt_cd varchar(40),
		objecttp_cd varchar(50),
		regtp_cd varchar(50),
		disttp_cd varchar(20),
		citytp_cd varchar(50),
		loctp_cd varchar(40),
		strtp_cd varchar(50),
		level1tp_cd varchar(50),
		level2tp_cd varchar(50),
		level3tp_cd varchar(50),
		aparttp_cd varchar(50),
		purposetp_cd varchar(50),
		objectst_cd varchar(20),
		actst_cd varchar(20),
		fakt_cd varchar(511),
		bydoc_cd varchar(511),
		groundcat_cd varchar(255),
		purpose varchar(4000),
		invnum varchar(100),
		literbti varchar(60),
		addr_refmark varchar(255),
		addr_id varchar(30),
		addr_cdcountry varchar(3),
		addr_cdokato varchar(11),
		addr_postcd varchar(6),
		addr_dist_name varchar(255),
		addr_dist_cd varchar(50),
		addr_city_name varchar(255),
		addr_city_cd varchar(50),
		addr_loc_name varchar(255),
		addr_loc_cd varchar(50),
		addr_str_name varchar(255),
		addr_str_cd varchar(50),
		addr_level1_num varchar(255),
		addr_level2_num varchar(255),
		addr_level3_num varchar(255),
		addr_apart varchar(255),
		addr_other varchar(4000),
		addr_note varchar(4000),
		num_cadnum varchar(400),
		num_condnum varchar(3000),
		name varchar(4000),
		floor_gr varchar(100),
		floor_und varchar(100),
		techar_height varchar(22),
		techar_lenght varchar(22),
		techar_vol varchar(22),
		num_floor varchar(200),
		num_flat varchar(50),
		regdt timestamp without time zone,
		brkdt timestamp without time zone,
		mdfdt timestamp without time zone,
		updt timestamp without time zone,
		act_dt timestamp without time zone,
		object_id varchar(30),
		update_date timestamp without time zone,
		actual_id bigint,
		actual_on_date timestamp without time zone,
		address_total varchar(4000),
		json varchar(100),
		load_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'id'))then
    	ALTER TABLE ehd_egrp_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'emp_id'))then
    	ALTER TABLE ehd_egrp_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'actual'))then
    	ALTER TABLE ehd_egrp_q
        	ADD actual bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'status'))then
    	ALTER TABLE ehd_egrp_q
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'dept_id'))then
    	ALTER TABLE ehd_egrp_q
        	ADD dept_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 's_'))then
    	ALTER TABLE ehd_egrp_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'po_'))then
    	ALTER TABLE ehd_egrp_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'id_in_ehd_egrp'))then
    	ALTER TABLE ehd_egrp_q
        	ADD id_in_ehd_egrp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'area'))then
    	ALTER TABLE ehd_egrp_q
        	ADD area numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'global_id'))then
    	ALTER TABLE ehd_egrp_q
        	ADD global_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'objt_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD objt_cd varchar(40);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'objecttp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD objecttp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'regtp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD regtp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'disttp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD disttp_cd varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'citytp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD citytp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'loctp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD loctp_cd varchar(40);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'strtp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD strtp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'level1tp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD level1tp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'level2tp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD level2tp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'level3tp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD level3tp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'aparttp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD aparttp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'purposetp_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD purposetp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'objectst_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD objectst_cd varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'actst_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD actst_cd varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'fakt_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD fakt_cd varchar(511);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'bydoc_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD bydoc_cd varchar(511);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'groundcat_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD groundcat_cd varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'purpose'))then
    	ALTER TABLE ehd_egrp_q
        	ADD purpose varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'invnum'))then
    	ALTER TABLE ehd_egrp_q
        	ADD invnum varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'literbti'))then
    	ALTER TABLE ehd_egrp_q
        	ADD literbti varchar(60);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_refmark'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_refmark varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_id'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_id varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_cdcountry'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_cdcountry varchar(3);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_cdokato'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_cdokato varchar(11);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_postcd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_postcd varchar(6);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_dist_name'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_dist_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_dist_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_dist_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_city_name'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_city_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_city_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_city_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_loc_name'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_loc_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_loc_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_loc_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_str_name'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_str_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_str_cd'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_str_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_level1_num'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_level1_num varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_level2_num'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_level2_num varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_level3_num'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_level3_num varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_apart'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_apart varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_other'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_other varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'addr_note'))then
    	ALTER TABLE ehd_egrp_q
        	ADD addr_note varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'num_cadnum'))then
    	ALTER TABLE ehd_egrp_q
        	ADD num_cadnum varchar(400);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'num_condnum'))then
    	ALTER TABLE ehd_egrp_q
        	ADD num_condnum varchar(3000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'name'))then
    	ALTER TABLE ehd_egrp_q
        	ADD name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'floor_gr'))then
    	ALTER TABLE ehd_egrp_q
        	ADD floor_gr varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'floor_und'))then
    	ALTER TABLE ehd_egrp_q
        	ADD floor_und varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'techar_height'))then
    	ALTER TABLE ehd_egrp_q
        	ADD techar_height varchar(22);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'techar_lenght'))then
    	ALTER TABLE ehd_egrp_q
        	ADD techar_lenght varchar(22);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'techar_vol'))then
    	ALTER TABLE ehd_egrp_q
        	ADD techar_vol varchar(22);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'num_floor'))then
    	ALTER TABLE ehd_egrp_q
        	ADD num_floor varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'num_flat'))then
    	ALTER TABLE ehd_egrp_q
        	ADD num_flat varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'regdt'))then
    	ALTER TABLE ehd_egrp_q
        	ADD regdt timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'brkdt'))then
    	ALTER TABLE ehd_egrp_q
        	ADD brkdt timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'mdfdt'))then
    	ALTER TABLE ehd_egrp_q
        	ADD mdfdt timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'updt'))then
    	ALTER TABLE ehd_egrp_q
        	ADD updt timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'act_dt'))then
    	ALTER TABLE ehd_egrp_q
        	ADD act_dt timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'object_id'))then
    	ALTER TABLE ehd_egrp_q
        	ADD object_id varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'update_date'))then
    	ALTER TABLE ehd_egrp_q
        	ADD update_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'actual_id'))then
    	ALTER TABLE ehd_egrp_q
        	ADD actual_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'actual_on_date'))then
    	ALTER TABLE ehd_egrp_q
        	ADD actual_on_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'address_total'))then
    	ALTER TABLE ehd_egrp_q
        	ADD address_total varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'json'))then
    	ALTER TABLE ehd_egrp_q
        	ADD json varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_egrp_q', 'load_date'))then
    	ALTER TABLE ehd_egrp_q
        	ADD load_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_right_q')) IS NULL)then
        CREATE TABLE ehd_right_q
        (
            id bigint NOT NULL,
		emp_id bigint NOT NULL,
		actual bigint NOT NULL,
		status bigint NOT NULL,
		dept_id bigint NOT NULL,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		egrp_id bigint,
		global_id bigint,
		id_ehd_right bigint,
		mdfdt timestamp without time zone,
		object_id varchar(30),
		reg_close_regdt timestamp without time zone,
		reg_close_regnum varchar(50),
		reg_open_regdt timestamp without time zone,
		reg_open_regnum varchar(50),
		rightst_cd varchar(14),
		righttp_cd varchar(50),
		right_key varchar(30),
		sharecomflat_text varchar(4000),
		sharecom_text varchar(4000),
		share_den numeric,
		share_num numeric,
		share_text varchar(4000),
		tp_name varchar(400),
		sharecomflat_den numeric,
		sharecomflat_num numeric,
		sharecom_den numeric,
		sharecom_num numeric,
		load_date timestamp without time zone,
		righttp_cd_code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_right_q', 'id'))then
    	ALTER TABLE ehd_right_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'emp_id'))then
    	ALTER TABLE ehd_right_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'actual'))then
    	ALTER TABLE ehd_right_q
        	ADD actual bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'status'))then
    	ALTER TABLE ehd_right_q
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'dept_id'))then
    	ALTER TABLE ehd_right_q
        	ADD dept_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 's_'))then
    	ALTER TABLE ehd_right_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'po_'))then
    	ALTER TABLE ehd_right_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'egrp_id'))then
    	ALTER TABLE ehd_right_q
        	ADD egrp_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'global_id'))then
    	ALTER TABLE ehd_right_q
        	ADD global_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'id_ehd_right'))then
    	ALTER TABLE ehd_right_q
        	ADD id_ehd_right bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'mdfdt'))then
    	ALTER TABLE ehd_right_q
        	ADD mdfdt timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'object_id'))then
    	ALTER TABLE ehd_right_q
        	ADD object_id varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'reg_close_regdt'))then
    	ALTER TABLE ehd_right_q
        	ADD reg_close_regdt timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'reg_close_regnum'))then
    	ALTER TABLE ehd_right_q
        	ADD reg_close_regnum varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'reg_open_regdt'))then
    	ALTER TABLE ehd_right_q
        	ADD reg_open_regdt timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'reg_open_regnum'))then
    	ALTER TABLE ehd_right_q
        	ADD reg_open_regnum varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'rightst_cd'))then
    	ALTER TABLE ehd_right_q
        	ADD rightst_cd varchar(14);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'righttp_cd'))then
    	ALTER TABLE ehd_right_q
        	ADD righttp_cd varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'right_key'))then
    	ALTER TABLE ehd_right_q
        	ADD right_key varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'sharecomflat_text'))then
    	ALTER TABLE ehd_right_q
        	ADD sharecomflat_text varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'sharecom_text'))then
    	ALTER TABLE ehd_right_q
        	ADD sharecom_text varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'share_den'))then
    	ALTER TABLE ehd_right_q
        	ADD share_den numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'share_num'))then
    	ALTER TABLE ehd_right_q
        	ADD share_num numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'share_text'))then
    	ALTER TABLE ehd_right_q
        	ADD share_text varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'tp_name'))then
    	ALTER TABLE ehd_right_q
        	ADD tp_name varchar(400);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'sharecomflat_den'))then
    	ALTER TABLE ehd_right_q
        	ADD sharecomflat_den numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'sharecomflat_num'))then
    	ALTER TABLE ehd_right_q
        	ADD sharecomflat_num numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'sharecom_den'))then
    	ALTER TABLE ehd_right_q
        	ADD sharecom_den numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'sharecom_num'))then
    	ALTER TABLE ehd_right_q
        	ADD sharecom_num numeric;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'load_date'))then
    	ALTER TABLE ehd_right_q
        	ADD load_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_right_q', 'righttp_cd_code'))then
    	ALTER TABLE ehd_right_q
        	ADD righttp_cd_code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_register_q')) IS NULL)then
        CREATE TABLE ehd_register_q
        (
            id bigint,
		emp_id bigint NOT NULL,
		actual bigint,
		status bigint,
		dept_id bigint,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		load_date timestamp without time zone,
		building_parcel_id bigint,
		global_id bigint,
		cadastral_number_parent varchar(50),
		cadastral_number varchar(50),
		date_created timestamp without time zone,
		date_removed timestamp without time zone,
		state varchar(4000),
		state_id bigint,
		method varchar(4000),
		cadastral_number_oks varchar(50),
		cadastral_number_kk varchar(50),
		cadastral_number_flat varchar(50),
		totalass varchar(4000),
		assftp1 varchar(4000),
		assftp_cd varchar(4000),
		assftp1_code bigint,
		assftp_cd_code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_register_q', 'id'))then
    	ALTER TABLE ehd_register_q
        	ADD id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'emp_id'))then
    	ALTER TABLE ehd_register_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'actual'))then
    	ALTER TABLE ehd_register_q
        	ADD actual bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'status'))then
    	ALTER TABLE ehd_register_q
        	ADD status bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'dept_id'))then
    	ALTER TABLE ehd_register_q
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 's_'))then
    	ALTER TABLE ehd_register_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'po_'))then
    	ALTER TABLE ehd_register_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'load_date'))then
    	ALTER TABLE ehd_register_q
        	ADD load_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'building_parcel_id'))then
    	ALTER TABLE ehd_register_q
        	ADD building_parcel_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'global_id'))then
    	ALTER TABLE ehd_register_q
        	ADD global_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number_parent'))then
    	ALTER TABLE ehd_register_q
        	ADD cadastral_number_parent varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number'))then
    	ALTER TABLE ehd_register_q
        	ADD cadastral_number varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'date_created'))then
    	ALTER TABLE ehd_register_q
        	ADD date_created timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'date_removed'))then
    	ALTER TABLE ehd_register_q
        	ADD date_removed timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'state'))then
    	ALTER TABLE ehd_register_q
        	ADD state varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'state_id'))then
    	ALTER TABLE ehd_register_q
        	ADD state_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'method'))then
    	ALTER TABLE ehd_register_q
        	ADD method varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number_oks'))then
    	ALTER TABLE ehd_register_q
        	ADD cadastral_number_oks varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number_kk'))then
    	ALTER TABLE ehd_register_q
        	ADD cadastral_number_kk varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'cadastral_number_flat'))then
    	ALTER TABLE ehd_register_q
        	ADD cadastral_number_flat varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'totalass'))then
    	ALTER TABLE ehd_register_q
        	ADD totalass varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'assftp1'))then
    	ALTER TABLE ehd_register_q
        	ADD assftp1 varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'assftp_cd'))then
    	ALTER TABLE ehd_register_q
        	ADD assftp_cd varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'assftp1_code'))then
    	ALTER TABLE ehd_register_q
        	ADD assftp1_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_q', 'assftp_cd_code'))then
    	ALTER TABLE ehd_register_q
        	ADD assftp_cd_code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_register_o')) IS NULL)then
        CREATE TABLE ehd_register_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_register_o', 'id'))then
    	ALTER TABLE ehd_register_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_o', 'info'))then
    	ALTER TABLE ehd_register_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_o', 'deleted'))then
    	ALTER TABLE ehd_register_o
        	ADD deleted bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_o', 'uid'))then
    	ALTER TABLE ehd_register_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_register_o', 'enddatechange'))then
    	ALTER TABLE ehd_register_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fias_estatestatus')) IS NULL)then
        CREATE TABLE fias_estatestatus
        (
            estatestatusid integer NOT NULL,
		estatestatusname varchar(60),
		estatestatusshortname varchar(20)
        );
    else
            
	if(not core_updstru_checkexistcolumn('fias_estatestatus', 'estatestatusid'))then
    	ALTER TABLE fias_estatestatus
        	ADD estatestatusid integer NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('fias_estatestatus', 'estatestatusname'))then
    	ALTER TABLE fias_estatestatus
        	ADD estatestatusname varchar(60);
	end if;


	if(not core_updstru_checkexistcolumn('fias_estatestatus', 'estatestatusshortname'))then
    	ALTER TABLE fias_estatestatus
        	ADD estatestatusshortname varchar(20);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fias_structurestatus')) IS NULL)then
        CREATE TABLE fias_structurestatus
        (
            structurestatusid integer NOT NULL,
		structurestatusname varchar(60),
		structurestatusshortname varchar(20)
        );
    else
            
	if(not core_updstru_checkexistcolumn('fias_structurestatus', 'structurestatusid'))then
    	ALTER TABLE fias_structurestatus
        	ADD structurestatusid integer NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('fias_structurestatus', 'structurestatusname'))then
    	ALTER TABLE fias_structurestatus
        	ADD structurestatusname varchar(60);
	end if;


	if(not core_updstru_checkexistcolumn('fias_structurestatus', 'structurestatusshortname'))then
    	ALTER TABLE fias_structurestatus
        	ADD structurestatusshortname varchar(20);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('spd_request_registration')) IS NULL)then
        CREATE TABLE spd_request_registration
        (
            id bigint NOT NULL,
		app_date timestamp without time zone,
		app_name varchar(1024),
		app_id bigint,
		app_status varchar(1024),
		doc_date timestamp without time zone,
		doc_name varchar(1024),
		doc_link varchar(1024),
		sig_link varchar(1024),
		app_doc_id bigint,
		doc_type varchar(1024),
		custom_xml text,
		fls varchar(1024),
		status varchar(1024),
		status_code bigint,
		date_create timestamp without time zone,
		date_confirm timestamp without time zone,
		date_perform timestamp without time zone,
		doc_number varchar(1024),
		name varchar(1024),
		error bigint,
		error_message varchar(4000),
		regid bigint,
		app_status_id bigint,
		inn varchar(12),
		typerus varchar(1024)
        );
    else
            
	if(not core_updstru_checkexistcolumn('spd_request_registration', 'id'))then
    	ALTER TABLE spd_request_registration
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'app_date'))then
    	ALTER TABLE spd_request_registration
        	ADD app_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'app_name'))then
    	ALTER TABLE spd_request_registration
        	ADD app_name varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'app_id'))then
    	ALTER TABLE spd_request_registration
        	ADD app_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'app_status'))then
    	ALTER TABLE spd_request_registration
        	ADD app_status varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'doc_date'))then
    	ALTER TABLE spd_request_registration
        	ADD doc_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'doc_name'))then
    	ALTER TABLE spd_request_registration
        	ADD doc_name varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'doc_link'))then
    	ALTER TABLE spd_request_registration
        	ADD doc_link varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'sig_link'))then
    	ALTER TABLE spd_request_registration
        	ADD sig_link varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'app_doc_id'))then
    	ALTER TABLE spd_request_registration
        	ADD app_doc_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'doc_type'))then
    	ALTER TABLE spd_request_registration
        	ADD doc_type varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'custom_xml'))then
    	ALTER TABLE spd_request_registration
        	ADD custom_xml text;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'fls'))then
    	ALTER TABLE spd_request_registration
        	ADD fls varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'status'))then
    	ALTER TABLE spd_request_registration
        	ADD status varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'status_code'))then
    	ALTER TABLE spd_request_registration
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'date_create'))then
    	ALTER TABLE spd_request_registration
        	ADD date_create timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'date_confirm'))then
    	ALTER TABLE spd_request_registration
        	ADD date_confirm timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'date_perform'))then
    	ALTER TABLE spd_request_registration
        	ADD date_perform timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'doc_number'))then
    	ALTER TABLE spd_request_registration
        	ADD doc_number varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'name'))then
    	ALTER TABLE spd_request_registration
        	ADD name varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'error'))then
    	ALTER TABLE spd_request_registration
        	ADD error bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'error_message'))then
    	ALTER TABLE spd_request_registration
        	ADD error_message varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'regid'))then
    	ALTER TABLE spd_request_registration
        	ADD regid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'app_status_id'))then
    	ALTER TABLE spd_request_registration
        	ADD app_status_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'inn'))then
    	ALTER TABLE spd_request_registration
        	ADD inn varchar(12);
	end if;


	if(not core_updstru_checkexistcolumn('spd_request_registration', 'typerus'))then
    	ALTER TABLE spd_request_registration
        	ADD typerus varchar(1024);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('spd_doc_agreement')) IS NULL)then
        CREATE TABLE spd_doc_agreement
        (
            id bigint NOT NULL,
		planid bigint,
		status varchar(1024),
		status_code bigint,
		createdate timestamp without time zone,
		changedate timestamp without time zone,
		spd_appid bigint,
		spd_appdocid bigint,
		spd_definition varchar(4000),
		spd_ishand bigint,
		spd_issogl bigint,
		spd_num bigint,
		spd_podp varchar(1024),
		spd_soglcode varchar(1024),
		spd_sogldate timestamp without time zone,
		spd_userid bigint,
		docattached smallint,
		userid bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'id'))then
    	ALTER TABLE spd_doc_agreement
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'planid'))then
    	ALTER TABLE spd_doc_agreement
        	ADD planid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'status'))then
    	ALTER TABLE spd_doc_agreement
        	ADD status varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'status_code'))then
    	ALTER TABLE spd_doc_agreement
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'createdate'))then
    	ALTER TABLE spd_doc_agreement
        	ADD createdate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'changedate'))then
    	ALTER TABLE spd_doc_agreement
        	ADD changedate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_appid'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_appid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_appdocid'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_appdocid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_definition'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_definition varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_ishand'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_ishand bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_issogl'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_issogl bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_num'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_num bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_podp'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_podp varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_soglcode'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_soglcode varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_sogldate'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_sogldate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'spd_userid'))then
    	ALTER TABLE spd_doc_agreement
        	ADD spd_userid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'docattached'))then
    	ALTER TABLE spd_doc_agreement
        	ADD docattached smallint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_doc_agreement', 'userid'))then
    	ALTER TABLE spd_doc_agreement
        	ADD userid bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('spd_create_full_app_log')) IS NULL)then
        CREATE TABLE spd_create_full_app_log
        (
            id bigint NOT NULL,
		spd_profile_id varchar(128),
		create_date timestamp without time zone,
		comments varchar(4000),
		spd_send_date timestamp without time zone,
		message varchar(1024),
		error_id bigint,
		spd_app_date timestamp without time zone,
		spd_app_id bigint,
		spd_app_name varchar(128),
		object_register_id bigint,
		object_id bigint,
		object_ids varchar(4000),
		main_object_register_id bigint,
		main_object_id bigint,
		user_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_profile_id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD spd_profile_id varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'create_date'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD create_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'comments'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD comments varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_send_date'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD spd_send_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'message'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD message varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'error_id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD error_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_app_date'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD spd_app_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_app_id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD spd_app_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'spd_app_name'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD spd_app_name varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'object_register_id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD object_register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'object_id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'object_ids'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD object_ids varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'main_object_register_id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD main_object_register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'main_object_id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD main_object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('spd_create_full_app_log', 'user_id'))then
    	ALTER TABLE spd_create_full_app_log
        	ADD user_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_building_o')) IS NULL)then
        CREATE TABLE bti_building_o
        (
            id bigint NOT NULL,
		info varchar(100),
		deleted bigint NOT NULL DEFAULT 0,
		uid varchar(50),
		enddatechange timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_building_o', 'id'))then
    	ALTER TABLE bti_building_o
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_o', 'info'))then
    	ALTER TABLE bti_building_o
        	ADD info varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_o', 'deleted'))then
    	ALTER TABLE bti_building_o
        	ADD deleted bigint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_o', 'uid'))then
    	ALTER TABLE bti_building_o
        	ADD uid varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_o', 'enddatechange'))then
    	ALTER TABLE bti_building_o
        	ADD enddatechange timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_building_q')) IS NULL)then
        CREATE TABLE bti_building_q
        (
            id bigint NOT NULL,
		emp_id bigint NOT NULL,
		actual bigint NOT NULL,
		status bigint NOT NULL,
		dept_id bigint,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		unom bigint,
		kl_code bigint,
		kl varchar(4000),
		naz_code bigint,
		naz varchar(4000),
		mst_code bigint,
		mst varchar(4000),
		et bigint,
		gdpostr bigint,
		kad_n varchar(4000),
		et_min bigint,
		et_pdz bigint,
		opl numeric(22, 2),
		sost varchar(4000),
		sost_code bigint,
		dtsost timestamp without time zone,
		gddo1917 smallint,
		avarzd smallint,
		dtavarzd timestamp without time zone,
		samovol smallint,
		opl_g numeric(22, 2),
		opl_n numeric(22, 2),
		narpl numeric(22, 2),
		gdpereob smallint,
		gdkaprem smallint,
		ppl numeric(22, 2),
		ser varchar(1000),
		ser_code bigint,
		kap bigint,
		komm varchar(4000),
		source varchar(250),
		source_id bigint,
		kat_code bigint,
		kat varchar(250),
		obj_type_code bigint,
		obj_type varchar(250),
		download_date timestamp without time zone,
		pdvpl_n numeric(22, 2),
		actproc numeric(10, 2),
		gdproc bigint,
		pamarc smallint,
		krovpl numeric(25, 2),
		lfpq bigint,
		lfgpq bigint,
		lfgq bigint,
		pmq_g bigint,
		kmq_g bigint,
		kwq bigint,
		prkor bigint,
		hpl numeric(11, 2),
		eleq bigint,
		gazq bigint,
		bpl numeric(25, 2),
		lpl numeric(25, 2),
		perekr_code bigint,
		perekr varchar(255),
		krov_code bigint,
		krov varchar(255),
		otskorp varchar(255),
		otskorp_code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_building_q', 'id'))then
    	ALTER TABLE bti_building_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'emp_id'))then
    	ALTER TABLE bti_building_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'actual'))then
    	ALTER TABLE bti_building_q
        	ADD actual bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'status'))then
    	ALTER TABLE bti_building_q
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'dept_id'))then
    	ALTER TABLE bti_building_q
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 's_'))then
    	ALTER TABLE bti_building_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'po_'))then
    	ALTER TABLE bti_building_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'unom'))then
    	ALTER TABLE bti_building_q
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'kl_code'))then
    	ALTER TABLE bti_building_q
        	ADD kl_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'kl'))then
    	ALTER TABLE bti_building_q
        	ADD kl varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'naz_code'))then
    	ALTER TABLE bti_building_q
        	ADD naz_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'naz'))then
    	ALTER TABLE bti_building_q
        	ADD naz varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'mst_code'))then
    	ALTER TABLE bti_building_q
        	ADD mst_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'mst'))then
    	ALTER TABLE bti_building_q
        	ADD mst varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'et'))then
    	ALTER TABLE bti_building_q
        	ADD et bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'gdpostr'))then
    	ALTER TABLE bti_building_q
        	ADD gdpostr bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'kad_n'))then
    	ALTER TABLE bti_building_q
        	ADD kad_n varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'et_min'))then
    	ALTER TABLE bti_building_q
        	ADD et_min bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'et_pdz'))then
    	ALTER TABLE bti_building_q
        	ADD et_pdz bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'opl'))then
    	ALTER TABLE bti_building_q
        	ADD opl numeric(22, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'sost'))then
    	ALTER TABLE bti_building_q
        	ADD sost varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'sost_code'))then
    	ALTER TABLE bti_building_q
        	ADD sost_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'dtsost'))then
    	ALTER TABLE bti_building_q
        	ADD dtsost timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'gddo1917'))then
    	ALTER TABLE bti_building_q
        	ADD gddo1917 smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'avarzd'))then
    	ALTER TABLE bti_building_q
        	ADD avarzd smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'dtavarzd'))then
    	ALTER TABLE bti_building_q
        	ADD dtavarzd timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'samovol'))then
    	ALTER TABLE bti_building_q
        	ADD samovol smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'opl_g'))then
    	ALTER TABLE bti_building_q
        	ADD opl_g numeric(22, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'opl_n'))then
    	ALTER TABLE bti_building_q
        	ADD opl_n numeric(22, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'narpl'))then
    	ALTER TABLE bti_building_q
        	ADD narpl numeric(22, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'gdpereob'))then
    	ALTER TABLE bti_building_q
        	ADD gdpereob smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'gdkaprem'))then
    	ALTER TABLE bti_building_q
        	ADD gdkaprem smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'ppl'))then
    	ALTER TABLE bti_building_q
        	ADD ppl numeric(22, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'ser'))then
    	ALTER TABLE bti_building_q
        	ADD ser varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'ser_code'))then
    	ALTER TABLE bti_building_q
        	ADD ser_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'kap'))then
    	ALTER TABLE bti_building_q
        	ADD kap bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'komm'))then
    	ALTER TABLE bti_building_q
        	ADD komm varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'source'))then
    	ALTER TABLE bti_building_q
        	ADD source varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'source_id'))then
    	ALTER TABLE bti_building_q
        	ADD source_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'kat_code'))then
    	ALTER TABLE bti_building_q
        	ADD kat_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'kat'))then
    	ALTER TABLE bti_building_q
        	ADD kat varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'obj_type_code'))then
    	ALTER TABLE bti_building_q
        	ADD obj_type_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'obj_type'))then
    	ALTER TABLE bti_building_q
        	ADD obj_type varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'download_date'))then
    	ALTER TABLE bti_building_q
        	ADD download_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'pdvpl_n'))then
    	ALTER TABLE bti_building_q
        	ADD pdvpl_n numeric(22, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'actproc'))then
    	ALTER TABLE bti_building_q
        	ADD actproc numeric(10, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'gdproc'))then
    	ALTER TABLE bti_building_q
        	ADD gdproc bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'pamarc'))then
    	ALTER TABLE bti_building_q
        	ADD pamarc smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'krovpl'))then
    	ALTER TABLE bti_building_q
        	ADD krovpl numeric(25, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'lfpq'))then
    	ALTER TABLE bti_building_q
        	ADD lfpq bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'lfgpq'))then
    	ALTER TABLE bti_building_q
        	ADD lfgpq bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'lfgq'))then
    	ALTER TABLE bti_building_q
        	ADD lfgq bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'pmq_g'))then
    	ALTER TABLE bti_building_q
        	ADD pmq_g bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'kmq_g'))then
    	ALTER TABLE bti_building_q
        	ADD kmq_g bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'kwq'))then
    	ALTER TABLE bti_building_q
        	ADD kwq bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'prkor'))then
    	ALTER TABLE bti_building_q
        	ADD prkor bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'hpl'))then
    	ALTER TABLE bti_building_q
        	ADD hpl numeric(11, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'eleq'))then
    	ALTER TABLE bti_building_q
        	ADD eleq bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'gazq'))then
    	ALTER TABLE bti_building_q
        	ADD gazq bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'bpl'))then
    	ALTER TABLE bti_building_q
        	ADD bpl numeric(25, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'lpl'))then
    	ALTER TABLE bti_building_q
        	ADD lpl numeric(25, 2);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'perekr_code'))then
    	ALTER TABLE bti_building_q
        	ADD perekr_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'perekr'))then
    	ALTER TABLE bti_building_q
        	ADD perekr varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'krov_code'))then
    	ALTER TABLE bti_building_q
        	ADD krov_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'krov'))then
    	ALTER TABLE bti_building_q
        	ADD krov varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'otskorp'))then
    	ALTER TABLE bti_building_q
        	ADD otskorp varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_building_q', 'otskorp_code'))then
    	ALTER TABLE bti_building_q
        	ADD otskorp_code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_floor_q')) IS NULL)then
        CREATE TABLE bti_floor_q
        (
            id bigint NOT NULL,
		emp_id bigint NOT NULL,
		actual bigint NOT NULL,
		status bigint NOT NULL,
		dept_id bigint,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		building_id bigint,
		building_name varchar(4000),
		type_id bigint,
		type_name varchar(4000),
		floor_number bigint,
		floor_number_pp bigint,
		area_pp bigint,
		guid_pp varchar(38),
		number_pp bigint,
		type_pp varchar(1000),
		is_undeground smallint,
		register_object_number bigint,
		floor_plan_presence smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_floor_q', 'id'))then
    	ALTER TABLE bti_floor_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'emp_id'))then
    	ALTER TABLE bti_floor_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'actual'))then
    	ALTER TABLE bti_floor_q
        	ADD actual bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'status'))then
    	ALTER TABLE bti_floor_q
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'dept_id'))then
    	ALTER TABLE bti_floor_q
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 's_'))then
    	ALTER TABLE bti_floor_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'po_'))then
    	ALTER TABLE bti_floor_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'building_id'))then
    	ALTER TABLE bti_floor_q
        	ADD building_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'building_name'))then
    	ALTER TABLE bti_floor_q
        	ADD building_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'type_id'))then
    	ALTER TABLE bti_floor_q
        	ADD type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'type_name'))then
    	ALTER TABLE bti_floor_q
        	ADD type_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'floor_number'))then
    	ALTER TABLE bti_floor_q
        	ADD floor_number bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'floor_number_pp'))then
    	ALTER TABLE bti_floor_q
        	ADD floor_number_pp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'area_pp'))then
    	ALTER TABLE bti_floor_q
        	ADD area_pp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'guid_pp'))then
    	ALTER TABLE bti_floor_q
        	ADD guid_pp varchar(38);
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'number_pp'))then
    	ALTER TABLE bti_floor_q
        	ADD number_pp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'type_pp'))then
    	ALTER TABLE bti_floor_q
        	ADD type_pp varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'is_undeground'))then
    	ALTER TABLE bti_floor_q
        	ADD is_undeground smallint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'register_object_number'))then
    	ALTER TABLE bti_floor_q
        	ADD register_object_number bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_floor_q', 'floor_plan_presence'))then
    	ALTER TABLE bti_floor_q
        	ADD floor_plan_presence smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_attachment')) IS NULL)then
        CREATE TABLE core_attachment
        (
            id bigint NOT NULL,
		doc_number varchar(500),
		description varchar(120),
		barcode varchar(32),
		doc_type varchar(120),
		doc_type_id bigint,
		photo_type varchar(120),
		photo_type_id bigint,
		created_by_id bigint,
		created_date timestamp without time zone NOT NULL,
		is_deleted smallint NOT NULL,
		deleted_by_id bigint,
		deleted_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_attachment', 'id'))then
    	ALTER TABLE core_attachment
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'doc_number'))then
    	ALTER TABLE core_attachment
        	ADD doc_number varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'description'))then
    	ALTER TABLE core_attachment
        	ADD description varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'barcode'))then
    	ALTER TABLE core_attachment
        	ADD barcode varchar(32);
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'doc_type'))then
    	ALTER TABLE core_attachment
        	ADD doc_type varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'doc_type_id'))then
    	ALTER TABLE core_attachment
        	ADD doc_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'photo_type'))then
    	ALTER TABLE core_attachment
        	ADD photo_type varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'photo_type_id'))then
    	ALTER TABLE core_attachment
        	ADD photo_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'created_by_id'))then
    	ALTER TABLE core_attachment
        	ADD created_by_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'created_date'))then
    	ALTER TABLE core_attachment
        	ADD created_date timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'is_deleted'))then
    	ALTER TABLE core_attachment
        	ADD is_deleted smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'deleted_by_id'))then
    	ALTER TABLE core_attachment
        	ADD deleted_by_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment', 'deleted_date'))then
    	ALTER TABLE core_attachment
        	ADD deleted_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_attachment_object')) IS NULL)then
        CREATE TABLE core_attachment_object
        (
            id bigint NOT NULL,
		attachment_id bigint,
		register_id bigint,
		object_id bigint,
		is_deleted smallint NOT NULL,
		deleted_by_id bigint,
		deleted_date timestamp without time zone,
		is_main smallint,
		created_by_id bigint,
		created_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_attachment_object', 'id'))then
    	ALTER TABLE core_attachment_object
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'attachment_id'))then
    	ALTER TABLE core_attachment_object
        	ADD attachment_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'register_id'))then
    	ALTER TABLE core_attachment_object
        	ADD register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'object_id'))then
    	ALTER TABLE core_attachment_object
        	ADD object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'is_deleted'))then
    	ALTER TABLE core_attachment_object
        	ADD is_deleted smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'deleted_by_id'))then
    	ALTER TABLE core_attachment_object
        	ADD deleted_by_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'deleted_date'))then
    	ALTER TABLE core_attachment_object
        	ADD deleted_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'is_main'))then
    	ALTER TABLE core_attachment_object
        	ADD is_main smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'created_by_id'))then
    	ALTER TABLE core_attachment_object
        	ADD created_by_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_attachment_object', 'created_date'))then
    	ALTER TABLE core_attachment_object
        	ADD created_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_cache_updates')) IS NULL)then
        CREATE TABLE core_cache_updates
        (
            id bigint NOT NULL,
		cacheobject varchar(50) NOT NULL,
		cachekey bigint NOT NULL,
		extradata varchar(200) NOT NULL,
		cache_timestamp timestamp without time zone NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_cache_updates', 'id'))then
    	ALTER TABLE core_cache_updates
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_cache_updates', 'cacheobject'))then
    	ALTER TABLE core_cache_updates
        	ADD cacheobject varchar(50) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_cache_updates', 'cachekey'))then
    	ALTER TABLE core_cache_updates
        	ADD cachekey bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_cache_updates', 'extradata'))then
    	ALTER TABLE core_cache_updates
        	ADD extradata varchar(200) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_cache_updates', 'cache_timestamp'))then
    	ALTER TABLE core_cache_updates
        	ADD cache_timestamp timestamp without time zone NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_configparam')) IS NULL)then
        CREATE TABLE core_configparam
        (
            id bigint NOT NULL,
		parentkey varchar(50),
		childkey varchar(50),
		xmldata text,
		description varchar(100),
		chdate timestamp without time zone,
		userid varchar(100)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_configparam', 'id'))then
    	ALTER TABLE core_configparam
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_configparam', 'parentkey'))then
    	ALTER TABLE core_configparam
        	ADD parentkey varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('core_configparam', 'childkey'))then
    	ALTER TABLE core_configparam
        	ADD childkey varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('core_configparam', 'xmldata'))then
    	ALTER TABLE core_configparam
        	ADD xmldata text;
	end if;


	if(not core_updstru_checkexistcolumn('core_configparam', 'description'))then
    	ALTER TABLE core_configparam
        	ADD description varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_configparam', 'chdate'))then
    	ALTER TABLE core_configparam
        	ADD chdate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_configparam', 'userid'))then
    	ALTER TABLE core_configparam
        	ADD userid varchar(100);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_diagnostics')) IS NULL)then
        CREATE TABLE core_diagnostics
        (
            id bigint NOT NULL,
		user_id bigint,
		module varchar(250),
		method varchar(250),
		extra_key varchar(250),
		action_date timestamp without time zone NOT NULL,
		execution_duration bigint,
		action_descr text,
		callstack varchar(4000),
		callstack_clob text
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_diagnostics', 'id'))then
    	ALTER TABLE core_diagnostics
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'user_id'))then
    	ALTER TABLE core_diagnostics
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'module'))then
    	ALTER TABLE core_diagnostics
        	ADD module varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'method'))then
    	ALTER TABLE core_diagnostics
        	ADD method varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'extra_key'))then
    	ALTER TABLE core_diagnostics
        	ADD extra_key varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'action_date'))then
    	ALTER TABLE core_diagnostics
        	ADD action_date timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'execution_duration'))then
    	ALTER TABLE core_diagnostics
        	ADD execution_duration bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'action_descr'))then
    	ALTER TABLE core_diagnostics
        	ADD action_descr text;
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'callstack'))then
    	ALTER TABLE core_diagnostics
        	ADD callstack varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_diagnostics', 'callstack_clob'))then
    	ALTER TABLE core_diagnostics
        	ADD callstack_clob text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_error_log')) IS NULL)then
        CREATE TABLE core_error_log
        (
            id bigint NOT NULL,
		userid bigint,
		message varchar(2000),
		errordate timestamp without time zone NOT NULL,
		errorpage_shown smallint NOT NULL DEFAULT 0,
		msgtype varchar(10),
		params_short varchar(4000),
		params_clob text,
		callstack varchar(4000),
		callstack_clob text
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_error_log', 'id'))then
    	ALTER TABLE core_error_log
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'userid'))then
    	ALTER TABLE core_error_log
        	ADD userid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'message'))then
    	ALTER TABLE core_error_log
        	ADD message varchar(2000);
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'errordate'))then
    	ALTER TABLE core_error_log
        	ADD errordate timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'errorpage_shown'))then
    	ALTER TABLE core_error_log
        	ADD errorpage_shown smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'msgtype'))then
    	ALTER TABLE core_error_log
        	ADD msgtype varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'params_short'))then
    	ALTER TABLE core_error_log
        	ADD params_short varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'params_clob'))then
    	ALTER TABLE core_error_log
        	ADD params_clob text;
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'callstack'))then
    	ALTER TABLE core_error_log
        	ADD callstack varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_error_log', 'callstack_clob'))then
    	ALTER TABLE core_error_log
        	ADD callstack_clob text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_holidays')) IS NULL)then
        CREATE TABLE core_holidays
        (
            id bigint NOT NULL,
		value timestamp without time zone NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_holidays', 'id'))then
    	ALTER TABLE core_holidays
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_holidays', 'value'))then
    	ALTER TABLE core_holidays
        	ADD value timestamp without time zone NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_layout')) IS NULL)then
        CREATE TABLE core_layout
        (
            layoutid bigint NOT NULL,
		layoutname varchar(200),
		layoutcomment varchar(200),
		registerid bigint,
		defaultsort bigint,
		preffered smallint,
		username varchar(200),
		createdate timestamp without time zone,
		qsquery text,
		isdistinct smallint NOT NULL,
		ordertype varchar(20),
		user_id bigint,
		iscommon smallint NOT NULL,
		internal_name varchar(250),
		enable_minicards_mode smallint NOT NULL,
		register_view_id varchar(200),
		as_domain_id varchar(200)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_layout', 'layoutid'))then
    	ALTER TABLE core_layout
        	ADD layoutid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'layoutname'))then
    	ALTER TABLE core_layout
        	ADD layoutname varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'layoutcomment'))then
    	ALTER TABLE core_layout
        	ADD layoutcomment varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'registerid'))then
    	ALTER TABLE core_layout
        	ADD registerid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'defaultsort'))then
    	ALTER TABLE core_layout
        	ADD defaultsort bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'preffered'))then
    	ALTER TABLE core_layout
        	ADD preffered smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'username'))then
    	ALTER TABLE core_layout
        	ADD username varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'createdate'))then
    	ALTER TABLE core_layout
        	ADD createdate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'qsquery'))then
    	ALTER TABLE core_layout
        	ADD qsquery text;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'isdistinct'))then
    	ALTER TABLE core_layout
        	ADD isdistinct smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'ordertype'))then
    	ALTER TABLE core_layout
        	ADD ordertype varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'user_id'))then
    	ALTER TABLE core_layout
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'iscommon'))then
    	ALTER TABLE core_layout
        	ADD iscommon smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'internal_name'))then
    	ALTER TABLE core_layout
        	ADD internal_name varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'enable_minicards_mode'))then
    	ALTER TABLE core_layout
        	ADD enable_minicards_mode smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'register_view_id'))then
    	ALTER TABLE core_layout
        	ADD register_view_id varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout', 'as_domain_id'))then
    	ALTER TABLE core_layout
        	ADD as_domain_id varchar(200);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_layout_export')) IS NULL)then
        CREATE TABLE core_layout_export
        (
            id bigint NOT NULL,
		layout_id bigint,
		user_id bigint,
		start_date timestamp without time zone,
		end_date timestamp without time zone,
		status bigint,
		result_message varchar(4000),
		file_location varchar(4000),
		rows_count bigint,
		qs_query text,
		"type" varchar(10),
		register_view_id varchar(512)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_layout_export', 'id'))then
    	ALTER TABLE core_layout_export
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'layout_id'))then
    	ALTER TABLE core_layout_export
        	ADD layout_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'user_id'))then
    	ALTER TABLE core_layout_export
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'start_date'))then
    	ALTER TABLE core_layout_export
        	ADD start_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'end_date'))then
    	ALTER TABLE core_layout_export
        	ADD end_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'status'))then
    	ALTER TABLE core_layout_export
        	ADD status bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'result_message'))then
    	ALTER TABLE core_layout_export
        	ADD result_message varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'file_location'))then
    	ALTER TABLE core_layout_export
        	ADD file_location varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'rows_count'))then
    	ALTER TABLE core_layout_export
        	ADD rows_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'qs_query'))then
    	ALTER TABLE core_layout_export
        	ADD qs_query text;
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'type'))then
    	ALTER TABLE core_layout_export
        	ADD "type" varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('core_layout_export', 'register_view_id'))then
    	ALTER TABLE core_layout_export
        	ADD register_view_id varchar(512);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_list')) IS NULL)then
        CREATE TABLE core_list
        (
            id bigint NOT NULL,
		name varchar(500) NOT NULL,
		register_view_id varchar(200) NOT NULL,
		register_id bigint NOT NULL,
		author bigint NOT NULL,
		iscommon smallint NOT NULL,
		list_comment varchar(500),
		change_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_list', 'id'))then
    	ALTER TABLE core_list
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_list', 'name'))then
    	ALTER TABLE core_list
        	ADD name varchar(500) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_list', 'register_view_id'))then
    	ALTER TABLE core_list
        	ADD register_view_id varchar(200) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_list', 'register_id'))then
    	ALTER TABLE core_list
        	ADD register_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_list', 'author'))then
    	ALTER TABLE core_list
        	ADD author bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_list', 'iscommon'))then
    	ALTER TABLE core_list
        	ADD iscommon smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_list', 'list_comment'))then
    	ALTER TABLE core_list
        	ADD list_comment varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('core_list', 'change_date'))then
    	ALTER TABLE core_list
        	ADD change_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_long_process_log')) IS NULL)then
        CREATE TABLE core_long_process_log
        (
            id bigint NOT NULL,
		exe_info varchar(1024),
		start_date timestamp without time zone,
		last_check_date timestamp without time zone,
		status bigint,
		user_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_long_process_log', 'id'))then
    	ALTER TABLE core_long_process_log
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_log', 'exe_info'))then
    	ALTER TABLE core_long_process_log
        	ADD exe_info varchar(1024);
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_log', 'start_date'))then
    	ALTER TABLE core_long_process_log
        	ADD start_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_log', 'last_check_date'))then
    	ALTER TABLE core_long_process_log
        	ADD last_check_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_log', 'status'))then
    	ALTER TABLE core_long_process_log
        	ADD status bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_log', 'user_id'))then
    	ALTER TABLE core_long_process_log
        	ADD user_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_long_process_type')) IS NULL)then
        CREATE TABLE core_long_process_type
        (
            id bigint NOT NULL,
		process_name varchar(256) NOT NULL,
		class_name varchar(512) NOT NULL,
		schedule_type bigint,
		repeat_interval varchar(256),
		enabled smallint,
		run_count bigint,
		failure_count bigint,
		last_start_date timestamp without time zone,
		last_run_duration bigint,
		next_run_date timestamp without time zone,
		parameters varchar(4000),
		description varchar(255),
		test_result smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_long_process_type', 'id'))then
    	ALTER TABLE core_long_process_type
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'process_name'))then
    	ALTER TABLE core_long_process_type
        	ADD process_name varchar(256) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'class_name'))then
    	ALTER TABLE core_long_process_type
        	ADD class_name varchar(512) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'schedule_type'))then
    	ALTER TABLE core_long_process_type
        	ADD schedule_type bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'repeat_interval'))then
    	ALTER TABLE core_long_process_type
        	ADD repeat_interval varchar(256);
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'enabled'))then
    	ALTER TABLE core_long_process_type
        	ADD enabled smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'run_count'))then
    	ALTER TABLE core_long_process_type
        	ADD run_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'failure_count'))then
    	ALTER TABLE core_long_process_type
        	ADD failure_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'last_start_date'))then
    	ALTER TABLE core_long_process_type
        	ADD last_start_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'last_run_duration'))then
    	ALTER TABLE core_long_process_type
        	ADD last_run_duration bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'next_run_date'))then
    	ALTER TABLE core_long_process_type
        	ADD next_run_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'parameters'))then
    	ALTER TABLE core_long_process_type
        	ADD parameters varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'description'))then
    	ALTER TABLE core_long_process_type
        	ADD description varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_type', 'test_result'))then
    	ALTER TABLE core_long_process_type
        	ADD test_result smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register_lock')) IS NULL)then
        CREATE TABLE core_register_lock
        (
            id bigint NOT NULL,
		userid bigint NOT NULL,
		registerid bigint NOT NULL,
		objectid bigint NOT NULL,
		lockdate timestamp without time zone NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_register_lock', 'id'))then
    	ALTER TABLE core_register_lock
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_lock', 'userid'))then
    	ALTER TABLE core_register_lock
        	ADD userid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_lock', 'registerid'))then
    	ALTER TABLE core_register_lock
        	ADD registerid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_lock', 'objectid'))then
    	ALTER TABLE core_register_lock
        	ADD objectid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_lock', 'lockdate'))then
    	ALTER TABLE core_register_lock
        	ADD lockdate timestamp without time zone NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register_state')) IS NULL)then
        CREATE TABLE core_register_state
        (
            id bigint NOT NULL,
		register_view_id varchar(100) NOT NULL,
		user_id bigint NOT NULL,
		state_save_date timestamp without time zone NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_register_state', 'id'))then
    	ALTER TABLE core_register_state
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_state', 'register_view_id'))then
    	ALTER TABLE core_register_state
        	ADD register_view_id varchar(100) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_state', 'user_id'))then
    	ALTER TABLE core_register_state
        	ADD user_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_state', 'state_save_date'))then
    	ALTER TABLE core_register_state
        	ADD state_save_date timestamp without time zone NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_audit')) IS NULL)then
        CREATE TABLE core_srd_audit
        (
            id bigint NOT NULL,
		function_id bigint NOT NULL,
		user_id bigint NOT NULL,
		actiontime timestamp without time zone NOT NULL,
		result smallint,
		result_desc varchar(255),
		session_id bigint,
		object_register_id bigint,
		object_id bigint,
		object_status_id bigint,
		external_id bigint,
		main_object_register_id bigint,
		main_object_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_audit', 'id'))then
    	ALTER TABLE core_srd_audit
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'function_id'))then
    	ALTER TABLE core_srd_audit
        	ADD function_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'user_id'))then
    	ALTER TABLE core_srd_audit
        	ADD user_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'actiontime'))then
    	ALTER TABLE core_srd_audit
        	ADD actiontime timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'result'))then
    	ALTER TABLE core_srd_audit
        	ADD result smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'result_desc'))then
    	ALTER TABLE core_srd_audit
        	ADD result_desc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'session_id'))then
    	ALTER TABLE core_srd_audit
        	ADD session_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'object_register_id'))then
    	ALTER TABLE core_srd_audit
        	ADD object_register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'object_id'))then
    	ALTER TABLE core_srd_audit
        	ADD object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'object_status_id'))then
    	ALTER TABLE core_srd_audit
        	ADD object_status_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'external_id'))then
    	ALTER TABLE core_srd_audit
        	ADD external_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'main_object_register_id'))then
    	ALTER TABLE core_srd_audit
        	ADD main_object_register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_audit', 'main_object_id'))then
    	ALTER TABLE core_srd_audit
        	ADD main_object_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_session')) IS NULL)then
        CREATE TABLE core_srd_session
        (
            id bigint NOT NULL,
		user_id bigint,
		logintime timestamp without time zone,
		logouttime timestamp without time zone,
		ip varchar(30),
		asp_session_id varchar(40),
		browser_name varchar(255),
		browser_version varchar(30),
		browser_platform varchar(10),
		browser_js_version varchar(5),
		login_status smallint,
		commentary varchar(100),
		last_activity timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_session', 'id'))then
    	ALTER TABLE core_srd_session
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'user_id'))then
    	ALTER TABLE core_srd_session
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'logintime'))then
    	ALTER TABLE core_srd_session
        	ADD logintime timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'logouttime'))then
    	ALTER TABLE core_srd_session
        	ADD logouttime timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'ip'))then
    	ALTER TABLE core_srd_session
        	ADD ip varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'asp_session_id'))then
    	ALTER TABLE core_srd_session
        	ADD asp_session_id varchar(40);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'browser_name'))then
    	ALTER TABLE core_srd_session
        	ADD browser_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'browser_version'))then
    	ALTER TABLE core_srd_session
        	ADD browser_version varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'browser_platform'))then
    	ALTER TABLE core_srd_session
        	ADD browser_platform varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'browser_js_version'))then
    	ALTER TABLE core_srd_session
        	ADD browser_js_version varchar(5);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'login_status'))then
    	ALTER TABLE core_srd_session
        	ADD login_status smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'commentary'))then
    	ALTER TABLE core_srd_session
        	ADD commentary varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_session', 'last_activity'))then
    	ALTER TABLE core_srd_session
        	ADD last_activity timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_attachments')) IS NULL)then
        CREATE TABLE core_td_attachments
        (
            id bigint NOT NULL,
		td_id bigint,
		attachment_id bigint,
		is_deleted smallint,
		deleted_by varchar(120),
		deleted_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_attachments', 'id'))then
    	ALTER TABLE core_td_attachments
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_attachments', 'td_id'))then
    	ALTER TABLE core_td_attachments
        	ADD td_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_attachments', 'attachment_id'))then
    	ALTER TABLE core_td_attachments
        	ADD attachment_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_attachments', 'is_deleted'))then
    	ALTER TABLE core_td_attachments
        	ADD is_deleted smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_attachments', 'deleted_by'))then
    	ALTER TABLE core_td_attachments
        	ADD deleted_by varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_attachments', 'deleted_date'))then
    	ALTER TABLE core_td_attachments
        	ADD deleted_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_audit')) IS NULL)then
        CREATE TABLE core_td_audit
        (
            id bigint NOT NULL,
		td_id bigint NOT NULL,
		action_id bigint NOT NULL,
		date_time timestamp without time zone,
		actionresult bigint,
		statusafter bigint,
		newauthor varchar(68),
		newnumber varchar(40),
		description varchar(4000),
		user_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_audit', 'id'))then
    	ALTER TABLE core_td_audit
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'td_id'))then
    	ALTER TABLE core_td_audit
        	ADD td_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'action_id'))then
    	ALTER TABLE core_td_audit
        	ADD action_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'date_time'))then
    	ALTER TABLE core_td_audit
        	ADD date_time timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'actionresult'))then
    	ALTER TABLE core_td_audit
        	ADD actionresult bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'statusafter'))then
    	ALTER TABLE core_td_audit
        	ADD statusafter bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'newauthor'))then
    	ALTER TABLE core_td_audit
        	ADD newauthor varchar(68);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'newnumber'))then
    	ALTER TABLE core_td_audit
        	ADD newnumber varchar(40);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'description'))then
    	ALTER TABLE core_td_audit
        	ADD description varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_audit', 'user_id'))then
    	ALTER TABLE core_td_audit
        	ADD user_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_changeset')) IS NULL)then
        CREATE TABLE core_td_changeset
        (
            id bigint NOT NULL,
		td_id bigint,
		changeset_date timestamp without time zone,
		status bigint NOT NULL DEFAULT 1,
		user_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_changeset', 'id'))then
    	ALTER TABLE core_td_changeset
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_changeset', 'td_id'))then
    	ALTER TABLE core_td_changeset
        	ADD td_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_changeset', 'changeset_date'))then
    	ALTER TABLE core_td_changeset
        	ADD changeset_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_changeset', 'status'))then
    	ALTER TABLE core_td_changeset
        	ADD status bigint NOT NULL DEFAULT 1;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_changeset', 'user_id'))then
    	ALTER TABLE core_td_changeset
        	ADD user_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_instance')) IS NULL)then
        CREATE TABLE core_td_instance
        (
            id bigint NOT NULL,
		template_version_id bigint NOT NULL,
		description varchar(150),
		author_id bigint NOT NULL,
		regnumber varchar(40),
		create_date timestamp without time zone NOT NULL,
		change_date timestamp without time zone NOT NULL,
		status bigint NOT NULL,
		priority bigint NOT NULL,
		object_id bigint,
		approve_date timestamp without time zone,
		approve_user bigint,
		register_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_instance', 'id'))then
    	ALTER TABLE core_td_instance
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'template_version_id'))then
    	ALTER TABLE core_td_instance
        	ADD template_version_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'description'))then
    	ALTER TABLE core_td_instance
        	ADD description varchar(150);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'author_id'))then
    	ALTER TABLE core_td_instance
        	ADD author_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'regnumber'))then
    	ALTER TABLE core_td_instance
        	ADD regnumber varchar(40);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'create_date'))then
    	ALTER TABLE core_td_instance
        	ADD create_date timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'change_date'))then
    	ALTER TABLE core_td_instance
        	ADD change_date timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'status'))then
    	ALTER TABLE core_td_instance
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'priority'))then
    	ALTER TABLE core_td_instance
        	ADD priority bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'object_id'))then
    	ALTER TABLE core_td_instance
        	ADD object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'approve_date'))then
    	ALTER TABLE core_td_instance
        	ADD approve_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'approve_user'))then
    	ALTER TABLE core_td_instance
        	ADD approve_user bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_instance', 'register_id'))then
    	ALTER TABLE core_td_instance
        	ADD register_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_td_template_version')) IS NULL)then
        CREATE TABLE core_td_template_version
        (
            id bigint NOT NULL,
		template_id bigint,
		version bigint NOT NULL DEFAULT 1,
		xsd varchar(128),
		xsl_print varchar(128),
		publish_path varchar(500),
		create_date timestamp without time zone,
		author varchar(100),
		xsd_class_name varchar(128),
		template_type bigint,
		print_view_specified smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_td_template_version', 'id'))then
    	ALTER TABLE core_td_template_version
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'template_id'))then
    	ALTER TABLE core_td_template_version
        	ADD template_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'version'))then
    	ALTER TABLE core_td_template_version
        	ADD version bigint NOT NULL DEFAULT 1;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'xsd'))then
    	ALTER TABLE core_td_template_version
        	ADD xsd varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'xsl_print'))then
    	ALTER TABLE core_td_template_version
        	ADD xsl_print varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'publish_path'))then
    	ALTER TABLE core_td_template_version
        	ADD publish_path varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'create_date'))then
    	ALTER TABLE core_td_template_version
        	ADD create_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'author'))then
    	ALTER TABLE core_td_template_version
        	ADD author varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'xsd_class_name'))then
    	ALTER TABLE core_td_template_version
        	ADD xsd_class_name varchar(128);
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'template_type'))then
    	ALTER TABLE core_td_template_version
        	ADD template_type bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_td_template_version', 'print_view_specified'))then
    	ALTER TABLE core_td_template_version
        	ADD print_view_specified smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_updstru_log')) IS NULL)then
        CREATE TABLE core_updstru_log
        (
            id bigint NOT NULL,
		date_start timestamp without time zone NOT NULL,
		date_finish timestamp without time zone,
		script_name varchar(100) NOT NULL,
		script_version varchar(20),
		has_err bigint NOT NULL DEFAULT 0,
		result_message varchar(4000)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_updstru_log', 'id'))then
    	ALTER TABLE core_updstru_log
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_updstru_log', 'date_start'))then
    	ALTER TABLE core_updstru_log
        	ADD date_start timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_updstru_log', 'date_finish'))then
    	ALTER TABLE core_updstru_log
        	ADD date_finish timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_updstru_log', 'script_name'))then
    	ALTER TABLE core_updstru_log
        	ADD script_name varchar(100) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_updstru_log', 'script_version'))then
    	ALTER TABLE core_updstru_log
        	ADD script_version varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('core_updstru_log', 'has_err'))then
    	ALTER TABLE core_updstru_log
        	ADD has_err bigint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_updstru_log', 'result_message'))then
    	ALTER TABLE core_updstru_log
        	ADD result_message varchar(4000);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fias_house')) IS NULL)then
        CREATE TABLE fias_house
        (
            aoguid varchar(36),
		buildnum varchar(10),
		enddate timestamp without time zone,
		eststatus numeric(1, 0),
		houseguid varchar(36),
		houseid varchar(36),
		housenum varchar(20),
		statstatus numeric(5, 0),
		ifnsfl varchar(4),
		ifnsul varchar(4),
		okato varchar(11),
		oktmo varchar(11),
		postalcode varchar(6),
		startdate timestamp without time zone,
		strucnum varchar(10),
		strstatus numeric(1, 0),
		terrifnsfl varchar(4),
		terrifnsul varchar(4),
		updatedate timestamp without time zone,
		normdoc varchar(36),
		counter numeric(4, 0),
		cadnum varchar(100),
		divtype numeric(2, 0)
        );
    else
            
	if(not core_updstru_checkexistcolumn('fias_house', 'aoguid'))then
    	ALTER TABLE fias_house
        	ADD aoguid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'buildnum'))then
    	ALTER TABLE fias_house
        	ADD buildnum varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'enddate'))then
    	ALTER TABLE fias_house
        	ADD enddate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'eststatus'))then
    	ALTER TABLE fias_house
        	ADD eststatus numeric(1, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'houseguid'))then
    	ALTER TABLE fias_house
        	ADD houseguid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'houseid'))then
    	ALTER TABLE fias_house
        	ADD houseid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'housenum'))then
    	ALTER TABLE fias_house
        	ADD housenum varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'statstatus'))then
    	ALTER TABLE fias_house
        	ADD statstatus numeric(5, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'ifnsfl'))then
    	ALTER TABLE fias_house
        	ADD ifnsfl varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'ifnsul'))then
    	ALTER TABLE fias_house
        	ADD ifnsul varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'okato'))then
    	ALTER TABLE fias_house
        	ADD okato varchar(11);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'oktmo'))then
    	ALTER TABLE fias_house
        	ADD oktmo varchar(11);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'postalcode'))then
    	ALTER TABLE fias_house
        	ADD postalcode varchar(6);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'startdate'))then
    	ALTER TABLE fias_house
        	ADD startdate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'strucnum'))then
    	ALTER TABLE fias_house
        	ADD strucnum varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'strstatus'))then
    	ALTER TABLE fias_house
        	ADD strstatus numeric(1, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'terrifnsfl'))then
    	ALTER TABLE fias_house
        	ADD terrifnsfl varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'terrifnsul'))then
    	ALTER TABLE fias_house
        	ADD terrifnsul varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'updatedate'))then
    	ALTER TABLE fias_house
        	ADD updatedate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'normdoc'))then
    	ALTER TABLE fias_house
        	ADD normdoc varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'counter'))then
    	ALTER TABLE fias_house
        	ADD counter numeric(4, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'cadnum'))then
    	ALTER TABLE fias_house
        	ADD cadnum varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('fias_house', 'divtype'))then
    	ALTER TABLE fias_house
        	ADD divtype numeric(2, 0);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fias_room')) IS NULL)then
        CREATE TABLE fias_room
        (
            roomid varchar(36),
		roomguid varchar(36),
		houseguid varchar(36),
		regioncode varchar(2),
		flatnumber varchar(50),
		flattype numeric(2, 0),
		roomnumber varchar(50),
		roomtype varchar(2),
		cadnum varchar(100),
		roomcadnum varchar(100),
		postalcode varchar(6),
		updatedate timestamp without time zone,
		previd varchar(36),
		nextid varchar(36),
		operstatus numeric(2, 0),
		startdate timestamp without time zone,
		enddate timestamp without time zone,
		livestatus numeric(2, 0),
		normdoc varchar(36)
        );
    else
            
	if(not core_updstru_checkexistcolumn('fias_room', 'roomid'))then
    	ALTER TABLE fias_room
        	ADD roomid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'roomguid'))then
    	ALTER TABLE fias_room
        	ADD roomguid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'houseguid'))then
    	ALTER TABLE fias_room
        	ADD houseguid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'regioncode'))then
    	ALTER TABLE fias_room
        	ADD regioncode varchar(2);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'flatnumber'))then
    	ALTER TABLE fias_room
        	ADD flatnumber varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'flattype'))then
    	ALTER TABLE fias_room
        	ADD flattype numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'roomnumber'))then
    	ALTER TABLE fias_room
        	ADD roomnumber varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'roomtype'))then
    	ALTER TABLE fias_room
        	ADD roomtype varchar(2);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'cadnum'))then
    	ALTER TABLE fias_room
        	ADD cadnum varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'roomcadnum'))then
    	ALTER TABLE fias_room
        	ADD roomcadnum varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'postalcode'))then
    	ALTER TABLE fias_room
        	ADD postalcode varchar(6);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'updatedate'))then
    	ALTER TABLE fias_room
        	ADD updatedate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'previd'))then
    	ALTER TABLE fias_room
        	ADD previd varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'nextid'))then
    	ALTER TABLE fias_room
        	ADD nextid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'operstatus'))then
    	ALTER TABLE fias_room
        	ADD operstatus numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'startdate'))then
    	ALTER TABLE fias_room
        	ADD startdate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'enddate'))then
    	ALTER TABLE fias_room
        	ADD enddate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'livestatus'))then
    	ALTER TABLE fias_room
        	ADD livestatus numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_room', 'normdoc'))then
    	ALTER TABLE fias_room
        	ADD normdoc varchar(36);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_bank_plat')) IS NULL)then
        CREATE TABLE insur_bank_plat
        (
            emp_id bigint NOT NULL,
		link_id_file bigint,
		link_svod_bank bigint,
		district_id bigint,
		bank_day timestamp without time zone,
		kodpl varchar(255),
		period_reg_date timestamp without time zone,
		period timestamp without time zone,
		nom_doc varchar(255),
		sum_all numeric,
		kom_bank_all numeric,
		bic_bank bigint,
		data_pp timestamp without time zone,
		cod_doc bigint,
		kod_ysl bigint,
		kod_post bigint,
		sum_by_code numeric,
		kom_bank numeric,
		kom_bank_obr numeric,
		kom_eirc numeric,
		kom_plat numeric,
		flag_vozvr bigint,
		type_opl bigint,
		kod_ypravl bigint,
		flag_nach bigint,
		sum_vsego numeric,
		strok_vsego numeric,
		doc_period timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'emp_id'))then
    	ALTER TABLE insur_bank_plat
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'link_id_file'))then
    	ALTER TABLE insur_bank_plat
        	ADD link_id_file bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'link_svod_bank'))then
    	ALTER TABLE insur_bank_plat
        	ADD link_svod_bank bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'district_id'))then
    	ALTER TABLE insur_bank_plat
        	ADD district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'bank_day'))then
    	ALTER TABLE insur_bank_plat
        	ADD bank_day timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kodpl'))then
    	ALTER TABLE insur_bank_plat
        	ADD kodpl varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'period_reg_date'))then
    	ALTER TABLE insur_bank_plat
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'period'))then
    	ALTER TABLE insur_bank_plat
        	ADD period timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'nom_doc'))then
    	ALTER TABLE insur_bank_plat
        	ADD nom_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'sum_all'))then
    	ALTER TABLE insur_bank_plat
        	ADD sum_all numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kom_bank_all'))then
    	ALTER TABLE insur_bank_plat
        	ADD kom_bank_all numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'bic_bank'))then
    	ALTER TABLE insur_bank_plat
        	ADD bic_bank bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'data_pp'))then
    	ALTER TABLE insur_bank_plat
        	ADD data_pp timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'cod_doc'))then
    	ALTER TABLE insur_bank_plat
        	ADD cod_doc bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kod_ysl'))then
    	ALTER TABLE insur_bank_plat
        	ADD kod_ysl bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kod_post'))then
    	ALTER TABLE insur_bank_plat
        	ADD kod_post bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'sum_by_code'))then
    	ALTER TABLE insur_bank_plat
        	ADD sum_by_code numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kom_bank'))then
    	ALTER TABLE insur_bank_plat
        	ADD kom_bank numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kom_bank_obr'))then
    	ALTER TABLE insur_bank_plat
        	ADD kom_bank_obr numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kom_eirc'))then
    	ALTER TABLE insur_bank_plat
        	ADD kom_eirc numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kom_plat'))then
    	ALTER TABLE insur_bank_plat
        	ADD kom_plat numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'flag_vozvr'))then
    	ALTER TABLE insur_bank_plat
        	ADD flag_vozvr bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'type_opl'))then
    	ALTER TABLE insur_bank_plat
        	ADD type_opl bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'kod_ypravl'))then
    	ALTER TABLE insur_bank_plat
        	ADD kod_ypravl bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'flag_nach'))then
    	ALTER TABLE insur_bank_plat
        	ADD flag_nach bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'sum_vsego'))then
    	ALTER TABLE insur_bank_plat
        	ADD sum_vsego numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'strok_vsego'))then
    	ALTER TABLE insur_bank_plat
        	ADD strok_vsego numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank_plat', 'doc_period'))then
    	ALTER TABLE insur_bank_plat
        	ADD doc_period timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_svod_bank')) IS NULL)then
        CREATE TABLE insur_svod_bank
        (
            emp_id bigint NOT NULL,
		link_id_file bigint,
		file_name varchar(255),
		bank_day timestamp without time zone,
		district_id bigint,
		str bigint,
		pay_sum numeric,
		cod_post varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_svod_bank', 'emp_id'))then
    	ALTER TABLE insur_svod_bank
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_svod_bank', 'link_id_file'))then
    	ALTER TABLE insur_svod_bank
        	ADD link_id_file bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_svod_bank', 'file_name'))then
    	ALTER TABLE insur_svod_bank
        	ADD file_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_svod_bank', 'bank_day'))then
    	ALTER TABLE insur_svod_bank
        	ADD bank_day timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_svod_bank', 'district_id'))then
    	ALTER TABLE insur_svod_bank
        	ADD district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_svod_bank', 'str'))then
    	ALTER TABLE insur_svod_bank
        	ADD str bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_svod_bank', 'pay_sum'))then
    	ALTER TABLE insur_svod_bank
        	ADD pay_sum numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_svod_bank', 'cod_post'))then
    	ALTER TABLE insur_svod_bank
        	ADD cod_post varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_qry')) IS NULL)then
        CREATE TABLE core_qry
        (
            qryid bigint NOT NULL,
		name varchar(100),
		description varchar(200),
		datefrom timestamp without time zone,
		inlist smallint NOT NULL DEFAULT 0,
		qry_user varchar(100),
		registerid bigint,
		qscondition text,
		user_id bigint,
		iscommon smallint NOT NULL,
		internal_name varchar(250),
		register_view_id varchar(200) NOT NULL,
		author varchar(50)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_qry', 'qryid'))then
    	ALTER TABLE core_qry
        	ADD qryid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'name'))then
    	ALTER TABLE core_qry
        	ADD name varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'description'))then
    	ALTER TABLE core_qry
        	ADD description varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'datefrom'))then
    	ALTER TABLE core_qry
        	ADD datefrom timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'inlist'))then
    	ALTER TABLE core_qry
        	ADD inlist smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'qry_user'))then
    	ALTER TABLE core_qry
        	ADD qry_user varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'registerid'))then
    	ALTER TABLE core_qry
        	ADD registerid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'qscondition'))then
    	ALTER TABLE core_qry
        	ADD qscondition text;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'user_id'))then
    	ALTER TABLE core_qry
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'iscommon'))then
    	ALTER TABLE core_qry
        	ADD iscommon smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'internal_name'))then
    	ALTER TABLE core_qry
        	ADD internal_name varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'register_view_id'))then
    	ALTER TABLE core_qry
        	ADD register_view_id varchar(200) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_qry', 'author'))then
    	ALTER TABLE core_qry
        	ADD author varchar(50);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_link_causes_subreason_lp')) IS NULL)then
        CREATE TABLE insur_link_causes_subreason_lp
        (
            id bigint NOT NULL,
		causes_of_damage varchar(255),
		causes_of_damage_code bigint,
		subreason_causes_of_damage varchar(255),
		subreason_causes_of_damage_code bigint,
		refinement_subreason varchar(255),
		refinement_subreason_code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'id'))then
    	ALTER TABLE insur_link_causes_subreason_lp
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'causes_of_damage'))then
    	ALTER TABLE insur_link_causes_subreason_lp
        	ADD causes_of_damage varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'causes_of_damage_code'))then
    	ALTER TABLE insur_link_causes_subreason_lp
        	ADD causes_of_damage_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'subreason_causes_of_damage'))then
    	ALTER TABLE insur_link_causes_subreason_lp
        	ADD subreason_causes_of_damage varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'subreason_causes_of_damage_code'))then
    	ALTER TABLE insur_link_causes_subreason_lp
        	ADD subreason_causes_of_damage_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'refinement_subreason'))then
    	ALTER TABLE insur_link_causes_subreason_lp
        	ADD refinement_subreason varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_link_causes_subreason_lp', 'refinement_subreason_code'))then
    	ALTER TABLE insur_link_causes_subreason_lp
        	ADD refinement_subreason_code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_gbu_no_pay_reason')) IS NULL)then
        CREATE TABLE insur_gbu_no_pay_reason
        (
            id bigint NOT NULL,
		reason varchar(255),
		type_insur varchar(255),
		short_explanation varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_gbu_no_pay_reason', 'id'))then
    	ALTER TABLE insur_gbu_no_pay_reason
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_gbu_no_pay_reason', 'reason'))then
    	ALTER TABLE insur_gbu_no_pay_reason
        	ADD reason varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_gbu_no_pay_reason', 'type_insur'))then
    	ALTER TABLE insur_gbu_no_pay_reason
        	ADD type_insur varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_gbu_no_pay_reason', 'short_explanation'))then
    	ALTER TABLE insur_gbu_no_pay_reason
        	ADD short_explanation varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_pay_to')) IS NULL)then
        CREATE TABLE insur_pay_to
        (
            emp_id bigint NOT NULL,
		fsp_id bigint,
		contract_id bigint,
		id_reestr_contr bigint,
		type_doc varchar(255),
		type_doc_code bigint,
		period timestamp without time zone,
		insurance_organization_id bigint,
		aok bigint,
		unom bigint,
		nom varchar(255),
		nomi varchar(255),
		npol varchar(255),
		npoldate timestamp without time zone,
		comnumber varchar(255),
		comdate timestamp without time zone,
		sl numeric,
		sp_fact numeric,
		sp_no numeric,
		factnumber varchar(255),
		factdate timestamp without time zone,
		ndoc varchar(255),
		ndogdat timestamp without time zone,
		link_id_file bigint,
		obj_id bigint,
		obj_reestr_id bigint,
		link_damage_id bigint,
		subject_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_pay_to', 'emp_id'))then
    	ALTER TABLE insur_pay_to
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'fsp_id'))then
    	ALTER TABLE insur_pay_to
        	ADD fsp_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'contract_id'))then
    	ALTER TABLE insur_pay_to
        	ADD contract_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'id_reestr_contr'))then
    	ALTER TABLE insur_pay_to
        	ADD id_reestr_contr bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'type_doc'))then
    	ALTER TABLE insur_pay_to
        	ADD type_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'type_doc_code'))then
    	ALTER TABLE insur_pay_to
        	ADD type_doc_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'period'))then
    	ALTER TABLE insur_pay_to
        	ADD period timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'insurance_organization_id'))then
    	ALTER TABLE insur_pay_to
        	ADD insurance_organization_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'aok'))then
    	ALTER TABLE insur_pay_to
        	ADD aok bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'unom'))then
    	ALTER TABLE insur_pay_to
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'nom'))then
    	ALTER TABLE insur_pay_to
        	ADD nom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'nomi'))then
    	ALTER TABLE insur_pay_to
        	ADD nomi varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'npol'))then
    	ALTER TABLE insur_pay_to
        	ADD npol varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'npoldate'))then
    	ALTER TABLE insur_pay_to
        	ADD npoldate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'comnumber'))then
    	ALTER TABLE insur_pay_to
        	ADD comnumber varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'comdate'))then
    	ALTER TABLE insur_pay_to
        	ADD comdate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'sl'))then
    	ALTER TABLE insur_pay_to
        	ADD sl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'sp_fact'))then
    	ALTER TABLE insur_pay_to
        	ADD sp_fact numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'sp_no'))then
    	ALTER TABLE insur_pay_to
        	ADD sp_no numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'factnumber'))then
    	ALTER TABLE insur_pay_to
        	ADD factnumber varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'factdate'))then
    	ALTER TABLE insur_pay_to
        	ADD factdate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'ndoc'))then
    	ALTER TABLE insur_pay_to
        	ADD ndoc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'ndogdat'))then
    	ALTER TABLE insur_pay_to
        	ADD ndogdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'link_id_file'))then
    	ALTER TABLE insur_pay_to
        	ADD link_id_file bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'obj_id'))then
    	ALTER TABLE insur_pay_to
        	ADD obj_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'obj_reestr_id'))then
    	ALTER TABLE insur_pay_to
        	ADD obj_reestr_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'link_damage_id'))then
    	ALTER TABLE insur_pay_to
        	ADD link_damage_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_pay_to', 'subject_id'))then
    	ALTER TABLE insur_pay_to
        	ADD subject_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_no_pay')) IS NULL)then
        CREATE TABLE insur_no_pay
        (
            emp_id bigint NOT NULL,
		fsp_id bigint,
		contract_id bigint,
		id_reestr_contr bigint,
		type_doc varchar(255),
		type_doc_code bigint,
		period_reg_date timestamp without time zone,
		insurance_organization_id bigint,
		okrug_id bigint,
		npol varchar(255),
		npoldate timestamp without time zone,
		eventdat timestamp without time zone,
		appdat timestamp without time zone,
		reject_code bigint,
		renumber varchar(255),
		redat timestamp without time zone,
		unom bigint,
		subject_id bigint,
		name varchar(255),
		ndog varchar(255),
		ndogdat timestamp without time zone,
		phonedat timestamp without time zone,
		inspnumber varchar(255),
		inspdat timestamp without time zone,
		reason varchar(255),
		reason_code bigint,
		reasabs varchar(255),
		reasabs_code bigint,
		reject varchar(255),
		link_id_file bigint,
		obj_id bigint,
		obj_reestr_id bigint,
		org_type varchar(255),
		org_type_code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_no_pay', 'emp_id'))then
    	ALTER TABLE insur_no_pay
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'fsp_id'))then
    	ALTER TABLE insur_no_pay
        	ADD fsp_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'contract_id'))then
    	ALTER TABLE insur_no_pay
        	ADD contract_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'id_reestr_contr'))then
    	ALTER TABLE insur_no_pay
        	ADD id_reestr_contr bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'type_doc'))then
    	ALTER TABLE insur_no_pay
        	ADD type_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'type_doc_code'))then
    	ALTER TABLE insur_no_pay
        	ADD type_doc_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'period_reg_date'))then
    	ALTER TABLE insur_no_pay
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'insurance_organization_id'))then
    	ALTER TABLE insur_no_pay
        	ADD insurance_organization_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'okrug_id'))then
    	ALTER TABLE insur_no_pay
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'npol'))then
    	ALTER TABLE insur_no_pay
        	ADD npol varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'npoldate'))then
    	ALTER TABLE insur_no_pay
        	ADD npoldate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'eventdat'))then
    	ALTER TABLE insur_no_pay
        	ADD eventdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'appdat'))then
    	ALTER TABLE insur_no_pay
        	ADD appdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'reject_code'))then
    	ALTER TABLE insur_no_pay
        	ADD reject_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'renumber'))then
    	ALTER TABLE insur_no_pay
        	ADD renumber varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'redat'))then
    	ALTER TABLE insur_no_pay
        	ADD redat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'unom'))then
    	ALTER TABLE insur_no_pay
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'subject_id'))then
    	ALTER TABLE insur_no_pay
        	ADD subject_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'name'))then
    	ALTER TABLE insur_no_pay
        	ADD name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'ndog'))then
    	ALTER TABLE insur_no_pay
        	ADD ndog varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'ndogdat'))then
    	ALTER TABLE insur_no_pay
        	ADD ndogdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'phonedat'))then
    	ALTER TABLE insur_no_pay
        	ADD phonedat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'inspnumber'))then
    	ALTER TABLE insur_no_pay
        	ADD inspnumber varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'inspdat'))then
    	ALTER TABLE insur_no_pay
        	ADD inspdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'reason'))then
    	ALTER TABLE insur_no_pay
        	ADD reason varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'reason_code'))then
    	ALTER TABLE insur_no_pay
        	ADD reason_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'reasabs'))then
    	ALTER TABLE insur_no_pay
        	ADD reasabs varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'reasabs_code'))then
    	ALTER TABLE insur_no_pay
        	ADD reasabs_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'reject'))then
    	ALTER TABLE insur_no_pay
        	ADD reject varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'link_id_file'))then
    	ALTER TABLE insur_no_pay
        	ADD link_id_file bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'obj_id'))then
    	ALTER TABLE insur_no_pay
        	ADD obj_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'obj_reestr_id'))then
    	ALTER TABLE insur_no_pay
        	ADD obj_reestr_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'org_type'))then
    	ALTER TABLE insur_no_pay
        	ADD org_type varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_no_pay', 'org_type_code'))then
    	ALTER TABLE insur_no_pay
        	ADD org_type_code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_plat')) IS NULL)then
        CREATE TABLE insur_input_plat
        (
            emp_id bigint NOT NULL,
		link_id_file bigint,
		fsp_id bigint,
		link_bank_id bigint,
		unom bigint,
		adres varchar(2000),
		nom varchar(255),
		kodpl varchar(255),
		ls bigint,
		tx_id varchar(255),
		pmt_date timestamp without time zone,
		date_in_tofk timestamp without time zone,
		period timestamp without time zone,
		period_reg_date timestamp without time zone,
		sum_nach numeric,
		sum_opl numeric,
		fee numeric,
		opl numeric,
		flat_type_id bigint,
		fio varchar(255),
		comment varchar(4000),
		status_identif varchar(255),
		flag_unom_no bigint,
		kod bigint,
		ndog varchar(255),
		ndogdat timestamp without time zone,
		ndops varchar(255),
		paynumber varchar(255),
		status_identif_code bigint,
		district_id bigint,
		type_source_code bigint,
		type_doc_code bigint,
		type_source varchar(255),
		type_doc varchar(255),
		insur_policy_svd_id bigint,
		load_status varchar(255),
		load_status_code bigint,
		criteria_json text,
		link_all_property_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_input_plat', 'emp_id'))then
    	ALTER TABLE insur_input_plat
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'link_id_file'))then
    	ALTER TABLE insur_input_plat
        	ADD link_id_file bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'fsp_id'))then
    	ALTER TABLE insur_input_plat
        	ADD fsp_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'link_bank_id'))then
    	ALTER TABLE insur_input_plat
        	ADD link_bank_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'unom'))then
    	ALTER TABLE insur_input_plat
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'adres'))then
    	ALTER TABLE insur_input_plat
        	ADD adres varchar(2000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'nom'))then
    	ALTER TABLE insur_input_plat
        	ADD nom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'kodpl'))then
    	ALTER TABLE insur_input_plat
        	ADD kodpl varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'ls'))then
    	ALTER TABLE insur_input_plat
        	ADD ls bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'tx_id'))then
    	ALTER TABLE insur_input_plat
        	ADD tx_id varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'pmt_date'))then
    	ALTER TABLE insur_input_plat
        	ADD pmt_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'date_in_tofk'))then
    	ALTER TABLE insur_input_plat
        	ADD date_in_tofk timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'period'))then
    	ALTER TABLE insur_input_plat
        	ADD period timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'period_reg_date'))then
    	ALTER TABLE insur_input_plat
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'sum_nach'))then
    	ALTER TABLE insur_input_plat
        	ADD sum_nach numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'sum_opl'))then
    	ALTER TABLE insur_input_plat
        	ADD sum_opl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'fee'))then
    	ALTER TABLE insur_input_plat
        	ADD fee numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'opl'))then
    	ALTER TABLE insur_input_plat
        	ADD opl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'flat_type_id'))then
    	ALTER TABLE insur_input_plat
        	ADD flat_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'fio'))then
    	ALTER TABLE insur_input_plat
        	ADD fio varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'comment'))then
    	ALTER TABLE insur_input_plat
        	ADD comment varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'status_identif'))then
    	ALTER TABLE insur_input_plat
        	ADD status_identif varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'flag_unom_no'))then
    	ALTER TABLE insur_input_plat
        	ADD flag_unom_no bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'kod'))then
    	ALTER TABLE insur_input_plat
        	ADD kod bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'ndog'))then
    	ALTER TABLE insur_input_plat
        	ADD ndog varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'ndogdat'))then
    	ALTER TABLE insur_input_plat
        	ADD ndogdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'ndops'))then
    	ALTER TABLE insur_input_plat
        	ADD ndops varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'paynumber'))then
    	ALTER TABLE insur_input_plat
        	ADD paynumber varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'status_identif_code'))then
    	ALTER TABLE insur_input_plat
        	ADD status_identif_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'district_id'))then
    	ALTER TABLE insur_input_plat
        	ADD district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'type_source_code'))then
    	ALTER TABLE insur_input_plat
        	ADD type_source_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'type_doc_code'))then
    	ALTER TABLE insur_input_plat
        	ADD type_doc_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'type_source'))then
    	ALTER TABLE insur_input_plat
        	ADD type_source varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'type_doc'))then
    	ALTER TABLE insur_input_plat
        	ADD type_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'insur_policy_svd_id'))then
    	ALTER TABLE insur_input_plat
        	ADD insur_policy_svd_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'load_status'))then
    	ALTER TABLE insur_input_plat
        	ADD load_status varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'load_status_code'))then
    	ALTER TABLE insur_input_plat
        	ADD load_status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'criteria_json'))then
    	ALTER TABLE insur_input_plat
        	ADD criteria_json text;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_plat', 'link_all_property_id'))then
    	ALTER TABLE insur_input_plat
        	ADD link_all_property_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_user_role')) IS NULL)then
        CREATE TABLE core_srd_user_role
        (
            id bigint NOT NULL,
		user_id bigint NOT NULL,
		role_id bigint NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_user_role', 'id'))then
    	ALTER TABLE core_srd_user_role
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user_role', 'user_id'))then
    	ALTER TABLE core_srd_user_role
        	ADD user_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user_role', 'role_id'))then
    	ALTER TABLE core_srd_user_role
        	ADD role_id bigint NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role_register')) IS NULL)then
        CREATE TABLE core_srd_role_register
        (
            id bigint NOT NULL,
		role_id bigint NOT NULL,
		register_id bigint NOT NULL,
		can_read smallint NOT NULL DEFAULT 0,
		can_write smallint NOT NULL DEFAULT 0,
		all_attributes smallint NOT NULL DEFAULT 0
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_role_register', 'id'))then
    	ALTER TABLE core_srd_role_register
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_register', 'role_id'))then
    	ALTER TABLE core_srd_role_register
        	ADD role_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_register', 'register_id'))then
    	ALTER TABLE core_srd_role_register
        	ADD register_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_register', 'can_read'))then
    	ALTER TABLE core_srd_role_register
        	ADD can_read smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_register', 'can_write'))then
    	ALTER TABLE core_srd_role_register
        	ADD can_write smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_register', 'all_attributes'))then
    	ALTER TABLE core_srd_role_register
        	ADD all_attributes smallint NOT NULL DEFAULT 0;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role_function')) IS NULL)then
        CREATE TABLE core_srd_role_function
        (
            id bigint NOT NULL,
		function_id bigint NOT NULL,
		role_id bigint NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_role_function', 'id'))then
    	ALTER TABLE core_srd_role_function
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_function', 'function_id'))then
    	ALTER TABLE core_srd_role_function
        	ADD function_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_function', 'role_id'))then
    	ALTER TABLE core_srd_role_function
        	ADD role_id bigint NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role')) IS NULL)then
        CREATE TABLE core_srd_role
        (
            id bigint NOT NULL,
		rolename varchar(320),
		roletag varchar(320),
		isadmin smallint NOT NULL,
		all_registers_read smallint NOT NULL DEFAULT 0,
		all_registers_write smallint NOT NULL DEFAULT 0,
		subsystem varchar(128)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_role', 'id'))then
    	ALTER TABLE core_srd_role
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role', 'rolename'))then
    	ALTER TABLE core_srd_role
        	ADD rolename varchar(320);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role', 'roletag'))then
    	ALTER TABLE core_srd_role
        	ADD roletag varchar(320);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role', 'isadmin'))then
    	ALTER TABLE core_srd_role
        	ADD isadmin smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role', 'all_registers_read'))then
    	ALTER TABLE core_srd_role
        	ADD all_registers_read smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role', 'all_registers_write'))then
    	ALTER TABLE core_srd_role
        	ADD all_registers_write smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role', 'subsystem'))then
    	ALTER TABLE core_srd_role
        	ADD subsystem varchar(128);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_role_attr')) IS NULL)then
        CREATE TABLE core_srd_role_attr
        (
            id bigint NOT NULL,
		rule_id bigint NOT NULL,
		attribute_id bigint NOT NULL,
		can_read smallint NOT NULL DEFAULT 0,
		can_write smallint NOT NULL DEFAULT 0
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_role_attr', 'id'))then
    	ALTER TABLE core_srd_role_attr
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_attr', 'rule_id'))then
    	ALTER TABLE core_srd_role_attr
        	ADD rule_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_attr', 'attribute_id'))then
    	ALTER TABLE core_srd_role_attr
        	ADD attribute_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_attr', 'can_read'))then
    	ALTER TABLE core_srd_role_attr
        	ADD can_read smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_role_attr', 'can_write'))then
    	ALTER TABLE core_srd_role_attr
        	ADD can_write smallint NOT NULL DEFAULT 0;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_function')) IS NULL)then
        CREATE TABLE core_srd_function
        (
            id bigint NOT NULL,
		functionname varchar(100),
		functiontag varchar(100),
		parent_id bigint,
		description varchar(1000)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_function', 'id'))then
    	ALTER TABLE core_srd_function
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_function', 'functionname'))then
    	ALTER TABLE core_srd_function
        	ADD functionname varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_function', 'functiontag'))then
    	ALTER TABLE core_srd_function
        	ADD functiontag varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_function', 'parent_id'))then
    	ALTER TABLE core_srd_function
        	ADD parent_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_function', 'description'))then
    	ALTER TABLE core_srd_function
        	ADD description varchar(1000);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_department')) IS NULL)then
        CREATE TABLE core_srd_department
        (
            id bigint NOT NULL,
		code varchar(10),
		name varchar(100),
		manager bigint,
		name_genitive_case varchar(100),
		is_deleted smallint DEFAULT 0
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_department', 'id'))then
    	ALTER TABLE core_srd_department
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_department', 'code'))then
    	ALTER TABLE core_srd_department
        	ADD code varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_department', 'name'))then
    	ALTER TABLE core_srd_department
        	ADD name varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_department', 'manager'))then
    	ALTER TABLE core_srd_department
        	ADD manager bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_department', 'name_genitive_case'))then
    	ALTER TABLE core_srd_department
        	ADD name_genitive_case varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_department', 'is_deleted'))then
    	ALTER TABLE core_srd_department
        	ADD is_deleted smallint DEFAULT 0;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_comment')) IS NULL)then
        CREATE TABLE insur_comment
        (
            id bigint NOT NULL,
		comment varchar(255),
		user_id bigint,
		date_create timestamp without time zone,
		link_object_id bigint,
		link_reestr_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_comment', 'id'))then
    	ALTER TABLE insur_comment
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_comment', 'comment'))then
    	ALTER TABLE insur_comment
        	ADD comment varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_comment', 'user_id'))then
    	ALTER TABLE insur_comment
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_comment', 'date_create'))then
    	ALTER TABLE insur_comment
        	ADD date_create timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_comment', 'link_object_id'))then
    	ALTER TABLE insur_comment
        	ADD link_object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_comment', 'link_reestr_id'))then
    	ALTER TABLE insur_comment
        	ADD link_reestr_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_log_file')) IS NULL)then
        CREATE TABLE insur_log_file
        (
            emp_id bigint NOT NULL,
		file_storage_id bigint,
		loaddate timestamp without time zone,
		tracedata text,
		okrug_id bigint,
		period_reg_date timestamp without time zone,
		status varchar,
		status_code bigint,
		general_status varchar,
		general_status_code bigint,
		start_date timestamp without time zone,
		end_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_log_file', 'emp_id'))then
    	ALTER TABLE insur_log_file
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'file_storage_id'))then
    	ALTER TABLE insur_log_file
        	ADD file_storage_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'loaddate'))then
    	ALTER TABLE insur_log_file
        	ADD loaddate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'tracedata'))then
    	ALTER TABLE insur_log_file
        	ADD tracedata text;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'okrug_id'))then
    	ALTER TABLE insur_log_file
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'period_reg_date'))then
    	ALTER TABLE insur_log_file
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'status'))then
    	ALTER TABLE insur_log_file
        	ADD status varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'status_code'))then
    	ALTER TABLE insur_log_file
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'general_status'))then
    	ALTER TABLE insur_log_file
        	ADD general_status varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'general_status_code'))then
    	ALTER TABLE insur_log_file
        	ADD general_status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'start_date'))then
    	ALTER TABLE insur_log_file
        	ADD start_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_log_file', 'end_date'))then
    	ALTER TABLE insur_log_file
        	ADD end_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_file_storage')) IS NULL)then
        CREATE TABLE insur_file_storage
        (
            id bigint NOT NULL,
		filename varchar(255),
		is_virtual_directory smallint,
		virtual_directory_id bigint,
		period_reg_date timestamp without time zone,
		hash text
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_file_storage', 'id'))then
    	ALTER TABLE insur_file_storage
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_storage', 'filename'))then
    	ALTER TABLE insur_file_storage
        	ADD filename varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_storage', 'is_virtual_directory'))then
    	ALTER TABLE insur_file_storage
        	ADD is_virtual_directory smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_storage', 'virtual_directory_id'))then
    	ALTER TABLE insur_file_storage
        	ADD virtual_directory_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_storage', 'period_reg_date'))then
    	ALTER TABLE insur_file_storage
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_storage', 'hash'))then
    	ALTER TABLE insur_file_storage
        	ADD hash text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_subject')) IS NULL)then
        CREATE TABLE insur_subject
        (
            emp_id bigint NOT NULL,
		okrug_id bigint,
		kod_org bigint,
		kod_upk bigint,
		subject_name varchar(255),
		org_id_t varchar(255),
		empl_role varchar(255),
		fio_adm varchar(255),
		org_adr_u varchar(255),
		org_adr_f varchar(255),
		org_phone varchar(100),
		date_input timestamp without time zone,
		birthday timestamp without time zone,
		inn varchar(255),
		kpp varchar(255),
		rach_acc varchar(255),
		num_card varchar(255),
		nom_doc varchar(255),
		date_doc timestamp without time zone,
		date_in_doc timestamp without time zone,
		org_doc varchar(255),
		type_subject varchar(255),
		type_subject_code bigint,
		bank_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_subject', 'emp_id'))then
    	ALTER TABLE insur_subject
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'okrug_id'))then
    	ALTER TABLE insur_subject
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'kod_org'))then
    	ALTER TABLE insur_subject
        	ADD kod_org bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'kod_upk'))then
    	ALTER TABLE insur_subject
        	ADD kod_upk bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'subject_name'))then
    	ALTER TABLE insur_subject
        	ADD subject_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'org_id_t'))then
    	ALTER TABLE insur_subject
        	ADD org_id_t varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'empl_role'))then
    	ALTER TABLE insur_subject
        	ADD empl_role varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'fio_adm'))then
    	ALTER TABLE insur_subject
        	ADD fio_adm varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'org_adr_u'))then
    	ALTER TABLE insur_subject
        	ADD org_adr_u varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'org_adr_f'))then
    	ALTER TABLE insur_subject
        	ADD org_adr_f varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'org_phone'))then
    	ALTER TABLE insur_subject
        	ADD org_phone varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'date_input'))then
    	ALTER TABLE insur_subject
        	ADD date_input timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'birthday'))then
    	ALTER TABLE insur_subject
        	ADD birthday timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'inn'))then
    	ALTER TABLE insur_subject
        	ADD inn varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'kpp'))then
    	ALTER TABLE insur_subject
        	ADD kpp varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'rach_acc'))then
    	ALTER TABLE insur_subject
        	ADD rach_acc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'num_card'))then
    	ALTER TABLE insur_subject
        	ADD num_card varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'nom_doc'))then
    	ALTER TABLE insur_subject
        	ADD nom_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'date_doc'))then
    	ALTER TABLE insur_subject
        	ADD date_doc timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'date_in_doc'))then
    	ALTER TABLE insur_subject
        	ADD date_in_doc timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'org_doc'))then
    	ALTER TABLE insur_subject
        	ADD org_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'type_subject'))then
    	ALTER TABLE insur_subject
        	ADD type_subject varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'type_subject_code'))then
    	ALTER TABLE insur_subject
        	ADD type_subject_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_subject', 'bank_id'))then
    	ALTER TABLE insur_subject
        	ADD bank_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('fias_addrobj')) IS NULL)then
        CREATE TABLE fias_addrobj
        (
            actstatus numeric(2, 0),
		aoguid varchar(36),
		aoid varchar(36),
		aolevel numeric(2, 0),
		areacode varchar(3),
		autocode varchar(1),
		centstatus numeric(2, 0),
		citycode varchar(3),
		code varchar(17),
		currstatus numeric(2, 0),
		enddate timestamp without time zone,
		formalname varchar(120),
		ifnsfl varchar(4),
		ifnsul varchar(4),
		nextid varchar(36),
		offname varchar(120),
		okato varchar(11),
		oktmo varchar(11),
		operstatus numeric(2, 0),
		parentguid varchar(36),
		placecode varchar(3),
		plaincode varchar(15),
		postalcode varchar(6),
		previd varchar(36),
		regioncode varchar(2),
		shortname varchar(10),
		startdate timestamp without time zone,
		streetcode varchar(4),
		terrifnsfl varchar(4),
		terrifnsul varchar(4),
		updatedate timestamp without time zone,
		ctarcode varchar(3),
		extrcode varchar(4),
		sextcode varchar(3),
		livestatus numeric(2, 0),
		normdoc varchar(36),
		plancode varchar(4),
		cadnum varchar(100),
		divtype numeric(1, 0),
		emp_id bigint NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('fias_addrobj', 'actstatus'))then
    	ALTER TABLE fias_addrobj
        	ADD actstatus numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'aoguid'))then
    	ALTER TABLE fias_addrobj
        	ADD aoguid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'aoid'))then
    	ALTER TABLE fias_addrobj
        	ADD aoid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'aolevel'))then
    	ALTER TABLE fias_addrobj
        	ADD aolevel numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'areacode'))then
    	ALTER TABLE fias_addrobj
        	ADD areacode varchar(3);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'autocode'))then
    	ALTER TABLE fias_addrobj
        	ADD autocode varchar(1);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'centstatus'))then
    	ALTER TABLE fias_addrobj
        	ADD centstatus numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'citycode'))then
    	ALTER TABLE fias_addrobj
        	ADD citycode varchar(3);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'code'))then
    	ALTER TABLE fias_addrobj
        	ADD code varchar(17);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'currstatus'))then
    	ALTER TABLE fias_addrobj
        	ADD currstatus numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'enddate'))then
    	ALTER TABLE fias_addrobj
        	ADD enddate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'formalname'))then
    	ALTER TABLE fias_addrobj
        	ADD formalname varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'ifnsfl'))then
    	ALTER TABLE fias_addrobj
        	ADD ifnsfl varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'ifnsul'))then
    	ALTER TABLE fias_addrobj
        	ADD ifnsul varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'nextid'))then
    	ALTER TABLE fias_addrobj
        	ADD nextid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'offname'))then
    	ALTER TABLE fias_addrobj
        	ADD offname varchar(120);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'okato'))then
    	ALTER TABLE fias_addrobj
        	ADD okato varchar(11);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'oktmo'))then
    	ALTER TABLE fias_addrobj
        	ADD oktmo varchar(11);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'operstatus'))then
    	ALTER TABLE fias_addrobj
        	ADD operstatus numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'parentguid'))then
    	ALTER TABLE fias_addrobj
        	ADD parentguid varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'placecode'))then
    	ALTER TABLE fias_addrobj
        	ADD placecode varchar(3);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'plaincode'))then
    	ALTER TABLE fias_addrobj
        	ADD plaincode varchar(15);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'postalcode'))then
    	ALTER TABLE fias_addrobj
        	ADD postalcode varchar(6);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'previd'))then
    	ALTER TABLE fias_addrobj
        	ADD previd varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'regioncode'))then
    	ALTER TABLE fias_addrobj
        	ADD regioncode varchar(2);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'shortname'))then
    	ALTER TABLE fias_addrobj
        	ADD shortname varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'startdate'))then
    	ALTER TABLE fias_addrobj
        	ADD startdate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'streetcode'))then
    	ALTER TABLE fias_addrobj
        	ADD streetcode varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'terrifnsfl'))then
    	ALTER TABLE fias_addrobj
        	ADD terrifnsfl varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'terrifnsul'))then
    	ALTER TABLE fias_addrobj
        	ADD terrifnsul varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'updatedate'))then
    	ALTER TABLE fias_addrobj
        	ADD updatedate timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'ctarcode'))then
    	ALTER TABLE fias_addrobj
        	ADD ctarcode varchar(3);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'extrcode'))then
    	ALTER TABLE fias_addrobj
        	ADD extrcode varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'sextcode'))then
    	ALTER TABLE fias_addrobj
        	ADD sextcode varchar(3);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'livestatus'))then
    	ALTER TABLE fias_addrobj
        	ADD livestatus numeric(2, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'normdoc'))then
    	ALTER TABLE fias_addrobj
        	ADD normdoc varchar(36);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'plancode'))then
    	ALTER TABLE fias_addrobj
        	ADD plancode varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'cadnum'))then
    	ALTER TABLE fias_addrobj
        	ADD cadnum varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'divtype'))then
    	ALTER TABLE fias_addrobj
        	ADD divtype numeric(1, 0);
	end if;


	if(not core_updstru_checkexistcolumn('fias_addrobj', 'emp_id'))then
    	ALTER TABLE fias_addrobj
        	ADD emp_id bigint NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ehd_old_numbers')) IS NULL)then
        CREATE TABLE ehd_old_numbers
        (
            id bigint NOT NULL,
		global_id bigint,
		"type" varchar(50),
		number varchar(4000),
		date timestamp without time zone,
		organ varchar(255),
		register_id bigint,
		parcel_id bigint,
		load_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'id'))then
    	ALTER TABLE ehd_old_numbers
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'global_id'))then
    	ALTER TABLE ehd_old_numbers
        	ADD global_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'type'))then
    	ALTER TABLE ehd_old_numbers
        	ADD "type" varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'number'))then
    	ALTER TABLE ehd_old_numbers
        	ADD number varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'date'))then
    	ALTER TABLE ehd_old_numbers
        	ADD date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'organ'))then
    	ALTER TABLE ehd_old_numbers
        	ADD organ varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'register_id'))then
    	ALTER TABLE ehd_old_numbers
        	ADD register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'parcel_id'))then
    	ALTER TABLE ehd_old_numbers
        	ADD parcel_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ehd_old_numbers', 'load_date'))then
    	ALTER TABLE ehd_old_numbers
        	ADD load_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_bank')) IS NULL)then
        CREATE TABLE insur_bank
        (
            emp_id bigint NOT NULL,
		bank_name varchar(255),
		date_input timestamp without time zone,
		inn varchar(255),
		kpp varchar(255),
		bic varchar(255),
		kor_acc varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_bank', 'emp_id'))then
    	ALTER TABLE insur_bank
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank', 'bank_name'))then
    	ALTER TABLE insur_bank
        	ADD bank_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank', 'date_input'))then
    	ALTER TABLE insur_bank
        	ADD date_input timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank', 'inn'))then
    	ALTER TABLE insur_bank
        	ADD inn varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank', 'kpp'))then
    	ALTER TABLE insur_bank
        	ADD kpp varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank', 'bic'))then
    	ALTER TABLE insur_bank
        	ADD bic varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_bank', 'kor_acc'))then
    	ALTER TABLE insur_bank
        	ADD kor_acc varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('system_daily_stat_db_obj')) IS NULL)then
        CREATE TABLE system_daily_stat_db_obj
        (
            stat_date timestamp without time zone,
		segment_type varchar,
		segment_name varchar,
		table_name varchar,
		size_meg numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'stat_date'))then
    	ALTER TABLE system_daily_stat_db_obj
        	ADD stat_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'segment_type'))then
    	ALTER TABLE system_daily_stat_db_obj
        	ADD segment_type varchar;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'segment_name'))then
    	ALTER TABLE system_daily_stat_db_obj
        	ADD segment_name varchar;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'table_name'))then
    	ALTER TABLE system_daily_stat_db_obj
        	ADD table_name varchar;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_stat_db_obj', 'size_meg'))then
    	ALTER TABLE system_daily_stat_db_obj
        	ADD size_meg numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register')) IS NULL)then
        CREATE TABLE core_register
        (
            registerid bigint NOT NULL,
		registername varchar(80) NOT NULL,
		registerdescription varchar(200),
		allpri_table varchar(30),
		object_table varchar(30),
		quant_table varchar(40),
		track_changes_column varchar(30),
		storage_type bigint DEFAULT 1,
		object_sequence varchar(30) DEFAULT '''REG_OBJECT_SEQ'''::character varying,
		is_virtual smallint NOT NULL DEFAULT 0,
		contains_quant_in_future smallint NOT NULL DEFAULT 1,
		db_connection_name varchar(30)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_register', 'registerid'))then
    	ALTER TABLE core_register
        	ADD registerid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'registername'))then
    	ALTER TABLE core_register
        	ADD registername varchar(80) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'registerdescription'))then
    	ALTER TABLE core_register
        	ADD registerdescription varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'allpri_table'))then
    	ALTER TABLE core_register
        	ADD allpri_table varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'object_table'))then
    	ALTER TABLE core_register
        	ADD object_table varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'quant_table'))then
    	ALTER TABLE core_register
        	ADD quant_table varchar(40);
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'track_changes_column'))then
    	ALTER TABLE core_register
        	ADD track_changes_column varchar(30);
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'storage_type'))then
    	ALTER TABLE core_register
        	ADD storage_type bigint DEFAULT 1;
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'object_sequence'))then
    	ALTER TABLE core_register
        	ADD object_sequence varchar(30) DEFAULT '''REG_OBJECT_SEQ'''::character varying;
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'is_virtual'))then
    	ALTER TABLE core_register
        	ADD is_virtual smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'contains_quant_in_future'))then
    	ALTER TABLE core_register
        	ADD contains_quant_in_future smallint NOT NULL DEFAULT 1;
	end if;


	if(not core_updstru_checkexistcolumn('core_register', 'db_connection_name'))then
    	ALTER TABLE core_register
        	ADD db_connection_name varchar(30);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register_relation')) IS NULL)then
        CREATE TABLE core_register_relation
        (
            id bigint NOT NULL,
		name varchar(200),
		parentregister bigint,
		chieldregister bigint,
		cardinality varchar(4),
		kindid bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_register_relation', 'id'))then
    	ALTER TABLE core_register_relation
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_relation', 'name'))then
    	ALTER TABLE core_register_relation
        	ADD name varchar(200);
	end if;


	if(not core_updstru_checkexistcolumn('core_register_relation', 'parentregister'))then
    	ALTER TABLE core_register_relation
        	ADD parentregister bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_relation', 'chieldregister'))then
    	ALTER TABLE core_register_relation
        	ADD chieldregister bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_relation', 'cardinality'))then
    	ALTER TABLE core_register_relation
        	ADD cardinality varchar(4);
	end if;


	if(not core_updstru_checkexistcolumn('core_register_relation', 'kindid'))then
    	ALTER TABLE core_register_relation
        	ADD kindid bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_register_attribute')) IS NULL)then
        CREATE TABLE core_register_attribute
        (
            id bigint NOT NULL,
		name varchar(300) NOT NULL,
		registerid bigint NOT NULL,
		"type" bigint NOT NULL,
		parentid bigint,
		referenceid bigint,
		value_field varchar(32),
		code_field varchar(32),
		value_template varchar(4000),
		primary_key smallint,
		user_key smallint,
		qscolumn text,
		internal_name varchar(100),
		is_nullable smallint,
		description varchar(500),
		layout text
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_register_attribute', 'id'))then
    	ALTER TABLE core_register_attribute
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'name'))then
    	ALTER TABLE core_register_attribute
        	ADD name varchar(300) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'registerid'))then
    	ALTER TABLE core_register_attribute
        	ADD registerid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'type'))then
    	ALTER TABLE core_register_attribute
        	ADD "type" bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'parentid'))then
    	ALTER TABLE core_register_attribute
        	ADD parentid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'referenceid'))then
    	ALTER TABLE core_register_attribute
        	ADD referenceid bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'value_field'))then
    	ALTER TABLE core_register_attribute
        	ADD value_field varchar(32);
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'code_field'))then
    	ALTER TABLE core_register_attribute
        	ADD code_field varchar(32);
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'value_template'))then
    	ALTER TABLE core_register_attribute
        	ADD value_template varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'primary_key'))then
    	ALTER TABLE core_register_attribute
        	ADD primary_key smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'user_key'))then
    	ALTER TABLE core_register_attribute
        	ADD user_key smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'qscolumn'))then
    	ALTER TABLE core_register_attribute
        	ADD qscolumn text;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'internal_name'))then
    	ALTER TABLE core_register_attribute
        	ADD internal_name varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'is_nullable'))then
    	ALTER TABLE core_register_attribute
        	ADD is_nullable smallint;
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'description'))then
    	ALTER TABLE core_register_attribute
        	ADD description varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('core_register_attribute', 'layout'))then
    	ALTER TABLE core_register_attribute
        	ADD layout text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_changes_log')) IS NULL)then
        CREATE TABLE insur_changes_log
        (
            emp_id bigint NOT NULL,
		object_id bigint,
		reestr_id bigint,
		load_date timestamp without time zone,
		operation_type varchar,
		operation_type_code bigint,
		reason varchar,
		user_id bigint,
		old_value varchar,
		new_value varchar
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_changes_log', 'emp_id'))then
    	ALTER TABLE insur_changes_log
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'object_id'))then
    	ALTER TABLE insur_changes_log
        	ADD object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'reestr_id'))then
    	ALTER TABLE insur_changes_log
        	ADD reestr_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'load_date'))then
    	ALTER TABLE insur_changes_log
        	ADD load_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'operation_type'))then
    	ALTER TABLE insur_changes_log
        	ADD operation_type varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'operation_type_code'))then
    	ALTER TABLE insur_changes_log
        	ADD operation_type_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'reason'))then
    	ALTER TABLE insur_changes_log
        	ADD reason varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'user_id'))then
    	ALTER TABLE insur_changes_log
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'old_value'))then
    	ALTER TABLE insur_changes_log
        	ADD old_value varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_changes_log', 'new_value'))then
    	ALTER TABLE insur_changes_log
        	ADD new_value varchar;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_usersettingslayout')) IS NULL)then
        CREATE TABLE core_srd_usersettingslayout
        (
            id bigint NOT NULL,
		user_id bigint NOT NULL,
		layout_id bigint NOT NULL,
		settings text NOT NULL
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_usersettingslayout', 'id'))then
    	ALTER TABLE core_srd_usersettingslayout
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingslayout', 'user_id'))then
    	ALTER TABLE core_srd_usersettingslayout
        	ADD user_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingslayout', 'layout_id'))then
    	ALTER TABLE core_srd_usersettingslayout
        	ADD layout_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingslayout', 'settings'))then
    	ALTER TABLE core_srd_usersettingslayout
        	ADD settings text NOT NULL;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('import_log_insur_building')) IS NULL)then
        CREATE TABLE import_log_insur_building
        (
            id bigint NOT NULL,
		ehd_parcel_id bigint,
		bti_building_id bigint,
		insur_building_id bigint,
		date_loaded timestamp without time zone,
		error_message varchar,
		error_id bigint,
		is_error integer,
		update_date_ehd timestamp without time zone,
		cad_num varchar(50),
		unom bigint,
		update_date_bti timestamp without time zone,
		error_attempts_count bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'id'))then
    	ALTER TABLE import_log_insur_building
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'ehd_parcel_id'))then
    	ALTER TABLE import_log_insur_building
        	ADD ehd_parcel_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'bti_building_id'))then
    	ALTER TABLE import_log_insur_building
        	ADD bti_building_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'insur_building_id'))then
    	ALTER TABLE import_log_insur_building
        	ADD insur_building_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'date_loaded'))then
    	ALTER TABLE import_log_insur_building
        	ADD date_loaded timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'error_message'))then
    	ALTER TABLE import_log_insur_building
        	ADD error_message varchar;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'error_id'))then
    	ALTER TABLE import_log_insur_building
        	ADD error_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'is_error'))then
    	ALTER TABLE import_log_insur_building
        	ADD is_error integer;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'update_date_ehd'))then
    	ALTER TABLE import_log_insur_building
        	ADD update_date_ehd timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'cad_num'))then
    	ALTER TABLE import_log_insur_building
        	ADD cad_num varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'unom'))then
    	ALTER TABLE import_log_insur_building
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'update_date_bti'))then
    	ALTER TABLE import_log_insur_building
        	ADD update_date_bti timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_building', 'error_attempts_count'))then
    	ALTER TABLE import_log_insur_building
        	ADD error_attempts_count bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_type_building_floor_link')) IS NULL)then
        CREATE TABLE insur_type_building_floor_link
        (
            id bigint NOT NULL,
		type_building varchar(255),
		type_building_code bigint,
		type_building_structure varchar(255),
		type_building_structure_code bigint,
		type_floors varchar(255),
		type_floors_code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_type_building_floor_link', 'id'))then
    	ALTER TABLE insur_type_building_floor_link
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_building'))then
    	ALTER TABLE insur_type_building_floor_link
        	ADD type_building varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_building_code'))then
    	ALTER TABLE insur_type_building_floor_link
        	ADD type_building_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_building_structure'))then
    	ALTER TABLE insur_type_building_floor_link
        	ADD type_building_structure varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_building_structure_code'))then
    	ALTER TABLE insur_type_building_floor_link
        	ADD type_building_structure_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_floors'))then
    	ALTER TABLE insur_type_building_floor_link
        	ADD type_floors varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_type_building_floor_link', 'type_floors_code'))then
    	ALTER TABLE insur_type_building_floor_link
        	ADD type_floors_code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_fsp_q')) IS NULL)then
        CREATE TABLE insur_fsp_q
        (
            id bigint NOT NULL,
		emp_id bigint NOT NULL,
		actual smallint NOT NULL,
		status smallint NOT NULL,
		s_ timestamp without time zone,
		po_ timestamp without time zone,
		fsp_type varchar(255),
		fsp_number varchar(255),
		ls bigint,
		obj_id bigint,
		obj_reestr_id bigint,
		contract_id bigint,
		id_reestr_contr bigint,
		fsp_type_code bigint,
		date_open timestamp without time zone,
		kodpl varchar,
		opl_kodpl numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'id'))then
    	ALTER TABLE insur_fsp_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'emp_id'))then
    	ALTER TABLE insur_fsp_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'actual'))then
    	ALTER TABLE insur_fsp_q
        	ADD actual smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'status'))then
    	ALTER TABLE insur_fsp_q
        	ADD status smallint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 's_'))then
    	ALTER TABLE insur_fsp_q
        	ADD s_ timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'po_'))then
    	ALTER TABLE insur_fsp_q
        	ADD po_ timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'fsp_type'))then
    	ALTER TABLE insur_fsp_q
        	ADD fsp_type varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'fsp_number'))then
    	ALTER TABLE insur_fsp_q
        	ADD fsp_number varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'ls'))then
    	ALTER TABLE insur_fsp_q
        	ADD ls bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'obj_id'))then
    	ALTER TABLE insur_fsp_q
        	ADD obj_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'obj_reestr_id'))then
    	ALTER TABLE insur_fsp_q
        	ADD obj_reestr_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'contract_id'))then
    	ALTER TABLE insur_fsp_q
        	ADD contract_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'id_reestr_contr'))then
    	ALTER TABLE insur_fsp_q
        	ADD id_reestr_contr bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'fsp_type_code'))then
    	ALTER TABLE insur_fsp_q
        	ADD fsp_type_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'date_open'))then
    	ALTER TABLE insur_fsp_q
        	ADD date_open timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'kodpl'))then
    	ALTER TABLE insur_fsp_q
        	ADD kodpl varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_fsp_q', 'opl_kodpl'))then
    	ALTER TABLE insur_fsp_q
        	ADD opl_kodpl numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('import_log_insur_flat_b')) IS NULL)then
        CREATE TABLE import_log_insur_flat_b
        (
            id bigint NOT NULL,
		ehd_parcel_id bigint,
		bti_building_id bigint,
		insur_building_id bigint,
		date_loaded timestamp without time zone,
		error_message varchar,
		error_id bigint,
		is_error integer,
		update_date_ehd timestamp without time zone,
		update_date_bti timestamp without time zone,
		cad_num varchar(50),
		unom bigint,
		error_attempts_count bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'id'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'ehd_parcel_id'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD ehd_parcel_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'bti_building_id'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD bti_building_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'insur_building_id'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD insur_building_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'date_loaded'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD date_loaded timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'error_message'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD error_message varchar;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'error_id'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD error_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'is_error'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD is_error integer;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'update_date_ehd'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD update_date_ehd timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'update_date_bti'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD update_date_bti timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'cad_num'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD cad_num varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'unom'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat_b', 'error_attempts_count'))then
    	ALTER TABLE import_log_insur_flat_b
        	ADD error_attempts_count bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('cipjs_import_bti_daily_log')) IS NULL)then
        CREATE TABLE cipjs_import_bti_daily_log
        (
            bti_id bigint,
		num_cadnum varchar(255),
		unom varchar(255),
		is_new integer,
		alt_building_id bigint,
		dateedit timestamp without time zone,
		is_error integer,
		message varchar(255),
		error_id bigint,
		task_id bigint,
		insert_date timestamp without time zone,
		import_date timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'bti_id'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD bti_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'num_cadnum'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD num_cadnum varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'unom'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD unom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'is_new'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD is_new integer;
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'alt_building_id'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD alt_building_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'dateedit'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD dateedit timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'is_error'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD is_error integer;
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'message'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD message varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'error_id'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD error_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'task_id'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD task_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'insert_date'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD insert_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('cipjs_import_bti_daily_log', 'import_date'))then
    	ALTER TABLE cipjs_import_bti_daily_log
        	ADD import_date timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_file_process_log')) IS NULL)then
        CREATE TABLE insur_file_process_log
        (
            emp_id bigint NOT NULL,
		input_file_id bigint,
		total_count bigint,
		status varchar,
		status_code bigint,
		start_date timestamp without time zone,
		end_date timestamp without time zone,
		processed_count bigint,
		total_fsp_count bigint,
		processed_fsp_count bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'emp_id'))then
    	ALTER TABLE insur_file_process_log
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'input_file_id'))then
    	ALTER TABLE insur_file_process_log
        	ADD input_file_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'total_count'))then
    	ALTER TABLE insur_file_process_log
        	ADD total_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'status'))then
    	ALTER TABLE insur_file_process_log
        	ADD status varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'status_code'))then
    	ALTER TABLE insur_file_process_log
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'start_date'))then
    	ALTER TABLE insur_file_process_log
        	ADD start_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'end_date'))then
    	ALTER TABLE insur_file_process_log
        	ADD end_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'processed_count'))then
    	ALTER TABLE insur_file_process_log
        	ADD processed_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'total_fsp_count'))then
    	ALTER TABLE insur_file_process_log
        	ADD total_fsp_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_process_log', 'processed_fsp_count'))then
    	ALTER TABLE insur_file_process_log
        	ADD processed_fsp_count bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_documents')) IS NULL)then
        CREATE TABLE insur_documents
        (
            emp_id bigint NOT NULL,
		doc_type_id bigint,
		doc_is_have smallint,
		obj_id bigint,
		reestr_id bigint,
		doc_type_m varchar(255),
		doc_type_m_code bigint,
		doc_number varchar(255),
		doc_date timestamp without time zone,
		doc_org_id bigint,
		file_storage_id bigint,
		user_id bigint,
		date_create timestamp without time zone,
		fio_scan varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_documents', 'emp_id'))then
    	ALTER TABLE insur_documents
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'doc_type_id'))then
    	ALTER TABLE insur_documents
        	ADD doc_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'doc_is_have'))then
    	ALTER TABLE insur_documents
        	ADD doc_is_have smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'obj_id'))then
    	ALTER TABLE insur_documents
        	ADD obj_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'reestr_id'))then
    	ALTER TABLE insur_documents
        	ADD reestr_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'doc_type_m'))then
    	ALTER TABLE insur_documents
        	ADD doc_type_m varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'doc_type_m_code'))then
    	ALTER TABLE insur_documents
        	ADD doc_type_m_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'doc_number'))then
    	ALTER TABLE insur_documents
        	ADD doc_number varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'doc_date'))then
    	ALTER TABLE insur_documents
        	ADD doc_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'doc_org_id'))then
    	ALTER TABLE insur_documents
        	ADD doc_org_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'file_storage_id'))then
    	ALTER TABLE insur_documents
        	ADD file_storage_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'user_id'))then
    	ALTER TABLE insur_documents
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'date_create'))then
    	ALTER TABLE insur_documents
        	ADD date_create timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_documents', 'fio_scan'))then
    	ALTER TABLE insur_documents
        	ADD fio_scan varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_agreement_project')) IS NULL)then
        CREATE TABLE insur_agreement_project
        (
            emp_id bigint NOT NULL,
		got_user_id bigint,
		got_date timestamp without time zone,
		approval_user_id bigint,
		approval_date timestamp without time zone,
		note varchar(1000),
		calculation_id bigint,
		comment_spravka varchar(1000),
		resume_spravka varchar(1000),
		part_moscow numeric,
		kat_1 smallint,
		kat_2 smallint,
		kat_3 smallint,
		progect_num varchar(255),
		size_bonus_mkd numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'emp_id'))then
    	ALTER TABLE insur_agreement_project
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'got_user_id'))then
    	ALTER TABLE insur_agreement_project
        	ADD got_user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'got_date'))then
    	ALTER TABLE insur_agreement_project
        	ADD got_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'approval_user_id'))then
    	ALTER TABLE insur_agreement_project
        	ADD approval_user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'approval_date'))then
    	ALTER TABLE insur_agreement_project
        	ADD approval_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'note'))then
    	ALTER TABLE insur_agreement_project
        	ADD note varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'calculation_id'))then
    	ALTER TABLE insur_agreement_project
        	ADD calculation_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'comment_spravka'))then
    	ALTER TABLE insur_agreement_project
        	ADD comment_spravka varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'resume_spravka'))then
    	ALTER TABLE insur_agreement_project
        	ADD resume_spravka varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'part_moscow'))then
    	ALTER TABLE insur_agreement_project
        	ADD part_moscow numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'kat_1'))then
    	ALTER TABLE insur_agreement_project
        	ADD kat_1 smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'kat_2'))then
    	ALTER TABLE insur_agreement_project
        	ADD kat_2 smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'kat_3'))then
    	ALTER TABLE insur_agreement_project
        	ADD kat_3 smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'progect_num'))then
    	ALTER TABLE insur_agreement_project
        	ADD progect_num varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_agreement_project', 'size_bonus_mkd'))then
    	ALTER TABLE insur_agreement_project
        	ADD size_bonus_mkd numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_doc_base_type')) IS NULL)then
        CREATE TABLE insur_doc_base_type
        (
            id bigint NOT NULL,
		document_base varchar(255) NOT NULL,
		"type" varchar(10) NOT NULL,
		ordinal bigint DEFAULT 100,
		need_set_date smallint DEFAULT 0
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_doc_base_type', 'id'))then
    	ALTER TABLE insur_doc_base_type
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_doc_base_type', 'document_base'))then
    	ALTER TABLE insur_doc_base_type
        	ADD document_base varchar(255) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_doc_base_type', 'type'))then
    	ALTER TABLE insur_doc_base_type
        	ADD "type" varchar(10) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_doc_base_type', 'ordinal'))then
    	ALTER TABLE insur_doc_base_type
        	ADD ordinal bigint DEFAULT 100;
	end if;


	if(not core_updstru_checkexistcolumn('insur_doc_base_type', 'need_set_date'))then
    	ALTER TABLE insur_doc_base_type
        	ADD need_set_date smallint DEFAULT 0;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('ref_addr_okrug')) IS NULL)then
        CREATE TABLE ref_addr_okrug
        (
            okrug_id bigint NOT NULL,
		subject_rf_id bigint,
		full_name varchar(1000),
		short_name varchar(1000),
		name_for_sort varchar(1000),
		steks_code bigint,
		omk_code varchar(100),
		name varchar(1000),
		type_ref bigint,
		code_givc varchar(500),
		insurance_company_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'okrug_id'))then
    	ALTER TABLE ref_addr_okrug
        	ADD okrug_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'subject_rf_id'))then
    	ALTER TABLE ref_addr_okrug
        	ADD subject_rf_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'full_name'))then
    	ALTER TABLE ref_addr_okrug
        	ADD full_name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'short_name'))then
    	ALTER TABLE ref_addr_okrug
        	ADD short_name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'name_for_sort'))then
    	ALTER TABLE ref_addr_okrug
        	ADD name_for_sort varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'steks_code'))then
    	ALTER TABLE ref_addr_okrug
        	ADD steks_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'omk_code'))then
    	ALTER TABLE ref_addr_okrug
        	ADD omk_code varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'name'))then
    	ALTER TABLE ref_addr_okrug
        	ADD name varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'type_ref'))then
    	ALTER TABLE ref_addr_okrug
        	ADD type_ref bigint;
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'code_givc'))then
    	ALTER TABLE ref_addr_okrug
        	ADD code_givc varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('ref_addr_okrug', 'insurance_company_id'))then
    	ALTER TABLE ref_addr_okrug
        	ADD insurance_company_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_flat_q')) IS NULL)then
        CREATE TABLE insur_flat_q
        (
            id bigint NOT NULL,
		emp_id bigint NOT NULL,
		actual bigint NOT NULL,
		status bigint NOT NULL,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		load_date timestamp without time zone,
		cadastr_num varchar,
		unom bigint,
		kvnom varchar,
		flatstatus smallint,
		prkom smallint,
		kol_gp smallint,
		fopl numeric,
		ppl numeric,
		gpl numeric,
		guid_fias_flat varchar(255),
		guid_fias_mkd varchar(255),
		link_bti_flat bigint,
		link_insur_egrn bigint,
		link_object_mkd bigint,
		source_atrib varchar,
		flag_insur smallint,
		address_fias_mkd varchar(500),
		type_flat varchar(255),
		type_flat_code bigint,
		type_flat_2 varchar(255),
		type_flat_2_code bigint,
		cadastr_date timestamp without time zone,
		status_egrn varchar(255),
		status_egrn_code bigint,
		opl numeric,
		cadastr_remove timestamp without time zone,
		dept_id bigint NOT NULL,
		klass_flat_code bigint,
		klass_flat varchar(255),
		code_kladr varchar(25),
		source_input varchar(255),
		source_input_code bigint,
		note varchar(4000)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_flat_q', 'id'))then
    	ALTER TABLE insur_flat_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'emp_id'))then
    	ALTER TABLE insur_flat_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'actual'))then
    	ALTER TABLE insur_flat_q
        	ADD actual bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'status'))then
    	ALTER TABLE insur_flat_q
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 's_'))then
    	ALTER TABLE insur_flat_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'po_'))then
    	ALTER TABLE insur_flat_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'load_date'))then
    	ALTER TABLE insur_flat_q
        	ADD load_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'cadastr_num'))then
    	ALTER TABLE insur_flat_q
        	ADD cadastr_num varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'unom'))then
    	ALTER TABLE insur_flat_q
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'kvnom'))then
    	ALTER TABLE insur_flat_q
        	ADD kvnom varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'flatstatus'))then
    	ALTER TABLE insur_flat_q
        	ADD flatstatus smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'prkom'))then
    	ALTER TABLE insur_flat_q
        	ADD prkom smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'kol_gp'))then
    	ALTER TABLE insur_flat_q
        	ADD kol_gp smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'fopl'))then
    	ALTER TABLE insur_flat_q
        	ADD fopl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'ppl'))then
    	ALTER TABLE insur_flat_q
        	ADD ppl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'gpl'))then
    	ALTER TABLE insur_flat_q
        	ADD gpl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'guid_fias_flat'))then
    	ALTER TABLE insur_flat_q
        	ADD guid_fias_flat varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'guid_fias_mkd'))then
    	ALTER TABLE insur_flat_q
        	ADD guid_fias_mkd varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'link_bti_flat'))then
    	ALTER TABLE insur_flat_q
        	ADD link_bti_flat bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'link_insur_egrn'))then
    	ALTER TABLE insur_flat_q
        	ADD link_insur_egrn bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'link_object_mkd'))then
    	ALTER TABLE insur_flat_q
        	ADD link_object_mkd bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'source_atrib'))then
    	ALTER TABLE insur_flat_q
        	ADD source_atrib varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'flag_insur'))then
    	ALTER TABLE insur_flat_q
        	ADD flag_insur smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'address_fias_mkd'))then
    	ALTER TABLE insur_flat_q
        	ADD address_fias_mkd varchar(500);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'type_flat'))then
    	ALTER TABLE insur_flat_q
        	ADD type_flat varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'type_flat_code'))then
    	ALTER TABLE insur_flat_q
        	ADD type_flat_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'type_flat_2'))then
    	ALTER TABLE insur_flat_q
        	ADD type_flat_2 varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'type_flat_2_code'))then
    	ALTER TABLE insur_flat_q
        	ADD type_flat_2_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'cadastr_date'))then
    	ALTER TABLE insur_flat_q
        	ADD cadastr_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'status_egrn'))then
    	ALTER TABLE insur_flat_q
        	ADD status_egrn varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'status_egrn_code'))then
    	ALTER TABLE insur_flat_q
        	ADD status_egrn_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'opl'))then
    	ALTER TABLE insur_flat_q
        	ADD opl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'cadastr_remove'))then
    	ALTER TABLE insur_flat_q
        	ADD cadastr_remove timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'dept_id'))then
    	ALTER TABLE insur_flat_q
        	ADD dept_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'klass_flat_code'))then
    	ALTER TABLE insur_flat_q
        	ADD klass_flat_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'klass_flat'))then
    	ALTER TABLE insur_flat_q
        	ADD klass_flat varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'code_kladr'))then
    	ALTER TABLE insur_flat_q
        	ADD code_kladr varchar(25);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'source_input'))then
    	ALTER TABLE insur_flat_q
        	ADD source_input varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'source_input_code'))then
    	ALTER TABLE insur_flat_q
        	ADD source_input_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_flat_q', 'note'))then
    	ALTER TABLE insur_flat_q
        	ADD note varchar(4000);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('system_daily_statistics')) IS NULL)then
        CREATE TABLE system_daily_statistics
        (
            id bigint NOT NULL,
		stat_date timestamp without time zone,
		db_size numeric,
		errors bigint,
		warnings bigint,
		actions bigint,
		sessions bigint,
		changes bigint,
		diagnostics_slow bigint,
		bti_objects_loaded bigint,
		bti_objects_loaded_error bigint,
		ehd_objects_loaded bigint,
		ehd_objects_loaded_error bigint,
		insur_building_loaded bigint,
		insur_building_loaded_error bigint,
		insur_flat_loaded bigint,
		insur_flat_loaded_error bigint,
		total_count_insur_building bigint,
		total_count_insur_flat bigint,
		long_proc_run bigint,
		long_proc_run_error bigint,
		long_proc_insur_load_run bigint,
		long_proc_insur_load_run_error bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'id'))then
    	ALTER TABLE system_daily_statistics
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'stat_date'))then
    	ALTER TABLE system_daily_statistics
        	ADD stat_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'db_size'))then
    	ALTER TABLE system_daily_statistics
        	ADD db_size numeric;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'errors'))then
    	ALTER TABLE system_daily_statistics
        	ADD errors bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'warnings'))then
    	ALTER TABLE system_daily_statistics
        	ADD warnings bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'actions'))then
    	ALTER TABLE system_daily_statistics
        	ADD actions bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'sessions'))then
    	ALTER TABLE system_daily_statistics
        	ADD sessions bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'changes'))then
    	ALTER TABLE system_daily_statistics
        	ADD changes bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'diagnostics_slow'))then
    	ALTER TABLE system_daily_statistics
        	ADD diagnostics_slow bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'bti_objects_loaded'))then
    	ALTER TABLE system_daily_statistics
        	ADD bti_objects_loaded bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'bti_objects_loaded_error'))then
    	ALTER TABLE system_daily_statistics
        	ADD bti_objects_loaded_error bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'ehd_objects_loaded'))then
    	ALTER TABLE system_daily_statistics
        	ADD ehd_objects_loaded bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'ehd_objects_loaded_error'))then
    	ALTER TABLE system_daily_statistics
        	ADD ehd_objects_loaded_error bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'insur_building_loaded'))then
    	ALTER TABLE system_daily_statistics
        	ADD insur_building_loaded bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'insur_building_loaded_error'))then
    	ALTER TABLE system_daily_statistics
        	ADD insur_building_loaded_error bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'insur_flat_loaded'))then
    	ALTER TABLE system_daily_statistics
        	ADD insur_flat_loaded bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'insur_flat_loaded_error'))then
    	ALTER TABLE system_daily_statistics
        	ADD insur_flat_loaded_error bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'total_count_insur_building'))then
    	ALTER TABLE system_daily_statistics
        	ADD total_count_insur_building bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'total_count_insur_flat'))then
    	ALTER TABLE system_daily_statistics
        	ADD total_count_insur_flat bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'long_proc_run'))then
    	ALTER TABLE system_daily_statistics
        	ADD long_proc_run bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'long_proc_run_error'))then
    	ALTER TABLE system_daily_statistics
        	ADD long_proc_run_error bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'long_proc_insur_load_run'))then
    	ALTER TABLE system_daily_statistics
        	ADD long_proc_insur_load_run bigint;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_statistics', 'long_proc_insur_load_run_error'))then
    	ALTER TABLE system_daily_statistics
        	ADD long_proc_insur_load_run_error bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_district')) IS NULL)then
        CREATE TABLE insur_district
        (
            id bigint NOT NULL,
		code bigint,
		name varchar(255),
		okrug_id bigint,
		ref_addr_district_id bigint,
		ref_addr_district_code_givc bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_district', 'id'))then
    	ALTER TABLE insur_district
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_district', 'code'))then
    	ALTER TABLE insur_district
        	ADD code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_district', 'name'))then
    	ALTER TABLE insur_district
        	ADD name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_district', 'okrug_id'))then
    	ALTER TABLE insur_district
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_district', 'ref_addr_district_id'))then
    	ALTER TABLE insur_district
        	ADD ref_addr_district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_district', 'ref_addr_district_code_givc'))then
    	ALTER TABLE insur_district
        	ADD ref_addr_district_code_givc bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_okrug')) IS NULL)then
        CREATE TABLE insur_okrug
        (
            id bigint NOT NULL,
		code bigint NOT NULL,
		name varchar(255),
		short_name varchar(255),
		insurance_company_id bigint,
		ref_addr_okrug_id bigint,
		ref_addr_okrug_code_givc bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_okrug', 'id'))then
    	ALTER TABLE insur_okrug
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_okrug', 'code'))then
    	ALTER TABLE insur_okrug
        	ADD code bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_okrug', 'name'))then
    	ALTER TABLE insur_okrug
        	ADD name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_okrug', 'short_name'))then
    	ALTER TABLE insur_okrug
        	ADD short_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_okrug', 'insurance_company_id'))then
    	ALTER TABLE insur_okrug
        	ADD insurance_company_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_okrug', 'ref_addr_okrug_id'))then
    	ALTER TABLE insur_okrug
        	ADD ref_addr_okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_okrug', 'ref_addr_okrug_code_givc'))then
    	ALTER TABLE insur_okrug
        	ADD ref_addr_okrug_code_givc bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_building_q')) IS NULL)then
        CREATE TABLE insur_building_q
        (
            emp_id bigint NOT NULL,
		s_ timestamp without time zone NOT NULL,
		po_ timestamp without time zone NOT NULL,
		actual bigint NOT NULL,
		load_date timestamp without time zone,
		cadastr_num varchar,
		status_egrn varchar,
		status_sost_bti varchar,
		cadastr_date timestamp without time zone,
		okrug_id bigint,
		district_id bigint,
		unom bigint,
		type_mkd_code bigint,
		year_stroi smallint,
		count_floor smallint,
		kol_gp integer,
		opl numeric,
		opl_g numeric,
		opl_n numeric,
		bpl numeric,
		hpl numeric,
		lpl numeric,
		lfpq smallint,
		lfgpq smallint,
		lfgq smallint,
		guid_fias_mkd varchar(255),
		link_bti_fsks bigint,
		link_egrn_bild bigint,
		source_atrib varchar,
		flag_insur smallint,
		type_mkd varchar(255),
		purpose_name varchar(255),
		purpose_name_code bigint,
		status_egrn_code bigint,
		krovpl numeric,
		stroi_price numeric,
		status_sost_bti_code bigint,
		address_id bigint,
		cadastr_remove timestamp without time zone,
		id bigint NOT NULL,
		status bigint NOT NULL,
		dept_id bigint,
		code_kladr varchar(25),
		epl numeric,
		pizn numeric,
		source_input varchar(255),
		source_input_code bigint,
		note varchar(4000),
		flag_insur_calculated smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_building_q', 'emp_id'))then
    	ALTER TABLE insur_building_q
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 's_'))then
    	ALTER TABLE insur_building_q
        	ADD s_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'po_'))then
    	ALTER TABLE insur_building_q
        	ADD po_ timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'actual'))then
    	ALTER TABLE insur_building_q
        	ADD actual bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'load_date'))then
    	ALTER TABLE insur_building_q
        	ADD load_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'cadastr_num'))then
    	ALTER TABLE insur_building_q
        	ADD cadastr_num varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'status_egrn'))then
    	ALTER TABLE insur_building_q
        	ADD status_egrn varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'status_sost_bti'))then
    	ALTER TABLE insur_building_q
        	ADD status_sost_bti varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'cadastr_date'))then
    	ALTER TABLE insur_building_q
        	ADD cadastr_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'okrug_id'))then
    	ALTER TABLE insur_building_q
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'district_id'))then
    	ALTER TABLE insur_building_q
        	ADD district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'unom'))then
    	ALTER TABLE insur_building_q
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'type_mkd_code'))then
    	ALTER TABLE insur_building_q
        	ADD type_mkd_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'year_stroi'))then
    	ALTER TABLE insur_building_q
        	ADD year_stroi smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'count_floor'))then
    	ALTER TABLE insur_building_q
        	ADD count_floor smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'kol_gp'))then
    	ALTER TABLE insur_building_q
        	ADD kol_gp integer;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'opl'))then
    	ALTER TABLE insur_building_q
        	ADD opl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'opl_g'))then
    	ALTER TABLE insur_building_q
        	ADD opl_g numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'opl_n'))then
    	ALTER TABLE insur_building_q
        	ADD opl_n numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'bpl'))then
    	ALTER TABLE insur_building_q
        	ADD bpl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'hpl'))then
    	ALTER TABLE insur_building_q
        	ADD hpl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'lpl'))then
    	ALTER TABLE insur_building_q
        	ADD lpl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'lfpq'))then
    	ALTER TABLE insur_building_q
        	ADD lfpq smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'lfgpq'))then
    	ALTER TABLE insur_building_q
        	ADD lfgpq smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'lfgq'))then
    	ALTER TABLE insur_building_q
        	ADD lfgq smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'guid_fias_mkd'))then
    	ALTER TABLE insur_building_q
        	ADD guid_fias_mkd varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'link_bti_fsks'))then
    	ALTER TABLE insur_building_q
        	ADD link_bti_fsks bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'link_egrn_bild'))then
    	ALTER TABLE insur_building_q
        	ADD link_egrn_bild bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'source_atrib'))then
    	ALTER TABLE insur_building_q
        	ADD source_atrib varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'flag_insur'))then
    	ALTER TABLE insur_building_q
        	ADD flag_insur smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'type_mkd'))then
    	ALTER TABLE insur_building_q
        	ADD type_mkd varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'purpose_name'))then
    	ALTER TABLE insur_building_q
        	ADD purpose_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'purpose_name_code'))then
    	ALTER TABLE insur_building_q
        	ADD purpose_name_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'status_egrn_code'))then
    	ALTER TABLE insur_building_q
        	ADD status_egrn_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'krovpl'))then
    	ALTER TABLE insur_building_q
        	ADD krovpl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'stroi_price'))then
    	ALTER TABLE insur_building_q
        	ADD stroi_price numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'status_sost_bti_code'))then
    	ALTER TABLE insur_building_q
        	ADD status_sost_bti_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'address_id'))then
    	ALTER TABLE insur_building_q
        	ADD address_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'cadastr_remove'))then
    	ALTER TABLE insur_building_q
        	ADD cadastr_remove timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'id'))then
    	ALTER TABLE insur_building_q
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'status'))then
    	ALTER TABLE insur_building_q
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'dept_id'))then
    	ALTER TABLE insur_building_q
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'code_kladr'))then
    	ALTER TABLE insur_building_q
        	ADD code_kladr varchar(25);
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'epl'))then
    	ALTER TABLE insur_building_q
        	ADD epl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'pizn'))then
    	ALTER TABLE insur_building_q
        	ADD pizn numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'source_input'))then
    	ALTER TABLE insur_building_q
        	ADD source_input varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'source_input_code'))then
    	ALTER TABLE insur_building_q
        	ADD source_input_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'note'))then
    	ALTER TABLE insur_building_q
        	ADD note varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_building_q', 'flag_insur_calculated'))then
    	ALTER TABLE insur_building_q
        	ADD flag_insur_calculated smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_balance')) IS NULL)then
        CREATE TABLE insur_balance
        (
            emp_id bigint NOT NULL,
		fsp_id bigint,
		flag_opl smallint,
		link_input_nach bigint,
		flag_insur smallint,
		ostatok_sum numeric,
		period_reg_date timestamp without time zone,
		sum_opl numeric(10, 2),
		sum_nach_mfc numeric(10, 2),
		sum_nach_gby numeric(10, 2),
		sum_nach_opl numeric(10, 2),
		strah_end timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_balance', 'emp_id'))then
    	ALTER TABLE insur_balance
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'fsp_id'))then
    	ALTER TABLE insur_balance
        	ADD fsp_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'flag_opl'))then
    	ALTER TABLE insur_balance
        	ADD flag_opl smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'link_input_nach'))then
    	ALTER TABLE insur_balance
        	ADD link_input_nach bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'flag_insur'))then
    	ALTER TABLE insur_balance
        	ADD flag_insur smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'ostatok_sum'))then
    	ALTER TABLE insur_balance
        	ADD ostatok_sum numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'period_reg_date'))then
    	ALTER TABLE insur_balance
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'sum_opl'))then
    	ALTER TABLE insur_balance
        	ADD sum_opl numeric(10, 2);
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'sum_nach_mfc'))then
    	ALTER TABLE insur_balance
        	ADD sum_nach_mfc numeric(10, 2);
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'sum_nach_gby'))then
    	ALTER TABLE insur_balance
        	ADD sum_nach_gby numeric(10, 2);
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'sum_nach_opl'))then
    	ALTER TABLE insur_balance
        	ADD sum_nach_opl numeric(10, 2);
	end if;


	if(not core_updstru_checkexistcolumn('insur_balance', 'strah_end'))then
    	ALTER TABLE insur_balance
        	ADD strah_end timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_reestr_pay')) IS NULL)then
        CREATE TABLE insur_reestr_pay
        (
            emp_id bigint NOT NULL,
		num varchar(255),
		date timestamp without time zone,
		data_creation timestamp without time zone,
		data_payment timestamp without time zone,
		user_creation varchar(255),
		user_payment varchar(255),
		status varchar(255),
		status_code bigint,
		"type" varchar(255),
		type_code bigint,
		note varchar(255),
		file_storage_id_dgi bigint,
		file_storage_id_pay bigint,
		date_cancel timestamp without time zone,
		cancel_user_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'emp_id'))then
    	ALTER TABLE insur_reestr_pay
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'num'))then
    	ALTER TABLE insur_reestr_pay
        	ADD num varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'date'))then
    	ALTER TABLE insur_reestr_pay
        	ADD date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'data_creation'))then
    	ALTER TABLE insur_reestr_pay
        	ADD data_creation timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'data_payment'))then
    	ALTER TABLE insur_reestr_pay
        	ADD data_payment timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'user_creation'))then
    	ALTER TABLE insur_reestr_pay
        	ADD user_creation varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'user_payment'))then
    	ALTER TABLE insur_reestr_pay
        	ADD user_payment varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'status'))then
    	ALTER TABLE insur_reestr_pay
        	ADD status varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'status_code'))then
    	ALTER TABLE insur_reestr_pay
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'type'))then
    	ALTER TABLE insur_reestr_pay
        	ADD "type" varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'type_code'))then
    	ALTER TABLE insur_reestr_pay
        	ADD type_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'note'))then
    	ALTER TABLE insur_reestr_pay
        	ADD note varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'file_storage_id_dgi'))then
    	ALTER TABLE insur_reestr_pay
        	ADD file_storage_id_dgi bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'file_storage_id_pay'))then
    	ALTER TABLE insur_reestr_pay
        	ADD file_storage_id_pay bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'date_cancel'))then
    	ALTER TABLE insur_reestr_pay
        	ADD date_cancel timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_reestr_pay', 'cancel_user_id'))then
    	ALTER TABLE insur_reestr_pay
        	ADD cancel_user_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_file_plat_identify_log')) IS NULL)then
        CREATE TABLE insur_file_plat_identify_log
        (
            emp_id bigint NOT NULL,
		input_file_id bigint,
		plat_count bigint,
		status varchar,
		status_code bigint,
		start_date timestamp without time zone,
		end_date timestamp without time zone,
		identified_count bigint,
		not_identified_count bigint,
		need_process smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'emp_id'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'input_file_id'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD input_file_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'plat_count'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD plat_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'status'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD status varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'status_code'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'start_date'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD start_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'end_date'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD end_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'identified_count'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD identified_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'not_identified_count'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD not_identified_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_file_plat_identify_log', 'need_process'))then
    	ALTER TABLE insur_file_plat_identify_log
        	ADD need_process smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_all_property')) IS NULL)then
        CREATE TABLE insur_all_property
        (
            emp_id bigint NOT NULL,
		fsp_id bigint,
		insurance_id bigint,
		okrug_id bigint,
		unom bigint,
		subject_id bigint,
		name varchar(2000),
		ndog varchar(255),
		ndogdat timestamp without time zone,
		st1 numeric,
		st2 numeric,
		st3 numeric,
		ss1 numeric,
		ss2 numeric,
		ss3 numeric,
		part numeric,
		part_city numeric,
		ras_pripay numeric,
		link_id_file bigint,
		obj_id bigint,
		org_type varchar(255),
		org_type_code bigint,
		paysign varchar(255),
		paysign_code bigint,
		status varchar(255),
		status_code bigint,
		org_id_file bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_all_property', 'emp_id'))then
    	ALTER TABLE insur_all_property
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'fsp_id'))then
    	ALTER TABLE insur_all_property
        	ADD fsp_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'insurance_id'))then
    	ALTER TABLE insur_all_property
        	ADD insurance_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'okrug_id'))then
    	ALTER TABLE insur_all_property
        	ADD okrug_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'unom'))then
    	ALTER TABLE insur_all_property
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'subject_id'))then
    	ALTER TABLE insur_all_property
        	ADD subject_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'name'))then
    	ALTER TABLE insur_all_property
        	ADD name varchar(2000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'ndog'))then
    	ALTER TABLE insur_all_property
        	ADD ndog varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'ndogdat'))then
    	ALTER TABLE insur_all_property
        	ADD ndogdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'st1'))then
    	ALTER TABLE insur_all_property
        	ADD st1 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'st2'))then
    	ALTER TABLE insur_all_property
        	ADD st2 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'st3'))then
    	ALTER TABLE insur_all_property
        	ADD st3 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'ss1'))then
    	ALTER TABLE insur_all_property
        	ADD ss1 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'ss2'))then
    	ALTER TABLE insur_all_property
        	ADD ss2 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'ss3'))then
    	ALTER TABLE insur_all_property
        	ADD ss3 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'part'))then
    	ALTER TABLE insur_all_property
        	ADD part numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'part_city'))then
    	ALTER TABLE insur_all_property
        	ADD part_city numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'ras_pripay'))then
    	ALTER TABLE insur_all_property
        	ADD ras_pripay numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'link_id_file'))then
    	ALTER TABLE insur_all_property
        	ADD link_id_file bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'obj_id'))then
    	ALTER TABLE insur_all_property
        	ADD obj_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'org_type'))then
    	ALTER TABLE insur_all_property
        	ADD org_type varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'org_type_code'))then
    	ALTER TABLE insur_all_property
        	ADD org_type_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'paysign'))then
    	ALTER TABLE insur_all_property
        	ADD paysign varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'paysign_code'))then
    	ALTER TABLE insur_all_property
        	ADD paysign_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'status'))then
    	ALTER TABLE insur_all_property
        	ADD status varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'status_code'))then
    	ALTER TABLE insur_all_property
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_all_property', 'org_id_file'))then
    	ALTER TABLE insur_all_property
        	ADD org_id_file bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_damage_amount')) IS NULL)then
        CREATE TABLE insur_damage_amount
        (
            emp_id bigint NOT NULL,
		damage_id bigint,
		damage_assessment_method_id bigint,
		material_damage numeric,
		proportion_replacement_cost numeric,
		proportion_damaged_area numeric,
		damage_amount numeric,
		element_construction varchar(255),
		element_construction_code bigint,
		correction numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'emp_id'))then
    	ALTER TABLE insur_damage_amount
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'damage_id'))then
    	ALTER TABLE insur_damage_amount
        	ADD damage_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'damage_assessment_method_id'))then
    	ALTER TABLE insur_damage_amount
        	ADD damage_assessment_method_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'material_damage'))then
    	ALTER TABLE insur_damage_amount
        	ADD material_damage numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'proportion_replacement_cost'))then
    	ALTER TABLE insur_damage_amount
        	ADD proportion_replacement_cost numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'proportion_damaged_area'))then
    	ALTER TABLE insur_damage_amount
        	ADD proportion_damaged_area numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'damage_amount'))then
    	ALTER TABLE insur_damage_amount
        	ADD damage_amount numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'element_construction'))then
    	ALTER TABLE insur_damage_amount
        	ADD element_construction varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'element_construction_code'))then
    	ALTER TABLE insur_damage_amount
        	ADD element_construction_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_amount', 'correction'))then
    	ALTER TABLE insur_damage_amount
        	ADD correction numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('system_daily_stat_file_stor')) IS NULL)then
        CREATE TABLE system_daily_stat_file_stor
        (
            id bigint NOT NULL DEFAULT nextval('system_daily_stat_file_stor_seq'::regclass),
		stat_date timestamp without time zone,
		file_key varchar,
		description varchar,
		size_mb numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'id'))then
    	ALTER TABLE system_daily_stat_file_stor
        	ADD id bigint NOT NULL DEFAULT nextval('system_daily_stat_file_stor_seq'::regclass);
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'stat_date'))then
    	ALTER TABLE system_daily_stat_file_stor
        	ADD stat_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'file_key'))then
    	ALTER TABLE system_daily_stat_file_stor
        	ADD file_key varchar;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'description'))then
    	ALTER TABLE system_daily_stat_file_stor
        	ADD description varchar;
	end if;


	if(not core_updstru_checkexistcolumn('system_daily_stat_file_stor', 'size_mb'))then
    	ALTER TABLE system_daily_stat_file_stor
        	ADD size_mb numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_usersettingsreport')) IS NULL)then
        CREATE TABLE core_srd_usersettingsreport
        (
            id bigint NOT NULL,
		user_id bigint,
		report_id bigint,
		settings varchar(4000)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_usersettingsreport', 'id'))then
    	ALTER TABLE core_srd_usersettingsreport
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingsreport', 'user_id'))then
    	ALTER TABLE core_srd_usersettingsreport
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingsreport', 'report_id'))then
    	ALTER TABLE core_srd_usersettingsreport
        	ADD report_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettingsreport', 'settings'))then
    	ALTER TABLE core_srd_usersettingsreport
        	ADD settings varchar(4000);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_long_process_queue')) IS NULL)then
        CREATE TABLE core_long_process_queue
        (
            id bigint NOT NULL,
		user_id bigint,
		process_type_id bigint,
		object_register_id bigint,
		object_id bigint,
		create_date timestamp without time zone,
		start_date timestamp without time zone,
		end_date timestamp without time zone,
		status bigint NOT NULL,
		last_check_date timestamp without time zone,
		error_id bigint,
		message varchar(512),
		service_log_id bigint,
		log varchar,
		progress integer DEFAULT 0
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'id'))then
    	ALTER TABLE core_long_process_queue
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'user_id'))then
    	ALTER TABLE core_long_process_queue
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'process_type_id'))then
    	ALTER TABLE core_long_process_queue
        	ADD process_type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'object_register_id'))then
    	ALTER TABLE core_long_process_queue
        	ADD object_register_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'object_id'))then
    	ALTER TABLE core_long_process_queue
        	ADD object_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'create_date'))then
    	ALTER TABLE core_long_process_queue
        	ADD create_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'start_date'))then
    	ALTER TABLE core_long_process_queue
        	ADD start_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'end_date'))then
    	ALTER TABLE core_long_process_queue
        	ADD end_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'status'))then
    	ALTER TABLE core_long_process_queue
        	ADD status bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'last_check_date'))then
    	ALTER TABLE core_long_process_queue
        	ADD last_check_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'error_id'))then
    	ALTER TABLE core_long_process_queue
        	ADD error_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'message'))then
    	ALTER TABLE core_long_process_queue
        	ADD message varchar(512);
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'service_log_id'))then
    	ALTER TABLE core_long_process_queue
        	ADD service_log_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'log'))then
    	ALTER TABLE core_long_process_queue
        	ADD log varchar;
	end if;


	if(not core_updstru_checkexistcolumn('core_long_process_queue', 'progress'))then
    	ALTER TABLE core_long_process_queue
        	ADD progress integer DEFAULT 0;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_invoice')) IS NULL)then
        CREATE TABLE insur_invoice
        (
            emp_id bigint NOT NULL,
		subject_id bigint,
		subject_name varchar(255),
		phone varchar(255),
		data_input timestamp without time zone,
		inn varchar(255),
		kpp varchar(255),
		rach_acc varchar(255),
		num_card varchar(255),
		link_damage bigint,
		link_all_property bigint,
		link_fsp bigint,
		link_reestr_pay bigint,
		sum_opl numeric,
		status_name varchar,
		status_code bigint,
		user_id bigint,
		comment varchar,
		note_no_pay_id bigint,
		bank_id bigint,
		bank_name varchar(255),
		inn_bank varchar(255),
		kpp_bank varchar(255),
		bic_bank varchar(255),
		kor_acc varchar(255),
		date_agree timestamp without time zone,
		user_agree_id bigint,
		num_invoice varchar(255),
		date_invoice timestamp without time zone,
		contract_id bigint,
		reestr_contract_id bigint,
		part_dog numeric,
		svid_polyc_num varchar,
		svd_polyce_date timestamp without time zone,
		sort numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_invoice', 'emp_id'))then
    	ALTER TABLE insur_invoice
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'subject_id'))then
    	ALTER TABLE insur_invoice
        	ADD subject_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'subject_name'))then
    	ALTER TABLE insur_invoice
        	ADD subject_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'phone'))then
    	ALTER TABLE insur_invoice
        	ADD phone varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'data_input'))then
    	ALTER TABLE insur_invoice
        	ADD data_input timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'inn'))then
    	ALTER TABLE insur_invoice
        	ADD inn varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'kpp'))then
    	ALTER TABLE insur_invoice
        	ADD kpp varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'rach_acc'))then
    	ALTER TABLE insur_invoice
        	ADD rach_acc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'num_card'))then
    	ALTER TABLE insur_invoice
        	ADD num_card varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'link_damage'))then
    	ALTER TABLE insur_invoice
        	ADD link_damage bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'link_all_property'))then
    	ALTER TABLE insur_invoice
        	ADD link_all_property bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'link_fsp'))then
    	ALTER TABLE insur_invoice
        	ADD link_fsp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'link_reestr_pay'))then
    	ALTER TABLE insur_invoice
        	ADD link_reestr_pay bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'sum_opl'))then
    	ALTER TABLE insur_invoice
        	ADD sum_opl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'status_name'))then
    	ALTER TABLE insur_invoice
        	ADD status_name varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'status_code'))then
    	ALTER TABLE insur_invoice
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'user_id'))then
    	ALTER TABLE insur_invoice
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'comment'))then
    	ALTER TABLE insur_invoice
        	ADD comment varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'note_no_pay_id'))then
    	ALTER TABLE insur_invoice
        	ADD note_no_pay_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'bank_id'))then
    	ALTER TABLE insur_invoice
        	ADD bank_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'bank_name'))then
    	ALTER TABLE insur_invoice
        	ADD bank_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'inn_bank'))then
    	ALTER TABLE insur_invoice
        	ADD inn_bank varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'kpp_bank'))then
    	ALTER TABLE insur_invoice
        	ADD kpp_bank varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'bic_bank'))then
    	ALTER TABLE insur_invoice
        	ADD bic_bank varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'kor_acc'))then
    	ALTER TABLE insur_invoice
        	ADD kor_acc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'date_agree'))then
    	ALTER TABLE insur_invoice
        	ADD date_agree timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'user_agree_id'))then
    	ALTER TABLE insur_invoice
        	ADD user_agree_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'num_invoice'))then
    	ALTER TABLE insur_invoice
        	ADD num_invoice varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'date_invoice'))then
    	ALTER TABLE insur_invoice
        	ADD date_invoice timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'contract_id'))then
    	ALTER TABLE insur_invoice
        	ADD contract_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'reestr_contract_id'))then
    	ALTER TABLE insur_invoice
        	ADD reestr_contract_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'part_dog'))then
    	ALTER TABLE insur_invoice
        	ADD part_dog numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'svid_polyc_num'))then
    	ALTER TABLE insur_invoice
        	ADD svid_polyc_num varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'svd_polyce_date'))then
    	ALTER TABLE insur_invoice
        	ADD svd_polyce_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_invoice', 'sort'))then
    	ALTER TABLE insur_invoice
        	ADD sort numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_damage_assessment_method')) IS NULL)then
        CREATE TABLE insur_damage_assessment_method
        (
            id bigint NOT NULL,
		damage_symptom varchar(355) NOT NULL,
		material_damage varchar(100) NOT NULL,
		work_composition varchar(255) NOT NULL,
		element_construction_description varchar(255) NOT NULL,
		quantification varchar(255),
		element_construction varchar(255),
		element_construction_code bigint,
		material_damage_min bigint,
		material_damage_max bigint,
		ref_id bigint,
		ref_item_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'id'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'damage_symptom'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD damage_symptom varchar(355) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'material_damage'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD material_damage varchar(100) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'work_composition'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD work_composition varchar(255) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'element_construction_description'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD element_construction_description varchar(255) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'quantification'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD quantification varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'element_construction'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD element_construction varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'element_construction_code'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD element_construction_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'material_damage_min'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD material_damage_min bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'material_damage_max'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD material_damage_max bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'ref_id'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD ref_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage_assessment_method', 'ref_item_id'))then
    	ALTER TABLE insur_damage_assessment_method
        	ADD ref_item_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('bti_premase')) IS NULL)then
        CREATE TABLE bti_premase
        (
            emp_id bigint NOT NULL,
		dept_id bigint,
		kadastr varchar(64),
		floor_id bigint,
		inspection_date timestamp without time zone,
		unkv bigint,
		class_id bigint,
		class_name varchar(4000),
		type_id bigint,
		type_name varchar(4000),
		total_area numeric,
		height bigint,
		area_pp bigint,
		kvnom varchar(1000),
		section_number integer,
		living_area numeric,
		total_area_with_summer numeric,
		id_in_source bigint,
		rooms_count bigint,
		update_date timestamp without time zone,
		tet_code bigint,
		tet varchar(255),
		unom bigint,
		obj_type_code bigint,
		obj_type varchar(255),
		zpl numeric,
		bit0 integer
        );
    else
            
	if(not core_updstru_checkexistcolumn('bti_premase', 'emp_id'))then
    	ALTER TABLE bti_premase
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'dept_id'))then
    	ALTER TABLE bti_premase
        	ADD dept_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'kadastr'))then
    	ALTER TABLE bti_premase
        	ADD kadastr varchar(64);
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'floor_id'))then
    	ALTER TABLE bti_premase
        	ADD floor_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'inspection_date'))then
    	ALTER TABLE bti_premase
        	ADD inspection_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'unkv'))then
    	ALTER TABLE bti_premase
        	ADD unkv bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'class_id'))then
    	ALTER TABLE bti_premase
        	ADD class_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'class_name'))then
    	ALTER TABLE bti_premase
        	ADD class_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'type_id'))then
    	ALTER TABLE bti_premase
        	ADD type_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'type_name'))then
    	ALTER TABLE bti_premase
        	ADD type_name varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'total_area'))then
    	ALTER TABLE bti_premase
        	ADD total_area numeric;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'height'))then
    	ALTER TABLE bti_premase
        	ADD height bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'area_pp'))then
    	ALTER TABLE bti_premase
        	ADD area_pp bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'kvnom'))then
    	ALTER TABLE bti_premase
        	ADD kvnom varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'section_number'))then
    	ALTER TABLE bti_premase
        	ADD section_number integer;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'living_area'))then
    	ALTER TABLE bti_premase
        	ADD living_area numeric;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'total_area_with_summer'))then
    	ALTER TABLE bti_premase
        	ADD total_area_with_summer numeric;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'id_in_source'))then
    	ALTER TABLE bti_premase
        	ADD id_in_source bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'rooms_count'))then
    	ALTER TABLE bti_premase
        	ADD rooms_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'update_date'))then
    	ALTER TABLE bti_premase
        	ADD update_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'tet_code'))then
    	ALTER TABLE bti_premase
        	ADD tet_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'tet'))then
    	ALTER TABLE bti_premase
        	ADD tet varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'unom'))then
    	ALTER TABLE bti_premase
        	ADD unom bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'obj_type_code'))then
    	ALTER TABLE bti_premase
        	ADD obj_type_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'obj_type'))then
    	ALTER TABLE bti_premase
        	ADD obj_type varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'zpl'))then
    	ALTER TABLE bti_premase
        	ADD zpl numeric;
	end if;


	if(not core_updstru_checkexistcolumn('bti_premase', 'bit0'))then
    	ALTER TABLE bti_premase
        	ADD bit0 integer;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_messages_to')) IS NULL)then
        CREATE TABLE core_messages_to
        (
            id bigint NOT NULL,
		message_id bigint,
		user_id bigint,
		was_readed timestamp without time zone,
		was_deleted timestamp without time zone
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_messages_to', 'id'))then
    	ALTER TABLE core_messages_to
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages_to', 'message_id'))then
    	ALTER TABLE core_messages_to
        	ADD message_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages_to', 'user_id'))then
    	ALTER TABLE core_messages_to
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages_to', 'was_readed'))then
    	ALTER TABLE core_messages_to
        	ADD was_readed timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages_to', 'was_deleted'))then
    	ALTER TABLE core_messages_to
        	ADD was_deleted timestamp without time zone;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('mv_refresh_list')) IS NULL)then
        CREATE TABLE mv_refresh_list
        (
            id bigint,
		mv_name varchar(255)
        );
    else
            
	if(not core_updstru_checkexistcolumn('mv_refresh_list', 'id'))then
    	ALTER TABLE mv_refresh_list
        	ADD id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('mv_refresh_list', 'mv_name'))then
    	ALTER TABLE mv_refresh_list
        	ADD mv_name varchar(255);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('mv_refresh_log')) IS NULL)then
        CREATE TABLE mv_refresh_log
        (
            refresh_date timestamp without time zone,
		mv_name varchar(255),
		msg_event varchar(255),
		err_message text
        );
    else
            
	if(not core_updstru_checkexistcolumn('mv_refresh_log', 'refresh_date'))then
    	ALTER TABLE mv_refresh_log
        	ADD refresh_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('mv_refresh_log', 'mv_name'))then
    	ALTER TABLE mv_refresh_log
        	ADD mv_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('mv_refresh_log', 'msg_event'))then
    	ALTER TABLE mv_refresh_log
        	ADD msg_event varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('mv_refresh_log', 'err_message'))then
    	ALTER TABLE mv_refresh_log
        	ADD err_message text;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_usersettings')) IS NULL)then
        CREATE TABLE core_srd_usersettings
        (
            userid bigint NOT NULL,
		settings varchar(4000),
		default_layout_settings varchar(4000)
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_usersettings', 'userid'))then
    	ALTER TABLE core_srd_usersettings
        	ADD userid bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettings', 'settings'))then
    	ALTER TABLE core_srd_usersettings
        	ADD settings varchar(4000);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_usersettings', 'default_layout_settings'))then
    	ALTER TABLE core_srd_usersettings
        	ADD default_layout_settings varchar(4000);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_address')) IS NULL)then
        CREATE TABLE insur_address
        (
            emp_id bigint NOT NULL,
		full_address varchar(1000),
		guid_fias_house varchar(255),
		guid_fias_street varchar(255),
		type_city varchar(25),
		city varchar(255),
		type_urban_territory varchar(55),
		urban_territory varchar(255),
		type_district varchar(55),
		district varchar(255),
		type_street varchar(25),
		street varchar(255),
		type_house varchar(25),
		house varchar(255),
		type_corpus varchar(25),
		corpus varchar(255),
		type_structure varchar(25),
		structure varchar(255),
		guid_region varchar(255),
		type_region varchar(55),
		region varchar(255),
		type_avtonomnyy_okrug varchar(55),
		avtonomnyy_okrug varchar(255),
		type_locality varchar(55),
		locality varchar(255),
		postal_code varchar(10),
		source_address varchar(10),
		source_address_code bigint,
		short_address varchar(1000)
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_address', 'emp_id'))then
    	ALTER TABLE insur_address
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'full_address'))then
    	ALTER TABLE insur_address
        	ADD full_address varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'guid_fias_house'))then
    	ALTER TABLE insur_address
        	ADD guid_fias_house varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'guid_fias_street'))then
    	ALTER TABLE insur_address
        	ADD guid_fias_street varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_city'))then
    	ALTER TABLE insur_address
        	ADD type_city varchar(25);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'city'))then
    	ALTER TABLE insur_address
        	ADD city varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_urban_territory'))then
    	ALTER TABLE insur_address
        	ADD type_urban_territory varchar(55);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'urban_territory'))then
    	ALTER TABLE insur_address
        	ADD urban_territory varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_district'))then
    	ALTER TABLE insur_address
        	ADD type_district varchar(55);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'district'))then
    	ALTER TABLE insur_address
        	ADD district varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_street'))then
    	ALTER TABLE insur_address
        	ADD type_street varchar(25);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'street'))then
    	ALTER TABLE insur_address
        	ADD street varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_house'))then
    	ALTER TABLE insur_address
        	ADD type_house varchar(25);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'house'))then
    	ALTER TABLE insur_address
        	ADD house varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_corpus'))then
    	ALTER TABLE insur_address
        	ADD type_corpus varchar(25);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'corpus'))then
    	ALTER TABLE insur_address
        	ADD corpus varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_structure'))then
    	ALTER TABLE insur_address
        	ADD type_structure varchar(25);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'structure'))then
    	ALTER TABLE insur_address
        	ADD structure varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'guid_region'))then
    	ALTER TABLE insur_address
        	ADD guid_region varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_region'))then
    	ALTER TABLE insur_address
        	ADD type_region varchar(55);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'region'))then
    	ALTER TABLE insur_address
        	ADD region varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_avtonomnyy_okrug'))then
    	ALTER TABLE insur_address
        	ADD type_avtonomnyy_okrug varchar(55);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'avtonomnyy_okrug'))then
    	ALTER TABLE insur_address
        	ADD avtonomnyy_okrug varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'type_locality'))then
    	ALTER TABLE insur_address
        	ADD type_locality varchar(55);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'locality'))then
    	ALTER TABLE insur_address
        	ADD locality varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'postal_code'))then
    	ALTER TABLE insur_address
        	ADD postal_code varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'source_address'))then
    	ALTER TABLE insur_address
        	ADD source_address varchar(10);
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'source_address_code'))then
    	ALTER TABLE insur_address
        	ADD source_address_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_address', 'short_address'))then
    	ALTER TABLE insur_address
        	ADD short_address varchar(1000);
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('tmp_all_prop_psign_code')) IS NULL)then
        CREATE TABLE tmp_all_prop_psign_code
        (
            ndog varchar(255),
		ndogdat timestamp without time zone,
		paysign_code bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('tmp_all_prop_psign_code', 'ndog'))then
    	ALTER TABLE tmp_all_prop_psign_code
        	ADD ndog varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('tmp_all_prop_psign_code', 'ndogdat'))then
    	ALTER TABLE tmp_all_prop_psign_code
        	ADD ndogdat timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('tmp_all_prop_psign_code', 'paysign_code'))then
    	ALTER TABLE tmp_all_prop_psign_code
        	ADD paysign_code bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_param_calculation')) IS NULL)then
        CREATE TABLE insur_param_calculation
        (
            emp_id bigint NOT NULL,
		obj_id bigint,
		contract_id bigint,
		insurance_id bigint,
		part_СОmpensation numeric,
		actual_cost numeric,
		coef_actual_cost numeric,
		actual_cost_current numeric,
		indicator_r numeric,
		calculated_area numeric,
		ui_1 numeric,
		ui_2 numeric,
		ui_3 numeric,
		ui_4 numeric,
		ui_5 numeric,
		ui_6 numeric,
		ui_7 numeric,
		ui_8 numeric,
		ui_9 numeric,
		ui_10 numeric,
		ui_11 numeric,
		total_cost_1 numeric,
		design_cost_1 numeric,
		basic_rate_1 numeric,
		annual_bonus_1 numeric,
		total_cost_2 numeric,
		design_cost_2 numeric,
		basic_rate_2 numeric,
		annual_bonus_2 numeric,
		total_cost_3 numeric,
		design_cost_3 numeric,
		basic_rate_3 numeric,
		annual_bonus_3 numeric,
		size_annual_bonus numeric,
		request_number varchar(20),
		request_date timestamp without time zone,
		note varchar(1000),
		created_date timestamp without time zone,
		approval_date timestamp without time zone,
		created_user_id bigint,
		approval_user_id bigint,
		status varchar(255),
		deleted smallint DEFAULT 0,
		status_code bigint,
		subject_id bigint,
		date_fill_1 timestamp without time zone,
		date_fill_2 timestamp without time zone,
		agreement_id_1 bigint,
		agreement_id_2 bigint,
		package_num varchar(50),
		flag_okrugl smallint,
		all_area numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'emp_id'))then
    	ALTER TABLE insur_param_calculation
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'obj_id'))then
    	ALTER TABLE insur_param_calculation
        	ADD obj_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'contract_id'))then
    	ALTER TABLE insur_param_calculation
        	ADD contract_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'insurance_id'))then
    	ALTER TABLE insur_param_calculation
        	ADD insurance_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'part_СОmpensation'))then
    	ALTER TABLE insur_param_calculation
        	ADD part_СОmpensation numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'actual_cost'))then
    	ALTER TABLE insur_param_calculation
        	ADD actual_cost numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'coef_actual_cost'))then
    	ALTER TABLE insur_param_calculation
        	ADD coef_actual_cost numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'actual_cost_current'))then
    	ALTER TABLE insur_param_calculation
        	ADD actual_cost_current numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'indicator_r'))then
    	ALTER TABLE insur_param_calculation
        	ADD indicator_r numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'calculated_area'))then
    	ALTER TABLE insur_param_calculation
        	ADD calculated_area numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_1'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_1 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_2'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_2 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_3'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_3 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_4'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_4 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_5'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_5 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_6'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_6 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_7'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_7 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_8'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_8 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_9'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_9 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_10'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_10 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'ui_11'))then
    	ALTER TABLE insur_param_calculation
        	ADD ui_11 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'total_cost_1'))then
    	ALTER TABLE insur_param_calculation
        	ADD total_cost_1 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'design_cost_1'))then
    	ALTER TABLE insur_param_calculation
        	ADD design_cost_1 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'basic_rate_1'))then
    	ALTER TABLE insur_param_calculation
        	ADD basic_rate_1 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'annual_bonus_1'))then
    	ALTER TABLE insur_param_calculation
        	ADD annual_bonus_1 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'total_cost_2'))then
    	ALTER TABLE insur_param_calculation
        	ADD total_cost_2 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'design_cost_2'))then
    	ALTER TABLE insur_param_calculation
        	ADD design_cost_2 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'basic_rate_2'))then
    	ALTER TABLE insur_param_calculation
        	ADD basic_rate_2 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'annual_bonus_2'))then
    	ALTER TABLE insur_param_calculation
        	ADD annual_bonus_2 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'total_cost_3'))then
    	ALTER TABLE insur_param_calculation
        	ADD total_cost_3 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'design_cost_3'))then
    	ALTER TABLE insur_param_calculation
        	ADD design_cost_3 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'basic_rate_3'))then
    	ALTER TABLE insur_param_calculation
        	ADD basic_rate_3 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'annual_bonus_3'))then
    	ALTER TABLE insur_param_calculation
        	ADD annual_bonus_3 numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'size_annual_bonus'))then
    	ALTER TABLE insur_param_calculation
        	ADD size_annual_bonus numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'request_number'))then
    	ALTER TABLE insur_param_calculation
        	ADD request_number varchar(20);
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'request_date'))then
    	ALTER TABLE insur_param_calculation
        	ADD request_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'note'))then
    	ALTER TABLE insur_param_calculation
        	ADD note varchar(1000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'created_date'))then
    	ALTER TABLE insur_param_calculation
        	ADD created_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'approval_date'))then
    	ALTER TABLE insur_param_calculation
        	ADD approval_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'created_user_id'))then
    	ALTER TABLE insur_param_calculation
        	ADD created_user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'approval_user_id'))then
    	ALTER TABLE insur_param_calculation
        	ADD approval_user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'status'))then
    	ALTER TABLE insur_param_calculation
        	ADD status varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'deleted'))then
    	ALTER TABLE insur_param_calculation
        	ADD deleted smallint DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'status_code'))then
    	ALTER TABLE insur_param_calculation
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'subject_id'))then
    	ALTER TABLE insur_param_calculation
        	ADD subject_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'date_fill_1'))then
    	ALTER TABLE insur_param_calculation
        	ADD date_fill_1 timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'date_fill_2'))then
    	ALTER TABLE insur_param_calculation
        	ADD date_fill_2 timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'agreement_id_1'))then
    	ALTER TABLE insur_param_calculation
        	ADD agreement_id_1 bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'agreement_id_2'))then
    	ALTER TABLE insur_param_calculation
        	ADD agreement_id_2 bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'package_num'))then
    	ALTER TABLE insur_param_calculation
        	ADD package_num varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'flag_okrugl'))then
    	ALTER TABLE insur_param_calculation
        	ADD flag_okrugl smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_param_calculation', 'all_area'))then
    	ALTER TABLE insur_param_calculation
        	ADD all_area numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_srd_user')) IS NULL)then
        CREATE TABLE core_srd_user
        (
            id bigint NOT NULL,
		department_id bigint NOT NULL,
		username varchar(68) NOT NULL,
		fullname varchar(100) NOT NULL,
		name varchar(100),
		surname varchar(100),
		patronymic varchar(100),
		fullname_for_doc varchar(100),
		position varchar(250),
		is_deleted smallint NOT NULL DEFAULT 0,
		change_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
		password_md5 varchar(32),
		email varchar(100),
		phone varchar(100),
		external_id bigint,
		isdomainuser smallint DEFAULT 0
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_srd_user', 'id'))then
    	ALTER TABLE core_srd_user
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'department_id'))then
    	ALTER TABLE core_srd_user
        	ADD department_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'username'))then
    	ALTER TABLE core_srd_user
        	ADD username varchar(68) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'fullname'))then
    	ALTER TABLE core_srd_user
        	ADD fullname varchar(100) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'name'))then
    	ALTER TABLE core_srd_user
        	ADD name varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'surname'))then
    	ALTER TABLE core_srd_user
        	ADD surname varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'patronymic'))then
    	ALTER TABLE core_srd_user
        	ADD patronymic varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'fullname_for_doc'))then
    	ALTER TABLE core_srd_user
        	ADD fullname_for_doc varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'position'))then
    	ALTER TABLE core_srd_user
        	ADD position varchar(250);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'is_deleted'))then
    	ALTER TABLE core_srd_user
        	ADD is_deleted smallint NOT NULL DEFAULT 0;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'change_date'))then
    	ALTER TABLE core_srd_user
        	ADD change_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'password_md5'))then
    	ALTER TABLE core_srd_user
        	ADD password_md5 varchar(32);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'email'))then
    	ALTER TABLE core_srd_user
        	ADD email varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'phone'))then
    	ALTER TABLE core_srd_user
        	ADD phone varchar(100);
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'external_id'))then
    	ALTER TABLE core_srd_user
        	ADD external_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('core_srd_user', 'isdomainuser'))then
    	ALTER TABLE core_srd_user
        	ADD isdomainuser smallint DEFAULT 0;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_living_premise_insur_cost')) IS NULL)then
        CREATE TABLE insur_living_premise_insur_cost
        (
            id bigint NOT NULL,
		date_begin timestamp without time zone,
		condition varchar(255),
		value numeric(10, 4),
		strah_tarif numeric,
		strah_bonus numeric
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'id'))then
    	ALTER TABLE insur_living_premise_insur_cost
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'date_begin'))then
    	ALTER TABLE insur_living_premise_insur_cost
        	ADD date_begin timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'condition'))then
    	ALTER TABLE insur_living_premise_insur_cost
        	ADD condition varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'value'))then
    	ALTER TABLE insur_living_premise_insur_cost
        	ADD value numeric(10, 4);
	end if;


	if(not core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'strah_tarif'))then
    	ALTER TABLE insur_living_premise_insur_cost
        	ADD strah_tarif numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_living_premise_insur_cost', 'strah_bonus'))then
    	ALTER TABLE insur_living_premise_insur_cost
        	ADD strah_bonus numeric;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_input_file')) IS NULL)then
        CREATE TABLE insur_input_file
        (
            emp_id bigint NOT NULL,
		file_name varchar(255),
		type_file_code bigint,
		period_reg_date timestamp without time zone,
		district_id bigint,
		type_source varchar(255),
		date_input timestamp without time zone,
		count_str bigint,
		status varchar(255),
		type_file varchar(255),
		type_source_code bigint,
		status_code bigint,
		link_package bigint,
		user_id bigint,
		sum_all numeric,
		file_storage_id bigint,
		criteria_set smallint,
		kod_post bigint,
		parent_id bigint,
		count_str_load bigint,
		log_file_id bigint,
		insurance_organization_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_input_file', 'emp_id'))then
    	ALTER TABLE insur_input_file
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'file_name'))then
    	ALTER TABLE insur_input_file
        	ADD file_name varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'type_file_code'))then
    	ALTER TABLE insur_input_file
        	ADD type_file_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'period_reg_date'))then
    	ALTER TABLE insur_input_file
        	ADD period_reg_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'district_id'))then
    	ALTER TABLE insur_input_file
        	ADD district_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'type_source'))then
    	ALTER TABLE insur_input_file
        	ADD type_source varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'date_input'))then
    	ALTER TABLE insur_input_file
        	ADD date_input timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'count_str'))then
    	ALTER TABLE insur_input_file
        	ADD count_str bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'status'))then
    	ALTER TABLE insur_input_file
        	ADD status varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'type_file'))then
    	ALTER TABLE insur_input_file
        	ADD type_file varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'type_source_code'))then
    	ALTER TABLE insur_input_file
        	ADD type_source_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'status_code'))then
    	ALTER TABLE insur_input_file
        	ADD status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'link_package'))then
    	ALTER TABLE insur_input_file
        	ADD link_package bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'user_id'))then
    	ALTER TABLE insur_input_file
        	ADD user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'sum_all'))then
    	ALTER TABLE insur_input_file
        	ADD sum_all numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'file_storage_id'))then
    	ALTER TABLE insur_input_file
        	ADD file_storage_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'criteria_set'))then
    	ALTER TABLE insur_input_file
        	ADD criteria_set smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'kod_post'))then
    	ALTER TABLE insur_input_file
        	ADD kod_post bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'parent_id'))then
    	ALTER TABLE insur_input_file
        	ADD parent_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'count_str_load'))then
    	ALTER TABLE insur_input_file
        	ADD count_str_load bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'log_file_id'))then
    	ALTER TABLE insur_input_file
        	ADD log_file_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_input_file', 'insurance_organization_id'))then
    	ALTER TABLE insur_input_file
        	ADD insurance_organization_id bigint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('core_messages')) IS NULL)then
        CREATE TABLE core_messages
        (
            id bigint NOT NULL,
		user_id bigint NOT NULL,
		subject varchar(250) NOT NULL,
		message varchar(4000) NOT NULL,
		was_sended timestamp without time zone NOT NULL,
		field_to varchar
        );
    else
            
	if(not core_updstru_checkexistcolumn('core_messages', 'id'))then
    	ALTER TABLE core_messages
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages', 'user_id'))then
    	ALTER TABLE core_messages
        	ADD user_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages', 'subject'))then
    	ALTER TABLE core_messages
        	ADD subject varchar(250) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages', 'message'))then
    	ALTER TABLE core_messages
        	ADD message varchar(4000) NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages', 'was_sended'))then
    	ALTER TABLE core_messages
        	ADD was_sended timestamp without time zone NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('core_messages', 'field_to'))then
    	ALTER TABLE core_messages
        	ADD field_to varchar;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('insur_damage')) IS NULL)then
        CREATE TABLE insur_damage
        (
            emp_id bigint NOT NULL,
		obj_id bigint,
		type_doc varchar(255),
		type_doc_code bigint,
		insur_org_id bigint,
		insur_date timestamp without time zone,
		insur_nom varchar(255),
		damage_data timestamp without time zone,
		damage_reason_gp varchar(2000),
		damage_reason_gp_code bigint,
		damage_reason_oi varchar(255),
		damage_reason_oi_code bigint,
		estimated_value numeric,
		insur_sum numeric,
		part_insur numeric,
		type_build varchar(255),
		type_build_code bigint,
		type_cooker varchar(255),
		type_cooker_code bigint,
		type_floor_code bigint,
		type_floor varchar(255),
		sum_damage numeric,
		subsidy numeric,
		agreement_id_1 bigint,
		agreement_id_2 bigint,
		date_input timestamp without time zone,
		date_fill_1 timestamp without time zone,
		date_fill_2 timestamp without time zone,
		main_agreement_id bigint,
		date_fill_main timestamp without time zone,
		signature_id bigint,
		date_fill_signature timestamp without time zone,
		damage_amount_status varchar(255),
		damage_amount_status_code bigint,
		obj_reestr_id bigint,
		nom_doc varchar(255),
		nom_date timestamp without time zone,
		part_town numeric(10, 4),
		calcul_damage numeric,
		subreason_damage_reason varchar(255),
		subreason_damage_reason_code bigint,
		refinement_subreason varchar(255),
		refinement_subreason_code bigint,
		strah_plat numeric(10, 4),
		date_input_gby timestamp without time zone,
		date_doc_last_gby timestamp without time zone,
		date_doc_add_gby timestamp without time zone,
		note varchar(255),
		date_control timestamp without time zone,
		control_user_id bigint,
		franchise numeric,
		flag_slygebka smallint,
		additional_data varchar,
		sum_damage_base numeric,
		calc_note varchar,
		estimated_value_different smallint
        );
    else
            
	if(not core_updstru_checkexistcolumn('insur_damage', 'emp_id'))then
    	ALTER TABLE insur_damage
        	ADD emp_id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'obj_id'))then
    	ALTER TABLE insur_damage
        	ADD obj_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'type_doc'))then
    	ALTER TABLE insur_damage
        	ADD type_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'type_doc_code'))then
    	ALTER TABLE insur_damage
        	ADD type_doc_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'insur_org_id'))then
    	ALTER TABLE insur_damage
        	ADD insur_org_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'insur_date'))then
    	ALTER TABLE insur_damage
        	ADD insur_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'insur_nom'))then
    	ALTER TABLE insur_damage
        	ADD insur_nom varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'damage_data'))then
    	ALTER TABLE insur_damage
        	ADD damage_data timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'damage_reason_gp'))then
    	ALTER TABLE insur_damage
        	ADD damage_reason_gp varchar(2000);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'damage_reason_gp_code'))then
    	ALTER TABLE insur_damage
        	ADD damage_reason_gp_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'damage_reason_oi'))then
    	ALTER TABLE insur_damage
        	ADD damage_reason_oi varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'damage_reason_oi_code'))then
    	ALTER TABLE insur_damage
        	ADD damage_reason_oi_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'estimated_value'))then
    	ALTER TABLE insur_damage
        	ADD estimated_value numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'insur_sum'))then
    	ALTER TABLE insur_damage
        	ADD insur_sum numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'part_insur'))then
    	ALTER TABLE insur_damage
        	ADD part_insur numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'type_build'))then
    	ALTER TABLE insur_damage
        	ADD type_build varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'type_build_code'))then
    	ALTER TABLE insur_damage
        	ADD type_build_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'type_cooker'))then
    	ALTER TABLE insur_damage
        	ADD type_cooker varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'type_cooker_code'))then
    	ALTER TABLE insur_damage
        	ADD type_cooker_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'type_floor_code'))then
    	ALTER TABLE insur_damage
        	ADD type_floor_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'type_floor'))then
    	ALTER TABLE insur_damage
        	ADD type_floor varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'sum_damage'))then
    	ALTER TABLE insur_damage
        	ADD sum_damage numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'subsidy'))then
    	ALTER TABLE insur_damage
        	ADD subsidy numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'agreement_id_1'))then
    	ALTER TABLE insur_damage
        	ADD agreement_id_1 bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'agreement_id_2'))then
    	ALTER TABLE insur_damage
        	ADD agreement_id_2 bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_input'))then
    	ALTER TABLE insur_damage
        	ADD date_input timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_fill_1'))then
    	ALTER TABLE insur_damage
        	ADD date_fill_1 timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_fill_2'))then
    	ALTER TABLE insur_damage
        	ADD date_fill_2 timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'main_agreement_id'))then
    	ALTER TABLE insur_damage
        	ADD main_agreement_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_fill_main'))then
    	ALTER TABLE insur_damage
        	ADD date_fill_main timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'signature_id'))then
    	ALTER TABLE insur_damage
        	ADD signature_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_fill_signature'))then
    	ALTER TABLE insur_damage
        	ADD date_fill_signature timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'damage_amount_status'))then
    	ALTER TABLE insur_damage
        	ADD damage_amount_status varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'damage_amount_status_code'))then
    	ALTER TABLE insur_damage
        	ADD damage_amount_status_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'obj_reestr_id'))then
    	ALTER TABLE insur_damage
        	ADD obj_reestr_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'nom_doc'))then
    	ALTER TABLE insur_damage
        	ADD nom_doc varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'nom_date'))then
    	ALTER TABLE insur_damage
        	ADD nom_date timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'part_town'))then
    	ALTER TABLE insur_damage
        	ADD part_town numeric(10, 4);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'calcul_damage'))then
    	ALTER TABLE insur_damage
        	ADD calcul_damage numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'subreason_damage_reason'))then
    	ALTER TABLE insur_damage
        	ADD subreason_damage_reason varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'subreason_damage_reason_code'))then
    	ALTER TABLE insur_damage
        	ADD subreason_damage_reason_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'refinement_subreason'))then
    	ALTER TABLE insur_damage
        	ADD refinement_subreason varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'refinement_subreason_code'))then
    	ALTER TABLE insur_damage
        	ADD refinement_subreason_code bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'strah_plat'))then
    	ALTER TABLE insur_damage
        	ADD strah_plat numeric(10, 4);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_input_gby'))then
    	ALTER TABLE insur_damage
        	ADD date_input_gby timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_doc_last_gby'))then
    	ALTER TABLE insur_damage
        	ADD date_doc_last_gby timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_doc_add_gby'))then
    	ALTER TABLE insur_damage
        	ADD date_doc_add_gby timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'note'))then
    	ALTER TABLE insur_damage
        	ADD note varchar(255);
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'date_control'))then
    	ALTER TABLE insur_damage
        	ADD date_control timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'control_user_id'))then
    	ALTER TABLE insur_damage
        	ADD control_user_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'franchise'))then
    	ALTER TABLE insur_damage
        	ADD franchise numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'flag_slygebka'))then
    	ALTER TABLE insur_damage
        	ADD flag_slygebka smallint;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'additional_data'))then
    	ALTER TABLE insur_damage
        	ADD additional_data varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'sum_damage_base'))then
    	ALTER TABLE insur_damage
        	ADD sum_damage_base numeric;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'calc_note'))then
    	ALTER TABLE insur_damage
        	ADD calc_note varchar;
	end if;


	if(not core_updstru_checkexistcolumn('insur_damage', 'estimated_value_different'))then
    	ALTER TABLE insur_damage
        	ADD estimated_value_different smallint;
	end if;

    end if;
END $$;
--<DO>--
DO $$
BEGIN
    if((SELECT to_regclass('import_log_insur_flat')) IS NULL)then
        CREATE TABLE import_log_insur_flat
        (
            id bigint NOT NULL,
		ehd_parcel_id bigint,
		bti_flat_id bigint,
		insur_flat_id bigint,
		date_loaded timestamp without time zone,
		error_message varchar,
		error_id bigint,
		is_error integer,
		update_date_ehd timestamp without time zone,
		update_date_bti timestamp without time zone,
		cad_num varchar(50),
		kvnom varchar(50),
		error_attempts_count bigint,
		insur_building_id bigint
        );
    else
            
	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'id'))then
    	ALTER TABLE import_log_insur_flat
        	ADD id bigint NOT NULL;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'ehd_parcel_id'))then
    	ALTER TABLE import_log_insur_flat
        	ADD ehd_parcel_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'bti_flat_id'))then
    	ALTER TABLE import_log_insur_flat
        	ADD bti_flat_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'insur_flat_id'))then
    	ALTER TABLE import_log_insur_flat
        	ADD insur_flat_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'date_loaded'))then
    	ALTER TABLE import_log_insur_flat
        	ADD date_loaded timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'error_message'))then
    	ALTER TABLE import_log_insur_flat
        	ADD error_message varchar;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'error_id'))then
    	ALTER TABLE import_log_insur_flat
        	ADD error_id bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'is_error'))then
    	ALTER TABLE import_log_insur_flat
        	ADD is_error integer;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'update_date_ehd'))then
    	ALTER TABLE import_log_insur_flat
        	ADD update_date_ehd timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'update_date_bti'))then
    	ALTER TABLE import_log_insur_flat
        	ADD update_date_bti timestamp without time zone;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'cad_num'))then
    	ALTER TABLE import_log_insur_flat
        	ADD cad_num varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'kvnom'))then
    	ALTER TABLE import_log_insur_flat
        	ADD kvnom varchar(50);
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'error_attempts_count'))then
    	ALTER TABLE import_log_insur_flat
        	ADD error_attempts_count bigint;
	end if;


	if(not core_updstru_checkexistcolumn('import_log_insur_flat', 'insur_building_id'))then
    	ALTER TABLE import_log_insur_flat
        	ADD insur_building_id bigint;
	end if;

    end if;
END $$;