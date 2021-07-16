--коэффициент фактора из предыдущего тура
delete from core_register_attribute where id=21001200;
ALTER TABLE ko_model_factor DROP COLUMN previous_weight;


--свободные члены из предыдущего тура
delete from core_register_attribute where id in (20601800, 20601900, 20602000);
ALTER TABLE ko_model DROP COLUMN a0_linear_previous;
ALTER TABLE ko_model DROP COLUMN a0_multiplicative_previous;
ALTER TABLE ko_model DROP COLUMN a0_exponential_previous;
