update core_reference
set simple_values = (
    with sel as
             (select jsonb_array_elements(regexp_replace(simple_values, ',\n\]', ']')::jsonb) val
              from core_reference
              where referenceid = 801)
    select jsonb_pretty(jsonb_agg(val))
    from sel
    where val ->> 'Name' <> 'NormalisationFinal')
where referenceid = 801