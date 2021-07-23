INSERT INTO core_long_process_type (id, process_name, class_name, schedule_type, repeat_interval, enabled, run_count, failure_count, last_start_date, last_run_duration, next_run_date, parameters, description, test_result, parameters_setter_url)
VALUES (100, 'MarksCalculationLongProcess', 'KadOzenka.Dal.LongProcess.Modeling.MarksCalculationLongProcess, KadOzenka.Dal', 0, null, 1, null, null, null, null, null, null, 'Расчет меток для автоматической модели', 1, null);

ALTER TABLE ko_modeling_dictionaries_values ADD CONSTRAINT ko_modeling_dictionaries_values_idx1 UNIQUE (dictionary_id, value);

CREATE INDEX modeling_model_to_market_objects_idx ON modeling_model_to_market_objects USING btree (model_id);

CREATE INDEX ko_model_factor_idx ON ko_model_factor USING btree (model_id);