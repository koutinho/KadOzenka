update core_register_attribute set name='Кадастровый номер', value_field='CADASTRAL_NUMBER', internal_name='CadastralNumber' where id=70200200;

alter table modeling_model_to_market_objects rename column market_object_info to CADASTRAL_NUMBER;