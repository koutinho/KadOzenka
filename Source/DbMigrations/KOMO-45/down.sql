alter table market_core_object drop column download_date;
delete from core_register_attribute where id=10007800;

alter table market_core_object drop column external_advertisement_id;
delete from core_register_attribute where id=10007900;

alter table market_core_object drop column advertisement_description;
delete from core_register_attribute where id=10008000;
