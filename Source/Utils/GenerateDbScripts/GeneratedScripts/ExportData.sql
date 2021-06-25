delete from core_layout_details where layoutid > 1000000
--<DO>--
delete from core_qry_filter where qryid > 1000000
--<DO>--
delete from dashboards_panel where dashboard_id > 1000000
--<DO>--

insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(2, 2, NULL)
on conflict (id) do update set
"register_id"=2, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(3, 3, NULL)
on conflict (id) do update set
"register_id"=3, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(4, 4, NULL)
on conflict (id) do update set
"register_id"=4, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(5, 5, NULL)
on conflict (id) do update set
"register_id"=5, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(6, 6, NULL)
on conflict (id) do update set
"register_id"=6, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(7, 7, NULL)
on conflict (id) do update set
"register_id"=7, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(8, 8, NULL)
on conflict (id) do update set
"register_id"=8, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(9, 9, NULL)
on conflict (id) do update set
"register_id"=9, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(10, 10, NULL)
on conflict (id) do update set
"register_id"=10, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(11, 11, NULL)
on conflict (id) do update set
"register_id"=11, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(12, 12, NULL)
on conflict (id) do update set
"register_id"=12, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(13, 13, NULL)
on conflict (id) do update set
"register_id"=13, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(14, 14, NULL)
on conflict (id) do update set
"register_id"=14, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(15, 15, NULL)
on conflict (id) do update set
"register_id"=15, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(16, 16, NULL)
on conflict (id) do update set
"register_id"=16, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(17, 17, NULL)
on conflict (id) do update set
"register_id"=17, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(18, 18, NULL)
on conflict (id) do update set
"register_id"=18, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(19, 19, NULL)
on conflict (id) do update set
"register_id"=19, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(20, 20, NULL)
on conflict (id) do update set
"register_id"=20, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(21, 21, NULL)
on conflict (id) do update set
"register_id"=21, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(22, 22, NULL)
on conflict (id) do update set
"register_id"=22, "disable_editing"=NULL;

--<DO>--
insert into ko_objects_characteristics_register ("id", "register_id", "disable_editing") values
(23, 23, NULL)
on conflict (id) do update set
"register_id"=23, "disable_editing"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(2, 'Gbu.Source2', 'Источник: ЕГРН', 'GBU_SOURCE2_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 2, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source2', "registerdescription"='Источник: ЕГРН', "allpri_table"='GBU_SOURCE2_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=2, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(3, 'Gbu.Source3', 'Источник: База данных МОБТИ', 'GBU_SOURCE3_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source3', "registerdescription"='Источник: База данных МОБТИ', "allpri_table"='GBU_SOURCE3_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(4, 'Gbu.Source4', 'Источник: База данных предыдущего расчета', 'GBU_SOURCE4_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source4', "registerdescription"='Источник: База данных предыдущего расчета', "allpri_table"='GBU_SOURCE4_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(5, 'Gbu.Source5', 'Источник: Графический расчет / Сведения ОМС', 'GBU_SOURCE5_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source5', "registerdescription"='Источник: Графический расчет / Сведения ОМС', "allpri_table"='GBU_SOURCE5_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(6, 'Gbu.Source6', 'Источник: Идентификатор для загрузки из Excel', 'GBU_SOURCE6_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source6', "registerdescription"='Источник: Идентификатор для загрузки из Excel', "allpri_table"='GBU_SOURCE6_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(7, 'Gbu.Source7', 'Источник: Используется подсистемой моделирования', 'GBU_SOURCE7_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source7', "registerdescription"='Источник: Используется подсистемой моделирования', "allpri_table"='GBU_SOURCE7_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(8, 'Gbu.Source8', 'Источник: Источник информации о СЗЗ ТБО', 'GBU_SOURCE8_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source8', "registerdescription"='Источник: Источник информации о СЗЗ ТБО', "allpri_table"='GBU_SOURCE8_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(9, 'Gbu.Source9', 'Источник: Картографический расчет', 'GBU_SOURCE9_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source9', "registerdescription"='Источник: Картографический расчет', "allpri_table"='GBU_SOURCE9_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(10, 'Gbu.Source10', 'Источник: Минсельхоз', 'GBU_SOURCE10_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source10', "registerdescription"='Источник: Минсельхоз', "allpri_table"='GBU_SOURCE10_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(11, 'Gbu.Source11', 'Источник: Мособлстат', 'GBU_SOURCE11_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source11', "registerdescription"='Источник: Мособлстат', "allpri_table"='GBU_SOURCE11_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(12, 'Gbu.Source12', 'Источник: Определяется по результатам анализа изменений сведений ЕГРН', 'GBU_SOURCE12_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source12', "registerdescription"='Источник: Определяется по результатам анализа изменений сведений ЕГРН', "allpri_table"='GBU_SOURCE12_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(13, 'Gbu.Source13', 'Источник: Определяется по результатам расчета стоимости', 'GBU_SOURCE13_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source13', "registerdescription"='Источник: Определяется по результатам расчета стоимости', "allpri_table"='GBU_SOURCE13_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(14, 'Gbu.Source14', 'Источник: Пользовательский фактор', 'GBU_SOURCE14_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source14', "registerdescription"='Источник: Пользовательский фактор', "allpri_table"='GBU_SOURCE14_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(15, 'Gbu.Source15', 'Источник: Предоставлен ОМС', 'GBU_SOURCE15_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source15', "registerdescription"='Источник: Предоставлен ОМС', "allpri_table"='GBU_SOURCE15_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(16, 'Gbu.Source16', 'Источник: Предыдущий расчет объекта', 'GBU_SOURCE16_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source16', "registerdescription"='Источник: Предыдущий расчет объекта', "allpri_table"='GBU_SOURCE16_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(17, 'Gbu.Source17', 'Источник: Проставляется из справочников кодировки', 'GBU_SOURCE17_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source17', "registerdescription"='Источник: Проставляется из справочников кодировки', "allpri_table"='GBU_SOURCE17_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(18, 'Gbu.Source18', 'Источник: Расчетное значение', 'GBU_SOURCE18_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source18', "registerdescription"='Источник: Расчетное значение', "allpri_table"='GBU_SOURCE18_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(19, 'Gbu.Source19', 'Источник: РГИС', 'GBU_SOURCE19_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source19', "registerdescription"='Источник: РГИС', "allpri_table"='GBU_SOURCE19_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(20, 'Gbu.Source20', 'Источник: Рыночная информация', 'GBU_SOURCE20_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source20', "registerdescription"='Источник: Рыночная информация', "allpri_table"='GBU_SOURCE20_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(21, 'Gbu.Source21', 'Источник: Сведения о ЗУ', 'GBU_SOURCE21_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source21', "registerdescription"='Источник: Сведения о ЗУ', "allpri_table"='GBU_SOURCE21_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(22, 'Gbu.Source22', 'Источник: Соответствие с классификатором МЭР N 540', 'GBU_SOURCE22_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source22', "registerdescription"='Источник: Соответствие с классификатором МЭР N 540', "allpri_table"='GBU_SOURCE22_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(23, 'Gbu.Source23', 'Источник: Ссылка на предыдущее поступление объекта', 'GBU_SOURCE23_A', NULL, 'GBU_MAIN_OBJECT', NULL, 5, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, 1, 200)
on conflict (registerid) do update set
"registername"='Gbu.Source23', "registerdescription"='Источник: Ссылка на предыдущее поступление объекта', "allpri_table"='GBU_SOURCE23_A', "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=5, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=1, "main_register"=200;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(24, 'KO.ObjectsCharacteristicsRegister', 'Реестр хранения реестров с характеристиками объекта', NULL, NULL, 'KO_OBJECTS_CHARACTERISTICS_REGISTER', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.ObjectsCharacteristicsRegister', "registerdescription"='Реестр хранения реестров с характеристиками объекта', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_OBJECTS_CHARACTERISTICS_REGISTER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(80, 'Gbu.KadastrKvartal', 'Справочник кадастровых кварталов', NULL, NULL, 'GBU_KADASTR_KVARTAL', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Gbu.KadastrKvartal', "registerdescription"='Справочник кадастровых кварталов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='GBU_KADASTR_KVARTAL', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(81, 'Gbu.AttributeSettings', 'Реестр хранения настроек гбу атрибута', NULL, NULL, 'GBU_ATTRIBUTE_SETTINGS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Gbu.AttributeSettings', "registerdescription"='Реестр хранения настроек гбу атрибута', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='GBU_ATTRIBUTE_SETTINGS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(100, 'Market.CoreObject', 'Аналоги', NULL, NULL, 'MARKET_CORE_OBJECT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.CoreObject', "registerdescription"='Аналоги', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_CORE_OBJECT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(101, 'Market.YandexAddress', 'Адреса в яндек-формате', NULL, NULL, 'MARKET_ADDRESS_YANDEX', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.YandexAddress', "registerdescription"='Адреса в яндек-формате', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_ADDRESS_YANDEX', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(102, 'Gbu.AddressRosreestr', 'Адреса в формате Росреестра', NULL, NULL, 'GBU_ADDRESS_ROSREESTR', NULL, 4, 'REG_OBJECT_SEQ', 1, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Gbu.AddressRosreestr', "registerdescription"='Адреса в формате Росреестра', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='GBU_ADDRESS_ROSREESTR', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=1, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(103, 'Market.Settings', 'Таблица, содержащая настройки модуля', NULL, NULL, 'MARKET_SETTINGS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.Settings', "registerdescription"='Таблица, содержащая настройки модуля', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_SETTINGS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(104, 'Market.Screenshots', 'Таблица, содержащая информацию о скриншотах', NULL, NULL, 'MARKET_SCREENSHOTS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.Screenshots', "registerdescription"='Таблица, содержащая информацию о скриншотах', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_SCREENSHOTS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(105, 'Market.PriceHistory', 'Таблица, содержащая ретроспективу цен по объектам', NULL, NULL, 'MARKET_PRICE_HISTORY', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.PriceHistory', "registerdescription"='Таблица, содержащая ретроспективу цен по объектам', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_PRICE_HISTORY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(106, 'Market.DuplicatesHistory', 'Таблица, содержащая информацию о проведённых проверках на дублирование', NULL, NULL, 'MARKET_DUPLICATES_HISTORY', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.DuplicatesHistory', "registerdescription"='Таблица, содержащая информацию о проведённых проверках на дублирование', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_DUPLICATES_HISTORY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(107, 'Market.QuartalDictionary', 'Таблица, содержащая информацию о соответствии кад кварталов районам, округам и зонам ', NULL, NULL, 'MARKET_REGION_DICTIONATY', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.QuartalDictionary', "registerdescription"='Таблица, содержащая информацию о соответствии кад кварталов районам, округам и зонам ', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_REGION_DICTIONATY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(110, 'Market.CoreObjectTest', 'Временная таблица для проведения проверки механизма отбора дублей', NULL, NULL, 'MARKET_CORE_OBJECT_TEST', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.CoreObjectTest', "registerdescription"='Временная таблица для проведения проверки механизма отбора дублей', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_CORE_OBJECT_TEST', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(118, 'Market.CoefficientsOutliersChecking', 'Таблица, содержащая коэффициенты для проверки на выбросы', NULL, NULL, 'MARKET_COEFF_FOR_OUTLIERS_CHECKING', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.CoefficientsOutliersChecking', "registerdescription"='Таблица, содержащая коэффициенты для проверки на выбросы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_COEFF_FOR_OUTLIERS_CHECKING', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(119, 'Market.OutliersCheckingHistory', 'Таблица, содержащая информацию о проведённых проверках на выбросы', NULL, NULL, 'MARKET_OUTLIERS_CHECKING_HISTORY', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='Market.OutliersCheckingHistory', "registerdescription"='Таблица, содержащая информацию о проведённых проверках на выбросы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MARKET_OUTLIERS_CHECKING_HISTORY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(200, 'Gbu.MainObject', 'Объекты недвижимости', NULL, NULL, 'GBU_MAIN_OBJECT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Gbu.MainObject', "registerdescription"='Объекты недвижимости', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='GBU_MAIN_OBJECT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(201, 'KO.Unit', 'Единица оценки', NULL, NULL, 'KO_UNIT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.Unit', "registerdescription"='Единица оценки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_UNIT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(202, 'KO.Tour', 'Тур оценки', NULL, NULL, 'KO_TOUR', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.Tour', "registerdescription"='Тур оценки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_TOUR', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(203, 'KO.Task', 'Задание на оценку', NULL, NULL, 'KO_TASK', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.Task', "registerdescription"='Задание на оценку', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_TASK', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(205, 'KO.Group', 'Группы/Подгруппы', NULL, NULL, 'KO_GROUP', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.Group', "registerdescription"='Группы/Подгруппы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_GROUP', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(206, 'KO.Model', 'Модель', NULL, NULL, 'KO_MODEL', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.Model', "registerdescription"='Модель', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_MODEL', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(208, 'KO.GroupFactor', 'Факторы группы', NULL, NULL, 'KO_GROUP_FACTOR', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.GroupFactor', "registerdescription"='Факторы группы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_GROUP_FACTOR', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(210, 'KO.ModelFactor', 'Факторы модели', NULL, NULL, 'KO_MODEL_FACTOR', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.ModelFactor', "registerdescription"='Факторы модели', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_MODEL_FACTOR', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(211, 'KO.MarkCatalog', 'Справочник меток', NULL, NULL, 'KO_MARK_CATALOG', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.MarkCatalog', "registerdescription"='Справочник меток', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_MARK_CATALOG', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(212, 'KO.TourGroup', 'Группы тура', NULL, NULL, 'KO_TOUR_GROUPS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.TourGroup', "registerdescription"='Группы тура', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_TOUR_GROUPS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(213, 'KO.AttributeMap', 'Соответствие факторов учетной и расчетной части', NULL, NULL, 'KO_ATTRIBUTE_MAP', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.AttributeMap', "registerdescription"='Соответствие факторов учетной и расчетной части', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_ATTRIBUTE_MAP', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(215, 'KO.CodJob', 'Задания ЦОД', NULL, NULL, 'KO_COD_JOB', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.CodJob', "registerdescription"='Задания ЦОД', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_COD_JOB', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(216, 'KO.CostRosreestr', 'Данные о кадастровой стоимости из Росреестра', NULL, NULL, 'KO_COST_ROSREESTR', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.CostRosreestr', "registerdescription"='Данные о кадастровой стоимости из Росреестра', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_COST_ROSREESTR', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(217, 'KO.Explication', 'Экспликации площадей', NULL, NULL, 'KO_EXPLICATION', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.Explication', "registerdescription"='Экспликации площадей', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_EXPLICATION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(218, 'KO.Etalon', 'Эталонные объекты', NULL, NULL, 'KO_ETALON', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.Etalon', "registerdescription"='Эталонные объекты', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_ETALON', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(219, 'KO.TourFactorRegister', 'Реестр хранения факторов тура', NULL, NULL, 'KO_TOUR_FACTOR_REGISTER', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 1, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.TourFactorRegister', "registerdescription"='Реестр хранения факторов тура', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_TOUR_FACTOR_REGISTER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=1, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(220, 'KO.DocumentLink', 'Связи документов', NULL, NULL, 'KO_DOCUMENT_LINK', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.DocumentLink', "registerdescription"='Связи документов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_DOCUMENT_LINK', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(221, 'KO.KoResultSendJournal', 'Журнал отправки итогов расчета КО', NULL, NULL, 'KO_RESULT_SEND_JOURNAL', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.KoResultSendJournal', "registerdescription"='Журнал отправки итогов расчета КО', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_RESULT_SEND_JOURNAL', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(222, 'Ko.GroupToMarketSegmentRelation', 'Таблица для хранения отношений между группами и сегментами рынка', NULL, NULL, 'KO_GROUP_TO_MARKET_SEGMENT_RELATION', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='Ko.GroupToMarketSegmentRelation', "registerdescription"='Таблица для хранения отношений между группами и сегментами рынка', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_GROUP_TO_MARKET_SEGMENT_RELATION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(223, 'KO.ModelTrainingResultImages', 'Картинки с результатами обучения модели', NULL, NULL, 'ko_model_training_result_images', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.ModelTrainingResultImages', "registerdescription"='Картинки с результатами обучения модели', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='ko_model_training_result_images', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(250, 'KO.UnitParamsOks2018', 'Параметры расчета для ОКС 2018 года', NULL, NULL, 'KO_UNIT_PARAMS_OKS_2018', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.UnitParamsOks2018', "registerdescription"='Параметры расчета для ОКС 2018 года', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_UNIT_PARAMS_OKS_2018', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(251, 'KO.UnitParamsZu2018', 'Параметры расчета для ЗУ 2018 года', NULL, NULL, 'KO_UNIT_PARAMS_ZU_2018', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.UnitParamsZu2018', "registerdescription"='Параметры расчета для ЗУ 2018 года', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_UNIT_PARAMS_ZU_2018', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(252, 'KO.UnitParamsOks2016', 'Параметры расчета для ОКС 2016 года', NULL, NULL, 'KO_UNIT_PARAMS_OKS_2016', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.UnitParamsOks2016', "registerdescription"='Параметры расчета для ОКС 2016 года', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_UNIT_PARAMS_OKS_2016', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(253, 'KO.UnitParamsZu2016', 'Параметры расчета для ЗУ 2016 года', NULL, NULL, 'KO_UNIT_PARAMS_ZU_2016', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.UnitParamsZu2016', "registerdescription"='Параметры расчета для ЗУ 2016 года', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_UNIT_PARAMS_ZU_2016', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(254, 'KO.ComplianceGuide', 'Таблица соответствия кода и группы', NULL, NULL, 'KO_COMPLIANCE_GUIDE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.ComplianceGuide', "registerdescription"='Таблица соответствия кода и группы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_COMPLIANCE_GUIDE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(255, 'KO.CalcGroup', 'Реестр для зависимостей расчета', NULL, NULL, 'KO_CALC_GROUP', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.CalcGroup', "registerdescription"='Реестр для зависимостей расчета', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_CALC_GROUP', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(256, 'KO.UnitChange', 'Реестр для изменения сведений об объектах оценки', NULL, NULL, 'KO_UNIT_CHANGE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.UnitChange', "registerdescription"='Реестр для изменения сведений об объектах оценки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_UNIT_CHANGE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(257, 'KO.TransferAttributes', 'Соответствие атрибутов KO и GBU', NULL, NULL, 'KO_TRANSFER_ATTRIBUTES', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.TransferAttributes', "registerdescription"='Соответствие атрибутов KO и GBU', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_TRANSFER_ATTRIBUTES', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(258, 'KO.TourAttributeSettings', 'Реестр настройками использования заданных атрибутов для тура', NULL, NULL, 'KO_TOUR_ATTRIBUTE_SETTINGS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.TourAttributeSettings', "registerdescription"='Реестр настройками использования заданных атрибутов для тура', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_TOUR_ATTRIBUTE_SETTINGS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(259, 'KO.ReportHistory', 'Выгрузка отчетов', NULL, NULL, 'KO_REPORT_HISTORY', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.ReportHistory', "registerdescription"='Выгрузка отчетов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_REPORT_HISTORY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(260, 'KO.AutoCalculationSettings', 'Реестр настройки автоматического расчета', NULL, NULL, 'KO_AUTO_CALCULATION_SETTINGS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.AutoCalculationSettings', "registerdescription"='Реестр настройки автоматического расчета', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_AUTO_CALCULATION_SETTINGS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(262, 'KO.UnloadResultQueue', 'Реестр с данными о процессах выгрузки результатов оценки', NULL, NULL, 'KO_UNLOAD_RESULT_QUEUE', NULL, 4, 'REG_OBJECT_SEQ', 0, 1, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.UnloadResultQueue', "registerdescription"='Реестр с данными о процессах выгрузки результатов оценки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_UNLOAD_RESULT_QUEUE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=1, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(263, 'KO.FactorSettings', 'Настройки факторов', NULL, NULL, 'KO_FACTOR_SETTINGS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.FactorSettings', "registerdescription"='Настройки факторов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_FACTOR_SETTINGS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(264, 'KO.ModelingDictionary', 'Моделирование. Справочники', NULL, NULL, 'KO_MODELING_DICTIONARIES', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.ModelingDictionary', "registerdescription"='Моделирование. Справочники', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_MODELING_DICTIONARIES', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(265, 'KO.ModelingDictionariesValues', 'Моделирование. Значения справочников', NULL, NULL, 'KO_MODELING_DICTIONARIES_VALUES', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.ModelingDictionariesValues', "registerdescription"='Моделирование. Значения справочников', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_MODELING_DICTIONARIES_VALUES', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(266, 'KO.SystemAttributeSettings', 'Реестр с настройками атрибутов для общепользовательских операций', NULL, NULL, 'KO_SYSTEM_ATTRIBUTE_SETTINGS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.SystemAttributeSettings', "registerdescription"='Реестр с настройками атрибутов для общепользовательских операций', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_SYSTEM_ATTRIBUTE_SETTINGS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(267, 'KO.UnloadResultFiles', 'Реестр с файлами выгрузок результатов оценки', NULL, NULL, 'KO_UNLOAD_RESULT_FILES', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='KO.UnloadResultFiles', "registerdescription"='Реестр с файлами выгрузок результатов оценки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='KO_UNLOAD_RESULT_FILES', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(300, 'Sud.Zak', 'Экспертное заключение', 'SUD_ZAK_A', NULL, 'SUD_ZAK', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.Zak', "registerdescription"='Экспертное заключение', "allpri_table"='SUD_ZAK_A', "object_table"=NULL, "quant_table"='SUD_ZAK', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(301, 'Sud.Log', 'Журнал', NULL, NULL, 'SUD_LOG', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.Log', "registerdescription"='Журнал', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_LOG', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(302, 'Sud.ZakLink', 'Связь заключения с объектом', NULL, NULL, 'SUD_ZAKLINK', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.ZakLink', "registerdescription"='Связь заключения с объектом', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_ZAKLINK', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(303, 'Sud.DRS', 'Расчет ДРС', NULL, NULL, 'SUD_DRS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.DRS', "registerdescription"='Расчет ДРС', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_DRS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(304, 'Sud.OtchetLink', 'Связь отчета с объектом', NULL, NULL, 'SUD_OTCHETLINK', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.OtchetLink', "registerdescription"='Связь отчета с объектом', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_OTCHETLINK', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(305, 'Sud.ObjectStatus', 'Статус объекта', NULL, NULL, 'SUD_OBJECTSTATUS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.ObjectStatus', "registerdescription"='Статус объекта', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_OBJECTSTATUS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(306, 'Sud.OtchetLinkStatus', 'Связь отчета и статуса', NULL, NULL, 'SUD_OTCHETLINKSTATUS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.OtchetLinkStatus', "registerdescription"='Связь отчета и статуса', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_OTCHETLINKSTATUS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(307, 'Sud.OtchetStatus', 'Статус отчета', NULL, NULL, 'SUD_OTCHETSTATUS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.OtchetStatus', "registerdescription"='Статус отчета', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_OTCHETSTATUS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(308, 'Sud.Otchet', 'Отчеты', 'SUD_OTCHET_A', NULL, 'SUD_OTCHET', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.Otchet', "registerdescription"='Отчеты', "allpri_table"='SUD_OTCHET_A', "object_table"=NULL, "quant_table"='SUD_OTCHET', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(309, 'Sud.SudLinkStatus', 'Связь суда и статуса', NULL, NULL, 'SUD_SUDLINKSTATUS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.SudLinkStatus', "registerdescription"='Связь суда и статуса', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_SUDLINKSTATUS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(310, 'Sud.SudStatus', 'Статус', NULL, NULL, 'SUD_SUDSTATUS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.SudStatus', "registerdescription"='Статус', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_SUDSTATUS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(311, 'Sud.ZakLinkStatus', 'Связь заключения и статуса', NULL, NULL, 'SUD_ZAKLINKSTATUS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.ZakLinkStatus', "registerdescription"='Связь заключения и статуса', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_ZAKLINKSTATUS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(312, 'Sud.ZakStatus', 'Статус заключения', NULL, NULL, 'SUD_ZAKSTATUS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.ZakStatus', "registerdescription"='Статус заключения', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_ZAKSTATUS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(313, 'Sud.Dict', 'Справочник ФИО, организаций, СРО', NULL, NULL, 'SUD_DICT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.Dict', "registerdescription"='Справочник ФИО, организаций, СРО', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_DICT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(314, 'Sud.SudLink', 'Связь судебного дела и объекта', NULL, NULL, 'SUD_SUDLINK', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.SudLink', "registerdescription"='Связь судебного дела и объекта', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_SUDLINK', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(315, 'Sud.Object', 'Объект', 'SUD_OBJECT_A', NULL, 'SUD_OBJECT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.Object', "registerdescription"='Объект', "allpri_table"='SUD_OBJECT_A', "object_table"=NULL, "quant_table"='SUD_OBJECT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(316, 'Sud.Sud', 'Судебное заседание', 'SUD_SUD_A', NULL, 'SUD_SUD', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.Sud', "registerdescription"='Судебное заседание', "allpri_table"='SUD_SUD_A', "object_table"=NULL, "quant_table"='SUD_SUD', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(317, 'Sud.Param', 'Варианты значений полей со статусами', NULL, NULL, 'SUD_PARAM', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.Param', "registerdescription"='Варианты значений полей со статусами', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_PARAM', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(318, 'Sud.DopAnalisLog', 'Результаты выполнения процесса дополнительного анализа', NULL, NULL, 'SUD_DOPANALIZ_LOG', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Sud.DopAnalisLog', "registerdescription"='Результаты выполнения процесса дополнительного анализа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SUD_DOPANALIZ_LOG', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(400, 'Commission.Cost', 'Решение комиссий', 'COMISSION_COST_A', NULL, 'COMISSION_COST', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Commission.Cost', "registerdescription"='Решение комиссий', "allpri_table"='COMISSION_COST_A', "object_table"=NULL, "quant_table"='COMISSION_COST', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(500, 'Declarations.Book', 'Книги', 'DECLARATIONS_BOOK_A', NULL, 'DECLARATIONS_BOOK', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.Book', "registerdescription"='Книги', "allpri_table"='DECLARATIONS_BOOK_A', "object_table"=NULL, "quant_table"='DECLARATIONS_BOOK', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(501, 'Declarations.Declaration', 'Декларация', 'DECLARATIONS_DECLARATION_A', NULL, 'DECLARATIONS_DECLARATION', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.Declaration', "registerdescription"='Декларация', "allpri_table"='DECLARATIONS_DECLARATION_A', "object_table"=NULL, "quant_table"='DECLARATIONS_DECLARATION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(502, 'Declarations.HarOKS', 'Характеристики ОКС', 'DECLARATIONS_HAR_OKS_A', NULL, 'DECLARATIONS_HAR_OKS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.HarOKS', "registerdescription"='Характеристики ОКС', "allpri_table"='DECLARATIONS_HAR_OKS_A', "object_table"=NULL, "quant_table"='DECLARATIONS_HAR_OKS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(503, 'Declarations.HarParcel', 'Характеристики ЗУ', 'DECLARATIONS_HAR_PARCEL_A', NULL, 'DECLARATIONS_HAR_PARCEL', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.HarParcel', "registerdescription"='Характеристики ЗУ', "allpri_table"='DECLARATIONS_HAR_PARCEL_A', "object_table"=NULL, "quant_table"='DECLARATIONS_HAR_PARCEL', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(504, 'Declarations.Result', 'Результаты', NULL, NULL, 'DECLARATIONS_RESULT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.Result', "registerdescription"='Результаты', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DECLARATIONS_RESULT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(505, 'Declarations.Subject', 'Субъекты', 'DECLARATIONS_SUBJECT_A', NULL, 'DECLARATIONS_SUBJECT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.Subject', "registerdescription"='Субъекты', "allpri_table"='DECLARATIONS_SUBJECT_A', "object_table"=NULL, "quant_table"='DECLARATIONS_SUBJECT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(506, 'Declarations.Uved', 'Уведомления', 'DECLARATIONS_UVED_A', NULL, 'DECLARATIONS_UVED', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.Uved', "registerdescription"='Уведомления', "allpri_table"='DECLARATIONS_UVED_A', "object_table"=NULL, "quant_table"='DECLARATIONS_UVED', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(507, 'Declarations.UvedRejectionReasonType', 'Связь типов причин отказа и уведомлений об отказе и возврате документов', NULL, NULL, 'DECLARATIONS_UVED_REJECTION_REASON_TYPE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.UvedRejectionReasonType', "registerdescription"='Связь типов причин отказа и уведомлений об отказе и возврате документов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DECLARATIONS_UVED_REJECTION_REASON_TYPE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(508, 'Declarations.Signatory', 'Подписант', NULL, NULL, 'DECLARATIONS_SIGNATORY', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.Signatory', "registerdescription"='Подписант', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DECLARATIONS_SIGNATORY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(509, 'Declarations.HarOKSAdditionalInfo', 'Характеристики ОКС. Дополнительная информация', NULL, NULL, 'DECLARATIONS_HAR_OKS_ADDITIONAL_INFO', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.HarOKSAdditionalInfo', "registerdescription"='Характеристики ОКС. Дополнительная информация', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DECLARATIONS_HAR_OKS_ADDITIONAL_INFO', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(510, 'Declarations.HarParcelAdditionalInfo', 'Характеристики ЗУ. Дополнительная информация', NULL, NULL, 'DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Declarations.HarParcelAdditionalInfo', "registerdescription"='Характеристики ЗУ. Дополнительная информация', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DECLARATIONS_HAR_PARCEL_ADDITIONAL_INFO', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(600, 'ES.ExpressScore', 'Экспресс оценка', NULL, NULL, 'ES_EXPRESS_SCORE', NULL, 4, 'REG_OBJECT_SEQ', 0, 1, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='ES.ExpressScore', "registerdescription"='Экспресс оценка', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='ES_EXPRESS_SCORE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=1, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(702, 'Modeling.ModelToMarketObjects', 'Связь модели и объектов аналогов', NULL, NULL, 'MODELING_MODEL_TO_MARKET_OBJECTS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='Modeling.ModelToMarketObjects', "registerdescription"='Связь модели и объектов аналогов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='MODELING_MODEL_TO_MARKET_OBJECTS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(800, 'Common.ExportByTemplates', 'Журнал выгрузки данных', NULL, NULL, 'COMMON_EXPORT_BY_TEMPLATES', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Common.ExportByTemplates', "registerdescription"='Журнал выгрузки данных', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='COMMON_EXPORT_BY_TEMPLATES', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(801, 'Common.ImportDataLog', 'Журнал загрузки данных', NULL, NULL, 'COMMON_IMPORT_DATA_LOG', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Common.ImportDataLog', "registerdescription"='Журнал загрузки данных', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='COMMON_IMPORT_DATA_LOG', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(802, 'Common.DataFormStorage', 'Сохраненные данные форм ', NULL, NULL, 'COMMON_DATA_FORM_STORAGE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Common.DataFormStorage', "registerdescription"='Сохраненные данные форм ', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='COMMON_DATA_FORM_STORAGE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(809, 'Core.Reports.SavedReport', 'Сохраненные отчеты', NULL, NULL, 'FM_REPORTS_SAVEDREPORT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Reports.SavedReport', "registerdescription"='Сохраненные отчеты', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='FM_REPORTS_SAVEDREPORT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(812, 'Common.RecycleBin', 'Корзина с информацией об удаленных сушностях', NULL, NULL, 'COMMON_RECYCLE_BIN', NULL, 4, 'COMMON_RECYCLE_BIN_ID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Common.RecycleBin', "registerdescription"='Корзина с информацией об удаленных сушностях', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='COMMON_RECYCLE_BIN', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='COMMON_RECYCLE_BIN_ID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(813, 'Common.RegistersWithSoftDeletion', 'Информация о реестрах с логическим удалением', NULL, NULL, 'COMMON_REGISTERS_WITH_SOFT_DELETION', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Common.RegistersWithSoftDeletion', "registerdescription"='Информация о реестрах с логическим удалением', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='COMMON_REGISTERS_WITH_SOFT_DELETION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(814, 'Common.GbuOperationsReports', 'Таблица с отчетами для основных операций системы', NULL, NULL, 'COMMON_GBU_OPERATIONS_REPORTS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='Common.GbuOperationsReports', "registerdescription"='Таблица с отчетами для основных операций системы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='COMMON_GBU_OPERATIONS_REPORTS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(850, 'Core.Dashboards.Dashboard', 'Настройка дашбоарда', NULL, NULL, 'DASHBOARDS_DASHBOARD', NULL, 4, 'DASHBOARDS_DASHBOARD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Dashboards.Dashboard', "registerdescription"='Настройка дашбоарда', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DASHBOARDS_DASHBOARD', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='DASHBOARDS_DASHBOARD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(851, 'Core.Dashboards.Panel', 'Содержание дашбоарда', NULL, NULL, 'DASHBOARDS_PANEL', NULL, 4, 'DASHBOARDS_DASHBOARD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Dashboards.Panel', "registerdescription"='Содержание дашбоарда', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DASHBOARDS_PANEL', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='DASHBOARDS_DASHBOARD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(852, 'Core.Dashboards.PanelTypes', 'Типы панелей', NULL, NULL, 'DASHBOARDS_PANEL_TYPE', NULL, 4, 'DASHBOARDS_DASHBOARD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Dashboards.PanelTypes', "registerdescription"='Типы панелей', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DASHBOARDS_PANEL_TYPE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='DASHBOARDS_DASHBOARD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(853, 'Core.Dashboards.UserSettings', 'Пользовательские настройки панелей', NULL, NULL, 'DASHBOARDS_USER_SETTINGS', NULL, 4, 'DASHBOARDS_DASHBOARD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Dashboards.UserSettings', "registerdescription"='Пользовательские настройки панелей', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DASHBOARDS_USER_SETTINGS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='DASHBOARDS_DASHBOARD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(900, 'SPD.RequestRegistration', 'Журнал учёта запросов СПД', NULL, NULL, 'SPD_REQUEST_REGISTRATION', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='SPD.RequestRegistration', "registerdescription"='Журнал учёта запросов СПД', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='SPD_REQUEST_REGISTRATION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(920, 'Core.Register.List', 'Поименнованные списки объектов реестров', NULL, NULL, 'CORE_LIST', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.List', "registerdescription"='Поименнованные списки объектов реестров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LIST', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(921, 'Core.Register.ListObject', 'Идентификаторы объектов, входящих в список', NULL, NULL, 'CORE_LIST_OBJECT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.ListObject', "registerdescription"='Идентификаторы объектов, входящих в список', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LIST_OBJECT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(924, 'Core.Register.LayoutColumnType', 'Тип колонки раскладки', NULL, NULL, 'CORE_LAYOUT_COLUMN_TYPE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.LayoutColumnType', "registerdescription"='Тип колонки раскладки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LAYOUT_COLUMN_TYPE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(925, 'Core.SRD.UserSettingsRegisterView', 'Пользовательские настройки представления реестра', NULL, NULL, 'CORE_SRD_USERSETTINGSREGVIEW', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.UserSettingsRegisterView', "registerdescription"='Пользовательские настройки представления реестра', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_USERSETTINGSREGVIEW', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(926, 'Core.SRD.UserSettingsReport', 'Пользовательские настройки для отчетов', NULL, NULL, 'CORE_SRD_USERSETTINGSREPORT', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.UserSettingsReport', "registerdescription"='Пользовательские настройки для отчетов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_USERSETTINGSREPORT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(930, 'Core.Register.Register', 'Список реестров', NULL, NULL, 'CORE_REGISTER', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.Register', "registerdescription"='Список реестров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REGISTER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(931, 'Core.Register.Attribute', 'Список показателей реестра', NULL, NULL, 'CORE_REGISTER_ATTRIBUTE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, 'CHANGE_USER_ID', 'CHANGE_DATE', 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.Attribute', "registerdescription"='Список показателей реестра', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REGISTER_ATTRIBUTE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"='CHANGE_USER_ID', "track_changes_date"='CHANGE_DATE', "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(932, 'Core.Register.Relation', 'Список связей между реестрами', NULL, NULL, 'CORE_REGISTER_RELATION', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.Relation', "registerdescription"='Список связей между реестрами', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REGISTER_RELATION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(933, 'Core.Register.Layout', 'Раскладки', NULL, NULL, 'CORE_LAYOUT', NULL, 4, 'CORE_LAYOUT_ID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.Layout', "registerdescription"='Раскладки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LAYOUT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_LAYOUT_ID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(935, 'Core.Register.LayoutDetail', 'Детализация раскладок', NULL, NULL, 'CORE_LAYOUT_DETAILS', NULL, 4, 'CORE_LAYOUT_DET_ID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.LayoutDetail', "registerdescription"='Детализация раскладок', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LAYOUT_DETAILS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_LAYOUT_DET_ID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(936, 'Core.Register.Qry', 'Фильтры реестров', NULL, NULL, 'CORE_QRY', NULL, 4, 'QRY_QRYID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.Qry', "registerdescription"='Фильтры реестров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_QRY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='QRY_QRYID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(937, 'Core.Register.QryFilter', 'Условия фильтров', NULL, NULL, 'CORE_QRY_FILTER', NULL, 4, 'QRYFILTER_QRYFILTERID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.QryFilter', "registerdescription"='Условия фильтров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_QRY_FILTER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='QRYFILTER_QRYFILTERID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(938, 'Core.Register.QryOperation', 'Операции фильтров', NULL, NULL, 'CORE_QRY_OPERATION', NULL, 4, 'QRYOPERATION_QRYOPERATIONI_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.QryOperation', "registerdescription"='Операции фильтров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_QRY_OPERATION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='QRYOPERATION_QRYOPERATIONI_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(939, 'Core.Register.Lock', 'Блокировка объекта реестра', NULL, NULL, 'CORE_REGISTER_LOCK', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.Lock', "registerdescription"='Блокировка объекта реестра', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REGISTER_LOCK', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(940, 'Core.SRD.Audit', 'Аудит действий пользователей с функциями модулей (подсистем) системы', NULL, NULL, 'CORE_SRD_AUDIT', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.Audit', "registerdescription"='Аудит действий пользователей с функциями модулей (подсистем) системы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_AUDIT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(941, 'Core.SRD.Department', 'Подразделение в организации пользователя системы', NULL, NULL, 'CORE_SRD_DEPARTMENT', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.Department', "registerdescription"='Подразделение в организации пользователя системы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_DEPARTMENT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(942, 'Core.SRD.Function', 'Функции модулей (подсистем) системы', NULL, NULL, 'CORE_SRD_FUNCTION', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.Function', "registerdescription"='Функции модулей (подсистем) системы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_FUNCTION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(945, 'Core.SRD.Role', 'Роли в системе', NULL, NULL, 'CORE_SRD_ROLE', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.Role', "registerdescription"='Роли в системе', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_ROLE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(946, 'Core.SRD.RoleFunction', 'Функции роли (бывшая LOCROLE_LOCFUNCTION)', NULL, NULL, 'CORE_SRD_ROLE_FUNCTION', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.RoleFunction', "registerdescription"='Функции роли (бывшая LOCROLE_LOCFUNCTION)', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_ROLE_FUNCTION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(947, 'Core.SRD.RoleRegister', 'Права доступа роли к реестру', NULL, NULL, 'CORE_SRD_ROLE_REGISTER', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.RoleRegister', "registerdescription"='Права доступа роли к реестру', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_ROLE_REGISTER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(948, 'Core.SRD.RoleAttr', 'Права доступа роли к атрибутам реестра', NULL, NULL, 'CORE_SRD_ROLE_ATTR', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.RoleAttr', "registerdescription"='Права доступа роли к атрибутам реестра', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_ROLE_ATTR', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(949, 'Core.SRD.Session', 'Параметры сессии пользователя', NULL, NULL, 'CORE_SRD_SESSION', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.Session', "registerdescription"='Параметры сессии пользователя', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_SESSION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(950, 'Core.SRD.User', 'Пользователи системы', NULL, NULL, 'CORE_SRD_USER', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.User', "registerdescription"='Пользователи системы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_USER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(951, 'Core.SRD.UserSettings', 'Пользовательские настройки', NULL, NULL, 'CORE_SRD_USERSETTINGS', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.UserSettings', "registerdescription"='Пользовательские настройки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_USERSETTINGS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(952, 'Core.SRD.UserRole', 'Роли пользователя', NULL, NULL, 'CORE_SRD_USER_ROLE', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.UserRole', "registerdescription"='Роли пользователя', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_USER_ROLE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(953, 'Core.SRD.UserAlt', 'Пользователи системы', NULL, NULL, 'CORE_SRD_USER', NULL, 4, 'REG_OBJECT_SEQ', 1, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.UserAlt', "registerdescription"='Пользователи системы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_USER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=1, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(954, 'Core.SRD.UserSettingsLayout', 'Пользовательские настройки раскладкок', NULL, NULL, 'CORE_SRD_USERSETTINGSLAYOUT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.UserSettingsLayout', "registerdescription"='Пользовательские настройки раскладкок', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_USERSETTINGSLAYOUT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(955, 'Core.SRD.RoleFilter', 'Разграничение прав доступа по данным реестров', NULL, NULL, 'CORE_SRD_ROLE_FILTER', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.RoleFilter', "registerdescription"='Разграничение прав доступа по данным реестров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_ROLE_FILTER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(956, 'Core.Register.LayoutExport', 'Выгрузка данных по раскладке', NULL, NULL, 'CORE_LAYOUT_EXPORT', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.LayoutExport', "registerdescription"='Выгрузка данных по раскладке', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LAYOUT_EXPORT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(957, 'Core.SRD.RegisterCategory', 'Категории доступа к данным реестров', NULL, NULL, 'CORE_SRD_REGISTER_CATEGORY', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.RegisterCategory', "registerdescription"='Категории доступа к данным реестров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_REGISTER_CATEGORY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(958, 'Core.SRD.FunctionRegisterCategory', 'Доступ функции к категориям доступа реестров', NULL, NULL, 'CORE_SRD_FUNCTION_REG_CAT', NULL, 4, 'CORE_SRD_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.SRD.FunctionRegisterCategory', "registerdescription"='Доступ функции к категориям доступа реестров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_SRD_FUNCTION_REG_CAT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_SRD_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(960, 'Core.TD.Template', 'Шаблон (тип) технологического документа', NULL, NULL, 'CORE_TD_TEMPLATE', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.Template', "registerdescription"='Шаблон (тип) технологического документа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_TEMPLATE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(961, 'Core.TD.TemplateVersion', 'Версия шаблона технологического документа', NULL, NULL, 'CORE_TD_TEMPLATE_VERSION', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.TemplateVersion', "registerdescription"='Версия шаблона технологического документа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_TEMPLATE_VERSION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(962, 'Core.TD.Status', 'Типы статусов экземпляра технологического документа', NULL, NULL, 'CORE_TD_STATUS', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.Status', "registerdescription"='Типы статусов экземпляра технологического документа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_STATUS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(963, 'Core.TD.Instance', 'Документы', NULL, NULL, 'CORE_TD_INSTANCE', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.Instance', "registerdescription"='Документы', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_INSTANCE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(964, 'Core.TD.Changeset', 'Набор изменений в реестрах', NULL, NULL, 'CORE_TD_CHANGESET', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.Changeset', "registerdescription"='Набор изменений в реестрах', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_CHANGESET', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(965, 'Core.TD.Change', 'Изменение в реестре', NULL, NULL, 'CORE_TD_CHANGE', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.Change', "registerdescription"='Изменение в реестре', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_CHANGE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(966, 'Core.TD.AuditAction', 'Типы аудируемых действий с экземпляром технологического документа', NULL, NULL, 'CORE_TD_AUDIT_ACTION', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.AuditAction', "registerdescription"='Типы аудируемых действий с экземпляром технологического документа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_AUDIT_ACTION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(967, 'Core.TD.Audit', 'Аудит действий с экземпляром технологического документа', NULL, NULL, 'CORE_TD_AUDIT', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.Audit', "registerdescription"='Аудит действий с экземпляром технологического документа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_AUDIT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(968, 'Core.TD.Tree', 'Дерево шаблонов ', NULL, NULL, 'CORE_TD_TREE', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.Tree', "registerdescription"='Дерево шаблонов ', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_TREE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(969, 'Core.TD.Attachment', 'Электронные образы экземпляра документа', NULL, NULL, 'CORE_TD_ATTACHMENTS', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.Attachment', "registerdescription"='Электронные образы экземпляра документа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_ATTACHMENTS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(970, 'Core.TD.TP', 'Данные об уведомлении процесса, из которого создан экземпляр технологического документа', NULL, NULL, 'CORE_TD_TP', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.TP', "registerdescription"='Данные об уведомлении процесса, из которого создан экземпляр технологического документа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_TP', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(971, 'Core.TD.TemplateType', 'Тип шаблона технологического документа', NULL, NULL, 'CORE_TD_TEMPLATE_TYPE', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.TD.TemplateType', "registerdescription"='Тип шаблона технологического документа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_TD_TEMPLATE_TYPE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(975, 'Core.LongProcess.Queue', 'Очередь долгих процессов', NULL, NULL, 'CORE_LONG_PROCESS_QUEUE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.LongProcess.Queue', "registerdescription"='Очередь долгих процессов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LONG_PROCESS_QUEUE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(976, 'Core.LongProcess.ProcessType', 'Типы долгих процессов', NULL, NULL, 'CORE_LONG_PROCESS_TYPE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.LongProcess.ProcessType', "registerdescription"='Типы долгих процессов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LONG_PROCESS_TYPE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(977, 'Core.LongProcess.Log', 'Журнал менеджера долгих процессов', NULL, NULL, 'CORE_LONG_PROCESS_LOG', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.LongProcess.Log', "registerdescription"='Журнал менеджера долгих процессов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_LONG_PROCESS_LOG', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(978, 'Core.Shared.Configparam', 'Файлы конфигурации', NULL, NULL, 'CORE_CONFIGPARAM', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.Configparam', "registerdescription"='Файлы конфигурации', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_CONFIGPARAM', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(979, 'Core.LongProcess.BackgroundExport', 'Фоновые выгрузки', NULL, NULL, 'CORE_BACKGROUND_EXPORTS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.LongProcess.BackgroundExport', "registerdescription"='Фоновые выгрузки', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_BACKGROUND_EXPORTS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(980, 'Core.Message', 'Сообщения', NULL, NULL, 'CORE_MESSAGES', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Message', "registerdescription"='Сообщения', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_MESSAGES', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(981, 'Core.MessageTo', 'Статус прочтения сообщения пользователем', NULL, NULL, 'CORE_MESSAGES_TO', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.MessageTo', "registerdescription"='Статус прочтения сообщения пользователем', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_MESSAGES_TO', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(982, 'Core.Shared.Reference', 'Справочник', NULL, NULL, 'CORE_REFERENCE', NULL, 4, 'REFERENCE_ID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.Reference', "registerdescription"='Справочник', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REFERENCE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REFERENCE_ID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(983, 'Core.Shared.ReferenceItem', 'Справочное значение', NULL, NULL, 'CORE_REFERENCE_ITEM', NULL, 4, 'REFITEM_ITEMID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.ReferenceItem', "registerdescription"='Справочное значение', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REFERENCE_ITEM', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REFITEM_ITEMID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(984, 'Core.Shared.ReferenceRelation', 'Связи справочников', NULL, NULL, 'CORE_REFERENCE_RELATION', NULL, 4, 'REFRELATION_ID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.ReferenceRelation', "registerdescription"='Связи справочников', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REFERENCE_RELATION', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REFRELATION_ID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(985, 'Core.Shared.ReferenceTree', 'Связи справочных значений', NULL, NULL, 'CORE_REFERENCE_TREE', NULL, 4, 'TREEHELPER_ID_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.ReferenceTree', "registerdescription"='Связи справочных значений', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REFERENCE_TREE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='TREEHELPER_ID_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(986, 'Core.Shared.Attachment', 'Образ', NULL, NULL, 'CORE_ATTACHMENT', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.Attachment', "registerdescription"='Образ', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_ATTACHMENT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(987, 'Core.Shared.AttachmentFile', 'Файлы образа', NULL, NULL, 'CORE_ATTACHMENT_FILE', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.AttachmentFile', "registerdescription"='Файлы образа', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_ATTACHMENT_FILE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(988, 'Core.Shared.AttachmentObject', 'Связь образа и объекта реестра', NULL, NULL, 'CORE_ATTACHMENT_OBJECT', NULL, 4, 'SEQ_CORE_TD', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.AttachmentObject', "registerdescription"='Связь образа и объекта реестра', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_ATTACHMENT_OBJECT', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='SEQ_CORE_TD', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(989, 'Core.Shared.ErrorLog', 'Журнал ошибок', NULL, NULL, 'CORE_ERROR_LOG', NULL, 4, 'CORE_ERROR_LOG_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.ErrorLog', "registerdescription"='Журнал ошибок', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_ERROR_LOG', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_ERROR_LOG_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(990, 'Core.Shared.RefItemOrganisation', 'Связь объекта справочника с организацией', NULL, NULL, 'CORE_REFERENCE_ITEM_ORG', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.RefItemOrganisation', "registerdescription"='Связь объекта справочника с организацией', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REFERENCE_ITEM_ORG', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(991, 'Core.Shared.RegisterState', 'Сохраненные состояния представлений ', NULL, NULL, 'CORE_REGISTER_STATE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.RegisterState', "registerdescription"='Сохраненные состояния представлений ', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REGISTER_STATE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(992, 'Core.Shared.Diagnostics', 'Журнал отладочных сообщений', NULL, NULL, 'CORE_DIAGNOSTICS', NULL, 4, 'CORE_DIAGNOSTICS_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.Diagnostics', "registerdescription"='Журнал отладочных сообщений', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_DIAGNOSTICS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='CORE_DIAGNOSTICS_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(993, 'Core.Register.RegisterParent', 'Родительский реестр', NULL, NULL, 'CORE_REGISTER', NULL, 4, 'REG_OBJECT_SEQ', 1, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Register.RegisterParent', "registerdescription"='Родительский реестр', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REGISTER', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=1, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(994, 'Core.Shared.RegNomRepository', 'Репозиторий регистрационных номеров', NULL, NULL, 'CORE_REGNOM_REPOSITORY', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.RegNomRepository', "registerdescription"='Репозиторий регистрационных номеров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REGNOM_REPOSITORY', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(995, 'Core.Shared.RegNomSequences', 'Последовательности регистрационных номеров', NULL, NULL, 'CORE_REGNOM_SEQUENCES', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.RegNomSequences', "registerdescription"='Последовательности регистрационных номеров', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_REGNOM_SEQUENCES', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(996, 'Core.Shared.CacheUpdates', 'Временные метки обновления кэша', NULL, NULL, 'CORE_CACHE_UPDATES', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.CacheUpdates', "registerdescription"='Временные метки обновления кэша', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_CACHE_UPDATES', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(997, 'Core.Shared.UpdateStructure', 'Журнал обновления структуры БД', NULL, NULL, 'CORE_UPDSTRU_LOG', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.UpdateStructure', "registerdescription"='Журнал обновления структуры БД', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_UPDSTRU_LOG', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(998, 'Core.Shared.Holiday', 'Выходные и праздничные дни', NULL, NULL, 'CORE_HOLIDAYS', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, 0, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Shared.Holiday', "registerdescription"='Выходные и праздничные дни', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='CORE_HOLIDAYS', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=0, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_register ("registerid", "registername", "registerdescription", "allpri_table", "object_table", "quant_table", "track_changes_column", "storage_type", "object_sequence", "is_virtual", "contains_quant_in_future", "db_connection_name", "track_changes_userid", "track_changes_date", "is_deleted", "allpri_partitioning", "main_register") values
(999, 'Core.Dashboards.IndexCardCache', 'Кэш рабочих столов', NULL, NULL, 'DASHBOARDS_PANELINDEXCARDCACHE', NULL, 4, 'REG_OBJECT_SEQ', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL)
on conflict (registerid) do update set
"registername"='Core.Dashboards.IndexCardCache', "registerdescription"='Кэш рабочих столов', "allpri_table"=NULL, "object_table"=NULL, "quant_table"='DASHBOARDS_PANELINDEXCARDCACHE', "track_changes_column"=NULL, "storage_type"=4, "object_sequence"='REG_OBJECT_SEQ', "is_virtual"=0, "contains_quant_in_future"=0, "db_connection_name"=NULL, "track_changes_userid"=NULL, "track_changes_date"=NULL, "is_deleted"=NULL, "allpri_partitioning"=NULL, "main_register"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(101, 'Виды сторонних площадок', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'MarketTypes', NULL, NULL)
on conflict (referenceid) do update set
"description"='Виды сторонних площадок', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='MarketTypes', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(102, 'Виды объектов недвижимости', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'PropertyTypes', NULL, NULL)
on conflict (referenceid) do update set
"description"='Виды объектов недвижимости', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='PropertyTypes', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(103, 'Назначение зданий', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Назначение зданий', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(104, 'Типы наружных стен здания', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Типы наружных стен здания', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(105, 'Справочник регионов', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Справочник регионов', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(106, 'Справочник единиц измерения', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Справочник единиц измерения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(107, 'Виды культурного назначения ОКН', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Виды культурного назначения ОКН', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(108, 'Справочник типов документов', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Справочник типов документов', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(109, 'Справочник характеристик', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Справочник характеристик', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(110, 'Тип сделки', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'DealType', NULL, NULL)
on conflict (referenceid) do update set
"description"='Тип сделки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='DealType', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(111, 'Группа', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Группа', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(112, 'Подгруппа', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Подгруппа', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(113, 'Процесс обработки', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'ProcessStep', NULL, NULL)
on conflict (referenceid) do update set
"description"='Процесс обработки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='ProcessStep', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(114, 'Сегмент рынка недвижимости', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'MarketSegment', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Квартира",
		"Name": "Flat"
	},
	{
		"Id": 2,
		"Value": "Койко-место",
		"Name": "Bed"
	},
	{
		"Id": 3,
		"Value": "Комната",
		"Name": "Room"
	},
	{
		"Id": 4,
		"Value": "Дом/дача",
		"Name": "House"
	},
	{
		"Id": 5,
		"Value": "Коттедж",
		"Name": "Cottage"
	},
	{
		"Id": 6,
		"Value": "Таунхаус",
		"Name": "Townhouse"
	},
	{
		"Id": 7,
		"Value": "Часть дома",
		"Name": "HouseShare"
	},
	{
		"Id": 8,
		"Value": "Гараж",
		"Name": "Garage"
	},
	{
		"Id": 9,
		"Value": "Готовый бизнес",
		"Name": "Business"
	},
	{
		"Id": 10,
		"Value": "Здание",
		"Name": "Building"
	},
	{
		"Id": 11,
		"Value": "Коммерческая земля",
		"Name": "CommercialLand"
	},
	{
		"Id": 12,
		"Value": "Офис",
		"Name": "Office"
	},
	{
		"Id": 13,
		"Value": "Помещение свободного назначения",
		"Name": "FreeAppointmentObject"
	},
	{
		"Id": 14,
		"Value": "Производство",
		"Name": "Industry"
	},
	{
		"Id": 15,
		"Value": "Склад",
		"Name": "Warehouse"
	},
	{
		"Id": 16,
		"Value": "Торговая площадь",
		"Name": "ShoppingArea"
	},
	{
		"Id": 17,
		"Value": "Доля в квартире",
		"Name": "FlatShare"
	},
	{
		"Id": 17,
		"Value": "Квартира в новостройке",
		"Name": "NewBuildingFlat"
	},
	{
		"Id": 18,
		"Value": "Участок",
		"Name": "Land"
	}
]')
on conflict (referenceid) do update set
"description"='Сегмент рынка недвижимости', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='MarketSegment', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Квартира",
		"Name": "Flat"
	},
	{
		"Id": 2,
		"Value": "Койко-место",
		"Name": "Bed"
	},
	{
		"Id": 3,
		"Value": "Комната",
		"Name": "Room"
	},
	{
		"Id": 4,
		"Value": "Дом/дача",
		"Name": "House"
	},
	{
		"Id": 5,
		"Value": "Коттедж",
		"Name": "Cottage"
	},
	{
		"Id": 6,
		"Value": "Таунхаус",
		"Name": "Townhouse"
	},
	{
		"Id": 7,
		"Value": "Часть дома",
		"Name": "HouseShare"
	},
	{
		"Id": 8,
		"Value": "Гараж",
		"Name": "Garage"
	},
	{
		"Id": 9,
		"Value": "Готовый бизнес",
		"Name": "Business"
	},
	{
		"Id": 10,
		"Value": "Здание",
		"Name": "Building"
	},
	{
		"Id": 11,
		"Value": "Коммерческая земля",
		"Name": "CommercialLand"
	},
	{
		"Id": 12,
		"Value": "Офис",
		"Name": "Office"
	},
	{
		"Id": 13,
		"Value": "Помещение свободного назначения",
		"Name": "FreeAppointmentObject"
	},
	{
		"Id": 14,
		"Value": "Производство",
		"Name": "Industry"
	},
	{
		"Id": 15,
		"Value": "Склад",
		"Name": "Warehouse"
	},
	{
		"Id": 16,
		"Value": "Торговая площадь",
		"Name": "ShoppingArea"
	},
	{
		"Id": 17,
		"Value": "Доля в квартире",
		"Name": "FlatShare"
	},
	{
		"Id": 17,
		"Value": "Квартира в новостройке",
		"Name": "NewBuildingFlat"
	},
	{
		"Id": 18,
		"Value": "Участок",
		"Name": "Land"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(115, 'Материал стен', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'WallMaterial', NULL, NULL)
on conflict (referenceid) do update set
"description"='Материал стен', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='WallMaterial', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(116, 'Класс качества', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'QualityClass', NULL, NULL)
on conflict (referenceid) do update set
"description"='Класс качества', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='QualityClass', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(117, 'Причина исключения', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'ExclusionStatus', NULL, NULL)
on conflict (referenceid) do update set
"description"='Причина исключения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='ExclusionStatus', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(118, 'Виды объектов недвижимости ЦИПЖС', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'PropertyTypesCIPJS', NULL, NULL)
on conflict (referenceid) do update set
"description"='Виды объектов недвижимости ЦИПЖС', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='PropertyTypesCIPJS', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(119, 'Административные округа', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'Hunteds', NULL, NULL)
on conflict (referenceid) do update set
"description"='Административные округа', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='Hunteds', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(120, 'Районы', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'Districts', NULL, NULL)
on conflict (referenceid) do update set
"description"='Районы', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='Districts', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(121, 'Типы корректировок', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'MarketObjects.CorrectionTypes', NULL, '[
	{
		"Id": 1,
		"Value": "Корректировка на дату",
		"Name": "CorrectionByDate"
	},
	{
		"Id": 2,
		"Value": "Корректировка на торг",
		"Name": "CorrectionByBargain"
	},
	{
		"Id": 3,
		"Value": "Корректировка на комнатность",
		"Name": "CorrectionByRoom"
	},
	{
		"Id": 4,
		"Value": "Корректировка на этажность",
		"Name": "CorrectionByStage"
	}
]')
on conflict (referenceid) do update set
"description"='Типы корректировок', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='MarketObjects.CorrectionTypes', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Корректировка на дату",
		"Name": "CorrectionByDate"
	},
	{
		"Id": 2,
		"Value": "Корректировка на торг",
		"Name": "CorrectionByBargain"
	},
	{
		"Id": 3,
		"Value": "Корректировка на комнатность",
		"Name": "CorrectionByRoom"
	},
	{
		"Id": 4,
		"Value": "Корректировка на этажность",
		"Name": "CorrectionByStage"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(122, 'Налог', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'VatType', NULL, '[
	{
		"Id": 0,
		"Value": "Нет данных",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "НДС включен",
		"Name": "NDS"
	},
	{
		"Id": 2,
		"Value": "УСН",
		"Name": "USN"
	}
]')
on conflict (referenceid) do update set
"description"='Налог', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='VatType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Нет данных",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "НДС включен",
		"Name": "NDS"
	},
	{
		"Id": 2,
		"Value": "УСН",
		"Name": "USN"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(200, 'Статусы единицы оценки', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoUnitStatus', NULL, NULL)
on conflict (referenceid) do update set
"description"='Статусы единицы оценки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoUnitStatus', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(201, 'Тип статьи', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoNoteType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Ежедневка",
		"Name": "Day"
	},
	{
		"Id": 3,
		"Value": "Годовые",
		"Name": "Year"
	},
	{
		"Id": 4,
		"Value": "Исходный перечень",
		"Name": "Initial"
	},
]')
on conflict (referenceid) do update set
"description"='Тип статьи', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoNoteType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Ежедневка",
		"Name": "Day"
	},
	{
		"Id": 3,
		"Value": "Годовые",
		"Name": "Year"
	},
	{
		"Id": 4,
		"Value": "Исходный перечень",
		"Name": "Initial"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(202, 'Статусы заданий на оценку', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoTaskStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "В работе",
		"Name": "InWork"
	},
	{
		"Id": 2,
		"Value": "Готово",
		"Name": "Ready"
	},
]')
on conflict (referenceid) do update set
"description"='Статусы заданий на оценку', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoTaskStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "В работе",
		"Name": "InWork"
	},
	{
		"Id": 2,
		"Value": "Готово",
		"Name": "Ready"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(203, 'Тип документа', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoDocType', NULL, NULL)
on conflict (referenceid) do update set
"description"='Тип документа', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoDocType', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(204, 'Механизм группировки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoGroupAlgoritm', NULL, '[
	{
		"Id": 1,
		"Value": "Моделирование",
		"Name": "Model"
	},
	{
		"Id": 2,
		"Value": "Затратный",
		"Name": "Cost"
	},
	{
		"Id": 3,
		"Value": "Нормативный",
		"Name": "Normative"
	},
	{
		"Id": 8,
		"Value": "Здания по помещениям",
		"Name": "BuildingOnFlat"
	},
	{
		"Id": 9,
		"Value": "Помещения по зданиям",
		"Name": "FlatOnBuilding"
	},
	{
		"Id": 10,
		"Value": "Среднее",
		"Name": "AVG"
	},
	{
		"Id": 11,
		"Value": "ОНС",
		"Name": "UnComplited"
	},
	{
		"Id": 12,
		"Value": "Минимальное",
		"Name": "Min"
	},
	{
		"Id": 13,
		"Value": "Эталонный",
		"Name": "Etalon"
	},
	{
		"Id": 98,
		"Value": "Основная группа ОКС",
		"Name": "MainOKS"
	},
	{
		"Id": 99,
		"Value": "Основная группа Участки",
		"Name": "MainParcel"
	}
]')
on conflict (referenceid) do update set
"description"='Механизм группировки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoGroupAlgoritm', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Моделирование",
		"Name": "Model"
	},
	{
		"Id": 2,
		"Value": "Затратный",
		"Name": "Cost"
	},
	{
		"Id": 3,
		"Value": "Нормативный",
		"Name": "Normative"
	},
	{
		"Id": 8,
		"Value": "Здания по помещениям",
		"Name": "BuildingOnFlat"
	},
	{
		"Id": 9,
		"Value": "Помещения по зданиям",
		"Name": "FlatOnBuilding"
	},
	{
		"Id": 10,
		"Value": "Среднее",
		"Name": "AVG"
	},
	{
		"Id": 11,
		"Value": "ОНС",
		"Name": "UnComplited"
	},
	{
		"Id": 12,
		"Value": "Минимальное",
		"Name": "Min"
	},
	{
		"Id": 13,
		"Value": "Эталонный",
		"Name": "Etalon"
	},
	{
		"Id": 98,
		"Value": "Основная группа ОКС",
		"Name": "MainOKS"
	},
	{
		"Id": 99,
		"Value": "Основная группа Участки",
		"Name": "MainParcel"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(205, 'Алгоритм рассчёта', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoAlgoritmType', NULL, '[
               {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Экспоненциальная",
		"Name": "Exp"
	},
	{
		"Id": 2,
		"Value": "Линейная",
		"Name": "Line"
	},
	{
		"Id": 3,
		"Value": "Мультипликативная",
		"Name": "Multi"
	},
]')
on conflict (referenceid) do update set
"description"='Алгоритм рассчёта', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoAlgoritmType', "register_id"=NULL, "simple_values"='[
               {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Экспоненциальная",
		"Name": "Exp"
	},
	{
		"Id": 2,
		"Value": "Линейная",
		"Name": "Line"
	},
	{
		"Id": 3,
		"Value": "Мультипликативная",
		"Name": "Multi"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(206, 'Статус модели тура', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoModelStatus', NULL, NULL)
on conflict (referenceid) do update set
"description"='Статус модели тура', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoModelStatus', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(207, 'Тип данных фактора', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoFactorDataType', NULL, NULL)
on conflict (referenceid) do update set
"description"='Тип данных фактора', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoFactorDataType', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(208, 'Результат анализа стоимости', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoStatusResultCalc', NULL, NULL)
on conflict (referenceid) do update set
"description"='Результат анализа стоимости', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoStatusResultCalc', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(209, 'Cтатус расчета', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoStatusRepeatCalc', NULL, NULL)
on conflict (referenceid) do update set
"description"='Cтатус расчета', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoStatusRepeatCalc', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(210, 'Тип объекта, по которому было рассчитано среднее/минимальное значение', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoParentCalcType', NULL, '[
	{
		"Id": 0,
		"Value": "значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "кадастровый квартал",
		"Name": "CadastralBlock"
	},
	{
		"Id": 2,
		"Value": "кадастровый район",
		"Name": "CadastralRegion"
	},
	{
		"Id": 3,
		"Value": "субъект РФ",
		"Name": "RfSubject"
	},
]')
on conflict (referenceid) do update set
"description"='Тип объекта, по которому было рассчитано среднее/минимальное значение', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoParentCalcType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "кадастровый квартал",
		"Name": "CadastralBlock"
	},
	{
		"Id": 2,
		"Value": "кадастровый район",
		"Name": "CadastralRegion"
	},
	{
		"Id": 3,
		"Value": "субъект РФ",
		"Name": "RfSubject"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(211, 'Тип помещения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoTypeOfRoom', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Жилое",
		"Name": "Residential"
	},
	{
		"Id": 2,
		"Value": "Нежилое",
		"Name": "NotResidential"
	},
]')
on conflict (referenceid) do update set
"description"='Тип помещения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoTypeOfRoom', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Жилое",
		"Name": "Residential"
	},
	{
		"Id": 2,
		"Value": "Нежилое",
		"Name": "NotResidential"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(212, 'Статус изменения единицы оценки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoChangeStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Тип_объекта",
		"Name": "TypeObject"
	},
	{
		"Id": 2,
		"Value": "Площадь",
		"Name": "Square"
	},
	{
		"Id": 3,
		"Value": "Номер ЗУ",
		"Name": "NumberParcel"
	},
	{
		"Id": 4,
		"Value": "Наименование",
		"Name": "Name"
	},
	{
		"Id": 5,
		"Value": "Назначение",
		"Name": "Assignment"
	},
	{
		"Id": 6,
		"Value": "Адрес",
		"Name": "Adress"
	},
	{
		"Id": 7,
		"Value": "Местоположение",
		"Name": "Place"
	},
	{
		"Id": 8,
		"Value": "Год постройки",
		"Name": "YearBuild"
	},
	{
		"Id": 9,
		"Value": "Год ввода в эксплуатацию",
		"Name": "YearUse"
	},
	{
		"Id": 10,
		"Value": "Этажность",
		"Name": "Floors"
	},
	{
		"Id": 11,
		"Value": "Подземная этажность",
		"Name": "DownFloors"
	},
	{
		"Id": 12,
		"Value": "Кадастровый квартал",
		"Name": "CadastralBlock"
	},
	{
		"Id": 13,
		"Value": "Материал стен",
		"Name": "Walls"
	},
	{
		"Id": 14,
		"Value": "Кадастровый номер здания",
		"Name": "CadastralBuilding"
	},
	{
		"Id": 15,
		"Value": "Этаж",
		"Name": "NumberFloor"
	},
	{
		"Id": 16,
		"Value": "Характеристика",
		"Name": "Parameter"
	},
	{
		"Id": 17,
		"Value": "Процент готовности",
		"Name": "Procent"
	},
	{
		"Id": 18,
		"Value": "Вид использования",
		"Name": "Use"
	},
	{
		"Id": 19,
		"Value": "Категория",
		"Name": "Category"
	},
	{
		"Id": 20,
		"Value": "Обращение",
		"Name": "Appeal"
	},
	{
		"Id": 21,
		"Value": "Добавление нового объекта",
		"Name": "NewObjectAddition"
	}
]')
on conflict (referenceid) do update set
"description"='Статус изменения единицы оценки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoChangeStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Тип_объекта",
		"Name": "TypeObject"
	},
	{
		"Id": 2,
		"Value": "Площадь",
		"Name": "Square"
	},
	{
		"Id": 3,
		"Value": "Номер ЗУ",
		"Name": "NumberParcel"
	},
	{
		"Id": 4,
		"Value": "Наименование",
		"Name": "Name"
	},
	{
		"Id": 5,
		"Value": "Назначение",
		"Name": "Assignment"
	},
	{
		"Id": 6,
		"Value": "Адрес",
		"Name": "Adress"
	},
	{
		"Id": 7,
		"Value": "Местоположение",
		"Name": "Place"
	},
	{
		"Id": 8,
		"Value": "Год постройки",
		"Name": "YearBuild"
	},
	{
		"Id": 9,
		"Value": "Год ввода в эксплуатацию",
		"Name": "YearUse"
	},
	{
		"Id": 10,
		"Value": "Этажность",
		"Name": "Floors"
	},
	{
		"Id": 11,
		"Value": "Подземная этажность",
		"Name": "DownFloors"
	},
	{
		"Id": 12,
		"Value": "Кадастровый квартал",
		"Name": "CadastralBlock"
	},
	{
		"Id": 13,
		"Value": "Материал стен",
		"Name": "Walls"
	},
	{
		"Id": 14,
		"Value": "Кадастровый номер здания",
		"Name": "CadastralBuilding"
	},
	{
		"Id": 15,
		"Value": "Этаж",
		"Name": "NumberFloor"
	},
	{
		"Id": 16,
		"Value": "Характеристика",
		"Name": "Parameter"
	},
	{
		"Id": 17,
		"Value": "Процент готовности",
		"Name": "Procent"
	},
	{
		"Id": 18,
		"Value": "Вид использования",
		"Name": "Use"
	},
	{
		"Id": 19,
		"Value": "Категория",
		"Name": "Category"
	},
	{
		"Id": 20,
		"Value": "Обращение",
		"Name": "Appeal"
	},
	{
		"Id": 21,
		"Value": "Добавление нового объекта",
		"Name": "NewObjectAddition"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(213, 'Тип расчета', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoCalculationType', NULL, NULL)
on conflict (referenceid) do update set
"description"='Тип расчета', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoCalculationType', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(214, 'Метод расчета', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoCalculationMethod', NULL, NULL)
on conflict (referenceid) do update set
"description"='Метод расчета', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoCalculationMethod', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(215, 'Тип использования атрибута', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoAttributeUsingType', NULL, '[
	{
		"Id": 1,
		"Value": "Атрибут кода группы",
		"Name": "CodeGroupAttribute"
	},
	{
		"Id": 2,
		"Value": "Атрибут кадастрового квартала",
		"Name": "CodeQuarterAttribute"
	}
]')
on conflict (referenceid) do update set
"description"='Тип использования атрибута', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoAttributeUsingType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Атрибут кода группы",
		"Name": "CodeGroupAttribute"
	},
	{
		"Id": 2,
		"Value": "Атрибут кадастрового квартала",
		"Name": "CodeQuarterAttribute"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(216, 'Вид отчета', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoReportType', NULL, NULL)
on conflict (referenceid) do update set
"description"='Вид отчета', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoReportType', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(217, 'Тип территории', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'TerritoryType', NULL, NULL)
on conflict (referenceid) do update set
"description"='Тип территории', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='TerritoryType', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(218, 'Способ моделирования', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KoModelingWay', NULL, NULL)
on conflict (referenceid) do update set
"description"='Способ моделирования', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KoModelingWay', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(220, 'Тип выгрузки результатов кадастровой оценки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoUnloadResultType', NULL, '[   {
      "Id": 0,
      "Value": "Тип не указан",
      "Name": "None"
  },
  {
      "Id": 1,
      "Value": "Выгрузка изменений",
      "Name": "UnloadChange"
  },
  {
      "Id": 2,
      "Value": "Выгрузка истории по объектам",
      "Name": "UnloadHistory"
  },
  {
      "Id": 3,
      "Value": "Таблица 4. Группировка объектов недвижимости",
      "Name": "UnloadTable04"
  },
  {
      "Id": 4,
      "Value": "Таблица 5. Результаты моделирования",
      "Name": "UnloadTable05"
  },
  {
      "Id": 5,
      "Value": "Таблица 7. Обобщенные показатели по кадастровым районам",
      "Name": "UnloadTable07"
  },
  {
      "Id": 6,
      "Value": "Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам",
      "Name": "UnloadTable08"
  },
  {
      "Id": 7,
      "Value": "Таблица 9. Результаты определения кадастровой стоимости",
      "Name": "UnloadTable09"
  },
  {
      "Id": 8,
      "Value": "Таблица 10. Результаты государственной кадастровой оценки",
      "Name": "UnloadTable10"
  },
  {
      "Id": 9,
      "Value": "Таблица 11. Сводные результаты по кадастровому району",
      "Name": "UnloadTable11"
  },
  {
      "Id": 10,
      "Value": "Выгрузка в XML результатов Кадастровой оценки по объектам",
      "Name": "UnloadXML1"
  },
  {
      "Id": 11,
      "Value": "Выгрузка в XML результатов Кадастровой оценки по группам",
      "Name": "UnloadXML2"
  },
  {
      "Id": 12,
      "Value": "Выгрузка в XML результатов Кадастровой оценки по исходящим документам",
      "Name": "UnloadDEKOResponseDocExportToXml"
  },
  {
      "Id": 13,
      "Value": "Выгрузка в XML результатов Кадастровой оценки для ВУОН",
      "Name": "UnloadDEKOVuonExportToXml"
  }
]')
on conflict (referenceid) do update set
"description"='Тип выгрузки результатов кадастровой оценки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoUnloadResultType', "register_id"=NULL, "simple_values"='[   {
      "Id": 0,
      "Value": "Тип не указан",
      "Name": "None"
  },
  {
      "Id": 1,
      "Value": "Выгрузка изменений",
      "Name": "UnloadChange"
  },
  {
      "Id": 2,
      "Value": "Выгрузка истории по объектам",
      "Name": "UnloadHistory"
  },
  {
      "Id": 3,
      "Value": "Таблица 4. Группировка объектов недвижимости",
      "Name": "UnloadTable04"
  },
  {
      "Id": 4,
      "Value": "Таблица 5. Результаты моделирования",
      "Name": "UnloadTable05"
  },
  {
      "Id": 5,
      "Value": "Таблица 7. Обобщенные показатели по кадастровым районам",
      "Name": "UnloadTable07"
  },
  {
      "Id": 6,
      "Value": "Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам",
      "Name": "UnloadTable08"
  },
  {
      "Id": 7,
      "Value": "Таблица 9. Результаты определения кадастровой стоимости",
      "Name": "UnloadTable09"
  },
  {
      "Id": 8,
      "Value": "Таблица 10. Результаты государственной кадастровой оценки",
      "Name": "UnloadTable10"
  },
  {
      "Id": 9,
      "Value": "Таблица 11. Сводные результаты по кадастровому району",
      "Name": "UnloadTable11"
  },
  {
      "Id": 10,
      "Value": "Выгрузка в XML результатов Кадастровой оценки по объектам",
      "Name": "UnloadXML1"
  },
  {
      "Id": 11,
      "Value": "Выгрузка в XML результатов Кадастровой оценки по группам",
      "Name": "UnloadXML2"
  },
  {
      "Id": 12,
      "Value": "Выгрузка в XML результатов Кадастровой оценки по исходящим документам",
      "Name": "UnloadDEKOResponseDocExportToXml"
  },
  {
      "Id": 13,
      "Value": "Выгрузка в XML результатов Кадастровой оценки для ВУОН",
      "Name": "UnloadDEKOVuonExportToXml"
  }
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(221, 'Статусы обновления Единииц оценки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'UnitUpdateStatus', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Новый",
		"Name": "New"
	},
	{
		"Id": 2,
		"Value": "Без изменений",
		"Name": "WithoutChanges"
	},
	{
		"Id": 3,
		"Value": "Изменение группы",
		"Name": "GroupChange"
	},
	{
		"Id": 4,
		"Value": "Изменение характеристик ЕГРН",
		"Name": "EgrnChanges"
	},
	{
		"Id": 5,
		"Value": "Изменение ФС",
		"Name": "FsChange"
	},
	{
		"Id": 6,
		"Value": "Изменение группы, Изменение ФС",
		"Name": "GroupAndFsChange"
	},
	{
		"Id": 7,
		"Value": "Изменение группы, Изменение ЕГРН",
		"Name": "GroupAndEgrnChange"
	},
	{
		"Id": 8,
		"Value": "Изменение группы, Изменение ФС, Изменение характеристик ЕГРН",
		"Name": "GroupAndFsAndEgrnChanges"
	},
]')
on conflict (referenceid) do update set
"description"='Статусы обновления Единииц оценки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='UnitUpdateStatus', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Новый",
		"Name": "New"
	},
	{
		"Id": 2,
		"Value": "Без изменений",
		"Name": "WithoutChanges"
	},
	{
		"Id": 3,
		"Value": "Изменение группы",
		"Name": "GroupChange"
	},
	{
		"Id": 4,
		"Value": "Изменение характеристик ЕГРН",
		"Name": "EgrnChanges"
	},
	{
		"Id": 5,
		"Value": "Изменение ФС",
		"Name": "FsChange"
	},
	{
		"Id": 6,
		"Value": "Изменение группы, Изменение ФС",
		"Name": "GroupAndFsChange"
	},
	{
		"Id": 7,
		"Value": "Изменение группы, Изменение ЕГРН",
		"Name": "GroupAndEgrnChange"
	},
	{
		"Id": 8,
		"Value": "Изменение группы, Изменение ФС, Изменение характеристик ЕГРН",
		"Name": "GroupAndFsAndEgrnChanges"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(222, 'Тип модели', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoModelType', NULL, '[
              {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
               {
		"Id": 1,
		"Value": "Ручное вычисление",
		"Name": "Manual"
	},
	{
		"Id": 2,
		"Value": "Автоматический расчет",
		"Name": "Automatic"
	}
]')
on conflict (referenceid) do update set
"description"='Тип модели', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoModelType', "register_id"=NULL, "simple_values"='[
              {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
               {
		"Id": 1,
		"Value": "Ручное вычисление",
		"Name": "Manual"
	},
	{
		"Id": 2,
		"Value": "Автоматический расчет",
		"Name": "Automatic"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(223, 'Тип атрибута (для различных системных настроек)', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoAttributeTypeForSettings', NULL, '[
	{
		"Id": 1,
		"Value": "Атрибут кадастрового квартала",
		"Name": "CadastralQuarter"
	},
	{
		"Id": 2,
		"Value": "Атрибут кадастрового номера здания",
		"Name": "BuildingCadastralNumber"
	},
	{
		"Id": 3,
		"Value": "Атрибут оценочной группы",
		"Name": "EvaluativeGroup"
	}
]')
on conflict (referenceid) do update set
"description"='Тип атрибута (для различных системных настроек)', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoAttributeTypeForSettings', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Атрибут кадастрового квартала",
		"Name": "CadastralQuarter"
	},
	{
		"Id": 2,
		"Value": "Атрибут кадастрового номера здания",
		"Name": "BuildingCadastralNumber"
	},
	{
		"Id": 3,
		"Value": "Атрибут оценочной группы",
		"Name": "EvaluativeGroup"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(224, 'Статус после сравнения протоколов загрузки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoDataComparingTaskChangesStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Проверка не проводилась",
		"Name": "ComparingWasNotPerformed"
	},
	{
		"Id": 1,
		"Value": "Данные совпадают",
		"Name": "DataAreMatch"
	},
	{
		"Id": 2,
		"Value": "Имеются расхождения",
		"Name": "ThereAreInconsistencies"
	}
]')
on conflict (referenceid) do update set
"description"='Статус после сравнения протоколов загрузки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoDataComparingTaskChangesStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Проверка не проводилась",
		"Name": "ComparingWasNotPerformed"
	},
	{
		"Id": 1,
		"Value": "Данные совпадают",
		"Name": "DataAreMatch"
	},
	{
		"Id": 2,
		"Value": "Имеются расхождения",
		"Name": "ThereAreInconsistencies"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(225, 'Статус после сравнения протоколов КС', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'KoDataComparingCadastralCostStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Проверка не проводилась",
		"Name": "ComparingWasNotPerformed"
	},
	{
		"Id": 1,
		"Value": "Данные совпадают",
		"Name": "DataAreMatch"
	},
	{
		"Id": 2,
		"Value": "Наборы данных для сравнения не совпадают",
		"Name": "ThereAreUnitSetsInconsistencies"
	},
	{
		"Id": 3,
		"Value": "Имеются несоответствия в кадастровой стоимости",
		"Name": "ThereAreUnitCostsInconsistencies"
	}
]')
on conflict (referenceid) do update set
"description"='Статус после сравнения протоколов КС', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='KoDataComparingCadastralCostStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Проверка не проводилась",
		"Name": "ComparingWasNotPerformed"
	},
	{
		"Id": 1,
		"Value": "Данные совпадают",
		"Name": "DataAreMatch"
	},
	{
		"Id": 2,
		"Value": "Наборы данных для сравнения не совпадают",
		"Name": "ThereAreUnitSetsInconsistencies"
	},
	{
		"Id": 3,
		"Value": "Имеются несоответствия в кадастровой стоимости",
		"Name": "ThereAreUnitCostsInconsistencies"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(226, 'Тип метки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Ko.MarkType', NULL, '[
	{
		"Id": 0,
		"Value": "Не использовать метку",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По умолчанию",
		"Name": "Default"
	},
	{
		"Id": 2,
		"Value": "Прямая метка",
		"Name": "Straight"
	},
	{
		"Id": 3,
		"Value": "Обратная метка",
		"Name": "Reverse"
	}
]')
on conflict (referenceid) do update set
"description"='Тип метки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Ko.MarkType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Не использовать метку",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По умолчанию",
		"Name": "Default"
	},
	{
		"Id": 2,
		"Value": "Прямая метка",
		"Name": "Straight"
	},
	{
		"Id": 3,
		"Value": "Обратная метка",
		"Name": "Reverse"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(300, 'Тип объекта', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Sud.SudObjectType', NULL, '[
	{
		"Id": 1,
		"Value": "Участок",
		"Name": "Site"
	},
	{
		"Id": 2,
		"Value": "Здание",
		"Name": "Building"
	},
	{
		"Id": 3,
		"Value": "Помещение",
		"Name": "Room"
	},
	{
		"Id": 4,
		"Value": "Сооружение",
		"Name": "Construction"
	},
	{
		"Id": 5,
		"Value": "Онс",
		"Name": "Ons"
	},
	{
		"Id": 6,
		"Value": "Машиноместо",
		"Name": "ParkingPlace"
	},
	{
		"Id": 7,
		"Value": "Нет данных",
		"Name": "None"
	}
]')
on conflict (referenceid) do update set
"description"='Тип объекта', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Sud.SudObjectType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Участок",
		"Name": "Site"
	},
	{
		"Id": 2,
		"Value": "Здание",
		"Name": "Building"
	},
	{
		"Id": 3,
		"Value": "Помещение",
		"Name": "Room"
	},
	{
		"Id": 4,
		"Value": "Сооружение",
		"Name": "Construction"
	},
	{
		"Id": 5,
		"Value": "Онс",
		"Name": "Ons"
	},
	{
		"Id": 6,
		"Value": "Машиноместо",
		"Name": "ParkingPlace"
	},
	{
		"Id": 7,
		"Value": "Нет данных",
		"Name": "None"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(301, 'Статус обработки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Sud.ProcessingStatus', NULL, '[
	{
		"Id": 0,
		"Value": "В работе",
		"Name": "InWork"
	},
	{
		"Id": 1,
		"Value": "Актуальный",
		"Name": "Processed"
	},
]')
on conflict (referenceid) do update set
"description"='Статус обработки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Sud.ProcessingStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "В работе",
		"Name": "InWork"
	},
	{
		"Id": 1,
		"Value": "Актуальный",
		"Name": "Processed"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(302, 'Тип заявителя', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Sud.ApplicantType', NULL, '[
	{
		"Id": 1,
		"Value": "Физическое лицо",
		"Name": "Individual"
	},
	{
		"Id": 2,
		"Value": "Юридическое лицо",
		"Name": "Entity"
	}
]')
on conflict (referenceid) do update set
"description"='Тип заявителя', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Sud.ApplicantType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Физическое лицо",
		"Name": "Individual"
	},
	{
		"Id": 2,
		"Value": "Юридическое лицо",
		"Name": "Entity"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(303, 'Форма собственности', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Sud.TypeOfOwnership', NULL, '[
	{
		"Id": 1,
		"Value": "Федеральное имущество",
		"Name": "FederalProperty"
	},
	{
		"Id": 2,
		"Value": "Город Москва",
		"Name": "MoscowCity"
	},
	{
		"Id": 3,
		"Value": "Иное",
		"Name": "Other"
	}
]')
on conflict (referenceid) do update set
"description"='Форма собственности', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Sud.TypeOfOwnership', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Федеральное имущество",
		"Name": "FederalProperty"
	},
	{
		"Id": 2,
		"Value": "Город Москва",
		"Name": "MoscowCity"
	},
	{
		"Id": 3,
		"Value": "Иное",
		"Name": "Other"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(304, 'Статус судебного решения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Sud.CourtStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Без статуса",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Отказано",
		"Name": "Denied"
	},
	{
		"Id": 2,
		"Value": "Удовлетворено",
		"Name": "Satisfied"
	},
	{
		"Id": 3,
		"Value": "Приостановлено",
		"Name": "Paused"
	},
	{
		"Id": 4,
		"Value": "Частично удовлетворено",
		"Name": "PartiallySatisfied"
	}
]')
on conflict (referenceid) do update set
"description"='Статус судебного решения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Sud.CourtStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Без статуса",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Отказано",
		"Name": "Denied"
	},
	{
		"Id": 2,
		"Value": "Удовлетворено",
		"Name": "Satisfied"
	},
	{
		"Id": 3,
		"Value": "Приостановлено",
		"Name": "Paused"
	},
	{
		"Id": 4,
		"Value": "Частично удовлетворено",
		"Name": "PartiallySatisfied"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(400, 'Тип комиссии', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Commission.CommissionType', NULL, '[
	{
		"Id": 1,
		"Value": "По установлению кадастровой стоимости",
		"Name": "OnSetCadCost"
	},
	{
		"Id": 2,
		"Value": "По недостоверности",
		"Name": "OnUnreliability"
	},
]')
on conflict (referenceid) do update set
"description"='Тип комиссии', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Commission.CommissionType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "По установлению кадастровой стоимости",
		"Name": "OnSetCadCost"
	},
	{
		"Id": 2,
		"Value": "По недостоверности",
		"Name": "OnUnreliability"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(401, 'Статус заявителя', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Commission.ApplicantStatus', NULL, '[
	{
		"Id": 0,
		"Value": "ФЛ",
		"Name": "FL"
	},
	{
		"Id": 1,
		"Value": "ЮЛ",
		"Name": "UL"
	},
	{
		"Id": 2,
		"Value": "ДГИ",
		"Name": "DGI"
	},
	{
		"Id": 3,
		"Value": "ИП",
		"Name": "IP"
	},
	{
		"Id": 4,
		"Value": "ОГВ",
		"Name": "OGV"
	},
	{
		"Id": 5,
		"Value": "ФЛ, ЮЛ",
		"Name": "FlUl"
	},
	{
		"Id": 6,
		"Value": "ФЛ, ЮЛ, ИП",
		"Name": "FlUlIp"
	},
	{
		"Id": 7,
		"Value": "Нет данных",
		"Name": "NoData"
	},
]')
on conflict (referenceid) do update set
"description"='Статус заявителя', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Commission.ApplicantStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "ФЛ",
		"Name": "FL"
	},
	{
		"Id": 1,
		"Value": "ЮЛ",
		"Name": "UL"
	},
	{
		"Id": 2,
		"Value": "ДГИ",
		"Name": "DGI"
	},
	{
		"Id": 3,
		"Value": "ИП",
		"Name": "IP"
	},
	{
		"Id": 4,
		"Value": "ОГВ",
		"Name": "OGV"
	},
	{
		"Id": 5,
		"Value": "ФЛ, ЮЛ",
		"Name": "FlUl"
	},
	{
		"Id": 6,
		"Value": "ФЛ, ЮЛ, ИП",
		"Name": "FlUlIp"
	},
	{
		"Id": 7,
		"Value": "Нет данных",
		"Name": "NoData"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(402, 'Решение комиссии', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Commission.DecisionResult', NULL, '[
	{
		"Id": 1,
		"Value": "Положительное решение",
		"Name": "Approved"
	},
	{
		"Id": 2,
		"Value": "Отказано",
		"Name": "Rejected"
	},
]')
on conflict (referenceid) do update set
"description"='Решение комиссии', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Commission.DecisionResult', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Положительное решение",
		"Name": "Approved"
	},
	{
		"Id": 2,
		"Value": "Отказано",
		"Name": "Rejected"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(500, 'Наличие характеристики', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.HarAvailability', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "отсутствует",
		"Name": "NotExists"
	},
	{
		"Id": 2,
		"Value": "имеется",
		"Name": "Exists"
	},
]')
on conflict (referenceid) do update set
"description"='Наличие характеристики', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.HarAvailability', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "отсутствует",
		"Name": "NotExists"
	},
	{
		"Id": 2,
		"Value": "имеется",
		"Name": "Exists"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(501, 'Статус книги', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.BookStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "В работе",
		"Name": "InWork"
	},
	{
		"Id": 2,
		"Value": "Закрыто",
		"Name": "Closed"
	},
]')
on conflict (referenceid) do update set
"description"='Статус книги', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.BookStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "В работе",
		"Name": "InWork"
	},
	{
		"Id": 2,
		"Value": "Закрыто",
		"Name": "Closed"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(502, 'Тип книги', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.BookType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Книга деклараций",
		"Name": "Declarations"
	},
	{
		"Id": 2,
		"Value": "Книга уведомлений",
		"Name": "Notifications"
	},
]')
on conflict (referenceid) do update set
"description"='Тип книги', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.BookType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Книга деклараций",
		"Name": "Declarations"
	},
	{
		"Id": 2,
		"Value": "Книга уведомлений",
		"Name": "Notifications"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(503, 'Тип субъекта деклараций', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.SubjectType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Физлицо",
		"Name": "Fl"
	},
	{
		"Id": 2,
		"Value": "Юрлицо",
		"Name": "Ul"
	},
]')
on conflict (referenceid) do update set
"description"='Тип субъекта деклараций', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.SubjectType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Физлицо",
		"Name": "Fl"
	},
	{
		"Id": 2,
		"Value": "Юрлицо",
		"Name": "Ul"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(504, 'Тип уведомления', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.UvedType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Уведомление о принятии декларации",
		"Name": "Item1"
	},
	{
		"Id": 3,
		"Value": "Уведомление об учете информации из декларации",
		"Name": "Item3"
	},
	{
		"Id": 4,
		"Value": "Уведомление об отказе в учете информации из декларации",
		"Name": "Item4"
	},
	{
		"Id": 5,
		"Value": "Уведомление об отказе в рассмотрении декларации и возврате документов",
		"Name": "Item5"
	},
]')
on conflict (referenceid) do update set
"description"='Тип уведомления', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.UvedType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Уведомление о принятии декларации",
		"Name": "Item1"
	},
	{
		"Id": 3,
		"Value": "Уведомление об учете информации из декларации",
		"Name": "Item3"
	},
	{
		"Id": 4,
		"Value": "Уведомление об отказе в учете информации из декларации",
		"Name": "Item4"
	},
	{
		"Id": 5,
		"Value": "Уведомление об отказе в рассмотрении декларации и возврате документов",
		"Name": "Item5"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(505, 'Статус результата', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.StatusDec', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Отказ в рассмотрении",
		"Name": "Rejection"
	},
	{
		"Id": 2,
		"Value": "Принято на рассмотрение",
		"Name": "Accepted"
	},
	{
		"Id": 3,
		"Value": "Рассмотрено",
		"Name": "Considered"
	},
]')
on conflict (referenceid) do update set
"description"='Статус результата', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.StatusDec', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Отказ в рассмотрении",
		"Name": "Rejection"
	},
	{
		"Id": 2,
		"Value": "Принято на рассмотрение",
		"Name": "Accepted"
	},
	{
		"Id": 3,
		"Value": "Рассмотрено",
		"Name": "Considered"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(506, 'Тип объекта декларации', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.ObjectType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Земельный участок",
		"Name": "Site"
	},
	{
		"Id": 2,
		"Value": "Здание",
		"Name": "Building"
	},
	{
		"Id": 3,
		"Value": "Помещение",
		"Name": "Room"
	},
	{
		"Id": 4,
		"Value": "Сооружение",
		"Name": "Construction"
	},
	{
		"Id": 5,
		"Value": "Машино-место",
		"Name": "ParkingPlace"
	},
	{
		"Id": 6,
		"Value": "ОНС",
		"Name": "Ons"
	},
	{
		"Id": 7,
		"Value": "Единый недвижимый комплекс",
		"Name": "Ens"
	},
	{
		"Id": 8,
		"Value": "Производственно-имущественный комплекс",
		"Name": "Pik"
	},
	{
		"Id": 9,
		"Value": "Иное",
		"Name": "Other"
	},
]')
on conflict (referenceid) do update set
"description"='Тип объекта декларации', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.ObjectType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Земельный участок",
		"Name": "Site"
	},
	{
		"Id": 2,
		"Value": "Здание",
		"Name": "Building"
	},
	{
		"Id": 3,
		"Value": "Помещение",
		"Name": "Room"
	},
	{
		"Id": 4,
		"Value": "Сооружение",
		"Name": "Construction"
	},
	{
		"Id": 5,
		"Value": "Машино-место",
		"Name": "ParkingPlace"
	},
	{
		"Id": 6,
		"Value": "ОНС",
		"Name": "Ons"
	},
	{
		"Id": 7,
		"Value": "Единый недвижимый комплекс",
		"Name": "Ens"
	},
	{
		"Id": 8,
		"Value": "Производственно-имущественный комплекс",
		"Name": "Pik"
	},
	{
		"Id": 9,
		"Value": "Иное",
		"Name": "Other"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(507, 'Статус проверки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.CheckStatus', NULL, '[
	{
		"Id": 0,
		"Value": "В работе",
		"Name": "InWork"
	},
	{
		"Id": 1,
		"Value": "Да",
		"Name": "Yes"
	},
	{
		"Id": 2,
		"Value": "Нет",
		"Name": "No"
	},
]')
on conflict (referenceid) do update set
"description"='Статус проверки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.CheckStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "В работе",
		"Name": "InWork"
	},
	{
		"Id": 1,
		"Value": "Да",
		"Name": "Yes"
	},
	{
		"Id": 2,
		"Value": "Нет",
		"Name": "No"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(508, 'Цель декларации', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.DeclarationPurpose', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Декларация подается с целью доведения информации о характеристиках объекта недвижимости",
		"Name": "Item1"
	},
	{
		"Id": 2,
		"Value": "Декларация подается с целью предоставления отчета об определении рыночной стоимости объекта недвижимости",
		"Name": "Item2"
	},
]')
on conflict (referenceid) do update set
"description"='Цель декларации', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.DeclarationPurpose', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Декларация подается с целью доведения информации о характеристиках объекта недвижимости",
		"Name": "Item1"
	},
	{
		"Id": 2,
		"Value": "Декларация подается с целью предоставления отчета об определении рыночной стоимости объекта недвижимости",
		"Name": "Item2"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(509, 'Тип отправки уведомления', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.SendUvedType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Нет",
		"Name": "No"
	},
	{
		"Id": 2,
		"Value": "На почтовый адрес",
		"Name": "Post"
	},
	{
		"Id": 3,
		"Value": "На электронный адрес",
		"Name": "Email"
	},
	{
		"Id": 4,
		"Value": "На руки",
		"Name": "OnHands"
	},
]')
on conflict (referenceid) do update set
"description"='Тип отправки уведомления', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.SendUvedType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Нет",
		"Name": "No"
	},
	{
		"Id": 2,
		"Value": "На почтовый адрес",
		"Name": "Post"
	},
	{
		"Id": 3,
		"Value": "На электронный адрес",
		"Name": "Email"
	},
	{
		"Id": 4,
		"Value": "На руки",
		"Name": "OnHands"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(510, 'Тип владельца', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.OwnerType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Заявитель (правообладатель)",
		"Name": "Item1"
	},
	{
		"Id": 2,
		"Value": "Представитель заявителя",
		"Name": "Item2"
	},
]')
on conflict (referenceid) do update set
"description"='Тип владельца', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.OwnerType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Заявитель (правообладатель)",
		"Name": "Item1"
	},
	{
		"Id": 2,
		"Value": "Представитель заявителя",
		"Name": "Item2"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(511, 'Тип причины отказа для уведомлления об отказе декларации', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.RejectionReasonType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Заявитель, подавший декларацию, не является правообладателем объекта недвижимости, в отношении которого подается декларация",
		"Name": "ApplicantIsNotObjectTypeOwner"
	},
	{
		"Id": 2,
		"Value": "К декларации не приложены документы, предусмотренные пунктом 2 Приказа о декларациях",
		"Name": "DocumentsAreNotAttached"
	},
	{
		"Id": 3,
		"Value": "Декларация не соответствует форме, предусмотренной приложением № 2 к Приказу о декларациях",
		"Name": "DeclarationDoesNotMatchForm"
	},
	{
		"Id": 4,
		"Value": "Декларация не заверена в соответствии с пунктом 3 Приказа о декларациях",
		"Name": "DeclarationIsNotCertified "
	},
	{
		"Id": 5,
		"Value": "Декларация и прилагаемые к ней документы представлены не в соответствии с требованиями, предусмотренными пунктом 4 Приказа о декларациях",
		"Name": "DeclarationAndDocumentsDoNotMeetRequirements"
	},
	{
		"Id": 6,
		"Value": "Иное (вручную)",
		"Name": "Other"
	},
]')
on conflict (referenceid) do update set
"description"='Тип причины отказа для уведомлления об отказе декларации', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.RejectionReasonType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Заявитель, подавший декларацию, не является правообладателем объекта недвижимости, в отношении которого подается декларация",
		"Name": "ApplicantIsNotObjectTypeOwner"
	},
	{
		"Id": 2,
		"Value": "К декларации не приложены документы, предусмотренные пунктом 2 Приказа о декларациях",
		"Name": "DocumentsAreNotAttached"
	},
	{
		"Id": 3,
		"Value": "Декларация не соответствует форме, предусмотренной приложением № 2 к Приказу о декларациях",
		"Name": "DeclarationDoesNotMatchForm"
	},
	{
		"Id": 4,
		"Value": "Декларация не заверена в соответствии с пунктом 3 Приказа о декларациях",
		"Name": "DeclarationIsNotCertified "
	},
	{
		"Id": 5,
		"Value": "Декларация и прилагаемые к ней документы представлены не в соответствии с требованиями, предусмотренными пунктом 4 Приказа о декларациях",
		"Name": "DeclarationAndDocumentsDoNotMeetRequirements"
	},
	{
		"Id": 6,
		"Value": "Иное (вручную)",
		"Name": "Other"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(512, 'Статус характеристики', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Declarations.HarStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Принято",
		"Name": "Accepted"
	},
	{
		"Id": 2,
		"Value": "Не принято",
		"Name": "Rejected"
	},
]')
on conflict (referenceid) do update set
"description"='Статус характеристики', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Declarations.HarStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Принято",
		"Name": "Accepted"
	},
	{
		"Id": 2,
		"Value": "Не принято",
		"Name": "Rejected"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(600, 'Тип данных кода справочника', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'ES.ReferenceItemCodeType', NULL, '[
	{
		"Id": 1,
		"Value": "Число",
		"Name": "Number"
	},
	{
		"Id": 4,
		"Value": "Строка",
		"Name": "String"
	},
	{
		"Id": 5,
		"Value": "Дата и время",
		"Name": "Date"
	}
]')
on conflict (referenceid) do update set
"description"='Тип данных кода справочника', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='ES.ReferenceItemCodeType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 1,
		"Value": "Число",
		"Name": "Number"
	},
	{
		"Id": 4,
		"Value": "Строка",
		"Name": "String"
	},
	{
		"Id": 5,
		"Value": "Дата и время",
		"Name": "Date"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(601, 'Сценарий расчета', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'ES.ScenarioType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Расчет ЕОН (ОКС + ЗУ)",
		"Name": "Eon"
	},
	{
		"Id": 2,
		"Value": "Расчет ОКС без доли ЗУ",
		"Name": "Oks"
	},
]')
on conflict (referenceid) do update set
"description"='Сценарий расчета', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='ES.ScenarioType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Расчет ЕОН (ОКС + ЗУ)",
		"Name": "Eon"
	},
	{
		"Id": 2,
		"Value": "Расчет ОКС без доли ЗУ",
		"Name": "Oks"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(800, 'Статус импорта данных', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Common.ImportStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Создана",
		"Name": "Added"
	},
	{
		"Id": 1,
		"Value": "В работе",
		"Name": "Running"
	},
	{
		"Id": 2,
		"Value": "Завершено",
		"Name": "Completed"
	},
	{
		"Id": 3,
		"Value": "Ошибка",
		"Name": "Faulted"
	},
]')
on conflict (referenceid) do update set
"description"='Статус импорта данных', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Common.ImportStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Создана",
		"Name": "Added"
	},
	{
		"Id": 1,
		"Value": "В работе",
		"Name": "Running"
	},
	{
		"Id": 2,
		"Value": "Завершено",
		"Name": "Completed"
	},
	{
		"Id": 3,
		"Value": "Ошибка",
		"Name": "Faulted"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(801, 'Тип формы', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Common.DataFormStorege', NULL, '[
    {
        "Id": 1,
        "Name": "Normalisation",
        "Value": "Нормализация"
    },
    {
        "Id": 2,
        "Name": "Harmonization",
        "Value": "Гармонизация"
    },
    {
        "Id": 5,
        "Name": "EstimatedGroup",
        "Value": "Проставление оценочной группы"
    },
    {
        "Id": 6,
        "Name": "TransferAttributesWithoutCreate",
        "Value": "Перенос атрибутов (без создания)"
    },
    {
        "Id": 7,
        "Name": "TransferAttributesWithCreate",
        "Value": "Перенос атрибутов (с созданием)"
    },
    {
        "Id": 8,
        "Name": "Inheritance",
        "Value": "Наследование"
    },
    {
        "Id": 9,
        "Name": "ExportFactorsByTask",
        "Value": "Выгрузка факторов единиц оценки по заданию на оценку"
    },
    {
        "Id": 10,
        "Name": "NormalisationFinal",
        "Value": "Финализация нормализации"
    }
]')
on conflict (referenceid) do update set
"description"='Тип формы', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Common.DataFormStorege', "register_id"=NULL, "simple_values"='[
    {
        "Id": 1,
        "Name": "Normalisation",
        "Value": "Нормализация"
    },
    {
        "Id": 2,
        "Name": "Harmonization",
        "Value": "Гармонизация"
    },
    {
        "Id": 5,
        "Name": "EstimatedGroup",
        "Value": "Проставление оценочной группы"
    },
    {
        "Id": 6,
        "Name": "TransferAttributesWithoutCreate",
        "Value": "Перенос атрибутов (без создания)"
    },
    {
        "Id": 7,
        "Name": "TransferAttributesWithCreate",
        "Value": "Перенос атрибутов (с созданием)"
    },
    {
        "Id": 8,
        "Name": "Inheritance",
        "Value": "Наследование"
    },
    {
        "Id": 9,
        "Name": "ExportFactorsByTask",
        "Value": "Выгрузка факторов единиц оценки по заданию на оценку"
    },
    {
        "Id": 10,
        "Name": "NormalisationFinal",
        "Value": "Финализация нормализации"
    }
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(802, 'Статус экспорта файлов', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Common.ExportStatus', NULL, '[
	{
		"Id": 0,
		"Value": "Создана",
		"Name": "Added"
	},
	{
		"Id": 1,
		"Value": "Запущена",
		"Name": "Running"
	},
	{
		"Id": 2,
		"Value": "Завершена",
		"Name": "Completed"
	},
	{
		"Id": 3,
		"Value": "Ошибка",
		"Name": "Faulted"
	},
]')
on conflict (referenceid) do update set
"description"='Статус экспорта файлов', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Common.ExportStatus', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Создана",
		"Name": "Added"
	},
	{
		"Id": 1,
		"Value": "Запущена",
		"Name": "Running"
	},
	{
		"Id": 2,
		"Value": "Завершена",
		"Name": "Completed"
	},
	{
		"Id": 3,
		"Value": "Ошибка",
		"Name": "Faulted"
	},
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(900, 'Статус процесса службы фоновых процессов', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Core.LongProcess.Status', NULL, '[
	{
		"Id": 0,
		"Value": "Добавлен",
		"Name": "Added"
	},
	{
		"Id": 1,
		"Value": "Подготовлен к запуску",
		"Name": "PrepareToRun"
	},
	{
		"Id": 2,
		"Value": "Выполняется",
		"Name": "Running"
	},
	{
		"Id": 3,
		"Value": "Завершен успешно",
		"Name": "Completed"
	},
	{
		"Id": 4,
		"Value": "Завершен с ошибкой",
		"Name": "Faulted"
	},
	{
		"Id": 5,
		"Value": "Отправлен запрос на остановку",
		"Name": "CancelRequested"
	},
	{
		"Id": 6,
		"Value": "Остановлен",
		"Name": "Stopped"
	}
]')
on conflict (referenceid) do update set
"description"='Статус процесса службы фоновых процессов', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Core.LongProcess.Status', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Добавлен",
		"Name": "Added"
	},
	{
		"Id": 1,
		"Value": "Подготовлен к запуску",
		"Name": "PrepareToRun"
	},
	{
		"Id": 2,
		"Value": "Выполняется",
		"Name": "Running"
	},
	{
		"Id": 3,
		"Value": "Завершен успешно",
		"Name": "Completed"
	},
	{
		"Id": 4,
		"Value": "Завершен с ошибкой",
		"Name": "Faulted"
	},
	{
		"Id": 5,
		"Value": "Отправлен запрос на остановку",
		"Name": "CancelRequested"
	},
	{
		"Id": 6,
		"Value": "Остановлен",
		"Name": "Stopped"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(901, 'Периодичность формирования фоновых выгрузок', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Core.BackgroundExport.SchedulerType', NULL, '[
	{
		"Id": 0,
		"Value": "Каждый день",
		"Name": "EveryDay"
	},
	{
		"Id": 1,
		"Value": "Раз в неделю",
		"Name": "OnceAWeek"
	},
	{
		"Id": 2,
		"Value": "Раз в месяц",
		"Name": "OnceAMonth"
	},
	{
		"Id": 3,
		"Value": "Раз в квартал",
		"Name": "OnceAQuarter"
	},
	{
		"Id": 4,
		"Value": "Раз в год",
		"Name": "OnceAYear"
	}
]')
on conflict (referenceid) do update set
"description"='Периодичность формирования фоновых выгрузок', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Core.BackgroundExport.SchedulerType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Каждый день",
		"Name": "EveryDay"
	},
	{
		"Id": 1,
		"Value": "Раз в неделю",
		"Name": "OnceAWeek"
	},
	{
		"Id": 2,
		"Value": "Раз в месяц",
		"Name": "OnceAMonth"
	},
	{
		"Id": 3,
		"Value": "Раз в квартал",
		"Name": "OnceAQuarter"
	},
	{
		"Id": 4,
		"Value": "Раз в год",
		"Name": "OnceAYear"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(902, 'Тип наследования', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, 'KO.FactorInheritance', NULL, NULL)
on conflict (referenceid) do update set
"description"='Тип наследования', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"='KO.FactorInheritance', "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12072, 'Тип фотографии', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Тип фотографии', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12082, 'Тип образа', 1, 'Core.RefLib.Executors.ReferenceExecutor', NULL, NULL, NULL, NULL, NULL)
on conflict (referenceid) do update set
"description"='Тип образа', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutor', "istree"=NULL, "defaultvalue"=NULL, "name"=NULL, "register_id"=NULL, "simple_values"=NULL;

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12083, 'Линия застройки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'HouseLineType', NULL, '[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Первая",
		"Name": "First"
	},
	{
		"Id": 2,
		"Value": "Вторая",
		"Name": "Second "
	},
	{
		"Id": 3,
		"Value": "Иная",
		"Name": "Other"
	}
]')
on conflict (referenceid) do update set
"description"='Линия застройки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='HouseLineType', "register_id"=NULL, "simple_values"='[
	{
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Первая",
		"Name": "First"
	},
	{
		"Id": 2,
		"Value": "Вторая",
		"Name": "Second "
	},
	{
		"Id": 3,
		"Value": "Иная",
		"Name": "Other"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12084, 'Состояние отделки', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'FinishingCondition', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Требуется косметический ремонт",
		"Name": "CosmeticRepairsRequired"
	},
	{
		"Id": 2,
		"Value": "Дизайнерский ремонт",
		"Name": "Design"
	},
	{
		"Id": 3,
		"Value": "Под чистовую отделку",
		"Name": "Finishing"
	},
	{
		"Id": 4,
		"Value": "Требуется капитальный ремонт",
		"Name": "MajorRepairsRequired"
	},
	{
		"Id": 5,
		"Value": "Офисная отделка",
		"Name": "Office"
	},
	{
		"Id": 6,
		"Value": "Типовой ремонт",
		"Name": "Typical"
	}
]')
on conflict (referenceid) do update set
"description"='Состояние отделки', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='FinishingCondition', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Требуется косметический ремонт",
		"Name": "CosmeticRepairsRequired"
	},
	{
		"Id": 2,
		"Value": "Дизайнерский ремонт",
		"Name": "Design"
	},
	{
		"Id": 3,
		"Value": "Под чистовую отделку",
		"Name": "Finishing"
	},
	{
		"Id": 4,
		"Value": "Требуется капитальный ремонт",
		"Name": "MajorRepairsRequired"
	},
	{
		"Id": 5,
		"Value": "Офисная отделка",
		"Name": "Office"
	},
	{
		"Id": 6,
		"Value": "Типовой ремонт",
		"Name": "Typical"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12085, 'Тип дома', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'HouseType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Газобетонный блок",
		"Name": "AerocreteBlock"
	},
	{
		"Id": 2,
		"Value": "Блочный",
		"Name": "Block"
	},
	{
		"Id": 3,
		"Value": "Щитовой",
		"Name": "Boards"
	},
	{
		"Id": 4,
		"Value": "Кирпичный",
		"Name": "Brick"
	},
	{
		"Id": 5,
		"Value": "Пенобетонный блок",
		"Name": "FoamConcreteBlock"
	},
	{
		"Id": 6,
		"Value": "Газосиликатный блок",
		"Name": "GasSilicateBlock"
	},
	{
		"Id": 7,
		"Value": "Монолитный",
		"Name": "Monolith"
	},
	{
		"Id": 8,
		"Value": "Монолитно-кирпичный",
		"Name": "MonolithBrick"
	},
	{
		"Id": 9,
		"Value": "Старый фонд",
		"Name": "Old"
	},
	{
		"Id": 10,
		"Value": "Панельный",
		"Name": "Panel "
	},
	{
		"Id": 11,
		"Value": "Сталинский",
		"Name": "Stalin"
	},
	{
		"Id": 12,
		"Value": "Каркасный",
		"Name": "Wireframe"
	},
	{
		"Id": 13,
		"Value": "Деревянный",
		"Name": "Wood"
	}
]')
on conflict (referenceid) do update set
"description"='Тип дома', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='HouseType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Газобетонный блок",
		"Name": "AerocreteBlock"
	},
	{
		"Id": 2,
		"Value": "Блочный",
		"Name": "Block"
	},
	{
		"Id": 3,
		"Value": "Щитовой",
		"Name": "Boards"
	},
	{
		"Id": 4,
		"Value": "Кирпичный",
		"Name": "Brick"
	},
	{
		"Id": 5,
		"Value": "Пенобетонный блок",
		"Name": "FoamConcreteBlock"
	},
	{
		"Id": 6,
		"Value": "Газосиликатный блок",
		"Name": "GasSilicateBlock"
	},
	{
		"Id": 7,
		"Value": "Монолитный",
		"Name": "Monolith"
	},
	{
		"Id": 8,
		"Value": "Монолитно-кирпичный",
		"Name": "MonolithBrick"
	},
	{
		"Id": 9,
		"Value": "Старый фонд",
		"Name": "Old"
	},
	{
		"Id": 10,
		"Value": "Панельный",
		"Name": "Panel "
	},
	{
		"Id": 11,
		"Value": "Сталинский",
		"Name": "Stalin"
	},
	{
		"Id": 12,
		"Value": "Каркасный",
		"Name": "Wireframe"
	},
	{
		"Id": 13,
		"Value": "Деревянный",
		"Name": "Wood"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12086, 'Планировка', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'Layout', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Кабинетная",
		"Name": "Cabinet"
	},
	{
		"Id": 2,
		"Value": "Коридорная",
		"Name": "Corridorplan"
	},
	{
		"Id": 3,
		"Value": "Смешанная",
		"Name": "Mixed"
	},
	{
		"Id": 4,
		"Value": "Открытая",
		"Name": "OpenSpace"
	}
]')
on conflict (referenceid) do update set
"description"='Планировка', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='Layout', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Кабинетная",
		"Name": "Cabinet"
	},
	{
		"Id": 2,
		"Value": "Коридорная",
		"Name": "Corridorplan"
	},
	{
		"Id": 3,
		"Value": "Смешанная",
		"Name": "Mixed"
	},
	{
		"Id": 4,
		"Value": "Открытая",
		"Name": "OpenSpace"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12087, 'Вид разрешённого использования', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'PermittedUseType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Cельскохозяйственное использование",
		"Name": "Agricultural"
	},
	{
		"Id": 2,
		"Value": "Деловое управление",
		"Name": "BusinessManagement"
	},
	{
		"Id": 3,
		"Value": "Общее пользование территории",
		"Name": "CommonUseArea"
	},
	{
		"Id": 4,
		"Value": "Высотная застройка",
		"Name": "HighriseBuildings"
	},
	{
		"Id": 5,
		"Value": "Гостиничное обслуживание",
		"Name": "HotelAmenities"
	},
	{
		"Id": 6,
		"Value": "Индивидуальное жилищное строительство (ИЖС)",
		"Name": "IndividualHousingConstruction"
	},
	{
		"Id": 7,
		"Value": "Промышленность",
		"Name": "Industry"
	},
	{
		"Id": 8,
		"Value": "Отдых (рекреация)",
		"Name": "Leisure"
	},
	{
		"Id": 9,
		"Value": "Малоэтажное жилищное строительство (МЖС)",
		"Name": "LowriseHousing"
	},
	{
		"Id": 10,
		"Value": "Общественное использование объектов капитального строительства",
		"Name": "PublicUseOfCapitalConstruction"
	},
	{
		"Id": 11,
		"Value": "Обслуживание автотранспорта",
		"Name": "ServiceVehicles"
	},
	{
		"Id": 12,
		"Value": "Торговые центры",
		"Name": "ShoppingCenters"
	},
	{
		"Id": 13,
		"Value": "Склады",
		"Name": "Warehouses"
	}
]')
on conflict (referenceid) do update set
"description"='Вид разрешённого использования', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='PermittedUseType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Cельскохозяйственное использование",
		"Name": "Agricultural"
	},
	{
		"Id": 2,
		"Value": "Деловое управление",
		"Name": "BusinessManagement"
	},
	{
		"Id": 3,
		"Value": "Общее пользование территории",
		"Name": "CommonUseArea"
	},
	{
		"Id": 4,
		"Value": "Высотная застройка",
		"Name": "HighriseBuildings"
	},
	{
		"Id": 5,
		"Value": "Гостиничное обслуживание",
		"Name": "HotelAmenities"
	},
	{
		"Id": 6,
		"Value": "Индивидуальное жилищное строительство (ИЖС)",
		"Name": "IndividualHousingConstruction"
	},
	{
		"Id": 7,
		"Value": "Промышленность",
		"Name": "Industry"
	},
	{
		"Id": 8,
		"Value": "Отдых (рекреация)",
		"Name": "Leisure"
	},
	{
		"Id": 9,
		"Value": "Малоэтажное жилищное строительство (МЖС)",
		"Name": "LowriseHousing"
	},
	{
		"Id": 10,
		"Value": "Общественное использование объектов капитального строительства",
		"Name": "PublicUseOfCapitalConstruction"
	},
	{
		"Id": 11,
		"Value": "Обслуживание автотранспорта",
		"Name": "ServiceVehicles"
	},
	{
		"Id": 12,
		"Value": "Торговые центры",
		"Name": "ShoppingCenters"
	},
	{
		"Id": 13,
		"Value": "Склады",
		"Name": "Warehouses"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12088, 'Подъездные пути', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'DrivewayType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Асфальтированная дорога",
		"Name": "Asphalt"
	},
	{
		"Id": 2,
		"Value": "Грунтовая дорога",
		"Name": "Ground"
	},
	{
		"Id": 3,
		"Value": "Нет",
		"Name": "No"
	}
]')
on conflict (referenceid) do update set
"description"='Подъездные пути', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='DrivewayType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Асфальтированная дорога",
		"Name": "Asphalt"
	},
	{
		"Id": 2,
		"Value": "Грунтовая дорога",
		"Name": "Ground"
	},
	{
		"Id": 3,
		"Value": "Нет",
		"Name": "No"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12089, 'Единица измерения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'ParcelAreaUnitType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Гектар",
		"Name": "Hectare"
	},
	{
		"Id": 2,
		"Value": "Сотка",
		"Name": "Sotka"
	}
]')
on conflict (referenceid) do update set
"description"='Единица измерения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='ParcelAreaUnitType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Гектар",
		"Name": "Hectare"
	},
	{
		"Id": 2,
		"Value": "Сотка",
		"Name": "Sotka"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12090, 'Тип участка', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'ParcelType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "В собственности",
		"Name": "Owned"
	},
	{
		"Id": 2,
		"Value": "В аренде",
		"Name": "Rent"
	}
]')
on conflict (referenceid) do update set
"description"='Тип участка', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='ParcelType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "В собственности",
		"Name": "Owned"
	},
	{
		"Id": 2,
		"Value": "В аренде",
		"Name": "Rent"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12091, 'Статус земли', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'ParcelStatus', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Фермерское хозяйство",
		"Name": "Farm"
	},
	{
		"Id": 2,
		"Value": "Садоводство",
		"Name": "Gardening"
	},
	{
		"Id": 3,
		"Value": "Индивидуальное жилищное строительство",
		"Name": "IndividualHousingConstruction"
	},
	{
		"Id": 4,
		"Value": "Земля промышленного назначения",
		"Name": "IndustrialLand"
	},
	{
		"Id": 5,
		"Value": "Инвестпроект",
		"Name": "InvestmentProject"
	},
	{
		"Id": 6,
		"Value": "Личное подсобное хозяйство",
		"Name": "PrivateFarm"
	},
	{
		"Id": 7,
		"Value": "Дачное некоммерческое партнерство",
		"Name": "SuburbanNonProfitPartnership"
	},
	{
		"Id": 8,
		"Value": "Участок сельскохозяйственного назначения",
		"Name": "ForAgriculturalPurposes"
	},
	{
		"Id": 9,
		"Value": "Участок промышленности, транспорта, связи и иного не сельхоз. назначения",
		"Name": "IndustryTransportCommunications"
	},
	{
		"Id": 10,
		"Value": "Поселений",
		"Name": "Settlements"
	}
]')
on conflict (referenceid) do update set
"description"='Статус земли', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='ParcelStatus', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Фермерское хозяйство",
		"Name": "Farm"
	},
	{
		"Id": 2,
		"Value": "Садоводство",
		"Name": "Gardening"
	},
	{
		"Id": 3,
		"Value": "Индивидуальное жилищное строительство",
		"Name": "IndividualHousingConstruction"
	},
	{
		"Id": 4,
		"Value": "Земля промышленного назначения",
		"Name": "IndustrialLand"
	},
	{
		"Id": 5,
		"Value": "Инвестпроект",
		"Name": "InvestmentProject"
	},
	{
		"Id": 6,
		"Value": "Личное подсобное хозяйство",
		"Name": "PrivateFarm"
	},
	{
		"Id": 7,
		"Value": "Дачное некоммерческое партнерство",
		"Name": "SuburbanNonProfitPartnership"
	},
	{
		"Id": 8,
		"Value": "Участок сельскохозяйственного назначения",
		"Name": "ForAgriculturalPurposes"
	},
	{
		"Id": 9,
		"Value": "Участок промышленности, транспорта, связи и иного не сельхоз. назначения",
		"Name": "IndustryTransportCommunications"
	},
	{
		"Id": 10,
		"Value": "Поселений",
		"Name": "Settlements"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12092, 'Локация электроснабжения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'ElectricityLocationType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]')
on conflict (referenceid) do update set
"description"='Локация электроснабжения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='ElectricityLocationType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12093, 'Локация газоснабжения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'GasLocationType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]')
on conflict (referenceid) do update set
"description"='Локация газоснабжения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='GasLocationType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12094, 'Давление газа', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'GasPressureType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Высокое",
		"Name": "High"
	},
	{
		"Id": 2,
		"Value": "Среднее",
		"Name": "Middle"
	},
	{
		"Id": 3,
		"Value": "Низкое",
		"Name": "Low"
	}
]')
on conflict (referenceid) do update set
"description"='Давление газа', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='GasPressureType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Высокое",
		"Name": "High"
	},
	{
		"Id": 2,
		"Value": "Среднее",
		"Name": "Middle"
	},
	{
		"Id": 3,
		"Value": "Низкое",
		"Name": "Low"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12095, 'Локация канализации', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'DrainageLocationType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]')
on conflict (referenceid) do update set
"description"='Локация канализации', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='DrainageLocationType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12096, 'Тип канализации', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'DrainageType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Автономная",
		"Name": "Autonomous"
	},
	{
		"Id": 2,
		"Value": "Центральная",
		"Name": "Central"
	}
]')
on conflict (referenceid) do update set
"description"='Тип канализации', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='DrainageType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Автономная",
		"Name": "Autonomous"
	},
	{
		"Id": 2,
		"Value": "Центральная",
		"Name": "Central"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12097, 'Локация водоснабжения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'WaterLocationType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]')
on conflict (referenceid) do update set
"description"='Локация водоснабжения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='WaterLocationType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "По границе участка",
		"Name": "Border"
	},
	{
		"Id": 2,
		"Value": "На участке",
		"Name": "Location"
	}
]';

--<DO>--
insert into core_reference ("referenceid", "description", "readonly", "progid", "istree", "defaultvalue", "name", "register_id", "simple_values") values
(12098, 'Тип водоснабжения', 1, 'Core.RefLib.Executors.ReferenceExecutorSimple', NULL, NULL, 'WaterType', NULL, '[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Автономная",
		"Name": "Autonomous"
	},
	{
		"Id": 2,
		"Value": "Центральная",
		"Name": "Central"
	},
	{
		"Id": 3,
		"Value": "Водонапорная станция",
		"Name": "PumpingStation"
	},
	{
		"Id": 4,
		"Value": "Водозаборный узел",
		"Name": "WaterIntakeFacility"
	},
	{
		"Id": 5,
		"Value": "Водонапорная башня",
		"Name": "WaterTower"
	}
]')
on conflict (referenceid) do update set
"description"='Тип водоснабжения', "readonly"=1, "progid"='Core.RefLib.Executors.ReferenceExecutorSimple', "istree"=NULL, "defaultvalue"=NULL, "name"='WaterType', "register_id"=NULL, "simple_values"='[
    {
		"Id": 0,
		"Value": "Значение отсутствует",
		"Name": "None"
	},
	{
		"Id": 1,
		"Value": "Автономная",
		"Name": "Autonomous"
	},
	{
		"Id": 2,
		"Value": "Центральная",
		"Name": "Central"
	},
	{
		"Id": 3,
		"Value": "Водонапорная станция",
		"Name": "PumpingStation"
	},
	{
		"Id": 4,
		"Value": "Водозаборный узел",
		"Name": "WaterIntakeFacility"
	},
	{
		"Id": 5,
		"Value": "Водонапорная башня",
		"Name": "WaterTower"
	}
]';

--<DO>--
