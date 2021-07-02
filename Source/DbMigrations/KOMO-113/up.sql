INSERT INTO core_srd_function (id, functionname, functiontag, parent_id, description)
VALUES (666, 'Настройка факторов для Наследования', 'KO.TASKS.INHERITANCE_FACTOR_SETTINGS', 618, null);

update core_register_attribute set is_nullable=0 where id in (26300200, 26300500);
ALTER TABLE KO_FACTOR_SETTINGS ALTER COLUMN factor_id SET NOT NULL;
ALTER TABLE KO_FACTOR_SETTINGS ALTER COLUMN correct_factor_id SET NOT NULL;
