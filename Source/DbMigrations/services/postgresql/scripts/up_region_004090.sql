DO $$
BEGIN
perform create_source_register(25,'РГИС - геофакторы ОКС');
perform create_source_register(26,'РГИС - геофакторы ЗУ');

perform create_source_register_attribute(2500200,'X-координата',25,2);
perform create_source_register_attribute(2500300,'Y-координата',25,2);
perform create_source_register_attribute(2500400,'Наименование ближайшей к населенному пункту дороги федерального значения',25,4);
perform create_source_register_attribute(2500500,'Наименование ГНП',25,4);
perform create_source_register_attribute(2500600,'Наличие вблизи объекта (-ов), положительно влияющего(-их) на стоимость объектов недвижимости',25,4);
perform create_source_register_attribute(2500700,'Наличие вблизи объекта (-ов) отрицательно влияющего(-их) на стоимость  (свалки и т.п.)',25,4);
perform create_source_register_attribute(2500800,'Направление от г Москвы по дорогам федерального значения и основным шоссе (граф)',25,4);
perform create_source_register_attribute(2500900,'Направление от г Москвы по сторонам света',25,4);
perform create_source_register_attribute(2501000,'Обеспеченность территории центральной канализацией',25,4);
perform create_source_register_attribute(2501100,'Обеспеченность территории центральным водоснабжением',25,4);
perform create_source_register_attribute(2501300,'Обеспеченность территории центральным теплоснабжением',25,4);
perform create_source_register_attribute(2501400,'Обеспеченность территории центральным газоснабжением',25,4);
perform create_source_register_attribute(2501500,'Обеспеченность территории центральным электроснабжением',25,4);
perform create_source_register_attribute(2501600,'Подгруппа привязки',25,2);
perform create_source_register_attribute(2501700,'Расположение объекта относительно ближайшего водного объекта',25,2);
perform create_source_register_attribute(2501800,'Расположение объекта относительно ближайшей рекреационной зоны',25,2);
perform create_source_register_attribute(2501900,'Расстояние до ближайшей к населенному пункту дороги регионального значения',25,2);
perform create_source_register_attribute(2502000,'Расстояние до ближайшей к населенному пункту дороги федерального значения',25,2);
perform create_source_register_attribute(2502100,'Расстояние до ближайшей станции метро',25,2);
perform create_source_register_attribute(2502200,'Расстояние до государственной поликлиники, больницы',25,2);
perform create_source_register_attribute(2502300,'Расстояние до дорог федерального значения и основных шоссе Московской области (граф)',25,2);
perform create_source_register_attribute(2502400,'Расстояние до историко-культурного центра населенного пункта',25,2);
perform create_source_register_attribute(2502500,'Расстояние до МКАД для расчёта корректировки',25,2);
perform create_source_register_attribute(2502600,'Расстояние до общественно-делового центра населенного пункта',25,2);
perform create_source_register_attribute(2502700,'Расстояние до остановок общественного транспорта',25,2);
perform create_source_register_attribute(2502800,'Расстояние до школы или детского сада',25,2);
perform create_source_register_attribute(2502900,'Расстояние от населенного пункта до ближайших ж/д вокзала, станции, платформы',25,2);
perform create_source_register_attribute(2503000,'Расстояние от населенного пункта до центра муниципального района, городского округа',25,2);
perform create_source_register_attribute(2503100,'Расстояние от объекта до административного центра населенного пункта',25,2);
perform create_source_register_attribute(2503200,'Расстояние от объекта до ближайшего Ж\Д вокзала, станции, платформы(граф)',25,2);
perform create_source_register_attribute(2503300,'Расстояние от объекта до ближайшей из основных дорог населенного пункта',25,2);
perform create_source_register_attribute(2503400,'Расстояние от объекта до МКАД (граф)',25,2);
perform create_source_register_attribute(2503500,'Удаленность от объекта до МКАД (кольца)',25,1);




perform create_source_register_attribute(2600200,'X-координата',26,2);
perform create_source_register_attribute(2600300,'Y-координата',26,2);
perform create_source_register_attribute(2600400,'Доля ОЗИК',26,2);
perform create_source_register_attribute(2600500,'Доля СЗЗ',26,2);
perform create_source_register_attribute(2600600,'Зона ЗУ',26,4);
perform create_source_register_attribute(2600700,'Название ГСК',26,4);
perform create_source_register_attribute(2600800,'Наименование ГНП',26,4);
perform create_source_register_attribute(2600900,'Направление от г Москвы по дорогам федерального значения и основным шоссе (граф)',26,4);
perform create_source_register_attribute(2601000,'Направление от г Москвы по сторонам света (граф)',26,4);
perform create_source_register_attribute(2601100,'Обеспеченность территории центральной канализацией',26,4);
perform create_source_register_attribute(2601200,'Обеспеченность территории центральным водоснабжением',26,4);
perform create_source_register_attribute(2601300,'Обеспеченность территории центральным газоснабжением',26,4);
perform create_source_register_attribute(2601400,'Обеспеченность территории центральным теплоснабжением',26,4);
perform create_source_register_attribute(2601500,'Обеспеченность территории центральным электроснабжением',26,4);
perform create_source_register_attribute(2601700,'Подгруппа привязки',26,2);
perform create_source_register_attribute(2601800,'Попадание объекта в санитарную зону ТБО',26,4);
perform create_source_register_attribute(2601900,'Потенциальное нахождение в элитном посёлке',26,4);
perform create_source_register_attribute(2602000,'Расстояние до ближайшего населенного пункта',26,2);
perform create_source_register_attribute(2602100,'Расстояние до ближайшей к населенному пункту дороги регионального значения',26,2);
perform create_source_register_attribute(2602200,'Расстояние до ближайшей к населенному пункту дороги федерального значения',26,2);
perform create_source_register_attribute(2602300,'Расстояние до ближайшей остановки общественного транспорта',26,2);
perform create_source_register_attribute(2602400,'Расстояние до ближайшей станций метро',26,2);
perform create_source_register_attribute(2602500,'Расстояние до государственной поликлиники, больницы (не включая специализированные больницы)',26,2);
perform create_source_register_attribute(2602700,'Расстояние до дорог федерального значения и основных шоссе Московской области (граф)',26,2);
perform create_source_register_attribute(2602800,'Расстояние до историко-культурного центра населенного пункта',26,2);
perform create_source_register_attribute(2602900,'Расстояние до локального центра, отрицательно влияющего на стоимость объектов недвижимости',26,2);
perform create_source_register_attribute(2603000,'Расстояние до локального центра, положительно влияющего на стоимость объектов недвижимости',26,2);
perform create_source_register_attribute(2603100,'Расстояние до общественно-делового центра населенного пункта',26,2);
perform create_source_register_attribute(2603200,'Расстояние до остановок общественного транспорта (в т.ч. автовокзалы, автостанции и т.п.)',26,2);
perform create_source_register_attribute(2603300,'Расстояние до школы или детского сада',26,2);
perform create_source_register_attribute(2603400,'Расстояние от населенного пункта до ближайшей железной дороги промышленного назначения',26,2);
perform create_source_register_attribute(2603500,'Расстояние от населенного пункта до ближайших ж/д вокзала, станции, платформы',26,2);
perform create_source_register_attribute(2603600,'Расстояние от населенного пункта до центра муниципального района (городского округа)',26,2);
perform create_source_register_attribute(2603700,'Расстояние от объекта до административного центра населенного пункта',26,2);
perform create_source_register_attribute(2603800,'Расстояние от объекта до ближайшего водного объекта',26,2);
perform create_source_register_attribute(2603900,'Расстояние от объекта до ближайшего Ж\Д вокзала, станции, платформы МО (граф)',26,2);
perform create_source_register_attribute(2604000,'Расстояние от объекта до ближайшей из основных дорог населенного пункта',26,2);
perform create_source_register_attribute(2604100,'Расстояние от объекта до ближайшей рекреационной зоны',26,2);
perform create_source_register_attribute(2604200,'Расстояние от объекта до МКАД (граф)',26,2);
perform create_source_register_attribute(2604300,'Удаленность от объекта до МКАД (кольца)',26,4);
perform create_source_register_attribute(2604400,'Зона ПЗЗ',26,4);


perform create_source_register_tables_from_records(25);
perform create_source_register_tables_from_records(26);
end $$;

update core_srd_function set functiontag = 'KO.TASKS.DOWNLOAD_GEOGRAPHIC_FACTORS_FROM_RGIS', functionname = 'Загрузка географических факторов из ИС РГИС' where id = 630;


insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(82, 'Gbu.RgisLayers', 'Таблица, содержащая список слоев для факторов из РГИС', NULL, NULL, 'GBU_RGIS_LAYERS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL);

insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(8200100, 'Идентификатор', 82, 1, NULL, NULL, 'ID', NULL, NULL, 1, NULL, NULL, 'Id', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0);

insert into core_register_attribute ("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "change_user_id", "change_date", "hidden") values
(8200200, 'Название слоя', 82, 4, NULL, NULL, 'LAYER_NAME', NULL, NULL, 0, NULL, NULL, 'LayerName', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0);


create table GBU_RGIS_LAYERS (
    "id" BIGINT NOT NULL PRIMARY KEY,
    "layer_name"  VARCHAR(255) NOT NULL);

insert into GBU_RGIS_LAYERS ("id", "layer_name") values (2603300, 'BASE.DSOBRAZOVATELNYE_UCHREJDENIYA_MO_7903');
insert into GBU_RGIS_LAYERS ("id", "layer_name") values (2502800, 'BASE.DSOBRAZOVATELNYE_UCHREJDENIYA_MO_7903');

insert into core_long_process_type ("id", "process_name", "class_name", "schedule_type", "repeat_interval", "enabled", "run_count", "failure_count", "last_start_date", "last_run_duration", "next_run_date", "parameters", "description", "test_result", "parameters_setter_url") values
(99, 'GeoFactorsFromRgisLongProcess', 'KadOzenka.Dal.LongProcess.TaskLongProcesses.GeoFactorsFromRgisLongProcess, KadOzenka.Dal', 0, NULL, 1, NULL, NULL, null, NULL, NULL, NULL, 'Импорт данных из РГИС', 1, NULL);