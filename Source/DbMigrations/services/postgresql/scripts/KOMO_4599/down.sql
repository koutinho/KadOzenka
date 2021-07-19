update core_register_attribute set value_field='Weight', internal_name = 'Weight' where id=21000500;
alter table ko_model_factor rename column correction to Weight;

update core_register_attribute set name='Добавочный коэффициент (B0)', value_field='B0', internal_name = 'B0' where id=21000600;
alter table ko_model_factor rename column Coefficient to b0;