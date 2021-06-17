delete from core_reference where referenceid = 226;


alter table ko_model_factor drop column sign_exponentiation;
delete from core_register_attribute where id=21001400;


alter table ko_model_factor drop column mark_type;
alter table ko_model_factor drop column mark_type_code;
delete from core_register_attribute where id=21001500;

