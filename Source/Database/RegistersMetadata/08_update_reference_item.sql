
-- VI. Загрузка раскладок
--<DO>--
-- 12120002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '14',
        VALUE = 'Реестр полисов',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Policy'
    WHERE ITEMID = 12120002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120002, 12120, '14', 'Реестр полисов', NULL, NULL, NULL, NULL, NULL, NULL, 'Policy');
    END IF;
END $$;

--<DO>--
-- 12120003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '1',
        VALUE = 'Начисления',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Nach'
    WHERE ITEMID = 12120003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120003, 12120, '1', 'Начисления', NULL, NULL, NULL, NULL, NULL, NULL, 'Nach');
    END IF;
END $$;

--<DO>--
-- 12120004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '2',
        VALUE = 'Зачисления',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Strah'
    WHERE ITEMID = 12120004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120004, 12120, '2', 'Зачисления', NULL, NULL, NULL, NULL, NULL, NULL, 'Strah');
    END IF;
END $$;

--<DO>--
-- 12120007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '6',
        VALUE = 'Реестр страховых выплат по жилым помещениям',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InsurancePayments'
    WHERE ITEMID = 12120007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120007, 12120, '6', 'Реестр страховых выплат по жилым помещениям', NULL, NULL, NULL, NULL, NULL, NULL, 'InsurancePayments');
    END IF;
END $$;

--<DO>--
-- 12121004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12121,
        CODE = '3',
        VALUE = 'ГБУ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Gbu'
    WHERE ITEMID = 12121004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12121004, 12121, '3', 'ГБУ', NULL, 0, NULL, NULL, NULL, NULL, 'Gbu');
    END IF;
END $$;

--<DO>--
-- 10001094
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45318000',
        VALUE = 'Дорогомилово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001094;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001094, 12157, '45318000', 'Дорогомилово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 333002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 333,
        CODE = NULL,
        VALUE = 'Полис',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Polis'
    WHERE ITEMID = 333002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (333002, 333, NULL, 'Полис', NULL, NULL, NULL, NULL, NULL, NULL, 'Polis');
    END IF;
END $$;

--<DO>--
-- 333003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 333,
        CODE = NULL,
        VALUE = 'Свидетельство',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Svidetelstvo'
    WHERE ITEMID = 333003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (333003, 333, NULL, 'Свидетельство', NULL, NULL, NULL, NULL, NULL, NULL, 'Svidetelstvo');
    END IF;
END $$;

--<DO>--
-- 333004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 333,
        CODE = NULL,
        VALUE = 'Общее имущество',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ObscheeImuschestvo'
    WHERE ITEMID = 333004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (333004, 333, NULL, 'Общее имущество', NULL, NULL, NULL, NULL, NULL, NULL, 'ObscheeImuschestvo');
    END IF;
END $$;

--<DO>--
-- 10000257
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12119,
        CODE = NULL,
        VALUE = 'Обработан полностью',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ProcessedCompletely'
    WHERE ITEMID = 10000257;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000257, 12119, NULL, 'Обработан полностью', NULL, NULL, NULL, NULL, NULL, NULL, 'ProcessedCompletely');
    END IF;
END $$;

--<DO>--
-- 10000259
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12119,
        CODE = NULL,
        VALUE = 'Ошибки импорта',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ImportError'
    WHERE ITEMID = 10000259;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000259, 12119, NULL, 'Ошибки импорта', NULL, NULL, NULL, NULL, NULL, NULL, 'ImportError');
    END IF;
END $$;

--<DO>--
-- 10000261
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12119,
        CODE = NULL,
        VALUE = 'Не обработан',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NotProcessed'
    WHERE ITEMID = 10000261;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000261, 12119, NULL, 'Не обработан', NULL, NULL, NULL, NULL, NULL, NULL, 'NotProcessed');
    END IF;
END $$;

--<DO>--
-- 100008321
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12119,
        CODE = NULL,
        VALUE = 'Обрабатывается',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InProcess'
    WHERE ITEMID = 100008321;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (100008321, 12119, NULL, 'Обрабатывается', NULL, NULL, NULL, NULL, NULL, NULL, 'InProcess');
    END IF;
END $$;

--<DO>--
-- 10000107
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12093,
        CODE = NULL,
        VALUE = 'Идентифицирован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Identified'
    WHERE ITEMID = 10000107;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000107, 12093, NULL, 'Идентифицирован', NULL, NULL, NULL, NULL, NULL, NULL, 'Identified');
    END IF;
END $$;

--<DO>--
-- 10000109
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12093,
        CODE = NULL,
        VALUE = 'Частично идентифицирован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PartiallyIdentified'
    WHERE ITEMID = 10000109;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000109, 12093, NULL, 'Частично идентифицирован', NULL, NULL, NULL, NULL, NULL, NULL, 'PartiallyIdentified');
    END IF;
END $$;

--<DO>--
-- 10000111
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12093,
        CODE = NULL,
        VALUE = 'Не идентифицирован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NotIdentified'
    WHERE ITEMID = 10000111;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000111, 12093, NULL, 'Не идентифицирован', NULL, NULL, NULL, NULL, NULL, NULL, 'NotIdentified');
    END IF;
END $$;

--<DO>--
-- 12121001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12121,
        CODE = '1',
        VALUE = 'МФЦ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Mfc'
    WHERE ITEMID = 12121001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12121001, 12121, '1', 'МФЦ', NULL, 0, NULL, NULL, NULL, NULL, 'Mfc');
    END IF;
END $$;

--<DO>--
-- 12121002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12121,
        CODE = '2',
        VALUE = 'СК',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Sk'
    WHERE ITEMID = 12121002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12121002, 12121, '2', 'СК', NULL, 0, NULL, NULL, NULL, NULL, 'Sk');
    END IF;
END $$;

--<DO>--
-- 12120001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '13',
        VALUE = 'Банковские файлы оплат',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BankPayment'
    WHERE ITEMID = 12120001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120001, 12120, '13', 'Банковские файлы оплат', NULL, NULL, NULL, NULL, NULL, NULL, 'BankPayment');
    END IF;
END $$;

--<DO>--
-- 12122001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12122,
        CODE = '1',
        VALUE = 'Договор страхования жилых помещений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Dwelling'
    WHERE ITEMID = 12122001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12122001, 12122, '1', 'Договор страхования жилых помещений', NULL, NULL, NULL, NULL, NULL, NULL, 'Dwelling');
    END IF;
END $$;

--<DO>--
-- 12122002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12122,
        CODE = '2',
        VALUE = 'Договор страхования общего имущества',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CommonOwnership'
    WHERE ITEMID = 12122002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12122002, 12122, '2', 'Договор страхования общего имущества', NULL, NULL, NULL, NULL, NULL, NULL, 'CommonOwnership');
    END IF;
END $$;

--<DO>--
-- 12123001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12123,
        CODE = '1',
        VALUE = 'Полис',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Polis'
    WHERE ITEMID = 12123001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12123001, 12123, '1', 'Полис', NULL, NULL, NULL, NULL, NULL, NULL, 'Polis');
    END IF;
END $$;

--<DO>--
-- 12123002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12123,
        CODE = '2',
        VALUE = 'Свидетельство',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Certificate'
    WHERE ITEMID = 12123002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12123002, 12123, '2', 'Свидетельство', NULL, NULL, NULL, NULL, NULL, NULL, 'Certificate');
    END IF;
END $$;

--<DO>--
-- 12120005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '4',
        VALUE = 'Реестр расторгнутых полисов',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TerminatedPolicy'
    WHERE ITEMID = 12120005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120005, 12120, '4', 'Реестр расторгнутых полисов', NULL, NULL, NULL, NULL, NULL, NULL, 'TerminatedPolicy');
    END IF;
END $$;

--<DO>--
-- 12120006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '5',
        VALUE = 'Реестр свидетельств',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Certificate'
    WHERE ITEMID = 12120006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120006, 12120, '5', 'Реестр свидетельств', NULL, NULL, NULL, NULL, NULL, NULL, 'Certificate');
    END IF;
END $$;

--<DO>--
-- 12121003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12121,
        CODE = '5',
        VALUE = 'Банк',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Bank'
    WHERE ITEMID = 12121003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12121003, 12121, '5', 'Банк', NULL, 0, NULL, NULL, NULL, NULL, 'Bank');
    END IF;
END $$;

--<DO>--
-- 333001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 333,
        CODE = NULL,
        VALUE = 'ЕПД',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'EPD'
    WHERE ITEMID = 333001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (333001, 333, NULL, 'ЕПД', NULL, NULL, NULL, NULL, NULL, NULL, 'EPD');
    END IF;
END $$;

--<DO>--
-- 12120008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '7',
        VALUE = 'Реестр сведений об отказах  в страховых выплатах по жилым помещениям',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InsurancePaymentsRefusal'
    WHERE ITEMID = 12120008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120008, 12120, '7', 'Реестр сведений об отказах  в страховых выплатах по жилым помещениям', NULL, NULL, NULL, NULL, NULL, NULL, 'InsurancePaymentsRefusal');
    END IF;
END $$;

--<DO>--
-- 12124001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12124,
        CODE = '1',
        VALUE = 'Газовая',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'GasStove'
    WHERE ITEMID = 12124001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12124001, 12124, '1', 'Газовая', NULL, 0, '1', NULL, NULL, NULL, 'GasStove');
    END IF;
END $$;

--<DO>--
-- 12124002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12124,
        CODE = '2',
        VALUE = 'Электрическая',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ElectricalSotve'
    WHERE ITEMID = 12124002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12124002, 12124, '2', 'Электрическая', NULL, 0, '1', NULL, NULL, NULL, 'ElectricalSotve');
    END IF;
END $$;

--<DO>--
-- 12120009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '8',
        VALUE = 'Реестр сведений о заключенных договорах страхования общего имущества собственников помещений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InsuranceContractConcluded'
    WHERE ITEMID = 12120009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120009, 12120, '8', 'Реестр сведений о заключенных договорах страхования общего имущества собственников помещений', NULL, NULL, NULL, NULL, NULL, NULL, 'InsuranceContractConcluded');
    END IF;
END $$;

--<DO>--
-- 12120010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '9',
        VALUE = 'Реестр сведений о заключенных дополнительных соглашениях к договорам страхования общего имущества собственников помещений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AddInsuranceContractConcluded'
    WHERE ITEMID = 12120010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120010, 12120, '9', 'Реестр сведений о заключенных дополнительных соглашениях к договорам страхования общего имущества собственников помещений', NULL, NULL, NULL, NULL, NULL, NULL, 'AddInsuranceContractConcluded');
    END IF;
END $$;

--<DO>--
-- 12120011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '10',
        VALUE = 'Реестр сведений о поступивших платежах по договорам страхования общего имущества собственников помещений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PaymentReceived'
    WHERE ITEMID = 12120011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120011, 12120, '10', 'Реестр сведений о поступивших платежах по договорам страхования общего имущества собственников помещений', NULL, NULL, NULL, NULL, NULL, NULL, 'PaymentReceived');
    END IF;
END $$;

--<DO>--
-- 12120012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '11',
        VALUE = 'Cведения о страховых выплатах по договорам страхования общего имущества собственников помещений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InsurancePaymentsUnderContracts'
    WHERE ITEMID = 12120012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120012, 12120, '11', 'Cведения о страховых выплатах по договорам страхования общего имущества собственников помещений', NULL, NULL, NULL, NULL, NULL, NULL, 'InsurancePaymentsUnderContracts');
    END IF;
END $$;

--<DO>--
-- 12120013
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12120,
        CODE = '12',
        VALUE = 'Реестр сведений о заявленных и неурегулированных страховых событиях по договорам общего имущества собственников помещений и об отказах в страховых выплатах по ним',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DeclaredUnsettledInsuranceEvents'
    WHERE ITEMID = 12120013;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12120013, 12120, '12', 'Реестр сведений о заявленных и неурегулированных страховых событиях по договорам общего имущества собственников помещений и об отказах в страховых выплатах по ним', NULL, NULL, NULL, NULL, NULL, NULL, 'DeclaredUnsettledInsuranceEvents');
    END IF;
END $$;

--<DO>--
-- 12125002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '1',
        VALUE = 'Пожар',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Fire'
    WHERE ITEMID = 12125002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125002, 12125, '1', 'Пожар', NULL, 0, '1', NULL, NULL, NULL, 'Fire');
    END IF;
END $$;

--<DO>--
-- 12125003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '2',
        VALUE = 'Последствия тушения пожара',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ConsequencesOfFireExtinguishing'
    WHERE ITEMID = 12125003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125003, 12125, '2', 'Последствия тушения пожара', NULL, 0, '1', NULL, NULL, NULL, 'ConsequencesOfFireExtinguishing');
    END IF;
END $$;

--<DO>--
-- 12125004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '3',
        VALUE = 'Взрыв',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Explosion'
    WHERE ITEMID = 12125004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125004, 12125, '3', 'Взрыв', NULL, 0, '1', NULL, NULL, NULL, 'Explosion');
    END IF;
END $$;

--<DO>--
-- 12125005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '4',
        VALUE = 'Авария систем водоснабжения',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AccidentOfWaterSupplySystems'
    WHERE ITEMID = 12125005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125005, 12125, '4', 'Авария систем водоснабжения', NULL, 0, '1', NULL, NULL, NULL, 'AccidentOfWaterSupplySystems');
    END IF;
END $$;

--<DO>--
-- 12125006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '5',
        VALUE = 'Авария систем отопления',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AccidentOfHeatingSystems'
    WHERE ITEMID = 12125006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125006, 12125, '5', 'Авария систем отопления', NULL, 0, '1', NULL, NULL, NULL, 'AccidentOfHeatingSystems');
    END IF;
END $$;

--<DO>--
-- 12125007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '6',
        VALUE = 'Авария систем канализации',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AccidentOfSewageSystems'
    WHERE ITEMID = 12125007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125007, 12125, '6', 'Авария систем канализации', NULL, 0, '1', NULL, NULL, NULL, 'AccidentOfSewageSystems');
    END IF;
END $$;

--<DO>--
-- 12125008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '7',
        VALUE = 'Авария внутреннего водостока',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DownholeAccident'
    WHERE ITEMID = 12125008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125008, 12125, '7', 'Авария внутреннего водостока', NULL, 0, '1', NULL, NULL, NULL, 'DownholeAccident');
    END IF;
END $$;

--<DO>--
-- 12126001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '1',
        VALUE = 'Стены и перегородки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'WallsAndPartitions'
    WHERE ITEMID = 12126001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126001, 12126, '1', 'Стены и перегородки', NULL, 0, '1', NULL, NULL, NULL, 'WallsAndPartitions');
    END IF;
END $$;

--<DO>--
-- 12126002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '2',
        VALUE = 'Перекрытия',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Overlapping'
    WHERE ITEMID = 12126002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126002, 12126, '2', 'Перекрытия', NULL, 0, '1', NULL, NULL, NULL, 'Overlapping');
    END IF;
END $$;

--<DO>--
-- 12127001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12127,
        CODE = '1',
        VALUE = 'Полы дощатые',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'WoodFlooring'
    WHERE ITEMID = 12127001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12127001, 12127, '1', 'Полы дощатые', NULL, 0, '1', NULL, NULL, NULL, 'WoodFlooring');
    END IF;
END $$;

--<DO>--
-- 12127002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12127,
        CODE = '2',
        VALUE = 'Полы из линолеума, ламината',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LinoleumLaminateFlooring'
    WHERE ITEMID = 12127002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12127002, 12127, '2', 'Полы из линолеума, ламината', NULL, 0, '1', NULL, NULL, NULL, 'LinoleumLaminateFlooring');
    END IF;
END $$;

--<DO>--
-- 12127003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12127,
        CODE = '3',
        VALUE = 'Полы из паркета',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ParquetFlooring'
    WHERE ITEMID = 12127003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12127003, 12127, '3', 'Полы из паркета', NULL, 0, '1', NULL, NULL, NULL, 'ParquetFlooring');
    END IF;
END $$;

--<DO>--
-- 12128001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '0',
        VALUE = 'Причина, отличная от имеющих коды 1-12',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OtherReason'
    WHERE ITEMID = 12128001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128001, 12128, '0', 'Причина, отличная от имеющих коды 1-12', NULL, 0, '1', NULL, NULL, NULL, 'OtherReason');
    END IF;
END $$;

--<DO>--
-- 12128002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '1',
        VALUE = 'Повреждение жилого помещения произошло в результате события, которое не предусмотрено договором страхования (не страховой случай)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NoInsureCase'
    WHERE ITEMID = 12128002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128002, 12128, '1', 'Повреждение жилого помещения произошло в результате события, которое не предусмотрено договором страхования (не страховой случай)', NULL, 0, '1', NULL, NULL, NULL, 'NoInsureCase');
    END IF;
END $$;

--<DO>--
-- 12128003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '2',
        VALUE = 'Страховое событие произошло в неоплаченный период договора страхования',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NoPayPeriod'
    WHERE ITEMID = 12128003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128003, 12128, '2', 'Страховое событие произошло в неоплаченный период договора страхования', NULL, 0, '1', NULL, NULL, NULL, 'NoPayPeriod');
    END IF;
END $$;

--<DO>--
-- 12128004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '3',
        VALUE = 'Ущерб возмещен полностью по иному договору страхования или за счет средств виновной стороны',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PatGuiltySide'
    WHERE ITEMID = 12128004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128004, 12128, '3', 'Ущерб возмещен полностью по иному договору страхования или за счет средств виновной стороны', NULL, 0, '1', NULL, NULL, NULL, 'PatGuiltySide');
    END IF;
END $$;

--<DO>--
-- 12128005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '4',
        VALUE = 'Повреждения, связанные с предыдущими страховыми случаями (событиями), не устранены страхователем до наступления последующего страхового случая (нет увеличения ущерба по ранее поврежденным элементам конструкций)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DamageBeforeCase'
    WHERE ITEMID = 12128005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128005, 12128, '4', 'Повреждения, связанные с предыдущими страховыми случаями (событиями), не устранены страхователем до наступления последующего страхового случая (нет увеличения ущерба по ранее поврежденным элементам конструкций)', NULL, 0, '1', NULL, NULL, NULL, 'DamageBeforeCase');
    END IF;
END $$;

--<DO>--
-- 12128006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '5',
        VALUE = 'Ремонт произведен до осмотра квартиры представителем страховой организации',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RepairBefore'
    WHERE ITEMID = 12128006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128006, 12128, '5', 'Ремонт произведен до осмотра квартиры представителем страховой организации', NULL, 0, '1', NULL, NULL, NULL, 'RepairBefore');
    END IF;
END $$;

--<DO>--
-- 12128007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '6',
        VALUE = 'Страхователь не является гражданином России или является лицом без гражданства',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PolicyholderNotRF'
    WHERE ITEMID = 12128007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128007, 12128, '6', 'Страхователь не является гражданином России или является лицом без гражданства', NULL, 0, '1', NULL, NULL, NULL, 'PolicyholderNotRF');
    END IF;
END $$;

--<DO>--
-- 12128008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '7',
        VALUE = 'Страхователь, являясь собственником жилого помещения, не зарегистрирован в нем',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PolicyholderNotRegister'
    WHERE ITEMID = 12128008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128008, 12128, '7', 'Страхователь, являясь собственником жилого помещения, не зарегистрирован в нем', NULL, 0, '1', NULL, NULL, NULL, 'PolicyholderNotRegister');
    END IF;
END $$;

--<DO>--
-- 12128009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '8',
        VALUE = 'Жилое помещение до наступления страхового случая (события) признано в установленном порядке аварийным',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LivingPremiseEmergency'
    WHERE ITEMID = 12128009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128009, 12128, '8', 'Жилое помещение до наступления страхового случая (события) признано в установленном порядке аварийным', NULL, 0, '1', NULL, NULL, NULL, 'LivingPremiseEmergency');
    END IF;
END $$;

--<DO>--
-- 12128010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '9',
        VALUE = 'Жилое помещение расположено в доме, включенном Правительством Москвы в ежегодный перечень адресов жилых домов, подлежащих освобождению в связи со сносом, реконструкцией, переоборудованием в нежилые, изъятием земельного участка или по другим основаниям',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LivingPremiseMustDismissal'
    WHERE ITEMID = 12128010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128010, 12128, '9', 'Жилое помещение расположено в доме, включенном Правительством Москвы в ежегодный перечень адресов жилых домов, подлежащих освобождению в связи со сносом, реконструкцией, переоборудованием в нежилые, изъятием земельного участка или по другим основаниям', NULL, 0, '1', NULL, NULL, NULL, 'LivingPremiseMustDismissal');
    END IF;
END $$;

--<DO>--
-- 12128011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '10',
        VALUE = 'На жилое помещение обращено взыскание по обязательствам',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LivingPremiseMustPenalty'
    WHERE ITEMID = 12128011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128011, 12128, '10', 'На жилое помещение обращено взыскание по обязательствам', NULL, 0, '1', NULL, NULL, NULL, 'LivingPremiseMustPenalty');
    END IF;
END $$;

--<DO>--
-- 12128012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '11',
        VALUE = 'Жилое помещение подлежит конфискации',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LivingPremiseMustConfiscation'
    WHERE ITEMID = 12128012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128012, 12128, '11', 'Жилое помещение подлежит конфискации', NULL, 0, '1', NULL, NULL, NULL, 'LivingPremiseMustConfiscation');
    END IF;
END $$;

--<DO>--
-- 12128013
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12128,
        CODE = '12',
        VALUE = 'Прекращение права найма или права собственности на жилое помещение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LivingPremiseRightTermination'
    WHERE ITEMID = 12128013;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12128013, 12128, '12', 'Прекращение права найма или права собственности на жилое помещение', NULL, 0, '1', NULL, NULL, NULL, 'LivingPremiseRightTermination');
    END IF;
END $$;

--<DO>--
-- 12129001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '0',
        VALUE = 'Причина, отличная от имеющих коды 1-11',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OtherReason'
    WHERE ITEMID = 12129001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129001, 12129, '0', 'Причина, отличная от имеющих коды 1-11', NULL, 0, '1', NULL, NULL, NULL, 'OtherReason');
    END IF;
END $$;

--<DO>--
-- 12129002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '1',
        VALUE = 'Повреждение объекта общего имущества произошло в результате события, которое не предусмотрено договором страхования (не страховой случай)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NoInsureCase'
    WHERE ITEMID = 12129002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129002, 12129, '1', 'Повреждение объекта общего имущества произошло в результате события, которое не предусмотрено договором страхования (не страховой случай)', NULL, 0, '1', NULL, NULL, NULL, 'NoInsureCase');
    END IF;
END $$;

--<DO>--
-- 12129003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '2',
        VALUE = 'Страховое событие произошло в неоплаченный период договора страхования',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NoPayPeriod'
    WHERE ITEMID = 12129003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129003, 12129, '2', 'Страховое событие произошло в неоплаченный период договора страхования', NULL, 0, '1', NULL, NULL, NULL, 'NoPayPeriod');
    END IF;
END $$;

--<DO>--
-- 12129004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '3',
        VALUE = 'Ущерб возмещен полностью по иному договору страхования или за счет средств виновной стороны',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PatGuiltySide'
    WHERE ITEMID = 12129004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129004, 12129, '3', 'Ущерб возмещен полностью по иному договору страхования или за счет средств виновной стороны', NULL, 0, '1', NULL, NULL, NULL, 'PatGuiltySide');
    END IF;
END $$;

--<DO>--
-- 12126019
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '19',
        VALUE = 'Телевидение в т.ч.',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TelevisionIncl'
    WHERE ITEMID = 12126019;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126019, 12126, '19', 'Телевидение в т.ч.', NULL, 0, '1', NULL, NULL, NULL, 'TelevisionIncl');
    END IF;
END $$;

--<DO>--
-- 12126015
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '15',
        VALUE = 'Радио в т.ч.',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RadioIncl'
    WHERE ITEMID = 12126015;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126015, 12126, '15', 'Радио в т.ч.', NULL, 0, '1', NULL, NULL, NULL, 'RadioIncl');
    END IF;
END $$;

--<DO>--
-- 12126014
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '14',
        VALUE = 'Газоснабжение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'GasSupply'
    WHERE ITEMID = 12126014;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126014, 12126, '14', 'Газоснабжение', NULL, 0, '1', NULL, NULL, NULL, 'GasSupply');
    END IF;
END $$;

--<DO>--
-- 12125001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '10',
        VALUE = 'Иное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OtherReason'
    WHERE ITEMID = 12125001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125001, 12125, '10', 'Иное', NULL, 0, '1', NULL, NULL, NULL, 'OtherReason');
    END IF;
END $$;

--<DO>--
-- 12126013
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '13',
        VALUE = 'Электромонтажные работы',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ElectricInstallationWork'
    WHERE ITEMID = 12126013;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126013, 12126, '13', 'Электромонтажные работы', NULL, 0, '1', NULL, NULL, NULL, 'ElectricInstallationWork');
    END IF;
END $$;

--<DO>--
-- 12126012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '12',
        VALUE = 'Горячее водоснабжение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'HotWaterSupply'
    WHERE ITEMID = 12126012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126012, 12126, '12', 'Горячее водоснабжение', NULL, 0, '1', NULL, NULL, NULL, 'HotWaterSupply');
    END IF;
END $$;

--<DO>--
-- 12126011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '11',
        VALUE = 'Водопровод, канализация',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'WaterSupplyAndSewerage'
    WHERE ITEMID = 12126011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126011, 12126, '11', 'Водопровод, канализация', NULL, 0, '1', NULL, NULL, NULL, 'WaterSupplyAndSewerage');
    END IF;
END $$;

--<DO>--
-- 12126010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '10',
        VALUE = 'Центральное отопление',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CentralHeating'
    WHERE ITEMID = 12126010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126010, 12126, '10', 'Центральное отопление', NULL, 0, '1', NULL, NULL, NULL, 'CentralHeating');
    END IF;
END $$;

--<DO>--
-- 12126006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '6',
        VALUE = 'Отделочные работы, в т.ч.',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FinishingWorkIncl'
    WHERE ITEMID = 12126006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126006, 12126, '6', 'Отделочные работы, в т.ч.', NULL, 0, '1', NULL, NULL, NULL, 'FinishingWorkIncl');
    END IF;
END $$;

--<DO>--
-- 12126005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '5',
        VALUE = 'Полы',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Floors'
    WHERE ITEMID = 12126005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126005, 12126, '5', 'Полы', NULL, 0, '1', NULL, NULL, NULL, 'Floors');
    END IF;
END $$;

--<DO>--
-- 12126003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '3',
        VALUE = 'Проемы: окна',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OpeningWindows'
    WHERE ITEMID = 12126003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126003, 12126, '3', 'Проемы: окна', NULL, 0, '1', NULL, NULL, NULL, 'OpeningWindows');
    END IF;
END $$;

--<DO>--
-- 12129005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '4',
        VALUE = 'Повреждения, связанные с предыдущими страховыми случаями (событиями), не устранены страхователем до наступления последующего страхового случая (нет увеличения ущерба по ранее поврежденным объектам общего имущества)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DamageBeforeCase'
    WHERE ITEMID = 12129005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129005, 12129, '4', 'Повреждения, связанные с предыдущими страховыми случаями (событиями), не устранены страхователем до наступления последующего страхового случая (нет увеличения ущерба по ранее поврежденным объектам общего имущества)', NULL, 0, '1', NULL, NULL, NULL, 'DamageBeforeCase');
    END IF;
END $$;

--<DO>--
-- 12129006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '5',
        VALUE = 'Ремонт поврежденного объекта общего имущества произведен до его осмотра представителем страховой организации',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RepairBefore'
    WHERE ITEMID = 12129006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129006, 12129, '5', 'Ремонт поврежденного объекта общего имущества произведен до его осмотра представителем страховой организации', NULL, 0, '1', NULL, NULL, NULL, 'RepairBefore');
    END IF;
END $$;

--<DO>--
-- 12129007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '6',
        VALUE = 'Многоквартирный дом до наступления страхового случая (события) признан в установленном порядке аварийным или включен в ежегодный перечень адресов жилых домов, подлежащих освобождению в связи со сносом, реконструкцией, переоборудованием в нежилое строение, изъятием земельного участка или по другим основаниям',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CommonPropertyMustDismissal'
    WHERE ITEMID = 12129007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129007, 12129, '6', 'Многоквартирный дом до наступления страхового случая (события) признан в установленном порядке аварийным или включен в ежегодный перечень адресов жилых домов, подлежащих освобождению в связи со сносом, реконструкцией, переоборудованием в нежилое строение, изъятием земельного участка или по другим основаниям', NULL, 0, '1', NULL, NULL, NULL, 'CommonPropertyMustDismissal');
    END IF;
END $$;

--<DO>--
-- 12129008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '7',
        VALUE = 'Повреждение объектов общего имущества произошло вследствие умысла страхователя',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PolicyholderDamage'
    WHERE ITEMID = 12129008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129008, 12129, '7', 'Повреждение объектов общего имущества произошло вследствие умысла страхователя', NULL, 0, '1', NULL, NULL, NULL, 'PolicyholderDamage');
    END IF;
END $$;

--<DO>--
-- 12129009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '8',
        VALUE = 'Невыполнение страхователем правил пожарной безопасности, правил и норм технической эксплуатации жилищного фонда (отсутствует документальное подтверждение выполнения регламентных работ)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PolicyholderIgnoreFireRules'
    WHERE ITEMID = 12129009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129009, 12129, '8', 'Невыполнение страхователем правил пожарной безопасности, правил и норм технической эксплуатации жилищного фонда (отсутствует документальное подтверждение выполнения регламентных работ)', NULL, 0, '1', NULL, NULL, NULL, 'PolicyholderIgnoreFireRules');
    END IF;
END $$;

--<DO>--
-- 12129010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '9',
        VALUE = 'Невыполнение страхователем в установленный срок требований (предписаний) в отношении состояния застрахованного общего имущества, выданных соответствующими органами надзора',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PolicyholderIgnoreDemands'
    WHERE ITEMID = 12129010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129010, 12129, '9', 'Невыполнение страхователем в установленный срок требований (предписаний) в отношении состояния застрахованного общего имущества, выданных соответствующими органами надзора', NULL, 0, '1', NULL, NULL, NULL, 'PolicyholderIgnoreDemands');
    END IF;
END $$;

--<DO>--
-- 12129011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '10',
        VALUE = 'Размер причиненного ущерба меньше размера франшизы',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AmountDamageLessThanDeductible'
    WHERE ITEMID = 12129011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129011, 12129, '10', 'Размер причиненного ущерба меньше размера франшизы', NULL, 0, '1', NULL, NULL, NULL, 'AmountDamageLessThanDeductible');
    END IF;
END $$;

--<DO>--
-- 12129012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12129,
        CODE = '11',
        VALUE = 'В договоре страхования исчерпана страховая сумма по категории общего имущества, к которой относится застрахованный объект',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ExhaustedSumInsured'
    WHERE ITEMID = 12129012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12129012, 12129, '11', 'В договоре страхования исчерпана страховая сумма по категории общего имущества, к которой относится застрахованный объект', NULL, 0, '1', NULL, NULL, NULL, 'ExhaustedSumInsured');
    END IF;
END $$;

--<DO>--
-- 12130001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12130,
        CODE = '0',
        VALUE = 'Отсутствие заявления страхователя',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AbsenceStatementInsured'
    WHERE ITEMID = 12130001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12130001, 12130, '0', 'Отсутствие заявления страхователя', NULL, 0, '1', NULL, NULL, NULL, 'AbsenceStatementInsured');
    END IF;
END $$;

--<DO>--
-- 12130002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12130,
        CODE = '1',
        VALUE = 'Сбор документов страхователем',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CollectionDocumentsInsured'
    WHERE ITEMID = 12130002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12130002, 12130, '1', 'Сбор документов страхователем', NULL, 0, '1', NULL, NULL, NULL, 'CollectionDocumentsInsured');
    END IF;
END $$;

--<DO>--
-- 12130003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12130,
        CODE = '2',
        VALUE = 'Сбор документов страховой организацией',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CollectionDocumentsInsuranceOrganization'
    WHERE ITEMID = 12130003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12130003, 12130, '2', 'Сбор документов страховой организацией', NULL, 0, '1', NULL, NULL, NULL, 'CollectionDocumentsInsuranceOrganization');
    END IF;
END $$;

--<DO>--
-- 12130004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12130,
        CODE = '3',
        VALUE = 'Документов, представленных страхователем и собранных страховой организацией, недостаточно для принятия решения о выплате',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DocumentsNotEnoughDecision'
    WHERE ITEMID = 12130004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12130004, 12130, '3', 'Документов, представленных страхователем и собранных страховой организацией, недостаточно для принятия решения о выплате', NULL, 0, '1', NULL, NULL, NULL, 'DocumentsNotEnoughDecision');
    END IF;
END $$;

--<DO>--
-- 12130005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12130,
        CODE = '4',
        VALUE = 'Отказ страхователя от страхового возмещения',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RefusalPolicyholderCompensation'
    WHERE ITEMID = 12130005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12130005, 12130, '4', 'Отказ страхователя от страхового возмещения', NULL, 0, '1', NULL, NULL, NULL, 'RefusalPolicyholderCompensation');
    END IF;
END $$;

--<DO>--
-- 12130006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12130,
        CODE = '5',
        VALUE = 'Иная причина',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OtherReason'
    WHERE ITEMID = 12130006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12130006, 12130, '5', 'Иная причина', NULL, 0, '1', NULL, NULL, NULL, 'OtherReason');
    END IF;
END $$;

--<DO>--
-- 12132001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '1',
        VALUE = 'Здания полносборные из ж.б. панелей и/или блоков (до 4-х этажей включительно)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FullAssembledBuildingsFourFloors'
    WHERE ITEMID = 12132001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132001, 12132, '1', 'Здания полносборные из ж.б. панелей и/или блоков (до 4-х этажей включительно)', NULL, 0, '1', NULL, NULL, NULL, 'FullAssembledBuildingsFourFloors');
    END IF;
END $$;

--<DO>--
-- 12132002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '2',
        VALUE = 'Здания полносборные из ж.б. панелей и/или блоков (5-13 этажей включительно)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FullAssembledBuildingsThirteenFloors'
    WHERE ITEMID = 12132002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132002, 12132, '2', 'Здания полносборные из ж.б. панелей и/или блоков (5-13 этажей включительно)', NULL, 0, '1', NULL, NULL, NULL, 'FullAssembledBuildingsThirteenFloors');
    END IF;
END $$;

--<DO>--
-- 12132003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '3',
        VALUE = 'Здания полносборные из ж.б. панелей и/или блоков (14 этажей и выше)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FullAssembledBuildingsMoreFourteenFloors'
    WHERE ITEMID = 12132003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132003, 12132, '3', 'Здания полносборные из ж.б. панелей и/или блоков (14 этажей и выше)', NULL, 0, '1', NULL, NULL, NULL, 'FullAssembledBuildingsMoreFourteenFloors');
    END IF;
END $$;

--<DO>--
-- 12132004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '4',
        VALUE = 'Здания полносборные из ж.б. панелей и/или блоков (произвольной этажности)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FullAssembledBuildingsFreeFloors'
    WHERE ITEMID = 12132004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132004, 12132, '4', 'Здания полносборные из ж.б. панелей и/или блоков (произвольной этажности)', NULL, 0, '1', NULL, NULL, NULL, 'FullAssembledBuildingsFreeFloors');
    END IF;
END $$;

--<DO>--
-- 12132005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '5',
        VALUE = 'Здания кирпичные (до 4-х этажей включительно) c центральным отоплением и горячим водоснабжением от АГВ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BrickBuildingsFourFloorsCentralHeatingHotWaterAGV'
    WHERE ITEMID = 12132005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132005, 12132, '5', 'Здания кирпичные (до 4-х этажей включительно) c центральным отоплением и горячим водоснабжением от АГВ', NULL, 0, '1', NULL, NULL, NULL, 'BrickBuildingsFourFloorsCentralHeatingHotWaterAGV');
    END IF;
END $$;

--<DO>--
-- 12132006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '6',
        VALUE = 'Здания кирпичные (до 4-х этажей включительно) c центральным отоплением и горячим водоснабжением',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BrickBuildingsFourFloorsCentralHeatingHotWater'
    WHERE ITEMID = 12132006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132006, 12132, '6', 'Здания кирпичные (до 4-х этажей включительно) c центральным отоплением и горячим водоснабжением', NULL, 0, '1', NULL, NULL, NULL, 'BrickBuildingsFourFloorsCentralHeatingHotWater');
    END IF;
END $$;

--<DO>--
-- 12132007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '7',
        VALUE = 'Здания кирпичные (5-13 этажей включительно)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BrickBuildingsThirteenFloors'
    WHERE ITEMID = 12132007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132007, 12132, '7', 'Здания кирпичные (5-13 этажей включительно)', NULL, 0, '1', NULL, NULL, NULL, 'BrickBuildingsThirteenFloors');
    END IF;
END $$;

--<DO>--
-- 12132008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '8',
        VALUE = 'Здания кирпичные (14 этажей и выше)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BrickBuildingsMoreFourteenFloors'
    WHERE ITEMID = 12132008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132008, 12132, '8', 'Здания кирпичные (14 этажей и выше)', NULL, 0, '1', NULL, NULL, NULL, 'BrickBuildingsMoreFourteenFloors');
    END IF;
END $$;

--<DO>--
-- 12132009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '9',
        VALUE = 'Здания кирпичные (произвольной этажности) c железобетонными перекрытиями',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BrickBuildingsFreeFloorsReinforcedConcreteSlabs'
    WHERE ITEMID = 12132009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132009, 12132, '9', 'Здания кирпичные (произвольной этажности) c железобетонными перекрытиями', NULL, 0, '1', NULL, NULL, NULL, 'BrickBuildingsFreeFloorsReinforcedConcreteSlabs');
    END IF;
END $$;

--<DO>--
-- 12132010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '10',
        VALUE = 'Здания кирпичные (произвольной этажности) c деревянными перекрытиями',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BrickBuildingsFreeFloorsWoodenFloors'
    WHERE ITEMID = 12132010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132010, 12132, '10', 'Здания кирпичные (произвольной этажности) c деревянными перекрытиями', NULL, 0, '1', NULL, NULL, NULL, 'BrickBuildingsFreeFloorsWoodenFloors');
    END IF;
END $$;

--<DO>--
-- 12132011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '11',
        VALUE = 'Здания из облегчённых блоков (до 5-ти этажей включительно)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LightBlockBuildingsFiveFloors'
    WHERE ITEMID = 12132011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132011, 12132, '11', 'Здания из облегчённых блоков (до 5-ти этажей включительно)', NULL, 0, '1', NULL, NULL, NULL, 'LightBlockBuildingsFiveFloors');
    END IF;
END $$;

--<DO>--
-- 12132012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '12',
        VALUE = 'Здания из облегчённых блоков (произвольной этажности c ж.б. перекрытиями)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LightBlockBuildingsFreeFloorsReinforcedConcreteSlabs'
    WHERE ITEMID = 12132012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132012, 12132, '12', 'Здания из облегчённых блоков (произвольной этажности c ж.б. перекрытиями)', NULL, 0, '1', NULL, NULL, NULL, 'LightBlockBuildingsFreeFloorsReinforcedConcreteSlabs');
    END IF;
END $$;

--<DO>--
-- 12132013
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '13',
        VALUE = 'Здания из облегчённых блоков (произвольной этажности c дер/перекрытиями)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LightBlockBuildingsFreeFloorsWoodenFloors'
    WHERE ITEMID = 12132013;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132013, 12132, '13', 'Здания из облегчённых блоков (произвольной этажности c дер/перекрытиями)', NULL, 0, '1', NULL, NULL, NULL, 'LightBlockBuildingsFreeFloorsWoodenFloors');
    END IF;
END $$;

--<DO>--
-- 12132014
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '14',
        VALUE = 'Здания смешанные (до 3-х этажей включительно)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MixBuildingsThreeFloors'
    WHERE ITEMID = 12132014;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132014, 12132, '14', 'Здания смешанные (до 3-х этажей включительно)', NULL, 0, '1', NULL, NULL, NULL, 'MixBuildingsThreeFloors');
    END IF;
END $$;

--<DO>--
-- 12132015
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '15',
        VALUE = 'Здания смешанные (произвольной этажности)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MixBuildingsFreeFloors'
    WHERE ITEMID = 12132015;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132015, 12132, '15', 'Здания смешанные (произвольной этажности)', NULL, 0, '1', NULL, NULL, NULL, 'MixBuildingsFreeFloors');
    END IF;
END $$;

--<DO>--
-- 12132016
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '16',
        VALUE = 'Здания брусчатые или бревенчатые (до 3-х этажей включительно)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PavedLogBuildingsThreeFloors'
    WHERE ITEMID = 12132016;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132016, 12132, '16', 'Здания брусчатые или бревенчатые (до 3-х этажей включительно)', NULL, 0, '1', NULL, NULL, NULL, 'PavedLogBuildingsThreeFloors');
    END IF;
END $$;

--<DO>--
-- 12132017
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '17',
        VALUE = 'Здания из монолитного бетона (до 4-х этажей включительно)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MonolithicConcreteBuildingsFourFloors'
    WHERE ITEMID = 12132017;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132017, 12132, '17', 'Здания из монолитного бетона (до 4-х этажей включительно)', NULL, 0, '1', NULL, NULL, NULL, 'MonolithicConcreteBuildingsFourFloors');
    END IF;
END $$;

--<DO>--
-- 12132018
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '18',
        VALUE = 'Здания из монолитного бетона (5-13 этажей включительно)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MonolithicConcreteBuildingsThirteenFloors'
    WHERE ITEMID = 12132018;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132018, 12132, '18', 'Здания из монолитного бетона (5-13 этажей включительно)', NULL, 0, '1', NULL, NULL, NULL, 'MonolithicConcreteBuildingsThirteenFloors');
    END IF;
END $$;

--<DO>--
-- 12132019
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '19',
        VALUE = 'Здания из монолитного бетона  (14 этажей и выше)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MonolithicConcreteBuildingsMoreFourteenFloors'
    WHERE ITEMID = 12132019;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132019, 12132, '19', 'Здания из монолитного бетона  (14 этажей и выше)', NULL, 0, '1', NULL, NULL, NULL, 'MonolithicConcreteBuildingsMoreFourteenFloors');
    END IF;
END $$;

--<DO>--
-- 12132020
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12132,
        CODE = '20',
        VALUE = 'Здания из монолитного бетона (произвольной этажности)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MonolithicConcreteBuildingsFreeFloors'
    WHERE ITEMID = 12132020;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12132020, 12132, '20', 'Здания из монолитного бетона (произвольной этажности)', NULL, 0, '1', NULL, NULL, NULL, 'MonolithicConcreteBuildingsFreeFloors');
    END IF;
END $$;

--<DO>--
-- 301001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 301,
        CODE = '1',
        VALUE = 'ЕГРН',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Egrn'
    WHERE ITEMID = 301001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (301001, 301, '1', 'ЕГРН', NULL, 0, '1', NULL, NULL, NULL, 'Egrn');
    END IF;
END $$;

--<DO>--
-- 301002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 301,
        CODE = '2',
        VALUE = 'БТИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Bti'
    WHERE ITEMID = 301002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (301002, 301, '2', 'БТИ', NULL, 0, '1', NULL, NULL, NULL, 'Bti');
    END IF;
END $$;

--<DO>--
-- 301003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 301,
        CODE = '3',
        VALUE = 'Ручной ввод',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ManualInput'
    WHERE ITEMID = 301003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (301003, 301, '3', 'Ручной ввод', NULL, 0, '1', NULL, NULL, NULL, 'ManualInput');
    END IF;
END $$;

--<DO>--
-- 301004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 301,
        CODE = '4',
        VALUE = 'МФЦ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Mfc'
    WHERE ITEMID = 301004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (301004, 301, '4', 'МФЦ', NULL, 0, '1', NULL, NULL, NULL, 'Mfc');
    END IF;
END $$;

--<DO>--
-- 301005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 301,
        CODE = '5',
        VALUE = 'СК',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InsuranceCompany'
    WHERE ITEMID = 301005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (301005, 301, '5', 'СК', NULL, 0, '1', NULL, NULL, NULL, 'InsuranceCompany');
    END IF;
END $$;

--<DO>--
-- 10001095
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45319000',
        VALUE = 'Крылатское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001095;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001095, 12157, '45319000', 'Крылатское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001096
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45320000',
        VALUE = 'Кунцево',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001096;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001096, 12157, '45320000', 'Кунцево', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001097
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45321000',
        VALUE = 'Можайский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001097;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001097, 12157, '45321000', 'Можайский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001098
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45322000',
        VALUE = 'Ново-Переделкино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001098;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001098, 12157, '45322000', 'Ново-Переделкино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 53833
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 28,
        CODE = NULL,
        VALUE = 'нумерованный',
        SHORT_TITLE = 'нум',
        IS_ARCHIVES = 0,
        USER_NAME = 'andreeva',
        DATE_END_CHANGE = '2013-07-16 00:00:00',
        DATE_S = '2013-07-16 00:00:00',
        FLAG = NULL,
        NAME = 'Numbered'
    WHERE ITEMID = 53833;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (53833, 28, NULL, 'нумерованный', 'нум', 0, 'andreeva', '2013-07-16 00:00:00', '2013-07-16 00:00:00', NULL, 'Numbered');
    END IF;
END $$;

--<DO>--
-- 10001088
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45312000',
        VALUE = 'Перово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001088;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001088, 12157, '45312000', 'Перово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001089
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45313000',
        VALUE = 'Северное Измайлово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001089;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001089, 12157, '45313000', 'Северное Измайлово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001090
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45314000',
        VALUE = 'Соколиная гора',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001090;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001090, 12157, '45314000', 'Соколиная гора', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 53813
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 38,
        CODE = NULL,
        VALUE = 'епее',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 1,
        USER_NAME = 'andreeva',
        DATE_END_CHANGE = '2013-07-04 00:00:00',
        DATE_S = '2013-07-04 00:00:00',
        FLAG = NULL,
        NAME = 'Epee'
    WHERE ITEMID = 53813;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (53813, 38, NULL, 'епее', NULL, 1, 'andreeva', '2013-07-04 00:00:00', '2013-07-04 00:00:00', NULL, 'Epee');
    END IF;
END $$;

--<DO>--
-- 10001091
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45315000',
        VALUE = 'Сокольники',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001091;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001091, 12157, '45315000', 'Сокольники', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001092
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45316000',
        VALUE = 'Преображенское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001092;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001092, 12157, '45316000', 'Преображенское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001093
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45317000',
        VALUE = 'Внуково',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001093;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001093, 12157, '45317000', 'Внуково', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001211
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45941000',
        VALUE = 'поселение Внуковское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001211;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001211, 12157, '45941000', 'поселение Внуковское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001212
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45942000',
        VALUE = 'поселение Воскресенское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001212;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001212, 12157, '45942000', 'поселение Воскресенское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001213
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45944000',
        VALUE = 'поселение Десеновское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001213;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001213, 12157, '45944000', 'поселение Десеновское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001214
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45947000',
        VALUE = 'поселение Кокошкино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001214;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001214, 12157, '45947000', 'поселение Кокошкино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001215
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45949000',
        VALUE = 'поселение Марушкинское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001215;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001215, 12157, '45949000', 'поселение Марушкинское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001216
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45952000',
        VALUE = 'поселение Московский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001216;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001216, 12157, '45952000', 'поселение Московский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001217
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45953000',
        VALUE = 'поселение "Мосрентген"',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001217;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001217, 12157, '45953000', 'поселение "Мосрентген"', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001218
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45957000',
        VALUE = 'поселение Рязановское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001218;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001218, 12157, '45957000', 'поселение Рязановское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001219
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45958000',
        VALUE = 'поселение Сосенское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001219;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001219, 12157, '45958000', 'поселение Сосенское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001220
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45959000',
        VALUE = 'поселение Филимонковское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001220;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001220, 12157, '45959000', 'поселение Филимонковское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001221
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45932000',
        VALUE = 'городской округ Щербинка',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001221;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001221, 12157, '45932000', 'городской округ Щербинка', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001222
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45943000',
        VALUE = 'поселение Вороновское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001222;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001222, 12157, '45943000', 'поселение Вороновское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001223
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45945000',
        VALUE = 'поселение Киевский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001223;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001223, 12157, '45945000', 'поселение Киевский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001224
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45946000',
        VALUE = 'поселение Кленовское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001224;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001224, 12157, '45946000', 'поселение Кленовское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001225
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45948000',
        VALUE = 'поселение Краснопахорское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001225;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001225, 12157, '45948000', 'поселение Краснопахорское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001226
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45951000',
        VALUE = 'поселение Михайлово-Ярцевское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001226;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001226, 12157, '45951000', 'поселение Михайлово-Ярцевское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001227
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45954000',
        VALUE = 'поселение Новофедоровское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001227;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001227, 12157, '45954000', 'поселение Новофедоровское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001228
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45955000',
        VALUE = 'поселение Первомайское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001228;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001228, 12157, '45955000', 'поселение Первомайское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001229
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45956000',
        VALUE = 'поселение Роговское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001229;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001229, 12157, '45956000', 'поселение Роговское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001230
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45931000',
        VALUE = 'городской округ Троицк',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001230;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001230, 12157, '45931000', 'городской округ Троицк', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001231
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45961000',
        VALUE = 'поселение Щаповское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001231;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001231, 12157, '45961000', 'поселение Щаповское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001077
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45301000',
        VALUE = 'Богородское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001077;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001077, 12157, '45301000', 'Богородское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001078
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45302000',
        VALUE = 'Вешняки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001078;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001078, 12157, '45302000', 'Вешняки', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001079
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45303000',
        VALUE = 'Восточное Измайлово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001079;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001079, 12157, '45303000', 'Восточное Измайлово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001080
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45304000',
        VALUE = 'Восточный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001080;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001080, 12157, '45304000', 'Восточный', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001081
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45305000',
        VALUE = 'Гольяново',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001081;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001081, 12157, '45305000', 'Гольяново', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001082
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45306000',
        VALUE = 'Ивановское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001082;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001082, 12157, '45306000', 'Ивановское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001083
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45307000',
        VALUE = 'Измайлово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001083;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001083, 12157, '45307000', 'Измайлово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001084
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45308000',
        VALUE = 'Косино-Ухтомский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001084;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001084, 12157, '45308000', 'Косино-Ухтомский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001085
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45309000',
        VALUE = 'Новогиреево',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001085;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001085, 12157, '45309000', 'Новогиреево', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001086
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45310000',
        VALUE = 'Новокосино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001086;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001086, 12157, '45310000', 'Новокосино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001087
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45311000',
        VALUE = 'Метрогородок',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001087;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001087, 12157, '45311000', 'Метрогородок', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001099
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45323000',
        VALUE = 'Очаково-Матвеевское',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001099;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001099, 12157, '45323000', 'Очаково-Матвеевское', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001100
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45324000',
        VALUE = 'Проспект Вернадского',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001100;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001100, 12157, '45324000', 'Проспект Вернадского', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001101
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45325000',
        VALUE = 'Раменки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001101;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001101, 12157, '45325000', 'Раменки', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001102
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45326000',
        VALUE = 'Солнцево',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001102;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001102, 12157, '45326000', 'Солнцево', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001103
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45327000',
        VALUE = 'Тропарево-Никулино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001103;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001103, 12157, '45327000', 'Тропарево-Никулино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001104
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45328000',
        VALUE = 'Филевский парк',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001104;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001104, 12157, '45328000', 'Филевский парк', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001105
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45329000',
        VALUE = 'Фили-Давыдково',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001105;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001105, 12157, '45329000', 'Фили-Давыдково', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001106
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45330000',
        VALUE = 'Крюково',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001106;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001106, 12157, '45330000', 'Крюково', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001107
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45331000',
        VALUE = 'Матушкино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001107;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001107, 12157, '45331000', 'Матушкино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001108
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45332000',
        VALUE = 'Силино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001108;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001108, 12157, '45332000', 'Силино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001109
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45333000',
        VALUE = 'Аэропорт',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001109;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001109, 12157, '45333000', 'Аэропорт', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001110
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45334000',
        VALUE = 'Беговой',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001110;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001110, 12157, '45334000', 'Беговой', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001111
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45335000',
        VALUE = 'Бескудниковский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001111;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001111, 12157, '45335000', 'Бескудниковский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001112
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45336000',
        VALUE = 'Войковский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001112;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001112, 12157, '45336000', 'Войковский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001113
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45337000',
        VALUE = 'Восточное Дегунино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001113;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001113, 12157, '45337000', 'Восточное Дегунино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001114
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45338000',
        VALUE = 'Головинский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001114;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001114, 12157, '45338000', 'Головинский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001115
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45339000',
        VALUE = 'Дмитровский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001115;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001115, 12157, '45339000', 'Дмитровский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001116
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45340000',
        VALUE = 'Западное Дегунино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001116;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001116, 12157, '45340000', 'Западное Дегунино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001117
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45341000',
        VALUE = 'Коптево',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001117;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001117, 12157, '45341000', 'Коптево', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001118
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45342000',
        VALUE = 'Левобережный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001118;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001118, 12157, '45342000', 'Левобережный', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001119
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45343000',
        VALUE = 'Молжаниновский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001119;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001119, 12157, '45343000', 'Молжаниновский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001120
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45344000',
        VALUE = 'Савеловский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001120;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001120, 12157, '45344000', 'Савеловский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001121
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45345000',
        VALUE = 'Сокол',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001121;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001121, 12157, '45345000', 'Сокол', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001122
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45346000',
        VALUE = 'Тимирязевский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001122;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001122, 12157, '45346000', 'Тимирязевский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001123
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45347000',
        VALUE = 'Ховрино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001123;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001123, 12157, '45347000', 'Ховрино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001124
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45348000',
        VALUE = 'Хорошевский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001124;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001124, 12157, '45348000', 'Хорошевский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001125
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45349000',
        VALUE = 'Алексеевский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Alekseevski'
    WHERE ITEMID = 10001125;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001125, 12157, '45349000', 'Алексеевский', NULL, NULL, NULL, NULL, NULL, NULL, 'Alekseevski');
    END IF;
END $$;

--<DO>--
-- 10001126
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45350000',
        VALUE = 'Алтуфьевский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Altufievski'
    WHERE ITEMID = 10001126;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001126, 12157, '45350000', 'Алтуфьевский', NULL, NULL, NULL, NULL, NULL, NULL, 'Altufievski');
    END IF;
END $$;

--<DO>--
-- 10001127
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45351000',
        VALUE = 'Бабушкинский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001127;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001127, 12157, '45351000', 'Бабушкинский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001128
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45352000',
        VALUE = 'Бибирево',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001128;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001128, 12157, '45352000', 'Бибирево', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001129
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45353000',
        VALUE = 'Бутырский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001129;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001129, 12157, '45353000', 'Бутырский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001130
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45354000',
        VALUE = 'Лианозово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001130;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001130, 12157, '45354000', 'Лианозово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001131
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45355000',
        VALUE = 'Лосиноостровский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001131;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001131, 12157, '45355000', 'Лосиноостровский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001132
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45356000',
        VALUE = 'Марфино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001132;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001132, 12157, '45356000', 'Марфино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001133
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45357000',
        VALUE = 'Марьина роща',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001133;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001133, 12157, '45357000', 'Марьина роща', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001134
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45358000',
        VALUE = 'Останкинский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001134;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001134, 12157, '45358000', 'Останкинский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001135
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45359000',
        VALUE = 'Отрадное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001135;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001135, 12157, '45359000', 'Отрадное', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001136
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45360000',
        VALUE = 'Ростокино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001136;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001136, 12157, '45360000', 'Ростокино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001137
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45361000',
        VALUE = 'Свиблово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001137;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001137, 12157, '45361000', 'Свиблово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001138
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45362000',
        VALUE = 'Северное Медведково',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001138;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001138, 12157, '45362000', 'Северное Медведково', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001139
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45363000',
        VALUE = 'Северный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001139;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001139, 12157, '45363000', 'Северный', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001140
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45364000',
        VALUE = 'Южное Медведково',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001140;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001140, 12157, '45364000', 'Южное Медведково', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001141
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45365000',
        VALUE = 'Ярославский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001141;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001141, 12157, '45365000', 'Ярославский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001142
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45366000',
        VALUE = 'Куркино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001142;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001142, 12157, '45366000', 'Куркино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001143
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45367000',
        VALUE = 'Митино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001143;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001143, 12157, '45367000', 'Митино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001144
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45368000',
        VALUE = 'Покровское-Стрешнево',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001144;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001144, 12157, '45368000', 'Покровское-Стрешнево', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001145
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45369000',
        VALUE = 'Северное Тушино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001145;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001145, 12157, '45369000', 'Северное Тушино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001146
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45370000',
        VALUE = 'Строгино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001146;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001146, 12157, '45370000', 'Строгино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001147
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45371000',
        VALUE = 'Хорошево-Мневники',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001147;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001147, 12157, '45371000', 'Хорошево-Мневники', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001148
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45372000',
        VALUE = 'Щукино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001148;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001148, 12157, '45372000', 'Щукино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001149
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45373000',
        VALUE = 'Южное Тушино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001149;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001149, 12157, '45373000', 'Южное Тушино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001150
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45374000',
        VALUE = 'Арбат',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001150;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001150, 12157, '45374000', 'Арбат', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001151
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45375000',
        VALUE = 'Басманный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001151;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001151, 12157, '45375000', 'Басманный', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001152
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45376000',
        VALUE = 'Замоскворечье',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001152;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001152, 12157, '45376000', 'Замоскворечье', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001153
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45377000',
        VALUE = 'Савелки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001153;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001153, 12157, '45377000', 'Савелки', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001154
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45378000',
        VALUE = 'Красносельский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001154;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001154, 12157, '45378000', 'Красносельский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001155
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45379000',
        VALUE = 'Мещанский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001155;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001155, 12157, '45379000', 'Мещанский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001156
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45380000',
        VALUE = 'Пресненский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001156;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001156, 12157, '45380000', 'Пресненский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001157
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45381000',
        VALUE = 'Таганский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001157;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001157, 12157, '45381000', 'Таганский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001158
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45382000',
        VALUE = 'Тверской',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001158;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001158, 12157, '45382000', 'Тверской', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001159
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45383000',
        VALUE = 'Хамовники',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001159;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001159, 12157, '45383000', 'Хамовники', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001160
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45384000',
        VALUE = 'Якиманка',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001160;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001160, 12157, '45384000', 'Якиманка', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001170
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45385000',
        VALUE = 'Выхино-Жулебино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001170;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001170, 12157, '45385000', 'Выхино-Жулебино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001171
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45386000',
        VALUE = 'Капотня',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001171;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001171, 12157, '45386000', 'Капотня', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001172
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45387000',
        VALUE = 'Кузьминки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001172;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001172, 12157, '45387000', 'Кузьминки', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001173
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45388000',
        VALUE = 'Лефортово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001173;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001173, 12157, '45388000', 'Лефортово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001174
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45389000',
        VALUE = 'Люблино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001174;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001174, 12157, '45389000', 'Люблино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001175
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45390000',
        VALUE = 'Марьино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001175;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001175, 12157, '45390000', 'Марьино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001176
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45391000',
        VALUE = 'Некрасовка',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001176;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001176, 12157, '45391000', 'Некрасовка', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001177
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45392000',
        VALUE = 'Нижегородский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001177;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001177, 12157, '45392000', 'Нижегородский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001178
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45393000',
        VALUE = 'Печатники',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001178;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001178, 12157, '45393000', 'Печатники', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001179
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45394000',
        VALUE = 'Рязанский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001179;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001179, 12157, '45394000', 'Рязанский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001180
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45395000',
        VALUE = 'Текстильщики',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001180;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001180, 12157, '45395000', 'Текстильщики', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001181
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45396000',
        VALUE = 'Южнопортовый',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001181;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001181, 12157, '45396000', 'Южнопортовый', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001182
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45397000',
        VALUE = 'Академический',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Akademicheski'
    WHERE ITEMID = 10001182;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001182, 12157, '45397000', 'Академический', NULL, NULL, NULL, NULL, NULL, NULL, 'Akademicheski');
    END IF;
END $$;

--<DO>--
-- 10001183
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45398000',
        VALUE = 'Гагаринский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001183;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001183, 12157, '45398000', 'Гагаринский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001184
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45901000',
        VALUE = 'Зюзино',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001184;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001184, 12157, '45901000', 'Зюзино', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001185
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45902000',
        VALUE = 'Коньково',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001185;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001185, 12157, '45902000', 'Коньково', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001186
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45903000',
        VALUE = 'Котловка',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001186;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001186, 12157, '45903000', 'Котловка', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001187
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45904000',
        VALUE = 'Ломоносовский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001187;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001187, 12157, '45904000', 'Ломоносовский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001188
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45905000',
        VALUE = 'Обручевский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001188;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001188, 12157, '45905000', 'Обручевский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001189
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45906000',
        VALUE = 'Северное Бутово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001189;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001189, 12157, '45906000', 'Северное Бутово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001190
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45907000',
        VALUE = 'Теплый Стан',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001190;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001190, 12157, '45907000', 'Теплый Стан', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001191
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45908000',
        VALUE = 'Черемушки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001191;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001191, 12157, '45908000', 'Черемушки', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001192
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45909000',
        VALUE = 'Южное Бутово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001192;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001192, 12157, '45909000', 'Южное Бутово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001193
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45910000',
        VALUE = 'Ясенево',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001193;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001193, 12157, '45910000', 'Ясенево', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001194
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45911000',
        VALUE = 'Бирюлево Восточное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001194;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001194, 12157, '45911000', 'Бирюлево Восточное', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001195
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45912000',
        VALUE = 'Бирюлево Западное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001195;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001195, 12157, '45912000', 'Бирюлево Западное', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001196
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45913000',
        VALUE = 'Братеево',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001196;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001196, 12157, '45913000', 'Братеево', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001197
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45914000',
        VALUE = 'Даниловский',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001197;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001197, 12157, '45914000', 'Даниловский', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001198
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45915000',
        VALUE = 'Донской',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001198;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001198, 12157, '45915000', 'Донской', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001199
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45916000',
        VALUE = 'Зябликово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001199;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001199, 12157, '45916000', 'Зябликово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001200
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45917000',
        VALUE = 'Москворечье-Сабурово',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001200;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001200, 12157, '45917000', 'Москворечье-Сабурово', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001201
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45918000',
        VALUE = 'Нагатино-Садовники',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001201;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001201, 12157, '45918000', 'Нагатино-Садовники', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001202
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45919000',
        VALUE = 'Нагатинский затон',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001202;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001202, 12157, '45919000', 'Нагатинский затон', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001203
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45920000',
        VALUE = 'Нагорный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001203;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001203, 12157, '45920000', 'Нагорный', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001204
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45921000',
        VALUE = 'Орехово-Борисово Северное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001204;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001204, 12157, '45921000', 'Орехово-Борисово Северное', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001205
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45922000',
        VALUE = 'Орехово-Борисово Южное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001205;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001205, 12157, '45922000', 'Орехово-Борисово Южное', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001206
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45923000',
        VALUE = 'Царицыно',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001206;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001206, 12157, '45923000', 'Царицыно', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001207
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45924000',
        VALUE = 'Чертаново Северное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001207;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001207, 12157, '45924000', 'Чертаново Северное', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001208
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45925000',
        VALUE = 'Чертаново Центральное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001208;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001208, 12157, '45925000', 'Чертаново Центральное', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001209
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45926000',
        VALUE = 'Чертаново Южное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001209;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001209, 12157, '45926000', 'Чертаново Южное', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10001210
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12157,
        CODE = '45927000',
        VALUE = 'Старое Крюково',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = NULL
    WHERE ITEMID = 10001210;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001210, 12157, '45927000', 'Старое Крюково', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
    END IF;
END $$;

--<DO>--
-- 10007575
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'DNPP',
        VALUE = 'ДНиПП',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DNP'
    WHERE ITEMID = 10007575;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10007575, 4, 'DNPP', 'ДНиПП', NULL, 0, NULL, NULL, NULL, NULL, 'DNP');
    END IF;
END $$;

--<DO>--
-- 100007607
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = NULL,
        VALUE = 'FLS2005',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Fls2005'
    WHERE ITEMID = 100007607;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (100007607, 4, NULL, 'FLS2005', NULL, NULL, NULL, NULL, NULL, NULL, 'Fls2005');
    END IF;
END $$;

--<DO>--
-- 10001065
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'MS4',
        VALUE = 'MS4',
        SHORT_TITLE = 'MS4',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MS4'
    WHERE ITEMID = 10001065;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001065, 4, 'MS4', 'MS4', 'MS4', 0, NULL, NULL, NULL, NULL, 'MS4');
    END IF;
END $$;

--<DO>--
-- 10000072
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'DJP',
        VALUE = 'ДЖПиЖФ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DJP'
    WHERE ITEMID = 10000072;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000072, 4, 'DJP', 'ДЖПиЖФ', NULL, 0, NULL, NULL, NULL, NULL, 'DJP');
    END IF;
END $$;

--<DO>--
-- 1301733
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'INVENT',
        VALUE = 'Инвентаризация БТИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InvBTI'
    WHERE ITEMID = 1301733;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301733, 4, 'INVENT', 'Инвентаризация БТИ', NULL, 0, NULL, NULL, NULL, NULL, 'InvBTI');
    END IF;
END $$;

--<DO>--
-- 1281646
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'Rosreestr',
        VALUE = 'Росреестр',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Rosreestr'
    WHERE ITEMID = 1281646;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1281646, 4, 'Rosreestr', 'Росреестр', NULL, 0, NULL, NULL, NULL, NULL, 'Rosreestr');
    END IF;
END $$;

--<DO>--
-- 1299941
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = '01',
        VALUE = 'БТИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BTI'
    WHERE ITEMID = 1299941;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1299941, 4, '01', 'БТИ', NULL, 0, NULL, NULL, NULL, NULL, 'BTI');
    END IF;
END $$;

--<DO>--
-- 1300124
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'JSC',
        VALUE = 'Акционерные общества',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'JSC'
    WHERE ITEMID = 1300124;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1300124, 4, 'JSC', 'Акционерные общества', NULL, 0, NULL, NULL, NULL, NULL, 'JSC');
    END IF;
END $$;

--<DO>--
-- 1300125
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'UE',
        VALUE = 'Государственные унитарные предприятия',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'UE'
    WHERE ITEMID = 1300125;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1300125, 4, 'UE', 'Государственные унитарные предприятия', NULL, 0, NULL, NULL, NULL, NULL, 'UE');
    END IF;
END $$;

--<DO>--
-- 1300126
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'PO',
        VALUE = 'Государственные учреждения',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PO'
    WHERE ITEMID = 1300126;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1300126, 4, 'PO', 'Государственные учреждения', NULL, 0, NULL, NULL, NULL, NULL, 'PO');
    END IF;
END $$;

--<DO>--
-- 1299693
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'N2',
        VALUE = 'Н2',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'N2'
    WHERE ITEMID = 1299693;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1299693, 4, 'N2', 'Н2', NULL, 0, NULL, NULL, NULL, NULL, 'N2');
    END IF;
END $$;

--<DO>--
-- 12067
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'REON',
        VALUE = 'РЕОН',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'REON'
    WHERE ITEMID = 12067;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12067, 4, 'REON', 'РЕОН', NULL, 0, NULL, NULL, NULL, NULL, 'REON');
    END IF;
END $$;

--<DO>--
-- 1300544
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'SPD',
        VALUE = 'СПД',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SPD'
    WHERE ITEMID = 1300544;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1300544, 4, 'SPD', 'СПД', NULL, 0, NULL, NULL, NULL, NULL, 'SPD');
    END IF;
END $$;

--<DO>--
-- 10007576
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'ISIO',
        VALUE = 'ИСИО',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Isio'
    WHERE ITEMID = 10007576;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10007576, 4, 'ISIO', 'ИСИО', NULL, 0, NULL, NULL, NULL, NULL, 'Isio');
    END IF;
END $$;

--<DO>--
-- 1301280
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'GUPMGGT',
        VALUE = 'ГУП Мосгоргеотрест',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'GUPMGGT'
    WHERE ITEMID = 1301280;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301280, 4, 'GUPMGGT', 'ГУП Мосгоргеотрест', NULL, 0, NULL, NULL, NULL, NULL, 'GUPMGGT');
    END IF;
END $$;

--<DO>--
-- 1412413
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'FP',
        VALUE = 'ФП',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = 'PSV',
        DATE_END_CHANGE = '2015-08-10 00:00:00',
        DATE_S = '2015-08-10 00:00:00',
        FLAG = NULL,
        NAME = 'FP'
    WHERE ITEMID = 1412413;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1412413, 4, 'FP', 'ФП', NULL, 0, 'PSV', '2015-08-10 00:00:00', '2015-08-10 00:00:00', NULL, 'FP');
    END IF;
END $$;

--<DO>--
-- 10007606
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'ASUR_NSI',
        VALUE = 'АС УР (НСИ)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AsurNsi'
    WHERE ITEMID = 10007606;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10007606, 4, 'ASUR_NSI', 'АС УР (НСИ)', NULL, NULL, NULL, NULL, NULL, NULL, 'AsurNsi');
    END IF;
END $$;

--<DO>--
-- 1000235005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = NULL,
        VALUE = 'Реестр договоров',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RegisterOfContracts'
    WHERE ITEMID = 1000235005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1000235005, 4, NULL, 'Реестр договоров', NULL, 0, '1', NULL, NULL, NULL, 'RegisterOfContracts');
    END IF;
END $$;

--<DO>--
-- 10001064
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'InvDGI',
        VALUE = 'Инвентаризация ДГИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InvDGI'
    WHERE ITEMID = 10001064;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10001064, 4, 'InvDGI', 'Инвентаризация ДГИ', NULL, 0, NULL, NULL, NULL, NULL, 'InvDGI');
    END IF;
END $$;

--<DO>--
-- 1301061
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 4,
        CODE = 'CombinedView',
        VALUE = 'Комбинированное представление',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CombinedView'
    WHERE ITEMID = 1301061;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301061, 4, 'CombinedView', 'Комбинированное представление', NULL, NULL, NULL, NULL, NULL, NULL, 'CombinedView');
    END IF;
END $$;

--<DO>--
-- 12121005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12121,
        CODE = '4',
        VALUE = 'БТИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Bti'
    WHERE ITEMID = 12121005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12121005, 12121, '4', 'БТИ', NULL, 0, NULL, NULL, NULL, NULL, 'Bti');
    END IF;
END $$;

--<DO>--
-- 12126026
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '26',
        VALUE = 'Прочие',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Other'
    WHERE ITEMID = 12126026;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126026, 12126, '26', 'Прочие', NULL, 0, '1', NULL, NULL, NULL, 'Other');
    END IF;
END $$;

--<DO>--
-- 12126022
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '22',
        VALUE = 'Телефон в т.ч.',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TelephoneIncl'
    WHERE ITEMID = 12126022;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126022, 12126, '22', 'Телефон в т.ч.', NULL, 0, '1', NULL, NULL, NULL, 'TelephoneIncl');
    END IF;
END $$;

--<DO>--
-- 1216001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12160,
        CODE = '1',
        VALUE = 'Ранее учтенный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PreviouslyPosted'
    WHERE ITEMID = 1216001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216001, 12160, '1', 'Ранее учтенный', NULL, NULL, NULL, NULL, NULL, NULL, 'PreviouslyPosted');
    END IF;
END $$;

--<DO>--
-- 1216002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12160,
        CODE = '2',
        VALUE = 'Временный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Temporary'
    WHERE ITEMID = 1216002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216002, 12160, '2', 'Временный', NULL, NULL, NULL, NULL, NULL, NULL, 'Temporary');
    END IF;
END $$;

--<DO>--
-- 1216003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12160,
        CODE = '3',
        VALUE = 'Учтенный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Posted'
    WHERE ITEMID = 1216003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216003, 12160, '3', 'Учтенный', NULL, NULL, NULL, NULL, NULL, NULL, 'Posted');
    END IF;
END $$;

--<DO>--
-- 1216004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12160,
        CODE = '4',
        VALUE = 'Снят с учета',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RemovedFromRegister'
    WHERE ITEMID = 1216004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216004, 12160, '4', 'Снят с учета', NULL, NULL, NULL, NULL, NULL, NULL, 'RemovedFromRegister');
    END IF;
END $$;

--<DO>--
-- 1216005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12160,
        CODE = '5',
        VALUE = 'Аннулированный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Cancelled'
    WHERE ITEMID = 1216005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216005, 12160, '5', 'Аннулированный', NULL, NULL, NULL, NULL, NULL, NULL, 'Cancelled');
    END IF;
END $$;

--<DO>--
-- 1216101
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12161,
        CODE = '1',
        VALUE = 'Квартира',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Flat'
    WHERE ITEMID = 1216101;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216101, 12161, '1', 'Квартира', NULL, NULL, NULL, NULL, NULL, NULL, 'Flat');
    END IF;
END $$;

--<DO>--
-- 1216102
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12161,
        CODE = '2',
        VALUE = 'Комната',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Room'
    WHERE ITEMID = 1216102;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216102, 12161, '2', 'Комната', NULL, NULL, NULL, NULL, NULL, NULL, 'Room');
    END IF;
END $$;

--<DO>--
-- 12126004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '4',
        VALUE = 'Проемы: двери',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OpeningDoors'
    WHERE ITEMID = 12126004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126004, 12126, '4', 'Проемы: двери', NULL, 0, '1', NULL, NULL, NULL, 'OpeningDoors');
    END IF;
END $$;

--<DO>--
-- 12158001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12158,
        CODE = '1',
        VALUE = 'Загружен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Loaded'
    WHERE ITEMID = 12158001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12158001, 12158, '1', 'Загружен', NULL, 0, '1', NULL, NULL, NULL, 'Loaded');
    END IF;
END $$;

--<DO>--
-- 12158002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12158,
        CODE = '2',
        VALUE = 'Обработан',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Processed'
    WHERE ITEMID = 12158002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12158002, 12158, '2', 'Обработан', NULL, 0, '1', NULL, NULL, NULL, 'Processed');
    END IF;
END $$;

--<DO>--
-- 12159001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12159,
        CODE = '1',
        VALUE = 'Сформирован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Formed'
    WHERE ITEMID = 12159001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12159001, 12159, '1', 'Сформирован', NULL, NULL, NULL, NULL, NULL, NULL, 'Formed');
    END IF;
END $$;

--<DO>--
-- 12159002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12159,
        CODE = '2',
        VALUE = 'Выгружен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Unloaded'
    WHERE ITEMID = 12159002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12159002, 12159, '2', 'Выгружен', NULL, NULL, NULL, NULL, NULL, NULL, 'Unloaded');
    END IF;
END $$;

--<DO>--
-- 12159003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12159,
        CODE = '3',
        VALUE = 'Передан на согласование',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SubmittedApproval'
    WHERE ITEMID = 12159003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12159003, 12159, '3', 'Передан на согласование', NULL, NULL, NULL, NULL, NULL, NULL, 'SubmittedApproval');
    END IF;
END $$;

--<DO>--
-- 12159004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12159,
        CODE = '4',
        VALUE = 'Согласован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Agreed'
    WHERE ITEMID = 12159004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12159004, 12159, '4', 'Согласован', NULL, NULL, NULL, NULL, NULL, NULL, 'Agreed');
    END IF;
END $$;

--<DO>--
-- 12159005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12159,
        CODE = '5',
        VALUE = 'Не согласован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NotAgreed'
    WHERE ITEMID = 12159005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12159005, 12159, '5', 'Не согласован', NULL, NULL, NULL, NULL, NULL, NULL, 'NotAgreed');
    END IF;
END $$;

--<DO>--
-- 12159006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12159,
        CODE = '6',
        VALUE = 'Повторно выгружен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Reloaded'
    WHERE ITEMID = 12159006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12159006, 12159, '6', 'Повторно выгружен', NULL, NULL, NULL, NULL, NULL, NULL, 'Reloaded');
    END IF;
END $$;

--<DO>--
-- 1216201
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12162,
        CODE = '1',
        VALUE = 'Жилое помещение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Dwelling'
    WHERE ITEMID = 1216201;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216201, 12162, '1', 'Жилое помещение', NULL, NULL, NULL, NULL, NULL, NULL, 'Dwelling');
    END IF;
END $$;

--<DO>--
-- 1216202
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12162,
        CODE = '2',
        VALUE = 'Нежилое помещение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'UninhabitedPremise'
    WHERE ITEMID = 1216202;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216202, 12162, '2', 'Нежилое помещение', NULL, NULL, NULL, NULL, NULL, NULL, 'UninhabitedPremise');
    END IF;
END $$;

--<DO>--
-- 1281445
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 60,
        CODE = '005001001000',
        VALUE = 'Нежилое здание',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NonresidentialBuilding'
    WHERE ITEMID = 1281445;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1281445, 60, '005001001000', 'Нежилое здание', NULL, NULL, NULL, NULL, NULL, NULL, 'NonresidentialBuilding');
    END IF;
END $$;

--<DO>--
-- 1281446
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 60,
        CODE = '005001002000',
        VALUE = 'Жилой дом',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ResidentialBuilding'
    WHERE ITEMID = 1281446;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1281446, 60, '005001002000', 'Жилой дом', NULL, NULL, NULL, NULL, NULL, NULL, 'ResidentialBuilding');
    END IF;
END $$;

--<DO>--
-- 1281447
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 60,
        CODE = '005001003000',
        VALUE = 'Многоквартирный дом',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ApartmentBuilding'
    WHERE ITEMID = 1281447;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1281447, 60, '005001003000', 'Многоквартирный дом', NULL, NULL, NULL, NULL, NULL, NULL, 'ApartmentBuilding');
    END IF;
END $$;

--<DO>--
-- 1281448
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 60,
        CODE = '005001999000',
        VALUE = 'Иное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Otherwise'
    WHERE ITEMID = 1281448;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1281448, 60, '005001999000', 'Иное', NULL, NULL, NULL, NULL, NULL, NULL, 'Otherwise');
    END IF;
END $$;

--<DO>--
-- 12133001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12133,
        CODE = NULL,
        VALUE = 'Частная',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PrivateProperty'
    WHERE ITEMID = 12133001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12133001, 12133, NULL, 'Частная', NULL, NULL, NULL, NULL, NULL, NULL, 'PrivateProperty');
    END IF;
END $$;

--<DO>--
-- 12133002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12133,
        CODE = NULL,
        VALUE = 'Общая',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CommonProperty'
    WHERE ITEMID = 12133002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12133002, 12133, NULL, 'Общая', NULL, NULL, NULL, NULL, NULL, NULL, 'CommonProperty');
    END IF;
END $$;

--<DO>--
-- 100008322
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12119,
        CODE = NULL,
        VALUE = 'Загружен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Loaded'
    WHERE ITEMID = 100008322;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (100008322, 12119, NULL, 'Загружен', NULL, NULL, NULL, NULL, NULL, NULL, 'Loaded');
    END IF;
END $$;

--<DO>--
-- 1216301
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '1',
        VALUE = 'Аварийный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Emergency'
    WHERE ITEMID = 1216301;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216301, 12163, '1', 'Аварийный', NULL, NULL, NULL, NULL, NULL, NULL, 'Emergency');
    END IF;
END $$;

--<DO>--
-- 1216302
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '2',
        VALUE = 'Общежитие (весь дом)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Hostel'
    WHERE ITEMID = 1216302;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216302, 12163, '2', 'Общежитие (весь дом)', NULL, NULL, NULL, NULL, NULL, NULL, 'Hostel');
    END IF;
END $$;

--<DO>--
-- 1216303
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '3',
        VALUE = 'Часть дома общежитие',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PartHostel'
    WHERE ITEMID = 1216303;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216303, 12163, '3', 'Часть дома общежитие', NULL, NULL, NULL, NULL, NULL, NULL, 'PartHostel');
    END IF;
END $$;

--<DO>--
-- 1216304
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '4',
        VALUE = 'В программе реновации',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Renovation'
    WHERE ITEMID = 1216304;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216304, 12163, '4', 'В программе реновации', NULL, NULL, NULL, NULL, NULL, NULL, 'Renovation');
    END IF;
END $$;

--<DO>--
-- 1216305
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '5',
        VALUE = 'На реконструкции (капремонте)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Reconstruction'
    WHERE ITEMID = 1216305;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216305, 12163, '5', 'На реконструкции (капремонте)', NULL, NULL, NULL, NULL, NULL, NULL, 'Reconstruction');
    END IF;
END $$;

--<DO>--
-- 1216306
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '6',
        VALUE = 'Отселяется',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Resettled'
    WHERE ITEMID = 1216306;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216306, 12163, '6', 'Отселяется', NULL, NULL, NULL, NULL, NULL, NULL, 'Resettled');
    END IF;
END $$;

--<DO>--
-- 1216307
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '7',
        VALUE = 'Строится',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LineUp'
    WHERE ITEMID = 1216307;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216307, 12163, '7', 'Строится', NULL, NULL, NULL, NULL, NULL, NULL, 'LineUp');
    END IF;
END $$;

--<DO>--
-- 1216308
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '8',
        VALUE = 'Построен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Built'
    WHERE ITEMID = 1216308;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216308, 12163, '8', 'Построен', NULL, NULL, NULL, NULL, NULL, NULL, 'Built');
    END IF;
END $$;

--<DO>--
-- 1216309
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '9',
        VALUE = 'Заселен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Populated'
    WHERE ITEMID = 1216309;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216309, 12163, '9', 'Заселен', NULL, NULL, NULL, NULL, NULL, NULL, 'Populated');
    END IF;
END $$;

--<DO>--
-- 1216310
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '10',
        VALUE = 'МКД без ЕПД',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MKDwithoutEPD'
    WHERE ITEMID = 1216310;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216310, 12163, '10', 'МКД без ЕПД', NULL, NULL, NULL, NULL, NULL, NULL, 'MKDwithoutEPD');
    END IF;
END $$;

--<DO>--
-- 1216311
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '11',
        VALUE = 'переведен в нежилой',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Transferred'
    WHERE ITEMID = 1216311;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216311, 12163, '11', 'переведен в нежилой', NULL, NULL, NULL, NULL, NULL, NULL, 'Transferred');
    END IF;
END $$;

--<DO>--
-- 1216312
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '12',
        VALUE = 'не МКД',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NotMKD'
    WHERE ITEMID = 1216312;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216312, 12163, '12', 'не МКД', NULL, NULL, NULL, NULL, NULL, NULL, 'NotMKD');
    END IF;
END $$;

--<DO>--
-- 1216313
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '13',
        VALUE = 'нежилой',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Uninhabited'
    WHERE ITEMID = 1216313;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216313, 12163, '13', 'нежилой', NULL, NULL, NULL, NULL, NULL, NULL, 'Uninhabited');
    END IF;
END $$;

--<DO>--
-- 1216314
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '14',
        VALUE = 'элитный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Elite'
    WHERE ITEMID = 1216314;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216314, 12163, '14', 'элитный', NULL, NULL, NULL, NULL, NULL, NULL, 'Elite');
    END IF;
END $$;

--<DO>--
-- 1216315
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12163,
        CODE = '15',
        VALUE = 'нет доступа на свою территорию',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NoAccess'
    WHERE ITEMID = 1216315;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216315, 12163, '15', 'нет доступа на свою территорию', NULL, NULL, NULL, NULL, NULL, NULL, 'NoAccess');
    END IF;
END $$;

--<DO>--
-- 12131002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12131,
        CODE = '1',
        VALUE = 'Отдельная квартира',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SingleApartment'
    WHERE ITEMID = 12131002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12131002, 12131, '1', 'Отдельная квартира', NULL, 0, '1', NULL, NULL, NULL, 'SingleApartment');
    END IF;
END $$;

--<DO>--
-- 12131003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12131,
        CODE = '2',
        VALUE = 'Коммунальная квартира',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CommunalApartmentRoom'
    WHERE ITEMID = 12131003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12131003, 12131, '2', 'Коммунальная квартира', NULL, 0, '1', NULL, NULL, NULL, 'CommunalApartmentRoom');
    END IF;
END $$;

--<DO>--
-- 12131004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12131,
        CODE = '3',
        VALUE = 'Отдельная квартира в долевой собственности',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SingleApartmentSharedOwnership'
    WHERE ITEMID = 12131004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12131004, 12131, '3', 'Отдельная квартира в долевой собственности', NULL, 0, '1', NULL, NULL, NULL, 'SingleApartmentSharedOwnership');
    END IF;
END $$;

--<DO>--
-- 305001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 305,
        CODE = '6',
        VALUE = 'ЖСК/ЖК',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'GskGk'
    WHERE ITEMID = 305001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (305001, 305, '6', 'ЖСК/ЖК', NULL, NULL, NULL, NULL, NULL, NULL, 'GskGk');
    END IF;
END $$;

--<DO>--
-- 305002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 305,
        CODE = '7',
        VALUE = 'ТСЖ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Tsg'
    WHERE ITEMID = 305002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (305002, 305, '7', 'ТСЖ', NULL, NULL, NULL, NULL, NULL, NULL, 'Tsg');
    END IF;
END $$;

--<DO>--
-- 305003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 305,
        CODE = '8',
        VALUE = 'БО',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Bo'
    WHERE ITEMID = 305003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (305003, 305, '8', 'БО', NULL, NULL, NULL, NULL, NULL, NULL, 'Bo');
    END IF;
END $$;

--<DO>--
-- 304001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 304,
        CODE = '1',
        VALUE = 'Жилые помещения по общему тарифу',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ComonRate'
    WHERE ITEMID = 304001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (304001, 304, '1', 'Жилые помещения по общему тарифу', NULL, NULL, NULL, NULL, NULL, NULL, 'ComonRate');
    END IF;
END $$;

--<DO>--
-- 304002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 304,
        CODE = '2',
        VALUE = 'Жилые помещения по индивидуальному тарифу',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'IndividualRate'
    WHERE ITEMID = 304002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (304002, 304, '2', 'Жилые помещения по индивидуальному тарифу', NULL, NULL, NULL, NULL, NULL, NULL, 'IndividualRate');
    END IF;
END $$;

--<DO>--
-- 304003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 304,
        CODE = '3',
        VALUE = 'Общее имущество',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CommonProperty'
    WHERE ITEMID = 304003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (304003, 304, '3', 'Общее имущество', NULL, NULL, NULL, NULL, NULL, NULL, 'CommonProperty');
    END IF;
END $$;

--<DO>--
-- 306001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 306,
        CODE = '1',
        VALUE = 'Полный платеж',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FullPayment'
    WHERE ITEMID = 306001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (306001, 306, '1', 'Полный платеж', NULL, NULL, NULL, NULL, NULL, NULL, 'FullPayment');
    END IF;
END $$;

--<DO>--
-- 306002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 306,
        CODE = '2',
        VALUE = 'По полугодиям',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PaymentByHalfYear'
    WHERE ITEMID = 306002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (306002, 306, '2', 'По полугодиям', NULL, NULL, NULL, NULL, NULL, NULL, 'PaymentByHalfYear');
    END IF;
END $$;

--<DO>--
-- 306003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 306,
        CODE = '3',
        VALUE = 'По кварталам',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PaymentByQuarter'
    WHERE ITEMID = 306003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (306003, 306, '3', 'По кварталам', NULL, NULL, NULL, NULL, NULL, NULL, 'PaymentByQuarter');
    END IF;
END $$;

--<DO>--
-- 306004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 306,
        CODE = '4',
        VALUE = 'По месяцам',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PaymentByMonth'
    WHERE ITEMID = 306004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (306004, 306, '4', 'По месяцам', NULL, NULL, NULL, NULL, NULL, NULL, 'PaymentByMonth');
    END IF;
END $$;

--<DO>--
-- 306005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 306,
        CODE = '5',
        VALUE = 'Иная рассрочка',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OtherInstallments'
    WHERE ITEMID = 306005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (306005, 306, '5', 'Иная рассрочка', NULL, NULL, NULL, NULL, NULL, NULL, 'OtherInstallments');
    END IF;
END $$;

--<DO>--
-- 1216401
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12164,
        CODE = '0',
        VALUE = 'Общие',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Common'
    WHERE ITEMID = 1216401;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216401, 12164, '0', 'Общие', NULL, NULL, NULL, NULL, NULL, NULL, 'Common');
    END IF;
END $$;

--<DO>--
-- 1216402
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12164,
        CODE = '1',
        VALUE = 'Особые',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Special'
    WHERE ITEMID = 1216402;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1216402, 12164, '1', 'Особые', NULL, NULL, NULL, NULL, NULL, NULL, 'Special');
    END IF;
END $$;

--<DO>--
-- 12165001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Создано',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Created'
    WHERE ITEMID = 12165001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165001, 12165, NULL, 'Создано', NULL, NULL, NULL, NULL, NULL, NULL, 'Created');
    END IF;
END $$;

--<DO>--
-- 303003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 303,
        CODE = '3',
        VALUE = 'Проект договора',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ProjectAgreement'
    WHERE ITEMID = 303003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (303003, 303, '3', 'Проект договора', NULL, 0, '1', NULL, NULL, NULL, 'ProjectAgreement');
    END IF;
END $$;

--<DO>--
-- 12123003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12123,
        CODE = '3',
        VALUE = 'ЕПД',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'EPD'
    WHERE ITEMID = 12123003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12123003, 12123, '3', 'ЕПД', NULL, NULL, NULL, NULL, NULL, NULL, 'EPD');
    END IF;
END $$;

--<DO>--
-- 12167001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12167,
        CODE = NULL,
        VALUE = 'Бумажная',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DocTypePaper'
    WHERE ITEMID = 12167001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12167001, 12167, NULL, 'Бумажная', NULL, NULL, NULL, NULL, NULL, NULL, 'DocTypePaper');
    END IF;
END $$;

--<DO>--
-- 12167002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12167,
        CODE = NULL,
        VALUE = 'Электронная',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DocTypeElectro'
    WHERE ITEMID = 12167002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12167002, 12167, NULL, 'Электронная', NULL, NULL, NULL, NULL, NULL, NULL, 'DocTypeElectro');
    END IF;
END $$;

--<DO>--
-- 301006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 301,
        CODE = '6',
        VALUE = 'Система',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'System'
    WHERE ITEMID = 301006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (301006, 301, '6', 'Система', NULL, 0, '1', NULL, NULL, NULL, 'System');
    END IF;
END $$;

--<DO>--
-- 12166001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '1',
        VALUE = 'Для зачислений МФЦ отсутствуют соответствующие строки банка',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NotIdentifiedPlat'
    WHERE ITEMID = 12166001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166001, 12166, '1', 'Для зачислений МФЦ отсутствуют соответствующие строки банка', NULL, NULL, NULL, NULL, NULL, NULL, 'NotIdentifiedPlat');
    END IF;
END $$;

--<DO>--
-- 12166002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '2',
        VALUE = 'Для банковских строк оплат отсутствуют соответствующие зачисления в данных МФЦ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BankPlatWithoutPlat'
    WHERE ITEMID = 12166002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166002, 12166, '2', 'Для банковских строк оплат отсутствуют соответствующие зачисления в данных МФЦ', NULL, NULL, NULL, NULL, NULL, NULL, 'BankPlatWithoutPlat');
    END IF;
END $$;

--<DO>--
-- 12166010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '10',
        VALUE = 'В данных МФЦ квартира присутствует, а объект страхования (квартира) в Системе не найден',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FlatNotFound'
    WHERE ITEMID = 12166010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166010, 12166, '10', 'В данных МФЦ квартира присутствует, а объект страхования (квартира) в Системе не найден', NULL, NULL, NULL, NULL, NULL, NULL, 'FlatNotFound');
    END IF;
END $$;

--<DO>--
-- 12166011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '11',
        VALUE = 'В данных МФЦ неверная общая площадь квартиры',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FlatOplMismatch'
    WHERE ITEMID = 12166011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166011, 12166, '11', 'В данных МФЦ неверная общая площадь квартиры', NULL, NULL, NULL, NULL, NULL, NULL, 'FlatOplMismatch');
    END IF;
END $$;

--<DO>--
-- 12166012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '12',
        VALUE = 'В данных МФЦ неверное количество комнат в квартире',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FlatKolgpMismatch'
    WHERE ITEMID = 12166012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166012, 12166, '12', 'В данных МФЦ неверное количество комнат в квартире', NULL, NULL, NULL, NULL, NULL, NULL, 'FlatKolgpMismatch');
    END IF;
END $$;

--<DO>--
-- 12166003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '3',
        VALUE = 'Расхождения в суммах начислений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CalcSumMismatch'
    WHERE ITEMID = 12166003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166003, 12166, '3', 'Расхождения в суммах начислений', NULL, NULL, NULL, NULL, NULL, NULL, 'CalcSumMismatch');
    END IF;
END $$;

--<DO>--
-- 12166004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '4',
        VALUE = 'UNOM с разными адресами',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'UnomAddressMismatch'
    WHERE ITEMID = 12166004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166004, 12166, '4', 'UNOM с разными адресами', NULL, NULL, NULL, NULL, NULL, NULL, 'UnomAddressMismatch');
    END IF;
END $$;

--<DO>--
-- 12166005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '5',
        VALUE = 'Подозрительные UNOM',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SuspiciousUnom'
    WHERE ITEMID = 12166005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166005, 12166, '5', 'Подозрительные UNOM', NULL, NULL, NULL, NULL, NULL, NULL, 'SuspiciousUnom');
    END IF;
END $$;

--<DO>--
-- 12166006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '6',
        VALUE = 'Несовпадение NOM+NOMI',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KvnomNomMismatch'
    WHERE ITEMID = 12166006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166006, 12166, '6', 'Несовпадение NOM+NOMI', NULL, NULL, NULL, NULL, NULL, NULL, 'KvnomNomMismatch');
    END IF;
END $$;

--<DO>--
-- 12166007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '7',
        VALUE = 'Наличие более одного начисления на одного плательщика',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MoreThanOneNach'
    WHERE ITEMID = 12166007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166007, 12166, '7', 'Наличие более одного начисления на одного плательщика', NULL, NULL, NULL, NULL, NULL, NULL, 'MoreThanOneNach');
    END IF;
END $$;

--<DO>--
-- 12166008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '8',
        VALUE = 'Есть начисление, нет площади',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NachWithoutOpl'
    WHERE ITEMID = 12166008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166008, 12166, '8', 'Есть начисление, нет площади', NULL, NULL, NULL, NULL, NULL, NULL, 'NachWithoutOpl');
    END IF;
END $$;

--<DO>--
-- 12166009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12166,
        CODE = '9',
        VALUE = 'Площадь страхования не совпадает с площадью квартиры, для отдельных квартир',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FoplOplMismatch'
    WHERE ITEMID = 12166009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12166009, 12166, '9', 'Площадь страхования не совпадает с площадью квартиры, для отдельных квартир', NULL, NULL, NULL, NULL, NULL, NULL, 'FoplOplMismatch');
    END IF;
END $$;

--<DO>--
-- 1290855
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Универсальный',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Universalnyj'
    WHERE ITEMID = 1290855;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290855, 12029, NULL, 'Универсальный', NULL, 0, NULL, NULL, NULL, NULL, 'Universalnyj');
    END IF;
END $$;

--<DO>--
-- 1301161
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Инвестиционный договор',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InvestitsionnyjDogovor'
    WHERE ITEMID = 1301161;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301161, 12029, NULL, 'Инвестиционный договор', NULL, NULL, NULL, NULL, NULL, NULL, 'InvestitsionnyjDogovor');
    END IF;
END $$;

--<DO>--
-- 1301162
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '46',
        VALUE = 'Залог',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Zalog'
    WHERE ITEMID = 1301162;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301162, 12029, '46', 'Залог', NULL, NULL, NULL, NULL, NULL, NULL, 'Zalog');
    END IF;
END $$;

--<DO>--
-- 1301164
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Договор простого товарищества',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DogovorProstogoTovarishestva'
    WHERE ITEMID = 1301164;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301164, 12029, NULL, 'Договор простого товарищества', NULL, NULL, NULL, NULL, NULL, NULL, 'DogovorProstogoTovarishestva');
    END IF;
END $$;

--<DO>--
-- 1301165
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Иные обременения',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InyeObremeneniya'
    WHERE ITEMID = 1301165;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301165, 12029, NULL, 'Иные обременения', NULL, NULL, NULL, NULL, NULL, NULL, 'InyeObremeneniya');
    END IF;
END $$;

--<DO>--
-- 1301166
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Иное право пользования',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InoePravoPolzovaniya'
    WHERE ITEMID = 1301166;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301166, 12029, NULL, 'Иное право пользования', NULL, NULL, NULL, NULL, NULL, NULL, 'InoePravoPolzovaniya');
    END IF;
END $$;

--<DO>--
-- 10000314
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '46',
        VALUE = 'Временное пользование',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'VremennoePolzovanie'
    WHERE ITEMID = 10000314;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000314, 12029, '46', 'Временное пользование', NULL, 0, NULL, NULL, NULL, NULL, 'VremennoePolzovanie');
    END IF;
END $$;

--<DO>--
-- 10000316
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '48',
        VALUE = 'Краткосрочная аренда',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KratkosrochnayaArenda'
    WHERE ITEMID = 10000316;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000316, 12029, '48', 'Краткосрочная аренда', NULL, 0, NULL, NULL, NULL, NULL, 'KratkosrochnayaArenda');
    END IF;
END $$;

--<DO>--
-- 10000317
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '49',
        VALUE = 'Краткосрочная аренда на период строительства',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KratkosrochnayaArendaNaPeriodStroitelstva'
    WHERE ITEMID = 10000317;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000317, 12029, '49', 'Краткосрочная аренда на период строительства', NULL, 0, NULL, NULL, NULL, NULL, 'KratkosrochnayaArendaNaPeriodStroitelstva');
    END IF;
END $$;

--<DO>--
-- 10000318
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '50',
        VALUE = 'Безвозмездное временное пользование',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BezvozmezdnoeVremennoePolzovanie'
    WHERE ITEMID = 10000318;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000318, 12029, '50', 'Безвозмездное временное пользование', NULL, 0, NULL, NULL, NULL, NULL, 'BezvozmezdnoeVremennoePolzovanie');
    END IF;
END $$;

--<DO>--
-- 10000319
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '51',
        VALUE = 'Аренда с множественностью лиц',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ArendaSMnozhestvennostyuLits'
    WHERE ITEMID = 10000319;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000319, 12029, '51', 'Аренда с множественностью лиц', NULL, 0, NULL, NULL, NULL, NULL, 'ArendaSMnozhestvennostyuLits');
    END IF;
END $$;

--<DO>--
-- 10000320
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '52',
        VALUE = 'Право ограниченного пользования ЗУ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PravoOgranichennogoPolzovaniyaZu'
    WHERE ITEMID = 10000320;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000320, 12029, '52', 'Право ограниченного пользования ЗУ', NULL, 0, NULL, NULL, NULL, NULL, 'PravoOgranichennogoPolzovaniyaZu');
    END IF;
END $$;

--<DO>--
-- 10000321
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '53',
        VALUE = 'Декларирование (отсутствует)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DeklarirovanieOtsutstvuet'
    WHERE ITEMID = 10000321;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000321, 12029, '53', 'Декларирование (отсутствует)', NULL, 0, NULL, NULL, NULL, NULL, 'DeklarirovanieOtsutstvuet');
    END IF;
END $$;

--<DO>--
-- 10000322
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '54',
        VALUE = 'Резервирование',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Rezervirovanie'
    WHERE ITEMID = 10000322;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000322, 12029, '54', 'Резервирование', NULL, 0, NULL, NULL, NULL, NULL, 'Rezervirovanie');
    END IF;
END $$;

--<DO>--
-- 10000323
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '55',
        VALUE = 'Не определен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NeOpredelen'
    WHERE ITEMID = 10000323;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000323, 12029, '55', 'Не определен', NULL, 0, NULL, NULL, NULL, NULL, 'NeOpredelen');
    END IF;
END $$;

--<DO>--
-- 10000073
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '24',
        VALUE = 'Коммерческий найм',
        SHORT_TITLE = 'КН',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KommercheskijNajm'
    WHERE ITEMID = 10000073;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000073, 12029, '24', 'Коммерческий найм', 'КН', 0, NULL, NULL, NULL, NULL, 'KommercheskijNajm');
    END IF;
END $$;

--<DO>--
-- 10000074
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '25',
        VALUE = 'Ограничение распоряжения жилым помещением',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OgranichenieRasporyazheniyaZhilymPomesheniem'
    WHERE ITEMID = 10000074;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000074, 12029, '25', 'Ограничение распоряжения жилым помещением', NULL, 0, NULL, NULL, NULL, NULL, 'OgranichenieRasporyazheniyaZhilymPomesheniem');
    END IF;
END $$;

--<DO>--
-- 10000075
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '26',
        VALUE = 'Запрещение сделок с имуществом',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ZapreshenieSdelokSImushestvom'
    WHERE ITEMID = 10000075;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000075, 12029, '26', 'Запрещение сделок с имуществом', NULL, 0, NULL, NULL, NULL, NULL, 'ZapreshenieSdelokSImushestvom');
    END IF;
END $$;

--<DO>--
-- 10000076
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '27',
        VALUE = 'Ипотека в силу закона',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'IpotekaVSiluZakona'
    WHERE ITEMID = 10000076;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000076, 12029, '27', 'Ипотека в силу закона', NULL, 0, NULL, NULL, NULL, NULL, 'IpotekaVSiluZakona');
    END IF;
END $$;

--<DO>--
-- 10000077
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '28',
        VALUE = 'Купля-продажа с рассрочкой платежа',
        SHORT_TITLE = 'КПР',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KuplyaprodazhaSRassrochkoj'
    WHERE ITEMID = 10000077;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000077, 12029, '28', 'Купля-продажа с рассрочкой платежа', 'КПР', 0, NULL, NULL, NULL, NULL, 'KuplyaprodazhaSRassrochkoj');
    END IF;
END $$;

--<DO>--
-- 10000078
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '29',
        VALUE = 'Найм служебного помещения',
        SHORT_TITLE = 'НСП',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NajmSluzhebnogoPomesheniya'
    WHERE ITEMID = 10000078;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000078, 12029, '29', 'Найм служебного помещения', 'НСП', 0, NULL, NULL, NULL, NULL, 'NajmSluzhebnogoPomesheniya');
    END IF;
END $$;

--<DO>--
-- 10000079
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '30',
        VALUE = 'Купля-продажа ипотека',
        SHORT_TITLE = 'КПИ',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KuplyaprodazhaPoSotsialnojIpoteke'
    WHERE ITEMID = 10000079;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000079, 12029, '30', 'Купля-продажа ипотека', 'КПИ', 0, NULL, NULL, NULL, NULL, 'KuplyaprodazhaPoSotsialnojIpoteke');
    END IF;
END $$;

--<DO>--
-- 10000080
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '31',
        VALUE = 'Срочное возмездное пользование (найм)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SrochnoeVozmezdnoePolzovanieNajm'
    WHERE ITEMID = 10000080;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000080, 12029, '31', 'Срочное возмездное пользование (найм)', NULL, 0, NULL, NULL, NULL, NULL, 'SrochnoeVozmezdnoePolzovanieNajm');
    END IF;
END $$;

--<DO>--
-- 10000081
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '32',
        VALUE = 'Арест',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Arest'
    WHERE ITEMID = 10000081;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000081, 12029, '32', 'Арест', NULL, 0, NULL, NULL, NULL, NULL, 'Arest');
    END IF;
END $$;

--<DO>--
-- 10000082
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '33',
        VALUE = 'Мена передача в порядке компенсации',
        SHORT_TITLE = 'Мп',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MenaPeredachaVPoryadkeKompensatsii'
    WHERE ITEMID = 10000082;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000082, 12029, '33', 'Мена передача в порядке компенсации', 'Мп', 0, NULL, NULL, NULL, NULL, 'MenaPeredachaVPoryadkeKompensatsii');
    END IF;
END $$;

--<DO>--
-- 10000083
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '34',
        VALUE = 'Коммерческий наем',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KommercheskijNaem'
    WHERE ITEMID = 10000083;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000083, 12029, '34', 'Коммерческий наем', NULL, 0, NULL, NULL, NULL, NULL, 'KommercheskijNaem');
    END IF;
END $$;

--<DO>--
-- 10000084
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '35',
        VALUE = 'Рента',
        SHORT_TITLE = 'Р',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Renta'
    WHERE ITEMID = 10000084;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000084, 12029, '35', 'Рента', 'Р', 0, NULL, NULL, NULL, NULL, 'Renta');
    END IF;
END $$;

--<DO>--
-- 10000085
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '36',
        VALUE = 'Запрещение заключения сделок с имуществом',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ZapreshenieZaklyucheniyaSdelokSImushestvom'
    WHERE ITEMID = 10000085;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000085, 12029, '36', 'Запрещение заключения сделок с имуществом', NULL, 0, NULL, NULL, NULL, NULL, 'ZapreshenieZaklyucheniyaSdelokSImushestvom');
    END IF;
END $$;

--<DO>--
-- 10000086
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '37',
        VALUE = 'Найм в общежитии',
        SHORT_TITLE = 'Нобщ',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NajmPomesheniyaVObshezhitii'
    WHERE ITEMID = 10000086;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000086, 12029, '37', 'Найм в общежитии', 'Нобщ', 0, NULL, NULL, NULL, NULL, 'NajmPomesheniyaVObshezhitii');
    END IF;
END $$;

--<DO>--
-- 10000087
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '38',
        VALUE = 'Мена с оплатой разницы по ипотеке',
        SHORT_TITLE = 'МРи',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MenaPoSotsialnojIpoteke'
    WHERE ITEMID = 10000087;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000087, 12029, '38', 'Мена с оплатой разницы по ипотеке', 'МРи', 0, NULL, NULL, NULL, NULL, 'MenaPoSotsialnojIpoteke');
    END IF;
END $$;

--<DO>--
-- 10000088
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '39',
        VALUE = 'Право требования по договору',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PravoTrebovaniyaPoDogovoru'
    WHERE ITEMID = 10000088;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000088, 12029, '39', 'Право требования по договору', NULL, 0, NULL, NULL, NULL, NULL, 'PravoTrebovaniyaPoDogovoru');
    END IF;
END $$;

--<DO>--
-- 10000089
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '40',
        VALUE = 'Залог в силу закона',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ZalogVSiluZakona'
    WHERE ITEMID = 10000089;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000089, 12029, '40', 'Залог в силу закона', NULL, 0, NULL, NULL, NULL, NULL, 'ZalogVSiluZakona');
    END IF;
END $$;

--<DO>--
-- 10000090
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '41',
        VALUE = 'Найм маневренный фонд',
        SHORT_TITLE = 'Нмф',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NajmManevrennogoFonda'
    WHERE ITEMID = 10000090;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000090, 12029, '41', 'Найм маневренный фонд', 'Нмф', 0, NULL, NULL, NULL, NULL, 'NajmManevrennogoFonda');
    END IF;
END $$;

--<DO>--
-- 10000091
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '42',
        VALUE = 'Купля-продажа',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Kuplyaprodazha'
    WHERE ITEMID = 10000091;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000091, 12029, '42', 'Купля-продажа', NULL, 0, NULL, NULL, NULL, NULL, 'Kuplyaprodazha');
    END IF;
END $$;

--<DO>--
-- 10000092
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '43',
        VALUE = 'Мена',
        SHORT_TITLE = 'М',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Mena'
    WHERE ITEMID = 10000092;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000092, 12029, '43', 'Мена', 'М', 0, NULL, NULL, NULL, NULL, 'Mena');
    END IF;
END $$;

--<DO>--
-- 10000093
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '44',
        VALUE = 'Арест',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Arest1'
    WHERE ITEMID = 10000093;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000093, 12029, '44', 'Арест', NULL, 0, NULL, NULL, NULL, NULL, 'Arest1');
    END IF;
END $$;

--<DO>--
-- 10000094
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '45',
        VALUE = 'Ограничение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Ogranichenie'
    WHERE ITEMID = 10000094;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000094, 12029, '45', 'Ограничение', NULL, 0, NULL, NULL, NULL, NULL, 'Ogranichenie');
    END IF;
END $$;

--<DO>--
-- 1299708
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '16',
        VALUE = 'Собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Sobstvennost'
    WHERE ITEMID = 1299708;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1299708, 12029, '16', 'Собственность', NULL, 0, NULL, NULL, NULL, NULL, 'Sobstvennost');
    END IF;
END $$;

--<DO>--
-- 1299709
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '17',
        VALUE = 'Незарегистрированная собственность г.Москвы',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NezaregistrirovannayaSobstvennostGmoskvy'
    WHERE ITEMID = 1299709;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1299709, 12029, '17', 'Незарегистрированная собственность г.Москвы', NULL, 0, NULL, NULL, NULL, NULL, 'NezaregistrirovannayaSobstvennostGmoskvy');
    END IF;
END $$;

--<DO>--
-- 1299710
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '18',
        VALUE = 'Возможная незарегистрированная собственность третьих лиц',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'VozmozhnayaNezaregistrirovannayaSobstvennostTretikhLits'
    WHERE ITEMID = 1299710;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1299710, 12029, '18', 'Возможная незарегистрированная собственность третьих лиц', NULL, 0, NULL, NULL, NULL, NULL, 'VozmozhnayaNezaregistrirovannayaSobstvennostTretikhLits');
    END IF;
END $$;

--<DO>--
-- 1290856
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '01',
        VALUE = 'Безвозмездное срочное пользование',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BezvozmezdnoeSrochnoePolzovanie'
    WHERE ITEMID = 1290856;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290856, 12029, '01', 'Безвозмездное срочное пользование', NULL, 0, NULL, NULL, NULL, NULL, 'BezvozmezdnoeSrochnoePolzovanie');
    END IF;
END $$;

--<DO>--
-- 1290857
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '02',
        VALUE = 'Постоянное (бессрочное) пользование',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PostoyannoeBessrochnoePolzovanie'
    WHERE ITEMID = 1290857;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290857, 12029, '02', 'Постоянное (бессрочное) пользование', NULL, 0, NULL, NULL, NULL, NULL, 'PostoyannoeBessrochnoePolzovanie');
    END IF;
END $$;

--<DO>--
-- 1290858
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '03',
        VALUE = 'Социальный наем',
        SHORT_TITLE = 'СН',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SotsialnyjNajm'
    WHERE ITEMID = 1290858;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290858, 12029, '03', 'Социальный наем', 'СН', 0, NULL, NULL, NULL, NULL, 'SotsialnyjNajm');
    END IF;
END $$;

--<DO>--
-- 1290859
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '04',
        VALUE = 'Региональная собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RegionalnayaSobstvennost'
    WHERE ITEMID = 1290859;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290859, 12029, '04', 'Региональная собственность', NULL, 0, NULL, NULL, NULL, NULL, 'RegionalnayaSobstvennost');
    END IF;
END $$;

--<DO>--
-- 1290860
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '05',
        VALUE = 'Безвозмездное пользование',
        SHORT_TITLE = 'БП',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BezvozmezdnoePolzovanie'
    WHERE ITEMID = 1290860;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290860, 12029, '05', 'Безвозмездное пользование', 'БП', 0, NULL, NULL, NULL, NULL, 'BezvozmezdnoePolzovanie');
    END IF;
END $$;

--<DO>--
-- 1290861
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '06',
        VALUE = 'Доверительное управление',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DoveritelnoeUpravlenie'
    WHERE ITEMID = 1290861;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290861, 12029, '06', 'Доверительное управление', NULL, 0, NULL, NULL, NULL, NULL, 'DoveritelnoeUpravlenie');
    END IF;
END $$;

--<DO>--
-- 1290862
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '07',
        VALUE = 'Оперативное управление',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OperativnoeUpravlenie'
    WHERE ITEMID = 1290862;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290862, 12029, '07', 'Оперативное управление', NULL, 0, NULL, NULL, NULL, NULL, 'OperativnoeUpravlenie');
    END IF;
END $$;

--<DO>--
-- 1290863
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '08',
        VALUE = 'Иностранная собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InostrannayaSobstvennost'
    WHERE ITEMID = 1290863;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290863, 12029, '08', 'Иностранная собственность', NULL, 0, NULL, NULL, NULL, NULL, 'InostrannayaSobstvennost');
    END IF;
END $$;

--<DO>--
-- 1290864
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '09',
        VALUE = 'Хозяйственное ведение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KhozyajstvennoeVedenie'
    WHERE ITEMID = 1290864;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290864, 12029, '09', 'Хозяйственное ведение', NULL, 0, NULL, NULL, NULL, NULL, 'KhozyajstvennoeVedenie');
    END IF;
END $$;

--<DO>--
-- 1290865
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '10',
        VALUE = 'Аренда',
        SHORT_TITLE = 'А',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Arenda'
    WHERE ITEMID = 1290865;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290865, 12029, '10', 'Аренда', 'А', 0, NULL, NULL, NULL, NULL, 'Arenda');
    END IF;
END $$;

--<DO>--
-- 1290866
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '11',
        VALUE = 'Частная собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ChastnayaSobstvennost'
    WHERE ITEMID = 1290866;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290866, 12029, '11', 'Частная собственность', NULL, 0, NULL, NULL, NULL, NULL, 'ChastnayaSobstvennost');
    END IF;
END $$;

--<DO>--
-- 1290867
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '12',
        VALUE = 'Федеральная собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FederalnayaSobstvennost'
    WHERE ITEMID = 1290867;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290867, 12029, '12', 'Федеральная собственность', NULL, 0, NULL, NULL, NULL, NULL, 'FederalnayaSobstvennost');
    END IF;
END $$;

--<DO>--
-- 1290868
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '13',
        VALUE = 'Общедолевая собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ObshedolevayaSobstvennost'
    WHERE ITEMID = 1290868;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290868, 12029, '13', 'Общедолевая собственность', NULL, 0, NULL, NULL, NULL, NULL, 'ObshedolevayaSobstvennost');
    END IF;
END $$;

--<DO>--
-- 1290869
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '14',
        VALUE = 'Субаренда',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Subarenda'
    WHERE ITEMID = 1290869;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290869, 12029, '14', 'Субаренда', NULL, 0, NULL, NULL, NULL, NULL, 'Subarenda');
    END IF;
END $$;

--<DO>--
-- 1290870
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '15',
        VALUE = 'Муниципальная собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MunitsipalnayaSobstvennost'
    WHERE ITEMID = 1290870;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1290870, 12029, '15', 'Муниципальная собственность', NULL, 0, NULL, NULL, NULL, NULL, 'MunitsipalnayaSobstvennost');
    END IF;
END $$;

--<DO>--
-- 1301084
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '19',
        VALUE = 'Долевая собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DolevayaSobstvennost'
    WHERE ITEMID = 1301084;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301084, 12029, '19', 'Долевая собственность', NULL, 0, NULL, NULL, NULL, NULL, 'DolevayaSobstvennost');
    END IF;
END $$;

--<DO>--
-- 1301085
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '20',
        VALUE = 'Совместная собственность',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SovmestnayaSobstvennost'
    WHERE ITEMID = 1301085;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301085, 12029, '20', 'Совместная собственность', NULL, 0, NULL, NULL, NULL, NULL, 'SovmestnayaSobstvennost');
    END IF;
END $$;

--<DO>--
-- 1301086
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '21',
        VALUE = 'Пожизненное-наследуемое владение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PozhiznennoenasleduemoeVladenie'
    WHERE ITEMID = 1301086;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301086, 12029, '21', 'Пожизненное-наследуемое владение', NULL, 0, NULL, NULL, NULL, NULL, 'PozhiznennoenasleduemoeVladenie');
    END IF;
END $$;

--<DO>--
-- 1301087
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '22',
        VALUE = 'Сервитут (право)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ServitutPravo'
    WHERE ITEMID = 1301087;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301087, 12029, '22', 'Сервитут (право)', NULL, 0, NULL, NULL, NULL, NULL, 'ServitutPravo');
    END IF;
END $$;

--<DO>--
-- 1301088
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = '23',
        VALUE = 'Иные права',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InyePrava'
    WHERE ITEMID = 1301088;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (1301088, 12029, '23', 'Иные права', NULL, 0, NULL, NULL, NULL, NULL, 'InyePrava');
    END IF;
END $$;

--<DO>--
-- 12029001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Передача в собственность равнозначное',
        SHORT_TITLE = 'ПСрз',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PeredachaVSobstvennostRavnoznachnoe'
    WHERE ITEMID = 12029001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029001, 12029, NULL, 'Передача в собственность равнозначное', 'ПСрз', 0, NULL, NULL, NULL, NULL, 'PeredachaVSobstvennostRavnoznachnoe');
    END IF;
END $$;

--<DO>--
-- 10000324
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Найм',
        SHORT_TITLE = 'Н',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Najm'
    WHERE ITEMID = 10000324;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (10000324, 12029, NULL, 'Найм', 'Н', 0, NULL, NULL, NULL, NULL, 'Najm');
    END IF;
END $$;

--<DO>--
-- 12029002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Найм дети-сироты',
        SHORT_TITLE = 'Ндс',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NajmDetySiroty'
    WHERE ITEMID = 12029002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029002, 12029, NULL, 'Найм дети-сироты', 'Ндс', 0, NULL, NULL, NULL, NULL, 'NajmDetySiroty');
    END IF;
END $$;

--<DO>--
-- 12029003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Найм краткосрочный',
        SHORT_TITLE = 'Нкр',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NajmKratkosrochniy'
    WHERE ITEMID = 12029003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029003, 12029, NULL, 'Найм краткосрочный', 'Нкр', 0, NULL, NULL, NULL, NULL, 'NajmKratkosrochniy');
    END IF;
END $$;

--<DO>--
-- 12029004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'обманутые вкладчики',
        SHORT_TITLE = 'ОВ',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ObmanutieVkladchiki'
    WHERE ITEMID = 12029004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029004, 12029, NULL, 'обманутые вкладчики', 'ОВ', 0, NULL, NULL, NULL, NULL, 'ObmanutieVkladchiki');
    END IF;
END $$;

--<DO>--
-- 12029005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'обманутые вкладчики с доплатой',
        SHORT_TITLE = 'ОВд',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ObmanutieVkladchikiSDoplatoi'
    WHERE ITEMID = 12029005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029005, 12029, NULL, 'обманутые вкладчики с доплатой', 'ОВд', 0, NULL, NULL, NULL, NULL, 'ObmanutieVkladchikiSDoplatoi');
    END IF;
END $$;

--<DO>--
-- 12029006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Пользование в общежитии',
        SHORT_TITLE = 'Побщ',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PolzovanieVObshejitii'
    WHERE ITEMID = 12029006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029006, 12029, NULL, 'Пользование в общежитии', 'Побщ', 0, NULL, NULL, NULL, NULL, 'PolzovanieVObshejitii');
    END IF;
END $$;

--<DO>--
-- 12029007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Переход права на равнозначное',
        SHORT_TITLE = 'ППрз',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PerehodPravaNaRavnoznachnoe'
    WHERE ITEMID = 12029007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029007, 12029, NULL, 'Переход права на равнозначное', 'ППрз', 0, NULL, NULL, NULL, NULL, 'PerehodPravaNaRavnoznachnoe');
    END IF;
END $$;

--<DO>--
-- 12029008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Переход права на равноценное',
        SHORT_TITLE = 'ППрц',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PerehodPravaNaRavnocennoe'
    WHERE ITEMID = 12029008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029008, 12029, NULL, 'Переход права на равноценное', 'ППрц', 0, NULL, NULL, NULL, NULL, 'PerehodPravaNaRavnocennoe');
    END IF;
END $$;

--<DO>--
-- 12029009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Передача в собственность бесплатно',
        SHORT_TITLE = 'ПСБ',
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PeredachaVSobstvennostBesplatno'
    WHERE ITEMID = 12029009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029009, 12029, NULL, 'Передача в собственность бесплатно', 'ПСБ', 0, NULL, NULL, NULL, NULL, 'PeredachaVSobstvennostBesplatno');
    END IF;
END $$;

--<DO>--
-- 12029010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Безвозмездное пользование в специализированном фонде',
        SHORT_TITLE = 'БПс',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BezvozmezdnoePolzovanieVSpecializirovannomFonde'
    WHERE ITEMID = 12029010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029010, 12029, NULL, 'Безвозмездное пользование в специализированном фонде', 'БПс', NULL, NULL, NULL, NULL, NULL, 'BezvozmezdnoePolzovanieVSpecializirovannomFonde');
    END IF;
END $$;

--<DO>--
-- 12029011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Безвозмездное пользование в специализированном фонде МК',
        SHORT_TITLE = 'БПс(мк)',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BezvozmezdnoePolzovanieVSpecializirovannomFondeMk'
    WHERE ITEMID = 12029011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029011, 12029, NULL, 'Безвозмездное пользование в специализированном фонде МК', 'БПс(мк)', NULL, NULL, NULL, NULL, NULL, 'BezvozmezdnoePolzovanieVSpecializirovannomFondeMk');
    END IF;
END $$;

--<DO>--
-- 12029012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Гос жилищный сертификат',
        SHORT_TITLE = 'ГЖС',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'GosGilishniyCertificat'
    WHERE ITEMID = 12029012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029012, 12029, NULL, 'Гос жилищный сертификат', 'ГЖС', NULL, NULL, NULL, NULL, NULL, 'GosGilishniyCertificat');
    END IF;
END $$;

--<DO>--
-- 12029013
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Коммерческий найм в бездотационном доме',
        SHORT_TITLE = 'КНб',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KommercheskiyNajmVBezdotacionnomDome'
    WHERE ITEMID = 12029013;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029013, 12029, NULL, 'Коммерческий найм в бездотационном доме', 'КНб', NULL, NULL, NULL, NULL, NULL, 'KommercheskiyNajmVBezdotacionnomDome');
    END IF;
END $$;

--<DO>--
-- 12029014
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Коммерческий найм молодая семья',
        SHORT_TITLE = 'КНм',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KommercheskiyNajmMolodajaSemja'
    WHERE ITEMID = 12029014;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029014, 12029, NULL, 'Коммерческий найм молодая семья', 'КНм', NULL, NULL, NULL, NULL, NULL, 'KommercheskiyNajmMolodajaSemja');
    END IF;
END $$;

--<DO>--
-- 12029015
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Коммерческий найм молодая семья в бездотационном доме',
        SHORT_TITLE = 'КНмб',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KommercheskiyNajmMolodajaSemjaVBezdotacionnomDome'
    WHERE ITEMID = 12029015;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029015, 12029, NULL, 'Коммерческий найм молодая семья в бездотационном доме', 'КНмб', NULL, NULL, NULL, NULL, NULL, 'KommercheskiyNajmMolodajaSemjaVBezdotacionnomDome');
    END IF;
END $$;

--<DO>--
-- 12029016
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Купля-продажа выкуп',
        SHORT_TITLE = 'КПв',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KupljaProdajaVikup'
    WHERE ITEMID = 12029016;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029016, 12029, NULL, 'Купля-продажа выкуп', 'КПв', NULL, NULL, NULL, NULL, NULL, 'KupljaProdajaVikup');
    END IF;
END $$;

--<DO>--
-- 12029017
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Купля-продажа с доплатой',
        SHORT_TITLE = 'КПд',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KupljaProdajaSDoplatoi'
    WHERE ITEMID = 12029017;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029017, 12029, NULL, 'Купля-продажа с доплатой', 'КПд', NULL, NULL, NULL, NULL, NULL, 'KupljaProdajaSDoplatoi');
    END IF;
END $$;

--<DO>--
-- 12029018
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Купля-продажа ипотека молодая семья',
        SHORT_TITLE = 'КПИм',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KupljaProdajaIpotekaMolodajaSemja'
    WHERE ITEMID = 12029018;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029018, 12029, NULL, 'Купля-продажа ипотека молодая семья', 'КПИм', NULL, NULL, NULL, NULL, NULL, 'KupljaProdajaIpotekaMolodajaSemja');
    END IF;
END $$;

--<DO>--
-- 12029019
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Купля-продажа льготная',
        SHORT_TITLE = 'КПл',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KupljaProdajaLgotnaja'
    WHERE ITEMID = 12029019;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029019, 12029, NULL, 'Купля-продажа льготная', 'КПл', NULL, NULL, NULL, NULL, NULL, 'KupljaProdajaLgotnaja');
    END IF;
END $$;

--<DO>--
-- 12029020
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Купля-продажа с рассрочкой платежа молодая семья',
        SHORT_TITLE = 'КПРм',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KupljaProdajaSRassrochkoiPlatejaMolodajaSemja'
    WHERE ITEMID = 12029020;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029020, 12029, NULL, 'Купля-продажа с рассрочкой платежа молодая семья', 'КПРм', NULL, NULL, NULL, NULL, NULL, 'KupljaProdajaSRassrochkoiPlatejaMolodajaSemja');
    END IF;
END $$;

--<DO>--
-- 12029021
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Купля-продажа рынок',
        SHORT_TITLE = 'КПрн',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'KupljaProdajaRynok'
    WHERE ITEMID = 12029021;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029021, 12029, NULL, 'Купля-продажа рынок', 'КПрн', NULL, NULL, NULL, NULL, NULL, 'KupljaProdajaRynok');
    END IF;
END $$;

--<DO>--
-- 12029022
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Мена с доплатой',
        SHORT_TITLE = 'Мд',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MenaSDoplatoi'
    WHERE ITEMID = 12029022;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029022, 12029, NULL, 'Мена с доплатой', 'Мд', NULL, NULL, NULL, NULL, NULL, 'MenaSDoplatoi');
    END IF;
END $$;

--<DO>--
-- 12029023
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Мена с оплатой разницы в счет в комнаты',
        SHORT_TITLE = 'МРк',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MenaSOplatoiVSchetKomnaty'
    WHERE ITEMID = 12029023;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029023, 12029, NULL, 'Мена с оплатой разницы в счет в комнаты', 'МРк', NULL, NULL, NULL, NULL, NULL, 'MenaSOplatoiVSchetKomnaty');
    END IF;
END $$;

--<DO>--
-- 12029024
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12029,
        CODE = NULL,
        VALUE = 'Мена с оплатой разницы в счет в комнаты молодая семья',
        SHORT_TITLE = 'МРкм',
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MenaSOplatoiVSchetKomnatyMolodajaSemja'
    WHERE ITEMID = 12029024;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12029024, 12029, NULL, 'Мена с оплатой разницы в счет в комнаты молодая семья', 'МРкм', NULL, NULL, NULL, NULL, NULL, 'MenaSOplatoiVSchetKomnatyMolodajaSemja');
    END IF;
END $$;

--<DO>--
-- 12165002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Расчет ущерба совпадает с данными СК',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DamageAmountCoincides'
    WHERE ITEMID = 12165002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165002, 12165, NULL, 'Расчет ущерба совпадает с данными СК', NULL, NULL, NULL, NULL, NULL, NULL, 'DamageAmountCoincides');
    END IF;
END $$;

--<DO>--
-- 12165003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Расхождения со СК в расчете ущерба',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DamageAmountDiscrepancies'
    WHERE ITEMID = 12165003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165003, 12165, NULL, 'Расхождения со СК в расчете ущерба', NULL, NULL, NULL, NULL, NULL, NULL, 'DamageAmountDiscrepancies');
    END IF;
END $$;

--<DO>--
-- 12165004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Проверено',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Checked'
    WHERE ITEMID = 12165004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165004, 12165, NULL, 'Проверено', NULL, NULL, NULL, NULL, NULL, NULL, 'Checked');
    END IF;
END $$;

--<DO>--
-- 12165005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Согласовано',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Agreed'
    WHERE ITEMID = 12165005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165005, 12165, NULL, 'Согласовано', NULL, NULL, NULL, NULL, NULL, NULL, 'Agreed');
    END IF;
END $$;

--<DO>--
-- 12165006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Сформирован реестр выплат',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PaymentRegisterFormed'
    WHERE ITEMID = 12165006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165006, 12165, NULL, 'Сформирован реестр выплат', NULL, NULL, NULL, NULL, NULL, NULL, 'PaymentRegisterFormed');
    END IF;
END $$;

--<DO>--
-- 12168001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12168,
        CODE = NULL,
        VALUE = 'Реестр выплат доли города по ущербу в ЖП',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DamageGP'
    WHERE ITEMID = 12168001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12168001, 12168, NULL, 'Реестр выплат доли города по ущербу в ЖП', NULL, NULL, NULL, NULL, NULL, NULL, 'DamageGP');
    END IF;
END $$;

--<DO>--
-- 12168002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12168,
        CODE = NULL,
        VALUE = 'Реестр выплат доли города по ущербу в ОИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DamageOI'
    WHERE ITEMID = 12168002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12168002, 12168, NULL, 'Реестр выплат доли города по ущербу в ОИ', NULL, NULL, NULL, NULL, NULL, NULL, 'DamageOI');
    END IF;
END $$;

--<DO>--
-- 12168003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12168,
        CODE = NULL,
        VALUE = 'Реестр возвратов части премии по ОИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ReturnBonusOI'
    WHERE ITEMID = 12168003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12168003, 12168, NULL, 'Реестр возвратов части премии по ОИ', NULL, NULL, NULL, NULL, NULL, NULL, 'ReturnBonusOI');
    END IF;
END $$;

--<DO>--
-- 12169001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12169,
        CODE = NULL,
        VALUE = 'Сформирован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Formed'
    WHERE ITEMID = 12169001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12169001, 12169, NULL, 'Сформирован', NULL, NULL, NULL, NULL, NULL, NULL, 'Formed');
    END IF;
END $$;

--<DO>--
-- 12170003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12170,
        CODE = NULL,
        VALUE = 'Счет оплачен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Paid'
    WHERE ITEMID = 12170003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12170003, 12170, NULL, 'Счет оплачен', NULL, NULL, NULL, NULL, NULL, NULL, 'Paid');
    END IF;
END $$;

--<DO>--
-- 12170004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12170,
        CODE = NULL,
        VALUE = 'Ошибка в реквизитах',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ErrorInDetails'
    WHERE ITEMID = 12170004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12170004, 12170, NULL, 'Ошибка в реквизитах', NULL, NULL, NULL, NULL, NULL, NULL, 'ErrorInDetails');
    END IF;
END $$;

--<DO>--
-- 12169002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12169,
        CODE = NULL,
        VALUE = 'Передан в ДГИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TransferredDGI'
    WHERE ITEMID = 12169002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12169002, 12169, NULL, 'Передан в ДГИ', NULL, NULL, NULL, NULL, NULL, NULL, 'TransferredDGI');
    END IF;
END $$;

--<DO>--
-- 12169003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12169,
        CODE = NULL,
        VALUE = 'Утвержден в ДГИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ApprovedDGI'
    WHERE ITEMID = 12169003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12169003, 12169, NULL, 'Утвержден в ДГИ', NULL, NULL, NULL, NULL, NULL, NULL, 'ApprovedDGI');
    END IF;
END $$;

--<DO>--
-- 12169004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12169,
        CODE = NULL,
        VALUE = 'Оплачен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Paid'
    WHERE ITEMID = 12169004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12169004, 12169, NULL, 'Оплачен', NULL, NULL, NULL, NULL, NULL, NULL, 'Paid');
    END IF;
END $$;

--<DO>--
-- 12169005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12169,
        CODE = NULL,
        VALUE = 'Требует корректировки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RequiresCorrects'
    WHERE ITEMID = 12169005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12169005, 12169, NULL, 'Требует корректировки', NULL, NULL, NULL, NULL, NULL, NULL, 'RequiresCorrects');
    END IF;
END $$;

--<DO>--
-- 12165008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Подготовлено Заключение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ConclusionPrepare'
    WHERE ITEMID = 12165008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165008, 12165, NULL, 'Подготовлено Заключение', NULL, NULL, NULL, NULL, NULL, NULL, 'ConclusionPrepare');
    END IF;
END $$;

--<DO>--
-- 12170005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12170,
        CODE = NULL,
        VALUE = 'Отказано в выплате',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Denied'
    WHERE ITEMID = 12170005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12170005, 12170, NULL, 'Отказано в выплате', NULL, NULL, NULL, NULL, NULL, NULL, 'Denied');
    END IF;
END $$;

--<DO>--
-- 12165007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Произведена выплата в полном объеме',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FullPaid'
    WHERE ITEMID = 12165007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165007, 12165, NULL, 'Произведена выплата в полном объеме', NULL, NULL, NULL, NULL, NULL, NULL, 'FullPaid');
    END IF;
END $$;

--<DO>--
-- 12165009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Произведена выплата частично',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PartPaid'
    WHERE ITEMID = 12165009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165009, 12165, NULL, 'Произведена выплата частично', NULL, NULL, NULL, NULL, NULL, NULL, 'PartPaid');
    END IF;
END $$;

--<DO>--
-- 12165010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12165,
        CODE = NULL,
        VALUE = 'Отказано в выплате',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Denied'
    WHERE ITEMID = 12165010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12165010, 12165, NULL, 'Отказано в выплате', NULL, NULL, NULL, NULL, NULL, NULL, 'Denied');
    END IF;
END $$;

--<DO>--
-- 12126007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '7',
        VALUE = 'Окраска',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FinishingWorkPainting'
    WHERE ITEMID = 12126007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126007, 12126, '7', 'Окраска', NULL, 0, '1', NULL, NULL, NULL, 'FinishingWorkPainting');
    END IF;
END $$;

--<DO>--
-- 12126008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '8',
        VALUE = 'Обои',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FinishingWorkWallpaper'
    WHERE ITEMID = 12126008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126008, 12126, '8', 'Обои', NULL, 0, '1', NULL, NULL, NULL, 'FinishingWorkWallpaper');
    END IF;
END $$;

--<DO>--
-- 12126009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '9',
        VALUE = 'Облицовка керамической плиткой',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FinishingWorkCeramic'
    WHERE ITEMID = 12126009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126009, 12126, '9', 'Облицовка керамической плиткой', NULL, 0, '1', NULL, NULL, NULL, 'FinishingWorkCeramic');
    END IF;
END $$;

--<DO>--
-- 12126016
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '16',
        VALUE = 'Провода',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RadioWires'
    WHERE ITEMID = 12126016;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126016, 12126, '16', 'Провода', NULL, 0, '1', NULL, NULL, NULL, 'RadioWires');
    END IF;
END $$;

--<DO>--
-- 12126017
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '17',
        VALUE = 'Вводное устройство',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RadioInputDevice'
    WHERE ITEMID = 12126017;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126017, 12126, '17', 'Вводное устройство', NULL, 0, '1', NULL, NULL, NULL, 'RadioInputDevice');
    END IF;
END $$;

--<DO>--
-- 12126018
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '18',
        VALUE = 'Аппаратура',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RadioEquipment'
    WHERE ITEMID = 12126018;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126018, 12126, '18', 'Аппаратура', NULL, 0, '1', NULL, NULL, NULL, 'RadioEquipment');
    END IF;
END $$;

--<DO>--
-- 12126020
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '20',
        VALUE = 'Провода',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TelevisionWires'
    WHERE ITEMID = 12126020;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126020, 12126, '20', 'Провода', NULL, 0, '1', NULL, NULL, NULL, 'TelevisionWires');
    END IF;
END $$;

--<DO>--
-- 12126021
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '21',
        VALUE = 'Вводное устройство',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TelevisionInputDevice'
    WHERE ITEMID = 12126021;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126021, 12126, '21', 'Вводное устройство', NULL, 0, '1', NULL, NULL, NULL, 'TelevisionInputDevice');
    END IF;
END $$;

--<DO>--
-- 12126023
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '23',
        VALUE = 'Провода',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TelephoneWires'
    WHERE ITEMID = 12126023;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126023, 12126, '23', 'Провода', NULL, 0, '1', NULL, NULL, NULL, 'TelephoneWires');
    END IF;
END $$;

--<DO>--
-- 12126024
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '24',
        VALUE = 'Вводное устройство',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TelephoneInputDevice'
    WHERE ITEMID = 12126024;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126024, 12126, '24', 'Вводное устройство', NULL, 0, '1', NULL, NULL, NULL, 'TelephoneInputDevice');
    END IF;
END $$;

--<DO>--
-- 12126025
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12126,
        CODE = '25',
        VALUE = 'Аппаратура',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TelephoneEquipment'
    WHERE ITEMID = 12126025;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12126025, 12126, '25', 'Аппаратура', NULL, 0, '1', NULL, NULL, NULL, 'TelephoneEquipment');
    END IF;
END $$;

--<DO>--
-- 12169006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12169,
        CODE = NULL,
        VALUE = 'Передано в оплату',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TransferredPayment'
    WHERE ITEMID = 12169006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12169006, 12169, NULL, 'Передано в оплату', NULL, NULL, NULL, NULL, NULL, NULL, 'TransferredPayment');
    END IF;
END $$;

--<DO>--
-- 12125009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '8',
        VALUE = 'Обрушение конструкций',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'StructuralCollapse'
    WHERE ITEMID = 12125009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125009, 12125, '8', 'Обрушение конструкций', NULL, 0, '1', NULL, NULL, NULL, 'StructuralCollapse');
    END IF;
END $$;

--<DO>--
-- 12125010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12125,
        CODE = '9',
        VALUE = 'Сильный ветер',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'StrongWind'
    WHERE ITEMID = 12125010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12125010, 12125, '9', 'Сильный ветер', NULL, 0, '1', NULL, NULL, NULL, 'StrongWind');
    END IF;
END $$;

--<DO>--
-- 12134001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '1',
        VALUE = 'Прочее',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OtherSubReason'
    WHERE ITEMID = 12134001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134001, 12134, '1', 'Прочее', NULL, 0, '1', NULL, NULL, NULL, 'OtherSubReason');
    END IF;
END $$;

--<DO>--
-- 12134002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '2',
        VALUE = 'Неосторожное обращение с огнем',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CarelessHandlingOfFire'
    WHERE ITEMID = 12134002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134002, 12134, '2', 'Неосторожное обращение с огнем', NULL, 0, '1', NULL, NULL, NULL, 'CarelessHandlingOfFire');
    END IF;
END $$;

--<DO>--
-- 12134003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '3',
        VALUE = 'Неисправность электропроводки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'WiringFault'
    WHERE ITEMID = 12134003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134003, 12134, '3', 'Неисправность электропроводки', NULL, 0, '1', NULL, NULL, NULL, 'WiringFault');
    END IF;
END $$;

--<DO>--
-- 12134004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '4',
        VALUE = 'Пожар вне дома',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FireOutsideHome'
    WHERE ITEMID = 12134004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134004, 12134, '4', 'Пожар вне дома', NULL, 0, '1', NULL, NULL, NULL, 'FireOutsideHome');
    END IF;
END $$;

--<DO>--
-- 12134006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '6',
        VALUE = 'Газовые приборы',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'GasAppliances'
    WHERE ITEMID = 12134006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134006, 12134, '6', 'Газовые приборы', NULL, NULL, NULL, NULL, NULL, NULL, 'GasAppliances');
    END IF;
END $$;

--<DO>--
-- 12134005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '5',
        VALUE = 'Подводка газа',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'GasSupply'
    WHERE ITEMID = 12134005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134005, 12134, '5', 'Подводка газа', NULL, NULL, NULL, NULL, NULL, NULL, 'GasSupply');
    END IF;
END $$;

--<DO>--
-- 12134007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '7',
        VALUE = 'Горячее водоснабжение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'HotWaterSupply'
    WHERE ITEMID = 12134007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134007, 12134, '7', 'Горячее водоснабжение', NULL, NULL, NULL, NULL, NULL, NULL, 'HotWaterSupply');
    END IF;
END $$;

--<DO>--
-- 12134008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '8',
        VALUE = 'Холодное водоснабжение',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ColdWaterSupply'
    WHERE ITEMID = 12134008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134008, 12134, '8', 'Холодное водоснабжение', NULL, NULL, NULL, NULL, NULL, NULL, 'ColdWaterSupply');
    END IF;
END $$;

--<DO>--
-- 12134009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '9',
        VALUE = 'Смесители',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Mixers'
    WHERE ITEMID = 12134009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134009, 12134, '9', 'Смесители', NULL, NULL, NULL, NULL, NULL, NULL, 'Mixers');
    END IF;
END $$;

--<DO>--
-- 12134010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '10',
        VALUE = 'Стояк',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Riser'
    WHERE ITEMID = 12134010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134010, 12134, '10', 'Стояк', NULL, NULL, NULL, NULL, NULL, NULL, 'Riser');
    END IF;
END $$;

--<DO>--
-- 12134011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '11',
        VALUE = 'Запорная арматура',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Valves'
    WHERE ITEMID = 12134011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134011, 12134, '11', 'Запорная арматура', NULL, NULL, NULL, NULL, NULL, NULL, 'Valves');
    END IF;
END $$;

--<DO>--
-- 12134012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '12',
        VALUE = 'Труба',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Pipe'
    WHERE ITEMID = 12134012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134012, 12134, '12', 'Труба', NULL, NULL, NULL, NULL, NULL, NULL, 'Pipe');
    END IF;
END $$;

--<DO>--
-- 12134013
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '13',
        VALUE = 'Радиатор',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Radiator'
    WHERE ITEMID = 12134013;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134013, 12134, '13', 'Радиатор', NULL, NULL, NULL, NULL, NULL, NULL, 'Radiator');
    END IF;
END $$;

--<DO>--
-- 12134014
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '14',
        VALUE = 'Расширительный бак',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ExpansionTank'
    WHERE ITEMID = 12134014;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134014, 12134, '14', 'Расширительный бак', NULL, NULL, NULL, NULL, NULL, NULL, 'ExpansionTank');
    END IF;
END $$;

--<DO>--
-- 12134015
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '15',
        VALUE = 'Отопительные приборы',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'HeatingAppliances'
    WHERE ITEMID = 12134015;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134015, 12134, '15', 'Отопительные приборы', NULL, NULL, NULL, NULL, NULL, NULL, 'HeatingAppliances');
    END IF;
END $$;

--<DO>--
-- 12134016
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '16',
        VALUE = 'Лежак',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Legak'
    WHERE ITEMID = 12134016;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134016, 12134, '16', 'Лежак', NULL, NULL, NULL, NULL, NULL, NULL, 'Legak');
    END IF;
END $$;

--<DO>--
-- 12134017
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '17',
        VALUE = 'Приборы',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Devices'
    WHERE ITEMID = 12134017;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134017, 12134, '17', 'Приборы', NULL, NULL, NULL, NULL, NULL, NULL, 'Devices');
    END IF;
END $$;

--<DO>--
-- 12134018
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '18',
        VALUE = 'Примыкание воронки к кровле',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FunneJunctionRoof'
    WHERE ITEMID = 12134018;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134018, 12134, '18', 'Примыкание воронки к кровле', NULL, NULL, NULL, NULL, NULL, NULL, 'FunneJunctionRoof');
    END IF;
END $$;

--<DO>--
-- 12134019
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '19',
        VALUE = 'Расчеканка раструба',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FlareBell'
    WHERE ITEMID = 12134019;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134019, 12134, '19', 'Расчеканка раструба', NULL, NULL, NULL, NULL, NULL, NULL, 'FlareBell');
    END IF;
END $$;

--<DO>--
-- 12134020
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12134,
        CODE = '20',
        VALUE = 'Засор внутреннего водостока',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'InternalDrainageBlockage'
    WHERE ITEMID = 12134020;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12134020, 12134, '20', 'Засор внутреннего водостока', NULL, NULL, NULL, NULL, NULL, NULL, 'InternalDrainageBlockage');
    END IF;
END $$;

--<DO>--
-- 12135001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12135,
        CODE = '1',
        VALUE = 'Прочее',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OtherRefinement'
    WHERE ITEMID = 12135001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12135001, 12135, '1', 'Прочее', NULL, NULL, NULL, NULL, NULL, NULL, 'OtherRefinement');
    END IF;
END $$;

--<DO>--
-- 12135003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12135,
        CODE = '3',
        VALUE = 'Запорная арматура',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ValvesRefinement'
    WHERE ITEMID = 12135003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12135003, 12135, '3', 'Запорная арматура', NULL, NULL, NULL, NULL, NULL, NULL, 'ValvesRefinement');
    END IF;
END $$;

--<DO>--
-- 12135002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12135,
        CODE = '2',
        VALUE = 'Стояк',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RiserRefinement'
    WHERE ITEMID = 12135002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12135002, 12135, '2', 'Стояк', NULL, NULL, NULL, NULL, NULL, NULL, 'RiserRefinement');
    END IF;
END $$;

--<DO>--
-- 12135004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12135,
        CODE = '4',
        VALUE = 'Труба',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PipeRefinement'
    WHERE ITEMID = 12135004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12135004, 12135, '4', 'Труба', NULL, NULL, NULL, NULL, NULL, NULL, 'PipeRefinement');
    END IF;
END $$;

--<DO>--
-- 12135005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12135,
        CODE = '5',
        VALUE = 'Входной шаровой кран',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BallValveRefinement'
    WHERE ITEMID = 12135005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12135005, 12135, '5', 'Входной шаровой кран', NULL, NULL, NULL, NULL, NULL, NULL, 'BallValveRefinement');
    END IF;
END $$;

--<DO>--
-- 12135006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12135,
        CODE = '6',
        VALUE = 'Гибкая подводка',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FlexibleEyelinerRefinement'
    WHERE ITEMID = 12135006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12135006, 12135, '6', 'Гибкая подводка', NULL, NULL, NULL, NULL, NULL, NULL, 'FlexibleEyelinerRefinement');
    END IF;
END $$;

--<DO>--
-- 12135007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12135,
        CODE = '7',
        VALUE = 'Фильтр',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FilterRefinement'
    WHERE ITEMID = 12135007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12135007, 12135, '7', 'Фильтр', NULL, NULL, NULL, NULL, NULL, NULL, 'FilterRefinement');
    END IF;
END $$;

--<DO>--
-- 12135008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12135,
        CODE = '8',
        VALUE = 'Полотенцесушитель',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'HeatedTowelRailRefinement'
    WHERE ITEMID = 12135008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12135008, 12135, '8', 'Полотенцесушитель', NULL, NULL, NULL, NULL, NULL, NULL, 'HeatedTowelRailRefinement');
    END IF;
END $$;

--<DO>--
-- 303001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 303,
        CODE = '1',
        VALUE = 'Сформирован расчет',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Created'
    WHERE ITEMID = 303001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (303001, 303, '1', 'Сформирован расчет', NULL, 0, '1', NULL, NULL, NULL, 'Created');
    END IF;
END $$;

--<DO>--
-- 303004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 303,
        CODE = '4',
        VALUE = 'Заключен договор',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ContractConcluded'
    WHERE ITEMID = 303004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (303004, 303, '4', 'Заключен договор', NULL, 0, '1', NULL, NULL, NULL, 'ContractConcluded');
    END IF;
END $$;

--<DO>--
-- 12170001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12170,
        CODE = NULL,
        VALUE = 'Счет создан',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Formed'
    WHERE ITEMID = 12170001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12170001, 12170, NULL, 'Счет создан', NULL, NULL, NULL, NULL, NULL, NULL, 'Formed');
    END IF;
END $$;

--<DO>--
-- 12170007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12170,
        CODE = NULL,
        VALUE = 'Счет согласован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Agreed'
    WHERE ITEMID = 12170007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12170007, 12170, NULL, 'Счет согласован', NULL, NULL, NULL, NULL, NULL, NULL, 'Agreed');
    END IF;
END $$;

--<DO>--
-- 12171001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12171,
        CODE = NULL,
        VALUE = 'Создан',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Created'
    WHERE ITEMID = 12171001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12171001, 12171, NULL, 'Создан', NULL, NULL, NULL, NULL, NULL, NULL, 'Created');
    END IF;
END $$;

--<DO>--
-- 12171002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12171,
        CODE = NULL,
        VALUE = 'Подготовлено уведомление',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NotificationPrepared'
    WHERE ITEMID = 12171002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12171002, 12171, NULL, 'Подготовлено уведомление', NULL, NULL, NULL, NULL, NULL, NULL, 'NotificationPrepared');
    END IF;
END $$;

--<DO>--
-- 12171003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12171,
        CODE = NULL,
        VALUE = 'Согласован',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Agreed'
    WHERE ITEMID = 12171003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12171003, 12171, NULL, 'Согласован', NULL, NULL, NULL, NULL, NULL, NULL, 'Agreed');
    END IF;
END $$;

--<DO>--
-- 12171004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12171,
        CODE = NULL,
        VALUE = 'Сформирован реестр выплат',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Formed'
    WHERE ITEMID = 12171004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12171004, 12171, NULL, 'Сформирован реестр выплат', NULL, NULL, NULL, NULL, NULL, NULL, 'Formed');
    END IF;
END $$;

--<DO>--
-- 12171005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12171,
        CODE = NULL,
        VALUE = 'Произведена выплата в полном объеме',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FullPaymentMade'
    WHERE ITEMID = 12171005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12171005, 12171, NULL, 'Произведена выплата в полном объеме', NULL, NULL, NULL, NULL, NULL, NULL, 'FullPaymentMade');
    END IF;
END $$;

--<DO>--
-- 12171006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12171,
        CODE = NULL,
        VALUE = 'Произведена частичная выплата',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PartialPaymentMade'
    WHERE ITEMID = 12171006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12171006, 12171, NULL, 'Произведена частичная выплата', NULL, NULL, NULL, NULL, NULL, NULL, 'PartialPaymentMade');
    END IF;
END $$;

--<DO>--
-- 303005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 303,
        CODE = '5',
        VALUE = 'Согласован расчет',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Agreed'
    WHERE ITEMID = 303005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (303005, 303, '5', 'Согласован расчет', NULL, 0, '1', NULL, NULL, NULL, 'Agreed');
    END IF;
END $$;

--<DO>--
-- 303002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 303,
        CODE = '2',
        VALUE = 'Согласован проект договора',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ProjectAgreed'
    WHERE ITEMID = 303002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (303002, 303, '2', 'Согласован проект договора', NULL, 0, '1', NULL, NULL, NULL, 'ProjectAgreed');
    END IF;
END $$;

--<DO>--
-- 12136001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '10',
        VALUE = 'Иное',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'OtherReason'
    WHERE ITEMID = 12136001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136001, 12136, '10', 'Иное', NULL, NULL, NULL, NULL, NULL, NULL, 'OtherReason');
    END IF;
END $$;

--<DO>--
-- 12136002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '1',
        VALUE = 'Пожар',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Fire'
    WHERE ITEMID = 12136002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136002, 12136, '1', 'Пожар', NULL, NULL, NULL, NULL, NULL, NULL, 'Fire');
    END IF;
END $$;

--<DO>--
-- 12136003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '2',
        VALUE = 'Последствия тушения пожара',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ConsequencesOfFireExtinguishing'
    WHERE ITEMID = 12136003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136003, 12136, '2', 'Последствия тушения пожара', NULL, NULL, NULL, NULL, NULL, NULL, 'ConsequencesOfFireExtinguishing');
    END IF;
END $$;

--<DO>--
-- 12136004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '3',
        VALUE = 'Взрыв',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Explosion'
    WHERE ITEMID = 12136004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136004, 12136, '3', 'Взрыв', NULL, NULL, NULL, NULL, NULL, NULL, 'Explosion');
    END IF;
END $$;

--<DO>--
-- 12136005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '4',
        VALUE = 'Авария систем водоснабжения',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AccidentOfWaterSupplySystems'
    WHERE ITEMID = 12136005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136005, 12136, '4', 'Авария систем водоснабжения', NULL, NULL, NULL, NULL, NULL, NULL, 'AccidentOfWaterSupplySystems');
    END IF;
END $$;

--<DO>--
-- 12136006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '5',
        VALUE = 'Авария систем отопления',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AccidentOfHeatingSystems'
    WHERE ITEMID = 12136006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136006, 12136, '5', 'Авария систем отопления', NULL, NULL, NULL, NULL, NULL, NULL, 'AccidentOfHeatingSystems');
    END IF;
END $$;

--<DO>--
-- 12136007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '6',
        VALUE = 'Авария систем канализации',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'AccidentOfSewageSystems'
    WHERE ITEMID = 12136007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136007, 12136, '6', 'Авария систем канализации', NULL, NULL, NULL, NULL, NULL, NULL, 'AccidentOfSewageSystems');
    END IF;
END $$;

--<DO>--
-- 12136008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '7',
        VALUE = 'Авария внутреннего водостока',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DownholeAccident'
    WHERE ITEMID = 12136008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136008, 12136, '7', 'Авария внутреннего водостока', NULL, NULL, NULL, NULL, NULL, NULL, 'DownholeAccident');
    END IF;
END $$;

--<DO>--
-- 12136009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '8',
        VALUE = 'Сильный ветер',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'StrongWind'
    WHERE ITEMID = 12136009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136009, 12136, '8', 'Сильный ветер', NULL, NULL, NULL, NULL, NULL, NULL, 'StrongWind');
    END IF;
END $$;

--<DO>--
-- 12136010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12136,
        CODE = '9',
        VALUE = 'Противоправные действия третьих лиц',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'IllegalActions'
    WHERE ITEMID = 12136010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12136010, 12136, '9', 'Противоправные действия третьих лиц', NULL, NULL, NULL, NULL, NULL, NULL, 'IllegalActions');
    END IF;
END $$;

--<DO>--
-- 12172001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '0',
        VALUE = 'Подготовка процесса загрузки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Prepare'
    WHERE ITEMID = 12172001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172001, 12172, '0', 'Подготовка процесса загрузки', NULL, NULL, NULL, NULL, NULL, NULL, 'Prepare');
    END IF;
END $$;

--<DO>--
-- 12172002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '5',
        VALUE = 'Распаковка загружаемых файлов',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'UnpackageFiles'
    WHERE ITEMID = 12172002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172002, 12172, '5', 'Распаковка загружаемых файлов', NULL, NULL, NULL, NULL, NULL, NULL, 'UnpackageFiles');
    END IF;
END $$;

--<DO>--
-- 12172003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '15',
        VALUE = 'Обработка загружаемых файлов оплат',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BankPlatProcess'
    WHERE ITEMID = 12172003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172003, 12172, '15', 'Обработка загружаемых файлов оплат', NULL, NULL, NULL, NULL, NULL, NULL, 'BankPlatProcess');
    END IF;
END $$;

--<DO>--
-- 12172004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '30',
        VALUE = 'Обработка загружаемых файлов начислений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NachProcess'
    WHERE ITEMID = 12172004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172004, 12172, '30', 'Обработка загружаемых файлов начислений', NULL, NULL, NULL, NULL, NULL, NULL, 'NachProcess');
    END IF;
END $$;

--<DO>--
-- 12172005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '45',
        VALUE = 'Обработка загружаемых файлов зачислений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PlatProcess'
    WHERE ITEMID = 12172005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172005, 12172, '45', 'Обработка загружаемых файлов зачислений', NULL, NULL, NULL, NULL, NULL, NULL, 'PlatProcess');
    END IF;
END $$;

--<DO>--
-- 12173001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12173,
        CODE = '1',
        VALUE = 'Загружается',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Loading'
    WHERE ITEMID = 12173001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12173001, 12173, '1', 'Загружается', NULL, 0, '1', NULL, NULL, NULL, 'Loading');
    END IF;
END $$;

--<DO>--
-- 12173002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12173,
        CODE = '2',
        VALUE = 'Загружен',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Loaded'
    WHERE ITEMID = 12173002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12173002, 12173, '2', 'Загружен', NULL, 0, '1', NULL, NULL, NULL, 'Loaded');
    END IF;
END $$;

--<DO>--
-- 12172008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '100',
        VALUE = 'Загрузка завершена',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Finished'
    WHERE ITEMID = 12172008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172008, 12172, '100', 'Загрузка завершена', NULL, NULL, NULL, NULL, NULL, NULL, 'Finished');
    END IF;
END $$;

--<DO>--
-- 12172009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '100',
        VALUE = 'Ошибка загрузки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Error'
    WHERE ITEMID = 12172009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172009, 12172, '100', 'Ошибка загрузки', NULL, NULL, NULL, NULL, NULL, NULL, 'Error');
    END IF;
END $$;

--<DO>--
-- 12172007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '60',
        VALUE = 'Сохранение данных загрузки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DbSave'
    WHERE ITEMID = 12172007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172007, 12172, '60', 'Сохранение данных загрузки', NULL, NULL, NULL, NULL, NULL, NULL, 'DbSave');
    END IF;
END $$;

--<DO>--
-- 12172006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '75',
        VALUE = 'Установка критериев',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSet'
    WHERE ITEMID = 12172006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172006, 12172, '75', 'Установка критериев', NULL, NULL, NULL, NULL, NULL, NULL, 'CriteriaSet');
    END IF;
END $$;

--<DO>--
-- 12172010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '76.5',
        VALUE = 'Установка критериев начислений (1/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach1'
    WHERE ITEMID = 12172010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172010, 12172, '76.5', 'Установка критериев начислений (1/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach1');
    END IF;
END $$;

--<DO>--
-- 12172011
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '78',
        VALUE = 'Установка критериев начислений (2/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach2'
    WHERE ITEMID = 12172011;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172011, 12172, '78', 'Установка критериев начислений (2/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach2');
    END IF;
END $$;

--<DO>--
-- 12172012
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '80',
        VALUE = 'Установка критериев начислений (3/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach3'
    WHERE ITEMID = 12172012;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172012, 12172, '80', 'Установка критериев начислений (3/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach3');
    END IF;
END $$;

--<DO>--
-- 12172013
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '81.5',
        VALUE = 'Установка критериев начислений (4/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach4'
    WHERE ITEMID = 12172013;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172013, 12172, '81.5', 'Установка критериев начислений (4/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach4');
    END IF;
END $$;

--<DO>--
-- 12172014
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '83',
        VALUE = 'Установка критериев начислений (5/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach5'
    WHERE ITEMID = 12172014;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172014, 12172, '83', 'Установка критериев начислений (5/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach5');
    END IF;
END $$;

--<DO>--
-- 12172015
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '84.5',
        VALUE = 'Установка критериев начислений (6/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach6'
    WHERE ITEMID = 12172015;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172015, 12172, '84.5', 'Установка критериев начислений (6/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach6');
    END IF;
END $$;

--<DO>--
-- 12172016
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '86',
        VALUE = 'Установка критериев начислений (7/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach7'
    WHERE ITEMID = 12172016;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172016, 12172, '86', 'Установка критериев начислений (7/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach7');
    END IF;
END $$;

--<DO>--
-- 12172017
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '87.5',
        VALUE = 'Установка критериев начислений (8/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach8'
    WHERE ITEMID = 12172017;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172017, 12172, '87.5', 'Установка критериев начислений (8/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach8');
    END IF;
END $$;

--<DO>--
-- 12172018
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '89',
        VALUE = 'Установка критериев начислений (9/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach9'
    WHERE ITEMID = 12172018;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172018, 12172, '89', 'Установка критериев начислений (9/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach9');
    END IF;
END $$;

--<DO>--
-- 12172019
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '90.5',
        VALUE = 'Установка критериев начислений (10/10)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetNach10'
    WHERE ITEMID = 12172019;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172019, 12172, '90.5', 'Установка критериев начислений (10/10)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetNach10');
    END IF;
END $$;

--<DO>--
-- 12172020
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '92',
        VALUE = 'Установка критериев зачислений (1/5)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetPlat1'
    WHERE ITEMID = 12172020;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172020, 12172, '92', 'Установка критериев зачислений (1/5)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetPlat1');
    END IF;
END $$;

--<DO>--
-- 12172021
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '93.5',
        VALUE = 'Установка критериев зачислений (2/5)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetPlat2'
    WHERE ITEMID = 12172021;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172021, 12172, '93.5', 'Установка критериев зачислений (2/5)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetPlat2');
    END IF;
END $$;

--<DO>--
-- 12172022
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '95.5',
        VALUE = 'Установка критериев зачислений (3/5)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetPlat3'
    WHERE ITEMID = 12172022;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172022, 12172, '95.5', 'Установка критериев зачислений (3/5)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetPlat3');
    END IF;
END $$;

--<DO>--
-- 12172023
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '97',
        VALUE = 'Установка критериев зачислений (4/5)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetPlat4'
    WHERE ITEMID = 12172023;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172023, 12172, '97', 'Установка критериев зачислений (4/5)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetPlat4');
    END IF;
END $$;

--<DO>--
-- 12172024
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12172,
        CODE = '98.5',
        VALUE = 'Установка критериев зачислений (5/5)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CriteriaSetPlat5'
    WHERE ITEMID = 12172024;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12172024, 12172, '98.5', 'Установка критериев зачислений (5/5)', NULL, 0, NULL, NULL, NULL, NULL, 'CriteriaSetPlat5');
    END IF;
END $$;

--<DO>--
-- 12142001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12142,
        CODE = NULL,
        VALUE = 'Физическое лицо',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Individual'
    WHERE ITEMID = 12142001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12142001, 12142, NULL, 'Физическое лицо', NULL, NULL, NULL, NULL, NULL, NULL, 'Individual');
    END IF;
END $$;

--<DO>--
-- 12142002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12142,
        CODE = NULL,
        VALUE = 'Управляющая компания',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ManagementCompany'
    WHERE ITEMID = 12142002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12142002, 12142, NULL, 'Управляющая компания', NULL, NULL, NULL, NULL, NULL, NULL, 'ManagementCompany');
    END IF;
END $$;

--<DO>--
-- 12142003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12142,
        CODE = NULL,
        VALUE = 'Юридическое лицо',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LegalEntity'
    WHERE ITEMID = 12142003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12142003, 12142, NULL, 'Юридическое лицо', NULL, NULL, NULL, NULL, NULL, NULL, 'LegalEntity');
    END IF;
END $$;

--<DO>--
-- 12174001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12174,
        CODE = '1',
        VALUE = 'Подготовка процесса идентифкации',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Prepare'
    WHERE ITEMID = 12174001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12174001, 12174, '1', 'Подготовка процесса идентифкации', NULL, 0, '1', NULL, NULL, NULL, 'Prepare');
    END IF;
END $$;

--<DO>--
-- 12174002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12174,
        CODE = '2',
        VALUE = 'Идентификация зачислений',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Identify'
    WHERE ITEMID = 12174002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12174002, 12174, '2', 'Идентификация зачислений', NULL, 0, '1', NULL, NULL, NULL, 'Identify');
    END IF;
END $$;

--<DO>--
-- 12174003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12174,
        CODE = '3',
        VALUE = 'Идентификация завершена',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = '1',
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Finished'
    WHERE ITEMID = 12174003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12174003, 12174, '3', 'Идентификация завершена', NULL, 0, '1', NULL, NULL, NULL, 'Finished');
    END IF;
END $$;

--<DO>--
-- 12175001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '1',
        VALUE = 'Изменение суммы платежа',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SumOpl'
    WHERE ITEMID = 12175001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175001, 12175, '1', 'Изменение суммы платежа', NULL, 0, NULL, NULL, NULL, NULL, 'SumOpl');
    END IF;
END $$;

--<DO>--
-- 12175002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '2',
        VALUE = 'Изменение даты платежа',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'DateOpl'
    WHERE ITEMID = 12175002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175002, 12175, '2', 'Изменение даты платежа', NULL, 0, NULL, NULL, NULL, NULL, 'DateOpl');
    END IF;
END $$;

--<DO>--
-- 12175003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '3',
        VALUE = 'Изменение UNOM',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Unom'
    WHERE ITEMID = 12175003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175003, 12175, '3', 'Изменение UNOM', NULL, 0, NULL, NULL, NULL, NULL, 'Unom');
    END IF;
END $$;

--<DO>--
-- 12175004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '4',
        VALUE = 'Изменение кода плательщика',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Kodpl'
    WHERE ITEMID = 12175004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175004, 12175, '4', 'Изменение кода плательщика', NULL, 0, NULL, NULL, NULL, NULL, 'Kodpl');
    END IF;
END $$;

--<DO>--
-- 12175005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '5',
        VALUE = 'Изменение адреса дома',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Adres'
    WHERE ITEMID = 12175005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175005, 12175, '5', 'Изменение адреса дома', NULL, 0, NULL, NULL, NULL, NULL, 'Adres');
    END IF;
END $$;

--<DO>--
-- 12175006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '6',
        VALUE = 'Изменение номера квартиры',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Kvnom'
    WHERE ITEMID = 12175006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175006, 12175, '6', 'Изменение номера квартиры', NULL, 0, NULL, NULL, NULL, NULL, 'Kvnom');
    END IF;
END $$;

--<DO>--
-- 12175007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '7',
        VALUE = 'Изменение суммы начисления',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SumNach'
    WHERE ITEMID = 12175007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175007, 12175, '7', 'Изменение суммы начисления', NULL, 0, NULL, NULL, NULL, NULL, 'SumNach');
    END IF;
END $$;

--<DO>--
-- 12175008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '8',
        VALUE = 'Изменение суммы зачисления',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'SumZach'
    WHERE ITEMID = 12175008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175008, 12175, '8', 'Изменение суммы зачисления', NULL, 0, NULL, NULL, NULL, NULL, 'SumZach');
    END IF;
END $$;

--<DO>--
-- 12175009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '9',
        VALUE = 'Изменение статуса идентификации',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'StatusIdentif'
    WHERE ITEMID = 12175009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175009, 12175, '9', 'Изменение статуса идентификации', NULL, 0, NULL, NULL, NULL, NULL, 'StatusIdentif');
    END IF;
END $$;

--<DO>--
-- 12093004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12093,
        CODE = '',
        VALUE = 'Не подтверждена банком',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'NotConfirmedByBank'
    WHERE ITEMID = 12093004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12093004, 12093, '', 'Не подтверждена банком', NULL, NULL, NULL, NULL, NULL, NULL, 'NotConfirmedByBank');
    END IF;
END $$;

--<DO>--
-- 12175010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12175,
        CODE = '10',
        VALUE = 'Изменение связи строки зачисления с оплатой',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = 0,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'LinkBankPlat'
    WHERE ITEMID = 12175010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12175010, 12175, '10', 'Изменение связи строки зачисления с оплатой', NULL, 0, NULL, NULL, NULL, NULL, 'LinkBankPlat');
    END IF;
END $$;

--<DO>--
-- 12119006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12119,
        CODE = NULL,
        VALUE = 'Обработан частично',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'ProcessedPartially'
    WHERE ITEMID = 12119006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12119006, 12119, NULL, 'Обработан частично', NULL, NULL, NULL, NULL, NULL, NULL, 'ProcessedPartially');
    END IF;
END $$;

--<DO>--
-- 307001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 307,
        CODE = '1',
        VALUE = 'ФИАС',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Fias'
    WHERE ITEMID = 307001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (307001, 307, '1', 'ФИАС', NULL, NULL, NULL, NULL, NULL, NULL, 'Fias');
    END IF;
END $$;

--<DO>--
-- 307002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 307,
        CODE = '2',
        VALUE = 'БТИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Bti'
    WHERE ITEMID = 307002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (307002, 307, '2', 'БТИ', NULL, NULL, NULL, NULL, NULL, NULL, 'Bti');
    END IF;
END $$;

--<DO>--
-- 307003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 307,
        CODE = '3',
        VALUE = 'ЕГРН',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Egrn'
    WHERE ITEMID = 307003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (307003, 307, '3', 'ЕГРН', NULL, NULL, NULL, NULL, NULL, NULL, 'Egrn');
    END IF;
END $$;

--<DO>--
-- 307004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 307,
        CODE = '4',
        VALUE = 'ФИАС/БТИ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FiasBti'
    WHERE ITEMID = 307004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (307004, 307, '4', 'ФИАС/БТИ', NULL, NULL, NULL, NULL, NULL, NULL, 'FiasBti');
    END IF;
END $$;

--<DO>--
-- 307005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 307,
        CODE = '5',
        VALUE = 'ФИАС/ЕГРН',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'FiasEgrn'
    WHERE ITEMID = 307005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (307005, 307, '5', 'ФИАС/ЕГРН', NULL, NULL, NULL, NULL, NULL, NULL, 'FiasEgrn');
    END IF;
END $$;

--<DO>--
-- 12143001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12143,
        CODE = NULL,
        VALUE = 'Здания полносборные из железобетонных панелей или блоков',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'PrefabricatedReinforcedPanelsBlocks'
    WHERE ITEMID = 12143001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12143001, 12143, NULL, 'Здания полносборные из железобетонных панелей или блоков', NULL, NULL, NULL, NULL, NULL, NULL, 'PrefabricatedReinforcedPanelsBlocks');
    END IF;
END $$;

--<DO>--
-- 12143002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12143,
        CODE = NULL,
        VALUE = 'Здания кирпичные',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Brick'
    WHERE ITEMID = 12143002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12143002, 12143, NULL, 'Здания кирпичные', NULL, NULL, NULL, NULL, NULL, NULL, 'Brick');
    END IF;
END $$;

--<DO>--
-- 12143003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12143,
        CODE = NULL,
        VALUE = 'Здания из облегчённых блоков',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Lightweight'
    WHERE ITEMID = 12143003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12143003, 12143, NULL, 'Здания из облегчённых блоков', NULL, NULL, NULL, NULL, NULL, NULL, 'Lightweight');
    END IF;
END $$;

--<DO>--
-- 12143004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12143,
        CODE = NULL,
        VALUE = 'Здания смешанные (кирпичные, деревянные)',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Mixed'
    WHERE ITEMID = 12143004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12143004, 12143, NULL, 'Здания смешанные (кирпичные, деревянные)', NULL, NULL, NULL, NULL, NULL, NULL, 'Mixed');
    END IF;
END $$;

--<DO>--
-- 12143005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12143,
        CODE = NULL,
        VALUE = 'Здания брусчатые или бревенчатые',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BlockTimbered'
    WHERE ITEMID = 12143005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12143005, 12143, NULL, 'Здания брусчатые или бревенчатые', NULL, NULL, NULL, NULL, NULL, NULL, 'BlockTimbered');
    END IF;
END $$;

--<DO>--
-- 12143006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12143,
        CODE = NULL,
        VALUE = 'Здания из монолитного железобетона',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'MonolithicReinforced'
    WHERE ITEMID = 12143006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12143006, 12143, NULL, 'Здания из монолитного железобетона', NULL, NULL, NULL, NULL, NULL, NULL, 'MonolithicReinforced');
    END IF;
END $$;

--<DO>--
-- 12144001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания до 3-х этажей включительно',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsTthreeFloors'
    WHERE ITEMID = 12144001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144001, 12144, NULL, 'Здания до 3-х этажей включительно', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsTthreeFloors');
    END IF;
END $$;

--<DO>--
-- 12144002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания до 4-х этажей включительно',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsFourFloors'
    WHERE ITEMID = 12144002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144002, 12144, NULL, 'Здания до 4-х этажей включительно', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsFourFloors');
    END IF;
END $$;

--<DO>--
-- 12144004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания до 4-х этажей включительно с центральным отоплением и горячим водоснабжением',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsFourFloorsCentralHeating'
    WHERE ITEMID = 12144004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144004, 12144, NULL, 'Здания до 4-х этажей включительно с центральным отоплением и горячим водоснабжением', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsFourFloorsCentralHeating');
    END IF;
END $$;

--<DO>--
-- 12144003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания до 4-х этажей включительно с центральным отоплением и горячим водоснабжением от АГВ',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsFourFloorsCentralHeatingAGW'
    WHERE ITEMID = 12144003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144003, 12144, NULL, 'Здания до 4-х этажей включительно с центральным отоплением и горячим водоснабжением от АГВ', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsFourFloorsCentralHeatingAGW');
    END IF;
END $$;

--<DO>--
-- 12144006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания  5 – 13 этажей',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsThirteenFloors'
    WHERE ITEMID = 12144006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144006, 12144, NULL, 'Здания  5 – 13 этажей', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsThirteenFloors');
    END IF;
END $$;

--<DO>--
-- 12144005
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания до 5-и этажей включительно',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsFiveFloors'
    WHERE ITEMID = 12144005;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144005, 12144, NULL, 'Здания до 5-и этажей включительно', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsFiveFloors');
    END IF;
END $$;

--<DO>--
-- 12144007
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания 14 этажей и выше',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsFourteenFloors'
    WHERE ITEMID = 12144007;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144007, 12144, NULL, 'Здания 14 этажей и выше', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsFourteenFloors');
    END IF;
END $$;

--<DO>--
-- 12144008
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания произвольной (в т.ч. переменной) этажности',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsVariable'
    WHERE ITEMID = 12144008;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144008, 12144, NULL, 'Здания произвольной (в т.ч. переменной) этажности', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsVariable');
    END IF;
END $$;

--<DO>--
-- 12144009
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания произвольной (в т.ч. переменной) этажности с ж.б. перекрытиями',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsVariableReinforced'
    WHERE ITEMID = 12144009;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144009, 12144, NULL, 'Здания произвольной (в т.ч. переменной) этажности с ж.б. перекрытиями', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsVariableReinforced');
    END IF;
END $$;

--<DO>--
-- 12144010
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12144,
        CODE = NULL,
        VALUE = 'Здания произвольной (в т.ч. переменной) этажности с деревянными перекрытиями и перегородками',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'BuildingsVariableWood'
    WHERE ITEMID = 12144010;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12144010, 12144, NULL, 'Здания произвольной (в т.ч. переменной) этажности с деревянными перекрытиями и перегородками', NULL, NULL, NULL, NULL, NULL, NULL, 'BuildingsVariableWood');
    END IF;
END $$;

--<DO>--
-- 12176001
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12176,
        CODE = '0',
        VALUE = 'Подготовка процесса обработки',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Prepare'
    WHERE ITEMID = 12176001;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12176001, 12176, '0', 'Подготовка процесса обработки', NULL, NULL, NULL, NULL, NULL, NULL, 'Prepare');
    END IF;
END $$;

--<DO>--
-- 12176002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12176,
        CODE = '75',
        VALUE = 'Создание/связка с ФСП',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'CreateBindFsp'
    WHERE ITEMID = 12176002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12176002, 12176, '75', 'Создание/связка с ФСП', NULL, NULL, NULL, NULL, NULL, NULL, 'CreateBindFsp');
    END IF;
END $$;

--<DO>--
-- 12176003
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12176,
        CODE = '100',
        VALUE = 'Перерасчет ФСП',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'RecalcFsp'
    WHERE ITEMID = 12176003;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12176003, 12176, '100', 'Перерасчет ФСП', NULL, NULL, NULL, NULL, NULL, NULL, 'RecalcFsp');
    END IF;
END $$;

--<DO>--
-- 12176004
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12176,
        CODE = '100',
        VALUE = 'Обработка завершена',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Finished'
    WHERE ITEMID = 12176004;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12176004, 12176, '100', 'Обработка завершена', NULL, NULL, NULL, NULL, NULL, NULL, 'Finished');
    END IF;
END $$;

--<DO>--
-- 12170002
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12170,
        CODE = NULL,
        VALUE = 'Счет включен в реестр выплат',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'Included'
    WHERE ITEMID = 12170002;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12170002, 12170, NULL, 'Счет включен в реестр выплат', NULL, NULL, NULL, NULL, NULL, NULL, 'Included');
    END IF;
END $$;

--<DO>--
-- 12170006
DO $$
BEGIN
    UPDATE  CORE_REFERENCE_ITEM
    SET REFERENCEID = 12170,
        CODE = NULL,
        VALUE = 'Счет передан на оплату',
        SHORT_TITLE = NULL,
        IS_ARCHIVES = NULL,
        USER_NAME = NULL,
        DATE_END_CHANGE = NULL,
        DATE_S = NULL,
        FLAG = NULL,
        NAME = 'TransferredPayment'
    WHERE ITEMID = 12170006;

    IF(not found)THEN
       INSERT INTO CORE_REFERENCE_ITEM (ITEMID,REFERENCEID,CODE,VALUE,SHORT_TITLE,IS_ARCHIVES,USER_NAME,DATE_END_CHANGE,DATE_S,FLAG,NAME)
       VALUES (12170006, 12170, NULL, 'Счет передан на оплату', NULL, NULL, NULL, NULL, NULL, NULL, 'TransferredPayment');
    END IF;
END $$;

