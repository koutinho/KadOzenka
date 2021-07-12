INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 260, 'KO_AUTO_CALCULATION_SETTINGS');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 210, 'KO_MODEL_FACTOR');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 702, 'MODELING_MODEL_TO_MARKET_OBJECTS');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 206, 'KO_MODEL');

INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 208, 'KO_GROUP_FACTOR');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 255, 'KO_CALC_GROUP');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 222, 'KO_GROUP_TO_MARKET_SEGMENT_RELATION');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 205, 'KO_GROUP');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 212, 'KO_TOUR_GROUPS');

INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 254, 'KO_COMPLIANCE_GUIDE');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 219, 'KO_TOUR_FACTOR_REGISTER');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 202, 'KO_TOUR');

INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 2, 'GBU_SOURCE2_A');
INSERT INTO common_registers_with_soft_deletion
	select nextval('REG_OBJECT_SEQ'), r.registerid, r.quant_table
	from core_register r
	where exists (select 1 
				 from ko_tour_factor_register fr where fr.register_id=r.registerid);				 
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 256, 'KO_UNIT_CHANGE');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 216, 'KO_COST_ROSREESTR');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 801, 'COMMON_IMPORT_DATA_LOG');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 975, 'CORE_LONG_PROCESS_QUEUE');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 201, 'KO_UNIT');
INSERT INTO common_registers_with_soft_deletion VALUES(nextval('REG_OBJECT_SEQ'), 203, 'KO_TASK');
