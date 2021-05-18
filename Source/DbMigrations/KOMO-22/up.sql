update core_reference
set simple_values = (
    with sel as (select jsonb_array_elements(regexp_replace(simple_values, ',\n\]', ']')::jsonb
        || '{"Id": 10,"Value":"Финализация нормализации", "Name":"NormalisationFinal" }') val
                 from core_reference
                 where referenceid = 801
    )
    select jsonb_pretty(jsonb_agg(val) ) from sel)
where referenceid = 801;