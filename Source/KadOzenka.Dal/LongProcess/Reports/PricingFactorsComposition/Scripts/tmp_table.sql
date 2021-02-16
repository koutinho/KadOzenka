CREATE TABLE public.data_composition_by_characteristics_tmp (
  object_id BIGINT NOT NULL,
  attributes BIGINT [],
  job_number BIGINT NOT NULL,
  is_done SMALLINT
) 
WITH (oids = false);

CREATE INDEX data_composition_by_characteristics_tmp_idx ON public.data_composition_by_characteristics_tmp
  USING btree (object_id);

ALTER TABLE public.data_composition_by_characteristics_tmp
  OWNER TO --REPLACE cipjs_kad_ozenka;

ALTER TABLE public.data_composition_by_characteristics_tmp
  ALTER COLUMN attributes SET STORAGE EXTENDED;