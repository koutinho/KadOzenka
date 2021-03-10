
with unit_data as (
	select u.cadastral_number as CadastralNumber,
	task.tour_id as TaskTourId,
	tour.year as TourYear,
	u.task_id as TaskId,
	task.creation_date as TaskCreationDate,
	u.property_type as PropertyType,
	u.square as Square,
	u.status_repeat_calc as Status,
	u.cadastral_cost as CadastralCost,
	u.upks as Upks
	from ko_unit u
		join ko_task task on task.id=u.task_id
		join ko_tour tour on tour.id=task.tour_id
	where u.task_id in ({0}) and u.property_type_code<>2190
),

prev_unit_with_the_same_kn_data as (
	select num_data.cadastral_number as CadastralNumber,
		num_data.upks as PreviousUpks, 
		num_data.cadastral_cost as PreviousCadastralCost
	from (select u.cadastral_cost, u.upks,
			u.cadastral_number,
			row_number() over (PARTITION BY u.cadastral_number order by task.creation_date desc) as num
		from ko_unit u
			join unit_data ud on u.cadastral_number=ud.CadastralNumber
			join ko_task task on u.task_id=task.id
			join ko_tour tour on tour.id=task.tour_id
		where tour.year<ud.TourYear and task.note_type_code=4 and u.property_type_code<>2190
	) num_data where num_data.num=1
)

select ud.CadastralNumber,
	ud.PropertyType,
	ud.Square,
	prev_ud.PreviousUpks,
	prev_ud.PreviousCadastralCost,
	ud.CadastralCost,
	ud.Upks,
	ud.Status
	from unit_data ud
		left join prev_unit_with_the_same_kn_data prev_ud on ud.CadastralNumber=prev_ud.CadastralNumber
		limit {1} offset {2} * {1}
	

