delete from core_register where registerid=269 and registername='KO.TourGroupGroupingSettings';
delete from core_register where registerid=270 and registername='KO.GroupingDictionary';
delete from core_register where registerid=271 and registername='KO.GroupingDictionariesValues';
delete from core_register_attribute where registerid=269;
delete from core_register_attribute where registerid=270;
delete from core_register_attribute where registerid=271;
drop table if exists ko_group_grouping_settings;
drop table if exists ko_grouping_dictionaries;
drop table if exists ko_grouping_dictionaries_values;
delete from core_register_relation where id=271 and parentregister=270 and chieldregister=271;

delete from core_srd_function where functionname = 'Присвоение оценочной группы' and functiontag =  'KO.GROUPING';
delete from core_srd_function where functionname = 'Настройка словарей для группировки' and functiontag = 'KO.GROUPING.DICT';