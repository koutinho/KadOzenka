INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10400100, '�������������', 104, 1, null, null, 'ID', null, null, 1, null, null, 'Id', 0, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10400200, '������������� ��������� �������', 104, 1, null, null, 'INITIAL_ID', null, null, null, null, null, 'InitialId', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10400300, '���� ������', 104, 5, null, null, 'CREATION_DATE', null, null, null, null, null, 'CreationDate', 1, null, null, null, 0, null, null, 0);
INSERT INTO core_register_attribute (id, name, registerid, type, parentid, referenceid, value_field, code_field, value_template, primary_key, user_key, qscolumn, internal_name, is_nullable, description, layout, export_column_name, is_deleted, change_user_id, change_date, hidden) VALUES (10400400, '��� ������', 104, 4, null, null, 'TYPE', null, null, null, null, null, 'Type', 1, null, null, null, 0, null, null, 0);


INSERT INTO core_register (registerid, registername, registerdescription, allpri_table, object_table, quant_table, track_changes_column, storage_type, object_sequence, is_virtual, contains_quant_in_future, db_connection_name, track_changes_userid, track_changes_date, is_deleted, allpri_partitioning, main_register) VALUES (104, 'Market.Screenshots', '�������, ���������� ���������� � ����������', null, null, 'MARKET_SCREENSHOTS', null, 4, 'REG_OBJECT_SEQ', 0, 0, null, null, null, 0, null, null);