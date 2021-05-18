alter table market_core_object drop column download_date;
delete from core_register_attribute where id=10007800;

alter table market_core_object drop column external_advertisement_id;
delete from core_register_attribute where id=10007900;

alter table market_core_object drop column advertisement_description;
delete from core_register_attribute where id=10008000;

alter table market_core_object drop column area_from;
delete from core_register_attribute where id=10008100;

alter table market_core_object drop column name;
delete from core_register_attribute where id=10008200;

alter table market_core_object drop column flat_number;
delete from core_register_attribute where id=10008300;

alter table market_core_object drop column section_number;
delete from core_register_attribute where id=10008400;

alter table market_core_object drop column flat_type;
delete from core_register_attribute where id=10008500;

alter table market_core_object drop column deal_type;
alter table market_core_object drop column deal_type_code;
delete from core_register_attribute where id=10003600;