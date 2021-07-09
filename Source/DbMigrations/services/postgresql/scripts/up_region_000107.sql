INSERT INTO core_register VALUES (269, 'KO.TourGroupGroupingSettings', 'Реестр условий группировки', null, null, 'KO_GROUP_GROUPING_SETTINGS', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, 0, null, null);

INSERT INTO core_register_attribute VALUES (26900100, 'Идентификатор', 269, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute VALUES (26900200, 'Идентификатор группы', 269, 1, null, null, 'GROUP_ID', null, null, null, null, null, 'FactorId', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute VALUES (26900300, 'Идентификатор атрибута', 269, 1, null, null, 'KO_ATTRIBUTE_ID', null, null, null, null, null, 'KoAttributeId', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute VALUES (26900400, 'Фильтр', 269, 4, null, null, 'FILTER', null, null, null, null, null, 'Filter', 1, null, null, null, null, null, null, 0);

create table if not exists ko_group_grouping_settings (
    id bigint not null
        constraint reg_269_q_pk
            primary key,
    group_id bigint not null,
    ko_attribute_id bigint not null,
    filter varchar(4000)
);
alter table ko_group_grouping_settings owner to postgres;