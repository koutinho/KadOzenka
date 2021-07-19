update core_register_attribute set value_field='CORRECTION', internal_name = 'Correction' where id=21000500;
alter table ko_model_factor rename column weight to correction;