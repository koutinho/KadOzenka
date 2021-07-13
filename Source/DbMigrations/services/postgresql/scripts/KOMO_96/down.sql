delete from core_reference where referenceid = 226;


alter table ko_model_factor drop column mark_type;
alter table ko_model_factor drop column mark_type_code;
delete from core_register_attribute where id=21001500;


alter table KO_MODEL_FACTOR  drop column correcting_term;
delete from core_register_attribute where id=21001600;


alter table KO_MODEL_FACTOR  drop column k;
delete from core_register_attribute where id=21001700;


update core_register_attribute set name='Вес фактора' where id = 21000500;

