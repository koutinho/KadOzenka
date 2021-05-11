INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (10008300, 'Показатель тест', 100, 1, null, 303, null, null, null, 0, null, null, null, 1, null, null, null, null, 2, '2020-03-24 13:00:51.000000', 0);

INSERT INTO public.core_register_relation (id, name, parentregister, chieldregister, cardinality, kindid, parentregister_attribute_id, qscondition) 
VALUES (103, 'От объекта аналога к объекту недвижимости', 200, 100, null, 10005400, 20000200, null);

INSERT INTO core_register_relation (id, name, parentregister, chieldregister, cardinality, kindid, parentregister_attribute_id, qscondition) 
VALUES (218, 'От единицы кадастровой оценки к справочнику кадастровых кварталов', 107, 201, null, 20101700, 10700200, null);

--Таблицы экспресс-оценки
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) 
VALUES (608, 'ES.EsToMarketCoreObject', 'Связь экспресс оценки и объектов аналогов', null, null, 'ES_TO_MARKET_CORE_OBJECT', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, 0, null, null);

INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) 
VALUES (609, 'ES.EsReference', 'Экспресс оценка. Справочники', null, null, 'ES_REFERENCE', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, null, null);

INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) 
VALUES (610, 'ES.EsReferenceItem', 'Экспресс оценка. Значения справочников', null, null, 'ES_REFERENCE_ITEM', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, 0, null, null);

INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) 
VALUES (611, 'Es.SettingsParams', 'Экспресс оценка. Настройка параметров', null, null, 'ES_SETTINGS_PARAMS', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, 0, null, null);


--Столбцы таблиц экспресс-оценки
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (60800100, 'Идентификатор', 608, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (60800200, 'Идентификатор экспресс оценки', 608, 1, null, null, 'ES_ID', null, null, 0, null, null, 'EsId', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (60800300, 'Идентификатор объктов аналогов', 608, 1, null, null, 'MARKET_OBJECT_ID', null, null, 0, null, null, 'MarketObjectId', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (60900100, 'Идентификатор', 609, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (60900200, 'Наименование справочника', 609, 4, null, null, 'NAME', null, null, null, 1, null, 'Name', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (60900300, 'Тип значения', 609, 4, null, 600, 'VALUE_TYPE', 'VALUE_TYPE_CODE', null, null, null, null, 'ValueType', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (60900400, 'Признак интервального справочника', 609, 3, null, null, 'USE_INTERVAL', null, null, null, null, null, 'UseInterval', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61000100, 'Идентификатор', 610, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61000200, 'Идентификатор справочника', 610, 1, null, null, 'ES_REFERENCE_ID', null, null, null, null, null, 'ReferenceId', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61000300, 'Значение', 610, 4, null, null, 'VALUE', null, null, null, 1, null, 'Value', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61000500, 'Значение для расчета', 610, 2, null, null, 'CALCULATION_VALUE', null, null, 0, null, null, 'CalculationValue', 1, null, null, null, 0, 2, '2020-10-08 12:30:57.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61000600, 'Общее значение', 610, 4, null, null, 'COMMON_VALUE', null, null, null, null, null, 'CommonValue', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61000700, 'Значение от', 610, 4, null, null, 'VALUE_FROM', null, null, null, null, null, 'ValueFrom', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61000800, 'Значение до', 610, 4, null, null, 'VALUE_TO', null, null, null, null, null, 'ValueTo', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61100100, 'Идентификатор', 611, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61100200, 'Идентификатор тура', 611, 1, null, null, 'ID_TOUR', null, null, 0, null, null, 'TourId', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61100300, 'Идентификатор реестра атрибутов КО', 611, 1, null, null, 'ID_REGISTER', null, null, 0, null, null, 'Registerid', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61100400, 'Оценочные факторы ', 611, 4, null, null, 'COST_FACTORS', null, null, 0, null, null, 'CostFacrors', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61100500, 'Тип сегмента', 611, 4, null, 114, null, 'SEGMENT_TYPE', null, 0, null, null, 'SegmentType', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (61100600, 'Идентификатор атрибута кадастрового номера строения', 611, 1, null, null, 'BUILD_CAD_NUMBER', null, null, 0, null, null, 'BuildCadNumber', 1, null, null, null, 0, null, null, 0);



CREATE TABLE public.es_to_market_core_object (
  id INTEGER NOT NULL,
  es_id INTEGER NOT NULL,
  market_object_id INTEGER NOT NULL,
  CONSTRAINT es_to_market_core_object_pkey PRIMARY KEY(id)
) 
WITH (oids = false);

ALTER TABLE public.es_to_market_core_object
  OWNER TO cipjs_kad_ozenka;




  CREATE TABLE public.es_reference (
  id BIGINT NOT NULL,
  name VARCHAR(255),
  value_type VARCHAR(255) NOT NULL,
  value_type_code BIGINT NOT NULL,
  use_interval SMALLINT,
  change_user_id BIGINT,
  change_date TIMESTAMP WITHOUT TIME ZONE,
  CONSTRAINT reg_609_q_pk PRIMARY KEY(id)
) 
WITH (oids = false);

COMMENT ON TABLE public.es_reference
IS 'Экспресс оценка. Справочники 
 609 реестр';

COMMENT ON COLUMN public.es_reference.use_interval
IS 'Признак что справочник интервальный';

ALTER TABLE public.es_reference
  OWNER TO cipjs_kad_ozenka;





CREATE TABLE public.es_reference_item (
  id BIGINT NOT NULL,
  es_reference_id BIGINT NOT NULL,
  value VARCHAR(255),
  calculation_value NUMERIC,
  common_value VARCHAR(255),
  value_from VARCHAR(255),
  value_to VARCHAR(255),
  CONSTRAINT reg_610_q_pk PRIMARY KEY(id)
) 
WITH (oids = false);

COMMENT ON TABLE public.es_reference_item
IS 'Экспресс оценка. Значения справочников';

COMMENT ON COLUMN public.es_reference_item.common_value
IS 'Общее значение, т.е например материал стен Бетон армированный то общее значение это Бетон';

COMMENT ON COLUMN public.es_reference_item.value_from
IS 'Значение от на случай если используется интервальный тип справочника';

COMMENT ON COLUMN public.es_reference_item.value_to
IS 'Значение до, если используется интервальный тип справочника';

ALTER TABLE public.es_reference_item
  OWNER TO cipjs_kad_ozenka;




CREATE TABLE public.es_settings_params (
  id INTEGER NOT NULL,
  id_tour INTEGER NOT NULL,
  id_register INTEGER NOT NULL,
  cost_factors VARCHAR,
  segment_type INTEGER NOT NULL,
  build_cad_number INTEGER,
  CONSTRAINT es_settings_params_pkey PRIMARY KEY(id)
) 
WITH (oids = false);

ALTER TABLE public.es_settings_params
  ALTER COLUMN id_tour SET STATISTICS 0;

COMMENT ON COLUMN public.es_settings_params.build_cad_number
IS 'Ид атрибута кадастрового номера';

ALTER TABLE public.es_settings_params
  OWNER TO cipjs_kad_ozenka;



 --Добавляем дашборд экспресс-оценки на стартовую страницу
INSERT INTO dashboards_panel (id, dashboard_id, title, column_index, order_in_column, panel_type_id, settings) 
VALUES (1000041, 1000007, 'Карточка значений показателей', 0, 7, 6, '<PanelIndexCardDto><BackgroundColor>#940889</BackgroundColor><TextColor>#ffffff</TextColor><IconClass>fas fa-users</IconClass><FontSizeTitle>18px</FontSizeTitle><FontSize>16px</FontSize><LinkUrl>{LinkParam}</LinkUrl><SourceType>Method</SourceType><LoadType>Sync</LoadType><DisplayBlockAll>No</DisplayBlockAll><SQL></SQL><Type>KadOzenka.Dal.Gadgets.GadgetService, KadOzenka.Dal</Type><Method>ExpressEvaluation</Method><WindowWidthStyle>100%</WindowWidthStyle><CacheSeconds></CacheSeconds><TitleUrl>/Dashboard?Subsystem=Es</TitleUrl><TitleIsUrl>Yes</TitleIsUrl><ContentIsUrl>Yes</ContentIsUrl><Id>1000041</Id><Title>Экспресс оценка</Title><WindowWidth></WindowWidth><WindowHeight></WindowHeight><SrdFunctionTag>EXPRESSSCORE</SrdFunctionTag></PanelIndexCardDto>');