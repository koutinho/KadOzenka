-- Удаление колонки запрета с реестра со списком реестров источников
alter table ko_objects_characteristics_register
    drop column if exists disable_editing;
delete from postgres.public.core_register_attribute where id = 2400300 and name = 'Запрет редактирования';

-- Удаление колонки запрета с реестра с параметрами атрибутов источников
alter table gbu_attribute_settings
    drop column if exists disable_editing;
delete from postgres.public.core_register_attribute where id = 8100600 and name = 'Запрет редактирования';