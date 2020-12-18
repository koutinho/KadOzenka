CREATE UNIQUE INDEX COMMON_DATA_FORM_STORAGE_UNIQUE_INDX
	ON COMMON_DATA_FORM_STORAGE (COALESCE(id_user, -1), formtype, COALESCE(template_name, ''), COALESCE(is_common, 0));

create unique index core_register_attribute_unique_name_constraint on core_register_attribute(trim(name), registerid) where is_deleted<>1 or is_deleted is null;

-- Индексы на каталог меток, ускорение поиска с 500-600мс до 1-2мс
create index group_id_idx on ko_mark_catalog(group_id);
create index factor_id_idx on ko_mark_catalog(factor_id);
create index value_factor_idx on ko_mark_catalog(value_factor);