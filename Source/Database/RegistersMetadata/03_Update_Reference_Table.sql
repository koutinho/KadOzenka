
-- III. Загрузка справочников
--<DO>--
-- 2. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 2;

    update core_reference
       set description           = 'Тип округа',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип округа',
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
-- 4. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 4;

    update core_reference
       set description           = 'Источник данных',
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

    -- Если справочник отсутствует, то добавить
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
             'Источник данных',
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
-- 5. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 5;

    update core_reference
       set description           = 'Субъект РФ',
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

    -- Если справочник отсутствует, то добавить
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
             'Субъект РФ',
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
-- 6. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 6;

    update core_reference
       set description           = 'Адм. округ',
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

    -- Если справочник отсутствует, то добавить
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
             'Адм. округ',
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
-- 7. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 7;

    update core_reference
       set description           = 'Район',
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

    -- Если справочник отсутствует, то добавить
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
             'Район',
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
-- 9. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 9;

    update core_reference
       set description           = 'Район города',
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

    -- Если справочник отсутствует, то добавить
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
             'Район города',
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
-- 10. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 10;

    update core_reference
       set description           = 'Нас. пункт',
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

    -- Если справочник отсутствует, то добавить
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
             'Нас. пункт',
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
-- 11. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 11;

    update core_reference
       set description           = 'Улица',
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

    -- Если справочник отсутствует, то добавить
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
             'Улица',
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
-- 15. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 15;

    update core_reference
       set description           = 'Тип топонима',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип топонима',
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
-- 16. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 16;

    update core_reference
       set description           = 'Тип района',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип района',
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
-- 17. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 17;

    update core_reference
       set description           = 'Тип владения',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип владения',
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
-- 18. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 18;

    update core_reference
       set description           = 'Тип сооружения',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип сооружения',
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
-- 20. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 20;

    update core_reference
       set description           = 'Класс строения',
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

    -- Если справочник отсутствует, то добавить
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
             'Класс строения',
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
-- 21. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 21;

    update core_reference
       set description           = 'Назначение',
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

    -- Если справочник отсутствует, то добавить
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
             'Назначение',
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
-- 22. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 22;

    update core_reference
       set description           = 'Материал стен',
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

    -- Если справочник отсутствует, то добавить
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
             'Материал стен',
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
-- 23. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 23;

    update core_reference
       set description           = 'Состояние адреса',
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

    -- Если справочник отсутствует, то добавить
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
             'Состояние адреса',
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
-- 24. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 24;

    update core_reference
       set description           = 'Статус адреса',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус адреса',
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
-- 25. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 25;

    update core_reference
       set description           = 'Тип документа основания',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип документа основания',
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
-- 26. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 26;

    update core_reference
       set description           = 'Содержание документа',
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

    -- Если справочник отсутствует, то добавить
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
             'Содержание документа',
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
-- 28. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 28;

    update core_reference
       set description           = 'Тип этажа',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип этажа',
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
-- 30. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 30;

    update core_reference
       set description           = 'Класс помещения',
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

    -- Если справочник отсутствует, то добавить
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
             'Класс помещения',
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
-- 31. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 31;

    update core_reference
       set description           = 'Тип помещения',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип помещения',
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
-- 33. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 33;

    update core_reference
       set description           = 'Способ образования ОКС',
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

    -- Если справочник отсутствует, то добавить
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
             'Способ образования ОКС',
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
-- 36. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 36;

    update core_reference
       set description           = 'Назначение комнаты',
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

    -- Если справочник отсутствует, то добавить
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
             'Назначение комнаты',
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
-- 37. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 37;

    update core_reference
       set description           = 'Специальное назначение комнаты',
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

    -- Если справочник отсутствует, то добавить
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
             'Специальное назначение комнаты',
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
-- 38. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 38;

    update core_reference
       set description           = 'Вид площади комнаты',
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

    -- Если справочник отсутствует, то добавить
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
             'Вид площади комнаты',
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
-- 39. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 39;

    update core_reference
       set description           = 'Тип площади комнаты',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип площади комнаты',
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
-- 40. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 40;

    update core_reference
       set description           = 'Понижающий коэффициент',
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

    -- Если справочник отсутствует, то добавить
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
             'Понижающий коэффициент',
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
-- 43. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 43;

    update core_reference
       set description           = 'Тип адресного элемента второго уровня',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип адресного элемента второго уровня',
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
-- 60. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 60;

    update core_reference
       set description           = 'Назначения здания (Росреестр)',
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

    -- Если справочник отсутствует, то добавить
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
             'Назначения здания (Росреестр)',
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
-- 63. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 63;

    update core_reference
       set description           = 'Тип документов по регистрации адреса',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип документов по регистрации адреса',
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
-- 65. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 65;

    update core_reference
       set description           = 'Состояние строения',
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

    -- Если справочник отсутствует, то добавить
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
             'Состояние строения',
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
-- 68. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 68;

    update core_reference
       set description           = 'Серия проекта',
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

    -- Если справочник отсутствует, то добавить
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
             'Серия проекта',
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
-- 69. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 69;

    update core_reference
       set description           = 'Шифр фонда',
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

    -- Если справочник отсутствует, то добавить
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
             'Шифр фонда',
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
-- 70. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 70;

    update core_reference
       set description           = 'ТБТИ',
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

    -- Если справочник отсутствует, то добавить
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
             'ТБТИ',
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
-- 85. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 85;

    update core_reference
       set description           = 'Индексы изменения сметной стоимости СМР',
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

    -- Если справочник отсутствует, то добавить
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
             'Индексы изменения сметной стоимости СМР',
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
-- 86. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 86;

    update core_reference
       set description           = 'Категория дома',
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

    -- Если справочник отсутствует, то добавить
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
             'Категория дома',
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
-- 87. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 87;

    update core_reference
       set description           = 'Характер использования помещения',
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

    -- Если справочник отсутствует, то добавить
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
             'Характер использования помещения',
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
-- 89. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 89;

    update core_reference
       set description           = 'Единица изменения',
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

    -- Если справочник отсутствует, то добавить
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
             'Единица изменения',
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
-- 90. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 90;

    update core_reference
       set description           = 'Тип объекта недвижимости',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип объекта недвижимости',
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
-- 91. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 91;

    update core_reference
       set description           = 'Материал перекрытий',
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

    -- Если справочник отсутствует, то добавить
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
             'Материал перекрытий',
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
-- 92. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 92;

    update core_reference
       set description           = 'Материал кровли',
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

    -- Если справочник отсутствует, то добавить
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
             'Материал кровли',
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
-- 93. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 93;

    update core_reference
       set description           = 'Состояние отселения корпуса',
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

    -- Если справочник отсутствует, то добавить
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
             'Состояние отселения корпуса',
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
-- 114. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 114;

    update core_reference
       set description           = 'Подразделения',
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

    -- Если справочник отсутствует, то добавить
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
             'Подразделения',
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
-- 301. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 301;

    update core_reference
       set description           = 'Источник заполнения',
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

    -- Если справочник отсутствует, то добавить
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
             'Источник заполнения',
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
-- 303. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 303;

    update core_reference
       set description           = 'Статус расчета',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус расчета',
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
-- 304. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 304;

    update core_reference
       set description           = 'Доля ответственности СК',
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

    -- Если справочник отсутствует, то добавить
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
             'Доля ответственности СК',
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
-- 305. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 305;

    update core_reference
       set description           = 'Форма объединения собственников',
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

    -- Если справочник отсутствует, то добавить
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
             'Форма объединения собственников',
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
-- 306. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 306;

    update core_reference
       set description           = 'Признак рассрочки платежа',
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

    -- Если справочник отсутствует, то добавить
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
             'Признак рассрочки платежа',
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
-- 307. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 307;

    update core_reference
       set description           = 'Источник поступления адреса',
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

    -- Если справочник отсутствует, то добавить
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
             'Источник поступления адреса',
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
-- 333. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 333;

    update core_reference
       set description           = 'Тип ФСП',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип ФСП',
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
-- 401. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 401;

    update core_reference
       set description           = 'Субъект РФ - ФИАС',
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

    -- Если справочник отсутствует, то добавить
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
             'Субъект РФ - ФИАС',
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
-- 402. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 402;

    update core_reference
       set description           = 'Адм. округ - ФИАС',
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

    -- Если справочник отсутствует, то добавить
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
             'Адм. округ - ФИАС',
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
-- 403. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 403;

    update core_reference
       set description           = 'Район - ФИАС',
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

    -- Если справочник отсутствует, то добавить
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
             'Район - ФИАС',
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
-- 404. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 404;

    update core_reference
       set description           = 'Город(с/п) - ФИАС',
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

    -- Если справочник отсутствует, то добавить
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
             'Город(с/п) - ФИАС',
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
-- 405. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 405;

    update core_reference
       set description           = 'Район города - ФИАС',
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

    -- Если справочник отсутствует, то добавить
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
             'Район города - ФИАС',
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
-- 406. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 406;

    update core_reference
       set description           = 'Нас. пункт - ФИАС',
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

    -- Если справочник отсутствует, то добавить
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
             'Нас. пункт - ФИАС',
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
-- 407. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 407;

    update core_reference
       set description           = 'Улица - ФИАС',
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

    -- Если справочник отсутствует, то добавить
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
             'Улица - ФИАС',
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
-- 408. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 408;

    update core_reference
       set description           = 'Дом - ФИАС',
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

    -- Если справочник отсутствует, то добавить
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
             'Дом - ФИАС',
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
-- 10079. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 10079;

    update core_reference
       set description           = 'Да / Нет',
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

    -- Если справочник отсутствует, то добавить
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
             'Да / Нет',
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
-- 10167. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 10167;

    update core_reference
       set description           = 'Тип строения',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип строения',
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
-- 12029. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12029;

    update core_reference
       set description           = 'Вид права расширенной части',
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

    -- Если справочник отсутствует, то добавить
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
             'Вид права расширенной части',
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
-- 12093. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12093;

    update core_reference
       set description           = 'Статус идентификации',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус идентификации',
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
-- 12119. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12119;

    update core_reference
       set description           = 'Статус обработки файла',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус обработки файла',
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
-- 12120. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12120;

    update core_reference
       set description           = 'Код типа файла',
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

    -- Если справочник отсутствует, то добавить
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
             'Код типа файла',
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
-- 12121. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12121;

    update core_reference
       set description           = 'Источник данных для страхования',
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

    -- Если справочник отсутствует, то добавить
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
             'Источник данных для страхования',
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
-- 12122. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12122;

    update core_reference
       set description           = 'Тип договора',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип договора',
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
-- 12123. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12123;

    update core_reference
       set description           = 'Тип документа',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип документа',
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
-- 12124. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12124;

    update core_reference
       set description           = 'Типы плит',
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

    -- Если справочник отсутствует, то добавить
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
             'Типы плит',
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
-- 12125. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12125;

    update core_reference
       set description           = 'Причины ущерба для ЖП',
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

    -- Если справочник отсутствует, то добавить
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
             'Причины ущерба для ЖП',
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
-- 12126. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12126;

    update core_reference
       set description           = 'Элементы конструкций',
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

    -- Если справочник отсутствует, то добавить
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
             'Элементы конструкций',
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
-- 12127. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12127;

    update core_reference
       set description           = 'Материал пола',
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

    -- Если справочник отсутствует, то добавить
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
             'Материал пола',
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
-- 12128. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12128;

    update core_reference
       set description           = 'Причины отказа в страховой выплате по договору страхования жилого помещения',
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

    -- Если справочник отсутствует, то добавить
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
             'Причины отказа в страховой выплате по договору страхования жилого помещения',
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
-- 12129. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12129;

    update core_reference
       set description           = 'Причины отказа в страховой выплате по договору страхования общего имущества',
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

    -- Если справочник отсутствует, то добавить
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
             'Причины отказа в страховой выплате по договору страхования общего имущества',
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
-- 12130. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12130;

    update core_reference
       set description           = 'Причины отсутствия решения о страховой выплате по договору страхования общего имущества',
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

    -- Если справочник отсутствует, то добавить
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
             'Причины отсутствия решения о страховой выплате по договору страхования общего имущества',
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
-- 12131. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12131;

    update core_reference
       set description           = 'Тип жилого помещения',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип жилого помещения',
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
-- 12132. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12132;

    update core_reference
       set description           = 'Типы зданий',
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

    -- Если справочник отсутствует, то добавить
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
             'Типы зданий',
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
-- 12133. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12133;

    update core_reference
       set description           = 'Тип собственности',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип собственности',
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
-- 12134. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12134;

    update core_reference
       set description           = 'Подпричины ущерба по ЖП',
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

    -- Если справочник отсутствует, то добавить
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
             'Подпричины ущерба по ЖП',
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
-- 12135. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12135;

    update core_reference
       set description           = 'Уточнение подпричин ущерба по ЖП',
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

    -- Если справочник отсутствует, то добавить
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
             'Уточнение подпричин ущерба по ЖП',
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
-- 12136. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12136;

    update core_reference
       set description           = 'Причины ущерба для ОИ',
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

    -- Если справочник отсутствует, то добавить
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
             'Причины ущерба для ОИ',
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
-- 12141. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12141;

    update core_reference
       set description           = 'Подписанты',
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

    -- Если справочник отсутствует, то добавить
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
             'Подписанты',
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
-- 12142. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12142;

    update core_reference
       set description           = 'Справочник «Тип субъекта»',
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

    -- Если справочник отсутствует, то добавить
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
             'Справочник «Тип субъекта»',
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
-- 12143. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12143;

    update core_reference
       set description           = 'Тип конструкции строения',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип конструкции строения',
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
-- 12144. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12144;

    update core_reference
       set description           = 'Этажность строения',
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

    -- Если справочник отсутствует, то добавить
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
             'Этажность строения',
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
-- 12157. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12157;

    update core_reference
       set description           = 'ОКТМО',
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

    -- Если справочник отсутствует, то добавить
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
             'ОКТМО',
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
-- 12158. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12158;

    update core_reference
       set description           = 'Статус загрузки начисления/зачисления',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус загрузки начисления/зачисления',
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
-- 12159. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12159;

    update core_reference
       set description           = 'Статус дел на оплату',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус дел на оплату',
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
-- 12160. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12160;

    update core_reference
       set description           = 'Статус объекта Росреест',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус объекта Росреест',
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
-- 12161. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12161;

    update core_reference
       set description           = 'Классификатор типа помещения',
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

    -- Если справочник отсутствует, то добавить
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
             'Классификатор типа помещения',
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
-- 12162. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12162;

    update core_reference
       set description           = 'Код назначения помещения',
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

    -- Если справочник отсутствует, то добавить
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
             'Код назначения помещения',
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
-- 12163. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12163;

    update core_reference
       set description           = 'Статус Дома',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус Дома',
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
-- 12164. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12164;

    update core_reference
       set description           = 'Условия страхования',
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

    -- Если справочник отсутствует, то добавить
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
             'Условия страхования',
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
-- 12165. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12165;

    update core_reference
       set description           = 'Статус дела расчета ущерба',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус дела расчета ущерба',
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
-- 12166. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12166;

    update core_reference
       set description           = 'Критерии сверки данных',
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

    -- Если справочник отсутствует, то добавить
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
             'Критерии сверки данных',
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
-- 12167. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12167;

    update core_reference
       set description           = 'Тип документа-основания дела',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип документа-основания дела',
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
-- 12168. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12168;

    update core_reference
       set description           = 'Тип реестра оплат',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип реестра оплат',
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
-- 12169. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12169;

    update core_reference
       set description           = 'Статусы реестра оплат',
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

    -- Если справочник отсутствует, то добавить
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
             'Статусы реестра оплат',
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
-- 12170. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12170;

    update core_reference
       set description           = 'Статус счета',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус счета',
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
-- 12171. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12171;

    update core_reference
       set description           = 'Статусы договора по ОИ',
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

    -- Если справочник отсутствует, то добавить
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
             'Статусы договора по ОИ',
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
-- 12172. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12172;

    update core_reference
       set description           = 'Статус загрузки файлов МФЦ',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус загрузки файлов МФЦ',
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
-- 12173. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12173;

    update core_reference
       set description           = 'Основной статус загрузки МФЦ',
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

    -- Если справочник отсутствует, то добавить
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
             'Основной статус загрузки МФЦ',
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
-- 12174. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12174;

    update core_reference
       set description           = 'Статус идентифкации зачислений',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус идентифкации зачислений',
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
-- 12175. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12175;

    update core_reference
       set description           = 'Тип операции по изменению данных',
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

    -- Если справочник отсутствует, то добавить
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
             'Тип операции по изменению данных',
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
-- 12176. Источник данных
DO $$
declare
    r_reference core_reference%ROWTYPE;
begin

    -- Если справочник есть, то обновить
    select * into r_reference from core_reference t where t.referenceid = 12176;

    update core_reference
       set description           = 'Статус обработки файлов',
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

    -- Если справочник отсутствует, то добавить
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
             'Статус обработки файлов',
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
