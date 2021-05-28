--Добавляем виджет для процедуры проверки на выбросы из рабочего стола Объектов-Ананлогов
INSERT INTO dashboards_panel (id, dashboard_id, title, column_index, order_in_column, panel_type_id, settings) 
VALUES (1000051, 1000002, 'Страница в системе', 0, 9, 5, '<PanelPartialViewDto><Url>~/Views/MarketObjects/OutliersChecking.cshtml</Url><Id>-1</Id><Title></Title><WindowWidth></WindowWidth><WindowHeight></WindowHeight><SrdFunctionTag></SrdFunctionTag></PanelPartialViewDto>');

--Добавляем виджет для процедуры проверки данных на дублирование
INSERT INTO dashboards_panel (id, dashboard_id, title, column_index, order_in_column, panel_type_id, settings) 
VALUES (1000008, 1000002, 'Страница в системе', 0, 7, 5, '<PanelPartialViewDto><Url>~/Views/CheckDuplicates/ProgressBar.cshtml</Url><Id>-1</Id><Title>Проверка на дублирование</Title><WindowWidth></WindowWidth><WindowHeight></WindowHeight><SrdFunctionTag></SrdFunctionTag></PanelPartialViewDto>');



--Добавляем таблицы для корректировок


--процессы
INSERT INTO core_long_process_type (id, process_name, class_name, schedule_type, repeat_interval, enabled, run_count, failure_count, last_start_date, last_run_duration, next_run_date, parameters, description, test_result, parameters_setter_url) VALUES (24, 'CorrectionByDateForMarketObjectsLongProcess', 'KadOzenka.Dal.LongProcess.CorrectionByDateForMarketObjectsLongProcess, KadOzenka.Dal', 0, null, 1, null, null, '2020-04-19 21:09:27.000000', null, null, null, 'Перерассчет цены объектов с учетом корректировки на дату', 1, null);
INSERT INTO core_long_process_type (id, process_name, class_name, schedule_type, repeat_interval, enabled, run_count, failure_count, last_start_date, last_run_duration, next_run_date, parameters, description, test_result, parameters_setter_url) VALUES (25, 'CorrectionByRoomForMarketObjectsLongProcess', 'KadOzenka.Dal.LongProcess.CorrectionByRoomForMarketObjectsLongProcess, KadOzenka.Dal', 1, 'FREQ=MONTHLY;BYDAY=1;BYHOUR=10;', 1, 7, null, '2021-01-31 10:00:04.000000', null, '2021-03-31 10:00:00.000000', null, 'Перерассчет цены объектов с учетом корректировки на комнатность', 1, null);
INSERT INTO core_long_process_type (id, process_name, class_name, schedule_type, repeat_interval, enabled, run_count, failure_count, last_start_date, last_run_duration, next_run_date, parameters, description, test_result, parameters_setter_url) VALUES (26, 'CorrectionByStageForMarketObjectsLongProcess', 'KadOzenka.Dal.LongProcess.CorrectionByStageForMarketObjectsLongProcess, KadOzenka.Dal', 1, 'FREQ=MONTHLY;BYDAY=1;BYHOUR=10;', 1, 7, null, '2021-01-31 10:00:04.000000', null, '2021-03-31 10:00:00.000000', null, 'Перерассчет цены объектов с учетом корректировки на этажность', 1, null);
INSERT INTO core_long_process_type (id, process_name, class_name, schedule_type, repeat_interval, enabled, run_count, failure_count, last_start_date, last_run_duration, next_run_date, parameters, description, test_result, parameters_setter_url) VALUES (27, 'CorrectionForFirstFloorForMarketObjectsLongProcess', 'KadOzenka.Dal.LongProcess.CorrectionForFirstFloorForMarketObjectsLongProcess, KadOzenka.Dal', 1, 'FREQ=MONTHLY;BYHOUR=11;', 1, 7, null, '2021-01-31 11:00:11.000000', null, '2021-03-31 11:00:00.000000', null, 'Перерассчет цены объектов с учетом корректировки на первый этаж', 1, null);



--реестры
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (108, 'Market.IndexesForDateCorrection', 'Индексы для корректировки на дату', null, null, 'MARKET_INDEXES_FOR_DATE_CORRECTION', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (111, 'Market.CoefficientsForCorrectionByRooms', 'Таблица, содержащая коэффициенты на квартиры по зданиям для корректировки на комнатность', null, null, 'MARKET_COEFFICIENT_FOR_ROOMS_CORRECTION', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (112, 'Market.PriceCorrectionByStageHistory', 'Таблица, содержащая историю изменения цены после корректировки на этажность', null, null, 'MARKET_PRICE_CORRECTION_BY_STAGE_HISTORY', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (113, 'Market.PriceAfterCorrectionByRoomsHistory', 'Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на комнатность', null, null, 'MARKET_PRICE_AFTER_CORRECTION_BY_ROOMS_H', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (114, 'Market.CoefficientsForFirstFloorCorr', 'Таблица, хранящая отношения цен первого этажа к верхним этажам', null, null, 'MARKET_COEFFICIENTS_FOR_FIRST_FLOOR_CORR', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (115, 'Market.PriceForFirstFloorHistory', 'Таблица, хранящая историю цен первых этажей', null, null, 'MARKET_PRICE_FOR_FIRST_FLOOR_HISTORY', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (116, 'Market.PriceAfterCorrectionByDateHistory', 'Таблица, содержащая ретроспективу цен по объектам с учетом корректировки на дату', null, null, 'MARKET_PRICE_AFTER_CORRECTION_BY_DATE_H', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (117, 'Market.CorrectionSettings', 'Таблица, содержащая настройки для коэффициентов корректировок', null, null, 'MARKET_CORRECTION_SETTINGS', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);


--атрибуты реестров
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10800100, 'Идентификатор', 108, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, '2020-03-17 18:06:22.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10800200, 'Дата', 108, 5, null, null, 'DATE', null, null, 0, null, null, 'Date', 0, null, null, null, null, 2, '2020-03-17 18:15:16.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10800400, 'Коэффициент', 108, 2, null, null, 'COEFFICIENT', null, null, 0, null, null, 'Coefficient', 0, null, null, null, null, 2, '2020-04-17 09:51:44.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10800600, 'Кадастровый номер здания', 108, 4, null, null, 'BUILDING_CADASTRAL_NUMBER', null, null, 0, null, null, 'BuildingCadastralNumber', 0, null, null, null, null, 2, '2020-04-17 09:51:36.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10800900, 'Сегмент рынка', 108, 4, null, 114, 'MARKET_SEGMENT', 'MARKET_SEGMENT_CODE', null, 0, null, null, 'MarketSegment', 0, null, null, null, null, 2, '2020-04-17 09:51:56.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10801000, 'Исключение здания из рассчета', 108, 3, null, null, 'IS_EXCLUDED', null, null, 0, null, null, 'IsExcluded', 1, null, null, null, null, 2, '2020-04-17 09:42:49.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11100100, 'Идентификатор', 111, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, '2020-03-26 14:50:29.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11100200, 'Кадастровый номер здания', 111, 4, null, null, 'BUILDING_CADASTRAL_NUMBER', null, null, 0, null, null, 'BuildingCadastralNumber', 0, null, null, null, null, 2, '2020-03-26 15:25:11.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11100300, 'Время изменения цены', 111, 5, null, null, 'CHANGING_DATE', null, null, 0, null, null, 'ChangingDate', 0, null, null, null, null, 2, '2020-03-26 14:52:25.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11100400, 'Коэффициент для 1-комнатной квартиры', 111, 2, null, null, 'ONE_ROOM_COEFFICIENT', null, null, 0, null, null, 'OneRoomCoefficient', 0, null, null, null, null, 2, '2020-03-26 15:26:29.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11100500, 'Коэффициент для 3-комнатной квартиры', 111, 2, null, null, 'THREE_ROOMS_COEFFICIENT', null, null, 0, null, null, 'ThreeRoomsCoefficient', 0, null, null, null, null, 2, '2020-03-27 15:18:34.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11100600, 'Сегмент рынка', 111, 4, null, 114, 'MARKET_SEGMENT', 'MARKET_SEGMENT_CODE', null, 0, null, null, 'MarketSegment', 1, null, null, null, null, 2, '2020-03-27 15:23:58.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11100700, 'Исключение здания из рассчета', 111, 3, null, null, 'IS_EXCLUDED', null, null, 0, null, null, 'IsExcluded', 1, null, null, null, null, 2, '2020-03-30 11:46:29.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11200100, 'Идентификатор', 112, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11200200, 'Кадастровый номер здания', 112, 4, null, null, 'BUILDING_CADASTRAL_NUMBER', null, null, 0, null, null, 'BuildingCadastralNumber', 0, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11200300, 'Время изменения цены', 112, 5, null, null, 'CHANGING_DATE', null, null, 0, null, null, 'ChangingDate', 0, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11200400, 'Коэффициент', 112, 2, null, null, 'STAGE_COEFFICIENT', null, null, 0, null, null, 'StageCoefficient', 0, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11200500, 'Сегмент рынка', 112, 4, null, 114, 'MARKET_SEGMENT', 'MARKET_SEGMENT_CODE', null, 0, null, null, 'MarketSegment', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11200600, 'Исключение здания из рассчета', 112, 3, null, null, 'IS_EXCLUDED', null, null, 0, null, null, 'IsExcluded', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11300100, 'Идентификатор', 113, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, '2020-03-31 15:20:52.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11300200, 'Идентификатор объекта', 113, 1, null, null, 'INITIAL_ID', null, null, 0, null, null, 'InitialId', 0, null, null, null, null, 2, '2020-03-31 15:21:47.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11300300, 'Время изменения цены', 113, 5, null, null, 'CHANGING_DATE', null, null, 0, null, null, 'ChangingDate', 0, null, null, null, null, 2, '2020-03-31 15:22:16.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11300400, 'Значение стоимости от', 113, 2, null, null, 'PRICE_VALUE_FROM', null, null, 0, null, null, 'PriceValueFrom', 1, null, null, null, null, 2, '2020-03-31 15:23:05.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11300500, 'Значение стоимости до', 113, 2, null, null, 'PRICE_VALUE_TO', null, null, 0, null, null, 'PriceValueTo', 1, null, null, null, null, 2, '2020-03-31 15:23:31.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11400100, 'Идентификатор', 114, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, '2020-04-01 13:44:14.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11400200, 'Дата сбора данных', 114, 5, null, null, 'STATS_DATE', null, null, 0, null, null, 'StatsDate', 0, null, null, null, null, 2, '2020-04-01 13:44:14.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11400300, 'Кадастровый номер здания', 114, 4, null, null, 'BUILDING_CADASTRAL_NUMBER', null, null, 0, null, null, 'BuildingCadastralNumber', 0, null, null, null, null, 2, '2020-04-01 13:44:14.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11400400, 'Сегмент рынка', 114, 4, null, 114, 'MARKET_SEGMENT', 'MARKET_SEGMENT_CODE', null, 0, null, null, 'MarketSegment', 1, null, null, null, null, 2, '2020-04-01 13:44:14.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11400500, 'Отношение цены первого этажа к верхним', 114, 2, null, null, 'FIRST_TO_UPPER_FLOOR_RATE', null, null, 0, null, null, 'FirstToUpperFloorRate', 0, null, null, null, null, 2, '2020-04-01 13:44:14.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11400600, 'Исключение здания из рассчета', 114, 3, null, null, 'IS_EXCLUDED_FROM_CALCULATION', null, null, 0, null, null, 'IsExcludedFromCalculation', 0, null, null, null, null, 2, '2020-04-01 13:44:14.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11500100, 'Идентификатор', 115, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, '2020-04-06 13:56:57.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11500200, 'Дата сбора данных', 115, 5, null, null, 'STATS_DATE', null, null, 0, null, null, 'StatsDate', 0, null, null, null, null, 2, '2020-04-06 13:56:57.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11500300, 'Цена с поправкой на первый этаж', 115, 2, null, null, 'PRICE_WITH_CORRECTION_FOR_FIRST_FLOOR', null, null, 0, null, null, 'PriceWithCorrectionForFirstFloor', 0, null, null, null, null, 2, '2020-04-06 13:56:57.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11500400, 'Объект аналог', 115, 1, null, null, 'OBJECT_ID', null, null, 0, null, null, 'ObjectId', 0, null, null, null, null, 2, '2020-04-06 13:56:57.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11600100, 'Идентификатор', 116, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, '2020-04-17 15:55:45.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11600200, 'Значение стоимости до', 116, 2, null, null, 'PRICE_VALUE_TO', null, null, 0, null, null, 'PriceValueTo', 1, null, null, null, null, 2, '2020-04-17 16:00:32.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11600300, 'Идентификатор объекта', 116, 1, null, null, 'INITIAL_ID', null, null, 0, null, null, 'InitialId', 0, null, null, null, null, 2, '2020-04-17 16:00:58.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11600400, 'Время изменения цены', 116, 5, null, null, 'CHANGING_DATE', null, null, 0, null, null, 'ChangingDate', 0, null, null, null, null, 2, '2020-04-17 16:01:35.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11600500, 'Значение стоимости от', 116, 2, null, null, 'PRICE_VALUE_FROM', null, null, 0, null, null, 'PriceValueFrom', 1, null, null, null, null, 2, '2020-04-17 16:02:04.000000', 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11700100, 'Идентификатор', 117, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11700200, 'Тип корректировки', 117, 4, null, 121, 'CORRECTION_TYPE', 'CORRECTION_TYPE_CODE', null, null, null, null, 'CorrectionType', 0, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (11700300, 'Настройки корректировки', 117, 4, null, null, 'SETTINGS', null, null, null, null, null, 'Settings', 1, null, null, null, null, null, null, 0);


--таблицы
CREATE TABLE public.market_indexes_for_date_correction (
  id BIGINT NOT NULL,
  date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
  building_cadastral_number VARCHAR(255) NOT NULL,
  coefficient NUMERIC NOT NULL,
  market_segment VARCHAR(255) NOT NULL,
  market_segment_code BIGINT NOT NULL,
  is_excluded SMALLINT,
  CONSTRAINT market_indexes_for_date_correction_date_key UNIQUE(date, building_cadastral_number, market_segment_code),
  CONSTRAINT reg_108_q_pk PRIMARY KEY(id)
) ;

ALTER TABLE public.market_indexes_for_date_correction
  OWNER TO postgres;



CREATE TABLE public.market_coefficient_for_rooms_correction (
  id BIGINT NOT NULL,
  building_cadastral_number VARCHAR NOT NULL,
  changing_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
  one_room_coefficient NUMERIC NOT NULL,
  three_rooms_coefficient NUMERIC NOT NULL,
  market_segment VARCHAR(255),
  market_segment_code BIGINT,
  is_excluded SMALLINT,
  CONSTRAINT reg_111_q_pk PRIMARY KEY(id)
) ;

CREATE UNIQUE INDEX correction_by_rooms_history_index ON public.market_coefficient_for_rooms_correction
  USING btree (building_cadastral_number COLLATE pg_catalog."default", changing_date, market_segment_code);

ALTER TABLE public.market_coefficient_for_rooms_correction
  OWNER TO postgres;




CREATE TABLE public.market_price_correction_by_stage_history (
  id BIGINT NOT NULL,
  building_cadastral_number VARCHAR NOT NULL,
  changing_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
  stage_coefficient NUMERIC NOT NULL,
  market_segment VARCHAR(255),
  market_segment_code BIGINT,
  is_excluded SMALLINT,
  CONSTRAINT reg_112_q_pk PRIMARY KEY(id)
) ;

ALTER TABLE public.market_price_correction_by_stage_history
  OWNER TO postgres;





CREATE TABLE public.market_price_after_correction_by_rooms_h (
  id BIGINT NOT NULL,
  initial_id BIGINT NOT NULL,
  changing_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
  price_value_from NUMERIC,
  price_value_to NUMERIC,
  CONSTRAINT reg_113_q_pk PRIMARY KEY(id)
) ;

ALTER TABLE public.market_price_after_correction_by_rooms_h
  OWNER TO postgres;




CREATE TABLE public.market_coefficients_for_first_floor_corr (
  id BIGINT NOT NULL,
  stats_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
  building_cadastral_number VARCHAR NOT NULL,
  market_segment VARCHAR(255),
  market_segment_code BIGINT,
  first_to_upper_floor_rate NUMERIC,
  is_excluded_from_calculation SMALLINT,
  CONSTRAINT reg_114_q_pk PRIMARY KEY(id)
) ;

CREATE UNIQUE INDEX coefficient_for_first_floor_correction_index ON public.market_coefficients_for_first_floor_corr
  USING btree (building_cadastral_number COLLATE pg_catalog."default", stats_date, market_segment_code);

ALTER TABLE public.market_coefficients_for_first_floor_corr
  OWNER TO postgres;





CREATE TABLE public.market_price_for_first_floor_history (
  id BIGINT NOT NULL,
  stats_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
  price_with_correction_for_first_floor NUMERIC NOT NULL,
  object_id BIGINT NOT NULL,
  CONSTRAINT reg_115_q_pk PRIMARY KEY(id)
) ;

ALTER TABLE public.market_price_for_first_floor_history
  OWNER TO postgres;






CREATE TABLE public.market_price_after_correction_by_date_h (
  id BIGINT NOT NULL,
  price_value_to NUMERIC,
  initial_id BIGINT NOT NULL,
  changing_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
  price_value_from NUMERIC,
  CONSTRAINT reg_116_q_pk PRIMARY KEY(id)
) ;

ALTER TABLE public.market_price_after_correction_by_date_h
  OWNER TO postgres;





CREATE TABLE public.market_correction_settings (
  id BIGINT NOT NULL,
  correction_type VARCHAR(255) NOT NULL,
  correction_type_code BIGINT NOT NULL,
  settings VARCHAR(4000),
  CONSTRAINT reg_117_q_pk PRIMARY KEY(id)
) ;

ALTER TABLE public.market_correction_settings
  OWNER TO postgres;