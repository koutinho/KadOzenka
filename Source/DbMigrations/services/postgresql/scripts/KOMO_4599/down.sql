update core_register_attribute set value_field='Weight', internal_name = 'Weight' where id=21000500;
alter table ko_model_factor rename column correction to Weight;