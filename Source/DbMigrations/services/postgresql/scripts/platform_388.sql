alter table CORE_LIST add "last_use_date" timestamp;
insert into core_register_attribute
("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "hidden")  values
(92001000, 'Дата последнего использования', 920, 5, NULL, NULL, 'LAST_USE_DATE', NULL, NULL, NULL, NULL, NULL, 'LastUseDate', 1, NULL, NULL, NULL, NULL, 0);