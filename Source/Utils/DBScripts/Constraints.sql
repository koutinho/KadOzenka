alter table ko_model add constraint ko_model_unique_constraint unique (name,group_id);
--требует чистки базы перед выполнением, имеются конфликты на данный момент
--alter table core_register_attribute add constraint core_register_attribute_unique_name_constraint unique (name, registerid);