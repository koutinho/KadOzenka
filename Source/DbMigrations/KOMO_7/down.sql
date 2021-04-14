
--Удаление колонки с информацией о созданной таблице для словаря
alter table ko_cod_job drop column register_id;
delete from core_register_attribute where id=21500400;

--Добавление удаленной колонки RESULT_JOB
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) 
VALUES (21500300, 'Результат', 215, 4, null, null, 'RESULT_JOB', null, null, null, null, null, 'ResultJob', 0, null, null, null, 0, null, null, 0);
ALTER TABLE KO_COD_JOB ADD COLUMN RESULT_JOB varchar(255);

--Отменяем мягкое удаления словаря
delete from common_registers_with_soft_deletion where register_id = 215;

--Возвращаем старую таблицу для хранения значений словаря
CREATE TABLE public.ko_cod_dictionary (
  id BIGINT NOT NULL,
  id_codjob BIGINT NOT NULL,
  value VARCHAR(4000),
  code VARCHAR(255),
  source VARCHAR(255),
  expert VARCHAR(255),
  CONSTRAINT reg_214_q_pk PRIMARY KEY(id)
) 
WITH (oids = false);

COMMENT ON TABLE public.ko_cod_dictionary
IS 'Справочник ЦОД';

COMMENT ON COLUMN public.ko_cod_dictionary.id
IS 'Идентификатор';

COMMENT ON COLUMN public.ko_cod_dictionary.id_codjob
IS 'Идентификатор задания ЦОД';

COMMENT ON COLUMN public.ko_cod_dictionary.value
IS 'Значение';

COMMENT ON COLUMN public.ko_cod_dictionary.code
IS 'Код';

COMMENT ON COLUMN public.ko_cod_dictionary.source
IS ' Источник';

COMMENT ON COLUMN public.ko_cod_dictionary.expert
IS 'ФИО эксперта';

ALTER TABLE public.ko_cod_dictionary
  OWNER TO cipjs_kad_ozenka;