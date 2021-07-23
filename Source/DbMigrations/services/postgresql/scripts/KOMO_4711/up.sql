update core_register_attribute set name='Описание объекта-аналога', value_field='market_object_info', internal_name='MarketObjectInfo' where id=70200200;

alter table modeling_model_to_market_objects rename column CADASTRAL_NUMBER to market_object_info;