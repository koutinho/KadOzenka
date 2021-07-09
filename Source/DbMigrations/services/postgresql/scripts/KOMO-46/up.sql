drop table if exists ko_cod_dictionary;
delete from core_register_attribute where registerid = 214;
delete from core_register where registerid = 214;

delete
from core_long_process_type
where id = 14
  and process_name = 'HarmonizationCodProcess';

update core_reference
set simple_values = (
    with sel as
             (select jsonb_array_elements(regexp_replace(simple_values, ',\r\n\]', ']')::jsonb) val
              from core_reference
              where referenceid = 801)
    select jsonb_pretty(jsonb_agg(val))
    from sel
    where val ->> 'Name' <> 'HarmonizationCOD')
where referenceid = 801