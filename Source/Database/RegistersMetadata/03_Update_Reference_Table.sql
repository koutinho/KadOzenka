
-- III. �������� ������������
--<DO>--
-- 2. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 2;

    update core_reference
       set description           = '��� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 2;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (2,
             '��� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 4. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 4;

    update core_reference
       set description           = '�������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'SourceType'
     where referenceid           = 4;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (4,
             '�������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'SourceType');
     end if;
end $$;
--<DO>--
-- 5. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 5;

    update core_reference
       set description           = '������� ��',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutors, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 5;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (5,
             '������� ��',
             4,
             1,
             'RefLibImpl.AddressExecutors, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 6. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 6;

    update core_reference
       set description           = '���. �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutors, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 6;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (6,
             '���. �����',
             4,
             1,
             'RefLibImpl.AddressExecutors, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 7. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 7;

    update core_reference
       set description           = '�����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutors, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 7;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (7,
             '�����',
             4,
             1,
             'RefLibImpl.AddressExecutors, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 9. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 9;

    update core_reference
       set description           = '����� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutors, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 9;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (9,
             '����� ������',
             4,
             1,
             'RefLibImpl.AddressExecutors, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 10. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 10;

    update core_reference
       set description           = '���. �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutors, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 10;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (10,
             '���. �����',
             4,
             1,
             'RefLibImpl.AddressExecutors, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 11. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 11;

    update core_reference
       set description           = '�����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutors, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 11;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (11,
             '�����',
             4,
             1,
             'RefLibImpl.AddressExecutors, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 15. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 15;

    update core_reference
       set description           = '��� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 15;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (15,
             '��� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 16. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 16;

    update core_reference
       set description           = '��� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 16;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (16,
             '��� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 17. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 17;

    update core_reference
       set description           = '��� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 150,
           controlwidth          = 200,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 17;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (17,
             '��� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             150,
             200,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 18. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 18;

    update core_reference
       set description           = '��� ����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 100,
           controlwidth          = 200,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 18;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (18,
             '��� ����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             100,
             200,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 20. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 20;

    update core_reference
       set description           = '����� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 150,
           controlwidth          = 200,
           DEFAULTVALUE          = NULL,
           NAME                  = 'BuildingClass'
     where referenceid           = 20;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (20,
             '����� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             150,
             200,
             NULL,
             'BuildingClass');
     end if;
end $$;
--<DO>--
-- 21. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 21;

    update core_reference
       set description           = '����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 450,
           controlwidth          = 350,
           DEFAULTVALUE          = NULL,
           NAME                  = 'Purpose'
     where referenceid           = 21;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (21,
             '����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             450,
             350,
             NULL,
             'Purpose');
     end if;
end $$;
--<DO>--
-- 22. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 22;

    update core_reference
       set description           = '�������� ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 250,
           controlwidth          = 200,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 22;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (22,
             '�������� ����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             250,
             200,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 23. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 23;

    update core_reference
       set description           = '��������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 23;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (23,
             '��������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 24. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 24;

    update core_reference
       set description           = '������ ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 150,
           controlwidth          = 250,
           DEFAULTVALUE          = NULL,
           NAME                  = 'AddressStatus'
     where referenceid           = 24;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (24,
             '������ ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             150,
             250,
             NULL,
             'AddressStatus');
     end if;
end $$;
--<DO>--
-- 25. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 25;

    update core_reference
       set description           = '��� ��������� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 200,
           controlwidth          = 200,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 25;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (25,
             '��� ��������� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             200,
             200,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 26. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 26;

    update core_reference
       set description           = '���������� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 300,
           controlwidth          = 250,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 26;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (26,
             '���������� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             300,
             250,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 28. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 28;

    update core_reference
       set description           = '��� �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 28;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (28,
             '��� �����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 30. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 30;

    update core_reference
       set description           = '����� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 30;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (30,
             '����� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 31. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 31;

    update core_reference
       set description           = '��� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceTreeExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 1,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'PremisesTypes'
     where referenceid           = 31;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (31,
             '��� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceTreeExecutor',
             0,
             0,
             1,
             0,
             0,
             0,
             NULL,
             'PremisesTypes');
     end if;
end $$;
--<DO>--
-- 33. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 33;

    update core_reference
       set description           = '������ ����������� ���',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 33;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (33,
             '������ ����������� ���',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 36. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 36;

    update core_reference
       set description           = '���������� �������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 36;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (36,
             '���������� �������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 37. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 37;

    update core_reference
       set description           = '����������� ���������� �������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 37;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (37,
             '����������� ���������� �������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 38. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 38;

    update core_reference
       set description           = '��� ������� �������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 38;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (38,
             '��� ������� �������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 39. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 39;

    update core_reference
       set description           = '��� ������� �������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 39;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (39,
             '��� ������� �������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 40. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 40;

    update core_reference
       set description           = '���������� �����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 40;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (40,
             '���������� �����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 43. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 43;

    update core_reference
       set description           = '��� ��������� �������� ������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 43;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (43,
             '��� ��������� �������� ������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 60. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 60;

    update core_reference
       set description           = '���������� ������ (���������)',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'BuildingPurposeRosreestr'
     where referenceid           = 60;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (60,
             '���������� ������ (���������)',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'BuildingPurposeRosreestr');
     end if;
end $$;
--<DO>--
-- 63. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 63;

    update core_reference
       set description           = '��� ���������� �� ����������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 63;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (63,
             '��� ���������� �� ����������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 65. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 65;

    update core_reference
       set description           = '��������� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'StructureStatus'
     where referenceid           = 65;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (65,
             '��������� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'StructureStatus');
     end if;
end $$;
--<DO>--
-- 68. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 68;

    update core_reference
       set description           = '����� �������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 68;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (68,
             '����� �������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 69. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 69;

    update core_reference
       set description           = '���� �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 69;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (69,
             '���� �����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 70. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 70;

    update core_reference
       set description           = '����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.SRDDepartmentsExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 70;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (70,
             '����',
             4,
             1,
             'Core.RefLib.Executors.SRDDepartmentsExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 85. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 85;

    update core_reference
       set description           = '������� ��������� ������� ��������� ���',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 85;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (85,
             '������� ��������� ������� ��������� ���',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 86. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 86;

    update core_reference
       set description           = '��������� ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 86;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (86,
             '��������� ����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 87. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 87;

    update core_reference
       set description           = '�������� ������������� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 87;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (87,
             '�������� ������������� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 89. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 89;

    update core_reference
       set description           = '������� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 89;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (89,
             '������� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 90. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 90;

    update core_reference
       set description           = '��� ������� ������������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 90;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (90,
             '��� ������� ������������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 91. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 91;

    update core_reference
       set description           = '�������� ����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 91;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (91,
             '�������� ����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 92. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 92;

    update core_reference
       set description           = '�������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 92;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (92,
             '�������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 93. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 93;

    update core_reference
       set description           = '��������� ��������� �������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 93;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (93,
             '��������� ��������� �������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 114. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 114;

    update core_reference
       set description           = '�������������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.SRDDepartmentsExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 114;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (114,
             '�������������',
             4,
             1,
             'Core.RefLib.Executors.SRDDepartmentsExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 301. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 301;

    update core_reference
       set description           = '�������� ����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'SourceInput'
     where referenceid           = 301;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (301,
             '�������� ����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'SourceInput');
     end if;
end $$;
--<DO>--
-- 303. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 303;

    update core_reference
       set description           = '������ �������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'CalculationStatus'
     where referenceid           = 303;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (303,
             '������ �������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'CalculationStatus');
     end if;
end $$;
--<DO>--
-- 304. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 304;

    update core_reference
       set description           = '���� ��������������� ��',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'PartCompensationType'
     where referenceid           = 304;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (304,
             '���� ��������������� ��',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'PartCompensationType');
     end if;
end $$;
--<DO>--
-- 305. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 305;

    update core_reference
       set description           = '����� ����������� �������������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FormAssociationOwners'
     where referenceid           = 305;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (305,
             '����� ����������� �������������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FormAssociationOwners');
     end if;
end $$;
--<DO>--
-- 306. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 306;

    update core_reference
       set description           = '������� ��������� �������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'SignInstallmentPayment'
     where referenceid           = 306;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (306,
             '������� ��������� �������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'SignInstallmentPayment');
     end if;
end $$;
--<DO>--
-- 307. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 307;

    update core_reference
       set description           = '�������� ����������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'AddressSource'
     where referenceid           = 307;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (307,
             '�������� ����������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'AddressSource');
     end if;
end $$;
--<DO>--
-- 333. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 333;

    update core_reference
       set description           = '��� ���',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FSPType'
     where referenceid           = 333;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (333,
             '��� ���',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FSPType');
     end if;
end $$;
--<DO>--
-- 401. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 401;

    update core_reference
       set description           = '������� �� - ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutorsFias, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FiasSubjectRf'
     where referenceid           = 401;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (401,
             '������� �� - ����',
             4,
             1,
             'RefLibImpl.AddressExecutorsFias, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FiasSubjectRf');
     end if;
end $$;
--<DO>--
-- 402. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 402;

    update core_reference
       set description           = '���. ����� - ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutorsFias, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FiasAdministrativeRegion'
     where referenceid           = 402;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (402,
             '���. ����� - ����',
             4,
             1,
             'RefLibImpl.AddressExecutorsFias, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FiasAdministrativeRegion');
     end if;
end $$;
--<DO>--
-- 403. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 403;

    update core_reference
       set description           = '����� - ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutorsFias, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FiasDistrict'
     where referenceid           = 403;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (403,
             '����� - ����',
             4,
             1,
             'RefLibImpl.AddressExecutorsFias, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FiasDistrict');
     end if;
end $$;
--<DO>--
-- 404. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 404;

    update core_reference
       set description           = '�����(�/�) - ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutorsFias, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FiasCity'
     where referenceid           = 404;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (404,
             '�����(�/�) - ����',
             4,
             1,
             'RefLibImpl.AddressExecutorsFias, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FiasCity');
     end if;
end $$;
--<DO>--
-- 405. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 405;

    update core_reference
       set description           = '����� ������ - ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutorsFias, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FiasCityDistrict'
     where referenceid           = 405;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (405,
             '����� ������ - ����',
             4,
             1,
             'RefLibImpl.AddressExecutorsFias, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FiasCityDistrict');
     end if;
end $$;
--<DO>--
-- 406. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 406;

    update core_reference
       set description           = '���. ����� - ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutorsFias, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FiasLocality'
     where referenceid           = 406;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (406,
             '���. ����� - ����',
             4,
             1,
             'RefLibImpl.AddressExecutorsFias, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FiasLocality');
     end if;
end $$;
--<DO>--
-- 407. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 407;

    update core_reference
       set description           = '����� - ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutorsFias, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FiasStreet'
     where referenceid           = 407;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (407,
             '����� - ����',
             4,
             1,
             'RefLibImpl.AddressExecutorsFias, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FiasStreet');
     end if;
end $$;
--<DO>--
-- 408. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 408;

    update core_reference
       set description           = '��� - ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.AddressExecutorsFias, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FiasHouse'
     where referenceid           = 408;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (408,
             '��� - ����',
             4,
             1,
             'RefLibImpl.AddressExecutorsFias, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FiasHouse');
     end if;
end $$;
--<DO>--
-- 10079. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 10079;

    update core_reference
       set description           = '�� / ���',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = NULL,
           usetreehelper         = NULL,
           istable               = NULL,
           controlheight         = 100,
           controlwidth          = 200,
           DEFAULTVALUE          = NULL,
           NAME                  = 'YesNo'
     where referenceid           = 10079;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (10079,
             '�� / ���',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             NULL,
             NULL,
             NULL,
             100,
             200,
             NULL,
             'YesNo');
     end if;
end $$;
--<DO>--
-- 10167. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 10167;

    update core_reference
       set description           = '��� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = NULL
     where referenceid           = 10167;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (10167,
             '��� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             NULL);
     end if;
end $$;
--<DO>--
-- 12029. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12029;

    update core_reference
       set description           = '��� ����� ����������� �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'VidPravaRasshirennoyChasti'
     where referenceid           = 12029;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12029,
             '��� ����� ����������� �����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'VidPravaRasshirennoyChasti');
     end if;
end $$;
--<DO>--
-- 12093. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12093;

    update core_reference
       set description           = '������ �������������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'StatusIdentifikacii'
     where referenceid           = 12093;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12093,
             '������ �������������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'StatusIdentifikacii');
     end if;
end $$;
--<DO>--
-- 12119. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12119;

    update core_reference
       set description           = '������ ��������� �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'UFKFileProcessingStatus'
     where referenceid           = 12119;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12119,
             '������ ��������� �����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'UFKFileProcessingStatus');
     end if;
end $$;
--<DO>--
-- 12120. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12120;

    update core_reference
       set description           = '��� ���� �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'TypeFile'
     where referenceid           = 12120;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12120,
             '��� ���� �����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'TypeFile');
     end if;
end $$;
--<DO>--
-- 12121. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12121;

    update core_reference
       set description           = '�������� ������ ��� �����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'InsuranceSourceType'
     where referenceid           = 12121;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12121,
             '�������� ������ ��� �����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'InsuranceSourceType');
     end if;
end $$;
--<DO>--
-- 12122. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12122;

    update core_reference
       set description           = '��� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ContractType'
     where referenceid           = 12122;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12122,
             '��� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ContractType');
     end if;
end $$;
--<DO>--
-- 12123. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12123;

    update core_reference
       set description           = '��� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'DocumentType'
     where referenceid           = 12123;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12123,
             '��� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'DocumentType');
     end if;
end $$;
--<DO>--
-- 12124. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12124;

    update core_reference
       set description           = '���� ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'StoveType'
     where referenceid           = 12124;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12124,
             '���� ����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'StoveType');
     end if;
end $$;
--<DO>--
-- 12125. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12125;

    update core_reference
       set description           = '������� ������ ��� ��',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'CausesOfDamageGP'
     where referenceid           = 12125;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12125,
             '������� ������ ��� ��',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'CausesOfDamageGP');
     end if;
end $$;
--<DO>--
-- 12126. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12126;

    update core_reference
       set description           = '�������� �����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ElementsOfConstructions'
     where referenceid           = 12126;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12126,
             '�������� �����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ElementsOfConstructions');
     end if;
end $$;
--<DO>--
-- 12127. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12127;

    update core_reference
       set description           = '�������� ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FloorMaterial'
     where referenceid           = 12127;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12127,
             '�������� ����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FloorMaterial');
     end if;
end $$;
--<DO>--
-- 12128. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12128;

    update core_reference
       set description           = '������� ������ � ��������� ������� �� �������� ����������� ������ ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ReasonsRefusalInsurancePaymentLivingPremise'
     where referenceid           = 12128;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12128,
             '������� ������ � ��������� ������� �� �������� ����������� ������ ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ReasonsRefusalInsurancePaymentLivingPremise');
     end if;
end $$;
--<DO>--
-- 12129. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12129;

    update core_reference
       set description           = '������� ������ � ��������� ������� �� �������� ����������� ������ ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ReasonsRefusalInsurancePaymentCommonProperty'
     where referenceid           = 12129;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12129,
             '������� ������ � ��������� ������� �� �������� ����������� ������ ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ReasonsRefusalInsurancePaymentCommonProperty');
     end if;
end $$;
--<DO>--
-- 12130. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12130;

    update core_reference
       set description           = '������� ���������� ������� � ��������� ������� �� �������� ����������� ������ ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ReasonsAbsenceDecision'
     where referenceid           = 12130;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12130,
             '������� ���������� ������� � ��������� ������� �� �������� ����������� ������ ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ReasonsAbsenceDecision');
     end if;
end $$;
--<DO>--
-- 12131. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12131;

    update core_reference
       set description           = '��� ������ ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'LivingPremiseType'
     where referenceid           = 12131;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12131,
             '��� ������ ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'LivingPremiseType');
     end if;
end $$;
--<DO>--
-- 12132. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12132;

    update core_reference
       set description           = '���� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'BuildingType'
     where referenceid           = 12132;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12132,
             '���� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'BuildingType');
     end if;
end $$;
--<DO>--
-- 12133. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12133;

    update core_reference
       set description           = '��� �������������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'TypeProperty'
     where referenceid           = 12133;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12133,
             '��� �������������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'TypeProperty');
     end if;
end $$;
--<DO>--
-- 12134. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12134;

    update core_reference
       set description           = '���������� ������ �� ��',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'SubReasonCausesOfDamage'
     where referenceid           = 12134;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12134,
             '���������� ������ �� ��',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'SubReasonCausesOfDamage');
     end if;
end $$;
--<DO>--
-- 12135. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12135;

    update core_reference
       set description           = '��������� ��������� ������ �� ��',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'RefinementSubReasonCOD'
     where referenceid           = 12135;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12135,
             '��������� ��������� ������ �� ��',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'RefinementSubReasonCOD');
     end if;
end $$;
--<DO>--
-- 12136. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12136;

    update core_reference
       set description           = '������� ������ ��� ��',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'CausesOfDamageOI'
     where referenceid           = 12136;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12136,
             '������� ������ ��� ��',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'CausesOfDamageOI');
     end if;
end $$;
--<DO>--
-- 12141. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12141;

    update core_reference
       set description           = '����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'RefLibImpl.PodpisantExecutor, RefLibImpl',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'Podpisant'
     where referenceid           = 12141;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12141,
             '����������',
             4,
             1,
             'RefLibImpl.PodpisantExecutor, RefLibImpl',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'Podpisant');
     end if;
end $$;
--<DO>--
-- 12142. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12142;

    update core_reference
       set description           = '���������� ���� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'SubjectType'
     where referenceid           = 12142;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12142,
             '���������� ���� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'SubjectType');
     end if;
end $$;
--<DO>--
-- 12143. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12143;

    update core_reference
       set description           = '��� ����������� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'TypeBuildingStructure'
     where referenceid           = 12143;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12143,
             '��� ����������� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'TypeBuildingStructure');
     end if;
end $$;
--<DO>--
-- 12144. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12144;

    update core_reference
       set description           = '��������� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'TypeFloors'
     where referenceid           = 12144;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12144,
             '��������� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'TypeFloors');
     end if;
end $$;
--<DO>--
-- 12157. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12157;

    update core_reference
       set description           = '�����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 1,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'Oktmo'
     where referenceid           = 12157;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12157,
             '�����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             1,
             0,
             0,
             0,
             NULL,
             'Oktmo');
     end if;
end $$;
--<DO>--
-- 12158. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12158;

    update core_reference
       set description           = '������ �������� ����������/����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'LoadStatus'
     where referenceid           = 12158;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12158,
             '������ �������� ����������/����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'LoadStatus');
     end if;
end $$;
--<DO>--
-- 12159. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12159;

    update core_reference
       set description           = '������ ��� �� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'PaymentCaseStatus'
     where referenceid           = 12159;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12159,
             '������ ��� �� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'PaymentCaseStatus');
     end if;
end $$;
--<DO>--
-- 12160. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12160;

    update core_reference
       set description           = '������ ������� ��������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'State'
     where referenceid           = 12160;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12160,
             '������ ������� ��������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'State');
     end if;
end $$;
--<DO>--
-- 12161. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12161;

    update core_reference
       set description           = '������������� ���� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'Assftp1'
     where referenceid           = 12161;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12161,
             '������������� ���� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'Assftp1');
     end if;
end $$;
--<DO>--
-- 12162. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12162;

    update core_reference
       set description           = '��� ���������� ���������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'Assftp_cd'
     where referenceid           = 12162;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12162,
             '��� ���������� ���������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'Assftp_cd');
     end if;
end $$;
--<DO>--
-- 12163. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12163;

    update core_reference
       set description           = '������ ����',
           viddoc                = 4,
           readonly              = 0,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'HouseStatus'
     where referenceid           = 12163;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12163,
             '������ ����',
             4,
             0,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'HouseStatus');
     end if;
end $$;
--<DO>--
-- 12164. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12164;

    update core_reference
       set description           = '������� �����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'InsuranceTerms'
     where referenceid           = 12164;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12164,
             '������� �����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'InsuranceTerms');
     end if;
end $$;
--<DO>--
-- 12165. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12165;

    update core_reference
       set description           = '������ ���� ������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'StatusDamageAmount'
     where referenceid           = 12165;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12165,
             '������ ���� ������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'StatusDamageAmount');
     end if;
end $$;
--<DO>--
-- 12166. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12166;

    update core_reference
       set description           = '�������� ������ ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'VerificationCriteria'
     where referenceid           = 12166;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12166,
             '�������� ������ ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'VerificationCriteria');
     end if;
end $$;
--<DO>--
-- 12167. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12167;

    update core_reference
       set description           = '��� ���������-��������� ����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'TypeDocBaseCase'
     where referenceid           = 12167;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12167,
             '��� ���������-��������� ����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'TypeDocBaseCase');
     end if;
end $$;
--<DO>--
-- 12168. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12168;

    update core_reference
       set description           = '��� ������� �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ReestrPayType'
     where referenceid           = 12168;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12168,
             '��� ������� �����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ReestrPayType');
     end if;
end $$;
--<DO>--
-- 12169. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12169;

    update core_reference
       set description           = '������� ������� �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ReestrPayStatus'
     where referenceid           = 12169;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12169,
             '������� ������� �����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ReestrPayStatus');
     end if;
end $$;
--<DO>--
-- 12170. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12170;

    update core_reference
       set description           = '������ �����',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'InvoiceStatus'
     where referenceid           = 12170;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12170,
             '������ �����',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'InvoiceStatus');
     end if;
end $$;
--<DO>--
-- 12171. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12171;

    update core_reference
       set description           = '������� �������� �� ��',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ContractStatus'
     where referenceid           = 12171;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12171,
             '������� �������� �� ��',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ContractStatus');
     end if;
end $$;
--<DO>--
-- 12172. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12172;

    update core_reference
       set description           = '������ �������� ������ ���',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'MfcUploadFileStatus'
     where referenceid           = 12172;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12172,
             '������ �������� ������ ���',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'MfcUploadFileStatus');
     end if;
end $$;
--<DO>--
-- 12173. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12173;

    update core_reference
       set description           = '�������� ������ �������� ���',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'MfcGeneralUploadStatus'
     where referenceid           = 12173;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12173,
             '�������� ������ �������� ���',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'MfcGeneralUploadStatus');
     end if;
end $$;
--<DO>--
-- 12174. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12174;

    update core_reference
       set description           = '������ ������������ ����������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'IdentifyPlatStatus'
     where referenceid           = 12174;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12174,
             '������ ������������ ����������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'IdentifyPlatStatus');
     end if;
end $$;
--<DO>--
-- 12175. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12175;

    update core_reference
       set description           = '��� �������� �� ��������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'ChangeOperationType'
     where referenceid           = 12175;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12175,
             '��� �������� �� ��������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'ChangeOperationType');
     end if;
end $$;
--<DO>--
-- 12176. �������� ������
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- ���� ���������� ����, �� ��������
    select * into r_reference from core_reference t where t.referenceid = 12176;

    update core_reference
       set description           = '������ ��������� ������',
           viddoc                = 4,
           readonly              = 1,
           progid                = 'Core.RefLib.Executors.ReferenceExecutor',
           istree                = 0,
           displaytree           = 0,
           usetreehelper         = 0,
           istable               = 0,
           controlheight         = 0,
           controlwidth          = 0,
           DEFAULTVALUE          = NULL,
           NAME                  = 'FileProcessStatus'
     where referenceid           = 12176;

    -- ���� ���������� �����������, �� ��������
     if(not found)then
          insert into core_reference(
               referenceid,
               description,
               viddoc,
               readonly,
               progid,
               istree,
               displaytree,
               usetreehelper,
               istable,
               controlheight,
               controlwidth,
               DEFAULTVALUE,
               NAME)
          values
            (12176,
             '������ ��������� ������',
             4,
             1,
             'Core.RefLib.Executors.ReferenceExecutor',
             0,
             0,
             0,
             0,
             0,
             0,
             NULL,
             'FileProcessStatus');
     end if;
end $$;
