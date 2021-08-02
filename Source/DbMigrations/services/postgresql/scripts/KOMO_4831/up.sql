update core_register_attribute set value_field='A0_FOR_LINEAR', internal_name='A0ForLinear' where id=20600700;
alter table ko_model rename column A0 to A0_FOR_LINEAR;