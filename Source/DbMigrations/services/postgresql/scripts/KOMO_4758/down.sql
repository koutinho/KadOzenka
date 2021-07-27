update core_register_attribute set name='Коэффициент', value_field='COEFFICIENT', internal_name='Coefficient' where id=21000600;
alter table ko_model_factor rename COEFFICIENT_FOR_LINEAR to coefficient;

delete from core_register_attribute where id in(21001800, 21001900);
ALTER TABLE ko_model_factor DROP COLUMN IF EXISTS COEFFICIENT_FOR_EXPONENTIAL;
ALTER TABLE ko_model_factor DROP COLUMN IF EXISTS COEFFICIENT_FOR_MULTIPLICATIVE;