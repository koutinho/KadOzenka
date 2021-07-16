--коэффициент фактора из предыдущего тура
delete from core_register_attribute where id=21001200;
ALTER TABLE ko_model_factor DROP COLUMN previous_weight;
