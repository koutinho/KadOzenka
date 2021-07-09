drop table if exists ko_cod_dictionary;
delete from core_register_attribute where registerid = 214;
delete from core_register where registerid = 214;

delete
from core_long_process_type
where id = 14
  and process_name = 'HarmonizationCodProcess';