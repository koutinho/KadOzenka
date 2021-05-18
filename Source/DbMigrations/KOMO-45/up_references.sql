--справочник "Источник объявления"
delete from core_reference_item where referenceid=101 and itemid <> 1;

--справочник "Класс строения"
INSERT INTO core_reference_item (itemid, referenceid, code, value, short_title, is_archives, user_name, date_end_change, date_s, flag, name) 
VALUES (1000902, 116, '6', 'B-', null, null, null, null, null, null, 'Bminus');
