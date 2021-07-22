begin;
-- 269 ��������� �����������
INSERT INTO core_register VALUES (269, 'KO.TourGroupGroupingSettings', '������ ������� �����������', null, null, 'KO_GROUP_GROUPING_SETTINGS', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, 0, null, null);

INSERT INTO core_register_attribute VALUES (26900100, '�������������', 269, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute VALUES (26900200, '������������� ������', 269, 1, null, null, 'GROUP_ID', null, null, null, null, null, 'GroupId', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute VALUES (26900300, '������������� ��������', 269, 1, null, null, 'KO_ATTRIBUTE_ID', null, null, null, null, null, 'KoAttributeId', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute VALUES (26900400, '������', 269, 4, null, null, 'FILTER', null, null, null, null, null, 'Filter', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute VALUES (26900500, '������������� �����������', 269, 1, null, null, 'DICTIONARY_ID', null, null, null, null, null, 'DictionaryId', 1, null, null, null, null, null, null, 0);
INSERT INTO core_register_attribute VALUES (26900600, '�������� �����������', 269, 4, null, null, 'DICTIONARY_VALUES', null, null, null, null, null, 'DictionaryValues', 1, null, null, null, null, null, null, 0);

create table if not exists ko_group_grouping_settings (
    id bigint not null
      constraint reg_269_q_pk
          primary key,
    group_id bigint not null,
    ko_attribute_id bigint not null,
    filter varchar(4000),
    dictionary_id bigint,
    dictionary_values varchar(4000)
);

alter table ko_group_grouping_settings owner to postgres;

-- 270 ����������� �����������
INSERT INTO core_register VALUES (270, 'KO.GroupingDictionary', '����������� �����������', null, null, 'KO_GROUPING_DICTIONARIES', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, 'CHANGE_USER_ID', 'CHANGE_DATE', null, null, null);
INSERT INTO core_register_attribute VALUES (27000100, '�������������', 270, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, now(), 0);
INSERT INTO core_register_attribute VALUES (27000200, '���', 270, 4, null, null, 'NAME', null, null, 0, null, null, 'Name', 0, null, null, null, 0, 2, now(), 0);
INSERT INTO core_register_attribute VALUES (27000300, '���', 270, 4, null, 600, 'TYPE', 'TYPE_CODE', null, 0, null, null, 'Type', 0, null, null, null, 0, 2, now(), 0);

create table if not exists ko_grouping_dictionaries
(
    id bigint not null
        constraint reg_270_q_pk
            primary key,
    name varchar(255) not null,
    type varchar(255) not null,
    type_code bigint not null,
    change_user_id bigint,
    change_date timestamp
);

alter table ko_grouping_dictionaries owner to postgres;

create unique index ko_grouping_dictionaries_name_key
    on ko_grouping_dictionaries (name);

-- 271 �������� ������������ �����������
INSERT INTO core_register VALUES (271, 'KO.GroupingDictionariesValues', '����������� �����������. �������� ������������', null, null, 'KO_GROUPING_DICTIONARIES_VALUES', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, null, null, null);
INSERT INTO core_register_attribute VALUES (27100100, '�������������', 271, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, null, 2, now(), 0);
INSERT INTO core_register_attribute VALUES (27100200, '�� �����������', 271, 1, null, null, 'DICTIONARY_ID', null, null, 0, null, null, 'DictionaryId', 0, null, null, null, 0, 2, now(), 0);
INSERT INTO core_register_attribute VALUES (27100300, '��������', 271, 4, null, null, 'VALUE', null, null, 0, null, null, 'Value', 1, null, null, null, 0, 2, now(), 0);
INSERT INTO core_register_attribute VALUES (27100400, '�������� ��� �����������', 271, 4, null, null, 'GROUPING_VALUE', null, null, 0, null, null, 'GroupingValue', 1, null, null, null, 0, 2, now(), 0);

create table if not exists ko_grouping_dictionaries_values
(
    id bigint not null
        constraint reg_271_q_pk
            primary key,
    dictionary_id bigint not null,
    value varchar(4000),
    grouping_value varchar(4000)
);

alter table ko_grouping_dictionaries_values owner to postgres;

-- ����� ����������� � ����������
INSERT INTO core_register_relation VALUES (271, '�� �������� ����������� ����������� � �����������', 270, 271, null, 27100200, null, null);

-- ���
INSERT INTO public.core_srd_function (id, functionname, functiontag, parent_id, description) VALUES (670, '���������� ��������� ������', 'KO.GROUPING', 502, null);
INSERT INTO public.core_srd_function (id, functionname, functiontag, parent_id, description) VALUES (671, '��������� �������� ��� �����������', 'KO.GROUPING.DICT', 670, null);

-- ���� ��� ��� ��������
alter table ko_group add column check_model_factors_values smallint;
INSERT INTO core_register_attribute VALUES (20502000, '��������� ������� �������� ��������', 205, 3, null, null, 'CHECK_MODEL_FACTORS_VALUES', null, null, null, null, null, 'CheckModelFactorsValues', 1, null, null, null, null, null, null, 0);

-- �������� ��� ���������� ������� �������� �� �����
CREATE OR REPLACE FUNCTION notify_ko_grouping_dictionaries_updating()
    RETURNS trigger
    LANGUAGE plpgsql
AS $function$
begin
    PERFORM pg_notify('notify_ko_grouping_dictionaries_updating'::text, 'notify_ko_grouping_dictionaries_updating'::text);
    return null;
end;
$function$;

DROP TRIGGER IF EXISTS ko_group_grouping_dictionaries_changed ON ko_grouping_dictionaries;
CREATE TRIGGER ko_group_grouping_dictionaries_changed AFTER INSERT OR UPDATE OR DELETE ON ko_grouping_dictionaries FOR EACH ROW EXECUTE FUNCTION notify_ko_grouping_dictionaries_updating();
commit;