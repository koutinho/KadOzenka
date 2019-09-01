
-- I. �������� ��������
--<DO>--
                -- 920. '�������������� ������ �������� ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 920;

                    UPDATE core_register
                       SET registername             = 'Core.Register.List',
                           registerdescription      = '�������������� ������ �������� ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LIST',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 920;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (920, 'Core.Register.List', '�������������� ������ �������� ��������', NULL, NULL, 'CORE_LIST', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 921. '�������������� ��������, �������� � ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 921;

                    UPDATE core_register
                       SET registername             = 'Core.Register.ListObject',
                           registerdescription      = '�������������� ��������, �������� � ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LIST_OBJECT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 921;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (921, 'Core.Register.ListObject', '�������������� ��������, �������� � ������', NULL, NULL, 'CORE_LIST_OBJECT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 924. '��� ������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 924;

                    UPDATE core_register
                       SET registername             = 'Core.Register.LayoutColumnType',
                           registerdescription      = '��� ������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LAYOUT_COLUMN_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 924;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (924, 'Core.Register.LayoutColumnType', '��� ������� ���������', NULL, NULL, 'CORE_LAYOUT_COLUMN_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 925. '���������������� ��������� ������������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 925;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserSettingsRegisterView',
                           registerdescription      = '���������������� ��������� ������������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USERSETTINGSREGVIEW',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 925;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (925, 'Core.SRD.UserSettingsRegisterView', '���������������� ��������� ������������� �������', NULL, NULL, 'CORE_SRD_USERSETTINGSREGVIEW', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 930. '������ ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 930;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Register',
                           registerdescription      = '������ ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 930;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (930, 'Core.Register.Register', '������ ��������', NULL, NULL, 'CORE_REGISTER', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 931. '������ ����������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 931;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Attribute',
                           registerdescription      = '������ ����������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER_ATTRIBUTE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 931;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (931, 'Core.Register.Attribute', '������ ����������� �������', NULL, NULL, 'CORE_REGISTER_ATTRIBUTE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 932. '������ ������ ����� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 932;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Relation',
                           registerdescription      = '������ ������ ����� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER_RELATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 932;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (932, 'Core.Register.Relation', '������ ������ ����� ���������', NULL, NULL, 'CORE_REGISTER_RELATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 933. '���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 933;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Layout',
                           registerdescription      = '���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LAYOUT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_LAYOUT_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 933;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (933, 'Core.Register.Layout', '���������', NULL, NULL, 'CORE_LAYOUT', 4, NULL, 'CORE_LAYOUT_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 935. '����������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 935;

                    UPDATE core_register
                       SET registername             = 'Core.Register.LayoutDetail',
                           registerdescription      = '����������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LAYOUT_DETAILS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_LAYOUT_DET_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 935;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (935, 'Core.Register.LayoutDetail', '����������� ���������', NULL, NULL, 'CORE_LAYOUT_DETAILS', 4, NULL, 'CORE_LAYOUT_DET_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 936. '������� ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 936;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Qry',
                           registerdescription      = '������� ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_QRY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'QRY_QRYID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 936;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (936, 'Core.Register.Qry', '������� ��������', NULL, NULL, 'CORE_QRY', 4, NULL, 'QRY_QRYID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 937. '������� ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 937;

                    UPDATE core_register
                       SET registername             = 'Core.Register.QryFilter',
                           registerdescription      = '������� ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_QRY_FILTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'QRYFILTER_QRYFILTERID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 937;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (937, 'Core.Register.QryFilter', '������� ��������', NULL, NULL, 'CORE_QRY_FILTER', 4, NULL, 'QRYFILTER_QRYFILTERID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 938. '�������� ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 938;

                    UPDATE core_register
                       SET registername             = 'Core.Register.QryOperation',
                           registerdescription      = '�������� ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_QRY_OPERATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'QRYOPERATION_QRYOPERATIONI_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 938;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (938, 'Core.Register.QryOperation', '�������� ��������', NULL, NULL, 'CORE_QRY_OPERATION', 4, NULL, 'QRYOPERATION_QRYOPERATIONI_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 939. '���������� ������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 939;

                    UPDATE core_register
                       SET registername             = 'Core.Register.Lock',
                           registerdescription      = '���������� ������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER_LOCK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 939;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (939, 'Core.Register.Lock', '���������� ������� �������', NULL, NULL, 'CORE_REGISTER_LOCK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 940. '����� �������� ������������� � ��������� ������� (���������) �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 940;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Audit',
                           registerdescription      = '����� �������� ������������� � ��������� ������� (���������) �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_AUDIT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 940;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (940, 'Core.SRD.Audit', '����� �������� ������������� � ��������� ������� (���������) �������', NULL, NULL, 'CORE_SRD_AUDIT', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 941. '������������� � ����������� ������������ �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 941;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Department',
                           registerdescription      = '������������� � ����������� ������������ �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_DEPARTMENT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 941;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (941, 'Core.SRD.Department', '������������� � ����������� ������������ �������', NULL, NULL, 'CORE_SRD_DEPARTMENT', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 942. '������� ������� (���������) �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 942;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Function',
                           registerdescription      = '������� ������� (���������) �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_FUNCTION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 942;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (942, 'Core.SRD.Function', '������� ������� (���������) �������', NULL, NULL, 'CORE_SRD_FUNCTION', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 945. '���� � �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 945;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Role',
                           registerdescription      = '���� � �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 945;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (945, 'Core.SRD.Role', '���� � �������', NULL, NULL, 'CORE_SRD_ROLE', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 946. '������� ���� (������ LOCROLE_LOCFUNCTION)'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 946;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RoleFunction',
                           registerdescription      = '������� ���� (������ LOCROLE_LOCFUNCTION)',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE_FUNCTION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 946;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (946, 'Core.SRD.RoleFunction', '������� ���� (������ LOCROLE_LOCFUNCTION)', NULL, NULL, 'CORE_SRD_ROLE_FUNCTION', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 947. '����� ������� ���� � �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 947;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RoleRegister',
                           registerdescription      = '����� ������� ���� � �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE_REGISTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 947;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (947, 'Core.SRD.RoleRegister', '����� ������� ���� � �������', NULL, NULL, 'CORE_SRD_ROLE_REGISTER', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 948. '����� ������� ���� � ��������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 948;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RoleAttr',
                           registerdescription      = '����� ������� ���� � ��������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE_ATTR',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 948;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (948, 'Core.SRD.RoleAttr', '����� ������� ���� � ��������� �������', NULL, NULL, 'CORE_SRD_ROLE_ATTR', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 949. '��������� ������ ������������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 949;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.Session',
                           registerdescription      = '��������� ������ ������������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_SESSION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 949;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (949, 'Core.SRD.Session', '��������� ������ ������������', NULL, NULL, 'CORE_SRD_SESSION', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 950. '������������ �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 950;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.User',
                           registerdescription      = '������������ �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 950;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (950, 'Core.SRD.User', '������������ �������', NULL, NULL, 'CORE_SRD_USER', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 951. '���������������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 951;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserSettings',
                           registerdescription      = '���������������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USERSETTINGS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 951;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (951, 'Core.SRD.UserSettings', '���������������� ���������', NULL, NULL, 'CORE_SRD_USERSETTINGS', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 952. '���� ������������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 952;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserRole',
                           registerdescription      = '���� ������������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USER_ROLE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 952;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (952, 'Core.SRD.UserRole', '���� ������������', NULL, NULL, 'CORE_SRD_USER_ROLE', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 953. '������������ �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 953;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserAlt',
                           registerdescription      = '������������ �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 953;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (953, 'Core.SRD.UserAlt', '������������ �������', NULL, NULL, 'CORE_SRD_USER', 4, NULL, 'REG_OBJECT_SEQ', 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 954. '���������������� ��������� ����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 954;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.UserSettingsLayout',
                           registerdescription      = '���������������� ��������� ����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_USERSETTINGSLAYOUT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 954;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (954, 'Core.SRD.UserSettingsLayout', '���������������� ��������� ����������', NULL, NULL, 'CORE_SRD_USERSETTINGSLAYOUT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 955. '������������� ���� ������� �� ������ ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 955;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RoleFilter',
                           registerdescription      = '������������� ���� ������� �� ������ ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_ROLE_FILTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 955;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (955, 'Core.SRD.RoleFilter', '������������� ���� ������� �� ������ ��������', NULL, NULL, 'CORE_SRD_ROLE_FILTER', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 956. '�������� ������ �� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 956;

                    UPDATE core_register
                       SET registername             = 'Core.Register.LayoutExport',
                           registerdescription      = '�������� ������ �� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LAYOUT_EXPORT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 956;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (956, 'Core.Register.LayoutExport', '�������� ������ �� ���������', NULL, NULL, 'CORE_LAYOUT_EXPORT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 957. '��������� ������� � ������ ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 957;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.RegisterCategory',
                           registerdescription      = '��������� ������� � ������ ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_REGISTER_CATEGORY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 957;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (957, 'Core.SRD.RegisterCategory', '��������� ������� � ������ ��������', NULL, NULL, 'CORE_SRD_REGISTER_CATEGORY', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 958. '������ ������� � ���������� ������� ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 958;

                    UPDATE core_register
                       SET registername             = 'Core.SRD.FunctionRegisterCategory',
                           registerdescription      = '������ ������� � ���������� ������� ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_SRD_FUNCTION_REG_CAT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_SRD_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 958;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (958, 'Core.SRD.FunctionRegisterCategory', '������ ������� � ���������� ������� ��������', NULL, NULL, 'CORE_SRD_FUNCTION_REG_CAT', 4, NULL, 'CORE_SRD_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 960. '������ (���) ���������������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 960;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Template',
                           registerdescription      = '������ (���) ���������������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TEMPLATE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 960;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (960, 'Core.TD.Template', '������ (���) ���������������� ���������', NULL, NULL, 'CORE_TD_TEMPLATE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 961. '������ ������� ���������������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 961;

                    UPDATE core_register
                       SET registername             = 'Core.TD.TemplateVersion',
                           registerdescription      = '������ ������� ���������������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TEMPLATE_VERSION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 961;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (961, 'Core.TD.TemplateVersion', '������ ������� ���������������� ���������', NULL, NULL, 'CORE_TD_TEMPLATE_VERSION', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 962. '���� �������� ���������� ���������������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 962;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Status',
                           registerdescription      = '���� �������� ���������� ���������������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_STATUS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 962;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (962, 'Core.TD.Status', '���� �������� ���������� ���������������� ���������', NULL, NULL, 'CORE_TD_STATUS', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 963. '���������� ��������������� ����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 963;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Instance',
                           registerdescription      = '���������� ��������������� ����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_INSTANCE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 963;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (963, 'Core.TD.Instance', '���������� ��������������� ����������', NULL, NULL, 'CORE_TD_INSTANCE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 964. '����� ��������� � ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 964;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Changeset',
                           registerdescription      = '����� ��������� � ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_CHANGESET',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 964;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (964, 'Core.TD.Changeset', '����� ��������� � ��������', NULL, NULL, 'CORE_TD_CHANGESET', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 965. '��������� � �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 965;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Change',
                           registerdescription      = '��������� � �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_CHANGE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 965;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (965, 'Core.TD.Change', '��������� � �������', NULL, NULL, 'CORE_TD_CHANGE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 966. '���� ���������� �������� � ����������� ���������������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 966;

                    UPDATE core_register
                       SET registername             = 'Core.TD.AuditAction',
                           registerdescription      = '���� ���������� �������� � ����������� ���������������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_AUDIT_ACTION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 966;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (966, 'Core.TD.AuditAction', '���� ���������� �������� � ����������� ���������������� ���������', NULL, NULL, 'CORE_TD_AUDIT_ACTION', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 967. '����� �������� � ����������� ���������������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 967;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Audit',
                           registerdescription      = '����� �������� � ����������� ���������������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_AUDIT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 967;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (967, 'Core.TD.Audit', '����� �������� � ����������� ���������������� ���������', NULL, NULL, 'CORE_TD_AUDIT', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 968. '������ �������� '
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 968;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Tree',
                           registerdescription      = '������ �������� ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TREE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 968;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (968, 'Core.TD.Tree', '������ �������� ', NULL, NULL, 'CORE_TD_TREE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 969. '����������� ������ ���������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 969;

                    UPDATE core_register
                       SET registername             = 'Core.TD.Attachment',
                           registerdescription      = '����������� ������ ���������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_ATTACHMENTS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 969;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (969, 'Core.TD.Attachment', '����������� ������ ���������� ���������', NULL, NULL, 'CORE_TD_ATTACHMENTS', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 970. '������ �� ����������� ��������, �� �������� ������ ��������� ���������������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 970;

                    UPDATE core_register
                       SET registername             = 'Core.TD.TP',
                           registerdescription      = '������ �� ����������� ��������, �� �������� ������ ��������� ���������������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TP',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 970;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (970, 'Core.TD.TP', '������ �� ����������� ��������, �� �������� ������ ��������� ���������������� ���������', NULL, NULL, 'CORE_TD_TP', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 971. '��� ������� ���������������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 971;

                    UPDATE core_register
                       SET registername             = 'Core.TD.TemplateType',
                           registerdescription      = '��� ������� ���������������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_TD_TEMPLATE_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 971;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (971, 'Core.TD.TemplateType', '��� ������� ���������������� ���������', NULL, NULL, 'CORE_TD_TEMPLATE_TYPE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 975. '������� ������ ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 975;

                    UPDATE core_register
                       SET registername             = 'Core.LongProcess.Queue',
                           registerdescription      = '������� ������ ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LONG_PROCESS_QUEUE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 975;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (975, 'Core.LongProcess.Queue', '������� ������ ���������', NULL, NULL, 'CORE_LONG_PROCESS_QUEUE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 976. '���� ������ ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 976;

                    UPDATE core_register
                       SET registername             = 'Core.LongProcess.ProcessType',
                           registerdescription      = '���� ������ ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LONG_PROCESS_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 976;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (976, 'Core.LongProcess.ProcessType', '���� ������ ���������', NULL, NULL, 'CORE_LONG_PROCESS_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 320. '���������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 320;

                    UPDATE core_register
                       SET registername             = 'Insur.Okrug',
                           registerdescription      = '���������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_OKRUG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 320;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (320, 'Insur.Okrug', '���������� �������', NULL, NULL, 'INSUR_OKRUG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 977. '������ ��������� ������ ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 977;

                    UPDATE core_register
                       SET registername             = 'Core.LongProcess.Log',
                           registerdescription      = '������ ��������� ������ ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_LONG_PROCESS_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 977;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (977, 'Core.LongProcess.Log', '������ ��������� ������ ���������', NULL, NULL, 'CORE_LONG_PROCESS_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 978. '����� ������������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 978;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Configparam',
                           registerdescription      = '����� ������������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_CONFIGPARAM',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 978;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (978, 'Core.Shared.Configparam', '����� ������������', NULL, NULL, 'CORE_CONFIGPARAM', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 982. '����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 982;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Reference',
                           registerdescription      = '����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REFERENCE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REFERENCE_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 982;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (982, 'Core.Shared.Reference', '����������', NULL, NULL, 'CORE_REFERENCE', 4, NULL, 'REFERENCE_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 983. '���������� ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 983;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.ReferenceItem',
                           registerdescription      = '���������� ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REFERENCE_ITEM',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REFITEM_ITEMID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 983;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (983, 'Core.Shared.ReferenceItem', '���������� ��������', NULL, NULL, 'CORE_REFERENCE_ITEM', 4, NULL, 'REFITEM_ITEMID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 984. '����� ������������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 984;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.ReferenceRelation',
                           registerdescription      = '����� ������������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REFERENCE_RELATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REFRELATION_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 984;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (984, 'Core.Shared.ReferenceRelation', '����� ������������', NULL, NULL, 'CORE_REFERENCE_RELATION', 4, NULL, 'REFRELATION_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 985. '����� ���������� ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 985;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.ReferenceTree',
                           registerdescription      = '����� ���������� ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REFERENCE_TREE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'TREEHELPER_ID_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 985;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (985, 'Core.Shared.ReferenceTree', '����� ���������� ��������', NULL, NULL, 'CORE_REFERENCE_TREE', 4, NULL, 'TREEHELPER_ID_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 986. '�����'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 986;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Attachment',
                           registerdescription      = '�����',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_ATTACHMENT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 986;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (986, 'Core.Shared.Attachment', '�����', NULL, NULL, 'CORE_ATTACHMENT', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 987. '����� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 987;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.AttachmentFile',
                           registerdescription      = '����� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_ATTACHMENT_FILE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 987;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (987, 'Core.Shared.AttachmentFile', '����� ������', NULL, NULL, 'CORE_ATTACHMENT_FILE', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 988. '����� ������ � ������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 988;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.AttachmentObject',
                           registerdescription      = '����� ������ � ������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_ATTACHMENT_OBJECT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'SEQ_CORE_TD',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 988;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (988, 'Core.Shared.AttachmentObject', '����� ������ � ������� �������', NULL, NULL, 'CORE_ATTACHMENT_OBJECT', 4, NULL, 'SEQ_CORE_TD', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 989. '������ ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 989;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.ErrorLog',
                           registerdescription      = '������ ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_ERROR_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_ERROR_LOG_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 989;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (989, 'Core.Shared.ErrorLog', '������ ������', NULL, NULL, 'CORE_ERROR_LOG', 4, NULL, 'CORE_ERROR_LOG_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 991. '����������� ��������� ������������� '
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 991;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.RegisterState',
                           registerdescription      = '����������� ��������� ������������� ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER_STATE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 991;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (991, 'Core.Shared.RegisterState', '����������� ��������� ������������� ', NULL, NULL, 'CORE_REGISTER_STATE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 992. '������ ���������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 992;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Diagnostics',
                           registerdescription      = '������ ���������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_DIAGNOSTICS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'CORE_DIAGNOSTICS_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 992;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (992, 'Core.Shared.Diagnostics', '������ ���������� ���������', NULL, NULL, 'CORE_DIAGNOSTICS', 4, NULL, 'CORE_DIAGNOSTICS_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 993. '������������ ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 993;

                    UPDATE core_register
                       SET registername             = 'Core.Register.RegisterParent',
                           registerdescription      = '������������ ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGISTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 993;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (993, 'Core.Register.RegisterParent', '������������ ������', NULL, NULL, 'CORE_REGISTER', 4, NULL, 'REG_OBJECT_SEQ', 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 994. '����������� ��������������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 994;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.RegNomRepository',
                           registerdescription      = '����������� ��������������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGNOM_REPOSITORY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 994;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (994, 'Core.Shared.RegNomRepository', '����������� ��������������� �������', NULL, NULL, 'CORE_REGNOM_REPOSITORY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 995. '������������������ ��������������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 995;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.RegNomSequences',
                           registerdescription      = '������������������ ��������������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_REGNOM_SEQUENCES',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 995;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (995, 'Core.Shared.RegNomSequences', '������������������ ��������������� �������', NULL, NULL, 'CORE_REGNOM_SEQUENCES', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 996. '��������� ����� ���������� ����'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 996;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.CacheUpdates',
                           registerdescription      = '��������� ����� ���������� ����',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_CACHE_UPDATES',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 996;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (996, 'Core.Shared.CacheUpdates', '��������� ����� ���������� ����', NULL, NULL, 'CORE_CACHE_UPDATES', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 997. '������ ���������� ��������� ��'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 997;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.UpdateStructure',
                           registerdescription      = '������ ���������� ��������� ��',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_UPDSTRU_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 997;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (997, 'Core.Shared.UpdateStructure', '������ ���������� ��������� ��', NULL, NULL, 'CORE_UPDSTRU_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 998. '�������� � ����������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 998;

                    UPDATE core_register
                       SET registername             = 'Core.Shared.Holiday',
                           registerdescription      = '�������� � ����������� ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CORE_HOLIDAYS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 998;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (998, 'Core.Shared.Holiday', '�������� � ����������� ���', NULL, NULL, 'CORE_HOLIDAYS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 321. '���������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 321;

                    UPDATE core_register
                       SET registername             = 'Insur.District',
                           registerdescription      = '���������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DISTRICT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 321;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (321, 'Insur.District', '���������� �������', NULL, NULL, 'INSUR_DISTRICT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 44. '����������� ����'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 44;

                    UPDATE core_register
                       SET registername             = 'EMPLOYEE',
                           registerdescription      = '����������� ����',
                           allpri_table             = NULL,
                           object_table             = 'R_EMPL_O',
                           quant_table              = 'R_EMPL_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 44;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (44, 'EMPLOYEE', '����������� ����', NULL, 'R_EMPL_O', 'R_EMPL_Q', 2, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 308. '������ ���������� ������ ������������ (���)'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 308;

                    UPDATE core_register
                       SET registername             = 'Insur.Fsp',
                           registerdescription      = '������ ���������� ������ ������������ (���)',
                           allpri_table             = NULL,
                           object_table             = 'INSUR_FSP_O',
                           quant_table              = 'INSUR_FSP_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 308;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (308, 'Insur.Fsp', '������ ���������� ������ ������������ (���)', NULL, 'INSUR_FSP_O', 'INSUR_FSP_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 302. '������ �������� ��������� ������ ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 302;

                    UPDATE core_register
                       SET registername             = 'Insur.LogFile',
                           registerdescription      = '������ �������� ��������� ������ ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LOG_FILE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 302;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (302, 'Insur.LogFile', '������ �������� ��������� ������ ������', NULL, NULL, 'INSUR_LOG_FILE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 850. '��������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 850;

                    UPDATE core_register
                       SET registername             = 'Dashboards.Dashboard',
                           registerdescription      = '��������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'DASHBOARDS_DASHBOARD',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 850;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (850, 'Dashboards.Dashboard', '��������� ���������', NULL, NULL, 'DASHBOARDS_DASHBOARD', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 851. '���������� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 851;

                    UPDATE core_register
                       SET registername             = 'Dashboards.Panel',
                           registerdescription      = '���������� ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'DASHBOARDS_PANEL',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 851;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (851, 'Dashboards.Panel', '���������� ���������', NULL, NULL, 'DASHBOARDS_PANEL', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 852. '���� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 852;

                    UPDATE core_register
                       SET registername             = 'Dashboards.PanelTypes',
                           registerdescription      = '���� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'DASHBOARDS_PANEL_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 852;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (852, 'Dashboards.PanelTypes', '���� �������', NULL, NULL, 'DASHBOARDS_PANEL_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 853. '���������������� ��������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 853;

                    UPDATE core_register
                       SET registername             = 'Dashboards.UserSettings',
                           registerdescription      = '���������������� ��������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'DASHBOARDS_USER_SETTINGS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 853;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (853, 'Dashboards.UserSettings', '���������������� ��������� �������', NULL, NULL, 'DASHBOARDS_USER_SETTINGS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 132. '�������� �����'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 132;

                    UPDATE core_register
                       SET registername             = 'FACT_LOCATION',
                           registerdescription      = '�������� �����',
                           allpri_table             = NULL,
                           object_table             = 'R_FACT_LOCATION_O',
                           quant_table              = 'R_FACT_LOCATION_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 132;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (132, 'FACT_LOCATION', '�������� �����', NULL, 'R_FACT_LOCATION_O', 'R_FACT_LOCATION_Q', 2, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 600. '������ ����� �������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 600;

                    UPDATE core_register
                       SET registername             = 'SPD.RequestRegistration',
                           registerdescription      = '������ ����� �������� ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'SPD_REQUEST_REGISTRATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 600;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (600, 'SPD.RequestRegistration', '������ ����� �������� ���', NULL, NULL, 'SPD_REQUEST_REGISTRATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 601. '������ �������� ��� CreateFullApplication'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 601;

                    UPDATE core_register
                       SET registername             = 'SPD.CreateFullApplicationLog',
                           registerdescription      = '������ �������� ��� CreateFullApplication',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'SPD_CREATE_FULL_APP_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 601;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (601, 'SPD.CreateFullApplicationLog', '������ �������� ��� CreateFullApplication', NULL, NULL, 'SPD_CREATE_FULL_APP_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 650. '������������ ����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 650;

                    UPDATE core_register
                       SET registername             = 'SPD.DocAgreement',
                           registerdescription      = '������������ ����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'SPD_DOC_AGREEMENT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 650;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (650, 'SPD.DocAgreement', '������������ ����������', NULL, NULL, 'SPD_DOC_AGREEMENT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 651. '������������ ������������� ��� � ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 651;

                    UPDATE core_register
                       SET registername             = 'SPD.UserSRD2SPD',
                           registerdescription      = '������������ ������������� ��� � ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'SPD_USERSRD2SPD',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 651;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (651, 'SPD.UserSRD2SPD', '������������ ������������� ��� � ���', NULL, NULL, 'SPD_USERSRD2SPD', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 301. '������ �������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 301;

                    UPDATE core_register
                       SET registername             = 'Insur.InputFile',
                           registerdescription      = '������ �������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INPUT_FILE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 301;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (301, 'Insur.InputFile', '������ �������� ������', NULL, NULL, 'INSUR_INPUT_FILE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 303. '������ ���������� ������ �����'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 303;

                    UPDATE core_register
                       SET registername             = 'Insur.BankPlat',
                           registerdescription      = '������ ���������� ������ �����',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_BANK_PLAT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 303;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (303, 'Insur.BankPlat', '������ ���������� ������ �����', NULL, NULL, 'INSUR_BANK_PLAT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 304. '������ c������ ������ �� ����� �����'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 304;

                    UPDATE core_register
                       SET registername             = 'Insur.SvodBank',
                           registerdescription      = '������ c������ ������ �� ����� �����',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_SVOD_BANK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 304;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (304, 'Insur.SvodBank', '������ c������ ������ �� ����� �����', NULL, NULL, 'INSUR_SVOD_BANK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 305. '������ ����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 305;

                    UPDATE core_register
                       SET registername             = 'Insur.InputNach',
                           registerdescription      = '������ ����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INPUT_NACH',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 305;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (305, 'Insur.InputNach', '������ ����������', NULL, NULL, 'INSUR_INPUT_NACH', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 306. '������ ���������� (��������)'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 306;

                    UPDATE core_register
                       SET registername             = 'Insur.InputPlat',
                           registerdescription      = '������ ���������� (��������)',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INPUT_PLAT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 306;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (306, 'Insur.InputPlat', '������ ���������� (��������)', NULL, NULL, 'INSUR_INPUT_PLAT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 307. '������ ��������� ����� ��������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 307;

                    UPDATE core_register
                       SET registername             = 'Insur.Balance',
                           registerdescription      = '������ ��������� ����� ��������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_BALANCE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 307;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (307, 'Insur.Balance', '������ ��������� ����� ��������� �������', NULL, NULL, 'INSUR_BALANCE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 309. '������ ��������� ������� � ������������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 309;

                    UPDATE core_register
                       SET registername             = 'Insur.PolicySvd',
                           registerdescription      = '������ ��������� ������� � ������������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_POLICY_SVD',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 309;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (309, 'Insur.PolicySvd', '������ ��������� ������� � ������������', NULL, NULL, 'INSUR_POLICY_SVD', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 310. '������ ��������� ����������� ������ ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 310;

                    UPDATE core_register
                       SET registername             = 'Insur.AllProperty',
                           registerdescription      = '������ ��������� ����������� ������ ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_ALL_PROPERTY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 310;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (310, 'Insur.AllProperty', '������ ��������� ����������� ������ ���������', NULL, NULL, 'INSUR_ALL_PROPERTY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 311. '������ ���. ���������� �� ��������� ������ ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 311;

                    UPDATE core_register
                       SET registername             = 'Insur.DopAllProperty',
                           registerdescription      = '������ ���. ���������� �� ��������� ������ ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DOP_ALL_PROPERTY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 311;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (311, 'Insur.DopAllProperty', '������ ���. ���������� �� ��������� ������ ���������', NULL, NULL, 'INSUR_DOP_ALL_PROPERTY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 312. '������ �������� ���������� �������� ������ ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 312;

                    UPDATE core_register
                       SET registername             = 'Insur.ParamCalculation',
                           registerdescription      = '������ �������� ���������� �������� ������ ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_PARAM_CALCULATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 312;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (312, 'Insur.ParamCalculation', '������ �������� ���������� �������� ������ ���������', NULL, NULL, 'INSUR_PARAM_CALCULATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 313. '������ ��� �� �������  ����� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 313;

                    UPDATE core_register
                       SET registername             = 'Insur.Damage',
                           registerdescription      = '������ ��� �� �������  ����� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DAMAGE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 313;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (313, 'Insur.Damage', '������ ��� �� �������  ����� ������', NULL, NULL, 'INSUR_DAMAGE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 314. '������ ��������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 314;

                    UPDATE core_register
                       SET registername             = 'Insur.PayTo',
                           registerdescription      = '������ ��������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_PAY_TO',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 314;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (314, 'Insur.PayTo', '������ ��������� ������', NULL, NULL, 'INSUR_PAY_TO', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 315. '������ �������� �� ������� � ��������� ��������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 315;

                    UPDATE core_register
                       SET registername             = 'Insur.NoPay',
                           registerdescription      = '������ �������� �� ������� � ��������� ��������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_NO_PAY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 315;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (315, 'Insur.NoPay', '������ �������� �� ������� � ��������� ��������', NULL, NULL, 'INSUR_NO_PAY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 318. '������ ����� ������ � �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 318;

                    UPDATE core_register
                       SET registername             = 'Insur.Addrlink',
                           registerdescription      = '������ ����� ������ � �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_ADDRLINK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 318;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (318, 'Insur.Addrlink', '������ ����� ������ � �������', NULL, NULL, 'INSUR_ADDRLINK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 319. '������ �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 319;

                    UPDATE core_register
                       SET registername             = 'Insur.Address',
                           registerdescription      = '������ �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_ADDRESS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 319;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (319, 'Insur.Address', '������ �������', NULL, NULL, 'INSUR_ADDRESS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 322. '��������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 322;

                    UPDATE core_register
                       SET registername             = 'Insur.FileStorage',
                           registerdescription      = '��������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FILE_STORAGE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 322;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (322, 'Insur.FileStorage', '��������� ������', NULL, NULL, 'INSUR_FILE_STORAGE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 323. '���������� ���� ����������-���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 323;

                    UPDATE core_register
                       SET registername             = 'Insur.DocBaseType',
                           registerdescription      = '���������� ���� ����������-���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DOC_BASE_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 323;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (323, 'Insur.DocBaseType', '���������� ���� ����������-���������', NULL, NULL, 'INSUR_DOC_BASE_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 324. '�������� ������ ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 324;

                    UPDATE core_register
                       SET registername             = 'Insur.DamageAssessmentMethod',
                           registerdescription      = '�������� ������ ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DAMAGE_ASSESSMENT_METHOD',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 324;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (324, 'Insur.DamageAssessmentMethod', '�������� ������ ������', NULL, NULL, 'INSUR_DAMAGE_ASSESSMENT_METHOD', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 325. '������ ����������� �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 325;

                    UPDATE core_register
                       SET registername             = 'Insur.InputFilePackage',
                           registerdescription      = '������ ����������� �������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INPUT_FILE_PACKAGE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 325;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (325, 'Insur.InputFilePackage', '������ ����������� �������', NULL, NULL, 'INSUR_INPUT_FILE_PACKAGE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 326. '������ ����� ������� ����������� ��� � ��������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 326;

                    UPDATE core_register
                       SET registername             = 'Insur.LinkBuildBti  ',
                           registerdescription      = '������ ����� ������� ����������� ��� � ��������� ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LINK_BUILD_BTI',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 326;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (326, 'Insur.LinkBuildBti  ', '������ ����� ������� ����������� ��� � ��������� ���', NULL, NULL, 'INSUR_LINK_BUILD_BTI', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 327. '������ ����� ����� �������� ����������� �� � ����������� � ����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 327;

                    UPDATE core_register
                       SET registername             = 'Insur.LinkFlatEgrn',
                           registerdescription      = '������ ����� ����� �������� ����������� �� � ����������� � ����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LINK_FLAT_EGRN',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 327;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (327, 'Insur.LinkFlatEgrn', '������ ����� ����� �������� ����������� �� � ����������� � ����������', NULL, NULL, 'INSUR_LINK_FLAT_EGRN', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 253. '������ ����� ����� �������� ����������� �� � ����������� � ����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 253;

                    UPDATE core_register
                       SET registername             = 'Bti.Floor',
                           registerdescription      = '������ ����� ����� �������� ����������� �� � ����������� � ����������',
                           allpri_table             = NULL,
                           object_table             = 'BTI_FLOOR_O',
                           quant_table              = 'BTI_FLOOR_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 253;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (253, 'Bti.Floor', '������ ����� ����� �������� ����������� �� � ����������� � ����������', NULL, 'BTI_FLOOR_O', 'BTI_FLOOR_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 257. '������ ������ ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 257;

                    UPDATE core_register
                       SET registername             = 'Bti.Rooms',
                           registerdescription      = '������ ������ ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'BTI_ROOMS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 257;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (257, 'Bti.Rooms', '������ ������ ���', NULL, NULL, 'BTI_ROOMS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 328. '���������� ���������� �����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 328;

                    UPDATE core_register
                       SET registername             = 'Insur.InsuranceOrganization',
                           registerdescription      = '���������� ���������� �����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INSURANCE_ORGANIZATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 328;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (328, 'Insur.InsuranceOrganization', '���������� ���������� �����������', NULL, NULL, 'INSUR_INSURANCE_ORGANIZATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 254. '������ ��������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 254;

                    UPDATE core_register
                       SET registername             = 'Bti.Premase',
                           registerdescription      = '������ ��������� ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'BTI_PREMASE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 254;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (254, 'Bti.Premase', '������ ��������� ���', NULL, NULL, 'BTI_PREMASE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 404. '���������� ������� ���� (����)'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 404;

                    UPDATE core_register
                       SET registername             = 'Fias.House',
                           registerdescription      = '���������� ������� ���� (����)',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'FIAS_HOUSE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 404;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (404, 'Fias.House', '���������� ������� ���� (����)', NULL, NULL, 'FIAS_HOUSE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 332. '���������� "������ �������� /����"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 332;

                    UPDATE core_register
                       SET registername             = 'Insur.FlatStatus',
                           registerdescription      = '���������� "������ �������� /����"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FLAT_STATUS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 332;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (332, 'Insur.FlatStatus', '���������� "������ �������� /����"', NULL, NULL, 'INSUR_FLAT_STATUS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 333. '���������� ��� ������ ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 333;

                    UPDATE core_register
                       SET registername             = 'Insur.FlatType',
                           registerdescription      = '���������� ��� ������ ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FLAT_TYPE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 333;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (333, 'Insur.FlatType', '���������� ��� ������ ���������', NULL, NULL, 'INSUR_FLAT_TYPE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 330. '���������� �������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 330;

                    UPDATE core_register
                       SET registername             = 'Insur.BaseTariff',
                           registerdescription      = '���������� �������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_BASE_TARIFF',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 330;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (330, 'Insur.BaseTariff', '���������� �������� ������', NULL, NULL, 'INSUR_BASE_TARIFF', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 329. '���������� ����� ��������������� �ʻ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 329;

                    UPDATE core_register
                       SET registername             = 'Insur.PartCompensation',
                           registerdescription      = '���������� ����� ��������������� �ʻ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_PART_COMPENSATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 329;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (329, 'Insur.PartCompensation', '���������� ����� ��������������� �ʻ', NULL, NULL, 'INSUR_PART_COMPENSATION', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 403. '���������� ������� ����'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 403;

                    UPDATE core_register
                       SET registername             = 'Fias.AddrObj',
                           registerdescription      = '���������� ������� ����',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'FIAS_ADDROBJ',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 403;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (403, 'Fias.AddrObj', '���������� ������� ����', NULL, NULL, 'FIAS_ADDROBJ', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 334. '������ �������� ��������� �����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 334;

                    UPDATE core_register
                       SET registername             = 'Insur.AgreementProject',
                           registerdescription      = '������ �������� ��������� �����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_AGREEMENT_PROJECT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 334;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (334, 'Insur.AgreementProject', '������ �������� ��������� �����������', NULL, NULL, 'INSUR_AGREEMENT_PROJECT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 344. '���������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 344;

                    UPDATE core_register
                       SET registername             = 'Insur.Bank',
                           registerdescription      = '���������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_BANK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 344;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (344, 'Insur.Bank', '���������� ������', NULL, NULL, 'INSUR_BANK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 350. '������ �������� ������ �� ��������� �����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 350;

                    UPDATE core_register
                       SET registername             = 'Insur.DamageAmount',
                           registerdescription      = '������ �������� ������ �� ��������� �����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DAMAGE_AMOUNT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 350;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (350, 'Insur.DamageAmount', '������ �������� ������ �� ��������� �����������', NULL, NULL, 'INSUR_DAMAGE_AMOUNT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 340. '������ ����������-��������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 340;

                    UPDATE core_register
                       SET registername             = 'Insur.Documents',
                           registerdescription      = '������ ����������-��������� ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_DOCUMENTS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 340;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (340, 'Insur.Documents', '������ ����������-��������� ���', NULL, NULL, 'INSUR_DOCUMENTS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 346. '���������� ���������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 346;

                    UPDATE core_register
                       SET registername             = 'Insur.InsurRate',
                           registerdescription      = '���������� ���������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INSUR_RATE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 346;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (346, 'Insur.InsurRate', '���������� ���������� ������', NULL, NULL, 'INSUR_INSUR_RATE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 347. '���������� "������ �� ����������� ������ ���������"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 347;

                    UPDATE core_register
                       SET registername             = 'Insur.CommonPropertyTariff',
                           registerdescription      = '���������� "������ �� ����������� ������ ���������"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_COMMON_PROPERTY_TARIFF',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 347;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (347, 'Insur.CommonPropertyTariff', '���������� "������ �� ����������� ������ ���������"', NULL, NULL, 'INSUR_COMMON_PROPERTY_TARIFF', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 348. '���������� ���������� ��������� �ϻ'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 348;

                    UPDATE core_register
                       SET registername             = 'Insur.LivingPremiseInsurCost',
                           registerdescription      = '���������� ���������� ��������� �ϻ',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LIVING_PREMISE_INSUR_COST',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 348;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (348, 'Insur.LivingPremiseInsurCost', '���������� ���������� ��������� �ϻ', NULL, NULL, 'INSUR_LIVING_PREMISE_INSUR_COST', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 349. '���������� "���� ��������������� �� � ������"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 349;

                    UPDATE core_register
                       SET registername             = 'Insur.ShareResponsibilityICCity',
                           registerdescription      = '���������� "���� ��������������� �� � ������"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_SHARE_RESPONSIBILITY_IC_CITY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 349;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (349, 'Insur.ShareResponsibilityICCity', '���������� "���� ��������������� �� � ������"', NULL, NULL, 'INSUR_SHARE_RESPONSIBILITY_IC_CITY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 809. '����������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 809;

                    UPDATE core_register
                       SET registername             = 'Fm.Reports.SavedReport',
                           registerdescription      = '����������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'FM_REPORTS_SAVEDREPORT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 809;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (809, 'Fm.Reports.SavedReport', '����������� ������', NULL, NULL, 'FM_REPORTS_SAVEDREPORT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 331. '���������� ����������� ����������� ��������� ���� ����������������� ��������� �������������� ��������� ������ ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 331;

                    UPDATE core_register
                       SET registername             = 'Insur.IntegrateIndicatorsReplecmentCost',
                           registerdescription      = '���������� ����������� ����������� ��������� ���� ����������������� ��������� �������������� ��������� ������ ���������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INTEGRATED_INDICATORS_REPL_COST',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 331;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (331, 'Insur.IntegrateIndicatorsReplecmentCost', '���������� ����������� ����������� ��������� ���� ����������������� ��������� �������������� ��������� ������ ���������', NULL, NULL, 'INSUR_INTEGRATED_INDICATORS_REPL_COST', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 351. '���������� ���������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 351;

                    UPDATE core_register
                       SET registername             = 'Insur.Tariff',
                           registerdescription      = '���������� ���������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_TARIFF',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 351;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (351, 'Insur.Tariff', '���������� ���������� ������', NULL, NULL, 'INSUR_TARIFF', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 353. '���������� "����������� ��������� �������������� ���������"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 353;

                    UPDATE core_register
                       SET registername             = 'Insur.ActualCostRatio',
                           registerdescription      = '���������� "����������� ��������� �������������� ���������"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_ACTUAL_COST_RATIO',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 353;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (353, 'Insur.ActualCostRatio', '���������� "����������� ��������� �������������� ���������"', NULL, NULL, 'INSUR_ACTUAL_COST_RATIO', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 354. '������ ����� � ������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 354;

                    UPDATE core_register
                       SET registername             = 'Insur.ReestrPay',
                           registerdescription      = '������ ����� � ������� ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_REESTR_PAY',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 354;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (354, 'Insur.ReestrPay', '������ ����� � ������� ���', NULL, NULL, 'INSUR_REESTR_PAY', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 355. '������ ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 355;

                    UPDATE core_register
                       SET registername             = 'Insur.Invoice',
                           registerdescription      = '������ ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_INVOICE',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 355;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (355, 'Insur.Invoice', '������ ������', NULL, NULL, 'INSUR_INVOICE', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 251. '������ ������ ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 251;

                    UPDATE core_register
                       SET registername             = 'Bti.BtiBuilding',
                           registerdescription      = '������ ������ ���',
                           allpri_table             = NULL,
                           object_table             = 'BTI_BUILDING_O',
                           quant_table              = 'BTI_BUILDING_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 251;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (251, 'Bti.BtiBuilding', '������ ������ ���', NULL, 'BTI_BUILDING_O', 'BTI_BUILDING_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 358. '������ "�����������"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 358;

                    UPDATE core_register
                       SET registername             = 'Insur.Comment',
                           registerdescription      = '������ "�����������"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_COMMENT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 358;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (358, 'Insur.Comment', '������ "�����������"', NULL, NULL, 'INSUR_COMMENT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 357. '���������� "������� ������ � ������� ������ ���"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 357;

                    UPDATE core_register
                       SET registername             = 'Insur.GbuNoPayReason',
                           registerdescription      = '���������� "������� ������ � ������� ������ ���"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_GBU_NO_PAY_REASON',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 357;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (357, 'Insur.GbuNoPayReason', '���������� "������� ������ � ������� ������ ���"', NULL, NULL, 'INSUR_GBU_NO_PAY_REASON', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 356. '������ ����� ���������� ������ ������ � �������� ��� ��'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 356;

                    UPDATE core_register
                       SET registername             = 'Insur.LinkCausesSubreasonLP',
                           registerdescription      = '������ ����� ���������� ������ ������ � �������� ��� ��',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_LINK_CAUSES_SUBREASON_LP',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 356;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (356, 'Insur.LinkCausesSubreasonLP', '������ ����� ���������� ������ ������ � �������� ��� ��', NULL, NULL, 'INSUR_LINK_CAUSES_SUBREASON_LP', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 360. '������ ������������ �������� ����������� ��� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 360;

                    UPDATE core_register
                       SET registername             = 'ImportLog.InsurBuildingLog',
                           registerdescription      = '������ ������������ �������� ����������� ��� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'IMPORT_LOG_INSUR_BUILDING',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 360;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (360, 'ImportLog.InsurBuildingLog', '������ ������������ �������� ����������� ��� ������', NULL, NULL, 'IMPORT_LOG_INSUR_BUILDING', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 401. 'ehd.register'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
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

                    -- ���� ������ �����������, �� ��������
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
                    -- ���� ������ ����, �� ��������
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

                    -- ���� ������ �����������, �� ��������
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
                    -- ���� ������ ����, �� ��������
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

                    -- ���� ������ �����������, �� ��������
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
                    -- ���� ������ ����, �� ��������
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

                    -- ���� ������ �����������, �� ��������
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
                    -- ���� ������ ����, �� ��������
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

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (407, 'Ehd.OldNumber', 'EHD.OLD_NUMBERS', NULL, NULL, 'EHD_OLD_NUMBERS', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 362. '������ �������� ������� ehd.building_parcel'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 362;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdBuildingParcelLog',
                           registerdescription      = '������ �������� ������� ehd.building_parcel',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_BUILDING_PARCEL',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 362;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (362, 'ImportLog.EhdBuildingParcelLog', '������ �������� ������� ehd.building_parcel', NULL, NULL, 'CIPJS_IMPORT_BUILDING_PARCEL', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 359. '������ ������������� ����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 359;

                    UPDATE core_register
                       SET registername             = 'Insur.FilePlatIdentifyLog',
                           registerdescription      = '������ ������������� ����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FILE_PLAT_IDENTIFY_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 359;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (359, 'Insur.FilePlatIdentifyLog', '������ ������������� ����������', NULL, NULL, 'INSUR_FILE_PLAT_IDENTIFY_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 345. '���������� "����������� ��������"'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 345;

                    UPDATE core_register
                       SET registername             = 'Insur.Subject',
                           registerdescription      = '���������� "����������� ��������"',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_SUBJECT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 345;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (345, 'Insur.Subject', '���������� "����������� ��������"', NULL, NULL, 'INSUR_SUBJECT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 363. '������ �������� ������� ehd.register'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 363;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdRegisterLog',
                           registerdescription      = '������ �������� ������� ehd.register',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_REGISTER',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 363;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (363, 'ImportLog.EhdRegisterLog', '������ �������� ������� ehd.register', NULL, NULL, 'CIPJS_IMPORT_REGISTER', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 364. '������ �������� ������� ehd.location'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 364;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdLocationLog',
                           registerdescription      = '������ �������� ������� ehd.location',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_LOCATION',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 364;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (364, 'ImportLog.EhdLocationLog', '������ �������� ������� ehd.location', NULL, NULL, 'CIPJS_IMPORT_LOCATION', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 365. '������ �������� ������� ehd.egrp'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 365;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdEGRPLog',
                           registerdescription      = '������ �������� ������� ehd.egrp',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_EGRP',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 365;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (365, 'ImportLog.EhdEGRPLog', '������ �������� ������� ehd.egrp', NULL, NULL, 'CIPJS_IMPORT_EGRP', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 366. '������ �������� ������� ehd.right'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 366;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdRightLog',
                           registerdescription      = '������ �������� ������� ehd.right',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_RIGHT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 366;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (366, 'ImportLog.EhdRightLog', '������ �������� ������� ehd.right', NULL, NULL, 'CIPJS_IMPORT_RIGHT', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 367. '������ �������� ������� ehd.old_numbers'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 367;

                    UPDATE core_register
                       SET registername             = 'ImportLog.EhdOldNumbersLog',
                           registerdescription      = '������ �������� ������� ehd.old_numbers',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_OLD_NUMBERS',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 367;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (367, 'ImportLog.EhdOldNumbersLog', '������ �������� ������� ehd.old_numbers', NULL, NULL, 'CIPJS_IMPORT_OLD_NUMBERS', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 352. '������ ����������� ��������� ������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 352;

                    UPDATE core_register
                       SET registername             = 'Insur.ChangesLog',
                           registerdescription      = '������ ����������� ��������� ������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_CHANGES_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 352;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (352, 'Insur.ChangesLog', '������ ����������� ��������� ������', NULL, NULL, 'INSUR_CHANGES_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 258. '������ ������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 258;

                    UPDATE core_register
                       SET registername             = 'Bti.BtiOkrug',
                           registerdescription      = '������ ������� ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'REF_ADDR_OKRUG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 258;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (258, 'Bti.BtiOkrug', '������ ������� ���', NULL, NULL, 'REF_ADDR_OKRUG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 259. '������ ������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 259;

                    UPDATE core_register
                       SET registername             = 'Bti.BtiDistrict',
                           registerdescription      = '������ ������� ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'REF_ADDR_DISTRICT',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 259;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (259, 'Bti.BtiDistrict', '������ ������� ���', NULL, NULL, 'REF_ADDR_DISTRICT', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 368. '������ ������ ����� ������ � ���������� � ����� �����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 368;

                    UPDATE core_register
                       SET registername             = 'Insur.TypeBuldingFloorLink',
                           registerdescription      = '������ ������ ����� ������ � ���������� � ����� �����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_TYPE_BUILDING_FLOOR_LINK',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 368;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (368, 'Insur.TypeBuldingFloorLink', '������ ������ ����� ������ � ���������� � ����� �����������', NULL, NULL, 'INSUR_TYPE_BUILDING_FLOOR_LINK', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 361. '������ ������������ ��������� �����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 361;

                    UPDATE core_register
                       SET registername             = 'ImportLog.InsurFlatBuildingLog',
                           registerdescription      = '������ ������������ ��������� �����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'IMPORT_LOG_INSUR_FLAT_B',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 361;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (361, 'ImportLog.InsurFlatBuildingLog', '������ ������������ ��������� �����������', NULL, NULL, 'IMPORT_LOG_INSUR_FLAT_B', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 370. '������ �������� ��������� ������ ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 370;

                    UPDATE core_register
                       SET registername             = 'Insur.FileProcessLog',
                           registerdescription      = '������ �������� ��������� ������ ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'INSUR_FILE_PROCESS_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 370;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (370, 'Insur.FileProcessLog', '������ �������� ��������� ������ ���', NULL, NULL, 'INSUR_FILE_PROCESS_LOG', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 369. '������ �������� ������ ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 369;

                    UPDATE core_register
                       SET registername             = 'ImportLog.BtiImportLog',
                           registerdescription      = '������ �������� ������ ���',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'CIPJS_IMPORT_BTI_DAILY_LOG',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 369;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (369, 'ImportLog.BtiImportLog', '������ �������� ������ ���', NULL, NULL, 'CIPJS_IMPORT_BTI_DAILY_LOG', 4, NULL, 'REG_OBJECT_SEQ', 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 380. '�� - ����� ����������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 380;

                    UPDATE core_register
                       SET registername             = 'Insur.ApCommon',
                           registerdescription      = '�� - ����� ����������',
                           allpri_table             = NULL,
                           object_table             = NULL,
                           quant_table              = 'AP_COMMON',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = NULL,
                           IS_VIRTUAL               = 1,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 380;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (380, 'Insur.ApCommon', '�� - ����� ����������', NULL, NULL, 'AP_COMMON', 4, NULL, NULL, 1, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 316. '������ �������� ����������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 316;

                    UPDATE core_register
                       SET registername             = 'Insur.Building',
                           registerdescription      = '������ �������� ����������� ���',
                           allpri_table             = NULL,
                           object_table             = 'INSUR_BUILDING_O',
                           quant_table              = 'INSUR_BUILDING_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 316;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (316, 'Insur.Building', '������ �������� ����������� ���', NULL, 'INSUR_BUILDING_O', 'INSUR_BUILDING_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 317. '������ �������� ����������� ����� ���������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 317;

                    UPDATE core_register
                       SET registername             = 'Insur.Flat',
                           registerdescription      = '������ �������� ����������� ����� ���������',
                           allpri_table             = NULL,
                           object_table             = 'INSUR_FLAT_O',
                           quant_table              = 'INSUR_FLAT_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 317;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (317, 'Insur.Flat', '������ �������� ����������� ����� ���������', NULL, 'INSUR_FLAT_O', 'INSUR_FLAT_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 400. '������� ����'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 400;

                    UPDATE core_register
                       SET registername             = 'Ehd.BuildParcel',
                           registerdescription      = '������� ����',
                           allpri_table             = NULL,
                           object_table             = 'EHD_BUILD_PARCEL_O',
                           quant_table              = 'EHD_BUILD_PARCEL_Q',
                           storage_type             = 4,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 400;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (400, 'Ehd.BuildParcel', '������� ����', NULL, 'EHD_BUILD_PARCEL_O', 'EHD_BUILD_PARCEL_Q', 4, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 50. '������ ������� ���'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 50;

                    UPDATE core_register
                       SET registername             = 'Bti.ADDRESS',
                           registerdescription      = '������ ������� ���',
                           allpri_table             = NULL,
                           object_table             = 'BTI_ADDRESS_O',
                           quant_table              = 'BTI_ADDRESS_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 50;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (50, 'Bti.ADDRESS', '������ ������� ���', NULL, 'BTI_ADDRESS_O', 'BTI_ADDRESS_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
--<DO>--
                -- 52. '������ ����� ������ ��� � �������'
                DO $$
                DECLARE
	                r_register core_register%ROWTYPE;
                BEGIN
                    -- ���� ������ ����, �� ��������
                    SELECT * INTO r_register FROM core_register AS t WHERE t.registerid = 52;

                    UPDATE core_register
                       SET registername             = 'Bti.ADDRLINK',
                           registerdescription      = '������ ����� ������ ��� � �������',
                           allpri_table             = NULL,
                           object_table             = 'BTI_ADDRLINK_O',
                           quant_table              = 'BTI_ADDRLINK_Q',
                           storage_type             = 2,
                           TRACK_CHANGES_COLUMN     = NULL,
                           OBJECT_SEQUENCE          = 'REG_OBJECT_SEQ',
                           IS_VIRTUAL               = 0,
                           CONTAINS_QUANT_IN_FUTURE = 0
                       WHERE registerid = 52;

                    -- ���� ������ �����������, �� ��������
                    if(not found)then
	                    INSERT INTO core_register(registerid, registername, registerdescription, allpri_table, object_table, quant_table, storage_type, TRACK_CHANGES_COLUMN, OBJECT_SEQUENCE, IS_VIRTUAL, CONTAINS_QUANT_IN_FUTURE)
                        VALUES (52, 'Bti.ADDRLINK', '������ ����� ������ ��� � �������', NULL, 'BTI_ADDRLINK_O', 'BTI_ADDRLINK_Q', 2, NULL, 'REG_OBJECT_SEQ', 0, 0);
                    end if;

                END $$;
            
