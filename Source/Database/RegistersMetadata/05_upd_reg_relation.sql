
-- V. Загрузка связей реестров
delete from core_register_relation;
--<DO>--
-- 'адрес с адресной связью'. 50 -> 52
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 48;

    update core_register_relation
       set name           = 'адрес с адресной связью',
           parentregister = 50,
           chieldregister = 52,
           cardinality    = NULL,
           kindid         = 5200600
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(48, 'адрес с адресной связью', 50, 52, NULL, 5200600);
     end if;
end $$;
--<DO>--
-- 'От адресной связи к зданию'. 251 -> 52
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 91;

    update core_register_relation
       set name           = 'От адресной связи к зданию',
           parentregister = 251,
           chieldregister = 52,
           cardinality    = NULL,
           kindid         = 5200700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(91, 'От адресной связи к зданию', 251, 52, NULL, 5200700);
     end if;
end $$;
--<DO>--
-- 'От этажа к зданию'. 251 -> 253
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 92;

    update core_register_relation
       set name           = 'От этажа к зданию',
           parentregister = 251,
           chieldregister = 253,
           cardinality    = NULL,
           kindid         = 25300200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(92, 'От этажа к зданию', 251, 253, NULL, 25300200);
     end if;
end $$;
--<DO>--
-- 'От помещения к этажу'. 253 -> 254
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 93;

    update core_register_relation
       set name           = 'От помещения к этажу',
           parentregister = 253,
           chieldregister = 254,
           cardinality    = NULL,
           kindid         = 25400300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(93, 'От помещения к этажу', 253, 254, NULL, 25400300);
     end if;
end $$;
--<DO>--
-- 'От комнаты к этажу'. 253 -> 257
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 94;

    update core_register_relation
       set name           = 'От комнаты к этажу',
           parentregister = 253,
           chieldregister = 257,
           cardinality    = NULL,
           kindid         = 25701200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(94, 'От комнаты к этажу', 253, 257, NULL, 25701200);
     end if;
end $$;
--<DO>--
-- 'От комнаты к помещению'. 254 -> 257
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 95;

    update core_register_relation
       set name           = 'От комнаты к помещению',
           parentregister = 254,
           chieldregister = 257,
           cardinality    = NULL,
           kindid         = 25700200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(95, 'От комнаты к помещению', 254, 257, NULL, 25700200);
     end if;
end $$;
--<DO>--
-- 'От 301 к 303'. 301 -> 303
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 301;

    update core_register_relation
       set name           = 'От 301 к 303',
           parentregister = 301,
           chieldregister = 303,
           cardinality    = NULL,
           kindid         = 303000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(301, 'От 301 к 303', 301, 303, NULL, 303000200);
     end if;
end $$;
--<DO>--
-- 'От 304 к 303'. 304 -> 303
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 302;

    update core_register_relation
       set name           = 'От 304 к 303',
           parentregister = 304,
           chieldregister = 303,
           cardinality    = NULL,
           kindid         = 303000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(302, 'От 304 к 303', 304, 303, NULL, 303000300);
     end if;
end $$;
--<DO>--
-- 'От 301 к 304'. 301 -> 304
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 303;

    update core_register_relation
       set name           = 'От 301 к 304',
           parentregister = 301,
           chieldregister = 304,
           cardinality    = NULL,
           kindid         = 304000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(303, 'От 301 к 304', 301, 304, NULL, 304000200);
     end if;
end $$;
--<DO>--
-- 'От 301 к 305'. 301 -> 305
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 304;

    update core_register_relation
       set name           = 'От 301 к 305',
           parentregister = 301,
           chieldregister = 305,
           cardinality    = NULL,
           kindid         = 305000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(304, 'От 301 к 305', 301, 305, NULL, 305000200);
     end if;
end $$;
--<DO>--
-- 'От 308 к 305'. 308 -> 305
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 305;

    update core_register_relation
       set name           = 'От 308 к 305',
           parentregister = 308,
           chieldregister = 305,
           cardinality    = NULL,
           kindid         = 305000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(305, 'От 308 к 305', 308, 305, NULL, 305000300);
     end if;
end $$;
--<DO>--
-- 'От 301 к 306'. 301 -> 306
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 306;

    update core_register_relation
       set name           = 'От 301 к 306',
           parentregister = 301,
           chieldregister = 306,
           cardinality    = NULL,
           kindid         = 306000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(306, 'От 301 к 306', 301, 306, NULL, 306000200);
     end if;
end $$;
--<DO>--
-- 'От 308 к 306'. 308 -> 306
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 307;

    update core_register_relation
       set name           = 'От 308 к 306',
           parentregister = 308,
           chieldregister = 306,
           cardinality    = NULL,
           kindid         = 306000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(307, 'От 308 к 306', 308, 306, NULL, 306000300);
     end if;
end $$;
--<DO>--
-- 'От 303 к 306'. 303 -> 306
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 308;

    update core_register_relation
       set name           = 'От 303 к 306',
           parentregister = 303,
           chieldregister = 306,
           cardinality    = NULL,
           kindid         = 306000400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(308, 'От 303 к 306', 303, 306, NULL, 306000400);
     end if;
end $$;
--<DO>--
-- 'От 308 к 307'. 308 -> 307
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 310;

    update core_register_relation
       set name           = 'От 308 к 307',
           parentregister = 308,
           chieldregister = 307,
           cardinality    = NULL,
           kindid         = 307000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(310, 'От 308 к 307', 308, 307, NULL, 307000200);
     end if;
end $$;
--<DO>--
-- 'От 305 к 307'. 305 -> 307
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 311;

    update core_register_relation
       set name           = 'От 305 к 307',
           parentregister = 305,
           chieldregister = 307,
           cardinality    = NULL,
           kindid         = 307000500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(311, 'От 305 к 307', 305, 307, NULL, 307000500);
     end if;
end $$;
--<DO>--
-- 'От 308 к 309'. 308 -> 309
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 312;

    update core_register_relation
       set name           = 'От 308 к 309',
           parentregister = 308,
           chieldregister = 309,
           cardinality    = NULL,
           kindid         = 309000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(312, 'От 308 к 309', 308, 309, NULL, 309000300);
     end if;
end $$;
--<DO>--
-- 'От 301 к 309'. 301 -> 309
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 313;

    update core_register_relation
       set name           = 'От 301 к 309',
           parentregister = 301,
           chieldregister = 309,
           cardinality    = NULL,
           kindid         = 309000400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(313, 'От 301 к 309', 301, 309, NULL, 309000400);
     end if;
end $$;
--<DO>--
-- 'От 308 к 314'. 308 -> 314
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 314;

    update core_register_relation
       set name           = 'От 308 к 314',
           parentregister = 308,
           chieldregister = 314,
           cardinality    = NULL,
           kindid         = 314000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(314, 'От 308 к 314', 308, 314, NULL, 314000200);
     end if;
end $$;
--<DO>--
-- 'От 308 к 315'. 308 -> 315
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 315;

    update core_register_relation
       set name           = 'От 308 к 315',
           parentregister = 308,
           chieldregister = 315,
           cardinality    = NULL,
           kindid         = 315000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(315, 'От 308 к 315', 308, 315, NULL, 315000200);
     end if;
end $$;
--<DO>--
-- 'От 310 к 312'. 310 -> 312
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 316;

    update core_register_relation
       set name           = 'От 310 к 312',
           parentregister = 310,
           chieldregister = 312,
           cardinality    = NULL,
           kindid         = 312000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(316, 'От 310 к 312', 310, 312, NULL, 312000300);
     end if;
end $$;
--<DO>--
-- 'От 308 к 310'. 308 -> 310
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 317;

    update core_register_relation
       set name           = 'От 308 к 310',
           parentregister = 308,
           chieldregister = 310,
           cardinality    = NULL,
           kindid         = 310000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(317, 'От 308 к 310', 308, 310, NULL, 310000200);
     end if;
end $$;
--<DO>--
-- 'От 310 к 311'. 310 -> 311
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 318;

    update core_register_relation
       set name           = 'От 310 к 311',
           parentregister = 310,
           chieldregister = 311,
           cardinality    = NULL,
           kindid         = 311000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(318, 'От 310 к 311', 310, 311, NULL, 311000200);
     end if;
end $$;
--<DO>--
-- 'От 324 к 350'. 324 -> 350
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 320;

    update core_register_relation
       set name           = 'От 324 к 350',
           parentregister = 324,
           chieldregister = 350,
           cardinality    = NULL,
           kindid         = 350000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(320, 'От 324 к 350', 324, 350, NULL, 350000300);
     end if;
end $$;
--<DO>--
-- 'От 328 к 315'. 328 -> 315
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 321;

    update core_register_relation
       set name           = 'От 328 к 315',
           parentregister = 328,
           chieldregister = 315,
           cardinality    = NULL,
           kindid         = 315001200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(321, 'От 328 к 315', 328, 315, NULL, 315001200);
     end if;
end $$;
--<DO>--
-- 'От 328 к 313'. 328 -> 313
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 322;

    update core_register_relation
       set name           = 'От 328 к 313',
           parentregister = 328,
           chieldregister = 313,
           cardinality    = NULL,
           kindid         = 313000400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(322, 'От 328 к 313', 328, 313, NULL, 313000400);
     end if;
end $$;
--<DO>--
-- 'От 317 к 313'. 317 -> 313
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 323;

    update core_register_relation
       set name           = 'От 317 к 313',
           parentregister = 317,
           chieldregister = 313,
           cardinality    = NULL,
           kindid         = 313000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(323, 'От 317 к 313', 317, 313, NULL, 313000200);
     end if;
end $$;
--<DO>--
-- 'От 307 к 313'. 307 -> 313
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 324;

    update core_register_relation
       set name           = 'От 307 к 313',
           parentregister = 307,
           chieldregister = 313,
           cardinality    = NULL,
           kindid         = 313003400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(324, 'От 307 к 313', 307, 313, NULL, 313003400);
     end if;
end $$;
--<DO>--
-- 'От 328 к 314'. 328 -> 314
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 325;

    update core_register_relation
       set name           = 'От 328 к 314',
           parentregister = 328,
           chieldregister = 314,
           cardinality    = NULL,
           kindid         = 314000700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(325, 'От 328 к 314', 328, 314, NULL, 314000700);
     end if;
end $$;
--<DO>--
-- 'От расчета к проекту договора (от 312 к 334)'. 312 -> 334
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 326;

    update core_register_relation
       set name           = 'От расчета к проекту договора (от 312 к 334)',
           parentregister = 312,
           chieldregister = 334,
           cardinality    = NULL,
           kindid         = 334000700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(326, 'От расчета к проекту договора (от 312 к 334)', 312, 334, NULL, 334000700);
     end if;
end $$;
--<DO>--
-- 'От 344 к 345 (Банк)'. 344 -> 345
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 328;

    update core_register_relation
       set name           = 'От 344 к 345 (Банк)',
           parentregister = 344,
           chieldregister = 345,
           cardinality    = NULL,
           kindid         = 345002500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(328, 'От 344 к 345 (Банк)', 344, 345, NULL, 345002500);
     end if;
end $$;
--<DO>--
-- 'От 323 к 340 (Вид документа-основания)'. 323 -> 340
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 331;

    update core_register_relation
       set name           = 'От 323 к 340 (Вид документа-основания)',
           parentregister = 323,
           chieldregister = 340,
           cardinality    = NULL,
           kindid         = 340000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(331, 'От 323 к 340 (Вид документа-основания)', 323, 340, NULL, 340000200);
     end if;
end $$;
--<DO>--
-- 'От 328 к 340 (Страховая компания)'. 328 -> 340
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 332;

    update core_register_relation
       set name           = 'От 328 к 340 (Страховая компания)',
           parentregister = 328,
           chieldregister = 340,
           cardinality    = NULL,
           kindid         = 340000700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(332, 'От 328 к 340 (Страховая компания)', 328, 340, NULL, 340000700);
     end if;
end $$;
--<DO>--
-- 'От 345 к 355 (Ссылка на субъект-получатель)'. 345 -> 355
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 336;

    update core_register_relation
       set name           = 'От 345 к 355 (Ссылка на субъект-получатель)',
           parentregister = 345,
           chieldregister = 355,
           cardinality    = NULL,
           kindid         = 355002800
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(336, 'От 345 к 355 (Ссылка на субъект-получатель)', 345, 355, NULL, 355002800);
     end if;
end $$;
--<DO>--
-- 'От 344 к 355 (Ссылка на банк)'. 344 -> 355
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 337;

    update core_register_relation
       set name           = 'От 344 к 355 (Ссылка на банк)',
           parentregister = 344,
           chieldregister = 355,
           cardinality    = NULL,
           kindid         = 355002900
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(337, 'От 344 к 355 (Ссылка на банк)', 344, 355, NULL, 355002900);
     end if;
end $$;
--<DO>--
-- 'От 258 к 316'. 258 -> 316
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 338;

    update core_register_relation
       set name           = 'От 258 к 316',
           parentregister = 258,
           chieldregister = 316,
           cardinality    = NULL,
           kindid         = 316000700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(338, 'От 258 к 316', 258, 316, NULL, 316000700);
     end if;
end $$;
--<DO>--
-- 'От 320 к 345 (Ссылка на округ)'. 320 -> 345
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 339;

    update core_register_relation
       set name           = 'От 320 к 345 (Ссылка на округ)',
           parentregister = 320,
           chieldregister = 345,
           cardinality    = NULL,
           kindid         = 345000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(339, 'От 320 к 345 (Ссылка на округ)', 320, 345, NULL, 345000200);
     end if;
end $$;
--<DO>--
-- 'От 259 к 316'. 259 -> 316
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 340;

    update core_register_relation
       set name           = 'От 259 к 316',
           parentregister = 259,
           chieldregister = 316,
           cardinality    = NULL,
           kindid         = 316000800
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(340, 'От 259 к 316', 259, 316, NULL, 316000800);
     end if;
end $$;
--<DO>--
-- 'От 328 к 320 (Страховая организация - Ссылка Округ)'. 328 -> 320
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 342;

    update core_register_relation
       set name           = 'От 328 к 320 (Страховая организация - Ссылка Округ)',
           parentregister = 328,
           chieldregister = 320,
           cardinality    = NULL,
           kindid         = 320000500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(342, 'От 328 к 320 (Страховая организация - Ссылка Округ)', 328, 320, NULL, 320000500);
     end if;
end $$;
--<DO>--
-- 'От 317 к 308 (Ссылка на помещение)'. 317 -> 308
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 343;

    update core_register_relation
       set name           = 'От 317 к 308 (Ссылка на помещение)',
           parentregister = 317,
           chieldregister = 308,
           cardinality    = NULL,
           kindid         = 308000600
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(343, 'От 317 к 308 (Ссылка на помещение)', 317, 308, NULL, 308000600);
     end if;
end $$;
--<DO>--
-- 'От реестра связи к зданию БТИ'. 251 -> 326
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 345;

    update core_register_relation
       set name           = 'От реестра связи к зданию БТИ',
           parentregister = 251,
           chieldregister = 326,
           cardinality    = NULL,
           kindid         = 326000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(345, 'От реестра связи к зданию БТИ', 251, 326, NULL, 326000200);
     end if;
end $$;
--<DO>--
-- 'От 313 к 355'. 313 -> 355
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 346;

    update core_register_relation
       set name           = 'От 313 к 355',
           parentregister = 313,
           chieldregister = 355,
           cardinality    = NULL,
           kindid         = 355001300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(346, 'От 313 к 355', 313, 355, NULL, 355001300);
     end if;
end $$;
--<DO>--
-- 'От 310 к 355'. 310 -> 355
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 347;

    update core_register_relation
       set name           = 'От 310 к 355',
           parentregister = 310,
           chieldregister = 355,
           cardinality    = NULL,
           kindid         = 355001400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(347, 'От 310 к 355', 310, 355, NULL, 355001400);
     end if;
end $$;
--<DO>--
-- 'От 354 к 355'. 354 -> 355
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 348;

    update core_register_relation
       set name           = 'От 354 к 355',
           parentregister = 354,
           chieldregister = 355,
           cardinality    = NULL,
           kindid         = 355001500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(348, 'От 354 к 355', 354, 355, NULL, 355001500);
     end if;
end $$;
--<DO>--
-- 'От 308 к 355'. 308 -> 355
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 349;

    update core_register_relation
       set name           = 'От 308 к 355',
           parentregister = 308,
           chieldregister = 355,
           cardinality    = NULL,
           kindid         = 355001700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(349, 'От 308 к 355', 308, 355, NULL, 355001700);
     end if;
end $$;
--<DO>--
-- 'От 319 к 316 (Ссылка на адрес)'. 319 -> 316
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 351;

    update core_register_relation
       set name           = 'От 319 к 316 (Ссылка на адрес)',
           parentregister = 319,
           chieldregister = 316,
           cardinality    = NULL,
           kindid         = 316004400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(351, 'От 319 к 316 (Ссылка на адрес)', 319, 316, NULL, 316004400);
     end if;
end $$;
--<DO>--
-- 'От 313 к 314 (Ссылка на реестр дел)'. 313 -> 314
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 352;

    update core_register_relation
       set name           = 'От 313 к 314 (Ссылка на реестр дел)',
           parentregister = 313,
           chieldregister = 314,
           cardinality    = NULL,
           kindid         = 314002700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(352, 'От 313 к 314 (Ссылка на реестр дел)', 313, 314, NULL, 314002700);
     end if;
end $$;
--<DO>--
-- 'От 950 к 340'. 950 -> 340
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 354;

    update core_register_relation
       set name           = 'От 950 к 340',
           parentregister = 950,
           chieldregister = 340,
           cardinality    = NULL,
           kindid         = 340001100
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(354, 'От 950 к 340', 950, 340, NULL, 340001100);
     end if;
end $$;
--<DO>--
-- 'От 357 к 355 (Ссылка на справочник причиин отказов)'. 357 -> 355
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 355;

    update core_register_relation
       set name           = 'От 357 к 355 (Ссылка на справочник причиин отказов)',
           parentregister = 357,
           chieldregister = 355,
           cardinality    = NULL,
           kindid         = 355001900
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(355, 'От 357 к 355 (Ссылка на справочник причиин отказов)', 357, 355, NULL, 355001900);
     end if;
end $$;
--<DO>--
-- 'От 950 к 358 (Ссылка на пользователя)'. 950 -> 358
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 356;

    update core_register_relation
       set name           = 'От 950 к 358 (Ссылка на пользователя)',
           parentregister = 950,
           chieldregister = 358,
           cardinality    = NULL,
           kindid         = 358000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(356, 'От 950 к 358 (Ссылка на пользователя)', 950, 358, NULL, 358000300);
     end if;
end $$;
--<DO>--
-- 'От 251 к 316 (Ссылка на здание БТИ)'. 251 -> 316
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 357;

    update core_register_relation
       set name           = 'От 251 к 316 (Ссылка на здание БТИ)',
           parentregister = 251,
           chieldregister = 316,
           cardinality    = NULL,
           kindid         = 316002400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(357, 'От 251 к 316 (Ссылка на здание БТИ)', 251, 316, NULL, 316002400);
     end if;
end $$;
--<DO>--
-- 'От 400 к 316 (Ссылка на здание ЕГРН)'. 400 -> 316
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 358;

    update core_register_relation
       set name           = 'От 400 к 316 (Ссылка на здание ЕГРН)',
           parentregister = 400,
           chieldregister = 316,
           cardinality    = NULL,
           kindid         = 316002500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(358, 'От 400 к 316 (Ссылка на здание ЕГРН)', 400, 316, NULL, 316002500);
     end if;
end $$;
--<DO>--
-- 'От объекта списка к списку'. 920 -> 921
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 913;

    update core_register_relation
       set name           = 'От объекта списка к списку',
           parentregister = 920,
           chieldregister = 921,
           cardinality    = NULL,
           kindid         = 92100200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(913, 'От объекта списка к списку', 920, 921, NULL, 92100200);
     end if;
end $$;
--<DO>--
-- 'От версии шаблона к шаблону'. 960 -> 961
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 914;

    update core_register_relation
       set name           = 'От версии шаблона к шаблону',
           parentregister = 960,
           chieldregister = 961,
           cardinality    = NULL,
           kindid         = 96100200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(914, 'От версии шаблона к шаблону', 960, 961, NULL, 96100200);
     end if;
end $$;
--<DO>--
-- 'От экземпляра документа к версии шаблона'. 961 -> 963
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 915;

    update core_register_relation
       set name           = 'От экземпляра документа к версии шаблона',
           parentregister = 961,
           chieldregister = 963,
           cardinality    = NULL,
           kindid         = 96300200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(915, 'От экземпляра документа к версии шаблона', 961, 963, NULL, 96300200);
     end if;
end $$;
--<DO>--
-- 'От экземпляра документа к статусу'. 962 -> 963
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 916;

    update core_register_relation
       set name           = 'От экземпляра документа к статусу',
           parentregister = 962,
           chieldregister = 963,
           cardinality    = NULL,
           kindid         = 96300900
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(916, 'От экземпляра документа к статусу', 962, 963, NULL, 96300900);
     end if;
end $$;
--<DO>--
-- 'От набора изменений к экземпляру документа'. 963 -> 964
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 917;

    update core_register_relation
       set name           = 'От набора изменений к экземпляру документа',
           parentregister = 963,
           chieldregister = 964,
           cardinality    = NULL,
           kindid         = 96400200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(917, 'От набора изменений к экземпляру документа', 963, 964, NULL, 96400200);
     end if;
end $$;
--<DO>--
-- 'От изменения к набору изменений'. 964 -> 965
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 918;

    update core_register_relation
       set name           = 'От изменения к набору изменений',
           parentregister = 964,
           chieldregister = 965,
           cardinality    = NULL,
           kindid         = 96500200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(918, 'От изменения к набору изменений', 964, 965, NULL, 96500200);
     end if;
end $$;
--<DO>--
-- 'От аудита к типу аудируемого действия'. 966 -> 967
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 919;

    update core_register_relation
       set name           = 'От аудита к типу аудируемого действия',
           parentregister = 966,
           chieldregister = 967,
           cardinality    = NULL,
           kindid         = 96700300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(919, 'От аудита к типу аудируемого действия', 966, 967, NULL, 96700300);
     end if;
end $$;
--<DO>--
-- 'От аудита к экземпляру документа'. 963 -> 967
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 920;

    update core_register_relation
       set name           = 'От аудита к экземпляру документа',
           parentregister = 963,
           chieldregister = 967,
           cardinality    = NULL,
           kindid         = 96700200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(920, 'От аудита к экземпляру документа', 963, 967, NULL, 96700200);
     end if;
end $$;
--<DO>--
-- 'От дерева к шаблону документа'. 960 -> 968
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 921;

    update core_register_relation
       set name           = 'От дерева к шаблону документа',
           parentregister = 960,
           chieldregister = 968,
           cardinality    = NULL,
           kindid         = 96800400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(921, 'От дерева к шаблону документа', 960, 968, NULL, 96800400);
     end if;
end $$;
--<DO>--
-- 'От образа документа к экземпляру документа'. 963 -> 969
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 922;

    update core_register_relation
       set name           = 'От образа документа к экземпляру документа',
           parentregister = 963,
           chieldregister = 969,
           cardinality    = NULL,
           kindid         = 96900200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(922, 'От образа документа к экземпляру документа', 963, 969, NULL, 96900200);
     end if;
end $$;
--<DO>--
-- 'Идентификатор функции модуля(подсистемы)'. 942 -> 940
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 923;

    update core_register_relation
       set name           = 'Идентификатор функции модуля(подсистемы)',
           parentregister = 942,
           chieldregister = 940,
           cardinality    = NULL,
           kindid         = 94000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(923, 'Идентификатор функции модуля(подсистемы)', 942, 940, NULL, 94000200);
     end if;
end $$;
--<DO>--
-- 'Внешний ключ к данным сессии'. 949 -> 940
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 924;

    update core_register_relation
       set name           = 'Внешний ключ к данным сессии',
           parentregister = 949,
           chieldregister = 940,
           cardinality    = NULL,
           kindid         = 94000700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(924, 'Внешний ключ к данным сессии', 949, 940, NULL, 94000700);
     end if;
end $$;
--<DO>--
-- 'Идентификатор пользователя'. 950 -> 940
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 925;

    update core_register_relation
       set name           = 'Идентификатор пользователя',
           parentregister = 950,
           chieldregister = 940,
           cardinality    = NULL,
           kindid         = 94000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(925, 'Идентификатор пользователя', 950, 940, NULL, 94000300);
     end if;
end $$;
--<DO>--
-- 'Идентификатор модуля (подсистемы) системы'. 942 -> 942
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 927;

    update core_register_relation
       set name           = 'Идентификатор модуля (подсистемы) системы',
           parentregister = 942,
           chieldregister = 942,
           cardinality    = NULL,
           kindid         = 94200400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(927, 'Идентификатор модуля (подсистемы) системы', 942, 942, NULL, 94200400);
     end if;
end $$;
--<DO>--
-- 'Внешний ключ к функции'. 942 -> 946
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 929;

    update core_register_relation
       set name           = 'Внешний ключ к функции',
           parentregister = 942,
           chieldregister = 946,
           cardinality    = NULL,
           kindid         = 94600100
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(929, 'Внешний ключ к функции', 942, 946, NULL, 94600100);
     end if;
end $$;
--<DO>--
-- 'Внешний ключ к роли'. 945 -> 946
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 930;

    update core_register_relation
       set name           = 'Внешний ключ к роли',
           parentregister = 945,
           chieldregister = 946,
           cardinality    = NULL,
           kindid         = 94600200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(930, 'Внешний ключ к роли', 945, 946, NULL, 94600200);
     end if;
end $$;
--<DO>--
-- 'Внешний ключ к роли'. 945 -> 947
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 931;

    update core_register_relation
       set name           = 'Внешний ключ к роли',
           parentregister = 945,
           chieldregister = 947,
           cardinality    = NULL,
           kindid         = 94700200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(931, 'Внешний ключ к роли', 945, 947, NULL, 94700200);
     end if;
end $$;
--<DO>--
-- 'Внешний ключ к правилу реестра'. 947 -> 948
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 932;

    update core_register_relation
       set name           = 'Внешний ключ к правилу реестра',
           parentregister = 947,
           chieldregister = 948,
           cardinality    = NULL,
           kindid         = 94800200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(932, 'Внешний ключ к правилу реестра', 947, 948, NULL, 94800200);
     end if;
end $$;
--<DO>--
-- 'Идентификатор пользователя'. 950 -> 949
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 933;

    update core_register_relation
       set name           = 'Идентификатор пользователя',
           parentregister = 950,
           chieldregister = 949,
           cardinality    = NULL,
           kindid         = 94900200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(933, 'Идентификатор пользователя', 950, 949, NULL, 94900200);
     end if;
end $$;
--<DO>--
-- 'Внешний ключ к подразделению организации'. 941 -> 950
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 934;

    update core_register_relation
       set name           = 'Внешний ключ к подразделению организации',
           parentregister = 941,
           chieldregister = 950,
           cardinality    = NULL,
           kindid         = 95000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(934, 'Внешний ключ к подразделению организации', 941, 950, NULL, 95000200);
     end if;
end $$;
--<DO>--
-- 'Внешний ключ к пользователю'. 950 -> 952
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 935;

    update core_register_relation
       set name           = 'Внешний ключ к пользователю',
           parentregister = 950,
           chieldregister = 952,
           cardinality    = NULL,
           kindid         = 95200100
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(935, 'Внешний ключ к пользователю', 950, 952, NULL, 95200100);
     end if;
end $$;
--<DO>--
-- 'Внешний ключ к роли'. 945 -> 952
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 936;

    update core_register_relation
       set name           = 'Внешний ключ к роли',
           parentregister = 945,
           chieldregister = 952,
           cardinality    = NULL,
           kindid         = 95200200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(936, 'Внешний ключ к роли', 945, 952, NULL, 95200200);
     end if;
end $$;
--<DO>--
-- 'От экземпляра ТД к автору'. 950 -> 963
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 938;

    update core_register_relation
       set name           = 'От экземпляра ТД к автору',
           parentregister = 950,
           chieldregister = 963,
           cardinality    = NULL,
           kindid         = 96300400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(938, 'От экземпляра ТД к автору', 950, 963, NULL, 96300400);
     end if;
end $$;
--<DO>--
-- 'От версии шаблона к типу шаблона'. 971 -> 961
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 939;

    update core_register_relation
       set name           = 'От версии шаблона к типу шаблона',
           parentregister = 971,
           chieldregister = 961,
           cardinality    = NULL,
           kindid         = 96101100
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(939, 'От версии шаблона к типу шаблона', 971, 961, NULL, 96101100);
     end if;
end $$;
--<DO>--
-- 'От ошибки к пользователю'. 950 -> 989
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 940;

    update core_register_relation
       set name           = 'От ошибки к пользователю',
           parentregister = 950,
           chieldregister = 989,
           cardinality    = NULL,
           kindid         = 98900200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(940, 'От ошибки к пользователю', 950, 989, NULL, 98900200);
     end if;
end $$;
--<DO>--
-- 'От показателя к реестру'. 931 -> 930
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 941;

    update core_register_relation
       set name           = 'От показателя к реестру',
           parentregister = 931,
           chieldregister = 930,
           cardinality    = NULL,
           kindid         = 93100300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(941, 'От показателя к реестру', 931, 930, NULL, 93100300);
     end if;
end $$;
--<DO>--
-- 'От сообщения диагностики к пользователю'. 950 -> 992
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 945;

    update core_register_relation
       set name           = 'От сообщения диагностики к пользователю',
           parentregister = 950,
           chieldregister = 992,
           cardinality    = NULL,
           kindid         = 99200200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(945, 'От сообщения диагностики к пользователю', 950, 992, NULL, 99200200);
     end if;
end $$;
--<DO>--
-- 'От показателя к справочнику'. 982 -> 931
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 963;

    update core_register_relation
       set name           = 'От показателя к справочнику',
           parentregister = 982,
           chieldregister = 931,
           cardinality    = NULL,
           kindid         = 93100600
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(963, 'От показателя к справочнику', 982, 931, NULL, 93100600);
     end if;
end $$;
--<DO>--
-- 'От связи реестров к дочернему реестру'. 930 -> 932
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 964;

    update core_register_relation
       set name           = 'От связи реестров к дочернему реестру',
           parentregister = 930,
           chieldregister = 932,
           cardinality    = NULL,
           kindid         = 93200400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(964, 'От связи реестров к дочернему реестру', 930, 932, NULL, 93200400);
     end if;
end $$;
--<DO>--
-- 'От связи реестров к показателю'. 931 -> 932
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 965;

    update core_register_relation
       set name           = 'От связи реестров к показателю',
           parentregister = 931,
           chieldregister = 932,
           cardinality    = NULL,
           kindid         = 93200600
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(965, 'От связи реестров к показателю', 931, 932, NULL, 93200600);
     end if;
end $$;
--<DO>--
-- 'От связи реестров к родительскому реестру'. 993 -> 932
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 966;

    update core_register_relation
       set name           = 'От связи реестров к родительскому реестру',
           parentregister = 993,
           chieldregister = 932,
           cardinality    = NULL,
           kindid         = 93200300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(966, 'От связи реестров к родительскому реестру', 993, 932, NULL, 93200300);
     end if;
end $$;
--<DO>--
-- 'От раскладки к пользователю'. 950 -> 933
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 967;

    update core_register_relation
       set name           = 'От раскладки к пользователю',
           parentregister = 950,
           chieldregister = 933,
           cardinality    = NULL,
           kindid         = 93301200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(967, 'От раскладки к пользователю', 950, 933, NULL, 93301200);
     end if;
end $$;
--<DO>--
-- 'От фильтра к пользователю'. 950 -> 936
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 968;

    update core_register_relation
       set name           = 'От фильтра к пользователю',
           parentregister = 950,
           chieldregister = 936,
           cardinality    = NULL,
           kindid         = 93601000
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(968, 'От фильтра к пользователю', 950, 936, NULL, 93601000);
     end if;
end $$;
--<DO>--
-- 'От детализации раскладок к раскладкам'. 933 -> 935
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 969;

    update core_register_relation
       set name           = 'От детализации раскладок к раскладкам',
           parentregister = 933,
           chieldregister = 935,
           cardinality    = NULL,
           kindid         = 93500200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(969, 'От детализации раскладок к раскладкам', 933, 935, NULL, 93500200);
     end if;
end $$;
--<DO>--
-- 'От автора списка к пользователю'. 950 -> 920
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 970;

    update core_register_relation
       set name           = 'От автора списка к пользователю',
           parentregister = 950,
           chieldregister = 920,
           cardinality    = NULL,
           kindid         = 92000500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(970, 'От автора списка к пользователю', 950, 920, NULL, 92000500);
     end if;
end $$;
--<DO>--
-- 'От файла к образу'. 986 -> 987
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 971;

    update core_register_relation
       set name           = 'От файла к образу',
           parentregister = 986,
           chieldregister = 987,
           cardinality    = NULL,
           kindid         = 98700200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(971, 'От файла к образу', 986, 987, NULL, 98700200);
     end if;
end $$;
--<DO>--
-- 'От образа к автору'. 953 -> 986
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 972;

    update core_register_relation
       set name           = 'От образа к автору',
           parentregister = 953,
           chieldregister = 986,
           cardinality    = NULL,
           kindid         = 98600700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(972, 'От образа к автору', 953, 986, NULL, 98600700);
     end if;
end $$;
--<DO>--
-- 'От связи к образу'. 986 -> 988
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 973;

    update core_register_relation
       set name           = 'От связи к образу',
           parentregister = 986,
           chieldregister = 988,
           cardinality    = NULL,
           kindid         = 98800200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(973, 'От связи к образу', 986, 988, NULL, 98800200);
     end if;
end $$;
--<DO>--
-- 'От связи к ТД'. 963 -> 988
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 974;

    update core_register_relation
       set name           = 'От связи к ТД',
           parentregister = 963,
           chieldregister = 988,
           cardinality    = NULL,
           kindid         = 98800400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(974, 'От связи к ТД', 963, 988, NULL, 98800400);
     end if;
end $$;
--<DO>--
-- 'От деталей фильтра к фильтру'. 936 -> 937
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 975;

    update core_register_relation
       set name           = 'От деталей фильтра к фильтру',
           parentregister = 936,
           chieldregister = 937,
           cardinality    = NULL,
           kindid         = 93700200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(975, 'От деталей фильтра к фильтру', 936, 937, NULL, 93700200);
     end if;
end $$;
--<DO>--
-- 'Связь с дочерним справочным значением'. 983 -> 985
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 976;

    update core_register_relation
       set name           = 'Связь с дочерним справочным значением',
           parentregister = 983,
           chieldregister = 985,
           cardinality    = NULL,
           kindid         = 98500400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(976, 'Связь с дочерним справочным значением', 983, 985, NULL, 98500400);
     end if;
end $$;
--<DO>--
-- 'От очереди долгого процесса к типу'. 976 -> 975
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 988;

    update core_register_relation
       set name           = 'От очереди долгого процесса к типу',
           parentregister = 976,
           chieldregister = 975,
           cardinality    = NULL,
           kindid         = 97500300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(988, 'От очереди долгого процесса к типу', 976, 975, NULL, 97500300);
     end if;
end $$;
--<DO>--
-- 'От 309 к 306'. 309 -> 306
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 989;

    update core_register_relation
       set name           = 'От 309 к 306',
           parentregister = 309,
           chieldregister = 306,
           cardinality    = NULL,
           kindid         = NULL
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(989, 'От 309 к 306', 309, 306, NULL, NULL);
     end if;
end $$;
--<DO>--
-- 'От 320 до 309'. 320 -> 309
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 990;

    update core_register_relation
       set name           = 'От 320 до 309',
           parentregister = 320,
           chieldregister = 309,
           cardinality    = NULL,
           kindid         = 309000700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(990, 'От 320 до 309', 320, 309, NULL, 309000700);
     end if;
end $$;
--<DO>--
-- 'От 321 до 309'. 321 -> 309
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 991;

    update core_register_relation
       set name           = 'От 321 до 309',
           parentregister = 321,
           chieldregister = 309,
           cardinality    = NULL,
           kindid         = 309000800
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(991, 'От 321 до 309', 321, 309, NULL, 309000800);
     end if;
end $$;
--<DO>--
-- 'От 321 к 303'. 321 -> 303
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 992;

    update core_register_relation
       set name           = 'От 321 к 303',
           parentregister = 321,
           chieldregister = 303,
           cardinality    = NULL,
           kindid         = 303000400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(992, 'От 321 к 303', 321, 303, NULL, 303000400);
     end if;
end $$;
--<DO>--
-- 'От 332 к 305'. 332 -> 305
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 993;

    update core_register_relation
       set name           = 'От 332 к 305',
           parentregister = 332,
           chieldregister = 305,
           cardinality    = NULL,
           kindid         = 305001800
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(993, 'От 332 к 305', 332, 305, NULL, 305001800);
     end if;
end $$;
--<DO>--
-- 'От 333 к 305'. 333 -> 305
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 994;

    update core_register_relation
       set name           = 'От 333 к 305',
           parentregister = 333,
           chieldregister = 305,
           cardinality    = NULL,
           kindid         = 305001900
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(994, 'От 333 к 305', 333, 305, NULL, 305001900);
     end if;
end $$;
--<DO>--
-- 'От 321 к 305'. 321 -> 305
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 996;

    update core_register_relation
       set name           = 'От 321 к 305',
           parentregister = 321,
           chieldregister = 305,
           cardinality    = NULL,
           kindid         = 305000900
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(996, 'От 321 к 305', 321, 305, NULL, 305000900);
     end if;
end $$;
--<DO>--
-- 'От 321 к 306'. 321 -> 306
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 997;

    update core_register_relation
       set name           = 'От 321 к 306',
           parentregister = 321,
           chieldregister = 306,
           cardinality    = NULL,
           kindid         = 306003300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(997, 'От 321 к 306', 321, 306, NULL, 306003300);
     end if;
end $$;
--<DO>--
-- 'От 333 к 306'. 333 -> 306
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 999;

    update core_register_relation
       set name           = 'От 333 к 306',
           parentregister = 333,
           chieldregister = 306,
           cardinality    = NULL,
           kindid         = 306001900
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(999, 'От 333 к 306', 333, 306, NULL, 306001900);
     end if;
end $$;
--<DO>--
-- 'От 328 к 309'. 328 -> 309
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1001;

    update core_register_relation
       set name           = 'От 328 к 309',
           parentregister = 328,
           chieldregister = 309,
           cardinality    = NULL,
           kindid         = 309000600
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1001, 'От 328 к 309', 328, 309, NULL, 309000600);
     end if;
end $$;
--<DO>--
-- 'От 950 к 301'. 950 -> 301
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1002;

    update core_register_relation
       set name           = 'От 950 к 301',
           parentregister = 950,
           chieldregister = 301,
           cardinality    = NULL,
           kindid         = 301001100
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1002, 'От 950 к 301', 950, 301, NULL, 301001100);
     end if;
end $$;
--<DO>--
-- 'От 320 к 325'. 320 -> 325
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1003;

    update core_register_relation
       set name           = 'От 320 к 325',
           parentregister = 320,
           chieldregister = 325,
           cardinality    = NULL,
           kindid         = 32500300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1003, 'От 320 к 325', 320, 325, NULL, 32500300);
     end if;
end $$;
--<DO>--
-- 'От 316 к 326'. 316 -> 326
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1004;

    update core_register_relation
       set name           = 'От 316 к 326',
           parentregister = 316,
           chieldregister = 326,
           cardinality    = NULL,
           kindid         = 326000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1004, 'От 316 к 326', 316, 326, NULL, 326000300);
     end if;
end $$;
--<DO>--
-- 'От 320 к 321'. 320 -> 321
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1005;

    update core_register_relation
       set name           = 'От 320 к 321',
           parentregister = 320,
           chieldregister = 321,
           cardinality    = NULL,
           kindid         = 321000400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1005, 'От 320 к 321', 320, 321, NULL, 321000400);
     end if;
end $$;
--<DO>--
-- 'От 321 к 304'. 321 -> 304
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1006;

    update core_register_relation
       set name           = 'От 321 к 304',
           parentregister = 321,
           chieldregister = 304,
           cardinality    = NULL,
           kindid         = 304000500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1006, 'От 321 к 304', 321, 304, NULL, 304000500);
     end if;
end $$;
--<DO>--
-- 'От 321 к 301'. 321 -> 301
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1007;

    update core_register_relation
       set name           = 'От 321 к 301',
           parentregister = 321,
           chieldregister = 301,
           cardinality    = NULL,
           kindid         = 301000500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1007, 'От 321 к 301', 321, 301, NULL, 301000500);
     end if;
end $$;
--<DO>--
-- 'От 301 к 314'. 301 -> 314
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1008;

    update core_register_relation
       set name           = 'От 301 к 314',
           parentregister = 301,
           chieldregister = 314,
           cardinality    = NULL,
           kindid         = 314002400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1008, 'От 301 к 314', 301, 314, NULL, 314002400);
     end if;
end $$;
--<DO>--
-- 'От 301 к 315'. 301 -> 315
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1009;

    update core_register_relation
       set name           = 'От 301 к 315',
           parentregister = 301,
           chieldregister = 315,
           cardinality    = NULL,
           kindid         = 315003200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1009, 'От 301 к 315', 301, 315, NULL, 315003200);
     end if;
end $$;
--<DO>--
-- 'От 301 к 311'. 301 -> 311
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1011;

    update core_register_relation
       set name           = 'От 301 к 311',
           parentregister = 301,
           chieldregister = 311,
           cardinality    = NULL,
           kindid         = 311001800
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1011, 'От 301 к 311', 301, 311, NULL, 311001800);
     end if;
end $$;
--<DO>--
-- 'От 316 к 317'. 316 -> 317
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1012;

    update core_register_relation
       set name           = 'От 316 к 317',
           parentregister = 316,
           chieldregister = 317,
           cardinality    = NULL,
           kindid         = 317002000
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1012, 'От 316 к 317', 316, 317, NULL, 317002000);
     end if;
end $$;
--<DO>--
-- 'От 310 к 809'. 310 -> 809
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1013;

    update core_register_relation
       set name           = 'От 310 к 809',
           parentregister = 310,
           chieldregister = 809,
           cardinality    = NULL,
           kindid         = 80900700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1013, 'От 310 к 809', 310, 809, NULL, 80900700);
     end if;
end $$;
--<DO>--
-- 'От 950 к 809'. 950 -> 809
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1014;

    update core_register_relation
       set name           = 'От 950 к 809',
           parentregister = 950,
           chieldregister = 809,
           cardinality    = NULL,
           kindid         = 80900500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1014, 'От 950 к 809', 950, 809, NULL, 80900500);
     end if;
end $$;
--<DO>--
-- 'От 333 к 317'. 333 -> 317
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1015;

    update core_register_relation
       set name           = 'От 333 к 317',
           parentregister = 333,
           chieldregister = 317,
           cardinality    = NULL,
           kindid         = 317001100
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1015, 'От 333 к 317', 333, 317, NULL, 317001100);
     end if;
end $$;
--<DO>--
-- 'От 332 к 317'. 332 -> 317
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1016;

    update core_register_relation
       set name           = 'От 332 к 317',
           parentregister = 332,
           chieldregister = 317,
           cardinality    = NULL,
           kindid         = 317001000
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1016, 'От 332 к 317', 332, 317, NULL, 317001000);
     end if;
end $$;
--<DO>--
-- 'От 345 к 312'. 345 -> 312
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1017;

    update core_register_relation
       set name           = 'От 345 к 312',
           parentregister = 345,
           chieldregister = 312,
           cardinality    = NULL,
           kindid         = 312004400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1017, 'От 345 к 312', 345, 312, NULL, 312004400);
     end if;
end $$;
--<DO>--
-- 'От 328 к 310'. 328 -> 310
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1018;

    update core_register_relation
       set name           = 'От 328 к 310',
           parentregister = 328,
           chieldregister = 310,
           cardinality    = NULL,
           kindid         = 310000300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1018, 'От 328 к 310', 328, 310, NULL, 310000300);
     end if;
end $$;
--<DO>--
-- 'От 316 к 310'. 316 -> 310
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1019;

    update core_register_relation
       set name           = 'От 316 к 310',
           parentregister = 316,
           chieldregister = 310,
           cardinality    = NULL,
           kindid         = 310002200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1019, 'От 316 к 310', 316, 310, NULL, 310002200);
     end if;
end $$;
--<DO>--
-- 'От 320 к 310'. 320 -> 310
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1020;

    update core_register_relation
       set name           = 'От 320 к 310',
           parentregister = 320,
           chieldregister = 310,
           cardinality    = NULL,
           kindid         = 310000400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1020, 'От 320 к 310', 320, 310, NULL, 310000400);
     end if;
end $$;
--<DO>--
-- 'От 345 к 310'. 345 -> 310
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1021;

    update core_register_relation
       set name           = 'От 345 к 310',
           parentregister = 345,
           chieldregister = 310,
           cardinality    = NULL,
           kindid         = 310000700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1021, 'От 345 к 310', 345, 310, NULL, 310000700);
     end if;
end $$;
--<DO>--
-- 'От 345 к 314'. 345 -> 314
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1022;

    update core_register_relation
       set name           = 'От 345 к 314',
           parentregister = 345,
           chieldregister = 314,
           cardinality    = NULL,
           kindid         = 314002100
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1022, 'От 345 к 314', 345, 314, NULL, 314002100);
     end if;
end $$;
--<DO>--
-- 'От 320 к 315'. 320 -> 315
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1023;

    update core_register_relation
       set name           = 'От 320 к 315',
           parentregister = 320,
           chieldregister = 315,
           cardinality    = NULL,
           kindid         = 315001300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1023, 'От 320 к 315', 320, 315, NULL, 315001300);
     end if;
end $$;
--<DO>--
-- 'От 345 к 315'. 345 -> 315
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1024;

    update core_register_relation
       set name           = 'От 345 к 315',
           parentregister = 345,
           chieldregister = 315,
           cardinality    = NULL,
           kindid         = 315002300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1024, 'От 345 к 315', 345, 315, NULL, 315002300);
     end if;
end $$;
--<DO>--
-- 'От 322 к 301'. 322 -> 301
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1025;

    update core_register_relation
       set name           = 'От 322 к 301',
           parentregister = 322,
           chieldregister = 301,
           cardinality    = NULL,
           kindid         = 301001300
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1025, 'От 322 к 301', 322, 301, NULL, 301001300);
     end if;
end $$;
--<DO>--
-- 'От 322 к 302'. 322 -> 302
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1026;

    update core_register_relation
       set name           = 'От 322 к 302',
           parentregister = 322,
           chieldregister = 302,
           cardinality    = NULL,
           kindid         = 302000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1026, 'От 322 к 302', 322, 302, NULL, 302000200);
     end if;
end $$;
--<DO>--
-- 'От 301 к 359'. 301 -> 359
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1027;

    update core_register_relation
       set name           = 'От 301 к 359',
           parentregister = 301,
           chieldregister = 359,
           cardinality    = NULL,
           kindid         = 359000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1027, 'От 301 к 359', 301, 359, NULL, 359000200);
     end if;
end $$;
--<DO>--
-- 'От 322 к 975'. 322 -> 975
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1029;

    update core_register_relation
       set name           = 'От 322 к 975',
           parentregister = 322,
           chieldregister = 975,
           cardinality    = NULL,
           kindid         = 97500500
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1029, 'От 322 к 975', 322, 975, NULL, 97500500);
     end if;
end $$;
--<DO>--
-- 'От 325 к 301'. 325 -> 301
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1030;

    update core_register_relation
       set name           = 'От 325 к 301',
           parentregister = 325,
           chieldregister = 301,
           cardinality    = NULL,
           kindid         = 301001000
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1030, 'От 325 к 301', 325, 301, NULL, 301001000);
     end if;
end $$;
--<DO>--
-- 'От 950 к 352'. 950 -> 352
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1031;

    update core_register_relation
       set name           = 'От 950 к 352',
           parentregister = 950,
           chieldregister = 352,
           cardinality    = NULL,
           kindid         = 352000700
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1031, 'От 950 к 352', 950, 352, NULL, 352000700);
     end if;
end $$;
--<DO>--
-- 'От 328 к 312'. 328 -> 312
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1032;

    update core_register_relation
       set name           = 'От 328 к 312',
           parentregister = 328,
           chieldregister = 312,
           cardinality    = NULL,
           kindid         = 312000400
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1032, 'От 328 к 312', 328, 312, NULL, 312000400);
     end if;
end $$;
--<DO>--
-- 'От 301 к 370'. 301 -> 370
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1033;

    update core_register_relation
       set name           = 'От 301 к 370',
           parentregister = 301,
           chieldregister = 370,
           cardinality    = NULL,
           kindid         = 370000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1033, 'От 301 к 370', 301, 370, NULL, 370000200);
     end if;
end $$;
--<DO>--
-- 'От 933 к 956'. 933 -> 956
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1034;

    update core_register_relation
       set name           = 'От 933 к 956',
           parentregister = 933,
           chieldregister = 956,
           cardinality    = NULL,
           kindid         = 95600200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1034, 'От 933 к 956', 933, 956, NULL, 95600200);
     end if;
end $$;
--<DO>--
-- 'От 328 к 258'. 328 -> 258
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1035;

    update core_register_relation
       set name           = 'От 328 к 258',
           parentregister = 328,
           chieldregister = 258,
           cardinality    = NULL,
           kindid         = 258001000
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1035, 'От 328 к 258', 328, 258, NULL, 258001000);
     end if;
end $$;
--<DO>--
-- 'От 316 к 312'. 316 -> 312
DO $$
declare
    r_relation core_register_relation%ROWTYPE;
    relation_next_id numeric;
begin

    -- Если связь есть, то обновить
    select * into r_relation from core_register_relation t where t.id = 1036;

    update core_register_relation
       set name           = 'От 316 к 312',
           parentregister = 316,
           chieldregister = 312,
           cardinality    = NULL,
           kindid         = 312000200
     where id = r_relation.id;

    -- Если атрибут отсутствует, то добавить
     if(not found)then
       insert into core_register_relation(id,name,parentregister,chieldregister,cardinality,kindid)
                                   values(1036, 'От 316 к 312', 316, 312, NULL, 312000200);
     end if;
end $$;
