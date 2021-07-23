delete from core_long_process_type where id=100;

ALTER TABLE ko_modeling_dictionaries_values DROP CONSTRAINT ko_modeling_dictionaries_values_idx1;

DROP INDEX modeling_model_to_market_objects_idx;

DROP INDEX ko_model_factor_idx;