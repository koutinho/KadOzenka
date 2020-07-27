drop table ap_common;

create table ap_common as
--���������� ����� ���������, ���������� �����������
select 1 as id, '���������� ����� ���������, ���������� �����������' as title, count(1) as amount from insur_flat_q f
where f.actual=1 and f.flag_insur=1

union all

--���������� ���, ����������� � ��������� �����������
select 2 as id, '���������� ���, ����������� � ��������� �����������' as title, count(*) as amount from insur_building_q b
where b.actual=1 and b.flag_insur=1

union all

--���������� ����� ���������, ����������� � ��������� ������ (���-�� ���)
select 3 as id, '���������� ����� ���������, ����������� � ��������� ������ (���-�� ���)' as title, count(*) as amount from insur_fsp_q fsp
where fsp.actual=1

union all

--���������� �������
select 4 as id, '���������� ������' as title, count(1) as amount from insur_damage d

union all

--����� �����, ���.
select 5 as id, '����� �����, ���.' as title, sum(d.sum_damage) as amount from insur_damage d

union all

--������� ������������� ������ �� ����� ����������, ���.
select 6 as id, '������� ������������� ������ �� ����� ����������, ���.' as title, sum(c.sum_opl) as amount from insur_invoice c
where c.link_all_property is null

union all

--������� ������������� ������ �� ������ ���������, ���.
select 7 as id, '������� ������������� ������ �� ������ ���������, ���.' as title, sum(c.sum_opl) as amount from insur_invoice c
where c.link_all_property is not null;
