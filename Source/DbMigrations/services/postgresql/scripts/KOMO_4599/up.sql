update core_register_attribute set value_field='CORRECTION', internal_name = 'Correction' where id=21000500;
alter table ko_model_factor rename column weight to correction;

update core_register_attribute set name='Коэффициент', value_field='COEFFICIENT', internal_name = 'Coefficient' where id=21000600;
alter table ko_model_factor rename column b0 to Coefficient;