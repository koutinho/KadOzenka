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

alter table market_core_object drop column house_line;
alter table market_core_object drop column house_line_code;
delete from core_register_attribute where id=10008600;

alter table market_core_object drop column developer;
delete from core_register_attribute where id=10008700;

alter table market_core_object drop column finishing_condition;
alter table market_core_object drop column finishing_condition_code;
delete from core_register_attribute where id=10008800;

alter table market_core_object drop column house_type;
alter table market_core_object drop column house_type_code;
delete from core_register_attribute where id=10008900;

alter table market_core_object drop column layout;
alter table market_core_object drop column layout_code;
delete from core_register_attribute where id=10009000;

alter table market_core_object drop column permitted_use_type;
alter table market_core_object drop column permitted_use_type_code;
delete from core_register_attribute where id=10009100;

alter table market_core_object drop column driveway_type;
alter table market_core_object drop column driveway_type_code;
delete from core_register_attribute where id=10009200;

alter table market_core_object drop column parcel_area_unit_type;
alter table market_core_object drop column parcel_area_unit_type_code;
delete from core_register_attribute where id=10009300;

alter table market_core_object drop column parcel_status;
alter table market_core_object drop column parcel_status_code;
delete from core_register_attribute where id=10009400;

alter table market_core_object drop column parcel_type;
alter table market_core_object drop column parcel_type_code;
delete from core_register_attribute where id=10009500;