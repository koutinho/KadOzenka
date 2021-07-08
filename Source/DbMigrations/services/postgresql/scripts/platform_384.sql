alter table CORE_LIST add "isarchive" smallint;
insert into core_register_attribute
("id", "name", "registerid", "type", "parentid", "referenceid", "value_field", "code_field", "value_template", "primary_key", "user_key", "qscolumn", "internal_name", "is_nullable", "description", "layout", "export_column_name", "is_deleted", "hidden") values
(92000900, 'Признак архива', 920, 3, NULL, NULL, 'ISARCHIVE', NULL, NULL, NULL, NULL, NULL, 'IsArchive', 1, NULL, NULL, NULL, NULL, 0);