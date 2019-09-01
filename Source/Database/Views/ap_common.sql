drop table ap_common;

create table ap_common as
--Количество жилых помещений, подлежащих страхованию
select 1 as id, 'Количество жилых помещений, подлежащих страхованию' as title, count(1) as amount from insur_flat_q f
where f.actual=1 and f.flag_insur=1

union all

--Количество МКД, участвующих в программе страхования
select 2 as id, 'Количество МКД, участвующих в программе страхования' as title, count(*) as amount from insur_building_q b
where b.actual=1 and b.flag_insur=1

union all

--Количество жилых помещений, участвующих в программе защиты (кол-во ФСП)
select 3 as id, 'Количество жилых помещений, участвующих в программе защиты (кол-во ФСП)' as title, count(*) as amount from insur_fsp_q fsp
where fsp.actual=1

union all

--Количество ущербов
select 4 as id, 'Количество ущербо' as title, count(1) as amount from insur_damage d

union all

--Общий ущерб, руб.
select 5 as id, 'Общий ущерб, руб.' as title, sum(d.sum_damage) as amount from insur_damage d

union all

--Выплаты Правительства Москвы по жилым помещениям, руб.
select 6 as id, 'Выплаты Правительства Москвы по жилым помещениям, руб.' as title, sum(c.sum_opl) as amount from insur_invoice c
where c.link_all_property is null

union all

--Выплаты Правительства Москвы по общему имуществу, руб.
select 7 as id, 'Выплаты Правительства Москвы по общему имуществу, руб.' as title, sum(c.sum_opl) as amount from insur_invoice c
where c.link_all_property is not null;
