
-- I. Загрузка реестров
--<DO>--
                -- 920. 'Поименнованные списки объектов реестров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 920;

                    UPDATE core_register
                       SET registername             = 'Core.Register.List',
                           registerdescription      = 'Поименнованные списки объектов реестров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LIST',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 920;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (920, 'Core.Register.List', 'Поименнованные списки объектов реестров', NULL, NULL, 'CORE_LIST', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 921. 'Идентификаторы объектов, входящих в список'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 921;

                    UPDATE core_register
                       SET registername             = 'Core.Register.ListObject',
                           registerdescription      = 'Идентификаторы объектов, входящих в список',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LIST_OBJECT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 921;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (921, 'Core.Register.ListObject', 'Идентификаторы объектов, входящих в список', NULL, NULL, 'CORE_LIST_OBJECT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 924. 'Тип колонки раскладки'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 924;

                    UPDATE core_register
                       SET registername             = 'Core.Register.LayoutColumnType',
                           registerdescription      = 'Тип колонки раскладки',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LAYOUT_COLUMN_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 924;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (924, 'Core.Register.LayoutColumnType', 'Тип колонки раскладки', NULL, NULL, 'CORE_LAYOUT_COLUMN_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 925. 'Пользовательские настройки представления реестра'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 925;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserSettingsRegisterView',
                           registerdescription      = 'Пользовательские настройки представления реестра',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USERSETTINGSREGVIEW',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 925;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (925, 'Core.SRD.UserSettingsRegisterView', 'Пользовательские настройки представления реестра', NULL, NULL, 'CORE_SRD_USERSETTINGSREGVIEW', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 930. 'Список реестров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 930;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Register',
                           registerdescription      = 'Список реестров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 930;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (930, 'Core.Register.Register', 'Список реестров', NULL, NULL, 'CORE_REGISTER', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 931. 'Список показателей реестра'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 931;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Attribute',
                           registerdescription      = 'Список показателей реестра',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER_ATTRIBUTE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 931;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (931, 'Core.Register.Attribute', 'Список показателей реестра', NULL, NULL, 'CORE_REGISTER_ATTRIBUTE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 932. 'Список связей между реестрами'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 932;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Relation',
                           registerdescription      = 'Список связей между реестрами',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER_RELATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 932;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (932, 'Core.Register.Relation', 'Список связей между реестрами', NULL, NULL, 'CORE_REGISTER_RELATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 933. 'Раскладки'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 933;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Layout',
                           registerdescription      = 'Раскладки',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LAYOUT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_LAYOUT_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 933;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (933, 'Core.Register.Layout', 'Раскладки', NULL, NULL, 'CORE_LAYOUT', 4, NULL, 'CORE_LAYOUT_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 935. 'Детализация раскладок'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 935;

                    UPDATE core_register
                       SET registername             = 'Core.Register.LayoutDetail',
                           registerdescription      = 'Детализация раскладок',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LAYOUT_DETAILS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_LAYOUT_DET_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 935;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (935, 'Core.Register.LayoutDetail', 'Детализация раскладок', NULL, NULL, 'CORE_LAYOUT_DETAILS', 4, NULL, 'CORE_LAYOUT_DET_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 936. 'Фильтры реестров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 936;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Qry',
                           registerdescription      = 'Фильтры реестров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_QRY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'QRY_QRYID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 936;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (936, 'Core.Register.Qry', 'Фильтры реестров', NULL, NULL, 'CORE_QRY', 4, NULL, 'QRY_QRYID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 937. 'Условия фильтров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 937;

                    UPDATE core_register
                       SET registername             = 'Core.Register.QryFilter',
                           registerdescription      = 'Условия фильтров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_QRY_FILTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'QRYFILTER_QRYFILTERID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 937;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (937, 'Core.Register.QryFilter', 'Условия фильтров', NULL, NULL, 'CORE_QRY_FILTER', 4, NULL, 'QRYFILTER_QRYFILTERID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 938. 'Операции фильтров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 938;

                    UPDATE core_register
                       SET registername             = 'Core.Register.QryOperation',
                           registerdescription      = 'Операции фильтров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_QRY_OPERATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'QRYOPERATION_QRYOPERATIONI_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 938;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (938, 'Core.Register.QryOperation', 'Операции фильтров', NULL, NULL, 'CORE_QRY_OPERATION', 4, NULL, 'QRYOPERATION_QRYOPERATIONI_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 939. 'Блокировка объекта реестра'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 939;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Lock',
                           registerdescription      = 'Блокировка объекта реестра',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER_LOCK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 939;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (939, 'Core.Register.Lock', 'Блокировка объекта реестра', NULL, NULL, 'CORE_REGISTER_LOCK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 940. 'Аудит действий пользователей с функциями модулей (подсистем) системы'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 940;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Audit',
                           registerdescription      = 'Аудит действий пользователей с функциями модулей (подсистем) системы',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_AUDIT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 940;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (940, 'Core.SRD.Audit', 'Аудит действий пользователей с функциями модулей (подсистем) системы', NULL, NULL, 'CORE_SRD_AUDIT', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 941. 'Подразделение в организации пользователя системы'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 941;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Department',
                           registerdescription      = 'Подразделение в организации пользователя системы',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_DEPARTMENT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 941;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (941, 'Core.SRD.Department', 'Подразделение в организации пользователя системы', NULL, NULL, 'CORE_SRD_DEPARTMENT', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 942. 'Функции модулей (подсистем) системы'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 942;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Function',
                           registerdescription      = 'Функции модулей (подсистем) системы',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_FUNCTION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 942;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (942, 'Core.SRD.Function', 'Функции модулей (подсистем) системы', NULL, NULL, 'CORE_SRD_FUNCTION', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 945. 'Роли в системе'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 945;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Role',
                           registerdescription      = 'Роли в системе',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 945;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (945, 'Core.SRD.Role', 'Роли в системе', NULL, NULL, 'CORE_SRD_ROLE', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 946. 'Функции роли (бывшая LOCROLE_LOCFUNCTION)'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 946;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RoleFunction',
                           registerdescription      = 'Функции роли (бывшая LOCROLE_LOCFUNCTION)',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE_FUNCTION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 946;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (946, 'Core.SRD.RoleFunction', 'Функции роли (бывшая LOCROLE_LOCFUNCTION)', NULL, NULL, 'CORE_SRD_ROLE_FUNCTION', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 947. 'Права доступа роли к реестру'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 947;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RoleRegister',
                           registerdescription      = 'Права доступа роли к реестру',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE_REGISTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 947;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (947, 'Core.SRD.RoleRegister', 'Права доступа роли к реестру', NULL, NULL, 'CORE_SRD_ROLE_REGISTER', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 948. 'Права доступа роли к атрибутам реестра'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 948;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RoleAttr',
                           registerdescription      = 'Права доступа роли к атрибутам реестра',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE_ATTR',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 948;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (948, 'Core.SRD.RoleAttr', 'Права доступа роли к атрибутам реестра', NULL, NULL, 'CORE_SRD_ROLE_ATTR', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 949. 'Параметры сессии пользователя'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 949;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Session',
                           registerdescription      = 'Параметры сессии пользователя',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_SESSION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 949;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (949, 'Core.SRD.Session', 'Параметры сессии пользователя', NULL, NULL, 'CORE_SRD_SESSION', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 950. 'Пользователи системы'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 950;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.User',
                           registerdescription      = 'Пользователи системы',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 950;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (950, 'Core.SRD.User', 'Пользователи системы', NULL, NULL, 'CORE_SRD_USER', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 951. 'Пользовательские настройки'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 951;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserSettings',
                           registerdescription      = 'Пользовательские настройки',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USERSETTINGS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 951;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (951, 'Core.SRD.UserSettings', 'Пользовательские настройки', NULL, NULL, 'CORE_SRD_USERSETTINGS', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 952. 'Роли пользователя'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 952;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserRole',
                           registerdescription      = 'Роли пользователя',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USER_ROLE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 952;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (952, 'Core.SRD.UserRole', 'Роли пользователя', NULL, NULL, 'CORE_SRD_USER_ROLE', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 953. 'Пользователи системы'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 953;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserAlt',
                           registerdescription      = 'Пользователи системы',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 953;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (953, 'Core.SRD.UserAlt', 'Пользователи системы', NULL, NULL, 'CORE_SRD_USER', 4, NULL, 'REG_OBJECT_SEQ', 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 954. 'Пользовательские настройки раскладкок'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 954;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserSettingsLayout',
                           registerdescription      = 'Пользовательские настройки раскладкок',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USERSETTINGSLAYOUT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 954;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (954, 'Core.SRD.UserSettingsLayout', 'Пользовательские настройки раскладкок', NULL, NULL, 'CORE_SRD_USERSETTINGSLAYOUT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 955. 'Разграничение прав доступа по данным реестров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 955;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RoleFilter',
                           registerdescription      = 'Разграничение прав доступа по данным реестров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE_FILTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 955;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (955, 'Core.SRD.RoleFilter', 'Разграничение прав доступа по данным реестров', NULL, NULL, 'CORE_SRD_ROLE_FILTER', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 956. 'Выгрузка данных по раскладке'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 956;

                    UPDATE core_register
                       SET registername             = 'Core.Register.LayoutExport',
                           registerdescription      = 'Выгрузка данных по раскладке',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LAYOUT_EXPORT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 956;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (956, 'Core.Register.LayoutExport', 'Выгрузка данных по раскладке', NULL, NULL, 'CORE_LAYOUT_EXPORT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 957. 'Категории доступа к данным реестров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 957;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RegisterCategory',
                           registerdescription      = 'Категории доступа к данным реестров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_REGISTER_CATEGORY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 957;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (957, 'Core.SRD.RegisterCategory', 'Категории доступа к данным реестров', NULL, NULL, 'CORE_SRD_REGISTER_CATEGORY', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 958. 'Доступ функции к категориям доступа реестров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 958;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.FunctionRegisterCategory',
                           registerdescription      = 'Доступ функции к категориям доступа реестров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_FUNCTION_REG_CAT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 958;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (958, 'Core.SRD.FunctionRegisterCategory', 'Доступ функции к категориям доступа реестров', NULL, NULL, 'CORE_SRD_FUNCTION_REG_CAT', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 960. 'Шаблон (тип) технологического документа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 960;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Template',
                           registerdescription      = 'Шаблон (тип) технологического документа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TEMPLATE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 960;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (960, 'Core.TD.Template', 'Шаблон (тип) технологического документа', NULL, NULL, 'CORE_TD_TEMPLATE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 961. 'Версия шаблона технологического документа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 961;

                    UPDATE core_register
                       SET registername             = 'Core.TD.TemplateVersion',
                           registerdescription      = 'Версия шаблона технологического документа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TEMPLATE_VERSION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 961;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (961, 'Core.TD.TemplateVersion', 'Версия шаблона технологического документа', NULL, NULL, 'CORE_TD_TEMPLATE_VERSION', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 962. 'Типы статусов экземпляра технологического документа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 962;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Status',
                           registerdescription      = 'Типы статусов экземпляра технологического документа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_STATUS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 962;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (962, 'Core.TD.Status', 'Типы статусов экземпляра технологического документа', NULL, NULL, 'CORE_TD_STATUS', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 963. 'Экземпляры технологическох документов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 963;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Instance',
                           registerdescription      = 'Экземпляры технологическох документов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_INSTANCE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 963;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (963, 'Core.TD.Instance', 'Экземпляры технологическох документов', NULL, NULL, 'CORE_TD_INSTANCE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 964. 'Набор изменений в реестрах'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 964;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Changeset',
                           registerdescription      = 'Набор изменений в реестрах',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_CHANGESET',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 964;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (964, 'Core.TD.Changeset', 'Набор изменений в реестрах', NULL, NULL, 'CORE_TD_CHANGESET', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 965. 'Изменение в реестре'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 965;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Change',
                           registerdescription      = 'Изменение в реестре',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_CHANGE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 965;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (965, 'Core.TD.Change', 'Изменение в реестре', NULL, NULL, 'CORE_TD_CHANGE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 966. 'Типы аудируемых действий с экземпляром технологического документа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 966;

                    UPDATE core_register
                       SET registername             = 'Core.TD.AuditAction',
                           registerdescription      = 'Типы аудируемых действий с экземпляром технологического документа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_AUDIT_ACTION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 966;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (966, 'Core.TD.AuditAction', 'Типы аудируемых действий с экземпляром технологического документа', NULL, NULL, 'CORE_TD_AUDIT_ACTION', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 967. 'Аудит действий с экземпляром технологического документа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 967;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Audit',
                           registerdescription      = 'Аудит действий с экземпляром технологического документа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_AUDIT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 967;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (967, 'Core.TD.Audit', 'Аудит действий с экземпляром технологического документа', NULL, NULL, 'CORE_TD_AUDIT', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 968. 'Дерево шаблонов '
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 968;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Tree',
                           registerdescription      = 'Дерево шаблонов ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TREE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 968;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (968, 'Core.TD.Tree', 'Дерево шаблонов ', NULL, NULL, 'CORE_TD_TREE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 969. 'Электронные образы экземпляра документа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 969;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Attachment',
                           registerdescription      = 'Электронные образы экземпляра документа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_ATTACHMENTS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 969;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (969, 'Core.TD.Attachment', 'Электронные образы экземпляра документа', NULL, NULL, 'CORE_TD_ATTACHMENTS', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 970. 'Данные об уведомлении процесса, из которого создан экземпляр технологического документа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 970;

                    UPDATE core_register
                       SET registername             = 'Core.TD.TP',
                           registerdescription      = 'Данные об уведомлении процесса, из которого создан экземпляр технологического документа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TP',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 970;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (970, 'Core.TD.TP', 'Данные об уведомлении процесса, из которого создан экземпляр технологического документа', NULL, NULL, 'CORE_TD_TP', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 971. 'Тип шаблона технологического документа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 971;

                    UPDATE core_register
                       SET registername             = 'Core.TD.TemplateType',
                           registerdescription      = 'Тип шаблона технологического документа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TEMPLATE_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 971;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (971, 'Core.TD.TemplateType', 'Тип шаблона технологического документа', NULL, NULL, 'CORE_TD_TEMPLATE_TYPE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 975. 'Очередь долгих процессов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 975;

                    UPDATE core_register
                       SET registername             = 'Core.LongProcess.Queue',
                           registerdescription      = 'Очередь долгих процессов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LONG_PROCESS_QUEUE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 975;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (975, 'Core.LongProcess.Queue', 'Очередь долгих процессов', NULL, NULL, 'CORE_LONG_PROCESS_QUEUE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 976. 'Типы долгих процессов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 976;

                    UPDATE core_register
                       SET registername             = 'Core.LongProcess.ProcessType',
                           registerdescription      = 'Типы долгих процессов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LONG_PROCESS_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 976;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (976, 'Core.LongProcess.ProcessType', 'Типы долгих процессов', NULL, NULL, 'CORE_LONG_PROCESS_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 320. 'Справочник округов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 320;

                    UPDATE core_register
                       SET registername             = 'Insur.Okrug',
                           registerdescription      = 'Справочник округов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_OKRUG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 320;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (320, 'Insur.Okrug', 'Справочник округов', NULL, NULL, 'INSUR_OKRUG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 977. 'Журнал менеджера долгих процессов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 977;

                    UPDATE core_register
                       SET registername             = 'Core.LongProcess.Log',
                           registerdescription      = 'Журнал менеджера долгих процессов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LONG_PROCESS_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 977;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (977, 'Core.LongProcess.Log', 'Журнал менеджера долгих процессов', NULL, NULL, 'CORE_LONG_PROCESS_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 978. 'Файлы конфигурации'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 978;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Configparam',
                           registerdescription      = 'Файлы конфигурации',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_CONFIGPARAM',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 978;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (978, 'Core.Shared.Configparam', 'Файлы конфигурации', NULL, NULL, 'CORE_CONFIGPARAM', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 982. 'Справочник'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 982;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Reference',
                           registerdescription      = 'Справочник',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REFERENCE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REFERENCE_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 982;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (982, 'Core.Shared.Reference', 'Справочник', NULL, NULL, 'CORE_REFERENCE', 4, NULL, 'REFERENCE_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 983. 'Справочное значение'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 983;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.ReferenceItem',
                           registerdescription      = 'Справочное значение',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REFERENCE_ITEM',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REFITEM_ITEMID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 983;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (983, 'Core.Shared.ReferenceItem', 'Справочное значение', NULL, NULL, 'CORE_REFERENCE_ITEM', 4, NULL, 'REFITEM_ITEMID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 984. 'Связи справочников'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 984;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.ReferenceRelation',
                           registerdescription      = 'Связи справочников',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REFERENCE_RELATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REFRELATION_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 984;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (984, 'Core.Shared.ReferenceRelation', 'Связи справочников', NULL, NULL, 'CORE_REFERENCE_RELATION', 4, NULL, 'REFRELATION_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 985. 'Связи справочных значений'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 985;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.ReferenceTree',
                           registerdescription      = 'Связи справочных значений',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REFERENCE_TREE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'TREEHELPER_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 985;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (985, 'Core.Shared.ReferenceTree', 'Связи справочных значений', NULL, NULL, 'CORE_REFERENCE_TREE', 4, NULL, 'TREEHELPER_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 986. 'Образ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 986;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Attachment',
                           registerdescription      = 'Образ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_ATTACHMENT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 986;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (986, 'Core.Shared.Attachment', 'Образ', NULL, NULL, 'CORE_ATTACHMENT', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 987. 'Файлы образа'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 987;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.AttachmentFile',
                           registerdescription      = 'Файлы образа',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_ATTACHMENT_FILE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 987;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (987, 'Core.Shared.AttachmentFile', 'Файлы образа', NULL, NULL, 'CORE_ATTACHMENT_FILE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 988. 'Связь образа и объекта реестра'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 988;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.AttachmentObject',
                           registerdescription      = 'Связь образа и объекта реестра',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_ATTACHMENT_OBJECT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 988;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (988, 'Core.Shared.AttachmentObject', 'Связь образа и объекта реестра', NULL, NULL, 'CORE_ATTACHMENT_OBJECT', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 989. 'Журнал ошибок'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 989;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.ErrorLog',
                           registerdescription      = 'Журнал ошибок',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_ERROR_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_ERROR_LOG_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 989;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (989, 'Core.Shared.ErrorLog', 'Журнал ошибок', NULL, NULL, 'CORE_ERROR_LOG', 4, NULL, 'CORE_ERROR_LOG_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 991. 'Сохраненные состояния представлений '
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 991;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.RegisterState',
                           registerdescription      = 'Сохраненные состояния представлений ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER_STATE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 991;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (991, 'Core.Shared.RegisterState', 'Сохраненные состояния представлений ', NULL, NULL, 'CORE_REGISTER_STATE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 992. 'Журнал отладочных сообщений'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 992;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Diagnostics',
                           registerdescription      = 'Журнал отладочных сообщений',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_DIAGNOSTICS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_DIAGNOSTICS_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 992;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (992, 'Core.Shared.Diagnostics', 'Журнал отладочных сообщений', NULL, NULL, 'CORE_DIAGNOSTICS', 4, NULL, 'CORE_DIAGNOSTICS_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 993. 'Родительский реестр'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 993;

                    UPDATE core_register
                       SET registername             = 'Core.Register.RegisterParent',
                           registerdescription      = 'Родительский реестр',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 993;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (993, 'Core.Register.RegisterParent', 'Родительский реестр', NULL, NULL, 'CORE_REGISTER', 4, NULL, 'REG_OBJECT_SEQ', 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 994. 'Репозиторий регистрационных номеров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 994;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.RegNomRepository',
                           registerdescription      = 'Репозиторий регистрационных номеров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGNOM_REPOSITORY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 994;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (994, 'Core.Shared.RegNomRepository', 'Репозиторий регистрационных номеров', NULL, NULL, 'CORE_REGNOM_REPOSITORY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 995. 'Последовательности регистрационных номеров'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 995;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.RegNomSequences',
                           registerdescription      = 'Последовательности регистрационных номеров',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGNOM_SEQUENCES',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 995;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (995, 'Core.Shared.RegNomSequences', 'Последовательности регистрационных номеров', NULL, NULL, 'CORE_REGNOM_SEQUENCES', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 996. 'Временные метки обновления кэша'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 996;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.CacheUpdates',
                           registerdescription      = 'Временные метки обновления кэша',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_CACHE_UPDATES',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 996;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (996, 'Core.Shared.CacheUpdates', 'Временные метки обновления кэша', NULL, NULL, 'CORE_CACHE_UPDATES', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 997. 'Журнал обновления структуры БД'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 997;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.UpdateStructure',
                           registerdescription      = 'Журнал обновления структуры БД',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_UPDSTRU_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 997;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (997, 'Core.Shared.UpdateStructure', 'Журнал обновления структуры БД', NULL, NULL, 'CORE_UPDSTRU_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 998. 'Выходные и праздничные дни'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 998;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Holiday',
                           registerdescription      = 'Выходные и праздничные дни',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_HOLIDAYS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 998;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (998, 'Core.Shared.Holiday', 'Выходные и праздничные дни', NULL, NULL, 'CORE_HOLIDAYS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 321. 'Справочник районов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 321;

                    UPDATE core_register
                       SET registername             = 'Insur.District',
                           registerdescription      = 'Справочник районов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DISTRICT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 321;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (321, 'Insur.District', 'Справочник районов', NULL, NULL, 'INSUR_DISTRICT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 44. 'Должностные лица'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 44;

                    UPDATE core_register
                       SET registername             = 'EMPLOYEE',
                           registerdescription      = 'Должностные лица',
                           allpri_table             = NULL,
                           object_table             = 'R_EMPL_O',
                           quant_table              = 'R_EMPL_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 44;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (44, 'EMPLOYEE', 'Должностные лица', NULL, 'R_EMPL_O', 'R_EMPL_Q', 2, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 308. 'Реестр Финансовых счетов плательщиков (ФСП)'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 308;

                    UPDATE core_register
                       SET registername             = 'Insur.Fsp',
                           registerdescription      = 'Реестр Финансовых счетов плательщиков (ФСП)',
                           allpri_table             = NULL,
                           object_table             = 'INSUR_FSP_O',
                           quant_table              = 'INSUR_FSP_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 308;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (308, 'Insur.Fsp', 'Реестр Финансовых счетов плательщиков (ФСП)', NULL, 'INSUR_FSP_O', 'INSUR_FSP_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 302. 'Реестр журналов обработки пакета файлов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 302;

                    UPDATE core_register
                       SET registername             = 'Insur.LogFile',
                           registerdescription      = 'Реестр журналов обработки пакета файлов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LOG_FILE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 302;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (302, 'Insur.LogFile', 'Реестр журналов обработки пакета файлов', NULL, NULL, 'INSUR_LOG_FILE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 850. 'Настройка дашбоарда'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 850;

                    UPDATE core_register
                       SET registername             = 'Dashboards.Dashboard',
                           registerdescription      = 'Настройка дашбоарда',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'DASHBOARDS_DASHBOARD',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 850;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (850, 'Dashboards.Dashboard', 'Настройка дашбоарда', NULL, NULL, 'DASHBOARDS_DASHBOARD', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 851. 'Содержание дашбоарда'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 851;

                    UPDATE core_register
                       SET registername             = 'Dashboards.Panel',
                           registerdescription      = 'Содержание дашбоарда',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'DASHBOARDS_PANEL',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 851;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (851, 'Dashboards.Panel', 'Содержание дашбоарда', NULL, NULL, 'DASHBOARDS_PANEL', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 852. 'Типы панелей'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 852;

                    UPDATE core_register
                       SET registername             = 'Dashboards.PanelTypes',
                           registerdescription      = 'Типы панелей',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'DASHBOARDS_PANEL_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 852;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (852, 'Dashboards.PanelTypes', 'Типы панелей', NULL, NULL, 'DASHBOARDS_PANEL_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 853. 'Пользовательские настройки панелей'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 853;

                    UPDATE core_register
                       SET registername             = 'Dashboards.UserSettings',
                           registerdescription      = 'Пользовательские настройки панелей',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'DASHBOARDS_USER_SETTINGS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 853;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (853, 'Dashboards.UserSettings', 'Пользовательские настройки панелей', NULL, NULL, 'DASHBOARDS_USER_SETTINGS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 132. 'Почтовый адрес'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 132;

                    UPDATE core_register
                       SET registername             = 'FACT_LOCATION',
                           registerdescription      = 'Почтовый адрес',
                           allpri_table             = NULL,
                           object_table             = 'R_FACT_LOCATION_O',
                           quant_table              = 'R_FACT_LOCATION_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 132;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (132, 'FACT_LOCATION', 'Почтовый адрес', NULL, 'R_FACT_LOCATION_O', 'R_FACT_LOCATION_Q', 2, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 600. 'Журнал учёта запросов СПД'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 600;

                    UPDATE core_register
                       SET registername             = 'SPD.RequestRegistration',
                           registerdescription      = 'Журнал учёта запросов СПД',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'SPD_REQUEST_REGISTRATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 600;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (600, 'SPD.RequestRegistration', 'Журнал учёта запросов СПД', NULL, NULL, 'SPD_REQUEST_REGISTRATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 601. 'Журнал запросов СПД CreateFullApplication'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 601;

                    UPDATE core_register
                       SET registername             = 'SPD.CreateFullApplicationLog',
                           registerdescription      = 'Журнал запросов СПД CreateFullApplication',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'SPD_CREATE_FULL_APP_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 601;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (601, 'SPD.CreateFullApplicationLog', 'Журнал запросов СПД CreateFullApplication', NULL, NULL, 'SPD_CREATE_FULL_APP_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 650. 'Согласование документов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 650;

                    UPDATE core_register
                       SET registername             = 'SPD.DocAgreement',
                           registerdescription      = 'Согласование документов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'SPD_DOC_AGREEMENT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 650;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (650, 'SPD.DocAgreement', 'Согласование документов', NULL, NULL, 'SPD_DOC_AGREEMENT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 651. 'Соответствие пользователей СРД и СПД'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 651;

                    UPDATE core_register
                       SET registername             = 'SPD.UserSRD2SPD',
                           registerdescription      = 'Соответствие пользователей СРД и СПД',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'SPD_USERSRD2SPD',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 651;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (651, 'SPD.UserSRD2SPD', 'Соответствие пользователей СРД и СПД', NULL, NULL, 'SPD_USERSRD2SPD', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 301. 'Реестр загрузки файлов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 301;

                    UPDATE core_register
                       SET registername             = 'Insur.InputFile',
                           registerdescription      = 'Реестр загрузки файлов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INPUT_FILE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 301;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (301, 'Insur.InputFile', 'Реестр загрузки файлов', NULL, NULL, 'INSUR_INPUT_FILE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 303. 'Реестр банковских файлов оплат'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 303;

                    UPDATE core_register
                       SET registername             = 'Insur.BankPlat',
                           registerdescription      = 'Реестр банковских файлов оплат',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_BANK_PLAT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 303;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (303, 'Insur.BankPlat', 'Реестр банковских файлов оплат', NULL, NULL, 'INSUR_BANK_PLAT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 304. 'Реестр cводные данные по файлу оплат'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 304;

                    UPDATE core_register
                       SET registername             = 'Insur.SvodBank',
                           registerdescription      = 'Реестр cводные данные по файлу оплат',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_SVOD_BANK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 304;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (304, 'Insur.SvodBank', 'Реестр cводные данные по файлу оплат', NULL, NULL, 'INSUR_SVOD_BANK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 305. 'Реестр начислений'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 305;

                    UPDATE core_register
                       SET registername             = 'Insur.InputNach',
                           registerdescription      = 'Реестр начислений',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INPUT_NACH',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 305;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (305, 'Insur.InputNach', 'Реестр начислений', NULL, NULL, 'INSUR_INPUT_NACH', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 306. 'Реестр зачислений (платежей)'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 306;

                    UPDATE core_register
                       SET registername             = 'Insur.InputPlat',
                           registerdescription      = 'Реестр зачислений (платежей)',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INPUT_PLAT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 306;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (306, 'Insur.InputPlat', 'Реестр зачислений (платежей)', NULL, NULL, 'INSUR_INPUT_PLAT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 307. 'Реестр ведомости учета страховых взносов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 307;

                    UPDATE core_register
                       SET registername             = 'Insur.Balance',
                           registerdescription      = 'Реестр ведомости учета страховых взносов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_BALANCE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 307;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (307, 'Insur.Balance', 'Реестр ведомости учета страховых взносов', NULL, NULL, 'INSUR_BALANCE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 309. 'Реестр страховых полисов и свидетельств'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 309;

                    UPDATE core_register
                       SET registername             = 'Insur.PolicySvd',
                           registerdescription      = 'Реестр страховых полисов и свидетельств',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_POLICY_SVD',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 309;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (309, 'Insur.PolicySvd', 'Реестр страховых полисов и свидетельств', NULL, NULL, 'INSUR_POLICY_SVD', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 310. 'Реестр договоров страхования общего имущества'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 310;

                    UPDATE core_register
                       SET registername             = 'Insur.AllProperty',
                           registerdescription      = 'Реестр договоров страхования общего имущества',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_ALL_PROPERTY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 310;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (310, 'Insur.AllProperty', 'Реестр договоров страхования общего имущества', NULL, NULL, 'INSUR_ALL_PROPERTY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 311. 'Реестр доп. соглашений по договорам общего имущества'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 311;

                    UPDATE core_register
                       SET registername             = 'Insur.DopAllProperty',
                           registerdescription      = 'Реестр доп. соглашений по договорам общего имущества',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DOP_ALL_PROPERTY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 311;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (311, 'Insur.DopAllProperty', 'Реестр доп. соглашений по договорам общего имущества', NULL, NULL, 'INSUR_DOP_ALL_PROPERTY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 312. 'Реестр расчетов параметров объектов общего имущества'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 312;

                    UPDATE core_register
                       SET registername             = 'Insur.ParamCalculation',
                           registerdescription      = 'Реестр расчетов параметров объектов общего имущества',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_PARAM_CALCULATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 312;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (312, 'Insur.ParamCalculation', 'Реестр расчетов параметров объектов общего имущества', NULL, NULL, 'INSUR_PARAM_CALCULATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 313. 'Реестр дел по расчету  суммы ущерба'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 313;

                    UPDATE core_register
                       SET registername             = 'Insur.Damage',
                           registerdescription      = 'Реестр дел по расчету  суммы ущерба',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DAMAGE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 313;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (313, 'Insur.Damage', 'Реестр дел по расчету  суммы ущерба', NULL, NULL, 'INSUR_DAMAGE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 314. 'Реестр страховых выплат'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 314;

                    UPDATE core_register
                       SET registername             = 'Insur.PayTo',
                           registerdescription      = 'Реестр страховых выплат',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_PAY_TO',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 314;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (314, 'Insur.PayTo', 'Реестр страховых выплат', NULL, NULL, 'INSUR_PAY_TO', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 315. 'Реестр сведений об отказах в страховых выплатах'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 315;

                    UPDATE core_register
                       SET registername             = 'Insur.NoPay',
                           registerdescription      = 'Реестр сведений об отказах в страховых выплатах',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_NO_PAY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 315;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (315, 'Insur.NoPay', 'Реестр сведений об отказах в страховых выплатах', NULL, NULL, 'INSUR_NO_PAY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 318. 'Реестр связи здания с адресом'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 318;

                    UPDATE core_register
                       SET registername             = 'Insur.Addrlink',
                           registerdescription      = 'Реестр связи здания с адресом',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_ADDRLINK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 318;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (318, 'Insur.Addrlink', 'Реестр связи здания с адресом', NULL, NULL, 'INSUR_ADDRLINK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 319. 'Реестр адресов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 319;

                    UPDATE core_register
                       SET registername             = 'Insur.Address',
                           registerdescription      = 'Реестр адресов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_ADDRESS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 319;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (319, 'Insur.Address', 'Реестр адресов', NULL, NULL, 'INSUR_ADDRESS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 322. 'Хранилище файлов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 322;

                    UPDATE core_register
                       SET registername             = 'Insur.FileStorage',
                           registerdescription      = 'Хранилище файлов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FILE_STORAGE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 322;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (322, 'Insur.FileStorage', 'Хранилище файлов', NULL, NULL, 'INSUR_FILE_STORAGE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 323. 'Справочник Виды документов-оснований'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 323;

                    UPDATE core_register
                       SET registername             = 'Insur.DocBaseType',
                           registerdescription      = 'Справочник Виды документов-оснований',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DOC_BASE_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 323;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (323, 'Insur.DocBaseType', 'Справочник Виды документов-оснований', NULL, NULL, 'INSUR_DOC_BASE_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 324. 'Методики оценки ущерба'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 324;

                    UPDATE core_register
                       SET registername             = 'Insur.DamageAssessmentMethod',
                           registerdescription      = 'Методики оценки ущерба',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DAMAGE_ASSESSMENT_METHOD',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 324;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (324, 'Insur.DamageAssessmentMethod', 'Методики оценки ущерба', NULL, NULL, 'INSUR_DAMAGE_ASSESSMENT_METHOD', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 325. 'Реестр загружаемых пакетов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 325;

                    UPDATE core_register
                       SET registername             = 'Insur.InputFilePackage',
                           registerdescription      = 'Реестр загружаемых пакетов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INPUT_FILE_PACKAGE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 325;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (325, 'Insur.InputFilePackage', 'Реестр загружаемых пакетов', NULL, NULL, 'INSUR_INPUT_FILE_PACKAGE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 326. 'Реестр связи объекта страхования МКД с объектами БТИ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 326;

                    UPDATE core_register
                       SET registername             = 'Insur.LinkBuildBti  ',
                           registerdescription      = 'Реестр связи объекта страхования МКД с объектами БТИ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LINK_BUILD_BTI',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 326;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (326, 'Insur.LinkBuildBti  ', 'Реестр связи объекта страхования МКД с объектами БТИ', NULL, NULL, 'INSUR_LINK_BUILD_BTI', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 327. 'Реестр связи между объектом страхования ЖП с помещениями в Росреестре'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 327;

                    UPDATE core_register
                       SET registername             = 'Insur.LinkFlatEgrn',
                           registerdescription      = 'Реестр связи между объектом страхования ЖП с помещениями в Росреестре',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LINK_FLAT_EGRN',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 327;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (327, 'Insur.LinkFlatEgrn', 'Реестр связи между объектом страхования ЖП с помещениями в Росреестре', NULL, NULL, 'INSUR_LINK_FLAT_EGRN', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 253. 'Реестр связи между объектом страхования ЖП с помещениями в Росреестре'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 253;

                    UPDATE core_register
                       SET registername             = 'Bti.Floor',
                           registerdescription      = 'Реестр связи между объектом страхования ЖП с помещениями в Росреестре',
                           allpri_table             = NULL,
                           object_table             = 'BTI_FLOOR_O',
                           quant_table              = 'BTI_FLOOR_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 253;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (253, 'Bti.Floor', 'Реестр связи между объектом страхования ЖП с помещениями в Росреестре', NULL, 'BTI_FLOOR_O', 'BTI_FLOOR_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 257. 'Реестр комнат БТИ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 257;

                    UPDATE core_register
                       SET registername             = 'Bti.Rooms',
                           registerdescription      = 'Реестр комнат БТИ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'BTI_ROOMS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 257;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (257, 'Bti.Rooms', 'Реестр комнат БТИ', NULL, NULL, 'BTI_ROOMS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 328. 'Справочник «Страховые организации»'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 328;

                    UPDATE core_register
                       SET registername             = 'Insur.InsuranceOrganization',
                           registerdescription      = 'Справочник «Страховые организации»',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INSURANCE_ORGANIZATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 328;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (328, 'Insur.InsuranceOrganization', 'Справочник «Страховые организации»', NULL, NULL, 'INSUR_INSURANCE_ORGANIZATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 254. 'Реестр помещений БТИ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 254;

                    UPDATE core_register
                       SET registername             = 'Bti.Premase',
                           registerdescription      = 'Реестр помещений БТИ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'BTI_PREMASE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 254;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (254, 'Bti.Premase', 'Реестр помещений БТИ', NULL, NULL, 'BTI_PREMASE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 404. 'Справочник адресов ФИАС (дома)'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 404;

                    UPDATE core_register
                       SET registername             = 'Fias.House',
                           registerdescription      = 'Справочник адресов ФИАС (дома)',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'FIAS_HOUSE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 404;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (404, 'Fias.House', 'Справочник адресов ФИАС (дома)', NULL, NULL, 'FIAS_HOUSE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 332. 'Справочник "Статус квартиры /доли"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 332;

                    UPDATE core_register
                       SET registername             = 'Insur.FlatStatus',
                           registerdescription      = 'Справочник "Статус квартиры /доли"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FLAT_STATUS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 332;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (332, 'Insur.FlatStatus', 'Справочник "Статус квартиры /доли"', NULL, NULL, 'INSUR_FLAT_STATUS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 333. 'Справочник Тип жилого помещения'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 333;

                    UPDATE core_register
                       SET registername             = 'Insur.FlatType',
                           registerdescription      = 'Справочник Тип жилого помещения',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FLAT_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 333;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (333, 'Insur.FlatType', 'Справочник Тип жилого помещения', NULL, NULL, 'INSUR_FLAT_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 330. 'Справочник «Базовый тариф»'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 330;

                    UPDATE core_register
                       SET registername             = 'Insur.BaseTariff',
                           registerdescription      = 'Справочник «Базовый тариф»',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_BASE_TARIFF',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 330;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (330, 'Insur.BaseTariff', 'Справочник «Базовый тариф»', NULL, NULL, 'INSUR_BASE_TARIFF', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 329. 'Справочник «Доля ответственности СК»'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 329;

                    UPDATE core_register
                       SET registername             = 'Insur.PartCompensation',
                           registerdescription      = 'Справочник «Доля ответственности СК»',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_PART_COMPENSATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 329;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (329, 'Insur.PartCompensation', 'Справочник «Доля ответственности СК»', NULL, NULL, 'INSUR_PART_COMPENSATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 403. 'Справочник адресов ФИАС'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 403;

                    UPDATE core_register
                       SET registername             = 'Fias.AddrObj',
                           registerdescription      = 'Справочник адресов ФИАС',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'FIAS_ADDROBJ',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 403;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (403, 'Fias.AddrObj', 'Справочник адресов ФИАС', NULL, NULL, 'FIAS_ADDROBJ', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 334. 'Реестр проектов договоров страхования'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 334;

                    UPDATE core_register
                       SET registername             = 'Insur.AgreementProject',
                           registerdescription      = 'Реестр проектов договоров страхования',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_AGREEMENT_PROJECT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 334;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (334, 'Insur.AgreementProject', 'Реестр проектов договоров страхования', NULL, NULL, 'INSUR_AGREEMENT_PROJECT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 344. 'Справочник «Банки»'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 344;

                    UPDATE core_register
                       SET registername             = 'Insur.Bank',
                           registerdescription      = 'Справочник «Банки»',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_BANK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 344;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (344, 'Insur.Bank', 'Справочник «Банки»', NULL, NULL, 'INSUR_BANK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 350. 'Реестр расчетов ущерба по элементам конструкций'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 350;

                    UPDATE core_register
                       SET registername             = 'Insur.DamageAmount',
                           registerdescription      = 'Реестр расчетов ущерба по элементам конструкций',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DAMAGE_AMOUNT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 350;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (350, 'Insur.DamageAmount', 'Реестр расчетов ущерба по элементам конструкций', NULL, NULL, 'INSUR_DAMAGE_AMOUNT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 340. 'Реестр документов-оснований дел'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 340;

                    UPDATE core_register
                       SET registername             = 'Insur.Documents',
                           registerdescription      = 'Реестр документов-оснований дел',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DOCUMENTS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 340;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (340, 'Insur.Documents', 'Реестр документов-оснований дел', NULL, NULL, 'INSUR_DOCUMENTS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 346. 'Справочник «Страховой тариф»'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 346;

                    UPDATE core_register
                       SET registername             = 'Insur.InsurRate',
                           registerdescription      = 'Справочник «Страховой тариф»',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INSUR_RATE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 346;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (346, 'Insur.InsurRate', 'Справочник «Страховой тариф»', NULL, NULL, 'INSUR_INSUR_RATE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 347. 'Справочник "Тарифы по страхованию общего имущества"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 347;

                    UPDATE core_register
                       SET registername             = 'Insur.CommonPropertyTariff',
                           registerdescription      = 'Справочник "Тарифы по страхованию общего имущества"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_COMMON_PROPERTY_TARIFF',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 347;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (347, 'Insur.CommonPropertyTariff', 'Справочник "Тарифы по страхованию общего имущества"', NULL, NULL, 'INSUR_COMMON_PROPERTY_TARIFF', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 348. 'Справочник «Страховая стоимость ЖП»'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 348;

                    UPDATE core_register
                       SET registername             = 'Insur.LivingPremiseInsurCost',
                           registerdescription      = 'Справочник «Страховая стоимость ЖП»',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LIVING_PREMISE_INSUR_COST',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 348;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (348, 'Insur.LivingPremiseInsurCost', 'Справочник «Страховая стоимость ЖП»', NULL, NULL, 'INSUR_LIVING_PREMISE_INSUR_COST', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 349. 'Справочник "Доля ответственности СК и города"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 349;

                    UPDATE core_register
                       SET registername             = 'Insur.ShareResponsibilityICCity',
                           registerdescription      = 'Справочник "Доля ответственности СК и города"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_SHARE_RESPONSIBILITY_IC_CITY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 349;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (349, 'Insur.ShareResponsibilityICCity', 'Справочник "Доля ответственности СК и города"', NULL, NULL, 'INSUR_SHARE_RESPONSIBILITY_IC_CITY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 809. 'Сохраненные отчеты'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 809;

                    UPDATE core_register
                       SET registername             = 'Fm.Reports.SavedReport',
                           registerdescription      = 'Сохраненные отчеты',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'FM_REPORTS_SAVEDREPORT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 809;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (809, 'Fm.Reports.SavedReport', 'Сохраненные отчеты', NULL, NULL, 'FM_REPORTS_SAVEDREPORT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 331. 'Справочник укрупненных показателей удельного веса восстановительной стоимости конструктивных элементов жилого помещения'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 331;

                    UPDATE core_register
                       SET registername             = 'Insur.IntegrateIndicatorsReplecmentCost',
                           registerdescription      = 'Справочник укрупненных показателей удельного веса восстановительной стоимости конструктивных элементов жилого помещения',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INTEGRATED_INDICATORS_REPL_COST',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 331;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (331, 'Insur.IntegrateIndicatorsReplecmentCost', 'Справочник укрупненных показателей удельного веса восстановительной стоимости конструктивных элементов жилого помещения', NULL, NULL, 'INSUR_INTEGRATED_INDICATORS_REPL_COST', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 351. 'Справочник «Страховой тариф»'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 351;

                    UPDATE core_register
                       SET registername             = 'Insur.Tariff',
                           registerdescription      = 'Справочник «Страховой тариф»',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_TARIFF',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 351;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (351, 'Insur.Tariff', 'Справочник «Страховой тариф»', NULL, NULL, 'INSUR_TARIFF', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 353. 'Справочник "Коэффициент пересчета действительной стоимости"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 353;

                    UPDATE core_register
                       SET registername             = 'Insur.ActualCostRatio',
                           registerdescription      = 'Справочник "Коэффициент пересчета действительной стоимости"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_ACTUAL_COST_RATIO',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 353;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (353, 'Insur.ActualCostRatio', 'Справочник "Коэффициент пересчета действительной стоимости"', NULL, NULL, 'INSUR_ACTUAL_COST_RATIO', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 354. 'Реестр оплат в системе ОПС'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 354;

                    UPDATE core_register
                       SET registername             = 'Insur.ReestrPay',
                           registerdescription      = 'Реестр оплат в системе ОПС',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_REESTR_PAY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 354;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (354, 'Insur.ReestrPay', 'Реестр оплат в системе ОПС', NULL, NULL, 'INSUR_REESTR_PAY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 355. 'Реестр счетов'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 355;

                    UPDATE core_register
                       SET registername             = 'Insur.Invoice',
                           registerdescription      = 'Реестр счетов',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INVOICE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 355;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (355, 'Insur.Invoice', 'Реестр счетов', NULL, NULL, 'INSUR_INVOICE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 251. 'Реестр зданий БТИ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 251;

                    UPDATE core_register
                       SET registername             = 'Bti.BtiBuilding',
                           registerdescription      = 'Реестр зданий БТИ',
                           allpri_table             = NULL,
                           object_table             = 'BTI_BUILDING_O',
                           quant_table              = 'BTI_BUILDING_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 251;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (251, 'Bti.BtiBuilding', 'Реестр зданий БТИ', NULL, 'BTI_BUILDING_O', 'BTI_BUILDING_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 358. 'Реестр "Комментарии"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 358;

                    UPDATE core_register
                       SET registername             = 'Insur.Comment',
                           registerdescription      = 'Реестр "Комментарии"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_COMMENT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 358;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (358, 'Insur.Comment', 'Реестр "Комментарии"', NULL, NULL, 'INSUR_COMMENT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 357. 'Справочник "Причины отказа в выплате ущерба ГБУ"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 357;

                    UPDATE core_register
                       SET registername             = 'Insur.GbuNoPayReason',
                           registerdescription      = 'Справочник "Причины отказа в выплате ущерба ГБУ"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_GBU_NO_PAY_REASON',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 357;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (357, 'Insur.GbuNoPayReason', 'Справочник "Причины отказа в выплате ущерба ГБУ"', NULL, NULL, 'INSUR_GBU_NO_PAY_REASON', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 356. 'Реестр связи справочник причин ущерба и подпричн для ЖП'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 356;

                    UPDATE core_register
                       SET registername             = 'Insur.LinkCausesSubreasonLP',
                           registerdescription      = 'Реестр связи справочник причин ущерба и подпричн для ЖП',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LINK_CAUSES_SUBREASON_LP',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 356;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (356, 'Insur.LinkCausesSubreasonLP', 'Реестр связи справочник причин ущерба и подпричн для ЖП', NULL, NULL, 'INSUR_LINK_CAUSES_SUBREASON_LP', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 360. 'Журнал формирования объектов страхования для Зданий'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 360;

                    UPDATE core_register
                       SET registername             = 'ImportLog.InsurBuildingLog',
                           registerdescription      = 'Журнал формирования объектов страхования для Зданий',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'IMPORT_LOG_INSUR_BUILDING',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 360;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (360, 'ImportLog.InsurBuildingLog', 'Журнал формирования объектов страхования для Зданий', NULL, NULL, 'IMPORT_LOG_INSUR_BUILDING', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 401. 'ehd.register'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 401;

                    UPDATE core_register
                       SET registername             = 'Ehd.Register',
                           registerdescription      = 'ehd.register',
                           allpri_table             = NULL,
                           object_table             = 'EHD_REGISTER_O',
                           quant_table              = 'EHD_REGISTER_Q',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 401;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (401, 'Ehd.Register', 'ehd.register', NULL, 'EHD_REGISTER_O', 'EHD_REGISTER_Q', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 402. 'ehd.location'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 402;

                    UPDATE core_register
                       SET registername             = 'Ehd.Location',
                           registerdescription      = 'ehd.location',
                           allpri_table             = NULL,
                           object_table             = 'EHD_LOCATION_O',
                           quant_table              = 'EHD_LOCATION_Q',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 402;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (402, 'Ehd.Location', 'ehd.location', NULL, 'EHD_LOCATION_O', 'EHD_LOCATION_Q', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 405. 'EHD.EGRP'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 405;

                    UPDATE core_register
                       SET registername             = 'Ehd.Egrp',
                           registerdescription      = 'EHD.EGRP',
                           allpri_table             = NULL,
                           object_table             = 'EHD_EGRP_O',
                           quant_table              = 'EHD_EGRP_Q',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 405;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (405, 'Ehd.Egrp', 'EHD.EGRP', NULL, 'EHD_EGRP_O', 'EHD_EGRP_Q', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 406. 'EHD.RIGHT'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 406;

                    UPDATE core_register
                       SET registername             = 'Ehd.Right',
                           registerdescription      = 'EHD.RIGHT',
                           allpri_table             = NULL,
                           object_table             = 'EHD_RIGHT_O',
                           quant_table              = 'EHD_RIGHT_Q',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 406;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (406, 'Ehd.Right', 'EHD.RIGHT', NULL, 'EHD_RIGHT_O', 'EHD_RIGHT_Q', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 407. 'EHD.OLD_NUMBERS'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 407;

                    UPDATE core_register
                       SET registername             = 'Ehd.OldNumber',
                           registerdescription      = 'EHD.OLD_NUMBERS',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'EHD_OLD_NUMBERS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 407;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (407, 'Ehd.OldNumber', 'EHD.OLD_NUMBERS', NULL, NULL, 'EHD_OLD_NUMBERS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 362. 'Журнал загрузки таблицы ehd.building_parcel'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 362;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdBuildingParcelLog',
                           registerdescription      = 'Журнал загрузки таблицы ehd.building_parcel',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_BUILDING_PARCEL',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 362;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (362, 'ImportLog.EhdBuildingParcelLog', 'Журнал загрузки таблицы ehd.building_parcel', NULL, NULL, 'CIPJS_IMPORT_BUILDING_PARCEL', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 359. 'Журнал идентификации зачислений'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 359;

                    UPDATE core_register
                       SET registername             = 'Insur.FilePlatIdentifyLog',
                           registerdescription      = 'Журнал идентификации зачислений',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FILE_PLAT_IDENTIFY_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 359;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (359, 'Insur.FilePlatIdentifyLog', 'Журнал идентификации зачислений', NULL, NULL, 'INSUR_FILE_PLAT_IDENTIFY_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 345. 'Справочник "Управляющие компании"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 345;

                    UPDATE core_register
                       SET registername             = 'Insur.Subject',
                           registerdescription      = 'Справочник "Управляющие компании"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_SUBJECT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 345;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (345, 'Insur.Subject', 'Справочник "Управляющие компании"', NULL, NULL, 'INSUR_SUBJECT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 363. 'Журнал загрузки таблицы ehd.register'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 363;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdRegisterLog',
                           registerdescription      = 'Журнал загрузки таблицы ehd.register',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_REGISTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 363;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (363, 'ImportLog.EhdRegisterLog', 'Журнал загрузки таблицы ehd.register', NULL, NULL, 'CIPJS_IMPORT_REGISTER', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 364. 'Журнал загрузки таблицы ehd.location'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 364;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdLocationLog',
                           registerdescription      = 'Журнал загрузки таблицы ehd.location',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_LOCATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 364;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (364, 'ImportLog.EhdLocationLog', 'Журнал загрузки таблицы ehd.location', NULL, NULL, 'CIPJS_IMPORT_LOCATION', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 365. 'Журнал загрузки таблицы ehd.egrp'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 365;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdEGRPLog',
                           registerdescription      = 'Журнал загрузки таблицы ehd.egrp',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_EGRP',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 365;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (365, 'ImportLog.EhdEGRPLog', 'Журнал загрузки таблицы ehd.egrp', NULL, NULL, 'CIPJS_IMPORT_EGRP', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 366. 'Журнал загрузки таблицы ehd.right'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 366;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdRightLog',
                           registerdescription      = 'Журнал загрузки таблицы ehd.right',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_RIGHT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 366;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (366, 'ImportLog.EhdRightLog', 'Журнал загрузки таблицы ehd.right', NULL, NULL, 'CIPJS_IMPORT_RIGHT', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 367. 'Журнал загрузки таблицы ehd.old_numbers'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 367;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdOldNumbersLog',
                           registerdescription      = 'Журнал загрузки таблицы ehd.old_numbers',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_OLD_NUMBERS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 367;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (367, 'ImportLog.EhdOldNumbersLog', 'Журнал загрузки таблицы ehd.old_numbers', NULL, NULL, 'CIPJS_IMPORT_OLD_NUMBERS', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 352. 'Реестр регистрации изменения данных'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 352;

                    UPDATE core_register
                       SET registername             = 'Insur.ChangesLog',
                           registerdescription      = 'Реестр регистрации изменения данных',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_CHANGES_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 352;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (352, 'Insur.ChangesLog', 'Реестр регистрации изменения данных', NULL, NULL, 'INSUR_CHANGES_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 258. 'Реестр округов БТИ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 258;

                    UPDATE core_register
                       SET registername             = 'Bti.BtiOkrug',
                           registerdescription      = 'Реестр округов БТИ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'REF_ADDR_OKRUG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 258;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (258, 'Bti.BtiOkrug', 'Реестр округов БТИ', NULL, NULL, 'REF_ADDR_OKRUG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 259. 'Реестр районов БТИ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 259;

                    UPDATE core_register
                       SET registername             = 'Bti.BtiDistrict',
                           registerdescription      = 'Реестр районов БТИ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'REF_ADDR_DISTRICT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 259;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (259, 'Bti.BtiDistrict', 'Реестр районов БТИ', NULL, NULL, 'REF_ADDR_DISTRICT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 368. 'Реестр связей типов здания с этажностью и типом констуркции'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 368;

                    UPDATE core_register
                       SET registername             = 'Insur.TypeBuldingFloorLink',
                           registerdescription      = 'Реестр связей типов здания с этажностью и типом констуркции',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_TYPE_BUILDING_FLOOR_LINK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 368;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (368, 'Insur.TypeBuldingFloorLink', 'Реестр связей типов здания с этажностью и типом констуркции', NULL, NULL, 'INSUR_TYPE_BUILDING_FLOOR_LINK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 361. 'Журнал формирования помещений страхования'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 361;

                    UPDATE core_register
                       SET registername             = 'ImportLog.InsurFlatBuildingLog',
                           registerdescription      = 'Журнал формирования помещений страхования',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'IMPORT_LOG_INSUR_FLAT_B',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 361;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (361, 'ImportLog.InsurFlatBuildingLog', 'Журнал формирования помещений страхования', NULL, NULL, 'IMPORT_LOG_INSUR_FLAT_B', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 370. 'Журнал процесса обработки файлов МФЦ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 370;

                    UPDATE core_register
                       SET registername             = 'Insur.FileProcessLog',
                           registerdescription      = 'Журнал процесса обработки файлов МФЦ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FILE_PROCESS_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 370;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (370, 'Insur.FileProcessLog', 'Журнал процесса обработки файлов МФЦ', NULL, NULL, 'INSUR_FILE_PROCESS_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 369. 'Журнал загрузки данных БТИ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 369;

                    UPDATE core_register
                       SET registername             = 'ImportLog.BtiImportLog',
                           registerdescription      = 'Журнал загрузки данных БТИ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_BTI_DAILY_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 369;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (369, 'ImportLog.BtiImportLog', 'Журнал загрузки данных БТИ', NULL, NULL, 'CIPJS_IMPORT_BTI_DAILY_LOG', 4, NULL, 'REG_OBJECT_SEQ', 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 380. 'АП - Общая статистика'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 380;

                    UPDATE core_register
                       SET registername             = 'Insur.ApCommon',
                           registerdescription      = 'АП - Общая статистика',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'AP_COMMON',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 380;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (380, 'Insur.ApCommon', 'АП - Общая статистика', NULL, NULL, 'AP_COMMON', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 316. 'Реестр объектов страхования МКД'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 316;

                    UPDATE core_register
                       SET registername             = 'Insur.Building',
                           registerdescription      = 'Реестр объектов страхования МКД',
                           allpri_table             = NULL,
                           object_table             = 'INSUR_BUILDING_O',
                           quant_table              = 'INSUR_BUILDING_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 316;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (316, 'Insur.Building', 'Реестр объектов страхования МКД', NULL, 'INSUR_BUILDING_O', 'INSUR_BUILDING_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 317. 'Реестр объектов страхования жилых помещений'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 317;

                    UPDATE core_register
                       SET registername             = 'Insur.Flat',
                           registerdescription      = 'Реестр объектов страхования жилых помещений',
                           allpri_table             = NULL,
                           object_table             = 'INSUR_FLAT_O',
                           quant_table              = 'INSUR_FLAT_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 317;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (317, 'Insur.Flat', 'Реестр объектов страхования жилых помещений', NULL, 'INSUR_FLAT_O', 'INSUR_FLAT_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 400. 'Объекты ЕГРН'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 400;

                    UPDATE core_register
                       SET registername             = 'Ehd.BuildParcel',
                           registerdescription      = 'Объекты ЕГРН',
                           allpri_table             = NULL,
                           object_table             = 'EHD_BUILD_PARCEL_O',
                           quant_table              = 'EHD_BUILD_PARCEL_Q',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 400;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (400, 'Ehd.BuildParcel', 'Объекты ЕГРН', NULL, 'EHD_BUILD_PARCEL_O', 'EHD_BUILD_PARCEL_Q', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 50. 'Реестр адресов БТИ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 50;

                    UPDATE core_register
                       SET registername             = 'Bti.ADDRESS',
                           registerdescription      = 'Реестр адресов БТИ',
                           allpri_table             = NULL,
                           object_table             = 'BTI_ADDRESS_O',
                           quant_table              = 'BTI_ADDRESS_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 50;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (50, 'Bti.ADDRESS', 'Реестр адресов БТИ', NULL, 'BTI_ADDRESS_O', 'BTI_ADDRESS_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 52. 'Реестр связи здания БТИ с адресом'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- Если реестр есть, то обновить
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 52;

                    UPDATE core_register
                       SET registername             = 'Bti.ADDRLINK',
                           registerdescription      = 'Реестр связи здания БТИ с адресом',
                           allpri_table             = NULL,
                           object_table             = 'BTI_ADDRLINK_O',
                           quant_table              = 'BTI_ADDRLINK_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 52;

                    -- Если реестр отсутствует, то добавить
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (52, 'Bti.ADDRLINK', 'Реестр связи здания БТИ с адресом', NULL, 'BTI_ADDRLINK_O', 'BTI_ADDRLINK_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
