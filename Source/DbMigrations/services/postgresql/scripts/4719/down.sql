-- Замена типа поля с varchar(255) на текстовое для описаний
alter table market_core_object rename column advertisement_description to advertisement_description_old;
alter table market_core_object add column advertisement_description varchar(255);
update market_core_object set advertisement_description = advertisement_description_old;
alter table market_core_object drop column advertisement_description_old;

alter table market_core_object alter column property_typets_cipjs_code set not null;

alter table market_core_object drop column latitude;
alter table market_core_object drop column longitude;

delete from core_register_attribute where id = 10003500;
delete from core_register_attribute where id = 10004000;

delete from core_long_process_type where id=103