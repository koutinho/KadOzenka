update core_reference
set simple_values = (
    with sel as (select jsonb_array_elements(regexp_replace(simple_values, ',\n\]', ']')::jsonb
        || '{"Id": 10,"Value":"Финализация нормализации", "Name":"NormalisationFinal" }') val
                 from core_reference
                 where referenceid = 801
    )
    select jsonb_pretty(jsonb_agg(val) ) from sel)
where referenceid = 801;

INSERT INTO core_long_process_type (id, process_name, class_name, schedule_type, repeat_interval, enabled, run_count, failure_count, last_start_date, last_run_duration, next_run_date, parameters, description, test_result, parameters_setter_url) VALUES (14, 'SetPriorityGroupFinalProcess', 'KadOzenka.Dal.LongProcess.SetPriorityGroupFinalProcess, KadOzenka.Dal', 0, null, 1, null, null, '2021-05-24 10:05:49.585243', null, null, null, 'Операция финализации нормализации', 1, null)
on conflict (id) do update set (id, process_name, class_name, schedule_type, repeat_interval, enabled, run_count, failure_count, last_start_date, last_run_duration, next_run_date, parameters, description, test_result, parameters_setter_url) = (14, 'SetPriorityGroupFinalProcess', 'KadOzenka.Dal.LongProcess.SetPriorityGroupFinalProcess, KadOzenka.Dal', 0, null, 1, null, null, '2021-05-24 10:05:49.585243', null, null, null, 'Операция финализации нормализации', 1, null);