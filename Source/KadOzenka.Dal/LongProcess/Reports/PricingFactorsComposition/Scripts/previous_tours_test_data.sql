--находим объекты, которые встречались в разных турах одновременно
select object_id from ko_unit where tour_id=2014
INTERSECT
select object_id from ko_unit where tour_id=2018

--находим задания с этими объектами
select * from ko_unit where object_id=17587970
--смотрим, сколько единиц в задании
select count(*) from ko_unit where task_id=17618270 --2016
select count(*) from ko_unit where task_id=17918527 --2014



--общий очень долгий запрос, который не выполнился за 30 мин
/*with data as
(
    select tour_id, task_id from ko_unit where object_id IN
    (
        select object_id from ko_unit where tour_id=2014
        INTERSECT
        select object_id from ko_unit where tour_id=2018
    )
)

select unit.tour_id, unit.task_id, count(*) 
	from ko_unit unit
    join data d on d.task_id = unit.task_id and d.tour_id = unit.tour_id
	group by unit.tour_id, unit.task_id 
    order by count(*) desc
*/